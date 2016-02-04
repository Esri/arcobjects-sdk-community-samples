'Copyright 2016 Esri

'Licensed under the Apache License, Version 2.0 (the "License");
'you may not use this file except in compliance with the License.
'You may obtain a copy of the License at

'    http://www.apache.org/licenses/LICENSE-2.0

'Unless required by applicable law or agreed to in writing, software
'distributed under the License is distributed on an "AS IS" BASIS,
'WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.

'See the License for the specific language governing permissions and
'limitations under the License.
Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.IO
Imports ESRI.ArcGIS.ArcMapUI
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.SystemUI
Imports ESRI.ArcGIS.Display
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Geometry


Namespace CustomUIElements
  Public Class AddGraphicsTool
    Inherits ESRI.ArcGIS.Desktop.AddIns.Tool
    Public Sub New()
    End Sub

    Protected Overrides Sub OnUpdate()
      Enabled = My.ArcMap.Application IsNot Nothing
    End Sub

    Protected Overrides Sub OnActivate()
      MyBase.OnActivate()

    End Sub
    Protected Overrides Sub OnMouseDown(ByVal arg As ESRI.ArcGIS.Desktop.AddIns.Tool.MouseEventArgs)
      MyBase.OnMouseDown(arg)

      'Get the active view from the AecMap static class
      Dim activeView As IActiveView = My.ArcMap.Document.ActiveView

      'if polyline object then get from the user's mouse clicks.
      Dim polyline As IPolyline = GetPolylineFromMouseClicks(activeView)

      'Make a color to draw the polyline. 
      Dim rgbColor As IRgbColor = New RgbColor
      rgbColor.Red = 255

      'Add the user's drawn graphics as persistent on the map.
      AddGraphicToMap(activeView.FocusMap, polyline, rgbColor, rgbColor)

      'Best practice: Only redraw the portion of the active view that contains graphics. 
      activeView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, Nothing, Nothing)

    End Sub

#Region "Add Graphic to Map"

    '''<summary>Draw a specified graphic on the map using the supplied colors.</summary>
    '''      
    '''<param name="map">An IMap interface.</param>
    '''<param name="geometry">An IGeometry interface. It can be of the geometry type: esriGeometryPoint, esriGeometryPolyline, or esriGeometryPolygon.</param>
    '''<param name="rgbColor">An IRgbColor interface. The color to draw the geometry.</param>
    '''<param name="outlineRgbColor">An IRgbColor interface. For those geometry's with an outline it will be this color.</param>
    '''      
    '''<remarks>Calling this function will not automatically make the graphics appear in the map area. Refresh the map area after after calling this function with Methods like IActiveView.Refresh or IActiveView.PartialRefresh.</remarks>
    Public Sub AddGraphicToMap(ByVal map As IMap, ByVal geometry As IGeometry, ByVal rgbColor As IRgbColor, ByVal outlineRgbColor As IRgbColor)
      Dim graphicsContainer As IGraphicsContainer = CType(map, IGraphicsContainer) ' Explicit Cast
      Dim element As IElement = Nothing
      If (geometry.GeometryType) = ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPoint Then
        ' Marker symbols
        Dim simpleMarkerSymbol As ISimpleMarkerSymbol = New SimpleMarkerSymbolClass()
        simpleMarkerSymbol.Color = rgbColor
        simpleMarkerSymbol.Outline = True
        simpleMarkerSymbol.OutlineColor = outlineRgbColor
        simpleMarkerSymbol.Size = 15
        simpleMarkerSymbol.Style = ESRI.ArcGIS.Display.esriSimpleMarkerStyle.esriSMSCircle

        Dim markerElement As IMarkerElement = New MarkerElementClass()
        markerElement.Symbol = simpleMarkerSymbol
        element = CType(markerElement, IElement) ' Explicit Cast
      ElseIf (geometry.GeometryType) = ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPolyline Then
        '  Line elements
        Dim simpleLineSymbol As ISimpleLineSymbol = New SimpleLineSymbolClass()
        simpleLineSymbol.Color = rgbColor
        simpleLineSymbol.Style = ESRI.ArcGIS.Display.esriSimpleLineStyle.esriSLSSolid
        simpleLineSymbol.Width = 5

        Dim lineElement As ILineElement = New LineElementClass()
        lineElement.Symbol = simpleLineSymbol
        element = CType(lineElement, IElement) ' Explicit Cast
      ElseIf (geometry.GeometryType) = ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPolygon Then
        ' Polygon elements
        Dim simpleFillSymbol As ISimpleFillSymbol = New SimpleFillSymbolClass()
        simpleFillSymbol.Color = rgbColor
        simpleFillSymbol.Style = ESRI.ArcGIS.Display.esriSimpleFillStyle.esriSFSForwardDiagonal
        Dim fillShapeElement As IFillShapeElement = New PolygonElementClass()
        fillShapeElement.Symbol = simpleFillSymbol
        element = CType(fillShapeElement, IElement) ' Explicit Cast
      End If
      If Not (element Is Nothing) Then
        element.Geometry = geometry
        graphicsContainer.AddElement(element, 0)
      End If
    End Sub
#End Region

#Region "Get Polyline From Mouse Clicks"

    '''<summary>
    '''Create a polyline geometry object using the RubberBand.TrackNew method when a user click the mouse on the map control. 
    '''</summary>
    '''<param name="activeView">An ESRI.ArcGIS.Carto.IActiveView interface that will user will interace with to draw a polyline.</param>
    '''<returns>An ESRI.ArcGIS.Geometry.IPolyline interface that is the polyline the user drew</returns>
    '''<remarks>Double click the left mouse button to end tracking the polyline.</remarks>
    Public Function GetPolylineFromMouseClicks(ByVal activeView As IActiveView) As IPolyline

      Dim screenDisplay As IScreenDisplay = activeView.ScreenDisplay

      Dim rubberBand As IRubberBand = New RubberLineClass()
      Dim geometry As IGeometry = rubberBand.TrackNew(screenDisplay, Nothing)
      Dim polyline As IPolyline = CType(geometry, IPolyline)
      Return polyline

    End Function
#End Region

  End Class

End Namespace