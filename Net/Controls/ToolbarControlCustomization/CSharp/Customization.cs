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


namespace Customization
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Customization : System.Windows.Forms.Form
	{
		public System.Windows.Forms.CheckBox chkCustomization;
		public System.Windows.Forms.Label Label1;
		public System.Windows.Forms.Label Label3;
		public System.Windows.Forms.Label Label2;
		private ESRI.ArcGIS.Controls.AxToolbarControl axToolbarControl1;
		private ESRI.ArcGIS.Controls.AxMapControl axMapControl1;
		private ESRI.ArcGIS.Controls.AxLicenseControl axLicenseControl1;
        public Label Label4;

        private ICustomizeDialog m_CustomizeDialog = new CustomizeDialogClass();		//The CustomizeDialog used by the ToolbarControl
        private ICustomizeDialogEvents_OnStartDialogEventHandler startDialogE;			//The CustomizeDialog start event
        private ICustomizeDialogEvents_OnCloseDialogEventHandler closeDialogE;	

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Customization()
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Customization));
            this.chkCustomization = new System.Windows.Forms.CheckBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.axToolbarControl1 = new ESRI.ArcGIS.Controls.AxToolbarControl();
            this.axMapControl1 = new ESRI.ArcGIS.Controls.AxMapControl();
            this.axLicenseControl1 = new ESRI.ArcGIS.Controls.AxLicenseControl();
            this.Label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.axToolbarControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // chkCustomization
            // 
            this.chkCustomization.BackColor = System.Drawing.SystemColors.Control;
            this.chkCustomization.Cursor = System.Windows.Forms.Cursors.Default;
            this.chkCustomization.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkCustomization.ForeColor = System.Drawing.SystemColors.ControlText;
            this.chkCustomization.Location = new System.Drawing.Point(416, 48);
            this.chkCustomization.Name = "chkCustomization";
            this.chkCustomization.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.chkCustomization.Size = new System.Drawing.Size(121, 17);
            this.chkCustomization.TabIndex = 6;
            this.chkCustomization.Text = "Customize";
            this.chkCustomization.UseVisualStyleBackColor = false;
            this.chkCustomization.Click += new System.EventHandler(this.chkCustomization_Click);
            // 
            // Label1
            // 
            this.Label1.BackColor = System.Drawing.SystemColors.Control;
            this.Label1.Cursor = System.Windows.Forms.Cursors.Default;
            this.Label1.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Label1.Location = new System.Drawing.Point(416, 144);
            this.Label1.Name = "Label1";
            this.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Label1.Size = new System.Drawing.Size(185, 73);
            this.Label1.TabIndex = 9;
            this.Label1.Text = "To delete an item, either select it with the left mouse button and drag it off th" +
                "e ToolbarControl or select it with the right mouse button and choose delete from" +
                " the customize menu.";
            // 
            // Label3
            // 
            this.Label3.BackColor = System.Drawing.SystemColors.Control;
            this.Label3.Cursor = System.Windows.Forms.Cursors.Default;
            this.Label3.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Label3.Location = new System.Drawing.Point(416, 224);
            this.Label3.Name = "Label3";
            this.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Label3.Size = new System.Drawing.Size(185, 57);
            this.Label3.TabIndex = 8;
            this.Label3.Text = "To change the group, group spacing or style of an item, select it with the right " +
                "mouse button to display the customize menu.";
            // 
            // Label2
            // 
            this.Label2.BackColor = System.Drawing.SystemColors.Control;
            this.Label2.Cursor = System.Windows.Forms.Cursors.Default;
            this.Label2.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Label2.Location = new System.Drawing.Point(416, 80);
            this.Label2.Name = "Label2";
            this.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Label2.Size = new System.Drawing.Size(185, 57);
            this.Label2.TabIndex = 7;
            this.Label2.Text = "To move an item, select it with the left mouse button and drag and drop it to the" +
                " location indicated by the black vertical bar. ";
            // 
            // axToolbarControl1
            // 
            this.axToolbarControl1.Location = new System.Drawing.Point(8, 8);
            this.axToolbarControl1.Name = "axToolbarControl1";
            this.axToolbarControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axToolbarControl1.OcxState")));
            this.axToolbarControl1.Size = new System.Drawing.Size(592, 28);
            this.axToolbarControl1.TabIndex = 10;
            // 
            // axMapControl1
            // 
            this.axMapControl1.Location = new System.Drawing.Point(8, 40);
            this.axMapControl1.Name = "axMapControl1";
            this.axMapControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axMapControl1.OcxState")));
            this.axMapControl1.Size = new System.Drawing.Size(400, 389);
            this.axMapControl1.TabIndex = 11;
            // 
            // axLicenseControl1
            // 
            this.axLicenseControl1.Enabled = true;
            this.axLicenseControl1.Location = new System.Drawing.Point(564, 48);
            this.axLicenseControl1.Name = "axLicenseControl1";
            this.axLicenseControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axLicenseControl1.OcxState")));
            this.axLicenseControl1.Size = new System.Drawing.Size(32, 32);
            this.axLicenseControl1.TabIndex = 12;
            // 
            // Label4
            // 
            this.Label4.BackColor = System.Drawing.SystemColors.Control;
            this.Label4.Cursor = System.Windows.Forms.Cursors.Default;
            this.Label4.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Label4.Location = new System.Drawing.Point(414, 283);
            this.Label4.Name = "Label4";
            this.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Label4.Size = new System.Drawing.Size(185, 93);
            this.Label4.TabIndex = 13;
            this.Label4.Text = "To add additional command, menu  and palette items either double click on them wi" +
                "thin the customize dialog or drag and drop them from the customize dialog onto th" +
                "e ToolbarControl.";
            // 
            // Customization
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(608, 441);
            this.Controls.Add(this.Label4);
            this.Controls.Add(this.axLicenseControl1);
            this.Controls.Add(this.axMapControl1);
            this.Controls.Add(this.axToolbarControl1);
            this.Controls.Add(this.chkCustomization);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.Label2);
            this.Name = "Customization";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.axToolbarControl1)).EndInit();
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

			Application.Run(new Customization());
		}

		private void Form1_Load(object sender, System.EventArgs e)
		{
			//Set the Buddy
			axToolbarControl1.SetBuddyControl(axMapControl1);

			//Create UID's and add new items to the ToolBarControl
			UID uID = new UIDClass();
			uID.Value = "esriControls.ControlsOpenDocCommand";
			axToolbarControl1.AddItem(uID, 0, -1, false, -1, esriCommandStyles.esriCommandStyleIconOnly);
			uID.Value = "esriControls.ControlsMapZoomInTool";
			axToolbarControl1.AddItem(uID, -1 , -1, true, -1, esriCommandStyles.esriCommandStyleIconAndText);
			uID.Value = "esriControls.ControlsMapZoomOutTool";
			axToolbarControl1.AddItem(uID, -1, -1, false, -1, esriCommandStyles.esriCommandStyleIconAndText);
			uID.Value = "esriControls.ControlsMapPanTool";
			axToolbarControl1.AddItem(uID, -1, -1, false, -1, esriCommandStyles.esriCommandStyleIconAndText);
			uID.Value = "esriControls.ControlsMapFullExtentCommand";
			axToolbarControl1.AddItem(uID, -1, -1, false, -1, esriCommandStyles.esriCommandStyleIconAndText);
			uID.Value = "esriControls.ControlsMapZoomToLastExtentBackCommand";
			axToolbarControl1.AddItem(uID, -1, -1, true, 20, esriCommandStyles.esriCommandStyleTextOnly);
			uID.Value = "esriControls.ControlsMapZoomToLastExtentForwardCommand";
			axToolbarControl1.AddItem(uID, -1, -1, false, -1, esriCommandStyles.esriCommandStyleTextOnly);
			
            //Create a new customize dialog
            m_CustomizeDialog = new CustomizeDialogClass();
            //Set the customize dialog events 
			startDialogE = new ICustomizeDialogEvents_OnStartDialogEventHandler(OnStartDialog);
			((ICustomizeDialogEvents_Event)m_CustomizeDialog).OnStartDialog += startDialogE;
			closeDialogE = new ICustomizeDialogEvents_OnCloseDialogEventHandler(OnCloseDialog);
			((ICustomizeDialogEvents_Event)m_CustomizeDialog).OnCloseDialog += closeDialogE;
            m_CustomizeDialog.SetDoubleClickDestination(axToolbarControl1);
            chkCustomization.CheckState = CheckState.Unchecked;
		}

		private void chkCustomization_Click(object sender, System.EventArgs e)
		{
            //Show or hide the customize dialog
            if (chkCustomization.Checked == false)
                m_CustomizeDialog.CloseDialog();
            else
                m_CustomizeDialog.StartDialog(axMapControl1.hWnd);
		}

        private void OnStartDialog()
        {
            //Put the ToolbarControl into customize mode
            axToolbarControl1.Customize = true;
        }

        private void OnCloseDialog()
        {
            //Take the ToolbarControl out of customize mode
            axToolbarControl1.Customize = false;
            chkCustomization.Checked = false;
        }
	}
}
