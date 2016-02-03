using System;
using System.Collections.Generic;
using System.Text;
using ESRI.ArcGIS.esriSystem;

namespace TestApp
{
  class Program
  {
    private static LicenseInitializer m_AOLicenseInitializer = new LicenseInitializer();

    [STAThread]
    static void Main(string[] args)
    {
      //ESRI License Initializer generated code.
      m_AOLicenseInitializer.InitializeApplication(new esriLicenseProductCode[] { esriLicenseProductCode.esriLicenseProductCodeEngine },
      new esriLicenseExtensionCode[] { });

      Console.WriteLine("Creating container object");
      //create a new instance of the test object which will internally clone our clonable object
      TestClass t = new TestClass();
      t.Test();

      Console.WriteLine("Done, hit any key to continue.");
      Console.ReadKey();

      //ESRI License Initializer generated code.
      //Do not make any call to ArcObjects after ShutDownApplication()
      m_AOLicenseInitializer.ShutdownApplication();
    }
  }
}
