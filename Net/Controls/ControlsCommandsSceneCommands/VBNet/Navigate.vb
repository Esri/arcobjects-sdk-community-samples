Imports System.Drawing
Imports ESRI.ArcGIS.Analyst3D
Imports ESRI.ArcGIS.GeomeTry
Imports ESRI.ArcGIS.Controls
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.ADF.CATIDs
Imports System.Runtime.InteropServices

<ComClass(Navigate.ClassId, Navigate.InterfaceId, Navigate.EventsId)> _
Public NotInheritable Class Navigate
    Inherits BaseTool

#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "E4BDFC47-0845-4DF1-B4B4-637EF86C299A"
    Public Const InterfaceId As String = "12F31942-BCB1-487C-9790-DB497983B5C7"
    Public Const EventsId As String = "2D9B12D3-59E9-4C0E-8218-FBD220446757"
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
    Declare Function GetCapture Lib "user32" (ByVal fuFlags As Integer) As Integer
    Declare Function SetCapture Lib "user32" (ByVal hwnd As Integer) As Integer
    Declare Function SetCursor Lib "user32" (ByVal hCursor As Integer) As Integer
    Declare Function ReleaseCapture Lib "user32" (ByVal hwnd As Integer) As Integer

    Private m_pSceneHookHelper As ISceneHookHelper
    Private m_bInUse As Boolean
    Private m_bGesture As Boolean
    Private bCancel As Boolean = False
    Private m_lMouseX, m_lMouseY As Long
    Private m_bSpinning As Boolean
    Private m_dSpinStep As Double
    Private m_pCursorNav As System.Windows.Forms.Cursor
    Private m_pCursorPan As System.Windows.Forms.Cursor
    Private m_pCursorZoom As System.Windows.Forms.Cursor
    Private m_pCursorGest As System.Windows.Forms.Cursor

  ' A creatable COM class must have a Public Sub New() 
  ' with no parameters, otherwise, the class will not be 
  ' registered in the COM registry and cannot be created 
  ' via CreateObject.
    Public Sub New()
        MyBase.New()

        MyBase.m_category = "Sample_SceneControl(VB.NET)"
        MyBase.m_caption = "Navigate"
        MyBase.m_toolTip = "Navigate"
        MyBase.m_name = "Sample_SceneControl(VB.NET)/Navigate"
        MyBase.m_message = "Navigates the scene"

        'Load resources
        Dim res() As String = GetType(Navigate).Assembly.GetManifestResourceNames()
        If res.GetLength(0) > 0 Then
            MyBase.m_bitmap = New System.Drawing.Bitmap(GetType(Navigate).Assembly.GetManifestResourceStream("SceneToolsVB.Navigation.bmp"))
        End If
        m_pCursorNav = New System.Windows.Forms.Cursor(GetType(Navigate).Assembly.GetManifestResourceStream("SceneToolsVB.navigation.cur"))
        m_pCursorPan = New System.Windows.Forms.Cursor(GetType(Navigate).Assembly.GetManifestResourceStream("SceneToolsVB.movehand.cur"))
        m_pCursorZoom = New System.Windows.Forms.Cursor(GetType(Navigate).Assembly.GetManifestResourceStream("SceneToolsVB.ZOOMINOUT.CUR"))
        m_pCursorGest = New System.Windows.Forms.Cursor(GetType(Navigate).Assembly.GetManifestResourceStream("SceneToolsVB.gesture.cur"))

        m_pSceneHookHelper = New SceneHookHelperClass
    End Sub

    Public Overrides Sub OnCreate(ByVal hook As Object)
        m_pSceneHookHelper.Hook = hook

        If (Not m_pSceneHookHelper Is Nothing) Then
            m_bGesture = m_pSceneHookHelper.ActiveViewer.GestureEnabled
            m_bSpinning = False
        End If
    End Sub

    Public Overrides ReadOnly Property Enabled() As Boolean
        Get
            'Disable if orthographic (2D) view
            If (m_pSceneHookHelper.Hook Is Nothing Or m_pSceneHookHelper.Scene Is Nothing) Then
                Return False
            Else
                Dim pCamera As ICamera = CType(m_pSceneHookHelper.Camera, ICamera)
                If (pCamera.ProjectionType = esri3DProjectionType.esriOrthoProjection) Then
                    Return False
                Else
                    Return True
                End If
            End If
        End Get
    End Property

    Public Overrides ReadOnly Property Cursor() As Integer
        Get
            If (m_bGesture = True) Then
                Return m_pCursorGest.Handle.ToInt32()
            Else
                Return m_pCursorNav.Handle.ToInt32()
            End If
        End Get
    End Property

    Public Overrides Function Deactivate() As Boolean
        Return True
    End Function

    Public Overrides Sub OnMouseDown(ByVal Button As Integer, ByVal Shift As Integer, ByVal X As Integer, ByVal Y As Integer)
        If (Button = 3) Then
            bCancel = True
        Else
            m_bInUse = True

            SetCapture(m_pSceneHookHelper.ActiveViewer.hWnd)

            m_lMouseX = X
            m_lMouseY = Y
        End If
    End Sub

    Public Overrides Sub OnMouseMove(ByVal Button As Integer, ByVal Shift As Integer, ByVal X As Integer, ByVal Y As Integer)
        If (m_bInUse = False) Then
            Return
        End If

        If (X - m_lMouseX = 0 And Y - m_lMouseY = 0) Then
            Return
        End If

        Dim dx, dy As Long
        dx = X - m_lMouseX
        dy = Y - m_lMouseY

        'If right mouse clicked
        If (Button = 2) Then
            'Set the zoom cursor
            SetCursor(m_pCursorZoom.Handle.ToInt32())

            If (dy < 0) Then
                m_pSceneHookHelper.Camera.Zoom(1.1)
            ElseIf (dy > 0) Then
                m_pSceneHookHelper.Camera.Zoom(0.9)
            End If
        End If

        'If two mouse buttons clicked
        If (Button = 3) Then
            'Set the pan cursor
            SetCursor(m_pCursorPan.Handle.ToInt32())

            'Create a point with previous mouse coordinates
            Dim pStartPoint As IPoint
            pStartPoint = New PointClass
            pStartPoint.PutCoords(m_lMouseX, m_lMouseY)

            'Create point with a new mouse coordinates
            Dim pEndPoint As IPoint
            pEndPoint = New PointClass
            pEndPoint.PutCoords(X, Y)

            'Pan camera
            m_pSceneHookHelper.Camera.Pan(pStartPoint, pEndPoint)
        End If

        'If left mouse clicked
        If (Button = 1) Then
            'If scene viewer gesturing is disabled move the camera observer
            If (m_bGesture = False) Then
                m_pSceneHookHelper.Camera.PolarUpdate(1, dx, dy, True)
            Else
                'If camera already spinning
                If (m_bSpinning = True) Then
                    'Move the camera observer
                    m_pSceneHookHelper.Camera.PolarUpdate(1, dx, dy, True)
                Else
                    'Windows API call to get windows client coordinates
                    Dim rect As Rectangle
                    rect = New Rectangle

                    GetClientRect(m_pSceneHookHelper.ActiveViewer.hWnd, rect)

                    'Recalculate the spin step
                    If (dx < 0) Then
                        m_dSpinStep = (180.0 / rect.Right - rect.Left) * (dx - m_pSceneHookHelper.ActiveViewer.GestureSensitivity)
                    Else
                        m_dSpinStep = (180.0 / rect.Right - rect.Left) * (dx + m_pSceneHookHelper.ActiveViewer.GestureSensitivity)
                    End If

                    'Start spinning
                    StartSpin()
                End If
            End If
        End If

        'Set Mouse coordinates for the next
        'OnMouse Event
        m_lMouseX = X
        m_lMouseY = Y

        'Redraw the scene viewer
        m_pSceneHookHelper.ActiveViewer.Redraw(True)

    End Sub

    Public Sub StartSpin()
        m_bSpinning = True

        'Get IMessageDispatcher interface
        Dim pMessageDispatcher As IMessageDispatcher
        pMessageDispatcher = New MessageDispatcherClass

        'Set the ESC key to be seen as a cancel action
        pMessageDispatcher.CancelOnClick = False
        pMessageDispatcher.CancelOnEscPress = True

        Do
            'Move the camera observer
            m_pSceneHookHelper.Camera.PolarUpdate(1, m_dSpinStep, 0, True)

            'Redraw the scene viewer
            m_pSceneHookHelper.ActiveViewer.Redraw(True)

            'Dispatch any waiting messages: OnMouseMove/ OnMouseDown/ OnKeyUp/ OnKeyDown events
            Dim b_oCancel As Object = Nothing
            pMessageDispatcher.Dispatch(m_pSceneHookHelper.ActiveViewer.hWnd, False, b_oCancel)

            If (bCancel = True) Then
                m_bSpinning = False
            End If
        Loop While (bCancel = False)

        bCancel = False

    End Sub

    Public Overrides Sub OnKeyDown(ByVal keyCode As Integer, ByVal Shift As Integer)
        If (keyCode = 27) Then
            bCancel = True
            SetCursor(m_pCursorNav.Handle.ToInt32())
        End If

        Select Case Shift
            Case 1 'Shift key
                SetCursor(m_pCursorPan.Handle.ToInt32())

            Case 2 'Control key
                SetCursor(m_pCursorZoom.Handle.ToInt32())

            Case 3 'shift + control key
                'Set scene viewer gesture enabled property
                If (m_bSpinning = False) Then
                    If (m_bGesture = True) Then
                        m_pSceneHookHelper.ActiveViewer.GestureEnabled = False
                        m_bGesture = False
                        SetCursor(m_pCursorNav.Handle.ToInt32())
                    Else
                        m_pSceneHookHelper.ActiveViewer.GestureEnabled = True
                        m_bGesture = True
                        SetCursor(m_pCursorGest.Handle.ToInt32())
                    End If
                End If

            Case Else
                Return
        End Select
    End Sub

    Public Overrides Sub OnKeyUp(ByVal keyCode As Integer, ByVal Shift As Integer)
        If (Shift = 1) Then
            'Pan the camera
            Select Case keyCode
                Case 38 'Up key
                    m_pSceneHookHelper.Camera.Move(esriCameraMovementType.esriCameraMoveDown, 0.01)

                Case 40 ' Down key
                    m_pSceneHookHelper.Camera.Move(esriCameraMovementType.esriCameraMoveUp, 0.01)

                Case 37 'Left key
                    m_pSceneHookHelper.Camera.Move(esriCameraMovementType.esriCameraMoveRight, 0.01)

                Case 39 'Right key
                    m_pSceneHookHelper.Camera.Move(esriCameraMovementType.esriCameraMoveLeft, 0.01)

                Case Else
                    Return
            End Select
        ElseIf (Shift = 2) Then 'Control key
            'Move camera in/out or turn camera around the observer
            Select Case keyCode
                Case 38
                    m_pSceneHookHelper.Camera.Move(esriCameraMovementType.esriCameraMoveAway, 0.01)

                Case 40
                    m_pSceneHookHelper.Camera.Move(esriCameraMovementType.esriCameraMoveToward, 0.01)

                Case 37
                    m_pSceneHookHelper.Camera.HTurnAround(-1)

                Case 39
                    m_pSceneHookHelper.Camera.HTurnAround(1)

                Case Else
                    Return
            End Select
        Else
            Dim d, dAltitude, dAzimuth As Double
            d = 5
            dAltitude = 2
            dAzimuth = 2

            'Move the camera observer by the given polar
            'increments or increase/decrease the spin speed
            Select Case keyCode
                Case 38
                    m_pSceneHookHelper.Camera.PolarUpdate(1, 0, -dAltitude * d, True)

                Case 40
                    m_pSceneHookHelper.Camera.PolarUpdate(1, 0, dAltitude * d, True)

                Case 37
                    m_pSceneHookHelper.Camera.PolarUpdate(1, dAzimuth * d, 0, True)

                Case 39
                    m_pSceneHookHelper.Camera.PolarUpdate(1, -dAzimuth * d, 0, True)

                Case 33 ' Page up
                    m_dSpinStep = m_dSpinStep * 1.1

                Case 34 ' Page down
                    m_dSpinStep = m_dSpinStep / 1.1

                Case Else
                    Return
            End Select
        End If

        'Set the navigation cursor
        If (m_bGesture = True) Then
            SetCursor(m_pCursorGest.Handle.ToInt32())
        Else
            SetCursor(m_pCursorNav.Handle.ToInt32())
        End If

        'Redraw the scene viewer
        m_pSceneHookHelper.ActiveViewer.Redraw(True)

    End Sub

    Public Overrides Sub OnMouseUp(ByVal Button As Integer, ByVal Shift As Integer, ByVal X As Integer, ByVal Y As Integer)
        'Set the navigation cursor
        If (m_bGesture = True) Then
            SetCursor(m_pCursorGest.Handle.ToInt32())
        Else
            SetCursor(m_pCursorNav.Handle.ToInt32())
        End If

        If (GetCapture(m_pSceneHookHelper.ActiveViewer.hWnd) <> 0) Then
            ReleaseCapture(m_pSceneHookHelper.ActiveViewer.hWnd)
        End If
    End Sub
End Class


