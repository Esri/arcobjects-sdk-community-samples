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
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.TrackingAnalyst;
using ESRI.ArcGIS.GeoDatabaseExtensions;

namespace TAPurgeRuleCommand
{
	public partial class PurgeRuleForm : Form
	{
		private IEnumLayer m_eTrackingLayers = null;

		public PurgeRuleForm()
		{
			InitializeComponent();
		}

		//The collection of temporal layers in the map/globe
		public IEnumLayer TrackingLayers
		{
			set
			{
				m_eTrackingLayers = value;
			}
		}

		//Initialize all the dialog controls
		public void PopulateDialog()
		{
			cbTrackingLayers.SelectedIndex = -1;
			PopulateTrackingLayerComboBox();
			LoadDialogFromFeatureClassSettings(null);
		}

		//Populate the tracking layer combo box with the names 
		//of all the temporal layers in the map
		private void PopulateTrackingLayerComboBox()
		{
			ILayer lyr = null;
			m_eTrackingLayers.Reset();

			cbTrackingLayers.Items.Clear();
			while ((lyr = m_eTrackingLayers.Next()) != null)
			{
				cbTrackingLayers.Items.Add(lyr.Name);
			}
		}

		//Get the ILayer using the name selected in the tracking layer combo box
		private ILayer GetSelectedLayer()
		{
			string sLayerName = Convert.ToString(cbTrackingLayers.SelectedItem);

			ILayer lyr = null;
			m_eTrackingLayers.Reset();

			while ((lyr = m_eTrackingLayers.Next()) != null)
			{
				if (lyr.Name == sLayerName)
				{
					return lyr;
				}
			}

			return null;
		}

		//Repopulate the dialog controls when a different layer is selected
		private void cbTrackingLayers_SelectionChangeCommitted(object sender, EventArgs e)
		{
			ILayer lyr = GetSelectedLayer();

			if (lyr != null)
			{
				ITemporalFeatureClass temporalFC = ((IFeatureLayer)lyr).FeatureClass as ITemporalFeatureClass;
				LoadDialogFromFeatureClassSettings(temporalFC);
			}

		}

		//Set the dialog controls according to the settings of the specified temporal feature class
		private void LoadDialogFromFeatureClassSettings(ITemporalFeatureClass temporalFC)
		{
			//If no feature class is specified clear all the controls
			if (temporalFC == null)
			{
				btnApply.Enabled = false;
				checkAutoPurge.Checked = false;
				cbPurgeRule.SelectedIndex = 0;
				txtThreshold.Text = "";
				txtPurgePercent.Text = "";
				return;
			}

			btnApply.Enabled = true;
			checkAutoPurge.Checked = temporalFC.AutoPurge;
			cbPurgeRule.SelectedIndex = (int)temporalFC.PurgeRule;
			txtThreshold.Text = Convert.ToString(temporalFC.Threshold);
			txtPurgePercent.Text = Convert.ToString(temporalFC.PurgePercentage);
		}

		//Apply the new settings to the temporal feature class
		private void btnApply_Click(object sender, EventArgs e)
		{
			try
			{
				ILayer lyr = GetSelectedLayer();

				if (lyr != null)
				{
					ITemporalFeatureClass temporalFC = (ITemporalFeatureClass)((IFeatureLayer)lyr).FeatureClass;

          				if (temporalFC != null)
          				{
            					temporalFC.AutoPurge = checkAutoPurge.Checked;

						//only set the other properties if Auto Purge is true
						if (checkAutoPurge.Checked)
						{
							temporalFC.PurgeRule = (enumPurgeRule)cbPurgeRule.SelectedIndex;
							temporalFC.Threshold = Convert.ToInt32(txtThreshold.Text);
							temporalFC.PurgePercentage = Convert.ToDouble(txtPurgePercent.Text);
						}
					}
				}

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Error");
			}
		}

		//Set the appropriate text boxes enabled depending on the purge rule selected
		private void cbPurgeRule_SelectedIndexChanged(object sender, EventArgs e)
		{
			switch (cbPurgeRule.SelectedIndex)
			{
				case 0:
				default:
					txtThreshold.Enabled = true;
					txtPurgePercent.Enabled = true;
					break;
				case 1:
					txtThreshold.Enabled = true;
					txtPurgePercent.Enabled = false;
					break;
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.Hide();
		}

		//Don't dispose the dialog when it's closed.  Just hide it so that it can be reopened.
		private void PurgeRuleForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			this.Hide();
			e.Cancel = true;
		}
	}
}