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
using System.Runtime.InteropServices;
using System.Reflection;

using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS;


namespace Controls
{
    /// <summary>
    /// Summary description for Form1.
    /// </summary>
    public class Form1 : System.Windows.Forms.Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;
        private TableLayoutPanel tableLayoutPanel1;
        private Label lblCulture;
        private AxLicenseControl axLicenseControl1;
        private AxPageLayoutControl axPageLayoutControl1;
        private AxTOCControl axTOCControl1;
        private AxToolbarControl axToolbarControl1;
        ToolbarMenu m_pToolbarMenu = new ToolbarMenuClass();

        public Form1()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblCulture = new System.Windows.Forms.Label();
            this.axLicenseControl1 = new ESRI.ArcGIS.Controls.AxLicenseControl();
            this.axPageLayoutControl1 = new ESRI.ArcGIS.Controls.AxPageLayoutControl();
            this.axTOCControl1 = new ESRI.ArcGIS.Controls.AxTOCControl();
            this.axToolbarControl1 = new ESRI.ArcGIS.Controls.AxToolbarControl();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axPageLayoutControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axTOCControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axToolbarControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.05695F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 74.94305F));
            this.tableLayoutPanel1.Controls.Add(this.lblCulture, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.axLicenseControl1, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.axPageLayoutControl1, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.axTOCControl1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.axToolbarControl1, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(2, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.598214F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90.40179F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(731, 445);
            this.tableLayoutPanel1.TabIndex = 6;
            // 
            // lblCulture
            // 
            this.lblCulture.AutoSize = true;
            this.lblCulture.Location = new System.Drawing.Point(3, 405);
            this.lblCulture.Name = "lblCulture";
            this.lblCulture.Size = new System.Drawing.Size(35, 13);
            this.lblCulture.TabIndex = 0;
            this.lblCulture.Text = "label1";
            // 
            // axLicenseControl1
            // 
            this.axLicenseControl1.Enabled = true;
            this.axLicenseControl1.Location = new System.Drawing.Point(186, 408);
            this.axLicenseControl1.Name = "axLicenseControl1";
            this.axLicenseControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axLicenseControl1.OcxState")));
            this.axLicenseControl1.Size = new System.Drawing.Size(32, 32);
            this.axLicenseControl1.TabIndex = 1;
            // 
            // axPageLayoutControl1
            // 
            this.axPageLayoutControl1.Location = new System.Drawing.Point(186, 41);
            this.axPageLayoutControl1.Name = "axPageLayoutControl1";
            this.axPageLayoutControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axPageLayoutControl1.OcxState")));
            this.axPageLayoutControl1.Size = new System.Drawing.Size(542, 361);
            this.axPageLayoutControl1.TabIndex = 2;
            // 
            // axTOCControl1
            // 
            this.axTOCControl1.Location = new System.Drawing.Point(3, 41);
            this.axTOCControl1.Name = "axTOCControl1";
            this.axTOCControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axTOCControl1.OcxState")));
            this.axTOCControl1.Size = new System.Drawing.Size(177, 361);
            this.axTOCControl1.TabIndex = 3;
            // 
            // axToolbarControl1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.axToolbarControl1, 2);
            this.axToolbarControl1.Location = new System.Drawing.Point(3, 3);
            this.axToolbarControl1.Name = "axToolbarControl1";
            this.axToolbarControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axToolbarControl1.OcxState")));
            this.axToolbarControl1.Size = new System.Drawing.Size(725, 28);
            this.axToolbarControl1.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(734, 454);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MaximumSize = new System.Drawing.Size(750, 492);
            this.MinimumSize = new System.Drawing.Size(750, 492);
            this.Name = "Form1";
            this.Text = "CulturalResources";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axPageLayoutControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axTOCControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axToolbarControl1)).EndInit();
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

            System.Globalization.CultureInfo pCulture;

            //Set the Thread UI Culture manually by uncommenting one of the three cultures 
            //that you wish to set below.

            pCulture = System.Globalization.CultureInfo.CreateSpecificCulture("fr-FR");
            //pCulture = System.Globalization.CultureInfo.CreateSpecificCulture("en-US");
            //pCulture = System.Globalization.CultureInfo.CreateSpecificCulture("es-ES");
            
            //Set the UI Culture
            System.Threading.Thread.CurrentThread.CurrentUICulture = pCulture;

            //Confirm that the Thread UI Culture is set.
            lblCulture.Text = "Current Thread UI Culture = " + System.Threading.Thread.CurrentThread.CurrentUICulture.DisplayName;
           
            //Add command to open an mxd document
            string sProgID;

            sProgID = "esriControls.ControlsOpenDocCommand";
            axToolbarControl1.AddItem(sProgID, -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);

            //Add Map navigation commands
            sProgID = "esriControls.ControlsMapZoomInTool";
            axToolbarControl1.AddItem(sProgID, -1, -1, true, 0, esriCommandStyles.esriCommandStyleIconOnly);
            sProgID = "esriControls.ControlsMapZoomOutTool";
            axToolbarControl1.AddItem(sProgID, -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
            sProgID = "esriControls.ControlsMapPanTool";
            axToolbarControl1.AddItem(sProgID, -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
            sProgID = "esriControls.ControlsMapFullExtentCommand";
            axToolbarControl1.AddItem(sProgID, -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);

            //Add PageLayout navigation commands
            sProgID = "esriControls.ControlsPageZoomInTool";
            axToolbarControl1.AddItem(sProgID, -1, -1, true, 0, esriCommandStyles.esriCommandStyleIconOnly);
            sProgID = "esriControls.ControlsPageZoomOutTool";
            axToolbarControl1.AddItem(sProgID, -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
            sProgID = "esriControls.ControlsPagePanTool";
            axToolbarControl1.AddItem(sProgID, -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
            sProgID = "esriControls.ControlsPageZoomWholePageCommand";
            axToolbarControl1.AddItem(sProgID, -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
            sProgID = "esriControls.ControlsPageZoomPageToLastExtentBackCommand";
            axToolbarControl1.AddItem(sProgID, -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
            sProgID = "esriControls.ControlsPageZoomPageToLastExtentForwardCommand";
            axToolbarControl1.AddItem(sProgID, -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
            sProgID = "esriControls.ControlsSelectTool";

            //Add Culture Tool
            axToolbarControl1.AddItem("VBCSharpCultureSample.CultureTool", -1, -1, true, 0, esriCommandStyles.esriCommandStyleIconAndText);

            //Add Culture Command
            axToolbarControl1.AddItem("VBCSharpCultureSample.CultureCommand", -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconAndText);

            //Add Culture Menu
            axToolbarControl1.AddItem("VBCSharpCultureSample.CultureMenu", -1, -1, false, 0, esriCommandStyles.esriCommandStyleTextOnly);

            
            //Add the MenuDef to the ToolbarMenu
            string progID = "VBCSharpCultureSample.CultureMenu";
            m_pToolbarMenu.AddItem(progID, -1, -1, false, esriCommandStyles.esriCommandStyleIconAndText);

            //Set the Toolbarmenu's hook
            m_pToolbarMenu.SetHook(axToolbarControl1);

            //Set Buddy Controls
            axTOCControl1.SetBuddyControl(axPageLayoutControl1);
            axToolbarControl1.SetBuddyControl(axPageLayoutControl1);
        }

        private void axPageLayoutControl1_OnMouseUp(object sender, IPageLayoutControlEvents_OnMouseUpEvent e)
        {
            //Display the Popup Menu so that it appears to the right of the mouse click 

            if (e.button == 2) 
            {
                //Cast to the IToolbarMenu2 interface of the m_pToolbarMenu
                IToolbarMenu2 m_pToolbarMenu2 = (IToolbarMenu2)m_pToolbarMenu;
 
                //Align the Menu so that it appears to the right of the user mouse click
                m_pToolbarMenu2.AlignLeft = true;

                //Popup the menu
                m_pToolbarMenu.PopupMenu(e.x, e.y, axPageLayoutControl1.hWnd);
            }
        }

    }
}