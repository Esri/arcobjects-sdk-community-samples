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