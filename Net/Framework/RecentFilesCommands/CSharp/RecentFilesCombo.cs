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
using System.IO;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.SystemUI;

namespace RecentFilesCommandsCS
{
  /// <summary>
  /// Command that works in ArcMap/Map/PageLayout, ArcScene/SceneControl
  /// or ArcGlobe/GlobeControl
  /// </summary>
  [Guid("39b803a3-4bbb-4099-b4d9-89273af3685d")]
  [ClassInterface(ClassInterfaceType.None)]
  [ProgId("RecentFilesCommandsCS.RecentFilesCombo")]
  public sealed class RecentFilesCombo : BaseCommand, IComboBox
  {
    #region COM Registration Function(s)
    [ComRegisterFunction()]
    [ComVisible(false)]
    static void RegisterFunction(Type registerType)
    {
      // Required for ArcGIS Component Category Registrar support
      ArcGISCategoryRegistration(registerType);
    }

    [ComUnregisterFunction()]
    [ComVisible(false)]
    static void UnregisterFunction(Type registerType)
    {
      // Required for ArcGIS Component Category Registrar support
      ArcGISCategoryUnregistration(registerType);
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
      MxCommands.Register(regKey);
      SxCommands.Register(regKey);
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
      MxCommands.Unregister(regKey);
      SxCommands.Unregister(regKey);
      ControlsCommands.Unregister(regKey);
    }

    #endregion
    #endregion

    private IApplication m_application;
    private System.Collections.Generic.Dictionary<int, string> m_itemMap;
    private string m_strWidth = @"c:\documents\map documents";

    public RecentFilesCombo()
    {
      m_itemMap = new System.Collections.Generic.Dictionary<int, string>();

      base.m_category = ".NET Samples"; 
      base.m_caption = "Recent Documents: ";  
      base.m_message = "Recent Documents"; 
      base.m_toolTip = "Recent Documents";
      base.m_name = "RecentDocsCombo";
    }

    #region Overriden Class Methods

    /// <summary>
    /// Occurs when this command is created
    /// </summary>
    /// <param name="hook">Instance of the application</param>
    public override void OnCreate(object hook)
    {
      IComboBoxHook comboHook = hook as IComboBoxHook;
      if (comboHook == null)
      {
        m_enabled = false;
        return;
      }

      m_application = comboHook.Hook as IApplication;

      int cookie = 0;

      foreach (string fileName in RecentFilesRegistryHelper.GetRecentFiles(m_application))
      {
        if (File.Exists(fileName))
        {
          //Add item to list
          cookie = comboHook.Add(fileName);
          m_itemMap.Add(cookie, fileName);
        }
      }   
    }

    /// <summary>
    /// Occurs when this command is clicked
    /// </summary>
    public override void OnClick()
    {
   
    }

    #endregion

    #region IComboBox Members

    public int DropDownHeight
    {
      get { return 50; }
    }

    public string DropDownWidth
    {
      get { return m_strWidth; }
    }

    public bool Editable
    {
      get { return false; }
    }

    public string HintText
    {
      get { return "Select document"; }
    }

    public void OnEditChange(string editString)
    {
    }

    public void OnEnter()
    {
    }

    public void OnFocus(bool set)
    {
    }

    public void OnSelChange(int cookie)
    {
      string selectedPath = m_itemMap[cookie];
      m_application.OpenDocument(selectedPath);
    }

    public bool ShowCaption
    {
      get { return false; }
    }

    public string Width
    {
      get { return m_strWidth; }
    }

    #endregion

  }
}
