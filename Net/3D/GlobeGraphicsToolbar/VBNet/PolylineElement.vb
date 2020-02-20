'Copyright 2019 Esri

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
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.GlobeCore

Namespace GlobeGraphicsToolbar
	Public Class PolylineElement
		Private _element As IElement
		Private _elementProperties As IGlobeGraphicsElementProperties

        Public Sub New(ByVal geometry As IGeometry, ByVal width As Double, ByVal simpleLineStyle As ESRI.ArcGIS.Display.esriSimpleLineStyle)
            _element = GetElement(geometry, width, simpleLineStyle)
            _elementProperties = GetElementProperties()
        End Sub

        Private Function GetElement(ByVal geometry As IGeometry, ByVal width As Double, ByVal simpleLineStyle As ESRI.ArcGIS.Display.esriSimpleLineStyle) As IElement
            Dim element As IElement

            Dim lineElement As ILineElement = New LineElementClass()
            element = TryCast(lineElement, IElement)

            Dim simpleLineSymbol As ISimpleLineSymbol = New SimpleLineSymbolClass()
            simpleLineSymbol.Style = simpleLineStyle
            simpleLineSymbol.Color = ColorSelection.GetColor()
            simpleLineSymbol.Width = width

            element.Geometry = geometry

            Dim lineSymbol As ILineSymbol = TryCast(simpleLineSymbol, ILineSymbol)

            lineElement.Symbol = lineSymbol

            Return element
        End Function

		Private Function GetElementProperties() As IGlobeGraphicsElementProperties
			Dim elementProperties As IGlobeGraphicsElementProperties = New GlobeGraphicsElementPropertiesClass()
			elementProperties.Rasterize = True

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