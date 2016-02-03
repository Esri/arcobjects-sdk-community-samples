namespace SelectionSample
{
  partial class SelCountDockWin
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelCountDockWin));
      this.listView1 = new System.Windows.Forms.ListView();
      this.LayerName = new System.Windows.Forms.ColumnHeader();
      this.FeatureCount = new System.Windows.Forms.ColumnHeader();
      this.label1 = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // listView1
      // 
      this.listView1.BackColor = System.Drawing.SystemColors.Window;
      this.listView1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.LayerName,
            this.FeatureCount});
      resources.ApplyResources(this.listView1, "listView1");
      this.listView1.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            ((System.Windows.Forms.ListViewItem)(resources.GetObject("listView1.Items"))),
            ((System.Windows.Forms.ListViewItem)(resources.GetObject("listView1.Items1")))});
      this.listView1.Name = "listView1";
      this.listView1.UseCompatibleStateImageBehavior = false;
      // 
      // LayerName
      // 
      resources.ApplyResources(this.LayerName, "LayerName");
      // 
      // FeatureCount
      // 
      resources.ApplyResources(this.FeatureCount, "FeatureCount");
      // 
      // label1
      // 
      resources.ApplyResources(this.label1, "label1");
      this.label1.Name = "label1";
      // 
      // SelCountDockWin
      // 
      this.Controls.Add(this.label1);
      this.Controls.Add(this.listView1);
      resources.ApplyResources(this, "$this");
      this.Name = "SelCountDockWin";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    internal System.Windows.Forms.ListView listView1;
    private System.Windows.Forms.ColumnHeader LayerName;
    private System.Windows.Forms.ColumnHeader FeatureCount;
    private System.Windows.Forms.Label label1;

  }
}
