Imports System
Imports System.Collections.Generic
Imports System.Runtime.InteropServices
Imports System.Windows.Forms
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.Geodatabase
Imports Timestamper.My.Resources

''' <summary>
''' A property page for object classes extended with the TimestampClassExtension.
''' </summary>
<Guid("2de16f54-8cd0-495e-a2a5-86319ef5d14c")> _
<ClassInterface(ClassInterfaceType.None)> _
<ProgId("Timestamper.TimestampPropertyPage")> _
<ComVisible(True)> _
Public Class TimestampPropertyPage
	Implements IComPropertyPage

#Region "Member Variables"
	''' <summary>
	''' Indicates whether the combo boxes have been changed.
	''' </summary>
	Private dirtyFlag As Boolean = False

	''' <summary>
	''' The dialog box containing the page.
	''' </summary>
	Private comPropertyPageSite As IComPropertyPageSite = Nothing

	''' <summary>
	''' The extension the property page refers to.
	''' </summary>
	Private timestampClassExtension As TimestampClassExtension = Nothing

	''' <summary>
	''' The class the extension is associated with.
	''' </summary>
	Private objectClass As IObjectClass = Nothing
#End Region

#Region "Constructors"
	''' <summary>
	''' The default constructor.
	''' </summary>
	Public Sub New()

		' This call is required by the Windows Form Designer.
		InitializeComponent()

		' Set all combo boxes to "Not Used" by default.
		cmbCreatedField.SelectedIndex = 0
		cmbModifiedField.SelectedIndex = 0
		cmbUserField.SelectedIndex = 0

	End Sub
#End Region

#Region "Private Methods"
	''' <summary>
	''' Fired by a change to the combo boxes.
	''' </summary>
	''' <param name="eventSource">The source of the event.</param>
	''' <param name="arguments">Event arguments.</param>
	Private Sub SetDirty(ByVal eventSource As Object, ByVal arguments As EventArgs)
		dirtyFlag = True

		If Not comPropertyPageSite Is Nothing And Me.Visible Then
			comPropertyPageSite.PageChanged()
		End If
	End Sub

	''' <summary>
	''' Load valid date and text fields from the class.
	''' </summary>
	Private Sub LoadValidFields()
		' Create lists for storing Date and Text fields.
		Dim dateFieldNames As List(Of String) = New List(Of String)
		Dim textFieldNames As List(Of String) = New List(Of String)

		' Iterate through the class' fields.
		Dim fields As IFields = objectClass.Fields
		For i As Integer = 0 To fields.FieldCount - 1
			Dim field As IField = fields.Field(i)

			If field.Type = esriFieldType.esriFieldTypeDate Then
				dateFieldNames.Add(field.Name)
			End If

			If field.Type = esriFieldType.esriFieldTypeString Then
				textFieldNames.Add(field.Name)
			End If
		Next

		' Add the valid fields to each combo box.
		For Each dateFieldName As String In dateFieldNames
			cmbCreatedField.Items.Add(dateFieldName)
			cmbModifiedField.Items.Add(dateFieldName)
		Next
		For Each textFieldName As String In textFieldNames
			cmbUserField.Items.Add(textFieldName)
		Next
	End Sub
#End Region

#Region "IComPropertyPage Members"
	''' <summary>
	''' Occurs on page creation.
	''' </summary>
	''' <returns>The handle of the page.</returns>
	Public Function Activate() As Integer Implements IComPropertyPage.Activate
		If Not timestampClassExtension Is Nothing Then
			' Load the potential date/text fields from the class.
			LoadValidFields()

			' Set the combo boxes selected indexes to the correct positions.
			If cmbCreatedField.Items.Contains(timestampClassExtension.CreatedField) Then
				cmbCreatedField.SelectedItem = timestampClassExtension.CreatedField
			End If

			If cmbModifiedField.Items.Contains(timestampClassExtension.ModifiedField) Then
				cmbModifiedField.SelectedItem = timestampClassExtension.ModifiedField
			End If

			If cmbUserField.Items.Contains(timestampClassExtension.UserField) Then
				cmbUserField.SelectedItem = timestampClassExtension.UserField
			End If

			' Register the event handler with the combo boxes.
			AddHandler cmbCreatedField.SelectedIndexChanged, AddressOf SetDirty
			AddHandler cmbModifiedField.SelectedIndexChanged, AddressOf SetDirty
			AddHandler cmbUserField.SelectedIndexChanged, AddressOf SetDirty
		End If

		Activate = Me.Handle.ToInt32()
	End Function

	''' <summary>
	''' Indicates if the page applies to the specified objects.
	''' Do not hold on to the objects here.
	''' </summary>
	Public Function Applies(ByVal objects As ISet) As Boolean Implements IComPropertyPage.Applies
		' Should only apply if a single class is selected.
		If (objects Is Nothing Or objects.Count <> 1) Then
			Return False
		End If

		' Check whether the provided object is an object class with the TimestampClassExtension.
		Dim isApplicable As Boolean = False
		objects.Reset()
		Dim providedObject As Object = objects.Next()
		Dim objectClass As IObjectClass = TryCast(providedObject, IObjectClass)
		If Not objectClass Is Nothing Then
			' Get the object class' extension.
			Dim classExtension As Object = objectClass.Extension
			If TypeOf classExtension Is TimestampClassExtension Then
				isApplicable = True
			End If
		End If

		Applies = isApplicable
	End Function

	''' <summary>
	''' Applies any changes to the class extension.
	''' </summary>
	Public Sub Apply() Implements IComPropertyPage.Apply
		If dirtyFlag Then
			Try
				If Not timestampClassExtension Is Nothing Then
					' Get the field names from the combo boxes.
					Dim createdField As String = CType(cmbCreatedField.SelectedItem, String)
					Dim modifiedField As String = CType(cmbModifiedField.SelectedItem, String)
					Dim userField As String = CType(cmbUserField.SelectedItem, String)

					' Check if any of the fields are set to "Not Used".
					If cmbCreatedField.SelectedIndex = 0 Then
						createdField = ""
					End If

					If cmbModifiedField.SelectedIndex = 0 Then
						modifiedField = ""
					End If

					If cmbUserField.SelectedIndex = 0 Then
						userField = ""
					End If

					' Set the field names on the extension.
					timestampClassExtension.SetTimestampFields(createdField, modifiedField, userField)
				End If
			Catch exc As Exception
				MessageBox.Show(String.Format("{0}{1}{2}", Resources.PropertyPageApplyErrorMsg, _
				 Environment.NewLine, exc.Message))
			End Try
		End If
	End Sub

	''' <summary>
	''' Cancels any changes to the class extension.
	''' </summary>
	Public Sub Cancel() Implements IComPropertyPage.Cancel
		dirtyFlag = False
	End Sub

	''' <summary>
	''' Destroys the page.
	''' </summary>
	Public Sub Deactivate() Implements IComPropertyPage.Deactivate
		Me.Dispose(True)
	End Sub

	''' <summary>
	''' The height of the page in pixels.
	''' </summary>
	Public ReadOnly Property ComPropertyPageHeight() As Integer Implements IComPropertyPage.Height
		Get
			ComPropertyPageHeight = Me.Height
		End Get
	End Property

	''' <summary>
	''' The help context ID for the specified control on the page.
	''' </summary>
	''' <param name="controlID">The control ID.</param>
	''' <returns>In this case, 0, indicating no help context.</returns>
	Public ReadOnly Property HelpContextID(ByVal controlID As Integer) As Integer Implements IComPropertyPage.HelpContextID
		Get
			HelpContextID = 0
		End Get
	End Property

	''' <summary>
	''' The help file for the page (none).
	''' </summary>
	Public ReadOnly Property HelpFile() As String Implements IComPropertyPage.HelpFile
		Get
			HelpFile = String.Empty
		End Get
	End Property

	''' <summary>
	''' Hides the page.
	''' </summary>
	Public Sub ComPropertyPageHide() Implements IComPropertyPage.Hide
		' No need to do anything here.
	End Sub

	''' <summary>
	''' Indicates if any changes have been made to the page.
	''' </summary>
	Public ReadOnly Property IsPageDirty() As Boolean Implements IComPropertyPage.IsPageDirty
		Get
			IsPageDirty = dirtyFlag
		End Get
	End Property

	''' <summary>
	''' The sheet that contains the page.
	''' </summary>
	Public WriteOnly Property PageSite() As IComPropertyPageSite Implements IComPropertyPage.PageSite
		Set(ByVal value As IComPropertyPageSite)
			comPropertyPageSite = value
		End Set
	End Property

	''' <summary>
	''' The page priority.
	''' </summary>
	''' <value>Setting a value has no effect.</value>
	Public Property Priority() As Integer Implements IComPropertyPage.Priority
		Get
			' Low-priority.
			Priority = 0
		End Get
		Set(ByVal value As Integer)
			' Do nothing.
		End Set
	End Property

	''' <summary>
	''' Supplies the page with the object(s) to be edited.
	''' </summary>
	''' <param name="objects">The object(s) this page applies to.</param>
	Public Sub SetObjects(ByVal objects As ISet) Implements IComPropertyPage.SetObjects
		If objects Is Nothing Or objects.Count <> 1 Then
			Exit Sub
		End If

		' Store the provided object class in a member variable.
		objects.Reset()
		Dim providedObject As Object = objects.Next()
		objectClass = TryCast(providedObject, IObjectClass)
		If Not objectClass Is Nothing Then
			' Get the object class' extension.
			timestampClassExtension = TryCast(objectClass.Extension, TimestampClassExtension)
		End If
	End Sub

	''' <summary>
	''' Shows the page.
	''' </summary>
	Public Sub ComPropertyPageShow() Implements IComPropertyPage.Show
		' No need to do anything here.
	End Sub

	''' <summary>
	''' The title of the property page.
	''' </summary>
	''' <value>Setting a value has no effect.</value>
	Public Property Title() As String Implements IComPropertyPage.Title
		Get
			Title = Resources.PropertyPageTitle
		End Get
		Set(ByVal value As String)
			' Do nothing.
		End Set
	End Property

	''' <summary>
	''' The width of the page in pixels.
	''' </summary>
	Public ReadOnly Property ComPropertyPageWidth() As Integer Implements IComPropertyPage.Width
		Get
			ComPropertyPageWidth = Me.Width
		End Get
	End Property
#End Region

#Region "COM Registration Functions"
	<ComRegisterFunction(), ComVisibleAttribute(False)> _
	Public Shared Sub RegisterFunction(ByVal registerType As Type)
		Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
		TablePropertyPages.Register(regKey)
		FeatureClassPropertyPages.Register(regKey)
	End Sub

	<ComUnregisterFunction(), ComVisibleAttribute(False)> _
	Public Shared Sub UnregisterFunction(ByVal registerType As Type)
		Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
		TablePropertyPages.Unregister(regKey)
		FeatureClassPropertyPages.Register(regKey)
	End Sub
#End Region

End Class
