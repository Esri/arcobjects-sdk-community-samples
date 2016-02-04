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
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;

namespace MyDynamicLayer
{
  /// <summary>
  /// Summary description for AddMyDynamicLayerCmd.
  /// </summary>
  [Guid("0ddfd2b0-45ba-416a-93c8-c7db41de30e4")]
  [ClassInterface(ClassInterfaceType.None)]
  [ComVisible(true)]
  [ProgId("MyDynamicLayer.AddMyDynamicLayerCmd")]
  public sealed class AddMyDynamicLayerCmd : BaseCommand
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

    private IHookHelper           m_hookHelper      = null;
    private MyDynamicLayerClass   m_dynamicLayer    = null;
    private bool                  m_bIsConnected    = false;
    private bool                  m_bOnce           = true;

    public AddMyDynamicLayerCmd()
    {
      base.m_category = ".NET Samples"; 
      base.m_caption = "Add Dynamic Layer"; 
      base.m_message = "Add Dynamic Layer";
      base.m_toolTip = "Add Dynamic Layer";
      base.m_name = "MyDynamicLayer_AddMyDynamicLayerCmd";   

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
      //make sure to switch into dynamic mode
      IDynamicMap dynamicMap = m_hookHelper.FocusMap as IDynamicMap;
      //make sure that the current Map supports the DynamicDisplay
      if (null == dynamicMap)
        return;

      if (!m_bIsConnected)
      {
        //make sure to switch into dynamic mode
        if (!dynamicMap.DynamicMapEnabled)
          dynamicMap.DynamicMapEnabled = true;

        //do some initializations...
        if (m_bOnce)
        {
          //initialize the dynamic layer
          m_dynamicLayer = new MyDynamicLayerClass();
          m_dynamicLayer.Name = "Dynamic Layer";

          m_bOnce = false;
        }

        //make sure that the layer did not get added to the map        
        bool bLayerHasBeenAdded = false;
        if (m_hookHelper.FocusMap.LayerCount > 0)
        {
          IEnumLayer layers = m_hookHelper.FocusMap.get_Layers(null, false);
          layers.Reset();
          ILayer layer = layers.Next();
          while (layer != null)
          {
            if (layer is MyDynamicLayerClass)
            {
              bLayerHasBeenAdded = true;
              break;
            }
            layer = layers.Next();
          }
        }

        if (!bLayerHasBeenAdded)
        {
          //add the dynamic layer to the map
          m_hookHelper.FocusMap.AddLayer(m_dynamicLayer);
        }
        //connect to the synthetic data source
        m_dynamicLayer.Connect();
      }
      else
      {
        //disconnect to the synthetic data source
        m_dynamicLayer.Disconnect();

        //delete the layer from the map
        m_hookHelper.FocusMap.DeleteLayer(m_dynamicLayer);

        //disable the dynamic display
        if (dynamicMap.DynamicMapEnabled)
          dynamicMap.DynamicMapEnabled = false;
      }

      //set the connected flag
      m_bIsConnected = !m_bIsConnected;
    }

    /// <summary>
    /// set the state of the button of the command
    /// </summary>
    public override bool Checked
    {
      get
      {
        return m_bIsConnected;
      }
    }

    #endregion

  }
}
