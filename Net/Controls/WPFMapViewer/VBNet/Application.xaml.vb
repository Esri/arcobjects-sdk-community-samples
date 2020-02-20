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
Imports System.Windows
Imports ESRI.ArcGIS.esriSystem

Class Application

	Protected Overrides Sub OnStartup(ByVal e As System.Windows.StartupEventArgs)
		MyBase.OnStartup(e)

		ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.Engine)
		InitializeEngineLicense()
	End Sub
	

	Public Sub InitializeEngineLicense()

		Dim aoi As AoInitialize = New AoInitializeClass()

		'more license choices could be included here
		Dim productCode As esriLicenseProductCode = esriLicenseProductCode.esriLicenseProductCodeEngine
		If (aoi.IsProductCodeAvailable(productCode) = esriLicenseStatus.esriLicenseAvailable) Then
			aoi.Initialize(productCode)
		End If

	End Sub

End Class
