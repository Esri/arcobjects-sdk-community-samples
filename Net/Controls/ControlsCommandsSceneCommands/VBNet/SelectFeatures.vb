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
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.GeomeTry
Imports ESRI.ArcGIS.Controls
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.ADF.CATIDs
Imports System.Runtime.InteropServices

<ComClass(SelectFeatures.ClassId, SelectFeatures.InterfaceId, SelectFeatures.EventsId)> _
Public NotInheritable Class SelectFeatures
    Inherits BaseTool

#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "24AADD40-4708-4B59-8F1F-3CC68FA510F6"
    Public Const InterfaceId As String = "93A59666-1479-47C2-96E2-A079ED988A47"
    Public Const EventsId As String = "9DD4B578-050E-418D-BE35-1B41572AC49D"
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
        MyBase.m_caption = "Select Features"
        MyBase.m_toolTip = "Select Features"
        MyBase.m_name = "Sample_SceneControl(VB.NET)/SelectFeatures"
        MyBase.m_message = "Select features by clicking"

        'Load resources
        Dim res() As String = GetType(SelectFeatures).Assembly.GetManifestResourceNames()
        If res.GetLength(0) > 0 Then
            MyBase.m_bitmap = New System.Drawing.Bitmap(GetType(SelectFeatures).Assembly.GetManifestResourceStream("SceneToolsVB.SelectFeatures.bmp"))
        End If
        m_pCursor = New System.Windows.Forms.Cursor(GetType(SelectFeatures).Assembly.GetManifestResourceStream("SceneToolsVB.SelectFeatures.cur"))

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
                Dim pScene As IScene = CType(m_pSceneHookHelper.Scene, IScene)

                'Disable if no layer
                If (pScene.LayerCount = 0) Then
                    Return False
                End If

                'Enable if any selectable layers
                Dim bSelectable As Boolean
                bSelectable = False

                Dim pEnumLayer As IEnumLayer
                pEnumLayer = pScene.Layers(Nothing, True)
                pEnumLayer.Reset()

                Dim pLayer As ILayer = CType(pEnumLayer.Next(), ILayer)

                'Loop through the scene layers
                Do Until (pLayer Is Nothing)
                    'Determine if there is a selectable feature layer
                    If TypeOf pLayer Is IFeatureLayer Then
                        Dim pFeatureLayer As IFeatureLayer = CType(pLayer, IFeatureLayer)

                        If (pFeatureLayer.Selectable = True) Then
                            bSelectable = True
                            Exit Do
                        End If
                    End If
                    pLayer = pEnumLayer.Next()
                Loop

                Return bSelectable
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

        'Get the scene
        Dim pScene As IScene = CType(m_pSceneHookHelper.Scene, IScene)

        Dim pPoint As IPoint = Nothing
        Dim pOwner As Object = Nothing, pObject As Object = Nothing

        'Translate screen coordinates into a 3D point
        pSceneGraph.Locate(pSceneGraph.ActiveViewer, X, Y, esriScenePickMode.esriScenePickGeography, True, pPoint, pOwner, pObject)

        'Get a selection environment
        Dim pSelectionEnv As ISelectionEnvironment
        pSelectionEnv = New SelectionEnvironmentClass

        If (Shift = 0) Then
            pSelectionEnv.CombinationMethod = ESRI.ArcGIS.Carto.esriSelectionResultEnum.esriSelectionResultNew

            'Clear previous selection
            If (pOwner Is Nothing) Then
                pScene.ClearSelection()
                Return
            End If
        Else
            pSelectionEnv.CombinationMethod = ESRI.ArcGIS.Carto.esriSelectionResultEnum.esriSelectionResultAdd
        End If

        'If the layer is a selectable feature layer
        If TypeOf pOwner Is IFeatureLayer Then
            Dim pFeatureLayer As IFeatureLayer = CType(pOwner, IFeatureLayer)
            If (pFeatureLayer.Selectable = True) Then
                'Select by Shape
                pScene.SelectByShape(pPoint, pSelectionEnv, False)
            End If

            'Refresh the scene viewer
            pSceneGraph.RefreshViewers()
        End If
    End Sub
End Class


