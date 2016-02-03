using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.TrackingAnalyst;

namespace TAUpdateControlSample
{
	public partial class TAUpdateControlForm : Form
	{
		ITAUpdateControl m_taUpdateCtrl = null;

		public TAUpdateControlForm()
		{
			InitializeComponent();
		}

		private void TAUpdateControlForm_Load(object sender, EventArgs e)
		{
			//Get the ITAUpdateControl interface from the TA Display Manager
			//Store it as a member variable for future use
			//The DisplayManager is a singleton so the reference should never change
			ITrackingEnvironment3 taEnv = new TrackingEnvironmentClass();
			m_taUpdateCtrl = (ITAUpdateControl)taEnv.DisplayManager;
		}

		//Populate the controls with the current settings
		public void PopulateDialog()
		{
			if (m_taUpdateCtrl != null)
			{
				checkManualUpdate.Checked = m_taUpdateCtrl.ManualUpdate;
				txtUpdateRate.Text = Convert.ToString(m_taUpdateCtrl.MaxUpdateRate);
				cbUpdateMethod.SelectedIndex = (int)m_taUpdateCtrl.UpdateMethod;
				txtUpdateValue.Text = Convert.ToString(m_taUpdateCtrl.UpdateValue);

				checkAutoRefresh.Checked = m_taUpdateCtrl.AutoRefresh;
				txtRefreshRate.Text = Convert.ToString(m_taUpdateCtrl.RefreshRate);

				cbRefreshType.SelectedIndex = 0;
			}
		}

		//Cause a map refresh
		//The type of refresh used is determined by the refresh method combo box
		private void btnRefresh_Click(object sender, EventArgs e)
		{
			enumScreenUpdateType eScreenUpdateType = (enumScreenUpdateType)cbRefreshType.SelectedIndex;
			m_taUpdateCtrl.RefreshDisplay(eScreenUpdateType);
		}

		//Output the display statistics to the Statistics text box
		//The Statistics are returned as an XML string
        //The XML string should be relatively self-explanatory
		private void btnStats_Click(object sender, EventArgs e)
		{
			txtStatistics.Clear();
			txtStatistics.Text = m_taUpdateCtrl.Statistics;
		}

		//Apply any setting changes to Display Manager 
		private void btnApply_Click(object sender, EventArgs e)
		{
			if (m_taUpdateCtrl != null)
			{
				m_taUpdateCtrl.ManualUpdate = checkManualUpdate.Checked;
				m_taUpdateCtrl.MaxUpdateRate = Convert.ToDouble(txtUpdateRate.Text);
				m_taUpdateCtrl.UpdateMethod = (enumScreenUpdateThresholdType)cbUpdateMethod.SelectedIndex;
				m_taUpdateCtrl.UpdateValue = Convert.ToInt32(txtUpdateValue.Text);

				m_taUpdateCtrl.AutoRefresh = checkAutoRefresh.Checked;
				m_taUpdateCtrl.RefreshRate = Convert.ToDouble(txtRefreshRate.Text);
			}
		}

		//Just hide the dialog instead of disposing it
		private void TAUpdateControlForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			this.Hide();
			e.Cancel = true;
		}

		//Disable the options that aren't used when causing the display to update manually
		private void checkManualUpdate_CheckedChanged(object sender, EventArgs e)
		{
			txtUpdateRate.Enabled = !checkManualUpdate.Checked;
			cbUpdateMethod.Enabled = !checkManualUpdate.Checked;
			txtUpdateValue.Enabled = !checkManualUpdate.Checked;
		}

		//Disable the options that aren't used when the display is in AutoRefresh mode
		private void checkAutoRefresh_CheckedChanged(object sender, EventArgs e)
		{
			txtRefreshRate.Enabled = checkAutoRefresh.Checked;
		}

		private void btnHelp_Click(object sender, EventArgs e)
		{
			string sHelpDescription = "Description of ITAUpdateControl settings:\n\n" +
				"Update Settings:\n" +
				"\tDescription: Determines when to refresh the map if data is actively being received from a server.\n" +
				"\tManual Update: When enabled Tracking Analyst will not automatically update the display.\n" +
				"\t\tThe user or another application is responsible for managing display updates\n." +
				"\tUpdate Rate: The minimum duration between screen refreshes.\n" +
				"\tUpdate Method: Criteria that triggers an update to occur.\n" +
				"\t\tEvent-based: An update is triggered when the number of messages specified by the Update Value has been received\n" +
				"\t\tCPU Usage-based: An update is triggered when the time it takes to draw is less than the specified percentage of the time between updates.\n" +
				"\tUpdate Value: The threshold value of points or CPU percentage that must be met before an update will occur.\n" +
				"\t\tThe update rate will prevent a low threshold from causing the map to update too often.\n" +
				"Refresh Settings:\n" +
				"\tDescription: Determines when to refresh the map if no data is being received.\n" +
				"\t\tIf no data is being received then the map will not be updated and time windows and aging would become stale.\n" +
				"\tAutomatic: When enabled Tracking Analyst will perform maintenance refreshes even if no new data has been received.\n" +
				"\tRefresh Rate: The maximum duration between screen refreshes/updates.\n" +
				"Display Refresh:\n" +
				"\tDescription: Causes a manual screen refresh.\n" +
				"\tRefresh Method: There are five different methods for refreshing the display.\n" +
				"\t\tSee RefreshDisplay in the Developer Help for more information on the methods.\n" +
				"Statistics:\n" +
				"\tDescription: XML string containing the current TAUpdateControl settings and metrics on previous screen update metrics.\n";

			MessageBox.Show(this, sHelpDescription, "TAUpdateControl Description");
		}
	}
}