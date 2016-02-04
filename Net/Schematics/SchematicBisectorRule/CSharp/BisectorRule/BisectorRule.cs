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
using ESRI.ArcGIS;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Schematic;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.esriSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace CustomRulesCS
{
    [ClassInterface(ClassInterfaceType.None)]
    [Guid(BisectorRule.GUID)]
    [ProgId(BisectorRule.PROGID)]
    public class BisectorRule : ISchematicRule, ISchematicRuleDesign
    {
        public const string GUID = "31C4B848-34AF-4369-97FA-CDD985BF0592";
        public const string PROGID = "CustomRulesCS.BisectorRule";

        // Register/unregister categories for this class
        #region "Component Category Registration"
        [System.Runtime.InteropServices.ComRegisterFunction()]
        public static void Register(string CLSID)
        {
            SchematicRules.Register(CLSID);
        }

        [System.Runtime.InteropServices.ComUnregisterFunction()]
        public static void Unregister(string CLSID)
        {
            SchematicRules.Unregister(CLSID);
        }
        #endregion

        private ISchematicDiagramClass m_diagramClass;
        private double m_distance = 0.5;
        private string m_description = "Bisector Rule C#";
        private string m_parentNodeClassName;
        private string m_targetNodeClassName;
        private string m_targetLinkClassName;

        private ISchematicInMemoryFeatureClass m_parentNodeClass;
        private ISchematicInMemoryFeatureClass m_targetNodeClass;
        private ISchematicInMemoryFeatureClass m_targetLinkClass;
        private const string Separator = "_";
        private const string extensionName = "BisectorRuleCS";


        public BisectorRule()
        {
        }

        ~BisectorRule()
        {
            m_diagramClass = null;
            m_parentNodeClass = null;
            m_targetNodeClass = null;
            m_targetLinkClass = null;
        }

        public double distance
        {
            get
            {
                return m_distance;
            }
            set
            {
                m_distance = value;
            }
        }

        public string parentNodeClassName
        {
            get
            {
                return m_parentNodeClassName;
            }
            set
            {
                m_parentNodeClassName = value;
            }
        }

        public string targetNodeClassName
        {
            get
            {
                return m_targetNodeClassName;
            }
            set
            {
                m_targetNodeClassName = value;
            }
        }

        public string targetLinkClassName
        {
            get
            {
                return m_targetLinkClassName;
            }
            set
            {
                m_targetLinkClassName = value;
            }
        }

        private string GetUniqueName(ISchematicInMemoryDiagram inMemoryDiagram, esriSchematicElementType elementType, string featureName)
        {
            string nameUnique = featureName;

            ISchematicInMemoryFeature schInMemoryfeature = null;
            int index = 1;
            bool endWhile = false;
            while (!endWhile)
            {
                schInMemoryfeature = inMemoryDiagram.GetSchematicInMemoryFeatureByType(elementType, nameUnique);
                if (schInMemoryfeature == null)
                {
                    endWhile = true;
                }
                else if (schInMemoryfeature.Displayed)
                {
                    nameUnique = nameUnique + index.ToString();
                    index++;
                }
                else
                {
                    endWhile = true;
                }
            }

            return nameUnique;
        }

        private void AddNodesDegreeTwo(IEnumSchematicInMemoryFeature enumInMemoryFeature,
                Dictionary<string, ISchematicInMemoryFeature> colSchfeatureNode, ISchematicRulesHelper rulesHelper)
        {
            ISchematicInMemoryFeature schInMemoryfeature;

            if (enumInMemoryFeature == null || colSchfeatureNode == null || rulesHelper == null) 
                return;

            enumInMemoryFeature.Reset();
            schInMemoryfeature = enumInMemoryFeature.Next();
            while (schInMemoryfeature != null)
            {
                if (schInMemoryfeature.Displayed)
                {
                    IEnumSchematicInMemoryFeature enumLinks = rulesHelper.GetDisplayedIncidentLinks((ISchematicInMemoryFeatureNode)schInMemoryfeature, esriSchematicEndPointType.esriSchematicOriginOrExtremityNode);
                    if (enumLinks != null && enumLinks.Count == 2)
                    {
                        if (!colSchfeatureNode.ContainsKey(schInMemoryfeature.Name))
                            colSchfeatureNode.Add(schInMemoryfeature.Name, schInMemoryfeature);
                    }
                }

                schInMemoryfeature = enumInMemoryFeature.Next();
            }
        }

        private ISchematicInMemoryFeatureClass GetSchematicInMemoryFeatureClass(ISchematicInMemoryDiagram inMemoryDiagram, ISchematicElementClass eltClass)
        {
            ISchematicInMemoryFeatureClassContainer SchInMemoryFeatureClassContainer = (ISchematicInMemoryFeatureClassContainer)inMemoryDiagram;
            return SchInMemoryFeatureClassContainer.GetSchematicInMemoryFeatureClass(eltClass);
        }

        private double CalculateAngle(IPoint pointFrom, IPoint pointTo)
        {
            const double radToDegre = 180 / Math.PI;
            double dX, dY, angle;
            angle = 0;

            dX = pointTo.X - pointFrom.X;
            dY = pointTo.Y - pointFrom.Y;

            if (dX != 0)
                angle = Math.Atan(dY / dX) * radToDegre;
            else // case 2 points are same abcisse
            {
                if (dY < 0)
                {
                    angle = 270;
                    return angle;
                }
                else
                {
                    angle = 90;
                    return angle;
                }
            }

            if (dX < 0)
                angle = angle + 180;

            if (angle < 0)
                angle = angle + 360;

            return angle;
        }

        private double CalculateAngleBisector(double angle1, double angle2)
        {
            double angleFinal;
            if (Math.Abs(angle1 - angle2) > 180)
                angleFinal = (angle1 + angle2) / 2 + 180;
            else
                angleFinal = (angle1 + angle2) / 2;

            return angleFinal;
        }

        private IPoint GetCoordPointBisector(IPoint pointOrigine, double degreeBiSectore, double distance)
        {
            IPoint pointBisector = new PointClass();
            double angleBisector, degreeFinal;
            double dX, dY;
            int casAngle = 0;
            if (degreeBiSectore <= 90)
            {
                degreeFinal = degreeBiSectore;
                casAngle = 1;

            }
            else if (degreeBiSectore <= 180)
            {
                degreeFinal = 180 - degreeBiSectore;
                casAngle = 2;
            }
            else if (degreeBiSectore <= 270)
            {
                degreeFinal = degreeBiSectore - 180;
                casAngle = 3;
            }
            else
            {
                degreeFinal = 360 - degreeBiSectore;
                casAngle = 4;
            }

            angleBisector = Math.PI * degreeFinal / 180.0;

            dY = distance * Math.Sin(angleBisector);
            dX = distance * Math.Cos(angleBisector);

            switch (casAngle)
            {
                case 1: // case which the vector is on the first of the circle
                    pointBisector.X = pointOrigine.X + dX;
                    pointBisector.Y = pointOrigine.Y + dY;
                    break;
                case 2: // case which the vector is on the second of the circle
                    pointBisector.X = pointOrigine.X - dX;
                    pointBisector.Y = pointOrigine.Y + dY;
                    break;
                case 3: // case which the vector is on the thirst of the circle
                    pointBisector.X = pointOrigine.X - dX;
                    pointBisector.Y = pointOrigine.Y - dY;
                    break;
                default: // case which the vector is on the fourth of the circle
                    pointBisector.X = pointOrigine.X + dX;
                    pointBisector.Y = pointOrigine.Y - dY;
                    break;
            }

            return pointBisector;
        }

        #region ISchematicRule Members

        public void Alter(ISchematicDiagramClass schematicDiagramClass, ESRI.ArcGIS.esriSystem.IPropertySet propertySet)
        {
            m_diagramClass = schematicDiagramClass;

            try
            {
                m_description = propertySet.GetProperty("DESCRIPTION").ToString();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message, "property DESCRIPTION");
            }

            try
            {
                m_parentNodeClassName = propertySet.GetProperty("PARENTNODECLASS").ToString();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message, "property PARENTNODECLASS");
            }
            try
            {
                m_targetNodeClassName = propertySet.GetProperty("TARGETNODECLASS").ToString();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message, "property TARGETNODECLASS");
            }

            try
            {
                m_targetLinkClassName = propertySet.GetProperty("TARGETLINKCLASS").ToString();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message, "property TARGETLINKCLASS");
            }

            try
            {
                m_distance = (double)propertySet.GetProperty("DISTANCE");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message, "property DISTANCE");
            }

        }

        public void Apply(ISchematicInMemoryDiagram inMemoryDiagram, ESRI.ArcGIS.esriSystem.ITrackCancel cancelTracker)
        {
            ISchematicRulesHelper rulesHelper = new SchematicRulesHelperClass();
            System.Collections.Generic.Dictionary<string, ISchematicInMemoryFeature> colSchfeatureNode = new Dictionary<string, ISchematicInMemoryFeature>();
            rulesHelper.InitHelper(inMemoryDiagram);
            rulesHelper.KeepVertices = true;

            ISchematicDiagramClass diagramClass = null;
            ISchematicElementClass elementClass;
            ISchematicElementClass elementClassParentNode = null;
            IEnumSchematicElementClass enumSchEltCls;

            try
            {
                diagramClass = inMemoryDiagram.SchematicDiagramClass;
            }
            catch { }

            if (diagramClass == null) return;

            enumSchEltCls = diagramClass.AssociatedSchematicElementClasses;

            if (enumSchEltCls.Count == 0) return;

            enumSchEltCls.Reset();
            elementClass = enumSchEltCls.Next();

            while (elementClass != null)
            {
                if (elementClass.Name == m_parentNodeClassName)
                {
                    elementClassParentNode = elementClass;
                    m_parentNodeClass = GetSchematicInMemoryFeatureClass(inMemoryDiagram, elementClass);
                }

                if (elementClass.Name == m_targetNodeClassName)
                {
                    m_targetNodeClass = GetSchematicInMemoryFeatureClass(inMemoryDiagram, elementClass);
                }

                if (elementClass.Name == m_targetLinkClassName)
                {
                    m_targetLinkClass = GetSchematicInMemoryFeatureClass(inMemoryDiagram, elementClass);
                }

                elementClass = enumSchEltCls.Next();
            }

            if (m_parentNodeClass == null || m_targetNodeClass == null || m_targetLinkClass == null)
                return;

            IEnumSchematicInMemoryFeature enumSchematicInMemoryFeature;
            // list nodes degree two
            // get all feature of parent node class
            enumSchematicInMemoryFeature = inMemoryDiagram.GetSchematicInMemoryFeaturesByClass(elementClassParentNode);
            enumSchematicInMemoryFeature.Reset();

            // add the node into collection if it contains only 2 links displayed.
            AddNodesDegreeTwo(enumSchematicInMemoryFeature, colSchfeatureNode, rulesHelper);
            ISchematicInMemoryFeature schFeatureParent;

            foreach (KeyValuePair<string, ISchematicInMemoryFeature> kvp in colSchfeatureNode)
            {
                schFeatureParent = colSchfeatureNode[kvp.Key];

                if (schFeatureParent == null)
                    continue;

                // get 2 links connected of eache feature node
                IEnumSchematicInMemoryFeature enumLinks = rulesHelper.GetDisplayedIncidentLinks((ISchematicInMemoryFeatureNode)schFeatureParent, esriSchematicEndPointType.esriSchematicOriginOrExtremityNode);
                // enumLinks surely not null    and it contain 2 links displayed

                double angle1, angle2, angleBisector;
                bool first = true;

                angle1 = angle2 = angleBisector = 0;
                IPoint pointParent = null;
                ISchematicInMemoryFeatureNodeGeometry geoParent;
                geoParent = (ISchematicInMemoryFeatureNodeGeometry)schFeatureParent;

                pointParent = geoParent.InitialPosition;
                IPoint pointSon = null;
                bool enableCalculate = true;

                ISchematicInMemoryFeature schInMemoryFeature = enumLinks.Next();

                ISchematicInMemoryFeatureLink schInMemoryLink = (ISchematicInMemoryFeatureLink)schInMemoryFeature;
                while (schInMemoryLink != null)
                {
                    ISchematicInMemoryFeatureNodeGeometry nodeGeo;
                    // get angle of 2 links connected
                    if (schInMemoryLink.FromNode.Name == schFeatureParent.Name)
                        nodeGeo = (ISchematicInMemoryFeatureNodeGeometry)schInMemoryLink.ToNode;
                    else
                        nodeGeo = (ISchematicInMemoryFeatureNodeGeometry)schInMemoryLink.FromNode;

                    if (nodeGeo == null)
                    {
                        enableCalculate = false;
                        break;
                    }

                    pointSon = nodeGeo.InitialPosition;
                    if (first)
                    {
                        angle1 = CalculateAngle(pointParent, pointSon);
                        first = false;
                    }
                    else
                        angle2 = CalculateAngle(pointParent, pointSon);

                    schInMemoryFeature = enumLinks.Next();
                    schInMemoryLink = (ISchematicInMemoryFeatureLink)schInMemoryFeature;
                }

                // caculate angle bisector
                if (enableCalculate)
                    angleBisector = CalculateAngleBisector(angle1, angle2);
                else
                    continue;

                // construct a geometry for the new node node
                // now call alterNode to create a new schematic feature
                // construct a correct name
                string uniqueNodeName, featureCreateName;
                featureCreateName = schFeatureParent.Name + Separator + extensionName;
                esriSchematicElementType elementType = esriSchematicElementType.esriSchematicNodeType;
                uniqueNodeName = GetUniqueName(inMemoryDiagram, elementType, featureCreateName);
                IWorkspace workspace = null;

                try
                {
                    workspace = inMemoryDiagram.SchematicDiagramClass.SchematicDataset.SchematicWorkspace.Workspace;
                }
                catch { }

                int datasourceID = -1;
                
                if(workspace != null)
                    datasourceID = rulesHelper.FindDataSourceID(workspace, false);

                if(datasourceID == -1)
                    datasourceID = m_diagramClass.SchematicDataset.DefaultSchematicDataSource.ID;

                ISchematicInMemoryFeature schFeatureNodeCreate = null;
                IPoint pointBisector = null;
                pointBisector = GetCoordPointBisector(pointParent, angleBisector, m_distance);
                try
                {
                    schFeatureNodeCreate = rulesHelper.AlterNode(m_targetNodeClass, uniqueNodeName, null, (IGeometry)pointBisector, datasourceID, 0);
                }
                catch(Exception ex)
                {
                    System.Diagnostics.Trace.WriteLine(ex.Message, "Impossible to create a feature Node");
                }

                // now construct a unique link name
                string linkName = schFeatureParent.Name + Separator + uniqueNodeName;
                string uniqueLinkName;

                elementType = esriSchematicElementType.esriSchematicLinkType;
                uniqueLinkName = GetUniqueName(inMemoryDiagram, elementType, linkName);
                // construct a link
                ISchematicInMemoryFeature schFeatureLinkCreate = null;
                try
                {
                    schFeatureLinkCreate = rulesHelper.AlterLink(m_targetLinkClass, uniqueLinkName, null, null, datasourceID, 0, schFeatureParent.Name, uniqueNodeName, esriFlowDirection.esriFDWithFlow, 0, 0);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Trace.WriteLine(ex.Message, "Impossible to create a feature link");
                }
            }

            if (colSchfeatureNode.Count > 0)
                colSchfeatureNode.Clear();

            colSchfeatureNode = null;
            rulesHelper = null;
        }

        public ESRI.ArcGIS.esriSystem.UID ClassID
        {
            get
            {
                ESRI.ArcGIS.esriSystem.UID ruleID = new ESRI.ArcGIS.esriSystem.UID();
                ruleID.Value = PROGID;
                return ruleID;
            }
        }

        public string Description
        {
            get
            {
                return m_description;
            }
            set
            {
                m_description = value;
            }
        }

        string ISchematicRule.Description
        {
            get { return m_description; }
        }

        public string Name
        {
            get
            {
                return "Bisector Rule C#";
            }
        }

        public ESRI.ArcGIS.esriSystem.IPropertySet PropertySet
        {
            get
            {
                ESRI.ArcGIS.esriSystem.IPropertySet propertySet = new ESRI.ArcGIS.esriSystem.PropertySet();
                propertySet.SetProperty("DESCRIPTION", m_description);
                propertySet.SetProperty("PARENTNODECLASS", m_parentNodeClassName);
                propertySet.SetProperty("TARGETNODECLASS", m_targetNodeClassName);
                propertySet.SetProperty("TARGETLINKCLASS", m_targetLinkClassName);
                propertySet.SetProperty("DISTANCE", m_distance);

                return propertySet;
            }
        }

        ISchematicDiagramClass ISchematicRule.SchematicDiagramClass
        {
            get { return m_diagramClass; }
        }

        #endregion

        #region ISchematicRuleDesign Members

        public void Detach()
        {
            m_diagramClass = null;
            m_parentNodeClass = null;
            m_targetNodeClass = null;
            m_targetLinkClass = null;
        }

        ESRI.ArcGIS.esriSystem.IPropertySet ISchematicRuleDesign.PropertySet
        {
            set
            {
                m_description = value.GetProperty("DESCRIPTION").ToString();
                m_parentNodeClassName = value.GetProperty("PARENTNODECLASS").ToString();
                m_targetNodeClassName = value.GetProperty("TARGETNODECLASS").ToString();
                m_targetLinkClassName = value.GetProperty("TARGETLINKCLASS").ToString();
                m_distance = (double)value.GetProperty("DISTANCE");
            }
        }

        ISchematicDiagramClass ISchematicRuleDesign.SchematicDiagramClass
        {
            get
            {
                return m_diagramClass;
            }
            set
            {
                m_diagramClass = value;
            }
        }
        #endregion ISchematicRuleDesign


    }
}
