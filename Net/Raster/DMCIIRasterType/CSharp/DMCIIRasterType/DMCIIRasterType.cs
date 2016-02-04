/*

   Copyright 2016 Esri

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.

   See the License for the specific language governing permissions and
   limitations under the License.

*/
using System;
using System.IO;
using System.Xml;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Carto;

/*
 * 
 * This sample shows how to implement a Custom Raster Type to provide support for DMCII data. 
 * Also provided is an optional test application to create a Geodatabase and a mosaic dataset 
 * and add data using the custom type.
 * The main interface to be implemented is the IRasterBuilder interface along with 
 * secondary interfaces such as IRasterBuilderInit (which provides access to the parent MD),
 * IPersistvariant (which implements persistence), IRasterBuilderInit2 and IRasterBuilder2 
 * (new interfaces added at 10.1).
 * A IRasterFactory implementation also needs to be created in order for the Raster type to 
 * show up in the list of Raster Types in the Add Rasters GP Tool. The factory is responsible 
 * for creating the raster type object and setting some properties on it. It also enables the 
 * use of the Raster Product.
 * 
 */


namespace SampleRasterType
{
    [Guid("5DEF8E3C-51E9-49af-A3BE-EF8C68A4BBBE")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("SampleRasterType.CustomRasterTypeFactory")]
    [ComVisible(true)]
    public class DMCIIRasterTypeFactory : IRasterTypeFactory
    {
        #region Private Members
        IStringArray myRasterTypeNames; // List of Raster Types that the factory can create.
        UID myUID; // UID for the DMCII Raster Type.
        #endregion

        #region IRasterTypeFactory Members

        public DMCIIRasterTypeFactory()
        {
            string rasterTypeName = "DMCII Raster Type";
            myRasterTypeNames = new StrArrayClass();
            myRasterTypeNames.Add(rasterTypeName);

            myUID = new UIDClass();
            myUID.Value = "{5DEF8E3C-51E9-49af-A3BE-EF8C68A4BBBE}";
        }

        public UID CLSID
        {
            get { return myUID; }
        }

        /// <summary>
        /// Create a Raster Type object given the name of the raster type (usually 
        /// the same name as the one in the UI list of raster types).
        /// </summary>
        /// <param name="RasterTypeName">Name of the Raster Type object to create.</param>
        /// <returns>The Raster type object.</returns>
        public IRasterType CreateRasterType(string RasterTypeName)
        {
            // Create a new RasterType object and its corresponding name object.
            IRasterType theRasterType = new RasterTypeClass();
            IRasterTypeName theRasterTypeName = new RasterTypeNameClass();
            theRasterTypeName.Name = RasterTypeName;
            theRasterType.FullName = (IName)theRasterTypeName;

            // Set the properties for the raster type object. These are shown in the 
            // 'General' tab of the raster type properties page.
            ((IRasterTypeProperties)theRasterType).Name = "DMCII Raster Type";
            ((IRasterTypeProperties)theRasterType).Description = "Raster Type for DMCII data.";
            ((IRasterTypeProperties)theRasterType).DataSourceFilter = "*.dim";
            ((IRasterTypeProperties)theRasterType).SupportsOrthorectification = true;

            // Create the Custom Raster Builder object
            IRasterBuilder customRasterBuilder = new DMCIIRasterBuilder();
            // Set the Raster Builder of theRasterType to the above created builder.
            theRasterType.RasterBuilder = customRasterBuilder;

            // Enable the use of the Raster Type as a Raster Product.
            ((IRasterTypeProperties2)theRasterType).IsSensorRasterType = true;

            #region Set Product Templates
            // Create a new array of templates if needed.
            if (theRasterType.ItemTemplates == null)
                theRasterType.ItemTemplates = new ItemTemplateArrayClass();

            // Add a 'Raw' template.
            IItemTemplate nullTemplate = new ItemTemplateClass();
            nullTemplate.Enabled = false;
            nullTemplate.Name = "Raw";
            ((IItemTemplate2)nullTemplate).IsSensorTemplate = true;
            ((IItemTemplate2)nullTemplate).SupportsEnhancement = false;
            theRasterType.ItemTemplates.Add(nullTemplate);

            // Add a 'Stretch' template. This is the default template.
            IItemTemplate strTemplate = new ItemTemplateClass();
            strTemplate.Enabled = true;
            strTemplate.Name = "Stretch";
            IRasterFunction stretchFunction = new StretchFunctionClass();
            IStretchFunctionArguments stretchFunctionArgs = new StretchFunctionArgumentsClass();
            stretchFunctionArgs.StretchType = esriRasterStretchType.esriRasterStretchMinimumMaximum;
            IRasterFunctionVariable rasterVar = new RasterFunctionVariableClass();
            rasterVar.IsDataset = true;
            rasterVar.Name = "MS";
            rasterVar.Aliases = new StrArrayClass();
            rasterVar.Aliases.Add("MS");
            rasterVar.Description = "Variable for input raster";
            stretchFunctionArgs.Raster = rasterVar;
            IRasterFunctionTemplate stretchFunctionTemplate = new RasterFunctionTemplateClass();
            stretchFunctionTemplate.Function = stretchFunction;
            stretchFunctionTemplate.Arguments = stretchFunctionArgs;
            strTemplate.RasterFunctionTemplate = stretchFunctionTemplate;
            ((IItemTemplate2)strTemplate).IsSensorTemplate = true;
            ((IItemTemplate2)strTemplate).SupportsEnhancement = true;
            theRasterType.ItemTemplates.Add(strTemplate);
            #endregion

            #region Set Product Types
            // Add Product types (called URI filters in the code).
            if (((IRasterTypeProperties)theRasterType).SupportedURIFilters == null)
                ((IRasterTypeProperties)theRasterType).SupportedURIFilters = new ArrayClass();
            // Create and setup URI Filters
            IItemURIFilter allFilter = new URIProductNameFilterClass();
            allFilter.Name = "All";
            allFilter.SupportsOrthorectification = true;
            allFilter.SupportedTemplateNames = new StrArrayClass();
            allFilter.SupportedTemplateNames.Add("Raw");
            allFilter.SupportedTemplateNames.Add("Stretch");
            IStringArray allProductNames = new StrArrayClass();
            allProductNames.Add("L1T");
            allProductNames.Add("L1R");
            ((IURIProductNameFilter)allFilter).ProductNames = allProductNames;

            // The L1T filter does not support orthorectification.
            IItemURIFilter l1tFilter = new URIProductNameFilterClass();
            l1tFilter.Name = "L1T";
            l1tFilter.SupportsOrthorectification = false;
            l1tFilter.SupportedTemplateNames = new StrArrayClass();
            l1tFilter.SupportedTemplateNames.Add("Raw");
            l1tFilter.SupportedTemplateNames.Add("Stretch");
            IStringArray l1tProductNames = new StrArrayClass();
            l1tProductNames.Add("L1T");
            ((IURIProductNameFilter)l1tFilter).ProductNames = l1tProductNames;

            IItemURIFilter l1rFilter = new URIProductNameFilterClass();
            l1rFilter.Name = "L1R";
            l1rFilter.SupportsOrthorectification = true;
            l1rFilter.SupportedTemplateNames = new StrArrayClass();
            l1rFilter.SupportedTemplateNames.Add("Raw");
            l1rFilter.SupportedTemplateNames.Add("Stretch");
            IStringArray l1rProductNames = new StrArrayClass();
            l1rProductNames.Add("L1R");
            ((IURIProductNameFilter)l1rFilter).ProductNames = l1rProductNames;

            // Add them to the supported uri filters list
            ((IRasterTypeProperties)theRasterType).SupportedURIFilters.Add(allFilter);
            ((IRasterTypeProperties)theRasterType).SupportedURIFilters.Add(l1tFilter);
            ((IRasterTypeProperties)theRasterType).SupportedURIFilters.Add(l1rFilter);
            // Set 'All' as default
            theRasterType.URIFilter = allFilter;
            #endregion

            return theRasterType;
        }

        /// <summary>
        /// Name of the Raster Type Factory
        /// </summary>
        public string Name
        {
            get { return "Custom Raster Type Factory"; }
        }

        /// <summary>
        /// Names of the Raster Types supported by the factory.
        /// </summary>
        public IStringArray RasterTypeNames
        {
            get { return myRasterTypeNames; }
        }
        #endregion

        #region COM Registration Function(s)
        [ComRegisterFunction()]
        static void Reg(string regKey)
        {
            ESRI.ArcGIS.ADF.CATIDs.RasterTypeFactory.Register(regKey);
        }

        [ComUnregisterFunction()]
        static void Unreg(string regKey)
        {
            ESRI.ArcGIS.ADF.CATIDs.RasterTypeFactory.Unregister(regKey);
        }
        #endregion
    }

    [Guid("316725CB-35F2-4159-BEBB-A1445ECE9CF1")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("SampleRasterType.CustomRasterType")]
    [ComVisible(true)]
    public class DMCIIRasterBuilder : IRasterBuilder, IRasterBuilderInit, IPersistVariant,
        IRasterBuilder2, IRasterBuilderInit2
    {
        #region Private Members
        // The Mosaic Dataset currently using the Raster Type.
        IMosaicDataset myMosaicDataset;
        // The default spatial reference to apply to added data (if no spatial reference exists).
        ISpatialReference myDefaultSpatialReference;

        // The Raster Type Operation object (usually a Raster Type object).
        IRasterTypeOperation myRasterTypeOperation;
        // The Raster Type Properties.
        IPropertySet myRasterTypeProperties;

        // Array to fill with ItemURI's.
        IItemURIArray myURIArray;

        // GeoTransform helper object.
        IGeoTransformationHelper myGeoTransformationHelper;
        // Flags to specify whether the Raster Type can merge items and 
        bool myCanMergeItems;
        // if it has merged item.
        bool myMergeItems;

        // Mapping from field names to names or properties in the item propertyset.
        IPropertySet myAuxiliaryFieldAlias;
        // Fields to add to the Mosaic Dataset when items are added through this Raster Type.
        IFields myAuxiliaryFields;

        ITrackCancel myTrackCancel;
        UID myUID; // UID for the Custom Builder.

        // The current dimap file being processed.
        string myCurrentDimFile;
        #endregion

        public DMCIIRasterBuilder()
        {
            myMosaicDataset = null;
            myDefaultSpatialReference = null;
            myRasterTypeOperation = null;
            myRasterTypeProperties = null;
            myTrackCancel = null;

            myURIArray = null;

            myGeoTransformationHelper = null;
            myCanMergeItems = false;
            myMergeItems = false;

            myAuxiliaryFieldAlias = null;
            myAuxiliaryFields = null;

            myUID = new UIDClass();
            myUID.Value = "{316725CB-35F2-4159-BEBB-A1445ECE9CF1}";
        }

        #region IRasterBuilder Members

        /// <summary>
        /// This defines a mapping from field names in the attribute table of the mosaic to
        /// properties in the property set associated with the dataset, incase a user wants 
        /// specify fields which are different from the property in the dataset.
        /// e.g. The field CloudCover may map to a property called C_C in the dataset built 
        /// by the builder.
        /// </summary>
        public IPropertySet AuxiliaryFieldAlias
        {
            get
            {
                return myAuxiliaryFieldAlias;
            }
            set
            {
                myAuxiliaryFieldAlias = value;
            }
        }

        /// <summary>
        /// Specify fields if necessary to be added to the Mosaic Dataset when 
        /// items are added throug hthis Raster Type.
        /// </summary>
        public IFields AuxiliaryFields
        {
            get
            {
                if (myAuxiliaryFields == null)
                {
                    myAuxiliaryFields = new FieldsClass();
                    AddFields(myAuxiliaryFields);
                }
                return myAuxiliaryFields;
            }
            set
            {
                myAuxiliaryFields = value;
            }
        }

        /// <summary>
        /// Get a crawler recommended by the Raster Type based on the data srouce properties provided.
        /// </summary>
        /// <param name="pDataSourceProperties">Data source properties.</param>
        /// <returns>Data source crawler recommended by the raster type.</returns>
        public IDataSourceCrawler GetRecommendedCrawler(IPropertySet pDataSourceProperties)
        {
            try
            {
                // This is usually a file crawler because it can crawl directories as well, unless
                // special types of data needs to be crawled.
                IDataSourceCrawler myCrawler = new FileCrawlerClass();
                ((IFileCrawler)myCrawler).Path = Convert.ToString(pDataSourceProperties.GetProperty("Source"));
                ((IFileCrawler)myCrawler).Recurse = Convert.ToBoolean(pDataSourceProperties.GetProperty("Recurse"));
                myCrawler.Filter = Convert.ToString(pDataSourceProperties.GetProperty("Filter"));
                if (myCrawler.Filter == null || myCrawler.Filter == "")
                    myCrawler.Filter = "*.dim";
                return myCrawler;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Prepare the Raster Type for generating item Unique Resource Identifier (URI)
        /// </summary>
        /// <param name="pCrawler">Crawler to use to generate the item URI's</param>
        public void BeginConstruction(IDataSourceCrawler pCrawler)
        {
            myURIArray = new ItemURIArrayClass();
        }

        /// <summary>
        /// Construct a Unique Resource Identifier (URI)
        /// for each crawler item
        /// </summary>
        /// <param name="crawlerItem">Crawled Item from which the URI is generated</param>
        public void ConstructURIs(object crawlerItem)
        {
            myCurrentDimFile = (string)crawlerItem;
        }

        /// <summary>
        /// Finish construction of the URI's
        /// </summary>
        /// <returns>Array containing finised URI's</returns>
        public IItemURIArray EndConstruction()
        {
            return myURIArray;
        }

        /// <summary>
        /// Generate the next URI.
        /// </summary>
        /// <returns>The URI generated.</returns>
        public IItemURI GetNextURI()
        {
            IItemURI newURI = null;
            try
            {
                // Check to see if the item cralwed is a .dim file.
                if (myCurrentDimFile != "" && myCurrentDimFile != null && myCurrentDimFile.EndsWith(".dim"))
                {
                    // Create a new Dimap Parser obect and item uri.
                    DiMapParser myDimParser = new DiMapParser(myCurrentDimFile);
                    newURI = new ItemURIClass();
                    // Set the display name, Group, Product Name, Tag and Key.
                    newURI.DisplayName = System.IO.Path.GetFileName(myCurrentDimFile);
                    newURI.Group = System.IO.Path.GetFileNameWithoutExtension(myCurrentDimFile);
                    newURI.Key = myCurrentDimFile;
                    newURI.ProductName = myDimParser.ProductType;
                    newURI.Tag = "MS";
                    // Set the timestamp of the dimfile as source time stamp. This helps 
                    // with synchronization later.
                    IRasterTypeEnvironment myEnv = new RasterTypeEnvironmentClass();
                    DateTime dimTS = myEnv.GetTimeStamp(myCurrentDimFile);
                    newURI.SourceTimeStamp = dimTS;

                    myDimParser = null;
                    myCurrentDimFile = "";
                    myURIArray.Add(newURI);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return newURI;
        }

        /// <summary>
        /// Build the Builder Item which includes the function raster dataset and its footprint 
        /// given the ItemURI.
        /// </summary>
        /// <param name="pItemURI">ItemURi to use to build the Builder Item.</param>
        /// <returns>The builder item.</returns>
        public IBuilderItem Build(IItemURI pItemURI)
        {
            try
            {
                // Create a new parser object and builder item.
                DiMapParser myDimParser = new DiMapParser(pItemURI.Key);
                IBuilderItem currItem = new BuilderItemClass();

                // Set Category and URI
                currItem.Category = esriRasterCatalogItemCategory.esriRasterCatalogItemCategoryPrimary;
                currItem.URI = pItemURI;

                // Set FunctionRasterDataset
                IFunctionRasterDataset inputFrd = GetFRD(myDimParser, pItemURI);
                currItem.Dataset = inputFrd;
                // Set band information for the function dataset including names, wavelengths and stats if available.
                SetBandProperties((IDataset)inputFrd, myDimParser);

                // Set Footprint
                IGeoDataset geoDset = (IGeoDataset)inputFrd;
                // Set it to the current raster extent first. If the raster has no 
                // spatial reference, the extents will be in pixel space.
                currItem.Footprint = (IGeometry)geoDset.Extent;
                // The get the footprint from the dim file is it exists.
                currItem.Footprint = GetFootprint(myDimParser);

                // Set Properties. These properties are used to fill the Auxiliary Fields 
                // defined earlier and also key properties if the names are correct.
                IPropertySet propSet = currItem.Dataset.Properties;
                if (null == propSet)
                    propSet = new PropertySetClass();
                double sunAzimuth = Convert.ToDouble(myDimParser.SunAzimuth);
                double sunElevation = Convert.ToDouble(myDimParser.SunElevation);
                double sensorAzimuth = Convert.ToDouble(myDimParser.SensorAzimuth);
                double sensorElevation = 180 - Convert.ToDouble(myDimParser.IncidenceAngle);
                string acqDate = myDimParser.AcquisitionDate;
                string acqTime = myDimParser.AcquisitionTime;
                // Create a time object from the provided date and time.
                ITime acqDateTimeObj = new TimeClass();
                acqDateTimeObj.SetFromTimeString(esriTimeStringFormat.esriTSFYearThruSubSecondWithDash,
                    acqDate + " " + acqTime);
                // and obtain a DateTime object to set as value of the property. This ensures the 
                // field displays the value correctly.
                DateTime acqDateTimeFieldVal = acqDateTimeObj.QueryOleTime();

                propSet.SetProperty("AcquisitionDate", acqDateTimeFieldVal);
                propSet.SetProperty("SensorName", myDimParser.MetadataProfile);
                propSet.SetProperty("SunAzimuth", sunAzimuth);
                propSet.SetProperty("SunElevation", sunElevation);
                propSet.SetProperty("SatAzimuth", sensorAzimuth);
                propSet.SetProperty("SatElevation", sensorElevation);
                currItem.Dataset.Properties = propSet;

                return currItem;
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        /// <summary>
        /// Flag to specify whether the Raster Builder can build items in place.
        /// </summary>
        public bool CanBuildInPlace
        {
            get { return false; }
        }

        /// <summary>
        /// Check if the item provided is "stale" or not valid
        /// </summary>
        /// <param name="pItemURI">URI for the item to be checked</param>
        /// <returns>Flag to specify whether the item is stale or not.</returns>
        public bool IsStale(IItemURI pItemURI)
        {
            try
            {
                IRasterTypeEnvironment myEnv = new RasterTypeEnvironmentClass();
                DateTime currDimTS = myEnv.GetTimeStamp(pItemURI.Key);
                return pItemURI.SourceTimeStamp != currDimTS;
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Properties associated with the Raster Type
        /// </summary>
        public IPropertySet Properties
        {
            get
            {
                if (myRasterTypeProperties == null)
                    myRasterTypeProperties = new PropertySetClass();
                return myRasterTypeProperties;
            }
            set
            {
                myRasterTypeProperties = value;
            }
        }

        /// <summary>
        /// Sets band properties on a given dataset including stats, band names and wavelengths.
        /// </summary>
        /// <param name="dataset">The dataset to set properties on.</param>
        /// <param name="dimParser">Dimap parser to read properties from.</param>
        private void SetBandProperties(IDataset dataset, DiMapParser dimParser)
        {
            try
            {
                // Set band band props.
                IRasterKeyProperties rasterKeyProps = (IRasterKeyProperties)dataset;
                IRasterBandCollection rasterBandColl = (IRasterBandCollection)dataset;
                int imageNumBands = ((IFunctionRasterDataset)dataset).RasterInfo.BandCount;
                int dinNumBands = dimParser.NumBands;
                int[] bandIndexes = new int[dinNumBands];
                IStringArray bandNames = new StrArrayClass();
                for (int i = 0; i < dinNumBands; ++i)
                {
                    // Get band index for the first band.
                    bandIndexes[i] = Convert.ToInt16(dimParser.GetBandIndex(i));
                    // Validate band index.
                    if (bandIndexes[i] > 0 && bandIndexes[i] <= imageNumBands)
                    {
                        // Get Band Name for the index.
                        bandNames.Add(dimParser.GetBandDesc(bandIndexes[i]));
                        // Get Band stats for the index.
                        IRasterStatistics bandStats = new RasterStatisticsClass();
                        bandStats.Minimum = Convert.ToDouble(dimParser.GetBandStatMin(bandIndexes[i]));
                        bandStats.Maximum = Convert.ToDouble(dimParser.GetBandStatMax(bandIndexes[i]));
                        bandStats.Mean = Convert.ToDouble(dimParser.GetBandStatMean(bandIndexes[i]));
                        bandStats.StandardDeviation = Convert.ToDouble(dimParser.GetBandStatStdDev(bandIndexes[i]));
                        // Set stats on the dataset.
                        ((IRasterBandEdit2)rasterBandColl.Item(bandIndexes[i] - 1)).AlterStatistics(bandStats);
                        // Set Band Name and wavelengths according to the name.
                        rasterKeyProps.SetBandProperty("BandName", (bandIndexes[i] - 1), bandNames.get_Element(i));
                        SetBandWavelengths(dataset, (bandIndexes[i] - 1));
                        // Refresh dataset so changes are saved.
                        ((IRasterDataset3)dataset).Refresh();
                    }
                }
            }
            catch (Exception exc)
            {
                string error = exc.Message;
            }
        }

        /// <summary>
        /// Set the wavelengths corresponding to the band name.
        /// </summary>
        private void SetBandWavelengths(IDataset dataset, int bandIndex)
        {
            IRasterKeyProperties rasterKeyProps = (IRasterKeyProperties)dataset;
            IRasterBandCollection rasterBandColl = (IRasterBandCollection)dataset;
            string bandName = (string)rasterKeyProps.GetBandProperty("BandName", bandIndex);
            // Set wavelengths for the bands
            switch (bandName.ToLower())
            {
                case "red":
                    {
                        rasterKeyProps.SetBandProperty("WavelengthMin", bandIndex, 630);
                        rasterKeyProps.SetBandProperty("WavelengthMax", bandIndex, 690);
                    }
                    break;

                case "green":
                    {
                        rasterKeyProps.SetBandProperty("WavelengthMin", bandIndex, 520);
                        rasterKeyProps.SetBandProperty("WavelengthMax", bandIndex, 600);
                    }
                    break;

                case "nir":
                case "nearinfrared":
                    {
                        rasterKeyProps.SetBandProperty("WavelengthMin", bandIndex, 770);
                        rasterKeyProps.SetBandProperty("WavelengthMax", bandIndex, 900);
                    }
                    break;
            }
        }

        //private IGeometry GetFootprint(DiMapParser dimParser)
        //{
        //    IGeometry currFootprint = null;
        //    dimParser.ResetVertexCount();
        //    string xs = "";
        //    string ys = "";
        //    string rows = "";
        //    string cols = "";
        //    double minX = 10000000000.0;
        //    double maxX = -1000000000.00;
        //    double minY = 1000000000.00;
        //    double maxY = -1000000000.00;
        //    double minRow = 1000000000.00;
        //    double maxRow = -1000000000.0;
        //    double minCol = 1000000000.00;
        //    double maxCol = -1000000000.0;
        //    double x = 0.0;
        //    double y = 0.0;
        //    double row = 0.0;
        //    double col = 0.0;

        //    while (dimParser.GetNextVertex(out xs, out ys, out rows, out cols))
        //    {
        //        x = Convert.ToDouble(xs);
        //        y = Convert.ToDouble(ys);
        //        row = Convert.ToDouble(rows);
        //        col = Convert.ToDouble(cols);

        //        if (x < minX)
        //            minX = x;
        //        if (x > maxX)
        //            maxX = x;

        //        if (y < minY)
        //            minY = y;
        //        if (y > maxY)
        //            maxY = y;

        //        if (row < minRow)
        //            minRow = row;
        //        if (row > maxRow)
        //            maxRow = row;

        //        if (col < minCol)
        //            minCol = col;
        //        if (col > maxCol)
        //            maxCol = col;

        //        x = 0.0;
        //        y = 0.0;
        //        row = 0.0;
        //        col = 0.0;
        //        xs = "";
        //        ys = "";
        //        rows = "";
        //        cols = "";
        //    }
        //    x = Convert.ToDouble(xs);
        //    y = Convert.ToDouble(ys);
        //    row = Convert.ToDouble(rows);
        //    col = Convert.ToDouble(cols);

        //    if (x < minX)
        //        minX = x;
        //    if (x > maxX)
        //        maxX = x;

        //    if (y < minY)
        //        minY = y;
        //    if (y > maxY)
        //        maxY = y;

        //    if (row < minRow)
        //        minRow = row;
        //    if (row > maxRow)
        //        maxRow = row;

        //    if (col < minCol)
        //        minCol = col;
        //    if (col > maxCol)
        //        maxCol = col;

        //    currFootprint = new PolygonClass();
        //    IPointCollection currPointColl = (IPointCollection)currFootprint;
        //    IEnvelope rectEnvelope = new EnvelopeClass();
        //    rectEnvelope.PutCoords(minX, minY, maxX, maxY);
        //    ISegmentCollection segmentCollection = (ISegmentCollection)currFootprint;
        //    segmentCollection.SetRectangle(rectEnvelope);

        //    // Get Srs
        //    int epsgcode = Convert.ToInt32((dimParser.SrsCode.Split(':'))[1]);
        //    ISpatialReferenceFactory3 srsfactory = new SpatialReferenceEnvironmentClass();
        //    ISpatialReference dimSrs = srsfactory.CreateSpatialReference(epsgcode);
        //    ISpatialReferenceResolution srsRes = (ISpatialReferenceResolution)dimSrs;
        //    srsRes.ConstructFromHorizon();
        //    srsRes.SetDefaultXYResolution();
        //    ((ISpatialReferenceTolerance)dimSrs).SetDefaultXYTolerance();
        //    currFootprint.SpatialReference = dimSrs;

        //    #region Commented
        //    //IEnvelope extent = new EnvelopeClass();
        //    //extent.XMin = geoDset.Extent.XMin;
        //    //extent.XMax = geoDset.Extent.XMax;
        //    //extent.YMin = geoDset.Extent.YMin;
        //    //extent.YMax = geoDset.Extent.YMax;
        //    //extent.SpatialReference = geoDset.SpatialReference;
        //    //extent.Width = inputFrd.RasterInfo.Extent.Width;
        //    //extent.Height = inputFrd.RasterInfo.Extent.Height;
        //    //currItem.Footprint = (IGeometry)extent;

        //    //myDimParser.ResetVertexCount();
        //    //string x = "";
        //    //string y = "";
        //    //string row = "";
        //    //string col = "";
        //    //IGeometry currFootprint = new PolygonClass();
        //    //IPointCollection currPointColl = (IPointCollection)currFootprint;

        //    // Creating a polygon!!!

        //    ////Build a polygon from a sequence of points. 
        //    ////Add arrays of points to a geometry using the IGeometryBridge2 interface on the 
        //    ////GeometryEnvironment singleton object.
        //    //IGeometryBridge2 geometryBridge2 = new GeometryEnvironmentClass();
        //    //IPointCollection4 pointCollection4 = new PolygonClass();

        //    ////TODO:
        //    ////pointCollection4.SpatialReference = 'Define the spatial reference of the new polygon.

        //    //WKSPoint[] aWKSPointBuffer = null;
        //    //long cPoints = 4; //The number of points in the first part.
        //    //aWKSPointBuffer = new WKSPoint[System.Convert.ToInt32(cPoints - 1) + 1];

        //    ////TODO:
        //    ////aWKSPointBuffer = 'Read cPoints into the point buffer.

        //    //geometryBridge2.SetWKSPoints(pointCollection4, ref aWKSPointBuffer);

        //    //myDimParser.GetNextVertex(out x, out y, out col, out row);
        //    //IPoint currPoint1 = new PointClass();
        //    //currPoint1.X = Convert.ToDouble(x);
        //    //currPoint1.Y = Convert.ToDouble(y);
        //    //myDimParser.GetNextVertex(out x, out y, out col, out row);
        //    //IPoint currPoint2 = new PointClass();
        //    //currPoint1.X = Convert.ToDouble(x);
        //    //currPoint1.Y = Convert.ToDouble(y);
        //    //myDimParser.GetNextVertex(out x, out y, out col, out row);
        //    //IPoint currPoint3 = new PointClass();
        //    //currPoint1.X = Convert.ToDouble(x);
        //    //currPoint1.Y = Convert.ToDouble(y);
        //    //myDimParser.GetNextVertex(out x, out y, out col, out row);
        //    //IPoint currPoint4 = new PointClass();
        //    //currPoint1.X = Convert.ToDouble(x);
        //    //currPoint1.Y = Convert.ToDouble(y);
        //    //object refPoint1 = (object)currPoint1;
        //    //object refPoint2 = (object)currPoint2;
        //    //object refPoint3 = (object)currPoint3;
        //    //object refPoint4 = (object)currPoint4;
        //    //currPointColl.AddPoint(currPoint1, ref refPoint4, ref refPoint2);
        //    //currPointColl.AddPoint(currPoint2, ref refPoint1, ref refPoint3);
        //    //currPointColl.AddPoint(currPoint3, ref refPoint2, ref refPoint4);
        //    //currPointColl.AddPoint(currPoint4, ref refPoint3, ref refPoint1);
        //    //((IPolygon)currFootprint).Close();
        //    //currFootprint.SpatialReference = dimSrs;
        //    #endregion
        //    return currFootprint;
        //}

        /// <summary>
        /// Get the footprint from the dimap file if it exists.
        /// </summary>
        /// <param name="dimParser">Dimap file parser.</param>
        /// <returns>Footprint geomtry.</returns>
        private IGeometry GetFootprint(DiMapParser dimParser)
        {
            IGeometry currFootprint = null;
            dimParser.ResetVertexCount();
            string xs = "";
            string ys = "";
            double minX = 10000000000.0;
            double maxX = -1000000000.00;
            double minY = 1000000000.00;
            double maxY = -1000000000.00;
            double x = 0.0;
            double y = 0.0;
            string units = dimParser.ProductType;
            if (units.ToLower() == "L1T".ToLower())
                units = "M";
            else if (units.ToLower() == "L1R".ToLower())
                units = "Deg";
            // Get vertices from the dimap file and figure out the min,max.
            while (dimParser.GetNextVertex2(out xs, out ys, units))
            {
                x = Convert.ToDouble(xs);
                y = Convert.ToDouble(ys);

                if (x < minX)
                    minX = x;
                if (x > maxX)
                    maxX = x;

                if (y < minY)
                    minY = y;
                if (y > maxY)
                    maxY = y;

                x = 0.0;
                y = 0.0;
                xs = "";
                ys = "";
            }
            x = Convert.ToDouble(xs);
            y = Convert.ToDouble(ys);

            if (x < minX)
                minX = x;
            if (x > maxX)
                maxX = x;

            if (y < minY)
                minY = y;
            if (y > maxY)
                maxY = y;

            // create a new polygon and fill it using the vertices calculated.
            currFootprint = new PolygonClass();
            IPointCollection currPointColl = (IPointCollection)currFootprint;
            IEnvelope rectEnvelope = new EnvelopeClass();
            rectEnvelope.PutCoords(minX, minY, maxX, maxY);
            ISegmentCollection segmentCollection = (ISegmentCollection)currFootprint;
            segmentCollection.SetRectangle(rectEnvelope);

            // Get Srs from the dim file and set it on the footprint.
            int epsgcode = Convert.ToInt32((dimParser.SrsCode.Split(':'))[1]);
            ISpatialReferenceFactory3 srsfactory = new SpatialReferenceEnvironmentClass();
            ISpatialReference dimSrs = srsfactory.CreateSpatialReference(epsgcode);
            ISpatialReferenceResolution srsRes = (ISpatialReferenceResolution)dimSrs;
            srsRes.ConstructFromHorizon();
            srsRes.SetDefaultXYResolution();
            ((ISpatialReferenceTolerance)dimSrs).SetDefaultXYTolerance();
            currFootprint.SpatialReference = dimSrs;
            return currFootprint;
        }

        /// <summary>
        /// Create the function raster dataset from the source images.
        /// </summary>
        /// <param name="dimPar">Parser for the dimap file.</param>
        /// <param name="pItemURI">ItemURi to use.</param>
        /// <returns>Function raster dataset created.</returns>
        private IFunctionRasterDataset GetFRD(DiMapParser dimPar, IItemURI pItemURI)
        {
            IFunctionRasterDataset opFrd = null;
            try
            {
                Type factoryType = Type.GetTypeFromProgID("esriDataSourcesRaster.RasterWorkspaceFactory");
                IWorkspaceFactory workspaceFactory = (IWorkspaceFactory)Activator.CreateInstance(factoryType);
                IWorkspace workspace = workspaceFactory.OpenFromFile(System.IO.Path.GetDirectoryName(pItemURI.Key), 0);
                IRasterWorkspace rasterWorkspace = (IRasterWorkspace)workspace;
                // Open the tif file associated with the .dim file as a raster dataset.
                string imagePath = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(pItemURI.Key),
                    pItemURI.Group + ".tif");
                string rpcPath = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(pItemURI.Key),
                    pItemURI.Group + ".rpc");
                IRasterDataset inputRasterDataset = null;
                if (File.Exists(imagePath))
                    inputRasterDataset = rasterWorkspace.OpenRasterDataset(pItemURI.Group + ".tif");
                else
                    return null;

                IFunctionRasterDataset intermedFrd = null;
                // If the file opes successfully, add a RasterInfo function on top.
                if (inputRasterDataset != null)
                {
                    // Create an Identity function dataset to get the raster info.
                    IRasterFunction identityFunction = new IdentityFunctionClass();
                    IFunctionRasterDataset idFrd = new FunctionRasterDatasetClass();
                    idFrd.Init(identityFunction, inputRasterDataset);
                    // Create a raster info function dataset.
                    IRasterFunction rasterInfoFunction = new RasterInfoFunctionClass();
                    IRasterInfoFunctionArguments rasterInfoFuncArgs = new RasterInfoFunctionArgumentsClass();
                    rasterInfoFuncArgs.Raster = inputRasterDataset;
                    rasterInfoFuncArgs.RasterInfo = idFrd.RasterInfo;
                    intermedFrd = new FunctionRasterDatasetClass();
                    intermedFrd.Init(rasterInfoFunction, rasterInfoFuncArgs);
                }
                else
                    return null;
                // Check if there is an RPC file associated with the image. If so
                // then add a geometric function to apply the rpc xform.
                if (File.Exists(rpcPath))
                    opFrd = ApplyRPC(rpcPath, (IRasterDataset)intermedFrd);

                // If no rpc pars exist or applying rpc fails, use the intermediate 
                // function raster dataset created.
                if (opFrd == null)
                    opFrd = intermedFrd;
                
                //IRasterFunction ebFunction = new ExtractBandFunctionClass();
                //IRasterFunctionArguments ebFuncArgs = new ExtractBandFunctionArgumentsClass();
                //ILongArray bandIDs = new LongArrayClass();
                //bandIDs.Add(2);
                //bandIDs.Add(1);
                //bandIDs.Add(0);
                ////bandIDs.Add(4);
                //((IExtractBandFunctionArguments)ebFuncArgs).BandIDs = bandIDs;
                //((IExtractBandFunctionArguments)ebFuncArgs).Raster = inputRasterDataset;
                //opFrd = new FunctionRasterDatasetClass();
                //opFrd.Init(ebFunction, ebFuncArgs);

                //if (opFrd == null)
                //{
                //    IRasterFunction identityFunction = new IdentityFunctionClass();
                //    opFrd = new FunctionRasterDatasetClass();
                //    opFrd.Init(identityFunction, inputRasterDataset);
                //}
            }
            catch (Exception exc)
            {
                string error = exc.Message;
            }
            return opFrd;
        }

        /// <summary>
        /// Parse the RPC parameters file associated with the image and create an RPCXform
        /// to bea applied to the inputDataset as a geomtric function.
        /// </summary>
        /// <param name="rpcPath">Path to the rpc parameters file.</param>
        /// <param name="inputDataset">Input dataset to apply the xform on.</param>
        /// <returns>Function raster dataset created.</returns>
        private IFunctionRasterDataset ApplyRPC(string rpcPath, IRasterDataset inputDataset)
        {
            IFunctionRasterDataset opFrd = null;
            try
            {
                // Open the RPC file and create Geometric transform
                IGeodataXform finalXForm = null;
                IRPCXform rpcXForm = GetRPCXForm(rpcPath);

                IFunctionRasterDataset idFrd = null;
                if (!(inputDataset is IFunctionRasterDataset))
                {
                    IRasterFunction identityFunction = new IdentityFunctionClass();
                    idFrd = new FunctionRasterDatasetClass();
                    idFrd.Init(identityFunction, inputDataset);
                }
                else
                    idFrd = (IFunctionRasterDataset)inputDataset;

                IRasterInfo datasetRasterInfo = idFrd.RasterInfo;
                IEnvelope datasetExtent = datasetRasterInfo.Extent;
                ISpatialReference datasetSrs = ((IGeoDataset)idFrd).SpatialReference;

                long dRows = datasetRasterInfo.Height;
                long dCols = datasetRasterInfo.Width;
                IEnvelope pixelExtent = new EnvelopeClass();
                pixelExtent.PutCoords(-0.5, 0.5 - dRows, -0.5 + dCols, 0.5);

                bool noAffineNeeded = ((IClone)pixelExtent).IsEqual((IClone)datasetExtent);
                if (!noAffineNeeded)
                {
                    // Tranform ground space to pixel space.
                    IAffineTransformation2D affineXform = new AffineTransformation2DClass();
                    affineXform.DefineFromEnvelopes(datasetExtent, pixelExtent);
                    IGeometricXform geoXform = new GeometricXformClass();
                    geoXform.Transformation = affineXform;
                    finalXForm = geoXform;
                }

                // Transform from pixel space back to ground space to set as the forward transform.
                IEnvelope groundExtent = ((IGeoDataset)idFrd).Extent;
                groundExtent.Project(datasetSrs);
                IAffineTransformation2D affineXform2 = new AffineTransformation2DClass();
                affineXform2.DefineFromEnvelopes(pixelExtent, groundExtent);
                IGeometricXform forwardXForm = new GeometricXformClass();
                forwardXForm.Transformation = affineXform2;
                rpcXForm.ForwardXform = forwardXForm;

                // Create the composite transform that changes ground values to pixel space
                // then applies the rpc transform which will transform them back to ground space.
                ICompositeXform compositeXForm = new CompositeXformClass();
                compositeXForm.Add(finalXForm);
                compositeXForm.Add(rpcXForm);
                finalXForm = (IGeodataXform)compositeXForm;

                // Then apply the transform on the raster using the geometric function.
                if (finalXForm != null)
                {
                    IRasterFunction geometricFunction = new GeometricFunctionClass();
                    IGeometricFunctionArguments geometricFunctionArgs = null;
                    // Get the geomtric function arguments if supplied by the user (in the UI).
                    if (myRasterTypeOperation != null &&
                        ((IRasterTypeProperties)myRasterTypeOperation).OrthorectificationParameters != null)
                        geometricFunctionArgs =
                        ((IRasterTypeProperties)myRasterTypeOperation).OrthorectificationParameters;
                    else
                        geometricFunctionArgs = new GeometricFunctionArgumentsClass();
                    // Append the xform to the existing ones from the image.
                    geometricFunctionArgs.AppendGeodataXform = true;
                    geometricFunctionArgs.GeodataXform = finalXForm;
                    geometricFunctionArgs.Raster = inputDataset;
                    opFrd = new FunctionRasterDatasetClass();
                    opFrd.Init(geometricFunction, geometricFunctionArgs);
                }
                return opFrd;
            }
            catch (Exception exc)
            {
                string error = exc.Message;
                return opFrd;
            }
        }

        /// <summary>
        /// Create an RPCXForm from a text file containing parameters.
        /// </summary>
        /// <param name="rpcFilePath">Text file containing the parameters.</param>
        /// <returns>The RPCXForm generated.</returns>
        private IRPCXform GetRPCXForm(string rpcFilePath)
        {
            try
            {
                // Array for parameters.
                double[] RPC = new double[90];
                // Propertyset to store properties as backup.
                //IPropertySet coefficients = new PropertySetClass();

                #region Parse RPC text file
                // Use the stream reader to open the file and read lines from it.
                using (StreamReader sr = new StreamReader(rpcFilePath))
                {
                    string line;
                    int lineNumber = 0;

                    while ((line = sr.ReadLine()) != null)
                    {
                        ++lineNumber;
                        try
                        {
                            // Split the line into tokens based on delimiters
                            char[] delimiters = { ':', ' ' };
                            string[] tokens = line.Split(delimiters, System.StringSplitOptions.RemoveEmptyEntries);
                            int numTokens = tokens.GetLength(0);
                            if (numTokens > 1)
                            {
                                string currPar = tokens[0];
                                double currValue = Convert.ToDouble(tokens[1]);
                                // Convert the Value to a double and store in the array.
                                RPC[(lineNumber - 1)] = currValue;
                                // Store the property and the value in the propertyset to lookup later if needed.
                                //coefficients.SetProperty(currPar, currValue);
                                // Read units for conversion if needed
                                //string currUnit = tokens[2];
                            }
                            else
                                Console.WriteLine("Could not parse line " + lineNumber.ToString());
                        }
                        catch (Exception ex)
                        {
                            Console.Write(ex.ToString());
                        }
                    }
                    sr.Close();
                }
                #endregion

                // Create the new RPCXForm from the parameter array.
                IRPCXform myRPCXform = (IRPCXform)new RPCXform();
                object rpcCoeffs = ((object)RPC);
                myRPCXform.DefineFromCoefficients(ref rpcCoeffs);

                return myRPCXform;
            }
            catch (Exception exc)
            {
                string error = exc.Message;
                throw exc;
            }
        }

        /// <summary>
        /// Create new fields to add to the mosaic dataset attribute table.
        /// </summary>
        /// <param name="myFields">Fields to be added.</param>
        private void AddFields(IFields myFields)
        {
            // Create a new field object
            IField pField = new FieldClass();
            // Set the field editor for this field
            IFieldEdit objectIDFieldEditor = (IFieldEdit)pField;
            // Set the name and alias of the field
            objectIDFieldEditor.Name_2 = "SensorName"; 
            objectIDFieldEditor.AliasName_2 = "Sensor Name";
            // Set the type of the field
            objectIDFieldEditor.Type_2 = esriFieldType.esriFieldTypeString;
            // Add the newly created field to list of existing fields
            IFieldsEdit fieldsEditor = (IFieldsEdit)myFields;
            fieldsEditor.AddField(pField);

            // Create a new field object
            pField = new FieldClass();
            // Set the field editor for this field
            objectIDFieldEditor = (IFieldEdit)pField;
            // Set the name and alias of the field
            objectIDFieldEditor.Name_2 = "AcquisitionDate"; 
            objectIDFieldEditor.AliasName_2 = "Acquisition Date";
            // Set the type of the field
            objectIDFieldEditor.Type_2 = esriFieldType.esriFieldTypeDate;
            fieldsEditor.AddField(pField);

            // Create a new field object
            pField = new FieldClass();
            // Set the field editor for this field
            objectIDFieldEditor = (IFieldEdit)pField;
            // Set the name and alias of the field
            objectIDFieldEditor.Name_2 = "SunAzimuth";
            objectIDFieldEditor.AliasName_2 = "Sun Azimuth";
            // Set the type of the field
            objectIDFieldEditor.Type_2 = esriFieldType.esriFieldTypeDouble;
            fieldsEditor.AddField(pField);

            // Create a new field object
            pField = new FieldClass();
            // Set the field editor for this field
            objectIDFieldEditor = (IFieldEdit)pField;
            // Set the name and alias of the field
            objectIDFieldEditor.Name_2 = "SunElevation";
            objectIDFieldEditor.AliasName_2 = "Sun Elevation";
            // Set the type of the field
            objectIDFieldEditor.Type_2 = esriFieldType.esriFieldTypeDouble;
            fieldsEditor.AddField(pField);

            // Create a new field object
            pField = new FieldClass();
            // Set the field editor for this field
            objectIDFieldEditor = (IFieldEdit)pField;
            // Set the name and alias of the field
            objectIDFieldEditor.Name_2 = "SatAzimuth";
            objectIDFieldEditor.AliasName_2 = "Satellite Azimuth";
            // Set the type of the field as Blob
            objectIDFieldEditor.Type_2 = esriFieldType.esriFieldTypeDouble;
            fieldsEditor.AddField(pField);

            // Create a new field object
            pField = new FieldClass();
            // Set the field editor for this field
            objectIDFieldEditor = (IFieldEdit)pField;
            // Set the name and alias of the field
            objectIDFieldEditor.Name_2 = "SatElevation";
            objectIDFieldEditor.AliasName_2 = "Satellite Elevation";
            // Set the type of the field as Blob
            objectIDFieldEditor.Type_2 = esriFieldType.esriFieldTypeDouble;
            fieldsEditor.AddField(pField);
        }
        #endregion

        #region IRasterBuilderInit Members

        public ISpatialReference DefaultSpatialReference
        {
            get
            {
                return myDefaultSpatialReference;
            }
            set
            {
                myDefaultSpatialReference = value;
            }
        }

        public IMosaicDataset MosaicDataset
        {
            get
            {
                return myMosaicDataset;
            }
            set
            {
                myMosaicDataset = value;
            }
        }

        public IRasterTypeOperation RasterTypeOperation
        {
            get
            {
                return myRasterTypeOperation;
            }
            set
            {
                myRasterTypeOperation = value;
            }
        }

        public ITrackCancel TrackCancel
        {
            get
            {
                return myTrackCancel;
            }
            set
            {
                myTrackCancel = value;
            }
        }

        #endregion

        #region IPersistVariant Members
        /// <summary>
        /// UID for the object implementing the Persist Variant
        /// </summary>
        public UID ID
        {
            get { return myUID; }
        }

        /// <summary>
        /// Load the object from the stream provided
        /// </summary>
        /// <param name="Stream">Stream that represents the serialized Raster Type</param>
        public void Load(IVariantStream Stream)
        {
            string name = (string)Stream.Read();
            //if (innerRasterBuilder is IPersistVariant)
            //    ((IPersistVariant)innerRasterBuilder).Load(Stream);
            //innerRasterBuilder = (IRasterBuilder)Stream.Read(); // Load the innerRasterBuilder from the stream.
        }

        /// <summary>
        /// Same the Raster Type to the stream provided
        /// </summary>
        /// <param name="Stream">Stream to serialize the Raster Type into</param>
        public void Save(IVariantStream Stream)
        {
            Stream.Write("CustomRasterType");
            //if (innerRasterBuilder is IPersistVariant)
            //    ((IPersistVariant)innerRasterBuilder).Save(Stream);
            //Stream.Write(innerRasterBuilder); // Save the innerRasterBuilder into the stream.
        }

        #endregion

        #region IRasterBuilder2 Members
        /// <summary>
        /// Check if the data source provided is a valid data source for the builder.
        /// </summary>
        /// <param name="vtDataSource">Data source (usually the path to a metadta file)</param>
        /// <returns>Flag to specify whether it is  valid source.</returns>
        public bool CanBuild(object vtDataSource)
        {
            if (!(vtDataSource is string))
                return false;
            string dimFilePath = (string)vtDataSource;
            if (!dimFilePath.ToLower().EndsWith(".dim"))
                return false;
            DiMapParser myDimParser = null;
            try
            {
                myDimParser = new DiMapParser(dimFilePath);
                if (myDimParser.MetadataProfile.ToLower() == "DMCII".ToLower())
                {
                    myDimParser = null;
                    return true;
                }
                else
                {
                    myDimParser = null;
                    return false;
                }
            }
            catch (Exception exc)
            {
                myDimParser = null;
                string error = exc.Message;
                return false;
            }
        }

        public bool CanMergeItems
        {
            get { return myCanMergeItems; }
        }

        public bool MergeItems
        {
            get
            {
                return myMergeItems;
            }
            set
            {
                myMergeItems = value;
            }
        }

        /// <summary>
        /// Check to see if the properties provided to the raster type/builder 
        /// are sufficient for it to work. Usually used for UI validation.
        /// </summary>
        public void Validate()
        {
            return;
        }
        #endregion

        #region IRasterBuilderInit2 Members

        /// <summary>
        /// Helper object to store geographic transformations set on the mosaic dataset.
        /// </summary>
        public IGeoTransformationHelper GeoTransformationHelper
        {
            get
            {
                return myGeoTransformationHelper;
            }
            set
            {
                myGeoTransformationHelper = null;
            }
        }

        #endregion
    }

    /// <summary>
    /// Class used to parse the dim file (xml format) and get relevant properties from it.
    /// </summary>
    public class DiMapParser
    {
        private string myXmlPath;
        private XmlDocument myXmlDoc;
        private XmlNodeList bandInfo;
        private XmlNodeList bandStats;
        private XmlNodeList footprintInfo;
        private int vertexCount;

        public DiMapParser()
        {
            myXmlPath = null;
            bandInfo = null;
            bandStats = null;
            footprintInfo = null;
            vertexCount = 0;
        }

        public DiMapParser(string xmlPath)
        {
            myXmlPath = xmlPath;
            bandInfo = null;
            bandStats = null;
            footprintInfo = null;
            vertexCount = 0;
            myXmlDoc = new XmlDocument();
            myXmlDoc.Load(myXmlPath);
        }

        /// <summary>
        /// Flag to specify whether the footprint exists in the xml file.
        /// </summary>
        public bool FootPrintExists
        {
            get
            {
                if (footprintInfo == null)
                    footprintInfo = myXmlDoc.SelectSingleNode("//Dataset_Frame").SelectNodes("Vertex");
                return footprintInfo != null;
            }
        }

        /// <summary>
        /// Reset the vertex count to get vertices of the footprint.
        /// </summary>
        public void ResetVertexCount()
        {
            vertexCount = 0;
        }

        public bool GetNextVertex(out string x, out string y, out string col, out string row)
        {
            if (footprintInfo == null)
                footprintInfo = myXmlDoc.SelectSingleNode("//Dataset_Frame").SelectNodes("Vertex");
            x = footprintInfo[vertexCount].SelectSingleNode("FRAME_LON").InnerText;
            y = footprintInfo[vertexCount].SelectSingleNode("FRAME_LAT").InnerText;
            col = footprintInfo[vertexCount].SelectSingleNode("FRAME_COL").InnerText;
            row = footprintInfo[vertexCount].SelectSingleNode("FRAME_ROW").InnerText;
            ++vertexCount;

            if (vertexCount >= footprintInfo.Count)
                return false;
            else
                return true;
        }

        /// <summary>
        /// Get next vertex from the footprint defined in the xml based on the vertex count and unit.
        /// </summary>
        /// <param name="x">The X value.</param>
        /// <param name="y">The Y value.</param>
        /// <param name="unit">Unit to check which parameter to get vertex from.</param>
        /// <returns>True if next vertex exists.</returns>
        public bool GetNextVertex2(out string x, out string y, string unit)
        {
            if (unit == "Deg")
            {
                if (footprintInfo == null)
                    footprintInfo = myXmlDoc.SelectSingleNode("//Dataset_Frame").SelectNodes("Vertex");
                x = footprintInfo[vertexCount].SelectSingleNode("FRAME_LON").InnerText;
                y = footprintInfo[vertexCount].SelectSingleNode("FRAME_LAT").InnerText;
                //col = footprintInfo[vertexCount].SelectSingleNode("FRAME_COL").InnerText;
                //row = footprintInfo[vertexCount].SelectSingleNode("FRAME_ROW").InnerText;
            }
            else
            {
                if (footprintInfo == null)
                    footprintInfo = myXmlDoc.SelectSingleNode("//Dataset_Frame").SelectNodes("Vertex");
                x = footprintInfo[vertexCount].SelectSingleNode("FRAME_X").InnerText;
                y = footprintInfo[vertexCount].SelectSingleNode("FRAME_Y").InnerText;
                //col = footprintInfo[vertexCount].SelectSingleNode("FRAME_COL").InnerText;
                //row = footprintInfo[vertexCount].SelectSingleNode("FRAME_ROW").InnerText;
            }
            ++vertexCount;

            if (vertexCount >= footprintInfo.Count)
                return false;
            else
                return true;
        }

        /// <summary>
        /// The number of bands defined in the xml.
        /// </summary>
        public int NumBands
        {
            get
            {
                if (bandInfo == null)
                    bandInfo = myXmlDoc.SelectNodes("//Spectral_Band_Info");
                if (bandStats == null)
                    bandStats = myXmlDoc.SelectNodes("//Band_Statistics");
                return bandInfo.Count;
            }
        }

        /// <summary>
        /// Index of the band based on the counter.
        /// </summary>
        /// <param name="indexCounter">Counter (similar to vertexCount) to get the index for.</param>
        /// <returns>Index of the band as string.</returns>
        public string GetBandIndex(int indexCounter)
        {
            if (bandInfo == null)
                bandInfo = myXmlDoc.SelectNodes("//Spectral_Band_Info");
            return bandInfo[indexCounter].SelectSingleNode("BAND_INDEX").InnerText;
        }

        /// <summary>
        /// Get the name of the band.
        /// </summary>
        /// <param name="bandIndex">Index of the band for which to get the name.</param>
        /// <returns>Band name as string.</returns>
        public string GetBandDesc(int bandIndex)
        {
            if (bandInfo == null)
                bandInfo = myXmlDoc.SelectNodes("//Spectral_Band_Info");
            return bandInfo[bandIndex - 1].SelectSingleNode("BAND_DESCRIPTION").InnerText;
        }

        /// <summary>
        /// Get minimum value for the band.
        /// </summary>
        /// <param name="bandIndex">Index of the band for which to get the value.</param>
        /// <returns>Value requested as string.</returns>
        public string GetBandStatMin(int bandIndex)
        {
            if (bandStats == null)
                bandStats = myXmlDoc.SelectNodes("//Band_Statistics");
            return bandStats[bandIndex - 1].SelectSingleNode("STX_LIN_MIN").InnerText;
        }

        /// <summary>
        /// Get maximum value for the band.
        /// </summary>
        /// <param name="bandIndex">Index of the band for which to get the value.</param>
        /// <returns>Value requested as string.</returns>
        public string GetBandStatMax(int bandIndex)
        {
            if (bandStats == null)
                bandStats = myXmlDoc.SelectNodes("//Band_Statistics");
            return bandStats[bandIndex - 1].SelectSingleNode("STX_LIN_MAX").InnerText;
        }

        /// <summary>
        /// Get mean value for the band.
        /// </summary>
        /// <param name="bandIndex">Index of the band for which to get the value.</param>
        /// <returns>Value requested as string.</returns>
        public string GetBandStatMean(int bandIndex)
        {
            if (bandStats == null)
                bandStats = myXmlDoc.SelectNodes("//Band_Statistics");
            return bandStats[bandIndex - 1].SelectSingleNode("STX_MEAN").InnerText;
        }

        /// <summary>
        /// Get standard deviation value for the band.
        /// </summary>
        /// <param name="bandIndex">Index of the band for which to get the value.</param>
        /// <returns>Value requested as string.</returns>
        public string GetBandStatStdDev(int bandIndex)
        {
            if (bandStats == null)
                bandStats = myXmlDoc.SelectNodes("//Band_Statistics");
            return bandStats[bandIndex - 1].SelectSingleNode("STX_STDV").InnerText;
        }

        /// <summary>
        /// Get the product type for the dataset.
        /// </summary>
        public string ProductType
        {
            get
            {
                return myXmlDoc.SelectSingleNode("//PRODUCT_TYPE").InnerText;
            }
        }

        /// <summary>
        /// Get the sensor name for the dataset.
        /// </summary>
        public string MetadataProfile
        {
            get
            {
                return myXmlDoc.SelectSingleNode("//METADATA_PROFILE").InnerText;
            }
        }

        /// <summary>
        /// Get the geometric processing for the dataset.
        /// </summary>
        public string GeometricProcessing
        {
            get
            {
                return myXmlDoc.SelectSingleNode("//GEOMETRIC_PROCESSING").InnerText;
            }
        }

        /// <summary>
        /// Get the Acquisition Date for the dataset.
        /// </summary>
        public string AcquisitionDate
        {
            get
            {
                return myXmlDoc.SelectSingleNode("//IMAGING_DATE").InnerText;
            }
        }

        /// <summary>
        /// Get the Acquisition Time for the dataset.
        /// </summary>
        public string AcquisitionTime
        {
            get
            {
                return myXmlDoc.SelectSingleNode("//IMAGING_TIME").InnerText;
            }
        }

        /// <summary>
        /// Get the Sensor Angle for the dataset.
        /// </summary>
        public string IncidenceAngle
        {
            get
            {
                return myXmlDoc.SelectSingleNode("//INCIDENCE_ANGLE").InnerText;
            }
        }

        /// <summary>
        /// Get the Sun Azimuth for the dataset.
        /// </summary>
        public string SunAzimuth
        {
            get
            {
                return myXmlDoc.SelectSingleNode("//SUN_AZIMUTH").InnerText;
            }
        }

        /// <summary>
        /// Get the Sun Elevation for the dataset.
        /// </summary>
        public string SunElevation
        {
            get
            {
                return myXmlDoc.SelectSingleNode("//SUN_ELEVATION").InnerText;
            }
        }

        /// <summary>
        /// Get the epsg code for the spatial reference of the dataset.
        /// </summary>
        public string SrsCode
        {
            get
            {
                return myXmlDoc.SelectSingleNode("//HORIZONTAL_CS_CODE").InnerText;
            }
        }

        /// <summary>
        /// Get the Sensor Azimuth for the dataset.
        /// </summary>
        public string SensorAzimuth
        {
            get
            {
                XmlNodeList qualityAssessments = myXmlDoc.SelectNodes("//Quality_Assessment");
                XmlNodeList qualityPars = qualityAssessments[1].SelectNodes("Quality_Parameter");
                for (int i = 0; i < qualityPars.Count; ++i)
                {
                    if (qualityPars[i].SelectSingleNode("QUALITY_PARAMETER_CODE").InnerText.Contains("SENSOR_AZIMUTH"))
                        return qualityPars[i].SelectSingleNode("QUALITY_PARAMETER_VALUE").InnerText;
                }
                return "";
            }
        }
    }
}