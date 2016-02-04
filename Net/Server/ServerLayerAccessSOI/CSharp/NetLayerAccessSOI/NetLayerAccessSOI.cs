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

using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Server;
using ESRI.ArcGIS.SOESupport;
using ESRI.ArcGIS.SOESupport.SOI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;


//TODO: sign the project (project properties > signing tab > sign the assembly)
//      this is strongly suggested if the dll will be registered using regasm.exe <your>.dll /codebase


namespace NetLayerAccessSOI
{
    [ComVisible(true)]
    [Guid("0b8694f2-1730-453d-b645-7022dfc24bef")]
    [ClassInterface(ClassInterfaceType.None)]
    [ServerObjectInterceptor("MapServer",
        Description = "SOI to control access to different layers of a service.",
        DisplayName = "DotNet Layer Access SOI Example",
        Properties = "")]
    public class NetLayerAccessSOI : IServerObjectExtension, IRESTRequestHandler, IWebRequestHandler, IRequestHandler2, IRequestHandler
    {
        private string _soiName;
        private IServerObjectHelper _soHelper;
        private ServerLogger _serverLog;
        private IServerObject _serverObject;
        RestSOIHelper _restServiceSOI;
        SoapSOIHelper _SoapSOIHelper;

        /// <summary>
        /// Map used to store permission information. Permission rules for each service is read form the permissons.json file.
        /// </summary>
        private Dictionary<String, String> _servicePermissionMap = new Dictionary<string,string>();
        /*
         * Permission are read from this external file. Advantage of an external file is that 
         * same SOI can be used for multiple services and permission for all of these services
         * is read from the permission.json file. 
         *  
         */
        private String _permissionFilePath = "C:\\arcgisserver\\permission.json"; //default path

        private String _wsdlFilePath = "C:\\Program Files\\ArcGIS\\Server\\XmlSchema\\MapServer.wsdl"; //default path


        HashSet<string> _authorizedLayerSet;

        public NetLayerAccessSOI ()
        {
            _soiName = this.GetType().Name;
        }

        private void InitFiltering ()
        {
            _restServiceSOI.FilterMap = new Dictionary<RestHandlerOpCode, RestFilter>
            {
                { RestHandlerOpCode.Root, new RestFilter
                                            { 
                                                PreFilter = null, 
                                                PostFilter = PostFilterRESTRoot
                                            } },
                { RestHandlerOpCode.RootExport, new RestFilter
                                            { 
                                                PreFilter = PreFilterExport, 
                                                PostFilter = null 
                                            } },
                { RestHandlerOpCode.RootFind, new RestFilter
                                            { 
                                                PreFilter = PreFilterFindAndKml, 
                                                PostFilter = null 
                                            } },
                { RestHandlerOpCode.RootGenerateKml, new RestFilter
                                            { 
                                                PreFilter = PreFilterFindAndKml, 
                                                PostFilter = null 
                                            } },
                { RestHandlerOpCode.RootIdentify, new RestFilter
                                            { 
                                                PreFilter = PreFilterIdentify, 
                                                PostFilter = null 
                                            } },
                { RestHandlerOpCode.RootLayers, new RestFilter 
                                            { 
                                                PreFilter = null, 
                                                PostFilter = PostFilterRootLayers
                                            } },
                { RestHandlerOpCode.RootLegend, new RestFilter
                                            { 
                                                PreFilter = null, 
                                                PostFilter = PostFilterRootLegend
                                            } },
                { RestHandlerOpCode.LayerGenerateRenderer, new RestFilter 
                                            { 
                                                PreFilter = PreFilterLayerQuery, 
                                                PostFilter = null 
                                            } },
                { RestHandlerOpCode.LayerQuery, new RestFilter
                                            { 
                                                PreFilter = PreFilterLayerQuery, 
                                                PostFilter = null 
                                            } },
                { RestHandlerOpCode.LayerQueryRelatedRecords, new RestFilter 
                                            { 
                                                PreFilter = PreFilterLayerQuery, 
                                                PostFilter = null 
                                            } },
                                            /*
                                             * TODO explain unused custom code here
                                             */
                { MyRESTHandlerOpCode.CustomOperationDemoOpCode, new RestFilter 
                                            { 
                                                PreFilter = null, 
                                                PostFilter = null 
                                            } }
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pSOH"></param>
        public void Init ( IServerObjectHelper pSOH )
        {
            try
            {
                _soHelper = pSOH;
                _serverLog = new ServerLogger();
                _serverObject = pSOH.ServerObject;

                _restServiceSOI = new RestSOIHelper(_soHelper);
                _SoapSOIHelper = new SoapSOIHelper(_soHelper, _wsdlFilePath);

                if (File.Exists(_permissionFilePath))
                {
                    //TODO REMOVE
                    _serverLog.LogMessage(ServerLogger.msgType.infoStandard, _soiName + ".init()", 200, "Reading permissions from " + _permissionFilePath);

                    _servicePermissionMap = ReadPermissionFile(_permissionFilePath);

                    //TODO REMOVE
                    _serverLog.LogMessage(ServerLogger.msgType.infoStandard, _soiName + ".init()", 200, "Total permission entries: " + _servicePermissionMap.Count());
                }
                else
                {
                    _serverLog.LogMessage(ServerLogger.msgType.error, _soiName + ".init()", 500, "Permission file does not exist at " + _permissionFilePath);
                }

                InitFiltering();

                _serverLog.LogMessage(ServerLogger.msgType.infoStandard, _soiName + ".init()", 200, "Initialized " + _soiName + " SOI.");
            }
            catch (Exception e)
            {
                _serverLog.LogMessage(ServerLogger.msgType.error, _soiName + ".init()", 500, "Exception: " + e.Message + " in " + e.StackTrace);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Shutdown ()
        {
            _serverLog.LogMessage(ServerLogger.msgType.infoStandard, _soiName + ".init()", 200, "Shutting down " + _soiName + " SOE.");
        }

        #region REST interceptors

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetSchema ()
        {
            try
            {
                IRESTRequestHandler restRequestHandler = _restServiceSOI.FindRequestHandlerDelegate<IRESTRequestHandler>();
                if (restRequestHandler == null)
                    return null;

                return restRequestHandler.GetSchema();
            }
            catch (Exception e)
            {
                _serverLog.LogMessage(ServerLogger.msgType.error, _soiName + ".GetSchema()", 500, "Exception: " + e.Message + " in " + e.StackTrace);
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="restFilterOp"></param>
        /// <param name="restInput"></param>
        /// <param name="authorizedLayers"></param>
        /// <param name="responseProperties"></param>
        /// <returns></returns>
        private byte[] FilterRESTRequest (
                                          RestHandlerOpCode opCode,
                                          RestRequestParameters restInput,
                                          out string responseProperties )
        {
            try
            {
                responseProperties = null;
                IRESTRequestHandler restRequestHandler = _restServiceSOI.FindRequestHandlerDelegate<IRESTRequestHandler>();
                if (restRequestHandler == null)
                    throw new RestErrorException("Service handler not found");

                RestFilter restFilterOp = _restServiceSOI.GetFilter(opCode);

                if (null != restFilterOp && null != restFilterOp.PreFilter)
                    restInput = restFilterOp.PreFilter(restInput);

                byte[] response =
                    restRequestHandler.HandleRESTRequest(restInput.Capabilities, restInput.ResourceName, restInput.OperationName, restInput.OperationInput,
                        restInput.OutputFormat, restInput.RequestProperties, out responseProperties);

                if (null == restFilterOp || null == restFilterOp.PostFilter)
                    return response;
                
                string newResponseProperties;
                var newResponse = restFilterOp.PostFilter(restInput, response, responseProperties, out newResponseProperties);
                responseProperties = newResponseProperties;

                return newResponse;
            }
            catch (RestErrorException restException)
            {
                // pre- or post- filters can throw restException with the error JSON output in the Message property.
                // we catch them here and return JSON response.
                responseProperties = "{\"Content-Type\":\"text/plain;charset=utf-8\"}";
                //catch and return a JSON error from the pre- or postFilter.
                return System.Text.Encoding.UTF8.GetBytes(restException.Message);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="restInput"></param>
        /// <returns></returns>
        private RestHandlerOpCode GetHandlerOpCode (string resourceName, string operationName)
        {
            RestHandlerOpCode opCode = RestSOIHelper.GetHandlerOpCode(resourceName, operationName);
            
            if (opCode != RestHandlerOpCode.DefaultNoOp)
                return opCode;

            // The code below deals with the custom REST operation codes. This is required to enable REST request filtering for custom SOEs.
            // In this example the switch statement simply duplicates RestSOIHelper.GetHandlerOpCode() call.
            
            // If you don't plan to support filtering for any custom SOEs, remove code below until the end of the method.
            // If you want to support filtering for custom SOEs, modify the code below to match your needs.
            
            var resName = resourceName.TrimStart('/'); //remove leading '/' to prevent empty string at index 0
            var resourceTokens = (resName ?? "").ToLower().Split('/');
            string opName = (operationName ?? "").ToLower();

            switch (resourceTokens[0])
            {
                case "":
                    switch (opName)
                    {
                        case "":
                            return RestHandlerOpCode.Root;
                        case "export":
                            return RestHandlerOpCode.RootExport;
                        case "find":
                            return RestHandlerOpCode.RootFind;
                        case "identify":
                            return RestHandlerOpCode.RootIdentify;
                        case "generatekml":
                            return RestHandlerOpCode.RootGenerateKml;
                        default:
                            return RestHandlerOpCode.DefaultNoOp;
                    }
                case "layers":
                    {
                        var tokenCount = resourceTokens.GetLength(0);
                        if (1 == tokenCount)
                            return RestHandlerOpCode.RootLayers;
                        if (2 == tokenCount)
                            switch (opName)
                            {
                                case "":
                                    return RestHandlerOpCode.LayerRoot;
                                case "query":
                                    return RestHandlerOpCode.LayerQuery;
                                case "queryRelatedRecords":
                                    return RestHandlerOpCode.LayerQueryRelatedRecords;
                                default:
                                    return RestHandlerOpCode.DefaultNoOp;
                            }
                    }
                    break;
                case "legend":
                    return RestHandlerOpCode.RootLegend;
                default:
                    return RestHandlerOpCode.DefaultNoOp;
            }
            return RestHandlerOpCode.DefaultNoOp;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="capabilities"></param>
        /// <param name="resourceName"></param>
        /// <param name="operationName"></param>
        /// <param name="operationInput"></param>
        /// <param name="outputFormat"></param>
        /// <param name="requestProperties"></param>
        /// <param name="responseProperties"></param>
        /// <returns></returns>
        public byte[] HandleRESTRequest ( string capabilities, string resourceName, string operationName,
            string operationInput, string outputFormat, string requestProperties, out string responseProperties )
        {
            try
            {
                responseProperties = null;
                _serverLog.LogMessage(ServerLogger.msgType.infoStandard, _soiName + ".HandleRESTRequest()",
                    200, "Request received in Layer Access SOI for handleRESTRequest");

                _authorizedLayerSet = GetAuthorizedLayers(_restServiceSOI);

                var restInput = new RestRequestParameters
                        {
                            Capabilities = capabilities,
                            ResourceName = resourceName,
                            OperationName = operationName,
                            OperationInput = operationInput,
                            OutputFormat = outputFormat,
                            RequestProperties = requestProperties
                        };

                var opCode = GetHandlerOpCode(restInput.ResourceName, restInput.OperationName);
                return FilterRESTRequest(opCode, restInput, out responseProperties);
            }
            catch (Exception e)
            {
                _serverLog.LogMessage(ServerLogger.msgType.error, _soiName + ".HandleRESTRequest()", 500, "Exception: " + e.Message + " in " + e.StackTrace);
                throw;
            }
        }
        #endregion

        #region REST Pre-filters
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="restInput"></param>
        /// <param name="authorizedLayers"></param>
        /// <returns></returns>
        private RestRequestParameters PreFilterExport ( RestRequestParameters restInput )
        {
            JavaScriptSerializer sr = new JavaScriptSerializer { MaxJsonLength = int.MaxValue };
            var operationInputJson = sr.DeserializeObject(restInput.OperationInput) as IDictionary<string, object>;

            operationInputJson["layers"] = "show:" + String.Join(",", _authorizedLayerSet);

            restInput.OperationInput = sr.Serialize(operationInputJson);
            return restInput;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="restInput"></param>
        /// <param name="authorizedLayers"></param>
        /// <returns></returns>
        private RestRequestParameters PreFilterFindAndKml ( RestRequestParameters restInput)
        {
            JavaScriptSerializer sr = new JavaScriptSerializer { MaxJsonLength = int.MaxValue };
            var operationInputJson = sr.DeserializeObject(restInput.OperationInput) as IDictionary<string, object>;

            var removeSpacesRegEx = new Regex("\\s+");
            String requestedLayers = operationInputJson.ContainsKey("layers") ? operationInputJson["layers"].ToString() : "";
            requestedLayers = removeSpacesRegEx.Replace(requestedLayers, "");
            operationInputJson["layers"] = RemoveUnauthorizedLayersFromRequestedLayers(requestedLayers, _authorizedLayerSet);

            restInput.OperationInput = sr.Serialize(operationInputJson);
            return restInput;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="restInput"></param>
        /// <param name="authorizedLayers"></param>
        /// <returns></returns>
        private RestRequestParameters PreFilterIdentify ( RestRequestParameters restInput )
        {
            JavaScriptSerializer sr = new JavaScriptSerializer { MaxJsonLength = int.MaxValue };
            var operationInputJson = sr.DeserializeObject(restInput.OperationInput) as IDictionary<string, object>;

            String requestedLayers = operationInputJson.ContainsKey("layers") ? operationInputJson["layers"].ToString() : "";
            if (string.IsNullOrEmpty(requestedLayers) || requestedLayers.StartsWith("top") || requestedLayers.StartsWith("all"))
            {
                operationInputJson["layers"] = "visible:" + _authorizedLayerSet;
            }
            else if (requestedLayers.StartsWith("visible"))
            {
                var reqLayerParts = requestedLayers.Split(':');
                var removeSpacesRegEx = new Regex("\\s+");
                operationInputJson["layers"] = "visible:" +
                    RemoveUnauthorizedLayersFromRequestedLayers(
                        removeSpacesRegEx.Replace(reqLayerParts[1], ""), _authorizedLayerSet);
            }
            else
            {
                operationInputJson["layers"] = "visible:" + _authorizedLayerSet;
            }

            restInput.OperationInput = sr.Serialize(operationInputJson);
            return restInput;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="restInput"></param>
        /// <param name="authorizedLayers"></param>
        /// <returns></returns>
        private RestRequestParameters PreFilterLayerQuery ( RestRequestParameters restInput)
        {
            JavaScriptSerializer sr = new JavaScriptSerializer { MaxJsonLength = int.MaxValue };
            var operationInputJson = sr.DeserializeObject(restInput.OperationInput) as IDictionary<string, object>;
            var resName = restInput.ResourceName.TrimStart('/');
            var rnameParts = resName.Split('/');
            var requestedLayerId = rnameParts[1];

            if (!_authorizedLayerSet.Contains(requestedLayerId))
            {
                var errorReturn = new Dictionary<string, object>
                                    {
                                        {"error", new Dictionary<string, object>
                                                {
                                                    {"code", 404},
                                                    {"message", "Access Denied"}
                                                }
                                        }
                                    };

                throw new RestErrorException(sr.Serialize(errorReturn));
            }

            restInput.OperationInput = sr.Serialize(operationInputJson);
            return restInput;
        }

        #endregion


        #region REST Post-filters

        /// <summary>
        /// 
        /// Filter REST root service info response.
        /// IMPORTANT: the JSON structure returned by the REST handler root resource 
        /// differs from what you usually see as the response from the service REST endpoint.
        /// 
        /// </summary>
        /// <param name="restInput"></param>
        /// <param name="originalResponse"></param>
        /// <param name="authorizedLayerSet"></param>
        /// <returns>Filtered json</returns>
        private byte[] PostFilterRESTRoot (RestRequestParameters restInput, byte[] responseBytes, string responseProperties, out string newResponseProperties)
        {
            //modify these later as needed
            newResponseProperties = responseProperties;

            if (null == _authorizedLayerSet)
                return null; //Just returning null for brevity. In real production code, return error JSON and set proper responseProperties.

            //restInput is not used here, but may be used later as needed

            try
            {
                // Perform JSON filtering
                // Note: The JSON syntax can change and you might have to adjust this piece of code accordingly

                String originalResponse = System.Text.Encoding.UTF8.GetString(responseBytes);

                /*
                 * Remove unauthorized layer information from 1. Under 'contents' tag 2. Under 'resources' tag
                 * 2.1 'layers' 2.2 'tables' 2.3 'legend'
                 */

                JavaScriptSerializer sr = new JavaScriptSerializer { MaxJsonLength = int.MaxValue };
                var jsonResObj = sr.DeserializeObject(originalResponse) as IDictionary<string, object>;

                // Filter for 'contents' tag
                var contentsJO = jsonResObj["contents"] as IDictionary<string, object>;
                var layersJA = contentsJO["layers"] as object[];
                var updatedLayers = new List<object>();
                foreach (var layerO in layersJA)
                {
                    var layerJO = layerO as IDictionary<string, object>;
                    var id = layerJO["id"].ToString();
                    if (_authorizedLayerSet.Contains(id))
                    {
                        updatedLayers.Add(layerJO);
                    }
                }
                contentsJO["layers"] = updatedLayers.ToArray();


                // Filter for 'resources' tag, very simplified filtering, may fail if ordering changes
                var allResourcesJA = jsonResObj["resources"] as object[];
                var layersRJO = allResourcesJA.FirstOrDefault(e =>
                {
                    var jo = e as IDictionary<string, object>;
                    if (!jo.ContainsKey("name"))
                        return false;
                    var name = jo["name"].ToString();
                    return ("layers" == name);
                }) as IDictionary<string, object>;

                var tablesRJO = allResourcesJA.FirstOrDefault(e =>
                {
                    var jo = e as IDictionary<string, object>;
                    if (!jo.ContainsKey("name"))
                        return false;
                    var name = jo["name"].ToString();
                    return ("tables" == name);
                }) as IDictionary<string, object>;

                var legendRJO = allResourcesJA.FirstOrDefault(e =>
                {
                    var jo = e as IDictionary<string, object>;
                    if (!jo.ContainsKey("name"))
                        return false;
                    var name = jo["name"].ToString();
                    return ("legend" == name);
                }) as IDictionary<string, object>;

                //filter and replace layers
                var filteredLayerResourceJA = new List<object>();
                if (null != layersRJO)
                {
                    // Filter for 'resources -> layers -> resources' tag
                    var layerResourceJA = layersRJO["resources"] as object[];
                    foreach (var lrJO in layerResourceJA)
                    {
                        var lrJODict = lrJO as IDictionary<string, object>;
                        if (_authorizedLayerSet.Contains(lrJODict["name"].ToString()))
                        {
                            filteredLayerResourceJA.Add(lrJO);
                        }
                    }
                }
                layersRJO["resources"] = filteredLayerResourceJA.ToArray();

                //filter and replace tables
                var filteredTableResourceJA = new List<object>();
                if (null != tablesRJO)
                {
                    // Filter for 'resources -> tables -> resources' tag
                    var tableResourceJA = tablesRJO["resources"] as object[];
                    foreach (var tbJO in tableResourceJA)
                    {
                        var tbJODict = tbJO as IDictionary<string, object>;
                        if (_authorizedLayerSet.Contains(tbJODict["name"].ToString()))
                        {
                            filteredTableResourceJA.Add(tbJO);
                        }
                    }
                }
                tablesRJO["resources"] = filteredTableResourceJA.ToArray();

                //filter and replace legend layers
                var filteredLegendLayersRJA = new List<object>();
                if (null != legendRJO)
                {
                    // Filter for 'resources -> legend -> contents->layers' tag
                    var legendContentsJO = legendRJO["contents"] as IDictionary<string, object>;
                    var legendLayersJA = legendContentsJO["layers"] as object[];
                    foreach (var lgJO in legendLayersJA)
                    {
                        var lgJODict = lgJO as IDictionary<string, object>;
                        if (_authorizedLayerSet.Contains(lgJODict["layerId"].ToString()))
                        {
                            filteredLegendLayersRJA.Add(lgJO);
                        }
                    }
                    legendContentsJO["layers"] = filteredLegendLayersRJA.ToArray();
                }

                // Return the filter response
                return System.Text.Encoding.UTF8.GetBytes(sr.Serialize(jsonResObj));
            }
            catch (Exception ignore)
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="restInput"></param>
        /// <param name="originalResponse"></param>
        /// <param name="authorizedLayerSet"></param>
        /// <returns></returns>
        private byte[] PostFilterRootLayers(RestRequestParameters restInput, byte[] responseBytes, string responseProperties, out string newResponseProperties)
        {

            //modify these later as needed
            newResponseProperties = responseProperties;

            if (null == _authorizedLayerSet)
                return null; //Just returning null for brevity. In real production code, return error JSON and set proper responseProperties.

            //restInput is not used here, but may be used later as needed

            try
            {
                // Perform JSON filtering
                // Note: The JSON syntax can change and you might have to adjust this piece of code accordingly

                string originalResponse = System.Text.Encoding.UTF8.GetString(responseBytes);

                /*
                 * Remove unauthorized layer information from 1. Under 'contents' tag 2. Under 'resources' tag
                 * 2.1 'layers' 2.2 'tables' 2.3 'legend'
                 */

                JavaScriptSerializer sr = new JavaScriptSerializer { MaxJsonLength = int.MaxValue };
                var jsonResObj = sr.DeserializeObject(originalResponse) as IDictionary<string, object>;

                // Filter for 'contents' tag
                var layersJA = jsonResObj["layers"] as object[];
                var updatedLayers = new List<object>();
                foreach (var layerO in layersJA)
                {
                    var layerJO = layerO as IDictionary<string, object>;
                    var id = layerJO["id"].ToString();
                    if (_authorizedLayerSet.Contains(id))
                    {
                        updatedLayers.Add(layerJO);
                    }
                }
                jsonResObj["layers"] = updatedLayers.ToArray();
                // Return the filter response
                return System.Text.Encoding.UTF8.GetBytes(sr.Serialize(jsonResObj));
            }
            catch (Exception ignore)
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="restInput"></param>
        /// <param name="originalResponse"></param>
        /// <param name="authorizedLayerSet"></param>
        /// <returns></returns>
        private byte[] PostFilterRootLegend(RestRequestParameters restInput, byte[] responseBytes, string responseProperties, out string newResponseProperties)
        {

            //modify these later as needed
            newResponseProperties = responseProperties;

            if (null == _authorizedLayerSet)
                return null; //Just returning null for brevity. In real production code, return error JSON and set proper responseProperties.

            //restInput is not used here, but may be used later as needed

            try
            {
                // Perform JSON filtering
                // Note: The JSON syntax can change and you might have to adjust this piece of code accordingly
                /*
                 * Remove unauthorized layer information from 1. Under 'contents' tag 2. Under 'resources' tag
                 * 2.1 'layers' 2.2 'tables' 2.3 'legend'
                 */
                string originalResponse = System.Text.Encoding.UTF8.GetString(responseBytes);

                JavaScriptSerializer sr = new JavaScriptSerializer { MaxJsonLength = int.MaxValue };
                var jsonResObj = sr.DeserializeObject(originalResponse) as IDictionary<string, object>;

                // Filter for 'contents' tag
                var layersJA = jsonResObj["layers"] as object[];
                var updatedLayers = new List<object>();
                foreach (var layerO in layersJA)
                {
                    var layerJO = layerO as IDictionary<string, object>;
                    var id = layerJO["layerId"].ToString();
                    if (_authorizedLayerSet.Contains(id))
                    {
                        updatedLayers.Add(layerJO);
                    }
                }
                jsonResObj["layers"] = updatedLayers.ToArray();
                // Return the filter response
                return System.Text.Encoding.UTF8.GetBytes(sr.Serialize(jsonResObj));
            }
            catch (Exception ignore)
            {
                return null;
            }
        }

        #endregion

        #region SOAP interceptors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpMethod"></param>
        /// <param name="requestURL"></param>
        /// <param name="queryString"></param>
        /// <param name="Capabilities"></param>
        /// <param name="requestData"></param>
        /// <param name="responseContentType"></param>
        /// <param name="respDataType"></param>
        /// <returns></returns>
        public byte[] HandleStringWebRequest ( esriHttpMethod httpMethod, string requestURL,
            string queryString, string Capabilities, string requestData,
            out string responseContentType, out esriWebResponseDataType respDataType )
        {
            _serverLog.LogMessage(ServerLogger.msgType.infoStandard, _soiName + ".HandleStringWebRequest()",
                200, "Request received in Layer Access SOI for HandleStringWebRequest");

            /*
             * Add code to manipulate OGC (WMS, WFC, WCS etc) requests here	
             */

            IWebRequestHandler webRequestHandler = _restServiceSOI.FindRequestHandlerDelegate<IWebRequestHandler>();
            if (webRequestHandler != null)
            {
                var response = webRequestHandler.HandleStringWebRequest(
                        httpMethod, requestURL, queryString, Capabilities, requestData, out responseContentType, out respDataType);


                return response;
            }

            responseContentType = null;
            respDataType = esriWebResponseDataType.esriWRDTPayload;
            //Insert error response here.
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public byte[] HandleBinaryRequest ( ref byte[] request )
        {
            _serverLog.LogMessage(ServerLogger.msgType.infoStandard, _soiName + ".HandleBinaryRequest()",
                  200, "Request received in Layer Access SOI for HandleBinaryRequest");

            /*
             * Add code to manipulate Binary requests from desktop here
             */

            // Generate a set of authorized layers for the user
            var authorizedLayerSet = GetAuthorizedLayers(_SoapSOIHelper);

            IMessage requestMessage = SoapSOIHelper.ConvertBinaryRequestToMessage(request);

            SoapBinaryRequest filteredRequest = FilterSoapRequest(requestMessage, SoapRequestType.Binary, authorizedLayerSet) as SoapBinaryRequest;
            if (null == filteredRequest)
            {
                filteredRequest = new SoapBinaryRequest { Body = request };
            }

            IRequestHandler requestHandler = _restServiceSOI.FindRequestHandlerDelegate<IRequestHandler>();
            if (requestHandler != null)
            {
                var response = requestHandler.HandleBinaryRequest(filteredRequest.Body);

                // Perform filtering for GetServerInfoResponse
                // Convert the XML request into a generic IMessage
                IMessage responseMessage = SoapSOIHelper.ConvertBinaryRequestToMessage(response);
                // Get operation name
                String name = responseMessage.Name;
                if ("GetServerInfoResponse".Equals(name, StringComparison.CurrentCultureIgnoreCase))
                {
                    // Intercept the response and perform filtering
                    var filteredResponse = FilterSoapRequest(responseMessage, SoapRequestType.Binary, authorizedLayerSet) as SoapBinaryRequest;
                    if (filteredResponse != null)
                    {
                        response = filteredResponse.Body;
                    }
                }
                return response;
            }

            //Insert error response here.
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Capabilities"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public byte[] HandleBinaryRequest2 ( string Capabilities, ref byte[] request )
        {
            _serverLog.LogMessage(ServerLogger.msgType.infoStandard, _soiName + ".HandleBinaryRequest2()",
                  200, "Request received in Layer Access SOI for HandleBinaryRequest2");

            IMessage requestMessage = SoapSOIHelper.ConvertBinaryRequestToMessage(request);

            var authorizedLayerSet = GetAuthorizedLayers(_SoapSOIHelper);

            SoapBinaryRequest filteredRequest = FilterSoapRequest(requestMessage, SoapRequestType.Binary, authorizedLayerSet) as SoapBinaryRequest;
            if (null == filteredRequest)
            {
                filteredRequest = new SoapBinaryRequest { Body = request };
            }

            IRequestHandler2 requestHandler = _restServiceSOI.FindRequestHandlerDelegate<IRequestHandler2>();
            if (requestHandler != null)
            {
                var response = requestHandler.HandleBinaryRequest2(Capabilities, filteredRequest.Body);
                
                // Perform filtering for GetServerInfoResponse
                // Convert the XML request into a generic IMessage
                IMessage responseMessage = SoapSOIHelper.ConvertBinaryRequestToMessage(response);
                // Get operation name
                String name = responseMessage.Name;
                if ("GetServerInfoResponse".Equals(name, StringComparison.CurrentCultureIgnoreCase))
                {
                    // Intercept the response and perform filtering
                    var filteredResponse = FilterSoapRequest(responseMessage, SoapRequestType.Binary, authorizedLayerSet) as SoapBinaryRequest;
                    if (filteredResponse != null)
                    {
                        response = filteredResponse.Body;
                    }
                }
                return response;
            }

            //Insert error response here.
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Capabilities"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public string HandleStringRequest ( string Capabilities, string request )
        {
            _serverLog.LogMessage(ServerLogger.msgType.infoStandard, _soiName + ".HandleStringRequest()",
                   200, "Request received in Layer Access SOI for HandleStringRequest");

            // Convert the XML request into a generic IMessage
            IMessage requestMessage = SoapSOIHelper.ConvertStringRequestToMessage(request);

            // Generate a set of authorized layers for the user
            var authorizedLayerSet = GetAuthorizedLayers(_SoapSOIHelper);

            // Intercept the request and perform filtering
            var filteredRequest = FilterSoapRequest(requestMessage, SoapRequestType.Xml, authorizedLayerSet) as SoapStringRequest;
            if (filteredRequest == null)
            {
                filteredRequest = new SoapStringRequest { Body = request };
            }

            IRequestHandler requestHandler = _restServiceSOI.FindRequestHandlerDelegate<IRequestHandler>();
            if (requestHandler != null)
            {
                var response = requestHandler.HandleStringRequest(Capabilities, filteredRequest.Body);

                // Perform filtering for GetServerInfoResponse
                // Convert the XML request into a generic IMessage
                IMessage responseMessage = SoapSOIHelper.ConvertStringRequestToMessage(response);
                // Get operation name
                String name = responseMessage.Name;
                if ("GetServerInfoResponse".Equals(name, StringComparison.CurrentCultureIgnoreCase))
                {
                    // Intercept the response and perform filtering
                    var filteredResponse = FilterSoapRequest(responseMessage, SoapRequestType.Xml, authorizedLayerSet) as SoapStringRequest;
                    if (filteredResponse != null)
                    {
                        response = filteredResponse.Body;
                    }
                }
                return response;
            }

            //Insert error response here.
            return null;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="inRequest"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        private SoapRequest FilterSoapRequest ( IMessage inRequest, SoapRequestType mode, HashSet<string> authorizedLayerSet )
        {
            // Get operation name
            String name = inRequest.Name;

            // Apply filter only on those operations we care about
            var nameInLowerCase = name.ToLower();
            switch (nameInLowerCase)
            {
                case "find":
                    inRequest = FilterLayerIds(inRequest, mode, authorizedLayerSet);
                    break;
                case "exportmapimage":
                    inRequest = FilterMapDescription(inRequest, mode, authorizedLayerSet);
                    break;
                case "identify":
                    inRequest = FilterLayerIds(inRequest, mode, authorizedLayerSet);
                    break;
                case "getlegendinfo":
                    inRequest = FilterLayerIds(inRequest, mode, authorizedLayerSet);
                    break;
                case "getserverinforesponse":
                    inRequest = FilterGetServerInfoResponse(inRequest, mode, authorizedLayerSet);
                    break;
            }

            return SoapRequestFactory.Create(mode, inRequest);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inRequest"></param>
        /// <param name="mode"></param>
        /// <param name="authorizedLayerSet"></param>
        /// <returns></returns>
        private IMessage FilterLayerIds ( IMessage inRequest, SoapRequestType mode, HashSet<String> authorizedLayerSet )
        {
            // 1. Find the index of the layers parameter
            // 2. Get the value for the interested parameter
            // 3. Manipulate it
            // 4. Put it back into IMessage

            int idx = -1;
            IXMLSerializeData inRequestData = inRequest.Parameters;
            try
            {
                idx = inRequestData.Find("LayerIDs");
            }
            catch (Exception ignore)
            {
            }

            LongArray layerIdInLA = null;
            if (idx >= 0)
            {
                // Get all the requested layers
                layerIdInLA = (LongArray)inRequestData.GetObject(idx, inRequest.NamespaceURI, "ArrayOfInt");

                // Perform filtering based on access to different layers. 
                // Remove restricted ids in-place
                for (int i = layerIdInLA.Count - 1; i >= 0; i--)
                {

                    if (!authorizedLayerSet.Contains(layerIdInLA.Element[i].ToString()))
                    {
                        layerIdInLA.Remove(i);
                    }
                }
            }
            else //no LayerIDs specified, attaching authorized layer list instead
            {
                layerIdInLA = new LongArrayClass();
                foreach (var goodLayerId in authorizedLayerSet)
                {
                    layerIdInLA.Add(Int32.Parse(goodLayerId));
                }
                inRequestData.AddObject("LayerIDs", layerIdInLA);
            }

            // If binary request we dont have to create and copy in a new Message object
            if (mode == SoapRequestType.Binary)
            {
                return inRequest;
            }

            // Create new request message
            IMessage modifiedInRequest = _SoapSOIHelper.CreateNewIMessage(inRequest);
            IXMLSerializeData modifiedInRequestData = modifiedInRequest.Parameters;

            // Put all parameters back in IMessage
            for (int i = 0; i < inRequestData.Count; i++)
            {
                if (_SoapSOIHelper.GetSoapOperationParameterName(inRequest.Name, i).Equals("LayerIDs", StringComparison.CurrentCultureIgnoreCase))
                {
                    // Add the modified MapDescription
                    _SoapSOIHelper.AddObjectToXMLSerializeData(_SoapSOIHelper.GetSoapOperationParameterName(inRequest.Name, i),
                      layerIdInLA, _SoapSOIHelper.GetSoapOperationParameterTypeName(inRequest.Name, i), modifiedInRequestData);
                }
                else
                {
                    /*
                     * Add other parameters as is. Here we are using the SOI helper to add and get parameters
                     * because we don't care about the type we just want to copy from one IMessage object to
                     * another.
                     */
                    _SoapSOIHelper.AddObjectToXMLSerializeData(
                      _SoapSOIHelper.GetSoapOperationParameterName(inRequest.Name, i),
                      _SoapSOIHelper.GetObjectFromXMLSerializeData(i, inRequest.NamespaceURI,
                          _SoapSOIHelper.GetSoapOperationParameterTypeName(inRequest.Name, i), inRequestData),
                      _SoapSOIHelper.GetSoapOperationParameterTypeName(inRequest.Name, i), modifiedInRequestData);
                }
            }

            return modifiedInRequest;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inRequest"></param>
        /// <param name="mode"></param>
        /// <param name="authorizedLayerSet"></param>
        /// <returns></returns>
        private IMessage FilterMapDescription ( IMessage inRequest, SoapRequestType mode, HashSet<string> authorizedLayerSet )
        {
            // 1. Find the index of the MapDescription parameter
            // 2. Get the value for the interested parameter
            // 3. Manipulate it
            // 4. Put it back into IMessage

            // Get the parameters out from the request object
            int idx = -1;
            IXMLSerializeData inRequestData = inRequest.Parameters;
            idx = inRequestData.Find("MapDescription");

            MapDescription md =
                (MapDescription)inRequestData.GetObject(idx, inRequest.NamespaceURI,
                    _SoapSOIHelper.GetSoapOperationParameterTypeName(inRequest.Name, idx));
            // Manipulate the MapDescription to perform layer level security
            ILayerDescriptions lds = md.LayerDescriptions;
            for (int i = 0; i < lds.Count; i++)
            {
                ILayerDescription ld = lds.Element[i];
                if (!authorizedLayerSet.Contains(ld.ID.ToString()))
                {
                    ld.Visible = false;
                }
            }

            // If binary request we dont have to create and copy in a new Message object
            if (mode == SoapRequestType.Binary)
            {
                return inRequest;
            }

            // Create new request message
            IMessage modifiedInRequest = _SoapSOIHelper.CreateNewIMessage(inRequest);
            IXMLSerializeData modifiedInRequestData = modifiedInRequest.Parameters;

            // Put all parameters back in IMessage
            for (int i = 0; i < inRequestData.Count; i++)
            {
                if (_SoapSOIHelper.GetSoapOperationParameterName(inRequest.Name, i).Equals("MapDescription", StringComparison.CurrentCultureIgnoreCase))
                {
                    // Add the modified MapDescription
                    modifiedInRequestData.AddObject(_SoapSOIHelper.GetSoapOperationParameterName(inRequest.Name, i), md);
                }
                else
                {
                    /*
                     * Add other parameters as is. Here we are using the SOI helper to add and get parameters
                     * because we don't care about the type we just want to copy from one IMessage object to
                     * another.
                     */
                    modifiedInRequestData.AddObject(
                        _SoapSOIHelper.GetSoapOperationParameterName(inRequest.Name, i),
                        inRequestData.GetObject(i, inRequest.NamespaceURI,
                            _SoapSOIHelper.GetSoapOperationParameterTypeName(inRequest.Name, i)));
                }
            }

            return modifiedInRequest;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inRequest"></param>
        /// <param name="mode"></param>
        /// <param name="authorizedLayerSet"></param>
        /// <returns></returns>
        private IMessage FilterGetServerInfoResponse ( IMessage inRequest, SoapRequestType mode, HashSet<string> authorizedLayerSet )
        {
            int idx = -1;
            IXMLSerializeData inRequestData = inRequest.Parameters;
            idx = inRequestData.Find("Result");

            // Get MapServerInfo
            MapServerInfo mapServerInfo = (MapServerInfo)inRequestData.GetObject(idx, inRequest.NamespaceURI,
                    "MapServerInfo");

            // Perform filtering based on access to different layers
            IMapLayerInfos layerInfos = mapServerInfo.MapLayerInfos;
            for (int i = layerInfos.Count - 1; i >= 0; i--)
            {
                if (!authorizedLayerSet.Contains(layerInfos.Element[i].ID.ToString()))
                {
                    layerInfos.Remove(i);
                }
            }

            IMapDescription mapDescription = mapServerInfo.DefaultMapDescription;
            ILayerDescriptions lds = mapDescription.LayerDescriptions;
            for (int i = lds.Count - 1; i >= 0; i--)
            {
                if (!authorizedLayerSet.Contains(lds.Element[i].ID.ToString()))
                {
                    lds.Remove(i);
                }
            }

            // If binary request we don't have to create and copy in a new Message object
            if (mode == SoapRequestType.Binary)
            {
                return inRequest;
            }

            // Create new request message
            IMessage modifiedInRequest = _SoapSOIHelper.CreateNewIMessage(inRequest);
            IXMLSerializeData modifiedInRequestData = modifiedInRequest.Parameters;

            // Put all parameters back in IMessage
            for (int i = 0; i < inRequestData.Count; i++)
            {
                if (_SoapSOIHelper.GetSoapOperationParameterName(inRequest.Name, i).Equals("Result", StringComparison.CurrentCultureIgnoreCase))
                {
                    // Add the modified MapDescription
                    modifiedInRequestData.AddObject(_SoapSOIHelper.GetSoapOperationParameterName(inRequest.Name, i), mapServerInfo);
                }
                else
                {
                    /*
                     * Add other parameters as is. Here we are using the SOI helper to add and get parameters
                     * because we don't care about the type we just want to copy from one IMessage object to
                     * another.
                     */
                    modifiedInRequestData.AddObject(
                        _SoapSOIHelper.GetSoapOperationParameterName(inRequest.Name, i),
                        inRequestData.GetObject(i, inRequest.NamespaceURI,
                            _SoapSOIHelper.GetSoapOperationParameterTypeName(inRequest.Name, i)));
                }
            }

            return modifiedInRequest;
        }

        #endregion

        #region Utility code

        /// <summary>
        /// Remove unauthorized layers from request. 
        /// </summary>
        /// <param name="requestedLayers">layer user is requesting information from</param>
        /// <param name="authorizedLayers">layers user is authorized to fetch information from</param>
        /// <returns></returns>
        private static String RemoveUnauthorizedLayersFromRequestedLayers (
                String requestedLayers, HashSet<String> authorizedLayers )
        {
            if (0 == authorizedLayers.Count)
                return "-1";

            // requested layers
            IEnumerable<String> requestedLayersList = null;
            try
            {
                requestedLayersList = requestedLayers.Split(new[] { ',' }).Select(e => e.Trim());
            }
            catch (Exception ignore) { }

            if (authorizedLayers != null)
            {
                var filteredLayers = requestedLayersList.Where(e => authorizedLayers.Contains(e));
                if (!filteredLayers.Any())
                    return "-1";

                return String.Join(",", filteredLayers.ToArray());
            }
            return "-1";
        }


        /// <summary>
        ///  Read permission information from disk
        /// </summary>
        /// <param name="fileName">path and name of the file to read permissions from</param>
        /// <returns></returns>
        private Dictionary<String, String> ReadPermissionFile ( String fileName )
        {
            // read the permissions file
            Dictionary<String, String> permissionMap = new Dictionary<String, String>();
            try
            {

                if (!File.Exists(fileName))
                    return permissionMap;

                String jsonStr = File.ReadAllText(fileName);


                var json = new ESRI.ArcGIS.SOESupport.JsonObject(jsonStr);
                System.Object[] permissions = null;
                // create a map of permissions
                // read the permissions array
                if (!json.TryGetArray("permissions", out permissions) || permissions == null)
                    return permissionMap;


                // add to map
                foreach (var permsObj in permissions)
                {
                    ESRI.ArcGIS.SOESupport.JsonObject permsJO = permsObj as ESRI.ArcGIS.SOESupport.JsonObject;
                    if (null == permsJO) continue;

                    // get the fqsn or service name
                    String fqsn = string.Empty;
                    permsJO.TryGetString("fqsn", out fqsn);
                    // read the permission for that service
                    System.Object[] permArray = null;
                    if (!permsJO.TryGetArray("permission", out permArray) || permArray == null)
                        continue;

                    foreach (var permObj in permArray)
                    {
                        ESRI.ArcGIS.SOESupport.JsonObject permJO = permObj as ESRI.ArcGIS.SOESupport.JsonObject;
                        String role = string.Empty;
                        if (!permJO.TryGetString("role", out role))
                            continue;

                        String authorizedLayers = string.Empty;
                        permJO.TryGetString("authorizedLayers", out authorizedLayers); //may be empty or null
                        permissionMap.Add(fqsn + "." + role, authorizedLayers);
                    }
                }
            }
            catch (Exception ignore)
            {
                //TODO error handling
            }
            return permissionMap;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="soi"></param>
        /// <returns></returns>
        private HashSet<string> GetAuthorizedLayers ( SOIBase soi )
        {
            var userRoleSet = soi.GetRoleInformation(soi.ServerEnvironment.UserInfo);
            HashSet<String> authorizedLayerSet = null;

            if (null == userRoleSet)
                return authorizedLayerSet;

            /*
            * Generate a set of authorized layers for the user
            */

            var fullServiceName = _serverObject.ConfigurationName + "." + _serverObject.TypeName;

            var removeSpacesRegEx = new Regex("\\s+");
            var authorizedRoles = userRoleSet.Where(role=>_servicePermissionMap.ContainsKey(fullServiceName + "." + role));
            var authorizedLayerList = authorizedRoles.SelectMany(role => removeSpacesRegEx.
                                                                                  Replace(_servicePermissionMap[fullServiceName + "." + role], "").
                                                                                  Split(','));
            return new HashSet<string>(authorizedLayerList);
        }
        #endregion
    }
}
