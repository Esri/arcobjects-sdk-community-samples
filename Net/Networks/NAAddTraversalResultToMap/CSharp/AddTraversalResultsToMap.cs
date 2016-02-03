//*************************************************************************************
//       ArcGIS Network Analyst extension - Add Traversal Results to Map Sample
//
//   This simple code is an ArcGIS Add-In that shows how to take the traversal results 
//     from a solved network analysis layer and add them to the map as feature layers.
//     The user can then step through the traversal results layers to see the 
//     individual features that make up the results.
//
//************************************************************************************

using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.CartoUI;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.NetworkAnalyst;
using ESRI.ArcGIS.NetworkAnalystUI;

namespace NAAddTraversalResultToMap
{
	public class AddTraversalResultsToMap : ESRI.ArcGIS.Desktop.AddIns.Button
	{
		public AddTraversalResultsToMap()
		{
		}

		/// <summary>
		/// OnClick is the main function for the add-in.  When the button is clicked in ArcMap,
		///  this code will execute.  Note that if you re-solve the analysis layer associated with 
		///  the traversal result, any open attribute tables associated with the traversal result
		///  will disconnect and need to be reopened.
		/// </summary>
		protected override void OnClick()
		{
			try
			{
				var networkAnalystExtension = ArcMap.Application.FindExtensionByName("Network Analyst") as INetworkAnalystExtension;
				if (networkAnalystExtension == null)
					throw new System.Exception("Network Analyst Extension is not available.");

				INALayer naLayer = networkAnalystExtension.NAWindow.ActiveAnalysis;
				if (naLayer == null)
					throw new System.Exception("Cannot add a traversal result.  There is no active network analysis layer.");

				var result = naLayer.Context.Result;
				if (result == null || !result.HasValidResult)
					throw new System.Exception("Cannot add a traversal result.  The active analysis layer either does not have a valid result or does not support traversal results.");

				// In the case of Vehicle Routing Problem (VRP) layers, get the traversal result from the internal route context, if available
				var vrpResult = result as INAVRPResult;
				if (vrpResult != null)
				{
					if (vrpResult.InternalRouteContext == null || vrpResult.InternalRouteContext.Result == null || !vrpResult.InternalRouteContext.Result.HasValidResult)
						throw new System.Exception("Cannot add a traversal result.  VRP layers cannot have a shape type of none.");

					result = vrpResult.InternalRouteContext.Result;
				}

				var naTraversalResultEdit = result as INATraversalResultEdit;
				if (naTraversalResultEdit == null)
					throw new System.Exception("Cannot add a traversal result.  The active analysis layer does not support traversal results.");

				// Geometry needs to be inferred for traversal edges in cases where the route geometry is not already generated.
				//  It doesn't hurt to make this call, though, even when a geometry is already present.
				naTraversalResultEdit.InferGeometry(string.Empty, null, new CancelTrackerClass());

				// Load the junction, edge and turn traversal results to the map
				// also add the internal route and internal NAContext if available
				IGroupLayer groupLayer = new GroupLayerClass();
				groupLayer.Name = "NAResults - " + ((ILayer)naLayer).Name;
				AddNATraversalResultLayersToGroup(groupLayer, result);

				// The newly added layer references in-memory features of the NALayer.
				//  Therefore, in order for the new layer to persist properly, it has to be 
				//  placed below the layer it references.
				var mapLayers = ArcMap.Document.FocusMap as IMapLayers2;
				int naLayerPosition = GetLayerIndex(naLayer as ILayer);
				mapLayers.InsertLayer(groupLayer, false, naLayerPosition + 1);
			}
			catch (System.Exception e)
			{
				System.Windows.Forms.MessageBox.Show(e.Message, "Add-In Exception", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
			}
		}

		/// <summary>
		/// AddNATraversalResultLayersToGroup will take the traversal results from an INAResult and add them to the map as a group layer
		/// </summary>
		private void AddNATraversalResultLayersToGroup(IGroupLayer groupLayer, INAResult result)
		{
			var naTraversalResultQuery = result as INATraversalResultQuery2;
			groupLayer.Add(GetTraversalResultLayer(esriNetworkElementType.esriNETJunction, naTraversalResultQuery));
			groupLayer.Add(GetTraversalResultLayer(esriNetworkElementType.esriNETTurn, naTraversalResultQuery));
			groupLayer.Add(GetTraversalResultLayer(esriNetworkElementType.esriNETEdge, naTraversalResultQuery));
		}

		/// <summary>
		/// GetTraversalResultLayer will generate a layer out of the traversal result for the specified element type
		/// </summary>
		private IFeatureLayer GetTraversalResultLayer(esriNetworkElementType elementType, INATraversalResultQuery2 naTraversalResultQuery)
		{
			//Junctions Traversal Result Feature Layer
			IFeatureClass traversalResultFeatureClass = naTraversalResultQuery.get_FeatureClass(elementType);
			if (traversalResultFeatureClass != null)
			{
				// save the rows in this class out when the MXD is saved
				INAClass naClass = traversalResultFeatureClass as INAClass;
				naClass.SaveRowsOnPersist = true;

				// create the traversal result layer
				IFeatureLayer traversalResultLayer = new FeatureLayerClass();
				traversalResultLayer.FeatureClass = traversalResultFeatureClass;
				traversalResultLayer.Name = traversalResultFeatureClass.AliasName;

				// Set up the layer with an appropriate symbology
				var geoFeatureLayer = traversalResultLayer as IGeoFeatureLayer;
				geoFeatureLayer.RendererPropertyPageClassID = (new SingleSymbolPropertyPageClass()).ClassID;
				geoFeatureLayer.Renderer = GetRenderer(elementType);

				return traversalResultLayer;
			}
			return null;
		}

		/// <summary>
		/// GetRenderer will return a feature renderer with symbology appropriate to the specified element type
		/// </summary>
		private IFeatureRenderer GetRenderer(esriNetworkElementType networkElementType)
		{
			ISimpleRenderer simpleRend = new SimpleRendererClass();
			IRgbColor color = new RgbColorClass();

			switch (networkElementType)
			{
				// The junction symbology will be a large yellow circle with a thick black outline
				case esriNetworkElementType.esriNETJunction:

					ISimpleMarkerSymbol junctionPointSymbol = new SimpleMarkerSymbolClass();
					junctionPointSymbol.Style = esriSimpleMarkerStyle.esriSMSCircle;
					junctionPointSymbol.Size = 10;

					//yellow
					color.Red = 255;
					color.Blue = 0;
					color.Green = 255;
					junctionPointSymbol.Color = color;

					junctionPointSymbol.Outline = true;
					junctionPointSymbol.OutlineSize = 2;

					//black
					color.Red = 0;
					color.Blue = 0;
					color.Green = 0;
					junctionPointSymbol.OutlineColor = color;

					simpleRend.Label = "TRFC_Junctions";
					simpleRend.Symbol = junctionPointSymbol as ISymbol;
					return simpleRend as IFeatureRenderer;

				// The turn symbology will be a thick purple line
				case esriNetworkElementType.esriNETTurn:

					ISimpleLineSymbol turnLineSymbol = new SimpleLineSymbolClass();
					turnLineSymbol.Style = esriSimpleLineStyle.esriSLSSolid;
					turnLineSymbol.Width = 4;

					//purple
					color.Red = 125;
					color.Blue = 125;
					color.Green = 0;
					turnLineSymbol.Color = color;

					simpleRend.Label = "TRFC_Turns";
					simpleRend.Symbol = turnLineSymbol as ISymbol;
					return simpleRend as IFeatureRenderer;

				// The edge symbology will be a thick blue line
				case esriNetworkElementType.esriNETEdge:

					ISimpleLineSymbol edgeLineSymbol = new SimpleLineSymbolClass();
					edgeLineSymbol.Style = esriSimpleLineStyle.esriSLSSolid;
					edgeLineSymbol.Width = 4;

					//blue
					color.Red = 0;
					color.Blue = 255;
					color.Green = 0;
					edgeLineSymbol.Color = color;

					simpleRend.Label = "TRFC_Edges";
					simpleRend.Symbol = edgeLineSymbol as ISymbol;
					return simpleRend as IFeatureRenderer;
			}
			return null;
		}

		private int GetLayerIndex(ILayer layer)
		{
			for (int index = 0; index < ArcMap.Document.FocusMap.LayerCount; index++)
			{
				ILayer layerAtIndex = ArcMap.Document.FocusMap.get_Layer(index);
				if (layerAtIndex == layer)
					return index;
			}
			return -1;
		}

		protected override void OnUpdate()
		{
			Enabled = ArcMap.Application != null;
		}
	}
}
