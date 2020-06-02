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
Namespace GlobeGraphicsToolbar
	Partial Public Class StyleGalleryForm
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
			Dim resources As New System.ComponentModel.ComponentResourceManager(GetType(StyleGalleryForm))
			Me.axSymbologyControl1 = New ESRI.ArcGIS.Controls.AxSymbologyControl()
			Me.axLicenseControl1 = New ESRI.ArcGIS.Controls.AxLicenseControl()
			Me.button1 = New System.Windows.Forms.Button()
			CType(Me.axSymbologyControl1, System.ComponentModel.ISupportInitialize).BeginInit()
			CType(Me.axLicenseControl1, System.ComponentModel.ISupportInitialize).BeginInit()
			Me.SuspendLayout()
			' 
			' axSymbologyControl1
			' 
			Me.axSymbologyControl1.Location = New System.Drawing.Point(12, 1)
			Me.axSymbologyControl1.Name = "axSymbologyControl1"
			Me.axSymbologyControl1.OcxState = (CType(resources.GetObject("axSymbologyControl1.OcxState"), System.Windows.Forms.AxHost.State))
			Me.axSymbologyControl1.Size = New System.Drawing.Size(265, 265)
			Me.axSymbologyControl1.TabIndex = 0
'			Me.axSymbologyControl1.OnItemSelected += New ESRI.ArcGIS.Controls.ISymbologyControlEvents_Ax_OnItemSelectedEventHandler(Me.axSymbologyControl1_OnItemSelected);
			' 
			' axLicenseControl1
			' 
			Me.axLicenseControl1.Enabled = True
			Me.axLicenseControl1.Location = New System.Drawing.Point(262, 289)
			Me.axLicenseControl1.Name = "axLicenseControl1"
			Me.axLicenseControl1.OcxState = (CType(resources.GetObject("axLicenseControl1.OcxState"), System.Windows.Forms.AxHost.State))
			Me.axLicenseControl1.Size = New System.Drawing.Size(32, 32)
			Me.axLicenseControl1.TabIndex = 1
			' 
			' button1
			' 
			Me.button1.Location = New System.Drawing.Point(92, 289)
			Me.button1.Name = "button1"
			Me.button1.Size = New System.Drawing.Size(75, 23)
			Me.button1.TabIndex = 2
			Me.button1.Text = "OK"
			Me.button1.UseVisualStyleBackColor = True
'			Me.button1.Click += New System.EventHandler(Me.button1_Click);
			' 
			' StyleGalleryForm
			' 
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.ClientSize = New System.Drawing.Size(285, 329)
			Me.Controls.Add(Me.button1)
			Me.Controls.Add(Me.axLicenseControl1)
			Me.Controls.Add(Me.axSymbologyControl1)
			Me.Name = "StyleGalleryForm"
			Me.Text = "StyleGalleryForm"
			CType(Me.axSymbologyControl1, System.ComponentModel.ISupportInitialize).EndInit()
			CType(Me.axLicenseControl1, System.ComponentModel.ISupportInitialize).EndInit()
			Me.ResumeLayout(False)

		End Sub

		#End Region

		Private WithEvents axSymbologyControl1 As ESRI.ArcGIS.Controls.AxSymbologyControl
		Private axLicenseControl1 As ESRI.ArcGIS.Controls.AxLicenseControl
		Private WithEvents button1 As System.Windows.Forms.Button
	End Class
End Namespace