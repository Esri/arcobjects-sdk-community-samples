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
using System.Data;
using System.IO;
using System.Runtime.InteropServices;

using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.TrackingAnalyst;
using ESRI.ArcGIS.Geodatabase;
using TemporalStatistics2008;
using ESRI.ArcGIS.GeoDatabaseExtensions;

namespace TemporalStatistics
{
	public sealed partial class MainForm : Form
	{
		#region class private members
		private IMapControl3 m_mapControl = null;
		private string m_mapDocumentName = string.Empty;
		#endregion
		IWorkspaceFactory m_amsWorkspaceFactory = null;
		bool m_bTAInitialized = false;
		private const string TEMPORALLAYERCLSID = "{78C7430C-17CF-11D5-B7CF-00010265ADC5}"; //CLSID for ITemporalLayer

		#region class constructor
		public MainForm()
		{
			InitializeComponent();
		}
		#endregion

		private void MainForm_Load(object sender, EventArgs e)
		{
			//get the MapControl
			m_mapControl = (IMapControl3)axMapControl1.Object;

			//disable the Save menu (since there is no document yet)
			menuSaveDoc.Enabled = false;

			timerStats.Start();
		}

		#region Main Menu event handlers
		private void menuNewDoc_Click(object sender, EventArgs e)
		{
			//execute New Document command
			ICommand command = new CreateNewDocument();
			command.OnCreate(m_mapControl.Object);
			command.OnClick();
		}

		private void menuOpenDoc_Click(object sender, EventArgs e)
		{
			//execute Open Document command
			ICommand command = new ControlsOpenDocCommandClass();
			command.OnCreate(m_mapControl.Object);
			command.OnClick();
		}

		private void menuSaveDoc_Click(object sender, EventArgs e)
		{
			//execute Save Document command
			if (m_mapControl.CheckMxFile(m_mapDocumentName))
			{
				//create a new instance of a MapDocument
				IMapDocument mapDoc = new MapDocumentClass();
				mapDoc.Open(m_mapDocumentName, string.Empty);

				//Make sure that the MapDocument is not readonly
				if (mapDoc.get_IsReadOnly(m_mapDocumentName))
				{
					MessageBox.Show("Map document is read only!");
					mapDoc.Close();
					return;
				}

				//Replace its contents with the current map
				mapDoc.ReplaceContents((IMxdContents)m_mapControl.Map);

				//save the MapDocument in order to persist it
				mapDoc.Save(mapDoc.UsesRelativePaths, false);

				//close the MapDocument
				mapDoc.Close();
			}
		}

		private void menuSaveAs_Click(object sender, EventArgs e)
		{
			//execute SaveAs Document command
			ICommand command = new ControlsSaveAsDocCommandClass();
			command.OnCreate(m_mapControl.Object);
			command.OnClick();
		}

		private void menuExitApp_Click(object sender, EventArgs e)
		{
			//exit the application
			Application.Exit();
		}
		#endregion

        //listen to MapReplaced event in order to update the status bar and the Save menu
		private void axMapControl1_OnMapReplaced(object sender, IMapControlEvents2_OnMapReplacedEvent e)
		{
			//get the current document name from the MapControl
			m_mapDocumentName = m_mapControl.DocumentFilename;

            //if there is no MapDocument, disable the Save menu and clear the status bar
			if (m_mapDocumentName == string.Empty)
			{
				menuSaveDoc.Enabled = false;
				statusBarXY.Text = string.Empty;
			}
			else
			{
				//enable the Save menu and write the doc name to the status bar
				menuSaveDoc.Enabled = true;
				statusBarXY.Text = Path.GetFileName(m_mapDocumentName);
			}

			//Update combo list of tracking services
			PopulateTrackingServices();
		}

		private void axMapControl1_OnMouseMove(object sender, IMapControlEvents2_OnMouseMoveEvent e)
		{
			statusBarXY.Text = string.Format("{0}, {1}  {2}", e.mapX.ToString("#######.##"), e.mapY.ToString("#######.##"), axMapControl1.MapUnits.ToString().Substring(4));
		}

		//Initialize the Tracking Analyst Environment
		private ITrackingEnvironment3 setupTrackingEnv(ref object mapObj)
		{
			IExtensionManager extentionManager = new ExtensionManagerClass();

			UID uid = new UIDClass();
			uid.Value = "esriTrackingAnalyst.TrackingEngineUtil";

			((IExtensionManagerAdmin)extentionManager).AddExtension(uid, ref mapObj);

			ITrackingEnvironment3 trackingEnv = new TrackingEnvironmentClass();
			trackingEnv.Initialize(ref mapObj);
			trackingEnv.EnableTemporalDisplayManagement = true;
			return trackingEnv;
		}

		//Periodically update the statistics information
		private void timerStats_Tick(object sender, EventArgs e)
		{
			//Initialize TA if there is hasn't been already and there are Tracking layers in the map
			if (!m_bTAInitialized && 
				GetAllTrackingLayers() != null)
			{
				object oMapControl = m_mapControl;
                ITrackingEnvironment3 taEnv = setupTrackingEnv(ref oMapControl);
                //ITrackingEnvironment3 taEnv = setupTrackingEnv(ref oMapControl);

				if (taEnv != null)
				{
					m_bTAInitialized = true;
				}

				//Need to refresh the map once to get the tracks moving
				m_mapControl.Refresh(esriViewDrawPhase.esriViewGeography, null, null);
			}

			RefreshStatistics();
		}

		private void RefreshStatistics()
		{
			try
			{
				ITemporalLayer temporalLayer = GetSelectedTemporalLayer();

				//If a temporal layer is selected in the combo box only update that layer's stats
				if (temporalLayer == null)
				{
					RefreshAllStatistics();
				}
				else
				{
					RefreshLayerStatistics(temporalLayer.Name,
						(ITemporalFeatureClassStatistics)((IFeatureLayer)temporalLayer).FeatureClass);
				}
			}
			catch (Exception ex)
			{
				statusBarXY.Text = ex.Message;
			}
		}

		//Refresh the statistics for all tracking layers in the map
		//The AMSWorkspaceFactory provides easy access to query the statistics for every layer at once
		private void RefreshAllStatistics()
		{
			try
			{
				object oNames, oValues;
				string[] sNames;

				if (m_amsWorkspaceFactory == null)
				{
					m_amsWorkspaceFactory = new AMSWorkspaceFactoryClass();
				}

				//Get the AMS Workspace Factory Statistics interface
				ITemporalWorkspaceStatistics temporalWsfStatistics = (ITemporalWorkspaceStatistics)m_amsWorkspaceFactory;
				//Get the message rates for all the tracking connections in the map
				IPropertySet psMessageRates = temporalWsfStatistics.AllMessageRates;
				psMessageRates.GetAllProperties(out oNames, out oValues);
				sNames = (string[])oNames;
				object[] oaMessageRates = (object[])oValues;

				//Get the feature counts for all the tracking connections in the map
				IPropertySet psTotalFeatureCounts = temporalWsfStatistics.AllTotalFeatureCounts;
				psTotalFeatureCounts.GetAllProperties(out oNames, out oValues);
				object[] oaFeatureCounts = (object[])oValues;

				//Get the connection status for all the tracking connections in the map
				IPropertySet psConnectionStatus = temporalWsfStatistics.ConnectionStatus;
				psConnectionStatus.GetAllProperties(out oNames, out oValues);
				string[] sConnectionNames = (string[])oNames;
				object[] oaConnectionStatus = (object[])oValues;
				Hashtable htConnectionStatus = new Hashtable(sConnectionNames.Length);
				for (int i = 0; i < sConnectionNames.Length; ++i)
				{
					htConnectionStatus.Add(sConnectionNames[i], oaConnectionStatus[i]);
				}

				//Get the track counts for all the tracking connections in the map
				IPropertySet psTrackCounts = temporalWsfStatistics.AllTrackCounts;
				psTrackCounts.GetAllProperties(out oNames, out oValues);
				object[] oaTrackCounts = (object[])oValues;

				//Get the sample sizes for all the tracking connections in the map
				IPropertySet psSampleSizes = temporalWsfStatistics.AllSampleSizes;
				psSampleSizes.GetAllProperties(out oNames, out oValues);
				object[] oaSampleSizes = (object[])oValues;

				//Clear the existing list view items and repopulate from the collection
				lvStatistics.BeginUpdate();
				lvStatistics.Items.Clear();

				//Create list view items from statistics' IPropertySets
				for (int i = 0; i < sNames.Length; ++i)
				{
					ListViewItem lvItem = new ListViewItem(sNames[i]);
					lvItem.SubItems.Add(Convert.ToString(oaMessageRates[i]));
					lvItem.SubItems.Add(Convert.ToString(oaFeatureCounts[i]));

					string sConnName = sNames[i].Split(new Char[] { '/' })[0];
					esriWorkspaceConnectionStatus eWCS = (esriWorkspaceConnectionStatus)Convert.ToInt32(htConnectionStatus[sConnName]);
					lvItem.SubItems.Add(eWCS.ToString());

					lvItem.SubItems.Add(Convert.ToString(oaTrackCounts[i]));
					lvItem.SubItems.Add(Convert.ToString(oaSampleSizes[i]));
					lvStatistics.Items.Add(lvItem);
				}

				lvStatistics.EndUpdate();
			}
			catch (System.Exception ex)
			{
				statusBarXY.Text = ex.Message;
			}
		}

		//Refresh the statistics for a single layer using the ITemporalFeatureClassStatistics
		private void RefreshLayerStatistics(string sLayerName, ITemporalFeatureClassStatistics temporalFCStats)
		{
			ListViewItem lvItem = new ListViewItem(sLayerName);
			lvItem.SubItems.Add(Convert.ToString(temporalFCStats.MessageRate));
			lvItem.SubItems.Add(Convert.ToString(temporalFCStats.TotalFeatureCount));
			lvItem.SubItems.Add("Not Available");
			lvItem.SubItems.Add(Convert.ToString(temporalFCStats.TrackCount));
			lvItem.SubItems.Add(Convert.ToString(temporalFCStats.SampleSize));

			lvStatistics.Items.Clear();
			lvStatistics.Items.Add(lvItem);
		}

		//Cause a manual refresh of the statistics, also update the timer interval
		private void btnRefresh_Click(object sender, EventArgs e)
		{
			try
			{
				timerStats.Stop();
				SetSampleSize();
				RefreshStatistics();
				double dTimerRate = Convert.ToDouble(txtRate.Text);
				timerStats.Interval = Convert.ToInt32(dTimerRate * 1000);
				timerStats.Start();
			}
			catch (Exception ex)
			{
				statusBarXY.Text = ex.Message;
			}
		}

		//Populate the combo box with the tracking services in the map
		private void PopulateTrackingServices()
		{
			ILayer lyr = null;
			IEnumLayer temporalLayers = GetAllTrackingLayers();

			cbTrackingServices.Items.Clear();
			cbTrackingServices.Items.Add("All");

			if (temporalLayers != null)
			{
				while ((lyr = temporalLayers.Next()) != null)
				{
					cbTrackingServices.Items.Add(lyr.Name);
				}
			}

			cbTrackingServices.SelectedIndex = 0;
		}

		//Query the map for all the tracking layers in it
		private IEnumLayer GetAllTrackingLayers()
		{
			try
			{
				IUID uidTemoralLayer = new UIDClass();
				uidTemoralLayer.Value = TEMPORALLAYERCLSID;

				//This call throws an E_FAIL exception if the map has no layers, caught below
				return m_mapControl.ActiveView.FocusMap.get_Layers((UID)uidTemoralLayer, true);
			}
			catch
			{
				return null;
			}
		}

		//Get the tracking layer that is selected in the combo box according to its name
		private ITemporalLayer GetSelectedTemporalLayer()
		{
			ITemporalLayer temporalLayer = null;

			if (cbTrackingServices.SelectedIndex > 0)
			{
				ILayer lyr = null;
				IEnumLayer temporalLayers = GetAllTrackingLayers();
				string selectedLayerName = cbTrackingServices.Text;

				while ((lyr = temporalLayers.Next()) != null)
				{
					if (lyr.Name == selectedLayerName)
					{
						temporalLayer = (ITemporalLayer)lyr;
					}
				}
			}

			return temporalLayer;
		}

		//Reset the statistic's feature count for one or all of the layers in the map
		private void btnResetFC_Click(object sender, EventArgs e)
		{
			try
			{
				ITemporalLayer temporalLayer = GetSelectedTemporalLayer();

				if (temporalLayer == null)
				{
					if (m_amsWorkspaceFactory == null)
					{
						m_amsWorkspaceFactory = new AMSWorkspaceFactoryClass();
					}

					//Get the AMS Workspace Factory Statistics interface
					ITemporalWorkspaceStatistics temporalWsfStatistics = (ITemporalWorkspaceStatistics)m_amsWorkspaceFactory;
					temporalWsfStatistics.ResetAllFeatureCounts();
				}
				else
				{
					ITemporalFeatureClassStatistics temporalFCStats =
						(ITemporalFeatureClassStatistics)((IFeatureLayer)temporalLayer).FeatureClass;
					temporalFCStats.ResetFeatureCount();
				}
			}
			catch (Exception ex)
			{
				statusBarXY.Text = ex.Message;
			}
		}

		//Reset the statistic's message rate for one or all of the layers in the map
		private void btnResetMsgRate_Click(object sender, EventArgs e)
		{
			try
			{
				ITemporalLayer temporalLayer = GetSelectedTemporalLayer();

				if (temporalLayer == null)
				{
					if (m_amsWorkspaceFactory == null)
					{
						m_amsWorkspaceFactory = new AMSWorkspaceFactoryClass();
					}

					//Get the AMS Workspace Factory Statistics interface
					ITemporalWorkspaceStatistics temporalWsfStatistics = (ITemporalWorkspaceStatistics)m_amsWorkspaceFactory;
					temporalWsfStatistics.ResetAllMessageRates();
				}
				else
				{
					ITemporalFeatureClassStatistics temporalFCStats =
						(ITemporalFeatureClassStatistics)((IFeatureLayer)temporalLayer).FeatureClass;
					temporalFCStats.ResetMessageRate();
				}
			}
			catch (Exception ex)
			{
				statusBarXY.Text = ex.Message;
			}
		}

		//Set the sampling size for one or all of the layers in the map
		//The sampling size determines how many messages are factored into the message rate calculation.  
		//For instance a sampling size of 500 will store the times the last 500 messages were received.
		//The message rate is calculated as the (oldest timestamp - current time) / number of messages
		private void SetSampleSize()
		{
			try
			{
				int samplingSize = Convert.ToInt32(txtSampleSize.Text);
				ITemporalLayer temporalLayer = GetSelectedTemporalLayer();

				if (temporalLayer == null)
				{
					if (m_amsWorkspaceFactory == null)
					{
						m_amsWorkspaceFactory = new AMSWorkspaceFactoryClass();
					}

					//Get the AMS Workspace Factory Statistics interface
					ITemporalWorkspaceStatistics temporalWsfStatistics = (ITemporalWorkspaceStatistics)m_amsWorkspaceFactory;
					temporalWsfStatistics.SetAllSampleSizes(samplingSize);
				}
				else
				{
					ITemporalFeatureClassStatistics temporalFCStats =
						(ITemporalFeatureClassStatistics)((IFeatureLayer)temporalLayer).FeatureClass;
					temporalFCStats.SampleSize = samplingSize;
				}
			}
			catch (Exception ex)
			{
				statusBarXY.Text = ex.Message;
			}
		}

		private void cbTrackingServices_SelectionChangeCommitted(object sender, EventArgs e)
		{
			RefreshStatistics();
		}
	}
}