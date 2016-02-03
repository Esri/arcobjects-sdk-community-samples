using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using System.Windows.Forms;
using Microsoft.Win32;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.Geometry;

namespace GlobeGraphicsToolbar
{
    public class StyleTool : ESRI.ArcGIS.Desktop.AddIns.Tool
    {
        private const int LeftButton = 1;
        private const esriSRGeoCSType GeographicCoordinateSystem = esriSRGeoCSType.esriSRGeoCS_WGS1984;
        private const double StyleElementSize = 50000;
        private const string GraphicsLayerName = "Globe Graphics";

        public StyleTool()
        {
        }

        protected override void OnUpdate()
        {

        }

        protected override void OnMouseUp(ESRI.ArcGIS.Desktop.AddIns.Tool.MouseEventArgs arg)
        {
            if (arg.Button == MouseButtons.Left)
            {
                GeographicCoordinates geographicCoordinates = new GeographicCoordinates(ArcGlobe.Globe, arg.X, arg.Y);

                SpatialReferenceFactory spatialReferenceFactory = new SpatialReferenceFactory((int)GeographicCoordinateSystem);

                PointGeometry pointGeometry = new PointGeometry(geographicCoordinates.Longitude, geographicCoordinates.Latitude, geographicCoordinates.AltitudeInKilometers, spatialReferenceFactory.SpatialReference);

                IStyleGalleryItem styleGalleryItem = StyleGallerySelection.GetStyleGalleryItem();

                if (styleGalleryItem != null)
                {
                    StyleElement styleElement = new StyleElement(pointGeometry.Geometry, StyleElementSize, styleGalleryItem);

                    TableOfContents tableOfContents = new TableOfContents(ArcGlobe.Globe);

                    if (!tableOfContents.LayerExists(GraphicsLayerName))
                    {
                        tableOfContents.ConstructLayer(GraphicsLayerName);
                    }

                    Layer layer = new Layer(tableOfContents[GraphicsLayerName]);

                    layer.AddElement(styleElement.Element, styleElement.ElementProperties);

                    ArcGlobe.Globe.GlobeDisplay.RefreshViewers();
                }
            }
        }
    }

}
