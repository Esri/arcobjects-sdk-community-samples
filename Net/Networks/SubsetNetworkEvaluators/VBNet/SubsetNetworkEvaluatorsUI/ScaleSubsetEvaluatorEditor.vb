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
	''' The ScaleSubsetEvaluatorEditor is used to help you to assign the scale subset evaluator manually
	''' using the evaluator dialog launched from the attributes page of the network dataset property sheet.
	''' The registration of the class as a NetworkEvaluatorEditor component allows the evaluator to show up
	''' as a choice of types in the evaluators dialog.
	''' </summary>
	<ClassInterface(ClassInterfaceType.None), Guid("08aed73c-da8a-4d02-9e3a-453870cda178"), ProgId("SubsetNetworkEvaluatorsUI.ScaleSubsetEvaluatorEditor")> _
	Public Class ScaleSubsetEvaluatorEditor : Implements IEvaluatorEditor
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
		Private Shared s_description As String = "Scale Subset Evaluator:" & ControlChars.CrLf & ControlChars.CrLf & "Usage: Returns the base attribute value times a ScaleSubset_Factor for a subset of" & ControlChars.CrLf & "elements of a base attribute (and non-scaled elements simply return an non-scaled" & ControlChars.CrLf & "base attribute value for the element).  The base attribute and this evaluator's" & ControlChars.CrLf & "attribute should both be a double cost attribute of the same units.  The base" & ControlChars.CrLf & "attribute is implied by this evaluator's attribute name. The base attribute name" & ControlChars.CrLf & "is the part of this evaluator's attribute name after the first underscore ('_')" & ControlChars.CrLf & "character. So, if this attribute is 'ScaleSubset_Minutes', then the base attribute is" & ControlChars.CrLf & "'Minutes'. The parameter values indicate the ScaleSubset_Factor and the subset of elements to" & ControlChars.CrLf & "be scaled." & ControlChars.CrLf & ControlChars.CrLf & "Parameters: (name:defaultvalue; type:domain; description)" & ControlChars.CrLf & "-ScaleSubset_Factor:1; double:>0; the amount to scale applicable base attribute values" & ControlChars.CrLf & "-ScaleSubset_EIDs_<Source>:<null>; integer[]; subset of EIDs for a given source name to scale" & ControlChars.CrLf & "(replace <source> with a specific network source name such as streets without angle" & ControlChars.CrLf & "brackets to scale a specified subset of element ids for that source.  Multiple parameter" & ControlChars.CrLf & "EID subset list parameters can be added to scale a subset of multiple network sources."

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
				m_EditEvaluators = value
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
				uid.Value = "{67cf8446-22a2-4baf-9c97-3c22a33cc0c7}" ' Returns the GUID of ScaleEvaluator
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
