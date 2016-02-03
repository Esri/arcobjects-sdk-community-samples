using System;
using ESRI.ArcGIS.Geometry;

namespace MultiPatchExamples
{
    public static class Transform3DExamples
    {
        private static object _missing = Type.Missing;

        public static IGeometry GetExample1()
        {
            const double XOffset = 7.5;
            const double YOffset = 7.5;
            const double ZOffset = -10;

            //Transform3D: Cylinder Repositioned Via Move3D()

            IGeometry geometry = Vector3DExamples.GetExample3();

            ITransform3D transform3D = geometry as ITransform3D;
            transform3D.Move3D(XOffset, YOffset, ZOffset);

            return geometry;
        }

        public static IGeometry GetExample2()
        {
            const double XScale = 2;
            const double YScale = 2;
            const double ZScale = 3;

            //Transform3D: Cylinder Scaled Via Scale3D()

            IGeometry geometry = Vector3DExamples.GetExample3();

            //Define Origin At Which Scale Operation Should Be Performed

            IPoint originPoint = GeometryUtilities.ConstructPoint3D(0, 0, 0);

            ITransform3D transform3D = geometry as ITransform3D;
            transform3D.Scale3D(originPoint, XScale, YScale, ZScale);

            return geometry;
        }

        public static IGeometry GetExample3()
        {
            const double DegreesOfRotation = 45;

            //Transform3D: Cylinder Rotated Around An Axis Via RotateVector3D()

            IGeometry geometry = Vector3DExamples.GetExample3();

            //Construct A Vector3D Corresponding To The Desired Axis Of Rotation

            IVector3D axisOfRotationVector3D = GeometryUtilities.ConstructVector3D(0, 10, 0);

            //Obtain Angle Of Rotation In Radians

            double angleOfRotationInRadians = GeometryUtilities.GetRadians(DegreesOfRotation);

            ITransform3D transform3D = geometry as ITransform3D;
            transform3D.RotateVector3D(axisOfRotationVector3D, angleOfRotationInRadians);

            return geometry;
        }

        public static IGeometry GetExample4()
        {
            const double XScale = 0.5;
            const double YScale = 0.5;
            const double ZScale = 2;
            const double XOffset = -5;
            const double YOffset = -5;
            const double ZOffset = -8;
            const double DegreesOfRotation = 90;

            //Transform3D: Cylinder Scaled, Rotated, Repositioned Via Move3D(), Scale3D(), RotateVector3D()

            IGeometry geometry = Vector3DExamples.GetExample3();

            ITransform3D transform3D = geometry as ITransform3D;

            //Stretch The Cylinder So It Looks Like A Tube

            IPoint originPoint = GeometryUtilities.ConstructPoint3D(0, 0, 0);

            transform3D.Scale3D(originPoint, XScale, YScale, ZScale);

            //Rotate The Cylinder So It Lies On Its Side

            IVector3D axisOfRotationVector3D = GeometryUtilities.ConstructVector3D(0, 10, 0);

            double angleOfRotationInRadians = GeometryUtilities.GetRadians(DegreesOfRotation);

            transform3D.RotateVector3D(axisOfRotationVector3D, angleOfRotationInRadians);

            //Reposition The Cylinder So It Is Located Underground

            transform3D.Move3D(XOffset, YOffset, ZOffset);

            return geometry;
        }    
    }
}