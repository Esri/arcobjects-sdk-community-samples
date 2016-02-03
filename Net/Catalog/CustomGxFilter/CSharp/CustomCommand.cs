using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.CatalogUI;
using ESRI.ArcGIS.Catalog;
using ESRI.ArcGIS.ArcCatalog;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;


namespace CustomGxFilter_CS
{
    public class CustomCommand : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        #region Member Variables
        private IGxApplication m_pApp;
        #endregion

        public CustomCommand()
        {
        }

        protected override void OnClick()
        {
            // set
            m_pApp = (IGxApplication)CustomGxFilter_CS.ArcCatalog.Application;
            IGxCatalog pCat = null;
            IGxFileFilter pFileFilter = null;
            IEnumGxObject pSelection = null;
            IGxDialog pDlg = null;
            IGxObjectFilter pFilter = null;
            try
            {
                pDlg = new GxDialog();
                pCat = pDlg.InternalCatalog ;
                pFileFilter = pCat.FileFilter;
                if (pFileFilter.FindFileType("py") < 0)
                {
                    //enter the third parameter with the location of the icon as needed
                    pFileFilter.AddFileType("PY", "Python file", "");
                }

                pFilter = new CustomGxFilter_CS.CustomFilter();
                pDlg.ObjectFilter = pFilter;
                pDlg.Title = "Please select a .Py file";
                pDlg.AllowMultiSelect = true;
                pDlg.DoModalOpen(0, out pSelection);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
            }
            finally
            {
                pCat = null;
                pFileFilter = null;
                pSelection = null;
                pDlg = null;
                pFilter = null;
            }
        }

        protected override void OnUpdate()
        {
            Enabled = ArcCatalog.Application != null;
        }
    }
}
