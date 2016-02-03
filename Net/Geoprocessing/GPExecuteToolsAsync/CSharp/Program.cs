using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS;

namespace RunGPAsync
{
  static class Program
  {
    
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

      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(new RunGPForm());
    }
  }
}