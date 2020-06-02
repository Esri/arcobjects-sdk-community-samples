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
using System.Xml;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ESRI.ArcGIS.Controls;
using System.Collections.Generic;
using Microsoft.Win32;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS;

namespace DynamicObjectTracking
{

  /// <summary>
  /// A user defined data structure
  /// </summary>
  public struct NavigationData
  {
    public double X;
    public double Y;
    public double Azimuth;

    /// <summary>
    /// struct constructor
    /// </summary>
    /// <param name="x">map x coordinate</param>
    /// <param name="y">map x coordinate</param>
    /// <param name="azimuth">the new map azimuth</param>
    public NavigationData(double x, double y, double azimuth)
    {
      X = x;
      Y = y;
      Azimuth = azimuth;
    }
  }

  /// <summary>
  /// This command triggers the tracking functionality using Dynamic Display
  /// </summary>
  [Guid("803D4188-AB2F-49f9-9340-42C809887063")]
  [ComVisible(true)]
  [ClassInterface(ClassInterfaceType.None)]
  [ProgId("DynamicObjectTracking.TrackObject")]
  public sealed class TrackObject : BaseCommand
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
    #endregion

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

    #region class members
    private IHookHelper m_hookHelper = null;
    private IDynamicMap m_dynamicMap = null;
    private IPoint m_point = null;
    private IDynamicGlyph m_VWmarkerGlyph = null;
    private IDynamicGlyphFactory2 m_dynamicGlyphFactory = null;
    private IDynamicSymbolProperties2 m_dynamicSymbolProps = null;
    private bool m_bDrawOnce = true;
    private IDisplayTransformation m_displayTransformation = null;
    private List<WKSPoint> m_points = null;
    private static int m_pointIndex = 0;
    private bool m_bIsRunning = false;
    private bool m_bOnce = true;
    private string m_VWFileName = string.Empty;
    private string m_navigationDataFileName = string.Empty;
    private NavigationData m_navigationData;
    #endregion

    /// <summary>
    /// class constructor
    /// </summary>
    public TrackObject()
    {
      base.m_category = ".NET Samples";
      base.m_caption = "Track Dynamic Object";
      base.m_message = "Tracking a dynamic object";
      base.m_toolTip = "Tracking a dynamic object";
      base.m_name = "DotNetSamples.TrackDynamicObject";

      try
      {
        string bitmapResourceName = GetType().Name + ".bmp";
        base.m_bitmap = new Bitmap(GetType(), bitmapResourceName);
      }
      catch (Exception ex)
      {
        System.Diagnostics.Trace.WriteLine(ex.Message, "Invalid Bitmap");
      }
    }

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

      m_point = new PointClass();
      m_points = new List<WKSPoint>();
    }

    /// <summary>
    /// Occurs when this command is clicked
    /// </summary>
    public override void OnClick()
    {
      //make sure to switch into dynamic mode
      m_dynamicMap = (IDynamicMap)m_hookHelper.FocusMap;
      if (!m_dynamicMap.DynamicMapEnabled)
        m_dynamicMap.DynamicMapEnabled = true;

      //do initializations
      if (m_bOnce)
      {
        //generate the navigation data
        GenerateNavigationData();

        m_displayTransformation = m_hookHelper.ActiveView.ScreenDisplay.DisplayTransformation;

        m_bOnce = false;
      }

      //hook the dynamic display events
      if (!m_bIsRunning)
      {
        ((IDynamicMapEvents_Event)m_dynamicMap).DynamicMapFinished += new IDynamicMapEvents_DynamicMapFinishedEventHandler(OnTimerElapsed);
        ((IDynamicMapEvents_Event)m_dynamicMap).AfterDynamicDraw += new IDynamicMapEvents_AfterDynamicDrawEventHandler(OnAfterDynamicDraw);
      }
      else
      {
        ((IDynamicMapEvents_Event)m_dynamicMap).DynamicMapFinished -= new IDynamicMapEvents_DynamicMapFinishedEventHandler(OnTimerElapsed);
        ((IDynamicMapEvents_Event)m_dynamicMap).AfterDynamicDraw -= new IDynamicMapEvents_AfterDynamicDrawEventHandler(OnAfterDynamicDraw);
      }

      //set the running flag
      m_bIsRunning = !m_bIsRunning;
    }

    /// <summary>
    /// set the state of the button of the command
    /// </summary>
    public override bool Checked
    {
      get
      {
        return m_bIsRunning;
      }
    }

    #endregion

    private void OnTimerElapsed(IDisplay Display, IDynamicDisplay dynamicDisplay)
    {
      try
      {
        //make sure that the current tracking point index does not exceed the list index
        if (m_pointIndex == (m_points.Count - 1))
        {
          m_pointIndex = 0;
          return;
        }

        //get the current and the next track location
        WKSPoint currentPoint = m_points[m_pointIndex];
        WKSPoint nextPoint = m_points[m_pointIndex + 1];

        //calculate the azimuth to the next location
        double azimuth = (180.0 / Math.PI) * Math.Atan2(nextPoint.X - currentPoint.X, nextPoint.Y - currentPoint.Y);

        //set the navigation data structure
        m_navigationData.X = currentPoint.X;
        m_navigationData.Y = currentPoint.Y;
        m_navigationData.Azimuth = azimuth;

        //update the map extent and rotation
        CenterMap(m_navigationData);

        //increment the tracking point index
        m_pointIndex++;
      }
      catch (Exception ex)
      {
        System.Diagnostics.Trace.WriteLine(ex.Message);
      }
    }

    private void CenterMap(NavigationData navigationData)
    {
      try
      {
        //get the current map visible extent
        IEnvelope envelope = m_displayTransformation.VisibleBounds;
        if (null == m_point)
        {
          m_point = new PointClass();
        }
        //set new map center coordinate
        m_point.PutCoords(navigationData.X, navigationData.Y);
        //center the map
        envelope.CenterAt(m_point);
        m_displayTransformation.VisibleBounds = envelope;
        //rotate the map to new angle
        m_displayTransformation.Rotation = navigationData.Azimuth;
      }
      catch (Exception ex)
      {
        System.Diagnostics.Trace.WriteLine(ex.Message);
      }
    }

    private void GenerateNavigationData()
    {
      try
      {
        string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        //get navigationData.xml file from DeveloperKit
        m_navigationDataFileName = System.IO.Path.Combine(path, @"ArcGIS\data\USAMajorHighways\NavigationData.xml");
        if (!System.IO.File.Exists(m_navigationDataFileName))
        {
          throw new Exception("File " + m_navigationDataFileName + " cannot be found!");
        }

        XmlTextReader reader = new XmlTextReader(m_navigationDataFileName);

        XmlDocument doc = new XmlDocument();
        doc.Load(reader);

        reader.Close();

        double X;
        double Y;
        //get the navigation items
        XmlNodeList nodes = doc.DocumentElement.SelectNodes("./navigationItem");
        foreach (XmlNode node in nodes)
        {
          X = Convert.ToDouble(node.Attributes[0].Value);
          Y = Convert.ToDouble(node.Attributes[1].Value);

          WKSPoint p = new WKSPoint();
          p.X = X;
          p.Y = Y;
          m_points.Add(p);
        }
      }
      catch (Exception ex)
      {
        System.Diagnostics.Trace.WriteLine(ex.Message);
      }
    }

    void OnAfterDynamicDraw(esriDynamicMapDrawPhase DynamicMapDrawPhase, IDisplay Display, IDynamicDisplay dynamicDisplay)
    {
      if (DynamicMapDrawPhase != esriDynamicMapDrawPhase.esriDMDPDynamicLayers)
        return;

      if (m_bDrawOnce)
      {
        //cast the DynamicDisplay into DynamicGlyphFactory
        m_dynamicGlyphFactory = dynamicDisplay.DynamicGlyphFactory as IDynamicGlyphFactory2;
        //cast the DynamicDisplay into DynamicSymbolProperties
        m_dynamicSymbolProps = dynamicDisplay as IDynamicSymbolProperties2;

        //create the VW dynamic marker glyph from the embedded bitmap resource
        Bitmap bitmap = new Bitmap(GetType(), "VW.bmp");
        //get bitmap handler
        int hBmp = bitmap.GetHbitmap().ToInt32();
        //set white transparency color
        IColor whiteTransparencyColor = ESRI.ArcGIS.ADF.Connection.Local.Converter.ToRGBColor(Color.FromArgb(0, 0, 0)) as IColor;

        //get the VM dynamic marker glyph
        m_VWmarkerGlyph = m_dynamicGlyphFactory.CreateDynamicGlyphFromBitmap(esriDynamicGlyphType.esriDGlyphMarker, hBmp, false, whiteTransparencyColor);

        m_bDrawOnce = false;
      }

      //set the symbol alignment so that it will align with towards the symbol heading
      m_dynamicSymbolProps.set_RotationAlignment(esriDynamicSymbolType.esriDSymbolMarker, esriDynamicSymbolRotationAlignment.esriDSRANorth);

      //set the symbol's properties
      m_dynamicSymbolProps.set_DynamicGlyph(esriDynamicSymbolType.esriDSymbolMarker, m_VWmarkerGlyph);
      m_dynamicSymbolProps.SetScale(esriDynamicSymbolType.esriDSymbolMarker, 1.3f, 1.3f);
      m_dynamicSymbolProps.SetColor(esriDynamicSymbolType.esriDSymbolMarker, 1.0f, 1.0f, 0.0f, 1.0f); // yellow

      //set the heading of the current symbol
      m_dynamicSymbolProps.set_Heading(esriDynamicSymbolType.esriDSymbolMarker, (float)m_navigationData.Azimuth);

      //draw the current location
      dynamicDisplay.DrawMarker(m_point);
    }
  }
}
