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