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
Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports ESRI.ArcGIS.Framework

Namespace SelectionSample
  Partial Public Class SelCountDockWin
    Inherits UserControl
    Private Shared s_listView As System.Windows.Forms.ListView
    Private Shared s_label As Label

    Private Shared s_enabled As Boolean

    Public Sub New(ByVal hook As Object)
      InitializeComponent()
      Me.Hook = hook

      s_listView = listView1
      s_label = label1
      listView1.View = View.Details
    End Sub

    Friend Shared ReadOnly Property Exists() As Boolean
      Get
        Return If((s_listView Is Nothing), False, True)
      End Get
    End Property

    Friend Shared Sub Clear()
      If s_listView IsNot Nothing Then
        s_listView.Items.Clear()
      End If
    End Sub

    Friend Shared Sub AddItem(ByVal layerName As String, ByVal selectionCount As Integer)
      If s_listView Is Nothing Then
        Return
      End If

      Dim item As New ListViewItem(layerName)
      item.SubItems.Add(selectionCount.ToString())
      s_listView.Items.Add(item)
    End Sub

    Friend Shared Sub SetEnabled(ByVal enabled As Boolean)
      s_enabled = enabled

      ' if the dockable window was never displayed, listview could be null
      If s_listView Is Nothing Then
        Return
      End If

      If enabled Then
        s_label.Visible = False
        s_listView.Visible = True
      Else
        Clear()
        s_label.Visible = True
        s_listView.Visible = False
      End If
    End Sub

    ''' <summary>
    ''' Host object of the dockable window
    ''' </summary>
    Private privateHook As Object
    Private Property Hook() As Object
      Get
        Return privateHook
      End Get
      Set(ByVal value As Object)
        privateHook = value
      End Set
    End Property

    ''' <summary>
    ''' Implementation class of the dockable window add-in. It is responsible for 
    ''' creating and disposing the user interface class of the dockable window.
    ''' </summary>
    Public Class AddinImpl
      Inherits ESRI.ArcGIS.Desktop.AddIns.DockableWindow
      Private m_windowUI As SelCountDockWin

      Public Sub New()
      End Sub

      Protected Overrides Function OnCreateChild() As IntPtr
        m_windowUI = New SelCountDockWin(Me.Hook)
        Return m_windowUI.Handle
      End Function

      Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If m_windowUI IsNot Nothing Then
          m_windowUI.Dispose(disposing)
        End If

        MyBase.Dispose(disposing)
      End Sub

    End Class
  End Class
End Namespace
