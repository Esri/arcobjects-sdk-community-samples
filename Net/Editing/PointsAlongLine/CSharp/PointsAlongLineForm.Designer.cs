namespace PointsAlongLine
{
  partial class PointsAlongLineForm
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
      this.tbLineLength = new System.Windows.Forms.TextBox();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.txtDist = new System.Windows.Forms.TextBox();
      this.txtNOP = new System.Windows.Forms.TextBox();
      this.chkEnds = new System.Windows.Forms.CheckBox();
      this.rbDist = new System.Windows.Forms.RadioButton();
      this.rbNOP = new System.Windows.Forms.RadioButton();
      this.cmdCancel = new System.Windows.Forms.Button();
      this.cmdOK = new System.Windows.Forms.Button();
      this.groupBox1.SuspendLayout();
      this.SuspendLayout();
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(12, 9);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(66, 13);
      this.label1.TabIndex = 0;
      this.label1.Text = "Line Length:";
      // 
      // tbLineLength
      // 
      this.tbLineLength.Location = new System.Drawing.Point(84, 6);
      this.tbLineLength.Name = "tbLineLength";
      this.tbLineLength.ReadOnly = true;
      this.tbLineLength.Size = new System.Drawing.Size(188, 20);
      this.tbLineLength.TabIndex = 1;
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.txtDist);
      this.groupBox1.Controls.Add(this.txtNOP);
      this.groupBox1.Controls.Add(this.chkEnds);
      this.groupBox1.Controls.Add(this.rbDist);
      this.groupBox1.Controls.Add(this.rbNOP);
      this.groupBox1.Location = new System.Drawing.Point(15, 32);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(257, 107);
      this.groupBox1.TabIndex = 2;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Construction Options";
      // 
      // txtDist
      // 
      this.txtDist.Location = new System.Drawing.Point(151, 42);
      this.txtDist.Name = "txtDist";
      this.txtDist.Size = new System.Drawing.Size(100, 20);
      this.txtDist.TabIndex = 4;
      // 
      // txtNOP
      // 
      this.txtNOP.Location = new System.Drawing.Point(151, 19);
      this.txtNOP.Name = "txtNOP";
      this.txtNOP.Size = new System.Drawing.Size(100, 20);
      this.txtNOP.TabIndex = 3;
      this.txtNOP.Text = "1";
      // 
      // chkEnds
      // 
      this.chkEnds.AutoSize = true;
      this.chkEnds.Location = new System.Drawing.Point(6, 77);
      this.chkEnds.Name = "chkEnds";
      this.chkEnds.Size = new System.Drawing.Size(177, 17);
      this.chkEnds.TabIndex = 2;
      this.chkEnds.Text = "Create additional points on ends";
      this.chkEnds.UseVisualStyleBackColor = true;
      // 
      // rbDist
      // 
      this.rbDist.AutoSize = true;
      this.rbDist.Location = new System.Drawing.Point(6, 42);
      this.rbDist.Name = "rbDist";
      this.rbDist.Size = new System.Drawing.Size(67, 17);
      this.rbDist.TabIndex = 1;
      this.rbDist.TabStop = true;
      this.rbDist.Text = "Distance";
      this.rbDist.UseVisualStyleBackColor = true;
      // 
      // rbNOP
      // 
      this.rbNOP.AutoSize = true;
      this.rbNOP.Checked = true;
      this.rbNOP.Location = new System.Drawing.Point(6, 19);
      this.rbNOP.Name = "rbNOP";
      this.rbNOP.Size = new System.Drawing.Size(105, 17);
      this.rbNOP.TabIndex = 0;
      this.rbNOP.TabStop = true;
      this.rbNOP.Text = "Number of points";
      this.rbNOP.UseVisualStyleBackColor = true;
      // 
      // cmdCancel
      // 
      this.cmdCancel.Location = new System.Drawing.Point(193, 145);
      this.cmdCancel.Name = "cmdCancel";
      this.cmdCancel.Size = new System.Drawing.Size(75, 23);
      this.cmdCancel.TabIndex = 3;
      this.cmdCancel.Text = "Cancel";
      this.cmdCancel.UseVisualStyleBackColor = true;
      this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
      // 
      // cmdOK
      // 
      this.cmdOK.Location = new System.Drawing.Point(112, 145);
      this.cmdOK.Name = "cmdOK";
      this.cmdOK.Size = new System.Drawing.Size(75, 23);
      this.cmdOK.TabIndex = 4;
      this.cmdOK.Text = "OK";
      this.cmdOK.UseVisualStyleBackColor = true;
      this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
      // 
      // PointsAlongLineForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(284, 180);
      this.Controls.Add(this.cmdOK);
      this.Controls.Add(this.cmdCancel);
      this.Controls.Add(this.groupBox1);
      this.Controls.Add(this.tbLineLength);
      this.Controls.Add(this.label1);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "PointsAlongLineForm";
      this.ShowIcon = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
      this.Text = "Construct points along a line";
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox tbLineLength;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.Button cmdCancel;
    private System.Windows.Forms.TextBox txtDist;
    private System.Windows.Forms.TextBox txtNOP;
    private System.Windows.Forms.CheckBox chkEnds;
    private System.Windows.Forms.RadioButton rbDist;
    private System.Windows.Forms.RadioButton rbNOP;
    private System.Windows.Forms.Button cmdOK;
  }
}