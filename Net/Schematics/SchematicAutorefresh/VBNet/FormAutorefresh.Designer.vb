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
Partial Class FormAutorefresh
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
		Me.components = New System.ComponentModel.Container
		Me.ButtonOK = New System.Windows.Forms.Button
		Me.ButtonCancel = New System.Windows.Forms.Button
		Me.AutoOn = New System.Windows.Forms.RadioButton
		Me.AutoOff = New System.Windows.Forms.RadioButton
		Me.IntervalMinute = New System.Windows.Forms.ComboBox
		Me.IntervalSecond = New System.Windows.Forms.ComboBox
		Me.Label1 = New System.Windows.Forms.Label
		Me.Label2 = New System.Windows.Forms.Label
		Me.Label3 = New System.Windows.Forms.Label
		Me.timerAutoRefresh = New System.Windows.Forms.Timer(Me.components)
		Me.SuspendLayout()
		'
		'ButtonOK
		'
		Me.ButtonOK.Anchor = System.Windows.Forms.AnchorStyles.None
		Me.ButtonOK.Location = New System.Drawing.Point(162, 153)
		Me.ButtonOK.Name = "ButtonOK"
		Me.ButtonOK.Size = New System.Drawing.Size(71, 23)
		Me.ButtonOK.TabIndex = 0
		Me.ButtonOK.Text = "OK"
		'
		'ButtonCancel
		'
		Me.ButtonCancel.Location = New System.Drawing.Point(260, 151)
		Me.ButtonCancel.Name = "ButtonCancel"
		Me.ButtonCancel.Size = New System.Drawing.Size(74, 25)
		Me.ButtonCancel.TabIndex = 1
		Me.ButtonCancel.Text = "Cancel"
		Me.ButtonCancel.UseVisualStyleBackColor = True
		'
		'AutoOn
		'
		Me.AutoOn.AutoSize = True
		Me.AutoOn.Location = New System.Drawing.Point(26, 102)
		Me.AutoOn.Name = "AutoOn"
		Me.AutoOn.Size = New System.Drawing.Size(104, 17)
		Me.AutoOn.TabIndex = 2
		Me.AutoOn.Text = "Auto Refresh On"
		Me.AutoOn.UseVisualStyleBackColor = True
		'
		'AutoOff
		'
		Me.AutoOff.AutoSize = True
		Me.AutoOff.Checked = True
		Me.AutoOff.Location = New System.Drawing.Point(152, 102)
		Me.AutoOff.Name = "AutoOff"
		Me.AutoOff.Size = New System.Drawing.Size(104, 17)
		Me.AutoOff.TabIndex = 3
		Me.AutoOff.TabStop = True
		Me.AutoOff.Text = "Auto Refresh Off"
		Me.AutoOff.UseVisualStyleBackColor = True
		'
		'IntervalMinute
		'
		Me.IntervalMinute.FormattingEnabled = True
		Me.IntervalMinute.Location = New System.Drawing.Point(83, 33)
		Me.IntervalMinute.Name = "IntervalMinute"
		Me.IntervalMinute.Size = New System.Drawing.Size(60, 21)
		Me.IntervalMinute.TabIndex = 4
		'
		'IntervalSecond
		'
		Me.IntervalSecond.FormattingEnabled = True
		Me.IntervalSecond.Location = New System.Drawing.Point(228, 33)
		Me.IntervalSecond.Name = "IntervalSecond"
		Me.IntervalSecond.Size = New System.Drawing.Size(61, 21)
		Me.IntervalSecond.TabIndex = 5
		'
		'Label1
		'
		Me.Label1.AutoSize = True
		Me.Label1.Location = New System.Drawing.Point(10, 38)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(67, 13)
		Me.Label1.TabIndex = 6
		Me.Label1.Text = "Time interval"
		'
		'Label2
		'
		Me.Label2.AutoSize = True
		Me.Label2.Location = New System.Drawing.Point(153, 36)
		Me.Label2.Name = "Label2"
		Me.Label2.Size = New System.Drawing.Size(49, 13)
		Me.Label2.TabIndex = 7
		Me.Label2.Text = "minute(s)"
		'
		'Label3
		'
		Me.Label3.AutoSize = True
		Me.Label3.Location = New System.Drawing.Point(302, 36)
		Me.Label3.Name = "Label3"
		Me.Label3.Size = New System.Drawing.Size(53, 13)
		Me.Label3.TabIndex = 8
		Me.Label3.Text = "second(s)"
		'
		'timerAutoRefresh
		'
		'
		'FormAutorefresh
		'
		Me.AcceptButton = Me.ButtonOK
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(366, 198)
		Me.Controls.Add(Me.Label3)
		Me.Controls.Add(Me.Label2)
		Me.Controls.Add(Me.Label1)
		Me.Controls.Add(Me.IntervalSecond)
		Me.Controls.Add(Me.IntervalMinute)
		Me.Controls.Add(Me.AutoOff)
		Me.Controls.Add(Me.AutoOn)
		Me.Controls.Add(Me.ButtonCancel)
		Me.Controls.Add(Me.ButtonOK)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
		Me.MaximizeBox = False
		Me.MinimizeBox = False
		Me.Name = "FormAutorefresh"
		Me.ShowInTaskbar = False
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
		Me.Text = "Schematic Auto Refresh Properties"
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub
	Friend WithEvents ButtonOK As System.Windows.Forms.Button
	Friend WithEvents ButtonCancel As System.Windows.Forms.Button
	Friend WithEvents AutoOn As System.Windows.Forms.RadioButton
	Friend WithEvents AutoOff As System.Windows.Forms.RadioButton
	Friend WithEvents IntervalMinute As System.Windows.Forms.ComboBox
	Friend WithEvents IntervalSecond As System.Windows.Forms.ComboBox
	Friend WithEvents Label1 As System.Windows.Forms.Label
	Friend WithEvents Label2 As System.Windows.Forms.Label
	Friend WithEvents Label3 As System.Windows.Forms.Label
	Friend WithEvents timerAutoRefresh As System.Windows.Forms.Timer

End Class
