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

<ComClass(ZoomIn.ClassId, ZoomIn.InterfaceId, ZoomIn.EventsId)> _
Public NotInheritable Class ZoomIn
    Inherits BaseTool

#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "DAEDFD5C-7CFE-4EB2-AC50-E62CFEB0EABA"
    Public Const InterfaceId As String = "2EA141CC-94C4-48C7-912F-4786422D362A"
    Public Const EventsId As String = "1582A9D9-AE09-432F-8DB2-9BE2A0030B96"
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
    Private m_feedBack As INewEnvelopeFeedback
    Private m_point As IPoint
    Private m_isMouseDown As Boolean
    Private m_zoomInCur As System.Windows.Forms.Cursor
    Private m_moveZoomInCur As System.Windows.Forms.Cursor

  ' A creatable COM class must have a Public Sub New() 
  ' with no parameters, otherwise, the class will not be 
  ' registered in the COM registry and cannot be created 
  ' via CreateObject.
    Public Sub New()
        MyBase.New()

        MyBase.m_category = "Sample_Pan_VBNET/Zoom"
        MyBase.m_caption = "Zoom In"
        MyBase.m_message = "Zooms the Display In By Rectangle or Single Click"
        MyBase.m_toolTip = "Zoom In"
        MyBase.m_name = "Sample_Pan/Zoom_Zoom In"

        Dim res() As String = GetType(ZoomIn).Assembly.GetManifestResourceNames()
        If res.GetLength(0) > 0 Then
            MyBase.m_bitmap = New System.Drawing.Bitmap(GetType(ZoomIn).Assembly.GetManifestResourceStream("PanZoomVBNET.ZoomIn.bmp"))
        End If
        m_pHookHelper = New HookHelperClass
    End Sub

    Public Overrides Sub OnCreate(ByVal hook As Object)
        m_pHookHelper.Hook = hook
        m_zoomInCur = New System.Windows.Forms.Cursor(GetType(ZoomIn).Assembly.GetManifestResourceStream("PanZoomVBNET.ZoomIn.cur"))
        m_moveZoomInCur = New System.Windows.Forms.Cursor(GetType(ZoomIn).Assembly.GetManifestResourceStream("PanZoomVBNET.MoveZoomIn.cur"))
    End Sub

    Public Overrides ReadOnly Property Enabled() As Boolean
        Get
            If m_pHookHelper.FocusMap Is Nothing Then
                Return False
            End If

            Return True
        End Get
    End Property

    Public Overrides Sub OnMouseDown(ByVal Button As Integer, ByVal Shift As Integer, ByVal X As Integer, ByVal Y As Integer)

        If m_pHookHelper.ActiveView Is Nothing Then
            Return
        End If

        'If the active view is a page layout
        If TypeOf m_pHookHelper.ActiveView Is IPageLayout Then
            'Create a point in map coordinates
            Dim pPoint As IPoint = CType(m_pHookHelper.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(X, Y), IPoint)

            'Get the map if the point is within a data frame
            Dim pMap As IMap = m_pHookHelper.ActiveView.HitTestMap(pPoint)

            If pMap Is Nothing Then
                Return
            End If

            'Set the map to be the page layout's focus map
            If Not pMap Is m_pHookHelper.FocusMap Then
                m_pHookHelper.ActiveView.FocusMap = pMap
                m_pHookHelper.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, Nothing, Nothing)
            End If
        End If
        'Create a point in map coordinates
        Dim pActiveView As IActiveView = CType(m_pHookHelper.FocusMap, IActiveView)
        m_point = pActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(X, Y)

        m_isMouseDown = True
    End Sub

    Public Overrides Sub OnMouseMove(ByVal Button As Integer, ByVal Shift As Integer, ByVal X As Integer, ByVal Y As Integer)
        If Not m_isMouseDown Then
            Return
        End If

        'Get the focus map
        Dim pActiveView As IActiveView = CType(m_pHookHelper.FocusMap, IActiveView)

        'Start an envelope feedback
        If m_feedBack Is Nothing Then
            m_feedBack = New NewEnvelopeFeedbackClass
            m_feedBack.Display = pActiveView.ScreenDisplay
            m_feedBack.Start(m_point)
        End If

        'Move the envelope feedback
        m_feedBack.MoveTo(pActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(X, Y))
    End Sub

    Public Overrides Sub OnMouseUp(ByVal Button As Integer, ByVal Shift As Integer, ByVal X As Integer, ByVal Y As Integer)

        If Not m_isMouseDown Then
            Return
        End If

        'Get the focus map
        Dim pActiveView As IActiveView = CType(m_pHookHelper.FocusMap, IActiveView)

        'If an envelope has not been tracked
        Dim pEnvelope As IEnvelope

        If m_feedBack Is Nothing Then
            'Zoom in from mouse click
            pEnvelope = pActiveView.Extent
            pEnvelope.Expand(0.5, 0.5, True)
            pEnvelope.CenterAt(m_point)
        Else
            'Stop the envelope feedback
            pEnvelope = m_feedBack.Stop()

            'Exit if the envelope height or width is 0
            If pEnvelope.Width = 0 Or pEnvelope.Height = 0 Then
                m_feedBack = Nothing
                m_isMouseDown = False
            End If
        End If

        'Set the new extent
        pActiveView.Extent = pEnvelope

        'Refresh the active view
        pActiveView.Refresh()
        m_feedBack = Nothing
        m_isMouseDown = False
    End Sub

    Public Overrides Sub OnKeyDown(ByVal keyCode As Integer, ByVal Shift As Integer)
        If (m_isMouseDown) Then
            If (keyCode = 27) Then
                m_isMouseDown = False
                m_feedBack = Nothing
                m_pHookHelper.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewForeground, Nothing, Nothing)
            End If
        End If
    End Sub

    Public Overrides ReadOnly Property Cursor() As Integer
        Get
            If (m_isMouseDown) Then
                Return m_moveZoomInCur.Handle.ToInt32()
            Else
                Return m_zoomInCur.Handle.ToInt32()
            End If
        End Get
    End Property
End Class


