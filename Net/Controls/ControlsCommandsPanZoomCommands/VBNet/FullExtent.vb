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
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Controls
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.ADF.CATIDs
Imports System.Runtime.InteropServices

<ComClass(FullExtent.ClassId, FullExtent.InterfaceId, FullExtent.EventsId)> _
Public NotInheritable Class FullExtent
    Inherits BaseCommand

#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "4C02C1E7-D695-4EA8-AF88-A2813464FFC3"
    Public Const InterfaceId As String = "F58A91E4-9FF1-4A7E-963C-FD10D375DA1C"
    Public Const EventsId As String = "7D83311E-C7B3-4E2D-B1C9-5A5EAC9E77EC"
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

    ' A creatable COM class must have a Public Sub New() 
    ' with no parameters, otherwise, the class will not be 
    ' registered in the COM registry and cannot be created 
    ' via CreateObject.

    Private m_pEnabled As Boolean
    Private m_pHookHelper As IHookHelper

  Public Sub New()
    MyBase.New()

    MyBase.m_category = "Sample_Pan_VBNET/Zoom"
    MyBase.m_caption = "Full Extent"
    MyBase.m_message = "Zooms the Display to Full Extent of the Data"
    MyBase.m_toolTip = "Full Extent"
    MyBase.m_name = "Sample_Pan/Zoom_Full Extent"

    Dim res() As String = GetType(FullExtent).Assembly.GetManifestResourceNames()
    If res.GetLength(0) > 0 Then
      MyBase.m_bitmap = New System.Drawing.Bitmap(GetType(FullExtent).Assembly.GetManifestResourceStream("PanZoomVBNET.FullExtent.bmp"))
    End If
    m_pHookHelper = New HookHelperClass

  End Sub

  Public Overrides Sub OnClick()
    'Get IActiveView interface
    Dim pActiveView As IActiveView = CType(m_pHookHelper.FocusMap, IActiveView)

    'Set the extent to the full extent
    pActiveView.Extent = pActiveView.FullExtent

    'Refresh the active view
    pActiveView.Refresh()
  End Sub

  Public Overrides Sub OnCreate(ByVal hook As Object)

        m_pHookHelper.Hook = hook
        m_pEnabled = True

  End Sub

  Public Overrides ReadOnly Property Enabled() As Boolean
    Get
      Return m_pEnabled
    End Get
  End Property
End Class


