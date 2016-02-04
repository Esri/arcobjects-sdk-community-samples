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
using System.Drawing;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.esriSystem;

namespace TriangleElement
{
  [ComVisible(true)]
  [Guid("DC8482C9-5DD6-44dc-BF3C-54B18AB813C9")]
  public interface ITriangleElement
  {
    ISimpleFillSymbol FillSymbol { get; set;}
    double Size { get; set;}
    double Angle { get; set;}
  }

  [Guid(TriangleElementClass.CLASSGUID)]
  [ClassInterface(ClassInterfaceType.None)]
  [ProgId("TriangleElement.TriangleElementClass")]
  public sealed class TriangleElementClass : ITriangleElement,
                                             IElement, 
                                             IElementProperties, 
                                             IElementProperties2, 
                                             IElementProperties3,
                                             IBoundsProperties, 
                                             ITransform2D, 
                                             IGraphicElement, 
                                             IPersistVariant,
                                             IClone,
                                             IDocumentVersionSupportGEN
  {
    #region class members

    //some win32 imports and constants
    [System.Runtime.InteropServices.DllImport("gdi32", EntryPoint = "GetDeviceCaps", ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Ansi, SetLastError = true)]
    public static extern int GetDeviceCaps(int hDC, int nIndex);
    private const double          c_Cosine30          = 0.866025403784439;
    private const double          c_Deg2Rad           = (Math.PI / 180.0);
    private const double          c_Rad2Deg           = (180.0 / Math.PI);
    private const int             c_Version           = 2;
    public const string           CLASSGUID           = "cbf943e2-ce6d-49f4-a4a7-ce16f02379ad";
    public const int              LOGPIXELSX          = 88;
    public const int              LOGPIXELSY          = 90;

    private IPolygon              m_triangle          = null;
    private IPoint                m_pointGeometry     = null;
    private ISimpleFillSymbol     m_fillSymbol        = null;
    private double                m_rotation          = 0.0;
    private double                m_size              = 20.0;
    private ISelectionTracker     m_selectionTracker  = null;
    private IDisplay              m_cachedDisplay     = null;
    private ISpatialReference     m_nativeSR          = null;
    private string                m_elementName       = string.Empty;
    private string                m_elementType       = "TriangleElement";
    private object                m_customProperty    = null;
    private bool                  m_autoTrans         = true;
    private double                m_scaleRef          = 0.0;
    private esriAnchorPointEnum   m_anchorPointType   = esriAnchorPointEnum.esriCenterPoint;
    private double                m_dDeviceRatio      = 0;
    #endregion

    #region class constructor
    public TriangleElementClass()
    {
      //initialize the element's geometry
      m_triangle = new PolygonClass();
      m_triangle.SetEmpty();

      InitMembers();
    }
    #endregion

    #region ITriangleElement Members

    public ISimpleFillSymbol FillSymbol
    {
      get
      {
        return m_fillSymbol;
      }
      set
      {
        m_fillSymbol = value;
      }
    }

    public double Size
    {
      get
      {
        return m_size;
      }
      set
      {
        m_size = value;
      }
    }

    public double Angle
    {
      get
      {
        return m_rotation;
      }
      set
      {
        m_rotation = value;
      }
    }

    #endregion

    #region IElement Members

    public void Activate(IDisplay Display)
    {
      //cache the display
      m_cachedDisplay = Display;

      SetupDeviceRatio(Display.hDC, Display);

      //need to calculate the points of the triangle polygon
      if(m_triangle.IsEmpty)
        BuildTriangleGeometry(m_pointGeometry);

      //need to refresh the element's tracker
      RefreshTracker();
    }

    public void Deactivate()
    {
      m_cachedDisplay = null;
    }

    public void Draw(IDisplay Display, ITrackCancel TrackCancel)
    {      
      if (null != m_triangle && null != m_fillSymbol)
      {
        Display.SetSymbol((ISymbol)m_fillSymbol);
        Display.DrawPolygon(m_triangle);
      }
    }

    public IGeometry Geometry
    {
      get
      {
        return Clone(m_pointGeometry) as IGeometry;
      }
      set
      {
        try
        {
          m_pointGeometry = Clone(value) as IPoint;

          UpdateElementSpatialRef();
        }
        catch (Exception ex)
        {
          System.Diagnostics.Trace.WriteLine(ex.Message);
        }
      }
    }

    public bool HitTest(double x, double y, double Tolerance)
    {
      if (null == m_cachedDisplay)
        return false;

      IPoint point = new PointClass();
      point.PutCoords(x,y);

      return ((IRelationalOperator)m_triangle).Contains((IGeometry)point);
    }

    public bool Locked
    {
      get
      {
        return false;
      }
      set
      {
        
      }
    }
    
    public void QueryBounds(IDisplay Display, IEnvelope Bounds)
    {
      //return a bounding envelope
      IPolygon polygon = new PolygonClass();
      polygon.SetEmpty();

      ((ISymbol)m_fillSymbol).QueryBoundary(Display.hDC, Display.DisplayTransformation, m_triangle, polygon);

      Bounds.XMin = polygon.Envelope.XMin;
      Bounds.XMax = polygon.Envelope.XMax;
      Bounds.YMin = polygon.Envelope.YMin;
      Bounds.YMax = polygon.Envelope.YMax;
      Bounds.SpatialReference = polygon.Envelope.SpatialReference;
    }

    public void QueryOutline(IDisplay Display, IPolygon Outline)
    {
      //return a polygon which is the outline of the element
      IPolygon polygon = new PolygonClass();
      polygon.SetEmpty();
      ((ISymbol)m_fillSymbol).QueryBoundary(Display.hDC, Display.DisplayTransformation, m_triangle, polygon);
      ((IPointCollection)Outline).AddPointCollection((IPointCollection)polygon);
    }

    public ISelectionTracker SelectionTracker
    {
      get { return m_selectionTracker; }
    }

    #endregion

    #region IElementProperties Members

    /// <summary>
    /// Indicates if transform is applied to symbols and other parts of element.
    /// False = only apply transform to geometry.
    /// Update font size in ITransform2D routines
    /// </summary>
    public bool AutoTransform
    {
      get
      {
        return m_autoTrans;
      }
      set
      {
        m_autoTrans = value;
      }
    }

    public object CustomProperty
    {
      get
      {
        return m_customProperty;
      }
      set
      {
        m_customProperty = value;
      }
    }

    public string Name
    {
      get
      {
        return m_elementName;
      }
      set
      {
        m_elementName = value;
      }
    }

    public string Type
    {
      get
      {
        return m_elementType;
      }
      set
      {
        m_elementType = value;
      }
    }

    #endregion

    #region IElementProperties2 Members


    public bool CanRotate()
    {
      return true;
    }

    public double ReferenceScale
    {
      get
      {
        return m_scaleRef;
      }
      set
      {
        m_scaleRef = value;
      }
    }

    #endregion

    #region IElementProperties3 Members

    public esriAnchorPointEnum AnchorPoint
    {
      get
      {
        return m_anchorPointType;
      }
      set
      {
        m_anchorPointType = value;
      }
    }

    #endregion

    #region IBoundsProperties Members

    public bool FixedAspectRatio
    {
      get
      {
        return true;
      }
      set
      {
        throw new Exception("The method or operation is not implemented.");
      }
    }

    public bool FixedSize
    {
      get { return true; }
    }

    #endregion

    #region ITransform2D Members

    public void Move(double dx, double dy)
    {
      if (null == m_triangle)
        return;

      ((ITransform2D)m_triangle).Move(dx, dy);
      ((ITransform2D)m_pointGeometry).Move(dx, dy);
      
      RefreshTracker();
    }

    public void MoveVector(ILine v)
    {
      if (null == m_triangle)
        return;

      ((ITransform2D)m_triangle).MoveVector(v);
      ((ITransform2D)m_pointGeometry).MoveVector(v);

      RefreshTracker();
    }

    public void Rotate(IPoint Origin, double rotationAngle)
    {
      if (null == m_triangle)
        return;

      ((ITransform2D)m_triangle).Rotate(Origin, rotationAngle);
      ((ITransform2D)m_pointGeometry).Rotate(Origin, rotationAngle);

      m_rotation = rotationAngle * c_Rad2Deg;

      RefreshTracker();
    }

    public void Scale(IPoint Origin, double sx, double sy)
    {
      if (null == m_triangle)
        return;

      ((ITransform2D)m_triangle).Scale(Origin, sx, sy);
      ((ITransform2D)m_pointGeometry).Scale(Origin, sx, sy);

      if (m_autoTrans)
      {
        m_size *= Math.Max(sx, sy);
      }

      RefreshTracker();
    }

    public void Transform(esriTransformDirection direction, ITransformation transformation)
    {
      if (null == m_triangle)
        return;

      //Geometry
      ((ITransform2D)m_triangle).Transform(direction, transformation);

      IAffineTransformation2D affineTrans = (IAffineTransformation2D)transformation;
      if (affineTrans.YScale != 1.0)
        m_size *= Math.Max(affineTrans.YScale, affineTrans.XScale);

      RefreshTracker();
    }

    #endregion

    #region IGraphicElement Members

    public ISpatialReference SpatialReference
    {
      get
      {
        return m_nativeSR;
      }
      set
      {
        m_nativeSR = value;
        UpdateElementSpatialRef();
      }
    }

    #endregion

    #region IPersistVariant Members

    public UID ID
    {
      get
      {
        UID uid = new UIDClass();
        uid.Value = "{" + TriangleElementClass.CLASSGUID + "}";
        return uid;
      }
    }

    public void Load(IVariantStream Stream)
    {
      int ver = (int)Stream.Read();
      if (ver > c_Version || ver <= 0)
        throw new Exception("Wrong version!");

      InitMembers();

      m_size = (double)Stream.Read();
      m_scaleRef = (double)Stream.Read();
      m_anchorPointType = (esriAnchorPointEnum)Stream.Read();
      m_autoTrans = (bool)Stream.Read();
      m_elementType = (string)Stream.Read();
      m_elementName = (string)Stream.Read();
      m_nativeSR = Stream.Read() as ISpatialReference;
      m_fillSymbol = Stream.Read() as ISimpleFillSymbol;
      m_pointGeometry = Stream.Read() as IPoint;
      m_triangle = Stream.Read() as IPolygon;

      if (ver == 2)
      {
        m_rotation = (double)Stream.Read();
      }
    }

    public void Save(IVariantStream Stream)
    {
      Stream.Write(c_Version);

      Stream.Write(m_size);
      Stream.Write(m_scaleRef);
      Stream.Write(m_anchorPointType);
      Stream.Write(m_autoTrans);
      Stream.Write(m_elementType);
      Stream.Write(m_elementName);
      Stream.Write(m_nativeSR);
      Stream.Write(m_fillSymbol);
      Stream.Write(m_pointGeometry);
      Stream.Write(m_triangle);

      Stream.Write(m_rotation);
    }

    #endregion

    #region IClone Members

    public void Assign(IClone src)
    {

      //1. make sure that src is pointing to a valid object
      if (null == src)
      {
        throw new COMException("Invalid object.");
      }

      //2. make sure that the type of src is of type 'TriangleElementClass'
      if (!(src is TriangleElementClass))
      {
        throw new COMException("Bad object type.");
      }

      //3. assign the properties of src to the current instance
      TriangleElementClass srcTriangle = (TriangleElementClass)src;
      m_elementName = srcTriangle.Name;
      m_elementType = srcTriangle.Type;
      m_autoTrans = srcTriangle.AutoTransform;
      m_scaleRef = srcTriangle.ReferenceScale;
      m_rotation = srcTriangle.Angle;
      m_size = srcTriangle.Size;
      m_anchorPointType = srcTriangle.AnchorPoint;

      IObjectCopy objCopy = new ObjectCopyClass();

      //take care of the custom property
      if (null != srcTriangle.CustomProperty)
      {
        if (srcTriangle.CustomProperty is IClone)
          m_customProperty = (object)((IClone)srcTriangle.CustomProperty).Clone();
        else if (srcTriangle.CustomProperty is IPersistStream)
        {
          m_customProperty = objCopy.Copy((object)srcTriangle.CustomProperty);
        }
        else if (srcTriangle.CustomProperty.GetType().IsSerializable)
        {
          //serialize to a memory stream
          MemoryStream memoryStream = new MemoryStream();
          BinaryFormatter binaryFormatter = new BinaryFormatter();
          binaryFormatter.Serialize(memoryStream, srcTriangle.CustomProperty);
          byte[] bytes = memoryStream.ToArray();

          memoryStream = new MemoryStream(bytes);
          m_customProperty = binaryFormatter.Deserialize(memoryStream);
        }
      }

      if (null != srcTriangle.SpatialReference)
        m_nativeSR = objCopy.Copy(srcTriangle.SpatialReference) as ISpatialReference;
      else
        m_nativeSR = null;

      if (null != srcTriangle.FillSymbol)
      {
        m_fillSymbol = objCopy.Copy(srcTriangle.FillSymbol) as ISimpleFillSymbol;
      }
      else
        m_fillSymbol = null;

      if (null != srcTriangle.Geometry)
      {
        m_triangle = objCopy.Copy(srcTriangle.Geometry) as IPolygon;
        m_pointGeometry = objCopy.Copy(((IArea)m_triangle).Centroid) as IPoint;
      }
      else
      {
        m_triangle = null;
        m_pointGeometry = null;
      }
    }

    public IClone Clone()
    {
      TriangleElementClass triangle = new TriangleElementClass();
      triangle.Assign((IClone)this);

      return (IClone)triangle;
    }

    public bool IsEqual(IClone other)
    {
      //1. make sure that the 'other' object is pointing to a valid object
      if (null == other)
        throw new COMException("Invalid object.");

      //2. verify the type of 'other'
      if (!(other is TriangleElementClass))
        throw new COMException("Bad object type.");

      TriangleElementClass otherTriangle = (TriangleElementClass)other;
      //test that all of the object's properties are the same.
      //please note the usage of IsEqual when using ArcObjects components that
      //supports cloning
      if (otherTriangle.Name == m_elementName &&
          otherTriangle.Type == m_elementType &&
          otherTriangle.AutoTransform == m_autoTrans &&
          otherTriangle.ReferenceScale == m_scaleRef &&
          otherTriangle.Angle == m_rotation &&
          otherTriangle.Size == m_size &&
          otherTriangle.AnchorPoint == m_anchorPointType &&
          ((IClone)otherTriangle.Geometry).IsEqual((IClone)m_triangle) &&
          ((IClone)otherTriangle.FillSymbol).IsEqual((IClone)m_fillSymbol) &&
          ((IClone)otherTriangle.SpatialReference).IsEqual((IClone)m_nativeSR))
        return true;

      return false;
    }

    public bool IsIdentical(IClone other)
    {
      //1. make sure that the 'other' object is pointing to a valid object
      if (null == other)
        throw new COMException("Invalid object.");

      //2. verify the type of 'other'
      if (!(other is TriangleElementClass))
        throw new COMException("Bad object type.");

      //3. test if the other is the 'this'
      if ((TriangleElementClass)other == this)
        return true;

      return false;
    }

    #endregion

    #region IDocumentVersionSupportGEN Members

    public object ConvertToSupportedObject(esriArcGISVersion docVersion)
    {
      //in case of 8.3, create a character marker element and use a triangle marker...
      ICharacterMarkerSymbol charMarkerSymbol = new CharacterMarkerSymbolClass();
      charMarkerSymbol.Color = m_fillSymbol.Color;
      charMarkerSymbol.Angle = m_rotation;
      charMarkerSymbol.Size = m_size;
      charMarkerSymbol.Font = ESRI.ArcGIS.ADF.Connection.Local.Converter.ToStdFont(new Font("ESRI Default Marker", (float)m_size, FontStyle.Regular));
      charMarkerSymbol.CharacterIndex = 184;

      IMarkerElement markerElement = new MarkerElementClass();
      markerElement.Symbol = (IMarkerSymbol)charMarkerSymbol;

      IPoint point = ((IClone)m_pointGeometry).Clone() as IPoint;
      IElement element = (IElement)markerElement;
      element.Geometry = (IGeometry)point;

      return element;
    }

    public bool IsSupportedAtVersion(esriArcGISVersion docVersion)
    {
      //support all versions except 8.3
      if (esriArcGISVersion.esriArcGISVersion83 == docVersion)
        return false;
      else
        return true;
    }

    #endregion

    #region private methods
    private IClone Clone(object obj)
    {
      if (null == obj || !(obj is IClone))
        return null;

      return ((IClone)obj).Clone();
    }

    private int TwipsPerPixelX()
    {
      return 16;
    }

    private int TwipsPerPixelY()
    {
      return 16;
    }

    private void SetupDeviceRatio(int hDC, ESRI.ArcGIS.Display.IDisplay display)
    {
      if (display.DisplayTransformation != null)
      {
        if (display.DisplayTransformation.Resolution != 0)
        {
          m_dDeviceRatio = display.DisplayTransformation.Resolution / 72;
          //  Check the ReferenceScale of the display transformation. If not zero, we need to
          //  adjust the Size, XOffset and YOffset of the Symbol we hold internally before drawing.
          if (display.DisplayTransformation.ReferenceScale != 0)
            m_dDeviceRatio = m_dDeviceRatio * display.DisplayTransformation.ReferenceScale / display.DisplayTransformation.ScaleRatio;
        }
      }
      else
      {
        // If we don't have a display transformation, calculate the resolution
        // from the actual device.
        if (display.hDC != 0)
        {
          // Get the resolution from the device context hDC.
          m_dDeviceRatio = System.Convert.ToDouble(GetDeviceCaps(hDC, LOGPIXELSX)) / 72;
        }
        else
        {
          // If invalid hDC assume we're drawing to the screen.
          m_dDeviceRatio = 1 / (TwipsPerPixelX() / 20); // 1 Point = 20 Twips.
        }
      }
    }

    private double PointsToMap(IDisplayTransformation displayTransform, double dPointSize)
    {
      double tempPointsToMap = 0;
      if (displayTransform == null)
        tempPointsToMap = dPointSize * m_dDeviceRatio;
      else
      {
        tempPointsToMap = displayTransform.FromPoints(dPointSize);
      }
      return tempPointsToMap;
    }

    private void BuildTriangleGeometry(IPoint pointGeometry)
    {
      try
      {
        if (null == m_triangle || null == pointGeometry || null == m_cachedDisplay)
          return;

        m_triangle.SpatialReference = pointGeometry.SpatialReference;
        m_triangle.SetEmpty();

        object missing = System.Reflection.Missing.Value;
        IPointCollection pointCollection = (IPointCollection)m_triangle;

        double radius = PointsToMap(m_cachedDisplay.DisplayTransformation, m_size);

        double X = pointGeometry.X;
        double Y = pointGeometry.Y;

        IPoint point = new PointClass();
        point.X = X + radius * c_Cosine30;
        point.Y = Y - 0.5 * radius;
        pointCollection.AddPoint(point, ref missing, ref missing);

        point = new PointClass();
        point.X = X;
        point.Y = Y + radius;
        pointCollection.AddPoint(point, ref missing, ref missing);

        point = new PointClass();
        point.X = X - radius * c_Cosine30;
        point.Y = Y - 0.5 * radius;
        pointCollection.AddPoint(point, ref missing, ref missing);

        m_triangle.Close();

        if (m_rotation != 0.0)
        {
          ((ITransform2D)pointCollection).Rotate(pointGeometry, m_rotation * c_Deg2Rad);
        }

        return;
      }
      catch (Exception ex)
      {
        System.Diagnostics.Trace.WriteLine(ex.Message);
      }
    }

    private void SetDefaultDymbol()
    {
      IColor color = (IColor)ESRI.ArcGIS.ADF.Connection.Local.Converter.ToRGBColor(Color.Black);
      ISimpleLineSymbol lineSymbol = new SimpleLineSymbolClass();
      lineSymbol.Style = esriSimpleLineStyle.esriSLSSolid;
      lineSymbol.Width = 1.0;
      lineSymbol.Color = color;

      color = (IColor)ESRI.ArcGIS.ADF.Connection.Local.Converter.ToRGBColor(Color.Navy);
      if (null == m_fillSymbol)
        m_fillSymbol = new SimpleFillSymbolClass();
      m_fillSymbol.Color = color;
      m_fillSymbol.Style = esriSimpleFillStyle.esriSFSSolid;
      m_fillSymbol.Outline = (ILineSymbol)lineSymbol;
    }


    /// <summary>
    /// assign the triangle's geometry to the selection tracker
    /// </summary>
    private void RefreshTracker()
    {
      if (null == m_cachedDisplay)
        return;

      m_selectionTracker.Display = (IScreenDisplay)m_cachedDisplay;

      
      IPolygon outline = new PolygonClass(); 
      this.QueryOutline(m_cachedDisplay, outline);

      m_selectionTracker.Geometry = (IGeometry)outline;
    }

    private void UpdateElementSpatialRef()
    {
      if (null == m_cachedDisplay ||
          null == m_nativeSR ||
          null == m_triangle ||
          null == m_cachedDisplay.DisplayTransformation.SpatialReference)
        return;

      if (null == m_triangle.SpatialReference)
        m_triangle.SpatialReference = m_cachedDisplay.DisplayTransformation.SpatialReference;

      m_triangle.Project(m_nativeSR);

      RefreshTracker();
    }

    private void InitMembers()
    {
      //initialize the selection tracker
      m_selectionTracker = new PolygonTrackerClass();
      m_selectionTracker.Locked = false;
      m_selectionTracker.ShowHandles = true;

      //set a default symbol
      SetDefaultDymbol();
    }
    #endregion

  }
}
