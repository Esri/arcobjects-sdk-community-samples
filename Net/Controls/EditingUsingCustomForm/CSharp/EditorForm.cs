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
using System.Collections;

using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace EditingUsingCustomForm
{
    public partial class EditorForm : Form
    {

        #region private members
        private MainForm m_mainForm;
        private IMapControl3 m_mapControl;
        private ArrayList m_commands;
        private IEngineEditor m_engineEditor;
        private IOperationStack m_operationStack;
        private ICommandPool m_pool;
        #endregion

        public EditorForm()
        {
            InitializeComponent();
        }

        private void EditorForm_Load(object sender, EventArgs e)
        {
            //*********  Important *************
            //Obtain a reference to the MainForm using the EditHelper class
            m_mainForm = EditHelper.TheMainForm;
            m_mapControl = m_mainForm.MapControl;

            //buddy the toolbars with the MapControl 
            axBlankToolBar.SetBuddyControl(m_mapControl);
            axModifyToolbar.SetBuddyControl(m_mapControl);
            axReshapeToolbar.SetBuddyControl(m_mapControl);
            axUndoRedoToolbar.SetBuddyControl(m_mapControl);
            axCreateToolbar.SetBuddyControl(m_mapControl);

            //Create and share command pool
            m_pool = new CommandPoolClass();
            axCreateToolbar.CommandPool = m_pool;  
            axBlankToolBar.CommandPool = m_pool;
            axModifyToolbar.CommandPool = m_pool;
            axReshapeToolbar.CommandPool = m_pool;
            axUndoRedoToolbar.CommandPool = m_pool;

            //Create and share operation stack
            m_operationStack = new ControlsOperationStackClass();
            axModifyToolbar.OperationStack = m_operationStack;
            axReshapeToolbar.OperationStack = m_operationStack;
            axUndoRedoToolbar.OperationStack = m_operationStack;
            axCreateToolbar.OperationStack = m_operationStack;

            //load items for the axModifyToolbar
            axModifyToolbar.AddItem("esriControls.ControlsEditingEditTool", 0, 0, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
            axModifyToolbar.AddItem("VertexCommands_CS.CustomVertexCommands", 1, 1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
            axModifyToolbar.AddItem("VertexCommands_CS.CustomVertexCommands", 2, 2, false, 0, esriCommandStyles.esriCommandStyleIconOnly);

            //load items for the axReshapeToolbar
            axReshapeToolbar.AddItem("esriControls.ControlsEditingEditTool", 0, 0, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
            axReshapeToolbar.AddItem("esriControls.ControlsEditingSketchTool", 0, 1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);

            //load items for the axCreateToolbar
            axCreateToolbar.AddItem("esriControls.ControlsEditingSketchTool", 0, 0, false, 0, esriCommandStyles.esriCommandStyleIconOnly);

            //set up the EngineEditor
            m_engineEditor = new EngineEditorClass();
            m_engineEditor.EnableUndoRedo(true);
            ((IEngineEditProperties2)m_engineEditor).StickyMoveTolerance = 10000;
            object tbr = (object)axCreateToolbar.Object;
            IExtension engineEditorExt = m_engineEditor as IExtension;
            engineEditorExt.Startup(ref tbr); //ensures that the operationStack will function correctly

            //Listen to OnSketchModified engine editor event
            ((IEngineEditEvents_Event)m_engineEditor).OnSketchModified += new IEngineEditEvents_OnSketchModifiedEventHandler(OnSketchModified);
            //listen to MainForm events in case application is closed while editing
            EditHelper.TheMainForm.FormClosing += new FormClosingEventHandler(TheMainForm_FormClosing);

            #region Form Management
            m_commands = new ArrayList();
            m_commands.Add(cmdModify);
            m_commands.Add(cmdReshape);
            m_commands.Add(cmdCreate);
            
            DisableButtons();
            txtInfo.Text = "";
            this.Size = new Size(242, 208);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            SetErrorLabel("");
            EditHelper.IsEditorFormOpen = true;
            #endregion
        }

        private void cmdCreate_Click(object sender, EventArgs e)
        {
            IEngineEditTask edittask = m_engineEditor.GetTaskByUniqueName("ControlToolsEditing_CreateNewFeatureTask");
  
            if (edittask != null)
            {
                m_engineEditor.CurrentTask = edittask;
                axCreateToolbar.CurrentTool = axCreateToolbar.GetItem(0).Command as ITool;
                
                SetButtonColors(sender as Button);
                txtInfo.Text = "";
                label1.Text = "";
                this.flowLayoutPanel1.Controls.Clear();
                this.flowLayoutPanel1.Controls.Add(axCreateToolbar);
                this.flowLayoutPanel2.Controls.Clear();
                this.flowLayoutPanel2.Controls.Add(axUndoRedoToolbar);
            }
        }

        private void cmdModify_Click(object sender, EventArgs e)
        {
            IEngineEditTask edittask = m_engineEditor.GetTaskByUniqueName("ControlToolsEditing_ModifyFeatureTask");
  
            if (edittask != null)
            {       
                m_engineEditor.CurrentTask = edittask;
                axModifyToolbar.CurrentTool = axModifyToolbar.GetItem(0).Command as ITool;
                
                SetButtonColors(sender as Button);
                txtInfo.Text = "";
                label1.Text = "";
                this.flowLayoutPanel1.Controls.Clear();
                this.flowLayoutPanel1.Controls.Add(axModifyToolbar);
                this.flowLayoutPanel2.Controls.Clear();
                this.flowLayoutPanel2.Controls.Add(axUndoRedoToolbar);
            }
        }

        private void cmdReshape_Click(object sender, EventArgs e)
        {
            IEngineEditTask edittask = m_engineEditor.GetTaskByUniqueName("ReshapePolylineEditTask_Reshape Polyline_CSharp");
  
            if (edittask != null)
            {
                m_engineEditor.CurrentTask = edittask;
                axReshapeToolbar.CurrentTool = axReshapeToolbar.GetItem(0).Command as ITool;

                SetButtonColors(sender as Button);
                txtInfo.Text = "";
                label1.Text = "";
                this.flowLayoutPanel1.Controls.Clear();
                this.flowLayoutPanel1.Controls.Add(axReshapeToolbar);
                this.flowLayoutPanel2.Controls.Clear();
                this.flowLayoutPanel2.Controls.Add(axUndoRedoToolbar);
            }
        }

        private void cmdEdit_Click(object sender, EventArgs e)
        {
            if (cmdEdit.Text == "Edit")
            {
                IFeatureLayer featlayer = FindFeatureLayer("usa_major_highways");

                if (featlayer != null)
                {
                    m_engineEditor.StartEditing(((IDataset)featlayer.FeatureClass).Workspace, m_mapControl.Map);
                    IEngineEditLayers editLayer = (IEngineEditLayers)m_engineEditor;
                    editLayer.SetTargetLayer(featlayer,0);
                    EnableButtons();

                    cmdEdit.Text = "Finish";
                    Color color = Color.Red;
                    cmdEdit.BackColor = color;
                    cmdCreate_Click(cmdCreate, null);
                }
            }
            else
            {
                SaveEdits();
                DisableButtons();
                cmdEdit.Text = "Edit";
                Color color = Color.White;
                cmdEdit.BackColor = color;
                SetErrorLabel("");
            }
        }
        
        private void EditorForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            CleanupOnFormClose();
        }

        void TheMainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            CleanupOnFormClose();
        }

        void OnSketchModified()
        {
            if (IsHighwaysEditValid)
            {
                SetErrorLabel("");
            }
            else
            {
                m_operationStack.Undo();
                SetErrorLabel("Invalid Edit");
            }
        }


        #region private form and button management

        private void SetSelectableLayerStatus(bool enable)
        {
            IMap map = m_mapControl.Map;
            
            for (int i = 0; i < map.LayerCount - 1; i++)
            {
                IFeatureLayer layer = (IFeatureLayer)map.get_Layer(i);
                layer.Selectable = enable;
            }
        }

        private void SetErrorLabel(string message)
        {
            label1.Text = message;
        }

        private void DisableButtons()
        {
            cmdReshape.Enabled = false;
            cmdCreate.Enabled = false;
            cmdModify.Enabled = false;

            foreach (Button button in m_commands)
            {
                Color color = Color.White;
                button.BackColor = color;
            }
        }

        private void EnableButtons()
        {
            cmdReshape.Enabled = true;
            cmdCreate.Enabled = true;
            cmdModify.Enabled = true;
        }

        private void SetButtonColors(Button clickedButton)
        {
            Color color;

            foreach (Button button in m_commands)
            {
                if (clickedButton == button)
                {
                    color = Color.ForestGreen;
                }
                else
                {
                    color = Color.White;
                }

                button.BackColor = color;
            }
        }

        private void SetInfoLabel(object sender, int index)
        {
            AxToolbarControl toolbarControl = sender as AxToolbarControl;
            IToolbarControl2 toolbar = toolbarControl.Object as IToolbarControl2;
            IToolbarItem item = toolbar.GetItem(index);
            ICommand command = item.Command;
            txtInfo.Text = command.Message;
        }

        private void axModifyToolbar_OnItemClick(object sender, IToolbarControlEvents_OnItemClickEvent e)
        {

            SetInfoLabel(sender, e.index);
        }

        private void axReshapeToolbar_OnItemClick(object sender, IToolbarControlEvents_OnItemClickEvent e)
        {
            SetInfoLabel(sender, e.index);
        }

        #endregion

        #region private helper methods/properties

        private void CleanupOnFormClose()
        {
            if (m_engineEditor.EditState == esriEngineEditState.esriEngineStateEditing)
            {
                SaveEdits();
            }

            EditHelper.IsEditorFormOpen = false;

            //unregister the event handlers
            ((IEngineEditEvents_Event)m_engineEditor).OnSketchModified -= new IEngineEditEvents_OnSketchModifiedEventHandler(OnSketchModified);
            EditHelper.TheMainForm.FormClosing -= new FormClosingEventHandler(TheMainForm_FormClosing);
            
        }

        private void SaveEdits()
        {
            bool saveEdits = false;

            if (m_engineEditor.HasEdits())
            {
                string message = "Do you wish to save your edits?";
                string caption = "Save Edits";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result;

                result = MessageBox.Show(message, caption, buttons);

                if (result == DialogResult.Yes)
                {
                    saveEdits = true;
                }
            }

            m_engineEditor.StopEditing(saveEdits);
        }

       
        private IFeatureLayer FindFeatureLayer(string name)
        {
            
            IFeatureLayer foundLayer = null;
            IDataset dataset = null;
            IMap map = m_mapControl.Map;
            
            for (int i = 0; i < map.LayerCount; i++)
            {
                IFeatureLayer layer = (IFeatureLayer)map.get_Layer(i);
                dataset = (IDataset)layer.FeatureClass;
                if (dataset.Name == name)
                {
                    foundLayer = layer;
                    break;
                }
            }

            return foundLayer;
        }


        private bool IsHighwaysEditValid
        {
            get
            {
                //put in all business logic here
                //In this example highways are not allowed to intersect the lakes layer
                bool success = true;

                //get the edit sketch
                IEngineEditSketch editsketch = (IEngineEditSketch)m_engineEditor;

                //get the protected areas layer
                IFeatureLayer lakesLayer = FindFeatureLayer("us_lakes");
               
                //do a spatial filter 
                if ((editsketch != null) && (lakesLayer != null) && (editsketch.Geometry !=null))
                {
                    IFeatureCursor cursor = FindFeatures(editsketch.Geometry, lakesLayer.FeatureClass, esriSpatialRelEnum.esriSpatialRelIntersects, m_mapControl.Map);
                    IFeature feature = cursor.NextFeature();

                    //could put more sophisticated logic in here
                    if (feature != null)
                        success = false;
                }

                return success;
            }

        }

        private IFeatureCursor FindFeatures(IGeometry geometry, IFeatureClass featureClass, esriSpatialRelEnum spatialRelationship, IMap map)
        {      
            //1 = esriSpatialRelIntersects
            //7 = esriSpatialWithin
            //8 = esriSpatialRelContains

            ISpatialFilter spatialFilter = new SpatialFilter();
            spatialFilter.Geometry = geometry;
            spatialFilter.set_OutputSpatialReference(featureClass.ShapeFieldName, map.SpatialReference);
            spatialFilter.GeometryField = featureClass.ShapeFieldName;
            spatialFilter.SpatialRel = spatialRelationship;

            IFeatureCursor featureCursor = featureClass.Search(spatialFilter, false);
            return featureCursor;
        }

        #endregion

    }
}