'*************************************************************************************
'       ArcGIS Network Analyst extension - Location-Allocation Demonstration
'
'   This simple code shows how to :
'    1) Open a workspace and open a Network DataSet
'    2) Create a NAContext and its NASolver
'    3) Load Incidents/Facilites from Feature Classes and create Network Locations
'    4) Set the Solver parameters
'    5) Solve a Location-Allocation problem
'    6) Read the Facilities and LALines output to display the facilities chosen
'       and the list the demand points allocated
'************************************************************************************

Imports System
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.NetworkAnalyst
Imports Microsoft.VisualBasic

Public Class frmLocationAllocationSolver
    Private m_NAContext As INAContext
    Private m_ProblemType As String = "Minimize Impedance"

    Public Sub New()
        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        Initialize()
    End Sub

    '*********************************************************************************
    ' Initialize the form, create a NA context, load some locations and draw the map
    '*********************************************************************************
    Private Sub Initialize()
        ' Open geodatabase and network dataset
        Dim featureWorkspace As IFeatureWorkspace = Nothing
        Dim networkDataset As INetworkDataset = Nothing

        Try
            ' Open the geodatabase and network dataset
            Dim workspace As IWorkspace = OpenWorkspace(Application.StartupPath & "\..\..\..\..\..\Data\SanFrancisco\SanFrancisco.gdb")
            networkDataset = OpenNetworkDataset(workspace, "Transportation", "Streets_ND")
            featureWorkspace = TryCast(workspace, IFeatureWorkspace)
        Catch ex As Exception
            Windows.Forms.MessageBox.Show("Unable to open dataset. Error Message: " + ex.Message)
            Me.Close()
            Return
        End Try

        ' Create NAContext and NASolver
        m_NAContext = CreateSolverContext(networkDataset)

        ' Get Cost Attributes and populate the combo drop down box
        Dim networkAttribute As INetworkAttribute
        For i As Integer = 0 To networkDataset.AttributeCount - 2
            networkAttribute = networkDataset.Attribute(i)
            If networkAttribute.UsageType = esriNetworkAttributeUsageType.esriNAUTCost Then
                cboCostAttribute.Items.Add(networkAttribute.Name)
                cboCostAttribute.SelectedIndex = 0
            End If
        Next i

        ' Set the default number of facilities to solve for
        txtFacilitiesToLocate.Text = "1"

        ' Set up the no cutoff for the Minimize Impedance case.
        ' See the cboProblemType_SelectedIndexChanged routine for how this is managed for other problem types
        txtCutOff.Text = "<None>"

        ' Load facility locations from FC
        Dim inputFClass As IFeatureClass = featureWorkspace.OpenFeatureClass("CandidateStores")
        LoadNANetworkLocations("Facilities", inputFClass, 500)

        ' Load demand point locations from FC
        inputFClass = featureWorkspace.OpenFeatureClass("TractCentroids")
        LoadNANetworkLocations("DemandPoints", inputFClass, 500)

        ' Populate combo box with Location-Allocation problem types
        cboProblemType.Items.Add("Minimize Impedance")
        cboProblemType.Items.Add("Maximize Coverage")
        cboProblemType.Items.Add("Maximize Capacitated Coverage")
        cboProblemType.Items.Add("Minimize Facilities")
        cboProblemType.Items.Add("Maximize Attendance")
        cboProblemType.Items.Add("Maximize Market Share")
        cboProblemType.Items.Add("Target Market Share")
        cboProblemType.Text = "Minimize Impedance"
        m_ProblemType = "Minimize Impedance"

        ' Populate combo box with Impedance Transformation choices
        cboImpTransformation.Items.Add("Linear")
        cboImpTransformation.Items.Add("Power")
        cboImpTransformation.Items.Add("Exponential")
        cboImpTransformation.Text = "Linear"

        ' Set the default impedance transformation parameter
        txtImpParameter.Text = "1.0"

        ' Set up the default percentage for the Target Market Share problem type
        txtTargetMarketShare.Text = "10.0"

        ' Set up the default capacity
        txtDefaultCapacity.Text = "1.0"

        ' Create Layer for Network Dataset and add to Ax Map Control
        Dim networkLayer As INetworkLayer = New NetworkLayerClass
        networkLayer.NetworkDataset = networkDataset
        Dim layer As ILayer = TryCast(networkLayer, ILayer)
        layer.Name = "Network Dataset"
        axMapControl.AddLayer(layer, 0)

        ' Create a Network Analysis Layer and add to Ax Map Control
        Dim naLayer As INALayer = m_NAContext.Solver.CreateLayer(m_NAContext)
        layer = TryCast(naLayer, ILayer)
        layer.Name = m_NAContext.Solver.DisplayName
        axMapControl.AddLayer(layer, 0)
    End Sub

    '*********************************************************************************
    ' ArcGIS Network Analyst extension functions
    ' ********************************************************************************

    '*********************************************************************************
    ' Create NASolver and NAContext
    '*********************************************************************************
    Public Function CreateSolverContext(ByVal networkDataset As INetworkDataset) As INAContext
        'Get the Data Element
        Dim deNDS As IDENetworkDataset = GetDENetworkDataset(networkDataset)

        Dim naSolver As INASolver = New NALocationAllocationSolverClass()
        Dim contextEdit As INAContextEdit = TryCast(naSolver.CreateContext(deNDS, naSolver.Name), INAContextEdit)
        contextEdit.Bind(networkDataset, New GPMessagesClass())
        Return TryCast(contextEdit, INAContext)
    End Function

    '*********************************************************************************
    ' Load Network Locations
    '*********************************************************************************
    Public Sub LoadNANetworkLocations(ByVal strNAClassName As String, ByVal inputFC As IFeatureClass, ByVal maxSnapTolerance As Double)
        Dim classes As INamedSet = m_NAContext.NAClasses
        Dim naClass As INAClass = TryCast(classes.ItemByName(strNAClassName), INAClass)

        ' Delete existing Locations before loading new ones
        naClass.DeleteAllRows()

        ' Avoid loading network locations onto non-traversable portions of elements
        Dim locator As INALocator3 = TryCast(m_NAContext.Locator, INALocator3)
        locator.ExcludeRestrictedElements = True
        locator.CacheRestrictedElements(m_NAContext)

        ' Create a NAClassLoader and set the maximum snap tolerance (meters unit)
        Dim classLoader As INAClassLoader = New NAClassLoader
        classLoader.Locator = m_NAContext.Locator
        If maxSnapTolerance > 0 Then
            locator.MaxSnapTolerance = maxSnapTolerance
        End If
        classLoader.NAClass = naClass

        'Create field map to automatically map fields from input class to naclass
        Dim fieldMap As INAClassFieldMap = New NAClassFieldMap()
        fieldMap.CreateMapping(naClass.ClassDefinition, inputFC.Fields)
        classLoader.FieldMap = fieldMap

        'Load Network Locations
        Dim rowsIn As Integer = 0
        Dim rowsLocated As Integer = 0
        Dim featureCursor As IFeatureCursor = inputFC.Search(Nothing, True)
        classLoader.Load(CType(featureCursor, ICursor), Nothing, rowsIn, rowsLocated)

        'Message all of the network analysis agents that the analysis context has changed
        CType(m_NAContext, INAContextEdit).ContextChanged()
    End Sub

    '*********************************************************************************
    ' Set Solver Settings
    '*********************************************************************************
    Public Sub SetSolverSettings()
        'Set Location-Allocation specific Settings
        Dim naSolver As INASolver = m_NAContext.Solver

        Dim laSolver As INALocationAllocationSolver2 = TryCast(naSolver, INALocationAllocationSolver2)

        ' Set number of facilities to locate
        If txtFacilitiesToLocate.Text.Length > 0 AndAlso IsNumeric(txtFacilitiesToLocate.Text) Then
            laSolver.NumberFacilitiesToLocate = Integer.Parse(txtFacilitiesToLocate.Text)
        Else
            laSolver.NumberFacilitiesToLocate = 1
        End If

        ' Set impedance cutoff
        If txtCutOff.Text.Length > 0 AndAlso IsNumeric(txtCutOff.Text.Trim()) Then
            laSolver.DefaultCutoff = txtCutOff.Text
        Else
            laSolver.DefaultCutoff = Nothing
        End If

        ' Set up Location-Allocation problem type
        If cboProblemType.Text.Equals("Maximize Attendance") Then
            laSolver.ProblemType = esriNALocationAllocationProblemType.esriNALAPTMaximizeAttendance
        ElseIf cboProblemType.Text.Equals("Maximize Coverage") Then
            laSolver.ProblemType = esriNALocationAllocationProblemType.esriNALAPTMaximizeCoverage
        ElseIf cboProblemType.Text.Equals("Maximize Capacitated Coverage") Then
            laSolver.ProblemType = esriNALocationAllocationProblemType.esriNALAPTMaximizeCapacitatedCoverage
        ElseIf cboProblemType.Text.Equals("Minimize Facilities") Then
            laSolver.ProblemType = esriNALocationAllocationProblemType.esriNALAPTMaximizeCoverageMinimizeFacilities
        ElseIf cboProblemType.Text.Equals("Maximize Market Share") Then
            laSolver.ProblemType = esriNALocationAllocationProblemType.esriNALAPTMaximizeMarketShare
        ElseIf cboProblemType.Text.Equals("Minimize Impedance") Then
            laSolver.ProblemType = esriNALocationAllocationProblemType.esriNALAPTMinimizeWeightedImpedance
        ElseIf cboProblemType.Text.Equals("Target Market Share") Then
            laSolver.ProblemType = esriNALocationAllocationProblemType.esriNALAPTTargetMarketShare
        Else
            laSolver.ProblemType = esriNALocationAllocationProblemType.esriNALAPTMinimizeWeightedImpedance
        End If

        ' Set Impedance Transformation type
        If cboImpTransformation.Text.Equals("Linear") Then
            laSolver.ImpedanceTransformation = esriNAImpedanceTransformationType.esriNAITTLinear
        ElseIf cboImpTransformation.Text.Equals("Power") Then
            laSolver.ImpedanceTransformation = esriNAImpedanceTransformationType.esriNAITTPower
        ElseIf cboImpTransformation.Text.Equals("Exponential") Then
            laSolver.ImpedanceTransformation = esriNAImpedanceTransformationType.esriNAITTExponential
        End If

        ' Set Impedance Transformation Parameter (distance decay beta)
        If txtImpParameter.Text.Length > 0 AndAlso IsNumeric(txtCutOff.Text.Trim()) Then
            laSolver.TransformationParameter = Double.Parse(txtImpParameter.Text)
        Else
            laSolver.TransformationParameter = 1.0
        End If

        ' Set target market share percentage (should be between 0.0 and 100.0)
        If txtTargetMarketShare.Text.Length > 0 AndAlso IsNumeric(txtCutOff.Text.Trim()) Then
            Dim targetPercentage As Double
            targetPercentage = Double.Parse(txtTargetMarketShare.Text)

            If (targetPercentage <= 0.0) OrElse (targetPercentage > 100.0) Then
                targetPercentage = 10.0
                lstOutput.Items.Add("Target percentage out of range. Reset to 10%")
            End If
            laSolver.TargetMarketSharePercentage = targetPercentage
            txtTargetMarketShare.Text = laSolver.TargetMarketSharePercentage.ToString()
        Else
            laSolver.TargetMarketSharePercentage = 10.0
        End If

        ' Set default capacity
        If txtDefaultCapacity.Text.Length > 0 AndAlso IsNumeric(txtDefaultCapacity.Text.Trim()) Then
            Dim defaultCapacity As Double
            defaultCapacity = Double.Parse(txtDefaultCapacity.Text)

            If (defaultCapacity <= 0.0) Then
                defaultCapacity = 1.0
                lstOutput.Items.Add("Default capacity must be greater than zero.")
            End If

            laSolver.DefaultCapacity = defaultCapacity
            txtDefaultCapacity.Text = laSolver.DefaultCapacity.ToString()
        Else
            laSolver.DefaultCapacity = 1.0
        End If

        ' Set any other solver settings
        laSolver.OutputLines = esriNAOutputLineType.esriNAOutputLineStraight
        laSolver.TravelDirection = esriNATravelDirection.esriNATravelDirectionFromFacility

        ' Set the impedance attribute
        Dim naSolverSettings As INASolverSettings = TryCast(naSolver, INASolverSettings)

        naSolverSettings.ImpedanceAttributeName = cboCostAttribute.Text

        naSolverSettings.IgnoreInvalidLocations = True

        ' Do not forget to update the context after you set your impedance
        naSolver.UpdateContext(m_NAContext, GetDENetworkDataset(m_NAContext.NetworkDataset), New GPMessagesClass())
    End Sub

    '*********************************************************************************
    'Get Located Facilities information from the Facilities Class and summarize some statistics
    '*********************************************************************************
    Public Sub GetLAFacilitiesOutput(ByVal strNAClass As String)
        Dim table As ITable = TryCast(m_NAContext.NAClasses.ItemByName(strNAClass), ITable)
        If table Is Nothing Then
            lstOutput.Items.Add("Impossible to get the " & strNAClass & " table")
        End If

        If table.RowCount(Nothing) > 0 Then
            Dim cursor As ICursor
            Dim row As IRow
            Dim facilityName As String
            Dim demandWeight, total_impedance As Double
            Dim demandCount As Long
            Dim facilityCount As Long = 0
            Dim sumDemand As Long = 0
            Dim sumWeightedDistance As Double = 0.0

            cursor = table.Search(Nothing, False)
            row = cursor.NextRow()
            Do While Not row Is Nothing
                demandCount = Long.Parse(row.Value(table.FindField("DemandCount")).ToString())
                If demandCount > 0 Then
                    facilityCount = facilityCount + 1
                    facilityName = row.Value(table.FindField("Name")).ToString()
                    demandWeight = Double.Parse(row.Value(table.FindField("DemandWeight")).ToString())
                    total_impedance = Double.Parse(row.Value(table.FindField("TotalWeighted_" & cboCostAttribute.Text)).ToString())
                    sumWeightedDistance = sumWeightedDistance + total_impedance
                    sumDemand = sumDemand + demandCount
                End If
                row = cursor.NextRow()
            Loop
            lstOutput.Items.Add("Number of facilities Located " & facilityCount.ToString())
            lstOutput.Items.Add("Number of demand points Allocated " & sumDemand.ToString())
            lstOutput.Items.Add("Sum of weighted " & cboCostAttribute.Text & " " & sumWeightedDistance.ToString())
            lstOutput.Items.Add("")
        End If
        lstOutput.Refresh()
    End Sub

    '*********************************************************************************
    ' Get the Impedance Cost form the LALines Class Output and list out the allocation
    '*********************************************************************************
    Public Sub GetLALinesOutput(ByVal strNAClass As String)
        Dim table As ITable = TryCast(m_NAContext.NAClasses.ItemByName(strNAClass), ITable)
        If table Is Nothing Then
            lstOutput.Items.Add("Impossible to get the " & strNAClass & " table")
        End If
        lstOutput.Items.Add("Allocation Table:")
        If table.RowCount(Nothing) > 0 Then
            lstOutput.Items.Add("DemandID,FacilityID,Weight,TotalWeighted_" & cboCostAttribute.Text)
            Dim total_impedance As Double
            Dim demandID As Long
            Dim facilityID As Long
            Dim facilityWeight As Double
            Dim cursor As ICursor
            Dim row As IRow

            cursor = table.Search(Nothing, False)
            row = cursor.NextRow()
            Do While Not row Is Nothing
                facilityID = Long.Parse(row.Value(table.FindField("FacilityID")).ToString())
                demandID = Long.Parse(row.Value(table.FindField("DemandID")).ToString())
                facilityWeight = Double.Parse(row.Value(table.FindField("Weight")).ToString())
                total_impedance = Double.Parse(row.Value(table.FindField("TotalWeighted_" & cboCostAttribute.Text)).ToString())
                lstOutput.Items.Add(demandID.ToString() & "," & Constants.vbTab + facilityID.ToString() & "," & Constants.vbTab + facilityWeight.ToString() & "," & Constants.vbTab + total_impedance.ToString("F2"))
                row = cursor.NextRow()
            Loop
        End If
        lstOutput.Refresh()
    End Sub

    '*********************************************************************************
    ' Geodatabase functions
    ' ********************************************************************************
    Public Function OpenWorkspace(ByVal strGDBName As String) As IWorkspace
        ' As Workspace Factories are Singleton objects, they must be instantiated with the Activator
        Dim workspaceFactory As IWorkspaceFactory = TryCast(Activator.CreateInstance(Type.GetTypeFromProgID("esriDataSourcesGDB.FileGDBWorkspaceFactory")), IWorkspaceFactory)
        Return workspaceFactory.OpenFromFile(strGDBName, 0)
    End Function

    '*********************************************************************************
    ' Open the network dataset
    '*********************************************************************************
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

    Public Function GetDENetworkDataset(ByVal networkDataset As INetworkDataset) As IDENetworkDataset
        ' Cast from the Network Dataset to the DatasetComponent
        Dim dsComponent As IDatasetComponent = TryCast(networkDataset, IDatasetComponent)

        ' Get the Data Element
        Return TryCast(dsComponent.DataElement, IDENetworkDataset)
    End Function

    '*********************************************************************************
    ' Read the solver settings from the user and solve the Location-Allocation problem
    '*********************************************************************************
    Private Sub cmdSolve_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdSolve.Click
        Dim gpMessages As IGPMessages = New GPMessagesClass()
        Try
            lstOutput.Items.Clear()
            lstOutput.Items.Add("Solving...")

            SetSolverSettings()

            ' Solve
            If (Not m_NAContext.Solver.Solve(m_NAContext, gpMessages, Nothing)) Then
                lstOutput.Items.Add("Partial Result")
            End If

            GetLAFacilitiesOutput("Facilities")
            GetLALinesOutput("LALines")

            'Zoom to the extent of the route
            Dim geoDataset As IGeoDataset = TryCast(m_NAContext.NAClasses.ItemByName("LALines"), IGeoDataset)
            Dim envelope As IEnvelope = geoDataset.Extent
            If (Not envelope.IsEmpty) Then
                envelope.Expand(1.1, 1.1, True)
                axMapControl.Extent = envelope
            End If
            axMapControl.Refresh()

        Catch ee As Exception
            lstOutput.Items.Add("Failure: " + ee.Message)
        End Try

        lstOutput.Items.Add(GetGPMessagesAsString(gpMessages))
        cmdSolve.Text = "Solve"
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

    '*********************************************************************************
    ' Manage the cutoff, either <None> or some intelligent default
    '*********************************************************************************
    Private Sub cboProblemType_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cboProblemType.SelectedIndexChanged
        ' All problem types except Minimize Impedance need an impedance cutoff.
        ' So manage an intelligent default other than <None> for them.
        ' If cutoff is set and problem type switches back to Minimize Impedance, reset cutoff to <None>
        ' Note: 3.0 is ok for Minutes but if impedance is something else like Meters, 3.0 will be too small
        ' and cause some solver errors like "Value does not fall within the expected range."
        If cboProblemType.Text.Equals("Minimize Impedance") Then
            If (Not m_ProblemType.Equals("Minimize Impedance")) Then
                If (Not txtCutOff.Text.Equals("<None>")) Then
                    txtCutOff.Text = "<None>"
                End If
            End If
        Else
            If txtCutOff.Text.Equals("<None>") Then
                txtCutOff.Text = "3.0"
            End If
        End If

        m_ProblemType = cboProblemType.Text
    End Sub

    Private Function IsNumeric(ByVal str As String) As Boolean
        Try
            Double.Parse(str.Trim())
        Catch e1 As Exception
            Return False
        End Try
        Return True
    End Function

End Class

