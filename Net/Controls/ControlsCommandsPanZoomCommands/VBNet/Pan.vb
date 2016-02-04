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
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.GeomeTry
Imports ESRI.ArcGIS.Controls
Imports ESRI.ArcGIS.Display
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
    Public Const ClassId As String = "64653147-697B-4B0B-B728-9529BA8A2AAF"
    Public Const InterfaceId As String = "1B28A647-9A8B-4F92-A0C8-AFE121C847E1"
    Public Const EventsId As String = "E35275E7-89D3-43E1-96B8-C2088CE7C7AE"
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

    Private m_pHookHelper As IHookHelper
    Private m_pEnabled As Boolean
    Private m_focusScreenDisplay As IScreenDisplay
    Private m_PanOperation As Boolean
    Private m_check As Boolean
    Private m_cursorStart As System.Windows.Forms.Cursor
    Private m_cursorMove As System.Windows.Forms.Cursor

    ' A creatable COM class must have a Public Sub New() 
    ' with no parameters, otherwise, the class will not be 
    ' registered in the COM registry and cannot be created 
    ' via CreateObject.
    Public Sub New()
        MyBase.New()

        MyBase.m_category = "Sample_Pan_VBNET/Zoom"
        MyBase.m_caption = "Pan"
        MyBase.m_message = "Pans The Display by Grabbing"
        MyBase.m_toolTip = "Pan by Grab"
        MyBase.m_name = "Sample_Pan/Zoom_Pan"

        Dim res() As String = GetType(Pan).Assembly.GetManifestResourceNames()
        If res.GetLength(0) > 0 Then
            MyBase.m_bitmap = New System.Drawing.Bitmap(GetType(Pan).Assembly.GetManifestResourceStream("PanZoomVBNET.Pan.bmp"))
        End If
        m_pHookHelper = New HookHelperClass
    End Sub

    Public Overrides Sub OnCreate(ByVal hook As Object)
        m_pHookHelper.Hook = hook
        m_pEnabled = True
        m_check = False
        m_cursorStart = New System.Windows.Forms.Cursor(GetType(Pan).Assembly.GetManifestResourceStream("PanZoomVBNET.Hand.cur"))
        m_cursorMove = New System.Windows.Forms.Cursor(GetType(Pan).Assembly.GetManifestResourceStream("PanZoomVBNET.MoveHand.cur"))
    End Sub

    Public Overrides Sub OnMouseDown(ByVal Button As Integer, ByVal Shift As Integer, ByVal X As Integer, ByVal Y As Integer)

        If Button <> 1 Then
            Return
        End If

        Dim activeView As IActiveView = m_pHookHelper.ActiveView
        Dim point As IPoint
        'If in PageLayout view, find the closest map
        If Not TypeOf activeView Is IMap Then
            point = activeView.ScreenDisplay.DisplayTransformation.ToMapPoint(X, Y)
            Dim hitMap As IMap = activeView.HitTestMap(point)

            'Exit if no map found
            If hitMap Is Nothing Then
                Return
            End If

            If Not activeView Is m_pHookHelper.FocusMap Then
                activeView.FocusMap = hitMap
            End If

        End If
        'Start the pan operation
        point = m_pHookHelper.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(X, Y)
        m_focusScreenDisplay = getFocusDisplay()
        m_focusScreenDisplay.PanStart(point)

        m_PanOperation = True
    End Sub

    Private Function getFocusDisplay() As IScreenDisplay
        'Get the ScreenDisplay that has focus
        Dim activeView As IActiveView = m_pHookHelper.ActiveView

        activeView = CType(m_pHookHelper.ActiveView.FocusMap, IActiveView)  'layout view
        Return activeView.ScreenDisplay

    End Function

    Public Overrides Sub OnMouseMove(ByVal Button As Integer, ByVal Shift As Integer, ByVal X As Integer, ByVal Y As Integer)

        If Button <> 1 Then
            Return
        End If

        If Not m_PanOperation Then
            Return
        End If

        Dim point As IPoint = m_focusScreenDisplay.DisplayTransformation.ToMapPoint(X, Y)
        m_focusScreenDisplay.PanMoveTo(point)
    End Sub

    Public Overrides Sub OnMouseUp(ByVal Button As Integer, ByVal Shift As Integer, ByVal X As Integer, ByVal Y As Integer)

        If Button <> 1 Then
            Return
        End If

        If Not m_PanOperation Then
            Return
        End If

        Dim extent As IEnvelope = m_focusScreenDisplay.PanStop()

        'Check if user dragged a rectangle or just clicked.
        'If a rectangle was dragged, m_ipFeedback in non-NULL
        If Not extent Is Nothing Then
            m_focusScreenDisplay.DisplayTransformation.VisibleBounds = extent
            m_focusScreenDisplay.Invalidate(Nothing, True, CType(esriScreenCache.esriAllScreenCaches, Short))
        End If

        m_PanOperation = False
    End Sub

    Public Overrides Function Deactivate() As Boolean
        Return True
    End Function

    Public Overrides ReadOnly Property Cursor() As Integer
        Get
            If (m_PanOperation) Then
                Return m_cursorMove.Handle.ToInt32()
            Else
                Return m_cursorStart.Handle.ToInt32()
            End If
        End Get
    End Property
End Class


