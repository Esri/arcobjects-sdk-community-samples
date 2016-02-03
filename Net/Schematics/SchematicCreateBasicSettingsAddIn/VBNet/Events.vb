' Copyright 2010 ESRI
' 
' All rights reserved under the copyright laws of the United States
' and applicable international laws, treaties, and conventions.
'
' You may freely redistribute and use this sample code, with or
' without modification, provided you include the original copyright
' notice and use restrictions.
' 
' See the use restrictions at &ltyour ArcGIS install location&gt/DeveloperKit10.0/userestrictions.txt.

Imports System
Imports System.Collections.Generic
Imports System.Collections.Specialized
Imports System.Linq
Imports System.Text

Public Class ReduceEvents
	Inherits EventArgs
	Private m_selectedObjects() As String

	Public Sub New(ByVal items() As String)
		Me.m_selectedObjects = items
	End Sub

	Public Property SelectedObjects() As String()
		Get
			Return m_selectedObjects
		End Get
		Set(ByVal value As String())
			m_selectedObjects = value
		End Set
	End Property
End Class

Public Class NameEvents
	Inherits EventArgs

	Private blnNewDataset As Boolean
	Private strDatasetName As String
	Private strTemplateName As String
	Private blnUseVertices As Boolean


    Public Sub New(ByVal blnNewDataset As Boolean, ByVal strDatasetName As String, ByVal strTemplateName As String, ByVal blnUseVertices As Boolean)

        Me.blnNewDataset = blnNewDataset
        Me.strDatasetName = strDatasetName
        Me.strTemplateName = strTemplateName
        Me.blnUseVertices = blnUseVertices
    End Sub

	Public Property NewDataset() As Boolean
		Get
			Return blnNewDataset
		End Get
		Set(ByVal value As Boolean)
			blnNewDataset = value
		End Set
	End Property

	Public Property DatasetName() As String
		Get
			Return strDatasetName
		End Get
		Set(ByVal value As String)
			strDatasetName = value
		End Set
	End Property

	Public Property TemplateName() As String
		Get
			Return strTemplateName
		End Get
		Set(ByVal value As String)
			strTemplateName = value
		End Set
	End Property

	Public Property UseVertices() As Boolean
		Get
			Return blnUseVertices
		End Get
		Set(ByVal value As Boolean)
			blnUseVertices = value
		End Set
	End Property

End Class

Public Class AdvancedEvents

	Inherits EventArgs
	Private strAlgorithmName As String
	Private dicAlgorithmParams As Dictionary(Of String, String)
	Private strRootClass As String
	Private m_FieldsToCreate As NameValueCollection

	Public Sub New(ByVal AlgorithmName As String, ByVal AlgorithmParams As Dictionary(Of String, String), ByVal RootClass As String, ByVal FieldsToCreate As NameValueCollection)
		strAlgorithmName = AlgorithmName
		dicAlgorithmParams = AlgorithmParams
		strRootClass = RootClass
		m_FieldsToCreate = FieldsToCreate
	End Sub

	Public Property AlgorithmName() As String
		Get
			Return strAlgorithmName
		End Get
		Set(ByVal value As String)
			strAlgorithmName = value
		End Set
	End Property

	Public Property RootClass() As String
		Get
			Return strRootClass
		End Get
		Set(ByVal value As String)
			strRootClass = value
		End Set
	End Property

	Public Property AlgorithmParams() As Dictionary(Of String, String)
		Get
			Return dicAlgorithmParams
		End Get

		Set(ByVal value As Dictionary(Of String, String))
			dicAlgorithmParams = value
		End Set
	End Property

	Public Property FieldsToCreate() As NameValueCollection
		Get
			Return m_FieldsToCreate
		End Get
		Set(ByVal value As NameValueCollection)
			m_FieldsToCreate = value
		End Set
	End Property
End Class
