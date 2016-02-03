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

using System.Runtime.InteropServices;

using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Server;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Carto;

using ESRI.ArcGIS.SOESupport;

namespace NetSimpleSoapSOE
{
    [ComVisible(true)]
    [Guid("a27db62f-c6bc-4870-a23e-9a6d75bc153b")]
    [ClassInterface(ClassInterfaceType.None)]
    [ServerObjectExtension("MapServer",
        AllCapabilities = "",
        DefaultCapabilities = "",
        Description = ".NET Simple SOAP SOE Sample",
        DisplayName = ".NET Simple SOAP SOE",
        Properties = "",
        SupportsREST = false,
        SupportsSOAP = true, 
        SOAPNamespaceURI= "http://examples.esri.com/schemas/NetSimpleSoapSOE/1.0")]
    public class NetSimpleSoapSOE : IRequestHandler2, IServerObjectExtension, IObjectConstruct
    {
        private const string c_SOEName = "SimpleSoapSOE";
        internal static string c_SOENamespace = "http://examples.esri.com/schemas/NetSimpleSoapSOE/1.0";
        internal static string c_ESRINamespace = "http://www.esri.com/schemas/ArcGIS/10.1";

        IRequestHandler2 reqHandler;

        public NetSimpleSoapSOE()
        {
            SoapCapabilities soapCapabilities = new SoapCapabilities();
            soapCapabilities.AddMethod("EchoInput", "Echo");

            SoeSoapImpl soapImpl = new SoeSoapImpl(c_SOEName, soapCapabilities, HandleSoapMessage);
            reqHandler = (IRequestHandler2)soapImpl;
        }

        #region IServerObjectExtension
        public void Init(IServerObjectHelper pSOH) { }

        public void Shutdown() { }
        #endregion

        #region IObjectConstruct
        public void Construct(IPropertySet props) { }
        #endregion

        #region IRequestHandler
        public byte[] HandleBinaryRequest(ref byte[] request)
        {
            throw new NotImplementedException();
        }

        public byte[] HandleBinaryRequest2(string Capabilities, ref byte[] request)
        {
            throw new NotImplementedException();
        }

        public string HandleStringRequest(string Capabilities, string request)
        {
            return reqHandler.HandleStringRequest(Capabilities, request);
        }
        #endregion

        public void HandleSoapMessage(IMessage reqMsg, IMessage respMsg)
        {
            string methodName = reqMsg.Name;

            if (string.Compare(methodName, "EchoInput", true) == 0)
                EchoInput(reqMsg, respMsg);

            else
                throw new ArgumentException(string.Format("Method not supported: {0}.{1}", c_SOEName, methodName));
        }

        private void EchoInput(IMessage reqMsg, IMessage respMsg)
        {
            IXMLSerializeData reqParams = reqMsg.Parameters;

            string inputString = reqParams.GetString(FindParam("Text", reqParams));

            respMsg.Name = "EchoInputResponse";
            respMsg.NamespaceURI = c_SOENamespace;
            respMsg.Parameters.AddString("Result", inputString);
        }

        private int FindParam(string parameterName, IXMLSerializeData msgParams)
        {
            int idx = msgParams.Find(parameterName);
            if (idx == -1)
                throw new ArgumentNullException(parameterName);
            return idx;
        }
    }
}
