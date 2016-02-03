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
' 
Partial Class frmDatasetTemplateName
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
		Me.btnNext = New System.Windows.Forms.Button
		Me.label1 = New System.Windows.Forms.Label
		Me.label2 = New System.Windows.Forms.Label
		Me.txtDatasetName = New System.Windows.Forms.TextBox
		Me.txtTemplateName = New System.Windows.Forms.TextBox
		Me.btnCancel = New System.Windows.Forms.Button
		Me.chkVertices = New System.Windows.Forms.CheckBox
		Me.SuspendLayout()
		'
		'btnNext
		'
		Me.btnNext.DialogResult = System.Windows.Forms.DialogResult.OK
		Me.btnNext.Enabled = False
		Me.btnNext.Location = New System.Drawing.Point(183, 118)
		Me.btnNext.Margin = New System.Windows.Forms.Padding(2)
		Me.btnNext.Name = "btnNext"
		Me.btnNext.Size = New System.Drawing.Size(56, 19)
		Me.btnNext.TabIndex = 0
		Me.btnNext.Text = "Next >"
		Me.btnNext.UseVisualStyleBackColor = True
		'
		'label1
		'
		Me.label1.AutoSize = True
		Me.label1.Location = New System.Drawing.Point(9, 19)
		Me.label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
		Me.label1.Name = "label1"
		Me.label1.Size = New System.Drawing.Size(78, 13)
		Me.label1.TabIndex = 1
		Me.label1.Text = "Dataset Name:"
		'
		'label2
		'
		Me.label2.AutoSize = True
		Me.label2.Location = New System.Drawing.Point(9, 47)
		Me.label2.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
		Me.label2.Name = "label2"
		Me.label2.Size = New System.Drawing.Size(85, 13)
		Me.label2.TabIndex = 2
		Me.label2.Text = "Template Name:"
		'
		'txtDatasetName
		'
		Me.txtDatasetName.Location = New System.Drawing.Point(90, 16)
		Me.txtDatasetName.Margin = New System.Windows.Forms.Padding(2)
		Me.txtDatasetName.Name = "txtDatasetName"
		Me.txtDatasetName.Size = New System.Drawing.Size(150, 20)
		Me.txtDatasetName.TabIndex = 3
		'
		'txtTemplateName
		'
		Me.txtTemplateName.Location = New System.Drawing.Point(98, 45)
		Me.txtTemplateName.Margin = New System.Windows.Forms.Padding(2)
		Me.txtTemplateName.Name = "txtTemplateName"
		Me.txtTemplateName.Size = New System.Drawing.Size(143, 20)
		Me.txtTemplateName.TabIndex = 4
		'
		'btnCancel
		'
		Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
		Me.btnCancel.Location = New System.Drawing.Point(122, 118)
		Me.btnCancel.Margin = New System.Windows.Forms.Padding(2)
		Me.btnCancel.Name = "btnCancel"
		Me.btnCancel.Size = New System.Drawing.Size(56, 19)
		Me.btnCancel.TabIndex = 5
		Me.btnCancel.Text = "Cancel"
		Me.btnCancel.UseVisualStyleBackColor = True
		'
		'chkVertices
		'
		Me.chkVertices.AutoSize = True
		Me.chkVertices.Location = New System.Drawing.Point(11, 73)
		Me.chkVertices.Margin = New System.Windows.Forms.Padding(2)
		Me.chkVertices.Name = "chkVertices"
		Me.chkVertices.Size = New System.Drawing.Size(126, 17)
		Me.chkVertices.TabIndex = 6
		Me.chkVertices.Text = "Use digitized vertices"
		Me.chkVertices.UseVisualStyleBackColor = True
		'
		'frmDatasetTemplateName
		'
		Me.AcceptButton = Me.btnNext
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(251, 141)
		Me.ControlBox = False
		Me.Controls.Add(Me.chkVertices)
		Me.Controls.Add(Me.btnCancel)
		Me.Controls.Add(Me.txtTemplateName)
		Me.Controls.Add(Me.txtDatasetName)
		Me.Controls.Add(Me.label2)
		Me.Controls.Add(Me.label1)
		Me.Controls.Add(Me.btnNext)
		Me.Margin = New System.Windows.Forms.Padding(2)
		Me.Name = "frmDatasetTemplateName"
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
		Me.Text = "Dataset and Template Name"
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub

#End Region

	Public btnNext As System.Windows.Forms.Button
	Private label1 As System.Windows.Forms.Label
	Private label2 As System.Windows.Forms.Label
	Public txtDatasetName As System.Windows.Forms.TextBox
	Public txtTemplateName As System.Windows.Forms.TextBox
	Public btnCancel As System.Windows.Forms.Button
	Private chkVertices As System.Windows.Forms.CheckBox
End Class