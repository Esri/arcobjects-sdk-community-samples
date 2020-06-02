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
using ESRI.ArcGIS.NetworkAnalyst;


// This command brings up the property pages for the NALayer.
namespace NAEngine
{
	[Guid("04B67C95-96DD-4afe-AF62-942255ACBA71")]
	[ClassInterface(ClassInterfaceType.None)]
	[ProgId("NAEngine.NALayerProperties")]
	public sealed class cmdNALayerProperties : ESRI.ArcGIS.ADF.BaseClasses.BaseCommand
	{
		private ESRI.ArcGIS.Controls.IMapControl3 m_mapControl;

		public cmdNALayerProperties()
		{
			base.m_caption = "Properties...";
		}

		public override void OnClick()
		{
			if (m_mapControl == null)
			{
				MessageBox.Show("Error: Map control is null for this command");
				return;
			}

			// Get the NALayer that was right-clicked on in the table of contents
			// m_MapControl.CustomProperty was set in frmMain.axTOCControl1_OnMouseDown
			var naLayer = m_mapControl.CustomProperty as INALayer3;
			if (naLayer == null)
			{
				MessageBox.Show("Error: NALayer was not set as the CustomProperty of the map control");
				return;
			}

			// Show the Property Page form for the NALayer
			var props = new frmNALayerProperties();
			if (props.ShowModal(naLayer))
			{
				// Notify the ActiveView that the contents have changed so the TOC and NAWindow know to refresh themselves.
				m_mapControl.ActiveView.ContentsChanged();
			}
		}

		public override void OnCreate(object hook)
		{
			// The hook for OnCreate is set as a MapControl in frmMain_Load
			m_mapControl = hook as ESRI.ArcGIS.Controls.IMapControl3;
		}
	}
}
