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
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.GlobeCore;

namespace GlobeGraphicsToolbar
{
    public class PolygonElement
    {
        private IElement _element;
        private IGlobeGraphicsElementProperties _elementProperties;

        public PolygonElement(IGeometry geometry, esriSimpleFillStyle simpleFillStyle)
        {
            _element = GetElement(geometry, simpleFillStyle);
            _elementProperties = GetElementProperties();
        }

        private IElement GetElement(IGeometry geometry, esriSimpleFillStyle simpleFillStyle)
        {
            IElement element;

            IPolygonElement polygonElement = new PolygonElementClass();
            element = polygonElement as IElement;

            IFillShapeElement fillShapeElement = polygonElement as IFillShapeElement;

            ISimpleFillSymbol simpleFillSymbol = new SimpleFillSymbolClass();
            simpleFillSymbol.Style = simpleFillStyle;
            simpleFillSymbol.Color = ColorSelection.GetColor();

            element.Geometry = geometry;

            fillShapeElement.Symbol = simpleFillSymbol;

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