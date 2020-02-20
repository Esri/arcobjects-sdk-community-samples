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
Imports System.Windows.Forms
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Location
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Geometry

Namespace FindAddress
	Partial Public Class AddressForm
		Inherits Form
		Private m_license As AoInitialize = Nothing

		Public Sub New()
			GetLicense()

			InitializeComponent()

			ReturnLicence()
		End Sub

		Private Sub StateTextBox_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles StateTextBox.KeyDown
			' pressing "enter" should do the same as clicking the button for locating
			If e.KeyValue = 13 Then
				FindButton_Click(Me, New System.EventArgs())
			End If
		End Sub

		Private Sub ZipTextBox_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles ZipTextBox.KeyDown
			' pressing "enter" should do the same as clicking the button for locating
			If e.KeyValue = 13 Then
				FindButton_Click(Me, New System.EventArgs())
			End If
		End Sub

		Private Sub FindButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles FindButton.Click
      GeocodeAddress()
		End Sub

    Private Sub GeocodeAddress()
      ' Get the locator
      Dim obj As System.Object = Activator.CreateInstance(Type.GetTypeFromProgID("esriLocation.LocatorManager"))
      Dim locatorManager As ILocatorManager2 = TryCast(obj, ILocatorManager2)
      Dim locatorWorkspace As ILocatorWorkspace = locatorManager.GetLocatorWorkspaceFromPath("C:\locators")
      Dim locator As ILocator = locatorWorkspace.GetLocator("California_city_state_zip")

      ' Set up the address properties
      Dim addressInputs As IAddressInputs = TryCast(locator, IAddressInputs)
      Dim addressFields As IFields = addressInputs.AddressFields
      Dim addressProperties As IPropertySet = New PropertySetClass()
      addressProperties.SetProperty(addressFields.Field(0).Name, Me.AddressTextBox.Text)
      addressProperties.SetProperty(addressFields.Field(1).Name, Me.CityTextBox.Text)
      addressProperties.SetProperty(addressFields.Field(2).Name, Me.StateTextBox.Text)
      addressProperties.SetProperty(addressFields.Field(3).Name, Me.ZipTextBox.Text)

      ' Match the Address
      Dim addressGeocoding As IAddressGeocoding = TryCast(locator, IAddressGeocoding)
      Dim resultSet As IPropertySet = addressGeocoding.MatchAddress(addressProperties)

      ' Print out the results
      Dim names, values As Object
      resultSet.GetAllProperties(names, values)
      Dim namesArray() As String = TryCast(names, String())
      Dim valuesArray() As Object = TryCast(values, Object())
      Dim length As Integer = namesArray.Length
      Dim point As IPoint = Nothing
      For i As Integer = 0 To length - 1
        If namesArray(i) <> "Shape" Then
          Me.ResultsTextBox.Text += namesArray(i) & ": " & valuesArray(i).ToString() & Constants.vbLf
        Else
          If point IsNot Nothing AndAlso (Not point.IsEmpty) Then
            point = TryCast(valuesArray(i), IPoint)
            Me.ResultsTextBox.Text &= "X: " & point.X + Constants.vbLf
            Me.ResultsTextBox.Text &= "Y: " & point.Y + Constants.vbLf
          End If
        End If
      Next i

      Me.ResultsTextBox.Text += Constants.vbLf
    End Sub

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
