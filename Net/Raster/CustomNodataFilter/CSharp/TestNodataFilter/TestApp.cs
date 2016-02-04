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
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS;

//Tests the custom NodataFilter
namespace TestNodataFilter
{
    class App
    {
        static void Main()
        {
            #region Initialize license
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
            #endregion
            try
            {
                //Get the location for data installed with .net sdk
                string versionInfo = RuntimeManager.ActiveRuntime.Version;
                RegistryKey regKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\ESRI\ArcObjectsSDK" + versionInfo + @"\.NET");
                string path = System.Convert.ToString(regKey.GetValue("MainDir"));
                string rasterFolder = System.IO.Path.Combine(path, @"Samples\ArcObjectsNET\CustomNodataFilter");
                IPixelOperation raster = (IPixelOperation)OpenRasterDataset(rasterFolder, "testimage.tif");
                
                if (raster == null)
                {
                    Console.WriteLine("invalid raster");
                    return;
                }

                //create nodatafilter and set properties
                CustomNodataFilter.INodataFilter nFilter = new CustomNodataFilter.NodataFilter();

                //filter out all values between 0 and 50 as nodata
                nFilter.MinNodataValue = 0;
                nFilter.MaxNodataValue = 50;

                //apply the convolutionfilter to raster
                raster.PixelFilter = nFilter;

                //set nodata value using the minimum of the nodata range
                IRasterProps rasterProps = (IRasterProps)raster;
                rasterProps.NoDataValue = 0;

                //save the filtered raster to a new raster dataset in TEMP directory
                ISaveAs saveAs = (ISaveAs)raster;
                //IWorkspace workspace = OpenWorkspace(Environment.GetEnvironmentVariable("TEMP"));
                IWorkspace workspace = OpenWorkspace(rasterFolder);
                saveAs.SaveAs("nodata.tif", workspace, "TIFF");

                Console.WriteLine("Completed");
                Console.ReadLine();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            //ESRI License Initializer generated code.
            //Do not make any call to ArcObjects after ShutDown()
            aoInit.Shutdown();
        }

        //Open raster dataset and get raster
        static IRaster OpenRasterDataset(string path, string datasetName)
        {
            IRasterWorkspace rasterWorkspace = (IRasterWorkspace)OpenWorkspace(path);

            if (rasterWorkspace == null)
                return null;

            IRasterDataset2 rasterDataset;
            rasterDataset = (IRasterDataset2)rasterWorkspace.OpenRasterDataset(datasetName);

            if (rasterDataset == null)
                return null;

            return rasterDataset.CreateFullRaster();
        }

        //Open file based raster workspace
        static IWorkspace OpenWorkspace(string path)
        {
            IWorkspaceFactory workspaceFactory = new RasterWorkspaceFactoryClass();
            IWorkspace rasterWorkspace = workspaceFactory.OpenFromFile(path, 0);

            return rasterWorkspace;

        }
    }
}