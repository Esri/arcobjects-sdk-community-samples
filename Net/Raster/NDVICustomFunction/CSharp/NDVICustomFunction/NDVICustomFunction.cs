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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.ADF.CATIDs;

namespace CustomFunction
{
    [Guid("652642F3-9106-4EB3-9262-A4C39E03BC56")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("CustomFunction.NDVICustomFunction")]
    [ComVisible(true)]
    public class NDVICustomFunction : IRasterFunction,
                                    IPersistVariant,
                                    IDocumentVersionSupportGEN,
                                    IXMLSerialize,
                                    IXMLVersionSupport
    {
        #region Private Members
        UID myUID; // UID for the Function.
        IRasterInfo myRasterInfo; // Raster Info for the Function
        rstPixelType myPixeltype; // Pixel Type of the Function.
        string myName; // Name of the Function.
        string myDescription; // Description of the Function.
        bool myValidFlag; // Flag to specify validity of the Function.
        IRasterFunctionHelper myFunctionHelper; // Raster Function Helper object.

        rstPixelType myInpPixeltype;
        int myInpNumBands;

        string[] myBandIndices;
        #endregion

        public NDVICustomFunction()
        {
            myName = "NDVI Custom Function";
            myPixeltype = rstPixelType.PT_FLOAT;
            myDescription = "Custom NDVI Function which calculates the NDVI without any scaling.";
            myValidFlag = true;
            myFunctionHelper = new RasterFunctionHelper();

            myInpPixeltype = myPixeltype;
            myInpNumBands = 0;

            myBandIndices = null;

            myUID = new UID();
            myUID.Value = "{652642F3-9106-4EB3-9262-A4C39E03BC56}";
        }

        #region IRasterFunction Members

        /// <summary>
        /// Name of the Raster Function.
        /// </summary>
        public string Name
        {
            get
            {
                return myName;
            }
            set
            {
                myName = value;
            }
        }

        /// <summary>
        /// Pixel Type of the Raster Function
        /// </summary>
        public rstPixelType PixelType
        {
            get
            {
                return myPixeltype;
            }
            set
            {
                myPixeltype = value;
            }
        }

        /// <summary>
        /// Output Raster Info for the Raster Function
        /// </summary>
        public IRasterInfo RasterInfo
        {
            get
            {
                return myRasterInfo;
            }
        }

        /// <summary>
        /// Description of the Raster Function
        /// </summary>
        public string Description
        {
            get
            {
                return myDescription;
            }
            set
            {
                myDescription = value;
            }
        }

        /// <summary>
        /// Initialize the Raster function using the argument object. This is one of the two
        /// main functions to implement for a custom Raster function. The raster object is 
        /// dereferenced if required and given to the RasterFuntionHelper object to bind.
        /// </summary>
        /// <param name="pArguments">Arguments object used for initialization</param>
        public void Bind(object pArguments)
        {
            try
            {
                // Check if the Arguments object is of the correct type.
                INDVICustomFunctionArguments customFunctionArgs = null;
                if (pArguments is INDVICustomFunctionArguments)
                {
                    customFunctionArgs = (INDVICustomFunctionArguments)pArguments;
                    object inputRaster = customFunctionArgs.Raster;
                    if (customFunctionArgs.Raster is IRasterFunctionVariable)
                    {
                        IRasterFunctionVariable rasterFunctionVariable =
                            (IRasterFunctionVariable)customFunctionArgs.Raster;
                        inputRaster = rasterFunctionVariable.Value;
                    }

                    // Call the Bind method of the Raster Function Helper object.
                    myFunctionHelper.Bind(inputRaster);
                }
                else 
                {
                    // Throw an error if incorrect arguments object is passed.
                    throw new System.Exception(
                        "Incorrect arguments object. Expected: INDVICustomFunctionArguments");
                }

                // Check to see if Band Indices exist.
                if (customFunctionArgs.BandIndices != null && customFunctionArgs.BandIndices != "")
                    myBandIndices = customFunctionArgs.BandIndices.Split(' ');
                else
                {
                    // If not, throw an error.
                    throw new System.Exception(
                        "Incorrect parameters specified. Expected: Valid band indices.");
                }

                // Create a new RasterInfo object and initialize from the FunctionHelper object.
                // A new RasterInfo Object is created because assigning myFunctionHelper.RasterInfo
                // directly creates a reference.
                myRasterInfo = new RasterInfo();
                myRasterInfo.BandCount = myFunctionHelper.RasterInfo.BandCount;
                myRasterInfo.BlockHeight = myFunctionHelper.RasterInfo.BlockHeight;
                myRasterInfo.BlockWidth = myFunctionHelper.RasterInfo.BlockWidth;
                myRasterInfo.CellSize = myFunctionHelper.RasterInfo.CellSize;
                myRasterInfo.Extent = myFunctionHelper.RasterInfo.Extent;
                myRasterInfo.FirstPyramidLevel = myFunctionHelper.RasterInfo.FirstPyramidLevel;
                myRasterInfo.Format = myFunctionHelper.RasterInfo.Format;
                myRasterInfo.GeodataXform = myFunctionHelper.RasterInfo.GeodataXform;
                myRasterInfo.MaximumPyramidLevel = myFunctionHelper.RasterInfo.MaximumPyramidLevel;
                myRasterInfo.NativeExtent = myFunctionHelper.RasterInfo.NativeExtent;
                myRasterInfo.NativeSpatialReference = myFunctionHelper.RasterInfo.NativeSpatialReference;
                myRasterInfo.NoData = myFunctionHelper.RasterInfo.NoData;
                myRasterInfo.Origin = myFunctionHelper.RasterInfo.Origin;
                myRasterInfo.PixelType = rstPixelType.PT_FLOAT; // Output pixel type should be output of the NDVI.
                myRasterInfo.Resampling = myFunctionHelper.RasterInfo.Resampling;
                myRasterInfo.SupportBandSelection = myFunctionHelper.RasterInfo.SupportBandSelection;

                // Store required input properties.
                myInpPixeltype = myRasterInfo.PixelType;
                myInpNumBands = myRasterInfo.BandCount;

                // Set output pixel properties.
                myRasterInfo.BandCount = 1;
                myPixeltype = rstPixelType.PT_FLOAT;

                // Perform validation to see if the indices passed are valid.
                if (myInpNumBands < 2 || myBandIndices.Length < 2)
                {
                    // If not, throw an error.
                    throw new System.Exception(
                        "Incorrect parameters specified. Expected: Valid band indices.");
                }
                for (int i = 0; i < myBandIndices.Length; ++i)
                {
                    int currBand = Convert.ToInt16(myBandIndices[i]) - 1;
                    if ((currBand < 0) || (currBand > myInpNumBands))
                    {
                        // If not, throw an error.
                        throw new System.Exception(
                            "Incorrect parameters specified. Expected: Valid band indices.");
                    }
                }
            }
            catch (Exception exc)
            {
                System.Exception myExc = new System.Exception(
                    "Exception caught in Bind method: " + exc.Message, exc);
                throw myExc;
            }
        }

        /// <summary>
        /// Read pixels from the input Raster and fill the PixelBlock provided with processed pixels.
        /// </summary>
        /// <param name="pTlc">Point to start the reading from in the Raster</param>
        /// <param name="pRaster">Reference Raster for the PixelBlock</param>
        /// <param name="pPixelBlock">PixelBlock to be filled in</param>
        public void Read(IPnt pTlc, IRaster pRaster, IPixelBlock pPixelBlock)
        {
            try
            {
                // Create a new pixel block to read the input data into.
                // This is done because the pPixelBlock represents the output
                // pixel block which is different from the input. 
                int pBHeight = pPixelBlock.Height;
                int pBWidth = pPixelBlock.Width;
                IPnt pBlockSize = new Pnt();
                pBlockSize.X = pBWidth;
                pBlockSize.Y = pBHeight;
                IPixelBlock inputPixelBlock = new PixelBlock();
                ((IPixelBlock4)inputPixelBlock).Create(myInpNumBands, pBWidth, pBHeight, myInpPixeltype);

                // Call Read method of the Raster Function Helper object to read the input pixels into
                // the inputPixelBlock.
                myFunctionHelper.Read(pTlc, null, pRaster, inputPixelBlock);

                System.Array inpPixelValues1;
                System.Array inpPixelValues2;
                System.Array outPixelValues;
                int index1 = Convert.ToInt16(myBandIndices[0]) - 1; ; // Get NIR band index specified by user.
                int index2 = Convert.ToInt16(myBandIndices[1]) - 1; ; // Get Red Band index specified by user.

                // Get the correct bands from the input.
                IPixelBlock3 ipPixelBlock = (IPixelBlock3)inputPixelBlock;
                inpPixelValues1 = (System.Array)(ipPixelBlock.get_PixelData(index1));
                inpPixelValues2 = (System.Array)(ipPixelBlock.get_PixelData(index2));
                outPixelValues = (System.Array)(((IPixelBlock3)pPixelBlock).get_PixelData(0));
                int i = 0;
                int k = 0;
                double pixelValue = 0.0;
                double nirValue = 0.0;
                double irValue = 0.0;
                // Perform the NDVI computation and store the result in the output pixel block.
                for (i = 0; i < pBHeight; i++)
                {
                    for (k = 0; k < pBWidth; k++)
                    {
                        nirValue = Convert.ToDouble(inpPixelValues1.GetValue(k, i));
                        irValue = Convert.ToDouble(inpPixelValues2.GetValue(k, i));
                        // Check if input is not NoData.
                        if ((Convert.ToByte(ipPixelBlock.GetNoDataMaskVal(index1, k, i)) == 1) &&
                            Convert.ToByte(ipPixelBlock.GetNoDataMaskVal(index2, k, i)) == 1)
                        {
                            // NDVI[k] = (NIR[k]-Red[k])/(NIR[k]+Red[k]);
                            if ((nirValue + irValue) != 0) // Check for division by 0.
                            {
                                pixelValue = (nirValue - irValue) / (nirValue + irValue);
                                if (pixelValue < -1.0 || pixelValue > 1.0)
                                    pixelValue = 0.0;
                            }
                            else
                                pixelValue = 0.0;
                        }
                        outPixelValues.SetValue((float)(pixelValue), k, i);
                    }
                }
                // Set the output pixel values on the output pixel block.
                ((IPixelBlock3)pPixelBlock).set_PixelData(0, outPixelValues);
                // Copy over the NoData mask from the input and set it on the output.
                ((IPixelBlock3)pPixelBlock).set_NoDataMask(0, ((IPixelBlock3)inputPixelBlock).get_NoDataMask(0));
            }
            catch (Exception exc)
            {
                System.Exception myExc = new System.Exception(
                    "Exception caught in Read method: " + exc.Message, exc);
                throw myExc;
            }
        }

        /// <summary>
        /// Update the Raster Function
        /// </summary>
        public void Update()
        {
            try
            {
            }
            catch (Exception exc)
            {
                System.Exception myExc = new System.Exception(
                    "Exception caught in Update method: ", exc);
                throw myExc;
            }
        }

        /// <summary>
        /// Property that specifies whether the Raster Function is still valid.
        /// </summary>
        public bool Valid
        {
            get { return myValidFlag; }
        }

        #endregion

        #region IPersistVariant Members

        /// <summary>
        /// UID to identify the function.
        /// </summary>
        public UID ID
        {
            get
            {
                return myUID;
            }
        }

        /// <summary>
        /// Load the properties of the function from the stream provided
        /// </summary>
        /// <param name="Stream">Stream that contains the serialized form of the function</param>
        public void Load(IVariantStream Stream)
        {
            if (Stream is IDocumentVersion)
            {
                IDocumentVersion docVersion = (IDocumentVersion)Stream;
                if (docVersion.DocumentVersion >= esriArcGISVersion.esriArcGISVersion10)
                {
                    esriArcGISVersion streamVersion = (esriArcGISVersion)((int)Stream.Read());
                    if (streamVersion >= esriArcGISVersion.esriArcGISVersion10)
                    {
                        myName = (string)Stream.Read();
                        myDescription = (string)Stream.Read();
                        myPixeltype = (rstPixelType)((int)Stream.Read());
                    }
                }
            }
        }

        /// <summary>
        /// Save the properties of the function to the stream provided
        /// </summary>
        /// <param name="Stream">Stream to which to serialize the function into</param>
        public void Save(IVariantStream Stream)
        {
            if (Stream is IDocumentVersion)
            {
                IDocumentVersion docVersion = (IDocumentVersion)Stream;
                if (docVersion.DocumentVersion >= esriArcGISVersion.esriArcGISVersion10)
                {
                    Stream.Write((int)esriArcGISVersion.esriArcGISVersion10);
                    Stream.Write(myName);
                    Stream.Write(myDescription);
                    Stream.Write((int)myPixeltype);
                }
            }
        }

        #endregion

        #region IDocumentVersionSupportGEN Members

        /// <summary>
        /// Convert the instance into an object supported by the given version
        /// </summary>
        /// <param name="docVersion">Version to convert to</param>
        /// <returns>Object that supports given version</returns>
        public object ConvertToSupportedObject(esriArcGISVersion docVersion)
        {
            return null;
        }

        /// <summary>
        /// Check if the object is supported at the given version
        /// </summary>
        /// <param name="docVersion">Version to check against</param>
        /// <returns>True if the object is supported</returns>
        public bool IsSupportedAtVersion(esriArcGISVersion docVersion)
        {
            if (docVersion >= esriArcGISVersion.esriArcGISVersion10)
                return true;
            else
                return false;
        }

        #endregion

        #region IXMLSerialize Members

        /// <summary>
        /// Deserialize the Raster Function from the datastream provided
        /// </summary>
        /// <param name="data">Xml stream to deserialize the function from</param>
        public void Deserialize(IXMLSerializeData data)
        {
            myName = data.GetString(data.Find("Name"));
            myDescription = data.GetString(data.Find("Description"));
            myPixeltype = (rstPixelType)(data.GetInteger(data.Find("PixelType")));
        }

        /// <summary>
        /// Serialize the Raster Function into the stream provided.
        /// </summary>
        /// <param name="data">Xml stream to serialize the function into</param>
        public void Serialize(IXMLSerializeData data)
        {
            data.TypeName = "NDVICustomFunction";
            data.TypeNamespaceURI = @"http://www.esri.com/schemas/ArcGIS/10.2";
            data.AddString("Name", myName);
            data.AddString("Description", myDescription);
            data.AddInteger("PixelType", (int)myPixeltype);
        }

        #endregion

        #region IXMLVersionSupport Members

        /// <summary>
        /// Returns the namespaces supported by the object
        /// </summary>
        public string MinNamespaceSupported
        {
            get { return @"http://www.esri.com/schemas/ArcGIS/10.2"; }
        }

        #endregion

        #region COM Registration Function(s)
        [ComRegisterFunction()]
        static void Reg(string regKey)
        {
            ESRI.ArcGIS.ADF.CATIDs.RasterFunctions.Register(regKey);
        }

        [ComUnregisterFunction()]
        static void Unreg(string regKey)
        {
            ESRI.ArcGIS.ADF.CATIDs.RasterFunctions.Unregister(regKey);
        }
        #endregion
    }

    [Guid("157ACDCC-9F2B-4CC4-BFFD-FEB933F3E788")]
    [InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
    public interface INDVICustomFunctionArguments : IRasterFunctionArguments,
                                                          IPersistVariant,
                                                          IDocumentVersionSupportGEN,
                                                          IXMLSerialize,
                                                          IXMLVersionSupport
    {
        #region INDVICustomFunctionArguments Members

        [DispId(0x50505001)]
        object Raster
        {
            get;
            set;
        }

        [DispId(0x50505002)]
        string BandIndices
        {
            get;
            set;
        }
        #endregion
    }

    [Guid("CB684500-0A15-46C5-B1C1-8062A1629F66")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("CustomFunction.NDVICustomFunctionArguments")]
    [ComVisible(true)]
    public class NDVICustomFunctionArguments : INDVICustomFunctionArguments
    {
        #region Private Members
        string myName;
        string myDescription;

        UID myUID; // UID for the NDVI Custom Function Arguments.
        IPropertySet myProperties; // Property set to store the properties of the arguments object.
        #endregion

        public NDVICustomFunctionArguments()
        {
            myName = "NDVI Custom Function Arguments";
            myDescription = "Arguments object for the NDVI Custom Function";

            // Set default values
            myProperties = new PropertySet();
            myProperties.SetProperty("Raster", null);
            myProperties.SetProperty("BandIndices", "1 2"); // Default value for band indexes: first two bands of image.

            myUID = new UID();
            myUID.Value = "{CB684500-0A15-46C5-B1C1-8062A1629F66}";
        }

        #region NDVICustomFunctionArguments Members

        /// <summary>
        /// Raster to apply the raster function to.
        /// </summary>
        public object Raster
        {
            get
            {
                return GetDereferencedValue("Raster");
            }
            set
            {
                myProperties.SetProperty("Raster", value);
            }
        }

        /// <summary>
        /// Indices of the bands to use in the NDVI computation.
        /// </summary>
        public string BandIndices
        {
            get
            {
                return Convert.ToString(GetDereferencedValue("BandIndices"));
            }
            set
            {
                myProperties.SetProperty("BandIndices", value);
            }
        }

        /// <summary>
        /// Dereference and return the value of the property whose name is given if necessary.
        /// </summary>
        /// <param name="name">Name of the property to check.</param>
        /// <returns>Dereferenced value of the property corresponding to the name given.</returns>
        private object GetDereferencedValue(string name)
        {
            object value = myProperties.GetProperty(name);
            if (value != null && value is IRasterFunctionVariable && !(value is IRasterFunctionTemplate))
            {
                IRasterFunctionVariable rFVar = (IRasterFunctionVariable)value;
                return rFVar.Value;
            }
            return value;
        }

        #endregion

        #region IRasterFunctionArguments Members

        /// <summary>
        /// A list of files associated with the raster
        /// </summary>
        public IStringArray FileList
        {
            get
            {
                object rasterObject = myProperties.GetProperty("Raster");
                IRasterDataset rasterDataset = null;
                if (rasterObject is IRasterDataset)
                    rasterDataset = (IRasterDataset)rasterObject;
                else if (rasterObject is IName)
                    rasterDataset = (IRasterDataset)((IName)rasterObject).Open();
                if (rasterDataset != null && rasterDataset is IRasterDatasetInfo)
                {
                    IRasterDatasetInfo rasterDatasetInfo = (IRasterDatasetInfo)rasterDataset;
                    return rasterDatasetInfo.FileList;
                }
                else
                    return null;
            }
        }

        /// <summary>
        /// Get the value associated with the name provided.
        /// </summary>
        /// <param name="Name">Name of the property</param>
        /// <returns>Value of the property name provided</returns>
        public object GetValue(string Name)
        {
            return myProperties.GetProperty(Name);
        }

        /// <summary>
        /// A list of all the names in the property set.
        /// </summary>
        public IStringArray Names
        {
            get // Generate a list of names in the propertyset.
            {
                object names = null, values = null;
                myProperties.GetAllProperties(out names, out values);
                IStringArray myNames = new StrArray();
                string[] nameArray = (string[])names;
                for (int i = 0; i < nameArray.GetLength(0); ++i)
                    myNames.Add(nameArray[i]);
                return myNames;
            }
        }

        /// <summary>
        /// Set the given property name to the given value
        /// </summary>
        /// <param name="Name">Name of the property</param>
        /// <param name="Value">Value of the property</param>
        public void PutValue(string Name, object Value)
        {
            myProperties.SetProperty(Name, Value);
        }

        /// <summary>
        /// Remove the value of the property name provided
        /// </summary>
        /// <param name="Name">Name of the property to be removed</param>
        public void Remove(string Name)
        {
            myProperties.RemoveProperty(Name);
        }

        /// <summary>
        /// Clear the property set of all names and values.
        /// </summary>
        public void RemoveAll()
        {
            myProperties = null;
            myProperties = new PropertySet();
        }

        /// <summary>
        /// A list of all the values in the property set
        /// </summary>
        public IVariantArray Values
        {
            get // Generate a list of values in the propertyset.
            {
                object names = null, values = null;
                myProperties.GetAllProperties(out names, out values);
                IVariantArray myValues = new VarArray();
                object[] valArray = (object[])values;
                for (int i = 0; i < valArray.GetLength(0); ++i)
                    myValues.Add(valArray[i]);
                return myValues;
            }
        }

        /// <summary>
        /// Resolve variables containing field names with the corresponding values.
        /// </summary>
        /// <param name="pRow">The row corresponding to the function raster dataset.</param>
        /// <param name="pPropertySet">Property Set to add the list of the names and the resolved values to.</param>
        public void Resolve(IRow pRow, IPropertySet pPropertySet)
        {
            ResolveRasterVal(pRow);
        }

        /// <summary>
        /// Update the variables containing field names to their updated values.
        /// </summary>
        /// <param name="pRow">The row corresponding to the function raster dataset.</param>
        /// <param name="pPropertySet">Property Set to add the list of the names and the updated values to.</param>
        /// <param name="pTemplateArguments">The arguements object containing the properties to update if</param>
        public void Update(IRow pRow, IPropertySet pPropertySet, IRasterFunctionArguments pTemplateArguments)
        {
            Resolve(pRow, pPropertySet);
        }

        /// <summary>
        /// Resolve the 'Raster' variable if it contains field names with the corresponding values.
        /// </summary>
        /// <param name="pRow">The row corresponding to the function raster dataset.</param>
        private void ResolveRasterVal(IRow pRow)
        {
            try
            {
                // Get the Raster property.
                object myRasterObject = myProperties.GetProperty("Raster");
                // Check to see if it is a variable
                if (myRasterObject is IRasterFunctionVariable)
                {
                    IRasterFunctionVariable rasterVar = ((IRasterFunctionVariable)myRasterObject);
                    object rasterVal = FindPropertyInRow(rasterVar, pRow);
                    if (rasterVal != null && rasterVal is string)
                    {
                        string datasetPath = (string)rasterVal;
                        rasterVar.Value = OpenRasterDataset(datasetPath);
                    }
                }
            }
            catch (Exception exc)
            {
                System.Exception myExc = new System.Exception(
                    "Exception caught in ResolveRasterVal: " + exc.Message, exc);
                throw myExc;
            }
        }

        /// <summary>
        /// Check the Name and Alias properties of the given Raster Function Variable to see
        /// if they contain a field name and get the value of the corresponding field if needed.
        /// </summary>
        /// <param name="rasterFunctionVar">The Raster Function Variable to check.</param>
        /// <param name="pRow">The row corresponding to the function raster dataset.</param>
        /// <returns></returns>
        private object FindPropertyInRow(IRasterFunctionVariable rasterFunctionVar, IRow pRow)
        {
            string varName = "";
            IStringArray varNames = new StrArray();
            varName = rasterFunctionVar.Name;
            // If the name of  the variable contains '@Field'
            if (varName.Contains("@Field."))
                varNames.Add(varName); // Add it to the list of names.
            // Check the aliases of the variable
            for (int i = 0; i < rasterFunctionVar.Aliases.Count; ++i)
            {
                // Check the list of aliases for the '@Field' string
                varName = rasterFunctionVar.Aliases.get_Element(i);
                if (varName.Contains("@Field."))
                    varNames.Add(varName); // and add any that are found to the list of names.
            }

            // Use the list of names and find the value by looking up the appropriate field.
            for (int i = 0; i < varNames.Count; ++i)
            {
                // Get the variable name containing the field string
                varName = varNames.get_Element(i);
                // Replace the '@Field' with nothing to get just the name of the field.
                string fieldName = varName.Replace("@Field.", "");
                IFields rowFields = pRow.Fields;
                // Look up the index of the field name in the row.
                int fieldIndex = rowFields.FindField(fieldName);
                // If it is a valid index and the field type is string, return the value.
                if (fieldIndex != -1 &&
                   ((rowFields.get_Field(fieldIndex)).Type == esriFieldType.esriFieldTypeString))
                    return pRow.get_Value(fieldIndex);
            }
            // If no value has been returned yet, return null.
            return null;
        }

        /// <summary>
        /// Open the Raster Dataset given the path to the file.
        /// </summary>
        /// <param name="path">Path to the Raster Dataset file.</param>
        /// <returns>The opened Raster Dataset.</returns>
        private IRasterDataset OpenRasterDataset(string path)
        {
            try
            {
                string inputWorkspace = System.IO.Path.GetDirectoryName(path);
                string inputDatasetName = System.IO.Path.GetFileName(path);
                Type factoryType = Type.GetTypeFromProgID("esriDataSourcesRaster.RasterWorkspaceFactory");
                IWorkspaceFactory workspaceFactory = (IWorkspaceFactory)Activator.CreateInstance(factoryType);
                IWorkspace workspace = workspaceFactory.OpenFromFile(inputWorkspace, 0);
                IRasterWorkspace rasterWorkspace = (IRasterWorkspace)workspace;
                IRasterDataset myRasterDataset = rasterWorkspace.OpenRasterDataset(inputDatasetName);
                return myRasterDataset;
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }
        #endregion

        #region IPersistVariant Members

        /// <summary>
        /// UID to identify the object.
        /// </summary>
        public UID ID
        {
            get
            {
                return myUID;
            }
        }

        /// <summary>
        /// Load the properties of the argument object from the stream provided
        /// </summary>
        /// <param name="Stream">Stream that contains the serialized form of the argument object</param>
        public void Load(IVariantStream Stream)
        {
            if (Stream is IDocumentVersion)
            {
                IDocumentVersion docVersion = (IDocumentVersion)Stream;
                if (docVersion.DocumentVersion >= esriArcGISVersion.esriArcGISVersion10)
                {
                    esriArcGISVersion streamVersion = (esriArcGISVersion)((int)Stream.Read());
                    if (streamVersion >= esriArcGISVersion.esriArcGISVersion10)
                    {
                        myName = (string)Stream.Read();
                        myDescription = (string)Stream.Read();
                        myProperties = (IPropertySet)Stream.Read();
                    }
                }
            }
        }

        /// <summary>
        /// Save the properties of the argument object to the stream provided
        /// </summary>
        /// <param name="Stream">Stream to which to serialize the argument object into</param>
        public void Save(IVariantStream Stream)
        {
            if (Stream is IDocumentVersion)
            {
                IDocumentVersion docVersion = (IDocumentVersion)Stream;
                if (docVersion.DocumentVersion >= esriArcGISVersion.esriArcGISVersion10)
                {
                    object names = null, values = null;
                    myProperties.GetAllProperties(out names, out values);
                    string[] nameArray = (string[])names;
                    object[] valArray = (object[])values;
                    for (int i = 0; i < nameArray.GetLength(0); ++i)
                    {
                        if (valArray[i] is IDataset)
                        {
                            IName myDatasetName = ((IDataset)valArray[i]).FullName;
                            myProperties.SetProperty(nameArray[i], myDatasetName);
                        }
                    }
                    Stream.Write((int)esriArcGISVersion.esriArcGISVersion10);
                    Stream.Write(myName);
                    Stream.Write(myDescription);
                    Stream.Write(myProperties);
                }
            }
        }

        #endregion

        #region IDocumentVersionSupportGEN Members

        /// <summary>
        /// Convert the instance into an object supported by the given version
        /// </summary>
        /// <param name="docVersion">Version to convert to</param>
        /// <returns>Object that supports given version</returns>
        public object ConvertToSupportedObject(esriArcGISVersion docVersion)
        {
            return null;
        }

        /// <summary>
        /// Check if the object is supported at the given version
        /// </summary>
        /// <param name="docVersion">Version to check against</param>
        /// <returns>True if the object is supported</returns>
        public bool IsSupportedAtVersion(esriArcGISVersion docVersion)
        {
            if (docVersion >= esriArcGISVersion.esriArcGISVersion10)
                return true;
            else
                return false;
        }

        #endregion

        #region IXMLSerialize Members

        /// <summary>
        /// Deserialize the argument object from the datastream provided
        /// </summary>
        /// <param name="data">Xml stream to deserialize the argument object from</param>
        public void Deserialize(IXMLSerializeData data)
        {
            int nameIndex = data.Find("Names");
            int valIndex = data.Find("Values");
            if (nameIndex != -1 && valIndex != -1)
            {
                IStringArray myNames = (IStringArray)data.GetVariant(nameIndex);
                IVariantArray myValues = (IVariantArray)data.GetVariant(valIndex);
                for (int i = 0; i < myNames.Count; ++i)
                {
                    myProperties.SetProperty(myNames.get_Element(i),
                        myValues.get_Element(i));
                }
            }
        }

        /// <summary>
        /// Serialize the argument object into the stream provided.
        /// </summary>
        /// <param name="data">Xml stream to serialize the argument object into</param>
        public void Serialize(IXMLSerializeData data)
        {
            #region Prepare PropertySet
            object names = null, values = null;
            myProperties.GetAllProperties(out names, out values);
            IStringArray myNames = new StrArray();
            string[] nameArray = (string[])names;
            IVariantArray myValues = new VarArray();
            object[] valArray = (object[])values;
            for (int i = 0; i < nameArray.GetLength(0); ++i)
            {
                myNames.Add(nameArray[i]);
                if (valArray[i] is IDataset)
                {
                    IName myDatasetName = ((IDataset)valArray[i]).FullName;
                    myValues.Add(myDatasetName);
                }
                else
                    myValues.Add(valArray[i]);
            }
            #endregion
            data.TypeName = "NDVICustomFunctionArguments";
            data.TypeNamespaceURI = @"http://www.esri.com/schemas/ArcGIS/10.2";
            data.AddObject("Names", myNames);
            data.AddObject("Values", myValues);
        }

        #endregion

        #region IXMLVersionSupport Members

        /// <summary>
        /// Returns the namespaces supported by the object
        /// </summary>
        public string MinNamespaceSupported
        {
            get { return @"http://www.esri.com/schemas/ArcGIS/10.2"; }
        }

        #endregion
    }
}
