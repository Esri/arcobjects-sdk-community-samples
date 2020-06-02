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
namespace GraphicsLayerToolControl
{
  partial class GraphicsLayersListCtrl
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
      this.cmbGraphicsLayerList = new System.Windows.Forms.ComboBox();
      this.lblGraphicsLayer = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // cmbGraphicsLayerList
      // 
      this.cmbGraphicsLayerList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.cmbGraphicsLayerList.Location = new System.Drawing.Point(84, 3);
      this.cmbGraphicsLayerList.Name = "cmbGraphicsLayerList";
      this.cmbGraphicsLayerList.Size = new System.Drawing.Size(162, 21);
      this.cmbGraphicsLayerList.TabIndex = 3;
      this.cmbGraphicsLayerList.SelectedIndexChanged += new System.EventHandler(this.cmbGraphicsLayerList_SelectedIndexChanged);
      // 
      // lblGraphicsLayer
      // 
      this.lblGraphicsLayer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)));
      this.lblGraphicsLayer.Location = new System.Drawing.Point(1, 4);
      this.lblGraphicsLayer.Name = "lblGraphicsLayer";
      this.lblGraphicsLayer.Size = new System.Drawing.Size(88, 17);
      this.lblGraphicsLayer.TabIndex = 2;
      this.lblGraphicsLayer.Text = "Graphics Layer:";
      this.lblGraphicsLayer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // GraphicsLayersListCtrl
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.cmbGraphicsLayerList);
      this.Controls.Add(this.lblGraphicsLayer);
      this.Name = "GraphicsLayersListCtrl";
      this.Size = new System.Drawing.Size(249, 28);
      this.ResumeLayout(false);

    }

    #endregion

    public System.Windows.Forms.ComboBox cmbGraphicsLayerList;
    public System.Windows.Forms.Label lblGraphicsLayer;
  }
}
