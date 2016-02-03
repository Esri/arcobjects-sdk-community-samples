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
