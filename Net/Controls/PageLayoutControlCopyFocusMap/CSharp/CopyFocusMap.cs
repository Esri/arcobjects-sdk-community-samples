using System;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS;


namespace CopyFocusMap
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		public System.Windows.Forms.ComboBox cboMaps;
		public System.Windows.Forms.TextBox txbPath;
		public System.Windows.Forms.Button cmdLoad;
		public System.Windows.Forms.Label Label4;
		public System.Windows.Forms.Button cmdZoomPage;
		public System.Windows.Forms.Button cmdFullExtent;
		public System.Windows.Forms.Label Label2;
		public System.Windows.Forms.Label Label1;
		private bool m_bUpdateFocusMap;
		private bool m_bReplacedPageLayout;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private ESRI.ArcGIS.Controls.AxPageLayoutControl axPageLayoutControl1;
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Form1));
			this.cboMaps = new System.Windows.Forms.ComboBox();
			this.txbPath = new System.Windows.Forms.TextBox();
			this.cmdLoad = new System.Windows.Forms.Button();
			this.Label4 = new System.Windows.Forms.Label();
			this.cmdZoomPage = new System.Windows.Forms.Button();
			this.cmdFullExtent = new System.Windows.Forms.Button();
			this.Label2 = new System.Windows.Forms.Label();
			this.Label1 = new System.Windows.Forms.Label();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.axPageLayoutControl1 = new ESRI.ArcGIS.Controls.AxPageLayoutControl();
			this.axMapControl1 = new ESRI.ArcGIS.Controls.AxMapControl();
			this.axLicenseControl1 = new ESRI.ArcGIS.Controls.AxLicenseControl();
			((System.ComponentModel.ISupportInitialize)(this.axPageLayoutControl1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.axMapControl1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).BeginInit();
			this.SuspendLayout();
			// 
			// cboMaps
			// 
			this.cboMaps.BackColor = System.Drawing.SystemColors.Window;
			this.cboMaps.Cursor = System.Windows.Forms.Cursors.Default;
			this.cboMaps.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.cboMaps.ForeColor = System.Drawing.SystemColors.WindowText;
			this.cboMaps.Location = new System.Drawing.Point(72, 48);
			this.cboMaps.Name = "cboMaps";
			this.cboMaps.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cboMaps.Size = new System.Drawing.Size(161, 22);
			this.cboMaps.TabIndex = 11;
			this.cboMaps.SelectedIndexChanged += new System.EventHandler(this.cboMaps_SelectedIndexChanged);
			// 
			// txbPath
			// 
			this.txbPath.AcceptsReturn = true;
			this.txbPath.AutoSize = false;
			this.txbPath.BackColor = System.Drawing.SystemColors.Window;
			this.txbPath.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.txbPath.Enabled = false;
			this.txbPath.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txbPath.ForeColor = System.Drawing.SystemColors.WindowText;
			this.txbPath.Location = new System.Drawing.Point(136, 8);
			this.txbPath.MaxLength = 0;
			this.txbPath.Name = "txbPath";
			this.txbPath.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.txbPath.Size = new System.Drawing.Size(369, 19);
			this.txbPath.TabIndex = 10;
			this.txbPath.Text = "";
			// 
			// cmdLoad
			// 
			this.cmdLoad.BackColor = System.Drawing.SystemColors.Control;
			this.cmdLoad.Cursor = System.Windows.Forms.Cursors.Default;
			this.cmdLoad.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.cmdLoad.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cmdLoad.Location = new System.Drawing.Point(8, 8);
			this.cmdLoad.Name = "cmdLoad";
			this.cmdLoad.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cmdLoad.Size = new System.Drawing.Size(121, 25);
			this.cmdLoad.TabIndex = 9;
			this.cmdLoad.Text = "Load Map Document";
			this.cmdLoad.Click += new System.EventHandler(this.cmdLoad_Click);
			// 
			// Label4
			// 
			this.Label4.BackColor = System.Drawing.SystemColors.Control;
			this.Label4.Cursor = System.Windows.Forms.Cursors.Default;
			this.Label4.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Label4.ForeColor = System.Drawing.SystemColors.ControlText;
			this.Label4.Location = new System.Drawing.Point(8, 48);
			this.Label4.Name = "Label4";
			this.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Label4.Size = new System.Drawing.Size(65, 17);
			this.Label4.TabIndex = 12;
			this.Label4.Text = "Focus Map:";
			// 
			// cmdZoomPage
			// 
			this.cmdZoomPage.BackColor = System.Drawing.SystemColors.Control;
			this.cmdZoomPage.Cursor = System.Windows.Forms.Cursors.Default;
			this.cmdZoomPage.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.cmdZoomPage.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cmdZoomPage.Location = new System.Drawing.Point(12, 352);
			this.cmdZoomPage.Name = "cmdZoomPage";
			this.cmdZoomPage.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cmdZoomPage.Size = new System.Drawing.Size(97, 25);
			this.cmdZoomPage.TabIndex = 16;
			this.cmdZoomPage.Text = "Zoom to Page";
			this.cmdZoomPage.Click += new System.EventHandler(this.cmdZoomPage_Click);
			// 
			// cmdFullExtent
			// 
			this.cmdFullExtent.BackColor = System.Drawing.SystemColors.Control;
			this.cmdFullExtent.Cursor = System.Windows.Forms.Cursors.Default;
			this.cmdFullExtent.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.cmdFullExtent.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cmdFullExtent.Location = new System.Drawing.Point(356, 352);
			this.cmdFullExtent.Name = "cmdFullExtent";
			this.cmdFullExtent.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cmdFullExtent.Size = new System.Drawing.Size(153, 25);
			this.cmdFullExtent.TabIndex = 13;
			this.cmdFullExtent.Text = "Zoom to Full Data Extent";
			this.cmdFullExtent.Click += new System.EventHandler(this.cmdFullExtent_Click);
			// 
			// Label2
			// 
			this.Label2.BackColor = System.Drawing.SystemColors.Control;
			this.Label2.Cursor = System.Windows.Forms.Cursors.Default;
			this.Label2.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Label2.ForeColor = System.Drawing.SystemColors.ControlText;
			this.Label2.Location = new System.Drawing.Point(124, 352);
			this.Label2.Name = "Label2";
			this.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Label2.Size = new System.Drawing.Size(257, 17);
			this.Label2.TabIndex = 15;
			this.Label2.Text = "Right mouse button to pan page or data.";
			// 
			// Label1
			// 
			this.Label1.BackColor = System.Drawing.SystemColors.Control;
			this.Label1.Cursor = System.Windows.Forms.Cursors.Default;
			this.Label1.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Label1.ForeColor = System.Drawing.SystemColors.ControlText;
			this.Label1.Location = new System.Drawing.Point(124, 368);
			this.Label1.Name = "Label1";
			this.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Label1.Size = new System.Drawing.Size(249, 17);
			this.Label1.TabIndex = 14;
			this.Label1.Text = "Left mouse button to zoom in on page or data.";
			// 
			// axPageLayoutControl1
			// 
			this.axPageLayoutControl1.Location = new System.Drawing.Point(8, 72);
			this.axPageLayoutControl1.Name = "axPageLayoutControl1";
			this.axPageLayoutControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axPageLayoutControl1.OcxState")));
			this.axPageLayoutControl1.Size = new System.Drawing.Size(224, 272);
			this.axPageLayoutControl1.TabIndex = 17;
			this.axPageLayoutControl1.OnMouseDown += new ESRI.ArcGIS.Controls.IPageLayoutControlEvents_Ax_OnMouseDownEventHandler(this.axPageLayoutControl1_OnMouseDown);
			this.axPageLayoutControl1.OnPageLayoutReplaced += new ESRI.ArcGIS.Controls.IPageLayoutControlEvents_Ax_OnPageLayoutReplacedEventHandler(this.axPageLayoutControl1_OnPageLayoutReplaced);
			this.axPageLayoutControl1.OnAfterScreenDraw += new ESRI.ArcGIS.Controls.IPageLayoutControlEvents_Ax_OnAfterScreenDrawEventHandler(this.axPageLayoutControl1_OnAfterScreenDraw);
			this.axPageLayoutControl1.OnFocusMapChanged += new System.EventHandler(this.axPageLayoutControl1_OnFocusMapChanged);
			this.axPageLayoutControl1.OnBeforeScreenDraw += new ESRI.ArcGIS.Controls.IPageLayoutControlEvents_Ax_OnBeforeScreenDrawEventHandler(this.axPageLayoutControl1_OnBeforeScreenDraw);
			// 
			// axMapControl1
			// 
			this.axMapControl1.Location = new System.Drawing.Point(240, 48);
			this.axMapControl1.Name = "axMapControl1";
			this.axMapControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axMapControl1.OcxState")));
			this.axMapControl1.Size = new System.Drawing.Size(272, 296);
			this.axMapControl1.TabIndex = 18;
			this.axMapControl1.OnMouseDown += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMouseDownEventHandler(this.axMapControl1_OnMouseDown);
			this.axMapControl1.OnAfterScreenDraw += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnAfterScreenDrawEventHandler(this.axMapControl1_OnAfterScreenDraw);
			this.axMapControl1.OnBeforeScreenDraw += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnBeforeScreenDrawEventHandler(this.axMapControl1_OnBeforeScreenDraw);
			// 
			// axLicenseControl1
			// 
			this.axLicenseControl1.Enabled = true;
			this.axLicenseControl1.Location = new System.Drawing.Point(160, 96);
			this.axLicenseControl1.Name = "axLicenseControl1";
			this.axLicenseControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axLicenseControl1.OcxState")));
			this.axLicenseControl1.Size = new System.Drawing.Size(200, 50);
			this.axLicenseControl1.TabIndex = 19;
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(520, 390);
			this.Controls.Add(this.axLicenseControl1);
			this.Controls.Add(this.axMapControl1);
			this.Controls.Add(this.axPageLayoutControl1);
			this.Controls.Add(this.cmdZoomPage);
			this.Controls.Add(this.cmdFullExtent);
			this.Controls.Add(this.Label2);
			this.Controls.Add(this.Label1);
			this.Controls.Add(this.cboMaps);
			this.Controls.Add(this.txbPath);
			this.Controls.Add(this.cmdLoad);
			this.Controls.Add(this.Label4);
			this.Name = "Form1";
			this.Text = "Form1";
			this.Load += new System.EventHandler(this.Form1_Load);
			((System.ComponentModel.ISupportInitialize)(this.axPageLayoutControl1)).EndInit();
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

			m_bUpdateFocusMap = false;
			m_bReplacedPageLayout = false;
			axMapControl1.ShowScrollbars = false;
		}

		private void cmdFullExtent_Click(object sender, System.EventArgs e)
		{
			m_bUpdateFocusMap = true;

			//Zoom to the full extent of the data in the map
			axMapControl1.Extent = axMapControl1.FullExtent;
		}

		private void cmdZoomPage_Click(object sender, System.EventArgs e)
		{
			//Zoom to the whole page
			axPageLayoutControl1.ZoomToWholePage();
		}

		private void cmdLoad_Click(object sender, System.EventArgs e)
		{
			//Open a file dialog for selecting map documents
			openFileDialog1.Title = "Browse Map Document";
			openFileDialog1.Filter = "Map Documents (*.mxd)|*.mxd";
			openFileDialog1.ShowDialog();

			//Exit if no map document is selected
			string filePath = openFileDialog1.FileName;
			if (filePath == "") return;

			//If valid map document
			if (axPageLayoutControl1.CheckMxFile(filePath))
			{
				//Set mouse pointers
				axPageLayoutControl1.MousePointer = esriControlsMousePointer.esriPointerHourglass;
				axMapControl1.MousePointer = esriControlsMousePointer.esriPointerHourglass;
				//Reset controls
				axMapControl1.ActiveView.Clear();
				axMapControl1.ActiveView.GraphicsContainer.DeleteAllElements();
				axMapControl1.Refresh();
				cboMaps.Items.Clear();
				txbPath.Text = filePath;
				//Load map document
				axPageLayoutControl1.LoadMxFile(filePath,Type.Missing);
				//Set mouse pointers
				axPageLayoutControl1.MousePointer = esriControlsMousePointer.esriPointerDefault;
				axMapControl1.MousePointer = esriControlsMousePointer.esriPointerDefault;
			}
			else
			{
				MessageBox.Show(filePath + " is not a valid ArcMap document");
				return;
			}
		}

		private void ListMaps()
		{
			//Get IGraphicsContainer interface 
			IGraphicsContainer graphicsContainer = axPageLayoutControl1.GraphicsContainer;
			graphicsContainer.Reset();

			//Query Interface for IElement interface 
			IElement element = graphicsContainer.Next();
			
			//Loop through the elements
			int index = 0;
			while (element != null)
			{
				if (element is IMapFrame)
				{
					IMapFrame mapFrame = (IMapFrame) element;
					//Query interface for IElementProperties interface
					IElementProperties elementProperties = (IElementProperties) element;
					//Get the name of the Map in the MapFrame
					string sMapName = mapFrame.Map.Name;

					//Set the name of the MapFrame to the Map's name
					elementProperties.Name = sMapName;
					//Add the map name to the ComboBox
					cboMaps.Items.Insert(index, mapFrame.Map.Name);

					//If the Map is the FocusMap select the MapName in the ComboBox
					if (sMapName == axPageLayoutControl1.ActiveView.FocusMap.Name)
					{
						cboMaps.SelectedIndex = index;
					}
					index = index + 1;
				}
				element = graphicsContainer.Next();
			}
		}

		private void CopyAndOverwriteMap()
		{
			//Get IObjectCopy interface
			IObjectCopy objectCopy = new ObjectCopyClass(); 

			//Get IUnknown interface (map to copy)
			object toCopyMap = axPageLayoutControl1.ActiveView.FocusMap;

            //Each Map contained within the PageLayout encapsulated by the 
            //PageLayoutControl, resides within a separate MapFrame, and therefore 
            //have their IMap::IsFramed property set to True. A Map contained within the 
            //MapControl does not reside within a MapFrame. As such before 
            //overwriting the MapControl's map, the IMap::IsFramed property must be set 
            //to False. Failure to do this will lead to corrupted map documents saved 
            //containing the contents of the MapControl.
            IMap map = toCopyMap as IMap;
            map.IsFramed = false;

			//Get IUnknown interface (copied map)
			object copiedMap = objectCopy.Copy(toCopyMap);

			//Get IUnknown interface (map to overwrite)
			object toOverwriteMap = axMapControl1.Map;

			//Overwrite the MapControl's map
			objectCopy.Overwrite(copiedMap, ref toOverwriteMap);

			SetMapExtent();
		}
	
		private void SetMapExtent()
		{
			//Get IActiveView interface
			IActiveView activeView = (IActiveView) axPageLayoutControl1.ActiveView.FocusMap;
			//Set the control's extent
			axMapControl1.Extent = activeView.Extent;
			//Refresh the display
			axMapControl1.Refresh();
		}

		private void axPageLayoutControl1_OnFocusMapChanged(object sender, System.EventArgs e)
		{
			CopyAndOverwriteMap();
		}

		private void cboMaps_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			IPageLayoutControl pageLayoutControl = (IPageLayoutControl) axPageLayoutControl1.GetOcx();
			//Get IMapFrame interface 
			IMapFrame element = (IMapFrame) pageLayoutControl.FindElementByName(cboMaps.Text, 1);
			
			//Set the FocusMap
			axPageLayoutControl1.ActiveView.FocusMap = element.Map;
		}

		private void axMapControl1_OnBeforeScreenDraw(object sender, ESRI.ArcGIS.Controls.IMapControlEvents2_OnBeforeScreenDrawEvent e)
		{
			//Set mouse pointers
			axPageLayoutControl1.MousePointer = esriControlsMousePointer.esriPointerHourglass;
			axMapControl1.MousePointer = esriControlsMousePointer.esriPointerHourglass;
		}

		private void axMapControl1_OnMouseDown(object sender, ESRI.ArcGIS.Controls.IMapControlEvents2_OnMouseDownEvent e)
		{
			m_bUpdateFocusMap = true;

			if (e.button == 1)
			{
				axMapControl1.Extent = axMapControl1.TrackRectangle();
			}
			else if (e.button == 2)
			{
				axMapControl1.Pan();
		    }
		}

		private void axMapControl1_OnAfterScreenDraw(object sender, ESRI.ArcGIS.Controls.IMapControlEvents2_OnAfterScreenDrawEvent e)
		{
			//Set mouse pointers
			axPageLayoutControl1.MousePointer = esriControlsMousePointer.esriPointerDefault;
			axMapControl1.MousePointer = esriControlsMousePointer.esriPointerDefault;

			if (m_bUpdateFocusMap == false) return;

			//Get IActiveView interface
			IActiveView activeView = (IActiveView) axPageLayoutControl1.ActiveView.FocusMap;

			//Get IDisplayTransformation interface
			IDisplayTransformation displayTransformation = activeView.ScreenDisplay.DisplayTransformation;

			//Set the visible extent of the focus map
			displayTransformation.VisibleBounds = axMapControl1.Extent;
			//Refresh the focus map
			activeView.Refresh();

			m_bUpdateFocusMap = false;
		}

		private void axPageLayoutControl1_OnAfterScreenDraw(object sender, ESRI.ArcGIS.Controls.IPageLayoutControlEvents_OnAfterScreenDrawEvent e)
		{
			//Set mouse pointers
			axPageLayoutControl1.MousePointer = esriControlsMousePointer.esriPointerDefault;
			axMapControl1.MousePointer = esriControlsMousePointer.esriPointerDefault;

			if (m_bReplacedPageLayout == false) return;

			CopyAndOverwriteMap();
			m_bReplacedPageLayout = false;
		}

		private void axPageLayoutControl1_OnMouseDown(object sender, ESRI.ArcGIS.Controls.IPageLayoutControlEvents_OnMouseDownEvent e)
		{
			if (e.button == 1)
			{
				axPageLayoutControl1.Extent = axPageLayoutControl1.TrackRectangle();
			}
			else if (e.button == 2)
			{
				axPageLayoutControl1.Pan();
			}
		}

		private void axPageLayoutControl1_OnPageLayoutReplaced(object sender, ESRI.ArcGIS.Controls.IPageLayoutControlEvents_OnPageLayoutReplacedEvent e)
		{
			m_bReplacedPageLayout = true;
			ListMaps();
		}

		private void axPageLayoutControl1_OnBeforeScreenDraw(object sender, ESRI.ArcGIS.Controls.IPageLayoutControlEvents_OnBeforeScreenDrawEvent e)
		{
			//Set mouse pointers
			axPageLayoutControl1.MousePointer = esriControlsMousePointer.esriPointerHourglass;
			axMapControl1.MousePointer = esriControlsMousePointer.esriPointerHourglass;
		}

	}
}
