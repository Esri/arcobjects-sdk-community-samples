using ESRI.ArcGIS.GlobeCore;
using ESRI.ArcGIS.Analyst3D;

namespace GlobeGraphicsToolbar
{
    public class GeographicCoordinates
    {
        private double _longitude;
        private double _latitude;
        private double _altitudeInKilometers;

        public GeographicCoordinates(IGlobe globe, int screenX, int screenY)
        {
            GetGeographicCoordinates(globe, screenX, screenY, ref _longitude, ref _latitude, ref _altitudeInKilometers);
        }

        private void GetGeographicCoordinates(IGlobe globe, int screenX, int screenY, ref double longitude, ref double latitude, ref double altitudeInKilometers)
        {
            IGlobeDisplay globeDisplay = globe.GlobeDisplay;

            ISceneViewer sceneViewer = globeDisplay.ActiveViewer;

            ICamera camera = globeDisplay.ActiveViewer.Camera;

            IGlobeViewUtil globeViewUtil = camera as IGlobeViewUtil;

            globeViewUtil.WindowToGeographic(globeDisplay, sceneViewer, screenX, screenY, true, out longitude, out latitude, out altitudeInKilometers);
        }

        public double Longitude
        {
            get
            {
                return _longitude;
            }
        }

        public double Latitude
        {
            get
            {
                return _latitude;
            }
        }

        public double AltitudeInKilometers
        {
            get
            {
                return _altitudeInKilometers;
            }
        }    
    }
}