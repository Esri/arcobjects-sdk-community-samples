/*This project already has the COM Component Category Registration information embedded. If you build this project it will 
overwrite ArcMap's default behavior for rendering a raster catalog. Since this project alters the default behavior it 
may be desirable to remove this projects functionality from the system to restore the default image rendering capabilities.
This can be accomplished by:
1. Open a Visual Studio 2008 Command Prompt and navigate to the location of this projects .dll that gets created (for 
example: C:\temp\RasterRendererMaker_1bit_TIFF_VBNET\bin\Debug)
2. Use the esriregasm.exe utility from the Program Files\Common Files\ArcGIS\bin to unregister the DLL.
*/

using ESRI.ArcGIS.esriSystem;
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.Display;

namespace CustomRasterCatalogRendererPicker_CSharp
{
    [Guid("b148cd51-201f-4d92-9b53-b4ce38d362b0")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("CustomRasterCatalogRendererPicker_CSharp.CustomRasterCatalogRenderer_CSharp")]
    public class CustomRasterCatalogRenderer_CSharp : ESRI.ArcGIS.Carto.IRasterCatalogRendererPicker
    {
        public CustomRasterCatalogRenderer_CSharp()
        {
        }
       
        #region IRasterCatalogRendererPicker Members

        public string[] AllAvailableRenderersCLSID
        {
            get
            {
                string[] Renderers = new string[2];
                //Define the raster renderers using class ID
                Renderers[0] = "esriCarto.RasterRGBRenderer";
                Renderers[1] = "esriCarto.RasterStretchColorRampRenderer";
                return Renderers;
            }
        }

        public string[] DefaultUseRenderersCLSID
        {
            get
            {
                string[] Renderers = new string[2];
                //Define the available raster renderers
                Renderers[0] = "esriCarto.RasterRGBRenderer";
                Renderers[1] = "esriCarto.RasterStretchColorRampRenderer";
                return Renderers;
            }
        }

        public ESRI.ArcGIS.Carto.IRasterRenderer Pick(ESRI.ArcGIS.esriSystem.IArray pRenderers, ESRI.ArcGIS.Geodatabase.IRasterDataset pRasterDataset)
        {
            IRasterRenderer rasterRenderer = null;
            IRasterRGBRenderer rasterRGBRenderer = null;
            IRasterStretchColorRampRenderer rasterStretchColorRampRenderer = null;

            // Get the renderers
            int Count = pRenderers.Count;
            int i = 0;
            for (i = 0; i < Count; i++)
            {

                rasterRenderer = (IRasterRenderer)(pRenderers.get_Element(i));

                if (rasterRenderer is IRasterStretchColorRampRenderer)
                {
                    rasterStretchColorRampRenderer = (IRasterStretchColorRampRenderer)rasterRenderer;
                }
                else if (rasterRenderer is IRasterRGBRenderer)
                {
                    rasterRGBRenderer = (IRasterRGBRenderer)rasterRenderer;
                }

            }

            IRasterDataset2 rasterDataset2 = (IRasterDataset2)pRasterDataset;
            IRasterBandCollection rasterBandCollection = (IRasterBandCollection)rasterDataset2;

            if (rasterBandCollection.Count > 5)
            {

                // Use band 4,5 and 3 as red, green and blue
                rasterRenderer = (IRasterRenderer)rasterRGBRenderer;
                rasterRGBRenderer.SetBandIndices(3, 4, 2);
                return (IRasterRenderer)rasterRGBRenderer;

            }
            else // Special stretch
            {

                IRasterBand rasterBand = rasterBandCollection.Item(0);

                bool hasTable = false;
                rasterBand.HasTable(out hasTable);

                if (hasTable == false)
                {

                    // Simply change the color ramp for the stretch renderer
                    //IColor fromColor = new RgbColorClass();
                    //fromColor.RGB = Microsoft.VisualBasic.Information.RGB(255, 200, 50);
                    IColor fromColor = CreateRGBColor(255, 200, 50) as IColor;

                    //IColor toColor = new RgbColorClass();
                    //toColor.RGB = Microsoft.VisualBasic.Information.RGB(180, 125, 0);
                    IColor toColor = CreateRGBColor(180, 125, 0) as IColor;

                    // Create color ramp
                    IAlgorithmicColorRamp algorithmicColorRamp = new AlgorithmicColorRampClass();
                    algorithmicColorRamp.Size = 255;
                    algorithmicColorRamp.FromColor = fromColor;
                    algorithmicColorRamp.ToColor = toColor;
                    bool createRamp = false;
                    algorithmicColorRamp.CreateRamp(out createRamp);

                    if (createRamp == true)
                    {

                        rasterRenderer = (IRasterRenderer)rasterStretchColorRampRenderer;
                        rasterStretchColorRampRenderer.BandIndex = 0;
                        rasterStretchColorRampRenderer.ColorRamp = algorithmicColorRamp;
                        return (IRasterRenderer)rasterStretchColorRampRenderer;

                    }

                }

            }

            return rasterRenderer;
        }

        public int Priority
        {
            get
            {
                return 10;
            }
        }

        #endregion
        #region COM Registration Function(s)
        [ComRegisterFunction()]
        static void Reg(string regKey)
        {
            ESRI.ArcGIS.ADF.CATIDs.RasterCatalogRendererPickers.Register(regKey);
        }

        [ComUnregisterFunction()]
        static void Unreg(string regKey)
        {
            ESRI.ArcGIS.ADF.CATIDs.RasterCatalogRendererPickers.Unregister(regKey);
        }
        #endregion
        #region "Create RGBColor"

        ///<summary>Generate an RgbColor by specifying the amount of Red, Green and Blue.</summary>
        /// 
        ///<param name="myRed">A byte (0 to 255) used to represent the Red color. Example: 0</param>
        ///<param name="myGreen">A byte (0 to 255) used to represent the Green color. Example: 255</param>
        ///<param name="myBlue">A byte (0 to 255) used to represent the Blue color. Example: 123</param>
        ///  
        ///<returns>An IRgbColor interface</returns>
        ///  
        ///<remarks></remarks>
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
