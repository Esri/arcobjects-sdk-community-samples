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
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS;


namespace DesktopAutomationCS
{
    public partial class Form1 : Form
    {
        private IApplication m_application;

        //Application removed event
        private IAppROTEvents_Event m_appROTEvent;
        private int m_appHWnd = 0;

        //Retrieve the hWnd of the active popup/modal dialog of an owner window
        [System.Runtime.InteropServices.DllImport("user32")]
        private static extern int GetLastActivePopup(int hwndOwnder);

        public Form1()
        {
          ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.EngineOrDesktop);
            InitializeComponent();

            //Preselect option
            cboApps.SelectedIndex = 0;
        }

        private void btnStartApp_Click(object sender, EventArgs e)
        {
            IDocument doc = null;
            try
            {
                this.Cursor = Cursors.WaitCursor;
                switch (cboApps.SelectedItem.ToString())
                {
                    case "ArcMap":
                        doc = new ESRI.ArcGIS.ArcMapUI.MxDocumentClass();
                        break;
                    case "ArcScene":
                        doc = new ESRI.ArcGIS.ArcScene.SxDocumentClass();
                        break;
                    case "ArcGlobe":
                        doc = new ESRI.ArcGIS.ArcGlobe.GMxDocumentClass();
                        break;
                }
            }
            catch { } //Fail if you haven't installed the target application
            finally
            {
                this.Cursor = Cursors.Default;
            }

            if (doc != null)
            {
                //Advanced (AppROT event): Handle manual shutdown, comment out if not needed
                m_appROTEvent = new AppROTClass();
                m_appROTEvent.AppRemoved += new IAppROTEvents_AppRemovedEventHandler(m_appROTEvent_AppRemoved);

                //Get a reference of the application and make it visible
                m_application = doc.Parent;
                m_application.Visible = true;
                m_appHWnd = m_application.hWnd;

                //Enable/disable controls accordingly
                txtShapeFilePath.Enabled = true;
                btnShutdown.Enabled = true;
                btnDrive.Enabled = ShouldEnableAddLayer;
                cboApps.Enabled = btnStartApp.Enabled = false;
            }
            else
            {
                m_appROTEvent = null;
                m_application = null;

                txtShapeFilePath.Enabled = false;
                btnShutdown.Enabled = btnDrive.Enabled = false;
                cboApps.Enabled = btnStartApp.Enabled = true;
            }
        }

        private void btnShutdown_Click(object sender, EventArgs e)
        {
            if (m_application != null)
            {
                //Try to close any modal dialogs by sending the Escape key
                //It doesn't handle the followings: 
                //- VBA is up and has a modal dialog
                //- Modal dialog doesn't close with the Escape key
                Microsoft.VisualBasic.Interaction.AppActivate(m_application.Caption);
                int nestModalHwnd = 0;
                while ((nestModalHwnd = GetLastActivePopup(m_application.hWnd)) != m_application.hWnd)
                {
                    SendKeys.SendWait("{ESC}");
                }

                //Manage document dirty flag - abandon changes
                IDocumentDirty2 docDirtyFlag = (IDocumentDirty2)m_application.Document;
                docDirtyFlag.SetClean();

                //Stop listening before exiting
                m_appROTEvent.AppRemoved -= new IAppROTEvents_AppRemovedEventHandler(m_appROTEvent_AppRemoved);
                m_appROTEvent = null;

                //Exit
                m_application.Shutdown();
                m_application = null;
                
                //Reset UI for next automation
                txtShapeFilePath.Enabled = false;
                btnShutdown.Enabled = btnDrive.Enabled = false;
                cboApps.Enabled = btnStartApp.Enabled = true;
            }
        }

        private void btnDrive_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                IObjectFactory objFactory = m_application as IObjectFactory;

                //Use reflection to get ClsID of ShapefileWorkspaceFactory
                Type shpWkspFactType = typeof(ShapefileWorkspaceFactoryClass);
                string typeClsID = shpWkspFactType.GUID.ToString("B");

                string shapeFile = System.IO.Path.GetFileNameWithoutExtension(txtShapeFilePath.Text);
                string fileFolder = System.IO.Path.GetDirectoryName(txtShapeFilePath.Text);
                IWorkspaceFactory workspaceFactory = (IWorkspaceFactory)objFactory.Create(typeClsID);
                IFeatureWorkspace featureWorkspace = (IFeatureWorkspace)workspaceFactory.OpenFromFile(fileFolder, 0); //(@"C:\data\test", 0);

                //Create the layer
                IFeatureLayer featureLayer = (IFeatureLayer)objFactory.Create("esriCarto.FeatureLayer");
                featureLayer.FeatureClass = featureWorkspace.OpenFeatureClass(shapeFile); // ("worldgrid");
                featureLayer.Name = featureLayer.FeatureClass.AliasName;

                //Add the layer to document
                IBasicDocument document = (IBasicDocument)m_application.Document;
                document.AddLayer(featureLayer);
                document.UpdateContents();
            }
            catch { } //Or make sure it is a valid shp file first

            this.Cursor = Cursors.Default;
        }

        private void txtShapeFilePath_TextChanged(object sender, EventArgs e)
        {
            btnDrive.Enabled = ShouldEnableAddLayer;
        }

        private bool ShouldEnableAddLayer
        {
            get
            {
                //Only allow .shp file
                if (System.IO.File.Exists(txtShapeFilePath.Text))
                {
                    return (System.IO.Path.GetExtension(txtShapeFilePath.Text).ToLower() == ".shp");
                }
                else
                {
                    return false;
                }
            }
        }

        #region "Handle the case when the application is shutdown by user manually"
        void m_appROTEvent_AppRemoved(AppRef pApp)
        {
            //Application manually shuts down. Stop listening
            if (pApp.hWnd == m_appHWnd) //compare by hwnd
            {
                m_appROTEvent.AppRemoved -= new IAppROTEvents_AppRemovedEventHandler(m_appROTEvent_AppRemoved);
                m_appROTEvent = null;
                m_application = null;
                m_appHWnd = 0;

                //Reset UI has to be in the form UI thread of this application, 
                //not the AppROT thread;
                if (this.InvokeRequired) //i.e. not on the right thread
                {
                    this.BeginInvoke(new IAppROTEvents_AppRemovedEventHandler(AppRemovedResetUI), pApp);
                }
                else
                {
                    AppRemovedResetUI(pApp); //call directly
                }
            }
        }

        void AppRemovedResetUI(AppRef pApp)
        {
            txtShapeFilePath.Enabled = false;
            btnShutdown.Enabled = btnDrive.Enabled = false;
            cboApps.Enabled = btnStartApp.Enabled = true;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Clean up
            if (m_appROTEvent != null)
            {
                m_appROTEvent.AppRemoved -= new IAppROTEvents_AppRemovedEventHandler(m_appROTEvent_AppRemoved);
                m_appROTEvent = null;
            }
        }
        #endregion
    }
}