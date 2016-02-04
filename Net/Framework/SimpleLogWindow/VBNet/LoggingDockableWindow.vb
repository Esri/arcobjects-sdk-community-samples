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
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.SystemUI
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.ArcMapUI

<ComClass(LoggingDockableWindow.ClassId, LoggingDockableWindow.InterfaceId, LoggingDockableWindow.EventsId), _
 ProgId("SimpleLogWindowVB.LoggingDockableWindow")> _
Public Class LoggingDockableWindow
    Implements IDockableWindowDef

#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "8582b32d-120c-407b-af34-8719b8960b30"
    Public Const InterfaceId As String = "fec13c95-bd53-44cd-b8ef-f7d83f33fbb2"
    Public Const EventsId As String = "1e9d36cc-df74-40c4-a8a7-461a0f75b13d"
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
        GMxDockableWindows.Register(regKey)
        MxDockableWindows.Register(regKey)
        SxDockableWindows.Register(regKey)
        GxDockableWindows.Register(regKey)

    End Sub
    ''' <summary>
    ''' Required method for ArcGIS Component Category unregistration -
    ''' Do not modify the contents of this method with the code editor.
    ''' </summary>
    Private Shared Sub ArcGISCategoryUnregistration(ByVal registerType As Type)
        Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
        GMxDockableWindows.Unregister(regKey)
        MxDockableWindows.Unregister(regKey)
        SxDockableWindows.Unregister(regKey)
        GxDockableWindows.Unregister(regKey)

    End Sub

#End Region
#End Region

    Private m_application As IApplication

#Region "IDockableWindowDef Members"

    Public ReadOnly Property Caption() As String Implements ESRI.ArcGIS.Framework.IDockableWindowDef.Caption
        Get
            Return "Simple Logging (VB.Net)"
        End Get
    End Property

    Public ReadOnly Property ChildHWND() As Integer Implements ESRI.ArcGIS.Framework.IDockableWindowDef.ChildHWND
        Get
            Return Me.Handle.ToInt32()
        End Get
    End Property

    Public ReadOnly Property Name1() As String Implements ESRI.ArcGIS.Framework.IDockableWindowDef.Name
        Get
            Return "SimpleLoggingDockableWindowVB"
        End Get
    End Property

    Public Sub OnCreate(ByVal hook As Object) Implements ESRI.ArcGIS.Framework.IDockableWindowDef.OnCreate
        m_application = CType(hook, IApplication)
    End Sub

    Public Sub OnDestroy() Implements ESRI.ArcGIS.Framework.IDockableWindowDef.OnDestroy

    End Sub

    Public ReadOnly Property UserData() As Object Implements ESRI.ArcGIS.Framework.IDockableWindowDef.UserData
        Get
            Return lstPendingMessage
        End Get
    End Property
#End Region

    Private Sub radCtxMenu_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles radFramework.CheckedChanged, radDynamic.CheckedChanged, radDotNetCtx.CheckedChanged
        If radDotNetCtx.Checked Then
            lstPendingMessage.ContextMenuStrip = ctxMenuStrip
        Else
            lstPendingMessage.ContextMenuStrip = Nothing
        End If
    End Sub


#Region "Handling private pure .Net context menu strip"
    Private Sub ctxMenuStrip_Opening(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles ctxMenuStrip.Opening
        'Clear item first
        ctxMenuStrip.Items.Clear()

        'Add items
        ctxMenuStrip.Items.Add(clearToolStripMenuItem)
        ctxMenuStrip.Items.Add(toolStripSeparator1)

        For i As Integer = 0 To lstPendingMessage.Items.Count - 1
            Dim formatMessage As String = lstPendingMessage.Items(i)
            If formatMessage.Length > 25 Then 'Trim display string
                formatMessage = formatMessage.Substring(0, 11) + "..." + formatMessage.Substring(formatMessage.Length - 11)
            End If
            ctxMenuStrip.Items.Add(String.Format("Delete line {0}: {1}", i + 1, formatMessage))
        Next
    End Sub

    Private Sub ctxMenuStrip_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles ctxMenuStrip.ItemClicked
        If e.ClickedItem IsNot clearToolStripMenuItem Then
            Dim lineNum As Integer = ctxMenuStrip.Items.IndexOf(e.ClickedItem)
            lstPendingMessage.Items.RemoveAt(lineNum)
        End If
    End Sub

    Private Sub clearToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles clearToolStripMenuItem.Click
        lstPendingMessage.Items.Clear()
    End Sub
#End Region

    Private Sub lstPendingMessage_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lstPendingMessage.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Right AndAlso _
            lstPendingMessage.ContextMenuStrip Is Nothing Then

            'Before showing the context menu, set ContextItem to be the actual dockable window 
            'which represents this definition class in the framework
            Dim doc As IDocument = m_application.Document
            Dim windowManager As IDockableWindowManager = DirectCast(m_application, IDockableWindowManager)
            Dim dockwinID As New UIDClass()
            dockwinID.Value = Me.GetType().GUID.ToString("B")
            Dim frameworkWindow As IDockableWindow = windowManager.GetDockableWindow(dockwinID)
            If TypeOf doc Is IBasicDocument Then
                Dim basicDocument As IBasicDocument = DirectCast(doc, IBasicDocument)
                basicDocument.ContextItem = frameworkWindow
            End If

            'Get the context menu to show
            Dim documentBars As ICommandBars = m_application.Document.CommandBars
            Dim ctxMenu As ICommandBar
            If radDynamic.Checked Then 'Create context menu dynamically
                'Disadvantage(s)
                '1. ICommandBars.Create will set document to dirty
                '2. Cannot insert separator
                ctxMenu = documentBars.Create("DockableWindowCtxTempVB", esriCmdBarType.esriCmdBarTypeShortcutMenu)

                'Add commands to context menu
                Dim cmdID As New UIDClass()
                cmdID.Value = "{" + ClearLoggingCommand.ClassId + "}"
                ctxMenu.Add(cmdID)

                cmdID.Value = "{" + LogLineMultiItemCmd.ClassId + "}"
                ctxMenu.Add(cmdID)
            Else 'Use predefined context menu
                Dim menuID As New UIDClass()
                menuID.Value = "{" + LoggingWindowCtxMnu.ClassId + "}"
                Dim locateMenu As ICommandItem = documentBars.Find(menuID)
                If locateMenu IsNot Nothing Then
                    ctxMenu = TryCast(locateMenu, ICommandBar)
                End If
            End If

            'Pop up context menu at screen location
            Dim scrnPoint As Drawing.Point = lstPendingMessage.PointToScreen(e.Location)
            If ctxMenu IsNot Nothing Then _
                ctxMenu.Popup(scrnPoint.X, scrnPoint.Y)
        End If
    End Sub

    ''' <summary>
    ''' Enter text into listbox by listening to the Enter key. KeyPress won't be fired for enter key
    ''' so listening to key up event instead
    ''' </summary>
    Private Sub txtInput_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtInput.KeyUp
        If e.KeyCode = System.Windows.Forms.Keys.Return Then
            If Not String.IsNullOrEmpty(txtInput.Text.Trim()) Then
                lstPendingMessage.Items.Add(txtInput.Text)
                txtInput.Clear()
                e.Handled = True
            End If
        End If
    End Sub
End Class
