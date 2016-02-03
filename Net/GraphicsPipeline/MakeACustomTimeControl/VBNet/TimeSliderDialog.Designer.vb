<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TimeSliderDialog
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
    Me.m_timeSlider = New System.Windows.Forms.TrackBar
    Me.m_datePicker = New System.Windows.Forms.DateTimePicker
    CType(Me.m_timeSlider, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'm_timeSlider
    '
    Me.m_timeSlider.Location = New System.Drawing.Point(12, 12)
    Me.m_timeSlider.Maximum = 100
    Me.m_timeSlider.Minimum = 1
    Me.m_timeSlider.Name = "m_timeSlider"
    Me.m_timeSlider.Size = New System.Drawing.Size(401, 45)
    Me.m_timeSlider.TabIndex = 0
    Me.m_timeSlider.TickFrequency = 4
    Me.m_timeSlider.Value = 1
    '
    'm_datePicker
    '
    Me.m_datePicker.Location = New System.Drawing.Point(102, 63)
    Me.m_datePicker.Name = "m_datePicker"
    Me.m_datePicker.Size = New System.Drawing.Size(200, 20)
    Me.m_datePicker.TabIndex = 1
    '
    'TimeSliderDialog
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(435, 121)
    Me.Controls.Add(Me.m_datePicker)
    Me.Controls.Add(Me.m_timeSlider)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "TimeSliderDialog"
    Me.ShowInTaskbar = False
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
    Me.Text = "Custom Time Control"
    CType(Me.m_timeSlider, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents m_timeSlider As System.Windows.Forms.TrackBar
  Friend WithEvents m_datePicker As System.Windows.Forms.DateTimePicker

End Class
