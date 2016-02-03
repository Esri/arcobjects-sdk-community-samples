using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geoprocessing;
using ESRI.ArcGIS.Geoprocessor;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.ConversionTools;

namespace GPFieldMapping
{
    class FieldMapping
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
            RunGPFieldMapping();

            aoLicenseInitializer.ShutdownApplication();
        }

        private static void RunGPFieldMapping()
        {
            // Initialize the Geoprocessor
            ESRI.ArcGIS.Geoprocessor.Geoprocessor GP = new ESRI.ArcGIS.Geoprocessor.Geoprocessor();
            GP.OverwriteOutput = true;

            // Create the GPUtilites object
            IGPUtilities gputilities = new GPUtilitiesClass();

            // Create a DETable data element object 
            IDETable inputTableA = (IDETable)gputilities.MakeDataElement(@"C:\data\citiblocks.gdb\census", null, null);

            // Create an array of input tables
            IArray inputtables = new ArrayClass();
            inputtables.Add(inputTableA);

            // Initialize the GPFieldMapping
            IGPFieldMapping fieldmapping = new GPFieldMappingClass();
            fieldmapping.Initialize(inputtables, null);

            // Create a new output field
            IFieldEdit trackidfield = new FieldClass();
            trackidfield.Name_2 = "TRACTID";
            trackidfield.Type_2 = esriFieldType.esriFieldTypeString;
            trackidfield.Length_2 = 50;

            // Create a new FieldMap
            IGPFieldMap trackid = new GPFieldMapClass();
            trackid.OutputField = trackidfield;

            // Find field map "STFID" containing the input field "STFID". Add input field to the new field map.
            int fieldmap_index = fieldmapping.FindFieldMap("STFID");
            IGPFieldMap stfid_fieldmap = fieldmapping.GetFieldMap(fieldmap_index);
            int field_index = stfid_fieldmap.FindInputField(inputTableA, "STFID");
            IField inputField = stfid_fieldmap.GetField(field_index);
            trackid.AddInputField(inputTableA, inputField, 5, 10);

            // Add the new field map to the field mapping
            fieldmapping.AddFieldMap(trackid);

            // Execute Table to Table tool using the FieldMapping
            TableToTable tblTotbl = new TableToTable();
            tblTotbl.in_rows = inputTableA;
            tblTotbl.out_path = @"C:\data\citiblocks.gdb";
            tblTotbl.out_name = "census_out";
            tblTotbl.field_mapping = fieldmapping;

            object sev = null;
            try
            {
                GP.Execute(tblTotbl, null);
                System.Windows.Forms.MessageBox.Show(GP.GetMessages(ref sev));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                System.Windows.Forms.MessageBox.Show(GP.GetMessages(ref sev));
            }
        }
    }
}
