/*

   Copyright 2019 Esri

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.

   See the License for the specific language governing permissions and
   limitations under the License.

*/
//*************************************************************************************
//       ArcGIS Network Analyst extension - Load network layer from active analysis to the table of contents
//
//   This simple code shows how to load the network layer and its associated sources
//    to a map
//
//************************************************************************************

using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.NetworkAnalyst;
using ESRI.ArcGIS.NetworkAnalystUI;

namespace LoadNetworkLayerFromActiveAnalysisToTOC
{
	public class LoadNetworkLayerFromActiveAnalysisToTOC : ESRI.ArcGIS.Desktop.AddIns.Button
	{
		public LoadNetworkLayerFromActiveAnalysisToTOC()
		{
		}

		/// <summary>
		/// OnClick is the main function for the add-in.  When the button is clicked in ArcMap,
		///  this code will execute
		/// </summary>
		protected override void OnClick()
		{
			// Verify that the network extension has been loaded properly.
			var naExt = ArcMap.Application.FindExtensionByName("Network Analyst") as INetworkAnalystExtension;
			if (naExt == null || naExt.NAWindow == null)
				return;

			// There must be an active analysis layer
			INALayer naLayer = naExt.NAWindow.ActiveAnalysis;
			if (naLayer == null || naLayer.Context == null)
				return;

			// Use the network layer factory to generate the layers
			ILayerFactory layerFactory = new EngineNetworkLayerFactoryClass();
			var dataset = naLayer.Context.NetworkDataset as IDataset;
			if (layerFactory.get_CanCreate(dataset.FullName))
			{
				// Calling create will open a popup asking the user if the network sources
				//  should be added as well.  If the user clicks NO, then the returned
				//  IEnumLayer will only return a network layer for the network dataset.
				//  If the user clicks YES, then IEnumLayer will also return layers for 
				//  the network sources
				IEnumLayer enumLayer = layerFactory.Create(dataset.FullName);
				ILayer layer = enumLayer.Next();
				while (layer != null)
				{
					ArcMap.Document.FocusMap.AddLayer(layer);
					layer = enumLayer.Next();
				}
			}
		}

		protected override void OnUpdate()
		{
			Enabled = (ArcMap.Application != null);
		}
	}

}
