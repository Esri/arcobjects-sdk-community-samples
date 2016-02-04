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
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Runtime.InteropServices;

using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Display;

namespace EditingUsingCustomForm
{
    public sealed partial class MainForm : Form
    {
        #region private members
        private IMapControl3 m_mapControl = null;
        #endregion

        #region class constructor
        public MainForm()
        {
            InitializeComponent();
        }
        #endregion

        private void MainForm_Load(object sender, EventArgs e)
        {        
            m_mapControl = (IMapControl3) axMapControl1.Object;

            //relative file path to the sample data from EXE location
            string filePath = @"..\..\..\data\USAMajorHighways";
 
            //Add Lakes layer
            IWorkspaceFactory workspaceFactory = new ShapefileWorkspaceFactoryClass();
            IFeatureWorkspace workspace = (IFeatureWorkspace)workspaceFactory.OpenFromFile(filePath, axMapControl1.hWnd);
            IFeatureLayer featureLayer = new FeatureLayerClass();
            featureLayer.Name = "Lakes";
            featureLayer.Visible = true;
            featureLayer.FeatureClass = workspace.OpenFeatureClass("us_lakes");

            #region create a SimplerRenderer
            IRgbColor color = new RgbColorClass();
            color.Red = 190;
            color.Green = 232;
            color.Blue = 255;

            ISimpleFillSymbol sym = new SimpleFillSymbolClass();
            sym.Color = color;

            ISimpleRenderer renderer = new SimpleRendererClass();
            renderer.Symbol = sym as ISymbol;
            #endregion

            ((IGeoFeatureLayer)featureLayer).Renderer = renderer as IFeatureRenderer;
            axMapControl1.Map.AddLayer((ILayer)featureLayer);

            //Add Highways layer
            featureLayer = new FeatureLayerClass();
            featureLayer.Name = "Highways";
            featureLayer.Visible = true;
            featureLayer.FeatureClass = workspace.OpenFeatureClass("usa_major_highways");
            axMapControl1.Map.AddLayer((ILayer)featureLayer);

            //******** Important *************
            //store a reference to this form (Mainform) using the EditHelper class
            EditHelper.TheMainForm = this;
            EditHelper.IsEditorFormOpen = false;

            //add the EditCmd command to the toolbar
            axEditorToolbar.AddItem("esriControls.ControlsOpenDocCommand", 0, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
            axEditorToolbar.AddItem("esriControls.ControlsSaveAsDocCommand", 0, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
            axEditorToolbar.AddItem("esriControls.ControlsAddDataCommand", 0, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
            axEditorToolbar.AddItem(new EditCmd(), 0, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
             
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            //Warn users if the ArcGIS Engine samples used by this application have not been compiled
            ArrayList checkList = new ArrayList();
            checkList.Add("ReshapePolylineEditTask_CS.ReshapePolylineEditTask");
            checkList.Add("VertexCommands_CS.UsingOutOfBoxVertexCommands");

            Type t = null;
            bool success = true;

            foreach (string item in checkList)
            {
                t = Type.GetTypeFromProgID(item);

                if (t == null)
                {
                    success = false;
                    break;
                }
            }

            if (!success)
                MessageBox.Show("Editing will not function correctly until the C# ReshapePolylineEditTask and VertexCommands samples have been compiled. More information can be found in the 'How to use' section for this sample.",
                    "Warning",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            
        }


        #region public properties

        // Returns the MapControl
        public IMapControl3 MapControl
        {
            get { return m_mapControl; }
        }

        #endregion

       
        




    }
}