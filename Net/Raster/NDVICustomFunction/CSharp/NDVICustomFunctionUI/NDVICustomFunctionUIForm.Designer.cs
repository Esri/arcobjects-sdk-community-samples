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
namespace NDVICustomFunctionUI
{
    partial class NDVICustomFunctionUIForm
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
            this.inputRasterBtn = new System.Windows.Forms.Button();
            this.inputRasterTxtbox = new System.Windows.Forms.TextBox();
            this.inputRasterLbl = new System.Windows.Forms.Label();
            this.BandIndicesTxtBox = new System.Windows.Forms.TextBox();
            this.HintLbl = new System.Windows.Forms.Label();
            this.BandIndicesLbl = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // inputRasterBtn
            // 
            this.inputRasterBtn.Location = new System.Drawing.Point(347, 51);
            this.inputRasterBtn.Name = "inputRasterBtn";
            this.inputRasterBtn.Size = new System.Drawing.Size(31, 23);
            this.inputRasterBtn.TabIndex = 12;
            this.inputRasterBtn.Text = "...";
            this.inputRasterBtn.UseVisualStyleBackColor = true;
            // 
            // inputRasterTxtbox
            // 
            this.inputRasterTxtbox.Location = new System.Drawing.Point(154, 54);
            this.inputRasterTxtbox.Name = "inputRasterTxtbox";
            this.inputRasterTxtbox.Size = new System.Drawing.Size(186, 20);
            this.inputRasterTxtbox.TabIndex = 11;
            // 
            // inputRasterLbl
            // 
            this.inputRasterLbl.AutoSize = true;
            this.inputRasterLbl.Location = new System.Drawing.Point(22, 61);
            this.inputRasterLbl.Name = "inputRasterLbl";
            this.inputRasterLbl.Size = new System.Drawing.Size(68, 13);
            this.inputRasterLbl.TabIndex = 10;
            this.inputRasterLbl.Text = "Input Raster:";
            // 
            // BandIndicesTxtBox
            // 
            this.BandIndicesTxtBox.Location = new System.Drawing.Point(154, 123);
            this.BandIndicesTxtBox.Name = "BandIndicesTxtBox";
            this.BandIndicesTxtBox.Size = new System.Drawing.Size(186, 20);
            this.BandIndicesTxtBox.TabIndex = 15;
            this.BandIndicesTxtBox.TextChanged += new System.EventHandler(this.BandIndicesTxtBox_TextChanged);
            // 
            // HintLbl
            // 
            this.HintLbl.AutoSize = true;
            this.HintLbl.Location = new System.Drawing.Point(151, 107);
            this.HintLbl.Name = "HintLbl";
            this.HintLbl.Size = new System.Drawing.Size(0, 13);
            this.HintLbl.TabIndex = 14;
            // 
            // BandIndicesLbl
            // 
            this.BandIndicesLbl.AutoSize = true;
            this.BandIndicesLbl.Location = new System.Drawing.Point(22, 130);
            this.BandIndicesLbl.Name = "BandIndicesLbl";
            this.BandIndicesLbl.Size = new System.Drawing.Size(69, 13);
            this.BandIndicesLbl.TabIndex = 13;
            this.BandIndicesLbl.Text = "Band Indices";
            // 
            // NDVICustomFunctionUIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(402, 197);
            this.Controls.Add(this.BandIndicesTxtBox);
            this.Controls.Add(this.HintLbl);
            this.Controls.Add(this.BandIndicesLbl);
            this.Controls.Add(this.inputRasterBtn);
            this.Controls.Add(this.inputRasterTxtbox);
            this.Controls.Add(this.inputRasterLbl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "NDVICustomFunctionUIForm";
            this.Text = "NDVICustomFunction";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button inputRasterBtn;
        private System.Windows.Forms.TextBox inputRasterTxtbox;
        private System.Windows.Forms.Label inputRasterLbl;
        private System.Windows.Forms.TextBox BandIndicesTxtBox;
        private System.Windows.Forms.Label HintLbl;
        private System.Windows.Forms.Label BandIndicesLbl;
    }
}

