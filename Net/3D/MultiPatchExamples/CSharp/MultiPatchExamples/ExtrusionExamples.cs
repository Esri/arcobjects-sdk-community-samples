using System;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;
using System.Diagnostics;
using ESRI.ArcGIS.esriSystem;

namespace MultiPatchExamples
{
    public static class ExtrusionExamples
    {
        private static object _missing = Type.Missing;

        public static IGeometry GetExample1()
        {
            const double FromZ = 0;
            const double ToZ = 9;

            //Extrusion: Two Point 2D Polyline Extruded To Generate 3D Wall Via ConstructExtrudeFromTo()

            IPointCollection polylinePointCollection = new PolylineClass();

            polylinePointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(-5, 5), ref _missing, ref _missing);
            polylinePointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(5, -5), ref _missing, ref _missing);

            IGeometry polylineGeometry = polylinePointCollection as IGeometry;

            ITopologicalOperator topologicalOperator = polylineGeometry as ITopologicalOperator;
            topologicalOperator.Simplify();

            IConstructMultiPatch constructMultiPatch = new MultiPatchClass();
            constructMultiPatch.ConstructExtrudeFromTo(FromZ, ToZ, polylineGeometry);

            return constructMultiPatch as IGeometry;
        }

        public static IGeometry GetExample2()
        {
            const double FromZ = -0.1;
            const double ToZ = -8;

            //Extrusion: Multiple Point 2D Polyline Extruded To Generate 3D Wall Via ConstructExtrudeFromTo()

            IPointCollection polylinePointCollection = new PolylineClass();

            polylinePointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(-10, -10), ref _missing, ref _missing);
            polylinePointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(-8, -7), ref _missing, ref _missing);
            polylinePointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(-5, -5), ref _missing, ref _missing);
            polylinePointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(-3, -2), ref _missing, ref _missing);
            polylinePointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(0, 0), ref _missing, ref _missing);
            polylinePointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(3, 2), ref _missing, ref _missing);
            polylinePointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(5, 5), ref _missing, ref _missing);
            polylinePointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(8, 7), ref _missing, ref _missing);
            polylinePointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(10, 10), ref _missing, ref _missing);

            IGeometry polylineGeometry = polylinePointCollection as IGeometry;

            ITopologicalOperator topologicalOperator = polylineGeometry as ITopologicalOperator;
            topologicalOperator.Simplify();

            IConstructMultiPatch constructMultiPatch = new MultiPatchClass();
            constructMultiPatch.ConstructExtrudeFromTo(FromZ, ToZ, polylineGeometry);

            return constructMultiPatch as IGeometry;
        }

        public static IGeometry GetExample3() 
        {
            const double FromZ = 0;
            const double ToZ = 9.5;

            //Extrusion: Square Shaped 2D Polygon Extruded To Generate 3D Building Via ConstructExtrudeFromTo()

            IPointCollection polygonPointCollection = new PolygonClass();

            polygonPointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(-2, 2), ref _missing, ref _missing);
            polygonPointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(2, 2), ref _missing, ref _missing);
            polygonPointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(2, -2), ref _missing, ref _missing);
            polygonPointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(-2, -2), ref _missing, ref _missing);

            IPolygon polygon = polygonPointCollection as IPolygon;
            polygon.Close();

            IGeometry polygonGeometry = polygonPointCollection as IGeometry;

            ITopologicalOperator topologicalOperator = polygonGeometry as ITopologicalOperator;
            topologicalOperator.Simplify();

            IConstructMultiPatch constructMultiPatch = new MultiPatchClass();
            constructMultiPatch.ConstructExtrudeFromTo(FromZ, ToZ, polygonGeometry);

            return constructMultiPatch as IGeometry;
        }

        public static IGeometry GetExample4()
        {
            const double FromZ = 0;
            const double ToZ = 8.5;

            //Extrusion: 2D Polygon Composed Of Multiple Square Shaped Rings, Extruded To Generate Multiple 
            //           3D Buildings Via ConstructExtrudeFromTo()

            IPolygon polygon = new PolygonClass();

            IGeometryCollection geometryCollection = polygon as IGeometryCollection;

            //Ring 1

            IPointCollection ring1PointCollection = new RingClass();
            ring1PointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(1, 1), ref _missing, ref _missing);
            ring1PointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(1, 4), ref _missing, ref _missing);
            ring1PointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(4, 4), ref _missing, ref _missing);
            ring1PointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(4, 1), ref _missing, ref _missing);

            IRing ring1 = ring1PointCollection as IRing;
            ring1.Close();

            geometryCollection.AddGeometry(ring1 as IGeometry, ref _missing, ref _missing);

            //Ring 2

            IPointCollection ring2PointCollection = new RingClass();
            ring2PointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(1, -1), ref _missing, ref _missing);
            ring2PointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(4, -1), ref _missing, ref _missing);
            ring2PointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(4, -4), ref _missing, ref _missing);
            ring2PointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(1, -4), ref _missing, ref _missing);

            IRing ring2 = ring2PointCollection as IRing;
            ring2.Close();

            geometryCollection.AddGeometry(ring2 as IGeometry, ref _missing, ref _missing);

            //Ring 3

            IPointCollection ring3PointCollection = new RingClass();
            ring3PointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(-1, 1), ref _missing, ref _missing);
            ring3PointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(-4, 1), ref _missing, ref _missing);
            ring3PointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(-4, 4), ref _missing, ref _missing);
            ring3PointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(-1, 4), ref _missing, ref _missing);

            IRing ring3 = ring3PointCollection as IRing;
            ring3.Close();

            geometryCollection.AddGeometry(ring3 as IGeometry, ref _missing, ref _missing);

            //Ring 4

            IPointCollection ring4PointCollection = new RingClass();
            ring4PointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(-1, -1), ref _missing, ref _missing);
            ring4PointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(-1, -4), ref _missing, ref _missing);
            ring4PointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(-4, -4), ref _missing, ref _missing);
            ring4PointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(-4, -1), ref _missing, ref _missing);

            IRing ring4 = ring4PointCollection as IRing;
            ring4.Close();

            geometryCollection.AddGeometry(ring4 as IGeometry, ref _missing, ref _missing);

            IGeometry polygonGeometry = polygon as IGeometry;

            ITopologicalOperator topologicalOperator = polygonGeometry as ITopologicalOperator;
            topologicalOperator.Simplify();

            IConstructMultiPatch constructMultiPatch = new MultiPatchClass();
            constructMultiPatch.ConstructExtrudeFromTo(FromZ, ToZ, polygonGeometry);

            return constructMultiPatch as IGeometry;
        }

        public static IGeometry GetExample5()
        {
            const double FromZ = 0;
            const double ToZ = 8.5;

            //Extrusion: 2D Polygon Composed Of Multiple Square Shaped Exterior Rings And Corresponding Interior Rings,
            //           Extruded To Generate Multiple 3D Buildings With Hollow Interiors Via ConstructExtrudeFromTo()

            IPolygon polygon = new PolygonClass();

            IGeometryCollection geometryCollection = polygon as IGeometryCollection;

            //Exterior Ring 1

            IPointCollection exteriorRing1PointCollection = new RingClass();
            exteriorRing1PointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(1, 1), ref _missing, ref _missing);
            exteriorRing1PointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(1, 4), ref _missing, ref _missing);
            exteriorRing1PointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(4, 4), ref _missing, ref _missing);
            exteriorRing1PointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(4, 1), ref _missing, ref _missing);

            IRing exteriorRing1 = exteriorRing1PointCollection as IRing;
            exteriorRing1.Close();

            geometryCollection.AddGeometry(exteriorRing1 as IGeometry, ref _missing, ref _missing);

            //Interior Ring 1

            IPointCollection interiorRing1PointCollection = new RingClass();
            interiorRing1PointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(1.5, 1.5), ref _missing, ref _missing);
            interiorRing1PointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(1.5, 3.5), ref _missing, ref _missing);
            interiorRing1PointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(3.5, 3.5), ref _missing, ref _missing);
            interiorRing1PointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(3.5, 1.5), ref _missing, ref _missing);

            IRing interiorRing1 = interiorRing1PointCollection as IRing;
            interiorRing1.Close();

            geometryCollection.AddGeometry(interiorRing1 as IGeometry, ref _missing, ref _missing);

            //Exterior Ring 2

            IPointCollection exteriorRing2PointCollection = new RingClass();
            exteriorRing2PointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(1, -1), ref _missing, ref _missing);
            exteriorRing2PointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(4, -1), ref _missing, ref _missing);
            exteriorRing2PointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(4, -4), ref _missing, ref _missing);
            exteriorRing2PointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(1, -4), ref _missing, ref _missing);

            IRing exteriorRing2 = exteriorRing2PointCollection as IRing;
            exteriorRing2.Close();

            geometryCollection.AddGeometry(exteriorRing2 as IGeometry, ref _missing, ref _missing);

            //Interior Ring 2

            IPointCollection interiorRing2PointCollection = new RingClass();
            interiorRing2PointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(1.5, -1.5), ref _missing, ref _missing);
            interiorRing2PointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(3.5, -1.5), ref _missing, ref _missing);
            interiorRing2PointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(3.5, -3.5), ref _missing, ref _missing);
            interiorRing2PointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(1.5, -3.5), ref _missing, ref _missing);

            IRing interiorRing2 = interiorRing2PointCollection as IRing;
            interiorRing2.Close();

            geometryCollection.AddGeometry(interiorRing2 as IGeometry, ref _missing, ref _missing);

            //Exterior Ring 3

            IPointCollection exteriorRing3PointCollection = new RingClass();
            exteriorRing3PointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(-1, 1), ref _missing, ref _missing);
            exteriorRing3PointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(-4, 1), ref _missing, ref _missing);
            exteriorRing3PointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(-4, 4), ref _missing, ref _missing);
            exteriorRing3PointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(-1, 4), ref _missing, ref _missing);

            IRing exteriorRing3 = exteriorRing3PointCollection as IRing;
            exteriorRing3.Close();

            geometryCollection.AddGeometry(exteriorRing3 as IGeometry, ref _missing, ref _missing);

            //Interior Ring 3

            IPointCollection interiorRing3PointCollection = new RingClass();
            interiorRing3PointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(-1.5, 1.5), ref _missing, ref _missing);
            interiorRing3PointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(-3.5, 1.5), ref _missing, ref _missing);
            interiorRing3PointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(-3.5, 3.5), ref _missing, ref _missing);
            interiorRing3PointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(-1.5, 3.5), ref _missing, ref _missing);

            IRing interiorRing3 = interiorRing3PointCollection as IRing;
            interiorRing3.Close();

            geometryCollection.AddGeometry(interiorRing3 as IGeometry, ref _missing, ref _missing);
            
            //Exterior Ring 4

            IPointCollection exteriorRing4PointCollection = new RingClass();
            exteriorRing4PointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(-1, -1), ref _missing, ref _missing);
            exteriorRing4PointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(-1, -4), ref _missing, ref _missing);
            exteriorRing4PointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(-4, -4), ref _missing, ref _missing);
            exteriorRing4PointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(-4, -1), ref _missing, ref _missing);

            IRing exteriorRing4 = exteriorRing4PointCollection as IRing;
            exteriorRing4.Close();

            geometryCollection.AddGeometry(exteriorRing4 as IGeometry, ref _missing, ref _missing);

            //Interior Ring 5

            IPointCollection interiorRing4PointCollection = new RingClass();
            interiorRing4PointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(-1.5, -1.5), ref _missing, ref _missing);
            interiorRing4PointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(-1.5, -3.5), ref _missing, ref _missing);
            interiorRing4PointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(-3.5, -3.5), ref _missing, ref _missing);
            interiorRing4PointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(-3.5, -1.5), ref _missing, ref _missing);

            IRing interiorRing4 = interiorRing4PointCollection as IRing;
            interiorRing4.Close();

            geometryCollection.AddGeometry(interiorRing4 as IGeometry, ref _missing, ref _missing);
            
            IGeometry polygonGeometry = polygon as IGeometry;

            ITopologicalOperator topologicalOperator = polygonGeometry as ITopologicalOperator;
            topologicalOperator.Simplify();

            IConstructMultiPatch constructMultiPatch = new MultiPatchClass();
            constructMultiPatch.ConstructExtrudeFromTo(FromZ, ToZ, polygonGeometry);

            return constructMultiPatch as IGeometry;
        }

        public static IGeometry GetExample6()
        {
            const double CircleDegrees = 360.0;
            const int CircleDivisions = 36;
            const double VectorComponentOffset = 0.0000001;
            const double CircleRadius = 3.0;
            const double BaseZ = -10;
            const double ToZ = -3;

            //Extrusion: 3D Circle Polygon Having Vertices With Varying Z Values, Extruded To Specified Z Value
            //           Via ConstrucExtrudeAbsolute()

            IGeometryCollection multiPatchGeometryCollection = new MultiPatchClass();

            IPointCollection polygonPointCollection = new PolygonClass();

            IPoint originPoint = GeometryUtilities.ConstructPoint3D(0, 0, 0);

            IVector3D upperAxisVector3D = GeometryUtilities.ConstructVector3D(0, 0, 10);

            IVector3D lowerAxisVector3D = GeometryUtilities.ConstructVector3D(0, 0, -10);

            lowerAxisVector3D.XComponent += VectorComponentOffset;

            IVector3D normalVector3D = upperAxisVector3D.CrossProduct(lowerAxisVector3D) as IVector3D;

            normalVector3D.Magnitude = CircleRadius;

            double rotationAngleInRadians = GeometryUtilities.GetRadians(CircleDegrees / CircleDivisions);

            Random random = new Random();

            for (int i = 0; i < CircleDivisions; i++)
            {
                normalVector3D.Rotate(-1 * rotationAngleInRadians, upperAxisVector3D);

                IPoint vertexPoint = GeometryUtilities.ConstructPoint3D(originPoint.X + normalVector3D.XComponent,
                                                                        originPoint.Y + normalVector3D.YComponent,
                                                                        BaseZ + 2 * Math.Sin(random.NextDouble()));

                polygonPointCollection.AddPoint(vertexPoint, ref _missing, ref _missing);
            }

            IPolygon polygon = polygonPointCollection as IPolygon;
            polygon.Close();

            IGeometry polygonGeometry = polygon as IGeometry;

            GeometryUtilities.MakeZAware(polygonGeometry);

            ITopologicalOperator topologicalOperator = polygon as ITopologicalOperator;
            topologicalOperator.Simplify();

            IConstructMultiPatch constructMultiPatch = new MultiPatchClass();
            constructMultiPatch.ConstructExtrudeAbsolute(ToZ, polygonGeometry);

            return constructMultiPatch as IGeometry;
        }

        public static IGeometry GetExample7()
        {
            const int DensificationDivisions = 20;
            const double MaxDeviation = 0.1;
            const double BaseZ = 0;
            const double ToZ = -10;

            //Extrusion: 3D Polyline Having Vertices With Varying Z Values, Extruded To Specified Z Value
            //           Via ConstructExtrudeAbsolute()

            IPointCollection polylinePointCollection = new PolylineClass();

            polylinePointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(-10, -10), ref _missing, ref _missing);
            polylinePointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(0, -5), ref _missing, ref _missing);
            polylinePointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(0, 5), ref _missing, ref _missing);
            polylinePointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(10, 10), ref _missing, ref _missing);

            IPolyline polyline = polylinePointCollection as IPolyline;
            polyline.Densify(polyline.Length / DensificationDivisions, MaxDeviation);

            IGeometry polylineGeometry = polyline as IGeometry;

            GeometryUtilities.MakeZAware(polylineGeometry);

            Random random = new Random();

            for (int i = 0; i < polylinePointCollection.PointCount; i++)
            {
                IPoint polylinePoint = polylinePointCollection.get_Point(i);

                polylinePointCollection.UpdatePoint(i, GeometryUtilities.ConstructPoint3D(polylinePoint.X, polylinePoint.Y, BaseZ - 2 * Math.Sin(random.NextDouble())));
            }

            ITopologicalOperator topologicalOperator = polylineGeometry as ITopologicalOperator;
            topologicalOperator.Simplify();

            IConstructMultiPatch constructMultiPatch = new MultiPatchClass();
            constructMultiPatch.ConstructExtrudeAbsolute(ToZ, polylineGeometry);

            return constructMultiPatch as IGeometry;
        }

        public static IGeometry GetExample8()
        {
            const double CircleDegrees = 360.0;
            const int CircleDivisions = 36;
            const double VectorComponentOffset = 0.0000001;
            const double CircleRadius = 3.0;
            const double BaseZ = -10;
            const double OffsetZ = 5;

            //Extrusion: 3D Circle Polygon Having Vertices With Varying Z Values, Extruded Relative To Existing 
            //           Vertex Z Values Via ConstructExtrude()

            IGeometryCollection multiPatchGeometryCollection = new MultiPatchClass();

            IPointCollection polygonPointCollection = new PolygonClass();

            IPoint originPoint = GeometryUtilities.ConstructPoint3D(0, 0, 0);

            IVector3D upperAxisVector3D = GeometryUtilities.ConstructVector3D(0, 0, 10);

            IVector3D lowerAxisVector3D = GeometryUtilities.ConstructVector3D(0, 0, -10);

            lowerAxisVector3D.XComponent += VectorComponentOffset;

            IVector3D normalVector3D = upperAxisVector3D.CrossProduct(lowerAxisVector3D) as IVector3D;

            normalVector3D.Magnitude = CircleRadius;

            double rotationAngleInRadians = GeometryUtilities.GetRadians(CircleDegrees / CircleDivisions);

            Random random = new Random();

            for (int i = 0; i < CircleDivisions; i++)
            {
                normalVector3D.Rotate(-1 * rotationAngleInRadians, upperAxisVector3D);

                IPoint vertexPoint = GeometryUtilities.ConstructPoint3D(originPoint.X + normalVector3D.XComponent,
                                                                        originPoint.Y + normalVector3D.YComponent,
                                                                        BaseZ + 2 * Math.Sin(random.NextDouble()));

                polygonPointCollection.AddPoint(vertexPoint, ref _missing, ref _missing);
            }

            IPolygon polygon = polygonPointCollection as IPolygon;
            polygon.Close();

            IGeometry polygonGeometry = polygon as IGeometry;

            GeometryUtilities.MakeZAware(polygonGeometry);

            ITopologicalOperator topologicalOperator = polygon as ITopologicalOperator;
            topologicalOperator.Simplify();

            IConstructMultiPatch constructMultiPatch = new MultiPatchClass();
            constructMultiPatch.ConstructExtrude(OffsetZ, polygonGeometry);

            return constructMultiPatch as IGeometry;
        }

        public static IGeometry GetExample9()
        {
            const int DensificationDivisions = 20;
            const double MaxDeviation = 0.1;
            const double BaseZ = 0;
            const double OffsetZ = -7;

            //Extrusion: 3D Polyline Having Vertices With Varying Z Values, Extruded Relative To Existing
            //           Vertex Z Values Via ConstructExtrude()

            IPointCollection polylinePointCollection = new PolylineClass();

            polylinePointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(-10, -10), ref _missing, ref _missing);
            polylinePointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(0, -5), ref _missing, ref _missing);
            polylinePointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(0, 5), ref _missing, ref _missing);
            polylinePointCollection.AddPoint(GeometryUtilities.ConstructPoint2D(10, 10), ref _missing, ref _missing);

            IPolyline polyline = polylinePointCollection as IPolyline;
            polyline.Densify(polyline.Length / DensificationDivisions, MaxDeviation);

            IGeometry polylineGeometry = polyline as IGeometry;

            GeometryUtilities.MakeZAware(polylineGeometry);

            Random random = new Random();

            for (int i = 0; i < polylinePointCollection.PointCount; i++)
            {
                IPoint polylinePoint = polylinePointCollection.get_Point(i);

                polylinePointCollection.UpdatePoint(i, GeometryUtilities.ConstructPoint3D(polylinePoint.X, polylinePoint.Y, BaseZ - 2 * Math.Sin(random.NextDouble())));
            }

            ITopologicalOperator topologicalOperator = polylineGeometry as ITopologicalOperator;
            topologicalOperator.Simplify();

            IConstructMultiPatch constructMultiPatch = new MultiPatchClass();
            constructMultiPatch.ConstructExtrude(OffsetZ, polylineGeometry);

            return constructMultiPatch as IGeometry;
        }

        public static IGeometry GetExample10()
        {
            const double CircleDegrees = 360.0;
            const int CircleDivisions = 36;
            const double VectorComponentOffset = 0.0000001;
            const double CircleRadius = 3.0;
            const double BaseZ = 0.0;

            //Extrusion: 3D Circle Polygon Extruded Along 3D Line Via ConstructExtrudeAlongLine()

            IPointCollection polygonPointCollection = new PolygonClass();

            IGeometry polygonGeometry = polygonPointCollection as IGeometry;

            GeometryUtilities.MakeZAware(polygonGeometry);

            IPoint originPoint = GeometryUtilities.ConstructPoint3D(0, 0, 0);

            IVector3D upperAxisVector3D = GeometryUtilities.ConstructVector3D(0, 0, 10);

            IVector3D lowerAxisVector3D = GeometryUtilities.ConstructVector3D(0, 0, -10);

            lowerAxisVector3D.XComponent += VectorComponentOffset;

            IVector3D normalVector3D = upperAxisVector3D.CrossProduct(lowerAxisVector3D) as IVector3D;

            normalVector3D.Magnitude = CircleRadius;

            double rotationAngleInRadians = GeometryUtilities.GetRadians(CircleDegrees / CircleDivisions);

            for (int i = 0; i < CircleDivisions; i++)
            {
                normalVector3D.Rotate(-1 * rotationAngleInRadians, upperAxisVector3D);

                IPoint vertexPoint = GeometryUtilities.ConstructPoint3D(originPoint.X + normalVector3D.XComponent,
                                                                      originPoint.Y + normalVector3D.YComponent,
                                                                      BaseZ);

                polygonPointCollection.AddPoint(vertexPoint, ref _missing, ref _missing);
            }

            polygonPointCollection.AddPoint(polygonPointCollection.get_Point(0), ref _missing, ref _missing);

            ITopologicalOperator topologicalOperator = polygonGeometry as ITopologicalOperator;
            topologicalOperator.Simplify();

            //Define Line To Extrude Along

            ILine extrusionLine = new LineClass();
            extrusionLine.FromPoint = GeometryUtilities.ConstructPoint3D(-4, -4, -5);
            extrusionLine.ToPoint = GeometryUtilities.ConstructPoint3D(4, 4, 5);

            //Perform Extrusion

            IConstructMultiPatch constructMultiPatch = new MultiPatchClass();
            constructMultiPatch.ConstructExtrudeAlongLine(extrusionLine, polygonGeometry);

            //Transform Extrusion Result

            IArea area = polygonGeometry as IArea;

            ITransform2D transform2D = constructMultiPatch as ITransform2D;
            transform2D.Move(extrusionLine.FromPoint.X - area.Centroid.X, extrusionLine.FromPoint.Y - area.Centroid.Y);

            return constructMultiPatch as IGeometry;
        }

        public static IGeometry GetExample11()
        {
            const double CircleDegrees = 360.0;
            const int CircleDivisions = 36;
            const double VectorComponentOffset = 0.0000001;
            const double CircleRadius = 3.0;
            const double BaseZ = 0.0;

            //Extrusion: 3D Circle Polyline Extruded Along 3D Line Via ConstructExtrudeAlongLine()

            IPointCollection polylinePointCollection = new PolylineClass();

            IGeometry polylineGeometry = polylinePointCollection as IGeometry;

            GeometryUtilities.MakeZAware(polylineGeometry);

            IPoint originPoint = GeometryUtilities.ConstructPoint3D(0, 0, 0);

            IVector3D upperAxisVector3D = GeometryUtilities.ConstructVector3D(0, 0, 10);

            IVector3D lowerAxisVector3D = GeometryUtilities.ConstructVector3D(0, 0, -10);

            lowerAxisVector3D.XComponent += VectorComponentOffset;

            IVector3D normalVector3D = upperAxisVector3D.CrossProduct(lowerAxisVector3D) as IVector3D;

            normalVector3D.Magnitude = CircleRadius;

            double rotationAngleInRadians = GeometryUtilities.GetRadians(CircleDegrees / CircleDivisions);

            for (int i = 0; i < CircleDivisions; i++)
            {
                normalVector3D.Rotate(-1 * rotationAngleInRadians, upperAxisVector3D);

                IPoint vertexPoint = GeometryUtilities.ConstructPoint3D(originPoint.X + normalVector3D.XComponent,
                                                                      originPoint.Y + normalVector3D.YComponent,
                                                                      BaseZ);

                polylinePointCollection.AddPoint(vertexPoint, ref _missing, ref _missing);
            }

            polylinePointCollection.AddPoint(polylinePointCollection.get_Point(0), ref _missing, ref _missing);

            ITopologicalOperator topologicalOperator = polylineGeometry as ITopologicalOperator;
            topologicalOperator.Simplify();

            //Define Line To Extrude Along

            ILine extrusionLine = new LineClass();
            extrusionLine.FromPoint = GeometryUtilities.ConstructPoint3D(-4, -4, -5);
            extrusionLine.ToPoint = GeometryUtilities.ConstructPoint3D(4, 4, 5);

            //Perform Extrusion

            IConstructMultiPatch constructMultiPatch = new MultiPatchClass();
            constructMultiPatch.ConstructExtrudeAlongLine(extrusionLine, polylineGeometry);

            //Transform Extrusion Result

            IPoint centroid = GeometryUtilities.ConstructPoint2D(0.5 * (polylineGeometry.Envelope.XMax + polylineGeometry.Envelope.XMin),
                                                                 0.5 * (polylineGeometry.Envelope.YMax + polylineGeometry.Envelope.YMin));

            ITransform2D transform2D = constructMultiPatch as ITransform2D;
            transform2D.Move(extrusionLine.FromPoint.X - centroid.X, extrusionLine.FromPoint.Y - centroid.Y);

            return constructMultiPatch as IGeometry;
        }

        public static IGeometry GetExample12()
        {
            const double CircleDegrees = 360.0;
            const int CircleDivisions = 36;
            const double VectorComponentOffset = 0.0000001;
            const double CircleRadius = 3.0;
            const double BaseZ = 0.0;
            const double RotationAngleInDegrees = 89.9;

            //Extrusion: 3D Circle Polygon Extruded Along 3D Vector Via ConstructExtrudeRelative()

            IPointCollection pathPointCollection = new PathClass();

            IGeometry pathGeometry = pathPointCollection as IGeometry;

            GeometryUtilities.MakeZAware(pathGeometry);

            IPoint originPoint = GeometryUtilities.ConstructPoint3D(0, 0, 0);

            IVector3D upperAxisVector3D = GeometryUtilities.ConstructVector3D(0, 0, 10);

            IVector3D lowerAxisVector3D = GeometryUtilities.ConstructVector3D(0, 0, -10);

            lowerAxisVector3D.XComponent += VectorComponentOffset;

            IVector3D normalVector3D = upperAxisVector3D.CrossProduct(lowerAxisVector3D) as IVector3D;

            normalVector3D.Magnitude = CircleRadius;

            double rotationAngleInRadians = GeometryUtilities.GetRadians(CircleDegrees / CircleDivisions);

            for (int i = 0; i < CircleDivisions; i++)
            {
                normalVector3D.Rotate(-1 * rotationAngleInRadians, upperAxisVector3D);

                IPoint vertexPoint = GeometryUtilities.ConstructPoint3D(originPoint.X + normalVector3D.XComponent,
                                                                      originPoint.Y + normalVector3D.YComponent,
                                                                      BaseZ);

                pathPointCollection.AddPoint(vertexPoint, ref _missing, ref _missing);
            }

            pathPointCollection.AddPoint(pathPointCollection.get_Point(0), ref _missing, ref _missing);

            //Rotate Geometry

            IVector3D rotationAxisVector3D = GeometryUtilities.ConstructVector3D(0, 10, 0);

            ITransform3D transform3D = pathGeometry as ITransform3D;
            transform3D.RotateVector3D(rotationAxisVector3D, GeometryUtilities.GetRadians(RotationAngleInDegrees));

            //Construct Polygon From Path Vertices

            IGeometry polygonGeometry = new PolygonClass();

            GeometryUtilities.MakeZAware(polygonGeometry);

            IPointCollection polygonPointCollection = polygonGeometry as IPointCollection;

            for (int i = 0; i < pathPointCollection.PointCount; i++)
            {
                polygonPointCollection.AddPoint(pathPointCollection.get_Point(i), ref _missing, ref _missing);
            }

            ITopologicalOperator topologicalOperator = polygonGeometry as ITopologicalOperator;
            topologicalOperator.Simplify();

            //Define Vector To Extrude Along

            IVector3D extrusionVector3D = GeometryUtilities.ConstructVector3D(10, 0, 5);

            //Perform Extrusion

            IConstructMultiPatch constructMultiPatch = new MultiPatchClass();
            constructMultiPatch.ConstructExtrudeRelative(extrusionVector3D, polygonGeometry);

            return constructMultiPatch as IGeometry;
        }

        public static IGeometry GetExample13()
        {
            const double CircleDegrees = 360.0;
            const int CircleDivisions = 36;
            const double VectorComponentOffset = 0.0000001;
            const double CircleRadius = 3.0;
            const double BaseZ = 0.0;
            const double RotationAngleInDegrees = 89.9;

            //Extrusion: 3D Circle Polyline Extruded Along 3D Vector Via ConstructExtrudeRelative()

            IPointCollection pathPointCollection = new PathClass();

            IGeometry pathGeometry = pathPointCollection as IGeometry;

            GeometryUtilities.MakeZAware(pathGeometry);

            IPoint originPoint = GeometryUtilities.ConstructPoint3D(0, 0, 0);

            IVector3D upperAxisVector3D = GeometryUtilities.ConstructVector3D(0, 0, 10);

            IVector3D lowerAxisVector3D = GeometryUtilities.ConstructVector3D(0, 0, -10);

            lowerAxisVector3D.XComponent += VectorComponentOffset;

            IVector3D normalVector3D = upperAxisVector3D.CrossProduct(lowerAxisVector3D) as IVector3D;

            normalVector3D.Magnitude = CircleRadius;

            double rotationAngleInRadians = GeometryUtilities.GetRadians(CircleDegrees / CircleDivisions);

            for (int i = 0; i < CircleDivisions; i++)
            {
                normalVector3D.Rotate(-1 * rotationAngleInRadians, upperAxisVector3D);

                IPoint vertexPoint = GeometryUtilities.ConstructPoint3D(originPoint.X + normalVector3D.XComponent,
                                                                      originPoint.Y + normalVector3D.YComponent,
                                                                      BaseZ);

                pathPointCollection.AddPoint(vertexPoint, ref _missing, ref _missing);
            }

            pathPointCollection.AddPoint(pathPointCollection.get_Point(0), ref _missing, ref _missing);

            //Rotate Geometry

            IVector3D rotationAxisVector3D = GeometryUtilities.ConstructVector3D(0, 10, 0);

            ITransform3D transform3D = pathGeometry as ITransform3D;
            transform3D.RotateVector3D(rotationAxisVector3D, GeometryUtilities.GetRadians(RotationAngleInDegrees));

            //Construct Polyline From Path Vertices

            IGeometry polylineGeometry = new PolylineClass();

            GeometryUtilities.MakeZAware(polylineGeometry);

            IPointCollection polylinePointCollection = polylineGeometry as IPointCollection;

            for (int i = 0; i < pathPointCollection.PointCount; i++)
            {
                polylinePointCollection.AddPoint(pathPointCollection.get_Point(i), ref _missing, ref _missing);
            }

            ITopologicalOperator topologicalOperator = polylineGeometry as ITopologicalOperator;
            topologicalOperator.Simplify();

            //Define Vector To Extrude Along

            IVector3D extrusionVector3D = GeometryUtilities.ConstructVector3D(10, 0, 5);

            //Perform Extrusion

            IConstructMultiPatch constructMultiPatch = new MultiPatchClass();
            constructMultiPatch.ConstructExtrudeRelative(extrusionVector3D, polylineGeometry);

            return constructMultiPatch as IGeometry;
        }

        public static IGeometry GetExample14()
        {
            const int PointCount = 100;
            const double ZMin = 0;
            const double ZMax = 4;

            //Extrusion: Square Shaped Base Geometry Extruded Between Single TIN-Based Functional Surface

            IGeometryCollection multiPatchGeometryCollection = new MultiPatchClass();

            //Base Geometry

            IEnvelope envelope = new EnvelopeClass();
            envelope.XMin = -10;
            envelope.XMax = 10;
            envelope.YMin = -10;
            envelope.YMax = 10;

            IGeometry baseGeometry = envelope as IGeometry;

            //Upper Functional Surface

            ITinEdit tinEdit = new TinClass();
            tinEdit.InitNew(envelope);

            Random random = new Random();

            for (int i = 0; i < PointCount; i++)
            {
                double x = envelope.XMin + (envelope.XMax - envelope.XMin) * random.NextDouble();
                double y = envelope.YMin + (envelope.YMax - envelope.YMin) * random.NextDouble();
                double z = ZMin + (ZMax - ZMin) * random.NextDouble();

                IPoint point = GeometryUtilities.ConstructPoint3D(x, y, z);

                tinEdit.AddPointZ(point, 0);
            }

            IFunctionalSurface functionalSurface = tinEdit as IFunctionalSurface;

            IConstructMultiPatch constructMultiPatch = new MultiPatchClass();
            constructMultiPatch.ConstructExtrudeBetween(functionalSurface, functionalSurface, baseGeometry);

            return constructMultiPatch as IGeometry;
        }

        public static IGeometry GetExample15()
        {
            const double CircleDegrees = 360.0;
            const int CircleDivisions = 36;
            const double VectorComponentOffset = 0.0000001;
            const double CircleRadius = 9.5;
            const int PointCount = 100;
            const double UpperZMin = 7;
            const double UpperZMax = 10;
            const double LowerZMin = 0;
            const double LowerZMax = 3;

            //Extrusion: Circle Shaped Base Geometry Extruded Between Two Different TIN-Based Functional Surfaces

            IGeometryCollection multiPatchGeometryCollection = new MultiPatchClass();

            //Base Geometry

            IPointCollection polygonPointCollection = new PolygonClass();

            IPoint originPoint = GeometryUtilities.ConstructPoint3D(0, 0, 0);

            IVector3D upperAxisVector3D = GeometryUtilities.ConstructVector3D(0, 0, 10);

            IVector3D lowerAxisVector3D = GeometryUtilities.ConstructVector3D(0, 0, -10);

            lowerAxisVector3D.XComponent += VectorComponentOffset;

            IVector3D normalVector3D = upperAxisVector3D.CrossProduct(lowerAxisVector3D) as IVector3D;

            normalVector3D.Magnitude = CircleRadius;

            double rotationAngleInRadians = GeometryUtilities.GetRadians(CircleDegrees / CircleDivisions);

            for (int i = 0; i < CircleDivisions; i++)
            {
                normalVector3D.Rotate(-1 * rotationAngleInRadians, upperAxisVector3D);

                IPoint vertexPoint = GeometryUtilities.ConstructPoint2D(originPoint.X + normalVector3D.XComponent,
                                                                        originPoint.Y + normalVector3D.YComponent);

                polygonPointCollection.AddPoint(vertexPoint, ref _missing, ref _missing);
            }

            IPolygon polygon = polygonPointCollection as IPolygon;
            polygon.Close();

            IGeometry baseGeometry = polygon as IGeometry;

            ITopologicalOperator topologicalOperator = polygon as ITopologicalOperator;
            topologicalOperator.Simplify();

            //Functional Surfaces

            IEnvelope envelope = new EnvelopeClass();
            envelope.XMin = -10;
            envelope.XMax = 10;
            envelope.YMin = -10;
            envelope.YMax = 10;

            Random random = new Random();

            //Upper Functional Surface

            ITinEdit upperTinEdit = new TinClass();
            upperTinEdit.InitNew(envelope);

            for (int i = 0; i < PointCount; i++)
            {
                double x = envelope.XMin + (envelope.XMax - envelope.XMin) * random.NextDouble();
                double y = envelope.YMin + (envelope.YMax - envelope.YMin) * random.NextDouble();
                double z = UpperZMin + (UpperZMax - UpperZMin) * random.NextDouble();

                IPoint point = GeometryUtilities.ConstructPoint3D(x, y, z);

                upperTinEdit.AddPointZ(point, 0);
            }

            IFunctionalSurface upperFunctionalSurface = upperTinEdit as IFunctionalSurface;

            //Lower Functional Surface

            ITinEdit lowerTinEdit = new TinClass();
            lowerTinEdit.InitNew(envelope);

            for (int i = 0; i < PointCount; i++)
            {
                double x = envelope.XMin + (envelope.XMax - envelope.XMin) * random.NextDouble();
                double y = envelope.YMin + (envelope.YMax - envelope.YMin) * random.NextDouble();
                double z = LowerZMin + (LowerZMax - LowerZMin) * random.NextDouble();

                IPoint point = GeometryUtilities.ConstructPoint3D(x, y, z);

                lowerTinEdit.AddPointZ(point, 0);
            }

            IFunctionalSurface lowerFunctionalSurface = lowerTinEdit as IFunctionalSurface;

            IConstructMultiPatch constructMultiPatch = new MultiPatchClass();
            constructMultiPatch.ConstructExtrudeBetween(upperFunctionalSurface, lowerFunctionalSurface, baseGeometry);

            return constructMultiPatch as IGeometry;
        }
    }
}