using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace TabbedFeatureInspector
{
  [Guid(TabbedInspectorCLSID)]
  [ClassInterface(ClassInterfaceType.None)]
  [ProgId("TabbedFeatureInspector.TabbedInspectorCS")]
  public partial class TabbedInspector : UserControl, IEngineObjectInspector, IClassExtension, IFeatureClassExtension
  {
    public const string TabbedInspectorCLSID = "65a43962-8cc0-49c0-bfa3-015d0ff8350e";

    #region COM Registration Function(s)

    [ComRegisterFunction]
    [ComVisible(false)]
    private static void RegisterFunction(Type registerType)
    {
      // Required for ArcGIS Component Category Registrar support
      ArcGISCategoryRegistration(registerType);
    }

    [ComUnregisterFunction]
    [ComVisible(false)]
    private static void UnregisterFunction(Type registerType)
    {
      // Required for ArcGIS Component Category Registrar support
      ArcGISCategoryUnregistration(registerType);
    }

    #region ArcGIS Component Category Registrar generated code

    /// <summary>
    /// Required method for ArcGIS Component Category registration -
    /// Do not modify the contents of this method with the code editor.
    /// </summary>
    private static void ArcGISCategoryRegistration(Type registerType)
    {
      string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
      GeoObjectClassExtensions.Register(regKey);
    }

    /// <summary>
    /// Required method for ArcGIS Component Category unregistration -
    /// Do not modify the contents of this method with the code editor.
    /// </summary>
    private static void ArcGISCategoryUnregistration(Type registerType)
    {
      string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
      GeoObjectClassExtensions.Unregister(regKey);
    }

    #endregion

    #endregion

    private const short SW_SHOW = 5;
    public static string title = "Custom Feature Inspector Properties:";
    private IEngineObjectInspector m_inspector;

    public TabbedInspector()
    {
      InitializeComponent();
      if (m_inspector == null)
      {
        m_inspector = new EngineFeatureInspector();
      }
    }

    #region "IEngineObjectInspector Implementations"

    /// <summary>
    /// Returns the handle for the picture box.
    /// </summary>
    public int picHwnd
    {
      get { return defaultPictureBox.Handle.ToInt32(); }
    }

    /// <summary>
    /// Returns the handle property of the tab page that holds the default inspector.
    /// </summary>
    public int stabHwnd
    {
      get { return standardTabPage.Handle.ToInt32(); }
    }

    /// <summary>
    /// Clear the inspector before inspecting another object.
    /// </summary>
    public void Clear()
    {
      m_inspector.Clear();
      customListBox.Items.Clear();
    }

    /// <summary>
    /// Copies the values from srcRow to the row being edited.
    /// </summary>
    /// <param name="srcRow"></param>
    public void Copy(IRow srcRow)
    {
      m_inspector.Copy(srcRow);
    }

    /// <summary>
    /// The window handle for the inspector.
    /// </summary>
    public int hWnd
    {
      get { return Handle.ToInt32(); }
    }

    /// <summary>
    /// Inspects the properties of the features.
    /// </summary>
    /// <param name="objects"></param>
    /// <param name="Editor"></param>
    public void Inspect(IEngineEnumRow objects, IEngineEditor Editor)
    {
      try
      {
        SetParent(m_inspector.hWnd, stabHwnd);
        SetParent(stabHwnd, picHwnd);

        ShowWindow(m_inspector.hWnd, SW_SHOW);
        m_inspector.Inspect(objects, Editor);

        IEngineEnumRow enumRow = objects;
        IRow row = enumRow.Next();
        IFeature inspFeature = (IFeature) row;

        //user selected the layer name instead of a feature.
        if (objects.Count > 1)
          return;

        switch (inspFeature.Shape.GeometryType)
        {
          case esriGeometryType.esriGeometryPolygon:
            {
              //do this for polygons.
              customListBox.Items.Clear();
              ReportPolygons(inspFeature);
              break;
            }
          case esriGeometryType.esriGeometryPolyline:
            {
              //do this for lines.
              customListBox.Items.Clear();
              ReportPolylines(inspFeature);
              break;
            }
          case esriGeometryType.esriGeometryPoint:
            {
              //do this for points.
              customListBox.Items.Clear();
              ReportPoints(inspFeature);
              break;
            }
          default:
            break;
        } //End switch.
      }
      catch (Exception ex)
      {
        MessageBox.Show("IObjectInspector_Inspect: " + ex.Message);
      }
    }

    #endregion

    #region IClassExtension Implementations

    /// <summary>
    /// Initializes the extension, passing in a reference to its class helper.
    /// </summary>
    /// <param name="pClassHelper"></param>
    /// <param name="pExtensionProperties"></param>
    public void Init(IClassHelper pClassHelper, IPropertySet pExtensionProperties)
    {
     }

    /// <summary>
    /// Informs the extension that its class helper is going away.
    /// </summary>
    public void Shutdown()
    {
      m_inspector = null;
    }

    #endregion

    [DllImport("user32.dll", CharSet = CharSet.Ansi)]
    private static extern int SetParent(int hWndChild, int hWndNewParent);

    [DllImport("user32.dll", CharSet = CharSet.Ansi)]
    private static extern int ShowWindow(int hWnd, int nCmdShow);

    /// <summary>
    /// Reports the area, perimeter, and number of vertices for the selected polygon
    /// </summary>
    /// <param name="inspFeature"></param>
    private void ReportPolygons(IFeature inspFeature)
    {
      try
      {
        IPolygon shpPolygon = (IPolygon) inspFeature.Shape;
        IArea polyArea = (IArea) shpPolygon;
        ICurve polyCurve = shpPolygon;
        IPointCollection polyPoints = (IPointCollection) shpPolygon;

        customListBox.BeginUpdate();
        customListBox.Items.Add(title);
        customListBox.Items.Add("");
        customListBox.Items.Add("FID:" + inspFeature.OID);

        //Report Area First
        customListBox.Items.Add("AREA :       " + Convert.ToString(polyArea.Area));

        //Then Perimeter
        customListBox.Items.Add("PERIMETER:   " + Convert.ToString(polyCurve.Length));

        //Number of vertices
        //polyPoints = shpPolygon
        customListBox.Items.Add("VERTICES:   " + Convert.ToString(polyPoints.PointCount));

        // Determine the width of the items in the list to get the best column width setting.
        int width =
          (int)
          customListBox.CreateGraphics().MeasureString(customListBox.Items[customListBox.Items.Count - 1].ToString(),
                                                       customListBox.Font).Width;

        // Set the column width based on the width of each item in the list.
        customListBox.ColumnWidth = width;

        customListBox.EndUpdate();
      }
      catch (FormatException ex)
      {
        MessageBox.Show("ReportPolygons: " + ex.Message);
      }
    }

    /// <summary>
    /// Reports length, FromPoint-x, FromPoint-y, ToPoint-x, ToPoint-x of selected polylines.
    /// </summary>
    /// <param name="inspFeature"></param>
    private void ReportPolylines(IFeature inspFeature)
    {
      ICurve lCurve = (ICurve) inspFeature.Shape;

      customListBox.BeginUpdate();

      customListBox.Items.Add(title);
      customListBox.Items.Add("");
      customListBox.Items.Add("FID:                    " + Convert.ToString(inspFeature.OID));

      //Report Length First
      customListBox.Items.Add("LENGTH:                " + Convert.ToString(lCurve.Length));

      //Report Start Point next
      customListBox.Items.Add("FROMPOINT-X:    " + Convert.ToString(lCurve.FromPoint.X));

      //Report End Point 
      customListBox.Items.Add("FROMPOINT-Y:    " + Convert.ToString(lCurve.FromPoint.Y));

      //Report End Point last
      customListBox.Items.Add("TOPOINT-X:        " + Convert.ToString(lCurve.ToPoint.X));

      customListBox.Items.Add("TOPOINT-Y:        " + Convert.ToString(lCurve.ToPoint.Y));

      // Determine the width of the items in the list to get the best column width setting.
      int width =
        (int)
        customListBox.CreateGraphics().MeasureString(customListBox.Items[customListBox.Items.Count - 1].ToString(),
                                                     customListBox.Font).Width;
      // Set the column width based on the width of each item in the list.
      customListBox.ColumnWidth = width;
      customListBox.EndUpdate();
    }

    /// <summary>
    /// Reports the coordinates of the selected point features.
    /// </summary>
    /// <param name="inspFeature"></param>
    private void ReportPoints(IFeature inspFeature)
    {
      IPoint shpPt = (IPoint) inspFeature.Shape;

      customListBox.BeginUpdate();
      customListBox.Items.Add(title);
      customListBox.Items.Add("");
      customListBox.Items.Add("FID:       " + inspFeature.OID);

      //Report X and Y coordinate locations
      customListBox.Items.Add("X-COORD :   " + Convert.ToString(shpPt.X));
      customListBox.Items.Add("Y-COORD :   " + Convert.ToString(shpPt.Y));
      // Determine the width of the items in the list to get the best column width setting.
      int width =
        (int)
        customListBox.CreateGraphics().MeasureString(customListBox.Items[customListBox.Items.Count - 1].ToString(),
                                                     customListBox.Font).Width;
      // Set the column width based on the width of each item in the list.
      customListBox.ColumnWidth = width;

      customListBox.EndUpdate();
    }
  }
}