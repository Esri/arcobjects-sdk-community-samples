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
using System;
namespace ApplicativeAlgorithmsPageCS
{
    partial class TranslateTreePropPage
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
        }

        #region Windows Form

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnRestore = new System.Windows.Forms.Button();
            this.txtYTrans = new System.Windows.Forms.TextBox();
            this.lblYTrans = new System.Windows.Forms.Label();
            this.txtXTrans = new System.Windows.Forms.TextBox();
            this.lblXTrans = new System.Windows.Forms.Label();
            this.timApply = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // btnRestore
            // 
            this.btnRestore.Location = new System.Drawing.Point(209, 163);
            this.btnRestore.Name = "btnRestore";
            this.btnRestore.Size = new System.Drawing.Size(118, 37);
            this.btnRestore.TabIndex = 4;
            this.btnRestore.Text = "Restore default";
            this.btnRestore.UseVisualStyleBackColor = true;
            this.btnRestore.Click += new System.EventHandler(this.btnRestore_Click);
            // 
            // txtYTrans
            // 
            this.txtYTrans.Location = new System.Drawing.Point(160, 87);
            this.txtYTrans.Name = "txtYTrans";
            this.txtYTrans.Size = new System.Drawing.Size(93, 20);
            this.txtYTrans.TabIndex = 3;
            this.txtYTrans.TextChanged += new System.EventHandler(this.ChangedTexte);
            this.txtYTrans.Enter += new System.EventHandler(this.TexteEnter);
            // 
            // lblYTrans
            // 
            this.lblYTrans.AutoSize = true;
            this.lblYTrans.Location = new System.Drawing.Point(65, 89);
            this.lblYTrans.Name = "lblYTrans";
            this.lblYTrans.Size = new System.Drawing.Size(65, 13);
            this.lblYTrans.TabIndex = 2;
            this.lblYTrans.Text = "Y translation";
            // 
            // txtXTrans
            // 
            this.txtXTrans.Location = new System.Drawing.Point(160, 43);
            this.txtXTrans.Name = "txtXTrans";
            this.txtXTrans.Size = new System.Drawing.Size(93, 20);
            this.txtXTrans.TabIndex = 1;
            this.txtXTrans.TextChanged += new System.EventHandler(this.ChangedTexte);
            this.txtXTrans.Enter += new System.EventHandler(this.TexteEnter);
            // 
            // lblXTrans
            // 
            this.lblXTrans.AutoSize = true;
            this.lblXTrans.Location = new System.Drawing.Point(65, 45);
            this.lblXTrans.Name = "lblXTrans";
            this.lblXTrans.Size = new System.Drawing.Size(65, 13);
            this.lblXTrans.TabIndex = 0;
            this.lblXTrans.Text = "X translation";
            // 
            // TranslateTreePropPage
            // 
            this.AccessibleRole = System.Windows.Forms.AccessibleRole.PropertyPage;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(343, 215);
            this.Controls.Add(this.btnRestore);
            this.Controls.Add(this.txtYTrans);
            this.Controls.Add(this.lblYTrans);
            this.Controls.Add(this.txtXTrans);
            this.Controls.Add(this.lblXTrans);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MinimumSize = new System.Drawing.Size(343, 215);
            this.Name = "TranslateTreePropPage";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Translate Tree";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Button btnRestore;
        internal System.Windows.Forms.TextBox txtYTrans;
        internal System.Windows.Forms.Label lblYTrans;
        internal System.Windows.Forms.TextBox txtXTrans;
        internal System.Windows.Forms.Label lblXTrans;
        private System.Windows.Forms.Timer timApply;
    }
}
