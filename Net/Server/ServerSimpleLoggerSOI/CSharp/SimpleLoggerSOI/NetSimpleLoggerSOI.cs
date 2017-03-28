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

using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Server;
using ESRI.ArcGIS.SOESupport;
using ESRI.ArcGIS.SOESupport.SOI;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace NetSimpleLoggerSOI
{

    [ComVisible(true)]
    [Guid("E911E5BE-9B35-481B-A8CA-3D29CC431817")]
    [ClassInterface(ClassInterfaceType.None)]
    [ServerObjectInterceptor("MapServer",
        Description = "DotNet Simple Logger REST SOI Example",
        DisplayName = "DotNet Simple Logger REST SOI Example",
        Properties = "")]
    public class NetSimpleLoggerSOI : IServerObjectExtension, IRESTRequestHandler, IWebRequestHandler, IRequestHandler2, IRequestHandler
    {
        private string _soiName;
        private IServerObjectHelper _soHelper;
        private ServerLogger _serverLog;
        private RestSOIHelper _restSOIHelper;


        public NetSimpleLoggerSOI()
        {
            _soiName = this.GetType().Name;
        }

        public void Init(IServerObjectHelper pSOH)
        {
            _soHelper = pSOH;
            _serverLog = new ServerLogger();
            _restSOIHelper = new RestSOIHelper(pSOH);

            _serverLog.LogMessage(ServerLogger.msgType.infoStandard, _soiName + ".init()", 200, "Initialized " + _soiName + " SOI.");
        }

        public void Shutdown()
        {
            _serverLog.LogMessage(ServerLogger.msgType.infoStandard, _soiName + ".init()", 200, "Shutting down " + _soiName + " SOI.");
        }

        public string GetSchema()
        {
            IRESTRequestHandler restRequestHandler = _restSOIHelper.FindRequestHandlerDelegate<IRESTRequestHandler>();
            if (restRequestHandler == null)
                throw new RestErrorException("Service handler not found");

            return restRequestHandler.GetSchema();
        }

        public byte[] HandleRESTRequest(string Capabilities, string resourceName, string operationName,
            string operationInput, string outputFormat, string requestProperties, out string responseProperties)
        {
            try
            {

                responseProperties = null;
                _serverLog.LogMessage(ServerLogger.msgType.infoStandard, _soiName + ".HandleRESTRequest()",
                    200, "Request received in Server Object Interceptor for handleRESTRequest");

                /*
                * Add code to manipulate REST requests here
                */

                // Find the correct delegate to forward the request too
                IRESTRequestHandler restRequestHandler = _restSOIHelper.FindRequestHandlerDelegate<IRESTRequestHandler>();
                if (restRequestHandler == null)
                    throw new RestErrorException("Service handler not found");

                return restRequestHandler.HandleRESTRequest(
                        Capabilities, resourceName, operationName, operationInput,
                        outputFormat, requestProperties, out responseProperties);
            }
            catch (RestErrorException e)
            {
                responseProperties = "{\"Content-Type\":\"text/plain;charset=utf-8\"}";
                return System.Text.Encoding.UTF8.GetBytes(e.Message);
            }
        }

        public byte[] HandleStringWebRequest(esriHttpMethod httpMethod, string requestURL, 
            string queryString, string Capabilities, string requestData, 
            out string responseContentType, out esriWebResponseDataType respDataType)
        {
            _serverLog.LogMessage(ServerLogger.msgType.infoStandard, _soiName + ".HandleStringWebRequest()",
                200, "Request received in Server Object Interceptor for HandleStringWebRequest");

            /*
             * Add code to manipulate requests here
             */

            IWebRequestHandler webRequestHandler = _restSOIHelper.FindRequestHandlerDelegate<IWebRequestHandler>();
            if (webRequestHandler != null)
            {
                return webRequestHandler.HandleStringWebRequest(
                        httpMethod, requestURL, queryString, Capabilities, requestData, out responseContentType, out respDataType);
            }

            responseContentType = null;
            respDataType = esriWebResponseDataType.esriWRDTPayload;
            //Insert error response here.
            return null;
        }

        public byte[] HandleBinaryRequest(ref byte[] request)
        {
            _serverLog.LogMessage(ServerLogger.msgType.infoStandard, _soiName + ".HandleBinaryRequest()",
                  200, "Request received in Server Object Interceptor for HandleBinaryRequest");

            /*
             * Add code to manipulate requests here
             */

            IRequestHandler requestHandler = _restSOIHelper.FindRequestHandlerDelegate<IRequestHandler>();
            if (requestHandler != null)
            {
                return requestHandler.HandleBinaryRequest(request);
            }

            //Insert error response here.
            return null;
        }

        public byte[] HandleBinaryRequest2(string Capabilities, ref byte[] request)
        {
            _serverLog.LogMessage(ServerLogger.msgType.infoStandard, _soiName + ".HandleBinaryRequest2()",
                  200, "Request received in Server Object Interceptor for HandleBinaryRequest2");

            /*
             * Add code to manipulate requests here
             */

            IRequestHandler2 requestHandler = _restSOIHelper.FindRequestHandlerDelegate<IRequestHandler2>();
            if (requestHandler != null)
            {
                return requestHandler.HandleBinaryRequest2(Capabilities, request);
            }

            //Insert error response here.
            return null;
        }

        public string HandleStringRequest(string Capabilities, string request)
        {
            _serverLog.LogMessage(ServerLogger.msgType.infoStandard, _soiName + ".HandleStringRequest()",
                   200, "Request received in Server Object Interceptor for HandleStringRequest");

            /*
             * Add code to manipulate requests here
             */

            IRequestHandler requestHandler = _restSOIHelper.FindRequestHandlerDelegate<IRequestHandler>();
            if (requestHandler != null)
            {
                return requestHandler.HandleStringRequest(Capabilities, request);
            }

            //Insert error response here.
            return null;
        }
    }
}
