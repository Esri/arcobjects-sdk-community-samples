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
/* IMPORTANT INFORMATION:
 ======================
 This project already has the COM Component Category Registration information embedded. If you build this project it will 
 overwrite ArcMap's default behavior for rendering of 1bit TIFF images. Since this project alters the default behavior it 
 may be desirable to remove this projects functionality from the system to restore the default image rendering capabilities.
 This can be accomplished by:
 1. Open a Visual Studio 2008 Command Prompt and navigate to the location of this projects .dll that gets created (for 
 example: C:\temp\RasterRendererMaker_1bit_TIFF_VBNET\bin\Debug)
 2. Use the esriregasm.exe utility from the Program Files\Common Files\ArcGIS\bin to unregister the DLL.
*/
using System;
using System.Collections.Generic;
using System.Collections;
using System.Runtime.InteropServices;
using System.Linq;
using System.Text;

using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.ADF.CATIDs;


namespace RasterRenderMakerCSharp
{
    [Guid("F3D07C2C-54C7-488f-821D-12D99A36B143")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("RasterRenderMakerCSharp.RasterRenderMaker_1bitTiff_csharp")]
    [ComVisible(true)]
    public class RasterRenderMaker_1bitTiff_csharp : IRasterRendererMaker
    {
        public IRasterRenderer CreateDefaultRasterRenderer(IRaster raster)
        {

            //Get raster dataset
            IRasterBandCollection rasterBandCollection = (IRasterBandCollection)raster;
            IRasterBand rasterBand = rasterBandCollection.Item(0);
            IRasterDataset rasterDataset = (IRasterDataset)rasterBand;

            //Check for TIFF format
            string format_Renamed = rasterDataset.Format;
            if (format_Renamed.Substring(0, 4) != "TIFF")
            {
                return null;
            }

            //check for bit depth
            IRasterProps rasterProps = (IRasterProps)rasterBand;
            if (rasterProps.PixelType != rstPixelType.PT_U1)
            {
                return null;
            }

            //create renderer for 1 bit raster
            //Create a unique value renderer and associate it with raster
            IRasterUniqueValueRenderer rasterUniqueValueRenderer = new RasterUniqueValueRendererClass();
            IRasterRenderer rasterRenderer = (IRasterRenderer)rasterUniqueValueRenderer;
            rasterRenderer.Raster = raster;
            rasterRenderer.Update();

            //Define the renderer
            rasterUniqueValueRenderer.HeadingCount = 1;
            rasterUniqueValueRenderer.set_Heading(0, "");
            rasterUniqueValueRenderer.set_ClassCount(0, 2);
            rasterUniqueValueRenderer.Field = "VALUE";
            rasterUniqueValueRenderer.AddValue(0, 0, 0);
            rasterUniqueValueRenderer.AddValue(0, 1, 1);
            rasterUniqueValueRenderer.set_Label(0, 0, "0");
            rasterUniqueValueRenderer.set_Label(0, 1, "1");

            // Define symbology for rendering value 0
            IColor color1 = (IColor)(CreateRGBColor(200, 50, 0)); //Brown color

            ISimpleFillSymbol simpleFillSymbol1 = new SimpleFillSymbolClass();
            simpleFillSymbol1.Color = color1;
            rasterUniqueValueRenderer.set_Symbol(0, 0, (ISymbol)simpleFillSymbol1);

            IColor color2 = new RgbColorClass();
            color2.NullColor = true;

            ISimpleFillSymbol simpleFillSymbol2 = new SimpleFillSymbolClass();
            simpleFillSymbol2.Color = color2;

            rasterUniqueValueRenderer.set_Symbol(0, 1, (ISymbol)simpleFillSymbol2);
            return rasterRenderer;
        }

        public int Priority
        {
            get
            {
                //Give it a higher priority, so this renderer will be used as default
                return 98;
            }
        }

        #region COM Registration Function(s)
        [ComRegisterFunction()]
        static void Reg(string regKey)
        {
            ESRI.ArcGIS.ADF.CATIDs.RasterRendererMakers.Register(regKey);
        }

        [ComUnregisterFunction()]
        static void Unreg(string regKey)
        {
            ESRI.ArcGIS.ADF.CATIDs.RasterRendererMakers.Unregister(regKey);
        }
        #endregion

        #region Create RGBColor
        public IRgbColor CreateRGBColor(System.Byte myRed, System.Byte myGreen, System.Byte myBlue)
        {

            IRgbColor rgbColor = new RgbColorClass();
            rgbColor.Red = myRed;
            rgbColor.Green = myGreen;
            rgbColor.Blue = myBlue;
            rgbColor.UseWindowsDithering = true;

            return rgbColor;

        }
        #endregion
    }
}
