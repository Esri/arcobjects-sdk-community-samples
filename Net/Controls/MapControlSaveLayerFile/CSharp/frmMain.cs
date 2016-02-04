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
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS;


namespace MapControlSaveLayerFile
{
  /// <summary>
  /// Summary description for frmMain.
  /// </summary>
  public class frmMain : System.Windows.Forms.Form
  {

    #region GUI element members
    private ESRI.ArcGIS.Controls.AxLicenseControl axLicenseControl1;
    private ESRI.ArcGIS.Controls.AxMapControl axMapControl1;
    private ESRI.ArcGIS.Controls.AxTOCControl axTOCControl1;
    private ESRI.ArcGIS.Controls.AxToolbarControl axToolbarControl1;
    private System.Windows.Forms.Splitter splitter1;
    private System.Windows.Forms.StatusBar statusBar1;
    private System.Windows.Forms.MainMenu mainMenu1;
    private System.Windows.Forms.MenuItem menuFile;
    private System.Windows.Forms.MenuItem menuNewDoc;
    private System.Windows.Forms.MenuItem menuOpenDoc;
    private System.Windows.Forms.MenuItem menuSaveDoc;
    private System.Windows.Forms.MenuItem menuSaveAsDoc;
    private System.Windows.Forms.MenuItem menuSeparator;
    private System.Windows.Forms.MenuItem menuExitApp;
    #endregion

    //private class members
    private IMapControl3 m_mapControl = null;
    private string m_mapDocumentName = string.Empty;
    private StatusBarPanel statusBarXY;
    private IContainer components;
    private ContextMenuClass m_contextMenu = null;

    public frmMain()
    {
      //
      // Required for Windows Form Designer support
      //
      InitializeComponent();

      //
      // TODO: Add any constructor code after InitializeComponent call
      //
    }

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    protected override void Dispose(bool disposing)
    {
      //Ensures that any ESRI libraries that have been used are unloaded in the correct order. 
      //Failure to do this may result in random crashes on exit due to the operating system unloading 
      //the libraries in the incorrect order. 
      ESRI.ArcGIS.ADF.COMSupport.AOUninitialize.Shutdown();

      if (disposing)
      {
        if (components != null)
        {
          components.Dispose();
        }
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code
    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
      this.statusBar1 = new System.Windows.Forms.StatusBar();
      this.statusBarXY = new System.Windows.Forms.StatusBarPanel();
      this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
      this.menuFile = new System.Windows.Forms.MenuItem();
      this.menuNewDoc = new System.Windows.Forms.MenuItem();
      this.menuOpenDoc = new System.Windows.Forms.MenuItem();
      this.menuSaveDoc = new System.Windows.Forms.MenuItem();
      this.menuSaveAsDoc = new System.Windows.Forms.MenuItem();
      this.menuSeparator = new System.Windows.Forms.MenuItem();
      this.menuExitApp = new System.Windows.Forms.MenuItem();
      this.axLicenseControl1 = new ESRI.ArcGIS.Controls.AxLicenseControl();
      this.axMapControl1 = new ESRI.ArcGIS.Controls.AxMapControl();
      this.axTOCControl1 = new ESRI.ArcGIS.Controls.AxTOCControl();
      this.axToolbarControl1 = new ESRI.ArcGIS.Controls.AxToolbarControl();
      this.splitter1 = new System.Windows.Forms.Splitter();
      ((System.ComponentModel.ISupportInitialize)(this.statusBarXY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.axMapControl1)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.axTOCControl1)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.axToolbarControl1)).BeginInit();
      this.SuspendLayout();
      // 
      // statusBar1
      // 
      this.statusBar1.Location = new System.Drawing.Point(0, 512);
      this.statusBar1.Name = "statusBar1";
      this.statusBar1.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
            this.statusBarXY});
      this.statusBar1.ShowPanels = true;
      this.statusBar1.Size = new System.Drawing.Size(784, 22);
      this.statusBar1.TabIndex = 4;
      // 
      // statusBarXY
      // 
      this.statusBarXY.Name = "statusBarXY";
      this.statusBarXY.Width = 210;
      // 
      // mainMenu1
      // 
      this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuFile});
      // 
      // menuFile
      // 
      this.menuFile.Index = 0;
      this.menuFile.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuNewDoc,
            this.menuOpenDoc,
            this.menuSaveDoc,
            this.menuSaveAsDoc,
            this.menuSeparator,
            this.menuExitApp});
      this.menuFile.Text = "File";
      // 
      // menuNewDoc
      // 
      this.menuNewDoc.Index = 0;
      this.menuNewDoc.Text = "New Document";
      this.menuNewDoc.Click += new System.EventHandler(this.menuNewDoc_Click);
      // 
      // menuOpenDoc
      // 
      this.menuOpenDoc.Index = 1;
      this.menuOpenDoc.Text = "Open Document...";
      this.menuOpenDoc.Click += new System.EventHandler(this.menuOpenDoc_Click);
      // 
      // menuSaveDoc
      // 
      this.menuSaveDoc.Index = 2;
      this.menuSaveDoc.Text = "SaveDocuement";
      this.menuSaveDoc.Click += new System.EventHandler(this.menuSaveDoc_Click);
      // 
      // menuSaveAsDoc
      // 
      this.menuSaveAsDoc.Index = 3;
      this.menuSaveAsDoc.Text = "Save As...";
      this.menuSaveAsDoc.Click += new System.EventHandler(this.menuSaveAsDoc_Click);
      // 
      // menuSeparator
      // 
      this.menuSeparator.Index = 4;
      this.menuSeparator.Text = "-";
      // 
      // menuExitApp
      // 
      this.menuExitApp.Index = 5;
      this.menuExitApp.Text = "Exit";
      this.menuExitApp.Click += new System.EventHandler(this.menuExitApp_Click);
      // 
      // axLicenseControl1
      // 
      this.axLicenseControl1.Enabled = true;
      this.axLicenseControl1.Location = new System.Drawing.Point(320, 176);
      this.axLicenseControl1.Name = "axLicenseControl1";
      this.axLicenseControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axLicenseControl1.OcxState")));
      this.axLicenseControl1.Size = new System.Drawing.Size(32, 32);
      this.axLicenseControl1.TabIndex = 5;
      // 
      // axMapControl1
      // 
      this.axMapControl1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.axMapControl1.Location = new System.Drawing.Point(0, 28);
      this.axMapControl1.Name = "axMapControl1";
      this.axMapControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axMapControl1.OcxState")));
      this.axMapControl1.Size = new System.Drawing.Size(784, 484);
      this.axMapControl1.TabIndex = 6;
      this.axMapControl1.OnMouseMove += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMouseMoveEventHandler(this.axMapControl1_OnMouseMove);
      this.axMapControl1.OnMapReplaced += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMapReplacedEventHandler(this.axMapControl1_OnMapReplaced);
      // 
      // axTOCControl1
      // 
      this.axTOCControl1.Dock = System.Windows.Forms.DockStyle.Left;
      this.axTOCControl1.Location = new System.Drawing.Point(3, 28);
      this.axTOCControl1.Name = "axTOCControl1";
      this.axTOCControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axTOCControl1.OcxState")));
      this.axTOCControl1.Size = new System.Drawing.Size(209, 484);
      this.axTOCControl1.TabIndex = 7;
      this.axTOCControl1.OnMouseDown += new ESRI.ArcGIS.Controls.ITOCControlEvents_Ax_OnMouseDownEventHandler(this.axTOCControl1_OnMouseDown);
      // 
      // axToolbarControl1
      // 
      this.axToolbarControl1.Dock = System.Windows.Forms.DockStyle.Top;
      this.axToolbarControl1.Location = new System.Drawing.Point(0, 0);
      this.axToolbarControl1.Name = "axToolbarControl1";
      this.axToolbarControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axToolbarControl1.OcxState")));
      this.axToolbarControl1.Size = new System.Drawing.Size(784, 28);
      this.axToolbarControl1.TabIndex = 8;
      // 
      // splitter1
      // 
      this.splitter1.Location = new System.Drawing.Point(0, 28);
      this.splitter1.Name = "splitter1";
      this.splitter1.Size = new System.Drawing.Size(3, 484);
      this.splitter1.TabIndex = 9;
      this.splitter1.TabStop = false;
      // 
      // frmMain
      // 
      this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
      this.ClientSize = new System.Drawing.Size(784, 534);
      this.Controls.Add(this.axTOCControl1);
      this.Controls.Add(this.axLicenseControl1);
      this.Controls.Add(this.splitter1);
      this.Controls.Add(this.axMapControl1);
      this.Controls.Add(this.axToolbarControl1);
      this.Controls.Add(this.statusBar1);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Menu = this.mainMenu1;
      this.Name = "frmMain";
      this.Text = "ArcEngine Controls Application";
      this.Load += new System.EventHandler(this.frmMain_Load);
      ((System.ComponentModel.ISupportInitialize)(this.statusBarXY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.axMapControl1)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.axTOCControl1)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.axToolbarControl1)).EndInit();
      this.ResumeLayout(false);

    }
    #endregion

    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        
        if (!RuntimeManager.Bind(ProductCode.Engine))
        {
            if (!RuntimeManager.Bind(ProductCode.Desktop))
            {
                MessageBox.Show("Unable to bind to ArcGIS runtime. Application will be shut down.");
                return;
            }
        }

        Application.Run(new frmMain());
    }

    private void frmMain_Load(object sender, System.EventArgs e)
    {
      //get the MapControl
      m_mapControl = (IMapControl3)axMapControl1.Object;

      //disable the Save menu (since there is no document yet)
      menuSaveDoc.Enabled = false;

      m_contextMenu = new ContextMenuClass();
      m_contextMenu.SetHook(axMapControl1.Object);

      //add the load layer file command to the map
      axToolbarControl1.AddItem(new LoadLayerFileCmd(), -1, 2, false, -1, esriCommandStyles.esriCommandStyleIconOnly);

      m_contextMenu.ContextMenu.AddItem(new SaveLayerFileCmd(), -1, -1, false, esriCommandStyles.esriCommandStyleIconAndText);
      m_contextMenu.ContextMenu.AddItem(new RemoveLayerCmd(), -1, -1, false, esriCommandStyles.esriCommandStyleIconAndText);
    }

    #region Main Menu event handlers
    private void menuExitApp_Click(object sender, System.EventArgs e)
    {
      //exit the application
      Application.Exit();
    }

    private void menuNewDoc_Click(object sender, System.EventArgs e)
    {
      //execute New Document command
      ICommand command = new CreateNewDocument();
      command.OnCreate(m_mapControl.Object);
      command.OnClick();
    }

    private void menuOpenDoc_Click(object sender, System.EventArgs e)
    {
      //execute Open Document command
      ICommand command = new ControlsOpenDocCommandClass();
      command.OnCreate(m_mapControl.Object);
      command.OnClick();
    }

    private void menuSaveDoc_Click(object sender, System.EventArgs e)
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

    private void menuSaveAsDoc_Click(object sender, System.EventArgs e)
    {
      //execute SaveAs Document command
      ICommand command = new ControlsSaveAsDocCommandClass();
      command.OnCreate(m_mapControl.Object);
      command.OnClick();
    }
    #endregion

    //listen to MapReplaced event in order to update the statusbar and the Save menu
    private void axMapControl1_OnMapReplaced(object sender, ESRI.ArcGIS.Controls.IMapControlEvents2_OnMapReplacedEvent e)
    {
      //get the current document name from the MapControl
      m_mapDocumentName = m_mapControl.DocumentFilename;

      //if there is no MapDocument, disable the Save menu and clear the statusbar
      if (m_mapDocumentName == string.Empty)
      {
        menuSaveDoc.Enabled = false;
        statusBar1.Text = string.Empty;
      }
      else
      {
        //enable the Save menu and write the doc name to the statusbar
        menuSaveDoc.Enabled = true;
        statusBar1.Text = Path.GetFileName(m_mapDocumentName);
      }
    }

    private void axMapControl1_OnMouseMove(object sender, IMapControlEvents2_OnMouseMoveEvent e)
    {
      statusBarXY.Text = string.Format("{0}, {1}  {2}", e.mapX.ToString("#######.##"), e.mapY.ToString("#######.##"), axMapControl1.MapUnits.ToString().Substring(4));
    }

    private void axTOCControl1_OnMouseDown(object sender, ITOCControlEvents_OnMouseDownEvent e)
    {
      //make sure that the user right clicked
      if (2 != e.button)
        return;

      //use HitTest in order to test whether the user has selected a featureLayer
      esriTOCControlItem item = esriTOCControlItem.esriTOCControlItemNone;
      IBasicMap map = null; ILayer layer = null;
      object other = null; object index = null;

      //do the HitTest
      axTOCControl1.HitTest(e.x, e.y, ref item, ref map, ref layer, ref other, ref index);

      //Determine what kind of item has been clicked on
      if (null == layer || !(layer is IFeatureLayer))
        return;

      //set the featurelayer as the custom property of the MapControl
      axMapControl1.CustomProperty = layer;

      //popup a context menu with a 'Properties' command
      m_contextMenu.PopupMenu(e.x, e.y, axTOCControl1.hWnd);
    }
  }
}
