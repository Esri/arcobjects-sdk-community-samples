using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.ADF.CATIDs;

namespace SelectionCOMSample
{
  [Guid("3c20265d-1db1-4c94-b145-a2f6cdb504bf")]
  [ClassInterface(ClassInterfaceType.None)]
  [ProgId("SelectionCOMSample.SelectionCountDockWin")]
  public partial class SelectionCountDockWin : UserControl, IDockableWindowDef, IDockableWindowInitialPlacement, IDockableWindowImageDef
  {
    private IApplication m_application;
    private static System.Windows.Forms.ListView s_listView;

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
      MxDockableWindows.Register(regKey);

    }
    /// <summary>
    /// Required method for ArcGIS Component Category unregistration -
    /// Do not modify the contents of this method with the code editor.
    /// </summary>
    private static void ArcGISCategoryUnregistration(Type registerType)
    {
      string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
      MxDockableWindows.Unregister(regKey);

    }

    #endregion
    #endregion

    public SelectionCountDockWin()
    {
      InitializeComponent();
      s_listView = listView1;
      listView1.View = View.Details;

    }
    internal static void Clear()
    {
      if (s_listView != null)
        s_listView.Items.Clear();
    }

    internal static void AddItem(string layerName, int selectionCount)
    {
      if (s_listView == null)
        return;

      ListViewItem item = new ListViewItem(layerName);
      item.SubItems.Add(selectionCount.ToString());
      s_listView.Items.Add(item);
    }


    #region IDockableWindowDef Members

    string IDockableWindowDef.Caption
    {
      get
      {
        return "Selected Features Count";
      }
    }

    int IDockableWindowDef.ChildHWND
    {
      get { return this.Handle.ToInt32(); }
    }

    string IDockableWindowDef.Name
    {
      get
      {
        return "ESRI_SelectionCOMSample_SelCountDockWin";
      }
    }

    void IDockableWindowDef.OnCreate(object hook)
    {
      m_application = hook as IApplication;
    }

    void IDockableWindowDef.OnDestroy()
    {
    }

    object IDockableWindowDef.UserData
    {
      get { return null; }
    }

    #endregion


    #region IDockableWindowImageDef Members

    public int Bitmap
    {
      get
      {
        System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(
          GetType().Assembly.GetManifestResourceStream("SelectionCOMSample.Images.ToggleDockWinBtn.png"));
        bitmap.MakeTransparent();
        return bitmap.GetHbitmap().ToInt32();
      }
    }

    #endregion



    #region IDockableWindowInitialPlacement Members

    esriDockFlags IDockableWindowInitialPlacement.DockPosition
    {
      get { return esriDockFlags.esriDockRight | esriDockFlags.esriDockTabbed; }
    }

    int IDockableWindowInitialPlacement.Height
    {
      get { return 300; }
    }

    UID IDockableWindowInitialPlacement.Neighbor
    {
      get
      {
        UID uid = new UIDClass();
        uid.Value = "esriArcMapUI.TOCDockableWindow"; //TOC

        return uid; 
      }
    }

    int IDockableWindowInitialPlacement.Width
    {
      get { return 300; }
    }

    #endregion
  }
}
