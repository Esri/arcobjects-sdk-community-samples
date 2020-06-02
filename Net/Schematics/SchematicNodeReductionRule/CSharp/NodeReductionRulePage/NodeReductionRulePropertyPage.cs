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
using ESRI.ArcGIS;
using ESRI.ArcGIS.ADF.CATIDs;
using Schematic = ESRI.ArcGIS.Schematic;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.esriSystem;
using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Windows.Forms;
using CustomRulesCS;



namespace CustomRulesPageCS
{
    [ClassInterface(ClassInterfaceType.None)]
    [Guid(NodeReductionRulePropertyPage.GUID)]
    [ProgId(NodeReductionRulePropertyPage.PROGID)]
    public class NodeReductionRulePropertyPage : IComPropertyPage
    {
        // Register/unregister categories for this class
        #region "Component Category Registration"
        [System.Runtime.InteropServices.ComRegisterFunction()]
        public static void Register(string CLSID)
        {
            SchematicRulePropertyPages.Register(CLSID);
        }

        [System.Runtime.InteropServices.ComUnregisterFunction()]
        public static void Unregister(string CLSID)
        {
            SchematicRulePropertyPages.Unregister(CLSID);
        }
        #endregion

        public const string GUID = "F58F5916-3A99-49CF-9A7D-5EB97E7618FD";
        public const string PROGID = "CustomRulesPageCS.NodeReductionRulePropertyPage";

        private FrmNodeReductionRule m_form = new FrmNodeReductionRule();     // the custom form
        private NodeReductionRule m_myNodeReductionRule;                                        // the custom rule
        private string m_title = "Node Reduction Rule C#";                                    // the form title
        private int m_priority = 0;                                                                                 // the IComPage priority

        #region IComPropertyPage Membres

        public int Activate()
        {
            if (m_form == null) m_form = new FrmNodeReductionRule();
            return (int)m_form.Handle;
        }

        public bool Applies(ISet objects)
        {
            Schematic.ISchematicRule mySchematicRule;
            mySchematicRule = FindMyRule(objects);
            return (mySchematicRule != null);
        }

        public void Apply()
        {
            try
            {
                m_myNodeReductionRule.Description = m_form.TxtDescription.Text;
            }
            catch { }

            try
            {
                m_myNodeReductionRule.ReducedNodeClassName = m_form.cmbReducedNodeClass.SelectedItem.ToString();
            }
            catch { }

            try
            {
                m_myNodeReductionRule.SuperpanLinkClassName = m_form.cmbTargetSuperspanClass.SelectedItem.ToString();
            }
            catch { }

            try
            {
                m_myNodeReductionRule.KeepVertices = m_form.chkKeepVertices.Checked;
            }
            catch { }

            try
            {
                m_myNodeReductionRule.LengthAttributeName = m_form.cmbAttributeName.SelectedItem.ToString();
            }
            catch { }

            try
            {
                m_myNodeReductionRule.LinkAttribute = m_form.chkLinkAttribute.Checked;
            }
            catch { }

            try
            {
                m_myNodeReductionRule.LinkAttributeName = m_form.txtLinkAttribute.Text;
            }
            catch { }

            m_form.IsDirty = false;
        }

        public void Cancel()
        {
            m_form.IsDirty = false;
        }

        public void Deactivate()
        {
            m_form.DiagramClass = null;
            m_form.Close();
        }

        public int Height
        {
            get { return m_form.Height; }
        }

        public int get_HelpContextID(int controlID)
        {
            // TODO: return context ID if desired
            return 0;
        }

        public string HelpFile
        {
            get { return ""; }
        }

        public void Hide()
        {
            m_form.Hide();
        }

        public bool IsPageDirty
        {
            get { return m_form.IsDirty; }
        }

        public IComPropertyPageSite PageSite
        {
            set { m_form.PageSite = value; }
        }

        public int Priority
        {
            get
            {
                return m_priority;
            }
            set
            {
                m_priority = value;
            }
        }

        public void SetObjects(ISet objects)
        {
            // Search for the custom rule object instance
            m_myNodeReductionRule = FindMyRule(objects);
        }


        public void Show()
        {
            Schematic.ISchematicDiagramClass diagramClass;
            diagramClass = ((Schematic.ISchematicRule)m_myNodeReductionRule).SchematicDiagramClass;
            if (diagramClass == null) return;

            m_form.DiagramClass = diagramClass;
            Schematic.ISchematicElementClass elementClass;

            Schematic.IEnumSchematicElementClass enumElementClass;
            enumElementClass = diagramClass.AssociatedSchematicElementClasses;

            m_form.cmbReducedNodeClass.Items.Clear();
            m_form.cmbTargetSuperspanClass.Items.Clear();
            m_form.cmbAttributeName.Items.Clear();

            try
            {
                enumElementClass.Reset();
                elementClass = enumElementClass.Next();
                while (elementClass != null)
                {
                    if (elementClass.SchematicElementType == ESRI.ArcGIS.Schematic.esriSchematicElementType.esriSchematicNodeType)
                        m_form.cmbReducedNodeClass.Items.Add(elementClass.Name);
                    else if (elementClass.SchematicElementType == ESRI.ArcGIS.Schematic.esriSchematicElementType.esriSchematicLinkType)
                    {
                        m_form.cmbTargetSuperspanClass.Items.Add(elementClass.Name);
                    }

                    elementClass = enumElementClass.Next();
                }

                m_form.cmbAttributeName.Text = m_myNodeReductionRule.LengthAttributeName;
                m_form.TxtDescription.Text = m_myNodeReductionRule.Description;
                m_form.cmbReducedNodeClass.Text = m_myNodeReductionRule.ReducedNodeClassName;
                m_form.cmbTargetSuperspanClass.Text = m_myNodeReductionRule.SuperpanLinkClassName;
                m_form.chkKeepVertices.Checked = m_myNodeReductionRule.KeepVertices;
                m_form.chkLinkAttribute.Checked = m_myNodeReductionRule.LinkAttribute;
                m_form.txtLinkAttribute.Text = m_myNodeReductionRule.LinkAttributeName;
                m_form.IsDirty = false;

                SetVisibleControls();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message, "Unable to initialize property page", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void SetVisibleControls()
        {
            m_form.Visible = true;
            m_form.lblDescription.Visible = true;
            m_form.lblGroup.Visible = true;
            m_form.lblReducedNode.Visible = true;
            m_form.lblTargetSuperspan.Visible = true;
            m_form.lblAttributeName.Visible = true;
            m_form.cmbReducedNodeClass.Visible = true;
            m_form.cmbTargetSuperspanClass.Visible = true;
            m_form.chkKeepVertices.Visible = true;
            m_form.chkLinkAttribute.Visible = true;
            m_form.txtLinkAttribute.Visible = true;
        }

        public string Title
        {
            get
            {
                return m_title;
            }
            set
            {
                m_title = value;
            }
        }

        public int Width
        {
            get { return m_form.Width; }
        }

        #endregion

        ~NodeReductionRulePropertyPage()
        {
            m_form.DiagramClass = null;
            m_form = null;
            m_myNodeReductionRule = null;
        }

        // Find and return this rule from the passed in objects 
        private NodeReductionRule FindMyRule(ESRI.ArcGIS.esriSystem.ISet Objectset)
        {
            if (Objectset.Count == 0)
                return null;

            Objectset.Reset();

            object obj;
            obj = Objectset.Next();

            while (obj != null)
            {
                if (obj is CustomRulesCS.NodeReductionRule)
                {
                    break;
                }

                obj = Objectset.Next();
            }

            return (NodeReductionRule)obj;
        }
    }
}
