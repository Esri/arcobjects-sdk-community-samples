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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Timers;
using System.Threading;
using System.Xml;
using System.Text.RegularExpressions;
using System.IO;
using Microsoft.Win32;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.GlobeCore;
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.DataSourcesFile;

using OpenGL;
using ESRI.ArcGIS;

namespace RSSWeatherLayer3D
{
  /// <summary>
  /// RSSWeatherLayer3D is a custom layer for GlobeControl/ArcGlobe. It inherits GlobeCustomLayerBase
  /// which implements the relevant interfaces required by globe.
  /// This sample is a comprehensive sample of a real life scenario for creating a new layer in 
  /// order to consume a web service and display the information in a 3D environment using direct OpenGL plug-in.
  /// In this sample you can find implementation of simple editing capabilities, selection by 
  /// attribute and by location, persistence and identify.
  /// </summary>
  [Guid("C3E81E34-7421-4bce-86D6-4DB58813A1B4")]
  [ClassInterface(ClassInterfaceType.None)]
  [ComVisible(true)]
  [ProgId("RSSWeatherLayer3D.RSSWeatherLayer3DClass")]
  public sealed class RSSWeatherLayer3DClass : GlobeCustomLayerBase, IIdentify
  {
    #region class members
    private IVector3D           m_vector3D              = null;
    private IGlobeDisplay       m_globeDisplay          = null;
    private IGlobeViewUtil      m_globeViewUtil         = null;
    private bool                m_bDisplayListCreated   = false;
    private long                m_lItemToFlash          = -1L;
    private DataTable           m_TextureMap            = null;
    private DataTable           m_locations             = null;
    private System.Timers.Timer m_timer                 = null;
    private System.Timers.Timer m_redrawTimer           = null;
    private Thread              m_updateThread          = null;
    private uint                m_billboardRectList     = 0;
    private uint                m_selectionDisplayList  = 0;
    private double[]            m_modelViewMatrix       = null;
    private double[]            m_billboardMatrix       = null;
    private double[]            m_projMatrix            = null;
    private int[]               m_viewport              = null;
    private static int          m_flashCount            = 0;
    private static bool         m_flashDraw             = true;
    private string              m_iconFolder            = string.Empty;
    private string              m_weatherXmlFile        = string.Empty;
    private bool                m_bTimerIsRunning       = false;

    
    //redraw delegate, invokes the timer's event handler on the main thread
    private delegate void RedrawEventHandler();

    #endregion

    #region Ctor
    /// <summary>
    /// The class has only default CTor.
    /// </summary>
    public RSSWeatherLayer3DClass() : base()
    {
      m_sName = "RSSWeatherLayer3D";

      m_vector3D = new Vector3DClass();

      //set the bitmaps for the LayerInfo
      base.m_smallBitmap = new System.Drawing.Bitmap(GetType().Assembly.GetManifestResourceStream(GetType(), "Bitmaps.JaneSmall.bmp"));
      if (base.m_smallBitmap != null)
      {
        base.m_smallBitmap.MakeTransparent(base.m_smallBitmap.GetPixel(1, 1));
        base.m_hSmallBitmap = m_smallBitmap.GetHbitmap();
      }

      base.m_largeBitmap = new System.Drawing.Bitmap(GetType().Assembly.GetManifestResourceStream(GetType(), "Bitmaps.JaneLarge.bmp"));
      if (base.m_largeBitmap != null)
      {
        base.m_largeBitmap.MakeTransparent(base.m_largeBitmap.GetPixel(1, 1));
        base.m_hLargeBitmap = m_largeBitmap.GetHbitmap();
      }

      //get the directory for the layer's cache. If it does not exist, create it.
      string dir = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "WeatherIcons");
      if (!System.IO.Directory.Exists(dir))
      {
        System.IO.Directory.CreateDirectory(dir);
      }
      m_iconFolder = dir;

      //The cached data of the layer gets stored as XML
      m_weatherXmlFile = System.IO.Path.Combine(m_iconFolder, "Weather.xml");

      //create the spatial reference for the layer (in this case WGS1984)
      m_spRef = CreateGeographicSpatialReference();

      //initialize the layer's tables (main table as well as the textures table)
      InitializeTables();

      //get the location list from a featureclass (US major cities) and synchronize it with the 
      //cached information should it exists.
      if (null == m_locations)
        InitializeLocations();

      //instantiate the timer for the weather update (to periodically update the 
      //information against the RSS weather service)
      m_timer = new System.Timers.Timer(1000);
      m_timer.Enabled = false;
      m_timer.Elapsed += new ElapsedEventHandler(OnUpdateTimer);

      //initialize the redraw timer
      m_redrawTimer = new System.Timers.Timer(200);
      m_redrawTimer.Enabled = false;
      m_redrawTimer.Elapsed += new ElapsedEventHandler(OnRedrawUpdateTimer);

      //initialize the OpenGL matrices
      m_modelViewMatrix   = new double[16];
      m_billboardMatrix   = new double[16];
      m_projMatrix        = new double[16];
      m_viewport          = new int[4];
    }
    #endregion

    #region overriden methods

    /// <summary>
    /// This is where the actual drawing takes place.
    /// </summary>
    /// <param name="pGlobeViewer"></param>
    public override void DrawImmediate(IGlobeViewer pGlobeViewer)
    {
      //make sure that the layer is valid, visible and that the main table exists 
      if (!m_bVisible || !m_bValid | null == m_table)
        return;

      
      //get the OpenGL rendering mode
      uint mode;
      unsafe
      {
        int m;
        GL.glGetIntegerv(GL.GL_RENDER_MODE, &m);
        mode = (uint)m;
          //GL.glGetIntegerv(GL.GL_RENDER_MODE, out mode);
        
      }

      //get the OpenGL matrices (required for the viewport filtering and the billboard orientation)
      GL.glGetDoublev(GL.GL_MODELVIEW_MATRIX, m_modelViewMatrix);
      GL.glGetIntegerv(GL.GL_VIEWPORT, m_viewport);
      GL.glGetDoublev(GL.GL_PROJECTION_MATRIX, m_projMatrix);

      //populate the billboard matrix
      m_billboardMatrix[0] = m_modelViewMatrix[0];
      m_billboardMatrix[1] = m_modelViewMatrix[4];
      m_billboardMatrix[2] = m_modelViewMatrix[8];
      m_billboardMatrix[3] = 0;

      m_billboardMatrix[4] = m_modelViewMatrix[1];
      m_billboardMatrix[5] = m_modelViewMatrix[5];
      m_billboardMatrix[6] = m_modelViewMatrix[9];
      m_billboardMatrix[7] = 0;

      m_billboardMatrix[8] = m_modelViewMatrix[2];
      m_billboardMatrix[9] = m_modelViewMatrix[6];
      m_billboardMatrix[10] = m_modelViewMatrix[10];
      m_billboardMatrix[11] = 0;

      m_billboardMatrix[12] = 0;
      m_billboardMatrix[13] = 0;
      m_billboardMatrix[14] = 0;
      m_billboardMatrix[15] = 1;

      ISceneViewer sceneViewer = pGlobeViewer.GlobeDisplay.ActiveViewer;

      //only once, create display lists and do initializations
      if (!m_bDisplayListCreated)
      {
        CreateDisplayLists();
        m_globeDisplay = pGlobeViewer.GlobeDisplay;
      }

      //get the globeViewUtil which allow to convert between the different globe coordinate systems
      m_globeViewUtil = sceneViewer.Camera as IGlobeViewUtil;
      IGlobeViewUtil globeViewUtil = sceneViewer.Camera as IGlobeViewUtil;
      IGlobeAdvancedOptions advOpt = m_globeDisplay.AdvancedOptions;

      //the ClipNear value is required for the viewport filtering (since we don't
      //want to draw an item which is beyond the clipping planes).
      double clipNear = advOpt.ClipNear;

      double dblObsX, dblObsY, dblObsZ, dMagnitude;
      ICamera camera = pGlobeViewer.GlobeDisplay.ActiveViewer.Camera;
      
      //query the camera location in geocentric coordinate (OpenGL coord system)
      camera.Observer.QueryCoords(out dblObsX, out dblObsY);
      dblObsZ = camera.Observer.Z;

      double lat, lon, X = 0.0, Y = 0.0, Z = 0.0;

      //iterate through all the records of the layer, test whether the item is within the 
      //viewport are and draw it onto the globe.
      foreach (DataRow rec in m_table.Rows)
      {

        lat = Convert.ToDouble(rec[3]);
        lon = Convert.ToDouble(rec[4]);
        X = Convert.ToDouble(rec[5]);
        Y = Convert.ToDouble(rec[6]);
        Z = Convert.ToDouble(rec[7]);

        #region get the OGL geocentric coordinates X,Y,Z
        if (X == 0.0 && Y == 0.0 && Z == 0.0)
        {
          //calculate the geocentric coordinates
          globeViewUtil.GeographicToGeocentric(lon, lat, 1000.0, out X, out Y, out Z);

          //write the calculated geocentric coordinate to the table
          lock (m_table)
          {
            rec[5] = X;
            rec[6] = Y;
            rec[7] = Z;
            rec.AcceptChanges();
          }
        }
        #endregion

        //make sure that the item is inside the viewport, otherwise no need to draw it
        if (!InsideViewport(X, Y, Z, clipNear, mode))
          continue;

        //get the distance from the camera to the drawn item.
        //This distance will determine whether to draw the item as a dot or as
        //a full icon.
        m_vector3D.SetComponents(dblObsX - X, dblObsY - Y, dblObsZ - Z);
        dMagnitude = m_vector3D.Magnitude;

        //call the drawing method
        DrawItem(rec, dMagnitude);
      }
    }

    //since this is Globe where the default coord-sys is WGS1984, there is no need to 
    //reproject the underlying data (which is already WGS1984)
    public override ISpatialReference SpatialReference
    {
      get
      {
        if (null == m_spRef )
        {
          m_spRef  = CreateGeographicSpatialReference();
        }
        return m_spRef ;
      }
    }
    public override IEnvelope AreaOfInterest
    {
      get
      {
        return this.Extent;
      }
    }
    public override IEnvelope Extent
    {
      get
      {
        //iterate through all the items in the layer and get the overall extent.
        m_extent  = GetLayerExtent();
        if (null == m_extent )
          return null;

        //return a copy of the extent (in case that the caller is on another thread)
        IEnvelope env = ((IClone)m_extent ).Clone() as IEnvelope;
  
        return env;
      }
    }

    //the ProgID of the layer
    public override UID ID
    {
      get
      {
        ESRI.ArcGIS.esriSystem.UID uid = new ESRI.ArcGIS.esriSystem.UIDClass();
        uid.Value = "RSSWeatherLayer3D.RSSWeatherLayer3DClass";
        return uid;
      }
    }

    //the guid of the layer
    public override void GetClassID(out Guid pClassID)
    {
      ESRI.ArcGIS.esriSystem.UID uid = new ESRI.ArcGIS.esriSystem.UIDClass();
      uid.Value = "RSSWeatherLayer3D.RSSWeatherLayer3DClass";

      pClassID = new Guid(Convert.ToString(uid.Value));
    }

    public override void Load(IStream pstm)
    {
      //allocate a new vector 3D, no need to mess around with writing and reading...
      m_vector3D = new Vector3DClass();

      base.Load(pstm);
    }
    
    
    /// <summary>
    /// Hit method is used to get items by interacting with the display (such as select by area).
    /// The mechanism used in here is OpenGL selection buffer.
    /// Each object is assigned with a unique id (zipCode) and is loaded to the selection buffer
    /// 'glLoadName'. The globe framework will return this ID and we will use it in order to 
    /// select the items from the table.
    /// </summary>
    /// <param name="hitObjectID"></param>
    /// <param name="pHit3D"></param>
    public override void Hit(int hitObjectID, ESRI.ArcGIS.Analyst3D.IHit3D pHit3D)
    {
      try
      {
        object owner = pHit3D.Owner;
        
        //make sure that the owner is a weather layer
        if (!(owner is RSSWeatherLayer3DClass))
          return;

        //get the record by the zip code received from the selection buffer 
        DataRow[] rows = m_table.Select("ZIPCODE = " + hitObjectID.ToString());
        if (rows.Length == 0)
          return;

        //serialize the information into a propertySet.
        DataRow r = rows[0];
        IPropertySet propSet = new PropertySetClass();
        propSet.SetProperty("ZIPCODE", r[1]);
        propSet.SetProperty("CITYNAME", r[2]);
        propSet.SetProperty("LATITUDE", r[3]);
        propSet.SetProperty("LONGITUDE", r[4]);
        propSet.SetProperty("TEMPERATURE", r[8]);
        propSet.SetProperty("DESCRIPTION", r[9]);
        propSet.SetProperty("DAY", r[11]);
        propSet.SetProperty("DATE", r[12]);
        propSet.SetProperty("LOW", r[13]);
        propSet.SetProperty("HIGH", r[14]);
        propSet.SetProperty("UPDATED", r[16]);

        IPoint hitPoint = pHit3D.Point;
        if (null == hitPoint)
        {
          double lat, lon, alt;
          lat = Convert.ToDouble(r[3]);
          lon = Convert.ToDouble(r[4]);
          alt = 1000.0;

          IPoint point = new PointClass();
          ((IZAware)point).ZAware = true;
          point.PutCoords(lon, lat);
          point.Z = alt;

          pHit3D.Point = point;
        }

        string shortIconName = Convert.ToString(r[10]);
        shortIconName = shortIconName.Substring(shortIconName.LastIndexOf("/") + 1);
        propSet.SetProperty("ICON", shortIconName);

        //pass the propertySet to the caller. 
        pHit3D.Object = propSet;
      }
      catch (Exception ex)
      {
        System.Diagnostics.Trace.WriteLine("Hit: " + ex.Message);
      }
    }

    //override the OnHandleDestroyed in order to prevent from calling Invoke after the handle 
    //has been destroyed
    protected override void OnHandleDestroyed(EventArgs e)
    {
      m_bTimerIsRunning = false;

      base.OnHandleDestroyed(e);
    }
    #endregion

    #region private utility methods

    /// <summary>
    /// create a WGS1984 geographic coordinate system.
    /// In this case, the underlying data is in WGS1984 as well as the input required by the
    /// globe. for that reason, there is no need to reproject the data.
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
      if (null == base.m_spRef)
      {
        base.m_spRef = CreateGeographicSpatialReference();
      }

      IEnvelope env = new EnvelopeClass();
      env.SpatialReference = base.m_spRef;
      IPoint point = new PointClass();
      point.SpatialReference = m_spRef;
      foreach (DataRow r in m_table.Rows)
      {
        point.Y = Convert.ToDouble(r[3]);
        point.X = Convert.ToDouble(r[4]);

        env.Union(point.Envelope);
      }

      return env;
    }

    /// <summary>
    /// initialize the main table used by the layer as well as the texture table.
    /// The base class calls new on the table and adds a default ID field.
    /// </summary>
    private void InitializeTables()
    {
      //In case that there is no existing cache on the local machine, create the table.
      if (!System.IO.File.Exists(m_weatherXmlFile))
      {
        //add fields to the table
        m_table.Columns.Add("ZIPCODE", typeof(long));
        m_table.Columns.Add("CITYNAME", typeof(string));
        m_table.Columns.Add("LAT", typeof(double)); //3
        m_table.Columns.Add("LON", typeof(double));
        m_table.Columns.Add("X", typeof(double));
        m_table.Columns.Add("Y", typeof(double));
        m_table.Columns.Add("Z", typeof(double)); //7
        m_table.Columns.Add("TEMP", typeof(int));
        m_table.Columns.Add("CONDITION", typeof(string));
        m_table.Columns.Add("ICONNAME", typeof(string)); //10
        m_table.Columns.Add("DAY", typeof(string));
        m_table.Columns.Add("DATE", typeof(string));
        m_table.Columns.Add("LOW", typeof(string));
        m_table.Columns.Add("HIGH", typeof(string));
        m_table.Columns.Add("SELECTED", typeof(bool)); //15
        m_table.Columns.Add("UPDATEDATE", typeof(DateTime)); //16
        m_table.Columns.Add("ICONID", typeof(int)); //17

        //make sure to autoincrement the ID column
        m_table.Columns[0].AutoIncrement = true;
        m_table.Columns[0].ReadOnly = true;

        //ZipCode columns must not be null
        m_table.Columns[1].AllowDBNull = false;

        // set the ZIPCODE primary key for the table
        m_table.PrimaryKey = new DataColumn[] { m_table.Columns["ZIPCODE"] };

        //initialize the texture map table
        m_TextureMap = new DataTable("TextureMap");

        //add fields to the table
        m_TextureMap.Columns.Add("ID", typeof(int));
        m_TextureMap.Columns.Add("ICONNAME", typeof(string));
        m_TextureMap.Columns.Add("TEXTUREID", typeof(int));
        m_TextureMap.Columns.Add("ICONID", typeof(int));

        //make sure to autoincrement the ID column
        m_TextureMap.Columns[0].AutoIncrement = true;
        m_TextureMap.Columns[0].ReadOnly = true;

        //TextureID columns must not be null
        m_TextureMap.Columns[1].AllowDBNull = false;

        //set ICONID as the primary key for the table.
        //searching for a texture by its ID is most efficient.
        m_TextureMap.PrimaryKey = new DataColumn[] { m_TextureMap.Columns["ICONID"] };
      }
      else //in case that the local cache exists, simply load the tables from the cache.
      {
        DataSet ds = new DataSet();
        ds.ReadXml(m_weatherXmlFile);

        m_table = ds.Tables["RECORDS"];
        m_TextureMap = ds.Tables["TextureMap"];

        // set the ZIPCODE primary key for the table
        m_table.PrimaryKey = new DataColumn[] { m_table.Columns["ZIPCODE"] };

        //set ICONID as the primary key for the table
        m_TextureMap.PrimaryKey = new DataColumn[] { m_TextureMap.Columns["ICONID"] };

        //synchronize the locations table
        foreach (DataRow r in m_table.Rows)
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
            if (0 == rows.Length) //in case that the record does not exists
            {
              DataRow rec = m_locations.NewRow();
              rec[1] = Convert.ToString(r[1]);
              rec[2] = Convert.ToInt64(r[2]);

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
        ds.Tables.Remove(m_table);
        ds.Tables.Remove(m_TextureMap);
        ds.Dispose();
        GC.Collect();
      }
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

      //spawn a background thread to populate the locations table
      Thread t = new Thread(new ThreadStart(PopulateLocationsTableProc));
      t.Start();

      System.Threading.Thread.Sleep(200);
    }

    /// <summary>
    /// Load the information from the MajorCities featureclass to the locations table
    /// </summary>
    private void PopulateLocationsTableProc()
    {
      //get the ArcGIS path from the registry
      String versionNumber = RuntimeManager.ActiveRuntime.Version;
      RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\ESRI\ArcObjectsSDK" + versionNumber);
      string path = Convert.ToString(key.GetValue("InstallDir"));
      //set the path to the featureclass used by the GPS simulator
      path = System.IO.Path.Combine(path, @"Samples\data\USZipCodeData");

       
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
    /// the main update thread for the layer.
    /// </summary>
    /// <remarks>Sine the layer gets the weather information from a web service which might
    /// take a while to respond, it is not logical to let the application hand while waiting
    /// for respond. Therefore, running the request on a different thread frees the application to 
    /// continue working while waiting for a response. 
    /// Please note that in this case, synchronization of shared resources must be addressed,
    /// otherwise you might end up getting unexpected results.</remarks>
    private void ThreadProc()
    {
      try
      {
        long lZipCode;
        //iterate through all the records in the main table and update it against 
        //the information on the website.
        foreach (DataRow r in m_locations.Rows)
        {
          System.Threading.Thread.Sleep(300);

          lZipCode = Convert.ToInt32(r[1]);

          AddWeatherItem(lZipCode, 0.0, 0.0);
        }

        //save the DS

        //clone the texture map table
        DataTable clonedTextureMap = m_TextureMap.Copy();
        //set all the texture IDs to -1 in order to prevent problems at load time
        //next time that the application loads the layer from the cache.
        foreach (DataRow r in clonedTextureMap.Rows)
        {
          r[2] = -1;
          r.AcceptChanges();
        }

        //serialize the tables onto the local machine
        DataSet ds = new DataSet();
        ds.Tables.Add(m_table);
        ds.Tables.Add(clonedTextureMap);
        ds.WriteXml(m_weatherXmlFile);
        ds.Tables.Remove(m_table);
        ds.Tables.Remove(clonedTextureMap);
        clonedTextureMap.Dispose();
        ds.Dispose();
        GC.Collect();
      }
      catch (Exception ex)
      {
        System.Diagnostics.Trace.WriteLine(ex.Message);
      }
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
    /// Globe redraw timer event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnRedrawUpdateTimer(object sender, ElapsedEventArgs e)
    {
      //since this is the timer event handler, it gets executed on a different thread than
      //the main one. Therefore the Invoke call is required in order to force the call
      //on the main thread and thus prevent cross apartment calls
      if(m_bTimerIsRunning)
        base.Invoke(new RedrawEventHandler(OnRedrawEventHandler));  
    }

    /// <summary>
    /// Globe redraw event handler.
    /// </summary>
    /// <remarks>since this method is the delegate method of an 'Invoke' call it is 
    /// guaranteed to run on the main thread and therefore does not end up in 
    /// making cross apartment COM calls</remarks>
    void OnRedrawEventHandler()
    {
      m_globeDisplay.RefreshViewers();
    }

    /// <summary>
    /// Create the display lists used by the layer
    /// </summary>
    /// <remarks>the cal to this method must be made from BeforeDraw, AfterDraw or DrawImmidiate.
    /// calling this method from anywhere else might end up in unexpected results since OpenGL state is
    /// not guaranteed</remarks>
    private void CreateDisplayLists()
    {
      //create the display list for the weather icons
      //the quad size is set to 1 unit. Therefore you will have to scale it 
      //each time before drawing.
      m_billboardRectList = GL.glGenLists(1);
      GL.glNewList(m_billboardRectList, GL.GL_COMPILE);
        GL.glPushMatrix();
          //shift the item 1/2 unit to the left so that it'll get drawn around the 
          //middle of its base
          GL.glTranslatef(-0.5f, 0.0f, 0.0f);
          GL.glPolygonMode(GL.GL_FRONT, GL.GL_FILL);
          GL.glDisable(GL.GL_LIGHTING);
          //enable texture in order to allow for texture binding
          GL.glTexEnvi(GL.GL_TEXTURE_ENV, GL.GL_TEXTURE_ENV_MODE, (int)GL.GL_MODULATE);
          //set blending to allow for transparency
          GL.glEnable(GL.GL_BLEND);
          GL.glBlendFunc(GL.GL_SRC_ALPHA, GL.GL_ONE_MINUS_SRC_ALPHA);
          GL.glDepthFunc(GL.GL_LEQUAL);

          //create the geometry (quad) and specify the texture coordinates
          GL.glBegin(GL.GL_QUADS);
          GL.glTexCoord2f(0.0f, 0.0f); GL.glVertex3f(0.0f, 0.0f, 0.0f);
          GL.glTexCoord2f(0.0f, 1.0f); GL.glVertex3f(0.0f, 1.0f, 0.0f);
          GL.glTexCoord2f(1.0f, 1.0f); GL.glVertex3f(1.0f, 1.0f, 0.0f);
          GL.glTexCoord2f(1.0f, 0.0f); GL.glVertex3f(1.0f, 0.0f, 0.0f);
          GL.glEnd();

        GL.glPopMatrix();
      GL.glEndList();


      //create the list for the selection symbol (used for selection and to flash items)
      m_selectionDisplayList = GL.glGenLists(1);
      GL.glNewList(m_selectionDisplayList, GL.GL_COMPILE);
        GL.glPushMatrix();
          GL.glLineWidth(2.0f);
          GL.glTranslatef(-0.5f, 0.0f, 0.0f);
          GL.glBegin(GL.GL_LINE_STRIP);
          GL.glVertex3f(0.0f, 0.0f, 0.0f);
          GL.glVertex3f(1.0f, 0.0f, 0.0f);
          GL.glVertex3f(1.0f, 1.0f, 0.0f);
          GL.glVertex3f(0.0f, 1.0f, 0.0f);
          GL.glVertex3f(0.0f, 0.0f, 0.0f);
          GL.glEnd();
        GL.glPopMatrix();
      GL.glEndList();

      m_bDisplayListCreated = true;
    }

    /// <summary>
    /// Given a bitmap (GDI+), create for it an OpenGL texture and return its ID
    /// </summary>
    /// <param name="bitmap"></param>
    /// <returns>the OGL texture id</returns>
    /// <remarks>in order to allow hardware acceleration, texture size must be power of two.</remarks>
    private uint CreateTexture(Bitmap bitmap)
    {
      try
      {
        //get the bitmap's dimensions
        int h = bitmap.Height;
        int w = bitmap.Width;
        int s = Math.Max(h, w);

        //calculate the closest power of two to match the bitmap's size
        //(thank god for high-school math...)
        double x = Math.Log(Convert.ToDouble(s)) / Math.Log(2.0);
        s = Convert.ToInt32(Math.Pow(2.0, Convert.ToDouble(Math.Ceiling(x))));

        int bufferSizeInPixels = s * s;

        //get the bitmap's raw data 
        Rectangle rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
        bitmap.RotateFlip(RotateFlipType.RotateNoneFlipY);
        System.Drawing.Imaging.BitmapData bitmapData;
        bitmapData = bitmap.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
  
        byte[] bgrBuffer = new byte[bufferSizeInPixels * 3];
        
        //scale the bitmap to be a power of two
        unsafe
        {
          fixed (byte* pBgrBuffer = bgrBuffer)
          {
            GLU.gluScaleImage(GL.GL_BGR_EXT, bitmap.Size.Width, bitmap.Size.Height, GL.GL_UNSIGNED_BYTE, bitmapData.Scan0.ToPointer(), s, s, GL.GL_UNSIGNED_BYTE, pBgrBuffer);
          }
        }

        //create a new buffer to store the raw data and set the transparency color (alpha = 0)
        byte[] bgraBuffer = new byte[bufferSizeInPixels * 4];

        int posBgr = 0;
        int posBgra = 0;
        for (int i = 0; i < bufferSizeInPixels; i++)
        {
          bgraBuffer[posBgra] = bgrBuffer[posBgr];			    //B
          bgraBuffer[posBgra + 1] = bgrBuffer[posBgr + 1];  //G
          bgraBuffer[posBgra + 2] = bgrBuffer[posBgr + 2];  //R

          //take care of the alpha
          if (255 == bgrBuffer[posBgr] && 255 == bgrBuffer[posBgr + 1] && 255 == bgrBuffer[posBgr + 2])
          {
            bgraBuffer[posBgra + 3] = 0;
          }
          else
          {
            bgraBuffer[posBgra + 3] = 255;
          }
          posBgr += 3;
          posBgra += 4;
        }

        //create the texture
        uint[] texture = new uint[1];
        GL.glEnable(GL.GL_TEXTURE_2D);
        GL.glGenTextures(1, texture);
        GL.glBindTexture(GL.GL_TEXTURE_2D, texture[0]);

        //set the texture parameters
        GL.glTexParameteri(GL.GL_TEXTURE_2D, GL.GL_TEXTURE_MAG_FILTER, (int)GL.GL_LINEAR);
        GL.glTexParameteri(GL.GL_TEXTURE_2D, GL.GL_TEXTURE_MIN_FILTER, (int)GL.GL_LINEAR);
        GL.glTexEnvi(GL.GL_TEXTURE_ENV, GL.GL_TEXTURE_ENV_MODE, (int)GL.GL_MODULATE);

        unsafe
        {
          fixed (byte* pBgraBuffer = bgraBuffer)
          {
            GL.glTexImage2D(GL.GL_TEXTURE_2D, 0, (int)GL.GL_RGBA, s, s, 0, GL.GL_BGRA_EXT, GL.GL_UNSIGNED_BYTE, pBgraBuffer);
          }
        }

        //unlock the bitmap from memory
        bitmap.UnlockBits(bitmapData);

        //return the newly created texture id
        return texture[0];

      }
      catch (Exception ex)
      {
        System.Diagnostics.Trace.WriteLine(ex.Message);
      }
      return (uint)0;
    }

    /// <summary>
    /// orient the weather icons so that it'll face the camera
    /// </summary>
    private void OrientBillboard()
    {
      GL.glMultMatrixd(m_billboardMatrix);
    }

    /// <summary>
    /// Test whether an item is inside the current viewport
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="z"></param>
    /// <param name="clipNear"></param>
    /// <param name="mode"></param>
    /// <returns></returns>
    /// <remarks>given geocentric coordinate, convert it into window coordinate and then 
    /// test whether it is within the current viewport</remarks>
    private bool InsideViewport(double x, double y, double z, double clipNear, uint mode)
    {
      bool inside = true;

      //In selection mode the projection matrix is changed.
      //Therefore use the GlobeViewUtil because calling gluProject would give unexpected results.
      if (GL.GL_SELECT == mode)
      {
        int winX, winY;
        m_globeViewUtil.GeocentricToWindow(x, y, z, out winX, out winY);
        inside = (winX >= m_viewport[0] && winX <= m_viewport[2]) && (winY >= m_viewport[1] && winY <= m_viewport[3]);

      }
      else
      {
        
        //use gluProject in order to convert into windows coordinate
        unsafe
        {
          double winx, winy, winz;
          GLU.gluProject(x, y, z, m_modelViewMatrix, m_projMatrix, m_viewport, &winx, &winy, &winz);

          inside = (winx >= m_viewport[0] && winx <= m_viewport[2]) && (winy >= m_viewport[1] && winy <= m_viewport[3] && (winz >= clipNear && winz <= 1.0));
        }        
      }

      return inside;
    }
    /// <summary>
    /// Makes a request against Yahoo! RSS Weather service and add update the layer's table
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

        //get the cityname
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


        //use regex in order to extract the icon name. The description tag is an HTML tag.
        Match m = Regex.Match(desc, regxQry);
        if (m.Success)
        {
          iconPath = m.Value;

          //add the iconPath to the textureMap table
          DataRow tr = m_TextureMap.Rows.Find(iconCode);
          //test whether the texture table does not already include an entry for that icon
          if (null == tr)
          {
            //create a new record
            tr = m_TextureMap.NewRow();
            tr[1] = iconPath;
            tr[2] = -1;
            tr[3] = iconCode;
            lock (m_TextureMap)
            {
              m_TextureMap.Rows.Add(tr);
            }
          }
        }
        else
        {
          iconPath = "";
        }

        //test whether the record already exists in the layer's table.
        DataRow dbr = m_table.Rows.Find(zipCode);
        if (null == dbr)
        {
          //create a new record
          dbr = m_table.NewRow();

          dbr[1] = zipCode;
          dbr[2] = cityName;
          dbr[3] = lat;
          dbr[4] = lon;
          dbr[5] = 0.0;
          dbr[6] = 0.0;
          dbr[7] = 0.0;
          dbr[8] = temp;
          dbr[9] = condition;
          dbr[10] = iconPath;
          dbr[11] = day;
          dbr[12] = date;
          dbr[13] = low;
          dbr[14] = high;
          dbr[15] = false;
          dbr[16] = DateTime.Now;
          dbr[17] = iconCode;

          //add the new record to the table
          lock (m_table)
          {
            m_table.Rows.Add(dbr);
          }
        }
        else //update the existing record
        {
          dbr[8] = temp;
          dbr[9] = condition;
          dbr[10] = iconPath;
          dbr[11] = day;
          dbr[12] = date;
          dbr[13] = low;
          dbr[14] = high;
          dbr[16] = DateTime.Now;
          dbr[17] = iconCode;
          lock (m_table)
          {
            dbr.AcceptChanges();
          }
        }
      }
      catch (Exception ex)
      {
        System.Diagnostics.Trace.WriteLine(ex.Message);
      }
    }

    /// <summary>
    /// draw an item on the globe's surface
    /// </summary>
    /// <param name="r"></param>
    /// <param name="dMagnitude"></param>
    /// <remarks>the magnitude determine whether to draw the item as a dot or with the icon symbol.
    /// The determination of the magnitude threshold is empirically.</remarks>
    private void DrawItem(DataRow r, double dMagnitude)
    {
      double lat, lon, X = 0.0, Y = 0.0, Z = 0.0;
      long lZipCode;
      string cityName;
      string condition;
      string iconPath;
      int iconId;
      int temp;
      bool bIsSelected;
      Bitmap b = null;

      if (null != r)
      {
        //get the information from the record
        lZipCode = Convert.ToInt64(r[1]);
        lat = Convert.ToDouble(r[3]);
        lon = Convert.ToDouble(r[4]);
        X = Convert.ToDouble(r[5]);
        Y = Convert.ToDouble(r[6]);
        Z = Convert.ToDouble(r[7]);
        cityName = Convert.ToString(r[2]);
        condition = Convert.ToString(r[9]);
        iconPath = Convert.ToString(r[10]);
        temp = Convert.ToInt32(r[8]);
        bIsSelected = Convert.ToBoolean(r[15]);
        iconId = Convert.ToInt32(r[17]);

        #region search for the icon in the texture map table
        DataRow tr = m_TextureMap.Rows.Find(iconId);
        int bitmapTextureId = -1;
        if (null != tr)
        {
          bitmapTextureId = Convert.ToInt32(tr[2]);
          //in case that the texture id is not valid, create the texture
          if (-1 == bitmapTextureId || (byte)0 == GL.glIsTexture((uint)bitmapTextureId))
          {
            try
            {
              //search for the icon on the local hard drive first
              string iconFileName = System.IO.Path.Combine(m_iconFolder, iconPath.Substring(iconPath.LastIndexOf("/") + 1));
              if (File.Exists(iconFileName))
              {
                b = new Bitmap(iconFileName, true);
              }
              else
              {
                //get the bitmap from the web
                using (System.Net.WebClient webClient = new System.Net.WebClient())
                {
                  using (System.IO.Stream stream = webClient.OpenRead(iconPath))
                  {
                    b = new Bitmap(stream, true);

                    //save the bitmap to the icons folder
                    b.Save(iconFileName);
                  }
                }
              }

              //create the texture and store it in the textures table
              if (null != b)
              {
                bitmapTextureId = (int)CreateTexture(b);
                tr[2] = bitmapTextureId;

                //add the record to the table
                lock (m_TextureMap)
                {
                  tr.AcceptChanges();
                }
              }
            }
            catch (Exception ex)
            {
              System.Diagnostics.Trace.WriteLine(ex.Message);
            }
          }
        }
        #endregion

        #region determine weather the item need to be flashed
        //in case that the user has identified the item or explicitly asked required to flash 
        //the item. The layer is using timer to redraw the display in a constant pace, we will 
        //take advantage of that fact in order to alternate the selection list around the flashed
        //item and therefore give the notion of blinking.
        if (lZipCode == m_lItemToFlash)
        {
          if (m_flashCount > 10)
          {
            m_lItemToFlash = -1;
            m_flashDraw = true;
            m_flashCount = 0;
          }
          else
          {
            m_flashCount++;
            m_flashDraw = !m_flashDraw;
          }
        }
        #endregion
        if (dMagnitude > 1.75)
        {
          #region draw the point location
          //in case that the item is far from the camera, it can be drawn as a simple dot.
          GL.glEnable(GL.GL_POINT_SMOOTH);
          if (bIsSelected)
          {
            //set selection size and color
            GL.glPointSize(7.0f);
            GL.glColor3ub(255, 255, 128);
          }
          else
          {
            //set size and color
            GL.glPointSize(5.0f);
            GL.glColor3ub(255, 0, 0);
          }

          //flash the shape
          if (lZipCode == m_lItemToFlash && m_flashDraw == true)
          {
            GL.glPointSize(5.0f);
            GL.glColor3ub(0, 255, 0);
          }

          //draw the dot
          GL.glLoadName((uint)lZipCode);
          GL.glBegin(GL.GL_POINTS);
          GL.glVertex3f(Convert.ToSingle(X), Convert.ToSingle(Y), Convert.ToSingle(Z));
          GL.glEnd();
          GL.glDisable(GL.GL_POINT_SMOOTH);
          #endregion
        }
        else
        {
          #region draw the forecast icon
          //enable 2D texture and bind the icon's texture
          GL.glEnable(GL.GL_TEXTURE_2D);
          if (-1 != bitmapTextureId)
          {
            GL.glBindTexture(GL.GL_TEXTURE_2D, (uint)bitmapTextureId);
          }
          
          GL.glPushMatrix();

          //translate to the items location
          GL.glTranslatef(Convert.ToSingle(X), Convert.ToSingle(Y), Convert.ToSingle(Z));
          //orient the icon so that it'll face the camera
          OrientBillboard();

          //scale the item (original size is 1 ubit)
          double useScale = 0.04 * dMagnitude;
          GL.glScaled(useScale, useScale, 1.0);

          GL.glColor4f(1.0f, 1.0f, 1.0f, 1.0f);
          //loads the zipcode onto the name stack. The name stack is used during selection mode (please refer to method 'Hit').
          GL.glLoadName((uint)lZipCode);
          //draw the item
          GL.glCallList(m_billboardRectList);

          //switch back the OGL different modes 
          GL.glDisable(GL.GL_TEXTURE_2D);
          GL.glDisable(GL.GL_BLEND);
          GL.glDisable(GL.GL_DEPTH_TEST);
          GL.glDisable(GL.GL_ALPHA_TEST);
          
          //in case that the item is selected, draw the selection list.
          if (bIsSelected)
          {
            GL.glColor4ub(255, 255, 128, 255);
            GL.glCallList(m_selectionDisplayList);
          }

          //flash the shape
          if (lZipCode == m_lItemToFlash && m_flashDraw == true)
          {
            GL.glColor4ub(0, 255, 0, 255);
            GL.glCallList(m_selectionDisplayList);
          }
          GL.glPopMatrix();
          
          #endregion
        }
      }
    }
    #endregion

    #region public methods and props
    
    /// <summary>
    /// connects to the RSS weather service
    /// </summary>
    public void Connect()
    {
      //make sure that the control's handle got created since it is necessary
      //for the 'Invoke' calls 
      if (IntPtr.Zero == this.Handle)
        throw new Exception("RSSWeatherLayer3D.Connect: Error creating handle for the layer");


      //enable the update and redraw timers
      m_timer.Enabled = true;
      m_redrawTimer.Enabled = true;

      m_bTimerIsRunning = true;
    }

    /// <summary>
    /// disconnects from the RSS weather service
    /// </summary>
    public void Disconnect()
    {
      //disable the update timer
      m_bTimerIsRunning = false;
      m_timer.Enabled = false;
      m_redrawTimer.Enabled = false;
      try
      {
        //abort the update thread in case that it is alive
        if (m_updateThread.IsAlive)
          m_updateThread.Abort();
      }
      catch
      {
        System.Diagnostics.Trace.WriteLine("RSSWeatherLayer3D update thread has been terminated");
      }
    }

    /// <summary>
    /// specify the item to flash
    /// </summary>
    /// <param name="zipCode"></param>
    public void Flash(long zipCode)
    {
      m_lItemToFlash = zipCode;
    }

    /// <summary>
    /// Zoom to a weather item according to its city name
    /// </summary>
    /// <param name="cityName"></param>
    public void ZoomTo(string cityName)
    {
      if (null == m_table)
        return;

      DataRow[] rows = m_table.Select("CITYNAME = '" + cityName + "'");
      if (rows.Length == 0)
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
      if (null == m_table || null == m_globeDisplay)
        return;

      DataRow[] rows = m_table.Select("ZIPCODE = " + zipCode.ToString());
      if (rows.Length == 0)
        return;

      DataRow r = rows[0];

      //get the lat/lon from the table
      double lat = Convert.ToDouble(r[3]);
      double lon = Convert.ToDouble(r[4]);

      //set an envelope around the point
      IEnvelope env = new EnvelopeClass();
      ((IZAware)env).ZAware = true;

      env.PutCoords(lon - 0.1, lat - 0.1, lon + 0.1, lat + 0.1);
      env.ZMax = 1500.0;
      env.ZMin = 500.0;

      m_globeDisplay.IsNavigating = true;
      //zoom to the item's location
      ((IGlobeCamera)m_globeDisplay.ActiveViewer.Camera).SetToZoomToExtents(env, m_globeDisplay.Globe, m_globeDisplay.ActiveViewer);
      m_globeDisplay.IsNavigating = false;
    }

    /// <summary>
    /// select a weather item by its zipCode
    /// </summary>
    /// <param name="zipCode"></param>
    /// <param name="newSelection"></param>
    public void Select(long zipCode, bool newSelection)
    {
      if (null == m_table)
        return;

      //if it is a new selection, unselect any selected items
      if (newSelection)
      {
        //unselect all the currently selected items
        lock (m_table)
        {
          foreach (DataRow r in m_table.Rows)
          {
            r[15] = false;
          }
          lock (m_table)
            m_table.AcceptChanges();
        }
      }

      //get the record from the table
      DataRow[] rows = m_table.Select("ZIPCODE = " + zipCode.ToString());
      //make sure that the record exists
      if (rows.Length == 0)
        return;

      DataRow rec = rows[0];
      //set the selection flag to true
      lock (m_table)
      {
        rec[15] = true;
        rec.AcceptChanges();
      }
    }

    /// <summary>
    /// Run the update thread
    /// </summary>
    /// <remarks>calling this method too frequently might end up in blockage of RSS service.
    /// The service will interpret the excessive calls as an offence and thus would block the service. </remarks>
    public void RefreshDB()
    {
      try
      {
        m_updateThread = new Thread(new ThreadStart(ThreadProc));

        //run the update thread
        m_updateThread.Start();
          
      }
      catch (Exception ex)
      {
        System.Diagnostics.Trace.WriteLine(ex.Message);
      }
    }

    //add a new item given only a zipcode (will use the default location given by the service)
    //should the item exists, it will get updated
    public bool AddItem(long zipCode)
    {
      return AddItem(zipCode, 0.0, 0.0);
    }

    //adds a new item given a zipcode and a coordinate.
    //Should the item already exists, it will get updated and will move to the new coordinate.
    public bool AddItem(long zipCode, double lat, double lon)
    {
      try
      {
        if (null == m_table)
          return false;
        double X = 0.0, Y = 0.0, Z = 0.0;

        DataRow r = m_table.Rows.Find(zipCode);
        if (null != r) //if the record with this zipCode already exists
        {
          //in case that the record exists and the input coordinates are not valid 
          if (lat == 0.0 && lon == 0.0)
            return false;
          else //update the location according to the new coordinate
          {
            //calculate the geocentric coordinates
            if (null != m_globeDisplay)
            {
              IGlobeViewUtil globeViewUtil = (IGlobeViewUtil)m_globeDisplay.ActiveViewer.Camera;
              globeViewUtil.GeographicToGeocentric(lon, lat, 1000.0, out X, out Y, out Z);
            }

            //update the record
            r[3] = lat;
            r[4] = lon;
            r[5] = X;
            r[6] = Y;
            r[7] = Z;

            lock (m_table)
            {
              r.AcceptChanges();
            }

          }
        }
        else
        {
          //add new zip code to the locations list
          DataRow rec = m_locations.NewRow();

          rec[1] = zipCode;
          lock (m_locations)
          {
            m_locations.Rows.Add(rec);
          }

          //need to connect to the service and get the info
          AddWeatherItem(zipCode, lat, lon);
        }

        return true;
      }
      catch (Exception ex)
      {
        System.Diagnostics.Trace.WriteLine(ex.Message);
        return false;
      }
    }

    /// <summary>
    /// delete an item from the dataset
    /// </summary>
    /// <param name="zipCode"></param>
    /// <returns></returns>
    public bool DeleteItem(long zipCode)
    {
      if (null == m_table)
        return false;

      try
      {
        DataRow r = m_table.Rows.Find(zipCode);
        if (null != r) //if the record with this zipCode already exists
        {
          //delete the record
          lock (m_table)
          {
            r.Delete();
            m_table.AcceptChanges();
          }
          return true;
        }
        return false;
      }
      catch (Exception ex)
      {
        System.Diagnostics.Trace.WriteLine(ex.Message);
        return false;
      }
    }

    /// <summary>
    /// get a list of all citynames currently in the dataset.
    /// </summary>
    /// <returns></returns>
    /// <remarks>Please note that since the unique ID is zipCode, it is possible
    /// to have a city name appearing more than once.</remarks>
    public string[] GetCityNames()
    {
      if (null == m_table || 0 == m_table.Rows.Count)
        return null;

      string[] cityNames = new string[m_table.Rows.Count];
      for (int i = 0; i < m_table.Rows.Count; i++)
      {
        //column #2 stores the cityName
        cityNames[i] = Convert.ToString(m_table.Rows[i][2]);
      }

      return cityNames;
    }

    /// <summary>
    /// unselect all weather items
    /// </summary>
    public void UnselectAll()
    {
      if (null == m_table)
        return;

      //unselect all the currently selected items
      lock (m_table)
      {
        foreach (DataRow r in m_table.Rows)
        {
          //13 is the selection column ID
          r[13] = false;
        }
        m_table.AcceptChanges();
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
      if (null == m_table)
        return null;

      DataRow[] rows = m_table.Select("CITYNAME = '" + cityName + "'");
      if (rows.Length == 0)
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
      if (null == r)
        return null;

      IPropertySet propSet = new PropertySetClass();
      propSet.SetProperty("ID", r[0]);
      propSet.SetProperty("ZIPCODE", r[1]);
      propSet.SetProperty("CITYNAME", r[2]);
      propSet.SetProperty("LAT", r[3]);
      propSet.SetProperty("LON", r[4]);
      propSet.SetProperty("TEMPERATURE", r[5]);
      propSet.SetProperty("CONDITION", r[6]);
      propSet.SetProperty("ICONNAME", r[7]);
      propSet.SetProperty("ICONID", r[8]);
      propSet.SetProperty("DAY", r[9]);
      propSet.SetProperty("DATE", r[10]);
      propSet.SetProperty("LOW", r[11]);
      propSet.SetProperty("HIGH", r[12]);
      propSet.SetProperty("UPDATEDATE", r[14]);

      return propSet;
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
      try
      {
        if (!m_bValid || null == m_globeDisplay)
          return null;

        IEnvelope intersectEnv = new EnvelopeClass();
        IEnvelope inEnv;
        IArray array = new ArrayClass();

        //get the envelope from the geometry
        if (pGeom.GeometryType == esriGeometryType.esriGeometryEnvelope)
          inEnv = pGeom.Envelope;
        else
          inEnv = pGeom as IEnvelope;

        if (inEnv.IsEmpty)
          return array;

        double xMin, xMax, yMin, yMax, zMin, zMax;
        inEnv.QueryCoords(out xMin, out yMin, out xMax, out yMax);
        zMin = inEnv.ZMin;
        zMax = inEnv.ZMax;

        //get the middle coordinate of the envelope
        double xC, yC, zC;
        xC = (xMin + xMax) * 0.5;
        yC = (yMin + yMax) * 0.5;
        zC = (zMin + zMax) * 0.5;

        ISceneViewer sceneViewer = m_globeDisplay.ActiveViewer;
        IGlobeViewUtil globeViewUtil = (IGlobeViewUtil)sceneViewer.Camera;
        int winX, winY;
        globeViewUtil.GeographicToWindow(xC, yC, zC * 1000.0, out winX, out winY);
        IHit3DSet hits;

        //locate all the items that falls within the given location
        m_globeDisplay.LocateMultiple(sceneViewer, winX, winY, true, false, false, false, out hits);
        if (null == hits)
          return array;


        IHit3D hit3D = null;
        IPropertySet propSet = null;
        IIdentifyObj idObj = null;
        IIdentifyObject idObject = null;
        IArray objArray = hits.Hits;
        int nCount = objArray.Count;
        bool bIdentify = false;
        //iterate through the hit items and create identify objects
        for (int i = 0; i < nCount; i++)
        {
          hit3D = objArray.get_Element(i) as IHit3D;

          //make sure that the hit object is a propertyset
          propSet = hit3D.Object as IPropertySet;
          if (null != propSet)
          {
            //instantiate the identify object and add it to the array
            idObj = new GlobeWeatherIdentifyObject();
            //test whether the layer can be identified
            bIdentify = idObj.CanIdentify((ILayer)this);
            if (bIdentify)
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
      catch (Exception ex)
      {
        System.Diagnostics.Trace.WriteLine("IIdentify.Identify: " + ex.Message);
        return null;
      }
    }

    #endregion
  }
}