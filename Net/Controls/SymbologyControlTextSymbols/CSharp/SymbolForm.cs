using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using ESRI.ArcGIS.Display;

namespace TextSymbols
{

	public class Form2 : System.Windows.Forms.Form
	{
		private ESRI.ArcGIS.Controls.AxSymbologyControl axSymbologyControl1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.ComponentModel.Container components = null;
		private IStyleGalleryItem m_StyleGalleryItem; 

		public Form2()
		{

			InitializeComponent();

		}


		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Form2));
			this.axSymbologyControl1 = new ESRI.ArcGIS.Controls.AxSymbologyControl();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.axSymbologyControl1)).BeginInit();
			this.SuspendLayout();
			// 
			// axSymbologyControl1
			// 
			this.axSymbologyControl1.Location = new System.Drawing.Point(8, 8);
			this.axSymbologyControl1.Name = "axSymbologyControl1";
			this.axSymbologyControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axSymbologyControl1.OcxState")));
			this.axSymbologyControl1.Size = new System.Drawing.Size(265, 272);
			this.axSymbologyControl1.TabIndex = 0;
			this.axSymbologyControl1.OnItemSelected += new ESRI.ArcGIS.Controls.ISymbologyControlEvents_Ax_OnItemSelectedEventHandler(this.axSymbologyControl1_OnItemSelected);
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(120, 288);
			this.button1.Name = "button1";
			this.button1.TabIndex = 2;
			this.button1.Text = "OK";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(200, 288);
			this.button2.Name = "button2";
			this.button2.TabIndex = 3;
			this.button2.Text = "Cancel";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// Form2
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(280, 320);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.axSymbologyControl1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "Form2";
			this.Text = "Symbol Form";
			this.Load += new System.EventHandler(this.Form2_Load);
			((System.ComponentModel.ISupportInitialize)(this.axSymbologyControl1)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private void button1_Click(object sender, System.EventArgs e)
		{
			//Hide the form
			this.Hide(); 
		}


		private void button2_Click(object sender, System.EventArgs e)
		{
			m_StyleGalleryItem = null;
			//Hide the form
			this.Hide(); 
		}

	
		public IStyleGalleryItem GetItem(ESRI.ArcGIS.Controls.esriSymbologyStyleClass styleClass)
		{
			m_StyleGalleryItem = null;
			//Disable ok button
			button1.Enabled = false; 
	      
			//Set the style class
			axSymbologyControl1.StyleClass = styleClass; 
			//Unselect any selected item in the current style class
			axSymbologyControl1.GetStyleClass(styleClass).UnselectItem(); 
	                  
			//Show the modal form
			this.ShowDialog();

			//Return the selected label style
			return m_StyleGalleryItem;
		}

	    
		private void Form2_Load(object sender, System.EventArgs e)
		{
			//Get the ArcGIS install location
			string sInstall = ESRI.ArcGIS.RuntimeManager.ActiveRuntime.Path;
			//Load the ESRI.ServerStyle file into the SymbologyControl
            axSymbologyControl1.LoadStyleFile(sInstall + "\\Styles\\ESRI.ServerStyle");            
		}

		
		private void axSymbologyControl1_OnItemSelected(object sender, ESRI.ArcGIS.Controls.ISymbologyControlEvents_OnItemSelectedEvent e)
		{
			//Get the selected item
			m_StyleGalleryItem = axSymbologyControl1.GetStyleClass(axSymbologyControl1.StyleClass).GetSelectedItem();
			//Enable ok button
			button1.Enabled = true; 
		}		
	}
}
