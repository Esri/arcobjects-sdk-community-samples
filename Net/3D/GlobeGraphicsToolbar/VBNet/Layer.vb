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