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
using System.IO;
using System.Runtime.InteropServices;

using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS;

namespace MapAndPageLayoutSynchApp
{
  /// <summary>
  /// Summary description for frmMain.
  /// </summary>
  public class frmMain : System.Windows.Forms.Form
  {
    #region GUI elements

    private System.Windows.Forms.StatusBar statusBar1;
    private System.Windows.Forms.StatusBarPanel statusBarXYUnits;
    private System.Windows.Forms.MainMenu mainMenu1;
    private System.Windows.Forms.MenuItem menuFile;
    private System.Windows.Forms.MenuItem menuOpenDoc;
    private System.Windows.Forms.MenuItem menuNewDoc;
    private System.Windows.Forms.MenuItem menuSaveDoc;
    private System.Windows.Forms.MenuItem menuSaveAsDoc;
    private System.Windows.Forms.MenuItem menuSeparator;
    private System.Windows.Forms.MenuItem menuAppExit;
    private ESRI.ArcGIS.Controls.AxToolbarControl axToolbarControl1;
    private ESRI.ArcGIS.Controls.AxTOCControl axTOCControl1;
    private System.Windows.Forms.TabControl tabControl1;
    private System.Windows.Forms.TabPage tabPage1;
    private System.Windows.Forms.TabPage tabPage2;
    private ESRI.ArcGIS.Controls.AxToolbarControl axToolbarControl2;
    private ESRI.ArcGIS.Controls.AxMapControl axMapControl1;
    private ESRI.ArcGIS.Controls.AxPageLayoutControl axPageLayoutControl1;
    private System.Windows.Forms.Splitter splitter1;
    #endregion

    private ESRI.ArcGIS.Controls.IMapControl3 m_mapControl = null;
    private ESRI.ArcGIS.Controls.IPageLayoutControl2 m_pageLayoutControl = null;
    private ControlsSynchronizer m_controlsSynchronizer = null;
    private AxLicenseControl axLicenseControl1;
    private IContainer components;
    private string m_documentFileName = string.Empty;

    public frmMain()
    {
      InitializeComponent();
    }

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    protected override void Dispose(bool disposing)
    {
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

    #region Windows Form Designer generated code
    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
      this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
      this.menuFile = new System.Windows.Forms.MenuItem();
      this.menuOpenDoc = new System.Windows.Forms.MenuItem();
      this.menuNewDoc = new System.Windows.Forms.MenuItem();
      this.menuSaveDoc = new System.Windows.Forms.MenuItem();
      this.menuSaveAsDoc = new System.Windows.Forms.MenuItem();
      this.menuSeparator = new System.Windows.Forms.MenuItem();
      this.menuAppExit = new System.Windows.Forms.MenuItem();
      this.statusBar1 = new System.Windows.Forms.StatusBar();
      this.statusBarXYUnits = new System.Windows.Forms.StatusBarPanel();
      this.axToolbarControl1 = new ESRI.ArcGIS.Controls.AxToolbarControl();
      this.axTOCControl1 = new ESRI.ArcGIS.Controls.AxTOCControl();
      this.tabControl1 = new System.Windows.Forms.TabControl();
      this.tabPage1 = new System.Windows.Forms.TabPage();
      this.axLicenseControl1 = new ESRI.ArcGIS.Controls.AxLicenseControl();
      this.axMapControl1 = new ESRI.ArcGIS.Controls.AxMapControl();
      this.tabPage2 = new System.Windows.Forms.TabPage();
      this.axPageLayoutControl1 = new ESRI.ArcGIS.Controls.AxPageLayoutControl();
      this.axToolbarControl2 = new ESRI.ArcGIS.Controls.AxToolbarControl();
      this.splitter1 = new System.Windows.Forms.Splitter();
      ((System.ComponentModel.ISupportInitialize)(this.statusBarXYUnits)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.axToolbarControl1)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.axTOCControl1)).BeginInit();
      this.tabControl1.SuspendLayout();
      this.tabPage1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.axMapControl1)).BeginInit();
      this.tabPage2.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.axPageLayoutControl1)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.axToolbarControl2)).BeginInit();
      this.SuspendLayout();
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
            this.menuOpenDoc,
            this.menuNewDoc,
            this.menuSaveDoc,
            this.menuSaveAsDoc,
            this.menuSeparator,
            this.menuAppExit});
      this.menuFile.Text = "File";
      // 
      // menuOpenDoc
      // 
      this.menuOpenDoc.Index = 0;
      this.menuOpenDoc.Text = "Open...";
      this.menuOpenDoc.Click += new System.EventHandler(this.menuOpenDoc_Click);
      // 
      // menuNewDoc
      // 
      this.menuNewDoc.Index = 1;
      this.menuNewDoc.Text = "New...";
      this.menuNewDoc.Click += new System.EventHandler(this.menuNewDoc_Click);
      // 
      // menuSaveDoc
      // 
      this.menuSaveDoc.Index = 2;
      this.menuSaveDoc.Text = "Save";
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
      // menuAppExit
      // 
      this.menuAppExit.Index = 5;
      this.menuAppExit.Text = "Exit";
      this.menuAppExit.Click += new System.EventHandler(this.menuAppExit_Click);
      // 
      // statusBar1
      // 
      this.statusBar1.Location = new System.Drawing.Point(0, 544);
      this.statusBar1.Name = "statusBar1";
      this.statusBar1.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
            this.statusBarXYUnits});
      this.statusBar1.ShowPanels = true;
      this.statusBar1.Size = new System.Drawing.Size(784, 22);
      this.statusBar1.TabIndex = 5;
      // 
      // statusBarXYUnits
      // 
      this.statusBarXYUnits.Name = "statusBarXYUnits";
      this.statusBarXYUnits.Width = 200;
      // 
      // axToolbarControl1
      // 
      this.axToolbarControl1.Dock = System.Windows.Forms.DockStyle.Top;
      this.axToolbarControl1.Location = new System.Drawing.Point(0, 0);
      this.axToolbarControl1.Name = "axToolbarControl1";
      this.axToolbarControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axToolbarControl1.OcxState")));
      this.axToolbarControl1.Size = new System.Drawing.Size(784, 28);
      this.axToolbarControl1.TabIndex = 7;
      // 
      // axTOCControl1
      // 
      this.axTOCControl1.Dock = System.Windows.Forms.DockStyle.Left;
      this.axTOCControl1.Location = new System.Drawing.Point(0, 28);
      this.axTOCControl1.Name = "axTOCControl1";
      this.axTOCControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axTOCControl1.OcxState")));
      this.axTOCControl1.Size = new System.Drawing.Size(200, 516);
      this.axTOCControl1.TabIndex = 8;
      // 
      // tabControl1
      // 
      this.tabControl1.Controls.Add(this.tabPage1);
      this.tabControl1.Controls.Add(this.tabPage2);
      this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tabControl1.Location = new System.Drawing.Point(200, 28);
      this.tabControl1.Name = "tabControl1";
      this.tabControl1.SelectedIndex = 0;
      this.tabControl1.Size = new System.Drawing.Size(584, 488);
      this.tabControl1.TabIndex = 9;
      this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
      // 
      // tabPage1
      // 
      this.tabPage1.Controls.Add(this.axLicenseControl1);
      this.tabPage1.Controls.Add(this.axMapControl1);
      this.tabPage1.Location = new System.Drawing.Point(4, 22);
      this.tabPage1.Name = "tabPage1";
      this.tabPage1.Size = new System.Drawing.Size(576, 462);
      this.tabPage1.TabIndex = 0;
      this.tabPage1.Text = "Map";
      // 
      // axLicenseControl1
      // 
      this.axLicenseControl1.Enabled = true;
      this.axLicenseControl1.Location = new System.Drawing.Point(210, 220);
      this.axLicenseControl1.Name = "axLicenseControl1";
      this.axLicenseControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axLicenseControl1.OcxState")));
      this.axLicenseControl1.Size = new System.Drawing.Size(32, 32);
      this.axLicenseControl1.TabIndex = 1;
      // 
      // axMapControl1
      // 
      this.axMapControl1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.axMapControl1.Location = new System.Drawing.Point(0, 0);
      this.axMapControl1.Name = "axMapControl1";
      this.axMapControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axMapControl1.OcxState")));
      this.axMapControl1.Size = new System.Drawing.Size(576, 462);
      this.axMapControl1.TabIndex = 0;
      this.axMapControl1.OnMouseMove += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMouseMoveEventHandler(this.axMapControl1_OnMouseMove);
      // 
      // tabPage2
      // 
      this.tabPage2.Controls.Add(this.axPageLayoutControl1);
      this.tabPage2.Location = new System.Drawing.Point(4, 22);
      this.tabPage2.Name = "tabPage2";
      this.tabPage2.Size = new System.Drawing.Size(576, 462);
      this.tabPage2.TabIndex = 1;
      this.tabPage2.Text = "Layout";
      // 
      // axPageLayoutControl1
      // 
      this.axPageLayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.axPageLayoutControl1.Location = new System.Drawing.Point(0, 0);
      this.axPageLayoutControl1.Name = "axPageLayoutControl1";
      this.axPageLayoutControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axPageLayoutControl1.OcxState")));
      this.axPageLayoutControl1.Size = new System.Drawing.Size(576, 462);
      this.axPageLayoutControl1.TabIndex = 0;
      this.axPageLayoutControl1.OnMouseMove += new ESRI.ArcGIS.Controls.IPageLayoutControlEvents_Ax_OnMouseMoveEventHandler(this.axPageLayoutControl1_OnMouseMove);
      // 
      // axToolbarControl2
      // 
      this.axToolbarControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.axToolbarControl2.Location = new System.Drawing.Point(200, 516);
      this.axToolbarControl2.Name = "axToolbarControl2";
      this.axToolbarControl2.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axToolbarControl2.OcxState")));
      this.axToolbarControl2.Size = new System.Drawing.Size(584, 28);
      this.axToolbarControl2.TabIndex = 10;
      // 
      // splitter1
      // 
      this.splitter1.Location = new System.Drawing.Point(200, 28);
      this.splitter1.Name = "splitter1";
      this.splitter1.Size = new System.Drawing.Size(3, 488);
      this.splitter1.TabIndex = 11;
      this.splitter1.TabStop = false;
      // 
      // frmMain
      // 
      this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
      this.ClientSize = new System.Drawing.Size(784, 566);
      this.Controls.Add(this.splitter1);
      this.Controls.Add(this.tabControl1);
      this.Controls.Add(this.axToolbarControl2);
      this.Controls.Add(this.axTOCControl1);
      this.Controls.Add(this.axToolbarControl1);
      this.Controls.Add(this.statusBar1);
      this.Menu = this.mainMenu1;
      this.Name = "frmMain";
      this.Text = "Map & PageLayout synchronization";
      this.Load += new System.EventHandler(this.frmMain_Load);
      ((System.ComponentModel.ISupportInitialize)(this.statusBarXYUnits)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.axToolbarControl1)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.axTOCControl1)).EndInit();
      this.tabControl1.ResumeLayout(false);
      this.tabPage1.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.axMapControl1)).EndInit();
      this.tabPage2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.axPageLayoutControl1)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.axToolbarControl2)).EndInit();
      this.ResumeLayout(false);

    }
    #endregion

    #region menu event handlers
    /// <summary>
    /// Open New Document menu event handler
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void menuOpenDoc_Click(object sender, System.EventArgs e)
    {
      // switch to map view
      tabControl1.SelectedTab = (TabPage)tabControl1.Controls[0];
      
      //launch the OpenMapDoc command
      OpenNewMapDocument openMapDoc = new OpenNewMapDocument(m_controlsSynchronizer);
      openMapDoc.OnCreate(m_controlsSynchronizer.MapControl.Object);
      openMapDoc.OnClick();

      m_documentFileName = openMapDoc.DocumentFileName;
    }

    /// <summary>
    /// New Map document menu event handler
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void menuNewDoc_Click(object sender, System.EventArgs e)
    {
      //ask the user whether he'd like to save the current doc
      DialogResult res = MessageBox.Show("Would you like to save the current document?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
      if (res == DialogResult.Yes)
      {
        //if yes, launch the SaveAs command
        ICommand command = new ControlsSaveAsDocCommandClass();
        command.OnCreate(m_controlsSynchronizer.PageLayoutControl.Object);
        command.OnClick();
      }

      // switch to map view
      tabControl1.SelectedTab = (TabPage)tabControl1.Controls[0];

      //create a new Map instance
      IMap map = new MapClass();
      map.Name = "Map";
      //replace the shared map with the new Map instance
      m_controlsSynchronizer.ReplaceMap(map);

      m_documentFileName = string.Empty;
    }

    /// <summary>
    /// Save document menu event handler
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <remarks>Save the current MapDocument</remarks>
    private void menuSaveDoc_Click(object sender, System.EventArgs e)
    {
      //make sure that the current MapDoc is valid first
      if (m_documentFileName != string.Empty && m_mapControl.CheckMxFile(m_documentFileName))
      {
        //create a new instance of a MapDocument class
        IMapDocument mapDoc = new MapDocumentClass();
        //Open the current document into the MapDocument
        mapDoc.Open(m_documentFileName, string.Empty);

        //Replace the map with the one of the PageLayout
        mapDoc.ReplaceContents((IMxdContents)m_pageLayoutControl.PageLayout);

        //save the document
        mapDoc.Save(mapDoc.UsesRelativePaths, false);
        mapDoc.Close();
      }
    }

    /// <summary>
    /// SaveAs document menu event handler
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <remarks>Save the current MapDocument as a new doc</remarks>
    private void menuSaveAsDoc_Click(object sender, System.EventArgs e)
    {
      //launch the SaveAs command. Always use the PageLayoutControl in order to keep 
      // all of the map surrounds. 
      ICommand command = new ControlsSaveAsDocCommandClass();
      command.OnCreate(m_controlsSynchronizer.PageLayoutControl.Object);
      command.OnClick();
    }

    /// <summary>
    /// Exit menu event handler
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void menuAppExit_Click(object sender, System.EventArgs e)
    {
      Application.Exit();
    }
    #endregion

    /// <summary>
    /// Form.Load method
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void frmMain_Load(object sender, System.EventArgs e)
    {
      //get a reference to the MapControl and the PageLayoutControl
      m_mapControl = (IMapControl3)axMapControl1.Object;
      m_pageLayoutControl = (IPageLayoutControl2)axPageLayoutControl1.Object;

      //initialize the controls synchronization class
      m_controlsSynchronizer = new ControlsSynchronizer(m_mapControl, m_pageLayoutControl);

      //bind the controls together (both point at the same map) and set the MapControl as the active control
      m_controlsSynchronizer.BindControls(true);

      //add the framework controls (TOC and Toolbars) in order to synchronize then when the
      //active control changes (call SetBuddyControl)
      m_controlsSynchronizer.AddFrameworkControl(axToolbarControl1.Object);
      m_controlsSynchronizer.AddFrameworkControl(axToolbarControl2.Object);
      m_controlsSynchronizer.AddFrameworkControl(axTOCControl1.Object);

      //add the Open Map Document command onto the toolbar
      OpenNewMapDocument openMapDoc = new OpenNewMapDocument(m_controlsSynchronizer);
      axToolbarControl1.AddItem(openMapDoc, -1, 0, false, -1, esriCommandStyles.esriCommandStyleIconOnly);
    }

    /// <summary>
    /// SelectedIndexChanged event handler
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void tabControl1_SelectedIndexChanged(object sender, System.EventArgs e)
    {
      if (tabControl1.SelectedIndex == 0) //map view
      {
        //activate the MapControl and deactivate the PageLayoutControl
        m_controlsSynchronizer.ActivateMap();
      }
      else //layout view
      {
        //activate the PageLayoutControl and deactivate the MapControl
        m_controlsSynchronizer.ActivatePageLayout();
      }
    }

    /// <summary>
    /// MapControl MouseMove event handler
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void axMapControl1_OnMouseMove(object sender, IMapControlEvents2_OnMouseMoveEvent e)
    {
      statusBarXYUnits.Text = string.Format("{0} {1} {2}", e.mapX.ToString("#######.###"), e.mapY.ToString("#######.###"), axMapControl1.MapUnits.ToString().Substring(4));
    }

    /// <summary>
    /// PageLayoutControl MouseMove event handler
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void axPageLayoutControl1_OnMouseMove(object sender, IPageLayoutControlEvents_OnMouseMoveEvent e)
    {
      statusBarXYUnits.Text = string.Format("{0} {1} {2}", e.pageX.ToString("###.##"), e.pageY.ToString("###.##"), axPageLayoutControl1.Page.Units.ToString().Substring(4));
    }
  }
}
