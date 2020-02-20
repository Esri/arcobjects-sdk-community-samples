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
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS;
namespace LayerRendering
{

	public class Form1 : System.Windows.Forms.Form
	{
		public System.Windows.Forms.Label Label1;
		public System.Windows.Forms.Label Label2;
		private ESRI.ArcGIS.Controls.AxToolbarControl axToolbarControl1;
		private ESRI.ArcGIS.Controls.AxTOCControl axTOCControl1;
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
			this.Label1 = new System.Windows.Forms.Label();
			this.Label2 = new System.Windows.Forms.Label();
			this.axToolbarControl1 = new ESRI.ArcGIS.Controls.AxToolbarControl();
			this.axTOCControl1 = new ESRI.ArcGIS.Controls.AxTOCControl();
			this.axPageLayoutControl1 = new ESRI.ArcGIS.Controls.AxPageLayoutControl();
			this.axLicenseControl1 = new ESRI.ArcGIS.Controls.AxLicenseControl();
			((System.ComponentModel.ISupportInitialize)(this.axToolbarControl1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.axTOCControl1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.axPageLayoutControl1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).BeginInit();
			this.SuspendLayout();
			// 
			// Label1
			// 
			this.Label1.BackColor = System.Drawing.SystemColors.Control;
			this.Label1.Cursor = System.Windows.Forms.Cursors.Default;
			this.Label1.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Label1.ForeColor = System.Drawing.SystemColors.ControlText;
			this.Label1.Location = new System.Drawing.Point(568, 48);
			this.Label1.Name = "Label1";
			this.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Label1.Size = new System.Drawing.Size(160, 33);
			this.Label1.TabIndex = 6;
			this.Label1.Text = "1) Load a map document into the PageLayoutControl. ";
			// 
			// Label2
			// 
			this.Label2.BackColor = System.Drawing.SystemColors.Control;
			this.Label2.Cursor = System.Windows.Forms.Cursors.Default;
			this.Label2.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Label2.ForeColor = System.Drawing.SystemColors.ControlText;
			this.Label2.Location = new System.Drawing.Point(568, 80);
			this.Label2.Name = "Label2";
			this.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Label2.Size = new System.Drawing.Size(152, 33);
			this.Label2.TabIndex = 5;
			this.Label2.Text = "2) Right click on a feature layer to change its symbology";
			// 
			// axToolbarControl1
			// 
			this.axToolbarControl1.Location = new System.Drawing.Point(8, 8);
			this.axToolbarControl1.Name = "axToolbarControl1";
			this.axToolbarControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axToolbarControl1.OcxState")));
			this.axToolbarControl1.Size = new System.Drawing.Size(712, 28);
			this.axToolbarControl1.TabIndex = 7;
			// 
			// axTOCControl1
			// 
			this.axTOCControl1.Location = new System.Drawing.Point(8, 40);
			this.axTOCControl1.Name = "axTOCControl1";
			this.axTOCControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axTOCControl1.OcxState")));
			this.axTOCControl1.Size = new System.Drawing.Size(192, 352);
			this.axTOCControl1.TabIndex = 8;
			this.axTOCControl1.OnMouseDown += new ESRI.ArcGIS.Controls.ITOCControlEvents_Ax_OnMouseDownEventHandler(this.axTOCControl1_OnMouseDown);
			// 
			// axPageLayoutControl1
			// 
			this.axPageLayoutControl1.Location = new System.Drawing.Point(208, 40);
			this.axPageLayoutControl1.Name = "axPageLayoutControl1";
			this.axPageLayoutControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axPageLayoutControl1.OcxState")));
			this.axPageLayoutControl1.Size = new System.Drawing.Size(352, 352);
			this.axPageLayoutControl1.TabIndex = 9;
			// 
			// axLicenseControl1
			// 
			this.axLicenseControl1.Enabled = true;
			this.axLicenseControl1.Location = new System.Drawing.Point(576, 136);
			this.axLicenseControl1.Name = "axLicenseControl1";
			this.axLicenseControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axLicenseControl1.OcxState")));
			this.axLicenseControl1.Size = new System.Drawing.Size(32, 32);
			this.axLicenseControl1.TabIndex = 10;
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(728, 398);
			this.Controls.Add(this.axLicenseControl1);
			this.Controls.Add(this.axPageLayoutControl1);
			this.Controls.Add(this.axTOCControl1);
			this.Controls.Add(this.axToolbarControl1);
			this.Controls.Add(this.Label1);
			this.Controls.Add(this.Label2);
			this.Name = "Form1";
			this.Text = "Layer Rendering";
			this.Load += new System.EventHandler(this.Form1_Load);
			((System.ComponentModel.ISupportInitialize)(this.axToolbarControl1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.axTOCControl1)).EndInit();
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
			//Set buddy control
			axToolbarControl1.SetBuddyControl(axPageLayoutControl1);
			axTOCControl1.SetBuddyControl(axPageLayoutControl1);

			//Add ToolbarControl items
			axToolbarControl1.AddItem("esriControls.ControlsOpenDocCommand", -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
			axToolbarControl1.AddItem("esriControls.ControlsPageZoomInTool", -1, -1, true, 0, esriCommandStyles.esriCommandStyleIconOnly);
			axToolbarControl1.AddItem("esriControls.ControlsPageZoomOutTool", -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
			axToolbarControl1.AddItem("esriControls.ControlsPageZoomWholePageCommand", -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
			axToolbarControl1.AddItem("esriControls.ControlsPageFocusNextMapCommand", -1, -1, true, 0, esriCommandStyles.esriCommandStyleIconOnly);
			axToolbarControl1.AddItem("esriControls.ControlsPageFocusPreviousMapCommand", -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
			axToolbarControl1.AddItem("esriControls.ControlsSelectTool", -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
		}

		private void axTOCControl1_OnMouseDown(object sender, ESRI.ArcGIS.Controls.ITOCControlEvents_OnMouseDownEvent e)
		{
			//Exit if not right mouse button
			if (e.button != 2) return;

			IBasicMap map = new MapClass();
			ILayer layer = new FeatureLayerClass();
			object other = new Object();
			object index = new Object();
			esriTOCControlItem item = new esriTOCControlItem(); 

			//Determine what kind of item has been clicked on
			axTOCControl1.HitTest(e.x, e.y, ref item, ref map, ref layer, ref other, ref index);

			//QI to IFeatureLayer and IGeoFeatuerLayer interface
			if (layer == null) return;
			IFeatureLayer featureLayer = layer as IFeatureLayer;
			if (featureLayer == null) return;
			IGeoFeatureLayer geoFeatureLayer = (IGeoFeatureLayer) featureLayer;
			ISimpleRenderer simpleRenderer = (ISimpleRenderer) geoFeatureLayer.Renderer;

			//Create the form with the SymbologyControl
			frmSymbol symbolForm = new frmSymbol();

			//Get the IStyleGalleryItem
			IStyleGalleryItem styleGalleryItem = null;
			//Select SymbologyStyleClass based upon feature type
			switch (featureLayer.FeatureClass.ShapeType)
			{
				case esriGeometryType.esriGeometryPoint:
					styleGalleryItem = symbolForm.GetItem(esriSymbologyStyleClass.esriStyleClassMarkerSymbols, simpleRenderer.Symbol);
					break;
				case esriGeometryType.esriGeometryPolyline:
					styleGalleryItem = symbolForm.GetItem(esriSymbologyStyleClass.esriStyleClassLineSymbols, simpleRenderer.Symbol);
					break;
				case esriGeometryType.esriGeometryPolygon:
					styleGalleryItem = symbolForm.GetItem(esriSymbologyStyleClass.esriStyleClassFillSymbols, simpleRenderer.Symbol);
					break;
			}

			//Release the form
			symbolForm.Dispose();
			this.Activate();

			if (styleGalleryItem == null) return; 

			//Create a new renderer
			simpleRenderer = new SimpleRendererClass();
			//Set its symbol from the styleGalleryItem
			simpleRenderer.Symbol = (ISymbol) styleGalleryItem.Item;
			//Set the renderer into the geoFeatureLayer
			geoFeatureLayer.Renderer = (IFeatureRenderer) simpleRenderer;

			//Fire contents changed event that the TOCControl listens to
			axPageLayoutControl1.ActiveView.ContentsChanged();
			//Refresh the display
			axPageLayoutControl1.Refresh(esriViewDrawPhase.esriViewGeography, null, null);

		}
	}
}
