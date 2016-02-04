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
Namespace ServiceAreaSolver
	Partial Public Class frmServiceAreaSolver
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
            Me.btnSolve = New System.Windows.Forms.Button()
            Me.ckbUseRestriction = New System.Windows.Forms.CheckBox()
            Me.cbCostAttribute = New System.Windows.Forms.ComboBox()
            Me.lbOutput = New System.Windows.Forms.ListBox()
            Me.label2 = New System.Windows.Forms.Label()
            Me.txtCutOff = New System.Windows.Forms.TextBox()
            Me.label3 = New System.Windows.Forms.Label()
            Me.btnLoadMap = New System.Windows.Forms.Button()
            Me.groupBox1 = New System.Windows.Forms.GroupBox()
            Me.txtFeatureDataset = New System.Windows.Forms.TextBox()
            Me.Label6 = New System.Windows.Forms.Label()
            Me.txtInputFacilities = New System.Windows.Forms.TextBox()
            Me.txtNetworkDataset = New System.Windows.Forms.TextBox()
            Me.label5 = New System.Windows.Forms.Label()
            Me.label4 = New System.Windows.Forms.Label()
            Me.label1 = New System.Windows.Forms.Label()
            Me.txtWorkspacePath = New System.Windows.Forms.TextBox()
            Me.gbServiceAreaSolver = New System.Windows.Forms.GroupBox()
            Me.tableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
            Me.axMapControl = New ESRI.ArcGIS.Controls.AxMapControl()
            Me.groupBox1.SuspendLayout()
            Me.gbServiceAreaSolver.SuspendLayout()
            Me.tableLayoutPanel1.SuspendLayout()
            CType(Me.axMapControl, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'btnSolve
            '
            Me.btnSolve.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.btnSolve.Location = New System.Drawing.Point(54, 243)
            Me.btnSolve.Name = "btnSolve"
            Me.btnSolve.Size = New System.Drawing.Size(89, 34)
            Me.btnSolve.TabIndex = 1
            Me.btnSolve.Text = "Solve"
            Me.btnSolve.UseVisualStyleBackColor = True
            '
            'ckbUseRestriction
            '
            Me.ckbUseRestriction.AutoSize = True
            Me.ckbUseRestriction.Location = New System.Drawing.Point(9, 81)
            Me.ckbUseRestriction.Name = "ckbUseRestriction"
            Me.ckbUseRestriction.Size = New System.Drawing.Size(98, 17)
            Me.ckbUseRestriction.TabIndex = 3
            Me.ckbUseRestriction.Text = "Use Restriction"
            Me.ckbUseRestriction.UseVisualStyleBackColor = True
            '
            'cbCostAttribute
            '
            Me.cbCostAttribute.FormattingEnabled = True
            Me.cbCostAttribute.Location = New System.Drawing.Point(78, 27)
            Me.cbCostAttribute.Name = "cbCostAttribute"
            Me.cbCostAttribute.Size = New System.Drawing.Size(121, 21)
            Me.cbCostAttribute.TabIndex = 4
            '
            'lbOutput
            '
            Me.lbOutput.FormattingEnabled = True
            Me.lbOutput.Location = New System.Drawing.Point(9, 108)
            Me.lbOutput.Name = "lbOutput"
            Me.lbOutput.Size = New System.Drawing.Size(189, 121)
            Me.lbOutput.TabIndex = 5
            '
            'label2
            '
            Me.label2.AutoSize = True
            Me.label2.Location = New System.Drawing.Point(6, 30)
            Me.label2.Name = "label2"
            Me.label2.Size = New System.Drawing.Size(70, 13)
            Me.label2.TabIndex = 8
            Me.label2.Text = "Cost Attribute"
            '
            'txtCutOff
            '
            Me.txtCutOff.Location = New System.Drawing.Point(78, 55)
            Me.txtCutOff.Name = "txtCutOff"
            Me.txtCutOff.Size = New System.Drawing.Size(120, 20)
            Me.txtCutOff.TabIndex = 9
            Me.txtCutOff.Text = "0"
            '
            'label3
            '
            Me.label3.AutoSize = True
            Me.label3.Location = New System.Drawing.Point(6, 58)
            Me.label3.Name = "label3"
            Me.label3.Size = New System.Drawing.Size(40, 13)
            Me.label3.TabIndex = 10
            Me.label3.Text = "Cut Off"
            '
            'btnLoadMap
            '
            Me.btnLoadMap.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.btnLoadMap.Location = New System.Drawing.Point(443, 38)
            Me.btnLoadMap.Name = "btnLoadMap"
            Me.btnLoadMap.Size = New System.Drawing.Size(94, 38)
            Me.btnLoadMap.TabIndex = 11
            Me.btnLoadMap.Text = "Setup Service Area Problem"
            Me.btnLoadMap.UseVisualStyleBackColor = True
            '
            'groupBox1
            '
            Me.groupBox1.Controls.Add(Me.txtFeatureDataset)
            Me.groupBox1.Controls.Add(Me.Label6)
            Me.groupBox1.Controls.Add(Me.txtInputFacilities)
            Me.groupBox1.Controls.Add(Me.btnLoadMap)
            Me.groupBox1.Controls.Add(Me.txtNetworkDataset)
            Me.groupBox1.Controls.Add(Me.label5)
            Me.groupBox1.Controls.Add(Me.label4)
            Me.groupBox1.Controls.Add(Me.label1)
            Me.groupBox1.Controls.Add(Me.txtWorkspacePath)
            Me.groupBox1.Location = New System.Drawing.Point(12, 12)
            Me.groupBox1.Name = "groupBox1"
            Me.groupBox1.Size = New System.Drawing.Size(555, 128)
            Me.groupBox1.TabIndex = 12
            Me.groupBox1.TabStop = False
            Me.groupBox1.Text = "Map Configuration"
            '
            'txtFeatureDataset
            '
            Me.txtFeatureDataset.Location = New System.Drawing.Point(106, 71)
            Me.txtFeatureDataset.Name = "txtFeatureDataset"
            Me.txtFeatureDataset.Size = New System.Drawing.Size(308, 20)
            Me.txtFeatureDataset.TabIndex = 15
            '
            'Label6
            '
            Me.Label6.AutoSize = True
            Me.Label6.Location = New System.Drawing.Point(6, 74)
            Me.Label6.Name = "Label6"
            Me.Label6.Size = New System.Drawing.Size(83, 13)
            Me.Label6.TabIndex = 14
            Me.Label6.Text = "Feature Dataset"
            '
            'txtInputFacilities
            '
            Me.txtInputFacilities.Location = New System.Drawing.Point(106, 97)
            Me.txtInputFacilities.Name = "txtInputFacilities"
            Me.txtInputFacilities.Size = New System.Drawing.Size(308, 20)
            Me.txtInputFacilities.TabIndex = 13
            '
            'txtNetworkDataset
            '
            Me.txtNetworkDataset.Location = New System.Drawing.Point(106, 45)
            Me.txtNetworkDataset.Name = "txtNetworkDataset"
            Me.txtNetworkDataset.Size = New System.Drawing.Size(308, 20)
            Me.txtNetworkDataset.TabIndex = 12
            '
            'label5
            '
            Me.label5.AutoSize = True
            Me.label5.Location = New System.Drawing.Point(6, 100)
            Me.label5.Name = "label5"
            Me.label5.Size = New System.Drawing.Size(74, 13)
            Me.label5.TabIndex = 11
            Me.label5.Text = "Input Facilities"
            '
            'label4
            '
            Me.label4.AutoSize = True
            Me.label4.Location = New System.Drawing.Point(6, 48)
            Me.label4.Name = "label4"
            Me.label4.Size = New System.Drawing.Size(87, 13)
            Me.label4.TabIndex = 10
            Me.label4.Text = "Network Dataset"
            '
            'label1
            '
            Me.label1.AutoSize = True
            Me.label1.Location = New System.Drawing.Point(6, 22)
            Me.label1.Name = "label1"
            Me.label1.Size = New System.Drawing.Size(87, 13)
            Me.label1.TabIndex = 9
            Me.label1.Text = "Workspace Path"
            '
            'txtWorkspacePath
            '
            Me.txtWorkspacePath.Location = New System.Drawing.Point(106, 19)
            Me.txtWorkspacePath.Name = "txtWorkspacePath"
            Me.txtWorkspacePath.Size = New System.Drawing.Size(308, 20)
            Me.txtWorkspacePath.TabIndex = 8
            '
            'gbServiceAreaSolver
            '
            Me.gbServiceAreaSolver.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.gbServiceAreaSolver.Controls.Add(Me.tableLayoutPanel1)
            Me.gbServiceAreaSolver.Controls.Add(Me.btnSolve)
            Me.gbServiceAreaSolver.Controls.Add(Me.label3)
            Me.gbServiceAreaSolver.Controls.Add(Me.label2)
            Me.gbServiceAreaSolver.Controls.Add(Me.txtCutOff)
            Me.gbServiceAreaSolver.Controls.Add(Me.ckbUseRestriction)
            Me.gbServiceAreaSolver.Controls.Add(Me.lbOutput)
            Me.gbServiceAreaSolver.Controls.Add(Me.cbCostAttribute)
            Me.gbServiceAreaSolver.Enabled = False
            Me.gbServiceAreaSolver.Location = New System.Drawing.Point(12, 143)
            Me.gbServiceAreaSolver.Name = "gbServiceAreaSolver"
            Me.gbServiceAreaSolver.Size = New System.Drawing.Size(555, 295)
            Me.gbServiceAreaSolver.TabIndex = 13
            Me.gbServiceAreaSolver.TabStop = False
            Me.gbServiceAreaSolver.Text = "Service Area Solver"
            '
            'tableLayoutPanel1
            '
            Me.tableLayoutPanel1.ColumnCount = 1
            Me.tableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
            Me.tableLayoutPanel1.Controls.Add(Me.axMapControl, 0, 0)
            Me.tableLayoutPanel1.Location = New System.Drawing.Point(205, 19)
            Me.tableLayoutPanel1.Name = "tableLayoutPanel1"
            Me.tableLayoutPanel1.RowCount = 1
            Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
            Me.tableLayoutPanel1.Size = New System.Drawing.Size(332, 258)
            Me.tableLayoutPanel1.TabIndex = 25
            '
            'axMapControl
            '
            Me.axMapControl.Location = New System.Drawing.Point(2, 2)
            Me.axMapControl.Margin = New System.Windows.Forms.Padding(2)
            Me.axMapControl.Name = "axMapControl"
            Me.axMapControl.Size = New System.Drawing.Size(328, 254)
            Me.axMapControl.TabIndex = 11
            '
            'frmServiceAreaSolver
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(576, 450)
            Me.Controls.Add(Me.gbServiceAreaSolver)
            Me.Controls.Add(Me.groupBox1)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
            Me.Name = "frmServiceAreaSolver"
            Me.Text = "ArcGIS Engine Network Analyst Service Area Solver"
            Me.groupBox1.ResumeLayout(False)
            Me.groupBox1.PerformLayout()
            Me.gbServiceAreaSolver.ResumeLayout(False)
            Me.gbServiceAreaSolver.PerformLayout()
            Me.tableLayoutPanel1.ResumeLayout(False)
            CType(Me.axMapControl, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub

		#End Region

		Private WithEvents btnSolve As System.Windows.Forms.Button
        Private ckbUseRestriction As System.Windows.Forms.CheckBox
		Public cbCostAttribute As System.Windows.Forms.ComboBox
		Private lbOutput As System.Windows.Forms.ListBox
		Private label2 As System.Windows.Forms.Label
		Private txtCutOff As System.Windows.Forms.TextBox
		Private label3 As System.Windows.Forms.Label
		Private WithEvents btnLoadMap As System.Windows.Forms.Button
		Private groupBox1 As System.Windows.Forms.GroupBox
		Private txtNetworkDataset As System.Windows.Forms.TextBox
		Private label5 As System.Windows.Forms.Label
		Private label4 As System.Windows.Forms.Label
		Private label1 As System.Windows.Forms.Label
		Private txtWorkspacePath As System.Windows.Forms.TextBox
		Private txtInputFacilities As System.Windows.Forms.TextBox
		Private gbServiceAreaSolver As System.Windows.Forms.GroupBox
		Private WithEvents txtFeatureDataset As System.Windows.Forms.TextBox
		Private WithEvents Label6 As System.Windows.Forms.Label
		Private WithEvents tableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
		Private WithEvents axMapControl As ESRI.ArcGIS.Controls.AxMapControl
	End Class
End Namespace

