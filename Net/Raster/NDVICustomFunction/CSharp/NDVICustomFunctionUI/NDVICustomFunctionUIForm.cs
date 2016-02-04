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
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Catalog;
using ESRI.ArcGIS.CatalogUI;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.esriSystem;

namespace NDVICustomFunctionUI
{
    public partial class NDVICustomFunctionUIForm : Form
    {
        #region Private Members
        private object myInputRaster;
        private string myBandIndices;
        private bool myDirtyFlag;
        #endregion

        #region NDVICustomFunctionUIForm Properties
        /// <summary>
        /// Constructor
        /// </summary>
        public NDVICustomFunctionUIForm()
        {
            InitializeComponent();
            myInputRaster = null;
            myBandIndices = "";
            HintLbl.Text = "NIR Red";
        }

        /// <summary>
        /// Get or set the band indices.
        /// </summary>
        public string BandIndices
        {
            get
            {
                myBandIndices = BandIndicesTxtBox.Text;
                return myBandIndices;
            }
            set
            {
                myBandIndices = value;
                BandIndicesTxtBox.Text = value;
            }
        }

        /// <summary>
        /// Flag to specify if the form has changed
        /// </summary>
        public bool IsFormDirty
        {
            get
            {
                return myDirtyFlag;
            }
            set
            {
                myDirtyFlag = value;
            }
        }

        /// <summary>
        /// Get or set the input raster
        /// </summary>
        public object InputRaster
        {
            get
            {
                return myInputRaster;
            }
            set
            {
                myInputRaster = value;
                inputRasterTxtbox.Text = GetInputRasterName(myInputRaster);
            }
        }

        #endregion

        #region NDVICustomFunctionUIForm Members

        /// <summary>
        /// This function takes a raster object and returns the formatted name of  
        /// the object for display in the UI.
        /// </summary>
        /// <param name="inputRaster">Object whose name is to be found</param>
        /// <returns>Name of the object</returns>
        private string GetInputRasterName(object inputRaster)
        {
            if ((inputRaster is IRasterDataset))
            {
                IRasterDataset rasterDataset = (IRasterDataset)inputRaster;
                return rasterDataset.CompleteName;
            }

            if ((inputRaster is IRaster))
            {
                IRaster myRaster = (IRaster)inputRaster;
                return ((IRaster2)myRaster).RasterDataset.CompleteName;
            }

            if (inputRaster is IDataset)
            {
                IDataset dataset = (IDataset)inputRaster;
                return dataset.Name;
            }

            if (inputRaster is IName)
            {
                if (inputRaster is IDatasetName)
                {
                    IDatasetName inputDSName = (IDatasetName)inputRaster;
                    return inputDSName.Name;
                }

                if (inputRaster is IFunctionRasterDatasetName)
                {
                    IFunctionRasterDatasetName inputFRDName = (IFunctionRasterDatasetName)inputRaster;
                    return inputFRDName.BrowseName;
                }

                if (inputRaster is IMosaicDatasetName)
                {
                    IMosaicDatasetName inputMDName = (IMosaicDatasetName)inputRaster;
                    return "MD";
                }

                IName inputName = (IName)inputRaster;
                return inputName.NameString;
            }

            if (inputRaster is IRasterFunctionTemplate)
            {
                IRasterFunctionTemplate rasterFunctionTemplate =
                    (IRasterFunctionTemplate)inputRaster;
                return rasterFunctionTemplate.Function.Name;
            }

            if (inputRaster is IRasterFunctionVariable)
            {
                IRasterFunctionVariable rasterFunctionVariable =
                    (IRasterFunctionVariable)inputRaster;
                return rasterFunctionVariable.Name;
            }

            return "";
        }

        /// <summary>
        /// Updates the UI textboxes using the properties that have been set.
        /// </summary>
        public void UpdateUI()
        {
            if (myInputRaster != null)
                inputRasterTxtbox.Text = GetInputRasterName(myInputRaster);
            BandIndicesTxtBox.Text = myBandIndices;
        }

        private void inputRasterBtn_Click(object sender, EventArgs e)
        {
            IEnumGxObject ipSelectedObjects = null;
            ShowRasterDatasetBrowser((int)(Handle.ToInt32()), out ipSelectedObjects);

            IGxObject selectedObject = ipSelectedObjects.Next();
            if (selectedObject is IGxDataset)
            {
                IGxDataset ipGxDS = (IGxDataset)selectedObject;
                IDataset ipDataset;
                ipDataset = ipGxDS.Dataset;
                myInputRaster = ipDataset.FullName;
                inputRasterTxtbox.Text = GetInputRasterName(myInputRaster);
                myDirtyFlag = true;
            }
        }

        public void ShowRasterDatasetBrowser(int handle, out IEnumGxObject ipSelectedObjects)
        {
            IGxObjectFilterCollection ipFilterCollection = new GxDialogClass();

            IGxObjectFilter ipFilter1 = new GxFilterRasterDatasetsClass();
            ipFilterCollection.AddFilter(ipFilter1, true);
            IGxDialog ipGxDialog = (IGxDialog)(ipFilterCollection);

            ipGxDialog.RememberLocation = true;
            ipGxDialog.Title = "Open";

            ipGxDialog.AllowMultiSelect = false;
            ipGxDialog.RememberLocation = true;

            ipGxDialog.DoModalOpen((int)(Handle.ToInt32()), out ipSelectedObjects);
            return;
        }

        private void BandIndicesTxtBox_TextChanged(object sender, EventArgs e)
        {
            myBandIndices = BandIndicesTxtBox.Text;
            myDirtyFlag = true;
        }

        #endregion
    }
}
