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
using Microsoft.Win32;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataManagementTools;
using ESRI.ArcGIS.Geoprocessor;
using ESRI.ArcGIS.Geoprocessing;
using ESRI.ArcGIS.DataSourcesGDB;

namespace CreateFileRasterMosaic
{
    //Sample creating a file raster mosaic from rasters in a folder and its subfolders
    //Steps:
    //  1. Create an unmanaged PGDB raster catalog
    //  2. Load rasters in the input folder and its subfolders to the new raster catalog
    //  3. Create a mosaic file raster dataset from the unmanaged raster catalog

    class CreateFileRasterMosaic
    {
        //Local variables for data path
        //The TEMP directory will be used to create temporary raster catalog and output raster dataset
        //Remove temp.mdb in TEMP directory if it exists
        //You can substitute the paths with your data location

        static string inputFolder = @"C:\data";
        static string outputFolder = @"C:\Temp";
        static string outputName = "mosaic.tif";
        static string tempRasterCatalog = "temp_rc";
        static string tempPGDB = "temp.mdb";
        static string tempPGDBPath = outputFolder + "\\" + tempPGDB;
        static string tempRasterCatalogPath = tempPGDBPath + "\\" + tempRasterCatalog;

        static void Main(string[] args)
        {
            ESRI.ArcGIS.esriSystem.AoInitialize aoInit = null;
            try
            {
                ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.Desktop);
                aoInit = new AoInitializeClass();
                esriLicenseStatus licStatus = aoInit.Initialize(esriLicenseProductCode.esriLicenseProductCodeBasic);
                Console.WriteLine("License Checkout successful.");
            }
            catch (Exception exc)
            {
                // If it fails at this point, shutdown the test and ignore any subsequent errors.
                Console.WriteLine(exc.Message);
            }

            try
            {
                //Create temporary unmanaged raster catalog and load all rasters
                CreateUnmanagedRasterCatalog();

                //Open raster catalog
                IRasterWorkspaceEx rasterWorkspaceEx = (IRasterWorkspaceEx)OpenRasterPGDBWorkspace(tempPGDBPath);
                IRasterCatalog rasterCatalog = rasterWorkspaceEx.OpenRasterCatalog(tempRasterCatalog);

                //Mosaic rasters in the raster catalog
                Mosaic(rasterCatalog);
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
            }

            Console.Write("Please press any key to close the application.");
            Console.ReadKey();

            //Do not make any call to ArcObjects after ShutDown() call
            aoInit.Shutdown();
        }

        static void CreateUnmanagedRasterCatalog()
        {
            try
            {
                //Use geoprocessing to create the geodatabase, the raster catalog, and load our directory
                //to the raster catalog.
                Geoprocessor geoprocessor = new Geoprocessor();

                //Create personal GDB in the TEMP directory
                CreatePersonalGDB createPersonalGDB = new CreatePersonalGDB();
                createPersonalGDB.out_folder_path = outputFolder;
                createPersonalGDB.out_name = tempPGDB;

                geoprocessor.Execute(createPersonalGDB, null);

                //Create an unmanaged raster catalog in the newly created personal GDB
                CreateRasterCatalog createRasterCatalog = new CreateRasterCatalog();

                createRasterCatalog.out_path = tempPGDBPath;
                createRasterCatalog.out_name = tempRasterCatalog;
                createRasterCatalog.raster_management_type = "unmanaged";

                geoprocessor.Execute(createRasterCatalog, null);

                //Load data into the unmanaged raster catalog
                WorkspaceToRasterCatalog wsToRasterCatalog = new WorkspaceToRasterCatalog();

                wsToRasterCatalog.in_raster_catalog = tempRasterCatalogPath;
                wsToRasterCatalog.in_workspace = inputFolder;
                wsToRasterCatalog.include_subdirectories = "INCLUDE_SUBDIRECTORIES";

                geoprocessor.Execute(wsToRasterCatalog, null);
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
            }
        }

        static void Mosaic(IRasterCatalog rasterCatalog)
        {
            try
            {
                //Mosaics all rasters in the raster catalog to an output raster dataset
                IMosaicRaster mosaicRaster = new MosaicRasterClass();
                mosaicRaster.RasterCatalog = rasterCatalog;

                //Set mosaicking options, you may not need to set these for your data
                mosaicRaster.MosaicColormapMode = rstMosaicColormapMode.MM_MATCH;
                mosaicRaster.MosaicOperatorType = rstMosaicOperatorType.MT_LAST;

                //Open output workspace
                IWorkspaceFactory workspaceFactory = new RasterWorkspaceFactoryClass();
                IWorkspace workspace = workspaceFactory.OpenFromFile(outputFolder, 0);

                //Save out to a target raster dataset
                //It can be saved to TIFF, IMG, GRID, BMP, GIF, JPEG2000, JPEG, Geodatabase, ect.
                ISaveAs saveas = (ISaveAs)mosaicRaster;
                saveas.SaveAs(outputName, workspace, "TIFF");
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
            }
        }

        static IWorkspace OpenRasterPGDBWorkspace(string connStr)
        {
            Type t = Type.GetTypeFromProgID("esriDataSourcesGDB.AccessWorkspaceFactory");
            System.Object obj = Activator.CreateInstance(t);
            IWorkspaceFactory2 workspaceFactory = obj as IWorkspaceFactory2;
            return workspaceFactory.OpenFromFile(connStr, 0);
        }
    }
}
