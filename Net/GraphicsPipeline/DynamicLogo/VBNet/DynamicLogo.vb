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
Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Windows.Forms
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.Controls
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Display
Imports Microsoft.Win32
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.ADF
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS

''' <summary>
''' Command that works in ArcMap/Map/PageLayout
''' </summary>
<Guid("638eb76e-0b28-4538-92ba-89cebf4e1acb"), ClassInterface(ClassInterfaceType.None), ProgId("DynamicLogo.DynamicLogo")> _
Public NotInheritable Class DynamicLogo : Inherits BaseCommand
#Region "COM Registration Function(s)"
  <ComRegisterFunction(), ComVisible(False)> _
  Private Shared Sub RegisterFunction(ByVal registerType As Type)
    ' Required for ArcGIS Component Category Registrar support
    ArcGISCategoryRegistration(registerType)

    '
    ' TODO: Add any COM registration code here
    ''
  End Sub

  <ComUnregisterFunction(), ComVisible(False)> _
  Private Shared Sub UnregisterFunction(ByVal registerType As Type)
    ' Required for ArcGIS Component Category Registrar support
    ArcGISCategoryUnregistration(registerType)

    '
    ' TODO: Add any COM unregistration code here
    ''
  End Sub

#Region "ArcGIS Component Category Registrar generated code"
  ''' <summary>
  ''' Required method for ArcGIS Component Category registration -
  ''' Do not modify the contents of this method with the code editor.
  ''' </summary>
  Private Shared Sub ArcGISCategoryRegistration(ByVal registerType As Type)
    Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
    ControlsCommands.Register(regKey)
  End Sub
  ''' <summary>
  ''' Required method for ArcGIS Component Category unregistration -
  ''' Do not modify the contents of this method with the code editor.
  ''' </summary>
  Private Shared Sub ArcGISCategoryUnregistration(ByVal registerType As Type)
    Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
    ControlsCommands.Unregister(regKey)
  End Sub

#End Region
#End Region

  Private m_hookHelper As IHookHelper = Nothing
  Private m_logoPath As String = String.Empty
  Private m_logoSymbol As ISymbol = Nothing
  Public m_bOnce As Boolean = True
  Private m_logoGlyph As IDynamicGlyph = Nothing
  Private m_dynamicGlyphFactory As IDynamicGlyphFactory = Nothing
  Private m_dynamicSymbolProps As IDynamicSymbolProperties = Nothing
  Private m_dynamicDrawScreen As IDynamicDrawScreen = Nothing
  Private m_point As IPoint
    Private m_bIsOn As Boolean = False
    Private avEvents As IActiveViewEvents_Event



  Public Sub New()
    '
    ' TODO: Define values for the public properties
    '
    MyBase.m_category = ".NET Samples" 'localizable text
    MyBase.m_caption = "Show Logo" 'localizable text
    MyBase.m_message = "Show or hide the logo" 'localizable text
    MyBase.m_toolTip = "Show or hide the logo" 'localizable text
    MyBase.m_name = "DynamicLogo_ShowDynamicLogo" 'unique id, non-localizable (e.g. "MyCategory_MyCommand")

    Try
      Dim bitmapResourceName As String = Me.GetType().Name & ".bmp"
      MyBase.m_bitmap = New Bitmap(Me.GetType(), bitmapResourceName)
    Catch ex As Exception
      System.Diagnostics.Trace.WriteLine(ex.Message, "Invalid Bitmap")
    End Try
  End Sub

#Region "Overridden Class Methods"

  ''' <summary>
  ''' Occurs when this command is created
  ''' </summary>
  ''' <param name="hook">Instance of the application</param>
  Public Overrides Sub OnCreate(ByVal hook As Object)
    If hook Is Nothing Then
      Return
    End If

    Try
      m_hookHelper = New HookHelperClass()
      m_hookHelper.Hook = hook
      If m_hookHelper.ActiveView Is Nothing Then
        m_hookHelper = Nothing
      End If
    Catch
      m_hookHelper = Nothing
    End Try

    If m_hookHelper Is Nothing Then
      MyBase.m_enabled = False
    Else
      MyBase.m_enabled = True
    End If


  End Sub

  ''' <summary>
  ''' Occurs when this command is clicked
  ''' </summary>
  Public Overrides Sub OnClick()
    m_logoPath = GetLogoPath()
    Dim map As IMap = m_hookHelper.FocusMap
    Dim dynamicMap As IDynamicMap = TryCast(map, IDynamicMap)

    Dim activeView As IActiveView = TryCast(map, IActiveView)
        'Dim avEvents As IActiveViewEvents_Event 
        avEvents = TryCast(activeView, IActiveViewEvents_Event)
    Dim dynamicMapEvents As IDynamicMapEvents_Event = TryCast(dynamicMap, IDynamicMapEvents_Event)
    Dim screenDisplay As IScreenDisplay = activeView.ScreenDisplay

    If (Not m_bIsOn) Then

      AddHandler avEvents.AfterDraw, AddressOf avEvents_AfterDraw
      AddHandler dynamicMapEvents.AfterDynamicDraw, AddressOf dynamicMapEvents_AfterDynamicDraw

    Else
      RemoveHandler dynamicMapEvents.AfterDynamicDraw, AddressOf dynamicMapEvents_AfterDynamicDraw
      RemoveHandler avEvents.AfterDraw, AddressOf avEvents_AfterDraw
    End If
    m_bIsOn = Not m_bIsOn
    screenDisplay.Invalidate(Nothing, True, CShort(esriScreenCache.esriNoScreenCache))
    screenDisplay.UpdateWindow()
  End Sub

  Public Overrides ReadOnly Property Checked() As Boolean
    Get
      Return m_bIsOn
    End Get
  End Property

  Private Sub dynamicMapEvents_AfterDynamicDraw(ByVal DynamicMapDrawPhase As esriDynamicMapDrawPhase, ByVal Display As IDisplay, ByVal dynamicDisplay As IDynamicDisplay)
    If DynamicMapDrawPhase <> esriDynamicMapDrawPhase.esriDMDPDynamicLayers Then
      Return
    End If
    DrawDynamicLogo(dynamicDisplay)


  End Sub

  Private Sub DrawDynamicLogo(ByVal dynamicDisplay As IDynamicDisplay)
    If m_bOnce Then
      'cast the DynamicDisplay into DynamicGlyphFactory
      m_dynamicGlyphFactory = dynamicDisplay.DynamicGlyphFactory
      'cast the DynamicDisplay into DynamicSymbolProperties
      m_dynamicSymbolProps = TryCast(dynamicDisplay, IDynamicSymbolProperties)

      m_dynamicDrawScreen = TryCast(dynamicDisplay, IDynamicDrawScreen)

      'create the dynamic glyph for the logo
      m_logoGlyph = m_dynamicGlyphFactory.CreateDynamicGlyphFromFile(esriDynamicGlyphType.esriDGlyphMarker, m_logoPath, ESRI.ArcGIS.ADF.Connection.Local.Converter.ToRGBColor(Color.White))

      m_point = New PointClass()
      m_point.PutCoords(120, 160)
      m_bOnce = False
    End If

    m_dynamicSymbolProps.DynamicGlyph(esriDynamicSymbolType.esriDSymbolMarker) = m_logoGlyph
    m_dynamicSymbolProps.SetScale(esriDynamicSymbolType.esriDSymbolMarker, 0.435F, 0.435F)
    m_dynamicDrawScreen.DrawScreenMarker(m_point)
  End Sub

  Private Sub avEvents_AfterDraw(ByVal Display As ESRI.ArcGIS.Display.IDisplay, ByVal phase As esriViewDrawPhase)
    If phase <> esriViewDrawPhase.esriViewForeground Then
      Return
    End If
    DrawLogoStandard(Display)
  End Sub

  Private Sub DrawLogoStandard(ByVal Display As IDisplay)
    Dim r As tagRECT = Display.DisplayTransformation.DeviceFrame
    Display.StartDrawing(Display.hDC, CShort(esriScreenCache.esriNoScreenCache))
    If Nothing Is m_logoSymbol Then
      m_logoSymbol = CreateStandardLogoSymbol()
    End If
    Display.SetSymbol(m_logoSymbol)
    Display.DrawPoint(Display.DisplayTransformation.ToMapPoint(120, r.bottom - 160))
    Display.FinishDrawing()
  End Sub

#End Region

  Private Function GetLogoPath() As String

     'relative file path to the sample data from project location
        dim sLogoPath as string = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
        sLogoPath = System.IO.Path.Combine(sLogoPath, "ArcGIS\data\ESRILogo\ESRI_LOGO.bmp")
        dim filePath as DirectoryInfo = new DirectoryInfo(sLogoPath)
        System.Diagnostics.Debug.WriteLine(string.Format("File path for data root: {0} [{1}]", filePath.FullName, Directory.GetCurrentDirectory()))

    'set the of the logo
    If (Not System.IO.File.Exists(sLogoPath)) Then
      MessageBox.Show(string.Format("File path for the logo was not found: {0} [{1}] please correct in code or place logo file in proper path", filePath.FullName, Directory.GetCurrentDirectory()))
      Return String.Empty
    End If

    Return sLogoPath
  End Function

  Private Function CreateStandardLogoSymbol() As ISymbol
    Dim pictureMarkerSymbol As IPictureMarkerSymbol = New PictureMarkerSymbolClass()
    pictureMarkerSymbol.CreateMarkerSymbolFromFile(esriIPictureType.esriIPictureBitmap, m_logoPath)
    pictureMarkerSymbol.Size = 100
    Dim whiteTransparencyColor As IColor = TryCast(ESRI.ArcGIS.ADF.Connection.Local.Converter.ToRGBColor(Color.FromArgb(255, 255, 255)), IColor)
    pictureMarkerSymbol.BitmapTransparencyColor = whiteTransparencyColor

    Return TryCast(pictureMarkerSymbol, ISymbol)
  End Function
End Class
