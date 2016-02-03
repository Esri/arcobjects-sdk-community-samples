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
