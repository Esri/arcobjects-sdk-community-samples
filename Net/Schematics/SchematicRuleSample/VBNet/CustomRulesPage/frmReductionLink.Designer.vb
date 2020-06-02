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
Partial Class frmReductionLink
    Inherits System.Windows.Forms.Form

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

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.cboReduce = New System.Windows.Forms.ComboBox
        Me.lblReduce = New System.Windows.Forms.Label
        Me.chkUsePort = New System.Windows.Forms.CheckBox
        Me.txtDescription = New System.Windows.Forms.TextBox
        Me.lblDescription = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'cboReduce
        '
        Me.cboReduce.FormattingEnabled = True
        Me.cboReduce.Location = New System.Drawing.Point(12, 85)
        Me.cboReduce.Name = "cboReduce"
        Me.cboReduce.Size = New System.Drawing.Size(257, 22)
        Me.cboReduce.TabIndex = 10
        '
        'lblReduce
        '
        Me.lblReduce.BackColor = System.Drawing.SystemColors.Control
        Me.lblReduce.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblReduce.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblReduce.Location = New System.Drawing.Point(12, 64)
        Me.lblReduce.Name = "lblReduce"
        Me.lblReduce.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblReduce.Size = New System.Drawing.Size(257, 18)
        Me.lblReduce.TabIndex = 9
        Me.lblReduce.Text = "Links to reduce"
        '
        'chkUsePort
        '
        Me.chkUsePort.AutoSize = True
        Me.chkUsePort.Location = New System.Drawing.Point(13, 126)
        Me.chkUsePort.Name = "chkUsePort"
        Me.chkUsePort.Size = New System.Drawing.Size(67, 18)
        Me.chkUsePort.TabIndex = 11
        Me.chkUsePort.Text = "Use Port"
        Me.chkUsePort.UseVisualStyleBackColor = True
        '
        'txtDescription
        '
        Me.txtDescription.AcceptsReturn = True
        Me.txtDescription.BackColor = System.Drawing.SystemColors.Window
        Me.txtDescription.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDescription.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDescription.Location = New System.Drawing.Point(12, 30)
        Me.txtDescription.MaxLength = 0
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDescription.Size = New System.Drawing.Size(257, 20)
        Me.txtDescription.TabIndex = 13
        '
        'lblDescription
        '
        Me.lblDescription.BackColor = System.Drawing.SystemColors.Control
        Me.lblDescription.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDescription.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDescription.Location = New System.Drawing.Point(9, 9)
        Me.lblDescription.Name = "lblDescription"
        Me.lblDescription.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDescription.Size = New System.Drawing.Size(257, 18)
        Me.lblDescription.TabIndex = 12
        Me.lblDescription.Text = "Description"
        '
        'frmReductionLink
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(283, 153)
        Me.Controls.Add(Me.txtDescription)
        Me.Controls.Add(Me.lblDescription)
        Me.Controls.Add(Me.chkUsePort)
        Me.Controls.Add(Me.cboReduce)
        Me.Controls.Add(Me.lblReduce)
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmReductionLink"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "frmReductionLink"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Public WithEvents cboReduce As System.Windows.Forms.ComboBox
    Public lblReduce As System.Windows.Forms.Label
    Public WithEvents chkUsePort As System.Windows.Forms.CheckBox
    Public WithEvents txtDescription As System.Windows.Forms.TextBox
    Public lblDescription As System.Windows.Forms.Label
End Class
