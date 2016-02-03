Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.IO
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.GlobeCore
Imports ESRI.ArcGIS.Analyst3D
Imports ESRI.ArcGIS.Carto

Namespace GlobeGraphicsToolbar
	Public Class PointTool
		Inherits ESRI.ArcGIS.Desktop.AddIns.Tool
		Private Const LeftButton As Integer = 1
        Private Const GeographicCoordinateSystem As ESRI.ArcGIS.Geometry.esriSRGeoCSType = ESRI.ArcGIS.Geometry.esriSRGeoCSType.esriSRGeoCS_WGS1984
		Private Const PointElementSize As Double = 100000
		Private Const PointElementStyle As esriSimple3DMarkerStyle = esriSimple3DMarkerStyle.esriS3DMSSphere
		Private Const GraphicsLayerName As String = "Globe Graphics"

		Public Sub New()
		End Sub

		Protected Overrides Sub OnUpdate()
			Enabled = ArcGlobe.Application IsNot Nothing
		End Sub

		Protected Overrides Sub OnMouseUp(ByVal arg As ESRI.ArcGIS.Desktop.AddIns.Tool.MouseEventArgs)
			If arg.Button = System.Windows.Forms.MouseButtons.Left Then
				Dim geographicCoordinates As New GeographicCoordinates(ArcGlobe.Globe, arg.X, arg.Y)

				Dim spatialReferenceFactory As New SpatialReferenceFactory(CInt(Fix(GeographicCoordinateSystem)))

				Dim pointGeometry As New PointGeometry(geographicCoordinates.Longitude, geographicCoordinates.Latitude, geographicCoordinates.AltitudeInKilometers, spatialReferenceFactory.SpatialReference)

				Dim pointElement As New PointElement(pointGeometry.Geometry, PointElementSize, PointElementStyle)

				Dim tableOfContents As New TableOfContents(ArcGlobe.Globe)

				If (Not tableOfContents.LayerExists(GraphicsLayerName)) Then
					tableOfContents.ConstructLayer(GraphicsLayerName)
				End If

				Dim layer As New Layer(tableOfContents(GraphicsLayerName))

				layer.AddElement(pointElement.Element, pointElement.ElementProperties)

				ArcGlobe.Globe.GlobeDisplay.RefreshViewers()
			End If
		End Sub
	End Class

End Namespace
