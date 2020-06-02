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
Imports System.Drawing
Imports System.Data
Imports System.Text
Imports System.Windows.Forms
Imports ESRI.ArcGIS.Controls
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.SystemUI
Imports ESRI.ArcGIS.esriSystem

  ''' <summary>
  ''' This user control hosts a combobox which allow the user to control over the active graphics layer 
  ''' </summary>
Partial Public Class GraphicsLayersListCtrl : Inherits UserControl
#Region "class members"
  Private m_map As IMap = Nothing
  Private m_uid As UID = Nothing
#End Region

#Region "class constructor"
  Public Sub New()
    InitializeComponent()

    'initialize the UID that will be used later to get the graphics layers
    m_uid = New UIDClass()
    m_uid.Value = "{34B2EF81-F4AC-11D1-A245-080009B6F22B}" 'graphics layers category
  End Sub
#End Region

  ''' <summary>
  ''' Get the current map and wire the ActiveViewEvents
  ''' </summary>
  Public Property Map() As IMap
    Get
      Return m_map
    End Get
    Set(ByVal value As IMap)
      m_map = value
      If Nothing Is m_map Then
        Return
      End If

      'set verbose events in order to be able to listen to the various 'ItemXXX' events 
      CType(m_map, IViewManager).VerboseEvents = True

      'register document events in order to listen to layers which gets added or removed
      AddHandler (CType(m_map, IActiveViewEvents_Event)).ItemAdded, AddressOf OnItemAdded
      AddHandler (CType(m_map, IActiveViewEvents_Event)).ItemReordered, AddressOf OnItemReordered
      AddHandler (CType(m_map, IActiveViewEvents_Event)).ItemDeleted, AddressOf OnItemDeleted

      'populate the combo with a list of the graphics layers
      PopulateCombo()
    End Set
  End Property

  ''' <summary>
  ''' occurs when the user select an item from the combo
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub cmbGraphicsLayerList_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbGraphicsLayerList.SelectedIndexChanged
    If Nothing Is m_map Then
      Return
    End If

    'get the basic graphics layer from the map
    Dim activeLayer As ILayer = TryCast(m_map.BasicGraphicsLayer, ILayer)
    'if the name of the selected item is the basic graphics layer, make it the active graphics layer
    If activeLayer.Name = cmbGraphicsLayerList.SelectedItem.ToString() Then
      m_map.ActiveGraphicsLayer = TryCast(m_map.BasicGraphicsLayer, ILayer)
      Return
    End If
    'iterate through the graphics layers
    Dim layers As IEnumLayer = GetGraphicsLayersList()
    If Nothing Is layers Then
      Return
    End If

    layers.Reset()
    Dim layer As ILayer = layers.Next()

    Do While Not layer Is Nothing
      If TypeOf layer Is IGroupLayer Then
        Continue Do
      End If

      If TypeOf layer Is IGraphicsLayer Then
        'make the select item the active graphics layer
        If layer.Name = cmbGraphicsLayerList.SelectedItem.ToString() Then
          m_map.ActiveGraphicsLayer = layer
        End If
      End If
      layer = layers.Next()
    Loop
  End Sub

  ''' <summary>
  ''' get the list of all graphics layers in the map
  ''' </summary>
  ''' <returns></returns>
  Private Function GetGraphicsLayersList() As IEnumLayer
    Dim layers As IEnumLayer = Nothing
    If Nothing Is m_map OrElse 0 = m_map.LayerCount Then
      Return Nothing
    End If

    Try
      layers = m_map.Layers(m_uid, True)
    Catch ex As Exception
      System.Diagnostics.Trace.WriteLine(ex.Message)
      Return Nothing
    End Try

    Return layers
  End Function

  ''' <summary>
  ''' list the graphics layers in the combo and select the active graphics layer
  ''' </summary>
  Private Sub PopulateCombo()
    If Nothing Is m_map Then
      Return
    End If

    'clear the items list of the combo
    cmbGraphicsLayerList.Items.Clear()

    'add the basic graphics layer name
    cmbGraphicsLayerList.Items.Add((CType(m_map.BasicGraphicsLayer, ILayer)).Name)

    'get the active graphics layer
    Dim activeLayer As ILayer = m_map.ActiveGraphicsLayer

    'get the list of all graphics layers in the map
    Dim layers As IEnumLayer = GetGraphicsLayersList()
    If Not Nothing Is layers Then

      'add the layer names to the combo
      layers.Reset()
      Dim layer As ILayer = layers.Next()

      Do While Not layer Is Nothing
        cmbGraphicsLayerList.Items.Add(layer.Name)
        layer = layers.Next()
      Loop
    End If
    'set the selected item to be the active layer
    cmbGraphicsLayerList.SelectedItem = activeLayer.Name
  End Sub

  ''' <summary>
  ''' occurs when a layer is being deleted from the map
  ''' </summary>
  ''' <param name="Item"></param>
  Private Sub OnItemDeleted(ByVal Item As Object)
    PopulateCombo()
  End Sub

  ''' <summary>
  ''' occurs when a layer is being reordered in the TOC
  ''' </summary>
  ''' <param name="Item"></param>
  ''' <param name="toIndex"></param>
  Private Sub OnItemReordered(ByVal Item As Object, ByVal toIndex As Integer)
    PopulateCombo()
  End Sub

  ''' <summary>
  ''' occurs when a layer is being added to the map
  ''' </summary>
  ''' <param name="Item"></param>
  Private Sub OnItemAdded(ByVal Item As Object)
    PopulateCombo()
  End Sub
End Class
