using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.GlobeCore;

namespace GlobeGraphicsToolbar
{
    public class StyleElement
    {
        private IElement _element;
        private IGlobeGraphicsElementProperties _elementProperties;

        public StyleElement(IGeometry geometry, double size, IStyleGalleryItem styleGalleryItem)
        {
            _element = GetElement(geometry, size, styleGalleryItem);
            _elementProperties = GetElementProperties();
        }

        private IElement GetElement(IGeometry geometry, double size, IStyleGalleryItem styleGalleryItem)
        {
            IElement element;

            IMarkerElement markerElement = new MarkerElementClass();
            element = markerElement as IElement;

            IMarkerSymbol markerSymbol = styleGalleryItem.Item as IMarkerSymbol;
            markerSymbol.Size = size;

            element.Geometry = geometry;

            markerElement.Symbol = markerSymbol;

            return element;
        }

        private IGlobeGraphicsElementProperties GetElementProperties()
        {
            IGlobeGraphicsElementProperties elementProperties = new GlobeGraphicsElementPropertiesClass();
            elementProperties.DrapeElement = true;
            elementProperties.Illuminate = true;

            return elementProperties;
        }

        public IElement Element
        {
            get
            {
                return _element;
            }
        }

        public IGlobeGraphicsElementProperties ElementProperties
        {
            get
            {
                return _elementProperties;
            }
        }    
    }
}