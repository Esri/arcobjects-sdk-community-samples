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
Imports System.Runtime.InteropServices
Imports System.Drawing
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.ADF.CATIDs
Imports System.Windows.Forms
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Display
Imports ESRI.ArcGIS.ArcMapUI

<ComClass(ToolSample.ClassId, ToolSample.InterfaceId, ToolSample.EventsId), _
 ProgId("ToolAndControlSampleVB.ToolSample")> _
Public NotInheritable Class ToolSample
    Inherits BaseTool
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
        MxCommands.Register(regKey)

    End Sub
    ''' <summary>
    ''' Required method for ArcGIS Component Category unregistration -
    ''' Do not modify the contents of this method with the code editor.
    ''' </summary>
    Private Shared Sub ArcGISCategoryUnregistration(ByVal registerType As Type)
        Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
        MxCommands.Unregister(regKey)

    End Sub

#End Region
#End Region

#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "99b863f5-add8-4a3d-9a3c-72092553263c"
    Public Const InterfaceId As String = "fdf22bda-0294-43e6-aa74-2b0e92890ed1"
    Public Const EventsId As String = "9b627f0d-f7d5-4a13-a843-36f3387a4e68"
#End Region

    Private m_application As IApplication
    Private m_circleFeedback As INewCircleFeedback
    Private m_centerPoint, m_lastPoint As IPoint

    Private m_geomEnv As IGeometryEnvironment
    Private m_feedbackSymbol As ISimpleLineSymbol

    ' A creatable COM class must have a Public Sub New() 
    ' with no parameters, otherwise, the class will not be 
    ' registered in the COM registry and cannot be created 
    ' via CreateObject.
    Public Sub New()
        MyBase.New()

        MyBase.m_category = ".NET Samples"
        MyBase.m_caption = "Feedback Tool (VB.Net)"
        MyBase.m_message = "Circle feedback with color changes when pressing Ctrl key (VB.Net)"
        MyBase.m_toolTip = "Circle feedback (VB.Net)"
        MyBase.m_name = "VBNETSamples_ToolSampleFeedback"

        Try
            Dim bitmapResourceName As String = Me.GetType().Name + ".bmp"
            MyBase.m_bitmap = New Bitmap(Me.GetType(), bitmapResourceName)
            MyBase.m_cursor = Cursors.Cross
        Catch ex As Exception
            System.Diagnostics.Trace.WriteLine(ex.Message, "Invalid Bitmap")
        End Try
    End Sub


    Public Overrides Sub OnCreate(ByVal hook As Object)
        m_application = TryCast(hook, IApplication)

        'set up
        m_geomEnv = New GeometryEnvironmentClass()

        m_feedbackSymbol = New SimpleLineSymbolClass()
        DirectCast(m_feedbackSymbol, ISymbol).ROP2 = esriRasterOpCode.esriROPNotXOrPen
        Dim solidColor As IRgbColor = New RgbColorClass()
        solidColor.Red = 255
        m_feedbackSymbol.Color = solidColor
        m_feedbackSymbol.Width = 2
    End Sub


    Public Overrides Sub OnMouseDown(ByVal Button As Integer, ByVal Shift As Integer, ByVal X As Integer, ByVal Y As Integer)
        If Button <> 1 Then Return 'exit sub

        If m_circleFeedback Is Nothing Then
            m_circleFeedback = New NewCircleFeedbackClass()
            m_circleFeedback.Symbol = DirectCast(m_feedbackSymbol, ISymbol)

            'Use AppDisplay so it will work on magnifying windows
            Dim disp As IScreenDisplay = DirectCast(m_application, IMxApplication).Display
            m_centerPoint = disp.DisplayTransformation.ToMapPoint(X, Y)

            m_circleFeedback.Display = disp
            m_circleFeedback.Start(m_centerPoint)
        Else
            Reset(False)
        End If
    End Sub

    Public Overrides Sub OnMouseMove(ByVal Button As Integer, ByVal Shift As Integer, ByVal X As Integer, ByVal Y As Integer)
        If m_circleFeedback IsNot Nothing Then
            Dim mxDoc As IMxDocument = TryCast(m_application.Document, IMxDocument)

            'Cache last location just in case color change is requested in keydown to
            'refresh the feedback
            Dim disp As IScreenDisplay = DirectCast(m_application, IMxApplication).Display
            m_lastPoint = disp.DisplayTransformation.ToMapPoint(X, Y)

            'Move feedback
            m_circleFeedback.MoveTo(m_lastPoint)

            'Calculate angle to determine cursor
            Dim constructAngle As IConstructAngle = DirectCast(m_geomEnv, IConstructAngle)
            Dim angleLine As ILine = New LineClass()
            angleLine.PutCoords(m_centerPoint, m_lastPoint)
            Dim angle As Double = constructAngle.ConstructLine(angleLine)
            SetCursor(angle)
        End If
    End Sub

    Public Overrides Function Deactivate() As Boolean
        'Option 1: Always allow to deactivate. 
        'Reset things before deactivate (easier and recommended)
        Reset(True)
        Return True

        'Option 2: Do not allow deactivate in the middle of the feedback.
        'Return (m_circleFeedback Is Nothing)
    End Function

    Public Overrides Sub OnKeyDown(ByVal keyCode As Integer, ByVal Shift As Integer)
        'Change feedback symbology
        If Shift = 2 And m_circleFeedback IsNot Nothing Then 'Ctrl
            m_circleFeedback.Stop()

            'change color randomly
            Dim solidColor As IRgbColor = New RgbColorClass()
            Dim num As Random = New Random()
            solidColor.Red = num.Next(255)
            solidColor.Green = num.Next(255)
            solidColor.Blue = num.Next(255)
            m_feedbackSymbol.Color = solidColor

            'Restart the feedback with newly assigned color
            m_circleFeedback.Start(m_centerPoint)
            m_circleFeedback.MoveTo(m_lastPoint)
        End If
    End Sub

    Public Overrides Sub Refresh(ByVal hDC As Integer)
        If m_circleFeedback IsNot Nothing Then
            m_circleFeedback.Refresh(hDC)
        End If
    End Sub

    Private Sub Reset(ByVal resetColor As Boolean)
        If m_circleFeedback IsNot Nothing Then
            m_circleFeedback.Stop()
            m_circleFeedback = Nothing
            MyBase.m_cursor = Cursors.Cross
            If resetColor Then
                'Reset symbol color to red
                Dim solidColor As IRgbColor = New RgbColorClass()
                solidColor.Red = 255
                m_feedbackSymbol.Color = solidColor
            End If
        End If
    End Sub
    Private Sub SetCursor(ByVal radianAngle As Double)
        Dim absAngle As Double = Math.Abs(radianAngle)
        If radianAngle < 0 Then 'Southern portion
            If (absAngle >= 0 And absAngle < Math.PI / 8) Then 'E
                MyBase.m_cursor = Cursors.PanEast
            ElseIf (absAngle >= Math.PI / 8 And absAngle < 3 * Math.PI / 8) Then 'SE
                MyBase.m_cursor = Cursors.PanSE
            ElseIf (absAngle >= 3 * Math.PI / 8 And absAngle < 5 * Math.PI / 8) Then 'S
                MyBase.m_cursor = Cursors.PanSouth
            ElseIf (absAngle >= 5 * Math.PI / 8 And absAngle < 7 * Math.PI / 8) Then 'SW
                MyBase.m_cursor = Cursors.PanSW
            Else 'W
                MyBase.m_cursor = Cursors.PanWest
            End If
        Else 'northern
            If (absAngle >= 0 And absAngle < Math.PI / 8) Then 'E
                MyBase.m_cursor = Cursors.PanEast
            ElseIf (absAngle >= Math.PI / 8 And absAngle < 3 * Math.PI / 8) Then 'NE
                MyBase.m_cursor = Cursors.PanNE
            ElseIf (absAngle >= 3 * Math.PI / 8 And absAngle < 5 * Math.PI / 8) Then 'N
                MyBase.m_cursor = Cursors.PanNorth
            ElseIf (absAngle >= 5 * Math.PI / 8 And absAngle < 7 * Math.PI / 8) Then 'NW
                MyBase.m_cursor = Cursors.PanNW
            Else 'W
                MyBase.m_cursor = Cursors.PanWest
            End If
        End If
    End Sub

End Class


