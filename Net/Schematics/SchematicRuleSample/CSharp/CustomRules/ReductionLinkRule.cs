using System;
using System.Collections.Generic;
using System.Text;
using ESRI.ArcGIS;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Schematic;
using esriSystem = ESRI.ArcGIS.esriSystem;

namespace CustomRulesCS
{
    [ClassInterface(ClassInterfaceType.None)]
    [Guid(ReductionLinkRule.GUID)]
    [ProgId(ReductionLinkRule.PROGID)]
    public class ReductionLinkRule : ISchematicRule, ISchematicRuleDesign
    {
        public const string GUID = "52E0F9A1-5E21-4b4a-A5C8-C30721CC1ED4";
        public const string PROGID = "CustomRulesCS.ReductionLinkRule";

        // Register/unregister categories for this class
        #region Component Category Registration
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

        private ESRI.ArcGIS.Schematic.ISchematicDiagramClass m_diagramClass;
        private string m_reductionLinkName;
        private bool m_usePort = false;
        private string m_description = "Reduction Link Rule C#";

        public ReductionLinkRule()
        {
        }

        ~ReductionLinkRule()
        {
            m_diagramClass = null;
        }

        public string ReductionLinkName
        {
            get
            {
                return m_reductionLinkName;
            }
            set
            {
                m_reductionLinkName = value;
            }
        }

        public bool UsePort
        {
            get
            {
                return m_usePort;
            }
            set
            {
                m_usePort = value;
            }
        }

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
                m_reductionLinkName = propertySet.GetProperty("REDUCTIONLINKNAME").ToString();
            }
            catch { }
            try
            {
                m_usePort = (bool)propertySet.GetProperty("USEPORT");
            }
            catch { }
        }

        public void Apply(ISchematicInMemoryDiagram inMemoryDiagram, ESRI.ArcGIS.esriSystem.ITrackCancel cancelTracker)
        {
            if (m_reductionLinkName == "") return;

            IEnumSchematicInMemoryFeature enumSchematicElement;
            ISchematicInMemoryFeature schemElement;
            ISchematicDiagramClass diagramClass = null;
            ISchematicElementClass elementClass;
            IEnumSchematicElementClass enumElementClass;
            Microsoft.VisualBasic.Collection allreadyUsed = new Microsoft.VisualBasic.Collection();
            try
            {
                diagramClass = inMemoryDiagram.SchematicDiagramClass;
            }
            catch { }
            if (diagramClass == null) return;

            enumElementClass = diagramClass.AssociatedSchematicElementClasses;
            enumElementClass.Reset();
            elementClass = enumElementClass.Next();
            while (elementClass != null)
            {
                if (elementClass.Name == m_reductionLinkName) break;
                elementClass = enumElementClass.Next();
            }
            if (elementClass == null) return;

            // Get all link from selected class
            enumSchematicElement = inMemoryDiagram.GetSchematicInMemoryFeaturesByClass(elementClass);
            enumSchematicElement.Reset();

            ISchematicInMemoryFeatureLink link = null;
            ISchematicInMemoryFeatureNode fromNode = null;
            ISchematicInMemoryFeatureNode toNode = null;
            int iFromPort = 0;
            int iToPort = 0;
            ISchematicInMemoryFeature newElem = null;
            IEnumSchematicInMemoryFeatureLink enumIncidentLinks;
            ISchematicInMemoryFeatureLinkerEdit schemLinker = (ISchematicInMemoryFeatureLinkerEdit)(new SchematicLinkerClass());
            bool bReduction = false;

            schemElement = enumSchematicElement.Next();
            while (schemElement != null)
            {
                try
                {
                    string elemName = allreadyUsed[schemElement.Name].ToString();
                    // if found, this link is allready used
                    schemElement = enumSchematicElement.Next();
                    continue;
                }
                catch
                {
                    // Add link to collection
                    allreadyUsed.Add(schemElement.Name, schemElement.Name, null, null);
                }

                // Get from node and to node
                link = (ISchematicInMemoryFeatureLink)schemElement;
                fromNode = link.FromNode;
                toNode = link.ToNode;
                if (m_usePort)
                {
                    iFromPort = link.FromPort;
                    iToPort = link.ToPort;
                }
                // Get all links from this node
                enumIncidentLinks = fromNode.GetIncidentLinks(esriSchematicEndPointType.esriSchematicOriginOrExtremityNode);
                enumIncidentLinks.Reset();
                newElem = enumIncidentLinks.Next();
                while (newElem != null)
                {
                    bReduction = false;
                    if (newElem == schemElement)
                    {
                        // the new link is the same link we works on
                        newElem = enumIncidentLinks.Next();
                        continue;
                    }
                    link = (ISchematicInMemoryFeatureLink)newElem;

                    // 1st case of comparison
                    if (fromNode == link.FromNode && toNode == link.ToNode)
                    {
                        if (m_usePort)
                            bReduction = (iFromPort == link.FromPort && iToPort == link.ToPort);
                        else
                            bReduction = true;
                    }
                    // 2nd case of comparison
                    else if (fromNode == link.ToNode && toNode == link.FromNode)
                    {
                        if (m_usePort)
                            bReduction = (iFromPort == link.ToPort && iToPort == link.FromPort);
                        else
                            bReduction = true;
                    }

                    if (bReduction)
                    {
                        try
                        {
                            schemLinker.ReportAssociations(newElem, schemElement); // Reports asssociation to first link
                            allreadyUsed.Add(newElem.Name, newElem.Name, null, null);    // Add link to collection
                            newElem.Displayed = false; // this link is not visible
                        }
                        catch { }
                    }
                    newElem = enumIncidentLinks.Next();
                }
                schemElement.Displayed = true;
                schemElement = enumSchematicElement.Next();
            }
        }

        public ESRI.ArcGIS.esriSystem.UID ClassID
        {
            get
            {
                esriSystem.UID ruleID = new esriSystem.UID();
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
                return "Reduction Link Rule C#";
            }
        }

        public ESRI.ArcGIS.esriSystem.IPropertySet PropertySet
        {
            get
            {
                esriSystem.IPropertySet propertySet = new esriSystem.PropertySet();

                propertySet.SetProperty("DESCRIPTION", m_description);
                propertySet.SetProperty("USEPORT", m_usePort);
                propertySet.SetProperty("REDUCTIONLINKNAME", m_reductionLinkName);

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
                m_usePort = (bool)value.GetProperty("USEPORT");
                m_reductionLinkName = value.GetProperty("REDUCTIONLINKNAME").ToString();
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
        #endregion
    }
}
