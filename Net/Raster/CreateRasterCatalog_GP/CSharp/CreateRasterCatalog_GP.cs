/*

   Copyright 2019 Esri

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
using System.Collections.Generic;
using System.Text;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geoprocessing;
using ESRI.ArcGIS.DataManagementTools;
using ESRI.ArcGIS.Geoprocessor;
using ESRI.ArcGIS.esriSystem;


namespace CreateRasterCatalogs
{
    class CreateRasterCatalogApp
    {       
        //Set variables
        static string sdePath = @"Database Connections\Connection to tiny.sde";
        static string rasterFolder = @"C:\Temp\data";
        static string catalogName = "rc_1";
        static string outRC =  sdePath + "\\" + catalogName;

        static void Main(string[] args)
        {

            #region Initialize the license 
                ESRI.ArcGIS.esriSystem.AoInitialize aoInit = null;
                try
                {
                    Console.WriteLine("Obtaining license");
                    ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.Desktop);
                    aoInit = new AoInitializeClass();
                    esriLicenseStatus licStatus = aoInit.Initialize(esriLicenseProductCode.esriLicenseProductCodeStandard);
                    Console.WriteLine("Ready with license.");
                }
                catch (Exception exc)
                {
                    // If it fails at this point, shutdown the test and ignore any subsequent errors.
                    Console.WriteLine(exc.Message);
                }
            #endregion


            //Coordinate system for raster column
            IGPCoordinateSystem rSR = new GPCoordinateSystemClass();
            rSR.SpatialReference = CreateSpatialReference((int)esriSRProjCSType.esriSRProjCS_World_Mercator);
            //Coordinate system for geometry column
            IGPSpatialReference gSR = new GPSpatialReferenceClass();
            gSR.SpatialReference = CreateSpatialReference((int)esriSRProjCSType.esriSRProjCS_World_Mercator);

            //Creates raster catalog
            CreateRasterCatalog_GP(rSR, gSR);

            //Loads rasters in the given directory to raster catalog
            LoadDirToRasterCatalog(outRC, rasterFolder);
            System.Console.WriteLine("Loading completed");

            System.Console.ReadLine();//waiting user to click a key to finish

           //Do not make any call to ArcObjects after license is shut down.
            aoInit.Shutdown();
        }

        //Creates raster catalog using GP CreateRasterCatalog class
        static void CreateRasterCatalog_GP(object rasterCoordSys, object geometryCoordsys)
        {
            //Initialize GeoProcessor
            ESRI.ArcGIS.Geoprocessor.Geoprocessor geoProcessor = new ESRI.ArcGIS.Geoprocessor.Geoprocessor();

            //CreateRasterCatalog GP tool
            CreateRasterCatalog createRasterCatalog = new CreateRasterCatalog();

            //Set parameters
            createRasterCatalog.out_path = sdePath;
            createRasterCatalog.out_name = catalogName;
            createRasterCatalog.raster_spatial_reference = rasterCoordSys;
            createRasterCatalog.spatial_reference = geometryCoordsys;

            //Execute the tool to create a raster catalog
            geoProcessor.Execute(createRasterCatalog, null);
            ReturnMessages(geoProcessor);
        }

        //GP message handling
        private static void ReturnMessages(Geoprocessor gp)
        {
            if (gp.MessageCount > 0)
            {
                for (int Count = 0; Count <= gp.MessageCount - 1; Count++)
                {
                    Console.WriteLine(gp.GetMessage(Count));
                }
            }
        }

        static void LoadDirToRasterCatalog(string outRasterCatalog, string inputDir)
        {
            //Initialize GeoProcessor
            ESRI.ArcGIS.Geoprocessor.Geoprocessor geoProcessor = new ESRI.ArcGIS.Geoprocessor.Geoprocessor();

            //Set parameters
            IVariantArray parameters = new VarArrayClass();

            //Set input folder
            parameters.Add(inputDir);

            //Set target GDB raster catalog
            parameters.Add(outRasterCatalog);

            //Execute the tool to load rasters in the directory to raster catalog
            geoProcessor.Execute("WorkspaceToRasterCatalog", parameters, null);
            ReturnMessages(geoProcessor);
        }

        //Create a spatial reference with given factory code
        static ISpatialReference CreateSpatialReference(int code)
        {
            ISpatialReferenceFactory2 spatialReferenceFact = new SpatialReferenceEnvironmentClass();
            try
            {
                return spatialReferenceFact.CreateSpatialReference(code);
            }
            catch
            {
                return new UnknownCoordinateSystemClass();
            }
        }
    }

}