using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Catalog;
using ESRI.ArcGIS.esriSystem;

namespace CustomGxObject_CS
{
    [Guid("ec66864f-fb7c-49c0-b491-0e6919bc98b4")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("CustomGxObject_CS.GxPyFactory")]
    public class GxPyFactory : ESRI.ArcGIS.Catalog.IGxObjectFactory
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
            GxObjectFactory.Register(regKey);
        }
        /// <summary>
        /// Required method for ArcGIS Component Category unregistration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryUnregistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            GxObjectFactory.Unregister(regKey);

        }

        #endregion
        #endregion

        #region "Member Variables"
        private IGxCatalog m_catalog = null;
        #endregion

        public GxPyFactory()
		{
			m_catalog = null;
		}

        #region "IGxObjectFactory Implementations"
        public ESRI.ArcGIS.Catalog.IGxCatalog Catalog
        {
            set
            {
                if (value != null)
                {
                    // Store incoming value of Catalog.
                    m_catalog = value;
                }
            }
        }

        public ESRI.ArcGIS.Catalog.IEnumGxObject GetChildren(string parentDir, ESRI.ArcGIS.esriSystem.IFileNames fileNames)
        {
            IGxObjectArray gxChildren = new GxObjectArray();

            if (fileNames != null)
            {

                fileNames.Reset();

                string fileName = fileNames.Next();
                while (fileName != null)
                {
                    if (fileName.Length > 0)
                    {
                        if (!(fileNames.IsDirectory()))
                        {
                            if (fileName.ToUpper().EndsWith(".PY"))
                            {
                                GxPyObject gxChild = new GxPyObject(fileName);
                                gxChildren.Insert(-1, gxChild);
                                gxChild = null;
                                // Remove file name from the list for other GxObjectFactories to search.
                                fileNames.Remove();
                            }
                        }
                    }
                    fileName = fileNames.Next();
                }
            }

            if (gxChildren.Count > 0)
            {
                IEnumGxObject enumChildren = (IEnumGxObject)gxChildren;
                enumChildren.Reset();
                return enumChildren;
            }
            else
                return null;
        }

        public bool HasChildren(string parentDir, ESRI.ArcGIS.esriSystem.IFileNames fileNames)
        {
            if (fileNames != null)
            {
                fileNames.Reset();
                string fileName = fileNames.Next();
                while ((fileName != null) & (fileName.Length > 0))
                {
                    if (fileName.ToUpper().EndsWith(".PY"))
                        return true;
                    fileName = fileNames.Next();
                }
            }
            return false;
        }

        public string Name
        {
            get
            {
                return "Python Files";
            }
        }
        #endregion

    }
}
