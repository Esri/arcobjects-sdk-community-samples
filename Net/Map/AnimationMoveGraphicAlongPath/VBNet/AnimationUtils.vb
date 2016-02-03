Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Windows.Forms
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.Animation
Imports ESRI.ArcGIS.ADF
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Controls
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.SystemUI
Imports ESRI.ArcGIS.Geometry

Public Class AnimationUtils
    Public Shared Sub CreateMapGraphicTrack(ByVal pOptions As ICreateGraphicTrackOptions, ByVal tracks As IAGAnimationTracks, ByVal pContainer As IAGAnimationContainer)
        pOptions.PathGeometry = SimplifyPath2D(pOptions.PathGeometry, pOptions.ReverseOrder, pOptions.SimplificationFactor)
        Dim animType As IAGAnimationType = New AnimationTypeMapGraphic()

        Dim i As Integer = 0
        'remove tracks with the same name if overwrite is true
        If pOptions.OverwriteTrack = True Then
            Dim trackArray As IArray = New ArrayClass()
            trackArray = tracks.TracksOfType(animType)
            Dim count As Integer = trackArray.Count
            Do While i < count
                Dim temp As IAGAnimationTrack = CType(trackArray.Element(i), IAGAnimationTrack)
                If temp.Name = pOptions.TrackName Then
                    tracks.RemoveTrack(temp)
                End If
                i += 1
            Loop
        End If

        'create the new track
        Dim animTrack As IAGAnimationTrack = tracks.CreateTrack(animType)
        Dim animTrackKeyframes As IAGAnimationTrackKeyframes = CType(animTrack, IAGAnimationTrackKeyframes)
        animTrackKeyframes.EvenTimeStamps = False

        animTrack.Name = pOptions.TrackName

        Dim path As IGeometry = pOptions.PathGeometry
        Dim pointCollection As IPointCollection = CType(path, IPointCollection)

        Dim curve As ICurve = CType(path, ICurve)
        Dim length As Double = curve.Length
        Dim accuLength As Double = 0

        'loop through all points to create the keyframes
        Dim pointCount As Integer = pointCollection.PointCount
        If pointCount <= 1 Then
            Return
        End If
        i = 0
        Do While i < pointCount
            Dim currentPoint As IPoint = pointCollection.Point(i)

            Dim nextPoint As IPoint = New PointClass()
            If i < pointCount - 1 Then
                nextPoint = pointCollection.Point(i + 1)
            End If

            Dim lastPoint As IPoint = New PointClass()
            If i = 0 Then
                lastPoint = currentPoint
            Else
                lastPoint = pointCollection.Point(i - 1)
            End If

            Dim tempKeyframe As IAGKeyframe = animTrackKeyframes.CreateKeyframe(i)

            'set keyframe properties
            Dim x As Double
            Dim y As Double
            currentPoint.QueryCoords(x, y)
            tempKeyframe.PropertyValue(0) = currentPoint
            tempKeyframe.Name = "Map Graphic keyframe " & i.ToString()

            'set keyframe timestamp
            accuLength += CalculateDistance(lastPoint, currentPoint)
            Dim timeStamp As Double = accuLength / length
            tempKeyframe.TimeStamp = timeStamp

            Dim x1 As Double
            Dim y1 As Double
            Dim angle As Double
            If i < pointCount - 1 Then
                nextPoint.QueryCoords(x1, y1)
                If (y1 - y) > 0 Then
                    angle = Math.Acos((x1 - x) / Math.Sqrt((x1 - x) * (x1 - x) + (y1 - y) * (y1 - y)))
                Else
                    angle = 6.2832 - Math.Acos((x1 - x) / Math.Sqrt((x1 - x) * (x1 - x) + (y1 - y) * (y1 - y)))
                End If
                tempKeyframe.PropertyValue(1) = angle
            Else
                Dim lastKeyframe As IAGKeyframe = animTrackKeyframes.Keyframe(i - 1)
                tempKeyframe.PropertyValue(1) = lastKeyframe.PropertyValue(1)
            End If
            i += 1
        Loop

        'attach the point element
        If Not pOptions.PointElement Is Nothing Then
            animTrack.AttachObject(pOptions.PointElement)
        End If

        'attach the track extension, which contains a line element for trace
        Dim graphicTrackExtension As IMapGraphicTrackExtension = New MapGraphicTrackExtension()
        graphicTrackExtension.ShowTrace = pOptions.AnimatePath
        Dim trackExtensions As IAGAnimationTrackExtensions = CType(animTrack, IAGAnimationTrackExtensions)
        trackExtensions.AddExtension(graphicTrackExtension)
    End Sub

    Private Shared Function CalculateDistance(ByVal FromPoint As IPoint, ByVal ToPoint As IPoint) As Double
        Dim distance As Double
        distance = Math.Sqrt((ToPoint.X - FromPoint.X) * (ToPoint.X - FromPoint.X) + (ToPoint.Y - FromPoint.Y) * (ToPoint.Y - FromPoint.Y))
        Return distance
    End Function

    Private Shared Function SimplifyPath2D(ByVal path As IGeometry, ByVal bReverse As Boolean, ByVal simpFactor As Double) As IGeometry
        Dim oldPath As IGeometry = path
        Dim oldPointCollection As IPointCollection = TryCast(oldPath, IPointCollection)
        Dim newPath As IPolyline = New PolylineClass()
        Dim newPointCollection As IPointCollection = TryCast(newPath, IPointCollection)
        Dim sr As ISpatialReference = oldPath.SpatialReference

        Dim pointCount As Integer
        pointCount = oldPointCollection.PointCount
        Dim lastCoord As Double() = New Double(1) {}

        Dim beginningPoint As IPoint = New PointClass()
        If bReverse Then
            oldPointCollection.QueryPoint(pointCount - 1, beginningPoint)
        Else
            oldPointCollection.QueryPoint(0, beginningPoint)
        End If
        beginningPoint.QueryCoords(lastCoord(0), lastCoord(1))

        Dim bKeep As Boolean = True

        Dim oldLine As IPolyline = TryCast(oldPath, IPolyline)
        Dim length As Double = oldLine.Length

        Dim Missing As Object = Type.Missing
        newPointCollection.AddPoint(beginningPoint, Missing, Missing)

        Dim i As Integer = 1
        Do While i < pointCount - 1 'simplify 2D path
            Dim coord As Double() = New Double(1) {}
            Dim currentPoint As IPoint = New PointClass()
            If bReverse Then
                oldPointCollection.QueryPoint(pointCount - i - 1, currentPoint)
            Else
                oldPointCollection.QueryPoint(i, currentPoint)
            End If
            currentPoint.QueryCoords(coord(0), coord(1))

            Dim d As Double() = New Double(1) {}
            d(0) = coord(0) - lastCoord(0)
            d(1) = coord(1) - lastCoord(1)

            Dim distance As Double
            distance = Math.Sqrt(d(0) * d(0) + d(1) * d(1))

            If distance < (0.25 * simpFactor * length) Then
                bKeep = False
            Else
                bKeep = True
            End If

            If bKeep Then
                newPointCollection.AddPoint(currentPoint, Missing, Missing)
                lastCoord(0) = coord(0)
                lastCoord(1) = coord(1)
            End If
            i += 1
        Loop

        Dim finalPoint As IPoint = New PointClass()
        If bReverse Then
            oldPointCollection.QueryPoint(0, finalPoint)
        Else
            oldPointCollection.QueryPoint(pointCount - 1, finalPoint)
        End If
        newPointCollection.AddPoint(finalPoint, Missing, Missing)

        newPath.SpatialReference = sr
        Return CType(newPath, IGeometry)
    End Function
End Class

