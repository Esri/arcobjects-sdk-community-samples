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
Imports Microsoft.Win32
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.Controls
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Display
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.DataSourcesFile

	''' <summary>
	''' Summary description for AddWeatherItemTool.
	''' </summary>
	<ClassInterface(ClassInterfaceType.None), Guid("D74A98E8-2CAA-4068-AA3D-C4DFA25BD5BE"), ProgId("AddWeatherItemTool"), ComVisible(True)> _
	Public NotInheritable Class AddWeatherItemTool : Inherits BaseTool
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

		#Region "ArcGIS Component Category Registrar generated code"
		''' <summary>
		''' Required method for ArcGIS Component Category registration -
		''' Do not modify the contents of this method with the code editor.
		''' </summary>
		Private Shared Sub ArcGISCategoryRegistration(ByVal registerType As Type)
			Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
			MxCommands.Register(regKey)
	  ControlsCommands.Register(regKey)
		End Sub
		''' <summary>
		''' Required method for ArcGIS Component Category unregistration -
		''' Do not modify the contents of this method with the code editor.
		''' </summary>
		Private Shared Sub ArcGISCategoryUnregistration(ByVal registerType As Type)
			Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
			MxCommands.Unregister(regKey)
	  ControlsCommands.Unregister(regKey)
		End Sub

		#End Region
		#End Region

		Private m_hookHelper As IHookHelper = Nothing
		Private m_weatherLayer As RSSWeatherLayerClass = Nothing
		Private m_featureClass As IFeatureClass = Nothing
		Private m_spatialRefWGS84 As ISpatialReference = Nothing

		Public Sub New()
			MyBase.m_category = "Weather"
			MyBase.m_caption = "Add Weather Item"
			MyBase.m_message = "Add Weather item"
			MyBase.m_toolTip = "Add Weather item"
			MyBase.m_name = MyBase.m_category & "_" & MyBase.m_caption

			Try
				MyBase.m_bitmap = New System.Drawing.Bitmap(Me.GetType().Assembly.GetManifestResourceStream(Me.GetType(), "AddWeatherItemTool.bmp"))
				MyBase.m_cursor = New System.Windows.Forms.Cursor(Me.GetType().Assembly.GetManifestResourceStream(Me.GetType(), "AddWeatherItemTool.cur"))
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
        'Instantiate the hook helper
        If m_hookHelper Is Nothing Then
            m_hookHelper = New HookHelperClass()
        End If

        'set the hook
        m_hookHelper.Hook = hook

        'connect to the ZipCodes featureclass
        Dim path As String = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)

        path = System.IO.Path.Combine(path, "ArcGIS\data\USZipCodeData\")
        if (not Directory.Exists(path)) then throw new Exception(string.Format("Fix code to point to your sample data: {0} was not found", path))


        Dim wf As IWorkspaceFactory = New ShapefileWorkspaceFactoryClass()
        Dim ws As IWorkspace = wf.OpenFromFile(path, 0)
        Dim fw As IFeatureWorkspace = TryCast(ws, IFeatureWorkspace)
        m_featureClass = fw.OpenFeatureClass("US_ZipCodes")

        m_spatialRefWGS84 = CreateGeoCoordSys()
    End Sub

    ''' <summary>
    ''' Occurs when this command is clicked
    ''' </summary>
    Public Overrides Sub OnClick()
        Try
            If 0 = m_hookHelper.FocusMap.LayerCount Then
                Return
            End If

            'get the weather layer
            Dim layers As IEnumLayer = m_hookHelper.FocusMap.Layers(Nothing, False)
            layers.Reset()
            Dim layer As ILayer = layers.Next()
            Do While Not layer Is Nothing
                If TypeOf layer Is RSSWeatherLayerClass Then
                    m_weatherLayer = CType(layer, RSSWeatherLayerClass)
                    Exit Do
                End If
                layer = layers.Next()
            Loop
        Catch ex As Exception
            System.Diagnostics.Trace.WriteLine(ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Occurs when the user click inside the globe
    ''' </summary>
    ''' <param name="Button"></param>
    ''' <param name="Shift"></param>
    ''' <param name="X"></param>
    ''' <param name="Y"></param>
    Public Overrides Sub OnMouseDown(ByVal Button As Integer, ByVal Shift As Integer, ByVal X As Integer, ByVal Y As Integer)
        'make sure that the weatherlayer and the featureclass exists
        If Nothing Is m_weatherLayer OrElse Nothing Is m_featureClass Then
            Return
        End If

        'get the current point (convert the mouse coordinate into geographics coordinate)
        Dim activeView As IActiveView = m_hookHelper.ActiveView
        Dim displayTrans As IDisplayTransformation = activeView.ScreenDisplay.DisplayTransformation
        Dim point As IPoint = displayTrans.ToMapPoint(X, Y)
        point.SpatialReference = m_hookHelper.FocusMap.SpatialReference

        'project the point to WGS1984 CoordSys
        If Not Nothing Is point.SpatialReference Then
            If point.SpatialReference.FactoryCode <> m_spatialRefWGS84.FactoryCode Then
                point.Project(m_spatialRefWGS84)
            End If
        End If

        'create the spatial filter in order to select the relevant zipCode
        Dim spatialFilter As ISpatialFilter = New SpatialFilterClass()
        spatialFilter.Geometry = TryCast(point, IGeometry)
        spatialFilter.GeometryField = m_featureClass.ShapeFieldName

        'The spatial filter supposed to filter all the polygons containing the point
        spatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelWithin

        'query the featureclass for the containing features
        Dim featureCursor As IFeatureCursor = m_featureClass.Search(spatialFilter, True)
        Dim feature As IFeature = featureCursor.NextFeature()
        If Nothing Is feature Then
            Return
        End If

        'get the zip code from the containing feature
        Dim zipCode As Long = Convert.ToInt64(feature.Value(feature.Fields.FindField("ZIP")))

        'add the weather item to the layer
        m_weatherLayer.AddItem(zipCode, point.Y, point.X)

        'refresh the map
        activeView.PartialRefresh(esriViewDrawPhase.esriViewGeography, m_weatherLayer, displayTrans.FittedBounds)
        activeView.ScreenDisplay.UpdateWindow()

        'release the featurecursor
        Marshal.ReleaseComObject(featureCursor)
    End Sub

    ''' <summary>
    ''' Occurs when the user move the mouse
    ''' </summary>
    ''' <param name="Button"></param>
    ''' <param name="Shift"></param>
    ''' <param name="X"></param>
    ''' <param name="Y"></param>
    Public Overrides Sub OnMouseMove(ByVal Button As Integer, ByVal Shift As Integer, ByVal X As Integer, ByVal Y As Integer)
        ' TODO:  Add AddWeatherItemTool.OnMouseMove implementation
    End Sub

    ''' <summary>
    ''' Occurs when then user release the mouse button
    ''' </summary>
    ''' <param name="Button"></param>
    ''' <param name="Shift"></param>
    ''' <param name="X"></param>
    ''' <param name="Y"></param>
    Public Overrides Sub OnMouseUp(ByVal Button As Integer, ByVal Shift As Integer, ByVal X As Integer, ByVal Y As Integer)
        ' TODO:  Add AddWeatherItemTool.OnMouseUp implementation
    End Sub
#End Region

	Private Function CreateGeoCoordSys() As ISpatialReference
	  Dim spatialRefFactory As ISpatialReferenceFactory2 = New SpatialReferenceEnvironmentClass()

	  Dim wgs84GeoCoordSys As IGeographicCoordinateSystem = spatialRefFactory.CreateGeographicCoordinateSystem(CInt(esriSRGeoCSType.esriSRGeoCS_WGS1984))

	  Return CType(wgs84GeoCoordSys, ISpatialReference)
	End Function
	End Class
