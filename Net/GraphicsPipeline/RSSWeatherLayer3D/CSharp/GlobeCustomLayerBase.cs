using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.GlobeCore;


namespace RSSWeatherLayer3D
{

  /// <summary>
  /// An abstract base class for implementation of a custom globe layer
  /// </summary>
  public abstract class GlobeCustomLayerBase : Control,
                                               ILayer,
                                               IGeoDataset,
                                               ILayerGeneralProperties,
                                               IPersistVariant,
                                               IPersistStream,
                                               ILayerExtensions,
                                               ILayerInfo,
                                               ILegendInfo,
                                               ILayerDrawingProperties,
                                               ICustomGlobeLayer,
                                               IEnumerable,
                                               IDisposable
  {
    #region class members
    /// <summary>
    /// a win32 delete object	reference
    /// </summary>
    /// <param name="hObject"></param>
    /// <returns></returns>
    [DllImport("gdi32")]
    internal static extern int DeleteObject(IntPtr hObject);

    /// <summary>
    /// Keep the layer's extent. Returned by the ILayer::Extent property
    /// </summary>
    /// <remarks>The extent should be spatial-referenced to the DateFrame's spatial reference.
    /// </remarks>
    protected IEnvelope m_extent = null;

    /// <summary>
    /// Store the layer's underlying data spatial reference. Returned by IGeoDataset::SpatialReference.
    /// </summary>
    /// <remarks>This spatial reference should not be reprojected. In your inheriting 
    /// class you will need to have another parameter that will keep the DataFrame's spatial reference
    /// that would use to reproject the geometries and the extent of the layer.</remarks>
    protected ISpatialReference m_spRef = null;

    /// <summary>
    /// The legend group to store the legend class required by the TOC for the layer's renderer.
    /// Returned by ILegendGroup::get_LegendGroup property.
    /// </summary>
    protected ILegendGroup m_legendGroup = null;

    /// <summary>
    /// Layer's name. Returned by ILayer::Name property
    /// </summary>
    protected string m_sName = "GlobeCustomLayer";

    /// <summary>
    /// Flag which determines whether the layers is visible. Returned by ILayer::Visible
    /// </summary>
    /// <remarks>You should use this member in your inherited class in the Draw method.</remarks>
    protected bool m_bVisible = true;

    /// <summary>
    /// determines whether the layers is cached. Returned by ILayer::Cached.
    /// </summary>
    protected bool m_bCached = false;

    /// <summary>
    /// Flag which determines whether the layer is valid (connected to its data source, has valid information etc.).
    /// Returned by ILAyer::Valid.
    /// </summary>
    /// <remarks>You can use this flag to determine for example whether the layer can be available or not.</remarks>
    protected bool m_bValid = true;

    /// <summary>
    /// Indicates if the layer drawing properties are dirty.
    /// </summary>
    protected bool m_bDrawDirty = false;

    /// <summary>
    /// Keep the maximum scale value at which the layer will display
    /// </summary>
    protected double m_dblMaxScale;

    /// <summary>
    /// Keep the minimum scale value at which the layer will display
    /// </summary>
    protected double m_dblMinScale;

    /// <summary>
    /// determines whether the layers is supposed to show its MapTips
    /// </summary>
    protected bool m_ShowTips = false;

    /// <summary>
    /// The bitmap which represent the small image of the layer.
    /// </summary>
    /// <remarks>Shown in ArcCatalog or in the identify dialog</remarks>
    protected System.Drawing.Bitmap m_smallBitmap = null;

    /// <summary>
    /// The bitmap which represent the large image of the layer. 
    /// </summary>
    /// <remarks>This image will be shown in ArcCatalog when in Thumbnail view</remarks>
    protected System.Drawing.Bitmap m_largeBitmap = null;

    /// <summary>
    /// Handle for the small bitmap
    /// </summary>
    protected IntPtr m_hSmallBitmap = IntPtr.Zero;

    /// <summary>
    /// Handle for the large bitmap
    /// </summary>
    protected IntPtr m_hLargeBitmap = IntPtr.Zero;

    /// <summary>
    /// An arraylist to store the layer's extensions.
    /// </summary>
    protected ArrayList m_extensions = null;

    /// <summary>
    /// The data structure for the custom layer
    /// </summary>
    /// <remarks>By default, the DataTable gets initialized in the BaseClass Ctor with an ID column
    /// which is AutoIncremented. The BaseClass also support enumerator which is actually en enumerator of
    /// the table as well as a direct indexer.</remarks>
    protected DataTable m_table = null;
    #endregion

    #region constructor
    /// <summary>
    /// Class default constructor
    /// </summary>
    public GlobeCustomLayerBase()
    {
      m_sName = string.Empty;
      m_bVisible = true;
      m_bValid = true;
      m_bCached = false;
      m_dblMaxScale = 0;
      m_dblMinScale = 0;
      m_table = new DataTable("RECORDS");

      m_table.Columns.Add("ID", typeof(long));

      m_extensions = new ArrayList();

      ILegendClass legendClass = new LegendClassClass();
      legendClass.Label = "GlobeCustomLayer";

      m_legendGroup = new LegendGroupClass();
      m_legendGroup.Heading = "";
      m_legendGroup.Editable = false;
      m_legendGroup.AddClass(legendClass);

      m_sName = "GlobeCustomLayer";

      //call CreateControl in order to create the handle
      this.CreateControl();
    }
    #endregion

    #region IGeoDataset Members

    /// <summary>
    ///The layers geodataset extent which is a union of the extents of all
    /// the items of the layer
    /// </summary>
    /// <remarks>In your inheriting class, consider the following code to calculate the layer's extent:
    /// <code>
    /// public override IEnvelope Extent
    ///{
    ///  get
    ///  {
    ///    m_extent  = GetLayerExtent();
    ///    if (null == m_extent )
    ///      return null;
    ///
    ///    IEnvelope env = ((IClone)m_extent ).Clone() as IEnvelope;
    /// 
    ///    return env;
    ///  }
    ///}
    ///    private IEnvelope GetLayerExtent()
    ///{
    ///  if (null == base.m_spRef)
    ///  {
    ///    base.m_spRef = CreateGeographicSpatialReference();
    ///  }
    ///
    ///  IEnvelope env = new EnvelopeClass();
    ///  env.SpatialReference = base.m_spRef;
    ///  IPoint point = new PointClass();
    ///  point.SpatialReference = m_spRef;
    ///  foreach (DataRow r in m_table.Rows)
    ///  {
    ///    point.Y = Convert.ToDouble(r[3]);
    ///    point.X = Convert.ToDouble(r[4]);
    ///
    ///    env.Union(point.Envelope);
    ///  }
    /// 
    ///  return env;
    ///} 
    /// </code>
    /// </remarks>
    virtual public IEnvelope Extent
    {
      get
      {
        return m_extent;
      }
    }

    /// <summary>
    /// The spatial reference of the underlying data.
    /// </summary>
    /// <remarks>The property must return the underlying data spatial reference and
    /// must not reproject it into the layer's spatial reference </remarks>	
    virtual public ISpatialReference SpatialReference
    {
      get
      {
        return m_spRef;
      }
    }

    #endregion

    #region ILayerGeneralProperties Members

    /// <summary>
    /// Last maximum scale setting used by layer.
    /// </summary>
    virtual public double LastMaximumScale
    {
      get
      {
        return 0;
      }
    }

    /// <summary>
    /// Last minimum scale setting used by layer.
    /// </summary>
    virtual public double LastMinimumScale
    {
      get
      {
        return 0;
      }
    }

    /// <summary>
    /// Description for the layer.
    /// </summary>
    virtual public string LayerDescription
    {
      get
      {
        return null;
      }
      set
      {

      }
    }
    #endregion

    #region IPersistVariant Members

    /// <summary>
    /// The ID of the object.
    /// </summary>
    virtual public UID ID
    {
      get
      {
        // TODO:  Add clsCustomLayer.ID getter implementation
        return null;
      }
    }

    /// <summary>
    /// Loads the object properties from the stream.
    /// </summary>
    /// <param name="Stream"></param>
    /// <remarks>The Load method must read the data from the stream in the same order the data was 
    /// written to the stream in the Save method. 
    /// Streams are sequential; you must ensure that your data is saved and loaded in the correct order, 
    /// so that the correct data is written to the correct member.
    /// </remarks>
    virtual public void Load(IVariantStream Stream)
    {
      m_sName = (string)Stream.Read();
      m_bValid = (bool)Stream.Read();
      m_bCached = (bool)Stream.Read();
      m_dblMinScale = (double)Stream.Read();
      m_dblMaxScale = (double)Stream.Read();
      m_bDrawDirty = (bool)Stream.Read();

      m_spRef = (ISpatialReference)Stream.Read();
      m_extent = (IEnvelope)Stream.Read();

      m_extensions = (ArrayList)Stream.Read();
    }

    /// <summary>
    /// Saves the object properties to the stream.
    /// </summary>
    /// <param name="Stream"></param>
    virtual public void Save(IVariantStream Stream)
    {
      Stream.Write(m_sName);
      Stream.Write(m_bValid);
      Stream.Write(m_bCached);
      Stream.Write(m_dblMinScale);
      Stream.Write(m_dblMaxScale);
      Stream.Write(m_bDrawDirty);

      Stream.Write(m_spRef);
      Stream.Write(m_extent);

      Stream.Write(m_extensions);
    }

    #endregion

    #region IPersistStream Members
    /// <summary>
    /// Returns the class identifier (CLSID) for the component object.
    /// </summary>
    /// <param name="pClassID"></param>
    public virtual void GetClassID(out Guid pClassID)
    {
      throw new Exception("The method or operation is not implemented.");
    }

    /// <summary>
    /// Return the size in bytes of the stream needed to save the object.
    /// </summary>
    /// <param name="pcbSize"></param>
    public virtual void GetSizeMax(out _ULARGE_INTEGER pcbSize)
    {
      pcbSize = new _ULARGE_INTEGER();
    }

    /// <summary>
    /// Checks the object for changes since it was last saved.
    /// </summary>
    public virtual void IsDirty()
    {

    }

    /// <summary>
    /// Initializes an object from the stream where it was previously saved.
    /// </summary>
    /// <param name="pstm"></param>
    public virtual void Load(IStream pstm)
    {

    }

    /// <summary>
    /// Saves an object into the specified stream and indicates whether the object should reset its dirty flag.
    /// </summary>
    /// <param name="pstm"></param>
    /// <param name="fClearDirty"></param>
    public virtual void Save(IStream pstm, int fClearDirty)
    {

    }

    #endregion

    #region ILayer Members

    #region Properties

    /// <summary>
    /// Indicates if the layer shows map tips.
    /// </summary>
    /// <remarks>Indicates whether or not map tips are shown for the layer. 
    /// If set to True, then map tips will be shown for the layer. 
    /// You can determine the text that will be shown via TipText. 
    ///</remarks>
    virtual public bool ShowTips
    {
      get
      {
        return m_ShowTips;
      }
      set
      {
        m_ShowTips = value;
      }
    }

    /// <summary>
    /// The default area of interest for the layer. Returns the spatial-referenced extent of the layer.
    /// </summary>
    virtual public IEnvelope AreaOfInterest
    {
      get
      {
        return m_extent;
      }
    }

    /// <summary>
    /// Indicates if the layer is currently visible.
    /// </summary>
    virtual public new bool Visible
    {
      get
      {
        return m_bVisible;
      }
      set
      {
        m_bVisible = value;
      }
    }


    /// <summary>
    /// Indicates if the layer needs its own display cache.
    /// </summary>
    /// <remarks>This property indicates whether or not the layer requires its own display cache. 
    /// If this property is True, then the Map will use a separate display cache for the layer so 
    /// that it can be refreshed independently of other layers.</remarks>
    virtual public bool Cached
    {
      get
      {
        return m_bCached;
      }
      set
      {
        m_bCached = value;
      }
    }

    /// <summary>
    /// Minimum scale (representative fraction) at which the layer will display.
    /// </summary>
    /// <remarks>Specifies the minimum scale at which the layer will be displayed. 
    /// This means that if you zoom out beyond this scale, the layer will not display. 
    /// For example, specify 1000 to have the layer not display when zoomed out beyond 1:1000.</remarks>
    virtual public double MinimumScale
    {
      get
      {
        return m_dblMinScale;
      }
      set
      {
        m_dblMinScale = value;
      }
    }

    /// <summary>
    /// Indicates if the layer is currently valid.
    /// </summary>
    /// <remarks>The valid property indicates if the layer is currently valid.
    /// Layers that reference feature classes are valid when they hold a reference to a valid feature class.
    /// The property does not however validate the integrity of the feature classes reference to the database.
    /// Therefore, in rare situations if a datasource is removed after a layer is initialized, 
    /// the layer will report itself as valid but query attempts to the data source will error due to the lack 
    /// of underlying data.</remarks>
    virtual public bool Valid
    {
      get
      {
        return m_bValid;
      }
    }

    /// <summary>
    /// The Layer name.
    /// </summary>
    virtual public new string Name
    {
      get
      {
        return m_sName;
      }
      set
      {
        m_sName = value;
      }
    }

    /// <summary>
    /// Maximum scale (representative fraction) at which the layer will display.
    /// </summary>
    /// <remarks>Specifies the maximum scale at which the layer will be displayed. 
    /// This means that if you zoom in beyond this scale, the layer will not display. 
    /// For example, specify 500 to have the layer not display when zoomed in beyond 1:500.</remarks>
    virtual public double MaximumScale
    {
      get
      {
        return m_dblMaxScale;
      }
      set
      {
        m_dblMaxScale = value;
      }
    }

    /// <summary>
    /// Supported draw phases.
    /// </summary>
    /// <remarks>Indicates the draw phases supported by the layer (esriDPGeography, esriDPAnnotation, 
    /// esriDPSelection, or any combination of the three). 
    /// The supported draw phases are defined by esriDrawPhase. 
    /// When multiple draw phases are supported, the sum of the constants is used. 
    /// For example, if SupportedDrawPhases = 3 then the layer supports drawing in the geography and annotation phases.</remarks>
    public int SupportedDrawPhases
    {
      get
      {
        return (int)esriDrawPhase.esriDPGeography;
      }
    }

    /// <summary>
    /// Spatial reference for the layer.
    /// </summary>
    ///<remarks>This property is only used for map display, setting this property does not 
    ///change the spatial reference of the layer's underlying data. 
    ///The ArcGIS framework uses this property to pass the spatial reference from the map 
    ///to the layer in order to support on-the-fly projection.</remarks> 
    ISpatialReference ESRI.ArcGIS.Carto.ILayer.SpatialReference
    {
      set
      {
        m_spRef = value;
      }
    }

    #endregion

    #region Methods
    /// <summary>
    /// Map tip text at the specified location. 
    /// </summary>
    /// <param name="X"></param>
    /// <param name="Y"></param>
    /// <param name="Tolerance"></param>
    /// <returns>The text string that gets displayed as a map tip if ShowTips = true.</returns>
    virtual public string get_TipText(double X, double Y, double Tolerance)
    {
      return null;
    }

    /// <summary>
    /// Draws the layer to the specified display for the given draw phase.
    /// </summary>
    /// <param name="drawPhase"></param>
    /// <param name="Display"></param>
    /// <param name="trackCancel"></param>
    /// <remarks>This method draws the layer to the Display for the specified DrawPhase. 
    /// Use the TrackCancel object to allow the drawing of the layer to be interrupted by the user.</remarks>
    public virtual void Draw(esriDrawPhase drawPhase, IDisplay Display, ITrackCancel trackCancel)
    {

      return;
    }
    #endregion
    #endregion

    #region ILayerExtensions Members

    /// <summary>
    /// Removes the specified extension.
    /// </summary>
    /// <param name="Index"></param>
    public virtual void RemoveExtension(int Index)
    {
      if (Index < 0 || Index > m_extensions.Count - 1)
        return;

      m_extensions.RemoveAt(Index);
    }

    /// <summary>
    /// Number of extensions.
    /// </summary>
    public virtual int ExtensionCount
    {
      get
      {
        return m_extensions.Count;
      }
    }

    /// <summary>
    /// Adds a new extension.
    /// </summary>
    /// <param name="ext"></param>
    public virtual void AddExtension(object ext)
    {
      if (null == ext)
        return;

      m_extensions.Add(ext);
    }

    /// <summary>
    /// The extension at the specified index. 
    /// </summary>
    /// <param name="Index"></param>
    /// <returns></returns>
    public virtual object get_Extension(int Index)
    {
      if (Index < 0 || Index > m_extensions.Count - 1)
        return null;

      return m_extensions[Index];
    }

    #endregion

    #region ILayerInfo Members

    /// <summary>
    /// Small image that represents the layer.
    /// </summary>
    /// <remarks>The icon used to represent the layer in ArcCatalog's 'List' and 'Details' views.</remarks>
    public virtual int SmallImage
    {
      get
      {
        return m_hSmallBitmap.ToInt32();
      }
    }

    /// <summary>
    /// Large image that represents the layer when it is selected.
    /// </summary>
    /// <remarks>The icon used to represent the layer when it is selected in ArcCatalog's 'Large Icon' view.</remarks>
    public virtual int LargeSelectedImage
    {
      get
      {
        return m_hLargeBitmap.ToInt32();
      }
    }

    /// <summary>
    /// Small image that represents the layer when it is selected.
    /// </summary>
    /// <remarks>The icon used to represent the layer when it is selected in ArcCatalog's 'List' and 'Details' views.</remarks>
    public virtual int SmallSelectedImage
    {
      get
      {
        return m_hSmallBitmap.ToInt32();
      }
    }

    /// <summary>
    /// Large image that represents the layer.
    /// </summary>
    /// <remarks>The icon used to represent the layer in ArcCatalog's 'Large Icon' view.</remarks>
    public virtual int LargeImage
    {
      get
      {
        return m_hLargeBitmap.ToInt32();
      }
    }

    #endregion

    #region ILegendInfo Members

    /// <summary>
    /// Number of legend groups contained by the object.
    /// </summary>
    /// <remarks>The number of legend groups is determined by the implementation of the renderer, 
    /// consequently this property is read only. For example, SimpleRenderer has one group, 
    /// while a BiUniqueValueRenderer has any number of groups.</remarks>
    public virtual int LegendGroupCount
    {
      get { return 1; }
    }

    /// <summary>
    /// Optional. Defines legend formatting for layer rendered with this object.
    /// </summary>
    /// <remarks>Layer or renderer legend information is further formatted for display in ArcMap legends. 
    /// A renderer can override this formatting by returning a LegendItem for this property. 
    /// ESRI renderers typically do not return anything for this property. With this configuration, 
    /// legend formatting becomes a user or developer choice on the legend object.</remarks>
    public virtual ILegendItem LegendItem
    {
      get { return null; }
    }

    /// <summary>
    /// Indicates if symbols are graduated.
    /// </summary>
    /// <remarks>For example the proportional symbol renderer returns True for this property.
    /// You can use this property to distinguish between a layer symbolized with graduated color or 
    /// graduated symbol type layer symbology. Both of these symbolizations use a ClassBreaksRenderer, 
    /// but only a graduated symbol symbolization will return True for this property.</remarks>
    public virtual bool SymbolsAreGraduated
    {
      get { return false; }
      set { }
    }

    /// <summary>
    /// Legend group at the specified index.
    /// </summary>
    /// <param name="Index"></param>
    /// <returns></returns>
    /// <remarks>The content and number of legend groups is determined by the implementation of the renderer, 
    /// consequently this property is read only.</remarks>
    public virtual ILegendGroup get_LegendGroup(int Index)
    {
      return m_legendGroup;
    }

    #endregion

    #region ILayerDrawingProperties Members

    /// <summary>
    /// Indicates if the layer drawing properties are dirty.
    /// </summary>
    public virtual bool DrawingPropsDirty
    {
      get
      {
        return m_bDrawDirty;
      }
      set
      {
        m_bDrawDirty = value;
      }
    }

    #endregion

    #region ICustomGlobeLayer Members

    /// <summary>
    /// For custom OpenGL layers, perform immediate drawing.
    /// </summary>
    /// <param name="pGlobeViewer"></param>
    /// <remarks>This is where you should add your drawings on the Globe. This method must be overridden in 
    /// your inheriting class. DrawImmediate together with IGlobeDisplayEvents::BeforeDraw and  IGlobeDisplayEvents::AfterDraw
    /// is the only safe place where the OpenGL state is ready for custom actions.</remarks>
    public abstract void DrawImmediate(IGlobeViewer pGlobeViewer);

    /// <summary>
    /// The custom draw method.
    /// </summary>
    public virtual esriGlobeCustomDrawType DrawType
    {
      get { return esriGlobeCustomDrawType.esriGlobeCustomDrawOpenGL; }
    }

    /// <summary>
    /// Gets a rasterized data tile for the given globe tesselation coordinates.
    /// </summary>
    /// <param name="tilesize"></param>
    /// <param name="face"></param>
    /// <param name="level"></param>
    /// <param name="row"></param>
    /// <param name="col"></param>
    /// <param name="pData"></param>
    public virtual void GetTile(int tilesize, int face, int level, int row, int col, out byte[] pData)
    {
      pData = null;
    }

    /// <summary>
    /// The layer is hit by a picking operation.
    /// </summary>
    /// <param name="hitObjectID"></param>
    /// <param name="pHit3D"></param>
    public virtual void Hit(int hitObjectID, ESRI.ArcGIS.Analyst3D.IHit3D pHit3D)
    {
      return;
    }

    /// <summary>
    /// For rasterized types, defines highest resolution. Zero value indicates that globe-generated default value should be used.
    /// </summary>
    public virtual double MinimumCellSize
    {
      get { return 0; }
    }

    /// <summary>
    /// The symbol scale factor for the custom rasterized type.
    /// </summary>
    public virtual double SymbologyScalingFactor
    {
      get { return 0; }
    }

    /// <summary>
    /// The option to use Globe's disk caching.
    /// </summary>
    public virtual bool UseCache
    {
      get { return false; }
    }

    /// <summary>
    /// The option to use a local coordinate system origin for high precision drawing
    /// </summary>
    /// <remarks>Use this option (set to 'true') in case where you need to draw elements which
    /// are either small and close to the globe's surface.</remarks>
    public virtual bool HandlesLocalOrigin
    {
      get { return false; }
    }

    /// <summary>
    /// The local coordinate system origin for high precision drawing
    /// </summary>
    ///<remarks>In case of setting the HandlesLocalOrigin to 'true', The point of origin passed by the Globe.</remarks>
    public virtual WKSPointZ LocalOrigin
    {
      set { }
    }

    #endregion

    #region Data Structure basic functionality
    /// <summary>
    /// Create a new item (does not add it to the layer)
    /// </summary>
    /// <returns></returns>
    virtual public DataRow NewItem()
    {
      return m_table.NewRow();
    }

    /// <summary>
    /// Add an item to the layer
    /// </summary>
    /// <param name="row"></param>
    virtual public void AddItem(DataRow row)
    {
      m_table.Rows.Add(row);
    }

    /// <summary>
    /// Add an item to the Layer
    /// </summary>
    /// <param name="values"></param>
    virtual public void AddItem(object[] values)
    {
      m_table.Rows.Add(values);
    }

    /// <summary>
    /// Query for items in the layer
    /// </summary>
    /// <param name="queryFilter">WHERE clause</param>
    /// <returns></returns>
    virtual public DataRow[] Select(string queryFilter)
    {
      return m_table.Select(queryFilter);
    }

    /// <summary>
    /// Remove all items in the layer
    /// </summary>
    virtual public void Clear()
    {
      m_table.Rows.Clear();
    }

    /// <summary>
    /// Remove the item from the layer
    /// </summary>
    /// <param name="row">The row to remove</param>
    virtual public void RemoveItem(DataRow row)
    {
      m_table.Rows.Remove(row);
    }

    //indexer. Pass an index and return a record
    /// <summary>
    /// Indexer, returns the underlying DataRow for the given index
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public DataRow this[int index]
    {
      get
      {
        return m_table.Rows[index];
      }
    }

    /// <summary>
    /// return the number of geometries in the layer
    /// </summary>
    public int NumOfRecords
    {
      get
      {
        return m_table.Rows.Count;
      }
    }
    #endregion

    #region IEnumerable Members

    /// <summary>
    /// Allow users to directly enumerate through the layer's records
    /// </summary>
    /// <returns></returns>
    public IEnumerator GetEnumerator()
    {
      return m_table.Rows.GetEnumerator();
    }

    #endregion

    #region IDisposable Members

    /// <summary>
    /// Dispose the layer
    /// </summary>
    /// <returns></returns>
    public virtual new void Dispose()
    {
      if (IntPtr.Zero != m_hSmallBitmap)
        DeleteObject(m_hSmallBitmap);

      if (IntPtr.Zero != m_hLargeBitmap)
        DeleteObject(m_hLargeBitmap);

      m_extensions.Clear();
      m_table.Dispose();

      m_extent = null;
      m_spRef = null;
      m_legendGroup = null;

      GC.Collect();
      GC.WaitForPendingFinalizers();
    }

    #endregion

  }
}

