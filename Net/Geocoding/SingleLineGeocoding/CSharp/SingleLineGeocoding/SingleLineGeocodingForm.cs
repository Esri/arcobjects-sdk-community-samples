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
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Location;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;

namespace SingleLineGeocoding
{
    public partial class SingleLineGeocodingForm : Form
    {
        private AoInitialize m_license = null;
        private ILocator m_locator = null;
        private String[] m_addressFields;
        private String m_orgAddrLabel = "Address";

        public SingleLineGeocodingForm()
        {
            GetLicense();

            InitializeComponent();

            ReturnLicence();
        }

        private void GeocodeAddress(IPropertySet addressProperties)
        {
            // Match the Address
            IAddressGeocoding addressGeocoding = m_locator as IAddressGeocoding;
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

        private void locatorButton_Click(object sender, EventArgs e)
        {
            addressLabel.Text = m_orgAddrLabel;

            DialogResult result = openFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                String locatorPath = openFileDialog.FileName;
                locatorPath = locatorPath.Substring(0, locatorPath.LastIndexOf('.'));
                if (locatorPath != null && locatorPath != "")
                {
                    locatorTextBox.Text = locatorPath;
                    addressTextBox.Enabled = true;

                    // Open the workspace
                    String workspaceName = locatorPath.Substring(0, locatorPath.LastIndexOf("\\"));
                    String locatorName = locatorPath.Substring(locatorPath.LastIndexOf("\\") + 1);
                    

                    // Get the locator
                    System.Object obj = Activator.CreateInstance(Type.GetTypeFromProgID("esriLocation.LocatorManager"));
                    ILocatorManager2 locatorManager = obj as ILocatorManager2;
                    ILocatorWorkspace locatorWorkspace = locatorManager.GetLocatorWorkspaceFromPath(workspaceName);
                    m_locator = locatorWorkspace.GetLocator(locatorName);

                    m_addressFields = get_AddressFields();
                    addressLabel.Text += " (" + String.Join(",", m_addressFields) + ")";
                }
            }
        }

        private void findButton_Click(object sender, EventArgs e)
        {
            String[] addressValues = addressTextBox.Text.Split(',');
            if (addressValues.Length == m_addressFields.Length)
            {
                IPropertySet addressProperties = createAddressProperties(m_addressFields, addressValues);
                GeocodeAddress(addressProperties);
            }
            else if(m_addressFields.Length == 1)
            {
                IPropertySet addressProperties = createAddressProperties(m_addressFields, addressValues);
                GeocodeAddress(addressProperties);
            }
            else
            {
                MessageBox.Show("Your address needs a comma between each expected address field or just commas to delimit those fields ",
                    "Address Input Error");
            }
        }

        void addressTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            // pressing "enter" should do the same as clicking the button for locating
            if (e.KeyValue == 13)
                findButton_Click(this, new System.EventArgs());
        }

        /// <summary>
        /// Get the address fields for the locator
        /// </summary>
        /// <param name="locator"></param>
        /// <returns>A String array of address fields</returns>
        private String[] get_AddressFields()
        {
            ISingleLineAddressInput singleLineInput = m_locator as ISingleLineAddressInput;
            IAddressInputs addressInputs = null;
            String[] fields;
            if (singleLineInput != null)
            {
                IField singleField = singleLineInput.SingleLineAddressField;
                fields = new String[] { singleField.Name };
            }
            else
            {
                addressInputs = m_locator as IAddressInputs;
                IFields multiFields = addressInputs.AddressFields;
                int fieldCount = multiFields.FieldCount;
                fields = new String[fieldCount];
                for (int i = 0; i < fieldCount; i++)
                {
                    fields[i] = multiFields.get_Field(i).Name;
                }
            }
            return fields;
        }

        /// <summary>
        /// Create a propertySet of address fields and values
        /// </summary>
        /// <param name="addressFields"></param>
        /// <param name="addressValues"></param>
        /// <returns>A propertySet that contains address fields and address values that correspond to the fields.</returns>
        private IPropertySet createAddressProperties(String[] addressFields, String[] addressValues)
        {
            int fieldCount = addressFields.Length;
            if (fieldCount > 1 && fieldCount != addressValues.Length)
                throw new Exception("There must be the same amount of address fields as address values. ");

            IPropertySet propertySet = new PropertySetClass();
            for (int i = 0; i < fieldCount; i++)
            {
                propertySet.SetProperty(addressFields[i], addressValues[i]);
            }
            return propertySet;
        }

        private void GetLicense()
        {
            if (!ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.Desktop))
                throw new Exception("Could not bind to license manager. ");

            m_license = new AoInitializeClass();
            m_license.Initialize(esriLicenseProductCode.esriLicenseProductCodeAdvanced);
        }

        private void ReturnLicence()
        {
            m_license.Shutdown();
        }
    }
}
