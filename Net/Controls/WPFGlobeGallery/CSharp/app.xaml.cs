/*

   Copyright 2016 Esri

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.

   See the License for the specific language governing permissions and
   limitations under the License.

*/
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