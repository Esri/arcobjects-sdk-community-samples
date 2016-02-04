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

namespace RSSWeatherLayer
{
	/// <summary>
	/// Refreshes the layer's data (runs the layer update thread)
	/// </summary>     
  [Guid("30FD7DC6-6696-4101-99AD-4C8CDE968E4B")]
  [ProgId("RSSWeatherLayer.RefreshLayerCmd")]
  [ComVisible(true)]
	public class RefreshLayerCmd: BaseCommand
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

    //Class members
    private IHookHelper m_hookHelper = null;

    /// <summary>
    /// CTor
    /// </summary>
		public RefreshLayerCmd()
		{
      base.m_category = "Weather";
      base.m_caption = "Refresh layer";
      base.m_message = "Refresh the layer";
      base.m_toolTip = "Refresh layer";
      base.m_name = base.m_category + "_" + base.m_caption;

      try
      {
        base.m_bitmap = new Bitmap(GetType().Assembly.GetManifestResourceStream(GetType(), "RefreshLayerCmd.bmp"));
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
      //Instantiate the hook helper
      if (m_hookHelper == null)
        m_hookHelper = new HookHelperClass();

      //set the hook
      m_hookHelper.Hook = hook;
    }

    /// <summary>
    /// Occurs when this command is clicked
    /// </summary>
    public override void OnClick()
    {
      try
      {
        if (0 == m_hookHelper.FocusMap.LayerCount)
          return;

        RSSWeatherLayerClass weatherLayer = null;
        
        //get the weather layer
        IEnumLayer layers = m_hookHelper.FocusMap.get_Layers(null, false);
        layers.Reset();
        ILayer layer = layers.Next();
        while (layer != null)
        {
          if (layer is RSSWeatherLayerClass)
          {
            weatherLayer = (RSSWeatherLayerClass)layer;
            weatherLayer.Refresh();
            return;
          }
          layer = layers.Next();
        }
      }
      catch (Exception ex)
      {
        System.Diagnostics.Trace.WriteLine(ex.Message);
      }
    }

    #endregion
	}
}
