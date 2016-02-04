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

namespace MultiPatchExamples
{
    public static class TriangleFanExamples
    {
        private static object _missing = Type.Missing;

        public static IGeometry GetExample1()
        {
            //TriangleFan: Square Lying On XY Plane, Z < 0

            IGeometryCollection multiPatchGeometryCollection = new MultiPatchClass();

            IPointCollection triangleFanPointCollection = new TriangleFanClass();

            triangleFanPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, 0, -5), ref _missing, ref _missing);
            triangleFanPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-6, -6, -5), ref _missing, ref _missing);
            triangleFanPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-6, 6, -5), ref _missing, ref _missing);
            triangleFanPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(6, 6, -5), ref _missing, ref _missing);
            triangleFanPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(6, -6, -5), ref _missing, ref _missing);
            triangleFanPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-6, -6, -5), ref _missing, ref _missing);

            multiPatchGeometryCollection.AddGeometry(triangleFanPointCollection as IGeometry, ref _missing, ref _missing);

            return multiPatchGeometryCollection as IGeometry;
        }

        public static IGeometry GetExample2()
        {
            //TriangleFan: Upright Square

            IGeometryCollection multiPatchGeometryCollection = new MultiPatchClass();

            IPointCollection triangleFanPointCollection = new TriangleFanClass();

            triangleFanPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, 0, 0), ref _missing, ref _missing);
            triangleFanPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-5, 0, -5), ref _missing, ref _missing);
            triangleFanPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-5, 0, 5), ref _missing, ref _missing);
            triangleFanPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(5, 0, 5), ref _missing, ref _missing);
            triangleFanPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(5, 0, -5), ref _missing, ref _missing);
            triangleFanPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-5, 0, -5), ref _missing, ref _missing);

            multiPatchGeometryCollection.AddGeometry(triangleFanPointCollection as IGeometry, ref _missing, ref _missing);

            return multiPatchGeometryCollection as IGeometry;
        }

        public static IGeometry GetExample3()
        {
            //TriangleFan: Square Based Pyramid

            IGeometryCollection multiPatchGeometryCollection = new MultiPatchClass();

            IPointCollection triangleFanPointCollection = new TriangleFanClass();

            triangleFanPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, 0, 7), ref _missing, ref _missing);
            triangleFanPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-6, -6, 0), ref _missing, ref _missing);
            triangleFanPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-6, 6, 0), ref _missing, ref _missing);
            triangleFanPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(6, 6, 0), ref _missing, ref _missing);
            triangleFanPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(6, -6, 0), ref _missing, ref _missing);
            triangleFanPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-6, -6, 0), ref _missing, ref _missing);

            multiPatchGeometryCollection.AddGeometry(triangleFanPointCollection as IGeometry, ref _missing, ref _missing);

            return multiPatchGeometryCollection as IGeometry;
        }

        public static IGeometry GetExample4()
        {
            //TriangleFan: Triangle Based Pyramid

            IGeometryCollection multiPatchGeometryCollection = new MultiPatchClass();

            IPointCollection triangleFanPointCollection = new TriangleFanClass();

            triangleFanPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, 0, 6), ref _missing, ref _missing);
            triangleFanPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-3 * Math.Sqrt(3), -3, 0), ref _missing, ref _missing);
            triangleFanPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, 6, 0), ref _missing, ref _missing);
            triangleFanPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(3 * Math.Sqrt(3), -3, 0), ref _missing, ref _missing);
            triangleFanPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-3 * Math.Sqrt(3), -3, 0), ref _missing, ref _missing);

            multiPatchGeometryCollection.AddGeometry(triangleFanPointCollection as IGeometry, ref _missing, ref _missing);

            return multiPatchGeometryCollection as IGeometry;
        }

        public static IGeometry GetExample5()
        {
            //TriangleFan: Alternating Fan

            IGeometryCollection multiPatchGeometryCollection = new MultiPatchClass();

            IPointCollection triangleFanPointCollection = new TriangleFanClass();

            triangleFanPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, 0, 0), ref _missing, ref _missing);
            triangleFanPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, -6, 3), ref _missing, ref _missing);
            triangleFanPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-3 * Math.Sqrt(2), -3 * Math.Sqrt(2), -3), ref _missing, ref _missing);
            triangleFanPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-6, 0, 3), ref _missing, ref _missing);
            triangleFanPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-3 * Math.Sqrt(2), 3 * Math.Sqrt(2), -3), ref _missing, ref _missing);
            triangleFanPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, 6, 3), ref _missing, ref _missing);
            triangleFanPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(3 * Math.Sqrt(2), 3 * Math.Sqrt(2), -3), ref _missing, ref _missing);
            triangleFanPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(6, 0, 3), ref _missing, ref _missing);
            triangleFanPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(3 * Math.Sqrt(2), -3 * Math.Sqrt(2), -3), ref _missing, ref _missing);
            triangleFanPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, -6, 3), ref _missing, ref _missing);

            multiPatchGeometryCollection.AddGeometry(triangleFanPointCollection as IGeometry, ref _missing, ref _missing);

            return multiPatchGeometryCollection as IGeometry;
        }

        public static IGeometry GetExample6()
        {
            //TriangleFan: Partial Fan, Two Levels Of Zs

            IGeometryCollection multiPatchGeometryCollection = new MultiPatchClass();

            IPointCollection triangleFanPointCollection = new TriangleFanClass();

            triangleFanPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, 0, 3), ref _missing, ref _missing);
            triangleFanPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, -6, 3), ref _missing, ref _missing);
            triangleFanPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-3 * Math.Sqrt(2), -3 * Math.Sqrt(2), 3), ref _missing, ref _missing);
            triangleFanPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-6, 0, 3), ref _missing, ref _missing);
            triangleFanPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-3 * Math.Sqrt(2), 3 * Math.Sqrt(2), 0), ref _missing, ref _missing);
            triangleFanPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, 6, 0), ref _missing, ref _missing);
            triangleFanPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(3 * Math.Sqrt(2), 3 * Math.Sqrt(2), 0), ref _missing, ref _missing);
            triangleFanPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(6, 0, 0), ref _missing, ref _missing);

            multiPatchGeometryCollection.AddGeometry(triangleFanPointCollection as IGeometry, ref _missing, ref _missing);

            return multiPatchGeometryCollection as IGeometry;
        }
    }
}


