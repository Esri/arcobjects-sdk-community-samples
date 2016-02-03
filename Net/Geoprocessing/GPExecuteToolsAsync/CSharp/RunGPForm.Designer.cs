namespace RunGPAsync
{
  partial class RunGPForm
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
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RunGPForm));
      this.imageList1 = new System.Windows.Forms.ImageList(this.components);
      this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
      this.panel1 = new System.Windows.Forms.Panel();
      this.textBox7 = new System.Windows.Forms.TextBox();
      this.textBox6 = new System.Windows.Forms.TextBox();
      this.textBox5 = new System.Windows.Forms.TextBox();
      this.txtBufferDistance = new System.Windows.Forms.TextBox();
      this.textBox4 = new System.Windows.Forms.TextBox();
      this.textBox3 = new System.Windows.Forms.TextBox();
      this.textBox2 = new System.Windows.Forms.TextBox();
      this.textBox1 = new System.Windows.Forms.TextBox();
      this.axLicenseControl1 = new ESRI.ArcGIS.Controls.AxLicenseControl();
      this.btnRunGP = new System.Windows.Forms.Button();
      this.listView1 = new System.Windows.Forms.ListView();
      this.axMapControl1 = new ESRI.ArcGIS.Controls.AxMapControl();
      this.axToolbarControl1 = new ESRI.ArcGIS.Controls.AxToolbarControl();
      this.panel2 = new System.Windows.Forms.Panel();
      this.axTOCControl1 = new ESRI.ArcGIS.Controls.AxTOCControl();
      this.tableLayoutPanel1.SuspendLayout();
      this.panel1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.axMapControl1)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.axToolbarControl1)).BeginInit();
      this.panel2.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.axTOCControl1)).BeginInit();
      this.SuspendLayout();
      // 
      // imageList1
      // 
      this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
      this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
      this.imageList1.Images.SetKeyName(0, "error");
      this.imageList1.Images.SetKeyName(1, "information");
      this.imageList1.Images.SetKeyName(2, "warning");
      this.imageList1.Images.SetKeyName(3, "success");
      // 
      // tableLayoutPanel1
      // 
      this.tableLayoutPanel1.BackColor = System.Drawing.Color.White;
      this.tableLayoutPanel1.ColumnCount = 2;
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.70563F));
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 72.29437F));
      this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
      this.tableLayoutPanel1.Controls.Add(this.listView1, 1, 2);
      this.tableLayoutPanel1.Controls.Add(this.axMapControl1, 1, 1);
      this.tableLayoutPanel1.Controls.Add(this.axToolbarControl1, 1, 0);
      this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 2);
      this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.tableLayoutPanel1.RowCount = 3;
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.042253F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 92.95775F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 211F));
      this.tableLayoutPanel1.Size = new System.Drawing.Size(693, 558);
      this.tableLayoutPanel1.TabIndex = 7;
      // 
      // panel1
      // 
      this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
      this.panel1.Controls.Add(this.textBox7);
      this.panel1.Controls.Add(this.textBox6);
      this.panel1.Controls.Add(this.textBox5);
      this.panel1.Controls.Add(this.txtBufferDistance);
      this.panel1.Controls.Add(this.textBox4);
      this.panel1.Controls.Add(this.textBox3);
      this.panel1.Controls.Add(this.textBox2);
      this.panel1.Controls.Add(this.textBox1);
      this.panel1.Controls.Add(this.axLicenseControl1);
      this.panel1.Controls.Add(this.btnRunGP);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel1.Location = new System.Drawing.Point(3, 3);
      this.panel1.Name = "panel1";
      this.tableLayoutPanel1.SetRowSpan(this.panel1, 2);
      this.panel1.Size = new System.Drawing.Size(186, 340);
      this.panel1.TabIndex = 0;
      // 
      // textBox7
      // 
      this.textBox7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
      this.textBox7.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.textBox7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.textBox7.Location = new System.Drawing.Point(3, 276);
      this.textBox7.Multiline = true;
      this.textBox7.Name = "textBox7";
      this.textBox7.Size = new System.Drawing.Size(180, 61);
      this.textBox7.TabIndex = 15;
      this.textBox7.Text = "4. Test that the application stays responsive while the tools are executing, e.g." +
          " try to Pan and Zoom the map.";
      // 
      // textBox6
      // 
      this.textBox6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
      this.textBox6.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.textBox6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.textBox6.Location = new System.Drawing.Point(3, 201);
      this.textBox6.Multiline = true;
      this.textBox6.Name = "textBox6";
      this.textBox6.Size = new System.Drawing.Size(180, 33);
      this.textBox6.TabIndex = 14;
      this.textBox6.Text = "3. Click Run to perform the analysis:";
      // 
      // textBox5
      // 
      this.textBox5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
      this.textBox5.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.textBox5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.textBox5.Location = new System.Drawing.Point(69, 180);
      this.textBox5.Multiline = true;
      this.textBox5.Name = "textBox5";
      this.textBox5.Size = new System.Drawing.Size(114, 13);
      this.textBox5.TabIndex = 13;
      this.textBox5.Text = "Miles";
      // 
      // txtBufferDistance
      // 
      this.txtBufferDistance.BackColor = System.Drawing.Color.White;
      this.txtBufferDistance.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.txtBufferDistance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtBufferDistance.Location = new System.Drawing.Point(17, 180);
      this.txtBufferDistance.Name = "txtBufferDistance";
      this.txtBufferDistance.Size = new System.Drawing.Size(46, 13);
      this.txtBufferDistance.TabIndex = 12;
      this.txtBufferDistance.Text = "30";
      this.txtBufferDistance.TextChanged += new System.EventHandler(this.txtBufferDistance_TextChanged);
      // 
      // textBox4
      // 
      this.textBox4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
      this.textBox4.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.textBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.textBox4.Location = new System.Drawing.Point(3, 158);
      this.textBox4.Multiline = true;
      this.textBox4.Name = "textBox4";
      this.textBox4.Size = new System.Drawing.Size(180, 25);
      this.textBox4.TabIndex = 11;
      this.textBox4.Text = "2. Enter a buffer distance:";
      // 
      // textBox3
      // 
      this.textBox3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
      this.textBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.textBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.textBox3.Location = new System.Drawing.Point(3, 112);
      this.textBox3.Multiline = true;
      this.textBox3.Name = "textBox3";
      this.textBox3.Size = new System.Drawing.Size(180, 51);
      this.textBox3.TabIndex = 10;
      this.textBox3.Text = "1. Use the currently selected city, or use the Select Features tool to select som" +
          "e other cities";
      // 
      // textBox2
      // 
      this.textBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
      this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.textBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.textBox2.Location = new System.Drawing.Point(3, 95);
      this.textBox2.Multiline = true;
      this.textBox2.Name = "textBox2";
      this.textBox2.Size = new System.Drawing.Size(180, 25);
      this.textBox2.TabIndex = 9;
      this.textBox2.Text = "Steps:";
      // 
      // textBox1
      // 
      this.textBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
      this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.textBox1.Location = new System.Drawing.Point(3, 3);
      this.textBox1.Multiline = true;
      this.textBox1.Name = "textBox1";
      this.textBox1.Size = new System.Drawing.Size(180, 92);
      this.textBox1.TabIndex = 8;
      this.textBox1.Text = "This sample demonstrates a two stage geoprocessing analysis. The selected cities" +
          " are buffered and the output layer is used to clip the zip codes layer. The resu" +
          "lt layer is then added to the map.";
      // 
      // axLicenseControl1
      // 
      this.axLicenseControl1.Enabled = true;
      this.axLicenseControl1.Location = new System.Drawing.Point(130, 238);
      this.axLicenseControl1.Name = "axLicenseControl1";
      this.axLicenseControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axLicenseControl1.OcxState")));
      this.axLicenseControl1.Size = new System.Drawing.Size(32, 32);
      this.axLicenseControl1.TabIndex = 6;
      // 
      // btnRunGP
      // 
      this.btnRunGP.Enabled = false;
      this.btnRunGP.Location = new System.Drawing.Point(17, 236);
      this.btnRunGP.Name = "btnRunGP";
      this.btnRunGP.Size = new System.Drawing.Size(75, 23);
      this.btnRunGP.TabIndex = 5;
      this.btnRunGP.Text = "Run";
      this.btnRunGP.UseVisualStyleBackColor = true;
      this.btnRunGP.Click += new System.EventHandler(this.btnRunGP_Click);
      // 
      // listView1
      // 
      this.listView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.listView1.Location = new System.Drawing.Point(195, 349);
      this.listView1.Name = "listView1";
      this.listView1.Size = new System.Drawing.Size(495, 206);
      this.listView1.SmallImageList = this.imageList1;
      this.listView1.TabIndex = 1;
      this.listView1.UseCompatibleStateImageBehavior = false;
      this.listView1.View = System.Windows.Forms.View.Details;
      // 
      // axMapControl1
      // 
      this.axMapControl1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.axMapControl1.Location = new System.Drawing.Point(195, 27);
      this.axMapControl1.Name = "axMapControl1";
      this.axMapControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axMapControl1.OcxState")));
      this.axMapControl1.Size = new System.Drawing.Size(495, 316);
      this.axMapControl1.TabIndex = 2;
      // 
      // axToolbarControl1
      // 
      this.axToolbarControl1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.axToolbarControl1.Location = new System.Drawing.Point(195, 3);
      this.axToolbarControl1.Name = "axToolbarControl1";
      this.axToolbarControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axToolbarControl1.OcxState")));
      this.axToolbarControl1.Size = new System.Drawing.Size(495, 28);
      this.axToolbarControl1.TabIndex = 3;
      // 
      // panel2
      // 
      this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
      this.panel2.Controls.Add(this.axTOCControl1);
      this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel2.Location = new System.Drawing.Point(3, 349);
      this.panel2.Name = "panel2";
      this.panel2.Size = new System.Drawing.Size(186, 206);
      this.panel2.TabIndex = 4;
      // 
      // axTOCControl1
      // 
      this.axTOCControl1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.axTOCControl1.Location = new System.Drawing.Point(0, 0);
      this.axTOCControl1.Name = "axTOCControl1";
      this.axTOCControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axTOCControl1.OcxState")));
      this.axTOCControl1.Size = new System.Drawing.Size(186, 206);
      this.axTOCControl1.TabIndex = 0;
      // 
      // RunGPForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(693, 558);
      this.Controls.Add(this.tableLayoutPanel1);
      this.Name = "RunGPForm";
      this.Text = "Run Geoprocessing Tools Asynchronously";
      this.tableLayoutPanel1.ResumeLayout(false);
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.axMapControl1)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.axToolbarControl1)).EndInit();
      this.panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.axTOCControl1)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.ImageList imageList1;
    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Button btnRunGP;
    private ESRI.ArcGIS.Controls.AxLicenseControl axLicenseControl1;
    private System.Windows.Forms.ListView listView1;
    private ESRI.ArcGIS.Controls.AxMapControl axMapControl1;
    private ESRI.ArcGIS.Controls.AxToolbarControl axToolbarControl1;
    private System.Windows.Forms.TextBox textBox1;
    private System.Windows.Forms.Panel panel2;
    private ESRI.ArcGIS.Controls.AxTOCControl axTOCControl1;
    private System.Windows.Forms.TextBox textBox2;
    private System.Windows.Forms.TextBox textBox5;
    private System.Windows.Forms.TextBox txtBufferDistance;
    private System.Windows.Forms.TextBox textBox4;
    private System.Windows.Forms.TextBox textBox3;
    private System.Windows.Forms.TextBox textBox7;
    private System.Windows.Forms.TextBox textBox6;
  }
}

