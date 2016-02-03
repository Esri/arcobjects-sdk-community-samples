using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.Catalog;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Display;

namespace CustomRootObject_CS
{
    [Guid("8350163c-3a94-43c6-bb28-3941c424991d")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("CustomRootObject_CS.clsGxStyleGalleryItem")]
    public class clsGxStyleGalleryItem : ESRI.ArcGIS.Catalog.IGxObject
    {
        #region Member Variables
        private clsGxStyleGalleryClass m_pParent;
        private IGxCatalog m_pCatalog;
        private IStyleGalleryItem m_pItem;
        #endregion

        #region Constructors
        public clsGxStyleGalleryItem()
            : base()
        {
        }

        public IStyleGalleryItem StyleGalleryItem
        {
            set
            {
                m_pItem = value;
            }
        }
        #endregion

        public void PreviewItem(int hDC, tagRECT r)
        {
            m_pParent.PreviewItem(m_pItem, hDC, r);
        }

        #region IGxObject Implementations
        public void Attach(ESRI.ArcGIS.Catalog.IGxObject Parent, ESRI.ArcGIS.Catalog.IGxCatalog pCatalog)
        {
            m_pParent = (clsGxStyleGalleryClass)Parent;
            m_pCatalog = pCatalog;
        }

        public string BaseName
        {
            get
            {
                return m_pItem.Name;
            }
        }

        public string Category
        {
            get
            {
                return m_pItem.Category;
            }
        }

        public ESRI.ArcGIS.esriSystem.UID ClassID
        {
            get
            {
                return null;
            }
        }

        public void Detach()
        {
            m_pParent = null;
            m_pCatalog = null;
        }

        public string FullName
        {
            get
            {
                return m_pItem.Name;
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
                return m_pItem.Name;
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
            // TODO: Add clsGxStyleGalleryItem.Refresh implementation
        }
        #endregion
    }
}
