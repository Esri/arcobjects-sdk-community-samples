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
// Copyright 2010 ESRI
// 
// All rights reserved under the copyright laws of the United States
// and applicable international laws, treaties, and conventions.
// 
// You may freely redistribute and use this sample code, with or
// without modification, provided you include the original copyright
// notice and use restrictions.
// 
// See the use restrictions at &lt;your ArcGIS install location&gt;/DeveloperKit10.0/userestrictions.txt.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.ArcCatalog;
using ESRI.ArcGIS.Catalog;
namespace SchematicCreateBasicSettingsAddIn
{
    public partial class frmDatasetTemplateName : Form
    {
        public Boolean blnNewDataset = false;
        public event EventHandler cancelFormEvent;
        public event EventHandler<NameEvents> nextFormEvent;

        public frmDatasetTemplateName()
        {
            InitializeComponent();
        }

        private void frmDatasetTemplateName_Load(object sender, EventArgs e)
        {
            if (blnNewDataset == false)
            {
                txtDatasetName.Enabled = false;
                txtDatasetName.Text = ArcCatalog.ThisApplication.SelectedObject.Name;
            }
        }
        
        private void btnCancel_Click(object sender, EventArgs e)
        {    
            //this.cancelFormEvent(sender, e);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            //raise event back to controller
            NameEvents evts = new NameEvents((bool)blnNewDataset, (string)txtDatasetName.Text, (string)txtTemplateName.Text,(bool)chkVertices.Checked);
            this.nextFormEvent(sender,evts);
        }

        private void txtDatasetName_TextChanged(object sender, EventArgs e)
        {
            if ((txtDatasetName.Text.Length > 0) && (txtTemplateName.Text.Length > 0))
            {
                btnNext.Enabled = true;
            }
            else
            {
                btnNext.Enabled = false;
            }
        }

        private void txtTemplateName_TextChanged(object sender, EventArgs e)
        {
            if ((txtDatasetName.Text.Length > 0) && (txtTemplateName.Text.Length > 0))
            {
                btnNext.Enabled = true;
            }
            else
            {
                btnNext.Enabled = false;
            }
        }

    }
}
