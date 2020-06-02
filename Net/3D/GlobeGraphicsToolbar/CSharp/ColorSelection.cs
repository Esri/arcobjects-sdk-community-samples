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
using ESRI.ArcGIS.Display;

namespace GlobeGraphicsToolbar
{
    public class ColorSelection
    {
        private static IColor _color = null;

        public static void SetColor(int red, int green, int blue)
        {
            IRgbColor rgbColor = new RgbColorClass();
            rgbColor.Red = red;
            rgbColor.Green = green;
            rgbColor.Blue = blue;

            _color = rgbColor as IColor;
        }

        public static IColor GetColor()
        {
            if (_color == null)
            {
                IRgbColor rgbColor = new RgbColorClass();
                rgbColor.Red = 255;
                rgbColor.Green = 0;
                rgbColor.Blue = 0;

                _color = rgbColor as IColor;
            }
            return _color;
        }
    }
}