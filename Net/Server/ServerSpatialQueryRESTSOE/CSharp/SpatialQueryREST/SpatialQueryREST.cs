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


//TODO: sign the project (project properties > signing tab > sign the assembly)
//      this is strongly suggested if the dll will be registered using regasm.exe <your>.dll /codebase


namespace SpatialQueryREST
{
    [ComVisible(true)]
    [Guid("0d9be731-0094-45ee-873a-db2c10c773b3")]
    [ClassInterface(ClassInterfaceType.None)]
    [ServerObjectExtension("MapServer",
        AllCapabilities = "",
        DefaultCapabilities = "",
        Description = ".NET Spatial Query REST SOE Sample",
        DisplayName = ".NET Spatial Query REST SOE",
        Properties = "FieldName=PRIMARY_;LayerName=veg;FilePath=NotSet",
        SupportsREST = true,
        SupportsSOAP = false)]
    public class SpatialQueryREST : IServerObjectExtension, IObjectConstruct, IRESTRequestHandler
    {
        private string soe_name;

        private IPropertySet configProps;
        private IServerObjectHelper serverObjectHelper;
        private ServerLogger logger;
        private IRESTRequestHandler reqHandler;
        private IFeatureClass m_fcToQuery;
        private string m_mapLayerNameToQuery;
        private string m_mapFieldToQuery;

        public SpatialQueryREST()
        {
            soe_name = this.GetType().Name;
            logger = new ServerLogger();
            reqHandler = new SoeRestImpl(soe_name, CreateRestSchema()) as IRESTRequestHandler;
        }

        #region IServerObjectExtension Members

        public void Init(IServerObjectHelper pSOH)
        {
            serverObjectHelper = pSOH;
        }


        public void Shutdown()
        {
            logger.LogMessage(ServerLogger.msgType.infoStandard, "Shutdown", 8000, "Custom message: Shutting down the SOE");
            soe_name = null;
            m_fcToQuery = null;
            m_mapFieldToQuery = null;
            serverObjectHelper = null;
            logger = null;
        }

        #endregion

        #region IObjectConstruct Members

        public void Construct(IPropertySet props)
        {
            configProps = props;
            // Read the properties.

            if (props.GetProperty("FieldName") != null)
            {
                m_mapFieldToQuery = props.GetProperty("FieldName") as string;
            }
            else
            {
                throw new ArgumentNullException();
            }
            if (props.GetProperty("LayerName") != null)
            {
                m_mapLayerNameToQuery = props.GetProperty("LayerName") as string;
            }
            else
            {
                throw new ArgumentNullException();
            }
            try
            {
                // Get the feature layer to be queried.
                // Since the layer is a property of the SOE, this only has to be done once.
                IMapServer3 mapServer = (IMapServer3)serverObjectHelper.ServerObject;
                string mapName = mapServer.DefaultMapName;
                IMapLayerInfo layerInfo;
                IMapLayerInfos layerInfos = mapServer.GetServerInfo(mapName).MapLayerInfos;
                // Find the index position of the map layer to query.
                int c = layerInfos.Count;
                int layerIndex = 0;
                for (int i = 0; i < c; i++)
                {
                    layerInfo = layerInfos.get_Element(i);
                    if (layerInfo.Name == m_mapLayerNameToQuery)
                    {
                        layerIndex = i;
                        break;
                    }
                }
                // Use IMapServerDataAccess to get the data
                IMapServerDataAccess dataAccess = (IMapServerDataAccess)mapServer;
                // Get access to the source feature class.
                m_fcToQuery = (IFeatureClass)dataAccess.GetDataSource(mapName, layerIndex);
                if (m_fcToQuery == null)
                {
                    logger.LogMessage(ServerLogger.msgType.error, "Construct", 8000, "SOE custom error: Layer name not found.");
                    return;
                }
                // Make sure the layer contains the field specified by the SOE's configuration.
                if (m_fcToQuery.FindField(m_mapFieldToQuery) == -1)
                {
                    logger.LogMessage(ServerLogger.msgType.error, "Construct", 8000, "SOE custom error: Field not found in layer.");
                }
            }
            catch
            {
                logger.LogMessage(ServerLogger.msgType.error, "Construct", 8000, "SOE custom error: Could not get the feature layer.");
            }
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
            RestResource rootRes = new RestResource(soe_name, false, RootResHandler);
            RestOperation spatialQueryOper = new RestOperation("SpatialQuery",
                                                      new string[] { "location", "distance" },
                                                      new string[] { "json" },
                                                      SpatialQueryOperationHandler);
            rootRes.operations.Add(spatialQueryOper);
            return rootRes;
        }

        private byte[] RootResHandler(NameValueCollection boundVariables, string outputFormat, string requestProperties, out string responseProperties)
        {
            responseProperties = null;

            JsonObject result = new JsonObject();

            return Encoding.UTF8.GetBytes(result.ToJson());
        }

        private byte[] SpatialQueryOperationHandler(NameValueCollection boundVariables,
                                                  JsonObject operationInput,
                                                      string outputFormat,
                                                      string requestProperties,
                                                  out string responseProperties)
        {
            responseProperties = null; 

            // Deserialize the location.
            JsonObject jsonPoint;
            if (!operationInput.TryGetJsonObject("location", out jsonPoint))
                throw new ArgumentNullException("location");
            IPoint location = Conversion.ToGeometry(jsonPoint,
                esriGeometryType.esriGeometryPoint) as IPoint;
            if (location == null)
                throw new ArgumentException("SpatialQueryREST: invalid location", "location");
            // Deserialize the distance.
            double? distance;
            if (!operationInput.TryGetAsDouble("distance", out distance) || !distance.HasValue)
                throw new ArgumentException("SpatialQueryREST: invalid distance", "distance");
            byte[] result = QueryPoint(location, distance.Value);
            return result;
        }

        private byte[] QueryPoint(ESRI.ArcGIS.Geometry.IPoint location, double distance)
        {
            if (distance <= 0.0)
                throw new ArgumentOutOfRangeException("distance");
            // Buffer the point.
            ITopologicalOperator topologicalOperator = (ESRI.ArcGIS.Geometry.ITopologicalOperator)location;
            IGeometry queryGeometry = topologicalOperator.Buffer(distance);
            // Query the feature class.
            ISpatialFilter spatialFilter = new ESRI.ArcGIS.Geodatabase.SpatialFilter();
            spatialFilter.Geometry = queryGeometry;
            spatialFilter.SpatialRel = ESRI.ArcGIS.Geodatabase.esriSpatialRelEnum.esriSpatialRelIntersects;
            spatialFilter.GeometryField = m_fcToQuery.ShapeFieldName;
            IFeatureCursor resultsFeatureCursor = m_fcToQuery.Search(spatialFilter, true);
            // Loop through the features, clip each geometry to the buffer
            // and total areas by attribute value.
            topologicalOperator = (ESRI.ArcGIS.Geometry.ITopologicalOperator)queryGeometry;
            int classFieldIndex = m_fcToQuery.FindField(m_mapFieldToQuery);
            // System.Collections.Specialized.ListDictionary summaryStatsDictionary = new System.Collections.Specialized.ListDictionary();
            Dictionary<string, double> summaryStatsDictionary = new Dictionary<string, double>();
            // Initialize a list to hold JSON geometries.
            List<JsonObject> jsonGeometries = new List<JsonObject>();

            IFeature resultsFeature = null;
            while ((resultsFeature = resultsFeatureCursor.NextFeature()) != null)
            {
                // Clip the geometry.
                IPolygon clippedResultsGeometry = (IPolygon)topologicalOperator.Intersect(resultsFeature.Shape,
                    ESRI.ArcGIS.Geometry.esriGeometryDimension.esriGeometry2Dimension);
                clippedResultsGeometry.Densify(0, 0); // Densify to maintain curved appearance when converted to JSON. 
                // Convert the geometry to JSON and add it to the list.
                JsonObject jsonClippedResultsGeometry = Conversion.ToJsonObject(clippedResultsGeometry);
                jsonGeometries.Add(jsonClippedResultsGeometry);
                // Get statistics.
                IArea area = (IArea)clippedResultsGeometry;
                string resultsClass = resultsFeature.get_Value(classFieldIndex) as string;
                // If the class is already in the dictionary, add the current feature's area to the existing entry.
                if (summaryStatsDictionary.ContainsKey(resultsClass))
                    summaryStatsDictionary[resultsClass] = (double)summaryStatsDictionary[resultsClass] + area.Area;
                else
                    summaryStatsDictionary[resultsClass] = area.Area;
            }
            // Use a helper method to get a JSON array of area records.
            JsonObject[] areaResultJson = CreateJsonRecords(summaryStatsDictionary) as JsonObject[];
            // Create a JSON object of the geometry results and the area records.
            JsonObject resultJsonObject = new JsonObject();
            resultJsonObject.AddArray("geometries", jsonGeometries.ToArray());
            resultJsonObject.AddArray("records", areaResultJson);
            // Get byte array of json and return results.
            byte[] result = Encoding.UTF8.GetBytes(resultJsonObject.ToJson());
            return result;
        }
        // Helper method to read the items in a dictionary and make a JSON object from them.
        private JsonObject[] CreateJsonRecords(Dictionary<string, double> inListDictionary)
        {
            JsonObject[] jsonRecordsArray = new JsonObject[inListDictionary.Count];
            int i = 0;
            // Loop through dictionary.
            foreach (KeyValuePair<string, double> kvp in inListDictionary)
            {
                // Get the current key and value.
                string currentKey = kvp.Key.ToString();
                string currentValue = kvp.Value.ToString();
                // Add the key and value to a JSON object.
                JsonObject currentKeyValue = new JsonObject();
                currentKeyValue.AddString(m_mapLayerNameToQuery, currentKey);
                currentKeyValue.AddString("value", currentValue);
                // Add the record object to an array.
                jsonRecordsArray.SetValue(currentKeyValue, i);
                i++;
            }

            return jsonRecordsArray;
        }


    }
}

