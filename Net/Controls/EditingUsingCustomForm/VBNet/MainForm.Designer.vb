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
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
        Me.AxMapControl1 = New ESRI.ArcGIS.Controls.AxMapControl
        Me.AxLicenseControl1 = New ESRI.ArcGIS.Controls.AxLicenseControl
        Me.AxToolbarControl1 = New ESRI.ArcGIS.Controls.AxToolbarControl
        Me.axEditorToolbar = New ESRI.ArcGIS.Controls.AxToolbarControl
        Me.AxTOCControl1 = New ESRI.ArcGIS.Controls.AxTOCControl
        CType(Me.AxMapControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AxLicenseControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AxToolbarControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.axEditorToolbar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AxTOCControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'AxMapControl1
        '
        Me.AxMapControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.AxMapControl1.Location = New System.Drawing.Point(203, 0)
        Me.AxMapControl1.Name = "AxMapControl1"
        Me.AxMapControl1.OcxState = CType(resources.GetObject("AxMapControl1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxMapControl1.Size = New System.Drawing.Size(448, 507)
        Me.AxMapControl1.TabIndex = 0
        '
        'AxLicenseControl1
        '
        Me.AxLicenseControl1.Enabled = True
        Me.AxLicenseControl1.Location = New System.Drawing.Point(274, 240)
        Me.AxLicenseControl1.Name = "AxLicenseControl1"
        Me.AxLicenseControl1.OcxState = CType(resources.GetObject("AxLicenseControl1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxLicenseControl1.Size = New System.Drawing.Size(32, 32)
        Me.AxLicenseControl1.TabIndex = 1
        '
        'AxToolbarControl1
        '
        Me.AxToolbarControl1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.AxToolbarControl1.Location = New System.Drawing.Point(179, 0)
        Me.AxToolbarControl1.Name = "AxToolbarControl1"
        Me.AxToolbarControl1.OcxState = CType(resources.GetObject("AxToolbarControl1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxToolbarControl1.Size = New System.Drawing.Size(28, 507)
        Me.AxToolbarControl1.TabIndex = 2
        '
        'axEditorToolbar
        '
        Me.axEditorToolbar.Location = New System.Drawing.Point(-2, 0)
        Me.axEditorToolbar.Name = "axEditorToolbar"
        Me.axEditorToolbar.OcxState = CType(resources.GetObject("axEditorToolbar.OcxState"), System.Windows.Forms.AxHost.State)
        Me.axEditorToolbar.Size = New System.Drawing.Size(182, 28)
        Me.axEditorToolbar.TabIndex = 3
        '
        'AxTOCControl1
        '
        Me.AxTOCControl1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.AxTOCControl1.Location = New System.Drawing.Point(-2, 24)
        Me.AxTOCControl1.Name = "AxTOCControl1"
        Me.AxTOCControl1.OcxState = CType(resources.GetObject("AxTOCControl1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxTOCControl1.Size = New System.Drawing.Size(182, 483)
        Me.AxTOCControl1.TabIndex = 4
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(642, 504)
        Me.Controls.Add(Me.AxTOCControl1)
        Me.Controls.Add(Me.axEditorToolbar)
        Me.Controls.Add(Me.AxToolbarControl1)
        Me.Controls.Add(Me.AxLicenseControl1)
        Me.Controls.Add(Me.AxMapControl1)
        Me.Name = "MainForm"
        Me.Text = "Custom Editing Application"
        CType(Me.AxMapControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AxLicenseControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AxToolbarControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.axEditorToolbar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AxTOCControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents AxMapControl1 As ESRI.ArcGIS.Controls.AxMapControl
    Friend WithEvents AxLicenseControl1 As ESRI.ArcGIS.Controls.AxLicenseControl
    Friend WithEvents AxToolbarControl1 As ESRI.ArcGIS.Controls.AxToolbarControl
    Friend WithEvents axEditorToolbar As ESRI.ArcGIS.Controls.AxToolbarControl
    Friend WithEvents AxTOCControl1 As ESRI.ArcGIS.Controls.AxTOCControl

End Class
