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
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Drawing.Printing;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Output;
using ESRI.ArcGIS;


namespace PrintPreview
{

	public class Form1 : System.Windows.Forms.Form
	{
		private System.ComponentModel.Container components = null;
		//buttons for open file, print preview, print dialog, page setup
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.ComboBox comboBox1;
        
		//declare the dialogs for print preview, print dialog, page setup
		internal PrintPreviewDialog printPreviewDialog1;
		internal PrintDialog printDialog1;
		internal PageSetupDialog pageSetupDialog1;
		//declare a PrintDocument object named document, to be displayed in the print preview
		private System.Drawing.Printing.PrintDocument document = new System.Drawing.Printing.PrintDocument();
		//cancel tracker which is passed to the output function when printing to the print preview
		private ITrackCancel m_TrackCancel = new CancelTrackerClass();
        private ESRI.ArcGIS.Controls.AxPageLayoutControl axPageLayoutControl1;
        private ESRI.ArcGIS.Controls.AxLicenseControl axLicenseControl1;
		//the page that is currently printed to the print preview
		private short m_CurrentPrintPage;

		public Form1()
		{
			InitializeComponent();
		}


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
      System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Form1));
      this.button1 = new System.Windows.Forms.Button();
      this.button2 = new System.Windows.Forms.Button();
      this.button3 = new System.Windows.Forms.Button();
      this.button4 = new System.Windows.Forms.Button();
      this.comboBox1 = new System.Windows.Forms.ComboBox();
      this.axPageLayoutControl1 = new ESRI.ArcGIS.Controls.AxPageLayoutControl();
      this.axLicenseControl1 = new ESRI.ArcGIS.Controls.AxLicenseControl();
      ((System.ComponentModel.ISupportInitialize)(this.axPageLayoutControl1)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).BeginInit();
      this.SuspendLayout();
      // 
      // button1
      // 
      this.button1.Location = new System.Drawing.Point(8, 8);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(88, 24);
      this.button1.TabIndex = 1;
      this.button1.Text = "Open File";
      this.button1.Click += new System.EventHandler(this.button1_Click);
      // 
      // button2
      // 
      this.button2.Location = new System.Drawing.Point(112, 8);
      this.button2.Name = "button2";
      this.button2.Size = new System.Drawing.Size(88, 24);
      this.button2.TabIndex = 4;
      this.button2.Text = "Page Setup";
      this.button2.Click += new System.EventHandler(this.button2_Click);
      // 
      // button3
      // 
      this.button3.Location = new System.Drawing.Point(216, 8);
      this.button3.Name = "button3";
      this.button3.Size = new System.Drawing.Size(88, 24);
      this.button3.TabIndex = 5;
      this.button3.Text = "Print Preview";
      this.button3.Click += new System.EventHandler(this.button3_Click);
      // 
      // button4
      // 
      this.button4.Location = new System.Drawing.Point(320, 8);
      this.button4.Name = "button4";
      this.button4.Size = new System.Drawing.Size(88, 24);
      this.button4.TabIndex = 6;
      this.button4.Text = "Print";
      this.button4.Click += new System.EventHandler(this.button4_Click);
      // 
      // comboBox1
      // 
      this.comboBox1.Location = new System.Drawing.Point(424, 8);
      this.comboBox1.Name = "comboBox1";
      this.comboBox1.Size = new System.Drawing.Size(144, 21);
      this.comboBox1.TabIndex = 8;
      // 
      // axPageLayoutControl1
      // 
      this.axPageLayoutControl1.Location = new System.Drawing.Point(0, 48);
      this.axPageLayoutControl1.Name = "axPageLayoutControl1";
      this.axPageLayoutControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axPageLayoutControl1.OcxState")));
      this.axPageLayoutControl1.Size = new System.Drawing.Size(680, 520);
      this.axPageLayoutControl1.TabIndex = 9;
      // 
      // axLicenseControl1
      // 
      this.axLicenseControl1.Enabled = true;
      this.axLicenseControl1.Location = new System.Drawing.Point(512, 88);
      this.axLicenseControl1.Name = "axLicenseControl1";
      this.axLicenseControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axLicenseControl1.OcxState")));
      this.axLicenseControl1.Size = new System.Drawing.Size(32, 32);
      this.axLicenseControl1.TabIndex = 10;
      // 
      // Form1
      // 
      this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
      this.ClientSize = new System.Drawing.Size(680, 566);
      this.Controls.Add(this.axLicenseControl1);
      this.Controls.Add(this.axPageLayoutControl1);
      this.Controls.Add(this.comboBox1);
      this.Controls.Add(this.button4);
      this.Controls.Add(this.button3);
      this.Controls.Add(this.button2);
      this.Controls.Add(this.button1);
      this.Name = "Form1";
      this.Text = "Print Preview / Print dialog Sample";
      this.Load += new System.EventHandler(this.Form1_Load);
      ((System.ComponentModel.ISupportInitialize)(this.axPageLayoutControl1)).EndInit();
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
			InitializePrintPreviewDialog(); //initialize the print preview dialog
			printDialog1 = new PrintDialog(); //create a print dialog object
			InitializePageSetupDialog(); //initialize the page setup dialog   
			
			comboBox1.Items.Add("esriPageMappingTile");
			comboBox1.Items.Add("esriPageMappingCrop");
			comboBox1.Items.Add("esriPageMappingScale");
			comboBox1.SelectedIndex= 0;
		}

		private void InitializePrintPreviewDialog()
		{
			// create a new PrintPreviewDialog using constructor
			printPreviewDialog1 = new PrintPreviewDialog();
			//set the size, location, name and the minimum size the dialog can be resized to
			printPreviewDialog1.ClientSize = new System.Drawing.Size(800, 600);
			printPreviewDialog1.Location = new System.Drawing.Point(29, 29);
			printPreviewDialog1.Name = "PrintPreviewDialog1";
            printPreviewDialog1.MinimumSize = new System.Drawing.Size(375, 250);
            //set UseAntiAlias to true to allow the operating system to smooth fonts
			printPreviewDialog1.UseAntiAlias = true;

			//associate the event-handling method with the document's PrintPage event
			this.document.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(document_PrintPage);
		}

		private void InitializePageSetupDialog()
		{
			//create a new PageSetupDialog using constructor
			pageSetupDialog1 = new PageSetupDialog();
			//initialize the dialog's PrinterSettings property to hold user defined printer settings
			pageSetupDialog1.PageSettings = new System.Drawing.Printing.PageSettings();
			//initialize dialog's PrinterSettings property to hold user set printer settings
			pageSetupDialog1.PrinterSettings = new System.Drawing.Printing.PrinterSettings();
			//do not show the network in the printer dialog
			pageSetupDialog1.ShowNetwork = false;
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			Stream myStream;

			//create an open file dialog
			OpenFileDialog openFileDialog1 = new OpenFileDialog();

			//set the file extension filter, filter index and restore directory flag
			openFileDialog1.Filter = "template files (*.mxt)|*.mxt|mxd files (*.mxd)|*.mxd" ;
			openFileDialog1.FilterIndex = 2 ;
			openFileDialog1.RestoreDirectory = true ;

			//display open file dialog and check if user clicked ok button
			if(openFileDialog1.ShowDialog() == DialogResult.OK)
			{
				//check if a file was selected
				if((myStream = openFileDialog1.OpenFile())!= null)
				{
					//get the selected filename and path
					string fileName = openFileDialog1.FileName;

					//check if selected file is mxd file
					if (axPageLayoutControl1.CheckMxFile(fileName))
					{
						//load the mxd file into PageLayout	control
						axPageLayoutControl1.LoadMxFile(fileName, "");
					}
					myStream.Close();
				}
			}
		}

		private void button2_Click(object sender, System.EventArgs e)
		{

            //Show the page setup dialog storing the result.
            DialogResult result = pageSetupDialog1.ShowDialog();

            //set the printer settings of the preview document to the selected printer settings
            document.PrinterSettings = pageSetupDialog1.PrinterSettings;

            //set the page settings of the preview document to the selected page settings
            document.DefaultPageSettings = pageSetupDialog1.PageSettings;

            //due to a bug in PageSetupDialog the PaperSize has to be set explicitly by iterating through the
            //available PaperSizes in the PageSetupDialog finding the selected PaperSize
            int i;
            IEnumerator paperSizes = pageSetupDialog1.PrinterSettings.PaperSizes.GetEnumerator();
            paperSizes.Reset();

            for(i = 0; i<pageSetupDialog1.PrinterSettings.PaperSizes.Count; ++i)
            {
                paperSizes.MoveNext();
                if(((PaperSize)paperSizes.Current).Kind == document.DefaultPageSettings.PaperSize.Kind)
                {
                document.DefaultPageSettings.PaperSize = ((PaperSize)paperSizes.Current);
                }
            }
    
            /////////////////////////////////////////////////////////////
            ///initialize the current printer from the printer settings selected
            ///in the page setup dialog
            /////////////////////////////////////////////////////////////
            IPaper paper; 
            paper = new PaperClass(); //create a paper object

            IPrinter printer;
            printer = new EmfPrinterClass(); //create a printer object
            //in this case an EMF printer, alternatively a PS printer could be used

            //initialize the paper with the DEVMODE and DEVNAMES structures from the windows GDI
            //these structures specify information about the initialization and environment of a printer as well as
            //driver, device, and output port names for a printer
            paper.Attach(pageSetupDialog1.PrinterSettings.GetHdevmode(pageSetupDialog1.PageSettings).ToInt32(), pageSetupDialog1.PrinterSettings.GetHdevnames().ToInt32());

            //pass the paper to the emf printer
            printer.Paper = paper;

            //set the page layout control's printer to the currently selected printer
            axPageLayoutControl1.Printer = printer;
		}

	    private void button3_Click(object sender, System.EventArgs e)
		{
			//initialize the currently printed page number
			m_CurrentPrintPage = 0;

			//check if a document is loaded into PageLayout	control
			if(axPageLayoutControl1.DocumentFilename==null) return;
			//set the name of the print preview document to the name of the mxd doc
			document.DocumentName = axPageLayoutControl1.DocumentFilename;

			//set the PrintPreviewDialog.Document property to the PrintDocument object selected by the user
			printPreviewDialog1.Document = document;

			//show the dialog - this triggers the document's PrintPage event
			printPreviewDialog1.ShowDialog();
    }

		private void button4_Click(object sender, System.EventArgs e)
		{
			//allow the user to choose the page range to be printed
			printDialog1.AllowSomePages = true;
			//show the help button.
			printDialog1.ShowHelp = true;

			//set the Document property to the PrintDocument for which the PrintPage Event 
			//has been handled. To display the dialog, either this property or the 
			//PrinterSettings property must be set 
			printDialog1.Document = document;

			//show the print dialog and wait for user input
			DialogResult result = printDialog1.ShowDialog();

			// If the result is OK then print the document.
			if (result==DialogResult.OK) document.Print();
		}

		private void document_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
		{
			//this code will be called when the PrintPreviewDialog.Show method is called
			//set the PageToPrinterMapping property of the Page. This specifies how the page 
			//is mapped onto the printer page. By default the page will be tiled 
			//get the selected mapping option
			string sPageToPrinterMapping = (string)this.comboBox1.SelectedItem; 
			if(sPageToPrinterMapping == null) 
				//if no selection has been made the default is tiling
				axPageLayoutControl1.Page.PageToPrinterMapping = esriPageToPrinterMapping.esriPageMappingTile;
			else if (sPageToPrinterMapping.Equals("esriPageMappingTile"))
				axPageLayoutControl1.Page.PageToPrinterMapping = esriPageToPrinterMapping.esriPageMappingTile;
			else if(sPageToPrinterMapping.Equals("esriPageMappingCrop"))
				axPageLayoutControl1.Page.PageToPrinterMapping = esriPageToPrinterMapping.esriPageMappingCrop;
			else if(sPageToPrinterMapping.Equals("esriPageMappingScale"))
				axPageLayoutControl1.Page.PageToPrinterMapping = esriPageToPrinterMapping.esriPageMappingScale; 
            else
				axPageLayoutControl1.Page.PageToPrinterMapping = esriPageToPrinterMapping.esriPageMappingTile;

            //get the resolution of the graphics device used by the print preview (including the graphics device)
			short dpi = (short)e.Graphics.DpiX;
			//envelope for the device boundaries
			IEnvelope devBounds = new EnvelopeClass(); 
			//get page
			IPage page = axPageLayoutControl1.Page;
      
			//the number of printer pages the page will be printed on
			short printPageCount;  
			printPageCount = axPageLayoutControl1.get_PrinterPageCount(0);
			m_CurrentPrintPage++;

			//the currently selected printer
			IPrinter printer = axPageLayoutControl1.Printer; 
			//get the device bounds of the currently selected printer
			page.GetDeviceBounds(printer, m_CurrentPrintPage, 0, dpi, devBounds);

			//structure for the device boundaries
			tagRECT deviceRect; 
			//Returns the coordinates of lower, left and upper, right corners
			double xmin,ymin,xmax,ymax;
			devBounds.QueryCoords(out xmin, out ymin, out xmax, out ymax);
			//initialize the structure for the device boundaries
			deviceRect.bottom = (int) ymax;
			deviceRect.left = (int) xmin;
			deviceRect.top = (int) ymin;
			deviceRect.right = (int) xmax;

			//determine the visible bounds of the currently printed page
			IEnvelope visBounds = new EnvelopeClass();
			page.GetPageBounds(printer, m_CurrentPrintPage, 0, visBounds);

			//get a handle to the graphics device that the print preview will be drawn to
			IntPtr hdc = e.Graphics.GetHdc();
			
			//print the page to the graphics device using the specified boundaries 
			axPageLayoutControl1.ActiveView.Output(hdc.ToInt32(), dpi, ref deviceRect, visBounds, m_TrackCancel);  

			//release the graphics device handle
			e.Graphics.ReleaseHdc(hdc);

			//check if further pages have to be printed
			if( m_CurrentPrintPage < printPageCount)
				e.HasMorePages = true; //document_PrintPage event will be called again
			else
				e.HasMorePages = false;

		}

	}  
}
