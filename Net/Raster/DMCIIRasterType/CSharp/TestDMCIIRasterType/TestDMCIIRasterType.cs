using System;
using System.IO;
using ESRI.ArcGIS;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

/*
 * 
 * This test application is an optional test application to create a mosaic dataset 
 * using the DMCII custom raster type and add data to it from the source specified 
 * by the user. The user can also change properties like product temaplates and 
 * product filters and choose whether to build overviews and what geodatabase to 
 * create the mosaic dataset in. 
 * Usage:
 * Change the properties under the 'Setup MD Parameters' region to control what 
 * geodatabase to create, where to create it, its name and the name of the md.
 * Specify where to add the data from.
 * Choose whether to empty and or create the output directory (gdb parent folder).
 * Run the application. The console will show detailed messaging while the 
 * application runs, including any errors that occur.
 * 
 */

namespace TestDMCIIRasterType
{
    class TestDMCIIRasterType
    {
        [STAThread]
        static void Main(string[] args)
        {
            #region Initialize License
            ESRI.ArcGIS.esriSystem.AoInitialize aoInit = null;
            try
            {
                Console.WriteLine("Obtaining license");
                ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.Desktop);
                aoInit = new AoInitializeClass();
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
                #region Setup MD Parameters
                MDParameters.gdbParentFolder = @"e:\MD\CustomRasterType\DMCII";
                // Choose which type of gdb to create/open.
                // 0 - Create File Gdb
                // 1 - Create Personal Gdb
                // 2 - Open SDE
                int gdbOption = 0;
                // Provide the proper extension based on the gdb you want to create. 
                // e.g. MDParameters.gdbName = "samplePGdb.mdb" to create a personal gdb.
                // To use an SDE, set SDE connection properties below.
                MDParameters.gdbName = @"CustomTypeGdb.gdb";
                MDParameters.mosaicDatasetName = @"CustomTypeMD";

                // Specify the srs of the mosaic dataset
                ISpatialReferenceFactory spatialrefFactory = new SpatialReferenceEnvironmentClass();
                MDParameters.mosaicDatasetSrs = spatialrefFactory.CreateProjectedCoordinateSystem(
                    (int)(esriSRProjCSType.esriSRProjCS_World_Mercator));

                // 0 and PT_UNKNOWN for bits and bands = use defaults.
                MDParameters.mosaicDatasetBands = 0;
                MDParameters.mosaicDatasetBits = rstPixelType.PT_UNKNOWN;
                MDParameters.configKeyword = "";
                
                // Product Definition key choices:
                // None
                // NATURAL_COLOR_RGB
                // NATURAL_COLOR_RGBI
                // FALSE_COLOR_IRG
                // FORMOSAT-2_4BANDS
                // GEOEYE-1_4BANDS
                // IKONOS_4BANDS
                // KOMPSAT-2_4BANDS
                // LANDSAT_6BANDS
                // LANDSAT_MSS_4BANDS
                // QUICKBIRD_4BANDS
                // RAPIDEYE_5BANDS
                // SPOT-5_4BANDS
                // WORLDVIEW-2_8BANDS

                // Setting this property ensures any data added to the MD with its
                // metadata defined gets added with the correct band combination.
                MDParameters.productDefinitionKey = "FALSE_COLOR_IRG";

                MDParameters.rasterTypeName = "DMCIIRasterType";
                // The next two properties can be left blank for defaults
                // The product filter defines which specific product of the raster
                // type to add, e.g. To specfiy Quickbird Basic use value "Basic"
                MDParameters.rasterTypeProductFilter = "";
                // The product name specifies which template to use when adding data.
                // e.g. "Pansharpen and Multispectral" means both multispectral and 
                // pansharpened rasters are added to the mosaic dataset.
                MDParameters.rasterTypeProductName = "Raw";

                // Data source from which to read the data.
                MDParameters.dataSource = @"F:\Data\DMCii";
                MDParameters.dataSourceFilter = @"";
                // No need to set if data source has an srs or if you want to use the MD srs as data source srs.
                MDParameters.dataSourceSrs = null;

                MDParameters.buildOverviews = false;
                #endregion

                MDParameters.emptyGdbFolder = false;
                MDParameters.createGdbParentFolder = false;
                #region Empty/Create Output Directory
                if (MDParameters.emptyGdbFolder)
                {
                    try
                    {
                        Console.WriteLine("Emptying Output Directory");
                        Directory.Delete(MDParameters.gdbParentFolder, true);
                        Directory.CreateDirectory(MDParameters.gdbParentFolder);
                    }
                    catch (Exception)
                    {
                    }
                }
                if (MDParameters.createGdbParentFolder && !System.IO.Directory.Exists(MDParameters.gdbParentFolder))
                {
                    Console.WriteLine("Creating Output Directory");
                    Directory.CreateDirectory(MDParameters.gdbParentFolder);
                }
                #endregion

                CreateMD createMD = new CreateMD();

                if (gdbOption == 0)
                {
                    #region Create MD in File GDB
                    Console.WriteLine("Creating File GDB: " + MDParameters.gdbName);
                    IWorkspace fgdbWorkspace = CreateFileGdbWorkspace(MDParameters.gdbParentFolder, MDParameters.gdbName);
                    createMD.CreateMosaicDataset(fgdbWorkspace);
                    #endregion
                }
                else if (gdbOption == 1)
                {
                    #region Create MD in Personal GDB
                    Console.WriteLine("Creating Personal GDB: " + MDParameters.gdbName);
                    IWorkspace pGdbWorkspace = CreateAccessWorkspace(MDParameters.gdbParentFolder, MDParameters.gdbName);
                    createMD.CreateMosaicDataset(pGdbWorkspace);
                    #endregion
                }
                else if (gdbOption == 2)
                {
                    #region Open SDE GDB
                    // Set SDE connection properties.
                    IPropertySet sdeProperties = new PropertySetClass();
                    sdeProperties.SetProperty("SERVER", "barbados");
                    sdeProperties.SetProperty("INSTANCE", "9411");
                    sdeProperties.SetProperty("VERSION", "sde.DEFAULT");
                    sdeProperties.SetProperty("USER", "gdb");
                    sdeProperties.SetProperty("PASSWORD", "gdb");
                    sdeProperties.SetProperty("DATABASE", "VTEST");
                    IWorkspace sdeWorkspace = CreateSdeWorkspace(sdeProperties);
                    if (sdeWorkspace == null)
                    {
                        Console.WriteLine("Could not open SDE workspace: ");
                        return;
                    }

                    #endregion

                    #region Create MD in SDE
                    MDParameters.mosaicDatasetName = @"sampleMD";
                    createMD.CreateMosaicDataset(sdeWorkspace);
                    #endregion
                }

                #region Shutdown
                Console.WriteLine("Press any key...");
                Console.ReadKey();
                // Shutdown License
                aoInit.Shutdown();
                #endregion
            }
            catch (Exception exc)
            {
                #region Report
                Console.WriteLine("Exception Caught in Main: " + exc.Message);
                Console.WriteLine("Shutting down.");
                #endregion

                #region Shutdown
                Console.WriteLine("Press any key...");
                Console.ReadKey();
                // Shutdown License
                aoInit.Shutdown();
                #endregion
            }
        }

        /// <summary>
        /// Create a File Geodatabase given the name and parent folder.
        /// </summary>
        /// <param name="gdbParentFolder">Folder to create the new gdb in.</param>
        /// <param name="gdbName">Name of the gdb to be created.</param>
        /// <returns>Workspace reference to the new geodatabase.</returns>
        public static IWorkspace CreateFileGdbWorkspace(string gdbParentFolder, string gdbName)
        {
            // Instantiate a file geodatabase workspace factory and create a file geodatabase.
            // The Create method returns a workspace object.
            Type factoryType = Type.GetTypeFromProgID(
              "esriDataSourcesGDB.FileGDBWorkspaceFactory");
            IWorkspaceFactory workspaceFactory = (IWorkspaceFactory)
              Activator.CreateInstance(factoryType);
            IWorkspaceName workspaceName = workspaceFactory.Create(gdbParentFolder,
                gdbName, null, 0);

            // Cast the workspace name object to the IName interface and open the workspace.
            IName name = (IName)workspaceName;
            IWorkspace workspace = (IWorkspace)name.Open();
            return workspace;
        }

        /// <summary>
        /// Create a Personal Geodatabase given the name and parent folder.
        /// </summary>
        /// <param name="gdbParentFolder">Folder to create the new gdb in.</param>
        /// <param name="gdbName">Name of the gdb to be created.</param>
        /// <returns>Workspace reference to the new geodatabase.</returns>
        public static IWorkspace CreateAccessWorkspace(string gdbParentFolder, string gdbName)
        {
            // Instantiate an Access workspace factory and create a personal geodatabase.
            // The Create method returns a workspace object.
            Type factoryType = Type.GetTypeFromProgID(
              "esriDataSourcesGDB.AccessWorkspaceFactory");
            IWorkspaceFactory workspaceFactory = (IWorkspaceFactory)
              Activator.CreateInstance(factoryType);
            IWorkspaceName workspaceName = workspaceFactory.Create(gdbParentFolder,
                gdbName, null, 0);

            // Cast the workspace name object to the IName interface and open the workspace.
            IName name = (IName)workspaceName;
            IWorkspace workspace = (IWorkspace)name.Open();
            return workspace;
        }

        /// <summary>
        /// Retrieves an SDE workspace using the specified property set.
        /// </summary>
        /// <param name="propertySet">The connection parameters.</param>
        /// <returns>An IWorkspace reference to an SDE workspace.</returns>
        public static IWorkspace CreateSdeWorkspace(IPropertySet propertySet)
        {
            // Create the workspace factory and connect to the workspace.
            Type factoryType = Type.GetTypeFromProgID("esriDataSourcesGDB.SdeWorkspaceFactory");
            IWorkspaceFactory workspaceFactory = (IWorkspaceFactory)Activator.CreateInstance(factoryType);
            IWorkspace workspace = workspaceFactory.Open(propertySet, 0);                        
            return workspace;
        }

    };

    public static class MDParameters
    {
        // Define the folder in which the GDB resides or is to be created
        public static string gdbParentFolder;
        // Name of the GDB
        public static string gdbName;
        // Configuration keyword for the Gdb (optional)
        public static string configKeyword;

        // Mosaic Dataset Properties
        public static string mosaicDatasetName; // Name of the Mosaic Dataset to create.
        public static ISpatialReference mosaicDatasetSrs; // Srs for the Mosaic Dataset
        public static int mosaicDatasetBands; // Number of bands of the Mosaic Dataset
        public static rstPixelType mosaicDatasetBits; // Pixel Type of the Mosaic Dataset
        public static string productDefinitionKey; // The product definition key.
        public static IArray productDefinitionProps; // Properties of the product definition (Bands names and wavelengths).

        // Raster Type Properties
        public static string rasterTypeName; // Name of the Raster type to use (or path to the .art file)
        public static string rasterTypeProductFilter; // The product filter to set on the Raster Type
        public static string rasterTypeProductName; // The name of the product to create from the added data
        public static bool rasterTypeAddDEM; // Flag to specify whether to add a DEM to a Raster Type.
        public static string rasterTypeDemPath; // Path to the DEM if previous property is true.

        // Crawler Properties
        public static string dataSource; // Path to the data.
        public static string dataSourceFilter; // File filter to use to crawl data.
        // Srs of the input data if the input does not contain an srs 
        // and it is different from the srs of the mosaic dataset(optional)
        public static ISpatialReference dataSourceSrs;

        // Operational flags
        public static bool buildOverviews; // Generate overviews for the Mosaic Dataset
        public static bool emptyGdbFolder; // Delete the parent folder for the GDB
        public static bool createGdbParentFolder; // Create the Parent folder for the GDB
    };

    public class CreateMD
    {
        /// <summary>
        /// Create a Mosaic Dataset in the geodatabase provided using the parameters defined by MDParamaters.
        /// </summary>
        /// <param name="gdbWorkspace">Geodatabase to create the Mosaic dataser in.</param>
        public void CreateMosaicDataset(IWorkspace gdbWorkspace)
        {
            try
            {
                #region Global Declarations
                IMosaicDataset theMosaicDataset = null;
                IMosaicDatasetOperation theMosaicDatasetOperation = null;
                IMosaicWorkspaceExtensionHelper mosaicExtHelper = null;
                IMosaicWorkspaceExtension mosaicExt = null;
                #endregion

                #region CreateMosaicDataset
                try
                {
                    Console.WriteLine("Create Mosaic Dataset: " + MDParameters.mosaicDatasetName + ".amd");
                    /// Setup workspaces.
                    /// Create Srs
                    ISpatialReferenceFactory spatialrefFactory = new SpatialReferenceEnvironmentClass();

                    // Create the mosaic dataset creation parameters object.
                    ICreateMosaicDatasetParameters creationPars = new CreateMosaicDatasetParametersClass();
                    // Set the number of bands for the mosaic dataset.
                    // If defined as zero leave defaults
                    if (MDParameters.mosaicDatasetBands != 0)
                        creationPars.BandCount = MDParameters.mosaicDatasetBands;
                    // Set the pixel type of the mosaic dataset.
                    // If defined as unknown leave defaults
                    if (MDParameters.mosaicDatasetBits != rstPixelType.PT_UNKNOWN)
                        creationPars.PixelType = MDParameters.mosaicDatasetBits;
                    // Create the mosaic workspace extension helper class.
                    mosaicExtHelper = new MosaicWorkspaceExtensionHelperClass();
                    // Find the right extension from the workspace.
                    mosaicExt = mosaicExtHelper.FindExtension(gdbWorkspace);

                    // Default is none.
                    if (MDParameters.productDefinitionKey.ToLower() != "none")
                    {
                        // Set the product definition keyword and properties.
                        // (The property is called band definition keyword and band properties in the object).
                        ((ICreateMosaicDatasetParameters2)creationPars).BandDefinitionKeyword = MDParameters.productDefinitionKey;
                        MDParameters.productDefinitionProps = SetBandProperties(MDParameters.productDefinitionKey);
                        if (MDParameters.productDefinitionProps.Count == 0)
                        {
                            Console.WriteLine("Setting production definition properties failed.");
                            return;
                        }
                        ((ICreateMosaicDatasetParameters2)creationPars).BandProperties = MDParameters.productDefinitionProps;
                    }
                    
                    // Use the extension to create a new mosaic dataset, supplying the 
                    // spatial reference and the creation parameters object created above.
                    theMosaicDataset = mosaicExt.CreateMosaicDataset(MDParameters.mosaicDatasetName,
                        MDParameters.mosaicDatasetSrs, creationPars, MDParameters.configKeyword);
                }
                catch (Exception exc)
                {
                    Console.WriteLine("Exception Caught while creating Mosaic Dataset: " + exc.Message);
                    return;
                }
                #endregion

                #region OpenMosaicDataset
                Console.WriteLine("Opening Mosaic Dataset");
                theMosaicDataset = null;
                // Use the extension to open the mosaic dataset.
                theMosaicDataset = mosaicExt.OpenMosaicDataset(MDParameters.mosaicDatasetName);
                // The mosaic dataset operation interface is used to perform operations on 
                // a mosaic dataset.
                theMosaicDatasetOperation = (IMosaicDatasetOperation)(theMosaicDataset);
                #endregion

                #region Preparing Raster Type
                Console.WriteLine("Preparing Raster Type");
                // Create a Raster Type Name object.
                IRasterTypeName theRasterTypeName = new RasterTypeNameClass();
                // Assign the name of the Raster Type to the name object.
                // The Name field accepts a path to an .art file as well 
                // the name for a built in Raster Type.
                theRasterTypeName.Name = MDParameters.rasterTypeName;
                // Use the Open function from the IName interface to get the Raster Type object.
                IRasterType theRasterType = (IRasterType)(((IName)theRasterTypeName).Open());
                if (theRasterType == null)
                    Console.WriteLine("Raster Type not found " + MDParameters.rasterTypeName);

                // Set the URI Filter on the loaded raster type.
                if (MDParameters.rasterTypeProductFilter != "")
                {
                    // Get the supported URI filters from the raster type object using the 
                    // raster type properties interface.
                    IArray mySuppFilters = ((IRasterTypeProperties)theRasterType).SupportedURIFilters;
                    IItemURIFilter productFilter = null;
                    for (int i = 0; i < mySuppFilters.Count; ++i)
                    {
                        // Set the desired filter from the supported filters.
                        productFilter = (IItemURIFilter)mySuppFilters.get_Element(i);
                        if (productFilter.Name == MDParameters.rasterTypeProductFilter)
                            theRasterType.URIFilter = productFilter;
                    }
                }
                // Enable the correct templates in the raster type.
                string[] rasterProductNames = MDParameters.rasterTypeProductName.Split(';');
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

                if (MDParameters.dataSourceSrs != null)
                {
                    ((IRasterTypeProperties)theRasterType).SynchronizeParameters.DefaultSpatialReference =
                        MDParameters.dataSourceSrs;
                }
                #endregion

                #region Add DEM To Raster Type
                if (MDParameters.rasterTypeAddDEM && ((IRasterTypeProperties)theRasterType).SupportsOrthorectification)
                {
                    // Open the Raster Dataset
                    Type factoryType = Type.GetTypeFromProgID("esriDataSourcesRaster.RasterWorkspaceFactory");
                    IWorkspaceFactory workspaceFactory = (IWorkspaceFactory)Activator.CreateInstance(factoryType);
                    IRasterWorkspace rasterWorkspace = (IRasterWorkspace)workspaceFactory.OpenFromFile(
                        System.IO.Path.GetDirectoryName(MDParameters.rasterTypeDemPath), 0); ;
                    IRasterDataset myRasterDataset = rasterWorkspace.OpenRasterDataset(
                        System.IO.Path.GetFileName(MDParameters.rasterTypeDemPath));

                    IGeometricFunctionArguments geometricFunctionArguments =
                        new GeometricFunctionArgumentsClass();
                    geometricFunctionArguments.DEM = myRasterDataset;
                    ((IRasterTypeProperties)theRasterType).OrthorectificationParameters =
                        geometricFunctionArguments;
                }
                #endregion

                #region Preparing Data Source Crawler
                Console.WriteLine("Preparing Data Source Crawler");
                // Create a new property set to specify crawler properties.
                IPropertySet crawlerProps = new PropertySetClass();
                // Specify a file filter
                crawlerProps.SetProperty("Filter", MDParameters.dataSourceFilter);
                // Specify whether to search subdirectories.
                crawlerProps.SetProperty("Recurse", true);
                // Specify the source path.
                crawlerProps.SetProperty("Source", MDParameters.dataSource);
                // Get the recommended crawler from the raster type based on the specified 
                // properties using the IRasterBuilder interface.
                IDataSourceCrawler theCrawler = ((IRasterBuilder)theRasterType).GetRecommendedCrawler(crawlerProps);
                #endregion

                #region Add Rasters
                Console.WriteLine("Adding Rasters");
                // Create a AddRaster parameters object.
                IAddRastersParameters AddRastersArgs = new AddRastersParametersClass();
                // Specify the data crawler to be used to crawl the data.
                AddRastersArgs.Crawler = theCrawler;
                // Specify the raster type to be used to add the data.
                AddRastersArgs.RasterType = theRasterType;
                // Use the mosaic dataset operation interface to add 
                // rasters to the mosaic dataset.
                theMosaicDatasetOperation.AddRasters(AddRastersArgs, null);
                #endregion

                #region Compute Pixel Size Ranges
                Console.WriteLine("Computing Pixel Size Ranges");
                // Create a calculate cellsize ranges parameters object.
                ICalculateCellSizeRangesParameters computeArgs = new CalculateCellSizeRangesParametersClass();
                // Use the mosaic dataset operation interface to calculate cellsize ranges.
                theMosaicDatasetOperation.CalculateCellSizeRanges(computeArgs, null);
                #endregion

                #region Building Boundary
                Console.WriteLine("Building Boundary");
                // Create a build boundary parameters object.
                IBuildBoundaryParameters boundaryArgs = new BuildBoundaryParametersClass();
                // Set flags that control boundary generation.
                boundaryArgs.AppendToExistingBoundary = true;
                // Use the mosaic dataset operation interface to build boundary.
                theMosaicDatasetOperation.BuildBoundary(boundaryArgs, null);
                #endregion

                if (MDParameters.buildOverviews)
                {
                    #region Defining Overviews
                    Console.WriteLine("Defining Overviews");
                    // Create a define overview parameters object.
                    IDefineOverviewsParameters defineOvArgs = new DefineOverviewsParametersClass();
                    // Use the overview tile parameters interface to specify the overview factor
                    // used to generate overviews.
                    ((IOverviewTileParameters)defineOvArgs).OverviewFactor = 3;
                    // Use the mosaic dataset operation interface to define overviews.
                    theMosaicDatasetOperation.DefineOverviews(defineOvArgs, null);
                    #endregion

                    #region Compute Pixel Size Ranges
                    Console.WriteLine("Computing Pixel Size Ranges");
                    // Calculate cell size ranges to update the Min/Max pixel sizes.
                    theMosaicDatasetOperation.CalculateCellSizeRanges(computeArgs, null);
                    #endregion

                    #region Generating Overviews
                    Console.WriteLine("Generating Overviews");
                    // Create a generate overviews parameters object.
                    IGenerateOverviewsParameters genPars = new GenerateOverviewsParametersClass();
                    // Set properties to control overview generation.
                    IQueryFilter genQuery = new QueryFilterClass();
                    ((ISelectionParameters)genPars).QueryFilter = genQuery;
                    genPars.GenerateMissingImages = true;
                    genPars.GenerateStaleImages = true;
                    // Use the mosaic dataset operation interface to generate overviews.
                    theMosaicDatasetOperation.GenerateOverviews(genPars, null);
                    #endregion
                }

                #region Report
                Console.WriteLine("Success.");
                #endregion
            }
            catch (Exception exc)
            {
                #region Report
                Console.WriteLine("Exception Caught in CreateMD: " + exc.Message);
                Console.WriteLine("Shutting down.");
                #endregion
            }
        }

        /// <summary>
        /// Create an array with the right BandName and Wavelength values for the corresponding key. 
        /// </summary>
        /// <param name="key">Key to use.</param>
        /// <returns>Array with the correct BandName and Wavelength values.</returns>
        private static IArray SetBandProperties(string key)
        {
            IArray productDefProps = new ArrayClass();
            IPropertySet band1Def = new PropertySetClass();
            IPropertySet band2Def = new PropertySetClass();
            IPropertySet band3Def = new PropertySetClass();
            if (key == "NATURAL_COLOR_RGB" || key == "NATURAL_COLOR_RGBI")
            {
                band1Def.SetProperty("BandName", "Red");
                band1Def.SetProperty("WavelengthMin", 630);
                band1Def.SetProperty("WavelengthMax", 690);

                band2Def.SetProperty("BandName", "Green");
                band2Def.SetProperty("WavelengthMin", 530);
                band2Def.SetProperty("WavelengthMax", 570);

                band3Def.SetProperty("BandName", "Blue");
                band3Def.SetProperty("WavelengthMin", 440);
                band3Def.SetProperty("WavelengthMax", 480);

                productDefProps.Add(band1Def);
                productDefProps.Add(band2Def);
                productDefProps.Add(band3Def);

                if (key == "NATURAL_COLOR_RGBI")
                {
                    IPropertySet band4Def = new PropertySetClass();
                    band4Def.SetProperty("BandName", "NearInfrared");
                    band4Def.SetProperty("WavelengthMin", 770);
                    band4Def.SetProperty("WavelengthMax", 830);
                    productDefProps.Add(band4Def);
                }
            }
            else if (key == "FALSE_COLOR_IRG")
            {
                band1Def.SetProperty("BandName", "Infrared");
                band1Def.SetProperty("WavelengthMin", 770);
                band1Def.SetProperty("WavelengthMax", 830);

                band2Def.SetProperty("BandName", "Red");
                band2Def.SetProperty("WavelengthMin", 630);
                band2Def.SetProperty("WavelengthMax", 690);

                band3Def.SetProperty("BandName", "Green");
                band3Def.SetProperty("WavelengthMin", 530);
                band3Def.SetProperty("WavelengthMax", 570);

                productDefProps.Add(band1Def);
                productDefProps.Add(band2Def);
                productDefProps.Add(band3Def);
            }
            else if (key == "FORMOSAT-2_4BANDS")
            {
                IPropertySet band4Def = new PropertySetClass();

                band1Def.SetProperty("BandName", "Blue");
                band1Def.SetProperty("WavelengthMin", 450);
                band1Def.SetProperty("WavelengthMax", 520);

                band2Def.SetProperty("BandName", "Green");
                band2Def.SetProperty("WavelengthMin", 520);
                band2Def.SetProperty("WavelengthMax", 600);

                band3Def.SetProperty("BandName", "Red");
                band3Def.SetProperty("WavelengthMin", 630);
                band3Def.SetProperty("WavelengthMax", 690);

                band4Def.SetProperty("BandName", "NearInfrared");
                band4Def.SetProperty("WavelengthMin", 760);
                band4Def.SetProperty("WavelengthMax", 900);

                productDefProps.Add(band1Def);
                productDefProps.Add(band2Def);
                productDefProps.Add(band3Def);
                productDefProps.Add(band4Def);
            }
            else if (key == "GEOEYE-1_4BANDS")
            {
                IPropertySet band4Def = new PropertySetClass();

                band1Def.SetProperty("BandName", "Blue");
                band1Def.SetProperty("WavelengthMin", 450);
                band1Def.SetProperty("WavelengthMax", 510);

                band2Def.SetProperty("BandName", "Green");
                band2Def.SetProperty("WavelengthMin", 510);
                band2Def.SetProperty("WavelengthMax", 580);

                band3Def.SetProperty("BandName", "Red");
                band3Def.SetProperty("WavelengthMin", 655);
                band3Def.SetProperty("WavelengthMax", 690);

                band4Def.SetProperty("BandName", "NearInfrared");
                band4Def.SetProperty("WavelengthMin", 780);
                band4Def.SetProperty("WavelengthMax", 920);

                productDefProps.Add(band1Def);
                productDefProps.Add(band2Def);
                productDefProps.Add(band3Def);
                productDefProps.Add(band4Def);
            }
            else if (key == "IKONOS_4BANDS")
            {
                IPropertySet band4Def = new PropertySetClass();

                band1Def.SetProperty("BandName", "Blue");
                band1Def.SetProperty("WavelengthMin", 445);
                band1Def.SetProperty("WavelengthMax", 516);

                band2Def.SetProperty("BandName", "Green");
                band2Def.SetProperty("WavelengthMin", 506);
                band2Def.SetProperty("WavelengthMax", 595);

                band3Def.SetProperty("BandName", "Red");
                band3Def.SetProperty("WavelengthMin", 632);
                band3Def.SetProperty("WavelengthMax", 698);

                band4Def.SetProperty("BandName", "NearInfrared");
                band4Def.SetProperty("WavelengthMin", 757);
                band4Def.SetProperty("WavelengthMax", 863);

                productDefProps.Add(band1Def);
                productDefProps.Add(band2Def);
                productDefProps.Add(band3Def);
                productDefProps.Add(band4Def);
            }
            else if (key == "KOMPSAT-2_4BANDS")
            {
                IPropertySet band4Def = new PropertySetClass();

                band1Def.SetProperty("BandName", "Blue");
                band1Def.SetProperty("WavelengthMin", 450);
                band1Def.SetProperty("WavelengthMax", 520);

                band2Def.SetProperty("BandName", "Green");
                band2Def.SetProperty("WavelengthMin", 520);
                band2Def.SetProperty("WavelengthMax", 600);

                band3Def.SetProperty("BandName", "Red");
                band3Def.SetProperty("WavelengthMin", 630);
                band3Def.SetProperty("WavelengthMax", 690);

                band4Def.SetProperty("BandName", "NearInfrared");
                band4Def.SetProperty("WavelengthMin", 760);
                band4Def.SetProperty("WavelengthMax", 900);

                productDefProps.Add(band1Def);
                productDefProps.Add(band2Def);
                productDefProps.Add(band3Def);
                productDefProps.Add(band4Def);
            }
            else if (key == "LANDSAT_6BANDS")
            {
                IPropertySet band4Def = new PropertySetClass();
                IPropertySet band5Def = new PropertySetClass();
                IPropertySet band6Def = new PropertySetClass();

                band1Def.SetProperty("BandName", "Blue");
                band1Def.SetProperty("WavelengthMin", 450);
                band1Def.SetProperty("WavelengthMax", 520);

                band2Def.SetProperty("BandName", "Green");
                band2Def.SetProperty("WavelengthMin", 520);
                band2Def.SetProperty("WavelengthMax", 600);

                band3Def.SetProperty("BandName", "Red");
                band3Def.SetProperty("WavelengthMin", 630);
                band3Def.SetProperty("WavelengthMax", 690);

                band4Def.SetProperty("BandName", "NearInfrared_1");
                band4Def.SetProperty("WavelengthMin", 760);
                band4Def.SetProperty("WavelengthMax", 900);

                band5Def.SetProperty("BandName", "NearInfrared_2");
                band5Def.SetProperty("WavelengthMin", 1550);
                band5Def.SetProperty("WavelengthMax", 1750);

                band6Def.SetProperty("BandName", "MidInfrared");
                band6Def.SetProperty("WavelengthMin", 2080);
                band6Def.SetProperty("WavelengthMax", 2350);

                productDefProps.Add(band1Def);
                productDefProps.Add(band2Def);
                productDefProps.Add(band3Def);
                productDefProps.Add(band4Def);
                productDefProps.Add(band5Def);
                productDefProps.Add(band6Def);
            }
            else if (key == "QUICKBIRD_4BANDS")
            {
                IPropertySet band4Def = new PropertySetClass();

                band1Def.SetProperty("BandName", "Blue");
                band1Def.SetProperty("WavelengthMin", 450);
                band1Def.SetProperty("WavelengthMax", 520);

                band2Def.SetProperty("BandName", "Green");
                band2Def.SetProperty("WavelengthMin", 520);
                band2Def.SetProperty("WavelengthMax", 600);

                band3Def.SetProperty("BandName", "Red");
                band3Def.SetProperty("WavelengthMin", 630);
                band3Def.SetProperty("WavelengthMax", 690);

                band4Def.SetProperty("BandName", "NearInfrared");
                band4Def.SetProperty("WavelengthMin", 760);
                band4Def.SetProperty("WavelengthMax", 900);

                productDefProps.Add(band1Def);
                productDefProps.Add(band2Def);
                productDefProps.Add(band3Def);
                productDefProps.Add(band4Def);
            }
            else if (key == "RAPIDEYE_5BANDS")
            {
                IPropertySet band4Def = new PropertySetClass();
                IPropertySet band5Def = new PropertySetClass();

                band1Def.SetProperty("BandName", "Blue");
                band1Def.SetProperty("WavelengthMin", 440);
                band1Def.SetProperty("WavelengthMax", 510);

                band2Def.SetProperty("BandName", "Green");
                band2Def.SetProperty("WavelengthMin", 520);
                band2Def.SetProperty("WavelengthMax", 590);

                band3Def.SetProperty("BandName", "Red");
                band3Def.SetProperty("WavelengthMin", 630);
                band3Def.SetProperty("WavelengthMax", 685);

                band4Def.SetProperty("BandName", "RedEdge");
                band4Def.SetProperty("WavelengthMin", 690);
                band4Def.SetProperty("WavelengthMax", 730);

                band5Def.SetProperty("BandName", "NearInfrared");
                band5Def.SetProperty("WavelengthMin", 760);
                band5Def.SetProperty("WavelengthMax", 850);

                productDefProps.Add(band1Def);
                productDefProps.Add(band2Def);
                productDefProps.Add(band3Def);
                productDefProps.Add(band4Def);
                productDefProps.Add(band5Def);
            }
            else if (key == "SPOT-5_4BANDS")
            {
                IPropertySet band4Def = new PropertySetClass();

                band1Def.SetProperty("BandName", "Green");
                band1Def.SetProperty("WavelengthMin", 500);
                band1Def.SetProperty("WavelengthMax", 590);

                band2Def.SetProperty("BandName", "Red");
                band2Def.SetProperty("WavelengthMin", 610);
                band2Def.SetProperty("WavelengthMax", 680);

                band3Def.SetProperty("BandName", "NearInfrared");
                band3Def.SetProperty("WavelengthMin", 780);
                band3Def.SetProperty("WavelengthMax", 890);

                band4Def.SetProperty("BandName", "ShortWaveInfrared");
                band4Def.SetProperty("WavelengthMin", 1580);
                band4Def.SetProperty("WavelengthMax", 1750);

                productDefProps.Add(band1Def);
                productDefProps.Add(band2Def);
                productDefProps.Add(band3Def);
                productDefProps.Add(band4Def);
            }
            else if (key == "WORLDVIEW-2_8BANDS")
            {
                IPropertySet band4Def = new PropertySetClass();
                IPropertySet band5Def = new PropertySetClass();
                IPropertySet band6Def = new PropertySetClass();
                IPropertySet band7Def = new PropertySetClass();
                IPropertySet band8Def = new PropertySetClass();

                band1Def.SetProperty("BandName", "CoastalBlue");
                band1Def.SetProperty("WavelengthMin", 400);
                band1Def.SetProperty("WavelengthMax", 450);

                band2Def.SetProperty("BandName", "Blue");
                band2Def.SetProperty("WavelengthMin", 450);
                band2Def.SetProperty("WavelengthMax", 510);

                band3Def.SetProperty("BandName", "Green");
                band3Def.SetProperty("WavelengthMin", 510);
                band3Def.SetProperty("WavelengthMax", 580);

                band4Def.SetProperty("BandName", "Yellow");
                band4Def.SetProperty("WavelengthMin", 585);
                band4Def.SetProperty("WavelengthMax", 625);

                band5Def.SetProperty("BandName", "Red");
                band5Def.SetProperty("WavelengthMin", 630);
                band5Def.SetProperty("WavelengthMax", 690);

                band6Def.SetProperty("BandName", "RedEdge");
                band6Def.SetProperty("WavelengthMin", 705);
                band6Def.SetProperty("WavelengthMax", 745);

                band7Def.SetProperty("BandName", "NearInfrared_1");
                band7Def.SetProperty("WavelengthMin", 770);
                band7Def.SetProperty("WavelengthMax", 895);

                band8Def.SetProperty("BandName", "NearInfrared_2");
                band8Def.SetProperty("WavelengthMin", 860);
                band8Def.SetProperty("WavelengthMax", 1040);

                productDefProps.Add(band1Def);
                productDefProps.Add(band2Def);
                productDefProps.Add(band3Def);
                productDefProps.Add(band4Def);
                productDefProps.Add(band5Def);
                productDefProps.Add(band6Def);
                productDefProps.Add(band7Def);
                productDefProps.Add(band8Def);
            }
            return productDefProps;
        }
    }
}
