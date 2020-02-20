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

	Partial Public Class frmODCostMatrixSolver
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
			Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmODCostMatrixSolver))
			Me.comboCostAttribute = New System.Windows.Forms.ComboBox
			Me.label1 = New System.Windows.Forms.Label
			Me.textTargetFacility = New System.Windows.Forms.TextBox
			Me.label2 = New System.Windows.Forms.Label
			Me.label3 = New System.Windows.Forms.Label
			Me.textCutoff = New System.Windows.Forms.TextBox
			Me.checkUseRestriction = New System.Windows.Forms.CheckBox
			Me.checkUseHierarchy = New System.Windows.Forms.CheckBox
			Me.cmdSolve = New System.Windows.Forms.Button
			Me.listOutput = New System.Windows.Forms.ListBox
			Me.tableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
			Me.axMapControl = New ESRI.ArcGIS.Controls.AxMapControl
			Me.tableLayoutPanel1.SuspendLayout()
			CType(Me.axMapControl, System.ComponentModel.ISupportInitialize).BeginInit()
			Me.SuspendLayout()
			'
			'comboCostAttribute
			'
			Me.comboCostAttribute.FormattingEnabled = True
			Me.comboCostAttribute.Location = New System.Drawing.Point(135, 17)
			Me.comboCostAttribute.Margin = New System.Windows.Forms.Padding(2)
			Me.comboCostAttribute.Name = "comboCostAttribute"
			Me.comboCostAttribute.Size = New System.Drawing.Size(90, 21)
			Me.comboCostAttribute.TabIndex = 0
			'
			'label1
			'
			Me.label1.AutoSize = True
			Me.label1.Location = New System.Drawing.Point(20, 23)
			Me.label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
			Me.label1.Name = "label1"
			Me.label1.Size = New System.Drawing.Size(70, 13)
			Me.label1.TabIndex = 1
			Me.label1.Text = "Cost Attribute"
			'
			'textTargetFacility
			'
			Me.textTargetFacility.Location = New System.Drawing.Point(135, 49)
			Me.textTargetFacility.Margin = New System.Windows.Forms.Padding(2)
			Me.textTargetFacility.Name = "textTargetFacility"
			Me.textTargetFacility.Size = New System.Drawing.Size(90, 20)
			Me.textTargetFacility.TabIndex = 2
			'
			'label2
			'
			Me.label2.AutoSize = True
			Me.label2.Location = New System.Drawing.Point(20, 52)
			Me.label2.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
			Me.label2.Name = "label2"
			Me.label2.Size = New System.Drawing.Size(104, 13)
			Me.label2.TabIndex = 3
			Me.label2.Text = "Target Facility Count"
			'
			'label3
			'
			Me.label3.AutoSize = True
			Me.label3.Location = New System.Drawing.Point(20, 85)
			Me.label3.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
			Me.label3.Name = "label3"
			Me.label3.Size = New System.Drawing.Size(35, 13)
			Me.label3.TabIndex = 4
			Me.label3.Text = "Cutoff"
			'
			'textCutoff
			'
			Me.textCutoff.Location = New System.Drawing.Point(135, 81)
			Me.textCutoff.Margin = New System.Windows.Forms.Padding(2)
			Me.textCutoff.Name = "textCutoff"
			Me.textCutoff.Size = New System.Drawing.Size(90, 20)
			Me.textCutoff.TabIndex = 5
			'
			'checkUseRestriction
			'
			Me.checkUseRestriction.AutoSize = True
			Me.checkUseRestriction.Checked = True
			Me.checkUseRestriction.CheckState = System.Windows.Forms.CheckState.Checked
			Me.checkUseRestriction.Location = New System.Drawing.Point(22, 124)
			Me.checkUseRestriction.Margin = New System.Windows.Forms.Padding(2)
			Me.checkUseRestriction.Name = "checkUseRestriction"
			Me.checkUseRestriction.Size = New System.Drawing.Size(140, 17)
			Me.checkUseRestriction.TabIndex = 6
			Me.checkUseRestriction.Text = "Use Oneway Restriction"
			Me.checkUseRestriction.UseVisualStyleBackColor = True
			'
			'checkUseHierarchy
			'
			Me.checkUseHierarchy.AutoSize = True
			Me.checkUseHierarchy.Location = New System.Drawing.Point(22, 153)
			Me.checkUseHierarchy.Margin = New System.Windows.Forms.Padding(2)
			Me.checkUseHierarchy.Name = "checkUseHierarchy"
			Me.checkUseHierarchy.Size = New System.Drawing.Size(93, 17)
			Me.checkUseHierarchy.TabIndex = 7
			Me.checkUseHierarchy.Text = "Use Hierarchy"
			Me.checkUseHierarchy.UseVisualStyleBackColor = True
			'
			'cmdSolve
			'
			Me.cmdSolve.Location = New System.Drawing.Point(58, 184)
			Me.cmdSolve.Margin = New System.Windows.Forms.Padding(2)
			Me.cmdSolve.Name = "cmdSolve"
			Me.cmdSolve.Size = New System.Drawing.Size(128, 24)
			Me.cmdSolve.TabIndex = 8
			Me.cmdSolve.Text = "Find OD Cost Matrix"
			Me.cmdSolve.UseVisualStyleBackColor = True
			'
			'listOutput
			'
			Me.listOutput.FormattingEnabled = True
			Me.listOutput.Location = New System.Drawing.Point(22, 223)
			Me.listOutput.Margin = New System.Windows.Forms.Padding(2)
			Me.listOutput.Name = "listOutput"
			Me.listOutput.Size = New System.Drawing.Size(202, 160)
			Me.listOutput.TabIndex = 9
			'
			'tableLayoutPanel1
			'
			Me.tableLayoutPanel1.ColumnCount = 1
			Me.tableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
			Me.tableLayoutPanel1.Controls.Add(Me.axMapControl, 0, 0)
			Me.tableLayoutPanel1.Location = New System.Drawing.Point(230, 18)
			Me.tableLayoutPanel1.Name = "tableLayoutPanel1"
			Me.tableLayoutPanel1.RowCount = 1
			Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
			Me.tableLayoutPanel1.Size = New System.Drawing.Size(307, 367)
			Me.tableLayoutPanel1.TabIndex = 24
			'
			'axMapControl
			'
			Me.axMapControl.Location = New System.Drawing.Point(2, 2)
			Me.axMapControl.Margin = New System.Windows.Forms.Padding(2)
			Me.axMapControl.Name = "axMapControl"
			Me.axMapControl.OcxState = CType(resources.GetObject("axMapControl.OcxState"), System.Windows.Forms.AxHost.State)
			Me.axMapControl.Size = New System.Drawing.Size(303, 363)
			Me.axMapControl.TabIndex = 11
			'
			'frmODCostMatrixSolver
			'
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.ClientSize = New System.Drawing.Size(546, 393)
			Me.Controls.Add(Me.tableLayoutPanel1)
			Me.Controls.Add(Me.listOutput)
			Me.Controls.Add(Me.cmdSolve)
			Me.Controls.Add(Me.checkUseHierarchy)
			Me.Controls.Add(Me.checkUseRestriction)
			Me.Controls.Add(Me.textCutoff)
			Me.Controls.Add(Me.label3)
			Me.Controls.Add(Me.label2)
			Me.Controls.Add(Me.textTargetFacility)
			Me.Controls.Add(Me.label1)
			Me.Controls.Add(Me.comboCostAttribute)
			Me.Margin = New System.Windows.Forms.Padding(2)
			Me.Name = "frmODCostMatrixSolver"
        Me.Text = "ArcGIS Network Analyst extension - OD Cost Matrix Demonstration"
			Me.tableLayoutPanel1.ResumeLayout(False)
			CType(Me.axMapControl, System.ComponentModel.ISupportInitialize).EndInit()
			Me.ResumeLayout(False)
			Me.PerformLayout()

		End Sub

		#End Region

		Private comboCostAttribute As System.Windows.Forms.ComboBox
		Private label1 As System.Windows.Forms.Label
		Private textTargetFacility As System.Windows.Forms.TextBox
		Private label2 As System.Windows.Forms.Label
		Private label3 As System.Windows.Forms.Label
		Private textCutoff As System.Windows.Forms.TextBox
		Private checkUseRestriction As System.Windows.Forms.CheckBox
		Private checkUseHierarchy As System.Windows.Forms.CheckBox
		Private WithEvents cmdSolve As System.Windows.Forms.Button
		Private listOutput As System.Windows.Forms.ListBox
		Private WithEvents tableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
		Private WithEvents axMapControl As ESRI.ArcGIS.Controls.AxMapControl
	End Class


