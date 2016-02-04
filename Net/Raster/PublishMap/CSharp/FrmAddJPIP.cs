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
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Carto;
using System.IO;

namespace AddJPIPLayer
{
    public partial class FrmAddJPIP : Form
    {
        public FrmAddJPIP()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //create raster dataset from the JPIP service url
                Type factoryType = Type.GetTypeFromProgID("esriDataSourcesRaster.RasterWorkspaceFactory");
                IWorkspaceFactory wsFact = Activator.CreateInstance(factoryType) as IWorkspaceFactory;
                string tempPath = Path.GetTempPath();
                IRasterWorkspace2 ws = wsFact.OpenFromFile(tempPath, 0) as IRasterWorkspace2;
                IRasterDataset rds = ws.OpenRasterDataset(txtJPIPUrl.Text);

                //create a layer from the raster dataset
                IRasterLayer rasterLayer = new RasterLayerClass();
                rasterLayer.CreateFromDataset(rds);
                string layerName = txtLayerName.Text;
                if (layerName == "")
                    layerName = txtJPIPUrl.Text.Substring(txtJPIPUrl.Text.LastIndexOf("/") + 1, txtJPIPUrl.Text.Length - txtJPIPUrl.Text.LastIndexOf("/") - 1);
                rasterLayer.Name = layerName;

                //add the JPIP layer to the current data frame of ArcMap
                ArcMap.Document.FocusMap.AddLayer(rasterLayer);
                this.Close();
            }
            catch
            {                
                MessageBox.Show("Couldn't connect to the specified URL, sample url: jpip://myserver:8080/JP2Server/imagealias");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmAddJPIP_Load(object sender, EventArgs e)
        {
        }

        private void txtJPIPUrl_TextChanged(object sender, EventArgs e)
        {
            if (txtJPIPUrl.Text.ToLower().Contains("jp2server/"))
                txtLayerName.Text = txtJPIPUrl.Text.Substring(txtJPIPUrl.Text.LastIndexOf("/") + 1, txtJPIPUrl.Text.Length - txtJPIPUrl.Text.LastIndexOf("/") - 1);
            else
                txtLayerName.Text = "";
        }
       
    }
}
