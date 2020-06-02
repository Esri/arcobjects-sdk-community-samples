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
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.ADF.BaseClasses;

namespace SelectionCOMSample
{
  /// <summary>
  /// Summary description for SelectionToolbar.
  /// </summary>
  [Guid("680d2553-a6a7-4fbe-88e6-f16650276126")]
  [ClassInterface(ClassInterfaceType.None)]
  [ProgId("SelectionCOMSample.SelectionToolbar")]
  public sealed class SelectionToolbar : BaseToolbar
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
    }
    /// <summary>
    /// Required method for ArcGIS Component Category unregistration -
    /// Do not modify the contents of this method with the code editor.
    /// </summary>
    private static void ArcGISCategoryUnregistration(Type registerType)
    {
      string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
      MxCommandBars.Unregister(regKey);
    }

    #endregion
    #endregion

    public SelectionToolbar()
    {
      AddItem("SelectionCOMSample.SelectionTargetComboBox");
      BeginGroup();
      AddItem("SelectionCOMSample.SelectionZoomToLayerMenu");
      BeginGroup();
      AddItem("{26cd6ddc-c2d0-4102-a853-5f7043c6e797}");  //Toggle dockwin button
      AddItem("{15de72ff-f31f-4655-98b6-191b7348375a}"); //Select by line tool
      AddItem("SelectionCOMSample.SelectionToolPalette");
      BeginGroup();
      AddItem("SelectionCOMSample.SelectionMenu");
    }

    public override string Caption
    {
      get
      {
        return "Selection COM Toolbar";
      }
    }
    public override string Name
    {
      get
      {
        return "ESRI_SelectionCOMSample_SelectionToolbar";
      }
    }
  }
}