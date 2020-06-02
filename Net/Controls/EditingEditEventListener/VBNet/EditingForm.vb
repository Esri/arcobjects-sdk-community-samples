'Copyright 2019 Esri

'Licensed under the Apache License, Version 2.0 (the "License");
'you may not use this file except in compliance with the License.
'You may obtain a copy of the License at

'    http://www.apache.org/licenses/LICENSE-2.0

'Unless required by applicable law or agreed to in writing, software
'distributed under the License is distributed on an "AS IS" BASIS,
'WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.

'See the License for the specific language governing permissions and
'limitations under the License.
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports System.IO

Imports ESRI.ArcGIS.Controls
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Display
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.SystemUI
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.DataSourcesFile
Imports ESRI.ArcGIS.DataSourcesGDB

Imports Application.Events

Public Class EditingForm


  Private m_toolbarMenu As IToolbarMenu
  Private m_editor As IEngineEditor
  Friend m_selectTab As TabPage
  Friend m_listenTab As TabPage
    Private eventListener As EventListener



  Private Sub EditingForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    m_editor = New EngineEditorClass()

    'set buddy controls
    AxTOCControl1.SetBuddyControl(AxMapControl1)
    AxEditorToolbar.SetBuddyControl(AxMapControl1)
    AxToolbarControl1.SetBuddyControl(AxMapControl1)

    'Add items to the ToolbarControl
    AxToolbarControl1.AddItem("esriControls.ControlsOpenDocCommand", 0, -1, False, 0, esriCommandStyles.esriCommandStyleIconOnly)
    AxToolbarControl1.AddItem("esriControls.ControlsSaveAsDocCommand", 0, -1, False, 0, esriCommandStyles.esriCommandStyleIconOnly)
    AxToolbarControl1.AddItem("esriControls.ControlsAddDataCommand", 0, -1, False, 0, esriCommandStyles.esriCommandStyleIconOnly)
    AxToolbarControl1.AddItem("esriControls.ControlsMapZoomInTool", 0, -1, True, 0, esriCommandStyles.esriCommandStyleIconOnly)
    AxToolbarControl1.AddItem("esriControls.ControlsMapZoomOutTool", 0, -1, False, 0, esriCommandStyles.esriCommandStyleIconOnly)
    AxToolbarControl1.AddItem("esriControls.ControlsMapPanTool", 0, -1, False, 0, esriCommandStyles.esriCommandStyleIconOnly)
    AxToolbarControl1.AddItem("esriControls.ControlsMapFullExtentCommand", 0, -1, False, 0, esriCommandStyles.esriCommandStyleIconOnly)
    AxToolbarControl1.AddItem("esriControls.ControlsMapZoomToLastExtentBackCommand", 0, -1, False, 0, esriCommandStyles.esriCommandStyleIconOnly)
    AxToolbarControl1.AddItem("esriControls.ControlsMapZoomToLastExtentForwardCommand", 0, -1, False, 0, esriCommandStyles.esriCommandStyleIconOnly)

    'Add items to the custom editor toolbar          
    AxEditorToolbar.AddItem("esriControls.ControlsEditingEditorMenu", 0, -1, False, 0, esriCommandStyles.esriCommandStyleIconOnly)
    AxEditorToolbar.AddItem("esriControls.ControlsEditingEditTool", 0, -1, False, 0, esriCommandStyles.esriCommandStyleIconOnly)
    AxEditorToolbar.AddItem("esriControls.ControlsEditingSketchTool", 0, -1, False, 0, esriCommandStyles.esriCommandStyleIconOnly)
    AxEditorToolbar.AddItem("esriControls.ControlsUndoCommand", 0, -1, True, 0, esriCommandStyles.esriCommandStyleIconOnly)
    AxEditorToolbar.AddItem("esriControls.ControlsRedoCommand", 0, -1, False, 0, esriCommandStyles.esriCommandStyleIconOnly)
    AxEditorToolbar.AddItem("esriControls.ControlsEditingTaskToolControl", 0, -1, True, 0, esriCommandStyles.esriCommandStyleIconOnly)
    AxEditorToolbar.AddItem("esriControls.ControlsEditingTargetToolControl", 0, -1, True, 0, esriCommandStyles.esriCommandStyleIconOnly)

    'Create a popup menu
    m_toolbarMenu = New ToolbarMenuClass()
    m_toolbarMenu.AddItem("esriControls.ControlsEditingSketchContextMenu", 0, 0, False, esriCommandStyles.esriCommandStyleTextOnly)

    'share the command pool
    AxToolbarControl1.CommandPool = AxEditorToolbar.CommandPool
    m_toolbarMenu.CommandPool = AxEditorToolbar.CommandPool

    'Create an operation stack for the undo and redo commands to use
    Dim operationStack As IOperationStack = New ControlsOperationStackClass()
    AxEditorToolbar.OperationStack = operationStack

    ' Fill the Check List of Events for Selection 
    Dim tabControl As TabControl = TryCast(eventTabControl, TabControl)
    Dim enumTabs As System.Collections.IEnumerator = tabControl.TabPages.GetEnumerator()
    enumTabs.MoveNext()
    m_listenTab = TryCast(enumTabs.Current, TabPage)
    enumTabs.MoveNext()
    m_selectTab = TryCast(enumTabs.Current, TabPage)

    Dim editEventList As CheckedListBox = TryCast(m_selectTab.GetNextControl(m_selectTab, True), CheckedListBox)
    AddHandler editEventList.ItemCheck, AddressOf editEventList_ItemCheck

    Dim listEvent As ListBox = TryCast(m_listenTab.GetNextControl(m_listenTab, True), ListBox)
    AddHandler listEvent.MouseDown, AddressOf listEvent_MouseDown

    eventListener = New EventListener(m_editor)

    AddHandler eventListener.Changed, AddressOf eventListener_Changed

    'populate the editor events 
    editEventList.Items.AddRange([Enum].GetNames(GetType(EventListener.EditorEvent)))


    'add some sample line data to the map
    Dim workspaceFactory As IWorkspaceFactory = New AccessWorkspaceFactoryClass()
    'relative file path to the sample data from EXE location
    Dim filePath As String = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
    filePath = System.IO.Path.Combine (filePath, "ArcGIS\data\StreamflowDateTime\Streamflow.mdb")
    
    Dim workspace As IFeatureWorkspace = CType(workspaceFactory.OpenFromFile(filePath, AxMapControl1.hWnd), IFeatureWorkspace)

    'Add the various layers 
    Dim featureLayer1 As IFeatureLayer = New FeatureLayerClass()
    featureLayer1.Name = "Watershed"
    featureLayer1.Visible = True
    featureLayer1.FeatureClass = workspace.OpenFeatureClass("Watershed")
    AxMapControl1.Map.AddLayer(DirectCast(featureLayer1, ILayer))

    Dim featureLayer2 As IFeatureLayer = New FeatureLayerClass()
    featureLayer2.Name = "TimSerTool"
    featureLayer2.Visible = True
    featureLayer2.FeatureClass = workspace.OpenFeatureClass("TimSerTool")
    AxMapControl1.Map.AddLayer(DirectCast(featureLayer2, ILayer))

  End Sub

    Private Sub AxMapControl1_OnMouseDown(ByVal sender As System.Object, ByVal e As ESRI.ArcGIS.Controls.IMapControlEvents2_OnMouseDownEvent) Handles AxMapControl1.OnMouseDown
        'popup the menu
        If e.button = 2 Then m_toolbarMenu.PopupMenu(e.x, e.y, AxMapControl1.hWnd)
    End Sub

  Private Sub editEventList_ItemCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemCheckEventArgs)
    ' start or stop listening for event based on checked state 
    eventListener.ListenToEvents(DirectCast(e.Index, EventListener.EditorEvent), e.NewValue = CheckState.Checked)
  End Sub
  Private Sub eventListener_Changed(ByVal sender As Object, ByVal e As EditorEventArgs)
    DirectCast(m_listenTab.GetNextControl(m_listenTab, True), ListBox).Items.Add(e.eventType.ToString())
  End Sub
  Private Sub listEvent_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs)
    If e.Button = Windows.Forms.MouseButtons.Right Then
      Me.lstEditorEvents.Items.Clear()
      Me.lstEditorEvents.Refresh()
    End If
  End Sub

  Private Sub clearEvents_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles clearEvents.Click
    Me.lstEditorEvents.Items.Clear()
    Me.lstEditorEvents.Refresh()
  End Sub

  Private Sub selectAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles selectAll.Click
    Dim editEventList As CheckedListBox
    editEventList = m_selectTab.GetNextControl(m_selectTab, True)
    Dim i As Integer
    For i = 0 To editEventList.Items.Count - 1 Step i + 1
      editEventList.SetItemChecked(i, True)
    Next
  End Sub

  Private Sub deselectAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles deselectAll.Click
    Dim editEventList As CheckedListBox
    editEventList = m_selectTab.GetNextControl(m_selectTab, True)
    Dim i As Integer
    For i = 0 To editEventList.Items.Count - 1 Step i + 1
      editEventList.SetItemChecked(i, False)
    Next
  End Sub
End Class
