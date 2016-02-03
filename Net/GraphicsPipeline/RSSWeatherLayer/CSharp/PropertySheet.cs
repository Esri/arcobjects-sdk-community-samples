using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Runtime.Remoting;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace RSSWeatherLayer
{
  [Guid("71D93E11-AD59-4104-B922-A92B5F2BF69E")]
  [ComVisible(true)]
  [ProgId("RSSWeatherLayer.PropertySheet")]
  [ClassInterface(ClassInterfaceType.None)]
  public partial class PropertySheet : UserControl, IProvideObjectHandle, ISpecifyPropertyPages
  {
    #region Control Registration
    [ComRegisterFunction]
    static void ComRegister(Type t)
    {
      string keyName = @"CLSID\" + t.GUID.ToString("B");
      using (RegistryKey key =
               Registry.ClassesRoot.OpenSubKey(keyName, true))
      {
        key.CreateSubKey("Control").Close();
        using (RegistryKey subkey = key.CreateSubKey("MiscStatus"))
        {
          subkey.SetValue("", "131457");
        }
        using (RegistryKey subkey = key.CreateSubKey("TypeLib"))
        {
          Guid libid = Marshal.GetTypeLibGuidForAssembly(t.Assembly);
          subkey.SetValue("", libid.ToString("B"));
        }
        using (RegistryKey subkey = key.CreateSubKey("Version"))
        {
          Version ver = t.Assembly.GetName().Version;
          string version =
            string.Format("{0}.{1}",
            ver.Major,
            ver.Minor);
          if (version == "0.0") version = "1.0";
          subkey.SetValue("", version);
        }
      }
    }

    [ComUnregisterFunction]
    static void ComUnregister(Type t)
    {
      // Delete entire CLSID\{clsid} subtree
      string keyName = @"CLSID\" + t.GUID.ToString("B");
      Registry.ClassesRoot.DeleteSubKeyTree(keyName);
    }
    #endregion

    RSSWeatherLayerClass m_layer = null;

    public PropertySheet()
    {
      InitializeComponent();
    }

    #region IProvideObjectHandle Members

    public ObjectHandle ObjectHandle
    {
      get { return new ObjectHandle(this); }
    }

    #endregion

    #region ISpecifyPropertyPages Members

    public void GetPages(ref CAUUID pPages)
    {
      Guid[] g = new Guid[2];

      g[0] = typeof(RSSLayerProps).GUID;
      g[1] = typeof(RSSLayerProps2).GUID;
      pPages.SetPages(g);
    }

    public RSSWeatherLayerClass RSSWatherLayer
    {
      get { return m_layer;  }
      set { m_layer = value; }
    }
    #endregion
  }
}
