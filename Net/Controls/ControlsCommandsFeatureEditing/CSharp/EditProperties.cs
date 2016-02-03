using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;

namespace EditPropertiesDialog
{
	public class EditProperties : System.Windows.Forms.Form
	{
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtSketchWidth;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button btnSketchColor;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.ColorDialog colorDialog1;
		private System.Windows.Forms.TextBox txtPrecision;
		private System.Windows.Forms.Label lblStream;
		private System.Windows.Forms.TextBox txtStreamCount;
		private System.Windows.Forms.TextBox txtTolerance;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.CheckBox chkStretch;
		private IEngineEditProperties  m_engineEditProperties = new EngineEditorClass();
		private bool bSketchColor;
		private int R;
		private int B;
		private int G;

		private System.ComponentModel.Container components = null;

		public EditProperties()
		{
			InitializeComponent();
		}

		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.btnSketchColor = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.txtSketchWidth = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.colorDialog1 = new System.Windows.Forms.ColorDialog();
			this.label8 = new System.Windows.Forms.Label();
			this.txtPrecision = new System.Windows.Forms.TextBox();
			this.lblStream = new System.Windows.Forms.Label();
			this.txtStreamCount = new System.Windows.Forms.TextBox();
			this.txtTolerance = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.chkStretch = new System.Windows.Forms.CheckBox();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.btnSketchColor);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.txtSketchWidth);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Location = new System.Drawing.Point(8, 144);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(160, 96);
			this.groupBox1.TabIndex = 2;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Sketch Symbol";
			// 
			// btnSketchColor
			// 
			this.btnSketchColor.Location = new System.Drawing.Point(72, 56);
			this.btnSketchColor.Name = "btnSketchColor";
			this.btnSketchColor.Size = new System.Drawing.Size(64, 24);
			this.btnSketchColor.TabIndex = 3;
			this.btnSketchColor.Text = "Pick Color";
			this.btnSketchColor.Click += new System.EventHandler(this.btnSketchColor_Click);
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(16, 64);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(48, 24);
			this.label3.TabIndex = 2;
			this.label3.Text = "Color:";
			// 
			// txtSketchWidth
			// 
			this.txtSketchWidth.Location = new System.Drawing.Point(72, 24);
			this.txtSketchWidth.Name = "txtSketchWidth";
			this.txtSketchWidth.Size = new System.Drawing.Size(64, 20);
			this.txtSketchWidth.TabIndex = 1;
			this.txtSketchWidth.Text = "";
			this.txtSketchWidth.TextChanged += new System.EventHandler(this.txtSketchWidth_TextChanged);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(16, 24);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(48, 24);
			this.label2.TabIndex = 0;
			this.label2.Text = "Width:";
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(8, 16);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(104, 16);
			this.label8.TabIndex = 4;
			this.label8.Text = "Report Precision:";
			// 
			// txtPrecision
			// 
			this.txtPrecision.Location = new System.Drawing.Point(113, 14);
			this.txtPrecision.Name = "txtPrecision";
			this.txtPrecision.Size = new System.Drawing.Size(56, 20);
			this.txtPrecision.TabIndex = 5;
			this.txtPrecision.Text = "";
			this.txtPrecision.TextChanged += new System.EventHandler(this.txtPrecision_TextChanged);
			// 
			// lblStream
			// 
			this.lblStream.Location = new System.Drawing.Point(8, 48);
			this.lblStream.Name = "lblStream";
			this.lblStream.Size = new System.Drawing.Size(104, 16);
			this.lblStream.TabIndex = 4;
			this.lblStream.Text = "Stream Count:";
			// 
			// txtStreamCount
			// 
			this.txtStreamCount.Location = new System.Drawing.Point(113, 48);
			this.txtStreamCount.Name = "txtStreamCount";
			this.txtStreamCount.Size = new System.Drawing.Size(56, 20);
			this.txtStreamCount.TabIndex = 7;
			this.txtStreamCount.Text = "";
			this.txtStreamCount.TextChanged += new System.EventHandler(this.txtStreamCount_TextChanged);
			// 
			// txtTolerance
			// 
			this.txtTolerance.Location = new System.Drawing.Point(113, 80);
			this.txtTolerance.Name = "txtTolerance";
			this.txtTolerance.Size = new System.Drawing.Size(56, 20);
			this.txtTolerance.TabIndex = 9;
			this.txtTolerance.Text = "";
			this.txtTolerance.TextChanged += new System.EventHandler(this.txtTolerance_TextChanged);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 80);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(104, 16);
			this.label1.TabIndex = 4;
			this.label1.Text = "Stream Tolerance:";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(8, 112);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(104, 16);
			this.label4.TabIndex = 4;
			this.label4.Text = "Stretch Geometry:";
			// 
			// chkStretch
			// 
			this.chkStretch.Location = new System.Drawing.Point(112, 104);
			this.chkStretch.Name = "chkStretch";
			this.chkStretch.Size = new System.Drawing.Size(16, 32);
			this.chkStretch.TabIndex = 11;
			// 
			// EditProperties
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(177, 253);
			this.Controls.Add(this.chkStretch);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.txtTolerance);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.txtStreamCount);
			this.Controls.Add(this.lblStream);
			this.Controls.Add(this.txtPrecision);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.groupBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "EditProperties";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Edit Properties";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.EditProperties_Closing);
			this.Load += new System.EventHandler(this.EditProperties_Load);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void EditProperties_Load(object sender, System.EventArgs e)
		{
			//Populate form with current IEngineProperties values 
			txtPrecision.Text   = m_engineEditProperties.ReportPrecision.ToString();
			txtSketchWidth.Text = m_engineEditProperties.SketchSymbol.Width.ToString();
			txtStreamCount.Text = m_engineEditProperties.StreamGroupingCount.ToString();
			txtTolerance.Text   = m_engineEditProperties.StreamTolerance.ToString();

			if (m_engineEditProperties.StretchGeometry)
				chkStretch.Checked = true;
			else
				chkStretch.Checked = false;

			txtPrecision.Focus();
		}


		private void EditProperties_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			//Update precision property
			if (txtPrecision.Text != "")
				m_engineEditProperties.ReportPrecision = Convert.ToInt32(txtPrecision.Text);

			//Update stream grouping count
			if (txtStreamCount.Text != "")
				m_engineEditProperties.StreamGroupingCount = Convert.ToInt32(txtStreamCount.Text);

			//Update stream tolerance
			if (txtTolerance.Text != "")
				m_engineEditProperties.StreamTolerance = Convert.ToInt32(txtTolerance.Text);

			//Update stretch geometry property
			if (chkStretch.Checked)
				m_engineEditProperties.StretchGeometry = true;
			else
				m_engineEditProperties.StretchGeometry = false;

			//Update sketch symbol property
			if (bSketchColor || txtSketchWidth.Text != "")
			{
				ILineSymbol lineSymbol = m_engineEditProperties.SketchSymbol; 

				if(bSketchColor)
				{
					IRgbColor color = new RgbColorClass();
					color.Red   = R;
					color.Blue  = B;
					color.Green = G;
					lineSymbol.Color = color;
				}

				if (txtSketchWidth.Text != "")
				{
					lineSymbol.Width = Convert.ToInt32(txtSketchWidth.Text);
				}
		      
				m_engineEditProperties.SketchSymbol = lineSymbol; 
			}
		}

		private void btnSketchColor_Click(object sender, System.EventArgs e)
		{	
			//Create a new color dialog
			ColorDialog colorDialog = new ColorDialog();
			//Prevent the user from selecting a custom color
			colorDialog.AllowFullOpen = false;
			//Allows the user to obtain help (default is false)
			colorDialog.ShowHelp = true;
	            
			if (colorDialog.ShowDialog() == DialogResult.OK)
			{
				R = Convert.ToInt32(colorDialog.Color.R);
				B = Convert.ToInt32(colorDialog.Color.B);
				G = Convert.ToInt32(colorDialog.Color.G);
				bSketchColor = true;
			}
		}

		private void txtSketchWidth_TextChanged(object sender, System.EventArgs e)
		{
			//Validate sketch width
			try
			{
				if (txtSketchWidth.Text != "")
				Convert.ToInt32(txtSketchWidth.Text);
			}
			catch (FormatException)
			{
				MessageBox.Show("Sketch width should be a numeric value", "Error sketch width");
				txtSketchWidth.Text = "";
				txtSketchWidth.Focus();
			}
		
		}

		private void txtPrecision_TextChanged(object sender, System.EventArgs e)
		{
			//Validate precision
			try
			{
				if (txtPrecision.Text != "")
				Convert.ToInt32(txtPrecision.Text);
			}
			catch (FormatException)
			{
				MessageBox.Show("Precision should be a numeric value", "Error precision");
				txtPrecision.Text = "";
				txtPrecision.Focus();
			}
		}

		private void txtStreamCount_TextChanged(object sender, System.EventArgs e)
		{
			//Validate tolerance
			try
			{
				if (txtStreamCount.Text != "")
				Convert.ToInt32(txtStreamCount.Text);
			}
			catch (FormatException)
			{
				MessageBox.Show("Stream count should be a numeric value", "Error Stream Count");
				txtStreamCount.Text = "";
				txtStreamCount.Focus();
			}
		}

		private void txtTolerance_TextChanged(object sender, System.EventArgs e)
		{
			//Validate tolerance
			try
			{
				if (txtTolerance.Text != "")
				Convert.ToInt32(txtTolerance.Text);
			}
			catch (FormatException)
			{
				MessageBox.Show("Stream Tolerance should be a numeric value", "Error Stream Tolerance");
				txtTolerance.Text = "";
				txtTolerance.Focus();
			}
		}

	}
}
