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
Partial Class HookActions
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(HookActions))
        Me.tabControl1 = New System.Windows.Forms.TabControl()
        Me.tabPage1 = New System.Windows.Forms.TabPage()
        Me.AxMapControl1 = New ESRI.ArcGIS.Controls.AxMapControl()
        Me.AxToolbarControl1 = New ESRI.ArcGIS.Controls.AxToolbarControl()
        Me.tabPage2 = New System.Windows.Forms.TabPage()
        Me.AxToolbarControl2 = New ESRI.ArcGIS.Controls.AxToolbarControl()
        Me.AxGlobeControl1 = New ESRI.ArcGIS.Controls.AxGlobeControl()
        Me.AxLicenseControl1 = New ESRI.ArcGIS.Controls.AxLicenseControl()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.tabControl1.SuspendLayout()
        Me.tabPage1.SuspendLayout()
        CType(Me.AxMapControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AxToolbarControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabPage2.SuspendLayout()
        CType(Me.AxToolbarControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AxGlobeControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AxLicenseControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'tabControl1
        '
        Me.tabControl1.Controls.Add(Me.tabPage1)
        Me.tabControl1.Controls.Add(Me.tabPage2)
        Me.tabControl1.Location = New System.Drawing.Point(13, 59)
        Me.tabControl1.Margin = New System.Windows.Forms.Padding(2)
        Me.tabControl1.Name = "tabControl1"
        Me.tabControl1.SelectedIndex = 0
        Me.tabControl1.Size = New System.Drawing.Size(569, 509)
        Me.tabControl1.TabIndex = 1
        '
        'tabPage1
        '
        Me.tabPage1.Controls.Add(Me.AxMapControl1)
        Me.tabPage1.Controls.Add(Me.AxToolbarControl1)
        Me.tabPage1.Location = New System.Drawing.Point(4, 22)
        Me.tabPage1.Margin = New System.Windows.Forms.Padding(2)
        Me.tabPage1.Name = "tabPage1"
        Me.tabPage1.Padding = New System.Windows.Forms.Padding(2)
        Me.tabPage1.Size = New System.Drawing.Size(561, 483)
        Me.tabPage1.TabIndex = 0
        Me.tabPage1.Text = "MapControl"
        Me.tabPage1.UseVisualStyleBackColor = True
        '
        'AxMapControl1
        '
        Me.AxMapControl1.Location = New System.Drawing.Point(4, 36)
        Me.AxMapControl1.Margin = New System.Windows.Forms.Padding(2)
        Me.AxMapControl1.Name = "AxMapControl1"
        Me.AxMapControl1.OcxState = CType(resources.GetObject("AxMapControl1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxMapControl1.Size = New System.Drawing.Size(553, 443)
        Me.AxMapControl1.TabIndex = 0
        '
        'AxToolbarControl1
        '
        Me.AxToolbarControl1.Location = New System.Drawing.Point(4, 4)
        Me.AxToolbarControl1.Margin = New System.Windows.Forms.Padding(2)
        Me.AxToolbarControl1.Name = "AxToolbarControl1"
        Me.AxToolbarControl1.OcxState = CType(resources.GetObject("AxToolbarControl1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxToolbarControl1.Size = New System.Drawing.Size(553, 28)
        Me.AxToolbarControl1.TabIndex = 2
        '
        'tabPage2
        '
        Me.tabPage2.Controls.Add(Me.AxToolbarControl2)
        Me.tabPage2.Controls.Add(Me.AxGlobeControl1)
        Me.tabPage2.Location = New System.Drawing.Point(4, 22)
        Me.tabPage2.Margin = New System.Windows.Forms.Padding(2)
        Me.tabPage2.Name = "tabPage2"
        Me.tabPage2.Padding = New System.Windows.Forms.Padding(2)
        Me.tabPage2.Size = New System.Drawing.Size(561, 483)
        Me.tabPage2.TabIndex = 1
        Me.tabPage2.Text = "GlobeControl"
        Me.tabPage2.UseVisualStyleBackColor = True
        '
        'AxToolbarControl2
        '
        Me.AxToolbarControl2.Location = New System.Drawing.Point(6, 4)
        Me.AxToolbarControl2.Margin = New System.Windows.Forms.Padding(2)
        Me.AxToolbarControl2.Name = "AxToolbarControl2"
        Me.AxToolbarControl2.OcxState = CType(resources.GetObject("AxToolbarControl2.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxToolbarControl2.Size = New System.Drawing.Size(553, 28)
        Me.AxToolbarControl2.TabIndex = 3
        '
        'AxGlobeControl1
        '
        Me.AxGlobeControl1.Location = New System.Drawing.Point(4, 36)
        Me.AxGlobeControl1.Margin = New System.Windows.Forms.Padding(2)
        Me.AxGlobeControl1.Name = "AxGlobeControl1"
        Me.AxGlobeControl1.OcxState = CType(resources.GetObject("AxGlobeControl1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxGlobeControl1.Size = New System.Drawing.Size(553, 443)
        Me.AxGlobeControl1.TabIndex = 0
        '
        'AxLicenseControl1
        '
        Me.AxLicenseControl1.Enabled = True
        Me.AxLicenseControl1.Location = New System.Drawing.Point(716, 32)
        Me.AxLicenseControl1.Margin = New System.Windows.Forms.Padding(2)
        Me.AxLicenseControl1.Name = "AxLicenseControl1"
        Me.AxLicenseControl1.OcxState = CType(resources.GetObject("AxLicenseControl1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxLicenseControl1.Size = New System.Drawing.Size(32, 32)
        Me.AxLicenseControl1.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.Label1.Location = New System.Drawing.Point(14, 12)
        Me.Label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(209, 15)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "1) Add data and select some features"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.Label3.Location = New System.Drawing.Point(14, 29)
        Me.Label3.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(552, 15)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "2) Right click the display to zoom, pan, flash or create graphics, labels or call" & _
            "outs of selected features"
        '
        'HookActions
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(595, 578)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.AxLicenseControl1)
        Me.Controls.Add(Me.tabControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.Name = "HookActions"
        Me.Text = "HookActions"
        Me.tabControl1.ResumeLayout(False)
        Me.tabPage1.ResumeLayout(False)
        CType(Me.AxMapControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AxToolbarControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabPage2.ResumeLayout(False)
        CType(Me.AxToolbarControl2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AxGlobeControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AxLicenseControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents tabControl1 As System.Windows.Forms.TabControl
    Private WithEvents tabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents AxMapControl1 As ESRI.ArcGIS.Controls.AxMapControl
    Private WithEvents tabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents AxGlobeControl1 As ESRI.ArcGIS.Controls.AxGlobeControl
    Friend WithEvents AxToolbarControl1 As ESRI.ArcGIS.Controls.AxToolbarControl
    Friend WithEvents AxLicenseControl1 As ESRI.ArcGIS.Controls.AxLicenseControl
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents AxToolbarControl2 As ESRI.ArcGIS.Controls.AxToolbarControl

End Class
