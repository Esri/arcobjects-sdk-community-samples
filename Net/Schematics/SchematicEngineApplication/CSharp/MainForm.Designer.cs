/*

   Copyright 2016 Esri

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.

   See the License for the specific language governing permissions and
   limitations under the License.

*/
namespace SchematicApplication
{
	partial class MainForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			//Ensures that any ESRI libraries that have been used are unloaded in the correct order. 
			//Failure to do this may result in random crashes on exit due to the operating system unloading 
			//the libraries in the incorrect order. 
			ESRI.ArcGIS.ADF.COMSupport.AOUninitialize.Shutdown();

			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.menuFile = new System.Windows.Forms.ToolStripMenuItem();
			this.menuNewDoc = new System.Windows.Forms.ToolStripMenuItem();
			this.menuOpenDoc = new System.Windows.Forms.ToolStripMenuItem();
			this.menuSaveDoc = new System.Windows.Forms.ToolStripMenuItem();
			this.menuSaveAs = new System.Windows.Forms.ToolStripMenuItem();
			this.menuSeparator = new System.Windows.Forms.ToolStripSeparator();
			this.menuExitApp = new System.Windows.Forms.ToolStripMenuItem();
			this.axToolbarControl1 = new ESRI.ArcGIS.Controls.AxToolbarControl();
			this.axLicenseControl1 = new ESRI.ArcGIS.Controls.AxLicenseControl();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.statusBarXY = new System.Windows.Forms.ToolStripStatusLabel();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.cboFrame = new System.Windows.Forms.ComboBox();
			this.axTOCControl1 = new ESRI.ArcGIS.Controls.AxTOCControl();
			this.axMapControl1 = new ESRI.ArcGIS.Controls.AxMapControl();
			this.panel1 = new System.Windows.Forms.Panel();
			this.axToolbarControl2 = new ESRI.ArcGIS.Controls.AxToolbarControl();
			this.menuStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.axToolbarControl1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).BeginInit();
			this.statusStrip1.SuspendLayout();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.axTOCControl1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.axMapControl1)).BeginInit();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.axToolbarControl2)).BeginInit();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFile});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(1026, 24);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// menuFile
			// 
			this.menuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuNewDoc,
            this.menuOpenDoc,
            this.menuSaveDoc,
            this.menuSaveAs,
            this.menuSeparator,
            this.menuExitApp});
			this.menuFile.Name = "menuFile";
			this.menuFile.Size = new System.Drawing.Size(37, 20);
			this.menuFile.Text = "File";
			// 
			// menuNewDoc
			// 
			this.menuNewDoc.Image = ((System.Drawing.Image)(resources.GetObject("menuNewDoc.Image")));
			this.menuNewDoc.ImageTransparentColor = System.Drawing.Color.White;
			this.menuNewDoc.Name = "menuNewDoc";
			this.menuNewDoc.Size = new System.Drawing.Size(171, 22);
			this.menuNewDoc.Text = "New Document";
			this.menuNewDoc.Click += new System.EventHandler(this.menuNewDoc_Click);
			// 
			// menuOpenDoc
			// 
			this.menuOpenDoc.Image = ((System.Drawing.Image)(resources.GetObject("menuOpenDoc.Image")));
			this.menuOpenDoc.ImageTransparentColor = System.Drawing.Color.White;
			this.menuOpenDoc.Name = "menuOpenDoc";
			this.menuOpenDoc.Size = new System.Drawing.Size(171, 22);
			this.menuOpenDoc.Text = "Open Document...";
			this.menuOpenDoc.Click += new System.EventHandler(this.menuOpenDoc_Click);
			// 
			// menuSaveDoc
			// 
			this.menuSaveDoc.Image = ((System.Drawing.Image)(resources.GetObject("menuSaveDoc.Image")));
			this.menuSaveDoc.ImageTransparentColor = System.Drawing.Color.White;
			this.menuSaveDoc.Name = "menuSaveDoc";
			this.menuSaveDoc.Size = new System.Drawing.Size(171, 22);
			this.menuSaveDoc.Text = "SaveDocument";
			this.menuSaveDoc.Click += new System.EventHandler(this.menuSaveDoc_Click);
			// 
			// menuSaveAs
			// 
			this.menuSaveAs.Name = "menuSaveAs";
			this.menuSaveAs.Size = new System.Drawing.Size(171, 22);
			this.menuSaveAs.Text = "Save As...";
			this.menuSaveAs.Click += new System.EventHandler(this.menuSaveAs_Click);
			// 
			// menuSeparator
			// 
			this.menuSeparator.Name = "menuSeparator";
			this.menuSeparator.Size = new System.Drawing.Size(168, 6);
			// 
			// menuExitApp
			// 
			this.menuExitApp.Name = "menuExitApp";
			this.menuExitApp.Size = new System.Drawing.Size(171, 22);
			this.menuExitApp.Text = "Exit";
			this.menuExitApp.Click += new System.EventHandler(this.menuExitApp_Click);
			// 
			// axToolbarControl1
			// 
			this.axToolbarControl1.Dock = System.Windows.Forms.DockStyle.Top;
			this.axToolbarControl1.Location = new System.Drawing.Point(0, 24);
			this.axToolbarControl1.Name = "axToolbarControl1";
			this.axToolbarControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axToolbarControl1.OcxState")));
			this.axToolbarControl1.Size = new System.Drawing.Size(1026, 28);
			this.axToolbarControl1.TabIndex = 3;
			// 
			// axLicenseControl1
			// 
			this.axLicenseControl1.Enabled = true;
			this.axLicenseControl1.Location = new System.Drawing.Point(466, 278);
			this.axLicenseControl1.Name = "axLicenseControl1";
			this.axLicenseControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axLicenseControl1.OcxState")));
			this.axLicenseControl1.Size = new System.Drawing.Size(32, 32);
			this.axLicenseControl1.TabIndex = 5;
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusBarXY});
			this.statusStrip1.Location = new System.Drawing.Point(0, 564);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(1026, 22);
			this.statusStrip1.Stretch = false;
			this.statusStrip1.TabIndex = 7;
			this.statusStrip1.Text = "statusBar1";
			// 
			// statusBarXY
			// 
			this.statusBarXY.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
			this.statusBarXY.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.statusBarXY.Name = "statusBarXY";
			this.statusBarXY.Size = new System.Drawing.Size(308, 17);
			this.statusBarXY.Text = "Load a map document or a diagram into the MapControl";
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 87);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.cboFrame);
			this.splitContainer1.Panel1.Controls.Add(this.axTOCControl1);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.axMapControl1);
			this.splitContainer1.Size = new System.Drawing.Size(1026, 477);
			this.splitContainer1.SplitterDistance = 218;
			this.splitContainer1.TabIndex = 8;
			// 
			// cboFrame
			// 
			this.cboFrame.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
									| System.Windows.Forms.AnchorStyles.Right)));
			this.cboFrame.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboFrame.FormattingEnabled = true;
			this.cboFrame.Location = new System.Drawing.Point(4, 7);
			this.cboFrame.Name = "cboFrame";
			this.cboFrame.Size = new System.Drawing.Size(211, 21);
			this.cboFrame.TabIndex = 6;
			this.cboFrame.SelectedValueChanged += new System.EventHandler(this.onSelectedValueChanged);
			// 
			// axTOCControl1
			// 
			this.axTOCControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
									| System.Windows.Forms.AnchorStyles.Left)
									| System.Windows.Forms.AnchorStyles.Right)));
			this.axTOCControl1.Location = new System.Drawing.Point(0, 34);
			this.axTOCControl1.Name = "axTOCControl1";
			this.axTOCControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axTOCControl1.OcxState")));
			this.axTOCControl1.Size = new System.Drawing.Size(218, 443);
			this.axTOCControl1.TabIndex = 5;
			this.axTOCControl1.OnMouseDown += new ESRI.ArcGIS.Controls.ITOCControlEvents_Ax_OnMouseDownEventHandler(this.axTOCControl1_OnMouseDown);
			// 
			// axMapControl1
			// 
			this.axMapControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.axMapControl1.Location = new System.Drawing.Point(0, 0);
			this.axMapControl1.Name = "axMapControl1";
			this.axMapControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axMapControl1.OcxState")));
			this.axMapControl1.Size = new System.Drawing.Size(804, 477);
			this.axMapControl1.TabIndex = 3;
			this.axMapControl1.OnMouseMove += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMouseMoveEventHandler(this.axMapControl1_OnMouseMove);
			this.axMapControl1.OnMapReplaced += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMapReplacedEventHandler(this.axMapControl1_OnMapReplaced);
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.axToolbarControl2);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 52);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(1026, 35);
			this.panel1.TabIndex = 9;
			// 
			// axToolbarControl2
			// 
			this.axToolbarControl2.Location = new System.Drawing.Point(0, 1);
			this.axToolbarControl2.Name = "axToolbarControl2";
			this.axToolbarControl2.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axToolbarControl2.OcxState")));
			this.axToolbarControl2.Size = new System.Drawing.Size(1023, 28);
			this.axToolbarControl2.TabIndex = 0;
			this.axToolbarControl2.OnItemClick += new ESRI.ArcGIS.Controls.IToolbarControlEvents_Ax_OnItemClickEventHandler(this.onToolbarItemClick);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1026, 586);
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.axLicenseControl1);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.axToolbarControl1);
			this.Controls.Add(this.menuStrip1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "MainForm";
			this.Text = "Schematic Engine Application";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.axToolbarControl1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).EndInit();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.axTOCControl1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.axMapControl1)).EndInit();
			this.panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.axToolbarControl2)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem menuFile;
		private System.Windows.Forms.ToolStripMenuItem menuNewDoc;
		private System.Windows.Forms.ToolStripMenuItem menuOpenDoc;
		private System.Windows.Forms.ToolStripMenuItem menuSaveDoc;
		private System.Windows.Forms.ToolStripMenuItem menuSaveAs;
		private System.Windows.Forms.ToolStripMenuItem menuExitApp;
		private System.Windows.Forms.ToolStripSeparator menuSeparator;
		private ESRI.ArcGIS.Controls.AxToolbarControl axToolbarControl1;
		private ESRI.ArcGIS.Controls.AxLicenseControl axLicenseControl1;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel statusBarXY;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private ESRI.ArcGIS.Controls.AxTOCControl axTOCControl1;
		private ESRI.ArcGIS.Controls.AxMapControl axMapControl1;
		private System.Windows.Forms.Panel panel1;
		private ESRI.ArcGIS.Controls.AxToolbarControl axToolbarControl2;
		private System.Windows.Forms.ComboBox cboFrame;
	}
}

