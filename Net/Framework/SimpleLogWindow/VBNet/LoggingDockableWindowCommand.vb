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
Imports System.Runtime.InteropServices
Imports System.Drawing
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.esriSystem

<ComClass(LoggingDockableWindowCommand.ClassId, LoggingDockableWindowCommand.InterfaceId, LoggingDockableWindowCommand.EventsId), _
 ProgId("SimpleLogWindowVB.LoggingDockableWindowCommand")> _
Public NotInheritable Class LoggingDockableWindowCommand
    Inherits BaseCommand

#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "d563c450-f66c-42be-8020-cece82c0dce4"
    Public Const InterfaceId As String = "6db9f5db-a044-4835-ac94-3a678372cd86"
    Public Const EventsId As String = "a4e4cc5b-24d1-4056-a1c6-8c88230b1586"
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
        MxCommands.Register(regKey)
        GxCommands.Register(regKey)
        GMxCommands.Register(regKey)
        SxCommands.Register(regKey)

    End Sub
    ''' <summary>
    ''' Required method for ArcGIS Component Category unregistration -
    ''' Do not modify the contents of this method with the code editor.
    ''' </summary>
    Private Shared Sub ArcGISCategoryUnregistration(ByVal registerType As Type)
        Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
        MxCommands.Unregister(regKey)
        GxCommands.Unregister(regKey)
        GMxCommands.Unregister(regKey)
        SxCommands.Unregister(regKey)

    End Sub

#End Region
#End Region

    Private m_application As IApplication
    Private m_dockableWindow As IDockableWindow

    Private Const DockableWindowGuid As String = "{8582b32d-120c-407b-af34-8719b8960b30}"

    ' A creatable COM class must have a Public Sub New() 
    ' with no parameters, otherwise, the class will not be 
    ' registered in the COM registry and cannot be created 
    ' via CreateObject.
    Public Sub New()
        MyBase.New()

        MyBase.m_category = ".NET Samples"
        MyBase.m_caption = "Show/Hide Log Window (VB.Net)"
        MyBase.m_message = "Command toggles Simple Logging Window visibility (VB.Net)"
        MyBase.m_toolTip = "Toggle Simple Logging Window (VB.Net)"
        MyBase.m_name = "VBNETSamples_ToggleLogging"

        Try
            Dim bitmapResourceName As String = Me.GetType().Name + ".bmp"
            MyBase.m_bitmap = New Bitmap(Me.GetType(), bitmapResourceName)
        Catch ex As Exception
            System.Diagnostics.Trace.WriteLine(ex.Message, "Invalid Bitmap")
        End Try
    End Sub

    ''' <summary>
    ''' Occurs when this command is created
    ''' </summary>
    ''' <param name="hook">Instance of the application</param>
    Public Overrides Sub OnCreate(ByVal hook As Object)
        If Not hook Is Nothing Then
            m_application = CType(hook, IApplication)
        End If

        If m_application IsNot Nothing Then
            SetupDockableWindow()
            MyBase.m_enabled = m_dockableWindow IsNot Nothing
        Else
            MyBase.m_enabled = False
        End If
    End Sub

    ''' <summary>
    ''' Toggle visibility of dockable window and show the visible state by its checked property
    ''' </summary>
    Public Overrides Sub OnClick()
        If m_dockableWindow Is Nothing Then Return

        If m_dockableWindow.IsVisible() Then
            m_dockableWindow.Show(False)
        Else
            m_dockableWindow.Show(True)
        End If

        MyBase.m_checked = m_dockableWindow.IsVisible()
    End Sub

    Public Overrides ReadOnly Property Checked() As Boolean
        Get
            Return (m_dockableWindow IsNot Nothing) AndAlso (m_dockableWindow.IsVisible())
        End Get
    End Property

    Private Sub SetupDockableWindow()
        If m_dockableWindow Is Nothing Then
            Dim dockWindowManager As IDockableWindowManager
            dockWindowManager = CType(m_application, IDockableWindowManager)
            If Not dockWindowManager Is Nothing Then
                Dim windowID As UID = New UIDClass
                windowID.Value = DockableWindowGuid
                m_dockableWindow = dockWindowManager.GetDockableWindow(windowID)
            End If
        End If
    End Sub
End Class



