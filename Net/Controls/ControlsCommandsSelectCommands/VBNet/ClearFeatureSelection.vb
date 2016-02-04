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
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.Controls
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.ADF.CATIDs
Imports System.Runtime.InteropServices

<ComClass(ClearFeatureSelection.ClassId, ClearFeatureSelection.InterfaceId, ClearFeatureSelection.EventsId)> _
Public NotInheritable Class ClearFeatureSelection
    Inherits BaseCommand

#Region "COM GUIDs"
    'These GUIDs provide the COM identity for this class and its COM interfaces. 
    'If you change them, existing clients will no longer be able to access the class.
    Public Const ClassId As String = "4790a169-e186-415b-94a8-9cfe77d5ee6c"
    Public Const InterfaceId As String = "99bf90a4-26de-4582-b8b1-53cc7413ed85"
    Public Const EventsId As String = "7953f056-f86d-4918-b864-37996731b098"
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

    Private m_pHookHelper As IHookHelper

    Public Sub New()
        MyBase.New()

        'Create an IHookHelper object
        m_pHookHelper = New HookHelperClass

        'Set the tool properties
        MyBase.m_caption = "Clear Feature Selection"
        MyBase.m_category = "Sample_Select(VB.NET)"
        MyBase.m_name = "Sample_Select(VB.NET)_Clear Feature Selection"
        MyBase.m_message = "Clear Current Feature Selection"
        MyBase.m_toolTip = "Clear Feature Selection"
        MyBase.m_bitmap = New System.Drawing.Bitmap(GetType(ClearFeatureSelection).Assembly.GetManifestResourceStream(GetType(ClearFeatureSelection), "ClearSelection.bmp"))

    End Sub

    Public Overrides Sub OnCreate(ByVal hook As Object)
        m_pHookHelper.Hook = hook
    End Sub

    Public Overrides ReadOnly Property Enabled() As Boolean
        Get
            If (m_pHookHelper.FocusMap Is Nothing) Then Exit Property
            Return (m_pHookHelper.FocusMap.SelectionCount > 0)
        End Get
    End Property

    Public Overrides Sub OnClick()
        'Clear selection
        m_pHookHelper.FocusMap.ClearSelection()

        'Get the IActiveView of the FocusMap
        Dim pActiveView As IActiveView
        pActiveView = m_pHookHelper.FocusMap

        'Get the visible extent of the display
        Dim pBounds As IEnvelope
        pBounds = pActiveView.ScreenDisplay.DisplayTransformation.FittedBounds

        'Refresh the visible extent of the display
        pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, pBounds)
    End Sub

End Class


