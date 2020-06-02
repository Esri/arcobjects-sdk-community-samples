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
Imports ESRI.ArcGIS.Analyst3D
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.GlobeCore

Namespace GlobeGraphicsToolbar
	Public Class Layer
		Private _layer As ILayer

		Public Sub New(ByVal layer As ILayer)
			_layer = layer
		End Sub

		Public Sub AddElement(ByVal element As IElement, ByVal elementProperties As IGlobeGraphicsElementProperties)
			Dim elementIndex As Integer

			Dim globeGraphicsLayer As IGlobeGraphicsLayer = TryCast(_layer, IGlobeGraphicsLayer)
			globeGraphicsLayer.AddElement(element, elementProperties, elementIndex)
		End Sub

		Public Sub RemoveElement(ByVal index As Integer)
			Dim graphicsContainer3D As IGraphicsContainer3D = TryCast(_layer, IGraphicsContainer3D)
			graphicsContainer3D.DeleteElement(Me(index))
		End Sub

		Default Public ReadOnly Property Item(ByVal i As Integer) As IElement
			Get
				Dim graphicsContainer3D As IGraphicsContainer3D = TryCast(_layer, IGraphicsContainer3D)
				Return graphicsContainer3D.Element(i)
			End Get
		End Property

		Public ReadOnly Property ElementCount() As Integer
			Get
				Dim graphicsContainer3D As IGraphicsContainer3D = TryCast(_layer, IGraphicsContainer3D)
				Return graphicsContainer3D.ElementCount
			End Get
		End Property
	End Class
End Namespace