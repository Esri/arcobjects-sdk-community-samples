Imports System
Imports System.Runtime.InteropServices
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Geodatabase
Imports Timestamper.My.Resources

''' <summary>
''' A feature class extension for timestamping features with creation dates, modification dates, and
''' the name of the user who created or last modified the feature.
''' </summary>
<Guid("becd0269-32f2-4a21-9145-619a891e7862")> _
<ClassInterface(ClassInterfaceType.None)> _
<ProgId("Timestamper.TimestampClassExtension")> _
<ComVisible(True)> _
Public Class TimestampClassExtension
	Implements IClassExtension
	Implements IFeatureClassExtension
	Implements IObjectClassEvents
	Implements IObjectClassExtension
	Implements IObjectClassInfo

#Region "Member Variables"
	''' <summary>
	''' Provides a reference to the extension's class.
	''' </summary>
	Private classHelper As IClassHelper = Nothing

	''' <summary>
	''' The extension properties.
	''' </summary>
	Private extensionProperties As IPropertySet = Nothing

	''' <summary>
	''' The name of the "created" date field.
	''' </summary>
	Private createdFieldName As String = Resources.DefaultCreatedField

	''' <summary>
	''' The position of the "created" date field.
	''' </summary>
	Private createdFieldIndex As Integer = -1

	''' <summary>
	''' The name of the "modified" date field.
	''' </summary>
	Private modifiedFieldName As String = Resources.DefaultModifiedField

	''' <summary>
	''' The position of the "modified" date field.
	''' </summary>
	Private modifiedFieldIndex As Integer = -1

	''' <summary>
	''' The name of the "user" text field.
	''' </summary>
	Private userFieldName As String = Resources.DefaultUserField

	''' <summary>
	''' The position of the "user" text field.
	''' </summary>
	Private userFieldIndex As Integer = -1

	''' <summary>
	''' The length of the "user" text field.
	''' </summary>
	Private userFieldLength As Integer = 0

	''' <summary>
	''' The name of the current user.
	''' </summary>
	Private userName As String = ""
#End Region

#Region "IClassExtension Methods"
	''' <summary>
	''' Initializes the extension.
	''' </summary>
	''' <param name="classHelper">Provides a reference to the extension's class.</param>
	''' <param name="extensionProperties">A set of properties unique to the extension.</param>
	Public Sub Init(ByVal classHelper As IClassHelper, ByVal extensionProperties As IPropertySet) Implements IClassExtension.Init
		' Store the class helper as a member variable.
		Me.classHelper = classHelper
		Dim baseClass As IClass = classHelper.Class

		' Get the names of the created and modified fields, if they exist.
		If Not extensionProperties Is Nothing Then
			Me.extensionProperties = extensionProperties

			Dim createdObject As Object = extensionProperties.GetProperty(Resources.CreatedFieldKey)
			Dim modifiedObject As Object = extensionProperties.GetProperty(Resources.ModifiedFieldKey)
			Dim userObject As Object = extensionProperties.GetProperty(Resources.UserFieldKey)

			' Make sure the properties exist and are strings.
			If Not createdObject Is Nothing And TypeOf createdObject Is String Then
				createdFieldName = TryCast(createdObject, String)
			End If

			If Not modifiedObject Is Nothing And TypeOf modifiedObject Is String Then
				modifiedFieldName = TryCast(modifiedObject, String)
			End If

			If Not userObject Is Nothing And TypeOf userObject Is String Then
				userFieldName = TryCast(userObject, String)
			End If
		Else
			' First time the extension has been run. Initialize with default values.
			InitNewExtension()
		End If

		' Set the positions of the fields.
		SetFieldIndexes()

		' Set the current user name.
		userName = GetCurrentUser()
	End Sub

	''' <summary>
	''' Informs the extension that the class is being disposed of.
	''' </summary>
	Public Sub Shutdown() Implements IClassExtension.Shutdown
		classHelper = Nothing
	End Sub
#End Region

#Region "IObjectClassEvents Methods"
	''' <summary>
	''' Fired when an object's attributes or geometry is updated.
	''' </summary>
	''' <param name="obj">The updated object.</param>
	Public Sub OnChange(ByVal obj As IObject) Implements IObjectClassEvents.OnChange
		' Set the modified field's value to the current date and time.
		If modifiedFieldIndex <> -1 Then
			obj.Value(modifiedFieldIndex) = DateTime.Now

			' Set the user field's value to the current user.
			If userFieldIndex <> -1 Then
				obj.Value(userFieldIndex) = userName
			End If
		End If
	End Sub

	''' <summary>
	''' Fired when a new object is created.
	''' </summary>
	''' <param name="obj">The new object.</param>
	Public Sub OnCreate(ByVal obj As IObject) Implements IObjectClassEvents.OnCreate
		' Set the created field's value to the current date and time.
		If createdFieldIndex <> -1 Then
			obj.Value(createdFieldIndex) = DateTime.Now
		End If

		' Set the user field's value to the current user.
		If userFieldIndex <> -1 Then
			obj.Value(userFieldIndex) = userName
		End If
	End Sub

	''' <summary>
	''' Fired when an object is deleted.
	''' </summary>
	''' <param name="obj">The deleted object.</param>
	Public Sub OnDelete(ByVal obj As IObject) Implements IObjectClassEvents.OnDelete
	End Sub
#End Region

#Region "IObjectClassInfo Methods"
	''' <summary>
	''' Indicates if updates to objects can bypass the Store method and OnChange notifications for efficiency.
	''' </summary>
	''' <returns>False; this extension requires Store to be called.</returns>
	Public Function CanBypassStoreMethod() As Boolean Implements IObjectClassInfo.CanBypassStoreMethod
		Return False
	End Function
#End Region

#Region "Public Members"
	''' <summary>
	''' Changes the member variables and extension properties to store the provided field names
	''' as the created, modified and user fields (positions are also refreshed). Empty strings
	''' indicate the values should not be saved in a field.
	''' </summary>
	''' <param name="createdField">The name of the "created" field.</param>
	''' <param name="modifiedField">The name of the "modified" field.</param>
	''' <param name="userField">The name of the "user" field.</param>
	Public Sub SetTimestampFields(ByVal createdField As String, ByVal modifiedField As String, ByVal userField As String)
		Dim baseClass As IClass = classHelper.Class
		Dim schemaLock As ISchemaLock = CType(baseClass, ISchemaLock)
		Try
			' Get an exclusive lock. We want to do this prior to making any changes
			' to ensure the member variables and extension properties remain synchronized.
			schemaLock.ChangeSchemaLock(esriSchemaLock.esriExclusiveSchemaLock)

			' Set the name member variables.
			createdFieldName = createdField
			modifiedFieldName = modifiedField
			userFieldName = userField

			' Modify the extension properties.
			extensionProperties.SetProperty(Resources.CreatedFieldKey, createdFieldName)
			extensionProperties.SetProperty(Resources.ModifiedFieldKey, modifiedFieldName)
			extensionProperties.SetProperty(Resources.UserFieldKey, userFieldName)

			' Change the properties.
			Dim classSchemaEdit As IClassSchemaEdit2 = CType(baseClass, IClassSchemaEdit2)
			classSchemaEdit.AlterClassExtensionProperties(extensionProperties)
		Catch comExc As Exception
			Throw New Exception(Resources.FailedToSavePropertiesMsg, comExc)
		Finally
			schemaLock.ChangeSchemaLock(esriSchemaLock.esriSharedSchemaLock)
		End Try
	End Sub

	''' <summary>
	''' The field storing the creation date of features.
	''' </summary>
	Public ReadOnly Property CreatedField() As String
		Get
			CreatedField = createdFieldName
		End Get
	End Property

	''' <summary>
	''' The field storing the modification date of features.
	''' </summary>
	Public ReadOnly Property ModifiedField() As String
		Get
			ModifiedField = modifiedFieldName
		End Get
	End Property

	''' <summary>
	''' The field storing the user who created or last modified the feature.
	''' </summary>
	Public ReadOnly Property UserField() As String
		Get
			UserField = userFieldName
		End Get
	End Property
#End Region

#Region "Private Methods"
	''' <summary>
	''' This method should be called the first time the extension is initialized, when the
	''' extension properties are null. This will create a new set of properties with the default
	''' field names.
	''' </summary>
	Private Sub InitNewExtension()
		' First time the extension has been run, initialize the extension properties.
		extensionProperties = New PropertySetClass()
		extensionProperties.SetProperty(Resources.CreatedFieldKey, createdFieldName)
		extensionProperties.SetProperty(Resources.ModifiedFieldKey, modifiedFieldName)
		extensionProperties.SetProperty(Resources.UserFieldKey, userFieldName)

		' Store the properties.
		Dim baseClass As IClass = classHelper.Class
		Dim classSchemaEdit As IClassSchemaEdit2 = CType(baseClass, IClassSchemaEdit2)
		classSchemaEdit.AlterClassExtensionProperties(extensionProperties)
	End Sub

	''' <summary>
	''' Gets the name of the extension's user. For local geodatabases, this is the username as known
	''' by the operating system (in a domain\username format). For remote geodatabases, the
	''' IDatabaseConnectionInfo interface is utilized.
	''' </summary>
	''' <returns>The name of the current user.</returns>
	Private Function GetCurrentUser() As String
		' Get the base class' workspace.
		Dim baseClass As IClass = classHelper.Class
		Dim dataset As IDataset = CType(baseClass, IDataset)
		Dim workspace As IWorkspace = dataset.Workspace

		' If supported, use the IDatabaseConnectionInfo interface to get the username.
		Dim databaseConnectionInfo As IDatabaseConnectionInfo = TryCast(workspace, IDatabaseConnectionInfo)
		If Not databaseConnectionInfo Is Nothing Then
			Dim connectedUser As String = databaseConnectionInfo.ConnectedUser

			' If the username is longer than the user field allows, shorten it.
			If connectedUser.Length > userFieldLength Then
				connectedUser = connectedUser.Substring(0, userFieldLength)
			End If

			Return connectedUser
		End If

		' Get the current Windows user.
		Dim userDomain As String = Environment.UserDomainName
		Dim userName As String = Environment.UserName
		Dim qualifiedUserName As String = String.Format("{0}\{1}", userDomain, userName)

		' If the user name is longer than the user field allows, shorten it.
		If (qualifiedUserName.Length > userFieldLength) Then
			qualifiedUserName = qualifiedUserName.Substring(0, userFieldLength)
		End If

		GetCurrentUser = qualifiedUserName
	End Function

	''' <summary>
	''' Finds the positions of the created, modified and user fields, and verifies that
	''' the specified field has the correct data type.
	''' </summary>
	Private Sub SetFieldIndexes()
		' Get the base class from the class helper.
		Dim baseClass As IClass = classHelper.Class

		' Find the indexes of the fields.
		createdFieldIndex = baseClass.FindField(createdFieldName)
		modifiedFieldIndex = baseClass.FindField(modifiedFieldName)
		userFieldIndex = baseClass.FindField(userFieldName)

		' Verify that the field data types are correct.
		Dim fields As IFields = baseClass.Fields

		If createdFieldIndex <> -1 Then
			Dim createdField As IField = fields.Field(createdFieldIndex)

			' If the "created" field is not a date field, do not use it.
			If createdField.Type <> esriFieldType.esriFieldTypeDate Then
				createdFieldIndex = -1
			End If
		End If

		If modifiedFieldIndex <> -1 Then
			Dim modifiedField As IField = fields.Field(modifiedFieldIndex)

			' If the "modified" field is not a date field, do not use it.
			If modifiedField.Type <> esriFieldType.esriFieldTypeDate Then
				modifiedFieldIndex = -1
			End If
		End If

		If userFieldIndex <> -1 Then
			Dim userField As IField = fields.Field(userFieldIndex)

			' If the "user" field is not a text field, do not use it.
			If userField.Type <> esriFieldType.esriFieldTypeString Then
				userFieldIndex = -1
			Else
				' Get the length of the text field.
				userFieldLength = userField.Length
			End If
		End If
	End Sub
#End Region

#Region "COM Registration Function(s)"
	<ComRegisterFunction(), ComVisibleAttribute(False)> _
	Public Shared Sub RegisterFunction(ByVal registerType As Type)
		Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
		GeoObjectClassExtensions.Register(regKey)
	End Sub

	<ComUnregisterFunction(), ComVisibleAttribute(False)> _
	Public Shared Sub UnregisterFunction(ByVal registerType As Type)
		Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
		GeoObjectClassExtensions.Unregister(regKey)
	End Sub
#End Region
End Class

