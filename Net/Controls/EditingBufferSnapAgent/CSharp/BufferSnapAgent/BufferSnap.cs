using System;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;

namespace BufferSnapCS
{
  [Guid("44BDCF61-5CD0-41f3-A934-8CAFCD931DEF")]
  [ClassInterface(ClassInterfaceType.None)]
  [ProgId("BufferSnapCS.BufferSnap")]

  /// <summary>
  /// Uses the Create Feature event to turn on the extension, which 
  /// implements a snapping agent. The Buffer Snap agent is based on a buffer
  /// around the points of the first editable point feature class.
  /// A buffer of 1000 map units is created and if the next point feature created
  /// is within the tolerance it is snapped to the buffer ring. 
  /// </summary>
  public sealed class BufferSnap : IEngineSnapAgent, IEngineSnapAgentCategory, IPersistVariant, IExtension
  {

    #region COM Registration Function(s)
    [ComRegisterFunction()]
    [ComVisible(false)]
    static void RegisterFunction(Type registerType)
    {
      // Required for ArcGIS Component Category Registrar support
      ArcGISCategoryRegistration(registerType);
    }

    [ComUnregisterFunction()]
    [ComVisible(false)]
    static void UnregisterFunction(Type registerType)
    {
      // Required for ArcGIS Component Category Registrar support
      ArcGISCategoryUnregistration(registerType);

    }

    #region ArcGIS Component Category Registrar generated code
    /// <summary>
    /// Required method for ArcGIS Component Category registration -
    /// Do not modify the contents of this method with the code editor.
    /// </summary>
    private static void ArcGISCategoryRegistration(Type registerType)
    {
      string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
      EngineSnapAgents.Register(regKey);


    }
    /// <summary>
    /// Required method for ArcGIS Component Category unregistration -
    /// Do not modify the contents of this method with the code editor.
    /// </summary>
    private static void ArcGISCategoryUnregistration(Type registerType)
    {
      string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
      EngineSnapAgents.Unregister(regKey);

    }

    #endregion
    #endregion

    //declare and initialize class variables.
    private IFeatureCache     m_featureCache;
    private IFeatureClass     m_featureClass;
    private IEngineEditor     m_engineeditor;

    public BufferSnap()
    {
    }

    #region "IPersistVariant Implementations"

    public ESRI.ArcGIS.esriSystem.UID ID
    {
      get
      {
        UID uID = new UIDClass();
        uID.Value = "BufferSnapCS.BufferSnap";
        return uID;
      }
    }

    public void Load(ESRI.ArcGIS.esriSystem.IVariantStream Stream)
    {
    }

    public void Save(ESRI.ArcGIS.esriSystem.IVariantStream Stream)
    {
    }

    #endregion

    #region "IEngineSnapAgent Implementations"

    public string Name
    {
      get
      {
        return "Buffer Snap CS";
      }     
    }

    public bool Snap(ESRI.ArcGIS.Geometry.IGeometry geom,
      ESRI.ArcGIS.Geometry.IPoint point, double tolerance)
    {
      GetFeatureClass();

      bool b_setNewFeatureCache = false;
      
      if (m_featureClass == null || m_engineeditor == null)
        return false;

      if (m_featureClass.ShapeType != esriGeometryType.esriGeometryPoint)
        return false;

      //Check if a feature cache has been created.
      if (!b_setNewFeatureCache)
      {
        m_featureCache = new FeatureCache();
        b_setNewFeatureCache = true;
      }

      //Fill the cache with the geometries. 
      //It is up to the developer to choose an appropriate value
      //given the map units and the scale at which editing will be undertaken.
      FillCache(m_featureClass, point, 10000);
      
      IProximityOperator proximityOp = point as IProximityOperator;
      double minDist = tolerance;

      IPoint cachePt = new PointClass();
      IPoint snapPt = new PointClass();
      IPolygon outPoly = new PolygonClass();
      ITopologicalOperator topoOp;

      IFeature feature;
      int Index = 0;
      for (int Count = 0; Count < m_featureCache.Count; Count++)
      {
        feature = m_featureCache.get_Feature(Count);
        cachePt = feature.Shape as IPoint;
        topoOp = cachePt as ITopologicalOperator;

        //Set the buffer distance to an appropriate value
        //given the map units and data being edited
        outPoly = topoOp.Buffer(1000) as IPolygon;

        double Dist = proximityOp.ReturnDistance(outPoly);
        if (Dist < minDist)
        {
          Index = Count;
          minDist = Dist;
        }
      }

      //Make sure minDist is within the search tolerance.
      if (minDist >= tolerance)
        return false;

      //Retrieve the feature and its part again.
      feature = m_featureCache.get_Feature(Index);
      cachePt = feature.Shape as IPoint;
      topoOp = cachePt as ITopologicalOperator;

      //Set the buffer distance to an appropriate value
      //given the map scale and data being edited
      outPoly = topoOp.Buffer(1000) as IPolygon;
      proximityOp = outPoly as IProximityOperator;
      snapPt = proximityOp.ReturnNearestPoint(point,esriSegmentExtension.esriNoExtension);

      //Since point was passed in ByValue, we have to modify its values instead.
      //of giving it a new address.
      point.PutCoords(snapPt.X, snapPt.Y);

      return true;
    
    }

    private void FillCache(ESRI.ArcGIS.Geodatabase.IFeatureClass FClass,
      ESRI.ArcGIS.Geometry.IPoint pPoint, double Distance)
    {
      m_featureCache.Initialize(pPoint, Distance);
      m_featureCache.AddFeatures(FClass);
    }

    #endregion

    private void GetFeatureClass()
    {
      IMap map = m_engineeditor.Map as IMap;
      IEngineEditLayers snapLayers = m_engineeditor as IEngineEditLayers;
      IFeatureLayer featLayer = snapLayers.TargetLayer as IFeatureLayer;

      //Search the editable layers and set the snap feature class to the point layer.
      for (int CountLayers = 0; CountLayers < map.LayerCount; CountLayers++)
      {
        if (featLayer == null)
          return;

        if (featLayer.FeatureClass.ShapeType != esriGeometryType.esriGeometryPoint)
        {
          return;
        }
        else
        {
          m_featureClass = featLayer.FeatureClass;
        }
      }
    }

    #region "IExtension Members"

    public void Shutdown()
    {
      m_engineeditor = null;
      m_featureCache = null;
    }

    public void Startup(ref object initializationData)
    {
      if (initializationData != null && initializationData is IEngineEditor)
      {
        m_engineeditor = (IEngineEditor)initializationData;
      }

    #endregion
    }


    #region "IEngineSnapAgentCategory Members"

    string IEngineSnapAgentCategory.Category
    {
      get {return ("Buffer Snap Category CS");}
    }

    #endregion
  }
}
