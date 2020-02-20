'Copyright 2019 Esri

'Licensed under the Apache License, Version 2.0 (the "License");
'you may not use this file except in compliance with the License.
'You may obtain a copy of the License at

'    http://www.apache.org/licenses/LICENSE-2.0

'Unless required by applicable law or agreed to in writing, software
'distributed under the License is distributed on an "AS IS" BASIS,
'WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.

'See the License for the specific language governing permissions and
'limitations under the License.
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.Display
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Carto
Imports System.Runtime.InteropServices
Imports System.Drawing
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.ArcMapUI
Imports System.Windows.Forms

<ComClass(DrawGraphicLine.ClassId, DrawGraphicLine.InterfaceId, DrawGraphicLine.EventsId), _
 ProgId("CustomTool.DrawGraphicLine")> _
Public NotInheritable Class DrawGraphicLine
    Inherits BaseTool

#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "001c57ca-c292-459d-95a7-9984d78d0d93"
    Public Const InterfaceId As String = "8704b76e-9dcd-4222-979b-60bcb7831ff8"
    Public Const EventsId As String = "1ea60cb7-6138-4658-ba05-77c8b3283599"
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
    Private Shared Sub ArcGISCategoryRegistration(ByVal registerType As Type)
        Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
        MxCommands.Register(regKey)

    End Sub
    Private Shared Sub ArcGISCategoryUnregistration(ByVal registerType As Type)
        Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
        MxCommands.Unregister(regKey)

    End Sub

#End Region
#End Region


    Private m_application As IApplication

    ' A creatable COM class must have a Public Sub New() 
    ' with no parameters, otherwise, the class will not be 
    ' registered in the COM registry and cannot be created 
    ' via CreateObject.
    Public Sub New()
        MyBase.New()

        ' TODO: Define values for the public properties
        ' TODO: Define values for the public properties
        MyBase.m_category = "Walkthroughs"  'localizable text 
        MyBase.m_caption = "Draw Graphic Line"   'localizable text 
        MyBase.m_message = ""   'localizable text 
        MyBase.m_toolTip = "Draws a graphic line in the map window of ArcMap."  'localizable text 
        MyBase.m_name = "CustomTool_DrawGraphicLine"  'unique id, non-localizable (e.g. "MyCategory_ArcMapTool")

        Try
            'TODO: change resource name if necessary
            Dim bitmapResourceName As String = Me.GetType().Name + ".bmp"
            MyBase.m_bitmap = New Bitmap(Me.GetType(), bitmapResourceName)
            MyBase.m_cursor = New System.Windows.Forms.Cursor(Me.GetType(), Me.GetType().Name + ".cur")
        Catch ex As Exception
            System.Diagnostics.Trace.WriteLine(ex.Message, "Invalid Bitmap")
        End Try
    End Sub


    Public Overrides Sub OnCreate(ByVal hook As Object)
        If Not hook Is Nothing Then
            m_application = CType(hook, IApplication)

            'Disable if it is not ArcMap
            If TypeOf hook Is IMxApplication Then
                MyBase.m_enabled = True
            Else
                MyBase.m_enabled = False
            End If
        End If

        ' TODO:  Add other initialization code
    End Sub

    Public Overrides Sub OnClick()
        'TODO: Add DrawGraphicLine.OnClick implementation
    End Sub

    Public Overrides Sub OnMouseDown(ByVal Button As Integer, ByVal Shift As Integer, ByVal X As Integer, ByVal Y As Integer)
        'TODO: Add DrawGraphicLine.OnMouseDown implementation

        'Get the active view from the application object (ie. hook)
        Dim activeView As IActiveView = GetActiveViewFromArcMap(m_application)

        'Get the polyline object from the users mouse clicks
        Dim polyline As IPolyline = GetPolylineFromMouseClicks(activeView)

        'Make a color to draw the polyline
    Dim rgbColor As IRgbColor = New ESRI.ArcGIS.Display.RgbColor()
    rgbColor.Red = 255

        'Add the users drawn graphics as persistent on the map
        AddGraphicToMap(activeView.FocusMap, polyline, rgbColor, rgbColor)

        'Only redraw the portion of the active view that contains the graphics
        activeView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, Nothing, Nothing)

    End Sub

    Public Overrides Sub OnMouseMove(ByVal Button As Integer, ByVal Shift As Integer, ByVal X As Integer, ByVal Y As Integer)
        'TODO: Add DrawGraphicLine.OnMouseMove implementation
    End Sub

    Public Overrides Sub OnMouseUp(ByVal Button As Integer, ByVal Shift As Integer, ByVal X As Integer, ByVal Y As Integer)
        'TODO: Add DrawGraphicLine.OnMouseUp implementation
    End Sub

    '#### ArcGIS Snippets ####

#Region "Get ActiveView from ArcMap"

'''<summary>Get ActiveView from ArcMap</summary>
'''  
'''<param name="application">An IApplication interface that is the ArcMap application.</param>
'''   
'''<returns>An IActiveView interface.</returns>
'''   
'''<remarks></remarks>
Public Function GetActiveViewFromArcMap(ByVal application As IApplication) As IActiveView

  If application Is Nothing Then
    Return Nothing
  End If

  Dim mxDocument As IMxDocument = TryCast(application.Document, IMxDocument) ' Dynamic Cast
  Dim activeView As IActiveView = mxDocument.ActiveView

  Return activeView

End Function
#End Region

#Region "Get Polyline From Mouse Clicks"

'''<summary>
'''Create a polyline geometry object using the RubberBand.TrackNew method when a user click the mouse on the map control. 
'''</summary>
'''<param name="activeView">An ESRI.ArcGIS.Carto.IActiveView interface that will user will interace with to draw a polyline.</param>
'''<returns>An ESRI.ArcGIS.Geometry.IPolyline interface that is the polyline the user drew</returns>
'''<remarks>Double click the left mouse button to end tracking the polyline.</remarks>
Public Function GetPolylineFromMouseClicks(ByVal activeView As IActiveView) As IPolyline

  Dim screenDisplay As IScreenDisplay = activeView.ScreenDisplay

  Dim rubberBand As IRubberBand = New RubberLineClass
  Dim geometry As IGeometry = rubberBand.TrackNew(screenDisplay, Nothing)

  Dim polyline As IPolyline = CType(geometry, IPolyline)

  Return polyline

End Function
#End Region

#Region "Add Graphic to Map"

    '''<summary>Draw a specified graphic on the map using the supplied colors.</summary>
    '''      
    '''<param name="map">An IMap interface.</param>
    '''<param name="geometry">An IGeometry interface. It can be of the geometry type: esriGeometryPoint, esriGeometryPolyline, or esriGeometryPolygon.</param>
    '''<param name="rgbColor">An IRgbColor interface. The color to draw the geometry.</param>
    '''<param name="outlineRgbColor">An IRgbColor interface. For those geometry's with an outline it will be this color.</param>
    '''      
    '''<remarks></remarks>
    Public Sub AddGraphicToMap(ByVal map As IMap, ByVal geometry As IGeometry, ByVal rgbColor As IRgbColor, ByVal outlineRgbColor As IRgbColor)

        Dim graphicsContainer As IGraphicsContainer = CType(map, IGraphicsContainer) ' Explicit Cast
        Dim element As IElement = Nothing

        If (geometry.GeometryType) = esriGeometryType.esriGeometryPoint Then

            ' Marker symbols
            Dim simpleMarkerSymbol As ISimpleMarkerSymbol = New SimpleMarkerSymbolClass()
            simpleMarkerSymbol.Color = rgbColor
            simpleMarkerSymbol.Outline = True
            simpleMarkerSymbol.OutlineColor = outlineRgbColor
            simpleMarkerSymbol.Size = 15
            simpleMarkerSymbol.Style = esriSimpleMarkerStyle.esriSMSCircle

            Dim markerElement As IMarkerElement = New MarkerElementClass()
            markerElement.Symbol = simpleMarkerSymbol
            element = CType(markerElement, IElement) ' Explicit Cast

        ElseIf (geometry.GeometryType) = esriGeometryType.esriGeometryPolyline Then

            '  Line elements
            Dim simpleLineSymbol As ISimpleLineSymbol = New SimpleLineSymbolClass()
            simpleLineSymbol.Color = rgbColor
            simpleLineSymbol.Style = esriSimpleLineStyle.esriSLSSolid
            simpleLineSymbol.Width = 5

            Dim lineElement As ILineElement = New LineElementClass()
            lineElement.Symbol = simpleLineSymbol
            element = CType(lineElement, IElement) ' Explicit Cast

        ElseIf (geometry.GeometryType) = esriGeometryType.esriGeometryPolygon Then

            ' Polygon elements
            Dim simpleFillSymbol As ISimpleFillSymbol = New SimpleFillSymbolClass()
            simpleFillSymbol.Color = rgbColor
            simpleFillSymbol.Style = esriSimpleFillStyle.esriSFSForwardDiagonal
            Dim fillShapeElement As IFillShapeElement = New PolygonElementClass()
            fillShapeElement.Symbol = simpleFillSymbol
            element = CType(fillShapeElement, IElement) ' Explicit Cast

        End If

        If Not (element Is Nothing) Then

            element.Geometry = geometry
            graphicsContainer.AddElement(element, 0)

        End If

    End Sub
#End Region

End Class

