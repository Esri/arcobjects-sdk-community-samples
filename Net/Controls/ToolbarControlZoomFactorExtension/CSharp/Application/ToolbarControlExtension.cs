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
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS;
using Microsoft.Win32;


namespace ToolbarControlExtension
{
	public class Form1 : System.Windows.Forms.Form
	{
		private System.ComponentModel.Container components = null;
		public System.Windows.Forms.CheckBox chkExtension;
		public System.Windows.Forms.Label Label3;
		public System.Windows.Forms.Label Label1;
		public System.Windows.Forms.Label Label2;
		private ESRI.ArcGIS.Controls.AxToolbarControl axToolbarControl1;
		private ESRI.ArcGIS.Controls.AxMapControl axMapControl1;
		private ESRI.ArcGIS.Controls.AxLicenseControl axLicenseControl1;
		private IExtensionManagerAdmin m_ExtensionManagerAdmin;

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

		protected override void Dispose( bool disposing )
		{

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.chkExtension = new System.Windows.Forms.CheckBox();
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
            // chkExtension
            // 
            this.chkExtension.BackColor = System.Drawing.SystemColors.Control;
            this.chkExtension.Cursor = System.Windows.Forms.Cursors.Default;
            this.chkExtension.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkExtension.ForeColor = System.Drawing.SystemColors.ControlText;
            this.chkExtension.Location = new System.Drawing.Point(592, 8);
            this.chkExtension.Name = "chkExtension";
            this.chkExtension.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.chkExtension.Size = new System.Drawing.Size(113, 25);
            this.chkExtension.TabIndex = 6;
            this.chkExtension.Text = "Enable Extension";
            this.chkExtension.UseVisualStyleBackColor = false;
            this.chkExtension.CheckedChanged += new System.EventHandler(this.chkExtension_CheckedChanged);
            // 
            // Label3
            // 
            this.Label3.BackColor = System.Drawing.SystemColors.Control;
            this.Label3.Cursor = System.Windows.Forms.Cursors.Default;
            this.Label3.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Label3.Location = new System.Drawing.Point(592, 136);
            this.Label3.Name = "Label3";
            this.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Label3.Size = new System.Drawing.Size(137, 65);
            this.Label3.TabIndex = 9;
            this.Label3.Text = "Enable the extension and navigate around the data using the commands from the ext" +
                "ension.";
            // 
            // Label1
            // 
            this.Label1.BackColor = System.Drawing.SystemColors.Control;
            this.Label1.Cursor = System.Windows.Forms.Cursors.Default;
            this.Label1.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Label1.Location = new System.Drawing.Point(592, 88);
            this.Label1.Name = "Label1";
            this.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Label1.Size = new System.Drawing.Size(137, 41);
            this.Label1.TabIndex = 8;
            this.Label1.Text = "Navigate around the data using the commands on the ToolbarControl.";
            // 
            // Label2
            // 
            this.Label2.BackColor = System.Drawing.SystemColors.Control;
            this.Label2.Cursor = System.Windows.Forms.Cursors.Default;
            this.Label2.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Label2.Location = new System.Drawing.Point(592, 40);
            this.Label2.Name = "Label2";
            this.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Label2.Size = new System.Drawing.Size(137, 40);
            this.Label2.TabIndex = 7;
            this.Label2.Text = "Browse to a map document to load into the MapControl.";
            // 
            // axToolbarControl1
            // 
            this.axToolbarControl1.Location = new System.Drawing.Point(8, 8);
            this.axToolbarControl1.Name = "axToolbarControl1";
            this.axToolbarControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axToolbarControl1.OcxState")));
            this.axToolbarControl1.Size = new System.Drawing.Size(576, 28);
            this.axToolbarControl1.TabIndex = 10;
            // 
            // axMapControl1
            // 
            this.axMapControl1.Location = new System.Drawing.Point(8, 40);
            this.axMapControl1.Name = "axMapControl1";
            this.axMapControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axMapControl1.OcxState")));
            this.axMapControl1.Size = new System.Drawing.Size(576, 432);
            this.axMapControl1.TabIndex = 11;
            // 
            // axLicenseControl1
            // 
            this.axLicenseControl1.Enabled = true;
            this.axLicenseControl1.Location = new System.Drawing.Point(595, 204);
            this.axLicenseControl1.Name = "axLicenseControl1";
            this.axLicenseControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axLicenseControl1.OcxState")));
            this.axLicenseControl1.Size = new System.Drawing.Size(32, 32);
            this.axLicenseControl1.TabIndex = 12;
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(736, 478);
            this.Controls.Add(this.axLicenseControl1);
            this.Controls.Add(this.axMapControl1);
            this.Controls.Add(this.axToolbarControl1);
            this.Controls.Add(this.chkExtension);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.Label2);
            this.Name = "Form1";
            this.Text = "MenuTracking";
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
			axToolbarControl1.SetBuddyControl(axMapControl1.Object);

			//Add control command items to the ToolbarControl
			axToolbarControl1.AddItem("esriControls.ControlsOpenDocCommand",-1,-1,false,0,esriCommandStyles.esriCommandStyleIconOnly);
			axToolbarControl1.AddItem("esriControls.ControlsMapZoomInTool",-1,-1,true,0,esriCommandStyles.esriCommandStyleIconOnly);
			axToolbarControl1.AddItem("esriControls.ControlsMapZoomOutTool",-1,-1,false,0,esriCommandStyles.esriCommandStyleIconOnly);
			axToolbarControl1.AddItem("esriControls.ControlsMapPanTool",-1,-1,false,0,esriCommandStyles.esriCommandStyleIconOnly);
			//Add extension command items to the ToolbarControl
			axToolbarControl1.AddItem("ZoomFactorExtensionCSharp.SetZoomFactor",-1,-1,true,0,esriCommandStyles.esriCommandStyleIconAndText);
			axToolbarControl1.AddItem("ZoomFactorExtensionCSharp.ZoomIn",-1,-1,true,0,esriCommandStyles.esriCommandStyleIconAndText);
			axToolbarControl1.AddItem("ZoomFactorExtensionCSharp.ZoomOut",-1,-1,true,0,esriCommandStyles.esriCommandStyleIconAndText);

			//Get the extension manager admin
			m_ExtensionManagerAdmin = (IExtensionManagerAdmin) new ExtensionManagerClass();

			//Add the extension to the extension manager
			UID uID = new UIDClass();
			uID.Value = "ZoomFactorExtensionCSharp.ZoomExtension";
			object obj = new object();
			m_ExtensionManagerAdmin.AddExtension(uID, ref obj);
		}

		private void chkExtension_CheckedChanged(object sender, System.EventArgs e)
		{
			//Get the extension manager
			IExtensionManager extensionManager = (IExtensionManager) m_ExtensionManagerAdmin;
			//Get the extension from the extension manager
			IExtensionConfig extensionConfig = (IExtensionConfig) extensionManager.FindExtension("Zoom Factor Extension");
			//Set the enabled state
			if (chkExtension.CheckState == CheckState.Checked) extensionConfig.State = esriExtensionState.esriESEnabled;
			else extensionConfig.State = esriExtensionState.esriESDisabled;
		}

	}
}
