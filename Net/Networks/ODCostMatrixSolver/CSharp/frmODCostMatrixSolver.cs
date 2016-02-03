//*************************************************************************************
//       ArcGIS Network Analyst extension - OD Cost Matrix Demonstration
//
//   This simple code shows how to :
//    1) Open a workspace and open a Network DataSet
//    2) Create a NAContext and its NASolver
//    3) Load Origins/Destinations from Feature Classes and create Network Locations
//    4) Set the Solver parameters
//    5) Solve an OD Cost Matrix problem
//    6) Read the ODLines output to display the total number of routes found 
//       and the route details
//************************************************************************************

using System;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.NetworkAnalyst;

namespace OD_Cost_Matrix_CSharp
{
	public partial class frmODCostMatrixSolver : Form
	{
		private INAContext m_NAContext;

		public frmODCostMatrixSolver()
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
				// Open the Network Dataset
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
			for (int i = 0; i < networkDataset.AttributeCount - 1; i++)
			{
				networkAttribute = networkDataset.get_Attribute(i);
				if (networkAttribute.UsageType == esriNetworkAttributeUsageType.esriNAUTCost)
				{
					comboCostAttribute.Items.Add(networkAttribute.Name);
				}
			}
			comboCostAttribute.SelectedIndex = 0;

			textTargetFacility.Text = "";
			textCutoff.Text = "";

			// Load locations from feature class
			IFeatureClass inputFClass = featureWorkspace.OpenFeatureClass("Stores");
			LoadNANetworkLocations("Origins", inputFClass, 100);
			inputFClass = featureWorkspace.OpenFeatureClass("Hospitals");
			LoadNANetworkLocations("Destinations", inputFClass, 5100);

			// Create layer for network dataset and add to ArcMap
			INetworkLayer networkLayer = new NetworkLayerClass();
			networkLayer.NetworkDataset = networkDataset;
			var layer = networkLayer as ILayer;
			layer.Name = "Network Dataset";
			axMapControl.AddLayer(layer, 0);

			// Create a network analysis layer and add to ArcMap
			INALayer naLayer = m_NAContext.Solver.CreateLayer(m_NAContext);
			layer = naLayer as ILayer;
			layer.Name = m_NAContext.Solver.DisplayName;
			axMapControl.AddLayer(layer, 0);
		}

		/// <summary>
		/// Call the OD cost matrix solver and display the results
		/// </summary>
		/// <param name="sender">Sender of the event</param>
		/// <param name="e">Event</param>
		private void cmdSolve_Click(object sender, EventArgs e)
		{
            IGPMessages gpMessages = new GPMessagesClass();
            try
			{
				lstOutput.Items.Clear();
				cmdSolve.Text = "Solving...";

				SetSolverSettings();

				// Solve
				if (!m_NAContext.Solver.Solve(m_NAContext, gpMessages, null))
					lstOutput.Items.Add("Partial Result");

				DisplayOutput();

				// Zoom to the extent of the route
				IGeoDataset gDataset = m_NAContext.NAClasses.get_ItemByName("ODLines") as IGeoDataset;
				IEnvelope envelope = gDataset.Extent;
				if (!envelope.IsEmpty)
				{
					envelope.Expand(1.1, 1.1, true);
					axMapControl.Extent = envelope;
				}

				axMapControl.Refresh();
			}
			catch (Exception ex)
			{
                lstOutput.Items.Add("Failure: " + ex.Message);
            }

            lstOutput.Items.Add(GetGPMessagesAsString(gpMessages));
            cmdSolve.Text = "Find OD Cost Matrix";
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
		/// Display analysis results in the list box
		/// </summary>
		public void DisplayOutput()
		{
			ITable naTable = m_NAContext.NAClasses.get_ItemByName("ODLines") as ITable;
			if (naTable == null)
				lstOutput.Items.Add("Impossible to get the ODLines table");

			lstOutput.Items.Add("Number of destinations found: " + naTable.RowCount(null).ToString());
			lstOutput.Items.Add("");

			if (naTable.RowCount(null) > 0)
			{
				lstOutput.Items.Add("OriginID, DestinationID, DestinationRank, Total_" + comboCostAttribute.Text);
				double total_impedance;
				long OriginID;
				long DestinationID;
				long DestinationRank;

				ICursor naCursor = naTable.Search(null, false);
				IRow naRow = naCursor.NextRow();
				while (naRow != null)
				{
					OriginID = long.Parse(naRow.get_Value(naTable.FindField("OriginID")).ToString());
					DestinationID = long.Parse(naRow.get_Value(naTable.FindField("DestinationID")).ToString());
					DestinationRank = long.Parse(naRow.get_Value(naTable.FindField("DestinationRank")).ToString());
					total_impedance = double.Parse(naRow.get_Value(naTable.FindField("Total_" + comboCostAttribute.Text)).ToString());
					lstOutput.Items.Add(OriginID.ToString() + ", " + DestinationID.ToString() + ", " +
						DestinationRank.ToString() + ", " + total_impedance.ToString("#0.00"));

					naRow = naCursor.NextRow();
				}
			}

			lstOutput.Refresh();
		}

		#region Network analyst functions

		/// <summary>
		/// Create NASolver and NAContext
		/// </summary>
		/// <param name="networkDataset">Input network dataset</param>
		/// <returns>NAContext</returns>
		public INAContext CreateSolverContext(INetworkDataset networkDataset)
		{
			//Get the data element
			IDENetworkDataset deNDS = GetDENetworkDataset(networkDataset);
			INASolver naSolver = new NAODCostMatrixSolver();
			INAContextEdit contextEdit = naSolver.CreateContext(deNDS, naSolver.Name) as INAContextEdit;
			//Bind a context using the network dataset 
			contextEdit.Bind(networkDataset, new GPMessagesClass());

			return contextEdit as INAContext;
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

			// Delete existing locations from the specified NAClass
			naClass.DeleteAllRows();

			// Create a NAClassLoader and set the snap tolerance (meters unit)
            INAClassLoader classLoader = new NAClassLoader();
            classLoader.Locator = m_NAContext.Locator;
            if (maxSnapTolerance > 0) ((INALocator3)classLoader.Locator).MaxSnapTolerance = maxSnapTolerance;
            classLoader.NAClass = naClass;

			// Create field map to automatically map fields from input class to NAClass
			INAClassFieldMap fieldMap = new NAClassFieldMapClass();
			fieldMap.CreateMapping(naClass.ClassDefinition, inputFC.Fields);
            classLoader.FieldMap = fieldMap;

			// Avoid loading network locations onto non-traversable portions of elements
			INALocator3 locator = m_NAContext.Locator as INALocator3;
			locator.ExcludeRestrictedElements = true;
			locator.CacheRestrictedElements(m_NAContext);

			// Load network locations
			int rowsIn = 0;
			int rowsLocated = 0;
            classLoader.Load((ICursor)inputFC.Search(null, true), null, ref rowsIn, ref rowsLocated);

			// Message all of the network analysis agents that the analysis context has changed.
            ((INAContextEdit)m_NAContext).ContextChanged();
        }

		/// <summary>
		/// Set solver settings
		/// </summary>
		public void SetSolverSettings()
		{
			// Set OD solver specific settings
			INASolver solver = m_NAContext.Solver;

			INAODCostMatrixSolver odSolver = solver as INAODCostMatrixSolver;
			if (textCutoff.Text.Length > 0 && IsNumeric(textCutoff.Text.Trim()))
				odSolver.DefaultCutoff = textCutoff.Text;
			else
				odSolver.DefaultCutoff = null;

			if (textTargetFacility.Text.Length > 0 && IsNumeric(textTargetFacility.Text.Trim()))
				odSolver.DefaultTargetDestinationCount = textTargetFacility.Text;
			else
				odSolver.DefaultTargetDestinationCount = null;

			odSolver.OutputLines = esriNAOutputLineType.esriNAOutputLineStraight;

			// Set generic solver settings
			// Set the impedance attribute
			INASolverSettings solverSettings = solver as INASolverSettings;
			solverSettings.ImpedanceAttributeName = comboCostAttribute.Text;

			// Set the OneWay restriction if necessary
			IStringArray restrictions = solverSettings.RestrictionAttributeNames;
			restrictions.RemoveAll();
			if (checkUseRestriction.Checked)
				restrictions.Add("oneway");
			solverSettings.RestrictionAttributeNames = restrictions;

			// Restrict UTurns
			solverSettings.RestrictUTurns = esriNetworkForwardStarBacktrack.esriNFSBNoBacktrack;
			solverSettings.IgnoreInvalidLocations = true;

			// Set the hierarchy attribute
			solverSettings.UseHierarchy = checkUseHierarchy.Checked;
			if (solverSettings.UseHierarchy)
                solverSettings.HierarchyAttributeName = "HierarchyMultiNet";

			// Do not forget to update the context after you set your impedance
			solver.UpdateContext(m_NAContext, GetDENetworkDataset(m_NAContext.NetworkDataset), new GPMessagesClass());
		}

		/// <summary>
		/// Geodatabase function: open work space
		/// </summary>
		/// <param name="strGDBName">Input file name</param>
		/// <returns>Workspace</returns>
		public IWorkspace OpenWorkspace(string strGDBName)
		{
			// As Workspace Factories are Singleton objects, they must be instantiated with the Activator
			var workspaceFactory = System.Activator.CreateInstance(System.Type.GetTypeFromProgID("esriDataSourcesGDB.FileGDBWorkspaceFactory")) as ESRI.ArcGIS.Geodatabase.IWorkspaceFactory;
			return workspaceFactory.OpenFromFile(strGDBName, 0);
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

		#endregion

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
	}
}
