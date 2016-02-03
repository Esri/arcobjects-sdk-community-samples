using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.ADF.CATIDs;

namespace CustomFunction
{
    /// <summary>
    /// Enumerator for the location of the watermark.
    /// </summary>
    [Guid("148F193A-0D46-4d4c-BC9C-A05AC4BE0BAB")]
    [ComVisible(true)]
    public enum esriWatermarkLocation
    {
        esriWatermarkTopLeft = 0,
        esriWatermarkTopRight,
        esriWatermarkCenter,
        esriWatermarkBottomLeft,
        esriWatermarkBottomRight
    };

    [Guid("168721E7-7010-4a36-B886-F644437B164D")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("CustomFunction.WatermarkFunction")]
    [ComVisible(true)]
    public class WatermarkFunction : IRasterFunction, 
                                     IPersistVariant,
                                     IDocumentVersionSupportGEN,
                                     IXMLSerialize,
                                     IXMLVersionSupport
    {
        #region Private Members
        UID myUID; // UID for the Watermark Function.
        IRasterInfo myRasterInfo; // Raster Info for the Watermark Function
        rstPixelType myPixeltype; // Pixel Type of the Watermark Function.
        string myName; // Name of the Watermark Function.
        string myDescription; // Description of the Watermark Function.
        bool myValidFlag; // Flag to specify validity of the Watermark Function.
        string myWatermarkImagePath; // Path to the Watermark Image.
        double myBlendPercentage; // Percentage of the blending.
        double blendValue; // Actual value of the blend percentage.
        esriWatermarkLocation myWatermarkLocation; // Location of the Watermark.
        IRasterFunctionHelper myFunctionHelper; // Raster Function Helper object.
        Bitmap myWatermarkImage; // Watermark Image object.
        #endregion

        public WatermarkFunction()
        {
            myWatermarkImagePath = "";
            myBlendPercentage = 50.00; // Default value for the blending percentage.
            blendValue = 0.50; // Default value for the blend value.
            myWatermarkLocation = esriWatermarkLocation.esriWatermarkBottomRight;

            myName = "WatermarkFunction";
            myPixeltype = rstPixelType.PT_UNKNOWN; // Default value for the pixel type.
            myDescription = "Add a watermark to the request.";
            myValidFlag = true;

            myFunctionHelper = new RasterFunctionHelperClass();

            myWatermarkImage = null;

            myUID = new UIDClass();
            myUID.Value = "{" + "168721E7-7010-4a36-B886-F644437B164D" + "}";
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
                IWatermarkFunctionArguments watermarkFuncArgs = null;
                if (pArguments is IWatermarkFunctionArguments)
                {
                    watermarkFuncArgs = (IWatermarkFunctionArguments)pArguments;
                    myBlendPercentage = watermarkFuncArgs.BlendPercentage;
                    myWatermarkImagePath = watermarkFuncArgs.WatermarkImagePath;
                    myWatermarkLocation = watermarkFuncArgs.WatermarkLocation;
                    
                    object inputRaster = watermarkFuncArgs.Raster;
                    if (watermarkFuncArgs.Raster is IRasterFunctionVariable)
                    {
                        IRasterFunctionVariable rasterFunctionVariable =
                            (IRasterFunctionVariable)watermarkFuncArgs.Raster;
                        inputRaster = rasterFunctionVariable.Value;
                    }
                    
                    // Call the Bind method of the Raster Function Helper object.
                    myFunctionHelper.Bind(inputRaster);
                }
                else
                {
                    // Throw an error if incorrect arguments object is passed.
                    throw new System.Exception(
                        "Incorrect arguments object. Expected: IWatermarkFunctionArguments");
                }

                // Get the raster info and Pixel Type from the RasterFunctionHelper object.
                myRasterInfo = myFunctionHelper.RasterInfo;
                myPixeltype = myRasterInfo.PixelType;

                // Convert blending percentage to blending value.
                if (myBlendPercentage >= 0.0 && myBlendPercentage <= 100.0)
                    blendValue = myBlendPercentage / 100.0;
                else /// A value of 50% is used as default.
                    blendValue = 0.50;

                if (myWatermarkImagePath != "")
                {
                    // Load the watermark image from the path provided
                    myWatermarkImage = new Bitmap(myWatermarkImagePath);
                    // and check the pixel type of the loaded image to see if its compatible.
                    if (myWatermarkImage.PixelFormat != System.Drawing.Imaging.PixelFormat.Format32bppArgb &&
                        myWatermarkImage.PixelFormat != System.Drawing.Imaging.PixelFormat.Format24bppRgb)
                    {
                        // Throw error if the image is not compatible.
                        throw new System.Exception(
                            "Invalid watermark image. Please provide one with 8 bits per band in ARGB or RGB format.");
                    }

                    // Cleanup
                    myWatermarkImage.Dispose();
                    myWatermarkImage = null;
                }
            }
            catch (Exception exc)
            {
                #region Cleanup
                if (myWatermarkImage != null)
                    myWatermarkImage.Dispose();
                myWatermarkImage = null;
                #endregion

                System.Exception myExc = new System.Exception(
                    "Exception caught in Bind method of Watermark Function. " + exc.Message, exc);
                throw myExc;
            }
        }

        /// <summary>
        /// Read pixels from the input Raster and fill the PixelBlock provided with processed pixels.
        /// The RasterFunctionHelper object is used to handle pixel type conversion and resampling.
        /// The watermark image is then blended to the bottom right corner of the pixel block. 
        /// </summary>
        /// <param name="pTlc">Point to start the reading from in the Raster</param>
        /// <param name="pRaster">Reference Raster for the PixelBlock</param>
        /// <param name="pPixelBlock">PixelBlock to be filled in</param>
        public void Read(IPnt pTlc, IRaster pRaster, IPixelBlock pPixelBlock)
        {
            BitmapData wMBitmapData = null;
            double pixelValue = 0.0;
            int wmRow = 0;
            int wmCol = 0;
            try
            {
                // Call Read method of the Raster Function Helper object.
                myFunctionHelper.Read(pTlc, null, pRaster, pPixelBlock);

                int wMBandOffset = 0;

                #region Reference Raster Properties
                // Get the pixel type of the reference raster to see if 
                // it is compatible (8 bit).
                IRasterProps referenceProps = (IRasterProps)pRaster;
                if (referenceProps.PixelType != rstPixelType.PT_UCHAR &&
                    referenceProps.PixelType != rstPixelType.PT_CHAR)
                {
                    throw new System.Exception(
                        "Function can only be applied to 8bit data.");
                }

                #endregion

                #region Load watermark image object
                // Create new image object for the watermark image.
                myWatermarkImage = new Bitmap(myWatermarkImagePath);
                // Read number of bands of the watermark image.
                if (myWatermarkImage.PixelFormat == System.Drawing.Imaging.PixelFormat.Format32bppArgb)
                    wMBandOffset = 4;
                else
                {
                    if (myWatermarkImage.PixelFormat == System.Drawing.Imaging.PixelFormat.Format24bppRgb)
                        wMBandOffset = 3;
                    else
                    {
                        throw new System.Exception(
                            "Invalid bitmap. Please provide one with 8bits per band in ARGB or RGB format.");
                    }
                }
                #endregion

                int pBHeight = pPixelBlock.Height;
                int pBWidth = pPixelBlock.Width;
                int wMHeight = myWatermarkImage.Height;
                int wMWidth = myWatermarkImage.Width;
                int wMRowIndex = 0;
                int wMColIndex = 0;
                int pBRowIndex = 0;
                int pBColIndex = 0;
                int endRow = 0;
                int endCol = 0;
                bool waterStartCol = false;
                bool waterStartRow = false;

                // Calculate the row/column values that specify where to start the blending.
                #region Calculate Indices
                /// If the number of rows of the pixelblock are more than the watermark image
                endRow = pBHeight;
                if (pBHeight >= wMHeight)
                {
                    /// Set the row index to start blending in the pixelblock.
                    switch (myWatermarkLocation)
                    {
                        case esriWatermarkLocation.esriWatermarkTopLeft:
                            {
                                pBRowIndex = 0;
                                endRow = pBRowIndex + wMHeight;
                                break;
                            }
                        case esriWatermarkLocation.esriWatermarkTopRight:
                            {
                                pBRowIndex = 0;
                                endRow = pBRowIndex + wMHeight;
                                break;
                            }
                        case esriWatermarkLocation.esriWatermarkCenter:
                            {
                                pBRowIndex = (pBHeight / 2) - (wMHeight / 2);
                                endRow = pBRowIndex + wMHeight;
                                break;
                            }
                        case esriWatermarkLocation.esriWatermarkBottomLeft:
                            {
                                pBRowIndex = pBHeight - wMHeight;
                                break;
                            }
                        case esriWatermarkLocation.esriWatermarkBottomRight:
                            {
                                pBRowIndex = pBHeight - wMHeight;
                                break;
                            }
                        default:
                            break;
                    }

                    if (myWatermarkLocation == esriWatermarkLocation.esriWatermarkCenter)
                    {
                        pBRowIndex = (pBHeight / 2) - (wMHeight / 2);
                        endRow = pBRowIndex + wMHeight;
                    }
                }
                else /// If the number of rows of the watermark image is more than that of the pixelblock.
                {
                    /// Set the row index to start blending in the watermark image.
                    wMRowIndex = (wMHeight - pBHeight);
                    waterStartRow = true;
                }

                /// If the number of cols of the pixelblock are more than the watermark image
                endCol = pBWidth;
                if (pBWidth >= wMWidth)
                {
                    /// Set the col index to start blending in the pixelblock.
                    /// Set the row index to start blending in the pixelblock.
                    switch (myWatermarkLocation)
                    {
                        case esriWatermarkLocation.esriWatermarkTopLeft:
                            {
                                pBColIndex = 0;
                                endCol = pBColIndex + wMWidth;
                                break;
                            }
                        case esriWatermarkLocation.esriWatermarkTopRight:
                            {
                                pBColIndex = pBWidth - wMWidth;
                                break;
                            }
                        case esriWatermarkLocation.esriWatermarkCenter:
                            {
                                pBColIndex = (pBWidth / 2) - (wMWidth / 2);
                                endCol = pBColIndex + wMWidth;
                                break;
                            }
                        case esriWatermarkLocation.esriWatermarkBottomLeft:
                            {
                                pBColIndex = 0;
                                endCol = pBColIndex + wMWidth;
                                break;
                            }
                        case esriWatermarkLocation.esriWatermarkBottomRight:
                            {
                                pBColIndex = pBWidth - wMWidth;
                                break;
                            }
                        default:
                            break;
                    }
                }
                else /// If the number of cols of the watermark image is more than that of the pixelblock.
                {
                    /// Set the col index to start blending in the watermark image.
                    wMColIndex = (wMWidth - pBWidth);
                    waterStartCol = true;
                }
                #endregion

                #region Prepare Watermark Image for reading
                // Get the pixels from the watermark image using the lockbits function.
                wMBitmapData = myWatermarkImage.LockBits(new Rectangle(0, 0, wMWidth, wMHeight),
                    ImageLockMode.ReadOnly, myWatermarkImage.PixelFormat);

                System.IntPtr wMScan0 = wMBitmapData.Scan0;
                int wMStride = wMBitmapData.Stride;
                #endregion

                // The unsafe keyword is used so that pointers can be used to access pixels
                // from the watermark image.
                unsafe
                {
                    int wMPaddingOffset = wMStride - (myWatermarkImage.Width * wMBandOffset);

                    // Start filling from the correct row, col in the pixelblock 
                    // using the indices calculated above
                    System.Array pixelValues;
                    if (pPixelBlock.Planes == 3)
                    {
                        if (wMBandOffset == 4) // To check for transparency in WM Image
                        {
                            #region 3 Band PixelBlock
                            for (int nBand = 0; nBand < pPixelBlock.Planes; ++nBand)
                            {
                                byte* wMStartByte = (byte*)(void*)wMScan0;

                                /// If the number of rows of the watermark image are more than the request.
                                if (waterStartRow) /// Skip to the correct row in the watermark image.
                                    wMStartByte += (wMStride * wMRowIndex);

                                IPixelBlock3 ipPixelBlock = (IPixelBlock3)pPixelBlock;
                                pixelValues = (System.Array)(ipPixelBlock.get_PixelData(nBand));
                                for (int i = pBRowIndex; i < endRow; i++, ++wmRow)
                                {
                                    /// If the number of cols of the watermark image are more than the request.
                                    if (waterStartCol) /// Skip to the correct column in the watermark image.                                
                                        wMStartByte += (wMColIndex * wMBandOffset);

                                    for (int k = pBColIndex; k < endCol; k++, ++wmCol)
                                    {
                                        pixelValue = Convert.ToDouble(pixelValues.GetValue(k, i));
                                        if (Convert.ToDouble(wMStartByte[3]) != 0.0 &&
                                            Convert.ToByte(ipPixelBlock.GetNoDataMaskVal(nBand, k, i)) == 1)
                                        {
                                            // Blend the pixelValue from the PixelBlock with the corresponding
                                            // pixel from the watermark image.
                                            pixelValue = ((1 - blendValue) * pixelValue) + (blendValue *
                                                Convert.ToDouble(wMStartByte[2 - nBand]));
                                        }
                                        pixelValues.SetValue(Convert.ToByte(pixelValue), k, i);

                                        wMStartByte += wMBandOffset;
                                    }
                                    wMStartByte += wMPaddingOffset;
                                }
                                ((IPixelBlock3)pPixelBlock).set_PixelData(nBand, pixelValues);
                            }
                            #endregion
                        }
                        else
                        {
                            #region 3 Band PixelBlock
                            for (int nBand = 0; nBand < pPixelBlock.Planes; ++nBand)
                            {
                                byte* wMStartByte = (byte*)(void*)wMScan0;

                                /// If the number of rows of the watermark image are more than the request.
                                if (waterStartRow) /// Skip to the correct row in the watermark image.
                                    wMStartByte += (wMStride * wMRowIndex);

                                IPixelBlock3 ipPixelBlock = (IPixelBlock3)pPixelBlock;
                                pixelValues = (System.Array)(ipPixelBlock.get_PixelData(nBand));
                                for (int i = pBRowIndex; i < endRow; i++, ++wmRow)
                                {
                                    /// If the number of cols of the watermark image are more than the request.
                                    if (waterStartCol) /// Skip to the correct column in the watermark image.                                
                                        wMStartByte += (wMColIndex * wMBandOffset);

                                    for (int k = pBColIndex; k < endCol; k++, ++wmCol)
                                    {
                                        pixelValue = Convert.ToDouble(pixelValues.GetValue(k, i));
                                        if (Convert.ToByte(ipPixelBlock.GetNoDataMaskVal(nBand, k, i)) == 1)
                                        {
                                            // Blend the pixelValue from the PixelBlock with the corresponding
                                            // pixel from the watermark image.
                                            pixelValue = ((1 - blendValue) * pixelValue) + (blendValue *
                                                Convert.ToDouble(wMStartByte[2 - nBand]));
                                        }
                                        pixelValues.SetValue(Convert.ToByte(pixelValue), k, i);

                                        wMStartByte += wMBandOffset;
                                    }
                                    wMStartByte += wMPaddingOffset;
                                }
                                ((IPixelBlock3)pPixelBlock).set_PixelData(nBand, pixelValues);
                            }
                            #endregion
                        }
                    }
                    else
                    {
                        if (wMBandOffset == 4) // To check for transparency in WM Image
                        {
                            #region Not 3 Band PixelBlock
                            for (int nBand = 0; nBand < pPixelBlock.Planes; ++nBand)
                            {
                                byte* wMStartByte = (byte*)(void*)wMScan0;

                                /// If the number of rows of the watermark image are more than the request.
                                if (waterStartRow) /// Skip to the correct row in the watermark image.
                                    wMStartByte += (wMStride * wMRowIndex);

                                IPixelBlock3 ipPixelBlock = (IPixelBlock3)pPixelBlock;
                                pixelValues = (System.Array)(ipPixelBlock.get_PixelData(nBand));
                                int nooftimes = 0;
                                int noofskips = 0;
                                for (int i = pBRowIndex; i < endRow; i++, ++wmRow)
                                {
                                    /// If the number of cols of the watermark image are more than the request.
                                    if (waterStartCol) /// Skip to the correct column in the watermark image.                                
                                        wMStartByte += (wMColIndex * wMBandOffset);

                                    for (int k = pBColIndex; k < endCol; k++, ++wmCol)
                                    {
                                        pixelValue = Convert.ToDouble(pixelValues.GetValue(k, i));
                                        if (Convert.ToByte(ipPixelBlock.GetNoDataMaskVal(nBand, k, i)) == 1)
                                        //Convert.ToDouble(wMStartByte[3]) != 0.0 &&
                                        {
                                            // Calculate the average value of the pixels of the watermark image
                                            double avgValue = (Convert.ToDouble(wMStartByte[0]) +
                                                               Convert.ToDouble(wMStartByte[1]) +
                                                               Convert.ToDouble(wMStartByte[2])) / 3;

                                            // and blend it with the pixelValue from the PixelBlock.
                                            pixelValue = ((1 - blendValue) * pixelValue) +
                                                         (blendValue * avgValue);
                                        }
                                        pixelValues.SetValue(Convert.ToByte(pixelValue), k, i);

                                        ++nooftimes;
                                        noofskips += wMBandOffset;
                                        wMStartByte += wMBandOffset;
                                    }
                                    wMStartByte += wMPaddingOffset;
                                }
                                ((IPixelBlock3)pPixelBlock).set_PixelData(nBand, pixelValues);
                            }
                            #endregion
                        }
                        else
                        {
                            #region Not 3 Band PixelBlock
                            for (int nBand = 0; nBand < pPixelBlock.Planes; ++nBand)
                            {
                                byte* wMStartByte = (byte*)(void*)wMScan0;

                                /// If the number of rows of the watermark image are more than the request.
                                if (waterStartRow) /// Skip to the correct row in the watermark image.
                                    wMStartByte += (wMStride * wMRowIndex);

                                IPixelBlock3 ipPixelBlock = (IPixelBlock3)pPixelBlock;
                                pixelValues = (System.Array)(ipPixelBlock.get_PixelData(nBand));
                                int nooftimes = 0;
                                int noofskips = 0;
                                for (int i = pBRowIndex; i < endRow; i++, ++wmRow)
                                {
                                    /// If the number of cols of the watermark image are more than the request.
                                    if (waterStartCol) /// Skip to the correct column in the watermark image.                                
                                        wMStartByte += (wMColIndex * wMBandOffset);

                                    for (int k = pBColIndex; k < endCol; k++, ++wmCol)
                                    {
                                        pixelValue = Convert.ToDouble(pixelValues.GetValue(k, i));
                                        if (Convert.ToByte(ipPixelBlock.GetNoDataMaskVal(nBand, k, i)) == 1)
                                        {
                                            // Calculate the average value of the pixels of the watermark image
                                            double avgValue = (Convert.ToDouble(wMStartByte[0]) +
                                                               Convert.ToDouble(wMStartByte[1]) +
                                                               Convert.ToDouble(wMStartByte[2])) / 3;

                                            // and blend it with the pixelValue from the PixelBlock.
                                            pixelValue = ((1 - blendValue) * pixelValue) +
                                                         (blendValue * avgValue);
                                        }
                                        pixelValues.SetValue(Convert.ToByte(pixelValue), k, i);

                                        ++nooftimes;
                                        noofskips += wMBandOffset;
                                        wMStartByte += wMBandOffset;
                                    }
                                    wMStartByte += wMPaddingOffset;
                                }
                                ((IPixelBlock3)pPixelBlock).set_PixelData(nBand, pixelValues);
                            }
                            #endregion
                        }
                    }
                }

                #region Cleanup
                myWatermarkImage.UnlockBits(wMBitmapData);
                myWatermarkImage.Dispose();
                myWatermarkImage = null;
                wMBitmapData = null;
                wMScan0 = (System.IntPtr)null;
                wMStride = 0;
                #endregion
            }
            catch (Exception exc)
            {
                #region Cleanup
                if (wMBitmapData != null)
                    myWatermarkImage.UnlockBits(wMBitmapData);
                wMBitmapData = null;
                if (myWatermarkImage != null)
                    myWatermarkImage.Dispose();
                myWatermarkImage = null;
                #endregion

                System.Exception myExc = new System.Exception(
                    "Exception caught in Read method of Watermark Function. " + exc.Message, exc);
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
                    "Exception caught in Update method of Watermark Function", exc);
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
            data.TypeName = "WatermarkFunction";
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

    [Guid("933A9DEF-D56F-4e37-911D-AC16982CA697")]
    [InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IWatermarkFunctionArguments : IRasterFunctionArguments, IPersistVariant, IXMLSerialize
    {
        #region WatermarkFunctionArguments Members
        [DispId(0x50505001)]
        object Raster
        {
            get;
            set;
        }

        [DispId(0x50505002)]
        string WatermarkImagePath
        {
            get;
            set;
        }

        [DispId(0x50505003)]
        double BlendPercentage
        {
            get;
            set;
        }

        [DispId(0x50505004)]
        esriWatermarkLocation WatermarkLocation
        {
            get;
            set;
        }
        #endregion
    }

    [Guid("996DC8E5-086B-41b5-919A-A3B9B86F2B30")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("CustomFunction.WatermarkFunctionArguments")]
    [ComVisible(true)]
    public class WatermarkFunctionArguments : IWatermarkFunctionArguments, 
                                              IPersistVariant,
                                              IDocumentVersionSupportGEN,
                                              IXMLSerialize,
                                              IXMLVersionSupport
    {
        #region Private Members
        string myName;
        string myDescription;
        UID myUID; // UID for the Watermark Function.
        IPropertySet myProperties; // Property set to store the properties of the arguments object.
        #endregion

        public WatermarkFunctionArguments()
        {
            myName = "WatermarkFunctionArguments";
            myDescription = "Arguments object for the Watermark Function";

            // Set default values
            myProperties = new PropertySetClass();
            myProperties.SetProperty("Raster", null);
            myProperties.SetProperty("WatermarkImagePath", "");
            myProperties.SetProperty("BlendPercentage", 50.00);
            myProperties.SetProperty("WatermarkLocation", CustomFunction.esriWatermarkLocation.esriWatermarkBottomRight);

            myUID = new UIDClass();
            myUID.Value = "{" + "996DC8E5-086B-41b5-919A-A3B9B86F2B30" + "}";
        }

        #region WatermarkFunctionArguments Members
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
        /// Path to the image to blend into the raster.
        /// </summary>
        public string WatermarkImagePath
        {
            get
            {
                return Convert.ToString(GetDereferencedValue("WatermarkImagePath"));
            }
            set
            {
                myProperties.SetProperty("WatermarkImagePath", value);
            }
        }

        /// <summary>
        /// Percentage value by which to blend the watermark image with the raster
        /// </summary>
        public double BlendPercentage
        {
            get
            {
                return Convert.ToDouble(GetDereferencedValue("BlendPercentage"));
            }
            set
            {
                myProperties.SetProperty("BlendPercentage", value);
            }
        }

        public esriWatermarkLocation WatermarkLocation
        {
            get
            {
                return (esriWatermarkLocation)(Convert.ToInt16(GetDereferencedValue("WatermarkLocation")));
            }
            set
            {
                myProperties.SetProperty("WatermarkLocation", value);
            }
        }

        /// <summary>
        /// Dereference and return the value of the property whose name is given if necessary.
        /// </summary>
        /// <param name="name">Name of the property to check.</param>
        /// <returns>Dereferenced value of the property corresponding to the name given.</returns>
        public object GetDereferencedValue(string name)
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
                for (int i = 0; i < nameArray.GetLength(0);++i)
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
            myProperties = new PropertySetClass();
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
            try
            {
                ResolveRasterVal(pRow);
                ResolveBlendPVal(pRow);
                ResolveWatermarkPathVal(pRow);
            }
            catch (Exception exc)
            {
                System.Exception myExc = new System.Exception(
                    "Exception caught in Resolve: " + exc.Message, exc);
                throw myExc;
            }
        }

        /// <summary>
        /// Update the variables containing field names to their updated values.
        /// </summary>
        /// <param name="pRow">The row corresponding to the function raster dataset.</param>
        /// <param name="pPropertySet">Property Set to add the list of the names and the updated values to.</param>
        /// <param name="pTemplateArguments">The arguments object containing the properties to update if</param>
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
                        // Open the Raster Dataset from the path provided.
                        string datasetPath = (string)rasterVal;
                        IRasterDataset rasterDataset = null;
                        rasterVar.Value = rasterDataset;
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

        /// <summary>
        /// Resolve the 'BlendPercentage' variable if it contains field names with the corresponding values.
        /// </summary>
        /// <param name="pRow">The row corresponding to the function raster dataset.</param>
        private void ResolveBlendPVal(IRow pRow)
        {
            // Get the BlendPercentage property.
            object myRasterObject = myProperties.GetProperty("BlendPercentage");
            // Check to see if it is a variable
            if (myRasterObject is IRasterFunctionVariable)
            {
                IRasterFunctionVariable bpVar = ((IRasterFunctionVariable)myRasterObject);
                object rasterVal = FindPropertyInRow(bpVar, pRow);
                if (rasterVal != null && rasterVal is string)
                {
                    // Get the blend percentage value from string
                    try { bpVar.Value = Convert.ToDouble((string)rasterVal); }
                    catch (Exception) { }
                }
            }
        }

        /// <summary>
        /// Resolve the 'WatermarkImagePath' variable if it contains field names with the corresponding values.
        /// </summary>
        /// <param name="pRow">The row corresponding to the function raster dataset.</param>
        private void ResolveWatermarkPathVal(IRow pRow)
        {
            // Get the WatermarkImagePath property.
            object myRasterObject = myProperties.GetProperty("WatermarkImagePath");
            // Check to see if it is a variable
            if (myRasterObject is IRasterFunctionVariable)
            {
                IRasterFunctionVariable wipVar = ((IRasterFunctionVariable)myRasterObject);
                object rasterVal = FindPropertyInRow(wipVar, pRow);
                if (rasterVal != null && rasterVal is string)
                {
                    // Get the blend percentage value from string
                    wipVar.Value = (string)rasterVal;
                }
            }
        }

        /// <summary>
        /// Check the Name and Alias properties of the given Raster Function Variable to see
        /// if they contain a reference to a field and get the value of the corresponding field if needed.
        /// </summary>
        /// <param name="rasterFunctionVar">The Raster Function Variable to check.</param>
        /// <param name="pRow">The row corresponding to the function raster dataset.</param>
        /// <returns></returns>
        private object FindPropertyInRow(IRasterFunctionVariable rasterFunctionVar, IRow pRow)
        {
            string varName = "";
            IStringArray varNames = new StrArrayClass();
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
        /// Deserialize the argument object from the stream provided
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
            data.TypeName = "WatermarkFunctionArguments";
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
