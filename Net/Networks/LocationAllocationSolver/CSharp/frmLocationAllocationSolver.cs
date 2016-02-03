//*************************************************************************************
//       ArcGIS Network Analyst extension - Location-Allocation Demonstration
//
//   This simple code shows how to :
//    1) Open a workspace and open a Network Dataset
//    2) Create a NAContext and its NASolver
//    3) Load Facilites/DemandPoints from Feature Classes and create Network Locations
//    4) Set the Solver parameters
//    5) Solve a Location-Allocation problem
//    6) Read the Facilities and LALines output to display the facilities chosen
//       and the list the demand points allocated
//************************************************************************************

using System;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.NetworkAnalyst;
using System.Text;

namespace LocationAllocationSolver
{
	public partial class frmLocationAllocationSolver : Form
	{
		private INAContext m_NAContext;
		private string m_ProblemType = "Minimize Impedance";

		public frmLocationAllocationSolver()
		{
			InitializeComponent();
			Initialize();
		}

		//*********************************************************************************
		// Initialize the form, create a NA context, load some locations and draw the map
		//*********************************************************************************
		private void Initialize()
		{
			// Open Geodatabase and network dataset
			IFeatureWorkspace featureWorkspace = OpenWorkspace(Application.StartupPath + @"\..\..\..\..\..\Data\SanFrancisco\SanFrancisco.gdb") as IFeatureWorkspace;
			INetworkDataset networkDataset = OpenNetworkDataset(featureWorkspace as IWorkspace, "Transportation", "Streets_ND");

			// Create NAContext and NASolver
			m_NAContext = CreateSolverContext(networkDataset);

			// Get Cost Attributes and populate the combo drop down box
			INetworkAttribute networkAttribute;
			for (int i = 0; i < networkDataset.AttributeCount - 1; i++)
			{
				networkAttribute = networkDataset.get_Attribute(i);
				if (networkAttribute.UsageType == esriNetworkAttributeUsageType.esriNAUTCost)
				{
					cboCostAttribute.Items.Add(networkAttribute.Name);
					cboCostAttribute.SelectedIndex = 0;
				}
			}

			// Set the default number of facilities to solve for
			txtFacilitiesToLocate.Text = "1";

			// Set up the no cutoff for the Minimize Impedance case.
			// See the cboProblemType_SelectedIndexChanged routine for how this is managed for other problem types
			txtCutOff.Text = "<None>";

			// Populate combo box with Location-Allocation problem types
			cboProblemType.Items.Add("Minimize Impedance");
			cboProblemType.Items.Add("Maximize Coverage");
            cboProblemType.Items.Add("Maximize Capacitated Coverage");
            cboProblemType.Items.Add("Minimize Facilities");
			cboProblemType.Items.Add("Maximize Attendance");
			cboProblemType.Items.Add("Maximize Market Share");
			cboProblemType.Items.Add("Target Market Share");
			cboProblemType.Text = "Minimize Impedance";
			m_ProblemType = "Minimize Impedance";

			// Populate combo box with Impedance Transformation choices
			cboImpTransformation.Items.Add("Linear");
			cboImpTransformation.Items.Add("Power");
			cboImpTransformation.Items.Add("Exponential");
			cboImpTransformation.Text = "Linear";

			// Set the default impedance transformation parameter
			txtImpParameter.Text = "1.0";

			// Set up the default percentage for the Target Market Share problem type
			txtTargetMarketShare.Text = "10.0";

            // Set up the default capacity
            txtDefaultCapacity.Text = "1.0";

			// Load facility locations from feature class
			IFeatureClass inputFClass = featureWorkspace.OpenFeatureClass("CandidateStores");
			LoadNANetworkLocations("Facilities", inputFClass, 500);

			// Load demand point locations from feature class
			inputFClass = featureWorkspace.OpenFeatureClass("TractCentroids");
			LoadNANetworkLocations("DemandPoints", inputFClass, 500);

			// Create Layer for Network Dataset and add to Ax Map Control
			ILayer layer;
			INetworkLayer networkLayer;
			networkLayer = new NetworkLayerClass();
			networkLayer.NetworkDataset = networkDataset;
			layer = networkLayer as ILayer;
			layer.Name = "Network Dataset";
			axMapControl.AddLayer(layer, 0);

			// Create a Network Analysis Layer and add to Ax Map Control
			INALayer naLayer = m_NAContext.Solver.CreateLayer(m_NAContext);
			layer = naLayer as ILayer;
			layer.Name = m_NAContext.Solver.DisplayName;
			axMapControl.AddLayer(layer, 0);
		}

		//*********************************************************************************
		// ArcGIS Network Analyst extension functions
		// ********************************************************************************

		//*********************************************************************************
		// Create NASolver and NAContext
		//*********************************************************************************
		public INAContext CreateSolverContext(INetworkDataset networkDataset)
		{
			//Get the Data Element
			IDENetworkDataset deNDS = GetDENetworkDataset(networkDataset);

			INASolver naSolver = new NALocationAllocationSolverClass();
			INAContextEdit contextEdit = naSolver.CreateContext(deNDS, naSolver.Name) as INAContextEdit;
			contextEdit.Bind(networkDataset, new GPMessagesClass());
			return contextEdit as INAContext;
		}

		//*********************************************************************************
		// Load Network Locations
		//*********************************************************************************
		public void LoadNANetworkLocations(string strNAClassName, IFeatureClass inputFC, double maxSnapTolerance)
		{
			INamedSet classes = m_NAContext.NAClasses;
			INAClass naClass = classes.get_ItemByName(strNAClassName) as INAClass;

			// Delete existing Locations before loading new ones
			naClass.DeleteAllRows();

			// Create a NAClassLoader and set the snap tolerance (meters unit)
			INAClassLoader classLoader = new NAClassLoader();
			classLoader.Locator = m_NAContext.Locator;
            if (maxSnapTolerance > 0) ((INALocator3)classLoader.Locator).MaxSnapTolerance = maxSnapTolerance;
            classLoader.NAClass = naClass;

			//Create field map to automatically map fields from input class to NAClass
			INAClassFieldMap fieldMap = new NAClassFieldMap();
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

		//*********************************************************************************
		// Set Solver Settings for the Location-Allocation Solver
		//*********************************************************************************
		public void SetSolverSettings()
		{
			//Set Location-Allocation specific Settings
			INASolver naSolver = m_NAContext.Solver;

			INALocationAllocationSolver2 laSolver = naSolver as INALocationAllocationSolver2;

			// Set number of facilities to locate
			if (txtFacilitiesToLocate.Text.Length > 0 && IsNumeric(txtFacilitiesToLocate.Text))
				laSolver.NumberFacilitiesToLocate = int.Parse(txtFacilitiesToLocate.Text);
			else
				laSolver.NumberFacilitiesToLocate = 1;

			// Set impedance cutoff
			if (txtCutOff.Text.Length > 0 && IsNumeric(txtCutOff.Text.Trim()))
				laSolver.DefaultCutoff = txtCutOff.Text;
			else
				laSolver.DefaultCutoff = null;

			// Set up Location-Allocation problem type
			if (cboProblemType.Text.Equals("Maximize Attendance"))
				laSolver.ProblemType = esriNALocationAllocationProblemType.esriNALAPTMaximizeAttendance;
			else if (cboProblemType.Text.Equals("Maximize Coverage"))
				laSolver.ProblemType = esriNALocationAllocationProblemType.esriNALAPTMaximizeCoverage;
            else if (cboProblemType.Text.Equals("Maximize Capacitated Coverage"))
                laSolver.ProblemType = esriNALocationAllocationProblemType.esriNALAPTMaximizeCapacitatedCoverage;
            else if (cboProblemType.Text.Equals("Minimize Facilities"))
				laSolver.ProblemType = esriNALocationAllocationProblemType.esriNALAPTMaximizeCoverageMinimizeFacilities;
			else if (cboProblemType.Text.Equals("Maximize Market Share"))
				laSolver.ProblemType = esriNALocationAllocationProblemType.esriNALAPTMaximizeMarketShare;
			else if (cboProblemType.Text.Equals("Minimize Impedance"))
				laSolver.ProblemType = esriNALocationAllocationProblemType.esriNALAPTMinimizeWeightedImpedance;
			else if (cboProblemType.Text.Equals("Target Market Share"))
				laSolver.ProblemType = esriNALocationAllocationProblemType.esriNALAPTTargetMarketShare;
			else
				laSolver.ProblemType = esriNALocationAllocationProblemType.esriNALAPTMinimizeWeightedImpedance;

			// Set Impedance Transformation type
			if (cboImpTransformation.Text.Equals("Linear"))
				laSolver.ImpedanceTransformation = esriNAImpedanceTransformationType.esriNAITTLinear;
			else if (cboImpTransformation.Text.Equals("Power"))
				laSolver.ImpedanceTransformation = esriNAImpedanceTransformationType.esriNAITTPower;
			else if (cboImpTransformation.Text.Equals("Exponential"))
				laSolver.ImpedanceTransformation = esriNAImpedanceTransformationType.esriNAITTExponential;

			// Set Impedance Transformation Parameter (distance decay beta)
			if (txtImpParameter.Text.Length > 0 && IsNumeric(txtCutOff.Text.Trim()))
				laSolver.TransformationParameter = double.Parse(txtImpParameter.Text);
			else
				laSolver.TransformationParameter = 1.0;

			// Set target market share percentage (should be between 0.0 and 100.0)
			if (txtTargetMarketShare.Text.Length > 0 && IsNumeric(txtCutOff.Text.Trim()))
			{
				double targetPercentage;
				targetPercentage = double.Parse(txtTargetMarketShare.Text);

				if ((targetPercentage <= 0.0) || (targetPercentage > 100.0))
				{
					targetPercentage = 10.0;
					lstOutput.Items.Add("Target percentage out of range. Reset to 10%");
				}
				laSolver.TargetMarketSharePercentage = targetPercentage;
				txtTargetMarketShare.Text = laSolver.TargetMarketSharePercentage.ToString();
			}
			else
				laSolver.TargetMarketSharePercentage = 10.0;

            // Set default capacity
            if (txtDefaultCapacity.Text.Length > 0 && IsNumeric(txtDefaultCapacity.Text.Trim()))
            {
                double defaultCapacity;
                defaultCapacity = double.Parse(txtDefaultCapacity.Text);

                if ((defaultCapacity <= 0.0))
                {
                    defaultCapacity = 1.0;
                    lstOutput.Items.Add("Default capacity must be greater than zero.");
                }
                laSolver.DefaultCapacity = defaultCapacity;
                txtDefaultCapacity.Text = laSolver.DefaultCapacity.ToString();
            }
            else
                laSolver.DefaultCapacity = 1.0;


			// Set any other solver settings
			laSolver.OutputLines = esriNAOutputLineType.esriNAOutputLineStraight;
			laSolver.TravelDirection = esriNATravelDirection.esriNATravelDirectionFromFacility;

			// Set the impedance attribute
			INASolverSettings naSolverSettings = naSolver as INASolverSettings;

			naSolverSettings.ImpedanceAttributeName = cboCostAttribute.Text;

			naSolverSettings.IgnoreInvalidLocations = true;

			// Do not forget to update the context after you set your impedance
			naSolver.UpdateContext(m_NAContext, GetDENetworkDataset(m_NAContext.NetworkDataset), new GPMessagesClass());
		}

		//*********************************************************************************
		//Get Located Facilities information from the Facilities Class and summarize some statistics
		//*********************************************************************************
		public void GetLAFacilitiesOutput(string strNAClass)
		{
			ITable table = m_NAContext.NAClasses.get_ItemByName(strNAClass) as ITable;
			if (table == null)
				lstOutput.Items.Add("Impossible to get the " + strNAClass + " table");

			if (table.RowCount(null) > 0)
			{
				ICursor cursor;
				IRow row;
				string facilityName;
				double demandWeight, total_impedance;
				long demandCount;
				long facilityCount = 0;
				long sumDemand = 0;
				double sumWeightedDistance = 0.0;

				cursor = table.Search(null, false);
				row = cursor.NextRow();
				while (row != null)
				{
					demandCount = long.Parse(row.get_Value(table.FindField("DemandCount")).ToString());
					if (demandCount > 0)
					{
						facilityCount = facilityCount + 1;
						facilityName = row.get_Value(table.FindField("Name")).ToString();
						demandWeight = double.Parse(row.get_Value(table.FindField("DemandWeight")).ToString());
						total_impedance = double.Parse(row.get_Value(table.FindField("TotalWeighted_" + cboCostAttribute.Text)).ToString());
						sumWeightedDistance = sumWeightedDistance + total_impedance;
						sumDemand = sumDemand + demandCount;
					}
					row = cursor.NextRow();
				}
				lstOutput.Items.Add("Number of facilities Located " + facilityCount.ToString());
				lstOutput.Items.Add("Number of demand points Allocated " + sumDemand.ToString());
				lstOutput.Items.Add("Sum of weighted " + cboCostAttribute.Text + " " + sumWeightedDistance.ToString());
				lstOutput.Items.Add("");
			}
			lstOutput.Refresh();
		}

		//*********************************************************************************
		// Get the Impedance Cost form the LALines Class Output and list out the allocation
		//*********************************************************************************
		public void GetLALinesOutput(string strNAClass)
		{
			ITable table = m_NAContext.NAClasses.get_ItemByName(strNAClass) as ITable;
			if (table == null)
			{
				lstOutput.Items.Add("Impossible to get the " + strNAClass + " table");
			}
			lstOutput.Items.Add("Allocation Table:");
			if (table.RowCount(null) > 0)
			{
				lstOutput.Items.Add("DemandID,FacilityID,Weight,TotalWeighted_" + cboCostAttribute.Text);
				double total_impedance;
				long demandID;
				long facilityID;
				double facilityWeight;
				ICursor cursor;
				IRow row;

				cursor = table.Search(null, false);
				row = cursor.NextRow();
				while (row != null)
				{
					facilityID = long.Parse(row.get_Value(table.FindField("FacilityID")).ToString());
					demandID = long.Parse(row.get_Value(table.FindField("DemandID")).ToString());
					facilityWeight = double.Parse(row.get_Value(table.FindField("Weight")).ToString());
					total_impedance = double.Parse(row.get_Value(table.FindField("TotalWeighted_" + cboCostAttribute.Text)).ToString());
					lstOutput.Items.Add(demandID.ToString() + ",\t" + facilityID.ToString() +
					  ",\t" + facilityWeight.ToString() + ",\t" + total_impedance.ToString("F2"));
					row = cursor.NextRow();
				}
			}
			lstOutput.Refresh();
		}

		//*********************************************************************************
		// Geodatabase functions
		// ********************************************************************************
		public IWorkspace OpenWorkspace(string strGDBName)
		{
			// As Workspace Factories are Singleton objects, they must be instantiated with the Activator
			var workspaceFactory = System.Activator.CreateInstance(System.Type.GetTypeFromProgID("esriDataSourcesGDB.FileGDBWorkspaceFactory")) as ESRI.ArcGIS.Geodatabase.IWorkspaceFactory;
			return workspaceFactory.OpenFromFile(strGDBName, 0);
		}

		//*********************************************************************************
		// Open the network dataset
		//*********************************************************************************
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

		public IDENetworkDataset GetDENetworkDataset(INetworkDataset networkDataset)
		{
			// Cast from the Network Dataset to the DatasetComponent
			IDatasetComponent dsComponent = networkDataset as IDatasetComponent;

			// Get the Data Element
			return dsComponent.DataElement as IDENetworkDataset;
		}

		private bool IsNumeric(string str)
		{
			try
			{
				double.Parse(str.Trim());
			}
			catch (Exception) { return false; }
			return true;
		}

		//*********************************************************************************
		// Read the solver settings from the user and solve the Location-Allocation problem
		//*********************************************************************************
		private void cmdSolve_Click(object sender, EventArgs e)
		{
            IGPMessages gpMessages = new GPMessagesClass();
            try
			{
				lstOutput.Items.Clear();
				lstOutput.Items.Add("Solving...");

				SetSolverSettings();

				// Solve
				if (!m_NAContext.Solver.Solve(m_NAContext, gpMessages, null))
				{
					GetLAFacilitiesOutput("Facilities");
					GetLALinesOutput("LALines");
				}
				else
					lstOutput.Items.Add("Partial Result");

				//Zoom to the extent of the route
				IGeoDataset geoDataset = m_NAContext.NAClasses.get_ItemByName("LALines") as IGeoDataset;
				IEnvelope envelope = geoDataset.Extent;
				if (!envelope.IsEmpty)
					envelope.Expand(1.1, 1.1, true);
				axMapControl.Extent = envelope;
				axMapControl.Refresh();
			}

			catch (Exception ee)
			{
                lstOutput.Items.Add("Failure: " + ee.Message);
            }

            lstOutput.Items.Add(GetGPMessagesAsString(gpMessages));
            cmdSolve.Text = "Solve";
        }

        //*********************************************************************************
        // Gather the error/warning/informative messages from GPMessages
        //*********************************************************************************
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

		//*********************************************************************************
		// Manage the cutoff, either <None> or some intelligent default
		//*********************************************************************************
		private void cboProblemType_SelectedIndexChanged(object sender, EventArgs e)
		{
			// All problem types except Minimize Impedance need an impedance cutoff.
			// So manage an intelligent default other than <None> for them.
			// If cutoff is set and problem type switches back to Minimize Impedance, reset cutoff to <None>
			// Note: 3.0 is ok for Minutes but if impedance is something else like Meters, 3.0 will be too small
			// and cause some solver errors like "Value does not fall within the expected range."
			if (cboProblemType.Text.Equals("Minimize Impedance"))
			{
				if (!m_ProblemType.Equals("Minimize Impedance"))
					if (!txtCutOff.Text.Equals("<None>"))
						txtCutOff.Text = "<None>";
			}
			else
				if (txtCutOff.Text.Equals("<None>"))
					txtCutOff.Text = "3.0";

			m_ProblemType = cboProblemType.Text;
		}
	}
}
