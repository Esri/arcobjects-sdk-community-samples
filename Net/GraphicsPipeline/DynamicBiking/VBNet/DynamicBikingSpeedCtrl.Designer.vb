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

	Public Partial Class DynamicBikingSpeedCtrl
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

		#Region "Component Designer generated code"

		''' <summary> 
		''' Required method for Designer support - do not modify 
		''' the contents of this method with the code editor.
		''' </summary>
		Private Sub InitializeComponent()
			Me.components = New System.ComponentModel.Container()
			Me.trackBar1 = New System.Windows.Forms.TrackBar()
			Me.lblMin = New System.Windows.Forms.Label()
			Me.lblMaximum = New System.Windows.Forms.Label()
			Me.lblSpeed = New System.Windows.Forms.Label()
			Me.toolTip1 = New System.Windows.Forms.ToolTip(Me.components)
			CType(Me.trackBar1, System.ComponentModel.ISupportInitialize).BeginInit()
			Me.SuspendLayout()
			' 
			' trackBar1
			' 
			Me.trackBar1.Location = New System.Drawing.Point(110, 4)
			Me.trackBar1.Maximum = 20
			Me.trackBar1.Minimum = 1
			Me.trackBar1.Name = "trackBar1"
			Me.trackBar1.Size = New System.Drawing.Size(145, 45)
			Me.trackBar1.TabIndex = 0
			Me.trackBar1.TickFrequency = 2
			Me.toolTip1.SetToolTip(Me.trackBar1, "Playback speed")
			Me.trackBar1.Value = 10
'			Me.trackBar1.ValueChanged += New System.EventHandler(Me.trackBar1_ValueChanged);
			' 
			' lblMin
			' 
			Me.lblMin.AutoSize = True
			Me.lblMin.Location = New System.Drawing.Point(95, 14)
			Me.lblMin.Name = "lblMin"
			Me.lblMin.Size = New System.Drawing.Size(20, 13)
			Me.lblMin.TabIndex = 1
			Me.lblMin.Text = "X1"
			' 
			' lblMaximum
			' 
			Me.lblMaximum.AutoSize = True
			Me.lblMaximum.Location = New System.Drawing.Point(250, 14)
			Me.lblMaximum.Name = "lblMaximum"
			Me.lblMaximum.Size = New System.Drawing.Size(26, 13)
			Me.lblMaximum.TabIndex = 2
			Me.lblMaximum.Text = "X20"
			' 
			' lblSpeed
			' 
			Me.lblSpeed.AutoSize = True
			Me.lblSpeed.Location = New System.Drawing.Point(2, 13)
			Me.lblSpeed.Name = "lblSpeed"
			Me.lblSpeed.Size = New System.Drawing.Size(85, 13)
			Me.lblSpeed.TabIndex = 3
			Me.lblSpeed.Text = "playback speed:"
			' 
			' toolTip1
			' 
			Me.toolTip1.AutoPopDelay = 500
			Me.toolTip1.InitialDelay = 500
			Me.toolTip1.ReshowDelay = 100
			Me.toolTip1.ToolTipTitle = "Playback speed"
			' 
			' DynamicBikingSpeedCtrl
			' 
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.Controls.Add(Me.lblSpeed)
			Me.Controls.Add(Me.lblMaximum)
			Me.Controls.Add(Me.lblMin)
			Me.Controls.Add(Me.trackBar1)
			Me.Name = "DynamicBikingSpeedCtrl"
			Me.Size = New System.Drawing.Size(280, 41)
			CType(Me.trackBar1, System.ComponentModel.ISupportInitialize).EndInit()
			Me.ResumeLayout(False)
			Me.PerformLayout()

		End Sub

		#End Region

		Private WithEvents trackBar1 As System.Windows.Forms.TrackBar
		Private lblMin As System.Windows.Forms.Label
		Private lblMaximum As System.Windows.Forms.Label
		Private lblSpeed As System.Windows.Forms.Label
		Private toolTip1 As System.Windows.Forms.ToolTip
	End Class
