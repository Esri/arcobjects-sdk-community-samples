using System;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;

namespace RasterSamples
{
    class CreateFunctionRasterDataset
    {
        [STAThread]
        static void Main(string[] args)
        {
            ESRI.ArcGIS.esriSystem.AoInitialize aoInit;

            #region Initialize License
            try
            {
                Console.WriteLine("Obtaining license");
                ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.Desktop);
                aoInit = new AoInitializeClass();
                esriLicenseStatus licStatus = aoInit.Initialize(esriLicenseProductCode.esriLicenseProductCodeBasic);
                Console.WriteLine("Ready with license.");
            }
            catch (Exception exc)
            {
                // If it fails at this point, shutdown the test and ignore any subsequent errors.
                Console.WriteLine(exc.Message);
                return;
            }
            #endregion

            try
            {
                // Specify input directory and dataset name.
                string inputWorkspace = @"C:\Data";
                string inputDatasetName = "8bitSampleImage.tif";
                // Specify output filename.
                string outputDataset = @"c:\Temp\testArithmaticCS.afr";

                // Open the Raster Dataset
                Type factoryType = Type.GetTypeFromProgID("esriDataSourcesRaster.RasterWorkspaceFactory");
                IWorkspaceFactory workspaceFactory = (IWorkspaceFactory)Activator.CreateInstance(factoryType);
                IWorkspace workspace = workspaceFactory.OpenFromFile(inputWorkspace, 0);
                IRasterWorkspace rasterWorkspace = (IRasterWorkspace)workspace;
                IRasterDataset myRasterDataset = rasterWorkspace.OpenRasterDataset(inputDatasetName);

                // Create the Function Arguments object
                IArithmeticFunctionArguments rasterFunctionArguments =
                    (IArithmeticFunctionArguments)new ArithmeticFunctionArguments();                
                // Set the parameters for the function:
                // Specify the operation as addition (esriRasterPlus)
                rasterFunctionArguments.Operation = esriRasterArithmeticOperation.esriRasterPlus;
                // Specify the first operand, i.e. the Raster Dataset opened above.
                rasterFunctionArguments.Raster = myRasterDataset;
                // For the second operand, create an array of double values
                // containing the scalar value to be used as the second operand 
                // to each band of the input dataset.
                // The number of values in the array should equal the number 
                // of bands of the input dataset.
                double[] scalars = { 128.0, 128.0, 128.0 };
                // Create a new Scalar object and specify
                // the array as its value.
                IScalar scalarVals = new ScalarClass();
                scalarVals.Value = scalars;
                // Specify the scalar object as the second operand.
                rasterFunctionArguments.Raster2 = scalarVals;

                // Create the Raster Function object.
                IRasterFunction rasterFunction = new ArithmeticFunction();
                rasterFunction.PixelType = rstPixelType.PT_USHORT;

                // Create the Function Raster Dataset Object.
                IFunctionRasterDataset functionRasterDataset = new FunctionRasterDataset();

                // Create a name object for the Function Raster Dataset.
                IFunctionRasterDatasetName functionRasterDatasetName =
                    (IFunctionRasterDatasetName)new FunctionRasterDatasetName();

                // Specify the output filename for the new dataset (including 
                // the .afr extension at the end).
                functionRasterDatasetName.FullName = outputDataset;
                functionRasterDataset.FullName = (IName)functionRasterDatasetName;
                // Initialize the new Function Raster Dataset with the Raster Function 
                // and its arguments.
                functionRasterDataset.Init(rasterFunction, rasterFunctionArguments);

                // QI for the Temporary Dataset interface
                ITemporaryDataset myTempDset = (ITemporaryDataset)functionRasterDataset;
                // and make it a permanent dataset. This creates the afr file.
                myTempDset.MakePermanent();

                // Report
                Console.WriteLine("Success.");
                Console.WriteLine("Press any key...");
                Console.ReadKey();

                // Shutdown License
                aoInit.Shutdown();
            }
            catch (Exception exc)
            {
                // Report
                Console.WriteLine("Exception Caught while creating Function Raster Dataset. " + exc.Message);
                Console.WriteLine("Failed.");
                Console.WriteLine("Press any key...");
                Console.ReadKey();

                // Shutdown License
                aoInit.Shutdown();
            }
        }
    }
}
