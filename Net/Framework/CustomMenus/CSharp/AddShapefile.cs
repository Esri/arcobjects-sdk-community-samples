/*

   Copyright 2019 Esri

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
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;

namespace CustomMenus
{
    /// <summary>
    /// Summary description for AddShapefile.
    /// </summary>
    [Guid("6f8f48fe-bfbc-4b2a-b1e7-25eced548743")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("CustomMenus.AddShapefile")]
    public sealed class AddShapefile : BaseCommand
    {
        #region COM Registration Function(s)
        [ComRegisterFunction()]
        [ComVisible(false)]
        static void RegisterFunction(Type registerType)
        {
            // Required for ArcGIS Component Category Registrar support
            ArcGISCategoryRegistration(registerType);
        }

        [ComUnregisterFunction()]
        [ComVisible(false)]
        static void UnregisterFunction(Type registerType)
        {
            // Required for ArcGIS Component Category Registrar support
            ArcGISCategoryUnregistration(registerType);
        }

        #region ArcGIS Component Category Registrar generated code
        /// <summary>
        /// Required method for ArcGIS Component Category registration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryRegistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            MxCommands.Register(regKey);

        }
        /// <summary>
        /// Required method for ArcGIS Component Category unregistration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryUnregistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            MxCommands.Unregister(regKey);

        }

        #endregion
        #endregion

        private IApplication m_application;

        public AddShapefile()
        {
            base.m_category = "Developer Samples";
            base.m_caption = "Add Shapefile";
            base.m_message = "Adds a shapefile to the focus map";
            base.m_toolTip = "Adds a shapefile";
            base.m_name = "CustomMenus_AddShapefile";

            try
            {
                string bitmapResourceName = GetType().Name + ".bmp";
                base.m_bitmap = new Bitmap(GetType(), bitmapResourceName);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message, "Invalid Bitmap");
            }
        }

        #region Overriden Class Methods

        /// <summary>
        /// Occurs when this command is created
        /// </summary>
        /// <param name="hook">Instance of the application</param>
        public override void OnCreate(object hook)
        {
            if (hook == null)
                return;

            m_application = hook as IApplication;

            //Disable if it is not ArcMap
            if (hook is IMxApplication)
                base.m_enabled = true;
            else
                base.m_enabled = false;
        }

        /// <summary>
        /// Occurs when this command is clicked
        /// </summary>
        public override void OnClick()
        {
            IMxDocument mxDocument = m_application.Document as IMxDocument;
            AddShapefileUsingOpenFileDialog(mxDocument.ActiveView);
        }

        #endregion

        //#### ArcGIS Snippets ####
        
        #region "Add Shapefile Using OpenFileDialog"

        ///<summary>Add a shapefile to the ActiveView using the Windows.Forms.OpenFileDialog control.</summary>
        ///
        ///<param name="activeView">An IActiveView interface</param>
        /// 
        ///<remarks></remarks>
        public void AddShapefileUsingOpenFileDialog(ESRI.ArcGIS.Carto.IActiveView activeView)
        {
          //parameter check
          if (activeView == null)
          {
            return;
          }

          // Use the OpenFileDialog Class to choose which shapefile to load.
          System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog();
          openFileDialog.InitialDirectory = "c:\\";
          openFileDialog.Filter = "Shapefiles (*.shp)|*.shp";
          openFileDialog.FilterIndex = 2;
          openFileDialog.RestoreDirectory = true;
          openFileDialog.Multiselect = false;


          if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
          {
            // The user chose a particular shapefile.

            // The returned string will be the full path, filename and file-extension for the chosen shapefile. Example: "C:\test\cities.shp"
            string shapefileLocation = openFileDialog.FileName;

            if (shapefileLocation != "")
            {
              // Ensure the user chooses a shapefile

              // Create a new ShapefileWorkspaceFactory CoClass to create a new workspace
              ESRI.ArcGIS.Geodatabase.IWorkspaceFactory workspaceFactory = new ESRI.ArcGIS.DataSourcesFile.ShapefileWorkspaceFactory();

              // System.IO.Path.GetDirectoryName(shapefileLocation) returns the directory part of the string. Example: "C:\test\"
              ESRI.ArcGIS.Geodatabase.IFeatureWorkspace featureWorkspace = (ESRI.ArcGIS.Geodatabase.IFeatureWorkspace)workspaceFactory.OpenFromFile(System.IO.Path.GetDirectoryName(shapefileLocation), 0); // Explicit Cast

              // System.IO.Path.GetFileNameWithoutExtension(shapefileLocation) returns the base filename (without extension). Example: "cities"
              ESRI.ArcGIS.Geodatabase.IFeatureClass featureClass = featureWorkspace.OpenFeatureClass(System.IO.Path.GetFileNameWithoutExtension(shapefileLocation));

              ESRI.ArcGIS.Carto.IFeatureLayer featureLayer = new ESRI.ArcGIS.Carto.FeatureLayer();
              featureLayer.FeatureClass = featureClass;
              featureLayer.Name = featureClass.AliasName;
              featureLayer.Visible = true;
              activeView.FocusMap.AddLayer(featureLayer);

              // Zoom the display to the full extent of all layers in the map
              activeView.Extent = activeView.FullExtent;
              activeView.PartialRefresh(ESRI.ArcGIS.Carto.esriViewDrawPhase.esriViewGeography, null, null);
            }
            else
            {
              // The user did not choose a shapefile.
              // Do whatever remedial actions as necessary
              // System.Windows.Forms.MessageBox.Show("No shapefile chosen", "No Choice #1",
              //                                     System.Windows.Forms.MessageBoxButtons.OK,
              //                                     System.Windows.Forms.MessageBoxIcon.Exclamation);
            }
          }
          else
          {
            // The user did not choose a shapefile. They clicked Cancel or closed the dialog by the "X" button.
            // Do whatever remedial actions as necessary.
            // System.Windows.Forms.MessageBox.Show("No shapefile chosen", "No Choice #2",
            //                                      System.Windows.Forms.MessageBoxButtons.OK,
            //                                      System.Windows.Forms.MessageBoxIcon.Exclamation);
          }
        }
        #endregion
    }
}
