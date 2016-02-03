Imports Microsoft.VisualBasic
Imports System
Imports System.Runtime.InteropServices
Imports System.Collections
Imports System.Collections.Generic
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.ArcMapUI
Imports ESRI.ArcGIS.Carto

Namespace SelectionRestriction
	<ClassInterface(ClassInterfaceType.None), Guid("32C6254F-7E03-4b57-9EA2-35EA8DAFA306")> _
	Public Class SelectionRestrictionEvaluator : Implements INetworkEvaluator2, INetworkEvaluatorSetup
#Region "Member Variables"

		Private m_networkDataset As INetworkDataset						' Used to store a reference to the network dataset
		Private m_networkSource As INetworkSource						' Used to store a reference to the network source associated with this evaluator
		Private m_mxDocument As IMxDocument								' Used to store a reference to the current MxDocument
		Private m_sourceHashTable As Dictionary(Of Integer, Integer)	' Used to store a dynamic hash table of selected OIDs for the evaluator's network source

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
				Return "SelectionRestriction"
			End Get
		End Property

		Public ReadOnly Property Name() As String Implements INetworkEvaluator.Name, INetworkEvaluator2.Name
			Get
				Return "SelectionRestriction.SelectionRestrictionEvaluator"
			End Get
		End Property

#End Region

#Region "INetworkEvaluatorSetup Members"

		Public ReadOnly Property CLSID() As UID Implements INetworkEvaluatorSetup.CLSID
			Get
				' Create and return the GUID for this custom evaluator
				Dim uid As UID = New UIDClass()
				uid.Value = "{32C6254F-7E03-4b57-9EA2-35EA8DAFA306}"
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

		Public Sub Initialize(ByVal networkDataset As INetworkDataset, ByVal dataElement As IDENetworkDataset, ByVal source As INetworkSource, ByVal attribute As IEvaluatedNetworkAttribute) Implements INetworkEvaluatorSetup.Initialize
			' Initialize is called once per session (ArcMap session, ArcCatalog session, etc.) to initialize the evaluator for an associated network dataset
			Dim t As Type = Type.GetTypeFromProgID("esriFramework.AppRef")
			' Activator.CreateInstance(t) is expected to error if the evaluator is created in an engine application 
			' which can’t get a reference to the AppRef singleton.  
			' This evaluator won’t work in Engine due to this design limitation.  It is, however,
			' fully functional in ArcMap.
			Try
				Dim obj As System.Object = Activator.CreateInstance(t)
				Dim app As IApplication = TryCast(obj, IApplication)
				If Not app Is Nothing AndAlso TypeOf app Is IMxApplication Then
					m_mxDocument = TryCast(app.Document, IMxDocument)
				End If
			Catch ex As Exception
				m_mxDocument = Nothing
			End Try

			' Store reference to the network dataset and the network source
			m_networkDataset = networkDataset
			m_networkSource = source

			' Create a new Dictionary hash table for this network source
			m_sourceHashTable = New Dictionary(Of Integer, Integer)()
		End Sub

		Public Function QueryValue(ByVal element As INetworkElement, ByVal row As IRow) As Object Implements INetworkEvaluatorSetup.QueryValue
			' This element is restricted if its associated ObjectID is currently stored within the network source's hash table
			Return m_sourceHashTable.ContainsKey(element.OID)
		End Function

		Public Function SupportsDefault(ByVal elementType As esriNetworkElementType, ByVal attribute As IEvaluatedNetworkAttribute) As Boolean Implements INetworkEvaluatorSetup.SupportsDefault
			' This custom evaluator can not be used for assigning default attribute values
			Return False
		End Function

		Public Function SupportsSource(ByVal source As INetworkSource, ByVal attribute As IEvaluatedNetworkAttribute) As Boolean Implements INetworkEvaluatorSetup.SupportsSource
			' This custom evaluator supports restriction attributes for all sources
			Return attribute.UsageType = esriNetworkAttributeUsageType.esriNAUTRestriction
		End Function

		Public Function ValidateDefault(ByVal elementType As esriNetworkElementType, ByVal attribute As IEvaluatedNetworkAttribute, ByRef errorCode As Integer, ByRef errorDescription As String, ByRef errorAppendInfo As String) As Boolean Implements INetworkEvaluatorSetup.ValidateDefault
			If SupportsDefault(elementType, attribute) Then
				errorCode = 0
				errorDescription = String.Empty
				errorAppendInfo = String.Empty
				Return True
			Else
				errorCode = -1
				errorDescription = String.Empty
				errorAppendInfo = String.Empty
				Return False
			End If
		End Function

		Public Function ValidateSource(ByVal datasetContainer As IDatasetContainer2, ByVal networkSource As INetworkSource, ByVal attribute As IEvaluatedNetworkAttribute, ByRef errorCode As Integer, ByRef errorDescription As String, ByRef errorAppendInfo As String) As Boolean Implements INetworkEvaluatorSetup.ValidateSource
			If SupportsSource(networkSource, attribute) Then
				errorCode = 0
				errorDescription = String.Empty
				errorAppendInfo = String.Empty
				Return True
			Else
				errorCode = -1
				errorDescription = String.Empty
				errorAppendInfo = String.Empty
				Return False
			End If
		End Function

#End Region

#Region "INetworkEvaluator2 Members"

		Public Sub Refresh() Implements INetworkEvaluator2.Refresh
			' This method is called internally during a solve operation immediately prior to performing the actual solve
			' This gives us an opportunity to update our evaluator's internal state based on changes to the current source feature selection set within ArcMap

			If Not m_mxDocument Is Nothing Then
				' Clear the hash table of any previous selections
				m_sourceHashTable.Clear()

				' Loop through every layer in the map, find the appropriate network source feature layer, and add its selection set to the source hash table
				Dim map As IMap = m_mxDocument.FocusMap
				Dim fcContainer As IFeatureClassContainer = CType(m_networkDataset, IFeatureClassContainer)
				Dim sourceFC As IFeatureClass = fcContainer.ClassByName(m_networkSource.Name)

				Dim layer As ILayer
				Dim layerFC As IFeatureClass
				Dim enumLayer As IEnumLayer = map.Layers(Nothing, True)
				layer = enumLayer.Next()
				Do While Not layer Is Nothing
					If layer.Visible AndAlso TypeOf layer Is IFeatureLayer Then
						layerFC = (CType(layer, IFeatureLayer)).FeatureClass
						If layerFC Is sourceFC Then
							Dim featureSelection As IFeatureSelection = CType(layer, IFeatureSelection)
							Dim selectionSet As ISelectionSet = featureSelection.SelectionSet
							Dim idEnumerator As IEnumIDs = selectionSet.IDs
							idEnumerator.Reset()
							Dim oid As Integer
							oid = idEnumerator.Next()
							Do While oid <> -1
								m_sourceHashTable.Add(oid, oid)
								oid = idEnumerator.Next()
							Loop
							Exit Do
						End If
					End If
					layer = enumLayer.Next()
				Loop
			End If
		End Sub

		Public ReadOnly Property RequiredFieldNames() As IStringArray Implements INetworkEvaluator2.RequiredFieldNames
			' This custom evaluator does not require any field names
			Get
				Return Nothing
			End Get
		End Property

#End Region
	End Class
End Namespace
