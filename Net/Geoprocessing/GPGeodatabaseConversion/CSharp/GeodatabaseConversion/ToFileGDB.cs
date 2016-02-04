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
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.ConversionTools;
using ESRI.ArcGIS.DataManagementTools;
using ESRI.ArcGIS.Geoprocessor;
using ESRI.ArcGIS.Geoprocessing;
using ESRI.ArcGIS.esriSystem;

namespace GeodatabaseConversion
{
    class ToFileGDB
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

            // run geoprocessing code
            ConvertPersonalGeodatabaseToFileGeodatabase();

            // shutdown application
            aoLicenseInitializer.ShutdownApplication();
            
        }

        private static void ConvertPersonalGeodatabaseToFileGeodatabase()
        {
            // Initialize the Geoprocessor
            Geoprocessor geoprocessor = new Geoprocessor();

            // Allow for the overwriting of file geodatabases, if they previously exist.
            geoprocessor.OverwriteOutput = true;

            // Set the workspace to a folder containing personal geodatabases.
            geoprocessor.SetEnvironmentValue("workspace", @"C:\data");

            // Identify personal geodatabases.
            IGpEnumList workspaces = geoprocessor.ListWorkspaces("*", "Access");
            string workspace = workspaces.Next();
            while (workspace != "")
            {
                // Set workspace to current personal geodatabase
                geoprocessor.SetEnvironmentValue("workspace", workspace);

                // Create a file geodatabase with the same name as the personal geodatabase
                string gdbname = System.IO.Path.GetFileName(workspace).Replace(".mdb", "");
                string dirname = System.IO.Path.GetDirectoryName(workspace);

                // Execute CreateFileGDB tool
                CreateFileGDB createFileGDBTool = new CreateFileGDB(dirname, gdbname + ".gdb");
                geoprocessor.Execute(createFileGDBTool, null);

                // Initialize the Copy Tool
                Copy copyTool = new Copy();

                // Identify feature classes and copy to file geodatabase
                IGpEnumList fcs = geoprocessor.ListFeatureClasses("", "", "");
                string fc = fcs.Next();
                while (fc != "")
                {
                    Console.WriteLine("Copying " + fc + " to " + gdbname + ".gdb");
                    copyTool.in_data = fc;
                    copyTool.out_data = dirname + "\\" + gdbname + ".gdb" + "\\" + fc;
                    geoprocessor.Execute(copyTool, null);
                    fc = fcs.Next();
                }

                // Identify feature datasets and copy to file geodatabase
                IGpEnumList fds = geoprocessor.ListDatasets("", "");
                string fd = fds.Next();
                while (fd != "")
                {
                    Console.WriteLine("Copying " + fd + " to " + gdbname + ".gdb");
                    copyTool.in_data = fd;
                    copyTool.data_type = "FeatureDataset";
                    copyTool.out_data = dirname + "\\" + gdbname + ".gdb" + "\\" + fd;
                    try
                    {
                        geoprocessor.Execute(copyTool, null);
                    }
                    catch (Exception ex)
                    {
                        object sev = null;
                        System.Windows.Forms.MessageBox.Show(ex.Message);
                    }
                    fd = fds.Next();
                }

                // Identify tables and copy to file geodatabase
                IGpEnumList tbls = geoprocessor.ListTables("", "");
                string tbl = tbls.Next();
                while (tbl != "")
                {
                    Console.WriteLine("Copying " + tbl + " to " + gdbname + ".gdb");
                    copyTool.in_data = tbl;
                    copyTool.out_data = dirname + "\\" + gdbname + ".gdb" + "\\" + tbl;
                    geoprocessor.Execute(copyTool, null);
                    tbl = tbls.Next();
                }

                workspace = workspaces.Next();
            }
        }
    }
}
