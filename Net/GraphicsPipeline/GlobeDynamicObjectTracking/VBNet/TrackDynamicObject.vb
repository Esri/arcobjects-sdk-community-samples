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
Imports System.Runtime.InteropServices
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
Imports ESRI.ArcGIS.GlobeCore
Imports ESRI.ArcGIS.Controls
Imports ESRI.ArcGIS.Analyst3D

''' <summary>
''' This command demonstrates tracking dynamic object in ArcGlobe/GlobeControl with the camera
''' </summary>
<Guid("DCB871A1-390A-456f-8A0D-9FDB6A20F721"), ClassInterface(ClassInterfaceType.None), ProgId("GlobeControlApp.TrackDynamicObject")> _
Public NotInheritable Class TrackDynamicObject : Inherits BaseCommand : Implements IDisposable
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
    GMxCommands.Register(regKey)
    ControlsCommands.Register(regKey)

  End Sub
  ''' <summary>
  ''' Required method for ArcGIS Component Category unregistration -
  ''' Do not modify the contents of this method with the code editor.
  ''' </summary>
  Private Shared Sub ArcGISCategoryUnregistration(ByVal registerType As Type)
    Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
    GMxCommands.Unregister(regKey)
    ControlsCommands.Unregister(regKey)

  End Sub

#End Region
#End Region

  'class members
  Private m_globeHookHelper As IGlobeHookHelper = Nothing
  Private m_globeDisplay As IGlobeDisplay = Nothing
  Private m_sceneViwer As ISceneViewer = Nothing
  Private m_globeGraphicsLayer As IGlobeGraphicsLayer = Nothing
  Private m_realTimeFeedManager As IRealTimeFeedManager = Nothing
  Private m_realTimeFeed As IRealTimeFeed = Nothing
  Private m_bConnected As Boolean = False
  Private m_bTrackAboveTarget As Boolean = True
  Private m_once As Boolean = True
  Private m_trackObjectIndex As Integer = -1
  Private m_shapefileName As String = String.Empty


#Region "class constructor"

  ''' <summary>
  ''' Class Ctor
  ''' </summary>
  Public Sub New()
    MyBase.m_category = ".NET Samples"
    MyBase.m_caption = "Track Dynamic Object"
    MyBase.m_message = "Tracking a dynamic object"
    MyBase.m_toolTip = "Track Dynamic Object"
    MyBase.m_name = MyBase.m_category & "_" & MyBase.m_caption

    Try
      Dim bitmapResourceName As String = Me.GetType().Name & ".bmp"
      MyBase.m_bitmap = New Bitmap(Me.GetType(), bitmapResourceName)
    Catch ex As Exception
      System.Diagnostics.Trace.WriteLine(ex.Message, "Invalid Bitmap")
    End Try
  End Sub
#End Region

#Region "Overridden Class Methods"

  ''' <summary>
  ''' Occurs when this command is created
  ''' </summary>
  ''' <param name="hook">Instance of the application</param>
  Public Overrides Sub OnCreate(ByVal hook As Object)
    'initialize the hook-helper
    If m_globeHookHelper Is Nothing Then
      m_globeHookHelper = New GlobeHookHelper()
    End If

    'set the hook
    m_globeHookHelper.Hook = hook

    'connect to the ZipCodes featureclass
    Dim path As String = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)

    'set the path to the featureclass used by the GPS simulator
    m_shapefileName = System.IO.Path.Combine(path, "ArcGIS\data\USAMajorHighways\usa_major_highways.shp")
    System.Diagnostics.Debug.WriteLine(String.Format("File path for data root: {0} ", m_shapefileName))
    If (not System.IO.File.Exists(m_shapefileName)) Then Throw New Exception(String.Format("Fix code to point to your sample data: {0} was not found", m_shapefileName))
    
    'get the GlobeDisplsy from the hook helper
    m_globeDisplay = m_globeHookHelper.GlobeDisplay

    'initialize the real-time manager
    If Nothing Is m_realTimeFeedManager Then
      m_realTimeFeedManager = New RealTimeFeedManagerClass()
    End If

    'use the built in simulator of the real-time manager
    m_realTimeFeedManager.RealTimeFeed = TryCast(m_realTimeFeedManager.RealTimeFeedSimulator, IRealTimeFeed)

    'keep a reference to the RealTimeManager in order to prevent the garbage collector to try and dispose it
    m_realTimeFeed = m_realTimeFeedManager.RealTimeFeed
  End Sub

  ''' <summary>
  ''' Occurs when this command is clicked
  ''' </summary>
  Public Overrides Sub OnClick()
    Try

      If (Not m_bConnected) Then
        'show the tracking type selection dialog (whether to track the element from above or follow it from behind)
        Dim dlg As TrackSelectionDlg = New TrackSelectionDlg()
        If System.Windows.Forms.DialogResult.OK <> dlg.ShowDialog() Then
          Return
        End If

        'get the required tracking mode
        m_bTrackAboveTarget = dlg.UseOrthoTrackingMode

        'do only once initializations
        If m_once Then
          'create the graphics layer to manage the dynamic object
          m_globeGraphicsLayer = New GlobeGraphicsLayerClass()
          CType(m_globeGraphicsLayer, ILayer).Name = "DynamicObjects"

          Dim scene As IScene = CType(m_globeDisplay.Globe, IScene)

          'add the new graphic layer to the globe
          scene.AddLayer(CType(m_globeGraphicsLayer, ILayer), False)

          'activate the graphics layer
          scene.ActiveGraphicsLayer = CType(m_globeGraphicsLayer, ILayer)

          'open a polyline featurelayer that would serve the real-time feed GPS simulator
          Dim featureLayer As IFeatureLayer = GetFeatureLayer()
          If featureLayer Is Nothing Then
            Return
          End If

          'assign the featurelayer to the GPS simulator
          m_realTimeFeedManager.RealTimeFeedSimulator.FeatureLayer = featureLayer

          m_once = False
        End If

        'get the GlobeViewUtil which is needed for coordinate transformations
        m_sceneViwer = m_globeDisplay.ActiveViewer

        'Set the globe mode to terrain mode, since otherwise it will not be possible to set the target position
        CType(m_sceneViwer.Camera, IGlobeCamera).OrientationMode = esriGlobeCameraOrientationMode.esriGlobeCameraOrientationLocal

        'set the simulator elapsed time
        m_realTimeFeedManager.RealTimeFeedSimulator.TimeIncrement = 0.1 'sec

        'wire the real-time feed PositionUpdate event
        AddHandler (CType(m_realTimeFeed, IRealTimeFeedEvents_Event)).PositionUpdated, AddressOf OnPositionUpdated

        'start the real-time listener
        m_realTimeFeed.Start()
      Else
        'stop the real-time listener
        m_realTimeFeed.Stop()

        'unhook the PositionUpdated event handler
        RemoveHandler (CType(m_realTimeFeed, IRealTimeFeedEvents_Event)).PositionUpdated, AddressOf TrackDynamicObject_PositionUpdated
      End If

      'switch the connection flag
      m_bConnected = Not m_bConnected
    Catch ex As Exception
      System.Diagnostics.Trace.WriteLine(ex.Message)
    End Try
  End Sub

  ''' <summary>
  ''' The Checked property indicates the state of this Command. 
  ''' </summary>
  ''' <remarks>If a command item appears depressed on a commandbar, the command is checked.</remarks>
  Public Overrides ReadOnly Property Checked() As Boolean
    Get
      Return m_bConnected
    End Get
  End Property

#End Region

#Region "helper methods"

  ''' <summary>
  ''' get a featurelayer that would be used by the real-time simulator
  ''' </summary>
  ''' <returns></returns>
  Private Function GetFeatureLayer() As IFeatureLayer
    'instantiate a new featurelayer
    Dim featureLayer As IFeatureLayer = New FeatureLayerClass()

    'set the layer's name
    featureLayer.Name = "GPS Data"

    'open the featureclass
    Dim featureClass As IFeatureClass = OpenFeatureClass()
    If featureClass Is Nothing Then
      Return Nothing
    End If

    'set the featurelayer featureclass
    featureLayer.FeatureClass = featureClass

    'return the featurelayer
    Return featureLayer
  End Function

  ''' <summary>
  ''' Opens a shapefile polyline featureclass
  ''' </summary>
  ''' <returns></returns>
  Private Function OpenFeatureClass() As IFeatureClass
    Dim path As String = System.IO.Path.GetDirectoryName(m_shapefileName)
    Dim fileName As String = System.IO.Path.GetFileNameWithoutExtension(m_shapefileName)
    'instantiate a new workspace factory
    Dim workspaceFactory As IWorkspaceFactory = New ShapefileWorkspaceFactoryClass()

    'open the workspace containing the featureclass
    Dim featureWorkspace As IFeatureWorkspace = TryCast(workspaceFactory.OpenFromFile(path, 0), IFeatureWorkspace)

    'open the featureclass
    Dim featureClass As IFeatureClass = featureWorkspace.OpenFeatureClass(fileName)

    'make sure that the featureclass type is polyline
    If featureClass.ShapeType <> esriGeometryType.esriGeometryPolyline Then
      featureClass = Nothing
    End If

    'return the featureclass
    Return featureClass
  End Function

  ''' <summary>
  ''' Adds a sphere element to the given graphics layer at the specified position
  ''' </summary>
  ''' <param name="globeGraphicsLayer"></param>
  ''' <param name="position"></param>
  ''' <returns></returns>
  Private Function AddTrackElement(ByVal globeGraphicsLayer As IGlobeGraphicsLayer, ByVal position As esriGpsPositionInfo) As Integer
    If Nothing Is globeGraphicsLayer Then
      Return -1
    End If

    'create a new point at the given position
    Dim point As IPoint = New PointClass()
    CType(point, IZAware).ZAware = True
    point.X = position.longitude
    point.Y = position.latitude
    point.Z = 0.0

    'set the color for the element (red)
    Dim color As IRgbColor = New RgbColorClass()
    color.Red = 255
    color.Green = 0
    color.Blue = 0

    'create a new 3D marker symbol
    Dim markerSymbol As IMarkerSymbol = New SimpleMarker3DSymbolClass()

    'set the marker symbol's style and resolution
    CType(markerSymbol, ISimpleMarker3DSymbol).Style = esriSimple3DMarkerStyle.esriS3DMSSphere
    CType(markerSymbol, ISimpleMarker3DSymbol).ResolutionQuality = 1.0

    'set the symbol's size and color
    markerSymbol.Size = 700
    markerSymbol.Color = TryCast(color, IColor)

    'crate the graphic element
    Dim trackElement As IElement = New MarkerElementClass()

    'set the element's symbol and geometry (location and shape)
    CType(trackElement, IMarkerElement).Symbol = markerSymbol
    trackElement.Geometry = TryCast(point, IPoint)


    'add the element to the graphics layer
    Dim elemIndex As Integer = 0
    CType(globeGraphicsLayer, IGraphicsContainer).AddElement(trackElement, 0)

    'get the element's index
    globeGraphicsLayer.FindElementIndex(trackElement, elemIndex)
    Return elemIndex
  End Function

  ''' <summary>
  ''' The real-time feed position updated event handler
  ''' </summary>
  ''' <param name="position">a GPS position information</param>
  ''' <param name="estimate">indicates whether this is an estimated time or real time</param>
  Private Sub OnPositionUpdated(ByRef position As esriGpsPositionInfo, ByVal estimate As Boolean)
    Try
      'add the tracking element to the tracking graphics layer (should happen only once)
      If -1 = m_trackObjectIndex Then
        Dim index As Integer = AddTrackElement(m_globeGraphicsLayer, position)
        If -1 = index Then
          Throw New Exception("could not add tracking object")
        End If

        'cache the element's index
        m_trackObjectIndex = index

        Return
      End If
      'get the element by its index
      Dim elem As IElement = (CType(m_globeGraphicsLayer, IGraphicsContainer3D)).Element(m_trackObjectIndex)

      'keep the previous location
      Dim lat, lon, alt As Double
      CType(elem.Geometry, IPoint).QueryCoords(lon, lat)
      alt = (CType(elem.Geometry, IPoint)).Z

      'update the element's position
      Dim point As IPoint = TryCast(elem.Geometry, IPoint)
      point.X = position.longitude
      point.Y = position.latitude
      point.Z = alt
      elem.Geometry = CType(point, IGeometry)

      'update the element in the graphics layer.
      SyncLock m_globeGraphicsLayer
        m_globeGraphicsLayer.UpdateElementByIndex(m_trackObjectIndex)
      End SyncLock

      Dim globeCamera As IGlobeCamera = TryCast(m_sceneViwer.Camera, IGlobeCamera)

      'set the camera position in order to track the element
      If m_bTrackAboveTarget Then
        TrackAboveTarget(globeCamera, point)
      Else
        TrackFollowTarget(globeCamera, point.X, point.Y, point.Z, lon, lat, alt)
      End If

    Catch ex As Exception
      System.Diagnostics.Trace.WriteLine(ex.Message)
    End Try
  End Sub
  Private Sub TrackDynamicObject_PositionUpdated(ByRef position As esriGpsPositionInfo, ByVal estimate As Boolean)

  End Sub

  ''' <summary>
  ''' If the user chose to track the element from behind, set the camera behind the element 
  ''' so that the camera will be placed on the line connecting the previous and the current element's position.
  ''' </summary>
  ''' <param name="globeCamera"></param>
  ''' <param name="newLon"></param>
  ''' <param name="newLat"></param>
  ''' <param name="newAlt"></param>
  ''' <param name="oldLon"></param>
  ''' <param name="oldLat"></param>
  ''' <param name="oldAlt"></param>
  Private Sub TrackFollowTarget(ByVal globeCamera As IGlobeCamera, ByVal newLon As Double, ByVal newLat As Double, ByVal newAlt As Double, ByVal oldLon As Double, ByVal oldLat As Double, ByVal oldAlt As Double)
    'make sure that the camera position is not directly above the element. Otherwise it can lead to
    'an ill condition
    If newLon = oldLon AndAlso newLat = oldLat Then
      newLon += 0.00001
      newLat += 0.00001
    End If

    'calculate the azimuth from the previous position to the current position
    Dim azimuth As Double = Math.Atan2(newLat - oldLat, newLon - oldLon) * (Math.PI / 180.0)

    'the camera new position, right behind the element
    Dim obsX As Double = newLon - 0.04 * Math.Cos(azimuth * (Math.PI / 180))
    Dim obsY As Double = newLat - 0.04 * Math.Sin(azimuth * (Math.PI / 180))

    'set the camera position. The camera must be locked in order to prevent a dead-lock caused by the cache manager
    SyncLock globeCamera
      globeCamera.SetTargetLatLonAlt(newLat, newLon, newAlt / 1000.0)
      globeCamera.SetObserverLatLonAlt(obsY, obsX, newAlt / 1000.0 + 0.7)
      m_sceneViwer.Camera.Apply()
    End SyncLock

    'refresh the globe display
    m_globeDisplay.RefreshViewers()
  End Sub

  ''' <summary>
  ''' should the user choose to track the element from above, set the camera above the element
  ''' </summary>
  ''' <param name="globeCamera"></param>
  ''' <param name="objectLocation"></param>
  Private Sub TrackAboveTarget(ByVal globeCamera As IGlobeCamera, ByVal objectLocation As IPoint)
    'Update the observer as well as the camera position
    'The camera must be locked in order to prevent a dead-lock caused by the cache manager
    SyncLock globeCamera

      globeCamera.SetTargetLatLonAlt(objectLocation.Y, objectLocation.X, objectLocation.Z / 1000.0)

      'The camera must nut be located exactly above the target, since it results in poor orientation computation
      'and therefore the camera gets jumpy.
      globeCamera.SetObserverLatLonAlt(objectLocation.Y - 0.000001, objectLocation.X - 0.000001, objectLocation.Z / 1000.0 + 30.0)
      m_sceneViwer.Camera.Apply()
    End SyncLock
    m_globeDisplay.RefreshViewers()
  End Sub

#End Region

#Region "IDisposable Members"

  Public Sub Dispose() Implements IDisposable.Dispose

  End Sub

#End Region
End Class
