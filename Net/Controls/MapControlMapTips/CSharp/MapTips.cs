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
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS;


namespace MapTips
{
	public class Form1 : System.Windows.Forms.Form
	{
		public System.Windows.Forms.CheckBox chkShowTips;
		public System.Windows.Forms.Button cmdLoadData;
		public System.Windows.Forms.ComboBox cboDataField;
		public System.Windows.Forms.ComboBox cboDataLayer;
		public System.Windows.Forms.Label Label2;
		public System.Windows.Forms.Label Label1;
		public System.Windows.Forms.Button cmdFullExtent;
		public System.Windows.Forms.Label Label3;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private ESRI.ArcGIS.Controls.AxMapControl axMapControl1;
		private ESRI.ArcGIS.Controls.AxLicenseControl axLicenseControl1;
        internal CheckBox chkTransparent;
		private System.ComponentModel.IContainer components;

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
            this.chkShowTips = new System.Windows.Forms.CheckBox();
            this.cmdLoadData = new System.Windows.Forms.Button();
            this.cboDataField = new System.Windows.Forms.ComboBox();
            this.cboDataLayer = new System.Windows.Forms.ComboBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.cmdFullExtent = new System.Windows.Forms.Button();
            this.Label3 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.axMapControl1 = new ESRI.ArcGIS.Controls.AxMapControl();
            this.axLicenseControl1 = new ESRI.ArcGIS.Controls.AxLicenseControl();
            this.chkTransparent = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // chkShowTips
            // 
            this.chkShowTips.BackColor = System.Drawing.SystemColors.Control;
            this.chkShowTips.Cursor = System.Windows.Forms.Cursors.Default;
            this.chkShowTips.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkShowTips.ForeColor = System.Drawing.SystemColors.ControlText;
            this.chkShowTips.Location = new System.Drawing.Point(8, 40);
            this.chkShowTips.Name = "chkShowTips";
            this.chkShowTips.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.chkShowTips.Size = new System.Drawing.Size(129, 25);
            this.chkShowTips.TabIndex = 10;
            this.chkShowTips.Text = "Show Map Tips";
            this.chkShowTips.UseVisualStyleBackColor = false;
            this.chkShowTips.CheckedChanged += new System.EventHandler(this.chkShowTips_CheckedChanged);
            this.chkShowTips.CheckStateChanged += new System.EventHandler(this.chkShowTips_CheckStateChanged);
            // 
            // cmdLoadData
            // 
            this.cmdLoadData.BackColor = System.Drawing.SystemColors.Control;
            this.cmdLoadData.Cursor = System.Windows.Forms.Cursors.Default;
            this.cmdLoadData.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdLoadData.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdLoadData.Location = new System.Drawing.Point(8, 8);
            this.cmdLoadData.Name = "cmdLoadData";
            this.cmdLoadData.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmdLoadData.Size = new System.Drawing.Size(113, 25);
            this.cmdLoadData.TabIndex = 9;
            this.cmdLoadData.Text = "Load Document...";
            this.cmdLoadData.UseVisualStyleBackColor = false;
            this.cmdLoadData.Click += new System.EventHandler(this.cmdLoadData_Click);
            // 
            // cboDataField
            // 
            this.cboDataField.BackColor = System.Drawing.SystemColors.Window;
            this.cboDataField.Cursor = System.Windows.Forms.Cursors.Default;
            this.cboDataField.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboDataField.ForeColor = System.Drawing.SystemColors.WindowText;
            this.cboDataField.Location = new System.Drawing.Point(176, 40);
            this.cboDataField.Name = "cboDataField";
            this.cboDataField.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cboDataField.Size = new System.Drawing.Size(161, 22);
            this.cboDataField.TabIndex = 8;
            this.cboDataField.SelectedIndexChanged += new System.EventHandler(this.cboDataField_SelectedIndexChanged);
            // 
            // cboDataLayer
            // 
            this.cboDataLayer.BackColor = System.Drawing.SystemColors.Window;
            this.cboDataLayer.Cursor = System.Windows.Forms.Cursors.Default;
            this.cboDataLayer.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboDataLayer.ForeColor = System.Drawing.SystemColors.WindowText;
            this.cboDataLayer.Location = new System.Drawing.Point(176, 16);
            this.cboDataLayer.Name = "cboDataLayer";
            this.cboDataLayer.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cboDataLayer.Size = new System.Drawing.Size(161, 22);
            this.cboDataLayer.TabIndex = 7;
            this.cboDataLayer.SelectedIndexChanged += new System.EventHandler(this.cboDataLayer_SelectedIndexChanged);
            // 
            // Label2
            // 
            this.Label2.BackColor = System.Drawing.SystemColors.Control;
            this.Label2.Cursor = System.Windows.Forms.Cursors.Default;
            this.Label2.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Label2.Location = new System.Drawing.Point(136, 40);
            this.Label2.Name = "Label2";
            this.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Label2.Size = new System.Drawing.Size(33, 17);
            this.Label2.TabIndex = 12;
            this.Label2.Text = "Fields:";
            // 
            // Label1
            // 
            this.Label1.BackColor = System.Drawing.SystemColors.Control;
            this.Label1.Cursor = System.Windows.Forms.Cursors.Default;
            this.Label1.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Label1.Location = new System.Drawing.Point(136, 16);
            this.Label1.Name = "Label1";
            this.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Label1.Size = new System.Drawing.Size(41, 17);
            this.Label1.TabIndex = 11;
            this.Label1.Text = "Layers:";
            // 
            // cmdFullExtent
            // 
            this.cmdFullExtent.BackColor = System.Drawing.SystemColors.Control;
            this.cmdFullExtent.Cursor = System.Windows.Forms.Cursors.Default;
            this.cmdFullExtent.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdFullExtent.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdFullExtent.Location = new System.Drawing.Point(224, 365);
            this.cmdFullExtent.Name = "cmdFullExtent";
            this.cmdFullExtent.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmdFullExtent.Size = new System.Drawing.Size(113, 25);
            this.cmdFullExtent.TabIndex = 13;
            this.cmdFullExtent.Text = "Zoom to Full Extent";
            this.cmdFullExtent.UseVisualStyleBackColor = false;
            this.cmdFullExtent.Click += new System.EventHandler(this.cmdFullExtent_Click);
            // 
            // Label3
            // 
            this.Label3.BackColor = System.Drawing.SystemColors.Control;
            this.Label3.Cursor = System.Windows.Forms.Cursors.Default;
            this.Label3.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Label3.Location = new System.Drawing.Point(8, 373);
            this.Label3.Name = "Label3";
            this.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Label3.Size = new System.Drawing.Size(209, 17);
            this.Label3.TabIndex = 14;
            this.Label3.Text = "Left mouse button to zoomin, right to pan";
            // 
            // axMapControl1
            // 
            this.axMapControl1.Location = new System.Drawing.Point(8, 88);
            this.axMapControl1.Name = "axMapControl1";
            this.axMapControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axMapControl1.OcxState")));
            this.axMapControl1.Size = new System.Drawing.Size(328, 272);
            this.axMapControl1.TabIndex = 15;
            this.axMapControl1.OnMouseDown += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMouseDownEventHandler(this.axMapControl1_OnMouseDown);
            // 
            // axLicenseControl1
            // 
            this.axLicenseControl1.Enabled = true;
            this.axLicenseControl1.Location = new System.Drawing.Point(300, 68);
            this.axLicenseControl1.Name = "axLicenseControl1";
            this.axLicenseControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axLicenseControl1.OcxState")));
            this.axLicenseControl1.Size = new System.Drawing.Size(32, 32);
            this.axLicenseControl1.TabIndex = 16;
            // 
            // chkTransparent
            // 
            this.chkTransparent.AutoSize = true;
            this.chkTransparent.Location = new System.Drawing.Point(8, 65);
            this.chkTransparent.Name = "chkTransparent";
            this.chkTransparent.Size = new System.Drawing.Size(106, 17);
            this.chkTransparent.TabIndex = 17;
            this.chkTransparent.Text = "Transparent Tips";
            this.chkTransparent.UseVisualStyleBackColor = true;
            this.chkTransparent.CheckedChanged += new System.EventHandler(this.chkTransparent_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(344, 400);
            this.Controls.Add(this.chkTransparent);
            this.Controls.Add(this.axLicenseControl1);
            this.Controls.Add(this.axMapControl1);
            this.Controls.Add(this.cmdFullExtent);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.chkShowTips);
            this.Controls.Add(this.cmdLoadData);
            this.Controls.Add(this.cboDataField);
            this.Controls.Add(this.cboDataLayer);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.Label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl1)).EndInit();
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
			//Disable controls
			chkShowTips.Enabled = false;
            chkTransparent.Enabled = false;
			cboDataLayer.Enabled = false;
			cboDataField.Enabled = false;
		}

		private void cboDataLayer_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			//Disable field combo if feature layer is not selected and exit
			if (axMapControl1.get_Layer(cboDataLayer.SelectedIndex) is IFeatureLayer == false)
			{
				cboDataField.Items.Clear();
				cboDataField.Enabled = false;
				return;
			}

			//Get IFeatureLayer interface
			IFeatureLayer featureLayer = (IFeatureLayer) axMapControl1.get_Layer(cboDataLayer.SelectedIndex);
			//Query interface for ILayerFields
			ILayerFields layerFields = (ILayerFields) featureLayer;

			int j = 0;
			cboDataField.Items.Clear();
			cboDataField.Enabled = true;
			//Loop through the fields
			for (int i = 0; i <= layerFields.FieldCount - 1; i++)
			{
				//Get IField interface
				IField field = layerFields.get_Field(i);
				//If the field is not the shape field
				if (field.Type != esriFieldType.esriFieldTypeGeometry)
				{
					//Add field name to the control
					cboDataField.Items.Insert(j, field.Name);
					//If the field name is the display field
					if (field.Name == featureLayer.DisplayField)
					{
						//Select the field name in the control
						cboDataField.SelectedIndex = j;
					}
					j = j + 1;
				}
			}
			ShowLayerTips();
		}

		private void chkShowTips_CheckStateChanged(object sender, System.EventArgs e)
		{
			ShowLayerTips();
		}

		private void cmdFullExtent_Click(object sender, System.EventArgs e)
		{
			//Zoom to full extent of data
			axMapControl1.Extent = axMapControl1.FullExtent;
		}

		private void cmdLoadData_Click(object sender, System.EventArgs e)
		{
			openFileDialog1.Title = "Browse Map Document";
			openFileDialog1.Filter = "Map Documents (*.mxd)|*.mxd";
			openFileDialog1.ShowDialog();

			//Exit if no map document is selected
			string sFilePath = openFileDialog1.FileName;
			if (sFilePath == "")
			{
				return;
			}

			//Validate and load map document
			if (axMapControl1.CheckMxFile(sFilePath)== true)
			{
				axMapControl1.LoadMxFile(sFilePath,Type.Missing,"");
				//Enabled MapControl
				axMapControl1.Enabled = true;
			}
			else
			{
				MessageBox.Show(sFilePath + " is not a valid ArcMap document");
					return;
			}

			//Add the layer names to combo
			cboDataLayer.Items.Clear();
			for (int i = 0; i <= axMapControl1.LayerCount - 1; i++)
			{
				cboDataLayer.Items.Insert(i, axMapControl1.get_Layer(i).Name);
			}

			//Select first layer in control
			cboDataLayer.SelectedIndex = 0;
			//Enable controls if disabled
			if (chkShowTips.Enabled == false) chkShowTips.Enabled = true;
            if (chkTransparent.Enabled == false) chkTransparent.Enabled = true;
			if (cboDataLayer.Enabled == false) cboDataLayer.Enabled = true;
			if (cboDataField.Enabled == false) cboDataField.Enabled = true;
		}

		private void ShowLayerTips()
		{
			//Loop through the maps layers
			for (int i = 0; i <= axMapControl1.LayerCount - 1; i++)
			{
				//Get ILayer interface
				ILayer layer = axMapControl1.get_Layer(i);
				//If is the layer selected in the control
				if (cboDataLayer.SelectedIndex == i)
				{
					//If want to show map tips
					if (chkShowTips.CheckState == CheckState.Checked)
					{
						layer.ShowTips = true;
					}
					else
					{
						layer.ShowTips = false;
					}
				}
				else
				{
					layer.ShowTips = false;
				}
			}
		}

		private void cboDataField_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			//Get IFeatureLayer interface 
			IFeatureLayer featureLayer = (IFeatureLayer) axMapControl1.get_Layer(cboDataLayer.SelectedIndex);
			//Query interface for IlayerFields
			ILayerFields layerFields = (ILayerFields) featureLayer;

			//Loop through the fields
			for (int i = 0; i <= layerFields.FieldCount - 1; i++)
			{
				//Get IField interface
				IField field = layerFields.get_Field(i);
				//If the field name is the name selected in the control
				if (field.Name == cboDataField.Text)
				{
					//Set the field as the display field
					featureLayer.DisplayField = field.Name;
					break;
				}
			}
		}

		private void axMapControl1_OnMouseDown(object sender, ESRI.ArcGIS.Controls.IMapControlEvents2_OnMouseDownEvent e)
		{
			//If left mouse button zoom in
			if (e.button == 1)
			{
				axMapControl1.Extent = axMapControl1.TrackRectangle();
			}
				//If right mouse button pan
			else if (e.button == 2)
			{
				axMapControl1.Pan();
			}
		}

        private void chkShowTips_CheckedChanged(object sender, EventArgs e)
        {
            if (chkShowTips.CheckState == CheckState.Checked)
                axMapControl1.ShowMapTips = true;
            else
                axMapControl1.ShowMapTips = false;
        }

        private void chkTransparent_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTransparent.CheckState == CheckState.Checked)
                axMapControl1.TipStyle = esriTipStyle.esriTipStyleTransparent;
            else
                axMapControl1.TipStyle = esriTipStyle.esriTipStyleSolid;
        }

	}
}
