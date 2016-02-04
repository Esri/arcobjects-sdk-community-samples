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

namespace PublishMap2008CSharp
{
    public class PublishMap : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public PublishMap()
        {
        }

        protected override void OnClick()
        {
            //Enable publisher extension
            if (EnablePublisherExtension())
            {
                System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;

                IMxDocument mapDoc = ArcMap.Document;

                IPMFPublish3 publishMaps = new PublisherEngineClass();

                //Accept the default settings when the map is published.
                IPropertySet settings = publishMaps.GetDefaultPublisherSettings();              

                IArray orderedMaps = new ArrayClass();
                //Loop over all the maps in the map document and each one to an Array object
                for (int i = 0; i <= mapDoc.Maps.Count - 1; i++)
                {
                    orderedMaps.Add(mapDoc.Maps.get_Item(i));
                }

                try
                {
                    //Create the PMF file using the current settings and the map order specified in the IArray parameter
                    //It will be written to C:\\PublishedMap.pmf and the data will be referenced using relative paths.
                    publishMaps.PublishWithOrder(orderedMaps, mapDoc.PageLayout, mapDoc.ActiveView, settings,
                                                 true, "C:\\PublishedMap.pmf");


                    //Report outcome to the user
                    string mapdocTitle = ((IDocument)ArcMap.Document).Title;
                    string msg = string.Empty;

                    if (orderedMaps.Count == 1)
                    {
                        msg = "The map in " + mapdocTitle + " has been published successfully";
                    }
                    else
                    {
                        msg = "The maps in " + mapdocTitle + " have been published successfully";
                    }

                    MessageBox.Show(msg);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error Publishing Map: " + ex.Message, "Error");
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
