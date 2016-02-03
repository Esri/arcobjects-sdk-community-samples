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
