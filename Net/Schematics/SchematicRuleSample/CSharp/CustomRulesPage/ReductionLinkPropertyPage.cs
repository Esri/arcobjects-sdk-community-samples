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
    [Guid(ReductionLinkPropertyPage.GUID)]
    [ProgId(ReductionLinkPropertyPage.PROGID)]
    public class ReductionLinkPropertyPage : IComPropertyPage
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

        public const string GUID = "9D1CD5C2-AF73-4a70-B1DD-8B092601CFE8";
        public const string PROGID = "CustomRulesPageCS.ReductionLinkPropertyPage";

        private frmReductionLink m_form = new frmReductionLink();   // the custom form
        private ReductionLinkRule m_mySchematicRule;                 // the custom rule
        private string m_title = "Reduction Links Rule C#";         // the form title
        private int m_priority = 0;                                   // the IComPage priority

        #region IComPropertyPage Membres

        public int Activate()
        {
            // Create a new RemoveElementForm but do not show it 
            if (m_form == null) m_form = new frmReductionLink();
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
                m_mySchematicRule.Description = m_form.txtDescription.Text;
                m_mySchematicRule.ReductionLinkName = m_form.cboReduce.SelectedItem.ToString();
                m_mySchematicRule.UsePort = m_form.chkUsePort.Checked;
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
            m_mySchematicRule = FindMyRule(objects);
        }

        public void Show()
        {
            try
            {
                if (m_form.cboReduce.Items.Count == 0)
                {
                    Schematic.ISchematicDiagramClass diagramClass;
                    diagramClass = ((Schematic.ISchematicRule)m_mySchematicRule).SchematicDiagramClass;
                    if (diagramClass == null) return;

                    Schematic.ISchematicElementClass elementClass;
                    Schematic.IEnumSchematicElementClass enumElementClass;
                    enumElementClass = diagramClass.AssociatedSchematicElementClasses;
                    enumElementClass.Reset();
                    elementClass = enumElementClass.Next();
                    while (elementClass != null)
                    {
                        if (elementClass.SchematicElementType == Schematic.esriSchematicElementType.esriSchematicLinkType)
                            m_form.cboReduce.Items.Add(elementClass.Name);

                        elementClass = enumElementClass.Next();
                    }
                }

                m_form.cboReduce.Text = m_mySchematicRule.ReductionLinkName;
                m_form.txtDescription.Text = m_mySchematicRule.Description;
                m_form.chkUsePort.Checked = m_mySchematicRule.UsePort;
                m_form.IsDirty = false;

                m_form.Visible = true;
                m_form.lblDescription.Visible = true;
                m_form.lblReduce.Visible = true;
                m_form.txtDescription.Visible = true;
                m_form.cboReduce.Visible = true;
                m_form.chkUsePort.Visible = true;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message, "Unable to initialize property page", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
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

        ~ReductionLinkPropertyPage()
        {
            m_form = null;
            m_mySchematicRule = null;
        }

        // Find and return this rule from the passed in objects 
        private ReductionLinkRule FindMyRule(ESRI.ArcGIS.esriSystem.ISet Objectset)
        {
            if (Objectset.Count == 0)
                return null;

            Objectset.Reset();

            object obj;
            obj = Objectset.Next();

            while (obj != null)
            {
                if (obj is ReductionLinkRule)
                {
                    break;
                }

                obj = Objectset.Next();
            }

            return (ReductionLinkRule)obj;
        }

    }
}
