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
namespace MultipleGlobeViewers
{
    partial class SecondaryViewerForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.viewerListBox = new System.Windows.Forms.ListBox();
            this.topDownButton = new System.Windows.Forms.Button();
            this.normalButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(2, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select the Viewer";
            // 
            // viewerListBox
            // 
            this.viewerListBox.FormattingEnabled = true;
            this.viewerListBox.Location = new System.Drawing.Point(98, 12);
            this.viewerListBox.Name = "viewerListBox";
            this.viewerListBox.Size = new System.Drawing.Size(128, 56);
            this.viewerListBox.TabIndex = 1;
            // 
            // topDownButton
            // 
            this.topDownButton.Location = new System.Drawing.Point(29, 83);
            this.topDownButton.Name = "topDownButton";
            this.topDownButton.Size = new System.Drawing.Size(75, 23);
            this.topDownButton.TabIndex = 2;
            this.topDownButton.Text = "Top Down";
            this.topDownButton.UseVisualStyleBackColor = true;
            // 
            // normalButton
            // 
            this.normalButton.Location = new System.Drawing.Point(129, 83);
            this.normalButton.Name = "normalButton";
            this.normalButton.Size = new System.Drawing.Size(75, 23);
            this.normalButton.TabIndex = 3;
            this.normalButton.Text = "Syncd Up";
            this.normalButton.UseVisualStyleBackColor = true;
            // 
            // SecondaryViewerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(235, 113);
            this.Controls.Add(this.normalButton);
            this.Controls.Add(this.topDownButton);
            this.Controls.Add(this.viewerListBox);
            this.Controls.Add(this.label1);
            this.Name = "SecondaryViewerForm";
            this.Text = "SecondaryViewer";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.ListBox viewerListBox;
        public System.Windows.Forms.Button topDownButton;
        public System.Windows.Forms.Button normalButton;

    }
}