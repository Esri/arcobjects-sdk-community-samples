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
Imports System.Xml
Imports System.Drawing
Imports System.Runtime.InteropServices
Imports System.Windows.Forms
Imports ESRI.ArcGIS.Controls
Imports System.Collections.Generic
Imports Microsoft.Win32

Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.DataSourcesFile
Imports ESRI.ArcGIS.Display
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.SystemUI
Imports ESRI.ArcGIS.ADF
Imports ESRI.ArcGIS

''' <summary>
''' A user defined data structure
''' </summary>
Public Structure NavigationData
  Public X As Double
  Public Y As Double
  Public Azimuth As Double

  ''' <summary>
  ''' struct constructor
  ''' </summary>
  ''' <param name="x">map x coordinate</param>
  ''' <param name="y">map x coordinate</param>
  ''' <param name="azimuth">the new map azimuth</param>
  Public Sub New(ByVal newX As Double, ByVal newY As Double, ByVal newAzimuth As Double)
    X = newX
    Y = newY
    Azimuth = newAzimuth
  End Sub
End Structure

''' <summary>
''' This command triggers the tracking functionality using Dynamic Display
''' </summary>
<Guid("803D4188-AB2F-49f9-9340-42C809887063"), ComVisible(True), ClassInterface(ClassInterfaceType.None), ProgId("DynamicObjectTracking.TrackObject")> _
Public NotInheritable Class TrackObject : Inherits BaseCommand
#Region "COM Registration Function(s)"
  <ComRegisterFunction(), ComVisible(False)> _
  Private Shared Sub RegisterFunction(ByVal registerType As Type)
    ' Required for ArcGIS Component Category Registrar support
    ArcGISCategoryRegistration(registerType)

    '
    ' TODO: Add any COM registration code here
    '
  End Sub

  <ComUnregisterFunction(), ComVisible(False)> _
  Private Shared Sub UnregisterFunction(ByVal registerType As Type)
    ' Required for ArcGIS Component Category Registrar support
    ArcGISCategoryUnregistration(registerType)

    '
    ' TODO: Add any COM unregistration code here
    '
  End Sub
#End Region

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

#Region "class members"
  Private m_hookHelper As IHookHelper = Nothing
  Private m_dynamicMap As IDynamicMap = Nothing
  Private m_point As IPoint = Nothing
  Private m_VWmarkerGlyph As IDynamicGlyph = Nothing
  Private m_dynamicGlyphFactory As IDynamicGlyphFactory2 = Nothing
  Private m_dynamicSymbolProps As IDynamicSymbolProperties2 = Nothing
  Private m_bDrawOnce As Boolean = True
  Private m_displayTransformation As IDisplayTransformation = Nothing
  Private m_points As List(Of WKSPoint) = Nothing
  Private Shared m_pointIndex As Integer = 0
  Private m_bIsRunning As Boolean = False
  Private m_bOnce As Boolean = True
  Private m_VWFileName As String = String.Empty
  Private m_navigationDataFileName As String = String.Empty
  Private m_navigationData As NavigationData
#End Region

  ''' <summary>
  ''' class constructor
  ''' </summary>
  Public Sub New()
    MyBase.m_category = ".NET Samples"
    MyBase.m_caption = "Track Dynamic Object"
    MyBase.m_message = "Tracking a dynamic object"
    MyBase.m_toolTip = "Tracking a dynamic object"
    MyBase.m_name = "TrackDynamicObject"

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

    If m_hookHelper Is Nothing Then
      m_hookHelper = New HookHelperClass()
    End If

    m_hookHelper.Hook = hook

    m_point = New PointClass()
    m_points = New List(Of WKSPoint)()
  End Sub

  ''' <summary>
  ''' Occurs when this command is clicked
  ''' </summary>
  Public Overrides Sub OnClick()
    'make sure to switch into dynamic mode
    m_dynamicMap = CType(m_hookHelper.FocusMap, IDynamicMap)
    If (Not m_dynamicMap.DynamicMapEnabled) Then
      m_dynamicMap.DynamicMapEnabled = True
    End If

    'do initializations
    If m_bOnce Then
      'generate the navigation data
      GenerateNavigationData()
      m_displayTransformation = m_hookHelper.ActiveView.ScreenDisplay.DisplayTransformation
      m_bOnce = False
    End If

    'hook the dynamic display events
    If (Not m_bIsRunning) Then
      AddHandler (CType(m_dynamicMap, IDynamicMapEvents_Event)).DynamicMapFinished, AddressOf OnTimerElapsed
      AddHandler (CType(m_dynamicMap, IDynamicMapEvents_Event)).AfterDynamicDraw, AddressOf OnAfterDynamicDraw
    Else
      RemoveHandler (CType(m_dynamicMap, IDynamicMapEvents_Event)).DynamicMapFinished, AddressOf OnTimerElapsed
      RemoveHandler (CType(m_dynamicMap, IDynamicMapEvents_Event)).AfterDynamicDraw, AddressOf OnAfterDynamicDraw
    End If

    'set the running flag
    m_bIsRunning = Not m_bIsRunning
  End Sub

  ''' <summary>
  ''' set the state of the button of the command
  ''' </summary>
  Public Overrides ReadOnly Property Checked() As Boolean
    Get
      Return m_bIsRunning
    End Get
  End Property

#End Region

  Private Sub OnTimerElapsed(ByVal Display As IDisplay, ByVal dynamicDisplay As IDynamicDisplay)
    Try
      'make sure that the current tracking point index does not exceed the list index
      If m_pointIndex = (m_points.Count - 1) Then
        m_pointIndex = 0
        Return
      End If

      'get the current and the next track location
      Dim currentPoint As WKSPoint = m_points(m_pointIndex)
      Dim nextPoint As WKSPoint = m_points(m_pointIndex + 1)

      'calculate the azimuth to the next location
      Dim azimuth As Double = (180.0 / Math.PI) * Math.Atan2(nextPoint.X - currentPoint.X, nextPoint.Y - currentPoint.Y)

      'set the navigation data structure
      m_navigationData.X = currentPoint.X
      m_navigationData.Y = currentPoint.Y
      m_navigationData.Azimuth = azimuth

      'update the map extent and rotation
      CenterMap(m_navigationData)

      'increment the tracking point index
      m_pointIndex += 1

    Catch ex As Exception
      System.Diagnostics.Trace.WriteLine(ex.Message)
    End Try
  End Sub

  Private Sub CenterMap(ByVal navigationData As NavigationData)
    Try
      'get the current map visible extent
      Dim envelope As IEnvelope = m_displayTransformation.VisibleBounds
      If Nothing Is m_point Then
        m_point = New PointClass()
      End If
      'set new map center coordinate
      m_point.PutCoords(navigationData.X, navigationData.Y)
      'center the map
      envelope.CenterAt(m_point)
      m_displayTransformation.VisibleBounds = envelope
      'rotate the map to new angle
      m_displayTransformation.Rotation = navigationData.Azimuth
    Catch ex As Exception
      System.Diagnostics.Trace.WriteLine(ex.Message)
    End Try
  End Sub

  Private Sub GenerateNavigationData()
    Try
      Dim path As String = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
      'get navigationData.xml file from DeveloperKit
      m_navigationDataFileName = System.IO.Path.Combine(path, "ArcGIS\data\USAMajorHighways\NavigationData.xml")
      If (Not System.IO.File.Exists(m_navigationDataFileName)) Then
        Throw New Exception("File " & m_navigationDataFileName & " cannot be found!")
      End If

      Dim reader As XmlTextReader = New XmlTextReader(m_navigationDataFileName)

      Dim doc As XmlDocument = New XmlDocument()
      doc.Load(reader)

      reader.Close()

      Dim X As Double
      Dim Y As Double
      'get the navigation items
      Dim nodes As XmlNodeList = doc.DocumentElement.SelectNodes("./navigationItem")
      For Each node As XmlNode In nodes
        X = Convert.ToDouble(node.Attributes(0).Value)
        Y = Convert.ToDouble(node.Attributes(1).Value)

        Dim p As WKSPoint = New WKSPoint()
        p.X = X
        p.Y = Y
        m_points.Add(p)
      Next node
    Catch ex As Exception
      System.Diagnostics.Trace.WriteLine(ex.Message)
    End Try
  End Sub

  Private Sub OnAfterDynamicDraw(ByVal DynamicMapDrawPhase As esriDynamicMapDrawPhase, ByVal Display As IDisplay, ByVal dynamicDisplay As IDynamicDisplay)

    If (DynamicMapDrawPhase <> esriDynamicMapDrawPhase.esriDMDPDynamicLayers) Then
      Return
    End If

    If m_bDrawOnce Then
      'cast the DynamicDisplay into DynamicGlyphFactory
      m_dynamicGlyphFactory = TryCast(dynamicDisplay.DynamicGlyphFactory, IDynamicGlyphFactory2)
      'cast the DynamicDisplay into DynamicSymbolProperties
      m_dynamicSymbolProps = TryCast(dynamicDisplay, IDynamicSymbolProperties2)

      'create the VW dynamic marker glyph from the embedded bitmap resource
      Dim bitmap As Bitmap = New Bitmap(Me.GetType(), "VW.bmp")
      'get bitmap handler
      Dim hBmp As Integer = bitmap.GetHbitmap().ToInt32()
      'set white transparency color
      Dim whiteTransparencyColor As IColor
      whiteTransparencyColor = TryCast(ESRI.ArcGIS.ADF.Connection.Local.Converter.ToRGBColor(Color.FromArgb(0, 0, 0)), IColor)
      'get the VM dynamic marker glyph
      m_VWmarkerGlyph = m_dynamicGlyphFactory.CreateDynamicGlyphFromBitmap(esriDynamicGlyphType.esriDGlyphMarker, hBmp, False, whiteTransparencyColor)

      m_bDrawOnce = False
    End If

    'set the symbol alignment so that it will align with towards the symbol heading
    m_dynamicSymbolProps.RotationAlignment(esriDynamicSymbolType.esriDSymbolMarker) = esriDynamicSymbolRotationAlignment.esriDSRANorth

    'set the symbol's properties
    m_dynamicSymbolProps.DynamicGlyph(esriDynamicSymbolType.esriDSymbolMarker) = m_VWmarkerGlyph
    m_dynamicSymbolProps.SetScale(esriDynamicSymbolType.esriDSymbolMarker, 1.3F, 1.3F)
    m_dynamicSymbolProps.SetColor(esriDynamicSymbolType.esriDSymbolMarker, 1.0F, 1.0F, 0.0F, 1.0F) ' yellow

    'set the heading of the current symbol
    m_dynamicSymbolProps.Heading(esriDynamicSymbolType.esriDSymbolMarker) = CSng(m_navigationData.Azimuth)

    'draw the current location
    dynamicDisplay.DrawMarker(m_point)
  End Sub
End Class
