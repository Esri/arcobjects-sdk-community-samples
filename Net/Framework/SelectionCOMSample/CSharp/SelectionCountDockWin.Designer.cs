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
namespace SelectionCOMSample
{
  partial class SelectionCountDockWin
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
      System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("");
      System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("");
      this.listView1 = new System.Windows.Forms.ListView();
      this.LayerName = new System.Windows.Forms.ColumnHeader();
      this.FeatureCount = new System.Windows.Forms.ColumnHeader();
      this.SuspendLayout();
      // 
      // listView1
      // 
      this.listView1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.LayerName,
            this.FeatureCount});
      this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.listView1.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2});
      this.listView1.Location = new System.Drawing.Point(0, 0);
      this.listView1.Name = "listView1";
      this.listView1.Size = new System.Drawing.Size(344, 287);
      this.listView1.TabIndex = 1;
      this.listView1.UseCompatibleStateImageBehavior = false;
      // 
      // LayerName
      // 
      this.LayerName.Text = "Layer Name";
      this.LayerName.Width = 120;
      // 
      // FeatureCount
      // 
      this.FeatureCount.Text = "Selected Features Count";
      this.FeatureCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      this.FeatureCount.Width = 200;
      // 
      // SelectionCountDockWin
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.listView1);
      this.Name = "SelectionCountDockWin";
      this.Size = new System.Drawing.Size(344, 287);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.ListView listView1;
    private System.Windows.Forms.ColumnHeader LayerName;
    private System.Windows.Forms.ColumnHeader FeatureCount;

  }
}
