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
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.SystemUI;

namespace MyDynamicLayer
{
  /// <summary>
  /// A toolbar class for ArcGIS applications.
  /// <para>In order to add commandItems to the toolbar, use the ToolDef array definition. The parameters passed to the 
  /// ToolDef struct constructor are the commandItem's CLSID or ProgID, boolean flag which indicates whether
  /// to begin a new group on the menu and the subtype number of the command. If the CommnadItem does not implements 
  /// any subtypes, just pass -1</para>
  /// </summary>
  [Guid("0515b4e8-83b9-409b-bcde-e7d59c6a86a8")]
  [ClassInterface(ClassInterfaceType.None)]
  [ProgId("MyDynamicLayer.DynamicLayerToolbar")]
  [ComVisible(true)]
  public class DynamicLayerToolbar : IToolBarDef
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
    }
    /// <summary>
    /// Required method for ArcGIS Component Category unregistration -
    /// Do not modify the contents of this method with the code editor.
    /// </summary>
    private static void ArcGISCategoryUnregistration(Type registerType)
    {
      string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
      ControlsToolbars.Unregister(regKey);
    }

    #endregion
    #endregion

    //class members
    string m_sToolbarName;
    string m_sToolbarCaption;

    /// <summary>
    /// a data-structure used in order to store and manage item definitions
    /// </summary>
    struct ToolDef
    {
      public string itemDef;
      public bool group;
      public int subType;

      /// <summary>
      /// struct constructor
      /// </summary>
      /// <param name="itd">The CLSID or PROGID of the item being defined</param>
      /// <param name="grp">Indicates if the defined item should start a group on the toolbar.</param>
      /// <param name="subtype">The subtype of the item being defined</param>
      public ToolDef(string itd, bool grp, int subtype)
      {
        itemDef = itd;
        group = grp;
        subType = subtype;
      }
    };

    //an array of item definitions which will be used to create the commends for the toolbar
    private ToolDef[] m_toolDefs = {
                                     //add the 'AddMyDynamicLayerCmd' command onto the toolbar
                                     new ToolDef("MyDynamicLayer.AddMyDynamicLayerCmd", false, -1)
                                   };

    /// <summary>
    /// Class constructor
    /// </summary>
    public DynamicLayerToolbar()
    {
      //name the toolbar and set its caption
      m_sToolbarName = "MyDynamicLayer";
      m_sToolbarCaption = "MyDynamicLayer";
    }

    #region IToolBarDef Implementations
    /// <summary>
    /// The CLSID for the item on this toolbar at the specified index.
    /// </summary>
    /// <param name="pos">the locational index number of this item on the toolbar</param>
    /// <param name="itemDef">IItemDef object that defines the item at this position of the toolbar</param>
    public void GetItemInfo(int pos, ESRI.ArcGIS.SystemUI.IItemDef itemDef)
    {
      itemDef.ID = m_toolDefs[pos].itemDef;
      itemDef.Group = m_toolDefs[pos].group;
      itemDef.SubType = m_toolDefs[pos].subType;
    }

    /// <summary>
    /// The caption of this toolbar
    /// </summary>
    public string Caption
    {
      get
      {
        return m_sToolbarCaption;
      }
    }

    /// <summary>
    /// The name of this toolbar
    /// </summary>
    public string Name
    {
      get
      {
        return m_sToolbarName;
      }
    }

    /// <summary>
    /// The number of items in this toolbar
    /// </summary>
    public int ItemCount
    {
      get
      {
        return m_toolDefs.Length;
      }
    }
    #endregion
  }
}
