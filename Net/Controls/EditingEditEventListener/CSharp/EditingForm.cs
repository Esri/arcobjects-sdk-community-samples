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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;

using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.Geodatabase;
namespace EditingSampleApp
{
    public partial class EditingForm : Form
    {

        #region class private members
        private IToolbarMenu m_toolbarMenu;       

        #endregion
        internal TabPage m_selectTab;
        internal TabPage m_listenTab;
        Events.EventListener eventListener;
        IEngineEditor m_editor;
        
        public EditingForm()
        {
            InitializeComponent();
        }

        private void EngineEditingForm_Load(object sender, EventArgs e)
        {
            m_editor = new EngineEditorClass();
           
            //Set buddy controls
            axTOCControl1.SetBuddyControl(axMapControl1);
            axEditorToolbar.SetBuddyControl(axMapControl1);
            axToolbarControl1.SetBuddyControl(axMapControl1);

            //Add items to the ToolbarControl
            axToolbarControl1.AddItem("esriControls.ControlsOpenDocCommand", 0, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
            axToolbarControl1.AddItem("esriControls.ControlsSaveAsDocCommand", 0, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
            axToolbarControl1.AddItem("esriControls.ControlsAddDataCommand", 0, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);            
            axToolbarControl1.AddItem("esriControls.ControlsMapZoomInTool", 0, -1, true, 0, esriCommandStyles.esriCommandStyleIconOnly);
            axToolbarControl1.AddItem("esriControls.ControlsMapZoomOutTool", 0, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
            axToolbarControl1.AddItem("esriControls.ControlsMapPanTool", 0, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
            axToolbarControl1.AddItem("esriControls.ControlsMapFullExtentCommand", 0, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
            axToolbarControl1.AddItem("esriControls.ControlsMapZoomToLastExtentBackCommand", 0, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
            axToolbarControl1.AddItem("esriControls.ControlsMapZoomToLastExtentForwardCommand", 0, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
           
            //Add items to the custom editor toolbar          
            axEditorToolbar.AddItem("esriControls.ControlsEditingEditorMenu", 0, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
            axEditorToolbar.AddItem("esriControls.ControlsEditingEditTool", 0, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
            axEditorToolbar.AddItem("esriControls.ControlsEditingSketchTool", 0, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
            axEditorToolbar.AddItem("esriControls.ControlsUndoCommand", 0, -1, true, 0, esriCommandStyles.esriCommandStyleIconOnly);
            axEditorToolbar.AddItem("esriControls.ControlsRedoCommand", 0, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
            axEditorToolbar.AddItem("esriControls.ControlsEditingTargetToolControl", 0, -1, true, 0, esriCommandStyles.esriCommandStyleIconOnly);
            axEditorToolbar.AddItem("esriControls.ControlsEditingTaskToolControl", 0, -1, true, 0, esriCommandStyles.esriCommandStyleIconOnly);
                       
            //Create a popup menu
            m_toolbarMenu = new ToolbarMenuClass();
            m_toolbarMenu.AddItem("esriControls.ControlsEditingSketchContextMenu", 0, 0, false, esriCommandStyles.esriCommandStyleTextOnly);

            //share the command pool          
            axToolbarControl1.CommandPool = axEditorToolbar.CommandPool;
            m_toolbarMenu.CommandPool = axEditorToolbar.CommandPool;

            //Create an operation stack for the undo and redo commands to use
            IOperationStack operationStack = new ControlsOperationStackClass();
            axEditorToolbar.OperationStack = operationStack;

            // Fill the Check List of Events for Selection
            TabControl tabControl = eventTabControl as TabControl;
            System.Collections.IEnumerator enumTabs = tabControl.TabPages.GetEnumerator();
            enumTabs.MoveNext();
            m_listenTab = enumTabs.Current as TabPage;
            enumTabs.MoveNext();
            m_selectTab = enumTabs.Current as TabPage;

            CheckedListBox editEventList = m_selectTab.GetNextControl(m_selectTab, true) as CheckedListBox;
            editEventList.ItemCheck += new ItemCheckEventHandler(editEventList_ItemCheck);

            ListBox listEvent = m_listenTab.GetNextControl(m_listenTab, true) as ListBox;
            listEvent.MouseDown += new MouseEventHandler(listEvent_MouseDown);

            eventListener = new Events.EventListener(m_editor);

            eventListener.Changed += new Events.ChangedEventHandler(eventListener_Changed);

            //populate the editor events            
            editEventList.Items.AddRange(Enum.GetNames(typeof(Events.EventListener.EditorEvent)));

            //add some sample line data to the map
            IWorkspaceFactory workspaceFactory = new AccessWorkspaceFactoryClass();
            //relative file path to the sample data from EXE location
            string filePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            filePath = System.IO.Path.Combine(filePath, @"ArcGIS\data\StreamflowDateTime\Streamflow.mdb");
            
            IFeatureWorkspace workspace = (IFeatureWorkspace)workspaceFactory.OpenFromFile(filePath, axMapControl1.hWnd);

            //Add the various layers
            IFeatureLayer featureLayer1 = new FeatureLayerClass();
            featureLayer1.Name = "Watershed";
            featureLayer1.Visible = true;
            featureLayer1.FeatureClass = workspace.OpenFeatureClass("Watershed");
            axMapControl1.Map.AddLayer((ILayer)featureLayer1);

            IFeatureLayer featureLayer2 = new FeatureLayerClass();
            featureLayer2.Name = "TimSerTool";
            featureLayer2.Visible = true;
            featureLayer2.FeatureClass = workspace.OpenFeatureClass("TimSerTool");
            axMapControl1.Map.AddLayer((ILayer)featureLayer2);

        }


        private void axMapControl1_OnMouseUp(object sender, IMapControlEvents2_OnMouseUpEvent e)
        {
            if (e.button == 2) m_toolbarMenu.PopupMenu(e.x, e.y, axMapControl1.hWnd);

        }

        void editEventList_ItemCheck(object sender, System.Windows.Forms.ItemCheckEventArgs e)
        {
          // start or stop listening for event based on checked state
          eventListener.ListenToEvents((Events.EventListener.EditorEvent)e.Index, e.NewValue == CheckState.Checked);
        }
        void eventListener_Changed(object sender, Events.EditorEventArgs e)
        {
          ((ListBox)m_listenTab.GetNextControl(m_listenTab, true)).Items.Add(e.eventType.ToString());
        }
        void listEvent_MouseDown(object sender, MouseEventArgs e)
        {
          if (e.Button == MouseButtons.Right)
          {
            this.lstEditorEvents.Items.Clear();
            this.lstEditorEvents.Refresh();
          }
        }

      private void chkAllOn_Click(object sender, EventArgs e)
      {
        CheckedListBox editEventList = m_selectTab.GetNextControl(m_selectTab, true) as CheckedListBox;
        for (int i = 0; i < editEventList.Items.Count; i++ )
        {
          editEventList.SetItemChecked(i, true);
        }      
      }

      private void chkAllOff_Click(object sender, EventArgs e)
      {
        CheckedListBox editEventList = m_selectTab.GetNextControl(m_selectTab, true) as CheckedListBox;
        for (int i = 0; i < editEventList.Items.Count; i++)
        {
          editEventList.SetItemChecked(i, false);
        }      
      }

      private void clearEventList_Click(object sender, EventArgs e)
      {
        this.lstEditorEvents.Items.Clear();
        this.lstEditorEvents.Refresh();
      }
    }
}