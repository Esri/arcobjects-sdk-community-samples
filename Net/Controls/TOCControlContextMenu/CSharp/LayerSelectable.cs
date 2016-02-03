using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.SystemUI;

namespace ContextMenu
{
	public sealed class LayerSelectable : BaseCommand, ICommandSubType
	{
		private IMapControl3 m_mapControl;
		private long m_subType;

		public LayerSelectable()
		{
		}
	
		public override void OnClick()
		{
			IFeatureLayer layer = (IFeatureLayer) m_mapControl.CustomProperty;
			if (m_subType == 1)	layer.Selectable = true;
			if (m_subType == 2) layer.Selectable = false;
		}
	
		public override void OnCreate(object hook)
		{
			m_mapControl = (IMapControl3) hook;
		}
		
		public override bool Enabled
		{
			get
			{
				ILayer layer = (ILayer) m_mapControl.CustomProperty;
				if (layer is IFeatureLayer)
				{
					IFeatureLayer featureLayer = (IFeatureLayer) layer;
					if (m_subType == 1) return !featureLayer.Selectable;
					else return featureLayer.Selectable;
				}
				else
				{
					return false;
				}
			}
		}
	
		public int GetCount()
		{
			return 2;
		}
	
		public void SetSubType(int SubType)
		{
			m_subType = SubType;
		}
	
		public override string Caption
		{
			get
			{
				if (m_subType == 1) return "Layer Selectable";
				else  return "Layer Unselectable";
			}
		}
	}
}

