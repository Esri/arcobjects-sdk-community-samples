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
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS;


namespace ToolbarMenu
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		public System.Windows.Forms.Button cmdAddSubMenu;
		public System.Windows.Forms.Button cmdAddMenu;
		public System.Windows.Forms.Label Label4;
		public System.Windows.Forms.Label Label2;
		public System.Windows.Forms.Label Label3;
		public System.Windows.Forms.Label Label1;
		private ESRI.ArcGIS.Controls.AxToolbarControl axToolbarControl1;
		private ESRI.ArcGIS.Controls.AxMapControl axMapControl1;
		private ESRI.ArcGIS.Controls.AxLicenseControl axLicenseControl1;

        private IToolbarMenu m_navigationMenu = new ToolbarMenuClass();
        public Label Label5;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.cmdAddSubMenu = new System.Windows.Forms.Button();
            this.cmdAddMenu = new System.Windows.Forms.Button();
            this.Label4 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.axToolbarControl1 = new ESRI.ArcGIS.Controls.AxToolbarControl();
            this.axMapControl1 = new ESRI.ArcGIS.Controls.AxMapControl();
            this.axLicenseControl1 = new ESRI.ArcGIS.Controls.AxLicenseControl();
            this.Label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.axToolbarControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdAddSubMenu
            // 
            this.cmdAddSubMenu.BackColor = System.Drawing.SystemColors.Control;
            this.cmdAddSubMenu.Cursor = System.Windows.Forms.Cursors.Default;
            this.cmdAddSubMenu.Enabled = false;
            this.cmdAddSubMenu.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdAddSubMenu.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdAddSubMenu.Location = new System.Drawing.Point(464, 272);
            this.cmdAddSubMenu.Name = "cmdAddSubMenu";
            this.cmdAddSubMenu.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmdAddSubMenu.Size = new System.Drawing.Size(97, 33);
            this.cmdAddSubMenu.TabIndex = 12;
            this.cmdAddSubMenu.Text = "Add Sub Menu";
            this.cmdAddSubMenu.UseVisualStyleBackColor = false;
            this.cmdAddSubMenu.Click += new System.EventHandler(this.cmdAddSubMenu_Click);
            // 
            // cmdAddMenu
            // 
            this.cmdAddMenu.BackColor = System.Drawing.SystemColors.Control;
            this.cmdAddMenu.Cursor = System.Windows.Forms.Cursors.Default;
            this.cmdAddMenu.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdAddMenu.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdAddMenu.Location = new System.Drawing.Point(464, 176);
            this.cmdAddMenu.Name = "cmdAddMenu";
            this.cmdAddMenu.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmdAddMenu.Size = new System.Drawing.Size(97, 33);
            this.cmdAddMenu.TabIndex = 11;
            this.cmdAddMenu.Text = "Add Menu";
            this.cmdAddMenu.UseVisualStyleBackColor = false;
            this.cmdAddMenu.Click += new System.EventHandler(this.cmdAddMenu_Click);
            // 
            // Label4
            // 
            this.Label4.BackColor = System.Drawing.SystemColors.Control;
            this.Label4.Cursor = System.Windows.Forms.Cursors.Default;
            this.Label4.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Label4.Location = new System.Drawing.Point(440, 232);
            this.Label4.Name = "Label4";
            this.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Label4.Size = new System.Drawing.Size(137, 41);
            this.Label4.TabIndex = 13;
            this.Label4.Text = "Add a sub-menu to the Navigation menu.";
            // 
            // Label2
            // 
            this.Label2.BackColor = System.Drawing.SystemColors.Control;
            this.Label2.Cursor = System.Windows.Forms.Cursors.Default;
            this.Label2.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Label2.Location = new System.Drawing.Point(440, 40);
            this.Label2.Name = "Label2";
            this.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Label2.Size = new System.Drawing.Size(144, 33);
            this.Label2.TabIndex = 10;
            this.Label2.Text = "Browse to a map document to load into the MapControl.";
            // 
            // Label3
            // 
            this.Label3.BackColor = System.Drawing.SystemColors.Control;
            this.Label3.Cursor = System.Windows.Forms.Cursors.Default;
            this.Label3.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Label3.Location = new System.Drawing.Point(440, 136);
            this.Label3.Name = "Label3";
            this.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Label3.Size = new System.Drawing.Size(145, 33);
            this.Label3.TabIndex = 9;
            this.Label3.Text = "Add the Navigation menu onto the ToolbarControl.";
            // 
            // Label1
            // 
            this.Label1.BackColor = System.Drawing.SystemColors.Control;
            this.Label1.Cursor = System.Windows.Forms.Cursors.Default;
            this.Label1.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Label1.Location = new System.Drawing.Point(440, 80);
            this.Label1.Name = "Label1";
            this.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Label1.Size = new System.Drawing.Size(137, 41);
            this.Label1.TabIndex = 8;
            this.Label1.Text = "Navigate around the data using the commands on the ToolbarControl.";
            // 
            // axToolbarControl1
            // 
            this.axToolbarControl1.Location = new System.Drawing.Point(8, 8);
            this.axToolbarControl1.Name = "axToolbarControl1";
            this.axToolbarControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axToolbarControl1.OcxState")));
            this.axToolbarControl1.Size = new System.Drawing.Size(592, 28);
            this.axToolbarControl1.TabIndex = 14;
            // 
            // axMapControl1
            // 
            this.axMapControl1.Location = new System.Drawing.Point(8, 40);
            this.axMapControl1.Name = "axMapControl1";
            this.axMapControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axMapControl1.OcxState")));
            this.axMapControl1.Size = new System.Drawing.Size(424, 368);
            this.axMapControl1.TabIndex = 15;
            this.axMapControl1.OnMouseDown += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMouseDownEventHandler(this.axMapControl1_OnMouseDown);
            // 
            // axLicenseControl1
            // 
            this.axLicenseControl1.Enabled = true;
            this.axLicenseControl1.Location = new System.Drawing.Point(24, 56);
            this.axLicenseControl1.Name = "axLicenseControl1";
            this.axLicenseControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axLicenseControl1.OcxState")));
            this.axLicenseControl1.Size = new System.Drawing.Size(32, 32);
            this.axLicenseControl1.TabIndex = 16;
            // 
            // Label5
            // 
            this.Label5.BackColor = System.Drawing.SystemColors.Control;
            this.Label5.Cursor = System.Windows.Forms.Cursors.Default;
            this.Label5.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Label5.Location = new System.Drawing.Point(439, 328);
            this.Label5.Name = "Label5";
            this.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Label5.Size = new System.Drawing.Size(145, 33);
            this.Label5.TabIndex = 17;
            this.Label5.Text = "Right click on the display to popup the Navigation menu";
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(608, 414);
            this.Controls.Add(this.Label5);
            this.Controls.Add(this.axLicenseControl1);
            this.Controls.Add(this.axMapControl1);
            this.Controls.Add(this.axToolbarControl1);
            this.Controls.Add(this.cmdAddSubMenu);
            this.Controls.Add(this.cmdAddMenu);
            this.Controls.Add(this.Label4);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.Label1);
            this.Name = "Form1";
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

			Application.Run(new Form1());
		}

		private void Form1_Load(object sender, System.EventArgs e)
		{
			//Set buddy control
			axToolbarControl1.SetBuddyControl(axMapControl1);

			//Create UID's and add new items to the ToolbarControl
			UID uID = new UIDClass();
			uID.Value = "esriControls.ControlsOpenDocCommand";
			axToolbarControl1.AddItem(uID, -1, -1, false, -1, esriCommandStyles.esriCommandStyleIconAndText);
			uID.Value = "esriControls.ControlsMapZoomInTool";
			axToolbarControl1.AddItem(uID, -1, -1, true, -1, esriCommandStyles.esriCommandStyleIconAndText);
			uID.Value = "esriControls.ControlsMapZoomOutTool";
			axToolbarControl1.AddItem(uID, -1, -1, false, -1, esriCommandStyles.esriCommandStyleIconAndText);
			uID.Value = "esriControls.ControlsMapPanTool";
			axToolbarControl1.AddItem(uID, -1, -1, false, -1, esriCommandStyles.esriCommandStyleIconAndText);
			uID.Value = "esriControls.ControlsMapFullExtentCommand";
			axToolbarControl1.AddItem(uID, -1, -1, false, -1, esriCommandStyles.esriCommandStyleIconAndText);

            //Create a MenuDef object
            IMenuDef menuDef = new NavigationMenu();
            //Create a ToolbarMenu
            m_navigationMenu.AddItem(menuDef, 0, -1, false, esriCommandStyles.esriCommandStyleIconAndText);
            //Set the ToolbarMenu's hook
            m_navigationMenu.SetHook(axToolbarControl1.Object);
            //Set the ToolbarMenu's caption
            m_navigationMenu.Caption ="Navigation";
		}

		private void cmdAddMenu_Click(object sender, System.EventArgs e)
		{
            //Add to the end of the Toolbar - it will be the 6th item
            axToolbarControl1.AddItem(m_navigationMenu, -1, -1, false, 0, esriCommandStyles.esriCommandStyleMenuBar);

			cmdAddMenu.Enabled = false;
			cmdAddSubMenu.Enabled = true;
		}

		private void cmdAddSubMenu_Click(object sender, System.EventArgs e)
		{
			//Create a MenuDef object
			IMenuDef menuDef = new ToolbarSubMenu();
			//Get the menu, which is the 6th item on the toolbar (indexing from 0)
			IToolbarItem toolbarItem = axToolbarControl1.GetItem(5);
			IToolbarMenu toolbarMenu = toolbarItem.Menu;
			//Add the sub-menu as the third item on the Navigation menu, making it start a new group
			toolbarMenu.AddSubMenu(menuDef, 2, true);

			cmdAddSubMenu.Enabled = false;
		}

        private void axMapControl1_OnMouseDown(object sender, IMapControlEvents2_OnMouseDownEvent e)
        {
            if (e.button == 2)
                //Popup the menu
                m_navigationMenu.PopupMenu(e.x, e.y, axMapControl1.hWnd);
        }

	}
}
