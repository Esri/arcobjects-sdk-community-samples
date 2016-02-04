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
//INSTANT C# NOTE: Formerly VB.NET project-level imports:
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.SystemUI;
using Microsoft.VisualBasic.Compatibility.VB6;
using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace AlgorithmicColorRamp
{
	internal partial class frmAlgorithmicColorRamp : System.Windows.Forms.Form
	{


		// This form allows a user to set properties determining the constraints of a
		// AlgoritmicColorRamp, which is then used to populate an existing ClassBreaksRenderer
		// on an existing FeatureLayer.
		//
		//
		// The m_lngClasses variable is set by the calling function, to indicate the
		// number of random colors required by the classbreaksrenderer.
		//
		public int m_lngClasses;
		//
		// The m_enumNewColors variable holds the colors to be returned to the calling function.
		//
		public IEnumColors m_enumNewColors;
		//
		// The m_lngColors variable holds the index of the last color displayed in the array.
		//
		private int m_lngColors;

		private System.Windows.Forms.Button[] Buttons = new System.Windows.Forms.Button[3];
		private System.Windows.Forms.Label[] LabelsIndex = new System.Windows.Forms.Label[11];
		private System.Windows.Forms.TextBox[] TextBoxColors = new System.Windows.Forms.TextBox[11];

		private void cmbAlgorithm_SelectedIndexChanged(object eventSender, System.EventArgs eventArgs)
		{
			if (cmbAlgorithm.SelectedIndex > -1)
				UpdateRamp();
		}

		private void cmdCancel_Click(object eventSender, System.EventArgs eventArgs)
		{
			//
			// User pressed Cancel, so we set the colors enumeration to nothing and
			// unload the form.
			//
			m_enumNewColors = null;
			this.Close();
		}

		private void cmdEnumColorsNext_Click(object eventSender, System.EventArgs eventArgs)
		{
			//
			// Increase the indicator variable m_lngColors by 10, so we can display the
			// next ten colors to the user.
			//
			if (m_enumNewColors != null)
			{
				m_lngColors = m_lngColors + 10;
				HideColors();
				LooRGBColors();
			}
		}

		private void cmdEnumColorsFirst_Click(object eventSender, System.EventArgs eventArgs)
		{
			//
			// Reset the indicator variable to zero.
			//
			if (m_enumNewColors != null)
			{
				m_lngColors = 0;
				HideColors();
				LooRGBColors();
			}
		}

		private void cmdOK_Click(object eventSender, System.EventArgs eventArgs)
		{
			//
			// Check we have a colors enumeration.
			//
			if (m_enumNewColors == null)
				MessageBox.Show("You have not created a new color ramp." + "Your layer symbology will be unchanged.", "No Ramp Created", MessageBoxButtons.OK, MessageBoxIcon.Information);
			else
				this.Hide();
		}

		private void frmAlgorithmicColorRamp_Load(object eventSender, System.EventArgs eventArgs)
		{
			//
			// Initialize the controls.
			//
			SetupControls();
		}

		private void UpdateRamp()
		{
			//
			// Create a new AlgorithmicColorRamp object, and get it's
			// IAlgorithmicColorRamp interface.
			//
			IAlgorithmicColorRamp AlgortihmicColorRamp = null;
			AlgortihmicColorRamp = new ESRI.ArcGIS.Display.AlgorithmicColorRamp();
			//
			// Set the size of the color ramp to the number of classes
			// to be renderered.
			//
			AlgortihmicColorRamp.Size = m_lngClasses;
			//
			// Set the color ramps properties.
			//
			IRgbColor RGBColor = null;
			RGBColor = new RgbColor();
			RGBColor.RGB = System.Drawing.ColorTranslator.ToOle(txtStartColor.BackColor);
			AlgortihmicColorRamp.FromColor = RGBColor;
			RGBColor.RGB = System.Drawing.ColorTranslator.ToOle(txtEndColor.BackColor);
			AlgortihmicColorRamp.ToColor = RGBColor;
			AlgortihmicColorRamp.Algorithm = (esriColorRampAlgorithm)cmbAlgorithm.SelectedIndex;

			bool boolRamp = false;
			if (AlgortihmicColorRamp.Size > 0)
			{

				boolRamp = true;
				AlgortihmicColorRamp.CreateRamp(out boolRamp);
				if (boolRamp)
				{
					m_enumNewColors = AlgortihmicColorRamp.Colors;
					m_enumNewColors.Reset();
					cmdOK.Enabled = true;
					//
					// Check if we should be showing the colors.
					//
					if (chkShowColors.CheckState == System.Windows.Forms.CheckState.Checked)
					{
						//
						// Populate the Colors textbox array and it's labels.
						//
						m_lngColors = 0;
						ShowColorsArray();
					}
				}
			}
		}

		private void SetupControls()
		{
			LabelsIndex[0] = Label1;
			LabelsIndex[1] = Label2;
			LabelsIndex[2] = Label3;
			LabelsIndex[3] = Label4;
			LabelsIndex[4] = Label5;
			LabelsIndex[5] = Label6;
			LabelsIndex[6] = Label7;
			LabelsIndex[7] = Label8;
			LabelsIndex[8] = Label9;
			LabelsIndex[9] = Label10;

			TextBoxColors[0] = TextBox1;
			TextBoxColors[1] = TextBox2;
			TextBoxColors[2] = TextBox3;
			TextBoxColors[3] = TextBox4;
			TextBoxColors[4] = TextBox5;
			TextBoxColors[5] = TextBox6;
			TextBoxColors[6] = TextBox7;
			TextBoxColors[7] = TextBox8;
			TextBoxColors[8] = TextBox9;
			TextBoxColors[9] = TextBox10;

			HideColors();

			txtStartColor.Text = "";
			txtEndColor.Text = "";
			txtStartColor.BackColor = System.Drawing.ColorTranslator.FromOle(0XFF);
			txtEndColor.BackColor = System.Drawing.ColorTranslator.FromOle(0XFF);
			//MsgBox("Before ", MsgBoxStyle.Information, "SetupControls ")

			Buttons[0] = Button1;
			Buttons[1] = Button2;

			//MsgBox("After  ", MsgBoxStyle.Information, "SetupControls")

			cmbAlgorithm.Items.Clear();
			cmbAlgorithm.Items.Insert(0, "0 - esriHSVAlgorithm");
			cmbAlgorithm.Items.Insert(1, "1 - esriCIELabAlgorithm");
			cmbAlgorithm.Items.Insert(2, "2 - esriLabLChAlgorithm");
			cmbAlgorithm.SelectedIndex = 0;

			cmdOK.Enabled = false;
			chkShowColors.CheckState = System.Windows.Forms.CheckState.Unchecked;

			UpdateRamp();
			chkShowColors_CheckStateChanged(chkShowColors, new System.EventArgs());
		}

		private void ShowColorsArray()
		{
			if (m_enumNewColors == null)
				return;
			else
			{
				//
				// Iterate and show all colors in the ColorRamp.
				//
				HideColors();
				LooRGBColors(); //m_lngColors
			}
		}

		private void LooRGBColors()
		{
			//
			// Move to the required Color to show. We only have space to show ten colors
			// at a time on the form. So when we wish to show the next ten colors,
			// (colors 11-20, 21-30 etc) we iterate the colors enumeration appropriately.
			//
			int lngMoveNext = 0;
			m_enumNewColors.Reset();
			while (lngMoveNext < m_lngColors)
			{
				m_enumNewColors.Next();
				lngMoveNext = lngMoveNext + 1;
			}
			//
			// Show colors in textboxes as necessary.
			//
			IColor colNew = null;
			int lngCount = 0;
			for (lngCount = 0; lngCount <= 9; lngCount++)
			{
				//commented the control array txtColor - OLD
				//With txtColor(lngCount)
				//    colNew = m_enumNewColors.Next
				//    '
				//    ' If getting the next color returns nothing, we have got to
				//    ' the end of the colors enumeration.
				//    '
				//    If colNew Is Nothing Then
				//        Exit For
				//    End If
				//    .BackColor = System.Drawing.ColorTranslator.FromOle(colNew.RGB)
				//    .Visible = True
				//End With
				colNew = m_enumNewColors.Next();
					//
					// If getting the next color returns nothing, we have got to
					// the end of the colors enumeration.
					//
				if (colNew == null)
					break;
				TextBoxColors[lngCount].BackColor = System.Drawing.ColorTranslator.FromOle(colNew.RGB);
				TextBoxColors[lngCount].Visible = true;
				//Commented the control array lblIndex - OLD
				//With lblIndex(lngCount)
				//    .Text = CStr(lngCount + m_lngColors)
				//    .Visible = True
				//End With
				//LabelsIndex[lngCount].Text = System.Convert().ToString(lngCount + m_lngColors);
                LabelsIndex[lngCount].Text = Convert.ToString (lngCount+m_lngColors);
				LabelsIndex[lngCount].Visible = true;

			}
		}

		private void HideColors()
		{
			//
			// Hide all Color textboxes.
			//
			short i = 0;
			for (i = 0; i <= 9; i++)
			{
				//txtColor(i).Visible = False '- OLD control array
				TextBoxColors[i].Visible = false;
				//lblIndex(i).Visible = False '- OLD control array
				LabelsIndex[i].Visible = false;
			}
		}

		private void frmAlgorithmicColorRamp_FormClosing(object eventSender, System.Windows.Forms.FormClosingEventArgs eventArgs)
		{
			bool Cancel = eventArgs.Cancel;
			System.Windows.Forms.CloseReason UnloadMode = eventArgs.CloseReason;
			if (m_enumNewColors == null)
			{
				//
				// User has exited the Form without setting a new ColorRamp.
				//
				MessageBox.Show("You have not created a new ColorRamp." + System.Environment.NewLine + "Your symbology will not be changed.", "ColorRamp not set", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			eventArgs.Cancel = Cancel;
		}

		private void chkShowColors_Click(object sender, System.EventArgs e)
		{
			if (chkShowColors.CheckState == System.Windows.Forms.CheckState.Checked)
			{
				this.Width = (int)Support.TwipsToPixelsX(3705);
				ShowColorsArray();
			}
			else
        this.Width = (int)Support.TwipsToPixelsX(2355);

		}

		private void chkShowColors_CheckStateChanged(object eventSender, System.EventArgs eventArgs)
		{
			//
			// Show and hide the colors array.
			//
			if (chkShowColors.CheckState == System.Windows.Forms.CheckState.Checked)
			{
				this.Width = (int)Support.TwipsToPixelsX(3705);
				ShowColorsArray();
			}
			else
        this.Width = (int)Support.TwipsToPixelsX(2355);
		}

		private void frmAlgorithmicColorRamp_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{

		}

		private void Button1_Click(object sender, System.EventArgs e)
		{
			IColorSelector ColorSelector = null;
			if (Buttons[0] == sender)
			{
				// Create color selector object.
				ColorSelector = new ColorSelectorClass();
				// Open the selector dialog.
				if (ColorSelector.DoModal(this.Handle.ToInt32()))
				{
					//
					// A Color was selected (if the above method returned false,
					// this indicates that the user pressed Cancel).
					txtStartColor.BackColor = System.Drawing.ColorTranslator.FromOle(ColorSelector.Color.RGB);
				}
				UpdateRamp();

			}
			else
			{
				ColorSelector = new ColorSelectorClass();
				if (ColorSelector.DoModal(this.Handle.ToInt32()))
					txtEndColor.BackColor = System.Drawing.ColorTranslator.FromOle(ColorSelector.Color.RGB);
				UpdateRamp();
			}
		}

		private void Label9_Click(object sender, System.EventArgs e)
		{

		}

		private void _txtColor_0_TextChanged(object sender, System.EventArgs e)
		{

		}
	}
} //end of root namespace