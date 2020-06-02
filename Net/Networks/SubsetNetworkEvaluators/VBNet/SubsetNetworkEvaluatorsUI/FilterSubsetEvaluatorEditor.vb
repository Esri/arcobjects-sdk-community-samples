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
Imports System.Windows.Forms
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.CatalogUI
Imports ESRI.ArcGIS.ADF.CATIDs

Namespace SubsetNetworkEvaluatorsUI
	''' <summary>
	''' The FilterSubsetEvaluatorEditor is used to help you to assign the filter subset evaluator manually
	''' using the evaluator dialog launched from the attributes page of the network dataset property sheet.
	''' The registration of the class as a NetworkEvaluatorEditor component allows the evaluator to show up
	''' as a choice of types in the evaluators dialog.
	''' </summary>
	<ClassInterface(ClassInterfaceType.None), Guid("bce52bcb-f560-4cce-94a5-a7625626de8c"), ProgId("SubsetNetworkEvaluatorsUI.FilterSubsetEvaluatorEditor")> _
	Public Class FilterSubsetEvaluatorEditor : Implements IEvaluatorEditor
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

#Region "Member Variables"

		Private m_EditEvaluators As IEditEvaluators
		Private Shared s_description As String = "Filter Subset Evaluator:" & ControlChars.CrLf & ControlChars.CrLf & "Usage: Restricts a subset of network elements included in a list of EIDs, or optionally restricts" & ControlChars.CrLf & "those elements NOT in the list of EIDs if FilterSubset_Restrict is false.  If FilterSubset_Restrict is false" & ControlChars.CrLf & "then in the special case that a source has NO EIDs in the list then no elements are restricted" & ControlChars.CrLf & "rather than restricting all elements." & ControlChars.CrLf & ControlChars.CrLf & "Parameters: (name:defaultvalue; type:domain; description)" & ControlChars.CrLf & "-FilterSubset_Restrict:true; Boolean; indicated if listed EIDs should be restricted (FilterSubset_Restrict=true)," & ControlChars.CrLf & "or those not in the EID list should be restricted (FilterSubset_Restrict=false)" & ControlChars.CrLf & "-FilterSubset_EIDs_<Source>:<null>; integer[]; subset of EIDs for a given source name to filter" & ControlChars.CrLf & "(replace <source> with a specific network source name such as streets without angle" & ControlChars.CrLf & "brackets to filter a specified subset of element ids for that source.  Multiple parameter" & ControlChars.CrLf & "EID subset list parameters can be added to filter a subset of multiple network sources."

#End Region

#Region "IEvaluatorEditor Members"

		Public ReadOnly Property ContextSupportsEditDescriptors() As Boolean Implements IEvaluatorEditor.ContextSupportsEditDescriptors
			' TODO: implement alternative ContextSupportsEditDescriptors logic here
			Get
				Return False
			End Get
		End Property

		Public ReadOnly Property ContextSupportsEditProperties() As Boolean Implements IEvaluatorEditor.ContextSupportsEditProperties
			' TODO: implement alternative ContextSupportsEditProperties logic here
			Get
				Return True
			End Get
		End Property

		Public Sub EditDescriptors(ByVal value As String) Implements IEvaluatorEditor.EditDescriptors
			' TODO: implement EditDescriptors
		End Sub

		Public WriteOnly Property EditEvaluators() As IEditEvaluators Implements IEvaluatorEditor.EditEvaluators
			' TODO: implement alternative EditEvaluators logic here
			Set(ByVal value As IEditEvaluators)
				m_EditEvaluators = Value
			End Set
		End Property

		Public Function EditProperties(ByVal parentWindow As Integer) As Boolean Implements IEvaluatorEditor.EditProperties
			Dim dlg As SimpleEvaluatorProperties = New SimpleEvaluatorProperties(s_description)
			Dim parentWin32Handle As IWin32Window = CType(New WindowWrapper(New IntPtr(parentWindow)), IWin32Window)
			dlg.ShowDialog(parentWin32Handle)

			Return False ' edited
		End Function

		Public ReadOnly Property EvaluatorCLSID() As UID Implements IEvaluatorEditor.EvaluatorCLSID
			Get
				Dim uid As UID = New UIDClass()
				uid.Value = "{e2a9fbbf-8950-48cb-b487-0ee3f43dccca}" ' Returns the GUID of FilterEvaluator
				Return uid
			End Get
		End Property

		Public Sub SetDefaultProperties(ByVal index As Integer) Implements IEvaluatorEditor.SetDefaultProperties
			' TODO: implement SetDefaultProperties
		End Sub

		Public WriteOnly Property ValueChoice() As Integer Implements IEvaluatorEditor.ValueChoice
			Set(ByVal value As Integer)
				' TODO: implement ValueChoice
			End Set
		End Property

		Public ReadOnly Property ValueChoiceCount() As Integer Implements IEvaluatorEditor.ValueChoiceCount
			' TODO: implement alternative ValueChoiceCount logic here
			Get
				Return 0
			End Get
		End Property

		Public ReadOnly Property Descriptor(ByVal index As Integer) As String Implements ESRI.ArcGIS.CatalogUI.IEvaluatorEditor.Descriptor
			' TODO: implement alternative Descriptor logic here
			Get
				Return String.Empty
			End Get
		End Property

		Public ReadOnly Property FullDescription(ByVal index As Integer) As String Implements ESRI.ArcGIS.CatalogUI.IEvaluatorEditor.FullDescription
			' TODO: implement alternative FullDescription logic here
			Get
				Return String.Empty
			End Get
		End Property

		Public ReadOnly Property ValueChoiceDescriptor(ByVal choice As Integer) As String Implements ESRI.ArcGIS.CatalogUI.IEvaluatorEditor.ValueChoiceDescriptor
			' TODO: implement alternative ValueChoiceDescriptor logic here
			Get
				Return String.Empty
			End Get
		End Property

#End Region
	End Class
End Namespace
