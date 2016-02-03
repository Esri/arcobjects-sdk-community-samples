Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Text
Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Windows.Forms
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.GlobeCore
Imports ESRI.ArcGIS.Animation
Imports ESRI.ArcGIS.Analyst3D
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.Display
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Controls
Imports ESRI.ArcGIS.DataSourcesGDB

Namespace VisualizeCameraPath
  Public Class VisualizeCameraPath : Inherits ESRI.ArcGIS.Desktop.AddIns.Button

#Region "Member Variables"

    Private globe As ESRI.ArcGIS.GlobeCore.IGlobe
    Private globeDisplay As ESRI.ArcGIS.GlobeCore.IGlobeDisplay
    Private graphicsLayer As ESRI.ArcGIS.Carto.IGraphicsContainer
    Private globeCamera As ESRI.ArcGIS.GlobeCore.IGlobeCamera
    Private animationTracks As ESRI.ArcGIS.Animation.IAGAnimationTracks
    Private animationTrack As ESRI.ArcGIS.Animation.IAGAnimationTrack
    Private theCamForm As VisualizeCameraPathForm
    Private animUtils As ESRI.ArcGIS.Animation.IAGAnimationUtils
    Private animEvent As ESRI.ArcGIS.Animation.IAnimationEvents_Event
    Private animEnv As ESRI.ArcGIS.Animation.IAGAnimationEnvironment
    Private animPlayer As ESRI.ArcGIS.Animation.IAGAnimationPlayer
    Private globeDispEvent As ESRI.ArcGIS.GlobeCore.IGlobeDisplayEvents_Event
    Private toolIsInitialized As Boolean = False
    Private animationDuration As Double = 0

#End Region

#Region "DLLImportFunction"

    <DllImport("gdi32.dll")> _
    Shared Function DeleteObject(ByVal hObject As IntPtr) As Boolean
    End Function

    <DllImport("user32.dll")> _
    Shared Function ShowWindow(ByVal hwnd As Integer, ByVal nCmdShow As Integer) As Integer
    End Function


#End Region

    Public Sub New()
      globe = ArcGlobe.Globe
      globeDisplay = globe.GlobeDisplay
      globeCamera = TryCast(globeDisplay.ActiveViewer.Camera, IGlobeCamera)
    End Sub


    Protected Overrides Sub Finalize()
      If Not theCamForm Is Nothing Then
        theCamForm.Dispose()
      End If
    End Sub

    Protected Overrides Sub OnClick()
      'The first time button is clicked
      If toolIsInitialized = False Then
        theCamForm = New VisualizeCameraPathForm()

        'Add event handlers for form's button click events
        AddHandler theCamForm.playButton.Click, AddressOf formPlayButtonClickEventHandler
        AddHandler theCamForm.stopButton.Click, AddressOf formStopButtonClickEventHandler
        AddHandler theCamForm.generatePathButton.Click, AddressOf formGeneratePathButtonClickEventHandler

        AddHandler theCamForm.generateCamPathCheckBox.CheckedChanged, AddressOf formCheckbox1CheckedChanged

        AddHandler theCamForm.Closing, AddressOf theCamForm_Closing

        animEnv = New AGAnimationEnvironmentClass()

        'True = Button has been already clicked
        toolIsInitialized = True
      'If the main form is already open - do not open another one
      ElseIf toolIsInitialized = True Then
        'Clear the list of animation tracks
        theCamForm.animTracksListBox.Items.Clear()
      End If
      'Get the list of animation tracks
      Me.getCameraAnimationTracksFromGlobe()
      theCamForm.Show()

    End Sub

    Protected Overrides Sub OnUpdate()
      Enabled = Not ArcGlobe.Application Is Nothing
    End Sub


#Region "Custom Functions and Event Handlers"

    'function for getting camera animation tracks
    Public Sub getCameraAnimationTracksFromGlobe()
      Dim animationType As ESRI.ArcGIS.Animation.IAGAnimationType = New AnimationTypeGlobeCameraClass()
            animationTracks = CType(globe, ESRI.ArcGIS.Animation.IAGAnimationTracks)
      Dim animCounter As Integer = 0
      Do While animCounter < animationTracks.AGTracks.Count
                animationTrack = CType(animationTracks.AGTracks.Element(animCounter), ESRI.ArcGIS.Animation.IAGAnimationTrack)
        If animationTrack.AnimationType Is animationType Then
          theCamForm.animTracksListBox.Items.Add(animationTrack.Name)
        End If
        animCounter = animCounter + 1
      Loop
    End Sub

    'function for enabling selected animation track
    Public Sub enableSelectedTrack()
      If Not theCamForm.animTracksListBox.SelectedItem Is Nothing Then
        Dim selectedTrack As String = theCamForm.animTracksListBox.SelectedItem.ToString()
        Dim animCounter As Integer = 0
        Do While animCounter < animationTracks.AGTracks.Count
                    animationTrack = CType(animationTracks.AGTracks.Element(animCounter), IAGAnimationTrack)
          If animationTrack.Name <> selectedTrack Then
                        Dim trackToDisable As IAGAnimationTrack
                        trackToDisable = Nothing
            animationTracks.FindTrack(animationTrack.Name, trackToDisable)
            trackToDisable.IsEnabled = False
          ElseIf animationTrack.Name = selectedTrack Then
            animationTrack.IsEnabled = True
          End If
          animCounter = animCounter + 1
        Loop

      ElseIf theCamForm.animTracksListBox.SelectedItem Is Nothing Then
        MessageBox.Show("No Track Selected - All enabled tracks will be played")
      End If
    End Sub

    'function for playing animation
    Public Sub playAnimation()
      animUtils = New AGAnimationUtilsClass()
      'register/unregister events for tracing camera path based on selection
      animEvent = CType(animUtils, IAnimationEvents_Event)

      'set animation duration
      If theCamForm.animDurationTextBox.Text <> "" And theCamForm.animDurationTextBox.Text <> "Optional" Then
        animEnv.AnimationDuration = Convert.ToDouble(theCamForm.animDurationTextBox.Text)
      Else
        MessageBox.Show("Please enter animation duration", "Error")
        Return
      End If

      'register animation event handler
      AddHandler animEvent.StateChanged, AddressOf myAnimationEventHandler

      'enable/disable other buttons
      theCamForm.stopButton.Enabled = True
      theCamForm.generatePathButton.Enabled = False
      theCamForm.playButton.Enabled = False

      animPlayer = CType(animUtils, IAGAnimationPlayer)

      animationDuration = animEnv.AnimationDuration

      animPlayer.PlayAnimation(animationTracks, animEnv, Nothing)
    End Sub

    'function for creating specified number of graphics per second
    Public Sub generatePathPerSecond()
      'set animation duration
      If theCamForm.animDurationTextBox.Text <> "" And theCamForm.animDurationTextBox.Text <> "Optional" Then
        animEnv.AnimationDuration = Convert.ToDouble(theCamForm.animDurationTextBox.Text)
      Else
        MessageBox.Show("Please enter animation duration", "Error")
        Return
      End If
      animationDuration = animEnv.AnimationDuration

      Dim numPtsPerSecond As Integer = 0
      If theCamForm.numPtsPerSecTextBox.Text <> "" Then
        numPtsPerSecond = Convert.ToInt32(theCamForm.numPtsPerSecTextBox.Text)
      End If

      addGraphicLayer()

      Dim selectedTrack As String = theCamForm.animTracksListBox.SelectedItem.ToString()

      animationTracks.FindTrack(selectedTrack, animationTrack)

      Dim kFrames As IAGAnimationTrackKeyframes = CType(animationTrack, IAGAnimationTrackKeyframes)

      Dim kFrameCount As Integer = kFrames.KeyframeCount

      'total number of points to be created
      Dim totalPts As Integer = CInt(numPtsPerSecond * animationDuration)

      'this is the from point for the lines connecting the interpolated point graphics
      Dim previousPt As IPoint = New PointClass()
      Dim prevPtZAware As IZAware = CType(previousPt, IZAware)
      prevPtZAware.ZAware = True
      previousPt.PutCoords(0, 0)

      'this is the line connecting the interpolated camera positions
      Dim connectingLine As IPolyline = New PolylineClass()
      Dim lineZAware As IZAware = CType(connectingLine, IZAware)
      lineZAware.ZAware = True

      'disable all buttons
      theCamForm.playButton.Enabled = False
      theCamForm.stopButton.Enabled = False
      theCamForm.generatePathButton.Enabled = False

      'loop over the keyframes in the selected camera track
      Dim i As Integer = 0
      Do While i < kFrameCount
                Dim currentKeyframe As IAGKeyframe = CType(kFrames.Keyframe(i), ESRI.ArcGIS.Animation.IAGKeyframe)
                Dim prevKeyframe As IAGKeyframe
                Dim nextKeyframe As IAGKeyframe
                Dim afterNextKeyframe As IAGKeyframe

                'if else statements to determine the keyframe arguments to the interpolate method
                'this is needed because the first, second-last and the last keyframes should be handled differently
                'than the middle keyframes
                If i > 0 Then
                    prevKeyframe = CType(kFrames.Keyframe(i - 1), ESRI.ArcGIS.Animation.IAGKeyframe)

                Else
                    prevKeyframe = CType(kFrames.Keyframe(i), ESRI.ArcGIS.Animation.IAGKeyframe)
                End If

                If i < kFrameCount - 1 Then
                    nextKeyframe = CType(kFrames.Keyframe(i + 1), ESRI.ArcGIS.Animation.IAGKeyframe)
                Else
                    nextKeyframe = CType(kFrames.Keyframe(i), ESRI.ArcGIS.Animation.IAGKeyframe)
                End If

                If i < kFrameCount - 2 Then
                    afterNextKeyframe = CType(kFrames.Keyframe(i + 2), ESRI.ArcGIS.Animation.IAGKeyframe)
                Else
                    'this should be equal to the nextKeyFrame for the last keyframe
                    afterNextKeyframe = nextKeyframe 'kFrames.Keyframe(i);
                End If

                Dim origCamLat, origCamLong, origCamAlt As Double
                Dim interLat, interLong, interAlt As Double
                Dim tarLat, tarLong, tarAlt As Double
                Dim interTarLat, interTarLong, interTarAlt As Double

                globeCamera.GetObserverLatLonAlt(origCamLat, origCamLong, origCamAlt)
                globeCamera.GetTargetLatLonAlt(tarLat, tarLong, tarAlt)

                Dim pAnimContainer As IAGAnimationContainer = animationTracks.AnimationObjectContainer

                Dim objToInterpolate As Object = CObj(globeCamera)

                Dim timeDiff As Double = nextKeyframe.TimeStamp - currentKeyframe.TimeStamp

                Dim numPtsToInterpolateNow As Integer
                numPtsToInterpolateNow = Convert.ToInt32((timeDiff * totalPts))

                'interpolate positions between keyframes and draw the graphics

                'for 0 to n-1 keyframes
                If i < kFrameCount - 1 Then
                    Dim j As Integer = 0
                    Do While j < numPtsToInterpolateNow
                        Dim timeToInterpolate As Double
                        timeToInterpolate = currentKeyframe.TimeStamp + j * (timeDiff / (numPtsToInterpolateNow))

                        currentKeyframe.Interpolate(animationTrack, pAnimContainer, objToInterpolate, 1, timeToInterpolate, nextKeyframe, prevKeyframe, afterNextKeyframe)
                        currentKeyframe.Interpolate(animationTrack, pAnimContainer, objToInterpolate, 2, timeToInterpolate, nextKeyframe, prevKeyframe, afterNextKeyframe)
                        currentKeyframe.Interpolate(animationTrack, pAnimContainer, objToInterpolate, 3, timeToInterpolate, nextKeyframe, prevKeyframe, afterNextKeyframe)
                        currentKeyframe.Interpolate(animationTrack, pAnimContainer, objToInterpolate, 4, timeToInterpolate, nextKeyframe, prevKeyframe, afterNextKeyframe)
                        currentKeyframe.Interpolate(animationTrack, pAnimContainer, objToInterpolate, 5, timeToInterpolate, nextKeyframe, prevKeyframe, afterNextKeyframe)
                        currentKeyframe.Interpolate(animationTrack, pAnimContainer, objToInterpolate, 6, timeToInterpolate, nextKeyframe, prevKeyframe, afterNextKeyframe)

                        'get observer and target lat, long, alt after interpolation
                        globeCamera.GetObserverLatLonAlt(interLat, interLong, interAlt)
                        globeCamera.GetTargetLatLonAlt(interTarLat, interTarLong, interTarAlt)

                        'set observer and target lat, long, alt to original values before interpolation
                        globeCamera.SetObserverLatLonAlt(origCamLat, origCamLong, origCamAlt)
                        globeCamera.SetTargetLatLonAlt(tarLat, tarLong, tarAlt)

                        Dim pObs As IPoint = New PointClass()
                        Dim obsZAware As IZAware = CType(pObs, IZAware)
                        obsZAware.ZAware = True
                        pObs.X = interLong
                        pObs.Y = interLat
                        pObs.Z = interAlt * 1000

                        Dim symbolSize As Double = 10000

                        'change the symbol size based on distance to ground
                        If pObs.Z >= 10000 Then
                            symbolSize = 10000 + pObs.Z / 10
                        Else
                            symbolSize = pObs.Z
                        End If

                        'add graphics - keyframes (j=0) are colored differently
                        If j = 0 Then
                            addPointGraphicElements(pObs, 2552550, symbolSize)
                        Else
                            addPointGraphicElements(pObs, 16732415, symbolSize)
                        End If

                        connectingLine.FromPoint = previousPt
                        connectingLine.ToPoint = pObs

                        'barring the first keyframe, create the line connecting the interpolated points
                        If i = 0 And j = 0 Then
                        Else
                            addLineGraphicElements(connectingLine, 150150150)
                        End If

                        'update the previous point
                        previousPt.PutCoords(pObs.X, pObs.Y)
                        previousPt.Z = pObs.Z

                        'add camera to target direction
                        If theCamForm.camToTargetDirectionCheckBox.Checked = True Then
                            cameraToTargetDirection(interLat, interLong, interAlt, interTarLat, interTarLong, interTarAlt)
                        End If

                        globeDisplay.RefreshViewers()
                        j += 1
                    Loop
                End If

                'for last keyframe
                If i = kFrameCount - 1 Then
                    currentKeyframe.Interpolate(animationTrack, pAnimContainer, objToInterpolate, 4, 1, nextKeyframe, prevKeyframe, afterNextKeyframe)
                    currentKeyframe.Interpolate(animationTrack, pAnimContainer, objToInterpolate, 5, 1, nextKeyframe, prevKeyframe, afterNextKeyframe)
                    currentKeyframe.Interpolate(animationTrack, pAnimContainer, objToInterpolate, 6, 1, nextKeyframe, prevKeyframe, afterNextKeyframe)
                    currentKeyframe.Interpolate(animationTrack, pAnimContainer, objToInterpolate, 1, 1, nextKeyframe, prevKeyframe, afterNextKeyframe)
                    currentKeyframe.Interpolate(animationTrack, pAnimContainer, objToInterpolate, 2, 1, nextKeyframe, prevKeyframe, afterNextKeyframe)
                    currentKeyframe.Interpolate(animationTrack, pAnimContainer, objToInterpolate, 3, 1, nextKeyframe, prevKeyframe, afterNextKeyframe)

                    globeCamera.GetObserverLatLonAlt(interLat, interLong, interAlt)
                    globeCamera.GetTargetLatLonAlt(interTarLat, interTarLong, interTarAlt)

                    globeCamera.SetObserverLatLonAlt(origCamLat, origCamLong, origCamAlt)
                    globeCamera.SetTargetLatLonAlt(tarLat, tarLong, tarAlt)

                    Dim pObs As IPoint = New PointClass()
                    Dim obsZAware As IZAware = CType(pObs, IZAware)
                    obsZAware.ZAware = True
                    pObs.X = interLong
                    pObs.Y = interLat
                    pObs.Z = interAlt * 1000

                    Dim symbolSize As Double = 10000

                    If pObs.Z >= 10000 Then
                        symbolSize = 10000 + pObs.Z / 10
                    Else
                        symbolSize = pObs.Z
                    End If

                    connectingLine.FromPoint = previousPt
                    connectingLine.ToPoint = pObs

                    addPointGraphicElements(pObs, 2552550, symbolSize)
                    addLineGraphicElements(connectingLine, 150150150)

                    'add camera to target orientation
                    If theCamForm.camToTargetDirectionCheckBox.Checked = True Then
                        cameraToTargetDirection(interLat, interLong, interAlt, interTarLat, interTarLong, interTarAlt)
                    End If

                    globeDisplay.RefreshViewers()
                End If
                i += 1
            Loop

            'enable buttons
            theCamForm.playButton.Enabled = True
            theCamForm.generatePathButton.Enabled = True
        End Sub

        'function for creating specified number of graphics between keyframe positions
        Public Sub generatePathBtwnKFrames()
            Dim numPtsBtwnKFrames As Integer = 0

            'this is the from point for the lines connecting the interpolated point graphics
            Dim previousPt As IPoint = New PointClass()
            Dim prevPtZAware As IZAware = CType(previousPt, IZAware)
            prevPtZAware.ZAware = True
            previousPt.PutCoords(0, 0)

            'this is the line connecting the interpolated camera positions
            Dim connectingLine As IPolyline = New PolylineClass()
            Dim lineZAware As IZAware = CType(connectingLine, IZAware)
            lineZAware.ZAware = True

            If theCamForm.ptsBtwnKframeTextBox.Text <> "" Then
                numPtsBtwnKFrames = Convert.ToInt32(theCamForm.ptsBtwnKframeTextBox.Text)
            Else
                MessageBox.Show("Please enter the number of points to be created")
                Return
            End If
            theCamForm.playButton.Enabled = False
            theCamForm.stopButton.Enabled = False
            theCamForm.generatePathButton.Enabled = False

            addGraphicLayer()

            Dim selectedTrack As String = theCamForm.animTracksListBox.SelectedItem.ToString()

            animationTracks.FindTrack(selectedTrack, animationTrack)

            Dim kFrames As IAGAnimationTrackKeyframes = CType(animationTrack, IAGAnimationTrackKeyframes)

            Dim kFrameCount As Integer = kFrames.KeyframeCount

            'loop over the keyframes in the selected camera track
            Dim i As Integer = 0
            Do While i < kFrameCount
                Dim currentKeyframe As IAGKeyframe = CType(kFrames.Keyframe(i), ESRI.ArcGIS.Animation.IAGKeyframe)
                Dim prevKeyframe As IAGKeyframe
                Dim nextKeyframe As IAGKeyframe
                Dim afterNextKeyframe As IAGKeyframe

                'if else statements to determine the keyframe arguments to the interpolate method
                'this is needed because the first and the last keyframes should be handled differently
                'than the middle keyframes
                If i > 0 Then
                    prevKeyframe = CType(kFrames.Keyframe(i - 1), ESRI.ArcGIS.Animation.IAGKeyframe)

                Else
                    prevKeyframe = CType(kFrames.Keyframe(i), ESRI.ArcGIS.Animation.IAGKeyframe)
                End If

                If i < kFrameCount - 1 Then
                    nextKeyframe = CType(kFrames.Keyframe(i + 1), ESRI.ArcGIS.Animation.IAGKeyframe)
                Else
                    nextKeyframe = CType(kFrames.Keyframe(i), ESRI.ArcGIS.Animation.IAGKeyframe)
                End If

                If i < kFrameCount - 2 Then
                    afterNextKeyframe = CType(kFrames.Keyframe(i + 2), ESRI.ArcGIS.Animation.IAGKeyframe)
                Else
                    'this should be equal to the nextKeyFrame for the last keyframe
                    afterNextKeyframe = nextKeyframe 'kFrames.Keyframe(i);
                End If

                Dim origCamLat, origCamLong, origCamAlt As Double
                Dim interLat, interLong, interAlt As Double
                Dim tarLat, tarLong, tarAlt As Double
                Dim interTarLat, interTarLong, interTarAlt As Double

                globeCamera.GetObserverLatLonAlt(origCamLat, origCamLong, origCamAlt)
                globeCamera.GetTargetLatLonAlt(tarLat, tarLong, tarAlt)


                Dim pAnimContainer As IAGAnimationContainer = animationTracks.AnimationObjectContainer

                Dim objToInterpolate As Object = CObj(globeCamera)

                Dim timeDiff As Double = nextKeyframe.TimeStamp - currentKeyframe.TimeStamp

                'interpolate positions between keyframes and draw the graphics
                Dim j As Integer = 0
                Do While j < numPtsBtwnKFrames + 1
                    Dim timeToInterpolate As Double = currentKeyframe.TimeStamp + j * (timeDiff / (numPtsBtwnKFrames + 1))

                    'for 0 to n-1 keyframes
                    If i >= 0 And i < kFrameCount - 1 Then
                        currentKeyframe.Interpolate(animationTrack, pAnimContainer, objToInterpolate, 4, timeToInterpolate, nextKeyframe, prevKeyframe, afterNextKeyframe)
                        currentKeyframe.Interpolate(animationTrack, pAnimContainer, objToInterpolate, 5, timeToInterpolate, nextKeyframe, prevKeyframe, afterNextKeyframe)
                        currentKeyframe.Interpolate(animationTrack, pAnimContainer, objToInterpolate, 6, timeToInterpolate, nextKeyframe, prevKeyframe, afterNextKeyframe)
                        currentKeyframe.Interpolate(animationTrack, pAnimContainer, objToInterpolate, 1, timeToInterpolate, nextKeyframe, prevKeyframe, afterNextKeyframe)
                        currentKeyframe.Interpolate(animationTrack, pAnimContainer, objToInterpolate, 2, timeToInterpolate, nextKeyframe, prevKeyframe, afterNextKeyframe)
                        currentKeyframe.Interpolate(animationTrack, pAnimContainer, objToInterpolate, 3, timeToInterpolate, nextKeyframe, prevKeyframe, afterNextKeyframe)

                        globeCamera.GetObserverLatLonAlt(interLat, interLong, interAlt)
                        globeCamera.GetTargetLatLonAlt(interTarLat, interTarLong, interTarAlt)

                        globeCamera.SetObserverLatLonAlt(origCamLat, origCamLong, origCamAlt)
                        globeCamera.SetTargetLatLonAlt(tarLat, tarLong, tarAlt)

                        Dim pObs As IPoint = New PointClass()
                        Dim obsZAware As IZAware = CType(pObs, IZAware)
                        obsZAware.ZAware = True
                        pObs.X = interLong
                        pObs.Y = interLat
                        pObs.Z = interAlt * 1000

                        Dim symbolSize As Double = 10000

                        If pObs.Z >= 10000 Then
                            symbolSize = 10000 + pObs.Z / 10
                        Else
                            symbolSize = pObs.Z
                        End If

                        If j = 0 Then
                            addPointGraphicElements(pObs, 2552550, symbolSize)
                        Else
                            addPointGraphicElements(pObs, 16732415, symbolSize)
                        End If

                        connectingLine.FromPoint = previousPt
                        connectingLine.ToPoint = pObs

                        If i = 0 And j = 0 Then
                        Else
                            addLineGraphicElements(connectingLine, 150150150)
                        End If

                        previousPt.PutCoords(pObs.X, pObs.Y)
                        previousPt.Z = pObs.Z

                        'add camera to target orientation
                        If theCamForm.camToTargetDirectionCheckBox.Checked = True Then
                            cameraToTargetDirection(interLat, interLong, interAlt, interTarLat, interTarLong, interTarAlt)
                        End If

                        globeDisplay.RefreshViewers()

                        'for last keyframe
                    ElseIf i = kFrameCount - 1 Then
                        If j = 0 Then
                            currentKeyframe.Interpolate(animationTrack, pAnimContainer, objToInterpolate, 4, 1, nextKeyframe, prevKeyframe, afterNextKeyframe)
                            currentKeyframe.Interpolate(animationTrack, pAnimContainer, objToInterpolate, 5, 1, nextKeyframe, prevKeyframe, afterNextKeyframe)
                            currentKeyframe.Interpolate(animationTrack, pAnimContainer, objToInterpolate, 6, 1, nextKeyframe, prevKeyframe, afterNextKeyframe)
                            currentKeyframe.Interpolate(animationTrack, pAnimContainer, objToInterpolate, 1, 1, nextKeyframe, prevKeyframe, afterNextKeyframe)
                            currentKeyframe.Interpolate(animationTrack, pAnimContainer, objToInterpolate, 2, 1, nextKeyframe, prevKeyframe, afterNextKeyframe)
                            currentKeyframe.Interpolate(animationTrack, pAnimContainer, objToInterpolate, 3, 1, nextKeyframe, prevKeyframe, afterNextKeyframe)

                            globeCamera.GetObserverLatLonAlt(interLat, interLong, interAlt)
                            globeCamera.GetTargetLatLonAlt(interTarLat, interTarLong, interTarAlt)

                            globeCamera.SetObserverLatLonAlt(origCamLat, origCamLong, origCamAlt)
                            globeCamera.SetTargetLatLonAlt(tarLat, tarLong, tarAlt)

                            Dim pObs As IPoint = New PointClass()
                            Dim obsZAware As IZAware = CType(pObs, IZAware)
                            obsZAware.ZAware = True
                            pObs.X = interLong
                            pObs.Y = interLat
                            pObs.Z = interAlt * 1000

                            Dim symbolSize As Double = 10000

                            If pObs.Z >= 10000 Then
                                symbolSize = 10000 + pObs.Z / 10
                            Else
                                symbolSize = pObs.Z
                            End If

                            connectingLine.FromPoint = previousPt
                            connectingLine.ToPoint = pObs

                            addPointGraphicElements(pObs, 2552550, symbolSize)
                            addLineGraphicElements(connectingLine, 150150150)

                            'add camera to target orientation
                            If theCamForm.camToTargetDirectionCheckBox.Checked = True Then
                                cameraToTargetDirection(interLat, interLong, interAlt, interTarLat, interTarLong, interTarAlt)
                            End If

                            globeDisplay.RefreshViewers()
                        End If
                    End If
                    j += 1
                Loop
                i += 1
            Loop

            'enable buttons
            theCamForm.playButton.Enabled = True
            theCamForm.generatePathButton.Enabled = True
        End Sub

    'function for generating camera to target direction
    Public Sub cameraToTargetDirection(ByVal camLat As Double, ByVal camLong As Double, ByVal camAlt As Double, ByVal tarLat As Double, ByVal tarLong As Double, ByVal tarAlt As Double)
      Dim camPosition As IPoint = New PointClass()
      Dim targetPosition As IPoint = New PointClass()

      Dim pCamera As ICamera = CType(globeCamera, ICamera)

      Dim obsZAware As IZAware = CType(camPosition, IZAware)
      obsZAware.ZAware = True

      camPosition.PutCoords(camLong, camLat)
      camPosition.Z = camAlt * 1000

      Dim targetZAware As IZAware = CType(targetPosition, IZAware)
      targetZAware.ZAware = True

      targetPosition.PutCoords(tarLong, tarLat)
      targetPosition.Z = tarAlt

      Dim directionLine As IPolyline = New PolylineClass()
      Dim zAwareLine As IZAware = CType(directionLine, IZAware)
      zAwareLine.ZAware = True

      directionLine.FromPoint = camPosition
      directionLine.ToPoint = targetPosition


      addLineGraphicElements(directionLine, 255)

    End Sub

    'function for adding a graphics layer
    Public Sub addGraphicLayer()
      graphicsLayer = New GlobeGraphicsLayerClass()
      Dim pLayer As ILayer
      pLayer = CType(graphicsLayer, ILayer)
      pLayer.Name = "CameraPathGraphicsLayer"
      globe.AddLayerType(pLayer, esriGlobeLayerType.esriGlobeLayerTypeDraped, True)
    End Sub

    'function for adding point markers
    Public Sub addPointGraphicElements(ByVal inPoint As ESRI.ArcGIS.Geometry.IPoint, ByVal symbolColor As Integer, ByVal symbolSize As Double)
      Dim pElement As IElement = New MarkerElementClass()
      Dim symbol3d As ISimpleMarker3DSymbol = New SimpleMarker3DSymbolClass()

      Dim markerStyle As String = ""

      If Not theCamForm.symbolTypeListBox.SelectedItem Is Nothing Then
        markerStyle = theCamForm.symbolTypeListBox.SelectedItem.ToString()
      End If

      If markerStyle = "Cone" Then
      symbol3d.Style = esriSimple3DMarkerStyle.esriS3DMSCone
      ElseIf markerStyle = "Sphere" Then
      symbol3d.Style = esriSimple3DMarkerStyle.esriS3DMSSphere
      ElseIf markerStyle = "Cylinder" Then
      symbol3d.Style = esriSimple3DMarkerStyle.esriS3DMSCylinder
      ElseIf markerStyle = "Cube" Then
      symbol3d.Style = esriSimple3DMarkerStyle.esriS3DMSCube
      ElseIf markerStyle = "Diamond" Then
      symbol3d.Style = esriSimple3DMarkerStyle.esriS3DMSDiamond
      ElseIf markerStyle = "Tetrahedron" Then
      symbol3d.Style = esriSimple3DMarkerStyle.esriS3DMSTetra
      Else
        symbol3d.Style = esriSimple3DMarkerStyle.esriS3DMSCone
      End If

      symbol3d.ResolutionQuality = 1
      Dim pColor As IColor = New RgbColorClass()
      pColor.RGB = symbolColor '16732415;

      Dim pMarkerSymbol As IMarkerSymbol
      pMarkerSymbol = CType(symbol3d, IMarkerSymbol)
      pMarkerSymbol.Color = pColor

      If symbolSize < 0 Then
      symbolSize = Math.Abs(symbolSize)
      End If
      If symbolSize = 0 Then
      symbolSize = 5000
      End If
      pMarkerSymbol.Size = symbolSize

      pElement.Geometry = inPoint
      Dim pMarkerElement As IMarkerElement
      pMarkerElement = CType(pElement, IMarkerElement)
      pMarkerElement.Symbol = pMarkerSymbol

      graphicsLayer.AddElement(pElement, 1)

    End Sub

    'function for adding line graphics elements
    Public Sub addLineGraphicElements(ByVal inLine As ESRI.ArcGIS.Geometry.IPolyline, ByVal symbolColor As Integer)
      Dim pElement As IElement = New LineElementClass() ' MarkerElementClass();
      Dim symbol3d As ISimpleLine3DSymbol = New SimpleLine3DSymbolClass()

      Dim markerStyle As String = ""

      If Not theCamForm.symbolTypeListBox.SelectedItem Is Nothing Then
        markerStyle = theCamForm.symbolTypeListBox.SelectedItem.ToString()
      End If

      If markerStyle = "Strip" Then
      symbol3d.Style = esriSimple3DLineStyle.esriS3DLSStrip
      ElseIf markerStyle = "Wall" Then
      symbol3d.Style = esriSimple3DLineStyle.esriS3DLSWall
      Else
        symbol3d.Style = esriSimple3DLineStyle.esriS3DLSTube
      End If

      symbol3d.ResolutionQuality = 1
      Dim pColor As IColor = New RgbColorClass()
      pColor.RGB = symbolColor

      Dim pLineSymbol As ILineSymbol
      pLineSymbol = CType(symbol3d, ILineSymbol)
      pLineSymbol.Color = pColor
      pLineSymbol.Width = 1

      pElement.Geometry = inLine
      Dim pLineElement As ILineElement
      pLineElement = CType(pElement, ILineElement)
      pLineElement.Symbol = pLineSymbol

      graphicsLayer.AddElement(pElement, 1)
    End Sub


    'event handlers
    Public Sub formPlayButtonClickEventHandler(ByVal sender As Object, ByVal e As System.EventArgs)
      If Not theCamForm.animTracksListBox.SelectedItem Is Nothing Then
        enableSelectedTrack()
        'play the animation
        Me.playAnimation()
      Else
        MessageBox.Show("Please select a camera track", "Error")
      End If
    End Sub

    Public Sub formStopButtonClickEventHandler(ByVal sender As Object, ByVal e As System.EventArgs)
      animPlayer.StopAnimation()
      theCamForm.stopButton.Enabled = False
      If theCamForm.generateCamPathCheckBox.Checked = True Then
      theCamForm.generatePathButton.Enabled = True
      End If
    End Sub

    Public Sub formGeneratePathButtonClickEventHandler(ByVal sender As Object, ByVal e As System.EventArgs)
      If Not theCamForm.animTracksListBox.SelectedItem Is Nothing Then
        If theCamForm.ptsPerSecRadioButton.Checked = True Then
          If theCamForm.numPtsPerSecTextBox.Text = "" Then
            MessageBox.Show("Please enter number of points to be created per second", "Error")
            Return
          End If
          generatePathPerSecond()
        ElseIf theCamForm.ptsBtwnKframeRadioButton.Checked = True Then
          If theCamForm.ptsBtwnKframeTextBox.Text = "" Then
            MessageBox.Show("Please enter number of points to be created between keyframes", "Error")
            Return
          End If
          generatePathBtwnKFrames()
        End If
      Else
        MessageBox.Show("Please select a camera track")
      End If
    End Sub

    Public Sub formCheckbox1CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
      If theCamForm.generateCamPathCheckBox.Checked = True Then
      theCamForm.generatePathButton.Enabled = True
      Else
        theCamForm.generatePathButton.Enabled = False
      End If
    End Sub

    Private Sub theCamForm_Closing(ByVal sender As Object, ByVal e As CancelEventArgs)
      theCamForm.animTracksListBox.Items.Clear()
      toolIsInitialized = False
    End Sub

    Public Sub myAnimationEventHandler(ByVal animState As esriAnimationState)
      globeDispEvent = CType(globeDisplay, IGlobeDisplayEvents_Event)

      If animState = esriAnimationState.esriAnimationStopped Then
        theCamForm.playButton.Enabled = True
        theCamForm.generatePathButton.Enabled = True
        theCamForm.stopButton.Enabled = False
      End If
    End Sub

#End Region

  End Class

End Namespace