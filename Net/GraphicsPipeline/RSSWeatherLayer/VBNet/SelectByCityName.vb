Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Runtime.InteropServices
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.Controls
Imports ESRI.ArcGIS.Carto

  ''' <summary>
  ''' Selects an item by a given city name
  ''' </summary>
	<ClassInterface(ClassInterfaceType.None), Guid("B44F2830-4116-42c2-8A2C-A7097CD7F7BE"), ProgId("SelectByCityName"), ComVisible(True)> _
	Public NotInheritable Class SelectByCityName : Inherits BaseCommand
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

	'Class members
		Private m_pHookHelper As IHookHelper = Nothing
		Private m_weatherLayer As RSSWeatherLayerClass = Nothing
		Private m_selectionDlg As WeatherItemSelectionDlg = Nothing

		Public Sub New()
			MyBase.m_category = "Weather"
			MyBase.m_caption = "Select Weather item By Cityname"
			MyBase.m_message = "Select By Cityname"
			MyBase.m_toolTip = "Select By Cityname"
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
    End Sub

    ''' <summary>
    ''' Occurs when this command is clicked
    ''' </summary>
    Public Overrides Sub OnClick()
        Try
            If m_pHookHelper.FocusMap.LayerCount = 0 Then
                Return
            End If

            'get the weather layer
            Dim layers As IEnumLayer = m_pHookHelper.FocusMap.Layers(Nothing, False)
            layers.Reset()
            Dim layer As ILayer = layers.Next()
            Do While Not layer Is Nothing
                If TypeOf layer Is RSSWeatherLayerClass Then
                    m_weatherLayer = CType(layer, RSSWeatherLayerClass)
                    Exit Do
                End If
                layer = layers.Next()
            Loop

            If Not m_weatherLayer Is Nothing Then
                If Nothing Is m_selectionDlg OrElse m_selectionDlg.IsDisposed Then
                    m_selectionDlg = New WeatherItemSelectionDlg(m_weatherLayer, m_pHookHelper.ActiveView)
                End If

                m_selectionDlg.Show()
            End If
        Catch ex As Exception
            System.Diagnostics.Trace.WriteLine(ex.Message)
        End Try
    End Sub

#End Region
	End Class
