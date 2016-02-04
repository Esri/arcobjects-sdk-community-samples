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