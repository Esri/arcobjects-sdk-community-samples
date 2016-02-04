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
namespace EditingApplication
{
    partial class EditingForm
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
          System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditingForm));
          this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
          this.axMapControl1 = new ESRI.ArcGIS.Controls.AxMapControl();
          this.axToolbarControl1 = new ESRI.ArcGIS.Controls.AxToolbarControl();
          this.axEditorToolbar = new ESRI.ArcGIS.Controls.AxToolbarControl();
          this.axTOCControl1 = new ESRI.ArcGIS.Controls.AxTOCControl();
          this.axLicenseControl1 = new ESRI.ArcGIS.Controls.AxLicenseControl();
          this.tableLayoutPanel1.SuspendLayout();
          ((System.ComponentModel.ISupportInitialize)(this.axMapControl1)).BeginInit();
          ((System.ComponentModel.ISupportInitialize)(this.axToolbarControl1)).BeginInit();
          ((System.ComponentModel.ISupportInitialize)(this.axEditorToolbar)).BeginInit();
          ((System.ComponentModel.ISupportInitialize)(this.axTOCControl1)).BeginInit();
          ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).BeginInit();
          this.SuspendLayout();
          // 
          // tableLayoutPanel1
          // 
          this.tableLayoutPanel1.ColumnCount = 2;
          this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 26.94611F));
          this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 73.05389F));
          this.tableLayoutPanel1.Controls.Add(this.axMapControl1, 1, 2);
          this.tableLayoutPanel1.Controls.Add(this.axToolbarControl1, 0, 1);
          this.tableLayoutPanel1.Controls.Add(this.axEditorToolbar, 0, 0);
          this.tableLayoutPanel1.Controls.Add(this.axTOCControl1, 0, 2);
          this.tableLayoutPanel1.Location = new System.Drawing.Point(0, -3);
          this.tableLayoutPanel1.Name = "tableLayoutPanel1";
          this.tableLayoutPanel1.RowCount = 4;
          this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
          this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.695652F));
          this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 91.30434F));
          this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 8F));
          this.tableLayoutPanel1.Size = new System.Drawing.Size(929, 571);
          this.tableLayoutPanel1.TabIndex = 1;
          // 
          // axMapControl1
          // 
          this.axMapControl1.Dock = System.Windows.Forms.DockStyle.Fill;
          this.axMapControl1.Location = new System.Drawing.Point(253, 81);
          this.axMapControl1.Name = "axMapControl1";
          this.axMapControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axMapControl1.OcxState")));
          this.tableLayoutPanel1.SetRowSpan(this.axMapControl1, 2);
          this.axMapControl1.Size = new System.Drawing.Size(673, 487);
          this.axMapControl1.TabIndex = 1;
          this.axMapControl1.OnMouseDown += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMouseDownEventHandler(this.axMapControl1_OnMouseDown);
          // 
          // axToolbarControl1
          // 
          this.tableLayoutPanel1.SetColumnSpan(this.axToolbarControl1, 2);
          this.axToolbarControl1.Location = new System.Drawing.Point(3, 35);
          this.axToolbarControl1.Name = "axToolbarControl1";
          this.axToolbarControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axToolbarControl1.OcxState")));
          this.axToolbarControl1.Size = new System.Drawing.Size(923, 28);
          this.axToolbarControl1.TabIndex = 4;
          // 
          // axEditorToolbar
          // 
          this.tableLayoutPanel1.SetColumnSpan(this.axEditorToolbar, 2);
          this.axEditorToolbar.Location = new System.Drawing.Point(3, 3);
          this.axEditorToolbar.Name = "axEditorToolbar";
          this.axEditorToolbar.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axEditorToolbar.OcxState")));
          this.axEditorToolbar.Size = new System.Drawing.Size(923, 28);
          this.axEditorToolbar.TabIndex = 5;
          // 
          // axTOCControl1
          // 
          this.axTOCControl1.Location = new System.Drawing.Point(3, 81);
          this.axTOCControl1.Name = "axTOCControl1";
          this.axTOCControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axTOCControl1.OcxState")));
          this.axTOCControl1.Size = new System.Drawing.Size(216, 478);
          this.axTOCControl1.TabIndex = 2;
          // 
          // axLicenseControl1
          // 
          this.axLicenseControl1.Enabled = true;
          this.axLicenseControl1.Location = new System.Drawing.Point(239, 476);
          this.axLicenseControl1.Name = "axLicenseControl1";
          this.axLicenseControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axLicenseControl1.OcxState")));
          this.axLicenseControl1.Size = new System.Drawing.Size(32, 32);
          this.axLicenseControl1.TabIndex = 2;
          // 
          // EditingForm
          // 
          this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
          this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
          this.BackColor = System.Drawing.Color.White;
          this.ClientSize = new System.Drawing.Size(935, 575);
          this.Controls.Add(this.axLicenseControl1);
          this.Controls.Add(this.tableLayoutPanel1);
          this.Name = "EditingForm";
          this.Text = "Snap Environment Sample (C#)";
          this.Load += new System.EventHandler(this.EngineEditingForm_Load);
          this.tableLayoutPanel1.ResumeLayout(false);
          ((System.ComponentModel.ISupportInitialize)(this.axMapControl1)).EndInit();
          ((System.ComponentModel.ISupportInitialize)(this.axToolbarControl1)).EndInit();
          ((System.ComponentModel.ISupportInitialize)(this.axEditorToolbar)).EndInit();
          ((System.ComponentModel.ISupportInitialize)(this.axTOCControl1)).EndInit();
          ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).EndInit();
          this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
      
        private ESRI.ArcGIS.Controls.AxMapControl axMapControl1;
        private ESRI.ArcGIS.Controls.AxTOCControl axTOCControl1;
        private ESRI.ArcGIS.Controls.AxLicenseControl axLicenseControl1;
        private ESRI.ArcGIS.Controls.AxToolbarControl axToolbarControl1;
      private ESRI.ArcGIS.Controls.AxToolbarControl axEditorToolbar;
        
    }
}

