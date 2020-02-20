/*

   Copyright 2019 Esri

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
namespace WorkingWithPackages
{
  partial class FrmMapControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMapControl));
            this.axMapControl1 = new ESRI.ArcGIS.Controls.AxMapControl();
            this.axTOCControl1 = new ESRI.ArcGIS.Controls.AxTOCControl();
            this.axToolbarControl1 = new ESRI.ArcGIS.Controls.AxToolbarControl();
            this.axLicenseControl1 = new ESRI.ArcGIS.Controls.AxLicenseControl();
            this.btnLoadlpk = new System.Windows.Forms.Button();
            this.btnLoadmpk = new System.Windows.Forms.Button();
            this.txtLayerPackage = new System.Windows.Forms.TextBox();
            this.txtMapPackage = new System.Windows.Forms.TextBox();
            this.txtWebMap = new System.Windows.Forms.TextBox();
            this.btnWebMap = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axTOCControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axToolbarControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // axMapControl1
            // 
            this.axMapControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.axMapControl1.Location = new System.Drawing.Point(214, 41);
            this.axMapControl1.Name = "axMapControl1";
            this.axMapControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axMapControl1.OcxState")));
            this.axMapControl1.Size = new System.Drawing.Size(440, 328);
            this.axMapControl1.TabIndex = 0;
            // 
            // axTOCControl1
            // 
            this.axTOCControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.axTOCControl1.Location = new System.Drawing.Point(12, 41);
            this.axTOCControl1.Name = "axTOCControl1";
            this.axTOCControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axTOCControl1.OcxState")));
            this.axTOCControl1.Size = new System.Drawing.Size(196, 328);
            this.axTOCControl1.TabIndex = 1;
            // 
            // axToolbarControl1
            // 
            this.axToolbarControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.axToolbarControl1.Location = new System.Drawing.Point(13, 12);
            this.axToolbarControl1.Name = "axToolbarControl1";
            this.axToolbarControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axToolbarControl1.OcxState")));
            this.axToolbarControl1.Size = new System.Drawing.Size(641, 28);
            this.axToolbarControl1.TabIndex = 2;
            // 
            // axLicenseControl1
            // 
            this.axLicenseControl1.Enabled = true;
            this.axLicenseControl1.Location = new System.Drawing.Point(237, 139);
            this.axLicenseControl1.Name = "axLicenseControl1";
            this.axLicenseControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axLicenseControl1.OcxState")));
            this.axLicenseControl1.Size = new System.Drawing.Size(32, 32);
            this.axLicenseControl1.TabIndex = 3;
            // 
            // btnLoadlpk
            // 
            this.btnLoadlpk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnLoadlpk.Location = new System.Drawing.Point(13, 375);
            this.btnLoadlpk.Name = "btnLoadlpk";
            this.btnLoadlpk.Size = new System.Drawing.Size(130, 34);
            this.btnLoadlpk.TabIndex = 4;
            this.btnLoadlpk.Text = "Load Layer Package";
            this.btnLoadlpk.UseVisualStyleBackColor = true;
            this.btnLoadlpk.Click += new System.EventHandler(this.btnLoadlpk_Click);
            // 
            // btnLoadmpk
            // 
            this.btnLoadmpk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnLoadmpk.Location = new System.Drawing.Point(12, 415);
            this.btnLoadmpk.Name = "btnLoadmpk";
            this.btnLoadmpk.Size = new System.Drawing.Size(130, 34);
            this.btnLoadmpk.TabIndex = 5;
            this.btnLoadmpk.Text = "Load Map Package";
            this.btnLoadmpk.UseVisualStyleBackColor = true;
            this.btnLoadmpk.Click += new System.EventHandler(this.btnLoadmpk_Click);
            // 
            // txtLayerPackage
            // 
            this.txtLayerPackage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLayerPackage.Location = new System.Drawing.Point(150, 388);
            this.txtLayerPackage.Name = "txtLayerPackage";
            this.txtLayerPackage.Size = new System.Drawing.Size(504, 20);
            this.txtLayerPackage.TabIndex = 6;
            this.txtLayerPackage.Text = "http://www.arcgis.com/sharing/content/items/483b230c56a44c33beb13f9b9ab9f88d/item" +
    ".pkinfo";
            // 
            // txtMapPackage
            // 
            this.txtMapPackage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMapPackage.Location = new System.Drawing.Point(150, 429);
            this.txtMapPackage.Name = "txtMapPackage";
            this.txtMapPackage.Size = new System.Drawing.Size(504, 20);
            this.txtMapPackage.TabIndex = 7;
            this.txtMapPackage.Text = "http://www.arcgis.com/sharing/content/items/326babea97ba4ab79e4292904e0478cc/item" +
    ".pkinfo";
            // 
            // txtWebMap
            // 
            this.txtWebMap.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtWebMap.Location = new System.Drawing.Point(150, 469);
            this.txtWebMap.Name = "txtWebMap";
            this.txtWebMap.Size = new System.Drawing.Size(504, 20);
            this.txtWebMap.TabIndex = 9;
            this.txtWebMap.Text = "http://www.arcgis.com/sharing/content/items/931d892ac7a843d7ba29d085e0433465/item" +
    ".pkinfo";
            // 
            // btnWebMap
            // 
            this.btnWebMap.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnWebMap.Location = new System.Drawing.Point(12, 455);
            this.btnWebMap.Name = "btnWebMap";
            this.btnWebMap.Size = new System.Drawing.Size(130, 34);
            this.btnWebMap.TabIndex = 8;
            this.btnWebMap.Text = "Load Web Map";
            this.btnWebMap.UseVisualStyleBackColor = true;
            this.btnWebMap.Click += new System.EventHandler(this.btnWebMap_Click);
            // 
            // FrmMapControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(666, 501);
            this.Controls.Add(this.txtWebMap);
            this.Controls.Add(this.btnWebMap);
            this.Controls.Add(this.txtMapPackage);
            this.Controls.Add(this.txtLayerPackage);
            this.Controls.Add(this.btnLoadmpk);
            this.Controls.Add(this.btnLoadlpk);
            this.Controls.Add(this.axLicenseControl1);
            this.Controls.Add(this.axToolbarControl1);
            this.Controls.Add(this.axTOCControl1);
            this.Controls.Add(this.axMapControl1);
            this.Name = "FrmMapControl";
            this.Text = "Working With Packages";
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axTOCControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axToolbarControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private ESRI.ArcGIS.Controls.AxMapControl axMapControl1;
    private ESRI.ArcGIS.Controls.AxTOCControl axTOCControl1;
    private ESRI.ArcGIS.Controls.AxToolbarControl axToolbarControl1;
    private ESRI.ArcGIS.Controls.AxLicenseControl axLicenseControl1;
    private System.Windows.Forms.Button btnLoadlpk;
    private System.Windows.Forms.Button btnLoadmpk;
    private System.Windows.Forms.TextBox txtLayerPackage;
    private System.Windows.Forms.TextBox txtMapPackage;
    private System.Windows.Forms.TextBox txtWebMap;
    private System.Windows.Forms.Button btnWebMap;
  }
}

