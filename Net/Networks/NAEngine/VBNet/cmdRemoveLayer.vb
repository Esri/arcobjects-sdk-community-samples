Imports Microsoft.VisualBasic
Imports System.Runtime.InteropServices
Imports System.Windows.Forms
Imports ESRI.ArcGIS.Carto

' This command removes the selected layer from the map
Namespace NAEngine
	<Guid("53399A29-2B65-48d5-930F-804B88B85A34"), ClassInterface(ClassInterfaceType.None), ProgId("NAEngine.RemoveLayer")> _
	Public NotInheritable Class cmdRemoveLayer : Inherits ESRI.ArcGIS.ADF.BaseClasses.BaseCommand
		Private m_mapControl As ESRI.ArcGIS.Controls.IMapControl3

		Public Sub New()
			MyBase.m_caption = "Remove Layer"
		End Sub

		Public Overrides Sub OnClick()
			If m_mapControl Is Nothing Then
				MessageBox.Show("Error: Map control is null for this command")
				Return
			End If

			' Get the layer that was right-clicked on in the table of contents
			' m_MapControl.CustomProperty was set in frmMain.axTOCControl1_OnMouseDown
            Dim layer As ILayer = TryCast(m_mapControl.CustomProperty, ILayer)
			If layer Is Nothing Then
				MessageBox.Show("Error: The selected layer was not set as the CustomProperty of the map control")
				Return
			End If

			' Remove the selected layer, then redraw the map
			m_mapControl.Map.DeleteLayer(layer)
			m_mapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, Nothing, layer.AreaOfInterest)
		End Sub

		Public Overrides Sub OnCreate(ByVal hook As Object)
			' The "hook" was set as a MapControl in formMain_Load
			m_mapControl = TryCast(hook, ESRI.ArcGIS.Controls.IMapControl3)
		End Sub
	End Class
End Namespace
