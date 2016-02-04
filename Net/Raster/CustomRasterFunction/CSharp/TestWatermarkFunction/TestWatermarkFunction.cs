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
using ESRI.ArcGIS;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using CustomFunction;

/* 
    This is an optional test program which allows the user to use the Custom 
    Raster Function in a variety of ways:
    1.) Create a function raster dataset by applying the custom function on top
        of a raster dataset.
    2.) Add the custom function on top of a mosaic dataset.
    3.) Create a RasterFunctionTemplate from the function.
    4.) Serialize the function in the form of a RasterFunctionTemplate object to an xml.
    5.) Get a RasterFunctionTemplate object back from a serialized xml.

    Note: Successsful serialization to xml involves changes to the XmlSupport.dat file in the 
    "<Program Files>\ArcGIS\Desktop10.2\bin" folder.
*/

namespace SampleTest
{
    public class TestWatermarkFunction
    {
        [STAThread]
        public static void Main(string[] args)
        {
            #region Initialize License
            ESRI.ArcGIS.esriSystem.AoInitialize aoInit;
            try
            {
                Console.WriteLine("Obtaining license");
                ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.Desktop);
                aoInit = new AoInitializeClass();
                // To make changes to a Mosaic Dataset, a Standard or Advanced license is required.
                esriLicenseStatus licStatus = aoInit.Initialize(esriLicenseProductCode.esriLicenseProductCodeAdvanced);
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
                // Flags to specify the operation to perform
                bool addToRD = true; // Create Watermark Function Raster Dataset
                bool addToMD = false; // Add Watermark Function to MD
                bool writeTemplateToXml = false; // Serialize a template form of the NDVI Custom Funtion to Xml.
                bool getfromXml = false; // Get a template object back from its serialized xml.

                #region Specify inputs.
                // Raster Dataset parameters
                string workspaceFolder = @"f:\data\RasterDataset\LACounty\";
                string rasterDatasetName = "6466_1741c.tif";
                // Output parameters for Function Raster Dataset
                string outputFolder = @"c:\temp\CustomFunction";
                string outputName = "WatermarkSample.afr";

                // Mosaic Dataset parameters
                // GDB containing the Mosaic Dataset
                string mdWorkspaceFolder = @"c:\temp\CustomFunction\SampleGdb.gdb";
                // Name of the Mosaic Dataset
                string mdName = "SampleMD";
                
                // Watermark Parameters
                string watermarkImagePath =
                    @"e:\Dev\SDK\Raster\NET\Samples\CustomRasterFunction\CSharp\TestWatermarkFunction\Sample.png";
                double blendPercentage = 80.00;
                esriWatermarkLocation wmLocation = esriWatermarkLocation.esriWatermarkCenter;

                // Xml file path to save to or read from xml.
                string xmlFilePath = @"c:\temp\CustomFunction\Xml\Watermark.RFT.xml";
                #endregion

                if (addToRD)
                {
                    // Open the Raster Dataset
                    Type factoryType = Type.GetTypeFromProgID("esriDataSourcesRaster.RasterWorkspaceFactory");
                    IWorkspaceFactory workspaceFactory = (IWorkspaceFactory)Activator.CreateInstance(factoryType);
                    IRasterWorkspace rasterWorkspace = (IRasterWorkspace)workspaceFactory.OpenFromFile(workspaceFolder, 0);
                    IRasterDataset rasterDataset = rasterWorkspace.OpenRasterDataset(rasterDatasetName);
                    AddWatermarkToRD(rasterDataset, outputFolder, outputName, watermarkImagePath, blendPercentage, wmLocation);
                    // Cleanup
                    workspaceFactory = null;
                    rasterWorkspace = null;
                    rasterDataset = null;
                }

                if (addToMD)
                    AddWatermarkDataToMD(mdWorkspaceFolder, mdName, watermarkImagePath, blendPercentage, wmLocation, true);

                if (writeTemplateToXml && xmlFilePath != "")
                {
                    // Create a template with the Watermark Function.
                    IRasterFunctionTemplate watermarkFunctionTemplate =
                        CreateWatermarkTemplate(watermarkImagePath, blendPercentage, wmLocation);
                    // Serialize the template to an xml file.
                    bool status = WriteToXml(watermarkFunctionTemplate, xmlFilePath);
                }

                if (getfromXml && xmlFilePath != "")
                {
                    // Create a RasterFunctionTemplate object from the serialized xml. 
                    object serializedObj = ReadFromXml(xmlFilePath);
                    if (serializedObj is IRasterFunctionTemplate)
                        Console.WriteLine("Success.");
                    else
                        Console.WriteLine("Failed.");
                }

                Console.WriteLine("Press any key...");
                Console.ReadKey();
                aoInit.Shutdown();
            }
            catch (Exception exc)
            {
                Console.WriteLine("Exception Caught in Main: " + exc.Message);
                Console.WriteLine("Failed.");
                Console.WriteLine("Press any key...");
                Console.ReadKey();
                aoInit.Shutdown();
            }
        }

        public static bool AddWatermarkToRD(IRasterDataset RasterDataset, string OutputFolder, string OutputName,
            string watermarkImagePath, double blendPercentage, esriWatermarkLocation watermarklocation)
        {
            try
            {
                // Create Watermark Function
                IRasterFunction rasterFunction = new CustomFunction.WatermarkFunction();
                // Create the Watermark Function Arguments object
                IWatermarkFunctionArguments rasterFunctionArguments =
                    new WatermarkFunctionArguments();
                // Set the WatermarkImagePath
                rasterFunctionArguments.WatermarkImagePath = watermarkImagePath;
                // the blending percentage,
                rasterFunctionArguments.BlendPercentage = blendPercentage;
                // and the watermark location.
                rasterFunctionArguments.WatermarkLocation = watermarklocation;
                // Set the Raster Dataset as the input raster
                rasterFunctionArguments.Raster = RasterDataset;

                // Create Function Dataset
                IFunctionRasterDataset functionRasterDataset = new FunctionRasterDataset();
                // Create a Function Raster Dataset Name object
                IFunctionRasterDatasetName functionRasterDatasetName =
                    (IFunctionRasterDatasetName)new FunctionRasterDatasetName();
                // Set the path for the output Function Raster Dataset
                functionRasterDatasetName.FullName = System.IO.Path.Combine(OutputFolder, OutputName);
                functionRasterDataset.FullName = (IName)functionRasterDatasetName;
                // Initialize the Function Raster Dataset with the function and 
                // its arguments object
                functionRasterDataset.Init(rasterFunction, rasterFunctionArguments);

                // Save as Function Raster Dataset as an .afr file
                ITemporaryDataset myTempDset = (ITemporaryDataset)functionRasterDataset;
                myTempDset.MakePermanent();

                Console.WriteLine("Generated " + OutputName + ".");
                Console.WriteLine("Success.");
                return true;
            }
            catch (Exception exc)
            {
                Console.WriteLine("Exception Caught while adding watermark to Raster Dataset: " + exc.Message);
                Console.WriteLine("Failed.");
                return false;
            }
        }

        public static bool AddWatermarkDataToMD(string MDWorkspaceFolder, string MDName, string watermarkImagePath, 
            double blendPercentage, esriWatermarkLocation watermarklocation, bool clearFunctions)
        {
            try
            {
                // Open MD
                Type factoryType = Type.GetTypeFromProgID("esriDataSourcesGDB.FileGDBWorkspaceFactory");
                IWorkspaceFactory mdWorkspaceFactory = (IWorkspaceFactory)Activator.CreateInstance(factoryType);
                IWorkspace mdWorkspace = mdWorkspaceFactory.OpenFromFile(MDWorkspaceFolder, 0);
                IRasterWorkspaceEx workspaceEx = (IRasterWorkspaceEx)(mdWorkspace);
                IMosaicDataset mosaicDataset = (IMosaicDataset)workspaceEx.OpenRasterDataset(
                    MDName);

                if (clearFunctions) // Clear functions already added to MD.
                    mosaicDataset.ClearFunction();

                // Create Watermark Function
                IRasterFunction rasterFunction = new CustomFunction.WatermarkFunction();
                // Create the Watermark Function Arguments object
                IWatermarkFunctionArguments rasterFunctionArguments =
                    new WatermarkFunctionArguments();
                // Set the WatermarkImagePath
                rasterFunctionArguments.WatermarkImagePath =
                    watermarkImagePath;
                // the blending percentage,
                rasterFunctionArguments.BlendPercentage = blendPercentage;
                // and the watermark location.
                rasterFunctionArguments.WatermarkLocation = watermarklocation;

                // Add function to MD.
                // This function takes the name of the property corresponding to the Raster 
                // property of the Arguments object (in this case is it called Raster itself: 
                // rasterFunctionArguments.Raster) as its third argument.
                mosaicDataset.ApplyFunction(rasterFunction, rasterFunctionArguments, "Raster");

                Console.WriteLine("Added Watermark to MD: " + MDName + ".");
                Console.WriteLine("Success.");
                return true;
            }
            catch (Exception exc)
            {
                Console.WriteLine("Exception Caught while adding watermark to MD: " + exc.Message);
                Console.WriteLine("Failed.");
                return false;
            }
        }

        public static IRasterFunctionTemplate CreateWatermarkTemplate(string watermarkImagePath, 
            double blendPercentage, esriWatermarkLocation watermarklocation)
        {
            #region Setup Raster Function Vars
            IRasterFunctionVariable watermarkRasterRFV = new RasterFunctionVariableClass();
            watermarkRasterRFV.Name = "Raster";
            watermarkRasterRFV.IsDataset = true;
            IRasterFunctionVariable watermarkImagePathRFV = new RasterFunctionVariableClass();
            watermarkImagePathRFV.Name = "WatermarkImagePath";
            watermarkImagePathRFV.Value = watermarkImagePath;
            watermarkImagePathRFV.IsDataset = false;
            IRasterFunctionVariable watermarkBlendPercRFV = new RasterFunctionVariableClass();
            watermarkBlendPercRFV.Name = "BlendPercentage";
            watermarkBlendPercRFV.Value = blendPercentage;
            IRasterFunctionVariable watermarkLocationRFV = new RasterFunctionVariableClass();
            watermarkLocationRFV.Name = "Watermarklocation";
            watermarkLocationRFV.Value = watermarklocation;
            #endregion

            #region Setup Raster Function Template
            // Create the Watermark Function Arguments object
            IRasterFunctionArguments rasterFunctionArguments =
                new CustomFunction.WatermarkFunctionArguments();
            // Set the WatermarkImagePath
            rasterFunctionArguments.PutValue("WatermarkImagePath", watermarkImagePathRFV);
            // the blending percentage,
            rasterFunctionArguments.PutValue("BlendPercentage", watermarkBlendPercRFV);
            // and the watermark location.
            rasterFunctionArguments.PutValue("WatermarkLocation", watermarkLocationRFV);
            // Set the Raster Dataset as the input raster
            rasterFunctionArguments.PutValue("Raster", watermarkRasterRFV);

            IRasterFunction watermarkFunction = new CustomFunction.WatermarkFunction();
            IRasterFunctionTemplate watermarkFunctionTemplate = new RasterFunctionTemplateClass();
            watermarkFunctionTemplate.Function = watermarkFunction;
            watermarkFunctionTemplate.Arguments = rasterFunctionArguments;
            #endregion

            return watermarkFunctionTemplate;
        }

        public static bool WriteToXml(object inputDataset, string xmlFilePath)
        {
            try
            {
                // Check if file exists
                if (File.Exists(xmlFilePath))
                {
                    Console.WriteLine("File already exists.");
                    return false;
                }
                // Create new file.
                IFile xmlFile = new FileStreamClass();
                xmlFile.Open(xmlFilePath, esriFilePermission.esriReadWrite);
                // See if the input dataset can be Xml serialized.
                IXMLSerialize mySerializeData = (IXMLSerialize)inputDataset;
                // Create new XmlWriter object.
                IXMLWriter myXmlWriter = new XMLWriterClass();
                myXmlWriter.WriteTo((IStream)xmlFile);
                myXmlWriter.WriteXMLDeclaration();
                IXMLSerializer myXmlSerializer = new XMLSerializerClass();
                // Write to XML File
                myXmlSerializer.WriteObject(myXmlWriter, null, null, null, null, mySerializeData);
                Console.WriteLine("Success.");
                return true;
            }
            catch (Exception exc)
            {
                Console.WriteLine("Exception caught in WriteToXml: " + exc.Message);
                Console.WriteLine("Failed.");
                return false;
            }
        }

        public static object ReadFromXml(string xmlFilePath)
        {
            try
            {
                IFile inputXmlFile = new FileStreamClass();
                inputXmlFile.Open(xmlFilePath, esriFilePermission.esriReadWrite);
                IXMLReader myXmlReader = new XMLReaderClass();
                myXmlReader.ReadFrom((IStream)inputXmlFile);
                IXMLSerializer myInputXmlSerializer = new XMLSerializerClass();
                object myFunctionObject = myInputXmlSerializer.ReadObject(myXmlReader, null, null);
                return myFunctionObject;
            }
            catch (Exception exc)
            {
                Console.WriteLine("Exception caught in ReadFromXml: " + exc.Message);
                return null;
            }
        }
    }
}