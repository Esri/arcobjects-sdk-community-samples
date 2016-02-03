using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;

namespace ColorRamps
{

	public class Form2 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ComboBox comboBox1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.TextBox textBox2;
		private System.Windows.Forms.TextBox textBox3;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button3;
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private ESRI.ArcGIS.Controls.AxSymbologyControl axSymbologyControl1;
    
		private IClassBreaksRenderer m_classBreaksRenderer;
		private IStyleGalleryItem m_styleGalleryItem;
		private IFeatureLayer m_featureLayer;

		public Form2()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Form2));
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.textBox3 = new System.Windows.Forms.TextBox();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.axSymbologyControl1 = new ESRI.ArcGIS.Controls.AxSymbologyControl();
			this.button2 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.axSymbologyControl1)).BeginInit();
			this.SuspendLayout();
			// 
			// comboBox1
			// 
			this.comboBox1.Location = new System.Drawing.Point(16, 56);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(168, 21);
			this.comboBox1.TabIndex = 0;
			this.comboBox1.Text = "comboBox1";
			this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.comboBox1);
			this.groupBox1.Location = new System.Drawing.Point(304, 24);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(232, 100);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "FeatureLayer";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 32);
			this.label1.Name = "label1";
			this.label1.TabIndex = 1;
			this.label1.Text = "Numeric Fields:";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.label4);
			this.groupBox2.Controls.Add(this.label3);
			this.groupBox2.Controls.Add(this.label2);
			this.groupBox2.Controls.Add(this.textBox3);
			this.groupBox2.Controls.Add(this.textBox2);
			this.groupBox2.Controls.Add(this.textBox1);
			this.groupBox2.Location = new System.Drawing.Point(304, 128);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(232, 128);
			this.groupBox2.TabIndex = 1;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Break";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(8, 96);
			this.label4.Name = "label4";
			this.label4.TabIndex = 7;
			this.label4.Text = "Max Value:";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(8, 64);
			this.label3.Name = "label3";
			this.label3.TabIndex = 6;
			this.label3.Text = "Min Value:";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 32);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(104, 23);
			this.label2.TabIndex = 5;
			this.label2.Text = "Number of Classes:";
			// 
			// textBox3
			// 
			this.textBox3.Enabled = false;
			this.textBox3.Location = new System.Drawing.Point(120, 96);
			this.textBox3.Name = "textBox3";
			this.textBox3.TabIndex = 4;
			this.textBox3.Text = "";
			// 
			// textBox2
			// 
			this.textBox2.Enabled = false;
			this.textBox2.Location = new System.Drawing.Point(120, 64);
			this.textBox2.Name = "textBox2";
			this.textBox2.TabIndex = 3;
			this.textBox2.Text = "";
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(120, 32);
			this.textBox1.Name = "textBox1";
			this.textBox1.TabIndex = 2;
			this.textBox1.Text = "";
			this.textBox1.Leave += new System.EventHandler(this.textBox1_Leave);
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.axSymbologyControl1);
			this.groupBox3.Location = new System.Drawing.Point(8, 8);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(288, 288);
			this.groupBox3.TabIndex = 3;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Symbology";
			// 
			// axSymbologyControl1
			// 
			this.axSymbologyControl1.ContainingControl = this;
			this.axSymbologyControl1.Location = new System.Drawing.Point(8, 16);
			this.axSymbologyControl1.Name = "axSymbologyControl1";
			this.axSymbologyControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axSymbologyControl1.OcxState")));
			this.axSymbologyControl1.Size = new System.Drawing.Size(272, 265);
			this.axSymbologyControl1.TabIndex = 0;
			this.axSymbologyControl1.OnItemSelected += new ESRI.ArcGIS.Controls.ISymbologyControlEvents_Ax_OnItemSelectedEventHandler(this.axSymbologyControl1_OnItemSelected);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(384, 264);
			this.button2.Name = "button2";
			this.button2.TabIndex = 3;
			this.button2.Text = "OK";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(464, 264);
			this.button3.Name = "button3";
			this.button3.TabIndex = 4;
			this.button3.Text = "Cancel";
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// Form2
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(546, 304);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.button2);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "Form2";
			this.Text = "Class Breaks Renderer";
			this.Load += new System.EventHandler(this.Form3_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.axSymbologyControl1)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private void Form3_Load(object sender, System.EventArgs e)
		{
    
			//Get the ArcGIS install location
            string sInstall = ESRI.ArcGIS.RuntimeManager.ActiveRuntime.Path;

			//Load the ESRI.ServerStyle file into the SymbologyControl
			axSymbologyControl1.LoadStyleFile(sInstall + "\\Styles\\ESRI.ServerStyle");

			//Set the style class
			axSymbologyControl1.StyleClass = esriSymbologyStyleClass.esriStyleClassColorRamps;

			//Select the color ramp item
			axSymbologyControl1.GetStyleClass(axSymbologyControl1.StyleClass).SelectItem(0);
		}

   
		private void button2_Click(object sender, System.EventArgs e)
		{
			//Create a new ClassBreaksRenderer and set properties
			m_classBreaksRenderer = new ClassBreaksRenderer();
			m_classBreaksRenderer.Field = comboBox1.SelectedItem.ToString();
			m_classBreaksRenderer.BreakCount = Convert.ToInt32(textBox1.Text);
			m_classBreaksRenderer.MinimumBreak = Convert.ToDouble(textBox2.Text);

			//Calculate the class interval by a simple mean value
			double interval = (Convert.ToDouble(textBox3.Text) - m_classBreaksRenderer.MinimumBreak) / m_classBreaksRenderer.BreakCount;

			//Get the color ramp
			IColorRamp colorRamp = (IColorRamp) m_styleGalleryItem.Item;
			//Set the size of the color ramp and recreate it
			colorRamp.Size = Convert.ToInt32(textBox1.Text);
			bool createRamp;
			colorRamp.CreateRamp(out createRamp);

			//Get the enumeration of colors from the color ramp
			IEnumColors enumColors = colorRamp.Colors;
			enumColors.Reset();
			double currentBreak = m_classBreaksRenderer.MinimumBreak;

			ISimpleFillSymbol simpleFillSymbol;
			//Loop through each class break
			for (int i = 0; i <= m_classBreaksRenderer.BreakCount-1; i++)
			{
				//Set class break
				m_classBreaksRenderer.set_Break(i,currentBreak);
				//Create simple fill symbol and set color
				simpleFillSymbol = new SimpleFillSymbolClass();
				simpleFillSymbol.Color = enumColors.Next();
				//Add symbol to renderer
				m_classBreaksRenderer.set_Symbol(i, (ISymbol)simpleFillSymbol);
				currentBreak += interval;
			}

			//Hide the form
			this.Hide();
		}


		private void button3_Click(object sender, System.EventArgs e)
		{
			this.Hide();
		}

		
		private void textBox1_Leave(object sender, System.EventArgs e)
		{
			IColorRamp colorRamp = (IColorRamp) m_styleGalleryItem.Item;

			//Ensure is numeric
			if (IsInteger(textBox1.Text) == false) 
			{
				System.Windows.Forms.MessageBox.Show("Must be a numeric!");
				textBox1.Text = "10";
				return;
			}
			else if (Convert.ToInt32(textBox1.Text) <= 0)
			{
				//Ensure is not zero
				System.Windows.Forms.MessageBox.Show("Must be greater than 0!");
				textBox1.Text = "10";
				return;
			}
			else if (Convert.ToInt32(textBox1.Text) > colorRamp.Size)
			{
				//Ensure does not exceed number of colors in color ramp
				System.Windows.Forms.MessageBox.Show("Must be less than " + colorRamp.Size + "!");
				textBox1.Text = "10";
				return;
			}

		}

		private void comboBox1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			//Find the selected field in the feature layer
			IFeatureClass featureClass = m_featureLayer.FeatureClass;
			IField field = featureClass.Fields.get_Field(featureClass.FindField(comboBox1.Text));

			//Get a feature cursor
			ICursor cursor = (ICursor) m_featureLayer.Search(null, false);

			//Create a DataStatistics object and initialize properties
			IDataStatistics dataStatistics = new DataStatisticsClass();
			dataStatistics.Field = field.Name;
			dataStatistics.Cursor = cursor;

			//Get the result statistics
			IStatisticsResults statisticsResults = dataStatistics.Statistics;

			//Set the values min and max values
			textBox2.Text = statisticsResults.Minimum.ToString();
			textBox3.Text = statisticsResults.Maximum.ToString();
			textBox1.Text = "10";
		}	

		public IClassBreaksRenderer GetClassBreaksRenderer(IFeatureLayer featureLayer)
		{		
			m_featureLayer = featureLayer;

			comboBox1.Items.Clear();

			//Add numeric fields names to the combobox
			IFields fields = m_featureLayer.FeatureClass.Fields;
			for (int i = 0; i<=fields.FieldCount-1; i++)
			{
				if ((fields.get_Field(i).Type == esriFieldType.esriFieldTypeDouble) ||
					(fields.get_Field(i).Type == esriFieldType.esriFieldTypeInteger) ||
					(fields.get_Field(i).Type == esriFieldType.esriFieldTypeSingle) ||
					(fields.get_Field(i).Type == esriFieldType.esriFieldTypeSmallInteger) )
				{
					comboBox1.Items.Add(fields.get_Field(i).Name);
				}
			}
			comboBox1.SelectedIndex = 0;

			//Show form modally and wait for user input
			this.ShowDialog();

			//Return the ClassBreaksRenderer
			return m_classBreaksRenderer;
		}

		private void axSymbologyControl1_OnItemSelected(object sender, ESRI.ArcGIS.Controls.ISymbologyControlEvents_OnItemSelectedEvent e)
		{
			//Get the selected item
			m_styleGalleryItem = (IStyleGalleryItem) e.styleGalleryItem;
		}

		public bool IsInteger(string s)
		{
			try 
			{
				Int32.Parse(s);
			}
			catch 
			{
				return false;
			}
			return true;
		}


	}
}
