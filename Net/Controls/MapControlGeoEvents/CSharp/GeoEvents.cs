/*

   Copyright 2016 Esri

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
using System;
using System.Windows.Forms;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.ADF.COMSupport;
using ESRI.ArcGIS;


namespace GeoEvents
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	/// 

	public class GeoEvents : System.Windows.Forms.Form
	{
		public System.Windows.Forms.Button cmdFullExtent;
		public System.Windows.Forms.CheckBox chkTracking;
		public System.Windows.Forms.Label Label1;
		private System.Windows.Forms.Timer timer1;
		private System.ComponentModel.IContainer components;
		private IGeographicCoordinateSystem m_GeographicCoordinateSystem; 
		private IProjectedCoordinateSystem m_ProjectedCoordinateSystem;
		private IGraphicsContainer m_GraphicsContainer;
		private IMapControl2 m_MapControl;
		private ESRI.ArcGIS.Controls.AxMapControl axMapControl1;
		private ESRI.ArcGIS.Controls.AxLicenseControl axLicenseControl1;  
		private AGENT_IN_FIELD[] agentArray = new AGENT_IN_FIELD[20]; 

		struct AGENT_IN_FIELD
		{
			public bool Located;
			public double Latitude;
			public double Longitude;
			public string CodeNumber;
		}

		public GeoEvents()
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
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(GeoEvents));
			this.cmdFullExtent = new System.Windows.Forms.Button();
			this.chkTracking = new System.Windows.Forms.CheckBox();
			this.Label1 = new System.Windows.Forms.Label();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.axMapControl1 = new ESRI.ArcGIS.Controls.AxMapControl();
			this.axLicenseControl1 = new ESRI.ArcGIS.Controls.AxLicenseControl();
			((System.ComponentModel.ISupportInitialize)(this.axMapControl1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).BeginInit();
			this.SuspendLayout();
			// 
			// cmdFullExtent
			// 
			this.cmdFullExtent.BackColor = System.Drawing.SystemColors.Control;
			this.cmdFullExtent.Cursor = System.Windows.Forms.Cursors.Default;
			this.cmdFullExtent.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.cmdFullExtent.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cmdFullExtent.Location = new System.Drawing.Point(560, 416);
			this.cmdFullExtent.Name = "cmdFullExtent";
			this.cmdFullExtent.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cmdFullExtent.Size = new System.Drawing.Size(121, 25);
			this.cmdFullExtent.TabIndex = 4;
			this.cmdFullExtent.Text = "Zoom to Full Extent";
			this.cmdFullExtent.Click += new System.EventHandler(this.cmdFullExtent_Click);
			// 
			// chkTracking
			// 
			this.chkTracking.BackColor = System.Drawing.SystemColors.Control;
			this.chkTracking.Cursor = System.Windows.Forms.Cursors.Default;
			this.chkTracking.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.chkTracking.ForeColor = System.Drawing.SystemColors.ControlText;
			this.chkTracking.Location = new System.Drawing.Point(16, 424);
			this.chkTracking.Name = "chkTracking";
			this.chkTracking.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.chkTracking.Size = new System.Drawing.Size(137, 17);
			this.chkTracking.TabIndex = 3;
			this.chkTracking.Text = "Enable GPS Tracking";
			this.chkTracking.Click += new System.EventHandler(this.chkTracking_Click);
			// 
			// Label1
			// 
			this.Label1.BackColor = System.Drawing.SystemColors.Control;
			this.Label1.Cursor = System.Windows.Forms.Cursors.Default;
			this.Label1.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Label1.ForeColor = System.Drawing.SystemColors.ControlText;
			this.Label1.Location = new System.Drawing.Point(152, 416);
			this.Label1.Name = "Label1";
			this.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Label1.Size = new System.Drawing.Size(353, 33);
			this.Label1.TabIndex = 5;
			this.Label1.Text = "Use the left hand mouse button to zoom in. Use the other mouse buttons to click o" +
				"n an agent and change the symbology.  ";
			// 
			// timer1
			// 
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// axMapControl1
			// 
			this.axMapControl1.Location = new System.Drawing.Point(8, 8);
			this.axMapControl1.Name = "axMapControl1";
			this.axMapControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axMapControl1.OcxState")));
			this.axMapControl1.Size = new System.Drawing.Size(664, 400);
			this.axMapControl1.TabIndex = 6;
			this.axMapControl1.OnMouseDown += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMouseDownEventHandler(this.axMapControl1_OnMouseDown);
			// 
			// axLicenseControl1
			// 
			this.axLicenseControl1.Enabled = true;
			this.axLicenseControl1.Location = new System.Drawing.Point(24, 16);
			this.axLicenseControl1.Name = "axLicenseControl1";
			this.axLicenseControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axLicenseControl1.OcxState")));
			this.axLicenseControl1.Size = new System.Drawing.Size(200, 50);
			this.axLicenseControl1.TabIndex = 7;
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(680, 454);
			this.Controls.Add(this.axLicenseControl1);
			this.Controls.Add(this.axMapControl1);
			this.Controls.Add(this.cmdFullExtent);
			this.Controls.Add(this.chkTracking);
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

            Application.Run(new GeoEvents());
		}

		private void Form1_Load(object sender, System.EventArgs e)
		{
			m_MapControl = (IMapControl2) axMapControl1.GetOcx();

            //Find sample data
            string filePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            filePath = System.IO.Path.Combine(filePath, @"ArcGIS\data\World");

			//Add sample shapefile data
			m_MapControl.AddShapeFile(filePath, "world30");
			m_MapControl.AddLayerFromFile(System.IO.Path.Combine(filePath , @"continents.lyr"),0);

			//Symbolize the data
			SymbolizeData(m_MapControl.get_Layer(0), 0.1, GetRGBColor(0, 0, 0), GetRGBColor(0, 128, 0));
			SymbolizeData(m_MapControl.get_Layer(1), 0.1, GetRGBColor(0, 0, 0), GetRGBColor(140, 196, 254));
			//Set up a global Geographic Coordinate System
			MakeCoordinateSystems();

			//Get the MapControl's graphics container and get the IGraphicsContainer interface
			m_GraphicsContainer = m_MapControl.ActiveView.GraphicsContainer;

			//Populate an array with agent id's and locations
			LoadAgentArray();

			//Loop through the array and display each agent location
			for (int i = 0; i <= 19; i++)
			{
				DisplayAgentLocation(agentArray[i]);
			}

			timer1.Interval = 800;
		}

		private void cmdFullExtent_Click(object sender, System.EventArgs e)
		{
			m_MapControl.Extent = m_MapControl.FullExtent;
		}

		private void timer1_Tick(object sender, System.EventArgs e)
		{
			//Distance used in calculating the new point location
			double dMaxDistance = m_MapControl.Extent.Width / 20;

			//Loop through the elements in the GraphicContainer and get the IElement interface
			m_GraphicsContainer.Reset();
			IElement element = m_GraphicsContainer.Next();
			
			Random r = new Random();
			while (element != null)
			{
				//QI for IElementProperties interface from IElement interface
				IElementProperties elementProperties = (IElementProperties) element;
				//If agent has not been located
				if (elementProperties.Name == false.ToString())
				{
					//Get hold of the IPoint interface from the elements geometry
					IPoint point = (IPoint) element.Geometry;
					//Create new random point coordinates based upon current location
					
					point.X = point.X - (dMaxDistance * (r.NextDouble() - 0.5));
					point.Y = point.Y - (dMaxDistance * (r.NextDouble() - 0.5));
					//Set the point onto the GeographicCoordinateSystem - WHERE the point came FROM
					point.Project(m_GeographicCoordinateSystem);
                    if (point.IsEmpty == false)
                    {
                        //Ensure that the point is within the horizon of the coordinate system. Mollweide
                        //has a generic rectangular horizon with the following limits:
                        //-179.999988540844, -90.000000000000, 179.999988540844, 90.000000000000
                        if (point.X > 179.999988540844)
                        {
                            point.X = point.X - 359.999977081688;
                        }
                        else if (point.X < -179.999988540844)
                        {
                            point.X = point.X + 359.999977081688;
                        }
                        if (point.Y > 89.891975)   //fudge value to clip near poles
                        {
                            point.Y = point.Y - 179.78395;
                        }
                        else if (point.Y < -89.891975)  //fudge value to clip near poles
                        {
                            point.Y = point.Y + 179.78395;
                        }
                        //Project the point onto the displays current spatial reference
                        //WHERE the point is going TO
                        point.Project(m_ProjectedCoordinateSystem);
                        element.Geometry = point;
                    }
				}
				element = m_GraphicsContainer.Next();
			}

			//Refresh the graphics
			m_MapControl.Refresh(esriViewDrawPhase.esriViewGraphics,Type.Missing,Type.Missing);
		}

		private void chkTracking_Click(object sender, System.EventArgs e)
		{
			//Turn the timer on or off
			if (chkTracking.CheckState == CheckState.Checked)
			{
				timer1.Start();
			}
			else if (chkTracking.CheckState == CheckState.Unchecked)
			{
				timer1.Stop();
			}
		}

		private IRgbColor GetRGBColor(int red,int green, int blue) 
		{
			//Create rgb color and grab hold of the IRGBColor interface
			IRgbColor rGB = new RgbColorClass(); 
			//Set rgb color properties
			rGB.Red = red;
			rGB.Green = green;
			rGB.Blue = blue;
			return rGB;
		}

		private void SymbolizeData(ILayer layer, double dWidth, IRgbColor colorLine, IRgbColor colorFill)
		{
			//Create a line symbol and get the ILineSymbol interface
			ILineSymbol lineSymbol = new SimpleLineSymbolClass();
			//Set the line symbol properties
			lineSymbol.Width = dWidth;
			lineSymbol.Color = colorLine;

			//Create a fill symbol and get the IFillSymbol interface
			ISimpleFillSymbol fillSymbol = new SimpleFillSymbolClass();
			//Set the fill symbol properties
			fillSymbol.Outline = lineSymbol;
			fillSymbol.Color = colorFill;

			//Create a simple renderer and get the ISimpleRenderer interface 
			ISimpleRenderer simpleRenderer = new SimpleRendererClass();
			//Set the simple renderer properties
			simpleRenderer.Symbol = (ISymbol) fillSymbol;

			//QI for the IGeoFeatureLayer interface from the ILayer2 interface
			IGeoFeatureLayer geoFeatureLayer = (IGeoFeatureLayer) layer;
			//Set the GeoFeatureLayer properties
			geoFeatureLayer.Renderer = (IFeatureRenderer) simpleRenderer;
		}

		private void MakeCoordinateSystems()
		{
			//Create a spatial reference environment and get theISpatialReferenceFactory2 interface
			ISpatialReferenceFactory2 spatRefFact = new SpatialReferenceEnvironmentClass();
			//Create a geographic coordinate system and get the IGeographicCoordinateSystem interface
			m_GeographicCoordinateSystem = spatRefFact.CreateGeographicCoordinateSystem((int) esriSRGeoCSType.esriSRGeoCS_WGS1984);
			//Create a projected coordinate system and get the IProjectedCoordinateSystem interface
			m_ProjectedCoordinateSystem = spatRefFact.CreateProjectedCoordinateSystem((int)esriSRProjCSType.esriSRProjCS_World_Mollweide);
			//Set the map controls spatial reference property
			m_MapControl.SpatialReference = m_ProjectedCoordinateSystem;
		}

		private ICharacterMarkerSymbol GetMarkerSymbol(bool bLocated)
		{
			//Create a new system draw font
			System.Drawing.Font drawFont = new System.Drawing.Font("ESRI Crime Analysis", 21);

			//Create a new CharacterMarkerSymbol and get the ICharacterMarkerSymbol interface
			ICharacterMarkerSymbol charMarker = new CharacterMarkerSymbolClass();
			//Set the marker symbol properties
			charMarker.Font = (stdole.IFontDisp) OLE.GetIFontDispFromFont(drawFont);
			
			if (bLocated == true)
			{
				charMarker.CharacterIndex = 56;
				charMarker.Color = GetRGBColor(255, 0, 0);
				charMarker.Size = 30;
			}
			else
			{
				charMarker.CharacterIndex = 46;
				charMarker.Color = GetRGBColor(0, 0, 0);
				charMarker.Size = 30;
			}
			return charMarker;
		}

		private void DisplayAgentLocation(AGENT_IN_FIELD agent)
		{
			//Create a point and get the IPoint interface
			IPoint point = new PointClass();
			//Set the points x and y coordinates
			point.PutCoords(agent.Longitude, agent.Latitude);
			//Set the points spatial reference - WHERE the point is coming FROM
			point.SpatialReference = m_GeographicCoordinateSystem;
			//Project the point onto the displays current spatial reference - WHERE the point is going TO
			point.Project(m_ProjectedCoordinateSystem);

			//Create a marker element and get the IElement interface
			IElement element = new MarkerElementClass();
			//Set the elements geometry
			element.Geometry = point;

			//QI for the IMarkerElement interface from the IElement interface
			IMarkerElement markerElement = (IMarkerElement) element;
			//Set the marker symbol
			markerElement.Symbol = GetMarkerSymbol(agent.Located);

			//QI for the IElementProperties interface from the IMarkerElement interface
			IElementProperties elementProperties = (IElementProperties) markerElement;
			elementProperties.Name = agent.Located.ToString();

			//Add the element to the graphics container
			m_GraphicsContainer.AddElement(element, 0);
		}

		private void LoadAgentArray()
		{
			//Populate an array of agent locations and id's. The locations are in decimal degrees,
			//based on the WGS1984 geographic coordinate system. [ie unprojected].
			//Obviously, these values could be read directly from a GPS unit.
			agentArray[0].CodeNumber = "001";
			agentArray[0].Latitude = 56.185128983308;
			agentArray[0].Longitude = 37.556904400607;
			agentArray[0].Located = false;
			agentArray[1].CodeNumber = "002";
			agentArray[1].Latitude = 48.3732928679818;
			agentArray[1].Longitude = 6.91047040971168;
			agentArray[1].Located = false;
			agentArray[2].CodeNumber = "003";
			agentArray[2].Latitude = 32.1487101669196;
			agentArray[2].Longitude = 39.3596358118361;
			agentArray[2].Located = false;
			agentArray[3].CodeNumber = "004";
			agentArray[3].Latitude = 29.7450682852807;
			agentArray[3].Longitude = 71.2078907435508;
			agentArray[3].Located = false;
			agentArray[4].CodeNumber = "005";
			agentArray[4].Latitude = 38.7587253414264;
			agentArray[4].Longitude = 138.509863429439;
			agentArray[4].Located = false;
			agentArray[5].CodeNumber = "006";
			agentArray[5].Latitude = 35.1532625189681;
			agentArray[5].Longitude = -82.0242792109256;
			agentArray[5].Located = false;
			agentArray[6].CodeNumber = "007";
			agentArray[6].Latitude = -26.1396054628225;
			agentArray[6].Longitude = 28.5432473444613;
			agentArray[6].Located = true;
			agentArray[7].CodeNumber = "008";
			agentArray[7].Latitude = 33.9514415781487;
			agentArray[7].Longitude = 3.90591805766313;
			agentArray[7].Located = false;
			agentArray[8].CodeNumber = "009";
			agentArray[8].Latitude = 29.7450682852807;
			agentArray[8].Longitude = 16.5250379362671;
			agentArray[8].Located = false;
			agentArray[9].CodeNumber = "010";
			agentArray[9].Latitude = 45.9696509863429;
			agentArray[9].Longitude = 23.1350531107739;
			agentArray[9].Located = false;
			agentArray[10].CodeNumber = "011";
			agentArray[10].Latitude = 48.9742033383915;
			agentArray[10].Longitude = 14.1213960546282;
			agentArray[10].Located = false;
			agentArray[11].CodeNumber = "012";
			agentArray[11].Latitude = 29.7450682852807;
			agentArray[11].Longitude = 79.0197268588771;
			agentArray[11].Located = false;
			agentArray[12].CodeNumber = "013";
			agentArray[12].Latitude = 43.5660091047041;
			agentArray[12].Longitude = 125.289833080425;
			agentArray[12].Located = false;
			agentArray[13].CodeNumber = "014";
			agentArray[13].Latitude = 7.5113808801214;
			agentArray[13].Longitude = -68.2033383915023;
			agentArray[13].Located = false;
			agentArray[14].CodeNumber = "015";
			agentArray[14].Latitude = 9.31411229135053;
			agentArray[14].Longitude = -79.6206373292868;
			agentArray[14].Located = false;
			agentArray[15].CodeNumber = "016";
			agentArray[15].Latitude = 8.71320182094082;
			agentArray[15].Longitude = -9.31411229135053;
			agentArray[15].Located = true;
			agentArray[16].CodeNumber = "017";
			agentArray[16].Latitude = 22.5341426403642;
			agentArray[16].Longitude = 53.7814871016692;
			agentArray[16].Located = false;
			agentArray[17].CodeNumber = "018";
			agentArray[17].Latitude = 42.3641881638847;
			agentArray[17].Longitude = 45.9696509863429;
			agentArray[17].Located = false;
			agentArray[18].CodeNumber = "019";
			agentArray[18].Latitude = 39.3596358118361;
			agentArray[18].Longitude = 27.9423368740516;
			agentArray[18].Located = false;
			agentArray[19].CodeNumber = "020";
			agentArray[19].Latitude = 22.5341426403642;
			agentArray[19].Longitude = 104.257966616085;
			agentArray[19].Located = false;
		}

		private void axMapControl1_OnMouseDown(object sender, ESRI.ArcGIS.Controls.IMapControlEvents2_OnMouseDownEvent e)
		{
			//If left mouse button then zoom in
			if (e.button == 1)
			{
				m_MapControl.Extent = m_MapControl.TrackRectangle();
			}
			else
			{
				//Create a point and get the IPoint interface
				IPoint point = new PointClass();
				//Set points coordinates
				point.PutCoords(e.mapX, e.mapY);

				//QI for ITopologicalOperator interface through IPoint interface
				ITopologicalOperator topologicalOperator = (ITopologicalOperator) point;
				//Create a polygon by buffering the point and get the IPolygon interface
				IPolygon polygon = (IPolygon) topologicalOperator.Buffer(m_MapControl.Extent.Width * 0.02);
				//QI for IRelationalOperator interface through IPolygon interface
				IRelationalOperator relationalOperator = (IRelationalOperator) polygon;

				object o = null;
				//Draw the polygon
				m_MapControl.DrawShape(polygon, ref o);

				//Loop through the elements in the GraphicContainer and get the IElement interface
				m_GraphicsContainer.Reset();
				IElement element = m_GraphicsContainer.Next();
				while (element != null)
				{
					//If the polygon contains the point
					if (relationalOperator.Contains(element.Geometry) == true)
					{
						//QI for IMarkerElement interface through IElement interface
						IMarkerElement markerElement = (IMarkerElement) element;
						markerElement.Symbol = GetMarkerSymbol(true);
						//QI for the IElementProperties interface through IElement interface
						IElementProperties elementProperties = (IElementProperties) element;
						elementProperties.Name = true.ToString();
					}
					element = m_GraphicsContainer.Next();
				}
				if (chkTracking.CheckState == CheckState.Unchecked)
					//Refresh the graphics
					m_MapControl.Refresh(esriViewDrawPhase.esriViewGraphics, Type.Missing, Type.Missing);
			}
		}
	}
}
