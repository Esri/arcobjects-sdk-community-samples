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
namespace GlobeDynamicObjectTracking
{
  partial class TrackSelectionDlg
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
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.chkOrthogonal = new System.Windows.Forms.RadioButton();
      this.chkTerrain = new System.Windows.Forms.RadioButton();
      this.btnOK = new System.Windows.Forms.Button();
      this.btnCancel = new System.Windows.Forms.Button();
      this.groupBox1.SuspendLayout();
      this.SuspendLayout();
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.chkTerrain);
      this.groupBox1.Controls.Add(this.chkOrthogonal);
      this.groupBox1.Location = new System.Drawing.Point(11, 5);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(200, 90);
      this.groupBox1.TabIndex = 0;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Select tracking mode";
      // 
      // chkOrthogonal
      // 
      this.chkOrthogonal.AutoSize = true;
      this.chkOrthogonal.Checked = true;
      this.chkOrthogonal.Location = new System.Drawing.Point(18, 25);
      this.chkOrthogonal.Name = "chkOrthogonal";
      this.chkOrthogonal.Size = new System.Drawing.Size(109, 17);
      this.chkOrthogonal.TabIndex = 0;
      this.chkOrthogonal.TabStop = true;
      this.chkOrthogonal.Text = "Stay above target";
      this.chkOrthogonal.UseVisualStyleBackColor = true;
      // 
      // chkTerrain
      // 
      this.chkTerrain.AutoSize = true;
      this.chkTerrain.Location = new System.Drawing.Point(18, 58);
      this.chkTerrain.Name = "chkTerrain";
      this.chkTerrain.Size = new System.Drawing.Size(85, 17);
      this.chkTerrain.TabIndex = 1;
      this.chkTerrain.Text = "Follow target";
      this.chkTerrain.UseVisualStyleBackColor = true;
      // 
      // btnOK
      // 
      this.btnOK.Location = new System.Drawing.Point(13, 117);
      this.btnOK.Name = "btnOK";
      this.btnOK.Size = new System.Drawing.Size(75, 23);
      this.btnOK.TabIndex = 1;
      this.btnOK.Text = "OK";
      this.btnOK.UseVisualStyleBackColor = true;
      this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(136, 116);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(75, 23);
      this.btnCancel.TabIndex = 2;
      this.btnCancel.Text = "Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
      // 
      // TrackSelectionDlg
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(222, 153);
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.btnOK);
      this.Controls.Add(this.groupBox1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
      this.Name = "TrackSelectionDlg";
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "TrackSelectionDlg";
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.RadioButton chkTerrain;
    private System.Windows.Forms.RadioButton chkOrthogonal;
    private System.Windows.Forms.Button btnOK;
    private System.Windows.Forms.Button btnCancel;
  }
}