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
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS;
namespace AddMapSurrounds
{
	public class AddMapSurrounds : System.Windows.Forms.Form
	{
		internal System.Windows.Forms.Label Label2;
		internal System.Windows.Forms.Label Label1;
		private ESRI.ArcGIS.Controls.AxLicenseControl axLicenseControl1;
		private ESRI.ArcGIS.Controls.AxToolbarControl axToolbarControl1;
		private ESRI.ArcGIS.Controls.AxTOCControl axTOCControl1;
		private ESRI.ArcGIS.Controls.AxPageLayoutControl axPageLayoutControl1;
		private System.ComponentModel.Container components = null;

		public AddMapSurrounds()
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

		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddMapSurrounds));
            this.Label2 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.axLicenseControl1 = new ESRI.ArcGIS.Controls.AxLicenseControl();
            this.axToolbarControl1 = new ESRI.ArcGIS.Controls.AxToolbarControl();
            this.axTOCControl1 = new ESRI.ArcGIS.Controls.AxTOCControl();
            this.axPageLayoutControl1 = new ESRI.ArcGIS.Controls.AxPageLayoutControl();
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axToolbarControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axTOCControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axPageLayoutControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // Label2
            // 
            this.Label2.Location = new System.Drawing.Point(627, 83);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(153, 31);
            this.Label2.TabIndex = 6;
            this.Label2.Text = "2) Use the palette to add map surrounds onto the page";
            // 
            // Label1
            // 
            this.Label1.Location = new System.Drawing.Point(627, 42);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(153, 32);
            this.Label1.TabIndex = 5;
            this.Label1.Text = "1) Load a map document into the PageLayoutControl. ";
            // 
            // axLicenseControl1
            // 
            this.axLicenseControl1.Enabled = true;
            this.axLicenseControl1.Location = new System.Drawing.Point(627, 125);
            this.axLicenseControl1.Name = "axLicenseControl1";
            this.axLicenseControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axLicenseControl1.OcxState")));
            this.axLicenseControl1.Size = new System.Drawing.Size(32, 32);
            this.axLicenseControl1.TabIndex = 7;
            // 
            // axToolbarControl1
            // 
            this.axToolbarControl1.Location = new System.Drawing.Point(7, 7);
            this.axToolbarControl1.Name = "axToolbarControl1";
            this.axToolbarControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axToolbarControl1.OcxState")));
            this.axToolbarControl1.Size = new System.Drawing.Size(773, 28);
            this.axToolbarControl1.TabIndex = 8;
            // 
            // axTOCControl1
            // 
            this.axTOCControl1.Location = new System.Drawing.Point(7, 42);
            this.axTOCControl1.Name = "axTOCControl1";
            this.axTOCControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axTOCControl1.OcxState")));
            this.axTOCControl1.Size = new System.Drawing.Size(180, 450);
            this.axTOCControl1.TabIndex = 9;
            // 
            // axPageLayoutControl1
            // 
            this.axPageLayoutControl1.Location = new System.Drawing.Point(193, 42);
            this.axPageLayoutControl1.Name = "axPageLayoutControl1";
            this.axPageLayoutControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axPageLayoutControl1.OcxState")));
            this.axPageLayoutControl1.Size = new System.Drawing.Size(428, 450);
            this.axPageLayoutControl1.TabIndex = 10;
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(792, 498);
            this.Controls.Add(this.axPageLayoutControl1);
            this.Controls.Add(this.axTOCControl1);
            this.Controls.Add(this.axToolbarControl1);
            this.Controls.Add(this.axLicenseControl1);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.Label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Form1";
            this.Text = "Add Map Surrounds";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axToolbarControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axTOCControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axPageLayoutControl1)).EndInit();
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
            Application.Run(new AddMapSurrounds());
        }

		private void Form1_Load(object sender, System.EventArgs e)
		{
	        //Set buddy control
			axToolbarControl1.SetBuddyControl(axPageLayoutControl1);
            axTOCControl1.SetBuddyControl(axPageLayoutControl1);

			//Add ToolbarControl items
			axToolbarControl1.AddItem("esriControls.ControlsOpenDocCommand", 0, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
			axToolbarControl1.AddItem("esriControls.ControlsPageZoomInTool", 0, -1, true, 0, esriCommandStyles.esriCommandStyleIconOnly);
			axToolbarControl1.AddItem("esriControls.ControlsPageZoomOutTool", 0, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
			axToolbarControl1.AddItem("esriControls.ControlsPageZoomWholePageCommand", 0, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
			axToolbarControl1.AddItem("esriControls.ControlsMapZoomInTool", 0, -1, true, 0, esriCommandStyles.esriCommandStyleIconOnly);
			axToolbarControl1.AddItem("esriControls.ControlsMapZoomOutTool", 0, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
			axToolbarControl1.AddItem("esriControls.ControlsMapPanTool", 0, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
			axToolbarControl1.AddItem("esriControls.ControlsMapFullExtentCommand", 0, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
			axToolbarControl1.AddItem("esriControls.ControlsSelectTool", 0, -1, true, 0, esriCommandStyles.esriCommandStyleIconOnly);

			//Create a new ToolbarPalette 
			IToolbarPalette toolbarPalette = new ToolbarPalette();
			toolbarPalette.Caption = "Map Surrounds";
			toolbarPalette.AddItem(new CreateNorthArrow(), -1, -1);
			toolbarPalette.AddItem(new CreateScaleBar(), -1, -1);
			toolbarPalette.AddItem(new CreateScaleText(), -1, -1);
			//Add the ToolbarPalette to the ToolbarControl
			axToolbarControl1.AddItem(toolbarPalette, -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
		}
	}
}
