'Copyright 2019 Esri

'Licensed under the Apache License, Version 2.0 (the "License");
'you may not use this file except in compliance with the License.
'You may obtain a copy of the License at

'    http://www.apache.org/licenses/LICENSE-2.0

'Unless required by applicable law or agreed to in writing, software
'distributed under the License is distributed on an "AS IS" BASIS,
'WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.

'See the License for the specific language governing permissions and
'limitations under the License.
Imports System.IO
Imports ESRI.ArcGIS
Imports ESRI.ArcGIS.DataSourcesGDB
Imports ESRI.ArcGIS.DataSourcesRaster
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Geometry

'
' * 
' * This test application is an optional test application to create a mosaic dataset 
' * using the DMCII custom raster type and add data to it from the source specified 
' * by the user. The user can also change properties like product temaplates and 
' * product filters and choose whether to build overviews and what geodatabase to 
' * create the mosaic dataset in. 
' * Usage:
' * Change the properties under the 'Setup MD Parameters' region to control what 
' * geodatabase to create, where to create it, its name and the name of the md.
' * Specify where to add the data from.
' * Choose whether to empty and or create the output directory (gdb parent folder).
' * Run the application. The console will show detailed messaging while the 
' * application runs, including any errors that occur.
' * 
' 


Class TestDMCIIRasterType
	<STAThread> _
	Friend Shared Sub Main(args As String())
		'#Region "Initialize License"
		Dim aoInit As ESRI.ArcGIS.esriSystem.AoInitialize = Nothing
		Try
			Console.WriteLine("Obtaining license")
			ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.Desktop)
			aoInit = New AoInitializeClass()
			Dim licStatus As esriLicenseStatus = aoInit.Initialize(esriLicenseProductCode.esriLicenseProductCodeAdvanced)
			Console.WriteLine("Ready with license.")
		Catch exc As Exception
			' If it fails at this point, shutdown the test and ignore any subsequent errors.
			Console.WriteLine(exc.Message)
		End Try
		'#End Region

		Try
			'#Region "Setup MD Parameters"
			MDParameters.gdbParentFolder = "e:\MD\CustomRasterType\DMCII"
			' Choose which type of gdb to create/open.
			' 0 - Create File Gdb
			' 1 - Create Personal Gdb
			' 2 - Open SDE
			Dim gdbOption As Integer = 0
			' Provide the proper extension based on the gdb you want to create. 
			' e.g. MDParameters.gdbName = "samplePGdb.mdb" to create a personal gdb.
			' To use an SDE, set SDE connection properties below.
			MDParameters.gdbName = "CustomTypeGdb.gdb"
			MDParameters.mosaicDatasetName = "CustomTypeMD"

			' Specify the srs of the mosaic dataset
			Dim spatialrefFactory As ISpatialReferenceFactory = New SpatialReferenceEnvironmentClass()
			MDParameters.mosaicDatasetSrs = spatialrefFactory.CreateProjectedCoordinateSystem(CInt(esriSRProjCSType.esriSRProjCS_World_Mercator))

			' 0 and PT_UNKNOWN for bits and bands = use defaults.
			MDParameters.mosaicDatasetBands = 0
			MDParameters.mosaicDatasetBits = rstPixelType.PT_UNKNOWN
			MDParameters.configKeyword = ""

			' Product Definition key choices:
			' None
			' NATURAL_COLOR_RGB
			' NATURAL_COLOR_RGBI
			' FALSE_COLOR_IRG
			' FORMOSAT-2_4BANDS
			' GEOEYE-1_4BANDS
			' IKONOS_4BANDS
			' KOMPSAT-2_4BANDS
			' LANDSAT_6BANDS
			' LANDSAT_MSS_4BANDS
			' QUICKBIRD_4BANDS
			' RAPIDEYE_5BANDS
			' SPOT-5_4BANDS
			' WORLDVIEW-2_8BANDS

			' Setting this property ensures any data added to the MD with its
			' metadata defined gets added with the correct band combination.
			MDParameters.productDefinitionKey = "FALSE_COLOR_IRG"

			MDParameters.rasterTypeName = "DMCIIRasterType"
			' The next two properties can be left blank for defaults
			' The product filter defines which specific product of the raster
			' type to add, e.g. To specfiy Quickbird Basic use value "Basic"
			MDParameters.rasterTypeProductFilter = ""
			' The product name specifies which template to use when adding data.
			' e.g. "Pansharpen and Multispectral" means both multispectral and 
			' pansharpened rasters are added to the mosaic dataset.
			MDParameters.rasterTypeProductName = "Raw"

			' Data source from which to read the data.
			MDParameters.dataSource = "F:\Data\DMCii"
			MDParameters.dataSourceFilter = ""
			' No need to set if data source has an srs or if you want to use the MD srs as data source srs.
			MDParameters.dataSourceSrs = Nothing

			MDParameters.buildOverviews = False
			'#End Region

			MDParameters.emptyGdbFolder = False
			MDParameters.createGdbParentFolder = False
			'#Region "Empty/Create Output Directory"
			If MDParameters.emptyGdbFolder Then
				Try
					Console.WriteLine("Emptying Output Directory")
					Directory.Delete(MDParameters.gdbParentFolder, True)
					Directory.CreateDirectory(MDParameters.gdbParentFolder)
				Catch generatedExceptionName As Exception
				End Try
			End If
			If MDParameters.createGdbParentFolder AndAlso Not System.IO.Directory.Exists(MDParameters.gdbParentFolder) Then
				Console.WriteLine("Creating Output Directory")
				Directory.CreateDirectory(MDParameters.gdbParentFolder)
			End If
			'#End Region

			Dim createMD As New CreateMD()

			If gdbOption = 0 Then
				'#Region "Create MD in File GDB"
				Console.WriteLine("Creating File GDB: " & MDParameters.gdbName)
				Dim fgdbWorkspace As IWorkspace = CreateFileGdbWorkspace(MDParameters.gdbParentFolder, MDParameters.gdbName)
					'#End Region
				createMD.CreateMosaicDataset(fgdbWorkspace)
			ElseIf gdbOption = 1 Then
				'#Region "Create MD in Personal GDB"
				Console.WriteLine("Creating Personal GDB: " & MDParameters.gdbName)
				Dim pGdbWorkspace As IWorkspace = CreateAccessWorkspace(MDParameters.gdbParentFolder, MDParameters.gdbName)
					'#End Region
				createMD.CreateMosaicDataset(pGdbWorkspace)
			ElseIf gdbOption = 2 Then
				'#Region "Open SDE GDB"
				' Set SDE connection properties.
				Dim sdeProperties As IPropertySet = New PropertySetClass()
				sdeProperties.SetProperty("SERVER", "barbados")
				sdeProperties.SetProperty("INSTANCE", "9411")
				sdeProperties.SetProperty("VERSION", "sde.DEFAULT")
				sdeProperties.SetProperty("USER", "gdb")
				sdeProperties.SetProperty("PASSWORD", "gdb")
				sdeProperties.SetProperty("DATABASE", "VTEST")
				Dim sdeWorkspace As IWorkspace = CreateSdeWorkspace(sdeProperties)
				If sdeWorkspace Is Nothing Then
					Console.WriteLine("Could not open SDE workspace: ")
					Return
				End If

				'#End Region

				'#Region "Create MD in SDE"
				MDParameters.mosaicDatasetName = "sampleMD"
					'#End Region
				createMD.CreateMosaicDataset(sdeWorkspace)
			End If

			'#Region "Shutdown"
			Console.WriteLine("Press any key...")
			Console.ReadKey()
			' Shutdown License
				'#End Region
			aoInit.Shutdown()
		Catch exc As Exception
			'#Region "Report"
			Console.WriteLine("Exception Caught in Main: " & exc.Message)
			Console.WriteLine("Shutting down.")
			'#End Region

			'#Region "Shutdown"
			Console.WriteLine("Press any key...")
			Console.ReadKey()
			' Shutdown License
				'#End Region
			aoInit.Shutdown()
		End Try
	End Sub

	''' <summary>
	''' Create a File Geodatabase given the name and parent folder.
	''' </summary>
	''' <param name="gdbParentFolder">Folder to create the new gdb in.</param>
	''' <param name="gdbName">Name of the gdb to be created.</param>
	''' <returns>Workspace reference to the new geodatabase.</returns>
	Public Shared Function CreateFileGdbWorkspace(gdbParentFolder As String, gdbName As String) As IWorkspace
		' Instantiate a file geodatabase workspace factory and create a file geodatabase.
		' The Create method returns a workspace object.
		Dim factoryType As Type = Type.GetTypeFromProgID("esriDataSourcesGDB.FileGDBWorkspaceFactory")
		Dim workspaceFactory As IWorkspaceFactory = DirectCast(Activator.CreateInstance(factoryType), IWorkspaceFactory)
		Dim workspaceName As IWorkspaceName = workspaceFactory.Create(gdbParentFolder, gdbName, Nothing, 0)

		' Cast the workspace name object to the IName interface and open the workspace.
		Dim name As IName = DirectCast(workspaceName, IName)
		Dim workspace As IWorkspace = DirectCast(name.Open(), IWorkspace)
		Return workspace
	End Function

	''' <summary>
	''' Create a Personal Geodatabase given the name and parent folder.
	''' </summary>
	''' <param name="gdbParentFolder">Folder to create the new gdb in.</param>
	''' <param name="gdbName">Name of the gdb to be created.</param>
	''' <returns>Workspace reference to the new geodatabase.</returns>
	Public Shared Function CreateAccessWorkspace(gdbParentFolder As String, gdbName As String) As IWorkspace
		' Instantiate an Access workspace factory and create a personal geodatabase.
		' The Create method returns a workspace object.
		Dim factoryType As Type = Type.GetTypeFromProgID("esriDataSourcesGDB.AccessWorkspaceFactory")
		Dim workspaceFactory As IWorkspaceFactory = DirectCast(Activator.CreateInstance(factoryType), IWorkspaceFactory)
		Dim workspaceName As IWorkspaceName = workspaceFactory.Create(gdbParentFolder, gdbName, Nothing, 0)

		' Cast the workspace name object to the IName interface and open the workspace.
		Dim name As IName = DirectCast(workspaceName, IName)
		Dim workspace As IWorkspace = DirectCast(name.Open(), IWorkspace)
		Return workspace
	End Function

	''' <summary>
	''' Retrieves an SDE workspace using the specified property set.
	''' </summary>
	''' <param name="propertySet">The connection parameters.</param>
	''' <returns>An IWorkspace reference to an SDE workspace.</returns>
	Public Shared Function CreateSdeWorkspace(propertySet As IPropertySet) As IWorkspace
		' Create the workspace factory and connect to the workspace.
		Dim factoryType As Type = Type.GetTypeFromProgID("esriDataSourcesGDB.SdeWorkspaceFactory")
		Dim workspaceFactory As IWorkspaceFactory = DirectCast(Activator.CreateInstance(factoryType), IWorkspaceFactory)
		Dim workspace As IWorkspace = workspaceFactory.Open(propertySet, 0)
		Return workspace
	End Function

End Class

Public NotInheritable Class MDParameters
	Private Sub New()
	End Sub
	' Define the folder in which the GDB resides or is to be created
	Public Shared gdbParentFolder As String
	' Name of the GDB
	Public Shared gdbName As String
	' Configuration keyword for the Gdb (optional)
	Public Shared configKeyword As String

	' Mosaic Dataset Properties
	Public Shared mosaicDatasetName As String
	' Name of the Mosaic Dataset to create.
	Public Shared mosaicDatasetSrs As ISpatialReference
	' Srs for the Mosaic Dataset
	Public Shared mosaicDatasetBands As Integer
	' Number of bands of the Mosaic Dataset
	Public Shared mosaicDatasetBits As rstPixelType
	' Pixel Type of the Mosaic Dataset
	Public Shared productDefinitionKey As String
	' The product definition key.
	Public Shared productDefinitionProps As IArray
	' Properties of the product definition (Bands names and wavelengths).
	' Raster Type Properties
	Public Shared rasterTypeName As String
	' Name of the Raster type to use (or path to the .art file)
	Public Shared rasterTypeProductFilter As String
	' The product filter to set on the Raster Type
	Public Shared rasterTypeProductName As String
	' The name of the product to create from the added data
	Public Shared rasterTypeAddDEM As Boolean
	' Flag to specify whether to add a DEM to a Raster Type.
	Public Shared rasterTypeDemPath As String
	' Path to the DEM if previous property is true.
	' Crawler Properties
	Public Shared dataSource As String
	' Path to the data.
	Public Shared dataSourceFilter As String
	' File filter to use to crawl data.
	' Srs of the input data if the input does not contain an srs 
	' and it is different from the srs of the mosaic dataset(optional)
	Public Shared dataSourceSrs As ISpatialReference

	' Operational flags
	Public Shared buildOverviews As Boolean
	' Generate overviews for the Mosaic Dataset
	Public Shared emptyGdbFolder As Boolean
	' Delete the parent folder for the GDB
	Public Shared createGdbParentFolder As Boolean
	' Create the Parent folder for the GDB
End Class

Public Class CreateMD
	''' <summary>
	''' Create a Mosaic Dataset in the geodatabase provided using the parameters defined by MDParamaters.
	''' </summary>
	''' <param name="gdbWorkspace">Geodatabase to create the Mosaic dataser in.</param>
	Public Sub CreateMosaicDataset(gdbWorkspace As IWorkspace)
		Try
			'#Region "Global Declarations"
			Dim theMosaicDataset As IMosaicDataset = Nothing
			Dim theMosaicDatasetOperation As IMosaicDatasetOperation = Nothing
			Dim mosaicExtHelper As IMosaicWorkspaceExtensionHelper = Nothing
			Dim mosaicExt As IMosaicWorkspaceExtension = Nothing
			'#End Region

			'#Region "CreateMosaicDataset"
			Try
				Console.WriteLine("Create Mosaic Dataset: " & MDParameters.mosaicDatasetName & ".amd")
				''' Setup workspaces.
				''' Create Srs
				Dim spatialrefFactory As ISpatialReferenceFactory = New SpatialReferenceEnvironmentClass()

				' Create the mosaic dataset creation parameters object.
				Dim creationPars As ICreateMosaicDatasetParameters = New CreateMosaicDatasetParametersClass()
				' Set the number of bands for the mosaic dataset.
				' If defined as zero leave defaults
				If MDParameters.mosaicDatasetBands <> 0 Then
					creationPars.BandCount = MDParameters.mosaicDatasetBands
				End If
				' Set the pixel type of the mosaic dataset.
				' If defined as unknown leave defaults
				If MDParameters.mosaicDatasetBits <> rstPixelType.PT_UNKNOWN Then
					creationPars.PixelType = MDParameters.mosaicDatasetBits
				End If
				' Create the mosaic workspace extension helper class.
				mosaicExtHelper = New MosaicWorkspaceExtensionHelperClass()
				' Find the right extension from the workspace.
				mosaicExt = mosaicExtHelper.FindExtension(gdbWorkspace)

				' Default is none.
				If MDParameters.productDefinitionKey.ToLower() <> "none" Then
					' Set the product definition keyword and properties.
					' (The property is called band definition keyword and band properties in the object).
					DirectCast(creationPars, ICreateMosaicDatasetParameters2).BandDefinitionKeyword = MDParameters.productDefinitionKey
					MDParameters.productDefinitionProps = SetBandProperties(MDParameters.productDefinitionKey)
					If MDParameters.productDefinitionProps.Count = 0 Then
						Console.WriteLine("Setting production definition properties failed.")
						Return
					End If
					DirectCast(creationPars, ICreateMosaicDatasetParameters2).BandProperties = MDParameters.productDefinitionProps
				End If

				' Use the extension to create a new mosaic dataset, supplying the 
				' spatial reference and the creation parameters object created above.
				theMosaicDataset = mosaicExt.CreateMosaicDataset(MDParameters.mosaicDatasetName, MDParameters.mosaicDatasetSrs, creationPars, MDParameters.configKeyword)
			Catch exc As Exception
				Console.WriteLine("Exception Caught while creating Mosaic Dataset: " & exc.Message)
				Return
			End Try
			'#End Region

			'#Region "OpenMosaicDataset"
			Console.WriteLine("Opening Mosaic Dataset")
			theMosaicDataset = Nothing
			' Use the extension to open the mosaic dataset.
			theMosaicDataset = mosaicExt.OpenMosaicDataset(MDParameters.mosaicDatasetName)
			' The mosaic dataset operation interface is used to perform operations on 
			' a mosaic dataset.
			theMosaicDatasetOperation = DirectCast(theMosaicDataset, IMosaicDatasetOperation)
			'#End Region

			'#Region "Preparing Raster Type"
			Console.WriteLine("Preparing Raster Type")
			' Create a Raster Type Name object.
			Dim theRasterTypeName As IRasterTypeName = New RasterTypeNameClass()
			' Assign the name of the Raster Type to the name object.
			' The Name field accepts a path to an .art file as well 
			' the name for a built in Raster Type.
			theRasterTypeName.Name = MDParameters.rasterTypeName
			' Use the Open function from the IName interface to get the Raster Type object.
			Dim theRasterType As IRasterType = DirectCast(DirectCast(theRasterTypeName, IName).Open(), IRasterType)
			If theRasterType Is Nothing Then
				Console.WriteLine("Raster Type not found " & MDParameters.rasterTypeName)
			End If

			' Set the URI Filter on the loaded raster type.
			If MDParameters.rasterTypeProductFilter <> "" Then
				' Get the supported URI filters from the raster type object using the 
				' raster type properties interface.
				Dim mySuppFilters As IArray = DirectCast(theRasterType, IRasterTypeProperties).SupportedURIFilters
				Dim productFilter As IItemURIFilter = Nothing
				For i As Integer = 0 To mySuppFilters.Count - 1
					' Set the desired filter from the supported filters.
                    productFilter = DirectCast(mySuppFilters.Element(i), IItemURIFilter)
					If productFilter.Name = MDParameters.rasterTypeProductFilter Then
						theRasterType.URIFilter = productFilter
					End If
				Next
			End If
			' Enable the correct templates in the raster type.
			Dim rasterProductNames As String() = MDParameters.rasterTypeProductName.Split(";"C)
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

			If MDParameters.dataSourceSrs IsNot Nothing Then
				DirectCast(theRasterType, IRasterTypeProperties).SynchronizeParameters.DefaultSpatialReference = MDParameters.dataSourceSrs
			End If
			'#End Region

			'#Region "Add DEM To Raster Type"
			If MDParameters.rasterTypeAddDEM AndAlso DirectCast(theRasterType, IRasterTypeProperties).SupportsOrthorectification Then
				' Open the Raster Dataset
				Dim factoryType As Type = Type.GetTypeFromProgID("esriDataSourcesRaster.RasterWorkspaceFactory")
				Dim workspaceFactory As IWorkspaceFactory = DirectCast(Activator.CreateInstance(factoryType), IWorkspaceFactory)
				Dim rasterWorkspace As IRasterWorkspace = DirectCast(workspaceFactory.OpenFromFile(System.IO.Path.GetDirectoryName(MDParameters.rasterTypeDemPath), 0), IRasterWorkspace)
				

				Dim myRasterDataset As IRasterDataset = rasterWorkspace.OpenRasterDataset(System.IO.Path.GetFileName(MDParameters.rasterTypeDemPath))

				Dim geometricFunctionArguments As IGeometricFunctionArguments = New GeometricFunctionArgumentsClass()
				geometricFunctionArguments.DEM = myRasterDataset
				DirectCast(theRasterType, IRasterTypeProperties).OrthorectificationParameters = geometricFunctionArguments
			End If
			'#End Region

			'#Region "Preparing Data Source Crawler"
			Console.WriteLine("Preparing Data Source Crawler")
			' Create a new property set to specify crawler properties.
			Dim crawlerProps As IPropertySet = New PropertySetClass()
			' Specify a file filter
			crawlerProps.SetProperty("Filter", MDParameters.dataSourceFilter)
			' Specify whether to search subdirectories.
			crawlerProps.SetProperty("Recurse", True)
			' Specify the source path.
			crawlerProps.SetProperty("Source", MDParameters.dataSource)
			' Get the recommended crawler from the raster type based on the specified 
			' properties using the IRasterBuilder interface.
			Dim theCrawler As IDataSourceCrawler = DirectCast(theRasterType, IRasterBuilder).GetRecommendedCrawler(crawlerProps)
			'#End Region

			'#Region "Add Rasters"
			Console.WriteLine("Adding Rasters")
			' Create a AddRaster parameters object.
			Dim AddRastersArgs As IAddRastersParameters = New AddRastersParametersClass()
			' Specify the data crawler to be used to crawl the data.
			AddRastersArgs.Crawler = theCrawler
			' Specify the raster type to be used to add the data.
			AddRastersArgs.RasterType = theRasterType
			' Use the mosaic dataset operation interface to add 
			' rasters to the mosaic dataset.
			theMosaicDatasetOperation.AddRasters(AddRastersArgs, Nothing)
			'#End Region

			'#Region "Compute Pixel Size Ranges"
			Console.WriteLine("Computing Pixel Size Ranges")
			' Create a calculate cellsize ranges parameters object.
			Dim computeArgs As ICalculateCellSizeRangesParameters = New CalculateCellSizeRangesParametersClass()
			' Use the mosaic dataset operation interface to calculate cellsize ranges.
			theMosaicDatasetOperation.CalculateCellSizeRanges(computeArgs, Nothing)
			'#End Region

			'#Region "Building Boundary"
			Console.WriteLine("Building Boundary")
			' Create a build boundary parameters object.
			Dim boundaryArgs As IBuildBoundaryParameters = New BuildBoundaryParametersClass()
			' Set flags that control boundary generation.
			boundaryArgs.AppendToExistingBoundary = True
			' Use the mosaic dataset operation interface to build boundary.
			theMosaicDatasetOperation.BuildBoundary(boundaryArgs, Nothing)
			'#End Region

			If MDParameters.buildOverviews Then
				'#Region "Defining Overviews"
				Console.WriteLine("Defining Overviews")
				' Create a define overview parameters object.
				Dim defineOvArgs As IDefineOverviewsParameters = New DefineOverviewsParametersClass()
				' Use the overview tile parameters interface to specify the overview factor
				' used to generate overviews.
				DirectCast(defineOvArgs, IOverviewTileParameters).OverviewFactor = 3
				' Use the mosaic dataset operation interface to define overviews.
				theMosaicDatasetOperation.DefineOverviews(defineOvArgs, Nothing)
				'#End Region

				'#Region "Compute Pixel Size Ranges"
				Console.WriteLine("Computing Pixel Size Ranges")
				' Calculate cell size ranges to update the Min/Max pixel sizes.
				theMosaicDatasetOperation.CalculateCellSizeRanges(computeArgs, Nothing)
				'#End Region

				'#Region "Generating Overviews"
				Console.WriteLine("Generating Overviews")
				' Create a generate overviews parameters object.
				Dim genPars As IGenerateOverviewsParameters = New GenerateOverviewsParametersClass()
				' Set properties to control overview generation.
				Dim genQuery As IQueryFilter = New QueryFilterClass()
				DirectCast(genPars, ISelectionParameters).QueryFilter = genQuery
				genPars.GenerateMissingImages = True
				genPars.GenerateStaleImages = True
				' Use the mosaic dataset operation interface to generate overviews.
					'#End Region
				theMosaicDatasetOperation.GenerateOverviews(genPars, Nothing)
			End If

			'#Region "Report"
				'#End Region
			Console.WriteLine("Success.")
		Catch exc As Exception
			'#Region "Report"
			Console.WriteLine("Exception Caught in CreateMD: " & exc.Message)
				'#End Region
			Console.WriteLine("Shutting down.")
		End Try
	End Sub

	''' <summary>
	''' Create an array with the right BandName and Wavelength values for the corresponding key. 
	''' </summary>
	''' <param name="key">Key to use.</param>
	''' <returns>Array with the correct BandName and Wavelength values.</returns>
	Private Shared Function SetBandProperties(key As String) As IArray
		Dim productDefProps As IArray = New ArrayClass()
		Dim band1Def As IPropertySet = New PropertySetClass()
		Dim band2Def As IPropertySet = New PropertySetClass()
		Dim band3Def As IPropertySet = New PropertySetClass()
		If key = "NATURAL_COLOR_RGB" OrElse key = "NATURAL_COLOR_RGBI" Then
			band1Def.SetProperty("BandName", "Red")
			band1Def.SetProperty("WavelengthMin", 630)
			band1Def.SetProperty("WavelengthMax", 690)

			band2Def.SetProperty("BandName", "Green")
			band2Def.SetProperty("WavelengthMin", 530)
			band2Def.SetProperty("WavelengthMax", 570)

			band3Def.SetProperty("BandName", "Blue")
			band3Def.SetProperty("WavelengthMin", 440)
			band3Def.SetProperty("WavelengthMax", 480)

			productDefProps.Add(band1Def)
			productDefProps.Add(band2Def)
			productDefProps.Add(band3Def)

			If key = "NATURAL_COLOR_RGBI" Then
				Dim band4Def As IPropertySet = New PropertySetClass()
				band4Def.SetProperty("BandName", "NearInfrared")
				band4Def.SetProperty("WavelengthMin", 770)
				band4Def.SetProperty("WavelengthMax", 830)
				productDefProps.Add(band4Def)
			End If
		ElseIf key = "FALSE_COLOR_IRG" Then
			band1Def.SetProperty("BandName", "Infrared")
			band1Def.SetProperty("WavelengthMin", 770)
			band1Def.SetProperty("WavelengthMax", 830)

			band2Def.SetProperty("BandName", "Red")
			band2Def.SetProperty("WavelengthMin", 630)
			band2Def.SetProperty("WavelengthMax", 690)

			band3Def.SetProperty("BandName", "Green")
			band3Def.SetProperty("WavelengthMin", 530)
			band3Def.SetProperty("WavelengthMax", 570)

			productDefProps.Add(band1Def)
			productDefProps.Add(band2Def)
			productDefProps.Add(band3Def)
		ElseIf key = "FORMOSAT-2_4BANDS" Then
			Dim band4Def As IPropertySet = New PropertySetClass()

			band1Def.SetProperty("BandName", "Blue")
			band1Def.SetProperty("WavelengthMin", 450)
			band1Def.SetProperty("WavelengthMax", 520)

			band2Def.SetProperty("BandName", "Green")
			band2Def.SetProperty("WavelengthMin", 520)
			band2Def.SetProperty("WavelengthMax", 600)

			band3Def.SetProperty("BandName", "Red")
			band3Def.SetProperty("WavelengthMin", 630)
			band3Def.SetProperty("WavelengthMax", 690)

			band4Def.SetProperty("BandName", "NearInfrared")
			band4Def.SetProperty("WavelengthMin", 760)
			band4Def.SetProperty("WavelengthMax", 900)

			productDefProps.Add(band1Def)
			productDefProps.Add(band2Def)
			productDefProps.Add(band3Def)
			productDefProps.Add(band4Def)
		ElseIf key = "GEOEYE-1_4BANDS" Then
			Dim band4Def As IPropertySet = New PropertySetClass()

			band1Def.SetProperty("BandName", "Blue")
			band1Def.SetProperty("WavelengthMin", 450)
			band1Def.SetProperty("WavelengthMax", 510)

			band2Def.SetProperty("BandName", "Green")
			band2Def.SetProperty("WavelengthMin", 510)
			band2Def.SetProperty("WavelengthMax", 580)

			band3Def.SetProperty("BandName", "Red")
			band3Def.SetProperty("WavelengthMin", 655)
			band3Def.SetProperty("WavelengthMax", 690)

			band4Def.SetProperty("BandName", "NearInfrared")
			band4Def.SetProperty("WavelengthMin", 780)
			band4Def.SetProperty("WavelengthMax", 920)

			productDefProps.Add(band1Def)
			productDefProps.Add(band2Def)
			productDefProps.Add(band3Def)
			productDefProps.Add(band4Def)
		ElseIf key = "IKONOS_4BANDS" Then
			Dim band4Def As IPropertySet = New PropertySetClass()

			band1Def.SetProperty("BandName", "Blue")
			band1Def.SetProperty("WavelengthMin", 445)
			band1Def.SetProperty("WavelengthMax", 516)

			band2Def.SetProperty("BandName", "Green")
			band2Def.SetProperty("WavelengthMin", 506)
			band2Def.SetProperty("WavelengthMax", 595)

			band3Def.SetProperty("BandName", "Red")
			band3Def.SetProperty("WavelengthMin", 632)
			band3Def.SetProperty("WavelengthMax", 698)

			band4Def.SetProperty("BandName", "NearInfrared")
			band4Def.SetProperty("WavelengthMin", 757)
			band4Def.SetProperty("WavelengthMax", 863)

			productDefProps.Add(band1Def)
			productDefProps.Add(band2Def)
			productDefProps.Add(band3Def)
			productDefProps.Add(band4Def)
		ElseIf key = "KOMPSAT-2_4BANDS" Then
			Dim band4Def As IPropertySet = New PropertySetClass()

			band1Def.SetProperty("BandName", "Blue")
			band1Def.SetProperty("WavelengthMin", 450)
			band1Def.SetProperty("WavelengthMax", 520)

			band2Def.SetProperty("BandName", "Green")
			band2Def.SetProperty("WavelengthMin", 520)
			band2Def.SetProperty("WavelengthMax", 600)

			band3Def.SetProperty("BandName", "Red")
			band3Def.SetProperty("WavelengthMin", 630)
			band3Def.SetProperty("WavelengthMax", 690)

			band4Def.SetProperty("BandName", "NearInfrared")
			band4Def.SetProperty("WavelengthMin", 760)
			band4Def.SetProperty("WavelengthMax", 900)

			productDefProps.Add(band1Def)
			productDefProps.Add(band2Def)
			productDefProps.Add(band3Def)
			productDefProps.Add(band4Def)
		ElseIf key = "LANDSAT_6BANDS" Then
			Dim band4Def As IPropertySet = New PropertySetClass()
			Dim band5Def As IPropertySet = New PropertySetClass()
			Dim band6Def As IPropertySet = New PropertySetClass()

			band1Def.SetProperty("BandName", "Blue")
			band1Def.SetProperty("WavelengthMin", 450)
			band1Def.SetProperty("WavelengthMax", 520)

			band2Def.SetProperty("BandName", "Green")
			band2Def.SetProperty("WavelengthMin", 520)
			band2Def.SetProperty("WavelengthMax", 600)

			band3Def.SetProperty("BandName", "Red")
			band3Def.SetProperty("WavelengthMin", 630)
			band3Def.SetProperty("WavelengthMax", 690)

			band4Def.SetProperty("BandName", "NearInfrared_1")
			band4Def.SetProperty("WavelengthMin", 760)
			band4Def.SetProperty("WavelengthMax", 900)

			band5Def.SetProperty("BandName", "NearInfrared_2")
			band5Def.SetProperty("WavelengthMin", 1550)
			band5Def.SetProperty("WavelengthMax", 1750)

			band6Def.SetProperty("BandName", "MidInfrared")
			band6Def.SetProperty("WavelengthMin", 2080)
			band6Def.SetProperty("WavelengthMax", 2350)

			productDefProps.Add(band1Def)
			productDefProps.Add(band2Def)
			productDefProps.Add(band3Def)
			productDefProps.Add(band4Def)
			productDefProps.Add(band5Def)
			productDefProps.Add(band6Def)
		ElseIf key = "QUICKBIRD_4BANDS" Then
			Dim band4Def As IPropertySet = New PropertySetClass()

			band1Def.SetProperty("BandName", "Blue")
			band1Def.SetProperty("WavelengthMin", 450)
			band1Def.SetProperty("WavelengthMax", 520)

			band2Def.SetProperty("BandName", "Green")
			band2Def.SetProperty("WavelengthMin", 520)
			band2Def.SetProperty("WavelengthMax", 600)

			band3Def.SetProperty("BandName", "Red")
			band3Def.SetProperty("WavelengthMin", 630)
			band3Def.SetProperty("WavelengthMax", 690)

			band4Def.SetProperty("BandName", "NearInfrared")
			band4Def.SetProperty("WavelengthMin", 760)
			band4Def.SetProperty("WavelengthMax", 900)

			productDefProps.Add(band1Def)
			productDefProps.Add(band2Def)
			productDefProps.Add(band3Def)
			productDefProps.Add(band4Def)
		ElseIf key = "RAPIDEYE_5BANDS" Then
			Dim band4Def As IPropertySet = New PropertySetClass()
			Dim band5Def As IPropertySet = New PropertySetClass()

			band1Def.SetProperty("BandName", "Blue")
			band1Def.SetProperty("WavelengthMin", 440)
			band1Def.SetProperty("WavelengthMax", 510)

			band2Def.SetProperty("BandName", "Green")
			band2Def.SetProperty("WavelengthMin", 520)
			band2Def.SetProperty("WavelengthMax", 590)

			band3Def.SetProperty("BandName", "Red")
			band3Def.SetProperty("WavelengthMin", 630)
			band3Def.SetProperty("WavelengthMax", 685)

			band4Def.SetProperty("BandName", "RedEdge")
			band4Def.SetProperty("WavelengthMin", 690)
			band4Def.SetProperty("WavelengthMax", 730)

			band5Def.SetProperty("BandName", "NearInfrared")
			band5Def.SetProperty("WavelengthMin", 760)
			band5Def.SetProperty("WavelengthMax", 850)

			productDefProps.Add(band1Def)
			productDefProps.Add(band2Def)
			productDefProps.Add(band3Def)
			productDefProps.Add(band4Def)
			productDefProps.Add(band5Def)
		ElseIf key = "SPOT-5_4BANDS" Then
			Dim band4Def As IPropertySet = New PropertySetClass()

			band1Def.SetProperty("BandName", "Green")
			band1Def.SetProperty("WavelengthMin", 500)
			band1Def.SetProperty("WavelengthMax", 590)

			band2Def.SetProperty("BandName", "Red")
			band2Def.SetProperty("WavelengthMin", 610)
			band2Def.SetProperty("WavelengthMax", 680)

			band3Def.SetProperty("BandName", "NearInfrared")
			band3Def.SetProperty("WavelengthMin", 780)
			band3Def.SetProperty("WavelengthMax", 890)

			band4Def.SetProperty("BandName", "ShortWaveInfrared")
			band4Def.SetProperty("WavelengthMin", 1580)
			band4Def.SetProperty("WavelengthMax", 1750)

			productDefProps.Add(band1Def)
			productDefProps.Add(band2Def)
			productDefProps.Add(band3Def)
			productDefProps.Add(band4Def)
		ElseIf key = "WORLDVIEW-2_8BANDS" Then
			Dim band4Def As IPropertySet = New PropertySetClass()
			Dim band5Def As IPropertySet = New PropertySetClass()
			Dim band6Def As IPropertySet = New PropertySetClass()
			Dim band7Def As IPropertySet = New PropertySetClass()
			Dim band8Def As IPropertySet = New PropertySetClass()

			band1Def.SetProperty("BandName", "CoastalBlue")
			band1Def.SetProperty("WavelengthMin", 400)
			band1Def.SetProperty("WavelengthMax", 450)

			band2Def.SetProperty("BandName", "Blue")
			band2Def.SetProperty("WavelengthMin", 450)
			band2Def.SetProperty("WavelengthMax", 510)

			band3Def.SetProperty("BandName", "Green")
			band3Def.SetProperty("WavelengthMin", 510)
			band3Def.SetProperty("WavelengthMax", 580)

			band4Def.SetProperty("BandName", "Yellow")
			band4Def.SetProperty("WavelengthMin", 585)
			band4Def.SetProperty("WavelengthMax", 625)

			band5Def.SetProperty("BandName", "Red")
			band5Def.SetProperty("WavelengthMin", 630)
			band5Def.SetProperty("WavelengthMax", 690)

			band6Def.SetProperty("BandName", "RedEdge")
			band6Def.SetProperty("WavelengthMin", 705)
			band6Def.SetProperty("WavelengthMax", 745)

			band7Def.SetProperty("BandName", "NearInfrared_1")
			band7Def.SetProperty("WavelengthMin", 770)
			band7Def.SetProperty("WavelengthMax", 895)

			band8Def.SetProperty("BandName", "NearInfrared_2")
			band8Def.SetProperty("WavelengthMin", 860)
			band8Def.SetProperty("WavelengthMax", 1040)

			productDefProps.Add(band1Def)
			productDefProps.Add(band2Def)
			productDefProps.Add(band3Def)
			productDefProps.Add(band4Def)
			productDefProps.Add(band5Def)
			productDefProps.Add(band6Def)
			productDefProps.Add(band7Def)
			productDefProps.Add(band8Def)
		End If
		Return productDefProps
	End Function
End Class
