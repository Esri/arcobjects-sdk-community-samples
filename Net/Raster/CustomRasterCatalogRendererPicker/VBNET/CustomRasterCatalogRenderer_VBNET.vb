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
' IMPORTANT INFORMATION:
' ======================
' This project already has the COM Component Category Registration information embedded. If you build this project it will 
' overwrite ArcMap's default behavior for rendering of RasterCatalogRenders. Since this project alters the default behavior it 
' may be desirable to remove this projects functionality from the system to restore the default image rendering capabilities.
' This can be accomplished by:
' 1. Open a Visual Studio 2008 Command Prompt and navigate to the location of this projects .dll that gets created (for 
' example: C:\temp\CustomRasterCatalogRendererPicker_VBNET\bin\Debug)
' 2. Use the esriregasm.exe utility from the Program Files\Common Files\ArcGIS\bin to unregister the DLL.

Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.ADF.CATIDs
Imports System.Runtime.InteropServices
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.DataSourcesRaster
Imports ESRI.ArcGIS.Display

<ComClass(CustomRasterCatalogRenderer_VBNET.ClassId, CustomRasterCatalogRenderer_VBNET.InterfaceId, CustomRasterCatalogRenderer_VBNET.EventsId), _
 ProgId("CustomRasterCatalogRendererPicker_VBNET.CustomRasterCatalogRenderer_VBNET")> _
Public Class CustomRasterCatalogRenderer_VBNET
  Implements ESRI.ArcGIS.Carto.IRasterCatalogRendererPicker

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
    RasterCatalogRendererPickers.Register(regKey)

  End Sub
  ''' <summary>
  ''' Required method for ArcGIS Component Category unregistration -
  ''' Do not modify the contents of this method with the code editor.
  ''' </summary>
  Private Shared Sub ArcGISCategoryUnregistration(ByVal registerType As Type)
    Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
    RasterCatalogRendererPickers.Unregister(regKey)

  End Sub

#End Region
#End Region

#Region "COM GUIDs"
  ' These  GUIDs provide the COM identity for this class 
  ' and its COM interfaces. If you change them, existing 
  ' clients will no longer be able to access the class.
  Public Const ClassId As String = "018daae4-dea8-4e0c-986e-db950ccc2e18"
  Public Const InterfaceId As String = "b6077420-e52d-4f1d-8728-91e0466769c5"
  Public Const EventsId As String = "c150f454-5e62-4b63-a092-83859b3ba7bc"
#End Region

  ' A creatable COM class must have a Public Sub New() 
  ' with no parameters, otherwise, the class will not be 
  ' registered in the COM registry and cannot be created 
  ' via CreateObject.
  Public Sub New()
    MyBase.New()
  End Sub

  Public ReadOnly Property AllAvailableRenderersCLSID() As String() Implements ESRI.ArcGIS.Carto.IRasterCatalogRendererPicker.AllAvailableRenderersCLSID
    Get
      Dim Renderers(1) As String
      'Define the raster renderers using class ID
      Renderers(0) = "esriCarto.RasterRGBRenderer"
      Renderers(1) = "esriCarto.RasterStretchColorRampRenderer"
      Return Renderers
    End Get
  End Property

  Public ReadOnly Property DefaultUseRenderersCLSID() As String() Implements ESRI.ArcGIS.Carto.IRasterCatalogRendererPicker.DefaultUseRenderersCLSID
    Get
      Dim Renderers(1) As String
      'Define the available raster renderers
      Renderers(0) = "esriCarto.RasterRGBRenderer"
      Renderers(1) = "esriCarto.RasterStretchColorRampRenderer"
      Return Renderers
    End Get
  End Property

  Public ReadOnly Property Priority() As Integer Implements ESRI.ArcGIS.Carto.IRasterCatalogRendererPicker.Priority
    Get
            Return 12
    End Get
  End Property

  Public Function Pick(ByVal pRenderers As ESRI.ArcGIS.esriSystem.IArray, ByVal pRasterDataset As ESRI.ArcGIS.Geodatabase.IRasterDataset) As ESRI.ArcGIS.Carto.IRasterRenderer Implements ESRI.ArcGIS.Carto.IRasterCatalogRendererPicker.Pick

    Dim rasterRenderer As IRasterRenderer = Nothing
    Dim rasterRGBRenderer As IRasterRGBRenderer = Nothing
    Dim rasterStretchColorRampRenderer As IRasterStretchColorRampRenderer = Nothing

    ' Get the renderers
    Dim Count As Integer = pRenderers.Count
    Dim i As Integer
    For i = 0 To Count - 1

      rasterRenderer = CType(pRenderers.Element(i), IRasterRenderer)

      If TypeOf rasterRenderer Is IRasterStretchColorRampRenderer Then
        rasterStretchColorRampRenderer = CType(rasterRenderer, IRasterStretchColorRampRenderer)
      ElseIf TypeOf rasterRenderer Is IRasterRGBRenderer Then
        rasterRGBRenderer = CType(rasterRenderer, IRasterRGBRenderer)
      End If

    Next i

    Dim rasterDataset2 As IRasterDataset2 = CType(pRasterDataset, IRasterDataset2)
    Dim rasterBandCollection As IRasterBandCollection = CType(rasterDataset2, IRasterBandCollection)

    If rasterBandCollection.Count > 5 Then

      ' Use band 4,5 and 3 as red, green and blue
      rasterRenderer = CType(rasterRGBRenderer, IRasterRenderer)
      rasterRGBRenderer.SetBandIndices(3, 4, 2)
      Return CType(rasterRGBRenderer, IRasterRenderer)

    Else ' Special stretch

      Dim rasterBand As IRasterBand = rasterBandCollection.Item(0)

      Dim hasTable As Boolean
      rasterBand.HasTable(hasTable)

      If hasTable = False Then


        ' Simply change the color ramp for the stretch renderer
        'Dim fromColor As IColor = New RgbColorClass
        'fromColor.RGB = RGB(255, 200, 50)
        Dim fromColor As IColor = CType(CreateRGBColor(255, 200, 50), IColor)

        'Dim toColor As IColor = New RgbColorClass
        'toColor.RGB = RGB(180, 125, 0)
        Dim toColor As IColor = CType(CreateRGBColor(180, 125, 0), IColor)

        ' Create color ramp
        Dim algorithmicColorRamp As IAlgorithmicColorRamp = New AlgorithmicColorRampClass
        algorithmicColorRamp.Size = 255
        algorithmicColorRamp.FromColor = fromColor
        algorithmicColorRamp.ToColor = toColor
        Dim createRamp As Boolean
        algorithmicColorRamp.CreateRamp(createRamp)

        If createRamp = True Then

          rasterRenderer = CType(rasterStretchColorRampRenderer, IRasterRenderer)
          rasterStretchColorRampRenderer.BandIndex = 0
          rasterStretchColorRampRenderer.ColorRamp = algorithmicColorRamp
          Return CType(rasterStretchColorRampRenderer, IRasterRenderer)

        End If

      End If

    End If

    Return rasterRenderer

  End Function

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