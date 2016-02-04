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

<ComClass(SetObserver.ClassId, SetObserver.InterfaceId, SetObserver.EventsId)> _
Public NotInheritable Class SetObserver
    Inherits BaseTool

#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "0120A73E-6E11-4B2A-9F57-B095EAAA4180"
    Public Const InterfaceId As String = "CFCD3896-B61A-4B8F-8B64-95D1229AB082"
    Public Const EventsId As String = "162DEDA7-F4C0-4B51-8F1B-05151A10D704"
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

    Private m_pCursor As System.Windows.Forms.Cursor
    Private m_pSceneHookHelper As ISceneHookHelper

  ' A creatable COM class must have a Public Sub New() 
  ' with no parameters, otherwise, the class will not be 
  ' registered in the COM registry and cannot be created 
  ' via CreateObject.
    Public Sub New()
        MyBase.New()

        MyBase.m_category = "Sample_SceneControl(VB.NET)"
        MyBase.m_caption = "Set Observer"
        MyBase.m_toolTip = "Set Observer"
        MyBase.m_name = "Sample_SceneControl(VB.NET)/SetObserver"
        MyBase.m_message = "Set observer position to selected point"

        'Load resources
        Dim res() As String = GetType(SetObserver).Assembly.GetManifestResourceNames()
        If res.GetLength(0) > 0 Then
            MyBase.m_bitmap = New System.Drawing.Bitmap(GetType(SetObserver).Assembly.GetManifestResourceStream("SceneToolsVB.observer.bmp"))
        End If
        m_pCursor = New System.Windows.Forms.Cursor(GetType(SetObserver).Assembly.GetManifestResourceStream("SceneToolsVB.observer.cur"))

        m_pSceneHookHelper = New SceneHookHelperClass

    End Sub

    Public Overrides Sub OnCreate(ByVal hook As Object)
        m_pSceneHookHelper.Hook = hook
    End Sub

    Public Overrides ReadOnly Property Enabled() As Boolean
        Get
            If (m_pSceneHookHelper.Hook Is Nothing Or m_pSceneHookHelper.Scene Is Nothing) Then
                Return False
            Else
                Dim pCamera As ICamera = CType(m_pSceneHookHelper.Camera, ICamera)

                'Disable if orthographic (2D) view
                If (pCamera.ProjectionType = esri3DProjectionType.esriOrthoProjection) Then
                    Return False
                Else
                    Return True
                End If
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

    Public Overrides Sub OnMouseUp(ByVal Button As Integer, ByVal Shift As Integer, ByVal X As Integer, ByVal Y As Integer)
        'Get the scene graph
        Dim pSceneGraph As ISceneGraph = CType(m_pSceneHookHelper.SceneGraph, ISceneGraph)

        Dim pNewObs As IPoint = Nothing
        Dim pOwner As Object = Nothing, pObject As Object = Nothing

        'Translate screen coordinates into a 3D point
        pSceneGraph.Locate(pSceneGraph.ActiveViewer, X, Y, esriScenePickMode.esriScenePickAll, True, pNewObs, pOwner, pObject)

        If (pNewObs Is Nothing) Then
            Return
        End If

        'Get the scene viewer's camera
        Dim pCamera As ICamera = CType(m_pSceneHookHelper.Camera, ICamera)

        'Get the camera's old observer
        Dim pOldObs As IPoint = CType(pCamera.Observer, IPoint)

        'Get the duration in seconds of last redraw
        'and the average number of frames per second
        Dim dlastFrameDuration, dMeanFrameRate As Double
        pSceneGraph.GetDrawingTimeInfo(dlastFrameDuration, dMeanFrameRate)

        If (dlastFrameDuration < 0.01) Then
            dlastFrameDuration = 0.01
        End If

        Dim iSteps As Integer
        iSteps = 2 / dlastFrameDuration

        If (iSteps < 1) Then
            iSteps = 1
        End If

        If (iSteps > 60) Then
            iSteps = 60
        End If

        Dim dxObs, dyObs, dzObs As Double
        dxObs = (pNewObs.X - pOldObs.X) / iSteps
        dyObs = (pNewObs.Y - pOldObs.Y) / iSteps
        dzObs = (pNewObs.Z - pOldObs.Z) / iSteps

        'Loop through each step moving the camera's observer from the old
        'position to the new position, refreshing the scene viewer each time

        Dim i As Integer
        For i = 0 To iSteps
            pNewObs.X = pOldObs.X + (i * dxObs)
            pNewObs.Y = pOldObs.Y + (i * dyObs)
            pNewObs.Z = pOldObs.Z + (i * dzObs)
            pCamera.Observer = pNewObs
            pSceneGraph.ActiveViewer.Redraw(True)
        Next i
    End Sub
End Class


