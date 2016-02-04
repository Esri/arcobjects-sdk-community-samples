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
using ESRI.ArcGIS.PublisherControls;

namespace MapTools
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class MapTools : System.Windows.Forms.Form
	{
		public System.Windows.Forms.Button cmdLoad;
		public System.Windows.Forms.TextBox txbPath;
		public System.Windows.Forms.Label _Label3_1;
		public System.Windows.Forms.Label Label6;
		public System.Windows.Forms.Label Label4;
		public System.Windows.Forms.Label _Label3_0;
		public System.Windows.Forms.Label Label2;
		public System.Windows.Forms.Button cmdRedo;
		public System.Windows.Forms.Button cmdUndo;
		public System.Windows.Forms.Button cmdFullExtent;
		public System.Windows.Forms.RadioButton optTool2;
		public System.Windows.Forms.RadioButton optTool1;
		public System.Windows.Forms.RadioButton optTool0;
		private esriARTool arTool;
			private ESRI.ArcGIS.PublisherControls.AxArcReaderControl axArcReaderControl1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public MapTools()
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
				System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(MapTools));
				this.cmdLoad = new System.Windows.Forms.Button();
				this.txbPath = new System.Windows.Forms.TextBox();
				this._Label3_1 = new System.Windows.Forms.Label();
				this.Label6 = new System.Windows.Forms.Label();
				this.Label4 = new System.Windows.Forms.Label();
				this._Label3_0 = new System.Windows.Forms.Label();
				this.Label2 = new System.Windows.Forms.Label();
				this.cmdRedo = new System.Windows.Forms.Button();
				this.cmdUndo = new System.Windows.Forms.Button();
				this.cmdFullExtent = new System.Windows.Forms.Button();
				this.optTool2 = new System.Windows.Forms.RadioButton();
				this.optTool1 = new System.Windows.Forms.RadioButton();
				this.optTool0 = new System.Windows.Forms.RadioButton();
				this.axArcReaderControl1 = new ESRI.ArcGIS.PublisherControls.AxArcReaderControl();
				((System.ComponentModel.ISupportInitialize)(this.axArcReaderControl1)).BeginInit();
				this.SuspendLayout();
				// 
				// cmdLoad
				// 
				this.cmdLoad.BackColor = System.Drawing.SystemColors.Control;
				this.cmdLoad.Cursor = System.Windows.Forms.Cursors.Default;
				this.cmdLoad.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
				this.cmdLoad.ForeColor = System.Drawing.SystemColors.ControlText;
				this.cmdLoad.Location = new System.Drawing.Point(392, 8);
				this.cmdLoad.Name = "cmdLoad";
				this.cmdLoad.RightToLeft = System.Windows.Forms.RightToLeft.No;
				this.cmdLoad.Size = new System.Drawing.Size(81, 25);
				this.cmdLoad.TabIndex = 4;
				this.cmdLoad.Text = "Load PMF";
				this.cmdLoad.Click += new System.EventHandler(this.cmdLoad_Click);
				// 
				// txbPath
				// 
				this.txbPath.AcceptsReturn = true;
				this.txbPath.AutoSize = false;
				this.txbPath.BackColor = System.Drawing.SystemColors.Window;
				this.txbPath.Cursor = System.Windows.Forms.Cursors.IBeam;
				this.txbPath.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
				this.txbPath.ForeColor = System.Drawing.SystemColors.WindowText;
				this.txbPath.Location = new System.Drawing.Point(8, 8);
				this.txbPath.MaxLength = 0;
				this.txbPath.Name = "txbPath";
				this.txbPath.RightToLeft = System.Windows.Forms.RightToLeft.No;
				this.txbPath.Size = new System.Drawing.Size(377, 25);
				this.txbPath.TabIndex = 3;
				this.txbPath.Text = "";
				// 
				// _Label3_1
				// 
				this._Label3_1.BackColor = System.Drawing.SystemColors.Control;
				this._Label3_1.Cursor = System.Windows.Forms.Cursors.Default;
				this._Label3_1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
				this._Label3_1.ForeColor = System.Drawing.SystemColors.Highlight;
				this._Label3_1.Location = new System.Drawing.Point(488, 120);
				this._Label3_1.Name = "_Label3_1";
				this._Label3_1.RightToLeft = System.Windows.Forms.RightToLeft.No;
				this._Label3_1.Size = new System.Drawing.Size(201, 33);
				this._Label3_1.TabIndex = 17;
				this._Label3_1.Text = "4) Use the \'Layout\' button to display the layout view. ";
				// 
				// Label6
				// 
				this.Label6.BackColor = System.Drawing.SystemColors.Control;
				this.Label6.Cursor = System.Windows.Forms.Cursors.Default;
				this.Label6.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
				this.Label6.ForeColor = System.Drawing.SystemColors.Highlight;
				this.Label6.Location = new System.Drawing.Point(488, 160);
				this.Label6.Name = "Label6";
				this.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No;
				this.Label6.Size = new System.Drawing.Size(201, 56);
				this.Label6.TabIndex = 16;
				this.Label6.Text = "5) Use the \'tools below to navigate the data within any of the data frames on the" +
						" page layout.";
				// 
				// Label4
				// 
				this.Label4.BackColor = System.Drawing.SystemColors.Control;
				this.Label4.Cursor = System.Windows.Forms.Cursors.Default;
				this.Label4.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
				this.Label4.ForeColor = System.Drawing.SystemColors.Highlight;
				this.Label4.Location = new System.Drawing.Point(488, 80);
				this.Label4.Name = "Label4";
				this.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No;
				this.Label4.Size = new System.Drawing.Size(201, 33);
				this.Label4.TabIndex = 15;
				this.Label4.Text = "3) Use the tools below to navigate the focus map.";
				// 
				// _Label3_0
				// 
				this._Label3_0.BackColor = System.Drawing.SystemColors.Control;
				this._Label3_0.Cursor = System.Windows.Forms.Cursors.Default;
				this._Label3_0.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
				this._Label3_0.ForeColor = System.Drawing.SystemColors.Highlight;
				this._Label3_0.Location = new System.Drawing.Point(488, 40);
				this._Label3_0.Name = "_Label3_0";
				this._Label3_0.RightToLeft = System.Windows.Forms.RightToLeft.No;
				this._Label3_0.Size = new System.Drawing.Size(201, 33);
				this._Label3_0.TabIndex = 14;
				this._Label3_0.Text = "2) Use the \'Map\' button to display the data view. ";
				// 
				// Label2
				// 
				this.Label2.BackColor = System.Drawing.SystemColors.Control;
				this.Label2.Cursor = System.Windows.Forms.Cursors.Default;
				this.Label2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
				this.Label2.ForeColor = System.Drawing.SystemColors.Highlight;
				this.Label2.Location = new System.Drawing.Point(488, 8);
				this.Label2.Name = "Label2";
				this.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No;
				this.Label2.Size = new System.Drawing.Size(201, 33);
				this.Label2.TabIndex = 13;
				this.Label2.Text = "1) Enter a valid file path and load the PMF.";
				// 
				// cmdRedo
				// 
				this.cmdRedo.BackColor = System.Drawing.SystemColors.Control;
				this.cmdRedo.Cursor = System.Windows.Forms.Cursors.Default;
				this.cmdRedo.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
				this.cmdRedo.ForeColor = System.Drawing.SystemColors.ControlText;
				this.cmdRedo.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
				this.cmdRedo.Location = new System.Drawing.Point(632, 288);
				this.cmdRedo.Name = "cmdRedo";
				this.cmdRedo.RightToLeft = System.Windows.Forms.RightToLeft.No;
				this.cmdRedo.Size = new System.Drawing.Size(64, 49);
				this.cmdRedo.TabIndex = 23;
				this.cmdRedo.Text = "Redo";
				this.cmdRedo.TextAlign = System.Drawing.ContentAlignment.TopCenter;
				this.cmdRedo.Click += new System.EventHandler(this.cmdRedo_Click);
				// 
				// cmdUndo
				// 
				this.cmdUndo.BackColor = System.Drawing.SystemColors.Control;
				this.cmdUndo.Cursor = System.Windows.Forms.Cursors.Default;
				this.cmdUndo.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
				this.cmdUndo.ForeColor = System.Drawing.SystemColors.ControlText;
				this.cmdUndo.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
				this.cmdUndo.Location = new System.Drawing.Point(560, 288);
				this.cmdUndo.Name = "cmdUndo";
				this.cmdUndo.RightToLeft = System.Windows.Forms.RightToLeft.No;
				this.cmdUndo.Size = new System.Drawing.Size(64, 49);
				this.cmdUndo.TabIndex = 22;
				this.cmdUndo.Text = "Undo";
				this.cmdUndo.TextAlign = System.Drawing.ContentAlignment.TopCenter;
				this.cmdUndo.Click += new System.EventHandler(this.cmdUndo_Click);
				// 
				// cmdFullExtent
				// 
				this.cmdFullExtent.BackColor = System.Drawing.SystemColors.Control;
				this.cmdFullExtent.Cursor = System.Windows.Forms.Cursors.Default;
				this.cmdFullExtent.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
				this.cmdFullExtent.ForeColor = System.Drawing.SystemColors.ControlText;
				this.cmdFullExtent.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
				this.cmdFullExtent.Location = new System.Drawing.Point(488, 288);
				this.cmdFullExtent.Name = "cmdFullExtent";
				this.cmdFullExtent.RightToLeft = System.Windows.Forms.RightToLeft.No;
				this.cmdFullExtent.Size = new System.Drawing.Size(64, 49);
				this.cmdFullExtent.TabIndex = 21;
				this.cmdFullExtent.Text = "FullExtent";
				this.cmdFullExtent.TextAlign = System.Drawing.ContentAlignment.TopCenter;
				this.cmdFullExtent.Click += new System.EventHandler(this.cmdFullExtent_Click);
				// 
				// optTool2
				// 
				this.optTool2.Appearance = System.Windows.Forms.Appearance.Button;
				this.optTool2.BackColor = System.Drawing.SystemColors.Control;
				this.optTool2.Cursor = System.Windows.Forms.Cursors.Default;
				this.optTool2.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
				this.optTool2.ForeColor = System.Drawing.SystemColors.ControlText;
				this.optTool2.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
				this.optTool2.Location = new System.Drawing.Point(632, 232);
				this.optTool2.Name = "optTool2";
				this.optTool2.RightToLeft = System.Windows.Forms.RightToLeft.No;
				this.optTool2.Size = new System.Drawing.Size(64, 49);
				this.optTool2.TabIndex = 18;
				this.optTool2.TabStop = true;
				this.optTool2.Text = "Pan";
				this.optTool2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
				this.optTool2.Click += new System.EventHandler(this.MixedControls_Click);
				// 
				// optTool1
				// 
				this.optTool1.Appearance = System.Windows.Forms.Appearance.Button;
				this.optTool1.BackColor = System.Drawing.SystemColors.Control;
				this.optTool1.Cursor = System.Windows.Forms.Cursors.Default;
				this.optTool1.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
				this.optTool1.ForeColor = System.Drawing.SystemColors.ControlText;
				this.optTool1.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
				this.optTool1.Location = new System.Drawing.Point(560, 232);
				this.optTool1.Name = "optTool1";
				this.optTool1.RightToLeft = System.Windows.Forms.RightToLeft.No;
				this.optTool1.Size = new System.Drawing.Size(64, 49);
				this.optTool1.TabIndex = 20;
				this.optTool1.TabStop = true;
				this.optTool1.Text = "ZoomOut";
				this.optTool1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
				this.optTool1.Click += new System.EventHandler(this.MixedControls_Click);
				// 
				// optTool0
				// 
				this.optTool0.Appearance = System.Windows.Forms.Appearance.Button;
				this.optTool0.BackColor = System.Drawing.SystemColors.Control;
				this.optTool0.Cursor = System.Windows.Forms.Cursors.Default;
				this.optTool0.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
				this.optTool0.ForeColor = System.Drawing.SystemColors.ControlText;
				this.optTool0.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
				this.optTool0.Location = new System.Drawing.Point(488, 232);
				this.optTool0.Name = "optTool0";
				this.optTool0.RightToLeft = System.Windows.Forms.RightToLeft.No;
				this.optTool0.Size = new System.Drawing.Size(64, 49);
				this.optTool0.TabIndex = 19;
				this.optTool0.TabStop = true;
				this.optTool0.Text = "ZoomIn";
				this.optTool0.TextAlign = System.Drawing.ContentAlignment.TopCenter;
				this.optTool0.Click += new System.EventHandler(this.MixedControls_Click);
				// 
				// axArcReaderControl1
				// 
				this.axArcReaderControl1.Location = new System.Drawing.Point(8, 40);
				this.axArcReaderControl1.Name = "axArcReaderControl1";
				this.axArcReaderControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axArcReaderControl1.OcxState")));
				this.axArcReaderControl1.Size = new System.Drawing.Size(464, 432);
				this.axArcReaderControl1.TabIndex = 24;
				this.axArcReaderControl1.OnAfterScreenDraw += new System.EventHandler(this.axArcReaderControl1_OnAfterScreenDraw);
				this.axArcReaderControl1.OnCurrentViewChanged += new ESRI.ArcGIS.PublisherControls.IARControlEvents_Ax_OnCurrentViewChangedEventHandler(this.axArcReaderControl1_OnCurrentViewChanged);
				// 
				// Form1
				// 
				this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
				this.ClientSize = new System.Drawing.Size(704, 478);
				this.Controls.Add(this.axArcReaderControl1);
				this.Controls.Add(this.cmdRedo);
				this.Controls.Add(this.cmdUndo);
				this.Controls.Add(this.cmdFullExtent);
				this.Controls.Add(this.optTool2);
				this.Controls.Add(this.optTool1);
				this.Controls.Add(this.optTool0);
				this.Controls.Add(this._Label3_1);
				this.Controls.Add(this.Label6);
				this.Controls.Add(this.Label4);
				this.Controls.Add(this._Label3_0);
				this.Controls.Add(this.Label2);
				this.Controls.Add(this.cmdLoad);
				this.Controls.Add(this.txbPath);
				this.Name = "Form1";
				this.Text = "Form1";
				this.Closing += new System.ComponentModel.CancelEventHandler(this.Form1_Closing);
				this.Load += new System.EventHandler(this.Form1_Load);
				((System.ComponentModel.ISupportInitialize)(this.axArcReaderControl1)).EndInit();
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

            Application.Run(new MapTools());
		}

		private void Form1_Load(object sender, System.EventArgs e)
		{
			//Load option/command button images
			System.Drawing.Bitmap bitmap1 = new System.Drawing.Bitmap (GetType().Assembly.GetManifestResourceStream(GetType(), "ZoomIn.bmp"));
			bitmap1.MakeTransparent(System.Drawing.Color.Teal);
			optTool0.Image = bitmap1;
			System.Drawing.Bitmap bitmap2 = new System.Drawing.Bitmap (GetType().Assembly.GetManifestResourceStream(GetType(), "ZoomOut.bmp"));
			bitmap2.MakeTransparent(System.Drawing.Color.Teal);
			optTool1.Image = bitmap2;
			System.Drawing.Bitmap bitmap3 = new System.Drawing.Bitmap (GetType().Assembly.GetManifestResourceStream(GetType(), "Pan.bmp"));
			bitmap3.MakeTransparent(System.Drawing.Color.Teal);
			optTool2.Image = bitmap3;		
			System.Drawing.Bitmap bitmap4 = new System.Drawing.Bitmap (GetType().Assembly.GetManifestResourceStream(GetType(), "FullExtent.bmp"));
			bitmap4.MakeTransparent(System.Drawing.Color.Teal);
			cmdFullExtent.Image = bitmap4;
			System.Drawing.Bitmap bitmap5 = new System.Drawing.Bitmap (GetType().Assembly.GetManifestResourceStream(GetType(), "UnDoDraw.bmp"));
			bitmap5.MakeTransparent(System.Drawing.Color.Teal);
			cmdUndo.Image = bitmap5;
			System.Drawing.Bitmap bitmap6 = new System.Drawing.Bitmap (GetType().Assembly.GetManifestResourceStream(GetType(), "ReDoDraw.bmp"));
			bitmap6.MakeTransparent(System.Drawing.Color.Teal);
			cmdRedo.Image = bitmap6;

			//Disable controls
			optTool0.Enabled = false;
			optTool1.Enabled = false;
			optTool2.Enabled = false;
			cmdUndo.Enabled = false;
			cmdRedo.Enabled = false;
			cmdFullExtent.Enabled = false;
		}

		private void cmdFullExtent_Click(object sender, System.EventArgs e)
		{
			//Set extent to full data extent
			double xMax = 0;
			double xMin = 0;
			double yMin = 0;
			double yMax = 0;
			axArcReaderControl1.ARPageLayout.FocusARMap.GetFullExtent(ref xMin, ref yMin, ref xMax, ref yMax);
			axArcReaderControl1.ARPageLayout.FocusARMap.SetExtent(xMin, yMin, xMax, yMax);
			//Refresh the display
			axArcReaderControl1.ARPageLayout.FocusARMap.Refresh(true);
		}

		private void cmdLoad_Click(object sender, System.EventArgs e)
		{
			//Load the specified pmf
			if (txbPath.Text == "")
			{
				return;
			}   
			if (axArcReaderControl1.CheckDocument(txbPath.Text) == true) 
			{
				axArcReaderControl1.LoadDocument(txbPath.Text,"");
			}
			else
			{
				MessageBox.Show("This document cannot be loaded!");
				return;
			}

			optTool0.Checked = true;
		}

		private void cmdRedo_Click(object sender, System.EventArgs e)
		{
			//Redo extent
			axArcReaderControl1.ARPageLayout.FocusARMap.RedoExtent();
		}

		private void cmdUndo_Click(object sender, System.EventArgs e)
		{
			//Undo extent
			axArcReaderControl1.ARPageLayout.FocusARMap.UndoExtent();
		}

		private void axArcReaderControl1_OnAfterScreenDraw(object sender, System.EventArgs e)
		{
			//Determine whether can undo/redo extent and enable/disbale control
			cmdUndo.Enabled = axArcReaderControl1.ARPageLayout.FocusARMap.CanUndoExtent;
			cmdRedo.Enabled = axArcReaderControl1.ARPageLayout.FocusARMap.CanRedoExtent; 
		}

		private void axArcReaderControl1_OnCurrentViewChanged(object sender, ESRI.ArcGIS.PublisherControls.IARControlEvents_OnCurrentViewChangedEvent e)
		{

			bool bEnabled = false;
			//Determine view type
			if (axArcReaderControl1.CurrentViewType != esriARViewType.esriARViewTypeNone)
			{
				bEnabled = true;
				//Update the current tool if necessary
				if (axArcReaderControl1.CurrentARTool != arTool)
				{
					axArcReaderControl1.CurrentARTool = arTool;
				}
			}

			//Enable/ disable controls
			optTool0.Enabled = bEnabled;
			optTool1.Enabled = bEnabled;
			optTool2.Enabled = bEnabled;
			cmdFullExtent.Enabled = bEnabled;
		}

		private void MixedControls_Click(object sender, System.EventArgs e)
		{
			RadioButton b = (RadioButton) sender;
			//Set current tool
			switch (b.Name)
			{
				case "optTool0":
					axArcReaderControl1.CurrentARTool = esriARTool.esriARToolMapZoomIn;
					break;
				case "optTool1":
					axArcReaderControl1.CurrentARTool = esriARTool.esriARToolMapZoomOut;
					break;
				case "optTool2":
					axArcReaderControl1.CurrentARTool = esriARTool.esriARToolMapPan;
					break;
			}

			//Remember the current tool
			arTool = axArcReaderControl1.CurrentARTool;
		}

		private void Form1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			//Release COM objects
			ESRI.ArcGIS.ADF.COMSupport.AOUninitialize.Shutdown();
		}
	}
}
