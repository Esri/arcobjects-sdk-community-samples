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
// Copyright 2015 ESRI
// 
// All rights reserved under the copyright laws of the United States
// and applicable international laws, treaties, and conventions.
// 
// You may freely redistribute and use this sample code, with or
// without modification, provided you include the original copyright
// notice and use restrictions.
// 
// See the use restrictions at <your ArcGIS install location>/DeveloperKit10.4/userestrictions.txt.
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

namespace NetRESTSOEProperties
{
    [ComVisible(true)]
    [Guid("b2d11535-586f-47e4-83dc-2ba7d0eb4bd3")]
    [ClassInterface(ClassInterfaceType.None)]
    [ServerObjectExtension("MapServer",
        AllCapabilities = "",
        DefaultCapabilities = "",
        Description = ".NET Simple REST SOE With Properties",
        DisplayName = ".NET Simple REST SOE With Properties",
        Properties = "layerType=feature;returnFormat=json;maxNumFeatures=100;isEditable=false",
        HasManagerPropertiesConfigurationPane = true,
        SupportsREST = true,
        SupportsSOAP = false)]
    public class NetSimpleRESTSOEWithProperties : IServerObjectExtension, IObjectConstruct, IRESTRequestHandler
    {
        private string soeName;
        private string layerType = null;
        private string returnFormat = null;
        private int maxNumFeatures = 0;
        private bool isEditable = false;

        private IServerObjectHelper soHelper;
        private ServerLogger serverLog;
        private IRESTRequestHandler reqHandler;
        private IMapServerDataAccess mapServerDataAccess;
        private IMapLayerInfos layerInfos;

        public NetSimpleRESTSOEWithProperties()
        {
            soeName = this.GetType().Name;
            reqHandler = new SoeRestImpl(soeName, CreateRestSchema()) as IRESTRequestHandler;
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

        #region IObjectConstruct Members

        public void Construct(IPropertySet props)
        {
            string lType = (string) props.GetProperty("layerType");
            if (lType.Equals("feature") || lType.Equals("raster") || lType.Equals("all"))
            {
                this.layerType = lType;
            }
            else
            {
                this.layerType = "feature";
            }

            string format = (string)props.GetProperty("returnFormat");
            if (format.ToLower().Equals("json") || format.ToLower().Equals("html") || format.ToLower().Equals("text"))
            {
                this.returnFormat = format;
            }
            else
            {
                this.returnFormat = "json";
            }

            int maxFeatures = Convert.ToInt32((string)props.GetProperty("maxNumFeatures"));
            if (maxFeatures <= 0)
            {
                this.maxNumFeatures = 100;
            }
            else
            {
                this.maxNumFeatures = maxFeatures;
            }

            this.isEditable = Convert.ToBoolean((string)props.GetProperty("isEditable"));
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
            RestResource soeResource = new RestResource(soeName, false, RootResHandler);

            RestResource layerResource = new RestResource("layers", false, LayersResHandler);
            soeResource.resources.Add(layerResource);

            RestResource propertiesResource = new RestResource("properties", false, PropertiesResHandler);
            soeResource.resources.Add(propertiesResource);

            return soeResource;
        }

        private byte[] RootResHandler(NameValueCollection boundVariables, string outputFormat, string requestProperties, out string responseProperties)
        {
            responseProperties = null;

            JSONObject result = new JSONObject();
            result.AddString("name", ".Net Simple REST SOE With properties");
            result.AddString("description", "Simple REST SOE with 1 sub-resource called \"layers\" and 1 property called layerType.");
            result.AddString("usage", "The \"layers\" subresource returns all layers of a certain type in the map service.\n"
                + "The layerType property defines the type of layers are returned by the \"layers\" subresource."); 

            return Encoding.UTF8.GetBytes(result.ToJSONString(null));
        }

        private byte[] LayersResHandler(NameValueCollection boundVariables, string outputFormat, string requestProperties, out string responseProperties)
        {
            responseProperties = "{\"Content-Type\" : \"application/json\"}";
            String aoLayerType = "";
            if (this.layerType.Equals("feature"))
            {
                aoLayerType = "Feature Layer";
            }
            else if (this.layerType.Equals("raster"))
            {
                aoLayerType = "Raster Layer";
            }
            else if (this.layerType.Equals("dataset"))
            {
                aoLayerType = "Network Dataset Layer";
            }
            else
            {
                throw new Exception("Propety layerType has invalid value. Acceptable values are \"feature\", \"raster\", and \"dataset\".");
            }

            JSONArray layersArray = new JSONArray();
            for (int i = 0; i < this.layerInfos.Count; i++)
            {
                IMapLayerInfo layerInfo = layerInfos.get_Element(i);
                String lType = layerInfo.Type;
                if (lType.Equals(aoLayerType))
                {
                    JSONObject jo = new JSONObject();
                    jo.AddString("name", layerInfo.Name);
                    jo.AddLong("id", layerInfo.ID);
                    jo.AddString("description", layerInfo.Description);
                    layersArray.AddJSONObject(jo);
                }
            }

            JSONObject result = new JSONObject();
            result.AddJSONArray("Layers", layersArray);
            return Encoding.UTF8.GetBytes(result.ToJSONString(null));
        }

        private byte[] PropertiesResHandler(NameValueCollection boundVariables, string outputFormat, string requestProperties, out string responseProperties)
        {
            responseProperties = "{\"Content-Type\" : \"application/json\"}";

            JsonObject result = new JsonObject();
            result.AddString("layers", this.layerType);
            result.AddString("returnFormat", this.returnFormat);
            result.AddLong("maxNumFeatures", this.maxNumFeatures);
            result.AddBoolean("isEditable", this.isEditable);

            return Encoding.UTF8.GetBytes(result.ToJson());         
            
        }

        private byte[] createErrorObject(int codeNumber, String errorMessageSummary, String[] errorMessageDetails)
        {
            if (errorMessageSummary.Length == 0 || errorMessageSummary == null)
            {
                throw new Exception("Invalid error message specified.");
            }

            JSONObject errorJSON = new JSONObject();
            errorJSON.AddLong("code", codeNumber);
            errorJSON.AddString("message", errorMessageSummary);

            if (errorMessageDetails == null)
            {
                errorJSON.AddString("details", "No error details specified.");
            }
            else
            {
                String errorMessages = "";
                for (int i = 0; i < errorMessageDetails.Length; i++)
                {
                    errorMessages = errorMessages + errorMessageDetails[i] + "\n";
                }

                errorJSON.AddString("details", errorMessages);
            }

            JSONObject error = new JSONObject();
            errorJSON.AddJSONObject("error", errorJSON);

            return Encoding.UTF8.GetBytes(errorJSON.ToJSONString(null));
        }
    }
}
