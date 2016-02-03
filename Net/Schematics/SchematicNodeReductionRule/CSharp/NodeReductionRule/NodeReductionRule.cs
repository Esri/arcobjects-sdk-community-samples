using ESRI.ArcGIS;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Schematic;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.esriSystem;
using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;



namespace CustomRulesCS
{
    [ClassInterface(ClassInterfaceType.None)]
    [Guid(NodeReductionRule.GUID)]
    [ProgId(NodeReductionRule.PROGID)]
    public class NodeReductionRule : ISchematicRule, ISchematicRuleDesign
    {
        public const string GUID = "A6CB9935-AE08-46FB-9850-77C2B4E7C6A9";
        public const string PROGID = "CustomRulesCS.NodeReductionRule";

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

        private string m_description = "Reduction Node Rule - Report cumulative value C#";
        private string m_lengthAttributeName;
        private string m_reducedNodeClassName;
        private string m_superspanLinkClassName;
        private string m_linkAttributeName;

        private bool m_keepVertices = true;
        private bool m_linkAttribute = false;


        #region NodeReductionRule interface
        public NodeReductionRule()
        {
        }

        ~NodeReductionRule()
        {
            m_diagramClass = null;
        }


        public bool LinkAttribute
        {
            get
            {
                return m_linkAttribute;
            }
            set
            {
                m_linkAttribute = value;
            }
        }

        public string LinkAttributeName
        {
            get
            {
                return m_linkAttributeName;
            }
            set
            {
                m_linkAttributeName = value;
            }
        }

        
        public bool KeepVertices 
        {
            get
            {
                return m_keepVertices;
            }
            set
            {
                m_keepVertices = value;
            }
        }


        public string LengthAttributeName
        {
            get
            {
                return m_lengthAttributeName;
            }
            set
            {
                m_lengthAttributeName = value;
            }
        }

        public string ReducedNodeClassName
        {
            get
            {
                return m_reducedNodeClassName;
            }
            set
            {
                m_reducedNodeClassName = value;
            }
        }


        public string SuperpanLinkClassName
        {
            get
            {
                return m_superspanLinkClassName;
            }
            set
            {
                m_superspanLinkClassName = value;
            }
        }
        #endregion

        #region ISchematicRule Members

        public void Alter(ISchematicDiagramClass schematicDiagramClass, ESRI.ArcGIS.esriSystem.IPropertySet propertySet)
        {
            m_diagramClass = schematicDiagramClass;

            try
            {
                m_description = propertySet.GetProperty("DESCRIPTION").ToString();
            }
            catch { }

            try
            {
                m_reducedNodeClassName = propertySet.GetProperty("REDUCEDNODECLASS").ToString();
            }
            catch { }

            try
            {
                m_superspanLinkClassName = propertySet.GetProperty("SUPERSPANLINKCLASS").ToString();
            }
            catch { }


            try
            {
                m_lengthAttributeName = propertySet.GetProperty("LENGTHATTRIBUTENAME").ToString();
            }
            catch { }

            try
            {
                m_keepVertices = (bool)propertySet.GetProperty("KEEPVERTICES");
            }
            catch { }


            try
            {
                m_linkAttribute = (bool)propertySet.GetProperty("LINKATTRIBUTE");
            }
            catch { }

            try
            {
                m_linkAttributeName = propertySet.GetProperty("LINKATTRIBUTENAME").ToString();
            }
            catch { }

        }
        
        public void Apply(ISchematicInMemoryDiagram inMemoryDiagram, ESRI.ArcGIS.esriSystem.ITrackCancel cancelTracker)
        {

            if (m_reducedNodeClassName == "" || inMemoryDiagram == null) return;

            // initialize the schematic rules helper
            ISchematicRulesHelper rulesHelper = new SchematicRulesHelperClass();
            rulesHelper.InitHelper(inMemoryDiagram);
            rulesHelper.KeepVertices = m_keepVertices;

            ////////////////////////
            // get the feature classes processed by the rule
            ISchematicDiagramClass diagramClass = null;
            try
            {
                diagramClass = inMemoryDiagram.SchematicDiagramClass;
            }
            catch { }

            if (diagramClass == null) return;
            ISchematicDataset schematicDataset = null;
            try
            {
                schematicDataset = diagramClass.SchematicDataset;
            }
            catch { }

            ISchematicElementClassContainer elementclassContainer = (ISchematicElementClassContainer)schematicDataset;
            if (elementclassContainer == null) return;

            ISchematicElementClass elementClassReducedNode = null;
            elementClassReducedNode = elementclassContainer.GetSchematicElementClass(m_reducedNodeClassName);

            ISchematicElementClass elementClassSuperspan    = null;
            elementClassSuperspan = elementclassContainer.GetSchematicElementClass(m_superspanLinkClassName);

            if (elementClassSuperspan == null || elementClassReducedNode == null) return;

            ISchematicInMemoryFeatureClassContainer featureClassContainer = (ISchematicInMemoryFeatureClassContainer)inMemoryDiagram;
            if (featureClassContainer == null) return;

         ISchematicInMemoryFeatureClass superspanLinkClass = featureClassContainer.GetSchematicInMemoryFeatureClass(elementClassSuperspan);
            //
            /////////////////////////

            // fetch the superspan spatial reference
            IGeoDataset geoDataset = (IGeoDataset)superspanLinkClass;

            ISpatialReference    spatialRef = null;
            if (geoDataset != null)    spatialRef = geoDataset.SpatialReference;
            if (spatialRef == null) return;

            // Retrieve the schematic in memory feature nodes to reduce 
 

            System.Collections.Generic.Dictionary<string, ISchematicInMemoryFeature> colSchfeatureNode = new Dictionary<string, ISchematicInMemoryFeature>();

            // get all feature of parent node class
            IEnumSchematicInMemoryFeature enumSchematicInMemoryFeature = inMemoryDiagram.GetSchematicInMemoryFeaturesByClass(elementClassReducedNode);

            // retain only the nodes of degree two
            RetainNodesDegreeTwo(enumSchematicInMemoryFeature, colSchfeatureNode, rulesHelper);    // there would be inserted a SQL query to also filter by attributes 

            IProgressor msgProgressor = null;
            if (cancelTracker != null)
            {
                msgProgressor = cancelTracker.Progressor;
                IStepProgressor stepProgressor = (IStepProgressor)msgProgressor;
                if (stepProgressor != null)
                {
                    stepProgressor.MinRange = 0;
                    stepProgressor.MaxRange = colSchfeatureNode.Count;
                    stepProgressor.StepValue = 1;
                    stepProgressor.Position = 0;
                    stepProgressor.Message = m_description;
                    cancelTracker.Reset();
                    cancelTracker.Progressor = msgProgressor;
                    stepProgressor.Show();
                }
            }

            ISchematicInMemoryFeature schFeatureToReduce;
            foreach (KeyValuePair<string, ISchematicInMemoryFeature> kvp in colSchfeatureNode)
            {
                if (cancelTracker != null)
                    if (cancelTracker.Continue() == false)
                        break;

                schFeatureToReduce = colSchfeatureNode[kvp.Key];
                if (schFeatureToReduce != null) ReduceNode(rulesHelper, superspanLinkClass, spatialRef, schFeatureToReduce);
            }


            // release memory
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
                return "Node Reduction Rule C#";
            }
        }

        public ESRI.ArcGIS.esriSystem.IPropertySet PropertySet
        {
            get
            {
                ESRI.ArcGIS.esriSystem.IPropertySet propertySet = new ESRI.ArcGIS.esriSystem.PropertySet();
                propertySet.SetProperty("DESCRIPTION", m_description);
                propertySet.SetProperty("REDUCEDNODECLASS", m_reducedNodeClassName);
                propertySet.SetProperty("SUPERSPANLINKCLASS", m_superspanLinkClassName);
                propertySet.SetProperty("KEEPVERTICES", m_keepVertices);
                propertySet.SetProperty("LENGTHATTRIBUTENAME", m_lengthAttributeName);
                propertySet.SetProperty("LINKATTRIBUTE", m_linkAttribute);
                propertySet.SetProperty("LINKATTRIBUTENAME", m_linkAttributeName);


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
        }

        ESRI.ArcGIS.esriSystem.IPropertySet ISchematicRuleDesign.PropertySet
        {
            set
            {
                m_description = value.GetProperty("DESCRIPTION").ToString();
                m_reducedNodeClassName = value.GetProperty("REDUCEDNODECLASS").ToString();
                m_superspanLinkClassName = value.GetProperty("SUPERSPANLINKCLASS").ToString();
                m_keepVertices = (bool)value.GetProperty("KEEPVERTICES");
                m_lengthAttributeName = value.GetProperty("LENGTHATTRIBUTENAME").ToString();
                m_linkAttribute = (bool)value.GetProperty("LINKATTRIBUTE");
                m_linkAttributeName = value.GetProperty("LINKATTRIBUTENAME").ToString();
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

        #region private methods

        
        private void ReduceNode(ISchematicRulesHelper rulesHelper, ISchematicInMemoryFeatureClass superspanLinkClass , ISpatialReference spatialRef, ISchematicInMemoryFeature schFeatureToReduce)
        {
            if (schFeatureToReduce.Displayed == false || rulesHelper == null || spatialRef == null)
                return;

            // get the two connected links 
            IEnumSchematicInMemoryFeature enumLink = rulesHelper.GetDisplayedIncidentLinks((ISchematicInMemoryFeatureNode)schFeatureToReduce, esriSchematicEndPointType.esriSchematicOriginOrExtremityNode);
            if (enumLink == null || enumLink.Count != 2)
                return;

            enumLink.Reset();
            ISchematicInMemoryFeature schFeat1 = enumLink.Next();
            ISchematicInMemoryFeature schFeat2 = enumLink.Next();
            ISchematicInMemoryFeatureLink schLink1 = (ISchematicInMemoryFeatureLink)schFeat1;
            ISchematicInMemoryFeatureLink schLink2 = (ISchematicInMemoryFeatureLink)schFeat2;
            if (schLink1 == null || schLink2 == null) return;


            ISchematicInMemoryFeature schFeatureSuperspan = null;
            ISchematicInMemoryFeature schFeatureTmp = null;

            ISchematicInMemoryFeatureNode schNodeToReduce = (ISchematicInMemoryFeatureNode)schFeatureToReduce;
            ISchematicInMemoryFeatureNode schFromNode;
            ISchematicInMemoryFeatureNode schToNode;
            int iFromPort;
            int iToPort;


            IGeometry superspanGeometry;
            if (schLink2.FromNode == schNodeToReduce)
            {
                superspanGeometry = BuildLinkGeometry(schLink1, schNodeToReduce, schLink2, rulesHelper);
                if (schLink1.ToNode == schNodeToReduce)
                {
                    schFromNode = schLink1.FromNode;
                    iFromPort = schLink1.FromPort;
                }
                else
                {
                    schFromNode = schLink1.ToNode;
                    iFromPort = schLink1.ToPort;
                }

                schToNode = schLink2.ToNode;
                iToPort = schLink2.ToPort;

            }
            else
            {
                superspanGeometry = BuildLinkGeometry(schLink2, schNodeToReduce, schLink1, rulesHelper);


                schFromNode = schLink2.FromNode;
                iFromPort = schLink2.FromPort;

                if (schLink1.FromNode == schNodeToReduce)
                {
                    schToNode = schLink1.ToNode;
                    iToPort = schLink1.ToPort;
                }
                else
                {
                    schToNode = schLink1.FromNode;
                    iToPort = schLink1.FromPort;
                }
            }

            if (superspanGeometry != null)
                superspanGeometry.SpatialReference = spatialRef;


            // find a unique name for the superspan
            string strFromName = schFromNode.Name;
            string strtoName = schToNode.Name;
            string strName;
            long lCount = 1;

            while (schFeatureSuperspan == null)
            {
                strName = strFromName + ";" + strtoName + ";" + lCount.ToString();
                if (strName.Length >= 128)
                    break; // too long a name

                try
                {
                    schFeatureTmp = rulesHelper.AlterLink(superspanLinkClass, strName, null, superspanGeometry, -2, -2,
                                                                                                            strFromName, strtoName, esriFlowDirection.esriFDWithFlow, iFromPort, iToPort);
                }
                catch
                {
                    schFeatureTmp = null;
                }

                if (schFeatureTmp == null)
                    continue;

                // valid new feature
                schFeatureSuperspan = schFeatureTmp;
            }


            // last chance for a unique name
            lCount = 1;
            while (schFeatureSuperspan == null)
            {
                strName = schNodeToReduce.Name + ";" + lCount.ToString();
                if (strName.Length >= 128)
                    break; // too long a name

                try
                {
                    schFeatureTmp = rulesHelper.AlterLink(superspanLinkClass, strName, null, superspanGeometry, -2, -2,
                                                                                                         strFromName, strtoName, esriFlowDirection.esriFDWithFlow, iFromPort, iToPort);
                }
                catch
                {
                    schFeatureTmp = null;
                }

                if (schFeatureTmp == null)
                    continue;

                // valid new feature
                schFeatureSuperspan = schFeatureTmp;
            }

            if (schFeatureSuperspan == null)
                return; // cannot find a unique name

            // otherwise report the cumulated length of the reduced links to the superspan
            ReportCumulativeValues(schFeat1, schFeat2, schFeatureSuperspan);

            //    report the associations on the superspan link
            rulesHelper.ReportAssociations(schFeatureToReduce, schFeatureSuperspan);
            rulesHelper.ReportAssociations(schFeat1, schFeatureSuperspan);
            rulesHelper.ReportAssociations(schFeat2, schFeatureSuperspan);

            // hide the reduced objects
            rulesHelper.HideFeature(schFeatureToReduce);
            rulesHelper.HideFeature(schFeat1);
            rulesHelper.HideFeature(schFeat2);
        }


        private void ReportCumulativeValues(ISchematicInMemoryFeature schFeat1, ISchematicInMemoryFeature schFeat2, ISchematicInMemoryFeature schTargetFeat)
        {

            if (schFeat1 == null || schFeat2 == null || schTargetFeat == null)
                return;

            // assume the attribute field name is the same on every schematic feature link classes
            IFields linkFields = schFeat1.Fields;
            int iIndex = linkFields.FindField(m_lengthAttributeName);
            if (iIndex < 0) return; // attribute field does not exist
            object value1 = schFeat1.get_Value(iIndex);

            linkFields = schFeat2.Fields;
            iIndex = linkFields.FindField(m_lengthAttributeName);
            if (iIndex < 0) return; // attribute field does not exist
            object value2 = schFeat2.get_Value(iIndex);

            double dValue1 = 0;
            double dValue2 = 0;

            if (!DBNull.Value.Equals(value1))
            {
                try
                {
                    dValue1 = Convert.ToDouble(value1);
                }
                catch { }
            }


            if (!DBNull.Value.Equals(value2))
            {
                try
                {
                    dValue2 = Convert.ToDouble(value2);
                }
                catch{ }
            }


            // assume the values to be numeric
            double dlength    = dValue1 + dValue2;

            linkFields = schTargetFeat.Fields;
            iIndex = linkFields.FindField(m_lengthAttributeName);
            if (iIndex < 0) return; // attribute field does not exist
            schTargetFeat.set_Value(iIndex, dlength);
        }




        private IGeometry BuildLinkGeometry(ISchematicInMemoryFeatureLink schLink1, ISchematicInMemoryFeatureNode schNodeToReduce, ISchematicInMemoryFeatureLink schLink2, ISchematicRulesHelper rulesHelper)
        {

            if (schLink1 == null || schLink2 == null || schNodeToReduce == null || rulesHelper == null)
                return null;

            if (m_keepVertices == false)
                return null; // no geometry

            Polyline newPoly = new Polyline();
            IPolyline polyLink1 = rulesHelper.GetLinkPoints(schLink1, (schLink1.FromNode == schNodeToReduce));
            IPolyline polyLink2 = rulesHelper.GetLinkPoints(schLink2, (schLink2.ToNode == schNodeToReduce));
            IPoint nodePt = rulesHelper.GetNodePoint(schNodeToReduce);
            IPoint Pt;

            IPointCollection newPts = (IPointCollection)newPoly;
            IPointCollection link1Pts = (IPointCollection)polyLink1;
            IPointCollection link2Pts = (IPointCollection)polyLink2;

            int Count = link1Pts.PointCount;
            int i;
            for (i = 0; i < Count - 1; i++)
            {
                Pt = link1Pts.get_Point(i);
                newPts.AddPoint(Pt);
            }

            newPts.AddPoint(nodePt);

            Count = link2Pts.PointCount;
            for (i = 1; i < Count; i++)
            {
                Pt = link2Pts.get_Point(i);
                newPts.AddPoint(Pt);
            }

            IGeometry buildGeometry = (IGeometry)newPoly;
            return buildGeometry;
        }

        private void RetainNodesDegreeTwo(IEnumSchematicInMemoryFeature enumInMemoryFeature,
                Dictionary<string, ISchematicInMemoryFeature> colSchfeatureNode, ISchematicRulesHelper ruleHelper)
        {
            ISchematicInMemoryFeature schInMemoryfeature;

            if (ruleHelper == null) return;

            enumInMemoryFeature.Reset();
            schInMemoryfeature = enumInMemoryFeature.Next();
            while (schInMemoryfeature != null)
            {
                if (schInMemoryfeature.Displayed)
                {
                    IEnumSchematicInMemoryFeature enumLinks = ruleHelper.GetDisplayedIncidentLinks((ISchematicInMemoryFeatureNode)schInMemoryfeature, esriSchematicEndPointType.esriSchematicOriginOrExtremityNode);
                    if (enumLinks != null && enumLinks.Count == 2)
                    {
                        // Valid degree two node
                        if (!colSchfeatureNode.ContainsKey(schInMemoryfeature.Name))
                        {
                            if (!LinkAttribute)
                                colSchfeatureNode.Add(schInMemoryfeature.Name, schInMemoryfeature);
                            else
                            {
                                if(SameIncidentLinkAttributeValue(enumLinks, LinkAttributeName, ruleHelper))
                                    colSchfeatureNode.Add(schInMemoryfeature.Name, schInMemoryfeature);
                            }
                        }
                    }
                }

                schInMemoryfeature = enumInMemoryFeature.Next();
            }
        }

        private bool SameIncidentLinkAttributeValue(IEnumSchematicInMemoryFeature enumInMemoryLinks, string attributeName, ISchematicRulesHelper ruleHelper)
        {
            ISchematicInMemoryFeature inMemoryFeature = null;

            enumInMemoryLinks.Reset();

            bool bFirstVariant = true;
            object vPreviousValue = null;
            object vCurrentValue = null;

            inMemoryFeature = enumInMemoryLinks.Next();

            while (inMemoryFeature != null)
            {
            // Do not take account the link if the link is not displayed
            //
            // Search for an attribute with the given name
            //
                ISchematicElementClass schematicElementClass;
                schematicElementClass = inMemoryFeature.SchematicElementClass;
                ISchematicAttributeContainer attributeContainer = (ISchematicAttributeContainer)schematicElementClass;
                ISchematicAttribute schematicAttribute = null;
                if (attributeContainer != null)
                    schematicAttribute = attributeContainer.GetSchematicAttribute(attributeName, true);

                if (schematicAttribute != null)
                {
                    ISchematicObject schematicObject = (ISchematicObject)inMemoryFeature;
                    vCurrentValue = schematicAttribute.GetValue(schematicObject);
                }
                else
                {
                // If schematic attribute not existing ==> find a field in the associated feature
                    IObject iObject = null;

                    ISchematicInMemoryFeaturePrimaryAssociation primaryAssociation = (ISchematicInMemoryFeaturePrimaryAssociation)inMemoryFeature;
                    if (primaryAssociation != null)
                        iObject = primaryAssociation.AssociatedObject;
            
                    IRow row = (IRow)iObject;

                    int fieldIndex = 0;
                    if (row != null)
                    {
                        IFields fields = row.Fields;

                        if (fields != null)
                            fieldIndex = fields.FindField(attributeName);
                    }

                    if (fieldIndex > 0)
                    {
                        vCurrentValue = row.get_Value(fieldIndex);
                        if (DBNull.Value.Equals(vCurrentValue))
                            return false;
                    }
                    else
                        return false;
                }

                if (bFirstVariant)
                {
                    vPreviousValue = vCurrentValue;
                    bFirstVariant = false;
                }
                else
                {
                    // Compare PreviousValue and CurrentValue
                    if (vPreviousValue.GetType() != vCurrentValue.GetType())
                        return false;

                    if (DBNull.Value.Equals(vPreviousValue) || DBNull.Value.Equals(vCurrentValue))
                        return false;
                    
                    if (vPreviousValue.GetType().FullName is System.String)//Speciale Case for string.
                    {
                        string str1 = (string)vPreviousValue;
                        string str2 = (string)vCurrentValue;
                        if( string.Compare(str1, str2, true) != 0)
                            return false;
                    }
                    else if (vPreviousValue != vCurrentValue)// == or != operator compare for Variant match the right type.
                        return false;
                }

                inMemoryFeature = enumInMemoryLinks.Next();
            }
        
            return true;
        }
        
    }
        
    #endregion
}
