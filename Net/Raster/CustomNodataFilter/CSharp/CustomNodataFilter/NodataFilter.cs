using System;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.Geodatabase;

namespace CustomNodataFilter
{
    //This sample shows the steps to create a customized pixelfilter
    //INodataFilter filters out a range of values in a raster to be nodata
    public interface INodataFilter : IPixelFilter
    {
        //IPixelFilter methods
        new void Filter(IPixelBlock pPixelBlock);
        new void GetCenterPosition(ref int x, ref int y);
        new void GetSize(ref int nCols, ref int nRows);

        //INodataFilter members
        int MinNodataValue { get; set;}
        int MaxNodataValue { get; set;}
    }

    public class NodataFilter : INodataFilter
    {
        public void Filter(IPixelBlock pPixelBlock)
        {
            try
            {
                IPixelBlock3 pPixelBlock3 = (IPixelBlock3)pPixelBlock;

                byte[] lookup = new byte[8] { 128, 64, 32, 16, 8, 4, 2, 1 };

                //get number of bands
                int plane = pPixelBlock.Planes;

                //loop through each band
                for (int i = 0; i < plane; i++)
                {
                    //get nodata mask array
                    byte[] outputArray = (byte[])pPixelBlock3.get_NoDataMaskByRef(i);

                    //loop through each pixel in the pixelblock and do calculation
                    for (int x = 0; x < pPixelBlock.Width; x++)
                    {
                        for (int y = 0; y < pPixelBlock.Height; y++)
                        {
                            //get index in the nodata mask byte array
                            int ind = x + y * (pPixelBlock.Width);

                            //get nodata mask byte 
                            byte nd = outputArray[ind / 8];

                            //get pixel value and check if it is nodata
                            object tempVal = pPixelBlock3.GetVal(i, x, y);

                            if (tempVal != null) //not nodata pixel
                            {
                                //convert pixel value to int and compare with nodata range

                                int curVal = Convert.ToInt32(tempVal);
                                if (curVal >= minNodataValue && curVal <= maxNodataValue)
                                {
                                    outputArray[ind / 8] = (byte)(nd & ~lookup[ind % 8]);
                                }
                            }
                        }
                    }
                    //set nodata mask array
                    pPixelBlock3.set_NoDataMask(i, outputArray);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        //implements IPixelFilter:GetCenterPosition
        public void GetCenterPosition(ref int x, ref int y)
        {
            x = 0;
            y = 0;
        }

        //implements IPixelFilter:GetSize
        public void GetSize(ref int nCols, ref int nRows)
        {
            nCols = 0;
            nRows = 0;
        }

        //get/set max range of nodata 
        public int MaxNodataValue
        {
            get
            {
                return maxNodataValue;
            }
            set
            {
                maxNodataValue = value;
            }
        }

        //get/set min range of nodata
        public int MinNodataValue
        {
            get
            {
                return minNodataValue;
            }
            set
            {
                minNodataValue = value;
            }
        }

        private int minNodataValue;
        private int maxNodataValue;

    }
}