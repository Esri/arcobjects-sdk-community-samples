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
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmClosestFacilitySolver
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.lstOutput = New System.Windows.Forms.ListBox()
        Me.cmdSolve = New System.Windows.Forms.Button()
        Me.txtTargetFacility = New System.Windows.Forms.TextBox()
        Me.txtCutOff = New System.Windows.Forms.TextBox()
        Me.cboCostAttribute = New System.Windows.Forms.ComboBox()
        Me.chkUseRestriction = New System.Windows.Forms.CheckBox()
        Me.chkUseHierarchy = New System.Windows.Forms.CheckBox()
        Me.lblCutOff = New System.Windows.Forms.Label()
        Me.lblNumFacility = New System.Windows.Forms.Label()
        Me.lblCostAttribute = New System.Windows.Forms.Label()
        Me.tableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.axMapControl = New ESRI.ArcGIS.Controls.AxMapControl()
        Me.tableLayoutPanel1.SuspendLayout()
        CType(Me.axMapControl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lstOutput
        '
        Me.lstOutput.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lstOutput.Location = New System.Drawing.Point(20, 229)
        Me.lstOutput.Name = "lstOutput"
        Me.lstOutput.Size = New System.Drawing.Size(300, 186)
        Me.lstOutput.TabIndex = 21
        '
        'cmdSolve
        '
        Me.cmdSolve.Location = New System.Drawing.Point(20, 194)
        Me.cmdSolve.Name = "cmdSolve"
        Me.cmdSolve.Size = New System.Drawing.Size(147, 21)
        Me.cmdSolve.TabIndex = 20
        Me.cmdSolve.Text = "Find Closest Facilities"
        '
        'txtTargetFacility
        '
        Me.txtTargetFacility.Location = New System.Drawing.Point(187, 55)
        Me.txtTargetFacility.Name = "txtTargetFacility"
        Me.txtTargetFacility.Size = New System.Drawing.Size(133, 20)
        Me.txtTargetFacility.TabIndex = 19
        '
        'txtCutOff
        '
        Me.txtCutOff.Location = New System.Drawing.Point(187, 90)
        Me.txtCutOff.Name = "txtCutOff"
        Me.txtCutOff.Size = New System.Drawing.Size(133, 20)
        Me.txtCutOff.TabIndex = 18
        '
        'cboCostAttribute
        '
        Me.cboCostAttribute.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCostAttribute.Location = New System.Drawing.Point(187, 28)
        Me.cboCostAttribute.Name = "cboCostAttribute"
        Me.cboCostAttribute.Size = New System.Drawing.Size(133, 21)
        Me.cboCostAttribute.TabIndex = 17
        '
        'chkUseRestriction
        '
        Me.chkUseRestriction.Location = New System.Drawing.Point(20, 132)
        Me.chkUseRestriction.Name = "chkUseRestriction"
        Me.chkUseRestriction.Size = New System.Drawing.Size(164, 21)
        Me.chkUseRestriction.TabIndex = 16
        Me.chkUseRestriction.Text = "Use Oneway Restriction"
        '
        'chkUseHierarchy
        '
        Me.chkUseHierarchy.Location = New System.Drawing.Point(20, 159)
        Me.chkUseHierarchy.Name = "chkUseHierarchy"
        Me.chkUseHierarchy.Size = New System.Drawing.Size(140, 21)
        Me.chkUseHierarchy.TabIndex = 15
        Me.chkUseHierarchy.Text = "Use Hierarchy"
        '
        'lblCutOff
        '
        Me.lblCutOff.Location = New System.Drawing.Point(20, 97)
        Me.lblCutOff.Name = "lblCutOff"
        Me.lblCutOff.Size = New System.Drawing.Size(133, 21)
        Me.lblCutOff.TabIndex = 14
        Me.lblCutOff.Text = "Cutoff"
        '
        'lblNumFacility
        '
        Me.lblNumFacility.Location = New System.Drawing.Point(20, 62)
        Me.lblNumFacility.Name = "lblNumFacility"
        Me.lblNumFacility.Size = New System.Drawing.Size(133, 20)
        Me.lblNumFacility.TabIndex = 13
        Me.lblNumFacility.Text = "Target Facility Count"
        '
        'lblCostAttribute
        '
        Me.lblCostAttribute.Location = New System.Drawing.Point(20, 28)
        Me.lblCostAttribute.Name = "lblCostAttribute"
        Me.lblCostAttribute.Size = New System.Drawing.Size(133, 21)
        Me.lblCostAttribute.TabIndex = 12
        Me.lblCostAttribute.Text = "Cost Attribute"
        '
        'tableLayoutPanel1
        '
        Me.tableLayoutPanel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tableLayoutPanel1.ColumnCount = 1
        Me.tableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tableLayoutPanel1.Controls.Add(Me.axMapControl, 0, 0)
        Me.tableLayoutPanel1.Location = New System.Drawing.Point(326, 28)
        Me.tableLayoutPanel1.Name = "tableLayoutPanel1"
        Me.tableLayoutPanel1.RowCount = 1
        Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tableLayoutPanel1.Size = New System.Drawing.Size(395, 387)
        Me.tableLayoutPanel1.TabIndex = 23
        '
        'axMapControl
        '
        Me.axMapControl.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.axMapControl.Location = New System.Drawing.Point(2, 2)
        Me.axMapControl.Margin = New System.Windows.Forms.Padding(2)
        Me.axMapControl.Name = "axMapControl"
        Me.axMapControl.Size = New System.Drawing.Size(391, 383)
        Me.axMapControl.TabIndex = 11
        '
        'frmClosestFacilitySolver
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(726, 422)
        Me.Controls.Add(Me.tableLayoutPanel1)
        Me.Controls.Add(Me.lstOutput)
        Me.Controls.Add(Me.cmdSolve)
        Me.Controls.Add(Me.txtTargetFacility)
        Me.Controls.Add(Me.txtCutOff)
        Me.Controls.Add(Me.cboCostAttribute)
        Me.Controls.Add(Me.chkUseRestriction)
        Me.Controls.Add(Me.chkUseHierarchy)
        Me.Controls.Add(Me.lblCutOff)
        Me.Controls.Add(Me.lblNumFacility)
        Me.Controls.Add(Me.lblCostAttribute)
        Me.Name = "frmClosestFacilitySolver"
        Me.Text = "ArcGIS Network Analyst extension - Closest Facility Demonstration"
        Me.tableLayoutPanel1.ResumeLayout(False)
        CType(Me.axMapControl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
	Friend WithEvents lstOutput As System.Windows.Forms.ListBox
    Friend WithEvents cmdSolve As System.Windows.Forms.Button
    Friend WithEvents txtTargetFacility As System.Windows.Forms.TextBox
    Friend WithEvents txtCutOff As System.Windows.Forms.TextBox
    Friend WithEvents cboCostAttribute As System.Windows.Forms.ComboBox
    Friend WithEvents chkUseRestriction As System.Windows.Forms.CheckBox
    Friend WithEvents chkUseHierarchy As System.Windows.Forms.CheckBox
    Friend WithEvents lblCutOff As System.Windows.Forms.Label
    Friend WithEvents lblNumFacility As System.Windows.Forms.Label
	Friend WithEvents lblCostAttribute As System.Windows.Forms.Label
	Private WithEvents tableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
	Private WithEvents axMapControl As ESRI.ArcGIS.Controls.AxMapControl

End Class
