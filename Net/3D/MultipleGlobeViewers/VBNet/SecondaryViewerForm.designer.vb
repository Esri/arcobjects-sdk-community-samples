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
Imports Microsoft.VisualBasic
Imports System
Namespace MultipleGlobeViewers
	Partial Public Class SecondaryViewerForm
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
			Me.label1 = New System.Windows.Forms.Label()
			Me.viewerListBox = New System.Windows.Forms.ListBox()
			Me.topDownButton = New System.Windows.Forms.Button()
			Me.normalButton = New System.Windows.Forms.Button()
			Me.SuspendLayout()
			' 
			' label1
			' 
			Me.label1.AutoSize = True
			Me.label1.Location = New System.Drawing.Point(2, 12)
			Me.label1.Name = "label1"
			Me.label1.Size = New System.Drawing.Size(90, 13)
			Me.label1.TabIndex = 0
			Me.label1.Text = "Select the Viewer"
			' 
			' viewerListBox
			' 
			Me.viewerListBox.FormattingEnabled = True
			Me.viewerListBox.Location = New System.Drawing.Point(98, 12)
			Me.viewerListBox.Name = "viewerListBox"
			Me.viewerListBox.Size = New System.Drawing.Size(128, 56)
			Me.viewerListBox.TabIndex = 1
			' 
			' topDownButton
			' 
			Me.topDownButton.Location = New System.Drawing.Point(29, 83)
			Me.topDownButton.Name = "topDownButton"
			Me.topDownButton.Size = New System.Drawing.Size(75, 23)
			Me.topDownButton.TabIndex = 2
			Me.topDownButton.Text = "Top Down"
			Me.topDownButton.UseVisualStyleBackColor = True
			' 
			' normalButton
			' 
			Me.normalButton.Location = New System.Drawing.Point(129, 83)
			Me.normalButton.Name = "normalButton"
			Me.normalButton.Size = New System.Drawing.Size(75, 23)
			Me.normalButton.TabIndex = 3
			Me.normalButton.Text = "Syncd Up"
			Me.normalButton.UseVisualStyleBackColor = True
			' 
			' SecondaryViewerForm
			' 
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.ClientSize = New System.Drawing.Size(235, 113)
			Me.Controls.Add(Me.normalButton)
			Me.Controls.Add(Me.topDownButton)
			Me.Controls.Add(Me.viewerListBox)
			Me.Controls.Add(Me.label1)
			Me.Name = "SecondaryViewerForm"
			Me.Text = "SecondaryViewer"
			Me.TopMost = True
			Me.ResumeLayout(False)
			Me.PerformLayout()

		End Sub

		#End Region

		Private label1 As System.Windows.Forms.Label
		Public viewerListBox As System.Windows.Forms.ListBox
		Public topDownButton As System.Windows.Forms.Button
		Public normalButton As System.Windows.Forms.Button

	End Class
End Namespace