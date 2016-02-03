Imports Microsoft.VisualBasic
Imports ESRI.ArcGIS.Geometry
Imports System


Public Class GeometryUtilities
    Private Shared _missing As Object = Type.Missing

    Private Sub New()
    End Sub
    Public Shared Sub MakeZAware(ByVal geometry As IGeometry)
        Dim zAware As IZAware = TryCast(geometry, IZAware)
        zAware.ZAware = True
    End Sub

    Public Shared Function ConstructVector3D(ByVal xComponent As Double, ByVal yComponent As Double, ByVal zComponent As Double) As IVector3D
        Dim vector3D As IVector3D = New Vector3DClass()
        vector3D.SetComponents(xComponent, yComponent, zComponent)

        Return vector3D
    End Function

    Public Shared Function GetRadians(ByVal decimalDegrees As Double) As Double
        Return decimalDegrees * (Math.PI / 180)
    End Function

    Public Shared Function ConstructPoint3D(ByVal x As Double, ByVal y As Double, ByVal z As Double) As IPoint
        Dim point As IPoint = ConstructPoint2D(x, y)
        point.Z = z

        MakeZAware(TryCast(point, IGeometry))

        Return point
    End Function

    Public Shared Function ConstructPoint2D(ByVal x As Double, ByVal y As Double) As IPoint
        Dim point As IPoint = New PointClass()
        point.X = x
        point.Y = y

        Return point
    End Function

    Public Shared Function ConstructMultiPatchOutline(ByVal multiPatchGeometry As IGeometry) As IGeometryCollection
        Dim outlineGeometryCollection As IGeometryCollection = New GeometryBagClass()

        Dim multiPatchGeometryCollection As IGeometryCollection = TryCast(multiPatchGeometry, IGeometryCollection)

        Dim i As Integer = 0
        Do While i < multiPatchGeometryCollection.GeometryCount
            Dim geometry As IGeometry = multiPatchGeometryCollection.Geometry(i)

            Select Case geometry.GeometryType
                Case (esriGeometryType.esriGeometryTriangleStrip)
                    outlineGeometryCollection.AddGeometryCollection(ConstructTriangleStripOutline(geometry))

                Case (esriGeometryType.esriGeometryTriangleFan)
                    outlineGeometryCollection.AddGeometryCollection(ConstructTriangleFanOutline(geometry))

                Case (esriGeometryType.esriGeometryTriangles)
                    outlineGeometryCollection.AddGeometryCollection(ConstructTrianglesOutline(geometry))

                Case (esriGeometryType.esriGeometryRing)
                    outlineGeometryCollection.AddGeometry(ConstructRingOutline(geometry), _missing, _missing)

                Case Else
                    Throw New Exception("Unhandled Geometry Type. " & geometry.GeometryType)
            End Select
            i += 1
        Loop

        Return outlineGeometryCollection
    End Function

    Public Shared Function ConstructTriangleStripOutline(ByVal triangleStripGeometry As IGeometry) As IGeometryCollection
        Dim outlineGeometryCollection As IGeometryCollection = New GeometryBagClass()

        Dim triangleStripPointCollection As IPointCollection = TryCast(triangleStripGeometry, IPointCollection)

        ' TriangleStrip: a linked strip of triangles, where every vertex (after the first two) completes a new triangle.
        '                A new triangle is always formed by connecting the new vertex with its two immediate predecessors.

        Dim i As Integer = 2
        Do While i < triangleStripPointCollection.PointCount
            Dim outlinePointCollection As IPointCollection = New PolylineClass()

            outlinePointCollection.AddPoint(triangleStripPointCollection.Point(i - 2), _missing, _missing)
            outlinePointCollection.AddPoint(triangleStripPointCollection.Point(i - 1), _missing, _missing)
            outlinePointCollection.AddPoint(triangleStripPointCollection.Point(i), _missing, _missing)
            outlinePointCollection.AddPoint(triangleStripPointCollection.Point(i - 2), _missing, _missing) 'Simulate: Polygon.Close

            Dim outlineGeometry As IGeometry = TryCast(outlinePointCollection, IGeometry)

            MakeZAware(outlineGeometry)

            outlineGeometryCollection.AddGeometry(outlineGeometry, _missing, _missing)
            i += 1
        Loop

        Return outlineGeometryCollection
    End Function

    Public Shared Function ConstructTriangleFanOutline(ByVal triangleFanGeometry As IGeometry) As IGeometryCollection
        Dim outlineGeometryCollection As IGeometryCollection = New GeometryBagClass()

        Dim triangleFanPointCollection As IPointCollection = TryCast(triangleFanGeometry, IPointCollection)

        ' TriangleFan: a linked fan of triangles, where every vertex (after the first two) completes a new triangle. 
        '              A new triangle is always formed by connecting the new vertex with its immediate predecessor 
        '              and the first vertex of the part.

        Dim i As Integer = 2
        Do While i < triangleFanPointCollection.PointCount
            Dim outlinePointCollection As IPointCollection = New PolylineClass()

            outlinePointCollection.AddPoint(triangleFanPointCollection.Point(0), _missing, _missing)
            outlinePointCollection.AddPoint(triangleFanPointCollection.Point(i - 1), _missing, _missing)
            outlinePointCollection.AddPoint(triangleFanPointCollection.Point(i), _missing, _missing)
            outlinePointCollection.AddPoint(triangleFanPointCollection.Point(0), _missing, _missing) 'Simulate: Polygon.Close

            Dim outlineGeometry As IGeometry = TryCast(outlinePointCollection, IGeometry)

            MakeZAware(outlineGeometry)

            outlineGeometryCollection.AddGeometry(outlineGeometry, _missing, _missing)
            i += 1
        Loop

        Return outlineGeometryCollection
    End Function

    Public Shared Function ConstructTrianglesOutline(ByVal trianglesGeometry As IGeometry) As IGeometryCollection
        Dim outlineGeometryCollection As IGeometryCollection = New GeometryBagClass()

        Dim trianglesPointCollection As IPointCollection = TryCast(trianglesGeometry, IPointCollection)

        ' Triangles: an unlinked set of triangles, where every three vertices completes a new triangle.

        If (trianglesPointCollection.PointCount Mod 3) <> 0 Then
            Throw New Exception("Triangles Geometry Point Count Must Be Divisible By 3. " & trianglesPointCollection.PointCount)
        Else
            Dim i As Integer = 0
            Do While i < trianglesPointCollection.PointCount
                Dim outlinePointCollection As IPointCollection = New PolylineClass()

                outlinePointCollection.AddPoint(trianglesPointCollection.Point(i), _missing, _missing)
                outlinePointCollection.AddPoint(trianglesPointCollection.Point(i + 1), _missing, _missing)
                outlinePointCollection.AddPoint(trianglesPointCollection.Point(i + 2), _missing, _missing)
                outlinePointCollection.AddPoint(trianglesPointCollection.Point(i), _missing, _missing) 'Simulate: Polygon.Close

                Dim outlineGeometry As IGeometry = TryCast(outlinePointCollection, IGeometry)

                MakeZAware(outlineGeometry)

                outlineGeometryCollection.AddGeometry(outlineGeometry, _missing, _missing)
                i += 3
            Loop
        End If

        Return outlineGeometryCollection
    End Function

    Public Shared Function ConstructRingOutline(ByVal ringGeometry As IGeometry) As IGeometry
        Dim outlineGeometry As IGeometry = New PolylineClass()

        Dim outlinePointCollection As IPointCollection = TryCast(outlineGeometry, IPointCollection)

        Dim ringPointCollection As IPointCollection = TryCast(ringGeometry, IPointCollection)

        Dim i As Integer = 0
        Do While i < ringPointCollection.PointCount
            outlinePointCollection.AddPoint(ringPointCollection.Point(i), _missing, _missing)
            i += 1
        Loop

        outlinePointCollection.AddPoint(ringPointCollection.Point(0), _missing, _missing) 'Simulate: Polygon.Close

        MakeZAware(outlineGeometry)

        Return outlineGeometry
    End Function
End Class
