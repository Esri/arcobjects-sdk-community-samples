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
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.SystemUI;

namespace ContextMenu
{
	public class ScaleThresholds : BaseCommand, ICommandSubType
	{
		private IMapControl3 m_mapControl;
		private long m_subType;

		public ScaleThresholds()
		{

		}
	
		public override void OnClick()
		{
			ILayer layer = (ILayer) m_mapControl.CustomProperty;
			if (m_subType == 1) layer.MaximumScale = m_mapControl.MapScale;
			if (m_subType == 2) layer.MinimumScale = m_mapControl.MapScale;
			if (m_subType == 3)
			{
				layer.MaximumScale = 0;
				layer.MinimumScale = 0;
			}
			m_mapControl.Refresh(esriViewDrawPhase.esriViewGeography,null,null);
		}
	
		public override void OnCreate(object hook)
		{
			m_mapControl = (IMapControl3) hook;
		}
	
		public int GetCount()
		{
			return 3;
		}
	
		public void SetSubType(int SubType)
		{
			m_subType = SubType;
		}
	
		public override string Caption
		{
			get
			{
				if (m_subType == 1) return "Set Maximum Scale";
				else if (m_subType == 2) return "Set Minimum Scale";
				else return "Remove Scale Thresholds";
			}
		}
	
		public override bool Enabled
		{
			get
			{
				bool enabled = true;
				ILayer layer = (ILayer) m_mapControl.CustomProperty;
				
				if (m_subType == 3)
				{
					if ((layer.MaximumScale == 0) & (layer.MinimumScale == 0)) enabled = false;
				}
				return enabled;
			}
		}
	}
}

