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
