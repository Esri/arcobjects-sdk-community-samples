using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.DataSourcesRaster;
using CustomRasterBuilder;
namespace RasterTest
{
    public class ThumbnailBuilderTest
    {
        [STAThread]
        public static void Main(string[] args)
        {
            ESRI.ArcGIS.esriSystem.AoInitialize aoInit;

            #region Initialize Licensing
            try
            {
                Console.WriteLine("Obtaining License");
                ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.Desktop);
                aoInit = new AoInitializeClass();
                esriLicenseStatus licStatus = aoInit.Initialize(esriLicenseProductCode.esriLicenseProductCodeAdvanced);
                Console.WriteLine("Ready with license");
            }
            catch (Exception exc)
            {
                
                // If it fails at this point, shutdown the test and ignore any subsequent errors.
                Console.WriteLine(exc.Message);
                return;
            }
            #endregion

            try
            {
                #region Specify input directory and dataset name
                // Specify where to create the File Gdb
                string fgdbFolder = @"c:\temp\CustomRasterType";
                // Specify the folder to add the data from
                string dataSource = @"c:\data\RasterDatasets";
                // Specify whether to DELETE the fgdbFolder and create it again (clearing it).
                bool clearFGdbFolder = false;

                // Specify whether to save the Custom Raster Type generated to an art file.
                bool saveToArtFile = true;
                // Specify the path and filename to save the custom type.
                string customTypeFilePath = @"C:\temp\ThumbnailType.art";
                #endregion

                #region Raster Type Parameters
                string rasterTypeName = @"Thumbnail Raster Dataset";
                // Specify the file filter to use to add data (Optional)
                string dataSourceFilter = "*.tif";
                string rasterTypeProductFilter = @"";
                string rasterTypeProductName = @"";
                #endregion

                TestThumbnailBuilder(rasterTypeName, rasterTypeProductFilter, rasterTypeProductName,
                    dataSource, dataSourceFilter, fgdbFolder, saveToArtFile, customTypeFilePath, clearFGdbFolder);

                #region Shutdown
                Console.WriteLine("Press any key...");
                Console.ReadKey();
                aoInit.Shutdown();
                #endregion
            }
            catch (Exception exc)
            {
                #region Shutdown
                Console.WriteLine("Exception Caught in Main: " + exc.Message);
                Console.WriteLine("Failed.");
                Console.WriteLine("Shutting down.");
                Console.WriteLine("Press any key...");
                Console.ReadKey();

                // Shutdown License
                aoInit.Shutdown();
                #endregion
            }
        }

        public static void TestThumbnailBuilder(string rasterTypeName, string rasterTypeProductFilter,
            string rasterTypeProductName, string dataSource, string dataSourceFilter, string fgdbParentFolder,
            bool saveToArt, string customTypeFilePath, bool clearGdbDirectory)
        {
            try
            {
                string[] rasterProductNames = rasterTypeProductName.Split(';');
                string nameString = rasterTypeName.Replace(" ", "") + rasterTypeProductFilter.Replace(" ", "") +
                    rasterProductNames[0].Replace(" ", "");

                #region Directory Declarations
                string fgdbName = nameString + ".gdb";
                string fgdbDir = fgdbParentFolder + "\\" + fgdbName;
                string MosaicDatasetName = nameString + "MD";
                #endregion

                #region Global Declarations
                IMosaicDataset theMosaicDataset = null;
                IMosaicDatasetOperation theMosaicDatasetOperation = null;
                IMosaicWorkspaceExtensionHelper mosaicExtHelper = null;
                IMosaicWorkspaceExtension mosaicExt = null;
                #endregion

                #region Create File GDB
                Console.WriteLine("Creating File GDB: " + fgdbName);
                if (clearGdbDirectory)
                {
                    try
                    {
                        Console.WriteLine("Emptying Gdb folder.");
                        System.IO.Directory.Delete(fgdbParentFolder, true);
                        System.IO.Directory.CreateDirectory(fgdbParentFolder);
                    }
                    catch (System.IO.IOException EX)
                    {
                        Console.WriteLine(EX.Message);
                        return;
                    }
                }

                // Create a File Gdb
                Type factoryType = Type.GetTypeFromProgID("esriDataSourcesGDB.FileGDBWorkspaceFactory");
                IWorkspaceFactory FgdbFactory = (IWorkspaceFactory)Activator.CreateInstance(factoryType);
                FgdbFactory.Create(fgdbParentFolder,
                    fgdbName, null, 0);
                #endregion

                #region Create Mosaic Dataset
                try
                {
                    Console.WriteLine("Create Mosaic Dataset: " + MosaicDatasetName);
                    // Setup workspaces.
                    IWorkspaceFactory workspaceFactory = (IWorkspaceFactory)Activator.CreateInstance(factoryType);
                    IWorkspace fgdbWorkspace = workspaceFactory.OpenFromFile(fgdbDir, 0);
                    // Create Srs
                    ISpatialReferenceFactory spatialrefFactory = new SpatialReferenceEnvironmentClass();
                    ISpatialReference mosaicSrs = spatialrefFactory.CreateProjectedCoordinateSystem(
                        (int)(esriSRProjCSType.esriSRProjCS_World_Mercator));
                    // Create the mosaic dataset creation parameters object.
                    ICreateMosaicDatasetParameters creationPars = new CreateMosaicDatasetParametersClass();
                    // Create the mosaic workspace extension helper class.
                    mosaicExtHelper = new MosaicWorkspaceExtensionHelperClass();
                    // Find the right extension from the workspace.
                    mosaicExt = mosaicExtHelper.FindExtension(fgdbWorkspace);
                    // Use the extension to create a new mosaic dataset, supplying the 
                    // spatial reference and the creation parameters object created above.
                    theMosaicDataset = mosaicExt.CreateMosaicDataset(MosaicDatasetName,
                        mosaicSrs, creationPars, "");
                    theMosaicDatasetOperation = (IMosaicDatasetOperation)(theMosaicDataset);
                }
                catch (Exception exc)
                {
                    Console.WriteLine("Error: Failed to create Mosaic Dataset : {0}.", 
                        MosaicDatasetName + " " + exc.Message);
                    return;
                }
                #endregion

                #region Create Custom Raster Type
                Console.WriteLine("Preparing Raster Type");
                // Create a Raster Type Name object.
                IRasterTypeName theRasterTypeName = new RasterTypeNameClass();
                // Assign the name of the Raster Type to the name object.
                // The Name field accepts a path to an .art file as well 
                // the name for a built in Raster Type.
                theRasterTypeName.Name = rasterTypeName;
                // Use the Open function from the IName interface to get the Raster Type object.
                IRasterType theRasterType = (IRasterType)(((IName)theRasterTypeName).Open());
                if (theRasterType == null)
                {
                    Console.WriteLine("Error:Raster Type not found " + rasterTypeName);
                    return;
                }
                #endregion

                #region Prepare Raster Type
                // Set the URI Filter on the loaded raster type.
                if (rasterTypeProductFilter != "")
                {
                    // Get the supported URI filters from the raster type object using the 
                    // raster type properties interface.
                    IArray mySuppFilters = ((IRasterTypeProperties)theRasterType).SupportedURIFilters;
                    IItemURIFilter productFilter = null;
                    for (int i = 0; i < mySuppFilters.Count; ++i)
                    {
                        // Set the desired filter from the supported filters.
                        productFilter = (IItemURIFilter)mySuppFilters.get_Element(i);
                        if (productFilter.Name == rasterTypeProductFilter)
                            theRasterType.URIFilter = productFilter;
                    }
                }
                // Enable the correct templates in the raster type.
                bool enableTemplate = false;
                if (rasterProductNames.Length >= 1 && (rasterProductNames[0] != ""))
                {
                    // Get the supported item templates from the raster type.
                    IItemTemplateArray templateArray = theRasterType.ItemTemplates;
                    for (int i = 0; i < templateArray.Count; ++i)
                    {
                        // Go through the supported item templates and enable the ones needed.
                        IItemTemplate template = templateArray.get_Element(i);
                        enableTemplate = false;
                        for (int j = 0; j < rasterProductNames.Length; ++j)
                            if (template.Name == rasterProductNames[j])
                                enableTemplate = true;
                        if (enableTemplate)
                            template.Enabled = true;
                        else
                            template.Enabled = false;
                    }
                }
                ((IRasterTypeProperties)theRasterType).DataSourceFilter = dataSourceFilter;
                #endregion

                #region Save Custom Raster Type
                if (saveToArt)
                {
                    IRasterTypeProperties rasterTypeProperties = (IRasterTypeProperties)theRasterType;
                    IRasterTypeEnvironment rasterTypeHelper = new RasterTypeEnvironmentClass();
                    rasterTypeProperties.Name = customTypeFilePath;

                    IMemoryBlobStream ipBlob = rasterTypeHelper.SaveRasterType(theRasterType);
                    ipBlob.SaveToFile(customTypeFilePath);
                }
                #endregion

                #region Preparing Data Source Crawler
                Console.WriteLine("Preparing Data Source Crawler");
                // Create a new property set to specify crawler properties.
                IPropertySet crawlerProps = new PropertySetClass();
                // Specify a file filter
                crawlerProps.SetProperty("Filter", dataSourceFilter);
                // Specify whether to search subdirectories.
                crawlerProps.SetProperty("Recurse", true);
                // Specify the source path.
                crawlerProps.SetProperty("Source", dataSource);
                // Get the recommended crawler from the raster type based on the specified 
                // properties using the IRasterBuilder interface.
                // Pass on the Thumbnailtype to the crawler...
                IDataSourceCrawler theCrawler = ((IRasterBuilder)theRasterType).GetRecommendedCrawler(crawlerProps);
                #endregion

                #region Add Rasters
                try
                {                    
                    Console.WriteLine("Adding Rasters");
                    // Create a AddRaster parameters object.
                    IAddRastersParameters AddRastersArgs = new AddRastersParametersClass();
                    // Specify the data crawler to be used to crawl the data.
                    AddRastersArgs.Crawler = theCrawler;
                    // Specify the Thumbnail raster type to be used to add the data.
                    AddRastersArgs.RasterType = theRasterType;
                    // Use the mosaic dataset operation interface to add 
                    // rasters to the mosaic dataset.
                    theMosaicDatasetOperation.AddRasters(AddRastersArgs, null);
                }
                catch (Exception ex)
                {                    
                    Console.WriteLine("Error: Add raster Failed." + ex.Message);
                    return;
                }
                #endregion

                #region Compute Pixel Size Ranges
                Console.WriteLine("Computing Pixel Size Ranges.");
                try
                {
                    // Create a calculate cellsize ranges parameters object.
                    ICalculateCellSizeRangesParameters computeArgs = new CalculateCellSizeRangesParametersClass();
                    // Use the mosaic dataset operation interface to calculate cellsize ranges.
                    theMosaicDatasetOperation.CalculateCellSizeRanges(computeArgs, null);
                }
                catch (Exception ex)
                {                    
                    Console.WriteLine("Error: Compute Pixel Size Failed." + ex.Message);
                    return;
                }
                #endregion

                #region Building Boundary
                Console.WriteLine("Building Boundary");
                try
                {
                    // Create a build boundary parameters object.
                    IBuildBoundaryParameters boundaryArgs = new BuildBoundaryParametersClass();
                    // Set flags that control boundary generation.
                    boundaryArgs.AppendToExistingBoundary = true;
                    // Use the mosaic dataset operation interface to build boundary.
                    theMosaicDatasetOperation.BuildBoundary(boundaryArgs, null);
                }
                catch (Exception ex)
                {                    
                    Console.WriteLine("Error: Build Boundary Failed." + ex.Message);
                    return;
                }
                #endregion

                #region Report
                Console.WriteLine("Successfully created MD: " + MosaicDatasetName + ". ");
                #endregion
            }
            catch (Exception exc)
            {                
                #region Report
                Console.WriteLine("Exception Caught in TestThumbnailBuilder: " + exc.Message);
                Console.WriteLine("Failed.");
                Console.WriteLine("Shutting down.");
                #endregion
            }
        }
    }
}
