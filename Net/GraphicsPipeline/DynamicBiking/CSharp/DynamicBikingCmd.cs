using System;
using System.IO;
using System.Drawing;
using System.Xml;
using System.Xml.XPath;
using System.Threading;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;

namespace DynamicBiking
{
  /// <summary>
  /// Summary description for DynamicBikingCmd.
  /// </summary>
  [Guid("f01054d2-0130-4124-8436-1bf2942bf2b6")]
  [ClassInterface(ClassInterfaceType.None)]
  [ProgId("DynamicBiking.DynamicBikingCmd")]
  public sealed class DynamicBikingCmd : BaseCommand
  {
    #region COM Registration Function(s)
    [ComRegisterFunction()]
    [ComVisible(false)]
    static void RegisterFunction(Type registerType)
    {
      // Required for ArcGIS Component Category Registrar support
      ArcGISCategoryRegistration(registerType);

      //
      // TODO: Add any COM registration code here
      //
    }

    [ComUnregisterFunction()]
    [ComVisible(false)]
    static void UnregisterFunction(Type registerType)
    {
      // Required for ArcGIS Component Category Registrar support
      ArcGISCategoryUnregistration(registerType);

      //
      // TODO: Add any COM unregistration code here
      //
    }

    #region ArcGIS Component Category Registrar generated code
    /// <summary>
    /// Required method for ArcGIS Component Category registration -
    /// Do not modify the contents of this method with the code editor.
    /// </summary>
    private static void ArcGISCategoryRegistration(Type registerType)
    {
      string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
      ControlsCommands.Register(regKey);

    }
    /// <summary>
    /// Required method for ArcGIS Component Category unregistration -
    /// Do not modify the contents of this method with the code editor.
    /// </summary>
    private static void ArcGISCategoryUnregistration(Type registerType)
    {
      string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
      ControlsCommands.Unregister(regKey);

    }

    #endregion
    #endregion

    #region class members
    private enum GPSPlaybackFormat
    {
      HST = 0,
      GPX = 1,
      XML = 2
    }
    private GPSPlaybackFormat m_playbackFormat = GPSPlaybackFormat.GPX;
    private IHookHelper m_hookHelper;
    private IDynamicMap m_dynamicMap = null;
    private IActiveView m_activeView = null;
    private bool m_bConnected = false;

    private IPoint m_gpsPosition = null;
    private IPoint m_additionalInfoPoint = null;
    private IPointCollection4 m_bikeRouteGeometry = null;
    private IGeometryBridge2 m_geometryBridge = null;
    private WKSPoint[] m_wksPoints = new WKSPoint[1];
    private WKSPoint m_wksPrevPosition;

    private IDynamicSymbolProperties2 m_dynamicSymbolProperties = null;
    private IDynamicCompoundMarker2 m_dynamicCompoundMarker = null;
    private IDynamicScreenDisplay m_dynamicScreenDisplay = null;
    private IDynamicGlyph m_bikeGlyph = null;
    private IDynamicGlyph m_bikeRouteGlyph = null;
    private IDynamicGlyph m_textGlyph = null;
    private IDynamicGlyph m_catGlyph = null;
    private IDynamicGlyph m_gpsGlyph = null;
    private IDynamicGlyph[] m_heartRateGlyph;
		private float m_gpsSymbolScale = 1.0f;
    private double m_heading = 0;
    private string m_heartRateString = string.Empty;
    private string m_altitudeString = string.Empty;
    private string m_speed = string.Empty;
    private bool m_bOnce = true;
    private int m_heartRateCounter = 0;
    private int m_drawCycles = 0;
    public int m_playbackSpeed = 10;
    private bool m_bTrackMode = false;
		string[] nullString = null;

    private string m_xmlPath = string.Empty;
    // xml loader thread stuff
    private Thread m_dataLoaderThread = null;
    private static AutoResetEvent m_autoEvent = new AutoResetEvent(false);
    private int m_bikePositionCount = 0;

    private sealed class BikePositionInfo
    {
      public BikePositionInfo()
      {

      }

      public WKSPoint position;
      public DateTime time;
      public double altitudeMeters;
      public int heartRate;
      public int lapCount;
      public int lapAverageHeartRate;
      public int lapMaximumHeartRate;
      public int lapCalories;
      public double lapMaximumSpeed;
      public double lapDistanceMeters;
      public double course;
      public double speed;
      public int positionCount;
    }
    private BikePositionInfo m_bikePositionInfo = null;

    private struct XmlDocTaksData
    {
      public string xmlDocPath;
    }

    #endregion

    #region class constructor
    public DynamicBikingCmd()
    {
      base.m_category = ".NET Samples";
      base.m_caption = "Dynamic Biking";
      base.m_message = "Dynamic Biking";
      base.m_toolTip = "Dynamic Biking";
      base.m_name = "DynamicBiking_DynamicBikingCmd";

      try
      {
        string bitmapResourceName = GetType().Name + ".bmp";
        base.m_bitmap = new Bitmap(GetType(), bitmapResourceName);
      }
      catch (Exception ex)
      {
        System.Diagnostics.Trace.WriteLine(ex.Message, "Invalid Bitmap.");
      }
    }

    ~DynamicBikingCmd()
    {
      if (m_dataLoaderThread != null && m_dataLoaderThread.ThreadState == ThreadState.Running)
      {
        m_autoEvent.Set();
        m_dataLoaderThread.Join();
      }
    }
    #endregion

    #region Overriden Class Methods

    /// <summary>
    /// Occurs when this command is created
    /// </summary>
    /// <param name="hook">Instance of the application</param>
    public override void OnCreate(object hook)
    {
      if (hook == null)
        return;

      if (m_hookHelper == null)
        m_hookHelper = new HookHelperClass();

      m_hookHelper.Hook = hook;

      m_activeView = m_hookHelper.ActiveView;

      m_geometryBridge = new GeometryEnvironmentClass();

      m_wksPrevPosition.X = 0;
      m_wksPrevPosition.Y = 0;
    }

    /// <summary>
    /// Occurs when this command is clicked
    /// </summary>
    public override void OnClick()
    {
      m_dynamicMap = m_hookHelper.FocusMap as IDynamicMap;
      if (m_dynamicMap == null)
        return;

      if (!m_dynamicMap.DynamicMapEnabled)
      {
        MessageBox.Show("Please enable dynamic mode and try again.");
        return;
      }

      if (!m_bConnected)
      {
        m_xmlPath = GetPlaybackXmlPath();
        if (m_xmlPath == string.Empty)
          return;

        m_bikePositionInfo = new BikePositionInfo();
        m_bikePositionInfo.positionCount = m_bikePositionCount;
        m_bikePositionInfo.altitudeMeters = 0;
        m_bikePositionInfo.time = DateTime.Now;
        m_bikePositionInfo.position.X = 0;
        m_bikePositionInfo.position.Y = 0;
        m_bikePositionInfo.heartRate = 0;
        m_bikePositionInfo.lapCount = 0;
        m_bikePositionInfo.lapAverageHeartRate = 0;
        m_bikePositionInfo.lapMaximumHeartRate = 0;
        m_bikePositionInfo.lapDistanceMeters = 0;
        m_bikePositionInfo.lapMaximumSpeed = 0;
        m_bikePositionInfo.lapCalories = 0;

        m_gpsPosition = new PointClass();
        m_additionalInfoPoint = new PointClass();
        m_additionalInfoPoint.PutCoords(70, 90);
        m_bikeRouteGeometry = new PolylineClass();

        // wire dynamic map events
        ((IDynamicMapEvents_Event)m_dynamicMap).AfterDynamicDraw += new IDynamicMapEvents_AfterDynamicDrawEventHandler(OnAfterDynamicDraw);
        ((IDynamicMapEvents_Event)m_dynamicMap).DynamicMapStarted += new IDynamicMapEvents_DynamicMapStartedEventHandler(OnDynamicMapStarted);

        // spin the thread that plays the data from the xml file
        m_dataLoaderThread = new Thread(new ParameterizedThreadStart(XmlReaderTask));

        XmlDocTaksData taskData;
        taskData.xmlDocPath = m_xmlPath;
        m_dataLoaderThread.Start(taskData);
      }
      else
      {
        // unwire wire dynamic map events
        ((IDynamicMapEvents_Event)m_dynamicMap).AfterDynamicDraw -= new IDynamicMapEvents_AfterDynamicDrawEventHandler(OnAfterDynamicDraw);
        ((IDynamicMapEvents_Event)m_dynamicMap).DynamicMapStarted -= new IDynamicMapEvents_DynamicMapStartedEventHandler(OnDynamicMapStarted);

        // force the bike xml playback thread to quite
        m_autoEvent.Set();
        m_dataLoaderThread.Join();

        System.Diagnostics.Trace.WriteLine("Done!!!");
      }

      m_bConnected = !m_bConnected;
    }

    public override bool Checked
    {
      get
      {
        return m_bConnected;
      }
    }

    #endregion

    #region public properties
    public bool IsPlaying
    {
      get { return m_bConnected; }
    }

    public bool TrackMode
    {
      get { return m_bTrackMode; }
      set { m_bTrackMode = value; }
    }

    public int PlaybackSpeed
    {
      get { return m_playbackSpeed; }
      set { m_playbackSpeed = value; }
    }
    #endregion

    #region Private methods
    private void OnAfterDynamicDraw(esriDynamicMapDrawPhase DynamicMapDrawPhase, IDisplay Display, IDynamicDisplay dynamicDisplay)
    {
			if (DynamicMapDrawPhase != esriDynamicMapDrawPhase.esriDMDPDynamicLayers)
				return;

      // initialize symbology for dynamic drawing
      if (m_bOnce)
      {
        // create the glyphs for the bike position as well as for the route
        IDynamicGlyphFactory2 dynamicGlyphFactory = dynamicDisplay.DynamicGlyphFactory as IDynamicGlyphFactory2;
        IColor whiteTransparentColor = (IColor)ESRI.ArcGIS.ADF.Connection.Local.Converter.ToRGBColor(Color.FromArgb(255, 255, 255));

        Bitmap bitmap = new Bitmap(GetType(), "Icons.bicycle-icon.bmp");
        m_bikeGlyph = dynamicGlyphFactory.CreateDynamicGlyphFromBitmap(esriDynamicGlyphType.esriDGlyphMarker, bitmap.GetHbitmap().ToInt32(), false, whiteTransparentColor);

        bitmap = new Bitmap(GetType(), "Icons.cat.bmp");
        m_catGlyph = dynamicGlyphFactory.CreateDynamicGlyphFromBitmap(esriDynamicGlyphType.esriDGlyphMarker, bitmap.GetHbitmap().ToInt32(), false, whiteTransparentColor);

        bitmap = new Bitmap(GetType(), "Icons.gps.png");
        m_gpsGlyph = dynamicGlyphFactory.CreateDynamicGlyphFromBitmap(esriDynamicGlyphType.esriDGlyphMarker, bitmap.GetHbitmap().ToInt32(), false, whiteTransparentColor);

        ISymbol routeSymbol = CreateBikeRouteSymbol();
        m_bikeRouteGlyph = dynamicGlyphFactory.CreateDynamicGlyph(routeSymbol);

        // create the heart rate glyphs series
        CreateHeartRateAnimationGlyphs(dynamicGlyphFactory);

        // get the default internal text glyph
        m_textGlyph = dynamicGlyphFactory.get_DynamicGlyph(1, esriDynamicGlyphType.esriDGlyphText, 1);

        // do one time casting
        m_dynamicSymbolProperties = dynamicDisplay as IDynamicSymbolProperties2;
        m_dynamicCompoundMarker = dynamicDisplay as IDynamicCompoundMarker2;
        m_dynamicScreenDisplay = dynamicDisplay as IDynamicScreenDisplay;

        m_bOnce = false;
      }

      // draw the trail
      m_dynamicSymbolProperties.set_DynamicGlyph(esriDynamicSymbolType.esriDSymbolLine, m_bikeRouteGlyph);
      m_dynamicSymbolProperties.SetColor(esriDynamicSymbolType.esriDSymbolLine, 1.0f, 1.0f, 1.0f, 1.0f);
      m_dynamicSymbolProperties.SetScale(esriDynamicSymbolType.esriDSymbolLine, 1.0f, 1.0f);
      m_dynamicSymbolProperties.LineContinuePattern = true;
      dynamicDisplay.DrawPolyline(m_bikeRouteGeometry);

      if (m_playbackFormat == GPSPlaybackFormat.HST)
      {
        // adjust the bike lap additional info point to draw at the top left corner of the window
        m_additionalInfoPoint.Y = Display.DisplayTransformation.get_DeviceFrame().bottom - 70;

        // draw additional lap information
        DrawLapInfo(dynamicDisplay);

        // draw the heart-rate and altitude
        DrawHeartRateAnimation(dynamicDisplay, m_gpsPosition);

				// draw the current position as a marker glyph
				m_dynamicSymbolProperties.set_DynamicGlyph(esriDynamicSymbolType.esriDSymbolMarker, m_bikeGlyph);
				m_dynamicSymbolProperties.SetColor(esriDynamicSymbolType.esriDSymbolMarker, 1.0f, 1.0f, 1.0f, 1.0f);
				m_dynamicSymbolProperties.SetScale(esriDynamicSymbolType.esriDSymbolMarker, 1.2f, 1.2f);
				m_dynamicSymbolProperties.set_RotationAlignment(esriDynamicSymbolType.esriDSymbolMarker, esriDynamicSymbolRotationAlignment.esriDSRANorth);
				m_dynamicSymbolProperties.set_Heading(esriDynamicSymbolType.esriDSymbolMarker, (float)(m_heading - 90));
				dynamicDisplay.DrawMarker(m_gpsPosition);
      }
      else
      {
				DrawGPSInfo(dynamicDisplay, m_gpsPosition);
      }

      

    }

    void OnDynamicMapStarted(IDisplay Display, IDynamicDisplay dynamicDisplay)
    {
      lock (m_bikePositionInfo)
      {
        // update the bike position
        if (m_bikePositionInfo.positionCount != m_bikePositionCount)
        {
          // update the geometry
          m_gpsPosition.PutCoords(m_bikePositionInfo.position.X, m_bikePositionInfo.position.Y);

          // check if needed to update the map extent
          if (m_bTrackMode)
          {
            IEnvelope mapExtent = m_hookHelper.ActiveView.ScreenDisplay.DisplayTransformation.FittedBounds;
            mapExtent.CenterAt(m_gpsPosition);
            m_hookHelper.ActiveView.ScreenDisplay.DisplayTransformation.VisibleBounds = mapExtent;
          }

          // update the bike trail
          m_wksPoints[0] = m_bikePositionInfo.position;
          m_geometryBridge.AddWKSPoints(m_bikeRouteGeometry, ref m_wksPoints);

          // get the GPS altitude reading
          m_altitudeString = string.Format("Altitude: {0} m", m_bikePositionInfo.altitudeMeters.ToString("####.#"));

          if (m_playbackFormat == GPSPlaybackFormat.HST)
          {
            // calculate the heading
            m_heading = Math.Atan2(m_bikePositionInfo.position.X - m_wksPrevPosition.X, m_bikePositionInfo.position.Y - m_wksPrevPosition.Y);
            m_heading *= (180 / Math.PI);
            if (m_heading < 0)
              m_heading += 360;

            m_heartRateString = string.Format("{0} BPM", m_bikePositionInfo.heartRate);
          }
          else
          {
            m_heading = m_bikePositionInfo.course;
            m_speed = string.Format("Speed: {0} MPH", m_bikePositionInfo.speed.ToString("###.#"));
          }

          m_wksPrevPosition.X = m_bikePositionInfo.position.X;
          m_wksPrevPosition.Y = m_bikePositionInfo.position.Y;
          m_bikePositionCount = m_bikePositionInfo.positionCount;
        }
      }

      // explicitly call refresh in order to make the dynamic display fire AfterDynamicDraw event 
      m_hookHelper.ActiveView.Refresh();
    }

    private void XmlReaderTask(object data)
    {
      bool bFirst = true;
      DateTime nextTime = DateTime.Now;
      DateTime currentTime = DateTime.Now;
      double lat = 0;
      double lon = 0;
      double altitude = 0;
      double course = 0;
      double speed = 0;
      int heartRate = 0;
      XmlNode trackPoint = null;
      XmlNode nextTrackPointTimeNode = null;

      XmlDocTaksData taskData = (XmlDocTaksData)data;

      XmlDocument bikeDataDoc = new XmlDocument();
      XmlTextReader xmlTextReader = new XmlTextReader(m_xmlPath);
      bikeDataDoc.Load(xmlTextReader);

      XmlElement rootElement = bikeDataDoc.DocumentElement;

      if (m_playbackFormat == GPSPlaybackFormat.HST)
      {
        XmlNodeList laps = rootElement.GetElementsByTagName("Lap");
        foreach (XmlNode lap in laps)
        {
          // get lap average and maximum heart rate
          XmlNode averageHeartRate = ((XmlElement)lap).GetElementsByTagName("AverageHeartRateBpm")[0];
          XmlNode maximumHeartRate = ((XmlElement)lap).GetElementsByTagName("MaximumHeartRateBpm")[0];
          XmlNode calories = ((XmlElement)lap).GetElementsByTagName("Calories")[0];
          XmlNode maxSpeed = ((XmlElement)lap).GetElementsByTagName("MaximumSpeed")[0];
          XmlNode distance = ((XmlElement)lap).GetElementsByTagName("DistanceMeters")[0];

          // update the position info
          lock (m_bikePositionInfo)
          {
            m_bikePositionInfo.lapCount++;
            m_bikePositionInfo.lapAverageHeartRate = Convert.ToInt32(averageHeartRate.InnerText);
            m_bikePositionInfo.lapMaximumHeartRate = Convert.ToInt32(maximumHeartRate.InnerText);
            m_bikePositionInfo.lapCalories = Convert.ToInt32(calories.InnerText);
            m_bikePositionInfo.lapMaximumSpeed = Convert.ToDouble(maxSpeed.InnerText);
            m_bikePositionInfo.lapDistanceMeters = Convert.ToDouble(distance.InnerText);
          }

          XmlNodeList tracks = ((XmlElement)lap).GetElementsByTagName("Track");
          foreach (XmlNode track in tracks)
          {
            XmlNodeList trackpoints = ((XmlElement)track).GetElementsByTagName("Trackpoint");
            // read each time one track point ahead in order to calculate the lag time between 
            // the current track point and the next one. This time will be used as the wait time
            // for the AutoResetEvent.
            foreach (XmlNode nextTrackPoint in trackpoints)
            {
              bool bNextTime = false;
              bool bTime = false;
              bool bPosition = false;
              bool bAltitude = false;
              bool bHeartRate = false;

              // if this is the first node in the first track make it current
              if (bFirst)
              {
                trackPoint = nextTrackPoint;
                bFirst = false;
                continue;
              }

              // get the time from the next point in order to calculate the lag time
              nextTrackPointTimeNode = ((XmlElement)nextTrackPoint).GetElementsByTagName("Time")[0];
              if (nextTrackPointTimeNode == null)
                continue;
              else
              {
                nextTime = Convert.ToDateTime(nextTrackPointTimeNode.InnerText);
                bNextTime = true;
              }

              if (trackPoint.ChildNodes.Count == 4)
              {
                foreach (XmlNode trackPointNode in trackPoint.ChildNodes)
                {

                  if (trackPointNode.Name == "Time")
                  {
                    currentTime = Convert.ToDateTime(trackPointNode.InnerText);
                    bTime = true;
                  }
                  else if (trackPointNode.Name == "Position")
                  {
                    XmlNode latNode = trackPointNode["LatitudeDegrees"];
                    lat = Convert.ToDouble(latNode.InnerText);

                    XmlNode lonNode = trackPointNode["LongitudeDegrees"];
                    lon = Convert.ToDouble(lonNode.InnerText);

                    bPosition = true;
                  }
                  else if (trackPointNode.Name == "AltitudeMeters")
                  {
                    altitude = Convert.ToDouble(trackPointNode.InnerText);
                    bAltitude = true;
                  }
                  else if (trackPointNode.Name == "HeartRateBpm")
                  {
                    heartRate = Convert.ToInt32(trackPointNode.InnerText);
                    bHeartRate = true;
                  }
                }

                if (bNextTime && bTime && bPosition && bAltitude && bHeartRate)
                {

                  TimeSpan ts = nextTime - currentTime;

                  lock (m_bikePositionInfo)
                  {
                    m_bikePositionInfo.position.X = lon;
                    m_bikePositionInfo.position.Y = lat;
                    m_bikePositionInfo.altitudeMeters = altitude;
                    m_bikePositionInfo.heartRate = heartRate;
                    m_bikePositionInfo.time = currentTime;
                    m_bikePositionInfo.positionCount++;
                  }

                  // wait for the duration of the time span or bail out if the thread was signaled
                  if (m_autoEvent.WaitOne((int)(ts.TotalMilliseconds / m_playbackSpeed), true))
                  {
                    return;
                  }
                }
              }

              // make the next track point current
              trackPoint = nextTrackPoint;
            }
          }
        }
      }
      else // GPX
      {
        XmlNodeList trackpoints = bikeDataDoc.DocumentElement.GetElementsByTagName("trkpt");

        // read each time one track point ahead in order to calculate the lag time between 
        // the current track point and the next one. This time will be used as the wait time
        // for the AutoResetEvent.
        foreach (XmlNode nextTrackPoint in trackpoints)
        {
          // if this is the first node in the first track make it current
          if (bFirst)
          {
            trackPoint = nextTrackPoint;
            bFirst = false;
            continue;
          }

          // get the time from the next point in order to calculate the lag time
          nextTrackPointTimeNode = ((XmlElement)nextTrackPoint).GetElementsByTagName("time")[0];
          if (nextTrackPointTimeNode == null)
            continue;
          else
          {
            nextTime = Convert.ToDateTime(nextTrackPointTimeNode.InnerText);
          }

          lat = Convert.ToDouble(trackPoint.Attributes["lat"].InnerText);
          lon = Convert.ToDouble(trackPoint.Attributes["lon"].InnerText);

          foreach (XmlNode trackPointNode in trackPoint.ChildNodes)
          {
            if (trackPointNode.Name == "time")
            {
              currentTime = Convert.ToDateTime(trackPointNode.InnerText);
            }
            else if (trackPointNode.Name == "ele")
            {
              altitude = Convert.ToDouble(trackPointNode.InnerText);
            }
            else if (trackPointNode.Name == "course")
            {
              course = Convert.ToDouble(trackPointNode.InnerText);
            }
            else if (trackPointNode.Name == "speed")
            {
              speed = Convert.ToDouble(trackPointNode.InnerText);
            }
          }          

          TimeSpan ts = nextTime - currentTime;

          lock (m_bikePositionInfo)
          {
            m_bikePositionInfo.position.X = lon;
            m_bikePositionInfo.position.Y = lat;
            m_bikePositionInfo.altitudeMeters = altitude;
            m_bikePositionInfo.time = currentTime;
            m_bikePositionInfo.course = course;
            m_bikePositionInfo.speed = speed;
            m_bikePositionInfo.positionCount++;
          }

          // wait for the duration of the time span or bail out if the thread was signaled
          if (m_autoEvent.WaitOne((int)(ts.TotalMilliseconds / m_playbackSpeed), true))
          {
            return;
          }

          // make the next track point current
          trackPoint = nextTrackPoint;
        }
      }

      // close the reader when done
      xmlTextReader.Close();
    }

    private ISymbol CreateBikeRouteSymbol()
    {
        IColor color = (IColor)ESRI.ArcGIS.ADF.Connection.Local.Converter.ToRGBColor(Color.FromArgb(0, 90, 250));
      ICharacterMarkerSymbol charMarkerSymbol = new CharacterMarkerSymbolClass();
      charMarkerSymbol.Color = color;
      charMarkerSymbol.Font = ESRI.ArcGIS.ADF.Connection.Local.Converter.ToStdFont(new Font("ESRI Default Marker", 17.0f, FontStyle.Bold));
      charMarkerSymbol.CharacterIndex = 189;
      charMarkerSymbol.Size = 17;

      IMarkerLineSymbol markerLineSymbol = new MarkerLineSymbolClass();
      markerLineSymbol.Color = color;
      markerLineSymbol.Width = 17.0;
      markerLineSymbol.MarkerSymbol = (IMarkerSymbol)charMarkerSymbol;

      // Makes a new Cartographic Line symbol and sets its properties
      ICartographicLineSymbol cartographicLineSymbol = markerLineSymbol as ICartographicLineSymbol;

      // In order to set additional properties like offsets and dash patterns we must create an ILineProperties object
      ILineProperties lineProperties = cartographicLineSymbol as ILineProperties;
      lineProperties.Offset = 0;

      // Here's how to do a template for the pattern of marks and gaps
      double[] hpe = new double[4];
      hpe[0] = 0;
      hpe[1] = 39;
      hpe[2] = 1;
      hpe[3] = 0;

      ITemplate template = new TemplateClass();
      template.Interval = 1;
      for (int i = 0; i < hpe.Length; i = i + 2)
      {
        template.AddPatternElement(hpe[i], hpe[i + 1]);
      }
      lineProperties.Template = template;

      // Set the basic and cartographic line properties
      cartographicLineSymbol.Color = color;

      color = (IColor)ESRI.ArcGIS.ADF.Connection.Local.Converter.ToRGBColor(Color.FromArgb(0, 220, 100));

      // create a simple line
      ISimpleLineSymbol simpleLineSymbol = new SimpleLineSymbolClass();
      simpleLineSymbol.Color = color;
      simpleLineSymbol.Style = esriSimpleLineStyle.esriSLSSolid;
      simpleLineSymbol.Width = 1.2;

      IMultiLayerLineSymbol multiLayerLineSymbol = new MultiLayerLineSymbolClass();
      multiLayerLineSymbol.AddLayer((ILineSymbol)cartographicLineSymbol);
      multiLayerLineSymbol.AddLayer((ILineSymbol)simpleLineSymbol);

      return multiLayerLineSymbol as ISymbol;
    }

    private void CreateHeartRateAnimationGlyphs(IDynamicGlyphFactory2 dynamicGlyphFactory)
    {
        IColor whiteTransparentColor = (IColor)ESRI.ArcGIS.ADF.Connection.Local.Converter.ToRGBColor(Color.FromArgb(255, 255, 255));

      m_heartRateGlyph = new IDynamicGlyph[5];
      string heartRateIcon;
      string heartIconBaseName = "Icons.valentine-heart";
      Bitmap bitmap = null;
      int imagesize = 16;
      for (int i = 0; i < 5; i++)
      {
        heartRateIcon = heartIconBaseName + imagesize + ".bmp";
        bitmap = new Bitmap(GetType(), heartRateIcon);
        m_heartRateGlyph[i] = dynamicGlyphFactory.CreateDynamicGlyphFromBitmap(esriDynamicGlyphType.esriDGlyphMarker, bitmap.GetHbitmap().ToInt32(), false, whiteTransparentColor);
        m_heartRateGlyph[i].SetAnchor(20.0f, -40.0f);
        imagesize += 2;
      }
    }

    private void DrawHeartRateAnimation(IDynamicDisplay dynamicDisplay, IPoint bikePoint)
    {
      m_dynamicSymbolProperties.set_DynamicGlyph(esriDynamicSymbolType.esriDSymbolMarker, m_heartRateGlyph[m_heartRateCounter]);
      m_dynamicSymbolProperties.SetColor(esriDynamicSymbolType.esriDSymbolMarker, 1.0f, 1.0f, 1.0f, 1.0f);
      m_dynamicSymbolProperties.SetScale(esriDynamicSymbolType.esriDSymbolMarker, 1.0f, 1.0f);
      m_dynamicSymbolProperties.set_RotationAlignment(esriDynamicSymbolType.esriDSymbolMarker, esriDynamicSymbolRotationAlignment.esriDSRAScreen);
      m_dynamicSymbolProperties.set_Heading(esriDynamicSymbolType.esriDSymbolMarker, 0.0f);
      dynamicDisplay.DrawMarker(bikePoint);

      m_textGlyph.SetAnchor(-35.0f, -50.0f);
      m_dynamicSymbolProperties.set_DynamicGlyph(esriDynamicSymbolType.esriDSymbolText, m_textGlyph);
      m_dynamicSymbolProperties.TextBoxUseDynamicFillSymbol = true;
      m_dynamicSymbolProperties.SetColor(esriDynamicSymbolType.esriDSymbolText, 0.0f, 0.8f, 0.0f, 1.0f);
      m_dynamicSymbolProperties.SetColor(esriDynamicSymbolType.esriDSymbolFill, 0.0f, 0.0f, 0.0f, 1.0f);
      m_dynamicSymbolProperties.TextBoxHorizontalAlignment = esriTextHorizontalAlignment.esriTHALeft;
      dynamicDisplay.DrawText(bikePoint, m_heartRateString);

      m_textGlyph.SetAnchor(-20.0f, -30.0f);
      m_dynamicSymbolProperties.set_DynamicGlyph(esriDynamicSymbolType.esriDSymbolText, m_textGlyph);
      dynamicDisplay.DrawText(bikePoint, m_altitudeString);

      if (m_drawCycles % 5 == 0)
      {
        m_heartRateCounter++;

        if (m_heartRateCounter > 4)
          m_heartRateCounter = 0;
      }

      m_drawCycles++;
      if (m_drawCycles == 5)
        m_drawCycles = 0;
    }

		private void DrawGPSInfo(IDynamicDisplay dynamicDisplay, IPoint gpsPosition)
		{

			// altitude is already available
			string course;
			string speed;

			lock (m_bikePositionInfo)
			{
				course = string.Format("Course {0} DEG", m_bikePositionInfo.course.ToString("###.##"));
				speed = string.Format("Speed {0} MPH", m_bikePositionInfo.speed.ToString("###.##"));
			}

			string gpsInfo = string.Format("{0}\n{1}\n{2}", course, speed, m_altitudeString);

		  m_textGlyph.SetAnchor(-35.0f, -47.0f);
			m_dynamicSymbolProperties.set_DynamicGlyph(esriDynamicSymbolType.esriDSymbolText, m_textGlyph);
			m_dynamicSymbolProperties.TextBoxUseDynamicFillSymbol = true;
			m_dynamicSymbolProperties.set_Heading(esriDynamicSymbolType.esriDSymbolText, 0.0f);
			m_dynamicSymbolProperties.SetColor(esriDynamicSymbolType.esriDSymbolText, 0.0f, 0.8f, 0.0f, 1.0f);
			m_dynamicSymbolProperties.SetScale(esriDynamicSymbolType.esriDSymbolText, 1.0f, 1.0f);
			m_dynamicSymbolProperties.SetColor(esriDynamicSymbolType.esriDSymbolFill, 0.0f, 0.0f, 0.0f, 0.6f);
			m_dynamicSymbolProperties.TextBoxHorizontalAlignment = esriTextHorizontalAlignment.esriTHALeft;
			dynamicDisplay.DrawText(m_gpsPosition, gpsInfo);


			m_dynamicSymbolProperties.set_DynamicGlyph(esriDynamicSymbolType.esriDSymbolMarker, m_gpsGlyph);
			m_dynamicSymbolProperties.SetColor(esriDynamicSymbolType.esriDSymbolMarker, 1.0f, 1.0f, 1.0f, 1.0f);
			m_dynamicSymbolProperties.SetScale(esriDynamicSymbolType.esriDSymbolMarker, m_gpsSymbolScale, m_gpsSymbolScale);
			m_dynamicSymbolProperties.set_RotationAlignment(esriDynamicSymbolType.esriDSymbolMarker, esriDynamicSymbolRotationAlignment.esriDSRANorth);
			m_dynamicSymbolProperties.set_Heading(esriDynamicSymbolType.esriDSymbolMarker, (float)(m_heading - 90));
			dynamicDisplay.DrawMarker(m_gpsPosition);

			if (m_drawCycles % 5 == 0)
			{
				// increment the symbol size
				m_gpsSymbolScale += 0.05f;

				if (m_gpsSymbolScale > 1.2f)
					m_gpsSymbolScale = 0.8f;
			}

			m_drawCycles++;
			if (m_drawCycles == 5)
				m_drawCycles = 0;
		}

    private void DrawLapInfo(IDynamicDisplay dynamicDisplay)
    {
      string lapCount;
      string lapInfo;
      string lapHeartRateInfo;

      lock (m_bikePositionInfo)
      {
        lapCount = string.Format("Lap #{0}", m_bikePositionInfo.lapCount);
        lapInfo = string.Format("Lap information:\nDistance: {0}m\nMaximum speed - {1}\nCalories - {2}", m_bikePositionInfo.lapDistanceMeters.ToString("#####.#"), m_bikePositionInfo.lapMaximumSpeed.ToString("###.#"), m_bikePositionInfo.lapCalories);
        lapHeartRateInfo = string.Format("Lap heart rate info:\nAverage - {0}\nMaximum - {1}", m_bikePositionInfo.lapAverageHeartRate, m_bikePositionInfo.lapMaximumHeartRate);
      }

      m_dynamicSymbolProperties.set_DynamicGlyph(esriDynamicSymbolType.esriDSymbolMarker, m_catGlyph);
      m_dynamicSymbolProperties.SetColor(esriDynamicSymbolType.esriDSymbolMarker, 1.0f, 1.0f, 1.0f, 1.0f);
      m_dynamicSymbolProperties.SetScale(esriDynamicSymbolType.esriDSymbolMarker, 1.0f, 1.0f);
      m_dynamicSymbolProperties.set_RotationAlignment(esriDynamicSymbolType.esriDSymbolMarker, esriDynamicSymbolRotationAlignment.esriDSRAScreen);
      m_dynamicSymbolProperties.set_Heading(esriDynamicSymbolType.esriDSymbolMarker, 0.0f);

      m_dynamicSymbolProperties.TextBoxUseDynamicFillSymbol = true;
      m_dynamicSymbolProperties.SetColor(esriDynamicSymbolType.esriDSymbolText, 0.0f, 0.8f, 0.0f, 1.0f);
      m_dynamicSymbolProperties.SetColor(esriDynamicSymbolType.esriDSymbolFill, 0.0f, 0.0f, 0.0f, 1.0f);
      m_dynamicSymbolProperties.TextBoxHorizontalAlignment = esriTextHorizontalAlignment.esriTHALeft;
      m_textGlyph.SetAnchor(0.0f, 0.0f);
      m_dynamicSymbolProperties.set_DynamicGlyph(esriDynamicSymbolType.esriDSymbolText, m_textGlyph);
			string[] strLapInfo = new string[] { lapCount, lapInfo, lapHeartRateInfo };
			m_dynamicCompoundMarker.DrawScreenArrayMarker(m_additionalInfoPoint, ref nullString, ref nullString, ref strLapInfo, ref nullString, ref nullString);

    }

    private string GetPlaybackXmlPath()
    {
      OpenFileDialog ofd = new OpenFileDialog();
      ofd.CheckFileExists = true;
      ofd.Multiselect = false;
			ofd.Filter = "HST files (*.hst)|*.hst|GPX files (*.gpx)|*.gpx|XML files (*.xml)|*.xml";
      ofd.RestoreDirectory = true;
      ofd.Title = "Input biking log file";

      DialogResult result = ofd.ShowDialog();
      if (result == DialogResult.OK)
      {
        string fileExtension = System.IO.Path.GetExtension(ofd.FileName).ToUpper();
        if (fileExtension == ".GPX")
          m_playbackFormat = GPSPlaybackFormat.GPX;
        else if (fileExtension == ".HST")
          m_playbackFormat = GPSPlaybackFormat.HST;
        else if (fileExtension == ".XML")
          m_playbackFormat = GPSPlaybackFormat.XML;

        return ofd.FileName;
      }

      return string.Empty;
    }
    #endregion
  }
}