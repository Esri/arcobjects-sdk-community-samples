using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;

namespace GlobeGraphicsToolbar
{
    public class StyleGalleryCommand : ESRI.ArcGIS.Desktop.AddIns.Button
    {

        private StyleGallery _styleGallery = null;

        public StyleGalleryCommand()
        {
        }

        protected override void OnClick()
        {
            _styleGallery = new StyleGallery();

            if (_styleGallery.IsStyleSelected() == true)
            {
                StyleGallerySelection.SetStyleGalleryItem(_styleGallery.StyleGalleryItem);
            }       
        }

        protected override void OnUpdate()
        {
        }
    }
}
