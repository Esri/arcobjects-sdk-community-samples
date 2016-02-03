using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Framework;
using Schematic = ESRI.ArcGIS.Schematic;
using ESRI.ArcGIS.Geodatabase;

namespace CustomRulesPageCS
{
    public partial class FrmNodeReductionRule : Form
    {
        private bool m_isDirty = false;
        private IComPropertyPageSite m_pageSite;
        private Schematic.ISchematicDiagramClass m_diagramClass;

        public FrmNodeReductionRule()
        {
            InitializeComponent();
        }

        ~FrmNodeReductionRule()
        {
            m_diagramClass = null; 
            m_pageSite = null;
        }

        // For managing the IsDirty flag that specifies whether 
        // or not controls in the custom form have been modified
        public bool IsDirty
        {
            get
            {
                return m_isDirty;
            }
            set
            {
                m_isDirty = value;
            }
        }

        //- For managing the related IComPropertyPageSite
        public IComPropertyPageSite PageSite
        {
            set
            {
                m_pageSite = value;
            }
        }

        private void Changed(object sender, System.EventArgs e)
        {
            // If the user changes something, mark the custom form; dirty and 
            // enable the apply button on the page site via the PageChanged method
            m_isDirty = true;

            if (m_pageSite != null)
            {
                m_pageSite.PageChanged();
            }
        }

        public Schematic.ISchematicDiagramClass DiagramClass
        {
            set
            {
                m_diagramClass = value;
            }
        }

        private Schematic.ISchematicElementClass GetElementClass(Schematic.IEnumSchematicElementClass enumElementClass, string linkCClassName)
        {

            enumElementClass.Reset();
            Schematic.ISchematicElementClass elementClass = enumElementClass.Next();
            while (elementClass != null)
            {
                if (elementClass.Name == linkCClassName)
                    return elementClass;

                elementClass = enumElementClass.Next();
            }

            return null;
        }


        private bool IsFieldNumeriqueAndEditable(string fieldName, Schematic.ISchematicElementClass elementClass)
        {
            ITable table = (ITable)elementClass;

            if (table == null)
                return false;

            int index = table.FindField(fieldName);
            if (index < 0) return false;
            esriFieldType fieldType = table.Fields.get_Field(index).Type;


            if ( ((fieldType ==    esriFieldType.esriFieldTypeDouble) || (fieldType ==    esriFieldType.esriFieldTypeInteger) || (fieldType ==    esriFieldType.esriFieldTypeSmallInteger))     && table.Fields.get_Field(index).Editable)
                return true;

            return false;
        }


        private void FillAttNames(object sender, EventArgs e)
        {

            // report to the propertysheet that the page is dirty
            m_isDirty = true;
            if (m_pageSite != null)
            {
                m_pageSite.PageChanged();
            }
    
            string linkClassName = "";
            if (cmbTargetSuperspanClass.SelectedItem != null) 
                 linkClassName = this.cmbTargetSuperspanClass.SelectedItem.ToString();


            if (linkClassName == "" || m_diagramClass == null)
            {
                cmbAttributeName.Items.Clear();
                return;
            }

            cmbAttributeName.Items.Clear();
            Schematic.ISchematicElementClass elementClass;
            Schematic.IEnumSchematicElementClass enumElementClass;
            enumElementClass = m_diagramClass.AssociatedSchematicElementClasses;

            elementClass = GetElementClass(enumElementClass, linkClassName);
            if (elementClass == null) return;
            Schematic.ISchematicAttributeContainer attCont = (Schematic.ISchematicAttributeContainer)elementClass;

            if (attCont == null) return;

            Schematic.IEnumSchematicAttribute enumAtt = attCont.SchematicAttributes;

            if (enumAtt == null) return;

            enumAtt.Reset();
            Schematic.ISchematicAttribute att = enumAtt.Next();

            while (att != null)
            {
                try
                {
                    Schematic.SchematicAttributeAssociatedField attField = (Schematic.SchematicAttributeAssociatedField)att;
                    if (attField != null)
                    {
                        string fieldName = attField.Name;
                        if (IsFieldNumeriqueAndEditable(fieldName, elementClass))
                            cmbAttributeName.Items.Add(fieldName);
                    }
                }
                catch { }

                att = enumAtt.Next();
            }
        }

        private void chkLinkAttribute_CheckedChanged(object sender, EventArgs e)
        {
            // report to the propertysheet that the page is dirty
            m_isDirty = true;
            if (m_pageSite != null)
            {
                m_pageSite.PageChanged();
            }

            if (!chkLinkAttribute.Checked)
                txtLinkAttribute.Text = "";
        }
    }
}
