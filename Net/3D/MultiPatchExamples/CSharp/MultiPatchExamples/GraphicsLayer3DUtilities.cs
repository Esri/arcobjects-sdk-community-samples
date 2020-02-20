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
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Analyst3D;

namespace MultiPatchExamples
{
    public static class GraphicsLayer3DUtilities
    {
        public static IGraphicsContainer3D ConstructGraphicsLayer3D(string name)
        {
            IGraphicsContainer3D graphicsContainer3D = new GraphicsLayer3DClass();
            
            ILayer layer = graphicsContainer3D as ILayer;
            layer.Name = name;

            return graphicsContainer3D;
        }

        public static void DisableLighting(IGraphicsContainer3D graphicsContainer3D)
        {
            I3DProperties properties3D = new Basic3DPropertiesClass();
            properties3D.Illuminate = false;

            ILayerExtensions layerExtensions = graphicsContainer3D as ILayerExtensions;
            layerExtensions.AddExtension(properties3D);

            properties3D.Apply3DProperties(graphicsContainer3D);
        }

        public static void AddAxisToGraphicsLayer3D(IGraphicsContainer3D graphicsContainer3D, IGeometry geometry, IColor color, esriSimple3DLineStyle style, double width)
        {
            graphicsContainer3D.AddElement(ElementUtilities.ConstructPolylineElement(geometry, color, style, width));
        }

        public static void AddOutlineToGraphicsLayer3D(IGraphicsContainer3D graphicsContainer3D, IGeometryCollection geometryCollection, IColor color, esriSimple3DLineStyle style, double width)
        {
            for (int i = 0; i < geometryCollection.GeometryCount; i++)
            {
                IGeometry geometry = geometryCollection.get_Geometry(i);

                graphicsContainer3D.AddElement(ElementUtilities.ConstructPolylineElement(geometry, color, style, width));
            }
        }

        public static void AddMultiPatchToGraphicsLayer3D(IGraphicsContainer3D graphicsContainer3D, IGeometry geometry, IColor color)
        {
            graphicsContainer3D.AddElement(ElementUtilities.ConstructMultiPatchElement(geometry, color));
        }
   }
}