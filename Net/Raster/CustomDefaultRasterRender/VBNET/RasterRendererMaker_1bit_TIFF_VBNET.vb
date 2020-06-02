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
' IMPORTANT INFORMATION:
' ======================
' This project already has the COM Component Category Registration information embedded. If you build this project it will 
' overwrite ArcMap's default behavior for rendering of 1bit TIFF images. Since this project alters the default behavior it 
' may be desirable to remove this projects functionality from the system to restore the default image rendering capabilities.
' This can be accomplished by:
' 1. Open a Visual Studio 2008 Command Prompt and navigate to the location of this projects .dll that gets created (for 
' example: C:\temp\RasterRendererMaker_1bit_TIFF_VBNET\bin\Debug)
' 2. Use the esriregasm.exe utility from the Program Files\Common Files\ArcGIS\bin to unregister the DLL.

Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.ADF.CATIDs
Imports System.Runtime.InteropServices
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.DataSourcesRaster
Imports ESRI.ArcGIS.Display
Imports ESRI.ArcGIS.Geodatabase

<ComClass(RasterRenderMaker_1bit_TIFF_VBNET.ClassId, RasterRenderMaker_1bit_TIFF_VBNET.InterfaceId, RasterRenderMaker_1bit_TIFF_VBNET.EventsId), _
 ProgId("RasterRendererMaker_1bit_TIFF_VBNET.RasterRenderMaker_1bit_TIFF_VBNET")> _
Public Class RasterRenderMaker_1bit_TIFF_VBNET
  Implements IRasterRendererMaker


#Region "COM Registration Function(s)"
  <ComRegisterFunction(), ComVisibleAttribute(False)> _
  Public Shared Sub RegisterFunction(ByVal registerType As Type)
    ' Required for ArcGIS Component Category Registrar support
    ArcGISCategoryRegistration(registerType)

    'Add any COM registration code after the ArcGISCategoryRegistration() call

  End Sub

  <ComUnregisterFunction(), ComVisibleAttribute(False)> _
  Public Shared Sub UnregisterFunction(ByVal registerType As Type)
    ' Required for ArcGIS Component Category Registrar support
    ArcGISCategoryUnregistration(registerType)

    'Add any COM unregistration code after the ArcGISCategoryUnregistration() call

  End Sub

#Region "ArcGIS Component Category Registrar generated code"
  ''' <summary>
  ''' Required method for ArcGIS Component Category registration -
  ''' Do not modify the contents of this method with the code editor.
  ''' </summary>
  Private Shared Sub ArcGISCategoryRegistration(ByVal registerType As Type)
    Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
    RasterRendererMakers.Register(regKey)

  End Sub
  ''' <summary>
  ''' Required method for ArcGIS Component Category unregistration -
  ''' Do not modify the contents of this method with the code editor.
  ''' </summary>
  Private Shared Sub ArcGISCategoryUnregistration(ByVal registerType As Type)
    Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
    RasterRendererMakers.Unregister(regKey)

  End Sub

#End Region
#End Region

#Region "COM GUIDs"
  ' These  GUIDs provide the COM identity for this class 
  ' and its COM interfaces. If you change them, existing 
  ' clients will no longer be able to access the class.
  Public Const ClassId As String = "ca7cea8d-e4b7-43cc-851b-c2b120c2fb57"
  Public Const InterfaceId As String = "a2246ac9-db33-4677-b9a8-24729374dfa6"
  Public Const EventsId As String = "37166b0a-a607-499c-824f-45cd09868df6"
#End Region

  ' A creatable COM class must have a Public Sub New() 
  ' with no parameters, otherwise, the class will not be 
  ' registered in the COM registry and cannot be created 
  ' via CreateObject.
  Public Sub New()
    MyBase.New()
  End Sub

  Public Function CreateDefaultRasterRenderer(ByVal pRaster As ESRI.ArcGIS.Geodatabase.IRaster) As ESRI.ArcGIS.Carto.IRasterRenderer Implements ESRI.ArcGIS.Carto.IRasterRendererMaker.CreateDefaultRasterRenderer

    'Get raster dataset
    Dim rasterBandCollection As IRasterBandCollection = CType(pRaster, IRasterBandCollection)
    Dim rasterBand As IRasterBand = rasterBandCollection.Item(0)
    Dim rasterDataset As IRasterDataset = CType(rasterBand, IRasterDataset)

    'Check for TIFF format
    Dim format_Renamed As String = rasterDataset.Format
    If Left(format_Renamed, 4) <> "TIFF" Then
      Return Nothing
    End If

    'check for bit depth
    Dim rasterProps As IRasterProps = CType(rasterBand, IRasterProps)
    If rasterProps.PixelType <> rstPixelType.PT_U1 Then
      Return Nothing
    End If

    'create renderer for 1 bit raster
        'Create a unique value renderer and associate it with raster
    Dim rasterUniqueValueRenderer As IRasterUniqueValueRenderer = New RasterUniqueValueRendererClass
    Dim rasterRenderer As IRasterRenderer = CType(rasterUniqueValueRenderer, IRasterRenderer)
    rasterRenderer.Raster = pRaster
    rasterRenderer.Update()

    'Define the renderer
    rasterUniqueValueRenderer.HeadingCount = 1
    rasterUniqueValueRenderer.Heading(0) = ""
    rasterUniqueValueRenderer.ClassCount(0) = 2
    rasterUniqueValueRenderer.Field = "VALUE"
    rasterUniqueValueRenderer.AddValue(0, 0, 0)
    rasterUniqueValueRenderer.AddValue(0, 1, 1)
    rasterUniqueValueRenderer.Label(0, 0) = "0"
    rasterUniqueValueRenderer.Label(0, 1) = "1"

    ' Define symbology for rendering value 0
    Dim color1 As IColor = CType(CreateRGBColor(200, 50, 0), IColor) 'Brown color

    Dim simpleFillSymbol1 As ISimpleFillSymbol = New SimpleFillSymbolClass
    simpleFillSymbol1.Color = color1
    rasterUniqueValueRenderer.Symbol(0, 0) = CType(simpleFillSymbol1, ISymbol)

    Dim color2 As IColor = New RgbColorClass
    color2.NullColor = True

    Dim simpleFillSymbol2 As ISimpleFillSymbol = New SimpleFillSymbolClass
    simpleFillSymbol2.Color = color2

    rasterUniqueValueRenderer.Symbol(0, 1) = CType(simpleFillSymbol2, ISymbol)
    Return rasterRenderer

  End Function

  Public ReadOnly Property Priority() As Integer Implements ESRI.ArcGIS.Carto.IRasterRendererMaker.Priority
    Get
      'Give it a higher priority, so this renderer will be used as default
      Return 99
    End Get
  End Property

#Region "Create RGBColor"

  '''<summary>Generate an RgbColor by specifying the amount of Red, Green and Blue.</summary>
  ''' 
  '''<param name="myRed">A byte (0 to 255) used to represent the Red color. Example: 0</param>
  '''<param name="myGreen">A byte (0 to 255) used to represent the Green color. Example: 255</param>
  '''<param name="myBlue">A byte (0 to 255) used to represent the Blue color. Example: 123</param>
  '''  
  '''<returns>An IRgbColor interface</returns>
  '''  
  '''<remarks></remarks>
  Public Function CreateRGBColor(ByVal myRed As System.Byte, ByVal myGreen As System.Byte, ByVal myBlue As System.Byte) As IRgbColor

    Dim rgbColor As IRgbColor = New RgbColorClass
    With rgbColor
      .Red = CInt(myRed)
      .Green = CInt(myGreen)
      .Blue = CInt(myBlue)
      .UseWindowsDithering = True
    End With

    Return rgbColor

  End Function
#End Region

End Class


