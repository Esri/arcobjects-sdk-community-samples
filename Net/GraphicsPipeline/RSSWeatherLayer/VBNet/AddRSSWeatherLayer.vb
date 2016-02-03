Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Windows.Forms
Imports System.Runtime.InteropServices
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Controls
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.Display

  ''' <summary>
  ''' Add a new weather item given a zipCode.
  ''' </summary>
  ''' <remarks>Should the weather item exist, it will be updated</remarks>
	<ClassInterface(ClassInterfaceType.None), Guid("C9260965-D3AA-4c28-B55A-023C41F1CA39"), ProgId("AddRSSWeatherLayer"), ComVisible(True)> _
	Public NotInheritable Class AddRSSWeatherLayer : Inherits BaseCommand
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
			ControlsCommands.Register(regKey)
	  MxCommands.Register(regKey)

		End Sub
		''' <summary>
		''' Required method for ArcGIS Component Category unregistration -
		''' Do not modify the contents of this method with the code editor.
		''' </summary>
		Private Shared Sub ArcGISCategoryUnregistration(ByVal registerType As Type)
			Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
			ControlsCommands.Unregister(regKey)
	  MxCommands.Unregister(regKey)

		End Sub

		#End Region
		#End Region

	Private NotInheritable Class InvokeHelper : Inherits Control
	  'delegate used to pass the invoked method to the main thread
	  Public Delegate Sub RefreshHelper(ByVal invalidateExtent As WKSEnvelope)
	  Public Delegate Sub RefreshWeatherItemHelper(ByVal weatherItemInfo As WeatherItemEventArgs)

	  Private m_activeView As IActiveView = Nothing
	  Private m_weatherLayer As RSSWeatherLayerClass = Nothing
	  Private m_invalidateExtent As IEnvelope = Nothing
	  Private m_point As IPoint

	  Public Sub New(ByVal activeView As IActiveView, ByVal weatherLayer As RSSWeatherLayerClass)
		m_activeView = activeView
		m_weatherLayer = weatherLayer

		CreateHandle()
		CreateControl()

		m_invalidateExtent = New EnvelopeClass()
		m_invalidateExtent.SpatialReference = activeView.ScreenDisplay.DisplayTransformation.SpatialReference

		m_point = New PointClass()
		m_point.SpatialReference = activeView.ScreenDisplay.DisplayTransformation.SpatialReference
	  End Sub

	  Public Sub RefreshWeatherItem(ByVal weatherItemInfo As WeatherItemEventArgs)
		Try
		  ' Invoke the RefreshInternal through its delegate
		  If (Not Me.IsDisposed) AndAlso Me.IsHandleCreated Then
			Invoke(New RefreshWeatherItemHelper(AddressOf RefreshWeatherItemInvoked), New Object() { weatherItemInfo })
		  End If
		Catch ex As Exception
		  System.Diagnostics.Trace.WriteLine(ex.Message)
		End Try
	  End Sub

	  Public Overloads Sub Refresh(ByVal invalidateExtent As WKSEnvelope)
		Try
		  ' Invoke the RefreshInternal through its delegate
		  If (Not Me.IsDisposed) AndAlso Me.IsHandleCreated Then
			Invoke(New RefreshHelper(AddressOf RefreshInvoked), New Object() { invalidateExtent })
		  End If
		Catch ex As Exception
		  System.Diagnostics.Trace.WriteLine(ex.Message)
		End Try
	  End Sub

	  Private Sub RefreshWeatherItemInvoked(ByVal weatherItemInfo As WeatherItemEventArgs)
		Dim transform As ITransformation = TryCast(m_activeView.ScreenDisplay.DisplayTransformation, ITransformation)
		If transform Is Nothing Then
		  Return
		End If

		Dim iconDimensions As Double() = New Double(1){}
		iconDimensions(0) = CDbl(weatherItemInfo.IconWidth)
		iconDimensions(1) = CDbl(weatherItemInfo.IconHeight)

		Dim iconDimensionsMap As Double() = New Double(1){}

		transform.TransformMeasuresFF(esriTransformDirection.esriTransformReverse, 1, iconDimensionsMap(0), iconDimensions(0))

		m_invalidateExtent.PutCoords(0, 0, iconDimensionsMap(0), iconDimensionsMap(0))
		m_point.PutCoords(weatherItemInfo.mapX, weatherItemInfo.mapY)
		m_invalidateExtent.CenterAt(m_point)

		m_activeView.PartialRefresh(esriViewDrawPhase.esriViewGeography, m_weatherLayer, m_invalidateExtent)
		m_activeView.ScreenDisplay.UpdateWindow()
	  End Sub

	  Private Sub RefreshInvoked(ByVal invalidateExtent As WKSEnvelope)
		m_invalidateExtent.PutWKSCoords(invalidateExtent)

		If (Not m_invalidateExtent.IsEmpty) Then
		  m_activeView.PartialRefresh(esriViewDrawPhase.esriViewGeography, m_weatherLayer, m_invalidateExtent)
		Else
		  m_activeView.PartialRefresh(esriViewDrawPhase.esriViewGeography, m_weatherLayer, Nothing)
		End If

		m_activeView.ScreenDisplay.UpdateWindow()
	  End Sub
	End Class

	Private m_invokeHelper As InvokeHelper = Nothing

	'class members
		Private m_pHookHelper As IHookHelper = Nothing
		Private m_weatherLayer As RSSWeatherLayerClass = Nothing
		Private m_bOnce As Boolean = True
	Private m_bConnected As Boolean = False

		Public Sub New()
			MyBase.m_category = "Weather"
			MyBase.m_caption = "Add RSS Weather Layer"
			MyBase.m_message = "Add RSS Weather Layer"
			MyBase.m_toolTip = "Add RSS Weather Layer"
			MyBase.m_name = MyBase.m_category & "_" & MyBase.m_caption

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
        'Instantiate the hook helper
        If m_pHookHelper Is Nothing Then
            m_pHookHelper = New HookHelperClass()
        End If

        'set the hook
        m_pHookHelper.Hook = hook

        'set verbose events in order to listen to ItemDeleted event
        CType(m_pHookHelper.FocusMap, IViewManager).VerboseEvents = True

        'hook the ItemDeleted event in order to track removal of the layer from the TOC
        AddHandler (CType(m_pHookHelper.FocusMap, IActiveViewEvents_Event)).ItemDeleted, AddressOf OnItemDeleted
    End Sub

    ''' <summary>
    ''' Occurs when this command is clicked
    ''' </summary>
    Public Overrides Sub OnClick()
        Try
            If (Not m_bConnected) Then
                'check first that the layer was added to the globe
                If m_bOnce = True Then
                    'instantiate the layer
                    m_weatherLayer = New RSSWeatherLayerClass()
                    m_invokeHelper = New InvokeHelper(m_pHookHelper.ActiveView, m_weatherLayer)

                    m_bOnce = False
                End If
                'test whether the layer has been added to the map
                Dim bLayerHasBeenAdded As Boolean = False
                Dim layer As ILayer = Nothing

                If m_pHookHelper.FocusMap.LayerCount <> 0 Then
                    Dim layers As IEnumLayer = m_pHookHelper.FocusMap.Layers(Nothing, False)
                    layers.Reset()
                    layer = layers.Next()
                    Do While Not layer Is Nothing
                        If TypeOf layer Is RSSWeatherLayerClass Then
                            bLayerHasBeenAdded = True
                            Exit Do
                        End If
                        layer = layers.Next()
                    Loop
                End If

                'add the layer to the map
                If (Not bLayerHasBeenAdded) Then
                    layer = CType(m_weatherLayer, ILayer)
                    layer.Name = "RSS Weather Layer"
                    Try
                        m_pHookHelper.FocusMap.AddLayer(layer)
                        'wires layer's events
                        AddHandler m_weatherLayer.OnWeatherItemAdded, AddressOf OnWeatherItemAdded
                        AddHandler m_weatherLayer.OnWeatherItemsUpdated, AddressOf OnWeatherItemsUpdated
                    Catch ex As Exception
                        System.Diagnostics.Trace.WriteLine("Failed" & ex.Message)
                    End Try
                End If

                'connect to the service
                m_weatherLayer.Connect()
            Else
                'disconnect from the service
                m_weatherLayer.Disconnect()

                'un-wires layer's events
                RemoveHandler m_weatherLayer.OnWeatherItemAdded, AddressOf OnWeatherItemAdded
                RemoveHandler m_weatherLayer.OnWeatherItemsUpdated, AddressOf OnWeatherItemsUpdated

                'delete the layer
                m_pHookHelper.FocusMap.DeleteLayer(m_weatherLayer)

                'dispose the layer
                m_weatherLayer = Nothing
                m_bOnce = True
            End If


            m_bConnected = Not m_bConnected
        Catch ex As Exception
            System.Diagnostics.Trace.WriteLine(ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Indicates whether or not this command is checked. 
    ''' </summary>
    Public Overrides ReadOnly Property Checked() As Boolean
        Get
            Return m_bConnected
        End Get
    End Property

#End Region

	''' <summary>
	''' weather layer ItemAdded event handler
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="args"></param>
	''' <remarks>gets fired when an item is added to the table</remarks>
		Private Sub OnWeatherItemAdded(ByVal sender As Object, ByVal args As WeatherItemEventArgs)
			' use the invoke helper since this event gets fired on a different thread
	  m_invokeHelper.RefreshWeatherItem(args)
		End Sub

	''' <summary>
	''' Weather layer ItemsUpdated event handler
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="args"></param>
	''' <remarks>gets fired when the update thread finish updating all the items in the table</remarks>
	Private Sub OnWeatherItemsUpdated(ByVal sender As Object, ByVal args As EventArgs)
		'refresh the display
		Dim emptyEnv As WKSEnvelope

		emptyEnv.XMax = 0
		emptyEnv.XMin = 0
		emptyEnv.YMax = 0
		emptyEnv.YMin = 0
		m_invokeHelper.Refresh(emptyEnv)
	End Sub

	''' <summary>
	''' Listen to ActiveViewEvents.ItemDeleted in order to track whether the layer has been 
	''' removed from the TOC
	''' </summary>
	''' <param name="Item"></param>
	Private Sub OnItemDeleted(ByVal Item As Object)
	  'test that the deleted layer is RSSWeatherLayerClass
	  If TypeOf Item Is RSSWeatherLayerClass Then
			If m_bConnected AndAlso Not Nothing Is m_weatherLayer Then
				m_bConnected = False

				'disconnect from the service
				m_weatherLayer.Disconnect()

				'dispose the layer
				m_weatherLayer = Nothing
				m_bOnce = True
			End If
	  End If
	End Sub
	End Class

