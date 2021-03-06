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
Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Runtime.InteropServices
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.Controls
Imports ESRI.ArcGIS.SystemUI

  ''' <summary>
  ''' ToolControl implementation
  ''' </summary>
<Guid("f6d7f99f-2a94-472c-9981-b7d7d6edc518"), ClassInterface(ClassInterfaceType.None), ProgId("GraphicsLayerToolControl.GraphicsLayersListToolCtrl")> _
Public NotInheritable Class GraphicsLayersListToolCtrl : Inherits BaseCommand : Implements IToolControl
#Region "COM Registration Function(s)"
  <ComRegisterFunction(), ComVisible(False)> _
  Private Shared Sub RegisterFunction(ByVal registerType As Type)
    ' Required for ArcGIS Component Category Registrar support
    ArcGISCategoryRegistration(registerType)

    '
    ' TODO: Add any COM registration code here
    '
  End Sub

  <ComUnregisterFunction(), ComVisible(False)> _
  Private Shared Sub UnregisterFunction(ByVal registerType As Type)
    ' Required for ArcGIS Component Category Registrar support
    ArcGISCategoryUnregistration(registerType)

    '
    ' TODO: Add any COM unregistration code here
    '
  End Sub

#Region "ArcGIS Component Category Registrar generated code"
  ''' <summary>
  ''' Required method for ArcGIS Component Category registration -
  ''' Do not modify the contents of this method with the code editor.
  ''' </summary>
  Private Shared Sub ArcGISCategoryRegistration(ByVal registerType As Type)
    Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
    ControlsCommands.Register(regKey)
    MxCommands.Register(regKey)

  End Sub
  ''' <summary>
  ''' Required method for ArcGIS Component Category unregistration -
  ''' Do not modify the contents of this method with the code editor.
  ''' </summary>
  Private Shared Sub ArcGISCategoryUnregistration(ByVal registerType As Type)
    Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
    ControlsCommands.Unregister(regKey)
    MxCommands.Unregister(regKey)

  End Sub

#End Region
#End Region

#Region "class members"
  Private m_hookHelper As IHookHelper = Nothing
  Private m_graphicsLayerListCtrl As GraphicsLayersListCtrl = Nothing
#End Region

#Region "constructor"
  Public Sub New()
    MyBase.m_category = ".NET Samples"
    MyBase.m_caption = "Graphics Layers"
    MyBase.m_message = "Active Graphics Layer"
    MyBase.m_toolTip = "Active Graphics Layer"
    MyBase.m_name = "ToolControlSample_GraphicsLayersListToolCtrl"

    Try
      Dim bitmapResourceName As String = Me.GetType().Name & ".bmp"
      MyBase.m_bitmap = New Bitmap(Me.GetType(), bitmapResourceName)
    Catch ex As Exception
      System.Diagnostics.Trace.WriteLine(ex.Message, "Invalid Bitmap")
    End Try
  End Sub
#End Region

#Region "Overridden Class Methods"

    ''' <summary>
    ''' Occurs when this command is created
    ''' </summary>
    ''' <param name="hook">Instance of the application</param>
    Public Overrides Sub OnCreate(ByVal hook As Object)
        If hook Is Nothing Then
            Return
        End If

        If m_hookHelper Is Nothing Then
            m_hookHelper = New HookHelperClass()
        End If

        m_hookHelper.Hook = hook

        'make sure that the usercontrol has been initialized
        If Nothing Is m_graphicsLayerListCtrl Then
            m_graphicsLayerListCtrl = New GraphicsLayersListCtrl()
            m_graphicsLayerListCtrl.CreateControl()
        End If
        'set the Map property of the control
        m_graphicsLayerListCtrl.Map = m_hookHelper.FocusMap
    End Sub

    ''' <summary>
    ''' Occurs when this command is clicked
    ''' </summary>
    Public Overrides Sub OnClick()
        'not much to do here
    End Sub

#End Region

#Region "IToolControl Members"

  Public Function OnDrop(ByVal barType As esriCmdBarType) As Boolean Implements IToolControl.OnDrop
    Return True
  End Function

  Public Sub OnFocus(ByVal complete As ICompletionNotify) Implements IToolControl.OnFocus

  End Sub

  Public ReadOnly Property hWnd() As Integer Implements IToolControl.hWnd
    Get
      'pass the handle of the usercontrol
      If Nothing Is m_graphicsLayerListCtrl Then
        m_graphicsLayerListCtrl = New GraphicsLayersListCtrl()
        m_graphicsLayerListCtrl.CreateControl()
      End If

      Return m_graphicsLayerListCtrl.Handle.ToInt32()

    End Get
  End Property

#End Region
End Class
