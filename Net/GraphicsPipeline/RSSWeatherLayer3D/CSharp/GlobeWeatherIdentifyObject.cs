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
using System.Collections;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.GlobeCore;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Analyst3D;

namespace RSSWeatherLayer3D
{
	/// <summary>
	/// Summary description for GlobeWeatherIdentifyObject.
	/// </summary>
	public class GlobeWeatherIdentifyObject : IIdentifyObj, IIdentifyObject, IDisposable 
	{
		private RSSWeatherLayer3DClass		m_weatherLayer	= null;
		private IPropertySet				        m_propset				= null;
		private IdentifyDlg					        m_identifyDlg		= null;
		
		private System.Windows.Forms.ContextMenu m_menu;

		public GlobeWeatherIdentifyObject()
		{
			InitializeContextMenu();
		}

		#region IIdentifyObject Members

		public IPropertySet PropertySet
		{
			get
			{
				return m_propset;
			}
			set
			{
				m_propset = value;
			}
		}

		public string Name
		{
			get
			{
				return "WeatherInfo";
			}
			set
			{
				// TODO:  Add GlobeWeatherIdentifyObject.Name setter implementation
			}
		}

		#endregion

		#region IIdentifyObj Members

		public void Flash(IScreenDisplay pDisplay)
		{
			if(null == m_propset)
				return;

			long zipCode = Convert.ToInt64(m_propset.GetProperty("ZIPCODE")); 
			m_weatherLayer.Flash(zipCode);
		}

		public bool CanIdentify(ILayer pLayer)
		{
			if(!(pLayer is RSSWeatherLayer3DClass))
				return false;
			
			m_weatherLayer = (RSSWeatherLayer3DClass)pLayer;	

			return true;;
		}

		public int hWnd
		{
			get
			{
				if(null == m_identifyDlg || m_identifyDlg.Handle.ToInt32() == 0)
				{
					m_identifyDlg = new IdentifyDlg();
					m_identifyDlg.CreateControl();

					m_identifyDlg.SetProperties(m_propset);
				}
				
				return m_identifyDlg.Handle.ToInt32();
			}
		}

		string ESRI.ArcGIS.Carto.IIdentifyObj.Name
		{
			get
			{
				return "WeatherInfo";
			}
		}

		public ILayer Layer
		{
			get
			{
				return m_weatherLayer;
			}
		}


		public void PopUpMenu(int x, int y)
		{
			//System.Windows.Forms.Form.ActiveForm
			//m_menu.Show(m_identifyDlg, new System.Drawing.Point(x,y));
			
			m_menu.Show(m_identifyDlg, m_identifyDlg.PointToClient(new System.Drawing.Point(x,y)));
		}

		#endregion

		#region IDisposable Members

		public void Dispose()
		{
			if(!m_identifyDlg.IsDisposed)
				m_identifyDlg.Dispose();

			m_weatherLayer	= null;
			m_propset				= null;
			
		}

		#endregion

		private void InitializeContextMenu()
		{
			m_menu = new System.Windows.Forms.ContextMenu();
			
			System.Windows.Forms.MenuItem menuFlash = new System.Windows.Forms.MenuItem("Flash", new System.EventHandler(menuFlash_Click));
			m_menu.MenuItems.Add(menuFlash);

			System.Windows.Forms.MenuItem menuSeparator = new System.Windows.Forms.MenuItem("-");
			m_menu.MenuItems.Add(menuSeparator);

			System.Windows.Forms.MenuItem menuZoomTo = new System.Windows.Forms.MenuItem("ZoomTo", new System.EventHandler(menuZoomTo_Click));
			m_menu.MenuItems.Add(menuZoomTo);

			System.Windows.Forms.MenuItem menuSelect = new System.Windows.Forms.MenuItem("Select", new System.EventHandler(menuSelect_Click));
			m_menu.MenuItems.Add(menuSelect);
		}

		private void menuFlash_Click(System.Object sender, System.EventArgs e) 
		{
			if(null == m_propset)
				return;

			long zipCode = Convert.ToInt64(m_propset.GetProperty("ZIPCODE")); 
			m_weatherLayer.Flash(zipCode);
		}

		private void menuZoomTo_Click(System.Object sender, System.EventArgs e) 
		{
			if(null == m_propset)
				return;

			long zipCode = Convert.ToInt64(m_propset.GetProperty("ZIPCODE")); 
			m_weatherLayer.ZoomTo(zipCode);
		}

		private void menuSelect_Click(System.Object sender, System.EventArgs e) 
		{
			if(null == m_propset)
				return;

			long zipCode = Convert.ToInt64(m_propset.GetProperty("ZIPCODE")); 
			m_weatherLayer.Select(zipCode, true);
		}

	}
}
