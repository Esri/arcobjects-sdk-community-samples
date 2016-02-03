Imports Microsoft.VisualBasic
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.Display
Imports ESRI.ArcGIS.Analyst3D
Imports System


Public Class DrawUtilities
    Private Shared _missing As Object = Type.Missing

    Private Sub New()
    End Sub
    Public Shared Sub DrawAxes(ByVal axesGraphicsContainer3D As IGraphicsContainer3D)
        Const AxisStyle As esriSimple3DLineStyle = esriSimple3DLineStyle.esriS3DLSTube
        Const AxisWidth As Double = 0.25

        DrawAxis(axesGraphicsContainer3D, GeometryUtilities.ConstructPoint3D(-10, 0, 0), GeometryUtilities.ConstructPoint3D(10, 0, 0), ColorUtilities.GetColor(255, 0, 0), AxisStyle, AxisWidth)
        DrawAxis(axesGraphicsContainer3D, GeometryUtilities.ConstructPoint3D(0, -10, 0), GeometryUtilities.ConstructPoint3D(0, 10, 0), ColorUtilities.GetColor(0, 0, 255), AxisStyle, AxisWidth)
        DrawAxis(axesGraphicsContainer3D, GeometryUtilities.ConstructPoint3D(0, 0, -10), GeometryUtilities.ConstructPoint3D(0, 0, 10), ColorUtilities.GetColor(0, 255, 0), AxisStyle, AxisWidth)

        DrawEnd(axesGraphicsContainer3D, GeometryUtilities.ConstructPoint3D(10, 0, 0), GeometryUtilities.ConstructVector3D(0, 10, 0), 90, ColorUtilities.GetColor(255, 0, 0), 0.2 * AxisWidth)
        DrawEnd(axesGraphicsContainer3D, GeometryUtilities.ConstructPoint3D(0, 10, 0), GeometryUtilities.ConstructVector3D(10, 0, 0), -90, ColorUtilities.GetColor(0, 0, 255), 0.2 * AxisWidth)
        DrawEnd(axesGraphicsContainer3D, GeometryUtilities.ConstructPoint3D(0, 0, 10), Nothing, 0, ColorUtilities.GetColor(0, 255, 0), 0.2 * AxisWidth)
    End Sub

    Private Shared Sub DrawAxis(ByVal axesGraphicsContainer3D As IGraphicsContainer3D, ByVal axisFromPoint As IPoint, ByVal axisToPoint As IPoint, ByVal axisColor As IColor, ByVal axisStyle As esriSimple3DLineStyle, ByVal axisWidth As Double)
        Dim axisPointCollection As IPointCollection = New PolylineClass()

        axisPointCollection.AddPoint(axisFromPoint, _missing, _missing)
        axisPointCollection.AddPoint(axisToPoint, _missing, _missing)

        GeometryUtilities.MakeZAware(TryCast(axisPointCollection, IGeometry))

        GraphicsLayer3DUtilities.AddAxisToGraphicsLayer3D(axesGraphicsContainer3D, TryCast(axisPointCollection, IGeometry), axisColor, axisStyle, axisWidth)
    End Sub

    Private Shared Sub DrawEnd(ByVal endGraphicsContainer3D As IGraphicsContainer3D, ByVal endPoint As IPoint, ByVal axisOfRotationVector3D As IVector3D, ByVal degreesOfRotation As Double, ByVal endColor As IColor, ByVal endRadius As Double)
        Dim endGeometry As IGeometry = Vector3DExamples.GetExample2()

        Dim transform3D As ITransform3D = TryCast(endGeometry, ITransform3D)

        Dim originPoint As IPoint = GeometryUtilities.ConstructPoint3D(0, 0, 0)

        transform3D.Scale3D(originPoint, endRadius, endRadius, 2 * endRadius)

        If degreesOfRotation <> 0 Then
            Dim angleOfRotationInRadians As Double = GeometryUtilities.GetRadians(degreesOfRotation)

            transform3D.RotateVector3D(axisOfRotationVector3D, angleOfRotationInRadians)
        End If

        transform3D.Move3D(endPoint.X - originPoint.X, endPoint.Y - originPoint.Y, endPoint.Z - originPoint.Z)

        GraphicsLayer3DUtilities.AddMultiPatchToGraphicsLayer3D(endGraphicsContainer3D, endGeometry, endColor)
    End Sub

    Public Shared Sub DrawMultiPatch(ByVal multiPatchGraphicsContainer3D As IGraphicsContainer3D, ByVal geometry As IGeometry)
        Const Yellow_R As Integer = 255
        Const Yellow_G As Integer = 255
        Const Yellow_B As Integer = 0

        Dim multiPatchColor As IColor = ColorUtilities.GetColor(Yellow_R, Yellow_G, Yellow_B)

        multiPatchGraphicsContainer3D.DeleteAllElements()

        GraphicsLayer3DUtilities.AddMultiPatchToGraphicsLayer3D(multiPatchGraphicsContainer3D, geometry, multiPatchColor)
    End Sub

    Public Shared Sub DrawOutline(ByVal outlineGraphicsContainer3D As IGraphicsContainer3D, ByVal geometry As IGeometry)
        Const OutlineStyle As esriSimple3DLineStyle = esriSimple3DLineStyle.esriS3DLSTube
        Const OutlineWidth As Double = 0.1

        Const Black_R As Integer = 0
        Const Black_G As Integer = 0
        Const Black_B As Integer = 0

        Dim outlineColor As IColor = ColorUtilities.GetColor(Black_R, Black_G, Black_B)

        outlineGraphicsContainer3D.DeleteAllElements()

        GraphicsLayer3DUtilities.AddOutlineToGraphicsLayer3D(outlineGraphicsContainer3D, GeometryUtilities.ConstructMultiPatchOutline(geometry), outlineColor, OutlineStyle, OutlineWidth)
    End Sub
End Class
