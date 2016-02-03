using System;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.DataManagementTools;
using ESRI.ArcGIS.Geoprocessor;
using Microsoft.Win32;

namespace CreateRasterDatasets
{
    class CreateRasterDataset_gp
    {
        //Set variables, you can substitute the paths with your data location
        //Remove temp.gdb in TEMP directory if it exists
        //The output is written to TEMP directory in temp.gdb file geodatabase

        static string outputFolder = @"C:\Temp";
        static string outFGDB ="temp.gdb";
        static string FGDBPath = outputFolder + "\\" + outFGDB;
        static string rasterFolder = @"C:\data";
        static string dsName = "mosaic";

        static void Main(string[] args)
        {
            //If creating a raster dataset in ArcSDE, it will need Standard or Advanced License
            ESRI.ArcGIS.esriSystem.AoInitialize aoInit = null;
            try
            {
                ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.Desktop);
                aoInit = new AoInitializeClass();
                esriLicenseStatus licStatus = aoInit.Initialize(esriLicenseProductCode.esriLicenseProductCodeAdvanced);
                Console.WriteLine("License Checkout successful.");
            }
            catch (Exception exc)
            {
                // If it fails at this point, shutdown the test and ignore any subsequent errors.
                Console.WriteLine(exc.Message);
            }

            try
            {
                //Creates an empty raster dataset
                //Make sure parameters of the empty raster dataset match our data (number of bands, bit depth, etc.)
                CreateRasterDS();

                //Loads rasters in the input folder to the new raster dataset
                LoadDirToRasterDataset(FGDBPath + "\\" + dsName, rasterFolder);
            }
            catch (Exception exc)
            {
                // If it fails at this point, shutdown the test and ignore any subsequent errors.
                Console.WriteLine(exc.Message);
            }

            Console.Write("Please press any key to close the application.");
            Console.ReadKey();

            //Do not make any call to ArcObjects after ShutDown()
            aoInit.Shutdown();
        }

        //Creates raster dataset using GP CreateRasterDataset class
        static void CreateRasterDS() 
        {
            try
            {
                //Initialize GeoProcessor
                Geoprocessor geoProcessor = new Geoprocessor();

                //Create file geodatabase 
                CreateFileGDB createFileGDB = new CreateFileGDB();
                createFileGDB.out_folder_path = outputFolder;
                createFileGDB.out_name = outFGDB;

                geoProcessor.Execute(createFileGDB, null);

                //Create a Raster Dataset 
                CreateRasterDataset createRasterDataset = new CreateRasterDataset();

                //Set parameters
                //Set output location and name
                createRasterDataset.out_name = dsName;
                createRasterDataset.out_path = FGDBPath;

                //Set number of band to 3
                createRasterDataset.number_of_bands = 3;

                //Set pixel type to unsigned 8 bit integer
                createRasterDataset.pixel_type = "8_BIT_UNSIGNED";

                //Build pyramid layers with GDB calculated number of levels
                createRasterDataset.pyramids = "PYRAMIDS -1 BILINEAR";

                //Set GDB dataset properties
                //Set JPEG compression of quality 50
                createRasterDataset.compression = "JPEG 50";

                //Set pyramid origin point so it takes advantage of partial pyramid building when mosaicking
                //Need to make sure that any raster that will be mosaicked is to the southeast of this point
                //If the rasters are in GCS, the following origin point is good.
                //createRasterDataset.pyramid_origin = "-180 90";

                //Execute the tool to create a raster dataset
                geoProcessor.Execute(createRasterDataset, null);
                ReturnMessages(geoProcessor);
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
            }
        }

        //GP message handling
        private static void ReturnMessages(Geoprocessor gp)
        {
            if (gp.MessageCount > 0)
            {
                for (int Count = 0; Count <= gp.MessageCount - 1; Count++)
                {
                    System.Console.WriteLine(gp.GetMessage(Count));
                }
            }
        }

        static void LoadDirToRasterDataset(string outRasterDataset, string inputDir)
        {
            try
            {
                //Initialize GeoProcessor
                Geoprocessor geoProcessor = new Geoprocessor();

                //Mosaic the works
                WorkspaceToRasterDataset wsToRasDs = new WorkspaceToRasterDataset();

                //Set input folder
                wsToRasDs.in_workspace = inputDir;

                //Set target GDB raster dataset
                wsToRasDs.in_raster_dataset = outRasterDataset;

                //Include rasters in the subdirectories
                wsToRasDs.include_subdirectories = "INCLUDE_SUBDIRECTORIES";

                //Set mosaic mode
                wsToRasDs.mosaic_type = "LAST";

                //Set colormap mode
                wsToRasDs.colormap = "MATCH";

                //Set background value
                wsToRasDs.background_value = 0;

                //Execute the tool to load rasters in the directory to raster dataset
                geoProcessor.Execute(wsToRasDs, null);
                ReturnMessages(geoProcessor);
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
            }
        }

    }
}
