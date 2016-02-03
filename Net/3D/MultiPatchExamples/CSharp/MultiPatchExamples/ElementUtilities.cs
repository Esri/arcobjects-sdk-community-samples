using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;

namespace MultiPatchExamples
{
    public static class ElementUtilities
    {
        private const double HighResolution = 1;
        private const esriUnits Units = esriUnits.esriUnknownUnits;

        public static IElement ConstructPolylineElement(IGeometry geometry, IColor color, esriSimple3DLineStyle style, double width)
        {
            ISimpleLine3DSymbol simpleLine3DSymbol = new SimpleLine3DSymbolClass();
            simpleLine3DSymbol.Style = style;
            simpleLine3DSymbol.ResolutionQuality = HighResolution;

            ILineSymbol lineSymbol = simpleLine3DSymbol as ILineSymbol;
            lineSymbol.Color = color;
            lineSymbol.Width = width;

            ILine3DPlacement line3DPlacement = lineSymbol as ILine3DPlacement;
            line3DPlacement.Units = Units;

            ILineElement lineElement = new LineElementClass();
            lineElement.Symbol = lineSymbol;

            IElement element = lineElement as IElement;
            element.Geometry = geometry;

            return element;
        }

        public static IElement ConstructMultiPatchElement(IGeometry geometry, IColor color)
        {
            ISimpleFillSymbol simpleFillSymbol = new SimpleFillSymbolClass();
            simpleFillSymbol.Color = color;

            IElement element = new MultiPatchElementClass();
            element.Geometry = geometry;

            IFillShapeElement fillShapeElement = element as IFillShapeElement;
            fillShapeElement.Symbol = simpleFillSymbol;

            return element;
        }
    }
}