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
Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.IO
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.ArcMapUI
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Carto

Namespace CustomUIElements
  Public Class ZoomToLayerButton
    Inherits ESRI.ArcGIS.Desktop.AddIns.Button

    Public Sub New()
    End Sub

    Protected Overrides Sub OnClick()
      ZoomToActiveLayerInTOC(TryCast(My.ArcMap.Application.Document, IMxDocument))
    End Sub

    Protected Overrides Sub OnUpdate()
      Enabled = My.ArcMap.Application IsNot Nothing
    End Sub

#Region "Zoom to Active Layer in TOC"


    Public Sub ZoomToActiveLayerInTOC(ByVal mxDocument As IMxDocument)
      If mxDocument Is Nothing Then
        Return
      End If
      Dim activeView As IActiveView = mxDocument.ActiveView

      ' Get the TOC
      Dim IContentsView As IContentsView = mxDocument.CurrentContentsView

      ' Get the selected layer
      Dim selectedItem As System.Object = IContentsView.SelectedItem
      If Not (TypeOf selectedItem Is ILayer) Then
        Return
      End If
      Dim layer As ILayer = TryCast(selectedItem, ILayer)
      ' Zoom to the extent of the layer and refresh the map
      activeView.Extent = layer.AreaOfInterest
      activeView.Refresh()
    End Sub
#End Region
  End Class


End Namespace
