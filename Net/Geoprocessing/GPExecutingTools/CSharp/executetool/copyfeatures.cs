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
/*
 
 * copyfeatures.cs : This C# sample uses the Geoprocessor class in conjunction with geoprocessing tools classes to
 * execute a series of geoprocessing tools. This sample will extract features to a new feature class based on a
 * location and an attribute query.

*/

using System;
using System.Collections;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.DataManagementTools;
using ESRI.ArcGIS.Geoprocessor;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.AnalysisTools;

namespace copyfeatures
{
    class copyfeatures
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

            // Run the geoprocessing code
            SelectFeaturesAndRunCopyFeatures();

            aoLicenseInitializer.ShutdownApplication();

        }


        private static void SelectFeaturesAndRunCopyFeatures()
        {
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////
            // STEP 1: Make feature layers using the MakeFeatureLayer tool for the inputs to the SelectByLocation tool.
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////

            // Initialize the Geoprocessor 
            Geoprocessor GP = new Geoprocessor();

            // Initialize the MakeFeatureLayer tool
            MakeFeatureLayer makefeaturelayer = new MakeFeatureLayer();
            makefeaturelayer.in_features = @"C:\data\nfld.gdb\wells";
            makefeaturelayer.out_layer = "Wells_Lyr";
            RunTool(GP, makefeaturelayer, null);

            makefeaturelayer.in_features = @"C:\data\nfld.gdb\bedrock";
            makefeaturelayer.out_layer = "bedrock_Lyr";
            RunTool(GP, makefeaturelayer, null);

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // STEP 2: Execute SelectLayerByLocation using the feature layers to select all wells that intersect the bedrock geology.
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            // Initialize the SelectLayerByLocation tool
            SelectLayerByLocation SelectByLocation = new SelectLayerByLocation();

            SelectByLocation.in_layer = "Wells_Lyr";
            SelectByLocation.select_features = "bedrock_Lyr";
            SelectByLocation.overlap_type = "INTERSECT";
            RunTool(GP, SelectByLocation, null);

            /////////////////////////////////////////////////////////////////////////////////////////////////
            // STEP 3: Execute SelectLayerByAttribute to select all wells that have a well yield > 150 L/min.
            /////////////////////////////////////////////////////////////////////////////////////////////////

            // Initialize the SelectLayerByAttribute tool
            SelectLayerByAttribute SelectByAttribute = new SelectLayerByAttribute();

            SelectByAttribute.in_layer_or_view = "Wells_Lyr";
            SelectByAttribute.selection_type = "NEW_SELECTION";
            SelectByAttribute.where_clause = "WELL_YIELD > 150";
            RunTool(GP, SelectByAttribute, null);

            ////////////////////////////////////////////////////////////////////////////////////////////////////////
            // STEP 4: Execute CopyFeatures tool to create a new feature class of wells with well yield > 150 L/min.
            ////////////////////////////////////////////////////////////////////////////////////////////////////////

            // Initialize the CopyFeatures tool
            CopyFeatures CopyFeatures = new CopyFeatures();

            CopyFeatures.in_features = "Wells_Lyr";
            CopyFeatures.out_feature_class = @"C:\data\nfld.gdb\high_yield_wells";


            RunTool(GP, CopyFeatures, null);
        }


        private static void RunTool(Geoprocessor geoprocessor, IGPProcess process, ITrackCancel TC)
        {
    
            // Set the overwrite output option to true
            geoprocessor.OverwriteOutput = true;

            // Execute the tool            
            try
            {
                geoprocessor.Execute(process, null);
                ReturnMessages(geoprocessor);

            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
                ReturnMessages(geoprocessor);
            }
        }

        // Function for returning the tool messages.
        private static void ReturnMessages(Geoprocessor gp)
        {
            if (gp.MessageCount > 0)
            {
                for (int Count = 0; Count <= gp.MessageCount - 1; Count++)
                {
                    Console.WriteLine(gp.GetMessage(Count));
                }
            }

        }
    }
}
