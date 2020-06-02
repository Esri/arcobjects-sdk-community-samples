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
using System.IO;

using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.Geodatabase;

namespace EditingSampleApp
{
    public partial class EditingForm : Form
    {

        #region class private members
        private IToolbarMenu m_toolbarMenu;
        private IEngineEditSketch m_engineEditor;

        #endregion
        
        public EditingForm()
        {
            InitializeComponent();
        }

        private void EngineEditingForm_Load(object sender, EventArgs e)
        {
           
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
            axEditorToolbar.AddItem("VertexCommands_CS.VertexCommandsMenu", 0, -1, false, 0, esriCommandStyles.esriCommandStyleIconAndText);
            axEditorToolbar.AddItem("esriControls.ControlsEditingEditTool", 0, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
            axEditorToolbar.AddItem("esriControls.ControlsEditingSketchTool", 0, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
            axEditorToolbar.AddItem("esriControls.ControlsUndoCommand", 0, -1, true, 0, esriCommandStyles.esriCommandStyleIconOnly);
            axEditorToolbar.AddItem("esriControls.ControlsRedoCommand", 0, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
            axEditorToolbar.AddItem("esriControls.ControlsEditingTargetToolControl", 0, -1, true, 0, esriCommandStyles.esriCommandStyleIconOnly);
            axEditorToolbar.AddItem("esriControls.ControlsEditingTaskToolControl", 0, -1, true, 0, esriCommandStyles.esriCommandStyleIconOnly);
           
            //Create a popup menu
            m_toolbarMenu = new ToolbarMenuClass();
            m_toolbarMenu.AddItem("esriControls.ControlsEditingSketchContextMenu", 0, 0, false, esriCommandStyles.esriCommandStyleTextOnly);

            m_engineEditor = new EngineEditorClass() as IEngineEditSketch; //this class is a singleton

            //share the command pool
            axToolbarControl1.CommandPool = axEditorToolbar.CommandPool;
            m_toolbarMenu.CommandPool = axToolbarControl1.CommandPool;

            //Create an operation stack for the undo and redo commands to use
            IOperationStack operationStack = new ControlsOperationStackClass();
            axEditorToolbar.OperationStack = operationStack;

            //add some sample line data to the map
            IWorkspaceFactory workspaceFactory = new ShapefileWorkspaceFactoryClass();
            //relative file path to the sample data from EXE location
            string filePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            filePath = System.IO.Path.Combine(filePath, @"ArcGIS\data\USAMajorHighways");
            IFeatureWorkspace workspace = (IFeatureWorkspace)workspaceFactory.OpenFromFile(filePath, axMapControl1.hWnd);

            IFeatureLayer featureLayer = new FeatureLayerClass();
            featureLayer.Name = "Highways";
            featureLayer.Visible = true;
            featureLayer.FeatureClass = workspace.OpenFeatureClass("usa_major_highways");

            axMapControl1.Map.AddLayer((ILayer)featureLayer);
            

        }

        private void axMapControl1_OnMouseDown(object sender, IMapControlEvents2_OnMouseDownEvent e)
        {
            if (e.button == 2)
            {
                m_toolbarMenu.PopupMenu(e.x, e.y, axMapControl1.hWnd);
            }
               
        }

        
    }
}