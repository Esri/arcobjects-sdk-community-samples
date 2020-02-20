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
namespace DesktopPropertyPageCS
{
    partial class LayerVisibilityPage
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.radioButtonShow = new System.Windows.Forms.RadioButton();
            this.radioButtonHide = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // radioButtonShow
            // 
            this.radioButtonShow.AutoSize = true;
            this.radioButtonShow.Location = new System.Drawing.Point(38, 37);
            this.radioButtonShow.Name = "radioButtonShow";
            this.radioButtonShow.Size = new System.Drawing.Size(55, 17);
            this.radioButtonShow.TabIndex = 0;
            this.radioButtonShow.TabStop = true;
            this.radioButtonShow.Text = "Visible";
            this.radioButtonShow.UseVisualStyleBackColor = true;
            this.radioButtonShow.CheckedChanged += new System.EventHandler(this.radioButtonShow_CheckedChanged);
            // 
            // radioButtonHide
            // 
            this.radioButtonHide.AutoSize = true;
            this.radioButtonHide.Location = new System.Drawing.Point(38, 72);
            this.radioButtonHide.Name = "radioButtonHide";
            this.radioButtonHide.Size = new System.Drawing.Size(63, 17);
            this.radioButtonHide.TabIndex = 1;
            this.radioButtonHide.TabStop = true;
            this.radioButtonHide.Text = "Invisible";
            this.radioButtonHide.UseVisualStyleBackColor = true;
            // 
            // LayerVisibilityPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.radioButtonHide);
            this.Controls.Add(this.radioButtonShow);
            this.Name = "LayerVisibilityPage";
            this.Size = new System.Drawing.Size(305, 173);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton radioButtonShow;
        private System.Windows.Forms.RadioButton radioButtonHide;




    }
}
