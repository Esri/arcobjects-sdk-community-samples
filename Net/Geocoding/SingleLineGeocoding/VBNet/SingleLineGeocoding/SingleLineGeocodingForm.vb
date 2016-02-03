Imports Microsoft.VisualBasic
Imports System
Imports System.Windows.Forms
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Location
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Geometry

Namespace SingleLineGeocoding
	Public Partial Class SingleLineGeocodingForm : Inherits Form
		Private m_license As AoInitialize = Nothing
		Private m_locator As ILocator = Nothing
		Private m_addressFields As String()
		Private m_orgAddrLabel As String = "Address"

		Public Sub New()
			GetLicense()

			InitializeComponent()

			ReturnLicence()
		End Sub

		Private Sub GeocodeAddress(ByVal addressProperties As IPropertySet)
			' Match the Address
			Dim addressGeocoding As IAddressGeocoding = TryCast(m_locator, IAddressGeocoding)
			Dim resultSet As IPropertySet = addressGeocoding.MatchAddress(addressProperties)

			' Print out the results
			Dim names, values As Object
			resultSet.GetAllProperties(names, values)
			Dim namesArray As String() = TryCast(names, String())
			Dim valuesArray As Object() = TryCast(values, Object())
			Dim length As Integer = namesArray.Length
			Dim point As IPoint = Nothing
			Dim i As Integer = 0
			Do While i < length
				If namesArray(i) <> "Shape" Then
					Me.ResultsTextBox.Text += namesArray(i) & ": " & valuesArray(i).ToString() & Constants.vbLf
				Else
					If Not point Is Nothing AndAlso (Not point.IsEmpty) Then
						point = TryCast(valuesArray(i), IPoint)
						Me.ResultsTextBox.Text &= "X: " & point.X + Constants.vbLf
						Me.ResultsTextBox.Text &= "Y: " & point.Y + Constants.vbLf
					End If
				End If
				i += 1
			Loop

			Me.ResultsTextBox.Text += Constants.vbLf
		End Sub

		Private Sub locatorButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles locatorButton.Click
			addressLabel.Text = m_orgAddrLabel

			Dim result As DialogResult = openFileDialog.ShowDialog()
			If result = System.Windows.Forms.DialogResult.OK Then
				Dim locatorPath As String = openFileDialog.FileName
				locatorPath = locatorPath.Substring(0, locatorPath.LastIndexOf("."c))
				If Not locatorPath Is Nothing AndAlso locatorPath <> "" Then
					locatorTextBox.Text = locatorPath
					addressTextBox.Enabled = True

					' Open the workspace
					Dim workspaceName As String = locatorPath.Substring(0, locatorPath.LastIndexOf("\"))
					Dim locatorName As String = locatorPath.Substring(locatorPath.LastIndexOf("\") + 1)

					' Get the locator
					Dim obj As System.Object = Activator.CreateInstance(Type.GetTypeFromProgID("esriLocation.LocatorManager"))
					Dim locatorManager As ILocatorManager2 = TryCast(obj, ILocatorManager2)
          Dim locatorWorkspace As ILocatorWorkspace = locatorManager.GetLocatorWorkspaceFromPath(workspaceName)
					m_locator = locatorWorkspace.GetLocator(locatorName)

          m_addressFields = get_AddressFields()
					addressLabel.Text &= " (" & String.Join(",", m_addressFields) & ")"
				End If
			End If
		End Sub

		Private Sub findButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles findButton.Click
			Dim addressValues As String() = addressTextBox.Text.Split(","c)
			If addressValues.Length = m_addressFields.Length Then
				Dim addressProperties As IPropertySet = createAddressProperties(m_addressFields, addressValues)
				GeocodeAddress(addressProperties)
			ElseIf m_addressFields.Length = 1 Then
				Dim addressProperties As IPropertySet = createAddressProperties(m_addressFields, addressValues)
				GeocodeAddress(addressProperties)
			Else
				MessageBox.Show("Your address needs a comma between each expected address field or just commas to delimit those fields ", "Address Input Error")
			End If
		End Sub

		Private Sub addressTextBox_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles addressTextBox.KeyDown
			' pressing "enter" should do the same as clicking the button for locating
			If e.KeyValue = 13 Then
				findButton_Click(Me, New System.EventArgs())
			End If
		End Sub

		''' <summary>
		''' Get the address fields for the locator
		''' </summary>
		''' <param name="locator"></param>
		''' <returns>A String array of address fields</returns>
    Private Function get_AddressFields() As String()
      Dim singleLineInput As ISingleLineAddressInput = TryCast(m_locator, ISingleLineAddressInput)
      Dim addressInputs As IAddressInputs = Nothing
      Dim fields As String()
      If Not singleLineInput Is Nothing Then
        Dim singleField As IField = singleLineInput.SingleLineAddressField
        fields = New String() {singleField.Name}
      Else
        addressInputs = TryCast(m_locator, IAddressInputs)
        Dim multiFields As IFields = addressInputs.AddressFields
        Dim fieldCount As Integer = multiFields.FieldCount
        fields = New String(fieldCount - 1) {}
        Dim i As Integer = 0
        Do While i < fieldCount
          fields(i) = multiFields.Field(i).Name()
          i += 1
        Loop
      End If
      Return fields
    End Function

		''' <summary>
		''' Create a propertySet of address fields and values
		''' </summary>
		''' <param name="addressFields"></param>
		''' <param name="addressValues"></param>
		''' <returns>A propertySet that contains address fields and address values that correspond to the fields.</returns>
		Private Function createAddressProperties(ByVal addressFields As String(), ByVal addressValues As String()) As IPropertySet
			Dim fieldCount As Integer = addressFields.Length
			If fieldCount > 1 AndAlso fieldCount <> addressValues.Length Then
				Throw New Exception("There must be the same amount of address fields as address values. ")
			End If

			Dim propertySet As IPropertySet = New PropertySetClass()
			Dim i As Integer = 0
			Do While i < fieldCount
				propertySet.SetProperty(addressFields(i), addressValues(i))
				i += 1
			Loop
			Return propertySet
		End Function

		Private Sub GetLicense()
			If (Not ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.Desktop)) Then
				Throw New Exception("Could not set version. ")
			End If

			m_license = New AoInitializeClass()
			m_license.Initialize(esriLicenseProductCode.esriLicenseProductCodeStandard)
		End Sub

		Private Sub ReturnLicence()
			m_license.Shutdown()
		End Sub
	End Class
End Namespace
