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
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Resources;
using System.Reflection;

using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.SystemUI;


namespace VertexCommands_CS
{
    /// <summary>
    /// Contains 2 tools to insert or delete vertices. 
    /// Both make use the out-of-the-box ControlsCommands to do this
    /// </summary>
    [Guid("6583D8C5-7A4A-4efc-9FAA-2FCD4EAD5BC3")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("VertexCommands_CS.UsingOutOfBoxVertexCommands")]
    public sealed class UsingOutOfBoxVertexCommands : BaseTool, ICommandSubType
    {
        #region COM Registration Function(s)
        [ComRegisterFunction()]
        [ComVisible(false)]
        static void RegisterFunction(Type registerType)
        {
            // Required for ArcGIS Component Category Registrar support
            ArcGISCategoryRegistration(registerType);
        }

        [ComUnregisterFunction()]
        [ComVisible(false)]
        static void UnregisterFunction(Type registerType)
        {
            // Required for ArcGIS Component Category Registrar support
            ArcGISCategoryUnregistration(registerType);
        }

        #region ArcGIS Component Category Registrar generated code
        /// <summary>
        /// Required method for ArcGIS Component Category registration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryRegistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            ControlsCommands.Register(regKey);
        }
        /// <summary>
        /// Required method for ArcGIS Component Category unregistration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryUnregistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            ControlsCommands.Unregister(regKey);
        }

        #endregion
        #endregion

        #region Private Members

        private long m_lSubType;
        private IHookHelper m_hookHelper = null;
        private IEngineEditor m_engineEditor;
        private IEngineEditLayers m_editLayer;
        private System.Windows.Forms.Cursor m_InsertVertexCursor;
        private System.Windows.Forms.Cursor m_DeleteVertexCursor;
       
        #endregion

        #region Class constructor
        public UsingOutOfBoxVertexCommands()
        {
            #region load the cursors

            try
            {
                m_InsertVertexCursor = new System.Windows.Forms.Cursor(GetType().Assembly.GetManifestResourceStream("VertexCommands_CS.InsertVertexCursor.cur"));
                m_DeleteVertexCursor = new System.Windows.Forms.Cursor(GetType().Assembly.GetManifestResourceStream("VertexCommands_CS.DeleteVertexCursor.cur"));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message, "Invalid Cursor");
            }
            #endregion
        }
        #endregion

        #region Overriden Class Methods

        /// <summary>
        /// Return the cursor to be used by the tool
        /// </summary>
        public override int Cursor
        {      
            get
            { 
                int iHandle = 0;

                switch (m_lSubType)
                {
                    case 1:
                        iHandle = m_InsertVertexCursor.Handle.ToInt32();
                        break;
                    
                    case 2:
                        iHandle = m_DeleteVertexCursor.Handle.ToInt32();
                        break;
                }

                return (iHandle); 
            }
        }

        public override void OnClick()
        {
            //Find the Modify Feature task and set it as the current task
            IEngineEditTask editTask = m_engineEditor.GetTaskByUniqueName("ControlToolsEditing_ModifyFeatureTask");
            m_engineEditor.CurrentTask = editTask;
        }

        /// <summary>
        /// Occurs when this tool is created
        /// </summary>
        /// <param name="hook">Instance of the application</param>
        public override void OnCreate(object hook)
        {
            try
            {
                m_hookHelper = new HookHelperClass();
                m_hookHelper.Hook = hook;    
                m_engineEditor = new EngineEditorClass(); //this class is a singleton
                m_editLayer = m_engineEditor as IEngineEditLayers;
            }
            catch
            {
                m_hookHelper = null;
            }
        }


        /// <summary>
        /// Perform checks so that the tool is enabled appropriately
        /// </summary>
        public override bool Enabled
        {       
            get
            {
                //check whether Editing 
                if (m_engineEditor.EditState == esriEngineEditState.esriEngineStateNotEditing)
                {
                    return false;
                }

                //check for appropriate geometry types
                esriGeometryType geomType = m_editLayer.TargetLayer.FeatureClass.ShapeType;
                if ((geomType != esriGeometryType.esriGeometryPolygon) & (geomType != esriGeometryType.esriGeometryPolyline))
                {
                    return false;
                }

                //check that only one feature is currently selected
                IFeatureSelection featureSelection = m_editLayer.TargetLayer as IFeatureSelection;
                ISelectionSet selectionSet = featureSelection.SelectionSet;
                if (selectionSet.Count != 1)
                {
                    return false;
                }

                return true;
            }
        }
        
        /// <summary>
        /// The mouse up performs the action appropriate for each sub-typed command: 
        /// insert vertex or delete vertex
        /// </summary>
        /// <param name="Button"></param>
        /// <param name="Shift"></param>
        /// <param name="X">The X screen coordinate of the clicked location</param>
        /// <param name="Y">The Y screen coordinate of the clicked location</param>
        public override void OnMouseUp(int Button, int Shift, int X, int Y)
        {
            try
            {
                //get layer being edited
                IFeatureLayer featureLayer = m_editLayer.TargetLayer as IFeatureLayer;

                //set the x,y location which will be used by the out-of-the-box commands         
                IEngineEditSketch editsketch = m_engineEditor as IEngineEditSketch;  
                editsketch.SetEditLocation(X, Y);

                Type t = null;
                object o = null;
                
                switch (m_lSubType)
                {

                    case 1: //Insert Vertex using out-of-the-box command 

                        t = Type.GetTypeFromProgID("esriControls.ControlsEditingVertexInsertCommand.1");
                        o = Activator.CreateInstance(t);
                        ICommand insertVertexCommand = o as ICommand;

                        if (insertVertexCommand != null)
                        {
                            insertVertexCommand.OnCreate(m_hookHelper.Hook);
                            insertVertexCommand.OnClick();
                        }

                        break;

                    case 2: //Delete Vertex using out-of-the-box command 

                        t = Type.GetTypeFromProgID("esriControls.ControlsEditingVertexDeleteCommand.1");
                        o = Activator.CreateInstance(t);
                        ICommand deleteVertexCommand = o as ICommand;

                        if (deleteVertexCommand != null)
                        {
                            deleteVertexCommand.OnCreate(m_hookHelper.Hook);
                            deleteVertexCommand.OnClick();
                        }
                       
                        break;
                }  
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message, "Unexpected Error");
            }
        }

        #endregion

        #region ICommandSubType Interface

        /// <summary>
        /// Returns the number of subtyped commands
        /// </summary>
        /// <returns></returns>
        public int GetCount()
        {
            return 2;
        }

        /// <summary>
        /// Sets the sub-type
        /// </summary>
        /// <param name="SubType"></param>
        public void SetSubType(int SubType)
        {
            m_lSubType = SubType;

            ResourceManager rm = new ResourceManager("VertexCommands_CS.Resources", Assembly.GetExecutingAssembly());

            //set a common Command category for all subtypes
            base.m_category = "Vertex Cmds (C#)";

            switch (m_lSubType)
            {
                case 1: //Insert Vertex using the out-of-the-box ControlsEditingSketchInsertPointCommand command

                    base.m_caption = (string)rm.GetString("OOBInsertVertex_CommandCaption");
                    base.m_message = (string)rm.GetString("OOBInsertVertex_CommandMessage");
                    base.m_toolTip = (string)rm.GetString("OOBInsertVertex_CommandToolTip");
                    base.m_name = "VertexCommands_UsingOutOfBoxInsertVertex";
                    base.m_cursor = m_InsertVertexCursor;

                    #region bitmap

                    try
                    {
                        base.m_bitmap = (System.Drawing.Bitmap)rm.GetObject("OOBInsertVertex");
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Trace.WriteLine(ex.Message, "Invalid Bitmap");
                    }

                    #endregion

                    break;

                case 2: //Delete vertex at clicked location using the out-of-the-box ControlsEditingSketchDeletePointCommand

                    base.m_caption = (string)rm.GetString("OOBDeleteVertex_CommandCaption");
                    base.m_message = (string)rm.GetString("OOBDeleteVertex_CommandMessage");
                    base.m_toolTip = (string)rm.GetString("OOBDeleteVertex_CommandToolTip");
                    base.m_name = "VertexCommands_UsingOutOfBoxDeleteVertex";
                    base.m_cursor = m_DeleteVertexCursor;

                    #region bitmap
                    try
                    {
                        base.m_bitmap = (System.Drawing.Bitmap)rm.GetObject("OOBDeleteVertex");
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Trace.WriteLine(ex.Message, "Invalid Bitmap");
                    }

                    #endregion

                    break;
            }
        }

        #endregion
    }
        
}
