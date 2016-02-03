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
using ESRI.ArcGIS.DataSourcesRaster;


//TODO: sign the project (project properties > signing tab > sign the assembly)
//      this is strongly suggested if the dll will be registered using regasm.exe <your>.dll /codebase


namespace ISSOERasterAccess
{
    [ComVisible(true)]
    [Guid("0C1F9104-404F-4307-8728-48BE3B3D5795")]
    [ClassInterface(ClassInterfaceType.None)]
    [ServerObjectExtension("ImageServer",
        AllCapabilities = "",
        DefaultCapabilities = "",
        Description = "Image Service SOE Sample",
        DisplayName = "Raster Access",
        Properties = "",
        SupportsREST = true,
        SupportsSOAP = false)]
    public class ISSOERasterAccess : IServerObjectExtension, IObjectConstruct, IRESTRequestHandler
    {
        private string _soename;

        private IPropertySet _configProps;
        private IServerObjectHelper _serverObjectHelper;
        private ServerLogger _logger;
        private IRESTRequestHandler _reqHandler;

        private IFeatureClass _mosaicCatalog;
        private bool _supportRasterItemAccess;
        public ISSOERasterAccess()
        {
            _soename = this.GetType().Name;
            _logger = new ServerLogger();
            _reqHandler = new SoeRestImpl(_soename, CreateRestSchema()) as IRESTRequestHandler;            
        }

        #region IServerObjectExtension Members

        public void Init(IServerObjectHelper pSOH)
        {
            _serverObjectHelper = pSOH;
            _logger.LogMessage(ServerLogger.msgType.infoStandard, _soename + ".Init", 200, "Initialized " + _soename + " SOE.");
        }

        public void Shutdown()
        {
            _logger.LogMessage(ServerLogger.msgType.infoStandard, "Shutdown", 8000, "Custom message: Shutting down the SOE");
            _serverObjectHelper = null;
            _logger = null;
            _mosaicCatalog = null;
        }

        #endregion

        #region IObjectConstruct Members

        public void Construct(IPropertySet props)
        {
            _configProps = props;
            IImageServerInit3 imageServer = (IImageServerInit3)_serverObjectHelper.ServerObject;
            IName mosaicName = imageServer.ImageDataSourceName;
            if (mosaicName is IMosaicDatasetName)
            {
                IMosaicDataset md = (IMosaicDataset)mosaicName.Open();
                _mosaicCatalog = md.Catalog;
                _supportRasterItemAccess = true;
            }
            else
                _supportRasterItemAccess = false;
        }

        #endregion

        #region IRESTRequestHandler Members

        public string GetSchema()
        {
            return _reqHandler.GetSchema();
        }

        public byte[] HandleRESTRequest(string Capabilities, string resourceName, string operationName, string operationInput, string outputFormat, string requestProperties, out string responseProperties)
        {
            return _reqHandler.HandleRESTRequest(Capabilities, resourceName, operationName, operationInput.ToLower(), outputFormat, requestProperties, out responseProperties);
        }

        #endregion

        private RestResource CreateRestSchema()
        {
            RestResource rootRes = new RestResource(_soename, false, RootResHandler);

            RestOperation getRasterStaticticsOper = new RestOperation("GetRasterStatistics",
                                                      new string[] { "ObjectID" },
                                                      new string[] { "json" },
                                                      GetRasterStatisticsOperHandler);

            rootRes.operations.Add(getRasterStaticticsOper);

            return rootRes;
        }

        private byte[] RootResHandler(NameValueCollection boundVariables, string outputFormat, string requestProperties, out string responseProperties)
        {
            responseProperties = null;

            JsonObject result = new JsonObject();
            result.AddString("Description", "Get raster item statistics in a mosaic dataset");
            result.AddBoolean("SupportRasterItemAccess", _supportRasterItemAccess);

            return Encoding.UTF8.GetBytes(result.ToJson());
        }

        private byte[] GetRasterStatisticsOperHandler(NameValueCollection boundVariables,
                                                  JsonObject operationInput,
                                                      string outputFormat,
                                                      string requestProperties,
                                                  out string responseProperties)
        {            
            _logger.LogMessage(ServerLogger.msgType.infoDetailed, _soename+ ".GetRasterStatistics",8000, "request received"); 
            if (!_supportRasterItemAccess)
                throw new ArgumentException("The image service does not have a catalog and does not support this operation");
            responseProperties = null;

            long? objectID;
            //case insensitive
            bool found = operationInput.TryGetAsLong("objectid", out objectID);
            if (!found || (objectID == null))
                throw new ArgumentNullException("ObjectID");
            IRasterCatalogItem rasterCatlogItem = null;
            IRasterBandCollection rasterBandsCol = null;
            IRasterStatistics statistics = null;
            try
            {
                rasterCatlogItem = _mosaicCatalog.GetFeature((int)objectID) as IRasterCatalogItem;
                if (rasterCatlogItem == null)
                {
                    _logger.LogMessage(ServerLogger.msgType.infoDetailed, _soename + ".GetRasterStatistics", 8000, "request finished with exception"); 
                    throw new ArgumentException("The input ObjectID does not exist");
                }
            }
            catch
            {
                _logger.LogMessage(ServerLogger.msgType.infoDetailed, _soename + ".GetRasterStatistics", 8000, "request finished with exception"); 
                throw new ArgumentException("The input ObjectID does not exist");
            }
            JsonObject result = new JsonObject();
            try
            {
                rasterBandsCol = (IRasterBandCollection)rasterCatlogItem.RasterDataset;
                List<object> maxvalues = new List<object>();
                List<object> minvalues = new List<object>();
                List<object> standarddeviationvalues = new List<object>();
                List<object> meanvalues = new List<object>();
                for (int i = 0; i < rasterBandsCol.Count; i++)
                {
                    statistics = rasterBandsCol.Item(i).Statistics;
                    maxvalues.Add(statistics.Maximum);
                    minvalues.Add(statistics.Minimum);
                    standarddeviationvalues.Add(statistics.StandardDeviation);
                    meanvalues.Add(statistics.Mean);
                    Marshal.ReleaseComObject(statistics);
                }

                result.AddArray("maxValues", maxvalues.ToArray());
                result.AddArray("minValues", minvalues.ToArray());
                result.AddArray("meanValues", meanvalues.ToArray());
                result.AddArray("stdvValues", standarddeviationvalues.ToArray());
            }
            catch 
            { 
                _logger.LogMessage(ServerLogger.msgType.infoDetailed, "GetRasterStatistics", 8000, "request completed. statistics does not exist");
            }
            finally
            {
                if (rasterBandsCol != null)
                    Marshal.ReleaseComObject(rasterBandsCol);
                if (rasterCatlogItem != null)
                    Marshal.ReleaseComObject(rasterCatlogItem);
            }
            _logger.LogMessage(ServerLogger.msgType.infoDetailed, _soename + ".GetRasterStatistics", 8000, "request completed successfully"); 
            return Encoding.UTF8.GetBytes(result.ToJson());
        }
    }
}
