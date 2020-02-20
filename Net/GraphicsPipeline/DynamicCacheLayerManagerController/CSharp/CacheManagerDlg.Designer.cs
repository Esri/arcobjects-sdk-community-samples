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
namespace DynamicCacheLayerManagerController
{
  partial class CacheManagerDlg
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
      this.lblLayer = new System.Windows.Forms.Label();
      this.cboLayerNames = new System.Windows.Forms.ComboBox();
      this.btnOK = new System.Windows.Forms.Button();
      this.btnApply = new System.Windows.Forms.Button();
      this.btnDismiss = new System.Windows.Forms.Button();
      this.groupDrawingProps = new System.Windows.Forms.GroupBox();
      this.numDetaildThreshold = new System.Windows.Forms.NumericUpDown();
      this.lblDetailsThreshold = new System.Windows.Forms.Label();
      this.chkAlwaysDrawCoarsestLevel = new System.Windows.Forms.CheckBox();
      this.lblUseDefaultTexture = new System.Windows.Forms.Label();
      this.lblCacheFolderName = new System.Windows.Forms.Label();
      this.lblFolderName = new System.Windows.Forms.Label();
      this.btnFolderPath = new System.Windows.Forms.Button();
      this.lblCacheFolderPath = new System.Windows.Forms.Label();
      this.lblCachePathName = new System.Windows.Forms.Label();
      this.btnRestoreDefaults = new System.Windows.Forms.Button();
      this.numMaxCacheScale = new System.Windows.Forms.NumericUpDown();
      this.lblMaxCacheScale = new System.Windows.Forms.Label();
      this.chkStrictOnDemandMode = new System.Windows.Forms.CheckBox();
      this.lblStrictOnDemandMode = new System.Windows.Forms.Label();
      this.numProgressiveFetchingLevels = new System.Windows.Forms.NumericUpDown();
      this.lblProgressiveFetchingLevels = new System.Windows.Forms.Label();
      this.numProgressiveDrawingLevels = new System.Windows.Forms.NumericUpDown();
      this.lblProgressiveDrawingLevels = new System.Windows.Forms.Label();
      this.rdoJPEG = new System.Windows.Forms.RadioButton();
      this.rdoPNG = new System.Windows.Forms.RadioButton();
      this.label1 = new System.Windows.Forms.Label();
      this.groupDrawingProps.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.numDetaildThreshold)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.numMaxCacheScale)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.numProgressiveFetchingLevels)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.numProgressiveDrawingLevels)).BeginInit();
      this.SuspendLayout();
      // 
      // lblLayer
      // 
      this.lblLayer.AutoSize = true;
      this.lblLayer.Location = new System.Drawing.Point(13, 13);
      this.lblLayer.Name = "lblLayer";
      this.lblLayer.Size = new System.Drawing.Size(69, 13);
      this.lblLayer.TabIndex = 0;
      this.lblLayer.Text = "Active Layer:";
      // 
      // cboLayerNames
      // 
      this.cboLayerNames.FormattingEnabled = true;
      this.cboLayerNames.Location = new System.Drawing.Point(88, 10);
      this.cboLayerNames.Name = "cboLayerNames";
      this.cboLayerNames.Size = new System.Drawing.Size(327, 21);
      this.cboLayerNames.TabIndex = 1;
      this.cboLayerNames.SelectedIndexChanged += new System.EventHandler(this.cboLayerNames_SelectedIndexChanged);
      // 
      // btnOK
      // 
      this.btnOK.Location = new System.Drawing.Point(12, 632);
      this.btnOK.Name = "btnOK";
      this.btnOK.Size = new System.Drawing.Size(75, 23);
      this.btnOK.TabIndex = 2;
      this.btnOK.Text = "OK";
      this.btnOK.UseVisualStyleBackColor = true;
      this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
      // 
      // btnApply
      // 
      this.btnApply.Location = new System.Drawing.Point(177, 632);
      this.btnApply.Name = "btnApply";
      this.btnApply.Size = new System.Drawing.Size(75, 23);
      this.btnApply.TabIndex = 3;
      this.btnApply.Text = "Apply";
      this.btnApply.UseVisualStyleBackColor = true;
      this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
      // 
      // btnDismiss
      // 
      this.btnDismiss.Location = new System.Drawing.Point(340, 632);
      this.btnDismiss.Name = "btnDismiss";
      this.btnDismiss.Size = new System.Drawing.Size(75, 23);
      this.btnDismiss.TabIndex = 4;
      this.btnDismiss.Text = "Dismiss";
      this.btnDismiss.UseVisualStyleBackColor = true;
      this.btnDismiss.Click += new System.EventHandler(this.btnDismiss_Click);
      // 
      // groupDrawingProps
      // 
      this.groupDrawingProps.Controls.Add(this.numDetaildThreshold);
      this.groupDrawingProps.Controls.Add(this.lblDetailsThreshold);
      this.groupDrawingProps.Controls.Add(this.chkAlwaysDrawCoarsestLevel);
      this.groupDrawingProps.Controls.Add(this.lblUseDefaultTexture);
      this.groupDrawingProps.Controls.Add(this.lblCacheFolderName);
      this.groupDrawingProps.Controls.Add(this.lblFolderName);
      this.groupDrawingProps.Controls.Add(this.btnFolderPath);
      this.groupDrawingProps.Controls.Add(this.lblCacheFolderPath);
      this.groupDrawingProps.Controls.Add(this.lblCachePathName);
      this.groupDrawingProps.Controls.Add(this.btnRestoreDefaults);
      this.groupDrawingProps.Controls.Add(this.numMaxCacheScale);
      this.groupDrawingProps.Controls.Add(this.lblMaxCacheScale);
      this.groupDrawingProps.Controls.Add(this.chkStrictOnDemandMode);
      this.groupDrawingProps.Controls.Add(this.lblStrictOnDemandMode);
      this.groupDrawingProps.Controls.Add(this.numProgressiveFetchingLevels);
      this.groupDrawingProps.Controls.Add(this.lblProgressiveFetchingLevels);
      this.groupDrawingProps.Controls.Add(this.numProgressiveDrawingLevels);
      this.groupDrawingProps.Controls.Add(this.lblProgressiveDrawingLevels);
      this.groupDrawingProps.Controls.Add(this.rdoJPEG);
      this.groupDrawingProps.Controls.Add(this.rdoPNG);
      this.groupDrawingProps.Controls.Add(this.label1);
      this.groupDrawingProps.Location = new System.Drawing.Point(12, 37);
      this.groupDrawingProps.Name = "groupDrawingProps";
      this.groupDrawingProps.Size = new System.Drawing.Size(403, 572);
      this.groupDrawingProps.TabIndex = 5;
      this.groupDrawingProps.TabStop = false;
      this.groupDrawingProps.Text = "Cache drawing properties";
      // 
      // numDetaildThreshold
      // 
      this.numDetaildThreshold.DecimalPlaces = 2;
      this.numDetaildThreshold.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
      this.numDetaildThreshold.Location = new System.Drawing.Point(261, 342);
      this.numDetaildThreshold.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.numDetaildThreshold.Name = "numDetaildThreshold";
      this.numDetaildThreshold.Size = new System.Drawing.Size(136, 20);
      this.numDetaildThreshold.TabIndex = 20;
      // 
      // lblDetailsThreshold
      // 
      this.lblDetailsThreshold.AutoSize = true;
      this.lblDetailsThreshold.Location = new System.Drawing.Point(10, 349);
      this.lblDetailsThreshold.Name = "lblDetailsThreshold";
      this.lblDetailsThreshold.Size = new System.Drawing.Size(88, 13);
      this.lblDetailsThreshold.TabIndex = 19;
      this.lblDetailsThreshold.Text = "Details threshold:";
      // 
      // chkAlwaysDrawCoarsestLevel
      // 
      this.chkAlwaysDrawCoarsestLevel.AutoSize = true;
      this.chkAlwaysDrawCoarsestLevel.Location = new System.Drawing.Point(260, 269);
      this.chkAlwaysDrawCoarsestLevel.Name = "chkAlwaysDrawCoarsestLevel";
      this.chkAlwaysDrawCoarsestLevel.Size = new System.Drawing.Size(15, 14);
      this.chkAlwaysDrawCoarsestLevel.TabIndex = 18;
      this.chkAlwaysDrawCoarsestLevel.UseVisualStyleBackColor = true;
      // 
      // lblUseDefaultTexture
      // 
      this.lblUseDefaultTexture.AutoSize = true;
      this.lblUseDefaultTexture.Location = new System.Drawing.Point(10, 269);
      this.lblUseDefaultTexture.Name = "lblUseDefaultTexture";
      this.lblUseDefaultTexture.Size = new System.Drawing.Size(137, 13);
      this.lblUseDefaultTexture.TabIndex = 17;
      this.lblUseDefaultTexture.Text = "Always draw coarsest level:";
      // 
      // lblCacheFolderName
      // 
      this.lblCacheFolderName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.lblCacheFolderName.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.lblCacheFolderName.Location = new System.Drawing.Point(10, 48);
      this.lblCacheFolderName.Name = "lblCacheFolderName";
      this.lblCacheFolderName.Size = new System.Drawing.Size(387, 20);
      this.lblCacheFolderName.TabIndex = 16;
      // 
      // lblFolderName
      // 
      this.lblFolderName.AutoSize = true;
      this.lblFolderName.Location = new System.Drawing.Point(10, 25);
      this.lblFolderName.Name = "lblFolderName";
      this.lblFolderName.Size = new System.Drawing.Size(70, 13);
      this.lblFolderName.TabIndex = 15;
      this.lblFolderName.Text = "Folder Name:";
      // 
      // btnFolderPath
      // 
      this.btnFolderPath.Location = new System.Drawing.Point(369, 112);
      this.btnFolderPath.Name = "btnFolderPath";
      this.btnFolderPath.Size = new System.Drawing.Size(28, 101);
      this.btnFolderPath.TabIndex = 14;
      this.btnFolderPath.Text = "...";
      this.btnFolderPath.UseVisualStyleBackColor = true;
      this.btnFolderPath.Click += new System.EventHandler(this.btnFolderPath_Click);
      // 
      // lblCacheFolderPath
      // 
      this.lblCacheFolderPath.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.lblCacheFolderPath.Location = new System.Drawing.Point(10, 112);
      this.lblCacheFolderPath.Name = "lblCacheFolderPath";
      this.lblCacheFolderPath.Size = new System.Drawing.Size(353, 101);
      this.lblCacheFolderPath.TabIndex = 13;
      this.lblCacheFolderPath.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lblCachePathName
      // 
      this.lblCachePathName.AutoSize = true;
      this.lblCachePathName.Location = new System.Drawing.Point(7, 89);
      this.lblCachePathName.Name = "lblCachePathName";
      this.lblCachePathName.Size = new System.Drawing.Size(63, 13);
      this.lblCachePathName.TabIndex = 12;
      this.lblCachePathName.Text = "Folder path:";
      // 
      // btnRestoreDefaults
      // 
      this.btnRestoreDefaults.Location = new System.Drawing.Point(261, 531);
      this.btnRestoreDefaults.Name = "btnRestoreDefaults";
      this.btnRestoreDefaults.Size = new System.Drawing.Size(136, 23);
      this.btnRestoreDefaults.TabIndex = 11;
      this.btnRestoreDefaults.Text = "Restore Defaults";
      this.btnRestoreDefaults.UseVisualStyleBackColor = true;
      this.btnRestoreDefaults.Click += new System.EventHandler(this.btnRestoreDefaults_Click);
      // 
      // numMaxCacheScale
      // 
      this.numMaxCacheScale.Location = new System.Drawing.Point(261, 491);
      this.numMaxCacheScale.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
      this.numMaxCacheScale.Name = "numMaxCacheScale";
      this.numMaxCacheScale.Size = new System.Drawing.Size(136, 20);
      this.numMaxCacheScale.TabIndex = 10;
      // 
      // lblMaxCacheScale
      // 
      this.lblMaxCacheScale.AutoSize = true;
      this.lblMaxCacheScale.Location = new System.Drawing.Point(10, 493);
      this.lblMaxCacheScale.Name = "lblMaxCacheScale";
      this.lblMaxCacheScale.Size = new System.Drawing.Size(115, 13);
      this.lblMaxCacheScale.TabIndex = 9;
      this.lblMaxCacheScale.Text = "Maximum cache scale:";
      // 
      // chkStrictOnDemandMode
      // 
      this.chkStrictOnDemandMode.AutoSize = true;
      this.chkStrictOnDemandMode.Location = new System.Drawing.Point(261, 307);
      this.chkStrictOnDemandMode.Name = "chkStrictOnDemandMode";
      this.chkStrictOnDemandMode.Size = new System.Drawing.Size(15, 14);
      this.chkStrictOnDemandMode.TabIndex = 8;
      this.chkStrictOnDemandMode.UseVisualStyleBackColor = true;
      // 
      // lblStrictOnDemandMode
      // 
      this.lblStrictOnDemandMode.AutoSize = true;
      this.lblStrictOnDemandMode.Location = new System.Drawing.Point(7, 307);
      this.lblStrictOnDemandMode.Name = "lblStrictOnDemandMode";
      this.lblStrictOnDemandMode.Size = new System.Drawing.Size(119, 13);
      this.lblStrictOnDemandMode.TabIndex = 7;
      this.lblStrictOnDemandMode.Text = "Strict on demand mode:";
      // 
      // numProgressiveFetchingLevels
      // 
      this.numProgressiveFetchingLevels.Location = new System.Drawing.Point(261, 441);
      this.numProgressiveFetchingLevels.Maximum = new decimal(new int[] {
            31,
            0,
            0,
            0});
      this.numProgressiveFetchingLevels.Name = "numProgressiveFetchingLevels";
      this.numProgressiveFetchingLevels.Size = new System.Drawing.Size(136, 20);
      this.numProgressiveFetchingLevels.TabIndex = 6;
      // 
      // lblProgressiveFetchingLevels
      // 
      this.lblProgressiveFetchingLevels.AutoSize = true;
      this.lblProgressiveFetchingLevels.Location = new System.Drawing.Point(7, 441);
      this.lblProgressiveFetchingLevels.Name = "lblProgressiveFetchingLevels";
      this.lblProgressiveFetchingLevels.Size = new System.Drawing.Size(136, 13);
      this.lblProgressiveFetchingLevels.TabIndex = 5;
      this.lblProgressiveFetchingLevels.Text = "Progressive fetching levels:";
      // 
      // numProgressiveDrawingLevels
      // 
      this.numProgressiveDrawingLevels.Location = new System.Drawing.Point(261, 390);
      this.numProgressiveDrawingLevels.Maximum = new decimal(new int[] {
            31,
            0,
            0,
            0});
      this.numProgressiveDrawingLevels.Name = "numProgressiveDrawingLevels";
      this.numProgressiveDrawingLevels.Size = new System.Drawing.Size(136, 20);
      this.numProgressiveDrawingLevels.TabIndex = 4;
      // 
      // lblProgressiveDrawingLevels
      // 
      this.lblProgressiveDrawingLevels.AutoSize = true;
      this.lblProgressiveDrawingLevels.Location = new System.Drawing.Point(7, 390);
      this.lblProgressiveDrawingLevels.Name = "lblProgressiveDrawingLevels";
      this.lblProgressiveDrawingLevels.Size = new System.Drawing.Size(135, 13);
      this.lblProgressiveDrawingLevels.TabIndex = 3;
      this.lblProgressiveDrawingLevels.Text = "Progressive drawing levels:";
      // 
      // rdoJPEG
      // 
      this.rdoJPEG.AutoSize = true;
      this.rdoJPEG.Location = new System.Drawing.Point(345, 231);
      this.rdoJPEG.Name = "rdoJPEG";
      this.rdoJPEG.Size = new System.Drawing.Size(52, 17);
      this.rdoJPEG.TabIndex = 2;
      this.rdoJPEG.TabStop = true;
      this.rdoJPEG.Text = "JPEG";
      this.rdoJPEG.UseVisualStyleBackColor = true;
      // 
      // rdoPNG
      // 
      this.rdoPNG.AutoSize = true;
      this.rdoPNG.Location = new System.Drawing.Point(261, 231);
      this.rdoPNG.Name = "rdoPNG";
      this.rdoPNG.Size = new System.Drawing.Size(48, 17);
      this.rdoPNG.TabIndex = 1;
      this.rdoPNG.TabStop = true;
      this.rdoPNG.Text = "PNG";
      this.rdoPNG.UseVisualStyleBackColor = true;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(7, 231);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(42, 13);
      this.label1.TabIndex = 0;
      this.label1.Text = "Format:";
      // 
      // CacheManagerDlg
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(427, 667);
      this.Controls.Add(this.groupDrawingProps);
      this.Controls.Add(this.btnDismiss);
      this.Controls.Add(this.btnApply);
      this.Controls.Add(this.btnOK);
      this.Controls.Add(this.cboLayerNames);
      this.Controls.Add(this.lblLayer);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
      this.Name = "CacheManagerDlg";
      this.ShowInTaskbar = false;
      this.Text = "CacheManagerDlg";
      this.TopMost = true;
      this.Load += new System.EventHandler(this.CacheManagerDlg_Load);
      this.groupDrawingProps.ResumeLayout(false);
      this.groupDrawingProps.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.numDetaildThreshold)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.numMaxCacheScale)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.numProgressiveFetchingLevels)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.numProgressiveDrawingLevels)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label lblLayer;
    private System.Windows.Forms.ComboBox cboLayerNames;
    private System.Windows.Forms.Button btnOK;
    private System.Windows.Forms.Button btnApply;
    private System.Windows.Forms.Button btnDismiss;
    private System.Windows.Forms.GroupBox groupDrawingProps;
    private System.Windows.Forms.RadioButton rdoJPEG;
    private System.Windows.Forms.RadioButton rdoPNG;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label lblProgressiveFetchingLevels;
    private System.Windows.Forms.NumericUpDown numProgressiveDrawingLevels;
    private System.Windows.Forms.Label lblProgressiveDrawingLevels;
    private System.Windows.Forms.CheckBox chkStrictOnDemandMode;
    private System.Windows.Forms.Label lblStrictOnDemandMode;
    private System.Windows.Forms.NumericUpDown numProgressiveFetchingLevels;
    private System.Windows.Forms.NumericUpDown numMaxCacheScale;
    private System.Windows.Forms.Label lblMaxCacheScale;
    private System.Windows.Forms.Button btnRestoreDefaults;
    private System.Windows.Forms.Label lblCacheFolderPath;
    private System.Windows.Forms.Label lblCachePathName;
    private System.Windows.Forms.Button btnFolderPath;
    private System.Windows.Forms.Label lblCacheFolderName;
    private System.Windows.Forms.Label lblFolderName;
    private System.Windows.Forms.CheckBox chkAlwaysDrawCoarsestLevel;
    private System.Windows.Forms.Label lblUseDefaultTexture;
    private System.Windows.Forms.Label lblDetailsThreshold;
    private System.Windows.Forms.NumericUpDown numDetaildThreshold;
  }
}