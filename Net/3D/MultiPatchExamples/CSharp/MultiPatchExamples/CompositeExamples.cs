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
    public static class CompositeExamples
    {
        private static object _missing = Type.Missing;

        public static IGeometry GetExample1()
        {
            //Composite: Multiple, Disjoint Geometries Contained Within A Single MultiPatch

            IGeometryCollection multiPatchGeometryCollection = new MultiPatchClass();

            IMultiPatch multiPatch = multiPatchGeometryCollection as IMultiPatch;

            //Vector3D Example 2

            IGeometry vector3DExample2Geometry = Vector3DExamples.GetExample2();

            ITransform3D vector3DExample2Transform3D = vector3DExample2Geometry as ITransform3D;
            vector3DExample2Transform3D.Move3D(5, 5, 0);

            IGeometryCollection vector3DExample2GeometryCollection = vector3DExample2Geometry as IGeometryCollection;

            for (int i = 0; i < vector3DExample2GeometryCollection.GeometryCount; i++)
            {
                multiPatchGeometryCollection.AddGeometry(vector3DExample2GeometryCollection.get_Geometry(i), ref _missing, ref _missing);
            }

            //Vector3D Example 3

            IGeometry vector3DExample3Geometry = Vector3DExamples.GetExample3();

            ITransform3D vector3DExample3Transform3D = vector3DExample3Geometry as ITransform3D;
            vector3DExample3Transform3D.Move3D(5, -5, 0);

            IGeometryCollection vector3DExample3GeometryCollection = vector3DExample3Geometry as IGeometryCollection;

            for (int i = 0; i < vector3DExample3GeometryCollection.GeometryCount; i++)
            {
                multiPatchGeometryCollection.AddGeometry(vector3DExample3GeometryCollection.get_Geometry(i), ref _missing, ref _missing);
            }

            //Vector3D Example 4

            IGeometry vector3DExample4Geometry = Vector3DExamples.GetExample4();

            ITransform3D vector3DExample4Transform3D = vector3DExample4Geometry as ITransform3D;
            vector3DExample4Transform3D.Move3D(-5, -5, 0);

            IGeometryCollection vector3DExample4GeometryCollection = vector3DExample4Geometry as IGeometryCollection;

            for (int i = 0; i < vector3DExample4GeometryCollection.GeometryCount; i++)
            {
                multiPatchGeometryCollection.AddGeometry(vector3DExample4GeometryCollection.get_Geometry(i), ref _missing, ref _missing);
            }

            //Vector3D Example 5

            IGeometry vector3DExample5Geometry = Vector3DExamples.GetExample5();

            ITransform3D vector3DExample5Transform3D = vector3DExample5Geometry as ITransform3D;
            vector3DExample5Transform3D.Move3D(-5, 5, 0);

            IGeometryCollection vector3DExample5GeometryCollection = vector3DExample5Geometry as IGeometryCollection;

            for (int i = 0; i < vector3DExample5GeometryCollection.GeometryCount; i++)
            {
                multiPatchGeometryCollection.AddGeometry(vector3DExample5GeometryCollection.get_Geometry(i), ref _missing, ref _missing);
            }

            return multiPatchGeometryCollection as IGeometry;
        }

        public static IGeometry GetExample2()
        {
            //Composite: Cutaway Of Building With Multiple Floors Composed Of 1 TriangleStrip And 5 Ring Parts

            IGeometryCollection multiPatchGeometryCollection = new MultiPatchClass();

            IMultiPatch multiPatch = multiPatchGeometryCollection as IMultiPatch;

            //Walls

            IPointCollection wallsPointCollection = new TriangleStripClass();

            //Start

            wallsPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(3, -3, 0), ref _missing, ref _missing);
            wallsPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(3, -3, 16), ref _missing, ref _missing);

            //Right Wall

            wallsPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(3, 3, 0), ref _missing, ref _missing);
            wallsPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(3, 3, 16), ref _missing, ref _missing);

            //Back Wall

            wallsPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-3, 3, 0), ref _missing, ref _missing);
            wallsPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-3, 3, 16), ref _missing, ref _missing);

            //Left Wall

            wallsPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-3, -3, 0), ref _missing, ref _missing);
            wallsPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-3, -3, 16), ref _missing, ref _missing);

            multiPatchGeometryCollection.AddGeometry(wallsPointCollection as IGeometry, ref _missing, ref _missing);

            //Floors

            //Base

            IPointCollection basePointCollection = new RingClass();
            basePointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(3, 3, 0), ref _missing, ref _missing);
            basePointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(3, -3, 0), ref _missing, ref _missing);
            basePointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-3, -3, 0), ref _missing, ref _missing);
            basePointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-3, 3, 0), ref _missing, ref _missing);

            IRing baseRing = basePointCollection as IRing;
            baseRing.Close();

            multiPatchGeometryCollection.AddGeometry(baseRing as IGeometry, ref _missing, ref _missing);

            //First Floor

            IPointCollection firstFloorPointCollection = new RingClass();
            firstFloorPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(3, 3, 4), ref _missing, ref _missing);
            firstFloorPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(3, -3, 4), ref _missing, ref _missing);
            firstFloorPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-3, -3, 4), ref _missing, ref _missing);
            firstFloorPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-3, 3, 4), ref _missing, ref _missing);

            IRing firstFloorRing = firstFloorPointCollection as IRing;
            firstFloorRing.Close();

            multiPatchGeometryCollection.AddGeometry(firstFloorRing as IGeometry, ref _missing, ref _missing);

            //Second Floor

            IPointCollection secondFloorPointCollection = new RingClass();
            secondFloorPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(3, 3, 8), ref _missing, ref _missing);
            secondFloorPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(3, -3, 8), ref _missing, ref _missing);
            secondFloorPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-3, -3, 8), ref _missing, ref _missing);
            secondFloorPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-3, 3, 8), ref _missing, ref _missing);

            IRing secondFloorRing = secondFloorPointCollection as IRing;
            secondFloorRing.Close();

            multiPatchGeometryCollection.AddGeometry(secondFloorRing as IGeometry, ref _missing, ref _missing);

            //Third Floor

            IPointCollection thirdFloorPointCollection = new RingClass();
            thirdFloorPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(3, 3, 12), ref _missing, ref _missing);
            thirdFloorPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(3, -3, 12), ref _missing, ref _missing);
            thirdFloorPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-3, -3, 12), ref _missing, ref _missing);
            thirdFloorPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-3, 3, 12), ref _missing, ref _missing);

            IRing thirdFloorRing = thirdFloorPointCollection as IRing;
            thirdFloorRing.Close();

            multiPatchGeometryCollection.AddGeometry(thirdFloorRing as IGeometry, ref _missing, ref _missing);

            //Roof

            IPointCollection roofPointCollection = new RingClass();
            roofPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(3, 3, 16), ref _missing, ref _missing);
            roofPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(3, -3, 16), ref _missing, ref _missing);
            roofPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-3, -3, 16), ref _missing, ref _missing);
            roofPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-3, 3, 16), ref _missing, ref _missing);

            IRing roofRing = roofPointCollection as IRing;
            roofRing.Close();

            multiPatchGeometryCollection.AddGeometry(roofRing as IGeometry, ref _missing, ref _missing);

            return multiPatchGeometryCollection as IGeometry;
        }

        public static IGeometry GetExample3()
        {
            //Composite: House Composed Of 7 Ring, 1 TriangleStrip, And 1 Triangles Parts

            IGeometryCollection multiPatchGeometryCollection = new MultiPatchClass();

            IMultiPatch multiPatch = multiPatchGeometryCollection as IMultiPatch;

            //Base (Exterior Ring)

            IPointCollection basePointCollection = new RingClass();
            basePointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(5, 4, 0), ref _missing, ref _missing);
            basePointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-5, 4, 0), ref _missing, ref _missing);
            basePointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-5, -4, 0), ref _missing, ref _missing);
            basePointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(5, -4, 0), ref _missing, ref _missing);
            basePointCollection.AddPoint(basePointCollection.get_Point(0), ref _missing, ref _missing);

            multiPatchGeometryCollection.AddGeometry(basePointCollection as IGeometry, ref _missing, ref _missing);

            multiPatch.PutRingType(basePointCollection as IRing, esriMultiPatchRingType.esriMultiPatchOuterRing);

            //Front With Cutaway For Door (Exterior Ring)

            IPointCollection frontPointCollection = new RingClass();
            frontPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(5, 4, 6), ref _missing, ref _missing);
            frontPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(5, 4, 0), ref _missing, ref _missing);
            frontPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(5, 1, 0), ref _missing, ref _missing);
            frontPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(5, 1, 4), ref _missing, ref _missing);
            frontPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(5, -1, 4), ref _missing, ref _missing);
            frontPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(5, -1, 0), ref _missing, ref _missing);
            frontPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(5, -4, 0), ref _missing, ref _missing);
            frontPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(5, -4, 6), ref _missing, ref _missing);
            frontPointCollection.AddPoint(frontPointCollection.get_Point(0), ref _missing, ref _missing);

            multiPatchGeometryCollection.AddGeometry(frontPointCollection as IGeometry, ref _missing, ref _missing);

            multiPatch.PutRingType(frontPointCollection as IRing, esriMultiPatchRingType.esriMultiPatchOuterRing);

            //Back (Exterior Ring)

            IPointCollection backPointCollection = new RingClass();
            backPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-5, 4, 6), ref _missing, ref _missing);
            backPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-5, -4, 6), ref _missing, ref _missing);
            backPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-5, -4, 0), ref _missing, ref _missing);
            backPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-5, 4, 0), ref _missing, ref _missing);
            backPointCollection.AddPoint(backPointCollection.get_Point(0), ref _missing, ref _missing);

            multiPatchGeometryCollection.AddGeometry(backPointCollection as IGeometry, ref _missing, ref _missing);

            multiPatch.PutRingType(backPointCollection as IRing, esriMultiPatchRingType.esriMultiPatchOuterRing);

            //Right Side (Ring Group)

            //Exterior Ring

            IPointCollection rightSideExteriorPointCollection = new RingClass();
            rightSideExteriorPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(5, 4, 6), ref _missing, ref _missing);
            rightSideExteriorPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-5, 4, 6), ref _missing, ref _missing);
            rightSideExteriorPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-5, 4, 0), ref _missing, ref _missing);
            rightSideExteriorPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(5, 4, 0), ref _missing, ref _missing);
            rightSideExteriorPointCollection.AddPoint(rightSideExteriorPointCollection.get_Point(0), ref _missing, ref _missing);

            multiPatchGeometryCollection.AddGeometry(rightSideExteriorPointCollection as IGeometry, ref _missing, ref _missing);

            multiPatch.PutRingType(rightSideExteriorPointCollection as IRing, esriMultiPatchRingType.esriMultiPatchOuterRing);

            //Interior Ring

            IPointCollection rightSideInteriorPointCollection = new RingClass();
            rightSideInteriorPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(1, 4, 4), ref _missing, ref _missing);
            rightSideInteriorPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(1, 4, 2), ref _missing, ref _missing);
            rightSideInteriorPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-1, 4, 2), ref _missing, ref _missing);
            rightSideInteriorPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-1, 4, 4), ref _missing, ref _missing);
            rightSideInteriorPointCollection.AddPoint(rightSideInteriorPointCollection.get_Point(0), ref _missing, ref _missing);

            multiPatchGeometryCollection.AddGeometry(rightSideInteriorPointCollection as IGeometry, ref _missing, ref _missing);

            multiPatch.PutRingType(rightSideInteriorPointCollection as IRing, esriMultiPatchRingType.esriMultiPatchInnerRing);

            //Left Side (Ring Group)

            //Exterior Ring

            IPointCollection leftSideExteriorPointCollection = new RingClass();
            leftSideExteriorPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(5, -4, 6), ref _missing, ref _missing);
            leftSideExteriorPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(5, -4, 0), ref _missing, ref _missing);
            leftSideExteriorPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-5, -4, 0), ref _missing, ref _missing);
            leftSideExteriorPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-5, -4, 6), ref _missing, ref _missing);
            leftSideExteriorPointCollection.AddPoint(leftSideExteriorPointCollection.get_Point(0), ref _missing, ref _missing);

            multiPatchGeometryCollection.AddGeometry(leftSideExteriorPointCollection as IGeometry, ref _missing, ref _missing);

            multiPatch.PutRingType(leftSideExteriorPointCollection as IRing, esriMultiPatchRingType.esriMultiPatchOuterRing);

            //Interior Ring

            IPointCollection leftSideInteriorPointCollection = new RingClass();
            leftSideInteriorPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(1, -4, 4), ref _missing, ref _missing);
            leftSideInteriorPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-1, -4, 4), ref _missing, ref _missing);
            leftSideInteriorPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-1, -4, 2), ref _missing, ref _missing);
            leftSideInteriorPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(1, -4, 2), ref _missing, ref _missing);
            leftSideInteriorPointCollection.AddPoint(leftSideInteriorPointCollection.get_Point(0), ref _missing, ref _missing);

            multiPatchGeometryCollection.AddGeometry(leftSideInteriorPointCollection as IGeometry, ref _missing, ref _missing);

            multiPatch.PutRingType(leftSideInteriorPointCollection as IRing, esriMultiPatchRingType.esriMultiPatchInnerRing);

            //Roof

            IPointCollection roofPointCollection = new TriangleStripClass();
            roofPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-5, 4, 6), ref _missing, ref _missing);
            roofPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(5, 4, 6), ref _missing, ref _missing);
            roofPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-5, 0, 9), ref _missing, ref _missing);
            roofPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(5, 0, 9), ref _missing, ref _missing);
            roofPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-5, -4, 6), ref _missing, ref _missing);
            roofPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(5, -4, 6), ref _missing, ref _missing);

            multiPatchGeometryCollection.AddGeometry(roofPointCollection as IGeometry, ref _missing, ref _missing);

            //Triangular Area Between Roof And Front/Back

            IPointCollection triangularAreaPointCollection = new TrianglesClass();

            //Area Between Roof And Front

            triangularAreaPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(5, 0, 9), ref _missing, ref _missing);
            triangularAreaPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(5, 4, 6), ref _missing, ref _missing);
            triangularAreaPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(5, -4, 6), ref _missing, ref _missing);

            //Area Between Roof And Back

            triangularAreaPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-5, 0, 9), ref _missing, ref _missing);
            triangularAreaPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-5, -4, 6), ref _missing, ref _missing);
            triangularAreaPointCollection.AddPoint(GeometryUtilities.ConstructPoint3D(-5, 4, 6), ref _missing, ref _missing);

            multiPatchGeometryCollection.AddGeometry(triangularAreaPointCollection as IGeometry, ref _missing, ref _missing);

            return multiPatchGeometryCollection as IGeometry;
        }

        public static IGeometry GetExample4()
        {
            const double CircleDegrees = 360.0;
            const int CircleDivisions = 18;
            const double VectorComponentOffset = 0.0000001;
            const double InnerBuildingRadius = 3.0;
            const double OuterBuildingExteriorRingRadius = 9.0;
            const double OuterBuildingInteriorRingRadius = 6.0;
            const double BaseZ = 0.0;
            const double InnerBuildingZ = 16.0;
            const double OuterBuildingZ = 6.0;

            //Composite: Tall Building Protruding Through Outer Ring-Shaped Building

            IMultiPatch multiPatch = new MultiPatchClass();

            IGeometryCollection multiPatchGeometryCollection = multiPatch as IGeometryCollection;

            IPoint originPoint = GeometryUtilities.ConstructPoint3D(0, 0, 0);

            IVector3D upperAxisVector3D = GeometryUtilities.ConstructVector3D(0, 0, 10);

            IVector3D lowerAxisVector3D = GeometryUtilities.ConstructVector3D(0, 0, -10);

            lowerAxisVector3D.XComponent += VectorComponentOffset;

            IVector3D normalVector3D = upperAxisVector3D.CrossProduct(lowerAxisVector3D) as IVector3D;

            double rotationAngleInRadians = GeometryUtilities.GetRadians(CircleDegrees / CircleDivisions);

            //Inner Building

            IGeometry innerBuildingBaseGeometry = new PolygonClass();

            IPointCollection innerBuildingBasePointCollection = innerBuildingBaseGeometry as IPointCollection;

            //Outer Building

            IGeometry outerBuildingBaseGeometry = new PolygonClass();

            IGeometryCollection outerBuildingBaseGeometryCollection = outerBuildingBaseGeometry as IGeometryCollection;

            IPointCollection outerBuildingBaseExteriorRingPointCollection = new RingClass();

            IPointCollection outerBuildingBaseInteriorRingPointCollection = new RingClass();

            for (int i = 0; i < CircleDivisions; i++)
            {
                normalVector3D.Rotate(-1 * rotationAngleInRadians, upperAxisVector3D);

                //Inner Building

                normalVector3D.Magnitude = InnerBuildingRadius;

                IPoint innerBuildingBaseVertexPoint = GeometryUtilities.ConstructPoint2D(originPoint.X + normalVector3D.XComponent,
                                                                                         originPoint.Y + normalVector3D.YComponent);

                innerBuildingBasePointCollection.AddPoint(innerBuildingBaseVertexPoint, ref _missing, ref _missing);

                //Outer Building

                //Exterior Ring

                normalVector3D.Magnitude = OuterBuildingExteriorRingRadius;

                IPoint outerBuildingBaseExteriorRingVertexPoint = GeometryUtilities.ConstructPoint2D(originPoint.X + normalVector3D.XComponent,
                                                                                                     originPoint.Y + normalVector3D.YComponent);

                outerBuildingBaseExteriorRingPointCollection.AddPoint(outerBuildingBaseExteriorRingVertexPoint, ref _missing, ref _missing);

                //Interior Ring

                normalVector3D.Magnitude = OuterBuildingInteriorRingRadius;

                IPoint outerBuildingBaseInteriorRingVertexPoint = GeometryUtilities.ConstructPoint2D(originPoint.X + normalVector3D.XComponent,
                                                                                                     originPoint.Y + normalVector3D.YComponent);

                outerBuildingBaseInteriorRingPointCollection.AddPoint(outerBuildingBaseInteriorRingVertexPoint, ref _missing, ref _missing);
            }

            IPolygon innerBuildingBasePolygon = innerBuildingBaseGeometry as IPolygon;
            innerBuildingBasePolygon.Close();

            IRing outerBuildingBaseExteriorRing = outerBuildingBaseExteriorRingPointCollection as IRing;
            outerBuildingBaseExteriorRing.Close();

            IRing outerBuildingBaseInteriorRing = outerBuildingBaseInteriorRingPointCollection as IRing;
            outerBuildingBaseInteriorRing.Close();
            outerBuildingBaseInteriorRing.ReverseOrientation();

            outerBuildingBaseGeometryCollection.AddGeometry(outerBuildingBaseExteriorRing as IGeometry, ref _missing, ref _missing);
            outerBuildingBaseGeometryCollection.AddGeometry(outerBuildingBaseInteriorRing as IGeometry, ref _missing, ref _missing);

            ITopologicalOperator topologicalOperator = outerBuildingBaseGeometry as ITopologicalOperator;
            topologicalOperator.Simplify();

            IConstructMultiPatch innerBuildingConstructMultiPatch = new MultiPatchClass();
            innerBuildingConstructMultiPatch.ConstructExtrudeFromTo(BaseZ, InnerBuildingZ, innerBuildingBaseGeometry);

            IGeometryCollection innerBuildingMultiPatchGeometryCollection = innerBuildingConstructMultiPatch as IGeometryCollection;

            for (int i = 0; i < innerBuildingMultiPatchGeometryCollection.GeometryCount; i++)
            {
                multiPatchGeometryCollection.AddGeometry(innerBuildingMultiPatchGeometryCollection.get_Geometry(i), ref _missing, ref _missing);
            }

            IConstructMultiPatch outerBuildingConstructMultiPatch = new MultiPatchClass();
            outerBuildingConstructMultiPatch.ConstructExtrudeFromTo(BaseZ, OuterBuildingZ, outerBuildingBaseGeometry);

            IMultiPatch outerBuildingMultiPatch = outerBuildingConstructMultiPatch as IMultiPatch;

            IGeometryCollection outerBuildingMultiPatchGeometryCollection = outerBuildingConstructMultiPatch as IGeometryCollection;

            for (int i = 0; i < outerBuildingMultiPatchGeometryCollection.GeometryCount; i++)
            {
                IGeometry outerBuildingPatchGeometry = outerBuildingMultiPatchGeometryCollection.get_Geometry(i);

                multiPatchGeometryCollection.AddGeometry(outerBuildingPatchGeometry, ref _missing, ref _missing);

                if (outerBuildingPatchGeometry.GeometryType == esriGeometryType.esriGeometryRing)
                {
                    bool isBeginningRing = false;

                    esriMultiPatchRingType multiPatchRingType = outerBuildingMultiPatch.GetRingType(outerBuildingPatchGeometry as IRing, ref isBeginningRing);

                    multiPatch.PutRingType(outerBuildingPatchGeometry as IRing, multiPatchRingType);
                }
            }

            return multiPatchGeometryCollection as IGeometry;            
        }
    }
}