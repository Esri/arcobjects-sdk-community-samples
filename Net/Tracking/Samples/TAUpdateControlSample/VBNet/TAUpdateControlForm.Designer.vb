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
Namespace TAUpdateControlSample
	Public Partial Class TAUpdateControlForm
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
			Me.checkManualUpdate = New System.Windows.Forms.CheckBox()
			Me.checkAutoRefresh = New System.Windows.Forms.CheckBox()
			Me.groupBox1 = New System.Windows.Forms.GroupBox()
			Me.groupBox2 = New System.Windows.Forms.GroupBox()
			Me.label1 = New System.Windows.Forms.Label()
			Me.label2 = New System.Windows.Forms.Label()
			Me.txtUpdateRate = New System.Windows.Forms.TextBox()
			Me.txtRefreshRate = New System.Windows.Forms.TextBox()
			Me.label3 = New System.Windows.Forms.Label()
			Me.label4 = New System.Windows.Forms.Label()
			Me.groupBox3 = New System.Windows.Forms.GroupBox()
			Me.btnStats = New System.Windows.Forms.Button()
			Me.txtStatistics = New System.Windows.Forms.TextBox()
			Me.groupBox4 = New System.Windows.Forms.GroupBox()
			Me.cbRefreshType = New System.Windows.Forms.ComboBox()
			Me.btnRefresh = New System.Windows.Forms.Button()
			Me.cbUpdateMethod = New System.Windows.Forms.ComboBox()
			Me.txtUpdateValue = New System.Windows.Forms.TextBox()
			Me.btnApply = New System.Windows.Forms.Button()
			Me.label5 = New System.Windows.Forms.Label()
			Me.label6 = New System.Windows.Forms.Label()
			Me.label7 = New System.Windows.Forms.Label()
			Me.btnHelp = New System.Windows.Forms.Button()
			Me.groupBox1.SuspendLayout()
			Me.groupBox2.SuspendLayout()
			Me.groupBox3.SuspendLayout()
			Me.groupBox4.SuspendLayout()
			Me.SuspendLayout()
			' 
			' checkManualUpdate
			' 
			Me.checkManualUpdate.AutoSize = True
			Me.checkManualUpdate.Location = New System.Drawing.Point(10, 19)
			Me.checkManualUpdate.Name = "checkManualUpdate"
			Me.checkManualUpdate.Size = New System.Drawing.Size(61, 17)
			Me.checkManualUpdate.TabIndex = 0
			Me.checkManualUpdate.Text = "Manual"
			Me.checkManualUpdate.UseVisualStyleBackColor = True
'			Me.checkManualUpdate.CheckedChanged += New System.EventHandler(Me.checkManualUpdate_CheckedChanged);
			' 
			' checkAutoRefresh
			' 
			Me.checkAutoRefresh.AutoSize = True
			Me.checkAutoRefresh.Location = New System.Drawing.Point(10, 22)
			Me.checkAutoRefresh.Name = "checkAutoRefresh"
			Me.checkAutoRefresh.Size = New System.Drawing.Size(73, 17)
			Me.checkAutoRefresh.TabIndex = 1
			Me.checkAutoRefresh.Text = "Automatic"
			Me.checkAutoRefresh.UseVisualStyleBackColor = True
'			Me.checkAutoRefresh.CheckedChanged += New System.EventHandler(Me.checkAutoRefresh_CheckedChanged);
			' 
			' groupBox1
			' 
			Me.groupBox1.Controls.Add(Me.label6)
			Me.groupBox1.Controls.Add(Me.label5)
			Me.groupBox1.Controls.Add(Me.txtUpdateValue)
			Me.groupBox1.Controls.Add(Me.cbUpdateMethod)
			Me.groupBox1.Controls.Add(Me.label3)
			Me.groupBox1.Controls.Add(Me.txtUpdateRate)
			Me.groupBox1.Controls.Add(Me.label1)
			Me.groupBox1.Controls.Add(Me.checkManualUpdate)
			Me.groupBox1.Location = New System.Drawing.Point(13, 13)
			Me.groupBox1.Name = "groupBox1"
			Me.groupBox1.Size = New System.Drawing.Size(308, 80)
			Me.groupBox1.TabIndex = 2
			Me.groupBox1.TabStop = False
			Me.groupBox1.Text = "Update Settings"
			' 
			' groupBox2
			' 
			Me.groupBox2.Controls.Add(Me.label4)
			Me.groupBox2.Controls.Add(Me.txtRefreshRate)
			Me.groupBox2.Controls.Add(Me.label2)
			Me.groupBox2.Controls.Add(Me.checkAutoRefresh)
			Me.groupBox2.Location = New System.Drawing.Point(13, 106)
			Me.groupBox2.Name = "groupBox2"
			Me.groupBox2.Size = New System.Drawing.Size(151, 80)
			Me.groupBox2.TabIndex = 3
			Me.groupBox2.TabStop = False
			Me.groupBox2.Text = "Refresh Settings"
			' 
			' label1
			' 
			Me.label1.AutoSize = True
			Me.label1.Location = New System.Drawing.Point(7, 48)
			Me.label1.Name = "label1"
			Me.label1.Size = New System.Drawing.Size(33, 13)
			Me.label1.TabIndex = 1
			Me.label1.Text = "Rate:"
			' 
			' label2
			' 
			Me.label2.AutoSize = True
			Me.label2.Location = New System.Drawing.Point(7, 52)
			Me.label2.Name = "label2"
			Me.label2.Size = New System.Drawing.Size(33, 13)
			Me.label2.TabIndex = 2
			Me.label2.Text = "Rate:"
			' 
			' txtUpdateRate
			' 
			Me.txtUpdateRate.Location = New System.Drawing.Point(46, 44)
			Me.txtUpdateRate.Name = "txtUpdateRate"
			Me.txtUpdateRate.Size = New System.Drawing.Size(68, 20)
			Me.txtUpdateRate.TabIndex = 2
			Me.txtUpdateRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
			' 
			' txtRefreshRate
			' 
			Me.txtRefreshRate.Location = New System.Drawing.Point(46, 48)
			Me.txtRefreshRate.Name = "txtRefreshRate"
			Me.txtRefreshRate.Size = New System.Drawing.Size(68, 20)
			Me.txtRefreshRate.TabIndex = 3
			Me.txtRefreshRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
			' 
			' label3
			' 
			Me.label3.AutoSize = True
			Me.label3.Location = New System.Drawing.Point(120, 48)
			Me.label3.Name = "label3"
			Me.label3.Size = New System.Drawing.Size(24, 13)
			Me.label3.TabIndex = 3
			Me.label3.Text = "sec"
			' 
			' label4
			' 
			Me.label4.AutoSize = True
			Me.label4.Location = New System.Drawing.Point(120, 52)
			Me.label4.Name = "label4"
			Me.label4.Size = New System.Drawing.Size(24, 13)
			Me.label4.TabIndex = 4
			Me.label4.Text = "sec"
			' 
			' groupBox3
			' 
			Me.groupBox3.Controls.Add(Me.txtStatistics)
			Me.groupBox3.Controls.Add(Me.btnStats)
			Me.groupBox3.Location = New System.Drawing.Point(13, 192)
			Me.groupBox3.Name = "groupBox3"
			Me.groupBox3.Size = New System.Drawing.Size(308, 199)
			Me.groupBox3.TabIndex = 4
			Me.groupBox3.TabStop = False
			Me.groupBox3.Text = "Statistics"
			' 
			' btnStats
			' 
			Me.btnStats.Location = New System.Drawing.Point(10, 20)
			Me.btnStats.Name = "btnStats"
			Me.btnStats.Size = New System.Drawing.Size(75, 23)
			Me.btnStats.TabIndex = 0
            Me.btnStats.Text = "Retrieve"
			Me.btnStats.UseVisualStyleBackColor = True
'			Me.btnStats.Click += New System.EventHandler(Me.btnStats_Click);
			' 
			' txtStatistics
			' 
			Me.txtStatistics.Location = New System.Drawing.Point(10, 49)
			Me.txtStatistics.Multiline = True
			Me.txtStatistics.Name = "txtStatistics"
			Me.txtStatistics.ScrollBars = System.Windows.Forms.ScrollBars.Both
			Me.txtStatistics.Size = New System.Drawing.Size(288, 144)
			Me.txtStatistics.TabIndex = 1
			' 
			' groupBox4
			' 
			Me.groupBox4.Controls.Add(Me.label7)
			Me.groupBox4.Controls.Add(Me.btnRefresh)
			Me.groupBox4.Controls.Add(Me.cbRefreshType)
			Me.groupBox4.Location = New System.Drawing.Point(170, 106)
			Me.groupBox4.Name = "groupBox4"
			Me.groupBox4.Size = New System.Drawing.Size(151, 80)
			Me.groupBox4.TabIndex = 5
			Me.groupBox4.TabStop = False
			Me.groupBox4.Text = "Display Refresh"
			' 
			' cbRefreshType
			' 
			Me.cbRefreshType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
			Me.cbRefreshType.FormattingEnabled = True
			Me.cbRefreshType.Items.AddRange(New Object() { "Short Update", "Quick Update", "Full Update", "Asynchronous Update", "Full Screen Redraw"})
			Me.cbRefreshType.Location = New System.Drawing.Point(58, 20)
			Me.cbRefreshType.Name = "cbRefreshType"
			Me.cbRefreshType.Size = New System.Drawing.Size(83, 21)
			Me.cbRefreshType.TabIndex = 0
			' 
			' btnRefresh
			' 
			Me.btnRefresh.Location = New System.Drawing.Point(66, 46)
			Me.btnRefresh.Name = "btnRefresh"
			Me.btnRefresh.Size = New System.Drawing.Size(75, 23)
			Me.btnRefresh.TabIndex = 1
			Me.btnRefresh.Text = "Refresh"
			Me.btnRefresh.UseVisualStyleBackColor = True
'			Me.btnRefresh.Click += New System.EventHandler(Me.btnRefresh_Click);
			' 
			' cbUpdateMethod
			' 
			Me.cbUpdateMethod.FormattingEnabled = True
			Me.cbUpdateMethod.Items.AddRange(New Object() { "Event-based", "CPU Usage-based"})
			Me.cbUpdateMethod.Location = New System.Drawing.Point(202, 17)
			Me.cbUpdateMethod.Name = "cbUpdateMethod"
			Me.cbUpdateMethod.Size = New System.Drawing.Size(96, 21)
			Me.cbUpdateMethod.TabIndex = 4
			' 
			' txtUpdateValue
			' 
			Me.txtUpdateValue.Location = New System.Drawing.Point(202, 44)
			Me.txtUpdateValue.Name = "txtUpdateValue"
			Me.txtUpdateValue.Size = New System.Drawing.Size(96, 20)
			Me.txtUpdateValue.TabIndex = 5
			' 
			' btnApply
			' 
			Me.btnApply.Location = New System.Drawing.Point(243, 397)
			Me.btnApply.Name = "btnApply"
			Me.btnApply.Size = New System.Drawing.Size(75, 23)
			Me.btnApply.TabIndex = 6
			Me.btnApply.Text = "Apply"
			Me.btnApply.UseVisualStyleBackColor = True
'			Me.btnApply.Click += New System.EventHandler(Me.btnApply_Click);
			' 
			' label5
			' 
			Me.label5.AutoSize = True
			Me.label5.Location = New System.Drawing.Point(150, 20)
			Me.label5.Name = "label5"
			Me.label5.Size = New System.Drawing.Size(46, 13)
			Me.label5.TabIndex = 6
			Me.label5.Text = "Method:"
			' 
			' label6
			' 
			Me.label6.AutoSize = True
			Me.label6.Location = New System.Drawing.Point(150, 48)
			Me.label6.Name = "label6"
			Me.label6.Size = New System.Drawing.Size(37, 13)
			Me.label6.TabIndex = 7
			Me.label6.Text = "Value:"
			' 
			' label7
			' 
			Me.label7.AutoSize = True
			Me.label7.Location = New System.Drawing.Point(6, 22)
			Me.label7.Name = "label7"
			Me.label7.Size = New System.Drawing.Size(46, 13)
			Me.label7.TabIndex = 8
			Me.label7.Text = "Method:"
			' 
			' btnHelp
			' 
			Me.btnHelp.Location = New System.Drawing.Point(162, 397)
			Me.btnHelp.Name = "btnHelp"
			Me.btnHelp.Size = New System.Drawing.Size(75, 23)
			Me.btnHelp.TabIndex = 7
			Me.btnHelp.Text = "Help"
			Me.btnHelp.UseVisualStyleBackColor = True
'			Me.btnHelp.Click += New System.EventHandler(Me.btnHelp_Click);
			' 
			' TAUpdateControlForm
			' 
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.ClientSize = New System.Drawing.Size(330, 428)
			Me.Controls.Add(Me.btnHelp)
			Me.Controls.Add(Me.btnApply)
			Me.Controls.Add(Me.groupBox4)
			Me.Controls.Add(Me.groupBox3)
			Me.Controls.Add(Me.groupBox2)
			Me.Controls.Add(Me.groupBox1)
			Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
			Me.Name = "TAUpdateControlForm"
			Me.ShowInTaskbar = False
			Me.Text = "TAUpdateControl Settings"
			Me.TopMost = True
'			Me.FormClosing += New System.Windows.Forms.FormClosingEventHandler(Me.TAUpdateControlForm_FormClosing);
'			Me.Load += New System.EventHandler(Me.TAUpdateControlForm_Load);
			Me.groupBox1.ResumeLayout(False)
			Me.groupBox1.PerformLayout()
			Me.groupBox2.ResumeLayout(False)
			Me.groupBox2.PerformLayout()
			Me.groupBox3.ResumeLayout(False)
			Me.groupBox3.PerformLayout()
			Me.groupBox4.ResumeLayout(False)
			Me.groupBox4.PerformLayout()
			Me.ResumeLayout(False)

		End Sub

		#End Region

		Private WithEvents checkManualUpdate As System.Windows.Forms.CheckBox
		Private WithEvents checkAutoRefresh As System.Windows.Forms.CheckBox
		Private groupBox1 As System.Windows.Forms.GroupBox
		Private groupBox2 As System.Windows.Forms.GroupBox
		Private label1 As System.Windows.Forms.Label
		Private label2 As System.Windows.Forms.Label
		Private txtUpdateRate As System.Windows.Forms.TextBox
		Private label3 As System.Windows.Forms.Label
		Private label4 As System.Windows.Forms.Label
		Private txtRefreshRate As System.Windows.Forms.TextBox
		Private groupBox3 As System.Windows.Forms.GroupBox
		Private txtStatistics As System.Windows.Forms.TextBox
		Private WithEvents btnStats As System.Windows.Forms.Button
		Private groupBox4 As System.Windows.Forms.GroupBox
		Private cbRefreshType As System.Windows.Forms.ComboBox
		Private WithEvents btnRefresh As System.Windows.Forms.Button
		Private cbUpdateMethod As System.Windows.Forms.ComboBox
		Private txtUpdateValue As System.Windows.Forms.TextBox
		Private WithEvents btnApply As System.Windows.Forms.Button
		Private label6 As System.Windows.Forms.Label
		Private label5 As System.Windows.Forms.Label
		Private label7 As System.Windows.Forms.Label
		Private WithEvents btnHelp As System.Windows.Forms.Button

	End Class
End Namespace