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
Imports Microsoft.Win32
Imports ESRI.ArcGIS.DataSourcesRaster
Imports ESRI.ArcGIS.DataSourcesGDB
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS

'Tests the custom NodataFilter
Module Module1
  Sub Main()
    'Initialize the license
    Dim aoInit As ESRI.ArcGIS.esriSystem.AoInitialize = Nothing
    Try
      ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.Desktop)
      aoInit = New AoInitializeClass()
      Dim licStatus As esriLicenseStatus = aoInit.Initialize(esriLicenseProductCode.esriLicenseProductCodeBasic)
      Console.WriteLine("License checkout successful.")
    Catch exc As Exception
      ' If it fails at this point, shutdown the test and ignore any subsequent errors.
      Console.WriteLine(exc.Message)
    End Try
    Try
      'Get the location for data installed with .net sdk
      Dim runtimeVersion As String = RuntimeManager.ActiveRuntime.Version
      Dim regKey As RegistryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\ESRI\ArcObjectsSDK" + runtimeVersion + "\.NET")
      Dim path As String = System.Convert.ToString(regKey.GetValue("MainDir"))
      Dim rasterFolder As String = System.IO.Path.Combine(path, "Samples\ArcObjectsNET\CustomNodataFilter")

      Dim raster As IPixelOperation = CType(OpenRasterDataset(rasterFolder, "testimage.tif"), IPixelOperation)

      If raster Is Nothing Then
        Console.WriteLine("invalid raster")
        Return
      End If

      'create nodatafilter and set properties
      Dim nFilter As CustomFilter_VB.INodataFilter = New CustomFilter_VB.NodataFilter()

      'filter out all values between 0 and 50 as nodata
      nFilter.MinNodataValue = 0
      nFilter.MaxNodataValue = 50


      'set nodata value using the minimum of the nodata range
      Dim rasterProps As IRasterProps
      rasterProps = CType(raster, IRasterProps)
      rasterProps.NoDataValue = 0

      'apply the convolutionfilter to raster
      raster.PixelFilter = nFilter

      'save the filtered raster to a new raster dataset in TEMP directory
      Dim saveAs As ISaveAs = CType(raster, ISaveAs)
      Dim workspace As IWorkspace = OpenWorkspace(rasterFolder)
      saveAs.SaveAs("nodataVB.tif", workspace, "IMAGINE Image")

      Console.WriteLine("Completed")
      Console.ReadLine()

    Catch e As Exception
      Console.WriteLine(e.Message)
    End Try
    'Do not make any call to ArcObjects after ShutDown()
    aoInit.Shutdown()
  End Sub

  'Open raster dataset and get raster
  Function OpenRasterDataset(ByVal path As String, ByVal datasetName As String) As IRaster
    Dim rasterWorkspace As IRasterWorkspace = CType(OpenWorkspace(path), IRasterWorkspace)

    If rasterWorkspace Is Nothing Then
      Return Nothing
    End If

    Dim rasterDataset As IRasterDataset2
    rasterDataset = CType(rasterWorkspace.OpenRasterDataset(datasetName), IRasterDataset2)

    If rasterDataset Is Nothing Then
      Return Nothing
    End If

    Return rasterDataset.CreateFullRaster()
  End Function

  'Open file based raster workspace
  Function OpenWorkspace(ByVal path As String) As IWorkspace
    Dim workspaceFactory As IWorkspaceFactory = New RasterWorkspaceFactoryClass()
    Dim rasterWorkspace As IWorkspace = workspaceFactory.OpenFromFile(path, 0)

    Return rasterWorkspace

  End Function
End Module

