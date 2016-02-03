using System;
using ESRI.ArcGIS.SystemUI;

namespace SchematicApplication
{
	/// <summary>
	/// Summary description for ToolbarSubMenu.
	/// </summary>
	public class CreateSubMenuSchematic : IMenuDef
	{
		public CreateSubMenuSchematic()
		{
		}
	
		public void GetItemInfo(int pos, IItemDef itemDef)
		{
			//Commands for the menu - the object browser lists these commands
			switch (pos)
			{
				case 0:
					itemDef.ID = "esriControls.ControlsSchematicRestoreDefaultLayerPropertiesCommand";
					break;
				case 1:
					itemDef.ID = "esriControls.ControlsSchematicPropagateLayerPropertiesCommand";
					break;
				case 2:
					itemDef.ID = "esriControls.ControlsSchematicImportLayerPropertiesCommand";
					break;
			}
		}
	
		public string Caption
		{
			get
			{
				return "Schematic Layer Properties";
			}
		}
	
		public int ItemCount
		{
			get
			{
				return 3; //There are 3 items on this submenu
			}
		}
	
		public string Name
		{
			get
			{
				return "SchematicLayerProperties";
			}
		}
	}
}
