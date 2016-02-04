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
Imports System.Windows.Forms


	Partial Public Class frmLocationAllocationSolver
		Inherits Form
		''' <summary>
		''' Required designer variable.
		''' </summary>
		Private components As System.ComponentModel.IContainer = Nothing

		''' <summary>
		''' Clean up any resources being used.
		''' </summary>
		''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
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
        Me.cmdSolve = New System.Windows.Forms.Button()
        Me.txtFacilitiesToLocate = New System.Windows.Forms.TextBox()
        Me.lblNumFacilities = New System.Windows.Forms.Label()
        Me.txtCutOff = New System.Windows.Forms.TextBox()
        Me.lblCutOff = New System.Windows.Forms.Label()
        Me.cboProblemType = New System.Windows.Forms.ComboBox()
        Me.lblProblemType = New System.Windows.Forms.Label()
        Me.lstOutput = New System.Windows.Forms.ListBox()
        Me.cboCostAttribute = New System.Windows.Forms.ComboBox()
        Me.lblCostAttribute = New System.Windows.Forms.Label()
        Me.lblImpTransformation = New System.Windows.Forms.Label()
        Me.txtImpParameter = New System.Windows.Forms.TextBox()
        Me.lblImpParameter = New System.Windows.Forms.Label()
        Me.cboImpTransformation = New System.Windows.Forms.ComboBox()
        Me.txtTargetMarketShare = New System.Windows.Forms.TextBox()
        Me.lblTargetMarketShare = New System.Windows.Forms.Label()
        Me.tableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.txtDefaultCapacity = New System.Windows.Forms.TextBox()
        Me.lblDefaultCapacity = New System.Windows.Forms.Label()
        Me.axMapControl = New ESRI.ArcGIS.Controls.AxMapControl()
        Me.tableLayoutPanel1.SuspendLayout()
        CType(Me.axMapControl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdSolve
        '
        Me.cmdSolve.Location = New System.Drawing.Point(145, 212)
        Me.cmdSolve.Margin = New System.Windows.Forms.Padding(2)
        Me.cmdSolve.Name = "cmdSolve"
        Me.cmdSolve.Size = New System.Drawing.Size(145, 21)
        Me.cmdSolve.TabIndex = 0
        Me.cmdSolve.Text = "Solve"
        Me.cmdSolve.UseVisualStyleBackColor = True
        '
        'txtFacilitiesToLocate
        '
        Me.txtFacilitiesToLocate.Location = New System.Drawing.Point(145, 61)
        Me.txtFacilitiesToLocate.Margin = New System.Windows.Forms.Padding(2)
        Me.txtFacilitiesToLocate.Name = "txtFacilitiesToLocate"
        Me.txtFacilitiesToLocate.Size = New System.Drawing.Size(145, 20)
        Me.txtFacilitiesToLocate.TabIndex = 1
        '
        'lblNumFacilities
        '
        Me.lblNumFacilities.AutoSize = True
        Me.lblNumFacilities.Location = New System.Drawing.Point(11, 64)
        Me.lblNumFacilities.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblNumFacilities.Name = "lblNumFacilities"
        Me.lblNumFacilities.Size = New System.Drawing.Size(95, 13)
        Me.lblNumFacilities.TabIndex = 2
        Me.lblNumFacilities.Text = "Facilities To locate"
        '
        'txtCutOff
        '
        Me.txtCutOff.Location = New System.Drawing.Point(145, 85)
        Me.txtCutOff.Margin = New System.Windows.Forms.Padding(2)
        Me.txtCutOff.Name = "txtCutOff"
        Me.txtCutOff.Size = New System.Drawing.Size(145, 20)
        Me.txtCutOff.TabIndex = 3
        '
        'lblCutOff
        '
        Me.lblCutOff.AutoSize = True
        Me.lblCutOff.Location = New System.Drawing.Point(11, 88)
        Me.lblCutOff.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblCutOff.Name = "lblCutOff"
        Me.lblCutOff.Size = New System.Drawing.Size(90, 13)
        Me.lblCutOff.TabIndex = 4
        Me.lblCutOff.Text = "Impedance cutoff"
        '
        'cboProblemType
        '
        Me.cboProblemType.FormattingEnabled = True
        Me.cboProblemType.Location = New System.Drawing.Point(145, 36)
        Me.cboProblemType.Margin = New System.Windows.Forms.Padding(2)
        Me.cboProblemType.Name = "cboProblemType"
        Me.cboProblemType.Size = New System.Drawing.Size(145, 21)
        Me.cboProblemType.TabIndex = 5
        '
        'lblProblemType
        '
        Me.lblProblemType.AutoSize = True
        Me.lblProblemType.Location = New System.Drawing.Point(11, 39)
        Me.lblProblemType.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblProblemType.Name = "lblProblemType"
        Me.lblProblemType.Size = New System.Drawing.Size(68, 13)
        Me.lblProblemType.TabIndex = 6
        Me.lblProblemType.Text = "Problem type"
        '
        'lstOutput
        '
        Me.lstOutput.FormattingEnabled = True
        Me.lstOutput.Location = New System.Drawing.Point(11, 244)
        Me.lstOutput.Margin = New System.Windows.Forms.Padding(2)
        Me.lstOutput.Name = "lstOutput"
        Me.lstOutput.ScrollAlwaysVisible = True
        Me.lstOutput.Size = New System.Drawing.Size(278, 264)
        Me.lstOutput.TabIndex = 7
        '
        'cboCostAttribute
        '
        Me.cboCostAttribute.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCostAttribute.FormattingEnabled = True
        Me.cboCostAttribute.Location = New System.Drawing.Point(145, 11)
        Me.cboCostAttribute.Margin = New System.Windows.Forms.Padding(2)
        Me.cboCostAttribute.Name = "cboCostAttribute"
        Me.cboCostAttribute.Size = New System.Drawing.Size(145, 21)
        Me.cboCostAttribute.TabIndex = 8
        '
        'lblCostAttribute
        '
        Me.lblCostAttribute.AutoSize = True
        Me.lblCostAttribute.Location = New System.Drawing.Point(11, 14)
        Me.lblCostAttribute.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblCostAttribute.Name = "lblCostAttribute"
        Me.lblCostAttribute.Size = New System.Drawing.Size(69, 13)
        Me.lblCostAttribute.TabIndex = 9
        Me.lblCostAttribute.Text = "Cost attribute"
        '
        'lblImpTransformation
        '
        Me.lblImpTransformation.AutoSize = True
        Me.lblImpTransformation.Location = New System.Drawing.Point(11, 112)
        Me.lblImpTransformation.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblImpTransformation.Name = "lblImpTransformation"
        Me.lblImpTransformation.Size = New System.Drawing.Size(129, 13)
        Me.lblImpTransformation.TabIndex = 12
        Me.lblImpTransformation.Text = "Impedance transformation"
        '
        'txtImpParameter
        '
        Me.txtImpParameter.Location = New System.Drawing.Point(145, 134)
        Me.txtImpParameter.Margin = New System.Windows.Forms.Padding(2)
        Me.txtImpParameter.Name = "txtImpParameter"
        Me.txtImpParameter.Size = New System.Drawing.Size(145, 20)
        Me.txtImpParameter.TabIndex = 13
        '
        'lblImpParameter
        '
        Me.lblImpParameter.AutoSize = True
        Me.lblImpParameter.Location = New System.Drawing.Point(11, 137)
        Me.lblImpParameter.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblImpParameter.Name = "lblImpParameter"
        Me.lblImpParameter.Size = New System.Drawing.Size(110, 13)
        Me.lblImpParameter.TabIndex = 14
        Me.lblImpParameter.Text = "Impedance parameter"
        '
        'cboImpTransformation
        '
        Me.cboImpTransformation.FormattingEnabled = True
        Me.cboImpTransformation.Location = New System.Drawing.Point(145, 109)
        Me.cboImpTransformation.Margin = New System.Windows.Forms.Padding(2)
        Me.cboImpTransformation.Name = "cboImpTransformation"
        Me.cboImpTransformation.Size = New System.Drawing.Size(145, 21)
        Me.cboImpTransformation.TabIndex = 15
        '
        'txtTargetMarketShare
        '
        Me.txtTargetMarketShare.Location = New System.Drawing.Point(145, 158)
        Me.txtTargetMarketShare.Margin = New System.Windows.Forms.Padding(2)
        Me.txtTargetMarketShare.Name = "txtTargetMarketShare"
        Me.txtTargetMarketShare.Size = New System.Drawing.Size(145, 20)
        Me.txtTargetMarketShare.TabIndex = 16
        '
        'lblTargetMarketShare
        '
        Me.lblTargetMarketShare.AutoSize = True
        Me.lblTargetMarketShare.Location = New System.Drawing.Point(11, 161)
        Me.lblTargetMarketShare.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblTargetMarketShare.Name = "lblTargetMarketShare"
        Me.lblTargetMarketShare.Size = New System.Drawing.Size(102, 13)
        Me.lblTargetMarketShare.TabIndex = 17
        Me.lblTargetMarketShare.Text = "Target market share"
        '
        'tableLayoutPanel1
        '
        Me.tableLayoutPanel1.ColumnCount = 1
        Me.tableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tableLayoutPanel1.Controls.Add(Me.axMapControl, 0, 0)
        Me.tableLayoutPanel1.Location = New System.Drawing.Point(298, 11)
        Me.tableLayoutPanel1.Name = "tableLayoutPanel1"
        Me.tableLayoutPanel1.RowCount = 1
        Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tableLayoutPanel1.Size = New System.Drawing.Size(352, 495)
        Me.tableLayoutPanel1.TabIndex = 19
        '
        'txtDefaultCapacity
        '
        Me.txtDefaultCapacity.Location = New System.Drawing.Point(145, 181)
        Me.txtDefaultCapacity.Margin = New System.Windows.Forms.Padding(2)
        Me.txtDefaultCapacity.Name = "txtDefaultCapacity"
        Me.txtDefaultCapacity.Size = New System.Drawing.Size(145, 20)
        Me.txtDefaultCapacity.TabIndex = 20
        '
        'lblDefaultCapacity
        '
        Me.lblDefaultCapacity.AutoSize = True
        Me.lblDefaultCapacity.Location = New System.Drawing.Point(11, 184)
        Me.lblDefaultCapacity.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblDefaultCapacity.Name = "lblDefaultCapacity"
        Me.lblDefaultCapacity.Size = New System.Drawing.Size(84, 13)
        Me.lblDefaultCapacity.TabIndex = 21
        Me.lblDefaultCapacity.Text = "Default capacity"
        '
        'axMapControl
        '
        Me.axMapControl.Location = New System.Drawing.Point(2, 2)
        Me.axMapControl.Margin = New System.Windows.Forms.Padding(2)
        Me.axMapControl.Name = "axMapControl"
        Me.axMapControl.Size = New System.Drawing.Size(348, 491)
        Me.axMapControl.TabIndex = 15
        '
        'frmLocationAllocationSolver
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(662, 514)
        Me.Controls.Add(Me.lblDefaultCapacity)
        Me.Controls.Add(Me.txtDefaultCapacity)
        Me.Controls.Add(Me.tableLayoutPanel1)
        Me.Controls.Add(Me.lblTargetMarketShare)
        Me.Controls.Add(Me.txtTargetMarketShare)
        Me.Controls.Add(Me.cboImpTransformation)
        Me.Controls.Add(Me.lblImpParameter)
        Me.Controls.Add(Me.txtImpParameter)
        Me.Controls.Add(Me.lblImpTransformation)
        Me.Controls.Add(Me.lblCostAttribute)
        Me.Controls.Add(Me.cboCostAttribute)
        Me.Controls.Add(Me.lstOutput)
        Me.Controls.Add(Me.lblProblemType)
        Me.Controls.Add(Me.cboProblemType)
        Me.Controls.Add(Me.lblCutOff)
        Me.Controls.Add(Me.txtCutOff)
        Me.Controls.Add(Me.lblNumFacilities)
        Me.Controls.Add(Me.txtFacilitiesToLocate)
        Me.Controls.Add(Me.cmdSolve)
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.Name = "frmLocationAllocationSolver"
        Me.Text = "Location-Allocation Solver"
        Me.tableLayoutPanel1.ResumeLayout(False)
        CType(Me.axMapControl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

		Private WithEvents cmdSolve As Button
		Private txtFacilitiesToLocate As TextBox
		Private lblNumFacilities As Label
		Private txtCutOff As TextBox
		Private lblCutOff As Label
		Private WithEvents cboProblemType As ComboBox
		Private lblProblemType As Label
		Private lstOutput As ListBox
		Private cboCostAttribute As ComboBox
		Private lblCostAttribute As Label
		Private lblImpTransformation As Label
		Private txtImpParameter As TextBox
		Private lblImpParameter As Label
		Private cboImpTransformation As ComboBox
		Private txtTargetMarketShare As TextBox
		Private lblTargetMarketShare As Label
    Private WithEvents tableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Private WithEvents txtDefaultCapacity As System.Windows.Forms.TextBox
    Private WithEvents lblDefaultCapacity As System.Windows.Forms.Label
    Private WithEvents axMapControl As ESRI.ArcGIS.Controls.AxMapControl

	End Class
