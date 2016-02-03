Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports System.Runtime.InteropServices

<ComClass(RootMenuRecentFiles.ClassId, RootMenuRecentFiles.InterfaceId, RootMenuRecentFiles.EventsId), _
 ProgId("RecentFilesCommandsVB.RootMenuRecentFiles")> _
Public NotInheritable Class RootMenuRecentFiles
    Inherits BaseMenu

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
        MxCommandBars.Register(regKey)
        GMxCommandBars.Register(regKey)
        SxCommandBars.Register(regKey)

    End Sub
    ''' <summary>
    ''' Required method for ArcGIS Component Category unregistration -
    ''' Do not modify the contents of this method with the code editor.
    ''' </summary>
    Private Shared Sub ArcGISCategoryUnregistration(ByVal registerType As Type)
        Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
        MxCommandBars.Unregister(regKey)
        GMxCommandBars.Unregister(regKey)
        SxCommandBars.Unregister(regKey)

    End Sub

#End Region
#End Region

#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "b1f38f88-c53f-4bdb-9c4d-c14070b12b98"
    Public Const InterfaceId As String = "65c561fd-6464-40a8-a08a-32ed38b2fd19"
    Public Const EventsId As String = "8aae0486-0ff4-457c-9012-4fd3dd2ea335"
#End Region

    ' A creatable COM class must have a Public Sub New() 
    ' with no parameters, otherwise, the class will not be 
    ' registered in the COM registry and cannot be created 
    ' via CreateObject.
    Public Sub New()
        AddItem("RecentFilesCommandsVB.CommandRecentFiles")
        BeginGroup()
        AddItem("{927b25f4-f24c-471c-a2cd-777c2c371d34}") 'MultiItem
    End Sub

    Public Overrides ReadOnly Property Caption() As String
        Get
            Return "Recent Files (VB.Net)"
        End Get
    End Property

    Public Overrides ReadOnly Property Name() As String
        Get
            Return "VBNETSamples_RecentFilesMenu"
        End Get
    End Property
End Class


