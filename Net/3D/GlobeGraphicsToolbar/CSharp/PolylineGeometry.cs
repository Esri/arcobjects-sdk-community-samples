using ESRI.ArcGIS.Geometry;
using System;

namespace GlobeGraphicsToolbar
{
    public class PolylineGeometry
    {
        private IGeometry _geometry;

        public PolylineGeometry(ISpatialReference spatialReference)
        {
            _geometry = GetGeometry(spatialReference);
        }

        public PolylineGeometry(IGeometry baseGeometry)
        {
            _geometry = GetGeometry(baseGeometry);
        }

        private IGeometry GetGeometry(ISpatialReference spatialReference)
        {
            IGeometry geometry;

            IPolyline polyline = new PolylineClass();

            polyline.SpatialReference = spatialReference;

            geometry = polyline as IGeometry;

            MakeZAware(geometry);

            return geometry;
        }

        private IGeometry GetGeometry(IGeometry baseGeometry)
        {
            IGeometry geometry;

            IPolyline polyline = new PolylineClass();

            polyline.SpatialReference = baseGeometry.SpatialReference;

            geometry = polyline as IGeometry;
            
            IPointCollection targetPointCollection = geometry as IPointCollection;

            IPointCollection basePointCollection = baseGeometry as IPointCollection;

            object missing = Type.Missing;

            for (int i = 0; i < basePointCollection.PointCount; i++)
            {
                targetPointCollection.AddPoint(basePointCollection.get_Point(i), ref missing, ref missing);
            }

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