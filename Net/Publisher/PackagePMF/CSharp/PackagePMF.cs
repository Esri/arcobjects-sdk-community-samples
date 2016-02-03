using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.Publisher;
using ESRI.ArcGIS.PublisherUI;
using ESRI.ArcGIS.Carto;

namespace PackagePMF2008CS
{
    public class PackagePMF : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public PackagePMF()
        {
        }

        protected override void OnClick()
        {
            //Enable publisher extension
            if (EnablePublisherExtension())
            {
                System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;

                IPMFPackage pmfPackager = new PackagerEngineClass();

                //Create a new directory to store the pmf and data folders of the package:
                Directory.CreateDirectory(@"C:\temp\MyPMFPackage");

                //All vector data will be converted to file geodatabase feature class format.
                //All raster data will be converted to compressed file geodatabase raster format.
                IPropertySet settings = pmfPackager.GetDefaultPackagerSettings();
                settings.SetProperty("Vector Type", esriAPEVectorType.esriAPEVectorTypeFileGDB);
                settings.SetProperty("Raster Type", esriAPERasterType.esriAPERasterTypeFileGDBCompressed);
                settings.SetProperty("Package Directory", @"C:\temp\MyPMFPackage");

                //Specify the name of the pmf to be packaged
                IStringArray strArray = new StrArrayClass();
                strArray.Add(@"C:\PublishedMap.pmf");

                try
                {
                    //Package the pmf with the specified settings
                    pmfPackager.Package(settings, null, strArray);

                    MessageBox.Show("Packaging is complete.", "Packaging Results");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to package the PMF: " + ex.Message);
                }

                System.Windows.Forms.Cursor.Current = Cursors.Default;

            }

            ArcMap.Application.CurrentTool = null;
        }
        protected override void OnUpdate()
        {
            Enabled = ArcMap.Application != null;
        }

        private bool EnablePublisherExtension()
        {

            bool checkedOutOK = false;

            try
            {
                IExtensionManager extMgr = new ExtensionManagerClass();

                IExtensionManagerAdmin extAdmin = (IExtensionManagerAdmin)extMgr;

                UID uid = new UID();
                uid.Value = "esriPublisherUI.Publisher";
                object obj = 0;
                extAdmin.AddExtension(uid, ref obj);

                IExtensionConfig extConfig = (IExtensionConfig)extMgr.FindExtension(uid);

                if ((!(extConfig.State == esriExtensionState.esriESUnavailable)))
                {
                    //This checks on the extension
                    extConfig.State = esriExtensionState.esriESEnabled;
                    checkedOutOK = true;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Publisher extension has failed to check out.", "Error");
            }

            return checkedOutOK;
        } 
    }

}
