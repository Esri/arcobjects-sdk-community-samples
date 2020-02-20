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
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.GeomeTry
Imports ESRI.ArcGIS.Controls
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.ADF.CATIDs
Imports System.Runtime.InteropServices

<ComClass(SelectGraphics.ClassId, SelectGraphics.InterfaceId, SelectGraphics.EventsId)> _
Public NotInheritable Class SelectGraphics
    Inherits BaseTool

#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "18563590-2592-4D8A-BD7C-4187A13E693E"
    Public Const InterfaceId As String = "87D0B98E-BC10-4574-8F29-E75697505A8F"
    Public Const EventsId As String = "23A96699-81F0-4ADC-A7B0-80B1D75E6A0C"
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
        MyBase.m_caption = "Select Graphics"
        MyBase.m_toolTip = "Select Graphics"
        MyBase.m_name = "Sample_SceneControl(VB.NET)/SelectGraphics"
        MyBase.m_message = "Select graphics by clicking"

        'Load resources
        Dim res() As String = GetType(SelectGraphics).Assembly.GetManifestResourceNames()
        If res.GetLength(0) > 0 Then
            MyBase.m_bitmap = New System.Drawing.Bitmap(GetType(SelectGraphics).Assembly.GetManifestResourceStream("SceneToolsVB.select.bmp"))
        End If
        m_pCursor = New System.Windows.Forms.Cursor(GetType(SelectGraphics).Assembly.GetManifestResourceStream("SceneToolsVB.selectGraphics.cur"))

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
                'Determine if there is a 3D graphics container
                Dim pScene As IScene = CType(m_pSceneHookHelper.Scene, IScene)
                If (TypeOf pScene.ActiveGraphicsLayer Is IGraphicsContainer3D) Then
                    Return True
                Else
                    Return False
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

        Dim pPoint As IPoint = Nothing
        Dim pOwner As Object = Nothing, pObject As Object = Nothing

        'Translate screen coordinates into a 3D point
        pSceneGraph.Locate(pSceneGraph.ActiveViewer, X, Y, esriScenePickMode.esriScenePickGraphics, False, pPoint, pOwner, pObject)

        Dim pGraphicsSelection As IGraphicsSelection

        If (pObject Is Nothing) Then
            If (Shift = 0) Then
                'Unselect selected graphics from the
                'basic graphics layer and each layer
                pGraphicsSelection = CType(pSceneGraph.Scene.BasicGraphicsLayer, IGraphicsSelection)
                pGraphicsSelection.UnselectAllElements()

                Dim i As Integer
                For i = 0 To pSceneGraph.Scene.LayerCount - 1
                    Dim pLayer As ILayer = CType(pSceneGraph.Scene.get_Layer(i), ILayer)
                    pGraphicsSelection = CType(pLayer, IGraphicsSelection)
                    pGraphicsSelection.UnselectAllElements()
                Next i
            End If
        Else
            pGraphicsSelection = CType(pOwner, IGraphicsSelection)

            'Unselect any selected graphics
            If (Shift = 0) Then
                pGraphicsSelection.UnselectAllElements()

                'Select element
                Dim pElement As IElement = CType(pObject, IElement)
                pGraphicsSelection.SelectElement(pElement)
            End If
        End If

        'Refresh the scene viewer
        pSceneGraph.ActiveViewer.Redraw(False)

    End Sub
End Class


