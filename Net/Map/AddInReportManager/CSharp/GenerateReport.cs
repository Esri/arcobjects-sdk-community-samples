using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.ArcMap;
using ESRI.ArcGIS.Carto;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace CustomReport_CS
{
    public class GenerateReport : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public GenerateReport()
        {
        }

        protected override void OnClick()
        {
            ArcMap.Application.CurrentTool = null;
            ExportReport exportReport = new ExportReport();
            exportReport.Show();
        }
        protected override void OnUpdate()
        {
            Enabled = ArcMap.Application != null;
        }       
    }

}
