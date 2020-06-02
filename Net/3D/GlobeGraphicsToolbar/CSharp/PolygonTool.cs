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
using ESRI.ArcGIS.Display;

namespace GlobeGraphicsToolbar
{
    public class PolygonTool : ESRI.ArcGIS.Desktop.AddIns.Tool
    {
        private PolygonGeometry _polygonGeometry = null;
        private const int LeftButton = 1;
        private const esriSRGeoCSType GeographicCoordinateSystem = esriSRGeoCSType.esriSRGeoCS_WGS1984;
        private const double PointElementSize = 1;
        private const esriSimpleMarkerStyle PointElementStyle = esriSimpleMarkerStyle.esriSMSCircle;
        private const double PolylineElementWidth = 1000;
        private const esriSimpleLineStyle PolylineElementStyle = esriSimpleLineStyle.esriSLSSolid;
        private const string GraphicsLayerName = "Globe Graphics";
        public PolygonTool()
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

                if (_polygonGeometry == null)
                {
                    _polygonGeometry = new PolygonGeometry(spatialReferenceFactory.SpatialReference);
                }

                _polygonGeometry.AddPoint(pointGeometry.Geometry as IPoint);

                TableOfContents tableOfContents = new TableOfContents(ArcGlobe.Globe);

                if (!tableOfContents.LayerExists(GraphicsLayerName))
                {
                    tableOfContents.ConstructLayer(GraphicsLayerName);
                }

                Layer layer = new Layer(tableOfContents[GraphicsLayerName]);

                if (_polygonGeometry.PointCount == 1)
                {
                    PointElement pointElement = new PointElement(pointGeometry.Geometry, PointElementSize, PointElementStyle);

                    layer.AddElement(pointElement.Element, pointElement.ElementProperties);
                }
                else
                {
                    layer.RemoveElement(layer.ElementCount - 1);

                    PolylineGeometry polylineGeometry = new PolylineGeometry(_polygonGeometry.Geometry);

                    PolylineElement polylineElement = new PolylineElement(polylineGeometry.Geometry, PolylineElementWidth, PolylineElementStyle);

                    layer.AddElement(polylineElement.Element, polylineElement.ElementProperties);
                }

                ArcGlobe.Globe.GlobeDisplay.RefreshViewers();
            }
        }

        protected override void OnDoubleClick()
        {
            if (_polygonGeometry.PointCount > 2)
            {
                TableOfContents tableOfContents = new TableOfContents(ArcGlobe.Globe);

                if (tableOfContents.LayerExists("Globe Graphics"))
                {
                    Layer layer = new Layer(tableOfContents["Globe Graphics"]);

                    layer.RemoveElement(layer.ElementCount - 1);

                    _polygonGeometry.Close();

                    PolygonElement polygonElement = new PolygonElement(_polygonGeometry.Geometry, esriSimpleFillStyle.esriSFSSolid);

                    layer.AddElement(polygonElement.Element, polygonElement.ElementProperties);

                    _polygonGeometry = null;

                    ArcGlobe.Globe.GlobeDisplay.RefreshViewers();
                }
            }
        }
    }

}
