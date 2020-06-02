/*

   Copyright 2019 Esri

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
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.Carto;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.esriSystem;

namespace TOCLayerFilterCS
{
    [Guid("aede0664-ef97-4c6c-94e6-b3c6216eedc5")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("TOCLayerFilterCS.TOCLayerFilter")]
    public partial class TOCLayerFilter : UserControl, IContentsView3
    {
        private IApplication m_application;
        private bool m_visible;
        private object m_contextItem;
        private bool m_isProcessEvents;

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
            ContentsViews.Register(regKey);

        }
        /// <summary>
        /// Required method for ArcGIS Component Category unregistration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryUnregistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            ContentsViews.Unregister(regKey);

        }

        #endregion
        #endregion
        public TOCLayerFilter()
        {
            InitializeComponent();
        }

        #region "IContentsView3 Implementations"

        public int Bitmap
        {
          get
          {
            string bitmapResourceName = GetType().Name + ".bmp";
            System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(GetType(), bitmapResourceName);
            bmp.MakeTransparent(bmp.GetPixel(1, 1)); //alternatively use a .png with transparency
            return bmp.GetHbitmap().ToInt32();
          }
        }

        public string Tooltip
        {
          get { return "Layer Types (C#)"; }
        }

        public bool Visible
        {
            get { return m_visible; }
            set { m_visible = value; }
        }
        string IContentsView3.Name { get { return "Layer Types (C#)"; } }
        public int hWnd { get { return this.Handle.ToInt32(); } }

        public object ContextItem //last right-clicked item
        {
            get { return m_contextItem; }
            set { }//Not implemented
        }

        public void Activate(int parentHWnd, IMxDocument Document)
        {
            if (m_application == null)
            {
                m_application = ((IDocument)Document).Parent;
                if (cboLayerType.SelectedIndex < 0)
                    cboLayerType.SelectedIndex = 0; //this should refresh the list automatically
                else
                    RefreshList();

                SetUpDocumentEvent(Document as IDocument);
            }
        }

        public void BasicActivate(int parentHWnd, IDocument Document)
        {
        }
        
        public void Refresh(object item)
        {
            if (item != this)
            {
                //when items are added, removed, reordered
                tvwLayer.SuspendLayout();
                RefreshList();
                tvwLayer.ResumeLayout();
            }
        }
        public void Deactivate()
        {
            //Any clean up? 
        }

        public void AddToSelectedItems(object item) { }
        public object SelectedItem
        {
            get
            {
                //No Multiselect so return selected node
                if (tvwLayer.SelectedNode != null)
                    return tvwLayer.SelectedNode.Tag;   //Layer
                return null;
            }
            set
            {
                //Not implemented
            }
        }
        public bool ProcessEvents { set { m_isProcessEvents = value; } }
        public void RemoveFromSelectedItems(object item) { }
        public bool ShowLines
        {
            get { return tvwLayer.ShowLines; }
            set { tvwLayer.ShowLines = value; }
        }
        #endregion

        private void RefreshList()
        {
            tvwLayer.Nodes.Clear();
            UID layerFilter = null;
            if (cboLayerType.SelectedItem.Equals("Feature Layers"))
            {
                layerFilter = new UIDClass();
                layerFilter.Value = "{40A9E885-5533-11d0-98BE-00805F7CED21}"; //typeof(IFeatureLayer).GUID.ToString("B");
            }
            else if (cboLayerType.SelectedItem.Equals("Raster Layers"))
            {
                layerFilter = new UIDClass();
                layerFilter.Value = typeof(IRasterLayer).GUID.ToString("B");
            }
            else if (cboLayerType.SelectedItem.Equals("Data Layers"))
            {
                layerFilter = new UIDClass();
                layerFilter.Value = "{6CA416B1-E160-11D2-9F4E-00C04F6BC78E}"; //typeof(IDataLayer).GUID.ToString("B");
            }

            IMxDocument document = (IMxDocument)m_application.Document;
            IMaps maps = document.Maps;
            for (int i = 0; i < maps.Count; i++)
            {
                IMap map = maps.get_Item(i);
                TreeNode mapNode = tvwLayer.Nodes.Add(map.Name);
                mapNode.Tag = map;
                if (map.LayerCount > 0)
                {
                    IEnumLayer layers = map.get_Layers(layerFilter, true);
                    layers.Reset();
                    ILayer lyr;
                    while ((lyr = layers.Next()) != null)
                    {
                        TreeNode lyrNode = mapNode.Nodes.Add(lyr.Name);
                        lyrNode.Tag = lyr;
                    }

                    mapNode.ExpandAll();
                }
            }
        }

        private void cboLayerType_SelectedIndexChanged(object sender, EventArgs e)
        {
            tvwLayer.SuspendLayout();
            RefreshList();
            tvwLayer.ResumeLayout();
        }
        private void tvwLayer_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                //Set item for context menu commands to work with
                m_contextItem = e.Node.Tag;

                //Show context menu
                UID menuID = new UIDClass();
                if (e.Node.Tag is IMap) //data frame
                {
                    menuID.Value = "{F42891B5-2C92-11D2-AE07-080009EC732A}"; //Data Frame Context Menu (TOC) 
                }
                else //Layer - custom menu
                {
                    menuID.Value = "{30cb4a78-6eba-4f60-b52e-38bc02bacc89}";
                }
                ICommandBar cmdBar = (ICommandBar)m_application.Document.CommandBars.Find(menuID, false, false);
                cmdBar.Popup(0, 0);
            }
        }

        #region "Add Event Wiring for New/Open Documents"
        // Event member variables
        private IDocumentEvents_Event m_docEvents;

        // Wiring
        private void SetUpDocumentEvent(IDocument myDocument)
        {
            m_docEvents = myDocument as IDocumentEvents_Event;

            m_docEvents.OpenDocument += new IDocumentEvents_OpenDocumentEventHandler(RefreshList);
            m_docEvents.NewDocument += new IDocumentEvents_NewDocumentEventHandler(RefreshList);
        }
      
        #endregion

    }
}
