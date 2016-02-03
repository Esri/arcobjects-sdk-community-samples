Imports System.Drawing
Imports System.Runtime.InteropServices
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.Controls
Imports ESRI.ArcGIS.Carto

''' <summary>
''' Command that works in ArcMap/Map/PageLayout, ArcScene/SceneControl
''' or ArcGlobe/GlobeControl
''' </summary>
<Guid("ffae67a3-92b6-47d6-9d33-a8dd909a53c4")> _
<ClassInterface(ClassInterfaceType.None)> _
<ProgId("RSSWeatherGraphicTracker.AddRSSWeather")> _
Public NotInheritable Class AddRSSWeather
	Inherits BaseCommand
	#Region "COM Registration Function(s)"
	<ComRegisterFunction> _
	<ComVisible(False)> _
	Private Shared Sub RegisterFunction(registerType As Type)
		' Required for ArcGIS Component Category Registrar support
		ArcGISCategoryRegistration(registerType)
	End Sub

	<ComUnregisterFunction> _
	<ComVisible(False)> _
	Private Shared Sub UnregisterFunction(registerType As Type)
		' Required for ArcGIS Component Category Registrar support
		ArcGISCategoryUnregistration(registerType)
	End Sub

	#Region "ArcGIS Component Category Registrar generated code"
	''' <summary>
	''' Required method for ArcGIS Component Category registration -
	''' Do not modify the contents of this method with the code editor.
	''' </summary>
	Private Shared Sub ArcGISCategoryRegistration(registerType As Type)
		Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
		GMxCommands.Register(regKey)
		MxCommands.Register(regKey)
		SxCommands.Register(regKey)
		ControlsCommands.Register(regKey)
	End Sub
	''' <summary>
	''' Required method for ArcGIS Component Category unregistration -
	''' Do not modify the contents of this method with the code editor.
	''' </summary>
	Private Shared Sub ArcGISCategoryUnregistration(registerType As Type)
		Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
		GMxCommands.Unregister(regKey)
		MxCommands.Unregister(regKey)
		SxCommands.Unregister(regKey)
		ControlsCommands.Unregister(regKey)
	End Sub

	#End Region
	#End Region

	Private m_hookHelper As IHookHelper = Nothing
	Private m_globeHookHelper As IGlobeHookHelper = Nothing

	Private m_bConnected As Boolean = False
	Private m_rssWeather As RSSWeather = Nothing

	Public Sub New()
		MyBase.m_category = "Weather"
		MyBase.m_caption = "Add RSS Weather"
		MyBase.m_message = "Add RSS Weather"
		MyBase.m_toolTip = "Add RSS Weather"
		MyBase.m_name = "Add RSS Weather"

		Try
			Dim bitmapResourceName As String = [GetType]().Name & ".bmp"
			MyBase.m_bitmap = New Bitmap([GetType](), bitmapResourceName)
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

        ' Test the hook that calls this command and disable if nothing is valid
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
            'Can be globe
            Try
                m_globeHookHelper = New GlobeHookHelperClass()
                m_globeHookHelper.Hook = hook
                If m_globeHookHelper.ActiveViewer Is Nothing Then
                    m_globeHookHelper = Nothing
                End If
            Catch
                m_globeHookHelper = Nothing
            End Try
        End If

        If m_globeHookHelper Is Nothing AndAlso m_hookHelper Is Nothing Then
            MyBase.m_enabled = False
        Else
            MyBase.m_enabled = True
        End If

    End Sub

    ''' <summary>
    ''' Occurs when this command is clicked
    ''' </summary>
    Public Overrides Sub OnClick()
        Dim basicMap As IBasicMap = Nothing
        If m_hookHelper IsNot Nothing Then
            basicMap = TryCast(m_hookHelper.FocusMap, IBasicMap)
        ElseIf m_globeHookHelper IsNot Nothing Then
            basicMap = TryCast(m_globeHookHelper.Globe, IBasicMap)
        End If

        If basicMap Is Nothing Then
            Return
        End If

        Try
            If Not m_bConnected Then
                m_rssWeather = New RSSWeather()
                m_rssWeather.Init(basicMap)
            Else
                m_rssWeather.Remove()
                m_rssWeather = Nothing
            End If

            m_bConnected = Not m_bConnected
        Catch ex As Exception
            System.Diagnostics.Trace.WriteLine(ex.Message)
        End Try
    End Sub

    Public Overrides ReadOnly Property Checked() As Boolean
        Get
            Return m_bConnected
        End Get
    End Property

#End Region
End Class
