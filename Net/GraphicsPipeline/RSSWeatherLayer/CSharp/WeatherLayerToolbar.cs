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
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.SystemUI;
using System;
using System.Runtime.InteropServices;
namespace RSSWeatherLayer
{

  /// <summary>
  /// The layer's command items toolbar
  /// </summary>
  [Guid("653D29A8-10A4-44b8-9140-86170B715931")]
  [ClassInterface(ClassInterfaceType.None)]
  [ProgId("RSSWeatherLayer.WeatherLayerToolbar")]
  [ComVisible(true)]
  public class WeatherLayerToolbar : IToolBarDef
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
      MxCommandBars.Register(regKey);
      ControlsToolbars.Register(regKey);
    }
    /// <summary>
    /// Required method for ArcGIS Component Category unregistration -
    /// Do not modify the contents of this method with the code editor.
    /// </summary>
    private static void ArcGISCategoryUnregistration(Type registerType)
    {
      string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
      MxCommandBars.Unregister(regKey);
      ControlsToolbars.Unregister(regKey);
    }

    #endregion
    #endregion

    struct ToolDef
    {
      public string  itemDef;
      public bool    group;
      public ToolDef(string itd, bool grp)
      {
        itemDef = itd;
        group = grp;
      }
    };

    private ToolDef[] m_toolDefs = {
                                     new ToolDef("RSSWeatherLayer.AddRSSWeatherLayer", false),
                                     new ToolDef("RSSWeatherLayer.SelectByCityName", false),
                                     new ToolDef("RSSWeatherLayer.AddWeatherItemCmd", false),
                                     new ToolDef("RSSWeatherLayer.AddWeatherItemTool", false),
                                     new ToolDef("RSSWeatherLayer.RefreshLayerCmd", false) 
                                   };

    public WeatherLayerToolbar()
    {
    }

  
    #region IToolBarDef Implementations
    public void GetItemInfo(int pos, ESRI.ArcGIS.SystemUI.IItemDef itemDef)
    {
      itemDef.ID = m_toolDefs[pos].itemDef;
      itemDef.Group = m_toolDefs[pos].group;
    }

    public string Caption
    {
      get
      {
        return "RSS Weather layer";
      }
    }

    public string Name
    {
      get
      {
        return "WeatherLayerToolbar";
      }
    }

    public int ItemCount
    {
      get
      {
        return m_toolDefs.Length;
      }
    }
    #endregion

  }
}
