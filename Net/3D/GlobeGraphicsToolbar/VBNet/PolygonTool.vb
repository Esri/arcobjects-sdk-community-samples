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
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.ADF.CATIDs
Imports System.Windows.Forms
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.Display

Namespace GlobeGraphicsToolbar
	Public Class PolygonTool
		Inherits ESRI.ArcGIS.Desktop.AddIns.Tool
		Private _polygonGeometry As PolygonGeometry = Nothing
		Private Const LeftButton As Integer = 1
        Private Const GeographicCoordinateSystem As ESRI.ArcGIS.Geometry.esriSRGeoCSType = ESRI.ArcGIS.Geometry.esriSRGeoCSType.esriSRGeoCS_WGS1984
		Private Const PointElementSize As Double = 1
        Private Const PointElementStyle As ESRI.ArcGIS.Display.esriSimpleMarkerStyle = ESRI.ArcGIS.Display.esriSimpleMarkerStyle.esriSMSCircle
		Private Const PolylineElementWidth As Double = 1000
        Private Const PolylineElementStyle As ESRI.ArcGIS.Display.esriSimpleLineStyle = ESRI.ArcGIS.Display.esriSimpleLineStyle.esriSLSSolid
		Private Const GraphicsLayerName As String = "Globe Graphics"
		Public Sub New()
		End Sub

		Protected Overrides Sub OnUpdate()

		End Sub

		Protected Overrides Sub OnMouseDown(ByVal arg As ESRI.ArcGIS.Desktop.AddIns.Tool.MouseEventArgs)
			If arg.Button = MouseButtons.Left Then
				Dim geographicCoordinates As New GeographicCoordinates(ArcGlobe.Globe, arg.X, arg.Y)

				Dim spatialReferenceFactory As New SpatialReferenceFactory(CInt(Fix(GeographicCoordinateSystem)))

				Dim pointGeometry As New PointGeometry(geographicCoordinates.Longitude, geographicCoordinates.Latitude, geographicCoordinates.AltitudeInKilometers, spatialReferenceFactory.SpatialReference)

				If _polygonGeometry Is Nothing Then
					_polygonGeometry = New PolygonGeometry(spatialReferenceFactory.SpatialReference)
				End If

				_polygonGeometry.AddPoint(TryCast(pointGeometry.Geometry, IPoint))

				Dim tableOfContents As New TableOfContents(ArcGlobe.Globe)

				If (Not tableOfContents.LayerExists(GraphicsLayerName)) Then
					tableOfContents.ConstructLayer(GraphicsLayerName)
				End If

				Dim layer As New Layer(tableOfContents(GraphicsLayerName))

				If _polygonGeometry.PointCount = 1 Then
					Dim pointElement As New PointElement(pointGeometry.Geometry, PointElementSize, PointElementStyle)

					layer.AddElement(pointElement.Element, pointElement.ElementProperties)
				Else
					layer.RemoveElement(layer.ElementCount - 1)

					Dim polylineGeometry As New PolylineGeometry(_polygonGeometry.Geometry)

					Dim polylineElement As New PolylineElement(polylineGeometry.Geometry, PolylineElementWidth, PolylineElementStyle)

					layer.AddElement(polylineElement.Element, polylineElement.ElementProperties)
				End If

				ArcGlobe.Globe.GlobeDisplay.RefreshViewers()
			End If
		End Sub

		Protected Overrides Sub OnDoubleClick()
			If _polygonGeometry.PointCount > 2 Then
				Dim tableOfContents As New TableOfContents(ArcGlobe.Globe)

				If tableOfContents.LayerExists("Globe Graphics") Then
					Dim layer As New Layer(tableOfContents("Globe Graphics"))

					layer.RemoveElement(layer.ElementCount - 1)

					_polygonGeometry.Close()

                    Dim polygonElement As New PolygonElement(_polygonGeometry.Geometry, ESRI.ArcGIS.Display.esriSimpleFillStyle.esriSFSSolid)

					layer.AddElement(polygonElement.Element, polygonElement.ElementProperties)

					_polygonGeometry = Nothing

					ArcGlobe.Globe.GlobeDisplay.RefreshViewers()
				End If
			End If
		End Sub
	End Class

End Namespace
