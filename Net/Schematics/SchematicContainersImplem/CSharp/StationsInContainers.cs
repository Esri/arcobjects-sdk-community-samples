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
using ESRI.ArcGIS.Schematic;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using Microsoft.VisualBasic;

namespace ContainerManagerCS
{
    public class StationsInContainers : ESRI.ArcGIS.Desktop.AddIns.Extension
    {
        //The OnAfterLoadDiagram is used to define the relations between elements
        //contained in a diagram. It is called by the AfterLoadDiagram 
        //event defined in the schematic project. The diagram contains Stations and 
        //Containers elements. For the Station element type, a particular attribute 
        //named RelatedFeeder has been created. This attribute is used to identify 
        //the container the station is related to. Near the top of the procedure, 
        //a new schematic relation is created (SchematicContainerManager). The 
        //ISchematicRelationControllerEdit CreateRelation method is used to specify 
        //that the station is related to its container. The value set for the 
        //RelatedFeeder attribute specifies whether or not the station is related 
        //to a container.

        private SchematicDatasetManager m_DatasetMgr;
        private static StationsInContainers s_extension;


        protected override void OnStartup()
        {
            s_extension = this;
            m_DatasetMgr = new SchematicDatasetManager();
        }


        protected override void OnShutdown()
        {
            ResetEvents(false);

            s_extension = null;
            m_DatasetMgr = null;

            base.OnShutdown();
        }


        protected override bool OnSetState(ESRI.ArcGIS.Desktop.AddIns.ExtensionState state)
        {
            //Users can optionally check license here.
            this.State = state;

            if (state == ESRI.ArcGIS.Desktop.AddIns.ExtensionState.Enabled)
                ResetEvents(true);
            else
                ResetEvents(false);

            return base.OnSetState(state);
        }

        protected override ESRI.ArcGIS.Desktop.AddIns.ExtensionState OnGetState()
        {
            return this.State;
        }



        public void OnStartEditLayer(ESRI.ArcGIS.Schematic.ISchematicLayer schLayer)
        {
            if (schLayer != null && this.State == ESRI.ArcGIS.Desktop.AddIns.ExtensionState.Enabled) 
             Updatecontainers(schLayer);
        }



        public void OnStopEditLayer(ESRI.ArcGIS.Schematic.ISchematicLayer schLayer)
        {
            if (schLayer != null && this.State == ESRI.ArcGIS.Desktop.AddIns.ExtensionState.Enabled)
                Updatecontainers(schLayer);
        }



        // Privates methods
        private void ResetEvents(bool bAdd)
        {
            // make sure the extension is turned on
            if (s_extension == null || m_DatasetMgr == null ||this.State == ESRI.ArcGIS.Desktop.AddIns.ExtensionState.Unavailable)
                return;

            // Reset event handlers
            if (bAdd && this.State == ESRI.ArcGIS.Desktop.AddIns.ExtensionState.Enabled)
            {
                m_DatasetMgr.OnStartEditLayer += new ISchematicDatasetEvents_OnStartEditLayerEventHandler(OnStartEditLayer);
                m_DatasetMgr.OnStopEditLayer += new ISchematicDatasetEvents_OnStopEditLayerEventHandler(OnStopEditLayer);
            }
            else
            {
                m_DatasetMgr.OnStartEditLayer -= OnStartEditLayer;
                m_DatasetMgr.OnStopEditLayer -= OnStopEditLayer;
            }

            // Process the opened schematic layers in the maps
            ProcessMaps();
        }

        private void ProcessMaps()
        {
            IMaps Maps = ArcMap.Document.Maps;

            // get the Maps
            long lNbMaps = Maps.Count;
            int i = 0;

            for (i = 0; i < lNbMaps; i++)
            {
                IMap Map = Maps.get_Item(i);
                if (Map == null) continue;

                // browse its layers for a schematic layer
                IEnumLayer Layers = Map.get_Layers(null, false);
                ILayer Layer = null;

                Layers.Reset();
                while ((Layer = Layers.Next()) != null)
                {
                    ISchematicLayer schLayer = (ISchematicLayer)Layer;
                    Updatecontainers(schLayer);
                }
            }
        }


        private void Updatecontainers(ESRI.ArcGIS.Schematic.ISchematicLayer schLayer)
        {
            if (schLayer == null) return;

            // get the inMemorydiagram if any
            ISchematicInMemoryDiagram inMemoryDiagram;
            inMemoryDiagram = schLayer.SchematicInMemoryDiagram;
            if (inMemoryDiagram == null) return;

            bool bCreate = false;

            // create or remove relations between containers and their contents
            if (this.State == ESRI.ArcGIS.Desktop.AddIns.ExtensionState.Enabled) bCreate = true;

            ISchematicElementClass schemContainerClass = null;
            ISchematicElementClass schemElementClass;
            ISchematicElementClass schemStationClass = null;
            IEnumSchematicInMemoryFeature enumElementsInContainer;
            IEnumSchematicInMemoryFeature enumContainerElements;
            ISchematicInMemoryFeature schemFeature = null;
            ISchematicInMemoryFeature schemContainerFeature = null;
            object feederOID;
            string containerNameID;
            IEnumSchematicElementClass enumElementClass;
            ISchematicAttributeContainer schemAttributeContainer;
            ISchematicAttribute schemAttributeRelatedFeeder;
            //ISchematicElement schemElement ;
            ISchematicRelationController schemRelationController = null;
            ISchematicRelationControllerEdit schemRelationControllerEdit = null;
            Collection colContElem = new Collection();

            // Getting SchematicFeature Class Stations and Containers
            enumElementClass = inMemoryDiagram.SchematicDiagramClass.AssociatedSchematicElementClasses;
            enumElementClass.Reset();
            schemElementClass = enumElementClass.Next();

            while (schemElementClass != null)
            {
                if (schemElementClass.Name == "Stations")
                    schemStationClass = schemElementClass;
                if (schemElementClass.Name == "Containers")
                    schemContainerClass = schemElementClass;
                if (schemStationClass != null && schemContainerClass != null)
                    break;

                schemElementClass = enumElementClass.Next();
            }
            // go out if schemStationClass or schemContainerClass are null
            if (schemStationClass == null || schemContainerClass == null)
                return;

            // Getting the Stations elements that will be displayed in the containers
            enumElementsInContainer = inMemoryDiagram.GetSchematicInMemoryFeaturesByClass(schemStationClass);
            if (enumElementsInContainer == null)
                return;

            // Creating the Schematic Container Manager
            schemRelationController = new SchematicRelationController();

            // Creating the Schematic Container Editor that will be used to define the relation between the stations and their container
            schemRelationControllerEdit = (ISchematicRelationControllerEdit)schemRelationController;

            // Defining each Container element as a schematic container
            enumContainerElements = inMemoryDiagram.GetSchematicInMemoryFeaturesByClass(schemContainerClass);

            // Add Container Element to a collection
            enumContainerElements.Reset();
            schemContainerFeature = enumContainerElements.Next();
            while (schemContainerFeature != null)
            {
                colContElem.Add(schemContainerFeature, schemContainerFeature.Name, null, null);
                schemContainerFeature = enumContainerElements.Next();
            }

            // Setting the relation between each station and its related container
            enumElementsInContainer.Reset();
            schemFeature = enumElementsInContainer.Next();

            while (schemFeature != null)
            {
                // The relation is specified by the RelatedFeeder attribute value defined for each station
                schemAttributeContainer = (ISchematicAttributeContainer)schemFeature.SchematicElementClass;
                schemAttributeRelatedFeeder = schemAttributeContainer.GetSchematicAttribute("RelatedFeeder", false);

                feederOID = schemAttributeRelatedFeeder.GetValue((ISchematicObject)schemFeature);

                if (feederOID != null)
                {
                    containerNameID = "Container-" + feederOID.ToString();

                    try
                    {
                        // Retrieve Container Element in the collection
                        schemContainerFeature = (ISchematicInMemoryFeature)colContElem[containerNameID];

                        if (bCreate)
                            schemRelationControllerEdit.CreateRelation(schemFeature, schemContainerFeature); // Create relation
                        else
                            schemRelationControllerEdit.DeleteRelation(schemFeature); // delete child relation
                    }
                    catch { }
                }

                schemContainerFeature = null;
                schemFeature = enumElementsInContainer.Next();
            }

            if (!bCreate)
            {
                // Force container geometry
                enumContainerElements.Reset();
                while ((schemContainerFeature = enumContainerElements.Next()) != null)
                {
                    try
                    {
                        // set an empty geometry 
                        // container does not have content at this stage
                        Polygon emptyRectangle = new Polygon();
                        IGeometry ContainerGeometry = (IGeometry)emptyRectangle;
                        schemContainerFeature.Shape = ContainerGeometry;
                    }
                    catch { }
                }
            }

            IActiveView activeView = (IActiveView)ArcMap.Document.FocusMap;
            if (activeView != null)
            {
                
                activeView.ContentsChanged();
                activeView.Refresh();
            }
        }
    }
}
