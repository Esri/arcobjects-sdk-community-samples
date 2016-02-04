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
using System.Runtime.InteropServices;
using ESRI.ArcGIS.Catalog;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.CatalogUI;

namespace CustomRootObject_CS
{
    [Guid("0ea89e53-2d0c-4b62-9de7-50a43414a92e")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("CustomRootObject_CS.clsGxStyleView")]
    public class clsGxStyleView : ESRI.ArcGIS.CatalogUI.IGxView
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
            GxPreviews.Register(regKey);

        }
        /// <summary>
        /// Required method for ArcGIS Component Category unregistration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryUnregistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            GxPreviews.Unregister(regKey);

        }

        #endregion
        #endregion

        #region Member Variables
        private IGxApplication m_pApp;
        private IGxCatalog m_pCatalog;
        private clsGxStyleGalleryItem m_pItem;
        private FrmGxStyleView frmGxStyleView = new FrmGxStyleView();
        //Subscribe to the events coming from the GxSelection.
        private GxSelection m_pSelection;
        #endregion

        public clsGxStyleView()
            : base()
        {
        }
        public void PreviewItem(long hDC, tagRECT r)
        {
            if (m_pItem == null)
                return;
            m_pItem.PreviewItem((Int32)hDC, r);
        }

        private void m_pSelection_OnSelectionChanged(IGxSelection Selection, ref object initiator)
        {
            Refresh();
            frmGxStyleView.RefreshView();
        }

        #region IGxView Implementations
        public void Activate(ESRI.ArcGIS.CatalogUI.IGxApplication Application, ESRI.ArcGIS.Catalog.IGxCatalog Catalog)
        {
            m_pApp = Application;
            m_pCatalog = Catalog;
            m_pSelection = (GxSelection)Catalog.Selection;
            m_pSelection.OnSelectionChanged += new IGxSelectionEvents_OnSelectionChangedEventHandler(m_pSelection_OnSelectionChanged);
            frmGxStyleView.GxStyleView = this;
            Refresh();
        }

        public bool Applies(ESRI.ArcGIS.Catalog.IGxObject Selection)
        {
            return Selection is clsGxStyleGalleryItem;
        }

        public ESRI.ArcGIS.esriSystem.UID ClassID
        {
            get
            {
                ESRI.ArcGIS.esriSystem.UID pUID = null;
                pUID = new UID();
                pUID.Value = "CustomRootObject_CS.clsGxStyleView";
                return pUID;
            }
        }

        public void Deactivate()
        {
            frmGxStyleView.GxStyleView = null;
            m_pApp = null;
            m_pCatalog = null;
        }

        public ESRI.ArcGIS.esriSystem.UID DefaultToolbarCLSID
        {
            get
            {
                return null;
            }
        }

        public string Name
        {
            get
            {
                return "Style";
            }
        }

        public void Refresh()
        {
            IGxObject pSelection = null;
            pSelection = m_pCatalog.SelectedObject;
            if (pSelection is clsGxStyleGalleryItem)
                m_pItem = (clsGxStyleGalleryItem) pSelection;
            else
                m_pItem = null;
        }

        public bool SupportsTools
        {
            get
            {
                return false; 
            }
        }

        public void SystemSettingChanged(int flag, string section)
        {
            // TODO: Add clsGxStyleView.SystemSettingChanged implementation
        }

        public int hWnd
        {
            get
            {
                return (Int32) frmGxStyleView.picStylePreview.Handle;
            }
        }
        #endregion

    }
}
