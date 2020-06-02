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
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports System.Runtime.InteropServices
Imports ESRI.ArcGIS.Framework

<ComClass(LoggingWindowCtxMnu.ClassId, LoggingWindowCtxMnu.InterfaceId, LoggingWindowCtxMnu.EventsId), _
 ProgId("SimpleLogWindowVB.LoggingWindowCtxMnu")> _
Public NotInheritable Class LoggingWindowCtxMnu
    Inherits BaseMenu
    Implements IShortcutMenu

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
        MxCommandBars.Register(regKey)
        GxCommandBars.Register(regKey)
        GMxCommandBars.Register(regKey)
        SxCommandBars.Register(regKey)

    End Sub
    ''' <summary>
    ''' Required method for ArcGIS Component Category unregistration -
    ''' Do not modify the contents of this method with the code editor.
    ''' </summary>
    Private Shared Sub ArcGISCategoryUnregistration(ByVal registerType As Type)
        Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
        MxCommandBars.Unregister(regKey)
        GxCommandBars.Unregister(regKey)
        GMxCommandBars.Unregister(regKey)
        SxCommandBars.Unregister(regKey)

    End Sub

#End Region
#End Region

#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "a04582d2-7e86-4484-a089-3adcb97f3d9d"
    Public Const InterfaceId As String = "ca4de0f4-e68a-42e0-bbae-ae08ac6cb242"
    Public Const EventsId As String = "45745a8b-2319-4b23-b20b-567cea95001b"
#End Region

    ' A creatable COM class must have a Public Sub New() 
    ' with no parameters, otherwise, the class will not be 
    ' registered in the COM registry and cannot be created 
    ' via CreateObject.
    Public Sub New()
        AddItem("{af3e8998-c592-42cf-b9c3-04a5b8e8d47b}")   'Clear log command
        AddItem("{a0e848c1-b873-4604-b4c0-3e4b6a901c79}")    'Delete log by line multiItem
        'BeginGroup()    'Separator
    End Sub

    Public Overrides ReadOnly Property Caption() As String
        Get
            Return "Logging Window Context Menu (VB.Net)"
        End Get
    End Property

    Public Overrides ReadOnly Property Name() As String
        Get
            Return "VBNETSamples_LoggingWindowCtxMnu"
        End Get
    End Property
End Class


