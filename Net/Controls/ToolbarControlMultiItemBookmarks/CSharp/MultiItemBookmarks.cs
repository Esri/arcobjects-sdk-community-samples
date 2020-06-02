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
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS;


namespace MultiItemBookmarks
{

	public class Form1 : System.Windows.Forms.Form
	{
		public System.Windows.Forms.Label Label4;
		public System.Windows.Forms.Label Label3;
		public System.Windows.Forms.Label Label1;
		public System.Windows.Forms.Label Label2;
		private ESRI.ArcGIS.Controls.AxToolbarControl axToolbarControl1;
		private ESRI.ArcGIS.Controls.AxMapControl axMapControl1;
		private ESRI.ArcGIS.Controls.AxLicenseControl axLicenseControl1;

		private System.ComponentModel.Container components = null;

		public Form1()
		{

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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Form1));
			this.Label4 = new System.Windows.Forms.Label();
			this.Label3 = new System.Windows.Forms.Label();
			this.Label1 = new System.Windows.Forms.Label();
			this.Label2 = new System.Windows.Forms.Label();
			this.axToolbarControl1 = new ESRI.ArcGIS.Controls.AxToolbarControl();
			this.axMapControl1 = new ESRI.ArcGIS.Controls.AxMapControl();
			this.axLicenseControl1 = new ESRI.ArcGIS.Controls.AxLicenseControl();
			((System.ComponentModel.ISupportInitialize)(this.axToolbarControl1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.axMapControl1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).BeginInit();
			this.SuspendLayout();
			// 
			// Label4
			// 
			this.Label4.BackColor = System.Drawing.SystemColors.Control;
			this.Label4.Cursor = System.Windows.Forms.Cursors.Default;
			this.Label4.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Label4.ForeColor = System.Drawing.SystemColors.ControlText;
			this.Label4.Location = new System.Drawing.Point(456, 192);
			this.Label4.Name = "Label4";
			this.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Label4.Size = new System.Drawing.Size(137, 49);
			this.Label4.TabIndex = 8;
			this.Label4.Text = "Create a new bookmark based on the current extent of the focus map.";
			// 
			// Label3
			// 
			this.Label3.BackColor = System.Drawing.SystemColors.Control;
			this.Label3.Cursor = System.Windows.Forms.Cursors.Default;
			this.Label3.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Label3.ForeColor = System.Drawing.SystemColors.ControlText;
			this.Label3.Location = new System.Drawing.Point(456, 96);
			this.Label3.Name = "Label3";
			this.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Label3.Size = new System.Drawing.Size(137, 41);
			this.Label3.TabIndex = 7;
			this.Label3.Text = "Navigate around the data using the commands on the ToolbarControl.";
			// 
			// Label1
			// 
			this.Label1.BackColor = System.Drawing.SystemColors.Control;
			this.Label1.Cursor = System.Windows.Forms.Cursors.Default;
			this.Label1.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Label1.ForeColor = System.Drawing.SystemColors.ControlText;
			this.Label1.Location = new System.Drawing.Point(456, 144);
			this.Label1.Name = "Label1";
			this.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Label1.Size = new System.Drawing.Size(137, 49);
			this.Label1.TabIndex = 6;
			this.Label1.Text = "Zoom to any bookmarks listed in the spatial bookmark menu.";
			// 
			// Label2
			// 
			this.Label2.BackColor = System.Drawing.SystemColors.Control;
			this.Label2.Cursor = System.Windows.Forms.Cursors.Default;
			this.Label2.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Label2.ForeColor = System.Drawing.SystemColors.ControlText;
			this.Label2.Location = new System.Drawing.Point(456, 48);
			this.Label2.Name = "Label2";
			this.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Label2.Size = new System.Drawing.Size(137, 40);
			this.Label2.TabIndex = 5;
			this.Label2.Text = "Browse to a map document to load into the MapControl.";
			// 
			// axToolbarControl1
			// 
			this.axToolbarControl1.Location = new System.Drawing.Point(8, 8);
			this.axToolbarControl1.Name = "axToolbarControl1";
			this.axToolbarControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axToolbarControl1.OcxState")));
			this.axToolbarControl1.Size = new System.Drawing.Size(584, 28);
			this.axToolbarControl1.TabIndex = 9;
			// 
			// axMapControl1
			// 
			this.axMapControl1.Location = new System.Drawing.Point(8, 40);
			this.axMapControl1.Name = "axMapControl1";
			this.axMapControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axMapControl1.OcxState")));
			this.axMapControl1.Size = new System.Drawing.Size(440, 312);
			this.axMapControl1.TabIndex = 10;
			// 
			// axLicenseControl1
			// 
			this.axLicenseControl1.Enabled = true;
			this.axLicenseControl1.Location = new System.Drawing.Point(360, 264);
			this.axLicenseControl1.Name = "axLicenseControl1";
			this.axLicenseControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axLicenseControl1.OcxState")));
			this.axLicenseControl1.Size = new System.Drawing.Size(200, 50);
			this.axLicenseControl1.TabIndex = 11;
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(600, 358);
			this.Controls.Add(this.axLicenseControl1);
			this.Controls.Add(this.axMapControl1);
			this.Controls.Add(this.axToolbarControl1);
			this.Controls.Add(this.Label4);
			this.Controls.Add(this.Label3);
			this.Controls.Add(this.Label1);
			this.Controls.Add(this.Label2);
			this.Name = "Form1";
			this.Text = "Form1";
			this.Load += new System.EventHandler(this.Form1_Load);
			((System.ComponentModel.ISupportInitialize)(this.axToolbarControl1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.axMapControl1)).EndInit();
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
			axToolbarControl1.SetBuddyControl(axMapControl1);
			
			//Add command items to the ToolbarControl
			axToolbarControl1.AddItem("esriControls.ControlsOpenDocCommand", -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconAndText);
			axToolbarControl1.AddItem("esriControls.ControlsMapZoomInTool", -1, -1, true, 0, esriCommandStyles.esriCommandStyleIconAndText);
			axToolbarControl1.AddItem("esriControls.ControlsMapZoomOutTool", -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconAndText);
			axToolbarControl1.AddItem("esriControls.ControlsMapPanTool", -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconAndText);
			axToolbarControl1.AddItem("esriControls.ControlsMapFullExtentCommand",-1 , -1, false, 0, esriCommandStyles.esriCommandStyleIconAndText);
			
			//Create a new ToolbarMenu 
			IToolbarMenu2 toolbarMenu = new ToolbarMenuClass();
			
			//Set a caption
			toolbarMenu.Caption = "Spatial Bookmarks";
			//Add the custom command to the ToolbarMenu
			toolbarMenu.AddItem(new CreateBookmark(), -1, -1, false, esriCommandStyles.esriCommandStyleTextOnly);
			//Add the custom multiitem to the ToolbarMenu
			toolbarMenu.AddMultiItem(new SpatialBookmarks(), -1, true, esriCommandStyles.esriCommandStyleTextOnly);
			
			//Add the menu item to the ToolbarControl
			axToolbarControl1.AddItem(toolbarMenu, -1, -1, true, 0, esriCommandStyles.esriCommandStyleMenuBar);
			
		}
	}
}
