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
Imports ESRI.ArcGIS.ArcMapUI
Imports ESRI.ArcGIS.Carto

<ComClass(TOCLayerFilter.ClassId, TOCLayerFilter.InterfaceId, TOCLayerFilter.EventsId), _
 ProgId("TOCLayerFilterVB.TOCLayerFilter")> _
 Public Class TOCLayerFilter
    Implements IContentsView3

    Private m_application As IApplication
    Private m_visible As Boolean
    Private m_contextItem As Object
    Private m_isProcessEvents As Boolean

#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "33a52cdc-c942-4429-9783-1a8627532775"
    Public Const InterfaceId As String = "0a4a8ab9-668b-41ef-a3a7-f35adbb80a20"
    Public Const EventsId As String = "982ce2e2-a0f7-4b8f-bc47-fbac92d50ea6"
#End Region
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
    ContentsViews.Register(regKey)
  End Sub
  ''' <summary>
  ''' Required method for ArcGIS Component Category unregistration -
  ''' Do not modify the contents of this method with the code editor.
  ''' </summary>
  Private Shared Sub ArcGISCategoryUnregistration(ByVal registerType As Type)
    Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
    ContentsViews.Unregister(regKey)
  End Sub

#End Region
#End Region

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub cboLayerType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboLayerType.SelectedIndexChanged
        tvwLayer.SuspendLayout()
        RefreshList()
        tvwLayer.ResumeLayout()
    End Sub

    Private Sub tvwLayer_NodeMouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles tvwLayer.NodeMouseClick
        If e.Button = Windows.Forms.MouseButtons.Right Then
            'Set item for context menu commands to work with
            m_contextItem = e.Node.Tag

            'Show context menu
            Dim menuID As New UIDClass()
            If TypeOf e.Node.Tag Is IMap Then 'data frame
                menuID.Value = "{F42891B5-2C92-11D2-AE07-080009EC732A}" 'Data Frame Context Menu (TOC) 
            Else 'Layer - custom menu
                menuID.Value = "{fc4032dd-323e-401e-8642-2d4a25b435c1}" 'TOCFilterLayerContextMenu class id
            End If

            Dim cmdBar As ICommandBar = DirectCast(m_application.Document.CommandBars.Find(menuID, False, False), ICommandBar)
            cmdBar.Popup(0, 0)
        End If
    End Sub

    Private Sub RefreshList()
        tvwLayer.Nodes.Clear()
        Dim layerFilter As UID = Nothing
        If cboLayerType.SelectedItem.Equals("Feature Layers") Then
            layerFilter = New UIDClass()
            layerFilter.Value = "{40A9E885-5533-11d0-98BE-00805F7CED21}" 'IFeatureLayer
        ElseIf cboLayerType.SelectedItem.Equals("Raster Layers") Then
            layerFilter = New UIDClass()
            layerFilter.Value = GetType(IRasterLayer).GUID.ToString("B")
        ElseIf cboLayerType.SelectedItem.Equals("Data Layers") Then
            layerFilter = New UIDClass()
            layerFilter.Value = "{6CA416B1-E160-11D2-9F4E-00C04F6BC78E}" 'IDataLayer
        End If

        Dim document As IMxDocument = DirectCast(m_application.Document, IMxDocument)
        Dim maps As IMaps = document.Maps
        For i As Integer = 0 To maps.Count - 1
            Dim map As IMap = maps.Item(i)
            Dim mapNode As Windows.Forms.TreeNode = tvwLayer.Nodes.Add(map.Name)
            mapNode.Tag = map
            If map.LayerCount > 0 Then
                Dim layers As IEnumLayer = map.Layers(layerFilter, True)
                layers.Reset()
                Dim lyr As ILayer = layers.Next()
                Do Until lyr Is Nothing
                    Dim lyrNode As Windows.Forms.TreeNode = mapNode.Nodes.Add(lyr.Name)
                    lyrNode.Tag = lyr

                    lyr = layers.Next()
                Loop

                mapNode.ExpandAll()
            End If

        Next
    End Sub

#Region "Add Event Wiring for New/Open Documents"
    'Event member variables
    Private m_docEvents As IDocumentEvents_Event
    'Wiring
    Private Sub SetUpDocumentEvent(ByVal myDocument As IDocument)
        m_docEvents = TryCast(myDocument, IDocumentEvents_Event)
        AddHandler m_docEvents.OpenDocument, AddressOf RefreshList
        AddHandler m_docEvents.NewDocument, AddressOf RefreshList
    End Sub
#End Region

#Region "IContentsView3 Implementation"

    Public Sub Activate(ByVal parentHWnd As Integer, ByVal Document As ESRI.ArcGIS.ArcMapUI.IMxDocument) Implements IContentsView.Activate, IContentsView2.Activate, IContentsView3.Activate
        If m_application Is Nothing Then
            m_application = DirectCast(Document, IDocument).Parent
            If cboLayerType.SelectedIndex < 0 Then
                cboLayerType.SelectedIndex = 0
            Else
                RefreshList()
            End If

            SetUpDocumentEvent(DirectCast(Document, IDocument))
        End If
    End Sub

    Public Sub AddToSelectedItems(ByVal item As Object) Implements IContentsView.AddToSelectedItems, IContentsView2.AddToSelectedItems, IContentsView3.AddToSelectedItems

    End Sub

    Public Property ContextItem() As Object Implements IContentsView.ContextItem, IContentsView2.ContextItem, IContentsView3.ContextItem
        Get
            Return m_contextItem
        End Get
        Set(ByVal value As Object)
            'not implemented
        End Set
    End Property

    Public Sub Deactivate() Implements IContentsView.Deactivate, IContentsView2.Deactivate, IContentsView3.Deactivate

    End Sub

    Public ReadOnly Property hWnd() As Integer Implements IContentsView.hWnd, IContentsView2.hWnd, IContentsView3.hWnd
        Get
            Return Me.Handle.ToInt32()
        End Get
    End Property

    Public ReadOnly Property Name1() As String Implements IContentsView.Name, IContentsView2.Name, IContentsView3.Name
        Get
            Return "Layer Types (VB.NET)"
        End Get
    End Property

    Public WriteOnly Property ProcessEvents() As Boolean Implements IContentsView.ProcessEvents, IContentsView2.ProcessEvents, IContentsView3.ProcessEvents
        Set(ByVal value As Boolean)
            m_isProcessEvents = value
        End Set
    End Property

    Public Sub Refresh1(ByVal item As Object) Implements IContentsView.Refresh, IContentsView2.Refresh, IContentsView3.Refresh
        If item IsNot Me Then
            'when items are added, removed, reordered
            tvwLayer.SuspendLayout()
            RefreshList()
            tvwLayer.ResumeLayout()
        End If
    End Sub

    Public Sub RemoveFromSelectedItems(ByVal item As Object) Implements IContentsView.RemoveFromSelectedItems, IContentsView2.RemoveFromSelectedItems, IContentsView3.RemoveFromSelectedItems

    End Sub

    Public Property SelectedItem() As Object Implements IContentsView.SelectedItem, IContentsView2.SelectedItem, IContentsView3.SelectedItem
        Get
            'No Multiselect so return selected node
            If tvwLayer.SelectedNode IsNot Nothing Then
                Return tvwLayer.SelectedNode.Tag 'Layer
            Else
                Return Nothing
            End If
        End Get
        Set(ByVal value As Object)
            'not implemented
        End Set
    End Property

    Public Property ShowLines() As Boolean Implements IContentsView.ShowLines, IContentsView2.ShowLines, IContentsView3.ShowLines
        Get
            Return tvwLayer.ShowLines
        End Get
        Set(ByVal value As Boolean)
            tvwLayer.ShowLines = value
        End Set
    End Property

    Public Property Visible1() As Boolean Implements IContentsView.Visible, IContentsView2.Visible, IContentsView3.Visible
        Get
            Return m_visible
        End Get
        Set(ByVal value As Boolean)
            m_visible = value
        End Set
    End Property

    Public Sub BasicActivate(ByVal parentHWnd As Integer, ByVal Document As ESRI.ArcGIS.Framework.IDocument) Implements IContentsView2.BasicActivate, IContentsView3.BasicActivate

    End Sub

    Public ReadOnly Property Bitmap() As Integer Implements IContentsView3.Bitmap
        Get
            Dim bitmapResourceName As String = Me.GetType().Name + ".bmp"
            Dim bmp As System.Drawing.Bitmap = New System.Drawing.Bitmap(Me.GetType(), bitmapResourceName)
            bmp.MakeTransparent(bmp.GetPixel(1, 1)) 'alternatively use a .png with transparency
            Return bmp.GetHbitmap().ToInt32()
        End Get
    End Property

    Public ReadOnly Property Tooltip() As String Implements IContentsView3.Tooltip
        Get
            Return "Layer Types (VB.NET)"
        End Get
    End Property
#End Region

End Class
