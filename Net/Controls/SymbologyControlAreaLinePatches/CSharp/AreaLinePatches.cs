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
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS;
namespace AreaLinePatches
{

	public class Form1 : System.Windows.Forms.Form
	{
		private ESRI.ArcGIS.Controls.AxPageLayoutControl axPageLayoutControl1;
		private ESRI.ArcGIS.Controls.AxLicenseControl axLicenseControl1;
		private ESRI.ArcGIS.Controls.AxToolbarControl axToolbarControl1;
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Button cmdDraw;
		private System.Windows.Forms.Button cmdDelete;
		private System.Windows.Forms.Button cmdChangeArea;
		private System.Windows.Forms.Button cmdChangeLine;

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
			this.axPageLayoutControl1 = new ESRI.ArcGIS.Controls.AxPageLayoutControl();
			this.axLicenseControl1 = new ESRI.ArcGIS.Controls.AxLicenseControl();
			this.axToolbarControl1 = new ESRI.ArcGIS.Controls.AxToolbarControl();
			this.cmdDraw = new System.Windows.Forms.Button();
			this.cmdDelete = new System.Windows.Forms.Button();
			this.cmdChangeArea = new System.Windows.Forms.Button();
			this.cmdChangeLine = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.axPageLayoutControl1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.axToolbarControl1)).BeginInit();
			this.SuspendLayout();
			// 
			// axPageLayoutControl1
			// 
			this.axPageLayoutControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.axPageLayoutControl1.Location = new System.Drawing.Point(0, 48);
			this.axPageLayoutControl1.Name = "axPageLayoutControl1";
			this.axPageLayoutControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axPageLayoutControl1.OcxState")));
			this.axPageLayoutControl1.Size = new System.Drawing.Size(800, 536);
			this.axPageLayoutControl1.TabIndex = 0;
			this.axPageLayoutControl1.OnPageLayoutReplaced += new ESRI.ArcGIS.Controls.IPageLayoutControlEvents_Ax_OnPageLayoutReplacedEventHandler(this.axPageLayoutControl1_OnPageLayoutReplaced);
			// 
			// axLicenseControl1
			// 
			this.axLicenseControl1.Enabled = true;
			this.axLicenseControl1.Location = new System.Drawing.Point(64, 32);
			this.axLicenseControl1.Name = "axLicenseControl1";
			this.axLicenseControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axLicenseControl1.OcxState")));
			this.axLicenseControl1.Size = new System.Drawing.Size(32, 32);
			this.axLicenseControl1.TabIndex = 1;
			// 
			// axToolbarControl1
			// 
			this.axToolbarControl1.Location = new System.Drawing.Point(0, 0);
			this.axToolbarControl1.Name = "axToolbarControl1";
			this.axToolbarControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axToolbarControl1.OcxState")));
			this.axToolbarControl1.Size = new System.Drawing.Size(352, 28);
			this.axToolbarControl1.TabIndex = 6;
			// 
			// cmdDraw
			// 
			this.cmdDraw.Location = new System.Drawing.Point(368, 8);
			this.cmdDraw.Name = "cmdDraw";
			this.cmdDraw.Size = new System.Drawing.Size(88, 23);
			this.cmdDraw.TabIndex = 7;
			this.cmdDraw.Text = "Add Legend";
			this.cmdDraw.Click += new System.EventHandler(this.button1_Click);
			// 
			// cmdDelete
			// 
			this.cmdDelete.Location = new System.Drawing.Point(464, 8);
			this.cmdDelete.Name = "cmdDelete";
			this.cmdDelete.Size = new System.Drawing.Size(88, 23);
			this.cmdDelete.TabIndex = 8;
			this.cmdDelete.Text = "Delete Legend";
			this.cmdDelete.Click += new System.EventHandler(this.button2_Click);
			// 
			// cmdChangeArea
			// 
			this.cmdChangeArea.Location = new System.Drawing.Point(560, 8);
			this.cmdChangeArea.Name = "cmdChangeArea";
			this.cmdChangeArea.Size = new System.Drawing.Size(112, 23);
			this.cmdChangeArea.TabIndex = 9;
			this.cmdChangeArea.Text = "Change Area Patch";
			this.cmdChangeArea.Click += new System.EventHandler(this.button3_Click);
			// 
			// cmdChangeLine
			// 
			this.cmdChangeLine.Location = new System.Drawing.Point(680, 8);
			this.cmdChangeLine.Name = "cmdChangeLine";
			this.cmdChangeLine.Size = new System.Drawing.Size(112, 23);
			this.cmdChangeLine.TabIndex = 10;
			this.cmdChangeLine.Text = "Change Line Patch";
			this.cmdChangeLine.Click += new System.EventHandler(this.button4_Click);
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(800, 582);
			this.Controls.Add(this.cmdChangeLine);
			this.Controls.Add(this.cmdChangeArea);
			this.Controls.Add(this.cmdDelete);
			this.Controls.Add(this.cmdDraw);
			this.Controls.Add(this.axToolbarControl1);
			this.Controls.Add(this.axLicenseControl1);
			this.Controls.Add(this.axPageLayoutControl1);
			this.Name = "Form1";
			this.Text = "AreaLinePatches";
			this.Load += new System.EventHandler(this.Form1_Load);
			((System.ComponentModel.ISupportInitialize)(this.axPageLayoutControl1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.axToolbarControl1)).EndInit();
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
			axToolbarControl1.SetBuddyControl(this.axPageLayoutControl1);
	      
			//Add ToolbarControl items
			axToolbarControl1.AddItem("esriControls.ControlsOpenDocCommand");
			axToolbarControl1.AddItem("esriControls.ControlsSaveAsDocCommand");
			axToolbarControl1.AddItem("esriControls.ControlsPageZoomInTool");
			axToolbarControl1.AddItem("esriControls.ControlsPageZoomOutTool");
			axToolbarControl1.AddItem("esriControls.ControlsPageZoomWholePageCommand");
			axToolbarControl1.AddItem("esriControls.ControlsSelectTool");

			//disable buttons for draw legend, change area/line patches, delete legend
			cmdDraw.Enabled = false;
			cmdDelete.Enabled = false;
			cmdChangeArea.Enabled = false;    
			cmdChangeLine.Enabled = false;
			
		}

		private void axPageLayoutControl1_OnPageLayoutReplaced(object sender, ESRI.ArcGIS.Controls.IPageLayoutControlEvents_OnPageLayoutReplacedEvent e)
		{
			//when a document gets loaded into the PageLayoutControl enable the draw legend button
			cmdDraw.Enabled = true;
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			//Get the GraphicsContainer
			IGraphicsContainer graphicsContainer = axPageLayoutControl1.GraphicsContainer;
      
			//Get the MapFrame
			IMapFrame mapFrame = (IMapFrame) graphicsContainer.FindFrame(axPageLayoutControl1.ActiveView.FocusMap);
			if (mapFrame == null) return;
			
			//Create a legend
			UID uID = new UIDClass();
			uID.Value = "esriCarto.Legend";

			//Create a MapSurroundFrame from the MapFrame
			IMapSurroundFrame mapSurroundFrame = mapFrame.CreateSurroundFrame(uID, null);      
			if (mapSurroundFrame == null) return;				
			if (mapSurroundFrame.MapSurround == null) return;
			//Set the name 
			mapSurroundFrame.MapSurround.Name = "Legend";
        
			//Envelope for the legend
			IEnvelope envelope = new EnvelopeClass();
			envelope.PutCoords(1, 1, 3.4, 2.4);

			//Set the geometry of the MapSurroundFrame 
			IElement element = (IElement) mapSurroundFrame;
			element.Geometry = envelope; 
        
			//Add the legend to the PageLayout
			axPageLayoutControl1.AddElement(element, Type.Missing, Type.Missing, "Legend", 0);
        
			//Refresh the PageLayoutControl
			axPageLayoutControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);

			//disable/enable buttons
			cmdDraw.Enabled = false;
			cmdDelete.Enabled = true;
			cmdChangeArea.Enabled = true;
			cmdChangeLine.Enabled = true;
		}

		private void button2_Click(object sender, System.EventArgs e)
		{
			//Find the legend 
			IElement element = axPageLayoutControl1.FindElementByName("Legend", 1);

			if(element != null)
			{
				//Delete the legend
				IGraphicsContainer graphicsContainer = axPageLayoutControl1.GraphicsContainer;
				graphicsContainer.DeleteElement(element); 
				//Refresh the display
				axPageLayoutControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
		   
				//enable/disable buttons
				cmdDraw.Enabled = true;
				cmdDelete.Enabled = false;
				cmdChangeArea.Enabled = false;
				cmdChangeLine.Enabled = false;
			}
    }

		private void button3_Click(object sender, System.EventArgs e)
		{
			//create the form with the SymbologyControl
			Form2 symbolForm = new Form2();
		      
			//Get the IStyleGalleryItem that has been selected in the SymbologyControl
			IStyleGalleryItem styleGalleryItem = symbolForm.GetItem(esriSymbologyStyleClass.esriStyleClassAreaPatches);

			//release the form
			symbolForm.Dispose();
			if(styleGalleryItem == null) return;
		      
			//Find the legend
			IElement element = axPageLayoutControl1.FindElementByName("Legend", 1);
			if (element == null) return;
			
			//Get the IMapSurroundFrame
			IMapSurroundFrame mapSurroundFrame = (IMapSurroundFrame) element;
			if (mapSurroundFrame == null) return;

			//If a legend exists change the default area patch
			ILegend legend = (ILegend) mapSurroundFrame.MapSurround;
			legend.Format.DefaultAreaPatch = (IAreaPatch) styleGalleryItem.Item;
			
			//Update the legend
			legend.Refresh();
			//Refresh the display
			axPageLayoutControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
		}

		private void button4_Click(object sender, System.EventArgs e)
		{
			//create the form with the SymbologyControl
			Form2 symbolForm = new Form2();
		      
			//Get the IStyleGalleryItem that has been selected in the SymbologyControl
			IStyleGalleryItem styleGalleryItem = symbolForm.GetItem(esriSymbologyStyleClass.esriStyleClassLinePatches);

			//release the form
			symbolForm.Dispose();
			if(styleGalleryItem == null) return;
		      
			//Find the legend
			IElement element = axPageLayoutControl1.FindElementByName("Legend", 1);
			if (element == null) return;
			
			//Get the IMapSurroundFrame
			IMapSurroundFrame mapSurroundFrame = (IMapSurroundFrame) element;
			if (mapSurroundFrame == null) return;

			//If a legend exists change the default area patch
			ILegend legend = (ILegend) mapSurroundFrame.MapSurround;
			legend.Format.DefaultLinePatch = (ILinePatch) styleGalleryItem.Item;
			
			//Update the legend
			legend.Refresh();
			//Refresh the display
			axPageLayoutControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
		}
	}
}
