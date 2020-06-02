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
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;

namespace RSSWeatherGraphicTracker
{
  /// <summary>
  /// Command that works in ArcMap/Map/PageLayout, ArcScene/SceneControl
  /// or ArcGlobe/GlobeControl
  /// </summary>
  [Guid("ffae67a3-92b6-47d6-9d33-a8dd909a53c4")]
  [ClassInterface(ClassInterfaceType.None)]
  [ProgId("RSSWeatherGraphicTracker.AddRSSWeather")]
  public sealed class AddRSSWeather : BaseCommand
  {
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
      GMxCommands.Register(regKey);
      MxCommands.Register(regKey);
      SxCommands.Register(regKey);
      ControlsCommands.Register(regKey);
    }
    /// <summary>
    /// Required method for ArcGIS Component Category unregistration -
    /// Do not modify the contents of this method with the code editor.
    /// </summary>
    private static void ArcGISCategoryUnregistration(Type registerType)
    {
      string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
      GMxCommands.Unregister(regKey);
      MxCommands.Unregister(regKey);
      SxCommands.Unregister(regKey);
      ControlsCommands.Unregister(regKey);
    }

    #endregion
    #endregion

    private IHookHelper m_hookHelper = null;
    private IGlobeHookHelper m_globeHookHelper = null;
    
    private bool m_bConnected = false;
    private RSSWeather m_rssWeather = null;

    public AddRSSWeather()
    {
      base.m_category = "Weather";
      base.m_caption = "Add RSS Weather";
      base.m_message = "Add RSS Weather";
      base.m_toolTip = "Add RSS Weather";
      base.m_name = "Add RSS Weather";

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

      // Test the hook that calls this command and disable if nothing is valid
      try
      {
        m_hookHelper = new HookHelperClass();
        m_hookHelper.Hook = hook;
        if (m_hookHelper.ActiveView == null)
        {
          m_hookHelper = null;
        }
      }
      catch
      {
        m_hookHelper = null;
      }
      if (m_hookHelper == null)
      {
        //Can be globe
        try
        {
          m_globeHookHelper = new GlobeHookHelperClass();
          m_globeHookHelper.Hook = hook;
          if (m_globeHookHelper.ActiveViewer == null)
          {
            m_globeHookHelper = null;
          }
        }
        catch
        {
          m_globeHookHelper = null;
        }       
      }

      if (m_globeHookHelper == null && m_hookHelper == null)
        base.m_enabled = false;
      else
        base.m_enabled = true;

    }

    /// <summary>
    /// Occurs when this command is clicked
    /// </summary>
    public override void OnClick()
    {
      IBasicMap basicMap = null;
      if (m_hookHelper != null)
      {
        basicMap = m_hookHelper.FocusMap as IBasicMap;
      }
      else if (m_globeHookHelper != null)
      {
        basicMap = m_globeHookHelper.Globe as IBasicMap;
      }

      if (basicMap == null)
        return;

      try
      {
        if (!m_bConnected)
        {
          m_rssWeather = new RSSWeather();
          m_rssWeather.Init(basicMap);
        }
        else
        {
          m_rssWeather.Remove();
          m_rssWeather = null;
        }

        m_bConnected = !m_bConnected;
      }
      catch (Exception ex)
      {
        System.Diagnostics.Trace.WriteLine(ex.Message);
      }
    }

    public override bool Checked
    {
      get
      {
        return m_bConnected;
      }
    }

    #endregion
  }
}
