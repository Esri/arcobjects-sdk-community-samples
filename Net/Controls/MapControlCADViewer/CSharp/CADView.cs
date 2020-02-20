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
using System;
using System.Windows.Forms;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS;

namespace CADView
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class CADView : System.Windows.Forms.Form
	{
		public System.Windows.Forms.Button CmdFullExtent;
		public System.Windows.Forms.Label Label2;
		public System.Windows.Forms.Label Label1;
		private ESRI.ArcGIS.Controls.AxMapControl axMapControl1;
		private ESRI.ArcGIS.Controls.AxLicenseControl axLicenseControl1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public CADView()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(CADView));
			this.CmdFullExtent = new System.Windows.Forms.Button();
			this.Label2 = new System.Windows.Forms.Label();
			this.Label1 = new System.Windows.Forms.Label();
			this.axMapControl1 = new ESRI.ArcGIS.Controls.AxMapControl();
			this.axLicenseControl1 = new ESRI.ArcGIS.Controls.AxLicenseControl();
			((System.ComponentModel.ISupportInitialize)(this.axMapControl1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).BeginInit();
			this.SuspendLayout();
			// 
			// CmdFullExtent
			// 
			this.CmdFullExtent.BackColor = System.Drawing.SystemColors.Control;
			this.CmdFullExtent.Cursor = System.Windows.Forms.Cursors.Default;
			this.CmdFullExtent.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.CmdFullExtent.ForeColor = System.Drawing.SystemColors.ControlText;
			this.CmdFullExtent.Location = new System.Drawing.Point(8, 328);
			this.CmdFullExtent.Name = "CmdFullExtent";
			this.CmdFullExtent.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.CmdFullExtent.Size = new System.Drawing.Size(97, 33);
			this.CmdFullExtent.TabIndex = 4;
			this.CmdFullExtent.Text = "Full Extent";
			this.CmdFullExtent.Click += new System.EventHandler(this.CmdFullExtent_Click);
			// 
			// Label2
			// 
			this.Label2.BackColor = System.Drawing.SystemColors.Control;
			this.Label2.Cursor = System.Windows.Forms.Cursors.Default;
			this.Label2.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Label2.ForeColor = System.Drawing.SystemColors.ControlText;
			this.Label2.Location = new System.Drawing.Point(112, 344);
			this.Label2.Name = "Label2";
			this.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Label2.Size = new System.Drawing.Size(401, 17);
			this.Label2.TabIndex = 6;
			this.Label2.Text = "Right mouse button to pan.";
			// 
			// Label1
			// 
			this.Label1.BackColor = System.Drawing.SystemColors.Control;
			this.Label1.Cursor = System.Windows.Forms.Cursors.Default;
			this.Label1.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Label1.ForeColor = System.Drawing.SystemColors.ControlText;
			this.Label1.Location = new System.Drawing.Point(112, 328);
			this.Label1.Name = "Label1";
			this.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Label1.Size = new System.Drawing.Size(401, 17);
			this.Label1.TabIndex = 5;
			this.Label1.Text = "Left mouse button to drag a rectangle to zoom in.";
			// 
			// axMapControl1
			// 
			this.axMapControl1.Location = new System.Drawing.Point(8, 8);
			this.axMapControl1.Name = "axMapControl1";
			this.axMapControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axMapControl1.OcxState")));
			this.axMapControl1.Size = new System.Drawing.Size(576, 312);
			this.axMapControl1.TabIndex = 7;
			this.axMapControl1.OnMouseDown += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMouseDownEventHandler(this.axMapControl1_OnMouseDown);
			// 
			// axLicenseControl1
			// 
			this.axLicenseControl1.Enabled = true;
			this.axLicenseControl1.Location = new System.Drawing.Point(24, 24);
			this.axLicenseControl1.Name = "axLicenseControl1";
			this.axLicenseControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axLicenseControl1.OcxState")));
			this.axLicenseControl1.Size = new System.Drawing.Size(200, 50);
			this.axLicenseControl1.TabIndex = 8;
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(592, 366);
			this.Controls.Add(this.axLicenseControl1);
			this.Controls.Add(this.axMapControl1);
			this.Controls.Add(this.CmdFullExtent);
			this.Controls.Add(this.Label2);
			this.Controls.Add(this.Label1);
			this.Name = "Form1";
			this.Text = "MapControl CAD Viewer";
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
            Application.Run(new CADView());
		}

		private void Form1_Load(object sender, System.EventArgs e)
		{
			//Set passed file to variable
			string[] arguments = Environment.GetCommandLineArgs();
			if (arguments.Length == 1)
			{
				MessageBox.Show("No filename passed", "CAD Fileviewer");
				this.Close();
				return;
			}

			string workspacePath = System.IO.Path.GetDirectoryName(arguments[1]);
			string fileName = System.IO.Path.GetFileName(arguments[1]);

			//Add passed file to MapControl
			ICadDrawingDataset cadDrawingDataset = GetCadDataset(workspacePath, fileName);
			if (cadDrawingDataset == null) return;
			ICadLayer cadLayer = new CadLayerClass();
			cadLayer.CadDrawingDataset = cadDrawingDataset;
			cadLayer.Name = fileName;
			axMapControl1.AddLayer(cadLayer,0);
		}

		private void CmdFullExtent_Click(object sender, System.EventArgs e)
		{
			//Get the MapContol's full extent and set the current extent to this
			axMapControl1.Extent = axMapControl1.FullExtent;
		}
	
		private ICadDrawingDataset GetCadDataset(string cadWorkspacePath, string cadFileName)
		{
			//Create a WorkspaceName object
			IWorkspaceName workspaceName = new WorkspaceNameClass();
			workspaceName.WorkspaceFactoryProgID = "esriDataSourcesFile.CadWorkspaceFactory";
			workspaceName.PathName = cadWorkspacePath;

			//Create a CadDrawingName object
			IDatasetName cadDatasetName = new CadDrawingNameClass();
			cadDatasetName.Name = cadFileName;
			cadDatasetName.WorkspaceName = workspaceName;

			//Open the CAD drawing
			IName name = (IName) cadDatasetName;
			return (ICadDrawingDataset) name.Open();
		}

		private void axMapControl1_OnMouseDown(object sender, ESRI.ArcGIS.Controls.IMapControlEvents2_OnMouseDownEvent e)
		{
			//Check which button has been pressed by the user
			if (e.button == 1)
			{
				//Left button - Track a Rectangle and use this to set the MapControl's extent
				axMapControl1.Extent = axMapControl1.TrackRectangle();
			}
			else
			{
				//Left or middle button - Pan
				axMapControl1.Pan();
			}
		}

	}
}
