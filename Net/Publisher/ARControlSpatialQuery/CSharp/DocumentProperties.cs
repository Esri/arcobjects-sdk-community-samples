using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using ESRI.ArcGIS.PublisherControls;
using ESRI.ArcGIS.ADF.COMSupport;

namespace DocumentProperties
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class DocumentProperties : System.Windows.Forms.Form
	{
		internal System.Windows.Forms.Label Label6;
		internal System.Windows.Forms.Label Label5;
		internal System.Windows.Forms.Label Label4;
		internal System.Windows.Forms.Label Label3;
		internal System.Windows.Forms.Label Label2;
		internal System.Windows.Forms.Label Label1;
		internal System.Windows.Forms.RichTextBox RichTextBox1;
		internal System.Windows.Forms.Button btnGlobeProperties;
		internal System.Windows.Forms.Button btnProperties;
        internal System.Windows.Forms.Button btnLoad;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		internal System.Windows.Forms.CheckBox chkTOC;
        private AxArcReaderGlobeControl axArcReaderGlobeControl1;
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
            this.Label6 = new System.Windows.Forms.Label();
            this.Label5 = new System.Windows.Forms.Label();
            this.Label4 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.RichTextBox1 = new System.Windows.Forms.RichTextBox();
            this.chkTOC = new System.Windows.Forms.CheckBox();
            this.btnGlobeProperties = new System.Windows.Forms.Button();
            this.btnProperties = new System.Windows.Forms.Button();
            this.btnLoad = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.axArcReaderGlobeControl1 = new ESRI.ArcGIS.PublisherControls.AxArcReaderGlobeControl();
            ((System.ComponentModel.ISupportInitialize)(this.axArcReaderGlobeControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // Label6
            // 
            this.Label6.ForeColor = System.Drawing.SystemColors.Highlight;
            this.Label6.Location = new System.Drawing.Point(480, 120);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(248, 24);
            this.Label6.TabIndex = 32;
            this.Label6.Text = "6) Display the globe and layer properties.";
            // 
            // Label5
            // 
            this.Label5.ForeColor = System.Drawing.SystemColors.Highlight;
            this.Label5.Location = new System.Drawing.Point(480, 104);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(248, 32);
            this.Label5.TabIndex = 31;
            this.Label5.Text = "5) Hide the TOC.";
            // 
            // Label4
            // 
            this.Label4.ForeColor = System.Drawing.SystemColors.Highlight;
            this.Label4.Location = new System.Drawing.Point(480, 72);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(248, 32);
            this.Label4.TabIndex = 30;
            this.Label4.Text = "4) Right click on a layer in the TOC to display the layer properties.";
            // 
            // Label3
            // 
            this.Label3.ForeColor = System.Drawing.SystemColors.Highlight;
            this.Label3.Location = new System.Drawing.Point(480, 40);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(248, 32);
            this.Label3.TabIndex = 29;
            this.Label3.Text = "3) Right click on the globe in the TOC to display the data frame properties.";
            // 
            // Label2
            // 
            this.Label2.ForeColor = System.Drawing.SystemColors.Highlight;
            this.Label2.Location = new System.Drawing.Point(480, 24);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(248, 16);
            this.Label2.TabIndex = 28;
            this.Label2.Text = "2) Display the file properties.";
            // 
            // Label1
            // 
            this.Label1.ForeColor = System.Drawing.SystemColors.Highlight;
            this.Label1.Location = new System.Drawing.Point(480, 8);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(248, 16);
            this.Label1.TabIndex = 27;
            this.Label1.Text = "1) Browse to a 3D PMF to load.";
            // 
            // RichTextBox1
            // 
            this.RichTextBox1.Location = new System.Drawing.Point(480, 144);
            this.RichTextBox1.Name = "RichTextBox1";
            this.RichTextBox1.Size = new System.Drawing.Size(256, 344);
            this.RichTextBox1.TabIndex = 26;
            this.RichTextBox1.Text = "";
            // 
            // chkTOC
            // 
            this.chkTOC.Location = new System.Drawing.Point(368, 24);
            this.chkTOC.Name = "chkTOC";
            this.chkTOC.Size = new System.Drawing.Size(88, 16);
            this.chkTOC.TabIndex = 25;
            this.chkTOC.Text = "TOC Visible";
            this.chkTOC.CheckedChanged += new System.EventHandler(this.chkTOC_Click);
            // 
            // btnGlobeProperties
            // 
            this.btnGlobeProperties.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnGlobeProperties.Location = new System.Drawing.Point(200, 8);
            this.btnGlobeProperties.Name = "btnGlobeProperties";
            this.btnGlobeProperties.Size = new System.Drawing.Size(136, 40);
            this.btnGlobeProperties.TabIndex = 24;
            this.btnGlobeProperties.Text = "Globe / Layer Properties";
            this.btnGlobeProperties.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnGlobeProperties.Click += new System.EventHandler(this.btnGlobeProperties_Click);
            // 
            // btnProperties
            // 
            this.btnProperties.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnProperties.Location = new System.Drawing.Point(104, 8);
            this.btnProperties.Name = "btnProperties";
            this.btnProperties.Size = new System.Drawing.Size(88, 40);
            this.btnProperties.TabIndex = 23;
            this.btnProperties.Text = "File Properties";
            this.btnProperties.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnProperties.Click += new System.EventHandler(this.btnProperties_Click);
            // 
            // btnLoad
            // 
            this.btnLoad.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnLoad.Location = new System.Drawing.Point(8, 8);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(88, 40);
            this.btnLoad.TabIndex = 22;
            this.btnLoad.Text = "Load PMF";
            this.btnLoad.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // axArcReaderGlobeControl1
            // 
            this.axArcReaderGlobeControl1.Location = new System.Drawing.Point(8, 54);
            this.axArcReaderGlobeControl1.Name = "axArcReaderGlobeControl1";
            this.axArcReaderGlobeControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axArcReaderGlobeControl1.OcxState")));
            this.axArcReaderGlobeControl1.Size = new System.Drawing.Size(460, 434);
            this.axArcReaderGlobeControl1.TabIndex = 33;
            // 
            // DocumentProperties
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(744, 494);
            this.Controls.Add(this.axArcReaderGlobeControl1);
            this.Controls.Add(this.Label6);
            this.Controls.Add(this.Label5);
            this.Controls.Add(this.Label4);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.RichTextBox1);
            this.Controls.Add(this.chkTOC);
            this.Controls.Add(this.btnGlobeProperties);
            this.Controls.Add(this.btnProperties);
            this.Controls.Add(this.btnLoad);
            this.Name = "DocumentProperties";
            this.Text = "DocumentProperties";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.DocumentProperties_Closing);
            this.Load += new System.EventHandler(this.DocumentProperties_Load);
            ((System.ComponentModel.ISupportInitialize)(this.axArcReaderGlobeControl1)).EndInit();
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

		private void btnLoad_Click(object sender, System.EventArgs e)
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
			if (axArcReaderGlobeControl1.CheckDocument(sFilePath) == true) 
			{
				axArcReaderGlobeControl1.LoadDocument(sFilePath, "");
			}
			else 
			{
				System.Windows.Forms.MessageBox.Show("This document cannot be loaded!");
				return;
			}
			if (axArcReaderGlobeControl1.HasDocumentPermission(esriARDocumentPermissions.esriARDocumentPermissionsViewTOC) == true) 
			{
				chkTOC.Enabled = true;
				if (axArcReaderGlobeControl1.TOCVisible == true) 
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

		private void btnGlobeProperties_Click(object sender, System.EventArgs e)
		{
			//Determine whether a document is loaded
			if (axArcReaderGlobeControl1.CurrentGlobeViewType == esriARGlobeViewType.esriARGlobeViewTypeNone)
			{
				MessageBox.Show("You must load a Published Map File (PMF) first!");
				return;
			}

			int j = 0;
			IARGlobe pGlobe = null;
			IARLayer pLayer = null;
			string sProps = "";

			sProps = "Globe and Layer Properties" + "\xA" + "\xA";

			//Populate rich text box ("\x9"->tab, "\xA"->new line)
			//Get the IARGlobe interface
			pGlobe = axArcReaderGlobeControl1.ARGlobe;
			//Get pmf properties
			sProps = sProps + axArcReaderGlobeControl1.DocumentFilename.ToUpper() + "\xA" + 
			"Description:" + axArcReaderGlobeControl1.DocumentComment + "\xA" + 
			"Spatial Reference:" + "\x9" + pGlobe.SpatialReferenceName + "\xA" + 
			"Units:" + "\x9" + "\x9" + axArcReaderGlobeControl1.ARUnitConverter.EsriUnitsAsString(pGlobe.GlobeUnits, esriARCaseAppearance.esriARCaseAppearanceUnchanged, true) + "\xA";
	
			sProps = sProps + "\xA";

			//Loop through each layer in the pmf
			int ForTemp1 = pGlobe.ARLayerCount - 1;
			for (j = 0; j <= ForTemp1; j++)
			{

				//Get the IARLayer interface
				pLayer = pGlobe.get_ARLayer(j);

				//Get the layer properties
				sProps = sProps + pLayer.Name + "\xA" + 
				"Description:" + "\x9" + pLayer.Description + "\xA" + 
				"Minimum Scale:" + "\x9" + pLayer.MinimumScale + "\xA" + 
				"Maximum Scale:" + "\x9" + pLayer.MaximumScale + "\xA" + "\xA";
			}

			sProps = sProps + "\xA" + "\xA";
			RichTextBox1.Text = sProps;

		}

		private void DocumentProperties_Load(object sender, System.EventArgs e)
		{
			System.Drawing.Bitmap bitmap1 = new System.Drawing.Bitmap (GetType().Assembly.GetManifestResourceStream(GetType(), "browse.bmp"));
			bitmap1.MakeTransparent(System.Drawing.Color.Teal);
			btnLoad.Image = bitmap1;
			System.Drawing.Bitmap bitmap2 = new System.Drawing.Bitmap (GetType().Assembly.GetManifestResourceStream(GetType(), "properti.bmp"));
			bitmap2.MakeTransparent(System.Drawing.Color.Teal);
			btnProperties.Image = bitmap2;
			btnGlobeProperties.Image = bitmap2;
			chkTOC.Enabled = false;
		}

		private void btnProperties_Click(object sender, System.EventArgs e)
		{
					if (axArcReaderGlobeControl1.CurrentGlobeViewType == esriARGlobeViewType.esriARGlobeViewTypeNone) 
			{
				System.Windows.Forms.MessageBox.Show("You must load a map document first!");
			}
			else 
			{
				axArcReaderGlobeControl1.ShowARGlobeWindow(esriARGlobeWindows.esriARGlobeWindowsFileProperties, true, Type.Missing);
			}
		}

		private void chkTOC_Click(object sender, System.EventArgs e)
		{
			if (chkTOC.CheckState == CheckState.Checked)
			{
				axArcReaderGlobeControl1.TOCVisible = true;
			}
			else 
			{
				axArcReaderGlobeControl1.TOCVisible = false;
			}
		}

		private void DocumentProperties_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			//Release COM objects
			ESRI.ArcGIS.ADF.COMSupport.AOUninitialize.Shutdown();
		}


	}
}
