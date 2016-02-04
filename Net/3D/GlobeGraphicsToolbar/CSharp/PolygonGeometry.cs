/*

   Copyright 2016 Esri

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