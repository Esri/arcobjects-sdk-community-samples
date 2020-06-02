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
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;

namespace DynamicDisplayHUD
{
  /// <summary>
  /// This command toggles dynamic mode
  /// </summary>
  [Guid("08b129ae-db54-46bb-b13c-5e44f62997b6")]
  [ClassInterface(ClassInterfaceType.None)]
  [ProgId("DynamicDisplayHUD.ToggleDynamicModeCmd")]
  public sealed class ToggleDynamicModeCmd : BaseCommand
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
    private IHookHelper   m_hookHelper = null;
    private bool          m_bIsDynamicMode = false;
    private IDynamicMap   m_dynamicMap = null;
    #endregion

    #region class constructor
    public ToggleDynamicModeCmd()
    {
      base.m_category = ".NET Samples";
      base.m_caption = "Toggle dynamic mode"; 
      base.m_message = "Switch in and out of dynamic mode";
      base.m_toolTip = "Toggle dynamic mode";
      base.m_name = "DynamicDisplayHUD_ToggleDynamicModeCmd";  

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

      //switch in and out of dynamic mode
      if (!m_bIsDynamicMode)
      {
        m_dynamicMap.DynamicMapEnabled = true;
      }
      else
      {
        m_dynamicMap.DynamicMapEnabled = false;
      }
      
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
  }
}
