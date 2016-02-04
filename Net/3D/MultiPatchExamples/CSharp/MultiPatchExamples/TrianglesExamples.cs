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
    public static class TrianglesExamples
    {
        private static object _missing = Type.Missing;

        public static IGeometry GetExample1()
        {
            //Triangles: One Triangle Lying On XY Plane

            IGeometryCollection multiPatchGeometryCollection = new MultiPatchClass();

            IPointCollection trianglesPointCollection = new TrianglesClass();

            trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(2.5, 2.5, 0), ref _missing, ref _missing);
            trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(7.5, 7.5, 0), ref _missing, ref _missing);
            trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(7.5, 2.5, 0), ref _missing, ref _missing);

            multiPatchGeometryCollection.AddGeometry(trianglesPointCollection as IGeometry, ref _missing, ref _missing);

            return multiPatchGeometryCollection as IGeometry;
        }

        public static IGeometry GetExample2()
        {
            //Triangles: One Upright Triangle

            IGeometryCollection multiPatchGeometryCollection = new MultiPatchClass();

            IPointCollection trianglesPointCollection = new TrianglesClass();

            trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(2.5, 2.5, 0), ref _missing, ref _missing);
            trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(2.5, 2.5, 7.5), ref _missing, ref _missing);
            trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(7.5, 2.5, 7.5), ref _missing, ref _missing);

            multiPatchGeometryCollection.AddGeometry(trianglesPointCollection as IGeometry, ref _missing, ref _missing);

            return multiPatchGeometryCollection as IGeometry;
        }

        public static IGeometry GetExample3()
        {
            //Triangles: Three Upright Triangles

            IGeometryCollection multiPatchGeometryCollection = new MultiPatchClass();

            IPointCollection trianglesPointCollection = new TrianglesClass();

            //Triangle 1

            trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(2.5, 2.5, 0), ref _missing, ref _missing);
            trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(2.5, 2.5, 7.5), ref _missing, ref _missing);
            trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(7.5, 2.5, 7.5), ref _missing, ref _missing);

            //Triangle 2

            trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-7.5, 2.5, 0), ref _missing, ref _missing);
            trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-7.5, 2.5, 7.5), ref _missing, ref _missing);
            trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-2.5, 2.5, 7.5), ref _missing, ref _missing);

            //Triangle 3

            trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-2.5, -2.5, 0), ref _missing, ref _missing);
            trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-2.5, -2.5, 7.5), ref _missing, ref _missing);
            trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(2.5, -2.5, 7.5), ref _missing, ref _missing);

            multiPatchGeometryCollection.AddGeometry(trianglesPointCollection as IGeometry, ref _missing, ref _missing);

            return multiPatchGeometryCollection as IGeometry;
        }

        public static IGeometry GetExample4()
        {
            //Triangles: Six Triangles Lying In Different Planes

            IGeometryCollection multiPatchGeometryCollection = new MultiPatchClass();

            IPointCollection trianglesPointCollection = new TrianglesClass();

            //Triangle 1

            trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(7.5, 7.5, 0), ref _missing, ref _missing);
            trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(10, 7.5, 0), ref _missing, ref _missing);
            trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(7.5, 5, 0), ref _missing, ref _missing);

            //Triangle 2

            trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-7.5, 7.5, 0), ref _missing, ref _missing);
            trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-5, 7.5, 0), ref _missing, ref _missing);
            trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-7.5, 5, 0), ref _missing, ref _missing);

            //Triangle 3

            trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, -5, 0), ref _missing, ref _missing);
            trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(2.5, -5, 0), ref _missing, ref _missing);
            trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, -7.5, 0), ref _missing, ref _missing);

            //Triangle 4

            trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, 7.5, 2.5), ref _missing, ref _missing);
            trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(2.5, 7.5, 0), ref _missing, ref _missing);
            trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, 7.5, 0), ref _missing, ref _missing);

            //Triangle 5

            trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-7.5, -7.5, 2.5), ref _missing, ref _missing);
            trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-5, -7.5, 0), ref _missing, ref _missing);
            trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-7.5, -7.5, 0), ref _missing, ref _missing);

            //Triangle 6

            trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(7.5, -7.5, 2.5), ref _missing, ref _missing);
            trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(10, -7.5, 0), ref _missing, ref _missing);
            trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(7.5, -7.5, 0), ref _missing, ref _missing);

            multiPatchGeometryCollection.AddGeometry(trianglesPointCollection as IGeometry, ref _missing, ref _missing);

            return multiPatchGeometryCollection as IGeometry;
        }

        public static IGeometry GetExample5()
        {
            //Triangles: Eighteen Triangles Lying In Different Planes

            IGeometryCollection multiPatchGeometryCollection = new MultiPatchClass();

            IPointCollection trianglesPointCollection = new TrianglesClass();

            //Z > 0

            //Triangle 1

            trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(7.5, 7.5, 5), ref _missing, ref _missing);
            trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(10, 7.5, 5), ref _missing, ref _missing);
            trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(7.5, 5, 5), ref _missing, ref _missing);

            //Triangle 2

            trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-7.5, 7.5, 5), ref _missing, ref _missing);
            trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-5, 7.5, 5), ref _missing, ref _missing);
            trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-7.5, 5, 5), ref _missing, ref _missing);

            //Triangle 3

            trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, -5, 5), ref _missing, ref _missing);
            trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(2.5, -5, 5), ref _missing, ref _missing);
            trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, -7.5, 5), ref _missing, ref _missing);

            //Triangle 4

            trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, 7.5, 7.5), ref _missing, ref _missing);
            trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(2.5, 7.5, 5), ref _missing, ref _missing);
            trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, 7.5, 5), ref _missing, ref _missing);

            //Triangle 5

            trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-7.5, -7.5, 7.5), ref _missing, ref _missing);
            trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-5, -7.5, 5), ref _missing, ref _missing);
            trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-7.5, -7.5, 5), ref _missing, ref _missing);

            //Triangle 6

            trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(7.5, -7.5, 7.5), ref _missing, ref _missing);
            trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(10, -7.5, 5), ref _missing, ref _missing);
            trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(7.5, -7.5, 5), ref _missing, ref _missing);

            //Z = 0

            //Triangle 1

            trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(7.5, 7.5, 0), ref _missing, ref _missing);
            trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(10, 7.5, 0), ref _missing, ref _missing);
            trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(7.5, 5, 0), ref _missing, ref _missing);

            //Triangle 2

            trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-7.5, 7.5, 0), ref _missing, ref _missing);
            trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-5, 7.5, 0), ref _missing, ref _missing);
            trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-7.5, 5, 0), ref _missing, ref _missing);

            //Triangle 3

            trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, -5, 0), ref _missing, ref _missing);
            trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(2.5, -5, 0), ref _missing, ref _missing);
            trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, -7.5, 0), ref _missing, ref _missing);

            //Triangle 4

            trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, 7.5, 2.5), ref _missing, ref _missing);
            trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(2.5, 7.5, 0), ref _missing, ref _missing);
            trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, 7.5, 0), ref _missing, ref _missing);

            //Triangle 5

            trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-7.5, -7.5, 2.5), ref _missing, ref _missing);
            trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-5, -7.5, 0), ref _missing, ref _missing);
            trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-7.5, -7.5, 0), ref _missing, ref _missing);

            //Triangle 6

            trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(7.5, -7.5, 2.5), ref _missing, ref _missing);
            trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(10, -7.5, 0), ref _missing, ref _missing);
            trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(7.5, -7.5, 0), ref _missing, ref _missing);

            //Z < 0

            //Triangle 1

            trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(7.5, 7.5, -5), ref _missing, ref _missing);
            trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(10, 7.5, -5), ref _missing, ref _missing);
            trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(7.5, 5, -5), ref _missing, ref _missing);

            //Triangle 2

            trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-7.5, 7.5, -5), ref _missing, ref _missing);
            trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-5, 7.5, -5), ref _missing, ref _missing);
            trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-7.5, 5, -5), ref _missing, ref _missing);

            //Triangle 3

            trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, -5, -5), ref _missing, ref _missing);
            trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(2.5, -5, -5), ref _missing, ref _missing);
            trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, -7.5, -5), ref _missing, ref _missing);

            //Triangle 4

            trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, 7.5, -2.5), ref _missing, ref _missing);
            trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(2.5, 7.5, -5), ref _missing, ref _missing);
            trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, 7.5, -5), ref _missing, ref _missing);

            //Triangle 5

            trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-7.5, -7.5, -2.5), ref _missing, ref _missing);
            trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-5, -7.5, -5), ref _missing, ref _missing);
            trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-7.5, -7.5, -5), ref _missing, ref _missing);

            //Triangle 6

            trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(7.5, -7.5, -2.5), ref _missing, ref _missing);
            trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(10, -7.5, -5), ref _missing, ref _missing);
            trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(7.5, -7.5, -5), ref _missing, ref _missing);

            multiPatchGeometryCollection.AddGeometry(trianglesPointCollection as IGeometry, ref _missing, ref _missing);

            return multiPatchGeometryCollection as IGeometry;
        }

        public static IGeometry GetExample6()
        {
            //Triangles: Closed Box Constructed From Single Triangles Part Composed Of 12 Triangles

            IGeometryCollection multiPatchGeometryCollection = new MultiPatchClass() as IGeometryCollection;

            IPointCollection trianglesPointCollection = new TrianglesClass() as IPointCollection;

            //Bottom

            trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, 0, 0), ref _missing, ref _missing);
            trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(5, 5, 0), ref _missing, ref _missing);
            trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, 5, 0), ref _missing, ref _missing);

            trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, 0, 0), ref _missing, ref _missing);
            trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(5, 0, 0), ref _missing, ref _missing);
            trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(5, 5, 0), ref _missing, ref _missing);

            //Side 1

            trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, 5, 0), ref _missing, ref _missing);
            trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, 5, 5), ref _missing, ref _missing);
            trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, 0, 0), ref _missing, ref _missing);

            trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, 0, 0), ref _missing, ref _missing);
            trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, 5, 5), ref _missing, ref _missing);
            trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, 0, 5), ref _missing, ref _missing);

            //Side 2

            trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(5, 5, 0), ref _missing, ref _missing);
            trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, 5, 5), ref _missing, ref _missing);
            trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, 5, 0), ref _missing, ref _missing);

            trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(5, 5, 0), ref _missing, ref _missing);
            trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(5, 5, 5), ref _missing, ref _missing);
            trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, 5, 5), ref _missing, ref _missing);

            //Side 3

            trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(5, 0, 0), ref _missing, ref _missing);
            trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(5, 5, 5), ref _missing, ref _missing);
            trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(5, 5, 0), ref _missing, ref _missing);

            trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(5, 0, 0), ref _missing, ref _missing);
            trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(5, 0, 5), ref _missing, ref _missing);
            trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(5, 5, 5), ref _missing, ref _missing);

            //Side 4

            trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, 0, 0), ref _missing, ref _missing);
            trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, 0, 5), ref _missing, ref _missing);
            trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(5, 0, 0), ref _missing, ref _missing);

            trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(5, 0, 0), ref _missing, ref _missing);
            trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, 0, 5), ref _missing, ref _missing);
            trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(5, 0, 5), ref _missing, ref _missing);

            //Top

            trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, 0, 5), ref _missing, ref _missing);
            trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, 5, 5), ref _missing, ref _missing);
            trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(5, 5, 5), ref _missing, ref _missing);

            trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, 0, 5), ref _missing, ref _missing);
            trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(5, 5, 5), ref _missing, ref _missing);
            trianglesPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(5, 0, 5), ref _missing, ref _missing);

            multiPatchGeometryCollection.AddGeometry(trianglesPointCollection as IGeometry, ref _missing, ref _missing);

            return multiPatchGeometryCollection as IGeometry;
        }    
    }
}
