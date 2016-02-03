Imports System.IO
Imports ESRI.ArcGIS
Imports ESRI.ArcGIS.DataSourcesGDB
Imports ESRI.ArcGIS.DataSourcesRaster
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Geometry
Imports CustomFunction.CustomFunction

' 
'   This is an optional test program which allows the user to use the Custom 
'   Raster Function in a variety of ways:
'   1.) Create a function raster dataset by applying the custom function on top
'       of a raster dataset.
'   2.) Add the custom function on top of a mosaic dataset.
'   3.) Create a RasterFunctionTemplate from the function.
'   4.) Serialize the function in the form of a RasterFunctionTemplate object to an xml.
'   5.) Get a RasterFunctionTemplate object back from a serialized xml.
'   
'   Note: Successsful serialization to xml involves changes to the XmlSupport.dat file in the 
'   "<Program Files>\ArcGIS\Desktop10.2\bin" folder.
'

Namespace SampleTest
    Public Class TestWatermarkFunction
        <STAThread()> _
        Public Shared Sub Main(ByVal args As String())
            '#Region "Initialize License"
            Dim aoInit As ESRI.ArcGIS.esriSystem.AoInitialize
            Try
                Console.WriteLine("Obtaining license")
                ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.Desktop)
                aoInit = New AoInitializeClass()
                ' To make changes to a Mosaic Dataset, a Standard or Advanced license is required.
                Dim licStatus As esriLicenseStatus = aoInit.Initialize(esriLicenseProductCode.esriLicenseProductCodeAdvanced)
                Console.WriteLine("Ready with license.")
            Catch exc As Exception
                ' If it fails at this point, shutdown the test and ignore any subsequent errors.
                Console.WriteLine(exc.Message)
                Return
            End Try
            '#End Region

            Try
                ' Flags to specify the operation to perform
                Dim addToRD As Boolean = True
                ' Create Watermark Function Raster Dataset
                Dim addToMD As Boolean = False
                ' Add Watermark Function to MD
                Dim writeTemplateToXml As Boolean = False
                ' Serialize a template form of the NDVI Custom Funtion to Xml.
                Dim getfromXml As Boolean = False
                ' Get a template object back from its serialized xml.
                '#Region "Specify inputs."
                ' Raster Dataset parameters
                Dim workspaceFolder As String = "f:\data\RasterDataset\LACounty\"
                Dim rasterDatasetName As String = "6466_1741c.tif"
                ' Output parameters for Function Raster Dataset
                Dim outputFolder As String = "c:\temp\CustomFunction"
                Dim outputName As String = "WatermarkSample.afr"

                ' Mosaic Dataset parameters
                ' GDB containing the Mosaic Dataset
                Dim mdWorkspaceFolder As String = "c:\temp\CustomFunction\SampleGdb.gdb"
                ' Name of the Mosaic Dataset
                Dim mdName As String = "SampleMD"

                ' Watermark Parameters
                Dim watermarkImagePath As String = "e:\Dev\SDK\Raster\NET\Samples\CustomRasterFunction\CSharp\TestWatermarkFunction\Sample.png"
                Dim blendPercentage As Double = 80.0
                Dim wmLocation As esriWatermarkLocation = esriWatermarkLocation.esriWatermarkCenter

                ' Xml file path to save to or read from xml.
                Dim xmlFilePath As String = "c:\temp\CustomFunction\Xml\Watermark.RFT.xml"
                '#End Region

                If addToRD Then
                    ' Open the Raster Dataset
                    Dim factoryType As Type = Type.GetTypeFromProgID("esriDataSourcesRaster.RasterWorkspaceFactory")
                    Dim workspaceFactory As IWorkspaceFactory = DirectCast(Activator.CreateInstance(factoryType), IWorkspaceFactory)
                    Dim rasterWorkspace As IRasterWorkspace = DirectCast(workspaceFactory.OpenFromFile(workspaceFolder, 0), IRasterWorkspace)
                    Dim rasterDataset As IRasterDataset = rasterWorkspace.OpenRasterDataset(rasterDatasetName)
                    AddWatermarkToRD(rasterDataset, outputFolder, outputName, watermarkImagePath, blendPercentage, wmLocation)
                    ' Cleanup
                    workspaceFactory = Nothing
                    rasterWorkspace = Nothing
                    rasterDataset = Nothing
                End If

                If addToMD Then
                    AddWatermarkDataToMD(mdWorkspaceFolder, mdName, watermarkImagePath, blendPercentage, wmLocation, True)
                End If

                If writeTemplateToXml AndAlso xmlFilePath <> "" Then
                    ' Create a template with the Watermark Function.
                    Dim watermarkFunctionTemplate As IRasterFunctionTemplate = CreateWatermarkTemplate(watermarkImagePath, blendPercentage, wmLocation)
                    ' Serialize the template to an xml file.
                    Dim status As Boolean = WriteToXml(watermarkFunctionTemplate, xmlFilePath)
                End If

                If getfromXml AndAlso xmlFilePath <> "" Then
                    ' Create a RasterFunctionTemplate object from the serialized xml. 
                    Dim serializedObj As Object = ReadFromXml(xmlFilePath)
                    If TypeOf serializedObj Is IRasterFunctionTemplate Then
                        Console.WriteLine("Success.")
                    Else
                        Console.WriteLine("Failed.")
                    End If
                End If

                Console.WriteLine("Press any key...")
                Console.ReadKey()
                aoInit.Shutdown()
            Catch exc As Exception
                Console.WriteLine("Exception Caught in Main: " & exc.Message)
                Console.WriteLine("Failed.")
                Console.WriteLine("Press any key...")
                Console.ReadKey()
                aoInit.Shutdown()
            End Try
        End Sub

        Public Shared Function AddWatermarkToRD(ByVal RasterDataset As IRasterDataset, ByVal OutputFolder As String, ByVal OutputName As String, ByVal watermarkImagePath As String, ByVal blendPercentage As Double, ByVal watermarklocation As esriWatermarkLocation) As Boolean
            Try
                ' Create Watermark Function
                Dim rasterFunction As IRasterFunction = New WatermarkFunction()
                ' Create the Watermark Function Arguments object
                Dim rasterFunctionArguments As IWatermarkFunctionArguments = New WatermarkFunctionArguments()
                ' Set the WatermarkImagePath
                rasterFunctionArguments.WatermarkImagePath = watermarkImagePath
                ' the blending percentage,
                rasterFunctionArguments.BlendPercentage = blendPercentage
                ' and the watermark location.
                rasterFunctionArguments.WatermarkLocation = watermarklocation
                ' Set the Raster Dataset as the input raster
                rasterFunctionArguments.Raster = RasterDataset

                ' Create Function Dataset
                Dim functionRasterDataset As IFunctionRasterDataset = New FunctionRasterDataset()
                ' Create a Function Raster Dataset Name object
                Dim functionRasterDatasetName As IFunctionRasterDatasetName = DirectCast(New FunctionRasterDatasetName(), IFunctionRasterDatasetName)
                ' Set the path for the output Function Raster Dataset
                functionRasterDatasetName.FullName = System.IO.Path.Combine(OutputFolder, OutputName)
                functionRasterDataset.FullName = DirectCast(functionRasterDatasetName, IName)
                ' Initialize the Function Raster Dataset with the function and 
                ' its arguments object
                functionRasterDataset.Init(rasterFunction, rasterFunctionArguments)

                ' Save as Function Raster Dataset as an .afr file
                Dim myTempDset As ITemporaryDataset = DirectCast(functionRasterDataset, ITemporaryDataset)
                myTempDset.MakePermanent()

                Console.WriteLine("Generated " & OutputName & ".")
                Console.WriteLine("Success.")
                Return True
            Catch exc As Exception
                Console.WriteLine("Exception Caught while adding watermark to Raster Dataset: " & exc.Message)
                Console.WriteLine("Failed.")
                Return False
            End Try
        End Function

        Public Shared Function AddWatermarkDataToMD(ByVal MDWorkspaceFolder As String, ByVal MDName As String, ByVal watermarkImagePath As String, ByVal blendPercentage As Double, ByVal watermarklocation As esriWatermarkLocation, ByVal clearFunctions As Boolean) As Boolean
            Try
                ' Open MD
                Dim factoryType As Type = Type.GetTypeFromProgID("esriDataSourcesGDB.FileGDBWorkspaceFactory")
                Dim mdWorkspaceFactory As IWorkspaceFactory = DirectCast(Activator.CreateInstance(factoryType), IWorkspaceFactory)
                Dim mdWorkspace As IWorkspace = mdWorkspaceFactory.OpenFromFile(MDWorkspaceFolder, 0)
                Dim workspaceEx As IRasterWorkspaceEx = DirectCast(mdWorkspace, IRasterWorkspaceEx)
                Dim mosaicDataset As IMosaicDataset = DirectCast(workspaceEx.OpenRasterDataset(MDName), IMosaicDataset)

                If clearFunctions Then
                    ' Clear functions already added to MD.
                    mosaicDataset.ClearFunction()
                End If

                ' Create Watermark Function
                Dim rasterFunction As IRasterFunction = New WatermarkFunction()
                ' Create the Watermark Function Arguments object
                Dim rasterFunctionArguments As IWatermarkFunctionArguments = New WatermarkFunctionArguments()
                ' Set the WatermarkImagePath
                rasterFunctionArguments.WatermarkImagePath = watermarkImagePath
                ' the blending percentage,
                rasterFunctionArguments.BlendPercentage = blendPercentage
                ' and the watermark location.
                rasterFunctionArguments.WatermarkLocation = watermarklocation

                ' Add function to MD.
                ' This function takes the name of the property corresponding to the Raster 
                ' property of the Arguments object (in this case is it called Raster itself: 
                ' rasterFunctionArguments.Raster) as its third argument.
                mosaicDataset.ApplyFunction(rasterFunction, rasterFunctionArguments, "Raster")

                Console.WriteLine("Added Watermark to MD: " & MDName & ".")
                Console.WriteLine("Success.")
                Return True
            Catch exc As Exception
                Console.WriteLine("Exception Caught while adding watermark to MD: " & exc.Message)
                Console.WriteLine("Failed.")
                Return False
            End Try
        End Function

        Public Shared Function CreateWatermarkTemplate(ByVal watermarkImagePath As String, ByVal blendPercentage As Double, ByVal watermarklocation As esriWatermarkLocation) As IRasterFunctionTemplate
            '#Region "Setup Raster Function Vars"
            Dim watermarkRasterRFV As IRasterFunctionVariable = New RasterFunctionVariableClass()
            watermarkRasterRFV.Name = "Raster"
            watermarkRasterRFV.IsDataset = True
            Dim watermarkImagePathRFV As IRasterFunctionVariable = New RasterFunctionVariableClass()
            watermarkImagePathRFV.Name = "WatermarkImagePath"
            watermarkImagePathRFV.Value = watermarkImagePath
            watermarkImagePathRFV.IsDataset = False
            Dim watermarkBlendPercRFV As IRasterFunctionVariable = New RasterFunctionVariableClass()
            watermarkBlendPercRFV.Name = "BlendPercentage"
            watermarkBlendPercRFV.Value = blendPercentage
            Dim watermarkLocationRFV As IRasterFunctionVariable = New RasterFunctionVariableClass()
            watermarkLocationRFV.Name = "Watermarklocation"
            watermarkLocationRFV.Value = watermarklocation
            '#End Region

            '#Region "Setup Raster Function Template"
            ' Create the Watermark Function Arguments object
            Dim rasterFunctionArguments As IRasterFunctionArguments = New WatermarkFunctionArguments()
            ' Set the WatermarkImagePath
            rasterFunctionArguments.PutValue("WatermarkImagePath", watermarkImagePathRFV)
            ' the blending percentage,
            rasterFunctionArguments.PutValue("BlendPercentage", watermarkBlendPercRFV)
            ' and the watermark location.
            rasterFunctionArguments.PutValue("WatermarkLocation", watermarkLocationRFV)
            ' Set the Raster Dataset as the input raster
            rasterFunctionArguments.PutValue("Raster", watermarkRasterRFV)

            Dim watermarkFunction As IRasterFunction = New WatermarkFunction()
            Dim watermarkFunctionTemplate As IRasterFunctionTemplate = New RasterFunctionTemplateClass()
            watermarkFunctionTemplate.[Function] = watermarkFunction
            watermarkFunctionTemplate.Arguments = rasterFunctionArguments
            '#End Region

            Return watermarkFunctionTemplate
        End Function

        Public Shared Function WriteToXml(ByVal inputDataset As Object, ByVal xmlFilePath As String) As Boolean
            Try
                ' Check if file exists
                If File.Exists(xmlFilePath) Then
                    Console.WriteLine("File already exists.")
                    Return False
                End If
                ' Create new file.
                Dim xmlFile As IFile = New FileStreamClass()
                xmlFile.Open(xmlFilePath, esriFilePermission.esriReadWrite)
                ' See if the input dataset can be Xml serialized.
                Dim mySerializeData As IXMLSerialize = DirectCast(inputDataset, IXMLSerialize)
                ' Create new XmlWriter object.
                Dim myXmlWriter As IXMLWriter = New XMLWriterClass()
                myXmlWriter.WriteTo(DirectCast(xmlFile, IStream))
                myXmlWriter.WriteXMLDeclaration()
                Dim myXmlSerializer As IXMLSerializer = New XMLSerializerClass()
                ' Write to XML File
                myXmlSerializer.WriteObject(myXmlWriter, Nothing, Nothing, Nothing, Nothing, mySerializeData)
                Console.WriteLine("Success.")
                Return True
            Catch exc As Exception
                Console.WriteLine("Exception caught in WriteToXml: " & exc.Message)
                Console.WriteLine("Failed.")
                Return False
            End Try
        End Function

        Public Shared Function ReadFromXml(ByVal xmlFilePath As String) As Object
            Try
                Dim inputXmlFile As IFile = New FileStreamClass()
                inputXmlFile.Open(xmlFilePath, esriFilePermission.esriReadWrite)
                Dim myXmlReader As IXMLReader = New XMLReaderClass()
                myXmlReader.ReadFrom(DirectCast(inputXmlFile, IStream))
                Dim myInputXmlSerializer As IXMLSerializer = New XMLSerializerClass()
                Dim myFunctionObject As Object = myInputXmlSerializer.ReadObject(myXmlReader, Nothing, Nothing)
                Return myFunctionObject
            Catch exc As Exception
                Console.WriteLine("Exception caught in ReadFromXml: " & exc.Message)
                Return Nothing
            End Try
        End Function
    End Class
End Namespace
