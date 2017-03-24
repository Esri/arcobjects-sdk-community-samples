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
Imports System.Runtime.InteropServices
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.EditorExt
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.NetworkAnalysis

<ComClass(CustomUpstreamTraceTaskVBNet.ClassId, CustomUpstreamTraceTaskVBNet.InterfaceId, CustomUpstreamTraceTaskVBNet.EventsId), _
 ProgId("CustomUpstreamTraceTask.CustomUpstreamTraceTaskVBNet")> _
Public NotInheritable Class CustomUpstreamTraceTaskVBNet
	Implements ESRI.ArcGIS.EditorExt.ITraceTask
	Implements ESRI.ArcGIS.EditorExt.ITraceTaskResults

#Region "COM Registration Function(s)"
	<ComRegisterFunction(), ComVisibleAttribute(False)> _
	Public Shared Sub RegisterFunction(ByVal registerType As Type)
		' Required for ArcGIS Component Category Registrar support
		ArcGISCategoryRegistration(registerType)

		'Add any COM registration code after the ArcGISCategoryRegistration() call

	End Sub

	<ComUnregisterFunction(), ComVisibleAttribute(False)> _
	Public Shared Sub UnregisterFunction(ByVal registerType As Type)
		' Required for ArcGIS Component Category Registrar support
		ArcGISCategoryUnregistration(registerType)

		'Add any COM unregistration code after the ArcGISCategoryUnregistration() call

	End Sub

#Region "ArcGIS Component Category Registrar generated code"
	''' <summary>
	''' Required method for ArcGIS Component Category registration -
	''' Do not modify the contents of this method with the code editor.
	''' </summary>
	Private Shared Sub ArcGISCategoryRegistration(ByVal registerType As Type)
		Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
		UtilityNetworkTasks.Register(regKey)

	End Sub
	''' <summary>
	''' Required method for ArcGIS Component Category unregistration -
	''' Do not modify the contents of this method with the code editor.
	''' </summary>
	Private Shared Sub ArcGISCategoryUnregistration(ByVal registerType As Type)
		Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
		UtilityNetworkTasks.Unregister(regKey)

	End Sub

#End Region
#End Region

#Region "COM GUIDs"
	' These  GUIDs provide the COM identity for this class 
	' and its COM interfaces. If you change them, existing 
	' clients will no longer be able to access the class.
	Public Const ClassId As String = "70826b66-d496-44ea-986c-490642f69946"
	Public Const InterfaceId As String = "afa932c0-6306-490b-808f-ffad55e3b397"
	Public Const EventsId As String = "27c9b97a-59c1-42d2-8a51-9ebe2f0f0c8d"
#End Region

	' A creatable COM class must have a Public Sub New() 
	' with no parameters, otherwise, the class will not be 
	' registered in the COM registry and cannot be created 
	' via CreateObject.
	Public Sub New()
		MyBase.New()
	End Sub

	Private m_utilNetExt As IUtilityNetworkAnalysisExt
	Private m_resultJunctions As IEnumNetEID
	Private m_resultEdges As IEnumNetEID

	Public ReadOnly Property EnableSolve() As Boolean Implements ESRI.ArcGIS.EditorExt.ITraceTask.EnableSolve
		Get
			' if there are no networks loaded, then the Solve button is disabled
			Dim nax As INetworkAnalysisExt = CType(m_utilNetExt, INetworkAnalysisExt)
			If nax.NetworkCount = 0 Then
				Return False
			End If

			' if there is at least one flag on the network,
			' then enable the Solve button
			Dim naxFlags As INetworkAnalysisExtFlags = CType(m_utilNetExt, INetworkAnalysisExtFlags)
			If naxFlags.EdgeFlagCount = 0 And naxFlags.JunctionFlagCount = 0 Then
				Return False
			Else
				Return True
			End If
		End Get
	End Property

	Public ReadOnly Property Name() As String Implements ESRI.ArcGIS.EditorExt.ITraceTask.Name
		Get
			Return "Custom Upstream Trace"
		End Get
	End Property

	Public Sub OnCreate(ByVal utilityNetworkAnalysis As ESRI.ArcGIS.EditorExt.IUtilityNetworkAnalysisExt) Implements ESRI.ArcGIS.EditorExt.ITraceTask.OnCreate
		m_utilNetExt = utilityNetworkAnalysis
	End Sub

	Public Sub OnTraceExecution() Implements ESRI.ArcGIS.EditorExt.ITraceTask.OnTraceExecution
		' prepare the network solver
		Dim tfs As ITraceFlowSolverGEN = UTIL_coreTraceSetup()
		If tfs Is Nothing Then
			Return
		End If

		' perform the trace task
		Dim resultJuncs As IEnumNetEID = New EnumNetEIDArray()
		Dim resultEdges As IEnumNetEID = New EnumNetEIDArray()
		Dim traceTasks As ITraceTasks = CType(m_utilNetExt, ITraceTasks)
		Dim flowElements As esriFlowElements = traceTasks.TraceFlowElements
		If traceTasks.TraceEnds Then
			' find the features stopping the trace
			tfs.FindFlowEndElements(esriFlowMethod.esriFMUpstream, flowElements, resultJuncs, resultEdges)
		Else
			' return the traced features
			tfs.FindFlowElements(esriFlowMethod.esriFMUpstream, flowElements, resultJuncs, resultEdges)
		End If

		' copy the results to the class level
		Dim nax As INetworkAnalysisExt = CType(m_utilNetExt, INetworkAnalysisExt)
		If resultJuncs Is Nothing Then
			' junctions were not returned -- create an empty enumeration
			Dim eidBuilder As IEnumNetEIDBuilder = New EnumNetEIDArray()
			eidBuilder.Network = nax.CurrentNetwork.Network
			eidBuilder.ElementType = esriElementType.esriETJunction
			m_resultJunctions = CType(eidBuilder, IEnumNetEID)
		Else
			m_resultJunctions = resultJuncs
		End If
		If resultEdges Is Nothing Then
			' edges were not returned -- create an empty enumeration
			Dim eidBuilder As IEnumNetEIDBuilder = New EnumNetEIDArray()
			eidBuilder.Network = nax.CurrentNetwork.Network
			eidBuilder.ElementType = esriElementType.esriETEdge
			m_resultEdges = CType(eidBuilder, IEnumNetEID)
		Else
			m_resultEdges = resultEdges
		End If

		' update the extension with the results
		Dim naxResults As INetworkAnalysisExtResults = CType(m_utilNetExt, INetworkAnalysisExtResults)
		naxResults.ClearResults()	' first remove the old results
		If naxResults.ResultsAsSelection Then
			naxResults.CreateSelection(resultJuncs, resultEdges)
		Else
			naxResults.SetResults(resultJuncs, resultEdges)
		End If
	End Sub

	Public ReadOnly Property ResultEdges() As ESRI.ArcGIS.Geodatabase.IEnumNetEID Implements ESRI.ArcGIS.EditorExt.ITraceTaskResults.ResultEdges
		Get
			Return m_resultEdges
		End Get
	End Property

	Public ReadOnly Property ResultJunctions() As ESRI.ArcGIS.Geodatabase.IEnumNetEID Implements ESRI.ArcGIS.EditorExt.ITraceTaskResults.ResultJunctions
		Get
			Return m_resultJunctions
		End Get
	End Property

	Public Function UTIL_coreTraceSetup() As ITraceFlowSolverGEN
		' get the current network's logical network
		Dim nax As INetworkAnalysisExt = CType(m_utilNetExt, INetworkAnalysisExt)
		Dim net As INetwork = nax.CurrentNetwork.Network

		' create a new TraceFlowSolver object and
		' set the source network for the solve
		Dim tfs As ITraceFlowSolverGEN = CType(New TraceFlowSolver(), ITraceFlowSolverGEN)
		Dim netSolver As INetSolver = CType(tfs, INetSolver)
		netSolver.SourceNetwork = net

		' get the barriers for the network, using the element barriers and 
		' selection barriers that have been added using the user interface
		Dim naxBarriers As INetworkAnalysisExtBarriers = CType(m_utilNetExt, INetworkAnalysisExtBarriers)
		Dim juncElemBarriers As INetElementBarriers = CType(New NetElementBarriers(), INetElementBarriers)
		Dim edgeElemBarriers As INetElementBarriers = CType(New NetElementBarriers(), INetElementBarriers)
		naxBarriers.CreateElementBarriers(juncElemBarriers, edgeElemBarriers)
		Dim selSetBarriers As ISelectionSetBarriers = New SelectionSetBarriers()
		naxBarriers.CreateSelectionBarriers(selSetBarriers)
		netSolver.ElementBarriers(esriElementType.esriETJunction) = juncElemBarriers
		netSolver.ElementBarriers(esriElementType.esriETEdge) = edgeElemBarriers
    netSolver.ElementBarriers(esriElementType.esriETEdge) = Nothing
		netSolver.SelectionSetBarriers = selSetBarriers

		' set up the disabled layers for the network solver
		' for each feature layer belonging to this network, determine if it is
		' enabled or disabled; if it's disabled, then notify the network solver
		For i = 0 To nax.FeatureLayerCount - 1
			Dim featureLayer As IFeatureLayer = nax.FeatureLayer(i)
			If naxBarriers.GetDisabledLayer(featureLayer) Then
				netSolver.DisableElementClass(featureLayer.FeatureClass.FeatureClassID)
			End If
		Next i

		Dim naxWeightFilter As INetworkAnalysisExtWeightFilter = CType(m_utilNetExt, INetworkAnalysisExtWeightFilter)
		Dim netSolverWeights As INetSolverWeightsGEN = CType(netSolver, INetSolverWeightsGEN)
		Dim netSchema As INetSchema = CType(net, INetSchema)

		' create the junction weight filter
		Dim juncFilterRangeCount As Integer = naxWeightFilter.FilterRangeCount(esriElementType.esriETJunction)
		If (juncFilterRangeCount > 0) Then
			Dim netWeight As INetWeight = netSchema.WeightByName(naxWeightFilter.JunctionWeightFilterName)
			netSolverWeights.JunctionFilterWeight = netWeight

			Dim juncWeightFilterType As esriWeightFilterType
			Dim juncApplyNotOperator As Boolean
			naxWeightFilter.GetFilterType(esriElementType.esriETJunction, juncWeightFilterType, juncApplyNotOperator)
			netSolverWeights.SetFilterType(esriElementType.esriETJunction, juncWeightFilterType, juncApplyNotOperator)

			Dim juncFromValues(juncFilterRangeCount - 1) As Object
			Dim juncToValues(juncFilterRangeCount - 1) As Object
			For i = 0 To juncFilterRangeCount - 1
				naxWeightFilter.GetFilterRange(esriElementType.esriETJunction, i, juncFromValues(i), juncToValues(i))
			Next i
			netSolverWeights.SetFilterRanges(esriElementType.esriETJunction, juncFromValues, juncToValues)
		End If

		' create the edge weight filters
		Dim edgeFilterRangeCount As Integer = naxWeightFilter.FilterRangeCount(esriElementType.esriETEdge)
		If (edgeFilterRangeCount > 0) Then
			Dim fromToNetWeight As INetWeight = netSchema.WeightByName(naxWeightFilter.FromToEdgeWeightFilterName)
			netSolverWeights.FromToEdgeFilterWeight = fromToNetWeight
			Dim toFromNetWeight As INetWeight = netSchema.WeightByName(naxWeightFilter.ToFromEdgeWeightFilterName)
			netSolverWeights.ToFromEdgeFilterWeight = toFromNetWeight

			Dim edgeWeightFilterType As esriWeightFilterType
			Dim edgeApplyNotOperator As Boolean
			naxWeightFilter.GetFilterType(esriElementType.esriETEdge, edgeWeightFilterType, edgeApplyNotOperator)
			netSolverWeights.SetFilterType(esriElementType.esriETEdge, edgeWeightFilterType, edgeApplyNotOperator)

			Dim edgeFromValues(0 To edgeFilterRangeCount - 1) As Object
			Dim edgeToValues(0 To edgeFilterRangeCount - 1) As Object
			For i = 0 To edgeFilterRangeCount - 1
				naxWeightFilter.GetFilterRange(esriElementType.esriETEdge, i, edgeFromValues(i), edgeToValues(i))
			Next i
			netSolverWeights.SetFilterRanges(esriElementType.esriETEdge, edgeFromValues, edgeToValues)
		End If

		Dim naxFlags As INetworkAnalysisExtFlags = CType(m_utilNetExt, INetworkAnalysisExtFlags)

		' assign the edge flags to the network solver
		Dim edgeFlagCount As Integer = naxFlags.EdgeFlagCount
		If (edgeFlagCount > 0) Then
			Dim edgeFlags(0 To edgeFlagCount - 1) As IEdgeFlag
			For i = 0 To edgeFlagCount - 1
				Dim edgeFlagDisplay As IEdgeFlagDisplay = naxFlags.EdgeFlag(i)
				Dim flagDisplay As IFlagDisplay = CType(edgeFlagDisplay, IFlagDisplay)
				Dim edgeFlag As IEdgeFlag = New EdgeFlag()
				edgeFlag.Position = Convert.ToSingle(edgeFlagDisplay.Percentage)
				Dim netFlag As INetFlag = CType(edgeFlag, INetFlag)
				netFlag.UserClassID = flagDisplay.FeatureClassID
				netFlag.UserID = flagDisplay.FID
				netFlag.UserSubID = flagDisplay.SubID
				edgeFlags(i) = edgeFlag
			Next i
			tfs.PutEdgeOrigins(edgeFlags)
		End If

		' assign the junction flags to the network solver
		Dim juncFlagCount As Integer = naxFlags.JunctionFlagCount
		If (juncFlagCount > 0) Then
			Dim juncFlags(0 To juncFlagCount - 1) As IJunctionFlag
			For i = 0 To juncFlagCount - 1
				Dim juncFlagDisplay As IJunctionFlagDisplay = naxFlags.JunctionFlag(i)
				Dim flagDisplay As IFlagDisplay = CType(juncFlagDisplay, IFlagDisplay)
				Dim juncFlag As IJunctionFlag = New JunctionFlag()
				Dim netFlag As INetFlag = CType(juncFlag, INetFlag)
				netFlag.UserClassID = flagDisplay.FeatureClassID
				netFlag.UserID = flagDisplay.FID
				netFlag.UserSubID = flagDisplay.SubID
				juncFlags(i) = juncFlag
			Next i
			tfs.PutJunctionOrigins(juncFlags)
		End If

		' set the option for tracing on indeterminate flow
		Dim traceTasks As ITraceTasks = CType(m_utilNetExt, ITraceTasks)
		tfs.TraceIndeterminateFlow = traceTasks.TraceIndeterminateFlow

		Return tfs
	End Function
End Class