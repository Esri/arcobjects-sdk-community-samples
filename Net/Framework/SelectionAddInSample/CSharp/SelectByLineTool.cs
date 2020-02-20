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
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.esriSystem;

namespace SelectionSample
{
  public class SelectByLineTool : ESRI.ArcGIS.Desktop.AddIns.Tool
  {
    private bool m_isMouseDown = false;
    private ESRI.ArcGIS.Display.INewLineFeedback m_lineFeedback;
    private IActiveView m_focusMap;

    public SelectByLineTool()
    {

    }

    protected override void OnMouseDown(MouseEventArgs arg)
    {
      IMxDocument mxDoc = ArcMap.Document;
      m_focusMap = mxDoc.FocusMap as IActiveView;
      IPoint point = m_focusMap.ScreenDisplay.DisplayTransformation.ToMapPoint(arg.X, arg.Y) as IPoint;

      if (m_lineFeedback == null)
      {
        m_lineFeedback = new ESRI.ArcGIS.Display.NewLineFeedback();
        m_lineFeedback.Display = m_focusMap.ScreenDisplay;
        m_lineFeedback.Start(point);
      }
      else
      {
        m_lineFeedback.AddPoint(point);
      }

      m_isMouseDown = true;
    }

    protected override void OnDoubleClick()
    {
      m_focusMap.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, null);

      IPolyline polyline;

      if (m_lineFeedback != null)
      {
        polyline = m_lineFeedback.Stop();
        if (polyline != null)
          ArcMap.Document.FocusMap.SelectByShape(polyline, null, false);
      }


      m_focusMap.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, null);

      m_lineFeedback = null;
      m_isMouseDown = false;
    }

    protected override void OnMouseMove(MouseEventArgs arg)
    {
      if (!m_isMouseDown) return;

      IPoint point = m_focusMap.ScreenDisplay.DisplayTransformation.ToMapPoint(arg.X, arg.Y) as IPoint;
      m_lineFeedback.MoveTo(point);
    }

    protected override void OnUpdate()
    {
      if (!SelectionExtension.IsExtensionEnabled())
      {
        this.Enabled = false;
        return;
      }

      this.Enabled = SelectionExtension.HasSelectableLayer();
    }
  }
}
