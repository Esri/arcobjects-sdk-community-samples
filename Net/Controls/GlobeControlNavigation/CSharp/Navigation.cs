using System;
using System.Windows.Forms;
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.GlobeCore;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS;


namespace GlobeNavigation
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Navigation : System.Windows.Forms.Form
	{
		internal System.Windows.Forms.TrackBar TrackBar1;
		public System.Windows.Forms.CheckBox chkSpin;
		public System.Windows.Forms.RadioButton optTools1;
		public System.Windows.Forms.RadioButton optTools0;
		public System.Windows.Forms.Button cmdLoadDocument;
		public System.Windows.Forms.Button cmdZoomOut;
		public System.Windows.Forms.Button cmdZoomIn;
		public System.Windows.Forms.Button cmdFullExtent;
		public System.Windows.Forms.Label Label1;
		public System.Windows.Forms.Label lblNavigate;
		public System.Windows.Forms.Label lblZoom;
		public System.Windows.Forms.Label lblLoad;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		
		private ISceneViewer m_ActiveView; 
		private ICamera m_Camera;
		private IPoint m_pMousePos = new PointClass();
		private bool m_bMouseDown;
		private bool m_bZooming = false;
		private double m_dSpinSpeed = 0;
		private double m_dZoom;
		
		const double cMinZoom = 1.00002;
		const double cMaxZoom = 1.1;
		const double cDistanceZoomLimit = 200.0;
		private ESRI.ArcGIS.Controls.AxGlobeControl axGlobeControl1;
		private ESRI.ArcGIS.Controls.AxLicenseControl axLicenseControl1;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Navigation()
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
            //Release COM objects, check in extension and shutdown
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Navigation));
			this.TrackBar1 = new System.Windows.Forms.TrackBar();
			this.chkSpin = new System.Windows.Forms.CheckBox();
			this.optTools1 = new System.Windows.Forms.RadioButton();
			this.optTools0 = new System.Windows.Forms.RadioButton();
			this.cmdLoadDocument = new System.Windows.Forms.Button();
			this.cmdZoomOut = new System.Windows.Forms.Button();
			this.cmdZoomIn = new System.Windows.Forms.Button();
			this.cmdFullExtent = new System.Windows.Forms.Button();
			this.Label1 = new System.Windows.Forms.Label();
			this.lblNavigate = new System.Windows.Forms.Label();
			this.lblZoom = new System.Windows.Forms.Label();
			this.lblLoad = new System.Windows.Forms.Label();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.axGlobeControl1 = new ESRI.ArcGIS.Controls.AxGlobeControl();
			this.axLicenseControl1 = new ESRI.ArcGIS.Controls.AxLicenseControl();
			((System.ComponentModel.ISupportInitialize)(this.TrackBar1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.axGlobeControl1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).BeginInit();
			this.SuspendLayout();
			// 
			// TrackBar1
			// 
			this.TrackBar1.Location = new System.Drawing.Point(440, 432);
			this.TrackBar1.Name = "TrackBar1";
			this.TrackBar1.Size = new System.Drawing.Size(104, 45);
			this.TrackBar1.TabIndex = 26;
			this.TrackBar1.Scroll += new System.EventHandler(this.TrackBar1_Scroll);
			// 
			// chkSpin
			// 
			this.chkSpin.BackColor = System.Drawing.SystemColors.Control;
			this.chkSpin.Cursor = System.Windows.Forms.Cursors.Default;
			this.chkSpin.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.chkSpin.ForeColor = System.Drawing.SystemColors.ControlText;
			this.chkSpin.Location = new System.Drawing.Point(456, 408);
			this.chkSpin.Name = "chkSpin";
			this.chkSpin.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.chkSpin.Size = new System.Drawing.Size(57, 17);
			this.chkSpin.TabIndex = 25;
			this.chkSpin.Text = "Spin";
			this.chkSpin.Click += new System.EventHandler(this.chkSpin_Click);
			// 
			// optTools1
			// 
			this.optTools1.Appearance = System.Windows.Forms.Appearance.Button;
			this.optTools1.BackColor = System.Drawing.SystemColors.Control;
			this.optTools1.Cursor = System.Windows.Forms.Cursors.Default;
			this.optTools1.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.optTools1.ForeColor = System.Drawing.SystemColors.ControlText;
			this.optTools1.Location = new System.Drawing.Point(448, 328);
			this.optTools1.Name = "optTools1";
			this.optTools1.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.optTools1.Size = new System.Drawing.Size(81, 25);
			this.optTools1.TabIndex = 20;
			this.optTools1.TabStop = true;
			this.optTools1.Text = "Zoom In/Out";
			this.optTools1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.optTools1.Click += new System.EventHandler(this.MixedControls_Click);
			// 
			// optTools0
			// 
			this.optTools0.Appearance = System.Windows.Forms.Appearance.Button;
			this.optTools0.BackColor = System.Drawing.SystemColors.Control;
			this.optTools0.Cursor = System.Windows.Forms.Cursors.Default;
			this.optTools0.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.optTools0.ForeColor = System.Drawing.SystemColors.ControlText;
			this.optTools0.Location = new System.Drawing.Point(448, 288);
			this.optTools0.Name = "optTools0";
			this.optTools0.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.optTools0.Size = new System.Drawing.Size(81, 25);
			this.optTools0.TabIndex = 19;
			this.optTools0.TabStop = true;
			this.optTools0.Text = "Navigate";
			this.optTools0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.optTools0.Click += new System.EventHandler(this.MixedControls_Click);
			// 
			// cmdLoadDocument
			// 
			this.cmdLoadDocument.BackColor = System.Drawing.SystemColors.Control;
			this.cmdLoadDocument.Cursor = System.Windows.Forms.Cursors.Default;
			this.cmdLoadDocument.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.cmdLoadDocument.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cmdLoadDocument.Location = new System.Drawing.Point(456, 48);
			this.cmdLoadDocument.Name = "cmdLoadDocument";
			this.cmdLoadDocument.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cmdLoadDocument.Size = new System.Drawing.Size(67, 25);
			this.cmdLoadDocument.TabIndex = 18;
			this.cmdLoadDocument.Text = "Load ...";
			this.cmdLoadDocument.Click += new System.EventHandler(this.cmdLoadDocument_Click);
			// 
			// cmdZoomOut
			// 
			this.cmdZoomOut.BackColor = System.Drawing.SystemColors.Control;
			this.cmdZoomOut.Cursor = System.Windows.Forms.Cursors.Default;
			this.cmdZoomOut.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.cmdZoomOut.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cmdZoomOut.Location = new System.Drawing.Point(440, 168);
			this.cmdZoomOut.Name = "cmdZoomOut";
			this.cmdZoomOut.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cmdZoomOut.Size = new System.Drawing.Size(99, 25);
			this.cmdZoomOut.TabIndex = 17;
			this.cmdZoomOut.Text = "Fixed Zoom Out";
			this.cmdZoomOut.Click += new System.EventHandler(this.cmdZoomOut_Click);
			// 
			// cmdZoomIn
			// 
			this.cmdZoomIn.BackColor = System.Drawing.SystemColors.Control;
			this.cmdZoomIn.Cursor = System.Windows.Forms.Cursors.Default;
			this.cmdZoomIn.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.cmdZoomIn.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cmdZoomIn.Location = new System.Drawing.Point(440, 136);
			this.cmdZoomIn.Name = "cmdZoomIn";
			this.cmdZoomIn.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cmdZoomIn.Size = new System.Drawing.Size(99, 25);
			this.cmdZoomIn.TabIndex = 16;
			this.cmdZoomIn.Text = "Fixed Zoom In";
			this.cmdZoomIn.Click += new System.EventHandler(this.cmdZoomIn_Click);
			// 
			// cmdFullExtent
			// 
			this.cmdFullExtent.BackColor = System.Drawing.SystemColors.Control;
			this.cmdFullExtent.Cursor = System.Windows.Forms.Cursors.Default;
			this.cmdFullExtent.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.cmdFullExtent.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cmdFullExtent.Location = new System.Drawing.Point(440, 200);
			this.cmdFullExtent.Name = "cmdFullExtent";
			this.cmdFullExtent.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cmdFullExtent.Size = new System.Drawing.Size(99, 25);
			this.cmdFullExtent.TabIndex = 15;
			this.cmdFullExtent.Text = "Full Extent";
			this.cmdFullExtent.Click += new System.EventHandler(this.cmdFullExtent_Click);
			// 
			// Label1
			// 
			this.Label1.BackColor = System.Drawing.SystemColors.Control;
			this.Label1.Cursor = System.Windows.Forms.Cursors.Default;
			this.Label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Label1.ForeColor = System.Drawing.SystemColors.Highlight;
			this.Label1.Location = new System.Drawing.Point(440, 360);
			this.Label1.Name = "Label1";
			this.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Label1.Size = new System.Drawing.Size(97, 49);
			this.Label1.TabIndex = 24;
			this.Label1.Text = "Control the spin speed with the slider.";
			// 
			// lblNavigate
			// 
			this.lblNavigate.BackColor = System.Drawing.SystemColors.Control;
			this.lblNavigate.Cursor = System.Windows.Forms.Cursors.Default;
			this.lblNavigate.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblNavigate.ForeColor = System.Drawing.SystemColors.Highlight;
			this.lblNavigate.Location = new System.Drawing.Point(440, 232);
			this.lblNavigate.Name = "lblNavigate";
			this.lblNavigate.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.lblNavigate.Size = new System.Drawing.Size(105, 49);
			this.lblNavigate.TabIndex = 23;
			this.lblNavigate.Text = "Use the option buttons to select a navigation tool. ";
			// 
			// lblZoom
			// 
			this.lblZoom.BackColor = System.Drawing.SystemColors.Control;
			this.lblZoom.Cursor = System.Windows.Forms.Cursors.Default;
			this.lblZoom.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblZoom.ForeColor = System.Drawing.SystemColors.Highlight;
			this.lblZoom.Location = new System.Drawing.Point(440, 80);
			this.lblZoom.Name = "lblZoom";
			this.lblZoom.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.lblZoom.Size = new System.Drawing.Size(104, 49);
			this.lblZoom.TabIndex = 22;
			this.lblZoom.Text = "Use the buttons to navigate the Globe data.";
			// 
			// lblLoad
			// 
			this.lblLoad.BackColor = System.Drawing.SystemColors.Control;
			this.lblLoad.Cursor = System.Windows.Forms.Cursors.Default;
			this.lblLoad.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblLoad.ForeColor = System.Drawing.SystemColors.Highlight;
			this.lblLoad.Location = new System.Drawing.Point(440, 8);
			this.lblLoad.Name = "lblLoad";
			this.lblLoad.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.lblLoad.Size = new System.Drawing.Size(97, 33);
			this.lblLoad.TabIndex = 21;
			this.lblLoad.Text = "Browse to a 3dd file to load.";
			// 
			// axGlobeControl1
			// 
			this.axGlobeControl1.Location = new System.Drawing.Point(8, 8);
			this.axGlobeControl1.Name = "axGlobeControl1";
			this.axGlobeControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axGlobeControl1.OcxState")));
			this.axGlobeControl1.Size = new System.Drawing.Size(424, 464);
			this.axGlobeControl1.TabIndex = 27;
			this.axGlobeControl1.OnMouseDown += new ESRI.ArcGIS.Controls.IGlobeControlEvents_Ax_OnMouseDownEventHandler(this.axGlobeControl1_OnMouseDown);
			this.axGlobeControl1.OnMouseMove += new ESRI.ArcGIS.Controls.IGlobeControlEvents_Ax_OnMouseMoveEventHandler(this.axGlobeControl1_OnMouseMove);
			this.axGlobeControl1.OnMouseUp += new ESRI.ArcGIS.Controls.IGlobeControlEvents_Ax_OnMouseUpEventHandler(this.axGlobeControl1_OnMouseUp);
			// 
			// axLicenseControl1
			// 
			this.axLicenseControl1.Enabled = true;
			this.axLicenseControl1.Location = new System.Drawing.Point(216, 24);
			this.axLicenseControl1.Name = "axLicenseControl1";
			this.axLicenseControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axLicenseControl1.OcxState")));
			this.axLicenseControl1.Size = new System.Drawing.Size(200, 50);
			this.axLicenseControl1.TabIndex = 28;
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(552, 478);
			this.Controls.Add(this.axLicenseControl1);
			this.Controls.Add(this.axGlobeControl1);
			this.Controls.Add(this.TrackBar1);
			this.Controls.Add(this.chkSpin);
			this.Controls.Add(this.optTools1);
			this.Controls.Add(this.optTools0);
			this.Controls.Add(this.cmdLoadDocument);
			this.Controls.Add(this.cmdZoomOut);
			this.Controls.Add(this.cmdZoomIn);
			this.Controls.Add(this.cmdFullExtent);
			this.Controls.Add(this.Label1);
			this.Controls.Add(this.lblNavigate);
			this.Controls.Add(this.lblZoom);
			this.Controls.Add(this.lblLoad);
			this.Name = "Form1";
			this.Text = "Form1";
			this.Load += new System.EventHandler(this.Form1_Load);
			((System.ComponentModel.ISupportInitialize)(this.TrackBar1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.axGlobeControl1)).EndInit();
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

            Application.Run(new Navigation());
		}

		private void Form1_Load(object sender, System.EventArgs e)
		{
			//Initialize member variables
			m_ActiveView = axGlobeControl1.GlobeDisplay.ActiveViewer;
			m_Camera = m_ActiveView.Camera;
		}

		private void cmdLoadDocument_Click(object sender, System.EventArgs e)
		{
			//Open a file dialog for selecting map documents
			openFileDialog1.Title = "Globe Documents";
			openFileDialog1.DefaultExt = ".3dd";
			openFileDialog1.Filter = "Globe Document(*.3dd)|*.3dd";
			openFileDialog1.ShowDialog();

			//Check a file is selected and that it's a valid Globe document
			//before attempting to load it
			if (openFileDialog1.FileName != "")
			{
				if (axGlobeControl1.Check3dFile(openFileDialog1.FileName) == true)
				{
					axGlobeControl1.Load3dFile(openFileDialog1.FileName);
				}
				else
				{
					MessageBox.Show("Cannot load " + openFileDialog1.FileName);
				}
			}
		}

		private void cmdFullExtent_Click(object sender, System.EventArgs e)
		{
			IGlobeCamera camera = (IGlobeCamera) m_Camera;

			//Make sure that the camera is using global coordinates
			camera.OrientationMode = esriGlobeCameraOrientationMode.esriGlobeCameraOrientationGlobal;

			//Point the camera at 0,0 in lat,long - this is the default target
			IPoint target = new PointClass();
			target.PutCoords(0.0, 0.0);
			target.Z = 0.0;
			m_Camera.Target = target;

			//Reset the camera to its default values
			m_Camera.Azimuth = 140;
			m_Camera.Inclination = 45;
			m_Camera.ViewingDistance = 4.0;
			m_Camera.ViewFieldAngle = 30.0;
			m_Camera.RollAngle = 0.0;
			m_Camera.RecalcUp();
			m_ActiveView.Redraw(false);
		}

		private void cmdZoomIn_Click(object sender, System.EventArgs e)
		{
			//Reset the camera field of view angle to 0.9 of its previous value
			double vfa = m_Camera.ViewFieldAngle;
			m_Camera.ViewFieldAngle = vfa * 0.9;
			m_ActiveView.Redraw(false);
		}

		private void cmdZoomOut_Click(object sender, System.EventArgs e)
		{
			//Reset the camera field of view angle to 1.1 times its previous value
			double vfa = m_Camera.ViewFieldAngle;
			m_Camera.ViewFieldAngle = vfa * 1.1;
			m_ActiveView.Redraw(false);
		}

		private void CalculateMoveFactors(double dist)
		{
			bool bIsAtCenter;
			int indexGlobe;

			//See if the camera is pointing at the center of the globe.
			IGlobeViewer globeViewer = (IGlobeViewer) m_ActiveView;
			globeViewer.GetIsTargetAtCenter(out bIsAtCenter, out indexGlobe);

			//If the camera is pointing at the center of the globe then the zoom speed
			//depends on how far away it is, otherwise the zoom factor is fixed. As the
			//camera approaches the globe surface (where dist = 1) the zoom speed slows
			//down. The other factors were worked out by trial and error.
			if (bIsAtCenter == true)
			{
				m_dZoom = cMinZoom + (cMaxZoom - cMinZoom) * (dist - 1.0) / 3.0;
			}
			else
			{
				m_dZoom = 1.1;
			}
		}

		private void chkSpin_Click(object sender, System.EventArgs e)
		{
			if (chkSpin.CheckState == CheckState.Checked)
            {
				axGlobeControl1.GlobeViewer.StartSpinning(esriGlobeSpinDirection.esriCounterClockwise);
				axGlobeControl1.GlobeViewer.SpinSpeed = 3;
				TrackBar1.Enabled = true;
				m_dSpinSpeed = 3;
			}
			else
			{
				axGlobeControl1.GlobeViewer.StopSpinning();
				TrackBar1.Enabled = false;
				m_dSpinSpeed = 0;
			}
		}

		private void TrackBar1_Scroll(object sender, System.EventArgs e)
		{
			//The globe should increase its spin speed with every increment greater than
			//5 and decrease it for every increment less.
			int sliderPos = (TrackBar1.Value - 5);
			if (sliderPos == 0)
			{
				m_dSpinSpeed = 3;
			}
			else if (sliderPos > 0) 
			{
				m_dSpinSpeed = 3 * (sliderPos + 1);
			}
			else
			{
				m_dSpinSpeed = 3 / (1 - sliderPos);
			}

			axGlobeControl1.GlobeViewer.SpinSpeed = m_dSpinSpeed;
		}

		private void MixedControls_Click(object sender, System.EventArgs e)
		{
			RadioButton b = (RadioButton) sender;
			//Set current tool
			switch (b.Name)
			{
				case "optTools0":
					m_bZooming = false;
					axGlobeControl1.Navigate = true;
					axGlobeControl1.MousePointer = esriControlsMousePointer.esriPointerDefault;
					break;
				case "optTools1":
					m_bZooming = true;
					axGlobeControl1.Navigate = false;
					axGlobeControl1.MousePointer = esriControlsMousePointer.esriPointerZoom;
					break;
			}
		}

		private void axGlobeControl1_OnMouseUp(object sender, ESRI.ArcGIS.Controls.IGlobeControlEvents_OnMouseUpEvent e)
		{
			//Cancel the zoom operation
			m_bMouseDown = false;
			//Navigate can cancel spin - make sure it starts again
			if (m_dSpinSpeed > 0)
			{
				axGlobeControl1.GlobeViewer.StartSpinning(esriGlobeSpinDirection.esriCounterClockwise);
			}
		}

		private void axGlobeControl1_OnMouseMove(object sender, ESRI.ArcGIS.Controls.IGlobeControlEvents_OnMouseMoveEvent e)
		{
			if (m_bMouseDown == false)
			{
				return;
			}

			//If we're in a zoom operation then
			//Get the difference in mouse position between this and the last one
			double dx = e.x - m_pMousePos.X;
			double dy = e.y - m_pMousePos.Y;
			if ((dx == 0) & (dy == 0)) 
			{
				return;					   
			}

			//Work out how far the observer is currently from the globe and use this
			//distance to determine how far to move
			IPoint observer = m_Camera.Observer;
			double zObs;
			double xObs; 
			double yObs;
			double dist; 
			observer.QueryCoords(out xObs, out yObs);
			zObs = observer.Z;
			dist = System.Math.Sqrt(xObs * xObs + yObs * yObs + zObs * zObs);
			CalculateMoveFactors(dist);

			//Zoom out and in as the mouse moves up and down the screen respectively
			if ((dy < 0) & (dist < cDistanceZoomLimit)) 
			{
				m_Camera.Zoom(m_dZoom);
			}
			else if (dy > 0) 
			{
				m_Camera.Zoom((1 / m_dZoom));
			}

			m_pMousePos.X = e.x;
			m_pMousePos.Y = e.y;
			m_ActiveView.Redraw(false);
		}

		private void axGlobeControl1_OnMouseDown(object sender, ESRI.ArcGIS.Controls.IGlobeControlEvents_OnMouseDownEvent e)
		{
			//Mouse down should initiate a zoom only if the Zoom check box is checked
			if (m_bZooming == false)
			{
				return;
			}

			if (e.button == 1)
			{
				m_bMouseDown = true;
				m_pMousePos.X = e.x;
				m_pMousePos.Y = e.y;
			}
		}

	}
}
