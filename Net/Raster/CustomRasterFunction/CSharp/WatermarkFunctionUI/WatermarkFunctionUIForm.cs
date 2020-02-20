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

namespace CustomFunctionUI
{
    public partial class WatermarkFunctionUIForm : Form
    {
        #region Private Members
        private object myInputRaster;
        private string myWaterMarkImagePath;
        private double myBlendPercentage;
        private CustomFunction.esriWatermarkLocation myWatermarkLocation;
        private bool myDirtyFlag;
        #endregion

        #region WatermarkFunctionUIForm Properties
        /// <summary>
        /// Constructor
        /// </summary>
        public WatermarkFunctionUIForm()
        {
            InitializeComponent();
            myInputRaster = null;
            myWaterMarkImagePath = "";
            myBlendPercentage = 0.0;
            myWatermarkLocation = CustomFunction.esriWatermarkLocation.esriWatermarkBottomRight;
        }

        /// <summary>
        /// Get or set the watermark image path
        /// </summary>
        public string WatermarkImagePath
        {
            get
            {
                myWaterMarkImagePath = watermarkImageTxtbox.Text;
                return myWaterMarkImagePath;
            }
            set
            {
                myWaterMarkImagePath = value;
                watermarkImageTxtbox.Text = value;
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

        /// <summary>
        /// Get or set the blending percentage
        /// </summary>
        public double BlendPercentage
        {
            get
            {
                if (blendPercentTxtbox.Text == "")
                    blendPercentTxtbox.Text = "50.00";
                myBlendPercentage = Convert.ToDouble(blendPercentTxtbox.Text);
                return myBlendPercentage;
            }
            set
            {
                myBlendPercentage = value;
                blendPercentTxtbox.Text = Convert.ToString(value);
            }
        }

        /// <summary>
        /// Get or set the watermark location.
        /// </summary>
        public CustomFunction.esriWatermarkLocation WatermarkLocation
        {
            get
            {
                return myWatermarkLocation;
            }
            set
            {
                myWatermarkLocation = value;
            }
        }
        #endregion

        #region WatermarkFunctionUIForm Members
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
            blendPercentTxtbox.Text = Convert.ToString(myBlendPercentage);
            watermarkImageTxtbox.Text = myWaterMarkImagePath;
            LocationComboBx.SelectedIndex = (int)myWatermarkLocation;
        }

        private void inputRasterBtn_Click(object sender, EventArgs e)
        {
            IEnumGxObject ipSelectedObjects = null;
            ShowRasterDatasetBrowser((int)(Handle.ToInt32()), out ipSelectedObjects);

            IGxObject selectedObject =  ipSelectedObjects.Next();
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

        private void LocationComboBx_SelectedIndexChanged(object sender, EventArgs e)
        {
            myWatermarkLocation = (CustomFunction.esriWatermarkLocation)LocationComboBx.SelectedIndex;
            myDirtyFlag = true;
        }
        
        private void watermarkImageBtn_Click(object sender, EventArgs e)
        {
            watermarkImageDlg.ShowDialog();
            if (watermarkImageDlg.FileName != "")
            {
                watermarkImageTxtbox.Text = watermarkImageDlg.FileName;
                myDirtyFlag = true;
            }
        }

        private void blendPercentTxtbox_ModifiedChanged(object sender, EventArgs e)
        {
            if (blendPercentTxtbox.Text != "")
            {
                myBlendPercentage = Convert.ToDouble(blendPercentTxtbox.Text);
                myDirtyFlag = true;
            }
        }
        #endregion

    }
}
