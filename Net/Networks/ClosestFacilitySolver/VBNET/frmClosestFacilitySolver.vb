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
'       ArcGIS Network Analyst extension - Closest Facility Demonstration
'
'   This simple code shows how to :
'    1) Open an shapefile workspace and open a Network DataSet
'    2) Create a NAContext and its NASolver
'    3) Load Incidents/Facilites from Feature Classes and create Network Locations
'    4) Set the Solver parameters
'    5) Solve a Closest Facility problem
'    6) Read the CFRoutes output to display the total facilities
'       and the list of the routes found
'************************************************************************************

Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.NetworkAnalyst

Public Class frmClosestFacilitySolver
    Private m_NAContext As INAContext

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
            Dim workspace As IWorkspace = OpenWorkspace(System.IO.Path.Combine (Environment.SpecialFolder.MyDocuments, "ArcGIS\data\SanFrancisco\SanFrancisco.gdb"))
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
        Next

        txtTargetFacility.Text = "1"
        txtCutOff.Text = "<None>"

        ' Load incidents from FC
        Dim inputFClass As IFeatureClass = featureWorkspace.OpenFeatureClass("Stores")
        LoadNANetworkLocations("Incidents", inputFClass, 100)

        ' Load facilities from FC
        inputFClass = featureWorkspace.OpenFeatureClass("FireStations")
        LoadNANetworkLocations("Facilities", inputFClass, 100)

        ' Create Layer for Network Dataset and add to ArcMap
        Dim networkLayer As INetworkLayer = New NetworkLayerClass
        networkLayer.NetworkDataset = networkDataset
        Dim layer As ILayer = TryCast(networkLayer, ILayer)
        layer.Name = "Network Dataset"
        axMapControl.AddLayer(layer, 0)

        ' Create a Network Analysis Layer and add to ArcMap
        Dim naLayer As INALayer = m_NAContext.Solver.CreateLayer(m_NAContext)
        layer = naLayer
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
        ' Get the Data Element
        Dim deNDS As IDENetworkDataset = GetDENetworkDataset(networkDataset)

        Dim naSolver As INASolver = New NAClosestFacilitySolver
        Dim contextEdit As INAContextEdit = naSolver.CreateContext(deNDS, naSolver.Name)
        contextEdit.Bind(networkDataset, New GPMessagesClass)
        Return TryCast(contextEdit, INAContext)
    End Function

    '*********************************************************************************
    ' Load network locations
    '*********************************************************************************
    Public Sub LoadNANetworkLocations(ByVal strNAClassName As String, ByVal inputFC As IFeatureClass, ByVal maxSnapTolerance As Double)
        Dim classes As INamedSet = m_NAContext.NAClasses
        Dim naClass As INAClass = TryCast(classes.ItemByName(strNAClassName), INAClass)

        ' delete existing Locations except if that a barriers
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

        ' Create field map to automatically map fields from input class to naclass
        Dim fieldMap As INAClassFieldMap = New NAClassFieldMap
        fieldMap.CreateMapping(naClass.ClassDefinition, inputFC.Fields)
        classLoader.FieldMap = fieldMap

        ' Load Network Locations
        Dim rowsIn As Integer = 0
        Dim rowsLocated As Integer = 0
        Dim featureCursor As IFeatureCursor = inputFC.Search(Nothing, True)
        classLoader.Load(featureCursor, Nothing, rowsIn, rowsLocated)

        ' Message all of the network analysis agents that the analysis context has changed
        CType(m_NAContext, INAContextEdit).ContextChanged()
    End Sub

    '*********************************************************************************
    ' Set Solver Settings
    '*********************************************************************************
    Public Sub SetSolverSettings()
        'Set Route specific Settings
        Dim naSolver As INASolver = m_NAContext.Solver

        Dim cfSolver As INAClosestFacilitySolver = TryCast(naSolver, INAClosestFacilitySolver)

        ' Set number of facilities to find
        If txtTargetFacility.Text.Length > 0 And IsNumeric(txtTargetFacility.Text) Then
            cfSolver.DefaultTargetFacilityCount = Integer.Parse(txtTargetFacility.Text)
        Else
            cfSolver.DefaultTargetFacilityCount = 1
        End If

        ' Set impedance cutoff
        If txtCutOff.Text.Length > 0 And IsNumeric(txtCutOff.Text.Trim()) Then
            cfSolver.DefaultCutoff = txtCutOff.Text
        Else
            cfSolver.DefaultCutoff = Nothing
        End If

        cfSolver.OutputLines = esriNAOutputLineType.esriNAOutputLineTrueShapeWithMeasure
        cfSolver.TravelDirection = esriNATravelDirection.esriNATravelDirectionToFacility

        'Set generic Solver settings
        ' set the impedance attribute
        Dim naSolverSettings As INASolverSettings = naSolver
        naSolverSettings.ImpedanceAttributeName = cboCostAttribute.Text

        ' Set the OneWay Restriction if necessary
        Dim restrictions As IStringArray = naSolverSettings.RestrictionAttributeNames
        restrictions.RemoveAll()
        If chkUseRestriction.Checked Then
            restrictions.Add("oneway")
        End If

        naSolverSettings.RestrictionAttributeNames = restrictions

        'Restrict UTurns
        naSolverSettings.RestrictUTurns = esriNetworkForwardStarBacktrack.esriNFSBNoBacktrack
        naSolverSettings.IgnoreInvalidLocations = True

        ' Set the Hierarchy attribute
        naSolverSettings.UseHierarchy = chkUseHierarchy.Checked
        If naSolverSettings.UseHierarchy Then
            naSolverSettings.HierarchyAttributeName = "HierarchyMultiNet"
        End If

        ' Do not forget to update the context after you set your impedance
        naSolver.UpdateContext(m_NAContext, GetDENetworkDataset(m_NAContext.NetworkDataset), New GPMessagesClass)
    End Sub



    '*********************************************************************************
    ' Geodatabase functions
    '*********************************************************************************
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

    Public Function GetDENetworkDataset(ByVal pNetDataset As INetworkDataset) As IDENetworkDataset
        'Cast from the Network Dataset to the DatasetComponent
        Dim dsComponent As IDatasetComponent = pNetDataset

        'Get the Data Element
        Return TryCast(dsComponent.DataElement, IDENetworkDataset)
    End Function

    Private Sub cmdSolve_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSolve.Click
        Dim gpMessages As IGPMessages = New GPMessagesClass
        Try
            lstOutput.Items.Clear()
            lstOutput.Items.Add("Solving...")

            SetSolverSettings()

            ' Solve
            If Not m_NAContext.Solver.Solve(m_NAContext, gpMessages, Nothing) Then
                lstOutput.Items.Add("Partial Result")
            End If

            DisplayOutput()

        Catch ee As Exception
            lstOutput.Items.Add("Failure: " + ee.Message)
        End Try

        lstOutput.Items.Add(GetGPMessagesAsString(gpMessages))
        cmdSolve.Text = "Find Closest Facilities"
    End Sub

    Private Sub UpdateMapDisplayAfterSolve()
        ' Zoom to the extent of the service areas
        Dim geoDataset As IGeoDataset = TryCast(m_NAContext.NAClasses.ItemByName("CFRoutes"), IGeoDataset)
        Dim envelope As IEnvelope = geoDataset.Extent
        If (Not envelope.IsEmpty) Then
            envelope.Expand(1.1, 1.1, True)
            axMapControl.Extent = envelope

            ' Call this to update the renderer for the service area polygons
            ' based on the new breaks.
            m_naContext.Solver.UpdateLayer(TryCast(axMapControl.get_Layer(0), INALayer))
        End If
        axMapControl.Refresh()
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

    ' Get the Impedance Cost form the CFRoute Class Output
    Public Sub DisplayOutput()
        Dim strNAClass As String = "CFRoutes"

        Dim table As ITable = m_NAContext.NAClasses.ItemByName(strNAClass)
        If table Is Nothing Then
            lstOutput.Items.Add("Impossible to get the " + strNAClass + " table")
        End If

        lstOutput.Items.Add("Number facilities found " + table.RowCount(Nothing).ToString())
        lstOutput.Items.Add("")
        If table.RowCount(Nothing) > 0 Then
            lstOutput.Items.Add("IncidentID, FacilityID, FacilityRank, Total_" + cboCostAttribute.Text)
            Dim total_impedance As Double
            Dim incidentID As Long
            Dim facilityID As Long
            Dim facilityRank As Long
            Dim cursor As ICursor
            Dim row As IRow

            cursor = table.Search(Nothing, False)
            row = cursor.NextRow()
            While Not row Is Nothing
                incidentID = Long.Parse(row.Value(table.FindField("IncidentID")).ToString())
                facilityID = Long.Parse(row.Value(table.FindField("FacilityID")).ToString())
                facilityRank = Long.Parse(row.Value(table.FindField("FacilityRank")).ToString())
                total_impedance = Double.Parse(row.Value(table.FindField("Total_" + cboCostAttribute.Text)).ToString())
                lstOutput.Items.Add(incidentID.ToString() + "," + vbTab + facilityID.ToString() + "," + vbTab + facilityRank.ToString() + "," + vbTab + total_impedance.ToString("F2"))

                row = cursor.NextRow()
            End While
        End If
        lstOutput.Refresh()
    End Sub

    Private Function IsNumeric(ByVal str As String) As Boolean
        Try
            Double.Parse(str.Trim())
        Catch
            Return False
        End Try
        Return True
    End Function
End Class

