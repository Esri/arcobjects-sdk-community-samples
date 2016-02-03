using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using System.Windows.Forms;


namespace GlobeGraphicsToolbar
{
    public class ColorCommand : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        private ColorPalette _colorPalette = null;

        public ColorCommand()
        {
            _colorPalette = new ColorPalette();

            ColorSelection.SetColor(_colorPalette.Red, _colorPalette.Green, _colorPalette.Blue);
        }

        protected override void OnClick()
        {
            if (_colorPalette.IsColorSelected() == true)
            {
                ColorSelection.SetColor(_colorPalette.Red, _colorPalette.Green, _colorPalette.Blue);
            }      
        }

        protected override void OnUpdate()
        {
            Enabled = ArcGlobe.Application != null;
        }
    }
}
