'Copyright 2016 Esri

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


Public Class RingExamples
    Private Shared _missing As Object = Type.Missing

    Private Sub New()
    End Sub
    Public Shared Function GetExample1() As IGeometry
        'Ring: Upright Rectangle

        Dim multiPatchGeometryCollection As IGeometryCollection = New MultiPatchClass()

        Dim ringPointCollection As IPointCollection = New RingClass()

        ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-7.5, 0, 0), _missing, _missing)
        ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-7.5, 0, 7.5), _missing, _missing)
        ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(7.5, 0, 7.5), _missing, _missing)
        ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(7.5, 0, 0), _missing, _missing)
        ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-7.5, 0, 0), _missing, _missing)

        multiPatchGeometryCollection.AddGeometry(TryCast(ringPointCollection, IGeometry), _missing, _missing)

        Return TryCast(multiPatchGeometryCollection, IGeometry)
    End Function

    Public Shared Function GetExample2() As IGeometry
        'Ring: Octagon Lying In XY Plane

        Dim multiPatchGeometryCollection As IGeometryCollection = New MultiPatchClass()

        Dim ringPointCollection As IPointCollection = New RingClass()

        ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-7.5, 7.5, 0), _missing, _missing)
        ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, 8.5, 0), _missing, _missing)
        ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(7.5, 7.5, 0), _missing, _missing)
        ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(8.5, 0, 0), _missing, _missing)
        ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(7.5, -7.5, 0), _missing, _missing)
        ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, -8.5, 0), _missing, _missing)
        ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-7.5, -7.5, 0), _missing, _missing)
        ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-8.5, 0, 0), _missing, _missing)
        ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-7.5, 7.5, 0), _missing, _missing)

        multiPatchGeometryCollection.AddGeometry(TryCast(ringPointCollection, IGeometry), _missing, _missing)

        Return TryCast(multiPatchGeometryCollection, IGeometry)
    End Function

    Public Shared Function GetExample3() As IGeometry
        'Ring: Octagon With Non-Coplanar Points

        Dim multiPatchGeometryCollection As IGeometryCollection = New MultiPatchClass()

        Dim ringPointCollection As IPointCollection = New RingClass()

        ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-7.5, 7.5, 5), _missing, _missing)
        ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, 8.5, 0), _missing, _missing)
        ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(7.5, 7.5, 5), _missing, _missing)
        ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(8.5, 0, 0), _missing, _missing)
        ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(7.5, -7.5, 5), _missing, _missing)
        ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, -8.5, 0), _missing, _missing)
        ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-7.5, -7.5, 5), _missing, _missing)
        ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-8.5, 0, 0), _missing, _missing)
        ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-7.5, 7.5, 5), _missing, _missing)

        multiPatchGeometryCollection.AddGeometry(TryCast(ringPointCollection, IGeometry), _missing, _missing)

        Return TryCast(multiPatchGeometryCollection, IGeometry)
    End Function

    Public Shared Function GetExample4() As IGeometry
        'Ring: Maze Lying On XY Plane

        Dim multiPatchGeometryCollection As IGeometryCollection = New MultiPatchClass()

        Dim ringPointCollection As IPointCollection = New RingClass()

        ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-10, 10, 0), _missing, _missing)
        ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(10, 10, 0), _missing, _missing)
        ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(10, -10, 0), _missing, _missing)
        ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-10, -10, 0), _missing, _missing)
        ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-10, 6, 0), _missing, _missing)
        ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(6, 6, 0), _missing, _missing)
        ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(6, -6, 0), _missing, _missing)
        ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-6, -6, 0), _missing, _missing)
        ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-6, 2, 0), _missing, _missing)
        ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-6, 2, 0), _missing, _missing)
        ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, 2, 0), _missing, _missing)
        ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, 0, 0), _missing, _missing)
        ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-4, 0, 0), _missing, _missing)
        ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-4, -4, 0), _missing, _missing)
        ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(4, -4, 0), _missing, _missing)
        ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(4, 4, 0), _missing, _missing)
        ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-8, 4, 0), _missing, _missing)
        ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-8, -8, 0), _missing, _missing)
        ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(8, -8, 0), _missing, _missing)
        ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(8, 8, 0), _missing, _missing)
        ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-10, 8, 0), _missing, _missing)
        ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-10, 10, 0), _missing, _missing)

        multiPatchGeometryCollection.AddGeometry(TryCast(ringPointCollection, IGeometry), _missing, _missing)

        Return TryCast(multiPatchGeometryCollection, IGeometry)
    End Function

    Public Shared Function GetExample5() As IGeometry
        'Ring: Maze With Non-Coplanar Points

        Dim multiPatchGeometryCollection As IGeometryCollection = New MultiPatchClass()

        Dim ringPointCollection As IPointCollection = New RingClass()

        ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-10, 10, 0), _missing, _missing)
        ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(10, 10, 5), _missing, _missing)
        ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(10, -10, -5), _missing, _missing)
        ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-10, -10, 0), _missing, _missing)
        ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-10, 6, 5), _missing, _missing)
        ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(6, 6, -5), _missing, _missing)
        ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(6, -6, 0), _missing, _missing)
        ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-6, -6, 5), _missing, _missing)
        ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-6, 2, -5), _missing, _missing)
        ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-6, 2, 0), _missing, _missing)
        ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, 2, 5), _missing, _missing)
        ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, 0, -5), _missing, _missing)
        ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-4, 0, 0), _missing, _missing)
        ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-4, -4, 5), _missing, _missing)
        ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(4, -4, -5), _missing, _missing)
        ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(4, 4, 0), _missing, _missing)
        ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-8, 4, 5), _missing, _missing)
        ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-8, -8, -5), _missing, _missing)
        ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(8, -8, 0), _missing, _missing)
        ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(8, 8, 5), _missing, _missing)
        ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-10, 8, -5), _missing, _missing)
        ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-10, 10, 0), _missing, _missing)

        multiPatchGeometryCollection.AddGeometry(TryCast(ringPointCollection, IGeometry), _missing, _missing)

        Return TryCast(multiPatchGeometryCollection, IGeometry)
    End Function
End Class
