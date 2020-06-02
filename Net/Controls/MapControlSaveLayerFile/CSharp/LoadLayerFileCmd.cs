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
using System.Windows.Forms;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;

namespace MapControlSaveLayerFile
{
  /// <summary>
  /// Summary description for LoadLayerFileCmd.
  /// </summary>
  [Guid("c45d8097-5c9b-443c-8859-1663b7b2c540")]
  [ClassInterface(ClassInterfaceType.None)]
  [ProgId("MapControlSaveLayerFile.LoadLayerFileCmd")]
  public sealed class LoadLayerFileCmd : BaseCommand
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

    public LoadLayerFileCmd()
    {
      base.m_category = ".NET Samples"; 
      base.m_caption = "Load Layer File";
      base.m_message = "Load Layer File from disk";
      base.m_toolTip = "Load Layer File";
      base.m_name = "MapControlSaveLayerFile_LoadLayerFileCmd";   

      try
      {
        //
        // TODO: change bitmap name if necessary
        //
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

      // TODO:  Add other initialization code
    }

    /// <summary>
    /// Occurs when this command is clicked
    /// </summary>
    public override void OnClick()
    {
      //need to get the layer from the custom-property of the map
      if (null == m_hookHelper)
        return;

      //get the mapControl hook
      object hook = null;
      if (m_hookHelper.Hook is IToolbarControl2)
      {
        hook = ((IToolbarControl2)m_hookHelper.Hook).Buddy;
      }
      else
      {
        hook = m_hookHelper.Hook;
      }

      //get the custom property from which is supposed to be the layer to be saved
      object customProperty = null;
      IMapControl3 mapControl = null;
      if (hook is IMapControl3)
      {
        mapControl = (IMapControl3)hook;
        customProperty = mapControl.CustomProperty;
      }
      else
        return;

      OpenFileDialog ofdlg = new OpenFileDialog();
      ofdlg.Multiselect = true;
      ofdlg.RestoreDirectory = true;
      ofdlg.Title = "Load Layer Files";
      ofdlg.Filter = "Layer File|*.lyr|All Files|*.*";
      DialogResult dr = ofdlg.ShowDialog();
      if (DialogResult.OK == dr)
      {
        string[] files = ofdlg.FileNames;

        foreach (string file in files)
        {
          if (System.IO.File.Exists(file))
            mapControl.AddLayerFromFile(file, 0);
        }
      }
    }

    #endregion
  }
}
