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
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PlayAnimation
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
				Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PlayAnimation))
				Me.lblInstructions2 = New System.Windows.Forms.Label
				Me.lblInstructions1 = New System.Windows.Forms.Label
				Me.lblInstructions = New System.Windows.Forms.Label
				Me.btnPlay = New System.Windows.Forms.Button
				Me.cboAnimations = New System.Windows.Forms.ComboBox
				Me.chkShowWindow = New System.Windows.Forms.CheckBox
				Me.btnLoad = New System.Windows.Forms.Button
				Me.AxArcReaderGlobeControl1 = New ESRI.ArcGIS.PublisherControls.AxArcReaderGlobeControl
				Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog
				CType(Me.AxArcReaderGlobeControl1, System.ComponentModel.ISupportInitialize).BeginInit()
				Me.SuspendLayout()
				'
				'lblInstructions2
				'
				Me.lblInstructions2.AutoSize = True
				Me.lblInstructions2.ForeColor = System.Drawing.SystemColors.Highlight
				Me.lblInstructions2.Location = New System.Drawing.Point(546, 86)
				Me.lblInstructions2.Name = "lblInstructions2"
				Me.lblInstructions2.Size = New System.Drawing.Size(193, 13)
				Me.lblInstructions2.TabIndex = 14
				Me.lblInstructions2.Text = "2) Use controls below to Play Animation"
				'
				'lblInstructions1
				'
				Me.lblInstructions1.AutoSize = True
				Me.lblInstructions1.ForeColor = System.Drawing.SystemColors.Highlight
				Me.lblInstructions1.Location = New System.Drawing.Point(546, 40)
				Me.lblInstructions1.Name = "lblInstructions1"
				Me.lblInstructions1.Size = New System.Drawing.Size(139, 13)
				Me.lblInstructions1.TabIndex = 13
				Me.lblInstructions1.Text = "    that contains animations.."
				'
				'lblInstructions
				'
				Me.lblInstructions.AutoSize = True
				Me.lblInstructions.ForeColor = System.Drawing.SystemColors.Highlight
				Me.lblInstructions.Location = New System.Drawing.Point(546, 18)
				Me.lblInstructions.Name = "lblInstructions"
				Me.lblInstructions.Size = New System.Drawing.Size(186, 13)
				Me.lblInstructions.TabIndex = 12
				Me.lblInstructions.Text = "1) Load PMF published from ArcGlobe"
				'
				'btnPlay
				'
				Me.btnPlay.Location = New System.Drawing.Point(697, 138)
				Me.btnPlay.Name = "btnPlay"
				Me.btnPlay.Size = New System.Drawing.Size(48, 29)
				Me.btnPlay.TabIndex = 11
				Me.btnPlay.Text = "Play"
				Me.btnPlay.UseVisualStyleBackColor = True
				'
				'cboAnimations
				'
				Me.cboAnimations.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
				Me.cboAnimations.FormattingEnabled = True
				Me.cboAnimations.Location = New System.Drawing.Point(549, 102)
				Me.cboAnimations.Name = "cboAnimations"
				Me.cboAnimations.Size = New System.Drawing.Size(195, 21)
				Me.cboAnimations.TabIndex = 10
				'
				'chkShowWindow
				'
				Me.chkShowWindow.AutoSize = True
				Me.chkShowWindow.Location = New System.Drawing.Point(549, 138)
				Me.chkShowWindow.Name = "chkShowWindow"
				Me.chkShowWindow.Size = New System.Drawing.Size(144, 17)
				Me.chkShowWindow.TabIndex = 9
				Me.chkShowWindow.Text = "Show Animation Window"
				Me.chkShowWindow.UseVisualStyleBackColor = True
				'
				'btnLoad
				'
				Me.btnLoad.Location = New System.Drawing.Point(693, 40)
				Me.btnLoad.Name = "btnLoad"
				Me.btnLoad.Size = New System.Drawing.Size(51, 26)
				Me.btnLoad.TabIndex = 8
				Me.btnLoad.Text = "Load"
				Me.btnLoad.UseVisualStyleBackColor = True
				'
				'AxArcReaderGlobeControl1
				'
				Me.AxArcReaderGlobeControl1.Location = New System.Drawing.Point(12, 12)
				Me.AxArcReaderGlobeControl1.Name = "AxArcReaderGlobeControl1"
				Me.AxArcReaderGlobeControl1.OcxState = CType(resources.GetObject("AxArcReaderGlobeControl1.OcxState"), System.Windows.Forms.AxHost.State)
				Me.AxArcReaderGlobeControl1.Size = New System.Drawing.Size(526, 388)
				Me.AxArcReaderGlobeControl1.TabIndex = 15
				'
				'OpenFileDialog1
				'
				Me.OpenFileDialog1.FileName = "OpenFileDialog1"
				'
				'PlayAnimation
				'
				Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
				Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
				Me.ClientSize = New System.Drawing.Size(755, 412)
				Me.Controls.Add(Me.AxArcReaderGlobeControl1)
				Me.Controls.Add(Me.lblInstructions2)
				Me.Controls.Add(Me.lblInstructions1)
				Me.Controls.Add(Me.lblInstructions)
				Me.Controls.Add(Me.btnPlay)
				Me.Controls.Add(Me.cboAnimations)
				Me.Controls.Add(Me.chkShowWindow)
				Me.Controls.Add(Me.btnLoad)
				Me.Name = "PlayAnimation"
				Me.Text = "PlayAnimation"
				CType(Me.AxArcReaderGlobeControl1, System.ComponentModel.ISupportInitialize).EndInit()
				Me.ResumeLayout(False)
				Me.PerformLayout()

		End Sub
		Private WithEvents lblInstructions2 As System.Windows.Forms.Label
		Private WithEvents lblInstructions1 As System.Windows.Forms.Label
		Private WithEvents lblInstructions As System.Windows.Forms.Label
		Private WithEvents btnPlay As System.Windows.Forms.Button
		Private WithEvents cboAnimations As System.Windows.Forms.ComboBox
		Private WithEvents chkShowWindow As System.Windows.Forms.CheckBox
		Private WithEvents btnLoad As System.Windows.Forms.Button
		Friend WithEvents AxArcReaderGlobeControl1 As ESRI.ArcGIS.PublisherControls.AxArcReaderGlobeControl
		Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog

End Class
