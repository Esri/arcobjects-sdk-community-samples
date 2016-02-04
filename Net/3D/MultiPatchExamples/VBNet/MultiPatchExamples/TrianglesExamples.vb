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


Public Class TrianglesExamples
    Private Shared _missing As Object = Type.Missing

    Private Sub New()
    End Sub
    Public Shared Function GetExample1() As IGeometry
        'Triangles: One Triangle Lying On XY Plane

        Dim multiPatchGeometryCollection As IGeometryCollection = New MultiPatchClass()

        Dim trianglesPointCollection As IPointCollection = New TrianglesClass()

        trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(2.5, 2.5, 0), _missing, _missing)
        trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(7.5, 7.5, 0), _missing, _missing)
        trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(7.5, 2.5, 0), _missing, _missing)

        multiPatchGeometryCollection.AddGeometry(TryCast(trianglesPointCollection, IGeometry), _missing, _missing)

        Return TryCast(multiPatchGeometryCollection, IGeometry)
    End Function

    Public Shared Function GetExample2() As IGeometry
        'Triangles: One Upright Triangle

        Dim multiPatchGeometryCollection As IGeometryCollection = New MultiPatchClass()

        Dim trianglesPointCollection As IPointCollection = New TrianglesClass()

        trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(2.5, 2.5, 0), _missing, _missing)
        trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(2.5, 2.5, 7.5), _missing, _missing)
        trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(7.5, 2.5, 7.5), _missing, _missing)

        multiPatchGeometryCollection.AddGeometry(TryCast(trianglesPointCollection, IGeometry), _missing, _missing)

        Return TryCast(multiPatchGeometryCollection, IGeometry)
    End Function

    Public Shared Function GetExample3() As IGeometry
        'Triangles: Three Upright Triangles

        Dim multiPatchGeometryCollection As IGeometryCollection = New MultiPatchClass()

        Dim trianglesPointCollection As IPointCollection = New TrianglesClass()

        'Triangle 1

        trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(2.5, 2.5, 0), _missing, _missing)
        trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(2.5, 2.5, 7.5), _missing, _missing)
        trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(7.5, 2.5, 7.5), _missing, _missing)

        'Triangle 2

        trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-7.5, 2.5, 0), _missing, _missing)
        trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-7.5, 2.5, 7.5), _missing, _missing)
        trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-2.5, 2.5, 7.5), _missing, _missing)

        'Triangle 3

        trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-2.5, -2.5, 0), _missing, _missing)
        trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-2.5, -2.5, 7.5), _missing, _missing)
        trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(2.5, -2.5, 7.5), _missing, _missing)

        multiPatchGeometryCollection.AddGeometry(TryCast(trianglesPointCollection, IGeometry), _missing, _missing)

        Return TryCast(multiPatchGeometryCollection, IGeometry)
    End Function

    Public Shared Function GetExample4() As IGeometry
        'Triangles: Six Triangles Lying In Different Planes

        Dim multiPatchGeometryCollection As IGeometryCollection = New MultiPatchClass()

        Dim trianglesPointCollection As IPointCollection = New TrianglesClass()

        'Triangle 1

        trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(7.5, 7.5, 0), _missing, _missing)
        trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(10, 7.5, 0), _missing, _missing)
        trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(7.5, 5, 0), _missing, _missing)

        'Triangle 2

        trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-7.5, 7.5, 0), _missing, _missing)
        trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-5, 7.5, 0), _missing, _missing)
        trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-7.5, 5, 0), _missing, _missing)

        'Triangle 3

        trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, -5, 0), _missing, _missing)
        trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(2.5, -5, 0), _missing, _missing)
        trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, -7.5, 0), _missing, _missing)

        'Triangle 4

        trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, 7.5, 2.5), _missing, _missing)
        trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(2.5, 7.5, 0), _missing, _missing)
        trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, 7.5, 0), _missing, _missing)

        'Triangle 5

        trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-7.5, -7.5, 2.5), _missing, _missing)
        trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-5, -7.5, 0), _missing, _missing)
        trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-7.5, -7.5, 0), _missing, _missing)

        'Triangle 6

        trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(7.5, -7.5, 2.5), _missing, _missing)
        trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(10, -7.5, 0), _missing, _missing)
        trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(7.5, -7.5, 0), _missing, _missing)

        multiPatchGeometryCollection.AddGeometry(TryCast(trianglesPointCollection, IGeometry), _missing, _missing)

        Return TryCast(multiPatchGeometryCollection, IGeometry)
    End Function

    Public Shared Function GetExample5() As IGeometry
        'Triangles: Eighteen Triangles Lying In Different Planes

        Dim multiPatchGeometryCollection As IGeometryCollection = New MultiPatchClass()

        Dim trianglesPointCollection As IPointCollection = New TrianglesClass()

        'Z > 0

        'Triangle 1

        trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(7.5, 7.5, 5), _missing, _missing)
        trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(10, 7.5, 5), _missing, _missing)
        trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(7.5, 5, 5), _missing, _missing)

        'Triangle 2

        trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-7.5, 7.5, 5), _missing, _missing)
        trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-5, 7.5, 5), _missing, _missing)
        trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-7.5, 5, 5), _missing, _missing)

        'Triangle 3

        trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, -5, 5), _missing, _missing)
        trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(2.5, -5, 5), _missing, _missing)
        trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, -7.5, 5), _missing, _missing)

        'Triangle 4

        trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, 7.5, 7.5), _missing, _missing)
        trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(2.5, 7.5, 5), _missing, _missing)
        trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, 7.5, 5), _missing, _missing)

        'Triangle 5

        trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-7.5, -7.5, 7.5), _missing, _missing)
        trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-5, -7.5, 5), _missing, _missing)
        trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-7.5, -7.5, 5), _missing, _missing)

        'Triangle 6

        trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(7.5, -7.5, 7.5), _missing, _missing)
        trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(10, -7.5, 5), _missing, _missing)
        trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(7.5, -7.5, 5), _missing, _missing)

        'Z = 0

        'Triangle 1

        trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(7.5, 7.5, 0), _missing, _missing)
        trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(10, 7.5, 0), _missing, _missing)
        trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(7.5, 5, 0), _missing, _missing)

        'Triangle 2

        trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-7.5, 7.5, 0), _missing, _missing)
        trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-5, 7.5, 0), _missing, _missing)
        trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-7.5, 5, 0), _missing, _missing)

        'Triangle 3

        trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, -5, 0), _missing, _missing)
        trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(2.5, -5, 0), _missing, _missing)
        trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, -7.5, 0), _missing, _missing)

        'Triangle 4

        trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, 7.5, 2.5), _missing, _missing)
        trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(2.5, 7.5, 0), _missing, _missing)
        trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, 7.5, 0), _missing, _missing)

        'Triangle 5

        trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-7.5, -7.5, 2.5), _missing, _missing)
        trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-5, -7.5, 0), _missing, _missing)
        trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-7.5, -7.5, 0), _missing, _missing)

        'Triangle 6

        trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(7.5, -7.5, 2.5), _missing, _missing)
        trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(10, -7.5, 0), _missing, _missing)
        trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(7.5, -7.5, 0), _missing, _missing)

        'Z < 0

        'Triangle 1

        trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(7.5, 7.5, -5), _missing, _missing)
        trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(10, 7.5, -5), _missing, _missing)
        trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(7.5, 5, -5), _missing, _missing)

        'Triangle 2

        trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-7.5, 7.5, -5), _missing, _missing)
        trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-5, 7.5, -5), _missing, _missing)
        trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-7.5, 5, -5), _missing, _missing)

        'Triangle 3

        trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, -5, -5), _missing, _missing)
        trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(2.5, -5, -5), _missing, _missing)
        trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, -7.5, -5), _missing, _missing)

        'Triangle 4

        trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, 7.5, -2.5), _missing, _missing)
        trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(2.5, 7.5, -5), _missing, _missing)
        trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, 7.5, -5), _missing, _missing)

        'Triangle 5

        trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-7.5, -7.5, -2.5), _missing, _missing)
        trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-5, -7.5, -5), _missing, _missing)
        trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-7.5, -7.5, -5), _missing, _missing)

        'Triangle 6

        trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(7.5, -7.5, -2.5), _missing, _missing)
        trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(10, -7.5, -5), _missing, _missing)
        trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(7.5, -7.5, -5), _missing, _missing)

        multiPatchGeometryCollection.AddGeometry(TryCast(trianglesPointCollection, IGeometry), _missing, _missing)

        Return TryCast(multiPatchGeometryCollection, IGeometry)
    End Function

    Public Shared Function GetExample6() As IGeometry
        'Triangles: Closed Box Constructed From Single Triangles Part Composed Of 12 Triangles

        Dim multiPatchGeometryCollection As IGeometryCollection = New MultiPatchClass()

        Dim trianglesPointCollection As IPointCollection = New TrianglesClass()

        'Bottom

        trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, 0, 0), _missing, _missing)
        trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(5, 5, 0), _missing, _missing)
        trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, 5, 0), _missing, _missing)

        trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, 0, 0), _missing, _missing)
        trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(5, 0, 0), _missing, _missing)
        trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(5, 5, 0), _missing, _missing)

        'Side 1

        trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, 5, 0), _missing, _missing)
        trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, 5, 5), _missing, _missing)
        trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, 0, 0), _missing, _missing)

        trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, 0, 0), _missing, _missing)
        trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, 5, 5), _missing, _missing)
        trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, 0, 5), _missing, _missing)

        'Side 2

        trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(5, 5, 0), _missing, _missing)
        trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, 5, 5), _missing, _missing)
        trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, 5, 0), _missing, _missing)

        trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(5, 5, 0), _missing, _missing)
        trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(5, 5, 5), _missing, _missing)
        trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, 5, 5), _missing, _missing)

        'Side 3

        trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(5, 0, 0), _missing, _missing)
        trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(5, 5, 5), _missing, _missing)
        trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(5, 5, 0), _missing, _missing)

        trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(5, 0, 0), _missing, _missing)
        trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(5, 0, 5), _missing, _missing)
        trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(5, 5, 5), _missing, _missing)

        'Side 4

        trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, 0, 0), _missing, _missing)
        trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, 0, 5), _missing, _missing)
        trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(5, 0, 0), _missing, _missing)

        trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(5, 0, 0), _missing, _missing)
        trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, 0, 5), _missing, _missing)
        trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(5, 0, 5), _missing, _missing)

        'Top

        trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, 0, 5), _missing, _missing)
        trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, 5, 5), _missing, _missing)
        trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(5, 5, 5), _missing, _missing)

        trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, 0, 5), _missing, _missing)
        trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(5, 5, 5), _missing, _missing)
        trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(5, 0, 5), _missing, _missing)

        multiPatchGeometryCollection.AddGeometry(TryCast(trianglesPointCollection, IGeometry), _missing, _missing)

        Return TryCast(multiPatchGeometryCollection, IGeometry)
    End Function
End Class

