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
    [Guid("2e04e777-0673-4e90-b494-3ad3fefefd01")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("CustomRootObject_CS.clsGxStyleGalleryClass")]
    public class clsGxStyleGalleryClass : ESRI.ArcGIS.Catalog.IGxObject, ESRI.ArcGIS.Catalog.IGxObjectContainer
    {
        #region Member Variable
        private CustomRootObject_CS.clsGxStyleGallery m_pParent;
        private IGxCatalog m_pCatalog;
        private IStyleGalleryClass m_pClass;
        private IGxObjectArray m_pChildren;
        private bool m_childrenLoaded;
        #endregion

        #region Constructors
        public clsGxStyleGalleryClass()
            : base()
        {
            m_pChildren = new GxObjectArray();
            m_childrenLoaded = false;
        }
        public IStyleGalleryClass StyleGalleryClass
        {
            set
            {
                m_pClass = value;
            }
        }
        #endregion

        public void PreviewItem(IStyleGalleryItem pItem, int hDC, tagRECT r)
        {
            //Draw a representation of the item to the given DC.
            try
            {
                m_pClass.Preview(pItem.Item, hDC, ref r);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
            }
        }

        #region IGxObject Implementations
        public void Attach(ESRI.ArcGIS.Catalog.IGxObject Parent, ESRI.ArcGIS.Catalog.IGxCatalog pCatalog)
        {
            m_pParent = (CustomRootObject_CS.clsGxStyleGallery)Parent;
            m_pCatalog = pCatalog;
        }

        public string BaseName
        {
            get
            {
                return m_pClass.Name;
            }
        }

        public string Category
        {
            get
            {
                return "Style Gallery Class";
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
            //It is our responsibility to detach all our children before deleting them.
            //This is to avoid circular referencing problems.
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
                return m_pClass.Name;
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
                return m_pClass.Name;
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

            //Our children are GxContainer objects that represent the actual style items
            //of a certain type.

            IEnumStyleGalleryItem pEnumItems = null;
            pEnumItems = m_pParent.StyleGallery.get_Items(m_pClass.Name, "ESRI.style", "");

            IStyleGalleryItem pItem = null;
            pItem = pEnumItems.Next();
            while (pItem != null)
            {
                clsGxStyleGalleryItem pGxItem = null;
                pGxItem = new clsGxStyleGalleryItem();
                pGxItem.StyleGalleryItem = pItem;

                IGxObject pGxObject = null;
                pGxObject = pGxItem;
                pGxObject.Attach(this, m_pCatalog);

                m_pChildren.Insert(-1, pGxObject);
                pItem = pEnumItems.Next();
            }
            m_childrenLoaded = true;
        }
    }
}
