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

Imports My

Namespace SelectionSample
  Public Class ZoomToLayerMultiItem
	  Inherits ESRI.ArcGIS.Desktop.AddIns.MultiItem
	Protected Overrides Sub OnClick(ByVal item As Item)
	  Dim layer As ESRI.ArcGIS.Carto.ILayer = TryCast(item.Tag, ESRI.ArcGIS.Carto.ILayer)
	  Dim env As ESRI.ArcGIS.Geometry.IEnvelope = layer.AreaOfInterest
	  ArcMap.Document.ActiveView.Extent = env
	  ArcMap.Document.ActiveView.Refresh()
	End Sub

	Protected Overrides Sub OnPopup(ByVal items As ItemCollection)
	  Dim map As ESRI.ArcGIS.Carto.IMap = ArcMap.Document.FocusMap
	  For i As Integer = 0 To map.LayerCount - 1
		Dim layer As ESRI.ArcGIS.Carto.ILayer = map.Layer(i)
		Dim item As New Item()
		item.Caption = layer.Name
		item.Enabled = layer.Visible
		item.Message = layer.Name
		item.Tag = layer
		items.Add(item)
	  Next i
	End Sub
  End Class
End Namespace
