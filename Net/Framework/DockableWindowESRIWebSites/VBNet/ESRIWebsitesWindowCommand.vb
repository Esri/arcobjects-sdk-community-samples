Imports System.Runtime.InteropServices
Imports System.Drawing
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.esriSystem

<ComClass(ESRIWebsitesWindowCommand.ClassId, ESRIWebsitesWindowCommand.InterfaceId, ESRIWebsitesWindowCommand.EventsId), _
 ProgId("ESRIWebSitesVB.ESRIWebsitesWindowCommand")> _
Public NotInheritable Class ESRIWebsitesWindowCommand
    Inherits BaseCommand

#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "b769c7a1-63a5-4750-86f1-54c805dda82e"
    Public Const InterfaceId As String = "97c95fb2-4026-4f50-969a-5e9265170d4a"
    Public Const EventsId As String = "8fc37324-0409-48aa-b9fa-8b7ec2e62cd0"
#End Region

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
    Private Shared Sub ArcGISCategoryRegistration(ByVal registerType As Type)
        Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
        MxCommands.Register(regKey)
        GxCommands.Register(regKey)
        SxCommands.Register(regKey)
        GMxCommands.Register(regKey)
    End Sub
    Private Shared Sub ArcGISCategoryUnregistration(ByVal registerType As Type)
        Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
        MxCommands.Unregister(regKey)
        GxCommands.Unregister(regKey)
        SxCommands.Unregister(regKey)
        GMxCommands.Unregister(regKey)
    End Sub

#End Region
#End Region

    Private m_application As IApplication
    Private m_dockableWindow As IDockableWindow

    Private Const DockableWindowGuid As String = "{9a26bf09-6875-4857-82a3-2fb3f25e5d37}"

    ' A creatable COM class must have a Public Sub New() 
    ' with no parameters, otherwise, the class will not be 
    ' registered in the COM registry and cannot be created 
    ' via CreateObject.
    Public Sub New()
        MyBase.New()

        MyBase.m_category = ".NET Samples"
        MyBase.m_caption = "Toggle ESRI Resources Window (VB.Net)"
        MyBase.m_message = "Command toggles ESRI Resources window (VB.Net)"
        MyBase.m_toolTip = "Toggle ESRI resources dockable window"
        MyBase.m_name = "VBNETTemplate_ESRIWebsitesWindowCommand"

        Try
            Dim bitmapResourceName As String = Me.GetType().Name + ".bmp"
            MyBase.m_bitmap = New Bitmap(Me.GetType(), bitmapResourceName)
        Catch ex As Exception
            System.Diagnostics.Trace.WriteLine(ex.Message, "Invalid Bitmap")
        End Try
    End Sub

    ''' <summary>
    ''' Occurs when this command is created
    ''' </summary>
    ''' <param name="hook">Instance of the application</param>
    Public Overrides Sub OnCreate(ByVal hook As Object)
        If Not hook Is Nothing Then
            m_application = CType(hook, IApplication)
        End If

        If m_application IsNot Nothing Then
            SetupDockableWindow()
            MyBase.m_enabled = m_dockableWindow IsNot Nothing
        Else
            MyBase.m_enabled = False
        End If
    End Sub

    ''' <summary>
    ''' Toggle visibility of dockable window and show the visible state by its checked property
    ''' </summary>
    Public Overrides Sub OnClick()
        If m_dockableWindow Is Nothing Then Return

        If m_dockableWindow.IsVisible() Then
            m_dockableWindow.Show(False)
        Else
            m_dockableWindow.Show(True)
        End If

        MyBase.m_checked = m_dockableWindow.IsVisible()
    End Sub

    Public Overrides ReadOnly Property Checked() As Boolean
        Get
            Return (m_dockableWindow IsNot Nothing) AndAlso (m_dockableWindow.IsVisible())
        End Get
    End Property

    Private Sub SetupDockableWindow()
        If m_dockableWindow Is Nothing Then
            Dim dockWindowManager As IDockableWindowManager
            dockWindowManager = CType(m_application, IDockableWindowManager)
            If Not dockWindowManager Is Nothing Then
                Dim windowID As UID = New UIDClass
                windowID.Value = DockableWindowGuid
                m_dockableWindow = dockWindowManager.GetDockableWindow(windowID)
            End If
        End If
    End Sub
End Class



