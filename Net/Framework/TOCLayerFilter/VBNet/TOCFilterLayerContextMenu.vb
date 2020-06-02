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

<ComClass(TOCFilterLayerContextMenu.ClassId, TOCFilterLayerContextMenu.InterfaceId, TOCFilterLayerContextMenu.EventsId), _
 ProgId("TOCLayerFilterVB.TOCFilterLayerContextMenu")> _
Public NotInheritable Class TOCFilterLayerContextMenu
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

    End Sub
    ''' <summary>
    ''' Required method for ArcGIS Component Category unregistration -
    ''' Do not modify the contents of this method with the code editor.
    ''' </summary>
    Private Shared Sub ArcGISCategoryUnregistration(ByVal registerType As Type)
        Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
        MxCommandBars.Unregister(regKey)

    End Sub

#End Region
#End Region

#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "fc4032dd-323e-401e-8642-2d4a25b435c1"
    Public Const InterfaceId As String = "60075b05-7778-40b5-afa1-cc52548724aa"
    Public Const EventsId As String = "7def099c-e687-466c-9d14-3be2569a3a3e"
#End Region

    ' A creatable COM class must have a Public Sub New() 
    ' with no parameters, otherwise, the class will not be 
    ' registered in the COM registry and cannot be created 
    ' via CreateObject.
    Public Sub New()
        AddItem("{18DF94D9-0F8A-11D2-94B1-080009EEBECB}", 7) 'Layer Zoom
        AddItem("{BF64319A-9062-11D2-AE71-080009EC732A}", 4) 'Table
        BeginGroup() 'Separator
        AddItem("{18DF94D9-0F8A-11D2-94B1-080009EEBECB}", 8) 'Save to file
        BeginGroup() 'Separator
        AddItem("{18DF94D9-0F8A-11D2-94B1-080009EEBECB}", 9) 'Properties
    End Sub

    Public Overrides ReadOnly Property Caption() As String
        Get

            Return "Custom TOC Layer Filter Menu (VB.NET)"
        End Get
    End Property

    Public Overrides ReadOnly Property Name() As String
        Get
            Return "VBNETSamples_TOCFilterMenu"
        End Get
    End Property
End Class


