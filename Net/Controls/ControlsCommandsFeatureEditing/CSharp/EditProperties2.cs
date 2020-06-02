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
using ESRI.ArcGIS.Controls;

namespace EditPropertiesDialog
{

	public class EditProperties2 : System.Windows.Forms.Form
	{
		private System.ComponentModel.Container components = null;

		public EditProperties2()
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
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.label4 = new System.Windows.Forms.Label();
      this.label5 = new System.Windows.Forms.Label();
      this.label6 = new System.Windows.Forms.Label();
      this.label7 = new System.Windows.Forms.Label();
      this.label8 = new System.Windows.Forms.Label();
      this.txtOffset = new System.Windows.Forms.TextBox();
      this.txtPrecision = new System.Windows.Forms.TextBox();
      this.cboType = new System.Windows.Forms.ComboBox();
      this.cboUnits = new System.Windows.Forms.ComboBox();
      this.txtFactor = new System.Windows.Forms.TextBox();
      this.chkSnapTips = new System.Windows.Forms.CheckBox();
      this.chkGrid = new System.Windows.Forms.CheckBox();
      this.txtTolerance = new System.Windows.Forms.TextBox();
      this.SuspendLayout();
      // 
      // label1
      // 
      this.label1.Location = new System.Drawing.Point(8, 16);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(136, 16);
      this.label1.TabIndex = 0;
      this.label1.Text = "Angular Correction Offset:";
      // 
      // label2
      // 
      this.label2.Location = new System.Drawing.Point(8, 48);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(120, 16);
      this.label2.TabIndex = 1;
      this.label2.Text = "Angular Unit Precision:";
      // 
      // label3
      // 
      this.label3.Location = new System.Drawing.Point(8, 80);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(88, 16);
      this.label3.TabIndex = 2;
      this.label3.Text = "Direction Type:";
      // 
      // label4
      // 
      this.label4.Location = new System.Drawing.Point(8, 112);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(88, 16);
      this.label4.TabIndex = 3;
      this.label4.Text = "Direction Units:";
      // 
      // label5
      // 
      this.label5.Location = new System.Drawing.Point(8, 144);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(144, 16);
      this.label5.TabIndex = 4;
      this.label5.Text = "Distance Correction Factor:";
      // 
      // label6
      // 
      this.label6.Location = new System.Drawing.Point(8, 176);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(88, 16);
      this.label6.TabIndex = 5;
      this.label6.Text = "Snap Tips:";
      // 
      // label7
      // 
      this.label7.Location = new System.Drawing.Point(8, 208);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(136, 16);
      this.label7.TabIndex = 6;
      this.label7.Text = "Sticky Move Tolerance:";
      // 
      // label8
      // 
      this.label8.Location = new System.Drawing.Point(8, 240);
      this.label8.Name = "label8";
      this.label8.Size = new System.Drawing.Size(120, 16);
      this.label8.TabIndex = 7;
      this.label8.Text = "Use Ground to Grid:";
      // 
      // txtOffset
      // 
      this.txtOffset.Location = new System.Drawing.Point(152, 16);
      this.txtOffset.Name = "txtOffset";
      this.txtOffset.Size = new System.Drawing.Size(80, 20);
      this.txtOffset.TabIndex = 8;
      this.txtOffset.Text = "";
      this.txtOffset.TextChanged += new System.EventHandler(this.txtOffset_TextChanged);
      // 
      // txtPrecision
      // 
      this.txtPrecision.Location = new System.Drawing.Point(152, 48);
      this.txtPrecision.Name = "txtPrecision";
      this.txtPrecision.Size = new System.Drawing.Size(80, 20);
      this.txtPrecision.TabIndex = 9;
      this.txtPrecision.Text = "";
      this.txtPrecision.TextChanged += new System.EventHandler(this.txtPrecision_TextChanged);
      // 
      // cboType
      // 
      this.cboType.Items.AddRange(new object[] {
                                                 "North Azimuth",
                                                 "South Azimuth",
                                                 "Polar",
                                                 "Quadrant Bearing"});
      this.cboType.Location = new System.Drawing.Point(96, 80);
      this.cboType.Name = "cboType";
      this.cboType.Size = new System.Drawing.Size(136, 21);
      this.cboType.TabIndex = 10;
      // 
      // cboUnits
      // 
      this.cboUnits.Items.AddRange(new object[] {
                                                  "Radians",
                                                  "Decimal Degrees",
                                                  "Degrees Minutes Seconds",
                                                  "Gradians",
                                                  "Gons"});
      this.cboUnits.Location = new System.Drawing.Point(96, 112);
      this.cboUnits.Name = "cboUnits";
      this.cboUnits.Size = new System.Drawing.Size(136, 21);
      this.cboUnits.TabIndex = 11;
      // 
      // txtFactor
      // 
      this.txtFactor.Location = new System.Drawing.Point(152, 144);
      this.txtFactor.Name = "txtFactor";
      this.txtFactor.Size = new System.Drawing.Size(80, 20);
      this.txtFactor.TabIndex = 12;
      this.txtFactor.Text = "";
      this.txtFactor.TextChanged += new System.EventHandler(this.txtFactor_TextChanged);
      // 
      // chkSnapTips
      // 
      this.chkSnapTips.Location = new System.Drawing.Point(152, 168);
      this.chkSnapTips.Name = "chkSnapTips";
      this.chkSnapTips.Size = new System.Drawing.Size(24, 32);
      this.chkSnapTips.TabIndex = 13;
      // 
      // chkGrid
      // 
      this.chkGrid.Location = new System.Drawing.Point(152, 232);
      this.chkGrid.Name = "chkGrid";
      this.chkGrid.Size = new System.Drawing.Size(24, 32);
      this.chkGrid.TabIndex = 14;
      // 
      // txtTolerance
      // 
      this.txtTolerance.Location = new System.Drawing.Point(152, 208);
      this.txtTolerance.Name = "txtTolerance";
      this.txtTolerance.Size = new System.Drawing.Size(80, 20);
      this.txtTolerance.TabIndex = 15;
      this.txtTolerance.Text = "";
      this.txtTolerance.TextChanged += new System.EventHandler(this.txtTolerance_TextChanged);
      // 
      // EditProperties2
      // 
      this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
      this.ClientSize = new System.Drawing.Size(242, 270);
      this.Controls.Add(this.txtTolerance);
      this.Controls.Add(this.chkGrid);
      this.Controls.Add(this.chkSnapTips);
      this.Controls.Add(this.txtFactor);
      this.Controls.Add(this.cboUnits);
      this.Controls.Add(this.cboType);
      this.Controls.Add(this.txtPrecision);
      this.Controls.Add(this.txtOffset);
      this.Controls.Add(this.label8);
      this.Controls.Add(this.label7);
      this.Controls.Add(this.label6);
      this.Controls.Add(this.label5);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.label1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "EditProperties2";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Edit Properties 2";
      this.Closing += new System.ComponentModel.CancelEventHandler(this.EditProperties2_Closing);
      this.Load += new System.EventHandler(this.EditProperties2_Load);
      this.ResumeLayout(false);

    }
		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TextBox txtOffset;
		private System.Windows.Forms.TextBox txtPrecision;
		private System.Windows.Forms.ComboBox cboType;
		private System.Windows.Forms.ComboBox cboUnits;
		private System.Windows.Forms.TextBox txtFactor;
		private System.Windows.Forms.CheckBox chkSnapTips;
		private System.Windows.Forms.CheckBox chkGrid;
		private System.Windows.Forms.TextBox txtTolerance;
		private IEngineEditProperties2 m_engineEditProperties2 = new EngineEditorClass();

		private void EditProperties2_Load(object sender, System.EventArgs e)
		{
			//Populate form with current IEngineProperties2 values 
			txtOffset.Text = m_engineEditProperties2.AngularCorrectionOffset.ToString();
			txtPrecision.Text = m_engineEditProperties2.AngularUnitPrecision.ToString();
			txtFactor.Text = m_engineEditProperties2.DistanceCorrectionFactor.ToString();
			txtTolerance.Text = m_engineEditProperties2.StickyMoveTolerance.ToString();

			if (m_engineEditProperties2.SnapTips)
				chkSnapTips.Checked = true;
			else
				chkSnapTips.Checked = false;

			if (m_engineEditProperties2.UseGroundToGrid)
				chkGrid.Checked = true;
			else
				chkGrid.Checked = false;

			//Select current direction type
			esriEngineDirectionType type = m_engineEditProperties2.DirectionType;
			switch (type.ToString())
			{
				case "esriEngineDTNorthAzimuth":
					cboType.SelectedItem = "North Azimuth";
					break;
				case "esriEngineDTSouthAzimuth":
					cboType.SelectedItem = "South Azimuth";
					break;
				case "esriEngineDTPolar":
					cboType.SelectedItem = "Polar";
					break;
				case "esriEngineDTQuadrantBearing":
					cboType.SelectedItem = "Quadrant Bearing";
					break;
				default:
					break;
			}

			//Select current direction units
			esriEngineDirectionUnits units = m_engineEditProperties2.DirectionUnits;
			switch (units.ToString())
			{
				case "esriEngineDURadians":
					cboUnits.SelectedItem = "Radians";
					break;
				case "esriEngineDUDecimalDegrees":
					cboUnits.SelectedItem = "Decimal Degrees";
					break;
				case "esriEngineDUDegreesMinutesSeconds":
					cboUnits.SelectedItem = "Degrees Minutes Seconds";
					break;
				case "esriEngineDUGradians":
					cboUnits.SelectedItem = "Gradians";
					break;
				case "esriEngineDUGons":
					cboUnits.SelectedItem = "Gons";
					break;
				default:
					break;
			}
		}

		private void EditProperties2_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			//Update Offset property
			if (txtOffset.Text != "")
				m_engineEditProperties2.AngularCorrectionOffset = Convert.ToInt32(txtOffset.Text);

			//Update Precision property
			if (txtPrecision.Text != "")
				m_engineEditProperties2.AngularUnitPrecision = Convert.ToInt32(txtPrecision.Text);

			//Update Distance Correction Factor property
			if (txtFactor.Text != "")
				m_engineEditProperties2.DistanceCorrectionFactor = Convert.ToInt32(txtFactor.Text);

			//Update Tolerance property
			if (txtTolerance.Text != "")
				m_engineEditProperties2.StickyMoveTolerance = Convert.ToInt32(txtTolerance.Text);

			//Update Snap Tips property
			if (chkSnapTips.Checked)
				m_engineEditProperties2.SnapTips = true;
			else
				m_engineEditProperties2.SnapTips = false;

			//Update Grid property
			if (chkGrid.Checked)
				m_engineEditProperties2.UseGroundToGrid = true;
			else
				m_engineEditProperties2.UseGroundToGrid = false;

			//Set Direction Type property
			string type = cboType.SelectedItem.ToString();
			switch (type)
			{
				case "North Azimuth":
					m_engineEditProperties2.DirectionType = esriEngineDirectionType.esriEngineDTNorthAzimuth;
					break;
				case "South Azimuth":
					m_engineEditProperties2.DirectionType = esriEngineDirectionType.esriEngineDTSouthAzimuth;
					break;
				case "Polar":
					m_engineEditProperties2.DirectionType = esriEngineDirectionType.esriEngineDTPolar;
					break;
				case "Quadrant Bearing":
					m_engineEditProperties2.DirectionType = esriEngineDirectionType.esriEngineDTQuadrantBearing;
					break;
				default:
					break;
			}

			//Set Direction Units property
			string units = cboUnits.SelectedItem.ToString();
			switch (units)
			{
				case "Radians":
					m_engineEditProperties2.DirectionUnits = esriEngineDirectionUnits.esriEngineDURadians;
					break;
				case "Decimal Degrees":
					m_engineEditProperties2.DirectionUnits = esriEngineDirectionUnits.esriEngineDUDecimalDegrees;
					break;
				case "Degrees Minutes Seconds":
					m_engineEditProperties2.DirectionUnits = esriEngineDirectionUnits.esriEngineDUDegreesMinutesSeconds;
					break;
				case "Gradians":
					m_engineEditProperties2.DirectionUnits = esriEngineDirectionUnits.esriEngineDUGradians;
					break;
				case "Gons":
					m_engineEditProperties2.DirectionUnits = esriEngineDirectionUnits.esriEngineDUGons;
					break;
				default:
					break;
			}
		}


		private void txtOffset_TextChanged(object sender, System.EventArgs e)
		{
			//Validate offset
			try
			{
				if (txtOffset.Text != "")
				Convert.ToInt32(txtOffset.Text);
			}
			catch (FormatException)
			{
				MessageBox.Show("Correction offset should be a numeric value", "Correction Offset");
				txtOffset.Text = "";
				txtOffset.Focus();
			}
		}

		private void txtPrecision_TextChanged(object sender, System.EventArgs e)
		{
			//Validate precision
			try
			{
				if (txtPrecision.Text != "")
				Convert.ToInt32(txtPrecision.Text);
			}
			catch (FormatException)
			{
                MessageBox.Show("Unit precision should be a numeric value", "Unit Precision");
				txtPrecision.Text = "";
				txtPrecision.Focus();
			}    
		}

		private void txtFactor_TextChanged(object sender, System.EventArgs e)
		{
			//Validate factor
			try
			{
				if (txtFactor.Text != "")
				Convert.ToInt32(txtFactor.Text);
			}
			catch (FormatException)
			{
				MessageBox.Show("Distance Correction Factor should be a numeric value", "Distance Correction Factor");
				txtFactor.Text = "";
				txtFactor.Focus();
			} 
		}

		private void txtTolerance_TextChanged(object sender, System.EventArgs e)
		{
			//Validate tolerance
			try
			{
				if (txtTolerance.Text != "")
				Convert.ToInt32(txtTolerance.Text);
			}
			catch (FormatException)
			{
				MessageBox.Show("Sticky Move Tolerance should be a numeric value", "Sticky Move Tolerance");
				txtTolerance.Text = "";
				txtTolerance.Focus();
			} 
		}

	}
}
