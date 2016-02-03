using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using ESRI.ArcGIS;

namespace WorkingWithPackages
{
  static class Program
  {
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
      if (!(RuntimeManager.Bind(ProductCode.EngineOrDesktop)))
      {
        MessageBox.Show("Could not bind to a Runtime!", "No ArcGIS Runtime available!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(new FrmMapControl());
    }
  }
}
