using System;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Editor;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace PointsAlongLine
{
  public partial class PointsAlongLineForm : Form
  {
    private IEditor3 m_editor;
    private IEditSketch3 m_edSketch;
    private IPolyline m_polyline;
    
    IEditTemplate m_editTemplate;
    IFeatureLayer m_featureLayer;
    IFeatureClass m_featureClass;

    public PointsAlongLineForm(IEditor3 editor)
    {
      InitializeComponent();

      m_editor = editor;
      m_edSketch = m_editor as IEditSketch3;
      
      m_polyline = m_edSketch.Geometry as IPolyline;
      tbLineLength.Text = (m_polyline.Length.ToString("F"));

      //get the template
      m_editTemplate = m_editor.CurrentTemplate;
      m_featureLayer = m_editTemplate.Layer as IFeatureLayer;
      m_featureClass = m_featureLayer.FeatureClass;

    }

    private void cmdCancel_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void cmdOK_Click(object sender, EventArgs e)
    {
      //calc distance between points
      double dbp = 0;
      if (rbNOP.Checked)
        dbp = m_polyline.Length / (int.Parse(txtNOP.Text) + 1);
      else
        dbp = int.Parse(txtDist.Text);
      
      m_editor.StartOperation();
      this.Cursor = Cursors.WaitCursor;
      
      //create points at distance between points up to total length
      for (double d = dbp; d < m_polyline.Length; d += dbp)
      {
        IConstructPoint contructionPoint = new PointClass();
        contructionPoint.ConstructAlong(m_polyline, esriSegmentExtension.esriNoExtension,d,false);
        CreatePoint(contructionPoint as IPoint);
      }
      
      //create points at start and end of sketch
      if (chkEnds.Checked)
      {
        CreatePoint(m_polyline.FromPoint);
        CreatePoint(m_polyline.ToPoint);
       }
      
      this.Cursor = Cursors.Default;
      m_editor.StopOperation("Create points along a line");
      this.Close();
     }

  private void CreatePoint(IPoint point)
  {
    //create point for the current template
    IFeature feature = m_featureClass.CreateFeature();
    feature.Shape = point;
    m_editTemplate.SetDefaultValues(feature);
    feature.Store();

    //Invalidate the area around the new feature
    m_editor.Display.Invalidate(feature.Extent, true, (short)esriScreenCache.esriAllScreenCaches);
  }

  }
}
