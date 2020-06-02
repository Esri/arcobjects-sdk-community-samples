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
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;

namespace DesktopPropertyPageCS
{
    /// <summary>
    /// Layer property page implementation for ArcMap, ArcScene or ArcGlobe.
    /// </summary>
    [Guid("7b01b172-57e4-4986-9650-d44b799180f3")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("DesktopPropertyPageCS.LayerVisibilityPage")]
    public partial class LayerVisibilityPage : UserControl, IComPropertyPage
    {
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
            LayerPropertyPages.Register(regKey);
            SxLayerPropertyPages.Register(regKey);
            GMxLayerPropertyPages.Register(regKey);
        }
        /// <summary>
        /// Required method for ArcGIS Component Category unregistration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryUnregistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            LayerPropertyPages.Unregister(regKey);
            SxLayerPropertyPages.Unregister(regKey);
            GMxLayerPropertyPages.Unregister(regKey);
        }

        #endregion
        #endregion

        private bool m_dirtyFlag = false;
        private string m_pageTitle;
        private IComPropertyPageSite m_pageSite = null;
        private ILayer m_targetLayer = null;
        private IActiveView m_activeView = null;

        public LayerVisibilityPage()
        {
            InitializeComponent();

            m_pageTitle = "Layer Visibility (C#)";
        }

        /// <summary>
        /// Helper function to set dirty flag whenever changes are made to the page
        /// </summary>
        private void SetPageDirty(bool dirty)
        {
            if (m_dirtyFlag != dirty)
            {
                m_dirtyFlag = dirty;
                if (m_pageSite != null)
                    m_pageSite.PageChanged();
            }
        }

        /// <summary>
        /// Example: update dirty flag when check state of radio button 
        /// (which controls layer visibility) changes
        /// </summary>
        private void radioButtonShow_CheckedChanged(object sender, EventArgs e)
        {
            SetPageDirty(true);
        }

        #region IComPropertyPage Members

        string IComPropertyPage.Title
        {
            get
            {
                return m_pageTitle;
            }
            set
            {
                //TODO: Uncomment if title can be modified
                //m_pageTitle = value;
            }
        }

        int IComPropertyPage.Width
        {
            get { return this.Width; }
        }

        int IComPropertyPage.Height
        {
            get { return this.Height; }
        }

        IComPropertyPageSite IComPropertyPage.PageSite
        {
            set
            {
                m_pageSite = value;
            }
        }

        /// <summary>
        /// Indicates if the page applies to the specified objects
        /// Do not hold on to the objects here.
        /// </summary>
        bool IComPropertyPage.Applies(ESRI.ArcGIS.esriSystem.ISet objects)
        {
            if (objects == null || objects.Count == 0)
                return false;

            bool isEditable = false;
            objects.Reset();
            object testObject;
            while ((testObject = objects.Next()) != null)
            {
                if (testObject is ILayer)
                {
                    isEditable = true;
                    break;
                }
            }

            return isEditable;
        }

        /// <summary>
        /// Supplies the page with the object(s) to be edited
        /// </summary>
        void IComPropertyPage.SetObjects(ESRI.ArcGIS.esriSystem.ISet objects)
        {
            if (objects == null || objects.Count == 0)
                return;

            m_activeView = null;
            m_targetLayer = null;

            objects.Reset();
            object testObject;
            while ((testObject = objects.Next()) != null)
            {
                if (testObject is ILayer)
                    m_targetLayer = testObject as ILayer;
                else if (testObject is IActiveView)
                    m_activeView = testObject as IActiveView;
                //else
                //{
                    //IApplication app = testObject as IApplication  //Use if needed
                //}
            }
        }

        /// <summary>
        /// Indicates if the Apply button should be enabled
        /// </summary>
        bool IComPropertyPage.IsPageDirty
        {
            get { return m_dirtyFlag; }
        }

        int IComPropertyPage.Activate()
        {
            //Set up page UI based on layer visibility
            if (m_targetLayer.Visible)
                radioButtonShow.Checked = true;
            else
                radioButtonHide.Checked = true;

            SetPageDirty(false);
            return this.Handle.ToInt32();
        }

        void IComPropertyPage.Deactivate()
        {
            m_targetLayer = null;
            m_activeView = null;
            this.Dispose(true);
        }

        void IComPropertyPage.Apply()
        {
            if (m_dirtyFlag)
            {
                //Set layer visibility
                m_targetLayer.Visible = radioButtonShow.Checked;

                //Refresh display after changes are made
                if (m_activeView != null)
                {
                    m_activeView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
                    m_activeView.ContentsChanged(); //notify clients listening to active view events, e.g. Source tab in the TOC
                }

                SetPageDirty(false);
            }
        }

        void IComPropertyPage.Cancel()
        {
            if (m_dirtyFlag)
            {
                //Reset UI or any temporary changes made to layer
                radioButtonShow.Checked = m_targetLayer.Visible;

                SetPageDirty(false);
            }
        }

        void IComPropertyPage.Show()
        {
        }

        void IComPropertyPage.Hide()
        {
        }

        string IComPropertyPage.HelpFile
        {
            get { return string.Empty; }
        }

        int IComPropertyPage.get_HelpContextID(int controlID)
        {
            return 0;
        }

        int IComPropertyPage.Priority
        {
            get
            {
                return 0;
            }
            set
            {
            }
        }

        #endregion

    }
}
