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
Imports ESRI.ArcGIS.Controls
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.SystemUI
Imports ESRI.ArcGIS


Public Class FeatureEditing
    Inherits System.Windows.Forms.Form

    Private m_toolbarMenuSketch As IToolbarMenu
    Private m_toolbarMenuVertex As IToolbarMenu
    Private m_CommandPool As ICommandPool
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Private m_engineEditor As IEngineEditor
    <STAThread()> _
    Shared Sub Main()

        'Load runtime 
        If Not RuntimeManager.Bind(ProductCode.Engine) Then
            If Not RuntimeManager.Bind(ProductCode.Desktop) Then
                MessageBox.Show("Unable to bind to ArcGIS runtime. Application will be shut down.")
                System.Environment.Exit(1) ' Force exit or other indication in the application
            End If
        End If

        Application.Run(New FeatureEditing())
    End Sub

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        ESRI.ArcGIS.ADF.COMSupport.AOUninitialize.Shutdown()
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents btnOptions2 As System.Windows.Forms.Button
    Friend WithEvents btnOptions1 As System.Windows.Forms.Button
    Friend WithEvents AxToolbarControl1 As ESRI.ArcGIS.Controls.AxToolbarControl
    Friend WithEvents AxToolbarControl2 As ESRI.ArcGIS.Controls.AxToolbarControl
    Friend WithEvents AxTOCControl1 As ESRI.ArcGIS.Controls.AxTOCControl
    Friend WithEvents AxMapControl1 As ESRI.ArcGIS.Controls.AxMapControl
    Friend WithEvents AxLicenseControl1 As ESRI.ArcGIS.Controls.AxLicenseControl
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FeatureEditing))
        Me.btnOptions2 = New System.Windows.Forms.Button
        Me.btnOptions1 = New System.Windows.Forms.Button
        Me.AxToolbarControl1 = New ESRI.ArcGIS.Controls.AxToolbarControl
        Me.AxToolbarControl2 = New ESRI.ArcGIS.Controls.AxToolbarControl
        Me.AxTOCControl1 = New ESRI.ArcGIS.Controls.AxTOCControl
        Me.AxMapControl1 = New ESRI.ArcGIS.Controls.AxMapControl
        Me.AxLicenseControl1 = New ESRI.ArcGIS.Controls.AxLicenseControl
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
        CType(Me.AxToolbarControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AxToolbarControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AxTOCControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AxMapControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AxLicenseControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnOptions2
        '
        Me.btnOptions2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnOptions2.Location = New System.Drawing.Point(839, 38)
        Me.btnOptions2.Name = "btnOptions2"
        Me.btnOptions2.Size = New System.Drawing.Size(87, 24)
        Me.btnOptions2.TabIndex = 8
        Me.btnOptions2.Text = "Edit Options2"
        '
        'btnOptions1
        '
        Me.btnOptions1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnOptions1.Location = New System.Drawing.Point(839, 3)
        Me.btnOptions1.Name = "btnOptions1"
        Me.btnOptions1.Size = New System.Drawing.Size(87, 23)
        Me.btnOptions1.TabIndex = 7
        Me.btnOptions1.Text = "Edit Options1"
        '
        'AxToolbarControl1
        '
        Me.AxToolbarControl1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.AxToolbarControl1.Location = New System.Drawing.Point(158, 38)
        Me.AxToolbarControl1.Name = "AxToolbarControl1"
        Me.AxToolbarControl1.OcxState = CType(resources.GetObject("AxToolbarControl1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxToolbarControl1.Size = New System.Drawing.Size(669, 28)
        Me.AxToolbarControl1.TabIndex = 9
        '
        'AxToolbarControl2
        '
        Me.AxToolbarControl2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.AxToolbarControl2.Location = New System.Drawing.Point(158, 3)
        Me.AxToolbarControl2.Name = "AxToolbarControl2"
        Me.AxToolbarControl2.OcxState = CType(resources.GetObject("AxToolbarControl2.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxToolbarControl2.Size = New System.Drawing.Size(669, 28)
        Me.AxToolbarControl2.TabIndex = 10
        '
        'AxTOCControl1
        '
        Me.AxTOCControl1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.AxTOCControl1.Location = New System.Drawing.Point(3, 71)
        Me.AxTOCControl1.Name = "AxTOCControl1"
        Me.AxTOCControl1.OcxState = CType(resources.GetObject("AxTOCControl1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxTOCControl1.Size = New System.Drawing.Size(147, 476)
        Me.AxTOCControl1.TabIndex = 11
        '
        'AxMapControl1
        '
        Me.AxMapControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.AxMapControl1.Location = New System.Drawing.Point(158, 71)
        Me.AxMapControl1.Name = "AxMapControl1"
        Me.AxMapControl1.OcxState = CType(resources.GetObject("AxMapControl1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxMapControl1.Size = New System.Drawing.Size(669, 476)
        Me.AxMapControl1.TabIndex = 12
        '
        'AxLicenseControl1
        '
        Me.AxLicenseControl1.Enabled = True
        Me.AxLicenseControl1.Location = New System.Drawing.Point(3, 3)
        Me.AxLicenseControl1.Name = "AxLicenseControl1"
        Me.AxLicenseControl1.OcxState = CType(resources.GetObject("AxLicenseControl1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxLicenseControl1.Size = New System.Drawing.Size(32, 32)
        Me.AxLicenseControl1.TabIndex = 13
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 3
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18.7247!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 81.27531!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 98.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.AxLicenseControl1, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.AxMapControl1, 1, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.AxTOCControl1, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.btnOptions2, 2, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.AxToolbarControl1, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.btnOptions1, 2, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.AxToolbarControl2, 1, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(3, 2)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 3
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 51.69492!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 48.30508!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 481.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(929, 550)
        Me.TableLayoutPanel1.TabIndex = 14
        '
        'FeatureEditing
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(1130, 641)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.KeyPreview = True
        Me.Name = "FeatureEditing"
        Me.Text = "Feature Editing"
        CType(Me.AxToolbarControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AxToolbarControl2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AxTOCControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AxMapControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AxLicenseControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'Set buddy controls
        AxTOCControl1.SetBuddyControl(AxMapControl1)
        AxToolbarControl1.SetBuddyControl(AxMapControl1)
        AxToolbarControl2.SetBuddyControl(AxMapControl1)

        'Share command pools
        m_CommandPool = New CommandPoolClass
        AxToolbarControl1.CommandPool = m_CommandPool
        AxToolbarControl2.CommandPool = m_CommandPool

        'Add items to the ToolbarControl
        AxToolbarControl1.AddItem("esriControls.ControlsEditingEditorMenu", 0, -1, False, 0, esriCommandStyles.esriCommandStyleIconOnly)
        AxToolbarControl1.AddItem("esriControls.ControlsEditingEditTool", 0, -1, False, 0, esriCommandStyles.esriCommandStyleIconOnly)
        AxToolbarControl1.AddItem("esriControls.ControlsEditingSketchTool", 0, -1, False, 0, esriCommandStyles.esriCommandStyleIconOnly)
        AxToolbarControl1.AddItem("esriControls.ControlsUndoCommand", 0, -1, True, 0, esriCommandStyles.esriCommandStyleIconOnly)
        AxToolbarControl1.AddItem("esriControls.ControlsRedoCommand", 0, -1, False, 0, esriCommandStyles.esriCommandStyleIconOnly)
        AxToolbarControl1.AddItem("esriControls.ControlsEditingTargetToolControl", 0, -1, True, 0, esriCommandStyles.esriCommandStyleIconOnly)
        AxToolbarControl1.AddItem("esriControls.ControlsEditingTaskToolControl", 0, -1, True, 0, esriCommandStyles.esriCommandStyleIconOnly)
        AxToolbarControl1.AddItem("esriControls.ControlsEditingAttributeCommand", 0, -1, False, 0, esriCommandStyles.esriCommandStyleIconOnly)
        AxToolbarControl1.AddItem("esriControls.ControlsEditingSketchPropertiesCommand", 0, -1, False, 0, esriCommandStyles.esriCommandStyleIconOnly)
        AxToolbarControl1.AddItem("esriControls.ControlsEditingCutCommand", 0, -1, True, 0, esriCommandStyles.esriCommandStyleIconOnly)
        AxToolbarControl1.AddItem("esriControls.ControlsEditingPasteCommand", 0, -1, False, 0, esriCommandStyles.esriCommandStyleIconOnly)
        AxToolbarControl1.AddItem("esriControls.ControlsEditingCopyCommand", 0, -1, False, 0, esriCommandStyles.esriCommandStyleIconOnly)
        AxToolbarControl1.AddItem("esriControls.ControlsEditingClearCommand", 0, -1, False, 0, esriCommandStyles.esriCommandStyleIconOnly)

        AxToolbarControl2.AddItem("esriControls.ControlsOpenDocCommand", 0, -1, False, 0, esriCommandStyles.esriCommandStyleIconOnly)
        AxToolbarControl2.AddItem("esriControls.ControlsAddDataCommand", 0, -1, False, 0, esriCommandStyles.esriCommandStyleIconOnly)
        AxToolbarControl2.AddItem("esriControls.ControlsSaveAsDocCommand", 0, -1, False, 0, esriCommandStyles.esriCommandStyleIconOnly)
        AxToolbarControl2.AddItem("esriControls.ControlsMapZoomInTool", 0, -1, True, 0, esriCommandStyles.esriCommandStyleIconOnly)
        AxToolbarControl2.AddItem("esriControls.ControlsMapZoomOutTool", 0, -1, False, 0, esriCommandStyles.esriCommandStyleIconOnly)
        AxToolbarControl2.AddItem("esriControls.ControlsMapPanTool", 0, -1, False, 0, esriCommandStyles.esriCommandStyleIconOnly)
        AxToolbarControl2.AddItem("esriControls.ControlsMapFullExtentCommand", 0, -1, False, 0, esriCommandStyles.esriCommandStyleIconOnly)
        AxToolbarControl2.AddItem("esriControls.ControlsMapZoomToLastExtentBackCommand", 0, -1, False, 0, esriCommandStyles.esriCommandStyleIconOnly)
        AxToolbarControl2.AddItem("esriControls.ControlsMapZoomToLastExtentForwardCommand", 0, -1, False, 0, esriCommandStyles.esriCommandStyleIconOnly)
        AxToolbarControl2.AddItem("esriControls.ControlsFullScreenCommand", 0, -1, True, 0, esriCommandStyles.esriCommandStyleIconOnly)
        AxToolbarControl2.AddItem("esriControls.ControlsMapIdentifyTool", 0, -1, False, 0, esriCommandStyles.esriCommandStyleIconOnly)

        'Create popup menus
        m_toolbarMenuSketch = New ToolbarMenuClass
        m_toolbarMenuVertex = New ToolbarMenuClass
        m_toolbarMenuSketch.AddItem("esriControls.ControlsEditingSketchContextMenu", 0, 0, False, esriCommandStyles.esriCommandStyleTextOnly)
        m_toolbarMenuVertex.AddItem("esriControls.ControlsEditingVertexContextMenu", 0, 0, False, esriCommandStyles.esriCommandStyleTextOnly)

        'Create an operation stack for the undo and redo commands to use
        Dim operationStack As IOperationStack
        operationStack = New ControlsOperationStackClass
        AxToolbarControl1.OperationStack = operationStack
        AxToolbarControl2.OperationStack = operationStack

        'Instantiate the EngineEditor singleton
        m_engineEditor = New EngineEditor()

        'Create each command on the ToolbarMenu so that the Accelerator Keys are recognized. Alternatively
        'the user must popup the menu before using the Accelerator Keys
        Dim itemCount As Integer
        itemCount = m_toolbarMenuSketch.CommandPool.Count
        Dim i As Integer
        For i = 0 To itemCount - 1
            Dim pCommand As ICommand
            pCommand = CType(m_toolbarMenuSketch.CommandPool.Command(i), ICommand)
            pCommand.OnCreate(AxMapControl1.Object)
        Next

        'Share CommandPool with the ToolbarMenu
        m_toolbarMenuSketch.CommandPool = m_CommandPool
        m_toolbarMenuVertex.CommandPool = m_CommandPool

    End Sub

    Private Sub AxMapControl1_OnMouseDown(ByVal sender As System.Object, ByVal e As ESRI.ArcGIS.Controls.IMapControlEvents2_OnMouseDownEvent) Handles AxMapControl1.OnMouseDown
        If (e.button = 2) Then

            'Logic to determine which popup menu to show based on the current task and current tool
            Dim currentTask As IEngineEditTask = m_engineEditor.CurrentTask

            Select Case currentTask.UniqueName

                Case "ControlToolsEditing_CreateNewFeatureTask"

                    If (CType(AxToolbarControl1.CurrentTool, ICommand)).Name = "ControlToolsEditing_Sketch" Then

                        m_toolbarMenuSketch.PopupMenu(e.x, e.y, AxMapControl1.hWnd)

                    ElseIf (CType(AxToolbarControl1.CurrentTool, ICommand)).Name = "ControlToolsEditing_Edit" Then

                        'SetEditLocation method must be called to enable commands
                        CType(m_engineEditor, IEngineEditSketch).SetEditLocation(e.x, e.y)
                        m_toolbarMenuVertex.PopupMenu(e.x, e.y, AxMapControl1.hWnd)

                    End If


                Case "ControlToolsEditing_ModifyFeatureTask"

                    'SetEditLocation method must be called to enable commands
                    CType(m_engineEditor, IEngineEditSketch).SetEditLocation(e.x, e.y)
                    m_toolbarMenuVertex.PopupMenu(e.x, e.y, AxMapControl1.hWnd)

            End Select

        End If
    End Sub

    Private Sub btnOptions1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOptions1.Click
        'Disable this window
        Me.Enabled = False
        Dim editOptions1 As New EditProperties

        'Show the options1 dialog
        editOptions1.ShowDialog()

        'Enable this window
        Me.Enabled = True
    End Sub

    Private Sub btnOptions2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOptions2.Click
        'Disable this window
        Me.Enabled = False

        Dim editOptions2 As New EditProperties2

        'Show the options2 dialog
        editOptions2.ShowDialog()

        'Enable this window
        Me.Enabled = True
    End Sub

    Private Sub FeatureEditing_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown

        Dim pPool2 As ICommandPool2
        pPool2 = CType(m_toolbarMenuSketch.CommandPool, ICommandPool2)
        Try
            pPool2.TranslateAcceleratorKey(e.KeyCode)
        Catch
        End Try

    End Sub

End Class
