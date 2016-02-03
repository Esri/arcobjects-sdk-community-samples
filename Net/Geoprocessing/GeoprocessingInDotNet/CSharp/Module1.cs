using System;
using System.Collections.Generic;
using System.Text;
using ESRI.ArcGIS.esriSystem;

namespace GeoprocessingInDotNet
{
    class Module1
    {
        private static LicenseInitializer m_AOLicenseInitializer = new GeoprocessingInDotNet.LicenseInitializer();
    
        [STAThread()]
        static void Main(string[] args)
        {
            //ESRI License Initializer generated code.
            m_AOLicenseInitializer.InitializeApplication(new esriLicenseProductCode[] { esriLicenseProductCode.esriLicenseProductCodeEngine },
            new esriLicenseExtensionCode[] { });
            //ESRI License Initializer generated code.
            //Do not make any call to ArcObjects after ShutDownApplication()
            m_AOLicenseInitializer.ShutdownApplication();

            //Call the main execution of the application
            RunTheApp();
        }

        // INSTRUCTIONS:
        //
        // 1. Create the following directories on your hard drive:
        // C:\gp
        // C:\gp\AirportsAndGolf
        // C:\gp\output
        //
        // 2. Copy the RunModel.xml file in this sample to C:\gp
        //
        // 3. Copy all of the shapefile sample data in the directory:
        // <your ArcGIS Developer Kit installation location>\Samples\data\AirportsAndGolf
        // to:
        // C:\gp\AirportsAndGolf
        //
        // 4. You must create a .NET Assembly from the custom model MY_CUSTOM_TOOLBOX.tbx 
        // that is provided with this sample. To create the .NET Assembly do the following:
        // Right click on the project name in the Solution Explorer and choose 'Add ArcGIS Toolbox Reference...'
        // In the ArcGIS Toolbox Reference dialog that opens:
        // - For the Toolbox: Navigate to the MY_CUSTOM_TOOLBOX.tbx in this sample 
        // - Accept the defaults for the 'Generated Assembly Name' and 'Generated Assembly Namespace'
        // - Specify the 'Generated Assembly Version' to be: 1.0.0.0
        // - Uncheck the 'Sign the Generated Assembly'
        // - Click OK
        //
        // 5. You should now be able to compile this sample and run it.


        /// <summary>
        /// Main execution of our application
        /// </summary>
        /// <remarks></remarks>
        public static void RunTheApp()
        {

            // Give message prompting the user for input
            Console.WriteLine("Enter the full path/filename of the xml driver file for the application.");
            Console.WriteLine("Example: C:\\gp\\RunModel.xml");
            Console.Write(">");

            // Obtain the input from the user
            System.String xmlPathFile = Console.ReadLine();

            // Let the user know something is happening
            Console.WriteLine("Processing...");

            //Get all of the models parameters
            System.Collections.Specialized.HybridDictionary modelParametersHybridDictionary = ReadXMLConfigurationFile(xmlPathFile);

            // Run the model
            System.String modelProcessingResultsString = ExecuteCustomGeoprocessingFunction(modelParametersHybridDictionary);

            // Display the results to the user
            Console.WriteLine(modelProcessingResultsString);

            // Close the application after the users hits any key
            Console.ReadLine();

        }

        /// <summary>
        /// Read in the arguments from the .xml file that we be used to run our model
        /// </summary>
        /// <param name="xmlPathFile">The full path and filename of the .xml file. Example: "C:\gp\RunModel.xml"</param>
        /// <returns>A HybridDictionary that contains the arguments to run our model.</returns>
        /// <remarks></remarks>
        public static System.Collections.Specialized.HybridDictionary ReadXMLConfigurationFile(System.String xmlPathFile)
        {

            System.Collections.Specialized.HybridDictionary modelParametersHybridDictionary = new System.Collections.Specialized.HybridDictionary();

            try
            {

                //Read the XML configuration file
                System.Xml.XmlDocument XMLdoc = new System.Xml.XmlDocument();
                XMLdoc.Load(xmlPathFile);

                // MY_CUSTOM_TOOLBOX
                System.Xml.XmlNode xMY_CUSTOM_TOOLBOX = XMLdoc["MY_CUSTOM_TOOLBOX"];

                // GolfFinder
                System.Xml.XmlNode xGolfFinder = xMY_CUSTOM_TOOLBOX["GolfFinder"];

                // BufferDistance
                System.Xml.XmlNode xBufferDistance = xGolfFinder["BufferDistance"];
                modelParametersHybridDictionary.Add("BufferDistance", xBufferDistance.InnerText);

                // Airports
                System.Xml.XmlNode xAirports = xGolfFinder["Airports"];
                modelParametersHybridDictionary.Add("Airports", xAirports.InnerText);

                // Golf
                System.Xml.XmlNode xGolf = xGolfFinder["Golf"];
                modelParametersHybridDictionary.Add("Golf", xGolf.InnerText);

                // AirportBuffer
                System.Xml.XmlNode xAirportBuffer = xGolfFinder["AirportBuffer"];
                modelParametersHybridDictionary.Add("AirportBuffer", xAirportBuffer.InnerText);

                // GolfNearAirports
                System.Xml.XmlNode xGolfNearAirports = xGolfFinder["GolfNearAirports"];
                modelParametersHybridDictionary.Add("GolfNearAirports", xGolfNearAirports.InnerText);

                return modelParametersHybridDictionary;
            }
            catch (Exception ex)
            {

                //The XML read was unsuccessful. Return an empty HybridDictionary
                return modelParametersHybridDictionary;

            }

        }


        /// <summary>
        /// Run the Geoprocessing model
        /// </summary>
        /// <param name="modelParametersHybridDictionary">A HybridDictionary that contains all of the arguments to run the model</param>
        /// <returns>A message of how well the model executed</returns>
        /// <remarks></remarks>
        public static System.String ExecuteCustomGeoprocessingFunction(System.Collections.Specialized.HybridDictionary modelParametersHybridDictionary)
        {

            try
            {

                // Create a Geoprocessor object
                ESRI.ArcGIS.Geoprocessor.Geoprocessor gp = new ESRI.ArcGIS.Geoprocessor.Geoprocessor();

                // Set the OverwriteOutput setting to True
                gp.OverwriteOutput = true;

                // Create a new instance of our custom model
                MYCUSTOMTOOLBOX.GolfFinder myModel = new MYCUSTOMTOOLBOX.GolfFinder();

                // Set the custom models parameters.
                myModel.BufferDistance = modelParametersHybridDictionary["BufferDistance"];
                myModel.AIRPORT = modelParametersHybridDictionary["Airports"];
                myModel.GOLF = modelParametersHybridDictionary["Golf"];
                myModel.AirportBuffer = modelParametersHybridDictionary["AirportBuffer"];
                myModel.Golf_Courses_Near_Airports = modelParametersHybridDictionary["GolfNearAirports"];

                // Execute the model and obtain the result from the run
                ESRI.ArcGIS.Geoprocessing.IGeoProcessorResult geoProcessorResult = (ESRI.ArcGIS.Geoprocessing.IGeoProcessorResult)gp.Execute(myModel, null);

                if (geoProcessorResult == null)
                {
                    // We have an error running the model. 
                    // If the run fails a Nothing (VB.NET) or null (C#) is returned from the gp.Execute
                    object sev = 2;
                    string messages = gp.GetMessages(ref sev);
                    return messages;
                }
                else
                {
                    // The model completed successfully
                    return "Output successful. The shapefiles are locacted at: " + geoProcessorResult.ReturnValue.ToString();
                }

            }
            catch (Exception ex)
            {
                // Catch any other errors
                return "Error running the model. Debug the application and test again.";
            }

        }   
    
    }
}
