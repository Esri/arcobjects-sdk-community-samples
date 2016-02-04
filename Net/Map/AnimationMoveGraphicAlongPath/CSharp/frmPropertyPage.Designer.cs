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
namespace AnimationDeveloperSamples
{
    partial class frmPropertyPage
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
            this.checkBoxTrace = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // checkBoxTrace
            // 
            this.checkBoxTrace.AutoSize = true;
            this.checkBoxTrace.Location = new System.Drawing.Point(6, 35);
            this.checkBoxTrace.Name = "checkBoxTrace";
            this.checkBoxTrace.Size = new System.Drawing.Size(185, 17);
            this.checkBoxTrace.TabIndex = 0;
            this.checkBoxTrace.Text = "Show trace of the moving graphic";
            this.checkBoxTrace.UseVisualStyleBackColor = true;
            this.checkBoxTrace.Click += new System.EventHandler(this.checkBoxTrace_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBoxTrace);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(301, 101);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Trace Properties";
            // 
            // frmPropertyPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(325, 139);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.HelpButton = true;
            this.Name = "frmPropertyPage";
            this.Text = "Graphic Track Properties";
            this.Load += new System.EventHandler(this.frmPropertyPage_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBoxTrace;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.HelpProvider helpProvider1;
    }
}