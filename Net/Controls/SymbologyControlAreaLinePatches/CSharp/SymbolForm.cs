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

using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Controls;

namespace AreaLinePatches
{
	public class Form2 : System.Windows.Forms.Form
	{
	
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private ESRI.ArcGIS.Controls.AxSymbologyControl axSymbologyControl1;

		private IStyleGalleryItem m_styleGalleryItem;

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
			this.label1 = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.axSymbologyControl1 = new ESRI.ArcGIS.Controls.AxSymbologyControl();
			((System.ComponentModel.ISupportInitialize)(this.axSymbologyControl1)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(0, 8);
			this.label1.Name = "label1";
			this.label1.TabIndex = 4;
			this.label1.Text = "Patches:";
			// 
			// button1
			// 
			this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.button1.Location = new System.Drawing.Point(152, 312);
			this.button1.Name = "button1";
			this.button1.TabIndex = 6;
			this.button1.Text = "OK";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.button2.Location = new System.Drawing.Point(232, 312);
			this.button2.Name = "button2";
			this.button2.TabIndex = 7;
			this.button2.Text = "Cancel";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// axSymbologyControl1
			// 
			this.axSymbologyControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.axSymbologyControl1.Location = new System.Drawing.Point(0, 32);
			this.axSymbologyControl1.Name = "axSymbologyControl1";
			this.axSymbologyControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axSymbologyControl1.OcxState")));
			this.axSymbologyControl1.Size = new System.Drawing.Size(312, 272);
			this.axSymbologyControl1.TabIndex = 8;
			this.axSymbologyControl1.OnItemSelected += new ESRI.ArcGIS.Controls.ISymbologyControlEvents_Ax_OnItemSelectedEventHandler(this.axSymbologyControl1_OnItemSelected);
			// 
			// Form2
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(312, 344);
			this.Controls.Add(this.axSymbologyControl1);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "Form2";
			this.Text = "Symbol Form";
			this.Load += new System.EventHandler(this.Form2_Load);
			((System.ComponentModel.ISupportInitialize)(this.axSymbologyControl1)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private void Form2_Load(object sender, System.EventArgs e)
		{
			//Get the ArcGIS install location
            string sInstall = ESRI.ArcGIS.RuntimeManager.ActiveRuntime.Path;
      
			//Load the ESRI.ServerStyle file into the SymbologyControl
			axSymbologyControl1.LoadStyleFile(sInstall + "\\Styles\\ESRI.ServerStyle");
		}		

		public IStyleGalleryItem GetItem(ESRI.ArcGIS.Controls.esriSymbologyStyleClass styleClass)
		{
			//Retrieve the selected area/line patch style from the SymbologyControl
			m_styleGalleryItem = null;
			//disable ok button
			button1.Enabled = false; 
      
			//Set the style class of SymbologyControl1
			axSymbologyControl1.StyleClass = styleClass; 
			//Unselect any selected item in the current style class
			axSymbologyControl1.GetStyleClass(styleClass).UnselectItem(); 
      
			//Show the modal form
			this.ShowDialog();

			//return the label style that has been selected from the SymbologyControl
			return m_styleGalleryItem;
		}

		private void axSymbologyControl1_OnItemSelected(object sender, ESRI.ArcGIS.Controls.ISymbologyControlEvents_OnItemSelectedEvent e)
		{
			//Get the selected item
			m_styleGalleryItem = axSymbologyControl1.GetStyleClass(axSymbologyControl1.StyleClass).GetSelectedItem();
			//enable ok button
			button1.Enabled = true; 
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			//hide the form
			this.Hide(); 
		}

		private void button2_Click(object sender, System.EventArgs e)
		{
			m_styleGalleryItem = null;
			//hide the form
			this.Hide(); 
		}
    
	}
}
