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
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS;

namespace ShareCommandPool
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class ShareCommandPool : System.Windows.Forms.Form
	{
		public System.Windows.Forms.CheckBox chkShare;
		public System.Windows.Forms.Label Label7;
		public System.Windows.Forms.Label Label6;
		public System.Windows.Forms.Label Label3;
		public System.Windows.Forms.Label Label1;
		public System.Windows.Forms.ListBox lstCommandPool2;
		public System.Windows.Forms.ListBox lstCommandPool1;
		public System.Windows.Forms.Label Label4;
		public System.Windows.Forms.Label Label2;
		private ICommandPool m_CommandPool1;
		private ICommandPool m_CommandPool2;
		private ESRI.ArcGIS.Controls.AxToolbarControl axToolbarControl1;
		private ESRI.ArcGIS.Controls.AxToolbarControl axToolbarControl2;
		private ESRI.ArcGIS.Controls.AxMapControl axMapControl1;
        private AxLicenseControl axLicenseControl1;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ShareCommandPool()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
            //Release COM objects and shutdown
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShareCommandPool));
            this.chkShare = new System.Windows.Forms.CheckBox();
            this.Label7 = new System.Windows.Forms.Label();
            this.Label6 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.lstCommandPool2 = new System.Windows.Forms.ListBox();
            this.lstCommandPool1 = new System.Windows.Forms.ListBox();
            this.Label4 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.axToolbarControl1 = new ESRI.ArcGIS.Controls.AxToolbarControl();
            this.axToolbarControl2 = new ESRI.ArcGIS.Controls.AxToolbarControl();
            this.axMapControl1 = new ESRI.ArcGIS.Controls.AxMapControl();
            this.axLicenseControl1 = new ESRI.ArcGIS.Controls.AxLicenseControl();
            ((System.ComponentModel.ISupportInitialize)(this.axToolbarControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axToolbarControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // chkShare
            // 
            this.chkShare.BackColor = System.Drawing.SystemColors.Control;
            this.chkShare.Cursor = System.Windows.Forms.Cursors.Default;
            this.chkShare.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkShare.ForeColor = System.Drawing.SystemColors.ControlText;
            this.chkShare.Location = new System.Drawing.Point(416, 24);
            this.chkShare.Name = "chkShare";
            this.chkShare.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.chkShare.Size = new System.Drawing.Size(129, 25);
            this.chkShare.TabIndex = 11;
            this.chkShare.Text = "Share CommandPool";
            this.chkShare.UseVisualStyleBackColor = false;
            this.chkShare.Click += new System.EventHandler(this.chkShare_Click);
            // 
            // Label7
            // 
            this.Label7.BackColor = System.Drawing.SystemColors.Control;
            this.Label7.Cursor = System.Windows.Forms.Cursors.Default;
            this.Label7.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label7.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Label7.Location = new System.Drawing.Point(408, 112);
            this.Label7.Name = "Label7";
            this.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Label7.Size = new System.Drawing.Size(193, 49);
            this.Label7.TabIndex = 12;
            this.Label7.Text = "2) Select a ZoomIn, ZoomOut or Pan tool. Notice that only one tool is depressed. " +
                "";
            // 
            // Label6
            // 
            this.Label6.BackColor = System.Drawing.SystemColors.Control;
            this.Label6.Cursor = System.Windows.Forms.Cursors.Default;
            this.Label6.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label6.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Label6.Location = new System.Drawing.Point(408, 80);
            this.Label6.Name = "Label6";
            this.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Label6.Size = new System.Drawing.Size(193, 33);
            this.Label6.TabIndex = 10;
            this.Label6.Text = "1) Browse to a map document to load into the MapControl.";
            // 
            // Label3
            // 
            this.Label3.BackColor = System.Drawing.SystemColors.Control;
            this.Label3.Cursor = System.Windows.Forms.Cursors.Default;
            this.Label3.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Label3.Location = new System.Drawing.Point(408, 160);
            this.Label3.Name = "Label3";
            this.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Label3.Size = new System.Drawing.Size(193, 49);
            this.Label3.TabIndex = 9;
            this.Label3.Text = "3) Share the same CommandPool between both ToolbarControls. Notice that the Usage" +
                "Count changes.";
            // 
            // Label1
            // 
            this.Label1.BackColor = System.Drawing.SystemColors.Control;
            this.Label1.Cursor = System.Windows.Forms.Cursors.Default;
            this.Label1.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Label1.Location = new System.Drawing.Point(408, 208);
            this.Label1.Name = "Label1";
            this.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Label1.Size = new System.Drawing.Size(193, 57);
            this.Label1.TabIndex = 8;
            this.Label1.Text = "4) Select a ZoomIn, ZoomOut or Pan tool. Notice that the same tool on both Toolba" +
                "rControls becomes depressed.";
            // 
            // lstCommandPool2
            // 
            this.lstCommandPool2.BackColor = System.Drawing.SystemColors.Window;
            this.lstCommandPool2.Cursor = System.Windows.Forms.Cursors.Default;
            this.lstCommandPool2.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstCommandPool2.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lstCommandPool2.ItemHeight = 14;
            this.lstCommandPool2.Location = new System.Drawing.Point(304, 296);
            this.lstCommandPool2.Name = "lstCommandPool2";
            this.lstCommandPool2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lstCommandPool2.Size = new System.Drawing.Size(281, 88);
            this.lstCommandPool2.TabIndex = 15;
            // 
            // lstCommandPool1
            // 
            this.lstCommandPool1.BackColor = System.Drawing.SystemColors.Window;
            this.lstCommandPool1.Cursor = System.Windows.Forms.Cursors.Default;
            this.lstCommandPool1.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstCommandPool1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lstCommandPool1.ItemHeight = 14;
            this.lstCommandPool1.Location = new System.Drawing.Point(8, 296);
            this.lstCommandPool1.Name = "lstCommandPool1";
            this.lstCommandPool1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lstCommandPool1.Size = new System.Drawing.Size(281, 88);
            this.lstCommandPool1.TabIndex = 14;
            // 
            // Label4
            // 
            this.Label4.BackColor = System.Drawing.SystemColors.Control;
            this.Label4.Cursor = System.Windows.Forms.Cursors.Default;
            this.Label4.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Label4.Location = new System.Drawing.Point(304, 280);
            this.Label4.Name = "Label4";
            this.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Label4.Size = new System.Drawing.Size(193, 17);
            this.Label4.TabIndex = 17;
            this.Label4.Text = "CommandPool2";
            // 
            // Label2
            // 
            this.Label2.BackColor = System.Drawing.SystemColors.Control;
            this.Label2.Cursor = System.Windows.Forms.Cursors.Default;
            this.Label2.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Label2.Location = new System.Drawing.Point(8, 280);
            this.Label2.Name = "Label2";
            this.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Label2.Size = new System.Drawing.Size(193, 17);
            this.Label2.TabIndex = 16;
            this.Label2.Text = "CommandPool1";
            // 
            // axToolbarControl1
            // 
            this.axToolbarControl1.Location = new System.Drawing.Point(8, 8);
            this.axToolbarControl1.Name = "axToolbarControl1";
            this.axToolbarControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axToolbarControl1.OcxState")));
            this.axToolbarControl1.Size = new System.Drawing.Size(392, 28);
            this.axToolbarControl1.TabIndex = 18;
            // 
            // axToolbarControl2
            // 
            this.axToolbarControl2.Location = new System.Drawing.Point(8, 40);
            this.axToolbarControl2.Name = "axToolbarControl2";
            this.axToolbarControl2.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axToolbarControl2.OcxState")));
            this.axToolbarControl2.Size = new System.Drawing.Size(392, 28);
            this.axToolbarControl2.TabIndex = 19;
            // 
            // axMapControl1
            // 
            this.axMapControl1.Location = new System.Drawing.Point(8, 72);
            this.axMapControl1.Name = "axMapControl1";
            this.axMapControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axMapControl1.OcxState")));
            this.axMapControl1.Size = new System.Drawing.Size(392, 200);
            this.axMapControl1.TabIndex = 20;
            // 
            // axLicenseControl1
            // 
            this.axLicenseControl1.Enabled = true;
            this.axLicenseControl1.Location = new System.Drawing.Point(556, 258);
            this.axLicenseControl1.Name = "axLicenseControl1";
            this.axLicenseControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axLicenseControl1.OcxState")));
            this.axLicenseControl1.Size = new System.Drawing.Size(32, 32);
            this.axLicenseControl1.TabIndex = 21;
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(600, 398);
            this.Controls.Add(this.axLicenseControl1);
            this.Controls.Add(this.axMapControl1);
            this.Controls.Add(this.axToolbarControl2);
            this.Controls.Add(this.axToolbarControl1);
            this.Controls.Add(this.lstCommandPool2);
            this.Controls.Add(this.lstCommandPool1);
            this.Controls.Add(this.Label4);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.chkShare);
            this.Controls.Add(this.Label7);
            this.Controls.Add(this.Label6);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.Label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.axToolbarControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axToolbarControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
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
			Application.Run(new ShareCommandPool());
		}

		private void Form1_Load(object sender, System.EventArgs e)
		{

		    //Set the Buddy property
            axToolbarControl1.SetBuddyControl(axMapControl1);
            axToolbarControl2.SetBuddyControl(axMapControl1);

            //Add items to the ToolbarControls
            axToolbarControl1.AddItem("esriControls.ControlsOpenDocCommand", -1, -1, false, -1, esriCommandStyles.esriCommandStyleIconOnly);
            axToolbarControl1.AddItem("esriControls.ControlsMapZoomInTool", -1, -1, false, -1, esriCommandStyles.esriCommandStyleIconAndText);
            axToolbarControl1.AddItem("esriControls.ControlsMapZoomOutTool", -1, -1, false, -1, esriCommandStyles.esriCommandStyleIconAndText);
            axToolbarControl1.AddItem("esriControls.ControlsMapPanTool", -1, -1, false, -1, esriCommandStyles.esriCommandStyleIconAndText);

            axToolbarControl2.AddItem("esriControls.ControlsMapFullExtentCommand", -1, -1, false, -1, esriCommandStyles.esriCommandStyleIconOnly);
            axToolbarControl2.AddItem("esriControls.ControlsMapZoomInTool", -1, -1, false, -1, esriCommandStyles.esriCommandStyleIconAndText);
            axToolbarControl2.AddItem("esriControls.ControlsMapZoomOutTool", -1, -1, false, -1, esriCommandStyles.esriCommandStyleIconAndText);
            axToolbarControl2.AddItem("esriControls.ControlsMapPanTool", -1, -1, false, -1, esriCommandStyles.esriCommandStyleIconAndText);

            //Get the CommandPool of ToolbarControl's
            m_CommandPool1 = axToolbarControl1.CommandPool;
            m_CommandPool2 = axToolbarControl2.CommandPool;

            UpdateUsageCount();
		}

		private void UpdateUsageCount()
		{
			ICommand pCommand;
			//Clear list boxes
			lstCommandPool1.Items.Clear();
			lstCommandPool2.Items.Clear();

			lstCommandPool1.Items.Add("UsageCount" + "\x9" +  "Name");
			//Loop through each command in CommandPool1 to get its UsageCount
			for (int i = 0; i <= m_CommandPool1.Count - 1; i++)
			{
				pCommand = m_CommandPool1.get_Command(i);
				lstCommandPool1.Items.Add(m_CommandPool1.get_UsageCount(pCommand) + "\x9" + pCommand.Name);
			}

			lstCommandPool2.Items.Add("UsageCount" + "\x9" +  "Name");
			//Loop through each command in CommandPool2 to get its UsageCount
			for (int i = 0; i <= m_CommandPool2.Count - 1; i++)
			{
				pCommand = m_CommandPool2.get_Command(i);
				lstCommandPool2.Items.Add(m_CommandPool2.get_UsageCount(pCommand) + "\x9" +  pCommand.Name);
			}
		}
       

		private void chkShare_Click(object sender, System.EventArgs e)
		{
			if (chkShare.CheckState == CheckState.Checked)
			{
				//Share the same CommandPool between both ToolbarControl's
				axToolbarControl2.CommandPool = m_CommandPool1;
			}
			else
			{
				//Do not share the same CommandPool between both ToolbarControl's
				axToolbarControl2.CommandPool = m_CommandPool2;
			}
			UpdateUsageCount();
		}
	
	}
}
