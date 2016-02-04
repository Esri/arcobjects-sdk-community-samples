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

<ComClass(GoBackToPreviousExtent.ClassId, GoBackToPreviousExtent.InterfaceId, GoBackToPreviousExtent.EventsId)> _
Public NotInheritable Class GoBackToPreviousExtent
    Inherits BaseCommand

#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "26D27BF0-934C-49CF-8083-96AEC30AC114"
    Public Const InterfaceId As String = "DE5352BB-55B5-4028-9745-6018697A53A9"
    Public Const EventsId As String = "6B6DDA8C-D0D5-421F-BE06-F92C3A5138CB"
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

    Private m_pEnabled As Boolean
    Private m_pHookHelper As IHookHelper

    ' A creatable COM class must have a Public Sub New() 
    ' with no parameters, otherwise, the class will not be 
    ' registered in the COM registry and cannot be created 
    ' via CreateObject.
    Public Sub New()
        MyBase.New()

        MyBase.m_category = "Sample_Pan_VBNET/Zoom"
        MyBase.m_caption = "Undo Extent"
        MyBase.m_message = "Go to Previous Screen location"
        MyBase.m_toolTip = "Draw Previous Extent"
        MyBase.m_name = "Sample_Pan/Zoom_Undo Extent"

        Dim res() As String = GetType(GoBackToPreviousExtent).Assembly.GetManifestResourceNames()
        If res.GetLength(0) > 0 Then
            MyBase.m_bitmap = New System.Drawing.Bitmap(GetType(GoBackToPreviousExtent).Assembly.GetManifestResourceStream("PanZoomVBNET.UnDoDraw.bmp"))
        End If
        m_pHookHelper = New HookHelperClass
    End Sub

    Public Overrides Sub OnClick()
        'Get the active view
        Dim pActiveView As IActiveView = CType(m_pHookHelper.FocusMap, IActiveView)

        'Get the extent stack
        Dim pMapExtent As IExtentStack = CType(pActiveView.ExtentStack, IExtentStack)

        'Undo the extent view	
        If pMapExtent.CanUndo() Then
            pMapExtent.Undo()
        End If
    End Sub

    Public Overrides Sub OnCreate(ByVal hook As Object)

        m_pHookHelper.Hook = hook
        m_pEnabled = True

    End Sub

    Public Overrides ReadOnly Property Enabled() As Boolean
        Get
            If m_pHookHelper.FocusMap Is Nothing Then Exit Property

            'Get the active view
            Dim pActiveView As IActiveView = CType(m_pHookHelper.FocusMap, IActiveView)

            'Get the extent stack
            Dim pMapStack As IExtentStack = CType(pActiveView.ExtentStack, IExtentStack)

            'Can the extent be undone
            Return m_pEnabled = pMapStack.CanUndo()
        End Get
    End Property
End Class


