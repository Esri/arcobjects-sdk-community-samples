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
Imports ESRI.ArcGIS.Analyst3D
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.Controls
Imports System.Runtime.InteropServices

<ComClass(ExpandFOV.ClassId, ExpandFOV.InterfaceId, ExpandFOV.EventsId)> _
Public NotInheritable Class ExpandFOV
    Inherits BaseCommand

#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "3C23D6D3-D211-4F81-A487-2528A78F0E00"
    Public Const InterfaceId As String = "22EE6E86-5517-4ED8-B64F-AC4F42E8E48C"
    Public Const EventsId As String = "A8BC9C14-8BC2-490F-8D6A-0AFAFE8F5391"
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

    Private m_pSceneHookHelper As ISceneHookHelper

    ' A creatable COM class must have a Public Sub New() 
    ' with no parameters, otherwise, the class will not be 
    ' registered in the COM registry and cannot be created 
    ' via CreateObject.
    Public Sub New()
        MyBase.New()

        MyBase.m_category = "Sample_SceneControl(VB.NET)"
        MyBase.m_caption = "Expand Field of View"
        MyBase.m_toolTip = "Expand Field of View"
        MyBase.m_name = "Sample_SceneControl(VB.NET)/Expand Field of View"
        MyBase.m_message = "Expand the Field of View"

        Dim res() As String = GetType(ExpandFOV).Assembly.GetManifestResourceNames()
        If res.GetLength(0) > 0 Then
            MyBase.m_bitmap = New System.Drawing.Bitmap(GetType(ExpandFOV).Assembly.GetManifestResourceStream("SceneToolsVB.expand.bmp"))
        End If
        m_pSceneHookHelper = New SceneHookHelperClass
    End Sub

    Public Overrides ReadOnly Property Enabled() As Boolean
        Get
            If m_pSceneHookHelper.Hook Is Nothing Or m_pSceneHookHelper.Scene Is Nothing Then
                Return False
            Else
                Dim pCamera As ICamera = CType(m_pSceneHookHelper.Camera, ICamera)

                'Disable if orthographic (2D) view
                If pCamera.ProjectionType = esri3DProjectionType.esriOrthoProjection Then
                    Return False
                Else
                    Return True
                End If
            End If

        End Get
    End Property

    Public Overrides Sub OnCreate(ByVal hook As Object)
        m_pSceneHookHelper.Hook = hook
    End Sub

    Public Overrides Sub OnClick()
        'Get scene viewer's camera
        Dim pCamera As ICamera = CType(m_pSceneHookHelper.Camera, ICamera)

        'Widen the field of view
        Dim dAngle As Double
        dAngle = pCamera.ViewFieldAngle
        pCamera.ViewFieldAngle = dAngle * 1.1

        'Redraw the scene viewer
        Dim pSceneViewer As ISceneViewer = CType(m_pSceneHookHelper.ActiveViewer, ISceneViewer)
        pSceneViewer.Redraw(False)
    End Sub
End Class


