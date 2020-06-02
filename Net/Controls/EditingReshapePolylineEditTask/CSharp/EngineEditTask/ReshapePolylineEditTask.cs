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
using System.Text;
using System.Runtime.InteropServices;

using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;

namespace ReshapePolylineEditTask_CS
{
    [Guid("89467aa7-76c1-4531-a160-e160e8d782f7")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("ReshapePolylineEditTask_CS.ReshapePolylineEditTask")]
    public class ReshapePolylineEditTask : ESRI.ArcGIS.Controls.IEngineEditTask
    {
        #region Private Members
        IEngineEditor m_engineEditor;
        IEngineEditSketch m_editSketch;
        IEngineEditLayers m_editLayer;
        #endregion

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
            EngineEditTasks.Register(regKey);

        }
        /// <summary>
        /// Required method for ArcGIS Component Category unregistration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryUnregistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            EngineEditTasks.Unregister(regKey);

        }

        #endregion
        #endregion

        #region IEngineEditTask Implementations

        public void Activate(ESRI.ArcGIS.Controls.IEngineEditor editor, ESRI.ArcGIS.Controls.IEngineEditTask oldTask)
        {
            if (editor == null)
                return;

            m_engineEditor = editor;
            m_editSketch = m_engineEditor as IEngineEditSketch;
            m_editSketch.GeometryType = esriGeometryType.esriGeometryPolyline;
            m_editLayer = m_editSketch as IEngineEditLayers; 
           
            //Listen to engine editor events
            ((IEngineEditEvents_Event)m_editSketch).OnTargetLayerChanged += new IEngineEditEvents_OnTargetLayerChangedEventHandler(OnTargetLayerChanged);
            ((IEngineEditEvents_Event)m_editSketch).OnSelectionChanged += new IEngineEditEvents_OnSelectionChangedEventHandler(OnSelectionChanged);
            ((IEngineEditEvents_Event)m_editSketch).OnCurrentTaskChanged += new IEngineEditEvents_OnCurrentTaskChangedEventHandler(OnCurrentTaskChanged);
        }

     
        public void Deactivate()
        {
            m_editSketch.RefreshSketch();
            
            //Stop listening to engine editor events.
            ((IEngineEditEvents_Event)m_editSketch).OnTargetLayerChanged -= OnTargetLayerChanged;
            ((IEngineEditEvents_Event)m_editSketch).OnSelectionChanged -= OnSelectionChanged;
            ((IEngineEditEvents_Event)m_editSketch).OnCurrentTaskChanged -= OnCurrentTaskChanged;

            //Release object references.
            m_engineEditor = null;
            m_editSketch = null;
            m_editLayer = null;
        }

        public string GroupName
        {
            get
            {    
                //This property allows groups to be created/used in the EngineEditTaskToolControl treeview.
                //If an empty string is supplied the task will be appear in an "Other Tasks" group. 
                //In this example the Reshape Polyline_CSharp task will appear in the existing Modify Tasks group.
                return "Modify Tasks";
            }
        }

        public string Name
        {
            get
            {    
                return "Reshape Polyline_CSharp"; //unique edit task name
            }
        }

        public void OnDeleteSketch()
        {
        }

        public void OnFinishSketch()
        {
            //get reference to featurelayer being edited
            IFeatureLayer featureLayer = m_editLayer.TargetLayer as IFeatureLayer;
            //get reference to the sketch geometry
            IGeometry reshapeGeom = m_editSketch.Geometry;

            if (reshapeGeom.IsEmpty == false)
            {
                //get the currently selected feature    
                IFeatureSelection featureSelection = featureLayer as IFeatureSelection;
                ISelectionSet selectionSet = featureSelection.SelectionSet;
                ICursor cursor;
                selectionSet.Search(null, false, out cursor);
                IFeatureCursor featureCursor = cursor as IFeatureCursor;
                //the PerformSketchToolEnabledChecks property has already checked that only 1 feature is selected
                IFeature feature = featureCursor.NextFeature();
                 
                //Take a copy of geometry for the selected feature
                IGeometry editShape = feature.ShapeCopy;

                //create a path from the editsketch geometry
                IPointCollection reshapePath = new PathClass();
                reshapePath.AddPointCollection(reshapeGeom as IPointCollection);

                //reshape the selected feature
                IPolyline polyline = editShape as IPolyline;
                polyline.Reshape(reshapePath as IPath);

                #region Perform an edit operation to store the new geometry for selected feature

                try
                {
                    m_engineEditor.StartOperation();
                    feature.Shape = editShape;
                    feature.Store();
                    m_engineEditor.StopOperation("Reshape Feature");
                }
                catch (Exception ex)
                {
                    m_engineEditor.AbortOperation();
                    System.Diagnostics.Trace.WriteLine(ex.Message, "Reshape Geometry Failed");
                }

                #endregion
            }

            //refresh the display 
            IActiveView activeView = m_engineEditor.Map as IActiveView;
            activeView.PartialRefresh(esriViewDrawPhase.esriViewGeography, (object)featureLayer, activeView.Extent);
          

        }

        public string UniqueName
        {      
            get 
            { 
                return "ReshapePolylineEditTask_Reshape Polyline_CSharp" ;
            }
        }

        #endregion

        #region Event Listeners
        public void OnTargetLayerChanged()
        {
            PerformSketchToolEnabledChecks();
        }

        public void OnSelectionChanged()
        {
            PerformSketchToolEnabledChecks();
        }


        void OnCurrentTaskChanged()
        {
            if (m_engineEditor.CurrentTask.Name == "Reshape Polyline_CSharp")
            {
                PerformSketchToolEnabledChecks();
            }
        }
        #endregion

        #region private methods

        private void PerformSketchToolEnabledChecks()
        {
            if (m_editLayer == null)
                return;

            //Only enable the sketch tool if there is a polyline target layer.
            if (m_editLayer.TargetLayer.FeatureClass.ShapeType != esriGeometryType.esriGeometryPolyline)
            {
                m_editSketch.GeometryType = esriGeometryType.esriGeometryNull;
                return;
            }

            //check that only one feature in the target layer is currently selected
            IFeatureSelection featureSelection = m_editLayer.TargetLayer as IFeatureSelection;
            ISelectionSet selectionSet = featureSelection.SelectionSet;
            if (selectionSet.Count != 1)
            {
                m_editSketch.GeometryType = esriGeometryType.esriGeometryNull;
                return;
            }

            m_editSketch.GeometryType = esriGeometryType.esriGeometryPolyline;

        }

        #endregion

    }
}
