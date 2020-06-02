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

namespace Appearance
{

	public class Form1 : System.Windows.Forms.Form
	{
		internal System.Windows.Forms.CheckBox chkFillDirection;
		internal System.Windows.Forms.Button btnFadeColor;
		internal System.Windows.Forms.Button btnBackColor;
		internal System.Windows.Forms.CheckBox chkHiddenItems;
		internal System.Windows.Forms.CheckBox chkOrientation;
		private ESRI.ArcGIS.Controls.AxToolbarControl axToolbarControl1;
		private ESRI.ArcGIS.Controls.AxGlobeControl axGlobeControl1;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.chkFillDirection = new System.Windows.Forms.CheckBox();
            this.btnFadeColor = new System.Windows.Forms.Button();
            this.btnBackColor = new System.Windows.Forms.Button();
            this.chkHiddenItems = new System.Windows.Forms.CheckBox();
            this.chkOrientation = new System.Windows.Forms.CheckBox();
            this.axToolbarControl1 = new ESRI.ArcGIS.Controls.AxToolbarControl();
            this.axGlobeControl1 = new ESRI.ArcGIS.Controls.AxGlobeControl();
            this.axLicenseControl1 = new ESRI.ArcGIS.Controls.AxLicenseControl();
            ((System.ComponentModel.ISupportInitialize)(this.axToolbarControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axGlobeControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // chkFillDirection
            // 
            this.chkFillDirection.Location = new System.Drawing.Point(408, 168);
            this.chkFillDirection.Name = "chkFillDirection";
            this.chkFillDirection.Size = new System.Drawing.Size(144, 24);
            this.chkFillDirection.TabIndex = 13;
            this.chkFillDirection.Text = "Vertical Fill Direction";
            this.chkFillDirection.CheckedChanged += new System.EventHandler(this.chkFillDirection_CheckedChanged);
            // 
            // btnFadeColor
            // 
            this.btnFadeColor.Location = new System.Drawing.Point(408, 136);
            this.btnFadeColor.Name = "btnFadeColor";
            this.btnFadeColor.Size = new System.Drawing.Size(112, 23);
            this.btnFadeColor.TabIndex = 12;
            this.btnFadeColor.Text = "Fade Color";
            this.btnFadeColor.Click += new System.EventHandler(this.btnFadeColor_Click);
            // 
            // btnBackColor
            // 
            this.btnBackColor.Location = new System.Drawing.Point(408, 104);
            this.btnBackColor.Name = "btnBackColor";
            this.btnBackColor.Size = new System.Drawing.Size(112, 23);
            this.btnBackColor.TabIndex = 11;
            this.btnBackColor.Text = "Back Color";
            this.btnBackColor.Click += new System.EventHandler(this.btnBackColor_Click);
            // 
            // chkHiddenItems
            // 
            this.chkHiddenItems.Location = new System.Drawing.Point(408, 72);
            this.chkHiddenItems.Name = "chkHiddenItems";
            this.chkHiddenItems.Size = new System.Drawing.Size(152, 16);
            this.chkHiddenItems.TabIndex = 10;
            this.chkHiddenItems.Text = "Show Hidden Items";
            this.chkHiddenItems.CheckedChanged += new System.EventHandler(this.chkHiddenItems_CheckedChanged);
            // 
            // chkOrientation
            // 
            this.chkOrientation.Location = new System.Drawing.Point(408, 48);
            this.chkOrientation.Name = "chkOrientation";
            this.chkOrientation.Size = new System.Drawing.Size(152, 16);
            this.chkOrientation.TabIndex = 9;
            this.chkOrientation.Text = "Vertical Orientation";
            this.chkOrientation.CheckedChanged += new System.EventHandler(this.chkOrientation_CheckedChanged);
            // 
            // axToolbarControl1
            // 
            this.axToolbarControl1.Location = new System.Drawing.Point(8, 8);
            this.axToolbarControl1.Name = "axToolbarControl1";
            this.axToolbarControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axToolbarControl1.OcxState")));
            this.axToolbarControl1.Size = new System.Drawing.Size(490, 28);
            this.axToolbarControl1.TabIndex = 14;
            // 
            // axGlobeControl1
            // 
            this.axGlobeControl1.Location = new System.Drawing.Point(40, 40);
            this.axGlobeControl1.Name = "axGlobeControl1";
            this.axGlobeControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axGlobeControl1.OcxState")));
            this.axGlobeControl1.Size = new System.Drawing.Size(362, 391);
            this.axGlobeControl1.TabIndex = 15;
            // 
            // axLicenseControl1
            // 
            this.axLicenseControl1.Enabled = true;
            this.axLicenseControl1.Location = new System.Drawing.Point(408, 399);
            this.axLicenseControl1.Name = "axLicenseControl1";
            this.axLicenseControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axLicenseControl1.OcxState")));
            this.axLicenseControl1.Size = new System.Drawing.Size(32, 32);
            this.axLicenseControl1.TabIndex = 16;
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(536, 443);
            this.Controls.Add(this.axLicenseControl1);
            this.Controls.Add(this.axGlobeControl1);
            this.Controls.Add(this.axToolbarControl1);
            this.Controls.Add(this.chkFillDirection);
            this.Controls.Add(this.btnFadeColor);
            this.Controls.Add(this.btnBackColor);
            this.Controls.Add(this.chkHiddenItems);
            this.Controls.Add(this.chkOrientation);
            this.Name = "Form1";
            this.Text = "Appearance";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.axToolbarControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axGlobeControl1)).EndInit();
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

		private void chkOrientation_CheckedChanged(object sender, System.EventArgs e)
		{
		    if (chkOrientation.Checked == true) 
				axToolbarControl1.Orientation = esriToolbarOrientation.esriToolbarOrientationVertical;
			else
				axToolbarControl1.Orientation = esriToolbarOrientation.esriToolbarOrientationHorizontal;
		}

		private void chkHiddenItems_CheckedChanged(object sender, System.EventArgs e)
		{
			if (chkHiddenItems.Checked == true) 
				axToolbarControl1.ShowHiddenItems = true;
			else
				axToolbarControl1.ShowHiddenItems = false;
		}

		private void btnBackColor_Click(object sender, System.EventArgs e)
		{
			//Create new ColorDialog control
			ColorDialog colorDialog = new ColorDialog();
			//Show the ColorDialog and exit if user cancelled
			if (colorDialog.ShowDialog() == DialogResult.Cancel) return;

			//Get color from ColorDialog
			System.Drawing.Color color = colorDialog.Color;

            //The ToolbarControl host wrapper expects a SystemDrawingColor. The 
			//ToolbarControl type library wrapper expects an OLE_Color. The OLE_Color
			//is made up as follows:(Red) + (Green * 256) + (Blue * 256 * 256)		
			//IToolbarControl2 toolbarControl = (IToolbarControl2) axToolbarControl1.Object;
			//toolbarControl.BackColor = (color.R + (color.G * 256) + (color.B * 256 * 256));
			axToolbarControl1.BackColor = color;

			colorDialog = null;
		}

		private void btnFadeColor_Click(object sender, System.EventArgs e)
		{
			//Create new ColorDialog control
			ColorDialog colorDialog = new ColorDialog();
			//Show the ColorDialog and exit if user cancelled
			if (colorDialog.ShowDialog() == DialogResult.Cancel) return;

			//Get color from ColorDialog
			System.Drawing.Color color = colorDialog.Color;

			//The ToolbarControl host wrapper expects a SystemDrawingColor. The 
			//ToolbarControl type library wrapper expects an OLE_Color. The OLE_Color
			//is made up as follows:(Red) + (Green * 256) + (Blue * 256 * 256)		
			//IToolbarControl2 toolbarControl = (IToolbarControl2) axToolbarControl1.Object;
			//toolbarControl.FadeColor = (color.R + (color.G * 256) + (color.B * 256 * 256));
			axToolbarControl1.FadeColor = color;

			colorDialog = null;
		}

		private void chkFillDirection_CheckedChanged(object sender, System.EventArgs e)
		{
			if (chkFillDirection.Checked == true)
				axToolbarControl1.FillDirection = esriToolbarFillDirection.esriToolbarFillVertical;
			else
				axToolbarControl1.FillDirection = esriToolbarFillDirection.esriToolbarFillHorizontal;
		}

		private void Form1_Load(object sender, System.EventArgs e)
		{
			//Set buddy control
			axToolbarControl1.SetBuddyControl(axGlobeControl1);

			//Add new items to the ToolbarControl
			axToolbarControl1.AddItem("esriControls.ControlsGlobeOpenDocCommand", 0,-1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
			axToolbarControl1.AddToolbarDef("esriControls.ControlsGlobeGlobeToolbar", -1, true, 0, esriCommandStyles.esriCommandStyleIconOnly);
			axToolbarControl1.AddToolbarDef("esriControls.ControlsGlobeRotateToolbar", -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
		}
	}
}
