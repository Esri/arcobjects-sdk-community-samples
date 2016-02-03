Imports Microsoft.VisualBasic
Imports System
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Analyst3D
Imports ESRI.ArcGIS.Display
Imports ESRI.ArcGIS.GlobeCore

Namespace GlobeGraphicsToolbar
	Public Class PointElement
		Private _element As IElement
		Private _elementProperties As IGlobeGraphicsElementProperties

        Public Sub New(ByVal geometry As IGeometry, ByVal size As Double, ByVal simpleMarkerStyle As ESRI.ArcGIS.Display.esriSimpleMarkerStyle)
            _element = GetElement(geometry, size, simpleMarkerStyle)
            _elementProperties = GetElementProperties()
        End Sub

        Public Sub New(ByVal geometry As IGeometry, ByVal size As Double, ByVal simple3DMarkerStyle As ESRI.ArcGIS.Analyst3D.esriSimple3DMarkerStyle)
            _element = GetElement(geometry, size, simple3DMarkerStyle)
            _elementProperties = GetElementProperties()
        End Sub

        Private Function GetElement(ByVal geometry As IGeometry, ByVal size As Double, ByVal simpleMarkerStyle As ESRI.ArcGIS.Display.esriSimpleMarkerStyle) As IElement
            Dim element As IElement

            Dim markerElement As IMarkerElement = New MarkerElementClass()
            element = TryCast(markerElement, IElement)

            Dim simpleMarkerSymbol As ISimpleMarkerSymbol = New SimpleMarkerSymbolClass()
            simpleMarkerSymbol.Style = simpleMarkerStyle
            simpleMarkerSymbol.Color = ColorSelection.GetColor()
            simpleMarkerSymbol.Size = size

            element.Geometry = geometry

            markerElement.Symbol = simpleMarkerSymbol

            Return element
        End Function

        Private Function GetElement(ByVal geometry As IGeometry, ByVal size As Double, ByVal simple3DMarkerStyle As ESRI.ArcGIS.Analyst3D.esriSimple3DMarkerStyle) As IElement
            Dim element As IElement

            Dim markerElement As IMarkerElement = New MarkerElementClass()
            element = TryCast(markerElement, IElement)

            Dim simpleMarker3DSymbol As ISimpleMarker3DSymbol = New SimpleMarker3DSymbolClass()
            simpleMarker3DSymbol.Style = simple3DMarkerStyle
            simpleMarker3DSymbol.ResolutionQuality = GetResolutionQuality()

            Dim markerSymbol As IMarkerSymbol = TryCast(simpleMarker3DSymbol, IMarkerSymbol)
            markerSymbol.Color = ColorSelection.GetColor()
            markerSymbol.Size = size

            Dim marker3DPlacement As IMarker3DPlacement = TryCast(markerSymbol, IMarker3DPlacement)
            SetMarker3DPlacement(marker3DPlacement, markerSymbol.Size)

            element.Geometry = geometry

            markerElement.Symbol = markerSymbol

            Return element
        End Function

		Private Function GetResolutionQuality() As Double
			Const HighQuality As Double = 1.0

			Return HighQuality
		End Function

		Private Sub SetMarker3DPlacement(ByVal marker3DPlacement As IMarker3DPlacement, ByVal size As Double)
			Const XOffset As Double = 0
			Const YOffset As Double = 0

			marker3DPlacement.XOffset = XOffset
			marker3DPlacement.YOffset = YOffset
			marker3DPlacement.ZOffset = size / 2
		End Sub

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