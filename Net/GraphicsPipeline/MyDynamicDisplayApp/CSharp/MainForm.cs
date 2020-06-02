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
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Runtime.InteropServices;

using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.SystemUI;

namespace MyDynamicDisplayApp
{
  public sealed partial class MainForm : Form
  {
    #region class private members
    private IMapControl3 m_mapControl = null;
    private string m_mapDocumentName = string.Empty;
    #endregion

    #region class constructor
    public MainForm()
    {
      InitializeComponent();
    }
    #endregion

    private void MainForm_Load(object sender, EventArgs e)
    {
      //get the MapControl
      m_mapControl = (IMapControl3)axMapControl1.Object;

      //disable the Save menu (since there is no document yet)
      menuSaveDoc.Enabled = false;

      axToolbarControl1.AddItem(new ToggleDynamicDisplayCmd());
      axToolbarControl1.AddItem(new LoadDynamicLayerCmd());
    }

    #region Main Menu event handlers
    private void menuNewDoc_Click(object sender, EventArgs e)
    {
      //execute New Document command
      ICommand command = new CreateNewDocument();
      command.OnCreate(m_mapControl.Object);
      command.OnClick();
    }

    private void menuOpenDoc_Click(object sender, EventArgs e)
    {
      //execute Open Document command
      ICommand command = new ControlsOpenDocCommandClass();
      command.OnCreate(m_mapControl.Object);
      command.OnClick();
    }

    private void menuSaveDoc_Click(object sender, EventArgs e)
    {
      //execute Save Document command
      if (m_mapControl.CheckMxFile(m_mapDocumentName))
      {
        //create a new instance of a MapDocument
        IMapDocument mapDoc = new MapDocumentClass();
        mapDoc.Open(m_mapDocumentName, string.Empty);

        //Make sure that the MapDocument is not readonly
        if (mapDoc.get_IsReadOnly(m_mapDocumentName))
        {
          MessageBox.Show("Map document is read only!");
          mapDoc.Close();
          return;
        }

        //Replace its contents with the current map
        mapDoc.ReplaceContents((IMxdContents)m_mapControl.Map);

        //save the MapDocument in order to persist it
        mapDoc.Save(mapDoc.UsesRelativePaths, false);

        //close the MapDocument
        mapDoc.Close();
      }
    }

    private void menuSaveAs_Click(object sender, EventArgs e)
    {
      //execute SaveAs Document command
      ICommand command = new ControlsSaveAsDocCommandClass();
      command.OnCreate(m_mapControl.Object);
      command.OnClick();
    }

    private void menuExitApp_Click(object sender, EventArgs e)
    {
      //exit the application
      Application.Exit();
    }
    #endregion

    //listen to MapReplaced event in order to update the status bar and the Save menu
    private void axMapControl1_OnMapReplaced(object sender, IMapControlEvents2_OnMapReplacedEvent e)
    {
      //get the current document name from the MapControl
      m_mapDocumentName = m_mapControl.DocumentFilename;

      //if there is no MapDocument, disable the Save menu and clear the status bar
      if (m_mapDocumentName == string.Empty)
      {
        menuSaveDoc.Enabled = false;
        statusBarXY.Text = string.Empty;
      }
      else
      {
          //enable the Save menu and write the doc name to the status bar
        menuSaveDoc.Enabled = true;
        statusBarXY.Text = Path.GetFileName(m_mapDocumentName);
      }
    }

    private void axMapControl1_OnMouseMove(object sender, IMapControlEvents2_OnMouseMoveEvent e)
    {
      statusBarXY.Text = string.Format("{0}, {1}  {2}", e.mapX.ToString("#######.##"), e.mapY.ToString("#######.##"), axMapControl1.MapUnits.ToString().Substring(4));
    }
  }
}