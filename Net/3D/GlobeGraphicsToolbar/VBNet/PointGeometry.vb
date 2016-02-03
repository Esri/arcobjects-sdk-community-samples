Imports Microsoft.VisualBasic
Imports System
Imports ESRI.ArcGIS.Geometry

Namespace GlobeGraphicsToolbar
	Public Class PointGeometry
		Private _geometry As IGeometry

		Public Sub New(ByVal longitude As Double, ByVal latitude As Double, ByVal altitudeInKilometers As Double, ByVal spatialReference As ISpatialReference)
			_geometry = GetGeometry(longitude, latitude, altitudeInKilometers, spatialReference)
		End Sub

		Private Function GetGeometry(ByVal longitude As Double, ByVal latitude As Double, ByVal altitudeInKilometers As Double, ByVal spatialReference As ISpatialReference) As IGeometry
			Dim geometry As IGeometry

			Dim point As IPoint = New PointClass()

			point.X = longitude
			point.Y = latitude
			point.Z = altitudeInKilometers

			point.SpatialReference = spatialReference

			geometry = TryCast(point, IGeometry)

			MakeZAware(geometry)

			Return geometry
		End Function

		Private Sub MakeZAware(ByVal geometry As IGeometry)
			Dim zAware As IZAware = TryCast(geometry, IZAware)
			zAware.ZAware = True
		End Sub

		Public ReadOnly Property Geometry() As IGeometry
			Get
				Return _geometry
			End Get
		End Property
	End Class
End Namespace