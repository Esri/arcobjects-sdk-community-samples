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