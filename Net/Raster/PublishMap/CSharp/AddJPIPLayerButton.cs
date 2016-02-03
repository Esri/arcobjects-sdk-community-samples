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
