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
Imports System.Collections.Generic
Imports System.Text
Imports ESRI.ArcGIS.esriSystem

Friend Class Program
 Private Shared m_AOLicenseInitializer As LicenseInitializer = New LicenseInitializer()

	<STAThread()> _
	Shared Sub Main(ByVal args As String())
	'ESRI License Initializer generated code.
	m_AOLicenseInitializer.InitializeApplication(New esriLicenseProductCode() {esriLicenseProductCode.esriLicenseProductCodeEngine}, New esriLicenseExtensionCode() {})

	Console.WriteLine("Creating container object")
	'create a new instance of the test object which will internally clone our clonable object
	Dim t As TestClass = New TestClass()
	t.Test()

	Console.WriteLine("Done, hit any key to continue.")
	Console.ReadKey()

	'ESRI License Initializer generated code.
	'Do not make any call to ArcObjects after ShutDownApplication()
	m_AOLicenseInitializer.ShutdownApplication()
 End Sub
End Class
