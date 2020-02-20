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
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS;
namespace PageProperties
{
	public class Form1 : System.Windows.Forms.Form
	{
		internal System.Windows.Forms.ComboBox ComboBox1;
		private ESRI.ArcGIS.Controls.AxToolbarControl axToolbarControl1;
		private ESRI.ArcGIS.Controls.AxSymbologyControl axSymbologyControl1;
		private ESRI.ArcGIS.Controls.AxPageLayoutControl axPageLayoutControl1;
		private ESRI.ArcGIS.Controls.AxLicenseControl axLicenseControl1;

		private System.ComponentModel.Container components = null;

		public Form1()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Form1));
			this.ComboBox1 = new System.Windows.Forms.ComboBox();
			this.axToolbarControl1 = new ESRI.ArcGIS.Controls.AxToolbarControl();
			this.axSymbologyControl1 = new ESRI.ArcGIS.Controls.AxSymbologyControl();
			this.axPageLayoutControl1 = new ESRI.ArcGIS.Controls.AxPageLayoutControl();
			this.axLicenseControl1 = new ESRI.ArcGIS.Controls.AxLicenseControl();
			((System.ComponentModel.ISupportInitialize)(this.axToolbarControl1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.axSymbologyControl1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.axPageLayoutControl1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).BeginInit();
			this.SuspendLayout();
			// 
			// ComboBox1
			// 
			this.ComboBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.ComboBox1.Location = new System.Drawing.Point(424, 8);
			this.ComboBox1.Name = "ComboBox1";
			this.ComboBox1.Size = new System.Drawing.Size(258, 21);
			this.ComboBox1.TabIndex = 5;
			this.ComboBox1.Text = "ComboBox1";
			this.ComboBox1.SelectedIndexChanged += new System.EventHandler(this.ComboBox1_SelectedIndexChanged);
			// 
			// axToolbarControl1
			// 
			this.axToolbarControl1.Location = new System.Drawing.Point(8, 8);
			this.axToolbarControl1.Name = "axToolbarControl1";
			this.axToolbarControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axToolbarControl1.OcxState")));
			this.axToolbarControl1.Size = new System.Drawing.Size(408, 28);
			this.axToolbarControl1.TabIndex = 6;
			// 
			// axSymbologyControl1
			// 
			this.axSymbologyControl1.Location = new System.Drawing.Point(424, 40);
			this.axSymbologyControl1.Name = "axSymbologyControl1";
			this.axSymbologyControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axSymbologyControl1.OcxState")));
			this.axSymbologyControl1.Size = new System.Drawing.Size(265, 265);
			this.axSymbologyControl1.TabIndex = 7;
			this.axSymbologyControl1.OnItemSelected += new ESRI.ArcGIS.Controls.ISymbologyControlEvents_Ax_OnItemSelectedEventHandler(this.axSymbologyControl1_OnItemSelected);
			// 
			// axPageLayoutControl1
			// 
			this.axPageLayoutControl1.Location = new System.Drawing.Point(8, 40);
			this.axPageLayoutControl1.Name = "axPageLayoutControl1";
			this.axPageLayoutControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axPageLayoutControl1.OcxState")));
			this.axPageLayoutControl1.Size = new System.Drawing.Size(408, 368);
			this.axPageLayoutControl1.TabIndex = 8;
			this.axPageLayoutControl1.OnPageLayoutReplaced += new ESRI.ArcGIS.Controls.IPageLayoutControlEvents_Ax_OnPageLayoutReplacedEventHandler(this.axPageLayoutControl1_OnPageLayoutReplaced);
			// 
			// axLicenseControl1
			// 
			this.axLicenseControl1.Enabled = true;
			this.axLicenseControl1.Location = new System.Drawing.Point(336, 24);
			this.axLicenseControl1.Name = "axLicenseControl1";
			this.axLicenseControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axLicenseControl1.OcxState")));
			this.axLicenseControl1.Size = new System.Drawing.Size(32, 32);
			this.axLicenseControl1.TabIndex = 9;
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(698, 414);
			this.Controls.Add(this.axLicenseControl1);
			this.Controls.Add(this.axPageLayoutControl1);
			this.Controls.Add(this.axSymbologyControl1);
			this.Controls.Add(this.axToolbarControl1);
			this.Controls.Add(this.ComboBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "Form1";
			this.Text = "Setting Frame Properties";
			this.Load += new System.EventHandler(this.Form1_Load);
			((System.ComponentModel.ISupportInitialize)(this.axToolbarControl1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.axSymbologyControl1)).EndInit();
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
            Application.Run(new Form1());
        }

		private void Form1_Load(object sender, System.EventArgs e)
		{
			//Set the buddy control
			axToolbarControl1.SetBuddyControl(axPageLayoutControl1);

			//Add items to the ToolbarControl
			axToolbarControl1.AddItem("esriControls.ControlsOpenDocCommand", -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconAndText);
			axToolbarControl1.AddItem("esriControls.ControlsSaveAsDocCommand", -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconAndText);
			axToolbarControl1.AddItem("esriControls.ControlsPageZoomInTool", -1, -1, true, 0, esriCommandStyles.esriCommandStyleIconAndText);
			axToolbarControl1.AddItem("esriControls.ControlsPageZoomOutTool", -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconAndText);
			axToolbarControl1.AddItem("esriControls.ControlsPageZoomWholePageCommand", -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconAndText);

			//Get the ArcGIS install location by opening the subkey for reading
            string sInstallPath = ESRI.ArcGIS.RuntimeManager.ActiveRuntime.Path;
			//Load the ESRI.ServerStyle file into the SymbologyControl
			axSymbologyControl1.LoadStyleFile(sInstallPath + "\\Styles\\ESRI.ServerStyle");

			//Add style classes to the combo box
			ComboBox1.Items.Add("Backgrounds");
			ComboBox1.Items.Add("Borders");
			ComboBox1.Items.Add("Shadows");
			ComboBox1.SelectedIndex = 0;

			//Update each style class. This forces item to be loaded into each style class.
			//When the contents of a server style file are loaded into the SymbologyControl 
			//items are 'demand loaded'. This is done to increase performance and means 
			//items are only loaded into a SymbologyStyleClass when it is the current StyleClass.
			axSymbologyControl1.GetStyleClass(esriSymbologyStyleClass.esriStyleClassBackgrounds).Update();
			axSymbologyControl1.GetStyleClass(esriSymbologyStyleClass.esriStyleClassBorders).Update();
			axSymbologyControl1.GetStyleClass(esriSymbologyStyleClass.esriStyleClassShadows).Update();
		}

		private void ComboBox1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			//Set the SymbologyControl style class
			if (ComboBox1.SelectedItem.ToString() == "Backgrounds")
				axSymbologyControl1.StyleClass = esriSymbologyStyleClass.esriStyleClassBackgrounds;
			else if (ComboBox1.SelectedItem.ToString() == "Borders")
				axSymbologyControl1.StyleClass = esriSymbologyStyleClass.esriStyleClassBorders;
			else if (ComboBox1.SelectedItem.ToString() == "Shadows" )
				axSymbologyControl1.StyleClass = esriSymbologyStyleClass.esriStyleClassShadows;
		}

		private void axSymbologyControl1_OnItemSelected(object sender, ESRI.ArcGIS.Controls.ISymbologyControlEvents_OnItemSelectedEvent e)
		{	
			IStyleGalleryItem styleGalleryItem = (IStyleGalleryItem) e.styleGalleryItem;

			//Get the frame containing the focus map
			IFrameProperties frameProperties = (IFrameProperties) axPageLayoutControl1.GraphicsContainer.FindFrame(axPageLayoutControl1.ActiveView.FocusMap);

			if  (styleGalleryItem.Item is IBackground)
				//Set the frame's background
				frameProperties.Background = (IBackground) styleGalleryItem.Item;
			else if (styleGalleryItem.Item is IBorder)
				//Set the frame's border
				frameProperties.Border = (IBorder) styleGalleryItem.Item;
			else if (styleGalleryItem.Item is IShadow)
				//Set the frame's shadow
				frameProperties.Shadow = (IShadow) styleGalleryItem.Item;

			//Refresh the PageLayoutControl
			axPageLayoutControl1.Refresh(esriViewDrawPhase.esriViewBackground, null, null);
		}

		private void axPageLayoutControl1_OnPageLayoutReplaced(object sender, ESRI.ArcGIS.Controls.IPageLayoutControlEvents_OnPageLayoutReplacedEvent e)
		{
			//Get the frame containing the focus map
			IFrameProperties frameProperties = (IFrameProperties) axPageLayoutControl1.GraphicsContainer.FindFrame(axPageLayoutControl1.ActiveView.FocusMap);

			//Create a new ServerStyleGalleryItem with its name set
			IStyleGalleryItem styleGalleryItem = new ServerStyleGalleryItemClass();
			styleGalleryItem.Name = "myStyle"; 
			
			ISymbologyStyleClass styleClass;


			//Get the background style class and remove any custom style
			styleClass = axSymbologyControl1.GetStyleClass(esriSymbologyStyleClass.esriStyleClassBackgrounds);
			if (styleClass.GetItem(0).Name == "myStyle") styleClass.RemoveItem(0);
			if (frameProperties.Background != null)
			{
				//Set the background into the style gallery item
				styleGalleryItem.Item = frameProperties.Background;
				//Add the item to the style class
				styleClass.AddItem(styleGalleryItem, 0);
			}

			//Get the border style class and remove any custom style
			styleClass = axSymbologyControl1.GetStyleClass(esriSymbologyStyleClass.esriStyleClassBorders);
			if (styleClass.GetItem(0).Name == "myStyle") styleClass.RemoveItem(0);
			if (frameProperties.Border != null)
			{
				//Set the border into the style gallery item
				styleGalleryItem.Item = frameProperties.Border;
				//Add the item to the style class
				styleClass.AddItem(styleGalleryItem, 0);
			}

			//Get the shadow style class and remove any custom style
			styleClass = axSymbologyControl1.GetStyleClass(esriSymbologyStyleClass.esriStyleClassShadows);
			if (styleClass.GetItem(0).Name == "myStyle") styleClass.RemoveItem(0);
			if (frameProperties.Shadow != null)
			{
				//Set the shadow into the style gallery item
				styleGalleryItem.Item = frameProperties.Shadow;
				//Add the item to the style class
				styleClass.AddItem(styleGalleryItem, 0);
			}

		}

	}
}
