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
Imports ESRI.ArcGIS.Controls
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
        'Release COM objects
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
    Friend WithEvents AxToolbarControl1 As ESRI.ArcGIS.Controls.AxToolbarControl
    Friend WithEvents AxPageLayoutControl1 As ESRI.ArcGIS.Controls.AxPageLayoutControl
    Friend WithEvents AxLicenseControl1 As ESRI.ArcGIS.Controls.AxLicenseControl
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents AxTOCControl1 As ESRI.ArcGIS.Controls.AxTOCControl
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.AxToolbarControl1 = New ESRI.ArcGIS.Controls.AxToolbarControl
        Me.AxPageLayoutControl1 = New ESRI.ArcGIS.Controls.AxPageLayoutControl
        Me.AxLicenseControl1 = New ESRI.ArcGIS.Controls.AxLicenseControl
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.AxTOCControl1 = New ESRI.ArcGIS.Controls.AxTOCControl
        CType(Me.AxToolbarControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AxPageLayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AxLicenseControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AxTOCControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'AxToolbarControl1
        '
        Me.AxToolbarControl1.Location = New System.Drawing.Point(8, 8)
        Me.AxToolbarControl1.Name = "AxToolbarControl1"
        Me.AxToolbarControl1.OcxState = CType(resources.GetObject("AxToolbarControl1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxToolbarControl1.Size = New System.Drawing.Size(764, 28)
        Me.AxToolbarControl1.TabIndex = 0
        '
        'AxPageLayoutControl1
        '
        Me.AxPageLayoutControl1.Location = New System.Drawing.Point(193, 40)
        Me.AxPageLayoutControl1.Name = "AxPageLayoutControl1"
        Me.AxPageLayoutControl1.OcxState = CType(resources.GetObject("AxPageLayoutControl1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxPageLayoutControl1.Size = New System.Drawing.Size(419, 468)
        Me.AxPageLayoutControl1.TabIndex = 1
        '
        'AxLicenseControl1
        '
        Me.AxLicenseControl1.Enabled = True
        Me.AxLicenseControl1.Location = New System.Drawing.Point(618, 124)
        Me.AxLicenseControl1.Name = "AxLicenseControl1"
        Me.AxLicenseControl1.OcxState = CType(resources.GetObject("AxLicenseControl1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxLicenseControl1.Size = New System.Drawing.Size(32, 32)
        Me.AxLicenseControl1.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(618, 47)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(154, 32)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "1) Load a map document into the PageLayoutControl. "
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(618, 87)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(154, 32)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "2) Use the palette to add map surrounds onto the page"
        '
        'AxTOCControl1
        '
        Me.AxTOCControl1.Location = New System.Drawing.Point(7, 42)
        Me.AxTOCControl1.Name = "AxTOCControl1"
        Me.AxTOCControl1.OcxState = CType(resources.GetObject("AxTOCControl1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxTOCControl1.Size = New System.Drawing.Size(180, 466)
        Me.AxTOCControl1.TabIndex = 5
        '
        'Form1
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(784, 520)
        Me.Controls.Add(Me.AxTOCControl1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.AxLicenseControl1)
        Me.Controls.Add(Me.AxPageLayoutControl1)
        Me.Controls.Add(Me.AxToolbarControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "Form1"
        Me.Text = "Add Map Surrounds"
        CType(Me.AxToolbarControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AxPageLayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AxLicenseControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AxTOCControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'Set buddy control
        AxToolbarControl1.SetBuddyControl(AxPageLayoutControl1)
        AxTOCControl1.SetBuddyControl(AxPageLayoutControl1)

        'Add ToolbarControl items
        AxToolbarControl1.AddItem("esriControls.ControlsOpenDocCommand", 0, -1, False, 0, esriCommandStyles.esriCommandStyleIconOnly)
        AxToolbarControl1.AddItem("esriControls.ControlsPageZoomInTool", 0, -1, True, 0, esriCommandStyles.esriCommandStyleIconOnly)
        AxToolbarControl1.AddItem("esriControls.ControlsPageZoomOutTool", 0, -1, False, 0, esriCommandStyles.esriCommandStyleIconOnly)
        AxToolbarControl1.AddItem("esriControls.ControlsPageZoomWholePageCommand", 0, -1, False, 0, esriCommandStyles.esriCommandStyleIconOnly)
        AxToolbarControl1.AddItem("esriControls.ControlsMapZoomInTool", 0, -1, True, 0, esriCommandStyles.esriCommandStyleIconOnly)
        AxToolbarControl1.AddItem("esriControls.ControlsMapZoomOutTool", 0, -1, False, 0, esriCommandStyles.esriCommandStyleIconOnly)
        AxToolbarControl1.AddItem("esriControls.ControlsMapPanTool", 0, -1, False, 0, esriCommandStyles.esriCommandStyleIconOnly)
        AxToolbarControl1.AddItem("esriControls.ControlsMapFullExtentCommand", 0, -1, False, 0, esriCommandStyles.esriCommandStyleIconOnly)
        AxToolbarControl1.AddItem("esriControls.ControlsSelectTool", 0, -1, True, 0, esriCommandStyles.esriCommandStyleIconOnly)

        'Create a new ToolbarPalette 
        Dim toolbarPalette As IToolbarPalette = New ToolbarPaletteClass
        toolbarPalette.Caption = "Map Surrounds"
        toolbarPalette.AddItem(New CreateNorthArrow, -1, -1)
        toolbarPalette.AddItem(New CreateScaleBar, -1, -1)
        toolbarPalette.AddItem(New CreateScaleText, -1, -1)
        'Add the ToolbarPalette to the ToolbarControl
        AxToolbarControl1.AddItem(toolbarPalette, -1, -1, False, 0, esriCommandStyles.esriCommandStyleIconOnly)
    End Sub
End Class
