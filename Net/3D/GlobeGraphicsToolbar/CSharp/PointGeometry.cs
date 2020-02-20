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