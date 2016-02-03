namespace Controls
{
    partial class MapViewer
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
          System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MapViewer));
          this.axMapControl1 = new ESRI.ArcGIS.Controls.AxMapControl();
          this.axTOCControl1 = new ESRI.ArcGIS.Controls.AxTOCControl();
          this.menuStrip1 = new System.Windows.Forms.MenuStrip();
          this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
          this.printToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
          this.axToolbarControl1 = new ESRI.ArcGIS.Controls.AxToolbarControl();
          this.axPageLayoutControl1 = new ESRI.ArcGIS.Controls.AxPageLayoutControl();
          this.axLicenseControl1 = new ESRI.ArcGIS.Controls.AxLicenseControl();
          this.chkCustomize = new System.Windows.Forms.CheckBox();
          ((System.ComponentModel.ISupportInitialize)(this.axMapControl1)).BeginInit();
          ((System.ComponentModel.ISupportInitialize)(this.axTOCControl1)).BeginInit();
          this.menuStrip1.SuspendLayout();
          ((System.ComponentModel.ISupportInitialize)(this.axToolbarControl1)).BeginInit();
          ((System.ComponentModel.ISupportInitialize)(this.axPageLayoutControl1)).BeginInit();
          ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).BeginInit();
          this.SuspendLayout();
          // 
          // axMapControl1
          // 
          this.axMapControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                      | System.Windows.Forms.AnchorStyles.Left)));
          this.axMapControl1.Location = new System.Drawing.Point(2, 318);
          this.axMapControl1.Margin = new System.Windows.Forms.Padding(2);
          this.axMapControl1.Name = "axMapControl1";
          this.axMapControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axMapControl1.OcxState")));
          this.axMapControl1.Size = new System.Drawing.Size(165, 133);
          this.axMapControl1.TabIndex = 0;
          this.axMapControl1.OnAfterDraw += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnAfterDrawEventHandler(this.axMapControl1_OnAfterDraw);
          // 
          // axTOCControl1
          // 
          this.axTOCControl1.Location = new System.Drawing.Point(2, 61);
          this.axTOCControl1.Margin = new System.Windows.Forms.Padding(2);
          this.axTOCControl1.Name = "axTOCControl1";
          this.axTOCControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axTOCControl1.OcxState")));
          this.axTOCControl1.Size = new System.Drawing.Size(165, 253);
          this.axTOCControl1.TabIndex = 1;
          this.axTOCControl1.OnEndLabelEdit += new ESRI.ArcGIS.Controls.ITOCControlEvents_Ax_OnEndLabelEditEventHandler(this.axTOCControl1_OnEndLabelEdit);
          // 
          // menuStrip1
          // 
          this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
          this.menuStrip1.Location = new System.Drawing.Point(0, 0);
          this.menuStrip1.Name = "menuStrip1";
          this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
          this.menuStrip1.Size = new System.Drawing.Size(719, 24);
          this.menuStrip1.TabIndex = 2;
          this.menuStrip1.Text = "menuStrip1";
          // 
          // fileToolStripMenuItem
          // 
          this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.printToolStripMenuItem});
          this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
          this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
          this.fileToolStripMenuItem.Text = "File";
          // 
          // printToolStripMenuItem
          // 
          this.printToolStripMenuItem.Name = "printToolStripMenuItem";
          this.printToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
          this.printToolStripMenuItem.Text = "Print...";
          this.printToolStripMenuItem.Click += new System.EventHandler(this.printToolStripMenuItem_Click);
          // 
          // axToolbarControl1
          // 
          this.axToolbarControl1.Location = new System.Drawing.Point(2, 28);
          this.axToolbarControl1.Margin = new System.Windows.Forms.Padding(2);
          this.axToolbarControl1.Name = "axToolbarControl1";
          this.axToolbarControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axToolbarControl1.OcxState")));
          this.axToolbarControl1.Size = new System.Drawing.Size(628, 28);
          this.axToolbarControl1.TabIndex = 3;
          // 
          // axPageLayoutControl1
          // 
          this.axPageLayoutControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                      | System.Windows.Forms.AnchorStyles.Left)
                      | System.Windows.Forms.AnchorStyles.Right)));
          this.axPageLayoutControl1.Location = new System.Drawing.Point(171, 61);
          this.axPageLayoutControl1.Margin = new System.Windows.Forms.Padding(2);
          this.axPageLayoutControl1.Name = "axPageLayoutControl1";
          this.axPageLayoutControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axPageLayoutControl1.OcxState")));
          this.axPageLayoutControl1.Size = new System.Drawing.Size(546, 390);
          this.axPageLayoutControl1.TabIndex = 4;
          this.axPageLayoutControl1.OnPageLayoutReplaced += new ESRI.ArcGIS.Controls.IPageLayoutControlEvents_Ax_OnPageLayoutReplacedEventHandler(this.axPageLayoutControl1_OnPageLayoutReplaced);
          this.axPageLayoutControl1.OnMouseDown += new ESRI.ArcGIS.Controls.IPageLayoutControlEvents_Ax_OnMouseDownEventHandler(this.axPageLayoutControl1_OnMouseDown);
          // 
          // axLicenseControl1
          // 
          this.axLicenseControl1.Enabled = true;
          this.axLicenseControl1.Location = new System.Drawing.Point(226, 107);
          this.axLicenseControl1.Margin = new System.Windows.Forms.Padding(2);
          this.axLicenseControl1.Name = "axLicenseControl1";
          this.axLicenseControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axLicenseControl1.OcxState")));
          this.axLicenseControl1.Size = new System.Drawing.Size(32, 32);
          this.axLicenseControl1.TabIndex = 5;
          // 
          // chkCustomize
          // 
          this.chkCustomize.AutoSize = true;
          this.chkCustomize.Location = new System.Drawing.Point(634, 28);
          this.chkCustomize.Margin = new System.Windows.Forms.Padding(2);
          this.chkCustomize.Name = "chkCustomize";
          this.chkCustomize.Size = new System.Drawing.Size(74, 17);
          this.chkCustomize.TabIndex = 6;
          this.chkCustomize.Text = "Customize";
          this.chkCustomize.UseVisualStyleBackColor = true;
          this.chkCustomize.CheckedChanged += new System.EventHandler(this.chkCustomize_CheckedChanged);
          // 
          // MapViewer
          // 
          this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
          this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
          this.ClientSize = new System.Drawing.Size(719, 453);
          this.Controls.Add(this.chkCustomize);
          this.Controls.Add(this.axLicenseControl1);
          this.Controls.Add(this.axPageLayoutControl1);
          this.Controls.Add(this.axToolbarControl1);
          this.Controls.Add(this.axTOCControl1);
          this.Controls.Add(this.axMapControl1);
          this.Controls.Add(this.menuStrip1);
          this.MainMenuStrip = this.menuStrip1;
          this.Margin = new System.Windows.Forms.Padding(2);
          this.Name = "MapViewer";
          this.Text = "Map Viewer";
          this.Load += new System.EventHandler(this.MapViewer_Load);
          this.ResizeBegin += new System.EventHandler(this.MapViewer_ResizeBegin);
          this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MapViewer_FormClosing);
          this.ResizeEnd += new System.EventHandler(this.MapViewer_ResizeEnd);
          ((System.ComponentModel.ISupportInitialize)(this.axMapControl1)).EndInit();
          ((System.ComponentModel.ISupportInitialize)(this.axTOCControl1)).EndInit();
          this.menuStrip1.ResumeLayout(false);
          this.menuStrip1.PerformLayout();
          ((System.ComponentModel.ISupportInitialize)(this.axToolbarControl1)).EndInit();
          ((System.ComponentModel.ISupportInitialize)(this.axPageLayoutControl1)).EndInit();
          ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).EndInit();
          this.ResumeLayout(false);
          this.PerformLayout();

        }

        #endregion

        private ESRI.ArcGIS.Controls.AxMapControl axMapControl1;
        private ESRI.ArcGIS.Controls.AxTOCControl axTOCControl1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private ESRI.ArcGIS.Controls.AxToolbarControl axToolbarControl1;
        private ESRI.ArcGIS.Controls.AxPageLayoutControl axPageLayoutControl1;
        private ESRI.ArcGIS.Controls.AxLicenseControl axLicenseControl1;
        private System.Windows.Forms.CheckBox chkCustomize;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem printToolStripMenuItem;
    }
}

