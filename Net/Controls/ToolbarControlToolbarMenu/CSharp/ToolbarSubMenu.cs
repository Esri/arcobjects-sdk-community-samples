using System;
using ESRI.ArcGIS.SystemUI;

namespace ToolbarMenu
{
	/// <summary>
	/// Summary description for ToolbarSubMenu.
	/// </summary>
	public class ToolbarSubMenu : IMenuDef
	{
		public ToolbarSubMenu()
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
					itemDef.ID = "esriControls.ControlsMapUpCommand";
					break;
				case 1:
					itemDef.ID = "esriControls.ControlsMapDownCommand";
					break;
				case 2:
					itemDef.ID = "esriControls.ControlsMapLeftCommand";
					break;
				case 3:
					itemDef.ID = "esriControls.ControlsMapRightCommand";
					break;
				case 4:
					itemDef.ID = "esriControls.ControlsMapPageUpCommand";
					itemDef.Group = true;
					break;
				case 5:
					itemDef.ID = "esriControls.ControlsMapPageDownCommand";
					break;
				case 6:
					itemDef.ID = "esriControls.ControlsMapPageLeftCommand";
					break;
				case 7:
					itemDef.ID = "esriControls.ControlsMapPageRightCommand";
					break;
			}
		}
	
		public string Caption
		{
			get
			{
				return "SubMenu";
			}
		}
	
		public int ItemCount
		{
			get
			{
				return 8;
			}
		}
	
		public string Name
		{
			get
			{
				return "SubMenu";
			}
		}
	}
}
