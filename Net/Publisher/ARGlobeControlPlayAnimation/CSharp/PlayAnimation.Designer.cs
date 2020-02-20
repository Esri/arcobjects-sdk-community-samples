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
namespace PlayAnimation
{
    partial class PlayAnimation
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
										System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlayAnimation));
										this.axArcReaderGlobeControl1 = new ESRI.ArcGIS.PublisherControls.AxArcReaderGlobeControl();
										this.btnLoad = new System.Windows.Forms.Button();
										this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
										this.chkShowWindow = new System.Windows.Forms.CheckBox();
										this.cboAnimations = new System.Windows.Forms.ComboBox();
										this.btnPlay = new System.Windows.Forms.Button();
										this.lblInstructions = new System.Windows.Forms.Label();
										this.lblInstructions1 = new System.Windows.Forms.Label();
										this.lblInstructions2 = new System.Windows.Forms.Label();
										((System.ComponentModel.ISupportInitialize)(this.axArcReaderGlobeControl1)).BeginInit();
										this.SuspendLayout();
										// 
										// axArcReaderGlobeControl1
										// 
										this.axArcReaderGlobeControl1.Location = new System.Drawing.Point(12, 12);
										this.axArcReaderGlobeControl1.Name = "axArcReaderGlobeControl1";
										this.axArcReaderGlobeControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axArcReaderGlobeControl1.OcxState")));
										this.axArcReaderGlobeControl1.Size = new System.Drawing.Size(526, 388);
										this.axArcReaderGlobeControl1.TabIndex = 0;
										// 
										// btnLoad
										// 
										this.btnLoad.Location = new System.Drawing.Point(691, 34);
										this.btnLoad.Name = "btnLoad";
										this.btnLoad.Size = new System.Drawing.Size(51, 26);
										this.btnLoad.TabIndex = 1;
										this.btnLoad.Text = "Load";
										this.btnLoad.UseVisualStyleBackColor = true;
										this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
										// 
										// openFileDialog1
										// 
										this.openFileDialog1.FileName = "openFileDialog1";
										// 
										// chkShowWindow
										// 
										this.chkShowWindow.AutoSize = true;
										this.chkShowWindow.Location = new System.Drawing.Point(547, 132);
										this.chkShowWindow.Name = "chkShowWindow";
										this.chkShowWindow.Size = new System.Drawing.Size(144, 17);
										this.chkShowWindow.TabIndex = 2;
										this.chkShowWindow.Text = "Show Animation Window";
										this.chkShowWindow.UseVisualStyleBackColor = true;
										this.chkShowWindow.CheckedChanged += new System.EventHandler(this.chkShowWindow_CheckedChanged);
										// 
										// cboAnimations
										// 
										this.cboAnimations.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
										this.cboAnimations.FormattingEnabled = true;
										this.cboAnimations.Location = new System.Drawing.Point(547, 96);
										this.cboAnimations.Name = "cboAnimations";
										this.cboAnimations.Size = new System.Drawing.Size(195, 21);
										this.cboAnimations.TabIndex = 3;
										this.cboAnimations.SelectedIndexChanged += new System.EventHandler(this.cboAnimations_SelectedIndexChanged);
										// 
										// btnPlay
										// 
										this.btnPlay.Location = new System.Drawing.Point(695, 132);
										this.btnPlay.Name = "btnPlay";
										this.btnPlay.Size = new System.Drawing.Size(48, 29);
										this.btnPlay.TabIndex = 4;
										this.btnPlay.Text = "Play";
										this.btnPlay.UseVisualStyleBackColor = true;
										this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
										// 
										// lblInstructions
										// 
										this.lblInstructions.AutoSize = true;
										this.lblInstructions.ForeColor = System.Drawing.SystemColors.Highlight;
										this.lblInstructions.Location = new System.Drawing.Point(544, 12);
										this.lblInstructions.Name = "lblInstructions";
										this.lblInstructions.Size = new System.Drawing.Size(186, 13);
										this.lblInstructions.TabIndex = 5;
										this.lblInstructions.Text = "1) Load PMF published from ArcGlobe";
										// 
										// lblInstructions1
										// 
										this.lblInstructions1.AutoSize = true;
										this.lblInstructions1.ForeColor = System.Drawing.SystemColors.Highlight;
										this.lblInstructions1.Location = new System.Drawing.Point(544, 34);
										this.lblInstructions1.Name = "lblInstructions1";
										this.lblInstructions1.Size = new System.Drawing.Size(139, 13);
										this.lblInstructions1.TabIndex = 6;
										this.lblInstructions1.Text = "    that contains animations..";
										// 
										// lblInstructions2
										// 
										this.lblInstructions2.AutoSize = true;
										this.lblInstructions2.ForeColor = System.Drawing.SystemColors.Highlight;
										this.lblInstructions2.Location = new System.Drawing.Point(544, 80);
										this.lblInstructions2.Name = "lblInstructions2";
										this.lblInstructions2.Size = new System.Drawing.Size(193, 13);
										this.lblInstructions2.TabIndex = 7;
										this.lblInstructions2.Text = "2) Use controls below to Play Animation";
										// 
										// PlayAnimation
										// 
										this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
										this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
										this.ClientSize = new System.Drawing.Size(755, 412);
										this.Controls.Add(this.lblInstructions2);
										this.Controls.Add(this.lblInstructions1);
										this.Controls.Add(this.lblInstructions);
										this.Controls.Add(this.btnPlay);
										this.Controls.Add(this.cboAnimations);
										this.Controls.Add(this.chkShowWindow);
										this.Controls.Add(this.btnLoad);
										this.Controls.Add(this.axArcReaderGlobeControl1);
										this.Name = "PlayAnimation";
										this.Text = "PlayAnimation";
										this.Load += new System.EventHandler(this.PlayAnimation_Load);
										((System.ComponentModel.ISupportInitialize)(this.axArcReaderGlobeControl1)).EndInit();
										this.ResumeLayout(false);
										this.PerformLayout();

        }

        #endregion

        private ESRI.ArcGIS.PublisherControls.AxArcReaderGlobeControl axArcReaderGlobeControl1;
        private System.Windows.Forms.Button btnLoad;
						private System.Windows.Forms.OpenFileDialog openFileDialog1;
						private System.Windows.Forms.CheckBox chkShowWindow;
						private System.Windows.Forms.ComboBox cboAnimations;
						private System.Windows.Forms.Button btnPlay;
						private System.Windows.Forms.Label lblInstructions;
						private System.Windows.Forms.Label lblInstructions1;
						private System.Windows.Forms.Label lblInstructions2;
    }
}

