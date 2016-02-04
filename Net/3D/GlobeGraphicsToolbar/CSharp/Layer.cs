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
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.GlobeCore;

namespace GlobeGraphicsToolbar
{
    public class Layer
    {
        private ILayer _layer;

        public Layer(ILayer layer)
        {
            _layer = layer;
        }

        public void AddElement(IElement element, IGlobeGraphicsElementProperties elementProperties)
        {
            int elementIndex;

            IGlobeGraphicsLayer globeGraphicsLayer = _layer as IGlobeGraphicsLayer;
            globeGraphicsLayer.AddElement(element, elementProperties, out elementIndex);
        }

        public void RemoveElement(int index)
        {
            IGraphicsContainer3D graphicsContainer3D = _layer as IGraphicsContainer3D;
            graphicsContainer3D.DeleteElement(this[index]);
        }

        public IElement this[int i]
        {
            get
            {
                IGraphicsContainer3D graphicsContainer3D = _layer as IGraphicsContainer3D;
                return graphicsContainer3D.get_Element(i);
            }
        }

        public int ElementCount
        {
            get
            {
                IGraphicsContainer3D graphicsContainer3D = _layer as IGraphicsContainer3D;
                return graphicsContainer3D.ElementCount;
            }
        }
    }
}