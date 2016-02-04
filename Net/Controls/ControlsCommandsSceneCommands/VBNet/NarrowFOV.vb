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
Imports ESRI.ArcGIS.Controls
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.ADF.CATIDs
Imports System.Runtime.InteropServices

<ComClass(NarrowFOV.ClassId, NarrowFOV.InterfaceId, NarrowFOV.EventsId)> _
Public NotInheritable Class NarrowFOV
    Inherits BaseCommand

#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "8379C5D4-AC6D-4BC9-90B9-D95BC4BE6828"
    Public Const InterfaceId As String = "C9A26C63-31EC-4CE0-A4C8-2476E40717D1"
    Public Const EventsId As String = "226947BD-DBCD-448E-907B-C5ADE3CF5373"
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
        MyBase.m_caption = "Narrow Field of View"
        MyBase.m_toolTip = "Narrow Field of View"
        MyBase.m_name = "Sample_SceneControl(VB.NET)/Narrow Field of View"
        MyBase.m_message = "Narrow Field of View"

        Dim res() As String = GetType(NarrowFOV).Assembly.GetManifestResourceNames()
        If res.GetLength(0) > 0 Then
            MyBase.m_bitmap = New System.Drawing.Bitmap(GetType(NarrowFOV).Assembly.GetManifestResourceStream("SceneToolsVB.narrow.bmp"))
        End If
        m_pSceneHookHelper = New SceneHookHelperClass
    End Sub

    Public Overrides Sub OnCreate(ByVal hook As Object)
        m_pSceneHookHelper.Hook = hook
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

    Public Overrides Sub OnClick()
        'Get scene viewer's camera
        Dim pCamera As ICamera = CType(m_pSceneHookHelper.Camera, ICamera)

        'Widen the field of view
        Dim dAngle As Double
        dAngle = pCamera.ViewFieldAngle
        pCamera.ViewFieldAngle = dAngle * 0.9

        'Redraw the scene viewer
        Dim pSceneViewer As ISceneViewer = CType(m_pSceneHookHelper.ActiveViewer, ISceneViewer)
        pSceneViewer.Redraw(False)
    End Sub
End Class


