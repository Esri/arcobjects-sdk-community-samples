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
using ESRI.ArcGIS.GlobeCore;

namespace GlobeGraphicsToolbar
{
    public class PointElement
    {
        private IElement _element;
        private IGlobeGraphicsElementProperties _elementProperties;

        public PointElement(IGeometry geometry, double size, esriSimpleMarkerStyle simpleMarkerStyle)
        {
            _element = GetElement(geometry, size, simpleMarkerStyle);
            _elementProperties = GetElementProperties();
        }

        public PointElement(IGeometry geometry, double size, esriSimple3DMarkerStyle simple3DMarkerStyle)
        {
            _element = GetElement(geometry, size, simple3DMarkerStyle);
            _elementProperties = GetElementProperties();
        }

        private IElement GetElement(IGeometry geometry, double size, esriSimpleMarkerStyle simpleMarkerStyle)
        {
            IElement element;

            IMarkerElement markerElement = new MarkerElementClass();
            element = markerElement as IElement;

            ISimpleMarkerSymbol simpleMarkerSymbol = new SimpleMarkerSymbolClass();
            simpleMarkerSymbol.Style = simpleMarkerStyle;
            simpleMarkerSymbol.Color = ColorSelection.GetColor();
            simpleMarkerSymbol.Size = size;

            element.Geometry = geometry;

            markerElement.Symbol = simpleMarkerSymbol;

            return element;
        }

        private IElement GetElement(IGeometry geometry, double size, esriSimple3DMarkerStyle simple3DMarkerStyle)
        {
            IElement element;

            IMarkerElement markerElement = new MarkerElementClass();
            element = markerElement as IElement;

            ISimpleMarker3DSymbol simpleMarker3DSymbol = new SimpleMarker3DSymbolClass();
            simpleMarker3DSymbol.Style = simple3DMarkerStyle;
            simpleMarker3DSymbol.ResolutionQuality = GetResolutionQuality();

            IMarkerSymbol markerSymbol = simpleMarker3DSymbol as IMarkerSymbol;
            markerSymbol.Color = ColorSelection.GetColor();
            markerSymbol.Size = size;

            IMarker3DPlacement marker3DPlacement = markerSymbol as IMarker3DPlacement;
            SetMarker3DPlacement(marker3DPlacement, markerSymbol.Size);

            element.Geometry = geometry;

            markerElement.Symbol = markerSymbol;

            return element;
        }

        private double GetResolutionQuality()
        {
            const double HighQuality = 1.0;

            return HighQuality;
        }

        private void SetMarker3DPlacement(IMarker3DPlacement marker3DPlacement, double size)
        {
            const double XOffset = 0;
            const double YOffset = 0;

            marker3DPlacement.XOffset = XOffset;
            marker3DPlacement.YOffset = YOffset;
            marker3DPlacement.ZOffset = size / 2;
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