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
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.ADF.BaseClasses;

namespace GraphicsLayerToolControl
{
  /// <summary>
  /// Implements a toolbar that hosts the graphics ToolControl as well as the NewGraphicsLayer command
  /// </summary>
  [Guid("254d7ad1-eb3d-42ae-991e-638580792efd")]
  [ClassInterface(ClassInterfaceType.None)]
  [ProgId("GraphicsLayerToolControl.GraphicsLayersToolbar")]
  public sealed class GraphicsLayersToolbar : BaseToolbar
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
      ControlsToolbars.Register(regKey);
      MxCommandBars.Register(regKey);

    }
    /// <summary>
    /// Required method for ArcGIS Component Category unregistration -
    /// Do not modify the contents of this method with the code editor.
    /// </summary>
    private static void ArcGISCategoryUnregistration(Type registerType)
    {
      string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
      ControlsToolbars.Unregister(regKey);
      MxCommandBars.Unregister(regKey);
    }

    #endregion
    #endregion

    #region class constructor
    public GraphicsLayersToolbar()
    {
      AddItem("GraphicsLayerToolControl.GraphicsLayersListToolCtrl");
      AddItem("GraphicsLayerToolControl.NewGraphicsLayerCmd");
    }
    #endregion

    /// <summary>
    /// the caption of the toolbar
    /// </summary>
    public override string Caption
    {
      get
      {
        return "Graphics Layers";
      }
    }

    //the internal name of the toolbar
    public override string Name
    {
      get
      {
        return "GraphicsLayersToolbar";
      }
    }
  }
}