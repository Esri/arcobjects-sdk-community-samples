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
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.SystemUI
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.ADF.BaseClasses

<ComClass(ZoomInCommands.ClassId, ZoomInCommands.InterfaceId, ZoomInCommands.EventsId), _
 ProgId("CommandSubtypeVB.ZoomInCommands")> _
Public NotInheritable Class ZoomInCommands
    Inherits BaseCommand
    Implements ICommandSubType

    Private m_application As IApplication
    Private m_subtype As Integer 'subtype code
    Private m_xoomCommand As ICommandItem 'command to perform the zooming task

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
        GMxCommands.Register(regKey)
        MxCommands.Register(regKey)
        SxCommands.Register(regKey)

    End Sub
    ''' <summary>
    ''' Required method for ArcGIS Component Category unregistration -
    ''' Do not modify the contents of this method with the code editor.
    ''' </summary>
    Private Shared Sub ArcGISCategoryUnregistration(ByVal registerType As Type)
        Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
        GMxCommands.Unregister(regKey)
        MxCommands.Unregister(regKey)
        SxCommands.Unregister(regKey)

    End Sub

#End Region
#End Region

#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "2a630f99-abd6-44fa-897b-c22afb0fe9ce"
    Public Const InterfaceId As String = "f5124c2b-40f5-4e7a-b164-5412f6f25327"
    Public Const EventsId As String = "aab2c866-df6b-40ee-ad54-7227fdafc348"
#End Region

    ' A creatable COM class must have a Public Sub New() 
    ' with no parameters, otherwise, the class will not be 
    ' registered in the COM registry and cannot be created 
    ' via CreateObject.
    Public Sub New()
        MyBase.New()

        'Set up common properties
        MyBase.m_category = ".NET Samples"
    End Sub

    Public Overrides Sub OnCreate(ByVal hook As Object)
        'Get the appropriate zoom in command based on application
        m_application = TryCast(hook, IApplication)
        MyBase.m_enabled = False
        If m_application IsNot Nothing Then
            Dim cmdUID As New UIDClass()
            Select Case m_application.Name 'or test by casting the appropriate application interface
                Case "ArcMap"
                    cmdUID.Value = "esriArcMapUI.ZoomInFixedCommand"
                Case "ArcScene"
                    cmdUID.Value = "esriArcScene.SxNarrowFOVCommand"
                Case "ArcGlobe"
                    cmdUID.Value = "esriArcGlobe.GMxNarrowFOVCommand"
            End Select

            Dim docBars As ICommandBars = m_application.Document.CommandBars
            m_xoomCommand = docBars.Find(cmdUID)

            'Enable only when the delegate command is available
            MyBase.m_enabled = m_xoomCommand IsNot Nothing
        End If
    End Sub

    Public Overrides Sub OnClick()
        For i As Integer = 1 To m_subtype 'Starts from 1
            m_xoomCommand.Execute()
        Next
    End Sub

#Region "ICommandSubType implementations"
    Public Function GetCount() As Integer Implements ESRI.ArcGIS.SystemUI.ICommandSubType.GetCount
        Return 3
    End Function

    Public Sub SetSubType(ByVal SubType As Integer) Implements ESRI.ArcGIS.SystemUI.ICommandSubType.SetSubType
        'Called by framework to indicate the subtype command the application is referring to
        'Subtype starts from 1
        m_subtype = SubType

        'Set up bitmap, caption, tooltip etc.
        If MyBase.Bitmap = 0 Then 'Load from project resource
            Select Case m_subtype
                Case 1
                    MyBase.m_bitmap = My.Resources.ZoomOnce
                Case 2
                    MyBase.m_bitmap = My.Resources.ZoomTwice
                Case 3
                    MyBase.m_bitmap = My.Resources.ZoomThrice
            End Select
        End If

        MyBase.m_caption = String.Format("Fixed zoom in x{0} (VB.Net)", m_subtype.ToString())
        MyBase.m_name = String.Format("VBNETSamples_SubTypeCommand{0}", m_subtype)
        MyBase.m_message = String.Format("Executing fixed zoom in {0} time(s)", m_subtype.ToString())
        MyBase.m_toolTip = String.Format("Fixed Zoom in x{0}", m_subtype)
    End Sub
#End Region

End Class


