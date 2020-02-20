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
Imports ESRI.ArcGIS.Geodatabase
Imports System.Diagnostics
Imports ESRI.ArcGIS.esriSystem


Public Class ExtrusionExamples
    Private Shared _missing As Object = Type.Missing

    Private Sub New()
    End Sub
    Public Shared Function GetExample1() As IGeometry
        Const FromZ As Double = 0
        Const ToZ As Double = 9

        'Extrusion: Two Point 2D Polyline Extruded To Generate 3D Wall Via ConstructExtrudeFromTo()

        Dim polylinePointCollection As IPointCollection = New PolylineClass()

        polylinePointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(-5, 5), _missing, _missing)
        polylinePointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(5, -5), _missing, _missing)

        Dim polylineGeometry As IGeometry = TryCast(polylinePointCollection, IGeometry)

        Dim topologicalOperator As ITopologicalOperator = TryCast(polylineGeometry, ITopologicalOperator)
        topologicalOperator.Simplify()

        Dim constructMultiPatch As IConstructMultiPatch = New MultiPatchClass()
        constructMultiPatch.ConstructExtrudeFromTo(FromZ, ToZ, polylineGeometry)

        Return TryCast(constructMultiPatch, IGeometry)
    End Function

    Public Shared Function GetExample2() As IGeometry
        Const FromZ As Double = -0.1
        Const ToZ As Double = -8

        'Extrusion: Multiple Point 2D Polyline Extruded To Generate 3D Wall Via ConstructExtrudeFromTo()

        Dim polylinePointCollection As IPointCollection = New PolylineClass()

        polylinePointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(-10, -10), _missing, _missing)
        polylinePointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(-8, -7), _missing, _missing)
        polylinePointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(-5, -5), _missing, _missing)
        polylinePointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(-3, -2), _missing, _missing)
        polylinePointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(0, 0), _missing, _missing)
        polylinePointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(3, 2), _missing, _missing)
        polylinePointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(5, 5), _missing, _missing)
        polylinePointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(8, 7), _missing, _missing)
        polylinePointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(10, 10), _missing, _missing)

        Dim polylineGeometry As IGeometry = TryCast(polylinePointCollection, IGeometry)

        Dim topologicalOperator As ITopologicalOperator = TryCast(polylineGeometry, ITopologicalOperator)
        topologicalOperator.Simplify()

        Dim constructMultiPatch As IConstructMultiPatch = New MultiPatchClass()
        constructMultiPatch.ConstructExtrudeFromTo(FromZ, ToZ, polylineGeometry)

        Return TryCast(constructMultiPatch, IGeometry)
    End Function

    Public Shared Function GetExample3() As IGeometry
        Const FromZ As Double = 0
        Const ToZ As Double = 9.5

        'Extrusion: Square Shaped 2D Polygon Extruded To Generate 3D Building Via ConstructExtrudeFromTo()

        Dim polygonPointCollection As IPointCollection = New PolygonClass()

        polygonPointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(-2, 2), _missing, _missing)
        polygonPointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(2, 2), _missing, _missing)
        polygonPointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(2, -2), _missing, _missing)
        polygonPointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(-2, -2), _missing, _missing)

        Dim polygon As IPolygon = TryCast(polygonPointCollection, IPolygon)
        polygon.Close()

        Dim polygonGeometry As IGeometry = TryCast(polygonPointCollection, IGeometry)

        Dim topologicalOperator As ITopologicalOperator = TryCast(polygonGeometry, ITopologicalOperator)
        topologicalOperator.Simplify()

        Dim constructMultiPatch As IConstructMultiPatch = New MultiPatchClass()
        constructMultiPatch.ConstructExtrudeFromTo(FromZ, ToZ, polygonGeometry)

        Return TryCast(constructMultiPatch, IGeometry)
    End Function

    Public Shared Function GetExample4() As IGeometry
        Const FromZ As Double = 0
        Const ToZ As Double = 8.5

        'Extrusion: 2D Polygon Composed Of Multiple Square Shaped Rings, Extruded To Generate Multiple 
        '           3D Buildings Via ConstructExtrudeFromTo()

        Dim polygon As IPolygon = New PolygonClass()

        Dim geometryCollection As IGeometryCollection = TryCast(polygon, IGeometryCollection)

        'Ring 1

        Dim ring1PointCollection As IPointCollection = New RingClass()
        ring1PointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(1, 1), _missing, _missing)
        ring1PointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(1, 4), _missing, _missing)
        ring1PointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(4, 4), _missing, _missing)
        ring1PointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(4, 1), _missing, _missing)

        Dim ring1 As IRing = TryCast(ring1PointCollection, IRing)
        ring1.Close()

        geometryCollection.AddGeometry(TryCast(ring1, IGeometry), _missing, _missing)

        'Ring 2

        Dim ring2PointCollection As IPointCollection = New RingClass()
        ring2PointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(1, -1), _missing, _missing)
        ring2PointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(4, -1), _missing, _missing)
        ring2PointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(4, -4), _missing, _missing)
        ring2PointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(1, -4), _missing, _missing)

        Dim ring2 As IRing = TryCast(ring2PointCollection, IRing)
        ring2.Close()

        geometryCollection.AddGeometry(TryCast(ring2, IGeometry), _missing, _missing)

        'Ring 3

        Dim ring3PointCollection As IPointCollection = New RingClass()
        ring3PointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(-1, 1), _missing, _missing)
        ring3PointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(-4, 1), _missing, _missing)
        ring3PointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(-4, 4), _missing, _missing)
        ring3PointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(-1, 4), _missing, _missing)

        Dim ring3 As IRing = TryCast(ring3PointCollection, IRing)
        ring3.Close()

        geometryCollection.AddGeometry(TryCast(ring3, IGeometry), _missing, _missing)

        'Ring 4

        Dim ring4PointCollection As IPointCollection = New RingClass()
        ring4PointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(-1, -1), _missing, _missing)
        ring4PointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(-1, -4), _missing, _missing)
        ring4PointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(-4, -4), _missing, _missing)
        ring4PointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(-4, -1), _missing, _missing)

        Dim ring4 As IRing = TryCast(ring4PointCollection, IRing)
        ring4.Close()

        geometryCollection.AddGeometry(TryCast(ring4, IGeometry), _missing, _missing)

        Dim polygonGeometry As IGeometry = TryCast(polygon, IGeometry)

        Dim topologicalOperator As ITopologicalOperator = TryCast(polygonGeometry, ITopologicalOperator)
        topologicalOperator.Simplify()

        Dim constructMultiPatch As IConstructMultiPatch = New MultiPatchClass()
        constructMultiPatch.ConstructExtrudeFromTo(FromZ, ToZ, polygonGeometry)

        Return TryCast(constructMultiPatch, IGeometry)
    End Function

    Public Shared Function GetExample5() As IGeometry
        Const FromZ As Double = 0
        Const ToZ As Double = 8.5

        'Extrusion: 2D Polygon Composed Of Multiple Square Shaped Exterior Rings And Corresponding Interior Rings,
        '           Extruded To Generate Multiple 3D Buildings With Hollow Interiors Via ConstructExtrudeFromTo()

        Dim polygon As IPolygon = New PolygonClass()

        Dim geometryCollection As IGeometryCollection = TryCast(polygon, IGeometryCollection)

        'Exterior Ring 1

        Dim exteriorRing1PointCollection As IPointCollection = New RingClass()
        exteriorRing1PointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(1, 1), _missing, _missing)
        exteriorRing1PointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(1, 4), _missing, _missing)
        exteriorRing1PointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(4, 4), _missing, _missing)
        exteriorRing1PointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(4, 1), _missing, _missing)

        Dim exteriorRing1 As IRing = TryCast(exteriorRing1PointCollection, IRing)
        exteriorRing1.Close()

        geometryCollection.AddGeometry(TryCast(exteriorRing1, IGeometry), _missing, _missing)

        'Interior Ring 1

        Dim interiorRing1PointCollection As IPointCollection = New RingClass()
        interiorRing1PointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(1.5, 1.5), _missing, _missing)
        interiorRing1PointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(1.5, 3.5), _missing, _missing)
        interiorRing1PointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(3.5, 3.5), _missing, _missing)
        interiorRing1PointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(3.5, 1.5), _missing, _missing)

        Dim interiorRing1 As IRing = TryCast(interiorRing1PointCollection, IRing)
        interiorRing1.Close()

        geometryCollection.AddGeometry(TryCast(interiorRing1, IGeometry), _missing, _missing)

        'Exterior Ring 2

        Dim exteriorRing2PointCollection As IPointCollection = New RingClass()
        exteriorRing2PointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(1, -1), _missing, _missing)
        exteriorRing2PointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(4, -1), _missing, _missing)
        exteriorRing2PointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(4, -4), _missing, _missing)
        exteriorRing2PointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(1, -4), _missing, _missing)

        Dim exteriorRing2 As IRing = TryCast(exteriorRing2PointCollection, IRing)
        exteriorRing2.Close()

        geometryCollection.AddGeometry(TryCast(exteriorRing2, IGeometry), _missing, _missing)

        'Interior Ring 2

        Dim interiorRing2PointCollection As IPointCollection = New RingClass()
        interiorRing2PointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(1.5, -1.5), _missing, _missing)
        interiorRing2PointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(3.5, -1.5), _missing, _missing)
        interiorRing2PointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(3.5, -3.5), _missing, _missing)
        interiorRing2PointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(1.5, -3.5), _missing, _missing)

        Dim interiorRing2 As IRing = TryCast(interiorRing2PointCollection, IRing)
        interiorRing2.Close()

        geometryCollection.AddGeometry(TryCast(interiorRing2, IGeometry), _missing, _missing)

        'Exterior Ring 3

        Dim exteriorRing3PointCollection As IPointCollection = New RingClass()
        exteriorRing3PointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(-1, 1), _missing, _missing)
        exteriorRing3PointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(-4, 1), _missing, _missing)
        exteriorRing3PointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(-4, 4), _missing, _missing)
        exteriorRing3PointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(-1, 4), _missing, _missing)

        Dim exteriorRing3 As IRing = TryCast(exteriorRing3PointCollection, IRing)
        exteriorRing3.Close()

        geometryCollection.AddGeometry(TryCast(exteriorRing3, IGeometry), _missing, _missing)

        'Interior Ring 3

        Dim interiorRing3PointCollection As IPointCollection = New RingClass()
        interiorRing3PointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(-1.5, 1.5), _missing, _missing)
        interiorRing3PointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(-3.5, 1.5), _missing, _missing)
        interiorRing3PointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(-3.5, 3.5), _missing, _missing)
        interiorRing3PointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(-1.5, 3.5), _missing, _missing)

        Dim interiorRing3 As IRing = TryCast(interiorRing3PointCollection, IRing)
        interiorRing3.Close()

        geometryCollection.AddGeometry(TryCast(interiorRing3, IGeometry), _missing, _missing)

        'Exterior Ring 4

        Dim exteriorRing4PointCollection As IPointCollection = New RingClass()
        exteriorRing4PointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(-1, -1), _missing, _missing)
        exteriorRing4PointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(-1, -4), _missing, _missing)
        exteriorRing4PointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(-4, -4), _missing, _missing)
        exteriorRing4PointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(-4, -1), _missing, _missing)

        Dim exteriorRing4 As IRing = TryCast(exteriorRing4PointCollection, IRing)
        exteriorRing4.Close()

        geometryCollection.AddGeometry(TryCast(exteriorRing4, IGeometry), _missing, _missing)

        'Interior Ring 5

        Dim interiorRing4PointCollection As IPointCollection = New RingClass()
        interiorRing4PointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(-1.5, -1.5), _missing, _missing)
        interiorRing4PointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(-1.5, -3.5), _missing, _missing)
        interiorRing4PointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(-3.5, -3.5), _missing, _missing)
        interiorRing4PointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(-3.5, -1.5), _missing, _missing)

        Dim interiorRing4 As IRing = TryCast(interiorRing4PointCollection, IRing)
        interiorRing4.Close()

        geometryCollection.AddGeometry(TryCast(interiorRing4, IGeometry), _missing, _missing)

        Dim polygonGeometry As IGeometry = TryCast(polygon, IGeometry)

        Dim topologicalOperator As ITopologicalOperator = TryCast(polygonGeometry, ITopologicalOperator)
        topologicalOperator.Simplify()

        Dim constructMultiPatch As IConstructMultiPatch = New MultiPatchClass()
        constructMultiPatch.ConstructExtrudeFromTo(FromZ, ToZ, polygonGeometry)

        Return TryCast(constructMultiPatch, IGeometry)
    End Function

    Public Shared Function GetExample6() As IGeometry
        Const CircleDegrees As Double = 360.0
        Const CircleDivisions As Integer = 36
        Const VectorComponentOffset As Double = 0.0000001
        Const CircleRadius As Double = 3.0
        Const BaseZ As Double = -10
        Const ToZ As Double = -3

        'Extrusion: 3D Circle Polygon Having Vertices With Varying Z Values, Extruded To Specified Z Value
        '           Via ConstrucExtrudeAbsolute()

        Dim multiPatchGeometryCollection As IGeometryCollection = New MultiPatchClass()

        Dim polygonPointCollection As IPointCollection = New PolygonClass()

        Dim originPoint As IPoint = GeometryUtilities.ConstructPoint3D(0, 0, 0)

        Dim upperAxisVector3D As IVector3D = GeometryUtilities.ConstructVector3D(0, 0, 10)

        Dim lowerAxisVector3D As IVector3D = GeometryUtilities.ConstructVector3D(0, 0, -10)

        lowerAxisVector3D.XComponent += VectorComponentOffset

        Dim normalVector3D As IVector3D = TryCast(upperAxisVector3D.CrossProduct(lowerAxisVector3D), IVector3D)

        normalVector3D.Magnitude = CircleRadius

        Dim rotationAngleInRadians As Double = GeometryUtilities.GetRadians(CircleDegrees / CircleDivisions)

        Dim random As Random = New Random()

        For i As Integer = 0 To CircleDivisions - 1
            normalVector3D.Rotate(-1 * rotationAngleInRadians, upperAxisVector3D)

            Dim vertexPoint As IPoint = GeometryUtilities.ConstructPoint3D(originPoint.X + normalVector3D.XComponent, originPoint.Y + normalVector3D.YComponent, BaseZ + 2 * Math.Sin(random.NextDouble()))

            polygonPointCollection.AddPoint(vertexPoint, _missing, _missing)
        Next i

        Dim polygon As IPolygon = TryCast(polygonPointCollection, IPolygon)
        polygon.Close()

        Dim polygonGeometry As IGeometry = TryCast(polygon, IGeometry)

        GeometryUtilities.MakeZAware(polygonGeometry)

        Dim topologicalOperator As ITopologicalOperator = TryCast(polygon, ITopologicalOperator)
        topologicalOperator.Simplify()

        Dim constructMultiPatch As IConstructMultiPatch = New MultiPatchClass()
        constructMultiPatch.ConstructExtrudeAbsolute(ToZ, polygonGeometry)

        Return TryCast(constructMultiPatch, IGeometry)
    End Function

    Public Shared Function GetExample7() As IGeometry
        Const DensificationDivisions As Integer = 20
        Const MaxDeviation As Double = 0.1
        Const BaseZ As Double = 0
        Const ToZ As Double = -10

        'Extrusion: 3D Polyline Having Vertices With Varying Z Values, Extruded To Specified Z Value
        '           Via ConstructExtrudeAbsolute()

        Dim polylinePointCollection As IPointCollection = New PolylineClass()

        polylinePointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(-10, -10), _missing, _missing)
        polylinePointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(0, -5), _missing, _missing)
        polylinePointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(0, 5), _missing, _missing)
        polylinePointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(10, 10), _missing, _missing)

        Dim polyline As IPolyline = TryCast(polylinePointCollection, IPolyline)
        polyline.Densify(polyline.Length / DensificationDivisions, MaxDeviation)

        Dim polylineGeometry As IGeometry = TryCast(polyline, IGeometry)

        GeometryUtilities.MakeZAware(polylineGeometry)

        Dim random As Random = New Random()

        Dim i As Integer = 0
        Do While i < polylinePointCollection.PointCount
            Dim polylinePoint As IPoint = polylinePointCollection.Point(i)

            polylinePointCollection.UpdatePoint(i, GeometryUtilities.ConstructPoint3D(polylinePoint.X, polylinePoint.Y, BaseZ - 2 * Math.Sin(random.NextDouble())))
            i += 1
        Loop

        Dim topologicalOperator As ITopologicalOperator = TryCast(polylineGeometry, ITopologicalOperator)
        topologicalOperator.Simplify()

        Dim constructMultiPatch As IConstructMultiPatch = New MultiPatchClass()
        constructMultiPatch.ConstructExtrudeAbsolute(ToZ, polylineGeometry)

        Return TryCast(constructMultiPatch, IGeometry)
    End Function

    Public Shared Function GetExample8() As IGeometry
        Const CircleDegrees As Double = 360.0
        Const CircleDivisions As Integer = 36
        Const VectorComponentOffset As Double = 0.0000001
        Const CircleRadius As Double = 3.0
        Const BaseZ As Double = -10
        Const OffsetZ As Double = 5

        'Extrusion: 3D Circle Polygon Having Vertices With Varying Z Values, Extruded Relative To Existing 
        '           Vertex Z Values Via ConstructExtrude()

        Dim multiPatchGeometryCollection As IGeometryCollection = New MultiPatchClass()

        Dim polygonPointCollection As IPointCollection = New PolygonClass()

        Dim originPoint As IPoint = GeometryUtilities.ConstructPoint3D(0, 0, 0)

        Dim upperAxisVector3D As IVector3D = GeometryUtilities.ConstructVector3D(0, 0, 10)

        Dim lowerAxisVector3D As IVector3D = GeometryUtilities.ConstructVector3D(0, 0, -10)

        lowerAxisVector3D.XComponent += VectorComponentOffset

        Dim normalVector3D As IVector3D = TryCast(upperAxisVector3D.CrossProduct(lowerAxisVector3D), IVector3D)

        normalVector3D.Magnitude = CircleRadius

        Dim rotationAngleInRadians As Double = GeometryUtilities.GetRadians(CircleDegrees / CircleDivisions)

        Dim random As Random = New Random()

        For i As Integer = 0 To CircleDivisions - 1
            normalVector3D.Rotate(-1 * rotationAngleInRadians, upperAxisVector3D)

            Dim vertexPoint As IPoint = GeometryUtilities.ConstructPoint3D(originPoint.X + normalVector3D.XComponent, originPoint.Y + normalVector3D.YComponent, BaseZ + 2 * Math.Sin(random.NextDouble()))

            polygonPointCollection.AddPoint(vertexPoint, _missing, _missing)
        Next i

        Dim polygon As IPolygon = TryCast(polygonPointCollection, IPolygon)
        polygon.Close()

        Dim polygonGeometry As IGeometry = TryCast(polygon, IGeometry)

        GeometryUtilities.MakeZAware(polygonGeometry)

        Dim topologicalOperator As ITopologicalOperator = TryCast(polygon, ITopologicalOperator)
        topologicalOperator.Simplify()

        Dim constructMultiPatch As IConstructMultiPatch = New MultiPatchClass()
        constructMultiPatch.ConstructExtrude(OffsetZ, polygonGeometry)

        Return TryCast(constructMultiPatch, IGeometry)
    End Function

    Public Shared Function GetExample9() As IGeometry
        Const DensificationDivisions As Integer = 20
        Const MaxDeviation As Double = 0.1
        Const BaseZ As Double = 0
        Const OffsetZ As Double = -7

        'Extrusion: 3D Polyline Having Vertices With Varying Z Values, Extruded Relative To Existing
        '           Vertex Z Values Via ConstructExtrude()

        Dim polylinePointCollection As IPointCollection = New PolylineClass()

        polylinePointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(-10, -10), _missing, _missing)
        polylinePointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(0, -5), _missing, _missing)
        polylinePointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(0, 5), _missing, _missing)
        polylinePointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(10, 10), _missing, _missing)

        Dim polyline As IPolyline = TryCast(polylinePointCollection, IPolyline)
        polyline.Densify(polyline.Length / DensificationDivisions, MaxDeviation)

        Dim polylineGeometry As IGeometry = TryCast(polyline, IGeometry)

        GeometryUtilities.MakeZAware(polylineGeometry)

        Dim random As Random = New Random()

        Dim i As Integer = 0
        Do While i < polylinePointCollection.PointCount
            Dim polylinePoint As IPoint = polylinePointCollection.Point(i)

            polylinePointCollection.UpdatePoint(i, GeometryUtilities.ConstructPoint3D(polylinePoint.X, polylinePoint.Y, BaseZ - 2 * Math.Sin(random.NextDouble())))
            i += 1
        Loop

        Dim topologicalOperator As ITopologicalOperator = TryCast(polylineGeometry, ITopologicalOperator)
        topologicalOperator.Simplify()

        Dim constructMultiPatch As IConstructMultiPatch = New MultiPatchClass()
        constructMultiPatch.ConstructExtrude(OffsetZ, polylineGeometry)

        Return TryCast(constructMultiPatch, IGeometry)
    End Function

    Public Shared Function GetExample10() As IGeometry
        Const CircleDegrees As Double = 360.0
        Const CircleDivisions As Integer = 36
        Const VectorComponentOffset As Double = 0.0000001
        Const CircleRadius As Double = 3.0
        Const BaseZ As Double = 0.0

        'Extrusion: 3D Circle Polygon Extruded Along 3D Line Via ConstructExtrudeAlongLine()

        Dim polygonPointCollection As IPointCollection = New PolygonClass()

        Dim polygonGeometry As IGeometry = TryCast(polygonPointCollection, IGeometry)

        GeometryUtilities.MakeZAware(polygonGeometry)

        Dim originPoint As IPoint = GeometryUtilities.ConstructPoint3D(0, 0, 0)

        Dim upperAxisVector3D As IVector3D = GeometryUtilities.ConstructVector3D(0, 0, 10)

        Dim lowerAxisVector3D As IVector3D = GeometryUtilities.ConstructVector3D(0, 0, -10)

        lowerAxisVector3D.XComponent += VectorComponentOffset

        Dim normalVector3D As IVector3D = TryCast(upperAxisVector3D.CrossProduct(lowerAxisVector3D), IVector3D)

        normalVector3D.Magnitude = CircleRadius

        Dim rotationAngleInRadians As Double = GeometryUtilities.GetRadians(CircleDegrees / CircleDivisions)

        For i As Integer = 0 To CircleDivisions - 1
            normalVector3D.Rotate(-1 * rotationAngleInRadians, upperAxisVector3D)

            Dim vertexPoint As IPoint = GeometryUtilities.ConstructPoint3D(originPoint.X + normalVector3D.XComponent, originPoint.Y + normalVector3D.YComponent, BaseZ)

            polygonPointCollection.AddPoint(vertexPoint, _missing, _missing)
        Next i

        polygonPointCollection.AddPoint(polygonPointCollection.Point(0), _missing, _missing)

        Dim topologicalOperator As ITopologicalOperator = TryCast(polygonGeometry, ITopologicalOperator)
        topologicalOperator.Simplify()

        'Define Line To Extrude Along

        Dim extrusionLine As ILine = New LineClass()
        extrusionLine.FromPoint = GeometryUtilities.ConstructPoint3D(-4, -4, -5)
        extrusionLine.ToPoint = GeometryUtilities.ConstructPoint3D(4, 4, 5)

        'Perform Extrusion

        Dim constructMultiPatch As IConstructMultiPatch = New MultiPatchClass()
        constructMultiPatch.ConstructExtrudeAlongLine(extrusionLine, polygonGeometry)

        'Transform Extrusion Result

        Dim area As IArea = TryCast(polygonGeometry, IArea)

        Dim transform2D As ITransform2D = TryCast(constructMultiPatch, ITransform2D)
        transform2D.Move(extrusionLine.FromPoint.X - area.Centroid.X, extrusionLine.FromPoint.Y - area.Centroid.Y)

        Return TryCast(constructMultiPatch, IGeometry)
    End Function

    Public Shared Function GetExample11() As IGeometry
        Const CircleDegrees As Double = 360.0
        Const CircleDivisions As Integer = 36
        Const VectorComponentOffset As Double = 0.0000001
        Const CircleRadius As Double = 3.0
        Const BaseZ As Double = 0.0

        'Extrusion: 3D Circle Polyline Extruded Along 3D Line Via ConstructExtrudeAlongLine()

        Dim polylinePointCollection As IPointCollection = New PolylineClass()

        Dim polylineGeometry As IGeometry = TryCast(polylinePointCollection, IGeometry)

        GeometryUtilities.MakeZAware(polylineGeometry)

        Dim originPoint As IPoint = GeometryUtilities.ConstructPoint3D(0, 0, 0)

        Dim upperAxisVector3D As IVector3D = GeometryUtilities.ConstructVector3D(0, 0, 10)

        Dim lowerAxisVector3D As IVector3D = GeometryUtilities.ConstructVector3D(0, 0, -10)

        lowerAxisVector3D.XComponent += VectorComponentOffset

        Dim normalVector3D As IVector3D = TryCast(upperAxisVector3D.CrossProduct(lowerAxisVector3D), IVector3D)

        normalVector3D.Magnitude = CircleRadius

        Dim rotationAngleInRadians As Double = GeometryUtilities.GetRadians(CircleDegrees / CircleDivisions)

        For i As Integer = 0 To CircleDivisions - 1
            normalVector3D.Rotate(-1 * rotationAngleInRadians, upperAxisVector3D)

            Dim vertexPoint As IPoint = GeometryUtilities.ConstructPoint3D(originPoint.X + normalVector3D.XComponent, originPoint.Y + normalVector3D.YComponent, BaseZ)

            polylinePointCollection.AddPoint(vertexPoint, _missing, _missing)
        Next i

        polylinePointCollection.AddPoint(polylinePointCollection.Point(0), _missing, _missing)

        Dim topologicalOperator As ITopologicalOperator = TryCast(polylineGeometry, ITopologicalOperator)
        topologicalOperator.Simplify()

        'Define Line To Extrude Along

        Dim extrusionLine As ILine = New LineClass()
        extrusionLine.FromPoint = GeometryUtilities.ConstructPoint3D(-4, -4, -5)
        extrusionLine.ToPoint = GeometryUtilities.ConstructPoint3D(4, 4, 5)

        'Perform Extrusion

        Dim constructMultiPatch As IConstructMultiPatch = New MultiPatchClass()
        constructMultiPatch.ConstructExtrudeAlongLine(extrusionLine, polylineGeometry)

        'Transform Extrusion Result

        Dim centroid As IPoint = GeometryUtilities.ConstructPoint2D(0.5 * (polylineGeometry.Envelope.XMax + polylineGeometry.Envelope.XMin), 0.5 * (polylineGeometry.Envelope.YMax + polylineGeometry.Envelope.YMin))

        Dim transform2D As ITransform2D = TryCast(constructMultiPatch, ITransform2D)
        transform2D.Move(extrusionLine.FromPoint.X - centroid.X, extrusionLine.FromPoint.Y - centroid.Y)

        Return TryCast(constructMultiPatch, IGeometry)
    End Function

    Public Shared Function GetExample12() As IGeometry
        Const CircleDegrees As Double = 360.0
        Const CircleDivisions As Integer = 36
        Const VectorComponentOffset As Double = 0.0000001
        Const CircleRadius As Double = 3.0
        Const BaseZ As Double = 0.0
        Const RotationAngleInDegrees As Double = 89.9

        'Extrusion: 3D Circle Polygon Extruded Along 3D Vector Via ConstructExtrudeRelative()

        Dim pathPointCollection As IPointCollection = New PathClass()

        Dim pathGeometry As IGeometry = TryCast(pathPointCollection, IGeometry)

        GeometryUtilities.MakeZAware(pathGeometry)

        Dim originPoint As IPoint = GeometryUtilities.ConstructPoint3D(0, 0, 0)

        Dim upperAxisVector3D As IVector3D = GeometryUtilities.ConstructVector3D(0, 0, 10)

        Dim lowerAxisVector3D As IVector3D = GeometryUtilities.ConstructVector3D(0, 0, -10)

        lowerAxisVector3D.XComponent += VectorComponentOffset

        Dim normalVector3D As IVector3D = TryCast(upperAxisVector3D.CrossProduct(lowerAxisVector3D), IVector3D)

        normalVector3D.Magnitude = CircleRadius

        Dim rotationAngleInRadians As Double = GeometryUtilities.GetRadians(CircleDegrees / CircleDivisions)

        Dim i As Integer
        For i = 0 To CircleDivisions - 1
            normalVector3D.Rotate(-1 * rotationAngleInRadians, upperAxisVector3D)

            Dim vertexPoint As IPoint = GeometryUtilities.ConstructPoint3D(originPoint.X + normalVector3D.XComponent, originPoint.Y + normalVector3D.YComponent, BaseZ)

            pathPointCollection.AddPoint(vertexPoint, _missing, _missing)
        Next i

        pathPointCollection.AddPoint(pathPointCollection.Point(0), _missing, _missing)

        'Rotate Geometry

        Dim rotationAxisVector3D As IVector3D = GeometryUtilities.ConstructVector3D(0, 10, 0)

        Dim transform3D As ITransform3D = TryCast(pathGeometry, ITransform3D)
        transform3D.RotateVector3D(rotationAxisVector3D, GeometryUtilities.GetRadians(RotationAngleInDegrees))

        'Construct Polygon From Path Vertices

        Dim polygonGeometry As IGeometry = New PolygonClass()

        GeometryUtilities.MakeZAware(polygonGeometry)

        Dim polygonPointCollection As IPointCollection = TryCast(polygonGeometry, IPointCollection)

        i = 0
        Do While i < pathPointCollection.PointCount
            polygonPointCollection.AddPoint(pathPointCollection.Point(i), _missing, _missing)
            i += 1
        Loop

        Dim topologicalOperator As ITopologicalOperator = TryCast(polygonGeometry, ITopologicalOperator)
        topologicalOperator.Simplify()

        'Define Vector To Extrude Along

        Dim extrusionVector3D As IVector3D = GeometryUtilities.ConstructVector3D(10, 0, 5)

        'Perform Extrusion

        Dim constructMultiPatch As IConstructMultiPatch = New MultiPatchClass()
        constructMultiPatch.ConstructExtrudeRelative(extrusionVector3D, polygonGeometry)

        Return TryCast(constructMultiPatch, IGeometry)
    End Function

    Public Shared Function GetExample13() As IGeometry
        Const CircleDegrees As Double = 360.0
        Const CircleDivisions As Integer = 36
        Const VectorComponentOffset As Double = 0.0000001
        Const CircleRadius As Double = 3.0
        Const BaseZ As Double = 0.0
        Const RotationAngleInDegrees As Double = 89.9

        'Extrusion: 3D Circle Polyline Extruded Along 3D Vector Via ConstructExtrudeRelative()

        Dim pathPointCollection As IPointCollection = New PathClass()

        Dim pathGeometry As IGeometry = TryCast(pathPointCollection, IGeometry)

        GeometryUtilities.MakeZAware(pathGeometry)

        Dim originPoint As IPoint = GeometryUtilities.ConstructPoint3D(0, 0, 0)

        Dim upperAxisVector3D As IVector3D = GeometryUtilities.ConstructVector3D(0, 0, 10)

        Dim lowerAxisVector3D As IVector3D = GeometryUtilities.ConstructVector3D(0, 0, -10)

        lowerAxisVector3D.XComponent += VectorComponentOffset

        Dim normalVector3D As IVector3D = TryCast(upperAxisVector3D.CrossProduct(lowerAxisVector3D), IVector3D)

        normalVector3D.Magnitude = CircleRadius

        Dim rotationAngleInRadians As Double = GeometryUtilities.GetRadians(CircleDegrees / CircleDivisions)

        Dim i As Integer
        For i = 0 To CircleDivisions - 1
            normalVector3D.Rotate(-1 * rotationAngleInRadians, upperAxisVector3D)

            Dim vertexPoint As IPoint = GeometryUtilities.ConstructPoint3D(originPoint.X + normalVector3D.XComponent, originPoint.Y + normalVector3D.YComponent, BaseZ)

            pathPointCollection.AddPoint(vertexPoint, _missing, _missing)
        Next i

        pathPointCollection.AddPoint(pathPointCollection.Point(0), _missing, _missing)

        'Rotate Geometry

        Dim rotationAxisVector3D As IVector3D = GeometryUtilities.ConstructVector3D(0, 10, 0)

        Dim transform3D As ITransform3D = TryCast(pathGeometry, ITransform3D)
        transform3D.RotateVector3D(rotationAxisVector3D, GeometryUtilities.GetRadians(RotationAngleInDegrees))

        'Construct Polyline From Path Vertices

        Dim polylineGeometry As IGeometry = New PolylineClass()

        GeometryUtilities.MakeZAware(polylineGeometry)

        Dim polylinePointCollection As IPointCollection = TryCast(polylineGeometry, IPointCollection)

        i = 0
        Do While i < pathPointCollection.PointCount
            polylinePointCollection.AddPoint(pathPointCollection.Point(i), _missing, _missing)
            i += 1
        Loop

        Dim topologicalOperator As ITopologicalOperator = TryCast(polylineGeometry, ITopologicalOperator)
        topologicalOperator.Simplify()

        'Define Vector To Extrude Along

        Dim extrusionVector3D As IVector3D = GeometryUtilities.ConstructVector3D(10, 0, 5)

        'Perform Extrusion

        Dim constructMultiPatch As IConstructMultiPatch = New MultiPatchClass()
        constructMultiPatch.ConstructExtrudeRelative(extrusionVector3D, polylineGeometry)

        Return TryCast(constructMultiPatch, IGeometry)
    End Function

    Public Shared Function GetExample14() As IGeometry
        Const PointCount As Integer = 100
        Const ZMin As Double = 0
        Const ZMax As Double = 4

        'Extrusion: Square Shaped Base Geometry Extruded Between Single TIN-Based Functional Surface

        Dim multiPatchGeometryCollection As IGeometryCollection = New MultiPatchClass()

        'Base Geometry

        Dim envelope As IEnvelope = New EnvelopeClass()
        envelope.XMin = -10
        envelope.XMax = 10
        envelope.YMin = -10
        envelope.YMax = 10

        Dim baseGeometry As IGeometry = TryCast(envelope, IGeometry)

        'Upper Functional Surface

        Dim tinEdit As ITinEdit = New TinClass()
        tinEdit.InitNew(envelope)

        Dim random As Random = New Random()

        For i As Integer = 0 To PointCount - 1
            Dim x As Double = envelope.XMin + (envelope.XMax - envelope.XMin) * random.NextDouble()
            Dim y As Double = envelope.YMin + (envelope.YMax - envelope.YMin) * random.NextDouble()
            Dim z As Double = ZMin + (ZMax - ZMin) * random.NextDouble()

            Dim point As IPoint = GeometryUtilities.ConstructPoint3D(x, y, z)

            tinEdit.AddPointZ(point, 0)
        Next i

        Dim functionalSurface As IFunctionalSurface = TryCast(tinEdit, IFunctionalSurface)

        Dim constructMultiPatch As IConstructMultiPatch = New MultiPatchClass()
        constructMultiPatch.ConstructExtrudeBetween(functionalSurface, functionalSurface, baseGeometry)

        Return TryCast(constructMultiPatch, IGeometry)
    End Function

    Public Shared Function GetExample15() As IGeometry
        Const CircleDegrees As Double = 360.0
        Const CircleDivisions As Integer = 36
        Const VectorComponentOffset As Double = 0.0000001
        Const CircleRadius As Double = 9.5
        Const PointCount As Integer = 100
        Const UpperZMin As Double = 7
        Const UpperZMax As Double = 10
        Const LowerZMin As Double = 0
        Const LowerZMax As Double = 3

        'Extrusion: Circle Shaped Base Geometry Extruded Between Two Different TIN-Based Functional Surfaces

        Dim multiPatchGeometryCollection As IGeometryCollection = New MultiPatchClass()

        'Base Geometry

        Dim polygonPointCollection As IPointCollection = New PolygonClass()

        Dim originPoint As IPoint = GeometryUtilities.ConstructPoint3D(0, 0, 0)

        Dim upperAxisVector3D As IVector3D = GeometryUtilities.ConstructVector3D(0, 0, 10)

        Dim lowerAxisVector3D As IVector3D = GeometryUtilities.ConstructVector3D(0, 0, -10)

        lowerAxisVector3D.XComponent += VectorComponentOffset

        Dim normalVector3D As IVector3D = TryCast(upperAxisVector3D.CrossProduct(lowerAxisVector3D), IVector3D)

        normalVector3D.Magnitude = CircleRadius

        Dim rotationAngleInRadians As Double = GeometryUtilities.GetRadians(CircleDegrees / CircleDivisions)

        For i As Integer = 0 To CircleDivisions - 1
            normalVector3D.Rotate(-1 * rotationAngleInRadians, upperAxisVector3D)

            Dim vertexPoint As IPoint = GeometryUtilities.ConstructPoint2D(originPoint.X + normalVector3D.XComponent, originPoint.Y + normalVector3D.YComponent)

            polygonPointCollection.AddPoint(vertexPoint, _missing, _missing)
        Next i

        Dim polygon As IPolygon = TryCast(polygonPointCollection, IPolygon)
        polygon.Close()

        Dim baseGeometry As IGeometry = TryCast(polygon, IGeometry)

        Dim topologicalOperator As ITopologicalOperator = TryCast(polygon, ITopologicalOperator)
        topologicalOperator.Simplify()

        'Functional Surfaces

        Dim envelope As IEnvelope = New EnvelopeClass()
        envelope.XMin = -10
        envelope.XMax = 10
        envelope.YMin = -10
        envelope.YMax = 10

        Dim random As Random = New Random()

        'Upper Functional Surface

        Dim upperTinEdit As ITinEdit = New TinClass()
        upperTinEdit.InitNew(envelope)

        For i As Integer = 0 To PointCount - 1
            Dim x As Double = envelope.XMin + (envelope.XMax - envelope.XMin) * random.NextDouble()
            Dim y As Double = envelope.YMin + (envelope.YMax - envelope.YMin) * random.NextDouble()
            Dim z As Double = UpperZMin + (UpperZMax - UpperZMin) * random.NextDouble()

            Dim point As IPoint = GeometryUtilities.ConstructPoint3D(x, y, z)

            upperTinEdit.AddPointZ(point, 0)
        Next i

        Dim upperFunctionalSurface As IFunctionalSurface = TryCast(upperTinEdit, IFunctionalSurface)

        'Lower Functional Surface

        Dim lowerTinEdit As ITinEdit = New TinClass()
        lowerTinEdit.InitNew(envelope)

        For i As Integer = 0 To PointCount - 1
            Dim x As Double = envelope.XMin + (envelope.XMax - envelope.XMin) * random.NextDouble()
            Dim y As Double = envelope.YMin + (envelope.YMax - envelope.YMin) * random.NextDouble()
            Dim z As Double = LowerZMin + (LowerZMax - LowerZMin) * random.NextDouble()

            Dim point As IPoint = GeometryUtilities.ConstructPoint3D(x, y, z)

            lowerTinEdit.AddPointZ(point, 0)
        Next i

        Dim lowerFunctionalSurface As IFunctionalSurface = TryCast(lowerTinEdit, IFunctionalSurface)

        Dim constructMultiPatch As IConstructMultiPatch = New MultiPatchClass()
        constructMultiPatch.ConstructExtrudeBetween(upperFunctionalSurface, lowerFunctionalSurface, baseGeometry)

        Return TryCast(constructMultiPatch, IGeometry)
    End Function
End Class
