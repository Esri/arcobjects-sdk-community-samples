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


Public Class Vector3DExamples
    Private Shared _missing As Object = Type.Missing

    Private Sub New()
    End Sub
    Public Shared Function GetExample1() As IGeometry
        Const CircleDegrees As Double = 360.0
        Const CircleDivisions As Integer = 36
        Const VectorComponentOffset As Double = 0.0000001
        Const CircleRadius As Double = 5.0
        Const CircleZ As Double = 0.0

        'Vector3D: Circle, TriangleFan With 36 Vertices

        Dim multiPatchGeometryCollection As IGeometryCollection = New MultiPatchClass()

        Dim triangleFanPointCollection As IPointCollection = New TriangleFanClass()

        'Set Circle Origin To (0, 0, CircleZ)

        Dim originPoint As IPoint = GeometryUtilities.ConstructPoint3D(0, 0, CircleZ)

        'Add Origin Point To Triangle Fan

        triangleFanPointCollection.AddPoint(originPoint, _missing, _missing)

        'Define Upper Portion Of Axis Around Which Vector Should Be Rotated To Generate Circle Vertices

        Dim upperAxisVector3D As IVector3D = GeometryUtilities.ConstructVector3D(0, 0, 10)

        'Define Lower Portion of Axis Around Which Vector Should Be Rotated To Generate Circle Vertices

        Dim lowerAxisVector3D As IVector3D = GeometryUtilities.ConstructVector3D(0, 0, -10)

        'Add Slight Offset To X or Y Component Of One Of Axis Vectors So Cross Product Does Not Return A Zero-Length Vector

        lowerAxisVector3D.XComponent += VectorComponentOffset

        'Obtain Cross Product Of Upper And Lower Axis Vectors To Obtain Normal Vector To Axis Of Rotation To Generate Circle Vertices

        Dim normalVector3D As IVector3D = TryCast(upperAxisVector3D.CrossProduct(lowerAxisVector3D), IVector3D)

        'Set Normal Vector Magnitude Equal To Radius Of Circle

        normalVector3D.Magnitude = CircleRadius

        'Obtain Angle Of Rotation In Radians As Function Of Number Of Divisions Within 360 Degree Sweep Of Circle

        Dim rotationAngleInRadians As Double = GeometryUtilities.GetRadians(CircleDegrees / CircleDivisions)

        For i As Integer = 0 To CircleDivisions - 1
            'Rotate Normal Vector Specified Rotation Angle In Radians Around Either Upper Or Lower Axis

            normalVector3D.Rotate(-1 * rotationAngleInRadians, upperAxisVector3D)

            'Construct Circle Vertex Whose XY Coordinates Are The Sum Of Origin XY Coordinates And Normal Vector XY Components

            Dim vertexPoint As IPoint = GeometryUtilities.ConstructPoint3D(originPoint.X + normalVector3D.XComponent, originPoint.Y + normalVector3D.YComponent, CircleZ)

            'Add Vertex To TriangleFan

            triangleFanPointCollection.AddPoint(vertexPoint, _missing, _missing)
        Next i

        'Re-Add The Second Point Of The Triangle Fan (First Vertex Added) To Close The Fan

        triangleFanPointCollection.AddPoint(triangleFanPointCollection.Point(1), _missing, _missing)

        'Add TriangleFan To MultiPatch

        multiPatchGeometryCollection.AddGeometry(TryCast(triangleFanPointCollection, IGeometry), _missing, _missing)

        Return TryCast(multiPatchGeometryCollection, IGeometry)
    End Function

    Public Shared Function GetExample2() As IGeometry
        Const ConeBaseDegrees As Double = 360.0
        Const ConeBaseDivisions As Integer = 36
        Const VectorComponentOffset As Double = 0.0000001
        Const ConeBaseRadius As Double = 6
        Const ConeBaseZ As Double = 0.0
        Const ConeApexZ As Double = 9.5

        'Vector3D: Cone, TriangleFan With 36 Vertices

        Dim multiPatchGeometryCollection As IGeometryCollection = New MultiPatchClass()

        Dim triangleFanPointCollection As IPointCollection = New TriangleFanClass()

        'Set Cone Apex To (0, 0, ConeApexZ)

        Dim coneApexPoint As IPoint = GeometryUtilities.ConstructPoint3D(0, 0, ConeApexZ)

        'Add Cone Apex To Triangle Fan

        triangleFanPointCollection.AddPoint(coneApexPoint, _missing, _missing)

        'Define Upper Portion Of Axis Around Which Vector Should Be Rotated To Generate Cone Base Vertices

        Dim upperAxisVector3D As IVector3D = GeometryUtilities.ConstructVector3D(0, 0, 10)

        'Define Lower Portion of Axis Around Which Vector Should Be Rotated To Generate Cone Base Vertices

        Dim lowerAxisVector3D As IVector3D = GeometryUtilities.ConstructVector3D(0, 0, -10)

        'Add A Slight Offset To X or Y Component Of One Of Axis Vectors So Cross Product Does Not Return A Zero-Length Vector

        lowerAxisVector3D.XComponent += VectorComponentOffset

        'Obtain Cross Product Of Upper And Lower Axis Vectors To Obtain Normal Vector To Axis Of Rotation To Generate Cone Base Vertices

        Dim normalVector3D As IVector3D = TryCast(upperAxisVector3D.CrossProduct(lowerAxisVector3D), IVector3D)

        'Set Normal Vector Magnitude Equal To Radius Of Cone Base

        normalVector3D.Magnitude = ConeBaseRadius

        'Obtain Angle Of Rotation In Radians As Function Of Number Of Divisions Within 360 Degree Sweep Of Cone Base

        Dim rotationAngleInRadians As Double = GeometryUtilities.GetRadians(ConeBaseDegrees / ConeBaseDivisions)

        For i As Integer = 0 To ConeBaseDivisions - 1
            'Rotate Normal Vector Specified Rotation Angle In Radians Around Either Upper Or Lower Axis

            normalVector3D.Rotate(-1 * rotationAngleInRadians, upperAxisVector3D)

            'Construct Cone Base Vertex Whose XY Coordinates Are The Sum Of Apex XY Coordinates And Normal Vector XY Components

            Dim vertexPoint As IPoint = GeometryUtilities.ConstructPoint3D(coneApexPoint.X + normalVector3D.XComponent, coneApexPoint.Y + normalVector3D.YComponent, ConeBaseZ)

            'Add Vertex To TriangleFan

            triangleFanPointCollection.AddPoint(vertexPoint, _missing, _missing)
        Next i

        'Re-Add The Second Point Of The Triangle Fan (First Vertex Added) To Close The Fan

        triangleFanPointCollection.AddPoint(triangleFanPointCollection.Point(1), _missing, _missing)

        'Add TriangleFan To MultiPatch

        multiPatchGeometryCollection.AddGeometry(TryCast(triangleFanPointCollection, IGeometry), _missing, _missing)

        Return TryCast(multiPatchGeometryCollection, IGeometry)
    End Function

    Public Shared Function GetExample3() As IGeometry
        Const CylinderBaseDegrees As Double = 360.0
        Const CylinderBaseDivisions As Integer = 36
        Const VectorComponentOffset As Double = 0.0000001
        Const CylinderBaseRadius As Double = 3
        Const CylinderUpperZ As Double = 8
        Const CylinderLowerZ As Double = 0

        'Vector3D: Cylinder, TriangleStrip With 36 Vertices

        Dim multiPatchGeometryCollection As IGeometryCollection = New MultiPatchClass()

        Dim triangleStripPointCollection As IPointCollection = New TriangleStripClass()

        'Set Cylinder Base Origin To (0, 0, 0)

        Dim originPoint As IPoint = GeometryUtilities.ConstructPoint3D(0, 0, 0)

        'Define Upper Portion Of Axis Around Which Vector Should Be Rotated To Generate Cylinder Base Vertices

        Dim upperAxisVector3D As IVector3D = GeometryUtilities.ConstructVector3D(0, 0, 10)

        'Define Lower Portion of Axis Around Which Vector Should Be Rotated To Generate Cylinder Base Vertices

        Dim lowerAxisVector3D As IVector3D = GeometryUtilities.ConstructVector3D(0, 0, -10)

        'Add A Slight Offset To X or Y Component Of One Of Axis Vectors So Cross Product Does Not Return A Zero-Length Vector

        lowerAxisVector3D.XComponent += VectorComponentOffset

        'Obtain Cross Product Of Upper And Lower Axis Vectors To Obtain Normal Vector To Axis Of Rotation To Generate Cylinder Base Vertices

        Dim normalVector3D As IVector3D = TryCast(upperAxisVector3D.CrossProduct(lowerAxisVector3D), IVector3D)

        'Set Normal Vector Magnitude Equal To Radius Of Cylinder Base

        normalVector3D.Magnitude = CylinderBaseRadius

        'Obtain Angle Of Rotation In Radians As Function Of Number Of Divisions Within 360 Degree Sweep Of Cylinder Base

        Dim rotationAngleInRadians As Double = GeometryUtilities.GetRadians(CylinderBaseDegrees / CylinderBaseDivisions)

        For i As Integer = 0 To CylinderBaseDivisions - 1
            'Rotate Normal Vector Specified Rotation Angle In Radians Around Either Upper Or Lower Axis

            normalVector3D.Rotate(rotationAngleInRadians, upperAxisVector3D)

            'Construct Cylinder Base Vertex Whose XY Coordinates Are The Sum Of Origin XY Coordinates And Normal Vector XY Components

            Dim vertexPoint As IPoint = GeometryUtilities.ConstructPoint3D(originPoint.X + normalVector3D.XComponent, originPoint.Y + normalVector3D.YComponent, 0)

            'Construct Lower Base Vertex From This Point And Add To TriangleStrip

            Dim lowerVertexPoint As IPoint = GeometryUtilities.ConstructPoint3D(vertexPoint.X, vertexPoint.Y, CylinderLowerZ)

            triangleStripPointCollection.AddPoint(lowerVertexPoint, _missing, _missing)

            'Construct Upper Base Vertex From This Point And Add To TriangleStrip

            Dim upperVertexPoint As IPoint = GeometryUtilities.ConstructPoint3D(vertexPoint.X, vertexPoint.Y, CylinderUpperZ)

            triangleStripPointCollection.AddPoint(upperVertexPoint, _missing, _missing)
        Next i

        'Re-Add The First And Second Points Of The Triangle Strip (First Two Vertices Added) To Close The Strip

        triangleStripPointCollection.AddPoint(triangleStripPointCollection.Point(0), _missing, _missing)

        triangleStripPointCollection.AddPoint(triangleStripPointCollection.Point(1), _missing, _missing)

        'Add TriangleStrip To MultiPatch

        multiPatchGeometryCollection.AddGeometry(TryCast(triangleStripPointCollection, IGeometry), _missing, _missing)

        Return TryCast(multiPatchGeometryCollection, IGeometry)
    End Function

    Public Shared Function GetExample4() As IGeometry
        Const ConeBaseDegrees As Double = 360.0
        Const ConeBaseDivisions As Integer = 8
        Const VectorComponentOffset As Double = 0.0000001
        Const ConeBaseRadius As Double = 6
        Const ConeBaseZ As Double = 0.0
        Const ConeApexZ As Double = 9.5

        'Vector3D: Cone, TriangleFan With 8 Vertices

        Dim multiPatchGeometryCollection As IGeometryCollection = New MultiPatchClass()

        Dim triangleFanPointCollection As IPointCollection = New TriangleFanClass()

        'Set Cone Apex To (0, 0, ConeApexZ)

        Dim coneApexPoint As IPoint = GeometryUtilities.ConstructPoint3D(0, 0, ConeApexZ)

        'Add Cone Apex To Triangle Fan

        triangleFanPointCollection.AddPoint(coneApexPoint, _missing, _missing)

        'Define Upper Portion Of Axis Around Which Vector Should Be Rotated To Generate Cone Base Vertices

        Dim upperAxisVector3D As IVector3D = GeometryUtilities.ConstructVector3D(0, 0, 10)

        'Define Lower Portion of Axis Around Which Vector Should Be Rotated To Generate Cone Base Vertices

        Dim lowerAxisVector3D As IVector3D = GeometryUtilities.ConstructVector3D(0, 0, -10)

        'Add A Slight Offset To X or Y Component Of One Of Axis Vectors So Cross Product Does Not Return A Zero-Length Vector

        lowerAxisVector3D.XComponent += VectorComponentOffset

        'Obtain Cross Product Of Upper And Lower Axis Vectors To Obtain Normal Vector To Axis Of Rotation To Generate Cone Base Vertices

        Dim normalVector3D As IVector3D = TryCast(upperAxisVector3D.CrossProduct(lowerAxisVector3D), IVector3D)

        'Set Normal Vector Magnitude Equal To Radius Of Cone Base

        normalVector3D.Magnitude = ConeBaseRadius

        'Obtain Angle Of Rotation In Radians As Function Of Number Of Divisions Within 360 Degree Sweep Of Cone Base

        Dim rotationAngleInRadians As Double = GeometryUtilities.GetRadians(ConeBaseDegrees / ConeBaseDivisions)

        For i As Integer = 0 To ConeBaseDivisions - 1
            'Rotate Normal Vector Specified Rotation Angle In Radians Around Either Upper Or Lower Axis

            normalVector3D.Rotate(-1 * rotationAngleInRadians, upperAxisVector3D)

            'Construct Cone Base Vertex Whose XY Coordinates Are The Sum Of Apex XY Coordinates And Normal Vector XY Components

            Dim vertexPoint As IPoint = GeometryUtilities.ConstructPoint3D(coneApexPoint.X + normalVector3D.XComponent, coneApexPoint.Y + normalVector3D.YComponent, ConeBaseZ)

            'Add Vertex To TriangleFan

            triangleFanPointCollection.AddPoint(vertexPoint, _missing, _missing)
        Next i

        'Re-Add The Second Point Of The Triangle Fan (First Vertex Added) To Close The Fan

        triangleFanPointCollection.AddPoint(triangleFanPointCollection.Point(1), _missing, _missing)

        'Add TriangleFan To MultiPatch

        multiPatchGeometryCollection.AddGeometry(TryCast(triangleFanPointCollection, IGeometry), _missing, _missing)

        Return TryCast(multiPatchGeometryCollection, IGeometry)
    End Function

    Public Shared Function GetExample5() As IGeometry
        Const CylinderBaseDegrees As Double = 360.0
        Const CylinderBaseDivisions As Integer = 8
        Const VectorComponentOffset As Double = 0.0000001
        Const CylinderBaseRadius As Double = 3
        Const CylinderUpperZ As Double = 8
        Const CylinderLowerZ As Double = 0

        'Vector3D: Cylinder, TriangleStrip With 8 Vertices

        Dim multiPatchGeometryCollection As IGeometryCollection = New MultiPatchClass()

        Dim triangleStripPointCollection As IPointCollection = New TriangleStripClass()

        'Set Cylinder Base Origin To (0, 0, 0)

        Dim originPoint As IPoint = GeometryUtilities.ConstructPoint3D(0, 0, 0)

        'Define Upper Portion Of Axis Around Which Vector Should Be Rotated To Generate Cylinder Base Vertices

        Dim upperAxisVector3D As IVector3D = GeometryUtilities.ConstructVector3D(0, 0, 10)

        'Define Lower Portion of Axis Around Which Vector Should Be Rotated To Generate Cylinder Base Vertices

        Dim lowerAxisVector3D As IVector3D = GeometryUtilities.ConstructVector3D(0, 0, -10)

        'Add A Slight Offset To X or Y Component Of One Of Axis Vectors So Cross Product Does Not Return A Zero-Length Vector

        lowerAxisVector3D.XComponent += VectorComponentOffset

        'Obtain Cross Product Of Upper And Lower Axis Vectors To Obtain Normal Vector To Axis Of Rotation To Generate Cylinder Base Vertices

        Dim normalVector3D As IVector3D = TryCast(upperAxisVector3D.CrossProduct(lowerAxisVector3D), IVector3D)

        'Set Normal Vector Magnitude Equal To Radius Of Cylinder Base

        normalVector3D.Magnitude = CylinderBaseRadius

        'Obtain Angle Of Rotation In Radians As Function Of Number Of Divisions Within 360 Degree Sweep Of Cylinder Base

        Dim rotationAngleInRadians As Double = GeometryUtilities.GetRadians(CylinderBaseDegrees / CylinderBaseDivisions)

        For i As Integer = 0 To CylinderBaseDivisions - 1
            'Rotate Normal Vector Specified Rotation Angle In Radians Around Either Upper Or Lower Axis

            normalVector3D.Rotate(rotationAngleInRadians, upperAxisVector3D)

            'Construct Cylinder Base Vertex Whose XY Coordinates Are The Sum Of Origin XY Coordinates And Normal Vector XY Components

            Dim vertexPoint As IPoint = GeometryUtilities.ConstructPoint3D(originPoint.X + normalVector3D.XComponent, originPoint.Y + normalVector3D.YComponent, 0)

            'Construct Lower Base Vertex From This Point And Add To TriangleStrip

            Dim lowerVertexPoint As IPoint = GeometryUtilities.ConstructPoint3D(vertexPoint.X, vertexPoint.Y, CylinderLowerZ)

            triangleStripPointCollection.AddPoint(lowerVertexPoint, _missing, _missing)

            'Construct Upper Base Vertex From This Point And Add To TriangleStrip

            Dim upperVertexPoint As IPoint = GeometryUtilities.ConstructPoint3D(vertexPoint.X, vertexPoint.Y, CylinderUpperZ)

            triangleStripPointCollection.AddPoint(upperVertexPoint, _missing, _missing)
        Next i

        'Re-Add The First And Second Points Of The Triangle Strip (First Two Vertices Added) To Close The Strip

        triangleStripPointCollection.AddPoint(triangleStripPointCollection.Point(0), _missing, _missing)

        triangleStripPointCollection.AddPoint(triangleStripPointCollection.Point(1), _missing, _missing)

        'Add TriangleStrip To MultiPatch

        multiPatchGeometryCollection.AddGeometry(TryCast(triangleStripPointCollection, IGeometry), _missing, _missing)

        Return TryCast(multiPatchGeometryCollection, IGeometry)
    End Function
End Class
