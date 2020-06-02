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
using System.Text;
using System.IO;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Display;
using System.Windows.Forms;

namespace MakeACustomTimeControl2008
{
  public class CustomTimeSliderButton : ESRI.ArcGIS.Desktop.AddIns.Button
  {
    ITimeExtent m_myLayerTimeExtent = null;
    ITimeDuration m_layerInterval = null;
    TimeSliderDialog m_sliderDlg = null;

    public CustomTimeSliderButton()
    {
    }

    protected override void OnClick()
    {
      IMxDocument pMxDoc = ArcMap.Document;
      if (pMxDoc.SelectedLayer == null)
      {
        MessageBox.Show("There is no layer selected.  First select a time-aware layer.");
        return;
      }

      IFeatureLayer pFLyr = pMxDoc.SelectedLayer as IFeatureLayer;
      ITimeData pTimeData = pFLyr as ITimeData;
      if (!pTimeData.SupportsTime)
      {
        MessageBox.Show("Select a time-aware layer first.");
        return;
      }
      m_myLayerTimeExtent = pTimeData.GetFullTimeExtent();

      ITimeDataDisplay pTimeDataDisplayProperties = pFLyr as ITimeDataDisplay;
      esriTimeUnits LayerIntervalUnits = pTimeDataDisplayProperties.TimeIntervalUnits;
      double LayerInterval = pTimeDataDisplayProperties.TimeInterval;
      ITime startTime = m_myLayerTimeExtent.StartTime;
      ITime endTime = (ITime)((IClone)startTime).Clone();

      switch (LayerIntervalUnits)
      {
        case esriTimeUnits.esriTimeUnitsYears:
          ((ITimeOffsetOperator)endTime).AddYears(LayerInterval, false, true);
          break;
        case esriTimeUnits.esriTimeUnitsMonths:
          ((ITimeOffsetOperator)endTime).AddMonths(LayerInterval, false, true);
          break;
        case esriTimeUnits.esriTimeUnitsDays:
          ((ITimeOffsetOperator)endTime).AddDays(LayerInterval);
          break;
        case esriTimeUnits.esriTimeUnitsHours:
          ((ITimeOffsetOperator)endTime).AddHours(LayerInterval);
          break;
        case esriTimeUnits.esriTimeUnitsMinutes:
          ((ITimeOffsetOperator)endTime).AddMinutes(LayerInterval);
          break;
        case esriTimeUnits.esriTimeUnitsSeconds:
          ((ITimeOffsetOperator)endTime).AddSeconds(LayerInterval);
          break;
      }

      ITimeExtent pTimeExt = new TimeExtentClass();
      pTimeExt.SetExtent(startTime, endTime);
      m_layerInterval = pTimeExt.QueryTimeDuration();


      m_sliderDlg = new TimeSliderDialog(this);
      m_sliderDlg.Show();
    }

    public ITimeExtent GetTimeExtent()
    {
      return m_myLayerTimeExtent;
    }

    public void UpdateCurrentTime(double progress)
    {
      if (progress <= 0)
        progress = 0.05;
      else if (progress >= 100)
        progress = 0.95;

      //Calculate how far into the layer to jump
      ITimeDuration offsetToNewCurrentTime = m_myLayerTimeExtent.QueryTimeDuration();
      offsetToNewCurrentTime.Scale(progress);

      IMxDocument pMxDoc = ArcMap.Document;
      IMap pMap = pMxDoc.FocusMap;
      IActiveView pActiveView = pMap as IActiveView;
      IScreenDisplay pScreenDisplay = pActiveView.ScreenDisplay;
      ITimeDisplay pTimeDisplay = pScreenDisplay as ITimeDisplay;

      ITime startTime = m_myLayerTimeExtent.StartTime;
      ITime endTime = (ITime)((IClone)startTime).Clone();
      ((ITimeOffsetOperator)endTime).AddDuration(m_layerInterval);
      ITimeExtent pTimeExt = new TimeExtentClass();
      pTimeExt.SetExtent(startTime, endTime);
      pTimeExt.Empty = false;
      ((ITimeOffsetOperator)pTimeExt).AddDuration(offsetToNewCurrentTime);
      pTimeDisplay.TimeValue = pTimeExt as ITimeValue;
      pActiveView.Refresh();

    }

    protected override void OnUpdate()
    {
      Enabled = true;
    }
  }

}
