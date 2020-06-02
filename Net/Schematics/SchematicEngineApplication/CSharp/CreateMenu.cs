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
