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
namespace DesktopAutomationCS
{
    partial class Form1
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
            this.btnStartApp = new System.Windows.Forms.Button();
            this.btnDrive = new System.Windows.Forms.Button();
            this.btnShutdown = new System.Windows.Forms.Button();
            this.cboApps = new System.Windows.Forms.ComboBox();
            this.txtShapeFilePath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnStartApp
            // 
            this.btnStartApp.Location = new System.Drawing.Point(12, 57);
            this.btnStartApp.Name = "btnStartApp";
            this.btnStartApp.Size = new System.Drawing.Size(290, 23);
            this.btnStartApp.TabIndex = 0;
            this.btnStartApp.Text = "Start";
            this.btnStartApp.UseVisualStyleBackColor = true;
            this.btnStartApp.Click += new System.EventHandler(this.btnStartApp_Click);
            // 
            // btnDrive
            // 
            this.btnDrive.Enabled = false;
            this.btnDrive.Location = new System.Drawing.Point(12, 131);
            this.btnDrive.Name = "btnDrive";
            this.btnDrive.Size = new System.Drawing.Size(290, 23);
            this.btnDrive.TabIndex = 1;
            this.btnDrive.Text = "Add a layer";
            this.btnDrive.UseVisualStyleBackColor = true;
            this.btnDrive.Click += new System.EventHandler(this.btnDrive_Click);
            // 
            // btnShutdown
            // 
            this.btnShutdown.Enabled = false;
            this.btnShutdown.Location = new System.Drawing.Point(12, 184);
            this.btnShutdown.Name = "btnShutdown";
            this.btnShutdown.Size = new System.Drawing.Size(290, 23);
            this.btnShutdown.TabIndex = 2;
            this.btnShutdown.Text = "Exit";
            this.btnShutdown.UseVisualStyleBackColor = true;
            this.btnShutdown.Click += new System.EventHandler(this.btnShutdown_Click);
            // 
            // cboApps
            // 
            this.cboApps.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboApps.FormattingEnabled = true;
            this.cboApps.Items.AddRange(new object[] {
            "ArcMap",
            "ArcScene",
            "ArcGlobe"});
            this.cboApps.Location = new System.Drawing.Point(12, 30);
            this.cboApps.Name = "cboApps";
            this.cboApps.Size = new System.Drawing.Size(290, 21);
            this.cboApps.TabIndex = 3;
            // 
            // txtShapeFilePath
            // 
            this.txtShapeFilePath.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtShapeFilePath.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem;
            this.txtShapeFilePath.Enabled = false;
            this.txtShapeFilePath.Location = new System.Drawing.Point(12, 105);
            this.txtShapeFilePath.Name = "txtShapeFilePath";
            this.txtShapeFilePath.Size = new System.Drawing.Size(290, 20);
            this.txtShapeFilePath.TabIndex = 4;
            this.txtShapeFilePath.TextChanged += new System.EventHandler(this.txtShapeFilePath_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(188, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Choose an ArcGIS Application to start:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 89);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(221, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Enter the shapefile path to add to application:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 168);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(253, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Shut down application (all changes are abandoned):";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(315, 218);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtShapeFilePath);
            this.Controls.Add(this.cboApps);
            this.Controls.Add(this.btnShutdown);
            this.Controls.Add(this.btnDrive);
            this.Controls.Add(this.btnStartApp);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.Text = "Driving Application Form";
            this.TopMost = true;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStartApp;
        private System.Windows.Forms.Button btnDrive;
        private System.Windows.Forms.Button btnShutdown;
        private System.Windows.Forms.ComboBox cboApps;
        private System.Windows.Forms.TextBox txtShapeFilePath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}

