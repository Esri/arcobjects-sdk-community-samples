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
using System.Drawing;
using System.Runtime.InteropServices;
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
using ESRI.ArcGIS.GlobeCore;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Analyst3D;

namespace GlobeDynamicObjectTracking
{
  /// <summary>
  /// This command demonstrates tracking dynamic object in ArcGlobe/GlobeControl with the camera
  /// </summary>
  [Guid("DCB871A1-390A-456f-8A0D-9FDB6A20F721")]
  [ClassInterface(ClassInterfaceType.None)]
  [ProgId("GlobeControlApp.TrackDynamicObject")]
  public sealed class TrackDynamicObject : BaseCommand, IDisposable
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
      GMxCommands.Register(regKey);
      ControlsCommands.Register(regKey);

    }
    /// <summary>
    /// Required method for ArcGIS Component Category unregistration -
    /// Do not modify the contents of this method with the code editor.
    /// </summary>
    private static void ArcGISCategoryUnregistration(Type registerType)
    {
      string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
      GMxCommands.Unregister(regKey);
      ControlsCommands.Unregister(regKey);

    }

    #endregion
    #endregion

    //class members
    private IGlobeHookHelper        m_globeHookHelper     = null;
    private IGlobeDisplay           m_globeDisplay        = null;
    private ISceneViewer            m_sceneViwer          = null;
    private IGlobeGraphicsLayer     m_globeGraphicsLayer  = null;
    private IRealTimeFeedManager    m_realTimeFeedManager = null;
    private IRealTimeFeed           m_realTimeFeed        = null;
    private bool                    m_bConnected          = false;
    private bool                    m_bTrackAboveTarget   = true;
    private bool                    m_once                = true;
    private int                     m_trackObjectIndex    = -1;
    private string                  m_shapefileName       = string.Empty;


    #region Ctor

    /// <summary>
    /// Class Ctor
    /// </summary>
    public TrackDynamicObject()
    {
      base.m_category = ".NET Samples";
      base.m_caption = "Track Dynamic Object";
      base.m_message = "Tracking a dynamic object";
      base.m_toolTip = "Track Dynamic Object";
      base.m_name = base.m_category + "_" + base.m_caption;

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
    #endregion

    #region Overriden Class Methods

    /// <summary>
    /// Occurs when this command is created
    /// </summary>
    /// <param name="hook">Instance of the application</param>
    public override void OnCreate(object hook)
    {
      //initialize the hook-helper
      if (m_globeHookHelper == null)
        m_globeHookHelper = new GlobeHookHelper();

      //set the hook
      m_globeHookHelper.Hook = hook;

      
      //get the ArcGIS path from the registry
      RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\ESRI\ArcObjectsSDK10.2");
      string path = Convert.ToString(key.GetValue("InstallDir"));

      //set the path to the featureclass used by the GPS simulator
      m_shapefileName = System.IO.Path.Combine(path, "Samples\\data\\USAMajorHighways\\usa_major_highways.shp");

      //get the GlobeDisplsy from the hook helper
      m_globeDisplay = m_globeHookHelper.GlobeDisplay;

      //initialize the real-time manager
      if (null == m_realTimeFeedManager)
        m_realTimeFeedManager = new RealTimeFeedManagerClass();

      //use the built in simulator of the real-time manager
      m_realTimeFeedManager.RealTimeFeed = m_realTimeFeedManager.RealTimeFeedSimulator as IRealTimeFeed;

      m_realTimeFeed = m_realTimeFeedManager.RealTimeFeed;
    }
    
    /// <summary>
    /// Occurs when this command is clicked
    /// </summary>
    public override void OnClick()
    {
      try
      {

        if (!m_bConnected)
        {
          //show the tracking type selection dialog (whether to track the element from above or follow it from behind)
          TrackSelectionDlg dlg = new TrackSelectionDlg();
          if (System.Windows.Forms.DialogResult.OK != dlg.ShowDialog())
            return;

          //get the required tracking mode
          m_bTrackAboveTarget = dlg.UseOrthoTrackingMode;

          //do only once initializations
          if (m_once)
          {
            //create the graphics layer to manage the dynamic object
            m_globeGraphicsLayer = new GlobeGraphicsLayerClass();
            ((ILayer)m_globeGraphicsLayer).Name = "DynamicObjects";

            IScene scene = (IScene)m_globeDisplay.Globe;

            //add the new graphic layer to the globe
            scene.AddLayer((ILayer)m_globeGraphicsLayer, false);

            //activate the graphics layer
            scene.ActiveGraphicsLayer = (ILayer)m_globeGraphicsLayer;

            //redraw the GlobeDisplay
            m_globeDisplay.RefreshViewers();

            //open a polyline featurelayer that would serve the real-time feed GPS simulator
            IFeatureLayer featureLayer = GetFeatureLayer();
            if (featureLayer == null)
              return;
            
            //assign the featurelayer to the GPS simulator
            m_realTimeFeedManager.RealTimeFeedSimulator.FeatureLayer = featureLayer;

            m_once = false;
          }

          //get the GlobeViewUtil which is needed for coordinate transformations
          m_sceneViwer = m_globeDisplay.ActiveViewer;

          //Set the globe mode to terrain mode, since otherwise it will not be possible to set the target position
         ((IGlobeCamera)m_sceneViwer.Camera).OrientationMode = esriGlobeCameraOrientationMode.esriGlobeCameraOrientationLocal;


          //set the simulator elapsed time
          m_realTimeFeedManager.RealTimeFeedSimulator.TimeIncrement = 0.1; //sec

          //wire the real-time feed PositionUpdate event
          ((IRealTimeFeedEvents_Event)m_realTimeFeed).PositionUpdated += new IRealTimeFeedEvents_PositionUpdatedEventHandler(OnPositionUpdated);

          //start the real-time listener
          m_realTimeFeed.Start();
        }
        else
        {
          //stop the real-time listener
          m_realTimeFeed.Stop();

          //un-wire the PositionUpdated event handler
          ((IRealTimeFeedEvents_Event)m_realTimeFeed).PositionUpdated -= new IRealTimeFeedEvents_PositionUpdatedEventHandler(OnPositionUpdated);
        }

        //switch the connection flag
        m_bConnected = !m_bConnected;
      }
      catch (Exception ex)
      {
        System.Diagnostics.Trace.WriteLine(ex.Message);
      }
    }

    /// <summary>
    /// The Checked property indicates the state of this Command. 
    /// </summary>
    /// <remarks>If a command item appears depressed on a commandbar, the command is checked.</remarks>
    public override bool Checked
    {
      get
      {
        return m_bConnected;
      }
    }

    #endregion

    #region helper methods

    /// <summary>
    /// get a featurelayer that would be used by the real-time simulator
    /// </summary>
    /// <returns></returns>
    private IFeatureLayer GetFeatureLayer()
    {
      //instantiate a new featurelayer
      IFeatureLayer featureLayer = new FeatureLayerClass();     

      //set the layer's name
      featureLayer.Name = "GPS Data";

      //open the featureclass
      IFeatureClass featureClass = OpenFeatureClass();
      if (featureClass == null)
        return null;

      //set the featurelayer featureclass
      featureLayer.FeatureClass = featureClass;

      //return the featurelayer
      return featureLayer;
    }

    /// <summary>
    /// Opens a shapefile polyline featureclass
    /// </summary>
    /// <returns></returns>
    private IFeatureClass OpenFeatureClass()
    {

      string fileName = System.IO.Path.GetFileNameWithoutExtension(m_shapefileName);
      
      //instantiate a new workspace factory
      IWorkspaceFactory workspaceFactory = new ShapefileWorkspaceFactoryClass();

      //get the workspace directory
      string path = System.IO.Path.GetDirectoryName(m_shapefileName);

      //open the workspace containing the featureclass
      IFeatureWorkspace featureWorkspace = workspaceFactory.OpenFromFile(path, 0) as IFeatureWorkspace;

      //open the featureclass
      IFeatureClass featureClass = featureWorkspace.OpenFeatureClass(fileName);

      //make sure that the featureclass type is polyline
      if (featureClass.ShapeType != esriGeometryType.esriGeometryPolyline)
      {
        featureClass = null;
      }

      //return the featureclass
      return featureClass;
    }

    /// <summary>
    /// Adds a sphere element to the given graphics layer at the specified position
    /// </summary>
    /// <param name="globeGraphicsLayer"></param>
    /// <param name="position"></param>
    /// <returns></returns>
    private int AddTrackElement(IGlobeGraphicsLayer globeGraphicsLayer, esriGpsPositionInfo position)
    {
      if (null == globeGraphicsLayer)
        return -1;

      //create a new point at the given position
      IPoint point = new PointClass();
      ((IZAware)point).ZAware = true;
      point.X = position.longitude;
      point.Y = position.latitude;
      point.Z = 0.0;

      //set the color for the element (red)
      IRgbColor color = new RgbColorClass();
      color.Red = 255;
      color.Green = 0;
      color.Blue = 0;

      //create a new 3D marker symbol
      IMarkerSymbol markerSymbol = new SimpleMarker3DSymbolClass();

      //set the marker symbol's style and resolution
      ((ISimpleMarker3DSymbol)markerSymbol).Style = esriSimple3DMarkerStyle.esriS3DMSSphere;
      ((ISimpleMarker3DSymbol)markerSymbol).ResolutionQuality = 1.0;

      //set the symbol's size and color
      markerSymbol.Size = 700;
      markerSymbol.Color = color as IColor;

      //crate the graphic element
      IElement trackElement = new MarkerElementClass();

      //set the element's symbol and geometry (location and shape)
      ((IMarkerElement)trackElement).Symbol = markerSymbol;
      trackElement.Geometry = point as IPoint;


      //add the element to the graphics layer
      int elemIndex = 0;
      ((IGraphicsContainer)globeGraphicsLayer).AddElement(trackElement, 0);

      //get the element's index
      globeGraphicsLayer.FindElementIndex(trackElement, out elemIndex);
      return elemIndex;
    }

    /// <summary>
    /// The real-time feed position updated event handler
    /// </summary>
    /// <param name="position">a GPS position information</param>
    /// <param name="estimate">indicates whether this is an estimated time or real time</param>
    void OnPositionUpdated(ref esriGpsPositionInfo position, bool estimate)
    {
      try
      {
        //add the tracking element to the tracking graphics layer (should happen only once)
        if (-1 == m_trackObjectIndex)
        {
          int index = AddTrackElement(m_globeGraphicsLayer, position);
          if (-1 == index)
            throw new Exception("could not add tracking object");

          //cache the element's index
          m_trackObjectIndex = index;

          return;
        }
        //get the element by its index
        IElement elem = ((IGraphicsContainer3D)m_globeGraphicsLayer).get_Element(m_trackObjectIndex);

        //keep the previous location
        double lat, lon, alt;
        ((IPoint)elem.Geometry).QueryCoords(out lon, out lat);
        alt = ((IPoint)elem.Geometry).Z;

        //update the element's position
        IPoint point = elem.Geometry as IPoint;
        point.X = position.longitude;
        point.Y = position.latitude;
        point.Z = alt;
        elem.Geometry = (IGeometry)point;

        //update the element in the graphics layer.
        lock (m_globeGraphicsLayer)
        {
          m_globeGraphicsLayer.UpdateElementByIndex(m_trackObjectIndex);
        }

        IGlobeCamera globeCamera = m_sceneViwer.Camera as IGlobeCamera;

        //set the camera position in order to track the element
        if (m_bTrackAboveTarget)
          TrackAboveTarget(globeCamera, point);
        else
          TrackFollowTarget(globeCamera, point.X, point.Y, point.Z, lon, lat, alt);

      }
      catch (Exception ex)
      {
        System.Diagnostics.Trace.WriteLine(ex.Message);
      }
    }
    void TrackDynamicObject_PositionUpdated(ref esriGpsPositionInfo position, bool estimate)
    {
      
    }

    /// <summary>
    /// If the user chose to track the element from behind, set the camera behind the element 
    /// so that the camera will be placed on the line connecting the previous and the current element's position.
    /// </summary>
    /// <param name="globeCamera"></param>
    /// <param name="newLon"></param>
    /// <param name="newLat"></param>
    /// <param name="newAlt"></param>
    /// <param name="oldLon"></param>
    /// <param name="oldLat"></param>
    /// <param name="oldAlt"></param>
    private void TrackFollowTarget(IGlobeCamera globeCamera, double newLon, double newLat, double newAlt, double oldLon, double oldLat, double oldAlt)
    {
      //make sure that the camera position is not directly above the element. Otherwise it can lead to
      //an ill condition
      if (newLon == oldLon && newLat == oldLat)
      {
        newLon += 0.00001;
        newLat += 0.00001;
      }

      //calculate the azimuth from the previous position to the current position
      double azimuth = Math.Atan2(newLat - oldLat, newLon - oldLon) * (Math.PI / 180.0);

      //the camera new position, right behind the element
      double obsX = newLon - 0.04 * Math.Cos(azimuth * (Math.PI / 180));
      double obsY = newLat - 0.04 * Math.Sin(azimuth * (Math.PI / 180));

      //set the camera position. The camera must be locked in order to prevent a dead-lock caused by the cache manager
      lock (globeCamera)
      {
        globeCamera.SetTargetLatLonAlt(newLat, newLon, newAlt / 1000.0);
        globeCamera.SetObserverLatLonAlt(obsY, obsX, newAlt / 1000.0 + 0.7);
        m_sceneViwer.Camera.Apply();
      }

      //refresh the globe display
      m_globeDisplay.RefreshViewers();
    }

    /// <summary>
    /// should the user choose to track the element from above, set the camera above the element
    /// </summary>
    /// <param name="globeCamera"></param>
    /// <param name="objectLocation"></param>
    private void TrackAboveTarget(IGlobeCamera globeCamera, IPoint objectLocation)
    {
      //Update the observer as well as the camera position
      //The camera must be locked in order to prevent a dead-lock caused by the cache manager
      lock (globeCamera)
      {

        globeCamera.SetTargetLatLonAlt(objectLocation.Y, objectLocation.X, objectLocation.Z / 1000.0);

        //The camera must nut be located exactly above the target, since it results in poor orientation computation
        //and therefore the camera gets jumpy.
        globeCamera.SetObserverLatLonAlt(objectLocation.Y - 0.000001, objectLocation.X - 0.000001, objectLocation.Z / 1000.0 + 30.0);
        m_sceneViwer.Camera.Apply();
      }
      m_globeDisplay.RefreshViewers();
    }

    #endregion

    #region IDisposable Members

    public void Dispose()
    {
    }

    #endregion
  }
}
