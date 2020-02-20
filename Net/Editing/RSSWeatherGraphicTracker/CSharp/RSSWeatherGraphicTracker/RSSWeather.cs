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
using System.Data;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Xml;
using System.IO;
using System.Threading;
using System.Timers;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.EngineCore;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesFile;

namespace RSSWeatherGraphicTracker
{
  #region WeatherItemEventArgs class members
  public sealed class WeatherItemEventArgs : EventArgs
  {
    private int m_iconId;
    private long m_zipCode;
    private double m_x;
    private double m_y;
    private int m_iconWidth;
    private int m_iconHeight;
    public WeatherItemEventArgs(int iconId, long zipCode, double X, double Y, int iconWidth, int iconHeight)
    {
      m_iconId = iconId;
      m_zipCode = zipCode;
      m_x = X;
      m_y = Y;
      m_iconWidth = iconWidth;
      m_iconHeight = iconHeight;
    }

    public int IconID
    {
      get { return m_iconId; }
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
  }
  #endregion

  //declare delegates for the event handling
  public delegate void WeatherItemAdded(object sender, WeatherItemEventArgs args);
  public delegate void WeatherItemsUpdated(object sender, EventArgs args);

  class RSSWeather
  {
    private sealed class InvokeHelper : Control
    {
      //delegate used to pass the invoked method to the main thread
      public delegate void RefreshWeatherItemHelper(WeatherItemEventArgs weatherItemInfo);

      private RSSWeather m_weather = null;

      public InvokeHelper(RSSWeather rssWeather)
      {
        m_weather = rssWeather;

        CreateHandle();
        CreateControl();         
      }

      public void RefreshWeatherItem(WeatherItemEventArgs weatherItemInfo)
      {
        try
        {
          // Invoke the RefreshInternal through its delegate
          if (!this.IsDisposed && this.IsHandleCreated)
            Invoke(new RefreshWeatherItemHelper(RefreshWeatherItemInvoked), new object[] { weatherItemInfo });
        }
        catch (Exception ex)
        {
          System.Diagnostics.Trace.WriteLine(ex.Message);
        }
      }

      private void RefreshWeatherItemInvoked(WeatherItemEventArgs weatherItemInfo)
      {
        if (m_weather != null)
          m_weather.UpdateTracker(weatherItemInfo);
      }
    }

    #region class members

    private System.Timers.Timer m_timer = null;
    private Thread m_updateThread = null;
    private string m_iconFolder = string.Empty;
    private DataTable m_weatherItemTable = null;
    private DataTable m_symbolTable = null;
    private DataTable m_locations = null;
    private string m_dataFolder = string.Empty;
    private string m_installationFolder = string.Empty;

    private IPoint m_point = null;
    private ISpatialReference m_SRWGS84 = null;
    private IBasicMap m_mapOrGlobe = null;
    private ISimpleTextSymbol m_textSymbol = null;
    private IGraphicTracker m_graphicTracker = null;
    private InvokeHelper m_invokeHelper = null;

    //weather items events
    public event WeatherItemAdded OnWeatherItemAdded;
    public event WeatherItemsUpdated OnWeatherItemsUpdated;

    #endregion


    public RSSWeather()
    {
      //get the directory for the cache. If it does not exist, create it.
      m_dataFolder = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "RSSWeather");
      if (!System.IO.Directory.Exists(m_dataFolder))
      {
        System.IO.Directory.CreateDirectory(m_dataFolder);
      }
      m_iconFolder = m_dataFolder;

      m_installationFolder = ESRI.ArcGIS.RuntimeManager.ActiveRuntime.Path;
    }

    public void Init(IBasicMap mapOrGlobe)
    {
      System.Diagnostics.Trace.WriteLine("Init - Thread ID: " + System.Threading.Thread.CurrentThread.ManagedThreadId.ToString());

      if (mapOrGlobe == null)
        return;

      m_mapOrGlobe = mapOrGlobe;

      try
      {
        //initialize the tables (main table as well as the symbols table)
        InitializeTables();

        //get the location list from a featureclass (US major cities) and synchronize it with the 
        //cached information in case it exists.
        if (null == m_locations)
          InitializeLocations();

        m_point = new PointClass();
        m_SRWGS84 = CreateGeographicSpatialReference();
        m_point.SpatialReference = m_SRWGS84;

        m_textSymbol = new TextSymbolClass() as ISimpleTextSymbol;
        m_textSymbol.Font = ToFontDisp(new Font("Tahoma", 10.0f, FontStyle.Bold));
        m_textSymbol.Size = 10.0;
        m_textSymbol.Color = (IColor)ToRGBColor(Color.FromArgb(0, 255, 0));
        m_textSymbol.XOffset = 0.0;
        m_textSymbol.YOffset = 16.0;
        
        
        m_graphicTracker = new GraphicTrackerClass();
        m_graphicTracker.Initialize(mapOrGlobe as object);

        if (m_weatherItemTable.Rows.Count > 0)
          PopulateGraphicTracker();

        m_invokeHelper = new InvokeHelper(this);

        this.OnWeatherItemAdded += new WeatherItemAdded(OnWeatherItemAddedEvent);

        //instantiate the timer for the weather update thread
        m_timer = new System.Timers.Timer(1000);
        m_timer.Elapsed += new ElapsedEventHandler(OnUpdateTimer);  
        //enable the update timer
        m_timer.Enabled = true;
      }
      catch (Exception ex)
      {
        System.Diagnostics.Trace.WriteLine(ex.Message);
      }
    }

    public void Remove()
    {
      this.OnWeatherItemAdded -= new WeatherItemAdded(OnWeatherItemAddedEvent);
      m_invokeHelper = null;
      m_timer.Enabled = false;
      // wait for the update thread to exit
      m_updateThread.Join();
      m_graphicTracker.RemoveAll();
      m_graphicTracker = null;
    }

    public void UpdateTracker(WeatherItemEventArgs weatherItemInfo)
    {

      System.Diagnostics.Trace.WriteLine("UpdateTracker - Thread ID: " + System.Threading.Thread.CurrentThread.ManagedThreadId.ToString());
      if (m_graphicTracker == null)
        throw new Exception("Graphic tracker is not initialized!");


      // 1. lock the m_weatherItemTable and get the record
      DataRow row;
      lock (m_weatherItemTable)
      {
        row = m_weatherItemTable.Rows.Find(weatherItemInfo.ZipCode);
        if (row == null)
          return;

        // 2. get the symbol for the item
        IGraphicTrackerSymbol symbol = GetSymbol(weatherItemInfo.IconID, Convert.ToString(row[7]));
        if (symbol == null)
          return;

        string label = string.Format("{0}, {1}?F", Convert.ToString(row[2]), Convert.ToString(row[5]));

        // 3. check whether it has a tracker ID (not equals -1)
        int trackerID = Convert.ToInt32(row[15]);
        //m_graphicTracker.SuspendUpdate = true;
        m_point.PutCoords(weatherItemInfo.mapX, weatherItemInfo.mapY);
        m_point.SpatialReference = m_SRWGS84;
        m_point.Project(m_mapOrGlobe.SpatialReference);
        if (trackerID == -1) // new tracker
        {
          trackerID = m_graphicTracker.Add(m_point as IGeometry, symbol);
          m_graphicTracker.SetTextSymbol(trackerID, m_textSymbol);
          
          row[15] = trackerID;
          
        }
        else // existing tracker
        {
          m_graphicTracker.MoveTo(trackerID, m_point.X, m_point.Y, 0);
          m_graphicTracker.SetSymbol(trackerID, symbol);
        }

        m_graphicTracker.SetLabel(trackerID, label);
        
        row.AcceptChanges();

        //m_graphicTracker.SuspendUpdate = false;
      }
    }


    #region private utility methods

    void PopulateGraphicTracker()
    {
      m_graphicTracker.SuspendUpdate = true;

      foreach (DataRow row in m_weatherItemTable.Rows)
      {
        IGraphicTrackerSymbol symbol = GetSymbol(Convert.ToInt32(row[8]), Convert.ToString(row[7]));
        if (symbol == null)
          continue;

        string label = string.Format("{0}, {1}?F", Convert.ToString(row[2]), Convert.ToString(row[5]));

        m_point.PutCoords(Convert.ToDouble(row[4]), Convert.ToDouble(row[3]));
        int trackerID = m_graphicTracker.Add(m_point as IGeometry, symbol);
        m_graphicTracker.SetTextSymbol(trackerID, m_textSymbol);
        m_graphicTracker.SetScaleMode(trackerID, esriGTScale.esriGTScaleAuto);
        m_graphicTracker.SetOrientationMode(trackerID, esriGTOrientation.esriGTOrientationAutomatic);
        m_graphicTracker.SetElevationMode(trackerID, esriGTElevation.esriGTElevationClampToGround);
        m_graphicTracker.SetLabel(trackerID, label);
        row[15] = trackerID;
        row.AcceptChanges();  
      }

      m_graphicTracker.SuspendUpdate = false;
    }

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
    /// the main update thread for the data.
    /// </summary>
    /// <remarks>Since the information is coming from a web service which might
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
        foreach (DataRow r in m_locations.Rows)
        {
          //put the thread to sleep in order not to overwhelm the RSS website
          //System.Threading.Thread.Sleep(200);

          //get the zip code of the record (column #1)
          lZipCode = Convert.ToInt32(r[1]);

          //make the request and update the item
          AddWeatherItem(lZipCode, 0.0, 0.0);
        }

        //serialize the tables onto the local machine
        DataSet ds = new DataSet();
        ds.Tables.Add(m_weatherItemTable);
        ds.WriteXml(System.IO.Path.Combine(m_dataFolder, "Weather.xml"));
        ds.Tables.Remove(m_weatherItemTable);
        ds.Dispose();
        GC.Collect();

        //fire an event to notify update of the weather items 
				if(OnWeatherItemsUpdated != null)
          OnWeatherItemsUpdated(this, new EventArgs());
      }
      catch (Exception ex)
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
      string iconFileName = System.IO.Path.Combine(m_iconFolder, System.IO.Path.GetFileNameWithoutExtension(iconPath) + ".png");
      width = 0;
      height = 0;
      Bitmap bitmap = null;
      if (!File.Exists(iconFileName))
      {
        using (System.Net.WebClient webClient = new System.Net.WebClient())
        {
          //open a readable stream to download the bitmap
          using (System.IO.Stream stream = webClient.OpenRead(iconPath))
          {
            bitmap = new Bitmap(stream, true);

            //save the image as a bitmap in the icons folder
            bitmap.Save(iconFileName, ImageFormat.Png);

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
    private IGraphicTrackerSymbol GetSymbol(int iconCode, string iconPath)
    {
      IGraphicTrackerSymbol symbol;
      int iconWidth, iconHeight;
      Bitmap bitmap = null;

      //search for an existing symbol in the table
      DataRow r = m_symbolTable.Rows.Find(iconCode);
      if (r == null) //in case that the symbol does not exist in the table, create a new entry
      {
        r = m_symbolTable.NewRow();
        r[1] = iconCode;

        //Initialize the picture marker symbol
        symbol = InitializeSymbol(iconPath, out iconWidth, out iconHeight, out bitmap);
        if (null == symbol)
          return null;

        //update the symbol table
        lock (m_symbolTable)
        {
          r[2] = symbol;
          r[3] = iconWidth;
          r[4] = iconHeight;
          r[5] = bitmap;
          m_symbolTable.Rows.Add(r);
        }
      }
      else
      {
        if (r[2] is DBNull) //in case that the record exists but the symbol hasn't been initialized
        {
          //Initialize the symbol
          symbol = InitializeSymbol(iconPath, out iconWidth, out iconHeight, out bitmap);
          if (null == symbol)
            return null;

          //update the symbol table
          lock (m_symbolTable)
          {
            r[2] = symbol;
            r[5] = bitmap;
            r.AcceptChanges();
          }
        }
        else //the record exists in the table and the symbol has been initialized
          //get the symbol
          symbol = r[2] as IGraphicTrackerSymbol;
      }

      //return the requested symbol
      return symbol;
    }

    /// <summary>
    /// Initialize a character marker symbol for a given bitmap path
    /// </summary>
    /// <param name="iconPath"></param>
    /// <param name="iconWidth"></param>
    /// <param name="iconHeight"></param>
    /// <returns></returns>
    private IGraphicTrackerSymbol InitializeSymbol(string iconPath, out int iconWidth, out int iconHeight, out Bitmap bitmap)
    {
      iconWidth = iconHeight = 0;
      bitmap = null;
      try
      {
        //make sure that the icon exit on dist or else download it
        DownloadIcon(iconPath, out iconWidth, out iconHeight);
        string iconFileName = System.IO.Path.Combine(m_iconFolder, System.IO.Path.GetFileNameWithoutExtension(iconPath) + ".png");
        if (!System.IO.File.Exists(iconFileName))
          return null;

        IGraphicTrackerSymbol symbol = m_graphicTracker.CreateSymbolFromPath(iconFileName, iconFileName);
        return symbol;

      }
      catch
      {
        return null;
      }
    }

    /// <summary>
    /// Makes a request against RSS Weather service and add update the table
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

        DataRow dbr = m_weatherItemTable.Rows.Find(zipCode);
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
        catch (Exception ex)
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
        if (null == node)
          return;

        //get the city name
        cityName = doc.DocumentElement.SelectSingleNode("/rss/channel/yweather:location/@city", xmlnsManager).InnerXml;
        if (Lat == 0.0 && Lon == 0.0)
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
        Match m = Regex.Match(desc, regxQry);
        if (m.Success)
        {
          iconPath = m.Value;

          //add the icon ID to the symbology table
          DataRow tr = m_symbolTable.Rows.Find(iconCode);
          if (null == tr)
          {
            //get the icon from the website
            bitmap = DownloadIcon(iconPath, out iconWidth, out iconHeight);

            //create a new record
            tr = m_symbolTable.NewRow();
            tr[1] = iconCode;
            tr[3] = iconWidth;
            tr[4] = iconHeight;
            tr[5] = bitmap;

            //update the symbol table. The initialization of the symbol cannot take place in here, since
            //this code gets executed on a background thread.
            lock (m_symbolTable)
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

        //test whether the record already exists in the table.
        if (null == dbr) //in case that the record does not exist
        {
          //create a new record
          dbr = m_weatherItemTable.NewRow();

          if (!m_weatherItemTable.Columns[0].AutoIncrement)
            dbr[0] = Convert.ToInt32(DateTime.Now.Millisecond);

          //add the item to the table
          lock (m_weatherItemTable)
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
            dbr[15] = -1;

            m_weatherItemTable.Rows.Add(dbr);
          }
        }
        else //in case that the record exists, just update it
        {
          //update the record
          lock (m_weatherItemTable)
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

        //fire an event to notify the user that the item has been updated
        if (OnWeatherItemAdded != null)
        {
          WeatherItemEventArgs weatherItemEventArgs = new WeatherItemEventArgs(iconCode, zipCode, lon, lat, iconWidth, iconHeight);
          OnWeatherItemAdded(this, weatherItemEventArgs);
        }
      }
      catch (Exception ex)
      {
        System.Diagnostics.Trace.WriteLine("AddWeatherItem: " + ex.Message);
      }
    }

    private IRgbColor ToRGBColor(System.Drawing.Color color)
    {
      IRgbColor rgbColor = new RgbColorClass();
      rgbColor.Red = color.R;
      rgbColor.Green = color.G;
      rgbColor.Blue = color.B;

      return rgbColor;
    }

    private stdole.IFontDisp ToFontDisp(System.Drawing.Font font)
    {
      stdole.IFont aFont;
      aFont = new stdole.StdFontClass();
      aFont.Name = font.Name;
      aFont.Size = (decimal)font.Size;
      aFont.Bold = font.Bold;
      aFont.Italic = font.Italic;
      aFont.Strikethrough = font.Strikeout;
      aFont.Underline = font.Underline;

      return aFont as stdole.IFontDisp;
    }

    /// <summary>
    /// initialize the main table as well as the symbols table.
    /// The base class calls new on the table and adds a default ID field.
    /// </summary>
    private void InitializeTables()
    {
      string path = System.IO.Path.Combine(m_dataFolder, "Weather.xml");
      //In case that there is no existing cache on the local machine, create the table.
      if (!System.IO.File.Exists(path))
      {

        //create the table the table	in addition to the default 'ID' and 'Geometry'	
        m_weatherItemTable = new DataTable("RECORDS");

        m_weatherItemTable.Columns.Add("ID", typeof(long));             //0
        m_weatherItemTable.Columns.Add("ZIPCODE", typeof(long));        //1
        m_weatherItemTable.Columns.Add("CITYNAME", typeof(string));     //2
        m_weatherItemTable.Columns.Add("LAT", typeof(double));          //3
        m_weatherItemTable.Columns.Add("LON", typeof(double));	        //4
        m_weatherItemTable.Columns.Add("TEMP", typeof(int));			      //5	
        m_weatherItemTable.Columns.Add("CONDITION", typeof(string));    //6
        m_weatherItemTable.Columns.Add("ICONNAME", typeof(string));	    //7	
        m_weatherItemTable.Columns.Add("ICONID", typeof(int));			    //8 
        m_weatherItemTable.Columns.Add("DAY", typeof(string));	        //9	
        m_weatherItemTable.Columns.Add("DATE", typeof(string));	        //10
        m_weatherItemTable.Columns.Add("LOW", typeof(string));	        //11
        m_weatherItemTable.Columns.Add("HIGH", typeof(string));	        //12
        m_weatherItemTable.Columns.Add("SELECTED", typeof(bool));		    //13
        m_weatherItemTable.Columns.Add("UPDATEDATE", typeof(DateTime)); //14	
        m_weatherItemTable.Columns.Add("TRACKERID", typeof(long));      //15


        //set the ID column to be auto increment
        m_weatherItemTable.Columns[0].AutoIncrement = true;
        m_weatherItemTable.Columns[0].ReadOnly = true;

        //the zipCode column must be the unique and nut allow null
        m_weatherItemTable.Columns[1].Unique = true;

        // set the ZIPCODE primary key for the table
        m_weatherItemTable.PrimaryKey = new DataColumn[] { m_weatherItemTable.Columns["ZIPCODE"] };

      }
      else //in case that the local cache exists, simply load the tables from the cache.
      {
        DataSet ds = new DataSet();
        ds.ReadXml(path);

        m_weatherItemTable = ds.Tables["RECORDS"];

        if (null == m_weatherItemTable)
          throw new Exception("Cannot find 'RECORDS' table");

        if (16 != m_weatherItemTable.Columns.Count)
          throw new Exception("Table 'RECORDS' does not have all required columns");

        m_weatherItemTable.Columns[0].ReadOnly = true;

        // set the ZIPCODE primary key for the table
        m_weatherItemTable.PrimaryKey = new DataColumn[] { m_weatherItemTable.Columns["ZIPCODE"] };

        //synchronize the locations table
        foreach (DataRow r in m_weatherItemTable.Rows)
        {
          try
          {
            //in case that the locations table does not exists, create and initialize it
            if (null == m_locations)
              InitializeLocations();

            //get the zipcode for the record
            string zip = Convert.ToString(r[1]);

            //make sure that there is no existing record with that zipCode already in the 
            //locations table.
            DataRow[] rows = m_locations.Select("ZIPCODE = " + zip);
            if (0 == rows.Length)
            {
              DataRow rec = m_locations.NewRow();
              rec[1] = Convert.ToInt64(r[1]);  //zip code 
              rec[2] = Convert.ToString(r[2]); //city name

              //add the new record to the locations table
              lock (m_locations)
              {
                m_locations.Rows.Add(rec);
              }
            }
          }
          catch (Exception ex)
          {
            System.Diagnostics.Trace.WriteLine(ex.Message);
          }
        }

        //dispose the DS
        ds.Tables.Remove(m_weatherItemTable);
        ds.Dispose();
        GC.Collect();
      }

      //initialize the symbol map table
      m_symbolTable = new DataTable("Symbology");

      //add the columns to the table
      m_symbolTable.Columns.Add("ID", typeof(int));			                    //0
      m_symbolTable.Columns.Add("ICONID", typeof(int));			                //1
      m_symbolTable.Columns.Add("SYMBOL", typeof(IGraphicTrackerSymbol));	  //2
      m_symbolTable.Columns.Add("SYMBOLWIDTH", typeof(int));	              //3
      m_symbolTable.Columns.Add("SYMBOLHEIGHT", typeof(int));	              //4
      m_symbolTable.Columns.Add("BITMAP", typeof(Bitmap));                  //5

      //set the ID column to be auto increment
      m_symbolTable.Columns[0].AutoIncrement = true;
      m_symbolTable.Columns[0].ReadOnly = true;

      m_symbolTable.Columns[1].AllowDBNull = false;
      m_symbolTable.Columns[1].Unique = true;

      //set ICONID as the primary key for the table
      m_symbolTable.PrimaryKey = new DataColumn[] { m_symbolTable.Columns["ICONID"] };
    }

    /// <summary>
    /// Initialize the location table. Gets the location from a featureclass
    /// </summary>
    private void InitializeLocations()
    {
      //create a new instance of the location table
      m_locations = new DataTable();

      //add fields to the table
      m_locations.Columns.Add("ID", typeof(int));
      m_locations.Columns.Add("ZIPCODE", typeof(long));
      m_locations.Columns.Add("CITYNAME", typeof(string));

      m_locations.Columns[0].AutoIncrement = true;
      m_locations.Columns[0].ReadOnly = true;

      //set ZIPCODE as the primary key for the table
      m_locations.PrimaryKey = new DataColumn[] { m_locations.Columns["ZIPCODE"] };

      PopulateLocationsTable();
    }

    /// <summary>
    /// Load the information from the MajorCities featureclass to the locations table
    /// </summary>
    private void PopulateLocationsTable()
    {
        string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        path = System.IO.Path.Combine(path, @"ArcGIS\data\USZipCodeData\");

      //open the featureclass
      IWorkspaceFactory wf = new ShapefileWorkspaceFactoryClass() as IWorkspaceFactory;
      IWorkspace ws = wf.OpenFromFile(path, 0);
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

        while (null != feature)
        {
          object obj = feature.get_Value(nameIndex);
          if (obj == null)
            continue;
          cityName = Convert.ToString(obj);

          obj = feature.get_Value(zipIndex);
          if (obj == null)
            continue;
          zip = long.Parse(Convert.ToString(obj));
          if (zip <= 0)
            continue;

          //add the current location to the location table
          DataRow r = m_locations.Rows.Find(zip);
          if (null == r)
          {
            r = m_locations.NewRow();
            r[1] = zip;
            r[2] = cityName;
            lock (m_locations)
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
      catch (Exception ex)
      {
        System.Diagnostics.Trace.WriteLine(ex.Message);
      }
    }


    /// <summary>
    /// weather ItemAdded event handler
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    /// <remarks>gets fired when an item is added to the table</remarks>
    private void OnWeatherItemAddedEvent(object sender, WeatherItemEventArgs args)
    {
      // use the invoke helper since this event gets fired on a different thread
      m_invokeHelper.RefreshWeatherItem(args);
    }
    #endregion
  }
}
