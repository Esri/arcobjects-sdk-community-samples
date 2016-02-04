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
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.SystemUI;

namespace SelectionCOMSample
{
  /// <summary>
  /// Summary description for SelectionToolPalette.
  /// </summary>
  [Guid("23a0177f-f011-434a-b7f3-80d718d93fd0")]
  [ClassInterface(ClassInterfaceType.None)]
  [ProgId("SelectionCOMSample.SelectionToolPalette")]
  public sealed class SelectionToolPalette : BaseCommand, IToolPalette
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

    }
    /// <summary>
    /// Required method for ArcGIS Component Category unregistration -
    /// Do not modify the contents of this method with the code editor.
    /// </summary>
    private static void ArcGISCategoryUnregistration(Type registerType)
    {
      string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
      MxCommands.Unregister(regKey);

    }

    #endregion
    #endregion

    private IApplication m_application;
    public SelectionToolPalette()
    {
      base.m_category = "Developer Samples";
      base.m_caption = "Selection Palette";
      base.m_name = "ESRI_SelectionCOMSample_ToolPalette";  
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

      m_application = hook as IApplication;

      //Disable if it is not ArcMap
      if (hook is IMxApplication)
        base.m_enabled = true;
      else
        base.m_enabled = false;
    }

    /// <summary>
    /// Occurs when this command is clicked
    /// </summary>
    public override void OnClick()
    {
    }

    #endregion

    #region IToolPalette Members

    public bool MenuStyle
    {
      get { return false; }
    }

    public int PaletteColumns
    {
      get { return 2; }
    }

    public int PaletteItemCount
    {
      get { return 3; }
    }

    public bool TearOff
    {
      get { return false; }
    }

    public string get_PaletteItem(int pos)
    {
      switch (pos)
      {
        case 0:
          return "esriArcMapUI.SelectByPolygonTool";
        case 1:
          return "esriArcMapUI.SelectByLayerCommand";
        case 2:
          return "SelectionCOMSample.SelectByLineTool";
        default:
          return "";
      }
    }

    #endregion
  }
}
