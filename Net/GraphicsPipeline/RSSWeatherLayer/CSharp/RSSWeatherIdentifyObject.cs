using System;
using System.Collections;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.Display;

namespace RSSWeatherLayer
{
	/// <summary>
	/// Summary description for GlobeWeatherIdentifyObject.
	/// </summary>
	public class RSSWeatherIdentifyObject : IIdentifyObj, IIdentifyObject, IDisposable 
	{
		private RSSWeatherLayerClass		m_weatherLayer	= null;
		private IPropertySet				      m_propset				= null;
		private IdentifyDlg					      m_identifyDlg		= null;

    #region Ctor
    /// <summary>
		/// Class Ctor
		/// </summary>
    public RSSWeatherIdentifyObject()
		{
    }
    #endregion

    #region IIdentifyObject Members

    /// <summary>
    /// PropertySet of the identify object
    /// </summary>
    /// <remarks>The information passed by the layer to the identify dialog is encapsulated
    /// in a PropertySet</remarks>
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

    /// <summary>
    /// Name of the identify object.
    /// </summary>
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

		/// <summary>
    /// Flashes the identified object on the screen.
		/// </summary>
		/// <param name="pDisplay"></param>
    public void Flash(IScreenDisplay pDisplay)
		{
		
		}

		/// <summary>
    /// Indicates if the object can identify the specified layer
		/// </summary>
		/// <param name="pLayer"></param>
		/// <returns></returns>
    public bool CanIdentify(ILayer pLayer)
		{
			if(!(pLayer is RSSWeatherLayerClass))
				return false;
			
			//cache the layer
      m_weatherLayer = (RSSWeatherLayerClass)pLayer;	

			return true;;
		}

    /// <summary>
    /// The window handle.
    /// </summary>
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

    /// <summary>
    /// Name of the identify object.
    /// </summary>
		string ESRI.ArcGIS.Carto.IIdentifyObj.Name
		{
			get
			{
				return "WeatherInfo";
			}
		}

		/// <summary>
    /// Target layer for identification.
		/// </summary>
    public ILayer Layer
		{
			get
			{
				return m_weatherLayer;
			}
		}


    /// <summary>
    /// Displays a context sensitive popup menu at the specified location.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
		public void PopUpMenu(int x, int y)
		{
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

	}
}
