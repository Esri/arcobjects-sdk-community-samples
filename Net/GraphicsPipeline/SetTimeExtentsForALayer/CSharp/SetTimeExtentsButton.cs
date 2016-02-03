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

namespace SetTimeExtentsForALayer2008
{
  public class SetTimeExtentsButton : ESRI.ArcGIS.Desktop.AddIns.Button
  {
    public SetTimeExtentsButton()
    {
    }

    protected override void OnClick()
    {
      IMxDocument pMxDoc = ArcMap.Document;
      IMap pMap = pMxDoc.FocusMap;
      string sampeMapFileName = "BasicHurricanes.mxd";

      if (pMap.LayerCount < 1)
      {
        MessageBox.Show("Before running this sample, load the associated file \'" + sampeMapFileName + "\'");
        return;
      }
      if (pMap.get_Layer(0).Name != "atlantic_hurricanes_2000")
      {
        MessageBox.Show("Before running this sample, load the associated file \'" + sampeMapFileName + "\'");
        return;
      }

      ITimeZoneFactory pTZFac = new TimeZoneFactoryClass();
      //making the first layer of the focused map time-aware
      IFeatureLayer pFLyr = pMap.get_Layer(0) as IFeatureLayer;
      ITimeData pTimeData = pFLyr as ITimeData;

      String localTimeZoneId = pTZFac.QueryLocalTimeZoneWindowsID();
      ITimeReference timeRef = pTZFac.CreateTimeReferenceFromWindowsID(localTimeZoneId);
      if (pTimeData.SupportsTime)
      {
        pTimeData.UseTime = true;
        ITimeTableDefinition pTimeDataDef = pFLyr as ITimeTableDefinition;
        pTimeDataDef.StartTimeFieldName = "Date_Time";

        pTimeDataDef.TimeReference = timeRef;
        ITimeDataDisplay pTimeAnimProp = pFLyr as ITimeDataDisplay;
        pTimeAnimProp.TimeIntervalUnits = esriTimeUnits.esriTimeUnitsHours;
        pTimeAnimProp.TimeInterval = 12.0;
      }

      //
      IActiveView pActiveView = pMap as IActiveView;
      IScreenDisplay pScreenDisplay = pActiveView.ScreenDisplay;
      ITimeDisplay pTimeDisplay = pScreenDisplay as ITimeDisplay;
      pTimeDisplay.TimeReference = timeRef;

      ITime pStartTime = new TimeClass();
      pStartTime.Year = 2000; pStartTime.Month = 9; pStartTime.Day = 25;
      ITime pEndTime = new TimeClass();
      pEndTime.Year = 2000; pEndTime.Month = 9; pEndTime.Day = 30;

      ITimeExtent pTimeExt = new TimeExtentClass();
      pTimeExt.StartTime = pStartTime;
      pTimeExt.EndTime = pEndTime;
      pTimeDisplay.TimeValue = pTimeExt as ITimeValue;

      pActiveView.ContentsChanged();
    }
    protected override void OnUpdate()
    {
      //Enabled = ArcMap.Application != null;
      Enabled = true;
    }
  }

}
