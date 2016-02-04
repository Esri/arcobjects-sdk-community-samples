'Copyright 2016 Esri

'Licensed under the Apache License, Version 2.0 (the "License");
'you may not use this file except in compliance with the License.
'You may obtain a copy of the License at

'    http://www.apache.org/licenses/LICENSE-2.0

'Unless required by applicable law or agreed to in writing, software
'distributed under the License is distributed on an "AS IS" BASIS,
'WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.

'See the License for the specific language governing permissions and
'limitations under the License.
Imports System.Runtime.InteropServices
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.SystemUI
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.ADF.CATIDs

<ComClass(ESRIWebsitesWindow.ClassId, ESRIWebsitesWindow.InterfaceId, ESRIWebsitesWindow.EventsId), _
 ProgId("ESRIWebSitesVB.ESRIWebsitesWindow")> _
Public Class ESRIWebsitesWindow
    Implements IDockableWindowDef

#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "9a26bf09-6875-4857-82a3-2fb3f25e5d37"
    Public Const InterfaceId As String = "677ea374-b472-4d16-bdfd-76c4f5185288"
    Public Const EventsId As String = "72d7b768-12e8-46b3-ad3a-05b31a519b9a"
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
        MxDockableWindows.Register(regKey)
        GxDockableWindows.Register(regKey)
        SxDockableWindows.Register(regKey)
        GMxDockableWindows.Register(regKey)
    End Sub
    ''' <summary>
    ''' Required method for ArcGIS Component Category unregistration -
    ''' Do not modify the contents of this method with the code editor.
    ''' </summary>
    Private Shared Sub ArcGISCategoryUnregistration(ByVal registerType As Type)
        Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
        MxDockableWindows.Unregister(regKey)
        GxDockableWindows.Unregister(regKey)
        SxDockableWindows.Unregister(regKey)
        GMxDockableWindows.Unregister(regKey)
    End Sub

#End Region
#End Region

    Private m_application As IApplication

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
    End Sub

#Region "IDockableWindowDef Members"

    Public ReadOnly Property Caption() As String Implements ESRI.ArcGIS.Framework.IDockableWindowDef.Caption
        Get
            Return "Useful ESRI URLs (VB.Net)"
        End Get
    End Property

    Public ReadOnly Property ChildHWND() As Integer Implements ESRI.ArcGIS.Framework.IDockableWindowDef.ChildHWND
        Get
            Return Me.Handle.ToInt32()
        End Get
    End Property

    Public ReadOnly Property Name1() As String Implements ESRI.ArcGIS.Framework.IDockableWindowDef.Name
        Get
            Return "VBNETSamples_ESRIWebsitesWindow"
        End Get
    End Property

    Public Sub OnCreate(ByVal hook As Object) Implements ESRI.ArcGIS.Framework.IDockableWindowDef.OnCreate
        m_application = CType(hook, IApplication)

        'Set up URLs to the combo
        Dim urls(9) As NameURLPair
        urls(0) = New NameURLPair()
        urls(0).Name = "ESRI"
        urls(0).URL = "http://www.esri.com"

        urls(1) = New NameURLPair()
        urls(1).Name = "ESRI User Conference"
        urls(1).URL = "http://www.esri.com/events/uc/index.html"

        urls(2) = New NameURLPair()
        urls(2).Name = "ESRI User Conference Blog"
        urls(2).URL = "http://blogs.esri.com/roller/page/ucblog"

        urls(3) = New NameURLPair()
        urls(3).Name = "ESRI Support"
        urls(3).URL = "http://support.esri.com/"

        urls(4) = New NameURLPair()
        urls(4).Name = "ESRI Discussion Forum"
        urls(4).URL = "http://support.esri.com/index.cfm?fa=forums.gateway"

        urls(5) = New NameURLPair()
        urls(5).Name = "ESRI Developer Network (EDN)"
        urls(5).URL = "http://edn.esri.com"

        urls(6) = New NameURLPair()
        urls(6).Name = "Current ArcObject Library"
        urls(6).URL = "http://edndoc.esri.com/arcobjects/9.1/default.asp"

        urls(7) = New NameURLPair()
        urls(7).Name = "ArcGIS 9.1 Desktop Help"
        urls(7).URL = "http://webhelp.esri.com/arcgisdesktop/9.1/"

        urls(8) = New NameURLPair()
        urls(8).Name = "ArcGIS 9.2 Desktop Help"
        urls(8).URL = "http://webhelp.esri.com/arcgisdesktop/9.2/index.cfm"

        cboURLs.DisplayMember = "Name"
        cboURLs.ValueMember = "URL"

        cboURLs.DataSource = urls

    End Sub

    Public Sub OnDestroy() Implements ESRI.ArcGIS.Framework.IDockableWindowDef.OnDestroy
        Me.Dispose() 'Call dispose
    End Sub

    Public ReadOnly Property UserData() As Object Implements ESRI.ArcGIS.Framework.IDockableWindowDef.UserData
        Get
            Return Nothing
        End Get
    End Property
#End Region

    Private Sub cboURLs_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboURLs.SelectedIndexChanged
        If cboURLs.SelectedIndex > -1 Then
            Dim url As String = cboURLs.SelectedValue.ToString()
            webBrowser1.Navigate(url)
        End If
    End Sub
    Private Class NameURLPair
        Private m_name As String
        Private m_url As String

        Public Property Name() As String
            Get
                Return m_name
            End Get
            Set(ByVal value As String)
                m_name = value
            End Set
        End Property

        Public Property URL() As String
            Get
                Return m_url
            End Get
            Set(ByVal value As String)
                m_url = value
            End Set
        End Property
    End Class


End Class


