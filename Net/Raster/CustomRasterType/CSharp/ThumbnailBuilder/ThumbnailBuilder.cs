using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.ADF.CATIDs;
using System.Runtime.InteropServices;

namespace CustomRasterBuilder
{
    interface IThumbnailBuilder : IRasterBuilder
    {
        #region ThumbnailBuilder Members
        ///<summary>
        /// Raster Builder to which the ThumbnailBuilder is attached.
        ///</summary>
        IRasterBuilder InnerRasterBuilder
        {
            get;
            set;
        }
        #endregion
    };

    /// <summary>
    /// The Raster Type Factory class that is used to create the Custom 
    /// Raster Type.
    /// </summary>
    [Guid("C6629CC4-B301-451a-9481-4D7751E9701C")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("CustomRasterBuilder.ThumbnailFactory")]
    [ComVisible(true)]
    public class ThumbnailFactory : IRasterTypeFactory
    {
        #region Private Members
        IStringArray myRasterTypeNames; // List of Raster Types that the factory can create.
        UID myUID; // UID for the Thumbnail Raster type.
        #endregion

        #region IRasterTypeFactory Members

        public ThumbnailFactory()
        {
            // The Raster Type name should follow the pattern 
            // 'Thumbnail ' follwed by the name of the built-in 
            // Raster Type to attach the Thumbnail Builder to.            
            myRasterTypeNames = new StrArrayClass();
            myRasterTypeNames.Add("Thumbnail Raster Dataset");
            myRasterTypeNames.Add("Thumbnail QuickBird");

            myUID = new UIDClass();
            myUID.Value = "{C6629CC4-B301-451a-9481-4D7751E9701C}";
        }

        /// <summary>
        /// The UID for the factory.
        /// </summary>
        public UID CLSID
        {
            get { return myUID; }
        }

        /// <summary>
        /// The main function which creates the Raster Type object.
        /// </summary>
        /// <param name="RasterTypeName">Name of the Raster Type to create.</param>
        /// <returns></returns>
        public IRasterType CreateRasterType(string RasterTypeName)
        {
            try
            {
                switch (RasterTypeName)
                {
                    case "Thumbnail Raster Dataset":
                        {
                            // Create a Raster Type Name object.
                            IRasterTypeName theRasterTypeName = new RasterTypeNameClass();
                            // Assign the name of the built-in Raster Type to the name object.
                            // The Name field accepts a path to an .art file as well 
                            // the name for a built-in Raster Type.
                            theRasterTypeName.Name = RasterTypeName.Replace("Thumbnail ", "");
                            // Use the Open function from the IName interface to get the Raster Type object.
                            IRasterType theRasterType = (IRasterType)(((IName)theRasterTypeName).Open());
                            if (theRasterType == null)
                            {
                                Console.WriteLine("Error:Raster Type not found " + theRasterTypeName.Name);
                                return null;
                            }

                            // Create a new ThumbnailBuilder object and set it's InnerRasterBuilder property to 
                            // the RasterBuilder from the RasterType object. Then set the thumbnail builder to 
                            // be the RasterBuilder for the RasterType object. This inserts the thumbnail builder in 
                            // between the RasterType and it's RasterBuilder.

                            // Create the Thumbnail Builder 
                            IRasterBuilder thumbnailBuilder = new ThumbnailBuilder();
                            //  Set the InnerRasterBuilder property with current Raster Type's Raster Builder
                            ((ThumbnailBuilder)thumbnailBuilder).InnerRasterBuilder = theRasterType.RasterBuilder;
                            // Set the Raster Builder of theRasterType to the above created thumbnail builder.
                            theRasterType.RasterBuilder = thumbnailBuilder;
                            IName theName = theRasterType.FullName;
                            ((IRasterTypeName)theName).Name = "Thumbnail Raster Dataset";
                            return theRasterType;
                        }

                    case "Thumbnail QuickBird":
                        {
                            // Create a Raster Type Name object.
                            IRasterTypeName theRasterTypeName = new RasterTypeNameClass();
                            // Assign the name of the built-in Raster Type to the name object.
                            // The Name field accepts a path to an .art file as well 
                            // the name for a built-in Raster Type.
                            theRasterTypeName.Name = RasterTypeName.Replace("Thumbnail ", "");
                            // Use the Open function from the IName interface to get the Raster Type object.
                            IRasterType theRasterType = (IRasterType)(((IName)theRasterTypeName).Open());
                            if (theRasterType == null)
                            {
                                Console.WriteLine("Error:Raster Type not found " + theRasterTypeName.Name);
                                return null;
                            }

                            // Create a new TumbnailBuilder object and set it's InnerRasterBuilder property to 
                            // the RasterBuilder from the RasterType object. Then set the thumbnail builder to 
                            // be the RasterBuilder for the RasterType object. This inserts the thumbnail builder in 
                            // between the RasterType and it's RasterBuilder.

                            // Create the Thumbnail Builder 
                            IRasterBuilder thumbnailBuilder = new ThumbnailBuilder();
                            //  Set the InnerRasterBuilder property with current Raster Type's Raster Builder
                            ((ThumbnailBuilder)thumbnailBuilder).InnerRasterBuilder = theRasterType.RasterBuilder;
                            // Set the Raster Builder of theRasterType to the above created thumbnail builder.
                            theRasterType.RasterBuilder = thumbnailBuilder;
                            IName theName = theRasterType.FullName;
                            ((IRasterTypeName)theName).Name = "Thumbnail QuickBird";
                            return theRasterType;
                        }

                    default:
                        return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error: Failed to create " + RasterTypeName + ": " + ex.Message);
            }
        }

        /// <summary>
        /// Name of the Raster Type factory.
        /// </summary>
        public string Name
        {
            get { return "Thumbnail Raster Type Factory"; }
        }

        /// <summary>
        /// List of Raster Types which the factory can create.
        /// </summary>
        public IStringArray RasterTypeNames
        {
            get { return myRasterTypeNames; }
        }
        #endregion

        #region COM Registration Function(s)
        [ComRegisterFunction()]
        static void Reg(string regKey)
        {
            ESRI.ArcGIS.ADF.CATIDs.RasterTypeFactory.Register(regKey);
        }

        [ComUnregisterFunction()]
        static void Unreg(string regKey)
        {
            ESRI.ArcGIS.ADF.CATIDs.RasterTypeFactory.Unregister(regKey);
        }
        #endregion
    }

    ///<summary>
    /// This class implements the interface IThumbnailBuilder, IPersistVariant.
    ///</summary>
    [Guid("5CACFBA7-F865-4873-B57C-BE2A46E1C5E3")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("CustomRasterBuilder.ThumbnailBuilder")]
    [ComVisible(true)]
    public class ThumbnailBuilder : IThumbnailBuilder, IPersistVariant, IRasterBuilderInit
    {
        #region Private Members
        public IRasterBuilder innerRasterBuilder; // Inner Raster Builder 
        UID myUID; // UID for the Thumbnail Builder.

        /// <summary>
        /// The following function creates a field called "ThumbNail" and adds it to the existing
        /// fields in the mosaic dataset catalog as a blob.
        /// </summary>
        /// <param name="myFields">List of fields added to the Mosaic Catalog</param>
        private static void AddingNewField(IFields myFields)
        {
            IField pField = new FieldClass(); // Create a new field object
            IFieldEdit objectIDFieldEditor = (IFieldEdit)pField; // Set the field editor for this field
            objectIDFieldEditor.Name_2 = "ThumbNail"; // Set the name of the field
            objectIDFieldEditor.Type_2 = esriFieldType.esriFieldTypeBlob; // Set the type of the field as Blob
            IFieldsEdit fieldsEditor = (IFieldsEdit)myFields; // Add the newly created field to list of existing fields
            fieldsEditor.AddField(pField);
            return;
        }

        /// <summary>
        /// Each BuilderItem contains a Function Raster Dataset which is used to create the thumbnail.
        /// The thumbnail is created by Nearest Neighbor resampling of the Function Raster Dataset. 
        /// The resampled raster is exported as a byte array and saved Into the IMemoryBlobStreamVariant. 
        /// The blob is then inserted into the Thumbnail field.
        /// </summary>
        /// <param name="mybuilderItem">Item containing the Function Dataset to be added to the Mosaic Dataset</param>
        private void Resampling(IBuilderItem mybuilderItem)
        {
            try
            {
                IFunctionRasterDataset pFuncRasterDataset = mybuilderItem.Dataset; // Get the FunctionRasterDataset from mybuilderItem
                IRasterDataset pRasterDataset;
                pRasterDataset = (IRasterDataset)pFuncRasterDataset; // Cast the FunctionRasterDataset to Raster Dataset
                IPropertySet thePropSet = pFuncRasterDataset.Properties; // Get the properties of the raster Dataset
                IRaster praster = pRasterDataset.CreateDefaultRaster(); // Create default raster from the above raster dataset
                praster.ResampleMethod = rstResamplingTypes.RSP_NearestNeighbor; // The raster is resampled by RSP_NearestNeighbor
                IRasterProps pRasterProps = (IRasterProps)praster; // Raster properties are used to update the height, width of the raster
                pRasterProps.Height = 256;
                pRasterProps.Width = 256;

                IRasterExporter pConverter = new RasterExporterClass(); // IRasterExporter object is used to convert the raster to byte array.
                byte[] pBytesArr;
                pBytesArr = pConverter.ExportToBytes(praster, "TIFF"); // Convert the resampled Raster to a Byte array 
                IMemoryBlobStream memBlobStream = new MemoryBlobStream(); // Create new IMemoryBlobStream
                IMemoryBlobStreamVariant varBlobStream = (IMemoryBlobStreamVariant)memBlobStream; // Assign to IMemoryBlobStreamVariant
                object anObject = pBytesArr;
                varBlobStream.ImportFromVariant(anObject); // IMemoryBlobStreamVariant object is assigned the byte array
                thePropSet.SetProperty("ThumbNail", memBlobStream); // and saved to the property "ThumbNail"
            }
            catch (Exception ex)
            {
                System.Exception myExc = new System.Exception(
                    "Error: Failed to Re-Sampled the raster.Thumbnails will not be created." + ex.Message, ex);
                throw myExc;
            }
            return;

        }
        #endregion

        #region ThumbnailBuilder Members
        /// <summary>
        /// This property gets and sets the raster type of the Thumbnail Builder.
        /// </summary>
        public IRasterBuilder InnerRasterBuilder
        {
            get
            {
                return innerRasterBuilder;
            }
            set
            {
                innerRasterBuilder = value;
            }
        }

        public ThumbnailBuilder()
        {
            innerRasterBuilder = null;

            myUID = new UIDClass();
            myUID.Value = "{" + "5CACFBA7-F865-4873-B57C-BE2A46E1C5E3" + "}";
        }
        #endregion

        #region IRasterBuilder Members
        /// <summary>
        /// AuxiliaryFieldAlias property gets and sets the Auxiliary fields Alias
        /// present in the raster builder
        /// </summary>
        public ESRI.ArcGIS.esriSystem.IPropertySet AuxiliaryFieldAlias
        {
            get
            {
                return innerRasterBuilder.AuxiliaryFieldAlias;
            }
            set
            {
                innerRasterBuilder.AuxiliaryFieldAlias = value;
            }
        }

        /// <summary>
        /// Flag to specify whether the Raster Builder can build items in place.
        /// </summary>
        public bool CanBuildInPlace
        {
            get { return innerRasterBuilder.CanBuildInPlace; }
        }

        /// <summary>
        /// For adding Thumbnails, a new field called "Thumbnail" of type blob is created
        /// and added to the Auxiliary Fields in raster builder.
        /// </summary>
        public IFields AuxiliaryFields
        {
            get
            {
                ESRI.ArcGIS.DataSourcesRaster.IRasterBuilder pRasterBuilder = innerRasterBuilder;
                IFields myFields = pRasterBuilder.AuxiliaryFields; // Existing Fields present in the Raster Builder. 
                try
                {
                    int count = myFields.FindField("ThumbNail"); // Check if the Field "ThumbNail" already exists.
                    if (count == -1)
                    {
                        AddingNewField(myFields); // If not add the new field "ThumbNail"
                        pRasterBuilder.AuxiliaryFields = myFields; // Assign the updated Fields back to the raster builder.
                    }
                }
                catch (Exception ex)
                {
                    System.Exception myExc = new System.Exception("Failed to add the field ThumbNail." + ex.Message, ex);
                    throw myExc;
                }
                return innerRasterBuilder.AuxiliaryFields; // Return the updated fields.
            }
            set
            {
                innerRasterBuilder.AuxiliaryFields = value;
            }
        }

        /// <summary>
        /// Prepare the Raster Type for generating Crawler items
        /// </summary>
        /// <param name="pCrawler">Crawler to use to generate the crawler items</param>
        public void BeginConstruction(IDataSourceCrawler pCrawler)
        {
            innerRasterBuilder.BeginConstruction(pCrawler);
        }

        /// <summary>
        /// Call the build function of the inner Raster Type and generate a thumbnail from the Builder Item created.
        /// </summary>
        /// <param name="pItemURI">URI of the Item to be built</param>
        /// <returns></returns>
        public IBuilderItem Build(IItemURI pItemURI)
        {
            IBuilderItem pbuilderItem = innerRasterBuilder.Build(pItemURI); // Generate the IBuilderItem
            Resampling(pbuilderItem); // Generate the Thumbnail from the item.
            return pbuilderItem;
        }

        /// <summary>
        /// Construct a Unique Resource Identifier (URI)
        /// for each crawler item
        /// </summary>
        /// <param name="crawlerItem">Crawled Item from which the URI is generated</param>
        public void ConstructURIs(object crawlerItem)
        {
            innerRasterBuilder.ConstructURIs(crawlerItem);
        }

        /// <summary>
        /// Finish Construction of the URI's
        /// </summary>
        /// <returns></returns>
        public IItemURIArray EndConstruction()
        {
            return innerRasterBuilder.EndConstruction();
        }

        /// <summary>
        /// Generate the next URI.
        /// </summary>
        /// <returns>The URI generated.</returns>
        public IItemURI GetNextURI()
        {
            return innerRasterBuilder.GetNextURI();
        }

        /// <summary>
        /// Get the recommended data crawler for the Raster Type based on 
        /// properties provided by the user.
        /// </summary>
        /// <param name="pDataSourceProperties">List of properties provided by the user</param>
        /// <returns></returns>
        public IDataSourceCrawler GetRecommendedCrawler(IPropertySet pDataSourceProperties)
        {
            return innerRasterBuilder.GetRecommendedCrawler(pDataSourceProperties);
        }

        /// <summary>
        /// Check if the item provided is "stale" or not valid
        /// </summary>
        /// <param name="pItemURI">URI for the item to be checked</param>
        /// <returns></returns>
        public bool IsStale(IItemURI pItemURI)
        {
            return innerRasterBuilder.IsStale(pItemURI);
        }

        /// <summary>
        /// Properties associated with the Raster Type
        /// </summary>
        public IPropertySet Properties
        {
            get
            {
                return innerRasterBuilder.Properties;
            }
            set
            {
                innerRasterBuilder.Properties = value;
            }
        }


        #endregion

        #region IPersistVariant Members
        /// <summary>
        /// UID for the object implementing the Persist Variant
        /// </summary>
        UID IPersistVariant.ID
        {
            get { return myUID; }
        }

        /// <summary>
        /// Load the object from the stream provided
        /// </summary>
        /// <param name="Stream">Stream that represents the serialized Raster Type</param>
        void IPersistVariant.Load(IVariantStream Stream)
        {
            string name = (string)Stream.Read();
            //if (innerRasterBuilder is IPersistVariant)
            //    ((IPersistVariant)innerRasterBuilder).Load(Stream);
            innerRasterBuilder = (IRasterBuilder)Stream.Read(); // Load the innerRasterBuilder from the stream.
        }

        /// <summary>
        /// Same the Raster Type to the stream provided
        /// </summary>
        /// <param name="Stream">Stream to serialize the Raster Type into</param>
        void IPersistVariant.Save(IVariantStream Stream)
        {
            Stream.Write("ThmbnailBuilder");
            //if (innerRasterBuilder is IPersistVariant)
            //    ((IPersistVariant)innerRasterBuilder).Save(Stream);
            Stream.Write(innerRasterBuilder); // Save the innerRasterBuilder into the stream.
        }
        #endregion

        #region IRasterBuilderInit Members
        /// <summary>
        /// Default spatial reference for the MD.
        /// </summary>
        ISpatialReference IRasterBuilderInit.DefaultSpatialReference
        {
            get
            {
                return ((IRasterBuilderInit)innerRasterBuilder).DefaultSpatialReference;
            }
            set
            {
                ((IRasterBuilderInit)innerRasterBuilder).DefaultSpatialReference = value;
            }
        }

        /// <summary>
        /// Parent mosaic dataset for the Raster Builder.
        /// </summary>
        IMosaicDataset IRasterBuilderInit.MosaicDataset
        {
            get
            {
                return ((IRasterBuilderInit)innerRasterBuilder).MosaicDataset;
            }
            set
            {
                ((IRasterBuilderInit)innerRasterBuilder).MosaicDataset = value;
            }
        }

        /// <summary>
        /// The raster type operation helper object associated with this raster type.
        /// </summary>
        IRasterTypeOperation IRasterBuilderInit.RasterTypeOperation
        {
            get
            {
                return ((IRasterBuilderInit)innerRasterBuilder).RasterTypeOperation;
            }
            set
            {
                ((IRasterBuilderInit)innerRasterBuilder).RasterTypeOperation = value;
            }
        }

        /// <summary>
        /// Tracker for when cancel is pressed.
        /// </summary>
        ITrackCancel IRasterBuilderInit.TrackCancel
        {
            get
            {
                return ((IRasterBuilderInit)innerRasterBuilder).TrackCancel;
            }
            set
            {
                ((IRasterBuilderInit)innerRasterBuilder).TrackCancel = value;
            }
        }
        #endregion
    }
}



