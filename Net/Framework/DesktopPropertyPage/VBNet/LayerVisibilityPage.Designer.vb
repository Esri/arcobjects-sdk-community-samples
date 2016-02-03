<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LayerVisibilityPage
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.radioButtonHide = New System.Windows.Forms.RadioButton
        Me.radioButtonShow = New System.Windows.Forms.RadioButton
        Me.SuspendLayout()
        '
        'radioButtonHide
        '
        Me.radioButtonHide.AutoSize = True
        Me.radioButtonHide.Location = New System.Drawing.Point(41, 65)
        Me.radioButtonHide.Name = "radioButtonHide"
        Me.radioButtonHide.Size = New System.Drawing.Size(63, 17)
        Me.radioButtonHide.TabIndex = 3
        Me.radioButtonHide.TabStop = True
        Me.radioButtonHide.Text = "Invisible"
        Me.radioButtonHide.UseVisualStyleBackColor = True
        '
        'radioButtonShow
        '
        Me.radioButtonShow.AutoSize = True
        Me.radioButtonShow.Location = New System.Drawing.Point(41, 30)
        Me.radioButtonShow.Name = "radioButtonShow"
        Me.radioButtonShow.Size = New System.Drawing.Size(55, 17)
        Me.radioButtonShow.TabIndex = 2
        Me.radioButtonShow.TabStop = True
        Me.radioButtonShow.Text = "Visible"
        Me.radioButtonShow.UseVisualStyleBackColor = True
        '
        'LayerVisibilityPage
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.radioButtonHide)
        Me.Controls.Add(Me.radioButtonShow)
        Me.Name = "LayerVisibilityPage"
        Me.Size = New System.Drawing.Size(300, 150)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents radioButtonHide As System.Windows.Forms.RadioButton
    Private WithEvents radioButtonShow As System.Windows.Forms.RadioButton

End Class
