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
Imports ESRI.ArcGIS.ADF.CATIDs
Imports System.Runtime.InteropServices

<ComClass(TextView.ClassId, TextView.InterfaceId, TextView.EventsId), _
 ProgId("TextTabVBNET.TextView")> _
Public Class TextView
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
        GxTabViews.Register(regKey)

    End Sub
    ''' <summary>
    ''' Required method for ArcGIS Component Category unregistration -
    ''' Do not modify the contents of this method with the code editor.
    ''' </summary>
    Private Shared Sub ArcGISCategoryUnregistration(ByVal registerType As Type)
        Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
        GxTabViews.Unregister(regKey)

    End Sub

#End Region
#End Region


#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "1629e1b8-2a12-4fae-a509-75930843bcaf"
    Public Const InterfaceId As String = "63a2a982-39d8-4996-84f5-0ec678abab12"
    Public Const EventsId As String = "836e2190-9c11-4df9-9e47-2a4c3dde7710"
#End Region

    ' A creatable COM class must have a Public Sub New() 
    ' with no parameters, otherwise, the class will not be 
    ' registered in the COM registry and cannot be created 
    ' via CreateObject.
#Region "Member Variables"
    Private WithEvents m_pSelection As GxSelection
    Private m_txtStream As Object
    Private m_fso As Object
    Private frmTextView As New FrmTextView
#End Region
    Public Sub New()
        MyBase.New()
    End Sub
    Private Sub OnSelectionChanged(ByVal Selection As IGxSelection, ByRef initiator As Object) Handles m_pSelection.OnSelectionChanged
        'Refresh view
        Refresh()
    End Sub

    Public Sub Activate(ByVal Application As ESRI.ArcGIS.CatalogUI.IGxApplication, ByVal Catalog As ESRI.ArcGIS.Catalog.IGxCatalog) Implements ESRI.ArcGIS.CatalogUI.IGxView.Activate
        m_pSelection = Application.Selection
        Refresh()
    End Sub

    Public Function Applies(ByVal Selection As ESRI.ArcGIS.Catalog.IGxObject) As Boolean Implements ESRI.ArcGIS.CatalogUI.IGxView.Applies
        'Set applies
        Applies = (Not Selection Is Nothing) And (TypeOf Selection Is IGxTextFile)
    End Function

    Public ReadOnly Property ClassID1() As ESRI.ArcGIS.esriSystem.UID Implements ESRI.ArcGIS.CatalogUI.IGxView.ClassID
        Get
            'Set class ID
            Dim pUID As New UID
            pUID.Value = "TextTabVBNET.TextView"
            ClassID1 = pUID
        End Get
    End Property

    Public Sub Deactivate() Implements ESRI.ArcGIS.CatalogUI.IGxView.Deactivate
        'Prevent circular reference
        If Not m_pSelection Is Nothing Then m_pSelection = Nothing
    End Sub

    Public ReadOnly Property DefaultToolbarCLSID() As ESRI.ArcGIS.esriSystem.UID Implements ESRI.ArcGIS.CatalogUI.IGxView.DefaultToolbarCLSID
        Get
            'You can set it to the CLSID of the toolbar you want to use
            DefaultToolbarCLSID = Nothing
        End Get
    End Property

    Public ReadOnly Property hWnd() As Integer Implements ESRI.ArcGIS.CatalogUI.IGxView.hWnd
        Get
            Try
                'Set view handle to be the control handle
                hWnd = frmTextView.txtContents.Handle
            Catch ex As Exception
                System.Windows.Forms.MessageBox.Show(ex.ToString())
            End Try
        End Get
    End Property

    Public ReadOnly Property Name() As String Implements ESRI.ArcGIS.CatalogUI.IGxView.Name
        Get
            'Set view name
            Name = "Text"
        End Get
    End Property

    Public Sub Refresh() Implements ESRI.ArcGIS.CatalogUI.IGxView.Refresh
        Dim pGxSelection As IGxSelection
        Dim pLocation As IGxObject
        pGxSelection = m_pSelection
        pLocation = pGxSelection.Location

        'Clean up
        frmTextView.txtContents.Clear()

        Dim fname As String
        fname = LCase(pLocation.Name)

        If InStr(fname, ".txt") Then
            If m_fso Is Nothing Then m_fso = CreateObject("Scripting.FileSystemObject")
            Const forReading = 1
            Try
                m_txtStream = m_fso.OpenTextFile(pLocation.FullName, forReading)
                frmTextView.txtContents.Text = m_txtStream.ReadAll.ToString
                m_txtStream.Close()
            Catch ex As Exception
                System.Windows.Forms.MessageBox.Show(ex.ToString())
            Finally
                m_fso = Nothing
                m_txtStream = Nothing
            End Try
        End If
    End Sub

    Public ReadOnly Property SupportsTools() As Boolean Implements ESRI.ArcGIS.CatalogUI.IGxView.SupportsTools
        Get
            'If you want this view to support tools, you can set it to "True"
            SupportsTools = False
        End Get
    End Property

    Public Sub SystemSettingChanged(ByVal flag As Integer, ByVal section As String) Implements ESRI.ArcGIS.CatalogUI.IGxView.SystemSettingChanged
        'Do nothing
    End Sub
End Class


