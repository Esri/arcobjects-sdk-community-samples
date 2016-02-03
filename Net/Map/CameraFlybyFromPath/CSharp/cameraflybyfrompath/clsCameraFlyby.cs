using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.GlobeCore;
using cameraflybyfrompath;

namespace CameraFlybyFromPath
{
    public class clsCameraFlyby : ESRI.ArcGIS.Desktop.AddIns.Button
    {

        public clsCameraFlyby()
        {
        }

        protected override void OnClick()
        {
            frmCameraPath formFlyby = new frmCameraPath();
            formFlyby.SetVariables(ArcGlobe.Globe);
            formFlyby.Show();
        }

        protected override void OnUpdate()
        {
            Enabled = ArcGlobe.Application != null;
        }
    }
}
