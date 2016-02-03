Imports Microsoft.VisualBasic
Imports ESRI.ArcGIS.Geometry
Imports System


Public Class CompositeExamples
    Private Shared _missing As Object = Type.Missing

    Private Sub New()
    End Sub
    Public Shared Function GetExample1() As IGeometry
        'Composite: Multiple, Disjoint Geometries Contained Within A Single MultiPatch

        Dim multiPatchGeometryCollection As IGeometryCollection = New MultiPatchClass()

        Dim multiPatch As IMultiPatch = TryCast(multiPatchGeometryCollection, IMultiPatch)

        'Vector3D Example 2

        Dim vector3DExample2Geometry As IGeometry = Vector3DExamples.GetExample2()

        Dim vector3DExample2Transform3D As ITransform3D = TryCast(vector3DExample2Geometry, ITransform3D)
        vector3DExample2Transform3D.Move3D(5, 5, 0)

        Dim vector3DExample2GeometryCollection As IGeometryCollection = TryCast(vector3DExample2Geometry, IGeometryCollection)

        Dim i As Integer = 0
        Do While i < vector3DExample2GeometryCollection.GeometryCount
            multiPatchGeometryCollection.AddGeometry(vector3DExample2GeometryCollection.Geometry(i), _missing, _missing)
            i += 1
        Loop

        'Vector3D Example 3

        Dim vector3DExample3Geometry As IGeometry = Vector3DExamples.GetExample3()

        Dim vector3DExample3Transform3D As ITransform3D = TryCast(vector3DExample3Geometry, ITransform3D)
        vector3DExample3Transform3D.Move3D(5, -5, 0)

        Dim vector3DExample3GeometryCollection As IGeometryCollection = TryCast(vector3DExample3Geometry, IGeometryCollection)

        i = 0
        Do While i < vector3DExample3GeometryCollection.GeometryCount
            multiPatchGeometryCollection.AddGeometry(vector3DExample3GeometryCollection.Geometry(i), _missing, _missing)
            i += 1
        Loop

        'Vector3D Example 4

        Dim vector3DExample4Geometry As IGeometry = Vector3DExamples.GetExample4()

        Dim vector3DExample4Transform3D As ITransform3D = TryCast(vector3DExample4Geometry, ITransform3D)
        vector3DExample4Transform3D.Move3D(-5, -5, 0)

        Dim vector3DExample4GeometryCollection As IGeometryCollection = TryCast(vector3DExample4Geometry, IGeometryCollection)

        i = 0
        Do While i < vector3DExample4GeometryCollection.GeometryCount
            multiPatchGeometryCollection.AddGeometry(vector3DExample4GeometryCollection.Geometry(i), _missing, _missing)
            i += 1
        Loop

        'Vector3D Example 5

        Dim vector3DExample5Geometry As IGeometry = Vector3DExamples.GetExample5()

        Dim vector3DExample5Transform3D As ITransform3D = TryCast(vector3DExample5Geometry, ITransform3D)
        vector3DExample5Transform3D.Move3D(-5, 5, 0)

        Dim vector3DExample5GeometryCollection As IGeometryCollection = TryCast(vector3DExample5Geometry, IGeometryCollection)

        i = 0
        Do While i < vector3DExample5GeometryCollection.GeometryCount
            multiPatchGeometryCollection.AddGeometry(vector3DExample5GeometryCollection.Geometry(i), _missing, _missing)
            i += 1
        Loop

        Return TryCast(multiPatchGeometryCollection, IGeometry)
    End Function

    Public Shared Function GetExample2() As IGeometry
        'Composite: Cutaway Of Building With Multiple Floors Composed Of 1 TriangleStrip And 5 Ring Parts

        Dim multiPatchGeometryCollection As IGeometryCollection = New MultiPatchClass()

        Dim multiPatch As IMultiPatch = TryCast(multiPatchGeometryCollection, IMultiPatch)

        'Walls

        Dim wallsPointCollection As IPointCollection = New TriangleStripClass()

        'Start

        wallsPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(3, -3, 0), _missing, _missing)
        wallsPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(3, -3, 16), _missing, _missing)

        'Right Wall

        wallsPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(3, 3, 0), _missing, _missing)
        wallsPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(3, 3, 16), _missing, _missing)

        'Back Wall

        wallsPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-3, 3, 0), _missing, _missing)
        wallsPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-3, 3, 16), _missing, _missing)

        'Left Wall

        wallsPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-3, -3, 0), _missing, _missing)
        wallsPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-3, -3, 16), _missing, _missing)

        multiPatchGeometryCollection.AddGeometry(TryCast(wallsPointCollection, IGeometry), _missing, _missing)

        'Floors

        'Base

        Dim basePointCollection As IPointCollection = New RingClass()
        basePointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(3, 3, 0), _missing, _missing)
        basePointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(3, -3, 0), _missing, _missing)
        basePointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-3, -3, 0), _missing, _missing)
        basePointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-3, 3, 0), _missing, _missing)

        Dim baseRing As IRing = TryCast(basePointCollection, IRing)
        baseRing.Close()

        multiPatchGeometryCollection.AddGeometry(TryCast(baseRing, IGeometry), _missing, _missing)

        'First Floor

        Dim firstFloorPointCollection As IPointCollection = New RingClass()
        firstFloorPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(3, 3, 4), _missing, _missing)
        firstFloorPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(3, -3, 4), _missing, _missing)
        firstFloorPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-3, -3, 4), _missing, _missing)
        firstFloorPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-3, 3, 4), _missing, _missing)

        Dim firstFloorRing As IRing = TryCast(firstFloorPointCollection, IRing)
        firstFloorRing.Close()

        multiPatchGeometryCollection.AddGeometry(TryCast(firstFloorRing, IGeometry), _missing, _missing)

        'Second Floor

        Dim secondFloorPointCollection As IPointCollection = New RingClass()
        secondFloorPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(3, 3, 8), _missing, _missing)
        secondFloorPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(3, -3, 8), _missing, _missing)
        secondFloorPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-3, -3, 8), _missing, _missing)
        secondFloorPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-3, 3, 8), _missing, _missing)

        Dim secondFloorRing As IRing = TryCast(secondFloorPointCollection, IRing)
        secondFloorRing.Close()

        multiPatchGeometryCollection.AddGeometry(TryCast(secondFloorRing, IGeometry), _missing, _missing)

        'Third Floor

        Dim thirdFloorPointCollection As IPointCollection = New RingClass()
        thirdFloorPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(3, 3, 12), _missing, _missing)
        thirdFloorPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(3, -3, 12), _missing, _missing)
        thirdFloorPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-3, -3, 12), _missing, _missing)
        thirdFloorPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-3, 3, 12), _missing, _missing)

        Dim thirdFloorRing As IRing = TryCast(thirdFloorPointCollection, IRing)
        thirdFloorRing.Close()

        multiPatchGeometryCollection.AddGeometry(TryCast(thirdFloorRing, IGeometry), _missing, _missing)

        'Roof

        Dim roofPointCollection As IPointCollection = New RingClass()
        roofPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(3, 3, 16), _missing, _missing)
        roofPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(3, -3, 16), _missing, _missing)
        roofPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-3, -3, 16), _missing, _missing)
        roofPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-3, 3, 16), _missing, _missing)

        Dim roofRing As IRing = TryCast(roofPointCollection, IRing)
        roofRing.Close()

        multiPatchGeometryCollection.AddGeometry(TryCast(roofRing, IGeometry), _missing, _missing)

        Return TryCast(multiPatchGeometryCollection, IGeometry)
    End Function

    Public Shared Function GetExample3() As IGeometry
        'Composite: House Composed Of 7 Ring, 1 TriangleStrip, And 1 Triangles Parts

        Dim multiPatchGeometryCollection As IGeometryCollection = New MultiPatchClass()

        Dim multiPatch As IMultiPatch = TryCast(multiPatchGeometryCollection, IMultiPatch)

        'Base (Exterior Ring)

        Dim basePointCollection As IPointCollection = New RingClass()
        basePointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(5, 4, 0), _missing, _missing)
        basePointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-5, 4, 0), _missing, _missing)
        basePointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-5, -4, 0), _missing, _missing)
        basePointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(5, -4, 0), _missing, _missing)

        Dim baseRing As IRing = TryCast(basePointCollection, IRing)
        baseRing.Close()

        multiPatchGeometryCollection.AddGeometry(TryCast(baseRing, IGeometry), _missing, _missing)

        multiPatch.PutRingType(baseRing, esriMultiPatchRingType.esriMultiPatchOuterRing)

        'Front With Cutaway For Door (Exterior Ring)

        Dim frontPointCollection As IPointCollection = New RingClass()
        frontPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(5, 4, 6), _missing, _missing)
        frontPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(5, 4, 0), _missing, _missing)
        frontPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(5, 1, 0), _missing, _missing)
        frontPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(5, 1, 4), _missing, _missing)
        frontPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(5, -1, 4), _missing, _missing)
        frontPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(5, -1, 0), _missing, _missing)
        frontPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(5, -4, 0), _missing, _missing)
        frontPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(5, -4, 6), _missing, _missing)

        Dim frontRing As IRing = TryCast(frontPointCollection, IRing)
        frontRing.Close()

        multiPatchGeometryCollection.AddGeometry(TryCast(frontRing, IGeometry), _missing, _missing)

        multiPatch.PutRingType(frontRing, esriMultiPatchRingType.esriMultiPatchOuterRing)

        'Back (Exterior Ring)

        Dim backPointCollection As IPointCollection = New RingClass()
        backPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-5, 4, 6), _missing, _missing)
        backPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-5, -4, 6), _missing, _missing)
        backPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-5, -4, 0), _missing, _missing)
        backPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-5, 4, 0), _missing, _missing)

        Dim backRing As IRing = TryCast(backPointCollection, IRing)
        backRing.Close()

        multiPatchGeometryCollection.AddGeometry(TryCast(backRing, IGeometry), _missing, _missing)

        multiPatch.PutRingType(backRing, esriMultiPatchRingType.esriMultiPatchOuterRing)

        'Right Side (Ring Group)

        'Exterior Ring

        Dim rightSideExteriorPointCollection As IPointCollection = New RingClass()
        rightSideExteriorPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(5, 4, 6), _missing, _missing)
        rightSideExteriorPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-5, 4, 6), _missing, _missing)
        rightSideExteriorPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-5, 4, 0), _missing, _missing)
        rightSideExteriorPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(5, 4, 0), _missing, _missing)

        Dim rightSideExteriorRing As IRing = TryCast(rightSideExteriorPointCollection, IRing)
        rightSideExteriorRing.Close()

        multiPatchGeometryCollection.AddGeometry(TryCast(rightSideExteriorRing, IGeometry), _missing, _missing)

        multiPatch.PutRingType(rightSideExteriorRing, esriMultiPatchRingType.esriMultiPatchOuterRing)

        'Interior Ring

        Dim rightSideInteriorPointCollection As IPointCollection = New RingClass()
        rightSideInteriorPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(1, 4, 4), _missing, _missing)
        rightSideInteriorPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(1, 4, 2), _missing, _missing)
        rightSideInteriorPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-1, 4, 2), _missing, _missing)
        rightSideInteriorPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-1, 4, 4), _missing, _missing)

        Dim rightSideInteriorRing As IRing = TryCast(rightSideInteriorPointCollection, IRing)
        rightSideInteriorRing.Close()

        multiPatchGeometryCollection.AddGeometry(TryCast(rightSideInteriorRing, IGeometry), _missing, _missing)

        multiPatch.PutRingType(rightSideInteriorRing, esriMultiPatchRingType.esriMultiPatchInnerRing)

        'Left Side (Ring Group)

        'Exterior Ring

        Dim leftSideExteriorPointCollection As IPointCollection = New RingClass()
        leftSideExteriorPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(5, -4, 6), _missing, _missing)
        leftSideExteriorPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(5, -4, 0), _missing, _missing)
        leftSideExteriorPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-5, -4, 0), _missing, _missing)
        leftSideExteriorPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-5, -4, 6), _missing, _missing)

        Dim leftSideExteriorRing As IRing = TryCast(leftSideExteriorPointCollection, IRing)
        leftSideExteriorRing.Close()

        multiPatchGeometryCollection.AddGeometry(TryCast(leftSideExteriorRing, IGeometry), _missing, _missing)

        multiPatch.PutRingType(leftSideExteriorRing, esriMultiPatchRingType.esriMultiPatchOuterRing)

        'Interior Ring

        Dim leftSideInteriorPointCollection As IPointCollection = New RingClass()
        leftSideInteriorPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(1, -4, 4), _missing, _missing)
        leftSideInteriorPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-1, -4, 4), _missing, _missing)
        leftSideInteriorPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-1, -4, 2), _missing, _missing)
        leftSideInteriorPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(1, -4, 2), _missing, _missing)

        Dim leftSideInteriorRing As IRing = TryCast(leftSideInteriorPointCollection, IRing)
        leftSideInteriorRing.Close()

        multiPatchGeometryCollection.AddGeometry(TryCast(leftSideInteriorRing, IGeometry), _missing, _missing)

        multiPatch.PutRingType(leftSideInteriorRing, esriMultiPatchRingType.esriMultiPatchInnerRing)

        'Roof

        Dim roofPointCollection As IPointCollection = New TriangleStripClass()
        roofPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-5, 4, 6), _missing, _missing)
        roofPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(5, 4, 6), _missing, _missing)
        roofPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-5, 0, 9), _missing, _missing)
        roofPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(5, 0, 9), _missing, _missing)
        roofPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-5, -4, 6), _missing, _missing)
        roofPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(5, -4, 6), _missing, _missing)

        multiPatchGeometryCollection.AddGeometry(TryCast(roofPointCollection, IGeometry), _missing, _missing)

        'Triangular Area Between Roof And Front/Back

        Dim triangularAreaPointCollection As IPointCollection = New TrianglesClass()

        'Area Between Roof And Front

        triangularAreaPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(5, 0, 9), _missing, _missing)
        triangularAreaPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(5, 4, 6), _missing, _missing)
        triangularAreaPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(5, -4, 6), _missing, _missing)

        'Area Between Roof And Back

        triangularAreaPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-5, 0, 9), _missing, _missing)
        triangularAreaPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-5, -4, 6), _missing, _missing)
        triangularAreaPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-5, 4, 6), _missing, _missing)

        multiPatchGeometryCollection.AddGeometry(TryCast(triangularAreaPointCollection, IGeometry), _missing, _missing)

        Return TryCast(multiPatchGeometryCollection, IGeometry)
    End Function

    Public Shared Function GetExample4() As IGeometry
        Const CircleDegrees As Double = 360.0
        Const CircleDivisions As Integer = 18
        Const VectorComponentOffset As Double = 0.0000001
        Const InnerBuildingRadius As Double = 3.0
        Const OuterBuildingExteriorRingRadius As Double = 9.0
        Const OuterBuildingInteriorRingRadius As Double = 6.0
        Const BaseZ As Double = 0.0
        Const InnerBuildingZ As Double = 16.0
        Const OuterBuildingZ As Double = 6.0

        'Composite: Tall Building Protruding Through Outer Ring-Shaped Building

        Dim multiPatch As IMultiPatch = New MultiPatchClass()

        Dim multiPatchGeometryCollection As IGeometryCollection = TryCast(multiPatch, IGeometryCollection)

        Dim originPoint As IPoint = GeometryUtilities.ConstructPoint3D(0, 0, 0)

        Dim upperAxisVector3D As IVector3D = GeometryUtilities.ConstructVector3D(0, 0, 10)

        Dim lowerAxisVector3D As IVector3D = GeometryUtilities.ConstructVector3D(0, 0, -10)

        lowerAxisVector3D.XComponent += VectorComponentOffset

        Dim normalVector3D As IVector3D = TryCast(upperAxisVector3D.CrossProduct(lowerAxisVector3D), IVector3D)

        Dim rotationAngleInRadians As Double = GeometryUtilities.GetRadians(CircleDegrees / CircleDivisions)

        'Inner Building

        Dim innerBuildingBaseGeometry As IGeometry = New PolygonClass()

        Dim innerBuildingBasePointCollection As IPointCollection = TryCast(innerBuildingBaseGeometry, IPointCollection)

        'Outer Building

        Dim outerBuildingBaseGeometry As IGeometry = New PolygonClass()

        Dim outerBuildingBaseGeometryCollection As IGeometryCollection = TryCast(outerBuildingBaseGeometry, IGeometryCollection)

        Dim outerBuildingBaseExteriorRingPointCollection As IPointCollection = New RingClass()

        Dim outerBuildingBaseInteriorRingPointCollection As IPointCollection = New RingClass()

        Dim i As Integer
        For i = 0 To CircleDivisions - 1
            normalVector3D.Rotate(-1 * rotationAngleInRadians, upperAxisVector3D)

            'Inner Building

            normalVector3D.Magnitude = InnerBuildingRadius

            Dim innerBuildingBaseVertexPoint As IPoint = GeometryUtilities.ConstructPoint2D(originPoint.X + normalVector3D.XComponent, originPoint.Y + normalVector3D.YComponent)

            innerBuildingBasePointCollection.AddPoint(innerBuildingBaseVertexPoint, _missing, _missing)

            'Outer Building

            'Exterior Ring

            normalVector3D.Magnitude = OuterBuildingExteriorRingRadius

            Dim outerBuildingBaseExteriorRingVertexPoint As IPoint = GeometryUtilities.ConstructPoint2D(originPoint.X + normalVector3D.XComponent, originPoint.Y + normalVector3D.YComponent)

            outerBuildingBaseExteriorRingPointCollection.AddPoint(outerBuildingBaseExteriorRingVertexPoint, _missing, _missing)

            'Interior Ring

            normalVector3D.Magnitude = OuterBuildingInteriorRingRadius

            Dim outerBuildingBaseInteriorRingVertexPoint As IPoint = GeometryUtilities.ConstructPoint2D(originPoint.X + normalVector3D.XComponent, originPoint.Y + normalVector3D.YComponent)

            outerBuildingBaseInteriorRingPointCollection.AddPoint(outerBuildingBaseInteriorRingVertexPoint, _missing, _missing)
        Next i

        Dim innerBuildingBasePolygon As IPolygon = TryCast(innerBuildingBaseGeometry, IPolygon)
        innerBuildingBasePolygon.Close()

        Dim outerBuildingBaseExteriorRing As IRing = TryCast(outerBuildingBaseExteriorRingPointCollection, IRing)
        outerBuildingBaseExteriorRing.Close()

        Dim outerBuildingBaseInteriorRing As IRing = TryCast(outerBuildingBaseInteriorRingPointCollection, IRing)
        outerBuildingBaseInteriorRing.Close()
        outerBuildingBaseInteriorRing.ReverseOrientation()

        outerBuildingBaseGeometryCollection.AddGeometry(TryCast(outerBuildingBaseExteriorRing, IGeometry), _missing, _missing)
        outerBuildingBaseGeometryCollection.AddGeometry(TryCast(outerBuildingBaseInteriorRing, IGeometry), _missing, _missing)

        Dim topologicalOperator As ITopologicalOperator = TryCast(outerBuildingBaseGeometry, ITopologicalOperator)
        topologicalOperator.Simplify()

        Dim innerBuildingConstructMultiPatch As IConstructMultiPatch = New MultiPatchClass()
        innerBuildingConstructMultiPatch.ConstructExtrudeFromTo(BaseZ, InnerBuildingZ, innerBuildingBaseGeometry)

        Dim innerBuildingMultiPatchGeometryCollection As IGeometryCollection = TryCast(innerBuildingConstructMultiPatch, IGeometryCollection)

        i = 0
        Do While i < innerBuildingMultiPatchGeometryCollection.GeometryCount
            multiPatchGeometryCollection.AddGeometry(innerBuildingMultiPatchGeometryCollection.Geometry(i), _missing, _missing)
            i += 1
        Loop

        Dim outerBuildingConstructMultiPatch As IConstructMultiPatch = New MultiPatchClass()
        outerBuildingConstructMultiPatch.ConstructExtrudeFromTo(BaseZ, OuterBuildingZ, outerBuildingBaseGeometry)

        Dim outerBuildingMultiPatch As IMultiPatch = TryCast(outerBuildingConstructMultiPatch, IMultiPatch)

        Dim outerBuildingMultiPatchGeometryCollection As IGeometryCollection = TryCast(outerBuildingConstructMultiPatch, IGeometryCollection)

        i = 0
        Do While i < outerBuildingMultiPatchGeometryCollection.GeometryCount
            Dim outerBuildingPatchGeometry As IGeometry = outerBuildingMultiPatchGeometryCollection.Geometry(i)

            multiPatchGeometryCollection.AddGeometry(outerBuildingPatchGeometry, _missing, _missing)

            If outerBuildingPatchGeometry.GeometryType = esriGeometryType.esriGeometryRing Then
                Dim isBeginningRing As Boolean = False

                Dim multiPatchRingType As esriMultiPatchRingType = outerBuildingMultiPatch.GetRingType(TryCast(outerBuildingPatchGeometry, IRing), isBeginningRing)

                multiPatch.PutRingType(TryCast(outerBuildingPatchGeometry, IRing), multiPatchRingType)
            End If
            i += 1
        Loop

        Return TryCast(multiPatchGeometryCollection, IGeometry)
    End Function
End Class
