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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;

namespace ESRI.ArcGIS.Samples.SimplePointPlugin
{
  public partial class OpenSimplePointDlg : Form
  {
    #region class memebers
    private IHookHelper m_hookHelper = null;
    IWorkspace m_workspace = null;
    #endregion

    #region class constructor
    public OpenSimplePointDlg(IHookHelper hookHelper)
    {
      if (null == hookHelper)
        throw new Exception("Hook helper is not initialize");

      InitializeComponent();
      
      m_hookHelper = hookHelper;
    }
    #endregion

    #region UI event handlers
    private void btnOpenDataSource_Click(object sender, EventArgs e)
    {
      m_workspace = OpenPlugInWorkspace();

      ListFeatureClasses();
    }

    private void lstDeatureClasses_DoubleClick(object sender, EventArgs e)
    {
      this.Hide();
      OpenDataset();
      this.Close();
    }

    private void btnOK_Click(object sender, EventArgs e)
    {
      this.Hide();
      OpenDataset();
      this.Close();
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      this.Close();
    }
    #endregion

    #region private methods
    private string GetFileName()
    {
      OpenFileDialog dlg = new OpenFileDialog();
      dlg.Filter = "Simple Point (*.csp)|*.csp";
      dlg.Title = "Open Simple Point file";
      dlg.RestoreDirectory = true;
      dlg.CheckPathExists = true;
      dlg.CheckFileExists = true;
      dlg.Multiselect = false;

      DialogResult dr = dlg.ShowDialog();
      if (DialogResult.OK == dr)
        return dlg.FileName;

      return string.Empty;
    }

    private IWorkspace OpenPlugInWorkspace()
    {
      try
      {
        string path = GetFileName();
        if (string.Empty == path)
          return null;

        //update the path textbox
        txtPath.Text = path;
        
        //get the type using the ProgID
        Type t = Type.GetTypeFromProgID("esriGeoDatabase.SimplePointPluginWorkspaceFactory");
        //Use activator in order to create an instance of the workspace factory
        IWorkspaceFactory workspaceFactory = Activator.CreateInstance(t) as IWorkspaceFactory;

        //open the workspace
        return workspaceFactory.OpenFromFile(System.IO.Path.GetDirectoryName(path), 0);
      }
      catch (Exception ex)
      {
        System.Diagnostics.Trace.WriteLine(ex.Message);
        return null;
      }
    }

    private void ListFeatureClasses()
    {
      lstDeatureClasses.Items.Clear();

      if (null == m_workspace)
        return;

      IEnumDatasetName datasetNames = m_workspace.get_DatasetNames(esriDatasetType.esriDTAny);
      datasetNames.Reset();
      IDatasetName dsName;
      while ((dsName = datasetNames.Next()) != null)
      {
        lstDeatureClasses.Items.Add(dsName.Name);
      }

      //select the first dataset on the list
      if (lstDeatureClasses.Items.Count > 0)
      {
        lstDeatureClasses.SelectedIndex = 0;
        lstDeatureClasses.Refresh();
      }
    }

    private void OpenDataset()
    {
      try
      {
        if (null == m_hookHelper || null == m_workspace)
          return;

        if (string.Empty == (string)lstDeatureClasses.SelectedItem)
          return;

        //get the selected item from the listbox
        string dataset = (string)lstDeatureClasses.SelectedItem;

        //cast the workspace into a feature workspace
        IFeatureWorkspace featureWorkspace = m_workspace as IFeatureWorkspace;
        if (null == featureWorkspace)
          return;

        //get a featureclass from the workspace
        IFeatureClass featureClass = featureWorkspace.OpenFeatureClass(dataset);

        //create a new feature layer and add it to the map
        IFeatureLayer featureLayer = new FeatureLayerClass();
        featureLayer.Name = featureClass.AliasName;
        featureLayer.FeatureClass = featureClass;
        m_hookHelper.FocusMap.AddLayer((ILayer)featureLayer);
      }
      catch (Exception ex)
      {
        System.Diagnostics.Trace.WriteLine(ex.Message);
      }
    }
    #endregion
  }
}