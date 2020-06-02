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
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.Carto;
using System.Collections.Generic;

namespace SelectionCOMSample
{
  /// <summary>
  /// Summary description for SelectionTargetComboBox.
  /// </summary>
  [Guid("80e577d6-1b27-4471-986e-c04050a0e2d8")]
  [ClassInterface(ClassInterfaceType.None)]
  [ProgId("SelectionCOMSample.SelectionTargetComboBox")]
  public sealed class SelectionTargetComboBox : BaseCommand, IComboBox
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

    private IMxDocument m_doc;
    private static SelectionTargetComboBox s_comboBox;
    private int m_selAllCookie;
    private IComboBoxHook m_comboBoxHook;
    private Dictionary<int, IFeatureLayer> m_list;

    public SelectionTargetComboBox()
    {
      base.m_category = "Developer Samples";
      base.m_caption = "Selection Target";
      base.m_message = "Select the selection target C#.";
      base.m_toolTip = "Select the selection target C#.";  
      base.m_name = "ESRI_SelectionCOMSample_SelectionTargetComboBox";

      m_selAllCookie = -1;
      s_comboBox = this;
      m_list = new Dictionary<int, IFeatureLayer>();
      try
      {
        base.m_bitmap = new Bitmap(GetType().Assembly.GetManifestResourceStream("SelectionCOMSample.Images.SelectionTargetComboBox.png"));
      }
      catch (Exception ex)
      {
        System.Diagnostics.Trace.WriteLine(ex.Message, "Invalid Bitmap");
      }
    }
    internal static SelectionTargetComboBox GetSelectionComboBox()
    {
      return s_comboBox;
    }
    internal void AddItem(string itemName, IFeatureLayer layer)
    {
      if (m_selAllCookie == -1)
        m_selAllCookie = m_comboBoxHook.Add("Select All");

      //Add each item to combo box.
      int cookie = m_comboBoxHook.Add(itemName);
      m_list.Add(cookie, layer);
    }

    internal void ClearAll()
    {
      m_selAllCookie = -1;
      m_comboBoxHook.Clear();
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

      m_comboBoxHook = hook as IComboBoxHook;

      IApplication application = m_comboBoxHook.Hook as IApplication;
      m_doc = application.Document as IMxDocument;

      //Disable if it is not ArcMap
      if (m_comboBoxHook.Hook is IMxApplication)
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

    #region IComboBox Members

    public int DropDownHeight
    {
      get { return 4; }
    }

    public string DropDownWidth
    {
      get { return "WWWWWWWWWWWWWWWWW"; }
    }

    public bool Editable
    {
      get { return false; }
    }

    public string HintText
    {
      get { return "Choose a target layer."; }
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
      if (cookie == -1)
        return;

      foreach (KeyValuePair<int, IFeatureLayer> item in m_list)
      {
        //All feature layers are selectable if "Select All" is selected;
        //otherwise, only the selected layer is selectable.
        IFeatureLayer fl = item.Value;
        if (fl == null)
          continue;

        if (cookie == item.Key)
        {
          fl.Selectable = true;
          continue;
        }

        fl.Selectable = (cookie == m_selAllCookie) ? true : false;
      }

      //Fire ContentsChanged event to cause TOC to refresh with new selected layers.
      m_doc.ActiveView.ContentsChanged(); ;

    }

    public bool ShowCaption
    {
      get { return false; }
    }

    public string Width
    {
      get { return "WWWWWWWWWWWWWX"; }
    }

    #endregion
  }
}
