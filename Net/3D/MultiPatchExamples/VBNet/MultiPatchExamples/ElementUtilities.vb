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
