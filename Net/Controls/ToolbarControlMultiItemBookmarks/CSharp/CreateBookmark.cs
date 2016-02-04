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
using System;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.ADF.BaseClasses;

namespace MultiItemBookmarks
{
    [ComVisible(false)]
	public sealed class CreateBookmark : BaseCommand
	{
		private IHookHelper m_HookHelper = new HookHelperClass();

		public CreateBookmark()
		{
			base.m_caption = "Create...";
			base.m_category = "Developer Samples";
			base.m_enabled = true;
			base.m_message = "Creates a spatial bookmark based upon the current extent";
			base.m_name = "Create...";
			base.m_toolTip = "Create spatial bookmark";
		}
	
		public override void OnCreate(object hook)
		{
			m_HookHelper.Hook = hook;
		}
	
	
		public override void OnClick()
		{
			//Get a name for bookmark from the user
			frmBookmark frm = new frmBookmark();
			frm.ShowDialog();
			int check = frm.Check; 
			string sName = "";	
	
			//OK button pressed					
			if (check == 1)
			{
				sName = frm.Bookmark;
			}
			if (sName == "") return;
			

			//Get the focus map 
			IActiveView activeView = (IActiveView) m_HookHelper.FocusMap;

			//Create a new bookmark 
			IAOIBookmark bookmark = new AOIBookmarkClass();
			//Set the location to the current extent of the focus map
			bookmark.Location = activeView.Extent;
			//Set the bookmark name
			bookmark.Name = sName;

			//Get the bookmark collection of the focus map
			IMapBookmarks mapBookmarks = (IMapBookmarks) m_HookHelper.FocusMap;
			//Add the bookmark to the bookmarks collection
			mapBookmarks.AddBookmark(bookmark);
		}
	}
}
