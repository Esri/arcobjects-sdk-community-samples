using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;


namespace MapAndPageLayoutSynchApp
{
  /// <summary>
  /// Summary description for OpenNewMapDocument.
  /// </summary>
  [Guid("5bf50443-f852-47cd-9c96-984184b6cca6")]
  [ClassInterface(ClassInterfaceType.None)]
  [ProgId("MapAndPageLayoutSynchApp.OpenNewMapDocument")]
  public sealed class OpenNewMapDocument : BaseCommand
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

    private IHookHelper m_hookHelper;
    private ControlsSynchronizer m_controlsSynchronizer = null;
    private string m_sDocumentPath = string.Empty;

    public OpenNewMapDocument(ControlsSynchronizer controlsSynchronizer)
    {
      base.m_category = ".NET Samples";
      base.m_caption = "Open Map Document";
      base.m_message = "Open Map Document";
      base.m_toolTip = "Open Map Document";
      base.m_name = "DotNetSamplesOpenMapDocument";

      m_controlsSynchronizer = controlsSynchronizer;

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
      //launch a new OpenFile dialog
      OpenFileDialog dlg = new OpenFileDialog();
      dlg.Filter = "Map Documents (*.mxd)|*.mxd";
      dlg.Multiselect = false;
      dlg.Title = "Open Map Document";
      if (dlg.ShowDialog() == DialogResult.OK)
      {
        string docName = dlg.FileName;

        IMapDocument mapDoc = new MapDocumentClass();
        if (mapDoc.get_IsPresent(docName) && !mapDoc.get_IsPasswordProtected(docName))
        {
          mapDoc.Open(docName, string.Empty);

          // set the first map as the active view
          IMap map = mapDoc.get_Map(0);
          mapDoc.SetActiveView((IActiveView)map);

          m_controlsSynchronizer.PageLayoutControl.PageLayout = mapDoc.PageLayout;

          m_controlsSynchronizer.ReplaceMap(map);

          mapDoc.Close();

          m_sDocumentPath = docName;
        }
      }
    }

    #endregion

    public string DocumentFileName
    {
      get { return m_sDocumentPath;  }
    }
  }
}
