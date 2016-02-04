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
Option Explicit On 

Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Display
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.Controls
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.ADF.CATIDs
Imports System.Runtime.InteropServices
Imports ESRI.ArcGIS.esriSystem

<ComClass(SelectFeatures.ClassId, SelectFeatures.InterfaceId, SelectFeatures.EventsId)> _
Public NotInheritable Class SelectFeatures

    Inherits BaseTool

    Private m_pHookHelper As IHookHelper
    Private m_pCursorMove As System.Windows.Forms.Cursor

#Region "COM GUIDs"
    'These GUIDs provide the COM identity for this class and its COM interfaces. 
    'If you change them, existing clients will no longer be able to access the class.
    Public Const ClassId As String = "310bd6df-9422-44c0-9158-f8e63aa4254e"
    Public Const InterfaceId As String = "8cca1681-2c5c-4c06-9760-fd9f3ca70908"
    Public Const EventsId As String = "ab0d2a64-3473-477b-a28d-93dc5e3820cc"
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

    Public Sub New()
        MyBase.New()

        'Create an IHookHelper object
        m_pHookHelper = New HookHelperClass

        'Set the tool properties
        MyBase.m_caption = "Select Features"
        MyBase.m_category = "Sample_Select(VB.NET)"
        MyBase.m_name = "Sample_Select(VB.NET)_Select Features"
        MyBase.m_message = "Selects Features By Rectangle Or Single Click"
        MyBase.m_toolTip = "Selects Features"
        MyBase.m_deactivate = True
        MyBase.m_bitmap = New System.Drawing.Bitmap(GetType(SelectFeatures).Assembly.GetManifestResourceStream(GetType(SelectFeatures), "SelectFeatures.bmp"))
        m_pCursorMove = New System.Windows.Forms.Cursor(GetType(SelectFeatures).Assembly.GetManifestResourceStream(GetType(SelectFeatures), "SelectFeatures.cur"))
    End Sub

    Public Overrides Sub OnCreate(ByVal hook As Object)
        m_pHookHelper.Hook = hook
    End Sub

    Public Overrides ReadOnly Property Enabled() As Boolean
        Get
            If (m_pHookHelper.FocusMap Is Nothing) Then Exit Property
            Return (m_pHookHelper.FocusMap.LayerCount > 0)
        End Get
    End Property

    Public Overrides Sub OnMouseDown(ByVal Button As Integer, ByVal Shift As Integer, ByVal X As Integer, ByVal Y As Integer)

        Dim map As IMap
        Dim clickedPoint As IPoint = m_pHookHelper.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(X, Y)

        'If ActiveView is a PageLayout
        If TypeOf m_pHookHelper.ActiveView Is IPageLayout Then

            'See whether the mouse has been clicked over a Map in the PageLayout
            map = m_pHookHelper.ActiveView.HitTestMap(clickedPoint)

            'If mouse click isn't over a Map exit
            If map Is Nothing Then Exit Sub

            'Ensure the Map is the FocusMap
            If Not map Is m_pHookHelper.FocusMap Then
                m_pHookHelper.ActiveView.FocusMap = map
            End If

            'Still need to convert the clickedPoint into map units using the map's IActiveView 
            clickedPoint = CType(map, IActiveView).ScreenDisplay.DisplayTransformation.ToMapPoint(X, Y)

        Else 'Or ActiveView is a Map
            map = m_pHookHelper.FocusMap
        End If

        Dim activeView As IActiveView = CType(map, IActiveView)
        Dim rubberEnv As IRubberBand = New RubberEnvelopeClass()
        Dim geom As IGeometry = rubberEnv.TrackNew(activeView.ScreenDisplay, Nothing)
        Dim area As IArea = CType(geom, IArea)

        'Extra logic to cater for the situation where the user simply clicks a point on the map
        'or where envelope is so small area is zero
        If (geom.IsEmpty = True) OrElse (area.Area = 0) Then

            'create a new envelope
            Dim tempEnv As IEnvelope = New EnvelopeClass()

            'create a small rectangle
            Dim RECT As ESRI.ArcGIS.esriSystem.tagRECT = New tagRECT()
            RECT.bottom = 0
            RECT.left = 0
            RECT.right = 5
            RECT.top = 5

            'transform rectangle into map units and apply to the tempEnv envelope
            Dim dispTrans As IDisplayTransformation = activeView.ScreenDisplay.DisplayTransformation
            dispTrans.TransformRect(tempEnv, RECT, 4) 'esriDisplayTransformationEnum.esriTransformToMap);
            tempEnv.CenterAt(clickedPoint)
            geom = CType(tempEnv, IGeometry)
        End If

        'Set the spatial reference of the search geometry to that of the Map
        Dim spatialReference As ISpatialReference = map.SpatialReference
        geom.SpatialReference = spatialReference

        map.SelectByShape(geom, Nothing, False)
        activeView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, activeView.Extent)

    End Sub

    Public Overrides ReadOnly Property Cursor() As Integer
        Get
            Return m_pCursorMove.Handle.ToInt32
        End Get
    End Property

End Class
