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
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS;
namespace ColorRamps
{

	public class Form1 : System.Windows.Forms.Form
	{
		private ESRI.ArcGIS.Controls.AxToolbarControl axToolbarControl1;
		private ESRI.ArcGIS.Controls.AxPageLayoutControl axPageLayoutControl1;
		private System.Windows.Forms.ComboBox comboBox1;
		private System.Windows.Forms.Button button1;
		private ESRI.ArcGIS.Controls.AxTOCControl axTOCControl1;
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
			this.axToolbarControl1 = new ESRI.ArcGIS.Controls.AxToolbarControl();
			this.axPageLayoutControl1 = new ESRI.ArcGIS.Controls.AxPageLayoutControl();
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.button1 = new System.Windows.Forms.Button();
			this.axTOCControl1 = new ESRI.ArcGIS.Controls.AxTOCControl();
			this.axLicenseControl1 = new ESRI.ArcGIS.Controls.AxLicenseControl();
			((System.ComponentModel.ISupportInitialize)(this.axToolbarControl1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.axPageLayoutControl1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.axTOCControl1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).BeginInit();
			this.SuspendLayout();
			// 
			// axToolbarControl1
			// 
			this.axToolbarControl1.Location = new System.Drawing.Point(8, 8);
			this.axToolbarControl1.Name = "axToolbarControl1";
			this.axToolbarControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axToolbarControl1.OcxState")));
			this.axToolbarControl1.Size = new System.Drawing.Size(392, 28);
			this.axToolbarControl1.TabIndex = 0;
			// 
			// axPageLayoutControl1
			// 
			this.axPageLayoutControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.axPageLayoutControl1.Location = new System.Drawing.Point(192, 40);
			this.axPageLayoutControl1.Name = "axPageLayoutControl1";
			this.axPageLayoutControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axPageLayoutControl1.OcxState")));
			this.axPageLayoutControl1.Size = new System.Drawing.Size(464, 384);
			this.axPageLayoutControl1.TabIndex = 1;
			this.axPageLayoutControl1.OnPageLayoutReplaced += new ESRI.ArcGIS.Controls.IPageLayoutControlEvents_Ax_OnPageLayoutReplacedEventHandler(this.axPageLayoutControl1_OnPageLayoutReplaced);
			// 
			// comboBox1
			// 
			this.comboBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.comboBox1.Location = new System.Drawing.Point(408, 8);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(121, 21);
			this.comboBox1.TabIndex = 3;
			this.comboBox1.Text = "comboBox1";
			// 
			// button1
			// 
			this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.button1.Location = new System.Drawing.Point(536, 8);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(120, 23);
			this.button1.TabIndex = 4;
			this.button1.Text = "Change Color Ramp";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// axTOCControl1
			// 
			this.axTOCControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left)));
			this.axTOCControl1.Location = new System.Drawing.Point(8, 40);
			this.axTOCControl1.Name = "axTOCControl1";
			this.axTOCControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axTOCControl1.OcxState")));
			this.axTOCControl1.Size = new System.Drawing.Size(176, 384);
			this.axTOCControl1.TabIndex = 5;
			// 
			// axLicenseControl1
			// 
			this.axLicenseControl1.Enabled = true;
			this.axLicenseControl1.Location = new System.Drawing.Point(176, 216);
			this.axLicenseControl1.Name = "axLicenseControl1";
			this.axLicenseControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axLicenseControl1.OcxState")));
			this.axLicenseControl1.Size = new System.Drawing.Size(32, 32);
			this.axLicenseControl1.TabIndex = 6;
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(664, 430);
			this.Controls.Add(this.axLicenseControl1);
			this.Controls.Add(this.axTOCControl1);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.comboBox1);
			this.Controls.Add(this.axPageLayoutControl1);
			this.Controls.Add(this.axToolbarControl1);
			this.Name = "Form1";
			this.Text = "Color Ramps";
			this.Load += new System.EventHandler(this.Form1_Load);
			((System.ComponentModel.ISupportInitialize)(this.axToolbarControl1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.axPageLayoutControl1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.axTOCControl1)).EndInit();
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
			axToolbarControl1.AddItem("esriControls.ControlsOpenDocCommand");
			axToolbarControl1.AddItem("esriControls.ControlsSaveAsDocCommand");
			axToolbarControl1.AddItem("esriControls.ControlsPageZoomInTool");
			axToolbarControl1.AddItem("esriControls.ControlsPageZoomOutTool");
			axToolbarControl1.AddItem("esriControls.ControlsPageZoomWholePageCommand");
			axToolbarControl1.AddItem("esriControls.ControlsMapZoomInTool");
			axToolbarControl1.AddItem("esriControls.ControlsMapZoomOutTool");
			axToolbarControl1.AddItem("esriControls.ControlsMapPanTool");
			axToolbarControl1.AddItem("esriControls.ControlsMapFullExtentCommand");
			axToolbarControl1.AddItem("esriControls.ControlsMapIdentifyTool");

			//Disable controls
			button1.Enabled = false;
			comboBox1.Enabled = false;
		}

		private void axPageLayoutControl1_OnPageLayoutReplaced(object sender, ESRI.ArcGIS.Controls.IPageLayoutControlEvents_OnPageLayoutReplacedEvent e)
		{
      
			//Clear the combo box
			comboBox1.Items.Clear();

			//Get IGeoFeatureLayers CLSID
			UID uid = new UIDClass();
			uid.Value = "{E156D7E5-22AF-11D3-9F99-00C04F6BC78E}";
			//Get IGeoFeatureLayers from the focus map 
			IEnumLayer layers = axPageLayoutControl1.ActiveView.FocusMap.get_Layers(uid, true);
			if (layers == null) return; 

			//Reset enumeration and loop through layers
			layers.Reset();
			IGeoFeatureLayer geoFeatureLayer = (IGeoFeatureLayer) layers.Next();
			while (geoFeatureLayer != null)
			{
				//If layer contains polygon features add to combo box
				if (geoFeatureLayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryPolygon)
				{
                    if ((geoFeatureLayer is IGroupLayer) == false) comboBox1.Items.Add(geoFeatureLayer.Name);
				}
				geoFeatureLayer = (IGeoFeatureLayer) layers.Next();
			}
			comboBox1.SelectedIndex = 0;

			//Enable controls
			button1.Enabled = true;
			comboBox1.Enabled = true;
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			//Get the layer selected in the combo box
			IGeoFeatureLayer geofeaturelayer = null;
			IMap map = axPageLayoutControl1.ActiveView.FocusMap;
			for (int i=0; i<= map.LayerCount-1; i++)
			{
				if (map.get_Layer(i).Name == comboBox1.SelectedItem.ToString())
				{
					geofeaturelayer = (IGeoFeatureLayer) map.get_Layer(i);
					break;
				}
			}
			if (geofeaturelayer == null) return;

			//Create ClassBreaks form 
			Form2 classBreaksForm = new  Form2();

			//Get a ClassBreakRenderer that uses the selected ColorRamp
			IClassBreaksRenderer classBreaksRenderer = classBreaksForm.GetClassBreaksRenderer(geofeaturelayer);
			if (classBreaksRenderer == null) return;

			//Set the new renderer 
			geofeaturelayer.Renderer = (IFeatureRenderer) classBreaksRenderer;

			//Trigger contents changed event for TOCControl
			axPageLayoutControl1.ActiveView.ContentsChanged();
			//Refresh the display 
			axPageLayoutControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, geofeaturelayer, null);

			//Dispose of the form
			classBreaksForm.Dispose();
		}

	}
}
