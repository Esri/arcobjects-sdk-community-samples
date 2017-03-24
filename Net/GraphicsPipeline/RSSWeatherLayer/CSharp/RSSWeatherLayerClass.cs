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
using System.Collections;
using System.Data;
using System.Runtime.InteropServices;
using System.Xml;
using System.Threading;
using System.Timers;
using System.Text.RegularExpressions;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.ComponentModel;
using Microsoft.Win32;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.DataSourcesFile;

namespace RSSWeatherLayer
{
  #region WeatherItemEventArgs class members
  public sealed class WeatherItemEventArgs : EventArgs
	{
		private int m_id;
		private long m_zipCode;
		private double m_x;
		private double m_y;
		private int m_iconWidth;
		private int m_iconHeight;
    public WeatherItemEventArgs(int id, long zipCode, double X, double Y, int iconWidth, int iconHeight)
		{
			m_id = id;
			m_zipCode = zipCode;
			m_x = X;
			m_y = Y;
			m_iconWidth = iconWidth;
			m_iconHeight = iconHeight;
		}

		public int ID
		{
			get { return m_id; }
		}
		public long ZipCode
		{
			get { return m_zipCode; }
		}
		public double mapY
		{
			get { return m_y; }
		}
		public double mapX
		{
			get { return m_x; }
		}
		public int IconWidth
		{
			get { return m_iconWidth; }
		}
		public int IconHeight
		{
			get { return m_iconHeight; }
		}
	}
  #endregion

	//declare delegates for the event handling
	public delegate void WeatherItemAdded(object sender, WeatherItemEventArgs args);
	public delegate void WeatherItemsUpdated(object sender, EventArgs args);


  /// <summary>
  /// RSSWeatherLayerClass is a custom layer for ArcMap/MapControl. It inherits CustomLayerBase
  /// which implements the relevant interfaces required by the Map.
  /// This sample is a comprehensive sample of a real life scenario for creating a new layer in 
  /// order to consume a web service and display the information in a map.
  /// In this sample you can find implementation of simple editing capabilities, selection by 
  /// attribute and by location, persistence and identify.
  /// </summary>
  [Guid("3460FB55-4326-4d28-9F96-D62211B0C754")]
  [ClassInterface(ClassInterfaceType.None)]
  [ComVisible(true)]
  [ProgId("RSSWeatherLayer.RSSWeatherLayerClass")]
  public sealed class RSSWeatherLayerClass : BaseDynamicLayer, IIdentify
  {
    #region class members

    private System.Timers.Timer			    m_timer									    = null;
		private Thread									    m_updateThread					    = null;
		private string									    m_iconFolder						    = string.Empty;
    private DataTable                   m_table                     = null;
    private DataTable								    m_symbolTable						    = null;
		private DataTable 							    m_locations							    = null;
		private ISymbol									    m_selectionSymbol				    = null;
    private IDisplay                    m_display                   = null;
		private string									    m_dataFolder						    = string.Empty;
		private int											    m_layerSRFactoryCode		    = 0;
		private int											    m_symbolSize			          = 32;

    private IPoint                      m_point                     = null;
    private IPoint                      m_llPnt                     = null;
    private IPoint                      m_urPnt                     = null;
    private IEnvelope                   m_env                       = null;

    // dynamic display members
    private IDynamicGlyphFactory2				m_dynamicGlyphFactory				= null;
    private IDynamicSymbolProperties2		m_dynamicSymbolProperties		= null;
    private IDynamicCompoundMarker2     m_dynamicCompoundMarker     = null;
    private IDynamicGlyph               m_textGlyph                 = null;
    private IDynamicGlyph               m_selectionGlyph            = null;
    private bool												m_bDDOnce										= true;

		//weather items events
		public event WeatherItemAdded       OnWeatherItemAdded;
		public event WeatherItemsUpdated    OnWeatherItemsUpdated;
    #endregion

    #region Constructor
    /// <summary>
    /// The class has only default CTor.
    /// </summary>
    public RSSWeatherLayerClass() : base()
		{
      try
      {
        //set the layer's name
        base.m_sName = "RSS Weather Layer";
        //ask the Map to create a separate cache for the layer
        base.m_IsCached = false;

				// the underlying data for this layer is always in WGS1984 geographical coordinate system
				m_spatialRef = CreateGeographicSpatialReference();
				m_layerSRFactoryCode = m_spatialRef.FactoryCode;

        //get the directory for the layer's cache. If it does not exist, create it.
        m_dataFolder = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "RSSWeatherLayer");
        if (!System.IO.Directory.Exists(m_dataFolder))
        {
          System.IO.Directory.CreateDirectory(m_dataFolder);
        }
        m_iconFolder = m_dataFolder;

        //instantiate the timer for the weather update
        m_timer = new System.Timers.Timer(1000);
        m_timer.Enabled = false;
        m_timer.Elapsed += new ElapsedEventHandler(OnUpdateTimer);

        //initialize the layer's tables (main table as well as the symbols table)
        InitializeTables();

        //get the location list from a featureclass (US major cities) and synchronize it with the 
        //cached information in case it exists.
        if (null == m_locations)
          InitializeLocations();

        //initialize the selection symbol used to highlight selected weather items
        InitializeSelectionSymbol();

        m_point = new PointClass();
        m_llPnt = new PointClass();
        m_urPnt = new PointClass();
        m_env = new EnvelopeClass();

        //connect to the RSS service
        Connect();
      }
      catch (Exception ex)
      {
        System.Diagnostics.Trace.WriteLine(ex.Message);
      }
		}

    

    ~RSSWeatherLayerClass()
    {
      Disconnect();
    }
		#endregion
		
		#region Overriden methods

    /// <summary>
    /// Draws the layer to the specified display for the given draw phase. 
    /// </summary>
    /// <param name="drawPhase"></param>
    /// <param name="Display"></param>
    /// <param name="trackCancel"></param>
    /// <remarks>the draw method is set as an abstract method and therefore must be overridden</remarks>
		public override void Draw(esriDrawPhase drawPhase, IDisplay Display, ITrackCancel trackCancel)
		{
			if(drawPhase != esriDrawPhase.esriDPGeography) return;
			if(Display == null) return;
			if(m_table == null || m_symbolTable == null) return;

			m_display = Display;
			
			IEnvelope envelope = Display.DisplayTransformation.FittedBounds as IEnvelope;	
			
			double lat, lon;
			int iconCode;
			bool selected;
			ISymbol symbol = null;

			//loop through the rows. Draw each row that has a shape
			foreach (DataRow row in m_table.Rows)
			{
				//get the Lat/Lon of the item
				lat = Convert.ToDouble(row[3]);
				lon = Convert.ToDouble(row[4]);
				//get the icon ID
				iconCode = Convert.ToInt32(row[8]);

				//get the selection state of the item
				selected = Convert.ToBoolean(row[13]);
				
				if(lon >= envelope.XMin && lon <= envelope.XMax && lat >= envelope.YMin && lat <= envelope.YMax) 
				{	
					//search for the symbol in the symbology table
					symbol = GetSymbol(iconCode, row);
					if(null == symbol)
						continue;

          m_point.X = lon;
          m_point.Y = lat;
          m_point.SpatialReference = m_spatialRef;

					//reproject the point to the DataFrame's spatial reference
					if (null != m_mapSpatialRef && m_mapSpatialRef.FactoryCode != m_layerSRFactoryCode)
            m_point.Project(m_mapSpatialRef);

					Display.SetSymbol(symbol);
          Display.DrawPoint(m_point);

					if(selected)
					{
						Display.SetSymbol(m_selectionSymbol);
            Display.DrawPoint(m_point);
					}
				}
			}
		}

    /// <summary>
    /// Draw the layer while in dynamic mode
    /// </summary>
    /// <param name="DynamicDrawPhase"></param>
    /// <param name="Display"></param>
    /// <param name="DynamicDisplay"></param>
    public override void DrawDynamicLayer(esriDynamicDrawPhase DynamicDrawPhase, IDisplay Display, IDynamicDisplay DynamicDisplay)
    {
      if (DynamicDrawPhase != esriDynamicDrawPhase.esriDDPCompiled)
        return;

      if (!m_bValid || !m_visible)
        return;


      if (m_bDDOnce)
      {
        m_dynamicGlyphFactory = DynamicDisplay.DynamicGlyphFactory as IDynamicGlyphFactory2;
        m_dynamicSymbolProperties = DynamicDisplay as IDynamicSymbolProperties2;
        m_dynamicCompoundMarker = DynamicDisplay as IDynamicCompoundMarker2;

        m_textGlyph = m_dynamicGlyphFactory.get_DynamicGlyph(1, esriDynamicGlyphType.esriDGlyphText, 1);

        // create glyph for the selection symbol
        if (m_selectionSymbol == null)
          InitializeSelectionSymbol();

        m_selectionGlyph = m_dynamicGlyphFactory.CreateDynamicGlyph(m_selectionSymbol);

        m_bDDOnce = false;
      }

			m_display = Display;


      double lat, lon;
      int iconCode;
      int iconWidth = 0;
      bool selected;
      IDynamicGlyph dynamicGlyph = null;
			float symbolSized;
      string citiName = string.Empty;
      string temperature = string.Empty;

      //loop through the rows. Draw each row that has a shape
      foreach (DataRow row in m_table.Rows)
      {
        //get the Lat/Lon of the item
        lat = Convert.ToDouble(row[3]);
        lon = Convert.ToDouble(row[4]);
        //get the icon ID
        iconCode = Convert.ToInt32(row[8]);

        // get citiname and temperature
        citiName = Convert.ToString(row[2]);
        temperature = string.Format("{0} F", row[5]);

        //get the selection state of the item
        selected = Convert.ToBoolean(row[13]);

        //search for the symbol in the symbology table
        dynamicGlyph = GetDynamicGlyph(m_dynamicGlyphFactory, iconCode, row, out iconWidth);
        if (null == dynamicGlyph)
          continue;

        m_point.X = lon;
        m_point.Y = lat;
        m_point.SpatialReference = m_spatialRef;

        //reproject the point to the DataFrame's spatial reference
				if (null != m_spatialRef && m_mapSpatialRef.FactoryCode != m_layerSRFactoryCode)
          m_point.Project(m_mapSpatialRef);

        symbolSized = 1.35f * (float)(m_symbolSize / (double)iconWidth);

        // draw the weather item

        // 1. set the whether symbol properties
				m_dynamicSymbolProperties.set_DynamicGlyph(esriDynamicSymbolType.esriDSymbolMarker, dynamicGlyph);
				m_dynamicSymbolProperties.set_RotationAlignment(esriDynamicSymbolType.esriDSymbolMarker, esriDynamicSymbolRotationAlignment.esriDSRAScreen);
				m_dynamicSymbolProperties.set_Heading(esriDynamicSymbolType.esriDSymbolMarker, 0.0f);
				m_dynamicSymbolProperties.SetColor(esriDynamicSymbolType.esriDSymbolMarker, 1.0f, 1.0f, 1.0f, 1.0f);
        m_dynamicSymbolProperties.SetScale(esriDynamicSymbolType.esriDSymbolMarker, symbolSized, symbolSized);
        m_dynamicSymbolProperties.set_Smooth(esriDynamicSymbolType.esriDSymbolMarker, false);

        // 2. set the text properties
        m_dynamicSymbolProperties.set_DynamicGlyph(esriDynamicSymbolType.esriDSymbolText, m_textGlyph);
        m_dynamicSymbolProperties.set_RotationAlignment(esriDynamicSymbolType.esriDSymbolMarker, esriDynamicSymbolRotationAlignment.esriDSRAScreen);
        m_dynamicSymbolProperties.set_Heading(esriDynamicSymbolType.esriDSymbolText, 0.0f);
        m_dynamicSymbolProperties.SetColor(esriDynamicSymbolType.esriDSymbolText, 0.0f, 0.85f, 0.0f, 1.0f);
        m_dynamicSymbolProperties.SetScale(esriDynamicSymbolType.esriDSymbolText, 1.0f, 1.0f);
        m_dynamicSymbolProperties.set_Smooth(esriDynamicSymbolType.esriDSymbolText, false);
        m_dynamicSymbolProperties.TextBoxUseDynamicFillSymbol = false;
        m_dynamicSymbolProperties.TextBoxHorizontalAlignment = esriTextHorizontalAlignment.esriTHACenter;
        m_dynamicSymbolProperties.TextRightToLeft = false;

        // draw both the icon and the text as a compound marker
        m_dynamicCompoundMarker.DrawCompoundMarker2(m_point, temperature, citiName);

        if (selected) // draw the selected symbol
        {
          m_dynamicSymbolProperties.SetColor(esriDynamicSymbolType.esriDSymbolMarker, 0.0f, 1.0f, 1.0f, 1.0f);
          m_dynamicSymbolProperties.set_DynamicGlyph(esriDynamicSymbolType.esriDSymbolMarker, m_selectionGlyph);
          DynamicDisplay.DrawMarker(m_point);
        }
      }

			base.m_bIsCompiledDirty = false;
    }
		
    /// <summary>
    /// The spatial reference of the underlying data.
    /// </summary>
    public override ISpatialReference SpatialReference
		{
			get
			{
				if(null == m_spatialRef)
				{
					m_spatialRef = CreateGeographicSpatialReference();
				}
				return m_spatialRef;
			}
		}

    /// <summary>
    /// The ID of the object. 
    /// </summary>
		public override ESRI.ArcGIS.esriSystem.UID ID
		{
			get
			{
				UID uid = new UIDClass();
				uid.Value = "RSSWeatherLayer.RSSWeatherLayerClass";

				return uid;
			}
		}
		
    /// <summary>
    /// The default area of interest for the layer. Returns the spatial-referenced extent of the layer. 
    /// </summary>
    public override IEnvelope AreaOfInterest
		{
			get
			{
				return this.Extent;
			}
		}

		/// <summary>
    /// The layer's extent which is a union of the extents of all the items of the layer 
		/// </summary>
    /// <remarks>In case where the DataFram's spatial reference is different than the underlying
    /// data's spatial reference the envelope must be projected</remarks>
    public override IEnvelope Extent
		{
			get
			{
				m_extent = GetLayerExtent();
				if(null == m_extent)
					return null;

				IEnvelope env = ((IClone)m_extent).Clone() as IEnvelope;
				if (null != m_mapSpatialRef && m_mapSpatialRef.FactoryCode != m_layerSRFactoryCode)
					env.Project(m_mapSpatialRef);
				
				return env;
			}
		}

    /// <summary>
    /// Map tip text at the specified mouse location.
    /// </summary>
    /// <param name="X"></param>
    /// <param name="Y"></param>
    /// <param name="Tolerance"></param>
    /// <returns></returns>
    public override string get_TipText(double X, double Y, double Tolerance)
    {
      IEnvelope envelope = new EnvelopeClass();
      envelope.PutCoords(X - Tolerance, Y - Tolerance,X + Tolerance, Y + Tolerance);
      
      //reproject the envelope to the datasource doordinate system
			if (null != m_mapSpatialRef && m_mapSpatialRef.FactoryCode != m_layerSRFactoryCode)
      {
        envelope.SpatialReference = m_spatialRef;
        envelope.Project(m_mapSpatialRef);
      }

      double xmin, ymin, xmax, ymax;
      envelope.QueryCoords(out xmin, out ymin, out xmax, out ymax);  
      
      //select all the records within the given extent
      string qry = "LON >= " + xmin.ToString() + " AND LON <= " + xmax.ToString() + " AND Lat >= " + ymin.ToString() + " AND LAT <= " + ymax.ToString();
      DataRow[] rows = m_table.Select(qry);
      if(0 == rows.Length)
        return string.Empty;

      DataRow r = rows[0];
      string zipCode = Convert.ToString(r[1]);
      string cityName = Convert.ToString(r[2]);
      string temperature = Convert.ToString(r[5]);

      return cityName + ", " + zipCode + ", " + temperature + "F";
    }

		#endregion

		#region public methods

    /// <summary>
    /// connects to RSS weather service
    /// </summary>
		public void Connect()
		{
      //enable the update timer
			m_timer.Enabled = true;

			base.m_bIsCompiledDirty = true;
		}

    /// <summary>
    /// disconnects from RSS weather service
    /// </summary>
		public void Disconnect()
		{
      //disable the update timer
      m_timer.Enabled = false;

			try
			{
        //abort the update thread in case that it is alive
				if(m_updateThread.IsAlive)
					m_updateThread.Abort();
			}
			catch
			{
				System.Diagnostics.Trace.WriteLine("WeatherLayer update thread has been terminated");	
			}
		}

    /// <summary>
    /// select a weather item by its zipCode
    /// </summary>
    /// <param name="zipCode"></param>
    /// <param name="newSelection"></param>
		public void Select(long zipCode, bool newSelection)
		{
			if(null == m_table)
				return;

			if(newSelection)
			{
				UnselectAll();
			}
			
			DataRow[] rows = m_table.Select("ZIPCODE = " + zipCode.ToString());
			if(rows.Length == 0)
				return;

			DataRow rec = rows[0];
			lock(m_table)
			{
				//13 is the selection column ID
				rec[13] = true;
				rec.AcceptChanges();
			}
			base.m_bIsCompiledDirty = true;
		}

    /// <summary>
    /// unselect all weather items
    /// </summary>
		public void UnselectAll()
		{
			if(null == m_table)
				return;

			//unselect all the currently selected items
			lock(m_table)
			{
				foreach(DataRow r in m_table.Rows)
				{
					//13 is the selection column ID
					r[13] = false;
				}
				m_table.AcceptChanges();
			}

			base.m_bIsCompiledDirty = true;
		}

    /// <summary>
    /// Run the update thread
    /// </summary>
    /// <remarks>calling this method to frequently might end up in blockage of RSS service.
    /// The service will interpret the excessive calls as an offence and thus would block the service for a while.</remarks>
		public void Refresh()
		{
			try
			{
				m_updateThread = new Thread(new ThreadStart(ThreadProc));

				//run the update thread
				m_updateThread.Start();
			}
			catch(Exception ex)
			{
				System.Diagnostics.Trace.WriteLine(ex.Message);
			}
		}

    /// <summary>
    /// add a new item given only a zipcode (will use the default location given by the service)
    /// should the item exists, it will get updated
    /// </summary>
    /// <param name="zipCode"></param>
    /// <returns></returns>
		public bool AddItem(long zipCode)
		{
			return AddItem(zipCode, 0.0 ,0.0);
		}

    /// <summary>
    /// adds a new item given a zipcode and a coordinate.
    /// Should the item already exists, it will get updated and will move to the new coordinate.
    /// </summary>
    /// <param name="zipCode"></param>
    /// <param name="lat"></param>
    /// <param name="lon"></param>
    /// <returns></returns>
		public bool AddItem(long zipCode, double lat, double lon)
		{
			if(null == m_table)
				return false;

			DataRow r = m_table.Rows.Find(zipCode);
			if(null != r) //if the record with this zipCode already exists
			{
				//in case that the record exists and the input coordinates are not valid 
				if(lat == 0.0 && lon == 0.0)
					return false;
				else //update the location according to the new coordinate
				{
					lock(m_table)
					{
						r[3] = lat;
						r[4] = lon;

						r.AcceptChanges();
					}
				}
			}
			else
			{
				//add new zip code to the locations list
				DataRow rec = m_locations.NewRow();
				lock(m_locations)
				{
					rec[1] = zipCode;
					m_locations.Rows.Add(rec);
				}

				//need to connect to the service and get the info
				AddWeatherItem(zipCode, lat, lon);
			}

			return true;
		}

    /// <summary>
    /// delete an item from the dataset
    /// </summary>
    /// <param name="zipCode"></param>
    /// <returns></returns>
		public bool DeleteItem(long zipCode)
		{
			if(null == m_table)
				return false;

			try
			{
				DataRow r = m_table.Rows.Find(zipCode);
				if(null != r) //if the record with this zipCode already exists
				{
					lock(m_table)
					{
						r.Delete();
					}
					base.m_bIsCompiledDirty = true;
					return true;
				}
				base.m_bIsCompiledDirty = true;
				return false;
			}
			catch(Exception ex)
			{
				System.Diagnostics.Trace.WriteLine(ex.Message);
				return false;
			}
		}

    /// <summary>
    /// get a weather item given a city name.
    /// </summary>
    /// <param name="cityName"></param>
    /// <returns></returns>
    /// <remarks>a city might have more than one zipCode and therefore this method will
    /// return the first zipcOde found for the specified city name.</remarks>
		public IPropertySet GetWeatherItem(string cityName)
		{
			if(null == m_table)
				return null;

			DataRow[] rows = m_table.Select("CITYNAME = '" + cityName + "'");
			if(rows.Length == 0)
				return null;

			long zipCode = Convert.ToInt64(rows[0][1]);
			return GetWeatherItem(zipCode);
		}

    /// <summary>
    /// This method searches for the record of the given zipcode and retunes the information as a PropertySet.
    /// </summary>
    /// <param name="zipCode"></param>
    /// <returns>a PropertySet encapsulating the weather information for the given weather item.</returns>
		public IPropertySet GetWeatherItem(long zipCode)
		{
			DataRow r = m_table.Rows.Find(zipCode);
			if(null == r)
				return null;

			IPropertySet propSet = new PropertySetClass();
			propSet.SetProperty(	"ID",						r[0]);
			propSet.SetProperty(	"ZIPCODE",			r[1]);
			propSet.SetProperty(	"CITYNAME",			r[2]);
			propSet.SetProperty(	"LAT",					r[3]);
			propSet.SetProperty(	"LON",					r[4]);
			propSet.SetProperty(	"TEMPERATURE",	r[5]);
			propSet.SetProperty(	"CONDITION",		r[6]);
			propSet.SetProperty(	"ICONNAME",			r[7]);
			propSet.SetProperty(	"ICONID",				r[8]);
			propSet.SetProperty(	"DAY",					r[9]);
			propSet.SetProperty(	"DATE",					r[10]);
			propSet.SetProperty(	"LOW",					r[11]);
			propSet.SetProperty(	"HIGH",					r[12]);
			propSet.SetProperty(	"UPDATEDATE",		r[14]);

			return propSet;
		}

    /// <summary>
    /// get a list of all citynames currently in the dataset.
    /// </summary>
    /// <returns></returns>
    /// <remarks>Please note that since the unique ID is zipCode, it is possible
    /// to have a city name appearing more than once.</remarks>
		public string[] GetCityNames()
		{
			if(null == m_table || 0 == m_table.Rows.Count)
				return null;

			string[] cityNames = new string[m_table.Rows.Count];
			for(int i=0; i<m_table.Rows.Count; i++)
			{
				//column #2 stores the cityName
				cityNames[i] = Convert.ToString(m_table.Rows[i][2]);
			}

			return cityNames;
		}

    /// <summary>
    /// Zoom to a weather item according to its city name
    /// </summary>
    /// <param name="cityName"></param>
    public void ZoomTo(string cityName)
    {
      if(null == m_table)
        return;

      DataRow[] rows = m_table.Select("CITYNAME = '" + cityName + "'");
      if(rows.Length == 0)
        return;

      long zipCode = Convert.ToInt64(rows[0][1]);
      ZoomTo(zipCode);
    }

    /// <summary>
    /// Zoom to weather item according to its zipcode
    /// </summary>
    /// <param name="zipCode"></param>
    public void ZoomTo(long zipCode)
    {
      if(null == m_table || null == m_symbolTable )
        return;

      if(null == m_display)
        return;

      //get the record for the requested zipCode
      DataRow r = m_table.Rows.Find(zipCode);
      if(null == r)
        return;

      //get the coordinate of the zipCode
      double lat = Convert.ToDouble(r[3]);
      double lon = Convert.ToDouble(r[4]);

      IPoint point = new PointClass();
      point.X = lon;
      point.Y = lat;
      point.SpatialReference = m_spatialRef;

			if (null != m_mapSpatialRef && m_mapSpatialRef.FactoryCode != m_layerSRFactoryCode)
        point.Project(m_mapSpatialRef);

      int iconCode = Convert.ToInt32(r[8]);
      //find the appropriate symbol record
      DataRow rec = m_symbolTable.Rows.Find(iconCode);
      if(rec == null)
        return;

      //get the icon's dimensions
      int iconWidth = Convert.ToInt32(rec[3]);
      int iconHeight = Convert.ToInt32(rec[4]);

      IDisplayTransformation displayTransformation = ((IScreenDisplay)m_display).DisplayTransformation;

      //Convert the icon coordinate into screen coordinate
      int windowX, windowY;
      displayTransformation.FromMapPoint(point,out windowX, out windowY);
      
      //get the upper left coord
      int ulx, uly;
      ulx = windowX - iconWidth/2;
      uly = windowY - iconHeight/2;
      IPoint ulPnt = displayTransformation.ToMapPoint(ulx, uly);

      //get the lower right coord
      int lrx,lry;
      lrx = windowX + iconWidth/2;
      lry = windowY + iconHeight/2;
      IPoint lrPnt = displayTransformation.ToMapPoint(lrx, lry);
      
      //construct the new extent
      IEnvelope envelope = new EnvelopeClass();
      envelope.PutCoords(ulPnt.X, lrPnt.Y, lrPnt.X, ulPnt.Y);
      envelope.Expand(2,2,false);
      
      //set the new extent and refresh the display
      displayTransformation.VisibleBounds = envelope;

			base.m_bIsCompiledDirty = true;

      ((IScreenDisplay)m_display).Invalidate(null, true, (short)esriScreenCache.esriAllScreenCaches);
      ((IScreenDisplay)m_display).UpdateWindow();
    }

    private void SetSymbolSize(int newSize)
    {
      if (newSize <= 0)
      {
        MessageBox.Show("Size is not allowed.");
        return;
      }

      m_symbolSize = newSize;

      if (null == m_symbolTable || 0 == m_symbolTable.Rows.Count)
        return;

      IPictureMarkerSymbol pictureMarkerSymbol = null;

      lock (m_symbolTable)
      {
        foreach (DataRow r in m_symbolTable.Rows)
        {
          pictureMarkerSymbol = r[2] as IPictureMarkerSymbol;
          if (null == pictureMarkerSymbol)
            continue;

          pictureMarkerSymbol.Size = newSize;
          r[2] = pictureMarkerSymbol;
          r.AcceptChanges();
        }
      }

			base.m_bIsCompiledDirty = true;

      ((IScreenDisplay)m_display).Invalidate(null, true, (short)esriScreenCache.esriAllScreenCaches);
      ((IScreenDisplay)m_display).UpdateWindow();
    }

    public int SymbolSize
    {
      set { SetSymbolSize(value); }
      get { return m_symbolSize; }
    }

		#endregion

		#region private utility methods

    /// <summary>
    /// create a WGS1984 geographic coordinate system.
    /// In this case, the underlying data provided by the service is in WGS1984.
    /// </summary>
    /// <returns></returns>
		private ISpatialReference CreateGeographicSpatialReference()
		{
			ISpatialReferenceFactory spatialRefFatcory = new SpatialReferenceEnvironmentClass();
			IGeographicCoordinateSystem geoCoordSys;
			geoCoordSys = spatialRefFatcory.CreateGeographicCoordinateSystem((int)esriSRGeoCSType.esriSRGeoCS_WGS1984);
			geoCoordSys.SetFalseOriginAndUnits(-180.0, -180.0, 5000000.0);
			geoCoordSys.SetZFalseOriginAndUnits(0.0, 100000.0);
			geoCoordSys.SetMFalseOriginAndUnits(0.0, 100000.0);

			return geoCoordSys as ISpatialReference;
		}

    /// <summary>
    /// get the overall extent of the items in the layer
    /// </summary>
    /// <returns></returns>
		private IEnvelope GetLayerExtent()
		{
      //iterate through all the items in the layers DB and get the bounding envelope
			IEnvelope env = new EnvelopeClass();
			env.SpatialReference = m_spatialRef;
			IPoint point = new PointClass();
			point.SpatialReference = m_spatialRef;
			int symbolCode = 0;
			double symbolSize = 0;
			foreach(DataRow r in m_table.Rows)
			{
				if (r[3] is DBNull || r[4] is DBNull)
					continue;

				point.Y = Convert.ToDouble(r[3]);
				point.X = Convert.ToDouble(r[4]);

				// need to get the symbol size in meters in order to add it to the total layer extent
				if (m_display != null)
				{
					symbolCode = Convert.ToInt32(r[8]);
					symbolSize = Math.Max(GetSymbolSize(m_display, symbolCode), symbolSize);
				}


				env.Union(point.Envelope);
			}

      // Expand the envelope in order to include the size of the symbol
			env.Expand(symbolSize, symbolSize, false);

      //return the layer's extent in the data underlying coordinate system
			return env;
		}

    /// <summary>
    /// initialize the main table used by the layer as well as the symbols table.
    /// The base class calles new on the table and adds a default ID field.
    /// </summary>
		private void InitializeTables()
		{
			string path =  System.IO.Path.Combine(m_dataFolder, "Weather.xml");
      //In case that there is no existing cache on the local machine, create the table.
      if(!System.IO.File.Exists(path))
			{

				//create the table the table	in addition to the default 'ID' and 'Geometry'	
        m_table = new DataTable("RECORDS");

        m_table.Columns.Add(  "ID",         typeof(long));     //0
        m_table.Columns.Add(	"ZIPCODE",		typeof(long));     //1
				m_table.Columns.Add(	"CITYNAME",		typeof(string));   //2
				m_table.Columns.Add(	"LAT",				typeof(double));   //3
				m_table.Columns.Add(	"LON",				typeof(double));	 //4
				m_table.Columns.Add(	"TEMP",				typeof(int));			 //5	
				m_table.Columns.Add(	"CONDITION",	typeof(string));	 //6
				m_table.Columns.Add(	"ICONNAME",		typeof(string));	 //7	
				m_table.Columns.Add(	"ICONID",	    typeof(int));			 //8 
				m_table.Columns.Add(	"DAY",				typeof(string));	 //9	
				m_table.Columns.Add(	"DATE",				typeof(string));	 //10
				m_table.Columns.Add(	"LOW",				typeof(string));	 //11
				m_table.Columns.Add(	"HIGH",				typeof(string));	 //12
				m_table.Columns.Add(	"SELECTED",		typeof(bool));		 //13
				m_table.Columns.Add(	"UPDATEDATE",	typeof(DateTime)); //14	
				
	
				//set the ID column to be auto increment
				m_table.Columns[0].AutoIncrement = true;
				m_table.Columns[0].ReadOnly = true;
				
				//the zipCode column must be the unique and nut allow null
				m_table.Columns[1].Unique = true;
				
				// set the ZIPCODE primary key for the table
				m_table.PrimaryKey = new DataColumn[] {m_table.Columns["ZIPCODE"]};

			}
      else //in case that the local cache exists, simply load the tables from the cache.
			{
				DataSet ds = new DataSet();
				ds.ReadXml(path);
					
				m_table = ds.Tables["RECORDS"];

        if (null == m_table)
          throw new Exception("Cannot find 'RECORDS' table");

        if (15 != m_table.Columns.Count)
          throw new Exception("Table 'RECORDS' does not have all required columns");

        m_table.Columns[0].ReadOnly = true;

        // set the ZIPCODE primary key for the table
				m_table.PrimaryKey = new DataColumn[] {m_table.Columns["ZIPCODE"]};
				
				//synchronize the locations table
				foreach(DataRow r in m_table.Rows)
				{
					try
					{
            //in case that the locations table does not exists, create and initialize it
						if(null == m_locations)
							InitializeLocations();

            //get the zipcode for the record
						string zip = Convert.ToString(r[1]);

            //make sure that there is no existing record with that zipCode already in the 
            //locations table.
            DataRow[] rows = m_locations.Select("ZIPCODE = " + zip);
						if(0 == rows.Length)
						{
							DataRow rec = m_locations.NewRow();
							rec[1] = Convert.ToInt64(r[1]);  //zip code 
							rec[2] = Convert.ToString(r[2]); //city name

              //add the new record to the locations table
              lock(m_locations)
							{
								m_locations.Rows.Add(rec);
							}
						}
					}
					catch(Exception ex)
					{
						System.Diagnostics.Trace.WriteLine(ex.Message);
					}
				}

        //displose the DS
				ds.Tables.Remove(m_table);
				ds.Dispose();
				GC.Collect();
			}

			//initialize the symbol map table
			m_symbolTable = new DataTable("Symbology");

			//add the columns to the table
			m_symbolTable.Columns.Add( "ID",						typeof(int));			      //0
			m_symbolTable.Columns.Add( "ICONID",		  	typeof(int));			      //1
			m_symbolTable.Columns.Add( "SYMBOL",		  	typeof(ISymbol));	      //2
			m_symbolTable.Columns.Add( "SYMBOLWIDTH",		typeof(int));			      //3
			m_symbolTable.Columns.Add( "SYMBOLHEIGHT",	typeof(int));			      //4
			m_symbolTable.Columns.Add( "DYNAMICGLYPH",  typeof(IDynamicGlyph)); //5
			m_symbolTable.Columns.Add( "BITMAP",        typeof(Bitmap));        //6
	
			//set the ID column to be auto increment
			m_symbolTable.Columns[0].AutoIncrement = true;
			m_symbolTable.Columns[0].ReadOnly = true;

			m_symbolTable.Columns[1].AllowDBNull = false;
			m_symbolTable.Columns[1].Unique = true;

			//set ICONID as the primary key for the table
			m_symbolTable.PrimaryKey = new DataColumn[] {m_symbolTable.Columns["ICONID"]};
		}

    /// <summary>
    /// Initialize the location table. Gets the location from a featureclass
    /// </summary>
		private void InitializeLocations()
		{
      //create a new instance of the location table
      m_locations = new DataTable();

      //add fields to the table
			m_locations.Columns.Add( "ID",				typeof(int));
			m_locations.Columns.Add( "ZIPCODE",		typeof(long));
			m_locations.Columns.Add( "CITYNAME",	typeof(string));

			m_locations.Columns[0].AutoIncrement = true;
			m_locations.Columns[0].ReadOnly = true;

			//set ZIPCODE as the primary key for the table
			m_locations.PrimaryKey = new DataColumn[] {m_locations.Columns["ZIPCODE"]};

      //spawn a thread to populate the locations table
      Thread t = new Thread(new ThreadStart(PopulateLocationsTableProc));
      t.Start();

      System.Threading.Thread.Sleep(1000);
		}

    /// <summary>
    /// Load the information from the MajorCities featureclass to the locations table
    /// </summary>
    private void PopulateLocationsTableProc()
    {
      //get the ArcGIS path from the registry
      string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            if (!System.IO.File.Exists(System.IO.Path.Combine(path, @"ArcGIS\data\USZipCodeData\ZipCode_Boundaries_US_Major_Cities.shp")))
      {
        MessageBox.Show("Cannot find file ZipCode_Boundaries_US_Major_Cities.shp!");
        return;
      }

      //open the featureclass
      IWorkspaceFactory wf = new ShapefileWorkspaceFactoryClass() as IWorkspaceFactory;
      IWorkspace ws = wf.OpenFromFile(System.IO.Path.Combine(path, @"ArcGIS\data\USZipCodeData"), 0);
      IFeatureWorkspace fw = ws as IFeatureWorkspace;
      IFeatureClass featureClass = fw.OpenFeatureClass("ZipCode_Boundaries_US_Major_Cities");
      //map the name and zip fields
      int zipIndex = featureClass.FindField("ZIP");
      int nameIndex = featureClass.FindField("NAME");
      string cityName;
      long zip;	
			
      try
      {
        //iterate through the features and add the information to the table
        IFeatureCursor fCursor = null;
        fCursor = featureClass.Search(null, true);
        IFeature feature = fCursor.NextFeature();
        int index = 0;

        while(null != feature)
        {
          object obj = feature.get_Value(nameIndex);
          if (obj == null)
            continue;
          cityName = Convert.ToString(obj);

          obj = feature.get_Value(zipIndex);
          if (obj == null)
            continue;
          zip = long.Parse(Convert.ToString(obj));
          if(zip <= 0)
            continue;
					
          //add the current location to the location table
          DataRow r = m_locations.Rows.Find(zip);
          if(null == r)
          {
            r = m_locations.NewRow();
            r[1] = zip;
            r[2] = cityName;
            lock(m_locations)
            {
              m_locations.Rows.Add(r);
            }
          }
				
          feature = fCursor.NextFeature();

          index++;
        }

        //release the feature cursor
        Marshal.ReleaseComObject(fCursor);
      }
      catch(Exception ex)
      {
        System.Diagnostics.Trace.WriteLine(ex.Message);
      }
    }
		
		/// <summary>
		/// Initialize the symbol that would use to highlight selected items
		/// </summary>
    private void InitializeSelectionSymbol()
		{
			//use a character marker symbol:
			ICharacterMarkerSymbol chMrkSym;
			chMrkSym = new CharacterMarkerSymbolClass();
			
      //Set the selection color (yellow)
			IRgbColor color;
			color = new RgbColorClass();
			color.Red = 0;
			color.Green = 255;
			color.Blue = 255;

      //set the font
			stdole.IFont aFont;
			aFont = new stdole.StdFontClass();
			aFont.Name = "ESRI Default Marker";
      aFont.Size = m_symbolSize;
			aFont.Bold = true;			

      //char #41 is just a rectangle
			chMrkSym.CharacterIndex = 41;
			chMrkSym.Color = color as IColor;
			chMrkSym.Font = aFont as stdole.IFontDisp;
      chMrkSym.Size = m_symbolSize;
			
			m_selectionSymbol = chMrkSym as ISymbol;
		}
    /// <summary>
    /// run the thread that does the update of the weather data
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
		private void OnUpdateTimer(object sender, ElapsedEventArgs e)
		{
			m_timer.Interval = 2700000; //(45 minutes)
			m_updateThread = new Thread(new ThreadStart(ThreadProc));

			//run the update thread
			m_updateThread.Start();
		}

    /// <summary>
    /// the main update thread for the layer.
    /// </summary>
    /// <remarks>Since the layer gets the weather information from a web service which might
    /// take a while to respond, it is not logical to let the application hang while waiting
    /// for response. Therefore, running the request on a different thread frees the application to 
    /// continue working while waiting for a response. 
    /// Please note that in this case, synchronization of shared resources must be addressed,
    /// otherwise you might end up getting unexpected results.</remarks>
		private void ThreadProc()
		{
			try
			{	

				long lZipCode;
        //iterate through all the records in the main table and update it against 
        //the information from the website.
				foreach(DataRow r in m_locations.Rows)
				{
          //put the thread to sleep in order not to overwhelm yahoo web site might
					System.Threading.Thread.Sleep(200);

					//get the zip code of the record (column #1)
					lZipCode = Convert.ToInt32(r[1]);

          //make the request and update the item
					AddWeatherItem(lZipCode, 0.0, 0.0);
				}

        //serialize the tables onto the local machine
				DataSet ds = new DataSet();
				ds.Tables.Add(m_table);
				ds.WriteXml(System.IO.Path.Combine(m_dataFolder, "Weather.xml"));
				ds.Tables.Remove(m_table);
				ds.Dispose();
				GC.Collect();

				base.m_bIsCompiledDirty = true;

				//fire an event to notify update of the weatheritems 
				if(OnWeatherItemsUpdated != null)
					OnWeatherItemsUpdated(this, new EventArgs());
			}
			catch(Exception ex)
			{
				System.Diagnostics.Trace.WriteLine(ex.Message);
			}
		}

		/// <summary>
		/// given a bitmap url, saves it on the local machine and returns its size
		/// </summary>
		/// <param name="iconPath"></param>
		/// <param name="width"></param>
		/// <param name="height"></param>
		private Bitmap DownloadIcon(string iconPath, out int width, out int height)
		{
			//if the icon does not exist on the local machine, get it from RSS site
			string iconFileName = System.IO.Path.Combine(m_iconFolder, System.IO.Path.GetFileNameWithoutExtension(iconPath) + ".bmp");
			width = 0;
			height = 0;
			Bitmap bitmap = null;
			if(!File.Exists(iconFileName))
			{
				using (System.Net.WebClient webClient = new System.Net.WebClient())
				{
					//open a readable stream to download the bitmap
          using (System.IO.Stream stream = webClient.OpenRead(iconPath))
					{
						bitmap = new Bitmap(stream, true);

						//save the image as a bitmap in the icons folder
						bitmap.Save(iconFileName, ImageFormat.Bmp);

            //get the bitmap's dimensions
						width = bitmap.Width;
						height = bitmap.Height;
					}
				}
			}
			else
			{
				//get the bitmap's dimensions
				{
					bitmap = new Bitmap(iconFileName);
					width = bitmap.Width;
					height = bitmap.Height;
				}
			}

			return bitmap;
		}

    /// <summary>
    /// get the specified symbol from the symbols table.
    /// </summary>
    /// <param name="iconCode"></param>
    /// <param name="dbr"></param>
    /// <returns></returns>
		private ISymbol GetSymbol(int iconCode, DataRow dbr)
		{
			ISymbol symbol = null;
			string iconPath;
			int iconWidth, iconHeight;
			Bitmap bitmap = null;

      //search for an existing symbol in the table
			DataRow r = m_symbolTable.Rows.Find(iconCode);
			if(r == null) //in case that the symbol does not exist in the table, create a new entry
			{
				r = m_symbolTable.NewRow();
				r[1] = iconCode;
				
				iconPath = Convert.ToString(dbr[7]);
        //Initialize the picture marker symbol
				symbol = InitializeSymbol(iconPath, out iconWidth, out iconHeight, out bitmap);
				if(null == symbol)
					return null;

        //update the symbol table
				lock (m_symbolTable)
				{
					r[2] = symbol;
					r[3] = iconWidth;
					r[4] = iconHeight;
					r[6] = bitmap;
					m_symbolTable.Rows.Add(r);
				}
			}
			else
			{
        if (r[2] is DBNull) //in case that the record exists but the symbol hasn't been initialized
				{
					iconPath = Convert.ToString(dbr[7]);
          //Initialize the picture marker symbol
					symbol = InitializeSymbol(iconPath, out iconWidth, out iconHeight, out bitmap);
					if(null == symbol)
						return null;

          //update the symbol table
					lock(m_symbolTable)
					{
						r[2] = symbol;
						r[6] = bitmap;
						r.AcceptChanges();
					}
				}
				else //the record exists in the table and the symbol has been initialized
          //get the symbol
					symbol = r[2] as ISymbol;
			}
			
      //return the requested symbol
      return symbol;
		}

		private IDynamicGlyph GetDynamicGlyph(IDynamicGlyphFactory2 dynamicGlyphFactory, int iconCode, DataRow dbr, out int originalIconSize)
		{
      originalIconSize = 0;

      if (dynamicGlyphFactory == null)
				return null;
			
			string iconPath;
			int iconWidth, iconHeight;
			Bitmap bitmap = null;
			IDynamicGlyph dynamicGlyph = null;

			//search for an existing symbol in the table
			DataRow r = m_symbolTable.Rows.Find(iconCode);
			if (r == null)
			{
				iconPath = Convert.ToString(dbr[7]);
				bitmap = DownloadIcon(iconPath, out iconWidth, out iconHeight);
				if (bitmap != null)
				{
          originalIconSize = iconWidth;

          dynamicGlyph = dynamicGlyphFactory.CreateDynamicGlyphFromBitmap(esriDynamicGlyphType.esriDGlyphMarker,
																						 bitmap.GetHbitmap().ToInt32(),
																						 false,
																						 (IColor)ESRI.ArcGIS.ADF.Connection.Local.Converter.ToRGBColor(Color.FromArgb(255, 255, 255))
																						 );

					
          //update the symbol table
					r = m_symbolTable.NewRow();
					lock (m_symbolTable)
					{
						r[1] = iconCode;
						r[3] = iconWidth;
						r[4] = iconHeight;
						r[5] = dynamicGlyph;
						r[6] = bitmap;
						m_symbolTable.Rows.Add(r);
					}
				}
			}
			else
			{
				if (r[5] is DBNull)
				{
					if (r[6] is DBNull)
					{
						iconPath = Convert.ToString(dbr[7]);
						bitmap = DownloadIcon(iconPath, out iconWidth, out iconHeight);
						if (bitmap == null)
							return null;

            originalIconSize = iconWidth;

						lock (m_symbolTable)
						{
              r[3] = iconWidth;
              r[4] = iconHeight;
              r[6] = bitmap;
						}
					}
					else
					{
            originalIconSize = Convert.ToInt32(r[3]);
            bitmap = (Bitmap)r[6];
					}
					dynamicGlyph = dynamicGlyphFactory.CreateDynamicGlyphFromBitmap(esriDynamicGlyphType.esriDGlyphMarker,
																						 bitmap.GetHbitmap().ToInt32(),
																						 false,
																						 (IColor)ESRI.ArcGIS.ADF.Connection.Local.Converter.ToRGBColor(Color.FromArgb(255, 255, 255))
																						 );

					lock (m_symbolTable)
					{
						r[5] = dynamicGlyph;
					}
				}
				else
				{
          originalIconSize = Convert.ToInt32(r[3]);
					dynamicGlyph = (IDynamicGlyph)r[5];
				}
			}

			return dynamicGlyph;
		}


		/// <summary>
    /// Initialize a character marker symbol for a given bitmap path
		/// </summary>
		/// <param name="iconPath"></param>
		/// <param name="iconWidth"></param>
		/// <param name="iconHeight"></param>
		/// <returns></returns>
    private ISymbol InitializeSymbol(string iconPath, out int iconWidth, out int iconHeight, out Bitmap bitmap)
		{
			iconWidth = iconHeight = 0;
			bitmap = null;
			try
			{ 
				//make sure that the icon exit on dist or else download it
				bitmap = DownloadIcon(iconPath, out iconWidth, out iconHeight);
				string iconFileName = System.IO.Path.Combine(m_iconFolder, System.IO.Path.GetFileNameWithoutExtension(iconPath) + ".bmp");
				if(!System.IO.File.Exists(iconFileName))
					return null;
				
        //initialize the transparent color
				IRgbColor rgbColor = new RgbColorClass();
				rgbColor.Red = 255;
				rgbColor.Blue = 255;
				rgbColor.Green = 255;
				
				//instantiate the marker symbol and set its properties
        IPictureMarkerSymbol pictureMarkerSymbol = new PictureMarkerSymbolClass();
				pictureMarkerSymbol.CreateMarkerSymbolFromFile(ESRI.ArcGIS.Display.esriIPictureType.esriIPictureBitmap, iconFileName);
				pictureMarkerSymbol.Angle = 0;
        pictureMarkerSymbol.Size = m_symbolSize;
				pictureMarkerSymbol.XOffset = 0;
				pictureMarkerSymbol.YOffset = 0;
				pictureMarkerSymbol.BitmapTransparencyColor = rgbColor as IColor;

				//return the symbol
        return (ISymbol)pictureMarkerSymbol;
			}
			catch
			{
				return null;
			}
		}

    /// <summary>
    /// Makes a request against RSS Weather service and add update the layer's table
    /// </summary>
    /// <param name="zipCode"></param>
    /// <param name="Lat"></param>
    /// <param name="Lon"></param>
		private void AddWeatherItem(long zipCode, double Lat, double Lon)
		{
			try
			{	
				string cityName;
				double lat, lon;
				int temp;
				string condition;
				string desc;
				string iconPath;
				string day;
				string date;
				int low;
				int high;
				int iconCode;
				int iconWidth = 52; //default values
				int iconHeight = 52;
				Bitmap bitmap = null;

				DataRow dbr = m_table.Rows.Find(zipCode);
				if (dbr != null)
				{
					// get the date 
					DateTime updateDate = Convert.ToDateTime(dbr[14]);
					TimeSpan ts = DateTime.Now - updateDate;
					
					// if the item had been updated in the past 15 minutes, simply bail out.
					if (ts.TotalMinutes < 15)
						return;
				}

        //the base URL for the service
				string url = "http://xml.weather.yahoo.com/forecastrss?p=";
        //the RegEx used to extract the icon path from the HTML tag
				string regxQry = "(http://(\\\")?(.*?\\.gif))";
				XmlTextReader reader = null;
				XmlDocument doc;
				XmlNode node;
				
				try
				{
          //make the request and get the result back into XmlReader
					reader = new XmlTextReader(url + zipCode.ToString());
				}
				catch(Exception ex)
				{
					System.Diagnostics.Trace.WriteLine(ex.Message);
					return;
				}

        //load the XmlReader to an xml doc
				doc = new XmlDocument();
				doc.Load(reader);

        //set an XmlNamespaceManager since we have to make explicit namespace searches
        XmlNamespaceManager xmlnsManager = new System.Xml.XmlNamespaceManager(doc.NameTable);
				//Add the namespaces used in the xml doc to the XmlNamespaceManager.
				xmlnsManager.AddNamespace("yweather", "http://xml.weather.yahoo.com/ns/rss/1.0");
				xmlnsManager.AddNamespace("geo", "http://www.w3.org/2003/01/geo/wgs84_pos#");

        //make sure that the node exists
				node = doc.DocumentElement.SelectSingleNode("/rss/channel/yweather:location/@city", xmlnsManager);
				if(null == node)
					return;

        //get the cityname
				cityName = doc.DocumentElement.SelectSingleNode("/rss/channel/yweather:location/@city", xmlnsManager).InnerXml;
				if(Lat == 0.0 && Lon == 0.0)
				{
          //in case that the caller did not specify a coordinate, get the default coordinate from the service
					lat = Convert.ToDouble(doc.DocumentElement.SelectSingleNode("/rss/channel/item/geo:lat", xmlnsManager).InnerXml);
					lon = Convert.ToDouble(doc.DocumentElement.SelectSingleNode("/rss/channel/item/geo:long", xmlnsManager).InnerXml);
				}
				else
				{
					lat = Lat;
					lon = Lon;
				}

        //extract the rest of the information from the RSS response
				condition = doc.DocumentElement.SelectSingleNode("/rss/channel/item/yweather:condition/@text", xmlnsManager).InnerXml;
				iconCode = Convert.ToInt32(doc.DocumentElement.SelectSingleNode("/rss/channel/item/yweather:condition/@code", xmlnsManager).InnerXml);
				temp = Convert.ToInt32(doc.DocumentElement.SelectSingleNode("/rss/channel/item/yweather:condition/@temp", xmlnsManager).InnerXml);
				desc = doc.DocumentElement.SelectSingleNode("/rss/channel/item/description").InnerXml;
				day = doc.DocumentElement.SelectSingleNode("/rss/channel/item/yweather:forecast/@day", xmlnsManager).InnerXml;
				date = doc.DocumentElement.SelectSingleNode("/rss/channel/item/yweather:forecast/@date", xmlnsManager).InnerXml;
				low = Convert.ToInt32(doc.DocumentElement.SelectSingleNode("/rss/channel/item/yweather:forecast/@low", xmlnsManager).InnerXml);
				high = Convert.ToInt32(doc.DocumentElement.SelectSingleNode("/rss/channel/item/yweather:forecast/@high", xmlnsManager).InnerXml);


				//use regex in order to extract the icon name from the html script
				Match m = Regex.Match(desc,regxQry);
				if(m.Success)
				{
					iconPath = m.Value;

					//add the icon ID to the symbology table
					DataRow tr = m_symbolTable.Rows.Find(iconCode);
					if(null == tr)
					{
						//get the icon from the website
						bitmap = DownloadIcon(iconPath, out iconWidth, out iconHeight);

            //create a new record
						tr = m_symbolTable.NewRow();
						tr[1] = iconCode;
						tr[3] = iconWidth;
						tr[4] = iconHeight;
						tr[6] = bitmap;
            
            //update the symbol table. The initialization of the symbol cannot take place in here, since
            //this code gets executed on a background thread.
						lock(m_symbolTable)
						{
							m_symbolTable.Rows.Add(tr);
						}
					}
					else //get the icon's dimensions from the table
					{
						//get the icon's dimensions from the table
						iconWidth = Convert.ToInt32(tr[3]);
						iconHeight = Convert.ToInt32(tr[4]);
					}
				}
				else
				{
					iconPath = "";
				}

        //test whether the record already exists in the layer's table.
				if(null == dbr) //in case that the record does not exist
				{
          //create a new record
					dbr = m_table.NewRow();

          if (!m_table.Columns[0].AutoIncrement)
            dbr[0] = Convert.ToInt32(DateTime.Now.Millisecond);

					//add the item to the table
					lock (m_table)
					{
						dbr[1] = zipCode;
						dbr[2] = cityName;
						dbr[3] = lat;
						dbr[4] = lon;
						dbr[5] = temp;
						dbr[6] = condition;
						dbr[7] = iconPath;
						dbr[8] = iconCode;
						dbr[9] = day;
						dbr[10] = date;
						dbr[11] = low;
						dbr[12] = high;
						dbr[13] = false;
						dbr[14] = DateTime.Now;
					
						m_table.Rows.Add(dbr);
					}
				}
				else //in case that the record exists, just update it
				{
					//update the record
					lock (m_table)
					{
						dbr[5] = temp;
						dbr[6] = condition;
						dbr[7] = iconPath;
						dbr[8] = iconCode;
						dbr[9] = day;
						dbr[10] = date;
						dbr[11] = low;
						dbr[12] = high;
						dbr[14] = DateTime.Now;

						dbr.AcceptChanges();
					}
				}

				base.m_bIsCompiledDirty = true;

				//fire an event to notify the user that the item has been updated
        if (OnWeatherItemAdded != null)
        {
          WeatherItemEventArgs weatherItemEventArgs = new WeatherItemEventArgs(Convert.ToInt32(dbr[0]), zipCode, lon, lat, iconWidth, iconHeight);
          OnWeatherItemAdded(this, weatherItemEventArgs);
        }

			}
			catch(Exception ex)
			{
				System.Diagnostics.Trace.WriteLine("AddWeatherItem: " + ex.Message);
			}
		}
		#endregion

    #region IIdentify Members

    /// <summary>
    /// Identifying all the weather items falling within the given envelope
    /// </summary>
    /// <param name="pGeom"></param>
    /// <returns></returns>
    public IArray Identify(IGeometry pGeom)
    {
      IEnvelope intersectEnv = new EnvelopeClass();
      IEnvelope inEnv;
      IArray array = new ArrayClass();

      //get the envelope from the geometry 
      if(pGeom.GeometryType == esriGeometryType.esriGeometryEnvelope)
        inEnv = pGeom.Envelope;
      else
        inEnv = pGeom as IEnvelope;

      //reproject the envelope to the source coordsys
      //this would allow to search directly on the Lat/Lon columns
			if (null != m_spatialRef && m_mapSpatialRef.FactoryCode != m_layerSRFactoryCode && null != inEnv.SpatialReference)
        inEnv.Project(base.m_spatialRef);

      //expand the envelope so that it'll cover the symbol
      inEnv.Expand(4,4,true);

      double xmin, ymin, xmax, ymax;
      inEnv.QueryCoords(out xmin, out ymin, out xmax, out ymax);  
      
      //select all the records within the given extent
      string qry = "LON >= " + xmin.ToString() + " AND LON <= " + xmax.ToString() + " AND Lat >= " + ymin.ToString() + " AND LAT <= " + ymax.ToString();
      DataRow[] rows = m_table.Select(qry);
      if(0 == rows.Length)
        return array;
      
      long zipCode;
      IPropertySet			propSet		= null;
      IIdentifyObj			idObj			= null;
      IIdentifyObject		idObject	= null;
      bool	            bIdentify	= false;

      foreach(DataRow r in rows)
      {
        //get the zipCode
        zipCode = Convert.ToInt64(r["ZIPCODE"]); 
 
        //get the properties of the given item in order to pass it to the identify object
        propSet = this.GetWeatherItem(zipCode);
        if(null != propSet)
        {
          //instantiate the identify object and add it to the array
          idObj = new RSSWeatherIdentifyObject();
          //test whether the layer can be identified
          bIdentify = idObj.CanIdentify((ILayer)this);
          if(bIdentify)
          {
            idObject = idObj as IIdentifyObject;
            idObject.PropertySet = propSet;
            array.Add(idObj);
          }
        }
      }

      //return the array with the identify objects
      return array;
    }

		private double GetSymbolSize(IDisplay display, int symbolCode)
		{
			if (display == null)
				return 0;

			double symbolSize = 0;
      double symbolSizePixels = 0;
			DataRow r = m_symbolTable.Rows.Find(symbolCode);
			if (r != null)
			{
				symbolSizePixels = Convert.ToDouble(m_symbolSize);

				// convert the symbol size from pixels to map units
				ITransformation transform = display.DisplayTransformation as ITransformation;
				if (transform == null)
					return 0;

				double[] symbolDimensions = new double[2];
				symbolDimensions[0] = (double)symbolSizePixels;
				symbolDimensions[1] = (double)symbolSizePixels;

				double[] symbolDimensionsMap = new double[2];

				transform.TransformMeasuresFF(esriTransformDirection.esriTransformReverse, 1, ref symbolDimensionsMap[0], ref symbolDimensions[0]);
				symbolSize = symbolDimensionsMap[0];
			}

			return symbolSize;
		}

    #endregion
  }
}
