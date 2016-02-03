Imports Microsoft.VisualBasic
Imports ESRI.ArcGIS.Geometry
Imports System

Namespace GlobeGraphicsToolbar
	Public Class PolygonGeometry
		Private _geometry As IGeometry

		Public Sub New(ByVal spatialReference As ISpatialReference)
			_geometry = GetGeometry(spatialReference)
		End Sub

		Private Function GetGeometry(ByVal spatialReference As ISpatialReference) As IGeometry
			Dim geometry As IGeometry

			Dim polygon As IPolygon = New PolygonClass()

			polygon.SpatialReference = spatialReference

			geometry = TryCast(polygon, IGeometry)

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

		Public Sub Close()
			Dim polygon As IPolygon = TryCast(_geometry, IPolygon)

			polygon.Close()
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