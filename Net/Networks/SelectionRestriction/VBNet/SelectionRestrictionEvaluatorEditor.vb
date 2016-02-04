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
Imports System.Runtime.InteropServices
Imports System.Windows.Forms
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.CatalogUI
Imports ESRI.ArcGIS.ADF.CATIDs

Namespace SelectionRestriction
	<ClassInterface(ClassInterfaceType.None), Guid("C6724DF5-42A5-4795-A21C-93A29450D686")> _
	Public Class SelectionRestrictionEvaluatorEditor : Implements IEvaluatorEditor
#Region "Component Category Registration"

		<ComRegisterFunction(), ComVisible(False)> _
		Private Shared Sub RegisterFunction(ByVal registerType As Type)
			Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
			NetworkEvaluatorEditor.Register(regKey)
		End Sub

		<ComUnregisterFunction(), ComVisible(False)> _
		Private Shared Sub UnregisterFunction(ByVal registerType As Type)
			Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
			NetworkEvaluatorEditor.Unregister(regKey)
		End Sub

#End Region

#Region "IEvaluatorEditor Members"

		Public ReadOnly Property ContextSupportsEditDescriptors() As Boolean Implements IEvaluatorEditor.ContextSupportsEditDescriptors
			' The descriptor text is the single line of text in the Evaluators dialog that appears under the Value column
			' This property indicates whether the descriptor text can be directly edited in the dialog by the user
			' Since this evaluator editor does not make use of descriptors, it returns false
			Get
				Return False
			End Get
		End Property

		Public ReadOnly Property ContextSupportsEditProperties() As Boolean Implements IEvaluatorEditor.ContextSupportsEditProperties
			' This property indicates whether the ArcCatalog user is able to bring up a dialog by clicking the Properties button (or pressing F12) in order to specify settings for the evaluator
			' This evaluator editor does not support editing of the evaluator properties
			Get
				Return False
			End Get
		End Property

		Public Sub EditDescriptors(ByVal value As String) Implements IEvaluatorEditor.EditDescriptors
			' This evaluator editor does not make use of descriptors
		End Sub

		Public WriteOnly Property EditEvaluators() As IEditEvaluators Implements IEvaluatorEditor.EditEvaluators
			' This property is used by ArcCatalog to set a reference to its EditEvaluators object on each registered EvaluatorEditor
			' This allows each EvaluatorEditor to access the current state of ArcCatalog's Evaluators dialog, such as how many evaluators are listed and which evaluators are currently selected
			' This evaluator editor does not make use of EditEvaluators
			Set(ByVal value As IEditEvaluators)
			End Set
		End Property

		Public Function EditProperties(ByVal parentWindow As Integer) As Boolean Implements IEvaluatorEditor.EditProperties
			' This evaluator editor does not support editing of the evaluator properties
			Return False
		End Function

		Public ReadOnly Property EvaluatorCLSID() As UID Implements IEvaluatorEditor.EvaluatorCLSID
			Get
				' This property returns the GUID of this EvaluatorEditor's associated INetworkEvaluator (e.g., SelectionRestrictionEvaluator)
				Dim uid As UID = New UIDClass()
				uid.Value = "{32C6254F-7E03-4b57-9EA2-35EA8DAFA306}"
				Return uid
			End Get
		End Property

		Public Sub SetDefaultProperties(ByVal index As Integer) Implements IEvaluatorEditor.SetDefaultProperties
			' This method is called when the ArcCatalog user selects this evaluator under the Type column of the Evaluators dialog.
			' This method can be used to initialize any dialogs that the evaluator editor uses
			' Since this evaluator editor has no dialogs, it does not need to initialize anything
		End Sub

		Public WriteOnly Property ValueChoice() As Integer Implements IEvaluatorEditor.ValueChoice
			' This evaluator editor does not support value choices
			Set(ByVal value As Integer)
			End Set
		End Property

		Public ReadOnly Property ValueChoiceCount() As Integer Implements IEvaluatorEditor.ValueChoiceCount
			' This evaluator editor has no value choices
			Get
				Return 0
			End Get
		End Property

		Public ReadOnly Property Descriptor(ByVal index As Integer) As String Implements IEvaluatorEditor.Descriptor
			' This evaluator editor does not make use of descriptors
			Get
				Return String.Empty
			End Get
		End Property

		Public ReadOnly Property FullDescription(ByVal index As Integer) As String Implements IEvaluatorEditor.FullDescription
			' This property is the text representation of all of the settings made on this evaluator
			' This evaluator editor does not make any settings changes, so it returns an empty string
			Get
				Return String.Empty
			End Get
		End Property

		Public ReadOnly Property ValueChoiceDescriptor(ByVal choice As Integer) As String Implements IEvaluatorEditor.ValueChoiceDescriptor
			' This evaluator editor does not make use of value choices
			Get
				Return String.Empty
			End Get
		End Property

#End Region
	End Class
End Namespace
