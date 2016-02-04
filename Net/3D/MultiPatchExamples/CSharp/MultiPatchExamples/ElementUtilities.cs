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