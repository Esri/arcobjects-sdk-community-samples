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

Partial Public Class frmVRPSolver
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
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmVRPSolver))
		Me.checkUseRestriction = New System.Windows.Forms.CheckBox
		Me.checkUseHierarchy = New System.Windows.Forms.CheckBox
		Me.cmdSolve = New System.Windows.Forms.Button
		Me.listOutput = New System.Windows.Forms.ListBox
		Me.label2 = New System.Windows.Forms.Label
		Me.label3 = New System.Windows.Forms.Label
		Me.comboTimeAttribute = New System.Windows.Forms.ComboBox
		Me.comboDistanceAttribute = New System.Windows.Forms.ComboBox
		Me.label4 = New System.Windows.Forms.Label
		Me.label5 = New System.Windows.Forms.Label
		Me.comboTimeUnits = New System.Windows.Forms.ComboBox
		Me.comboDistUnits = New System.Windows.Forms.ComboBox
		Me.label1 = New System.Windows.Forms.Label
		Me.comboTWImportance = New System.Windows.Forms.ComboBox
		Me.tableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
		Me.axMapControl = New ESRI.ArcGIS.Controls.AxMapControl
		Me.tableLayoutPanel1.SuspendLayout()
		CType(Me.axMapControl, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		'
		'checkUseRestriction
		'
		Me.checkUseRestriction.AutoSize = True
		Me.checkUseRestriction.Location = New System.Drawing.Point(22, 261)
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
		Me.checkUseHierarchy.Location = New System.Drawing.Point(22, 297)
		Me.checkUseHierarchy.Margin = New System.Windows.Forms.Padding(2)
		Me.checkUseHierarchy.Name = "checkUseHierarchy"
		Me.checkUseHierarchy.Size = New System.Drawing.Size(93, 17)
		Me.checkUseHierarchy.TabIndex = 7
		Me.checkUseHierarchy.Text = "Use Hierarchy"
		Me.checkUseHierarchy.UseVisualStyleBackColor = True
		'
		'cmdSolve
		'
		Me.cmdSolve.Location = New System.Drawing.Point(74, 346)
		Me.cmdSolve.Margin = New System.Windows.Forms.Padding(2)
		Me.cmdSolve.Name = "cmdSolve"
		Me.cmdSolve.Size = New System.Drawing.Size(128, 24)
		Me.cmdSolve.TabIndex = 8
		Me.cmdSolve.Text = "Find VRP Solution"
		Me.cmdSolve.UseVisualStyleBackColor = True
		'
		'listOutput
		'
		Me.listOutput.FormattingEnabled = True
		Me.listOutput.Location = New System.Drawing.Point(22, 404)
		Me.listOutput.Margin = New System.Windows.Forms.Padding(2)
		Me.listOutput.Name = "listOutput"
		Me.listOutput.Size = New System.Drawing.Size(682, 108)
		Me.listOutput.TabIndex = 9
		'
		'label2
		'
		Me.label2.AutoSize = True
		Me.label2.Location = New System.Drawing.Point(20, 45)
		Me.label2.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
		Me.label2.Name = "label2"
		Me.label2.Size = New System.Drawing.Size(72, 13)
		Me.label2.TabIndex = 3
		Me.label2.Text = "Time Attribute"
		'
		'label3
		'
		Me.label3.AutoSize = True
		Me.label3.Location = New System.Drawing.Point(20, 84)
		Me.label3.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
		Me.label3.Name = "label3"
		Me.label3.Size = New System.Drawing.Size(91, 13)
		Me.label3.TabIndex = 4
		Me.label3.Text = "Distance Attribute"
		'
		'comboTimeAttribute
		'
		Me.comboTimeAttribute.FormattingEnabled = True
		Me.comboTimeAttribute.Location = New System.Drawing.Point(154, 45)
		Me.comboTimeAttribute.Margin = New System.Windows.Forms.Padding(2)
		Me.comboTimeAttribute.Name = "comboTimeAttribute"
		Me.comboTimeAttribute.Size = New System.Drawing.Size(90, 21)
		Me.comboTimeAttribute.TabIndex = 12
		'
		'comboDistanceAttribute
		'
		Me.comboDistanceAttribute.FormattingEnabled = True
		Me.comboDistanceAttribute.Location = New System.Drawing.Point(154, 84)
		Me.comboDistanceAttribute.Margin = New System.Windows.Forms.Padding(2)
		Me.comboDistanceAttribute.Name = "comboDistanceAttribute"
		Me.comboDistanceAttribute.Size = New System.Drawing.Size(92, 21)
		Me.comboDistanceAttribute.TabIndex = 13
		'
		'label4
		'
		Me.label4.AutoSize = True
		Me.label4.Location = New System.Drawing.Point(20, 124)
		Me.label4.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
		Me.label4.Name = "label4"
		Me.label4.Size = New System.Drawing.Size(82, 13)
		Me.label4.TabIndex = 16
		Me.label4.Text = "Time Field Units"
		'
		'label5
		'
		Me.label5.AutoSize = True
		Me.label5.Location = New System.Drawing.Point(20, 166)
		Me.label5.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
		Me.label5.Name = "label5"
		Me.label5.Size = New System.Drawing.Size(101, 13)
		Me.label5.TabIndex = 17
		Me.label5.Text = "Distance Field Units"
		'
		'comboTimeUnits
		'
		Me.comboTimeUnits.FormattingEnabled = True
		Me.comboTimeUnits.Location = New System.Drawing.Point(154, 124)
		Me.comboTimeUnits.Margin = New System.Windows.Forms.Padding(2)
		Me.comboTimeUnits.Name = "comboTimeUnits"
		Me.comboTimeUnits.Size = New System.Drawing.Size(92, 21)
		Me.comboTimeUnits.TabIndex = 18
		'
		'comboDistUnits
		'
		Me.comboDistUnits.FormattingEnabled = True
		Me.comboDistUnits.Location = New System.Drawing.Point(154, 160)
		Me.comboDistUnits.Margin = New System.Windows.Forms.Padding(2)
		Me.comboDistUnits.Name = "comboDistUnits"
		Me.comboDistUnits.Size = New System.Drawing.Size(92, 21)
		Me.comboDistUnits.TabIndex = 19
		'
		'label1
		'
		Me.label1.Location = New System.Drawing.Point(20, 202)
		Me.label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
		Me.label1.Name = "label1"
		Me.label1.Size = New System.Drawing.Size(116, 37)
		Me.label1.TabIndex = 22
		Me.label1.Text = "Time Window Violations Importance"
		'
		'comboTWImportance
		'
		Me.comboTWImportance.FormattingEnabled = True
		Me.comboTWImportance.Location = New System.Drawing.Point(154, 202)
		Me.comboTWImportance.Margin = New System.Windows.Forms.Padding(2)
		Me.comboTWImportance.Name = "comboTWImportance"
		Me.comboTWImportance.Size = New System.Drawing.Size(92, 21)
		Me.comboTWImportance.TabIndex = 23
		'
		'tableLayoutPanel1
		'
		Me.tableLayoutPanel1.ColumnCount = 1
		Me.tableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
		Me.tableLayoutPanel1.Controls.Add(Me.axMapControl, 0, 0)
		Me.tableLayoutPanel1.Location = New System.Drawing.Point(268, 45)
		Me.tableLayoutPanel1.Name = "tableLayoutPanel1"
		Me.tableLayoutPanel1.RowCount = 1
		Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
		Me.tableLayoutPanel1.Size = New System.Drawing.Size(392, 325)
		Me.tableLayoutPanel1.TabIndex = 26
		'
		'axMapControl
		'
		Me.axMapControl.Location = New System.Drawing.Point(2, 2)
		Me.axMapControl.Margin = New System.Windows.Forms.Padding(2)
		Me.axMapControl.Name = "axMapControl"
		Me.axMapControl.OcxState = CType(resources.GetObject("axMapControl.OcxState"), System.Windows.Forms.AxHost.State)
		Me.axMapControl.Size = New System.Drawing.Size(388, 321)
		Me.axMapControl.TabIndex = 11
		'
		'frmVRPSolver
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(734, 540)
		Me.Controls.Add(Me.tableLayoutPanel1)
		Me.Controls.Add(Me.comboTWImportance)
		Me.Controls.Add(Me.label1)
		Me.Controls.Add(Me.comboDistUnits)
		Me.Controls.Add(Me.comboTimeUnits)
		Me.Controls.Add(Me.label5)
		Me.Controls.Add(Me.label4)
		Me.Controls.Add(Me.comboDistanceAttribute)
		Me.Controls.Add(Me.comboTimeAttribute)
		Me.Controls.Add(Me.listOutput)
		Me.Controls.Add(Me.cmdSolve)
		Me.Controls.Add(Me.checkUseHierarchy)
		Me.Controls.Add(Me.checkUseRestriction)
		Me.Controls.Add(Me.label3)
		Me.Controls.Add(Me.label2)
		Me.Margin = New System.Windows.Forms.Padding(2)
		Me.Name = "frmVRPSolver"
        Me.Text = "ArcGIS Network Analyst extension - VRP Solver Demonstration"
		Me.tableLayoutPanel1.ResumeLayout(False)
		CType(Me.axMapControl, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub

#End Region

	Private checkUseRestriction As System.Windows.Forms.CheckBox
	Private checkUseHierarchy As System.Windows.Forms.CheckBox
	Private WithEvents cmdSolve As System.Windows.Forms.Button
	Private listOutput As System.Windows.Forms.ListBox
	Private label2 As System.Windows.Forms.Label
	Private label3 As System.Windows.Forms.Label
	Private comboTimeAttribute As System.Windows.Forms.ComboBox
	Private comboDistanceAttribute As System.Windows.Forms.ComboBox
	Private label4 As System.Windows.Forms.Label
	Private label5 As System.Windows.Forms.Label
	Private comboTimeUnits As System.Windows.Forms.ComboBox
	Private comboDistUnits As System.Windows.Forms.ComboBox
	Private label1 As System.Windows.Forms.Label
	Private comboTWImportance As System.Windows.Forms.ComboBox
	Private WithEvents tableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
	Private WithEvents axMapControl As ESRI.ArcGIS.Controls.AxMapControl
End Class


