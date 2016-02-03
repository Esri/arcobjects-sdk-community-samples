Imports Microsoft.VisualBasic
Imports System
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.Display
Imports ESRI.ArcGIS.Analyst3D
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.esriSystem


Public Class ElementUtilities
    Private Const HighResolution As Double = 1
    Private Const Units As esriUnits = esriUnits.esriUnknownUnits

    Private Sub New()
    End Sub
    Public Shared Function ConstructPolylineElement(ByVal geometry As IGeometry, ByVal color As IColor, ByVal style As esriSimple3DLineStyle, ByVal width As Double) As IElement
        Dim simpleLine3DSymbol As ISimpleLine3DSymbol = New SimpleLine3DSymbolClass()
        simpleLine3DSymbol.Style = style
        simpleLine3DSymbol.ResolutionQuality = HighResolution

        Dim lineSymbol As ILineSymbol = TryCast(simpleLine3DSymbol, ILineSymbol)
        lineSymbol.Color = color
        lineSymbol.Width = width

        Dim line3DPlacement As ILine3DPlacement = TryCast(lineSymbol, ILine3DPlacement)
        line3DPlacement.Units = Units

        Dim lineElement As ILineElement = New LineElementClass()
        lineElement.Symbol = lineSymbol

        Dim element As IElement = TryCast(lineElement, IElement)
        element.Geometry = geometry

        Return element
    End Function

    Public Shared Function ConstructMultiPatchElement(ByVal geometry As IGeometry, ByVal color As IColor) As IElement
        Dim simpleFillSymbol As ISimpleFillSymbol = New SimpleFillSymbolClass()
        simpleFillSymbol.Color = color

        Dim element As IElement = New MultiPatchElementClass()
        element.Geometry = geometry

        Dim fillShapeElement As IFillShapeElement = TryCast(element, IFillShapeElement)
        fillShapeElement.Symbol = simpleFillSymbol

        Return element
    End Function
End Class
