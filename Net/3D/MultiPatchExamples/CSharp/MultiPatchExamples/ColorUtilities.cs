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
using ESRI.ArcGIS.Display;

namespace MultiPatchExamples
{
    public enum TransparencyType
    {
        Transparent = 0,
        Opaque = 255
    }

    public static class ColorUtilities
    {
        private static TransparencyType _transparency = TransparencyType.Opaque;

        public static IColor GetColor(int red, int green, int blue)
        {
            IRgbColor rgbColor = new RgbColorClass();
            rgbColor.Red = red;
            rgbColor.Green = green;
            rgbColor.Blue = blue;

            IColor color = rgbColor as IColor;
            color.Transparency = (byte)_transparency;

            return color;
        }
    }
}