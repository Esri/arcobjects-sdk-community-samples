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
namespace CustomRootObject_CS
{
    partial class FrmGxStyleView
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
            this.picStylePreview = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picStylePreview)).BeginInit();
            this.SuspendLayout();
            // 
            // picStylePreview
            // 
            this.picStylePreview.Location = new System.Drawing.Point(5, 7);
            this.picStylePreview.Name = "picStylePreview";
            this.picStylePreview.Size = new System.Drawing.Size(403, 355);
            this.picStylePreview.TabIndex = 0;
            this.picStylePreview.TabStop = false;
            //this.picStylePreview.Resize += new System.EventHandler(this.picStylePreview_Resize);
            this.picStylePreview.SizeChanged += new System.EventHandler(this.picStylePreview_SizeChanged);
            // 
            // FrmGxStyleView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(411, 369);
            this.Controls.Add(this.picStylePreview);
            this.Name = "FrmGxStyleView";
            this.Text = "FrmGxStyleView";
            ((System.ComponentModel.ISupportInitialize)(this.picStylePreview)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.PictureBox picStylePreview;
    }
}