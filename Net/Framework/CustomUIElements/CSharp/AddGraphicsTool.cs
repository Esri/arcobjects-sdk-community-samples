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
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;

namespace CustomUIElements
{
  public class AddGraphicsTool : ESRI.ArcGIS.Desktop.AddIns.Tool
  {
    public AddGraphicsTool()
    {
    }

    protected override void OnUpdate()
    {
      Enabled = ArcMap.Application != null;
    }

    protected override void OnMouseDown(MouseEventArgs arg)
    {
      //Get the active view from the AecMap static class
      IActiveView activeView = ArcMap.Document.ActiveView;

      //if polyline object then get from the user's mouse clicks.
      IPolyline polyline = GetPolylineFromMouseClicks(activeView);

      //Make a color to draw the polyline. 
      IRgbColor rgbColor = new RgbColorClass();
      rgbColor.Red = 255;     

      //Add the user's drawn graphics as persistent on the map.
      AddGraphicToMap(activeView.FocusMap, polyline, rgbColor, rgbColor);

      //Best practice: Only redraw the portion of the active view that contains graphics. 
      activeView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
    }

    #region "Add Graphic to Map"

    ///<summary>Draw a specified graphic on the map using the supplied colors.</summary>
    ///      
    ///<param name="map">An IMap interface.</param>
    ///<param name="geometry">An IGeometry interface. It can be of the geometry type: esriGeometryPoint, esriGeometryPolyline, or esriGeometryPolygon.</param>
    ///<param name="rgbColor">An IRgbColor interface. The color to draw the geometry.</param>
    ///<param name="outlineRgbColor">An IRgbColor interface. For those geometry's with an outline it will be this color.</param>
    ///      
    ///<remarks>Calling this function will not automatically make the graphics appear in the map area. Refresh the map area after calling this function with Methods like IActiveView.Refresh or IActiveView.PartialRefresh.</remarks>
    public void AddGraphicToMap(IMap map, IGeometry geometry, IRgbColor rgbColor, IRgbColor outlineRgbColor)
    {
      IGraphicsContainer graphicsContainer = (IGraphicsContainer)map; // Explicit Cast
      IElement element = null;
      if ((geometry.GeometryType) == esriGeometryType.esriGeometryPoint)
      {
        // Marker symbols
        ISimpleMarkerSymbol simpleMarkerSymbol = new SimpleMarkerSymbolClass();
        simpleMarkerSymbol.Color = rgbColor;
        simpleMarkerSymbol.Outline = true;
        simpleMarkerSymbol.OutlineColor = outlineRgbColor;
        simpleMarkerSymbol.Size = 15;
        simpleMarkerSymbol.Style = esriSimpleMarkerStyle.esriSMSCircle;

        IMarkerElement markerElement = new MarkerElementClass();
        markerElement.Symbol = simpleMarkerSymbol;
        element = (IElement)markerElement; // Explicit Cast
      }
      else if ((geometry.GeometryType) == esriGeometryType.esriGeometryPolyline)
      {
        //  Line elements
        ISimpleLineSymbol simpleLineSymbol = new SimpleLineSymbolClass();
        simpleLineSymbol.Color = rgbColor;
        simpleLineSymbol.Style = esriSimpleLineStyle.esriSLSSolid;
        simpleLineSymbol.Width = 5;

        ILineElement lineElement = new LineElementClass();
        lineElement.Symbol = simpleLineSymbol;
        element = (IElement)lineElement; // Explicit Cast
      }
      else if ((geometry.GeometryType) == esriGeometryType.esriGeometryPolygon)
      {
        // Polygon elements
        ISimpleFillSymbol simpleFillSymbol = new SimpleFillSymbolClass();
        simpleFillSymbol.Color = rgbColor;
        simpleFillSymbol.Style = esriSimpleFillStyle.esriSFSForwardDiagonal;
        IFillShapeElement fillShapeElement = new PolygonElementClass();
        fillShapeElement.Symbol = simpleFillSymbol;
        element = (IElement)fillShapeElement; // Explicit Cast
      }
      if (!(element == null))
      {
        element.Geometry = geometry;
        graphicsContainer.AddElement(element, 0);
      }
    }
    #endregion



    #region "Get Polyline From Mouse Clicks"

    ///<summary>
    ///Create a polyline geometry object using the RubberBand.TrackNew method when a user click the mouse on the map control. 
    ///</summary>
    ///<param name="activeView">An ESRI.ArcGIS.Carto.IActiveView interface that will user will interact with to draw a polyline.</param>
    ///<returns>An ESRI.ArcGIS.Geometry.IPolyline interface that is the polyline the user drew</returns>
    ///<remarks>Double click the left mouse button to end tracking the polyline.</remarks>
    public IPolyline GetPolylineFromMouseClicks(IActiveView activeView)
    {

      IScreenDisplay screenDisplay = activeView.ScreenDisplay;

      IRubberBand rubberBand = new RubberLineClass();
      IGeometry geometry = rubberBand.TrackNew(screenDisplay, null);
      IPolyline polyline = (IPolyline)geometry;
      return polyline;

    }
    #endregion
  }

}
