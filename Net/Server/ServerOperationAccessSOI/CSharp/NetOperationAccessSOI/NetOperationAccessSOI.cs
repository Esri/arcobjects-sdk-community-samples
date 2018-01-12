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
using System.Linq;
using System.Runtime.InteropServices;
using System.Web.Script.Serialization;


//TODO: sign the project (project properties > signing tab > sign the assembly)
//      this is strongly suggested if the dll will be registered using regasm.exe <your>.dll /codebase


namespace NetOperationAccessSOI
{
 
    [ComVisible(true)]
    [Guid("98cf5590-1546-4ef7-afb7-5c990ee17160")]
    [ClassInterface(ClassInterfaceType.None)]
    [ServerObjectInterceptor("MapServer",
        Description = "DotNet SOI Example to control access to different operations of a sevice",
        DisplayName = "DotNet Operation Access SOI Example",
        Properties = "")]
    public class NetOperationAccessSOI : IServerObjectExtension, IRESTRequestHandler, IWebRequestHandler, IRequestHandler2, IRequestHandler
    {
        private string _soiName;
        private IServerObjectHelper _soHelper;
        private ServerLogger _serverLog;
        private RestSOIHelper _restSOIHelper;

        public NetOperationAccessSOI()
        {
            _soiName = this.GetType().Name;
        }

        public void Init(IServerObjectHelper pSOH)
        {
            _soHelper = pSOH;
            _serverLog = new ServerLogger();
            _restSOIHelper = new RestSOIHelper(pSOH);

            _serverLog.LogMessage(ServerLogger.msgType.infoStandard, _soiName + ".init()", 200, "Initialized " + _soiName + " SOE.");
        }

        public void Shutdown()
        {
            _serverLog.LogMessage(ServerLogger.msgType.infoStandard, _soiName + ".init()", 200, "Shutting down " + _soiName + " SOE.");
        }

        #region Access Filters

        /// <summary>
        /// Very basic authorization filter. 
        /// Uses hard-coded role list. 
        /// Only checks authorization on find, identify and export, all other operations are forbidden.
        /// </summary>
        /// <param name="operationName">REST operation name</param>
        /// <returns>Returns true if access is allowed</returns>
        private bool CheckAuthorization(string operationName)
        {
            if (string.IsNullOrEmpty(operationName))
                return true; //allow resource access
            /*
             * By default, block access for all users.
             */

            /*
             * List of roles that have access.
             * 
             * Here we have defined a single list to control access for all
             * operations but depending on the use case we can create per operation
             * level lists or even read this information from an external file.
             */
            var authorizedRoles = new HashSet<String> 
                                        { "gold", 
                                          "platinum" 
                                        };


            /*
             * List of operations we need to authorize,
             */
            var operationsToCheckForAuthorization = new HashSet<String>
                                        {
                                            "find",
                                            "identify",
                                            "export"
                                        };

            /*
             * Check if the user if authorized to perform the operation.
             * 
             * Note: Here we are checking for all valid Map Service operations. If
             * you need to use this SOI for a published Image Service you need to
             * extend this to cover all Image Service operations.
             */
            if (operationsToCheckForAuthorization.Contains(operationName.ToLower()))
            {
                /*
                 * Get all roles the user belongs to.
                 */
                var userRoleSet = GetRoleInformation();

                //Check if user role set intersection with the authorized role set contains any elements.
                //In other words, if one of user's roles is authorized.
                return userRoleSet.Intersect(authorizedRoles).Any();                
            }

            /*
            * We support only operations find, identify, export 
            * for all other operations we do not allow access.
            */
            return false;
            
        }

        #endregion

        #region REST interceptors

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
            responseProperties = null;
            try
            {

                _serverLog.LogMessage(ServerLogger.msgType.infoStandard, _soiName + ".HandleRESTRequest()",
                    200, "Request received in Operation Access SOI for handleRESTRequest");

                // Find the correct delegate to forward the request too
                IRESTRequestHandler restRequestHandler = _restSOIHelper.FindRequestHandlerDelegate<IRESTRequestHandler>();
                if (restRequestHandler == null || !CheckAuthorization(operationName))
                {
                    JavaScriptSerializer sr = new JavaScriptSerializer { MaxJsonLength = int.MaxValue };
                    var errorReturn = new Dictionary<string, object>
                                    {
                                        {"error", new Dictionary<string, object>
                                                {
                                                    {"code", 404},
                                                    {"message", "Not Found"}
                                                }
                                        }
                                    };

                    throw new RestErrorException(sr.Serialize(errorReturn));

                }

                return restRequestHandler.HandleRESTRequest(
                        Capabilities, resourceName, operationName, operationInput,
                        outputFormat, requestProperties, out responseProperties);
            }
            catch (RestErrorException restError)
            {
                responseProperties = "{\"Content-Type\":\"text/plain;charset=utf-8\"}";
                return System.Text.Encoding.UTF8.GetBytes(restError.Message);
            }   
        }

        #endregion

        #region SOAP interceptors

        public byte[] HandleStringWebRequest(esriHttpMethod httpMethod, string requestURL,
            string queryString, string Capabilities, string requestData,
            out string responseContentType, out esriWebResponseDataType respDataType)
        {
            _serverLog.LogMessage(ServerLogger.msgType.infoStandard, _soiName + ".HandleStringWebRequest()",
                200, "Request received in Operation Access SOI for HandleStringWebRequest");


            IWebRequestHandler webRequestHandler = _restSOIHelper.FindRequestHandlerDelegate<IWebRequestHandler>();
            if (webRequestHandler != null)
            {

                /*
                 * Add code to manipulate requests here
                 * Note: Intercepting and authorizing SOAP handler operation requests is not implemented.
                 */

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
                  200, "Request received in Operation Access SOI for HandleBinaryRequest");

            IRequestHandler requestHandler = _restSOIHelper.FindRequestHandlerDelegate<IRequestHandler>();
            if (requestHandler != null)
            {

                /*
                 * Add code to manipulate requests here
                 * Note: Intercepting and authorizing SOAP handler operation requests is not implemented.
                 */

                return requestHandler.HandleBinaryRequest(request);
            }

            //Insert error response here.
            return null;
        }

        public byte[] HandleBinaryRequest2(string Capabilities, ref byte[] request)
        {
            _serverLog.LogMessage(ServerLogger.msgType.infoStandard, _soiName + ".HandleBinaryRequest2()",
                  200, "Request received in Operation Access SOI for HandleBinaryRequest2");

            IRequestHandler2 requestHandler = _restSOIHelper.FindRequestHandlerDelegate<IRequestHandler2>();
            if (requestHandler != null)
            {
                /*
                 * Add code to manipulate requests here
                 * Note: Intercepting and authorizing SOAP handler operation requests is not implemented.
                 */

                return requestHandler.HandleBinaryRequest2(Capabilities, request);
            }

            //Insert error response here.
            return null;
        }

        public string HandleStringRequest(string Capabilities, string request)
        {
            _serverLog.LogMessage(ServerLogger.msgType.infoStandard, _soiName + ".HandleStringRequest()",
                   200, "Request received in Operation Access SOI for HandleStringRequest");

            IRequestHandler requestHandler = _restSOIHelper.FindRequestHandlerDelegate<IRequestHandler>();
            if (requestHandler != null)
            {
                /*
                 * Add code to manipulate requests here
                 * Note: Intercepting and authorizing SOAP handler operation requests is not implemented.
                 */

                return requestHandler.HandleStringRequest(Capabilities, request);
            }

            //Insert error response here.
            return null;
        }

        #endregion

        #region Utility code

        /// <summary>
        /// Get allowed roles for the user making a request.
        /// </summary>
        /// <returns>set of roles</returns>
        private HashSet<String> GetRoleInformation()
        {
            // Roles set
            HashSet<String> roles = new HashSet<String>();
            try
            {
                /*
                 *  Get the user information.
                 */
                IServerUserInfo userInfo = _restSOIHelper.ServerEnvironment.UserInfo;
                /*
                 *  Get information on the user making the call.
                 */
                String userName = userInfo.Name;
                /*
                 *  Get all roles user belongs to.
                 */
                IEnumBSTR rolesEnum = userInfo.Roles;
                if (rolesEnum != null)
                {
                    String role = rolesEnum.Next();
                    while (!string.IsNullOrEmpty(role))
                    {
                        roles.Add(role);
                        role = rolesEnum.Next();
                    }
                }
                return roles;
            }
            catch (Exception ignore)
            {
                //TODO error handling
            }
            return roles;
        }

        #endregion
    }
}
