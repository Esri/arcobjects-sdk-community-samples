Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports ESRI.ArcGIS
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.DataSourcesRaster

Namespace RasterSamples
    Class CreateRasterFunctionTemplate
        <STAThread()> _
        Public Shared Sub Main(ByVal args As String())
            Dim aoInit As ESRI.ArcGIS.esriSystem.AoInitialize

            ' Initialize License
            Try
                Console.WriteLine("Obtaining license")
                ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.Desktop)
                aoInit = New AoInitializeClass()
                Dim licStatus As esriLicenseStatus = aoInit.Initialize(esriLicenseProductCode.esriLicenseProductCodeBasic)
                Console.WriteLine("Ready with license.")
            Catch exc As Exception
                ' If it fails at this point, shutdown the test and ignore any subsequent errors.
                Console.WriteLine(exc.Message)
                Exit Sub
            End Try

            Try
                ' Specify input directory and dataset name
                ' The directory which contains the Panchromatic Image.
                Dim panDir As String = "C:\Data\QB\Pan"
                ' The directory which contains the Multispectral Image.
                Dim rgbDir As String = "C:\Data\QB\MS"

                Dim panImageName As String = "05JAN27104436-P1BS-005533787010_01_P002.TIF"
                Dim rgbImageName As String = "05JAN27104436-M1BS-005533787010_01_P002.TIF"

                ' Output filename.
                Dim outputDataset As String = "c:\Temp\RFT\QBTemplateVB.afr"


                ' Initialize
                ' Setup Workspaces.
                Dim factoryType As Type = Type.GetTypeFromProgID("esriDataSourcesRaster.RasterWorkspaceFactory")
                Dim workspaceFactory As IWorkspaceFactory = DirectCast(Activator.CreateInstance(factoryType), IWorkspaceFactory)
                Dim panRasterWorkspace As IRasterWorkspace = DirectCast(workspaceFactory.OpenFromFile(panDir, 0), IRasterWorkspace)
                Dim rgbRasterWorkspace As IRasterWorkspace = DirectCast(workspaceFactory.OpenFromFile(rgbDir, 0), IRasterWorkspace)

                ' Open Datasets
                Dim panDataset As IRasterDataset = panRasterWorkspace.OpenRasterDataset(panImageName)
                Dim rgbDataset As IRasterDataset = rgbRasterWorkspace.OpenRasterDataset(rgbImageName)


                ' Create Variables
                ' Create one variable of type IRasterFunctionVariable for each 
                ' Raster Dataset opened above

                ' Create a new Raster Function Variable
                Dim panVar As IRasterFunctionVariable = New RasterFunctionVariableClass()
                ' Set the name of the variable
                panVar.Name = "panImage"
                ' Describe the variable
                panVar.Description = "Panchromatic Image to be used for pansharpening"
                ' Specify whether it represents a dataset
                panVar.IsDataset = True

                ' Create a new Raster Function Variable
                Dim rgbVar As IRasterFunctionVariable = New RasterFunctionVariableClass()
                ' Set the name of the variable
                rgbVar.Name = "rgbImage"
                ' Describe the variable
                rgbVar.Description = "Multispectral Image to be used for pansharpening"
                ' Specify whether it represents a dataset
                rgbVar.IsDataset = True


                ' Prepare the Pan Image
                ' Setup statistics for each band
                Dim statsArrayPan As IArray = New ArrayClass()
                Dim statsPanBand As IRasterStatistics = New RasterStatisticsClass()
                statsPanBand.Minimum = 1
                statsPanBand.Maximum = 2047
                statsArrayPan.Add(statsPanBand)
                ' Create the arguments object for the stretching function
                Dim stretchingPanFunctionArguements As IStretchFunctionArguments = New StretchFunctionArgumentsClass()
                ' Set the stretching type
                stretchingPanFunctionArguements.StretchType = esriRasterStretchType.esriRasterStretchMinimumMaximum
                ' Set the statistics created above
                stretchingPanFunctionArguements.Statistics = statsArrayPan
                ' Set the input raster, in this case, the variable for the Pan Image
                stretchingPanFunctionArguements.Raster = panVar

                ' Create the function object to stretch the Pan Image.
                Dim stretchingPanFunction As IRasterFunction = New StretchFunction()

                ' Create a Raster Function Template object for the stretch function
                Dim stretchingPanFunctionT As IRasterFunctionTemplate = New RasterFunctionTemplateClass()
                ' Set the function on the template
                stretchingPanFunctionT.[Function] = DirectCast(stretchingPanFunction, IRasterFunction)
                ' Set the arguments for the function
                stretchingPanFunctionT.Arguments = stretchingPanFunctionArguements


                ' Prepare the Multispectral (RGB) Image
                ' Create an array which defines the order of bands
                Dim bandIDs As ILongArray = New LongArrayClass()
                bandIDs.Add(2)
                bandIDs.Add(1)
                bandIDs.Add(0)
                ' Create an Extract Band Function Arguments object
                Dim extractRgbBandFunctionArgs As IExtractBandFunctionArguments = DirectCast(New ExtractBandFunctionArguments(), IExtractBandFunctionArguments)
                ' Set the order of bands of the output
                extractRgbBandFunctionArgs.BandIDs = bandIDs
                ' Set the input raster, in this case the variable for the Multispectral Image
                extractRgbBandFunctionArgs.Raster = rgbVar

                ' Create the Extract Band Function object
                Dim extractRgbBandFunction As IRasterFunction = New ExtractBandFunction()

                ' Create a Raster Function Template object for the function created above
                Dim extractRgbBandFunctionT As IRasterFunctionTemplate = New RasterFunctionTemplateClass()
                ' Set the function on the template
                extractRgbBandFunctionT.[Function] = DirectCast(extractRgbBandFunction, IRasterFunction)
                ' Set the arguments for the function
                extractRgbBandFunctionT.Arguments = extractRgbBandFunctionArgs

                ' Setup statistics for each band
                Dim statsArray As IArray = New ArrayClass()
                Dim statsMulBand1 As IRasterStatistics = New RasterStatisticsClass()
                statsMulBand1.Minimum = 100
                statsMulBand1.Maximum = 1721
                statsArray.Add(statsMulBand1)
                Dim statsMulBand2 As IRasterStatistics = New RasterStatisticsClass()
                statsMulBand2.Minimum = 95
                statsMulBand2.Maximum = 2047
                statsArray.Add(statsMulBand2)
                Dim statsMulBand3 As IRasterStatistics = New RasterStatisticsClass()
                statsMulBand3.Minimum = 34
                statsMulBand3.Maximum = 2006
                statsArray.Add(statsMulBand3)

                ' Create a stretching function for the multispectral image
                Dim stretchingRGBFunction As IRasterFunction = New StretchFunction()
                ' Create an arguments object for the stretch function
                Dim stretchingRGBFunctionArguments As IStretchFunctionArguments = New StretchFunctionArgumentsClass()
                ' Set the type of stretchings to perform
                stretchingRGBFunctionArguments.StretchType = esriRasterStretchType.esriRasterStretchMinimumMaximum
                ' Set the statistics created above
                stretchingRGBFunctionArguments.Statistics = statsArray
                ' Set the extract band function template created above as the input
                stretchingRGBFunctionArguments.Raster = extractRgbBandFunctionT

                ' Create a Raster Function Template object for the function created above
                Dim stretchingRGBFunctionT As IRasterFunctionTemplate = New RasterFunctionTemplateClass()
                ' Set the function on the template
                stretchingRGBFunctionT.[Function] = DirectCast(stretchingRGBFunction, IRasterFunction)
                ' Set the arguments for the function
                stretchingRGBFunctionT.Arguments = stretchingRGBFunctionArguments


                ' Pansharpen the Pan Image with the Multispectral
                ' Create a Raster Function Arguments object for the pansharpen function
                Dim pansharpFunctionArguements As IPansharpeningFunctionArguments = New PansharpeningFunctionArgumentsClass()
                ' Set the Panchromatic image which has been prepared above
                pansharpFunctionArguements.PanImage = stretchingPanFunctionT
                ' Set the Multispectral image which has been prepared above
                pansharpFunctionArguements.MSImage = stretchingRGBFunctionT

                ' Create a new pansharpen raster function object
                Dim pansharpenFunction As IRasterFunction = New PansharpeningFunction()
                ' Create a new pansharpen function arguments object
                Dim pansharpenFunctionArguements As IPansharpeningFunctionArguments = New PansharpeningFunctionArgumentsClass()
                ' Set the pansharpening type
                pansharpenFunctionArguements.PansharpeningType = esriPansharpeningType.esriPansharpeningESRI
                ' Create an array of doubles that sets the weights for each band
                Dim weightsArray As IDoubleArray = New DoubleArrayClass()
                weightsArray.Add(0.167)
                weightsArray.Add(0.166)
                weightsArray.Add(0.166)
                ' and set it on the arguments object
                pansharpenFunctionArguements.Weights = weightsArray

                ' Create a Raster Function Template object for the pansharpen function
                Dim rasterFunction As IRasterFunctionTemplate = New RasterFunctionTemplateClass()
                rasterFunction.[Function] = DirectCast(pansharpenFunction, IRasterFunction)
                rasterFunction.Arguments = pansharpFunctionArguements
                DirectCast(rasterFunction, IRasterFunction).PixelType = rstPixelType.PT_UCHAR


                ' Resolve variables
                ' Create a PropertySet object to set the values for the 
                ' Raster Function Variables created above
                Dim variables As IPropertySet = New PropertySet()
                variables.SetProperty("panImage", panDataset)
                variables.SetProperty("rgbImage", rgbDataset)


                ' Create the Function Raster Dataset
                ' Create the Function Raster Dataset Object for the Pansharpened Dataset
                Dim functionRasterDataset As IFunctionRasterDataset = New FunctionRasterDataset()
                ' Create a name object for the Function Raster Dataset.
                Dim functionRasterDatasetName As IFunctionRasterDatasetName = New FunctionRasterDatasetNameClass()
                ' Specify the output filename for the new dataset (including 
                ' the .afr extension at the end).
                functionRasterDatasetName.FullName = outputDataset
                functionRasterDataset.FullName = DirectCast(functionRasterDatasetName, IName)
                ' Initialize the new Function Raster Dataset with the Raster Function 
                ' Template and the property set containing the variables and their values.
                functionRasterDataset.Init(DirectCast(rasterFunction, IRasterFunction), variables)

                Dim myTempDset As ITemporaryDataset = DirectCast(functionRasterDataset, ITemporaryDataset)
                myTempDset.MakePermanent()


                ' Shutdown
                Console.WriteLine("Success.")
                Console.WriteLine("Press any key...")
                Console.ReadKey()

                ' Shutdown License
                aoInit.Shutdown()
            Catch exc As Exception
                ' Shutdown
                Console.WriteLine("Exception Caught while creating Function Raster Dataset. " & exc.Message)
                Console.WriteLine("Failed.")
                Console.WriteLine("Press any key...")
                Console.ReadKey()

                ' Shutdown License
                aoInit.Shutdown()
            End Try
        End Sub
    End Class
End Namespace
