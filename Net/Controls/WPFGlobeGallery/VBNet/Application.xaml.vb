Imports Microsoft.VisualBasic
Imports System
Imports System.Windows
Imports System.Windows.Data
Imports System.Xml
Imports System.Configuration
Imports ESRI.ArcGIS.esriSystem

Class Application

 Protected Overrides Sub OnStartUp(ByVal e As System.Windows.StartupEventArgs)

		MyBase.OnStartup(e)

		Dim galleryWindow As Gallery = New Gallery()
		galleryWindow.mapGallery = CType(Me.Resources("Maps"), ObjectDataProvider).Data
		galleryWindow.mapGallery = CType((TryCast(Me.Resources("Maps"), ObjectDataProvider)).Data, MapCollection)
		galleryWindow.mapGallery.Path = data.GetLocalDataPath() & "\GlobeImages"

		'bind to ArcGIS Engine installation
		ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.Engine)

		If (Not InitializeEngineLicense()) Then
            MessageBox.Show("ArcGIS Engine License or Globe Extension could not be initialized.  Closing...")
		End If
 End Sub


 Private Function InitializeEngineLicense() As Boolean
		Dim aoi As AoInitialize = New AoInitializeClass()

		'more license choices could be included here
		Dim productCode As ESRI.ArcGIS.esriSystem.esriLicenseProductCode = ESRI.ArcGIS.esriSystem.esriLicenseProductCode.esriLicenseProductCodeEngine
		Dim extensionCode As esriLicenseExtensionCode = esriLicenseExtensionCode.esriLicenseExtensionCode3DAnalyst

		If aoi.IsProductCodeAvailable(productCode) = ESRI.ArcGIS.esriSystem.esriLicenseStatus.esriLicenseAvailable Then
			If aoi.IsExtensionCodeAvailable(productCode, extensionCode) = ESRI.ArcGIS.esriSystem.esriLicenseStatus.esriLicenseAvailable Then
				aoi.Initialize(productCode)
				aoi.CheckOutExtension(extensionCode)
				Return True
			Else
				Return False
			End If
		End If
	End Function
End Class
