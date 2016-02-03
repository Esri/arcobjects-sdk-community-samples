Imports Microsoft.VisualBasic
Imports ESRI.ArcGIS.Geometry
Imports System

Namespace GlobeGraphicsToolbar
	Public Class PolylineGeometry
		Private _geometry As IGeometry

		Public Sub New(ByVal spatialReference As ISpatialReference)
			_geometry = GetGeometry(spatialReference)
		End Sub

		Public Sub New(ByVal baseGeometry As IGeometry)
			_geometry = GetGeometry(baseGeometry)
		End Sub

		Private Function GetGeometry(ByVal spatialReference As ISpatialReference) As IGeometry
			Dim geometry As IGeometry

			Dim polyline As IPolyline = New PolylineClass()

			polyline.SpatialReference = spatialReference

			geometry = TryCast(polyline, IGeometry)

			MakeZAware(geometry)

			Return geometry
		End Function

		Private Function GetGeometry(ByVal baseGeometry As IGeometry) As IGeometry
			Dim geometry As IGeometry

			Dim polyline As IPolyline = New PolylineClass()

			polyline.SpatialReference = baseGeometry.SpatialReference

			geometry = TryCast(polyline, IGeometry)

			Dim targetPointCollection As IPointCollection = TryCast(geometry, IPointCollection)

			Dim basePointCollection As IPointCollection = TryCast(baseGeometry, IPointCollection)

			Dim missing As Object = Type.Missing

			For i As Integer = 0 To basePointCollection.PointCount - 1
				targetPointCollection.AddPoint(basePointCollection.Point(i), missing, missing)
			Next i

			MakeZAware(geometry)

			Return geometry
		End Function

		Private Sub MakeZAware(ByVal geometry As IGeometry)
			Dim zAware As IZAware = TryCast(geometry, IZAware)
			zAware.ZAware = True
		End Sub

		Public Sub AddPoint(ByVal point As IPoint)
			Dim pointCollection As IPointCollection = TryCast(_geometry, IPointCollection)

			Dim missing As Object = Type.Missing

			pointCollection.AddPoint(point, missing, missing)
		End Sub

		Public ReadOnly Property Geometry() As IGeometry
			Get
				Return _geometry
			End Get
		End Property

		Public ReadOnly Property PointCount() As Integer
			Get
                Dim numPoints As Integer

                Dim pointCollection As IPointCollection = TryCast(_geometry, IPointCollection)

                numPoints = pointCollection.PointCount

                Return numPoints
            End Get
		End Property
	End Class
End Namespace