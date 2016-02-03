using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.SystemUI;

namespace ContextMenu
{
	public sealed class LayerVisibility : BaseCommand, ICommandSubType 
	{
		private IHookHelper m_hookHelper = new HookHelperClass();
		private long m_subType;

		public LayerVisibility()
		{
		}
	
		public override void OnClick()
		{
			for (int i=0; i <= m_hookHelper.FocusMap.LayerCount - 1; i++)
			{
				if (m_subType == 1) m_hookHelper.FocusMap.get_Layer(i).Visible = true;
				if (m_subType == 2) m_hookHelper.FocusMap.get_Layer(i).Visible = false;
			}
			m_hookHelper.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography,null,null);
		}
	
		public override void OnCreate(object hook)
		{
			m_hookHelper.Hook = hook;
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
				if (m_subType == 1) return "Turn All Layers On";
				else  return "Turn All Layers Off";
			}
		}
	
		public override bool Enabled
		{
			get
			{
				bool enabled = false; int i;
				if (m_subType == 1) 
				{
					for (i=0;i<=m_hookHelper.FocusMap.LayerCount - 1;i++)
					{
						if (m_hookHelper.ActiveView.FocusMap.get_Layer(i).Visible == false)
						{
							enabled = true;
							break;
						}
					}
				}
				else 
				{
					for (i=0;i<=m_hookHelper.FocusMap.LayerCount - 1;i++)
					{
						if (m_hookHelper.ActiveView.FocusMap.get_Layer(i).Visible == true)
						{
							enabled = true;
							break;
						}
					}
				}
				return enabled;
			}
		}
	}
}
