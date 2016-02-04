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
Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Runtime.InteropServices
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Geodatabase

Namespace SubsetNetworkEvaluators
	''' <summary>
	''' The filter subset network evaluator is a custom network evaluator for modeling dynamic restriction attributes
	''' where the subset of elements to restrict can be quickly switched at run-time without having to update the network
	''' dataset schema in ArcCatalog.  In this example the subset of network elements that are restricted is determined
	''' based on the selected network features in ArcMap, but it does not matter how the element subset is determined.
	''' The selected features can be interpreted as the only restricted features or as the only traversable features based
	''' on a parameter flag value, and the subset of elements is also specified as a network attribute parameter value.
	''' </summary>

	<ClassInterface(ClassInterfaceType.None), Guid("e2a9fbbf-8950-48cb-b487-0ee3f43dccca"), ProgId("SubsetNetworkEvaluators.FilterSubsetEvaluator")> _
	Public Class FilterSubsetEvaluator : Implements INetworkEvaluator2, INetworkEvaluatorSetup
#Region "Member Variables"

		Private m_networkDataset As INetworkDataset
		Private m_networkSource As INetworkSource
		Private m_networkAttribute As INetworkAttribute

		' Indicates if filter values should be restricted (otherwise NON filter values are restricted)
		' SPECIAL CASE: if NO elements are in the filter, then no elements for THIS SOURCE will be restricted.
		' number of EIDs to filter for this source 
		' the EIDs to override values for, by scaling, for this source
		Private m_restrictFilterElements As Boolean = True

		Private m_countSourceEIDs As Integer = 0
		Private m_sourceEIDHashTable As Dictionary(Of Integer, Integer)

#End Region

#Region "INetworkEvaluator Members"

		Public ReadOnly Property CacheAttribute() As Boolean Implements INetworkEvaluator.CacheAttribute, INetworkEvaluator2.CacheAttribute
			' CacheAttribute returns whether or not we want the network dataset to cache our evaluated attribute values during the network dataset build
			' Since this is a dynamic evaluator, we will return false, so that our attribute values are dynamically queried at runtime
			Get
				Return False
			End Get
		End Property

		Public ReadOnly Property DisplayName() As String Implements INetworkEvaluator.DisplayName, INetworkEvaluator2.DisplayName
			Get
				Return "FilterSubset"
			End Get
		End Property

		Public ReadOnly Property Name() As String Implements INetworkEvaluator.Name, INetworkEvaluator2.Name
			Get
				Return "SubsetNetworkEvaluators.FilterSubset"
			End Get
		End Property

#End Region

#Region "INetworkEvaluator2 Members"

		Public Sub Refresh() Implements INetworkEvaluator2.Refresh
			' This method is called internally during a solve operation immediately prior to performing the actual solve
			' This gives us an opportunity to update our evaluator's internal state based on parameter values

			m_restrictFilterElements = True
			m_countSourceEIDs = 0
			m_sourceEIDHashTable = New Dictionary(Of Integer, Integer)()

			Dim netAttribute2 As INetworkAttribute2 = TryCast(m_networkAttribute, INetworkAttribute2)
			Dim netAttributeParams As IArray = netAttribute2.Parameters

			' Parameters: "FilterSubset_Restrict", "FilterSubset_EIDs_<SourceName>"
			Dim prefix As String = BaseParameterName & "_"
			Dim paramRestrictFilterName As String = prefix & "Restrict"
			Dim paramEIDsName As String = prefix & "eids_" & m_networkSource.Name

			Dim nParamRestrictFilter As Integer = SubsetHelper.FindParameter(netAttributeParams, paramRestrictFilterName)
			Dim nParamEIDs As Integer = SubsetHelper.FindParameter(netAttributeParams, paramEIDsName)

			Dim value As Object

			Dim paramRestrictFilter As INetworkAttributeParameter
			Dim paramEIDs As INetworkAttributeParameter

			If nParamRestrictFilter >= 0 Then
				paramRestrictFilter = TryCast(netAttributeParams.Element(nParamRestrictFilter), INetworkAttributeParameter)
				value = paramRestrictFilter.Value
				If Not value Is Nothing Then
					m_restrictFilterElements = CBool(value)
				End If
			End If

			If nParamEIDs >= 0 Then
				paramEIDs = TryCast(netAttributeParams.Element(nParamEIDs), INetworkAttributeParameter)
				value = TryCast(paramEIDs.Value, Integer())
				If Not value Is Nothing Then
					Dim eid As Integer
					Dim rgEIDs As Integer()
					rgEIDs = CType(value, Integer())

					Dim lb As Integer = rgEIDs.GetLowerBound(0)
					Dim ub As Integer = rgEIDs.GetUpperBound(0)

					Dim i As Integer = lb
					Do While i <= ub
						m_countSourceEIDs += 1
						eid = rgEIDs(i)
						m_sourceEIDHashTable.Add(eid, eid)
						i += 1
					Loop
				End If
			End If
		End Sub

		Public ReadOnly Property RequiredFieldNames() As IStringArray Implements INetworkEvaluator2.RequiredFieldNames
			' This custom evaluator does not require any field names
			Get
				Return Nothing
			End Get
		End Property

#End Region

#Region "INetworkEvaluatorSetup Members"

		Public ReadOnly Property CLSID() As UID Implements INetworkEvaluatorSetup.CLSID
			Get
				Dim uid As UID = New UIDClass()
				uid.Value = "{e2a9fbbf-8950-48cb-b487-0ee3f43dccca}"
				Return uid
			End Get
		End Property

		Public Property Data() As IPropertySet Implements INetworkEvaluatorSetup.Data
			' The Data property is intended to make use of property sets to get/set the custom evaluator's properties using only one call to the evaluator object
			' This custom evaluator does not make use of this property
			Get
				Return Nothing
			End Get
			Set(ByVal value As IPropertySet)
			End Set
		End Property

		Public ReadOnly Property DataHasEdits() As Boolean Implements INetworkEvaluatorSetup.DataHasEdits
			' Since this custom evaluator does not make any data edits, return false
			Get
				Return False
			End Get
		End Property

		Public Sub Initialize(ByVal networkDataset As INetworkDataset, ByVal DataElement As IDENetworkDataset, ByVal netSource As INetworkSource, ByVal netAttribute As IEvaluatedNetworkAttribute) Implements INetworkEvaluatorSetup.Initialize
			' Initialize is called once per session (ArcMap session, ArcCatalog session, etc.) to initialize the evaluator for an associated network dataset            
			m_networkDataset = networkDataset
			m_networkSource = netSource
			m_networkAttribute = netAttribute

			Refresh()
		End Sub

		Public Function QueryValue(ByVal Element As INetworkElement, ByVal Row As IRow) As Object Implements INetworkEvaluatorSetup.QueryValue
			If m_countSourceEIDs <= 0 Then
				Return False
			End If

			Dim restrict As Boolean = Not m_restrictFilterElements
			Dim eid As Integer = -1
			If m_sourceEIDHashTable.TryGetValue(Element.EID, eid) Then
				restrict = m_restrictFilterElements
			End If

			Return restrict
		End Function

		Public Function SupportsDefault(ByVal ElementType As esriNetworkElementType, ByVal netAttribute As IEvaluatedNetworkAttribute) As Boolean Implements INetworkEvaluatorSetup.SupportsDefault
			Return False
		End Function

		Public Function SupportsSource(ByVal Source As INetworkSource, ByVal netAttribute As IEvaluatedNetworkAttribute) As Boolean Implements INetworkEvaluatorSetup.SupportsSource
			' This custom evaluator supports restriction attributes for all sources
			Return netAttribute.UsageType = esriNetworkAttributeUsageType.esriNAUTRestriction
		End Function

		Public Function ValidateDefault(ByVal ElementType As esriNetworkElementType, ByVal netAttribute As IEvaluatedNetworkAttribute, ByRef ErrorCode As Integer, ByRef ErrorDescription As String, ByRef errorAppendInfo As String) As Boolean Implements INetworkEvaluatorSetup.ValidateDefault
			If SupportsDefault(ElementType, netAttribute) Then
				ErrorCode = 0
				ErrorDescription = String.Empty
				errorAppendInfo = String.Empty
				Return True
			Else
				ErrorCode = -1
				ErrorDescription = String.Empty
				errorAppendInfo = String.Empty
				Return False
			End If
		End Function

		Public Function ValidateSource(ByVal datasetContainer As IDatasetContainer2, ByVal netSource As INetworkSource, ByVal netAttribute As IEvaluatedNetworkAttribute, ByRef ErrorCode As Integer, ByRef ErrorDescription As String, ByRef errorAppendInfo As String) As Boolean Implements INetworkEvaluatorSetup.ValidateSource
			If SupportsSource(netSource, netAttribute) Then
				ErrorCode = 0
				ErrorDescription = String.Empty
				errorAppendInfo = String.Empty
				Return True
			Else
				ErrorCode = -1
				ErrorDescription = String.Empty
				errorAppendInfo = String.Empty
				Return False
			End If
		End Function

#End Region

#Region "Static Members"

		Public Shared ReadOnly Property BaseParameterName() As String
			Get
				Return "FilterSubset"
			End Get
		End Property

		Public Shared Sub RemoveFilterSubsetAttribute(ByVal deNet As IDENetworkDataset)
			Dim netAttributes As IArray = SubsetHelper.RemoveAttributesByPrefix(deNet.Attributes, "Filter")
			deNet.Attributes = netAttributes
		End Sub

		Public Shared Function AddFilterSubsetAttribute(ByVal deNet As IDENetworkDataset) As IEvaluatedNetworkAttribute
			Dim netAttributes As IArray = deNet.Attributes
			Dim netAttribute As IEvaluatedNetworkAttribute = TryCast(New EvaluatedNetworkAttributeClass(), IEvaluatedNetworkAttribute)

			netAttribute.Name = BaseParameterName
			netAttribute.UsageType = esriNetworkAttributeUsageType.esriNAUTRestriction
			netAttribute.DataType = esriNetworkAttributeDataType.esriNADTBoolean
			netAttribute.Units = esriNetworkAttributeUnits.esriNAUUnknown

			Dim netAttribute2 As INetworkAttribute2 = TryCast(netAttribute, INetworkAttribute2)
			netAttribute2.UseByDefault = True

			Dim allNetSources As List(Of INetworkSource) = SubsetHelper.GetSourceList(deNet.Sources)
			Dim netSources As List(Of INetworkSource) = SubsetHelper.GetSourceList(allNetSources, esriNetworkElementType.esriNETEdge)
			Dim netSourceNames As List(Of String) = SubsetHelper.GetSourceNames(netSources)

			ResetFilterSubsetParameters(CType(netAttribute, INetworkAttribute2), netSourceNames)

			Dim supportTurns As Boolean = deNet.SupportsTurns

			'default evaluators
			SubsetHelper.SetDefaultEvaluator(netAttribute, False, esriNetworkElementType.esriNETEdge)
			SubsetHelper.SetDefaultEvaluator(netAttribute, False, esriNetworkElementType.esriNETJunction)
			If supportTurns Then
				SubsetHelper.SetDefaultEvaluator(netAttribute, False, esriNetworkElementType.esriNETTurn)
			End If

			'sourced evaluators
			For Each netSource As INetworkSource In netSources
				SubsetHelper.SetEvaluators(netAttribute, netSource, GetType(FilterSubsetEvaluator))
			Next netSource

			netAttributes.Add(netAttribute)
			deNet.Attributes = netAttributes

			Return netAttribute
		End Function

		Public Shared Sub ResetFilterSubsetParameters(ByVal netAttribute As INetworkAttribute2, ByVal netSourceNames As List(Of String))
			Dim netParams As IArray = New ESRI.ArcGIS.esriSystem.ArrayClass()
			Dim netParam As INetworkAttributeParameter = Nothing
			Dim paramValue As Object = Nothing

			netParam = New NetworkAttributeParameterClass()
			paramValue = True

			Dim paramName As String = ""

			paramName = BaseParameterName
			paramName &= "_Restrict"

			netParam.Name = paramName
			netParam.VarType = CInt(VarType.Bool)
			netParam.Value = paramValue
			netParam.DefaultValue = paramValue
			netParams.Add(netParam)

			netParam = New NetworkAttributeParameterClass()
			paramValue = 1

			For Each netSourceName As String In netSourceNames
				netParam = New NetworkAttributeParameterClass()
				paramValue = Nothing

				paramName = BaseParameterName
				paramName &= "_eids_"
				paramName &= netSourceName
				netParam.Name = paramName
				netParam.VarType = CInt(VarType.Array Or VarType.Integer)
				netParam.Value = paramValue
				netParam.DefaultValue = paramValue
				netParams.Add(netParam)
			Next netSourceName

			'does not preserve existing parameters if any
			netAttribute.Parameters = netParams
		End Sub

#End Region
	End Class
End Namespace