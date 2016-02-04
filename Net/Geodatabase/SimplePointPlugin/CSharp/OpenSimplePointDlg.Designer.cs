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
namespace ESRI.ArcGIS.Samples.SimplePointPlugin
{
  partial class OpenSimplePointDlg
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
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OpenSimplePointDlg));
      this.btnCancel = new System.Windows.Forms.Button();
      this.btnOK = new System.Windows.Forms.Button();
      this.btnOpenDataSource = new System.Windows.Forms.Button();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.lblDatasets = new System.Windows.Forms.Label();
      this.lstDeatureClasses = new System.Windows.Forms.ListBox();
      this.txtPath = new System.Windows.Forms.TextBox();
      this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
      this.groupBox1.SuspendLayout();
      this.SuspendLayout();
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(195, 276);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(75, 23);
      this.btnCancel.TabIndex = 0;
      this.btnCancel.Text = "Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
      // 
      // btnOK
      // 
      this.btnOK.Location = new System.Drawing.Point(37, 276);
      this.btnOK.Name = "btnOK";
      this.btnOK.Size = new System.Drawing.Size(75, 23);
      this.btnOK.TabIndex = 1;
      this.btnOK.Text = "OK";
      this.btnOK.UseVisualStyleBackColor = true;
      this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
      // 
      // btnOpenDataSource
      // 
      this.btnOpenDataSource.Image = ((System.Drawing.Image)(resources.GetObject("btnOpenDataSource.Image")));
      this.btnOpenDataSource.Location = new System.Drawing.Point(249, 17);
      this.btnOpenDataSource.Name = "btnOpenDataSource";
      this.btnOpenDataSource.Size = new System.Drawing.Size(23, 23);
      this.btnOpenDataSource.TabIndex = 2;
      this.toolTip1.SetToolTip(this.btnOpenDataSource, "Open Simple Point data");
      this.btnOpenDataSource.UseVisualStyleBackColor = true;
      this.btnOpenDataSource.Click += new System.EventHandler(this.btnOpenDataSource_Click);
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.lblDatasets);
      this.groupBox1.Controls.Add(this.lstDeatureClasses);
      this.groupBox1.Controls.Add(this.txtPath);
      this.groupBox1.Controls.Add(this.btnOpenDataSource);
      this.groupBox1.Location = new System.Drawing.Point(12, 12);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(280, 249);
      this.groupBox1.TabIndex = 3;
      this.groupBox1.TabStop = false;
      // 
      // lblDatasets
      // 
      this.lblDatasets.AutoSize = true;
      this.lblDatasets.Location = new System.Drawing.Point(6, 51);
      this.lblDatasets.Name = "lblDatasets";
      this.lblDatasets.Size = new System.Drawing.Size(52, 13);
      this.lblDatasets.TabIndex = 5;
      this.lblDatasets.Text = "Datasets:";
      // 
      // lstDeatureClasses
      // 
      this.lstDeatureClasses.FormattingEnabled = true;
      this.lstDeatureClasses.Location = new System.Drawing.Point(6, 67);
      this.lstDeatureClasses.Name = "lstDeatureClasses";
      this.lstDeatureClasses.Size = new System.Drawing.Size(266, 173);
      this.lstDeatureClasses.TabIndex = 4;
      this.lstDeatureClasses.DoubleClick += new System.EventHandler(this.lstDeatureClasses_DoubleClick);
      // 
      // txtPath
      // 
      this.txtPath.Location = new System.Drawing.Point(6, 19);
      this.txtPath.Name = "txtPath";
      this.txtPath.ReadOnly = true;
      this.txtPath.Size = new System.Drawing.Size(237, 20);
      this.txtPath.TabIndex = 3;
      // 
      // OpenSimplePointDlg
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(304, 312);
      this.Controls.Add(this.groupBox1);
      this.Controls.Add(this.btnOK);
      this.Controls.Add(this.btnCancel);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
      this.Name = "OpenSimplePointDlg";
      this.ShowInTaskbar = false;
      this.Text = "OpenSimplePointDlg";
      this.TopMost = true;
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.Button btnOK;
    private System.Windows.Forms.Button btnOpenDataSource;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.ToolTip toolTip1;
    private System.Windows.Forms.TextBox txtPath;
    private System.Windows.Forms.ListBox lstDeatureClasses;
    private System.Windows.Forms.Label lblDatasets;
  }
}