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
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.SystemUI;

namespace HookActions
{
    public partial class HookActions : Form
    {
        public HookActions()
        {
            InitializeComponent();
        }

        IToolbarMenu m_ToolbarMenu1;
        IToolbarMenu m_ToolbarMenu2; 

        private void HookActions_Load(object sender, EventArgs e)
        {
            //Add generic commands 
            axToolbarControl1.AddItem("esriControls.ControlsAddDataCommand", 0, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
            //Add map navigation commands
            axToolbarControl1.AddItem("esriControls.ControlsMapZoomInTool", 0, -1, true, 0, esriCommandStyles.esriCommandStyleIconOnly);
            axToolbarControl1.AddItem("esriControls.ControlsMapZoomOutTool", 0, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
            axToolbarControl1.AddItem("esriControls.ControlsMapPanTool", 0, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
            axToolbarControl1.AddItem("esriControls.ControlsMapFullExtentCommand", 0, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
            axToolbarControl1.AddItem("esriControls.ControlsSelectFeaturesTool", 0, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
            axToolbarControl1.AddItem("esriControls.ControlsSelectTool", 0, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);

            //Add generic commands 
            axToolbarControl2.AddItem("esriControls.ControlsAddDataCommand", 0, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
            //Add globe navigation commands
            axToolbarControl2.AddItem("esriControls.ControlsGlobeZoomInOutTool", 0, -1, true, 0, esriCommandStyles.esriCommandStyleIconOnly);
            axToolbarControl2.AddItem("esriControls.ControlsGlobePanDragTool", 0, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
            axToolbarControl2.AddItem("esriControls.ControlsGlobeFullExtentCommand", 0, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
            axToolbarControl2.AddItem("esriControls.ControlsGlobeSelectFeaturesTool", 0, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);

            //Create menu 
             m_ToolbarMenu1 = new ToolbarMenuClass();
            //Set hook and command pool
            m_ToolbarMenu1.SetHook(axToolbarControl1);
            m_ToolbarMenu1.CommandPool = axToolbarControl1.CommandPool;
            //Add custom commands
            m_ToolbarMenu1.AddItem(new hookActionsPan(), 0, -1, false, esriCommandStyles.esriCommandStyleTextOnly);
            m_ToolbarMenu1.AddItem(new hookActionsZoom(), 0, -1, false, esriCommandStyles.esriCommandStyleTextOnly);
            m_ToolbarMenu1.AddItem(new hookActionsFlash(), 0, -1, true, esriCommandStyles.esriCommandStyleTextOnly);
            m_ToolbarMenu1.AddItem(new hookActionsGraphic(), 0, -1, true, esriCommandStyles.esriCommandStyleTextOnly);
            m_ToolbarMenu1.AddItem(new hookActionsLabel(), 0, -1, false, esriCommandStyles.esriCommandStyleTextOnly);
            m_ToolbarMenu1.AddItem(new hookActionsCallout(), 0, -1, false, esriCommandStyles.esriCommandStyleTextOnly);

            //Create menu 
            m_ToolbarMenu2 = new ToolbarMenuClass();
            //Set hook and command pool
            m_ToolbarMenu2.SetHook(axToolbarControl2);
            m_ToolbarMenu2.CommandPool = axToolbarControl2.CommandPool;
            //Add custom commands
            m_ToolbarMenu2.AddItem(new hookActionsPan(), 0, -1, false, esriCommandStyles.esriCommandStyleTextOnly);
            m_ToolbarMenu2.AddItem(new hookActionsZoom(), 0, -1, false, esriCommandStyles.esriCommandStyleTextOnly);
            m_ToolbarMenu2.AddItem(new hookActionsFlash(), 0, -1, true, esriCommandStyles.esriCommandStyleTextOnly);
            m_ToolbarMenu2.AddItem(new hookActionsGraphic(), 0, -1, true, esriCommandStyles.esriCommandStyleTextOnly);
            m_ToolbarMenu2.AddItem(new hookActionsLabel(), 0, -1, false, esriCommandStyles.esriCommandStyleTextOnly);
            m_ToolbarMenu2.AddItem(new hookActionsCallout(), 0, -1, false, esriCommandStyles.esriCommandStyleTextOnly);

        }

        private void axMapControl1_OnMouseDown(object sender, ESRI.ArcGIS.Controls.IMapControlEvents2_OnMouseDownEvent e)
        {
            //Popup menu
            if (e.button == 2)
                m_ToolbarMenu1.PopupMenu(e.x, e.y, axMapControl1.hWnd);
        }

        private void axGlobeControl1_OnMouseDown(object sender, ESRI.ArcGIS.Controls.IGlobeControlEvents_OnMouseDownEvent e)
        {
            //Popup menu
            if (e.button == 2)
                m_ToolbarMenu2.PopupMenu(e.x, e.y, axGlobeControl1.hWnd);

        }
    }
}