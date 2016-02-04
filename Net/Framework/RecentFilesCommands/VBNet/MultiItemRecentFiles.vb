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
Imports System.Runtime.InteropServices
Imports ESRI.ArcGIS.SystemUI
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.esriSystem

<ComClass(MultiItemRecentFiles.ClassId, MultiItemRecentFiles.InterfaceId, MultiItemRecentFiles.EventsId), _
 ProgId("RecentFilesCommandsVB.MultiItemRecentFiles")> _
Public Class MultiItemRecentFiles
    Implements IMultiItem
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
    Public Const ClassId As String = "927b25f4-f24c-471c-a2cd-777c2c371d34"
    Public Const InterfaceId As String = "f4ab3750-49db-46be-aaa6-ffdd9fd8a7ac"
    Public Const EventsId As String = "00f31cd2-b7d7-4014-8145-801a2f49ac65"
#End Region

    Private m_application As Iapplication
    Private m_filePaths As String()

    ' A creatable COM class must have a Public Sub New() 
    ' with no parameters, otherwise, the class will not be 
    ' registered in the COM registry and cannot be created 
    ' via CreateObject.
    Public Sub New()
        MyBase.New()
    End Sub

    Public ReadOnly Property Caption() As String Implements IMultiItem.Caption
        Get
            Return "Recent Files Example (VB.Net)"
        End Get
    End Property

    Public ReadOnly Property HelpContextID() As Integer Implements IMultiItem.HelpContextID
        Get
            Return 0
        End Get
    End Property

    Public ReadOnly Property HelpFile() As String Implements IMultiItem.HelpFile
        Get
            Return String.Empty
        End Get
    End Property

    Public ReadOnly Property ItemBitmap(ByVal index As Integer) As Integer Implements IMultiItem.ItemBitmap
        Get
            Return 0
        End Get
    End Property

    Public ReadOnly Property ItemCaption(ByVal index As Integer) As String Implements IMultiItem.ItemCaption
        Get
            'Caption of item is "#. <File path>"
            Return String.Format("&{0}. {1}", index + 1, m_filePaths(index))
        End Get
    End Property

    Public ReadOnly Property ItemChecked(ByVal index As Integer) As Boolean Implements IMultiItem.ItemChecked
        Get
            Return False
        End Get
    End Property

    Public ReadOnly Property ItemEnabled(ByVal index As Integer) As Boolean Implements IMultiItem.ItemEnabled
        Get
            'check if file exists
            Return System.IO.File.Exists(m_filePaths(index))
        End Get
    End Property

    Public ReadOnly Property Message() As String Implements IMultiItem.Message
        Get
            Return String.Empty
        End Get
    End Property

    Public ReadOnly Property Name() As String Implements IMultiItem.Name
        Get
            Return "VBNETSamples_RecentFilesMultiItem"
        End Get
    End Property

    Public Sub OnItemClick(ByVal index As Integer) Implements IMultiItem.OnItemClick
        m_application.OpenDocument(m_filePaths(index))
    End Sub

    Public Function OnPopup(ByVal hook As Object) As Integer Implements IMultiItem.OnPopup
        'The incoming hook is the application, determine the number of
        'items by getting the recent files from the registry
        m_application = CType(hook, IApplication)
        m_filePaths = RecentFilesRegistryHelper.GetRecentFiles(m_application)
        Return m_filePaths.Length
    End Function
End Class


