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
