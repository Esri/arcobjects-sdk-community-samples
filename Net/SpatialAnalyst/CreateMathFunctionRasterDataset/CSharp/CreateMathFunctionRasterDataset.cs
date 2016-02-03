using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.SpatialAnalyst;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.esriSystem;


namespace CreateMathFunctionRasterDataset
{
    class Program
    {
        static void Main(string[] args)
        {
            ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.Desktop);
            IAoInitialize aoInit = new AoInitialize();
            esriLicenseStatus licStat = aoInit.Initialize(esriLicenseProductCode.esriLicenseProductCodeEngine);
            licStat = aoInit.CheckOutExtension(esriLicenseExtensionCode.esriLicenseExtensionCodeSpatialAnalyst);

            CreateMathFunctionRasterDataset();
            aoInit.Shutdown();
        }

        public static void CreateMathFunctionRasterDataset()
        {
            //Create the Raster Function object and Function Arguments object for first operation 
            IRasterFunction rasterFunction1 = new MathFunction();
            IMathFunctionArguments mathFunctionArguments1 = new MathFunctionArguments() as IMathFunctionArguments;

            //Specify operation to be "Plus" for the first operation
            mathFunctionArguments1.Operation = esriGeoAnalysisFunctionEnum.esriGeoAnalysisFunctionPlus;

            //Specify input rasters to the operation
            IRasterDataset ras01 = OpenRasterDataset("c:\\data\\test", "degs");
            IRasterDataset ras02 = OpenRasterDataset("c:\\data\\test", "negs");
            mathFunctionArguments1.Raster = ras01;
            mathFunctionArguments1.Raster2 = ras02;            

            //Create and initialize 1st function raster dataset with the Raster Function object and its arguments object
            IFunctionRasterDataset functionRasterDataset1;
            functionRasterDataset1 = new FunctionRasterDataset();
            functionRasterDataset1.Init(rasterFunction1, mathFunctionArguments1);

            //Create the Raster Function and the Function Arguments object for the 2nd operation
            IRasterFunction rasterFunction2 = new MathFunction();
            IMathFunctionArguments mathFunctionArguments2 = new MathFunctionArguments() as IMathFunctionArguments;

            //Specify operation to be "Divide" for the 2nd operation
            mathFunctionArguments2.Operation = esriGeoAnalysisFunctionEnum.esriGeoAnalysisFunctionDivide;

            //Specify input rasters to the 2nd operation
            //Use the output function raster dataset from the 1st operation as one of the input             
            mathFunctionArguments2.Raster = functionRasterDataset1;
            IRasterDataset ras03 = OpenRasterDataset("c:\\data\\test", "cost");
            mathFunctionArguments2.Raster2 = ras03;

            //Create and initialize the 2nd function raster dataset
            IFunctionRasterDataset functionRasterDataset2;
            functionRasterDataset2 = new FunctionRasterDataset();
            IFunctionRasterDatasetName functionRasterDatasetName = (IFunctionRasterDatasetName)new FunctionRasterDatasetName();
            functionRasterDatasetName.FullName = "c:\\output\\math_out.afr";
            functionRasterDataset2.FullName = (IName)functionRasterDatasetName;
            functionRasterDataset2.Init(rasterFunction2, mathFunctionArguments2);
                        
            //Save the 2nd function raster dataset            
            ITemporaryDataset temporaryDataset = (ITemporaryDataset)functionRasterDataset2;            
            temporaryDataset.MakePermanent();
        }        
        
        public static IRasterDataset OpenRasterDataset(string sPath, string sFileName)
        {
            // Returns RasterDataset object given a file name and its directory.
            // sPath: path of the input raster dataset.
            // sFileName: name of the input raster dataset.

            IRasterDataset rasterDataset = null;

            try
            {
                IWorkspaceFactory workspaceFactory = new RasterWorkspaceFactory();
                IRasterWorkspace rasterWorkspace;

                rasterWorkspace = (IRasterWorkspace)workspaceFactory.OpenFromFile(sPath, 0);
                rasterDataset = rasterWorkspace.OpenRasterDataset(sFileName);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed in Opening RasterDataset. " + ex.InnerException.ToString());
            }

            return rasterDataset;
        }

        public static IRasterWorkspace OpenRasterWorkspace(string sPath)
        {
            IWorkspaceFactory workspaceFactory = new RasterWorkspaceFactory();
            IRasterWorkspace rasterWorkspace = null;

            rasterWorkspace = (IRasterWorkspace)workspaceFactory.OpenFromFile(sPath, 0);

            return rasterWorkspace;
        }
    }
}
