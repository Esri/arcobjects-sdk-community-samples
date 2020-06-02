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