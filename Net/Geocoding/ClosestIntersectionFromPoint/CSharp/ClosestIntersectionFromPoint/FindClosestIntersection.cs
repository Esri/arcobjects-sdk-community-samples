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
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Location;
using ESRI.ArcGIS.esriSystem;

namespace ClosestIntersectionFromPoint
{
    class FindClosestIntersection
    {
        private static IAoInitialize m_License = null;

        // X and Y values can either be passed in at the command line or from the command prompt after starting
        static void Main(string[] args)
        {
            // Initialize the license
            getLicense();

            // See if the address point was passed in at the command line and if not, enter the values now
            double X, Y;
            if (args == null || args.Length == 0)
            {
                Console.WriteLine("Enter a X value: ");
                X = double.Parse(Console.ReadLine());
                Console.WriteLine("Enter a Y value: ");
                Y = double.Parse(Console.ReadLine());
            }
            else
            {
                X = double.Parse(args[0]);
                Y = double.Parse(args[1]);
            }

            // Call the reverseGeocode method
            ReverseGeocodeIntersection(X, Y);

            Console.WriteLine("Press any key to exit...");
            Console.ReadLine();

            // Return the license
            returnLicense();
        }

        /// <summary>
        /// The main functionality to use Intersection Reverse Geocoding 
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        private static void ReverseGeocodeIntersection(double X, double Y)
        {
            // Get a locator from the locator Workspace
            System.Object obj = Activator.CreateInstance(Type.GetTypeFromProgID("esriLocation.LocatorManager"));
            ILocatorManager2 locatorManager = obj as ILocatorManager2;
            ILocatorWorkspace locatorWorkspace = locatorManager.GetLocatorWorkspaceFromPath(@"C:\California_fdb.gdb");
            ILocator locator = locatorWorkspace.GetLocator("California_streets_10");
            IReverseGeocoding reverseGeocoding = locator as IReverseGeocoding;

            // Get the spatial reference from the locator
            IAddressGeocoding addressGeocoding = locator as IAddressGeocoding;
            IFields matchFields = addressGeocoding.MatchFields;
            IField shapeField = matchFields.get_Field(matchFields.FindField("Shape"));
            ISpatialReference spatialReference = shapeField.GeometryDef.SpatialReference;

            // Set up the point from the X and Y values
            IPoint point = new PointClass();
            point.SpatialReference = spatialReference;
            point.X = X;
            point.Y = Y;

            // Set the search tolerance for reverse geocoding
            IReverseGeocodingProperties reverseGeocodingProperties = reverseGeocoding as IReverseGeocodingProperties;
            reverseGeocodingProperties.SearchDistance = 2;
            reverseGeocodingProperties.SearchDistanceUnits = esriUnits.esriKilometers;

            // Determine if the locator supports intersection geocoding.
            // intersectionGeocoding will be null if it is not supported.
            IIntersectionGeocoding intersectionGeocoding = locator as IIntersectionGeocoding;
            if (intersectionGeocoding == null)
            {
                Console.WriteLine("You must use a locator that supports intersections.  Use a locator that was built off of one"
                    + "of the US Streets Locator styles.");
            }
            else
            {
                // Find the intersection that is nearest to the Point
                IPropertySet addressProperties = reverseGeocoding.ReverseGeocode(point, true);

                // Print the intersection properties
                IAddressInputs addressInputs = reverseGeocoding as IAddressInputs;
                IFields addressFields = addressInputs.AddressFields;
                for (int i = 0; i < addressFields.FieldCount; i++)
                {
                    IField addressField = addressFields.get_Field(i);
                    Console.WriteLine(addressField.AliasName + ": " + addressProperties.GetProperty(addressField.Name));
                }
            }
        }

        #region Helper Methods

        private static void getLicense()
        {
            if (!ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.Desktop))
                throw new Exception("Could not set version. ");
            m_License = new AoInitializeClass();
            m_License.Initialize(esriLicenseProductCode.esriLicenseProductCodeAdvanced);
        }

        private static void returnLicense()
        {
            m_License.Shutdown();
        }

        #endregion
    }
}
