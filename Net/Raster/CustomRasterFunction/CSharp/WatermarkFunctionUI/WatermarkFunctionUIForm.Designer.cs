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
namespace CustomFunctionUI
{
    partial class WatermarkFunctionUIForm
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
            this.inputRasterLbl = new System.Windows.Forms.Label();
            this.watermarkImageLbl = new System.Windows.Forms.Label();
            this.blendPercentLbl = new System.Windows.Forms.Label();
            this.inputRasterTxtbox = new System.Windows.Forms.TextBox();
            this.watermarkImageTxtbox = new System.Windows.Forms.TextBox();
            this.blendPercentTxtbox = new System.Windows.Forms.TextBox();
            this.inputRasterBtn = new System.Windows.Forms.Button();
            this.watermarkImageBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.watermarkImageDlg = new System.Windows.Forms.OpenFileDialog();
            this.LocationLbl = new System.Windows.Forms.Label();
            this.LocationComboBx = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // inputRasterLbl
            // 
            this.inputRasterLbl.AutoSize = true;
            this.inputRasterLbl.Location = new System.Drawing.Point(23, 49);
            this.inputRasterLbl.Name = "inputRasterLbl";
            this.inputRasterLbl.Size = new System.Drawing.Size(68, 13);
            this.inputRasterLbl.TabIndex = 0;
            this.inputRasterLbl.Text = "Input Raster:";
            // 
            // watermarkImageLbl
            // 
            this.watermarkImageLbl.AutoSize = true;
            this.watermarkImageLbl.Location = new System.Drawing.Point(23, 129);
            this.watermarkImageLbl.Name = "watermarkImageLbl";
            this.watermarkImageLbl.Size = new System.Drawing.Size(119, 13);
            this.watermarkImageLbl.TabIndex = 1;
            this.watermarkImageLbl.Text = "Watermark Image Path:";
            // 
            // blendPercentLbl
            // 
            this.blendPercentLbl.AutoSize = true;
            this.blendPercentLbl.Location = new System.Drawing.Point(23, 175);
            this.blendPercentLbl.Name = "blendPercentLbl";
            this.blendPercentLbl.Size = new System.Drawing.Size(95, 13);
            this.blendPercentLbl.TabIndex = 2;
            this.blendPercentLbl.Text = "Blend Percentage:";
            // 
            // inputRasterTxtbox
            // 
            this.inputRasterTxtbox.Location = new System.Drawing.Point(191, 41);
            this.inputRasterTxtbox.Name = "inputRasterTxtbox";
            this.inputRasterTxtbox.Size = new System.Drawing.Size(186, 20);
            this.inputRasterTxtbox.TabIndex = 3;
            // 
            // watermarkImageTxtbox
            // 
            this.watermarkImageTxtbox.Location = new System.Drawing.Point(191, 122);
            this.watermarkImageTxtbox.Name = "watermarkImageTxtbox";
            this.watermarkImageTxtbox.Size = new System.Drawing.Size(186, 20);
            this.watermarkImageTxtbox.TabIndex = 4;
            // 
            // blendPercentTxtbox
            // 
            this.blendPercentTxtbox.Location = new System.Drawing.Point(191, 168);
            this.blendPercentTxtbox.Name = "blendPercentTxtbox";
            this.blendPercentTxtbox.Size = new System.Drawing.Size(186, 20);
            this.blendPercentTxtbox.TabIndex = 5;
            this.blendPercentTxtbox.ModifiedChanged += new System.EventHandler(this.blendPercentTxtbox_ModifiedChanged);
            // 
            // inputRasterBtn
            // 
            this.inputRasterBtn.Location = new System.Drawing.Point(384, 38);
            this.inputRasterBtn.Name = "inputRasterBtn";
            this.inputRasterBtn.Size = new System.Drawing.Size(31, 23);
            this.inputRasterBtn.TabIndex = 6;
            this.inputRasterBtn.Text = "...";
            this.inputRasterBtn.UseVisualStyleBackColor = true;
            this.inputRasterBtn.Click += new System.EventHandler(this.inputRasterBtn_Click);
            // 
            // watermarkImageBtn
            // 
            this.watermarkImageBtn.Location = new System.Drawing.Point(384, 119);
            this.watermarkImageBtn.Name = "watermarkImageBtn";
            this.watermarkImageBtn.Size = new System.Drawing.Size(31, 23);
            this.watermarkImageBtn.TabIndex = 7;
            this.watermarkImageBtn.Text = "...";
            this.watermarkImageBtn.UseVisualStyleBackColor = true;
            this.watermarkImageBtn.Click += new System.EventHandler(this.watermarkImageBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(381, 171);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(15, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "%";
            // 
            // LocationLbl
            // 
            this.LocationLbl.AutoSize = true;
            this.LocationLbl.Location = new System.Drawing.Point(23, 87);
            this.LocationLbl.Name = "LocationLbl";
            this.LocationLbl.Size = new System.Drawing.Size(103, 13);
            this.LocationLbl.TabIndex = 10;
            this.LocationLbl.Text = "Watermark Location";
            // 
            // LocationComboBx
            // 
            this.LocationComboBx.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.LocationComboBx.Items.AddRange(new object[] {
            "Top Left",
            "Top Right",
            "Center",
            "Bottom Left",
            "Bottom Right"});
            this.LocationComboBx.Location = new System.Drawing.Point(191, 79);
            this.LocationComboBx.Name = "LocationComboBx";
            this.LocationComboBx.Size = new System.Drawing.Size(186, 21);
            this.LocationComboBx.TabIndex = 9;
            this.LocationComboBx.SelectedIndexChanged += new System.EventHandler(this.LocationComboBx_SelectedIndexChanged);
            // 
            // WatermarkFunctionUIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(457, 201);
            this.Controls.Add(this.LocationLbl);
            this.Controls.Add(this.LocationComboBx);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.watermarkImageBtn);
            this.Controls.Add(this.inputRasterBtn);
            this.Controls.Add(this.blendPercentTxtbox);
            this.Controls.Add(this.watermarkImageTxtbox);
            this.Controls.Add(this.inputRasterTxtbox);
            this.Controls.Add(this.blendPercentLbl);
            this.Controls.Add(this.watermarkImageLbl);
            this.Controls.Add(this.inputRasterLbl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "WatermarkFunctionUIForm";
            this.Text = "Watermark Raster Function";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label inputRasterLbl;
        private System.Windows.Forms.Label watermarkImageLbl;
        private System.Windows.Forms.Label blendPercentLbl;
        private System.Windows.Forms.TextBox inputRasterTxtbox;
        private System.Windows.Forms.TextBox watermarkImageTxtbox;
        private System.Windows.Forms.TextBox blendPercentTxtbox;
        private System.Windows.Forms.Button inputRasterBtn;
        private System.Windows.Forms.Button watermarkImageBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.OpenFileDialog watermarkImageDlg;
        private System.Windows.Forms.Label LocationLbl;
        private System.Windows.Forms.ComboBox LocationComboBx;
    }
}

