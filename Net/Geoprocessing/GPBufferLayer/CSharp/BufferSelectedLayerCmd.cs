using System;
using System.Drawing;
using System.Text;
//using System.Windows.Forms;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
//using ESRI.ArcGIS.Carto;
//using ESRI.ArcGIS.Geoprocessor;
//using ESRI.ArcGIS.Geoprocessing;
//using ESRI.ArcGIS.AnalysisTools;

namespace GpBufferLayer
{
  /// <summary>
  /// Summary description for BufferSelectedLayerCmd.
  /// </summary>
  [Guid("7dc0aa20-efe4-4714-9110-7f3c57bf00aa")]
  [ClassInterface(ClassInterfaceType.None)]
  [ProgId("GpBufferLayer.BufferSelectedLayerCmd")]
  public sealed class BufferSelectedLayerCmd : BaseCommand
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
      MxCommands.Register(regKey);

    }
    /// <summary>
    /// Required method for ArcGIS Component Category unregistration -
    /// Do not modify the contents of this method with the code editor.
    /// </summary>
    private static void ArcGISCategoryUnregistration(Type registerType)
    {
      string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
      ControlsCommands.Unregister(regKey);
      MxCommands.Unregister(regKey);

    }

    #endregion
    #endregion

    private IHookHelper m_hookHelper;

    public BufferSelectedLayerCmd()
    {
      base.m_category = ".NET Samples";
      base.m_caption = "Buffer selected layer";
      base.m_message = "Buffer selected layer";
      base.m_toolTip = "Buffer selected layer";
      base.m_name = "GpBufferLayer_BufferSelectedLayerCmd";

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

      if (m_hookHelper == null)
        m_hookHelper = new HookHelperClass();

      m_hookHelper.Hook = hook;
    }

    /// <summary>
    /// Occurs when this command is clicked
    /// </summary>
    public override void OnClick()
    {
      if (null == m_hookHelper)
        return;

      if (m_hookHelper.FocusMap.LayerCount > 0)
      {
        BufferDlg bufferDlg = new BufferDlg(m_hookHelper);
        bufferDlg.Show();
      }
    }

    #endregion
  }
}
