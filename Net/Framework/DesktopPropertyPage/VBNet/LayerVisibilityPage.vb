Imports System.Runtime.InteropServices
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.SystemUI
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.Carto

''' <summary>
''' Layer property page implementation for ArcMap, ArcScene or ArcGlobe.
''' </summary>
<ComClass(LayerVisibilityPage.ClassId, LayerVisibilityPage.InterfaceId, LayerVisibilityPage.EventsId), _
 ProgId("DesktopPropertyPageVB.LayerVisibilityPage")> _
Public Class LayerVisibilityPage
    Implements IComPropertyPage

#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "80c42689-3b85-44a4-9b04-9ba0390109f7"
    Public Const InterfaceId As String = "cd3588f5-60ba-4a8b-9082-0f6f502dceb4"
    Public Const EventsId As String = "7e17322f-8495-4d80-b275-d15824d536fa"
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
        SxLayerPropertyPages.Register(regKey)
        GMxLayerPropertyPages.Register(regKey)
        LayerPropertyPages.Register(regKey)

    End Sub
    ''' <summary>
    ''' Required method for ArcGIS Component Category unregistration -
    ''' Do not modify the contents of this method with the code editor.
    ''' </summary>
    Private Shared Sub ArcGISCategoryUnregistration(ByVal registerType As Type)
        Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
        SxLayerPropertyPages.Unregister(regKey)
        GMxLayerPropertyPages.Unregister(regKey)
        LayerPropertyPages.Unregister(regKey)

    End Sub

#End Region
#End Region

    Private m_pageTitle As String
    Private m_dirtyFlag As Boolean = False

    Private m_pageSite As IComPropertyPageSite
    Private m_targetLayer As ILayer
    Private m_activeView As IActiveView

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        m_pageTitle = "Layer Visibility (VB.Net)"
    End Sub

    ''' <summary>
    ''' Call this to set dirty flag whenever changes are made to the UI/page
    ''' </summary>
    Private Sub SetPageDirty(ByVal dirty As Boolean)
        If m_dirtyFlag <> dirty Then
            m_dirtyFlag = dirty
            If Not m_pageSite Is Nothing Then
                m_pageSite.PageChanged()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Example: update dirty flag when check state of radio button 
    ''' (which controls layer visibility) changes
    ''' </summary>
    Private Sub radioButtonShow_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles radioButtonShow.CheckedChanged
        SetPageDirty(True)
    End Sub

#Region "IComPropertyPage Members"
    Public Function Activate() As Integer Implements ESRI.ArcGIS.Framework.IComPropertyPage.Activate
        'Set up page UI based on layer visibility
        If m_targetLayer.Visible Then
            radioButtonShow.Checked = True
        Else
            radioButtonHide.Checked = True
        End If

        SetPageDirty(False)
        Return Me.Handle.ToInt32()
    End Function
    ''' <summary>
    ''' Indicates if the page applies to the specified objects
    ''' Do not hold on to the objects here.
    ''' </summary>
    Public Function Applies(ByVal objects As ESRI.ArcGIS.esriSystem.ISet) As Boolean Implements ESRI.ArcGIS.Framework.IComPropertyPage.Applies
        If objects Is Nothing Then Return False
        If objects.Count = 0 Then Return False

        Dim isEditable As Boolean = False
        objects.Reset()
        Dim testObject As Object = objects.Next()
        Do Until testObject Is Nothing
            If TypeOf testObject Is ILayer Then
                isEditable = True
                Exit Do
            End If
            testObject = objects.Next()
        Loop

        Return isEditable
    End Function

    Public Sub Apply() Implements ESRI.ArcGIS.Framework.IComPropertyPage.Apply
        If m_dirtyFlag Then

            'Set layer visibility
            m_targetLayer.Visible = radioButtonShow.Checked

            'Refresh display after changes are made
            If Not m_activeView Is Nothing Then
                m_activeView.PartialRefresh(esriViewDrawPhase.esriViewGeography, Nothing, Nothing)
                m_activeView.ContentsChanged() 'update TOC
            End If
            
            SetPageDirty(False)
        End If
    End Sub

    Public Sub Cancel() Implements ESRI.ArcGIS.Framework.IComPropertyPage.Cancel
        If m_dirtyFlag Then
            'Reset UI or any temporary changes made to layer
            radioButtonShow.Checked = m_targetLayer.Visible

            SetPageDirty(False)
        End If
    End Sub

    Public Sub Deactivate() Implements ESRI.ArcGIS.Framework.IComPropertyPage.Deactivate
        m_targetLayer = Nothing
        m_activeView = Nothing
        Me.Dispose(True)
    End Sub

    Public ReadOnly Property Height1() As Integer Implements ESRI.ArcGIS.Framework.IComPropertyPage.Height
        Get
            Return Me.Height
        End Get
    End Property

    Public ReadOnly Property HelpContextID(ByVal controlID As Integer) As Integer Implements ESRI.ArcGIS.Framework.IComPropertyPage.HelpContextID
        Get
            Return 0
        End Get
    End Property

    Public ReadOnly Property HelpFile() As String Implements ESRI.ArcGIS.Framework.IComPropertyPage.HelpFile
        Get
            Return String.Empty
        End Get
    End Property

    Public Sub Hide1() Implements ESRI.ArcGIS.Framework.IComPropertyPage.Hide

    End Sub
    ''' <summary>
    ''' Indicates if the Apply button should be enabled
    ''' </summary>
    Public ReadOnly Property IsPageDirty() As Boolean Implements ESRI.ArcGIS.Framework.IComPropertyPage.IsPageDirty
        Get
            Return m_dirtyFlag
        End Get
    End Property

    Public WriteOnly Property PageSite() As ESRI.ArcGIS.Framework.IComPropertyPageSite Implements ESRI.ArcGIS.Framework.IComPropertyPage.PageSite
        Set(ByVal value As ESRI.ArcGIS.Framework.IComPropertyPageSite)
            m_pageSite = value
        End Set
    End Property

    Public Property Priority() As Integer Implements ESRI.ArcGIS.Framework.IComPropertyPage.Priority
        Get
            Return 0
        End Get
        Set(ByVal value As Integer)

        End Set
    End Property

    ''' <summary>
    ''' Supplies the page with the object(s) to be edited
    ''' </summary>
    Public Sub SetObjects(ByVal objects As ESRI.ArcGIS.esriSystem.ISet) Implements ESRI.ArcGIS.Framework.IComPropertyPage.SetObjects
        If objects Is Nothing Then Return
        If objects.Count = 0 Then Return

        m_targetLayer = Nothing
        m_activeView = Nothing

        objects.Reset()
        Dim testObject As Object = objects.Next()
        Do Until testObject Is Nothing
            If TypeOf testObject Is ILayer Then
                m_targetLayer = CType(testObject, ILayer)
            ElseIf TypeOf testObject Is IActiveView Then
                m_activeView = CType(testObject, IActiveView)
            End If

            testObject = objects.Next()
        Loop
    End Sub

    Public Sub Show1() Implements ESRI.ArcGIS.Framework.IComPropertyPage.Show

    End Sub

    Public Property Title() As String Implements ESRI.ArcGIS.Framework.IComPropertyPage.Title
        Get
            Return m_pageTitle
        End Get
        Set(ByVal value As String)
            'TODO: Uncomment if title can be modified
            'm_pageTitle = value
        End Set
    End Property

    Public ReadOnly Property Width1() As Integer Implements ESRI.ArcGIS.Framework.IComPropertyPage.Width
        Get
            Return Me.Width
        End Get
    End Property
#End Region

   
End Class
