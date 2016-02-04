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
using System.Windows.Forms;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;


namespace ESRI.ArcGIS.Samples.SimplePointPlugin
{
  /// <summary>
  /// This command 
  /// </summary>
  [Guid("b358ec88-8304-4d19-a7af-80d63566e4d1")]
  [ClassInterface(ClassInterfaceType.None)]
  [ProgId("SimplePointPlugin.AddSimplePointLayerCmd")]
  public sealed class AddSimplePointLayerCmd : BaseCommand
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
      MxCommands.Register(regKey);
      ControlsCommands.Register(regKey);
    }
    /// <summary>
    /// Required method for ArcGIS Component Category unregistration -
    /// Do not modify the contents of this method with the code editor.
    /// </summary>
    private static void ArcGISCategoryUnregistration(Type registerType)
    {
      string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
      MxCommands.Unregister(regKey);
      ControlsCommands.Unregister(regKey);
    }

    #endregion
    #endregion

    private IHookHelper m_hookHelper = null;
    public AddSimplePointLayerCmd()
    {
      base.m_category = ".NET Samples"; 
      base.m_caption = "Add SimplePoint layer";
      base.m_message = "Add SimplePoint layer data source to the map";
      base.m_toolTip = "Add SimplePoint layer";
      base.m_name = "SimplePointPlugin_AddSimplePointLayerCmd";   

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
      try
      {
        OpenSimplePointDlg dlg = new OpenSimplePointDlg(m_hookHelper);
        dlg.Show();

        ////get the type using the ProgID
        //Type t = Type.GetTypeFromProgID("esriGeoDatabase.SimplePointPluginWorkspaceFactory");
        ////Use activator in order to create an instance of the workspace factory
        //IWorkspaceFactory workspaceFactory = Activator.CreateInstance(t) as IWorkspaceFactory;

        //string path = GetFileName();
        //if (string.Empty == path)
        //  return;

        ////open the workspace
        //IFeatureWorkspace featureWorkspace = (IFeatureWorkspace)workspaceFactory.OpenFromFile(@"C:\Data\Data", 0);
        
        ////get a featureclass from the workspace
        //IFeatureClass featureClass = featureWorkspace.OpenFeatureClass("points");

        ////create a new feature layer and add it to the map
        //IFeatureLayer featureLayer = new FeatureLayerClass();
        //featureLayer.Name = featureClass.AliasName;
        //featureLayer.FeatureClass = featureClass;
        //m_hookHelper.FocusMap.AddLayer((ILayer)featureLayer);
      }
      catch (Exception ex)
      {
        System.Diagnostics.Trace.WriteLine(ex.Message);
      }
    }

    #endregion
  }
}
