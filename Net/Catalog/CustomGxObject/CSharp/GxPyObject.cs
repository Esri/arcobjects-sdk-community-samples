using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Catalog;
using ESRI.ArcGIS.CatalogUI;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using System.IO;

namespace CustomGxObject_CS
{
    [Guid("65590ce5-9a58-4384-a85b-9efccbc0b727")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("CustomGxObject_CS.GxPyObject")]
    public class GxPyObject : ESRI.ArcGIS.Catalog.IGxObject, ESRI.ArcGIS.Catalog.IGxObjectUI, ESRI.ArcGIS.Catalog.IGxObjectEdit, ESRI.ArcGIS.Catalog.IGxObjectProperties
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

        [DllImport("gdi32.dll")]
        static extern bool DeleteObject(IntPtr hObject);

        #region Member Variables
		private IGxObject m_gxParent = null;
		private IGxCatalog m_gxCatalog = null;
		
		private string[] m_names = new string[3]; //0:FullName; 1:Name; 2:BaseName
		private string m_sCategory = "PY File";

		private System.Drawing.Bitmap[] m_bitmaps = new System.Drawing.Bitmap[2];
		private IntPtr[] m_hBitmap  = new IntPtr[2];

		#endregion

        #region Constructor/Destructor code 
		public GxPyObject()
		{
			this.SetBitmaps();
		}
		public GxPyObject(string name)
		{
			this.SetBitmaps();
			this.SetNames(name);
		}
		
		private void SetBitmaps()
		{
			try
			{
				// In the constructor, initialize the icons to use.
                m_bitmaps[0] = new System.Drawing.Bitmap(GetType().Assembly.GetManifestResourceStream("CustomGxObject_CS.LargeIcon.bmp"));
                m_bitmaps[1] = new System.Drawing.Bitmap(GetType().Assembly.GetManifestResourceStream("CustomGxObject_CS.SmallIcon.bmp"));
				if (m_bitmaps[0] != null)
				{
					m_bitmaps[0].MakeTransparent(m_bitmaps[0].GetPixel(1,1));
					m_hBitmap[0] = m_bitmaps[0].GetHbitmap();
				}
				if (m_bitmaps[1] != null)
				{
					m_bitmaps[1].MakeTransparent(m_bitmaps[1].GetPixel(1,1));
					m_hBitmap[1] = m_bitmaps[1].GetHbitmap();
				}
			}

			catch (System.ArgumentException ex)
			{
				if (ex.TargetSite.ToString() == "Void .ctor(System.IO.Stream)")
				{
					System.Diagnostics.Debug.WriteLine(ex.Message);
					// Error accessing the bitmap embedded resource.
					m_bitmaps[0] = null;
					m_bitmaps[1] = null;
				}
			}
		}

		private void SetNames(string newName)
		{
			if (newName != null)
			{
				// Set the Name, FullName and BaseName, based on the specified string.
				m_names[0] = newName;
				int indx = newName.LastIndexOf(@"\");
				if (indx > -1)
          m_names[1] = newName.Substring(indx + 1); 
				else
					m_names[1] = newName; 
				
				indx = m_names[1].LastIndexOf(".");
				if (indx > -1)
          m_names[2] = m_names[1].Remove(indx, m_names[1].Length - indx);
				else
					m_names[1] = newName; 
			}
   	}
        ~GxPyObject()
		{
			if (m_hBitmap[0].ToInt32() != 0)
				DeleteObject(m_hBitmap[0]);
			if (m_hBitmap[1].ToInt32() != 0)
				DeleteObject(m_hBitmap[1]);
		}

		#endregion

        #region Implementation of IGxObject
        public void Attach(IGxObject Parent, IGxCatalog pCatalog)
        {
            m_gxParent = Parent;
            m_gxCatalog = pCatalog;
        }
        public void Detach()
        {
            m_gxParent = null;
            m_gxCatalog = null;
        }

        public void Refresh()
        {
            // No impl.		
        }

        public IName InternalObjectName
        {
            get
            {
                IFileName fileName = new FileNameClass();
                fileName.Path = m_names[0];

                return (IName)fileName;
            }
        }

        public bool IsValid
        {
            get
            {
                return File.Exists(m_names[0]);
            }
        }

        public string FullName
        {
            get
            {
                return m_names[0];
            }
        }

        public string BaseName
        {
            get
            {
                return m_names[2];
            }
        }

        public string Name
        {
            get
            {
                return m_names[1];
            }
        }

        public UID ClassID
        {
            get
            {
                UID clsid = new UIDClass();
                clsid.Value = "{0E63CDC4-7E13-422f-8B2D-F5DF853F9CA1}";
                return clsid;
            }
        }

        public IGxObject Parent
        {
            get
            {
                return m_gxParent;
            }
        }

        public string Category
        {
            get
            {
                return m_sCategory;
            }
        }
        #endregion

        #region Implementation of IGxObjectUI
        public UID NewMenu
        {
            get
            {
                //If you have created a class for new menu of this object, you can implement it here
                return null;
            }
        }

        public int SmallImage
        {
            get
            {
                if (m_bitmaps[1] != null)
                    return m_bitmaps[1].GetHbitmap().ToInt32();
                else
                    return 0;
            }
        }

        public int LargeSelectedImage
        {
            get
            {
                if (m_bitmaps[0] != null)
                    return m_bitmaps[0].GetHbitmap().ToInt32();
                else
                    return 0;
            }
        }

        public int SmallSelectedImage
        {
            get
            {
                if (m_bitmaps[1] != null)
                    return m_bitmaps[1].GetHbitmap().ToInt32();
                else
                    return 0;
            }
        }

        public UID ContextMenu
        {
            get
            {
                //If you have created a class for context menu of this object, you can implement it here
                return null;
            }
        }

        public int LargeImage
        {
            get
            {
                if (m_bitmaps[0] != null)
                    return m_bitmaps[0].GetHbitmap().ToInt32();
                else
                    return 0;
            }
        }
        #endregion
        
        #region Implementation of IGxObjectEdit

        public bool CanCopy()
        {
            return true;
        }

        public bool CanDelete()
        {
            return File.Exists(m_names[0]) && File.GetAttributes(m_names[0]) != FileAttributes.ReadOnly;
        }

        public bool CanRename()
        {
            return true;
        }

        public void Delete()
        {
            File.Delete(m_names[0]);

            //Tell parent the object is gone
            IGxObjectContainer pGxObjectContainer = (IGxObjectContainer)m_gxParent;
            pGxObjectContainer.DeleteChild(this);
        }

        public void EditProperties(int hParent)
        {
            //Add implementation if you have defined property page
        }

        public void Rename(string newShortName)
        {
            //Trim AML extension
            if (newShortName.ToUpper().EndsWith(".AML"))
                newShortName = newShortName.Substring(0, newShortName.Length - 4);

            //Construct new name
            int pos = m_names[0].LastIndexOf("\\");
            String newName = m_names[0].Substring(0, pos) + newShortName + ".aml";

            //Rename
            File.Move(m_names[0], newName);

            //Tell parent that name is changed
            m_gxParent.Refresh();
        }

        #endregion

        #region Implementation of IGxObjectProperties
		public object GetProperty(string Name)
		{
			if (Name != null)
			{
				switch (Name)
				{
					case "ESRI_GxObject_Name":
						return this.Name;					
					case "ESRI_GxObject_Type":
						return this.Category;

					default:
						return null;
				}
			}
			return null;
		}

		public void GetPropByIndex(int Index, ref string pName, ref object pValue)
		{
			switch (Index)
			{
				case 0:
					pName = "ESRI_GxObject_Name";
					pValue = (System.Object) this.Name;
					return;
				case 1:
					pName = "ESRI_GxObject_Type";
					pValue = (System.Object) this.Category;
					return;
				default:
					pName = null;
					pValue = null;
					return;
			}
		}

		public void SetProperty(string Name, object Value)
		{
			//No implementation
			return;	
		}

		public int PropertyCount
		{
			get
			{
				return 2;
			}
		}
		#endregion
           
    }
}
