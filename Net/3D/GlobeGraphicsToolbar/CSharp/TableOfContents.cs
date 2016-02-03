using ESRI.ArcGIS.GlobeCore;
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.Carto;

namespace GlobeGraphicsToolbar
{
    public class TableOfContents
    {
        IScene _scene;

        public TableOfContents(IGlobe globe)
        {
            _scene = GetScene(globe);
        }

        private IScene GetScene(IGlobe globe)
        {
            IScene scene;

            scene = globe as IScene;

            return scene;
        }

        public bool LayerExists(string name)
        {
            bool exists = false;

            for (int i = 0; i < _scene.LayerCount; i++)
            {
                ILayer layer = _scene.get_Layer(i);

                if (layer.Name == name)
                {
                    exists = true;
                    break;
                }
            }

            return exists;
        }

        public void ConstructLayer(string name)
        {
            IGlobeGraphicsLayer globeGraphicsLayer = new GlobeGraphicsLayerClass();

            ILayer layer = globeGraphicsLayer as ILayer;

            layer.Name = name;

            _scene.AddLayer(layer, true);
        }

        public ILayer this[string name]
        {
            get
            {
                return GetLayer(name);
            }
        }

        private ILayer GetLayer(string name)
        {
            ILayer layer = null;

            for (int i = 0; i < _scene.LayerCount; i++)
            {
                ILayer currentLayer = _scene.get_Layer(i);

                if (currentLayer.Name == name)
                {
                    layer = currentLayer;
                    break;
                }
            }

            return layer;
        }
     }
}