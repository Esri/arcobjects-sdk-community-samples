using ESRI.ArcGIS;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.CATIDs;
using Schematic = ESRI.ArcGIS.Schematic;
using ESRI.ArcGIS.Framework;
using System.Windows.Forms;
using ESRI.ArcGIS.esriSystem;
using System;
using System.Collections.Generic;
using System.Text;
using CustomRulesCS;


namespace CustomRulesPageCS
{
    [ClassInterface(ClassInterfaceType.None)]
    [Guid(BisectorRulePropertyPage.GUID)]
    [ProgId(BisectorRulePropertyPage.PROGID)]
    public class BisectorRulePropertyPage : IComPropertyPage
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

        public const string GUID = "227ED86B-CFEB-4f9a-8237-2DC698B430EF";
        public const string PROGID = "CustomRulesPageCS.BisectorRulePropertyPage";

        private FrmBisectorRule m_form = new FrmBisectorRule();     // the custom form
        private BisectorRule m_myBisectorRule;                      // the custom rule
        private string m_title = "Bisector Rule C#";                // the form title
        private int m_priority = 0;                                 // the IComPage priority

        #region IComPropertyPage Membres

        public int Activate()
        {
            if (m_form == null) m_form = new FrmBisectorRule();
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
                m_myBisectorRule.Description = m_form.TxtDescription.Text;
                m_myBisectorRule.distance = Convert.ToDouble(m_form.txtDistance.Text);
                m_myBisectorRule.parentNodeClassName = m_form.cmbParentNodeClass.SelectedItem.ToString();
                m_myBisectorRule.targetNodeClassName = m_form.cmbTargetNodeClass.SelectedItem.ToString();
                m_myBisectorRule.targetLinkClassName = m_form.cmbTargetLinkClass.SelectedItem.ToString();

                m_form.IsDirty = false;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message, "Unable to initialize rule properties", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public void Cancel()
        {
            m_form.IsDirty = false;
        }

        public void Deactivate()
        {
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
            m_myBisectorRule = FindMyRule(objects);
        }

        public void Show()
        {
            Schematic.ISchematicDiagramClass diagramClass;
            diagramClass = ((Schematic.ISchematicRule)m_myBisectorRule).SchematicDiagramClass;
            if (diagramClass == null) return;

            Schematic.ISchematicElementClass elementClass;
            Schematic.IEnumSchematicElementClass enumElementClass;
            enumElementClass = diagramClass.AssociatedSchematicElementClasses;

            try
            {
                if (m_form.cmbParentNodeClass.Items.Count == 0)
                {
                    enumElementClass.Reset();
                    elementClass = enumElementClass.Next();
                    while (elementClass != null)
                    {
                        if (elementClass.SchematicElementType == ESRI.ArcGIS.Schematic.esriSchematicElementType.esriSchematicNodeType)
                            m_form.cmbParentNodeClass.Items.Add(elementClass.Name);

                        elementClass = enumElementClass.Next();
                    }
                }

                if (m_form.cmbTargetNodeClass.Items.Count == 0)
                {
                    enumElementClass.Reset();
                    elementClass = enumElementClass.Next();
                    while (elementClass != null)
                    {
                        if (elementClass.SchematicElementType == Schematic.esriSchematicElementType.esriSchematicNodeType)
                            m_form.cmbTargetNodeClass.Items.Add(elementClass.Name);

                        elementClass = enumElementClass.Next();
                    }
                }

                if (m_form.cmbTargetLinkClass.Items.Count == 0)
                {
                    enumElementClass.Reset();
                    elementClass = enumElementClass.Next();
                    while (elementClass != null)
                    {
                        if (elementClass.SchematicElementType == Schematic.esriSchematicElementType.esriSchematicLinkType)
                            m_form.cmbTargetLinkClass.Items.Add(elementClass.Name);

                        elementClass = enumElementClass.Next();
                    }
                }

                m_form.txtDistance.Text = m_myBisectorRule.distance.ToString();
                m_form.TxtDescription.Text = m_myBisectorRule.Description;
                m_form.cmbParentNodeClass.Text = m_myBisectorRule.parentNodeClassName;
                m_form.cmbTargetNodeClass.Text = m_myBisectorRule.targetNodeClassName;
                m_form.cmbTargetLinkClass.Text = m_myBisectorRule.targetLinkClassName;

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
            m_form.lblParentNode.Visible = true;
            m_form.lblTargetNode.Visible = true;
            m_form.lblTargetLink.Visible = true;
            m_form.lblDistance.Visible = true;
            m_form.cmbParentNodeClass.Visible = true;
            m_form.cmbTargetNodeClass.Visible = true;
            m_form.cmbTargetLinkClass.Visible = true;
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

        ~BisectorRulePropertyPage()
        {
            m_form = null;
            m_myBisectorRule = null;
        }

        // Find and return this rule from the passed in objects 
        private BisectorRule FindMyRule(ESRI.ArcGIS.esriSystem.ISet Objectset)
        {
            if (Objectset.Count == 0)
                return null;

            Objectset.Reset();

            object obj;
            obj = Objectset.Next();

            while (obj != null)
            {
                if (obj is BisectorRule)
                {
                    break;
                }

                obj = Objectset.Next();
            }

            return (BisectorRule)obj;
        }
    }
}
