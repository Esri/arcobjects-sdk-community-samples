'Copyright 2016 Esri

'Licensed under the Apache License, Version 2.0 (the "License");
'you may not use this file except in compliance with the License.
'You may obtain a copy of the License at

'    http://www.apache.org/licenses/LICENSE-2.0

'Unless required by applicable law or agreed to in writing, software
'distributed under the License is distributed on an "AS IS" BASIS,
'WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.

'See the License for the specific language governing permissions and
'limitations under the License.
'*************************************************************************************
'       ArcGIS Network Analyst extension - VRP Solver Demonstration
'
'   This simple code shows how to:
'    1) Open a workspace and open a Network DataSet
'    2) Create a NAContext and its NASolver
'    3) Load Orders, Routes, Depots and Breaks from Feature Classes (or Table) and create Network Locations
'    4) Set the Solver parameters
'    5) Solve a VRP problem
'    6) Read the VRP output to display the Route and Break output information 
'************************************************************************************
Imports Microsoft.VisualBasic
Imports System
Imports System.Windows.Forms
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.NetworkAnalyst
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Geometry

Partial Public Class frmVRPSolver
	Inherits Form
	Private m_NAContext As INAContext
	Private m_unitTimeList As System.Collections.Hashtable
	Private m_unitDistList As System.Collections.Hashtable

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

		' Open network dataset
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
		For i As Integer = 0 To networkDataset.AttributeCount - 1
			networkAttribute = networkDataset.Attribute(i)
			If networkAttribute.UsageType = esriNetworkAttributeUsageType.esriNAUTCost Then
				Dim unitType As String = GetAttributeUnitType(networkAttribute.Units)
				If unitType = "Time" Then
					comboTimeAttribute.Items.Add(networkAttribute.Name)
				ElseIf unitType = "Distance" Then
					comboDistanceAttribute.Items.Add(networkAttribute.Name)
				End If
			End If
		Next i
		comboTimeAttribute.SelectedIndex = 0
		comboDistanceAttribute.SelectedIndex = 0

		' Populate time field unit in comboBox
		m_unitTimeList = New System.Collections.Hashtable()
		m_unitTimeList.Add("Seconds", esriNetworkAttributeUnits.esriNAUSeconds)
		m_unitTimeList.Add("Minutes", esriNetworkAttributeUnits.esriNAUMinutes)
		For Each timeUnit As System.Collections.DictionaryEntry In m_unitTimeList
			comboTimeUnits.Items.Add(timeUnit.Key.ToString())
		Next timeUnit
		comboTimeUnits.SelectedIndex = 1

		' Populate distance field unit in comboBox
		m_unitDistList = New System.Collections.Hashtable()
		m_unitDistList.Add("Miles", esriNetworkAttributeUnits.esriNAUMiles)
		m_unitDistList.Add("Meters", esriNetworkAttributeUnits.esriNAUMeters)
		For Each distUnit As System.Collections.DictionaryEntry In m_unitDistList
			comboDistUnits.Items.Add(distUnit.Key.ToString())
		Next distUnit
		comboDistUnits.SelectedIndex = 0

		' Populate time window importance attribute in comboBox
		comboTWImportance.Items.Add("High")
		comboTWImportance.Items.Add("Medium")
		comboTWImportance.Items.Add("Low")
		comboTWImportance.SelectedIndex = 0

		' Load locations 
		Dim inputFClass As IFeatureClass = featureWorkspace.OpenFeatureClass("Stores")
		LoadNANetworkLocations("Orders", TryCast(inputFClass, ITable))
		inputFClass = featureWorkspace.OpenFeatureClass("DistributionCenter")
		LoadNANetworkLocations("Depots", TryCast(inputFClass, ITable))
		inputFClass = featureWorkspace.OpenFeatureClass("Routes")
		LoadNANetworkLocations("Routes", TryCast(inputFClass, ITable))
		Dim inputTable As ITable = featureWorkspace.OpenTable("Breaks")
		LoadNANetworkLocations("Breaks", inputTable)

		' Create layer for network dataset and add to ArcMap
		Dim networkLayer As INetworkLayer = New NetworkLayerClass()
		networkLayer.NetworkDataset = networkDataset
		Dim layer As ILayer = TryCast(networkLayer, ILayer)
		layer.Name = "Network Dataset"
		AxMapControl.AddLayer(layer, 0)

		' Create a network analysis layer and add to ArcMap
		Dim naLayer As INALayer = m_NAContext.Solver.CreateLayer(m_NAContext)
		layer = TryCast(naLayer, ILayer)
		layer.Name = m_NAContext.Solver.DisplayName
		AxMapControl.AddLayer(layer, 0)
	End Sub

	''' <summary>
	''' Call VRP solver and display the results
	''' </summary>
	''' <param name="sender">Sender of the event</param>
	''' <param name="e">Event</param>
	Private Sub cmdSolve_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdSolve.Click

        Dim gpMessages As IGPMessages = New GPMessagesClass()
        Try
            listOutput.Items.Clear()
            cmdSolve.Text = "Solving..."

            SetSolverSettings()

            ' Solve
            m_NAContext.Solver.Solve(m_NAContext, gpMessages, Nothing)

            ' Get the VRP output
            DisplayOutput()

            ' Zoom to the extent of the routes
            Dim gDataset As IGeoDataset = TryCast(m_NAContext.NAClasses.ItemByName("Routes"), IGeoDataset)
            Dim envelope As IEnvelope = gDataset.Extent
            If (Not envelope.IsEmpty) Then
                envelope.Expand(1.1, 1.1, True)
                axMapControl.Extent = envelope
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            cmdSolve.Text = "Find VRP Solution"
        End Try

        listOutput.Items.Add(GetGPMessagesAsString(GPMessages))
    End Sub

    '*********************************************************************************
    ' Gather the error/warning/informative messages from GPMessages
    '*********************************************************************************
    Public Function GetGPMessagesAsString(ByVal gpMessages As IGPMessages) As String

        Dim messages As System.Text.StringBuilder = New System.Text.StringBuilder()
        If Not gpMessages Is Nothing Then
            Dim i As Integer
            For i = 0 To gpMessages.Count - 1
                Dim gpMessage As IGPMessage = gpMessages.GetMessage(i)
                Dim message As String = gpMessage.Description
                Select Case gpMessage.Type
                    Case esriGPMessageType.esriGPMessageTypeError
                        messages.AppendLine("Error " + gpMessage.ErrorCode.ToString + ": " + message)
                    Case esriGPMessageType.esriGPMessageTypeWarning
                        messages.AppendLine("Warning: " + message)
                    Case Else
                        messages.AppendLine("Information: " + message)
                End Select
            Next
        End If

        Return messages.ToString()
    End Function

    ''' <summary>
    ''' Get the VRP route output
    ''' </summary>
    Public Sub DisplayOutput()
        ' Display route information
        Dim naTable As ITable = TryCast(m_NAContext.NAClasses.ItemByName("Routes"), ITable)
        If naTable.RowCount(Nothing) > 0 Then
            listOutput.Items.Add("Route Name," & Constants.vbTab & "Order Count," & Constants.vbTab & "Total Cost," & Constants.vbTab & "Total Time," & Constants.vbTab & "Total Distance," & Constants.vbTab & "Start Time," & Constants.vbTab & "End Time:")

            Dim routeName As String
            Dim orderCount As Long
            Dim totalCost As Double
            Dim totalTime As Double
            Dim totalDistance As Double
            Dim routeStart As String
            Dim routeEnd As String
            Dim naCursor As ICursor = naTable.Search(Nothing, False)
            Dim naRow As IRow = naCursor.NextRow()

            ' Display route details
            Do While Not naRow Is Nothing
                routeName = naRow.Value(naTable.FindField("Name")).ToString()
                orderCount = Long.Parse(naRow.Value(naTable.FindField("OrderCount")).ToString())
                totalCost = Double.Parse(naRow.Value(naTable.FindField("TotalCost")).ToString())
                totalTime = Double.Parse(naRow.Value(naTable.FindField("TotalTime")).ToString())
                totalDistance = Double.Parse(naRow.Value(naTable.FindField("TotalDistance")).ToString())
                routeStart = Convert.ToDateTime(naRow.Value(naTable.FindField("StartTime")).ToString()).ToString("T")
                routeEnd = Convert.ToDateTime(naRow.Value(naTable.FindField("EndTime")).ToString()).ToString("T")
                listOutput.Items.Add(routeName & "," & Constants.vbTab + Constants.vbTab + orderCount.ToString() & "," & Constants.vbTab + Constants.vbTab + totalCost.ToString("#0.00") & "," & Constants.vbTab + Constants.vbTab + totalTime.ToString("#0.00") & "," & Constants.vbTab + Constants.vbTab + totalDistance.ToString("#0.00") & "," & Constants.vbTab + Constants.vbTab & routeStart & "," & Constants.vbTab + Constants.vbTab & routeEnd)
                naRow = naCursor.NextRow()
            Loop
        End If

        listOutput.Items.Add("")

        ' Display lunch break information
        Dim naBreakTable As ITable = TryCast(m_NAContext.NAClasses.ItemByName("Breaks"), ITable)
        If naBreakTable.RowCount(Nothing) > 0 Then
            listOutput.Items.Add("Route Name," & Constants.vbTab & "Break Start Time," & Constants.vbTab & "Break End Time:")
            Dim naCursor As ICursor = naBreakTable.Search(Nothing, False)
            Dim naRow As IRow = naCursor.NextRow()
            Dim routeName As String
            Dim startTime As String
            Dim endTime As String

            ' Display lunch details for each route
            Do While Not naRow Is Nothing
                routeName = naRow.Value(naBreakTable.FindField("RouteName")).ToString()
                startTime = Convert.ToDateTime(naRow.Value(naBreakTable.FindField("ArriveTime")).ToString()).ToString("T")
                endTime = Convert.ToDateTime(naRow.Value(naBreakTable.FindField("DepartTime")).ToString()).ToString("T")
                listOutput.Items.Add(routeName & "," & Constants.vbTab + Constants.vbTab & startTime & "," & Constants.vbTab + Constants.vbTab & endTime)
                naRow = naCursor.NextRow()
            Loop
        End If

        listOutput.Refresh()
    End Sub


#Region "Network analyst functions"

	''' <summary>
	''' Create NASolver and NAContext
	''' </summary>
	''' <param name="networkDataset">Input network dataset</param>
	''' <returns>NAContext</returns>
	Public Function CreateSolverContext(ByVal networkDataset As INetworkDataset) As INAContext
		' Get the data element
		Dim deNDS As IDENetworkDataset = GetDENetworkDataset(networkDataset)
		Dim naSolver As INASolver = New NAVRPSolver()
		Dim contextEdit As INAContextEdit = TryCast(naSolver.CreateContext(deNDS, naSolver.Name), INAContextEdit)

		' Bind a context using the network dataset 
		contextEdit.Bind(networkDataset, New GPMessagesClass())

		Return TryCast(contextEdit, INAContext)
	End Function

	''' <summary>
	''' Load the input table and create field map to map fields from input table to NAClass
	''' </summary>
	''' <param name="strNAClassName">NAClass name</param>
	''' <param name="inputTable">Input table</param>
	Public Sub LoadNANetworkLocations(ByVal strNAClassName As String, ByVal inputTable As ITable)
		Dim classes As INamedSet = m_NAContext.NAClasses
		Dim naClass As INAClass = TryCast(classes.ItemByName(strNAClassName), INAClass)

		' Delete existing rows from the specified NAClass
		naClass.DeleteAllRows()

		' Create a NAClassLoader and set the snap tolerance (meters unit)
		Dim loader As INAClassLoader = New NAClassLoader()
		loader.Locator = m_NAContext.Locator
		loader.Locator.SnapTolerance = 100
		loader.NAClass = naClass

		' Create field map to automatically map fields from input table to NAclass
		Dim fieldMap As INAClassFieldMap = New NAClassFieldMapClass()
		fieldMap.CreateMapping(naClass.ClassDefinition, inputTable.Fields)
		loader.FieldMap = fieldMap

		' Avoid loading network locations onto non-traversable portions of elements
		Dim locator As INALocator3 = TryCast(m_NAContext.Locator, INALocator3)
		locator.ExcludeRestrictedElements = True
		locator.CacheRestrictedElements(m_NAContext)

		' Load input table
		Dim rowsIn As Integer = 0
		Dim rowsLocated As Integer = 0
		loader.Load(inputTable.Search(Nothing, True), Nothing, rowsIn, rowsLocated)

		' Message all of the network analysis agents that the analysis context has changed.
		Dim naContextEdit As INAContextEdit = TryCast(m_NAContext, INAContextEdit)
		naContextEdit.ContextChanged()
	End Sub

	''' <summary>
	''' Set solver settings
	''' </summary>
	Public Sub SetSolverSettings()
		' Set VRP solver specific settings
		Dim solver As INASolver = m_NAContext.Solver
		Dim vrpSolver As INAVRPSolver = TryCast(solver, INAVRPSolver)

		' Both orders and routes have capacity count of 2 in the input shape files. User can modify the input data and update this value accordingly.
		vrpSolver.CapacityCount = 2

		' Read the time and distance unit from comboBox
		vrpSolver.DistanceFieldUnits = CType(m_unitDistList(comboDistUnits.Items(comboDistUnits.SelectedIndex).ToString()), esriNetworkAttributeUnits)
		vrpSolver.TimeFieldUnits = CType(m_unitTimeList(comboTimeUnits.Items(comboTimeUnits.SelectedIndex).ToString()), esriNetworkAttributeUnits)

		' The value of time window violation penalty factor can be adjusted in terms of the user's preference.
		Dim importance As String = comboTWImportance.Items(comboTWImportance.SelectedIndex).ToString()
		If importance = "Low" Then
			vrpSolver.TimeWindowViolationPenaltyFactor = 0
		ElseIf importance = "Medium" Then
			vrpSolver.TimeWindowViolationPenaltyFactor = 1
		ElseIf importance = "High" Then
			vrpSolver.TimeWindowViolationPenaltyFactor = 10
		End If

		' Set output line type
		vrpSolver.OutputLines = esriNAOutputLineType.esriNAOutputLineStraight

		' Set generic solver settings
		' Set the impedance attribute
		Dim solverSettings As INASolverSettings = TryCast(solver, INASolverSettings)
		solverSettings.ImpedanceAttributeName = comboTimeAttribute.Text

		' Set the accumulated attribute
		Dim accumulatedAttributes As IStringArray = solverSettings.AccumulateAttributeNames
		accumulatedAttributes.RemoveAll()
		accumulatedAttributes.Insert(0, comboDistanceAttribute.Text)
		solverSettings.AccumulateAttributeNames = accumulatedAttributes

		' Set the oneway restriction if necessary
		Dim restrictions As IStringArray = solverSettings.RestrictionAttributeNames
		restrictions.RemoveAll()
		If checkUseRestriction.Checked Then
			restrictions.Add("oneway")
		End If
		solverSettings.RestrictionAttributeNames = restrictions

		' Restrict UTurns
		solverSettings.RestrictUTurns = esriNetworkForwardStarBacktrack.esriNFSBNoBacktrack

		' Set the hierarchy attribute
		solverSettings.UseHierarchy = checkUseHierarchy.Checked
		If solverSettings.UseHierarchy Then
            solverSettings.HierarchyAttributeName = "HierarchyMultiNet"
		End If

		' Do not forget to update the context after you set your impedance
		solver.UpdateContext(m_NAContext, GetDENetworkDataset(m_NAContext.NetworkDataset), New GPMessagesClass())
	End Sub

#End Region


#Region "Geodatabase functions: open workspace and network dataset"

	''' <summary>
	''' Geodatabase function: open workspace
	''' </summary>
	''' <param name="strGDBName">File name</param>
	''' <returns>Workspace</returns>
	Public Function OpenWorkspace(ByVal strGDBName As String) As IWorkspace
		' As Workspace Factories are Singleton objects, they must be instantiated with the Activator
		Dim workspaceFactory As IWorkspaceFactory = TryCast(Activator.CreateInstance(Type.GetTypeFromProgID("esriDataSourcesGDB.FileGDBWorkspaceFactory")), IWorkspaceFactory)
		Return workspaceFactory.OpenFromFile(strGDBName, 0)
	End Function

	''' <summary>
	''' Geodatabase function: open network dataset
	''' </summary>
	''' <param name="workspace">Work space</param>
	''' <param name="strNDSName">Dataset name</param>
	''' <returns>Network dataset</returns>
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
	''' Geodatabase function: get network dataset
	''' </summary>
	''' <param name="networkDataset">Input network dataset</param>
	''' <returns>DE network dataset</returns>
	Public Function GetDENetworkDataset(ByVal networkDataset As INetworkDataset) As IDENetworkDataset
		' Cast from the network dataset to the DatasetComponent
		Dim dsComponent As IDatasetComponent = TryCast(networkDataset, IDatasetComponent)

		' Get the data element
		Return TryCast(dsComponent.DataElement, IDENetworkDataset)
	End Function

#End Region

	''' <summary>
	''' Check whether the attribute unit is time or distance unit. 
	''' </summary>
	''' <param name="units">Input network attribute units</param>
	''' <returns>Unit type</returns>
	Private Function GetAttributeUnitType(ByVal units As esriNetworkAttributeUnits) As String
		Dim unitType As String = ""

		Select Case units
			Case esriNetworkAttributeUnits.esriNAUDays, esriNetworkAttributeUnits.esriNAUHours, esriNetworkAttributeUnits.esriNAUMinutes, esriNetworkAttributeUnits.esriNAUSeconds
				unitType = "Time"

			Case esriNetworkAttributeUnits.esriNAUYards, esriNetworkAttributeUnits.esriNAUMillimeters, esriNetworkAttributeUnits.esriNAUMiles, esriNetworkAttributeUnits.esriNAUMeters, esriNetworkAttributeUnits.esriNAUKilometers, esriNetworkAttributeUnits.esriNAUInches, esriNetworkAttributeUnits.esriNAUFeet, esriNetworkAttributeUnits.esriNAUDecimeters, esriNetworkAttributeUnits.esriNAUNauticalMiles, esriNetworkAttributeUnits.esriNAUCentimeters
				unitType = "Distance"

			Case Else
				listOutput.Items.Add("Failed to find Network Attribute Units.")
		End Select

		Return unitType
	End Function

End Class
