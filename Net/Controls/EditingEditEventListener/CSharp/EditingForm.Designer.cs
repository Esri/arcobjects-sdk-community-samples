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
namespace EditingSampleApp
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
          this.axEditorToolbar = new ESRI.ArcGIS.Controls.AxToolbarControl();
          this.axMapControl1 = new ESRI.ArcGIS.Controls.AxMapControl();
          this.axToolbarControl1 = new ESRI.ArcGIS.Controls.AxToolbarControl();
          this.axTOCControl1 = new ESRI.ArcGIS.Controls.AxTOCControl();
          this.eventTabControl = new System.Windows.Forms.TabControl();
          this.tabPage1 = new System.Windows.Forms.TabPage();
          this.clearEventList = new System.Windows.Forms.Button();
          this.lstEditorEvents = new System.Windows.Forms.ListBox();
          this.tabPage2 = new System.Windows.Forms.TabPage();
          this.chkAllOff = new System.Windows.Forms.Button();
          this.chkAllOn = new System.Windows.Forms.Button();
          this.chkListEvent = new System.Windows.Forms.CheckedListBox();
          this.axLicenseControl1 = new ESRI.ArcGIS.Controls.AxLicenseControl();
          this.tableLayoutPanel1.SuspendLayout();
          ((System.ComponentModel.ISupportInitialize)(this.axEditorToolbar)).BeginInit();
          ((System.ComponentModel.ISupportInitialize)(this.axMapControl1)).BeginInit();
          ((System.ComponentModel.ISupportInitialize)(this.axToolbarControl1)).BeginInit();
          ((System.ComponentModel.ISupportInitialize)(this.axTOCControl1)).BeginInit();
          this.eventTabControl.SuspendLayout();
          this.tabPage1.SuspendLayout();
          this.tabPage2.SuspendLayout();
          ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).BeginInit();
          this.SuspendLayout();
          // 
          // tableLayoutPanel1
          // 
          this.tableLayoutPanel1.ColumnCount = 3;
          this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 47.52066F));
          this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 52.47934F));
          this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 426F));
          this.tableLayoutPanel1.Controls.Add(this.axEditorToolbar, 0, 1);
          this.tableLayoutPanel1.Controls.Add(this.axMapControl1, 2, 3);
          this.tableLayoutPanel1.Controls.Add(this.axToolbarControl1, 0, 0);
          this.tableLayoutPanel1.Controls.Add(this.axTOCControl1, 0, 3);
          this.tableLayoutPanel1.Controls.Add(this.eventTabControl, 1, 3);
          this.tableLayoutPanel1.Location = new System.Drawing.Point(0, -3);
          this.tableLayoutPanel1.Name = "tableLayoutPanel1";
          this.tableLayoutPanel1.RowCount = 5;
          this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
          this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.88811F));
          this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 11F));
          this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 88.11189F));
          this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 319F));
          this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
          this.tableLayoutPanel1.Size = new System.Drawing.Size(905, 547);
          this.tableLayoutPanel1.TabIndex = 1;
          // 
          // axEditorToolbar
          // 
          this.tableLayoutPanel1.SetColumnSpan(this.axEditorToolbar, 3);
          this.axEditorToolbar.Dock = System.Windows.Forms.DockStyle.Top;
          this.axEditorToolbar.Location = new System.Drawing.Point(3, 32);
          this.axEditorToolbar.Name = "axEditorToolbar";
          this.axEditorToolbar.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axEditorToolbar.OcxState")));
          this.axEditorToolbar.Size = new System.Drawing.Size(899, 28);
          this.axEditorToolbar.TabIndex = 8;
          // 
          // axMapControl1
          // 
          this.axMapControl1.Dock = System.Windows.Forms.DockStyle.Fill;
          this.axMapControl1.Location = new System.Drawing.Point(481, 65);
          this.axMapControl1.Name = "axMapControl1";
          this.axMapControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axMapControl1.OcxState")));
          this.tableLayoutPanel1.SetRowSpan(this.axMapControl1, 2);
          this.axMapControl1.Size = new System.Drawing.Size(421, 479);
          this.axMapControl1.TabIndex = 1;
          this.axMapControl1.OnMouseUp += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMouseUpEventHandler(this.axMapControl1_OnMouseUp);
          // 
          // axToolbarControl1
          // 
          this.tableLayoutPanel1.SetColumnSpan(this.axToolbarControl1, 3);
          this.axToolbarControl1.Dock = System.Windows.Forms.DockStyle.Top;
          this.axToolbarControl1.Location = new System.Drawing.Point(3, 3);
          this.axToolbarControl1.Name = "axToolbarControl1";
          this.axToolbarControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axToolbarControl1.OcxState")));
          this.axToolbarControl1.Size = new System.Drawing.Size(899, 28);
          this.axToolbarControl1.TabIndex = 4;
          // 
          // axTOCControl1
          // 
          this.axTOCControl1.Dock = System.Windows.Forms.DockStyle.Fill;
          this.axTOCControl1.Location = new System.Drawing.Point(3, 65);
          this.axTOCControl1.Name = "axTOCControl1";
          this.axTOCControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axTOCControl1.OcxState")));
          this.tableLayoutPanel1.SetRowSpan(this.axTOCControl1, 2);
          this.axTOCControl1.Size = new System.Drawing.Size(221, 479);
          this.axTOCControl1.TabIndex = 2;
          // 
          // eventTabControl
          // 
          this.eventTabControl.Controls.Add(this.tabPage1);
          this.eventTabControl.Controls.Add(this.tabPage2);
          this.eventTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
          this.eventTabControl.Location = new System.Drawing.Point(230, 65);
          this.eventTabControl.Name = "eventTabControl";
          this.tableLayoutPanel1.SetRowSpan(this.eventTabControl, 2);
          this.eventTabControl.SelectedIndex = 0;
          this.eventTabControl.Size = new System.Drawing.Size(245, 479);
          this.eventTabControl.TabIndex = 7;
          // 
          // tabPage1
          // 
          this.tabPage1.Controls.Add(this.clearEventList);
          this.tabPage1.Controls.Add(this.lstEditorEvents);
          this.tabPage1.Location = new System.Drawing.Point(4, 22);
          this.tabPage1.Name = "tabPage1";
          this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
          this.tabPage1.Size = new System.Drawing.Size(237, 453);
          this.tabPage1.TabIndex = 0;
          this.tabPage1.Text = "Events Listener";
          this.tabPage1.UseVisualStyleBackColor = true;
          // 
          // clearEventList
          // 
          this.clearEventList.Location = new System.Drawing.Point(7, 365);
          this.clearEventList.Name = "clearEventList";
          this.clearEventList.Size = new System.Drawing.Size(75, 23);
          this.clearEventList.TabIndex = 1;
          this.clearEventList.Text = "Clear Events";
          this.clearEventList.UseVisualStyleBackColor = true;
          this.clearEventList.Click += new System.EventHandler(this.clearEventList_Click);
          // 
          // lstEditorEvents
          // 
          this.lstEditorEvents.Dock = System.Windows.Forms.DockStyle.Top;
          this.lstEditorEvents.FormattingEnabled = true;
          this.lstEditorEvents.Location = new System.Drawing.Point(3, 3);
          this.lstEditorEvents.Name = "lstEditorEvents";
          this.lstEditorEvents.SelectionMode = System.Windows.Forms.SelectionMode.None;
          this.lstEditorEvents.Size = new System.Drawing.Size(231, 355);
          this.lstEditorEvents.TabIndex = 0;
          // 
          // tabPage2
          // 
          this.tabPage2.Controls.Add(this.chkAllOff);
          this.tabPage2.Controls.Add(this.chkAllOn);
          this.tabPage2.Controls.Add(this.chkListEvent);
          this.tabPage2.Location = new System.Drawing.Point(4, 22);
          this.tabPage2.Name = "tabPage2";
          this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
          this.tabPage2.Size = new System.Drawing.Size(237, 453);
          this.tabPage2.TabIndex = 1;
          this.tabPage2.Text = "Select Events";
          this.tabPage2.UseVisualStyleBackColor = true;
          // 
          // chkAllOff
          // 
          this.chkAllOff.Location = new System.Drawing.Point(7, 389);
          this.chkAllOff.Name = "chkAllOff";
          this.chkAllOff.Size = new System.Drawing.Size(75, 23);
          this.chkAllOff.TabIndex = 3;
          this.chkAllOff.Text = "De-select All";
          this.chkAllOff.UseVisualStyleBackColor = true;
          this.chkAllOff.Click += new System.EventHandler(this.chkAllOff_Click);
          // 
          // chkAllOn
          // 
          this.chkAllOn.Location = new System.Drawing.Point(7, 359);
          this.chkAllOn.Name = "chkAllOn";
          this.chkAllOn.Size = new System.Drawing.Size(75, 23);
          this.chkAllOn.TabIndex = 2;
          this.chkAllOn.Text = "Select All";
          this.chkAllOn.UseVisualStyleBackColor = true;
          this.chkAllOn.Click += new System.EventHandler(this.chkAllOn_Click);
          // 
          // chkListEvent
          // 
          this.chkListEvent.CheckOnClick = true;
          this.chkListEvent.Dock = System.Windows.Forms.DockStyle.Top;
          this.chkListEvent.FormattingEnabled = true;
          this.chkListEvent.Location = new System.Drawing.Point(3, 3);
          this.chkListEvent.Name = "chkListEvent";
          this.chkListEvent.Size = new System.Drawing.Size(231, 349);
          this.chkListEvent.TabIndex = 1;
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
          this.ClientSize = new System.Drawing.Size(911, 549);
          this.Controls.Add(this.axLicenseControl1);
          this.Controls.Add(this.tableLayoutPanel1);
          this.Name = "EditingForm";
          this.Text = "Engine Edit Events Listener Sample (C#)";
          this.Load += new System.EventHandler(this.EngineEditingForm_Load);
          this.tableLayoutPanel1.ResumeLayout(false);
          ((System.ComponentModel.ISupportInitialize)(this.axEditorToolbar)).EndInit();
          ((System.ComponentModel.ISupportInitialize)(this.axMapControl1)).EndInit();
          ((System.ComponentModel.ISupportInitialize)(this.axToolbarControl1)).EndInit();
          ((System.ComponentModel.ISupportInitialize)(this.axTOCControl1)).EndInit();
          this.eventTabControl.ResumeLayout(false);
          this.tabPage1.ResumeLayout(false);
          this.tabPage2.ResumeLayout(false);
          ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).EndInit();
          this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
      
        private ESRI.ArcGIS.Controls.AxMapControl axMapControl1;
        private ESRI.ArcGIS.Controls.AxTOCControl axTOCControl1;
        private ESRI.ArcGIS.Controls.AxLicenseControl axLicenseControl1;
      private ESRI.ArcGIS.Controls.AxToolbarControl axToolbarControl1;
      private System.Windows.Forms.TabControl eventTabControl;
      private System.Windows.Forms.TabPage tabPage1;
      private System.Windows.Forms.TabPage tabPage2;
      private ESRI.ArcGIS.Controls.AxToolbarControl axEditorToolbar;
      private System.Windows.Forms.CheckedListBox chkListEvent;
      private System.Windows.Forms.ListBox lstEditorEvents;
      private System.Windows.Forms.Button chkAllOff;
      private System.Windows.Forms.Button chkAllOn;
      private System.Windows.Forms.Button clearEventList;
        
    }
}

