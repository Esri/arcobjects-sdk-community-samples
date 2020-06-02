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


Public Class TriangleStripExamples
    Private Shared _missing As Object = Type.Missing

    Private Sub New()
    End Sub
    Public Shared Function GetExample1() As IGeometry
        'TriangleStrip: Square Lying On XY Plane

        Dim multiPatchGeometryCollection As IGeometryCollection = New MultiPatchClass()

        Dim triangleStripPointCollection As IPointCollection = New TriangleStripClass()

        triangleStripPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-6, -6, 0), _missing, _missing)
        triangleStripPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-6, 6, 0), _missing, _missing)
        triangleStripPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(6, -6, 0), _missing, _missing)
        triangleStripPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(6, 6, 0), _missing, _missing)

        multiPatchGeometryCollection.AddGeometry(TryCast(triangleStripPointCollection, IGeometry), _missing, _missing)

        Return TryCast(multiPatchGeometryCollection, IGeometry)
    End Function

    Public Shared Function GetExample2() As IGeometry
        'TriangleStrip: Multi-Paneled Vertical Plane

        Dim multiPatchGeometryCollection As IGeometryCollection = New MultiPatchClass()

        Dim triangleStripPointCollection As IPointCollection = New TriangleStripClass()

        'Panel 1

        triangleStripPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-7.5, 7.5, 0), _missing, _missing)
        triangleStripPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-7.5, 7.5, 7.5), _missing, _missing)

        'Panel 2

        triangleStripPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-2.5, 2.5, 0), _missing, _missing)
        triangleStripPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-2.5, 2.5, 7.5), _missing, _missing)

        'Panel 3

        triangleStripPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(2.5, -2.5, 0), _missing, _missing)
        triangleStripPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(2.5, -2.5, 7.5), _missing, _missing)

        'Panel 4

        triangleStripPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(7.5, -7.5, 0), _missing, _missing)
        triangleStripPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(7.5, -7.5, 7.5), _missing, _missing)

        multiPatchGeometryCollection.AddGeometry(TryCast(triangleStripPointCollection, IGeometry), _missing, _missing)

        Return TryCast(multiPatchGeometryCollection, IGeometry)
    End Function

    Public Shared Function GetExample3() As IGeometry
        'TriangleStrip: Stairs

        Dim multiPatchGeometryCollection As IGeometryCollection = New MultiPatchClass()

        Dim triangleStripPointCollection As IPointCollection = New TriangleStripClass()

        'First Step

        triangleStripPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, 10, 10), _missing, _missing)
        triangleStripPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(10, 10, 10), _missing, _missing)

        triangleStripPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, 7.5, 10), _missing, _missing)
        triangleStripPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(10, 7.5, 10), _missing, _missing)

        'Second Step

        triangleStripPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, 7.5, 7.5), _missing, _missing)
        triangleStripPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(10, 7.5, 7.5), _missing, _missing)


        triangleStripPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, 5, 7.5), _missing, _missing)
        triangleStripPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(10, 5, 7.5), _missing, _missing)

        'Third Step

        triangleStripPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, 5, 5), _missing, _missing)
        triangleStripPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(10, 5, 5), _missing, _missing)


        triangleStripPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, 2.5, 5), _missing, _missing)
        triangleStripPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(10, 2.5, 5), _missing, _missing)

        'Fourth Step

        triangleStripPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, 2.5, 2.5), _missing, _missing)
        triangleStripPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(10, 2.5, 2.5), _missing, _missing)

        triangleStripPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, 0, 2.5), _missing, _missing)
        triangleStripPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(10, 0, 2.5), _missing, _missing)

        'End

        triangleStripPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, 0, 0), _missing, _missing)
        triangleStripPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(10, 0, 0), _missing, _missing)

        multiPatchGeometryCollection.AddGeometry(TryCast(triangleStripPointCollection, IGeometry), _missing, _missing)

        Return TryCast(multiPatchGeometryCollection, IGeometry)
    End Function

    Public Shared Function GetExample4() As IGeometry
        'TriangleStrip: Box Without Top or Bottom

        Dim multiPatchGeometryCollection As IGeometryCollection = New MultiPatchClass()

        Dim triangleStripPointCollection As IPointCollection = New TriangleStripClass()

        'Start

        triangleStripPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, 0, 0), _missing, _missing)
        triangleStripPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, 0, 10), _missing, _missing)

        'First Panel

        triangleStripPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(5, 0, 0), _missing, _missing)
        triangleStripPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(5, 0, 10), _missing, _missing)

        'Second Panel

        triangleStripPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(5, 5, 0), _missing, _missing)
        triangleStripPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(5, 5, 10), _missing, _missing)

        'Third Panel

        triangleStripPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, 5, 0), _missing, _missing)
        triangleStripPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, 5, 10), _missing, _missing)

        'End, To Close Box

        triangleStripPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, 0, 0), _missing, _missing)
        triangleStripPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, 0, 10), _missing, _missing)

        multiPatchGeometryCollection.AddGeometry(TryCast(triangleStripPointCollection, IGeometry), _missing, _missing)

        Return TryCast(multiPatchGeometryCollection, IGeometry)
    End Function

    Public Shared Function GetExample5() As IGeometry
        'TriangleStrip: Star Shaped Box Without Top or Bottom

        Dim multiPatchGeometryCollection As IGeometryCollection = New MultiPatchClass()

        Dim triangleStripPointCollection As IPointCollection = New TriangleStripClass()

        'Start

        triangleStripPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, 2, 0), _missing, _missing)
        triangleStripPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, 2, 5), _missing, _missing)

        'First Panel

        triangleStripPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-1 * Math.Sqrt(10), Math.Sqrt(10), 0), _missing, _missing)
        triangleStripPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-1 * Math.Sqrt(10), Math.Sqrt(10), 5), _missing, _missing)

        'Second Panel

        triangleStripPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-2, 0, 0), _missing, _missing)
        triangleStripPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-2, 0, 5), _missing, _missing)

        'Third Panel

        triangleStripPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-1 * Math.Sqrt(10), -1 * Math.Sqrt(10), 0), _missing, _missing)
        triangleStripPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-1 * Math.Sqrt(10), -1 * Math.Sqrt(10), 5), _missing, _missing)

        'Fourth Panel

        triangleStripPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, -2, 0), _missing, _missing)
        triangleStripPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, -2, 5), _missing, _missing)

        'Fifth Panel

        triangleStripPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(Math.Sqrt(10), -1 * Math.Sqrt(10), 0), _missing, _missing)
        triangleStripPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(Math.Sqrt(10), -1 * Math.Sqrt(10), 5), _missing, _missing)

        'Sixth Panel

        triangleStripPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(2, 0, 0), _missing, _missing)
        triangleStripPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(2, 0, 5), _missing, _missing)

        'Seventh Panel

        triangleStripPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(Math.Sqrt(10), Math.Sqrt(10), 0), _missing, _missing)
        triangleStripPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(Math.Sqrt(10), Math.Sqrt(10), 5), _missing, _missing)

        'End, To Close Box

        triangleStripPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, 2, 0), _missing, _missing)
        triangleStripPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, 2, 5), _missing, _missing)

        multiPatchGeometryCollection.AddGeometry(TryCast(triangleStripPointCollection, IGeometry), _missing, _missing)

        Return TryCast(multiPatchGeometryCollection, IGeometry)
    End Function
End Class
