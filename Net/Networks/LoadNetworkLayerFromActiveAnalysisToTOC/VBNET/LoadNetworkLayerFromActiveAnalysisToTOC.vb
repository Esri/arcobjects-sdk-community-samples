'*************************************************************************************
'       ArcGIS Network Analyst extension - Load network layer from active analysis to the table of contents
'
'   This simple code shows how to load the network layer and its associated sources
'    to a map
'
'************************************************************************************


Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.NetworkAnalyst
Imports ESRI.ArcGIS.NetworkAnalystUI

Namespace LoadNetworkLayerFromActiveAnalysisToTOC
	Public Class LoadNetworkLayerFromActiveAnalysisToTOC
		Inherits ESRI.ArcGIS.Desktop.AddIns.Button
		Public Sub New()
		End Sub

		''' <summary>
		''' OnClick is the main function for the add-in.  When the button is clicked in ArcMap,
		'''  this code will execute
		''' </summary>
		Protected Overrides Sub OnClick()
			' Verify that the network extension has been loaded properly.
			Dim naExt As INetworkAnalystExtension = TryCast(ArcMap.Application.FindExtensionByName("Network Analyst"), INetworkAnalystExtension)
			If naExt Is Nothing OrElse naExt.NAWindow Is Nothing Then
				Return
			End If

			' There must be an active analysis layer
			Dim naLayer As INALayer = naExt.NAWindow.ActiveAnalysis
			If naLayer Is Nothing OrElse naLayer.Context Is Nothing Then
				Return
			End If

			' Use the network layer factory to generate the layers
			Dim layerFactory As ILayerFactory = New EngineNetworkLayerFactoryClass()
			Dim dataset As IDataset = TryCast(naLayer.Context.NetworkDataset, IDataset)
			If layerFactory.CanCreate(dataset.FullName) Then
				' Calling create will open a popup asking the user if the network sources
				'  should be added as well.  If the user clicks NO, then the returned
				'  IEnumLayer will only return a network layer for the network dataset.
				'  If the user clicks YES, then IEnumLayer will also return layers for 
				'  the network sources
				Dim enumLayer As IEnumLayer = layerFactory.Create(dataset.FullName)
				Dim layer As ILayer = enumLayer.Next()
				Do While Not layer Is Nothing
					ArcMap.Document.FocusMap.AddLayer(layer)
					layer = enumLayer.Next()
				Loop
			End If
		End Sub

		Protected Overrides Sub OnUpdate()
			Enabled = (Not ArcMap.Application Is Nothing)
		End Sub
	End Class

End Namespace
