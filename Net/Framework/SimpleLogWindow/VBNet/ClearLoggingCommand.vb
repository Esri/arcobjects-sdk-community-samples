Imports System.Runtime.InteropServices
Imports System.Drawing
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.ArcMapUI
Imports ESRI.ArcGIS.esriSystem

<ComClass(ClearLoggingCommand.ClassId, ClearLoggingCommand.InterfaceId, ClearLoggingCommand.EventsId), _
 ProgId("SimpleLogWindowVB.ClearLoggingCommand")> _
Public NotInheritable Class ClearLoggingCommand
    Inherits BaseCommand

#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "af3e8998-c592-42cf-b9c3-04a5b8e8d47b"
    Public Const InterfaceId As String = "9e3086ca-2211-4e36-9113-4b9289596618"
    Public Const EventsId As String = "452b067e-8da7-41d8-b1d2-d6b1c2f4d49c"
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
    ''' <summary>
    ''' Required method for ArcGIS Component Category registration -
    ''' Do not modify the contents of this method with the code editor.
    ''' </summary>
    Private Shared Sub ArcGISCategoryRegistration(ByVal registerType As Type)
        Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
        GxCommands.Register(regKey)
        GMxCommands.Register(regKey)
        MxCommands.Register(regKey)
        SxCommands.Register(regKey)

    End Sub
    ''' <summary>
    ''' Required method for ArcGIS Component Category unregistration -
    ''' Do not modify the contents of this method with the code editor.
    ''' </summary>
    Private Shared Sub ArcGISCategoryUnregistration(ByVal registerType As Type)
        Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
        GxCommands.Unregister(regKey)
        GMxCommands.Unregister(regKey)
        MxCommands.Unregister(regKey)
        SxCommands.Unregister(regKey)

    End Sub

#End Region
#End Region


    Private m_application As IApplication

    ' A creatable COM class must have a Public Sub New() 
    ' with no parameters, otherwise, the class will not be 
    ' registered in the COM registry and cannot be created 
    ' via CreateObject.
    Public Sub New()
        MyBase.New()

        MyBase.m_category = ".NET Samples"
        MyBase.m_caption = "Clear Log (VB.Net)"
        MyBase.m_message = "Clear items in logging dockable window"
        MyBase.m_toolTip = "Clear log"
        MyBase.m_name = "VBNETSamples_ClearLogCommand"
    End Sub


    Public Overrides Sub OnCreate(ByVal hook As Object)
        If Not hook Is Nothing Then
            m_application = CType(hook, IApplication)
        End If
    End Sub

    Public Overrides Sub OnClick()
        'This command is designed to be on a context menu displayed when the 
        'logging window is right-clicked. Get the context item of the application
        Dim doc As IDocument = m_application.Document
        Dim contextItem As Object
        If TypeOf doc Is IBasicDocument Then
            contextItem = DirectCast(doc, IBasicDocument).ContextItem
        End If

        Dim dockWin As IDockableWindow
        Dim logWindowID As New UIDClass()
        logWindowID.Value = "{8582b32d-120c-407b-af34-8719b8960b30}"
        If contextItem IsNot Nothing AndAlso TypeOf contextItem Is IDockableWindow Then
            dockWin = DirectCast(contextItem, IDockableWindow)
        Else    'In the case of ArcCatalog or the command has been placed outside the designated context menu
            'Get the dockable window directly
            Dim dockWindowManager As IDockableWindowManager = DirectCast(m_application, IDockableWindowManager)
            dockWin = dockWindowManager.GetDockableWindow(logWindowID)
        End If

        'Clear list items in the dockable window
        If dockWin IsNot Nothing AndAlso dockWin.ID.Compare(logWindowID) Then
            Dim containedBox As System.Windows.Forms.ListBox = TryCast(dockWin.UserData, System.Windows.Forms.ListBox)
            containedBox.Items.Clear()
        End If
    End Sub
End Class



