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
Imports System.Collections.Generic
Imports System.Data
Imports System.Text
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.SystemUI
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.ADF.CATIDs
Imports System.Windows.Forms

''' <summary>
''' Designer class of the dockable window add-in. It contains user interfaces that
''' make up the dockable window.
''' </summary>
Partial Public Class EditorEventsDialog

    Friend Shared e_dockWinForm As EditorEventsDialog
    Friend m_selectTab As TabPage
    Friend m_listenTab As TabPage
    Friend tabControl As TabControl
    Private eventListener As EventListener
    Private m_editor As ESRI.ArcGIS.Editor.IEditor
    Private WithEvents editEventList As CheckedListBox
    Private WithEvents listEvent As ListBox

    Public Sub New(ByVal hook As Object)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        e_dockWinForm = Me

        ' Add any initialization after the InitializeComponent() call.
        Me.Hook = hook

        'Get a reference to the editor.
        Dim uidEditor As New UID
        uidEditor.Value = "esriEditor.Editor"
        m_editor = TryCast(My.ArcMap.Application.FindExtensionByCLSID(uidEditor), ESRI.ArcGIS.Editor.IEditor)

        tabControl = e_dockWinForm.tabControl1
        Dim e As System.Collections.IEnumerator = tabControl.TabPages.GetEnumerator()
        e.MoveNext()
        m_listenTab = TryCast(e.Current, TabPage)
        e.MoveNext()
        m_selectTab = TryCast(e.Current, TabPage)
        Dim editEventList As CheckedListBox = TryCast(m_selectTab.GetNextControl(m_selectTab, True), CheckedListBox)
        AddHandler editEventList.ItemCheck, AddressOf editEventList_ItemCheck

        Dim listEvent As ListBox = TryCast(m_listenTab.GetNextControl(m_listenTab, True), ListBox)
        AddHandler listEvent.MouseDown, AddressOf listEvent_MouseDown

        eventListener = New EventListener(m_editor)

        AddHandler eventListener.Changed, AddressOf eventListener_Changed

        'populate the editor events
        editEventList.Items.AddRange(System.Enum.GetNames(GetType(EventListener.EditorEvent)))
    End Sub


    Private m_hook As Object
    ''' <summary>
    ''' Host object of the dockable window
    ''' </summary> 
    Public Property Hook() As Object
        Get
            Return m_hook
        End Get
        Set(ByVal value As Object)
            m_hook = value
        End Set
    End Property

    ''' <summary>
    ''' Implementation class of the dockable window add-in. It is responsible for
    ''' creating and disposing the user interface class for the dockable window.
    ''' </summary>
    Public Class AddinImpl
        Inherits ESRI.ArcGIS.Desktop.AddIns.DockableWindow

        Private m_windowUI As EditorEventsDialog

        Protected Overrides Function OnCreateChild() As System.IntPtr
            m_windowUI = New EditorEventsDialog(Me.Hook)
            Return m_windowUI.Handle
        End Function

        Protected Overrides Sub Dispose(ByVal Param As Boolean)
            If m_windowUI IsNot Nothing Then
                m_windowUI.Dispose(Param)
            End If

            MyBase.Dispose(Param)
        End Sub

    End Class

    Private Sub editEventList_ItemCheck(ByVal sender As Object, _
    ByVal e As ItemCheckEventArgs) Handles editEventList.ItemCheck
        ' start or stop listening for event based on checked state
        eventListener.ListenToEvents(CType(e.Index, EventListener.EditorEvent), e.NewValue = CheckState.Checked)
    End Sub

    Private Sub listEvent_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) Handles listEvent.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Right Then
            Me.lstEditorEvents.Items.Clear()
            Me.lstEditorEvents.Refresh()
        End If
    End Sub

    Private Sub eventListener_Changed(ByVal sender As Object, ByVal e As EditorEventArgs)
        CType(m_listenTab.GetNextControl(m_listenTab, True), ListBox).Items.Add(e.eventType.ToString())
    End Sub

End Class