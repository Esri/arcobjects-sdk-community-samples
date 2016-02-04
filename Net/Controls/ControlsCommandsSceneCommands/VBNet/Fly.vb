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
Imports System.Drawing
Imports ESRI.ArcGIS.Analyst3D
Imports ESRI.ArcGIS.GeomeTry
Imports ESRI.ArcGIS.Controls
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.ADF.CATIDs
Imports System.Runtime.InteropServices

<ComClass(Fly.ClassId, Fly.InterfaceId, Fly.EventsId)> _
Public NotInheritable Class Fly
    Inherits BaseTool

#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "C74AE939-B3B1-4F28-9E79-D172CF1F2043"
    Public Const InterfaceId As String = "86B05C26-D3C0-49E9-957D-7F54B35C8940"
    Public Const EventsId As String = "E419FD47-DA90-45B7-8563-BD4D9D023555"
#End Region
#Region "COM Registration Function(s)"
    <ComRegisterFunction(), ComVisibleAttribute(False)> _
    Public Shared Sub RegisterFunction(ByVal registerType As Type)
        ' Required for ArcGIS Component Category Registrar support
        ArcGISCategoryRegistration(registerType)

        'Add any COM registration code after the ArcGISCategoryRegistration() call

    End Sub

    <ComUnregisterFunction(), ComVisibleAttribute(False)> _
    Public Shared Sub UnregisterFunction(ByVal registerType As Type)
        ' Required for ArcGIS Component Category Registrar support
        ArcGISCategoryUnregistration(registerType)

        'Add any COM unregistration code after the ArcGISCategoryUnregistration() call

    End Sub

#Region "ArcGIS Component Category Registrar generated code"
    ''' <summary>
    ''' Required method for ArcGIS Component Category registration -
    ''' Do not modify the contents of this method with the code editor.
    ''' </summary>
    Private Shared Sub ArcGISCategoryRegistration(ByVal registerType As Type)
        Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
        ControlsCommands.Register(regKey)

    End Sub
    ''' <summary>
    ''' Required method for ArcGIS Component Category unregistration -
    ''' Do not modify the contents of this method with the code editor.
    ''' </summary>
    Private Shared Sub ArcGISCategoryUnregistration(ByVal registerType As Type)
        Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
        ControlsCommands.Unregister(regKey)

    End Sub

#End Region
#End Region
    Declare Function GetClientRect Lib "user32" (ByVal hwnd As Integer, ByRef lpRect As Rectangle) As Integer
    Declare Function SetCursor Lib "user32" (ByVal hCursor As Integer) As Integer

    Private m_pSceneHookHelper As ISceneHookHelper
    Private m_bInUse As Boolean
    Dim bCancel As Boolean = False
    Private m_lMouseX As Long
    Private m_lMouseY As Long
    Private m_dMotion As Double 'speed of the scene fly through in scene units
    Private m_pPointObs As IPoint 'observer
    Private m_pPointTgt As IPoint 'target
    Private m_dDistance As Double 'distance between target and observer
    Private m_dElevation As Double 'normal fly angles in radians
    Private m_dAzimut As Double 'normal fly angles in radians
    Private m_iSpeed As Integer 'speed of a flight
    Private m_flyCur As System.Windows.Forms.Cursor
    Private m_moveFlyCur As System.Windows.Forms.Cursor

  ' A creatable COM class must have a Public Sub New() 
  ' with no parameters, otherwise, the class will not be 
  ' registered in the COM registry and cannot be created 
  ' via CreateObject.
    Public Sub New()
        MyBase.New()

        MyBase.m_category = "Sample_SceneControl(VB.NET)"
        MyBase.m_caption = "Fly"
        MyBase.m_toolTip = "Fly"
        MyBase.m_name = "Sample_SceneControl(VB.NET)/Fly"
        MyBase.m_message = "Flies through the scene"

        'Load resources
        Dim res() As String = GetType(Fly).Assembly.GetManifestResourceNames()
        If res.GetLength(0) > 0 Then
            MyBase.m_bitmap = New System.Drawing.Bitmap(GetType(Fly).Assembly.GetManifestResourceStream("SceneToolsVB.fly.bmp"))
        End If
        m_flyCur = New System.Windows.Forms.Cursor(GetType(Fly).Assembly.GetManifestResourceStream("SceneToolsVB.fly.cur"))
        m_moveFlyCur = New System.Windows.Forms.Cursor(GetType(Fly).Assembly.GetManifestResourceStream("SceneToolsVB.fly1.cur"))
        m_pSceneHookHelper = New SceneHookHelperClass
        m_iSpeed = 0
    End Sub

    Public Overrides Sub OnCreate(ByVal hook As Object)
        m_pSceneHookHelper.Hook = hook
    End Sub

    Public Overrides ReadOnly Property Enabled() As Boolean
        Get
            'Disable if orthographic (2D) view
            If m_pSceneHookHelper.Hook Is Nothing Or m_pSceneHookHelper.Scene Is Nothing Then
                Return False
            Else
                Dim pCamera As ICamera = CType(m_pSceneHookHelper.Camera, ICamera)
                If pCamera.ProjectionType = esri3DProjectionType.esriOrthoProjection Then
                    Return False
                Else
                    Return True
                End If
            End If
        End Get
    End Property

    Public Overrides ReadOnly Property Cursor() As Integer
        Get
            If (m_bInUse) Then
                Return m_moveFlyCur.Handle.ToInt32()
            Else
                Return m_flyCur.Handle.ToInt32()
            End If
        End Get
    End Property

    Public Overrides Function Deactivate() As Boolean
        Return True
    End Function

    Public Overrides Sub OnMouseUp(ByVal Button As Integer, ByVal Shift As Integer, ByVal X As Integer, ByVal Y As Integer)
        If (Not m_bInUse) Then
            m_lMouseX = X
            m_lMouseY = Y

            If (m_iSpeed = 0) Then
                StartFlight()
            End If
        Else
            'Set the speed
            If (Button = 1) Then
                m_iSpeed += 1
            ElseIf (Button = 2) Then
                m_iSpeed -= 1
            End If

            'Start or end the flight
            If (m_iSpeed = 0) Then
                EndFlight()
            Else
                StartFlight()
            End If
        End If
    End Sub

    Public Overrides Sub OnMouseMove(ByVal Button As Integer, ByVal Shift As Integer, ByVal X As Integer, ByVal Y As Integer)
        If (Not m_bInUse) Then
            Return
        End If
        m_lMouseX = X
        m_lMouseY = Y
    End Sub

    Public Overrides Sub OnKeyUp(ByVal keyCode As Integer, ByVal Shift As Integer)
        If (m_bInUse) Then
            'Slow down the speed of the fly through
            If (keyCode = 40 Or keyCode = 37) Then
                m_dMotion = m_dMotion / 2
                'Speed up the speed of the fly through
            ElseIf (keyCode = 38 Or keyCode = 39) Then
                m_dMotion = m_dMotion * 2
            End If
        End If
    End Sub

    Public Sub StartFlight()
        m_bInUse = True

        'Get the extent of the scene graph
        Dim pEnvelope As IEnvelope
        pEnvelope = m_pSceneHookHelper.SceneGraph.Extent

        If (pEnvelope.IsEmpty) Then
            Return
        End If

        'Query the coordinates of the extent
        Dim dXmin, dXmax, dYmin, dYmax As Double
        pEnvelope.QueryCoords(dXmin, dYmin, dXmax, dYmax)

        'Set the speed of the scene
        If ((dXmax - dXmin) > (dYmax - dYmin)) Then
            m_dMotion = (dXmax - dXmin) / 100
        Else
            m_dMotion = (dYmax - dYmin) / 100
        End If

        'Get camera's current observer and target
        Dim pCamera As ICamera = CType(m_pSceneHookHelper.Camera, ICamera)
        m_pPointObs = pCamera.Observer
        m_pPointTgt = pCamera.Target

        'Get the differences between the observer and target
        Dim dx, dy, dz As Double
        dx = m_pPointTgt.X - m_pPointObs.X
        dy = m_pPointTgt.Y - m_pPointObs.Y
        dz = m_pPointTgt.Z - m_pPointObs.Z

        'Determine the elevation and azimuth in radians and
        'the distance between the target and observer
        m_dElevation = Math.Atan(dz / Math.Sqrt(dx * dx + dy * dy))
        m_dAzimut = Math.Atan(dy / dx)
        m_dDistance = Math.Sqrt((dx * dx) + (dy * dy) + (dz * dz))

        'Windows API call to set cursor
        SetCursor(m_moveFlyCur.Handle.ToInt32())

        'Continue the flight
        Flight()

    End Sub

    Public Sub Flight()
        'Get IMessageDispatcher interface
        Dim pMessageDispatcher As IMessageDispatcher
        pMessageDispatcher = New MessageDispatcherClass

        'Set the ESC key to be seen as a cancel action
        pMessageDispatcher.CancelOnClick = False
        pMessageDispatcher.CancelOnEscPress = True

        'Get the scene graph
        Dim pSceneGraph As ISceneGraph = CType(m_pSceneHookHelper.SceneGraph, ISceneGraph)

        'Get the scene viewer
        Dim pSceneViewer As ISceneViewer = CType(m_pSceneHookHelper.ActiveViewer, ISceneViewer)

        'Get the camera
        Dim pCamera As ICamera = CType(m_pSceneHookHelper.Camera, ICamera)

        bCancel = False

        Do
            'Get the elapsed time
            Dim dlastFrameDuration, dMeanFrameRate As Double
            pSceneGraph.GetDrawingTimeInfo(dlastFrameDuration, dMeanFrameRate)

            If (dlastFrameDuration < 0.01) Then
                dlastFrameDuration = 0.01
            End If

            If (dlastFrameDuration > 1) Then
                dlastFrameDuration = 1
            End If

            'Windows API call to get windows client coordinates
            Dim rect As Rectangle
            rect = New Rectangle

            If (GetClientRect(m_pSceneHookHelper.ActiveViewer.hWnd, rect) = 0) Then
                Return
            End If

            'Get normal vectors
            Dim dXMouseNormal, dYMouseNormal As Double
            dXMouseNormal = 2 * (m_lMouseX / rect.Right) - 1 'should be double
            dYMouseNormal = 2 * (m_lMouseY / rect.Bottom) - 1

            'Set elevation and azimuth in radians for normal rotation
            m_dElevation = m_dElevation - (dlastFrameDuration * dYMouseNormal * Math.Abs(dYMouseNormal))
            m_dAzimut = m_dAzimut - (dlastFrameDuration * dXMouseNormal * Math.Abs(dXMouseNormal))

            If (m_dElevation > 0.45 * 3.141592) Then
                m_dElevation = 0.45 * 3.141592
            End If

            If (m_dElevation < -0.45 * 3.141592) Then
                m_dElevation = -0.45 * 3.141592
            End If

            If (m_dAzimut < 0) Then
                m_dAzimut = m_dAzimut + (2 * 3.141592)
            End If

            If (m_dAzimut > 2 * 3.141592) Then
                m_dAzimut = m_dAzimut - (2 * 3.141592)
            End If

            Dim dx, dy, dz As Double

            dx = Math.Cos(m_dElevation) * Math.Cos(m_dAzimut)
            dy = Math.Cos(m_dElevation) * Math.Sin(m_dAzimut)
            dz = Math.Sin(m_dElevation)

            'Change the viewing directions (target)
            m_pPointTgt.X = m_pPointObs.X + (m_dDistance * dx)
            m_pPointTgt.Y = m_pPointObs.Y + (m_dDistance * dy)
            m_pPointTgt.Z = m_pPointObs.Z + (m_dDistance * dz)

            'Move the camera in the viewing directions
            m_pPointObs.X = m_pPointObs.X + (dlastFrameDuration * (2 ^ m_iSpeed) * m_dMotion * dx)
            m_pPointObs.Y = m_pPointObs.Y + (dlastFrameDuration * (2 ^ m_iSpeed) * m_dMotion * dy)
            m_pPointTgt.X = m_pPointTgt.X + (dlastFrameDuration * (2 ^ m_iSpeed) * m_dMotion * dx)
            m_pPointTgt.Y = m_pPointTgt.Y + (dlastFrameDuration * (2 ^ m_iSpeed) * m_dMotion * dy)
            m_pPointObs.Z = m_pPointObs.Z + (dlastFrameDuration * (2 ^ m_iSpeed) * m_dMotion * dz)
            m_pPointTgt.Z = m_pPointTgt.Z + (dlastFrameDuration * (2 ^ m_iSpeed) * m_dMotion * dz)

            pCamera.Observer = m_pPointObs
            pCamera.Target = m_pPointTgt

            'Set the angle of the camera about the line of sight between the observer and target
            pCamera.RollAngle = 10 * dXMouseNormal * Math.Abs(dXMouseNormal)

            'Redraw the scene viewer 
            pSceneViewer.Redraw(True)

            Dim objCancel As Object = Nothing

            'Dispatch any waiting messages: OnMouseMove / OnMouseUp / OnKeyUp events
            'object objCancel = bCancel as object;
            pMessageDispatcher.Dispatch(m_pSceneHookHelper.ActiveViewer.hWnd, False, objCancel)

            'End flight if ESC key pressed
            If (bCancel = True) Then
                EndFlight()
            End If
        Loop While m_bInUse = True And bCancel = False

        SetCursor(m_flyCur.Handle.ToInt32())
        bCancel = False

    End Sub

    Public Sub EndFlight()
        m_bInUse = False

        'Get the scene graph
        Dim pSceneGraph As ISceneGraph = CType(m_pSceneHookHelper.SceneGraph, ISceneGraph)

        Dim pPointTgt As IPoint
        pPointTgt = New PointClass
        Dim pOwner As Object = Nothing, pObject As Object = Nothing
        Dim rect As Rectangle
        rect = New Rectangle

        'Windows API call to get windows client coordinates
        If (GetClientRect(m_pSceneHookHelper.ActiveViewer.hWnd, rect) <> 0) Then
            'Translate coordinates into a 3D point
            pSceneGraph.Locate(pSceneGraph.ActiveViewer, rect.Right / 2, rect.Bottom / 2, esriScenePickMode.esriScenePickAll, True, pPointTgt, pOwner, pObject)
        End If

        'Get the camera
        Dim pCamera As ICamera = CType(m_pSceneHookHelper.Camera, ICamera)

        If (Not pPointTgt Is Nothing) Then
            'Reposition target and observer
            pCamera.Target = pPointTgt
            pCamera.Observer = m_pPointObs
        End If

        'Set the angle of the camera about the line
        'of sight between the observer and target
        pCamera.RollAngle = 0
        pCamera.PropertiesChanged()

        'Windows API call to set cursor
        SetCursor(m_moveFlyCur.Handle.ToInt32())
        m_iSpeed = 0

    End Sub

    Public Overrides Sub OnKeyDown(ByVal keyCode As Integer, ByVal Shift As Integer)
        If (keyCode = 27) Then 'ESC is pressed
            bCancel = True
        End If
    End Sub
End Class


