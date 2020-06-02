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