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
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.ADF.CATIDs;

namespace ESRIWebSitesCS
{
    [Guid("ef536813-36f3-4a1b-8bdc-055f8e445330")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("ESRIWebSitesCS.ESRIWebsitesWindow")]
    public partial class ESRIWebsitesWindow : UserControl, IDockableWindowDef
    {
        private IApplication m_application;

        #region COM Registration Function(s)
        [ComRegisterFunction()]
        [ComVisible(false)]
        static void RegisterFunction(Type registerType)
        {
            // Required for ArcGIS Component Category Registrar support
            ArcGISCategoryRegistration(registerType);
            //
            // TODO: Add any COM registration code here
            //
        }

        [ComUnregisterFunction()]
        [ComVisible(false)]
        static void UnregisterFunction(Type registerType)
        {
            // Required for ArcGIS Component Category Registrar support
            ArcGISCategoryUnregistration(registerType);

            //
            // TODO: Add any COM unregistration code here
            //
        }

        #region ArcGIS Component Category Registrar generated code
        /// <summary>
        /// Required method for ArcGIS Component Category registration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryRegistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            MxDockableWindows.Register(regKey);
            GxDockableWindows.Register(regKey);
            SxDockableWindows.Register(regKey);
            GMxDockableWindows.Register(regKey);

        }
        /// <summary>
        /// Required method for ArcGIS Component Category unregistration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryUnregistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            MxDockableWindows.Unregister(regKey);
            GxDockableWindows.Unregister(regKey);
            SxDockableWindows.Unregister(regKey);
            GMxDockableWindows.Unregister(regKey);

        }

        #endregion
        #endregion

        public ESRIWebsitesWindow()
        {
            InitializeComponent();

            //Delay other setup until IDockableWindowDef.OnCreate
        }

        #region IDockableWindowDef Members

        string IDockableWindowDef.Caption { get { return "Useful ESRI URLs (C#)"; } }
        int IDockableWindowDef.ChildHWND { get { return this.Handle.ToInt32(); } }
        string IDockableWindowDef.Name { get { return "CSNETSamples_ESRIWebsitesWindow"; } }

        void IDockableWindowDef.OnCreate(object hook)
        {
            m_application = hook as IApplication;

            #region setup URLs to the combo
            NameURLPair[] urls = new NameURLPair[9];
            urls[0] = new NameURLPair();
            urls[0].Name = "ESRI";
            urls[0].URL = "http://www.esri.com";

            urls[1] = new NameURLPair();
            urls[1].Name = "ESRI User Conference";
            urls[1].URL = "http://www.esri.com/events/uc/index.html";

            urls[2] = new NameURLPair();
            urls[2].Name = "ESRI User Conference Blog";
            urls[2].URL = "http://blogs.esri.com/roller/page/ucblog";

            urls[3] = new NameURLPair();
            urls[3].Name = "ESRI Support";
            urls[3].URL = "http://support.esri.com/";

            urls[4] = new NameURLPair();
            urls[4].Name = "ESRI Discussion Forum";
            urls[4].URL =  "http://support.esri.com/index.cfm?fa=forums.gateway";

            urls[5] = new NameURLPair();
            urls[5].Name = "ESRI Developer Network (EDN)";
            urls[5].URL = "http://edn.esri.com";

            urls[6] = new NameURLPair();
            urls[6].Name = "Current ArcObject Library";
            urls[6].URL = "http://edndoc.esri.com/arcobjects/9.1/default.asp";

            urls[7] = new NameURLPair();
            urls[7].Name = "ArcGIS 9.1 Desktop Help";
            urls[7].URL = "http://webhelp.esri.com/arcgisdesktop/9.1/";

            urls[8] = new NameURLPair();
            urls[8].Name = "ArcGIS 9.2 Desktop Help";
            urls[8].URL = "http://webhelp.esri.com/arcgisdesktop/9.2/index.cfm";

            cboURLs.DisplayMember = "Name";
            cboURLs.ValueMember = "URL";

            cboURLs.DataSource = urls;
            #endregion
        }

        void IDockableWindowDef.OnDestroy()
        {
            this.Dispose(); //Call dispose
        }

        object IDockableWindowDef.UserData { get { return null; } }
        #endregion

        private void cboURLs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboURLs.SelectedIndex > -1)
            {
                string url = cboURLs.SelectedValue.ToString();
                webBrowser1.Navigate(url);
            }
        }

        private class NameURLPair
        {
            private string m_name;
            private string m_url;
            public NameURLPair() { }
            public string Name
            {
                get { return m_name; }
                set { m_name = value; }
            }
            public string URL
            {
                get { return m_url; }
                set { m_url = value; }
            }
        }
    }
}
