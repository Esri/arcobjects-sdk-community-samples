using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

/*
    This sample opens a Mosaic Dataset and goes through each row in the attribute table.
    It checks whether the dataset in the row has any band information (band properties) 
    associated with it. 
    If the item has no band information, it inserts band properties for the first 3 bands 
    in the item (if the item has 3 or more bands).
    Finally the mosaic dataset product definition is changed to Natural Color RGB so that 
    ArcGIS can use the band names of the mosaic dataset.
    The sample also shows how to set a key property on the mosaic dataset.

    The sample has functions to get/set any key property for a dataset.
    The sample has functions to get/set any band property for a dataset.
    The sample has functions to get all the properties and all the band properties 
    for a dataset.

    Key Properties:

    Key Properties of type 'double':
    CloudCover
    SunElevation
    SunAzimuth
    SensorElevation
    SensorAzimuth
    OffNadir
    VerticalAccuracy
    HorizontalAccuracy
    LowCellSize
    HighCellSize
    MinCellSize
    MaxCellSize

    Key Properties of type 'date':
    AcquisitionDate

    Key Properties of type 'string':
    SensorName
    ParentRasterType
    DataType
    ProductName
    DatasetTag
*/

namespace RasterSamples
{
    class GetSetKeyProperty
    {
        [STAThread]
        static void Main(string[] args)
        {
            #region Initialize
            ESRI.ArcGIS.esriSystem.AoInitialize aoInit = null;
            try
            {
                Console.WriteLine("Obtaining license");
                ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.Desktop);
                aoInit = new AoInitialize();
                esriLicenseStatus licStatus = aoInit.Initialize(esriLicenseProductCode.esriLicenseProductCodeAdvanced);
                Console.WriteLine("Ready with license.");
            }
            catch (Exception exc)
            {
                // If it fails at this point, shutdown the test and ignore any subsequent errors.
                Console.WriteLine(exc.Message);
            }
            #endregion

            try
            {
                // Input database and Mosaic Dataset
                string MDWorkspaceFolder = @"e:\md\Samples\GetSetKP\RasterSamples.gdb";
                string MDName = @"LAC";

                // Command line setting of above input if provided.
                string[] commandLineArgs = Environment.GetCommandLineArgs();
                if (commandLineArgs.GetLength(0) > 1)
                {
                    MDWorkspaceFolder = commandLineArgs[1];
                    MDName = commandLineArgs[2];
                }

                // Open MD
                Type factoryType = Type.GetTypeFromProgID("esriDataSourcesGDB.FileGDBWorkspaceFactory");
                IWorkspaceFactory mdWorkspaceFactory = (IWorkspaceFactory)Activator.CreateInstance(factoryType);
                IWorkspace mdWorkspace = mdWorkspaceFactory.OpenFromFile(MDWorkspaceFolder, 0);
                IRasterWorkspaceEx workspaceEx = (IRasterWorkspaceEx)(mdWorkspace);
                IMosaicDataset mosaicDataset = (IMosaicDataset)workspaceEx.OpenRasterDataset(MDName);

                // Set Mosaic Dataset item information.
                SetMosaicDatasetItemInformation(mosaicDataset);

                // Set Key Property 'DataType' on the Mosaic Dataset to value 'Processed'
                // The change will be reflected on the 'General' page of the mosaic dataset
                // properties under the 'Source Type' property.
                SetKeyProperty((IDataset)mosaicDataset, "DataType", "Processed");

                // Set the Product Definition on the Mosaic Dataset to 'NATURAL_COLOR_RGB'
                // First set the 'BandDefinitionKeyword' key property to natural color RGB.
                SetKeyProperty((IDataset)mosaicDataset, "BandDefinitionKeyword", "NATURAL_COLOR_RGB");
                // Then set band names and wavelengths on the mosaic dataset.
                SetBandProperties((IDataset)mosaicDataset);
                // Last and most important, refresh the mosaic dataset so the changes are saved.
                ((IRasterDataset3)mosaicDataset).Refresh();

                #region Shutdown
                Console.WriteLine("Success.");
                Console.WriteLine("Press any key...");
                Console.ReadKey();

                // Shutdown License
                aoInit.Shutdown();
                #endregion
            }
            catch (Exception exc)
            {
                #region Shutdown
                Console.WriteLine("Exception Caught while creating Function Raster Dataset. " + exc.Message);
                Console.WriteLine("Failed.");
                Console.WriteLine("Press any key...");
                Console.ReadKey();

                // Shutdown License
                aoInit.Shutdown();
                #endregion
            }
        }
        
        /// <summary>
        /// Sets band information on items in a mosaic dataset
        /// </summary>
        /// <param name="md">The mosaic dataset with the items</param>
        private static void SetMosaicDatasetItemInformation(IMosaicDataset md)
        {
            // Get the Attribute table from the Mosaic Dataset.
            IFeatureClass featureClass = md.Catalog;
            ISchemaLock schemaLock = (ISchemaLock)featureClass;
            IRasterDataset3 rasDs = null;
            try
            {
                // A try block is necessary, as an exclusive lock might not be available.
                schemaLock.ChangeSchemaLock(esriSchemaLock.esriExclusiveSchemaLock);

                // Get an update cursor going through all the rows in the Moasic Dataset.
                IFeatureCursor fcCursor = featureClass.Update(null, false);
                // Alternatively, a read cursor can be used if the item does not need to be changed.
                // featureClass.Search(null, false);

                // For each row,
                IRasterCatalogItem rasCatItem = (IRasterCatalogItem)fcCursor.NextFeature();
                while (rasCatItem != null)
                {
                    // get the functionrasterdataset from the Raster field.
                    IFunctionRasterDataset funcDs = (IFunctionRasterDataset)rasCatItem.RasterDataset;
                    if (funcDs != null)
                    {
                        // Check if the 'BandName' property exists in the dataset.
                        bool propertyExists = false;
                        for (int bandID = 0; bandID < funcDs.RasterInfo.BandCount; ++bandID)
                        {
                            object bandNameProperty = null;
                            bandNameProperty = GetBandProperty((IDataset)funcDs, "BandName", bandID);
                            if (bandNameProperty != null)
                                propertyExists = true;
                        }
                        if (propertyExists == false && funcDs.RasterInfo.BandCount > 2)
                        {
                            // If the property does not exist and the dataset has atleast 3 bands,
                            // set Band Definition Properties for first 3 bands of the dataset.
                            SetBandProperties((IDataset)funcDs);
                            funcDs.AlterDefinition();
                            rasDs = (IRasterDataset3)funcDs;
                            // Refresh the dataset.
                            rasDs.Refresh();
                        }
                    }
                    fcCursor.UpdateFeature((IFeature)rasCatItem);
                    rasDs = null;
                    rasCatItem = (IRasterCatalogItem)fcCursor.NextFeature();
                }
                rasCatItem = null;
                fcCursor = null;
                featureClass = null;
            }
            catch (Exception exc) { Console.WriteLine("Exception Caught in SetMosaicDatasetItemInformation: " + exc.Message); }
            finally
            {
                // Set the lock to shared, whether or not an error occurred.
                schemaLock.ChangeSchemaLock(esriSchemaLock.esriSharedSchemaLock);
            }
        }

        /// <summary>
        /// Sets band properties on a given dataset
        /// </summary>
        /// <param name="ds">The target dataset</param>
        private static void SetBandProperties(IDataset dataset)
        {
            try
            {
                // Set properties for Band 1.
                SetBandProperty(dataset, "BandName", 0, "Red");
                SetBandProperty(dataset, "WavelengthMin", 0, 630);
                SetBandProperty(dataset, "WavelengthMax", 0, 690);

                // Set properties for Band 2.
                SetBandProperty(dataset, "BandName", 1, "Green");
                SetBandProperty(dataset, "WavelengthMin", 1, 530);
                SetBandProperty(dataset, "WavelengthMax", 1, 570);

                // Set properties for Band 3.
                SetBandProperty(dataset, "BandName", 2, "Blue");
                SetBandProperty(dataset, "WavelengthMin", 2, 440);
                SetBandProperty(dataset, "WavelengthMax", 2, 480);
            }
            catch (Exception) { }
        }

        /// <summary>
        /// Get all the properties associated with the dataset.
        /// </summary>
        /// <param name="dataset">Dataset to get the property from.</param>
        /// <param name="allKeys">String Array passed in by reference to fill with all keys.</param>
        /// <param name="allProperties">Object array passed in by reference to fill with all properties.</param>
        static void GetAllProperties(IDataset dataset, ref IStringArray allKeys, ref IVariantArray allProperties)
        {
            IRasterKeyProperties rasterKeyProps = (IRasterKeyProperties)dataset;
            rasterKeyProps.GetAllProperties(out allKeys, out allProperties);
        }

        /// <summary>
        /// Get all the properties associated with a particular band of the dataset.
        /// </summary>
        /// <param name="dataset">Dataset to get the property from.</param>
        /// <param name="bandIndex">band for which to get all properties.</param>
        /// <param name="bandKeys">String Array passed in by reference to fill with all keys.</param>
        /// <param name="bandProperties">Object array passed in by reference to fill with all properties.</param>
        static void GetAllBandProperties(IDataset dataset, int bandIndex, ref IStringArray bandKeys, ref IVariantArray bandProperties)
        {
            IRasterKeyProperties rasterKeyProps = (IRasterKeyProperties)dataset;
            rasterKeyProps.GetAllBandProperties(bandIndex, out bandKeys, out bandProperties);
        }

        /// <summary>
        /// Get the Key Property corresponding to the key 'key' from the dataset.
        /// </summary>
        /// <param name="dataset">Dataset to get the property from.</param>
        /// <param name="key">The key for which to get the value.</param>
        /// <returns>Property corresponding to the key or null if it doesnt exist.</returns>
        static object GetKeyProperty(IDataset dataset, string key)
        {
            IRasterKeyProperties rasterKeyProps = (IRasterKeyProperties)dataset;
            object value = null;
            try { value = rasterKeyProps.GetProperty(key); }
            catch (Exception) { }
            return value;
        }

        /// <summary>
        /// Set the Key Property 'value' corresponding to the key 'key' on the dataset.
        /// </summary>
        /// <param name="dataset">Dataset to set the property on.</param>
        /// <param name="key">The key on which to set the property.</param>
        /// <param name="value">The value to set.</param>
        static void SetKeyProperty(IDataset dataset, string key, object value)
        {
            IRasterKeyProperties rasterKeyProps = (IRasterKeyProperties)dataset;
            rasterKeyProps.SetProperty(key, value);
        }

        /// <summary>
        /// Get the KeyProperty corresponding to the bandIndex and 'key' from the dataset.
        /// </summary>
        /// <param name="dataset">Dataset to get the property from.</param>
        /// <param name="key">The key for which to get the value.</param>
        /// <param name="bandIndex">Band for which to get the property.</param>
        /// <returns>Property corresponding to the key or null if none found.</returns>
        static object GetBandProperty(IDataset dataset, string key, int bandIndex)
        {
            IRasterKeyProperties rasterKeyProps = (IRasterKeyProperties)dataset;
            object value = null;
            try { value = rasterKeyProps.GetBandProperty(key, bandIndex); }
            catch (Exception) { }

            return value;
        }

        /// <summary>
        /// Set the KeyProperty corresponding to the bandIndex and 'key' from the dataset.
        /// </summary>
        /// <param name="dataset">Dataset to set the property on.</param>
        /// <param name="key">The key on which to set the property.</param>
        /// <param name="bandIndex">Band from which to get the property.</param>
        /// <param name="value">The value to set.</param>
        static void SetBandProperty(IDataset dataset, string key, int bandIndex, object value)
        {
            IRasterKeyProperties rasterKeyProps = (IRasterKeyProperties)dataset;
            rasterKeyProps.SetBandProperty(key, bandIndex, value);
        }
    }
}
