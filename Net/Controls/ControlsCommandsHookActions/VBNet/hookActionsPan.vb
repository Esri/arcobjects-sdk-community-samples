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
Imports ESRI.ArcGIS.Controls
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Geodatabase

<ComClass(hookActionsFlash.ClassId, hookActionsFlash.InterfaceId, hookActionsFlash.EventsId), _
 ProgId("HookActions.hookActionsPan")> _
Public NotInheritable Class hookActionsPan
    Inherits BaseCommand

#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "95B205CA-8E9D-4409-844F-AB777CFD2C95"
    Public Const InterfaceId As String = "DB7606BA-77A0-46b2-92D0-3841AC08489E"
    Public Const EventsId As String = "49482A53-B22C-4b69-84BA-3006029D5953"
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
        ControlsCommands.Register(regKey)
        MxCommands.Register(regKey)
        GMxCommands.Register(regKey)
    End Sub

    Private Shared Sub ArcGISCategoryUnregistration(ByVal registerType As Type)
        Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
        ControlsCommands.Unregister(regKey)
        MxCommands.Unregister(regKey)
        GMxCommands.Unregister(regKey)
    End Sub

#End Region
#End Region

    Private m_hookHelper As IHookHelper
    Private m_globeHookHelper As IGlobeHookHelper

    ' A creatable COM class must have a Public Sub New() 
    ' with no parameters, otherwise, the class will not be 
    ' registered in the COM registry and cannot be created 
    ' via CreateObject.
    Public Sub New()
        MyBase.New()

        MyBase.m_category = "HookActions"
        MyBase.m_caption = "Pan selected features"
        MyBase.m_message = "Pan selected features"
        MyBase.m_toolTip = "Pan selected features"
        MyBase.m_name = "HookActions_Pan"

    End Sub

    Public Overrides Sub OnCreate(ByVal hook As Object)

        ' Test the hook that calls this command and disable if nothing is valid
        If Not hook Is Nothing Then
            Try
                m_hookHelper = New HookHelperClass
                m_hookHelper.Hook = hook
                If m_hookHelper.ActiveView Is Nothing Then m_hookHelper = Nothing
            Catch
                m_hookHelper = Nothing
            End Try

            If m_hookHelper Is Nothing Then
                'Can be globe
                Try
                    m_globeHookHelper = New GlobeHookHelperClass
                    m_globeHookHelper.Hook = hook
                    If m_globeHookHelper.ActiveViewer Is Nothing Then m_globeHookHelper = Nothing
                Catch
                    m_globeHookHelper = Nothing
                End Try
            End If
        End If

        If m_globeHookHelper Is Nothing And m_hookHelper Is Nothing Then
            MyBase.m_enabled = False
        Else
            MyBase.m_enabled = True
        End If

    End Sub

    Public Overrides ReadOnly Property Enabled() As Boolean
        Get

            Dim hookActions As IHookActions = Nothing
            Dim basicMap As IBasicMap = Nothing

            'Get basic map and set hook actions
            If Not m_hookHelper Is Nothing Then
                basicMap = m_hookHelper.FocusMap
                hookActions = m_hookHelper
            ElseIf Not m_globeHookHelper Is Nothing Then
                basicMap = CType(m_globeHookHelper.Globe, IBasicMap)
                hookActions = m_globeHookHelper
            End If

            'Disable if no features selected
            Dim enumFeature As IEnumFeature = basicMap.FeatureSelection
            Dim feature As IFeature = enumFeature.Next
            If feature Is Nothing Then Return False

            'Enable if action supported on first selected feature
            If hookActions.ActionSupported(feature.Shape, esriHookActions.esriHookActionsPan) Then
                Return True
            Else
                Return False
            End If

        End Get
    End Property

    Public Overrides Sub OnClick()

        Dim hookActions As IHookActions = Nothing
        Dim basicMap As IBasicMap = Nothing

        'Get basic map and set hook actions
        If Not m_hookHelper Is Nothing Then
            basicMap = m_hookHelper.FocusMap
            hookActions = m_hookHelper
        ElseIf Not m_globeHookHelper Is Nothing Then
            basicMap = CType(m_globeHookHelper.Globe, IBasicMap)
            hookActions = m_globeHookHelper
        End If

        'Get feature selection
        Dim selection As ISelection
        selection = basicMap.FeatureSelection
        'Get enumerator
        Dim enumFeature As IEnumFeature, feature As IFeature
        enumFeature = selection
        enumFeature.Reset()
        'Set first feature
        feature = enumFeature.Next

        'Loop though the features
        Dim array As IArray = New Array()
        Do Until feature Is Nothing
            'Add feature to array
            array.Add(feature.Shape)
            'Set next feature
            feature = enumFeature.Next
        Loop

        'If the action is supported perform the action
        If hookActions.ActionSupportedOnMultiple(array, esriHookActions.esriHookActionsPan) Then
            hookActions.DoActionOnMultiple(array, esriHookActions.esriHookActionsPan)
        End If

    End Sub
End Class

