using ESRI.ArcGIS.Geometry;

namespace GlobeGraphicsToolbar
{
    public class PointGeometry
    {
        private IGeometry _geometry;

        public PointGeometry(double longitude, double latitude, double altitudeInKilometers, ISpatialReference spatialReference)
        {
            _geometry = GetGeometry(longitude, latitude, altitudeInKilometers, spatialReference);
        }

        private IGeometry GetGeometry(double longitude, double latitude, double altitudeInKilometers, ISpatialReference spatialReference)
        {
            IGeometry geometry;

            IPoint point = new PointClass();

            point.X = longitude;
            point.Y = latitude;
            point.Z = altitudeInKilometers;

            point.SpatialReference = spatialReference;

            geometry = point as IGeometry;

            MakeZAware(geometry);

            return geometry;
        }

        private void MakeZAware(IGeometry geometry)
        {
            IZAware zAware = geometry as IZAware;
            zAware.ZAware = true;
        }

        public IGeometry Geometry
        {
            get { return _geometry; }
        }
    }
}