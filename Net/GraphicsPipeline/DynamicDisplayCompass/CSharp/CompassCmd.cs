using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.SystemUI;
using OpenGL;

namespace DynamicDisplayCompass
{
  /// <summary>
  /// Command that works in ArcMap/Map/PageLayout
  /// </summary>
  [Guid("38360569-7dda-4892-a3a0-ea1f3531177b")]
  [ClassInterface(ClassInterfaceType.None)]
  [ProgId("DynamicDisplayCompass.CompassCmd")]
  public sealed class CompassCmd : BaseCommand
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
    private IHookHelper m_hookHelper = null;
    private IDynamicMap m_dynamicMap = null;
    private bool m_bIsDynamicMode = false;
    private bool m_bOnce = true;
    private uint m_compassList = 0;
    private tagRECT m_deviceFrame;
    #endregion

    #region class constructor
    /// <summary>
    /// constructor
    /// </summary>
    public CompassCmd()
    {
      base.m_category = ".NET Samples";
      base.m_caption = "Dynamic Display Compass";
      base.m_message = "Dynamic Display Compass";
      base.m_toolTip = "Dynamic Display Compass";
      base.m_name = "DynamicDisplay_Compass";

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

      // TODO:  Add other initialization code
    }

    /// <summary>
    /// Occurs when this command is clicked
    /// </summary>
    public override void OnClick()
    {
      m_dynamicMap = m_hookHelper.FocusMap as IDynamicMap;

      if (!m_bIsDynamicMode)
      {
        //switch into dynamic mode
        if (!m_dynamicMap.DynamicMapEnabled)
          m_dynamicMap.DynamicMapEnabled = true;

        //start listening to DynamicMap's 'After Draw' events
        ((IDynamicMapEvents_Event)m_dynamicMap).AfterDynamicDraw += new IDynamicMapEvents_AfterDynamicDrawEventHandler(OnAfterDynamicDraw);
      }
      else
      {
        //stop listening to DynamicMap's 'After Draw' events
        ((IDynamicMapEvents_Event)m_dynamicMap).AfterDynamicDraw -= new IDynamicMapEvents_AfterDynamicDrawEventHandler(OnAfterDynamicDraw);

        //leave dynamic mode
        if (m_dynamicMap.DynamicMapEnabled)
          m_dynamicMap.DynamicMapEnabled = false;


      }
      m_bIsDynamicMode = !m_bIsDynamicMode;
			m_hookHelper.ActiveView.Refresh ();
    }

    public override bool Checked
    {
      get { return m_bIsDynamicMode; }
    }

    #endregion

    #region  private methods
    /// <summary>
    /// nDeviceFrameUpdated event handler
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="sizeChanged"></param>
    private void OnDeviceFrameUpdated(IDisplayTransformation sender, bool sizeChanged)
    {
      //update the device frame rectangle
      m_deviceFrame = sender.get_DeviceFrame();
    }

    /// <summary>
    /// DynamicMap AfterDynamicDraw event handler method
    /// </summary>
    /// <param name="DynamicMapDrawPhase"></param>
    /// <param name="Display"></param>
    /// <param name="dynamicDisplay"></param>
    void OnAfterDynamicDraw(esriDynamicMapDrawPhase DynamicMapDrawPhase, IDisplay Display, IDynamicDisplay dynamicDisplay)
    {
      try
      {
				if (DynamicMapDrawPhase != esriDynamicMapDrawPhase.esriDMDPDynamicLayers)
					return;

        //make sure that the display is valid as well as that the layer is visible
        if (null == dynamicDisplay || null == Display)
          return;

        if (m_bOnce)
        {
          //get the device frame size
          m_deviceFrame = Display.DisplayTransformation.get_DeviceFrame();

          //start listening to display events
          ((ITransformEvents_Event)Display.DisplayTransformation).DeviceFrameUpdated += new ITransformEvents_DeviceFrameUpdatedEventHandler(OnDeviceFrameUpdated);

          CreateDisplayLists();

          m_bOnce = false;
        }

        GL.glPushMatrix();
        GL.glLoadIdentity();

        //draw the compass list
        GL.glPushMatrix();
        GL.glTranslatef((float)m_deviceFrame.left + 70.0f, (float)m_deviceFrame.top + 70.0f, 0.0f);
        GL.glScalef(90.0f, 90.0f, 0.0f);
        GL.glRotatef((float)Display.DisplayTransformation.Rotation, 0.0f, 0.0f, 1.0f);
        GL.glColor4f(1.0f, 1.0f, 1.0f, 1.0f);
        GL.glCallList(m_compassList);
        GL.glPopMatrix();

        GL.glPopMatrix();
      }
      catch (Exception ex)
      {
        System.Windows.Forms.MessageBox.Show(ex.Message, "ERROR", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
      }
    }

    /// <summary>
    /// create the display list for the compass symbol
    /// </summary>
    private void CreateDisplayLists()
    {
      //create the display list for the animated icons
      //the quad size is set to 1 unit. Therefore you will have to scale it 
      //each time before drawing.

      //open the compass bitmap
      Bitmap compassBitmap = new Bitmap(GetType(), "compass.gif");

      //create the texture for the bitmap
      uint textureId = CreateTexture(compassBitmap);

      m_compassList = GL.glGenLists(1);
      GL.glNewList(m_compassList, GL.GL_COMPILE);
      GL.glPushMatrix();
        //shift the item 1/2 unit to the middle so that it'll get drawn around the center
        GL.glTranslatef(-0.5f, -0.5f, 0.0f);
        //enable texture in order to allow for texture binding
        GL.glTexEnvi(GL.GL_TEXTURE_ENV, GL.GL_TEXTURE_ENV_MODE, (int)GL.GL_MODULATE);

        //bind the texture

        GL.glEnable(GL.GL_TEXTURE_2D);
        GL.glBindTexture(GL.GL_TEXTURE_2D, (uint)textureId);

        //create the geometry (quad) and specify the texture coordinates
				GL.glBegin (GL.GL_QUADS);
					GL.glTexCoord2f (0.0f, 0.0f); GL.glVertex2f (0.0f, 0.0f);
					GL.glTexCoord2f (1.0f, 0.0f); GL.glVertex2f (1.0f, 0.0f);
					GL.glTexCoord2f (1.0f, 1.0f); GL.glVertex2f (1.0f, 1.0f);
					GL.glTexCoord2f (0.0f, 1.0f); GL.glVertex2f (0.0f, 1.0f);
				GL.glEnd ();

      GL.glPopMatrix();
      GL.glEndList();
    }

    /// <summary>
    /// Given a bitmap (GDI+), create for it an OpenGL texture and return its ID
    /// </summary>
    /// <param name="bitmap"></param>
    /// <returns>the OGL texture id</returns>
    /// <remarks>in order to allow hardware acceleration, texture size must be power of two.</remarks>
    private uint CreateTexture(Bitmap bitmap)
    {
      try
      {
        //get the bitmap's dimensions
        int h = bitmap.Height;
        int w = bitmap.Width;
        int s = Math.Max(h, w);

        //calculate the closest power of two to match the bitmap's size
        //(thank god for high-school math...)
        double x = Math.Log(Convert.ToDouble(s)) / Math.Log(2.0);
        s = Convert.ToInt32(Math.Pow(2.0, Convert.ToDouble(Math.Ceiling(x))));

        int bufferSizeInPixels = s * s;

        //get the bitmap's raw data 
        Rectangle rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
        bitmap.RotateFlip(RotateFlipType.RotateNoneFlipY);
        System.Drawing.Imaging.BitmapData bitmapData;
        bitmapData = bitmap.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

        

        byte[] bgrBuffer = new byte[bufferSizeInPixels * 3];

        //scale the bitmap to be a power of two
        unsafe
        {
          fixed (byte* pBgrBuffer = bgrBuffer)
          {
            GLU.gluScaleImage(GL.GL_BGR_EXT, bitmap.Size.Width, bitmap.Size.Height, GL.GL_UNSIGNED_BYTE, bitmapData.Scan0.ToPointer(), s, s, GL.GL_UNSIGNED_BYTE, pBgrBuffer);
          }
        }

        //create a new buffer to store the raw data and set the transparency color (alpha = 0)
        byte[] bgraBuffer = new byte[bufferSizeInPixels * 4];

        int posBgr = 0;
        int posBgra = 0;
        for (int i = 0; i < bufferSizeInPixels; i++)
        {
          bgraBuffer[posBgra] = bgrBuffer[posBgr];			    //B
          bgraBuffer[posBgra + 1] = bgrBuffer[posBgr + 1];  //G
          bgraBuffer[posBgra + 2] = bgrBuffer[posBgr + 2];  //R

          //take care of the alpha
          if (255 == bgrBuffer[posBgr] && 255 == bgrBuffer[posBgr + 1] && 255 == bgrBuffer[posBgr + 2])
          {
            bgraBuffer[posBgra + 3] = 0;
          }
          else
          {
            bgraBuffer[posBgra + 3] = 255;
          }
          posBgr += 3;
          posBgra += 4;
        }

        //create the texture
        uint[] texture = new uint[1];
        GL.glEnable(GL.GL_TEXTURE_2D);
        GL.glGenTextures(1, texture);
        GL.glBindTexture(GL.GL_TEXTURE_2D, texture[0]);

        //set the texture parameters
        GL.glTexParameteri(GL.GL_TEXTURE_2D, GL.GL_TEXTURE_MAG_FILTER, (int)GL.GL_LINEAR);
        GL.glTexParameteri(GL.GL_TEXTURE_2D, GL.GL_TEXTURE_MIN_FILTER, (int)GL.GL_LINEAR);
        GL.glTexEnvi(GL.GL_TEXTURE_ENV, GL.GL_TEXTURE_ENV_MODE, (int)GL.GL_MODULATE);

        unsafe
        {
          fixed (byte* pBgraBuffer = bgraBuffer)
          {
            GL.glTexImage2D(GL.GL_TEXTURE_2D, 0, (int)GL.GL_RGBA, s, s, 0, GL.GL_BGRA_EXT, GL.GL_UNSIGNED_BYTE, pBgraBuffer);
          }
        }

        //unlock the bitmap from memory
        bitmap.UnlockBits(bitmapData);

        //return the newly created texture id
        return texture[0];

      }
      catch (Exception ex)
      {
        System.Diagnostics.Trace.WriteLine(ex.Message);
      }
      return (uint)0;
    }
    #endregion
  }
}
