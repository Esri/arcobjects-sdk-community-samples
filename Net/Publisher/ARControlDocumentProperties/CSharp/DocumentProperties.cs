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
using ESRI.ArcGIS.PublisherControls;

namespace DocumentProperties
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class DocumentProperties : System.Windows.Forms.Form
	{
		public System.Windows.Forms.Button cmdMapProperties;
		public System.Windows.Forms.CheckBox chkTOC;
		public System.Windows.Forms.Button cmdProperties;
		public System.Windows.Forms.Button cmdLoad;
		public System.Windows.Forms.Label Label6;
		public System.Windows.Forms.Label Label5;
		public System.Windows.Forms.Label Label4;
		public System.Windows.Forms.Label Label1;
		public System.Windows.Forms.Label Label3;
		public System.Windows.Forms.Label Label2;
		public System.Windows.Forms.RichTextBox RichTextBox1;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
			private ESRI.ArcGIS.PublisherControls.AxArcReaderControl axArcReaderControl1;  
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public DocumentProperties()
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DocumentProperties));
            this.cmdMapProperties = new System.Windows.Forms.Button();
            this.chkTOC = new System.Windows.Forms.CheckBox();
            this.cmdProperties = new System.Windows.Forms.Button();
            this.cmdLoad = new System.Windows.Forms.Button();
            this.Label6 = new System.Windows.Forms.Label();
            this.Label5 = new System.Windows.Forms.Label();
            this.Label4 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.RichTextBox1 = new System.Windows.Forms.RichTextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.axArcReaderControl1 = new ESRI.ArcGIS.PublisherControls.AxArcReaderControl();
            ((System.ComponentModel.ISupportInitialize)(this.axArcReaderControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdMapProperties
            // 
            this.cmdMapProperties.BackColor = System.Drawing.SystemColors.Control;
            this.cmdMapProperties.Cursor = System.Windows.Forms.Cursors.Default;
            this.cmdMapProperties.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdMapProperties.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdMapProperties.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdMapProperties.Location = new System.Drawing.Point(264, 8);
            this.cmdMapProperties.Name = "cmdMapProperties";
            this.cmdMapProperties.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmdMapProperties.Size = new System.Drawing.Size(121, 33);
            this.cmdMapProperties.TabIndex = 12;
            this.cmdMapProperties.Text = "Map / Layer Properties";
            this.cmdMapProperties.UseVisualStyleBackColor = false;
            this.cmdMapProperties.Click += new System.EventHandler(this.cmdMapProperties_Click);
            // 
            // chkTOC
            // 
            this.chkTOC.BackColor = System.Drawing.SystemColors.Control;
            this.chkTOC.Cursor = System.Windows.Forms.Cursors.Default;
            this.chkTOC.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkTOC.ForeColor = System.Drawing.SystemColors.ControlText;
            this.chkTOC.Location = new System.Drawing.Point(392, 16);
            this.chkTOC.Name = "chkTOC";
            this.chkTOC.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.chkTOC.Size = new System.Drawing.Size(88, 17);
            this.chkTOC.TabIndex = 11;
            this.chkTOC.Text = "TOC Visible";
            this.chkTOC.UseVisualStyleBackColor = false;
            this.chkTOC.Click += new System.EventHandler(this.chkTOC_Click);
            // 
            // cmdProperties
            // 
            this.cmdProperties.BackColor = System.Drawing.SystemColors.Control;
            this.cmdProperties.Cursor = System.Windows.Forms.Cursors.Default;
            this.cmdProperties.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdProperties.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdProperties.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdProperties.Location = new System.Drawing.Point(136, 8);
            this.cmdProperties.Name = "cmdProperties";
            this.cmdProperties.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmdProperties.Size = new System.Drawing.Size(121, 33);
            this.cmdProperties.TabIndex = 10;
            this.cmdProperties.Text = "File Properties";
            this.cmdProperties.UseVisualStyleBackColor = false;
            this.cmdProperties.Click += new System.EventHandler(this.cmdProperties_Click);
            // 
            // cmdLoad
            // 
            this.cmdLoad.BackColor = System.Drawing.SystemColors.Control;
            this.cmdLoad.Cursor = System.Windows.Forms.Cursors.Default;
            this.cmdLoad.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdLoad.ForeColor = System.Drawing.Color.Black;
            this.cmdLoad.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdLoad.Location = new System.Drawing.Point(8, 8);
            this.cmdLoad.Name = "cmdLoad";
            this.cmdLoad.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmdLoad.Size = new System.Drawing.Size(121, 33);
            this.cmdLoad.TabIndex = 9;
            this.cmdLoad.Text = "Load PMF";
            this.cmdLoad.UseVisualStyleBackColor = false;
            this.cmdLoad.Click += new System.EventHandler(this.cmdLoad_Click);
            // 
            // Label6
            // 
            this.Label6.BackColor = System.Drawing.SystemColors.Control;
            this.Label6.Cursor = System.Windows.Forms.Cursors.Default;
            this.Label6.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label6.ForeColor = System.Drawing.SystemColors.Highlight;
            this.Label6.Location = new System.Drawing.Point(488, 144);
            this.Label6.Name = "Label6";
            this.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Label6.Size = new System.Drawing.Size(249, 17);
            this.Label6.TabIndex = 18;
            this.Label6.Text = "6) Display the map and layer properties.";
            // 
            // Label5
            // 
            this.Label5.BackColor = System.Drawing.SystemColors.Control;
            this.Label5.Cursor = System.Windows.Forms.Cursors.Default;
            this.Label5.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label5.ForeColor = System.Drawing.SystemColors.Highlight;
            this.Label5.Location = new System.Drawing.Point(488, 120);
            this.Label5.Name = "Label5";
            this.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Label5.Size = new System.Drawing.Size(249, 17);
            this.Label5.TabIndex = 17;
            this.Label5.Text = "5) Hide the TOC.";
            // 
            // Label4
            // 
            this.Label4.BackColor = System.Drawing.SystemColors.Control;
            this.Label4.Cursor = System.Windows.Forms.Cursors.Default;
            this.Label4.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label4.ForeColor = System.Drawing.SystemColors.Highlight;
            this.Label4.Location = new System.Drawing.Point(488, 88);
            this.Label4.Name = "Label4";
            this.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Label4.Size = new System.Drawing.Size(249, 33);
            this.Label4.TabIndex = 16;
            this.Label4.Text = "4) Right click on a layer in the TOC to display the layer properties.";
            // 
            // Label1
            // 
            this.Label1.BackColor = System.Drawing.SystemColors.Control;
            this.Label1.Cursor = System.Windows.Forms.Cursors.Default;
            this.Label1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.ForeColor = System.Drawing.SystemColors.Highlight;
            this.Label1.Location = new System.Drawing.Point(488, 56);
            this.Label1.Name = "Label1";
            this.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Label1.Size = new System.Drawing.Size(249, 33);
            this.Label1.TabIndex = 15;
            this.Label1.Text = "3) Right click on a map in the TOC to display the data frame properties.";
            // 
            // Label3
            // 
            this.Label3.BackColor = System.Drawing.SystemColors.Control;
            this.Label3.Cursor = System.Windows.Forms.Cursors.Default;
            this.Label3.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label3.ForeColor = System.Drawing.SystemColors.Highlight;
            this.Label3.Location = new System.Drawing.Point(488, 32);
            this.Label3.Name = "Label3";
            this.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Label3.Size = new System.Drawing.Size(249, 17);
            this.Label3.TabIndex = 14;
            this.Label3.Text = "2) Display the file properties.";
            // 
            // Label2
            // 
            this.Label2.BackColor = System.Drawing.SystemColors.Control;
            this.Label2.Cursor = System.Windows.Forms.Cursors.Default;
            this.Label2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.ForeColor = System.Drawing.SystemColors.Highlight;
            this.Label2.Location = new System.Drawing.Point(488, 8);
            this.Label2.Name = "Label2";
            this.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Label2.Size = new System.Drawing.Size(249, 17);
            this.Label2.TabIndex = 13;
            this.Label2.Text = "1) Browse to a PMF to load.";
            // 
            // RichTextBox1
            // 
            this.RichTextBox1.Location = new System.Drawing.Point(480, 168);
            this.RichTextBox1.Name = "RichTextBox1";
            this.RichTextBox1.Size = new System.Drawing.Size(256, 320);
            this.RichTextBox1.TabIndex = 19;
            this.RichTextBox1.Text = "";
            // 
            // axArcReaderControl1
            // 
            this.axArcReaderControl1.Location = new System.Drawing.Point(8, 48);
            this.axArcReaderControl1.Name = "axArcReaderControl1";
            this.axArcReaderControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axArcReaderControl1.OcxState")));
            this.axArcReaderControl1.Size = new System.Drawing.Size(464, 440);
            this.axArcReaderControl1.TabIndex = 20;
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(744, 494);
            this.Controls.Add(this.axArcReaderControl1);
            this.Controls.Add(this.RichTextBox1);
            this.Controls.Add(this.Label6);
            this.Controls.Add(this.Label5);
            this.Controls.Add(this.Label4);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.cmdMapProperties);
            this.Controls.Add(this.chkTOC);
            this.Controls.Add(this.cmdProperties);
            this.Controls.Add(this.cmdLoad);
            this.Name = "Form1";
            this.Text = "DocumentProperties";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.Form1_Closing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.axArcReaderControl1)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
            if (!ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.ArcReader))
            {
                if (!ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.EngineOrDesktop))
                {
                    MessageBox.Show("Unable to bind to ArcGIS runtime. Application will be shut down.");
                    return;
                }
            }

			Application.Run(new DocumentProperties());
		}

		private void cmdLoad_Click(object sender, System.EventArgs e)
		{
			openFileDialog1.Title = "Select Published Map Document";
			openFileDialog1.Filter = "Published Map Documents (*.pmf)|*.pmf";
			openFileDialog1.ShowDialog();
			// Exit if no map document is selected
			string  sFilePath = "";
			sFilePath = openFileDialog1.FileName;
			if (sFilePath == "") 
			{
				return;
			}
			// Load the specified pmf
			if (axArcReaderControl1.CheckDocument(sFilePath) == true) 
			{
				axArcReaderControl1.LoadDocument(sFilePath, "");
			}
			else 
			{
				System.Windows.Forms.MessageBox.Show("This document cannot be loaded!");
				return;
			}
			if (axArcReaderControl1.HasDocumentPermission(esriARDocumentPermissions.esriARDocumentPermissionsViewTOC) == true) 
			{
				chkTOC.Enabled = true;
				if (axArcReaderControl1.TOCVisible == true) 
				{
					chkTOC.CheckState = CheckState.Checked;
				}
				else 
				{
					chkTOC.CheckState = CheckState.Unchecked;
				}
			}
			else 
			{
				System.Windows.Forms.MessageBox.Show("You do not have permission to toggle TOC visibility!");
			}
			RichTextBox1.Text = "";
		}

		private void cmdProperties_Click(object sender, System.EventArgs e)
		{
			if (axArcReaderControl1.CurrentViewType == esriARViewType.esriARViewTypeNone) 
			{
				System.Windows.Forms.MessageBox.Show("You must load a map document first!");
			}
			else 
			{
				axArcReaderControl1.ShowARWindow(esriARWindows.esriARWindowsFileProperties, true, Type.Missing);
			}
		}

		private void Form1_Load(object sender, System.EventArgs e)
		{
			System.Drawing.Bitmap bitmap1 = new System.Drawing.Bitmap (GetType().Assembly.GetManifestResourceStream(GetType(), "browse.bmp"));
			bitmap1.MakeTransparent(System.Drawing.Color.Teal);
			cmdLoad.Image = bitmap1;
			System.Drawing.Bitmap bitmap2 = new System.Drawing.Bitmap (GetType().Assembly.GetManifestResourceStream(GetType(), "properti.bmp"));
			bitmap2.MakeTransparent(System.Drawing.Color.Teal);
			cmdProperties.Image = bitmap2;
			cmdMapProperties.Image = bitmap2;
			chkTOC.Enabled = false;
		}

		private void chkTOC_Click(object sender, System.EventArgs e)
		{
			if (chkTOC.CheckState == CheckState.Checked)
			{
				axArcReaderControl1.TOCVisible = true;
			}
			else 
			{
				axArcReaderControl1.TOCVisible = false;
			}
		}

		private void cmdMapProperties_Click(object sender, System.EventArgs e)
		{
			if (axArcReaderControl1.CurrentViewType == esriARViewType.esriARViewTypeNone) 
			{
				System.Windows.Forms.MessageBox.Show("You must load a map document first!");
				return;
			}
			string	sProps = "Map and Layer Properties" + "\xA" + "\xA";
			// Loop through each map in the document 
			// Get the IARMap interface
			for (int i = 0; i <= axArcReaderControl1.ARPageLayout.ARMapCount - 1; i++)
			{
				ARMap map = axArcReaderControl1.ARPageLayout.get_ARMap(i);
				// Get map properties
				sProps = sProps + map.Name.ToUpper() + "\xA" + "Description:" + map.Description + "\xA" + "Spatial Reference:" +  "\x9" + map.SpatialReferenceName +  "\xA" + "Units:" +  "\x9" +  "\x9" + axArcReaderControl1.ARUnitConverter.EsriUnitsAsString(map.DistanceUnits, esriARCaseAppearance.esriARCaseAppearanceUnchanged, true) + "\xA";
				// Get map extent type
				esriARExtentType extent = axArcReaderControl1.ARPageLayout.get_MapExtentType(map); 
				if (extent == esriARExtentType.esriARExtentTypeFixedExtent) 
				{
					sProps = sProps + "Extent Type:" + "\x9" + "Fixed Extent" + "\xA";
				}
				else if (extent == esriARExtentType.esriARExtentTypeFixedScale) 
				{
					sProps = sProps + "Extent Type:" + "\x9" + "Fixed Scale" + "\xA";
				}
				else 
				{
					sProps = sProps + "Extent Type:" + "\x9" + "Automatic" + "\xA";
				}
				sProps = sProps + "\xA";
				// Loop through each layer in the map
				// Get the IARLayer interface
				for ( int j = 0; j <= map.ARLayerCount - 1; j++)
				{
					IARLayer layer = map.get_ARLayer(j);
					// Get the layer properties
					sProps = sProps + layer.Name + "\xA" + "Description:" + "\x9" + layer.Description + "\xA" + "Minimum Scale:" + "\x9" + layer.MinimumScale + "\xA"+ "Maximum Scale:" + "\x9" + layer.MaximumScale + "\xA" + "\xA" ;
				}
				sProps = sProps + "\xA";
			}
			RichTextBox1.Text = sProps;
		}

		private void Form1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			//Release COM objects
			ESRI.ArcGIS.ADF.COMSupport.AOUninitialize.Shutdown();
		}


	}
}
