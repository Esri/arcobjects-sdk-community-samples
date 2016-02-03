using System;
using System.IO;
using ESRI.ArcGIS;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.GISClient;
using ESRI.ArcGIS.Server;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.DataSourcesRaster;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Runtime.InteropServices;
using System.Web;
namespace ISConfig
{
    /// <summary>
    /// 1. Description:
    /// This sample demonstrate how to create an image service and set configurations based on data source types (raster dataset, mosaic dataset, raster layer); it also have additional features to start,stop,delete image service programmatically.
    /// The user running this utility needs access to data source.
    /// The application can be run locally on AGSServer machine or remotely.
    /// Source data must be accessible by the account that runs ArcGIS Server. Data is not copied to ArcGIS Server through this tool.
    /// CachingScheme/Metadata/thumbnail/iteminfo can't be defined through this tool.
    /// CachingScheme can be done through Caching geoprocessing tools. REST Metadata/thumbnail/iteminfo are not available through this app but can be developed using a similar approach to server admin endpoint
    /// The sample uses Raster API to populate dataset properties (used to construct service configuration), and can eitehr directly invoke rest Admin API to create service; or using AGSClient to create service.
    /// 2. Case sensitivity:  
    /// (1) switches are case sensitive. 
    /// (2) when publish a service, the service name is case sensitive
    /// 3. Usage:
    /// Run from command line environment. Usage. <>: required parameter; |: pick one.
    /// isconfig -o publish -h <host_adminurl> -d <datapath> -n <serviceName>
    /// isconfig -o <delete|start|stop|pause> -h <host> -n <serviceName>
    /// isconfig -o <list> -h <host>
    /// Example 1: isconfig -o publish -h "http://host:6080/arcgis/admin" -u "adminuser" -p "adminpassword" -d \\myserver\data\test.gdb\mdtest -n mdtest
    /// Example 2: isconfig -o stop -h myservername -u "adminuser" -p "adminpassword" -n mdtest
    /// Example 3: isconfig -o list -h myservername -u "adminuser" -p "adminpassword"
    /// </summary>
    class ISConfig
    {
        #region static variables
        private static string sourcePath = ""; //data source path: a raster dataset, a mosaic dataset
        private static string restAdmin = ""; //host admin url e.g. http://host:6080/arcgis/admin
        private static string serviceName = ""; //image service name        
        private static IRasterDataset rasterDataset = null;
        private static IServerObjectAdmin soAdmin = null;
        private static string username = ""; //user name for publisher/admin
        private static string password = ""; //password for publisher/admin
        #endregion

        [STAThread]
        public static void Main(string[] args)
        {
            try
            {
                //args = new string[] { "-o", "publish", "-h", "http://server:6080/arcgis/admin", "-u", "adminuser", "-p", "adminuser", "-d", @"\\server\test\rgb.tif", "-n", "mdtest12356" };
                //args = new string[] { "-o", "list", "-h", "http://server:6080/arcgis/admin", "-u", "adminuser", "-p", "adminuser" };//@"\\server\Images.gdb\mdtest"
                //validation
                if (!ValidateParams(args))
                    return;
                //license           
                if (!InitLicense())
                    return;

                //retrieve parameters
                Retrieve_Params(args);
                string operation = args[1];
                switch (operation.ToLower())
                {
                    case "publish":
                        //CreateISConfig();
                        CreateISConfig_RESTAdmin();
                        break;
                    case "delete":
                        //DeleteService();
                        DeleteService_RESTAdmin();
                        break;
                    case "start":
                        //StartService();
                        StartService_RESTAdmin();
                        break;
                    case "stop":
                        //StopService();
                        StopService_RESTAdmin();
                        break;
                    case "pause":
                        PauseService();
                        break;
                    case "list":
                        //ListServices();
                        ListServices_RESTAdmin();
                        break;
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine("Error: {0}", exc.Message);
            }
        }


        #region management operations
        /// <summary>
        /// create image service configuration
        /// </summary>
        private static void CreateISConfig()
        {
            try
            {
                if (!ConnectAGS(restAdmin)) return;

                //get source type
                esriImageServiceSourceType sourceType = GetSourceType(sourcePath);

                //connect to ArcGIS Server and create configuration
                IServerObjectConfiguration5 soConfig = (IServerObjectConfiguration5)soAdmin.CreateConfiguration();

                //set general service parameters
                soConfig.Name = serviceName;
                soConfig.TypeName = "ImageServer";
                soConfig.TargetCluster = "default";

                soConfig.StartupType = esriStartupType.esriSTAutomatic;
                soConfig.IsPooled = true;
                soConfig.IsolationLevel = esriServerIsolationLevel.esriServerIsolationHigh;
                soConfig.MinInstances = 1;
                soConfig.MaxInstances = 2;

                //customize recycle properties
                IPropertySet propertySet_Recycle = soConfig.RecycleProperties;
                propertySet_Recycle.SetProperty("Interval", "24");


                //path to the data
                IPropertySet propertySet = soConfig.Properties;
                IWorkspace workspace = ((IDataset)rasterDataset).Workspace;
                if (workspace.WorkspaceFactory.WorkspaceType == esriWorkspaceType.esriRemoteDatabaseWorkspace)
                {
                    IWorkspaceName2 wsName2 = ((IDataset)workspace).FullName as IWorkspaceName2;
                    string connString = wsName2.ConnectionString;
                    propertySet.SetProperty("ConnectionString", connString);
                    propertySet.SetProperty("Raster", ((IDataset)rasterDataset).Name);
                }
                else
                    propertySet.SetProperty("Path", sourcePath);
                propertySet.SetProperty("EsriImageServiceSourceType", sourceType.ToString());

                //MIME+URL (virtual directory)
                propertySet.SetProperty("SupportedImageReturnTypes", "MIME+URL");
                IEnumServerDirectory dirs = soAdmin.GetServerDirectories();
                dirs.Reset();
                IServerDirectory serverDir = dirs.Next();
                while (serverDir != null)
                {
                    if (((IServerDirectory2)serverDir).Type == esriServerDirectoryType.esriSDTypeOutput)
                    {
                        propertySet.SetProperty("OutputDir", serverDir.Path);
                        propertySet.SetProperty("VirtualOutputDir", serverDir.URL);
                        break;
                    }
                    serverDir = dirs.Next();
                }

                //copy right
                propertySet.SetProperty("CopyRight", "");

                //properties for a mosaic dataset;
                if (sourceType == esriImageServiceSourceType.esriImageServiceSourceTypeMosaicDataset)
                {
                    IFunctionRasterDataset functionRasterDataset = (IFunctionRasterDataset)rasterDataset;
                    IPropertySet propDefaults = functionRasterDataset.Properties;

                    object names, values;
                    propDefaults.GetAllProperties(out names, out values);
                    List<string> propNames = new List<string>();
                    propNames.AddRange((string[])names);
                    if (propNames.Contains("MaxImageHeight"))
                        propertySet.SetProperty("MaxImageHeight", propDefaults.GetProperty("MaxImageHeight"));//4100
                    if (propNames.Contains("MaxImageWidth"))
                        propertySet.SetProperty("MaxImageWidth", propDefaults.GetProperty("MaxImageWidth"));//15000
                    if (propNames.Contains("AllowedCompressions"))
                        propertySet.SetProperty("AllowedCompressions", propDefaults.GetProperty("AllowedCompressions"));//"None,JPEG,LZ77,LERC"
                    if (propNames.Contains("DefaultResamplingMethod"))
                        propertySet.SetProperty("DefaultResamplingMethod", propDefaults.GetProperty("DefaultResamplingMethod"));//0
                    if (propNames.Contains("DefaultCompressionQuality"))
                        propertySet.SetProperty("DefaultCompressionQuality", propDefaults.GetProperty("DefaultCompressionQuality"));//75
                    if (propNames.Contains("MaxRecordCount"))
                        propertySet.SetProperty("MaxRecordCount", propDefaults.GetProperty("MaxRecordCount"));//500
                    if (propNames.Contains("MaxMosaicImageCount"))
                        propertySet.SetProperty("MaxMosaicImageCount", propDefaults.GetProperty("MaxMosaicImageCount"));//20
                    if (propNames.Contains("MaxDownloadSizeLimit"))
                        propertySet.SetProperty("MaxDownloadSizeLimit", propDefaults.GetProperty("MaxDownloadSizeLimit"));//20
                    if (propNames.Contains("MaxDownloadImageCount"))
                        propertySet.SetProperty("MaxDownloadImageCount", propDefaults.GetProperty("MaxDownloadImageCount"));//20
                    if (propNames.Contains("AllowedFields"))
                        propertySet.SetProperty("AllowedFields", propDefaults.GetProperty("AllowedFields"));//"Name,MinPS,MaxPS,LowPS,HighPS,CenterX,CenterY"
                    if (propNames.Contains("AllowedMosaicMethods"))
                        propertySet.SetProperty("AllowedMosaicMethods", propDefaults.GetProperty("AllowedMosaicMethods"));//"Center,NorthWest,LockRaster,ByAttribute,Nadir,Viewpoint,Seamline"
                    if (propNames.Contains("AllowedItemMetadata"))
                        propertySet.SetProperty("AllowedItemMetadata", propDefaults.GetProperty("AllowedItemMetadata"));//"Full"
                    if (propNames.Contains("AllowedMensurationCapabilities"))
                        propertySet.SetProperty("AllowedMensurationCapabilities", propDefaults.GetProperty("AllowedMensurationCapabilities"));//"Full"
                    if (propNames.Contains("DefaultCompressionTolerance"))
                        propertySet.SetProperty("DefaultCompressionTolerance", propDefaults.GetProperty("DefaultCompressionTolerance"));//"0.01" LERC compression
                    //propertySet.SetProperty("RasterFunctions", @"\\server\dir\rft1.rft.xml,\\server\dir\rft2.rft.xml");//"put raster function templates here, the first one is applied to exportimage request by default"
                    //propertySet.SetProperty("RasterTypes", @"Raster Dataset,\\server\dir\art1.art.xml,\\server\dir\art2.art");//"put raster types here"
                    //propertySet.SetProperty("DynamicImageWorkspace", @"\\server\dynamicImageDir"); //put the workspace that holds uploaded imagery here
                    //propertySet.SetProperty("supportsOwnershipBasedAccessControl", true); //ownership based access control
                    //propertySet.SetProperty("AllowOthersToUpdate", true); //allow others to update a catalog item
                    //propertySet.SetProperty("AllowOthersToDelete", true); //allow others to delete a catalog item
                    //propertySet.SetProperty("DownloadDir", ""); //put the download directory here
                    //propertySet.SetProperty("VirutalDownloadDir", ""); //put the virtual download directory here
                }
                else
                {
                    propertySet.SetProperty("MaxImageHeight", 4100);
                    propertySet.SetProperty("MaxImageWidth", 15000);
                    propertySet.SetProperty("AllowedCompressions", "None,JPEG,LZ77");
                    propertySet.SetProperty("DefaultResamplingMethod", 0);
                    propertySet.SetProperty("DefaultCompressionQuality", 75);//for jpg compression
                    propertySet.SetProperty("DefaultCompressionTolerance", 0.01);//for LERC compression                 
                    //    rasterDataset = OpenRasterDataset(sourcePath);
                    IMensuration measure = new MensurationClass();
                    measure.Raster = ((IRasterDataset2)rasterDataset).CreateFullRaster();
                    string mensurationCaps = "";
                    if (measure.CanMeasure)
                        mensurationCaps = "Basic";
                    if (measure.CanMeasureHeightBaseToTop)
                        mensurationCaps += ",Base-Top Height";
                    if (measure.CanMeasureHeightBaseToTopShadow)
                        mensurationCaps += ",Base-Top Shadow Height";
                    if (measure.CanMeasureHeightTopToTopShadow)
                        mensurationCaps += ",Top-Top Shadow Height";
                    propertySet.SetProperty("AllowedMensurationCapabilities", mensurationCaps); //set mensuration here
                }

                //not cached
                propertySet.SetProperty("IsCached", false);
                propertySet.SetProperty("IgnoreCache", true);
                propertySet.SetProperty("UseLocalCacheDir", false);
                propertySet.SetProperty("ClientCachingAllowed", false);

                //propertySet.SetProperty("DEM", ""); //put the elevation raster dataset or service for 3D mensuration, may need to add 3D to AllowedMensurationCapabilities

                //convert colormap to RGB or not
                propertySet.SetProperty("ColormapToRGB", false);

                //whether to return jpgs for all jpgpng request or not
                propertySet.SetProperty("ReturnJPGPNGAsJPG", false);

                //allow server to process client defined function
                propertySet.SetProperty("AllowFunction", true); //allow raster function


                //capabilities
                if (sourceType == esriImageServiceSourceType.esriImageServiceSourceTypeMosaicDataset)
                    soConfig.Info.SetProperty("Capabilities", "Image,Catalog,Metadata,Mensuration");
                //Full set: Image,Catalog,Metadata,Download,Pixels,Edit,Mensuration
                else
                    soConfig.Info.SetProperty("Capabilities", "Image,Metadata,Mensuration");

                //enable wcs, assume data has spatial reference
                soConfig.set_ExtensionEnabled("WCSServer", true);
                IPropertySet wcsInfo = new PropertySetClass();
                wcsInfo.SetProperty("WebEnabled", "true");
                soConfig.set_ExtensionInfo("WCSServer", wcsInfo);
                IPropertySet propertySetWCS = new PropertySetClass();
                propertySetWCS.SetProperty("CustomGetCapabilities", false);
                propertySetWCS.SetProperty("PathToCustomGetCapabilitiesFiles", "");
                soConfig.set_ExtensionProperties("WCSServer", propertySetWCS);

                //enable wms
                soConfig.set_ExtensionEnabled("WMSServer", true);
                IPropertySet wmsInfo = new PropertySetClass();
                wmsInfo.SetProperty("WebEnabled", "true");
                soConfig.set_ExtensionInfo("WMSServer", wmsInfo);
                IPropertySet propertySetWMS = new PropertySetClass();
                propertySetWMS.SetProperty("name", "WMS"); //set other properties here
                soConfig.set_ExtensionProperties("WMSServer", propertySetWMS);


                //add configuration and start
                soAdmin.AddConfiguration(soConfig);
                soAdmin.StartConfiguration(serviceName, "ImageServer");

                if (soAdmin.GetConfigurationStatus(serviceName, "ImageServer").Status == esriConfigurationStatus.esriCSStarted)
                    Console.WriteLine("{0} on {1} has been configured and started.", serviceName, restAdmin);
                else
                    Console.WriteLine("{0} on {1} was configured but can not be started, please investigate.", serviceName, restAdmin);

                if (rasterDataset != null)
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(rasterDataset);
            }
            catch (Exception exc)
            {
                Console.WriteLine("Error: {0}", exc.Message);
            }
        }

        /// <summary>
        /// delete a service
        /// </summary>
        private static void DeleteService()
        {
            try
            {
                if (!ConnectAGS(restAdmin)) return;
                if (!ValidateServiceName(soAdmin, ref serviceName, restAdmin)) return;
                soAdmin.DeleteConfiguration(serviceName, "ImageServer");
                Console.WriteLine("{0} on {1} was deleted successfully.", serviceName, restAdmin);
            }
            catch (Exception exc)
            {
                Console.WriteLine("Error: {0}", exc.Message);
            }
        }

        /// <summary>
        /// start a service
        /// </summary>
        private static void StartService()
        {
            try
            {
                if (!ConnectAGS(restAdmin)) return;
                if (!ValidateServiceName(soAdmin, ref serviceName, restAdmin)) return;
                soAdmin.StartConfiguration(serviceName, "ImageServer");
                if (soAdmin.GetConfigurationStatus(serviceName, "ImageServer").Status == esriConfigurationStatus.esriCSStarted)
                    Console.WriteLine("{0} on {1} was started successfully.", serviceName, restAdmin);
                else
                    Console.WriteLine("{0} on {1} couldn't be started, please investigate.", serviceName, restAdmin);
            }
            catch (Exception exc)
            {
                Console.WriteLine("Error: {0}", exc.Message);
            }
        }

        /// <summary>
        /// stop a service
        /// </summary>
        private static void StopService()
        {
            try
            {
                if (!ConnectAGS(restAdmin)) return;
                if (!ValidateServiceName(soAdmin, ref serviceName, restAdmin)) return;
                soAdmin.StopConfiguration(serviceName, "ImageServer");
                if (soAdmin.GetConfigurationStatus(serviceName, "ImageServer").Status == esriConfigurationStatus.esriCSStopped)
                    Console.WriteLine("{0} on {1} was stopped successfully.", serviceName, restAdmin);
                else
                    Console.WriteLine("{0} on {1} couldn't be stopped, please investigate.", serviceName, restAdmin);
            }
            catch (Exception exc)
            {
                Console.WriteLine("Error: {0}", exc.Message);
            }
        }

        /// <summary>
        /// pause a service
        /// </summary>
        private static void PauseService()
        {
            try
            {
                if (!ConnectAGS(restAdmin)) return;
                if (!ValidateServiceName(soAdmin, ref serviceName, restAdmin)) return;
                if ((soAdmin.GetConfigurationStatus(serviceName, "ImageServer").Status == esriConfigurationStatus.esriCSStopped))
                {
                    Console.WriteLine("{0} on {1} is currently stopped --- not paused.", serviceName, restAdmin);
                    return;
                }
                soAdmin.PauseConfiguration(serviceName, "ImageServer");
                if (soAdmin.GetConfigurationStatus(serviceName, "ImageServer").Status == esriConfigurationStatus.esriCSPaused)
                    Console.WriteLine("{0} on {1} was paused successfully.", serviceName, restAdmin);
                else
                    Console.WriteLine("{0} on {1} couldn't be paused, please investigate.", serviceName, restAdmin);
            }
            catch (Exception exc)
            {
                Console.WriteLine("Error: {0}", exc.Message);
            }
        }

        /// <summary>
        /// List services
        /// </summary>
        private static void ListServices()
        {
            try
            {
                if (!ConnectAGS(restAdmin)) return;
                IEnumServerObjectConfiguration enumConfigs = soAdmin.GetConfigurations();
                enumConfigs.Reset();
                IServerObjectConfiguration soConfig = enumConfigs.Next();
                Console.WriteLine("ArcGIS Server {0} has the following image services:", restAdmin);
                while (soConfig != null)
                {
                    if (soConfig.TypeName == "ImageServer")
                        Console.WriteLine("{0}", soConfig.Name);
                    soConfig = enumConfigs.Next();
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine("Error: {0}", exc.Message);
            }
        }
        #endregion


        #region validation etc.
        /// <summary>
        /// connect to ags server
        /// </summary>
        /// <param name="host">host</param>
        /// <returns>true if connected</returns>
        private static bool ConnectAGS(string host)
        {
            try
            {
                IPropertySet propertySet = new PropertySetClass();
                propertySet.SetProperty("url", host);
                propertySet.SetProperty("ConnectionMode", esriAGSConnectionMode.esriAGSConnectionModePublisher);
                propertySet.SetProperty("ServerType", esriAGSServerType.esriAGSServerTypeDiscovery);
                propertySet.SetProperty("user", username);
                propertySet.SetProperty("password", password);
                propertySet.SetProperty("ALLOWINSECURETOKENURL", true);

                IAGSServerConnectionName3 connectName = new AGSServerConnectionNameClass() as IAGSServerConnectionName3;
                connectName.ConnectionProperties = propertySet;

                IAGSServerConnectionAdmin agsAdmin = ((IName)connectName).Open() as IAGSServerConnectionAdmin;
                soAdmin = agsAdmin.ServerObjectAdmin;
                return true;
            }
            catch (Exception exc)
            {
                Console.WriteLine("Error: Couldn't connect to AGSServer: {0}. Message: {1}", host, exc.Message);
                return false;
            }
        }

        /// <summary>
        /// Validate ServiceName
        /// </summary>
        /// <returns>Convert the config name to the correct case and returns true; if not exist in any cases, returns false </returns>
        private static bool ValidateServiceName(IServerObjectAdmin soAdmin, ref string serviceName, string host)
        {
            IEnumServerObjectConfiguration enumConfigs = soAdmin.GetConfigurations();
            enumConfigs.Reset();
            IServerObjectConfiguration soConfig = enumConfigs.Next();
            while (soConfig != null)
            {
                if (soConfig.Name.ToUpper() == serviceName.ToUpper())
                {
                    serviceName = soConfig.Name;
                    return true;
                }
                soConfig = enumConfigs.Next();
            }
            Console.WriteLine("Configuration {0} on {1} can not be found.", serviceName, host);
            return false;
        }

        /// <summary>
        /// Validate input parameters
        /// </summary>
        /// <param name="args">args</param>
        /// <returns>validation result</returns>
        private static bool ValidateParams(string[] args)
        {
            //at least two params
            if (args.Length == 0)
            {
                ShowUsage();
                Console.WriteLine("press any key to continue ...");
                Console.ReadKey();
                return false;
            }
            else if (args.Length < 2) // at least -o action
            {
                ShowUsage();
                return false;
            }

            // must start with -o
            string[] operations = new string[] { "publish", "delete", "start", "stop", "pause", "list" };
            if ((!args[0].StartsWith("-o")) || (!strInArray(args[1].ToLower(), operations)))
            {
                Console.WriteLine("Incorrect operation");
                ShowUsage();
                return false;
            }

            // for stop/start/pause/list, must contains "-n" and argument length is 4
            if ((args[1].ToLower() == "stop") || (args[1].ToLower() == "start") || (args[1].ToLower() == "pause") || (args[1].ToLower() == "delete"))
            {
                if (!strInArray("-h", args))
                {
                    Console.WriteLine("Missing host server -h");
                    return false;
                }
                if (!strInArray("-n", args))
                {
                    Console.WriteLine("Missing service name switch -n");
                    return false;
                }
                if (!strInArray("-u", args))
                {
                    Console.WriteLine("Missing admin/publisher username switch -u");
                    return false;
                }
                if (!strInArray("-p", args))
                {
                    Console.WriteLine("Missing admin/publisher name switch -p");
                    return false;
                }
                //if (args.Length > 8)
                //{
                //    Console.WriteLine("Too many arguments");
                //    return false;
                //}
            }
            // for publish, must contains "-d" "-n" and argument length is 6
            if (args[1].ToLower() == "publish")
            {
                if (!strInArray("-d", args))
                {
                    Console.WriteLine("Missing data source switch -d");
                    return false;
                }
                if (!strInArray("-n", args))
                {
                    Console.WriteLine("Missing service name switch -n");
                    return false;
                }
                if (!strInArray("-u", args))
                {
                    Console.WriteLine("Missing admin/publisher username switch -u");
                    return false;
                }
                if (!strInArray("-p", args))
                {
                    Console.WriteLine("Missing admin/publisher name switch -p");
                    return false;
                }
                //if (args.Length > 12)
                //{
                //    Console.WriteLine("Too many arguments");
                //    return false;
                //}
            }
            // validate each parameter: host, sourcepath, configname
            string[] parameters = new string[] { "-h", "-d", "-n", "-u", "-p" };
            for (int i = 2; i < args.Length; i++)
            {
                switch (args[i])
                {
                    case "-h":
                        if (i == args.Length - 1)
                        {
                            Console.WriteLine("Missing host parameter, switch -h");
                            return false;
                        }
                        else if (strInArray(args[i + 1], parameters))
                        {
                            Console.WriteLine("Missing host parameter, switch -h");
                            return false;
                        }
                        ++i;
                        break;
                    case "-d":
                        if (i == args.Length - 1)
                        {
                            Console.WriteLine("Missing data source parameter, switch -d");
                            return false;
                        }
                        else if (strInArray(args[i + 1], parameters))
                        {
                            Console.WriteLine("Missing data source parameter, switch -d");
                            return false;
                        }
                        ++i;
                        break;
                    case "-n":
                        if (i == args.Length - 1)
                        {
                            Console.WriteLine("Missing service name parameter, switch -n");
                            return false;
                        }
                        else if (strInArray(args[i + 1], parameters))
                        {
                            Console.WriteLine("Missing service name parameter, switch -n");
                            return false;
                        }
                        ++i;
                        break;
                    case "-u":
                        if (i == args.Length - 1)
                        {
                            Console.WriteLine("Missing admin/publisher username parameter, switch -u");
                            return false;
                        }
                        else if (strInArray(args[i + 1], parameters))
                        {
                            Console.WriteLine("Missing admin/publisher username parameter, switch -u");
                            return false;
                        }
                        ++i;
                        break;
                    case "-p":
                        if (i == args.Length - 1)
                        {
                            Console.WriteLine("Missing admin/publisher password parameter, switch -p");
                            return false;
                        }
                        else if (strInArray(args[i + 1], parameters))
                        {
                            Console.WriteLine("Missing admin/publisher password parameter, switch -p");
                            return false;
                        }
                        ++i;
                        break;
                    default:
                        Console.WriteLine("Incorrect parameter switch: {0} is not a recognized.", args[i]);
                        return false;
                }
            }
            return true;
        }

        /// <summary>
        /// string in array
        /// </summary>
        /// <param name="name"></param>
        /// <param name="nameArray"></param>
        /// <returns></returns>
        private static bool strInArray(string name, string[] nameArray)
        {
            for (int i = 0; i < nameArray.Length; i++)
            {
                if (nameArray[i] == name)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// initialize license
        /// </summary>
        /// <returns>status</returns>
        private static bool InitLicense()
        {
            RuntimeManager.Bind(ProductCode.Desktop);
            IAoInitialize aoInit = new AoInitializeClass();
            esriLicenseStatus status = aoInit.Initialize(esriLicenseProductCode.esriLicenseProductCodeBasic);
            if (status != esriLicenseStatus.esriLicenseCheckedOut)
            {
                Console.WriteLine("License initialization error");
                return false;
            }
            else
                return true;
        }
        #endregion


        #region helper methods
        /// <summary>
        /// Retrieve parameters
        /// </summary>
        /// <param name="args">args</param>
        private static void Retrieve_Params(string[] args)
        {
            for (int i = 2; i < args.Length; i++)
            {
                switch (args[i])
                {
                    case "-h":
                        restAdmin = args[++i];
                        break;
                    case "-d":
                        sourcePath = args[++i];
                        break;
                    case "-n":
                        serviceName = args[++i];
                        break;
                    case "-u":
                        username = args[++i];
                        break;
                    case "-p":
                        password = args[++i];
                        break;
                }
            }
        }

        /// <summary>
        /// Get Source Type
        /// </summary>
        /// <param name="sourcePath">path of the data source</param>
        /// <returns>data source type</returns>
        private static esriImageServiceSourceType GetSourceType(string sourcePath)
        {
            if (sourcePath.ToLower().EndsWith(".lyr"))
                return esriImageServiceSourceType.esriImageServiceSourceTypeLayer;
            else
            {
                FileInfo fileInfo = new FileInfo(sourcePath);
                OpenRasterDataset(fileInfo.DirectoryName, fileInfo.Name);
                if (rasterDataset is IMosaicDataset)
                    return esriImageServiceSourceType.esriImageServiceSourceTypeMosaicDataset;
                else
                    return esriImageServiceSourceType.esriImageServiceSourceTypeDataset;
            }
        }

        /// <summary>
        /// Open Raster Dataset
        /// </summary>
        /// <param name="path">path of the dataset</param>
        /// <param name="rasterDSName">name of the dataset</param>        
        private static void OpenRasterDataset(String path, String rasterDSName)
        {
            //this is why the utility user needs access to data source. image service configurations varies among data sources.
            IWorkspaceFactory workspaceFactory = null;
            IWorkspace workspace = null;
            IRasterWorkspaceEx rasterWorkspaceEx = null;
            Type factoryType = null;
            try
            {
                switch (path.Substring(path.Length - 4, 4).ToLower()) // a path can never be shorter than 4 characters, isn't it? c:\a
                {
                    case ".gdb":
                        factoryType = Type.GetTypeFromProgID("esriDataSourcesGDB.FileGDBWorkspaceFactory");
                        workspaceFactory = Activator.CreateInstance(factoryType) as IWorkspaceFactory;
                        workspace = workspaceFactory.OpenFromFile(path, 1);
                        rasterWorkspaceEx = (IRasterWorkspaceEx)workspace;
                        rasterDataset = rasterWorkspaceEx.OpenRasterDataset(rasterDSName);
                        break;
                    case ".sde":
                        factoryType = Type.GetTypeFromProgID("esriDataSourcesGDB.SdeWorkspaceFactory");
                        workspaceFactory = Activator.CreateInstance(factoryType) as IWorkspaceFactory;
                        workspace = workspaceFactory.OpenFromFile(path, 1);
                        rasterWorkspaceEx = (IRasterWorkspaceEx)workspace;
                        rasterDataset = rasterWorkspaceEx.OpenRasterDataset(rasterDSName);
                        break;
                    default:
                        factoryType = Type.GetTypeFromProgID("esriDataSourcesRaster.RasterWorkspaceFactory");
                        workspaceFactory = Activator.CreateInstance(factoryType) as IWorkspaceFactory;
                        workspace = workspaceFactory.OpenFromFile(path, 1);
                        IRasterWorkspace rasterWorkspace = (IRasterWorkspace)workspace;
                        rasterDataset = rasterWorkspace.OpenRasterDataset(rasterDSName);
                        break;
                }
            }
            catch (Exception)
            {
                throw new ArgumentException("Failed to open source data");
            }
        }

        /// <summary>
        /// Show usage
        /// </summary>
        private static void ShowUsage()
        {
            Console.WriteLine();
            Console.WriteLine("ArcObject Sample: command line image service publishing and configuration utility (10.2 ArcGIS Server). Data is not copied to server using this tool. source data must be accessible by ArcGIS Server running account. CachingScheme can't be defined through this tool but can be done through Caching geoprocessing tools. REST Metadata/thumbnail/iteminfo resource are not available through this app but can be developed using a similar approach to server admin endpoint.");
            Console.WriteLine();
            Console.WriteLine("Usage. <>: required parameter; |: pick one.");
            Console.WriteLine("isconfig -o publish -h <host_admin_url> -u <adminuser> -p <adminpassword> -d <datapath> -n <serviceName>");
            Console.WriteLine("isconfig -o <delete|start|stop> -h <host_admin_url> -u <adminuser> -p <adminpassword> -n <serviceName>");
            Console.WriteLine("isconfig -o <list> -h <host_admin_url> -u <adminuser> -p <adminpassword>");
            Console.WriteLine("e.g. isconfig -o list -h http://myserver:6080/arcgis/admin -u username -p password");
        }
        #endregion

        #region REST Admin based http requests
        /// <summary>
        /// Generate a token
        /// </summary>
        /// <param name="restAdmin">REST admin url: http://server:port/arcigs/admin</param>
        /// <returns>A token that has default expiration time</returns>
        public static string GenerateAGSToken_RESTAdmin()
        {
            try
            {
                string loginUrl = restAdmin + "/generateToken";
                WebRequest request = WebRequest.Create(loginUrl);
                request.Method = "POST";
                string credential = "username=" + username + "&password=" + password + "&client=requestip&expiration=&f=json";
                byte[] content = Encoding.UTF8.GetBytes(credential);
                request.ContentLength = content.Length;
                request.ContentType = "application/x-www-form-urlencoded";
                Stream requestStream = request.GetRequestStream();
                requestStream.Write(content, 0, content.Length);
                requestStream.Close();
                WebResponse response = request.GetResponse();
                Stream responseStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(responseStream);
                string result = reader.ReadToEnd();
                int index1 = result.IndexOf("token\":\"") + "token\":\"".Length;
                int index2 = result.IndexOf("\"", index1);
                //Dictionary<string, object> dictResult = DeserializeJSON(result, true);
                string token = result.Substring(index1, index2 - index1);
                return token;
            }
            catch { return ""; }
        }

        /// <summary>
        /// Create arcgis server folder
        /// </summary>
        /// <param name="restAdmin">REST admin url, e.g. http://server:port/arcgis/admin</param>
        /// <param name="folderName">Folder name</param>
        /// <param name="description">Description of the folder</param>
        /// <returns>True if successfully created</returns>
        private static bool CreateServerFolder_RESTAdmin(string folderName, string description)
        {
            try
            {
                string token = GenerateAGSToken_RESTAdmin();
                restAdmin = restAdmin.EndsWith("/") ? restAdmin.Substring(0, restAdmin.Length - 1) : restAdmin;
                string folderUrl = restAdmin + "/services/" + folderName + "?f=json&token=" + token;
                WebRequest request = WebRequest.Create(folderUrl);
                WebResponse response = request.GetResponse();
                Stream responseStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(responseStream);
                string result = reader.ReadToEnd();
                if (!result.Contains("error"))
                    return true;
                else
                {
                    string createFolderUrl = restAdmin + "/services/createFolder";
                    request = WebRequest.Create(createFolderUrl);
                    string postcontent = string.Format("folderName={0}&description={1}&f=pjson&token={2}", folderName, description, token);
                    Byte[] content = Encoding.UTF8.GetBytes(postcontent);
                    request.ContentLength = content.Length;
                    //((HttpWebRequest)request).UserAgent = "Mozilla/4.0";
                    request.ContentType = "application/x-www-form-urlencoded";
                    request.Method = "POST";
                    Stream requestStream = request.GetRequestStream();
                    requestStream.Write(content, 0, content.Length);
                    requestStream.Close();
                    response = request.GetResponse();
                    responseStream = response.GetResponseStream();
                    reader = new StreamReader(responseStream);
                    result = reader.ReadToEnd();
                    return result.Contains("success");
                }
            }
            catch { return false; }
        }
        private static void GetServerDirectory_RESTAdmin(string dirType, out string physicalPath, out string virtualPath)
        {
            physicalPath = "";
            virtualPath = "";
            try
            {
                string token = GenerateAGSToken_RESTAdmin();
                restAdmin = restAdmin.EndsWith("/") ? restAdmin.Substring(0, restAdmin.Length - 1) : restAdmin;
                string directoryAlias = dirType.ToString().ToLower().Replace("esrisdtype", "arcgis");
                string directoryUrl = restAdmin + "/system/directories/" + directoryAlias + "?f=json&token=" + token;
                WebRequest request = WebRequest.Create(directoryUrl);
                WebResponse response = request.GetResponse();
                Stream responseStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(responseStream);
                string result = reader.ReadToEnd();
                try
                {
                    int index = result.IndexOf(dirType);
                    int index1 = result.IndexOf("physicalPath\":\"", index) + "physicalPath\":\"".Length;
                    int index2 = result.IndexOf("\"", index1);
                    physicalPath = result.Substring(index1, index2 - index1);

                    index1 = result.IndexOf("virtualPath\":\"", index) + "virtualPath\":\"".Length;
                    index2 = result.IndexOf("\"", index1);
                    virtualPath = result.Substring(index1, index2 - index1);
                }
                catch { }
            }
            catch { }
        }
        /// <summary>
        /// Delete Service
        /// </summary>
        /// <param name="restAdmin">REST admin url, e.g. http://server:port/arcgis/admin</param>
        /// <param name="serviceName">Service Name</param>
        /// <param name="serviceType">Server Type, e.g. ImageServer, MapServer, GeoDataServer, GeoprocessingServer, GeometryServer, etc</param>
        /// <returns>True if successfully deleted</returns>
        public static bool DeleteService_RESTAdmin()
        {
            try
            {
                string token = GenerateAGSToken_RESTAdmin();
                restAdmin = restAdmin.EndsWith("/") ? restAdmin.Substring(0, restAdmin.Length - 1) : restAdmin;
                string serviceUrl = restAdmin + "/services/" + serviceName + "." + "ImageServer" + "/delete";
                WebRequest request = WebRequest.Create(serviceUrl);
                string postcontent = "f=pjson&token=" + token;
                Byte[] content = Encoding.UTF8.GetBytes(postcontent);
                request.ContentLength = content.Length;
                //((HttpWebRequest)request).UserAgent = "Mozilla/4.0";
                request.ContentType = "application/x-www-form-urlencoded";
                request.Method = "POST";
                Stream requestStream = request.GetRequestStream();
                requestStream.Write(content, 0, content.Length);
                requestStream.Close();
                WebResponse response = request.GetResponse();
                Stream responseStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(responseStream);
                string result = reader.ReadToEnd();
                Console.WriteLine("delete service {0}, result: {1}", serviceName, result);
                return result.Contains("success");
            }
            catch { return false; }
        }

        public static bool StartService_RESTAdmin()
        {
            try
            {
                string token = GenerateAGSToken_RESTAdmin();
                restAdmin = restAdmin.EndsWith("/") ? restAdmin.Substring(0, restAdmin.Length - 1) : restAdmin;
                string serviceUrl = restAdmin + "/services/" + serviceName + "." + "ImageServer" + "/start";
                WebRequest request = WebRequest.Create(serviceUrl);
                string postcontent = "f=pjson&token=" + token;
                Byte[] content = Encoding.UTF8.GetBytes(postcontent);
                request.ContentLength = content.Length;
                //((HttpWebRequest)request).UserAgent = "Mozilla/4.0";
                request.ContentType = "application/x-www-form-urlencoded";
                request.Method = "POST";
                Stream requestStream = request.GetRequestStream();
                requestStream.Write(content, 0, content.Length);
                requestStream.Close();
                WebResponse response = request.GetResponse();
                Stream responseStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(responseStream);
                string result = reader.ReadToEnd();
                Console.WriteLine("start service {0}, result: {1}", serviceName, result);
                return result.Contains("success");
            }
            catch { return false; }
        }

        public static bool StopService_RESTAdmin()
        {
            try
            {
                string token = GenerateAGSToken_RESTAdmin();
                restAdmin = restAdmin.EndsWith("/") ? restAdmin.Substring(0, restAdmin.Length - 1) : restAdmin;
                string serviceUrl = restAdmin + "/services/" + serviceName + "." + "ImageServer" + "/stop";
                WebRequest request = WebRequest.Create(serviceUrl);
                string postcontent = "f=pjson&token=" + token;
                Byte[] content = Encoding.UTF8.GetBytes(postcontent);
                request.ContentLength = content.Length;
                //((HttpWebRequest)request).UserAgent = "Mozilla/4.0";
                request.ContentType = "application/x-www-form-urlencoded";
                request.Method = "POST";
                Stream requestStream = request.GetRequestStream();
                requestStream.Write(content, 0, content.Length);
                requestStream.Close();
                WebResponse response = request.GetResponse();
                Stream responseStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(responseStream);
                string result = reader.ReadToEnd();
                Console.WriteLine("stop service {0}, result: {1}", serviceName, result);
                return result.Contains("success");
            }
            catch { return false; }
        }
        public static void ListServices_RESTAdmin()
        {
            Console.WriteLine("List of image services: ");
            ListServices_RESTAdmin(restAdmin + "/services", "");
        }
        public static void ListServices_RESTAdmin(string root, string folder)
        {
            try
            {
                string token = GenerateAGSToken_RESTAdmin();
                restAdmin = restAdmin.EndsWith("/") ? restAdmin.Substring(0, restAdmin.Length - 1) : restAdmin;
                string serviceUrl = root + "/" + folder;
                WebRequest request = WebRequest.Create(serviceUrl);
                string postcontent = "f=json&token=" + token;
                Byte[] content = Encoding.UTF8.GetBytes(postcontent);
                request.ContentLength = content.Length;
                //((HttpWebRequest)request).UserAgent = "Mozilla/4.0";
                request.ContentType = "application/x-www-form-urlencoded";
                request.Method = "POST";
                Stream requestStream = request.GetRequestStream();
                requestStream.Write(content, 0, content.Length);
                requestStream.Close();
                WebResponse response = request.GetResponse();
                Stream responseStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(responseStream);
                string result = reader.ReadToEnd();
                int indexfolder1 = result.IndexOf("folders\":[");
                if (indexfolder1 != -1)
                {
                    indexfolder1 += "folders\":[".Length;
                    int indexfolder2 = result.IndexOf("]", indexfolder1);
                    string folderlist = result.Substring(indexfolder1, indexfolder2 - indexfolder1);
                    string[] folders = folderlist.Replace("\"", "").Split(',');
                    foreach (string subfolder in folders)
                        ListServices_RESTAdmin(serviceUrl, subfolder);
                }


                int index = result.IndexOf("services");
                while (index > 0)
                {
                    try
                    {
                        int index1 = result.IndexOf("folderName\":\"", index);
                        if (index1 == -1)
                            break;
                        index1 += "folderName\":\"".Length;
                        int index2 = result.IndexOf("\"", index1);
                        string folderName = result.Substring(index1, index2 - index1);

                        index1 = result.IndexOf("serviceName\":\"", index2) + "serviceName\":\"".Length;
                        index2 = result.IndexOf("\"", index1);
                        string serviceName = result.Substring(index1, index2 - index1);

                        index1 = result.IndexOf("type\":\"", index2) + "type\":\"".Length;
                        index2 = result.IndexOf("\"", index1);
                        string serviceType = result.Substring(index1, index2 - index1);
                        if (serviceType == "ImageServer")
                        {
                            if (folderName == "/") //root
                                Console.WriteLine(serviceName);
                            else
                                Console.WriteLine(folderName + "/" + serviceName);
                        }
                        index = index2;
                    }
                    catch { }
                }
            }
            catch { }
        }
        /// <summary>
        /// create image service
        /// </summary>
        /// <param name="restAdmin">host machine name (windows or linux)</param>
        /// <param name="sourcePath">data source path, must be windows path (linux path is constructed automaticlly by windows path)</param>
        /// <param name="serviceName">configuration name</param>
        /// <param name="createImageServiceParams">Cration parameters, e.g. raster functions, colormaptorgb, raster types, dem, dynamicimageworkspace, customized propertyset etc</param>
        /// <returns>true if created successfully</returns>
        public static bool CreateISConfig_RESTAdmin()
        {
            //string restAdmin, string username, string password, string sourcePath, string serviceName
            try
            {
                esriImageServiceSourceType sourceType = GetSourceType(sourcePath);

                string serviceType = "ImageServer";
                //DeleteService_RESTAdmin();
                restAdmin = restAdmin.EndsWith("/") ? restAdmin.Substring(0, restAdmin.Length - 1) : restAdmin;
                string serviceFolder = "";
                if (serviceName.Contains("/"))
                {
                    serviceFolder = serviceName.Substring(0, serviceName.IndexOf("/"));
                    CreateServerFolder_RESTAdmin(serviceFolder, "");
                    serviceName = serviceName.Substring(serviceFolder.Length + 1, serviceName.Length - serviceFolder.Length - 1);
                }
                string createServiceUrl = "";
                if (serviceFolder == "")
                    createServiceUrl = restAdmin + "/services/createService";
                else
                    createServiceUrl = restAdmin + "/services/" + serviceFolder + "/createService";
                //createServiceUrl = "http://wenxue:6080/arcgis/admin/services/createService";
                WebRequest request = WebRequest.Create(createServiceUrl);

                //DataSourceIsReadOnly                                                                                                                                                                                                                                                                                                                                                                                                            
                StringBuilder sBuilder = new StringBuilder();
                sBuilder.Append("{");
                sBuilder.AppendFormat("{0}: {1},", QuoteString("serviceName"), QuoteString(serviceName));
                sBuilder.AppendFormat("{0}: {1},", QuoteString("type"), QuoteString(serviceType));
                sBuilder.AppendFormat("{0}: {1},", QuoteString("description"), QuoteString(""));

                sBuilder.AppendFormat("{0}: {1},", QuoteString("clusterName"), QuoteString("default"));
                sBuilder.AppendFormat("{0}: {1},", QuoteString("minInstancesPerNode"), 1);
                sBuilder.AppendFormat("{0}: {1},", QuoteString("maxInstancesPerNode"), 2);
                sBuilder.AppendFormat("{0}: {1},", QuoteString("maxWaitTime"), 10000);
                sBuilder.AppendFormat("{0}: {1},", QuoteString("maxIdleTime"), 1800);
                sBuilder.AppendFormat("{0}: {1},", QuoteString("maxUsageTime"), 600);
                sBuilder.AppendFormat("{0}: {1},", QuoteString("loadBalancing"), QuoteString("ROUND_ROBIN"));
                sBuilder.AppendFormat("{0}: {1},", QuoteString("isolationLevel"), QuoteString("HIGH"));
                sBuilder.AppendFormat("{0}: {1},", QuoteString("configuredState"), QuoteString("STARTED"));

                string webCapabilities = "";

                if (sourceType == esriImageServiceSourceType.esriImageServiceSourceTypeMosaicDataset)
                    webCapabilities = "Image,Catalog,Metadata,Mensuration";//full list "Image,Catalog,Metadata,Download,Pixels,Edit,Mensuration"
                else
                    webCapabilities = "Image,Metadata,Mensuration";
                sBuilder.AppendFormat("{0}: {1},", QuoteString("capabilities"), QuoteString(webCapabilities));


                sBuilder.AppendFormat("{0}: {1}", QuoteString("properties"), "{");
                sBuilder.AppendFormat("{0}: {1},", QuoteString("supportedImageReturnTypes"), QuoteString("MIME+URL"));

                IWorkspace workspace = ((IDataset)rasterDataset).Workspace;
                if (workspace.WorkspaceFactory.WorkspaceType == esriWorkspaceType.esriRemoteDatabaseWorkspace)
                {
                    IWorkspaceName2 wsName2 = ((IDataset)workspace).FullName as IWorkspaceName2;
                    string connString = wsName2.ConnectionString;
                    sBuilder.AppendFormat("{0}: {1},", QuoteString("connectionString"), QuoteString(connString));
                    sBuilder.AppendFormat("{0}: {1},", QuoteString("raster"), QuoteString(((IDataset)rasterDataset).Name));
                }
                else
                    sBuilder.AppendFormat("{0}: {1},", QuoteString("path"), QuoteString(sourcePath.Replace("\\", "\\\\")));

                sBuilder.AppendFormat("{0}: {1},", QuoteString("esriImageServiceSourceType"), QuoteString(sourceType.ToString()));

                string outputDir = "";
                string virtualDir = "";

                GetServerDirectory_RESTAdmin("arcgisoutput", out outputDir, out virtualDir);

                string cacheDir = "";
                string virtualCacheDir = "";
                GetServerDirectory_RESTAdmin("arcgisoutput", out cacheDir, out virtualCacheDir);


                if (outputDir != "")
                {
                    sBuilder.AppendFormat("{0}: {1},", QuoteString("outputDir"), QuoteString(outputDir));
                    sBuilder.AppendFormat("{0}: {1},", QuoteString("virtualOutputDir"), QuoteString(virtualDir));//http://istest2:6080/arcgis/server/arcgisoutput"));
                    //sBuilder.AppendFormat("{0}: {1},", QuoteString("dynamicImageWorkspace"), QuoteString(@"D:\UploadDir"));                    
                }
                //if (cacheDir != "")
                //{
                //    sBuilder.AppendFormat("{0}: {1},", QuoteString("cacheDir"), QuoteString(cacheDir));
                //    sBuilder.AppendFormat("{0}: {1},", QuoteString("virtualCacheDir"), QuoteString(virtualCacheDir));//http://istest2:6080/arcgis/server/arcgisoutput"));                    
                //}
                sBuilder.AppendFormat("{0}: {1},", QuoteString("copyright"), QuoteString(""));

                //properties for a mosaic Dataset;
                if (sourceType == esriImageServiceSourceType.esriImageServiceSourceTypeMosaicDataset)
                {
                    IFunctionRasterDataset functionRasterDataset = (IFunctionRasterDataset)rasterDataset;
                    IPropertySet propDefaults = functionRasterDataset.Properties;
                    object names, values;
                    propDefaults.GetAllProperties(out names, out values);
                    List<string> propNames = new List<string>();
                    propNames.AddRange((string[])names);
                    if (propNames.Contains("AllowedCompressions"))
                        sBuilder.AppendFormat("{0}: {1},", QuoteString("allowedCompressions"), QuoteString(propDefaults.GetProperty("AllowedCompressions").ToString()));//string
                    if (propNames.Contains("MaxImageHeight"))
                        sBuilder.AppendFormat("{0}: {1},", QuoteString("maxImageHeight"), QuoteString(propDefaults.GetProperty("MaxImageHeight").ToString()));//should be int     
                    if (propNames.Contains("MaxImageWidth"))
                        sBuilder.AppendFormat("{0}: {1},", QuoteString("maxImageWidth"), QuoteString(propDefaults.GetProperty("MaxImageWidth").ToString()));//should be int
                    if (propNames.Contains("DefaultResamplingMethod"))
                        sBuilder.AppendFormat("{0}: {1},", QuoteString("defaultResamplingMethod"), QuoteString(propDefaults.GetProperty("DefaultResamplingMethod").ToString()));//should be int
                    if (propNames.Contains("DefaultCompressionQuality"))
                        sBuilder.AppendFormat("{0}: {1},", QuoteString("defaultCompressionQuality"), QuoteString(propDefaults.GetProperty("DefaultCompressionQuality").ToString()));//should be int
                    if (propNames.Contains("MaxRecordCount"))
                        sBuilder.AppendFormat("{0}: {1},", QuoteString("maxRecordCount"), QuoteString(propDefaults.GetProperty("MaxRecordCount").ToString()));//should be int
                    if (propNames.Contains("MaxMosaicImageCount"))
                        sBuilder.AppendFormat("{0}: {1},", QuoteString("maxMosaicImageCount"), QuoteString(propDefaults.GetProperty("MaxMosaicImageCount").ToString()));//should be int
                    if (propNames.Contains("MaxDownloadImageCount"))
                        sBuilder.AppendFormat("{0}: {1},", QuoteString("maxDownloadImageCount"), QuoteString(propDefaults.GetProperty("MaxDownloadImageCount").ToString()));//should be int
                    if (propNames.Contains("MaxDownloadSizeLimit"))
                        sBuilder.AppendFormat("{0}: {1},", QuoteString("MaxDownloadSizeLimit"), QuoteString(propDefaults.GetProperty("MaxDownloadSizeLimit").ToString()));//should be int
                    if (propNames.Contains("AllowedFields"))
                        sBuilder.AppendFormat("{0}: {1},", QuoteString("allowedFields"), QuoteString(propDefaults.GetProperty("AllowedFields").ToString()));//string
                    if (propNames.Contains("AllowedMosaicMethods"))
                        sBuilder.AppendFormat("{0}: {1},", QuoteString("allowedMosaicMethods"), QuoteString(propDefaults.GetProperty("AllowedMosaicMethods").ToString()));//string
                    if (propNames.Contains("AllowedItemMetadata"))
                        sBuilder.AppendFormat("{0}: {1},", QuoteString("allowedItemMetadata"), QuoteString(propDefaults.GetProperty("AllowedItemMetadata").ToString()));//string
                    if (propNames.Contains("AllowedMensurationCapabilities"))
                        sBuilder.AppendFormat("{0}: {1},", QuoteString("AllowedMensurationCapabilities"), QuoteString(propDefaults.GetProperty("AllowedMensurationCapabilities").ToString()));//string
                    if (propNames.Contains("DefaultCompressionTolerance"))
                        sBuilder.AppendFormat("{0}: {1},", QuoteString("defaultCompressionTolerance"), QuoteString(propDefaults.GetProperty("DefaultCompressionTolerance").ToString()));//string                    
                    //sBuilder.AppendFormat("{0}: {1},", QuoteString("downloadDir"), QuoteString(@"c:\temp"));//string
                    //sBuilder.AppendFormat("{0}: {1},", QuoteString("virutalDownloadDir"), QuoteString(@"http://localhost/temp");//string
                }
                else if (sourceType != esriImageServiceSourceType.esriImageServiceSourceTypeCatalog) //not iscdef
                {
                    sBuilder.AppendFormat("{0}: {1},", QuoteString("allowedCompressions"), QuoteString("None,JPEG,LZ77"));
                    sBuilder.AppendFormat("{0}: {1},", QuoteString("maxImageHeight"), QuoteString("4100"));//should be int     
                    sBuilder.AppendFormat("{0}: {1},", QuoteString("maxImageWidth"), QuoteString("15000"));//should be int
                    sBuilder.AppendFormat("{0}: {1},", QuoteString("defaultResamplingMethod"), QuoteString("0"));//should be int
                    sBuilder.AppendFormat("{0}: {1},", QuoteString("defaultCompressionQuality"), QuoteString("75"));//should be int
                    sBuilder.AppendFormat("{0}: {1},", QuoteString("defaultCompressionTolerance"), QuoteString("0.01"));//should be int
                    IMensuration measure = new MensurationClass();
                    measure.Raster = ((IRasterDataset2)rasterDataset).CreateFullRaster();
                    string mensurationCaps = "";
                    if (measure.CanMeasure)
                        mensurationCaps = "Basic";
                    if (measure.CanMeasureHeightBaseToTop)
                        mensurationCaps += ",Base-Top Height";
                    if (measure.CanMeasureHeightBaseToTopShadow)
                        mensurationCaps += ",Base-Top Shadow Height";
                    if (measure.CanMeasureHeightTopToTopShadow)
                        mensurationCaps += ",Top-Top Shadow Height";
                    sBuilder.AppendFormat("{0}: {1},", QuoteString("AllowedMensurationCapabilities"), QuoteString(mensurationCaps));//string
                }

                //sBuilder.AppendFormat("{0}: {1},", QuoteString("dEM"), QuoteString(@"c:\elevation\elevation.tif"));                
                //sBuilder.AppendFormat("{0}: {1},", QuoteString("supportsOwnershipBasedAccessControl"), QuoteString("true"));
                //sBuilder.AppendFormat("{0}: {1},", QuoteString("allowOthersToUpdate"), QuoteString("true"));               
                //sBuilder.AppendFormat("{0}: {1},", QuoteString("allowOthersToDelete"), QuoteString("true"));
                //sBuilder.AppendFormat("{0}: {1},", QuoteString("cacheOnDemand"), QuoteString("false"));
                //sBuilder.AppendFormat("{0}: {1},", QuoteString("isCached"), QuoteString("false"));
                //sBuilder.AppendFormat("{0}: {1},", QuoteString("ignoreCache"), QuoteString("true"));
                //sBuilder.AppendFormat("{0}: {1},", QuoteString("useLocalCacheDir"), QuoteString("false"));
                //sBuilder.AppendFormat("{0}: {1},", QuoteString("clientCachingAllowed"), QuoteString("false"));


                sBuilder.AppendFormat("{0}: {1},", QuoteString("colormapToRGB"), QuoteString("false"));
                sBuilder.AppendFormat("{0}: {1},", QuoteString("returnJPGPNGAsJPG"), QuoteString("false"));
                sBuilder.AppendFormat("{0}: {1},", QuoteString("allowFunction"), QuoteString("true"));
                string rasterFunctions = "";
                sBuilder.AppendFormat("{0}: {1},", QuoteString("rasterFunctions"), QuoteString(rasterFunctions).Replace("\\", "\\\\"));
                string rasterTypes = "";
                sBuilder.AppendFormat("{0}: {1}", QuoteString("rasterTypes"), QuoteString(rasterTypes).Replace("\\", "\\\\"));

                sBuilder.Append("},");
                bool enableWCS = true;
                bool enableWMS = true;
                sBuilder.AppendFormat("{0}: {1}", QuoteString("extensions"), "[{\"typeName\":\"WCSServer\",\"enabled\":\"" + enableWCS + "\",\"capabilities\":null,\"properties\":{}},{\"typeName\":\"WMSServer\",\"enabled\":\"" + enableWMS + "\",\"capabilities\":null,\"properties\":{\"title\":\"WMS\",\"name\":\"WMS\",\"inheritLayerNames\":\"false\"}}");
                sBuilder.Append("],");
                sBuilder.AppendFormat("{0}: {1}", QuoteString("datasets"), "[]");
                sBuilder.Append("}");
                string postcontent = HttpUtility.UrlEncode(sBuilder.ToString());
                string token = GenerateAGSToken_RESTAdmin();
                postcontent = "service=" + postcontent + "&startAfterCreate=on&f=pjson&token=" + token;
                Byte[] content = Encoding.UTF8.GetBytes(postcontent);
                request.ContentLength = content.Length;
                request.ContentType = "application/x-www-form-urlencoded";
                request.Method = "POST";
                Stream requestStream = request.GetRequestStream();
                requestStream.Write(content, 0, content.Length);
                requestStream.Close();
                WebResponse response = request.GetResponse();
                Stream responseStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(responseStream);
                string result = reader.ReadToEnd();
                Console.WriteLine("create service:" + serviceName + " result:" + result);
                //wait for 5 seconds to reduce latency issue
                //System.Threading.Thread.Sleep(5000);
                return result.Contains("success");
            }
            catch (Exception exc) { Console.WriteLine(exc.Message); return false; }
        }
        private static string QuoteString(string input)
        {
            return "\"" + input + "\"";
        }
        private static string DeQuoteString(string input)
        {
            if (input.StartsWith("\""))
                return input.Substring(1, input.Length - 2).Trim();
            else
                return input;
        }

        #endregion

    }
}
