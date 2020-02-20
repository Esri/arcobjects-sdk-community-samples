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
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.esriSystem;

namespace GraphicsLayerToolControl
{
  /// <summary>
  /// This user control hosts a combobox which allow the user to control over the active graphics layer 
  /// </summary>
  public partial class GraphicsLayersListCtrl : UserControl
  {
    #region class members
    IMap m_map = null;
    UID m_uid = null;
    #endregion

    #region class constructor
    public GraphicsLayersListCtrl()
    {
      InitializeComponent();

      //initialize the UID that will be used later to get the graphics layers
      m_uid = new UIDClass();
      m_uid.Value = "{34B2EF81-F4AC-11D1-A245-080009B6F22B}"; //graphics layers category      
    }
    #endregion

    /// <summary>
    /// Get the current map and wire the ActiveViewEvents
    /// </summary>
    public IMap Map
    {
      get { return m_map; }
      set 
      {
        m_map = value;
        if (null == m_map)
          return;

        //set verbose events in order to be able to listen to the various 'ItemXXX' events 
        ((IViewManager)m_map).VerboseEvents = true;

        //register document events in order to listen to layers which gets added or removed
        ((IActiveViewEvents_Event)m_map).ItemAdded += new IActiveViewEvents_ItemAddedEventHandler(OnItemAdded);
        ((IActiveViewEvents_Event)m_map).ItemReordered += new IActiveViewEvents_ItemReorderedEventHandler(OnItemReordered);
        ((IActiveViewEvents_Event)m_map).ItemDeleted += new IActiveViewEvents_ItemDeletedEventHandler(OnItemDeleted);

        //populate the combo with a list of the graphics layers
        PopulateCombo();
      }
    }

    /// <summary>
    /// occurs when the user select an item from the combo
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void cmbGraphicsLayerList_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (null == m_map)
        return;

      //get the basic graphics layer from the map
      ILayer activeLayer = m_map.BasicGraphicsLayer as ILayer;
      //if the name of the selected item is the basic graphics layer, make it the active graphics layer
      if (activeLayer.Name == cmbGraphicsLayerList.SelectedItem.ToString())
      {
        m_map.ActiveGraphicsLayer = m_map.BasicGraphicsLayer as ILayer;
        return;
      }
      //iterate through the graphics layers
      IEnumLayer layers = GetGraphicsLayersList();
      if (null == layers)
        return;

      layers.Reset();
      ILayer layer = null;
      while ((layer = layers.Next()) != null)
      {
        if (layer is IGroupLayer)
          continue;

        if (layer is IGraphicsLayer)
        {
          //make the select item the active graphics layer
          if (layer.Name == cmbGraphicsLayerList.SelectedItem.ToString())
            m_map.ActiveGraphicsLayer = layer;
        }
      } 
    }

    /// <summary>
    /// get the list of all graphics layers in the map
    /// </summary>
    /// <returns></returns>
    private IEnumLayer GetGraphicsLayersList()
    {
      IEnumLayer layers = null;
      if (null == m_map || 0 == m_map.LayerCount)
        return null;

      try
      {
        layers = m_map.get_Layers(m_uid, true);
      }
      catch (Exception ex)
      {
        System.Diagnostics.Trace.WriteLine(ex.Message);
        return null;
      }

      return layers;
    }
    
    /// <summary>
    /// list the graphics layers in the combo and select the active graphics layer
    /// </summary>
    private void PopulateCombo()
    {
      if (null == m_map)
        return;

      //clear the items list of the combo
      cmbGraphicsLayerList.Items.Clear();

      //add the basic graphics layer name
      cmbGraphicsLayerList.Items.Add(((ILayer)m_map.BasicGraphicsLayer).Name);

      //get the active graphics layer
      ILayer activeLayer = m_map.ActiveGraphicsLayer;

      //get the list of all graphics layers in the map
      IEnumLayer layers = GetGraphicsLayersList();
      if (null != layers)
      {

        //add the layer names to the combo
        layers.Reset();
        ILayer layer = null;
        while ((layer = layers.Next()) != null)
        {
          cmbGraphicsLayerList.Items.Add(layer.Name);
        }
      }
      //set the selected item to be the active layer
      cmbGraphicsLayerList.SelectedItem = activeLayer.Name;
    }

    /// <summary>
    /// occurs when a layer is being deleted from the map
    /// </summary>
    /// <param name="Item"></param>
    void OnItemDeleted(object Item)
    {
      PopulateCombo();
    }

    /// <summary>
    /// occurs when a layer is being reordered in the TOC
    /// </summary>
    /// <param name="Item"></param>
    /// <param name="toIndex"></param>
    void OnItemReordered(object Item, int toIndex)
    {
      PopulateCombo();
    }

    /// <summary>
    /// occurs when a layer is being added to the map
    /// </summary>
    /// <param name="Item"></param>
    void OnItemAdded(object Item)
    {
      PopulateCombo();
    }
  }
}
