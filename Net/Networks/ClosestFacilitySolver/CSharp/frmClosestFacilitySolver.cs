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
//*************************************************************************************
//       ArcGIS Network Analyst extension - Closest Facility Demonstration
//
//   This simple code shows how to :
//    1) Open a workspace and open a Network DataSet
//    2) Create a NAContext and its NASolver
//    3) Load Incidents/Facilites from Feature Classes and create Network Locations
//    4) Set the Solver parameters
//    5) Solve a Closest Facility problem
//    6) Read the CFRoutes output to display the total facilities
//       and the list of the routes found
//************************************************************************************

using System;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.NetworkAnalyst;

namespace ClosestFacilitySolver
{
	public partial class frmClosestFacilitySolver : Form
	{
		private INAContext m_NAContext;
        private readonly string OUTPUTCLASSNAME = "CFRoutes";

        #region Main Form Constructor and Setup

        public frmClosestFacilitySolver()
		{
			InitializeComponent();
			Initialize();
		}

        /// <summary>
        /// Initialize the solver by calling the ArcGIS Network Analyst extension functions.
        /// </summary>
        private void Initialize()
		{
			IFeatureWorkspace featureWorkspace = null;
			INetworkDataset networkDataset = null;

			try
			{
				// Open Geodatabase and network dataset
				IWorkspace workspace = OpenWorkspace(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), @"ArcGIS\data\SanFrancisco\SanFrancisco.gdb"));
				networkDataset = OpenNetworkDataset(workspace, "Transportation", "Streets_ND");
				featureWorkspace = workspace as IFeatureWorkspace;
			}
			catch (Exception ex)
			{
				System.Windows.Forms.MessageBox.Show("Unable to open dataset. Error Message: " + ex.Message);
				this.Close();
				return;
			}

			// Create NAContext and NASolver
			CreateSolverContext(networkDataset);

            // Get available cost attributes from the network dataset
            INetworkAttribute networkAttribute;
			for (int i = 0; i < networkDataset.AttributeCount - 1; i++)
			{
				networkAttribute = networkDataset.get_Attribute(i);
				if (networkAttribute.UsageType == esriNetworkAttributeUsageType.esriNAUTCost)
				{
					cboCostAttribute.Items.Add(networkAttribute.Name);
				}
			}
            cboCostAttribute.SelectedIndex = 0;

			txtTargetFacility.Text = "1";
			txtCutOff.Text = "";

			// Load incidents from a feature class
			IFeatureClass inputFClass = featureWorkspace.OpenFeatureClass("Stores");
			LoadNANetworkLocations("Incidents", inputFClass, 500);

            // Load facilities from a feature class
            inputFClass = featureWorkspace.OpenFeatureClass("FireStations");
			LoadNANetworkLocations("Facilities", inputFClass, 500);

			//Create Layer for Network Dataset and add to ArcMap
            INetworkLayer networkLayer = new NetworkLayerClass();
            networkLayer.NetworkDataset = networkDataset;
            var layer = networkLayer as ILayer;
            layer.Name = "Network Dataset";
            axMapControl.AddLayer(layer, 0);

			//Create a Network Analysis Layer and add to ArcMap
			INALayer naLayer = m_NAContext.Solver.CreateLayer(m_NAContext);
			layer = naLayer as ILayer;
			layer.Name = m_NAContext.Solver.DisplayName;
			axMapControl.AddLayer(layer, 0);
		}

        #endregion

        #region Button Clicks

        /// <summary>
        /// Call the Closest Facility cost matrix solver and display the results
        /// </summary>
        /// <param name="sender">Sender of the event</param>
        /// <param name="e">Event</param>
        private void cmdSolve_Click(object sender, System.EventArgs e)
		{
            this.Cursor = Cursors.WaitCursor;
            lstOutput.Items.Clear();

            IGPMessages gpMessages = new GPMessagesClass();
            try
			{
                lstOutput.Items.Add("Solving...");

				SetSolverSettings();

				if (!m_NAContext.Solver.Solve(m_NAContext, gpMessages, null))
                    lstOutput.Items.Add("Partial Solve Generated.");

                DisplayOutput();
			}
			catch (Exception ee)
			{
                lstOutput.Items.Add("Failure: " + ee.Message);
 			}

            lstOutput.Items.Add(GetGPMessagesAsString(gpMessages));
            cmdSolve.Text = "Find Closest Facilities";

            RefreshMapDisplay();

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
        /// <returns>NAContext</returns>
        public void CreateSolverContext(INetworkDataset networkDataset)
        {
            if (networkDataset == null) return;

            //Get the Data Element
            IDENetworkDataset deNDS = GetDENetworkDataset(networkDataset);

            INASolver naSolver = new NAClosestFacilitySolver();
            m_NAContext = naSolver.CreateContext(deNDS, naSolver.Name);
            ((INAContextEdit)m_NAContext).Bind(networkDataset, new GPMessagesClass());
        }

        /// <summary>
        /// Set solver settings
        /// </summary>
        /// <param name="strNAClassName">NAClass name</param>
        /// <param name="inputFC">Input feature class</param>
        /// <param name="maxSnapTolerance">Max snap tolerance</param>
        public void LoadNANetworkLocations(string strNAClassName, IFeatureClass inputFC, double maxSnapTolerance)
        {
            INamedSet classes = m_NAContext.NAClasses;
            INAClass naClass = classes.get_ItemByName(strNAClassName) as INAClass;

            // delete existing Locations except if that a barriers
            naClass.DeleteAllRows();

            // Create a NAClassLoader and set the snap tolerance (meters unit)
            INAClassLoader classLoader = new NAClassLoader();
            classLoader.Locator = m_NAContext.Locator;
            if (maxSnapTolerance > 0) ((INALocator3)classLoader.Locator).MaxSnapTolerance = maxSnapTolerance;
            classLoader.NAClass = naClass;

            //Create field map to automatically map fields from input class to NAClass
            INAClassFieldMap fieldMap = new NAClassFieldMapClass();
            fieldMap.CreateMapping(naClass.ClassDefinition, inputFC.Fields);
            classLoader.FieldMap = fieldMap;

            // Avoid loading network locations onto non-traversable portions of elements
            INALocator3 locator = m_NAContext.Locator as INALocator3;
            locator.ExcludeRestrictedElements = true;
            locator.CacheRestrictedElements(m_NAContext);

            //Load Network Locations
            int rowsIn = 0;
            int rowsLocated = 0;
            IFeatureCursor featureCursor = inputFC.Search(null, true);
            classLoader.Load((ICursor)featureCursor, null, ref rowsIn, ref rowsLocated);

            //Message all of the network analysis agents that the analysis context has changed
            ((INAContextEdit)m_NAContext).ContextChanged();
        }

        #endregion

        #region Post-Solve

        /// <summary>
        /// Display analysis results in the list box
        /// </summary>
        public void DisplayOutput()
		{
            ITable table = m_NAContext.NAClasses.get_ItemByName(OUTPUTCLASSNAME) as ITable;
			if (table == null)
			{
                lstOutput.Items.Add("Impossible to get the " + OUTPUTCLASSNAME + " table");
			}
			lstOutput.Items.Add("Number facilities found " + table.RowCount(null).ToString());
			lstOutput.Items.Add("");
			if (table.RowCount(null) > 0)
			{
				lstOutput.Items.Add("IncidentID, FacilityID,FacilityRank,Total_" + cboCostAttribute.Text);
				double total_impedance;
				long incidentID;
				long facilityID;
				long facilityRank;
				ICursor cursor;
				IRow row;

				cursor = table.Search(null, false);
				row = cursor.NextRow();
				while (row != null)
				{
					incidentID = long.Parse(row.get_Value(table.FindField("IncidentID")).ToString());
					facilityID = long.Parse(row.get_Value(table.FindField("FacilityID")).ToString());
					facilityRank = long.Parse(row.get_Value(table.FindField("FacilityRank")).ToString());
					total_impedance = double.Parse(row.get_Value(table.FindField("Total_" + cboCostAttribute.Text)).ToString());
					lstOutput.Items.Add(incidentID.ToString() + ",\t" + facilityID.ToString() +
						",\t" + facilityRank.ToString() + ",\t" + total_impedance.ToString("F2"));

					row = cursor.NextRow();
				}
			}

			lstOutput.Refresh();
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

        /// <summary>
        /// Refresh the map display
        /// <summary>
        public void RefreshMapDisplay()
        {
            // Zoom to the extent of the service areas
            IGeoDataset geoDataset = m_NAContext.NAClasses.get_ItemByName(OUTPUTCLASSNAME) as IGeoDataset;
            IEnvelope envelope = geoDataset.Extent;
            if (!envelope.IsEmpty)
            {
                envelope.Expand(1.1, 1.1, true);
                axMapControl.Extent = envelope;
                m_NAContext.Solver.UpdateLayer(axMapControl.get_Layer(0) as INALayer);
            }
            axMapControl.Refresh();
        }

        #endregion

        #region Solver Settings

        /// <summary>
        /// Set solver settings
        /// </summary>
        public void SetSolverSettings()
		{
			//Set Route specific Settings
			INASolver naSolver = m_NAContext.Solver;

			INAClosestFacilitySolver cfSolver = naSolver as INAClosestFacilitySolver;
			if (txtCutOff.Text.Length > 0 && IsNumeric(txtCutOff.Text.Trim()))
				cfSolver.DefaultCutoff = txtCutOff.Text;
			else
				cfSolver.DefaultCutoff = null;

			if (txtTargetFacility.Text.Length > 0 && IsNumeric(txtTargetFacility.Text))
				cfSolver.DefaultTargetFacilityCount = int.Parse(txtTargetFacility.Text);
			else
				cfSolver.DefaultTargetFacilityCount = 1;

			cfSolver.OutputLines = esriNAOutputLineType.esriNAOutputLineTrueShapeWithMeasure;
			cfSolver.TravelDirection = esriNATravelDirection.esriNATravelDirectionToFacility;

			// Set generic solver settings
			// Set the impedance attribute
			INASolverSettings naSolverSettings;
			naSolverSettings = naSolver as INASolverSettings;
			naSolverSettings.ImpedanceAttributeName = cboCostAttribute.Text;

			// Set the OneWay Restriction if necessary
			IStringArray restrictions;
			restrictions = naSolverSettings.RestrictionAttributeNames;
			restrictions.RemoveAll();
			if (chkUseRestriction.Checked)
				restrictions.Add("oneway");

			naSolverSettings.RestrictionAttributeNames = restrictions;

			//Restrict UTurns
			naSolverSettings.RestrictUTurns = esriNetworkForwardStarBacktrack.esriNFSBNoBacktrack;
			naSolverSettings.IgnoreInvalidLocations = true;

			// Set the Hierarchy attribute
			naSolverSettings.UseHierarchy = chkUseHierarchy.Checked;
			if (naSolverSettings.UseHierarchy)
				naSolverSettings.HierarchyAttributeName = "HierarchyMultiNet";

			// Do not forget to update the context after you set your impedance
			naSolver.UpdateContext(m_NAContext, GetDENetworkDataset(m_NAContext.NetworkDataset), new GPMessagesClass());
		}

        /// <summary>
        /// Check whether a string represents a double value.
        /// </summary>
        /// <param name="str">String to test</param>
        /// <returns>bool</returns>
        private bool IsNumeric(string str)
        {
            try
            {
                double.Parse(str.Trim());
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        #endregion

	}
}