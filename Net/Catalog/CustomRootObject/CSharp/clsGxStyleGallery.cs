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
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Catalog;

namespace CustomRootObject_CS
{
    [Guid("04ac798b-a358-4beb-b656-cd1f3416cb97")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("CustomRootObject2008.clsGxStyleGallery")]
    public class clsGxStyleGallery : ESRI.ArcGIS.Catalog.IGxObject,ESRI.ArcGIS.Catalog.IGxObjectContainer
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
            GxRootObjects.Register(regKey);

        }
        /// <summary>
        /// Required method for ArcGIS Component Category unregistration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryUnregistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            GxRootObjects.Unregister(regKey);

        }

        #endregion
        #endregion

        #region Member Variables
        private IGxObject m_pParent;
        private IGxCatalog m_pCatalog;
        public IStyleGallery m_pGallery;
        private IGxObjectArray m_pChildren;
        private bool m_childrenLoaded;
        #endregion

        #region Constructors
        public clsGxStyleGallery()
            : base()
        {
            m_pChildren = new GxObjectArray();
            m_childrenLoaded = false;
            m_pGallery = new ESRI.ArcGIS.Framework.StyleGallery();
        }
        public IStyleGallery StyleGallery
        {
            get
            {
                return m_pGallery;
            }
        }
        #endregion

        #region IGxObject Implementations
        public void Attach(ESRI.ArcGIS.Catalog.IGxObject Parent, ESRI.ArcGIS.Catalog.IGxCatalog pCatalog)
        {
            m_pParent = Parent;
            m_pCatalog = pCatalog;
        }

        public string BaseName
        {
            get
            {
                return "Style Gallery";
            }
        }

        public string Category
        {
            get
            {
                return "Style Gallery Manager";
            }
        }

        public ESRI.ArcGIS.esriSystem.UID ClassID
        {
            get
            {
                ESRI.ArcGIS.esriSystem.UID pUID = null;
                pUID = new UID();
                pUID.Value = "CustomRootObject_CS.clsGxStyleGallery";
                return pUID;
            }
        }

        public void Detach()
        {
            //It is our responsibility to detach all of our children before deleting
            //them.  This is to avoid circular referencing problems.
            int i = 0;
            int tempFor1 = m_pChildren.Count;
            for (i = 0; i <= tempFor1; i++)
            {
                m_pChildren.Item(i).Detach();
            }
            m_pParent = null;
            m_pCatalog = null;
        }

        public string FullName
        {
            get
            {
                return "Style Gallery";
            }
        }

        public ESRI.ArcGIS.esriSystem.IName InternalObjectName
        {
            get
            {
                return null;
            }
        }

        public bool IsValid
        {
            get
            {
                return true;
            }
        }

        public string Name
        {
            get
            {
                return "Style Gallery";
            }
        }

        public ESRI.ArcGIS.Catalog.IGxObject Parent
        {
            get
            {
                return m_pParent;
            }
        }

        public void Refresh()
        {
            //Unload and reload the children.
            m_pChildren.Empty();
            m_childrenLoaded = false;
            LoadChildren();
        }
        #endregion

        #region IGxObjectContainer Implementations
        public ESRI.ArcGIS.Catalog.IGxObject AddChild(ESRI.ArcGIS.Catalog.IGxObject child)
        {
            return null;
        }

        public bool AreChildrenViewable
        {
            get
            {
                return true;
            }
        }

        public ESRI.ArcGIS.Catalog.IEnumGxObject Children
        {
            get
            {
                LoadChildren();
                return (IEnumGxObject)m_pChildren;
            }
        }

        public void DeleteChild(ESRI.ArcGIS.Catalog.IGxObject child)
        {
            // TODO: Add clsGxStyleGallery.DeleteChild implementation
        }

        public bool HasChildren
        {
            get
            {
                return true;
            }
        }
        #endregion

        private void LoadChildren()
        {
            if (m_childrenLoaded)
                return;

            //Our children are GxContainer objects that represent class folders
            //for all the different types of styles.  Loop over each of these
            //types, and create a clsGxStyleGalleryClass object for it, and attach it to the
            //tree correctly.
            int i = 0;
            int tempFor1 = m_pGallery.ClassCount;
            for (i = 0; i < tempFor1; i++)
            {
                CustomRootObject_CS.clsGxStyleGalleryClass pGxClass = null;
                pGxClass = new CustomRootObject_CS.clsGxStyleGalleryClass();
                pGxClass.StyleGalleryClass = (IStyleGalleryClass)m_pGallery.get_Class(i);

                IGxObject pGxObject = null;
                pGxObject = pGxClass;
                pGxObject.Attach(this, m_pCatalog);
                m_pChildren.Insert(-1, pGxObject);
            }
            m_childrenLoaded = true;
        }

    }
}
