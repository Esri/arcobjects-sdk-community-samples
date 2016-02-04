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
Partial Class MainForm
	Inherits System.Windows.Forms.Form
	''' <summary>
	''' Required designer variable.
	''' </summary>
	Private components As System.ComponentModel.IContainer = Nothing

	''' <summary>
	''' Clean up any resources being used.
	''' </summary>
	''' <param name="disposing">true if managed resources should be disposed otherwise, false.</param>
	<System.Diagnostics.DebuggerNonUserCode()> _
	Protected Overrides Sub Dispose(ByVal disposing As Boolean)

		'Ensures that any ESRI libraries that have been used are unloaded in the correct order. 
		'Failure to do this may result in random crashes on exit due to the operating system unloading 
		'the libraries in the incorrect order. 
		ESRI.ArcGIS.ADF.COMSupport.AOUninitialize.Shutdown()

		If disposing AndAlso components IsNot Nothing Then
			components.Dispose()
		End If
		MyBase.Dispose(disposing)
	End Sub

#Region "Windows Form Designer generated code"

	''' <summary>
	''' Required method for Designer support - do not modify
	''' the contents of me method with the code editor.
	''' </summary>
	<System.Diagnostics.DebuggerStepThrough()> _
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
		Me.axToolbarControl1 = New ESRI.ArcGIS.Controls.AxToolbarControl()
		Me.axLicenseControl1 = New ESRI.ArcGIS.Controls.AxLicenseControl()
		Me.statusStrip1 = New System.Windows.Forms.StatusStrip()
		Me.statusBarXY = New System.Windows.Forms.ToolStripStatusLabel()
		Me.splitContainer1 = New System.Windows.Forms.SplitContainer()
		Me.cboFrame = New System.Windows.Forms.ComboBox()
		Me.axTOCControl1 = New ESRI.ArcGIS.Controls.AxTOCControl()
		Me.axMapControl1 = New ESRI.ArcGIS.Controls.AxMapControl()
		Me.panel1 = New System.Windows.Forms.Panel()
		Me.axToolbarControl2 = New ESRI.ArcGIS.Controls.AxToolbarControl()
		Me.menuStrip1.SuspendLayout()
		CType(Me.axToolbarControl1, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.axLicenseControl1, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.statusStrip1.SuspendLayout()
		Me.splitContainer1.Panel1.SuspendLayout()
		Me.splitContainer1.Panel2.SuspendLayout()
		Me.splitContainer1.SuspendLayout()
		CType(Me.axTOCControl1, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.axMapControl1, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.panel1.SuspendLayout()
		CType(Me.axToolbarControl2, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		'
		'menuStrip1
		'
		Me.menuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.menuFile})
		Me.menuStrip1.Location = New System.Drawing.Point(0, 0)
		Me.menuStrip1.Name = "menuStrip1"
		Me.menuStrip1.Size = New System.Drawing.Size(1026, 24)
		Me.menuStrip1.TabIndex = 0
		Me.menuStrip1.Text = "menuStrip1"
		'
		'menuFile
		'
		Me.menuFile.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.menuNewDoc, Me.menuOpenDoc, Me.menuSaveDoc, Me.menuSaveAs, Me.menuSeparator, Me.menuExitApp})
		Me.menuFile.Name = "menuFile"
		Me.menuFile.Size = New System.Drawing.Size(37, 20)
		Me.menuFile.Text = "File"
		'
		'menuNewDoc
		'
		Me.menuNewDoc.Image = CType(resources.GetObject("menuNewDoc.Image"), System.Drawing.Image)
		Me.menuNewDoc.ImageTransparentColor = System.Drawing.Color.White
		Me.menuNewDoc.Name = "menuNewDoc"
		Me.menuNewDoc.Size = New System.Drawing.Size(171, 22)
		Me.menuNewDoc.Text = "New Document"
		'
		'menuOpenDoc
		'
		Me.menuOpenDoc.Image = CType(resources.GetObject("menuOpenDoc.Image"), System.Drawing.Image)
		Me.menuOpenDoc.ImageTransparentColor = System.Drawing.Color.White
		Me.menuOpenDoc.Name = "menuOpenDoc"
		Me.menuOpenDoc.Size = New System.Drawing.Size(171, 22)
		Me.menuOpenDoc.Text = "Open Document..."
		'
		'menuSaveDoc
		'
		Me.menuSaveDoc.Image = CType(resources.GetObject("menuSaveDoc.Image"), System.Drawing.Image)
		Me.menuSaveDoc.ImageTransparentColor = System.Drawing.Color.White
		Me.menuSaveDoc.Name = "menuSaveDoc"
		Me.menuSaveDoc.Size = New System.Drawing.Size(171, 22)
		Me.menuSaveDoc.Text = "SaveDocument"
		'
		'menuSaveAs
		'
		Me.menuSaveAs.Name = "menuSaveAs"
		Me.menuSaveAs.Size = New System.Drawing.Size(171, 22)
		Me.menuSaveAs.Text = "Save As..."
		'
		'menuSeparator
		'
		Me.menuSeparator.Name = "menuSeparator"
		Me.menuSeparator.Size = New System.Drawing.Size(168, 6)
		'
		'menuExitApp
		'
		Me.menuExitApp.Name = "menuExitApp"
		Me.menuExitApp.Size = New System.Drawing.Size(171, 22)
		Me.menuExitApp.Text = "Exit"
		'
		'axToolbarControl1
		'
		Me.axToolbarControl1.Dock = System.Windows.Forms.DockStyle.Top
		Me.axToolbarControl1.Location = New System.Drawing.Point(0, 24)
		Me.axToolbarControl1.Name = "axToolbarControl1"
		Me.axToolbarControl1.OcxState = CType(resources.GetObject("axToolbarControl1.OcxState"), System.Windows.Forms.AxHost.State)
		Me.axToolbarControl1.Size = New System.Drawing.Size(1026, 28)
		Me.axToolbarControl1.TabIndex = 3
		'
		'axLicenseControl1
		'
		Me.axLicenseControl1.Enabled = True
		Me.axLicenseControl1.Location = New System.Drawing.Point(466, 278)
		Me.axLicenseControl1.Name = "axLicenseControl1"
		Me.axLicenseControl1.OcxState = CType(resources.GetObject("axLicenseControl1.OcxState"), System.Windows.Forms.AxHost.State)
		Me.axLicenseControl1.Size = New System.Drawing.Size(32, 32)
		Me.axLicenseControl1.TabIndex = 5
		'
		'statusStrip1
		'
		Me.statusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.statusBarXY})
		Me.statusStrip1.Location = New System.Drawing.Point(0, 564)
		Me.statusStrip1.Name = "statusStrip1"
		Me.statusStrip1.Size = New System.Drawing.Size(1026, 22)
		Me.statusStrip1.Stretch = False
		Me.statusStrip1.TabIndex = 7
		Me.statusStrip1.Text = "statusBar1"
		'
		'statusBarXY
		'
		Me.statusBarXY.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter
		Me.statusBarXY.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
		Me.statusBarXY.Name = "statusBarXY"
		Me.statusBarXY.Size = New System.Drawing.Size(308, 17)
		Me.statusBarXY.Text = "Load a map document or a diagram into the MapControl"
		'
		'splitContainer1
		'
		Me.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.splitContainer1.Location = New System.Drawing.Point(0, 87)
		Me.splitContainer1.Name = "splitContainer1"
		'
		'splitContainer1.Panel1
		'
		Me.splitContainer1.Panel1.Controls.Add(Me.cboFrame)
		Me.splitContainer1.Panel1.Controls.Add(Me.axTOCControl1)
		'
		'splitContainer1.Panel2
		'
		Me.splitContainer1.Panel2.Controls.Add(Me.axMapControl1)
		Me.splitContainer1.Size = New System.Drawing.Size(1026, 477)
		Me.splitContainer1.SplitterDistance = 218
		Me.splitContainer1.TabIndex = 8
		'
		'cboFrame
		'
		Me.cboFrame.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			  Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.cboFrame.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cboFrame.FormattingEnabled = True
		Me.cboFrame.Location = New System.Drawing.Point(4, 7)
		Me.cboFrame.Name = "cboFrame"
		Me.cboFrame.Size = New System.Drawing.Size(211, 21)
		Me.cboFrame.TabIndex = 6
		'
		'axTOCControl1
		'
		Me.axTOCControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
			  Or System.Windows.Forms.AnchorStyles.Left) _
			  Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.axTOCControl1.Location = New System.Drawing.Point(0, 34)
		Me.axTOCControl1.Name = "axTOCControl1"
		Me.axTOCControl1.OcxState = CType(resources.GetObject("axTOCControl1.OcxState"), System.Windows.Forms.AxHost.State)
		Me.axTOCControl1.Size = New System.Drawing.Size(218, 443)
		Me.axTOCControl1.TabIndex = 5
		'
		'axMapControl1
		'
		Me.axMapControl1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.axMapControl1.Location = New System.Drawing.Point(0, 0)
		Me.axMapControl1.Name = "axMapControl1"
		Me.axMapControl1.OcxState = CType(resources.GetObject("axMapControl1.OcxState"), System.Windows.Forms.AxHost.State)
		Me.axMapControl1.Size = New System.Drawing.Size(804, 477)
		Me.axMapControl1.TabIndex = 3
		'
		'panel1
		'
		Me.panel1.Controls.Add(Me.axToolbarControl2)
		Me.panel1.Dock = System.Windows.Forms.DockStyle.Top
		Me.panel1.Location = New System.Drawing.Point(0, 52)
		Me.panel1.Name = "panel1"
		Me.panel1.Size = New System.Drawing.Size(1026, 35)
		Me.panel1.TabIndex = 9
		'
		'axToolbarControl2
		'
		Me.axToolbarControl2.Location = New System.Drawing.Point(0, 1)
		Me.axToolbarControl2.Name = "axToolbarControl2"
		Me.axToolbarControl2.OcxState = CType(resources.GetObject("axToolbarControl2.OcxState"), System.Windows.Forms.AxHost.State)
		Me.axToolbarControl2.Size = New System.Drawing.Size(1023, 28)
		Me.axToolbarControl2.TabIndex = 0
		'
		'MainForm
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(1026, 586)
		Me.Controls.Add(Me.splitContainer1)
		Me.Controls.Add(Me.panel1)
		Me.Controls.Add(Me.axLicenseControl1)
		Me.Controls.Add(Me.statusStrip1)
		Me.Controls.Add(Me.axToolbarControl1)
		Me.Controls.Add(Me.menuStrip1)
		Me.MainMenuStrip = Me.menuStrip1
		Me.Name = "MainForm"
		Me.Text = "Schematic Engine Application"
		Me.menuStrip1.ResumeLayout(False)
		Me.menuStrip1.PerformLayout()
		CType(Me.axToolbarControl1, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.axLicenseControl1, System.ComponentModel.ISupportInitialize).EndInit()
		Me.statusStrip1.ResumeLayout(False)
		Me.statusStrip1.PerformLayout()
		Me.splitContainer1.Panel1.ResumeLayout(False)
		Me.splitContainer1.Panel2.ResumeLayout(False)
		Me.splitContainer1.ResumeLayout(False)
		CType(Me.axTOCControl1, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.axMapControl1, System.ComponentModel.ISupportInitialize).EndInit()
		Me.panel1.ResumeLayout(False)
		CType(Me.axToolbarControl2, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub

#End Region

	Private menuStrip1 As System.Windows.Forms.MenuStrip
	Private WithEvents menuFile As System.Windows.Forms.ToolStripMenuItem
	Private WithEvents menuNewDoc As System.Windows.Forms.ToolStripMenuItem
	Private WithEvents menuOpenDoc As System.Windows.Forms.ToolStripMenuItem
	Private WithEvents menuSaveDoc As System.Windows.Forms.ToolStripMenuItem
	Private WithEvents menuSaveAs As System.Windows.Forms.ToolStripMenuItem
	Private WithEvents menuExitApp As System.Windows.Forms.ToolStripMenuItem
	Private menuSeparator As System.Windows.Forms.ToolStripSeparator
	Private WithEvents axToolbarControl1 As ESRI.ArcGIS.Controls.AxToolbarControl
	Private axLicenseControl1 As ESRI.ArcGIS.Controls.AxLicenseControl
	Private statusStrip1 As System.Windows.Forms.StatusStrip
	Private statusBarXY As System.Windows.Forms.ToolStripStatusLabel
	Private splitContainer1 As System.Windows.Forms.SplitContainer
	Private WithEvents axTOCControl1 As ESRI.ArcGIS.Controls.AxTOCControl
	Private WithEvents axMapControl1 As ESRI.ArcGIS.Controls.AxMapControl
	Private panel1 As System.Windows.Forms.Panel
	Private WithEvents axToolbarControl2 As ESRI.ArcGIS.Controls.AxToolbarControl
	Private WithEvents cboFrame As System.Windows.Forms.ComboBox
End Class
