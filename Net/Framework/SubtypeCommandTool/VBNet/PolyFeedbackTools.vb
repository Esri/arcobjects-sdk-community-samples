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
Imports System.Runtime.InteropServices
Imports System.Drawing
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.ArcMapUI
Imports System.Windows.Forms
Imports ESRI.ArcGIS.SystemUI
Imports ESRI.ArcGIS.Display
Imports ESRI.ArcGIS.Geometry

<ComClass(PolyFeedbackTools.ClassId, PolyFeedbackTools.InterfaceId, PolyFeedbackTools.EventsId), _
 ProgId("CommandSubtypeVB.PolyFeedbackTools")> _
Public NotInheritable Class PolyFeedbackTools
    Inherits BaseTool
    Implements ICommandSubType

#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "5846f005-22f6-4ee6-b563-75a08b0d219e"
    Public Const InterfaceId As String = "a86f93a9-867f-4caa-aa6b-ffa8ce132ba4"
    Public Const EventsId As String = "6f6ee1b5-8c27-403e-94e0-5e975511d6a2"
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

    Private m_polygonFeedback As INewPolygonFeedback
    Private m_screenDisplay As IScreenDisplay
    Private m_maxSides As Integer   'max allowed sides
    Private m_currentSides As Integer 'number of sides added to the feedback

    ' A creatable COM class must have a Public Sub New() 
    ' with no parameters, otherwise, the class will not be 
    ' registered in the COM registry and cannot be created 
    ' via CreateObject.
    Public Sub New()
        MyBase.New()

        'Set up common properties
        MyBase.m_category = ".NET Samples"
        MyBase.m_cursor = Cursors.Cross
    End Sub

    Public Overrides Sub OnCreate(ByVal hook As Object)
        If Not hook Is Nothing Then
            m_application = TryCast(hook, IApplication)

            If TypeOf hook Is IMxApplication Then
                MyBase.m_enabled = True
            Else
                MyBase.m_enabled = False
            End If
        End If
    End Sub

    Public Overrides Sub OnClick()
        'prepare screen display for mouse interaction
        Dim mxDoc As IMxDocument = DirectCast(m_application.Document, IMxDocument)
        m_screenDisplay = mxDoc.ActiveView.ScreenDisplay
    End Sub

    Public Overrides Sub OnMouseDown(ByVal Button As Integer, ByVal Shift As Integer, ByVal X As Integer, ByVal Y As Integer)

        If Button = 1 Then
            Dim cursorPoint As IPoint = m_screenDisplay.DisplayTransformation.ToMapPoint(X, Y)

            If m_polygonFeedback Is Nothing Then
                m_currentSides = 0
                m_polygonFeedback = New NewPolygonFeedbackClass()
                m_polygonFeedback.Display = m_screenDisplay

                m_polygonFeedback.Start(cursorPoint)
            Else
                m_polygonFeedback.AddPoint(cursorPoint)
            End If

            m_currentSides += 1
            If m_currentSides = m_maxSides Then 'Finish
                Dim polygon As IPolygon = m_polygonFeedback.Stop()

                'Report area on status bar
                Dim feedBackArea As IArea = DirectCast(polygon, IArea)
                m_application.StatusBar.Message(0) = "Feedback area = " + Math.Abs(feedBackArea.Area).ToString()

                m_polygonFeedback = Nothing
            Else
                'Report vertex remaining
                m_application.StatusBar.Message(0) = String.Format("Feedback: {0} point(s) remaining", m_maxSides - m_currentSides)
            End If

        End If

    End Sub

    Public Overrides Sub OnMouseMove(ByVal Button As Integer, ByVal Shift As Integer, ByVal X As Integer, ByVal Y As Integer)
        If m_polygonFeedback IsNot Nothing Then
            Dim cursorPoint As IPoint = m_screenDisplay.DisplayTransformation.ToMapPoint(X, Y)
            m_polygonFeedback.MoveTo(cursorPoint)
        End If
    End Sub

    Public Overrides Function Deactivate() As Boolean
        'Clean up
        If m_polygonFeedback IsNot Nothing Then
            m_polygonFeedback.Stop()
            m_polygonFeedback = Nothing
        End If
        m_screenDisplay = Nothing

        Return True
    End Function

#Region "ICommandSubType implementation"
    Public Function GetCount() As Integer Implements ESRI.ArcGIS.SystemUI.ICommandSubType.GetCount
        Return 3
    End Function

    Public Sub SetSubType(ByVal SubType As Integer) Implements ESRI.ArcGIS.SystemUI.ICommandSubType.SetSubType
        'Set up bitmap, caption, tooltip etc.
        If MyBase.Bitmap = 0 Then
            Select Case SubType
                Case 1
                    MyBase.m_bitmap = My.Resources.FeedBack3
                Case 2
                    MyBase.m_bitmap = My.Resources.FeedBack4
                Case 3
                    MyBase.m_bitmap = My.Resources.FeedBack5
            End Select
        End If

        MyBase.m_name = String.Format("VBNETSamples_SubTypeTool{0}", SubType)

        m_maxSides = SubType + 2 '3, 4 or 5 sides
        MyBase.m_caption = String.Format("{0} sides feedback (VB.Net)", m_maxSides)
        MyBase.m_toolTip = String.Format("{0} sides feedback", m_maxSides)
        MyBase.m_message = String.Format("Tool demonstrating {0} sides polygon feedback", m_maxSides)
    End Sub
#End Region
End Class

