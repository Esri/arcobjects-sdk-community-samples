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
Partial Class frmAdvanced
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
		Me.tabControl1 = New System.Windows.Forms.TabControl()
		Me.tabPage1 = New System.Windows.Forms.TabPage()
		Me.cboRoot = New System.Windows.Forms.ComboBox()
		Me.label2 = New System.Windows.Forms.Label()
		Me.label1 = New System.Windows.Forms.Label()
		Me.cboDirection = New System.Windows.Forms.ComboBox()
		Me.chkApplyAlgo = New System.Windows.Forms.CheckBox()
		Me.tabPage2 = New System.Windows.Forms.TabPage()
		Me.chkUseAttributes = New System.Windows.Forms.CheckBox()
		Me.chkFields = New System.Windows.Forms.CheckedListBox()
		Me.tvFeatureClasses = New System.Windows.Forms.TreeView()
		Me.btnDone = New System.Windows.Forms.Button()
		Me.tabControl1.SuspendLayout()
		Me.tabPage1.SuspendLayout()
		Me.tabPage2.SuspendLayout()
		Me.SuspendLayout()
		' 
		' tabControl1
		' 
		Me.tabControl1.Controls.Add(Me.tabPage1)
		Me.tabControl1.Controls.Add(Me.tabPage2)
		Me.tabControl1.Location = new System.Drawing.Point(10, 11)
		Me.tabControl1.Margin = new System.Windows.Forms.Padding(2)
		Me.tabControl1.Name = "tabControl1"
		Me.tabControl1.SelectedIndex = 0
		Me.tabControl1.Size = new System.Drawing.Size(344, 292)
		Me.tabControl1.TabIndex = 0
		' 
		' tabPage1
		' 
		Me.tabPage1.Controls.Add(Me.cboRoot)
		Me.tabPage1.Controls.Add(Me.label2)
		Me.tabPage1.Controls.Add(Me.label1)
		Me.tabPage1.Controls.Add(Me.cboDirection)
		Me.tabPage1.Controls.Add(Me.chkApplyAlgo)
		Me.tabPage1.Location = New System.Drawing.Point(4, 22)
		Me.tabPage1.Margin = New System.Windows.Forms.Padding(2)
		Me.tabPage1.Name = "tabPage1"
		Me.tabPage1.Padding = New System.Windows.Forms.Padding(2)
		Me.tabPage1.Size = New System.Drawing.Size(336, 251)
		Me.tabPage1.TabIndex = 0
		Me.tabPage1.Text = "Algorithm"
		Me.tabPage1.UseVisualStyleBackColor = True
		' 
		' cboRoot
		' 
		Me.cboRoot.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cboRoot.Enabled = False
		Me.cboRoot.FormattingEnabled = True
		Me.cboRoot.Location = New System.Drawing.Point(124, 58)
		Me.cboRoot.Margin = new System.Windows.Forms.Padding(2)
		Me.cboRoot.Name = "cboRoot"
		Me.cboRoot.Size = New System.Drawing.Size(210, 21)
		Me.cboRoot.TabIndex = 4
		' 
		' label2
		' 
		Me.label2.AutoSize = True
		Me.label2.Location = New System.Drawing.Point(17, 63)
		Me.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0)
		Me.label2.Name = "label2"
		Me.label2.Size = New System.Drawing.Size(100, 13)
		Me.label2.TabIndex = 3
		Me.label2.Text = "Root Feature Class:"
		' 
		' label1
		' 
		Me.label1.AutoSize = True
		Me.label1.Location = New System.Drawing.Point(17, 33)
		Me.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0)
		Me.label1.Name = "label1"
		Me.label1.Size = New System.Drawing.Size(52, 13)
		Me.label1.TabIndex = 2
		Me.label1.Text = "Direction:"
		' 
		' cboDirection
		' 
		Me.cboDirection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cboDirection.Enabled = False
		Me.cboDirection.FormattingEnabled = True
		Me.cboDirection.Items.AddRange(New Object() {"Left to Right", "Right to Left", "Top to Bottom", "Bottom to Top"})
		Me.cboDirection.Location = New System.Drawing.Point(73, 28)
		Me.cboDirection.Margin = new System.Windows.Forms.Padding(2)
		Me.cboDirection.Name = "cboDirection"
		Me.cboDirection.Size = New System.Drawing.Size(261, 21)
		Me.cboDirection.TabIndex = 1
		' 
		' chkApplyAlgo
		' 
		Me.chkApplyAlgo.AutoSize = True
		Me.chkApplyAlgo.Location = New System.Drawing.Point(5, 6)
		Me.chkApplyAlgo.Margin = new System.Windows.Forms.Padding(2)
		Me.chkApplyAlgo.Name = "chkApplyAlgo"
		Me.chkApplyAlgo.Size = New System.Drawing.Size(152, 17)
		Me.chkApplyAlgo.TabIndex = 0
		Me.chkApplyAlgo.Text = "Apply Smart Tree algorithm"
		Me.chkApplyAlgo.UseVisualStyleBackColor = True
		AddHandler Me.chkApplyAlgo.CheckedChanged, New System.EventHandler(AddressOf chkApplyAlgo_CheckedChanged)
		' 
		' tabPage2
		' 
		Me.tabPage2.Controls.Add(Me.chkUseAttributes)
		Me.tabPage2.Controls.Add(Me.chkFields)
		Me.tabPage2.Controls.Add(Me.tvFeatureClasses)
		Me.tabPage2.Location = New System.Drawing.Point(4, 22)
		Me.tabPage2.Margin = new System.Windows.Forms.Padding(2)
		Me.tabPage2.Name = "tabPage2"
		Me.tabPage2.Padding = New System.Windows.Forms.Padding(2)
		Me.tabPage2.Size = New System.Drawing.Size(336, 266)
		Me.tabPage2.TabIndex = 1
		Me.tabPage2.Text = "Attributes"
		Me.tabPage2.UseVisualStyleBackColor = True
		' 
		' chkUseAttributes
		' 
		Me.chkUseAttributes.AutoSize = True
		Me.chkUseAttributes.Location = New System.Drawing.Point(4, 5)
		Me.chkUseAttributes.Margin = new System.Windows.Forms.Padding(2)
		Me.chkUseAttributes.Name = "chkUseAttributes"
		Me.chkUseAttributes.Size = New System.Drawing.Size(157, 17)
		Me.chkUseAttributes.TabIndex = 2
		Me.chkUseAttributes.Text = "Create associated attributes"
		Me.chkUseAttributes.UseVisualStyleBackColor = True
		AddHandler Me.chkUseAttributes.CheckedChanged, New System.EventHandler(AddressOf chkUseAttributes_CheckedChanged)
		' 
		' chkFields
		' 
		Me.chkFields.CheckOnClick = True
		Me.chkFields.Enabled = False
		Me.chkFields.FormattingEnabled = True
		Me.chkFields.Location = New System.Drawing.Point(136, 26)
		Me.chkFields.Margin = new System.Windows.Forms.Padding(2)
		Me.chkFields.Name = "chkFields"
		Me.chkFields.Size = New System.Drawing.Size(198, 229)
		Me.chkFields.TabIndex = 1
		AddHandler Me.chkFields.SelectedIndexChanged, New System.EventHandler(AddressOf chkFields_SelectedIndexChanged)
		' 
		' tvFeatureClasses
		' 
		Me.tvFeatureClasses.Enabled = False
		Me.tvFeatureClasses.HideSelection = False
		Me.tvFeatureClasses.Location = New System.Drawing.Point(2, 26)
		Me.tvFeatureClasses.Margin = new System.Windows.Forms.Padding(2)
		Me.tvFeatureClasses.Name = "tvFeatureClasses"
		Me.tvFeatureClasses.Size = New System.Drawing.Size(130, 229)
		Me.tvFeatureClasses.TabIndex = 0
		AddHandler Me.tvFeatureClasses.AfterSelect, New System.Windows.Forms.TreeViewEventHandler(AddressOf tvFeatureClasses_AfterSelect)
		' 
		' btnDone
		' 
		Me.btnDone.Location = New System.Drawing.Point(294, 307)
		Me.btnDone.Margin = new System.Windows.Forms.Padding(2)
		Me.btnDone.Name = "btnDone"
		Me.btnDone.Size = New System.Drawing.Size(56, 19)
		Me.btnDone.TabIndex = 1
		Me.btnDone.Text = "Done"
		Me.btnDone.UseVisualStyleBackColor = True
		AddHandler Me.btnDone.Click, New System.EventHandler(AddressOf btnDone_Click)
		' 
		' frmAdvanced
		' 
		Me.AcceptButton = Me.btnDone
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0F, 13.0F)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(355, 333)
		Me.ControlBox = False
		Me.Controls.Add(Me.btnDone)
		Me.Controls.Add(Me.tabControl1)
		Me.Margin = new System.Windows.Forms.Padding(2)
		Me.Name = "frmAdvanced"
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
		Me.Text = "Advanced Options"
		Me.tabControl1.ResumeLayout(False)
		Me.tabPage1.ResumeLayout(False)
		Me.tabPage1.PerformLayout()
		Me.tabPage2.ResumeLayout(False)
		Me.tabPage2.PerformLayout()
		Me.ResumeLayout(False)

	End Sub

#End Region

	Private tabControl1 As System.Windows.Forms.TabControl
	Private tabPage1 As System.Windows.Forms.TabPage
	Private chkApplyAlgo As System.Windows.Forms.CheckBox
	Private tabPage2 As System.Windows.Forms.TabPage
	Private cboRoot As System.Windows.Forms.ComboBox
	Private label2 As System.Windows.Forms.Label
	Private label1 As System.Windows.Forms.Label
	Private cboDirection As System.Windows.Forms.ComboBox
	Private btnDone As System.Windows.Forms.Button
	Private chkUseAttributes As System.Windows.Forms.CheckBox
	Private chkFields As System.Windows.Forms.CheckedListBox
	Private tvFeatureClasses As System.Windows.Forms.TreeView

End Class