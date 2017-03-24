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
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesGDB;

namespace DemoITableBinding
{
  /// <summary>
  /// This class demonstrates how to bind an ITable to Windows Forms controls.
  /// </summary>
  /// <remarks>
  /// The form this class defines contains a DataGridView, a TextBox and a 
  /// BindingNavigator. The class will load the test data and bind it to both
  /// the TextBox and the DataGridView by using a BindingSource. The use of a
  /// BindingSource allows us to bind the test data to a single object that will
  /// act as a datasource for the grid, the text box and the navigation control.
  /// This allows changes made in one control to be reflected in the other controls
  /// with minimal coding effort. This is most clearly demonstrated by using the
  /// navigation control to step backwards and forwards through the grid rows
  /// while also showing the value of a single field in the current row in the
  /// text box.
  /// </remarks>
  public partial class MainWnd : Form
  {
    #region Private Memebers
    /// <summary>
    /// This is used to bind our ITable to the binding source. We need to keep
    /// a reference to it as we will need to re-attach it to the binding source
    /// to force a refresh whenever we change from displaying coded value domain
    /// values to displaying their text equivalents and vice versa.
    /// </summary>
    private ArcDataBinding.TableWrapper tableWrapper;
    
    /// <summary>
    /// This binding object sets the data member within the data source for the 
    /// text box. We need to keep a reference to this as it needs to be reset
    /// whenever viewing of coded value domains is changed.
    /// </summary>
    private Binding txtBoxBinding;
    #endregion Private Memebers

    #region Construction/Destruction
    public MainWnd()
    {
      InitializeComponent();
    }
    #endregion Construction/Destruction

    #region Private Event Handlers
    private void MainWnd_Load(object sender, EventArgs e)
    {
      // Get workspace and open mdb file
      IWorkspaceFactory2 wkspcFactory = (IWorkspaceFactory2)new FileGDBWorkspaceFactoryClass();
            string filePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        filePath = System.IO.Path.Combine(filePath, @"ArcGIS\data\SanFrancisco\SanFrancisco.gdb");
      IFeatureWorkspace wkspc = wkspcFactory.OpenFromFile(filePath, Handle.ToInt32()) as IFeatureWorkspace;

      //open the Geodatabase table
      ITable foundITable = wkspc.OpenFeatureClass("Parks") as ITable;

      if (null != foundITable)
      {
        // Bind dataset to the binding source
        tableWrapper = new ArcDataBinding.TableWrapper(foundITable);
        bindingSource1.DataSource = tableWrapper;

        // Bind binding source to grid. Alternatively it is possible to bind TableWrapper
        // directly to the grid to this offers less flexibility
        dataGridView1.DataSource = bindingSource1;

        // Bind binding source to text box, we are binding the NAME
        // field.
        txtBoxBinding = new Binding("Text", bindingSource1, "NAME");
        textBox1.DataBindings.Add(txtBoxBinding);
      }
    }

    private void chkUseCVD_CheckedChanged(object sender, EventArgs e)
    {
      // Set usage of CV domain on or off
      tableWrapper.UseCVDomains = chkUseCVD.Checked;

      // Refresh the binding source by setting it to null and then rebinding
      // to the data. This will refresh all the field types and ensures that all
      // the fields are using the correct type converters (we will need different
      // type converters depending on whether we are showing cv domain values or
      // their text equivalents). Note that as we will be setting the binding source
      // to null, the text box binding will fail as the "FULL_NAME" field will no
      // longer be present. To prevent any problems here, we need to remove and
      // reset the text box's binding.
      textBox1.DataBindings.Clear();
      bindingSource1.DataSource = null;
      bindingSource1.DataSource = tableWrapper;
      textBox1.DataBindings.Add(txtBoxBinding);
      dataGridView1.Refresh();
    } 
    #endregion Private Event Handlers
  }
}