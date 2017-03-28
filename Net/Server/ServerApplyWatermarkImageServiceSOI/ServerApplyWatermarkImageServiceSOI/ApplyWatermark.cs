// Copyright 2015 ESRI
// 
// All rights reserved under the copyright laws of the United States
// and applicable international laws, treaties, and conventions.
// 
// You may freely redistribute and use this sample code, with or
// without modification, provided you include the original copyright
// notice and use restrictions.
// 
// See the use restrictions at <your ArcGIS Developer Kit install location>/userestrictions.txt.
// 


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ApplyWatermark
{

    public class ApplyWatermark
    {
        public Color Color { get; set; }

        public Font Font { get; set; }

        public string WatermarkText { get; set; }

        public ContentAlignment TextAlignment { get; set; }

        /// <summary>
        /// Creates an instance of Watermarker class.
        /// </summary>
        public ApplyWatermark ()
        {
            Color = Color.Aquamarine;
            Font = new Font(FontFamily.GenericSansSerif, 16);
            WatermarkText = "(c) ESRI Inc.";
            TextAlignment = ContentAlignment.BottomLeft;
        }

        public Image Mark( Image image, string waterMarkText )
        {
            WatermarkText = waterMarkText;

            Bitmap originalBmp = (Bitmap)image;

            // avoid "A Graphics object cannot be created from an image that has an indexed pixel format." exception
            Bitmap tempBitmap = new Bitmap(originalBmp.Width, originalBmp.Height);
            // From this bitmap, the graphics can be obtained, because it has the right PixelFormat
            Graphics g = Graphics.FromImage(tempBitmap);

            using (Graphics graphics = Graphics.FromImage(tempBitmap))
            {
                // Draw the original bitmap onto the graphics of the new bitmap
                g.DrawImage(originalBmp, 0, 0);
                var size =
                    graphics.MeasureString(WatermarkText, Font);
                var brush =
                    new SolidBrush(Color.FromArgb(255, Color));
                graphics.DrawString
                    (WatermarkText, Font, brush,
                    GetTextPosition(image, size));
            }

            return tempBitmap as Image;
        }

        public Image Mark ( Image image )
        {
            return Mark(image, WatermarkText);
        }

        private PointF GetTextPosition ( Image image, SizeF size )
        {
            PointF point = default(PointF);
            switch (TextAlignment)
            {
                case ContentAlignment.BottomCenter:
                    point = new PointF((image.Width - size.Width) / 2,
                        (image.Height - size.Height));
                    break;
                case ContentAlignment.BottomLeft:
                    point = new PointF(0, (image.Height - size.Height));
                    break;
                case ContentAlignment.BottomRight:
                    point = new PointF((image.Width - size.Width),
                        (image.Height - size.Height));
                    break;
                case ContentAlignment.MiddleCenter:
                    point = new PointF((image.Width - size.Width) / 2,
                        (image.Height - size.Height) / 2);
                    break;
                case ContentAlignment.MiddleLeft:
                    point = new PointF(0, (image.Height - size.Height) / 2);
                    break;
                case ContentAlignment.MiddleRight:
                    point = new PointF((image.Width - size.Width),
                        (image.Height - size.Height) / 2);
                    break;
                case ContentAlignment.TopCenter:
                    point = new PointF((image.Width - size.Width) / 2, 0);
                    break;
                case ContentAlignment.TopLeft:
                    point = new PointF(0, 0);
                    break;
                case ContentAlignment.TopRight:
                    point = new PointF((image.Width - size.Width), 0);
                    break;
            }
            return point;
        }
    }
}
