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
using System.ComponentModel;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Editor;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace TabbedFeatureInspector
{
  [Guid("65a43962-8cc0-49c0-bfa3-015d0ff8350e")]
  [ClassInterface(ClassInterfaceType.None)]
  [ProgId("TabbedFeatureInspector.TabbedInspectorCS")]

  public partial class TabbedInspector : UserControl, ESRI.ArcGIS.Editor.IObjectInspector, ESRI.ArcGIS.Geodatabase.IClassExtension, ESRI.ArcGIS.Geodatabase.IFeatureClassExtension
  {
    #region COM Registration Function(s)
    [ComRegisterFunction()]
    [ComVisible(false)]
    static void RegisterFunction(Type registerType)
    {
      // Required for ArcGIS Component Category Registrar support
      ArcGISCategoryRegistration(registerType);

      //
      // TODO: Add any COM registration code here
      //
    }

    [ComUnregisterFunction()]
    [ComVisible(false)]
    static void UnregisterFunction(Type registerType)
    {
      // Required for ArcGIS Component Category Registrar support
      ArcGISCategoryUnregistration(registerType);

      //
      // TODO: Add any COM unregistration code here
      //
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

    IObjectInspector m_inspector;
    private IClass m_classHelper;
   
    [DllImport("user32.dll", CharSet=CharSet.Ansi)]
    private static extern int SetParent(int hWndChild, int hWndNewParent);

    [DllImport("user32.dll", CharSet = CharSet.Ansi)]
    private static extern int ShowWindow(int hWnd, int nCmdShow);
  
    private const short SW_SHOW = 5;
    private const short SW_HIDE = 0;

    public TabbedInspector()
    {
      InitializeComponent();
      if (m_inspector == null)
      {
        //this is the default inspector shipped with the editor
        m_inspector = new FeatureInspector();
      }
    }

    #region "IObjectInspector Implementations"
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
    public void Copy(ESRI.ArcGIS.Geodatabase.IRow srcRow)
    {
      m_inspector.Copy(srcRow);
    }
    /// <summary>
    /// The window handle for the inspector.
    /// </summary>
    public int HWND 
    {
      get
      {    
        
        return this.Handle.ToInt32();
      }    
    }
    /// <summary>
    /// Returns the handle for the picture box.
    /// </summary>
    public int picHwnd
    {
      get
      {
        return this.defaultPictureBox.Handle.ToInt32();
      }
    }
    /// <summary>
    /// Returns the handle property of the tab page that holds the default inspector.
    /// </summary>
    public int stabHwnd
    {
      get
      {
        return this.standardTabPage.Handle.ToInt32();
      }
    }
    /// <summary>
    /// Inspects the properties of the features.
    /// </summary>
    /// <param name="objects"></param>
    /// <param name="Editor"></param>
    public void Inspect(ESRI.ArcGIS.Editor.IEnumRow objects, ESRI.ArcGIS.Editor.IEditor Editor)
    {
      try
      {
        SetParent(m_inspector.HWND, stabHwnd);
        SetParent(stabHwnd, picHwnd);

        ShowWindow(m_inspector.HWND, SW_SHOW);
        m_inspector.Inspect(objects, Editor);

        IEnumRow enumRow = objects;
        IRow row = enumRow.Next();
        IFeature inspFeature = (IFeature)row;

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
        }     //End switch.

    }
    catch (Exception ex)
    {
      MessageBox.Show("IObjectInspector_Inspect: " + ex.Message);
    }
    }     
   
  #endregion

  #region "IClassExtension Implementations"
    /// <summary>
    /// Initializes the extension, passing in a reference to its class helper.
    /// </summary>
    /// <param name="pClassHelper"></param>
    /// <param name="pExtensionProperties"></param>
    public void Init(ESRI.ArcGIS.Geodatabase.IClassHelper pClassHelper, ESRI.ArcGIS.esriSystem.IPropertySet pExtensionProperties)
    {
      m_classHelper = pClassHelper.Class;
    }
    /// <summary>
    /// Informs the extension that its class helper is going away.
    /// </summary>
    public void Shutdown()
    {
      m_inspector = null;
      m_classHelper = null;
    }

 #endregion
    public static string title = "Custom Feature Inspector Properties:";
    /// <summary>
    /// Reports the area, perimeter, and number of vertices for the selected polygons.
    /// </summary>
    /// <param name="inspFeature"></param>
    private void ReportPolygons(IFeature inspFeature)
    {
      try
      {

        IPolygon shpPolygon = (IPolygon)inspFeature.Shape;
        IArea polyArea = (IArea)shpPolygon;
        ICurve polyCurve = shpPolygon;
        IPointCollection polyPoints = (IPointCollection)shpPolygon;

        customListBox.BeginUpdate();
        customListBox.Items.Add(title);
        customListBox.Items.Add("");
        customListBox.Items.Add("FID:" + inspFeature.OID);

      //Report Area First
        customListBox.Items.Add("Area :       " + Convert.ToString(polyArea.Area));

      //Then Perimeter
        customListBox.Items.Add("Perimeter:   " + Convert.ToString(polyCurve.Length));

      //Number of vertices
      //polyPoints = shpPolygon
        customListBox.Items.Add("VERTICES:   " + Convert.ToString(polyPoints.PointCount));

        // Determine the width of the items in the list to get the best column width setting.
        int width = (int)customListBox.CreateGraphics().MeasureString(customListBox.Items[customListBox.Items.Count - 1].ToString(),
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
      ICurve lCurve = (ICurve)inspFeature.Shape;

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
      int width = (int)customListBox.CreateGraphics().MeasureString(customListBox.Items[customListBox.Items.Count - 1].ToString(),
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
       IPoint shpPt = (IPoint)inspFeature.Shape;

       customListBox.BeginUpdate();
       customListBox.Items.Add(title);
       customListBox.Items.Add("");
       customListBox.Items.Add("FID:       " + inspFeature.OID);

      //Report X and Y coordinate locations
      customListBox.Items.Add("X-COORD :   " + Convert.ToString(shpPt.X));
      customListBox.Items.Add("Y-COORD :   " + Convert.ToString(shpPt.Y));
      // Determine the width of the items in the list to get the best column width setting.
      int width = (int)customListBox.CreateGraphics().MeasureString(customListBox.Items[customListBox.Items.Count - 1].ToString(),
         customListBox.Font).Width;
      // Set the column width based on the width of each item in the list.
      customListBox.ColumnWidth = width;

      customListBox.EndUpdate();
    }
  }
}
