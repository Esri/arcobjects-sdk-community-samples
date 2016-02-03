Imports ESRI.ArcGIS.Analyst3D
Imports ESRI.ArcGIS.GeomeTry
Imports ESRI.ArcGIS.Controls
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.ADF.CATIDs
Imports System.Runtime.InteropServices

<ComClass(ZoomInOut.ClassId, ZoomInOut.InterfaceId, ZoomInOut.EventsId)> _
Public NotInheritable Class ZoomInOut
    Inherits BaseTool

#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "8AB8145F-A325-43CA-9D43-B3D1BCAD0010"
    Public Const InterfaceId As String = "E4FC28D2-DFFC-4ADE-855A-8C5991CD37D1"
    Public Const EventsId As String = "2B16B9AF-D52A-4821-93ED-10F8F7FFA4B7"
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

    Private m_pCursor As System.Windows.Forms.Cursor
    Private m_pSceneHookHelper As ISceneHookHelper
    Private m_lMouseX As Long, m_lMouseY As Long
    Private m_bInUse As Boolean

  ' A creatable COM class must have a Public Sub New() 
  ' with no parameters, otherwise, the class will not be 
  ' registered in the COM registry and cannot be created 
  ' via CreateObject.
    Public Sub New()
        MyBase.New()

        MyBase.m_category = "Sample_SceneControl(VB.NET)"
        MyBase.m_caption = "Zoom In/Out"
        MyBase.m_toolTip = "Zoom In/Out"
        MyBase.m_name = "Sample_SceneControl(VB.NET)/Zoom In/Out"
        MyBase.m_message = "Dynamically zooms in and out on the scene"

        'Load resources
        Dim res() As String = GetType(ZoomInOut).Assembly.GetManifestResourceNames()
        If res.GetLength(0) > 0 Then
            MyBase.m_bitmap = New System.Drawing.Bitmap(GetType(ZoomInOut).Assembly.GetManifestResourceStream("SceneToolsVB.ZoomInOut.bmp"))
        End If
        m_pCursor = New System.Windows.Forms.Cursor(GetType(ZoomInOut).Assembly.GetManifestResourceStream("SceneToolsVB.ZOOMINOUT.CUR"))

        m_pSceneHookHelper = New SceneHookHelperClass
    End Sub

    Public Overrides Sub OnCreate(ByVal hook As Object)
        m_pSceneHookHelper.Hook = hook
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

    Public Overrides Sub OnMouseDown(ByVal Button As Integer, ByVal Shift As Integer, ByVal X As Integer, ByVal Y As Integer)
        'Initialize mouse coordinates
        m_bInUse = True
        m_lMouseX = X
        m_lMouseY = Y

        SetCapture(m_pSceneHookHelper.ActiveViewer.hWnd)
    End Sub

    Public Overrides Sub OnMouseMove(ByVal Button As Integer, ByVal Shift As Integer, ByVal X As Integer, ByVal Y As Integer)
        If Not m_bInUse Then
            Return
        End If

        'Differences between previous and current mouse coordinates
        Dim dx, dy As Long
        dx = X - m_lMouseX
        dy = Y - m_lMouseY

        If (dx = 0 And dy = 0) Then
            Return
        End If

        'Get the scene viewer's camera
        Dim pCamera As ICamera = CType(m_pSceneHookHelper.Camera, ICamera)

        'Zoom camera
        If (dy < 0) Then
            pCamera.Zoom(1.1)
        ElseIf (dy > 0) Then
            pCamera.Zoom(0.9)
        End If

        'Set mouse coordinates for the next OnMouseMove event
        m_lMouseX = X
        m_lMouseY = Y

        'Redraw the scene viewer
        Dim pSceneViewer As ISceneViewer = CType(m_pSceneHookHelper.ActiveViewer, ISceneViewer)
        pSceneViewer.Redraw(True)
    End Sub

    Public Overrides Sub OnMouseUp(ByVal Button As Integer, ByVal Shift As Integer, ByVal X As Integer, ByVal Y As Integer)
        If Not m_bInUse Then
            Return
        End If

        If (GetCapture(m_pSceneHookHelper.ActiveViewer.hWnd) <> 0) Then
            ReleaseCapture(m_pSceneHookHelper.ActiveViewer.hWnd)
        End If

        m_bInUse = False
    End Sub
End Class


