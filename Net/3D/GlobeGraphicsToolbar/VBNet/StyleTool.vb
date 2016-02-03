Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.IO
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.ADF.CATIDs
Imports System.Windows.Forms
Imports Microsoft.Win32
Imports ESRI.ArcGIS.Display
Imports ESRI.ArcGIS.Analyst3D
Imports ESRI.ArcGIS.Geometry

Namespace GlobeGraphicsToolbar
	Public Class StyleTool
		Inherits ESRI.ArcGIS.Desktop.AddIns.Tool
		Private Const LeftButton As Integer = 1
		Private Const GeographicCoordinateSystem As esriSRGeoCSType = esriSRGeoCSType.esriSRGeoCS_WGS1984
		Private Const StyleElementSize As Double = 50000
		Private Const GraphicsLayerName As String = "Globe Graphics"

		Public Sub New()
		End Sub

		Protected Overrides Sub OnUpdate()

		End Sub

		Protected Overrides Sub OnMouseUp(ByVal arg As ESRI.ArcGIS.Desktop.AddIns.Tool.MouseEventArgs)
			If arg.Button = MouseButtons.Left Then
				Dim geographicCoordinates As New GeographicCoordinates(ArcGlobe.Globe, arg.X, arg.Y)

				Dim spatialReferenceFactory As New SpatialReferenceFactory(CInt(Fix(GeographicCoordinateSystem)))

				Dim pointGeometry As New PointGeometry(geographicCoordinates.Longitude, geographicCoordinates.Latitude, geographicCoordinates.AltitudeInKilometers, spatialReferenceFactory.SpatialReference)

				Dim styleGalleryItem As IStyleGalleryItem = StyleGallerySelection.GetStyleGalleryItem()

				If styleGalleryItem IsNot Nothing Then
					Dim styleElement As New StyleElement(pointGeometry.Geometry, StyleElementSize, styleGalleryItem)

					Dim tableOfContents As New TableOfContents(ArcGlobe.Globe)

					If (Not tableOfContents.LayerExists(GraphicsLayerName)) Then
						tableOfContents.ConstructLayer(GraphicsLayerName)
					End If

					Dim layer As New Layer(tableOfContents(GraphicsLayerName))

					layer.AddElement(styleElement.Element, styleElement.ElementProperties)

					ArcGlobe.Globe.GlobeDisplay.RefreshViewers()
				End If
			End If
		End Sub
	End Class

End Namespace
