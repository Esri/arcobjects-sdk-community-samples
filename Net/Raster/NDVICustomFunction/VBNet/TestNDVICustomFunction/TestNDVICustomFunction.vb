'Copyright 2019 Esri

'Licensed under the Apache License, Version 2.0 (the "License");
'you may not use this file except in compliance with the License.
'You may obtain a copy of the License at

'    http://www.apache.org/licenses/LICENSE-2.0

'Unless required by applicable law or agreed to in writing, software
'distributed under the License is distributed on an "AS IS" BASIS,
'WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.

'See the License for the specific language governing permissions and
'limitations under the License.
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.IO
Imports ESRI.ArcGIS
Imports ESRI.ArcGIS.DataSourcesGDB
Imports ESRI.ArcGIS.DataSourcesRaster
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Geometry
Imports NDVIFunction.CustomFunction

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

Public Class TestNDVICustomFunction
    <STAThread()> _
    Public Shared Sub Main(ByVal args As String())
        '#Region "Initialize License"
        Dim aoInit As ESRI.ArcGIS.esriSystem.AoInitialize
        Try
            Console.WriteLine("Obtaining license")
            ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.Desktop)
            aoInit = New AoInitialize()
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
            ' Create NDVI Custom Function Raster Dataset
            Dim addToRD As Boolean = True
            ' Add NDVI Custom Function to MD
            Dim addToMD As Boolean = False
            ' Serialize a template form of the NDVI Custom Funtion to Xml.
            Dim writeTemplateToXml As Boolean = False
            ' Get a template object back from its serialized xml.
            Dim getfromXml As Boolean = False

            '#Region "Specify inputs."
            ' Raster Dataset parameters
            Dim workspaceFolder As String = "c:\temp"
            Dim rasterDatasetName As String = "Dubai_ov.tif"
            ' Output parameters for Function Raster Dataset
            Dim outputFolder As String = "c:\temp"
            Dim outputName As String = "NDVICustomFunctionSample.afr"

            ' Mosaic dataset parameters
            ' GDB containing the Mosaic Dataset
            Dim mdWorkspaceFolder As String = "c:\temp\testGdb.gdb"
            ' Name of the mosaic dataset
            Dim mdName As String = "testMD"

            ' NDVI Custom Function Parameters
            Dim bandIndices As String = "4 3"

            ' Xml file path to save to or read from xml.
            Dim xmlFilePath As String = "e:\Dev\Samples CSharp\CustomRasterFunction\Xml\NDVICustomAFR.xml"
            '#End Region

            If addToRD Then
                ' Open the Raster Dataset
                Dim factoryType As Type = Type.GetTypeFromProgID("esriDataSourcesRaster.RasterWorkspaceFactory")
                Dim workspaceFactory As IWorkspaceFactory = DirectCast(Activator.CreateInstance(factoryType), IWorkspaceFactory)
                Dim rasterWorkspace As IRasterWorkspace = DirectCast(workspaceFactory.OpenFromFile(workspaceFolder, 0), IRasterWorkspace)
                Dim rasterDataset As IRasterDataset = rasterWorkspace.OpenRasterDataset(rasterDatasetName)
                AddNDVICustomToRD(rasterDataset, outputFolder, outputName, bandIndices)
                ' Cleanup
                workspaceFactory = Nothing
                rasterWorkspace = Nothing
                rasterDataset = Nothing
            End If

            If addToMD Then
                AddNDVICustomDataToMD(mdWorkspaceFolder, mdName, bandIndices, True)
            End If


            If writeTemplateToXml AndAlso xmlFilePath <> "" Then
                ' Create a template with the NDVI Custom Function.
                Dim ndviCustomFunctionTemplate As IRasterFunctionTemplate = CreateNDVICustomTemplate(bandIndices)
                ' Serialize the template to an xml file.
                Dim status As Boolean = WriteToXml(ndviCustomFunctionTemplate, xmlFilePath)
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

    Public Shared Function AddNDVICustomToRD(ByVal RasterDataset As IRasterDataset, ByVal OutputFolder As String, ByVal OutputName As String, ByVal bandIndices As String) As Boolean
        Try
            ' Create NDVI Custom Function
            Dim rasterFunction As IRasterFunction = New NDVICustomFunction()
            ' Create the NDVI Custom Function Arguments object
            Dim rasterFunctionArguments As INDVICustomFunctionArguments = New NDVICustomFunctionArguments()
            ' Set the Band Indices
            rasterFunctionArguments.BandIndices = bandIndices

            ' Set the RasterDataset as the input raster
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
            Console.WriteLine("Exception Caught while adding NDVI Custom Function to Raster Dataset: " & exc.Message)
            Console.WriteLine("Failed.")
            Return False
        End Try
    End Function

    Public Shared Function AddNDVICustomDataToMD(ByVal MDWorkspaceFolder As String, ByVal MDName As String, ByVal bandIndices As String, ByVal clearFunctions As Boolean) As Boolean
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

            ' Create NDVI Custom Function
            Dim rasterFunction As IRasterFunction = New NDVICustomFunction()
            ' Create the NDVI Custom Function Arguments object
            Dim rasterFunctionArguments As INDVICustomFunctionArguments = New NDVICustomFunctionArguments()
            ' Set the Band Indices
            rasterFunctionArguments.BandIndices = bandIndices

            ' Add function to MD.
            ' This function takes the name of the property corresponding to the Raster 
            ' property of the Arguments object (in this case is it called Raster itself: 
            ' rasterFunctionArguments.Raster) as its third argument.
            mosaicDataset.ApplyFunction(rasterFunction, rasterFunctionArguments, "Raster")

            Console.WriteLine("Added NDVI Custom Function to MD: " & MDName & ".")
            Console.WriteLine("Success.")
            Return True
        Catch exc As Exception
            Console.WriteLine("Exception Caught while adding NDVI Custom Function to MD: " & exc.Message)
            Console.WriteLine("Failed.")
            Return False
        End Try
    End Function

    Public Shared Function CreateNDVICustomTemplate(ByVal bandIndices As String) As IRasterFunctionTemplate
        '#Region "Setup Raster Function Vars"
        Dim watermarkRasterRFV As IRasterFunctionVariable = New RasterFunctionVariable()
        watermarkRasterRFV.Name = "Raster"
        watermarkRasterRFV.IsDataset = True
        Dim bandIndicesRFV As IRasterFunctionVariable = New RasterFunctionVariable()
        bandIndicesRFV.Name = "BandIndices"
        bandIndicesRFV.Value = bandIndices
        bandIndicesRFV.IsDataset = False
        '#End Region

        '#Region "Setup Raster Function Template"
        ' Create the NDVI Custom Function Arguments object
        Dim rasterFunctionArguments As IRasterFunctionArguments = New NDVICustomFunctionArguments()
        ' Set the Band Indices
        rasterFunctionArguments.PutValue("BandIndices", bandIndicesRFV)
        ' Set the Raster Dataset as the input raster
        rasterFunctionArguments.PutValue("Raster", watermarkRasterRFV)
        ' Create the NDVI Custom Function
        Dim ndviCustomFunction As IRasterFunction = New NDVICustomFunction()

        Dim ndviCustomFunctionTemplate As IRasterFunctionTemplate = New RasterFunctionTemplate()
        ndviCustomFunctionTemplate.[Function] = ndviCustomFunction
        ndviCustomFunctionTemplate.Arguments = rasterFunctionArguments
        '#End Region

        Return ndviCustomFunctionTemplate
    End Function

    Public Shared Function WriteToXml(ByVal inputDataset As Object, ByVal xmlFilePath As String) As Boolean
        Try
            ' Check if file exists
            If File.Exists(xmlFilePath) Then
                Console.WriteLine("File already exists.")
                Return False
            End If
            ' Create new file.
            Dim xmlFile As IFile = New ESRI.ArcGIS.esriSystem.FileStream()
            xmlFile.Open(xmlFilePath, esriFilePermission.esriReadWrite)
            ' See if the input dataset can be Xml serialized.
            Dim mySerializeData As IXMLSerialize = DirectCast(inputDataset, IXMLSerialize)
            ' Create new XmlWriter object.
            Dim myXmlWriter As IXMLWriter = New XMLWriter()
            myXmlWriter.WriteTo(DirectCast(xmlFile, IStream))
            myXmlWriter.WriteXMLDeclaration()
            Dim myXmlSerializer As IXMLSerializer = New XMLSerializer()
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
            Dim inputXmlFile As IFile = New ESRI.ArcGIS.esriSystem.FileStream()
            inputXmlFile.Open(xmlFilePath, esriFilePermission.esriReadWrite)
            Dim myXmlReader As IXMLReader = New XMLReader()
            myXmlReader.ReadFrom(DirectCast(inputXmlFile, IStream))
            Dim myInputXmlSerializer As IXMLSerializer = New XMLSerializer()
            Dim myFunctionObject As Object = myInputXmlSerializer.ReadObject(myXmlReader, Nothing, Nothing)
            Return myFunctionObject
        Catch exc As Exception
            Console.WriteLine("Exception caught in ReadFromXml: " & exc.Message)
            Return Nothing
        End Try
    End Function
End Class
