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
Imports ESRI.ArcGIS.ADF.CATIDs


<ComClass(CultureMenu.ClassId, CultureMenu.InterfaceId, CultureMenu.EventsId)> _
Public NotInheritable Class CultureMenu
    Implements ESRI.ArcGIS.SystemUI.IMenuDef


#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "ef63d296-6c09-4d74-9475-612083133e3f"
    Public Const InterfaceId As String = "18460aa4-c6fc-4176-b1a1-91cd7de5982d"
    Public Const EventsId As String = "538eb995-e58d-46df-8774-a0b9dfd5255d"
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
#End Region

#Region "ArcGIS Component Category Registrar generated code"
    Private Shared Sub ArcGISCategoryRegistration(ByVal registerType As Type)
        Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
        ControlsMenus.Register(regKey)

    End Sub
    Private Shared Sub ArcGISCategoryUnregistration(ByVal registerType As Type)
        Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
        ControlsMenus.Unregister(regKey)

    End Sub

#End Region

    ' A creatable COM class must have a Public Sub New() 
    ' with no parameters, otherwise, the class will not be 
    ' registered in the COM registry and cannot be created 
    ' via CreateObject.
    Public Sub New()
        MyBase.New()
    End Sub

    Public ReadOnly Property Caption() As String Implements ESRI.ArcGIS.SystemUI.IMenuDef.Caption
        Get
            'The Caption is set from the 'MenuCaption' string 
            'stored in the Resource File. The ResourceManager acquires the appropriate 
            'Resource file according to the UI Culture of the current thread
            'system.Windows.Forms.MessageBox.Show(My.Resources.Culture.

            Dim pResourceManager As New System.Resources.ResourceManager("VBDotNetCultureSample.Resources", Me.GetType().Assembly)
            Dim pResource_str As String
            pResource_str = CType(pResourceManager.GetObject("MenuCaption"), String)
            Return pResource_str
        End Get
    End Property

    Public Sub GetItemInfo(ByVal pos As Integer, ByVal itemDef As ESRI.ArcGIS.SystemUI.IItemDef) Implements ESRI.ArcGIS.SystemUI.IMenuDef.GetItemInfo

        'Adding the Items to the Menu
        Select Case pos
            Case 0
                ' Set the item in the menu to the ClassID of multiItem above
                itemDef.ID = "esriControlToolsPageLayout.ControlsPageZoomInFixedCommand"
                itemDef.Group = False
            Case 1
                ' Set the item in the menu to the ClassID of multiItem above
                itemDef.ID = "esriControlToolsPageLayout.ControlsPageZoomOutFixedCommand"
                itemDef.Group = False
            Case 2
                ' Set the item in the menu to the ClassID of multiItem above
                itemDef.ID = "esriControlToolsPageLayout.ControlsPageZoomWholePageCommand"
                itemDef.Group = False
            Case 3
                ' Set the item in the menu to the ClassID of multiItem above
                itemDef.ID = "esriControlToolsPageLayout.ControlsPageZoomPageToLastExtentBackCommand"
                itemDef.Group = False
            Case 4
                ' Set the item in the menu to the ClassID of multiItem above
                itemDef.ID = "esriControlToolsPageLayout.ControlsPageZoomPageToLastExtentForwardCommand"
                itemDef.Group = False
        End Select
    End Sub

    Public ReadOnly Property ItemCount() As Integer Implements ESRI.ArcGIS.SystemUI.IMenuDef.ItemCount
        Get
            'States the number of items in the menu described in GetItemInfo
            Return 5
        End Get
    End Property

    Public ReadOnly Property Name() As String Implements ESRI.ArcGIS.SystemUI.IMenuDef.Name
        Get
            Return "CustomCommands_CultureMenu"
        End Get
    End Property
End Class


