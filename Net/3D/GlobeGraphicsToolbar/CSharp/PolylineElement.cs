using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.GlobeCore;

namespace GlobeGraphicsToolbar
{
    public class PolylineElement
    {
        private IElement _element;
        private IGlobeGraphicsElementProperties _elementProperties;

        public PolylineElement(IGeometry geometry, double width, esriSimpleLineStyle simpleLineStyle)
        {
            _element = GetElement(geometry, width, simpleLineStyle);
            _elementProperties = GetElementProperties();
        }

        private IElement GetElement(IGeometry geometry, double width, esriSimpleLineStyle simpleLineStyle)
        {
            IElement element;

            ILineElement lineElement = new LineElementClass();
            element = lineElement as IElement;

            ISimpleLineSymbol simpleLineSymbol = new SimpleLineSymbolClass();
            simpleLineSymbol.Style = simpleLineStyle;
            simpleLineSymbol.Color = ColorSelection.GetColor();
            simpleLineSymbol.Width = width;

            element.Geometry = geometry;

            ILineSymbol lineSymbol = simpleLineSymbol as ILineSymbol;

            lineElement.Symbol = lineSymbol;

            return element;
        }

        private IGlobeGraphicsElementProperties GetElementProperties()
        {
            IGlobeGraphicsElementProperties elementProperties = new GlobeGraphicsElementPropertiesClass();
            elementProperties.Rasterize = true;

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