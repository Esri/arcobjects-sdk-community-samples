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