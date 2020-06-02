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
using System;
using ESRI.ArcGIS.Geometry;

namespace MultiPatchExamples
{
    public static class RingGroupExamples
    {
        private static object _missing = Type.Missing;

        public static IGeometry GetExample1()
        {
            //RingGroup: Multiple Rings

            IGeometryCollection multiPatchGeometryCollection = new MultiPatchClass();

            //Ring 1

            IPointCollection ring1PointCollection = new RingClass();
            ring1PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(1, 1, 0), ref _missing, ref _missing);
            ring1PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(1, 4, 0), ref _missing, ref _missing);
            ring1PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(4, 4, 0), ref _missing, ref _missing);
            ring1PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(4, 1, 0), ref _missing, ref _missing);

            IRing ring1 = ring1PointCollection as IRing;
            ring1.Close();

            multiPatchGeometryCollection.AddGeometry(ring1 as IGeometry, ref _missing, ref _missing);

            //Ring 2

            IPointCollection ring2PointCollection = new RingClass();
            ring2PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(1, -1, 0), ref _missing, ref _missing);
            ring2PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(4, -1, 0), ref _missing, ref _missing);
            ring2PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(4, -4, 0), ref _missing, ref _missing);
            ring2PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(1, -4, 0), ref _missing, ref _missing);

            IRing ring2 = ring2PointCollection as IRing;
            ring2.Close();

            multiPatchGeometryCollection.AddGeometry(ring2 as IGeometry, ref _missing, ref _missing);

            //Ring 3

            IPointCollection ring3PointCollection = new RingClass();
            ring3PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-1, 1, 0), ref _missing, ref _missing);
            ring3PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-4, 1, 0), ref _missing, ref _missing);
            ring3PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-4, 4, 0), ref _missing, ref _missing);
            ring3PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-1, 4, 0), ref _missing, ref _missing);

            IRing ring3 = ring3PointCollection as IRing;
            ring3.Close();

            multiPatchGeometryCollection.AddGeometry(ring3 as IGeometry, ref _missing, ref _missing);

            //Ring 4

            IPointCollection ring4PointCollection = new RingClass();
            ring4PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-1, -1, 0), ref _missing, ref _missing);
            ring4PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-1, -4, 0), ref _missing, ref _missing);
            ring4PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-4, -4, 0), ref _missing, ref _missing);
            ring4PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-4, -1, 0), ref _missing, ref _missing);

            IRing ring4 = ring4PointCollection as IRing;
            ring4.Close();

            multiPatchGeometryCollection.AddGeometry(ring4 as IGeometry, ref _missing, ref _missing);

            return multiPatchGeometryCollection as IGeometry;
        }

        public static IGeometry GetExample2()
        {
            //RingGroup: Multiple Exterior Rings With Corresponding Interior Rings

            IGeometryCollection multiPatchGeometryCollection = new MultiPatchClass();

            IMultiPatch multiPatch = multiPatchGeometryCollection as IMultiPatch;

            //Exterior Ring 1

            IPointCollection exteriorRing1PointCollection = new RingClass();
            exteriorRing1PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(1, 1, 0), ref _missing, ref _missing);
            exteriorRing1PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(1, 4, 0), ref _missing, ref _missing);
            exteriorRing1PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(4, 4, 0), ref _missing, ref _missing);
            exteriorRing1PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(4, 1, 0), ref _missing, ref _missing);

            IRing exteriorRing1 = exteriorRing1PointCollection as IRing;
            exteriorRing1.Close();

            multiPatchGeometryCollection.AddGeometry(exteriorRing1 as IGeometry, ref _missing, ref _missing);

            multiPatch.PutRingType(exteriorRing1, esriMultiPatchRingType.esriMultiPatchOuterRing);

            //Interior Ring 1

            IPointCollection interiorRing1PointCollection = new RingClass();
            interiorRing1PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(3.5, 1.5, 0), ref _missing, ref _missing);
            interiorRing1PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(3.5, 3.5, 0), ref _missing, ref _missing);
            interiorRing1PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(1.5, 3.5, 0), ref _missing, ref _missing);
            interiorRing1PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(1.5, 1.5, 0), ref _missing, ref _missing);

            IRing interiorRing1 = interiorRing1PointCollection as IRing;
            interiorRing1.Close();

            multiPatchGeometryCollection.AddGeometry(interiorRing1 as IGeometry, ref _missing, ref _missing);

            multiPatch.PutRingType(interiorRing1, esriMultiPatchRingType.esriMultiPatchInnerRing);

            //Exterior Ring 2

            IPointCollection exteriorRing2PointCollection = new RingClass();
            exteriorRing2PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(1, -1, 0), ref _missing, ref _missing);
            exteriorRing2PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(4, -1, 0), ref _missing, ref _missing);
            exteriorRing2PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(4, -4, 0), ref _missing, ref _missing);
            exteriorRing2PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(1, -4, 0), ref _missing, ref _missing);

            IRing exteriorRing2 = exteriorRing2PointCollection as IRing;
            exteriorRing2.Close();

            multiPatchGeometryCollection.AddGeometry(exteriorRing2 as IGeometry, ref _missing, ref _missing);

            multiPatch.PutRingType(exteriorRing2, esriMultiPatchRingType.esriMultiPatchOuterRing);

            //Interior Ring 2

            IPointCollection interiorRing2PointCollection = new RingClass();
            interiorRing2PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(1.5, -1.5, 0), ref _missing, ref _missing);
            interiorRing2PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(3.5, -1.5, 0), ref _missing, ref _missing);
            interiorRing2PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(3.5, -3.5, 0), ref _missing, ref _missing);
            interiorRing2PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(1.5, -3.5, 0), ref _missing, ref _missing);

            IRing interiorRing2 = interiorRing2PointCollection as IRing;
            interiorRing2.Close();

            multiPatchGeometryCollection.AddGeometry(interiorRing2 as IGeometry, ref _missing, ref _missing);

            multiPatch.PutRingType(interiorRing2, esriMultiPatchRingType.esriMultiPatchInnerRing);

            //Exterior Ring 3

            IPointCollection exteriorRing3PointCollection = new RingClass();
            exteriorRing3PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-1, 1, 0), ref _missing, ref _missing);
            exteriorRing3PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-4, 1, 0), ref _missing, ref _missing);
            exteriorRing3PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-4, 4, 0), ref _missing, ref _missing);
            exteriorRing3PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-1, 4, 0), ref _missing, ref _missing);

            IRing exteriorRing3 = exteriorRing3PointCollection as IRing;
            exteriorRing3.Close();

            multiPatchGeometryCollection.AddGeometry(exteriorRing3 as IGeometry, ref _missing, ref _missing);

            multiPatch.PutRingType(exteriorRing3, esriMultiPatchRingType.esriMultiPatchOuterRing);

            //Interior Ring 3

            IPointCollection interiorRing3PointCollection = new RingClass();
            interiorRing3PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-1.5, 1.5, 0), ref _missing, ref _missing);
            interiorRing3PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-3.5, 1.5, 0), ref _missing, ref _missing);
            interiorRing3PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-3.5, 3.5, 0), ref _missing, ref _missing);
            interiorRing3PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-1.5, 3.5, 0), ref _missing, ref _missing);

            IRing interiorRing3 = interiorRing3PointCollection as IRing;
            interiorRing3.Close();

            multiPatchGeometryCollection.AddGeometry(interiorRing3 as IGeometry, ref _missing, ref _missing);

            multiPatch.PutRingType(interiorRing3, esriMultiPatchRingType.esriMultiPatchInnerRing);

            //Exterior Ring 4

            IPointCollection exteriorRing4PointCollection = new RingClass();
            exteriorRing4PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-1, -1, 0), ref _missing, ref _missing);
            exteriorRing4PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-1, -4, 0), ref _missing, ref _missing);
            exteriorRing4PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-4, -4, 0), ref _missing, ref _missing);
            exteriorRing4PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-4, -1, 0), ref _missing, ref _missing);

            IRing exteriorRing4 = exteriorRing4PointCollection as IRing;
            exteriorRing4.Close();

            multiPatchGeometryCollection.AddGeometry(exteriorRing4 as IGeometry, ref _missing, ref _missing);

            multiPatch.PutRingType(exteriorRing4, esriMultiPatchRingType.esriMultiPatchOuterRing);

            //Interior Ring 4

            IPointCollection interiorRing4PointCollection = new RingClass();
            interiorRing4PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-1.5, -1.5, 0), ref _missing, ref _missing);
            interiorRing4PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-1.5, -3.5, 0), ref _missing, ref _missing);
            interiorRing4PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-3.5, -3.5, 0), ref _missing, ref _missing);
            interiorRing4PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-3.5, -1.5, 0), ref _missing, ref _missing);

            IRing interiorRing4 = interiorRing4PointCollection as IRing;
            interiorRing4.Close();

            multiPatchGeometryCollection.AddGeometry(interiorRing4 as IGeometry, ref _missing, ref _missing);

            multiPatch.PutRingType(interiorRing4, esriMultiPatchRingType.esriMultiPatchInnerRing);

            return multiPatchGeometryCollection as IGeometry;
        }

        public static IGeometry GetExample3()
        {
            //RingGroup: Upright Square With Hole

            IGeometryCollection multiPatchGeometryCollection = new MultiPatchClass();

            IMultiPatch multiPatch = multiPatchGeometryCollection as IMultiPatch;

            //Exterior Ring 1

            IPointCollection exteriorRing1PointCollection = new RingClass();
            exteriorRing1PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(5, 0, -5), ref _missing, ref _missing);
            exteriorRing1PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-5, 0, -5), ref _missing, ref _missing);
            exteriorRing1PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-5, 0, 5), ref _missing, ref _missing);
            exteriorRing1PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(5, 0, 5), ref _missing, ref _missing);

            IRing exteriorRing1 = exteriorRing1PointCollection as IRing;
            exteriorRing1.Close();

            multiPatchGeometryCollection.AddGeometry(exteriorRing1 as IGeometry, ref _missing, ref _missing);

            multiPatch.PutRingType(exteriorRing1, esriMultiPatchRingType.esriMultiPatchOuterRing);

            //Interior Ring 1

            IPointCollection interiorRing1PointCollection = new RingClass();
            interiorRing1PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-4, 0, -4), ref _missing, ref _missing);
            interiorRing1PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(4, 0, -4), ref _missing, ref _missing);
            interiorRing1PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(4, 0, 4), ref _missing, ref _missing);
            interiorRing1PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-4, 0, 4), ref _missing, ref _missing);

            IRing interiorRing1 = interiorRing1PointCollection as IRing;
            interiorRing1.Close();

            multiPatchGeometryCollection.AddGeometry(interiorRing1 as IGeometry, ref _missing, ref _missing);

            multiPatch.PutRingType(interiorRing1, esriMultiPatchRingType.esriMultiPatchInnerRing);

            return multiPatchGeometryCollection as IGeometry;
        }

        public static IGeometry GetExample4()
        {
            //RingGroup: Upright Square Composed Of Multiple Exterior Rings And Multiple Interior Rings

            IGeometryCollection multiPatchGeometryCollection = new MultiPatchClass();

            IMultiPatch multiPatch = multiPatchGeometryCollection as IMultiPatch;

            //Exterior Ring 1

            IPointCollection exteriorRing1PointCollection = new RingClass();
            exteriorRing1PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(5, 0, -5), ref _missing, ref _missing);
            exteriorRing1PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-5, 0, -5), ref _missing, ref _missing);
            exteriorRing1PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-5, 0, 5), ref _missing, ref _missing);
            exteriorRing1PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(5, 0, 5), ref _missing, ref _missing);

            IRing exteriorRing1 = exteriorRing1PointCollection as IRing;
            exteriorRing1.Close();

            multiPatchGeometryCollection.AddGeometry(exteriorRing1 as IGeometry, ref _missing, ref _missing);

            multiPatch.PutRingType(exteriorRing1, esriMultiPatchRingType.esriMultiPatchOuterRing);

            //Interior Ring 1

            IPointCollection interiorRing1PointCollection = new RingClass();
            interiorRing1PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-4, 0, -4), ref _missing, ref _missing);
            interiorRing1PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(4, 0, -4), ref _missing, ref _missing);
            interiorRing1PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(4, 0, 4), ref _missing, ref _missing);
            interiorRing1PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-4, 0, 4), ref _missing, ref _missing);

            IRing interiorRing1 = interiorRing1PointCollection as IRing;
            interiorRing1.Close();

            multiPatchGeometryCollection.AddGeometry(interiorRing1 as IGeometry, ref _missing, ref _missing);

            multiPatch.PutRingType(interiorRing1, esriMultiPatchRingType.esriMultiPatchInnerRing);

            //Exterior Ring 2 

            IPointCollection exteriorRing2PointCollection = new RingClass();
            exteriorRing2PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(3, 0, -3), ref _missing, ref _missing);
            exteriorRing2PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-3, 0, -3), ref _missing, ref _missing);
            exteriorRing2PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-3, 0, 3), ref _missing, ref _missing);
            exteriorRing2PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(3, 0, 3), ref _missing, ref _missing);

            IRing exteriorRing2 = exteriorRing2PointCollection as IRing;
            exteriorRing2.Close();

            multiPatchGeometryCollection.AddGeometry(exteriorRing2 as IGeometry, ref _missing, ref _missing);

            multiPatch.PutRingType(exteriorRing2, esriMultiPatchRingType.esriMultiPatchOuterRing);

            //Interior Ring 2

            IPointCollection interiorRing2PointCollection = new RingClass();
            interiorRing2PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-2, 0, -2), ref _missing, ref _missing);
            interiorRing2PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(2, 0, -2), ref _missing, ref _missing);
            interiorRing2PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(2, 0, 2), ref _missing, ref _missing);
            interiorRing2PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-2, 0, 2), ref _missing, ref _missing);

            IRing interiorRing2 = interiorRing2PointCollection as IRing;
            interiorRing2.Close();

            multiPatchGeometryCollection.AddGeometry(interiorRing2 as IGeometry, ref _missing, ref _missing);

            multiPatch.PutRingType(interiorRing2, esriMultiPatchRingType.esriMultiPatchInnerRing);

            //Exterior Ring 3 

            IPointCollection exteriorRing3PointCollection = new RingClass();
            exteriorRing3PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(1, 0, -1), ref _missing, ref _missing);
            exteriorRing3PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-1, 0, -1), ref _missing, ref _missing);
            exteriorRing3PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-1, 0, 1), ref _missing, ref _missing);
            exteriorRing3PointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(1, 0, 1), ref _missing, ref _missing);

            IRing exteriorRing3 = exteriorRing3PointCollection as IRing;
            exteriorRing3.Close();

            multiPatchGeometryCollection.AddGeometry(exteriorRing3 as IGeometry, ref _missing, ref _missing);

            multiPatch.PutRingType(exteriorRing3, esriMultiPatchRingType.esriMultiPatchOuterRing);

            return multiPatchGeometryCollection as IGeometry;
        }

        public static IGeometry GetExample5()
        {
            const int XRange = 16;
            const int YRange = 16;
            const int InteriorRingCount = 25;
            const double HoleRange = 0.5;

            //RingGroup: Square Lying In XY Plane With Single Exterior Ring And Multiple Interior Rings

            IGeometryCollection multiPatchGeometryCollection = new MultiPatchClass();

            IMultiPatch multiPatch = multiPatchGeometryCollection as IMultiPatch;

            //Exterior Ring

            IPointCollection exteriorRingPointCollection = new RingClass();
            exteriorRingPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0.5 * (XRange + 2), -0.5 * (YRange + 2), 0), ref _missing, ref _missing);
            exteriorRingPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-0.5 * (XRange + 2), -0.5 * (YRange + 2), 0), ref _missing, ref _missing);
            exteriorRingPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-0.5 * (XRange + 2), 0.5 * (YRange + 2), 0), ref _missing, ref _missing);
            exteriorRingPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0.5 * (XRange + 2), 0.5 * (YRange + 2), 0), ref _missing, ref _missing);

            IRing exteriorRing = exteriorRingPointCollection as IRing;
            exteriorRing.Close();

            multiPatchGeometryCollection.AddGeometry(exteriorRing as IGeometry, ref _missing, ref _missing);

            multiPatch.PutRingType(exteriorRing, esriMultiPatchRingType.esriMultiPatchOuterRing);

            //Interior Rings

            Random random = new Random();

            for (int i = 0; i < InteriorRingCount; i++)
            {
                double interiorRingOriginX = XRange * (random.NextDouble() - 0.5);
                double interiorRingOriginY = YRange * (random.NextDouble() - 0.5);

                IPointCollection interiorRingPointCollection = new RingClass();
                interiorRingPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(interiorRingOriginX - 0.5 * HoleRange, interiorRingOriginY - 0.5 * HoleRange, 0), ref _missing, ref _missing);
                interiorRingPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(interiorRingOriginX + 0.5 * HoleRange, interiorRingOriginY - 0.5 * HoleRange, 0), ref _missing, ref _missing);
                interiorRingPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(interiorRingOriginX + 0.5 * HoleRange, interiorRingOriginY + 0.5 * HoleRange, 0), ref _missing, ref _missing);
                interiorRingPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(interiorRingOriginX - 0.5 * HoleRange, interiorRingOriginY + 0.5 * HoleRange, 0), ref _missing, ref _missing);

                IRing interiorRing = interiorRingPointCollection as IRing;
                interiorRing.Close();

                multiPatchGeometryCollection.AddGeometry(interiorRing as IGeometry, ref _missing, ref _missing);

                multiPatch.PutRingType(interiorRing, esriMultiPatchRingType.esriMultiPatchInnerRing);
            }

            return multiPatchGeometryCollection as IGeometry;
        }
    }
}
