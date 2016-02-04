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


Public Class TriangleFanExamples
    Private Shared _missing As Object = Type.Missing

    Private Sub New()
    End Sub
    Public Shared Function GetExample1() As IGeometry
        'TriangleFan: Square Lying On XY Plane, Z < 0

        Dim multiPatchGeometryCollection As IGeometryCollection = New MultiPatchClass()

        Dim triangleFanPointCollection As IPointCollection = New TriangleFanClass()

        triangleFanPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, 0, -5), _missing, _missing)
        triangleFanPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-6, -6, -5), _missing, _missing)
        triangleFanPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-6, 6, -5), _missing, _missing)
        triangleFanPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(6, 6, -5), _missing, _missing)
        triangleFanPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(6, -6, -5), _missing, _missing)
        triangleFanPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-6, -6, -5), _missing, _missing)

        multiPatchGeometryCollection.AddGeometry(TryCast(triangleFanPointCollection, IGeometry), _missing, _missing)

        Return TryCast(multiPatchGeometryCollection, IGeometry)
    End Function

    Public Shared Function GetExample2() As IGeometry
        'TriangleFan: Upright Square

        Dim multiPatchGeometryCollection As IGeometryCollection = New MultiPatchClass()

        Dim triangleFanPointCollection As IPointCollection = New TriangleFanClass()

        triangleFanPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, 0, 0), _missing, _missing)
        triangleFanPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-5, 0, -5), _missing, _missing)
        triangleFanPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-5, 0, 5), _missing, _missing)
        triangleFanPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(5, 0, 5), _missing, _missing)
        triangleFanPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(5, 0, -5), _missing, _missing)
        triangleFanPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-5, 0, -5), _missing, _missing)

        multiPatchGeometryCollection.AddGeometry(TryCast(triangleFanPointCollection, IGeometry), _missing, _missing)

        Return TryCast(multiPatchGeometryCollection, IGeometry)
    End Function

    Public Shared Function GetExample3() As IGeometry
        'TriangleFan: Square Based Pyramid

        Dim multiPatchGeometryCollection As IGeometryCollection = New MultiPatchClass()

        Dim triangleFanPointCollection As IPointCollection = New TriangleFanClass()

        triangleFanPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, 0, 7), _missing, _missing)
        triangleFanPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-6, -6, 0), _missing, _missing)
        triangleFanPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-6, 6, 0), _missing, _missing)
        triangleFanPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(6, 6, 0), _missing, _missing)
        triangleFanPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(6, -6, 0), _missing, _missing)
        triangleFanPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-6, -6, 0), _missing, _missing)

        multiPatchGeometryCollection.AddGeometry(TryCast(triangleFanPointCollection, IGeometry), _missing, _missing)

        Return TryCast(multiPatchGeometryCollection, IGeometry)
    End Function

    Public Shared Function GetExample4() As IGeometry
        'TriangleFan: Triangle Based Pyramid

        Dim multiPatchGeometryCollection As IGeometryCollection = New MultiPatchClass()

        Dim triangleFanPointCollection As IPointCollection = New TriangleFanClass()

        triangleFanPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, 0, 6), _missing, _missing)
        triangleFanPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-3 * Math.Sqrt(3), -3, 0), _missing, _missing)
        triangleFanPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, 6, 0), _missing, _missing)
        triangleFanPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(3 * Math.Sqrt(3), -3, 0), _missing, _missing)
        triangleFanPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-3 * Math.Sqrt(3), -3, 0), _missing, _missing)

        multiPatchGeometryCollection.AddGeometry(TryCast(triangleFanPointCollection, IGeometry), _missing, _missing)

        Return TryCast(multiPatchGeometryCollection, IGeometry)
    End Function

    Public Shared Function GetExample5() As IGeometry
        'TriangleFan: Alternating Fan

        Dim multiPatchGeometryCollection As IGeometryCollection = New MultiPatchClass()

        Dim triangleFanPointCollection As IPointCollection = New TriangleFanClass()

        triangleFanPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, 0, 0), _missing, _missing)
        triangleFanPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, -6, 3), _missing, _missing)
        triangleFanPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-3 * Math.Sqrt(2), -3 * Math.Sqrt(2), -3), _missing, _missing)
        triangleFanPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-6, 0, 3), _missing, _missing)
        triangleFanPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-3 * Math.Sqrt(2), 3 * Math.Sqrt(2), -3), _missing, _missing)
        triangleFanPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, 6, 3), _missing, _missing)
        triangleFanPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(3 * Math.Sqrt(2), 3 * Math.Sqrt(2), -3), _missing, _missing)
        triangleFanPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(6, 0, 3), _missing, _missing)
        triangleFanPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(3 * Math.Sqrt(2), -3 * Math.Sqrt(2), -3), _missing, _missing)
        triangleFanPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, -6, 3), _missing, _missing)

        multiPatchGeometryCollection.AddGeometry(TryCast(triangleFanPointCollection, IGeometry), _missing, _missing)

        Return TryCast(multiPatchGeometryCollection, IGeometry)
    End Function

    Public Shared Function GetExample6() As IGeometry
        'TriangleFan: Partial Fan, Two Levels Of Zs

        Dim multiPatchGeometryCollection As IGeometryCollection = New MultiPatchClass()

        Dim triangleFanPointCollection As IPointCollection = New TriangleFanClass()

        triangleFanPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, 0, 3), _missing, _missing)
        triangleFanPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, -6, 3), _missing, _missing)
        triangleFanPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-3 * Math.Sqrt(2), -3 * Math.Sqrt(2), 3), _missing, _missing)
        triangleFanPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-6, 0, 3), _missing, _missing)
        triangleFanPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-3 * Math.Sqrt(2), 3 * Math.Sqrt(2), 0), _missing, _missing)
        triangleFanPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, 6, 0), _missing, _missing)
        triangleFanPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(3 * Math.Sqrt(2), 3 * Math.Sqrt(2), 0), _missing, _missing)
        triangleFanPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(6, 0, 0), _missing, _missing)

        multiPatchGeometryCollection.AddGeometry(TryCast(triangleFanPointCollection, IGeometry), _missing, _missing)

        Return TryCast(multiPatchGeometryCollection, IGeometry)
    End Function
End Class



