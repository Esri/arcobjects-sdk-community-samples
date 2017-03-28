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

using ESRI.ArcGIS.DataSourcesRaster; 

//TODO: sign the project (project properties > signing tab > sign the assembly)
//      this is strongly suggested if the dll will be registered using regasm.exe <your>.dll /codebase


namespace NetEditFeaturesRESTSOE
{
    [ComVisible(true)]
    [Guid("6d79a67b-27d2-4a7f-85a3-6bf6fab061df")]
    [ClassInterface(ClassInterfaceType.None)]
    [ServerObjectExtension("MapServer",
        AllCapabilities = "",
        DefaultCapabilities = "",
        Description = ".Net Edit features SOE - allows feature validation and editing.",
        DisplayName = ".Net Edit Features REST SOE",
        Properties = "layerId=0",
        SupportsREST = true,
        SupportsSOAP = false)]
    public class NetEditFeaturesRESTSOE : IServerObjectExtension, IObjectConstruct, IRESTRequestHandler
    {
        private string soe_name;

        private IPropertySet configProps;
        private IServerObjectHelper serverObjectHelper;
        private ServerLogger logger;
        private IRESTRequestHandler reqHandler;

        private IMapServerInfo mapServerInfo = null;
        private IMapServerDataAccess mapServerDataAccess = null;
        private IMapLayerInfos layerInfos = null;
        private IMapLayerInfo editLayerInfo = null;
        private int layerId = -1;
        private IFeatureClass fc = null;

        public NetEditFeaturesRESTSOE()
        {
            soe_name = this.GetType().Name;
            logger = new ServerLogger();
            reqHandler = new SoeRestImpl(soe_name, CreateRestSchema()) as IRESTRequestHandler;
        }

        #region IServerObjectExtension Members

        public void Init(IServerObjectHelper pSOH)
        {
            serverObjectHelper = pSOH;

            mapServerDataAccess = (IMapServerDataAccess)pSOH.ServerObject;
            IMapServer3 ms = (IMapServer3)pSOH.ServerObject;
            this.mapServerInfo = ms.GetServerInfo(ms.DefaultMapName);
            this.layerInfos = mapServerInfo.MapLayerInfos;

            if (layerId < 0)
                layerId = 0;
        }

        public void Shutdown()
        {
        }

        #endregion

        #region IObjectConstruct Members

        public void Construct(IPropertySet props)
        {
            configProps = props;
            string lid = (string)props.GetProperty("layerId");
            this.layerId = Convert.ToInt32(lid);

            this.fc = (IFeatureClass) this.mapServerDataAccess.GetDataSource(this.mapServerInfo.Name, this.layerId);
            this.editLayerInfo = this.layerInfos.get_Element(this.layerId);
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

            RestResource layerResource = new RestResource("layers", false, LayersResHandler);
            rootRes.resources.Add(layerResource);

            RestOperation addNewFeatureOper = new RestOperation("addNewFeature",
                                                      new string[] { "featureJSON" },
                                                      new string[] { "json" },
                                                      addNewFeatureOperHandler);
            rootRes.operations.Add(addNewFeatureOper);

            RestOperation editFeatureOper = new RestOperation("editFeature",
                                                      new string[] { "featureId", "featureJSON" },
                                                      new string[] { "json" },
                                                      editFeatureOperHandler);
            rootRes.operations.Add(editFeatureOper);

            return rootRes;
        }

        private byte[] RootResHandler(NameValueCollection boundVariables, string outputFormat, string requestProperties, out string responseProperties)
        {
            responseProperties = null;
           
            JsonObject infoJSON = new JsonObject();
            infoJSON.AddString("name", ".Net Edit Features REST SOE");
            infoJSON.AddString("description", "This SOE adds and edits features to a selected layer in the host map service. "
                + "Note that this SOE is not designed to work with map services that have features stored in SDC data format."
                + " The \"layers\" subresource returns all layers in the map service."
                + " The \"editFeature\" operation allows editing an existing feature in the layer indicated by this SOE's layerId property.\n"
                + " The \"addFeatures\" operation allows addition of a new feature to the layer indicated by this SOE's layerId property.\n"
                + " The acceptableSchema JSON below indicates the correct schema that could be used to add/edit features. This schema belongs to the layer "
                + "selected for editing by the ArcGIS Server administrator via the SOE's layerId property. This property's value can be "
                + "modified using ArcGIS Manager.");

            // validation - ensure user has provided right layer id property value.
            if (this.layerId > this.layerInfos.Count - 1)
            {
                return createErrorObject(406, "Layer Id " + this.layerId + " is invalid.", new String[] {
				    "Acceptable layer ids are between 0 and "
					    + (layerInfos.Count - 1) + ".",
				    "Also ensure that the id points to a feature layer." });
            }

            // inform the user that edits can be done only on feature layers, if no
	        // feature layer corresponds to user-provided layerId
            if (this.editLayerInfo == null)
            {
                this.editLayerInfo = this.layerInfos.get_Element(this.layerId);
                if (!this.editLayerInfo.IsFeatureLayer)
                {
                    return createErrorObject(
                            403,
                            "The layerId property of this SOE currently points to a layer (id: "
                                + this.layerId
                                + ") that is not a feature layer.",
                            new String[] {
				            "Only feature layers can be edited by this SOE.",
				            "Modify SOE's layerId property using ArcGIS Manager or ArcGIS Desktop's Service Editor." });
                }
            }

            // Grab the fc powering the layer if its null, which means it did not get initialized in construct(), thereby 
            // suggesting that the layerId property value is incorrect.             
	        if (this.fc == null) 
            {
                // The down side of grabbing fc here is
                // that a new instance of fc is created once for every request.
                // Can't create fc in init(), since layerId property value for a
                // particular service is not necessarily available always when init() is invoked.	        
                this.fc = (IFeatureClass) this.mapServerDataAccess.GetDataSource(this.mapServerInfo.Name, this.layerId);
                if (this.fc == null)
                {
                    // if its still null, return error
                    return createErrorObject(
                        406,
                        "Incorrect layer id provided.",
                        new String[] { "Please provide layer id of a feature layer." });
                }
	        }

            infoJSON.AddString("Layer selected for editing", editLayerInfo.Name.ToString() + " (" + layerId + ")");
            JsonObject schemaJSON = getSchemaJSON();
            infoJSON.AddObject("acceptableSchema", schemaJSON);

            return Encoding.UTF8.GetBytes(infoJSON.ToJson());
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
            error.AddJSONObject("error", errorJSON);

            return Encoding.UTF8.GetBytes(error.ToJSONString(null));
        }

        private byte[] LayersResHandler(NameValueCollection boundVariables, string outputFormat, string requestProperties, out string responseProperties)
        {
            responseProperties = null;

            CustomLayerInfo[] layerInfos = GetLayerInfos();

            JsonObject[] jos = new JsonObject[layerInfos.Length];

            for (int i = 0; i < layerInfos.Length; i++)
                jos[i] = layerInfos[i].ToJsonObject();

            JsonObject result = new JsonObject();
            result.AddArray("layersInfo", jos);

            string json = result.ToJson();

            return Encoding.UTF8.GetBytes(json);
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

        private byte[] addNewFeatureOperHandler(NameValueCollection boundVariables,
                                                  JsonObject operationInput,
                                                      string outputFormat,
                                                      string requestProperties,
                                                  out string responseProperties)
        {

            responseProperties = null;

            // get the feature JSON
            JsonObject newFeatureJSON = null;
            operationInput.TryGetJsonObject("featureJSON", out newFeatureJSON);

            // add the new feature
            IFeature newFeature;
            var bytes = addFeature(newFeatureJSON, out newFeature);

            if (null == newFeature)
                return bytes; //return error
            
            // send response back to client app
            var response = new JsonObject();
            response.AddString("status", "success");
            response.AddString("message", "Feature " + newFeature.OID + " added.");

            return Encoding.UTF8.GetBytes(response.ToJson());
        }

        private byte[] editFeatureOperHandler(NameValueCollection boundVariables,
                                                  JsonObject operationInput,
                                                      string outputFormat,
                                                      string requestProperties,
                                                  out string responseProperties)
        {
            responseProperties = null;

             // get the id of the feature to be edited	        
            object featureIdObj;
            operationInput.TryGetObject("featureId", out featureIdObj);
            int updateFeatureId = Convert.ToInt32(featureIdObj.ToString());

            object featureJSONObj;
            operationInput.TryGetObject("featureJSON", out featureJSONObj);
            JsonObject updateFeatureJSON = (JsonObject)featureJSONObj;

	        // set a filter for the specific feature
	        QueryFilter queryFilter = new QueryFilter();
            if (this.fc == null)
            {
                return createErrorObject(
                        406,
                        "Incorrect layer id provided.",
                        new String[] { "Please provide layer id of a feature layer." });
            }
            
            IClass myClass = (IClass) this.fc;
	        queryFilter.WhereClause = myClass.OIDFieldName + "=" + updateFeatureId;

	        IFeatureCursor featureCursor = this.fc.Search(queryFilter, false);

	        // attempt retrieval of the feature and check if it does exist
            IFeatureCursor myFeatureCursor = (IFeatureCursor) featureCursor;
	        IFeature updateFeature = myFeatureCursor.NextFeature();
	        if (updateFeature == null) {
	        return createErrorObject(
		        406,
		        "Incorrect feature id provided.",
		        new String[] { "No feature exists for feature id "
			        + updateFeatureId + "." });
	        }

            JsonObject response = new JsonObject();

            // edit feature
            string result = System.Text.Encoding.GetEncoding("utf-8").GetString(performEdits(updateFeature, updateFeatureJSON));
            featureCursor.Flush();
            if (result.Equals(System.Boolean.TrueString))
            {
                response.AddString("status", "success");
                response.AddString("message", "Feature " + updateFeatureId + " updated");
            }
            else
            {
                response.AddString("status", "failure");
                response.AddString("message", result);
            }

	        // send response back to client app
	        return Encoding.UTF8.GetBytes(response.ToJson());
        }

        /**
         * Performs edits to the geodatabase powering the map service that this SOE
         * extends
         * 
         * @param feature
         * @param featureJSON
         * @throws Exception
         */
        private byte[] performEdits(IFeature feature, JsonObject featureJSON) 
        {
            IDataset fsDataset = (IDataset) this.fc;
	        IWorkspace ws = fsDataset.Workspace;
            IWorkspaceEdit wsEdit = (IWorkspaceEdit) ws;
	        try 
            {
	            // start an edit transaction to add a new feature to feature class
	            wsEdit.StartEditing(false);
	            wsEdit.StartEditOperation();

	            // set attributes
                if (this.editLayerInfo == null)
                {
                    this.editLayerInfo = this.layerInfos.get_Element(this.layerId);
                    if (!this.editLayerInfo.IsFeatureLayer)
                    {
                        return createErrorObject(
                                403,
                                "The layerId property of this SOE currently points to a layer (id: "
                                    + this.layerId
                                    + ") that is not a feature layer.",
                                new String[] {
				            "Only feature layers can be edited by this SOE.",
				            "Modify SOE's layerId property using ArcGIS Manager or ArcGIS Desktop's Service Editor." });
                    }
                }

	            IFields fields = this.editLayerInfo.Fields;

	            JsonObject attributesJSON = null;
                featureJSON.TryGetJsonObject("attributes", out attributesJSON);

                System.Collections.IEnumerator itKeys = attributesJSON.GetEnumerator();
                while (itKeys.MoveNext())
                {
                    KeyValuePair<string, object> kv = (KeyValuePair<string, object>) itKeys.Current;                    
                    String key = kv.Key;
                    int fieldId = fields.FindField(key);
                    IField field = fields.get_Field(fieldId);

                    object fieldValue = null; 
                    if(field.Editable)
                    {
                        //not using specific types based on field type, since can't assign value of any type to C# object
                        attributesJSON.TryGetObject(key, out fieldValue);

                        // set attribute field value
                        feature.set_Value(fieldId, fieldValue);
                    }
                }          

	            // retrieve geometry as json and convert it to ArcObject geometry
                JsonObject geometryJSON = null;
                featureJSON.TryGetJsonObject("geometry", out geometryJSON);

                IJSONConverterGeometry iConverter = new JSONConverterGeometryClass();
                IJSONObject obj = new JSONObjectClass();
                obj.ParseString(geometryJSON.ToJson());                

	            IGeometry geometry = null;
	            switch (this.fc.ShapeType) 
                {
	                case esriGeometryType.esriGeometryPoint:
                        geometry = iConverter.ToPoint(obj);
		            break;

	                case esriGeometryType.esriGeometryMultipoint:
                        geometry = iConverter.ToMultipoint(obj, false, false);
		            break;

	                case esriGeometryType.esriGeometryPolyline:
		                geometry = iConverter.ToPolyline(obj, false, false);
		            break;

	                case esriGeometryType.esriGeometryPolygon:
                    geometry = iConverter.ToPolygon(obj, false, false);
		            break;
	            }

	            // set geometry
	            feature.Shape = geometry;

	            // store feature in feature class
	            feature.Store();

	            // end edit transaction
	            wsEdit.StopEditOperation();
	            wsEdit.StopEditing(true);
	        } catch (Exception e) {
                if (wsEdit != null && wsEdit.IsBeingEdited())
                {
		            wsEdit.StopEditing(false);
	            }
                return createErrorObject(500,
		            "Error occured while editing layer " + this.layerId + ".",
		            new String[] { "Error details:", e.Message});
	        }

            return Encoding.UTF8.GetBytes(System.Boolean.TrueString);
        }

        /**
         * Performs edits to the geodatabase powering the map service that this SOE
         * extends
         * 
         * @param feature
         * @param featureJSON
         * @throws Exception
         */
        private byte[] addFeature(JsonObject featureJSON, out IFeature feature)
        {
            feature = null;
            IDataset fsDataset = (IDataset)this.fc;
            IWorkspace ws = fsDataset.Workspace;
            IWorkspaceEdit wsEdit = (IWorkspaceEdit)ws;
            try
            {
                // start an edit transaction to add a new feature to feature class
                wsEdit.StartEditing(false);
                wsEdit.StartEditOperation();
                
                feature = fc.CreateFeature();

                // set attributes
                if (this.editLayerInfo == null)
                {
                    this.editLayerInfo = this.layerInfos.get_Element(this.layerId);
                    if (!this.editLayerInfo.IsFeatureLayer)
                    {
                        return createErrorObject(
                                403,
                                "The layerId property of this SOE currently points to a layer (id: "
                                    + this.layerId
                                    + ") that is not a feature layer.",
                                new String[] {
				            "Only feature layers can be edited by this SOE.",
				            "Modify SOE's layerId property using ArcGIS Manager or ArcGIS Desktop's Service Editor." });
                    }
                }

                IFields fields = this.editLayerInfo.Fields;

                JsonObject attributesJSON = null;
                featureJSON.TryGetJsonObject("attributes", out attributesJSON);

                System.Collections.IEnumerator itKeys = attributesJSON.GetEnumerator();
                while (itKeys.MoveNext())
                {
                    KeyValuePair<string, object> kv = (KeyValuePair<string, object>)itKeys.Current;
                    String key = kv.Key;
                    int fieldId = fields.FindField(key);
                    IField field = fields.get_Field(fieldId);

                    object fieldValue = null;
                    if (field.Editable)
                    {
                        //not using specific types based on field type, since can't assign value of any type to C# object
                        attributesJSON.TryGetObject(key, out fieldValue);

                        // set attribute field value
                        feature.set_Value(fieldId, fieldValue);
                    }
                }

                // retrieve geometry as json and convert it to ArcObject geometry
                JsonObject geometryJSON = null;
                featureJSON.TryGetJsonObject("geometry", out geometryJSON);

                IJSONConverterGeometry iConverter = new JSONConverterGeometryClass();
                IJSONObject obj = new JSONObjectClass();
                obj.ParseString(geometryJSON.ToJson());

                IGeometry geometry = null;
                switch (this.fc.ShapeType)
                {
                    case esriGeometryType.esriGeometryPoint:
                        geometry = iConverter.ToPoint(obj);
                        break;

                    case esriGeometryType.esriGeometryMultipoint:
                        geometry = iConverter.ToMultipoint(obj, false, false);
                        break;

                    case esriGeometryType.esriGeometryPolyline:
                        geometry = iConverter.ToPolyline(obj, false, false);
                        break;

                    case esriGeometryType.esriGeometryPolygon:
                        geometry = iConverter.ToPolygon(obj, false, false);
                        break;
                }

                // set geometry
                feature.Shape = geometry;

                // store feature in feature class
                feature.Store();

                // end edit transaction
                wsEdit.StopEditOperation();
                wsEdit.StopEditing(true);
            }
            catch (Exception e)
            {
                if (wsEdit != null && wsEdit.IsBeingEdited())
                {
                    wsEdit.StopEditing(false);
                }
                return createErrorObject(500,
                    "Error occured while editing layer " + this.layerId + ".",
                    new String[] { "Error details:", e.Message });
            }

            return Encoding.UTF8.GetBytes(System.Boolean.TrueString);
        }    

        // Validates schema of JSON provided by user for editing
        private bool isSchemaValid(JsonObject userFeatureJson)
        {
            Fields fields = (Fields) editLayerInfo.Fields;
            string[] fieldsSet = new string[fields.FieldCount];
            for (int i = 0; i < fields.FieldCount; i++)
            {
                fieldsSet[i] = fields.get_Field(i).Name;
            }

            JsonObject attributesJson;
            userFeatureJson.TryGetJsonObject("attributes", out attributesJson);

            System.Collections.IEnumerator itKeys = attributesJson.GetEnumerator();

            while (itKeys.MoveNext())
            {
                String key = (String) itKeys.Current;
                IField field = fields.get_Field(fields.FindField(key));

                // as long as user supplied schema contains all editable fields and
        	    // is a subset of feature class schema, we are ok.
                if (field.Editable && !fieldsSet.Contains(key))
                {
                    return false;
                }
            }

            return true;
        }

        //Retrieves feature schema for selected layer that could be used to provide data for editing.
        private JsonObject getSchemaJSON()
        {
            Fields fields = (Fields) editLayerInfo.Fields;
            int fieldCount = fields.FieldCount;

            JsonObject attributeJsonObject = new JsonObject();
            for (int i = 0; i < fieldCount; i++)
            {
                Field field = (Field)fields.get_Field(i);
                String typeStr = null;

                switch (field.Type)
                {
                    case esriFieldType.esriFieldTypeBlob:
                        typeStr = "Blob";
                        break;

                    case esriFieldType.esriFieldTypeDate:
                        typeStr = "Date";
                        break;

                    case esriFieldType.esriFieldTypeDouble:
                        typeStr = "Double";
                        break;

                    case esriFieldType.esriFieldTypeInteger:
                        typeStr = "Integer";
                        break;

                    case esriFieldType.esriFieldTypeRaster:
                        typeStr = "Raster";
                        break;

                    case esriFieldType.esriFieldTypeSmallInteger:
                        typeStr = "Integer";
                        break;

                    case esriFieldType.esriFieldTypeString:
                        typeStr = "String";
                        break;

                    case esriFieldType.esriFieldTypeXML:
                        typeStr = "XML";
                        break;

                    default:
                        break;
                }

                if (typeStr != null && typeStr.Length > 0 && field.Editable)
                {
                    attributeJsonObject.AddString(field.Name, typeStr);
                }
            }

            JsonObject featuresJsonObject = new JsonObject();
            featuresJsonObject.AddJsonObject("attributes", attributeJsonObject);

            JsonObject geometryJson = new JsonObject();
            
            switch (((IFeatureClass)fc).ShapeType)
            {
                case esriGeometryType.esriGeometryPoint:
                    geometryJson.AddString("x", "x");
                    geometryJson.AddString("y", "y");
                    geometryJson.AddString("z", "z");
                    break;

                case esriGeometryType.esriGeometryMultipoint:
                    geometryJson.AddString("hasM", "true | false");
                    geometryJson.AddString("hasZ", "true | false");
                    geometryJson.AddString("points",
                        "[ [ x1, y1, z1, m1 ] , [ x2, y2, z2, m2 ], ... ]");
                    break;

                case esriGeometryType.esriGeometryPolyline:
                    geometryJson.AddString("hasM", "true | false");
                    geometryJson.AddString("hasZ", "true | false");
                    geometryJson.AddString("paths", "["
                        + "[ [x11, y11, z11, m11], [x12, y12, z12, m12] ],"
                        + "[ [x21, y21, z21, m21], [x22, y22, z22, m22] ]]");
                    break;

                case esriGeometryType.esriGeometryPolygon:
                    geometryJson.AddString("hasM", "true | false");
                    geometryJson.AddString("hasZ", "true | false");
                    geometryJson.AddString
                        ("rings",
                            "["
                                + "[ [x11, y11, z11, m11], [x12, y12, z12, m12], ..., [x11, y11, z11, m11] ],"
                                + "[ [x21, y21, z21, m21], [x22, y22, z22, m22], ..., [x21, y21, z21, m21] ]]");
                    break;

                default: break; 
            }

            JsonObject srJson = new JsonObject();
            srJson.AddString("wkid", "wkid");

            geometryJson.AddJsonObject("spatialReference", srJson);

            featuresJsonObject.AddJsonObject("geometry", geometryJson);

            return featuresJsonObject;
        }

    }
}
