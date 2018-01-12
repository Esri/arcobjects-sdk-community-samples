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
using System.IO;

using System.Collections.Specialized;

using System.Runtime.InteropServices;

using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Server;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.SOESupport;

namespace NetSimpleRESTSOEWithCapabilities
{
    [ComVisible(true)]
    [Guid("3f6cd6bf-75cc-402b-90ac-fb6f33aaa6ba")]
    [ClassInterface(ClassInterfaceType.None)]
    [ServerObjectExtension("MapServer",
        AllCapabilities = "BusServices,TrainServices",
        DefaultCapabilities = "BusServices",
        Description = ".NET Simple REST SOE With 2 Capabilities",
        DisplayName = ".NET Simple REST SOE With Capabilities",
        Properties = "",
        SupportsREST = true,
        SupportsSOAP = false)]
    public class NetSimpleRESTSOEWithCapabilities : IServerObjectExtension, IRESTRequestHandler
    {
        private string soeName;
        private const string c_CapabilityBusServices = "BusServices";
        private const string c_CapabilityTrainServices = "TrainServices";

        private IServerObjectHelper soHelper;
        private ServerLogger serverLog;
        private IRESTRequestHandler reqHandler;
        private IMapServerDataAccess mapServerDataAccess;
        private IMapLayerInfos layerInfos;

        public NetSimpleRESTSOEWithCapabilities()
        {
            soeName = this.GetType().Name;
            reqHandler = new SoeRestImpl(soeName, CreateRestSchema(), CheckCapabilities) as IRESTRequestHandler;
        }

        #region IServerObjectExtension Members

        public void Init(IServerObjectHelper pSOH)
        {
            this.soHelper = pSOH;
            this.serverLog = new ServerLogger();
            this.mapServerDataAccess = (IMapServerDataAccess)this.soHelper.ServerObject;
            IMapServer3 ms = (IMapServer3)this.mapServerDataAccess;
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

        #region IRESTRequestHandler Members

        public string GetSchema()
        {
            return reqHandler.GetSchema();
        }

        public byte[] HandleRESTRequest(string Capabilities, string resourceName, string operationName, string operationInput, string outputFormat, string requestProperties, out string responseProperties)
        {
            return reqHandler.HandleRESTRequest(Capabilities, resourceName, operationName, operationInput, outputFormat, requestProperties, out responseProperties);
        }

        #endregion

        private RestResource CreateRestSchema()
        {
            RestResource rootRes = new RestResource(soeName, false, RootResHandler);

            RestResource NumberOfBusStationsResource = new RestResource("NumberOfBusStations", false, 
                NumberOfBusStationsResHandler, c_CapabilityBusServices);
            rootRes.resources.Add(NumberOfBusStationsResource);

            RestResource NumberOfTrainStationsResource = new RestResource("NumberOfTrainStations", false, 
                NumberOfTrainStationsResHandler, c_CapabilityTrainServices);
            rootRes.resources.Add(NumberOfTrainStationsResource);

            RestOperation findBusStationByIdOp = new RestOperation("findBusStationById",
                                                      new string[] { "busStationId" },
                                                      new string[] { "json" },
                                                      findBusStationById, c_CapabilityBusServices);

            rootRes.operations.Add(findBusStationByIdOp);

            RestOperation findTrainStationByIdOp = new RestOperation("findTrainStationById",
                                          new string[] { "trainStationId" },
                                          new string[] { "json" },
                                          findTrainStationById, c_CapabilityTrainServices);

            rootRes.operations.Add(findTrainStationByIdOp);

            return rootRes;
        }

        #region Resource Handlers
        private byte[] RootResHandler(NameValueCollection boundVariables, string outputFormat, string requestProperties, out string responseProperties)
        {
            responseProperties = null;

            JSONObject json = new JSONObject();
            json.AddString("name", ".NET Simple REST SOE With Capabilities");
            json.AddJSONArray("layerInfo", getLayerInfo());

            return Encoding.UTF8.GetBytes(json.ToJSONString(null));
        }

        private byte[] NumberOfBusStationsResHandler(NameValueCollection boundVariables, string outputFormat, string requestProperties, out string responseProperties)
        {
            responseProperties = "{\"Content-Type\" : \"application/json\"}";

            JsonObject result = new JsonObject();
            result.AddString("numberOfBusStations", "100");

            return Encoding.UTF8.GetBytes(result.ToJson());
        }

        private byte[] NumberOfTrainStationsResHandler(NameValueCollection boundVariables, string outputFormat, string requestProperties, out string responseProperties)
        {
            responseProperties = "{\"Content-Type\" : \"application/json\"}";

            JsonObject result = new JsonObject();
            result.AddString("numberOfTrainStations", "100");

            return Encoding.UTF8.GetBytes(result.ToJson());
        }
        #endregion

        #region Operation Handlers

        private byte[] findBusStationById(NameValueCollection boundVariables,
                                          JsonObject operationInput,
                                              string outputFormat,
                                              string requestProperties,
                                          out string responseProperties)

        {
            responseProperties = null; 

            string IdValue;
            bool found = operationInput.TryGetString("busStationId", out IdValue);           
            if (!found || string.IsNullOrEmpty(IdValue))
                throw new ArgumentNullException("busStationId");

   
            JsonObject result = new JsonObject();
            result.AddString("stationName", "Bus Station " + IdValue);

            return Encoding.UTF8.GetBytes(result.ToJson());
        }

        private byte[] findTrainStationById(NameValueCollection boundVariables,
                                          JsonObject operationInput,
                                              string outputFormat,
                                              string requestProperties,
                                          out string responseProperties)
        {
            responseProperties = null; 

            string IdValue;
            bool found = operationInput.TryGetString("trainStationId", out IdValue);
            if (!found || string.IsNullOrEmpty(IdValue))
                throw new ArgumentNullException("trainStationId");


            JsonObject result = new JsonObject();
            result.AddString("stationName", "Train Station " + IdValue);

            return Encoding.UTF8.GetBytes(result.ToJson());
        }

        #endregion

        #region Business Methods
        private JSONArray getLayerInfo()
        {
            JSONArray layersArray = new JSONArray();
            for (int i = 0; i < this.layerInfos.Count; i++)
            {
                IMapLayerInfo layerInfo = layerInfos.get_Element(i);
                JSONObject jo = new JSONObject();
                jo.AddString("name", layerInfo.Name);
                jo.AddLong("id", layerInfo.ID);
                jo.AddString("description", layerInfo.Description);
                layersArray.AddJSONObject(jo);
            }

            return layersArray;
        }
        #endregion

        private bool CheckCapabilities(string configCapabilities, string requiredCapability,
          object resourceOrOperation, NameValueCollection boundVariables, JsonObject operInput, string requestProperties)
        {
            if (string.IsNullOrEmpty(requiredCapability))
                return true;
            else
            {
                if (string.IsNullOrEmpty(configCapabilities))
                    return false;
            }

            string caps = configCapabilities.ToLowerInvariant();

            HashSet<string> configCaps = new HashSet<string>(caps.Split(','));
            if (!configCaps.Contains(requiredCapability))
                return false;

            return true;
        }

    }
}
