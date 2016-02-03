Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.IO
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.ADF.CATIDs
Imports System.Windows.Forms
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.Analyst3D
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Display

Namespace GlobeGraphicsToolbar
	Public Class PolylineTool
		Inherits ESRI.ArcGIS.Desktop.AddIns.Tool
		Private _polylineGeometry As PolylineGeometry = Nothing
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

				If _polylineGeometry Is Nothing Then
					_polylineGeometry = New PolylineGeometry(spatialReferenceFactory.SpatialReference)
				End If

				_polylineGeometry.AddPoint(TryCast(pointGeometry.Geometry, IPoint))

				Dim tableOfContents As New TableOfContents(ArcGlobe.Globe)

				If (Not tableOfContents.LayerExists(GraphicsLayerName)) Then
					tableOfContents.ConstructLayer(GraphicsLayerName)
				End If

				Dim layer As New Layer(tableOfContents(GraphicsLayerName))

				If _polylineGeometry.PointCount = 1 Then
					Dim pointElement As New PointElement(pointGeometry.Geometry, PointElementSize, PointElementStyle)

					layer.AddElement(pointElement.Element, pointElement.ElementProperties)
				Else
					layer.RemoveElement(layer.ElementCount - 1)

					Dim polylineElement As New PolylineElement(_polylineGeometry.Geometry, PolylineElementWidth, PolylineElementStyle)

					layer.AddElement(polylineElement.Element, polylineElement.ElementProperties)
				End If

				ArcGlobe.Globe.GlobeDisplay.RefreshViewers()
			End If
		End Sub

		Protected Overrides Sub OnDoubleClick()
			_polylineGeometry = Nothing
		End Sub
	End Class

End Namespace
