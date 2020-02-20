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


Public Class RingGroupExamples
    Private Shared _missing As Object = Type.Missing

    Private Sub New()
    End Sub
    Public Shared Function GetExample1() As IGeometry
        'RingGroup: Multiple Rings

        Dim multiPatchGeometryCollection As IGeometryCollection = New MultiPatchClass()

        'Ring 1

        Dim ring1PointCollection As IPointCollection = New RingClass()
        ring1PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(1, 1, 0), _missing, _missing)
        ring1PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(1, 4, 0), _missing, _missing)
        ring1PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(4, 4, 0), _missing, _missing)
        ring1PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(4, 1, 0), _missing, _missing)

        Dim ring1 As IRing = TryCast(ring1PointCollection, IRing)
        ring1.Close()

        multiPatchGeometryCollection.AddGeometry(TryCast(ring1, IGeometry), _missing, _missing)

        'Ring 2

        Dim ring2PointCollection As IPointCollection = New RingClass()
        ring2PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(1, -1, 0), _missing, _missing)
        ring2PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(4, -1, 0), _missing, _missing)
        ring2PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(4, -4, 0), _missing, _missing)
        ring2PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(1, -4, 0), _missing, _missing)

        Dim ring2 As IRing = TryCast(ring2PointCollection, IRing)
        ring2.Close()

        multiPatchGeometryCollection.AddGeometry(TryCast(ring2, IGeometry), _missing, _missing)

        'Ring 3

        Dim ring3PointCollection As IPointCollection = New RingClass()
        ring3PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-1, 1, 0), _missing, _missing)
        ring3PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-4, 1, 0), _missing, _missing)
        ring3PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-4, 4, 0), _missing, _missing)
        ring3PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-1, 4, 0), _missing, _missing)

        Dim ring3 As IRing = TryCast(ring3PointCollection, IRing)
        ring3.Close()

        multiPatchGeometryCollection.AddGeometry(TryCast(ring3, IGeometry), _missing, _missing)

        'Ring 4

        Dim ring4PointCollection As IPointCollection = New RingClass()
        ring4PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-1, -1, 0), _missing, _missing)
        ring4PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-1, -4, 0), _missing, _missing)
        ring4PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-4, -4, 0), _missing, _missing)
        ring4PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-4, -1, 0), _missing, _missing)

        Dim ring4 As IRing = TryCast(ring4PointCollection, IRing)
        ring4.Close()

        multiPatchGeometryCollection.AddGeometry(TryCast(ring4, IGeometry), _missing, _missing)

        Return TryCast(multiPatchGeometryCollection, IGeometry)
    End Function

    Public Shared Function GetExample2() As IGeometry
        'RingGroup: Multiple Exterior Rings With Corresponding Interior Rings

        Dim multiPatchGeometryCollection As IGeometryCollection = New MultiPatchClass()

        Dim multiPatch As IMultiPatch = TryCast(multiPatchGeometryCollection, IMultiPatch)

        'Exterior Ring 1

        Dim exteriorRing1PointCollection As IPointCollection = New RingClass()
        exteriorRing1PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(1, 1, 0), _missing, _missing)
        exteriorRing1PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(1, 4, 0), _missing, _missing)
        exteriorRing1PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(4, 4, 0), _missing, _missing)
        exteriorRing1PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(4, 1, 0), _missing, _missing)

        Dim exteriorRing1 As IRing = TryCast(exteriorRing1PointCollection, IRing)
        exteriorRing1.Close()

        multiPatchGeometryCollection.AddGeometry(TryCast(exteriorRing1, IGeometry), _missing, _missing)

        multiPatch.PutRingType(exteriorRing1, esriMultiPatchRingType.esriMultiPatchOuterRing)

        'Interior Ring 1

        Dim interiorRing1PointCollection As IPointCollection = New RingClass()
        interiorRing1PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(3.5, 1.5, 0), _missing, _missing)
        interiorRing1PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(3.5, 3.5, 0), _missing, _missing)
        interiorRing1PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(1.5, 3.5, 0), _missing, _missing)
        interiorRing1PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(1.5, 1.5, 0), _missing, _missing)

        Dim interiorRing1 As IRing = TryCast(interiorRing1PointCollection, IRing)
        interiorRing1.Close()

        multiPatchGeometryCollection.AddGeometry(TryCast(interiorRing1, IGeometry), _missing, _missing)

        multiPatch.PutRingType(interiorRing1, esriMultiPatchRingType.esriMultiPatchInnerRing)

        'Exterior Ring 2

        Dim exteriorRing2PointCollection As IPointCollection = New RingClass()
        exteriorRing2PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(1, -1, 0), _missing, _missing)
        exteriorRing2PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(4, -1, 0), _missing, _missing)
        exteriorRing2PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(4, -4, 0), _missing, _missing)
        exteriorRing2PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(1, -4, 0), _missing, _missing)

        Dim exteriorRing2 As IRing = TryCast(exteriorRing2PointCollection, IRing)
        exteriorRing2.Close()

        multiPatchGeometryCollection.AddGeometry(TryCast(exteriorRing2, IGeometry), _missing, _missing)

        multiPatch.PutRingType(exteriorRing2, esriMultiPatchRingType.esriMultiPatchOuterRing)

        'Interior Ring 2

        Dim interiorRing2PointCollection As IPointCollection = New RingClass()
        interiorRing2PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(1.5, -1.5, 0), _missing, _missing)
        interiorRing2PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(3.5, -1.5, 0), _missing, _missing)
        interiorRing2PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(3.5, -3.5, 0), _missing, _missing)
        interiorRing2PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(1.5, -3.5, 0), _missing, _missing)

        Dim interiorRing2 As IRing = TryCast(interiorRing2PointCollection, IRing)
        interiorRing2.Close()

        multiPatchGeometryCollection.AddGeometry(TryCast(interiorRing2, IGeometry), _missing, _missing)

        multiPatch.PutRingType(interiorRing2, esriMultiPatchRingType.esriMultiPatchInnerRing)

        'Exterior Ring 3

        Dim exteriorRing3PointCollection As IPointCollection = New RingClass()
        exteriorRing3PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-1, 1, 0), _missing, _missing)
        exteriorRing3PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-4, 1, 0), _missing, _missing)
        exteriorRing3PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-4, 4, 0), _missing, _missing)
        exteriorRing3PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-1, 4, 0), _missing, _missing)

        Dim exteriorRing3 As IRing = TryCast(exteriorRing3PointCollection, IRing)
        exteriorRing3.Close()

        multiPatchGeometryCollection.AddGeometry(TryCast(exteriorRing3, IGeometry), _missing, _missing)

        multiPatch.PutRingType(exteriorRing3, esriMultiPatchRingType.esriMultiPatchOuterRing)

        'Interior Ring 3

        Dim interiorRing3PointCollection As IPointCollection = New RingClass()
        interiorRing3PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-1.5, 1.5, 0), _missing, _missing)
        interiorRing3PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-3.5, 1.5, 0), _missing, _missing)
        interiorRing3PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-3.5, 3.5, 0), _missing, _missing)
        interiorRing3PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-1.5, 3.5, 0), _missing, _missing)

        Dim interiorRing3 As IRing = TryCast(interiorRing3PointCollection, IRing)
        interiorRing3.Close()

        multiPatchGeometryCollection.AddGeometry(TryCast(interiorRing3, IGeometry), _missing, _missing)

        multiPatch.PutRingType(interiorRing3, esriMultiPatchRingType.esriMultiPatchInnerRing)

        'Exterior Ring 4

        Dim exteriorRing4PointCollection As IPointCollection = New RingClass()
        exteriorRing4PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-1, -1, 0), _missing, _missing)
        exteriorRing4PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-1, -4, 0), _missing, _missing)
        exteriorRing4PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-4, -4, 0), _missing, _missing)
        exteriorRing4PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-4, -1, 0), _missing, _missing)

        Dim exteriorRing4 As IRing = TryCast(exteriorRing4PointCollection, IRing)
        exteriorRing4.Close()

        multiPatchGeometryCollection.AddGeometry(TryCast(exteriorRing4, IGeometry), _missing, _missing)

        multiPatch.PutRingType(exteriorRing4, esriMultiPatchRingType.esriMultiPatchOuterRing)

        'Interior Ring 4

        Dim interiorRing4PointCollection As IPointCollection = New RingClass()
        interiorRing4PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-1.5, -1.5, 0), _missing, _missing)
        interiorRing4PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-1.5, -3.5, 0), _missing, _missing)
        interiorRing4PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-3.5, -3.5, 0), _missing, _missing)
        interiorRing4PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-3.5, -1.5, 0), _missing, _missing)

        Dim interiorRing4 As IRing = TryCast(interiorRing4PointCollection, IRing)
        interiorRing4.Close()

        multiPatchGeometryCollection.AddGeometry(TryCast(interiorRing4, IGeometry), _missing, _missing)

        multiPatch.PutRingType(interiorRing4, esriMultiPatchRingType.esriMultiPatchInnerRing)

        Return TryCast(multiPatchGeometryCollection, IGeometry)
    End Function

    Public Shared Function GetExample3() As IGeometry
        'RingGroup: Upright Square With Hole

        Dim multiPatchGeometryCollection As IGeometryCollection = New MultiPatchClass()

        Dim multiPatch As IMultiPatch = TryCast(multiPatchGeometryCollection, IMultiPatch)

        'Exterior Ring 1

        Dim exteriorRing1PointCollection As IPointCollection = New RingClass()
        exteriorRing1PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(5, 0, -5), _missing, _missing)
        exteriorRing1PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-5, 0, -5), _missing, _missing)
        exteriorRing1PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-5, 0, 5), _missing, _missing)
        exteriorRing1PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(5, 0, 5), _missing, _missing)

        Dim exteriorRing1 As IRing = TryCast(exteriorRing1PointCollection, IRing)
        exteriorRing1.Close()

        multiPatchGeometryCollection.AddGeometry(TryCast(exteriorRing1, IGeometry), _missing, _missing)

        multiPatch.PutRingType(exteriorRing1, esriMultiPatchRingType.esriMultiPatchOuterRing)

        'Interior Ring 1

        Dim interiorRing1PointCollection As IPointCollection = New RingClass()
        interiorRing1PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-4, 0, -4), _missing, _missing)
        interiorRing1PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(4, 0, -4), _missing, _missing)
        interiorRing1PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(4, 0, 4), _missing, _missing)
        interiorRing1PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-4, 0, 4), _missing, _missing)

        Dim interiorRing1 As IRing = TryCast(interiorRing1PointCollection, IRing)
        interiorRing1.Close()

        multiPatchGeometryCollection.AddGeometry(TryCast(interiorRing1, IGeometry), _missing, _missing)

        multiPatch.PutRingType(interiorRing1, esriMultiPatchRingType.esriMultiPatchInnerRing)

        Return TryCast(multiPatchGeometryCollection, IGeometry)
    End Function

    Public Shared Function GetExample4() As IGeometry
        'RingGroup: Upright Square Composed Of Multiple Exterior Rings And Multiple Interior Rings

        Dim multiPatchGeometryCollection As IGeometryCollection = New MultiPatchClass()

        Dim multiPatch As IMultiPatch = TryCast(multiPatchGeometryCollection, IMultiPatch)

        'Exterior Ring 1

        Dim exteriorRing1PointCollection As IPointCollection = New RingClass()
        exteriorRing1PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(5, 0, -5), _missing, _missing)
        exteriorRing1PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-5, 0, -5), _missing, _missing)
        exteriorRing1PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-5, 0, 5), _missing, _missing)
        exteriorRing1PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(5, 0, 5), _missing, _missing)

        Dim exteriorRing1 As IRing = TryCast(exteriorRing1PointCollection, IRing)
        exteriorRing1.Close()

        multiPatchGeometryCollection.AddGeometry(TryCast(exteriorRing1, IGeometry), _missing, _missing)

        multiPatch.PutRingType(exteriorRing1, esriMultiPatchRingType.esriMultiPatchOuterRing)

        'Interior Ring 1

        Dim interiorRing1PointCollection As IPointCollection = New RingClass()
        interiorRing1PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-4, 0, -4), _missing, _missing)
        interiorRing1PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(4, 0, -4), _missing, _missing)
        interiorRing1PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(4, 0, 4), _missing, _missing)
        interiorRing1PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-4, 0, 4), _missing, _missing)

        Dim interiorRing1 As IRing = TryCast(interiorRing1PointCollection, IRing)
        interiorRing1.Close()

        multiPatchGeometryCollection.AddGeometry(TryCast(interiorRing1, IGeometry), _missing, _missing)

        multiPatch.PutRingType(interiorRing1, esriMultiPatchRingType.esriMultiPatchInnerRing)

        'Exterior Ring 2 

        Dim exteriorRing2PointCollection As IPointCollection = New RingClass()
        exteriorRing2PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(3, 0, -3), _missing, _missing)
        exteriorRing2PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-3, 0, -3), _missing, _missing)
        exteriorRing2PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-3, 0, 3), _missing, _missing)
        exteriorRing2PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(3, 0, 3), _missing, _missing)

        Dim exteriorRing2 As IRing = TryCast(exteriorRing2PointCollection, IRing)
        exteriorRing2.Close()

        multiPatchGeometryCollection.AddGeometry(TryCast(exteriorRing2, IGeometry), _missing, _missing)

        multiPatch.PutRingType(exteriorRing2, esriMultiPatchRingType.esriMultiPatchOuterRing)

        'Interior Ring 2

        Dim interiorRing2PointCollection As IPointCollection = New RingClass()
        interiorRing2PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-2, 0, -2), _missing, _missing)
        interiorRing2PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(2, 0, -2), _missing, _missing)
        interiorRing2PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(2, 0, 2), _missing, _missing)
        interiorRing2PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-2, 0, 2), _missing, _missing)

        Dim interiorRing2 As IRing = TryCast(interiorRing2PointCollection, IRing)
        interiorRing2.Close()

        multiPatchGeometryCollection.AddGeometry(TryCast(interiorRing2, IGeometry), _missing, _missing)

        multiPatch.PutRingType(interiorRing2, esriMultiPatchRingType.esriMultiPatchInnerRing)

        'Exterior Ring 3 

        Dim exteriorRing3PointCollection As IPointCollection = New RingClass()
        exteriorRing3PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(1, 0, -1), _missing, _missing)
        exteriorRing3PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-1, 0, -1), _missing, _missing)
        exteriorRing3PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-1, 0, 1), _missing, _missing)
        exteriorRing3PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(1, 0, 1), _missing, _missing)

        Dim exteriorRing3 As IRing = TryCast(exteriorRing3PointCollection, IRing)
        exteriorRing3.Close()

        multiPatchGeometryCollection.AddGeometry(TryCast(exteriorRing3, IGeometry), _missing, _missing)

        multiPatch.PutRingType(exteriorRing3, esriMultiPatchRingType.esriMultiPatchOuterRing)

        Return TryCast(multiPatchGeometryCollection, IGeometry)
    End Function

    Public Shared Function GetExample5() As IGeometry
        Const XRange As Integer = 16
        Const YRange As Integer = 16
        Const InteriorRingCount As Integer = 25
        Const HoleRange As Double = 0.5

        'RingGroup: Square Lying In XY Plane With Single Exterior Ring And Multiple Interior Rings

        Dim multiPatchGeometryCollection As IGeometryCollection = New MultiPatchClass()

        Dim multiPatch As IMultiPatch = TryCast(multiPatchGeometryCollection, IMultiPatch)

        'Exterior Ring

        Dim exteriorRingPointCollection As IPointCollection = New RingClass()
        exteriorRingPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0.5 * (XRange + 2), -0.5 * (YRange + 2), 0), _missing, _missing)
        exteriorRingPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-0.5 * (XRange + 2), -0.5 * (YRange + 2), 0), _missing, _missing)
        exteriorRingPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-0.5 * (XRange + 2), 0.5 * (YRange + 2), 0), _missing, _missing)
        exteriorRingPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0.5 * (XRange + 2), 0.5 * (YRange + 2), 0), _missing, _missing)

        Dim exteriorRing As IRing = TryCast(exteriorRingPointCollection, IRing)
        exteriorRing.Close()

        multiPatchGeometryCollection.AddGeometry(TryCast(exteriorRing, IGeometry), _missing, _missing)

        multiPatch.PutRingType(exteriorRing, esriMultiPatchRingType.esriMultiPatchOuterRing)

        'Interior Rings

        Dim random As Random = New Random()

        For i As Integer = 0 To InteriorRingCount - 1
            Dim interiorRingOriginX As Double = XRange * (random.NextDouble() - 0.5)
            Dim interiorRingOriginY As Double = YRange * (random.NextDouble() - 0.5)

            Dim interiorRingPointCollection As IPointCollection = New RingClass()
            interiorRingPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(interiorRingOriginX - 0.5 * HoleRange, interiorRingOriginY - 0.5 * HoleRange, 0), _missing, _missing)
            interiorRingPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(interiorRingOriginX + 0.5 * HoleRange, interiorRingOriginY - 0.5 * HoleRange, 0), _missing, _missing)
            interiorRingPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(interiorRingOriginX + 0.5 * HoleRange, interiorRingOriginY + 0.5 * HoleRange, 0), _missing, _missing)
            interiorRingPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(interiorRingOriginX - 0.5 * HoleRange, interiorRingOriginY + 0.5 * HoleRange, 0), _missing, _missing)

            Dim interiorRing As IRing = TryCast(interiorRingPointCollection, IRing)
            interiorRing.Close()

            multiPatchGeometryCollection.AddGeometry(TryCast(interiorRing, IGeometry), _missing, _missing)

            multiPatch.PutRingType(interiorRing, esriMultiPatchRingType.esriMultiPatchInnerRing)
        Next i

        Return TryCast(multiPatchGeometryCollection, IGeometry)
    End Function
End Class

