Imports Microsoft.VisualBasic
Imports System
Imports ESRI.ArcGIS.Geometry

Namespace GlobeGraphicsToolbar
	Public Class SpatialReferenceFactory
		Private _spatialReference As ISpatialReference

		Public Sub New(ByVal xyCoordinateSystem As Integer)
			_spatialReference = GetSpatialReference(xyCoordinateSystem)
		End Sub

		Private Function GetSpatialReference(ByVal xyCoordinateSystem As Integer) As ISpatialReference
			Const IsHighPrecision As Boolean = True

			Dim spatialReference As ISpatialReference

			Dim spatialReferenceFactory As ISpatialReferenceFactory3 = New SpatialReferenceEnvironmentClass()
			spatialReference = spatialReferenceFactory.CreateSpatialReference(xyCoordinateSystem)

			Dim controlPrecision As IControlPrecision2 = TryCast(spatialReference, IControlPrecision2)
			controlPrecision.IsHighPrecision = IsHighPrecision

			Dim spatialReferenceResolution As ISpatialReferenceResolution = TryCast(spatialReference, ISpatialReferenceResolution)
			spatialReferenceResolution.ConstructFromHorizon()
			spatialReferenceResolution.SetDefaultXYResolution()
			spatialReferenceResolution.SetDefaultZResolution()
			spatialReferenceResolution.SetDefaultMResolution()

			Dim spatialReferenceTolerance As ISpatialReferenceTolerance = TryCast(spatialReference, ISpatialReferenceTolerance)
			spatialReferenceTolerance.SetDefaultXYTolerance()
			spatialReferenceTolerance.SetDefaultZTolerance()
			spatialReferenceTolerance.SetDefaultMTolerance()

			Return spatialReference
		End Function

		Public ReadOnly Property SpatialReference() As ISpatialReference
			Get
				Return _spatialReference
			End Get
		End Property

	End Class
End Namespace