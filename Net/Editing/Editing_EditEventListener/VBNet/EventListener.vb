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
Imports System.Collections.Generic
Imports System.Text
Imports System.Runtime.InteropServices
Imports ESRI.ArcGIS.Editor

<ComClass(EventListener.ClassId, EventListener.InterfaceId, EventListener.EventsId), _
 ProgId("VBNet.EventListener")> _
Public Class EventListener

#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "6c41019b-5be8-4c55-9fbe-ddf63fe0c67a"
    Public Const InterfaceId As String = "e661f2e8-c840-4d50-a93e-a88e2308c4c6"
    Public Const EventsId As String = "39bbcf43-ffce-4504-b9b6-8c35bb6d7eed"
#End Region

    ' A creatable COM class must have a Public Sub New() 
    ' with no parameters, otherwise, the class will not be 
    ' registered in the COM registry and cannot be created 
    ' via CreateObject.
    Public Sub New()
        MyBase.New()
    End Sub

#Region "Members"
    Public Event Changed As ChangedEventHandler
    Private m_editor As IEditor
#End Region

#Region "Enums"
    'contains all edit events listed on IEditEvents through IEditEvents4
    Public Enum EditorEvent
        AfterDrawSketch
        OnChangeFeature
        OnConflictsDetected
        OnCreateFeature
        OnCurrentLayerChanged
        OnCurrentTaskChanged
        OnDeleteFeature
        OnRedo
        OnSelectionChanged
        OnSketchFinished
        OnSketchModified
        OnStartEditing
        OnStopEditing
        OnUndo
        BeforeStopEditing
        BeforeStopOperation
        OnVertexAdded
        OnVertexMoved
        OnVertexDeleted
        BeforeDrawSketch
        OnAngularCorrectionOffsetChanged
        OnDistanceCorrectionFactorChanged
        OnUseGroundToGridChanged
    End Enum
#End Region

    ' Invoke the Changed event
    Protected Overridable Sub OnChanged(ByVal e As EditorEventArgs)
        If Not ChangedEvent Is Nothing Then
            RaiseEvent Changed(Me, e)
        End If
    End Sub

    ''' <summary>
    ''' Adds or removes the correct event handler based on the event and if it is checked.
    ''' </summary>
    ''' <param name="editEvent"></param>
    ''' <param name="start"></param>
    Public Sub ListenToEvents(ByVal editEvent As EditorEvent, ByVal start As Boolean)
        Select Case editEvent
            Case EditorEvent.AfterDrawSketch
                If start Then
                    AddHandler (CType(m_editor, IEditEvents_Event)).AfterDrawSketch, AddressOf EventListener_AfterDrawSketch
                Else
                    RemoveHandler (CType(m_editor, IEditEvents_Event)).AfterDrawSketch, AddressOf EventListener_AfterDrawSketch
                End If

            Case EditorEvent.OnChangeFeature
                If start Then
                    AddHandler (CType(m_editor, IEditEvents_Event)).OnChangeFeature, AddressOf EventListener_OnChangeFeature
                Else
                    RemoveHandler (CType(m_editor, IEditEvents_Event)).OnChangeFeature, AddressOf EventListener_OnChangeFeature
                End If

            Case EditorEvent.OnConflictsDetected
                If start Then
                    AddHandler (CType(m_editor, IEditEvents_Event)).OnConflictsDetected, AddressOf EventListener_OnConflictsDetected
                Else
                    RemoveHandler (CType(m_editor, IEditEvents_Event)).OnConflictsDetected, AddressOf EventListener_OnConflictsDetected
                End If

            Case EditorEvent.OnCreateFeature
                If start Then
                    AddHandler (CType(m_editor, IEditEvents_Event)).OnCreateFeature, AddressOf EventListener_OnCreateFeature
                Else
                    RemoveHandler (CType(m_editor, IEditEvents_Event)).OnCreateFeature, AddressOf EventListener_OnCreateFeature
                End If

            Case EditorEvent.OnCurrentLayerChanged
                If start Then
                    AddHandler (CType(m_editor, IEditEvents_Event)).OnCurrentLayerChanged, AddressOf EventListener_OnCurrentLayerChanged
                Else
                    RemoveHandler (CType(m_editor, IEditEvents_Event)).OnCurrentLayerChanged, AddressOf EventListener_OnCurrentLayerChanged
                End If

            Case EditorEvent.OnCurrentTaskChanged
                If start Then
                    AddHandler (CType(m_editor, IEditEvents_Event)).OnCurrentTaskChanged, AddressOf OnCurrentTaskChanged
                Else
                    RemoveHandler (CType(m_editor, IEditEvents_Event)).OnCurrentTaskChanged, AddressOf OnCurrentTaskChanged
                End If

            Case EditorEvent.OnDeleteFeature
                If start Then
                    AddHandler (CType(m_editor, IEditEvents_Event)).OnDeleteFeature, AddressOf EventListener_OnDeleteFeature
                Else
                    RemoveHandler (CType(m_editor, IEditEvents_Event)).OnDeleteFeature, AddressOf EventListener_OnDeleteFeature
                End If

            Case EditorEvent.OnRedo
                If start Then
                    AddHandler (CType(m_editor, IEditEvents_Event)).OnRedo, AddressOf EventListener_OnRedo
                Else
                    RemoveHandler (CType(m_editor, IEditEvents_Event)).OnRedo, AddressOf EventListener_OnRedo
                End If

            Case EditorEvent.OnSelectionChanged
                If start Then
                    AddHandler (CType(m_editor, IEditEvents_Event)).OnSelectionChanged, AddressOf EventListener_OnSelectionChanged
                Else
                    RemoveHandler (CType(m_editor, IEditEvents_Event)).OnSelectionChanged, AddressOf EventListener_OnSelectionChanged
                End If

            Case EditorEvent.OnSketchFinished
                If start Then
                    AddHandler (CType(m_editor, IEditEvents_Event)).OnSketchFinished, AddressOf EventListener_OnSketchFinished
                Else
                    RemoveHandler (CType(m_editor, IEditEvents_Event)).OnSketchFinished, AddressOf EventListener_OnSketchFinished
                End If

            Case EditorEvent.OnSketchModified
                If start Then
                    AddHandler (CType(m_editor, IEditEvents_Event)).OnSketchModified, AddressOf EventListener_OnSketchModified
                Else
                    RemoveHandler (CType(m_editor, IEditEvents_Event)).OnSketchModified, AddressOf EventListener_OnSketchModified
                End If

            Case EditorEvent.OnStartEditing
                If start Then
                    AddHandler (CType(m_editor, IEditEvents_Event)).OnStartEditing, AddressOf OnStartEditing
                Else
                    RemoveHandler (CType(m_editor, IEditEvents_Event)).OnStartEditing, AddressOf OnStartEditing
                End If

            Case EditorEvent.OnStopEditing
                If start Then
                    AddHandler (CType(m_editor, IEditEvents_Event)).OnStopEditing, AddressOf OnStopEditing
                Else
                    RemoveHandler (CType(m_editor, IEditEvents_Event)).OnStopEditing, AddressOf OnStopEditing
                End If

            Case EditorEvent.OnUndo
                If start Then
                    AddHandler (CType(m_editor, IEditEvents_Event)).OnUndo, AddressOf EventListener_OnUndo
                Else
                    RemoveHandler (CType(m_editor, IEditEvents_Event)).OnUndo, AddressOf EventListener_OnUndo
                End If

            Case EditorEvent.BeforeStopEditing
                If start Then
                    AddHandler (CType(m_editor, IEditEvents2_Event)).BeforeStopEditing, AddressOf EventListener_BeforeStopEditing
                Else
                    RemoveHandler (CType(m_editor, IEditEvents2_Event)).BeforeStopEditing, AddressOf EventListener_BeforeStopEditing
                End If

            Case EditorEvent.BeforeStopOperation
                If start Then
                    AddHandler (CType(m_editor, IEditEvents2_Event)).BeforeStopOperation, AddressOf EventListener_BeforeStopOperation
                Else
                    RemoveHandler (CType(m_editor, IEditEvents2_Event)).BeforeStopOperation, AddressOf EventListener_BeforeStopOperation
                End If

            Case EditorEvent.OnVertexAdded
                If start Then
                    AddHandler (CType(m_editor, IEditEvents2_Event)).OnVertexAdded, AddressOf EventListener_OnVertexAdded
                Else
                    RemoveHandler (CType(m_editor, IEditEvents2_Event)).OnVertexAdded, AddressOf EventListener_OnVertexAdded
                End If

            Case EditorEvent.OnVertexMoved
                If start Then
                    AddHandler (CType(m_editor, IEditEvents2_Event)).OnVertexMoved, AddressOf EventListener_OnVertexMoved
                Else
                    RemoveHandler (CType(m_editor, IEditEvents2_Event)).OnVertexMoved, AddressOf EventListener_OnVertexMoved
                End If

            Case EditorEvent.OnVertexDeleted
                If start Then
                    AddHandler (CType(m_editor, IEditEvents2_Event)).OnVertexDeleted, AddressOf EventListener_OnVertexDeleted
                Else
                    RemoveHandler (CType(m_editor, IEditEvents2_Event)).OnVertexDeleted, AddressOf EventListener_OnVertexDeleted
                End If

            Case EditorEvent.BeforeDrawSketch
                If start Then
                    AddHandler (CType(m_editor, IEditEvents3_Event)).BeforeDrawSketch, AddressOf EventListener_BeforeDrawSketch
                Else
                    RemoveHandler (CType(m_editor, IEditEvents3_Event)).BeforeDrawSketch, AddressOf EventListener_BeforeDrawSketch
                End If

            Case EditorEvent.OnAngularCorrectionOffsetChanged
                If start Then
                    AddHandler (CType(m_editor, IEditEvents4_Event)).OnAngularCorrectionOffsetChanged, AddressOf EventListener_OnAngularCorrectionOffsetChanged
                Else
                    RemoveHandler (CType(m_editor, IEditEvents4_Event)).OnAngularCorrectionOffsetChanged, AddressOf EventListener_OnAngularCorrectionOffsetChanged
                End If

            Case EditorEvent.OnDistanceCorrectionFactorChanged
                If start Then
                    AddHandler (CType(m_editor, IEditEvents4_Event)).OnDistanceCorrectionFactorChanged, AddressOf EventListener_OnDistanceCorrectionFactorChanged
                Else
                    RemoveHandler (CType(m_editor, IEditEvents4_Event)).OnDistanceCorrectionFactorChanged, AddressOf EventListener_OnDistanceCorrectionFactorChanged
                End If

            Case EditorEvent.OnUseGroundToGridChanged
                If start Then
                    AddHandler (CType(m_editor, IEditEvents4_Event)).OnUseGroundToGridChanged, AddressOf EventListener_OnUseGroundToGridChanged
                Else
                    RemoveHandler (CType(m_editor, IEditEvents4_Event)).OnUseGroundToGridChanged, AddressOf EventListener_OnUseGroundToGridChanged
                End If

            Case Else
        End Select
    End Sub

#Region "Constructors"
    Public Sub New(ByVal editor As IEditor)
        If editor Is Nothing Then
            Throw New ArgumentNullException()
        End If

        m_editor = editor
    End Sub

    Public Sub New(ByVal editor As IEditor, ByVal editEvent As EditorEvent)
        If editor Is Nothing Then
            Throw New ArgumentNullException()
        End If

        m_editor = editor
    End Sub

    Private Sub New(ByVal editor As IEditor, ByVal bListenAll As Boolean)
        If editor Is Nothing Then
            Throw New ArgumentNullException()
        End If

        m_editor = editor
    End Sub

#End Region

#Region "Event Handlers"
    Private Sub EventListener_OnCreateFeature(ByVal obj As ESRI.ArcGIS.Geodatabase.IObject)
        Dim e As EditorEventArgs = New EditorEventArgs("OnCreateFeature")
        OnChanged(e)
    End Sub

    Private Sub EventListener_OnChangeFeature(ByVal obj As ESRI.ArcGIS.Geodatabase.IObject)
        Dim e As EditorEventArgs = New EditorEventArgs("OnChangeFeature")
        OnChanged(e)
    End Sub

    Private Sub EventListener_OnConflictsDetected()
        Dim e As EditorEventArgs = New EditorEventArgs("OnConflictsDetected")
        OnChanged(e)
    End Sub

    Private Sub EventListener_OnCurrentLayerChanged()
        Dim e As EditorEventArgs = New EditorEventArgs("OnCurrentLayerChanged")
        OnChanged(e)
    End Sub

    Private Sub EventListener_OnDeleteFeature(ByVal obj As ESRI.ArcGIS.Geodatabase.IObject)
        Dim e As EditorEventArgs = New EditorEventArgs("OnDeleteFeature")
        OnChanged(e)
    End Sub

    Private Sub EventListener_OnRedo()
        Dim e As EditorEventArgs = New EditorEventArgs("OnRedo")
        OnChanged(e)
    End Sub

    Private Sub EventListener_OnSelectionChanged()
        Dim e As EditorEventArgs = New EditorEventArgs("OnSelectionChanged")
        OnChanged(e)
    End Sub

    Private Sub EventListener_OnSketchFinished()
        Dim e As EditorEventArgs = New EditorEventArgs("OnSketchFinished")
        OnChanged(e)
    End Sub

    Private Sub EventListener_OnSketchModified()
        Dim e As EditorEventArgs = New EditorEventArgs("OnSketchModified")
        OnChanged(e)
    End Sub

    Private Sub EventListener_OnUndo()
        Dim e As EditorEventArgs = New EditorEventArgs("OnUndo")
        OnChanged(e)
    End Sub

    Private Sub EventListener_BeforeStopEditing(ByVal save As Boolean)
        Dim e As EditorEventArgs = New EditorEventArgs("BeforeStopEditing")
        OnChanged(e)
    End Sub

    Private Sub EventListener_BeforeStopOperation()
        Dim e As EditorEventArgs = New EditorEventArgs("BeforeStopOperation")
        OnChanged(e)
    End Sub

    Private Sub EventListener_OnVertexAdded(ByVal point As ESRI.ArcGIS.Geometry.IPoint)
        Dim e As EditorEventArgs = New EditorEventArgs("OnVertexAdded")
        OnChanged(e)
    End Sub

    Private Sub EventListener_OnVertexMoved(ByVal point As ESRI.ArcGIS.Geometry.IPoint)
        Dim e As EditorEventArgs = New EditorEventArgs("OnVertexMoved")
        OnChanged(e)
    End Sub

    Private Sub EventListener_OnVertexDeleted(ByVal point As ESRI.ArcGIS.Geometry.IPoint)
        Dim e As EditorEventArgs = New EditorEventArgs("OnVertexDeleted")
        OnChanged(e)
    End Sub

    Private Sub EventListener_OnAngularCorrectionOffsetChanged(ByVal angOffset As Double)
        Dim e As EditorEventArgs = New EditorEventArgs("OnAngularCorrectionOffsetChanged")
        OnChanged(e)
    End Sub

    Private Sub EventListener_OnDistanceCorrectionFactorChanged(ByVal distFactor As Double)
        Dim e As EditorEventArgs = New EditorEventArgs("OnDistanceCorrectionFactorChanged")
        OnChanged(e)
    End Sub

    Private Sub EventListener_OnUseGroundToGridChanged(ByVal g2g As Boolean)
        Dim e As EditorEventArgs = New EditorEventArgs("OnUseGroundToGridChanged")
        OnChanged(e)
    End Sub

    Private Sub EventListener_BeforeDrawSketch(ByVal pDpy As ESRI.ArcGIS.Display.IDisplay)
        Dim e As EditorEventArgs = New EditorEventArgs("BeforeDrawSketch")
        OnChanged(e)
    End Sub

    Private Sub EventListener_AfterDrawSketch(ByVal pDpy As ESRI.ArcGIS.Display.IDisplay)
        Dim e As EditorEventArgs = New EditorEventArgs("AfterDrawSketch")
        OnChanged(e)
    End Sub

    Private Sub OnCurrentTaskChanged()
        Dim e As EditorEventArgs = New EditorEventArgs("OnCurrentTaskChanged")
        OnChanged(e)
    End Sub

    Private Sub OnStopEditing(ByVal SaveEdits As Boolean)
        Dim e As EditorEventArgs = New EditorEventArgs("OnStopEditing")
        OnChanged(e)
    End Sub

    Private Sub OnStartEditing()
        Dim e As EditorEventArgs = New EditorEventArgs("OnStartEditing")
        OnChanged(e)
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



