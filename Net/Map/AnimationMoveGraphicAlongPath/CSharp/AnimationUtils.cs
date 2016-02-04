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
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Animation;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.Geometry;

namespace AnimationDeveloperSamples
{
    public class AnimationUtils
    {
        public static void CreateMapGraphicTrack(ICreateGraphicTrackOptions pOptions, IAGAnimationTracks tracks, IAGAnimationContainer pContainer)
        {
            pOptions.PathGeometry=SimplifyPath2D(pOptions.PathGeometry,pOptions.ReverseOrder,pOptions.SimplificationFactor);
            IAGAnimationType animType = new AnimationTypeMapGraphic();

            //remove tracks with the same name if overwrite is true
            if (pOptions.OverwriteTrack == true)
            {
                IArray trackArray = new ArrayClass();
                trackArray = tracks.get_TracksOfType(animType);
                int count = trackArray.Count;
                for (int i = 0; i < count; i++)
                {
                    IAGAnimationTrack temp = (IAGAnimationTrack)trackArray.get_Element(i);
                    if (temp.Name == pOptions.TrackName)
                        tracks.RemoveTrack(temp);
                }
            }

            //create the new track
            IAGAnimationTrack animTrack = tracks.CreateTrack(animType);
            IAGAnimationTrackKeyframes animTrackKeyframes = (IAGAnimationTrackKeyframes)animTrack;
            animTrackKeyframes.EvenTimeStamps = false;

            animTrack.Name = pOptions.TrackName;

            IGeometry path = pOptions.PathGeometry;
            IPointCollection pointCollection = (IPointCollection)path;

            ICurve curve = (ICurve)path;
            double length = curve.Length;
            double accuLength = 0;

            //loop through all points to create the keyframes
            int pointCount = pointCollection.PointCount;
            if (pointCount <= 1)
                return;
            for (int i = 0; i < pointCount; i++)
            {
                IPoint currentPoint = pointCollection.get_Point(i);
                
                IPoint nextPoint = new PointClass();
                if (i < pointCount-1)
                    nextPoint = pointCollection.get_Point(i + 1);
                
                IPoint lastPoint = new PointClass();
                if (i == 0)
                    lastPoint = currentPoint;
                else
                    lastPoint = pointCollection.get_Point(i-1);

                IAGKeyframe tempKeyframe = animTrackKeyframes.CreateKeyframe(i);
                
                //set keyframe properties
                double x;
                double y;
                currentPoint.QueryCoords(out x, out y);
                tempKeyframe.set_PropertyValue(0, currentPoint);
                tempKeyframe.Name = "Map Graphic keyframe " + i.ToString();
                
                //set keyframe timestamp
                accuLength += CalculateDistance(lastPoint, currentPoint);
                double timeStamp = accuLength / length;
                tempKeyframe.TimeStamp = timeStamp;

                double x1;
                double y1;
                double angle;
                if (i < pointCount - 1)
                {
                    nextPoint.QueryCoords(out x1, out y1);
                    if ((y1 - y) > 0)
                        angle = Math.Acos((x1 - x) / Math.Sqrt((x1 - x) * (x1 - x) + (y1 - y) * (y1 - y)));
                    else
                    {
                        angle = 6.2832 - Math.Acos((x1 - x) / Math.Sqrt((x1 - x) * (x1 - x) + (y1 - y) * (y1 - y)));
                    }
                    tempKeyframe.set_PropertyValue(1, angle);
                }
                else
                { 
                    IAGKeyframe lastKeyframe = animTrackKeyframes.get_Keyframe(i-1);
                    tempKeyframe.set_PropertyValue(1, lastKeyframe.get_PropertyValue(1));
                }
            }

            //attach the point element
            if(pOptions.PointElement != null)
                animTrack.AttachObject(pOptions.PointElement);

            //attach the track extension, which contains a line element for trace
            IMapGraphicTrackExtension graphicTrackExtension = new MapGraphicTrackExtension();
            graphicTrackExtension.ShowTrace = pOptions.AnimatePath;
            IAGAnimationTrackExtensions trackExtensions = (IAGAnimationTrackExtensions)animTrack;
            trackExtensions.AddExtension(graphicTrackExtension);            
        }

        private static double CalculateDistance(IPoint FromPoint, IPoint ToPoint)
        {
            double distance;
            distance = Math.Sqrt((ToPoint.X - FromPoint.X) * (ToPoint.X - FromPoint.X) + (ToPoint.Y - FromPoint.Y) * (ToPoint.Y - FromPoint.Y));
            return distance;
        }

        private static IGeometry SimplifyPath2D(IGeometry path, bool bReverse, double simpFactor)
        {
            IGeometry oldPath = path;
            IPointCollection oldPointCollection = oldPath as IPointCollection;
            IPolyline newPath = new PolylineClass();
            IPointCollection newPointCollection = newPath as IPointCollection;
            ISpatialReference sr = oldPath.SpatialReference;

            int pointCount;
            pointCount = oldPointCollection.PointCount;
            double[] lastCoord = new double[2];

            IPoint beginningPoint = new PointClass();
            oldPointCollection.QueryPoint(bReverse ? (pointCount - 1) : 0, beginningPoint);
            beginningPoint.QueryCoords(out lastCoord[0], out lastCoord[1]);

            bool bKeep = true;

            IPolyline oldLine = oldPath as IPolyline;
            double length = oldLine.Length;

            object Missing = Type.Missing;
            newPointCollection.AddPoint(beginningPoint, ref Missing, ref Missing);

            for (int i = 1; i < pointCount - 1; i++) //simplify 2D path
            {
                double[] coord = new double[2];
                IPoint currentPoint = new PointClass();
                oldPointCollection.QueryPoint(bReverse ? (pointCount - i - 1) : i, currentPoint);
                currentPoint.QueryCoords(out coord[0], out coord[1]);

                double[] d = new double[2];
                d[0] = coord[0] - lastCoord[0];
                d[1] = coord[1] - lastCoord[1];

                double distance;
                distance = Math.Sqrt(d[0] * d[0] + d[1] * d[1]);

                if (distance < (0.25 * simpFactor * length))
                {
                    bKeep = false;
                }
                else
                {
                    bKeep = true;
                }

                if (bKeep)
                {
                    newPointCollection.AddPoint(currentPoint, ref Missing, ref Missing);
                    lastCoord[0] = coord[0];
                    lastCoord[1] = coord[1];
                }
            }

            IPoint finalPoint = new PointClass();
            oldPointCollection.QueryPoint(bReverse ? 0 : (pointCount - 1), finalPoint);
            newPointCollection.AddPoint(finalPoint, ref Missing, ref Missing);

            newPath.SpatialReference = sr;
            return (IGeometry)newPath;
        }
    }
}
