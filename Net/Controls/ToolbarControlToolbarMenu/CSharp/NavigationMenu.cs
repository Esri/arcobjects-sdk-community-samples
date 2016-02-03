using System;
using ESRI.ArcGIS.SystemUI;

namespace ToolbarMenu
{
	/// <summary>
	/// Summary description for NavigationMenu.
	/// </summary>
	public class NavigationMenu : IMenuDef
	{
		public NavigationMenu()
		{
			//
			// TODO: Add constructor logic here
			//
		}
	
		public void GetItemInfo(int pos, IItemDef itemDef)
		{
			//Commands for the menu - the object browser lists these commands
			switch (pos)
			{
				case 0:
					itemDef.ID = "esriControls.ControlsMapZoomInFixedCommand";
					break;
				case 1:
					itemDef.ID = "esriControls.ControlsMapZoomOutFixedCommand";
					break;
				case 2:
					itemDef.ID = "esriControls.ControlsMapFullExtentCommand";
					itemDef.Group = true;
					break;
				case 3:
					itemDef.ID = "esriControls.ControlsMapZoomToLastExtentBackCommand";
					break;
				case 4:
					itemDef.ID = "esriControls.ControlsMapZoomToLastExtentForwardCommand";
					break;
			}
		}
	
		public string Caption
		{
			get
			{
				return "Navigation";
			}
		}
	
		public int ItemCount
		{
			get
			{
				return 5;
			}
		}
	
		public string Name
		{
			get
			{
				return "Navigation";
			}
		}
	}
}
