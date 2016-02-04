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
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS;


namespace PageAppearance
{
	public class Form1 : System.Windows.Forms.Form
	{
		public System.Windows.Forms.TextBox txbPath;
        public System.Windows.Forms.Button cmdLoadDocument;
		public System.Windows.Forms.Label Label5;
		public System.Windows.Forms.Label Label4;
		public System.Windows.Forms.Label Label3;
		public System.Windows.Forms.Label Label2;
		public System.Windows.Forms.Label Label1;
		public System.Windows.Forms.CheckBox chkShowPrintableArea;
		public System.Windows.Forms.Button cmdReset;
		public System.Windows.Forms.GroupBox Frame1;
		public System.Windows.Forms.RadioButton optIPropertySupport;
		public System.Windows.Forms.RadioButton optIFrameProperties;
		public System.Windows.Forms.RadioButton optIPage;
		public System.Windows.Forms.Button cmdZoomPage;
		public System.Windows.Forms.Label Label7;
		public System.Windows.Forms.Label Label6;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private ESRI.ArcGIS.esriSystem.IArray[] m_StylesArray = new ESRI.ArcGIS.esriSystem.ArrayClass[4];

		private IStyleGallery m_StyleGallery;
		private ESRI.ArcGIS.Controls.AxPageLayoutControl axPageLayoutControl1;
		private ESRI.ArcGIS.Controls.AxLicenseControl axLicenseControl1;
        private ESRI.ArcGIS.Controls.AxSymbologyControl axSymbologyControl1;
        private ESRI.ArcGIS.Controls.AxSymbologyControl axSymbologyControl2;
        private ESRI.ArcGIS.Controls.AxSymbologyControl axSymbologyControl3;
        private ESRI.ArcGIS.Controls.AxSymbologyControl axSymbologyControl4;

		private System.ComponentModel.Container components = null;

		public Form1()
		{
			InitializeComponent();
		}

		protected override void Dispose( bool disposing )
		{
            //Release COM object 
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
            this.txbPath = new System.Windows.Forms.TextBox();
            this.cmdLoadDocument = new System.Windows.Forms.Button();
            this.Label5 = new System.Windows.Forms.Label();
            this.Label4 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.chkShowPrintableArea = new System.Windows.Forms.CheckBox();
            this.cmdReset = new System.Windows.Forms.Button();
            this.Frame1 = new System.Windows.Forms.GroupBox();
            this.optIPropertySupport = new System.Windows.Forms.RadioButton();
            this.optIFrameProperties = new System.Windows.Forms.RadioButton();
            this.optIPage = new System.Windows.Forms.RadioButton();
            this.cmdZoomPage = new System.Windows.Forms.Button();
            this.Label7 = new System.Windows.Forms.Label();
            this.Label6 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.axPageLayoutControl1 = new ESRI.ArcGIS.Controls.AxPageLayoutControl();
            this.axLicenseControl1 = new ESRI.ArcGIS.Controls.AxLicenseControl();
            this.axSymbologyControl1 = new ESRI.ArcGIS.Controls.AxSymbologyControl();
            this.axSymbologyControl2 = new ESRI.ArcGIS.Controls.AxSymbologyControl();
            this.axSymbologyControl3 = new ESRI.ArcGIS.Controls.AxSymbologyControl();
            this.axSymbologyControl4 = new ESRI.ArcGIS.Controls.AxSymbologyControl();
            this.Frame1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axPageLayoutControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axSymbologyControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axSymbologyControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axSymbologyControl3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axSymbologyControl4)).BeginInit();
            this.SuspendLayout();
            // 
            // txbPath
            // 
            this.txbPath.AcceptsReturn = true;
            this.txbPath.BackColor = System.Drawing.SystemColors.Window;
            this.txbPath.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txbPath.Enabled = false;
            this.txbPath.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txbPath.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txbPath.Location = new System.Drawing.Point(136, 8);
            this.txbPath.MaxLength = 0;
            this.txbPath.Name = "txbPath";
            this.txbPath.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txbPath.Size = new System.Drawing.Size(489, 20);
            this.txbPath.TabIndex = 20;
            // 
            // cmdLoadDocument
            // 
            this.cmdLoadDocument.BackColor = System.Drawing.SystemColors.Control;
            this.cmdLoadDocument.Cursor = System.Windows.Forms.Cursors.Default;
            this.cmdLoadDocument.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdLoadDocument.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdLoadDocument.Location = new System.Drawing.Point(8, 8);
            this.cmdLoadDocument.Name = "cmdLoadDocument";
            this.cmdLoadDocument.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmdLoadDocument.Size = new System.Drawing.Size(121, 25);
            this.cmdLoadDocument.TabIndex = 19;
            this.cmdLoadDocument.Text = "Load Map Document";
            this.cmdLoadDocument.UseVisualStyleBackColor = false;
            this.cmdLoadDocument.Click += new System.EventHandler(this.cmdLoadDocument_Click);
            // 
            // Label5
            // 
            this.Label5.BackColor = System.Drawing.SystemColors.Control;
            this.Label5.Cursor = System.Windows.Forms.Cursors.Default;
            this.Label5.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Label5.Location = new System.Drawing.Point(240, 40);
            this.Label5.Name = "Label5";
            this.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Label5.Size = new System.Drawing.Size(377, 17);
            this.Label5.TabIndex = 29;
            this.Label5.Text = "Symbols from Style Classes (double click a on a symbol to apply it to the page):";
            // 
            // Label4
            // 
            this.Label4.BackColor = System.Drawing.SystemColors.Control;
            this.Label4.Cursor = System.Windows.Forms.Cursors.Default;
            this.Label4.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Label4.Location = new System.Drawing.Point(528, 64);
            this.Label4.Name = "Label4";
            this.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Label4.Size = new System.Drawing.Size(97, 17);
            this.Label4.TabIndex = 28;
            this.Label4.Text = "Shadow";
            // 
            // Label3
            // 
            this.Label3.BackColor = System.Drawing.SystemColors.Control;
            this.Label3.Cursor = System.Windows.Forms.Cursors.Default;
            this.Label3.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Label3.Location = new System.Drawing.Point(432, 64);
            this.Label3.Name = "Label3";
            this.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Label3.Size = new System.Drawing.Size(89, 17);
            this.Label3.TabIndex = 27;
            this.Label3.Text = "Colors";
            // 
            // Label2
            // 
            this.Label2.BackColor = System.Drawing.SystemColors.Control;
            this.Label2.Cursor = System.Windows.Forms.Cursors.Default;
            this.Label2.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Label2.Location = new System.Drawing.Point(336, 64);
            this.Label2.Name = "Label2";
            this.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Label2.Size = new System.Drawing.Size(97, 17);
            this.Label2.TabIndex = 26;
            this.Label2.Text = "Backgrounds";
            // 
            // Label1
            // 
            this.Label1.BackColor = System.Drawing.SystemColors.Control;
            this.Label1.Cursor = System.Windows.Forms.Cursors.Default;
            this.Label1.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Label1.Location = new System.Drawing.Point(240, 64);
            this.Label1.Name = "Label1";
            this.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Label1.Size = new System.Drawing.Size(97, 17);
            this.Label1.TabIndex = 25;
            this.Label1.Text = "Borders";
            // 
            // chkShowPrintableArea
            // 
            this.chkShowPrintableArea.BackColor = System.Drawing.SystemColors.Control;
            this.chkShowPrintableArea.Checked = true;
            this.chkShowPrintableArea.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowPrintableArea.Cursor = System.Windows.Forms.Cursors.Default;
            this.chkShowPrintableArea.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkShowPrintableArea.ForeColor = System.Drawing.SystemColors.ControlText;
            this.chkShowPrintableArea.Location = new System.Drawing.Point(339, 362);
            this.chkShowPrintableArea.Name = "chkShowPrintableArea";
            this.chkShowPrintableArea.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.chkShowPrintableArea.Size = new System.Drawing.Size(128, 17);
            this.chkShowPrintableArea.TabIndex = 33;
            this.chkShowPrintableArea.Text = "Show Printable Area";
            this.chkShowPrintableArea.UseVisualStyleBackColor = false;
            this.chkShowPrintableArea.Click += new System.EventHandler(this.chkShowPrintableArea_Click);
            // 
            // cmdReset
            // 
            this.cmdReset.BackColor = System.Drawing.SystemColors.Control;
            this.cmdReset.Cursor = System.Windows.Forms.Cursors.Default;
            this.cmdReset.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdReset.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdReset.Location = new System.Drawing.Point(243, 362);
            this.cmdReset.Name = "cmdReset";
            this.cmdReset.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmdReset.Size = new System.Drawing.Size(89, 25);
            this.cmdReset.TabIndex = 32;
            this.cmdReset.Text = "Reset Page";
            this.cmdReset.UseVisualStyleBackColor = false;
            this.cmdReset.Click += new System.EventHandler(this.cmdReset_Click);
            // 
            // Frame1
            // 
            this.Frame1.BackColor = System.Drawing.SystemColors.Control;
            this.Frame1.Controls.Add(this.optIPropertySupport);
            this.Frame1.Controls.Add(this.optIFrameProperties);
            this.Frame1.Controls.Add(this.optIPage);
            this.Frame1.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Frame1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Frame1.Location = new System.Drawing.Point(496, 362);
            this.Frame1.Name = "Frame1";
            this.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Frame1.Size = new System.Drawing.Size(129, 81);
            this.Frame1.TabIndex = 31;
            this.Frame1.TabStop = false;
            this.Frame1.Text = "How to apply symbol";
            // 
            // optIPropertySupport
            // 
            this.optIPropertySupport.BackColor = System.Drawing.SystemColors.Control;
            this.optIPropertySupport.Cursor = System.Windows.Forms.Cursors.Default;
            this.optIPropertySupport.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optIPropertySupport.ForeColor = System.Drawing.SystemColors.ControlText;
            this.optIPropertySupport.Location = new System.Drawing.Point(16, 56);
            this.optIPropertySupport.Name = "optIPropertySupport";
            this.optIPropertySupport.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.optIPropertySupport.Size = new System.Drawing.Size(105, 17);
            this.optIPropertySupport.TabIndex = 13;
            this.optIPropertySupport.TabStop = true;
            this.optIPropertySupport.Text = "IPropertySupport";
            this.optIPropertySupport.UseVisualStyleBackColor = false;
            // 
            // optIFrameProperties
            // 
            this.optIFrameProperties.BackColor = System.Drawing.SystemColors.Control;
            this.optIFrameProperties.Cursor = System.Windows.Forms.Cursors.Default;
            this.optIFrameProperties.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optIFrameProperties.ForeColor = System.Drawing.SystemColors.ControlText;
            this.optIFrameProperties.Location = new System.Drawing.Point(16, 36);
            this.optIFrameProperties.Name = "optIFrameProperties";
            this.optIFrameProperties.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.optIFrameProperties.Size = new System.Drawing.Size(105, 17);
            this.optIFrameProperties.TabIndex = 12;
            this.optIFrameProperties.TabStop = true;
            this.optIFrameProperties.Text = "IFrameProperties";
            this.optIFrameProperties.UseVisualStyleBackColor = false;
            // 
            // optIPage
            // 
            this.optIPage.BackColor = System.Drawing.SystemColors.Control;
            this.optIPage.Checked = true;
            this.optIPage.Cursor = System.Windows.Forms.Cursors.Default;
            this.optIPage.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optIPage.ForeColor = System.Drawing.SystemColors.ControlText;
            this.optIPage.Location = new System.Drawing.Point(16, 16);
            this.optIPage.Name = "optIPage";
            this.optIPage.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.optIPage.Size = new System.Drawing.Size(105, 17);
            this.optIPage.TabIndex = 11;
            this.optIPage.TabStop = true;
            this.optIPage.Text = "IPage ";
            this.optIPage.UseVisualStyleBackColor = false;
            // 
            // cmdZoomPage
            // 
            this.cmdZoomPage.BackColor = System.Drawing.SystemColors.Control;
            this.cmdZoomPage.Cursor = System.Windows.Forms.Cursors.Default;
            this.cmdZoomPage.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdZoomPage.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdZoomPage.Location = new System.Drawing.Point(243, 394);
            this.cmdZoomPage.Name = "cmdZoomPage";
            this.cmdZoomPage.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmdZoomPage.Size = new System.Drawing.Size(89, 25);
            this.cmdZoomPage.TabIndex = 30;
            this.cmdZoomPage.Text = "Zoom to Page";
            this.cmdZoomPage.UseVisualStyleBackColor = false;
            this.cmdZoomPage.Click += new System.EventHandler(this.cmdZoomPage_Click);
            // 
            // Label7
            // 
            this.Label7.BackColor = System.Drawing.SystemColors.Control;
            this.Label7.Cursor = System.Windows.Forms.Cursors.Default;
            this.Label7.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label7.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Label7.Location = new System.Drawing.Point(338, 410);
            this.Label7.Name = "Label7";
            this.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Label7.Size = new System.Drawing.Size(137, 17);
            this.Label7.TabIndex = 35;
            this.Label7.Text = "Right mouse button to pan.";
            // 
            // Label6
            // 
            this.Label6.BackColor = System.Drawing.SystemColors.Control;
            this.Label6.Cursor = System.Windows.Forms.Cursors.Default;
            this.Label6.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label6.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Label6.Location = new System.Drawing.Point(338, 394);
            this.Label6.Name = "Label6";
            this.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Label6.Size = new System.Drawing.Size(153, 17);
            this.Label6.TabIndex = 34;
            this.Label6.Text = "Left mouse button to zoom in.";
            // 
            // axPageLayoutControl1
            // 
            this.axPageLayoutControl1.Location = new System.Drawing.Point(8, 64);
            this.axPageLayoutControl1.Name = "axPageLayoutControl1";
            this.axPageLayoutControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axPageLayoutControl1.OcxState")));
            this.axPageLayoutControl1.Size = new System.Drawing.Size(224, 280);
            this.axPageLayoutControl1.TabIndex = 36;
            this.axPageLayoutControl1.OnMouseDown += new ESRI.ArcGIS.Controls.IPageLayoutControlEvents_Ax_OnMouseDownEventHandler(this.axPageLayoutControl1_OnMouseDown);
            // 
            // axLicenseControl1
            // 
            this.axLicenseControl1.Enabled = true;
            this.axLicenseControl1.Location = new System.Drawing.Point(184, 192);
            this.axLicenseControl1.Name = "axLicenseControl1";
            this.axLicenseControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axLicenseControl1.OcxState")));
            this.axLicenseControl1.Size = new System.Drawing.Size(32, 32);
            this.axLicenseControl1.TabIndex = 37;
            // 
            // axSymbologyControl1
            // 
            this.axSymbologyControl1.Location = new System.Drawing.Point(243, 84);
            this.axSymbologyControl1.Name = "axSymbologyControl1";
            this.axSymbologyControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axSymbologyControl1.OcxState")));
            this.axSymbologyControl1.Size = new System.Drawing.Size(94, 264);
            this.axSymbologyControl1.TabIndex = 38;
            this.axSymbologyControl1.OnDoubleClick += new ESRI.ArcGIS.Controls.ISymbologyControlEvents_Ax_OnDoubleClickEventHandler(this.axSymbologyControl1_OnDoubleClick);
            // 
            // axSymbologyControl2
            // 
            this.axSymbologyControl2.Location = new System.Drawing.Point(339, 83);
            this.axSymbologyControl2.Name = "axSymbologyControl2";
            this.axSymbologyControl2.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axSymbologyControl2.OcxState")));
            this.axSymbologyControl2.Size = new System.Drawing.Size(94, 265);
            this.axSymbologyControl2.TabIndex = 39;
            this.axSymbologyControl2.OnDoubleClick += new ESRI.ArcGIS.Controls.ISymbologyControlEvents_Ax_OnDoubleClickEventHandler(this.axSymbologyControl2_OnDoubleClick);
            // 
            // axSymbologyControl3
            // 
            this.axSymbologyControl3.Location = new System.Drawing.Point(435, 83);
            this.axSymbologyControl3.Name = "axSymbologyControl3";
            this.axSymbologyControl3.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axSymbologyControl3.OcxState")));
            this.axSymbologyControl3.Size = new System.Drawing.Size(94, 265);
            this.axSymbologyControl3.TabIndex = 40;
            this.axSymbologyControl3.OnDoubleClick += new ESRI.ArcGIS.Controls.ISymbologyControlEvents_Ax_OnDoubleClickEventHandler(this.axSymbologyControl3_OnDoubleClick);
            // 
            // axSymbologyControl4
            // 
            this.axSymbologyControl4.Location = new System.Drawing.Point(531, 83);
            this.axSymbologyControl4.Name = "axSymbologyControl4";
            this.axSymbologyControl4.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axSymbologyControl4.OcxState")));
            this.axSymbologyControl4.Size = new System.Drawing.Size(94, 265);
            this.axSymbologyControl4.TabIndex = 41;
            this.axSymbologyControl4.OnDoubleClick += new ESRI.ArcGIS.Controls.ISymbologyControlEvents_Ax_OnDoubleClickEventHandler(this.axSymbologyControl4_OnDoubleClick);
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(632, 449);
            this.Controls.Add(this.axSymbologyControl4);
            this.Controls.Add(this.axSymbologyControl3);
            this.Controls.Add(this.axSymbologyControl2);
            this.Controls.Add(this.axSymbologyControl1);
            this.Controls.Add(this.axLicenseControl1);
            this.Controls.Add(this.axPageLayoutControl1);
            this.Controls.Add(this.chkShowPrintableArea);
            this.Controls.Add(this.cmdReset);
            this.Controls.Add(this.Frame1);
            this.Controls.Add(this.cmdZoomPage);
            this.Controls.Add(this.Label7);
            this.Controls.Add(this.Label6);
            this.Controls.Add(this.Label5);
            this.Controls.Add(this.Label4);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.txbPath);
            this.Controls.Add(this.cmdLoadDocument);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Frame1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axPageLayoutControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axSymbologyControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axSymbologyControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axSymbologyControl3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axSymbologyControl4)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

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

        private void cmdLoadDocument_Click(object sender, System.EventArgs e)
        {
            //Open a file dialog for selecting map documents
            openFileDialog1.Title = "Browse Map Document";
            openFileDialog1.Filter = "Map Documents (*.mxd)|*.mxd";
            openFileDialog1.ShowDialog();

            //Exit if no map document is selected
            string sFilePath = openFileDialog1.FileName;
            if (sFilePath == "") return;

            //Validate and load the Mx document
            if (axPageLayoutControl1.CheckMxFile(sFilePath))
            {
                axPageLayoutControl1.LoadMxFile(sFilePath, Type.Missing);
                txbPath.Text = sFilePath;
            }
            else
            {
                MessageBox.Show(sFilePath + " is not a valid ArcMap document");
            }
        }

        private void cmdReset_Click(object sender, System.EventArgs e)
        {
            //Replace the PageLayout object to reset all the changed values
            axPageLayoutControl1.PageLayout = new PageLayoutClass();
            chkShowPrintableArea.CheckState = CheckState.Checked;
        }

        private void cmdZoomPage_Click(object sender, System.EventArgs e)
        {
            //Zoom to page
            axPageLayoutControl1.ZoomToWholePage();
        }

        private void chkShowPrintableArea_Click(object sender, System.EventArgs e)
        {
            //Toggle whether the printable area is visible
            axPageLayoutControl1.Page.IsPrintableAreaVisible = (chkShowPrintableArea.Checked);
        }

        private void axPageLayoutControl1_OnMouseDown(object sender, ESRI.ArcGIS.Controls.IPageLayoutControlEvents_OnMouseDownEvent e)
        {
            //Zoom in or pan
            if (e.button == 1)
            {
                axPageLayoutControl1.Extent = axPageLayoutControl1.TrackRectangle();
            }
            else if (e.button == 2)
            {
                axPageLayoutControl1.Pan();
            }
        }

		private void Form1_Load(object sender, System.EventArgs e)
		{
            //Get the ArcGIS install location by opening the subkey for reading            
            String installationFolder = ESRI.ArcGIS.RuntimeManager.ActiveRuntime.Path;
            //Load the ESRI.ServerStyle file into the SymbologyControl's            
            axSymbologyControl1.LoadStyleFile(installationFolder + "\\Styles\\ESRI.ServerStyle");
            axSymbologyControl2.LoadStyleFile(installationFolder + "\\Styles\\ESRI.ServerStyle");
            axSymbologyControl3.LoadStyleFile(installationFolder + "\\Styles\\ESRI.ServerStyle");
            axSymbologyControl4.LoadStyleFile(installationFolder + "\\Styles\\ESRI.ServerStyle");
            //Set the SymbologyStyleClass
            axSymbologyControl1.StyleClass = esriSymbologyStyleClass.esriStyleClassBorders;
            axSymbologyControl2.StyleClass = esriSymbologyStyleClass.esriStyleClassBackgrounds;
            axSymbologyControl3.StyleClass = esriSymbologyStyleClass.esriStyleClassColors;
            axSymbologyControl4.StyleClass = esriSymbologyStyleClass.esriStyleClassShadows;
            //Set the SymbologyControl's display style
            axSymbologyControl1.DisplayStyle = esriSymbologyDisplayStyle.esriDisplayStyleList;
            axSymbologyControl2.DisplayStyle = esriSymbologyDisplayStyle.esriDisplayStyleList;
            axSymbologyControl3.DisplayStyle = esriSymbologyDisplayStyle.esriDisplayStyleList;
            axSymbologyControl4.DisplayStyle = esriSymbologyDisplayStyle.esriDisplayStyleList;
		}

        private void axSymbologyControl1_OnDoubleClick(object sender, ESRI.ArcGIS.Controls.ISymbologyControlEvents_OnDoubleClickEvent e)
        {
            //Update symbols with selected symbol
            UpdateSymbol(axSymbologyControl1.GetStyleClass(axSymbologyControl1.StyleClass).GetSelectedItem());
        }

        private void axSymbologyControl2_OnDoubleClick(object sender, ESRI.ArcGIS.Controls.ISymbologyControlEvents_OnDoubleClickEvent e)
        {
            //Update symbols with selected symbol
            UpdateSymbol(axSymbologyControl2.GetStyleClass(axSymbologyControl2.StyleClass).GetSelectedItem());
        }

        private void axSymbologyControl3_OnDoubleClick(object sender, ESRI.ArcGIS.Controls.ISymbologyControlEvents_OnDoubleClickEvent e)
        {
            //Update symbols with selected symbol
            UpdateSymbol(axSymbologyControl3.GetStyleClass(axSymbologyControl3.StyleClass).GetSelectedItem());
        }

        private void axSymbologyControl4_OnDoubleClick(object sender, ESRI.ArcGIS.Controls.ISymbologyControlEvents_OnDoubleClickEvent e)
        {
            //Update symbols with selected symbol
            UpdateSymbol(axSymbologyControl4.GetStyleClass(axSymbologyControl4.StyleClass).GetSelectedItem());
        }

        private void UpdateSymbol(IStyleGalleryItem styleGalleryItem)
        { 
            //Get IPage interface
            IPage page = axPageLayoutControl1.Page;

            //Apply the symbol as a property to the page
            if (optIPropertySupport.Checked)
            {
                //Query interface for IPropertySupport
                IPropertySupport propertySupport = page as IPropertySupport;
                //If the symbol can be applied
                if (propertySupport.CanApply(styleGalleryItem.Item))
                    //Apply the object
                    propertySupport.Apply(styleGalleryItem.Item);
                else
                    MessageBox.Show("Unable to apply this symbol!");
            }

            //Apply the symbol as an IFrameProperties property
            if (optIFrameProperties.Checked)
            {
                //Query interface for IFrameProperties
                IFrameProperties frameProperties = page as IFrameProperties;
                if (styleGalleryItem.Item is IBorder)
                    //Set the frame's border
                    frameProperties.Border = styleGalleryItem.Item as IBorder ;
                else if (styleGalleryItem.Item is IBackground)
                    //Set the frame's background
                    frameProperties.Background = styleGalleryItem.Item as IBackground;
                else if (styleGalleryItem.Item is IColor)
                    MessageBox.Show("There is no color property on IFrameProperties!");
                else if (styleGalleryItem.Item is IShadow) 
                    //Set the frame's shadow
                    frameProperties.Shadow = styleGalleryItem.Item as IShadow;
            }

            //Apply the symbol as an IPage property
            if (optIPage.Checked)
            {
                if (styleGalleryItem.Item is IBorder)
                    //Set the frame's border
                    page.Border = styleGalleryItem.Item as IBorder;
                else if (styleGalleryItem.Item is IBackground)
                    //Set the frame's background
                    page.Background = styleGalleryItem.Item as IBackground;
                else if (styleGalleryItem.Item is IColor)
                    //Set the frame's background color
                    page.BackgroundColor = styleGalleryItem.Item as IColor;
                else if (styleGalleryItem.Item is IShadow)
                    MessageBox.Show("There is no shadow property on IPage!");
            }

            //Refresh
            axPageLayoutControl1.Refresh(esriViewDrawPhase.esriViewBackground, null, null);
        }
	}
}
