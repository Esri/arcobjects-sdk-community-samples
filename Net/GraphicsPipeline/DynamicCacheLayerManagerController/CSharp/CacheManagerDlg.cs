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
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.SystemUI;

namespace DynamicCacheLayerManagerController
{
  public partial class CacheManagerDlg : Form, IDisposable
  {
    #region dialog class members
    public struct LayerCacheInfo
    {
      public string name;
      public string folderName;
      public string folderPath;
      public string format;
      public bool alwaysDrawCoarsestLevel;
      public bool strictOnDemand;
      public int progressiveDrawingLevels;
      public int progressiveFetchingLevels;
      public double detailsThreshold;
      public double maxCacheScale;

      // override ToString method in order to show only the layer name in the combo itself
      public override string ToString()
      {
        return name;
      }
    }
    
    
    private IHookHelper m_hookHelper = null;

    private Dictionary<string, LayerCacheInfo> m_layerCacheInfos = new Dictionary<string, LayerCacheInfo>();
    #endregion

    #region dialog constructor
    public CacheManagerDlg(IHookHelper hookHelper)
    {
      m_hookHelper = hookHelper;

      InitializeComponent();
    }
    #endregion

    #region private methods

    private void CacheManagerDlg_Load(object sender, EventArgs e)
    {
      InitializeForm();
    }

    private void cboLayerNames_SelectedIndexChanged(object sender, EventArgs e)
    {
      // get the layers from the map
      if (m_hookHelper.FocusMap.LayerCount == 0)
        return;

      IMap map = m_hookHelper.FocusMap;

      IDynamicCacheLayerManager dynamicCacheLayerManager = new DynamicCacheLayerManagerClass();

      // get all of the non-dynamic layers
      ILayer layer;
      for (int i = 0; i < map.LayerCount; i++)
      {
        layer = map.get_Layer(i);
        if (layer is IDynamicLayer)
          continue;

        dynamicCacheLayerManager.Init(map, layer);
        LayerCacheInfo layerInfo = (LayerCacheInfo)cboLayerNames.SelectedItem;
        if (dynamicCacheLayerManager.FolderName == layerInfo.folderName)
        {
          // populate the form
          lblCacheFolderName.Text = dynamicCacheLayerManager.FolderName;
          lblCacheFolderPath.Text = dynamicCacheLayerManager.FolderPath;
          if (dynamicCacheLayerManager.Format.ToUpper().IndexOf("PNG") > -1)
            rdoPNG.Checked = true;
          else
            rdoJPEG.Checked = true;

          chkAlwaysDrawCoarsestLevel.Checked = dynamicCacheLayerManager.AlwaysDrawCoarsestLevel;
          numDetaildThreshold.Value = Convert.ToDecimal(dynamicCacheLayerManager.DetailsThreshold);
          chkStrictOnDemandMode.Checked = dynamicCacheLayerManager.StrictOnDemandMode;
          numProgressiveDrawingLevels.Value = dynamicCacheLayerManager.ProgressiveDrawingLevels;
          numProgressiveFetchingLevels.Value = dynamicCacheLayerManager.ProgressiveFetchingLevels;
          numMaxCacheScale.Value = Convert.ToDecimal(dynamicCacheLayerManager.MaxCacheScale);
          return;
        }
      }
    }

    private void btnFolderPath_Click(object sender, EventArgs e)
    {
      FolderBrowserDialog dlg = new FolderBrowserDialog();
      dlg.ShowNewFolderButton = true;
      dlg.Description = "Cache Folder";

      if (cboLayerNames.SelectedIndex != -1)
      {
        LayerCacheInfo layerInfo = (LayerCacheInfo)cboLayerNames.SelectedItem;
        dlg.SelectedPath = layerInfo.folderPath;
      }

      if (DialogResult.OK == dlg.ShowDialog())
      {
        if (System.IO.Directory.Exists(dlg.SelectedPath))
          lblCacheFolderPath.Text = dlg.SelectedPath;
      }
    }

    private void btnRestoreDefaults_Click(object sender, EventArgs e)
    {
      // get the selected item
      if (cboLayerNames.SelectedIndex == -1)
        return;

      LayerCacheInfo layerInfo = (LayerCacheInfo)cboLayerNames.SelectedItem;
      if (m_layerCacheInfos.ContainsKey(layerInfo.folderName))
      {
        layerInfo = m_layerCacheInfos[layerInfo.folderName];
        // populate the form
        lblCacheFolderName.Text = layerInfo.folderName;
        lblCacheFolderPath.Text = layerInfo.folderPath;
        if (layerInfo.format.ToUpper().IndexOf("PNG") > -1)
          rdoPNG.Checked = true;
        else
          rdoJPEG.Checked = true;

        chkAlwaysDrawCoarsestLevel.Checked = layerInfo.alwaysDrawCoarsestLevel;
        chkStrictOnDemandMode.Checked = layerInfo.strictOnDemand;
        numDetaildThreshold.Value = Convert.ToDecimal(layerInfo.detailsThreshold);
        numProgressiveDrawingLevels.Value = layerInfo.progressiveDrawingLevels;
        numProgressiveFetchingLevels.Value = layerInfo.progressiveFetchingLevels;
        numMaxCacheScale.Value = Convert.ToDecimal(layerInfo.maxCacheScale);
      }


    }

    private void btnDismiss_Click(object sender, EventArgs e)
    {
      Close();
    }

    private void btnApply_Click(object sender, EventArgs e)
    {
      UpdateLayer();
    }

    private void btnOK_Click(object sender, EventArgs e)
    {
      UpdateLayer();
      Close();
    }

    private void GetCacheDefaultsProps()
    {
      m_layerCacheInfos.Clear();

      if (m_hookHelper.FocusMap.LayerCount == 0)
        return;

      IMap map = m_hookHelper.FocusMap;

      IDynamicCacheLayerManager dynamicCacheLayerManager = new DynamicCacheLayerManagerClass();

      // get all of the non-dynamic layers
      ILayer layer;
      for (int i = 0; i < map.LayerCount; i++)
      {
        layer = map.get_Layer(i);
        if (layer is IDynamicLayer)
          continue;

        dynamicCacheLayerManager.Init(map, layer);
        LayerCacheInfo layerCacheInfo;
        layerCacheInfo.name                       = layer.Name;
        layerCacheInfo.folderName                 = dynamicCacheLayerManager.FolderName;
        layerCacheInfo.folderPath                 = dynamicCacheLayerManager.FolderPath;
        layerCacheInfo.format                     = dynamicCacheLayerManager.Format;
        layerCacheInfo.detailsThreshold           = dynamicCacheLayerManager.DetailsThreshold;
        layerCacheInfo.alwaysDrawCoarsestLevel    = dynamicCacheLayerManager.AlwaysDrawCoarsestLevel;
        layerCacheInfo.progressiveDrawingLevels   = dynamicCacheLayerManager.ProgressiveDrawingLevels;
        layerCacheInfo.progressiveFetchingLevels  = dynamicCacheLayerManager.ProgressiveFetchingLevels;
        layerCacheInfo.strictOnDemand             = dynamicCacheLayerManager.StrictOnDemandMode;
        layerCacheInfo.maxCacheScale              = dynamicCacheLayerManager.MaxCacheScale;

        m_layerCacheInfos.Add(layerCacheInfo.folderName, layerCacheInfo);
      }      
    }

    private void InitializeForm()
    {
      GetCacheDefaultsProps();
      
      if (m_layerCacheInfos.Count == 0)
      {
        // clear the form
        cboLayerNames.Items.Clear();
        cboLayerNames.SelectedIndex = -1;

        lblCacheFolderPath.Text = string.Empty;
        btnFolderPath.Enabled = false;
        numProgressiveDrawingLevels.Value = 0;
        numProgressiveFetchingLevels.Value = 0;
        numMaxCacheScale.Value = 0;

        groupDrawingProps.Enabled = false;
        btnOK.Enabled = false;
        btnApply.Enabled = false;

        return;
      }

      groupDrawingProps.Enabled = true;
      btnFolderPath.Enabled = true;
      btnOK.Enabled = true;
      btnApply.Enabled = true;

      string selectedLayerName = string.Empty;
      LayerCacheInfo layerInfo;
      layerInfo.name = string.Empty;
      layerInfo.folderPath = string.Empty;
      layerInfo.folderName = string.Empty;
      layerInfo.format = string.Empty;
      layerInfo.alwaysDrawCoarsestLevel = false;
      layerInfo.detailsThreshold = 0;
      layerInfo.strictOnDemand = false;
      layerInfo.progressiveDrawingLevels = 0;
      layerInfo.progressiveFetchingLevels = 0;
      layerInfo.maxCacheScale = 0;

      int selectedIndex = cboLayerNames.SelectedIndex;
      if (selectedIndex != -1)
      {
        selectedLayerName = ((LayerCacheInfo)cboLayerNames.SelectedItem).folderName;
        if (m_layerCacheInfos.ContainsKey(selectedLayerName))
          layerInfo = m_layerCacheInfos[selectedLayerName];
      }

      // populate the combo
      cboLayerNames.Items.Clear();
      int index = 0;
      bool bFirst = true;
      foreach (KeyValuePair<string, LayerCacheInfo> layerCacheInfoPair in m_layerCacheInfos)
      {
        cboLayerNames.Items.Add(layerCacheInfoPair.Value);

        if (bFirst && layerInfo.name == string.Empty)
        {
          layerInfo = layerCacheInfoPair.Value;
          cboLayerNames.SelectedIndex = 0;
          bFirst = false;
        }

        if (selectedLayerName == layerCacheInfoPair.Key)
        {
          // make it the selected item
          cboLayerNames.SelectedIndex = index;
        }
        index++;
      }

      // populate the form
      lblCacheFolderName.Text = layerInfo.folderName;
      lblCacheFolderPath.Text = layerInfo.folderPath;
      if (layerInfo.format.ToUpper().IndexOf("PNG")  > -1)
        rdoPNG.Checked = true;
      else
        rdoJPEG.Checked = true;

      chkAlwaysDrawCoarsestLevel.Checked = layerInfo.alwaysDrawCoarsestLevel;
      chkStrictOnDemandMode.Checked = layerInfo.strictOnDemand;
      numDetaildThreshold.Value = Convert.ToDecimal(layerInfo.detailsThreshold);
      numProgressiveDrawingLevels.Value = layerInfo.progressiveDrawingLevels;
      numProgressiveFetchingLevels.Value = layerInfo.progressiveFetchingLevels;      
      numMaxCacheScale.Value = Convert.ToDecimal(layerInfo.maxCacheScale);
    }

    private void UpdateLayer()
    {
      // get the selected layer from the map
      if (m_hookHelper.FocusMap.LayerCount == 0)
        return;

      if (cboLayerNames.SelectedIndex == -1)
        return;

      IMap map = m_hookHelper.FocusMap;

      IDynamicCacheLayerManager dynamicCacheLayerManager = new DynamicCacheLayerManagerClass();

      // get all of the non-dynamic layers
      ILayer layer;
      for (int i = 0; i < map.LayerCount; i++)
      {
        layer = map.get_Layer(i);
        if (layer is IDynamicLayer)
          continue;

        dynamicCacheLayerManager.Init(map, layer);
        LayerCacheInfo layerInfo = (LayerCacheInfo)cboLayerNames.SelectedItem;
        if (dynamicCacheLayerManager.FolderName == layerInfo.folderName)
        {
          dynamicCacheLayerManager.FolderPath = lblCacheFolderPath.Text;
          dynamicCacheLayerManager.Format = (rdoPNG.Checked ? "PNG32" : "JPEG32");
          dynamicCacheLayerManager.StrictOnDemandMode = chkStrictOnDemandMode.Checked;
          dynamicCacheLayerManager.AlwaysDrawCoarsestLevel = chkAlwaysDrawCoarsestLevel.Checked;
          dynamicCacheLayerManager.DetailsThreshold = Convert.ToDouble(numDetaildThreshold.Value);
          dynamicCacheLayerManager.ProgressiveDrawingLevels = Convert.ToInt32(numProgressiveDrawingLevels.Value);
          dynamicCacheLayerManager.ProgressiveFetchingLevels = Convert.ToInt32(numProgressiveFetchingLevels.Value);
          dynamicCacheLayerManager.MaxCacheScale = Convert.ToDouble(numMaxCacheScale.Value);
        }
      }
    }

    #endregion
  }
}