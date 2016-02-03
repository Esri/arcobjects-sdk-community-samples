using ESRI.ArcGIS.Geometry;
using System;

namespace MultiPatchExamples
{
    public static class TriangleStripExamples
    {
        private static object _missing = Type.Missing;

        public static IGeometry GetExample1()
        {
            //TriangleStrip: Square Lying On XY Plane

            IGeometryCollection multiPatchGeometryCollection = new MultiPatchClass();

            IPointCollection triangleStripPointCollection = new TriangleStripClass();

            triangleStripPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-6, -6, 0), ref _missing, ref _missing);
            triangleStripPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-6, 6, 0), ref _missing, ref _missing);
            triangleStripPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(6, -6, 0), ref _missing, ref _missing);
            triangleStripPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(6, 6, 0), ref _missing, ref _missing);

            multiPatchGeometryCollection.AddGeometry(triangleStripPointCollection as IGeometry, ref _missing, ref _missing);

            return multiPatchGeometryCollection as IGeometry;
        }

        public static IGeometry GetExample2()
        {
            //TriangleStrip: Multi-Paneled Vertical Plane

            IGeometryCollection multiPatchGeometryCollection = new MultiPatchClass();

            IPointCollection triangleStripPointCollection = new TriangleStripClass();

            //Panel 1

            triangleStripPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-7.5, 7.5, 0), ref _missing, ref _missing);
            triangleStripPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-7.5, 7.5, 7.5), ref _missing, ref _missing);

            //Panel 2

            triangleStripPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-2.5, 2.5, 0), ref _missing, ref _missing);
            triangleStripPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-2.5, 2.5, 7.5), ref _missing, ref _missing);

            //Panel 3

            triangleStripPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(2.5, -2.5, 0), ref _missing, ref _missing);
            triangleStripPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(2.5, -2.5, 7.5), ref _missing, ref _missing);

            //Panel 4

            triangleStripPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(7.5, -7.5, 0), ref _missing, ref _missing);
            triangleStripPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(7.5, -7.5, 7.5), ref _missing, ref _missing);

            multiPatchGeometryCollection.AddGeometry(triangleStripPointCollection as IGeometry, ref _missing, ref _missing);

            return multiPatchGeometryCollection as IGeometry;
        }

        public static IGeometry GetExample3()
        {
            //TriangleStrip: Stairs

            IGeometryCollection multiPatchGeometryCollection = new MultiPatchClass();

            IPointCollection triangleStripPointCollection = new TriangleStripClass();

            //First Step
            
            triangleStripPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, 10, 10), ref _missing, ref _missing);
            triangleStripPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(10, 10, 10), ref _missing, ref _missing);
            
            triangleStripPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, 7.5, 10), ref _missing, ref _missing);
            triangleStripPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(10, 7.5, 10), ref _missing, ref _missing);
            
            //Second Step
            
            triangleStripPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, 7.5, 7.5), ref _missing, ref _missing);
            triangleStripPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(10, 7.5, 7.5), ref _missing, ref _missing);
            
            
            triangleStripPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, 5, 7.5), ref _missing, ref _missing);
            triangleStripPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(10, 5, 7.5), ref _missing, ref _missing);
            
            //Third Step
            
            triangleStripPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, 5, 5), ref _missing, ref _missing);
            triangleStripPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(10, 5, 5), ref _missing, ref _missing);
            
            
            triangleStripPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, 2.5, 5), ref _missing, ref _missing);
            triangleStripPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(10, 2.5, 5), ref _missing, ref _missing);
            
            //Fourth Step
            
            triangleStripPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, 2.5, 2.5), ref _missing, ref _missing);
            triangleStripPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(10, 2.5, 2.5), ref _missing, ref _missing);
            
            triangleStripPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, 0, 2.5), ref _missing, ref _missing);
            triangleStripPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(10, 0, 2.5), ref _missing, ref _missing);
            
            //End
            
            triangleStripPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, 0, 0), ref _missing, ref _missing);
            triangleStripPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(10, 0, 0), ref _missing, ref _missing);

            multiPatchGeometryCollection.AddGeometry(triangleStripPointCollection as IGeometry, ref _missing, ref _missing);

            return multiPatchGeometryCollection as IGeometry;
        }

        public static IGeometry GetExample4()
        {
            //TriangleStrip: Box Without Top or Bottom

            IGeometryCollection multiPatchGeometryCollection = new MultiPatchClass();

            IPointCollection triangleStripPointCollection = new TriangleStripClass();

            //Start
            
            triangleStripPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, 0, 0), ref _missing, ref _missing);
            triangleStripPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, 0, 10), ref _missing, ref _missing);

            //First Panel

            triangleStripPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(5, 0, 0), ref _missing, ref _missing);
            triangleStripPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(5, 0, 10), ref _missing, ref _missing);
            
            //Second Panel
            
            triangleStripPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(5, 5, 0), ref _missing, ref _missing);
            triangleStripPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(5, 5, 10), ref _missing, ref _missing);
            
            //Third Panel
            
            triangleStripPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, 5, 0), ref _missing, ref _missing);
            triangleStripPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, 5, 10), ref _missing, ref _missing);
            
            //End, To Close Box
            
            triangleStripPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, 0, 0), ref _missing, ref _missing);
            triangleStripPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, 0, 10), ref _missing, ref _missing);

            multiPatchGeometryCollection.AddGeometry(triangleStripPointCollection as IGeometry, ref _missing, ref _missing);

            return multiPatchGeometryCollection as IGeometry;
        }

        public static IGeometry GetExample5()
        {
            //TriangleStrip: Star Shaped Box Without Top or Bottom

            IGeometryCollection multiPatchGeometryCollection = new MultiPatchClass();

            IPointCollection triangleStripPointCollection = new TriangleStripClass();

            //Start

            triangleStripPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, 2, 0), ref _missing, ref _missing);
            triangleStripPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, 2, 5), ref _missing, ref _missing);

            //First Panel

            triangleStripPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-1 * Math.Sqrt(10), Math.Sqrt(10), 0), ref _missing, ref _missing);
            triangleStripPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-1 * Math.Sqrt(10), Math.Sqrt(10), 5), ref _missing, ref _missing);

            //Second Panel

            triangleStripPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-2, 0, 0), ref _missing, ref _missing);
            triangleStripPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-2, 0, 5), ref _missing, ref _missing);

            //Third Panel

            triangleStripPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-1 * Math.Sqrt(10), -1 * Math.Sqrt(10), 0), ref _missing, ref _missing);
            triangleStripPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-1 * Math.Sqrt(10), -1 * Math.Sqrt(10), 5), ref _missing, ref _missing);

            //Fourth Panel

            triangleStripPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, -2, 0), ref _missing, ref _missing);
            triangleStripPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, -2, 5), ref _missing, ref _missing);

            //Fifth Panel

            triangleStripPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(Math.Sqrt(10), -1 * Math.Sqrt(10), 0), ref _missing, ref _missing);
            triangleStripPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(Math.Sqrt(10), -1 * Math.Sqrt(10), 5), ref _missing, ref _missing);

            //Sixth Panel

            triangleStripPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(2, 0, 0), ref _missing, ref _missing);
            triangleStripPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(2, 0, 5), ref _missing, ref _missing);

            //Seventh Panel

            triangleStripPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(Math.Sqrt(10), Math.Sqrt(10), 0), ref _missing, ref _missing);
            triangleStripPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(Math.Sqrt(10), Math.Sqrt(10), 5), ref _missing, ref _missing);

            //End, To Close Box

            triangleStripPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, 2, 0), ref _missing, ref _missing);
            triangleStripPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(0, 2, 5), ref _missing, ref _missing);

            multiPatchGeometryCollection.AddGeometry(triangleStripPointCollection as IGeometry, ref _missing, ref _missing);

            return multiPatchGeometryCollection as IGeometry;
        }
    }
}