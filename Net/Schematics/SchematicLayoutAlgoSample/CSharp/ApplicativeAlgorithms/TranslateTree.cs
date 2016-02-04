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
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Schematic;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace ApplicativeAlgorithmsCS
{
    [ClassInterface(ClassInterfaceType.None)]
    [Guid(TranslateTree.GUID)]
    [ProgId(TranslateTree.PROGID)]
    public class TranslateTree : ISchematicAlgorithm, ISchematicJSONParameters, ITranslateTree
    {
        // private member data
        public const string GUID = "A675D260-6AF0-438d-80AB-630B3F956D05";
        private const string PROGID = "ApplicativeAlgorithms.TranslateTree";

        // property names (for the algorithm property set)
        private const string TranslationFactorXName = "Translation Factor X";
        private const string TranslationFactorYName = "Translation Factor Y";

        // the JSON parameter names 
        private const string JSONTranslationFactorX = "TranslationFactorX";
        private const string JSONTranslationFactorY = "TranslationFactorY";

        // Algorithms parameters JSON representation Names used by the REST interface 
        private const string JSONName = "name";
        private const string JSONType = "type";
        private const string JSONValue = "value";
        // Algorithms parameters JSON representation Types used by the REST interface
        private const string JSONLong = "Long";
        private const string JSONDouble = "Double";
        private const string JSONBoolean = "Boolean";
        private const string JSONString = "String";



        private string m_algoLabel = "Translate Tree C#";

        private bool m_available;
        private bool m_overridable;
        private bool m_useRootNode;
        private bool m_useEndNode;

        private double m_paramX;
        private double m_paramY;

        private ISchematicDiagramClassName m_schematicDiagramClassName;

        public TranslateTree()
        {
            m_paramX = 50.0;
            m_paramY = 50.0;
            m_available = true;// In this example, the algorithm is available by default
            m_overridable = true; // user is allowed to edit the parameters
            m_useRootNode = false; // don't need the user to define root nodes
            m_useEndNode = false; // don't need the user to define an end node

            m_schematicDiagramClassName = null;

        }

        ~TranslateTree()
        {
            m_schematicDiagramClassName = null;
        }


        #region Component Category Registration

        [ComRegisterFunction()]
        public static void Reg(string regKey)
        {
            SchematicAlgorithms.Register(regKey);
        }

        [ComUnregisterFunction()]
        public static void Unreg(string regKey)
        {
            SchematicAlgorithms.Unregister(regKey);
        }
        #endregion


        #region Implements ITranslateTree
        public double TranslationFactorX
        {
            get
            {
                return m_paramX;
            }
            set
            {
                m_paramX = value;
            }
        }

        public double TranslationFactorY
        {
            get
            {
                return m_paramY;
            }
            set
            {
                m_paramY = value;
            }
        }
        #endregion
        ///////////////////////////////////////////////////////////////////////////////////////
        //
        // ISchematicAlgorithm interface : Defines its properties and methods (mandatory)
        //
        #region Implements ISchematicAlgorithm


        public bool get_Enabled(ISchematicLayer schematicLayer)
        {
            if (schematicLayer == null)
                return false;

            // an algorithm needs the diagram to be in editing mode in order to run
            if (!schematicLayer.IsEditingSchematicDiagram())
                return false;

            IEnumSchematicFeature enumFeatures = schematicLayer.GetSchematicSelectedFeatures(true);
            if (enumFeatures == null)
                return false;

            // Count the selected nodes
            int iCount = 0;
            ISchematicFeature feature;
            enumFeatures.Reset();
            feature = enumFeatures.Next();
            while (feature != null && iCount < 2)
            {
                ISchematicInMemoryFeatureClass inMemoryFeatureClass;

                // just want SchematicFeatureNode
                inMemoryFeatureClass = (ISchematicInMemoryFeatureClass)feature.Class;

                if (inMemoryFeatureClass.SchematicElementClass.SchematicElementType == esriSchematicElementType.esriSchematicNodeType)
                    iCount++;
                feature = enumFeatures.Next();
            }

            if (iCount == 1)
                return true; // just want one selected node
            else
                return false;
        }

        public bool Available
        {
            get
            {
                return m_available;
            }
            set
            {
                m_available = value;
            }
        }

        public bool Overridable
        {
            get
            {
                return m_overridable;
            }
            set
            {
                m_overridable = value;
            }
        }

        public ISchematicDiagramClassName SchematicDiagramClassName
        {
            get
            {
                return m_schematicDiagramClassName;
            }
            set
            {
                m_schematicDiagramClassName = value;
            }
        }

        public string Label
        {
            get
            {
                return m_algoLabel;
            }
            set
            {
                m_algoLabel = value;
            }
        }

        public bool UseRootNode
        {
            get
            {
                return m_useRootNode;
            }
        }

        public bool UseEndNode
        {
            get
            {
                return m_useEndNode;
            }
        }

        public IPropertySet PropertySet
        {
            get
            {
                // build the property set
                IPropertySet propSet = new PropertySet();
                if (propSet == null)
                    return null;

                propSet.SetProperty(TranslationFactorXName, m_paramX);
                propSet.SetProperty(TranslationFactorYName, m_paramY);

                return propSet;
            }
            set
            {
                IPropertySet pPropertySet = value;

                if (pPropertySet != null)
                {
                    try
                    {
                        m_paramX = (double)pPropertySet.GetProperty(TranslationFactorXName);
                        m_paramY = (double)pPropertySet.GetProperty(TranslationFactorYName);
                    }
                    catch { }
                }
            }
        }

        public string AlgorithmCLSID
        {
            get
            {
                //return "{" + GUID + "}"; Working as well with GUID
                return PROGID;
            }
        }

        // The execute part of the algorithm
        public void Execute(ISchematicLayer schematicLayer, ITrackCancel CancelTracker)
        {
            if (schematicLayer == null)
                return;

            // Before Execute part
            ISchematicInMemoryDiagram inMemoryDiagram;
            inMemoryDiagram = schematicLayer.SchematicInMemoryDiagram;

            // Core algorithm
            InternalExecute(schematicLayer, inMemoryDiagram, CancelTracker);

            // Release the COM objects
            if (inMemoryDiagram != null)
                while (System.Runtime.InteropServices.Marshal.ReleaseComObject(inMemoryDiagram) > 0) { }

            while (System.Runtime.InteropServices.Marshal.ReleaseComObject(schematicLayer) > 0) { }
        }



        // The execute part of the algorithm
        private void InternalExecute(ISchematicLayer schematicLayer, ISchematicInMemoryDiagram inMemoryDiagram, ITrackCancel CancelTracker)
        {
            if (schematicLayer == null || inMemoryDiagram == null)
                return;

            // get the diagram spatial reference for geometry transformation
            IGeoDataset geoDataset = (IGeoDataset)inMemoryDiagram;
            if (geoDataset == null)
                return;

            ISpatialReference spatialReference = geoDataset.SpatialReference;

            ISchematicDiagramClass diagramClass;
            diagramClass = inMemoryDiagram.SchematicDiagramClass;
            if (diagramClass == null)
                return;

            ISchematicDataset schemDataset;
            schemDataset = diagramClass.SchematicDataset;
            if (schemDataset == null)
                return;

            ISchematicAlgorithmEventsTrigger algorithmEventsTrigger;
            algorithmEventsTrigger = (ISchematicAlgorithmEventsTrigger)schemDataset;
            if (algorithmEventsTrigger == null)
                return;

            ESRI.ArcGIS.Carto.ILayer layer = (ESRI.ArcGIS.Carto.ILayer)schematicLayer;
            ISchematicAlgorithm algorithm = (ISchematicAlgorithm)this;

            bool canExecute = true;
            algorithmEventsTrigger.FireBeforeExecuteAlgorithm(layer, algorithm, ref canExecute);
            if (!canExecute)
                return; // cannot execute

            // Get the selected Features
            IEnumSchematicFeature enumFeatures = schematicLayer.GetSchematicSelectedFeatures(true);
            if (enumFeatures == null)
                return;

            // Count the selected nodes
            ISchematicInMemoryFeatureClass inMemoryFeatureClass;
            ISchematicFeature selectedFeature = null;
            int iCount = 0;
            ISchematicFeature schemFeature;
            enumFeatures.Reset();
            schemFeature = enumFeatures.Next();
            while (schemFeature != null && iCount < 2)
            {
                // just want SchematicFeatureNode
                inMemoryFeatureClass = (ISchematicInMemoryFeatureClass)schemFeature.Class;

                if (inMemoryFeatureClass.SchematicElementClass.SchematicElementType == esriSchematicElementType.esriSchematicNodeType)
                {
                    selectedFeature = schemFeature;
                    iCount++;
                }
                schemFeature = enumFeatures.Next();
            }

            if (iCount != 1 || selectedFeature == null)
                return; // must be only one

            // Create a new SchematicAnalystFindConnected algorithm
            ISchematicAnalystFindConnected analystFindConnected = null;

            analystFindConnected = (ISchematicAnalystFindConnected)new SchematicAnalystFindConnected();
            if (analystFindConnected == null)
                return;

            // Modifying parameters value for this SchematicAnalystFindConnected algorithm so that when it is launched the trace result appears a selection set{
            analystFindConnected.SelectLink = true;
            analystFindConnected.SelectNode = true;
            analystFindConnected.UseFlow = false;
            //pAnalystFindConnected.FlowDirection = 1;
            // Execute the algorithm
            analystFindConnected.Execute(schematicLayer, CancelTracker);

            // Retrieving the trace result (if any)
            IEnumSchematicFeature resultFeatures;
            resultFeatures = analystFindConnected.TraceResult;

            // free the schematic analyst COM object
            while (System.Runtime.InteropServices.Marshal.ReleaseComObject(analystFindConnected) > 0) { }

            if (resultFeatures == null || resultFeatures.Count < 1)
                return;

            // Apply the translation to the result
            //ISchematicInMemoryDiagram inMemoryDiagram;
            //inMemoryDiagram = schematicLayer.SchematicInMemoryDiagram;

            // Translating each traced elements according to the TranslationFactorX and TranslationFactorY parameters current values
            ISchematicInMemoryFeature inMemoryFeature;
            resultFeatures.Reset();
            while ((inMemoryFeature = (ISchematicInMemoryFeature)resultFeatures.Next()) != null)
            {
                IGeometry geometry;
                ITransform2D transform;
                esriSchematicElementType elemType;

                inMemoryFeatureClass = (ISchematicInMemoryFeatureClass)inMemoryFeature.Class;
                elemType = inMemoryFeatureClass.SchematicElementClass.SchematicElementType;
                if (elemType == esriSchematicElementType.esriSchematicLinkType || elemType == esriSchematicElementType.esriSchematicNodeType)
                {
                    // get a copy of the feature geometry
                    // then process the cloned geometry rather than the feature geometry directly
                    // Thus the modifications are stored in the heap of the current operation
                    // meaning it can be undone then redo (undo/redo)
                    geometry = inMemoryFeature.ShapeCopy;
                    // Convert the geometry into the SpatialReference of diagram class
                    geometry.Project(spatialReference);
                    // Move the geometry
                    transform = (ITransform2D)geometry;
                    if (transform != null)
                    {
                        transform.Move(m_paramX, m_paramY);

                        // Convert the moved geometry into the spatial reference of storage
                        // and feed it back to the feature
                        IObjectClass table = inMemoryFeature.Class;
                        if (table == null)
                            continue;

                        IGeoDataset featureGeoDataset = (IGeoDataset)table;
                        if (featureGeoDataset == null)
                            continue;

                        ISpatialReference featureSpatialRef = featureGeoDataset.SpatialReference;
                        if (featureSpatialRef == null)
                            continue;

                        IGeometry movedGeometry = (IGeometry)transform;
                        movedGeometry.Project(featureSpatialRef);


                        inMemoryFeature.Shape = movedGeometry;
                    }
                }
            }

            // After Execute part
            algorithmEventsTrigger.FireAfterExecuteAlgorithm(layer, algorithm);

            // update the diagram extent
            schematicLayer.UpdateExtent();
        }

         
        #endregion

        ///////////////////////////////////////////////////////////////////////////////////////
        //
        // ISchematicJSONParameters interface : Defines its properties and methods (mandatory to run on server)
        //
        #region Implements ISchematicJSONParameters 

        public IJSONArray JSONParametersArray
        {
            get
            {

                JSONArray aJSONArray = new JSONArray();

                // build JSON object for the first parameter
                IJSONObject oJSONObject1 = new JSONObject();

                oJSONObject1.AddString(JSONName, JSONTranslationFactorX);
                oJSONObject1.AddString(JSONType, JSONDouble);
                oJSONObject1.AddDouble(JSONValue, m_paramX);

                aJSONArray.AddJSONObject(oJSONObject1);

                // build JSON object for the second parameter
                IJSONObject oJSONObject2 = new JSONObject();

                oJSONObject2.AddString(JSONName, JSONTranslationFactorY);
                oJSONObject2.AddString(JSONType, JSONDouble);
                oJSONObject2.AddDouble(JSONValue, m_paramY);

                aJSONArray.AddJSONObject(oJSONObject2);

                // encode JSON array as a string
                return aJSONArray; // null propertyset 

            }
        }


        public IJSONObject JSONParametersObject
        {
            set
            {
                IJSONObject oJSONObject = value;

                double paramX;
                double paramY;
                if (oJSONObject != null)
                {
                    // decode input parameters
                    if (oJSONObject.TryGetValueAsDouble(JSONTranslationFactorX, out paramX))
                        m_paramX = paramX; // otherwise use current value

                    if (oJSONObject.TryGetValueAsDouble(JSONTranslationFactorY, out paramY))
                        m_paramY = paramY; // otherwise use current value
                }
                
            }
        }
        #endregion
    }
}
