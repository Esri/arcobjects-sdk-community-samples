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
namespace FindAddress
{
    partial class AddressForm
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
            this.AddressTextBox = new System.Windows.Forms.TextBox();
            this.CityTextBox = new System.Windows.Forms.TextBox();
            this.StateTextBox = new System.Windows.Forms.TextBox();
            this.ZipTextBox = new System.Windows.Forms.TextBox();
            this.AddressLabel = new System.Windows.Forms.Label();
            this.CityLabel = new System.Windows.Forms.Label();
            this.StateLabel = new System.Windows.Forms.Label();
            this.ZipLabel = new System.Windows.Forms.Label();
            this.FindButton = new System.Windows.Forms.Button();
            this.ResultsTextBox = new System.Windows.Forms.RichTextBox();
            this.ResultsLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // AddressTextBox
            // 
            this.AddressTextBox.Location = new System.Drawing.Point(63, 12);
            this.AddressTextBox.Name = "AddressTextBox";
            this.AddressTextBox.Size = new System.Drawing.Size(245, 20);
            this.AddressTextBox.TabIndex = 0;
            // 
            // CityTextBox
            // 
            this.CityTextBox.Location = new System.Drawing.Point(63, 38);
            this.CityTextBox.Name = "CityTextBox";
            this.CityTextBox.Size = new System.Drawing.Size(245, 20);
            this.CityTextBox.TabIndex = 1;
            // 
            // StateTextBox
            // 
            this.StateTextBox.Location = new System.Drawing.Point(63, 64);
            this.StateTextBox.Name = "StateTextBox";
            this.StateTextBox.Size = new System.Drawing.Size(245, 20);
            this.StateTextBox.TabIndex = 2;
            this.StateTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(StateTextBox_KeyDown);
            // 
            // ZipTextBox
            // 
            this.ZipTextBox.Location = new System.Drawing.Point(63, 90);
            this.ZipTextBox.Name = "ZipTextBox";
            this.ZipTextBox.Size = new System.Drawing.Size(245, 20);
            this.ZipTextBox.TabIndex = 3;
            this.ZipTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(ZipTextBox_KeyDown);
            // 
            // AddressLabel
            // 
            this.AddressLabel.AutoSize = true;
            this.AddressLabel.Location = new System.Drawing.Point(12, 15);
            this.AddressLabel.Name = "AddressLabel";
            this.AddressLabel.Size = new System.Drawing.Size(45, 13);
            this.AddressLabel.TabIndex = 6;
            this.AddressLabel.Text = "Address";
            // 
            // CityLabel
            // 
            this.CityLabel.AutoSize = true;
            this.CityLabel.Location = new System.Drawing.Point(12, 41);
            this.CityLabel.Name = "CityLabel";
            this.CityLabel.Size = new System.Drawing.Size(24, 13);
            this.CityLabel.TabIndex = 7;
            this.CityLabel.Text = "City";
            // 
            // StateLabel
            // 
            this.StateLabel.AutoSize = true;
            this.StateLabel.Location = new System.Drawing.Point(12, 67);
            this.StateLabel.Name = "StateLabel";
            this.StateLabel.Size = new System.Drawing.Size(32, 13);
            this.StateLabel.TabIndex = 8;
            this.StateLabel.Text = "State";
            // 
            // ZipLabel
            // 
            this.ZipLabel.AutoSize = true;
            this.ZipLabel.Location = new System.Drawing.Point(12, 93);
            this.ZipLabel.Name = "ZipLabel";
            this.ZipLabel.Size = new System.Drawing.Size(22, 13);
            this.ZipLabel.TabIndex = 9;
            this.ZipLabel.Text = "Zip";
            // 
            // FindButton
            // 
            this.FindButton.Location = new System.Drawing.Point(314, 87);
            this.FindButton.Name = "FindButton";
            this.FindButton.Size = new System.Drawing.Size(75, 23);
            this.FindButton.TabIndex = 4;
            this.FindButton.Text = "Find";
            this.FindButton.UseVisualStyleBackColor = true;
            this.FindButton.Click += new System.EventHandler(this.FindButton_Click);
            // 
            // ResultsTextBox
            // 
            this.ResultsTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.ResultsTextBox.Location = new System.Drawing.Point(15, 152);
            this.ResultsTextBox.Name = "ResultsTextBox";
            this.ResultsTextBox.Size = new System.Drawing.Size(374, 119);
            this.ResultsTextBox.TabIndex = 5;
            this.ResultsTextBox.Text = "";
            // 
            // ResultsLabel
            // 
            this.ResultsLabel.AutoSize = true;
            this.ResultsLabel.Location = new System.Drawing.Point(22, 136);
            this.ResultsLabel.Name = "ResultsLabel";
            this.ResultsLabel.Size = new System.Drawing.Size(42, 13);
            this.ResultsLabel.TabIndex = 10;
            this.ResultsLabel.Text = "Results";
            // 
            // AddressForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(401, 283);
            this.Controls.Add(this.ResultsLabel);
            this.Controls.Add(this.ResultsTextBox);
            this.Controls.Add(this.FindButton);
            this.Controls.Add(this.ZipLabel);
            this.Controls.Add(this.StateLabel);
            this.Controls.Add(this.CityLabel);
            this.Controls.Add(this.AddressLabel);
            this.Controls.Add(this.ZipTextBox);
            this.Controls.Add(this.StateTextBox);
            this.Controls.Add(this.CityTextBox);
            this.Controls.Add(this.AddressTextBox);
            this.Name = "AddressForm";
            this.Text = "Address Search";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox AddressTextBox;
        private System.Windows.Forms.TextBox CityTextBox;
        private System.Windows.Forms.TextBox StateTextBox;
        private System.Windows.Forms.TextBox ZipTextBox;
        private System.Windows.Forms.Label AddressLabel;
        private System.Windows.Forms.Label CityLabel;
        private System.Windows.Forms.Label StateLabel;
        private System.Windows.Forms.Label ZipLabel;
        private System.Windows.Forms.Button FindButton;
        private System.Windows.Forms.RichTextBox ResultsTextBox;
        private System.Windows.Forms.Label ResultsLabel;
    }
}

