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
Partial Class FrmGxStyleView
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.picStylePreview = New System.Windows.Forms.PictureBox
        CType(Me.picStylePreview, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'picStylePreview
        '
        Me.picStylePreview.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.picStylePreview.Location = New System.Drawing.Point(8, 12)
        Me.picStylePreview.Name = "picStylePreview"
        Me.picStylePreview.Size = New System.Drawing.Size(419, 372)
        Me.picStylePreview.TabIndex = 0
        Me.picStylePreview.TabStop = False
        '
        'FrmGxStyleView
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(432, 388)
        Me.Controls.Add(Me.picStylePreview)
        Me.Name = "FrmGxStyleView"
        Me.Text = "FrmGxStyleView"
        CType(Me.picStylePreview, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents picStylePreview As System.Windows.Forms.PictureBox
    'Friend WithEvents picStylePreview As System.Windows.Forms.PictureBox
End Class
