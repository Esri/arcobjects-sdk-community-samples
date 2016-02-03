using ESRI.ArcGIS.Catalog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.CATIDs;

namespace CustomGxFilter_CS
{
    [Guid("be19861c-fd1d-4240-b287-b020a14acb09")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("CustomGxFilter_CS.CustomFilter")]
    public class CustomFilter : ESRI.ArcGIS.Catalog.IGxObjectFilter
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
            GxObjectFilters.Register(regKey);

        }
        /// <summary>
        /// Required method for ArcGIS Component Category unregistration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryUnregistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            GxObjectFilters.Unregister(regKey);

        }

        #endregion
        #endregion

        #region Member Variables
        private IGxObjectFilter m_pBasicFilter;
        #endregion

        public CustomFilter()
        {
            m_pBasicFilter = new GxFilterBasicTypesClass();
        }

        #region "IGxObjectFilter Implementations"
        public bool CanChooseObject(ESRI.ArcGIS.Catalog.IGxObject obj, ref ESRI.ArcGIS.Catalog.esriDoubleClickResult result)
        {
            //Set whether the selected object can be chosen
            bool bCanChoose = false;
            bCanChoose = false;
            if (obj is IGxFile)
            {
                string sExt = null;
                sExt = GetExtension(obj.Name);
                if (sExt.ToLower() == ".py")
                    bCanChoose = true;
            }
            return bCanChoose;
        }

        public bool CanDisplayObject(ESRI.ArcGIS.Catalog.IGxObject obj)
        {
            //Check objects can be displayed
            try
            {
                //Check objects can be displayed
                if (m_pBasicFilter.CanDisplayObject(obj))
                    return true;
                else if (obj is IGxFile)
                {
                    string sExt = null;
                    sExt = GetExtension(obj.Name);
                    if (sExt.ToLower() == ".py")
                        return true;
                }

            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
            }
            return false;
        }

        public bool CanSaveObject(ESRI.ArcGIS.Catalog.IGxObject Location, string newObjectName, ref bool objectAlreadyExists)
        {
            return false;
        }

        public string Description
        {
            get
            {
                //Set filet description
                return "Python file (.py)";
            }
        }

        public string Name
        {
            get
            {
                //Set filter name
                return "Python filter";
            }
        }
        #endregion

        private string GetExtension(string sFileName)
        {
            string tempGetExtension = null;
            //Get extension
            long pExtPos = 0;
            pExtPos = (sFileName.LastIndexOf(".") + 1);
            if (pExtPos > 0)
                tempGetExtension = sFileName.Substring((Int32)pExtPos - 1);
            else
                tempGetExtension = "";
            return tempGetExtension;
        }
    }
}
