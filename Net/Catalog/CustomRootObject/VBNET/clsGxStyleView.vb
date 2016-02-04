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
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Catalog
Imports ESRI.ArcGIS.CatalogUI
Imports ESRI.ArcGIS.ADF.CATIDs

Imports System.Runtime.InteropServices

<ComClass(clsGxStyleView.ClassId, clsGxStyleView.InterfaceId, clsGxStyleView.EventsId), _
 ProgId("CustomRootObjectVBNET.clsGxStyleView")> _
Public Class clsGxStyleView
    Implements ESRI.ArcGIS.CatalogUI.IGxView
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
        GxPreviews.Register(regKey)

    End Sub
    ''' <summary>
    ''' Required method for ArcGIS Component Category unregistration -
    ''' Do not modify the contents of this method with the code editor.
    ''' </summary>
    Private Shared Sub ArcGISCategoryUnregistration(ByVal registerType As Type)
        Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
        GxPreviews.Unregister(regKey)

    End Sub

#End Region
#End Region


#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "b12f0fcd-0ff9-49d3-85c3-81329b2bafd1"
    Public Const InterfaceId As String = "010f70e3-107e-4a3c-83e5-eb8a3dfac558"
    Public Const EventsId As String = "5a92b1a5-e996-4758-9e2f-c0ca49d2f08f"
#End Region

    ' A creatable COM class must have a Public Sub New() 
    ' with no parameters, otherwise, the class will not be 
    ' registered in the COM registry and cannot be created 
    ' via CreateObject.

#Region "Member Variables"
    Private m_pApp As IGxApplication
    Private m_pCatalog As IGxCatalog
    Private m_pItem As clsGxStyleGalleryItem
    Private frmGxStyleView As New FrmGxStyleView
    'Subscribe to the events coming from the GxSelection.
    Private WithEvents m_pSelection As GxSelection
#End Region
    Public Sub New()
        MyBase.New()
    End Sub
    Public Sub PreviewItem(ByVal hDC As Long, ByVal r As tagRECT)
        If m_pItem Is Nothing Then Exit Sub
        m_pItem.PreviewItem(hDC, r)
    End Sub
    Private Sub m_pSelection_OnSelectionChanged(ByVal Selection As IGxSelection, ByRef initiator As Object) Handles m_pSelection.OnSelectionChanged
        Refresh()
        frmGxStyleView.RefreshView()
    End Sub

    Public Sub Activate(ByVal Application As ESRI.ArcGIS.CatalogUI.IGxApplication, ByVal Catalog As ESRI.ArcGIS.Catalog.IGxCatalog) Implements ESRI.ArcGIS.CatalogUI.IGxView.Activate
        m_pApp = Application
        m_pCatalog = Catalog
        m_pSelection = Catalog.Selection
        frmGxStyleView.GxStyleView = Me
        Refresh()
    End Sub

    Public Function Applies(ByVal Selection As ESRI.ArcGIS.Catalog.IGxObject) As Boolean Implements ESRI.ArcGIS.CatalogUI.IGxView.Applies
        Applies = TypeOf Selection Is clsGxStyleGalleryItem
    End Function

    Public ReadOnly Property ClassID1() As ESRI.ArcGIS.esriSystem.UID Implements ESRI.ArcGIS.CatalogUI.IGxView.ClassID
        Get
            Dim pUID As IUID
            pUID = New UID
            pUID.Value = "CustomRootObjectVBNET.clsGxStyleView"
            ClassID1 = pUID
        End Get
    End Property

    Public Sub Deactivate() Implements ESRI.ArcGIS.CatalogUI.IGxView.Deactivate
        frmGxStyleView.GxStyleView = Nothing
        m_pApp = Nothing
        m_pCatalog = Nothing
    End Sub

    Public ReadOnly Property DefaultToolbarCLSID() As ESRI.ArcGIS.esriSystem.UID Implements ESRI.ArcGIS.CatalogUI.IGxView.DefaultToolbarCLSID
        Get
            DefaultToolbarCLSID = Nothing
        End Get
    End Property

    Public ReadOnly Property hWnd() As Integer Implements ESRI.ArcGIS.CatalogUI.IGxView.hWnd
        Get
            hWnd = frmGxStyleView.picStylePreview.Handle
        End Get
    End Property

    Public ReadOnly Property Name() As String Implements ESRI.ArcGIS.CatalogUI.IGxView.Name
        Get
            Name = "Style"
        End Get
    End Property
    Public Sub Refresh() Implements ESRI.ArcGIS.CatalogUI.IGxView.Refresh
        Dim pSelection As IGxObject
        pSelection = m_pCatalog.SelectedObject
        If TypeOf pSelection Is clsGxStyleGalleryItem Then
            m_pItem = pSelection
            frmGxStyleView.RefreshView()
        Else
            m_pItem = Nothing
        End If
    End Sub

    Public ReadOnly Property SupportsTools() As Boolean Implements ESRI.ArcGIS.CatalogUI.IGxView.SupportsTools
        Get
            SupportsTools = False
        End Get
    End Property

    Public Sub SystemSettingChanged(ByVal flag As Integer, ByVal section As String) Implements ESRI.ArcGIS.CatalogUI.IGxView.SystemSettingChanged

    End Sub
End Class


