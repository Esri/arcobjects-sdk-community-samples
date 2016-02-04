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
namespace CustomRulesPageCS
{
    partial class frmReductionLink
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
            this.cboReduce = new System.Windows.Forms.ComboBox();
            this.lblReduce = new System.Windows.Forms.Label();
            this.chkUsePort = new System.Windows.Forms.CheckBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cboReduce
            // 
            this.cboReduce.FormattingEnabled = true;
            this.cboReduce.Location = new System.Drawing.Point(12, 85);
            this.cboReduce.Name = "cboReduce";
            this.cboReduce.Size = new System.Drawing.Size(257, 22);
            this.cboReduce.TabIndex = 10;
            this.cboReduce.SelectedIndexChanged += new System.EventHandler(this.Changed);
            this.cboReduce.Click += new System.EventHandler(this.Changed);
            // 
            // lblReduce
            // 
            this.lblReduce.BackColor = System.Drawing.SystemColors.Control;
            this.lblReduce.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblReduce.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblReduce.Location = new System.Drawing.Point(12, 64);
            this.lblReduce.Name = "lblReduce";
            this.lblReduce.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblReduce.Size = new System.Drawing.Size(257, 18);
            this.lblReduce.TabIndex = 9;
            this.lblReduce.Text = "Links to reduce";
            // 
            // chkUsePort
            // 
            this.chkUsePort.AutoSize = true;
            this.chkUsePort.Location = new System.Drawing.Point(13, 126);
            this.chkUsePort.Name = "chkUsePort";
            this.chkUsePort.Size = new System.Drawing.Size(67, 18);
            this.chkUsePort.TabIndex = 11;
            this.chkUsePort.Text = "Use Port";
            this.chkUsePort.UseVisualStyleBackColor = true;
            this.chkUsePort.CheckStateChanged += new System.EventHandler(this.Changed);
            // 
            // txtDescription
            // 
            this.txtDescription.AcceptsReturn = true;
            this.txtDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                                    | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDescription.BackColor = System.Drawing.SystemColors.Window;
            this.txtDescription.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtDescription.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtDescription.Location = new System.Drawing.Point(12, 30);
            this.txtDescription.MaxLength = 0;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtDescription.Size = new System.Drawing.Size(257, 20);
            this.txtDescription.TabIndex = 13;
            this.txtDescription.TextChanged += new System.EventHandler(this.Changed);
            // 
            // lblDescription
            // 
            this.lblDescription.BackColor = System.Drawing.SystemColors.Control;
            this.lblDescription.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblDescription.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblDescription.Location = new System.Drawing.Point(9, 9);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblDescription.Size = new System.Drawing.Size(257, 18);
            this.lblDescription.TabIndex = 12;
            this.lblDescription.Text = "Description";
            // 
            // frmReductionLink
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(283, 153);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.chkUsePort);
            this.Controls.Add(this.cboReduce);
            this.Controls.Add(this.lblReduce);
            this.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmReductionLink";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmReductionLink";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.ComboBox cboReduce;
        public System.Windows.Forms.Label lblReduce;
        public System.Windows.Forms.CheckBox chkUsePort;
        public System.Windows.Forms.TextBox txtDescription;
        public System.Windows.Forms.Label lblDescription;
    }
}