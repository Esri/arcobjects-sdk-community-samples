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
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.NetworkAnalyst;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using System.Text;

namespace ServiceAreaSolver
{
	public partial class frmServiceAreaSolver : Form
	{
		private INAContext m_NAContext;

		#region Main Form Constructor and Setup

        /// <summary>
        /// Initialize the solver by calling the ArcGIS Network Analyst extension functions.
        /// </summary>
        public frmServiceAreaSolver()
        {
            InitializeComponent();
            Initialize();
        }

        /// <summary>
        /// Set up the default values on the form
        /// </summary>
        private void Initialize()
		{
			txtCutOff.Text = "5";
			lstOutput.Items.Clear();
			cbCostAttribute.Items.Clear();
			ckbUseRestriction.Checked = false;
			axMapControl.ClearLayers();

			txtWorkspacePath.Text = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), @"ArcGIS\data\SanFrancisco\SanFrancisco.gdb");
			txtNetworkDataset.Text = "Streets_ND";
			txtFeatureDataset.Text = "Transportation";
			txtInputFacilities.Text = "Hospitals";
			gbServiceAreaSolver.Enabled = false;
		}

		#endregion

		#region Button Clicks

        /// <summary>
        /// Call the Service Area solver and display the results
        /// </summary>
        /// <param name="sender">Sender of the event</param>
        /// <param name="e">Event</param>
        private void btnSolve_Click(object sender, EventArgs e)
		{
			this.Cursor = Cursors.WaitCursor;
			lstOutput.Items.Clear();

            IGPMessages gpMessages = new GPMessagesClass();
            try
            {
                lstOutput.Items.Clear();
                lstOutput.Items.Add("Solving...");

                ConfigureSolverSettings();

				if (m_NAContext.Solver.Solve(m_NAContext, gpMessages, null))
			        lstOutput.Items.Add("Partial Solve Generated.");

                DisplayOutput();

            }
			catch (Exception ee)
			{
                lstOutput.Items.Add("Failure: " + ee.Message);
            }

            lstOutput.Items.Add(GetGPMessagesAsString(gpMessages));

            RefreshMapDisplay();

			this.Cursor = Cursors.Default;
		}

        /// <summary>
        /// Open the network dataset and set up the map
        /// </summary>
        /// <param name="sender">Sender of the event</param>
        /// <param name="e">Event</param>
        private void btnLoadMap_Click(object sender, EventArgs e)
		{
			this.Cursor = Cursors.WaitCursor;

			gbServiceAreaSolver.Enabled = false;
			lstOutput.Items.Clear();

            // Verify that the workspace is valid
			IWorkspace workspace = OpenWorkspace(txtWorkspacePath.Text);
            if (workspace != null)
            {
                // Open the network dataset and generate a solver/context
                INetworkDataset networkDataset = OpenNetworkDataset(workspace, txtFeatureDataset.Text, txtNetworkDataset.Text);
                IFeatureWorkspace featureWorkspace = workspace as IFeatureWorkspace;
                CreateSolverContext(networkDataset);
                if (m_NAContext != null)
                {
                    LoadCostAttributes(networkDataset);
                    if (LoadLocations(featureWorkspace))
                    {
                        AddNetworkDatasetLayerToMap(networkDataset);
                        AddNetworkAnalysisLayerToMap();

                        // work around a transparency issue
                        IGeoDataset geoDataset = networkDataset as IGeoDataset;
                        axMapControl.Extent = axMapControl.FullExtent;
                        axMapControl.Extent = geoDataset.Extent;

                        if (m_NAContext != null) gbServiceAreaSolver.Enabled = true;
                    }
                }
            }

			this.Cursor = Cursors.Default;
		}

		#endregion

		#region Set up Context and Solver

        /// <summary>
        /// Geodatabase function: open work space
        /// </summary>
        /// <param name="strGDBName">Input file name</param>
        /// <returns>Workspace</returns>
        public IWorkspace OpenWorkspace(string strGDBName)
		{
			// As Workspace Factories are Singleton objects, they must be instantiated with the Activator
			var workspaceFactory = System.Activator.CreateInstance(System.Type.GetTypeFromProgID("esriDataSourcesGDB.FileGDBWorkspaceFactory")) as ESRI.ArcGIS.Geodatabase.IWorkspaceFactory;

            if (!System.IO.Directory.Exists(strGDBName))
			{
                MessageBox.Show("The workspace: " + strGDBName + " does not exist", "Workspace Error");
				return null;
			}

			IWorkspace workspace = null;
			try
			{
                workspace = workspaceFactory.OpenFromFile(strGDBName, 0);
			}
			catch (Exception ex)
			{
				MessageBox.Show("Opening workspace failed: " + ex.Message, "Workspace Error");
			}

			return workspace;
		}

        /// <summary>
        /// Geodatabase function: open network dataset
        /// </summary>
        /// <param name="workspace">Input workspace</param>
        /// <param name="strNDSName">Input network dataset name</param>
        /// <returns>NetworkDataset</returns>
        public INetworkDataset OpenNetworkDataset(IWorkspace workspace, string featureDatasetName, string strNDSName)
		{
			// Obtain the dataset container from the workspace
			var featureWorkspace = workspace as IFeatureWorkspace;
			ESRI.ArcGIS.Geodatabase.IFeatureDataset featureDataset = featureWorkspace.OpenFeatureDataset(featureDatasetName);
			var featureDatasetExtensionContainer = featureDataset as ESRI.ArcGIS.Geodatabase.IFeatureDatasetExtensionContainer;
			ESRI.ArcGIS.Geodatabase.IFeatureDatasetExtension featureDatasetExtension = featureDatasetExtensionContainer.FindExtension(ESRI.ArcGIS.Geodatabase.esriDatasetType.esriDTNetworkDataset);
			var datasetContainer3 = featureDatasetExtension as ESRI.ArcGIS.Geodatabase.IDatasetContainer3;

			// Use the container to open the network dataset.
			ESRI.ArcGIS.Geodatabase.IDataset dataset = datasetContainer3.get_DatasetByName(ESRI.ArcGIS.Geodatabase.esriDatasetType.esriDTNetworkDataset, strNDSName);
			return dataset as ESRI.ArcGIS.Geodatabase.INetworkDataset;
		}

        /// <summary>
        /// Geodatabase function: get network dataset
        /// </summary>
        /// <param name="networkDataset">Input network dataset</param>
        /// <returns>DE network dataset</returns>		
        public IDENetworkDataset GetDENetworkDataset(INetworkDataset networkDataset)
        {
            // Cast from the network dataset to the DatasetComponent
            IDatasetComponent dsComponent = networkDataset as IDatasetComponent;

            // Get the data element
            return dsComponent.DataElement as IDENetworkDataset;
        }

        /// <summary>
        /// Create NASolver and NAContext
        /// </summary>
        /// <param name="networkDataset">Input network dataset</param>
        private void CreateSolverContext(INetworkDataset networkDataset)
		{
			if (networkDataset == null) return;

            //Get the Data Element
            IDENetworkDataset deNDS = GetDENetworkDataset(networkDataset);

            INASolver naSolver = new NAServiceAreaSolverClass();
            m_NAContext = naSolver.CreateContext(deNDS, naSolver.Name);
            ((INAContextEdit)m_NAContext).Bind(networkDataset, new GPMessagesClass());
        }

		#endregion

		#region Load Form Controls

        /// <summary>
        /// Find and load the cost attributes into a combo box
        /// <summary>
        private void LoadCostAttributes(INetworkDataset networkDataset)
		{
			cbCostAttribute.Items.Clear();

			int attrCount = networkDataset.AttributeCount;
			for (int attrIndex = 0; attrIndex < attrCount; attrIndex++)
			{
				INetworkAttribute networkAttribute = networkDataset.get_Attribute(attrIndex);
				if (networkAttribute.UsageType == esriNetworkAttributeUsageType.esriNAUTCost)
					cbCostAttribute.Items.Add(networkAttribute.Name);
			}

			if (cbCostAttribute.Items.Count > 0)
				cbCostAttribute.SelectedIndex = 0;
		}

        /// <summary>
        /// Find and load the cost attributes into a combo box
        /// <summary>
        /// <param name="featureWorkspace">The workspace that holds the input feature class</param>
        /// <returns>Success</returns>
        private bool LoadLocations(IFeatureWorkspace featureWorkspace)
		{
			IFeatureClass inputFeatureClass = null;
			try
			{
				inputFeatureClass = featureWorkspace.OpenFeatureClass(txtInputFacilities.Text);
			}
			catch (Exception)
			{
				MessageBox.Show("Specified input feature class does not exist");
				return false;
			}

			INamedSet classes = m_NAContext.NAClasses;
			INAClass naClass = classes.get_ItemByName("Facilities") as INAClass;

			// delete existing locations, except barriers
			naClass.DeleteAllRows();

			// Create a NAClassLoader and set the snap tolerance (meters unit)
			INAClassLoader naClassLoader = new NAClassLoaderClass();
			naClassLoader.Locator = m_NAContext.Locator;
			((INALocator3)naClassLoader.Locator).MaxSnapTolerance = 500;
			naClassLoader.NAClass = naClass;

			// Create field map to automatically map fields from input class to NAClass
			INAClassFieldMap naClassFieldMap = new NAClassFieldMapClass();
			naClassFieldMap.CreateMapping(naClass.ClassDefinition, inputFeatureClass.Fields);
			naClassLoader.FieldMap = naClassFieldMap;

			// Avoid loading network locations onto non-traversable portions of elements
			INALocator3 locator = m_NAContext.Locator as INALocator3;
			locator.ExcludeRestrictedElements = true;
			locator.CacheRestrictedElements(m_NAContext);

			// load network locations
			int rowsIn = 0;
			int rowsLocated = 0;
			naClassLoader.Load(inputFeatureClass.Search(null, true) as ICursor, null, ref rowsIn, ref rowsLocated);

			if (rowsLocated <= 0)
			{
				MessageBox.Show("Facilities were not loaded from input feature class");
				return false;
			}

			// Message all of the network analysis agents that the analysis context has changed
			INAContextEdit naContextEdit = m_NAContext as INAContextEdit;
			naContextEdit.ContextChanged();

			return true;
		}

        /// <summary>
        /// Create a layer from the context and add it to the map
        /// </summary>
        private void AddNetworkAnalysisLayerToMap()
		{
			ILayer layer = m_NAContext.Solver.CreateLayer(m_NAContext) as ILayer;
			layer.Name = m_NAContext.Solver.DisplayName;
			axMapControl.AddLayer(layer);
		}

        /// <summary>
        /// Create a layer for the network dataset and add it to the map
        /// </summary>
        private void AddNetworkDatasetLayerToMap(INetworkDataset networkDataset)
		{
			INetworkLayer networkLayer = new NetworkLayerClass();
			networkLayer.NetworkDataset = networkDataset;
			ILayer layer = networkLayer as ILayer;
			layer.Name = "Network Dataset";
			axMapControl.AddLayer(layer);
		}

		#endregion

		#region Solver Settings

        /// <summary>
        /// Prepare the solver
        /// </summary>
        private void ConfigureSolverSettings()
		{
			ConfigureSettingsSpecificToServiceAreaSolver();

			ConfigureGenericSolverSettings();

			UpdateContextAfterChangingSettings();
		}

        /// <summary>
        /// Update settings that only apply to the Service Area
        /// </summary>
        private void ConfigureSettingsSpecificToServiceAreaSolver()
		{
			INAServiceAreaSolver naSASolver = m_NAContext.Solver as INAServiceAreaSolver;

			naSASolver.DefaultBreaks = ParseBreaks(txtCutOff.Text);

			naSASolver.MergeSimilarPolygonRanges = false;
			naSASolver.OutputPolygons = esriNAOutputPolygonType.esriNAOutputPolygonSimplified;
			naSASolver.OverlapLines = true;
			naSASolver.SplitLinesAtBreaks = false;
			naSASolver.TravelDirection = esriNATravelDirection.esriNATravelDirectionFromFacility;
			naSASolver.OutputLines = esriNAOutputLineType.esriNAOutputLineNone;
		}

        /// <summary>
        /// Update settings that apply to all solvers
        /// </summary>
        private void ConfigureGenericSolverSettings()
		{
			INASolverSettings naSolverSettings = m_NAContext.Solver as INASolverSettings;
			naSolverSettings.ImpedanceAttributeName = cbCostAttribute.Text;

			// set the oneway restriction, if necessary
			IStringArray restrictions = naSolverSettings.RestrictionAttributeNames;
			restrictions.RemoveAll();
			if (ckbUseRestriction.Checked)
				restrictions.Add("Oneway");
			naSolverSettings.RestrictionAttributeNames = restrictions;
			//naSolverSettings.RestrictUTurns = esriNetworkForwardStarBacktrack.esriNFSBNoBacktrack;
		}

        /// <summary>
        /// When the solver has been update, the context must be updated as well
        /// </summary>
        private void UpdateContextAfterChangingSettings()
		{
			IDatasetComponent datasetComponent = m_NAContext.NetworkDataset as IDatasetComponent;
			IDENetworkDataset deNetworkDataset = datasetComponent.DataElement as IDENetworkDataset;
			m_NAContext.Solver.UpdateContext(m_NAContext, deNetworkDataset, new GPMessagesClass());
		}

        /// <summary>
        /// Prepare the text string for breaks
        /// </summary>
        private IDoubleArray ParseBreaks(string p)
		{
			String[] breaks = p.Split(' ');
			IDoubleArray pBrks = new DoubleArrayClass();
			int firstIndex = breaks.GetLowerBound(0);
			int lastIndex = breaks.GetUpperBound(0);
			for (int splitIndex = firstIndex; splitIndex <= lastIndex; splitIndex++)
			{
				try
				{
					pBrks.Add(Convert.ToDouble(breaks[splitIndex]));
				}
				catch (FormatException)
				{
					MessageBox.Show("Breaks are not properly formatted.  Use only digits separated by spaces");
					pBrks.RemoveAll();
					return pBrks;
				}
			}

			return pBrks;
		}

		#endregion

		#region Post-Solve

        /// <summary>
        /// Display analysis results in the list box
        /// </summary>
        private void DisplayOutput()
		{
			ITable table = m_NAContext.NAClasses.get_ItemByName("SAPolygons") as ITable;
			if (table.RowCount(null) > 0)
			{
				IGPMessage gpMessage = new GPMessageClass();
				lstOutput.Items.Add("FacilityID, FromBreak, ToBreak");
				ICursor cursor = table.Search(null, true);
				IRow row = cursor.NextRow();
				while (row != null)
				{
					int facilityID = (int)row.get_Value(table.FindField("FacilityID"));
					double fromBreak = (double)row.get_Value(table.FindField("FromBreak"));
					double toBreak = (double)row.get_Value(table.FindField("ToBreak"));
					lstOutput.Items.Add(facilityID.ToString() + ", " + fromBreak.ToString("#####0.00") + ", " + toBreak.ToString("#####0.00"));
					row = cursor.NextRow();
				}
			}
		}

        /// <summary>
        /// Refresh the map display
        /// <summary>
        public void RefreshMapDisplay()
        {
			IGeoDataset geoDataset = m_NAContext.NAClasses.get_ItemByName("SAPolygons") as IGeoDataset;
			IEnvelope envelope = geoDataset.Extent;
			if (!envelope.IsEmpty)
			{
				envelope.Expand(1.1, 1.1, true);
				axMapControl.Extent = envelope;

				// Call this to update the renderer for the service area polygons
				// based on the new breaks.
				m_NAContext.Solver.UpdateLayer(axMapControl.get_Layer(0) as INALayer);
			}
			axMapControl.Refresh();
		}
        /// <summary>
        /// Gather the error/warning/informative messages from GPMessages
        /// <summary>
        /// <param name="gpMessages">GPMessages container</param>
        /// <returns>string of all GPMessages</returns>
        public string GetGPMessagesAsString(IGPMessages gpMessages)
        {
            // Gather Error/Warning/Informative Messages
            var messages = new StringBuilder();
            if (gpMessages != null)
            {
                for (int i = 0; i < gpMessages.Count; i++)
                {
                    IGPMessage gpMessage = gpMessages.GetMessage(i);
                    string message = gpMessage.Description;
                    switch (gpMessages.GetMessage(i).Type)
                    {
                        case esriGPMessageType.esriGPMessageTypeError:
                            messages.AppendLine("Error " + gpMessage.ErrorCode + ": " + message);
                            break;
                        case esriGPMessageType.esriGPMessageTypeWarning:
                            messages.AppendLine("Warning: " + message);
                            break;
                        default:
                            messages.AppendLine("Information: " + message);
                            break;
                    }
                }
            }
            return messages.ToString();
        }

		#endregion
	}
}