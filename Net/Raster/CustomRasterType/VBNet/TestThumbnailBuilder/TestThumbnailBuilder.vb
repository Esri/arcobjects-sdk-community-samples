Imports System.Collections.Generic
Imports System.Globalization
Imports System.Runtime.InteropServices
Imports System.Text
Imports System.Threading
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.DataSourcesGDB
Imports ESRI.ArcGIS.DataSourcesRaster
Imports ThumbnailBuilder.CustomRasterBuilder

Namespace RasterTest
	Public Class ThumbnailBuilderTest
		<STAThread> _
		Public Shared Sub Main(args As String())
			Dim aoInit As ESRI.ArcGIS.esriSystem.AoInitialize

			'#Region "Initialize Licensing"
			Try
				Console.WriteLine("Obtaining License")
				ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.Desktop)
				aoInit = New AoInitializeClass()
				Dim licStatus As esriLicenseStatus = aoInit.Initialize(esriLicenseProductCode.esriLicenseProductCodeAdvanced)
				Console.WriteLine("Ready with license")
			Catch exc As Exception

				' If it fails at this point, shutdown the test and ignore any subsequent errors.
				Console.WriteLine(exc.Message)
				Return
			End Try
			'#End Region

			Try
				'#Region "Specify input directory and dataset name"
				' Specify where to create the File Gdb
				Dim fgdbFolder As String = "c:\temp\CustomRasterType"
				' Specify the folder to add the data from
                Dim dataSource As String = "c:\data\RasterDatasets"
                '  Specify whether to DELETE the fgdbFolder and create it again (clearing it).
                Dim clearFGdbFolder As Boolean = False

				' Specify whether to save the Custom Raster Type generated to an art file.
				Dim saveToArtFile As Boolean = False
				' Specify the path and filename to save the custom type.
				Dim customTypeFilePath As String = "C:\temp\ThumbnailType.art"
				'#End Region

				'#Region "Raster Type Parameters"
                Dim rasterTypeName As String = "Thumbnail Raster Dataset"
				' Specify the file filter to use to add data (Optional)
				Dim dataSourceFilter As String = "*.tif"
				Dim rasterTypeProductFilter As String = ""
				Dim rasterTypeProductName As String = ""
				'#End Region

				TestThumbnailBuilder(rasterTypeName, rasterTypeProductFilter, rasterTypeProductName, dataSource, dataSourceFilter, fgdbFolder, _
					saveToArtFile, customTypeFilePath, clearFGdbFolder)

				'#Region "Shutdown"
				Console.WriteLine("Press any key...")
				Console.ReadKey()
					'#End Region
				aoInit.Shutdown()
			Catch exc As Exception
				'#Region "Shutdown"
				Console.WriteLine("Exception Caught in Main: " & exc.Message)
				Console.WriteLine("Failed.")
				Console.WriteLine("Shutting down.")
				Console.WriteLine("Press any key...")
				Console.ReadKey()

				' Shutdown License
					'#End Region
				aoInit.Shutdown()
			End Try
		End Sub

		Public Shared Sub TestThumbnailBuilder(rasterTypeName As String, rasterTypeProductFilter As String, rasterTypeProductName As String, dataSource As String, dataSourceFilter As String, fgdbParentFolder As String, _
			saveToArt As Boolean, customTypeFilePath As String, clearGdbDirectory As Boolean)
			Try
				Dim rasterProductNames As String() = rasterTypeProductName.Split(";"C)
				Dim nameString As String = rasterTypeName.Replace(" ", "") & rasterTypeProductFilter.Replace(" ", "") & rasterProductNames(0).Replace(" ", "")

				'#Region "Directory Declarations"
				Dim fgdbName As String = nameString & ".gdb"
				Dim fgdbDir As String = fgdbParentFolder & "\" & fgdbName
				Dim MosaicDatasetName As String = nameString & "MD"
				'#End Region

				'#Region "Global Declarations"
				Dim theMosaicDataset As IMosaicDataset = Nothing
				Dim theMosaicDatasetOperation As IMosaicDatasetOperation = Nothing
				Dim mosaicExtHelper As IMosaicWorkspaceExtensionHelper = Nothing
				Dim mosaicExt As IMosaicWorkspaceExtension = Nothing
				'#End Region

				'#Region "Create File GDB"
				Console.WriteLine("Creating File GDB: " & fgdbName)
				If clearGdbDirectory Then
					Try
						Console.WriteLine("Emptying Gdb folder.")
						System.IO.Directory.Delete(fgdbParentFolder, True)
						System.IO.Directory.CreateDirectory(fgdbParentFolder)
					Catch EX As System.IO.IOException
                        Console.WriteLine(EX.Message)
						Return
					End Try
				End If

				' Create a File Gdb
				Dim factoryType As Type = Type.GetTypeFromProgID("esriDataSourcesGDB.FileGDBWorkspaceFactory")
				Dim FgdbFactory As IWorkspaceFactory = DirectCast(Activator.CreateInstance(factoryType), IWorkspaceFactory)
				FgdbFactory.Create(fgdbParentFolder, fgdbName, Nothing, 0)
				'#End Region

				'#Region "Create Mosaic Dataset"
				Try
					Console.WriteLine("Create Mosaic Dataset: " & MosaicDatasetName)
					' Setup workspaces.
					Dim workspaceFactory As IWorkspaceFactory = DirectCast(Activator.CreateInstance(factoryType), IWorkspaceFactory)
					Dim fgdbWorkspace As IWorkspace = workspaceFactory.OpenFromFile(fgdbDir, 0)
					' Create Srs
					Dim spatialrefFactory As ISpatialReferenceFactory = New SpatialReferenceEnvironmentClass()
					Dim mosaicSrs As ISpatialReference = spatialrefFactory.CreateProjectedCoordinateSystem(CInt(esriSRProjCSType.esriSRProjCS_World_Mercator))
					' Create the mosaic dataset creation parameters object.
					Dim creationPars As ICreateMosaicDatasetParameters = New CreateMosaicDatasetParametersClass()
					' Create the mosaic workspace extension helper class.
					mosaicExtHelper = New MosaicWorkspaceExtensionHelperClass()
					' Find the right extension from the workspace.
					mosaicExt = mosaicExtHelper.FindExtension(fgdbWorkspace)
					' Use the extension to create a new mosaic dataset, supplying the 
					' spatial reference and the creation parameters object created above.
					theMosaicDataset = mosaicExt.CreateMosaicDataset(MosaicDatasetName, mosaicSrs, creationPars, "")
					theMosaicDatasetOperation = DirectCast(theMosaicDataset, IMosaicDatasetOperation)
				Catch exc As Exception
					Console.WriteLine("Error: Failed to create Mosaic Dataset : {0}.", MosaicDatasetName & " " & exc.Message)
					Return
				End Try
				'#End Region

				'#Region "Create Custom Raster Type"
				Console.WriteLine("Preparing Raster Type")
				' Create a Raster Type Name object.
				Dim theRasterTypeName As IRasterTypeName = New RasterTypeNameClass()
				' Assign the name of the Raster Type to the name object.
				' The Name field accepts a path to an .art file as well 
				' the name for a built in Raster Type.
				theRasterTypeName.Name = rasterTypeName
				' Use the Open function from the IName interface to get the Raster Type object.
				Dim theRasterType As IRasterType = DirectCast(DirectCast(theRasterTypeName, IName).Open(), IRasterType)
				If theRasterType Is Nothing Then
					Console.WriteLine("Error:Raster Type not found " & rasterTypeName)
					Return
				End If
                '#End Region

				'#Region "Prepare Raster Type"
				' Set the URI Filter on the loaded raster type.
				If rasterTypeProductFilter <> "" Then
					' Get the supported URI filters from the raster type object using the 
					' raster type properties interface.
					Dim mySuppFilters As IArray = DirectCast(theRasterType, IRasterTypeProperties).SupportedURIFilters
					Dim productFilter As IItemURIFilter = Nothing
					For i As Integer = 0 To mySuppFilters.Count - 1
						' Set the desired filter from the supported filters.
                        productFilter = DirectCast(mySuppFilters.Element(i), IItemURIFilter)
						If productFilter.Name = rasterTypeProductFilter Then
							theRasterType.URIFilter = productFilter
						End If
					Next
				End If
				' Enable the correct templates in the raster type.
				Dim enableTemplate As Boolean = False
				If rasterProductNames.Length >= 1 AndAlso (rasterProductNames(0) <> "") Then
					' Get the supported item templates from the raster type.
					Dim templateArray As IItemTemplateArray = theRasterType.ItemTemplates
					For i As Integer = 0 To templateArray.Count - 1
						' Go through the supported item templates and enable the ones needed.
                        Dim template As IItemTemplate = templateArray.Element(i)
						enableTemplate = False
						For j As Integer = 0 To rasterProductNames.Length - 1
							If template.Name = rasterProductNames(j) Then
								enableTemplate = True
							End If
						Next
						If enableTemplate Then
							template.Enabled = True
						Else
							template.Enabled = False
						End If
					Next
				End If
				DirectCast(theRasterType, IRasterTypeProperties).DataSourceFilter = dataSourceFilter
				'#End Region

                '#Region "Save Custom Raster Type"
				If saveToArt Then
					Dim rasterTypeProperties As IRasterTypeProperties = DirectCast(theRasterType, IRasterTypeProperties)
					Dim rasterTypeHelper As IRasterTypeEnvironment = New RasterTypeEnvironmentClass()
					rasterTypeProperties.Name = customTypeFilePath

					Dim ipBlob As IMemoryBlobStream = rasterTypeHelper.SaveRasterType(theRasterType)
					ipBlob.SaveToFile(customTypeFilePath)
				End If
				'#End Region

				'#Region "Preparing Data Source Crawler"
				Console.WriteLine("Preparing Data Source Crawler")
				' Create a new property set to specify crawler properties.
				Dim crawlerProps As IPropertySet = New PropertySetClass()
				' Specify a file filter
				crawlerProps.SetProperty("Filter", dataSourceFilter)
				' Specify whether to search subdirectories.
				crawlerProps.SetProperty("Recurse", True)
				' Specify the source path.
				crawlerProps.SetProperty("Source", dataSource)
				' Get the recommended crawler from the raster type based on the specified 
				' properties using the IRasterBuilder interface.
				' Pass on the Thumbnailtype to the crawler...
				Dim theCrawler As IDataSourceCrawler = DirectCast(theRasterType, IRasterBuilder).GetRecommendedCrawler(crawlerProps)
				'#End Region

				'#Region "Add Rasters"
				Try
					Console.WriteLine("Adding Rasters")
					' Create a AddRaster parameters object.
					Dim AddRastersArgs As IAddRastersParameters = New AddRastersParametersClass()
					' Specify the data crawler to be used to crawl the data.
					AddRastersArgs.Crawler = theCrawler
					' Specify the Thumbnail raster type to be used to add the data.
					AddRastersArgs.RasterType = theRasterType
					' Use the mosaic dataset operation interface to add 
					' rasters to the mosaic dataset.
					theMosaicDatasetOperation.AddRasters(AddRastersArgs, Nothing)
				Catch ex As Exception
                    Console.WriteLine("Error: Add raster Failed." & ex.Message)
					Return
				End Try
				'#End Region

				'#Region "Compute Pixel Size Ranges"
				Console.WriteLine("Computing Pixel Size Ranges.")
				Try
					' Create a calculate cellsize ranges parameters object.
					Dim computeArgs As ICalculateCellSizeRangesParameters = New CalculateCellSizeRangesParametersClass()
					' Use the mosaic dataset operation interface to calculate cellsize ranges.
					theMosaicDatasetOperation.CalculateCellSizeRanges(computeArgs, Nothing)
				Catch ex As Exception
                    Console.WriteLine("Error: Compute Pixel Size Failed." & ex.Message)
					Return
				End Try
				'#End Region

				'#Region "Building Boundary"
				Console.WriteLine("Building Boundary")
				Try
					' Create a build boundary parameters object.
					Dim boundaryArgs As IBuildBoundaryParameters = New BuildBoundaryParametersClass()
					' Set flags that control boundary generation.
					boundaryArgs.AppendToExistingBoundary = True
					' Use the mosaic dataset operation interface to build boundary.
					theMosaicDatasetOperation.BuildBoundary(boundaryArgs, Nothing)
				Catch ex As Exception
                    Console.WriteLine("Error: Build Boundary Failed." & ex.Message)
					Return
				End Try
				'#End Region

				'#Region "Report"
					'#End Region
				Console.WriteLine("Successfully created MD: " & MosaicDatasetName & ". ")
			Catch exc As Exception
				'#Region "Report"
				Console.WriteLine("Exception Caught in TestThumbnailBuilder: " & exc.Message)
				Console.WriteLine("Failed.")
					'#End Region
				Console.WriteLine("Shutting down.")
			End Try
		End Sub
	End Class
End Namespace
