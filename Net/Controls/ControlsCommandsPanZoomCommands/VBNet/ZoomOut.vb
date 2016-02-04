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

<ComClass(ZoomOut.ClassId, ZoomOut.InterfaceId, ZoomOut.EventsId)> _
Public NotInheritable Class ZoomOut
    Inherits BaseTool

#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "1A8E3B27-4AC0-4A79-919E-6805A35986F8"
    Public Const InterfaceId As String = "72FC4985-F481-46F2-9640-1B885AA70934"
    Public Const EventsId As String = "488ECCFE-6B4B-40F4-90DB-B4468295E9C9"
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
    Private m_zoomOutCur As System.Windows.Forms.Cursor
    Private m_moveZoomOutCur As System.Windows.Forms.Cursor

  ' A creatable COM class must have a Public Sub New() 
  ' with no parameters, otherwise, the class will not be 
  ' registered in the COM registry and cannot be created 
  ' via CreateObject.
    Public Sub New()
        MyBase.New()

        MyBase.m_category = "Sample_Pan_VBNET/Zoom"
        MyBase.m_caption = "Zoom Out"
        MyBase.m_message = "Zooms the Display Out By Rectangle or Single Click"
        MyBase.m_toolTip = "Zoom Out"
        MyBase.m_name = "Sample_Pan/Zoom_Zoom Out"

        Dim res() As String = GetType(ZoomOut).Assembly.GetManifestResourceNames()
        If res.GetLength(0) > 0 Then
            MyBase.m_bitmap = New System.Drawing.Bitmap(GetType(ZoomOut).Assembly.GetManifestResourceStream("PanZoomVBNET.ZoomOut.bmp"))
        End If
        m_pHookHelper = New HookHelperClass
    End Sub

    Public Overrides Sub OnCreate(ByVal hook As Object)
        m_pHookHelper.Hook = hook
        m_zoomOutCur = New System.Windows.Forms.Cursor(GetType(ZoomOut).Assembly.GetManifestResourceStream("PanZoomVBNET.ZoomOut.cur"))
        m_moveZoomOutCur = New System.Windows.Forms.Cursor(GetType(ZoomOut).Assembly.GetManifestResourceStream("PanZoomVBNET.MoveZoomOut.cur"))
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

        Dim pEnvelope As IEnvelope
        Dim pFeedEnvelope As IEnvelope

        Dim NewWidth As Double, NewHeight As Double

        'Get the focus map
        Dim pActiveView As IActiveView = CType(m_pHookHelper.FocusMap, IActiveView)

        'If an envelope has not been tracked
        If m_feedBack Is Nothing Then
            'Zoom out from the mouse click
            pEnvelope = pActiveView.Extent
            pEnvelope.Expand(2, 2, True)
            pEnvelope.CenterAt(m_point)
        Else
            'Stop the envelope feedback
            pFeedEnvelope = m_feedBack.Stop()

            'Exit if the envelope height or width is 0
            If pFeedEnvelope.Width = 0 Or pFeedEnvelope.Height = 0 Then
                m_feedBack = Nothing
                m_isMouseDown = False
            End If

            NewWidth = pActiveView.Extent.Width * (pActiveView.Extent.Width / pFeedEnvelope.Width)
            NewHeight = pActiveView.Extent.Height * (pActiveView.Extent.Height / pFeedEnvelope.Height)

            'Set the new extent coordinates
            pEnvelope = New EnvelopeClass
            pEnvelope.PutCoords(pActiveView.Extent.XMin - ((pFeedEnvelope.XMin - pActiveView.Extent.XMin) * (pActiveView.Extent.Width / pFeedEnvelope.Width)), _
            pActiveView.Extent.YMin - ((pFeedEnvelope.YMin - pActiveView.Extent.YMin) * (pActiveView.Extent.Height / pFeedEnvelope.Height)), _
            (pActiveView.Extent.XMin - ((pFeedEnvelope.XMin - pActiveView.Extent.XMin) * (pActiveView.Extent.Width / pFeedEnvelope.Width))) + NewWidth, _
            (pActiveView.Extent.YMin - ((pFeedEnvelope.YMin - pActiveView.Extent.YMin) * (pActiveView.Extent.Height / pFeedEnvelope.Height))) + NewHeight)
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
                Return m_moveZoomOutCur.Handle.ToInt32()
            Else
                Return m_zoomOutCur.Handle.ToInt32()
            End If
        End Get
    End Property
End Class


