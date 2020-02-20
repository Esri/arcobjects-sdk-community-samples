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
using System.Text;
using System.IO;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using System.Windows.Forms;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.GlobeCore;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using Microsoft.VisualBasic;

namespace GlobeGraphicsToolbar
{
    public class TextTool : ESRI.ArcGIS.Desktop.AddIns.Tool
    {
        private const int LeftButton = 1;
        private const esriSRGeoCSType GeographicCoordinateSystem = esriSRGeoCSType.esriSRGeoCS_WGS1984;
        private const float TextElementSize = 10;
        private const string GraphicsLayerName = "Globe Graphics";
        private object _missing = Type.Missing;

        public TextTool()
        {
        }

        protected override void OnUpdate()
        {

        }

        protected override void OnMouseDown(ESRI.ArcGIS.Desktop.AddIns.Tool.MouseEventArgs arg)
        {
            if (arg.Button == MouseButtons.Left)
            {
                GeographicCoordinates geographicCoordinates = new GeographicCoordinates(ArcGlobe.Globe, arg.X, arg.Y);

                SpatialReferenceFactory spatialReferenceFactory = new SpatialReferenceFactory((int)GeographicCoordinateSystem);

                PointGeometry pointGeometry = new PointGeometry(geographicCoordinates.Longitude, geographicCoordinates.Latitude, geographicCoordinates.AltitudeInKilometers, spatialReferenceFactory.SpatialReference);

                TextForm textForm = new TextForm();

                DialogResult dialogResult = textForm.ShowDialog();

                if (textForm.InputText.Length > 0)
                {
                    TextElement textElement = new TextElement(pointGeometry.Geometry, textForm.InputText, TextElementSize);

                    TableOfContents tableOfContents = new TableOfContents(ArcGlobe.Globe);

                    if (!tableOfContents.LayerExists(GraphicsLayerName))
                    {
                        tableOfContents.ConstructLayer(GraphicsLayerName);
                    }

                    Layer layer = new Layer(tableOfContents[GraphicsLayerName]);

                    layer.AddElement(textElement.Element, textElement.ElementProperties);

                    ArcGlobe.Globe.GlobeDisplay.RefreshViewers();
                }
            }
        }
    }

}
