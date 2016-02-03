using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Editor;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.esriSystem;
using System;
using System.Windows.Forms;
using Microsoft.Win32;


namespace ViperPin
{
  public partial class ViperPinForm : Form
  {
    private IEditor m_editor;
    private IPolyline m_curve;
    private IEditLayers m_editLayers;
    private IEditSketch3 m_edSketch;
    private int m_lotNum;

    public ViperPinForm(IEditor3 editor)
    {
      InitializeComponent();

      m_editor = editor;
      m_edSketch = m_editor as IEditSketch3;
      m_editLayers = m_editor as IEditLayers;

      lblEditLayer.Text = m_editLayers.CurrentLayer.Name;

      //Load field combo box with field names
      IFields pFields = m_editLayers.CurrentLayer.FeatureClass.Fields;
		  for (int i=0; i < pFields.FieldCount; i++)
      {
        cmbPINField.Items.Add(pFields.get_Field(i).Name);
      }

      //get pinfield from registry
      string pinField = null;
      RegistryKey pRegKey = Registry.CurrentUser.OpenSubKey("Software\\ESRI\\ViperPin");
      if (pRegKey != null)
      {
        pinField = pRegKey.GetValue("Pinfield").ToString();
      }

      //set the combo box to the pinfield
      for (int i = 0; i < pFields.FieldCount; i++)
      {
        if (pinField == pFields.get_Field(i).Name)
        {
          cmbPINField.Text = pinField;
          break;
        }
        else
        {
          cmbPINField.Text = "None";
        }
      }

      //cmbPINField.SelectedIndex = 0;
      cmbPINField.Refresh();
      m_lotNum = 1;
      txtlot.Text = "1";

      //Set center right of form to center right of screen
      this.StartPosition = FormStartPosition.Manual;
      this.Left = 0;
      this.Top = (Screen.PrimaryScreen.Bounds.Height / 2) - (this.Height / 2);
    }

    private void cmdOK_Click(object sender, EventArgs e)
    {
      m_lotNum = int.Parse(txtlot.Text);

      //Set pin value
      SetPINValue();

      //save pinfield
      RegistryKey regKey = Registry.CurrentUser.OpenSubKey("Software",true);
      RegistryKey newKey = regKey.CreateSubKey("ESRI\\ViperPin");
      newKey.SetValue("Pinfield", cmbPINField.Text);

      this.Hide();
      
      //redraw labels
      //m_editor.Display.Invalidate(null, true, (short)esriViewDrawPhase.esriViewGraphics);

      IMxDocument mxDoc;
      mxDoc = m_editor.Parent.Document as IMxDocument;
      mxDoc.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
    }

    private void cmdCancel_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void SetPINValue()
    {
      //The Theory.
      //Select polygons that intersect the sketch.
      //Construct one polyline from the boundaries and intersect with sketch.
      //Sort resulting intersection locations (multipoint) by distance of the intersect
      // from the start of the sketch and create new ordered multipoint.
      //Loop through new ordered multipoint, select underlying parcel and calc pin.

      IFeatureLayer featLayer = m_editLayers.CurrentLayer;
      m_curve = m_edSketch.Geometry as IPolyline;

      //Search parcel polys by graphic to get feature cursor
      ISpatialFilter spatialFilter = new SpatialFilter();
      spatialFilter.Geometry = m_curve;
      spatialFilter.GeometryField = m_editLayers.CurrentLayer.FeatureClass.ShapeFieldName;
      spatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelCrosses;

      IFeatureCursor featCursor = featLayer.Search(spatialFilter,true);
      IFeature feature = featCursor.NextFeature();

      //If we have no intersects then exit
      if (feature == null)
        return;
  
      //Make a GeomBag of the polygons boundaries (polylines)
      IGeometryCollection geomBag = new GeometryBagClass();
      object missing = Type.Missing;
      while (feature != null)
      {
        ITopologicalOperator poly = feature.Shape as ITopologicalOperator;
        geomBag.AddGeometry(poly.Boundary,ref missing,ref missing);
        feature = featCursor.NextFeature();
      }

      //Make one polyline from the boundaries
      IPolyline polyLineU = new PolylineClass();
      ITopologicalOperator topoOp = polyLineU as ITopologicalOperator;
      topoOp.ConstructUnion(geomBag as IEnumGeometry);

      //Get the intersections of the boundaries and the curve
      IPointCollection pointCol = topoOp.Intersect(m_curve, esriGeometryDimension.esriGeometry0Dimension) as IPointCollection;

      //The point collection is not ordered by distance along the curve so
      //need to create a new collection with this info

      int[] pointOrder = new int[pointCol.PointCount];
      double dac = 0, dfc = 0;
      bool bRS = false;

      for (int i = 0; i < pointCol.PointCount; i++)
      {
        IPoint queryPoint = new PointClass();
        pointCol.QueryPoint(i, queryPoint);
        m_curve.QueryPointAndDistance(esriSegmentExtension.esriNoExtension, queryPoint, false, null,ref dac,ref dfc,ref bRS);
        pointOrder[i] = (int)dac;
      }

      //use built in bubble sort
      System.Array.Sort(pointOrder);

      //Loop through the sorted array and calc midpoint between parcel boundaries
      IPointCollection midPoints = new MultipointClass();

      for (int i = 0; i < pointOrder.Length -1; i++)
      {
        //Get the midpoint distance
        double midPointDist = (pointOrder[i] + pointOrder[i + 1]) / 2;
        
        //create a point at the distance and store in point collection
        IPoint queryPoint = new PointClass();
        m_curve.QueryPoint(esriSegmentExtension.esriNoExtension, midPointDist, false, queryPoint);
        midPoints.AddPoint(queryPoint,ref missing,ref missing);
      }

      //If ends of sketch are included then add them as points
      if (chkEnds.Checked)
      {
        object before = 0 as object;
        midPoints.AddPoint(m_curve.FromPoint, ref before, ref missing);
        midPoints.AddPoint(m_curve.ToPoint, ref missing, ref missing);
      }

      m_editor.StartOperation();

      //Loop through calculated midpoints, select polygon and calc pin
      for (int i = 0; i < midPoints.PointCount; i++)
      {
        IPoint midPoint = midPoints.get_Point(i);
        spatialFilter = new SpatialFilter();
        spatialFilter.Geometry = midPoint;
        spatialFilter.GeometryField = m_editLayers.CurrentLayer.FeatureClass.ShapeFieldName;
        spatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelWithin;

        featCursor = featLayer.Search(spatialFilter, false);
        while ((feature = featCursor.NextFeature()) != null)
        {
          feature.set_Value(feature.Fields.FindField(cmbPINField.Text), m_lotNum);
          feature.Store();
          m_lotNum += int.Parse(txtlotinc.Text);
        }
      }
      m_editor.StopOperation("ViperPIN");
      txtlot.Text = m_lotNum.ToString();
    }
  }
}
