<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class EditingForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(EditingForm))
Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
Me.AxEditorToolbar = New ESRI.ArcGIS.Controls.AxToolbarControl
Me.AxToolbarControl1 = New ESRI.ArcGIS.Controls.AxToolbarControl
Me.AxMapControl1 = New ESRI.ArcGIS.Controls.AxMapControl
Me.AxTOCControl1 = New ESRI.ArcGIS.Controls.AxTOCControl
Me.GroupBox1 = New System.Windows.Forms.GroupBox
Me.AxLicenseControl1 = New ESRI.ArcGIS.Controls.AxLicenseControl
Me.label1 = New System.Windows.Forms.Label
Me.Label7 = New System.Windows.Forms.Label
Me.Label6 = New System.Windows.Forms.Label
Me.Label5 = New System.Windows.Forms.Label
Me.Label4 = New System.Windows.Forms.Label
Me.Label3 = New System.Windows.Forms.Label
Me.Label2 = New System.Windows.Forms.Label
Me.label16 = New System.Windows.Forms.Label
Me.TableLayoutPanel1.SuspendLayout()
CType(Me.AxEditorToolbar, System.ComponentModel.ISupportInitialize).BeginInit()
CType(Me.AxToolbarControl1, System.ComponentModel.ISupportInitialize).BeginInit()
CType(Me.AxMapControl1, System.ComponentModel.ISupportInitialize).BeginInit()
CType(Me.AxTOCControl1, System.ComponentModel.ISupportInitialize).BeginInit()
Me.GroupBox1.SuspendLayout()
CType(Me.AxLicenseControl1, System.ComponentModel.ISupportInitialize).BeginInit()
Me.SuspendLayout()
'
'TableLayoutPanel1
'
Me.TableLayoutPanel1.ColumnCount = 2
Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 32.87381!))
Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 67.12619!))
Me.TableLayoutPanel1.Controls.Add(Me.AxEditorToolbar, 0, 0)
Me.TableLayoutPanel1.Controls.Add(Me.AxToolbarControl1, 0, 1)
Me.TableLayoutPanel1.Controls.Add(Me.AxMapControl1, 1, 2)
Me.TableLayoutPanel1.Controls.Add(Me.AxTOCControl1, 0, 2)
Me.TableLayoutPanel1.Controls.Add(Me.GroupBox1, 0, 3)
Me.TableLayoutPanel1.Location = New System.Drawing.Point(3, 4)
Me.TableLayoutPanel1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
Me.TableLayoutPanel1.RowCount = 4
Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 41.0!))
Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 13.02817!))
Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 86.97183!))
Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 239.0!))
Me.TableLayoutPanel1.Size = New System.Drawing.Size(943, 639)
Me.TableLayoutPanel1.TabIndex = 0
'
'AxEditorToolbar
'
Me.TableLayoutPanel1.SetColumnSpan(Me.AxEditorToolbar, 2)
Me.AxEditorToolbar.Location = New System.Drawing.Point(4, 4)
Me.AxEditorToolbar.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
Me.AxEditorToolbar.Name = "AxEditorToolbar"
Me.AxEditorToolbar.OcxState = CType(resources.GetObject("AxEditorToolbar.OcxState"), System.Windows.Forms.AxHost.State)
Me.AxEditorToolbar.Size = New System.Drawing.Size(871, 28)
Me.AxEditorToolbar.TabIndex = 1
'
'AxToolbarControl1
'
Me.TableLayoutPanel1.SetColumnSpan(Me.AxToolbarControl1, 2)
Me.AxToolbarControl1.Location = New System.Drawing.Point(4, 45)
Me.AxToolbarControl1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
Me.AxToolbarControl1.Name = "AxToolbarControl1"
Me.AxToolbarControl1.OcxState = CType(resources.GetObject("AxToolbarControl1.OcxState"), System.Windows.Forms.AxHost.State)
Me.AxToolbarControl1.Size = New System.Drawing.Size(871, 28)
Me.AxToolbarControl1.TabIndex = 0
'
'AxMapControl1
'
Me.AxMapControl1.Location = New System.Drawing.Point(313, 91)
Me.AxMapControl1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
Me.AxMapControl1.Name = "AxMapControl1"
Me.AxMapControl1.OcxState = CType(resources.GetObject("AxMapControl1.OcxState"), System.Windows.Forms.AxHost.State)
Me.TableLayoutPanel1.SetRowSpan(Me.AxMapControl1, 2)
Me.AxMapControl1.Size = New System.Drawing.Size(604, 544)
Me.AxMapControl1.TabIndex = 3
'
'AxTOCControl1
'
Me.AxTOCControl1.Location = New System.Drawing.Point(4, 91)
Me.AxTOCControl1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
Me.AxTOCControl1.Name = "AxTOCControl1"
Me.AxTOCControl1.OcxState = CType(resources.GetObject("AxTOCControl1.OcxState"), System.Windows.Forms.AxHost.State)
Me.AxTOCControl1.Size = New System.Drawing.Size(301, 304)
Me.AxTOCControl1.TabIndex = 2
'
'GroupBox1
'
Me.GroupBox1.Controls.Add(Me.label1)
Me.GroupBox1.Controls.Add(Me.Label7)
Me.GroupBox1.Controls.Add(Me.Label6)
Me.GroupBox1.Controls.Add(Me.Label5)
Me.GroupBox1.Controls.Add(Me.Label4)
Me.GroupBox1.Controls.Add(Me.Label3)
Me.GroupBox1.Controls.Add(Me.Label2)
Me.GroupBox1.Controls.Add(Me.label16)
Me.GroupBox1.Location = New System.Drawing.Point(3, 401)
Me.GroupBox1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
Me.GroupBox1.Name = "GroupBox1"
Me.GroupBox1.Padding = New System.Windows.Forms.Padding(3, 2, 3, 2)
Me.GroupBox1.Size = New System.Drawing.Size(303, 230)
Me.GroupBox1.TabIndex = 4
Me.GroupBox1.TabStop = False
Me.GroupBox1.Text = "Steps..."
'
'AxLicenseControl1
'
Me.AxLicenseControl1.Enabled = True
Me.AxLicenseControl1.Location = New System.Drawing.Point(343, 469)
Me.AxLicenseControl1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
Me.AxLicenseControl1.Name = "AxLicenseControl1"
Me.AxLicenseControl1.OcxState = CType(resources.GetObject("AxLicenseControl1.OcxState"), System.Windows.Forms.AxHost.State)
Me.AxLicenseControl1.Size = New System.Drawing.Size(32, 32)
Me.AxLicenseControl1.TabIndex = 1
'
'label1
'
Me.label1.AutoSize = True
Me.label1.Location = New System.Drawing.Point(-1, 136)
Me.label1.Name = "label1"
Me.label1.Size = New System.Drawing.Size(298, 17)
Me.label1.TabIndex = 31
Me.label1.Text = "6. Move close to points and observe snapping"
'
'Label7
'
Me.Label7.AutoSize = True
Me.Label7.Location = New System.Drawing.Point(-1, 175)
Me.Label7.Name = "Label7"
Me.Label7.Size = New System.Drawing.Size(171, 17)
Me.Label7.TabIndex = 30
Me.Label7.Text = "8. Stop editing, save edits"
'
'Label6
'
Me.Label6.AutoSize = True
Me.Label6.Location = New System.Drawing.Point(-1, 156)
Me.Label6.Name = "Label6"
Me.Label6.Size = New System.Drawing.Size(112, 17)
Me.Label6.TabIndex = 29
Me.Label6.Text = "7. Digitize points"
'
'Label5
'
Me.Label5.AutoSize = True
Me.Label5.Location = New System.Drawing.Point(-1, 116)
Me.Label5.Name = "Label5"
Me.Label5.Size = New System.Drawing.Size(135, 17)
Me.Label5.TabIndex = 28
Me.Label5.Text = "5. Select sketch tool"
'
'Label4
'
Me.Label4.AutoSize = True
Me.Label4.Location = New System.Drawing.Point(-1, 97)
Me.Label4.Name = "Label4"
Me.Label4.Size = New System.Drawing.Size(203, 17)
Me.Label4.TabIndex = 27
Me.Label4.Text = "4. Check on Buffer Snap Agent"
'
'Label3
'
Me.Label3.AutoSize = True
Me.Label3.Location = New System.Drawing.Point(-1, 77)
Me.Label3.Name = "Label3"
Me.Label3.Size = New System.Drawing.Size(295, 17)
Me.Label3.TabIndex = 26
Me.Label3.Text = "3. Open Snapping Window (Editor>Snapping)"
'
'Label2
'
Me.Label2.AutoSize = True
Me.Label2.Location = New System.Drawing.Point(-1, 38)
Me.Label2.Name = "Label2"
Me.Label2.Size = New System.Drawing.Size(100, 17)
Me.Label2.TabIndex = 25
Me.Label2.Text = "1. Start editing"
'
'label16
'
Me.label16.AutoSize = True
Me.label16.Location = New System.Drawing.Point(-1, 57)
Me.label16.Name = "label16"
Me.label16.Size = New System.Drawing.Size(298, 17)
Me.label16.TabIndex = 24
Me.label16.Text = "2. Zoom in on point features to about 1:30000"
'
'EditingForm
'
Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
Me.ClientSize = New System.Drawing.Size(952, 662)
Me.Controls.Add(Me.AxLicenseControl1)
Me.Controls.Add(Me.TableLayoutPanel1)
Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
Me.Name = "EditingForm"
Me.Text = "Buffer Snap Agent Sample (VB.NET)"
Me.TableLayoutPanel1.ResumeLayout(False)
CType(Me.AxEditorToolbar, System.ComponentModel.ISupportInitialize).EndInit()
CType(Me.AxToolbarControl1, System.ComponentModel.ISupportInitialize).EndInit()
CType(Me.AxMapControl1, System.ComponentModel.ISupportInitialize).EndInit()
CType(Me.AxTOCControl1, System.ComponentModel.ISupportInitialize).EndInit()
Me.GroupBox1.ResumeLayout(False)
Me.GroupBox1.PerformLayout()
CType(Me.AxLicenseControl1, System.ComponentModel.ISupportInitialize).EndInit()
Me.ResumeLayout(False)

End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents AxLicenseControl1 As ESRI.ArcGIS.Controls.AxLicenseControl
    Friend WithEvents AxEditorToolbar As ESRI.ArcGIS.Controls.AxToolbarControl
    Friend WithEvents AxToolbarControl1 As ESRI.ArcGIS.Controls.AxToolbarControl
    Friend WithEvents AxTOCControl1 As ESRI.ArcGIS.Controls.AxTOCControl
    Friend WithEvents AxMapControl1 As ESRI.ArcGIS.Controls.AxMapControl
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents label1 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents label16 As System.Windows.Forms.Label

End Class
