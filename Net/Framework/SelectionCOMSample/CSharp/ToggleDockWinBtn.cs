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
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.ArcMapUI;

namespace SelectionCOMSample
{
  /// <summary>
  /// Summary description for ToggleDockWinBtn.
  /// </summary>
  [Guid("26cd6ddc-c2d0-4102-a853-5f7043c6e797")]
  [ClassInterface(ClassInterfaceType.None)]
  [ProgId("SelectionCOMSample.ToggleDockWinBtn")]
  public sealed class ToggleDockWinBtn : BaseCommand
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
    private SelectionExtension m_mainExtension;
    private IDockableWindow m_dockWindow;

    public ToggleDockWinBtn()
    {
      base.m_category = "Developer Samples"; 
      base.m_caption = "Toggle Dockable Window C#";  
      base.m_message = "Toggle dockable window C#.";  
      base.m_toolTip = "Toggle dockable window C#.\r\nSelection Sample Extension needs to be turned on in Extensions dialog.";  
      base.m_name = "ESRI_SelectionCOMSample_ToggleDockWinBtn";  

      try
      {
        base.m_bitmap = new Bitmap(GetType().Assembly.GetManifestResourceStream("SelectionCOMSample.Images.ToggleDockWinBtn.png")); 
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

      m_application = hook as IApplication;

      //Disable if it is not ArcMap
      if (hook is IMxApplication)
        base.m_enabled = true;
      else
        base.m_enabled = false;

      m_mainExtension = SelectionExtension.GetExtension();

      if (m_mainExtension != null)
        m_dockWindow = m_mainExtension.GetSelectionCountWindow;

    }

    /// <summary>
    /// Occurs when this command is clicked
    /// </summary>
    public override void OnClick()
    {
      if (m_dockWindow == null)
        return;

      m_dockWindow.Show(!m_dockWindow.IsVisible());

    }

    public override bool Enabled
    {
      get
      {
        if (m_mainExtension == null || m_dockWindow == null)
          return false;
        else
          return m_mainExtension.IsExtensionEnabled;
      }
    }
    #endregion
  }
}
