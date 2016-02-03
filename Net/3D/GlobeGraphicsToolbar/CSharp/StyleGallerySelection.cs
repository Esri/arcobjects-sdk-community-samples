using ESRI.ArcGIS.Display;

namespace GlobeGraphicsToolbar
{
    public class StyleGallerySelection
    {
        private static IStyleGalleryItem _styleGalleryItem = null;

        public static void SetStyleGalleryItem(IStyleGalleryItem styleGalleryItem)
        {
            _styleGalleryItem = styleGalleryItem;
        }

        public static IStyleGalleryItem GetStyleGalleryItem()
        {
            return _styleGalleryItem;
        }
    }
}