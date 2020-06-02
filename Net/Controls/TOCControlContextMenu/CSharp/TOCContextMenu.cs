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
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.esriSystem;

namespace ContextMenu
{
    public partial class ContextMenu : Form
    {
        private ITOCControl2 m_tocControl;
        private IMapControl3 m_mapControl;
        private IToolbarMenu m_menuMap;
        private IToolbarMenu m_menuLayer;

        public ContextMenu()
        {
            InitializeComponent();
        }

        private void ContextMenu_Load(object sender, EventArgs e)
        {
            m_tocControl = (ITOCControl2)axTOCControl1.Object;
            m_mapControl = (IMapControl3)axMapControl1.Object;

            //Set buddy control
            m_tocControl.SetBuddyControl(m_mapControl);
            axToolbarControl1.SetBuddyControl(m_mapControl);

            //Add pre-defined control commands to the ToolbarControl
            axToolbarControl1.AddItem("esriControls.ControlsSelectFeaturesTool", -1, 0, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
            axToolbarControl1.AddToolbarDef("esriControls.ControlsMapNavigationToolbar", 0, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
            axToolbarControl1.AddItem("esriControls.ControlsOpenDocCommand", -1, 0, false, 0, esriCommandStyles.esriCommandStyleIconOnly);

            //Add custom commands to the map menu
            m_menuMap = new ToolbarMenuClass();
            m_menuMap.AddItem(new LayerVisibility(), 1, 0, false, esriCommandStyles.esriCommandStyleTextOnly);
            m_menuMap.AddItem(new LayerVisibility(), 2, 1, false, esriCommandStyles.esriCommandStyleTextOnly);
            //Add pre-defined menu to the map menu as a sub menu 
            m_menuMap.AddSubMenu("esriControls.ControlsFeatureSelectionMenu", 2, true);
            //Add custom commands to the map menu
            m_menuLayer = new ToolbarMenuClass();
            m_menuLayer.AddItem(new  RemoveLayer(), -1, 0, false, esriCommandStyles.esriCommandStyleTextOnly);
            m_menuLayer.AddItem(new ScaleThresholds(), 1, 1, true, esriCommandStyles.esriCommandStyleTextOnly);
            m_menuLayer.AddItem(new ScaleThresholds(), 2, 2, false, esriCommandStyles.esriCommandStyleTextOnly);
            m_menuLayer.AddItem(new ScaleThresholds(), 3, 3, false, esriCommandStyles.esriCommandStyleTextOnly);
            m_menuLayer.AddItem(new LayerSelectable(), 1, 4, true, esriCommandStyles.esriCommandStyleTextOnly);
            m_menuLayer.AddItem(new LayerSelectable(), 2, 5, false, esriCommandStyles.esriCommandStyleTextOnly);
            m_menuLayer.AddItem(new ZoomToLayer(), -1, 6, true, esriCommandStyles.esriCommandStyleTextOnly);

            //Set the hook of each menu
            m_menuLayer.SetHook(m_mapControl);
            m_menuMap.SetHook(m_mapControl);
        }

        private void axTOCControl1_OnMouseDown(object sender, ITOCControlEvents_OnMouseDownEvent e)
        {
            if (e.button != 2) return;

            esriTOCControlItem item = esriTOCControlItem.esriTOCControlItemNone;
            IBasicMap map = null; ILayer layer = null;
            object other = null; object index = null;

            //Determine what kind of item is selected
            m_tocControl.HitTest(e.x, e.y, ref item, ref map, ref layer, ref other, ref index);
            
            //Ensure the item gets selected 
            if (item == esriTOCControlItem.esriTOCControlItemMap) 
                m_tocControl.SelectItem(map, null);
            else
                m_tocControl.SelectItem(layer, null);
            
            //Set the layer into the CustomProperty (this is used by the custom layer commands)			
            m_mapControl.CustomProperty = layer;

            //Popup the correct context menu
            if (item == esriTOCControlItem.esriTOCControlItemMap) m_menuMap.PopupMenu(e.x, e.y, m_tocControl.hWnd);
            if (item == esriTOCControlItem.esriTOCControlItemLayer) m_menuLayer.PopupMenu(e.x, e.y, m_tocControl.hWnd);

        }
    }
}