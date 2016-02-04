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
Imports System
Imports System.Runtime.InteropServices
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Geodatabase
Imports Timestamper.My.Resources

''' <summary>
''' A custom feature class description for use with the TimestampClassExtension. The properties return
''' nearly the same values as the FeatureClassDescription class, with the exception of the Name
''' ("Timestamped Class") and DefaultFields (returns three additional fields, Date fields for features'
''' creation and modification dates and a text field for a user name).
''' </summary>
<Guid("858e76eb-18b0-47e1-b69b-288541fe1c96")> _
<ClassInterface(ClassInterfaceType.None)> _
<ProgId("Timestamper.TimestampClassDescription")> _
<ComVisible(True)> _
Public Class TimestampClassDescription
	Implements IObjectClassDescription
	Implements IFeatureClassDescription

#Region "IObjectClassDescription Members"
	''' <summary>
	''' The alias of the described class.
	''' </summary>
	Public ReadOnly Property AliasName() As String Implements IObjectClassDescription.AliasName
		Get
			AliasName = String.Empty
		End Get
	End Property

	''' <summary>
	''' The described class' extension UID.
	''' </summary>
	Public ReadOnly Property ClassExtensionCLSID() As ESRI.ArcGIS.esriSystem.UID Implements IObjectClassDescription.ClassExtensionCLSID
		Get
			' Return the UID of TimestampClassExtension.
			Dim uid As UID = New UIDClass()
			uid.Value = "{becd0269-32f2-4a21-9145-619a891e7862}"
			ClassExtensionCLSID = uid
		End Get
	End Property

	''' <summary>
	''' The described class' instance UID.
	''' </summary>
	Public ReadOnly Property InstanceCLSID() As ESRI.ArcGIS.esriSystem.UID Implements IObjectClassDescription.InstanceCLSID
		Get
			' Return the UID of Feature.
			Dim uid As UID = New UIDClass()
			uid.Value = "{52353152-891A-11D0-BEC6-00805F7C4268}"
			InstanceCLSID = uid
		End Get
	End Property

	''' <summary>
	''' The model name of the described class.
	''' </summary>
	Public ReadOnly Property ModelName() As String Implements IObjectClassDescription.ModelName
		Get
			ModelName = String.Empty
		End Get
	End Property

	''' <summary>
	''' Indicates if the model name of the described class is unique.
	''' </summary>
	Public ReadOnly Property ModelNameUnique() As Boolean Implements IObjectClassDescription.ModelNameUnique
		Get
			ModelNameUnique = False
		End Get
	End Property

	''' <summary>
	''' The name of the described class.
	''' </summary>
	Public ReadOnly Property Name() As String Implements IObjectClassDescription.Name
		Get
			Name = Resources.TimestampedClassName
		End Get
	End Property

	''' <summary>
	''' The set of required fields for the described class.
	''' </summary>
	Public ReadOnly Property RequiredFields() As IFields Implements IObjectClassDescription.RequiredFields
		Get
			' Get the feature class required fields.
			Dim fcDescription As IFeatureClassDescription = New FeatureClassDescriptionClass()
			Dim ocDescription As IObjectClassDescription = CType(fcDescription, IObjectClassDescription)
			Dim fcRequiredFields As IFields = ocDescription.RequiredFields
			Dim fcRequiredFieldsEdit As IFieldsEdit = CType(fcRequiredFields, IFieldsEdit)

			' Add a created date field.
			Dim createdField As IField = New FieldClass()
			Dim createdFieldEdit As IFieldEdit = CType(createdField, IFieldEdit)
			createdFieldEdit.Name_2 = Resources.DefaultCreatedField
			createdFieldEdit.Required_2 = False
			createdFieldEdit.IsNullable_2 = True
			createdFieldEdit.Type_2 = esriFieldType.esriFieldTypeDate
			fcRequiredFieldsEdit.AddField(createdField)

			' Add a modified date field.
			Dim modifiedField As IField = New FieldClass()
			Dim modifiedFieldEdit As IFieldEdit = CType(modifiedField, IFieldEdit)
			modifiedFieldEdit.Name_2 = Resources.DefaultModifiedField
			modifiedFieldEdit.Required_2 = False
			modifiedFieldEdit.IsNullable_2 = True
			modifiedFieldEdit.Type_2 = esriFieldType.esriFieldTypeDate
			fcRequiredFieldsEdit.AddField(modifiedField)

			' Add a user text field.
			Dim userField As IField = New FieldClass()
			Dim userFieldEdit As IFieldEdit = CType(userField, IFieldEdit)
			userFieldEdit.Name_2 = Resources.DefaultUserField
			userFieldEdit.Required_2 = False
			userFieldEdit.IsNullable_2 = True
			userFieldEdit.Type_2 = esriFieldType.esriFieldTypeString
			userFieldEdit.Length_2 = 100
			fcRequiredFieldsEdit.AddField(userField)

			RequiredFields = fcRequiredFields
		End Get
	End Property
#End Region

#Region "IFeatureClassDescription Members"
	''' <summary>
	''' The esriFeatureType for the instances of the described class.
	''' </summary>
	Public ReadOnly Property FeatureType() As esriFeatureType Implements IFeatureClassDescription.FeatureType
		Get
			FeatureType = esriFeatureType.esriFTSimple
		End Get
	End Property

	''' <summary>
	''' The name of the described class' geometry field.
	''' </summary>
	Public ReadOnly Property ShapeFieldName() As String Implements IFeatureClassDescription.ShapeFieldName
		Get
			' Use the feature class default.
			Dim fcDescription As IFeatureClassDescription = New FeatureClassDescriptionClass()
			ShapeFieldName = fcDescription.ShapeFieldName
		End Get
	End Property
#End Region

#Region "COM Registration Function(s)"
	<ComRegisterFunction(), ComVisibleAttribute(False)> _
	Public Shared Sub RegisterFunction(ByVal registerType As Type)
		Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
		GeoObjectClassDescriptions.Register(regKey)
	End Sub

	<ComUnregisterFunction(), ComVisibleAttribute(False)> _
	Public Shared Sub UnregisterFunction(ByVal registerType As Type)
		Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
		GeoObjectClassDescriptions.Unregister(regKey)
	End Sub
#End Region
End Class
