using System;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;

namespace MultiItemBookmarks
{

	public class SpatialBookmarks : IMultiItem
	{
		private IHookHelper m_HookHelper;

		public SpatialBookmarks()
		{
			m_HookHelper = new HookHelperClass();
		}
	
		public int get_ItemBitmap(int index)
		{
			return 0;
		}
	
		public string get_ItemCaption(int index)
		{	
			//Get the bookmarks of the focus map
			IMapBookmarks mapBookmarks = (IMapBookmarks) m_HookHelper.FocusMap;

			//Get bookmarks enumerator
			IEnumSpatialBookmark enumSpatialBookmarks = mapBookmarks.Bookmarks;
			enumSpatialBookmarks.Reset();

			//Loop through the bookmarks to get bookmark names
			ISpatialBookmark spatialBookmark = enumSpatialBookmarks.Next();

			int bookmarkCount = 0;
			while (spatialBookmark != null)
			{
				//Get the correct bookmark
				if (bookmarkCount == index)
				{
					//Return the bookmark name
					return spatialBookmark.Name;
				}
				bookmarkCount = bookmarkCount + 1;
				spatialBookmark = enumSpatialBookmarks.Next();
			} 
			
			return "";
		}
	
		public bool get_ItemChecked(int index)
		{
			return false;
		}
	
		public bool get_ItemEnabled(int index)
		{
			return true;
		}
	
		public void OnItemClick(int index)
		{
			//Get the bookmarks of the focus map
			IMapBookmarks mapBookmarks = (IMapBookmarks) m_HookHelper.FocusMap;

			//Get bookmarks enumerator 
			IEnumSpatialBookmark enumSpatialBookmarks = mapBookmarks.Bookmarks;
			enumSpatialBookmarks.Reset();

			//Loop through the bookmarks to get bookmark to zoom to
			ISpatialBookmark spatialBookmark = enumSpatialBookmarks.Next();

			int bookmarkCount = 0;
			while (spatialBookmark != null)
			{
				//Get the correct bookmark
				if (bookmarkCount == index)
				{
					//Zoom to the bookmark
					spatialBookmark.ZoomTo(m_HookHelper.FocusMap);
					//Refresh the map
					m_HookHelper.ActiveView.Refresh();
				}
				bookmarkCount = bookmarkCount + 1;
				spatialBookmark = enumSpatialBookmarks.Next();
			} 

		}
	
		public int OnPopup(object hook)
		{
			m_HookHelper.Hook = hook;

			//Get the bookmarks of the focus map
			IMapBookmarks mapBookmarks = (IMapBookmarks) m_HookHelper.FocusMap;

			//Get bookmarks enumerator
			IEnumSpatialBookmark enumSpatialBookmarks = mapBookmarks.Bookmarks;
			enumSpatialBookmarks.Reset();

			//Loop through the bookmarks to count them
			ISpatialBookmark spatialBookmark = enumSpatialBookmarks.Next();

			int bookmarkCount = 0;

			while (spatialBookmark != null) 
			{
				bookmarkCount = bookmarkCount + 1;
				spatialBookmark = enumSpatialBookmarks.Next();
			} 

			//Return the number of multiitems
			return bookmarkCount;
		}
	
		public string Caption
		{
			get
			{
				return "Spatial Bookmarks";
			}
		}
	
		public int HelpContextID
		{
			get
			{
				return 0;
			}
		}
	
		public string HelpFile
		{
			get
			{
				return null;
			}
		}
	
		public string Message
		{
			get
			{
				return "Spatial bookmarks in the focus map";
			}
		}
	
		public string Name
		{
			get
			{
				return "Spatial Bookmarks";
			}
		}
	}
}
