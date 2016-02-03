Imports Microsoft.VisualBasic
Imports System
Namespace FindAddress
	Partial Public Class AddressForm
		''' <summary>
		''' Required designer variable.
		''' </summary>
		Private components As System.ComponentModel.IContainer = Nothing

		''' <summary>
		''' Clean up any resources being used.
		''' </summary>
		''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		Protected Overrides Sub Dispose(ByVal disposing As Boolean)
			If disposing AndAlso (components IsNot Nothing) Then
				components.Dispose()
			End If
			MyBase.Dispose(disposing)
		End Sub

		#Region "Windows Form Designer generated code"

		''' <summary>
		''' Required method for Designer support - do not modify
		''' the contents of this method with the code editor.
		''' </summary>
		Private Sub InitializeComponent()
			Me.AddressTextBox = New System.Windows.Forms.TextBox()
			Me.CityTextBox = New System.Windows.Forms.TextBox()
			Me.StateTextBox = New System.Windows.Forms.TextBox()
			Me.ZipTextBox = New System.Windows.Forms.TextBox()
			Me.AddressLabel = New System.Windows.Forms.Label()
			Me.CityLabel = New System.Windows.Forms.Label()
			Me.StateLabel = New System.Windows.Forms.Label()
			Me.ZipLabel = New System.Windows.Forms.Label()
			Me.FindButton = New System.Windows.Forms.Button()
			Me.ResultsTextBox = New System.Windows.Forms.RichTextBox()
			Me.ResultsLabel = New System.Windows.Forms.Label()
			Me.SuspendLayout()
			' 
			' AddressTextBox
			' 
			Me.AddressTextBox.Location = New System.Drawing.Point(63, 12)
			Me.AddressTextBox.Name = "AddressTextBox"
			Me.AddressTextBox.Size = New System.Drawing.Size(245, 20)
			Me.AddressTextBox.TabIndex = 0
			' 
			' CityTextBox
			' 
			Me.CityTextBox.Location = New System.Drawing.Point(63, 38)
			Me.CityTextBox.Name = "CityTextBox"
			Me.CityTextBox.Size = New System.Drawing.Size(245, 20)
			Me.CityTextBox.TabIndex = 1
			' 
			' StateTextBox
			' 
			Me.StateTextBox.Location = New System.Drawing.Point(63, 64)
			Me.StateTextBox.Name = "StateTextBox"
			Me.StateTextBox.Size = New System.Drawing.Size(245, 20)
			Me.StateTextBox.TabIndex = 2
'			Me.StateTextBox.KeyDown += New System.Windows.Forms.KeyEventHandler(StateTextBox_KeyDown);
			' 
			' ZipTextBox
			' 
			Me.ZipTextBox.Location = New System.Drawing.Point(63, 90)
			Me.ZipTextBox.Name = "ZipTextBox"
			Me.ZipTextBox.Size = New System.Drawing.Size(245, 20)
			Me.ZipTextBox.TabIndex = 3
'			Me.ZipTextBox.KeyDown += New System.Windows.Forms.KeyEventHandler(ZipTextBox_KeyDown);
			' 
			' AddressLabel
			' 
			Me.AddressLabel.AutoSize = True
			Me.AddressLabel.Location = New System.Drawing.Point(12, 15)
			Me.AddressLabel.Name = "AddressLabel"
			Me.AddressLabel.Size = New System.Drawing.Size(45, 13)
			Me.AddressLabel.TabIndex = 6
			Me.AddressLabel.Text = "Address"
			' 
			' CityLabel
			' 
			Me.CityLabel.AutoSize = True
			Me.CityLabel.Location = New System.Drawing.Point(12, 41)
			Me.CityLabel.Name = "CityLabel"
			Me.CityLabel.Size = New System.Drawing.Size(24, 13)
			Me.CityLabel.TabIndex = 7
			Me.CityLabel.Text = "City"
			' 
			' StateLabel
			' 
			Me.StateLabel.AutoSize = True
			Me.StateLabel.Location = New System.Drawing.Point(12, 67)
			Me.StateLabel.Name = "StateLabel"
			Me.StateLabel.Size = New System.Drawing.Size(32, 13)
			Me.StateLabel.TabIndex = 8
			Me.StateLabel.Text = "State"
			' 
			' ZipLabel
			' 
			Me.ZipLabel.AutoSize = True
			Me.ZipLabel.Location = New System.Drawing.Point(12, 93)
			Me.ZipLabel.Name = "ZipLabel"
			Me.ZipLabel.Size = New System.Drawing.Size(22, 13)
			Me.ZipLabel.TabIndex = 9
			Me.ZipLabel.Text = "Zip"
			' 
			' FindButton
			' 
			Me.FindButton.Location = New System.Drawing.Point(314, 87)
			Me.FindButton.Name = "FindButton"
			Me.FindButton.Size = New System.Drawing.Size(75, 23)
			Me.FindButton.TabIndex = 4
			Me.FindButton.Text = "Find"
			Me.FindButton.UseVisualStyleBackColor = True
'			Me.FindButton.Click += New System.EventHandler(Me.FindButton_Click);
			' 
			' ResultsTextBox
			' 
			Me.ResultsTextBox.BackColor = System.Drawing.SystemColors.Window
			Me.ResultsTextBox.Location = New System.Drawing.Point(15, 152)
			Me.ResultsTextBox.Name = "ResultsTextBox"
			Me.ResultsTextBox.Size = New System.Drawing.Size(374, 119)
			Me.ResultsTextBox.TabIndex = 5
			Me.ResultsTextBox.Text = ""
			' 
			' ResultsLabel
			' 
			Me.ResultsLabel.AutoSize = True
			Me.ResultsLabel.Location = New System.Drawing.Point(22, 136)
			Me.ResultsLabel.Name = "ResultsLabel"
			Me.ResultsLabel.Size = New System.Drawing.Size(42, 13)
			Me.ResultsLabel.TabIndex = 10
			Me.ResultsLabel.Text = "Results"
			' 
			' AddressForm
			' 
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.ClientSize = New System.Drawing.Size(401, 283)
			Me.Controls.Add(Me.ResultsLabel)
			Me.Controls.Add(Me.ResultsTextBox)
			Me.Controls.Add(Me.FindButton)
			Me.Controls.Add(Me.ZipLabel)
			Me.Controls.Add(Me.StateLabel)
			Me.Controls.Add(Me.CityLabel)
			Me.Controls.Add(Me.AddressLabel)
			Me.Controls.Add(Me.ZipTextBox)
			Me.Controls.Add(Me.StateTextBox)
			Me.Controls.Add(Me.CityTextBox)
			Me.Controls.Add(Me.AddressTextBox)
			Me.Name = "AddressForm"
			Me.Text = "Address Search"
			Me.ResumeLayout(False)
			Me.PerformLayout()

		End Sub

		#End Region

		Private AddressTextBox As System.Windows.Forms.TextBox
		Private CityTextBox As System.Windows.Forms.TextBox
		Private WithEvents StateTextBox As System.Windows.Forms.TextBox
		Private WithEvents ZipTextBox As System.Windows.Forms.TextBox
		Private AddressLabel As System.Windows.Forms.Label
		Private CityLabel As System.Windows.Forms.Label
		Private StateLabel As System.Windows.Forms.Label
		Private ZipLabel As System.Windows.Forms.Label
		Private WithEvents FindButton As System.Windows.Forms.Button
		Private ResultsTextBox As System.Windows.Forms.RichTextBox
		Private ResultsLabel As System.Windows.Forms.Label
	End Class
End Namespace

