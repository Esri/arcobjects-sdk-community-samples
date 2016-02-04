/*

   Copyright 2016 Esri

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

// This command removes the selected layer from the map
namespace NAEngine
{
	[Guid("53399A29-2B65-48d5-930F-804B88B85A34")]
	[ClassInterface(ClassInterfaceType.None)]
	[ProgId("NAEngine.RemoveLayer")]
	public sealed class cmdRemoveLayer : ESRI.ArcGIS.ADF.BaseClasses.BaseCommand
	{
		private ESRI.ArcGIS.Controls.IMapControl3 m_mapControl;

		public cmdRemoveLayer()
		{
			base.m_caption = "Remove Layer";
		}

		public override void OnClick()
		{
			if (m_mapControl == null)
			{
				MessageBox.Show("Error: Map control is null for this command");
				return;
			}

			// Get the layer that was right-clicked on in the table of contents
			// m_MapControl.CustomProperty was set in frmMain.axTOCControl1_OnMouseDown
			var layer = m_mapControl.CustomProperty as ILayer;
			if (layer == null)
			{
				MessageBox.Show("Error: The selected layer was not set as the CustomProperty of the map control");
				return;
			}

			// Remove the selected layer, then redraw the map
			m_mapControl.Map.DeleteLayer(layer);
			m_mapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, layer.AreaOfInterest);
		}

		public override void OnCreate(object hook)
		{
			// The "hook" was set as a MapControl in formMain_Load
			m_mapControl = hook as ESRI.ArcGIS.Controls.IMapControl3;
		}
	}
}
