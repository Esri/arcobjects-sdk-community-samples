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
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geoprocessing;

namespace GeoprocessingInDotNet2008
{
    class Program
    {
        private static LicenseInitializer m_AOLicenseInitializer = new GeoprocessingInDotNet2008.LicenseInitializer();
    
        [STAThread()]
        static void Main(string[] args)
        {
            // Load the product code and version to the version manager
            ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.Desktop);

            //ESRI License Initializer generated code.
            m_AOLicenseInitializer.InitializeApplication(new esriLicenseProductCode[] { esriLicenseProductCode.esriLicenseProductCodeAdvanced },
            new esriLicenseExtensionCode[] { });

            // Create geoprocessor. Overwrite true will replace existing output
            IGeoProcessor2 gp = new GeoProcessorClass();
            gp.OverwriteOutput = true;

            // Get the workspace from the user
            Console.WriteLine("Enter the path to folder where you copied the data folder.");
            Console.WriteLine("Example: C:\\AirportsAndGolf\\data");
            Console.Write(">");
            string wks = Console.ReadLine();

            // set the workspace to the value user entered
            gp.SetEnvironmentValue("workspace", wks + "\\" + "golf.gdb");

            // Add the custom toolbox to geoprocessor
            gp.AddToolbox(wks + "\\" + "Find Golf Courses.tbx");

            // Create a variant - data are in the workspace
            IVariantArray parameters = new VarArrayClass();
            parameters.Add("Airports");
            parameters.Add("8 Miles");
            parameters.Add("Golf");
            parameters.Add("GolfNearAirports");

            object sev = null;

            try
            {
                gp.Execute("GolfFinder", parameters, null);
                Console.WriteLine(gp.GetMessages(ref sev));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                string errorMsgs = gp.GetMessages(ref sev);
                Console.WriteLine(errorMsgs);
            }
            finally
            {
                Console.WriteLine("Hit Enter to quit");
                Console.ReadLine(); // pause the console to see messages
            }

            //ESRI License Initializer generated code.
            //Do not make any call to ArcObjects after ShutDownApplication()
            m_AOLicenseInitializer.ShutdownApplication();
        }
    }
}
