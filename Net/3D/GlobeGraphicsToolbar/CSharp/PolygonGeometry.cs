using ESRI.ArcGIS.Geometry;
using System;

namespace GlobeGraphicsToolbar
{
    public class PolygonGeometry
    {
        private IGeometry _geometry;

        public PolygonGeometry(ISpatialReference spatialReference)
        {
            _geometry = GetGeometry(spatialReference);
        }

        private IGeometry GetGeometry(ISpatialReference spatialReference)
        {
            IGeometry geometry;

            IPolygon polygon = new PolygonClass();

            polygon.SpatialReference = spatialReference;

            geometry = polygon as IGeometry;

            MakeZAware(geometry);

            return geometry;
        }

        private void MakeZAware(IGeometry geometry)
        {
            IZAware zAware = geometry as IZAware;
            zAware.ZAware = true;
        }

        public void AddPoint(IPoint point)
        {
            IPointCollection pointCollection = _geometry as IPointCollection;

            object missing = Type.Missing;

            pointCollection.AddPoint(point, ref missing, ref missing);
        }

        public void Close()
        {
            IPolygon polygon = _geometry as IPolygon;

            polygon.Close();
        }

        public IGeometry Geometry
        {
            get
            {
                return _geometry;
            }
        }

        public int PointCount
        {
            get
            {
                int pointCount;

                IPointCollection pointCollection = _geometry as IPointCollection;

                pointCount = pointCollection.PointCount;

                return pointCount;
            }
        }
    }
}