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
Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.IO
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.ADF.CATIDs
Imports System.Windows.Forms
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.Analyst3D
Imports ESRI.ArcGIS.GlobeCore
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Display
Imports Microsoft.VisualBasic

Namespace GlobeGraphicsToolbar
	Public Class TextTool
		Inherits ESRI.ArcGIS.Desktop.AddIns.Tool
		Private Const LeftButton As Integer = 1
        Private Const GeographicCoordinateSystem As ESRI.ArcGIS.Geometry.esriSRGeoCSType = ESRI.ArcGIS.Geometry.esriSRGeoCSType.esriSRGeoCS_WGS1984
		Private Const TextElementSize As Single = 10
		Private Const GraphicsLayerName As String = "Globe Graphics"
		Private _missing As Object = Type.Missing

		Public Sub New()
		End Sub

		Protected Overrides Sub OnUpdate()

		End Sub

		Protected Overrides Sub OnMouseDown(ByVal arg As ESRI.ArcGIS.Desktop.AddIns.Tool.MouseEventArgs)
			If arg.Button = MouseButtons.Left Then
				Dim geographicCoordinates As New GeographicCoordinates(ArcGlobe.Globe, arg.X, arg.Y)

				Dim spatialReferenceFactory As New SpatialReferenceFactory(CInt(Fix(GeographicCoordinateSystem)))

				Dim pointGeometry As New PointGeometry(geographicCoordinates.Longitude, geographicCoordinates.Latitude, geographicCoordinates.AltitudeInKilometers, spatialReferenceFactory.SpatialReference)

				Dim textForm As New TextForm()

				Dim dialogResult As DialogResult = textForm.ShowDialog()

				If textForm.InputText.Length > 0 Then
					Dim textElement As New TextElement(pointGeometry.Geometry, textForm.InputText, TextElementSize)

					Dim tableOfContents As New TableOfContents(ArcGlobe.Globe)

					If (Not tableOfContents.LayerExists(GraphicsLayerName)) Then
						tableOfContents.ConstructLayer(GraphicsLayerName)
					End If

					Dim layer As New Layer(tableOfContents(GraphicsLayerName))

					layer.AddElement(textElement.Element, textElement.ElementProperties)

					ArcGlobe.Globe.GlobeDisplay.RefreshViewers()
				End If
			End If
		End Sub
	End Class

End Namespace
