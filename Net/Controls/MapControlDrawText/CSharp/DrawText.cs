using System;
using System.Windows.Forms;
using System.Drawing;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.ADF.COMSupport;
using ESRI.ArcGIS;


namespace DrawText
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		public System.Windows.Forms.TextBox Text1;
		public System.Windows.Forms.Label Label1;
		public System.Windows.Forms.Button cmdReset;
		public System.Windows.Forms.Button cmdFullExtent;
		public System.Windows.Forms.Label Label3;
		public System.Windows.Forms.Label Label2;
		private IPointCollection m_PointCollection; 
		private IPolyline m_Polyline;
		private ESRI.ArcGIS.Controls.AxMapControl axMapControl1;
		private ESRI.ArcGIS.Controls.AxLicenseControl axLicenseControl1; 
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
            //Release COM objects 
            ESRI.ArcGIS.ADF.COMSupport.AOUninitialize.Shutdown();

			if( disposing )
			{
				if (components != null) 
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.Text1 = new System.Windows.Forms.TextBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.cmdReset = new System.Windows.Forms.Button();
            this.cmdFullExtent = new System.Windows.Forms.Button();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.axMapControl1 = new ESRI.ArcGIS.Controls.AxMapControl();
            this.axLicenseControl1 = new ESRI.ArcGIS.Controls.AxLicenseControl();
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // Text1
            // 
            this.Text1.AcceptsReturn = true;
            this.Text1.BackColor = System.Drawing.SystemColors.Window;
            this.Text1.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.Text1.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Text1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.Text1.Location = new System.Drawing.Point(8, 280);
            this.Text1.MaxLength = 0;
            this.Text1.Name = "Text1";
            this.Text1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Text1.Size = new System.Drawing.Size(321, 25);
            this.Text1.TabIndex = 4;
            this.Text1.Text = "Put a map in your app...";
            // 
            // Label1
            // 
            this.Label1.BackColor = System.Drawing.SystemColors.Control;
            this.Label1.Cursor = System.Windows.Forms.Cursors.Default;
            this.Label1.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Label1.Location = new System.Drawing.Point(8, 264);
            this.Label1.Name = "Label1";
            this.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Label1.Size = new System.Drawing.Size(129, 17);
            this.Label1.TabIndex = 5;
            this.Label1.Text = "Enter text:";
            // 
            // cmdReset
            // 
            this.cmdReset.BackColor = System.Drawing.SystemColors.Control;
            this.cmdReset.Cursor = System.Windows.Forms.Cursors.Default;
            this.cmdReset.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdReset.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdReset.Location = new System.Drawing.Point(344, 208);
            this.cmdReset.Name = "cmdReset";
            this.cmdReset.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmdReset.Size = new System.Drawing.Size(73, 25);
            this.cmdReset.TabIndex = 7;
            this.cmdReset.Text = "Clear Text";
            this.cmdReset.UseVisualStyleBackColor = false;
            this.cmdReset.Click += new System.EventHandler(this.cmdReset_Click);
            // 
            // cmdFullExtent
            // 
            this.cmdFullExtent.BackColor = System.Drawing.SystemColors.Control;
            this.cmdFullExtent.Cursor = System.Windows.Forms.Cursors.Default;
            this.cmdFullExtent.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdFullExtent.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdFullExtent.Location = new System.Drawing.Point(344, 240);
            this.cmdFullExtent.Name = "cmdFullExtent";
            this.cmdFullExtent.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmdFullExtent.Size = new System.Drawing.Size(73, 25);
            this.cmdFullExtent.TabIndex = 6;
            this.cmdFullExtent.Text = "Full Extent";
            this.cmdFullExtent.UseVisualStyleBackColor = false;
            this.cmdFullExtent.Click += new System.EventHandler(this.cmdFullExtent_Click);
            // 
            // Label3
            // 
            this.Label3.BackColor = System.Drawing.SystemColors.Control;
            this.Label3.Cursor = System.Windows.Forms.Cursors.Default;
            this.Label3.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Label3.Location = new System.Drawing.Point(344, 72);
            this.Label3.Name = "Label3";
            this.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Label3.Size = new System.Drawing.Size(81, 65);
            this.Label3.TabIndex = 8;
            this.Label3.Text = "Right mouse button to drag a rectangle to zoom in.";
            // 
            // Label2
            // 
            this.Label2.BackColor = System.Drawing.SystemColors.Control;
            this.Label2.Cursor = System.Windows.Forms.Cursors.Default;
            this.Label2.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Label2.Location = new System.Drawing.Point(344, 8);
            this.Label2.Name = "Label2";
            this.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Label2.Size = new System.Drawing.Size(81, 65);
            this.Label2.TabIndex = 9;
            this.Label2.Text = "Left mouse button to trace a line to draw text along. ";
            // 
            // axMapControl1
            // 
            this.axMapControl1.Location = new System.Drawing.Point(8, 8);
            this.axMapControl1.Name = "axMapControl1";
            this.axMapControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axMapControl1.OcxState")));
            this.axMapControl1.Size = new System.Drawing.Size(320, 248);
            this.axMapControl1.TabIndex = 10;
            this.axMapControl1.OnMouseDown += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMouseDownEventHandler(this.axMapControl1_OnMouseDown);
            this.axMapControl1.OnAfterDraw += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnAfterDrawEventHandler(this.axMapControl1_OnAfterDraw);
            // 
            // axLicenseControl1
            // 
            this.axLicenseControl1.Enabled = true;
            this.axLicenseControl1.Location = new System.Drawing.Point(347, 140);
            this.axLicenseControl1.Name = "axLicenseControl1";
            this.axLicenseControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axLicenseControl1.OcxState")));
            this.axLicenseControl1.Size = new System.Drawing.Size(32, 32);
            this.axLicenseControl1.TabIndex = 11;
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(432, 310);
            this.Controls.Add(this.axLicenseControl1);
            this.Controls.Add(this.axMapControl1);
            this.Controls.Add(this.cmdReset);
            this.Controls.Add(this.cmdFullExtent);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.Text1);
            this.Controls.Add(this.Label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
            
            if (!RuntimeManager.Bind(ProductCode.Engine))
            {
                if (!RuntimeManager.Bind(ProductCode.Desktop))
                {
                    MessageBox.Show("Unable to bind to ArcGIS runtime. Application will be shut down.");
                    return;
                }
            }

            Application.Run(new Form1());
		}

		private void Form1_Load(object sender, System.EventArgs e)
		{
			//Find sample data by navigating two folders up
			string sFilePath = @"..\..\..\Data\World\Continents.lyr";
            
			//Add sample 'country' shapefile data
			axMapControl1.AddLayerFromFile(sFilePath); 
            //Set the extent
            axMapControl1.Extent = axMapControl1.get_Layer(0).AreaOfInterest;
			
            //Grab hold of the IgeoFeaturelayer interface on the layer
			//in the map control in order to symbolize the data
			IGeoFeatureLayer geoFeatureLayer = (IGeoFeatureLayer) axMapControl1.get_Layer(0);

			//Create a simple renderer and grab hold of ISimpleRenderer interface
			ISimpleRenderer simpleRenderer = new  SimpleRendererClass();
			//Create a fill symbol and grab hold of the ISimpleFillSymbol interface
			ISimpleFillSymbol fillSymbol = new SimpleFillSymbolClass(); 
			//Create a line symbol and grab hold of the ISimpleLineSymbol interface
			ISimpleLineSymbol lineSymbol = new SimpleLineSymbolClass();

			//Assign line symbol and fill symbol properties
			lineSymbol.Width = 0.1;
			lineSymbol.Color = GetRGBColor(255, 0, 0); //Red
			fillSymbol.Outline = lineSymbol;
			fillSymbol.Color = GetRGBColor(0, 0, 255); //Blue

			//Set the symbol property of the renderer
			simpleRenderer.Symbol = (ISymbol) fillSymbol;

			//Set the renderer property of the geo feature layer
			geoFeatureLayer.Renderer = (IFeatureRenderer) simpleRenderer;
		}

		private void cmdFullExtent_Click(object sender, System.EventArgs e)
		{
			//Assign map controls extent property to the full extent of all the layers
			axMapControl1.Extent = axMapControl1.FullExtent;
		}

		private void cmdReset_Click(object sender, System.EventArgs e)
		{
			//Get rid of the line and points collection
			m_Polyline = null;
			m_PointCollection = null;
			//Refresh the foreground thereby removing any text annotation
			axMapControl1.Refresh(esriViewDrawPhase.esriViewForeground, Type.Missing, Type.Missing);
		}

		private IRgbColor GetRGBColor(int red,int green, int blue) 
		{
			//Create rgb color and grab hold of the IRGBColor interface
			IRgbColor rGB = new RgbColorClass(); 
			//Set rgb color properties
			rGB.Red = red;
			rGB.Green = green;
			rGB.Blue = blue;
			rGB.UseWindowsDithering = true;
			return rGB;
		}

		private void axMapControl1_OnMouseDown(object sender, ESRI.ArcGIS.Controls.IMapControlEvents2_OnMouseDownEvent e)
		{
			//If left hand mouse button
			if (e.button == 1) 
			{
				//Create a point and grab hold of the IPoint interface
				IPoint point = new PointClass();
				//Set point properties
				point.X = e.mapX;
				point.Y = e.mapY;

				//If this is the first point of a new line
				if (m_Polyline == null)
				{
					//Create the forms private polyline member and grab hold of the IPolyline interface
					m_Polyline = new PolylineClass();
				}

				//QI for the IPointsCollection interface using the IPolyline interface
				object o = Type.Missing;
				//object o1 = m_PointCollection.PointCount-1;
				m_PointCollection = (IPointCollection) m_Polyline;
				m_PointCollection.AddPoint(point, ref o, ref o);

				//Refresh the foreground thereby removing any text annotation
				axMapControl1.Refresh(esriViewDrawPhase.esriViewForeground, Type.Missing,Type.Missing);
			}
			else
			{
				//If right or middle mouse button zoom to user defined rectangle
				//Create an envelope and grab hold of the IEnvelope interface
				IEnvelope envelope = axMapControl1.TrackRectangle();
				//If user dragged a rectangle
				if (envelope != null)
				{
					//Set map controls extent property
					axMapControl1.Extent = envelope;
				}
			}
		}

		private void axMapControl1_OnAfterDraw(object sender, ESRI.ArcGIS.Controls.IMapControlEvents2_OnAfterDrawEvent e)
		{
			//If foreground refreshed
			if (e.viewDrawPhase == (int) esriViewDrawPhase.esriViewForeground)
			{
				//If a line object for splining text exists
				if (m_Polyline != null) 
				{
					//Ensure there's at least two points in the line
					if (m_PointCollection.PointCount > 1)
					{
						//Create a line symbol and grab hold of the ILineSymbol interface
						ILineSymbol lineSymbol = new SimpleLineSymbolClass();
						//Set line symbol properties
						lineSymbol.Color = GetRGBColor(0, 0, 0);
						lineSymbol.Width = 2;

						//Create a text symbol and grab hold of the ITextSymbol interface
						ITextSymbol textSymbol = new TextSymbolClass();
						//Create a system drawing font symbol with the specified properties
						System.Drawing.Font drawFont = new System.Drawing.Font("Arial", 16, FontStyle.Bold);

						//Set the text symbol font by getting the IFontDisp interface
						textSymbol.Font =  (stdole.IFontDisp) OLE.GetIFontDispFromFont(drawFont);
						textSymbol.Color = GetRGBColor(0, 0, 0);

						//Create a text path and grab hold of the ITextPath interface
						ITextPath textPath = new BezierTextPathClass();  //to spline the text
						//Grab hold of the ISimpleTextSymbol interface through the ITextSymbol interface
						ISimpleTextSymbol simpleTextSymbol = (ISimpleTextSymbol) textSymbol;
						//Set the text path of the simple text symbol
						simpleTextSymbol.TextPath = textPath;

						//Draw the line object and spline the user text around the line
						object oLineSymbol = lineSymbol;
						object oTextSymbol = textSymbol;
						axMapControl1.DrawShape(m_Polyline, ref oLineSymbol);
						axMapControl1.DrawText(m_Polyline, Text1.Text, ref oTextSymbol);
					}
				}
			}
		}

	}
}
