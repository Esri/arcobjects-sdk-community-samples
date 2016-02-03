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

namespace TestNDVICustomFunction
{
    public class TestNDVICustomFunction
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
                bool addToRD = true; // Create NDVI Custom Function Raster Dataset
                bool addToMD = false; // Add NDVI Custom Function to MD
                bool writeTemplateToXml = false; // Serialize a template form of the NDVI Custom Funtion to Xml.
                bool getfromXml = false; // Get a template object back from its serialized xml.

                #region Specify inputs.
                // Raster Dataset parameters
                string workspaceFolder = @"c:\temp";
                string rasterDatasetName = "Dubai_ov.tif";
                // Output parameters for Function Raster Dataset
                string outputFolder = @"c:\temp";
                string outputName = "NDVICustomFunctionSample.afr";

                // Mosaic dataset parameters
                // GDB containing the Mosaic Dataset
                string mdWorkspaceFolder = @"c:\temp\testGdb.gdb";
                // Name of the mosaic dataset
                string mdName = "testMD";

                // NDVI Custom Function Parameters
                string bandIndices = @"4 3";

                // Xml file path to save to or read from xml.
                string xmlFilePath = @"e:\Dev\Samples CSharp\CustomRasterFunction\Xml\NDVICustomAFR.xml";
                #endregion

                if (addToRD)
                {
                    // Open the Raster Dataset
                    Type factoryType = Type.GetTypeFromProgID("esriDataSourcesRaster.RasterWorkspaceFactory");
                    IWorkspaceFactory workspaceFactory = (IWorkspaceFactory)Activator.CreateInstance(factoryType);
                    IRasterWorkspace rasterWorkspace = (IRasterWorkspace)workspaceFactory.OpenFromFile(workspaceFolder, 0);
                    IRasterDataset rasterDataset = rasterWorkspace.OpenRasterDataset(rasterDatasetName);
                    AddNDVICustomToRD(rasterDataset, outputFolder, outputName, bandIndices);
                    // Cleanup
                    workspaceFactory = null;
                    rasterWorkspace = null;
                    rasterDataset = null;
                }

                if (addToMD)
                    AddNDVICustomDataToMD(mdWorkspaceFolder, mdName, bandIndices, true);

                if (writeTemplateToXml && xmlFilePath != "")
                {
                    // Create a template with the NDVI Custom Function.
                    IRasterFunctionTemplate ndviCustomFunctionTemplate = CreateNDVICustomTemplate(bandIndices);
                    // Serialize the template to an xml file.
                    bool status = WriteToXml(ndviCustomFunctionTemplate, xmlFilePath);
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

        public static bool AddNDVICustomToRD(IRasterDataset RasterDataset, string OutputFolder, string OutputName,
            string bandIndices)
        {
            try
            {
                // Create NDVI Custom Function
                IRasterFunction rasterFunction = new CustomFunction.NDVICustomFunction();
                // Create the NDVI Custom Function Arguments object
                INDVICustomFunctionArguments rasterFunctionArguments = new NDVICustomFunctionArguments();
                // Set the Band Indices
                rasterFunctionArguments.BandIndices = bandIndices;

                // Set the RasterDataset as the input raster
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
                Console.WriteLine("Exception Caught while adding NDVI Custom Function to Raster Dataset: " + exc.Message);
                Console.WriteLine("Failed.");
                return false;
            }
        }

        public static bool AddNDVICustomDataToMD(string MDWorkspaceFolder, string MDName,
            string bandIndices, bool clearFunctions)
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

                // Create NDVI Custom Function
                IRasterFunction rasterFunction = new CustomFunction.NDVICustomFunction();
                // Create the NDVI Custom Function Arguments object
                INDVICustomFunctionArguments rasterFunctionArguments = new NDVICustomFunctionArguments();
                // Set the Band Indices
                rasterFunctionArguments.BandIndices = bandIndices;

                // Add function to MD.
                // This function takes the name of the property corresponding to the Raster 
                // property of the Arguments object (in this case is it called Raster itself: 
                // rasterFunctionArguments.Raster) as its third argument.
                mosaicDataset.ApplyFunction(rasterFunction, rasterFunctionArguments, "Raster");

                Console.WriteLine("Added NDVI Custom Function to MD: " + MDName + ".");
                Console.WriteLine("Success.");
                return true;
            }
            catch (Exception exc)
            {
                Console.WriteLine("Exception Caught while adding NDVI Custom Function to MD: " + exc.Message);
                Console.WriteLine("Failed.");
                return false;
            }
        }

        public static IRasterFunctionTemplate CreateNDVICustomTemplate(string bandIndices)
        {
            #region Setup Raster Function Vars
            IRasterFunctionVariable watermarkRasterRFV = new RasterFunctionVariableClass();
            watermarkRasterRFV.Name = "Raster";
            watermarkRasterRFV.IsDataset = true;
            IRasterFunctionVariable bandIndicesRFV = new RasterFunctionVariableClass();
            bandIndicesRFV.Name = "BandIndices";
            bandIndicesRFV.Value = bandIndices;
            bandIndicesRFV.IsDataset = false;
            #endregion

            #region Setup Raster Function Template
            // Create the NDVI Custom Function Arguments object
            IRasterFunctionArguments rasterFunctionArguments = new CustomFunction.NDVICustomFunctionArguments();
            // Set the Band Indices
            rasterFunctionArguments.PutValue("BandIndices", bandIndicesRFV);
            // Set the Raster Dataset as the input raster
            rasterFunctionArguments.PutValue("Raster", watermarkRasterRFV);
            // Create the NDVI Custom Function
            IRasterFunction ndviCustomFunction = new CustomFunction.NDVICustomFunction();

            IRasterFunctionTemplate ndviCustomFunctionTemplate = new RasterFunctionTemplateClass();
            ndviCustomFunctionTemplate.Function = ndviCustomFunction;
            ndviCustomFunctionTemplate.Arguments = rasterFunctionArguments;
            #endregion

            return ndviCustomFunctionTemplate;
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
