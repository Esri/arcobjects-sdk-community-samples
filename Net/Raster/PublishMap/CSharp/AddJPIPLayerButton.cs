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
using System.Text;
using System.IO;
using ESRI.ArcGIS.ArcMap;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using System.Windows.Forms;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.Geodatabase;
namespace AddJPIPLayer
{
    public class AddJPIPLayerButton : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        FrmAddJPIP frmAddJPIP = null;
        public AddJPIPLayerButton()
        {
        }

        protected override void OnClick()
        {
            if (frmAddJPIP == null || !frmAddJPIP.Visible) //new instance
            {
                ArcMap.Application.CurrentTool = null;
                frmAddJPIP = new FrmAddJPIP();
                frmAddJPIP.Show();
            }
            else if (!frmAddJPIP.Focused) //focus
            {             
                frmAddJPIP.Focus();
            }
        }
        protected override void OnUpdate()
        {
            Enabled = ArcMap.Application != null;
        }
    }
}
