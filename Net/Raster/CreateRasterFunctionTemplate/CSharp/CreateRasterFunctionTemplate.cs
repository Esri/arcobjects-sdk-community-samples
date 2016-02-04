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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.DataSourcesRaster;

namespace RasterSamples
{
    class CreateRasterFunctionTemplate
    {
        [STAThread]
        static void Main(string[] args)
        {
            ESRI.ArcGIS.esriSystem.AoInitialize aoInit;

            #region Initialize License
            try
            {
                Console.WriteLine("Obtaining license");
                ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.Desktop);
                aoInit = new AoInitializeClass();
                esriLicenseStatus licStatus = aoInit.Initialize(esriLicenseProductCode.esriLicenseProductCodeBasic);
                Console.WriteLine("Ready with license.");
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
                // The directory which contains the Panchromatic Image.
                string panDir = @"C:\Data\QB\Pan";
                // The directory which contains the Multispectral Image.
                string rgbDir = @"C:\Data\QB\MS";
                
                string panImageName = "05JAN27104436-P1BS-005533787010_01_P002.TIF";
                string rgbImageName = "05JAN27104436-M1BS-005533787010_01_P002.TIF";

                // Output filename.
                string outputDataset = @"c:\Temp\QBTemplateCS.afr";
                #endregion

                #region Initialize
                // Setup Workspaces.
                Type factoryType = Type.GetTypeFromProgID("esriDataSourcesRaster.RasterWorkspaceFactory");
                IWorkspaceFactory workspaceFactory = (IWorkspaceFactory)Activator.CreateInstance(factoryType);
                IRasterWorkspace panRasterWorkspace = (IRasterWorkspace)workspaceFactory.OpenFromFile(panDir, 0);
                IRasterWorkspace rgbRasterWorkspace = (IRasterWorkspace)workspaceFactory.OpenFromFile(rgbDir, 0);

                // Open Datasets
                IRasterDataset panDataset = panRasterWorkspace.OpenRasterDataset(panImageName);
                IRasterDataset rgbDataset = rgbRasterWorkspace.OpenRasterDataset(rgbImageName);
                #endregion

                #region Create Variables
                // Create one variable of type IRasterFunctionVariable for each 
                // Raster Dataset opened above
                
                // Create a new Raster Function Variable
                IRasterFunctionVariable panVar = new RasterFunctionVariableClass();
                // Set the name of the variable
                panVar.Name = "panImage";
                // Describe the variable
                panVar.Description = "Panchromatic Image to be used for pansharpening";
                // Specify whether it represents a dataset
                panVar.IsDataset = true;

                // Create a new Raster Function Variable
                IRasterFunctionVariable rgbVar = new RasterFunctionVariableClass();
                // Set the name of the variable
                rgbVar.Name = "rgbImage";
                // Describe the variable
                rgbVar.Description = "Multispectral Image to be used for pansharpening";
                // Specify whether it represents a dataset
                rgbVar.IsDataset = true;
                #endregion

                #region Prepare the Pan Image
                // Setup statistics for each band
                IArray statsArrayPan = new ArrayClass();
                IRasterStatistics statsPanBand = new RasterStatisticsClass();
                statsPanBand.Minimum = 1;
                statsPanBand.Maximum = 2047;
                statsArrayPan.Add(statsPanBand);
                // Create the arguments object for the stretching function
                IStretchFunctionArguments stretchingPanFunctionArguements = new StretchFunctionArgumentsClass();
                // Set the stretching type
                stretchingPanFunctionArguements.StretchType =
                    esriRasterStretchType.esriRasterStretchMinimumMaximum;
                // Set the statistics created above
                stretchingPanFunctionArguements.Statistics = statsArrayPan;
                // Set the input raster, in this case, the variable for the Pan Image
                stretchingPanFunctionArguements.Raster = panVar;

                // Create the function object to stretch the Pan Image.
                IRasterFunction stretchingPanFunction = new StretchFunction();

                // Create a Raster Function Template object for the stretch function
                IRasterFunctionTemplate stretchingPanFunctionT = new RasterFunctionTemplateClass();
                // Set the function on the template
                stretchingPanFunctionT.Function = (IRasterFunction)stretchingPanFunction;
                // Set the arguments for the function
                stretchingPanFunctionT.Arguments = stretchingPanFunctionArguements;
                #endregion

                #region Prepare the Multispectral (RGB) Image
                // Create an array which defines the order of bands
                ILongArray bandIDs = new LongArrayClass();
                bandIDs.Add(2);
                bandIDs.Add(1);
                bandIDs.Add(0);
                // Create an Extract Band Function Arguments object
                IExtractBandFunctionArguments extractRgbBandFunctionArgs = (IExtractBandFunctionArguments)
                    new ExtractBandFunctionArguments();
                // Set the order of bands of the output
                extractRgbBandFunctionArgs.BandIDs = bandIDs;
                // Set the input raster, in this case the variable for the Multispectral Image
                extractRgbBandFunctionArgs.Raster = rgbVar;

                // Create the Extract Band Function object
                IRasterFunction extractRgbBandFunction = new ExtractBandFunction();

                // Create a Raster Function Template object for the function created above
                IRasterFunctionTemplate extractRgbBandFunctionT = new RasterFunctionTemplateClass();
                // Set the function on the template
                extractRgbBandFunctionT.Function = (IRasterFunction)extractRgbBandFunction;
                // Set the arguments for the function
                extractRgbBandFunctionT.Arguments = extractRgbBandFunctionArgs;

                // Setup statistics for each band
                IArray statsArray = new ArrayClass();
                IRasterStatistics statsMulBand1 = new RasterStatisticsClass();
                statsMulBand1.Minimum = 100;
                statsMulBand1.Maximum = 1721;
                statsArray.Add(statsMulBand1);
                IRasterStatistics statsMulBand2 = new RasterStatisticsClass();
                statsMulBand2.Minimum = 95;
                statsMulBand2.Maximum = 2047;
                statsArray.Add(statsMulBand2);
                IRasterStatistics statsMulBand3 = new RasterStatisticsClass();
                statsMulBand3.Minimum = 34;
                statsMulBand3.Maximum = 2006;
                statsArray.Add(statsMulBand3);

                // Create a stretching function for the multispectral image
                IRasterFunction stretchingRGBFunction = new StretchFunction();
                // Create an arguments object for the stretch function
                IStretchFunctionArguments stretchingRGBFunctionArguments = 
                    new StretchFunctionArgumentsClass();
                // Set the type of stretchings to perform
                stretchingRGBFunctionArguments.StretchType =
                    esriRasterStretchType.esriRasterStretchMinimumMaximum;
                // Set the statistics created above
                stretchingRGBFunctionArguments.Statistics = statsArray;
                // Set the extract band function template created above as the input
                stretchingRGBFunctionArguments.Raster = extractRgbBandFunctionT;

                // Create a Raster Function Template object for the function created above
                IRasterFunctionTemplate stretchingRGBFunctionT = new RasterFunctionTemplateClass();
                // Set the function on the template
                stretchingRGBFunctionT.Function = (IRasterFunction)stretchingRGBFunction;
                // Set the arguments for the function
                stretchingRGBFunctionT.Arguments = stretchingRGBFunctionArguments;
                #endregion

                #region Pansharpen the Pan Image with the Multispectral
                // Create a Raster Function Arguments object for the pansharpen function
                IPansharpeningFunctionArguments pansharpFunctionArguements = 
                    new PansharpeningFunctionArgumentsClass();
                // Set the Panchromatic image which has been prepared above
                pansharpFunctionArguements.PanImage = stretchingPanFunctionT;
                // Set the Multispectral image which has been prepared above
                pansharpFunctionArguements.MSImage = stretchingRGBFunctionT;

                // Create a new pansharpen raster function object
                IRasterFunction pansharpenFunction = new PansharpeningFunction();
                // Create a new pansharpen function arguments object
                IPansharpeningFunctionArguments pansharpenFunctionArguements = 
                    new PansharpeningFunctionArgumentsClass();
                // Set the pansharpening type
                pansharpenFunctionArguements.PansharpeningType = 
                    esriPansharpeningType.esriPansharpeningESRI;
                // Create an array of doubles that sets the weights for each band
                IDoubleArray weightsArray = new DoubleArrayClass();
                weightsArray.Add(0.167);
                weightsArray.Add(0.166);
                weightsArray.Add(0.166);
                // and set it on the arguments object
                pansharpenFunctionArguements.Weights = weightsArray;

                // Create a Raster Function Template object for the pansharpen function
                IRasterFunctionTemplate rasterFunction = new RasterFunctionTemplateClass();
                rasterFunction.Function = (IRasterFunction)pansharpenFunction;
                rasterFunction.Arguments = pansharpFunctionArguements;
                ((IRasterFunction)rasterFunction).PixelType = rstPixelType.PT_UCHAR;
                #endregion

                #region Resolve variables
                // Create a PropertySet object to set the values for the 
                // Raster Function Variables created above
                IPropertySet variables = new PropertySet();
                variables.SetProperty("panImage", panDataset);
                variables.SetProperty("rgbImage", rgbDataset);
                #endregion

                #region Create the Function Raster Dataset
                // Create the Function Raster Dataset Object for the Pansharpened Dataset
                IFunctionRasterDataset functionRasterDataset = new FunctionRasterDataset();
                // Create a name object for the Function Raster Dataset.
                IFunctionRasterDatasetName functionRasterDatasetName =
                    new FunctionRasterDatasetNameClass();
                // Specify the output filename for the new dataset (including 
                // the .afr extension at the end).
                functionRasterDatasetName.FullName = outputDataset;
                functionRasterDataset.FullName = (IName)functionRasterDatasetName;
                // Initialize the new Function Raster Dataset with the Raster Function 
                // Template and the property set containing the variables and their values.
                functionRasterDataset.Init((IRasterFunction)rasterFunction, variables);

                ITemporaryDataset myTempDset = (ITemporaryDataset)functionRasterDataset;
                myTempDset.MakePermanent();
                #endregion

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
    }
}
