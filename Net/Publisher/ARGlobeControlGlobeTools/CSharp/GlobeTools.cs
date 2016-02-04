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
using ESRI.ArcGIS.PublisherControls;

namespace GlobeTools
{
  /// <summary>
  /// Summary description for Form1.
  /// </summary>
  public class GlobeTools : System.Windows.Forms.Form
  {
    private ESRI.ArcGIS.PublisherControls.AxArcReaderGlobeControl axArcReaderGlobeControl1;
    internal System.Windows.Forms.RadioButton optTool4;
    internal System.Windows.Forms.RadioButton optTool3;
    internal System.Windows.Forms.RadioButton optTool2;
    internal System.Windows.Forms.RadioButton optTool1;
    internal System.Windows.Forms.RadioButton optTool0;
    internal System.Windows.Forms.Button btnFullExtent;
    internal System.Windows.Forms.Button btnLoad;
    private System.Windows.Forms.OpenFileDialog openFileDialog1;
    private esriARGlobeTool arGlobeTool;
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.Container components = null;

    public GlobeTools()
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
    protected override void Dispose( bool disposing )
    {
      if( disposing )
      {
        if (components != null) 
        {
          components.Dispose();
        }
      }
      base.Dispose( disposing );
    }

    #region Windows Form Designer generated code
    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(GlobeTools));
      this.axArcReaderGlobeControl1 = new ESRI.ArcGIS.PublisherControls.AxArcReaderGlobeControl();
      this.optTool4 = new System.Windows.Forms.RadioButton();
      this.optTool3 = new System.Windows.Forms.RadioButton();
      this.optTool2 = new System.Windows.Forms.RadioButton();
      this.optTool1 = new System.Windows.Forms.RadioButton();
      this.optTool0 = new System.Windows.Forms.RadioButton();
      this.btnFullExtent = new System.Windows.Forms.Button();
      this.btnLoad = new System.Windows.Forms.Button();
      this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
      ((System.ComponentModel.ISupportInitialize)(this.axArcReaderGlobeControl1)).BeginInit();
      this.SuspendLayout();
      // 
      // axArcReaderGlobeControl1
      // 
      this.axArcReaderGlobeControl1.Location = new System.Drawing.Point(12, 68);
      this.axArcReaderGlobeControl1.Name = "axArcReaderGlobeControl1";
      this.axArcReaderGlobeControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axArcReaderGlobeControl1.OcxState")));
      this.axArcReaderGlobeControl1.Size = new System.Drawing.Size(524, 332);
      this.axArcReaderGlobeControl1.TabIndex = 0;
      this.axArcReaderGlobeControl1.OnDocumentUnloaded += new System.EventHandler(this.axArcReaderGlobeControl1_OnDocumentUnloaded);
      this.axArcReaderGlobeControl1.OnDocumentLoaded += new ESRI.ArcGIS.PublisherControls.IARGlobeControlEvents_Ax_OnDocumentLoadedEventHandler(this.axArcReaderGlobeControl1_OnDocumentLoaded);
      // 
      // optTool4
      // 
      this.optTool4.Appearance = System.Windows.Forms.Appearance.Button;
      this.optTool4.Location = new System.Drawing.Point(372, 12);
      this.optTool4.Name = "optTool4";
      this.optTool4.Size = new System.Drawing.Size(84, 44);
      this.optTool4.TabIndex = 14;
      this.optTool4.Text = "Zoom In\\Out";
      this.optTool4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.optTool4.Click += new System.EventHandler(this.MixedControls_Click);

      // 
      // optTool3
      // 
      this.optTool3.Appearance = System.Windows.Forms.Appearance.Button;
      this.optTool3.Location = new System.Drawing.Point(300, 12);
      this.optTool3.Name = "optTool3";
      this.optTool3.Size = new System.Drawing.Size(72, 44);
      this.optTool3.TabIndex = 13;
      this.optTool3.Text = "Target";
      this.optTool3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.optTool3.Click += new System.EventHandler(this.MixedControls_Click);
      // 
      // optTool2
      // 
      this.optTool2.Appearance = System.Windows.Forms.Appearance.Button;
      this.optTool2.Location = new System.Drawing.Point(228, 12);
      this.optTool2.Name = "optTool2";
      this.optTool2.Size = new System.Drawing.Size(72, 44);
      this.optTool2.TabIndex = 12;
      this.optTool2.Text = "Navigate";
      this.optTool2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.optTool2.Click += new System.EventHandler(this.MixedControls_Click);
      // 
      // optTool1
      // 
      this.optTool1.Appearance = System.Windows.Forms.Appearance.Button;
      this.optTool1.Location = new System.Drawing.Point(156, 12);
      this.optTool1.Name = "optTool1";
      this.optTool1.Size = new System.Drawing.Size(72, 44);
      this.optTool1.TabIndex = 11;
      this.optTool1.Text = "Pivot";
      this.optTool1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.optTool1.Click += new System.EventHandler(this.MixedControls_Click);
      // 
      // optTool0
      // 
      this.optTool0.Appearance = System.Windows.Forms.Appearance.Button;
      this.optTool0.Location = new System.Drawing.Point(84, 12);
      this.optTool0.Name = "optTool0";
      this.optTool0.Size = new System.Drawing.Size(72, 44);
      this.optTool0.TabIndex = 10;
      this.optTool0.Text = "Pan";
      this.optTool0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.optTool0.Click += new System.EventHandler(this.MixedControls_Click);
      // 
      // btnFullExtent
      // 
      this.btnFullExtent.Location = new System.Drawing.Point(452, 12);
      this.btnFullExtent.Name = "btnFullExtent";
      this.btnFullExtent.Size = new System.Drawing.Size(84, 44);
      this.btnFullExtent.TabIndex = 9;
      this.btnFullExtent.Text = "Full Extent";
      this.btnFullExtent.Click += new System.EventHandler(this.btnFullExtent_Click);
      // 
      // btnLoad
      // 
      this.btnLoad.Location = new System.Drawing.Point(12, 12);
      this.btnLoad.Name = "btnLoad";
      this.btnLoad.Size = new System.Drawing.Size(72, 44);
      this.btnLoad.TabIndex = 8;
      this.btnLoad.Text = "Load";
      this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
      // 
      // GlobeTools
      // 
      this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
      this.ClientSize = new System.Drawing.Size(544, 410);
      this.Controls.Add(this.optTool4);
      this.Controls.Add(this.optTool3);
      this.Controls.Add(this.optTool2);
      this.Controls.Add(this.optTool1);
      this.Controls.Add(this.optTool0);
      this.Controls.Add(this.btnFullExtent);
      this.Controls.Add(this.btnLoad);
      this.Controls.Add(this.axArcReaderGlobeControl1);
      this.Name = "GlobeTools";
      this.Text = "GlobeTools";
      this.Closing += new System.ComponentModel.CancelEventHandler(this.GlobeTools_Closing);
      this.Load += new System.EventHandler(this.GlobeTools_Load);
      ((System.ComponentModel.ISupportInitialize)(this.axArcReaderGlobeControl1)).EndInit();
      this.ResumeLayout(false);

    }
    #endregion

    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main() 
    {
        if (!ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.ArcReader))
        {
            if (!ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.EngineOrDesktop))
            {
                MessageBox.Show("Unable to bind to ArcGIS runtime. Application will be shut down.");
                return;
            }
        }
        
        Application.Run(new GlobeTools());
    }

    private void btnLoad_Click(object sender, System.EventArgs e)
    {
      //Open a file dialog for selecting map documents
      openFileDialog1.Title = "Select Published Map Document";
      openFileDialog1.Filter = "Published Map Documents (*.pmf)|*.pmf";
      openFileDialog1.ShowDialog();

      //Exit if no map document is selected
      string sFilePath = openFileDialog1.FileName;
      if (sFilePath == "") return;

      //Load the specified pmf
      if (axArcReaderGlobeControl1.CheckDocument(sFilePath) == true)
      {
        axArcReaderGlobeControl1.LoadDocument(sFilePath,"");
      }
      else
      {
        System.Windows.Forms.MessageBox.Show("This document cannot be loaded!");
        return;
      }
    }

    private void MixedControls_Click(object sender, System.EventArgs e)
    {
      RadioButton b = (RadioButton) sender;
      //Set current tool
      switch (b.Name)
      {
        case "optTool0":
          axArcReaderGlobeControl1.CurrentARGlobeTool = esriARGlobeTool.esriARGlobeToolPan;
          break;
        case "optTool1":
          axArcReaderGlobeControl1.CurrentARGlobeTool = esriARGlobeTool.esriARGlobeToolPivot;
          break;
        case "optTool2":
          axArcReaderGlobeControl1.CurrentARGlobeTool = esriARGlobeTool.esriARGlobeToolNavigate;
          break;
        case "optTool3":
          axArcReaderGlobeControl1.CurrentARGlobeTool = esriARGlobeTool.esriARGlobeToolTarget;
          break;
        case "optTool4":
          axArcReaderGlobeControl1.CurrentARGlobeTool = esriARGlobeTool.esriARGlobeToolZoomInOut;
          break;
      }

      //Remember the current tool
      arGlobeTool = axArcReaderGlobeControl1.CurrentARGlobeTool;
    }

    private void GlobeTools_Closing(object sender, System.ComponentModel.CancelEventArgs e)
    {
      //Release COM objects
      ESRI.ArcGIS.ADF.COMSupport.AOUninitialize.Shutdown();
    }

    private void GlobeTools_Load(object sender, System.EventArgs e)
    {
      //Disable controls
      optTool0.Enabled = false;
      optTool1.Enabled = false;
      optTool2.Enabled = false;
      optTool3.Enabled = false;
      optTool4.Enabled = false;
      btnFullExtent.Enabled = false;
    }

    private void axArcReaderGlobeControl1_OnDocumentLoaded(object sender, ESRI.ArcGIS.PublisherControls.IARGlobeControlEvents_OnDocumentLoadedEvent e)
    {
      //Enable Tools
      optTool0.Enabled = true;
      optTool1.Enabled = true;
      optTool2.Enabled = true;
      optTool3.Enabled = true;
      optTool4.Enabled = true;
      btnFullExtent.Enabled = true;
    }

    private void axArcReaderGlobeControl1_OnDocumentUnloaded(object sender, System.EventArgs e)
    {
      //Enable Tools
      optTool0.Enabled = false;
      optTool1.Enabled = false;
      optTool2.Enabled = false;
      optTool3.Enabled = false;
      optTool4.Enabled = false;
      btnFullExtent.Enabled = false;
    }

    private void btnFullExtent_Click(object sender, System.EventArgs e)
    {
      //Zoom to Full Extent
      axArcReaderGlobeControl1.ARGlobe.ZoomToFullExtent();
    }


  }
}