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
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.ArcMapUI;

namespace SelectionCOMSample
{
  [Guid("c1537917-5ca0-4637-9728-a76b70517545")]
  [ClassInterface(ClassInterfaceType.None)]
  [ProgId("SelectionCOMSample.SelectionExtension")]
  public class SelectionExtension : IExtension, IExtensionConfig
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
      MxExtension.Register(regKey);

    }
    /// <summary>
    /// Required method for ArcGIS Component Category unregistration -
    /// Do not modify the contents of this method with the code editor.
    /// </summary>
    private static void ArcGISCategoryUnregistration(Type registerType)
    {
      string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
      MxExtension.Unregister(regKey);

    }

    #endregion
    #endregion
    private IApplication m_application;
    private esriExtensionState m_enableState;
    private static IDockableWindow s_dockWindow;
    private static SelectionExtension s_extension;
    private bool m_hasSelectableLayer;
    private IMap m_map;
    private IMxDocument m_doc;

    public SelectionExtension()
    {
      s_extension = this;
    }
    #region IExtension Members

    /// <summary>
    /// Name of extension. Do not exceed 31 characters
    /// </summary>
    public string Name
    {
      get
      {
        return "ESRI_SelectionCOMSample_SelectionExtension";
      }
    }

    public void Shutdown()
    {
      m_application = null;
    }

    public void Startup(ref object initializationData)
    {
      m_application = initializationData as IApplication;
      if (m_application == null)
        return;

      m_doc = m_application.Document as IMxDocument;

      //Get dockable window.
      IDockableWindowManager dockableWindowManager = m_application as IDockableWindowManager;
      UID dockWinID = new UIDClass();
      dockWinID.Value = @"SelectionCOMSample.SelectionCountDockWin";
      s_dockWindow = dockableWindowManager.GetDockableWindow(dockWinID);

      //Wire up events.
      IDocumentEvents_Event docEvents = m_doc as IDocumentEvents_Event;
      docEvents.NewDocument += new ESRI.ArcGIS.ArcMapUI.IDocumentEvents_NewDocumentEventHandler(ArcMap_NewOpenDocument);
      docEvents.OpenDocument += new ESRI.ArcGIS.ArcMapUI.IDocumentEvents_OpenDocumentEventHandler(ArcMap_NewOpenDocument);

    }

    #endregion

    internal IDockableWindow GetSelectionCountWindow
    {
      get
      {
        return s_dockWindow;
      }
    }

    internal static SelectionExtension GetExtension()
    {
      return s_extension;
    }

    private void ArcMap_NewOpenDocument()
    {
      IActiveViewEvents_Event pageLayoutEvent = m_doc.PageLayout as IActiveViewEvents_Event;
      pageLayoutEvent.FocusMapChanged += new IActiveViewEvents_FocusMapChangedEventHandler(AVEvents_FocusMapChanged);

      Initialize();
    }

    private void Initialize()
    {
      //Reset event handlers.
      IActiveViewEvents_Event avEvent = m_doc.FocusMap as IActiveViewEvents_Event;
      avEvent.ItemAdded += new IActiveViewEvents_ItemAddedEventHandler(AvEvent_ItemAdded);
      avEvent.ItemDeleted += new IActiveViewEvents_ItemDeletedEventHandler(AvEvent_ItemAdded);
      avEvent.SelectionChanged += new IActiveViewEvents_SelectionChangedEventHandler(UpdateSelCountDockWin);
      avEvent.ContentsChanged += new IActiveViewEvents_ContentsChangedEventHandler(avEvent_ContentsChanged);
      //Update the UI.
      m_map = m_doc.FocusMap;
      FillComboBox();
      UpdateSelCountDockWin();
      m_hasSelectableLayer = CheckForSelectableLayer();
    }

    private void avEvent_ContentsChanged()
    {
      m_hasSelectableLayer = CheckForSelectableLayer();
    }

    private void AvEvent_ItemAdded(object Item)
    {
      m_map = m_doc.FocusMap;
      FillComboBox();
      UpdateSelCountDockWin();
      m_hasSelectableLayer = CheckForSelectableLayer();
    }

    private void AVEvents_FocusMapChanged()
    {
      Initialize();
    }

    private void UpdateSelCountDockWin()
    {
      // Update the contents of the lsitView, when the selection changes in the map. 
      IFeatureLayer featureLayer;
      IFeatureSelection featSel;

      SelectionCountDockWin.Clear();

      // Loop through the layers in the map and add the layer's name and
      // selection count to the list box
      for (int i = 0; i < m_map.LayerCount; i++)
      {
        if (m_map.get_Layer(i) is IFeatureSelection)
        {
          featureLayer = m_map.get_Layer(i) as IFeatureLayer;
          if (featureLayer == null)
            break;

          featSel = featureLayer as IFeatureSelection;

          int count = 0;
          if (featSel.SelectionSet != null)
            count = featSel.SelectionSet.Count;
          SelectionCountDockWin.AddItem(featureLayer.Name, count);
        }
      }
    }

    private void FillComboBox()
    {
      SelectionTargetComboBox selCombo = SelectionTargetComboBox.GetSelectionComboBox();
      if (selCombo == null)
        return;

      selCombo.ClearAll();

      IFeatureLayer featureLayer;
      // Loop through the layers in the map and add the layer's name to the combo box.
      for (int i = 0; i < m_map.LayerCount; i++)
      {
        if (m_map.get_Layer(i) is IFeatureSelection)
        {
          featureLayer = m_map.get_Layer(i) as IFeatureLayer;
          if (featureLayer == null)
            break;

          selCombo.AddItem(featureLayer.Name, featureLayer);
        }
      }

    }

    private bool CheckForSelectableLayer()
    {
      IMap map = m_doc.FocusMap;
      // Bail if map has no layers
      if (map.LayerCount == 0)
        return false;

      // Fetch all the feature layers in the focus map
      // and see if at least one is selectable
      UIDClass uid = new UIDClass();
      uid.Value = "{40A9E885-5533-11d0-98BE-00805F7CED21}";
      IEnumLayer enumLayers = map.get_Layers(uid, true);
      IFeatureLayer featureLayer = enumLayers.Next() as IFeatureLayer;
      while (featureLayer != null)
      {
        if (featureLayer.Selectable == true)
          return true;
        featureLayer = enumLayers.Next() as IFeatureLayer;
      }
      return false;
    }
    internal bool IsExtensionEnabled
    {
      get
      {
        return this.State == esriExtensionState.esriESEnabled;
      }
    }

    internal bool HasSelectableLayer()
    {
      return m_hasSelectableLayer;
    }


    #region IExtensionConfig Members

    public string Description
    {
      get
      {
        return "SelectionExtension\r\n" +
            "Copyright � ESRI 2009\r\n\r\n" +
            "This extension is a selection sample extension in C#.";
      }
    }

    /// <summary>
    /// Friendly name shown in the Extension dialog
    /// </summary>
    public string ProductName
    {
      get
      {
        return "Selection Sample Extension";
      }
    }

    public esriExtensionState State
    {
      get
      {
        return m_enableState;
      }
      set
      {
        if (m_enableState != 0 && value == m_enableState)
          return;

        //Check if ok to enable or disable extension
        esriExtensionState requestState = value;
        if (requestState == esriExtensionState.esriESEnabled)
        {
          //Cannot enable if it's already in unavailable state
          if (m_enableState == esriExtensionState.esriESUnavailable)
          {
            throw new COMException("Cannot enable extension");
          }

          //Determine if state can be changed
          esriExtensionState checkState = StateCheck(true);
          m_enableState = checkState;
        }
        else if (requestState == 0 || requestState == esriExtensionState.esriESDisabled)
        {
          //Determine if state can be changed
          esriExtensionState checkState = StateCheck(false);
          if (checkState != m_enableState)
            m_enableState = checkState;
        }

      }
    }

    #endregion

    /// <summary>
    /// Determine extension state 
    /// </summary>
    /// <param name="requestEnable">true if to enable; false to disable</param>
    private esriExtensionState StateCheck(bool requestEnable)
    {
      //TODO: Replace with advanced extension state checking if needed
      //Turn on or off extension directly
      if (requestEnable)
        return esriExtensionState.esriESEnabled;
      else
        return esriExtensionState.esriESDisabled;
    }
  }
}