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
Namespace TAPurgeRuleCommand
	Public Partial Class PurgeRuleForm
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
			Me.cbTrackingLayers = New System.Windows.Forms.ComboBox()
			Me.label1 = New System.Windows.Forms.Label()
			Me.groupBox1 = New System.Windows.Forms.GroupBox()
			Me.label2 = New System.Windows.Forms.Label()
			Me.label3 = New System.Windows.Forms.Label()
			Me.cbPurgeRule = New System.Windows.Forms.ComboBox()
			Me.label4 = New System.Windows.Forms.Label()
			Me.checkAutoPurge = New System.Windows.Forms.CheckBox()
			Me.txtThreshold = New System.Windows.Forms.TextBox()
			Me.txtPurgePercent = New System.Windows.Forms.TextBox()
			Me.btnCancel = New System.Windows.Forms.Button()
			Me.btnApply = New System.Windows.Forms.Button()
			Me.groupBox1.SuspendLayout()
			Me.SuspendLayout()
			' 
			' cbTrackingLayers
			' 
			Me.cbTrackingLayers.Anchor = (CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
			Me.cbTrackingLayers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
			Me.cbTrackingLayers.FormattingEnabled = True
			Me.cbTrackingLayers.Location = New System.Drawing.Point(12, 29)
			Me.cbTrackingLayers.Name = "cbTrackingLayers"
			Me.cbTrackingLayers.Size = New System.Drawing.Size(182, 21)
			Me.cbTrackingLayers.TabIndex = 0
'			Me.cbTrackingLayers.SelectionChangeCommitted += New System.EventHandler(Me.cbTrackingLayers_SelectionChangeCommitted);
			' 
			' label1
			' 
			Me.label1.AutoSize = True
			Me.label1.Location = New System.Drawing.Point(13, 13)
			Me.label1.Name = "label1"
			Me.label1.Size = New System.Drawing.Size(81, 13)
			Me.label1.TabIndex = 1
			Me.label1.Text = "Tracking Layer:"
			' 
			' groupBox1
			' 
			Me.groupBox1.Anchor = (CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
			Me.groupBox1.Controls.Add(Me.txtPurgePercent)
			Me.groupBox1.Controls.Add(Me.txtThreshold)
			Me.groupBox1.Controls.Add(Me.checkAutoPurge)
			Me.groupBox1.Controls.Add(Me.label4)
			Me.groupBox1.Controls.Add(Me.cbPurgeRule)
			Me.groupBox1.Controls.Add(Me.label3)
			Me.groupBox1.Controls.Add(Me.label2)
			Me.groupBox1.Location = New System.Drawing.Point(12, 57)
			Me.groupBox1.Name = "groupBox1"
			Me.groupBox1.Size = New System.Drawing.Size(188, 128)
			Me.groupBox1.TabIndex = 2
			Me.groupBox1.TabStop = False
			Me.groupBox1.Text = "Purge Rule Settings"
			' 
			' label2
			' 
			Me.label2.AutoSize = True
			Me.label2.Location = New System.Drawing.Point(7, 43)
			Me.label2.Name = "label2"
			Me.label2.Size = New System.Drawing.Size(63, 13)
			Me.label2.TabIndex = 0
			Me.label2.Text = "Purge Rule:"
			' 
			' label3
			' 
			Me.label3.AutoSize = True
			Me.label3.Location = New System.Drawing.Point(7, 70)
			Me.label3.Name = "label3"
			Me.label3.Size = New System.Drawing.Size(57, 13)
			Me.label3.TabIndex = 1
			Me.label3.Text = "Threshold:"
			' 
			' cbPurgeRule
			' 
			Me.cbPurgeRule.Anchor = (CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
			Me.cbPurgeRule.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
			Me.cbPurgeRule.FormattingEnabled = True
			Me.cbPurgeRule.Items.AddRange(New Object() { "Oldest", "All Except Latest"})
			Me.cbPurgeRule.Location = New System.Drawing.Point(77, 40)
			Me.cbPurgeRule.Name = "cbPurgeRule"
			Me.cbPurgeRule.Size = New System.Drawing.Size(105, 21)
			Me.cbPurgeRule.TabIndex = 2
'			Me.cbPurgeRule.SelectedIndexChanged += New System.EventHandler(Me.cbPurgeRule_SelectedIndexChanged);
			' 
			' label4
			' 
			Me.label4.AutoSize = True
			Me.label4.Location = New System.Drawing.Point(7, 96)
			Me.label4.Name = "label4"
			Me.label4.Size = New System.Drawing.Size(90, 13)
			Me.label4.TabIndex = 3
			Me.label4.Text = "Percent to Purge:"
			' 
			' checkAutoPurge
			' 
			Me.checkAutoPurge.AutoSize = True
			Me.checkAutoPurge.Location = New System.Drawing.Point(10, 19)
			Me.checkAutoPurge.Name = "checkAutoPurge"
			Me.checkAutoPurge.Size = New System.Drawing.Size(79, 17)
			Me.checkAutoPurge.TabIndex = 4
			Me.checkAutoPurge.Text = "Auto Purge"
			Me.checkAutoPurge.UseVisualStyleBackColor = True
			' 
			' txtThreshold
			' 
			Me.txtThreshold.Anchor = (CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
			Me.txtThreshold.Location = New System.Drawing.Point(77, 67)
			Me.txtThreshold.Name = "txtThreshold"
			Me.txtThreshold.Size = New System.Drawing.Size(105, 20)
			Me.txtThreshold.TabIndex = 5
			Me.txtThreshold.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
			' 
			' txtPurgePercent
			' 
			Me.txtPurgePercent.Anchor = (CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
			Me.txtPurgePercent.Location = New System.Drawing.Point(103, 93)
			Me.txtPurgePercent.Name = "txtPurgePercent"
			Me.txtPurgePercent.Size = New System.Drawing.Size(79, 20)
			Me.txtPurgePercent.TabIndex = 6
			Me.txtPurgePercent.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
			' 
			' btnCancel
			' 
			Me.btnCancel.Location = New System.Drawing.Point(44, 191)
			Me.btnCancel.Name = "btnCancel"
			Me.btnCancel.Size = New System.Drawing.Size(75, 23)
			Me.btnCancel.TabIndex = 5
			Me.btnCancel.Text = "Cancel"
			Me.btnCancel.UseVisualStyleBackColor = True
'			Me.btnCancel.Click += New System.EventHandler(Me.btnCancel_Click);
			' 
			' btnApply
			' 
			Me.btnApply.Anchor = (CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
			Me.btnApply.Location = New System.Drawing.Point(125, 191)
			Me.btnApply.Name = "btnApply"
			Me.btnApply.Size = New System.Drawing.Size(75, 23)
			Me.btnApply.TabIndex = 6
			Me.btnApply.Text = "Apply"
			Me.btnApply.UseVisualStyleBackColor = True
'			Me.btnApply.Click += New System.EventHandler(Me.btnApply_Click);
			' 
			' PurgeRuleForm
			' 
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.ClientSize = New System.Drawing.Size(212, 221)
			Me.Controls.Add(Me.btnApply)
			Me.Controls.Add(Me.btnCancel)
			Me.Controls.Add(Me.groupBox1)
			Me.Controls.Add(Me.label1)
			Me.Controls.Add(Me.cbTrackingLayers)
			Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
			Me.Name = "PurgeRuleForm"
			Me.ShowInTaskbar = False
			Me.Text = "Purge Rule"
			Me.TopMost = True
'			Me.FormClosing += New System.Windows.Forms.FormClosingEventHandler(Me.PurgeRuleForm_FormClosing);
			Me.groupBox1.ResumeLayout(False)
			Me.groupBox1.PerformLayout()
			Me.ResumeLayout(False)
			Me.PerformLayout()

		End Sub

		#End Region

		Private WithEvents cbTrackingLayers As System.Windows.Forms.ComboBox
		Private label1 As System.Windows.Forms.Label
		Private groupBox1 As System.Windows.Forms.GroupBox
		Private label4 As System.Windows.Forms.Label
		Private WithEvents cbPurgeRule As System.Windows.Forms.ComboBox
		Private label3 As System.Windows.Forms.Label
		Private label2 As System.Windows.Forms.Label
		Private txtPurgePercent As System.Windows.Forms.TextBox
		Private txtThreshold As System.Windows.Forms.TextBox
		Private checkAutoPurge As System.Windows.Forms.CheckBox
		Private WithEvents btnCancel As System.Windows.Forms.Button
		Private WithEvents btnApply As System.Windows.Forms.Button
	End Class
End Namespace