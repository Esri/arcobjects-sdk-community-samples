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
Imports System.Collections.Generic
Imports System.Text
Imports System.Runtime.InteropServices
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Geodatabase

Namespace SubsetNetworkEvaluators
	''' <summary>
	''' The scale subset network evaluator is a custom network evaluator for modeling slowdown polygons
	''' where traffic speeds are slowed down in only a particular subset of the network.  In this
	''' example the subset of network elements that are slowed down is determined based on the geometry
	''' of graphic elements drawn in arc map, but it does not matter how the element subset is determined.
	''' The elements that are not in the subset just return the non-scaled base attribute value.  This could
	''' be useful, for example, if certain low lying areas had a flash flood, or other localized congestion that
	''' does not affect the network as a whole.  The subset of elements to be scaled and the scale factor to
	''' scale the base attribute by in the scale subset evaluator are network attribute parameters of the attribute
	''' the evaluator is assigned to.
	''' </summary>

	<ClassInterface(ClassInterfaceType.None), Guid("67cf8446-22a2-4baf-9c97-3c22a33cc0c7"), ProgId("SubsetNetworkEvaluators.ScaleSubsetEvaluator")> _
	Public Class ScaleSubsetEvaluator : Implements INetworkEvaluator2, INetworkEvaluatorSetup
#Region "Member Variables"

		Private m_networkDataset As INetworkDataset
		Private m_networkSource As INetworkSource
		Private m_networkAttribute As INetworkAttribute

		Private m_scaleFactor As Double = 1
		Private m_thisNetworkAttributeID As Integer = -1 ' the ID for this attribute
		Private m_baseNetworkAttributeID As Integer = -1 ' the ID for the other attribute that should be scale (determined based on attribute name)
		Private m_countSourceEIDs As Integer = 0 ' number of EIDs to override for this source
		Private m_sourceEIDHashTable As Dictionary(Of Integer, Integer)	' the EIDs to override values for, by scaling, for this source

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
				Return "ScaleSubset"
			End Get
		End Property

		Public ReadOnly Property Name() As String Implements INetworkEvaluator.Name, INetworkEvaluator2.Name
			Get
				Return "SubsetNetworkEvaluators.ScaleSubset"
			End Get
		End Property

#End Region

#Region "INetworkEvaluator2 Members"

		Public Sub Refresh() Implements INetworkEvaluator2.Refresh
			' This method is called internally during a solve operation immediately prior to performing the actual solve
			' This gives us an opportunity to update our evaluator's internal state based on parameter values

			m_scaleFactor = 1
			m_countSourceEIDs = 0
			m_sourceEIDHashTable = New Dictionary(Of Integer, Integer)()

			Dim netAttribute2 As INetworkAttribute2 = TryCast(m_networkAttribute, INetworkAttribute2)
			Dim netAttributeParams As IArray = netAttribute2.Parameters

			' Parameters: "ScaleSubset_Factor", "ScaleSubset_EIDs_<SourceName>"
			Dim prefix As String = BaseParameterName & "_"

			Dim paramScaleFactorName As String = prefix & "Factor"
			Dim paramEIDsName As String = prefix & "eids_" & m_networkSource.Name

			Dim nParamScaleFactor As Integer = SubsetHelper.FindParameter(netAttributeParams, paramScaleFactorName)
			Dim nParamEIDs As Integer = SubsetHelper.FindParameter(netAttributeParams, paramEIDsName)

			Dim value As Object

			Dim paramScaleFactor As INetworkAttributeParameter
			Dim paramEIDs As INetworkAttributeParameter

			If nParamScaleFactor >= 0 Then
				paramScaleFactor = TryCast(netAttributeParams.Element(nParamScaleFactor), INetworkAttributeParameter)
				value = paramScaleFactor.Value
				If Not value Is Nothing Then
					m_scaleFactor = CDbl(value)
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
				' Create and return the GUID for this custom evaluator
				Dim uid As UID = New UIDClass()
				uid.Value = "{67cf8446-22a2-4baf-9c97-3c22a33cc0c7}"
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

			m_thisNetworkAttributeID = netAttribute.ID
			m_baseNetworkAttributeID = -1

			'The attribute name must begin with one or more non underscore characters followed by
			'an underscore character and then the name of the base cost attribute.
			'The underscore prior to the base attribute name should be the first underscore in the name.

			Dim thisAttributeName As String = netAttribute.Name
			Dim nPos As Integer = thisAttributeName.IndexOf("_"c)
			Dim nLastPos As Integer = thisAttributeName.Length - 1

			Dim baseNetAttributeName As String
			Dim baseNetAttribute As INetworkAttribute = Nothing

			If nPos > 0 AndAlso nPos < nLastPos Then
				baseNetAttributeName = thisAttributeName.Remove(0, nPos + 1)
				Try
					baseNetAttribute = networkDataset.AttributeByName(baseNetAttributeName)
				Catch ex As COMException
					baseNetAttribute = Nothing
					Dim msg As String = String.Format("Base Attribute ({0}) not found. {1}.", baseNetAttributeName, ex.Message)
					System.Diagnostics.Trace.WriteLine(msg, "Scale Subset Network Evaluator")
				End Try

				If Not baseNetAttribute Is Nothing Then
					If baseNetAttribute.ID <> m_thisNetworkAttributeID Then
						m_baseNetworkAttributeID = baseNetAttribute.ID
					End If
				End If
			End If

			Refresh()
		End Sub

		Public Function QueryValue(ByVal Element As INetworkElement, ByVal Row As IRow) As Object Implements INetworkEvaluatorSetup.QueryValue
			If m_baseNetworkAttributeID < 0 Then
				Return -1
			End If

			Dim value As Object = Element.AttributeValue(m_baseNetworkAttributeID)
			If value Is Nothing Then
				Return -1
			End If

			Dim baseValue As Double = CDbl(value)
			If baseValue <= 0 OrElse m_scaleFactor = 1 OrElse m_countSourceEIDs <= 0 Then
				Return baseValue
			End If

			Dim isScaled As Boolean = False

			Dim eid As Integer = -1
			If m_sourceEIDHashTable.TryGetValue(Element.EID, eid) Then
				isScaled = (eid > 0)
			End If

			Dim resultValue As Object = baseValue
			If isScaled Then
				If m_scaleFactor >= 0 Then
					resultValue = m_scaleFactor * baseValue
				Else
					resultValue = -1
				End If
			End If

			Return resultValue
		End Function

		Public Function SupportsDefault(ByVal ElementType As esriNetworkElementType, ByVal netAttribute As IEvaluatedNetworkAttribute) As Boolean Implements INetworkEvaluatorSetup.SupportsDefault
			Return False
		End Function

		Public Function SupportsSource(ByVal netSource As INetworkSource, ByVal netAttribute As IEvaluatedNetworkAttribute) As Boolean Implements INetworkEvaluatorSetup.SupportsSource
			' This custom evaluator supports cost attributes for all sources
			Return netAttribute.UsageType = esriNetworkAttributeUsageType.esriNAUTCost
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
				Return "ScaleSubset"
			End Get
		End Property

		Public Shared Sub RemoveScaleSubsetAttributes(ByVal deNet As IDENetworkDataset)
			Dim netAttributes As IArray = SubsetHelper.RemoveAttributesByPrefix(deNet.Attributes, BaseParameterName)
			deNet.Attributes = netAttributes
		End Sub

		Public Shared Function AddScaleSubsetAttributes(ByVal deNet As IDENetworkDataset) As List(Of IEvaluatedNetworkAttribute)
			Dim scaleSubsetAttributes As List(Of IEvaluatedNetworkAttribute) = New List(Of IEvaluatedNetworkAttribute)()

			Dim netAttributesArray As IArray = deNet.Attributes
			Dim baseIndexes As List(Of Integer) = SubsetHelper.FindAttributeIndexes(netAttributesArray, esriNetworkAttributeUsageType.esriNAUTCost, esriNetworkAttributeDataType.esriNADTDouble, True, False)
			Dim baseNetAttributes As List(Of INetworkAttribute2) = SubsetHelper.FindAttributes(netAttributesArray, baseIndexes)
			For Each baseNetAttribute As INetworkAttribute2 In baseNetAttributes
				scaleSubsetAttributes.Add(AddScaleSubsetAttribute(deNet, baseNetAttribute))
			Next baseNetAttribute

			Return scaleSubsetAttributes
		End Function

		Public Shared Function AddScaleSubsetAttribute(ByVal deNet As IDENetworkDataset, ByVal baseNetAttribute As INetworkAttribute2) As IEvaluatedNetworkAttribute
			If baseNetAttribute Is Nothing Then
				Return Nothing
			End If

			If baseNetAttribute.UsageType <> esriNetworkAttributeUsageType.esriNAUTCost Then
				Return Nothing
			End If

			Dim netAttributes As IArray = deNet.Attributes
			Dim netAttribute As IEvaluatedNetworkAttribute = TryCast(New EvaluatedNetworkAttributeClass(), IEvaluatedNetworkAttribute)

			Dim netAttributeName As String = BaseParameterName
			netAttributeName &= "_"
			netAttributeName &= baseNetAttribute.Name

			netAttribute.Name = netAttributeName
			netAttribute.UsageType = baseNetAttribute.UsageType
			netAttribute.DataType = baseNetAttribute.DataType
			netAttribute.Units = baseNetAttribute.Units

			Dim allNetSources As List(Of INetworkSource) = SubsetHelper.GetSourceList(deNet.Sources)
			Dim netSources As List(Of INetworkSource) = SubsetHelper.GetSourceList(allNetSources, esriNetworkElementType.esriNETEdge)
			Dim netSourceNames As List(Of String) = SubsetHelper.GetSourceNames(netSources)

			ResetScaleSubsetParameters(CType(netAttribute, INetworkAttribute2), netSourceNames)

			Dim supportTurns As Boolean = deNet.SupportsTurns

			'default evaluators
			SubsetHelper.SetDefaultEvaluator(netAttribute, 0, esriNetworkElementType.esriNETEdge)
			SubsetHelper.SetDefaultEvaluator(netAttribute, 0, esriNetworkElementType.esriNETJunction)
			If supportTurns Then
				SubsetHelper.SetDefaultEvaluator(netAttribute, 0, esriNetworkElementType.esriNETTurn)
			End If

			'sourced evaluators
			For Each netSource As INetworkSource In netSources
				SubsetHelper.SetEvaluators(netAttribute, netSource, GetType(ScaleSubsetEvaluator))
			Next netSource

			netAttributes.Add(netAttribute)
			deNet.Attributes = netAttributes

			Return netAttribute
		End Function

		Public Shared Sub ResetScaleSubsetParameters(ByVal netAttribute As INetworkAttribute2, ByVal netSourceNames As List(Of String))
			Dim netParams As IArray = New ESRI.ArcGIS.esriSystem.ArrayClass()
			Dim netParam As INetworkAttributeParameter = Nothing
			Dim paramValue As Object = Nothing
			Dim paramName As String = ""

			netParam = New NetworkAttributeParameterClass()
			paramValue = 1

			paramName = BaseParameterName
			paramName &= "_Factor"

			netParam.Name = paramName
			netParam.VarType = CInt(VarType.Double)
			netParam.Value = paramValue
			netParam.DefaultValue = paramValue
			netParams.Add(netParam)

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

