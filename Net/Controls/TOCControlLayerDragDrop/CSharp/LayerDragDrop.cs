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
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS;
namespace LayerDragDrop
{

	public class Form1 : System.Windows.Forms.Form
	{
		internal System.Windows.Forms.CheckBox chkDragDrop;
		internal System.Windows.Forms.Label Label6;
		internal System.Windows.Forms.Label Label5;
		internal System.Windows.Forms.Label Label2;
		internal System.Windows.Forms.Label Label4;
		internal System.Windows.Forms.Label Label3;
		internal System.Windows.Forms.Label Label1;
		private ESRI.ArcGIS.Controls.AxToolbarControl axToolbarControl1;
		private ESRI.ArcGIS.Controls.AxTOCControl axTOCControl1;
		private ESRI.ArcGIS.Controls.AxPageLayoutControl axPageLayoutControl1;
		private ESRI.ArcGIS.Controls.AxLicenseControl axLicenseControl1;
		private System.ComponentModel.Container components = null;

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
		}

		protected override void Dispose( bool disposing )
		{
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.chkDragDrop = new System.Windows.Forms.CheckBox();
            this.Label6 = new System.Windows.Forms.Label();
            this.Label5 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label4 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
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
            // chkDragDrop
            // 
            this.chkDragDrop.Location = new System.Drawing.Point(432, 16);
            this.chkDragDrop.Name = "chkDragDrop";
            this.chkDragDrop.Size = new System.Drawing.Size(168, 16);
            this.chkDragDrop.TabIndex = 5;
            this.chkDragDrop.Text = "Layer Drag and Drop";
            this.chkDragDrop.Click += new System.EventHandler(this.chkDragDrop_Click);
            // 
            // Label6
            // 
            this.Label6.Location = new System.Drawing.Point(432, 234);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(184, 56);
            this.Label6.TabIndex = 16;
            this.Label6.Text = "6) Move a layer into the new map by dragging and dropping a layer whilst holding " +
                "the CTRL key down inside the TOCControl";
            // 
            // Label5
            // 
            this.Label5.Location = new System.Drawing.Point(432, 184);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(184, 56);
            this.Label5.TabIndex = 15;
            this.Label5.Text = "5) Copy a layer into the new map by dragging and dropping a layer inside the TOCC" +
                "ontrol";
            // 
            // Label2
            // 
            this.Label2.Location = new System.Drawing.Point(432, 96);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(176, 32);
            this.Label2.TabIndex = 12;
            this.Label2.Text = "3) Re-order the layers in the TOCControl";
            // 
            // Label4
            // 
            this.Label4.Location = new System.Drawing.Point(432, 128);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(176, 56);
            this.Label4.TabIndex = 14;
            this.Label4.Text = "4) Right click on the PageLayoutControl and drag a rectangle to create a new MapF" +
                "rame";
            // 
            // Label3
            // 
            this.Label3.Location = new System.Drawing.Point(432, 72);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(176, 32);
            this.Label3.TabIndex = 13;
            this.Label3.Text = "2) Enable layer drag and drop";
            // 
            // Label1
            // 
            this.Label1.Location = new System.Drawing.Point(432, 40);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(176, 32);
            this.Label1.TabIndex = 11;
            this.Label1.Text = "1) Load a map document into the PageLayoutControl";
            // 
            // axToolbarControl1
            // 
            this.axToolbarControl1.Location = new System.Drawing.Point(8, 8);
            this.axToolbarControl1.Name = "axToolbarControl1";
            this.axToolbarControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axToolbarControl1.OcxState")));
            this.axToolbarControl1.Size = new System.Drawing.Size(416, 28);
            this.axToolbarControl1.TabIndex = 17;
            // 
            // axTOCControl1
            // 
            this.axTOCControl1.Location = new System.Drawing.Point(8, 40);
            this.axTOCControl1.Name = "axTOCControl1";
            this.axTOCControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axTOCControl1.OcxState")));
            this.axTOCControl1.Size = new System.Drawing.Size(144, 344);
            this.axTOCControl1.TabIndex = 18;
            // 
            // axPageLayoutControl1
            // 
            this.axPageLayoutControl1.Location = new System.Drawing.Point(160, 40);
            this.axPageLayoutControl1.Name = "axPageLayoutControl1";
            this.axPageLayoutControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axPageLayoutControl1.OcxState")));
            this.axPageLayoutControl1.Size = new System.Drawing.Size(256, 344);
            this.axPageLayoutControl1.TabIndex = 19;
            this.axPageLayoutControl1.OnMouseDown += new ESRI.ArcGIS.Controls.IPageLayoutControlEvents_Ax_OnMouseDownEventHandler(this.axPageLayoutControl1_OnMouseDown);
            // 
            // axLicenseControl1
            // 
            this.axLicenseControl1.Enabled = true;
            this.axLicenseControl1.Location = new System.Drawing.Point(56, 48);
            this.axLicenseControl1.Name = "axLicenseControl1";
            this.axLicenseControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axLicenseControl1.OcxState")));
            this.axLicenseControl1.Size = new System.Drawing.Size(32, 32);
            this.axLicenseControl1.TabIndex = 20;
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(616, 390);
            this.Controls.Add(this.axLicenseControl1);
            this.Controls.Add(this.axPageLayoutControl1);
            this.Controls.Add(this.axTOCControl1);
            this.Controls.Add(this.axToolbarControl1);
            this.Controls.Add(this.Label6);
            this.Controls.Add(this.Label5);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.Label4);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.chkDragDrop);
            this.Name = "Form1";
            this.Text = "Layer Drag and Drop";
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

			//Add items to the ToolbarControl
			axToolbarControl1.AddItem("esriControls.ControlsOpenDocCommand",-1,-1,false,0,esriCommandStyles.esriCommandStyleIconOnly);
			axToolbarControl1.AddItem("esriControls.ControlsPageZoomInTool",-1,-1,true,0,esriCommandStyles.esriCommandStyleIconOnly);
			axToolbarControl1.AddItem("esriControls.ControlsPageZoomOutTool",-1,-1,false,0,esriCommandStyles.esriCommandStyleIconOnly);
			axToolbarControl1.AddItem("esriControls.ControlsPageZoomWholePageCommand",-1,-1,false,0,esriCommandStyles.esriCommandStyleIconOnly);
			axToolbarControl1.AddItem("esriControls.ControlsPageFocusPreviousMapCommand",-1,-1,true,0,esriCommandStyles.esriCommandStyleIconOnly);
			axToolbarControl1.AddItem("esriControls.ControlsPageFocusNextMapCommand",-1,-1,false,0,esriCommandStyles.esriCommandStyleIconOnly);
			axToolbarControl1.AddItem("esriControls.ControlsMapFullExtentCommand",-1,-1,true,0,esriCommandStyles.esriCommandStyleIconOnly);
			axToolbarControl1.AddItem("esriControls.ControlsMapZoomInTool",-1,-1,false,0,esriCommandStyles.esriCommandStyleIconOnly);
			axToolbarControl1.AddItem("esriControls.ControlsMapZoomOutTool",-1,-1,false,0,esriCommandStyles.esriCommandStyleIconOnly);
			axToolbarControl1.AddItem("esriControls.ControlsSelectTool",-1,-1,true,0,esriCommandStyles.esriCommandStyleIconOnly);
		}

		private void chkDragDrop_Click(object sender, System.EventArgs e)
		{
			//Enable layer and drag and drop
			axTOCControl1.EnableLayerDragDrop = chkDragDrop.Checked;
		}

		private void axPageLayoutControl1_OnMouseDown(object sender, ESRI.ArcGIS.Controls.IPageLayoutControlEvents_OnMouseDownEvent e)
		{
			if (e. button == 1) return; 

			//Create an envelope by tracking a rectangle
			IEnvelope envelope = axPageLayoutControl1.TrackRectangle();

			//Create a map frame element with a new map
			IMapFrame mapFrame = new MapFrameClass();
			mapFrame.Map = new MapClass();

			//Add the map frame to the PageLayoutControl with specified geometry
			axPageLayoutControl1.AddElement((IElement)mapFrame, envelope, null, null, 0);
			//Refresh the PageLayoutControl
			axPageLayoutControl1.Refresh(esriViewDrawPhase.esriViewGraphics, null, null);
		}

	}
}
