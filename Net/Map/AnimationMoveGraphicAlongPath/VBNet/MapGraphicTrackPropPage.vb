Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Drawing
Imports System.Data
Imports System.Text
Imports System.Windows.Forms
Imports System.Runtime.InteropServices
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.Animation
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.ADF.CATIDs

<Guid("EC7FC44A-3516-4872-8F93-944259B34662"), ClassInterface(ClassInterfaceType.None), ProgId("CustomMapAnimation1.MapGraphicTrackPropPage")> _
Public Class MapGraphicTrackPropPage : Implements IComPropertyPage
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
        AGAnimationTrackPropertyPages.Register(regKey)

    End Sub
    ''' <summary>
    ''' Required method for ArcGIS Component Category unregistration -
    ''' Do not modify the contents of this method with the code editor.
    ''' </summary>
    Private Shared Sub ArcGISCategoryUnregistration(ByVal registerType As Type)
        Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
        AGAnimationTrackPropertyPages.Unregister(regKey)

    End Sub

#End Region
#End Region
    Private targetTrack As IAGAnimationTrack
    Private priority_Renamed As Integer
    Private pageSite_Renamed As IComPropertyPageSite
    Private propPage As frmPropertyPage

    Public Sub New()
        propPage = New frmPropertyPage()
        priority_Renamed = 0
    End Sub

#Region "IComPropertyPage members"
    Public Function Activate() As Integer Implements IComPropertyPage.Activate
        Return propPage.Handle.ToInt32()
    End Function

    Public Function Applies(ByVal Objects As ISet) As Boolean Implements IComPropertyPage.Applies
        Dim obj As Object
        Dim track As IAGAnimationTrack
        Dim count As Integer = Objects.Count
        Objects.Reset()
        Dim appl As Boolean = False
        Dim i As Integer = 0
        Do While i < count
            obj = Objects.Next()
            track = CType(obj, IAGAnimationTrack)
            If Not track Is Nothing Then
                If track.AnimationType.Name = "Map Graphic" Then
                    appl = True
                    Exit Do
                End If
            End If
            i += 1
        Loop
        Return appl
    End Function

    Public Sub SetObjects(ByVal Objects As ISet) Implements IComPropertyPage.SetObjects
        Dim count As Integer = Objects.Count
        Objects.Reset()
        Dim i As Integer = 0
        Do While i < count
            Dim obj As Object = Objects.Next()
            targetTrack = CType(obj, IAGAnimationTrack)
            If Not targetTrack Is Nothing Then
                Exit Do
            End If
            i += 1
        Loop
        propPage.Init(targetTrack)
    End Sub

    Public Sub Apply() Implements IComPropertyPage.Apply
        Dim trackExtensions As IAGAnimationTrackExtensions = CType(targetTrack, IAGAnimationTrackExtensions)
        Dim trackExtension As IMapGraphicTrackExtension
        If trackExtensions.ExtensionCount = 0 Then 'if there is no extension, add one
            trackExtension = New MapGraphicTrackExtension()
            trackExtensions.AddExtension(trackExtension)
        Else
            trackExtension = CType(trackExtensions.Extension(0), IMapGraphicTrackExtension)
        End If

        trackExtension.ShowTrace = propPage.CheckBoxShowTrace.Checked
    End Sub

    Public Sub Cancel() Implements IComPropertyPage.Cancel

    End Sub
    Public Sub Deactivate() Implements IComPropertyPage.Deactivate
        targetTrack = Nothing
        propPage.Dispose()
    End Sub
    Public ReadOnly Property Height() As Integer Implements IComPropertyPage.Height
        Get
            Return propPage.Height
        End Get
    End Property
    Public ReadOnly Property Width() As Integer Implements IComPropertyPage.Width
        Get
            Return propPage.Width
        End Get
    End Property

    Public ReadOnly Property HelpContextID(ByVal controlID As Integer) As Integer Implements IComPropertyPage.HelpContextID
        Get
            Return 0
        End Get
    End Property
    Public ReadOnly Property HelpFile() As String Implements IComPropertyPage.HelpFile
        Get
            Return ""
        End Get
    End Property
    Public Sub Hide() Implements IComPropertyPage.Hide
        propPage.Hide()
    End Sub

    Public ReadOnly Property IsPageDirty() As Boolean Implements IComPropertyPage.IsPageDirty
        Get
            Return propPage.PageDirty
        End Get
    End Property
    Public WriteOnly Property PageSite() As IComPropertyPageSite Implements IComPropertyPage.PageSite
        Set(ByVal value As IComPropertyPageSite)
            pageSite_Renamed = value
        End Set
    End Property
    Public Property Priority() As Integer Implements IComPropertyPage.Priority
        Get
            Return priority_Renamed
        End Get
        Set(ByVal value As Integer)
            priority_Renamed = value
        End Set
    End Property

    Public Sub Show() Implements IComPropertyPage.Show
        propPage.Visible = True
    End Sub

    Public Property Title() As String Implements IComPropertyPage.Title
        Get
            Return propPage.Text
        End Get
        Set(ByVal value As String)
            propPage.Text = value
        End Set
    End Property
#End Region
End Class

