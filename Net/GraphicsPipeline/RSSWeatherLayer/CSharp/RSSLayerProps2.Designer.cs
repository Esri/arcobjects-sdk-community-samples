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
namespace RSSWeatherLayer
{
  partial class RSSLayerProps2
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
      this.txtSymbolSize = new System.Windows.Forms.TextBox();
      this.SuspendLayout();
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(13, 13);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(65, 13);
      this.label1.TabIndex = 0;
      this.label1.Text = "Symbol size:";
      // 
      // txtSymbolSize
      // 
      this.txtSymbolSize.Location = new System.Drawing.Point(84, 13);
      this.txtSymbolSize.Name = "txtSymbolSize";
      this.txtSymbolSize.Size = new System.Drawing.Size(66, 20);
      this.txtSymbolSize.TabIndex = 1;
      this.txtSymbolSize.TextChanged += new System.EventHandler(this.txtSymbolSize_TextChanged);
      // 
      // RSSLayerProps2
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(213, 273);
      this.Controls.Add(this.txtSymbolSize);
      this.Controls.Add(this.label1);
      this.Name = "RSSLayerProps2";
      this.Text = "RSS Layer input";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label1;
    public System.Windows.Forms.TextBox txtSymbolSize;
  }
}