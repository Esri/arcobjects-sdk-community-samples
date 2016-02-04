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

namespace ContextMenu
{
	public sealed class RemoveLayer : BaseCommand  
	{
		private IMapControl3 m_mapControl;

		public RemoveLayer()
		{
			base.m_caption = "Remove Layer";
		}
	
		public override void OnClick()
		{
			ILayer layer =  (ILayer) m_mapControl.CustomProperty;
			m_mapControl.Map.DeleteLayer(layer);
		}
	
		public override void OnCreate(object hook)
		{
			m_mapControl = (IMapControl3) hook;
		}

	}
}


