Imports Microsoft.VisualBasic
Imports System
Namespace TemporalStatistics
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
			Me.components = New System.ComponentModel.Container()
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
			Me.timerStats = New System.Windows.Forms.Timer(Me.components)
			Me.lvStatistics = New System.Windows.Forms.ListView()
			Me.ServiceName = New System.Windows.Forms.ColumnHeader()
			Me.DataRate = New System.Windows.Forms.ColumnHeader()
			Me.MessageCount = New System.Windows.Forms.ColumnHeader()
			Me.Status = New System.Windows.Forms.ColumnHeader()
			Me.TrackCount = New System.Windows.Forms.ColumnHeader()
			Me.SampleSize = New System.Windows.Forms.ColumnHeader()
			Me.panel1 = New System.Windows.Forms.Panel()
			Me.btnResetMsgRate = New System.Windows.Forms.Button()
			Me.btnResetFC = New System.Windows.Forms.Button()
			Me.btnRefresh = New System.Windows.Forms.Button()
			Me.cbTrackingServices = New System.Windows.Forms.ComboBox()
			Me.label2 = New System.Windows.Forms.Label()
			Me.txtRate = New System.Windows.Forms.TextBox()
			Me.label1 = New System.Windows.Forms.Label()
			Me.label3 = New System.Windows.Forms.Label()
			Me.txtSampleSize = New System.Windows.Forms.TextBox()
			Me.menuStrip1.SuspendLayout()
			CType(Me.axMapControl1, System.ComponentModel.ISupportInitialize).BeginInit()
			CType(Me.axToolbarControl1, System.ComponentModel.ISupportInitialize).BeginInit()
			CType(Me.axTOCControl1, System.ComponentModel.ISupportInitialize).BeginInit()
			CType(Me.axLicenseControl1, System.ComponentModel.ISupportInitialize).BeginInit()
			Me.statusStrip1.SuspendLayout()
			Me.panel1.SuspendLayout()
			Me.SuspendLayout()
			' 
			' menuStrip1
			' 
			Me.menuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() { Me.menuFile})
			Me.menuStrip1.Location = New System.Drawing.Point(0, 0)
			Me.menuStrip1.Name = "menuStrip1"
			Me.menuStrip1.Size = New System.Drawing.Size(985, 24)
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
			Me.menuNewDoc.Size = New System.Drawing.Size(174, 22)
			Me.menuNewDoc.Text = "New Document"
'			Me.menuNewDoc.Click += New System.EventHandler(Me.menuNewDoc_Click);
			' 
			' menuOpenDoc
			' 
			Me.menuOpenDoc.Image = (CType(resources.GetObject("menuOpenDoc.Image"), System.Drawing.Image))
			Me.menuOpenDoc.ImageTransparentColor = System.Drawing.Color.White
			Me.menuOpenDoc.Name = "menuOpenDoc"
			Me.menuOpenDoc.Size = New System.Drawing.Size(174, 22)
			Me.menuOpenDoc.Text = "Open Document..."
'			Me.menuOpenDoc.Click += New System.EventHandler(Me.menuOpenDoc_Click);
			' 
			' menuSaveDoc
			' 
			Me.menuSaveDoc.Image = (CType(resources.GetObject("menuSaveDoc.Image"), System.Drawing.Image))
			Me.menuSaveDoc.ImageTransparentColor = System.Drawing.Color.White
			Me.menuSaveDoc.Name = "menuSaveDoc"
			Me.menuSaveDoc.Size = New System.Drawing.Size(174, 22)
			Me.menuSaveDoc.Text = "SaveDocument"
'			Me.menuSaveDoc.Click += New System.EventHandler(Me.menuSaveDoc_Click);
			' 
			' menuSaveAs
			' 
			Me.menuSaveAs.Name = "menuSaveAs"
			Me.menuSaveAs.Size = New System.Drawing.Size(174, 22)
			Me.menuSaveAs.Text = "Save As..."
'			Me.menuSaveAs.Click += New System.EventHandler(Me.menuSaveAs_Click);
			' 
			' menuSeparator
			' 
			Me.menuSeparator.Name = "menuSeparator"
			Me.menuSeparator.Size = New System.Drawing.Size(171, 6)
			' 
			' menuExitApp
			' 
			Me.menuExitApp.Name = "menuExitApp"
			Me.menuExitApp.Size = New System.Drawing.Size(174, 22)
			Me.menuExitApp.Text = "Exit"
'			Me.menuExitApp.Click += New System.EventHandler(Me.menuExitApp_Click);
			' 
			' axMapControl1
			' 
			Me.axMapControl1.Dock = System.Windows.Forms.DockStyle.Fill
			Me.axMapControl1.Location = New System.Drawing.Point(191, 52)
			Me.axMapControl1.Name = "axMapControl1"
			Me.axMapControl1.OcxState = (CType(resources.GetObject("axMapControl1.OcxState"), System.Windows.Forms.AxHost.State))
			Me.axMapControl1.Size = New System.Drawing.Size(794, 512)
			Me.axMapControl1.TabIndex = 2
'			Me.axMapControl1.OnMouseMove += New ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMouseMoveEventHandler(Me.axMapControl1_OnMouseMove);
'			Me.axMapControl1.OnMapReplaced += New ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMapReplacedEventHandler(Me.axMapControl1_OnMapReplaced);
			' 
			' axToolbarControl1
			' 
			Me.axToolbarControl1.Dock = System.Windows.Forms.DockStyle.Top
			Me.axToolbarControl1.Location = New System.Drawing.Point(0, 24)
			Me.axToolbarControl1.Name = "axToolbarControl1"
			Me.axToolbarControl1.OcxState = (CType(resources.GetObject("axToolbarControl1.OcxState"), System.Windows.Forms.AxHost.State))
			Me.axToolbarControl1.Size = New System.Drawing.Size(985, 28)
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
			Me.statusStrip1.Size = New System.Drawing.Size(982, 22)
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
			' timerStats
			' 
			Me.timerStats.Interval = 5000
'			Me.timerStats.Tick += New System.EventHandler(Me.timerStats_Tick);
			' 
			' lvStatistics
			' 
			Me.lvStatistics.Columns.AddRange(New System.Windows.Forms.ColumnHeader() { Me.ServiceName, Me.DataRate, Me.MessageCount, Me.Status, Me.TrackCount, Me.SampleSize})
			Me.lvStatistics.Dock = System.Windows.Forms.DockStyle.Bottom
			Me.lvStatistics.Location = New System.Drawing.Point(191, 429)
			Me.lvStatistics.Name = "lvStatistics"
			Me.lvStatistics.Size = New System.Drawing.Size(794, 135)
			Me.lvStatistics.TabIndex = 8
			Me.lvStatistics.UseCompatibleStateImageBehavior = False
			Me.lvStatistics.View = System.Windows.Forms.View.Details
			' 
			' ServiceName
			' 
			Me.ServiceName.Text = "Service Name"
			Me.ServiceName.Width = 144
			' 
			' DataRate
			' 
			Me.DataRate.Text = "Data Rate (msg/sec)"
			Me.DataRate.Width = 122
			' 
			' MessageCount
			' 
			Me.MessageCount.Text = "Total Message Count"
			Me.MessageCount.Width = 123
			' 
			' Status
			' 
			Me.Status.Text = "Connection Status"
			Me.Status.Width = 117
			' 
			' TrackCount
			' 
			Me.TrackCount.Text = "Track Count"
			Me.TrackCount.Width = 77
			' 
			' SampleSize
			' 
			Me.SampleSize.Text = "Sample Size"
			Me.SampleSize.Width = 82
			' 
			' panel1
			' 
			Me.panel1.Controls.Add(Me.txtSampleSize)
			Me.panel1.Controls.Add(Me.label3)
			Me.panel1.Controls.Add(Me.label2)
			Me.panel1.Controls.Add(Me.txtRate)
			Me.panel1.Controls.Add(Me.label1)
			Me.panel1.Controls.Add(Me.btnResetMsgRate)
			Me.panel1.Controls.Add(Me.btnResetFC)
			Me.panel1.Controls.Add(Me.btnRefresh)
			Me.panel1.Controls.Add(Me.cbTrackingServices)
			Me.panel1.Dock = System.Windows.Forms.DockStyle.Bottom
			Me.panel1.Location = New System.Drawing.Point(191, 399)
			Me.panel1.Name = "panel1"
			Me.panel1.Size = New System.Drawing.Size(794, 30)
			Me.panel1.TabIndex = 10
			' 
			' btnResetMsgRate
			' 
			Me.btnResetMsgRate.Location = New System.Drawing.Point(663, 2)
			Me.btnResetMsgRate.Name = "btnResetMsgRate"
			Me.btnResetMsgRate.Size = New System.Drawing.Size(122, 23)
			Me.btnResetMsgRate.TabIndex = 3
			Me.btnResetMsgRate.Text = "Reset Message Rate"
			Me.btnResetMsgRate.UseVisualStyleBackColor = True
'			Me.btnResetMsgRate.Click += New System.EventHandler(Me.btnResetMsgRate_Click);
			' 
			' btnResetFC
			' 
			Me.btnResetFC.Location = New System.Drawing.Point(528, 2)
			Me.btnResetFC.Name = "btnResetFC"
			Me.btnResetFC.Size = New System.Drawing.Size(128, 23)
			Me.btnResetFC.TabIndex = 2
			Me.btnResetFC.Text = "Reset Feature Count"
			Me.btnResetFC.UseVisualStyleBackColor = True
'			Me.btnResetFC.Click += New System.EventHandler(Me.btnResetFC_Click);
			' 
			' btnRefresh
			' 
			Me.btnRefresh.Location = New System.Drawing.Point(447, 2)
			Me.btnRefresh.Name = "btnRefresh"
			Me.btnRefresh.Size = New System.Drawing.Size(75, 23)
			Me.btnRefresh.TabIndex = 1
			Me.btnRefresh.Text = "Refresh"
			Me.btnRefresh.UseVisualStyleBackColor = True
'			Me.btnRefresh.Click += New System.EventHandler(Me.btnRefresh_Click);
			' 
			' cbTrackingServices
			' 
			Me.cbTrackingServices.FormattingEnabled = True
			Me.cbTrackingServices.Location = New System.Drawing.Point(4, 4)
			Me.cbTrackingServices.Name = "cbTrackingServices"
			Me.cbTrackingServices.Size = New System.Drawing.Size(173, 21)
			Me.cbTrackingServices.TabIndex = 0
'			Me.cbTrackingServices.SelectionChangeCommitted += New System.EventHandler(Me.cbTrackingServices_SelectionChangeCommitted);
			' 
			' label2
			' 
			Me.label2.AutoSize = True
			Me.label2.Location = New System.Drawing.Point(299, 7)
			Me.label2.Name = "label2"
			Me.label2.Size = New System.Drawing.Size(24, 13)
			Me.label2.TabIndex = 9
			Me.label2.Text = "sec"
			' 
			' txtRate
			' 
			Me.txtRate.Location = New System.Drawing.Point(258, 4)
			Me.txtRate.Name = "txtRate"
			Me.txtRate.Size = New System.Drawing.Size(38, 20)
			Me.txtRate.TabIndex = 8
			Me.txtRate.Text = "5.0"
			Me.txtRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
			' 
			' label1
			' 
			Me.label1.AutoSize = True
			Me.label1.Location = New System.Drawing.Point(183, 7)
			Me.label1.Name = "label1"
			Me.label1.Size = New System.Drawing.Size(73, 13)
			Me.label1.TabIndex = 7
			Me.label1.Text = "Refresh Rate:"
			' 
			' label3
			' 
			Me.label3.AutoSize = True
			Me.label3.Location = New System.Drawing.Point(329, 7)
			Me.label3.Name = "label3"
			Me.label3.Size = New System.Drawing.Size(68, 13)
			Me.label3.TabIndex = 10
			Me.label3.Text = "Sample Size:"
			' 
			' txtSampleSize
			' 
			Me.txtSampleSize.Location = New System.Drawing.Point(403, 4)
			Me.txtSampleSize.Name = "txtSampleSize"
			Me.txtSampleSize.Size = New System.Drawing.Size(38, 20)
			Me.txtSampleSize.TabIndex = 11
			Me.txtSampleSize.Text = "500"
			Me.txtSampleSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
			' 
			' MainForm
			' 
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.ClientSize = New System.Drawing.Size(985, 586)
			Me.Controls.Add(Me.panel1)
			Me.Controls.Add(Me.lvStatistics)
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
'			Me.Load += New System.EventHandler(Me.MainForm_Load);
			Me.menuStrip1.ResumeLayout(False)
			Me.menuStrip1.PerformLayout()
			CType(Me.axMapControl1, System.ComponentModel.ISupportInitialize).EndInit()
			CType(Me.axToolbarControl1, System.ComponentModel.ISupportInitialize).EndInit()
			CType(Me.axTOCControl1, System.ComponentModel.ISupportInitialize).EndInit()
			CType(Me.axLicenseControl1, System.ComponentModel.ISupportInitialize).EndInit()
			Me.statusStrip1.ResumeLayout(False)
			Me.statusStrip1.PerformLayout()
			Me.panel1.ResumeLayout(False)
			Me.panel1.PerformLayout()
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
		Private WithEvents timerStats As System.Windows.Forms.Timer
		Private lvStatistics As System.Windows.Forms.ListView
		Private ServiceName As System.Windows.Forms.ColumnHeader
		Private DataRate As System.Windows.Forms.ColumnHeader
		Private MessageCount As System.Windows.Forms.ColumnHeader
		Private Status As System.Windows.Forms.ColumnHeader
		Private TrackCount As System.Windows.Forms.ColumnHeader
		Private SampleSize As System.Windows.Forms.ColumnHeader
		Private panel1 As System.Windows.Forms.Panel
		Private WithEvents btnResetFC As System.Windows.Forms.Button
		Private WithEvents btnRefresh As System.Windows.Forms.Button
		Private WithEvents cbTrackingServices As System.Windows.Forms.ComboBox
		Private WithEvents btnResetMsgRate As System.Windows.Forms.Button
		Private txtSampleSize As System.Windows.Forms.TextBox
		Private label3 As System.Windows.Forms.Label
		Private label2 As System.Windows.Forms.Label
		Private txtRate As System.Windows.Forms.TextBox
		Private label1 As System.Windows.Forms.Label
	End Class
End Namespace

