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
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.ArcMapUI;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;

namespace SelectionCOMSample
{
  /// <summary>
  /// Summary description for SelectByLineTool.
  /// </summary>
  [Guid("15de72ff-f31f-4655-98b6-191b7348375a")]
  [ClassInterface(ClassInterfaceType.None)]
  [ProgId("SelectionCOMSample.SelectByLineTool")]
  public sealed class SelectByLineTool : BaseTool
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
      MxCommands.Register(regKey);

    }
    /// <summary>
    /// Required method for ArcGIS Component Category unregistration -
    /// Do not modify the contents of this method with the code editor.
    /// </summary>
    private static void ArcGISCategoryUnregistration(Type registerType)
    {
      string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
      MxCommands.Unregister(regKey);

    }

    #endregion
    #endregion

    private bool m_isMouseDown = false;
    private ESRI.ArcGIS.Display.INewLineFeedback m_lineFeedback;
    private SelectionExtension m_mainExtension;
    private IMxDocument m_doc;

    public SelectByLineTool()
    {
      base.m_category = "Developer Samples"; 
      base.m_caption = "Select ByLine Tool C#.";  
      base.m_message = "Select by line tool C#.";
      base.m_toolTip = "Select by line tool C#.\r\nSelection Sample Extension needs to be turned on in Extensions dialog.";  
      base.m_name = "ESRI_SelectionCOMSample_SelectByLineTool";   
      try
      {
        base.m_bitmap = new Bitmap(GetType().Assembly.GetManifestResourceStream("SelectionCOMSample.Images.SelectByLine.png"));
        base.m_cursor = new System.Windows.Forms.Cursor(GetType().Assembly.GetManifestResourceStream("SelectionCOMSample.Images.SelectByLine.cur"));
      }
      catch (Exception ex)
      {
        System.Diagnostics.Trace.WriteLine(ex.Message, "Invalid Bitmap");
      }
      m_mainExtension = SelectionExtension.GetExtension();
    }

    #region Overriden Class Methods

    /// <summary>
    /// Occurs when this tool is created
    /// </summary>
    /// <param name="hook">Instance of the application</param>
    public override void OnCreate(object hook)
    {
      IApplication application = hook as IApplication;
      m_doc = application.Document as IMxDocument;

      //Disable if it is not ArcMap
      if (hook is IMxApplication)
        base.m_enabled = true;
      else
        base.m_enabled = false;
    }

    /// <summary>
    /// Occurs when this tool is clicked
    /// </summary>
    public override void OnClick()
    {
    }

    public override void OnMouseDown(int Button, int Shift, int X, int Y)
    {
      IActiveView activeView = m_doc.FocusMap as IActiveView;
      IPoint point = activeView.ScreenDisplay.DisplayTransformation.ToMapPoint(X, Y) as IPoint;

      if (m_lineFeedback == null)
      {
        m_lineFeedback = new ESRI.ArcGIS.Display.NewLineFeedback();
        m_lineFeedback.Display = activeView.ScreenDisplay;
        m_lineFeedback.Start(point);
      }
      else
      {
        m_lineFeedback.AddPoint(point);
      }

      m_isMouseDown = true;
    }

    public override void OnMouseMove(int Button, int Shift, int X, int Y)
    {
      if (!m_isMouseDown) return;

      IActiveView activeView = m_doc.FocusMap as IActiveView;

      IPoint point = activeView.ScreenDisplay.DisplayTransformation.ToMapPoint(X, Y) as IPoint;
      m_lineFeedback.MoveTo(point);
    }


    public override void OnDblClick()
    {
      IActiveView activeView = m_doc.FocusMap as IActiveView;

      activeView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, null);

      IPolyline polyline;

      if (m_lineFeedback != null)
      {
        polyline = m_lineFeedback.Stop();
        if (polyline != null)
          m_doc.FocusMap.SelectByShape(polyline, null, false);
      }


      activeView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, null);

      m_lineFeedback = null;
      m_isMouseDown = false;
    }

    public override bool Enabled
    {
      get
      {
        if (m_mainExtension == null)
          return false;
        else
          return m_mainExtension.HasSelectableLayer() && m_mainExtension.IsExtensionEnabled;
      }
    }
    #endregion
  }
}
