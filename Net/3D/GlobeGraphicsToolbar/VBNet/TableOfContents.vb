Imports Microsoft.VisualBasic
Imports System
Imports ESRI.ArcGIS.GlobeCore
Imports ESRI.ArcGIS.Analyst3D
Imports ESRI.ArcGIS.Carto

Namespace GlobeGraphicsToolbar
	Public Class TableOfContents
		Private _scene As IScene

		Public Sub New(ByVal globe As IGlobe)
			_scene = GetScene(globe)
		End Sub

		Private Function GetScene(ByVal globe As IGlobe) As IScene
			Dim scene As IScene

			scene = TryCast(globe, IScene)

			Return scene
		End Function

		Public Function LayerExists(ByVal name As String) As Boolean
			Dim exists As Boolean = False

			For i As Integer = 0 To _scene.LayerCount - 1
				Dim layer As ILayer = _scene.Layer(i)

				If layer.Name = name Then
					exists = True
					Exit For
				End If
			Next i

			Return exists
		End Function

		Public Sub ConstructLayer(ByVal name As String)
			Dim globeGraphicsLayer As IGlobeGraphicsLayer = New GlobeGraphicsLayerClass()

			Dim layer As ILayer = TryCast(globeGraphicsLayer, ILayer)

			layer.Name = name

			_scene.AddLayer(layer, True)
		End Sub

		Default Public ReadOnly Property Item(ByVal name As String) As ILayer
			Get
				Return GetLayer(name)
			End Get
		End Property

		Private Function GetLayer(ByVal name As String) As ILayer
			Dim layer As ILayer = Nothing

			For i As Integer = 0 To _scene.LayerCount - 1
				Dim currentLayer As ILayer = _scene.Layer(i)

				If currentLayer.Name = name Then
					layer = currentLayer
					Exit For
				End If
			Next i

			Return layer
		End Function
	End Class
End Namespace