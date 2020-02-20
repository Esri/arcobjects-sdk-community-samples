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
namespace EditingUsingCustomForm
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
          this.axMapControl1 = new ESRI.ArcGIS.Controls.AxMapControl();
          this.axTOCControl1 = new ESRI.ArcGIS.Controls.AxTOCControl();
          this.axLicenseControl1 = new ESRI.ArcGIS.Controls.AxLicenseControl();
          this.splitter1 = new System.Windows.Forms.Splitter();
          this.axEditorToolbar = new ESRI.ArcGIS.Controls.AxToolbarControl();
          this.axNavigationToolbar = new ESRI.ArcGIS.Controls.AxToolbarControl();
          ((System.ComponentModel.ISupportInitialize)(this.axMapControl1)).BeginInit();
          ((System.ComponentModel.ISupportInitialize)(this.axTOCControl1)).BeginInit();
          ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).BeginInit();
          ((System.ComponentModel.ISupportInitialize)(this.axEditorToolbar)).BeginInit();
          ((System.ComponentModel.ISupportInitialize)(this.axNavigationToolbar)).BeginInit();
          this.SuspendLayout();
          // 
          // axMapControl1
          // 
          this.axMapControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                      | System.Windows.Forms.AnchorStyles.Left)
                      | System.Windows.Forms.AnchorStyles.Right)));
          this.axMapControl1.Location = new System.Drawing.Point(250, 0);
          this.axMapControl1.Name = "axMapControl1";
          this.axMapControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axMapControl1.OcxState")));
          this.axMapControl1.Size = new System.Drawing.Size(724, 586);
          this.axMapControl1.TabIndex = 2;
          // 
          // axTOCControl1
          // 
          this.axTOCControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                      | System.Windows.Forms.AnchorStyles.Left)));
          this.axTOCControl1.Location = new System.Drawing.Point(0, 32);
          this.axTOCControl1.Name = "axTOCControl1";
          this.axTOCControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axTOCControl1.OcxState")));
          this.axTOCControl1.Size = new System.Drawing.Size(226, 668);
          this.axTOCControl1.TabIndex = 4;
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
          // splitter1
          // 
          this.splitter1.Location = new System.Drawing.Point(0, 0);
          this.splitter1.Name = "splitter1";
          this.splitter1.Size = new System.Drawing.Size(3, 586);
          this.splitter1.TabIndex = 6;
          this.splitter1.TabStop = false;
          // 
          // axEditorToolbar
          // 
          this.axEditorToolbar.Location = new System.Drawing.Point(0, 0);
          this.axEditorToolbar.Margin = new System.Windows.Forms.Padding(2);
          this.axEditorToolbar.Name = "axEditorToolbar";
          this.axEditorToolbar.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axEditorToolbar.OcxState")));
          this.axEditorToolbar.Size = new System.Drawing.Size(226, 32);
          this.axEditorToolbar.TabIndex = 8;
          // 
          // axNavigationToolbar
          // 
          this.axNavigationToolbar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                      | System.Windows.Forms.AnchorStyles.Left)));
          this.axNavigationToolbar.Location = new System.Drawing.Point(220, 0);
          this.axNavigationToolbar.Margin = new System.Windows.Forms.Padding(2);
          this.axNavigationToolbar.Name = "axNavigationToolbar";
          this.axNavigationToolbar.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axNavigationToolbar.OcxState")));
          this.axNavigationToolbar.Size = new System.Drawing.Size(32, 658);
          this.axNavigationToolbar.TabIndex = 9;
          // 
          // MainForm
          // 
          this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
          this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
          this.ClientSize = new System.Drawing.Size(974, 586);
          this.Controls.Add(this.axNavigationToolbar);
          this.Controls.Add(this.axEditorToolbar);
          this.Controls.Add(this.axLicenseControl1);
          this.Controls.Add(this.axMapControl1);
          this.Controls.Add(this.axTOCControl1);
          this.Controls.Add(this.splitter1);
          this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
          this.Name = "MainForm";
          this.Text = "Custom Editing Application";
          this.Shown += new System.EventHandler(this.MainForm_Shown);
          this.Load += new System.EventHandler(this.MainForm_Load);
          ((System.ComponentModel.ISupportInitialize)(this.axMapControl1)).EndInit();
          ((System.ComponentModel.ISupportInitialize)(this.axTOCControl1)).EndInit();
          ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).EndInit();
          ((System.ComponentModel.ISupportInitialize)(this.axEditorToolbar)).EndInit();
          ((System.ComponentModel.ISupportInitialize)(this.axNavigationToolbar)).EndInit();
          this.ResumeLayout(false);

        }

        #endregion

        private ESRI.ArcGIS.Controls.AxMapControl axMapControl1;
        private ESRI.ArcGIS.Controls.AxTOCControl axTOCControl1;
        private ESRI.ArcGIS.Controls.AxLicenseControl axLicenseControl1;
        private System.Windows.Forms.Splitter splitter1;
        private ESRI.ArcGIS.Controls.AxToolbarControl axEditorToolbar;
        private ESRI.ArcGIS.Controls.AxToolbarControl axNavigationToolbar;
    }
}

