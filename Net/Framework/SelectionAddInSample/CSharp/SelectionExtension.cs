using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Desktop.AddIns;

namespace SelectionSample
{
  public class SelectionExtension : ESRI.ArcGIS.Desktop.AddIns.Extension
  {
    private IMap m_map;
    private bool m_hasSelectableLayer;
    private static IDockableWindow s_dockWindow;
    private static SelectionExtension s_extension;

    // Overrides

    protected override void OnStartup()
    {
      s_extension = this;

      // Wire up events
      ArcMap.Events.NewDocument += ArcMap_NewOpenDocument;
      ArcMap.Events.OpenDocument += ArcMap_NewOpenDocument;

      Initialize();
    }

    protected override void OnShutdown()
    {
      Uninitialize();
      
      ArcMap.Events.NewDocument -= ArcMap_NewOpenDocument;
      ArcMap.Events.OpenDocument -= ArcMap_NewOpenDocument;

      m_map = null;
      s_dockWindow = null;
      s_extension = null;

      base.OnShutdown();
    }

    protected override bool OnSetState(ExtensionState state)
    {
      // Optionally check for a license here
      this.State = state;

      if (state == ExtensionState.Enabled)
        Initialize();
      else
        Uninitialize();

      return base.OnSetState(state);
    }

    protected override ExtensionState OnGetState()
    {
      return this.State;
    }

    // Privates
    private void Initialize()
    {
      // If the extension hasn't been started yet or it's been turned off, bail
      if (s_extension == null || this.State != ExtensionState.Enabled)
        return;

      // Reset event handlers
      IActiveViewEvents_Event avEvent = ArcMap.Document.FocusMap as IActiveViewEvents_Event;
      avEvent.ItemAdded += AvEvent_ItemAdded;
      avEvent.ItemDeleted += AvEvent_ItemAdded;
      avEvent.SelectionChanged += UpdateSelCountDockWin;
      avEvent.ContentsChanged += avEvent_ContentsChanged;
      
      // Update the UI
      m_map = ArcMap.Document.FocusMap;
      FillComboBox();
      SelCountDockWin.SetEnabled(true);
      UpdateSelCountDockWin();
      m_hasSelectableLayer = CheckForSelectableLayer();
    }

    private void Uninitialize()
    {
      if (s_extension == null)
        return;

      // Detach event handlers
      IActiveViewEvents_Event avEvent = m_map as IActiveViewEvents_Event;
      avEvent.ItemAdded -= AvEvent_ItemAdded;
      avEvent.ItemDeleted -= AvEvent_ItemAdded;
      avEvent.SelectionChanged -= UpdateSelCountDockWin;
      avEvent.ContentsChanged -= avEvent_ContentsChanged;
      avEvent = null;

      // Update UI
      SelectionTargetComboBox selCombo = SelectionTargetComboBox.GetSelectionComboBox();
      if (selCombo != null)
        selCombo.ClearAll();

      SelCountDockWin.SetEnabled(false);
    }

    private void UpdateSelCountDockWin()
    {
      // If the dockview hasn't been created yet
      if (!SelCountDockWin.Exists)
        return; 

      // Update the contents of the listView, when the selection changes in the map. 
      IFeatureLayer featureLayer;
      IFeatureSelection featSel;

      SelCountDockWin.Clear();

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
          SelCountDockWin.AddItem(featureLayer.Name, count);
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
      IMap map = ArcMap.Document.FocusMap;
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

    // Event handlers

    private void ArcMap_NewOpenDocument()
    {
      IActiveViewEvents_Event pageLayoutEvent = ArcMap.Document.PageLayout as IActiveViewEvents_Event;
      pageLayoutEvent.FocusMapChanged += new IActiveViewEvents_FocusMapChangedEventHandler(AVEvents_FocusMapChanged);

      Initialize();
    }

    private void avEvent_ContentsChanged()
    {
      m_hasSelectableLayer = CheckForSelectableLayer();
    }

    private void AvEvent_ItemAdded(object Item)
    {
      m_map = ArcMap.Document.FocusMap;
      FillComboBox();
      UpdateSelCountDockWin();
      m_hasSelectableLayer = CheckForSelectableLayer();
    }

    private void AVEvents_FocusMapChanged()
    {
      Initialize();
    }

    // Statics

    internal static IDockableWindow GetSelectionCountWindow()
    {
      if (s_extension == null)
        GetExtension();

      // Only get/create the dockable window if they ask for it
      if (s_dockWindow == null)
      {
        // Use GetDockableWindow directly intead of FromID as we want the client IDockableWindow not the internal class
        UID dockWinID = new UIDClass();
        dockWinID.Value = ThisAddIn.IDs.SelCountDockWin;
        s_dockWindow = ArcMap.DockableWindowManager.GetDockableWindow(dockWinID);
        s_extension.UpdateSelCountDockWin();
      }

      return s_dockWindow;
    }

    internal static bool IsExtensionEnabled() 
    {
      if (s_extension == null)
        GetExtension();

      if (s_extension == null)
        return false;

      return s_extension.State == ExtensionState.Enabled;
    }

    internal static bool HasSelectableLayer()
    {
      if (s_extension == null)
        GetExtension();

      if (s_extension == null)
        return false;

      return s_extension.m_hasSelectableLayer;
    }

    private static SelectionExtension GetExtension()
    {
      if (s_extension == null)
      {
        // Call FindExtension to load this just-in-time extension.
        UID id = new UIDClass();
        id.Value = ThisAddIn.IDs.SelectionExtension;
        ArcMap.Application.FindExtensionByCLSID(id);
      }

      return s_extension;
    }
  }
}
