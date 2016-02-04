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

using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS;

using ESRI.ArcGIS.ADF.CATIDs;

namespace CommandsEnvironment
{

	public class CommandsEnvironment : System.Windows.Forms.Form
	{
		internal System.Windows.Forms.ComboBox ComboBox1;
		private ESRI.ArcGIS.Controls.AxSymbologyControl axSymbologyControl1;
		private ESRI.ArcGIS.Controls.AxToolbarControl axToolbarControl1;
		private ESRI.ArcGIS.Controls.AxPageLayoutControl axPageLayoutControl1;
		private ESRI.ArcGIS.Controls.AxLicenseControl axLicenseControl1;
		private System.ComponentModel.Container components = null;
		private IGraphicProperties m_graphicProperties;

		public CommandsEnvironment()
		{
			InitializeComponent();

		}

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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(CommandsEnvironment));
			this.ComboBox1 = new System.Windows.Forms.ComboBox();
			this.axSymbologyControl1 = new ESRI.ArcGIS.Controls.AxSymbologyControl();
			this.axToolbarControl1 = new ESRI.ArcGIS.Controls.AxToolbarControl();
			this.axPageLayoutControl1 = new ESRI.ArcGIS.Controls.AxPageLayoutControl();
			this.axLicenseControl1 = new ESRI.ArcGIS.Controls.AxLicenseControl();
			((System.ComponentModel.ISupportInitialize)(this.axSymbologyControl1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.axToolbarControl1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.axPageLayoutControl1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).BeginInit();
			this.SuspendLayout();
			// 
			// ComboBox1
			// 
			this.ComboBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.ComboBox1.Location = new System.Drawing.Point(456, 8);
			this.ComboBox1.Name = "ComboBox1";
			this.ComboBox1.Size = new System.Drawing.Size(264, 21);
			this.ComboBox1.TabIndex = 4;
			this.ComboBox1.Text = "ComboBox1";
			this.ComboBox1.SelectedIndexChanged += new System.EventHandler(this.ComboBox1_SelectedIndexChanged);
			// 
			// axSymbologyControl1
			// 
			this.axSymbologyControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.axSymbologyControl1.Location = new System.Drawing.Point(456, 40);
			this.axSymbologyControl1.Name = "axSymbologyControl1";
			this.axSymbologyControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axSymbologyControl1.OcxState")));
			this.axSymbologyControl1.Size = new System.Drawing.Size(265, 265);
			this.axSymbologyControl1.TabIndex = 5;
			this.axSymbologyControl1.OnItemSelected += new ESRI.ArcGIS.Controls.ISymbologyControlEvents_Ax_OnItemSelectedEventHandler(this.axSymbologyControl1_OnItemSelected);
			// 
			// axToolbarControl1
			// 
			this.axToolbarControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.axToolbarControl1.Location = new System.Drawing.Point(8, 8);
			this.axToolbarControl1.Name = "axToolbarControl1";
			this.axToolbarControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axToolbarControl1.OcxState")));
			this.axToolbarControl1.Size = new System.Drawing.Size(440, 28);
			this.axToolbarControl1.TabIndex = 6;
			// 
			// axPageLayoutControl1
			// 
			this.axPageLayoutControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left)));
			this.axPageLayoutControl1.Location = new System.Drawing.Point(8, 40);
			this.axPageLayoutControl1.Name = "axPageLayoutControl1";
			this.axPageLayoutControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axPageLayoutControl1.OcxState")));
			this.axPageLayoutControl1.Size = new System.Drawing.Size(440, 432);
			this.axPageLayoutControl1.TabIndex = 7;
			this.axPageLayoutControl1.OnMouseDown += new ESRI.ArcGIS.Controls.IPageLayoutControlEvents_Ax_OnMouseDownEventHandler(this.axPageLayoutControl1_OnMouseDown);
			// 
			// axLicenseControl1
			// 
			this.axLicenseControl1.Enabled = true;
			this.axLicenseControl1.Location = new System.Drawing.Point(336, 24);
			this.axLicenseControl1.Name = "axLicenseControl1";
			this.axLicenseControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axLicenseControl1.OcxState")));
			this.axLicenseControl1.Size = new System.Drawing.Size(32, 32);
			this.axLicenseControl1.TabIndex = 8;
			// 
			// Form1
			// 
            
            //Removed setting for AutoScaleBaseSize
            //this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);

            //set AutoScaleDimensions & AutoScaleMode to allow AdjustBounds to scale controls correctly at 120 dpi
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;

			this.ClientSize = new System.Drawing.Size(728, 478);
			this.Controls.Add(this.axLicenseControl1);
			this.Controls.Add(this.axPageLayoutControl1);
			this.Controls.Add(this.axToolbarControl1);
			this.Controls.Add(this.axSymbologyControl1);
			this.Controls.Add(this.ComboBox1);
			this.Name = "Form1";
			this.Text = "Updating the Command Environment";
			this.Load += new System.EventHandler(this.Form1_Load);
			((System.ComponentModel.ISupportInitialize)(this.axSymbologyControl1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.axToolbarControl1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.axPageLayoutControl1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

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

            Application.Run(new CommandsEnvironment());
		}

		private void Form1_Load(object sender, System.EventArgs e)
		{
			//Resize the controls so that they scale correctly at both 96 and 120 dpi
            AdjustBounds(this.axToolbarControl1);
            AdjustBounds(this.axLicenseControl1);
            AdjustBounds(this.axPageLayoutControl1);
            AdjustBounds(this.axSymbologyControl1);
             
            //Set the buddy control
			axToolbarControl1.SetBuddyControl(axPageLayoutControl1);

			//Add items to the ToolbarControl
			axToolbarControl1.AddItem("esriControls.ControlsOpenDocCommand", -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
			axToolbarControl1.AddItem("esriControls.ControlsPageZoomInTool", -1, -1, true, 0, esriCommandStyles.esriCommandStyleIconOnly);
			axToolbarControl1.AddItem("esriControls.ControlsPageZoomOutTool", -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
			axToolbarControl1.AddItem("esriControls.ControlsPageZoomWholePageCommand", -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
			axToolbarControl1.AddItem("esriControls.ControlsSelectTool", -1, -1, true, 0, esriCommandStyles.esriCommandStyleIconOnly);
			axToolbarControl1.AddItem("esriControls.ControlsNewMarkerTool", -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
			axToolbarControl1.AddItem("esriControls.ControlsNewLineTool", -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
			axToolbarControl1.AddItem("esriControls.ControlsNewFreeHandTool", -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
			axToolbarControl1.AddItem("esriControls.ControlsNewRectangleTool", -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
			axToolbarControl1.AddItem("esriControls.ControlsNewPolygonTool", -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);

			//Get the ArcGIS install location by opening the subkey for reading			
			//Load the ESRI.ServerStyle file into the SymbologyControl
            string installationFolder = ESRI.ArcGIS.RuntimeManager.ActiveRuntime.Path;
            axSymbologyControl1.LoadStyleFile(installationFolder + "\\Styles\\ESRI.ServerStyle");

			//Add style classes to the combo box
			ComboBox1.Items.Add("Default Marker Symbol");
			ComboBox1.Items.Add("Default Line Symbol");
			ComboBox1.Items.Add("Default Fill Symbol");
			ComboBox1.Items.Add("Default Text Symbol");
			ComboBox1.SelectedIndex = 0;

			//Update each style class. This forces item to be loaded into each style class.
			//When the contents of a server style file are loaded into the SymbologyControl 
			//items are 'demand loaded'. This is done to increase performance and means 
			//items are only loaded into a SymbologyStyleClass when it is the current StyleClass.
			axSymbologyControl1.GetStyleClass(esriSymbologyStyleClass.esriStyleClassMarkerSymbols).Update();
			axSymbologyControl1.GetStyleClass(esriSymbologyStyleClass.esriStyleClassLineSymbols).Update();
			axSymbologyControl1.GetStyleClass(esriSymbologyStyleClass.esriStyleClassFillSymbols).Update();
			axSymbologyControl1.GetStyleClass(esriSymbologyStyleClass.esriStyleClassTextSymbols).Update();

			//Get the CommandsEnvironment singleton
			m_graphicProperties = new CommandsEnvironmentClass();

			//Create a new ServerStyleGalleryItem and set its name
			IStyleGalleryItem styleGalleryItem = new ServerStyleGalleryItemClass();
			styleGalleryItem.Name = "myStyle";

			ISymbologyStyleClass styleClass;

			//Get the marker symbol style class
			styleClass = axSymbologyControl1.GetStyleClass(esriSymbologyStyleClass.esriStyleClassMarkerSymbols);
			//Set the commands environment marker symbol into the item
			styleGalleryItem.Item = m_graphicProperties.MarkerSymbol;
			//Add the item to the style class
			styleClass.AddItem(styleGalleryItem, 0);

			//Get the line symbol style class
			styleClass = axSymbologyControl1.GetStyleClass(esriSymbologyStyleClass.esriStyleClassLineSymbols);
			//Set the commands environment line symbol into the item
			styleGalleryItem.Item = m_graphicProperties.LineSymbol;
			//Add the item to the style class
			styleClass.AddItem(styleGalleryItem, 0);

			//Get the fill symbol style class
			styleClass = axSymbologyControl1.GetStyleClass(esriSymbologyStyleClass.esriStyleClassFillSymbols);
			//Set the commands environment fill symbol into the item
			styleGalleryItem.Item = m_graphicProperties.FillSymbol;
			//Add the item to the style class
			styleClass.AddItem(styleGalleryItem, 0);

			//Get the text symbol style class
			styleClass = axSymbologyControl1.GetStyleClass(esriSymbologyStyleClass.esriStyleClassTextSymbols);
			//Set the commands environment text symbol into the item
			styleGalleryItem.Item = m_graphicProperties.TextSymbol;
			//Add the item to the style class
			styleClass.AddItem(styleGalleryItem, 0);

		}

		private void ComboBox1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			//Set the SymbologyControl style class
			if (ComboBox1.SelectedItem.ToString() == "Default Marker Symbol")
				axSymbologyControl1.StyleClass = esriSymbologyStyleClass.esriStyleClassMarkerSymbols;
			else if (ComboBox1.SelectedItem.ToString() == "Default Line Symbol")
				axSymbologyControl1.StyleClass = esriSymbologyStyleClass.esriStyleClassLineSymbols;
			else if (ComboBox1.SelectedItem.ToString() == "Default Fill Symbol")
				axSymbologyControl1.StyleClass = esriSymbologyStyleClass.esriStyleClassFillSymbols;
			else if (ComboBox1.SelectedItem.ToString() == "Default Text Symbol")
				axSymbologyControl1.StyleClass = esriSymbologyStyleClass.esriStyleClassTextSymbols;
		}

		private void axSymbologyControl1_OnItemSelected(object sender, ESRI.ArcGIS.Controls.ISymbologyControlEvents_OnItemSelectedEvent e)
		{
			IStyleGalleryItem styleGalleryItem = (IStyleGalleryItem) e.styleGalleryItem;

			if (styleGalleryItem.Item is IMarkerSymbol)
				//Set the default marker symbol
				m_graphicProperties.MarkerSymbol = (IMarkerSymbol) styleGalleryItem.Item;
			else if (styleGalleryItem.Item is ILineSymbol) 
				//Set the default line symbol
				m_graphicProperties.LineSymbol = (ILineSymbol) styleGalleryItem.Item;
			else if (styleGalleryItem.Item is IFillSymbol) 
				//Set the default fill symbol
				m_graphicProperties.FillSymbol = (IFillSymbol) styleGalleryItem.Item;
			else if (styleGalleryItem.Item is ITextSymbol)
				//Set the default text symbol
				m_graphicProperties.TextSymbol = (ITextSymbol) styleGalleryItem.Item;
		}

		private void axPageLayoutControl1_OnMouseDown(object sender, ESRI.ArcGIS.Controls.IPageLayoutControlEvents_OnMouseDownEvent e)
		{
			if (e.button != 2) return; 

			//Create a new point
			IPoint pPoint = new PointClass();
			pPoint.PutCoords(e.pageX, e.pageY);

			//Create a new text element 
			ITextElement textElement = new TextElementClass();
			//Set the text to display today's date
			textElement.Text = DateTime.Now.ToShortDateString();

			//Add element to graphics container using the CommandsEnvironment default text symbol
			axPageLayoutControl1.AddElement((IElement) textElement, pPoint, m_graphicProperties.TextSymbol, "", 0);
			//Refresh the graphics
			axPageLayoutControl1.Refresh(esriViewDrawPhase.esriViewGraphics, null, null);
		}

        private void AdjustBounds(AxHost controlToAdjust)
        {
            if (this.CurrentAutoScaleDimensions.Width != 6F)
            {
                //Adjust location: ActiveX control doesn't do this by itself 
                controlToAdjust.Left = Convert.ToInt32(controlToAdjust.Left * this.CurrentAutoScaleDimensions.Width / 6F);
                //Undo the automatic resize... 
                controlToAdjust.Width = controlToAdjust.Width / DPIX() * 96;
                //...and apply the appropriate resize
                controlToAdjust.Width = Convert.ToInt32(controlToAdjust.Width * this.CurrentAutoScaleDimensions.Width / 6F);
            }
            if (this.CurrentAutoScaleDimensions.Height != 13F)
            {
                //Adjust location: ActiveX control doesn't do this by itself 
                controlToAdjust.Top = Convert.ToInt32(controlToAdjust.Top * this.CurrentAutoScaleDimensions.Height / 13F);
                //Undo the automatic resize... 
                controlToAdjust.Height = controlToAdjust.Height / DPIY() * 96;
                //...and apply the appropriate resize 
                controlToAdjust.Height = Convert.ToInt32(controlToAdjust.Height * this.CurrentAutoScaleDimensions.Height / 13F);
            }
        }
        [System.Runtime.InteropServices.DllImport("Gdi32.dll")]
        static extern int GetDeviceCaps(IntPtr hDC, int nIndex);
        [System.Runtime.InteropServices.DllImport("Gdi32.dll")]
        static extern IntPtr CreateDC(string lpszDriver, string lpszDeviceName, string lpszOutput, IntPtr devMode);
        const int LOGPIXELSX = 88;
        const int LOGPIXELSY = 90;
        int DPIX()
        {
            return DPI(LOGPIXELSX);
        }
        int DPIY()
        {
            return DPI(LOGPIXELSY);
        }
        int DPI(int logPixelOrientation)
        {
            IntPtr displayPointer = CreateDC("DISPLAY", null, null, IntPtr.Zero);
            return Convert.ToInt32(GetDeviceCaps(displayPointer, logPixelOrientation));
        }
	} 
}
