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
using System.Windows.Forms;
using ESRI.ArcGIS.esriSystem;

namespace DemoITableBinding
{
  static class Program
  {
     // private static LicenseInitializer m_AOLicenseInitializer = new DemoITableBinding.LicenseInitializer();
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.Engine);
        LicenseInitializer m_AOLicenseInitializer = new DemoITableBinding.LicenseInitializer();
        //ESRI License Initializer generated code.
        if (!m_AOLicenseInitializer.InitializeApplication(new esriLicenseProductCode[] { esriLicenseProductCode.esriLicenseProductCodeEngine, esriLicenseProductCode.esriLicenseProductCodeEngineGeoDB, esriLicenseProductCode.esriLicenseProductCodeBasic, esriLicenseProductCode.esriLicenseProductCodeStandard, esriLicenseProductCode.esriLicenseProductCodeAdvanced },
        new esriLicenseExtensionCode[] { }))
        {
            System.Windows.Forms.MessageBox.Show(m_AOLicenseInitializer.LicenseMessage() +
            "\n\nThis application could not initialize with the correct ArcGIS license and will shutdown.",
            "ArcGIS License Failure");
            m_AOLicenseInitializer.ShutdownApplication();
            Application.Exit();
            return;
        }
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(new MainWnd());
      //ESRI License Initializer generated code.
      //Do not make any call to ArcObjects after ShutDownApplication()
      m_AOLicenseInitializer.ShutdownApplication();
    }
  }
}