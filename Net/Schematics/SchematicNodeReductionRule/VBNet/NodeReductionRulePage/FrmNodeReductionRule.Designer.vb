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
Partial Class FrmNodeReductionRule
    Inherits System.Windows.Forms.Form

    'Form remplace la méthode Dispose pour nettoyer la liste des composants.
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
        Me.TxtDescription = New System.Windows.Forms.TextBox()
        Me.chkKeepVertices = New System.Windows.Forms.CheckBox()
        Me.lblTargetSuperspan = New System.Windows.Forms.Label()
        Me.lblAttributeName = New System.Windows.Forms.Label()
        Me.lblGroup = New System.Windows.Forms.GroupBox()
        Me.txtLinkAttribute = New System.Windows.Forms.TextBox()
        Me.chkLinkAttribute = New System.Windows.Forms.CheckBox()
        Me.cmbAttributeName = New System.Windows.Forms.ComboBox()
        Me.cmbTargetSuperspanClass = New System.Windows.Forms.ComboBox()
        Me.lblReducedNode = New System.Windows.Forms.Label()
        Me.cmbReducedNodeClass = New System.Windows.Forms.ComboBox()
        Me.lblDescription = New System.Windows.Forms.Label()
        Me.lblGroup.SuspendLayout()
        Me.SuspendLayout()
        '
        'TxtDescription
        '
        Me.TxtDescription.Location = New System.Drawing.Point(30, 30)
        Me.TxtDescription.Name = "TxtDescription"
        Me.TxtDescription.Size = New System.Drawing.Size(336, 20)
        Me.TxtDescription.TabIndex = 8
        '
        'chkKeepVertices
        '
        Me.chkKeepVertices.Checked = True
        Me.chkKeepVertices.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkKeepVertices.Location = New System.Drawing.Point(11, 188)
        Me.chkKeepVertices.Name = "chkKeepVertices"
        Me.chkKeepVertices.Size = New System.Drawing.Size(305, 18)
        Me.chkKeepVertices.TabIndex = 11
        Me.chkKeepVertices.Text = "Keep vertices"
        Me.chkKeepVertices.UseVisualStyleBackColor = True
        '
        'lblTargetSuperspan
        '
        Me.lblTargetSuperspan.AutoSize = True
        Me.lblTargetSuperspan.Location = New System.Drawing.Point(7, 24)
        Me.lblTargetSuperspan.Name = "lblTargetSuperspan"
        Me.lblTargetSuperspan.Size = New System.Drawing.Size(136, 14)
        Me.lblTargetSuperspan.TabIndex = 8
        Me.lblTargetSuperspan.Text = "Select the schematic class"
        '
        'lblAttributeName
        '
        Me.lblAttributeName.AutoSize = True
        Me.lblAttributeName.Location = New System.Drawing.Point(8, 78)
        Me.lblAttributeName.Name = "lblAttributeName"
        Me.lblAttributeName.Size = New System.Drawing.Size(130, 14)
        Me.lblAttributeName.TabIndex = 10
        Me.lblAttributeName.Text = "Cumulative attribute name"
        '
        'lblGroup
        '
        Me.lblGroup.Controls.Add(Me.txtLinkAttribute)
        Me.lblGroup.Controls.Add(Me.chkLinkAttribute)
        Me.lblGroup.Controls.Add(Me.cmbAttributeName)
        Me.lblGroup.Controls.Add(Me.chkKeepVertices)
        Me.lblGroup.Controls.Add(Me.lblTargetSuperspan)
        Me.lblGroup.Controls.Add(Me.cmbTargetSuperspanClass)
        Me.lblGroup.Controls.Add(Me.lblAttributeName)
        Me.lblGroup.Location = New System.Drawing.Point(30, 124)
        Me.lblGroup.Name = "lblGroup"
        Me.lblGroup.Size = New System.Drawing.Size(336, 216)
        Me.lblGroup.TabIndex = 12
        Me.lblGroup.TabStop = False
        Me.lblGroup.Text = "Target superspan link"
        '
        'txtLinkAttribute
        '
        Me.txtLinkAttribute.Location = New System.Drawing.Point(17, 154)
        Me.txtLinkAttribute.Name = "txtLinkAttribute"
        Me.txtLinkAttribute.Size = New System.Drawing.Size(297, 20)
        Me.txtLinkAttribute.TabIndex = 14
        '
        'chkLinkAttribute
        '
        Me.chkLinkAttribute.AutoSize = True
        Me.chkLinkAttribute.Checked = True
        Me.chkLinkAttribute.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkLinkAttribute.Location = New System.Drawing.Point(17, 130)
        Me.chkLinkAttribute.Name = "chkLinkAttribute"
        Me.chkLinkAttribute.Size = New System.Drawing.Size(116, 17)
        Me.chkLinkAttribute.TabIndex = 13
        Me.chkLinkAttribute.Text = "By Connected Link"
        Me.chkLinkAttribute.UseVisualStyleBackColor = True
        '
        'cmbAttributeName
        '
        Me.cmbAttributeName.FormattingEnabled = True
        Me.cmbAttributeName.Location = New System.Drawing.Point(13, 100)
        Me.cmbAttributeName.Name = "cmbAttributeName"
        Me.cmbAttributeName.Size = New System.Drawing.Size(302, 22)
        Me.cmbAttributeName.TabIndex = 12
        '
        'cmbTargetSuperspanClass
        '
        Me.cmbTargetSuperspanClass.FormattingEnabled = True
        Me.cmbTargetSuperspanClass.Location = New System.Drawing.Point(11, 41)
        Me.cmbTargetSuperspanClass.Name = "cmbTargetSuperspanClass"
        Me.cmbTargetSuperspanClass.Size = New System.Drawing.Size(306, 22)
        Me.cmbTargetSuperspanClass.TabIndex = 2
        '
        'lblReducedNode
        '
        Me.lblReducedNode.AutoSize = True
        Me.lblReducedNode.Location = New System.Drawing.Point(27, 65)
        Me.lblReducedNode.Name = "lblReducedNode"
        Me.lblReducedNode.Size = New System.Drawing.Size(185, 14)
        Me.lblReducedNode.TabIndex = 11
        Me.lblReducedNode.Text = "Select the schematic class to reduce"
        '
        'cmbReducedNodeClass
        '
        Me.cmbReducedNodeClass.FormattingEnabled = True
        Me.cmbReducedNodeClass.Location = New System.Drawing.Point(30, 85)
        Me.cmbReducedNodeClass.Name = "cmbReducedNodeClass"
        Me.cmbReducedNodeClass.Size = New System.Drawing.Size(336, 22)
        Me.cmbReducedNodeClass.TabIndex = 9
        '
        'lblDescription
        '
        Me.lblDescription.AutoSize = True
        Me.lblDescription.Location = New System.Drawing.Point(27, 11)
        Me.lblDescription.Name = "lblDescription"
        Me.lblDescription.Size = New System.Drawing.Size(64, 14)
        Me.lblDescription.TabIndex = 10
        Me.lblDescription.Text = "Description:"
        '
        'FrmNodeReductionRule
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.ClientSize = New System.Drawing.Size(392, 352)
        Me.Controls.Add(Me.TxtDescription)
        Me.Controls.Add(Me.lblDescription)
        Me.Controls.Add(Me.lblGroup)
        Me.Controls.Add(Me.cmbReducedNodeClass)
        Me.Controls.Add(Me.lblReducedNode)
        Me.Font = New System.Drawing.Font("Arial", 8.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmNodeReductionRule"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "FrmNodeReductionRule"
        Me.lblGroup.ResumeLayout(False)
        Me.lblGroup.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Public WithEvents TxtDescription As System.Windows.Forms.TextBox
    Public WithEvents chkKeepVertices As System.Windows.Forms.CheckBox
    Public WithEvents lblTargetSuperspan As System.Windows.Forms.Label
    Public WithEvents lblAttributeName As System.Windows.Forms.Label
    Public WithEvents lblGroup As System.Windows.Forms.GroupBox
    Public WithEvents lblReducedNode As System.Windows.Forms.Label
    Public WithEvents cmbReducedNodeClass As System.Windows.Forms.ComboBox
    Public WithEvents lblDescription As System.Windows.Forms.Label
    Public WithEvents cmbTargetSuperspanClass As System.Windows.Forms.ComboBox
    Friend WithEvents cmbAttributeName As System.Windows.Forms.ComboBox
    Public WithEvents chkLinkAttribute As System.Windows.Forms.CheckBox
    Public WithEvents txtLinkAttribute As System.Windows.Forms.TextBox
End Class
