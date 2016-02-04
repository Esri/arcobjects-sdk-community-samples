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
Imports ESRI.ArcGIS.SpatialAnalyst
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.esriSystem

Module Module1

    Sub Main()
        ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.Desktop)
        Dim aoInit As IAoInitialize
        aoInit = New AoInitialize()
        Dim licStat As esriLicenseStatus
        licStat = aoInit.Initialize(esriLicenseProductCode.esriLicenseProductCodeEngine)
        licStat = aoInit.CheckOutExtension(esriLicenseExtensionCode.esriLicenseExtensionCodeSpatialAnalyst)

        CreateMathFunctionRasterDataset()
        aoInit.Shutdown()
    End Sub

    Public Sub CreateMathFunctionRasterDataset()

        'Create the Raster Function object and Function Arguments object for first operation 
        Dim rasterFunction1 As IRasterFunction
        rasterFunction1 = New MathFunction()
        Dim mathFunctionArguments1 As IMathFunctionArguments
        mathFunctionArguments1 = New MathFunctionArguments()

        'Specify operation to be "Plus" for the first operation
        mathFunctionArguments1.Operation = esriGeoAnalysisFunctionEnum.esriGeoAnalysisFunctionPlus

        'Specify input rasters to the operation
        Dim ras01 As IRasterDataset
        Dim ras02 As IRasterDataset
        ras01 = OpenRasterDataset("c:\data\test", "degs")
        ras02 = OpenRasterDataset("c:\data\test", "negs")
        mathFunctionArguments1.Raster = ras01
        mathFunctionArguments1.Raster2 = ras02

        'Create and initialize 1st function raster dataset with the Raster Function object and its arguments object
        Dim functionRasterDataset1 As IFunctionRasterDataset
        functionRasterDataset1 = New FunctionRasterDataset()
        functionRasterDataset1.Init(rasterFunction1, mathFunctionArguments1)

        'Create the Raster Function and the Function Arguments object for the 2nd operation
        Dim rasterFunction2 As IRasterFunction
        rasterFunction2 = New MathFunction()
        Dim mathFunctionArguments2 As IMathFunctionArguments
        mathFunctionArguments2 = New MathFunctionArguments()

        'Specify operation to be "Divide" for the 2nd operation
        mathFunctionArguments2.Operation = esriGeoAnalysisFunctionEnum.esriGeoAnalysisFunctionDivide

        'Specify input rasters to the 2nd operation
        'Use the output function raster dataset from the 1st operation as one of the input   
        mathFunctionArguments2.Raster = functionRasterDataset1
        Dim ras03 As IRasterDataset
        ras03 = OpenRasterDataset("c:\data\test", "cost")
        mathFunctionArguments2.Raster2 = ras03

        'Create and initialize the 2nd function raster dataset
        Dim functionRasterDataset2 As IFunctionRasterDataset
        functionRasterDataset2 = New FunctionRasterDataset()
        Dim functionRasterDatasetName As IFunctionRasterDatasetName
        functionRasterDatasetName = New FunctionRasterDatasetName()
        functionRasterDatasetName.FullName = "c:\output\math_out.afr"
        functionRasterDataset2.FullName = functionRasterDatasetName
        functionRasterDataset2.Init(rasterFunction2, mathFunctionArguments2)

        'Save the 2nd function raster dataset
        Dim temporaryDataset As ITemporaryDataset
        temporaryDataset = functionRasterDataset2
        temporaryDataset.MakePermanent()
        
    End Sub

    Public Function OpenRasterDataset(ByVal sPath As String, ByVal sFileName As String) As IRasterDataset

        ' Returns RasterDataset object given a file name and its directory.
        ' sPath: path of the input raster dataset.
        ' sFileName: name of the input raster dataset.

        Dim rasterDataset As IRasterDataset = Nothing

        Try
            Dim workspaceFactory As IWorkspaceFactory = New RasterWorkspaceFactory()
            Dim rasterWorkspace As IRasterWorkspace

            rasterWorkspace = CType(workspaceFactory.OpenFromFile(sPath, 0), IRasterWorkspace)
            rasterDataset = rasterWorkspace.OpenRasterDataset(sFileName)
        Catch ex As Exception
            Console.WriteLine("Failed in Opening RasterDataset. " & ex.InnerException.ToString)
        End Try

        Return rasterDataset

    End Function

    Public Function OpenRasterWorkspace(ByVal sPath As String) As IRasterWorkspace
        Dim workspaceFactory As IWorkspaceFactory
        workspaceFactory = New RasterWorkspaceFactory()

        Dim rasWS As IRasterWorkspace
        rasWS = Nothing

        rasWS = workspaceFactory.OpenFromFile(sPath, 0)
        OpenRasterWorkspace = rasWS
    End Function

End Module