using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.ADF.Connection.Local;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.SystemUI;
using OpenGL;

namespace DynamicDisplayHUD
{
  /// <summary>
  /// Adds a HUD (heads up display) showing the map's azimuth to the map
  /// while in dynamic mode
  /// </summary>
  [Guid("7ca05f9c-2c43-4f2b-984e-26605d46c1d5")]
  [ClassInterface(ClassInterfaceType.None)]
  [ProgId("DynamicDisplayHUD.DDHUDCmd")]
  public sealed class DDHUDCmd : BaseCommand
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

    #region class members
    private IHookHelper               m_hookHelper = null;
    private IDynamicMap               m_dynamicMap = null;
    private IDynamicGlyph             m_textGlyph = null;
    private IDynamicGlyphFactory      m_dynamicGlyphFactory = null;
    private IDynamicDrawScreen        m_dynamicDrawScreen = null;
    private IDynamicSymbolProperties  m_dynamicSymbolProps = null;
    private IPoint                    m_point = null;
    private bool                      m_bIsDynamicMode = false;
    private bool                      m_bOnce = true;
    #endregion

    #region class constructor
    public DDHUDCmd()
    {
      base.m_category = ".NET Samples"; 
      base.m_caption = "Dynamic HUD";
      base.m_message = "Dynamic Display HUD";
      base.m_toolTip = "Dynamic Display HUD";
      base.m_name = "DynamicDisplayHUD_DDHUDCmd";

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
    #endregion

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
      //cast into dynamic map. make sure that the current display supports dynamic display mode.
      m_dynamicMap = m_hookHelper.FocusMap as IDynamicMap;
      if (null == m_dynamicMap)
      {
        MessageBox.Show("The current display does not support dynamic mode.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        return;
      }

      if (!m_bIsDynamicMode)
      {
        //verify that the display is currently in dynamic mode
        if (!m_dynamicMap.DynamicMapEnabled)
        {
          MessageBox.Show("In order to add the HUD you must enable dynamic mode", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
          return;
        }

        //start listening to DynamicMap's 'After Draw' events
        ((IDynamicMapEvents_Event)m_dynamicMap).AfterDynamicDraw += new IDynamicMapEvents_AfterDynamicDrawEventHandler(OnAfterDynamicDraw);

        //need to redraw the screen
        m_hookHelper.ActiveView.ScreenDisplay.UpdateWindow();
      }
      else
      {
        //stop listening to DynamicMap's 'After Draw' events
        ((IDynamicMapEvents_Event)m_dynamicMap).AfterDynamicDraw -= new IDynamicMapEvents_AfterDynamicDrawEventHandler(OnAfterDynamicDraw);
      }

      //redraw the display
      m_hookHelper.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
      m_hookHelper.ActiveView.ScreenDisplay.UpdateWindow();

      m_bIsDynamicMode = !m_bIsDynamicMode;
    }

    /// <summary>
    /// Controls the appearance of the button (checked or unchecked)
    /// </summary>
    public override bool Checked
    {
      get { return m_bIsDynamicMode; }
    }

    #endregion

    #region private methods
    /// <summary>
    /// DynamicMap AfterDynamicDraw event handler method
    /// </summary>
    /// <param name="DynamicMapDrawPhase"></param>
    /// <param name="Display"></param>
    /// <param name="dynamicDisplay"></param>
    private void OnAfterDynamicDraw(esriDynamicMapDrawPhase DynamicMapDrawPhase, IDisplay Display, IDynamicDisplay dynamicDisplay)
    {
      try
      {
				if (DynamicMapDrawPhase != esriDynamicMapDrawPhase.esriDMDPDynamicLayers)
					return;
													 
				//make sure that the display is valid as well as that the layer is visible
        if (null == dynamicDisplay || null == Display)
          return;

        tagRECT rect = Display.DisplayTransformation.get_DeviceFrame();
        float rotation = (float)Display.DisplayTransformation.Rotation;

        if (m_bOnce)
        {
          //need to cache all the DynamicDisplay stuff
          m_dynamicGlyphFactory = dynamicDisplay.DynamicGlyphFactory;
          m_dynamicDrawScreen = (IDynamicDrawScreen)dynamicDisplay;
          m_dynamicSymbolProps = (IDynamicSymbolProperties)dynamicDisplay;

          //set the screen coordinates of the char symbol
          m_point = new ESRI.ArcGIS.Geometry.PointClass();

          CreateDynamicGlyphs(m_dynamicGlyphFactory);

          m_bOnce = false;
        }


        //draw the OpenGL compass
        GL.glPushMatrix();
        GL.glLoadIdentity();

        //use OpenGL to do the drawings
        DrawTicks(rect, rotation);

        GL.glPopMatrix();

        //please note that while you are rotating the map, the numbers showing in the HUD are
        //different than the number reported by the rotation tool. The reason for that is that 
        //the reported map rotation is the mathematical angle while the HUD shows the angle from 
        //the north (the map's azimuth). 
        DrawAzimuths(rect, rotation);
      }
      catch (Exception ex)
      {
        System.Windows.Forms.MessageBox.Show(ex.Message, "ERROR", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
      }
    }

    /// <summary>
    /// Creates the dynamic glyph used to draw the numbers in the HUD 
    /// </summary>
    /// <param name="pDynamicGlyphFactory"></param>
    private void CreateDynamicGlyphs(IDynamicGlyphFactory pDynamicGlyphFactory)
    {
      try
      {
        //create the text glyph using a text symbol
        ITextSymbol textSymbol = new TextSymbolClass();
        textSymbol.Size = 15.0;
        textSymbol.Angle = 0.0;
        textSymbol.VerticalAlignment = esriTextVerticalAlignment.esriTVACenter;
        textSymbol.HorizontalAlignment = esriTextHorizontalAlignment.esriTHACenter;
        textSymbol.Font = ESRI.ArcGIS.ADF.Connection.Local.Converter.ToStdFont(new Font("Arial", 15.0f, FontStyle.Regular));

        m_textGlyph = pDynamicGlyphFactory.CreateDynamicGlyph((ISymbol)textSymbol);

      }
      catch (Exception ex)
      {
        System.Diagnostics.Trace.WriteLine(ex.Message);
      }
    }

    /// <summary>
    /// draw the tick marks of the HUD
    /// </summary>
    /// <param name="deviceFrame"></param>
    /// <param name="azimuth"></param>
    private void DrawTicks(tagRECT deviceFrame, float azimuth)
    {
      //get the floor of the azimuth
      float floorAzi = (int)(azimuth / 10.0f) * 10.0f;
      float deltaAzi = (azimuth - floorAzi) * 6.0f;
      float deltaAziSmall = (azimuth - ((int)(azimuth / 2.0f) * 2.0f)) * 6.0f;

      float delta = 60.0f;
      float deltaSmall = 12.0f;
      float xmin = (float)deviceFrame.left;
      float xmax = (float)deviceFrame.right;
      float ymin = (float)deviceFrame.top;
      float xmiddle = (xmax + xmin) / 2.0f;

      GL.glDisable(GL.GL_TEXTURE_2D);

      //draw a line from left to right
      GL.glColor3f(0.0f, 0.5f, 0.0f);
      GL.glLineWidth(1.5f);
      GL.glBegin(GL.GL_LINES);
      GL.glVertex2f(xmiddle - 150.0f, ymin + 40.0f);
      GL.glVertex2f(xmiddle + 150.0f, ymin + 40.0f);
      GL.glEnd();

      //draw the 10 degrees big ticks
      float x = xmiddle - 150.0f + deltaAzi;

      for (int i = 0; i < 5; i++)
      {
        GL.glBegin(GL.GL_LINES);
        GL.glVertex2f(x, ymin + 40.0f);
        GL.glVertex2f(x, ymin + 80.0f);
        GL.glEnd();
        x += delta;
      }

      //draw the 2 degrees small ticks
      x = xmiddle - 150.0f + deltaAziSmall;
      GL.glLineWidth(1.0f);
      for (int i = 0; i < 25; i++)
      {
        GL.glBegin(GL.GL_LINES);
        GL.glVertex2f(x, ymin + 40.0f);
        GL.glVertex2f(x, ymin + 60.0f);
        GL.glEnd();
        x += deltaSmall;
      }

      
      GL.glLineWidth(2.0f);
      GL.glColor3f(0.0f, 0.0f, 0.0f);
      GL.glBegin(GL.GL_TRIANGLES);
        GL.glVertex2f(xmiddle, ymin + 40.0f);
        GL.glVertex2f(xmiddle - 8.0f, ymin + 60.0f);
        GL.glVertex2f(xmiddle + 8.0f, ymin + 60.0f);
      GL.glEnd();

      GL.glEnable(GL.GL_TEXTURE_2D);
    }

    /// <summary>
    /// draw the numbers (azimuth) in the HUD
    /// </summary>
    /// <param name="deviceFrame"></param>
    /// <param name="angle"></param>
    private void DrawAzimuths(tagRECT deviceFrame, float angle)
    {
      //need to draw the current azimuth
      m_dynamicSymbolProps.SetColor(esriDynamicSymbolType.esriDSymbolText, 0.0f, 0.8f, 0.0f, 1.0f); // Green
      //assign the item's glyph to the dynamic-symbol
      m_dynamicSymbolProps.set_DynamicGlyph(esriDynamicSymbolType.esriDSymbolText, m_textGlyph);

      //get the floor of the azimuth
      float azimuth = 180.0f - angle;
      if (azimuth > 360)
        azimuth -= 360;
      else if (azimuth < 0)
        azimuth += 360;

      float floorAzi = (int)(azimuth / 10.0f) * 10.0f;
      double deltaAzi = (angle - (float)((int)(angle / 10.0f) * 10.0f)) * 6.0; //(the shift to the X axis)

      double xmin = (double)deviceFrame.left;
      double xmax = (double)deviceFrame.right;
      double ymin = (double)deviceFrame.top;
      double xmiddle = (xmax + xmin) / 2.0;

      double x = xmiddle - 150.0 + deltaAzi;

      double dAzStartMiddle = (150.0 - 2.0 * deltaAzi) / 6.0;
      dAzStartMiddle = (int)(dAzStartMiddle / 60.0) * 60.0 + 10.0;
      int azi = (int)(floorAzi - dAzStartMiddle) - 5;
      double delta = 60.0;
      m_dynamicSymbolProps.set_Heading(esriDynamicSymbolType.esriDSymbolText, 0.0f);
      m_dynamicSymbolProps.set_RotationAlignment(esriDynamicSymbolType.esriDSymbolText, esriDynamicSymbolRotationAlignment.esriDSRAScreen);
      for (int i = 0; i < 5; i++)
      {
        m_point.PutCoords(x, ymin + 28.0);

        if (azi > 360)
          azi -= 360;
        else if (azi < 0)
          azi += 360;
        m_dynamicDrawScreen.DrawScreenText(m_point, azi.ToString());

        azi += 10;
        x += delta;
      }

      //need to draw the current azimuth
      m_dynamicSymbolProps.SetColor(esriDynamicSymbolType.esriDSymbolText, 0.0f, 0.0f, 0.0f, 1.0f); // Black
      m_point.PutCoords(xmiddle, ymin + 95.0);
      m_dynamicDrawScreen.DrawScreenText(m_point, azimuth.ToString("###"));
    }
    #endregion
  }
}
