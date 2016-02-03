Imports System.Drawing
Imports ESRI.ArcGIS.Analyst3D
Imports ESRI.ArcGIS.GeomeTry
Imports ESRI.ArcGIS.Controls
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.ADF.CATIDs
Imports System.Runtime.InteropServices

<ComClass(ZoomIn.ClassId, ZoomIn.InterfaceId, ZoomIn.EventsId)> _
Public NotInheritable Class ZoomIn
    Inherits BaseTool

#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "2E281E85-C9F7-4846-B40B-BC865EBCEC26"
    Public Const InterfaceId As String = "3A6847EC-AA6C-40C2-8F4A-3C60D5D3FC9D"
    Public Const EventsId As String = "D3A50432-2789-49E5-9262-5C750875405A"
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
    Declare Function GetCapture Lib "user32" (ByVal fuFlags As Integer) As Integer
    Declare Function SetCapture Lib "user32" (ByVal hwnd As Integer) As Integer
    Declare Function ReleaseCapture Lib "user32" (ByVal hwnd As Integer) As Integer
    Declare Function GetWindowRect Lib "user32" (ByVal hwnd As Integer, ByRef lpRect As Rectangle) As Integer

    Private m_pCursor As System.Windows.Forms.Cursor
    Private m_pSceneHookHelper As ISceneHookHelper
    Private m_lMouseX, m_lMouseY As Long
    Private m_bInUse As Boolean
    Private m_pen As Pen
    Private m_brush As Brush
    Private myGraphics As Graphics

  ' A creatable COM class must have a Public Sub New() 
  ' with no parameters, otherwise, the class will not be 
  ' registered in the COM registry and cannot be created 
  ' via CreateObject.
    Public Sub New()
        MyBase.New()

        MyBase.m_category = "Sample_SceneControl(VB.NET)"
        MyBase.m_caption = "Zoom In"
        MyBase.m_toolTip = "Zoom In"
        MyBase.m_name = "Sample_SceneControl(VB.NET)/Zoom In"
        MyBase.m_message = "Zooms in on the scene"

        'Load resources
        Dim res() As String = GetType(ZoomIn).Assembly.GetManifestResourceNames()
        If res.GetLength(0) > 0 Then
            MyBase.m_bitmap = New System.Drawing.Bitmap(GetType(ZoomIn).Assembly.GetManifestResourceStream("SceneToolsVB.ZoomIn.bmp"))
        End If
        m_pCursor = New System.Windows.Forms.Cursor(GetType(ZoomIn).Assembly.GetManifestResourceStream("SceneToolsVB.ZoomIn.cur"))

        m_pSceneHookHelper = New SceneHookHelperClass

    End Sub

    Public Overrides Sub OnCreate(ByVal hook As Object)
        m_pSceneHookHelper.Hook = hook
        Dim ptrHDC As IntPtr
        ptrHDC = New IntPtr(m_pSceneHookHelper.ActiveViewer.hDC)
        myGraphics = Graphics.FromHdc(ptrHDC)
        m_brush = New SolidBrush(Color.Transparent) 'hollow brush
        m_pen = New System.Drawing.Pen(Color.Black, 2)  'A solid, width of 2 black pen
    End Sub

    Public Overrides ReadOnly Property Enabled() As Boolean
        Get
            If (m_pSceneHookHelper.Scene Is Nothing) Then
                Return False
            Else
                Return True
            End If
        End Get
    End Property

    Public Overrides ReadOnly Property Cursor() As Integer
        Get
            Return m_pCursor.Handle.ToInt32()
        End Get
    End Property

    Public Overrides Function Deactivate() As Boolean
        Return True
    End Function

    Public Overrides Sub OnKeyDown(ByVal keyCode As Integer, ByVal Shift As Integer)
        If (m_bInUse = True) Then
            If (keyCode = 27) Then 'if ESC was pressed 
                'Redraw the scene viewer
                Dim pSceneViewer As ISceneViewer = CType(m_pSceneHookHelper.ActiveViewer, ISceneViewer)

                pSceneViewer.Redraw(True)
                ReleaseCapture(m_pSceneHookHelper.ActiveViewer.hWnd)

                m_bInUse = False
            End If
        End If
    End Sub

    Public Overrides Sub OnMouseDown(ByVal Button As Integer, ByVal Shift As Integer, ByVal X As Integer, ByVal Y As Integer)
        'Initialize mouse coordinates
        m_bInUse = True
        m_lMouseX = X
        m_lMouseY = Y

        Dim pEnvelope As IEnvelope = Nothing

        'Initialize(Envelope)
        CreateEnvelope(X, Y, pEnvelope)

        'Get the scene viewer
        Dim pSceneViewer As ISceneViewer = CType(m_pSceneHookHelper.ActiveViewer, ISceneViewer)

        SetCapture(m_pSceneHookHelper.ActiveViewer.hWnd)
    End Sub

    Public Overrides Sub OnMouseMove(ByVal Button As Integer, ByVal Shift As Integer, ByVal X As Integer, ByVal Y As Integer)
        If Not m_bInUse Then
            Return
        End If

        Dim pEnvelope As IEnvelope = Nothing

        'Draw rectangle on the device
        CreateEnvelope(X, Y, pEnvelope)
        DrawRectangle(pEnvelope)
    End Sub

    Public Overrides Sub OnMouseUp(ByVal Button As Integer, ByVal Shift As Integer, ByVal X As Integer, ByVal Y As Integer)

        If Not m_bInUse Then
            Return
        End If

        If (GetCapture(m_pSceneHookHelper.ActiveViewer.hWnd) <> 0) Then
            ReleaseCapture(m_pSceneHookHelper.ActiveViewer.hWnd)
        End If

        'Get the scene viewer's camera
        Dim pCamera As ICamera = CType(m_pSceneHookHelper.Camera, ICamera)

        'Get the scene graph
        Dim pSceneGraph As ISceneGraph = CType(m_pSceneHookHelper.SceneGraph, ISceneGraph)

        'Create(Envelope)
        Dim pEnvelope As IEnvelope = Nothing
        CreateEnvelope(X, Y, pEnvelope)

        Dim pPoint As IPoint = Nothing
        Dim pOwner As Object = Nothing, pObject As Object = Nothing

        If (pEnvelope.Width = 0 Or pEnvelope.Height = 0) Then
            'Translate screen coordinates into a 3D point
            pSceneGraph.Locate(pSceneGraph.ActiveViewer, X, Y, esriScenePickMode.esriScenePickAll, True, pPoint, pOwner, pObject)

            'Set camera target and zoom in
            pCamera.Target = pPoint
            pCamera.Zoom(0.75)
        Else
            'If perspective (3D) view
            If (pCamera.ProjectionType = esri3DProjectionType.esriPerspectiveProjection) Then
                'Zoom camera to the envelope
                pCamera.ZoomToRect(pEnvelope)
            Else
                'Translate screen coordinates into a 3D point
                pSceneGraph.Locate(pSceneGraph.ActiveViewer, pEnvelope.XMin + (pEnvelope.Width / 2), _
                   pEnvelope.YMin + (pEnvelope.Height / 2), esriScenePickMode.esriScenePickAll, True, _
                   pPoint, pOwner, pObject)

                'Set camera target
                pCamera.Target = pPoint

                'Get dimension of the scene viewer window
                Dim rect As Rectangle
                rect = New Rectangle

                If (GetWindowRect(m_pSceneHookHelper.ActiveViewer.hWnd, rect) = 0) Then
                    Return
                End If

                Dim dx, dy As Double
                dx = pEnvelope.Width
                dy = pEnvelope.Height

                'Determine zoom factor
                If (dx > 0 And dy > 0) Then
                    dx = dx / Math.Abs(rect.Right - rect.Left)
                    dy = dy / Math.Abs(rect.Top - rect.Bottom)

                    If (dx > dy) Then
                        pCamera.Zoom(dx)
                    Else
                        pCamera.Zoom(dy)
                    End If
                Else
                    pCamera.Zoom(0.75)
                End If
            End If
        End If

        'Redraw the scene viewer
        Dim pSceneViewer As ISceneViewer = CType(m_pSceneHookHelper.ActiveViewer, ISceneViewer)
        pSceneViewer.Redraw(True)

        m_bInUse = False
    End Sub

    Public Sub CreateEnvelope(ByVal X As Integer, ByVal Y As Integer, ByRef pEnvelope As IEnvelope)
        'Create envelope based upon the initial
        'and current mouse coordinates
        pEnvelope = New EnvelopeClass

        If (CType(m_lMouseX, Double) <= CType(X, Double)) Then
            pEnvelope.XMin = CType(m_lMouseX, Double)
            pEnvelope.XMax = CType(X, Double)
        Else
            pEnvelope.XMin = CType(X, Double)
            pEnvelope.XMax = CType(m_lMouseX, Double)
        End If

        If (CType(m_lMouseY, Double) <= CType(Y, Double)) Then

            pEnvelope.YMin = CType(m_lMouseY, Double)
            pEnvelope.YMax = CType(Y, Double)
        Else
            pEnvelope.YMin = CType(Y, Double)
            pEnvelope.YMax = CType(m_lMouseY, Double)
        End If

    End Sub

    Public Sub DrawRectangle(ByRef pEnvelope As IEnvelope)
        'Get the scene viewer
        Dim pSceneViewer As ISceneViewer = CType(m_pSceneHookHelper.ActiveViewer, ISceneViewer)

        'Redraw the rectangle
        pSceneViewer.Redraw(True)

        'GDI+ call to fill a rectangle with a hollow brush
        myGraphics.FillRectangle(m_brush, CType(pEnvelope.XMin, Integer), CType(pEnvelope.YMin, Integer), _
          CType(pEnvelope.Width, Integer), CType(pEnvelope.Height, Integer))

        'GDI+ call to draw a rectangle with a specified pen 
        myGraphics.DrawRectangle(m_pen, CType(pEnvelope.XMin, Integer), CType(pEnvelope.YMin, Integer), _
          CType(pEnvelope.Width, Integer), CType(pEnvelope.Height, Integer))
    End Sub
End Class


