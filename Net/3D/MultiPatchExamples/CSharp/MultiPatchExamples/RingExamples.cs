using ESRI.ArcGIS.Geometry;
using System;

namespace MultiPatchExamples
{
    public static class RingExamples
    {
        private static object _missing = Type.Missing;

        public static IGeometry GetExample1()
        {
            //Ring: Upright Rectangle

            IGeometryCollection multiPatchGeometryCollection = new MultiPatchClass();

            IPointCollection ringPointCollection = new RingClass();

            ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-7.5, 0, 0), ref _missing, ref _missing);
            ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-7.5, 0, 7.5), ref _missing, ref _missing);
            ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(7.5, 0, 7.5), ref _missing, ref _missing);
            ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(7.5, 0, 0), ref _missing, ref _missing);
            ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-7.5, 0, 0), ref _missing, ref _missing);

            multiPatchGeometryCollection.AddGeometry(ringPointCollection as IGeometry, ref _missing, ref _missing);

            return multiPatchGeometryCollection as IGeometry;
        }

        public static IGeometry GetExample2()
        {
            //Ring: Octagon Lying In XY Plane

            IGeometryCollection multiPatchGeometryCollection = new MultiPatchClass();

            IPointCollection ringPointCollection = new RingClass();

            ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-7.5, 7.5, 0), ref _missing, ref _missing);
            ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, 8.5, 0), ref _missing, ref _missing);
            ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(7.5, 7.5, 0), ref _missing, ref _missing);
            ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(8.5, 0, 0), ref _missing, ref _missing);
            ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(7.5, -7.5, 0), ref _missing, ref _missing);
            ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, -8.5, 0), ref _missing, ref _missing);
            ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-7.5, -7.5, 0), ref _missing, ref _missing);
            ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-8.5, 0, 0), ref _missing, ref _missing);
            ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-7.5, 7.5, 0), ref _missing, ref _missing);

            multiPatchGeometryCollection.AddGeometry(ringPointCollection as IGeometry, ref _missing, ref _missing);

            return multiPatchGeometryCollection as IGeometry;
        }

        public static IGeometry GetExample3()
        {
            //Ring: Octagon With Non-Coplanar Points

            IGeometryCollection multiPatchGeometryCollection = new MultiPatchClass();

            IPointCollection ringPointCollection = new RingClass();

            ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-7.5, 7.5, 5), ref _missing, ref _missing);
            ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, 8.5, 0), ref _missing, ref _missing);
            ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(7.5, 7.5, 5), ref _missing, ref _missing);
            ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(8.5, 0, 0), ref _missing, ref _missing);
            ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(7.5, -7.5, 5), ref _missing, ref _missing);
            ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, -8.5, 0), ref _missing, ref _missing);
            ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-7.5, -7.5, 5), ref _missing, ref _missing);
            ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-8.5, 0, 0), ref _missing, ref _missing);
            ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-7.5, 7.5, 5), ref _missing, ref _missing);

            multiPatchGeometryCollection.AddGeometry(ringPointCollection as IGeometry, ref _missing, ref _missing);

            return multiPatchGeometryCollection as IGeometry;
        }

        public static IGeometry GetExample4()
        {
            //Ring: Maze Lying On XY Plane

            IGeometryCollection multiPatchGeometryCollection = new MultiPatchClass();

            IPointCollection ringPointCollection = new RingClass();

            ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-10, 10, 0), ref _missing, ref _missing);
            ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(10, 10, 0), ref _missing, ref _missing);
            ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(10, -10, 0), ref _missing, ref _missing);
            ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-10, -10, 0), ref _missing, ref _missing);
            ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-10, 6, 0), ref _missing, ref _missing);
            ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(6, 6, 0), ref _missing, ref _missing);
            ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(6, -6, 0), ref _missing, ref _missing);
            ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-6, -6, 0), ref _missing, ref _missing);
            ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-6, 2, 0), ref _missing, ref _missing);
            ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-6, 2, 0), ref _missing, ref _missing);
            ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, 2, 0), ref _missing, ref _missing);
            ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, 0, 0), ref _missing, ref _missing);
            ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-4, 0, 0), ref _missing, ref _missing);
            ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-4, -4, 0), ref _missing, ref _missing);
            ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(4, -4, 0), ref _missing, ref _missing);
            ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(4, 4, 0), ref _missing, ref _missing);
            ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-8, 4, 0), ref _missing, ref _missing);
            ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-8, -8, 0), ref _missing, ref _missing);
            ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(8, -8, 0), ref _missing, ref _missing);
            ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(8, 8, 0), ref _missing, ref _missing);
            ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-10, 8, 0), ref _missing, ref _missing);
            ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-10, 10, 0), ref _missing, ref _missing);

            multiPatchGeometryCollection.AddGeometry(ringPointCollection as IGeometry, ref _missing, ref _missing);

            return multiPatchGeometryCollection as IGeometry;
        }

        public static IGeometry GetExample5()
        {
            //Ring: Maze With Non-Coplanar Points

            IGeometryCollection multiPatchGeometryCollection = new MultiPatchClass();

            IPointCollection ringPointCollection = new RingClass();

            ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-10, 10, 0), ref _missing, ref _missing);
            ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(10, 10, 5), ref _missing, ref _missing);
            ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(10, -10, -5), ref _missing, ref _missing);
            ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-10, -10, 0), ref _missing, ref _missing);
            ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-10, 6, 5), ref _missing, ref _missing);
            ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(6, 6, -5), ref _missing, ref _missing);
            ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(6, -6, 0), ref _missing, ref _missing);
            ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-6, -6, 5), ref _missing, ref _missing);
            ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-6, 2, -5), ref _missing, ref _missing);
            ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-6, 2, 0), ref _missing, ref _missing);
            ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, 2, 5), ref _missing, ref _missing);
            ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, 0, -5), ref _missing, ref _missing);
            ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-4, 0, 0), ref _missing, ref _missing);
            ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-4, -4, 5), ref _missing, ref _missing);
            ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(4, -4, -5), ref _missing, ref _missing);
            ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(4, 4, 0), ref _missing, ref _missing);
            ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-8, 4, 5), ref _missing, ref _missing);
            ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-8, -8, -5), ref _missing, ref _missing);
            ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(8, -8, 0), ref _missing, ref _missing);
            ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(8, 8, 5), ref _missing, ref _missing);
            ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-10, 8, -5), ref _missing, ref _missing);
            ringPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-10, 10, 0), ref _missing, ref _missing);

            multiPatchGeometryCollection.AddGeometry(ringPointCollection as IGeometry, ref _missing, ref _missing);

            return multiPatchGeometryCollection as IGeometry;
        }
    }
}