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
Imports Microsoft.VisualBasic
Imports System
Namespace SingleLineGeocoding
	Public Partial Class SingleLineGeocodingForm
		''' <summary>
		''' Required designer variable.
		''' </summary>
		Private components As System.ComponentModel.IContainer = Nothing

		''' <summary>
		''' Clean up any resources being used.
		''' </summary>
		''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		Protected Overrides Sub Dispose(ByVal disposing As Boolean)
			If disposing AndAlso (Not components Is Nothing) Then
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
            Me.locatorTextBox = New System.Windows.Forms.TextBox
            Me.locatorButton = New System.Windows.Forms.Button
            Me.locatorLabel = New System.Windows.Forms.Label
            Me.addressTextBox = New System.Windows.Forms.TextBox
            Me.addressLabel = New System.Windows.Forms.Label
            Me.ResultsTextBox = New System.Windows.Forms.RichTextBox
            Me.resultsLabel = New System.Windows.Forms.Label
            Me.openFileDialog = New System.Windows.Forms.OpenFileDialog
            Me.findButton = New System.Windows.Forms.Button
            Me.SuspendLayout()
            '
            'locatorTextBox
            '
            Me.locatorTextBox.Location = New System.Drawing.Point(12, 27)
            Me.locatorTextBox.Name = "locatorTextBox"
            Me.locatorTextBox.Size = New System.Drawing.Size(438, 20)
            Me.locatorTextBox.TabIndex = 0
            '
            'locatorButton
            '
            Me.locatorButton.Location = New System.Drawing.Point(456, 25)
            Me.locatorButton.Name = "locatorButton"
            Me.locatorButton.Size = New System.Drawing.Size(75, 23)
            Me.locatorButton.TabIndex = 1
            Me.locatorButton.Text = "Browse..."
            Me.locatorButton.UseVisualStyleBackColor = True
            '
            'locatorLabel
            '
            Me.locatorLabel.AutoSize = True
            Me.locatorLabel.Location = New System.Drawing.Point(12, 9)
            Me.locatorLabel.Name = "locatorLabel"
            Me.locatorLabel.Size = New System.Drawing.Size(84, 13)
            Me.locatorLabel.TabIndex = 2
            Me.locatorLabel.Text = "Address Locator"
            '
            'addressTextBox
            '
            Me.addressTextBox.Enabled = False
            Me.addressTextBox.Location = New System.Drawing.Point(12, 92)
            Me.addressTextBox.Name = "addressTextBox"
            Me.addressTextBox.Size = New System.Drawing.Size(438, 20)
            Me.addressTextBox.TabIndex = 3
            '
            'addressLabel
            '
            Me.addressLabel.AutoSize = True
            Me.addressLabel.Location = New System.Drawing.Point(12, 76)
            Me.addressLabel.Name = "addressLabel"
            Me.addressLabel.Size = New System.Drawing.Size(45, 13)
            Me.addressLabel.TabIndex = 4
            Me.addressLabel.Text = "Address"
            '
            'ResultsTextBox
            '
            Me.ResultsTextBox.Location = New System.Drawing.Point(12, 158)
            Me.ResultsTextBox.Name = "ResultsTextBox"
            Me.ResultsTextBox.Size = New System.Drawing.Size(519, 139)
            Me.ResultsTextBox.TabIndex = 5
            Me.ResultsTextBox.Text = ""
            '
            'resultsLabel
            '
            Me.resultsLabel.AutoSize = True
            Me.resultsLabel.Location = New System.Drawing.Point(12, 142)
            Me.resultsLabel.Name = "resultsLabel"
            Me.resultsLabel.Size = New System.Drawing.Size(42, 13)
            Me.resultsLabel.TabIndex = 6
            Me.resultsLabel.Text = "Results"
            '
            'openFileDialog
            '
            Me.openFileDialog.AddExtension = False
            Me.openFileDialog.Filter = """Locator (*.loc)""|*.loc"
            '
            'findButton
            '
            Me.findButton.Location = New System.Drawing.Point(456, 90)
            Me.findButton.Name = "findButton"
            Me.findButton.Size = New System.Drawing.Size(75, 23)
            Me.findButton.TabIndex = 7
            Me.findButton.Text = "Find"
            Me.findButton.UseVisualStyleBackColor = True
            '
            'SingleLineGeocodingForm
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(543, 309)
            Me.Controls.Add(Me.findButton)
            Me.Controls.Add(Me.resultsLabel)
            Me.Controls.Add(Me.ResultsTextBox)
            Me.Controls.Add(Me.addressLabel)
            Me.Controls.Add(Me.addressTextBox)
            Me.Controls.Add(Me.locatorLabel)
            Me.Controls.Add(Me.locatorButton)
            Me.Controls.Add(Me.locatorTextBox)
            Me.Name = "SingleLineGeocodingForm"
            Me.Text = "SingleLineGeocoding"
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub

		#End Region

		Private locatorTextBox As System.Windows.Forms.TextBox
		Private WithEvents locatorButton As System.Windows.Forms.Button
		Private locatorLabel As System.Windows.Forms.Label
		Private WithEvents addressTextBox As System.Windows.Forms.TextBox
		Private addressLabel As System.Windows.Forms.Label
		Private ResultsTextBox As System.Windows.Forms.RichTextBox
		Private resultsLabel As System.Windows.Forms.Label
		Private openFileDialog As System.Windows.Forms.OpenFileDialog
		Private WithEvents findButton As System.Windows.Forms.Button
	End Class
End Namespace

