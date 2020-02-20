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
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS;


namespace LoadMapControl
{
	public class frmMain : System.Windows.Forms.Form
	{
		public  System.Windows.Forms.TextBox txtPath;
		private System.Windows.Forms.Button cmdLoadDoc;

		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private ESRI.ArcGIS.Controls.AxPageLayoutControl axPageLayoutControl1;
		private ESRI.ArcGIS.Controls.AxLicenseControl axLicenseControl1;
		public System.ComponentModel.Container components = null;

		public frmMain()
		{
			InitializeComponent();
		}

		protected override void Dispose( bool disposing )
		{
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.txtPath = new System.Windows.Forms.TextBox();
            this.cmdLoadDoc = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.axPageLayoutControl1 = new ESRI.ArcGIS.Controls.AxPageLayoutControl();
            this.axLicenseControl1 = new ESRI.ArcGIS.Controls.AxLicenseControl();
            ((System.ComponentModel.ISupportInitialize)(this.axPageLayoutControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtPath
            // 
            this.txtPath.Enabled = false;
            this.txtPath.Location = new System.Drawing.Point(8, 40);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(352, 20);
            this.txtPath.TabIndex = 1;
            // 
            // cmdLoadDoc
            // 
            this.cmdLoadDoc.Location = new System.Drawing.Point(232, 8);
            this.cmdLoadDoc.Name = "cmdLoadDoc";
            this.cmdLoadDoc.Size = new System.Drawing.Size(128, 24);
            this.cmdLoadDoc.TabIndex = 2;
            this.cmdLoadDoc.Text = "Load Map Document";
            this.cmdLoadDoc.Click += new System.EventHandler(this.cmdLoadDoc_Click);
            // 
            // axPageLayoutControl1
            // 
            this.axPageLayoutControl1.Location = new System.Drawing.Point(8, 64);
            this.axPageLayoutControl1.Name = "axPageLayoutControl1";
            this.axPageLayoutControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axPageLayoutControl1.OcxState")));
            this.axPageLayoutControl1.Size = new System.Drawing.Size(352, 368);
            this.axPageLayoutControl1.TabIndex = 3;
            // 
            // axLicenseControl1
            // 
            this.axLicenseControl1.Enabled = true;
            this.axLicenseControl1.Location = new System.Drawing.Point(8, 2);
            this.axLicenseControl1.Name = "axLicenseControl1";
            this.axLicenseControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axLicenseControl1.OcxState")));
            this.axLicenseControl1.Size = new System.Drawing.Size(32, 32);
            this.axLicenseControl1.TabIndex = 4;
            // 
            // frmMain
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(368, 438);
            this.Controls.Add(this.axLicenseControl1);
            this.Controls.Add(this.axPageLayoutControl1);
            this.Controls.Add(this.cmdLoadDoc);
            this.Controls.Add(this.txtPath);
            this.Name = "frmMain";
            this.Text = "Load Map Document";
            ((System.ComponentModel.ISupportInitialize)(this.axPageLayoutControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

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
            Application.Run( new frmMain());
		}

		private void cmdLoadDoc_Click(object sender, System.EventArgs e)
		{
			//Open a file dialog for selecting map documents
			openFileDialog1.Title = "Browse Map Document";
			openFileDialog1.Filter = "Map Documents (*.mxd, *.mxt, *.pmf)|*.pmf; *.mxt; *.mxd";
			openFileDialog1.ShowDialog();

			//Exit if no map document is selected
			string sFilePath = openFileDialog1.FileName;
			if (sFilePath == "") return;

			bool bPass, bIsMapDoc;
			IMapDocument ipMapDoc;
			ipMapDoc = new MapDocumentClass();

			//Check if the map document is password protected
			bPass = ipMapDoc.get_IsPasswordProtected(sFilePath);

			if(bPass)
			{
				//Disable the main form
				this.Enabled = false;

				//Show the password dialog
				frmPassword Form2 = new frmPassword();
				Form2.ShowDialog (this);
				int check = Form2.Check; 
					
				//OK button pressed					
				if (check == 1)
				{
					try
					{
						//Set a waiting cursor
						Cursor.Current = Cursors.WaitCursor;
								
						//Load the password protected map
						axPageLayoutControl1.LoadMxFile(sFilePath, Form2.Password); 
						txtPath.Text = sFilePath;
						this.Enabled = true;

						//Set a default cursor
						Cursor.Current = Cursors.Default; 
					}
					catch
					{
						this.Enabled = true;
						MessageBox.Show("The Password was incorrect!");
					}
				}
				else
				{
					this.Enabled = true;
				}
			}
			else  
			{
				//Check whether the file is a map document
				bIsMapDoc = axPageLayoutControl1.CheckMxFile(sFilePath);
				
				if(bIsMapDoc)
				{
					Cursor.Current = Cursors.WaitCursor;
					
					//Load the Mx document	
					axPageLayoutControl1.LoadMxFile(sFilePath, Type.Missing); 
					txtPath.Text = sFilePath;
					//Set a default cursor
					Cursor.Current = Cursors.Default; 
				}
				else
				{
					MessageBox.Show(sFilePath + " is not a valid ArcMap document");
					sFilePath = "";
				}
			}
		}

	}
}
