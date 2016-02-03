using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.PublisherControls;

namespace SpinGlobe
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class SpinGlobe : System.Windows.Forms.Form
	{
		internal System.Windows.Forms.Label lblFaster;
		internal System.Windows.Forms.Label lblSlower;
		internal System.Windows.Forms.TrackBar TrackBar1;
		internal System.Windows.Forms.Button btnStop;
		internal System.Windows.Forms.Button btnClockwise;
		internal System.Windows.Forms.Button btnAntiClockwise;
		internal System.Windows.Forms.Button btnLoad;
		private ESRI.ArcGIS.PublisherControls.AxArcReaderGlobeControl axArcReaderGlobeControl1;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private bool m_RotateDirection;
		private double m_CurLat;
		private double m_CurLong;
		private double m_CurElev;
		private double i;
		private System.Windows.Forms.Timer timer1;
		private System.ComponentModel.IContainer components;

		public SpinGlobe()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(SpinGlobe));
			this.lblFaster = new System.Windows.Forms.Label();
			this.lblSlower = new System.Windows.Forms.Label();
			this.TrackBar1 = new System.Windows.Forms.TrackBar();
			this.btnStop = new System.Windows.Forms.Button();
			this.btnClockwise = new System.Windows.Forms.Button();
			this.btnAntiClockwise = new System.Windows.Forms.Button();
			this.btnLoad = new System.Windows.Forms.Button();
			this.axArcReaderGlobeControl1 = new ESRI.ArcGIS.PublisherControls.AxArcReaderGlobeControl();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			((System.ComponentModel.ISupportInitialize)(this.TrackBar1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.axArcReaderGlobeControl1)).BeginInit();
			this.SuspendLayout();
			// 
			// lblFaster
			// 
			this.lblFaster.Location = new System.Drawing.Point(152, 16);
			this.lblFaster.Name = "lblFaster";
			this.lblFaster.Size = new System.Drawing.Size(40, 20);
			this.lblFaster.TabIndex = 14;
			this.lblFaster.Text = "Faster";
			// 
			// lblSlower
			// 
			this.lblSlower.Location = new System.Drawing.Point(360, 16);
			this.lblSlower.Name = "lblSlower";
			this.lblSlower.Size = new System.Drawing.Size(40, 20);
			this.lblSlower.TabIndex = 13;
			this.lblSlower.Text = "Slower";
			// 
			// TrackBar1
			// 
			this.TrackBar1.Location = new System.Drawing.Point(192, 8);
			this.TrackBar1.Maximum = 1000;
			this.TrackBar1.Minimum = 100;
			this.TrackBar1.Name = "TrackBar1";
			this.TrackBar1.Size = new System.Drawing.Size(164, 45);
			this.TrackBar1.TabIndex = 12;
			this.TrackBar1.TickStyle = System.Windows.Forms.TickStyle.None;
			this.TrackBar1.Value = 100;
			this.TrackBar1.ValueChanged += new System.EventHandler(this.TrackBar1_ValueChanged);
			// 
			// btnStop
			// 
			this.btnStop.Location = new System.Drawing.Point(512, 16);
			this.btnStop.Name = "btnStop";
			this.btnStop.Size = new System.Drawing.Size(76, 36);
			this.btnStop.TabIndex = 11;
			this.btnStop.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
			// 
			// btnClockwise
			// 
			this.btnClockwise.Location = new System.Drawing.Point(424, 16);
			this.btnClockwise.Name = "btnClockwise";
			this.btnClockwise.Size = new System.Drawing.Size(76, 36);
			this.btnClockwise.TabIndex = 10;
			this.btnClockwise.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.btnClockwise.Click += new System.EventHandler(this.btnClockwise_Click);
			// 
			// btnAntiClockwise
			// 
			this.btnAntiClockwise.Location = new System.Drawing.Point(592, 16);
			this.btnAntiClockwise.Name = "btnAntiClockwise";
			this.btnAntiClockwise.Size = new System.Drawing.Size(76, 36);
			this.btnAntiClockwise.TabIndex = 9;
			this.btnAntiClockwise.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.btnAntiClockwise.Click += new System.EventHandler(this.btnAntiClockwise_Click);
			// 
			// btnLoad
			// 
			this.btnLoad.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.btnLoad.Location = new System.Drawing.Point(8, 16);
			this.btnLoad.Name = "btnLoad";
			this.btnLoad.Size = new System.Drawing.Size(76, 36);
			this.btnLoad.TabIndex = 8;
			this.btnLoad.Text = "Load";
			this.btnLoad.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
			// 
			// axArcReaderGlobeControl1
			// 
			this.axArcReaderGlobeControl1.Location = new System.Drawing.Point(8, 64);
			this.axArcReaderGlobeControl1.Name = "axArcReaderGlobeControl1";
			this.axArcReaderGlobeControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axArcReaderGlobeControl1.OcxState")));
			this.axArcReaderGlobeControl1.Size = new System.Drawing.Size(656, 376);
			this.axArcReaderGlobeControl1.TabIndex = 15;
			this.axArcReaderGlobeControl1.OnDocumentUnloaded += new System.EventHandler(this.axArcReaderGlobeControl1_OnDocumentUnloaded);
			this.axArcReaderGlobeControl1.OnDocumentLoaded += new ESRI.ArcGIS.PublisherControls.IARGlobeControlEvents_Ax_OnDocumentLoadedEventHandler(this.axArcReaderGlobeControl1_OnDocumentLoaded);
			this.axArcReaderGlobeControl1.OnMouseUp += new ESRI.ArcGIS.PublisherControls.IARGlobeControlEvents_Ax_OnMouseUpEventHandler(this.axArcReaderGlobeControl1_OnMouseUp);
			// 
			// timer1
			// 
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// SpinGlobe
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(676, 450);
			this.Controls.Add(this.axArcReaderGlobeControl1);
			this.Controls.Add(this.lblFaster);
			this.Controls.Add(this.lblSlower);
			this.Controls.Add(this.TrackBar1);
			this.Controls.Add(this.btnStop);
			this.Controls.Add(this.btnClockwise);
			this.Controls.Add(this.btnAntiClockwise);
			this.Controls.Add(this.btnLoad);
			this.Name = "SpinGlobe";
			this.Text = "SpinGlobe";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.SpinGlobe_Closing);
			this.Load += new System.EventHandler(this.SpinGlobe_Load);
			((System.ComponentModel.ISupportInitialize)(this.TrackBar1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.axArcReaderGlobeControl1)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
            if (!ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.ArcReader))
            {
                if (!ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.EngineOrDesktop))
                {
                    MessageBox.Show("Unable to bind to ArcGIS runtime. Application will be shut down.");
                    return;
                }
            }

			Application.Run(new SpinGlobe());
		}

		private void SpinGlobe_Load(object sender, System.EventArgs e)
		{
			//Load command button images
			System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap (GetType().Assembly.GetManifestResourceStream(GetType(), "browse.bmp"));
			bitmap.MakeTransparent(System.Drawing.Color.Teal);
			btnLoad.Image = bitmap;
			bitmap = new System.Drawing.Bitmap (GetType().Assembly.GetManifestResourceStream(GetType(), "spin_counterclockwise.bmp"));
			bitmap.MakeTransparent(System.Drawing.Color.Teal);
			btnAntiClockwise.Image = bitmap;
			bitmap = new System.Drawing.Bitmap (GetType().Assembly.GetManifestResourceStream(GetType(), "spin_clockwise.bmp"));
			bitmap.MakeTransparent(System.Drawing.Color.Teal);
			btnClockwise.Image = bitmap;
			bitmap = new System.Drawing.Bitmap (GetType().Assembly.GetManifestResourceStream(GetType(), "spin_stop.bmp"));
			bitmap.MakeTransparent(System.Drawing.Color.Teal);
			btnStop.Image = bitmap;

			//Set the current Slider value and timer interval to 100 milliseconds
			//Any faster and the Globe will not be able to refresh fast enough
			TrackBar1.Value = 100;
			timer1.Interval = 100;
			timer1.Enabled = false;
            i = 0;

			//Disable controls until doc is loaded
			btnAntiClockwise.Enabled = false;
			btnClockwise.Enabled = false;
			btnStop.Enabled = false;


		}

		private void btnLoad_Click(object sender, System.EventArgs e)
		{
		
			
			//Open a file dialog for selecting map documents
			openFileDialog1.Title = "Select Published Map Document";
			openFileDialog1.Filter = "Published Map Documents (*.pmf)|*.pmf";
			openFileDialog1.ShowDialog();

			//Exit if no map document is selected
			string sFilePath = "";
			sFilePath = openFileDialog1.FileName;
			if (sFilePath == "")
			{
				return;
			}

			//Load the specified pmf
			if (axArcReaderGlobeControl1.CheckDocument(sFilePath) == true)
			{
				axArcReaderGlobeControl1.LoadDocument(sFilePath);
			}
			else
			{
				MessageBox.Show("This document cannot be loaded!");
				return;
			}

			//Zoom to Full Extent
			axArcReaderGlobeControl1.ARGlobe.ZoomToFullExtent();

			//Set current tool to Globe Navigate
			axArcReaderGlobeControl1.CurrentARGlobeTool = ESRI.ArcGIS.PublisherControls.esriARGlobeTool.esriARGlobeToolNavigate;


		}

		private void TrackBar1_ValueChanged(object sender, System.EventArgs e)
		{
			//Update the timer interval to match the slider value
			timer1.Interval = TrackBar1.Value;
		}

		private void timer1_Tick(object sender, System.EventArgs e)
		{
			//Longitude Counter
			i = m_CurLong;

			//Increment Longitude by 1 decimal degree
			if (m_RotateDirection == false)
			{
				if (i == 360)
				{
					i = 0;
				}
				i = i + 1;
			}
			else
			{
				if (i == -360)
				{
					i = 0;
				}
				i = i - 1;
			}

			//Update the current location
			axArcReaderGlobeControl1.ARGlobe.SetObserverLocation(i, m_CurLat, m_CurElev);
			axArcReaderGlobeControl1.ARGlobe.GetObserverLocation(ref m_CurLong, ref m_CurLat, ref m_CurElev);
		}

		private void axArcReaderGlobeControl1_OnDocumentUnloaded(object sender, System.EventArgs e)
		{
			btnAntiClockwise.Enabled = false;
			btnClockwise.Enabled = false;
			btnStop.Enabled = false;
		}

		private void axArcReaderGlobeControl1_OnDocumentLoaded(object sender, ESRI.ArcGIS.PublisherControls.IARGlobeControlEvents_OnDocumentLoadedEvent e)
		{
			btnAntiClockwise.Enabled = true;
			btnClockwise.Enabled = true;
			btnStop.Enabled = true;
		}

		private void SpinGlobe_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			//Release COM Objects
			ESRI.ArcGIS.ADF.COMSupport.AOUninitialize.Shutdown();
		}

		private void btnClockwise_Click(object sender, System.EventArgs e)
		{
			//Get latest location and start timer
			axArcReaderGlobeControl1.ARGlobe.GetObserverLocation(ref m_CurLong, ref m_CurLat, ref m_CurElev);
			m_RotateDirection = false;
			timer1.Enabled = true;

			//Unselect the current tool
			axArcReaderGlobeControl1.CurrentARGlobeTool = ESRI.ArcGIS.PublisherControls.esriARGlobeTool.esriARGlobeToolNoneSelected;

		}

		private void btnStop_Click(object sender, System.EventArgs e)
		{
			//Stops Timer
			timer1.Enabled = false;

			//Set the current tool to Globe Navigate
			axArcReaderGlobeControl1.CurrentARGlobeTool = ESRI.ArcGIS.PublisherControls.esriARGlobeTool.esriARGlobeToolNavigate;
		}

		private void btnAntiClockwise_Click(object sender, System.EventArgs e)
		{
			//Get latest location and start timer
			axArcReaderGlobeControl1.ARGlobe.GetObserverLocation(ref m_CurLong, ref m_CurLat, ref m_CurElev);
			m_RotateDirection = true;
			timer1.Enabled = true;

			//Unselect the current tool
			axArcReaderGlobeControl1.CurrentARGlobeTool = ESRI.ArcGIS.PublisherControls.esriARGlobeTool.esriARGlobeToolNoneSelected;
		}

		private void axArcReaderGlobeControl1_OnMouseUp(object sender, ESRI.ArcGIS.PublisherControls.IARGlobeControlEvents_OnMouseUpEvent e)
		{
			//Update m_CurLong incase observer has been repositioned as a consequence of using another tool.
			axArcReaderGlobeControl1.ARGlobe.GetObserverLocation(ref m_CurLong, ref m_CurLat, ref m_CurElev);
		}
	}
}
