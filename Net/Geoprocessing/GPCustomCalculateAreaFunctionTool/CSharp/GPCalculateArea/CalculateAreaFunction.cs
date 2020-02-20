/*

   Copyright 2019 Esri

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
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geoprocessing;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.ADF.CATIDs;

namespace GPCalculateArea
{

    public class CalculateAreaFunction : IGPFunction2
    {        
        //Local members
        private string m_ToolName = "CalculateArea"; //Function Name
        private string m_metadatafile = "CalculateArea_area.xml";
        private IArray m_Parameters;             // Array of Parameters
        private IGPUtilities m_GPUtilities;      // GPUtilities object
        
        public CalculateAreaFunction()
        {
            m_GPUtilities = new GPUtilitiesClass();
        }

        #region IGPFunction Members

        // Set the name of the function tool. 
        // This name appears when executing the tool at the command line or in scripting. 
        // This name should be unique to each toolbox and must not contain spaces.
        public string Name
        {
            get { return m_ToolName; }
        }

        // Set the function tool Display Name as seen in ArcToolbox.
        public string DisplayName
        {
            get { return "Calculate Area"; }
        }

        // This is the location where the parameters to the Function Tool are defined. 
        // This property returns an IArray of parameter objects (IGPParameter). 
        // These objects define the characteristics of the input and output parameters. 
        public IArray ParameterInfo
        {                 
            get 
            {
                //Array to the hold the parameters	
                IArray parameters = new ArrayClass();

                //Input DataType is GPFeatureLayerType
                IGPParameterEdit3 inputParameter = new GPParameterClass();
                inputParameter.DataType = new GPFeatureLayerTypeClass();

                // Default Value object is GPFeatureLayer
                inputParameter.Value = new GPFeatureLayerClass();

                // Set Input Parameter properties
                inputParameter.Direction = esriGPParameterDirection.esriGPParameterDirectionInput;
                inputParameter.DisplayName = "Input Features";
                inputParameter.Name = "input_features";
                inputParameter.ParameterType = esriGPParameterType.esriGPParameterTypeRequired;
                parameters.Add(inputParameter);

                // Area field parameter
                inputParameter = new GPParameterClass();
                inputParameter.DataType = new GPStringTypeClass();

                // Value object is GPString
                IGPString gpStringValue = new GPStringClass();
                gpStringValue.Value = "Area";
                inputParameter.Value = (IGPValue)gpStringValue;

                // Set field name parameter properties
                inputParameter.Direction = esriGPParameterDirection.esriGPParameterDirectionInput;
                inputParameter.DisplayName = "Area Field Name";
                inputParameter.Name = "field_name";
                inputParameter.ParameterType = esriGPParameterType.esriGPParameterTypeRequired; 
                parameters.Add(inputParameter);

                // Output parameter (Derived) and data type is DEFeatureClass
                IGPParameterEdit3 outputParameter = new GPParameterClass();
                outputParameter.DataType = new GPFeatureLayerTypeClass();

                // Value object is DEFeatureClass
                outputParameter.Value = new DEFeatureClassClass();
                               
                // Set output parameter properties
                outputParameter.Direction = esriGPParameterDirection.esriGPParameterDirectionOutput;
                outputParameter.DisplayName = "Output FeatureClass";
                outputParameter.Name = "out_featureclass";
                outputParameter.ParameterType = esriGPParameterType.esriGPParameterTypeDerived;

                // Create a new schema object - schema means the structure or design of the feature class (field information, geometry information, extent)
                IGPFeatureSchema outputSchema = new GPFeatureSchemaClass();
                IGPSchema schema = (IGPSchema)outputSchema;

                // Clone the schema from the dependency. 
                //This means update the output with the same schema as the input feature class (the dependency).                                
                schema.CloneDependency = true;               

                // Set the schema on the output because this tool will add an additional field.
                outputParameter.Schema = outputSchema as IGPSchema;
                outputParameter.AddDependency("input_features");
                parameters.Add(outputParameter);

                return parameters;
            }
        }

        // Validate: 
        // - Validate is an IGPFunction method, and we need to implement it in case there
        //   is legacy code that queries for the IGPFunction interface instead of the IGPFunction2 
        //   interface.  
        // - This Validate code is boilerplate - copy and insert into any IGPFunction2 code..
        // - This is the calling sequence that the gp framework now uses when it QI's for IGPFunction2..
        public IGPMessages Validate(IArray paramvalues, bool updateValues, IGPEnvironmentManager envMgr)
        {
            if (m_Parameters == null)
                m_Parameters = ParameterInfo;

            // Call UpdateParameters(). 
            // Only Call if updatevalues is true.
            if (updateValues == true)
            {
                UpdateParameters(paramvalues, envMgr);
            }

            // Call InternalValidate (Basic Validation). Are all the required parameters supplied?
            // Are the Values to the parameters the correct data type?
            IGPMessages validateMsgs = m_GPUtilities.InternalValidate(m_Parameters, paramvalues, updateValues, true, envMgr);

            // Call UpdateMessages();
            UpdateMessages(paramvalues, envMgr, validateMsgs);

            // Return the messages
            return validateMsgs;
        }

        // This method will update the output parameter value with the additional area field.
        public void UpdateParameters(IArray paramvalues, IGPEnvironmentManager pEnvMgr)
        {
            m_Parameters = paramvalues;

            // Retrieve the input parameter value
            IGPValue parameterValue = m_GPUtilities.UnpackGPValue(m_Parameters.get_Element(0));

            // Get the derived output feature class schema and empty the additional fields. This will ensure you don't get duplicate entries.
            IGPParameter3 derivedFeatures = (IGPParameter3)paramvalues.get_Element(2);
            IGPFeatureSchema schema = (IGPFeatureSchema)derivedFeatures.Schema;
            schema.AdditionalFields = null;

            // If we have an input value, create a new field based on the field name the user entered.            
            if  (parameterValue.IsEmpty() == false)
            {
                IGPParameter3 fieldNameParameter = (IGPParameter3)paramvalues.get_Element(1);
                string fieldName = fieldNameParameter.Value.GetAsText();
                
                // Check if the user's input field already exists
                IField areaField = m_GPUtilities.FindField(parameterValue, fieldName);
                if (areaField == null)
                {
                    IFieldsEdit fieldsEdit = new FieldsClass();
                    IFieldEdit fieldEdit = new FieldClass();
                    fieldEdit.Name_2 = fieldName;
                    fieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
                    fieldsEdit.AddField(fieldEdit);

                    // Add an additional field for the area values to the derived output.
                    IFields fields = fieldsEdit as IFields;                    
                    schema.AdditionalFields = fields;
                }
                
            }
        }

        // Called after returning from the update parameters routine. 
        // You can examine the messages created from internal validation and change them if desired. 
        public void UpdateMessages(IArray paramvalues, IGPEnvironmentManager pEnvMgr, IGPMessages Messages)
        {
            // Check for error messages
            IGPMessage msg = (IGPMessage)Messages;
            if (msg.IsError())
                return;    

            // Get the first Input Parameter
            IGPParameter parameter = (IGPParameter)paramvalues.get_Element(0);

            // UnPackGPValue. This ensures you get the value either form the dataelement or GpVariable (ModelBuilder)
            IGPValue parameterValue = m_GPUtilities.UnpackGPValue(parameter);

            // Open the Input Dataset - Use DecodeFeatureLayer as the input might be a layer file or a feature layer from ArcMap.
            IFeatureClass inputFeatureClass;
            IQueryFilter qf;
            m_GPUtilities.DecodeFeatureLayer(parameterValue, out inputFeatureClass, out qf);

            IGPParameter3 fieldParameter = (IGPParameter3)paramvalues.get_Element(1);
            string fieldName = fieldParameter.Value.GetAsText();

            // Check if the field already exists and provide a warning.
            int indexA = inputFeatureClass.FindField(fieldName);
            if (indexA > 0)
            {
                Messages.ReplaceWarning(1, "Field already exists. It will be overwritten.");
            }

            return;
        }

        // Execute: Execute the function given the array of the parameters
        public void Execute(IArray paramvalues, ITrackCancel trackcancel, IGPEnvironmentManager envMgr, IGPMessages message)
        {
            
            // Get the first Input Parameter
            IGPParameter parameter = (IGPParameter)paramvalues.get_Element(0);

            // UnPackGPValue. This ensures you get the value either form the dataelement or GpVariable (ModelBuilder)
           IGPValue parameterValue = m_GPUtilities.UnpackGPValue(parameter);

            // Open Input Dataset
            IFeatureClass inputFeatureClass;
            IQueryFilter qf;
            m_GPUtilities.DecodeFeatureLayer(parameterValue, out inputFeatureClass, out qf);

            if (inputFeatureClass == null)
            {
                message.AddError(2, "Could not open input dataset.");
                return;
            } 
                
            // Add the field if it does not exist.
            int indexA;

            parameter = (IGPParameter)paramvalues.get_Element(1);
            string field = parameter.Value.GetAsText();

            
            indexA = inputFeatureClass.FindField(field);
            if (indexA < 0)
            {
                IFieldEdit fieldEdit = new FieldClass();
                fieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
                fieldEdit.Name_2 = field;
                inputFeatureClass.AddField(fieldEdit);
            }

             int featcount = inputFeatureClass.FeatureCount(null);     
      
             //Set the properties of the Step Progressor
            IStepProgressor pStepPro = (IStepProgressor)trackcancel;
            pStepPro.MinRange = 0;
            pStepPro.MaxRange = featcount;
            pStepPro.StepValue = (1);
            pStepPro.Message = "Calculating Area";
            pStepPro.Position = 0;
            pStepPro.Show();

            // Create an Update Cursor
            indexA = inputFeatureClass.FindField(field);
            IFeatureCursor updateCursor = inputFeatureClass.Update(qf, false);
            IFeature updateFeature = updateCursor.NextFeature();
            IGeometry geometry;
            IArea area;
            double dArea;   
            
            while (updateFeature != null)
            {
                geometry = updateFeature.Shape;
                area = (IArea)geometry;
                dArea = area.Area;
                updateFeature.set_Value(indexA, dArea);
                updateCursor.UpdateFeature(updateFeature);
                updateFeature.Store();
                updateFeature = updateCursor.NextFeature();
                pStepPro.Step();
            }

           pStepPro.Hide();
            
            // Release the update cursor to remove the lock on the input data.
           System.Runtime.InteropServices.Marshal.ReleaseComObject(updateCursor);
        }

        // This is the function name object for the Geoprocessing Function Tool. 
        // This name object is created and returned by the Function Factory.
        // The Function Factory must first be created before implementing this property.
        public IName FullName
        {
            get
            { 	
                // Add CalculateArea.FullName getter implementation
                IGPFunctionFactory functionFactory = new CalculateAreaFunctionFactory();
                return (IName)functionFactory.GetFunctionName(m_ToolName);
            }
        }

        // This is used to set a custom renderer for the output of the Function Tool.
        public object GetRenderer(IGPParameter pParam)
        {
            return null;
        }

        // This is the unique context identifier in a [MAP] file (.h). 
        // ESRI Knowledge Base article #27680 provides more information about creating a [MAP] file. 
        public int HelpContext
        {
            get { return 0; }
        }

        // This is the path to a .chm file which is used to describe and explain the function and its operation. 
        public string HelpFile
        {
            get { return ""; }
        }

        // This is used to return whether the function tool is licensed to execute.
        public bool IsLicensed()
        {
            IAoInitialize aoi = new AoInitializeClass();
            ILicenseInformation licInfo = (ILicenseInformation)aoi;

            string licName = licInfo.GetLicenseProductName(aoi.InitializedProduct());

            if (licName == "Advanced")
            {
                 return true;
            }
            else
                return false;
        }

        // This is the name of the (.xml) file containing the default metadata for this function tool. 
        // The metadata file is used to supply the parameter descriptions in the help panel in the dialog. 
        // If no (.chm) file is provided, the help is based on the metadata file. 
        // ESRI Knowledge Base article #27000 provides more information about creating a metadata file.
        public string MetadataFile
        {
            // if you just return the name of an *.xml file as follows:
            // get { return m_metadatafile; }
            // then the metadata file will be created 
            // in the default location - <install directory>\help\gp
            
            // alternatively, you can send the *.xml file to the location of the DLL.
            // 
            get
            {
                string filePath;
                filePath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                filePath = System.IO.Path.Combine(filePath, m_metadatafile);
                return filePath;
            }
        }

        // By default, the Toolbox will create a dialog based upon the parameters returned 
        // by the ParameterInfo property.
        public UID DialogCLSID
        {
            // DO NOT USE. INTERNAL USE ONLY.
            get { return null; }
        }

        #endregion
    }

    //////////////////////////////
    // Function Factory Class
    ////////////////////////////
    [
    Guid("2554BFC7-94F9-4d28-B3FE-14D17599B35A"),
    ComVisible(true)
    ]
    public class CalculateAreaFunctionFactory : IGPFunctionFactory
    {
        // Register the Function Factory with the ESRI Geoprocessor Function Factory Component Category.
        #region "Component Category Registration"
        [ComRegisterFunction()]
        static void Reg(string regKey)
        {
            
            GPFunctionFactories.Register(regKey);
        }

        [ComUnregisterFunction()]
        static void Unreg(string regKey)
        {
            GPFunctionFactories.Unregister(regKey);
        }
        #endregion

        // Utility Function added to create the function names.
        private IGPFunctionName CreateGPFunctionNames(long index)
        {
            IGPFunctionName functionName = new GPFunctionNameClass();
            functionName.MinimumProduct = esriProductCode.esriProductCodeAdvanced;
            IGPName name;

            switch (index)
            {
                case (0):
                    name = (IGPName)functionName;
                    name.Category = "AreaCalculation";
                    name.Description = "Calculate Area for FeatureClass";
                    name.DisplayName = "Calculate Area";
                    name.Name = "CalculateArea";                
                    name.Factory = (IGPFunctionFactory)this;
                    break;
            }

            return functionName;
        }

        // Implementation of the Function Factory
        #region IGPFunctionFactory Members

        // This is the name of the function factory. 
        // This is used when generating the Toolbox containing the function tools of the factory.
        public string Name
        {
            get { return "AreaCalculation"; }
        }

        // This is the alias name of the factory.
        public string Alias
        {
            get { return "area"; }
        }

        // This is the class id of the factory. 
        public UID CLSID
        {
            get
            {
                UID id = new UIDClass();
                id.Value = this.GetType().GUID.ToString("B");
                return id;
            }
        }

        // This method will create and return a function object based upon the input name.
        public IGPFunction GetFunction(string Name)
        {
            switch (Name)
            {
                case ("CalculateArea"):
                    IGPFunction gpFunction = new CalculateAreaFunction();
                    return gpFunction;
            }

            return null;
        }

        // This method will create and return a function name object based upon the input name.
        public IGPName GetFunctionName(string Name)
        {
            IGPName gpName = new GPFunctionNameClass();

            switch (Name)
            {
                case ("CalculateArea"):
                    return (IGPName)CreateGPFunctionNames(0);
                   
            }
            return null;
        }

        // This method will create and return an enumeration of function names that the factory supports.
        public IEnumGPName GetFunctionNames()
        {
            IArray nameArray = new EnumGPNameClass();
            nameArray.Add(CreateGPFunctionNames(0));
            return (IEnumGPName)nameArray;
        }

        // This method will create and return an enumeration of GPEnvironment objects. 
        // If tools published by this function factory required new environment settings, 
        //then you would define the additional environment settings here. 
        // This would be similar to how parameters are defined. 
        public IEnumGPEnvironment GetFunctionEnvironments()
        {
            return null;
        }

        #endregion
    }

}
