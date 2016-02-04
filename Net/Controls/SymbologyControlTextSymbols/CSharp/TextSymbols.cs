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

using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS;
namespace TextSymbols
{
	public class Form1 : System.Windows.Forms.Form
	{
		private ESRI.ArcGIS.Controls.AxToolbarControl axToolbarControl1;
		private ESRI.ArcGIS.Controls.AxPageLayoutControl axPageLayoutControl1;
		private System.Windows.Forms.Button button1;
		private System.ComponentModel.Container components = null;
		private ESRI.ArcGIS.Controls.AxLicenseControl axLicenseControl1;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.ComboBox comboBox1;
		internal System.Windows.Forms.Label Label1;
        internal Label label2;
		private ITextSymbol m_textSymbol; 

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.axToolbarControl1 = new ESRI.ArcGIS.Controls.AxToolbarControl();
            this.axPageLayoutControl1 = new ESRI.ArcGIS.Controls.AxPageLayoutControl();
            this.button1 = new System.Windows.Forms.Button();
            this.axLicenseControl1 = new ESRI.ArcGIS.Controls.AxLicenseControl();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.axToolbarControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axPageLayoutControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // axToolbarControl1
            // 
            this.axToolbarControl1.Location = new System.Drawing.Point(8, 8);
            this.axToolbarControl1.Name = "axToolbarControl1";
            this.axToolbarControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axToolbarControl1.OcxState")));
            this.axToolbarControl1.Size = new System.Drawing.Size(200, 28);
            this.axToolbarControl1.TabIndex = 0;
            // 
            // axPageLayoutControl1
            // 
            this.axPageLayoutControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.axPageLayoutControl1.Location = new System.Drawing.Point(0, 56);
            this.axPageLayoutControl1.Name = "axPageLayoutControl1";
            this.axPageLayoutControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axPageLayoutControl1.OcxState")));
            this.axPageLayoutControl1.Size = new System.Drawing.Size(640, 328);
            this.axPageLayoutControl1.TabIndex = 1;
            this.axPageLayoutControl1.OnMouseDown += new ESRI.ArcGIS.Controls.IPageLayoutControlEvents_Ax_OnMouseDownEventHandler(this.axPageLayoutControl1_OnMouseDown);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(216, 8);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(128, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Select Text Symbol";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // axLicenseControl1
            // 
            this.axLicenseControl1.Enabled = true;
            this.axLicenseControl1.Location = new System.Drawing.Point(80, 32);
            this.axLicenseControl1.Name = "axLicenseControl1";
            this.axLicenseControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axLicenseControl1.OcxState")));
            this.axLicenseControl1.Size = new System.Drawing.Size(32, 32);
            this.axLicenseControl1.TabIndex = 3;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(352, 8);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(192, 20);
            this.textBox1.TabIndex = 4;
            this.textBox1.Text = "TextElement with selected TextSymbol";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.Location = new System.Drawing.Point(552, 8);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(80, 21);
            this.comboBox1.TabIndex = 5;
            // 
            // Label1
            // 
            this.Label1.Location = new System.Drawing.Point(351, 38);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(296, 16);
            this.Label1.TabIndex = 7;
            this.Label1.Text = "2) Right click on the display to add a text element";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(214, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(296, 16);
            this.label2.TabIndex = 8;
            this.label2.Text = "1) Select a text symbol";
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(640, 382);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.axLicenseControl1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.axPageLayoutControl1);
            this.Controls.Add(this.axToolbarControl1);
            this.Controls.Add(this.label2);
            this.Name = "Form1";
            this.Text = "Change Text Symbol";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.axToolbarControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axPageLayoutControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).EndInit();
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

		private void Form1_Load(object sender, System.EventArgs e)
		{
			//Set buddy control
			axToolbarControl1.SetBuddyControl(this.axPageLayoutControl1);
      
			//Add ToolbarControl items
			axToolbarControl1.AddItem("esriControls.ControlsOpenDocCommand");
			axToolbarControl1.AddItem("esriControls.ControlsPageZoomInTool");
			axToolbarControl1.AddItem("esriControls.ControlsPageZoomOutTool");
			axToolbarControl1.AddItem("esriControls.ControlsPageZoomWholePageCommand");
			axToolbarControl1.AddItem("esriControls.ControlsSelectTool");

			//Add values for the text size to the combo box
			comboBox1.Items.Add("8pt");
			comboBox1.Items.Add("10pt");
			comboBox1.Items.Add("12pt");
			comboBox1.Items.Add("14pt");
			comboBox1.SelectedIndex = 0;
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			//Create a new SymbolForm
			Form2 symbolForm = new Form2();

			//Get the IStyleGalleryItem that has been selected in the SymbologyControl
			IStyleGalleryItem styleGalleryItem = symbolForm.GetItem(esriSymbologyStyleClass.esriStyleClassTextSymbols);
			if (styleGalleryItem == null) return;
				
			//Set the TextSymbol
			m_textSymbol = (ITextSymbol) styleGalleryItem.Item;

			//Release the SymbolForm
			symbolForm.Dispose();
    
		}

		private void axPageLayoutControl1_OnMouseDown(object sender, ESRI.ArcGIS.Controls.IPageLayoutControlEvents_OnMouseDownEvent e)
		{
			//Check if the right button of the mouse was clicked
			if (e.button != 2) return;
			//Ensure a text symbol has been selected
			if (m_textSymbol == null) return;

			//Create a point and set its coordinates
			IPoint point = new PointClass();
			point.X = e.pageX; 
			point.Y = e.pageY;

			//Create a text element
			ITextElement textElement = new TextElementClass();
			textElement.Text = textBox1.Text;

			//Set the size of the text
			if (comboBox1.SelectedItem.ToString() == ("8pt"))
				m_textSymbol.Size = 8.0;
			else if (comboBox1.SelectedItem.ToString() == ("10pt")) 
				m_textSymbol.Size = 10.0;
			else if (comboBox1.SelectedItem.ToString() == ("12pt"))
				m_textSymbol.Size = 12.0;
			else if (comboBox1.SelectedItem.ToString() == ("14pt")) 
				m_textSymbol.Size = 14.0;

			//Set the TextElement symbol to that of the selected text symbol
			textElement.Symbol = m_textSymbol;
			textElement.ScaleText = true;

			//QI to IElment
			IElement element = (IElement) textElement;
			//Set the TextElement's geometry
			element.Geometry = point;

			//Add the element to the GraphicsContainer
			axPageLayoutControl1.ActiveView.GraphicsContainer.AddElement(element, 0);
			//Refresh the PageLayout
			axPageLayoutControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, element, null);
		}
	}
}
