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
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS;


namespace Overview
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Overview : System.Windows.Forms.Form
	{
		public System.Windows.Forms.TextBox txbMxPath;
		public System.Windows.Forms.Button cmdLoadMxFile;
		public System.Windows.Forms.Button cmdZoomPage;
		public System.Windows.Forms.Label Label2;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private ESRI.ArcGIS.Controls.AxPageLayoutControl axPageLayoutControl1;
		private ESRI.ArcGIS.Controls.AxPageLayoutControl axPageLayoutControl2;
		private ESRI.ArcGIS.Controls.AxLicenseControl axLicenseControl1;
        private IPageLayoutControl m_PageLayoutControl;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Overview()
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Overview));
            this.txbMxPath = new System.Windows.Forms.TextBox();
            this.cmdLoadMxFile = new System.Windows.Forms.Button();
            this.cmdZoomPage = new System.Windows.Forms.Button();
            this.Label2 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.axPageLayoutControl1 = new ESRI.ArcGIS.Controls.AxPageLayoutControl();
            this.axPageLayoutControl2 = new ESRI.ArcGIS.Controls.AxPageLayoutControl();
            this.axLicenseControl1 = new ESRI.ArcGIS.Controls.AxLicenseControl();
            ((System.ComponentModel.ISupportInitialize)(this.axPageLayoutControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axPageLayoutControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // txbMxPath
            // 
            this.txbMxPath.AcceptsReturn = true;
            this.txbMxPath.BackColor = System.Drawing.SystemColors.Window;
            this.txbMxPath.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txbMxPath.Enabled = false;
            this.txbMxPath.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txbMxPath.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txbMxPath.Location = new System.Drawing.Point(8, 8);
            this.txbMxPath.MaxLength = 0;
            this.txbMxPath.Name = "txbMxPath";
            this.txbMxPath.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txbMxPath.Size = new System.Drawing.Size(249, 19);
            this.txbMxPath.TabIndex = 6;
            // 
            // cmdLoadMxFile
            // 
            this.cmdLoadMxFile.BackColor = System.Drawing.SystemColors.Control;
            this.cmdLoadMxFile.Cursor = System.Windows.Forms.Cursors.Default;
            this.cmdLoadMxFile.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdLoadMxFile.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdLoadMxFile.Location = new System.Drawing.Point(264, 8);
            this.cmdLoadMxFile.Name = "cmdLoadMxFile";
            this.cmdLoadMxFile.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmdLoadMxFile.Size = new System.Drawing.Size(113, 25);
            this.cmdLoadMxFile.TabIndex = 5;
            this.cmdLoadMxFile.Text = "Load Mx File";
            this.cmdLoadMxFile.UseVisualStyleBackColor = false;
            this.cmdLoadMxFile.Click += new System.EventHandler(this.cmdLoadMxFile_Click);
            // 
            // cmdZoomPage
            // 
            this.cmdZoomPage.BackColor = System.Drawing.SystemColors.Control;
            this.cmdZoomPage.Cursor = System.Windows.Forms.Cursors.Default;
            this.cmdZoomPage.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdZoomPage.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdZoomPage.Location = new System.Drawing.Point(264, 320);
            this.cmdZoomPage.Name = "cmdZoomPage";
            this.cmdZoomPage.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmdZoomPage.Size = new System.Drawing.Size(113, 25);
            this.cmdZoomPage.TabIndex = 9;
            this.cmdZoomPage.Text = "Zoom To Page";
            this.cmdZoomPage.UseVisualStyleBackColor = false;
            this.cmdZoomPage.Click += new System.EventHandler(this.cmdZoomPage_Click);
            // 
            // Label2
            // 
            this.Label2.BackColor = System.Drawing.SystemColors.Control;
            this.Label2.Cursor = System.Windows.Forms.Cursors.Default;
            this.Label2.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Label2.Location = new System.Drawing.Point(264, 256);
            this.Label2.Name = "Label2";
            this.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Label2.Size = new System.Drawing.Size(121, 57);
            this.Label2.TabIndex = 10;
            this.Label2.Text = "Use the left mouse button to drag a rectangle and  the right mouse button to pan." +
                "";
            // 
            // axPageLayoutControl1
            // 
            this.axPageLayoutControl1.Location = new System.Drawing.Point(8, 32);
            this.axPageLayoutControl1.Name = "axPageLayoutControl1";
            this.axPageLayoutControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axPageLayoutControl1.OcxState")));
            this.axPageLayoutControl1.Size = new System.Drawing.Size(248, 320);
            this.axPageLayoutControl1.TabIndex = 11;
            this.axPageLayoutControl1.OnMouseDown += new ESRI.ArcGIS.Controls.IPageLayoutControlEvents_Ax_OnMouseDownEventHandler(this.axPageLayoutControl1_OnMouseDown);
            this.axPageLayoutControl1.OnPageLayoutReplaced += new ESRI.ArcGIS.Controls.IPageLayoutControlEvents_Ax_OnPageLayoutReplacedEventHandler(this.axPageLayoutControl1_OnPageLayoutReplaced);
            this.axPageLayoutControl1.OnExtentUpdated += new ESRI.ArcGIS.Controls.IPageLayoutControlEvents_Ax_OnExtentUpdatedEventHandler(this.axPageLayoutControl1_OnExtentUpdated);
            // 
            // axPageLayoutControl2
            // 
            this.axPageLayoutControl2.Location = new System.Drawing.Point(264, 40);
            this.axPageLayoutControl2.Name = "axPageLayoutControl2";
            this.axPageLayoutControl2.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axPageLayoutControl2.OcxState")));
            this.axPageLayoutControl2.Size = new System.Drawing.Size(112, 176);
            this.axPageLayoutControl2.TabIndex = 12;
            // 
            // axLicenseControl1
            // 
            this.axLicenseControl1.Enabled = true;
            this.axLicenseControl1.Location = new System.Drawing.Point(24, 48);
            this.axLicenseControl1.Name = "axLicenseControl1";
            this.axLicenseControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axLicenseControl1.OcxState")));
            this.axLicenseControl1.Size = new System.Drawing.Size(32, 32);
            this.axLicenseControl1.TabIndex = 13;
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(384, 358);
            this.Controls.Add(this.axLicenseControl1);
            this.Controls.Add(this.axPageLayoutControl2);
            this.Controls.Add(this.axPageLayoutControl1);
            this.Controls.Add(this.cmdZoomPage);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.txbMxPath);
            this.Controls.Add(this.cmdLoadMxFile);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.axPageLayoutControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axPageLayoutControl2)).EndInit();
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
            Application.Run(new Overview());
		}

		private void Form1_Load(object sender, System.EventArgs e)
		{
			m_PageLayoutControl = (IPageLayoutControl) axPageLayoutControl2.Object;

			//Set PageLayoutControl properties
			axPageLayoutControl1.Enabled = true;
			m_PageLayoutControl.Enabled = false;
			axPageLayoutControl1.Appearance = esriControlsAppearance.esri3D;
			m_PageLayoutControl.Appearance = esriControlsAppearance.esriFlat;
			axPageLayoutControl1.BorderStyle = esriControlsBorderStyle.esriBorder;
			m_PageLayoutControl.BorderStyle = esriControlsBorderStyle.esriNoBorder;
		}

		private void cmdLoadMxFile_Click(object sender, System.EventArgs e)
		{
			//Open a file dialog for selecting map documents
			openFileDialog1.Title = "Browse Map Document";
			openFileDialog1.Filter = "Map Documents (*.mxd)|*.mxd";
			openFileDialog1.ShowDialog();

			//Exit if no map document is selected
			string sFilePath = openFileDialog1.FileName;
			if (sFilePath == "") return;

			//Validate and load the Mx document
			if (axPageLayoutControl1.CheckMxFile(sFilePath))
			{
				txbMxPath.Text = sFilePath;
				axPageLayoutControl1.MousePointer = esriControlsMousePointer.esriPointerHourglass;
				axPageLayoutControl1.LoadMxFile(sFilePath,Type.Missing);
				axPageLayoutControl1.MousePointer = esriControlsMousePointer.esriPointerDefault;
			}
			else
			{
				MessageBox.Show(sFilePath + " is not a valid ArcMap document");
			}
		}

		private void cmdZoomPage_Click(object sender, System.EventArgs e)
		{
			//Zoom to the whole page
			axPageLayoutControl1.ZoomToWholePage();

			//Get the IElement interface by finding an element by its name
			IElement element = m_PageLayoutControl.FindElementByName("ZoomExtent", 1);
			if (element != null)
			{
				//Delete the element
				m_PageLayoutControl.GraphicsContainer.DeleteElement(element);
				//Refresh the graphics
				m_PageLayoutControl.Refresh(esriViewDrawPhase.esriViewGraphics,Type.Missing, Type.Missing);
			}
		}

		private void axPageLayoutControl1_OnMouseDown(object sender, ESRI.ArcGIS.Controls.IPageLayoutControlEvents_OnMouseDownEvent e)
		{
			//Zoom in
			if (e.button == 1)
			{
				axPageLayoutControl1.Extent = axPageLayoutControl1.TrackRectangle();
			}
				//Pan
			else if (e.button == 2)
			{
				axPageLayoutControl1.Pan();
			}
		}

		private void axPageLayoutControl1_OnPageLayoutReplaced(object sender, ESRI.ArcGIS.Controls.IPageLayoutControlEvents_OnPageLayoutReplacedEvent e)
		{
			//Get the file path
			string sFilePath = txbMxPath.Text;
			//Validate and load the Mx document
			if (m_PageLayoutControl.CheckMxFile(sFilePath))
			{
				m_PageLayoutControl.LoadMxFile(sFilePath,Type.Missing);
			}
		}

        private void axPageLayoutControl1_OnExtentUpdated(object sender, IPageLayoutControlEvents_OnExtentUpdatedEvent e)
        {
            //QI for IEnvelope
            IEnvelope envelope = (IEnvelope)e.newEnvelope;

            //Get the IElement interface by finding an element by its name 
            IElement element = m_PageLayoutControl.FindElementByName("ZoomExtent", 1);
            if (element != null)
            {
                //Delete the graphic
                m_PageLayoutControl.GraphicsContainer.DeleteElement(element);
            }
            element = new RectangleElementClass();

            //Get the IRGBColor interface
            IRgbColor color = new RgbColorClass();
            //Set the color properties
            color.RGB = 255;
            color.Transparency = 255;

            //Get the ILine symbol interface
            ILineSymbol outline = new SimpleLineSymbolClass();
            //Set the line symbol properties
            outline.Width = 10;
            outline.Color = color;

            //Set the color properties
            color = new RgbColorClass();
            color.RGB = 255;
            color.Transparency = 0;

            //Get the IFillSymbol properties
            IFillSymbol fillSymbol = new SimpleFillSymbolClass();
            //Set the fill symbol properties
            fillSymbol.Color = color;
            fillSymbol.Outline = outline;

            //QI for IFillShapeElement interface through the IElement interface
            IFillShapeElement fillShapeElement = (IFillShapeElement)element;
            //Set the symbol property
            fillShapeElement.Symbol = fillSymbol;

            //Add the element
            m_PageLayoutControl.AddElement(element, e.newEnvelope, Type.Missing, "ZoomExtent", -1);
            //Refresh the graphics
            m_PageLayoutControl.Refresh(esriViewDrawPhase.esriViewGraphics, Type.Missing, Type.Missing);
        }

	}
}
