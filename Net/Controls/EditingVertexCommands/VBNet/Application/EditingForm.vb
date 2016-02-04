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

Public Class EditingForm


    Private m_toolbarMenu As IToolbarMenu


    Private Sub EditingForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

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
        AxEditorToolbar.AddItem("VertexCommands_VB.VertexCommandsMenu", 0, -1, False, 0, esriCommandStyles.esriCommandStyleIconAndText)
        AxEditorToolbar.AddItem("esriControls.ControlsEditingEditTool", 0, -1, False, 0, esriCommandStyles.esriCommandStyleIconOnly)
        AxEditorToolbar.AddItem("esriControls.ControlsEditingSketchTool", 0, -1, False, 0, esriCommandStyles.esriCommandStyleIconOnly)
        AxEditorToolbar.AddItem("esriControls.ControlsUndoCommand", 0, -1, True, 0, esriCommandStyles.esriCommandStyleIconOnly)
        AxEditorToolbar.AddItem("esriControls.ControlsRedoCommand", 0, -1, False, 0, esriCommandStyles.esriCommandStyleIconOnly)
        AxEditorToolbar.AddItem("esriControls.ControlsEditingTargetToolControl", 0, -1, True, 0, esriCommandStyles.esriCommandStyleIconOnly)
        AxEditorToolbar.AddItem("esriControls.ControlsEditingTaskToolControl", 0, -1, True, 0, esriCommandStyles.esriCommandStyleIconOnly)
       
        'Create a popup menu
        m_toolbarMenu = New ToolbarMenuClass()
        m_toolbarMenu.AddItem("esriControls.ControlsEditingSketchContextMenu", 0, 0, False, esriCommandStyles.esriCommandStyleTextOnly)
        
        'share the command pool
        AxToolbarControl1.CommandPool = AxEditorToolbar.CommandPool
        m_toolbarMenu.CommandPool = AxEditorToolbar.CommandPool

        'Create an operation stack for the undo and redo commands to use
        Dim operationStack As IOperationStack = New ControlsOperationStackClass()
        AxEditorToolbar.OperationStack = operationStack

        'add some sample line data to the map
        Dim workspaceFactory As IWorkspaceFactory = New ShapefileWorkspaceFactoryClass()
        'relative file path to the sample data from EXE location
        Dim filePath As String = "..\..\..\..\data\USAMajorHighways"
        Dim workspace As IFeatureWorkspace = CType(workspaceFactory.OpenFromFile(filePath, AxMapControl1.hWnd), IFeatureWorkspace)
        Dim featureLayer As IFeatureLayer = New FeatureLayerClass()

        featureLayer.Name = "Highways"
        featureLayer.Visible = True
        featureLayer.FeatureClass = workspace.OpenFeatureClass("usa_major_highways")

        AxMapControl1.Map.AddLayer(CType(featureLayer, ILayer))


    End Sub

    Private Sub AxMapControl1_OnMouseDown(ByVal sender As System.Object, ByVal e As ESRI.ArcGIS.Controls.IMapControlEvents2_OnMouseDownEvent) Handles AxMapControl1.OnMouseDown
        'popup the menu
        If e.button = 2 Then m_toolbarMenu.PopupMenu(e.x, e.y, AxMapControl1.hWnd)
    End Sub
End Class

