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
Imports ESRI.ArcGIS.SystemUI
Imports System.Runtime.InteropServices
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.ArcMapUI

<ComClass(LogLineMultiItemCmd.ClassId, LogLineMultiItemCmd.InterfaceId, LogLineMultiItemCmd.EventsId), _
 ProgId("SimpleLogWindowVB.LogLineMultiItemCmd")> _
Public Class LogLineMultiItemCmd
    Implements ESRI.ArcGIS.SystemUI.IMultiItem
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
        GxCommands.Register(regKey)
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
        GxCommands.Unregister(regKey)
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
    Public Const ClassId As String = "a0e848c1-b873-4604-b4c0-3e4b6a901c79"
    Public Const InterfaceId As String = "aba47356-846b-4770-901f-e424d11f91d9"
    Public Const EventsId As String = "e8c98041-1026-4c02-be21-8071ff1397e5"
#End Region

    Private m_targetListBox As System.Windows.Forms.ListBox

    ' A creatable COM class must have a Public Sub New() 
    ' with no parameters, otherwise, the class will not be 
    ' registered in the COM registry and cannot be created 
    ' via CreateObject.
    Public Sub New()
        MyBase.New()
    End Sub

    Public ReadOnly Property Caption() As String Implements ESRI.ArcGIS.SystemUI.IMultiItem.Caption
        Get
            Return "Delete log by line (VB.Net)"
        End Get
    End Property

    Public ReadOnly Property HelpContextID() As Integer Implements ESRI.ArcGIS.SystemUI.IMultiItem.HelpContextID
        Get
            Return 0
        End Get
    End Property

    Public ReadOnly Property HelpFile() As String Implements ESRI.ArcGIS.SystemUI.IMultiItem.HelpFile
        Get
            Return ""
        End Get
    End Property

    Public ReadOnly Property ItemBitmap(ByVal index As Integer) As Integer Implements ESRI.ArcGIS.SystemUI.IMultiItem.ItemBitmap
        Get
            Return 0
        End Get
    End Property

    Public ReadOnly Property ItemCaption(ByVal index As Integer) As String Implements ESRI.ArcGIS.SystemUI.IMultiItem.ItemCaption
        Get
            If index > -1 Then
                Dim formatMessage As String = m_targetListBox.Items(index)
                If formatMessage.Length > 25 Then 'Trim display string
                    formatMessage = formatMessage.Substring(0, 11) + "..." + formatMessage.Substring(formatMessage.Length - 11)
                End If
                Return String.Format("Delete line {0}: {1}", index + 1, formatMessage)
            End If

            Return ""
        End Get
    End Property

    Public ReadOnly Property ItemChecked(ByVal index As Integer) As Boolean Implements ESRI.ArcGIS.SystemUI.IMultiItem.ItemChecked
        Get
            Return False
        End Get
    End Property

    Public ReadOnly Property ItemEnabled(ByVal index As Integer) As Boolean Implements ESRI.ArcGIS.SystemUI.IMultiItem.ItemEnabled
        Get
            Return True
        End Get
    End Property

    Public ReadOnly Property Message() As String Implements ESRI.ArcGIS.SystemUI.IMultiItem.Message
        Get
            Return "Delete a specific line in the simple log dockable window"
        End Get
    End Property

    Public ReadOnly Property Name() As String Implements ESRI.ArcGIS.SystemUI.IMultiItem.Name
        Get
            Return "VBNETSamples_DeleteLogLineCommand"
        End Get
    End Property

    Public Sub OnItemClick(ByVal index As Integer) Implements ESRI.ArcGIS.SystemUI.IMultiItem.OnItemClick
        If index > -1 Then
            m_targetListBox.Items.RemoveAt(index) 'Delete the line
        End If
    End Sub

    Public Function OnPopup(ByVal hook As Object) As Integer Implements ESRI.ArcGIS.SystemUI.IMultiItem.OnPopup
        Dim app As IApplication = DirectCast(hook, IApplication)

        'This command is designed to be on a context menu displayed when the 
        'logging window is right-clicked. Get the context item of the application
        Dim doc As IDocument = app.Document
        Dim contextItem As Object
        If TypeOf doc Is IBasicDocument Then
            contextItem = DirectCast(doc, IBasicDocument).ContextItem
        End If

        Dim dockWin As IDockableWindow
        Dim logWindowID As New UIDClass()
        logWindowID.Value = "{8582b32d-120c-407b-af34-8719b8960b30}"
        If contextItem IsNot Nothing AndAlso TypeOf contextItem Is IDockableWindow Then
            dockWin = DirectCast(contextItem, IDockableWindow)
        Else    'In the case of ArcCatalog or the command has been placed outside the designated context menu
            'Get the dockable window directly
            Dim dockWindowManager As IDockableWindowManager = DirectCast(app, IDockableWindowManager)
            dockWin = dockWindowManager.GetDockableWindow(logWindowID)
        End If

        'Get list item count in the dockable window
        If dockWin IsNot Nothing AndAlso dockWin.ID.Compare(logWindowID) Then
            m_targetListBox = DirectCast(dockWin.UserData, System.Windows.Forms.ListBox)
            Return m_targetListBox.Items.Count
        End If

        Return 0    'failed or not applicable
    End Function
End Class


