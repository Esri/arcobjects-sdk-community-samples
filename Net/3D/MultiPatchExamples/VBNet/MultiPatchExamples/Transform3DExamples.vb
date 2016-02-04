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
Imports System
Imports ESRI.ArcGIS.Geometry


Public Class Transform3DExamples
    Private Shared _missing As Object = Type.Missing

    Private Sub New()
    End Sub
    Public Shared Function GetExample1() As IGeometry
        Const XOffset As Double = 7.5
        Const YOffset As Double = 7.5
        Const ZOffset As Double = -10

        'Transform3D: Cylinder Repositioned Via Move3D()

        Dim geometry As IGeometry = Vector3DExamples.GetExample3()

        Dim transform3D As ITransform3D = TryCast(geometry, ITransform3D)
        transform3D.Move3D(XOffset, YOffset, ZOffset)

        Return geometry
    End Function

    Public Shared Function GetExample2() As IGeometry
        Const XScale As Double = 2
        Const YScale As Double = 2
        Const ZScale As Double = 3

        'Transform3D: Cylinder Scaled Via Scale3D()

        Dim geometry As IGeometry = Vector3DExamples.GetExample3()

        'Define Origin At Which Scale Operation Should Be Performed

        Dim originPoint As IPoint = GeometryUtilities.ConstructPoint3D(0, 0, 0)

        Dim transform3D As ITransform3D = TryCast(geometry, ITransform3D)
        transform3D.Scale3D(originPoint, XScale, YScale, ZScale)

        Return geometry
    End Function

    Public Shared Function GetExample3() As IGeometry
        Const DegreesOfRotation As Double = 45

        'Transform3D: Cylinder Rotated Around An Axis Via RotateVector3D()

        Dim geometry As IGeometry = Vector3DExamples.GetExample3()

        'Construct A Vector3D Corresponding To The Desired Axis Of Rotation

        Dim axisOfRotationVector3D As IVector3D = GeometryUtilities.ConstructVector3D(0, 10, 0)

        'Obtain Angle Of Rotation In Radians

        Dim angleOfRotationInRadians As Double = GeometryUtilities.GetRadians(DegreesOfRotation)

        Dim transform3D As ITransform3D = TryCast(geometry, ITransform3D)
        transform3D.RotateVector3D(axisOfRotationVector3D, angleOfRotationInRadians)

        Return geometry
    End Function

    Public Shared Function GetExample4() As IGeometry
        Const XScale As Double = 0.5
        Const YScale As Double = 0.5
        Const ZScale As Double = 2
        Const XOffset As Double = -5
        Const YOffset As Double = -5
        Const ZOffset As Double = -8
        Const DegreesOfRotation As Double = 90

        'Transform3D: Cylinder Scaled, Rotated, Repositioned Via Move3D(), Scale3D(), RotateVector3D()

        Dim geometry As IGeometry = Vector3DExamples.GetExample3()

        Dim transform3D As ITransform3D = TryCast(geometry, ITransform3D)

        'Stretch The Cylinder So It Looks Like A Tube

        Dim originPoint As IPoint = GeometryUtilities.ConstructPoint3D(0, 0, 0)

        transform3D.Scale3D(originPoint, XScale, YScale, ZScale)

        'Rotate The Cylinder So It Lies On Its Side

        Dim axisOfRotationVector3D As IVector3D = GeometryUtilities.ConstructVector3D(0, 10, 0)

        Dim angleOfRotationInRadians As Double = GeometryUtilities.GetRadians(DegreesOfRotation)

        transform3D.RotateVector3D(axisOfRotationVector3D, angleOfRotationInRadians)

        'Reposition The Cylinder So It Is Located Underground

        transform3D.Move3D(XOffset, YOffset, ZOffset)

        Return geometry
    End Function
End Class
