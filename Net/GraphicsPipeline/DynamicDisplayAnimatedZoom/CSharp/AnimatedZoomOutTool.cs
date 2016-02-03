using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using System.Windows.Forms;

namespace DynamicDisplayAnimatedZoom
{
  /// <summary>
  /// Summary description for AnimatedZoomOutTool.
  /// </summary>
  [Guid("64b7e531-5e4f-4d48-83ef-2b07668509ba")]
  [ClassInterface(ClassInterfaceType.None)]
  [ProgId("DynamicDisplayAnimatedZoom.AnimatedZoomOutTool")]
  public sealed class AnimatedZoomOutTool : BaseTool
  {
    #region COM Registration Function(s)
    [ComRegisterFunction()]
    [ComVisible(false)]
    static void RegisterFunction(Type registerType)
    {
      // Required for ArcGIS Component Category Registrar support
      ArcGISCategoryRegistration(registerType);
    }

    [ComUnregisterFunction()]
    [ComVisible(false)]
    static void UnregisterFunction(Type registerType)
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
      ControlsCommands.Register(regKey);
    }
    /// <summary>
    /// Required method for ArcGIS Component Category unregistration -
    /// Do not modify the contents of this method with the code editor.
    /// </summary>
    private static void ArcGISCategoryUnregistration(Type registerType)
    {
      string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
      ControlsCommands.Unregister(regKey);
    }

    #endregion
    #endregion

    #region class members
    private IHookHelper m_hookHelper = null;

    private bool m_bIsAnimating = false;
    private bool m_bZoomIn = false;
    private double m_dStepCount = 0;
    private int m_nTotalSteps = 0;

    private IPoint m_Center = new PointClass();

    private WKSEnvelope m_wksStep = new WKSEnvelope();

    private IDynamicMapEvents_Event m_dynamicMapEvents = null;

    private const double c_dMinimumDelta = 0.01;
    private const double c_dSmoothFactor = 200000.0;
    private const double c_dMinimumSmoothZoom = 0.1;
    #endregion

    public AnimatedZoomOutTool()
    {
      base.m_category = ".NET Samples";
      base.m_caption = "Animated Zoom out";
      base.m_message = "Zoom out with animation";
      base.m_toolTip = "Animated Zoom out";
      base.m_name = "DynamicDisplayAnimatedZoom_AnimatedZoomOutTool";
      try
      {
        string bitmapResourceName = GetType().Name + ".bmp";
        base.m_bitmap = new Bitmap(GetType(), bitmapResourceName);
        base.m_cursor = new System.Windows.Forms.Cursor(GetType(), GetType().Name + ".cur");
      }
      catch (Exception ex)
      {
        System.Diagnostics.Trace.WriteLine(ex.Message, "Invalid Bitmap");
      }
    }

    #region Overriden Class Methods

    /// <summary>
    /// Occurs when this tool is created
    /// </summary>
    /// <param name="hook">Instance of the application</param>
    public override void OnCreate(object hook)
    {
      if (null == hook)
        return;

      try
      {
        m_hookHelper = new HookHelperClass();
        m_hookHelper.Hook = hook;
        if (null == m_hookHelper.ActiveView)
          m_hookHelper = null;
      }
      catch
      {
        m_hookHelper = null;
      }
    }

    /// <summary>
    /// The enabled state of this command, determines whether the command is usable.
    /// </summary>
    public override bool Enabled
    {
      get
      {
        if (null == m_hookHelper)
          return false;

        IDynamicMap dynamicMap = m_hookHelper.FocusMap as IDynamicMap;
        bool bIsDynamicMapEnabled = dynamicMap.DynamicMapEnabled;
        if (false == bIsDynamicMapEnabled)
        {
          m_bIsAnimating = false;
          m_dStepCount = 0;
          m_nTotalSteps = 0;
          m_dynamicMapEvents = null;
        }
        return bIsDynamicMapEnabled;
      }
    }

    /// <summary>
    /// Occurs when this tool is clicked
    /// </summary>
    public override void OnClick()
    {
      IDynamicMap dynamicMap = m_hookHelper.FocusMap as IDynamicMap;
      if (false == dynamicMap.DynamicMapEnabled)
        return;

      m_dynamicMapEvents = null;
      m_dynamicMapEvents = m_hookHelper.FocusMap as IDynamicMapEvents_Event;
      m_dynamicMapEvents.DynamicMapStarted += new IDynamicMapEvents_DynamicMapStartedEventHandler(DynamicMapEvents_DynamicMapStarted);

      m_bIsAnimating = false;
      m_dStepCount = 0;
      m_nTotalSteps = 0;
    }

    public override void OnMouseDown(int Button, int Shift, int X, int Y)
    {
      // Zoom on the focus map based on user drawn rectangle
      m_bZoomIn = Shift == 1;

      IActiveView activeView = m_hookHelper.FocusMap as IActiveView;
      IRubberBand rubberBand = new RubberEnvelopeClass();
      // This method intercepts the Mouse events from here
      IEnvelope zoomBounds = rubberBand.TrackNew(activeView.ScreenDisplay, null) as IEnvelope;
      if (null == zoomBounds)
        return;

      WKSEnvelope wksZoomBounds;
      zoomBounds.QueryWKSCoords(out wksZoomBounds);

      IEnvelope fittedBounds = activeView.ScreenDisplay.DisplayTransformation.FittedBounds;
      WKSEnvelope wksFittedBounds;
      fittedBounds.QueryWKSCoords(out wksFittedBounds);

      if (false == m_bZoomIn)
      {
        double dXScale = fittedBounds.Width  * fittedBounds.Width  / zoomBounds.Width;
        double dYScale = fittedBounds.Height * fittedBounds.Height / zoomBounds.Height;

        wksZoomBounds.XMin = fittedBounds.XMin - dXScale;
        wksZoomBounds.YMin = fittedBounds.YMin - dYScale;
        wksZoomBounds.XMax = fittedBounds.XMax + dXScale;
        wksZoomBounds.YMax = fittedBounds.YMax + dYScale;
      }

      m_wksStep.XMax = 1;
      m_wksStep.YMax = 1;
      m_wksStep.XMin = 1;
      m_wksStep.YMin = 1;
      m_nTotalSteps = 0;

      // Calculate how fast the zoom will go by changing the step size
      while ((System.Math.Abs(m_wksStep.XMax) > c_dMinimumDelta) ||
             (System.Math.Abs(m_wksStep.YMax) > c_dMinimumDelta) ||
             (System.Math.Abs(m_wksStep.XMin) > c_dMinimumDelta) ||
             (System.Math.Abs(m_wksStep.YMin) > c_dMinimumDelta))
      {
        m_nTotalSteps++;

        // calculate the step size
        // step size is the difference between the zoom bounds and the fitted bounds
        m_wksStep.XMin = (wksZoomBounds.XMin - wksFittedBounds.XMin) / m_nTotalSteps;
        m_wksStep.YMin = (wksZoomBounds.YMin - wksFittedBounds.YMin) / m_nTotalSteps;
        m_wksStep.XMax = (wksZoomBounds.XMax - wksFittedBounds.XMax) / m_nTotalSteps;
        m_wksStep.YMax = (wksZoomBounds.YMax - wksFittedBounds.YMax) / m_nTotalSteps;
      }

      m_bIsAnimating = true;
      m_dStepCount = 0;
    }

    public override bool Deactivate()
    {
      m_bIsAnimating = false;
      m_dStepCount = 0;
      m_nTotalSteps = 0;

      if (null == m_hookHelper)
        return false;

      IDynamicMap dynamicMap = m_hookHelper.FocusMap as IDynamicMap;
      if (false == dynamicMap.DynamicMapEnabled)
        return true;

      m_dynamicMapEvents = m_hookHelper.FocusMap as IDynamicMapEvents_Event;
      m_dynamicMapEvents.DynamicMapStarted -= new IDynamicMapEvents_DynamicMapStartedEventHandler(DynamicMapEvents_DynamicMapStarted);

      return true;
    }
    #endregion

    #region Dynamic Map Events
    void DynamicMapEvents_DynamicMapStarted(IDisplay Display, IDynamicDisplay dynamicDisplay)
    {
      if (false == m_bIsAnimating)
      {
        m_dStepCount = 0;
        m_nTotalSteps = 0;
        return;
      }

      if (m_dStepCount >= m_nTotalSteps)
      {
        m_bIsAnimating = false;
        m_dStepCount = 0;
        m_nTotalSteps = 0;
        return;
      }

      // Increase the bounds by the step amount
      IActiveView activeView = m_hookHelper.FocusMap as IActiveView;
      IEnvelope newVisibleBounds = activeView.ScreenDisplay.DisplayTransformation.FittedBounds;

      // Smooth the zooming.  Faster at higher scales, slower at lower
      double dSmoothZooom = activeView.FocusMap.MapScale / c_dSmoothFactor;
      if (dSmoothZooom < c_dMinimumSmoothZoom)
        dSmoothZooom = c_dMinimumSmoothZoom;

      newVisibleBounds.XMin = newVisibleBounds.XMin + (m_wksStep.XMin * dSmoothZooom);
      newVisibleBounds.YMin = newVisibleBounds.YMin + (m_wksStep.YMin * dSmoothZooom);
      newVisibleBounds.XMax = newVisibleBounds.XMax + (m_wksStep.XMax * dSmoothZooom);
      newVisibleBounds.YMax = newVisibleBounds.YMax + (m_wksStep.YMax * dSmoothZooom);

      activeView.ScreenDisplay.DisplayTransformation.VisibleBounds = newVisibleBounds;

      m_dStepCount = m_dStepCount + dSmoothZooom;
    }
    #endregion
  }
}
