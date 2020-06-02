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
namespace TabbedFeatureInspector
{
  partial class TabbedInspector
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
      this.inspectorTabControl = new System.Windows.Forms.TabControl();
      this.standardTabPage = new System.Windows.Forms.TabPage();
      this.defaultPictureBox = new System.Windows.Forms.PictureBox();
      this.customTabPage = new System.Windows.Forms.TabPage();
      this.customListBox = new System.Windows.Forms.ListBox();
      this.inspectorTabControl.SuspendLayout();
      this.standardTabPage.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.defaultPictureBox)).BeginInit();
      this.customTabPage.SuspendLayout();
      this.SuspendLayout();
      // 
      // inspectorTabControl
      // 
      this.inspectorTabControl.Controls.Add(this.standardTabPage);
      this.inspectorTabControl.Controls.Add(this.customTabPage);
      this.inspectorTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
      this.inspectorTabControl.Location = new System.Drawing.Point(0, 0);
      this.inspectorTabControl.MaximumSize = new System.Drawing.Size(1000, 1000);
      this.inspectorTabControl.Name = "inspectorTabControl";
      this.inspectorTabControl.SelectedIndex = 0;
      this.inspectorTabControl.Size = new System.Drawing.Size(300, 320);
      this.inspectorTabControl.TabIndex = 0;
      // 
      // standardTabPage
      // 
      this.standardTabPage.AutoScroll = true;
      this.standardTabPage.Controls.Add(this.defaultPictureBox);
      this.standardTabPage.Location = new System.Drawing.Point(4, 22);
      this.standardTabPage.Name = "standardTabPage";
      this.standardTabPage.Padding = new System.Windows.Forms.Padding(3);
      this.standardTabPage.Size = new System.Drawing.Size(292, 294);
      this.standardTabPage.TabIndex = 0;
      this.standardTabPage.Text = "Standard";
      this.standardTabPage.UseVisualStyleBackColor = true;
      // 
      // defaultPictureBox
      // 
      this.defaultPictureBox.BackColor = System.Drawing.SystemColors.Window;
      this.defaultPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.defaultPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
      this.defaultPictureBox.Location = new System.Drawing.Point(3, 3);
      this.defaultPictureBox.MaximumSize = new System.Drawing.Size(1000, 1000);
      this.defaultPictureBox.Name = "defaultPictureBox";
      this.defaultPictureBox.Size = new System.Drawing.Size(286, 288);
      this.defaultPictureBox.TabIndex = 0;
      this.defaultPictureBox.TabStop = false;
      // 
      // customTabPage
      // 
      this.customTabPage.AutoScroll = true;
      this.customTabPage.Controls.Add(this.customListBox);
      this.customTabPage.Location = new System.Drawing.Point(4, 22);
      this.customTabPage.Name = "customTabPage";
      this.customTabPage.Padding = new System.Windows.Forms.Padding(3);
      this.customTabPage.Size = new System.Drawing.Size(292, 294);
      this.customTabPage.TabIndex = 1;
      this.customTabPage.Text = "Custom";
      this.customTabPage.UseVisualStyleBackColor = true;
      // 
      // customListBox
      // 
      this.customListBox.Dock = System.Windows.Forms.DockStyle.Fill;
      this.customListBox.FormattingEnabled = true;
      this.customListBox.Location = new System.Drawing.Point(3, 3);
      this.customListBox.MaximumSize = new System.Drawing.Size(1000, 1000);
      this.customListBox.MultiColumn = true;
      this.customListBox.Name = "customListBox";
      this.customListBox.Size = new System.Drawing.Size(286, 277);
      this.customListBox.TabIndex = 0;
      // 
      // TabbedInspector
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
      this.Controls.Add(this.inspectorTabControl);
      this.Name = "TabbedInspector";
      this.Size = new System.Drawing.Size(300, 320);
      this.inspectorTabControl.ResumeLayout(false);
      this.standardTabPage.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.defaultPictureBox)).EndInit();
      this.customTabPage.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TabControl inspectorTabControl;
    private System.Windows.Forms.TabPage standardTabPage;
    private System.Windows.Forms.TabPage customTabPage;
    private System.Windows.Forms.PictureBox defaultPictureBox;
    private System.Windows.Forms.ListBox customListBox;
  }
}
