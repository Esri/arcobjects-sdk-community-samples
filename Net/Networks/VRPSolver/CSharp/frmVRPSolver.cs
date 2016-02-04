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
//       ArcGIS Network Analyst extension - VRP Solver Demonstration
//
//   This simple code shows how to:
//    1) Open a workspace and open a Network DataSet
//    2) Create a NAContext and its NASolver
//    3) Load Orders, Routes, Depots and Breaks from Feature Classes (or Table) and create Network Locations
//    4) Set the Solver parameters
//    5) Solve a VRP problem
//    6) Read the VRP output to display the Route and Break output information 
//************************************************************************************

using System;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.NetworkAnalyst;
using System.Text;


namespace VRP_CSharp
{
	public partial class frmVRPSolver : Form
	{
		private INAContext m_NAContext;
		private System.Collections.Hashtable m_unitTimeList;
		private System.Collections.Hashtable m_unitDistList;
        private readonly string OUTPUTCLASSNAME = "Routes";

		public frmVRPSolver()
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
				IWorkspace workspace = OpenWorkspace(Application.StartupPath + @"\..\..\..\..\..\Data\SanFrancisco\SanFrancisco.gdb");
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
			m_NAContext = CreateSolverContext(networkDataset);

			// Get available cost attributes from the network dataset
			INetworkAttribute networkAttribute;
			for (int i = 0; i < networkDataset.AttributeCount; i++)
			{
				networkAttribute = networkDataset.get_Attribute(i);
				if (networkAttribute.UsageType == esriNetworkAttributeUsageType.esriNAUTCost)
				{
					string unitType = GetAttributeUnitType(networkAttribute.Units);
					if (unitType == "Time")
					{
						comboTimeAttribute.Items.Add(networkAttribute.Name);
					}
					else if (unitType == "Distance")
					{
						comboDistanceAttribute.Items.Add(networkAttribute.Name);
					}
				}
			}
			comboTimeAttribute.SelectedIndex = 0;
			comboDistanceAttribute.SelectedIndex = 0;

			// Populate time field unit in comboBox
			m_unitTimeList = new System.Collections.Hashtable();
			m_unitTimeList.Add("Seconds", esriNetworkAttributeUnits.esriNAUSeconds);
			m_unitTimeList.Add("Minutes", esriNetworkAttributeUnits.esriNAUMinutes);
			foreach (System.Collections.DictionaryEntry timeUnit in m_unitTimeList)
			{
				comboTimeUnits.Items.Add(timeUnit.Key.ToString());
			}
			comboTimeUnits.SelectedIndex = 1;

			// Populate distance field unit in comboBox
			m_unitDistList = new System.Collections.Hashtable();
			m_unitDistList.Add("Miles", esriNetworkAttributeUnits.esriNAUMiles);
			m_unitDistList.Add("Meters", esriNetworkAttributeUnits.esriNAUMeters);
			foreach (System.Collections.DictionaryEntry distUnit in m_unitDistList)
			{
				comboDistUnits.Items.Add(distUnit.Key.ToString());
			}
			comboDistUnits.SelectedIndex = 0;

			// Populate time window importance attribute in comboBox
			comboTWImportance.Items.Add("High");
			comboTWImportance.Items.Add("Medium");
			comboTWImportance.Items.Add("Low");
			comboTWImportance.SelectedIndex = 0;

			// Load locations 
			IFeatureClass inputFClass = featureWorkspace.OpenFeatureClass("Stores");
			LoadNANetworkLocations("Orders", inputFClass as ITable);
			inputFClass = featureWorkspace.OpenFeatureClass("DistributionCenter");
			LoadNANetworkLocations("Depots", inputFClass as ITable);
			inputFClass = featureWorkspace.OpenFeatureClass("Routes");
			LoadNANetworkLocations("Routes", inputFClass as ITable);
			ITable inputTable = featureWorkspace.OpenTable("Breaks");
			LoadNANetworkLocations("Breaks", inputTable);

			// Create layer for network dataset and add to ArcMap
			INetworkLayer networkLayer = new NetworkLayerClass();
			networkLayer.NetworkDataset = networkDataset;
			ILayer layer = networkLayer as ILayer;
			layer.Name = "Network Dataset";
			axMapControl.AddLayer(layer, 0);

			// Create a network analysis layer and add to ArcMap
			INALayer naLayer = m_NAContext.Solver.CreateLayer(m_NAContext);
			layer = naLayer as ILayer;
			layer.Name = m_NAContext.Solver.DisplayName;
			axMapControl.AddLayer(layer, 0);
		}

		/// <summary>
		/// Call VRP solver and display the results
		/// </summary>
		/// <param name="sender">Sender of the event</param>
		/// <param name="e">Event</param>
		private void cmdSolve_Click(object sender, EventArgs e)
		{
            this.Cursor = Cursors.WaitCursor;
            listOutput.Items.Clear();

            IGPMessages gpMessages = new GPMessagesClass();
            try
			{
                listOutput.Items.Add("Solving...");

				SetSolverSettings();

				// Solve
				m_NAContext.Solver.Solve(m_NAContext, gpMessages, null);

				// Get the VRP output
                DisplayOutput();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}

            listOutput.Items.Add(GetGPMessagesAsString(gpMessages));
            cmdSolve.Text = "Find VRP Solution";

            RefreshMapDisplay();

            this.Cursor = Cursors.Default;
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

		/// <summary>
		/// Get the VRP route output
		/// </summary>
        public void DisplayOutput()
		{
			// Display route information
            ITable naTable = m_NAContext.NAClasses.get_ItemByName(OUTPUTCLASSNAME) as ITable;
			if (naTable.RowCount(null) > 0)
			{
				listOutput.Items.Add("Route Name,\tOrder Count,\tTotal Cost,\tTotal Time,\tTotal Distance,\tStart Time,\tEnd Time:");

				string routeName;
				long orderCount;
				double totalCost;
				double totalTime;
				double totalDistance;
				string routeStart;
				string routeEnd;
				ICursor naCursor = naTable.Search(null, false);
				IRow naRow = naCursor.NextRow();

				// Display route details
				while (naRow != null)
				{
					routeName = naRow.get_Value(naTable.FindField("Name")).ToString();
					orderCount = long.Parse(naRow.get_Value(naTable.FindField("OrderCount")).ToString());
					totalCost = double.Parse(naRow.get_Value(naTable.FindField("TotalCost")).ToString());
					totalTime = double.Parse(naRow.get_Value(naTable.FindField("TotalTime")).ToString());
					totalDistance = double.Parse(naRow.get_Value(naTable.FindField("TotalDistance")).ToString());
					routeStart = Convert.ToDateTime(naRow.get_Value(naTable.FindField("StartTime")).ToString()).ToString("T");
					routeEnd = Convert.ToDateTime(naRow.get_Value(naTable.FindField("EndTime")).ToString()).ToString("T");
					listOutput.Items.Add(routeName + ",\t\t" + orderCount.ToString() + ",\t\t" + totalCost.ToString("#0.00") + ",\t\t" + totalTime.ToString("#0.00")
						+ ",\t\t" + totalDistance.ToString("#0.00") + ",\t\t" + routeStart + ",\t\t" + routeEnd);
					naRow = naCursor.NextRow();
				}
			}

			listOutput.Items.Add("");

			// Display lunch break information
			ITable naBreakTable = m_NAContext.NAClasses.get_ItemByName("Breaks") as ITable;
			if (naBreakTable.RowCount(null) > 0)
			{
				listOutput.Items.Add("Route Name,\tBreak Start Time,\tBreak End Time:");
				ICursor naCursor = naBreakTable.Search(null, false);
				IRow naRow = naCursor.NextRow();
				string routeName;
				string startTime;
				string endTime;

				// Display lunch details for each route
				while (naRow != null)
				{
					routeName = naRow.get_Value(naBreakTable.FindField("RouteName")).ToString();
					startTime = Convert.ToDateTime(naRow.get_Value(naBreakTable.FindField("ArriveTime")).ToString()).ToString("T");
					endTime = Convert.ToDateTime(naRow.get_Value(naBreakTable.FindField("DepartTime")).ToString()).ToString("T");
					listOutput.Items.Add(routeName + ",\t\t" + startTime + ",\t\t" + endTime);

					naRow = naCursor.NextRow();
				}
			}

			listOutput.Refresh();
		}

        #region Geodatabase functions: open workspace and network dataset

        /// <summary>
        /// Geodatabase function: open workspace
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
        /// <param name="strNDSName">Dataset name</param>
        /// <returns>Network dataset</returns>
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

        #endregion

		#region Network analyst functions

		/// <summary>
		/// Create NASolver and NAContext
		/// </summary>
		/// <param name="networkDataset">Input network dataset</param>
		/// <returns>NAContext</returns>
		public INAContext CreateSolverContext(INetworkDataset networkDataset)
		{
			// Get the data element
			IDENetworkDataset deNDS = GetDENetworkDataset(networkDataset);
			INASolver naSolver = new NAVRPSolver();
			INAContextEdit contextEdit = naSolver.CreateContext(deNDS, naSolver.Name) as INAContextEdit;

			// Bind a context using the network dataset 
			contextEdit.Bind(networkDataset, new GPMessagesClass());

			return contextEdit as INAContext;
		}

		/// <summary>
		/// Load the input table and create field map to map fields from input table to NAClass
		/// </summary>
		/// <param name="strNAClassName">NAClass name</param>
		/// <param name="inputTable">Input table</param>
		public void LoadNANetworkLocations(string strNAClassName, ITable inputTable)
		{
			INamedSet classes = m_NAContext.NAClasses;
			INAClass naClass = classes.get_ItemByName(strNAClassName) as INAClass;

			// Delete existing rows from the specified NAClass
			naClass.DeleteAllRows();

			// Create a NAClassLoader and set the snap tolerance (meters unit)
			INAClassLoader loader = new NAClassLoader();
			loader.Locator = m_NAContext.Locator;
			loader.Locator.SnapTolerance = 100;
			loader.NAClass = naClass;

			// Create field map to automatically map fields from input table to NAclass
			INAClassFieldMap fieldMap = new NAClassFieldMapClass();
			fieldMap.CreateMapping(naClass.ClassDefinition, inputTable.Fields);
			loader.FieldMap = fieldMap;


			// Avoid loading network locations onto non-traversable portions of elements
			INALocator3 locator = m_NAContext.Locator as INALocator3;
			locator.ExcludeRestrictedElements = true;
			locator.CacheRestrictedElements(m_NAContext);

			// Load input table
			int rowsIn = 0;
			int rowsLocated = 0;
			loader.Load(inputTable.Search(null, true), null, ref rowsIn, ref rowsLocated);

			// Message all of the network analysis agents that the analysis context has changed.
			INAContextEdit naContextEdit = m_NAContext as INAContextEdit;
			naContextEdit.ContextChanged();
		}

		/// <summary>
		/// Set solver settings
		/// </summary>
		public void SetSolverSettings()
		{
			// Set VRP solver specific settings
			INASolver solver = m_NAContext.Solver;
			INAVRPSolver vrpSolver = solver as INAVRPSolver;

			// Both orders and routes have capacity count of 2 in the input shape files. User can modify the input data and update this value accordingly.
			vrpSolver.CapacityCount = 2;

			// Read the time and distance unit from comboBox
			vrpSolver.DistanceFieldUnits = (esriNetworkAttributeUnits)m_unitDistList[comboDistUnits.Items[comboDistUnits.SelectedIndex].ToString()];
			vrpSolver.TimeFieldUnits = (esriNetworkAttributeUnits)m_unitTimeList[comboTimeUnits.Items[comboTimeUnits.SelectedIndex].ToString()];

			// The value of time window violation penalty factor can be adjusted in terms of the user's preference.
			string importance = comboTWImportance.Items[comboTWImportance.SelectedIndex].ToString();
			if (importance == "Low")
				vrpSolver.TimeWindowViolationPenaltyFactor = 0;
			else if (importance == "Medium")
				vrpSolver.TimeWindowViolationPenaltyFactor = 1;
			else if (importance == "High")
				vrpSolver.TimeWindowViolationPenaltyFactor = 10;

			// Set output line type
			vrpSolver.OutputLines = esriNAOutputLineType.esriNAOutputLineStraight;

			// Set generic solver settings
			// Set the impedance attribute
			INASolverSettings solverSettings = solver as INASolverSettings;
			solverSettings.ImpedanceAttributeName = comboTimeAttribute.Text;

			// Set the accumulated attribute
			IStringArray accumulatedAttributes = solverSettings.AccumulateAttributeNames;
			accumulatedAttributes.RemoveAll();
			accumulatedAttributes.Insert(0, comboDistanceAttribute.Text);
			solverSettings.AccumulateAttributeNames = accumulatedAttributes;

			// Set the oneway restriction if necessary
			IStringArray restrictions = solverSettings.RestrictionAttributeNames;
			restrictions.RemoveAll();
			if (checkUseRestriction.Checked)
				restrictions.Add("oneway");
			solverSettings.RestrictionAttributeNames = restrictions;

			// Restrict UTurns
			solverSettings.RestrictUTurns = esriNetworkForwardStarBacktrack.esriNFSBNoBacktrack;

			// Set the hierarchy attribute
			solverSettings.UseHierarchy = checkUseHierarchy.Checked;
			if (solverSettings.UseHierarchy)
				solverSettings.HierarchyAttributeName = "HierarchyMultiNet";

			// Do not forget to update the context after you set your impedance
			solver.UpdateContext(m_NAContext, GetDENetworkDataset(m_NAContext.NetworkDataset), new GPMessagesClass());
		}

		#endregion

		/// <summary>
		/// Check whether the attribute unit is time or distance unit. 
		/// </summary>
		/// <param name="units">Input network attribute units</param>
		/// <returns>Unit type</returns>
		private string GetAttributeUnitType(esriNetworkAttributeUnits units)
		{
			string unitType = "";

			switch (units)
			{
				case esriNetworkAttributeUnits.esriNAUDays:
				case esriNetworkAttributeUnits.esriNAUHours:
				case esriNetworkAttributeUnits.esriNAUMinutes:
				case esriNetworkAttributeUnits.esriNAUSeconds:
					unitType = "Time";
					break;

				case esriNetworkAttributeUnits.esriNAUYards:
				case esriNetworkAttributeUnits.esriNAUMillimeters:
				case esriNetworkAttributeUnits.esriNAUMiles:
				case esriNetworkAttributeUnits.esriNAUMeters:
				case esriNetworkAttributeUnits.esriNAUKilometers:
				case esriNetworkAttributeUnits.esriNAUInches:
				case esriNetworkAttributeUnits.esriNAUFeet:
				case esriNetworkAttributeUnits.esriNAUDecimeters:
				case esriNetworkAttributeUnits.esriNAUNauticalMiles:
				case esriNetworkAttributeUnits.esriNAUCentimeters:
					unitType = "Distance";
					break;

				default:
					listOutput.Items.Add("Failed to find Network Attribute Units.");
					break;
			}

			return unitType;
		}

	}

}
