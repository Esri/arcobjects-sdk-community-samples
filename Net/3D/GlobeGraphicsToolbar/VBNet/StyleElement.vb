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