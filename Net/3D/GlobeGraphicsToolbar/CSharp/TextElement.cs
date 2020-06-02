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
using ESRI.ArcGIS.ADF.COMSupport;
using System.Drawing;
using System;


namespace GlobeGraphicsToolbar
{
    public class TextElement
    {
        private IElement _element;
        private IGlobeGraphicsElementProperties _elementProperties;

        public TextElement(IGeometry geometry, string text, float size)
        {
            _element = GetElement(geometry, text, size);
            _elementProperties = GetElementProperties();
        }

        private IElement GetElement(IGeometry geometry, string text, float size)
        {
            IElement element;

            ITextElement textElement = new TextElementClass();
            element = textElement as IElement;

            ITextSymbol textSymbol = new TextSymbolClass();
            textSymbol.Color = ColorSelection.GetColor();
            textSymbol.Size = Convert.ToDouble(size);
            textSymbol.Font = GetIFontDisp(size);
            textSymbol.HorizontalAlignment = GetHorizontalAlignment();
            textSymbol.VerticalAlignment = GetVerticalAlignment();

            element.Geometry = geometry;

            textElement.Symbol = textSymbol;
            textElement.Text = text;

            return element;
        }

        private stdole.IFontDisp GetIFontDisp(float size)
        {
            const string FontFamilyName = "Arial";
            const FontStyle FontStyle = FontStyle.Bold;

            Font font = new Font(FontFamilyName, size, FontStyle);

            return OLE.GetIFontDispFromFont(font) as stdole.IFontDisp;
        }

        private esriTextHorizontalAlignment GetHorizontalAlignment()
        {
            const esriTextHorizontalAlignment HorizontalAlignment = esriTextHorizontalAlignment.esriTHACenter;

            return HorizontalAlignment;
        }

        private esriTextVerticalAlignment GetVerticalAlignment()
        {
            const esriTextVerticalAlignment VerticalAlignment = esriTextVerticalAlignment.esriTVABaseline;

            return VerticalAlignment;
        }

        private IGlobeGraphicsElementProperties GetElementProperties()
        {
            IGlobeGraphicsElementProperties elementProperties = new GlobeGraphicsElementPropertiesClass();
            elementProperties.FixedScreenSize = true;
            elementProperties.DrapeElement = true;

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