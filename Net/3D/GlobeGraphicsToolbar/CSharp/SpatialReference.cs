using ESRI.ArcGIS.Geometry;

namespace GlobeGraphicsToolbar
{
    public class SpatialReferenceFactory
    {
        private ISpatialReference _spatialReference;

        public SpatialReferenceFactory(int xyCoordinateSystem)
        {
            _spatialReference = GetSpatialReference(xyCoordinateSystem);
        }

        private ISpatialReference GetSpatialReference(int xyCoordinateSystem)
        {
            const bool IsHighPrecision = true;

            ISpatialReference spatialReference;

            ISpatialReferenceFactory3 spatialReferenceFactory = new SpatialReferenceEnvironmentClass();
            spatialReference = spatialReferenceFactory.CreateSpatialReference(xyCoordinateSystem);

            IControlPrecision2 controlPrecision = spatialReference as IControlPrecision2;
            controlPrecision.IsHighPrecision = IsHighPrecision;

            ISpatialReferenceResolution spatialReferenceResolution = spatialReference as ISpatialReferenceResolution;
            spatialReferenceResolution.ConstructFromHorizon();
            spatialReferenceResolution.SetDefaultXYResolution();
            spatialReferenceResolution.SetDefaultZResolution();
            spatialReferenceResolution.SetDefaultMResolution();

            ISpatialReferenceTolerance spatialReferenceTolerance = spatialReference as ISpatialReferenceTolerance;
            spatialReferenceTolerance.SetDefaultXYTolerance();
            spatialReferenceTolerance.SetDefaultZTolerance();
            spatialReferenceTolerance.SetDefaultMTolerance();

            return spatialReference;
        }

        public ISpatialReference SpatialReference
        {
            get
            {
                return _spatialReference;
            }
        }

    }
}