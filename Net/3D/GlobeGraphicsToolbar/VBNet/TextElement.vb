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
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Analyst3D
Imports ESRI.ArcGIS.Display
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.GlobeCore
Imports ESRI.ArcGIS.ADF.COMSupport
Imports System.Drawing
Imports System


Namespace GlobeGraphicsToolbar
	Public Class TextElement
		Private _element As IElement
		Private _elementProperties As IGlobeGraphicsElementProperties

		Public Sub New(ByVal geometry As IGeometry, ByVal text As String, ByVal size As Single)
			_element = GetElement(geometry, text, size)
			_elementProperties = GetElementProperties()
		End Sub

		Private Function GetElement(ByVal geometry As IGeometry, ByVal text As String, ByVal size As Single) As IElement
			Dim element As IElement

			Dim textElement As ITextElement = New TextElementClass()
			element = TryCast(textElement, IElement)

			Dim textSymbol As ITextSymbol = New TextSymbolClass()
			textSymbol.Color = ColorSelection.GetColor()
			textSymbol.Size = Convert.ToDouble(size)
			textSymbol.Font = GetIFontDisp(size)
			textSymbol.HorizontalAlignment = GetHorizontalAlignment()
			textSymbol.VerticalAlignment = GetVerticalAlignment()

			element.Geometry = geometry

			textElement.Symbol = textSymbol
			textElement.Text = text

			Return element
		End Function

		Private Function GetIFontDisp(ByVal size As Single) As stdole.IFontDisp
			Const FontFamilyName As String = "Arial"
			Const FontStyle As FontStyle = FontStyle.Bold

			Dim font As New Font(FontFamilyName, size, FontStyle)

			Return TryCast(OLE.GetIFontDispFromFont(font), stdole.IFontDisp)
		End Function

        Private Function GetHorizontalAlignment() As ESRI.ArcGIS.Display.esriTextHorizontalAlignment
            Const HorizontalAlignment As ESRI.ArcGIS.Display.esriTextHorizontalAlignment = ESRI.ArcGIS.Display.esriTextHorizontalAlignment.esriTHACenter

            Return HorizontalAlignment
        End Function

        Private Function GetVerticalAlignment() As ESRI.ArcGIS.Display.esriTextVerticalAlignment
            Const VerticalAlignment As ESRI.ArcGIS.Display.esriTextVerticalAlignment = ESRI.ArcGIS.Display.esriTextVerticalAlignment.esriTVABaseline

            Return VerticalAlignment
        End Function

		Private Function GetElementProperties() As IGlobeGraphicsElementProperties
			Dim elementProperties As IGlobeGraphicsElementProperties = New GlobeGraphicsElementPropertiesClass()
			elementProperties.FixedScreenSize = True
			elementProperties.DrapeElement = True

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