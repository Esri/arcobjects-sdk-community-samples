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

namespace GraphicsLayerToolControl
{
  /// <summary>
  /// this command adds a new graphics layer to the map and makes it the active graphics layer
  /// </summary>
  [Guid("ba7007d0-545a-49a8-87a1-b42e5f2d2262")]
  [ClassInterface(ClassInterfaceType.None)]
  [ProgId("GraphicsLayerToolControl.NewGraphicsLayerCmd")]
  public sealed class NewGraphicsLayerCmd : BaseCommand
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
      MxCommands.Register(regKey);

    }
    /// <summary>
    /// Required method for ArcGIS Component Category unregistration -
    /// Do not modify the contents of this method with the code editor.
    /// </summary>
    private static void ArcGISCategoryUnregistration(Type registerType)
    {
      string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
      ControlsCommands.Unregister(regKey);
      MxCommands.Unregister(regKey);
    }

    #endregion
    #endregion

    #region class members
    private IHookHelper m_hookHelper = null;
    #endregion

    #region class constructor
    public NewGraphicsLayerCmd()
    {
      base.m_category = ".NET Samples";
      base.m_caption = "New Graphics Layer"; 
      base.m_message = "Add new Graphics layer";
      base.m_toolTip = "Add new Graphics layer";
      base.m_name = "ToolControlSample_NewGraphicsLayerCmd";   

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

      if (m_hookHelper == null)
        m_hookHelper = new HookHelperClass();

      m_hookHelper.Hook = hook;
    }

    /// <summary>
    /// Occurs when this command is clicked
    /// </summary>
    public override void OnClick()
    {
      //get the map
      IMap map = m_hookHelper.FocusMap;
      
      //count the graphics layers (will be used in order to name the new layer)
      int graphicsLayerCoount = 0;
      for (int i = 0; i < map.LayerCount; i++)
      {
        if (map.get_Layer(i) is IGraphicsLayer)
          graphicsLayerCoount++;
      }

      //create a new graphics layer
      IGraphicsLayer graphicsLayer = new CompositeGraphicsLayerClass();
      //name the new layer
      ((ILayer)graphicsLayer).Name = "Graphics Layer " + graphicsLayerCoount.ToString();
      //make the new graphics layer the active graphics layer
      map.ActiveGraphicsLayer = (ILayer)graphicsLayer;
      //add the new layer to the map
      map.AddLayer((ILayer)graphicsLayer);
    }

    #endregion
  }
}