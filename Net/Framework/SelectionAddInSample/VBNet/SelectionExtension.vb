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
Imports System.Text
Imports System.IO
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Desktop.AddIns

Imports My

Namespace SelectionSample

  Public Class SelectionExtension
    Inherits ESRI.ArcGIS.Desktop.AddIns.Extension

    Private m_map As IMap
    Private m_hasSelectableLayer As Boolean
    Private Shared s_dockWindow As ESRI.ArcGIS.Framework.IDockableWindow
    Private Shared s_extension As SelectionExtension


    ' Overrides

    Protected Overrides Sub OnStartup()
      s_extension = Me

      ' Wire up events
      AddHandler ArcMap.Events.NewDocument, AddressOf ArcMap_NewOpenDocument
      AddHandler ArcMap.Events.OpenDocument, AddressOf ArcMap_NewOpenDocument

      Initialize()
    End Sub

    Protected Overloads Overrides Sub OnShutdown()
      Uninitialize()


      RemoveHandler ArcMap.Events.NewDocument, AddressOf ArcMap_NewOpenDocument
      RemoveHandler ArcMap.Events.OpenDocument, AddressOf ArcMap_NewOpenDocument

      m_map = Nothing
      s_dockWindow = Nothing
      s_extension = Nothing

      MyBase.OnShutdown()
    End Sub

    Protected Overrides Function OnSetState(ByVal state As ExtensionState) As Boolean
      ' Optionally check for a license here
      Me.State = state

      If state = ExtensionState.Enabled Then
        Initialize()
      Else
        Uninitialize()
      End If

      Return MyBase.OnSetState(state)
    End Function

    Protected Overrides Function OnGetState() As ExtensionState
      Return Me.State
    End Function

    ' Privates

    Private Sub Initialize()
      If s_extension Is Nothing Or Me.State <> ExtensionState.Enabled Then
        Return
      End If

      ' Reset event handlers
      Dim avEvent As IActiveViewEvents_Event = TryCast(ArcMap.Document.FocusMap, IActiveViewEvents_Event)
      AddHandler avEvent.ItemAdded, AddressOf AvEvent_ItemAdded
      AddHandler avEvent.ItemDeleted, AddressOf AvEvent_ItemAdded
      AddHandler avEvent.SelectionChanged, AddressOf UpdateSelCountDockWin
      AddHandler avEvent.ContentsChanged, AddressOf avEvent_ContentsChanged

      ' Update the UI
      m_map = ArcMap.Document.FocusMap
      FillComboBox()
      SelCountDockWin.SetEnabled(True)
      UpdateSelCountDockWin()
      m_hasSelectableLayer = CheckForSelectableLayer()
    End Sub

    Private Sub Uninitialize()
      If s_extension Is Nothing Then
        Return
      End If

      ' Detach event handlers
      Dim avEvent As IActiveViewEvents_Event = TryCast(m_map, IActiveViewEvents_Event)
      RemoveHandler avEvent.ItemAdded, AddressOf AvEvent_ItemAdded
      RemoveHandler avEvent.ItemDeleted, AddressOf AvEvent_ItemAdded
      RemoveHandler avEvent.SelectionChanged, AddressOf UpdateSelCountDockWin
      RemoveHandler avEvent.ContentsChanged, AddressOf avEvent_ContentsChanged
      avEvent = Nothing

      ' Update UI
      Dim selCombo As SelectionTargetComboBox = SelectionTargetComboBox.GetSelectionComboBox()
      If selCombo IsNot Nothing Then
        selCombo.ClearAll()
      End If

      SelCountDockWin.SetEnabled(False)
    End Sub

    Private Sub UpdateSelCountDockWin()
      ' If the dockview hasn't been created yet
      If (Not SelCountDockWin.Exists) Then
        Return
      End If

      ' Update the contents of the listView, when the selection changes in the map. 
      Dim featureLayer As IFeatureLayer
      Dim featSel As IFeatureSelection

      SelCountDockWin.Clear()

      ' Loop through the layers in the map and add the layer's name and
      ' selection count to the list box
      For i As Integer = 0 To m_map.LayerCount - 1
        If TypeOf m_map.Layer(i) Is IFeatureSelection Then
          featureLayer = TryCast(m_map.Layer(i), IFeatureLayer)
          If featureLayer Is Nothing Then
            Exit For
          End If

          featSel = TryCast(featureLayer, IFeatureSelection)

          Dim count As Integer = 0
          If featSel.SelectionSet IsNot Nothing Then
            count = featSel.SelectionSet.Count
          End If
          SelCountDockWin.AddItem(featureLayer.Name, count)
        End If
      Next i
    End Sub

    Private Sub FillComboBox()
      Dim selCombo As SelectionTargetComboBox = SelectionTargetComboBox.GetSelectionComboBox()
      If selCombo Is Nothing Then
        Return
      End If

      selCombo.ClearAll()

      Dim featureLayer As IFeatureLayer
      ' Loop through the layers in the map and add the layer's name to the combo box.
      For i As Integer = 0 To m_map.LayerCount - 1
        If TypeOf m_map.Layer(i) Is IFeatureSelection Then
          featureLayer = TryCast(m_map.Layer(i), IFeatureLayer)
          If featureLayer Is Nothing Then
            Exit For
          End If

          selCombo.AddItem(featureLayer.Name, featureLayer)
        End If
      Next i
    End Sub

    Private Function CheckForSelectableLayer() As Boolean
      Dim map As IMap = ArcMap.Document.FocusMap
      ' Bail if map has no layers
      If map.LayerCount = 0 Then
        Return False
      End If

      ' Fetch all the feature layers in the focus map
      ' and see if at least one is selectable
      Dim uid As New UIDClass()
      uid.Value = "{40A9E885-5533-11d0-98BE-00805F7CED21}"
      Dim enumLayers As IEnumLayer = map.Layers(uid, True)
      Dim featureLayer As IFeatureLayer = TryCast(enumLayers.Next(), IFeatureLayer)
      Do While featureLayer IsNot Nothing
        If featureLayer.Selectable = True Then
          Return True
        End If
        featureLayer = TryCast(enumLayers.Next(), IFeatureLayer)
      Loop
      Return False
    End Function

    ' Event handlers

    Private Sub ArcMap_NewOpenDocument()
      Dim pageLayoutEvent As IActiveViewEvents_Event = TryCast(ArcMap.Document.PageLayout, IActiveViewEvents_Event)
      AddHandler pageLayoutEvent.FocusMapChanged, AddressOf AVEvents_FocusMapChanged

      Initialize()
    End Sub

    Private Sub avEvent_ContentsChanged()
      m_hasSelectableLayer = CheckForSelectableLayer()
    End Sub

    Private Sub AvEvent_ItemAdded(ByVal Item As Object)
      m_map = ArcMap.Document.FocusMap
      FillComboBox()
      UpdateSelCountDockWin()
      m_hasSelectableLayer = CheckForSelectableLayer()
    End Sub

    Private Sub AVEvents_FocusMapChanged()
      Initialize()
    End Sub

    ' Statics

    Friend Shared Function GetSelectionCountWindow() As ESRI.ArcGIS.Framework.IDockableWindow
      If s_extension Is Nothing Then
        GetExtension()
      End If

      ' Only get/create the dockable window if they ask for it
      If s_dockWindow Is Nothing Then
        Dim dockWinID As UID = New UIDClass()
        dockWinID.Value = ThisAddIn.IDs.SelCountDockWin
        s_dockWindow = ArcMap.DockableWindowManager.GetDockableWindow(dockWinID)
        s_extension.UpdateSelCountDockWin()
      End If

      Return s_dockWindow
    End Function

    Friend Shared Function IsExtensionEnabled() As Boolean
      If s_extension Is Nothing Then
        GetExtension()
      End If

      If s_extension Is Nothing Then
        Return False
      End If

      Return s_extension.State = ExtensionState.Enabled
    End Function

    Friend Shared Function HasSelectableLayer() As Boolean
      If s_extension Is Nothing Then
        GetExtension()
      End If

      If s_extension Is Nothing Then
        Return False
      End If

      Return s_extension.m_hasSelectableLayer
    End Function

    Private Shared Function GetExtension() As SelectionExtension
      If s_extension Is Nothing Then
        Dim extID As UID = New UIDClass()
        extID.Value = ThisAddIn.IDs.SelectionExtension
        ' Call FindExtension to load this just-in-time extension.
        ArcMap.Application.FindExtensionByCLSID(extID)
      End If
      Return s_extension
    End Function
  End Class
End Namespace
