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
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Location;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.Geometry;

namespace FindAddress
{
    public partial class AddressForm : Form
    {
        private AoInitialize m_license = null;

        public AddressForm()
        {
            GetLicense();

            InitializeComponent();

            ReturnLicence();
        }

        void StateTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            // pressing "enter" should do the same as clicking the button for locating
            if (e.KeyValue == 13)
                FindButton_Click(this, new System.EventArgs());
        }

        void ZipTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            // pressing "enter" should do the same as clicking the button for locating
            if (e.KeyValue == 13)
                FindButton_Click(this, new System.EventArgs());
        }

        private void FindButton_Click(object sender, EventArgs e)
        {
            GeocodeAddress();
        }

        private void GeocodeAddress()
        {
            // Get the locator
            System.Object obj = Activator.CreateInstance(Type.GetTypeFromProgID("esriLocation.LocatorManager"));
            ILocatorManager2 locatorManager = obj as ILocatorManager2;
            ILocatorWorkspace locatorWorkspace = locatorManager.GetLocatorWorkspaceFromPath(@"C:\California_fgdb.gdb");
            ILocator locator = locatorWorkspace.GetLocator("California_city_state_zip_94_new");

            // Set up the address properties
            IAddressInputs addressInputs = locator as IAddressInputs;
            IFields addressFields = addressInputs.AddressFields;
            IPropertySet addressProperties = new PropertySetClass();
            addressProperties.SetProperty(addressFields.get_Field(0).Name, this.AddressTextBox.Text);
            addressProperties.SetProperty(addressFields.get_Field(1).Name, this.CityTextBox.Text);
            addressProperties.SetProperty(addressFields.get_Field(2).Name, this.StateTextBox.Text);
            addressProperties.SetProperty(addressFields.get_Field(3).Name, this.ZipTextBox.Text);

            // Match the Address
            IAddressGeocoding addressGeocoding = locator as IAddressGeocoding;
            IPropertySet resultSet = addressGeocoding.MatchAddress(addressProperties);

            // Print out the results
            object names, values;
            resultSet.GetAllProperties(out names, out values);
            string[] namesArray = names as string[];
            object[] valuesArray = values as object[];
            int length = namesArray.Length;
            IPoint point = null;
            for (int i = 0; i < length; i++)
            {
                if (namesArray[i] != "Shape")
                    this.ResultsTextBox.Text += namesArray[i] + ": " + valuesArray[i].ToString() + "\n";
                else
                {
                    if (point != null && !point.IsEmpty)
                    {
                        point = valuesArray[i] as IPoint;
                        this.ResultsTextBox.Text += "X: " + point.X + "\n";
                        this.ResultsTextBox.Text += "Y: " + point.Y + "\n";
                    }
                }
            }

            this.ResultsTextBox.Text += "\n";
        }

        private void GetLicense()
        {
            if (!ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.Desktop))
                throw new Exception("Could not set version. ");

            m_license = new AoInitializeClass();
            m_license.Initialize(esriLicenseProductCode.esriLicenseProductCodeStandard);
        }

        private void ReturnLicence()
        {
            m_license.Shutdown();
        }
    }
}
