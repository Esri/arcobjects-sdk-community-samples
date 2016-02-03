using ESRI.ArcGIS.Geometry;
using System;

namespace MultiPatchExamples
{
    public static class Vector3DExamples
    {
        private static object _missing = Type.Missing;

        public static IGeometry GetExample1()
        {
            const double CircleDegrees = 360.0;
            const int CircleDivisions = 36;
            const double VectorComponentOffset = 0.0000001;
            const double CircleRadius = 5.0;
            const double CircleZ = 0.0;

            //Vector3D: Circle, TriangleFan With 36 Vertices

            IGeometryCollection multiPatchGeometryCollection = new MultiPatchClass();

            IPointCollection triangleFanPointCollection = new TriangleFanClass();

            //Set Circle Origin To (0, 0, CircleZ)

            IPoint originPoint = GeometryUtilities.ConstructPoint3D(0, 0, CircleZ);

            //Add Origin Point To Triangle Fan

            triangleFanPointCollection.AddPoint(originPoint, ref _missing, ref _missing);

            //Define Upper Portion Of Axis Around Which Vector Should Be Rotated To Generate Circle Vertices

            IVector3D upperAxisVector3D = GeometryUtilities.ConstructVector3D(0, 0, 10);

            //Define Lower Portion of Axis Around Which Vector Should Be Rotated To Generate Circle Vertices

            IVector3D lowerAxisVector3D = GeometryUtilities.ConstructVector3D(0, 0, -10);

            //Add Slight Offset To X or Y Component Of One Of Axis Vectors So Cross Product Does Not Return A Zero-Length Vector

            lowerAxisVector3D.XComponent += VectorComponentOffset;

            //Obtain Cross Product Of Upper And Lower Axis Vectors To Obtain Normal Vector To Axis Of Rotation To Generate Circle Vertices

            IVector3D normalVector3D = upperAxisVector3D.CrossProduct(lowerAxisVector3D) as IVector3D;

            //Set Normal Vector Magnitude Equal To Radius Of Circle

            normalVector3D.Magnitude = CircleRadius;

            //Obtain Angle Of Rotation In Radians As Function Of Number Of Divisions Within 360 Degree Sweep Of Circle

            double rotationAngleInRadians = GeometryUtilities.GetRadians(CircleDegrees / CircleDivisions);

            for (int i = 0; i < CircleDivisions; i++)
            {
                //Rotate Normal Vector Specified Rotation Angle In Radians Around Either Upper Or Lower Axis

                normalVector3D.Rotate(-1 * rotationAngleInRadians, upperAxisVector3D);

                //Construct Circle Vertex Whose XY Coordinates Are The Sum Of Origin XY Coordinates And Normal Vector XY Components

                IPoint vertexPoint = GeometryUtilities.ConstructPoint3D(originPoint.X + normalVector3D.XComponent,
                                                                      originPoint.Y + normalVector3D.YComponent,
                                                                      CircleZ);

                //Add Vertex To TriangleFan

                triangleFanPointCollection.AddPoint(vertexPoint, ref _missing, ref _missing);
            }

            //Re-Add The Second Point Of The Triangle Fan (First Vertex Added) To Close The Fan

            triangleFanPointCollection.AddPoint(triangleFanPointCollection.get_Point(1), ref _missing, ref _missing);

            //Add TriangleFan To MultiPatch

            multiPatchGeometryCollection.AddGeometry(triangleFanPointCollection as IGeometry, ref _missing, ref _missing);

            return multiPatchGeometryCollection as IGeometry;
        }

        public static IGeometry GetExample2()
        {
            const double ConeBaseDegrees = 360.0;
            const int ConeBaseDivisions = 36;
            const double VectorComponentOffset = 0.0000001;
            const double ConeBaseRadius = 6;
            const double ConeBaseZ = 0.0;
            const double ConeApexZ = 9.5;

            //Vector3D: Cone, TriangleFan With 36 Vertices

            IGeometryCollection multiPatchGeometryCollection = new MultiPatchClass();

            IPointCollection triangleFanPointCollection = new TriangleFanClass();

            //Set Cone Apex To (0, 0, ConeApexZ)

            IPoint coneApexPoint = GeometryUtilities.ConstructPoint3D(0, 0, ConeApexZ);

            //Add Cone Apex To Triangle Fan

            triangleFanPointCollection.AddPoint(coneApexPoint, ref _missing, ref _missing);

            //Define Upper Portion Of Axis Around Which Vector Should Be Rotated To Generate Cone Base Vertices

            IVector3D upperAxisVector3D = GeometryUtilities.ConstructVector3D(0, 0, 10);

            //Define Lower Portion of Axis Around Which Vector Should Be Rotated To Generate Cone Base Vertices

            IVector3D lowerAxisVector3D = GeometryUtilities.ConstructVector3D(0, 0, -10);

            //Add A Slight Offset To X or Y Component Of One Of Axis Vectors So Cross Product Does Not Return A Zero-Length Vector

            lowerAxisVector3D.XComponent += VectorComponentOffset;

            //Obtain Cross Product Of Upper And Lower Axis Vectors To Obtain Normal Vector To Axis Of Rotation To Generate Cone Base Vertices

            IVector3D normalVector3D = upperAxisVector3D.CrossProduct(lowerAxisVector3D) as IVector3D;

            //Set Normal Vector Magnitude Equal To Radius Of Cone Base

            normalVector3D.Magnitude = ConeBaseRadius;

            //Obtain Angle Of Rotation In Radians As Function Of Number Of Divisions Within 360 Degree Sweep Of Cone Base

            double rotationAngleInRadians = GeometryUtilities.GetRadians(ConeBaseDegrees / ConeBaseDivisions);

            for (int i = 0; i < ConeBaseDivisions; i++)
            {
                //Rotate Normal Vector Specified Rotation Angle In Radians Around Either Upper Or Lower Axis

                normalVector3D.Rotate(-1 * rotationAngleInRadians, upperAxisVector3D);

                //Construct Cone Base Vertex Whose XY Coordinates Are The Sum Of Apex XY Coordinates And Normal Vector XY Components

                IPoint vertexPoint = GeometryUtilities.ConstructPoint3D(coneApexPoint.X + normalVector3D.XComponent,
                                                                      coneApexPoint.Y + normalVector3D.YComponent,
                                                                      ConeBaseZ);

                //Add Vertex To TriangleFan

                triangleFanPointCollection.AddPoint(vertexPoint, ref _missing, ref _missing);
            }

            //Re-Add The Second Point Of The Triangle Fan (First Vertex Added) To Close The Fan

            triangleFanPointCollection.AddPoint(triangleFanPointCollection.get_Point(1), ref _missing, ref _missing);

            //Add TriangleFan To MultiPatch

            multiPatchGeometryCollection.AddGeometry(triangleFanPointCollection as IGeometry, ref _missing, ref _missing);

            return multiPatchGeometryCollection as IGeometry;
        }

        public static IGeometry GetExample3()
        {
            const double CylinderBaseDegrees = 360.0;
            const int CylinderBaseDivisions = 36;
            const double VectorComponentOffset = 0.0000001;
            const double CylinderBaseRadius = 3;
            const double CylinderUpperZ = 8;
            const double CylinderLowerZ = 0;

            //Vector3D: Cylinder, TriangleStrip With 36 Vertices

            IGeometryCollection multiPatchGeometryCollection = new MultiPatchClass();

            IPointCollection triangleStripPointCollection = new TriangleStripClass();

            //Set Cylinder Base Origin To (0, 0, 0)

            IPoint originPoint = GeometryUtilities.ConstructPoint3D(0, 0, 0);

            //Define Upper Portion Of Axis Around Which Vector Should Be Rotated To Generate Cylinder Base Vertices

            IVector3D upperAxisVector3D = GeometryUtilities.ConstructVector3D(0, 0, 10);

            //Define Lower Portion of Axis Around Which Vector Should Be Rotated To Generate Cylinder Base Vertices

            IVector3D lowerAxisVector3D = GeometryUtilities.ConstructVector3D(0, 0, -10);

            //Add A Slight Offset To X or Y Component Of One Of Axis Vectors So Cross Product Does Not Return A Zero-Length Vector

            lowerAxisVector3D.XComponent += VectorComponentOffset;

            //Obtain Cross Product Of Upper And Lower Axis Vectors To Obtain Normal Vector To Axis Of Rotation To Generate Cylinder Base Vertices

            IVector3D normalVector3D = upperAxisVector3D.CrossProduct(lowerAxisVector3D) as IVector3D;

            //Set Normal Vector Magnitude Equal To Radius Of Cylinder Base

            normalVector3D.Magnitude = CylinderBaseRadius;

            //Obtain Angle Of Rotation In Radians As Function Of Number Of Divisions Within 360 Degree Sweep Of Cylinder Base

            double rotationAngleInRadians = GeometryUtilities.GetRadians(CylinderBaseDegrees / CylinderBaseDivisions);

            for (int i = 0; i < CylinderBaseDivisions; i++)
            {
                //Rotate Normal Vector Specified Rotation Angle In Radians Around Either Upper Or Lower Axis

                normalVector3D.Rotate(rotationAngleInRadians, upperAxisVector3D);

                //Construct Cylinder Base Vertex Whose XY Coordinates Are The Sum Of Origin XY Coordinates And Normal Vector XY Components

                IPoint vertexPoint = GeometryUtilities.ConstructPoint3D(originPoint.X + normalVector3D.XComponent,
                                                                      originPoint.Y + normalVector3D.YComponent,
                                                                      0);

                //Construct Lower Base Vertex From This Point And Add To TriangleStrip

                IPoint lowerVertexPoint = GeometryUtilities.ConstructPoint3D(vertexPoint.X, vertexPoint.Y, CylinderLowerZ);

                triangleStripPointCollection.AddPoint(lowerVertexPoint, ref _missing, ref _missing);

                //Construct Upper Base Vertex From This Point And Add To TriangleStrip

                IPoint upperVertexPoint = GeometryUtilities.ConstructPoint3D(vertexPoint.X, vertexPoint.Y, CylinderUpperZ);

                triangleStripPointCollection.AddPoint(upperVertexPoint, ref _missing, ref _missing);
            }

            //Re-Add The First And Second Points Of The Triangle Strip (First Two Vertices Added) To Close The Strip

            triangleStripPointCollection.AddPoint(triangleStripPointCollection.get_Point(0), ref _missing, ref _missing);

            triangleStripPointCollection.AddPoint(triangleStripPointCollection.get_Point(1), ref _missing, ref _missing);

            //Add TriangleStrip To MultiPatch

            multiPatchGeometryCollection.AddGeometry(triangleStripPointCollection as IGeometry, ref _missing, ref _missing);

            return multiPatchGeometryCollection as IGeometry;
        }

        public static IGeometry GetExample4()
        {
            const double ConeBaseDegrees = 360.0;
            const int ConeBaseDivisions = 8;
            const double VectorComponentOffset = 0.0000001;
            const double ConeBaseRadius = 6;
            const double ConeBaseZ = 0.0;
            const double ConeApexZ = 9.5;

            //Vector3D: Cone, TriangleFan With 8 Vertices

            IGeometryCollection multiPatchGeometryCollection = new MultiPatchClass();

            IPointCollection triangleFanPointCollection = new TriangleFanClass();

            //Set Cone Apex To (0, 0, ConeApexZ)

            IPoint coneApexPoint = GeometryUtilities.ConstructPoint3D(0, 0, ConeApexZ);

            //Add Cone Apex To Triangle Fan

            triangleFanPointCollection.AddPoint(coneApexPoint, ref _missing, ref _missing);

            //Define Upper Portion Of Axis Around Which Vector Should Be Rotated To Generate Cone Base Vertices

            IVector3D upperAxisVector3D = GeometryUtilities.ConstructVector3D(0, 0, 10);

            //Define Lower Portion of Axis Around Which Vector Should Be Rotated To Generate Cone Base Vertices

            IVector3D lowerAxisVector3D = GeometryUtilities.ConstructVector3D(0, 0, -10);

            //Add A Slight Offset To X or Y Component Of One Of Axis Vectors So Cross Product Does Not Return A Zero-Length Vector

            lowerAxisVector3D.XComponent += VectorComponentOffset;

            //Obtain Cross Product Of Upper And Lower Axis Vectors To Obtain Normal Vector To Axis Of Rotation To Generate Cone Base Vertices

            IVector3D normalVector3D = upperAxisVector3D.CrossProduct(lowerAxisVector3D) as IVector3D;

            //Set Normal Vector Magnitude Equal To Radius Of Cone Base

            normalVector3D.Magnitude = ConeBaseRadius;

            //Obtain Angle Of Rotation In Radians As Function Of Number Of Divisions Within 360 Degree Sweep Of Cone Base

            double rotationAngleInRadians = GeometryUtilities.GetRadians(ConeBaseDegrees / ConeBaseDivisions);

            for (int i = 0; i < ConeBaseDivisions; i++)
            {
                //Rotate Normal Vector Specified Rotation Angle In Radians Around Either Upper Or Lower Axis

                normalVector3D.Rotate(-1 * rotationAngleInRadians, upperAxisVector3D);

                //Construct Cone Base Vertex Whose XY Coordinates Are The Sum Of Apex XY Coordinates And Normal Vector XY Components

                IPoint vertexPoint = GeometryUtilities.ConstructPoint3D(coneApexPoint.X + normalVector3D.XComponent,
                                                                      coneApexPoint.Y + normalVector3D.YComponent,
                                                                      ConeBaseZ);

                //Add Vertex To TriangleFan

                triangleFanPointCollection.AddPoint(vertexPoint, ref _missing, ref _missing);
            }

            //Re-Add The Second Point Of The Triangle Fan (First Vertex Added) To Close The Fan

            triangleFanPointCollection.AddPoint(triangleFanPointCollection.get_Point(1), ref _missing, ref _missing);

            //Add TriangleFan To MultiPatch

            multiPatchGeometryCollection.AddGeometry(triangleFanPointCollection as IGeometry, ref _missing, ref _missing);

            return multiPatchGeometryCollection as IGeometry;
        }

        public static IGeometry GetExample5()
        {
            const double CylinderBaseDegrees = 360.0;
            const int CylinderBaseDivisions = 8;
            const double VectorComponentOffset = 0.0000001;
            const double CylinderBaseRadius = 3;
            const double CylinderUpperZ = 8;
            const double CylinderLowerZ = 0;

            //Vector3D: Cylinder, TriangleStrip With 8 Vertices

            IGeometryCollection multiPatchGeometryCollection = new MultiPatchClass();

            IPointCollection triangleStripPointCollection = new TriangleStripClass();

            //Set Cylinder Base Origin To (0, 0, 0)

            IPoint originPoint = GeometryUtilities.ConstructPoint3D(0, 0, 0);

            //Define Upper Portion Of Axis Around Which Vector Should Be Rotated To Generate Cylinder Base Vertices

            IVector3D upperAxisVector3D = GeometryUtilities.ConstructVector3D(0, 0, 10);

            //Define Lower Portion of Axis Around Which Vector Should Be Rotated To Generate Cylinder Base Vertices

            IVector3D lowerAxisVector3D = GeometryUtilities.ConstructVector3D(0, 0, -10);

            //Add A Slight Offset To X or Y Component Of One Of Axis Vectors So Cross Product Does Not Return A Zero-Length Vector

            lowerAxisVector3D.XComponent += VectorComponentOffset;

            //Obtain Cross Product Of Upper And Lower Axis Vectors To Obtain Normal Vector To Axis Of Rotation To Generate Cylinder Base Vertices

            IVector3D normalVector3D = upperAxisVector3D.CrossProduct(lowerAxisVector3D) as IVector3D;

            //Set Normal Vector Magnitude Equal To Radius Of Cylinder Base

            normalVector3D.Magnitude = CylinderBaseRadius;

            //Obtain Angle Of Rotation In Radians As Function Of Number Of Divisions Within 360 Degree Sweep Of Cylinder Base

            double rotationAngleInRadians = GeometryUtilities.GetRadians(CylinderBaseDegrees / CylinderBaseDivisions);

            for (int i = 0; i < CylinderBaseDivisions; i++)
            {
                //Rotate Normal Vector Specified Rotation Angle In Radians Around Either Upper Or Lower Axis

                normalVector3D.Rotate(rotationAngleInRadians, upperAxisVector3D);

                //Construct Cylinder Base Vertex Whose XY Coordinates Are The Sum Of Origin XY Coordinates And Normal Vector XY Components

                IPoint vertexPoint = GeometryUtilities.ConstructPoint3D(originPoint.X + normalVector3D.XComponent,
                                                                      originPoint.Y + normalVector3D.YComponent,
                                                                      0);

                //Construct Lower Base Vertex From This Point And Add To TriangleStrip

                IPoint lowerVertexPoint = GeometryUtilities.ConstructPoint3D(vertexPoint.X, vertexPoint.Y, CylinderLowerZ);

                triangleStripPointCollection.AddPoint(lowerVertexPoint, ref _missing, ref _missing);

                //Construct Upper Base Vertex From This Point And Add To TriangleStrip

                IPoint upperVertexPoint = GeometryUtilities.ConstructPoint3D(vertexPoint.X, vertexPoint.Y, CylinderUpperZ);

                triangleStripPointCollection.AddPoint(upperVertexPoint, ref _missing, ref _missing);
            }

            //Re-Add The First And Second Points Of The Triangle Strip (First Two Vertices Added) To Close The Strip

            triangleStripPointCollection.AddPoint(triangleStripPointCollection.get_Point(0), ref _missing, ref _missing);

            triangleStripPointCollection.AddPoint(triangleStripPointCollection.get_Point(1), ref _missing, ref _missing);

            //Add TriangleStrip To MultiPatch

            multiPatchGeometryCollection.AddGeometry(triangleStripPointCollection as IGeometry, ref _missing, ref _missing);

            return multiPatchGeometryCollection as IGeometry;
        }    
    }
}