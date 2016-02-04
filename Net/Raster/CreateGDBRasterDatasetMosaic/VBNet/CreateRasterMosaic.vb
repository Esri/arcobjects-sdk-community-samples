'Copyright 2016 Esri

'Licensed under the Apache License, Version 2.0 (the "License");
'you may not use this file except in compliance with the License.
'You may obtain a copy of the License at

'    http://www.apache.org/licenses/LICENSE-2.0

'Unless required by applicable law or agreed to in writing, software
'distributed under the License is distributed on an "AS IS" BASIS,
'WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.

'See the License for the specific language governing permissions and
'limitations under the License.
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.DataManagementTools
Imports ESRI.ArcGIS.Geoprocessor
Imports Microsoft.Win32

Module CreateRasterDatasets

    'Set variables, you can substitute the paths with your data location
    'Remove temp.gdb in TEMP directory if it exists
    'The output is written to TEMP directory in temp.gdb file geodatabase

    Private outputFolder As String = "C:\Temp"
    Private outFGDB As String = "temp.gdb"
    Private FGDBPath As String = outputFolder + "\" + outFGDB
    Private rasterFolder As String = "C:\data"
    Private dsName As String = "mosaic"

    Sub Main(ByVal args As String())
        'If creating a raster dataset in ArcSDE, it will need Standard or Advanced License
        Dim aoInit As ESRI.ArcGIS.esriSystem.AoInitialize = Nothing
        Try
            ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.Desktop)
            aoInit = New AoInitializeClass()
            Dim licStatus As esriLicenseStatus = aoInit.Initialize(esriLicenseProductCode.esriLicenseProductCodeAdvanced)
            Console.WriteLine("License Checkout successful.")
        Catch exc As Exception
            ' If it fails at this point, shutdown the test and ignore any subsequent errors.
            Console.WriteLine(exc.Message)
        End Try
        Try

            'Creates an empty raster dataset
            'Make sure parameters of the empty raster dataset match our data (number of bands, bit depth, etc.)
            CreateRasterDS()

            'Loads rasters in the input folder to the new raster dataset
            LoadDirToRasterDataset(FGDBPath & "\" & dsName, rasterFolder)
        Catch exc As Exception
            ' If it fails at this point, shutdown the test and ignore any subsequent errors.
            Console.WriteLine(exc.Message)
        End Try

        Console.Write("Please press any key to close the application.")
        Console.ReadKey()

        'Do not make any call to ArcObjects after ShutDown()
        aoInit.Shutdown()
    End Sub

    'Creates raster dataset using GP CreateRasterDataset class
    Sub CreateRasterDS()
        Try
            'Initialize GeoProcessor
            Dim geoProcessor As New Geoprocessor()

            'Create file geodatabase 
            Dim createFileGDB As New CreateFileGDB()
            createFileGDB.out_folder_path = outputFolder
            createFileGDB.out_name = outFGDB

            geoProcessor.Execute(createFileGDB, Nothing)

            'Create a Raster Dataset 
            Dim createRasterDataset As New CreateRasterDataset()

            'Set parameters
            'Set output location and name
            createRasterDataset.out_name = dsName
            createRasterDataset.out_path = FGDBPath

            'Set number of band to 3
            createRasterDataset.number_of_bands = 3

            'Set pixel type to unsigned 8 bit integer
            createRasterDataset.pixel_type = "8_BIT_UNSIGNED"

            'Build pyramid layers with GDB calculated number of levels
            createRasterDataset.pyramids = "PYRAMIDS -1 BILINEAR"

            'Set GDB dataset properties
            'Set JPEG compression of quality 50
            createRasterDataset.compression = "JPEG 50"

            'Set pyramid origin point so it takes advantage of partial pyramid building when mosaicking
            'Need to make sure that any raster that will be mosaicked is to the southeast of this point
            'If the rasters are in GCS, the following origin point is good.
            'createRasterDataset.pyramid_origin = "-180 90";

            'Execute the tool to create a raster dataset
            geoProcessor.Execute(createRasterDataset, Nothing)
            ReturnMessages(geoProcessor)
        Catch exc As Exception
            Console.WriteLine(exc.Message)
        End Try
    End Sub

    'GP message handling
    Sub ReturnMessages(ByVal gp As Geoprocessor)
        If gp.MessageCount > 0 Then
            For Count As Integer = 0 To gp.MessageCount - 1
                System.Console.WriteLine(gp.GetMessage(Count))
            Next
        End If
    End Sub

    Sub LoadDirToRasterDataset(ByVal outRasterDataset As String, ByVal inputDir As String)
        Try
            'Initialize GeoProcessor
            Dim geoProcessor As New Geoprocessor()

            'Mosaic the works
            Dim wsToRasterDataset As New WorkspaceToRasterDataset()

            'Set input folder
            wsToRasterDataset.in_workspace = inputDir

            'Set target GDB raster dataset
            wsToRasterDataset.in_raster_dataset = outRasterDataset

            'Include rasters in the subdirectories
            wsToRasterDataset.include_subdirectories = "INCLUDE_SUBDIRECTORIES"

            'Set mosaic mode
            wsToRasterDataset.mosaic_type = "LAST"

            'Set colormap mode
            wsToRasterDataset.colormap = "MATCH"

            'Set background value
            wsToRasterDataset.background_value = 0

            'Execute the tool to load rasters in the directory to raster dataset
            geoProcessor.Execute(wsToRasterDataset, Nothing)
            ReturnMessages(geoProcessor)
        Catch exc As Exception
            Console.WriteLine(exc.Message)
        End Try
    End Sub

End Module
