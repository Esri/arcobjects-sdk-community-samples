using System;
using System.Windows.Forms;
using ESRI.ArcGIS.esriSystem;

namespace LocationAllocationSolver
{
	class Program
	{
		[STAThread]
		static void Main(string[] args)
		{
			if (!ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.Engine))
			{
				if (!ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.Desktop))
				{
					System.Windows.Forms.MessageBox.Show("This application could not load the correct version of ArcGIS.");
					return;
				}
			}

			LicenseInitializer aoLicenseInitializer = new LicenseInitializer();
			if (!aoLicenseInitializer.InitializeApplication(new esriLicenseProductCode[] { esriLicenseProductCode.esriLicenseProductCodeEngine, esriLicenseProductCode.esriLicenseProductCodeBasic, esriLicenseProductCode.esriLicenseProductCodeStandard, esriLicenseProductCode.esriLicenseProductCodeAdvanced },
			new esriLicenseExtensionCode[] { esriLicenseExtensionCode.esriLicenseExtensionCodeNetwork }))
			{
				System.Windows.Forms.MessageBox.Show("This application could not initialize with the correct ArcGIS license and will shutdown. LicenseMessage: " + aoLicenseInitializer.LicenseMessage());
				aoLicenseInitializer.ShutdownApplication();
				return;
			}

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			frmLocationAllocationSolver mainForm = new frmLocationAllocationSolver();

			// Check that the form was not already disposed of during initialization before running it.
			if (mainForm != null && !mainForm.IsDisposed)
				Application.Run(mainForm);

			aoLicenseInitializer.ShutdownApplication();
		}
	}
}
