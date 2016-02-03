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