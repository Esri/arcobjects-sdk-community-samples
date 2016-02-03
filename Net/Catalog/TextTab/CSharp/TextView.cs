using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Catalog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.CATIDs;

namespace TextTab2008_CS
{
    [Guid("67e3135b-116f-4f5b-ba5c-89c7fca09aee")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("TextTab2008_CS.TextView")]
    public class TextView : ESRI.ArcGIS.CatalogUI.IGxView
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
            GxTabViews.Register(regKey);

        }
        /// <summary>
        /// Required method for ArcGIS Component Category unregistration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryUnregistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            GxTabViews.Unregister(regKey);

        }

        #endregion
        #endregion

        #region Member Variables
        private GxSelection m_pSelection;
        private FrmTextView frmTextView = new FrmTextView();
        #endregion

        private void OnSelectionChanged(IGxSelection Selection, ref object initiator)
        {
            //Refresh view
            Refresh();
        }
        #region IGxView Implementations
        public void Activate(ESRI.ArcGIS.CatalogUI.IGxApplication Application, ESRI.ArcGIS.Catalog.IGxCatalog Catalog)
        {
            m_pSelection = (GxSelection) Application.Selection;
            m_pSelection.OnSelectionChanged += new IGxSelectionEvents_OnSelectionChangedEventHandler(OnSelectionChanged);
            Refresh();
        }

        public bool Applies(ESRI.ArcGIS.Catalog.IGxObject Selection)
        {
            //Set applies
            return (Selection != null) & (Selection is IGxTextFile);
        }

        public ESRI.ArcGIS.esriSystem.UID ClassID
        {
            get
            {
                //Set class ID
                UID pUID = new UID();
                pUID.Value = "TextTab2008_CS.TextView";
                return pUID;
            }
        }

        public void Deactivate()
        {
            //Prevent circular reference
            if (m_pSelection != null)
                m_pSelection = null;
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
                //Set view name
                return "Text";
            }
        }

        public void Refresh()
        {
            IGxSelection pGxSelection = null;
            IGxObject pLocation = null;
            pGxSelection = m_pSelection;
            pLocation = pGxSelection.Location;

            //Clean up
            frmTextView.txtContents.Clear();

            string fname = null;
            fname = pLocation.Name.ToLower();

            if (fname.IndexOf(".txt") != -1)
            {
                try
                {
                    frmTextView.txtContents.Text = System.IO.File.ReadAllText(pLocation.FullName);
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.ToString());
                }
                finally
                {
                    pGxSelection = null;
                    pLocation = null;
                }
            }
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
            // TODO: Add TextView.SystemSettingChanged implementation
        }

        public int hWnd
        {
            get
            {
                int temphWnd = 0;
                try
                {
                    //Set view handle to be the control handle
                    temphWnd = frmTextView.txtContents.Handle.ToInt32();
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.ToString());
                }
                return temphWnd;
            }
        }
        #endregion

    }
}
