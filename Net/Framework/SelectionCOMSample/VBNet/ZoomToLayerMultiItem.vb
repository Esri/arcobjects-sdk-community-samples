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
Imports ESRI.ArcGIS.SystemUI
Imports System.Runtime.InteropServices
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.ArcMapUI

Namespace SelectionCOMSample
  <Guid("B8147F77-BE16-4a43-A2F1-E6E030BD579E"), ClassInterface(ClassInterfaceType.None), ProgId("SelectionCOMSample.ZoomToLayerMultiItem")> _
  Public NotInheritable Class ZoomToLayerMultiItem
    Implements IMultiItem, IMultiItemEx
    Private m_map As IMap
    Private m_doc As IMxDocument

#Region "IMultiItem Members"

    Public ReadOnly Property Caption() As String Implements IMultiItem.Caption
      Get
        Return "Selection MultiItem VB.NET"
      End Get
    End Property

    Public ReadOnly Property HelpContextID() As Integer Implements IMultiItem.HelpContextID
      Get
        Return 0
      End Get
    End Property

    Public ReadOnly Property HelpFile() As String Implements IMultiItem.HelpFile
      Get
        Return ""
      End Get
    End Property

    Public ReadOnly Property Message() As String Implements IMultiItem.Message
      Get
        Return "Select layer to zoom to its full extent."
      End Get
    End Property

    Public ReadOnly Property Name() As String Implements IMultiItem.Name
      Get
        Return "ESRI_SelectionCOMSample_MultiItem"
      End Get
    End Property

    Public Sub OnItemClick(ByVal index As Integer) Implements IMultiItem.OnItemClick
      Dim layer As ESRI.ArcGIS.Carto.ILayer = m_map.Layer(index)
      Dim env As ESRI.ArcGIS.Geometry.IEnvelope = layer.AreaOfInterest
      m_doc.ActiveView.Extent = env
      m_doc.ActiveView.Refresh()
    End Sub

    Public Function OnPopup(ByVal Hook As Object) As Integer Implements IMultiItem.OnPopup
      Dim app As IApplication = TryCast(Hook, IApplication)
      If app Is Nothing Then
        Return 0
      End If

      m_doc = TryCast(app.Document, IMxDocument)
      m_map = m_doc.FocusMap

      Return m_map.LayerCount
    End Function
    Public ReadOnly Property ItemBitmap(ByVal index As Integer) As Integer Implements ESRI.ArcGIS.SystemUI.IMultiItem.ItemBitmap
      Get
        Return 0
      End Get
    End Property
    Public ReadOnly Property ItemCaption(ByVal index As Integer) As String Implements ESRI.ArcGIS.SystemUI.IMultiItem.ItemCaption
      Get
        Dim layer As ILayer = m_map.Layer(index)
        If layer IsNot Nothing Then
          Return layer.Name
        Else
          Return ""
        End If
      End Get
    End Property
    Public ReadOnly Property ItemEnabled(ByVal index As Integer) As Boolean Implements ESRI.ArcGIS.SystemUI.IMultiItem.ItemEnabled
      Get
        Dim layer As ILayer = m_map.Layer(index)
        If layer IsNot Nothing Then
          Return layer.Visible
        Else
          Return False
        End If
      End Get
    End Property
    Public ReadOnly Property ItemChecked(ByVal index As Integer) As Boolean Implements ESRI.ArcGIS.SystemUI.IMultiItem.ItemChecked
      Get
        Return False
      End Get
    End Property
#End Region

#Region "IMultiItemEx Members"
    Public ReadOnly Property ItemHelpContextID(ByVal index As Integer) As Integer Implements ESRI.ArcGIS.SystemUI.IMultiItemEx.ItemHelpContextID
      Get
        Return 0
      End Get
    End Property

    Public ReadOnly Property ItemHelpFile(ByVal index As Integer) As String Implements ESRI.ArcGIS.SystemUI.IMultiItemEx.ItemHelpFile
      Get
        Return ""
      End Get
    End Property

    Public ReadOnly Property ItemMessage(ByVal index As Integer) As String Implements ESRI.ArcGIS.SystemUI.IMultiItemEx.ItemMessage
      Get
        Dim layer As ILayer = m_map.Layer(index)
        If layer IsNot Nothing Then
          Return layer.Name
        Else
          Return ""
        End If
      End Get
    End Property
#End Region
  End Class
End Namespace
