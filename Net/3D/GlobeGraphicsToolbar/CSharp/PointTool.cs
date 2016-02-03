using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.GlobeCore;
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.Carto;

namespace GlobeGraphicsToolbar
{
    public class PointTool : ESRI.ArcGIS.Desktop.AddIns.Tool
    {
        private const int LeftButton = 1;
        private const esriSRGeoCSType GeographicCoordinateSystem = esriSRGeoCSType.esriSRGeoCS_WGS1984;
        private const double PointElementSize = 100000;
        private const esriSimple3DMarkerStyle PointElementStyle = esriSimple3DMarkerStyle.esriS3DMSSphere;
        private const string GraphicsLayerName = "Globe Graphics";

        public PointTool()
        {
        }

        protected override void OnUpdate()
        {
            Enabled = ArcGlobe.Application != null;
        }

        protected override void OnMouseUp(ESRI.ArcGIS.Desktop.AddIns.Tool.MouseEventArgs arg)
        {
            if (arg.Button == System.Windows.Forms.MouseButtons.Left)
            {
                GeographicCoordinates geographicCoordinates = new GeographicCoordinates(ArcGlobe.Globe, arg.X, arg.Y);

                SpatialReferenceFactory spatialReferenceFactory = new SpatialReferenceFactory((int)GeographicCoordinateSystem);

                PointGeometry pointGeometry = new PointGeometry(geographicCoordinates.Longitude, geographicCoordinates.Latitude, geographicCoordinates.AltitudeInKilometers, spatialReferenceFactory.SpatialReference);

                PointElement pointElement = new PointElement(pointGeometry.Geometry, PointElementSize, PointElementStyle);

                TableOfContents tableOfContents = new TableOfContents(ArcGlobe.Globe);

                if (!tableOfContents.LayerExists(GraphicsLayerName))
                {
                    tableOfContents.ConstructLayer(GraphicsLayerName);
                }

                Layer layer = new Layer(tableOfContents[GraphicsLayerName]);

                layer.AddElement(pointElement.Element, pointElement.ElementProperties);

                ArcGlobe.Globe.GlobeDisplay.RefreshViewers();
            }
        }
    }

}
