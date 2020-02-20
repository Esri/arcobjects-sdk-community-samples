'Copyright 2019 Esri

'Licensed under the Apache License, Version 2.0 (the "License");
'you may not use this file except in compliance with the License.
'You may obtain a copy of the License at

'    http://www.apache.org/licenses/LICENSE-2.0

'Unless required by applicable law or agreed to in writing, software
'distributed under the License is distributed on an "AS IS" BASIS,
'WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.

'See the License for the specific language governing permissions and
'limitations under the License.
Imports Microsoft.VisualBasic
Imports System
Imports System.Windows.Forms
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.NetworkAnalyst
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Geometry

Namespace ServiceAreaSolver
	Partial Public Class frmServiceAreaSolver
		Inherits Form
		Private m_naContext As INAContext

#Region "Main Form Constructor and Setup"

		Public Sub New()
			InitializeComponent()

			txtCutOff.Text = "5"
			lbOutput.Items.Clear()
			cbCostAttribute.Items.Clear()
			ckbUseRestriction.Checked = False
			axMapControl.ClearLayers()

			txtWorkspacePath.Text = System.IO.Path.Combine (Environment.SpecialFolder.MyDocuments, "ArcGIS\data\SanFrancisco\SanFrancisco.gdb")
			txtNetworkDataset.Text = "Streets_ND"
			txtFeatureDataset.Text = "Transportation"
			txtInputFacilities.Text = "Hospitals"
			gbServiceAreaSolver.Enabled = False
		End Sub

#End Region

#Region "Button Clicks"

		Private Sub btnSolve_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSolve.Click
			Me.Cursor = Cursors.WaitCursor
			lbOutput.Items.Clear()

            Dim gpMessages As IGPMessages = New GPMessagesClass()
            Try
                ConfigureSolverSettings()

                If (Not m_naContext.Solver.Solve(m_naContext, gpMessages, Nothing)) Then
                    lbOutput.Items.Add("Partial Result")
                End If

                DisplayOutput()

            Catch ex As Exception
                lbOutput.Items.Add("Solve Failed: " & ex.Message)
            End Try

            lbOutput.Items.Add(GetGPMessagesAsString(GPMessages))
            UpdateMapDisplayAfterSolve()

            Me.Cursor = Cursors.Default
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

        Private Sub btnLoadMap_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnLoadMap.Click
            Me.Cursor = Cursors.WaitCursor

            gbServiceAreaSolver.Enabled = False
            lbOutput.Items.Clear()

            ' Open geodatabase and network dataset
            Dim featureWorkspace As IFeatureWorkspace = Nothing
            Dim networkDataset As INetworkDataset = Nothing

            Try
                Dim workspace As IWorkspace = OpenWorkspace(System.IO.Path.Combine (Environment.SpecialFolder.MyDocuments, "ArcGIS\data\SanFrancisco\SanFrancisco.gdb"))
                networkDataset = OpenNetworkDataset(workspace, "Transportation", "Streets_ND")
                featureWorkspace = TryCast(workspace, IFeatureWorkspace)
            Catch ex As Exception
                Windows.Forms.MessageBox.Show("Unable to open dataset. Error Message: " + ex.Message)
                Me.Cursor = Cursors.Default
                Return
            End Try

            CreateContextAndSolver(networkDataset)
            If m_naContext Is Nothing Then
                Me.Cursor = Cursors.Default
                Return
            End If

            LoadCostAttributes(networkDataset)
            If (Not LoadLocations(featureWorkspace)) Then
                Me.Cursor = Cursors.Default
                Return
            End If

            AddNetworkDatasetLayerToMap(networkDataset)
            AddNetworkAnalysisLayerToMap()

            ' work around a transparency issue
            Dim geoDataset As IGeoDataset = TryCast(networkDataset, IGeoDataset)
            axMapControl.Extent = axMapControl.FullExtent
            axMapControl.Extent = geoDataset.Extent

            If m_naContext IsNot Nothing Then
                gbServiceAreaSolver.Enabled = True
            End If

            Me.Cursor = Cursors.Default
        End Sub

#End Region

#Region "Set up Context and Solver"

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

        Private Sub CreateContextAndSolver(ByVal networkDataset As INetworkDataset)
            If networkDataset Is Nothing Then
                Return
            End If

            Dim datasetComponent As IDatasetComponent = TryCast(networkDataset, IDatasetComponent)
            Dim deNetworkDataset As IDENetworkDataset = TryCast(datasetComponent.DataElement, IDENetworkDataset)

            Dim naSolver As INASolver = New NAServiceAreaSolverClass()
            m_naContext = naSolver.CreateContext(deNetworkDataset, "ServiceArea")
            Dim naContextEdit As INAContextEdit = TryCast(m_naContext, INAContextEdit)
            naContextEdit.Bind(networkDataset, New GPMessagesClass())
        End Sub

#End Region

#Region "Load Form Controls"

        Private Sub LoadCostAttributes(ByVal networkDataset As INetworkDataset)
            cbCostAttribute.Items.Clear()

            Dim attrCount As Integer = networkDataset.AttributeCount
            For attrIndex As Integer = 0 To attrCount - 1
                Dim networkAttribute As INetworkAttribute = networkDataset.Attribute(attrIndex)
                If networkAttribute.UsageType = esriNetworkAttributeUsageType.esriNAUTCost Then
                    cbCostAttribute.Items.Add(networkAttribute.Name)
                End If
            Next attrIndex

            If cbCostAttribute.Items.Count > 0 Then
                cbCostAttribute.SelectedIndex = 0
            End If
        End Sub

        Private Function LoadLocations(ByVal featureWorkspace As IFeatureWorkspace) As Boolean
            Dim inputFeatureClass As IFeatureClass = Nothing
            Try
                inputFeatureClass = featureWorkspace.OpenFeatureClass(txtInputFacilities.Text)
            Catch e1 As Exception
                MessageBox.Show("Specified input feature class does not exist")
                Return False
            End Try

            Dim classes As INamedSet = m_naContext.NAClasses
            Dim naClass As INAClass = TryCast(classes.ItemByName("Facilities"), INAClass)

            ' delete existing locations, except barriers
            naClass.DeleteAllRows()

            ' Create a NAClassLoader and set the snap tolerance (meters unit)
            Dim naClassLoader As INAClassLoader = New NAClassLoaderClass()
            naClassLoader.Locator = m_naContext.Locator
            naClassLoader.Locator.SnapTolerance = 100
            naClassLoader.NAClass = naClass

            ' Create field map to automatically map fields from input class to NAClass
            Dim naClassFieldMap As INAClassFieldMap = New NAClassFieldMapClass()
            naClassFieldMap.CreateMapping(naClass.ClassDefinition, inputFeatureClass.Fields)
            naClassLoader.FieldMap = naClassFieldMap

            ' Avoid loading network locations onto non-traversable portions of elements
            Dim locator As INALocator3 = TryCast(m_naContext.Locator, INALocator3)
            locator.ExcludeRestrictedElements = True
            locator.CacheRestrictedElements(m_naContext)

            ' load network locations
            Dim rowsIn As Integer = 0
            Dim rowsLocated As Integer = 0
            naClassLoader.Load(TryCast(inputFeatureClass.Search(Nothing, True), ICursor), Nothing, rowsIn, rowsLocated)

            If rowsLocated <= 0 Then
                MessageBox.Show("Facilities were not loaded from input feature class")
                Return False
            End If

            ' Message all of the network analysis agents that the analysis context has changed
            Dim naContextEdit As INAContextEdit = TryCast(m_naContext, INAContextEdit)
            naContextEdit.ContextChanged()

            Return True
        End Function

        Private Sub AddNetworkAnalysisLayerToMap()
            Dim layer As ILayer = TryCast(m_naContext.Solver.CreateLayer(m_naContext), ILayer)
            layer.Name = m_naContext.Solver.DisplayName
            axMapControl.AddLayer(layer)
        End Sub

        Private Sub AddNetworkDatasetLayerToMap(ByVal networkDataset As INetworkDataset)
            Dim networkLayer As INetworkLayer = New NetworkLayerClass()
            networkLayer.NetworkDataset = networkDataset
            Dim layer As ILayer = TryCast(networkLayer, ILayer)
            layer.Name = "Network Dataset"
            axMapControl.AddLayer(layer)
        End Sub

#End Region

#Region "Solver Settings"

        Private Sub ConfigureSolverSettings()
            ConfigureSettingsSpecificToServiceAreaSolver()

            ConfigureGenericSolverSettings()

            UpdateContextAfterChangingSettings()
        End Sub

        Private Sub ConfigureSettingsSpecificToServiceAreaSolver()
            Dim naSASolver As INAServiceAreaSolver = TryCast(m_naContext.Solver, INAServiceAreaSolver)

            naSASolver.DefaultBreaks = ParseBreaks(txtCutOff.Text)

            naSASolver.MergeSimilarPolygonRanges = False
            naSASolver.OutputPolygons = esriNAOutputPolygonType.esriNAOutputPolygonSimplified
            naSASolver.OverlapLines = True
            naSASolver.SplitLinesAtBreaks = False
            naSASolver.TravelDirection = esriNATravelDirection.esriNATravelDirectionFromFacility
            naSASolver.OutputLines = esriNAOutputLineType.esriNAOutputLineNone
        End Sub

        Private Sub ConfigureGenericSolverSettings()
            Dim naSolverSettings As INASolverSettings = TryCast(m_naContext.Solver, INASolverSettings)
            naSolverSettings.ImpedanceAttributeName = cbCostAttribute.Text

            ' set the oneway restriction, if necessary
            Dim restrictions As IStringArray = naSolverSettings.RestrictionAttributeNames
            restrictions.RemoveAll()
            If ckbUseRestriction.Checked Then
                restrictions.Add("oneway")
            End If
            naSolverSettings.RestrictionAttributeNames = restrictions
            naSolverSettings.RestrictUTurns = esriNetworkForwardStarBacktrack.esriNFSBNoBacktrack
        End Sub

        Private Sub UpdateContextAfterChangingSettings()
            Dim datasetComponent As IDatasetComponent = TryCast(m_naContext.NetworkDataset, IDatasetComponent)
            Dim deNetworkDataset As IDENetworkDataset = TryCast(datasetComponent.DataElement, IDENetworkDataset)
            m_naContext.Solver.UpdateContext(m_naContext, deNetworkDataset, New GPMessagesClass())
        End Sub

        Private Function ParseBreaks(ByVal p As String) As IDoubleArray
            Dim breaks() As String = p.Split(" "c)
            Dim pBrks As IDoubleArray = New DoubleArrayClass()
            Dim firstIndex As Integer = breaks.GetLowerBound(0)
            Dim lastIndex As Integer = breaks.GetUpperBound(0)
            For splitIndex As Integer = firstIndex To lastIndex
                Try
                    pBrks.Add(Convert.ToDouble(breaks(splitIndex)))
                Catch e1 As FormatException
                    MessageBox.Show("Breaks are not properly formatted.  Use only digits separated by spaces")
                    pBrks.RemoveAll()
                    Return pBrks
                End Try
            Next splitIndex

            Return pBrks
        End Function

#End Region

#Region "Post-Solve"

        Private Sub LoadListboxAfterPartialSolve(ByVal gpMessages As IGPMessages)
            lbOutput.Items.Add("Partial Solve Generated.")
            For msgIndex As Integer = 0 To gpMessages.Messages.Count - 1
                Dim errorText As String = ""
                Select Case gpMessages.GetMessage(msgIndex).Type
                    Case esriGPMessageType.esriGPMessageTypeError
                        errorText = "Error " & gpMessages.GetMessage(msgIndex).ErrorCode.ToString() & " " & gpMessages.GetMessage(msgIndex).Description
                    Case esriGPMessageType.esriGPMessageTypeWarning
                        errorText = "Warning " & gpMessages.GetMessage(msgIndex).ErrorCode.ToString() & " " & gpMessages.GetMessage(msgIndex).Description
                    Case Else
                        errorText = "Information " & gpMessages.GetMessage(msgIndex).Description
                End Select
                lbOutput.Items.Add(errorText)
            Next msgIndex
        End Sub

        Private Sub DisplayOutput()
            Dim table As ITable = TryCast(m_naContext.NAClasses.ItemByName("SAPolygons"), ITable)
            If table.RowCount(Nothing) > 0 Then
                Dim gpMessage As IGPMessage = New GPMessageClass()
                lbOutput.Items.Add("FacilityID, FromBreak, ToBreak")
                Dim cursor As ICursor = table.Search(Nothing, True)
                Dim row As IRow = cursor.NextRow()
                Do While row IsNot Nothing
                    Dim facilityID As Integer = CInt(Fix(row.Value(table.FindField("FacilityID"))))
                    Dim fromBreak As Double = CDbl(row.Value(table.FindField("FromBreak")))
                    Dim toBreak As Double = CDbl(row.Value(table.FindField("ToBreak")))
                    lbOutput.Items.Add(facilityID.ToString() & ", " & fromBreak.ToString("#####0.00") & ", " & toBreak.ToString("#####0.00"))
                    row = cursor.NextRow()
                Loop
            End If
        End Sub

        Private Sub UpdateMapDisplayAfterSolve()
            ' Zoom to the extent of the service areas
            Dim geoDataset As IGeoDataset = TryCast(m_naContext.NAClasses.ItemByName("SAPolygons"), IGeoDataset)
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

#End Region
	End Class
End Namespace