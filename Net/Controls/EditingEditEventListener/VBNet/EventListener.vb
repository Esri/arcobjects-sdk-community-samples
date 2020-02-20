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
Imports System
Imports System.Diagnostics
Imports System.Collections.Generic
Imports System.Text
Imports ESRI.ArcGIS.Controls
Imports ESRI.ArcGIS.Geodatabase

Namespace Events
  Public Class EventListener
#Region "Members"
    Public Event Changed As ChangedEventHandler
    Dim m_editor As IEngineEditor

    'contains all edit events listed on IEngineEditEvents
    Public Enum EditorEvent
      OnAbort
      OnAfterDrawSketch
      OnBeforeStopEditing
      OnBeforeStopOperation
      OnChangeFeature
      OnConflictsDetected
      OnCreateFeature
      OnCurrentTaskChanged
      OnCurrentZChanged
      OnDeleteFeature
      OnSaveEdits
      OnSelectionChanged
      OnSketchFinished
      OnSketchModified
      OnStartEditing
      OnStartOperation
      OnStopEditing
      OnStopOperation
      OnTargetLayerChanged
      OnVertexAdded
      OnVertexMoved
      OnVertexDeleted
    End Enum

#End Region

#Region "Constructor"
    Public Sub New(ByVal editor As IEngineEditor)
      If (editor Is Nothing) Then
        Throw New ArgumentNullException
      End If
      m_editor = editor
    End Sub
#End Region

#Region "Event Registration and Handling"
    Sub OnEvent()
      Dim eventName As String = GetEventName()
      UpdateEventList(eventName)
    End Sub

    Sub OnEvent(Of T)(ByVal param As T)
      Dim eventName As String = GetEventName()
      UpdateEventList(eventName)
    End Sub

    Public Sub ListenToEvents(ByVal editEvent As EditorEvent, ByVal start As Boolean)
      Select Case editEvent
        Case EditorEvent.OnAbort
          If start Then
            AddHandler DirectCast(m_editor, IEngineEditEvents_Event).OnAbort, AddressOf OnEvent
          Else
            RemoveHandler DirectCast(m_editor, IEngineEditEvents_Event).OnAbort, AddressOf OnEvent
          End If
          Exit Select
        Case EditorEvent.OnAfterDrawSketch
          If start Then
            AddHandler DirectCast(m_editor, IEngineEditEvents_Event).OnAfterDrawSketch, AddressOf OnEvent
          Else
            RemoveHandler DirectCast(m_editor, IEngineEditEvents_Event).OnAfterDrawSketch, AddressOf OnEvent
          End If
          Exit Select
        Case EditorEvent.OnBeforeStopEditing
          If start Then
            AddHandler DirectCast(m_editor, IEngineEditEvents_Event).OnBeforeStopEditing, AddressOf OnEvent
          Else
            RemoveHandler DirectCast(m_editor, IEngineEditEvents_Event).OnBeforeStopEditing, AddressOf OnEvent
          End If
          Exit Select
        Case EditorEvent.OnBeforeStopOperation
          If start Then
            AddHandler DirectCast(m_editor, IEngineEditEvents_Event).OnBeforeStopOperation, AddressOf OnEvent
          Else
            RemoveHandler DirectCast(m_editor, IEngineEditEvents_Event).OnBeforeStopOperation, AddressOf OnEvent
          End If
          Exit Select
        Case EditorEvent.OnChangeFeature
          If start Then
            AddHandler DirectCast(m_editor, IEngineEditEvents_Event).OnChangeFeature, AddressOf OnEvent
          Else
            RemoveHandler DirectCast(m_editor, IEngineEditEvents_Event).OnChangeFeature, AddressOf OnEvent
          End If
          Exit Select
        Case EditorEvent.OnConflictsDetected
          If start Then
            AddHandler DirectCast(m_editor, IEngineEditEvents_Event).OnConflictsDetected, AddressOf OnEvent
          Else
            RemoveHandler DirectCast(m_editor, IEngineEditEvents_Event).OnConflictsDetected, AddressOf OnEvent
          End If
          Exit Select
        Case EditorEvent.OnCreateFeature
          If start Then
            AddHandler DirectCast(m_editor, IEngineEditEvents_Event).OnCreateFeature, AddressOf OnEvent
          Else
            RemoveHandler DirectCast(m_editor, IEngineEditEvents_Event).OnCreateFeature, AddressOf OnEvent
          End If
          Exit Select
        Case EditorEvent.OnCurrentTaskChanged
          If start Then
            AddHandler DirectCast(m_editor, IEngineEditEvents_Event).OnCurrentTaskChanged, AddressOf OnEvent
          Else
            RemoveHandler DirectCast(m_editor, IEngineEditEvents_Event).OnCurrentTaskChanged, AddressOf OnEvent
          End If
          Exit Select
        Case EditorEvent.OnCurrentZChanged
          If start Then
            AddHandler DirectCast(m_editor, IEngineEditEvents_Event).OnCurrentZChanged, AddressOf OnEvent
          Else
            RemoveHandler DirectCast(m_editor, IEngineEditEvents_Event).OnCurrentZChanged, AddressOf OnEvent
          End If
          Exit Select
        Case EditorEvent.OnDeleteFeature
          If start Then
            AddHandler DirectCast(m_editor, IEngineEditEvents_Event).OnDeleteFeature, AddressOf OnEvent
          Else
            RemoveHandler DirectCast(m_editor, IEngineEditEvents_Event).OnDeleteFeature, AddressOf OnEvent
          End If
          Exit Select
        Case EditorEvent.OnSaveEdits
          If start Then
            AddHandler DirectCast(m_editor, IEngineEditEvents_Event).OnSaveEdits, AddressOf OnEvent
          Else
            RemoveHandler DirectCast(m_editor, IEngineEditEvents_Event).OnSaveEdits, AddressOf OnEvent
          End If
          Exit Select
        Case EditorEvent.OnSelectionChanged
          If start Then
            AddHandler DirectCast(m_editor, IEngineEditEvents_Event).OnSelectionChanged, AddressOf OnEvent
          Else
            RemoveHandler DirectCast(m_editor, IEngineEditEvents_Event).OnSelectionChanged, AddressOf OnEvent
          End If
          Exit Select
        Case EditorEvent.OnSketchFinished
          If start Then
            AddHandler DirectCast(m_editor, IEngineEditEvents_Event).OnSketchFinished, AddressOf OnEvent
          Else
            RemoveHandler DirectCast(m_editor, IEngineEditEvents_Event).OnSketchFinished, AddressOf OnEvent
          End If
          Exit Select
        Case EditorEvent.OnSketchModified
          If start Then
            AddHandler DirectCast(m_editor, IEngineEditEvents_Event).OnSketchModified, AddressOf OnEvent
          Else
            RemoveHandler DirectCast(m_editor, IEngineEditEvents_Event).OnSketchModified, AddressOf OnEvent
          End If
          Exit Select
        Case EditorEvent.OnStartEditing
          If start Then
            AddHandler DirectCast(m_editor, IEngineEditEvents_Event).OnStartEditing, AddressOf OnEvent
          Else
            RemoveHandler DirectCast(m_editor, IEngineEditEvents_Event).OnStartEditing, AddressOf OnEvent
          End If
          Exit Select
        Case EditorEvent.OnStartOperation
          If start Then
            AddHandler DirectCast(m_editor, IEngineEditEvents_Event).OnStartOperation, AddressOf OnEvent
          Else
            RemoveHandler DirectCast(m_editor, IEngineEditEvents_Event).OnStartOperation, AddressOf OnEvent
          End If
          Exit Select
        Case EditorEvent.OnStopEditing
          If start Then
            AddHandler DirectCast(m_editor, IEngineEditEvents_Event).OnStopEditing, AddressOf OnEvent
          Else
            RemoveHandler DirectCast(m_editor, IEngineEditEvents_Event).OnStopEditing, AddressOf OnEvent
          End If
          Exit Select
        Case EditorEvent.OnStopOperation
          If start Then
            AddHandler DirectCast(m_editor, IEngineEditEvents_Event).OnStopOperation, AddressOf OnEvent
          Else
            RemoveHandler DirectCast(m_editor, IEngineEditEvents_Event).OnStopOperation, AddressOf OnEvent
          End If
          Exit Select
        Case EditorEvent.OnTargetLayerChanged
          If start Then
            AddHandler DirectCast(m_editor, IEngineEditEvents_Event).OnTargetLayerChanged, AddressOf OnEvent
          Else
            RemoveHandler DirectCast(m_editor, IEngineEditEvents_Event).OnTargetLayerChanged, AddressOf OnEvent
          End If
          Exit Select
        Case EditorEvent.OnVertexAdded
          If start Then
            AddHandler DirectCast(m_editor, IEngineEditEvents_Event).OnVertexAdded, AddressOf OnEvent
          Else
            RemoveHandler DirectCast(m_editor, IEngineEditEvents_Event).OnVertexAdded, AddressOf OnEvent
          End If
          Exit Select
        Case EditorEvent.OnVertexMoved
          If start Then
            AddHandler DirectCast(m_editor, IEngineEditEvents_Event).OnVertexMoved, AddressOf OnEvent
          Else
            RemoveHandler DirectCast(m_editor, IEngineEditEvents_Event).OnVertexMoved, AddressOf OnEvent
          End If
          Exit Select
        Case EditorEvent.OnVertexDeleted
          If start Then
            AddHandler DirectCast(m_editor, IEngineEditEvents_Event).OnVertexDeleted, AddressOf OnEvent
          Else
            RemoveHandler DirectCast(m_editor, IEngineEditEvents_Event).OnVertexDeleted, AddressOf OnEvent
          End If
          Exit Select
        Case Else
          Throw New ArgumentOutOfRangeException()
      End Select
    End Sub

    Function GetEventName() As String
      'Get the name of the ArcEngine calling method and use this to indicate the event that was fired
      Dim st As StackTrace = New System.Diagnostics.StackTrace()
      Dim sf As StackFrame = st.GetFrame(2)
      GetEventName = sf.GetMethod().Name
    End Function

    Sub UpdateEventList(ByVal eventName As String)
      Dim e As EditorEventArgs = New EditorEventArgs(eventName)
      RaiseEvent Changed(Me, e)
    End Sub

#End Region
  End Class

  Public Class EditorEventArgs
    Inherits EventArgs
    Public Sub New(ByVal eventType As String)
      Me.eventType = eventType
    End Sub
    Public eventType As String
  End Class

  Public Delegate Sub ChangedEventHandler(ByVal sender As Object, ByVal e As EditorEventArgs)
End Namespace


