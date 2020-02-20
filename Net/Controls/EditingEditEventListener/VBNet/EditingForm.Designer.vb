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
        Me.AxTOCControl1 = New ESRI.ArcGIS.Controls.AxTOCControl
        Me.AxMapControl1 = New ESRI.ArcGIS.Controls.AxMapControl
        Me.eventTabControl = New System.Windows.Forms.TabControl
        Me.TabPage1 = New System.Windows.Forms.TabPage
        Me.clearEvents = New System.Windows.Forms.Button
        Me.lstEditorEvents = New System.Windows.Forms.ListBox
        Me.TabPage2 = New System.Windows.Forms.TabPage
        Me.deselectAll = New System.Windows.Forms.Button
        Me.selectAll = New System.Windows.Forms.Button
        Me.chkListEvent = New System.Windows.Forms.CheckedListBox
        Me.AxLicenseControl1 = New ESRI.ArcGIS.Controls.AxLicenseControl
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.AxEditorToolbar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AxToolbarControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AxTOCControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AxMapControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.eventTabControl.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        CType(Me.AxLicenseControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 3
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.97595!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 231.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 69.02405!))
        Me.TableLayoutPanel1.Controls.Add(Me.AxEditorToolbar, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.AxToolbarControl1, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.AxTOCControl1, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.AxMapControl1, 2, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.eventTabControl, 1, 2)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(2, 3)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 4
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.469945!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 91.53005!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 119.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(911, 532)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'AxEditorToolbar
        '
        Me.TableLayoutPanel1.SetColumnSpan(Me.AxEditorToolbar, 3)
        Me.AxEditorToolbar.Dock = System.Windows.Forms.DockStyle.Fill
        Me.AxEditorToolbar.Location = New System.Drawing.Point(3, 3)
        Me.AxEditorToolbar.Name = "AxEditorToolbar"
        Me.AxEditorToolbar.OcxState = CType(resources.GetObject("AxEditorToolbar.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxEditorToolbar.Size = New System.Drawing.Size(905, 28)
        Me.AxEditorToolbar.TabIndex = 1
        '
        'AxToolbarControl1
        '
        Me.TableLayoutPanel1.SetColumnSpan(Me.AxToolbarControl1, 3)
        Me.AxToolbarControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.AxToolbarControl1.Location = New System.Drawing.Point(3, 36)
        Me.AxToolbarControl1.Name = "AxToolbarControl1"
        Me.AxToolbarControl1.OcxState = CType(resources.GetObject("AxToolbarControl1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxToolbarControl1.Size = New System.Drawing.Size(905, 28)
        Me.AxToolbarControl1.TabIndex = 0
        '
        'AxTOCControl1
        '
        Me.AxTOCControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.AxTOCControl1.Location = New System.Drawing.Point(3, 68)
        Me.AxTOCControl1.Name = "AxTOCControl1"
        Me.AxTOCControl1.OcxState = CType(resources.GetObject("AxTOCControl1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.TableLayoutPanel1.SetRowSpan(Me.AxTOCControl1, 2)
        Me.AxTOCControl1.Size = New System.Drawing.Size(204, 461)
        Me.AxTOCControl1.TabIndex = 2
        '
        'AxMapControl1
        '
        Me.AxMapControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.AxMapControl1.Location = New System.Drawing.Point(444, 68)
        Me.AxMapControl1.Name = "AxMapControl1"
        Me.AxMapControl1.OcxState = CType(resources.GetObject("AxMapControl1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.TableLayoutPanel1.SetRowSpan(Me.AxMapControl1, 2)
        Me.AxMapControl1.Size = New System.Drawing.Size(464, 461)
        Me.AxMapControl1.TabIndex = 3
        '
        'eventTabControl
        '
        Me.eventTabControl.Controls.Add(Me.TabPage1)
        Me.eventTabControl.Controls.Add(Me.TabPage2)
        Me.eventTabControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.eventTabControl.Location = New System.Drawing.Point(213, 68)
        Me.eventTabControl.Name = "eventTabControl"
        Me.TableLayoutPanel1.SetRowSpan(Me.eventTabControl, 2)
        Me.eventTabControl.SelectedIndex = 0
        Me.eventTabControl.Size = New System.Drawing.Size(225, 461)
        Me.eventTabControl.TabIndex = 4
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.clearEvents)
        Me.TabPage1.Controls.Add(Me.lstEditorEvents)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(217, 435)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Events Listener"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'clearEvents
        '
        Me.clearEvents.Location = New System.Drawing.Point(7, 339)
        Me.clearEvents.Name = "clearEvents"
        Me.clearEvents.Size = New System.Drawing.Size(75, 23)
        Me.clearEvents.TabIndex = 1
        Me.clearEvents.Text = "Clear Events"
        Me.clearEvents.UseVisualStyleBackColor = True
        '
        'lstEditorEvents
        '
        Me.lstEditorEvents.Dock = System.Windows.Forms.DockStyle.Top
        Me.lstEditorEvents.FormattingEnabled = True
        Me.lstEditorEvents.Location = New System.Drawing.Point(3, 3)
        Me.lstEditorEvents.Name = "lstEditorEvents"
        Me.lstEditorEvents.Size = New System.Drawing.Size(211, 329)
        Me.lstEditorEvents.TabIndex = 0
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.deselectAll)
        Me.TabPage2.Controls.Add(Me.selectAll)
        Me.TabPage2.Controls.Add(Me.chkListEvent)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(217, 435)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Select Events"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'deselectAll
        '
        Me.deselectAll.Location = New System.Drawing.Point(7, 374)
        Me.deselectAll.Name = "deselectAll"
        Me.deselectAll.Size = New System.Drawing.Size(75, 23)
        Me.deselectAll.TabIndex = 2
        Me.deselectAll.Text = "De-select All"
        Me.deselectAll.UseVisualStyleBackColor = True
        '
        'selectAll
        '
        Me.selectAll.Location = New System.Drawing.Point(7, 344)
        Me.selectAll.Name = "selectAll"
        Me.selectAll.Size = New System.Drawing.Size(75, 23)
        Me.selectAll.TabIndex = 1
        Me.selectAll.Text = "Select All"
        Me.selectAll.UseVisualStyleBackColor = True
        '
        'chkListEvent
        '
        Me.chkListEvent.CheckOnClick = True
        Me.chkListEvent.Dock = System.Windows.Forms.DockStyle.Top
        Me.chkListEvent.FormattingEnabled = True
        Me.chkListEvent.Location = New System.Drawing.Point(3, 3)
        Me.chkListEvent.Name = "chkListEvent"
        Me.chkListEvent.Size = New System.Drawing.Size(211, 319)
        Me.chkListEvent.TabIndex = 0
        '
        'AxLicenseControl1
        '
        Me.AxLicenseControl1.Enabled = True
        Me.AxLicenseControl1.Location = New System.Drawing.Point(343, 469)
        Me.AxLicenseControl1.Name = "AxLicenseControl1"
        Me.AxLicenseControl1.OcxState = CType(resources.GetObject("AxLicenseControl1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxLicenseControl1.Size = New System.Drawing.Size(32, 32)
        Me.AxLicenseControl1.TabIndex = 1
        '
        'EditingForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(913, 538)
        Me.Controls.Add(Me.AxLicenseControl1)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Name = "EditingForm"
        Me.Text = "Listen to Edit Events Sample (VB.NET)"
        Me.TableLayoutPanel1.ResumeLayout(False)
        CType(Me.AxEditorToolbar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AxToolbarControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AxTOCControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AxMapControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.eventTabControl.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        CType(Me.AxLicenseControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents AxLicenseControl1 As ESRI.ArcGIS.Controls.AxLicenseControl
    Friend WithEvents AxEditorToolbar As ESRI.ArcGIS.Controls.AxToolbarControl
    Friend WithEvents AxToolbarControl1 As ESRI.ArcGIS.Controls.AxToolbarControl
    Friend WithEvents AxTOCControl1 As ESRI.ArcGIS.Controls.AxTOCControl
  Friend WithEvents AxMapControl1 As ESRI.ArcGIS.Controls.AxMapControl
  Friend WithEvents eventTabControl As System.Windows.Forms.TabControl
  Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
  Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
  Friend WithEvents lstEditorEvents As System.Windows.Forms.ListBox
  Friend WithEvents chkListEvent As System.Windows.Forms.CheckedListBox
  Friend WithEvents deselectAll As System.Windows.Forms.Button
  Friend WithEvents selectAll As System.Windows.Forms.Button
  Friend WithEvents clearEvents As System.Windows.Forms.Button

End Class
