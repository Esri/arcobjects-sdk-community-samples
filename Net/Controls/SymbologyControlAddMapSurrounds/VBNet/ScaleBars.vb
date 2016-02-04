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
Imports ESRI.ArcGIS.Controls
Imports ESRI.ArcGIS.Display
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.ADF.CATIDs
Imports System.Runtime.InteropServices

Public NotInheritable Class CreateScaleBar
    Inherits BaseTool
    Private m_HookHelper As IHookHelper
    Private m_Feedback As INewEnvelopeFeedback
    Private m_Point As IPoint
    Private m_InUse As Boolean

    'Windows API functions to capture mouse and keyboard
    'input to a window when the mouse is outside the window
    Private Declare Function SetCapture Lib "user32" (ByVal hWnd As Integer) As Integer
    Private Declare Function GetCapture Lib "user32" () As Integer
    Private Declare Function ReleaseCapture Lib "user32" () As Integer

#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "7B8442E6-19E9-4ac5-B970-0A2130B560B3"
    Public Const InterfaceId As String = "8186E496-BE0C-40c3-AF96-C18736EEC826"
    Public Const EventsId As String = "CCB3F85E-DA13-478c-A81C-BBABC27ED41D"
#End Region

#Region "Component Category Registration"
    <ComRegisterFunction(), ComVisibleAttribute(False)> _
    Public Shared Sub RegisterFunction(ByVal regKey As String)
        ControlsCommands.Register(regKey)
    End Sub

    <ComUnregisterFunction(), ComVisibleAttribute(False)> _
    Public Shared Sub UnregisterFunction(ByVal regKey As String)
        ControlsCommands.Unregister(regKey)
    End Sub
#End Region

    'A creatable COM class must have a Public Sub New() with no parameters, 
    'otherwise, the class will not be registered in the COM registry and cannot 
    'be created via CreateObject.
    Public Sub New()
        MyBase.New()

        'Create an IHookHelper object
        m_HookHelper = New HookHelperClass

        'Set the tool properties
        MyBase.m_bitmap = New System.Drawing.Bitmap(GetType(CreateScaleBar).Assembly.GetManifestResourceStream(GetType(CreateScaleBar), "scalebar.bmp"))
        MyBase.m_caption = "ScaleBar"
        MyBase.m_category = "myCustomCommands(VBNet)"
        MyBase.m_message = "Add a scale bar map surround"
        MyBase.m_name = "myCustomCommands(VBNet)_ScaleBar"
        MyBase.m_toolTip = "Add a scale bar"
        MyBase.m_deactivate = True
    End Sub

    Public Overrides Sub OnCreate(ByVal hook As Object)
        m_HookHelper.Hook = hook
    End Sub

    Public Overrides Sub OnMouseDown(ByVal Button As Integer, ByVal Shift As Integer, ByVal X As Integer, ByVal Y As Integer)
        'Create a point in map coordinates
        m_Point = m_HookHelper.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(X, Y)

        'Start capturing mouse events
        SetCapture(m_HookHelper.ActiveView.ScreenDisplay.hWnd)

        m_InUse = True
    End Sub

    Public Overrides Sub OnMouseMove(ByVal Button As Integer, ByVal Shift As Integer, ByVal X As Integer, ByVal Y As Integer)
        If (Not m_InUse) Then Exit Sub

        'Start an envelope feedback
        If (m_Feedback Is Nothing) Then
            m_Feedback = New NewEnvelopeFeedbackClass
            m_Feedback.Display = m_HookHelper.ActiveView.ScreenDisplay
            m_Feedback.Start(m_Point)
        End If

        'Move the envelope feedback
        m_Feedback.MoveTo(m_HookHelper.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(X, Y))

    End Sub

    Public Overrides Sub OnMouseUp(ByVal Button As Integer, ByVal Shift As Integer, ByVal X As Integer, ByVal Y As Integer)
        If (Not m_InUse) Then Exit Sub

        'Stop capturing mouse events
        If GetCapture = m_HookHelper.ActiveView.ScreenDisplay.hWnd Then
            ReleaseCapture()
        End If

        'If an envelope has not been tracked or its height/width is 0
        If (m_Feedback Is Nothing) Then
            m_Feedback = Nothing
            m_InUse = False
            Exit Sub
        End If
        Dim envelope As IEnvelope = m_Feedback.Stop
        If (envelope.IsEmpty) Or (envelope.Width = 0) Or (envelope.Height = 0) Then
            m_Feedback = Nothing
            m_InUse = False
            Exit Sub
        End If

        'Create the form with the SymbologyControl
        Dim symbolForm As New frmSymbol
        'Get the IStyleGalleryItem
        Dim styleGalleryItem As IStyleGalleryItem
        styleGalleryItem = symbolForm.GetItem(esriSymbologyStyleClass.esriStyleClassScaleBars)
        'Release the form
        symbolForm.Dispose()
        If styleGalleryItem Is Nothing Then Exit Sub

        'Get the map frame of the focus map
        Dim mapFrame As IMapFrame
        mapFrame = m_HookHelper.ActiveView.GraphicsContainer.FindFrame(m_HookHelper.ActiveView.FocusMap)

        'Create a map surround frame
        Dim mapSurroundFrame As IMapSurroundFrame = New MapSurroundFrameClass
        'Set its map frame and map surround
        mapSurroundFrame.MapFrame = mapFrame
        mapSurroundFrame.MapSurround = styleGalleryItem.Item

        'QI to IElement and set its geometry
        Dim element As IElement = mapSurroundFrame
        element.Geometry = envelope

        'Add the element to the graphics container
        m_HookHelper.ActiveView.GraphicsContainer.AddElement(mapSurroundFrame, 0)
        'Refresh
        m_HookHelper.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, mapSurroundFrame, Nothing)

        m_Feedback = Nothing
        m_InUse = False
    End Sub
End Class

