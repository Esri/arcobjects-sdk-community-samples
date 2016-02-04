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
Imports Microsoft.Win32
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.DataSourcesRaster
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.DataManagementTools
Imports ESRI.ArcGIS.Geoprocessor
Imports ESRI.ArcGIS.DataSourcesGDB

'Sample creating a file raster mosaic from rasters in a folder and its subfolders
'Steps:
'  1. Create an unmanaged PGDB raster catalog
'  2. Load rasters in the input folder and its subfolders to the new raster catalog
'  3. Create a mosaic file raster dataset from the unmanaged raster catalog

Module CreateFileRasterMosaic
    'Local variables for data path
    'The TEMP directory will be used to create temporary raster catalog and output raster dataset
    'Remove temp.mdb in TEMP directory if it exists
    'You can substitute the paths with your data location

    Private inputFolder As String = "C:\data"
    Private outputFolder As String = "C:\Temp"
    Private outputName As String = "mosaic.tif"
    Private tempRasterCatalog As String = "temp_rc"
    Private tempPGDB As String = "temp.mdb"
    Private tempPGDBPath As String = outputFolder + "\" + tempPGDB
    Private tempRasterCatalogPath As String = tempPGDBPath + "\" + tempRasterCatalog

    Sub Main(ByVal args As String())
        Dim aoInit As ESRI.ArcGIS.esriSystem.AoInitialize = Nothing
        Try
            ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.Desktop)
            aoInit = New AoInitializeClass()
            Dim licStatus As esriLicenseStatus = aoInit.Initialize(esriLicenseProductCode.esriLicenseProductCodeBasic)
            Console.WriteLine("License Checkout successful.")
        Catch exc As Exception
            ' If it fails at this point, shutdown the test and ignore any subsequent errors.
            Console.WriteLine(exc.Message)
        End Try

        Try
            'Create temporary unmanaged raster catalog and load all rasters
            CreateUnmanagedRasterCatalog()

            'Open raster catalog
            Dim rasterWorkspaceEx As IRasterWorkspaceEx = CType(OpenRasterPGDBWorkspace(tempPGDBPath), IRasterWorkspaceEx)
            Dim rasterCatalog As IRasterCatalog = rasterWorkspaceEx.OpenRasterCatalog(tempRasterCatalog)

            'Mosaic rasters in the raster catalog
            Mosaic(rasterCatalog)

        Catch exc As Exception
            Console.WriteLine(exc.Message)
        End Try

        Console.WriteLine("Please press any key to close the application.")
        Console.ReadKey()

        'Do not make any call to ArcObjects after ShutDown() call
        aoInit.Shutdown()
    End Sub

    Sub CreateUnmanagedRasterCatalog()
        Try
            'Use geoprocessing to create the geodatabase, the raster catalog, and load our directory
            'to the raster catalog.
            Dim geoprocessor As New Geoprocessor()

            'Create personal GDB in the TEMP directory
            Dim createPersonalGDB As New CreatePersonalGDB()
            createPersonalGDB.out_folder_path = outputFolder
            createPersonalGDB.out_name = tempPGDB

            geoprocessor.Execute(createPersonalGDB, Nothing)

            'Create an unmanaged raster catalog in the newly created personal GDB
            Dim createRasterCatalog As New CreateRasterCatalog()

            createRasterCatalog.out_path = tempPGDBPath
            createRasterCatalog.out_name = tempRasterCatalog
            createRasterCatalog.raster_management_type = "unmanaged"

            geoprocessor.Execute(createRasterCatalog, Nothing)

            'Load data into the unmanaged raster catalog
            Dim wsToRasterCatalog As New WorkspaceToRasterCatalog()

            wsToRasterCatalog.in_raster_catalog = tempRasterCatalogPath
            wsToRasterCatalog.in_workspace = inputFolder
            wsToRasterCatalog.include_subdirectories = "INCLUDE_SUBDIRECTORIES"

            geoprocessor.Execute(wsToRasterCatalog, Nothing)
        Catch exc As Exception
            Console.WriteLine(exc.Message)
        End Try
    End Sub

    Sub Mosaic(ByVal rasterCatalog As IRasterCatalog)
        Try
            'Mosaics all rasters in the raster catalog to an output raster dataset
            Dim mosaicRaster As IMosaicRaster = New MosaicRasterClass()
            mosaicRaster.RasterCatalog = rasterCatalog

            'Set mosaicking options, you may not need to set these for your data
            mosaicRaster.MosaicColormapMode = rstMosaicColormapMode.MM_MATCH
            mosaicRaster.MosaicOperatorType = rstMosaicOperatorType.MT_LAST

            'Open output workspace
            Dim workspaceFactory As IWorkspaceFactory = New RasterWorkspaceFactoryClass()
            Dim workspace As IWorkspace = workspaceFactory.OpenFromFile(outputFolder, 0)

            'Save out to a target raster dataset
            'It can be saved to TIFF, IMG, GRID, BMP, GIF, JPEG2000, JPEG, Geodatabase, ect.
            Dim saveas As ISaveAs = CType(mosaicRaster, ISaveAs)
            saveas.SaveAs(outputName, workspace, "TIFF")
        Catch exc As Exception
            Console.WriteLine(exc.Message)
        End Try
    End Sub

    Function OpenRasterPGDBWorkspace(ByVal connStr As String) As IWorkspace
        '        Dim workspaceFactory As IWorkspaceFactory2 = New AccessWorkspaceFactoryClass()
        Dim t As Type = Type.GetTypeFromProgID("esriDataSourcesGDB.AccessWorkspaceFactory")
        Dim obj As System.Object = Activator.CreateInstance(t)
        Dim workspaceFactory As IWorkspaceFactory2 = obj
        Return workspaceFactory.OpenFromFile(connStr, 0)
    End Function
End Module
