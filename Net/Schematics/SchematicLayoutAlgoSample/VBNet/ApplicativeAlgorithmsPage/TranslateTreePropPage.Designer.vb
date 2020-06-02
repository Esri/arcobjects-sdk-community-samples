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
Imports System

Partial Class TranslateTreePropPage
    Private components As System.ComponentModel.IContainer = Nothing

    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If (disposing AndAlso (components IsNot Nothing)) Then
            components.Dispose()
        End If
    End Sub

#Region "Windows Form"

    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.btnRestore = New System.Windows.Forms.Button
        Me.txtYTrans = New System.Windows.Forms.TextBox
        Me.lblYTrans = New System.Windows.Forms.Label
        Me.txtXTrans = New System.Windows.Forms.TextBox
        Me.lblXTrans = New System.Windows.Forms.Label
        Me.timApply = New System.Windows.Forms.Timer(Me.components)
        Me.SuspendLayout()
        '
        'btnRestore
        '
        Me.btnRestore.Location = New System.Drawing.Point(209, 163)
        Me.btnRestore.Name = "btnRestore"
        Me.btnRestore.Size = New System.Drawing.Size(118, 37)
        Me.btnRestore.TabIndex = 4
        Me.btnRestore.Text = "Restore default"
        Me.btnRestore.UseVisualStyleBackColor = True
        '
        'txtYTrans
        '
        Me.txtYTrans.Location = New System.Drawing.Point(160, 87)
        Me.txtYTrans.Name = "txtYTrans"
        Me.txtYTrans.Size = New System.Drawing.Size(93, 20)
        Me.txtYTrans.TabIndex = 3
        '
        'lblYTrans
        '
        Me.lblYTrans.AutoSize = True
        Me.lblYTrans.Location = New System.Drawing.Point(65, 89)
        Me.lblYTrans.Name = "lblYTrans"
        Me.lblYTrans.Size = New System.Drawing.Size(65, 13)
        Me.lblYTrans.TabIndex = 2
        Me.lblYTrans.Text = "Y translation"
        '
        'txtXTrans
        '
        Me.txtXTrans.Location = New System.Drawing.Point(160, 43)
        Me.txtXTrans.Name = "txtXTrans"
        Me.txtXTrans.Size = New System.Drawing.Size(93, 20)
        Me.txtXTrans.TabIndex = 1
        '
        'lblXTrans
        '
        Me.lblXTrans.AutoSize = True
        Me.lblXTrans.Location = New System.Drawing.Point(65, 45)
        Me.lblXTrans.Name = "lblXTrans"
        Me.lblXTrans.Size = New System.Drawing.Size(65, 13)
        Me.lblXTrans.TabIndex = 0
        Me.lblXTrans.Text = "X translation"
        '
        'TranslateTreePropPage
        '
        Me.AccessibleRole = System.Windows.Forms.AccessibleRole.PropertyPage
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ClientSize = New System.Drawing.Size(343, 215)
        Me.Controls.Add(Me.btnRestore)
        Me.Controls.Add(Me.txtYTrans)
        Me.Controls.Add(Me.lblYTrans)
        Me.Controls.Add(Me.txtXTrans)
        Me.Controls.Add(Me.lblXTrans)
        Me.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.MinimumSize = New System.Drawing.Size(343, 215)
        Me.Name = "TranslateTreePropPage"
        Me.ShowIcon = False
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.Text = "Translate Tree"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Friend WithEvents btnRestore As System.Windows.Forms.Button
    Friend WithEvents txtYTrans As System.Windows.Forms.TextBox
    Friend lblYTrans As System.Windows.Forms.Label
    Friend WithEvents txtXTrans As System.Windows.Forms.TextBox
    Friend lblXTrans As System.Windows.Forms.Label
    Friend WithEvents timApply As System.Windows.Forms.Timer
End Class
