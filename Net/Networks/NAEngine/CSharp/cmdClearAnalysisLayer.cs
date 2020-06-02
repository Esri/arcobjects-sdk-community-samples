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
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.NetworkAnalyst;

// This command deletes all the network locations and analysis results from the selected NALayer.
namespace NAEngine
{
	[Guid("773CCD44-C46A-42eb-A1B2-E00C7B765783")]
	[ClassInterface(ClassInterfaceType.None)]
	[ProgId("NAEngine.ClearAnalysisLayer")]
	public sealed class cmdClearAnalysisLayer : ESRI.ArcGIS.ADF.BaseClasses.BaseCommand
	{
		private IMapControl3 m_mapControl;

		public cmdClearAnalysisLayer()
		{
			base.m_caption = "Clear Analysis Layer";
		}

		public override void OnClick()
		{
		
			if (m_mapControl == null)
			{
				MessageBox.Show("Error: Map control is null for this command");
				return;
			}

			// Get the NALayer and corresponding NAContext of the layer that
			// was right-clicked on in the table of contents
			// m_MapControl.CustomProperty was set in frmMain.axTOCControl1_OnMouseDown
			INALayer naLayer = m_mapControl.CustomProperty as INALayer;
			if (naLayer == null)
			{
				MessageBox.Show("Error: NALayer was not set as the CustomProperty of the map control");
				return;
			}

			var naEnv = CommonFunctions.GetTheEngineNetworkAnalystEnvironment();
			if (naEnv == null || naEnv.NAWindow == null)
			{
				MessageBox.Show("Error: EngineNetworkAnalystEnvironment is not properly configured");
				return;
			} 

			// Set the active Analysis layer
			IEngineNAWindow naWindow = naEnv.NAWindow;
			if (naWindow.ActiveAnalysis != naLayer)
				naWindow.ActiveAnalysis = naLayer;

			// Remember what the current category is
			IEngineNAWindowCategory originalCategory = naWindow.ActiveCategory;

			// Loop through deleting all the items from all the categories
			INamedSet naClasses = naLayer.Context.NAClasses;
			var naHelper = naEnv as IEngineNetworkAnalystHelper;
			for (int i = 0; i < naClasses.Count; i++)
			{
				IEngineNAWindowCategory category = naWindow.get_CategoryByNAClassName(naClasses.get_Name(i));
				naWindow.ActiveCategory = category;
				naHelper.DeleteAllNetworkLocations();
			}

			//Reset to the original category
			naWindow.ActiveCategory = originalCategory;

			// Redraw the map
			m_mapControl.Refresh(esriViewDrawPhase.esriViewGeography, naLayer, m_mapControl.Extent);
		}

		public override void OnCreate(object hook)
		{
			// The "hook" was set as a MapControl in formMain_Load
			m_mapControl = hook as IMapControl3;
		}
	}
}
