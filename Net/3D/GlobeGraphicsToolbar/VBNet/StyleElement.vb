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
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Analyst3D
Imports ESRI.ArcGIS.Display
Imports ESRI.ArcGIS.GlobeCore

Namespace GlobeGraphicsToolbar
	Public Class StyleElement
		Private _element As IElement
		Private _elementProperties As IGlobeGraphicsElementProperties

		Public Sub New(ByVal geometry As IGeometry, ByVal size As Double, ByVal styleGalleryItem As IStyleGalleryItem)
			_element = GetElement(geometry, size, styleGalleryItem)
			_elementProperties = GetElementProperties()
		End Sub

		Private Function GetElement(ByVal geometry As IGeometry, ByVal size As Double, ByVal styleGalleryItem As IStyleGalleryItem) As IElement
			Dim element As IElement

			Dim markerElement As IMarkerElement = New MarkerElementClass()
			element = TryCast(markerElement, IElement)

			Dim markerSymbol As IMarkerSymbol = TryCast(styleGalleryItem.Item, IMarkerSymbol)
			markerSymbol.Size = size

			element.Geometry = geometry

			markerElement.Symbol = markerSymbol

			Return element
		End Function

		Private Function GetElementProperties() As IGlobeGraphicsElementProperties
			Dim elementProperties As IGlobeGraphicsElementProperties = New GlobeGraphicsElementPropertiesClass()
			elementProperties.DrapeElement = True
			elementProperties.Illuminate = True

			Return elementProperties
		End Function

		Public ReadOnly Property Element() As IElement
			Get
				Return _element
			End Get
		End Property

		Public ReadOnly Property ElementProperties() As IGlobeGraphicsElementProperties
			Get
				Return _elementProperties
			End Get
		End Property
	End Class
End Namespace