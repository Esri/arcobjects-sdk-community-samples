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
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.SystemUI
Imports ESRI.ArcGIS
Public Class Form1
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()
        'Load runtime 
        If Not RuntimeManager.Bind(ProductCode.Engine) Then
            If Not RuntimeManager.Bind(ProductCode.Desktop) Then
                MessageBox.Show("Unable to bind to ArcGIS runtime. Application will be shut down.")
                System.Environment.Exit(1) ' Force exit or other indication in the application
            End If
        End If
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
    Friend WithEvents AxTOCControl1 As ESRI.ArcGIS.Controls.AxTOCControl
    Friend WithEvents AxToolbarControl1 As ESRI.ArcGIS.Controls.AxToolbarControl
    Friend WithEvents AxPageLayoutControl1 As ESRI.ArcGIS.Controls.AxPageLayoutControl
    Friend WithEvents AxLicenseControl1 As ESRI.ArcGIS.Controls.AxLicenseControl
    Friend WithEvents chkDragDrop As System.Windows.Forms.CheckBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.AxTOCControl1 = New ESRI.ArcGIS.Controls.AxTOCControl
        Me.AxToolbarControl1 = New ESRI.ArcGIS.Controls.AxToolbarControl
        Me.AxPageLayoutControl1 = New ESRI.ArcGIS.Controls.AxPageLayoutControl
        Me.AxLicenseControl1 = New ESRI.ArcGIS.Controls.AxLicenseControl
        Me.chkDragDrop = New System.Windows.Forms.CheckBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        CType(Me.AxTOCControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AxToolbarControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AxPageLayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AxLicenseControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'AxTOCControl1
        '
        Me.AxTOCControl1.Location = New System.Drawing.Point(8, 40)
        Me.AxTOCControl1.Name = "AxTOCControl1"
        Me.AxTOCControl1.OcxState = CType(resources.GetObject("AxTOCControl1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxTOCControl1.Size = New System.Drawing.Size(144, 328)
        Me.AxTOCControl1.TabIndex = 0
        '
        'AxToolbarControl1
        '
        Me.AxToolbarControl1.Location = New System.Drawing.Point(8, 8)
        Me.AxToolbarControl1.Name = "AxToolbarControl1"
        Me.AxToolbarControl1.OcxState = CType(resources.GetObject("AxToolbarControl1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxToolbarControl1.Size = New System.Drawing.Size(424, 28)
        Me.AxToolbarControl1.TabIndex = 1
        '
        'AxPageLayoutControl1
        '
        Me.AxPageLayoutControl1.Location = New System.Drawing.Point(160, 40)
        Me.AxPageLayoutControl1.Name = "AxPageLayoutControl1"
        Me.AxPageLayoutControl1.OcxState = CType(resources.GetObject("AxPageLayoutControl1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxPageLayoutControl1.Size = New System.Drawing.Size(272, 328)
        Me.AxPageLayoutControl1.TabIndex = 2
        '
        'AxLicenseControl1
        '
        Me.AxLicenseControl1.Enabled = True
        Me.AxLicenseControl1.Location = New System.Drawing.Point(56, 56)
        Me.AxLicenseControl1.Name = "AxLicenseControl1"
        Me.AxLicenseControl1.OcxState = CType(resources.GetObject("AxLicenseControl1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxLicenseControl1.Size = New System.Drawing.Size(32, 32)
        Me.AxLicenseControl1.TabIndex = 3
        '
        'chkDragDrop
        '
        Me.chkDragDrop.Location = New System.Drawing.Point(440, 16)
        Me.chkDragDrop.Name = "chkDragDrop"
        Me.chkDragDrop.Size = New System.Drawing.Size(168, 16)
        Me.chkDragDrop.TabIndex = 4
        Me.chkDragDrop.Text = "Layer Drag and Drop"
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(440, 48)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(176, 32)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "1) Load a map document into the PageLayoutControl"
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(440, 104)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(176, 32)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "3) Re-order the layers in the TOCControl"
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(440, 80)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(176, 32)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "2) Enable layer drag and drop"
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(440, 136)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(176, 56)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "4) Right click on the PageLayoutControl and drag a rectangle to create a new MapF" & _
            "rame"
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(440, 192)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(184, 56)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "5) Copy a layer into the new map by dragging and dropping a layer inside the TOCC" & _
            "ontrol"
        '
        'Label6
        '
        Me.Label6.Location = New System.Drawing.Point(440, 239)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(184, 56)
        Me.Label6.TabIndex = 10
        Me.Label6.Text = "6) Move a layer into the new map by dragging and dropping a layer whilst holding " & _
            "the CTRL key down inside the TOCControl"
        '
        'Form1
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(624, 374)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.chkDragDrop)
        Me.Controls.Add(Me.AxLicenseControl1)
        Me.Controls.Add(Me.AxPageLayoutControl1)
        Me.Controls.Add(Me.AxToolbarControl1)
        Me.Controls.Add(Me.AxTOCControl1)
        Me.Name = "Form1"
        Me.Text = "Layer Drag and Drop"
        CType(Me.AxTOCControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AxToolbarControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AxPageLayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AxLicenseControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'Set buddy control
        AxToolbarControl1.SetBuddyControl(AxPageLayoutControl1)
        AxTOCControl1.SetBuddyControl(AxPageLayoutControl1)

        'Add items to the ToolbarControl
        AxToolbarControl1.AddItem("esriControls.ControlsOpenDocCommand", -1, -1, False, 0, esriCommandStyles.esriCommandStyleIconOnly)
        AxToolbarControl1.AddItem("esriControls.ControlsPageZoomInTool", -1, -1, True, 0, esriCommandStyles.esriCommandStyleIconOnly)
        AxToolbarControl1.AddItem("esriControls.ControlsPageZoomOutTool", -1, -1, False, 0, esriCommandStyles.esriCommandStyleIconOnly)
        AxToolbarControl1.AddItem("esriControls.ControlsPageZoomWholePageCommand", -1, -1, False, 0, esriCommandStyles.esriCommandStyleIconOnly)
        AxToolbarControl1.AddItem("esriControls.ControlsPageFocusPreviousMapCommand", -1, -1, True, 0, esriCommandStyles.esriCommandStyleIconOnly)
        AxToolbarControl1.AddItem("esriControls.ControlsPageFocusNextMapCommand", -1, -1, False, 0, esriCommandStyles.esriCommandStyleIconOnly)
        AxToolbarControl1.AddItem("esriControls.ControlsMapFullExtentCommand", -1, -1, True, 0, esriCommandStyles.esriCommandStyleIconOnly)
        AxToolbarControl1.AddItem("esriControls.ControlsMapZoomInTool", -1, -1, False, 0, esriCommandStyles.esriCommandStyleIconOnly)
        AxToolbarControl1.AddItem("esriControls.ControlsMapZoomOutTool", -1, -1, False, 0, esriCommandStyles.esriCommandStyleIconOnly)
        AxToolbarControl1.AddItem("esriControls.ControlsSelectTool", -1, -1, True, 0, esriCommandStyles.esriCommandStyleIconOnly)

    End Sub

    Private Sub chkDragDrop_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkDragDrop.Click
        'Enable layer and drag and drop
        AxTOCControl1.EnableLayerDragDrop = chkDragDrop.Checked
    End Sub

    Private Sub AxPageLayoutControl1_OnMouseDown(ByVal sender As System.Object, ByVal e As ESRI.ArcGIS.Controls.IPageLayoutControlEvents_OnMouseDownEvent) Handles AxPageLayoutControl1.OnMouseDown
        If e.button = 1 Then Exit Sub

        'Create an envelope by tracking a rectangle
        Dim envelope As IEnvelope
        envelope = AxPageLayoutControl1.TrackRectangle()

        'Create a map frame element with a new map
        Dim mapFrame As IMapFrame
        mapFrame = New MapFrameClass
        mapFrame.Map = New MapClass

        'Add the map frame to the PageLayoutControl with specified geometry
        AxPageLayoutControl1.AddElement(mapFrame, envelope, Nothing, Nothing, 0)
        'Refresh the PageLayoutControl
        AxPageLayoutControl1.Refresh(esriViewDrawPhase.esriViewGraphics, Nothing, Nothing)
    End Sub
End Class
