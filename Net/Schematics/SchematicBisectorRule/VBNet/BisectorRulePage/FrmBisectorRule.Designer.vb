<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmBisectorRule
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

    'Requise par le Concepteur Windows Form
    Private components As System.ComponentModel.IContainer

    'REMARQUE : la procédure suivante est requise par le Concepteur Windows Form
    'Elle peut être modifiée à l'aide du Concepteur Windows Form.  
    'Ne la modifiez pas à l'aide de l'éditeur de code.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.TxtDescription = New System.Windows.Forms.TextBox
        Me.lblDescription = New System.Windows.Forms.Label
        Me.cmbParentNodeClass = New System.Windows.Forms.ComboBox
        Me.lblGroup = New System.Windows.Forms.GroupBox
        Me.lblDistance = New System.Windows.Forms.Label
        Me.lblTargetLink = New System.Windows.Forms.Label
        Me.lblTargetNode = New System.Windows.Forms.Label
        Me.txtDistance = New System.Windows.Forms.TextBox
        Me.cmbTargetLinkClass = New System.Windows.Forms.ComboBox
        Me.cmbTargetNodeClass = New System.Windows.Forms.ComboBox
        Me.lblParentNode = New System.Windows.Forms.Label
        Me.lblGroup.SuspendLayout()
        Me.SuspendLayout()
        '
        'TxtDescription
        '
        Me.TxtDescription.Location = New System.Drawing.Point(31, 28)
        Me.TxtDescription.Name = "TxtDescription"
        Me.TxtDescription.Size = New System.Drawing.Size(334, 20)
        Me.TxtDescription.TabIndex = 0
        '
        'lblDescription
        '
        Me.lblDescription.AutoSize = True
        Me.lblDescription.Location = New System.Drawing.Point(28, 9)
        Me.lblDescription.Name = "lblDescription"
        Me.lblDescription.Size = New System.Drawing.Size(64, 14)
        Me.lblDescription.TabIndex = 5
        Me.lblDescription.Text = "Description:"
        '
        'cmbParentNodeClass
        '
        Me.cmbParentNodeClass.FormattingEnabled = True
        Me.cmbParentNodeClass.Location = New System.Drawing.Point(31, 78)
        Me.cmbParentNodeClass.Name = "cmbParentNodeClass"
        Me.cmbParentNodeClass.Size = New System.Drawing.Size(332, 22)
        Me.cmbParentNodeClass.TabIndex = 1
        '
        'lblGroup
        '
        Me.lblGroup.Controls.Add(Me.lblDistance)
        Me.lblGroup.Controls.Add(Me.lblTargetLink)
        Me.lblGroup.Controls.Add(Me.lblTargetNode)
        Me.lblGroup.Controls.Add(Me.txtDistance)
        Me.lblGroup.Controls.Add(Me.cmbTargetLinkClass)
        Me.lblGroup.Controls.Add(Me.cmbTargetNodeClass)
        Me.lblGroup.Location = New System.Drawing.Point(31, 115)
        Me.lblGroup.Name = "lblGroup"
        Me.lblGroup.Size = New System.Drawing.Size(332, 170)
        Me.lblGroup.TabIndex = 7
        Me.lblGroup.TabStop = False
        Me.lblGroup.Text = "Target schematic feature classes"
        '
        'lblDistance
        '
        Me.lblDistance.AutoSize = True
        Me.lblDistance.Location = New System.Drawing.Point(19, 143)
        Me.lblDistance.Name = "lblDistance"
        Me.lblDistance.Size = New System.Drawing.Size(52, 14)
        Me.lblDistance.TabIndex = 10
        Me.lblDistance.Text = "Distance:"
        '
        'lblTargetLink
        '
        Me.lblTargetLink.AutoSize = True
        Me.lblTargetLink.Location = New System.Drawing.Point(9, 78)
        Me.lblTargetLink.Name = "lblTargetLink"
        Me.lblTargetLink.Size = New System.Drawing.Size(226, 14)
        Me.lblTargetLink.TabIndex = 9
        Me.lblTargetLink.Text = "Select the target link schematic feature class:"
        '
        'lblTargetNode
        '
        Me.lblTargetNode.AutoSize = True
        Me.lblTargetNode.Location = New System.Drawing.Point(10, 24)
        Me.lblTargetNode.Name = "lblTargetNode"
        Me.lblTargetNode.Size = New System.Drawing.Size(235, 14)
        Me.lblTargetNode.TabIndex = 8
        Me.lblTargetNode.Text = "Select the target node schematic feature class:"
        '
        'txtDistance
        '
        Me.txtDistance.Location = New System.Drawing.Point(77, 138)
        Me.txtDistance.Name = "txtDistance"
        Me.txtDistance.Size = New System.Drawing.Size(232, 20)
        Me.txtDistance.TabIndex = 4
        '
        'cmbTargetLinkClass
        '
        Me.cmbTargetLinkClass.FormattingEnabled = True
        Me.cmbTargetLinkClass.Location = New System.Drawing.Point(10, 95)
        Me.cmbTargetLinkClass.Name = "cmbTargetLinkClass"
        Me.cmbTargetLinkClass.Size = New System.Drawing.Size(299, 22)
        Me.cmbTargetLinkClass.TabIndex = 3
        '
        'cmbTargetNodeClass
        '
        Me.cmbTargetNodeClass.FormattingEnabled = True
        Me.cmbTargetNodeClass.Location = New System.Drawing.Point(10, 42)
        Me.cmbTargetNodeClass.Name = "cmbTargetNodeClass"
        Me.cmbTargetNodeClass.Size = New System.Drawing.Size(303, 22)
        Me.cmbTargetNodeClass.TabIndex = 2
        '
        'lblParentNode
        '
        Me.lblParentNode.AutoSize = True
        Me.lblParentNode.Location = New System.Drawing.Point(28, 61)
        Me.lblParentNode.Name = "lblParentNode"
        Me.lblParentNode.Size = New System.Drawing.Size(238, 14)
        Me.lblParentNode.TabIndex = 6
        Me.lblParentNode.Text = "Select the parent node schematic feature class:"
        '
        'FrmBisectorRule
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.ClientSize = New System.Drawing.Size(392, 304)
        Me.Controls.Add(Me.lblParentNode)
        Me.Controls.Add(Me.lblGroup)
        Me.Controls.Add(Me.cmbParentNodeClass)
        Me.Controls.Add(Me.lblDescription)
        Me.Controls.Add(Me.TxtDescription)
        Me.Font = New System.Drawing.Font("Arial", 8.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmBisectorRule"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "FrmBisectorRule"
        Me.lblGroup.ResumeLayout(False)
        Me.lblGroup.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TxtDescription As System.Windows.Forms.TextBox
    Friend WithEvents lblDescription As System.Windows.Forms.Label
    Friend WithEvents cmbParentNodeClass As System.Windows.Forms.ComboBox
    Friend WithEvents lblGroup As System.Windows.Forms.GroupBox
    Friend WithEvents cmbTargetLinkClass As System.Windows.Forms.ComboBox
    Friend WithEvents cmbTargetNodeClass As System.Windows.Forms.ComboBox
    Friend WithEvents lblTargetLink As System.Windows.Forms.Label
    Friend WithEvents lblTargetNode As System.Windows.Forms.Label
    Friend WithEvents txtDistance As System.Windows.Forms.TextBox
    Friend WithEvents lblDistance As System.Windows.Forms.Label
    Friend WithEvents lblParentNode As System.Windows.Forms.Label
End Class
