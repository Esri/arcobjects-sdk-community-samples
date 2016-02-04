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
using ESRI.ArcGIS.Catalog;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using System.Windows.Forms;

namespace ExtentView_CS
{
    [Guid("0e572643-f419-4697-990e-800bd5f6c830")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("ExtentView_CS.clsExtentView")]
    public class clsExtentView : ESRI.ArcGIS.CatalogUI.IGxView
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
            GxTabViews.Register(regKey);

        }
        /// <summary>
        /// Required method for ArcGIS Component Category unregistration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryUnregistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            GxPreviews.Unregister(regKey);
            GxTabViews.Unregister(regKey);

        }

        #endregion
        #endregion

        #region Member Variables
        private IFillSymbol m_pFillSymbol;
        private GxSelection m_pSelection;
        private FrmExtentView frmExtentView = new FrmExtentView();
        private string m_path;
        #endregion

        #region Constructors
        public clsExtentView()
            : base()
        {
        }
        #endregion

        private void OnSelectionChanged(ESRI.ArcGIS.Catalog.IGxSelection Selection, ref object initiator)
        {
            //Refresh view
            if (m_pSelection != null) Refresh();
        }

        #region IGxView Implementations
        public void Activate(ESRI.ArcGIS.CatalogUI.IGxApplication Application, ESRI.ArcGIS.Catalog.IGxCatalog Catalog)
        {
            try
            {
                //Get selection
                m_pSelection = (GxSelection) Application.Selection;
                m_pSelection.OnSelectionChanged += new IGxSelectionEvents_OnSelectionChangedEventHandler(OnSelectionChanged);
                // get data from the MyProject's settings.
                // please change accordingly
                m_path = Properties.Settings.Default.DataLocation;
                //Add data to map control
                frmExtentView.AxMapControl1.AddShapeFile(m_path, "world30");
                frmExtentView.AxMapControl1.Extent = frmExtentView.AxMapControl1.FullExtent;

                //Create and setup the fill symbol that will be used to draw the dataset's extent
                // rectangle if it is not cached
                if (m_pFillSymbol == null)
                {
                    m_pFillSymbol = new SimpleFillSymbol();

                    IColor pColor = null;
                    ILineSymbol pLineSymbol = null;
                    pColor = new RgbColor();
                    pColor.NullColor = true;
                    m_pFillSymbol.Color = pColor;

                    pLineSymbol = new SimpleLineSymbol();
                    pColor.NullColor = false;
                    pColor.RGB = 200; //Red
                    pLineSymbol.Color = pColor;
                    pLineSymbol.Width = 2;
                    m_pFillSymbol.Outline = pLineSymbol;
                }

                //Draw extent
                Refresh();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
            }
        }

        public bool Applies(ESRI.ArcGIS.Catalog.IGxObject Selection)
        {
            //This view applies if the current Gx selection supports IGxDataset.
            return (Selection != null) & (Selection is IGxDataset);
        }

        public ESRI.ArcGIS.esriSystem.UID ClassID
        {
            get
            {
                //Set class ID
                ESRI.ArcGIS.esriSystem.IUID pUID = null;
                pUID = new UID();
                pUID.Value = "ExtentView_CS.clsExtentView";
                return (UID)pUID;
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
                return "Extent";
            }
        }

        public void Refresh()
        {
            //If the selection does not support IGxDataset, do nothing.
            IGxSelection pSelection = null;
            IGxObject pLocation = null;
            IGraphicsContainer pGraphicsLayer = null;
            try
            {
                pSelection = m_pSelection;
                pLocation = pSelection.Location;
                if (!(pLocation is IGxDataset))
                    return;

                //Clear the contents of the graphics layer.

                pGraphicsLayer = (IGraphicsContainer)frmExtentView.AxMapControl1.Map.BasicGraphicsLayer;
                pGraphicsLayer.DeleteAllElements();

                //Some dataset may not have content at all
                IGxDataset pGxDataset = null;
                IGeoDataset pGeoDataset = null;
                IElement pElement = null;
                IFillShapeElement pFillShapeElement = null;
                try
                {
                    //Get the geodataset out of the GxDataset.
                    pGxDataset = (IGxDataset)pLocation;
                    if (pGxDataset.Type == esriDatasetType.esriDTLayer | 
                        pGxDataset.Type == esriDatasetType.esriDTFeatureClass |
                        pGxDataset.Type == esriDatasetType.esriDTFeatureDataset)
                    {
                        pGeoDataset = (IGeoDataset)pGxDataset.Dataset;
                    }
                    else
                    {
                        return;
                    }

                    //Create a rectangular graphic element to represent the geodataset's full extent.

                    pElement = new RectangleElement();
                    pElement.Geometry = pGeoDataset.Extent;

                    //Set the element's symbology.

                    pFillShapeElement = (IFillShapeElement)pElement;
                    pFillShapeElement.Symbol = m_pFillSymbol;

                    //Add the rectangle element to the graphics layer, and force the map to redraw.
                    pGraphicsLayer.AddElement(pElement, 0);
                    frmExtentView.AxMapControl1.Refresh();

                }
                catch (Exception ex)
                {
                    frmExtentView.AxMapControl1.Refresh();
                    throw ex;
                }
                finally
                {
                    pGxDataset = null;
                    pGeoDataset = null;
                    pElement = null;
                    pFillShapeElement = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                pSelection = null;
                pLocation = null;
                pGraphicsLayer = null;
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
            // TODO: Add clsExtentView.SystemSettingChanged implementation
        }

        public int hWnd
        {
            get
            {
                //The map control's hWnd is to be used as the Gx view window.  Gx will embed this
                // hWnd inside the Preview window.
                return (Int32)frmExtentView.AxMapControl1.Handle;
            }
        }
        #endregion

    }
}
