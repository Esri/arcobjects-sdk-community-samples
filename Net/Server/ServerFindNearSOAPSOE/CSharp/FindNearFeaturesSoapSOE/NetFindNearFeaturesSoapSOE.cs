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

using System.Runtime.InteropServices;

using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Server;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Carto;

using ESRI.ArcGIS.SOESupport;

namespace NetFindNearFeaturesSoapSOE
{
    [ComVisible(true)]
    [Guid("ff21a501-5eb9-406a-aab1-29389d25b868")]
    [ClassInterface(ClassInterfaceType.None)]
    [ServerObjectExtension("MapServer",
        AllCapabilities = "GetInfo,FindFeatures,DemoCustomObject,DemoArrayOfCustomObjects",
        DefaultCapabilities = "GetInfo,DemoCustomObject,DemoArrayOfCustomObjects",
        Description = ".NET Find Near Features SOAP SOE Sample",
        DisplayName = ".NET Find Near Features Soap SOE",
        Properties = "",
        SupportsREST = false,
        SupportsSOAP = true,
        SOAPNamespaceURI = "http://examples.esri.com/schemas/NetFindNearFeaturesSoapSOE/1.0")]
    public class NetFindNearFeaturesSoapSOE : IRequestHandler2, IServerObjectExtension, IObjectConstruct
    {
        private IServerObjectHelper serverObjectHelper;
        private ServerLogger logger;
        private IPropertySet configProps;

        IRequestHandler2 reqHandler;

        public NetFindNearFeaturesSoapSOE()
        {
            SoapCapabilities soapCaps = new SoapCapabilities();
            soapCaps.AddMethod("GetLayerInfos", "getInfo");
            soapCaps.AddMethod("FindNearFeatures", "findFeatures");
            soapCaps.AddMethod("DemoCustomObjectInput", "DemoCustomObject");
            soapCaps.AddMethod("DemoArrayOfCustomObjectsInput", "DemoArrayOfCustomObjects");

            logger = new ServerLogger();

            SoeSoapImpl soapImpl = new SoeSoapImpl(Constants.SOEName, soapCaps, HandleSoapMessage);
            reqHandler = (IRequestHandler2)soapImpl;
        }

        //IServerObjectExtension
        public void Init(IServerObjectHelper pSOH)
        {
            serverObjectHelper = pSOH;
        }

        public void Shutdown()
        {
            serverObjectHelper = null;
        }


        //IObjectConstruct 
        public void Construct(IPropertySet props)
        {
            logger.LogMessage(ServerLogger.msgType.infoSimple, QualifiedMethodName(Constants.SOEName, "Construct"), -1, "Construct starting");

            configProps = props;

            logger.LogMessage(ServerLogger.msgType.infoSimple, QualifiedMethodName(Constants.SOEName, "Construct"), -1, "Construct finishing");
        }

        //IRequestHandler
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

        public void HandleSoapMessage(IMessage reqMsg, IMessage respMsg)
        {
            string methodName = reqMsg.Name;

            if (string.Compare(methodName, "GetLayerInfos", true) == 0)
                GetLayerInfos(reqMsg, respMsg);

            else if (string.Compare(methodName, "FindNearFeatures", true) == 0)
                FindNearFeatures(reqMsg, respMsg);

            else if (string.Compare(methodName, "DemoCustomObjectInput", true) == 0)
                DemoCustomObjectInput(reqMsg, respMsg);

            else if (string.Compare(methodName, "DemoArrayOfCustomObjectsInput", true) == 0)
                DemoArrayOfCustomObjectsInput(reqMsg, respMsg);

            else
                throw new ArgumentException("Method not supported: " + QualifiedMethodName(Constants.SOEName, methodName));
        }

        private string QualifiedMethodName(string soeName, string methodName)
        {
            return soeName + "." + methodName;
        }

        #region wrapperMethods

        private void GetLayerInfos(IMessage reqMsg, IMessage respMsg)
        {
            //no input parameters expected in request 

            CustomLayerInfos resultPropSet = GetLayerInfos();

            respMsg.Name = "GetLayerInfosResponse";
            respMsg.NamespaceURI = Constants.SOENamespaceURI;
            respMsg.Parameters.AddObject("Result", resultPropSet);
        }

        private void FindNearFeatures(IMessage reqMsg, IMessage respMsg)
        {
            IXMLSerializeData reqParams = reqMsg.Parameters;

            int layerID = reqParams.GetInteger(FindParam("LayerID", reqParams));

            IPoint location = (IPoint)reqParams.GetObject(FindParam("Location", reqParams), Constants.ESRINamespaceURI, "PointN");

            double distance = reqParams.GetDouble(FindParam("Distance", reqParams));

            IRecordSet recordSet = FindNearFeatures(layerID, location, distance);

            respMsg.Name = "FindNearFeaturesResponse";
            respMsg.NamespaceURI = Constants.SOENamespaceURI;
            respMsg.Parameters.AddObject("Result", recordSet);
        }


        private void DemoCustomObjectInput(IMessage reqMsg, IMessage respMsg)
        {
            IXMLSerializeData reqParams = reqMsg.Parameters;

            var layerInfo =
                (CustomLayerInfo)
                reqParams.GetObject(FindParam("layerInfo", reqParams), Constants.SOENamespaceURI, "CustomLayerInfo");

            respMsg.Name = "DemoCustomObjectInputResponse";
            respMsg.NamespaceURI = Constants.SOENamespaceURI;
            respMsg.Parameters.AddObject("Result", layerInfo);
        }


        private void DemoArrayOfCustomObjectsInput(IMessage reqMsg, IMessage respMsg)
        {
            IXMLSerializeData reqParams = reqMsg.Parameters;

            var layerInfos = (CustomLayerInfos)reqParams.GetObject(FindParam("layerInfos", reqParams), Constants.SOENamespaceURI, "ArrayOfCustomLayerInfo");

            respMsg.Name = "DemoArrayOfCustomObjectsInputResponse";
            respMsg.NamespaceURI = Constants.SOENamespaceURI;
            respMsg.Parameters.AddObject("Result", layerInfos);
        }

        #endregion wrapperMethods


        #region businessLogicMethods

        private CustomLayerInfos GetLayerInfos()
        {
            IMapServer3 mapServer = serverObjectHelper.ServerObject as IMapServer3;
            if (mapServer == null)
                throw new Exception("Unable to access the map server.");

            IMapServerInfo msInfo = mapServer.GetServerInfo(mapServer.DefaultMapName);
            IMapLayerInfos layerInfos = msInfo.MapLayerInfos;
            int c = layerInfos.Count;

            CustomLayerInfos customLayerInfos = new CustomLayerInfos(Constants.SOENamespaceURI);

            for (int i = 0; i < c; i++)
            {
                IMapLayerInfo layerInfo = layerInfos.get_Element(i);

                CustomLayerInfo customLayerInfo = new CustomLayerInfo();
                customLayerInfo.Name = layerInfo.Name;
                customLayerInfo.ID = layerInfo.ID;
                customLayerInfo.Extent = layerInfo.Extent;

                customLayerInfos.Add(customLayerInfo);
            }

            return customLayerInfos;
        }

        private IRecordSet FindNearFeatures(int layerID, IPoint location, double distance)
        {
            IMapServer3 mapServer = serverObjectHelper.ServerObject as IMapServer3;
            if (mapServer == null)
                throw new Exception("Unable to access the map server.");

            IGeometry queryGeometry = ((ITopologicalOperator)location).Buffer(distance);

            ISpatialFilter filter = new SpatialFilterClass();
            filter.Geometry = queryGeometry;
            filter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;

            IQueryResultOptions resultOptions = new QueryResultOptionsClass();
            resultOptions.Format = esriQueryResultFormat.esriQueryResultRecordSetAsObject;

            IMapTableDescription tableDesc = GetTableDesc(mapServer, layerID);

            IQueryResult result = mapServer.QueryData(mapServer.DefaultMapName, tableDesc, filter, resultOptions);

            return (RecordSet)result.Object;
        }
        #endregion businessLogicMethods


        #region helperMethods
        private int FindParam(string parameterName, IXMLSerializeData msgParams)
        {
            int idx = msgParams.Find(parameterName);
            if (idx == -1)
                throw new ArgumentNullException(parameterName);
            return idx;
        }

        private IMapTableDescription GetTableDesc(IMapServer3 mapServer, int layerID)
        {
            ILayerDescriptions layerDescs = mapServer.GetServerInfo(mapServer.DefaultMapName).DefaultMapDescription.LayerDescriptions;
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

        private IMapLayerInfo GetLayerInfo(IMapServer3 mapServer, int layerID)
        {
            IMapLayerInfo layerInfo;

            IMapLayerInfos layerInfos = mapServer.GetServerInfo(mapServer.DefaultMapName).MapLayerInfos;
            long c = layerInfos.Count;

            for (int i = 0; i < c; i++)
            {
                layerInfo = layerInfos.get_Element(i);
                if (layerInfo.ID == layerID)
                    return layerInfo;
            }

            throw new ArgumentOutOfRangeException("layerID");
        }
        #endregion helperMethods


    } //class 
}
