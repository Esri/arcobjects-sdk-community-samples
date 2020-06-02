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
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Analyst3D;
using System;

namespace MultiPatchExamples
{
    public static class DrawUtilities
    {
        private static object _missing = Type.Missing;

        public static void DrawAxes(IGraphicsContainer3D axesGraphicsContainer3D)
        {
            const esriSimple3DLineStyle AxisStyle = esriSimple3DLineStyle.esriS3DLSTube;
            const double AxisWidth = 0.25;

            DrawAxis(axesGraphicsContainer3D, GeometryUtilities.ConstructPoint3D(-10, 0, 0), GeometryUtilities.ConstructPoint3D(10, 0, 0), ColorUtilities.GetColor(255, 0, 0), AxisStyle, AxisWidth);
            DrawAxis(axesGraphicsContainer3D, GeometryUtilities.ConstructPoint3D(0, -10, 0), GeometryUtilities.ConstructPoint3D(0, 10, 0), ColorUtilities.GetColor(0, 0, 255), AxisStyle, AxisWidth);
            DrawAxis(axesGraphicsContainer3D, GeometryUtilities.ConstructPoint3D(0, 0, -10), GeometryUtilities.ConstructPoint3D(0, 0, 10), ColorUtilities.GetColor(0, 255, 0), AxisStyle, AxisWidth);

            DrawEnd(axesGraphicsContainer3D, GeometryUtilities.ConstructPoint3D(10, 0, 0), GeometryUtilities.ConstructVector3D(0, 10, 0), 90, ColorUtilities.GetColor(255, 0, 0), 0.2 * AxisWidth);
            DrawEnd(axesGraphicsContainer3D, GeometryUtilities.ConstructPoint3D(0, 10, 0), GeometryUtilities.ConstructVector3D(10, 0, 0), -90, ColorUtilities.GetColor(0, 0, 255), 0.2 * AxisWidth);
            DrawEnd(axesGraphicsContainer3D, GeometryUtilities.ConstructPoint3D(0, 0, 10), null, 0, ColorUtilities.GetColor(0, 255, 0), 0.2 * AxisWidth);
        }

        private static void DrawAxis(IGraphicsContainer3D axesGraphicsContainer3D, IPoint axisFromPoint, IPoint axisToPoint, IColor axisColor, esriSimple3DLineStyle axisStyle, double axisWidth)
        {
            IPointCollection axisPointCollection = new PolylineClass();

            axisPointCollection.AddPoint(axisFromPoint, ref _missing, ref _missing);
            axisPointCollection.AddPoint(axisToPoint, ref _missing, ref _missing);

            GeometryUtilities.MakeZAware(axisPointCollection as IGeometry);

            GraphicsLayer3DUtilities.AddAxisToGraphicsLayer3D(axesGraphicsContainer3D, axisPointCollection as IGeometry, axisColor, axisStyle, axisWidth);
        }

        private static void DrawEnd(IGraphicsContainer3D endGraphicsContainer3D, IPoint endPoint, IVector3D axisOfRotationVector3D, double degreesOfRotation, IColor endColor, double endRadius)
        {
            IGeometry endGeometry = Vector3DExamples.GetExample2();

            ITransform3D transform3D = endGeometry as ITransform3D;

            IPoint originPoint = GeometryUtilities.ConstructPoint3D(0, 0, 0);

            transform3D.Scale3D(originPoint, endRadius, endRadius, 2 * endRadius);

            if (degreesOfRotation != 0)
            {
                double angleOfRotationInRadians = GeometryUtilities.GetRadians(degreesOfRotation);

                transform3D.RotateVector3D(axisOfRotationVector3D, angleOfRotationInRadians);
            }

            transform3D.Move3D(endPoint.X - originPoint.X, endPoint.Y - originPoint.Y, endPoint.Z - originPoint.Z);

            GraphicsLayer3DUtilities.AddMultiPatchToGraphicsLayer3D(endGraphicsContainer3D, endGeometry, endColor);
        }

        public static void DrawMultiPatch(IGraphicsContainer3D multiPatchGraphicsContainer3D, IGeometry geometry)
        {
            const int Yellow_R = 255;
            const int Yellow_G = 255;
            const int Yellow_B = 0;

            IColor multiPatchColor = ColorUtilities.GetColor(Yellow_R, Yellow_G, Yellow_B);

            multiPatchGraphicsContainer3D.DeleteAllElements();

            GraphicsLayer3DUtilities.AddMultiPatchToGraphicsLayer3D(multiPatchGraphicsContainer3D, geometry, multiPatchColor);
        }

        public static void DrawOutline(IGraphicsContainer3D outlineGraphicsContainer3D, IGeometry geometry)
        {
            const esriSimple3DLineStyle OutlineStyle = esriSimple3DLineStyle.esriS3DLSTube;
            const double OutlineWidth = 0.1;

            const int Black_R = 0;
            const int Black_G = 0;
            const int Black_B = 0;

            IColor outlineColor = ColorUtilities.GetColor(Black_R, Black_G, Black_B);

            outlineGraphicsContainer3D.DeleteAllElements();

            GraphicsLayer3DUtilities.AddOutlineToGraphicsLayer3D(outlineGraphicsContainer3D, GeometryUtilities.ConstructMultiPatchOutline(geometry), outlineColor, OutlineStyle, OutlineWidth);
        }
    }
}