using System;
using ESRI.ArcGIS.SystemUI;

namespace SchematicApplication
{
	/// <summary>
	/// Summary description for NavigationMenu.
	/// </summary>
	public class CreateMenuSchematicEditor : IMenuDef
	{
		public CreateMenuSchematicEditor()
		{
		}
	
		public void GetItemInfo(int pos, IItemDef itemDef)
		{
			//Commands for the menu - the object browser lists these commands
			switch (pos)
			{
				case 0:
					itemDef.ID = "esriControls.ControlsSchematicStartEditCommand";
					break;
				case 1:
					itemDef.ID = "esriControls.ControlsSchematicStopEditCommand";
					break;
				case 2:
					itemDef.ID = "esriControls.ControlsSchematicSaveEditsCommand";
					itemDef.Group = true;
					break;
				case 3:
					itemDef.ID = "esriControls.ControlsSchematicRemoveLinkPointsCommand";
					itemDef.Group = true;
					break;
				case 4:
					itemDef.ID = "esriControls.ControlsSchematicSquareLinksCommand";
					break;
			}
		}
	
		public string Caption
		{
			get
			{
				return "SchematicEditor";
			}
		}
	
		public int ItemCount
		{
			get
			{
				return 5;   // there are 5 items on this menu 
			}
		}
	
		public string Name
		{
			get
			{
				return "SchematicEditor";
			}
		}
	}
}
