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

  Public Partial Class MainForm
	''' <summary>
	''' Required designer variable.
	''' </summary>
	Private components As System.ComponentModel.IContainer = Nothing

	''' <summary>
	''' Clean up any resources being used.
	''' </summary>
	''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
	Protected Overrides Sub Dispose(ByVal disposing As Boolean)
	  'Ensures that any ESRI libraries that have been used are unloaded in the correct order. 
	  'Failure to do this may result in random crashes on exit due to the operating system unloading 
	  'the libraries in the incorrect order. 
	  ESRI.ArcGIS.ADF.COMSupport.AOUninitialize.Shutdown()

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
	  Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
	  Me.menuStrip1 = New System.Windows.Forms.MenuStrip()
	  Me.menuFile = New System.Windows.Forms.ToolStripMenuItem()
	  Me.menuNewDoc = New System.Windows.Forms.ToolStripMenuItem()
	  Me.menuOpenDoc = New System.Windows.Forms.ToolStripMenuItem()
	  Me.menuSaveDoc = New System.Windows.Forms.ToolStripMenuItem()
	  Me.menuSaveAs = New System.Windows.Forms.ToolStripMenuItem()
	  Me.menuSeparator = New System.Windows.Forms.ToolStripSeparator()
	  Me.menuExitApp = New System.Windows.Forms.ToolStripMenuItem()
	  Me.axMapControl1 = New ESRI.ArcGIS.Controls.AxMapControl()
	  Me.axToolbarControl1 = New ESRI.ArcGIS.Controls.AxToolbarControl()
	  Me.axTOCControl1 = New ESRI.ArcGIS.Controls.AxTOCControl()
	  Me.axLicenseControl1 = New ESRI.ArcGIS.Controls.AxLicenseControl()
	  Me.splitter1 = New System.Windows.Forms.Splitter()
	  Me.statusStrip1 = New System.Windows.Forms.StatusStrip()
	  Me.statusBarXY = New System.Windows.Forms.ToolStripStatusLabel()
	  Me.menuStrip1.SuspendLayout()
	  CType(Me.axMapControl1, System.ComponentModel.ISupportInitialize).BeginInit()
	  CType(Me.axToolbarControl1, System.ComponentModel.ISupportInitialize).BeginInit()
	  CType(Me.axTOCControl1, System.ComponentModel.ISupportInitialize).BeginInit()
	  CType(Me.axLicenseControl1, System.ComponentModel.ISupportInitialize).BeginInit()
	  Me.statusStrip1.SuspendLayout()
	  Me.SuspendLayout()
	  ' 
	  ' menuStrip1
	  ' 
	  Me.menuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() { Me.menuFile})
	  Me.menuStrip1.Location = New System.Drawing.Point(0, 0)
	  Me.menuStrip1.Name = "menuStrip1"
	  Me.menuStrip1.Size = New System.Drawing.Size(859, 24)
	  Me.menuStrip1.TabIndex = 0
	  Me.menuStrip1.Text = "menuStrip1"
	  ' 
	  ' menuFile
	  ' 
	  Me.menuFile.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() { Me.menuNewDoc, Me.menuOpenDoc, Me.menuSaveDoc, Me.menuSaveAs, Me.menuSeparator, Me.menuExitApp})
	  Me.menuFile.Name = "menuFile"
	  Me.menuFile.Size = New System.Drawing.Size(35, 20)
	  Me.menuFile.Text = "File"
	  ' 
	  ' menuNewDoc
	  ' 
	  Me.menuNewDoc.Image = (CType(resources.GetObject("menuNewDoc.Image"), System.Drawing.Image))
	  Me.menuNewDoc.ImageTransparentColor = System.Drawing.Color.White
	  Me.menuNewDoc.Name = "menuNewDoc"
	  Me.menuNewDoc.Size = New System.Drawing.Size(163, 22)
	  Me.menuNewDoc.Text = "New Document"
'	  Me.menuNewDoc.Click += New System.EventHandler(Me.menuNewDoc_Click);
	  ' 
	  ' menuOpenDoc
	  ' 
	  Me.menuOpenDoc.Image = (CType(resources.GetObject("menuOpenDoc.Image"), System.Drawing.Image))
	  Me.menuOpenDoc.ImageTransparentColor = System.Drawing.Color.White
	  Me.menuOpenDoc.Name = "menuOpenDoc"
	  Me.menuOpenDoc.Size = New System.Drawing.Size(163, 22)
	  Me.menuOpenDoc.Text = "Open Document..."
'	  Me.menuOpenDoc.Click += New System.EventHandler(Me.menuOpenDoc_Click);
	  ' 
	  ' menuSaveDoc
	  ' 
	  Me.menuSaveDoc.Image = (CType(resources.GetObject("menuSaveDoc.Image"), System.Drawing.Image))
	  Me.menuSaveDoc.ImageTransparentColor = System.Drawing.Color.White
	  Me.menuSaveDoc.Name = "menuSaveDoc"
	  Me.menuSaveDoc.Size = New System.Drawing.Size(163, 22)
	  Me.menuSaveDoc.Text = "SaveDocument"
'	  Me.menuSaveDoc.Click += New System.EventHandler(Me.menuSaveDoc_Click);
	  ' 
	  ' menuSaveAs
	  ' 
	  Me.menuSaveAs.Name = "menuSaveAs"
	  Me.menuSaveAs.Size = New System.Drawing.Size(163, 22)
	  Me.menuSaveAs.Text = "Save As..."
'	  Me.menuSaveAs.Click += New System.EventHandler(Me.menuSaveAs_Click);
	  ' 
	  ' menuSeparator
	  ' 
	  Me.menuSeparator.Name = "menuSeparator"
	  Me.menuSeparator.Size = New System.Drawing.Size(160, 6)
	  ' 
	  ' menuExitApp
	  ' 
	  Me.menuExitApp.Name = "menuExitApp"
	  Me.menuExitApp.Size = New System.Drawing.Size(163, 22)
	  Me.menuExitApp.Text = "Exit"
'	  Me.menuExitApp.Click += New System.EventHandler(Me.menuExitApp_Click);
	  ' 
	  ' axMapControl1
	  ' 
	  Me.axMapControl1.Dock = System.Windows.Forms.DockStyle.Fill
	  Me.axMapControl1.Location = New System.Drawing.Point(191, 52)
	  Me.axMapControl1.Name = "axMapControl1"
	  Me.axMapControl1.OcxState = (CType(resources.GetObject("axMapControl1.OcxState"), System.Windows.Forms.AxHost.State))
	  Me.axMapControl1.Size = New System.Drawing.Size(668, 512)
	  Me.axMapControl1.TabIndex = 2
'	  Me.axMapControl1.OnMouseMove += New ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMouseMoveEventHandler(Me.axMapControl1_OnMouseMove);
'	  Me.axMapControl1.OnMapReplaced += New ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMapReplacedEventHandler(Me.axMapControl1_OnMapReplaced);
	  ' 
	  ' axToolbarControl1
	  ' 
	  Me.axToolbarControl1.Dock = System.Windows.Forms.DockStyle.Top
	  Me.axToolbarControl1.Location = New System.Drawing.Point(0, 24)
	  Me.axToolbarControl1.Name = "axToolbarControl1"
	  Me.axToolbarControl1.OcxState = (CType(resources.GetObject("axToolbarControl1.OcxState"), System.Windows.Forms.AxHost.State))
	  Me.axToolbarControl1.Size = New System.Drawing.Size(859, 28)
	  Me.axToolbarControl1.TabIndex = 3
	  ' 
	  ' axTOCControl1
	  ' 
	  Me.axTOCControl1.Dock = System.Windows.Forms.DockStyle.Left
	  Me.axTOCControl1.Location = New System.Drawing.Point(3, 52)
	  Me.axTOCControl1.Name = "axTOCControl1"
	  Me.axTOCControl1.OcxState = (CType(resources.GetObject("axTOCControl1.OcxState"), System.Windows.Forms.AxHost.State))
	  Me.axTOCControl1.Size = New System.Drawing.Size(188, 512)
	  Me.axTOCControl1.TabIndex = 4
	  ' 
	  ' axLicenseControl1
	  ' 
	  Me.axLicenseControl1.Enabled = True
	  Me.axLicenseControl1.Location = New System.Drawing.Point(466, 278)
	  Me.axLicenseControl1.Name = "axLicenseControl1"
	  Me.axLicenseControl1.OcxState = (CType(resources.GetObject("axLicenseControl1.OcxState"), System.Windows.Forms.AxHost.State))
	  Me.axLicenseControl1.Size = New System.Drawing.Size(32, 32)
	  Me.axLicenseControl1.TabIndex = 5
	  ' 
	  ' splitter1
	  ' 
	  Me.splitter1.Location = New System.Drawing.Point(0, 52)
	  Me.splitter1.Name = "splitter1"
	  Me.splitter1.Size = New System.Drawing.Size(3, 534)
	  Me.splitter1.TabIndex = 6
	  Me.splitter1.TabStop = False
	  ' 
	  ' statusStrip1
	  ' 
	  Me.statusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() { Me.statusBarXY})
	  Me.statusStrip1.Location = New System.Drawing.Point(3, 564)
	  Me.statusStrip1.Name = "statusStrip1"
	  Me.statusStrip1.Size = New System.Drawing.Size(856, 22)
	  Me.statusStrip1.Stretch = False
	  Me.statusStrip1.TabIndex = 7
	  Me.statusStrip1.Text = "statusBar1"
	  ' 
	  ' statusBarXY
	  ' 
	  Me.statusBarXY.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter
	  Me.statusBarXY.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
	  Me.statusBarXY.Name = "statusBarXY"
	  Me.statusBarXY.Size = New System.Drawing.Size(49, 17)
	  Me.statusBarXY.Text = "Test 123"
	  ' 
	  ' MainForm
	  ' 
	  Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
	  Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
	  Me.ClientSize = New System.Drawing.Size(859, 586)
	  Me.Controls.Add(Me.axLicenseControl1)
	  Me.Controls.Add(Me.axMapControl1)
	  Me.Controls.Add(Me.axTOCControl1)
	  Me.Controls.Add(Me.statusStrip1)
	  Me.Controls.Add(Me.splitter1)
	  Me.Controls.Add(Me.axToolbarControl1)
	  Me.Controls.Add(Me.menuStrip1)
	  Me.Icon = (CType(resources.GetObject("$this.Icon"), System.Drawing.Icon))
	  Me.MainMenuStrip = Me.menuStrip1
	  Me.Name = "MainForm"
	  Me.Text = "ArcEngine Controls Application"
'	  Me.Load += New System.EventHandler(Me.MainForm_Load);
	  Me.menuStrip1.ResumeLayout(False)
	  Me.menuStrip1.PerformLayout()
	  CType(Me.axMapControl1, System.ComponentModel.ISupportInitialize).EndInit()
	  CType(Me.axToolbarControl1, System.ComponentModel.ISupportInitialize).EndInit()
	  CType(Me.axTOCControl1, System.ComponentModel.ISupportInitialize).EndInit()
	  CType(Me.axLicenseControl1, System.ComponentModel.ISupportInitialize).EndInit()
	  Me.statusStrip1.ResumeLayout(False)
	  Me.statusStrip1.PerformLayout()
	  Me.ResumeLayout(False)
	  Me.PerformLayout()

	End Sub

	#End Region

	Private menuStrip1 As System.Windows.Forms.MenuStrip
	Private menuFile As System.Windows.Forms.ToolStripMenuItem
	Private WithEvents menuNewDoc As System.Windows.Forms.ToolStripMenuItem
	Private WithEvents menuOpenDoc As System.Windows.Forms.ToolStripMenuItem
	Private WithEvents menuSaveDoc As System.Windows.Forms.ToolStripMenuItem
	Private WithEvents menuSaveAs As System.Windows.Forms.ToolStripMenuItem
	Private WithEvents menuExitApp As System.Windows.Forms.ToolStripMenuItem
	Private menuSeparator As System.Windows.Forms.ToolStripSeparator
	Private WithEvents axMapControl1 As ESRI.ArcGIS.Controls.AxMapControl
	Private axToolbarControl1 As ESRI.ArcGIS.Controls.AxToolbarControl
	Private axTOCControl1 As ESRI.ArcGIS.Controls.AxTOCControl
	Private axLicenseControl1 As ESRI.ArcGIS.Controls.AxLicenseControl
	Private splitter1 As System.Windows.Forms.Splitter
	Private statusStrip1 As System.Windows.Forms.StatusStrip
	Private statusBarXY As System.Windows.Forms.ToolStripStatusLabel
  End Class


