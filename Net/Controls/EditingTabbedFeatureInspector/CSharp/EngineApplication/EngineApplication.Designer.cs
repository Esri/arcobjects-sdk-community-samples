namespace TabbedInspectorEngineApplication
{
  partial class EngineApplication
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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EngineApplication));
        this.toolbar = new ESRI.ArcGIS.Controls.AxToolbarControl();
        this.axLicenseControl1 = new ESRI.ArcGIS.Controls.AxLicenseControl();
        this.tableOfContents = new ESRI.ArcGIS.Controls.AxTOCControl();
        this.map = new ESRI.ArcGIS.Controls.AxMapControl();
        this.status = new System.Windows.Forms.TextBox();
        this.splitContainer1 = new System.Windows.Forms.SplitContainer();
        ((System.ComponentModel.ISupportInitialize)(this.toolbar)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.tableOfContents)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.map)).BeginInit();
        this.splitContainer1.Panel1.SuspendLayout();
        this.splitContainer1.Panel2.SuspendLayout();
        this.splitContainer1.SuspendLayout();
        this.SuspendLayout();
        // 
        // toolbar
        // 
        this.toolbar.Dock = System.Windows.Forms.DockStyle.Top;
        this.toolbar.Location = new System.Drawing.Point(0, 0);
        this.toolbar.Name = "toolbar";
        this.toolbar.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("toolbar.OcxState")));
        this.toolbar.Size = new System.Drawing.Size(942, 28);
        this.toolbar.TabIndex = 1;
        // 
        // axLicenseControl1
        // 
        this.axLicenseControl1.Enabled = true;
        this.axLicenseControl1.Location = new System.Drawing.Point(8, 8);
        this.axLicenseControl1.Name = "axLicenseControl1";
        this.axLicenseControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axLicenseControl1.OcxState")));
        this.axLicenseControl1.Size = new System.Drawing.Size(32, 32);
        this.axLicenseControl1.TabIndex = 3;
        // 
        // tableOfContents
        // 
        this.tableOfContents.Dock = System.Windows.Forms.DockStyle.Fill;
        this.tableOfContents.Location = new System.Drawing.Point(0, 0);
        this.tableOfContents.Name = "tableOfContents";
        this.tableOfContents.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("tableOfContents.OcxState")));
        this.tableOfContents.Size = new System.Drawing.Size(279, 544);
        this.tableOfContents.TabIndex = 4;
        // 
        // map
        // 
        this.map.Dock = System.Windows.Forms.DockStyle.Fill;
        this.map.Location = new System.Drawing.Point(0, 0);
        this.map.Name = "map";
        this.map.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("map.OcxState")));
        this.map.Size = new System.Drawing.Size(659, 544);
        this.map.TabIndex = 5;
        // 
        // status
        // 
        this.status.BackColor = System.Drawing.SystemColors.ButtonFace;
        this.status.Dock = System.Windows.Forms.DockStyle.Bottom;
        this.status.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.status.Location = new System.Drawing.Point(0, 572);
        this.status.Name = "status";
        this.status.Size = new System.Drawing.Size(942, 26);
        this.status.TabIndex = 6;
        // 
        // splitContainer1
        // 
        this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
        this.splitContainer1.Location = new System.Drawing.Point(0, 28);
        this.splitContainer1.Name = "splitContainer1";
        // 
        // splitContainer1.Panel1
        // 
        this.splitContainer1.Panel1.Controls.Add(this.tableOfContents);
        // 
        // splitContainer1.Panel2
        // 
        this.splitContainer1.Panel2.Controls.Add(this.map);
        this.splitContainer1.Size = new System.Drawing.Size(942, 544);
        this.splitContainer1.SplitterDistance = 279;
        this.splitContainer1.TabIndex = 7;
        // 
        // EngineApplication
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(942, 598);
        this.Controls.Add(this.splitContainer1);
        this.Controls.Add(this.status);
        this.Controls.Add(this.axLicenseControl1);
        this.Controls.Add(this.toolbar);
        this.Name = "EngineApplication";
        this.Text = "Sample Engine Application";
        ((System.ComponentModel.ISupportInitialize)(this.toolbar)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.tableOfContents)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.map)).EndInit();
        this.splitContainer1.Panel1.ResumeLayout(false);
        this.splitContainer1.Panel2.ResumeLayout(false);
        this.splitContainer1.ResumeLayout(false);
        this.ResumeLayout(false);
        this.PerformLayout();

    }

    #endregion

    private ESRI.ArcGIS.Controls.AxToolbarControl toolbar;
    private ESRI.ArcGIS.Controls.AxLicenseControl axLicenseControl1;
    private ESRI.ArcGIS.Controls.AxTOCControl tableOfContents;
    private ESRI.ArcGIS.Controls.AxMapControl map;
    private System.Windows.Forms.TextBox status;
    private System.Windows.Forms.SplitContainer splitContainer1;

  }
}

