/*

   Copyright 2019 Esri

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
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using Microsoft.Win32;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS;


namespace DynamicLogo
{
  /// <summary>
  /// Command that works in ArcMap/Map/PageLayout
  /// </summary>
  [Guid("638eb76e-0b28-4538-92ba-89cebf4e1acb")]
  [ClassInterface(ClassInterfaceType.None)]
  [ProgId("DynamicLogo.DynamicLogo")]
  public sealed class DynamicLogo : BaseCommand
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

    private IHookHelper m_hookHelper = null;
    private string m_logoPath = string.Empty;
    private ISymbol m_logoSymbol = null;
    public bool m_bOnce = true;
    private IDynamicGlyph m_logoGlyph = null;
    private IDynamicGlyphFactory m_dynamicGlyphFactory = null;
    private IDynamicSymbolProperties m_dynamicSymbolProps = null;
    private IDynamicDrawScreen m_dynamicDrawScreen = null;
    private IPoint m_point;
    private bool m_bIsOn = false;
    private IActiveViewEvents_Event avEvents;


    public DynamicLogo()
    {
      //
      // TODO: Define values for the public properties
      //
      base.m_category = ".NET Samples"; //localizable text
      base.m_caption = "Show Logo";  //localizable text 
      base.m_message = "Show or hide the logo";  //localizable text
      base.m_toolTip = "Show or hide the logo";  //localizable text
      base.m_name = "DynamicLogo_ShowDynamicLogo";   //unique id, non-localizable (e.g. "MyCategory_MyCommand")

      try
      {
        string bitmapResourceName = GetType().Name + ".bmp";
        base.m_bitmap = new Bitmap(GetType(), bitmapResourceName);
      }
      catch (Exception ex)
      {
        System.Diagnostics.Trace.WriteLine(ex.Message, "Invalid Bitmap");
      }
    }

    #region Overriden Class Methods

    /// <summary>
    /// Occurs when this command is created
    /// </summary>
    /// <param name="hook">Instance of the application</param>
    public override void OnCreate(object hook)
    {
      if (hook == null)
        return;

      try
      {
        m_hookHelper = new HookHelperClass();
        m_hookHelper.Hook = hook;
        if (m_hookHelper.ActiveView == null)
          m_hookHelper = null;
      }
      catch
      {
        m_hookHelper = null;
      }

      if (m_hookHelper == null)
        base.m_enabled = false;
      else
        base.m_enabled = true;


    }

    /// <summary>
    /// Occurs when this command is clicked
    /// </summary>
    public override void OnClick()
    {
      m_logoPath = GetLogoPath();
      IMap map = m_hookHelper.FocusMap;
      IDynamicMap dynamicMap = map as IDynamicMap;

      IActiveView activeView = map as IActiveView;
      /*IActiveViewEvents_Event */avEvents = activeView as IActiveViewEvents_Event;
      IDynamicMapEvents_Event dynamicMapEvents = dynamicMap as IDynamicMapEvents_Event;
      IScreenDisplay screenDisplay = activeView.ScreenDisplay;

      if (!m_bIsOn)
      {

        avEvents.AfterDraw += new IActiveViewEvents_AfterDrawEventHandler(avEvents_AfterDraw);
        dynamicMapEvents.AfterDynamicDraw += new IDynamicMapEvents_AfterDynamicDrawEventHandler(dynamicMapEvents_AfterDynamicDraw);

      }
      else
      {
        dynamicMapEvents.AfterDynamicDraw -= new IDynamicMapEvents_AfterDynamicDrawEventHandler(dynamicMapEvents_AfterDynamicDraw);
        avEvents.AfterDraw -= new IActiveViewEvents_AfterDrawEventHandler(avEvents_AfterDraw);
      }
      m_bIsOn = !m_bIsOn;
      screenDisplay.Invalidate(null, true, (short)esriScreenCache.esriNoScreenCache);
      screenDisplay.UpdateWindow();
    }

    public override bool Checked
    {
      get
      {
        return m_bIsOn;
      }
    }

    void dynamicMapEvents_AfterDynamicDraw(esriDynamicMapDrawPhase DynamicMapDrawPhase, IDisplay Display, IDynamicDisplay dynamicDisplay)
    {
      if (DynamicMapDrawPhase != esriDynamicMapDrawPhase.esriDMDPDynamicLayers)
        return;
      DrawDynamicLogo(dynamicDisplay);


    }

    private void DrawDynamicLogo(IDynamicDisplay dynamicDisplay)
    {
      if (m_bOnce)
      {
        //cast the DynamicDisplay into DynamicGlyphFactory
        m_dynamicGlyphFactory = dynamicDisplay.DynamicGlyphFactory;
        //cast the DynamicDisplay into DynamicSymbolProperties
        m_dynamicSymbolProps = dynamicDisplay as IDynamicSymbolProperties;

        m_dynamicDrawScreen = dynamicDisplay as IDynamicDrawScreen;

        //create the dynamic glyph for the logo
        m_logoGlyph = m_dynamicGlyphFactory.CreateDynamicGlyphFromFile(esriDynamicGlyphType.esriDGlyphMarker, m_logoPath, ESRI.ArcGIS.ADF.Connection.Local.Converter.ToRGBColor(Color.White));

        m_point = new PointClass();
        m_point.PutCoords(120, 160);
        m_bOnce = false;
      }

      m_dynamicSymbolProps.set_DynamicGlyph(esriDynamicSymbolType.esriDSymbolMarker, m_logoGlyph);
      m_dynamicSymbolProps.SetScale(esriDynamicSymbolType.esriDSymbolMarker, .435f, .435f);
      m_dynamicDrawScreen.DrawScreenMarker(m_point);
    }

    void avEvents_AfterDraw(ESRI.ArcGIS.Display.IDisplay Display, esriViewDrawPhase phase)
    {
      if (phase != esriViewDrawPhase.esriViewForeground)
        return;
      DrawLogoStandard(Display);
    }

    private void DrawLogoStandard(IDisplay Display)
    {
      tagRECT r = Display.DisplayTransformation.get_DeviceFrame();
      Display.StartDrawing(Display.hDC, (short)esriScreenCache.esriNoScreenCache);
      if (null == m_logoSymbol)
      {
        m_logoSymbol = CreateStandardLogoSymbol();
      }
      Display.SetSymbol(m_logoSymbol);
      Display.DrawPoint(Display.DisplayTransformation.ToMapPoint(120, r.bottom - 160));
      Display.FinishDrawing();
    }

    #endregion

    private string GetLogoPath()
    {
      //set the path of the logo
        //relative file path to the sample data from project location
        string sLogoPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        sLogoPath = System.IO.Path.Combine(sLogoPath, @"ArcGIS\data\ESRILogo\ESRI_LOGO.bmp");
        var filePath = new DirectoryInfo(sLogoPath);
        System.Diagnostics.Debug.WriteLine(string.Format("File path for data root: {0} [{1}]", filePath.FullName, Directory.GetCurrentDirectory()));

        if (!System.IO.File.Exists(sLogoPath))
      {
        MessageBox.Show(string.Format("File path for logo does not exist: {0} [{1}] please correct in sample code or copy the logo to the specified location", filePath.FullName, Directory.GetCurrentDirectory()));
        return string.Empty;
      }
      return sLogoPath;
    }

    private ISymbol CreateStandardLogoSymbol()
    {
      IPictureMarkerSymbol pictureMarkerSymbol = new PictureMarkerSymbolClass();
      pictureMarkerSymbol.CreateMarkerSymbolFromFile(esriIPictureType.esriIPictureBitmap, m_logoPath);
      pictureMarkerSymbol.Size = 100;
      IColor whiteTransparencyColor = ESRI.ArcGIS.ADF.Connection.Local.Converter.ToRGBColor(Color.FromArgb(255, 255, 255)) as IColor;
      pictureMarkerSymbol.BitmapTransparencyColor = whiteTransparencyColor;

      return pictureMarkerSymbol as ISymbol;
    }
  }
}
