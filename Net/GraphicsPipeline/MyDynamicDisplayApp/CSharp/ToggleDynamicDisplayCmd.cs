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
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;

namespace MyDynamicDisplayApp
{
  /// <summary>
  /// Summary description for ToggleDynamicDisplayCmd.
  /// </summary>
  [Guid("290c4325-84b9-49fd-9436-b27c4a4f1de5")]
  [ClassInterface(ClassInterfaceType.None)]
  [ProgId("MyDynamicDisplayApp.ToggleDynamicDisplayCmd")]
  public sealed class ToggleDynamicDisplayCmd : BaseCommand
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
    private IDynamicMap m_dynamicMap;

    public ToggleDynamicDisplayCmd()
    {
      base.m_category = "Toggle dynamic display"; 
      base.m_caption = "Toggle dynamic mode";  
      base.m_message = "Toggle dynamic mode on and off";   
      base.m_toolTip = "Toggle dynamic mode";
      base.m_name = "MyDynamicDisplayApp_ToggleDynamicDisplayCmd";   

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
      m_dynamicMap = m_hookHelper.FocusMap as IDynamicMap;
      if (m_dynamicMap == null)
        return;

      m_dynamicMap.DynamicDrawRate = 15;
      m_dynamicMap.DynamicMapEnabled = !m_dynamicMap.DynamicMapEnabled;
    }

    public override bool Checked
    {
      get
      {
        if (m_dynamicMap == null)
          return false;

        return m_dynamicMap.DynamicMapEnabled;
      }
    }

    #endregion
  }
}
