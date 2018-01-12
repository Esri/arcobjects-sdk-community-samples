// Copyright 2015 ESRI
// 
// All rights reserved under the copyright laws of the United States
// and applicable international laws, treaties, and conventions.
// 
// You may freely redistribute and use this sample code, with or
// without modification, provided you include the original copyright
// notice and use restrictions.
// 
// See the use restrictions at <your ArcGIS Developer Kit install location>/userestrictions.txt.
// 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Collections.Specialized;

using System.Runtime.InteropServices;

using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Server;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.SOESupport;

namespace NetFindNearFeaturesRESTSOE
{
  [ComVisible(true)]
  [Guid("f55a4539-de74-41d5-9f2c-bc59d74e63e1")]
  [ClassInterface(ClassInterfaceType.None)]
    [ServerObjectExtension("MapServer",
        AllCapabilities = "GetInfo,FindFeatures",
        DefaultCapabilities = "GetInfo",
        Description = ".NET Find Near Features REST SOE Sample",
        DisplayName = ".NET Find Near Features REST SOE",
        Properties = "",
        SupportsREST = true,
        SupportsSOAP = false)]
    public class NetFindNearFeaturesRESTSOE : IServerObjectExtension, IObjectConstruct, IRESTRequestHandler
  {
    private const string c_CapabilityGetInfo = "GetInfo";
    private const string c_CapabilityFindFeatures = "FindFeatures";

    private string soeName;
    private IPropertySet configProps;
    private IServerObjectHelper soHelper;
    private IMapServer3 ms;
    private ServerLogger serverLog;
    private IRESTRequestHandler reqHandler;
    private IMapServerDataAccess mapServerDataAccess;
    private IMapLayerInfos layerInfos;


    public NetFindNearFeaturesRESTSOE()
    {
        soeName = this.GetType().Name;
        reqHandler = new SoeRestImpl(soeName, CreateRestSchema()) as IRESTRequestHandler;
    }

    private RestResource CreateRestSchema()
    {
      RestResource soeResource = new RestResource("NetFindNearFeaturesRESTSOE", false, SOE, c_CapabilityGetInfo);

      RestResource customLayerResource = new RestResource("customLayers",true, true, CustomLayer, c_CapabilityGetInfo);

      RestOperation findNearFeatsOp = new RestOperation("findNearFeatures",
                                                new string[] { "location", "distance" },
                                                new string[] { "json" },
                                                FindNearFeatures,
                                                c_CapabilityFindFeatures);

      customLayerResource.operations.Add(findNearFeatsOp);

      soeResource.resources.Add(customLayerResource);

      return soeResource;
    }

    #region IServerObjectExtension
    public void Init(IServerObjectHelper pSOH)
    {
        this.soHelper = pSOH;
        this.serverLog = new ServerLogger();
        this.mapServerDataAccess = (IMapServerDataAccess)this.soHelper.ServerObject;
        this.ms = (IMapServer3)this.mapServerDataAccess;
        IMapServerInfo mapServerInfo = ms.GetServerInfo(ms.DefaultMapName);
        this.layerInfos = mapServerInfo.MapLayerInfos;

        serverLog.LogMessage(ServerLogger.msgType.infoStandard, this.soeName + ".init()", 200, "Initialized " + this.soeName + " SOE.");
    }

    public void Shutdown()
    {
        serverLog.LogMessage(ServerLogger.msgType.infoStandard, this.soeName + ".init()", 200, "Shutting down " + this.soeName + " SOE.");
        this.soHelper = null;
        this.serverLog = null;
        this.mapServerDataAccess = null;
        this.layerInfos = null;
    }
    #endregion

    #region IObjectConstruct
    public void Construct(IPropertySet props)
    {
      AutoTimer timer = new AutoTimer();
      serverLog.LogMessage(ServerLogger.msgType.infoSimple, "Construct", -1, soeName + " Construct has started.");

      configProps = props;

      //TODO - put any construct-time logic here
      serverLog.LogMessage(ServerLogger.msgType.infoSimple, "Construct", -1, timer.Elapsed, soeName + " Construct has completed.");
    }
    #endregion

    #region IRESTRequestHandler
    public string GetSchema()
    {
      return reqHandler.GetSchema();
    }

    public byte[] HandleRESTRequest(string Capabilities, string resourceName, string operationName, string operationInput, string outputFormat, string requestProperties, out string responseProperties)
    {
      return reqHandler.HandleRESTRequest(Capabilities, resourceName, operationName, operationInput, outputFormat, requestProperties, out responseProperties);
    }
    #endregion

    #region Resource Handlers

    private byte[] SOE(NameValueCollection boundVariables, string outputFormat, string requestProperties, out string responseProperties)
    {
      responseProperties = null;

      CustomLayerInfo[] layerInfos = GetLayerInfos();

      JsonObject[] jos = new JsonObject[layerInfos.Length];

      for (int i = 0; i < layerInfos.Length; i++)
        jos[i] = layerInfos[i].ToJsonObject();

      JsonObject result = new JsonObject();
      result.AddArray("customLayers", jos);

      string json = result.ToJson();

      return Encoding.UTF8.GetBytes(json);
    }

    //customLayers/{customLayersID}
    //returns json with simplified layerinfo (name, id, extent)
    private byte[] CustomLayer(NameValueCollection boundVariables, string outputFormat, string requestProperties, out string responseProperties)
    {
      responseProperties = "{\"Content-Type\" : \"application/json\"}";

      if (null == boundVariables["customLayersID"] )
      {
          JsonObject obj = new JsonObject();

          // put collection code here
          CustomLayerInfo[] layerInfos = GetLayerInfos();

          JsonObject[] jos = new JsonObject[layerInfos.Length];

          for (int i = 0; i < layerInfos.Length; i++)
              jos[i] = layerInfos[i].ToJsonObject();

          obj.AddArray("customLayers", jos);

          return Encoding.UTF8.GetBytes(obj.ToJson());
      }

      //layerID
      int layerID = Convert.ToInt32(boundVariables["customLayersID"]);

      //execute
      CustomLayerInfo layerInfo = GetLayerInfo(layerID);

      string json = layerInfo.ToJsonObject().ToJson();

      return Encoding.UTF8.GetBytes(json);
    }
    #endregion

    #region Operation Handlers
    //customLayers/{customLayersID}/findNearFeatures?location=<jsonPoint>&distance=<double>
    private byte[] FindNearFeatures(NameValueCollection boundVariables,
                                   JsonObject operationInput,
                                   string outputFormat,
                                   string requestProperties,
                               out string responseProperties)
    {
      responseProperties = "{\"Content-Type\" : \"application/json\"}";

      //layerID
      int layerID = Convert.ToInt32(boundVariables["customLayersID"]);

      //location
      JsonObject jsonPoint;
      if (!operationInput.TryGetJsonObject("location", out jsonPoint))
        throw new ArgumentNullException("location");

      IPoint location = Conversion.ToGeometry(jsonPoint, esriGeometryType.esriGeometryPoint) as IPoint;
      if (location == null)
        throw new ArgumentException("FindNearFeatures: invalid location", "location");

      //distance
      double? distance;
      if (!operationInput.TryGetAsDouble("distance", out distance) || !distance.HasValue)
        throw new ArgumentException("FindNearFeatures: invalid distance", "distance");

      //execute asking the map server to generate json directly (not an IRecordSet)
      byte[] result = FindNearFeatures(layerID, location, distance.Value);

      return result;
    }
    #endregion

    #region Business Methods
    private CustomLayerInfo GetLayerInfo(int layerID)
    {
      if (layerID < 0)
        throw new ArgumentOutOfRangeException("layerID");

      IMapLayerInfo layerInfo;
      long c = this.layerInfos.Count;

      for (int i = 0; i < c; i++)
      {
        layerInfo = this.layerInfos.get_Element(i);
        if (layerInfo.ID == layerID)
          return new CustomLayerInfo(layerInfo);
      }

      throw new ArgumentOutOfRangeException("layerID");
    }

    private CustomLayerInfo[] GetLayerInfos()
    {
      int c = this.layerInfos.Count;

      CustomLayerInfo[] customLayerInfos = new CustomLayerInfo[c];

      for (int i = 0; i < c; i++)
      {
        IMapLayerInfo layerInfo = layerInfos.get_Element(i);
        customLayerInfos[i] = new CustomLayerInfo(layerInfo);
      }

      return customLayerInfos;
    }

    private byte[] FindNearFeatures(int layerID, IPoint location, double distance)
    {
      if (layerID < 0)
        throw new ArgumentOutOfRangeException("layerID");

      if (distance <= 0.0)
        throw new ArgumentOutOfRangeException("distance");

      IGeometry queryGeometry = ((ITopologicalOperator)location).Buffer(distance);

      ISpatialFilter filter = new SpatialFilterClass();
      filter.Geometry = queryGeometry;
      filter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;

      IQueryResultOptions resultOptions = new QueryResultOptionsClass();
      resultOptions.Format = esriQueryResultFormat.esriQueryResultJsonAsMime;

      AutoTimer timer = new AutoTimer(); //starts the timer

      IMapTableDescription tableDesc = GetTableDesc(layerID);

      this.serverLog.LogMessage(ServerLogger.msgType.infoDetailed, "FindNearFeatures", -1, timer.Elapsed, "Finding table description elapsed this much.");

      IQueryResult result = this.ms.QueryData(this.ms.DefaultMapName, tableDesc, filter, resultOptions);

      return result.MimeData;
    }

    private IMapTableDescription GetTableDesc(int layerID)
    {
        ILayerDescriptions layerDescs = this.ms.GetServerInfo(this.ms.DefaultMapName).DefaultMapDescription.LayerDescriptions;
        long c = layerDescs.Count;

      for (int i = 0; i < c; i++)
      {
        ILayerDescription3 layerDesc = (ILayerDescription3)layerDescs.get_Element(i);

        if (layerDesc.ID == layerID)
        {
          layerDesc.LayerResultOptions = new LayerResultOptionsClass();
          layerDesc.LayerResultOptions.GeometryResultOptions = new GeometryResultOptionsClass();
          layerDesc.LayerResultOptions.GeometryResultOptions.DensifyGeometries = true;

          return (IMapTableDescription)layerDesc;
        }
      }

      throw new ArgumentOutOfRangeException("layerID");
    }
    #endregion

  }
}
