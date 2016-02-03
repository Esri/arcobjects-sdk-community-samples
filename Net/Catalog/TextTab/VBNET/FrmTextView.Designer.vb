<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmTextView
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
        Me.txtContents = New System.Windows.Forms.TextBox
        Me.SuspendLayout()
        '
        'txtContents
        '
        Me.txtContents.BackColor = System.Drawing.Color.White
        Me.txtContents.Location = New System.Drawing.Point(11, 9)
        Me.txtContents.Multiline = True
        Me.txtContents.Name = "txtContents"
        Me.txtContents.ReadOnly = True
        Me.txtContents.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtContents.Size = New System.Drawing.Size(423, 411)
        Me.txtContents.TabIndex = 0
        '
        'FrmTextView
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(442, 425)
        Me.Controls.Add(Me.txtContents)
        Me.Name = "FrmTextView"
        Me.Text = "Form2"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtContents As System.Windows.Forms.TextBox
End Class
