using System;
using System.Windows;
using System.Windows.Data;
using System.Xml;
using System.Configuration;
using ESRI.ArcGIS.esriSystem;

namespace GlobeGallery
{
    public partial class app : Application
    {
			protected override void OnStartup (StartupEventArgs e)
			{
				base.OnStartup (e);
		
        GalleryWindow galleryWindow = new GalleryWindow();
				//galleryWindow.Show ();
				galleryWindow.mapGallery = (MapCollection) (this.Resources["Maps"] as ObjectDataProvider).Data;
				galleryWindow.mapGallery.Path = data.GetLocalDataPath () + "\\GlobeImages";
			
				//bind to ArcGIS Engine installation
				ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.Engine);

        if (!InitializeEngineLicense())
        {
           MessageBox.Show("ArcGIS Engine License or Globe Extension could not be initialized.  Closing...");
           galleryWindow.Close();
        }
			}

			private bool InitializeEngineLicense ()
			{
					AoInitialize aoi = new AoInitializeClass ();

					//more license choices could be included here
					esriLicenseProductCode productCode = esriLicenseProductCode.esriLicenseProductCodeEngine;
          esriLicenseExtensionCode extensionCode = esriLicenseExtensionCode.esriLicenseExtensionCode3DAnalyst;

          if (aoi.IsProductCodeAvailable(productCode) == esriLicenseStatus.esriLicenseAvailable  && aoi.IsExtensionCodeAvailable(productCode, extensionCode) == esriLicenseStatus.esriLicenseAvailable)
          {
              aoi.Initialize(productCode);
              aoi.CheckOutExtension(extensionCode);
              return true;
          }
          else
              return false;
			}
    }
}