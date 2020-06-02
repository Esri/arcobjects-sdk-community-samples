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
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Framework;

namespace CustomRulesPageCS
{
    public partial class FrmBisectorRule : Form
    {
        private bool m_isDirty = false;
        private IComPropertyPageSite m_pageSite;

        public FrmBisectorRule()
        {
            InitializeComponent();
        }

        ~FrmBisectorRule()
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
