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
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports System.Runtime.InteropServices


<ComClass(VertexCommandsMenu.ClassId, VertexCommandsMenu.InterfaceId, VertexCommandsMenu.EventsId), _
 ProgId("VertexCommands_VB.VertexCommandsMenu")> _
Public NotInheritable Class VertexCommandsMenu

    Inherits BaseMenu


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
        ControlsMenus.Register(regKey)

    End Sub
    ''' <summary>
    ''' Required method for ArcGIS Component Category unregistration -
    ''' Do not modify the contents of this method with the code editor.
    ''' </summary>
    Private Shared Sub ArcGISCategoryUnregistration(ByVal registerType As Type)
        Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
        ControlsMenus.Unregister(regKey)

    End Sub

#End Region
#End Region

#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "B3AF0F44-8FC0-4a65-97E6-E86A12E4E581"
    Public Const InterfaceId As String = "B89AF658-4150-4440-B792-585E36210DBD"
    Public Const EventsId As String = "7979F99C-7A43-47b1-B2E4-47A5D5A6FD28"
#End Region

    ' A creatable COM class must have a Public Sub New() 
    ' with no parameters, otherwise, the class will not be 
    ' registered in the COM registry and cannot be created 
    ' via CreateObject.
    Public Sub New()

        AddItem("VertexCommands_VB.CustomVertexCommands", 1)  'Custom Insert vertex
        AddItem("VertexCommands_VB.CustomVertexCommands", 2)   'Custom Delete vertex 
        AddItem("VertexCommands_VB.UsingOutOfBoxVertexCommands", 1)  'Out-of-Box Insert vertex
        AddItem("VertexCommands_VB.UsingOutOfBoxVertexCommands", 2)   'Out-of-Box Delete vertex 

    End Sub

    Public Overrides ReadOnly Property Caption() As String
        Get
            Return "Vertex Tools"
        End Get
    End Property

    Public Overrides ReadOnly Property Name() As String
        Get
            Return "VertexCommands_VertexCommandsMenu_VB"
        End Get
    End Property
End Class

