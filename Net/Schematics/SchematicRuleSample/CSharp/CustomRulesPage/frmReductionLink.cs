using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Framework;

namespace CustomRulesPageCS
{
    public partial class frmReductionLink : Form
    {
        private bool m_isDirty = false;
        private IComPropertyPageSite m_pageSite;

        public frmReductionLink()
        {
            InitializeComponent();
        }

        ~frmReductionLink()
        {
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
    }
}
