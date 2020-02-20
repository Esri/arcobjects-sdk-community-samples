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
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.GlobeCore;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.Controls;

namespace RSSWeatherLayer3D
{
  /// <summary>
  /// Summary description for SelectItems.
  /// </summary>
  [ClassInterface(ClassInterfaceType.None)]
  [Guid("620F86DC-22BE-47c1-8808-C908C6F015AC")]
  [ProgId("RSSWeatherLayer3D.SelectItemsTool")]
  [ComVisible(true)]
  public sealed class SelectItemsTool: BaseTool
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
      GMxCommands.Register(regKey);
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
      ControlsCommands.Unregister(regKey);
    }

    #endregion
    #endregion

    private IGlobeHookHelper          m_globeHookHelper = null;
    private RSSWeatherLayer3DClass	  m_weatherLayer		= null;
    private IScene					          m_scene				    = null;

    public SelectItemsTool()
    {
      base.m_category = "Weather3D";
      base.m_caption = "Select Weather Items";
      base.m_message = "Select weather items";
      base.m_toolTip = "Select weather items";
      base.m_name = base.m_category + "_" + base.m_caption;

      try
      {
        base.m_bitmap = new System.Drawing.Bitmap(GetType().Assembly.GetManifestResourceStream(GetType(), "Bitmaps.SelectItemsTool.bmp"));
        base.m_cursor = new System.Windows.Forms.Cursor(GetType().Assembly.GetManifestResourceStream(GetType(), "Cursors.SelectItemsTool.cur"));
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
      if (null == m_globeHookHelper)
        m_globeHookHelper = new GlobeHookHelperClass();

      m_globeHookHelper.Hook = hook;

      m_scene = m_globeHookHelper.Globe as IScene;
    }

    /// <summary>
    /// Occurs when this command is clicked
    /// </summary>
    public override void OnClick()
    {
      try
      {
        m_weatherLayer = null;
				
        //get the weather layer
        IEnumLayer layers = m_scene.get_Layers(null, false);
        layers.Reset();
        ILayer layer = layers.Next();
        while(layer != null)
        {
          if(layer is RSSWeatherLayer3DClass)
          {
            m_weatherLayer = (RSSWeatherLayer3DClass)layer;
            break;
          }
          layer = layers.Next();
        }
      }
      catch(Exception ex)
      {
        System.Diagnostics.Trace.WriteLine(ex.Message);
      }
    }


    public override void OnMouseDown(int Button, int Shift, int X, int Y)
    {
      if(null == m_weatherLayer)
        return;

      object owner;
      object obj;

      IPoint hitPoint;
      IGlobeDisplay globedisplay = ((IGlobe)m_scene).GlobeDisplay;

      ISceneViewer sceneViewer = globedisplay.ActiveViewer;
      globedisplay.Locate(sceneViewer, X, Y, false, false, out hitPoint, out owner, out obj);

      if(obj != null)
      {
        if(!(obj is IPropertySet))
        {
          return;
        }
				
        IPropertySet propset = obj as IPropertySet;
				
        if(!(owner is RSSWeatherLayer3DClass))
          return;

        if(propset == null)
          return;
				
        //get the zip code
        object o = propset.GetProperty("ZIPCODE");
        long zipcode = Convert.ToInt64(o);
        m_weatherLayer.Select(zipcode, Shift != 1);
      }
      else
      {
        m_weatherLayer.Select(-1L, true);
      }

    }
  
    public override void OnMouseMove(int Button, int Shift, int X, int Y)
    {
      // TODO:  Add AddWeatherItemTool.OnMouseMove implementation
    }
  
    public override void OnMouseUp(int Button, int Shift, int X, int Y)
    {
      // TODO:  Add AddWeatherItemTool.OnMouseUp implementation
    }
    #endregion
  }
}
