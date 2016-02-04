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
Imports ESRI.ArcGIS.Analyst3D
Imports ESRI.ArcGIS.GeomeTry
Imports ESRI.ArcGIS.Controls
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.ADF.CATIDs
Imports System.Runtime.InteropServices

<ComClass(Pan.ClassId, Pan.InterfaceId, Pan.EventsId)> _
Public NotInheritable Class Pan
    Inherits BaseTool

#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "9A2B497D-700A-4EF1-9AAF-A1C6FCFDA2A2"
    Public Const InterfaceId As String = "69AC1A19-FD8D-4951-86E6-5740D07088D6"
    Public Const EventsId As String = "FB8D601A-CD2F-4018-A192-0975CBA715EF"
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

    Private m_pCursorStop As System.Windows.Forms.Cursor
    Private m_pCursorMove As System.Windows.Forms.Cursor
    Private m_pSceneHookHelper As ISceneHookHelper
    Private m_lMouseX, m_lMouseY As Long
    Private m_bInUse As Boolean = False

  ' A creatable COM class must have a Public Sub New() 
  ' with no parameters, otherwise, the class will not be 
  ' registered in the COM registry and cannot be created 
  ' via CreateObject.
    Public Sub New()
        MyBase.New()

        MyBase.m_category = "Sample_SceneControl(VB.NET)"
        MyBase.m_caption = "Pan"
        MyBase.m_toolTip = "Pan"
        MyBase.m_name = "Sample_SceneControl(VB.NET)/Pan"
        MyBase.m_message = "Pans the scene"

        'Load resources
        Dim res() As String = GetType(Pan).Assembly.GetManifestResourceNames()
        If res.GetLength(0) > 0 Then
            MyBase.m_bitmap = New System.Drawing.Bitmap(GetType(Pan).Assembly.GetManifestResourceStream("SceneToolsVB.pan.bmp"))
        End If
        m_pCursorStop = New System.Windows.Forms.Cursor(GetType(Pan).Assembly.GetManifestResourceStream("SceneToolsVB.HAND.CUR"))
        m_pCursorMove = New System.Windows.Forms.Cursor(GetType(Pan).Assembly.GetManifestResourceStream("SceneToolsVB.movehand.cur"))

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

    Public Overrides Function Deactivate() As Boolean
        Return True
    End Function

    Public Overrides ReadOnly Property Cursor() As Integer
        Get
            If (m_bInUse) Then
                Return m_pCursorMove.Handle.ToInt32()
            Else
                Return m_pCursorStop.Handle.ToInt32()
            End If
        End Get
    End Property

    Public Overrides Sub OnMouseDown(ByVal Button As Integer, ByVal Shift As Integer, ByVal X As Integer, ByVal Y As Integer)
        'Initialize mouse coordinates
        m_bInUse = True
        m_lMouseX = X
        m_lMouseY = Y

        SetCapture(m_pSceneHookHelper.ActiveViewer.hWnd)
    End Sub

    Public Overrides Sub OnMouseMove(ByVal Button As Integer, ByVal Shift As Integer, ByVal X As Integer, ByVal Y As Integer)
        If (Not m_bInUse) Then
            Return
        End If

        If (X - m_lMouseX = 0 And Y - m_lMouseY = 0) Then
            Return
        End If

        'Create point with previous mouse coordinates
        Dim pStartPoint As IPoint
        pStartPoint = New PointClass
        pStartPoint.PutCoords(m_lMouseX, m_lMouseY)

        'Create point with a new mouse coordinates
        Dim pEndPoint As IPoint
        pEndPoint = New PointClass
        pEndPoint.PutCoords(X, Y)

        'Set mouse coordinates for the next OnMouseMove event
        m_lMouseX = X
        m_lMouseY = Y

        'Get scene viewer's camera
        Dim pCamera As ICamera = CType(m_pSceneHookHelper.Camera, ICamera)

        'Pan the camera
        pCamera.Pan(pStartPoint, pEndPoint)

        'Redraw the scene viewer
        Dim pSceneViewer As ISceneViewer = m_pSceneHookHelper.ActiveViewer
        pSceneViewer.Redraw(False)
    End Sub

    Public Overrides Sub OnMouseUp(ByVal Button As Integer, ByVal Shift As Integer, ByVal X As Integer, ByVal Y As Integer)
        If (Not m_bInUse) Then
            Return
        End If

        If (GetCapture(m_pSceneHookHelper.ActiveViewer.hWnd) <> 0) Then
            ReleaseCapture(m_pSceneHookHelper.ActiveViewer.hWnd)
        End If

        m_bInUse = False
    End Sub
End Class


