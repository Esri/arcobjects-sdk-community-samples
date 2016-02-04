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
'*************************************************************************************
'       ArcGIS Network Analyst extension - Add Traversal Results to Map Sample
'
'   This simple code is an ArcGIS Add-In that shows how to take the traversal results 
'     from a solved network analysis layer and add them to the map as feature layers.
'     The user can then step through the traversal results layers to see the 
'     individual features that make up the results.
'
'************************************************************************************


Imports Microsoft.VisualBasic
Imports System
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Display
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.NetworkAnalyst
Imports ESRI.ArcGIS.NetworkAnalystUI

Namespace NAAddTraversalResultToMap
	Public Class AddTraversalResultsToMap
		Inherits ESRI.ArcGIS.Desktop.AddIns.Button
		Public Sub New()
		End Sub

		''' <summary>
		''' OnClick is the main function for the add-in.  When the button is clicked in ArcMap,
		'''  this code will execute.  Note that if you re-solve the analysis layer associated with 
		'''  the traversal result, any open attribute tables associated with the traversal result
		'''  will disconnect and need to be reopened.
		''' </summary>
		Protected Overrides Sub OnClick()
			Try
				Dim networkAnalystExtension As INetworkAnalystExtension = TryCast(ArcMap.Application.FindExtensionByName("Network Analyst"), INetworkAnalystExtension)
				If networkAnalystExtension Is Nothing Then
					Throw New System.Exception("Network Analyst Extension is not available.")
				End If

				Dim naLayer As INALayer = networkAnalystExtension.NAWindow.ActiveAnalysis
				If naLayer Is Nothing Then
					Throw New System.Exception("Cannot add a traversal result.  There is no active network analysis layer.")
				End If

				Dim result As INAResult = naLayer.Context.Result
				If result Is Nothing OrElse (Not result.HasValidResult) Then
					Throw New System.Exception("Cannot add a traversal result.  The active analysis layer either does not have a valid result or does not support traversal results.")
				End If

				' In the case of Vehicle Routing Problem (VRP) layers, get the traversal result from the internal route context, if available
				Dim vrpResult As INAVRPResult = TryCast(result, INAVRPResult)
				If vrpResult IsNot Nothing Then
					If vrpResult.InternalRouteContext Is Nothing OrElse vrpResult.InternalRouteContext.Result Is Nothing OrElse Not vrpResult.InternalRouteContext.Result.HasValidResult Then
						Throw New System.Exception("Cannot add a traversal result.  VRP layers cannot have a shape type of none.")
					End If

					result = vrpResult.InternalRouteContext.Result
				End If

				Dim naTraversalResultEdit As INATraversalResultEdit = TryCast(result, INATraversalResultEdit)
				If naTraversalResultEdit Is Nothing Then
					Throw New System.Exception("Cannot add a traversal result.  The active analysis layer does not support traversal results.")
				End If

				' Geometry needs to be inferred for traversal edges in cases where the route geometry is not already generated.
				'  It doesn't hurt to make this call, though, even when a geometry is already present.
				naTraversalResultEdit.InferGeometry(String.Empty, Nothing, New CancelTrackerClass())

				' Load the junction, edge and turn traversal results to the map
				' also add the internal route and internal NAContext if available
				Dim groupLayer As IGroupLayer = New GroupLayerClass()
				groupLayer.Name = "NAResults - " & DirectCast(naLayer, ILayer).Name
				AddNATraversalResultLayersToGroup(groupLayer, result)

				' The newly added layer references in-memory features of the NALayer.
				'  Therefore, in order for the new layer to persist properly, it has to be 
				'  placed below the layer it references.
				Dim mapLayers As IMapLayers2 = TryCast(ArcMap.Document.FocusMap, IMapLayers2)
				Dim naLayerPosition As Integer = GetLayerIndex(TryCast(naLayer, ILayer))
				mapLayers.InsertLayer(groupLayer, False, naLayerPosition + 1)

			Catch e As System.Exception
				System.Windows.Forms.MessageBox.Show(e.Message, "Add-In Exception", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.[Error])
			End Try
		End Sub

		''' <summary>
		''' AddFeatureLayerToMap will take the traversal result from an NAResult and add it to the map as a layer
		''' </summary>
		Private Sub AddFeatureLayerToMap(ByVal result As INAResult, ByVal naLayerName As String, ByVal naLayerPosition As Integer, ByVal elementType As esriNetworkElementType)
			Dim naTraversalResultQuery As INATraversalResultQuery = TryCast(result, ESRI.ArcGIS.NetworkAnalyst.INATraversalResultQuery)

			' Note that the traversal result layer that is being created will update each time the referenced layer
			'  completes another solve.
			Dim featureLayer As IFeatureLayer = New FeatureLayerClass()
			featureLayer.FeatureClass = naTraversalResultQuery.FeatureClass(elementType)
			featureLayer.Name = naLayerName & " - Traversal Result " & featureLayer.FeatureClass.AliasName

			' The newly added layer references in-memory features of the NALayer.
			'  Therefore, in order for the new layer to persist properly, it has to be 
			'  placed below the layer it references.
			Dim mapLayers As IMapLayers2 = TryCast(ArcMap.Document.FocusMap, IMapLayers2)
			mapLayers.InsertLayer(featureLayer, False, naLayerPosition + 1)
		End Sub

		''' <summary>
		''' AddNATraversalResultLayersToGroup will take the traversal results from an INAResult and add them to the map as a group layer
		''' </summary>
		Private Sub AddNATraversalResultLayersToGroup(ByVal groupLayer As IGroupLayer, ByVal result As INAResult)
			Dim naTraversalResultQuery As INATraversalResultQuery2 = TryCast(result, INATraversalResultQuery2)
			groupLayer.Add(GetTraversalResultLayer(esriNetworkElementType.esriNETJunction, naTraversalResultQuery))
			groupLayer.Add(GetTraversalResultLayer(esriNetworkElementType.esriNETTurn, naTraversalResultQuery))
			groupLayer.Add(GetTraversalResultLayer(esriNetworkElementType.esriNETEdge, naTraversalResultQuery))
		End Sub

		''' <summary>
		''' GetTraversalResultLayer will generate a layer out of the traversal result for the specified element type
		''' </summary>
		Private Function GetTraversalResultLayer(ByVal elementType As esriNetworkElementType, ByVal naTraversalResultQuery As INATraversalResultQuery2) As IFeatureLayer
			'Junctions Traversal Result Feature Layer
			Dim traversalResultFeatureClass As IFeatureClass = naTraversalResultQuery.FeatureClass(elementType)
			If traversalResultFeatureClass IsNot Nothing Then
				' save the rows in this class out when the MXD is saved
				Dim naClass As INAClass = TryCast(traversalResultFeatureClass, INAClass)
				naClass.SaveRowsOnPersist = True

				' create the traversal result layer
				Dim traversalResultLayer As IFeatureLayer = New FeatureLayerClass()
				traversalResultLayer.FeatureClass = traversalResultFeatureClass
				traversalResultLayer.Name = traversalResultFeatureClass.AliasName

				' Set up the layer with an appropriate symbology
				Dim geoFeatureLayer As IGeoFeatureLayer = TryCast(traversalResultLayer, IGeoFeatureLayer)
				geoFeatureLayer.RendererPropertyPageClassID = (New ESRI.ArcGIS.CartoUI.SingleSymbolPropertyPageClass()).ClassID
				geoFeatureLayer.Renderer = GetRenderer(elementType)

				Return traversalResultLayer
			End If
			Return Nothing
		End Function

		''' <summary>
		''' GetRenderer will return a feature renderer with symbology appropriate to the specified element type
		''' </summary>
		Private Function GetRenderer(ByVal networkElementType As esriNetworkElementType) As IFeatureRenderer
			Dim simpleRend As ISimpleRenderer = New SimpleRendererClass()
			Dim color As IRgbColor = New RgbColorClass()

			Select Case networkElementType
				' The junction symbology will be a large yellow circle with a thick black outline
				Case esriNetworkElementType.esriNETJunction

					Dim junctionPointSymbol As ISimpleMarkerSymbol = New SimpleMarkerSymbolClass()
					junctionPointSymbol.Style = ESRI.ArcGIS.Display.esriSimpleMarkerStyle.esriSMSCircle
					junctionPointSymbol.Size = 10

					'yellow
					color.Red = 255
					color.Blue = 0
					color.Green = 255
					junctionPointSymbol.Color = color

					junctionPointSymbol.Outline = True
					junctionPointSymbol.OutlineSize = 2

					'black
					color.Red = 0
					color.Blue = 0
					color.Green = 0
					junctionPointSymbol.OutlineColor = color

					simpleRend.Label = "TRFC_Junctions"
					simpleRend.Symbol = TryCast(junctionPointSymbol, ISymbol)
					Return TryCast(simpleRend, IFeatureRenderer)

					' The turn symbology will be a thick purple line
				Case esriNetworkElementType.esriNETTurn

					Dim turnLineSymbol As ISimpleLineSymbol = New SimpleLineSymbolClass()
					turnLineSymbol.Style = ESRI.ArcGIS.Display.esriSimpleLineStyle.esriSLSSolid
					turnLineSymbol.Width = 4

					'purple
					color.Red = 125
					color.Blue = 125
					color.Green = 0
					turnLineSymbol.Color = color

					simpleRend.Label = "TRFC_Turns"
					simpleRend.Symbol = TryCast(turnLineSymbol, ISymbol)
					Return TryCast(simpleRend, IFeatureRenderer)

					' The edge symbology will be a thick blue line
				Case esriNetworkElementType.esriNETEdge

					Dim edgeLineSymbol As ISimpleLineSymbol = New SimpleLineSymbolClass()
					edgeLineSymbol.Style = ESRI.ArcGIS.Display.esriSimpleLineStyle.esriSLSSolid
					edgeLineSymbol.Width = 4

					'blue
					color.Red = 0
					color.Blue = 255
					color.Green = 0
					edgeLineSymbol.Color = color

					simpleRend.Label = "TRFC_Edges"
					simpleRend.Symbol = TryCast(edgeLineSymbol, ISymbol)
					Return TryCast(simpleRend, IFeatureRenderer)
			End Select
			Return Nothing
		End Function

		Private Function GetLayerIndex(ByVal layer As ILayer) As Integer
			For index As Integer = 0 To ArcMap.Document.FocusMap.LayerCount - 1
				Dim layerAtIndex As ILayer = ArcMap.Document.FocusMap.Layer(index)
				If layerAtIndex Is layer Then
					Return index
				End If
			Next index

			Return -1
		End Function

		Protected Overrides Sub OnUpdate()
			Enabled = Not ArcMap.Application Is Nothing
		End Sub

	End Class

End Namespace
