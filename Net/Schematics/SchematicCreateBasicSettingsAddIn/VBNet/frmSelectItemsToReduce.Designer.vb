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
' Copyright 2010 ESRI
' 
' All rights reserved under the copyright laws of the United States
' and applicable international laws, treaties, and conventions.
'
' You may freely redistribute and use this sample code, with or
' without modification, provided you include the original copyright
' notice and use restrictions.
' 
' See the use restrictions at &ltyour ArcGIS install location&gt/DeveloperKit10.0/userestrictions.txt.

Partial Class frmSelectItemsToReduce
	Inherits System.Windows.Forms.Form
	''' <summary>
	''' Required designer variable.
	''' </summary>
	Private components As System.ComponentModel.IContainer = Nothing

	''' <summary>
	''' Clean up any resources being used.
	''' </summary>
	''' <param name="disposing">true if managed resources should be disposed otherwise, false.</param>
	Protected Overrides Sub Dispose(ByVal disposing As Boolean)
		If (disposing AndAlso (components IsNot Nothing)) Then
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
		Me.chkListBox = New System.Windows.Forms.CheckedListBox()
		Me.label1 = New System.Windows.Forms.Label()
		Me.btnDone = New System.Windows.Forms.Button()
		Me.btnCancel = New System.Windows.Forms.Button()
		Me.SuspendLayout()
		' 
		' chkListBox
		' 
		Me.chkListBox.CheckOnClick = True
		Me.chkListBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0)
		Me.chkListBox.FormattingEnabled = True
		Me.chkListBox.Location = New System.Drawing.Point(2, 36)
		Me.chkListBox.Name = "chkListBox"
		Me.chkListBox.Size = New System.Drawing.Size(484, 246)
		Me.chkListBox.TabIndex = 0
		' 
		' label1
		' 
		Me.label1.AutoSize = True
		Me.label1.Location = New System.Drawing.Point(-1, 9)
		Me.label1.Name = "label1"
		Me.label1.Size = New System.Drawing.Size(364, 17)
		Me.label1.TabIndex = 1
		Me.label1.Text = "Each checked item will end up with a node reduction rule"
		' 
		' btnDone
		' 
		Me.btnDone.Location = New System.Drawing.Point(411, 289)
		Me.btnDone.Name = "btnDone"
		Me.btnDone.Size = New System.Drawing.Size(75, 23)
		Me.btnDone.TabIndex = 2
		Me.btnDone.Text = "Next >"
		Me.btnDone.UseVisualStyleBackColor = True
		AddHandler Me.btnDone.Click, New System.EventHandler(AddressOf btnDone_Click)
		' 
		' btnCancel
		' 
		Me.btnCancel.Location = New System.Drawing.Point(330, 289)
		Me.btnCancel.Name = "btnCancel"
		Me.btnCancel.Size = New System.Drawing.Size(75, 23)
		Me.btnCancel.TabIndex = 3
		Me.btnCancel.Text = "Cancel"
		Me.btnCancel.UseVisualStyleBackColor = True
		AddHandler Me.btnCancel.Click, New System.EventHandler(AddressOf btnCancel_Click)
		' 
		' frmSelectItemsToReduce
		' 
		Me.AcceptButton = Me.btnDone
		Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0F, 16.0F)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(490, 317)
		Me.ControlBox = False
		Me.Controls.Add(Me.btnCancel)
		Me.Controls.Add(Me.btnDone)
		Me.Controls.Add(Me.label1)
		Me.Controls.Add(Me.chkListBox)
		Me.Name = "frmSelectItemsToReduce"
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
		Me.Text = "Select Network Feature Classes To Reduce"
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub

#End Region

	Private chkListBox As System.Windows.Forms.CheckedListBox
	Private label1 As System.Windows.Forms.Label
	Private btnDone As System.Windows.Forms.Button
	Private btnCancel As System.Windows.Forms.Button
End Class