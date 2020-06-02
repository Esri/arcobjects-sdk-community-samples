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
using System.IO;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS;

namespace MetadataViewer
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.Button buttonLoad;
		private System.Windows.Forms.ComboBox cboStyleSheets;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;

		private ITOCControl m_tocControl;
		private string m_tempFile;
		private string m_tempDir;
		private ILayer m_layer;
		private ESRI.ArcGIS.Controls.AxTOCControl axTOCControl1;
		private ESRI.ArcGIS.Controls.AxPageLayoutControl axPageLayoutControl1;
		private ESRI.ArcGIS.Controls.AxLicenseControl axLicenseControl1;
        private WebBrowser webBrowser1;

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
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.buttonLoad = new System.Windows.Forms.Button();
            this.cboStyleSheets = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.axTOCControl1 = new ESRI.ArcGIS.Controls.AxTOCControl();
            this.axPageLayoutControl1 = new ESRI.ArcGIS.Controls.AxPageLayoutControl();
            this.axLicenseControl1 = new ESRI.ArcGIS.Controls.AxLicenseControl();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            ((System.ComponentModel.ISupportInitialize)(this.axTOCControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axPageLayoutControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonLoad
            // 
            this.buttonLoad.Location = new System.Drawing.Point(8, 8);
            this.buttonLoad.Name = "buttonLoad";
            this.buttonLoad.Size = new System.Drawing.Size(176, 48);
            this.buttonLoad.TabIndex = 2;
            this.buttonLoad.Text = "Load Document";
            this.buttonLoad.Click += new System.EventHandler(this.button1_Click);
            // 
            // cboStyleSheets
            // 
            this.cboStyleSheets.Location = new System.Drawing.Point(550, 23);
            this.cboStyleSheets.Name = "cboStyleSheets";
            this.cboStyleSheets.Size = new System.Drawing.Size(326, 21);
            this.cboStyleSheets.TabIndex = 6;
            this.cboStyleSheets.SelectedIndexChanged += new System.EventHandler(this.cboStyleSheets_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label1.Location = new System.Drawing.Point(192, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(296, 16);
            this.label1.TabIndex = 8;
            this.label1.Text = "1) Load a map document into the PageLayoutControl";
            // 
            // label2
            // 
            this.label2.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label2.Location = new System.Drawing.Point(192, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(296, 16);
            this.label2.TabIndex = 9;
            this.label2.Text = "2) Select a style sheet or enter the file path to style sheet";
            // 
            // label3
            // 
            this.label3.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label3.Location = new System.Drawing.Point(192, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(336, 16);
            this.label3.TabIndex = 10;
            this.label3.Text = "3) Right click a layer on the TOCControl to display its metadata";
            // 
            // axTOCControl1
            // 
            this.axTOCControl1.Location = new System.Drawing.Point(12, 62);
            this.axTOCControl1.Name = "axTOCControl1";
            this.axTOCControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axTOCControl1.OcxState")));
            this.axTOCControl1.Size = new System.Drawing.Size(178, 412);
            this.axTOCControl1.TabIndex = 12;
            this.axTOCControl1.OnMouseDown += new ESRI.ArcGIS.Controls.ITOCControlEvents_Ax_OnMouseDownEventHandler(this.axTOCControl1_OnMouseDown);
            // 
            // axPageLayoutControl1
            // 
            this.axPageLayoutControl1.Location = new System.Drawing.Point(196, 64);
            this.axPageLayoutControl1.Name = "axPageLayoutControl1";
            this.axPageLayoutControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axPageLayoutControl1.OcxState")));
            this.axPageLayoutControl1.Size = new System.Drawing.Size(348, 410);
            this.axPageLayoutControl1.TabIndex = 13;
            // 
            // axLicenseControl1
            // 
            this.axLicenseControl1.Enabled = true;
            this.axLicenseControl1.Location = new System.Drawing.Point(128, 96);
            this.axLicenseControl1.Name = "axLicenseControl1";
            this.axLicenseControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axLicenseControl1.OcxState")));
            this.axLicenseControl1.Size = new System.Drawing.Size(32, 32);
            this.axLicenseControl1.TabIndex = 14;
            // 
            // webBrowser1
            // 
            this.webBrowser1.Location = new System.Drawing.Point(550, 61);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(326, 413);
            this.webBrowser1.TabIndex = 15;
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(888, 486);
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.axLicenseControl1);
            this.Controls.Add(this.axPageLayoutControl1);
            this.Controls.Add(this.axTOCControl1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cboStyleSheets);
            this.Controls.Add(this.buttonLoad);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Form1";
            this.Text = "MetadataViewer";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.axTOCControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axPageLayoutControl1)).EndInit();
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
			m_tocControl = (ITOCControl) axTOCControl1.Object;
			
			//Set buddy control
			m_tocControl.SetBuddyControl(axPageLayoutControl1);

			//Get the directory of the executable
			m_tempDir = System.Reflection.Assembly.GetExecutingAssembly().Location;
			m_tempDir = Path.GetDirectoryName(m_tempDir); 
			//The location to save the temporary metadata
			m_tempFile = m_tempDir + "metadata.htm";
						
			//Add style sheets to the combo box
			cboStyleSheets.Items.Insert(0, @"Brief.xsl");
			cboStyleSheets.Items.Insert(1, @"Attributes.xsl");
			cboStyleSheets.Items.Insert(2, @"DataDictionTable.xsl");
			cboStyleSheets.Items.Insert(3, @"DataDictionPage.xsl");
			cboStyleSheets.SelectedIndex = 0;
		}

		private void ShowMetadata (ILayer layer)
		{
			if (layer is IDataLayer)
			{
				//Check style sheet exists
				if (File.Exists(cboStyleSheets.Text)== false)
				{
					System.Windows.Forms.MessageBox.Show("The selected style sheet does not exist!","Missing Style Sheet");
					return;
				}

				//QI for IDataLayer
				IDataLayer dataLayer = (IDataLayer) layer;
				//Get the metadata
				IMetadata metaData = (IMetadata) dataLayer.DataSourceName;
				//Get the xml property set from the metadata
				IXmlPropertySet2 xml = (IXmlPropertySet2) metaData.Metadata;
				
				//Save the xml to a temporary file and transforms it using the selected style sheet
				xml.SaveAsFile(cboStyleSheets.Text,"",false, ref m_tempFile );
				
				//Navigate the web browser to the temporary file
				object obj = null;
				webBrowser1.Navigate(m_tempFile);
			}
			else
			{
				System.Windows.Forms.MessageBox.Show("Metadata shown for IDataLayer objects only", "IDataLayer objects only");
			}
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			//Open a file dialog for selecting map documents
			openFileDialog1.Title = "Select Map Document";
			openFileDialog1.Filter = "Map Documents (*.mxd)|*.mxd";
			openFileDialog1.ShowDialog();

			// Exit if no map document is selected
			string sFilePath = openFileDialog1.FileName;
			if (sFilePath == "") return;

			// Load the specified mxd
			if (axPageLayoutControl1.CheckMxFile(sFilePath) == false) 
			{
				System.Windows.Forms.MessageBox.Show("This document cannot be loaded!");
				return;
			}
			axPageLayoutControl1.LoadMxFile(sFilePath, "");
			
			//Set the current directory to the that of the executable
			Directory.SetCurrentDirectory(m_tempDir);
		}

		private void cboStyleSheets_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			//Show the metadata for the layer
			if (m_layer == null) return;
			ShowMetadata(m_layer);
		}

		private void axTOCControl1_OnMouseDown(object sender, ESRI.ArcGIS.Controls.ITOCControlEvents_OnMouseDownEvent e)
		{
			//Exit not a right mouse click
			if (e.button != 2) return;
			
			//Determine what kind of item has been clicked on
			esriTOCControlItem item = esriTOCControlItem.esriTOCControlItemNone;
			IBasicMap map = null;
			object other = null; object index = null;
			m_tocControl.HitTest(e.x, e.y, ref item, ref map, ref m_layer, ref other, ref index);
			
			//Show the metadata for the layer
			if (m_layer == null) return;
			ShowMetadata(m_layer);
		}
	}
}
