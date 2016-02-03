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