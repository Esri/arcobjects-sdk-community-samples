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
