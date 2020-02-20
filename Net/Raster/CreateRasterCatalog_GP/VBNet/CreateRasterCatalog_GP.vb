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

Imports System
Imports System.Collections.Generic
Imports System.Text
Imports ESRI.ArcGIS.DataSourcesGDB
Imports ESRI.ArcGIS.DataSourcesRaster
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.GeomeTry
Imports ESRI.ArcGIS.Geoprocessing
Imports ESRI.ArcGIS.DataManagementTools
Imports ESRI.ArcGIS.Geoprocessor
Imports ESRI.ArcGIS.esriSystem


Module CreateRasterCatalog1

    'Set variables
    Private sdePath As String = "Database Connections\Connection to tiny.sde"
    Private rasterFolder As String = "c:\temp\review"
    Private catalogName As String = "rc_3"
    Private outRC As String = sdePath + "\" + catalogName

    Sub Main(ByVal args() As String)

        'Initialize the license
        Dim aoInit As ESRI.ArcGIS.esriSystem.AoInitialize = Nothing
        Try
            ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.Desktop)
            aoInit = New AoInitializeClass()
            Dim licStatus As esriLicenseStatus = aoInit.Initialize(esriLicenseProductCode.esriLicenseProductCodeStandard)
            Console.WriteLine("License checkout successful.")
        Catch exc As Exception
            ' If it fails at this point, shutdown the test and ignore any subsequent errors.
            Console.WriteLine(exc.Message)
        End Try

        'Coordinate system for raster column 
        Dim rSR As IGPCoordinateSystem = New GPCoordinateSystemClass()
        rSR.SpatialReference = CreateSpatialReference(CInt(esriSRProjCSType.esriSRProjCS_World_Mercator))
        'Coordinate system for geometry column 
        Dim gSR As IGPSpatialReference = New GPSpatialReferenceClass()
        gSR.SpatialReference = CreateSpatialReference(CInt(esriSRProjCSType.esriSRProjCS_World_Mercator))

        'Creates raster catalog 
        CreateRasterCatalog_GP(rSR, gSR)

        'Loads rasters in the given directory to raster catalog 
        LoadDirtoRasterCatalog(outRC, rasterFolder)
        System.Console.WriteLine("Loading completed")
        System.Console.ReadLine() ' click a key to finish

        'Do not make any call to ArcObjects after Shutdown() 
        aoInit.Shutdown()
    End Sub
    Private Sub CreateRasterCatalog_GP(ByVal rasterCoordSys As Object, ByVal geometryCoordsys As Object)
        'Initialize GeoProcessor 
        Dim geoProcessor As New ESRI.ArcGIS.Geoprocessor.Geoprocessor()

        'CreateRasterCatalog GP tool 
        Dim createRasterCatalog As New CreateRasterCatalog()

        'Set parameters 
        createRasterCatalog.out_path = sdePath
        createRasterCatalog.out_name = catalogName
        createRasterCatalog.raster_spatial_reference = rasterCoordSys
        createRasterCatalog.spatial_reference = geometryCoordsys

        'Execute the tool to create a raster catalog 
        geoProcessor.Execute(createRasterCatalog, Nothing)
        ReturnMessages(geoProcessor)
    End Sub
    'GP message handling
    Private Sub ReturnMessages(ByVal gp As ESRI.ArcGIS.Geoprocessor.Geoprocessor)
        If gp.MessageCount > 0 Then
            Dim Count As Integer
            For Count = 0 To gp.MessageCount - 1 Step Count + 1
                Console.WriteLine(gp.GetMessage(Count))
            Next
        End If
    End Sub

    Private Sub LoadDirToRasterCatalog(ByVal outRasterCatalog As String, ByVal inputDir As String)
        'Initialize GeoProcessor 
        Dim geoProcessor As New ESRI.ArcGIS.Geoprocessor.Geoprocessor()

        'Set parameters 
        Dim parameters As IVariantArray = New VarArrayClass()

        'Set input folder 
        parameters.Add(inputDir)

        'Set target GDB raster catalog 
        parameters.Add(outRasterCatalog)

        'Execute the tool to load rasters in the directory to raster catalog 
        geoProcessor.Execute("WorkspaceToRasterCatalog", parameters, Nothing)
        ReturnMessages(geoProcessor)
    End Sub

    'Create a spatial reference with given factory code
    Private Function CreateSpatialReference(ByVal code As Integer) As ISpatialReference
        Dim spatialReferenceFact As ISpatialReferenceFactory2 = New SpatialReferenceEnvironmentClass()
        Try
            Return spatialReferenceFact.CreateSpatialReference(code)
        Catch
            Return New UnknownCoordinateSystemClass()
        End Try
    End Function
End Module

