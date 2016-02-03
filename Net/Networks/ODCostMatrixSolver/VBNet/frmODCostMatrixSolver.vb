'*************************************************************************************
'       ArcGIS Network Analyst extension - OD Cost Matrix Demonstration

'   This simple code shows how to :
'    1) Open a workspace and open a Network DataSet
'    2) Create a NAContext and its NASolver
'    3) Load Origins/Destinations from Feature Classes and create Network Locations
'    4) Set the Solver parameters
'    5) Solve an OD Cost Matrix problem
'    6) Read the ODLines output to display the total number of routes found 
'       and the route details
'************************************************************************************

Imports System
Imports System.Windows.Forms
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.NetworkAnalyst


Partial Public Class frmODCostMatrixSolver
	Inherits Form
	Private m_NAContext As INAContext

	Public Sub New()

		'This call is required by the Windows Form Designer.
		InitializeComponent()

		'Add any initialization after the InitializeComponent() call
		Initialize()
	End Sub
	''' <summary>
    ''' Initialize the solver by calling the ArcGIS Network Analyst extension functions.
	''' </summary>
	Private Sub Initialize()
		Dim featureWorkspace As IFeatureWorkspace = Nothing
		Dim networkDataset As INetworkDataset = Nothing

		' Open Network Dataset
		Try
			Dim workspace As IWorkspace = OpenWorkspace(Application.StartupPath & "\..\..\..\..\..\Data\SanFrancisco\SanFrancisco.gdb")
			networkDataset = OpenNetworkDataset(workspace, "Transportation", "Streets_ND")
			featureWorkspace = TryCast(workspace, IFeatureWorkspace)
		Catch ex As Exception
			System.Windows.Forms.MessageBox.Show("Unable to open dataset. Error Message: " & ex.Message)
			Me.Close()
			Return
		End Try

		' Create NAContext and NASolver
		m_NAContext = CreateSolverContext(networkDataset)

		' Get available cost attributes from the network dataset
		Dim networkAttribute As INetworkAttribute
		For i As Integer = 0 To networkDataset.AttributeCount - 2
			networkAttribute = networkDataset.Attribute(i)
			If networkAttribute.UsageType = esriNetworkAttributeUsageType.esriNAUTCost Then
				comboCostAttribute.Items.Add(networkAttribute.Name)
			End If
		Next i
		comboCostAttribute.SelectedIndex = 0
		textTargetFacility.Text = ""
		textCutoff.Text = ""

		' Load locations from feature class
		Dim inputFClass As IFeatureClass = featureWorkspace.OpenFeatureClass("Stores")
		LoadNANetworkLocations("Origins", inputFClass, 100)

		inputFClass = featureWorkspace.OpenFeatureClass("Hospitals")
		LoadNANetworkLocations("Destinations", inputFClass, 100)

		' Create layer for network dataset and add to ArcMap
		Dim networkLayer As INetworkLayer = New NetworkLayerClass()
		networkLayer.NetworkDataset = networkDataset
		Dim layer As ILayer = TryCast(networkLayer, ILayer)
		layer.Name = "Network Dataset"
		axMapControl.AddLayer(layer, 0)

		' Create a network analysis layer and add to ArcMap
		Dim naLayer As INALayer = m_NAContext.Solver.CreateLayer(m_NAContext)
		layer = TryCast(naLayer, ILayer)
		layer.Name = m_NAContext.Solver.DisplayName
		axMapControl.AddLayer(layer, 0)
	End Sub

	''' <summary>
	''' Call the OD cost matrix solver and display the results
	''' </summary>
	''' <param name="sender">Sender of the event</param>
	''' <param name="e">Event</param>
	Private Sub cmdSolve_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdSolve.Click
		Try
			listOutput.Items.Clear()
			cmdSolve.Text = "Solving..."

			SetSolverSettings()

			' Solve
			Dim gpMessages As IGPMessages = New GPMessagesClass()
			If (Not m_NAContext.Solver.Solve(m_NAContext, gpMessages, Nothing)) Then
				' Get the ODLines output
				GetODOutput()
			Else
				listOutput.Items.Add("Partial Result")
			End If

			' Display Error/Warning/Informative Messages
			If gpMessages IsNot Nothing Then
				For i As Integer = 0 To gpMessages.Count - 1
					Select Case gpMessages.GetMessage(i).Type
						Case esriGPMessageType.esriGPMessageTypeError
							listOutput.Items.Add("Error " & gpMessages.GetMessage(i).ErrorCode.ToString() & " " & gpMessages.GetMessage(i).Description)
						Case esriGPMessageType.esriGPMessageTypeWarning
							listOutput.Items.Add("Warning " & gpMessages.GetMessage(i).Description)
						Case Else
							listOutput.Items.Add("Information " & gpMessages.GetMessage(i).Description)
					End Select
				Next i
			End If

			' Zoom to the extent of the route
			Dim gDataset As IGeoDataset = TryCast(m_NAContext.NAClasses.ItemByName("ODLines"), IGeoDataset)
			Dim envelope As IEnvelope = gDataset.Extent
			If (Not envelope.IsEmpty) Then
				envelope.Expand(1.1, 1.1, True)
				axMapControl.Extent = envelope
			End If

			axMapControl.Refresh()
		Catch ex As Exception
			MessageBox.Show(ex.Message)
		Finally
			cmdSolve.Text = "Find OD Cost Matrix"
		End Try
	End Sub

	''' <summary>
	''' Get the Impedance Cost from the ODLines Class Output
	''' </summary>
	Public Sub GetODOutput()
		Dim naTable As ITable = TryCast(m_NAContext.NAClasses.ItemByName("ODLines"), ITable)
		If naTable Is Nothing Then
			listOutput.Items.Add("Impossible to get the ODLines table")
		End If

		listOutput.Items.Add("Number of destinations found: " & naTable.RowCount(Nothing).ToString())
		listOutput.Items.Add("")

		If naTable.RowCount(Nothing) > 0 Then
			listOutput.Items.Add("OriginID, DestinationID, DestinationRank, Total_" & comboCostAttribute.Text)
			Dim total_impedance As Double
			Dim OriginID As Long
			Dim DestinationID As Long
			Dim DestinationRank As Long

			Dim naCursor As ICursor = naTable.Search(Nothing, False)
			Dim naRow As IRow = naCursor.NextRow()
			Do While naRow IsNot Nothing
				OriginID = Long.Parse(naRow.Value(naTable.FindField("OriginID")).ToString())
				DestinationID = Long.Parse(naRow.Value(naTable.FindField("DestinationID")).ToString())
				DestinationRank = Long.Parse(naRow.Value(naTable.FindField("DestinationRank")).ToString())
				total_impedance = Double.Parse(naRow.Value(naTable.FindField("Total_" & comboCostAttribute.Text)).ToString())
				listOutput.Items.Add(OriginID.ToString() & ", " & DestinationID.ToString() & ", " & DestinationRank.ToString() & ", " & total_impedance.ToString("#0.00"))

				naRow = naCursor.NextRow()
			Loop
		End If

		listOutput.Refresh()
	End Sub

#Region "Network Analyst functions"

	''' <summary>
	''' Create NASolver and NAContext
	''' </summary>
	''' <param name="networkDataset">Input network dataset</param>
	''' <returns>NAContext</returns>
	Public Function CreateSolverContext(ByVal networkDataset As INetworkDataset) As INAContext
		'Get the Data Element
		Dim deNDS As IDENetworkDataset = GetDENetworkDataset(networkDataset)
		Dim naSolver As INASolver = New NAODCostMatrixSolver()
		Dim contextEdit As INAContextEdit = TryCast(naSolver.CreateContext(deNDS, naSolver.Name), INAContextEdit)
		'Bind a context using the network Dataset 
		contextEdit.Bind(networkDataset, New GPMessagesClass())

		Return TryCast(contextEdit, INAContext)
	End Function

	''' <summary>
	''' Set Solver Settings
	''' </summary>
	''' <param name="strNAClassName">NAClass name</param>
	''' <param name="inputFC">Input feature class</param>
	''' <param name="snapTolerance">Snap tolerance</param>
	Public Sub LoadNANetworkLocations(ByVal strNAClassName As String, ByVal inputFC As IFeatureClass, ByVal snapTolerance As Double)
		Dim classes As INamedSet = m_NAContext.NAClasses
		Dim naClass As INAClass = TryCast(classes.ItemByName(strNAClassName), INAClass)

		' Delete existing locations from the specified NAClass
		naClass.DeleteAllRows()

		' Create a NAClassLoader and set the snap tolerance (meters unit)
		Dim loader As INAClassLoader = New NAClassLoader()
		loader.Locator = m_NAContext.Locator
		If snapTolerance > 0 Then
			loader.Locator.SnapTolerance = snapTolerance
		End If
		loader.NAClass = naClass

		' Create field map to automatically map fields from input class to NAclass
		Dim fieldMap As INAClassFieldMap = New NAClassFieldMapClass()
		fieldMap.CreateMapping(naClass.ClassDefinition, inputFC.Fields)
		loader.FieldMap = fieldMap

		' Avoid loading network locations onto non-traversable portions of elements
		Dim locator As INALocator3 = TryCast(m_NAContext.Locator, INALocator3)
		locator.ExcludeRestrictedElements = True
		locator.CacheRestrictedElements(m_NAContext)

		' Load network locations
		Dim rowsIn As Integer = 0
		Dim rowsLocated As Integer = 0
		loader.Load(CType(inputFC.Search(Nothing, True), ICursor), Nothing, rowsIn, rowsLocated)

		' Message all of the network analysis agents that the analysis context has changed.
		Dim naContextEdit As INAContextEdit = TryCast(m_NAContext, INAContextEdit)
		naContextEdit.ContextChanged()
	End Sub

	''' <summary>
	''' Set Solver Settings
	''' </summary>
	Public Sub SetSolverSettings()
		' Set OD Solver specific settings
		Dim solver As INASolver = m_NAContext.Solver
		Dim odSolver As INAODCostMatrixSolver = TryCast(solver, INAODCostMatrixSolver)
		If textCutoff.Text.Length > 0 AndAlso IsNumeric(textCutoff.Text.Trim()) Then
			odSolver.DefaultCutoff = textCutoff.Text
		Else
			odSolver.DefaultCutoff = Nothing
		End If

		If textTargetFacility.Text.Length > 0 AndAlso IsNumeric(textTargetFacility.Text.Trim()) Then
			odSolver.DefaultTargetDestinationCount = textTargetFacility.Text
		Else
			odSolver.DefaultTargetDestinationCount = Nothing
		End If

		odSolver.OutputLines = esriNAOutputLineType.esriNAOutputLineStraight

		' Set generic solver settings
		' Set the impedance attribute
		Dim solverSettings As INASolverSettings = TryCast(solver, INASolverSettings)
		solverSettings.ImpedanceAttributeName = comboCostAttribute.Text

		' Set the OneWay restriction if necessary
		Dim restrictions As IStringArray = solverSettings.RestrictionAttributeNames
		restrictions.RemoveAll()
		If checkUseRestriction.Checked Then
			restrictions.Add("oneway")
		End If
		solverSettings.RestrictionAttributeNames = restrictions

		' Restrict UTurns
		solverSettings.RestrictUTurns = esriNetworkForwardStarBacktrack.esriNFSBNoBacktrack
		solverSettings.IgnoreInvalidLocations = True

		' Set the hierarchy attribute
		solverSettings.UseHierarchy = checkUseHierarchy.Checked
		If solverSettings.UseHierarchy Then
			solverSettings.HierarchyAttributeName = "hierarchy"
		End If

		' Do not forget to update the context after you set your impedance
		solver.UpdateContext(m_NAContext, GetDENetworkDataset(m_NAContext.NetworkDataset), New GPMessagesClass())
	End Sub

	''' <summary>
	''' Geodatabase function: open work space
	''' </summary>
	''' <param name="strGDBName">Input file name</param>
	''' <returns>Workspace</returns>
	Public Function OpenWorkspace(ByVal strGDBName As String) As IWorkspace
		' As Workspace Factories are Singleton objects, they must be instantiated with the Activator
		Dim workspaceFactory As IWorkspaceFactory = TryCast(Activator.CreateInstance(Type.GetTypeFromProgID("esriDataSourcesGDB.FileGDBWorkspaceFactory")), IWorkspaceFactory)
		Return workspaceFactory.OpenFromFile(strGDBName, 0)
	End Function

	''' <summary>
	''' Geodatabase function: open network Dataset
	''' </summary>
	''' <param name="workspace">Input workspace</param>
	''' <param name="strNDSName">Input network dataset name</param>
	''' <returns></returns>
	Public Function OpenNetworkDataset(ByVal workspace As IWorkspace, ByVal featureDatasetName As String, ByVal strNDSName As String) As INetworkDataset
		' Obtain the dataset container from the workspace
		Dim featureWorkspace As IFeatureWorkspace = TryCast(workspace, IFeatureWorkspace)
		Dim featureDataset As IFeatureDataset = featureWorkspace.OpenFeatureDataset(featureDatasetName)
		Dim featureDatasetExtensionContainer As IFeatureDatasetExtensionContainer = TryCast(featureDataset, IFeatureDatasetExtensionContainer)
		Dim featureDatasetExtension As IFeatureDatasetExtension = featureDatasetExtensionContainer.FindExtension(ESRI.ArcGIS.Geodatabase.esriDatasetType.esriDTNetworkDataset)
		Dim datasetContainer3 As IDatasetContainer3 = TryCast(featureDatasetExtension, IDatasetContainer3)

		' Use the container to open the network dataset
		Dim dataset As Object = datasetContainer3.DatasetByName(ESRI.ArcGIS.Geodatabase.esriDatasetType.esriDTNetworkDataset, strNDSName)
		Return TryCast(dataset, INetworkDataset)
	End Function

	''' <summary>
	''' Geodatabase function: get network Dataset
	''' </summary>
	''' <param name="networkDataset">Input network dataset</param>
	''' <returns>DE network dataset</returns>
	Public Function GetDENetworkDataset(ByVal networkDataset As INetworkDataset) As IDENetworkDataset
		' Cast from the Network Dataset to the DatasetComponent
		Dim dsComponent As IDatasetComponent = TryCast(networkDataset, IDatasetComponent)

		' Get the data element
		Return TryCast(dsComponent.DataElement, IDENetworkDataset)
	End Function
#End Region

	''' <summary>
	''' Check whether a string represents a double value.
	''' </summary>
	''' <param name="str"></param>
	''' <returns></returns>
	Private Function IsNumeric(ByVal str As String) As Boolean
		Try
			Double.Parse(str.Trim())
		Catch e1 As Exception
			Return False
		End Try

		Return True
	End Function
End Class

