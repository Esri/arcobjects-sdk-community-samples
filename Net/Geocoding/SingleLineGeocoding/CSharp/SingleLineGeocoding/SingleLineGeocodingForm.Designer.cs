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
namespace SingleLineGeocoding
{
    partial class SingleLineGeocodingForm
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
            this.locatorTextBox = new System.Windows.Forms.TextBox();
            this.locatorButton = new System.Windows.Forms.Button();
            this.locatorLabel = new System.Windows.Forms.Label();
            this.addressTextBox = new System.Windows.Forms.TextBox();
            this.addressLabel = new System.Windows.Forms.Label();
            this.ResultsTextBox = new System.Windows.Forms.RichTextBox();
            this.resultsLabel = new System.Windows.Forms.Label();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.findButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // locatorTextBox
            // 
            this.locatorTextBox.Location = new System.Drawing.Point(12, 27);
            this.locatorTextBox.Name = "locatorTextBox";
            this.locatorTextBox.Size = new System.Drawing.Size(438, 20);
            this.locatorTextBox.TabIndex = 0;
            // 
            // locatorButton
            // 
            this.locatorButton.Location = new System.Drawing.Point(456, 25);
            this.locatorButton.Name = "locatorButton";
            this.locatorButton.Size = new System.Drawing.Size(75, 23);
            this.locatorButton.TabIndex = 1;
            this.locatorButton.Text = "Browse...";
            this.locatorButton.UseVisualStyleBackColor = true;
            this.locatorButton.Click += new System.EventHandler(this.locatorButton_Click);
            // 
            // locatorLabel
            // 
            this.locatorLabel.AutoSize = true;
            this.locatorLabel.Location = new System.Drawing.Point(12, 9);
            this.locatorLabel.Name = "locatorLabel";
            this.locatorLabel.Size = new System.Drawing.Size(84, 13);
            this.locatorLabel.TabIndex = 2;
            this.locatorLabel.Text = "Address Locator";
            // 
            // addressTextBox
            // 
            this.addressTextBox.Enabled = false;
            this.addressTextBox.Location = new System.Drawing.Point(12, 92);
            this.addressTextBox.Name = "addressTextBox";
            this.addressTextBox.Size = new System.Drawing.Size(438, 20);
            this.addressTextBox.TabIndex = 3;
            this.addressTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.addressTextBox_KeyDown);
            // 
            // addressLabel
            // 
            this.addressLabel.AutoSize = true;
            this.addressLabel.Location = new System.Drawing.Point(12, 76);
            this.addressLabel.Name = "addressLabel";
            this.addressLabel.Size = new System.Drawing.Size(45, 13);
            this.addressLabel.TabIndex = 4;
            this.addressLabel.Text = "Address";
            // 
            // ResultsTextBox
            // 
            this.ResultsTextBox.Location = new System.Drawing.Point(12, 158);
            this.ResultsTextBox.Name = "ResultsTextBox";
            this.ResultsTextBox.Size = new System.Drawing.Size(519, 139);
            this.ResultsTextBox.TabIndex = 5;
            this.ResultsTextBox.Text = "";
            // 
            // resultsLabel
            // 
            this.resultsLabel.AutoSize = true;
            this.resultsLabel.Location = new System.Drawing.Point(12, 142);
            this.resultsLabel.Name = "resultsLabel";
            this.resultsLabel.Size = new System.Drawing.Size(42, 13);
            this.resultsLabel.TabIndex = 6;
            this.resultsLabel.Text = "Results";
            // 
            // openFileDialog
            // 
            this.openFileDialog.AddExtension = false;
            this.openFileDialog.Filter = "\"Locator (*.loc)\"|*.loc";
            // 
            // findButton
            // 
            this.findButton.Location = new System.Drawing.Point(456, 90);
            this.findButton.Name = "findButton";
            this.findButton.Size = new System.Drawing.Size(75, 23);
            this.findButton.TabIndex = 7;
            this.findButton.Text = "Find";
            this.findButton.UseVisualStyleBackColor = true;
            this.findButton.Click += new System.EventHandler(this.findButton_Click);
            // 
            // SingleLineGeocodingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(543, 309);
            this.Controls.Add(this.findButton);
            this.Controls.Add(this.resultsLabel);
            this.Controls.Add(this.ResultsTextBox);
            this.Controls.Add(this.addressLabel);
            this.Controls.Add(this.addressTextBox);
            this.Controls.Add(this.locatorLabel);
            this.Controls.Add(this.locatorButton);
            this.Controls.Add(this.locatorTextBox);
            this.Name = "SingleLineGeocodingForm";
            this.Text = "SingleLineGeocoding";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox locatorTextBox;
        private System.Windows.Forms.Button locatorButton;
        private System.Windows.Forms.Label locatorLabel;
        private System.Windows.Forms.TextBox addressTextBox;
        private System.Windows.Forms.Label addressLabel;
        private System.Windows.Forms.RichTextBox ResultsTextBox;
        private System.Windows.Forms.Label resultsLabel;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Button findButton;
    }
}

