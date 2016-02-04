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
Imports System.Collections

Imports ESRI.ArcGIS.Controls
Imports ESRI.ArcGIS.SystemUI
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Geometry


Public Class EditorForm

#Region "private members"
    Private m_mainForm As MainForm
    Private m_mapControl As IMapControl3
    Private m_commands As ArrayList
    Private m_engineEditor As IEngineEditor
    Private m_operationStack As IOperationStack
    Private m_pool As ICommandPool
#End Region

    
    Private Sub EditorForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        '*********  Important *************
        ' Obtain a reference to the MainForm using the EditHelper class
        m_mainForm = EditHelper.TheMainForm
        m_mapControl = m_mainForm.MapControl

        axBlankToolBar.SetBuddyControl(m_mapControl)
        axModifyToolbar.SetBuddyControl(m_mapControl)
        axReshapeToolbar.SetBuddyControl(m_mapControl)
        axUndoRedoToolbar.SetBuddyControl(m_mapControl)
        axCreateToolbar.SetBuddyControl(m_mapControl)

        'create and share CommandPool
        m_pool = New CommandPool()
        axCreateToolbar.CommandPool = m_pool
        axBlankToolBar.CommandPool = m_pool
        axModifyToolbar.CommandPool = m_pool
        axReshapeToolbar.CommandPool = m_pool
        axUndoRedoToolbar.CommandPool = m_pool

        'Create and share operation stack
        m_operationStack = New ControlsOperationStackClass()
        axModifyToolbar.OperationStack = m_operationStack
        axReshapeToolbar.OperationStack = m_operationStack
        axUndoRedoToolbar.OperationStack = m_operationStack
        axCreateToolbar.OperationStack = m_operationStack

        'load items for the axModifyToolbar
        axModifyToolbar.AddItem("esriControls.ControlsEditingEditTool", 0, 0, False, 0, esriCommandStyles.esriCommandStyleIconOnly)
        axModifyToolbar.AddItem("VertexCommands_CS.CustomVertexCommands", 1, 1, False, 0, esriCommandStyles.esriCommandStyleIconOnly)
        axModifyToolbar.AddItem("VertexCommands_CS.CustomVertexCommands", 2, 2, False, 0, esriCommandStyles.esriCommandStyleIconOnly)

        'load items for the axReshapeToolbar
        axReshapeToolbar.AddItem("esriControls.ControlsEditingEditTool", 0, 0, False, 0, esriCommandStyles.esriCommandStyleIconOnly)
        axReshapeToolbar.AddItem("esriControls.ControlsEditingSketchTool", 0, 1, False, 0, esriCommandStyles.esriCommandStyleIconOnly)

        'load items for the createtoolbar 
        axCreateToolbar.AddItem("esriControls.ControlsEditingSketchTool", 0, 0, False, 0, esriCommandStyles.esriCommandStyleIconOnly)

        'set up the EngineEditor
        m_engineEditor = New EngineEditorClass()
        m_engineEditor.EnableUndoRedo(True)
        CType(m_engineEditor, IEngineEditProperties2).StickyMoveTolerance = 10000
        Dim tbr As Object = axCreateToolbar.Object
        Dim engineEditorExt As IExtension = CType(m_engineEditor, IExtension)
        engineEditorExt.Startup(tbr) 'ensures that the operationStack will function correctly

        'Listen to OnSketchModified engine editor event
        AddHandler (CType(m_engineEditor, IEngineEditEvents_Event)).OnSketchModified, AddressOf OnSketchModified
        'listen to MainForm events in case application is closed while editing
        AddHandler EditHelper.TheMainForm.FormClosing, AddressOf MainForm_FormClosing

        m_commands = New ArrayList()
        m_commands.Add(cmdModify)
        m_commands.Add(cmdReshape)
        m_commands.Add(cmdCreate)

        DisableButtons()
        txtInfo.Text = ""
        Me.Size = New Size(242, 208)
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
        SetErrorLabel("")
        EditHelper.IsEditorFormOpen = True

    End Sub

    Private Sub cmdCreate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCreate.Click

        Dim edittask As IEngineEditTask = m_engineEditor.GetTaskByUniqueName("ControlToolsEditing_CreateNewFeatureTask")

        If Not edittask Is Nothing Then
            m_engineEditor.CurrentTask = edittask
            axCreateToolbar.CurrentTool = axCreateToolbar.GetItem(0).Command

            SetButtonColours(sender)
            txtInfo.Text = ""
            label1.Text = ""
            Me.flowLayoutPanel1.Controls.Clear()
            Me.flowLayoutPanel1.Controls.Add(axCreateToolbar)
            Me.flowLayoutPanel2.Controls.Clear()
            Me.flowLayoutPanel2.Controls.Add(axUndoRedoToolbar)
        End If

    End Sub

    Private Sub cmdModify_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdModify.Click

        Dim edittask As IEngineEditTask = m_engineEditor.GetTaskByUniqueName("ControlToolsEditing_ModifyFeatureTask")

        If Not edittask Is Nothing Then
            m_engineEditor.CurrentTask = edittask
            axModifyToolbar.CurrentTool = axModifyToolbar.GetItem(0).Command

            SetButtonColours(sender)
            txtInfo.Text = ""
            label1.Text = ""
            Me.flowLayoutPanel1.Controls.Clear()
            Me.flowLayoutPanel1.Controls.Add(axModifyToolbar)
            Me.flowLayoutPanel2.Controls.Clear()
            Me.flowLayoutPanel2.Controls.Add(axUndoRedoToolbar)
        End If

    End Sub

    Private Sub cmdReshape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdReshape.Click

        Dim edittask As IEngineEditTask = m_engineEditor.GetTaskByUniqueName("ReshapePolylineEditTask_Reshape Polyline_CSharp")

        If Not edittask Is Nothing Then
            m_engineEditor.CurrentTask = edittask
            axReshapeToolbar.CurrentTool = axReshapeToolbar.GetItem(0).Command

            SetButtonColours(sender)
            txtInfo.Text = ""
            label1.Text = ""
            Me.flowLayoutPanel1.Controls.Clear()
            Me.flowLayoutPanel1.Controls.Add(axReshapeToolbar)
            Me.flowLayoutPanel2.Controls.Clear()
            Me.flowLayoutPanel2.Controls.Add(axUndoRedoToolbar)
        End If

    End Sub

    Private Sub cmdEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEdit.Click
        If cmdEdit.Text = "Edit" Then
            Dim featlayer As IFeatureLayer = FindFeatureLayer("usa_major_highways")

            If Not featlayer Is Nothing Then
                m_engineEditor.StartEditing((CType(featlayer.FeatureClass, IDataset)).Workspace, m_mapControl.Map)
                Dim editLayer As IEngineEditLayers = CType(m_engineEditor, IEngineEditLayers)
                editLayer.SetTargetLayer(featlayer, 0)
                EnableButtons()

                cmdEdit.Text = "Finish"
                Dim color As Color = color.Red
                cmdEdit.BackColor = color
                cmdCreate_Click(cmdCreate, Nothing)
            End If
        Else
            SaveEdits()
            DisableButtons()
            cmdEdit.Text = "Edit"
            Dim color As Color = color.White
            cmdEdit.BackColor = color
            SetErrorLabel("")
        End If

    End Sub

    Private Sub EditorForm_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        CleanupOnFormClose()
    End Sub

    Private Sub MainForm_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        CleanupOnFormClose()
    End Sub

    Private Sub OnSketchModified()
        If (IsHighwaysEditValid) Then
            SetErrorLabel("")
        Else
            m_operationStack.Undo()
            SetErrorLabel("Invalid Edit")
        End If

    End Sub


#Region "private form and button management"


    Private Sub SetSelectableLayerStatus(ByVal enable As Boolean)
        Dim map As IMap = m_mapControl.Map
        'get the workspace to edit
        Dim i As Integer
        For i = 0 To map.LayerCount - 1 - 1 Step i + 1
            Dim layer As IFeatureLayer = CType(map.get_Layer(i), IFeatureLayer)
            layer.Selectable = enable

        Next
    End Sub


    Private Sub SetErrorLabel(ByVal message As String)
        label1.Text = message
    End Sub

    Private Sub DisableButtons()
        cmdReshape.Enabled = False
        cmdCreate.Enabled = False
        cmdModify.Enabled = False

        Dim button As Button
        For Each button In m_commands
            Dim color As Color = Color.White
            button.BackColor = color
        Next
    End Sub

    Private Sub EnableButtons()
        cmdReshape.Enabled = True
        cmdCreate.Enabled = True
        cmdModify.Enabled = True
    End Sub

    Private Sub SetButtonColours(ByVal clickedButton As Button)

        Dim color As Color

        Dim button As Button
        For Each button In m_commands
            If clickedButton Is button Then
                color = color.ForestGreen
            Else
                color = color.White
            End If

            button.BackColor = color

        Next
    End Sub

    Private Sub SetInfoLabel(ByVal sender As Object, ByVal index As Integer)
        Dim toolbarControl As AxToolbarControl = sender
        Dim toolbar As IToolbarControl2 = CType(toolbarControl.Object, IToolbarControl2)
        Dim item As IToolbarItem = toolbar.GetItem(index)
        Dim command As ICommand = item.Command
        txtInfo.Text = command.Message
    End Sub

    Private Sub axModifyToolbar_OnItemClick(ByVal sender As Object, ByVal e As IToolbarControlEvents_OnItemClickEvent)

        SetInfoLabel(sender, e.index)
    End Sub

    Private Sub axReshapeToolbar_OnItemClick(ByVal sender As Object, ByVal e As IToolbarControlEvents_OnItemClickEvent)
        SetInfoLabel(sender, e.index)
    End Sub

#End Region

#Region "private helper methods/properties"

    Private Sub CleanupOnFormClose()

        If (m_engineEditor.EditState = esriEngineEditState.esriEngineStateEditing) Then

            SaveEdits()
        End If

        EditHelper.IsEditorFormOpen = False

        'unregister the event handler
        RemoveHandler (CType(m_engineEditor, IEngineEditEvents_Event)).OnSketchModified, AddressOf OnSketchModified

    End Sub

    Private Sub SaveEdits()

        Dim saveEdits As Boolean = False

        If (m_engineEditor.HasEdits()) Then

            Dim message As String = "Do you wish to save edits?"
            Dim caption As String = "Save Edits"
            Dim buttons As MessageBoxButtons = MessageBoxButtons.YesNo
            Dim result As DialogResult

            result = MessageBox.Show(message, caption, buttons)

            If (result = DialogResult.Yes) Then
                saveEdits = True

            End If

            m_engineEditor.StopEditing(saveEdits)
        End If
    End Sub


    Private Function FindFeatureLayer(ByVal name As String) As IFeatureLayer

        Dim foundLayer As IFeatureLayer = Nothing
        Dim dataset As IDataset = Nothing
        Dim map As IMap = m_mapControl.Map

        Dim i As Integer
        For i = 0 To map.LayerCount - 1 Step i + 1
            Dim layer As IFeatureLayer = CType(map.Layer(i), IFeatureLayer)
            dataset = CType(layer.FeatureClass, IDataset)
            If (dataset.Name = name) Then

                foundLayer = layer
                Exit For
            End If

        Next

        Return foundLayer
    End Function


    Private ReadOnly Property IsHighwaysEditValid() As Boolean
        Get
            'put in all business logic here
            'In this example highways are not allowed to intersect the lakes layer
            Dim success As Boolean = True

            'get the edit sketch
            Dim editsketch As IEngineEditSketch = CType(m_engineEditor, IEngineEditSketch)

            'get the protected areas layer
            Dim lakesLayer As IFeatureLayer = FindFeatureLayer("us_lakes")

            'do a spatial filter 
            If Not editsketch Is Nothing And Not lakesLayer Is Nothing And Not editsketch.Geometry Is Nothing Then

                Dim cursor As IFeatureCursor = FindFeatures(editsketch.Geometry, lakesLayer.FeatureClass, esriSpatialRelEnum.esriSpatialRelIntersects, m_mapControl.Map)
                Dim feat As IFeature = cursor.NextFeature()

                'could put more sophisticated logic in here
                If Not feat Is Nothing Then
                    success = False
                End If

            End If
            Return success
        End Get
    End Property

    Private Function FindFeatures(ByVal geomeTry As IGeometry, ByVal featureClass As IFeatureClass, ByVal spatialRelationship As esriSpatialRelEnum, ByVal map As IMap) As IFeatureCursor

        Try
            '1 = esriSpatialRelIntersects
            '7 = esriSpatialWithin
            '8 = esriSpatialRelContains

            Dim spatialFilter As ISpatialFilter = New SpatialFilter()
            spatialFilter.Geometry = geomeTry
            spatialFilter.GeometryField = featureClass.ShapeFieldName
            spatialFilter.SpatialRel = spatialRelationship

            Dim featureCursor As IFeatureCursor = featureClass.Search(spatialFilter, False)
            Return featureCursor
        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine(ex.Message)
            Return Nothing
        End Try
    End Function

#End Region

End Class

