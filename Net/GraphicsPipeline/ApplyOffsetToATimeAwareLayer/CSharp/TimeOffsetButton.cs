using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Display;

namespace ApplyOffsetToATimeAwareLayer2008
{
  public class TimeOffsetButton : ESRI.ArcGIS.Desktop.AddIns.Button
  {
    public TimeOffsetButton()
    {
    }

    protected override void OnClick()
    {
      IMxDocument pMxDoc = ArcMap.Document;
      IMap pMap = pMxDoc.FocusMap;
      string filename = "TimeAwareHurricanes.mxd";

      if (pMap.LayerCount < 1)
      {
        MessageBox.Show("Before running this sample, load the associated file \'" + filename + "\'");
        return;
      }
      if (pMap.get_Layer(0).Name != "atlantic_hurricanes_2000")
      {
        MessageBox.Show("Before running this sample, load the associated file \'" + filename + "\'");
        return;
      }
      ILayer selectedLayer = ArcMap.Document.SelectedLayer;
      if (selectedLayer == null)
      {
          MessageBox.Show("There is no selected layer.  Select a time-aware layer");
          return;
      }
      ITimeZoneFactory pTZFac = new TimeZoneFactoryClass();
      IFeatureLayer pFLyr = selectedLayer as IFeatureLayer;
      ITimeData pTimeData = pFLyr as ITimeData;

      //making the first layer of the focused map time-aware
      if (pTimeData.SupportsTime)
      {
        pTimeData.UseTime = true;
        ITimeDataDisplay pTimeAnimProp = pFLyr as ITimeDataDisplay;
        pTimeAnimProp.TimeOffsetUnits = esriTimeUnits.esriTimeUnitsYears;
        pTimeAnimProp.TimeOffset = System.DateTime.Now.Year - 2000;
      }
      else
      {
        MessageBox.Show("Before running this sample, load the associated file \'" + filename + "\'");
        return;
      }

      IActiveView pActiveView = pMap as IActiveView;
      pActiveView.Refresh();
    }
    protected override void OnUpdate()
    {
      Enabled = true;
    }
  }

}
