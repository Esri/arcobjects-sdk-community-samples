'Copyright 2016 Esri

'Licensed under the Apache License, Version 2.0 (the "License");
'you may not use this file except in compliance with the License.
'You may obtain a copy of the License at

'    http://www.apache.org/licenses/LICENSE-2.0

'Unless required by applicable law or agreed to in writing, software
'distributed under the License is distributed on an "AS IS" BASIS,
'WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.

'See the License for the specific language governing permissions and
'limitations under the License.
Imports ESRI.ArcGIS.DataSourcesRaster
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Geodatabase

Namespace RasterSamples
	Class CreateFunctionRasterDataset
		<STAThread> _
		Friend Shared Sub Main(args As String())
			Dim aoInit As ESRI.ArcGIS.esriSystem.AoInitialize

			'#Region "Initialize License"
			Try
				Console.WriteLine("Obtaining license")
				ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.Desktop)
				aoInit = New AoInitializeClass()
				Dim licStatus As esriLicenseStatus = aoInit.Initialize(esriLicenseProductCode.esriLicenseProductCodeBasic)
				Console.WriteLine("Ready with license.")
			Catch exc As Exception
				' If it fails at this point, shutdown the test and ignore any subsequent errors.
				Console.WriteLine(exc.Message)
				Return
			End Try
			'#End Region

			Try
				' Specify input directory and dataset name.
				Dim inputWorkspace As String = "C:\Data"
				Dim inputDatasetName As String = "8bitSampleImage.tif"
				' Specify output filename.
                Dim outputDataset As String = "c:\Temp\testArithmaticVB.afr"

				' Open the Raster Dataset
				Dim factoryType As Type = Type.GetTypeFromProgID("esriDataSourcesRaster.RasterWorkspaceFactory")
				Dim workspaceFactory As IWorkspaceFactory = DirectCast(Activator.CreateInstance(factoryType), IWorkspaceFactory)
				Dim workspace As IWorkspace = workspaceFactory.OpenFromFile(inputWorkspace, 0)
				Dim rasterWorkspace As IRasterWorkspace = DirectCast(workspace, IRasterWorkspace)
				Dim myRasterDataset As IRasterDataset = rasterWorkspace.OpenRasterDataset(inputDatasetName)

				' Create the Function Arguments object
				Dim rasterFunctionArguments As IArithmeticFunctionArguments = DirectCast(New ArithmeticFunctionArguments(), IArithmeticFunctionArguments)
				' Set the parameters for the function:
				' Specify the operation as addition (esriRasterPlus)
				rasterFunctionArguments.Operation = esriRasterArithmeticOperation.esriRasterPlus
				' Specify the first operand, i.e. the Raster Dataset opened above.
				rasterFunctionArguments.Raster = myRasterDataset
				' For the second operand, create an array of double values
				' containing the scalar value to be used as the second operand 
				' to each band of the input dataset.
				' The number of values in the array should equal the number 
				' of bands of the input dataset.
				Dim scalars As Double() = {128.0, 128.0, 128.0}
				' Create a new Scalar object and specify
				' the array as its value.
				Dim scalarVals As IScalar = New ScalarClass()
				scalarVals.Value = scalars
				' Specify the scalar object as the second operand.
				rasterFunctionArguments.Raster2 = scalarVals

				' Create the Raster Function object.
				Dim rasterFunction As IRasterFunction = New ArithmeticFunction()
				rasterFunction.PixelType = rstPixelType.PT_USHORT

				' Create the Function Raster Dataset Object.
				Dim functionRasterDataset As IFunctionRasterDataset = New FunctionRasterDataset()

				' Create a name object for the Function Raster Dataset.
				Dim functionRasterDatasetName As IFunctionRasterDatasetName = DirectCast(New FunctionRasterDatasetName(), IFunctionRasterDatasetName)

				' Specify the output filename for the new dataset (including 
				' the .afr extension at the end).
				functionRasterDatasetName.FullName = outputDataset
				functionRasterDataset.FullName = DirectCast(functionRasterDatasetName, IName)
				' Initialize the new Function Raster Dataset with the Raster Function 
				' and its arguments.
				functionRasterDataset.Init(rasterFunction, rasterFunctionArguments)

				' QI for the Temporary Dataset interface
				Dim myTempDset As ITemporaryDataset = DirectCast(functionRasterDataset, ITemporaryDataset)
				' and make it a permanent dataset. This creates the afr file.
				myTempDset.MakePermanent()

				' Report
				Console.WriteLine("Success.")
				Console.WriteLine("Press any key...")
				Console.ReadKey()

				' Shutdown License
				aoInit.Shutdown()
			Catch exc As Exception
				' Report
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
