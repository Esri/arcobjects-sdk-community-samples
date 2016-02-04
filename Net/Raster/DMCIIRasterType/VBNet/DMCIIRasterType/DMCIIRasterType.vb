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
Imports System.IO
Imports System.Xml
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Runtime.InteropServices
Imports ESRI.ArcGIS.ADF
Imports ESRI.ArcGIS.DataSourcesRaster
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Carto

'
' * 
' * This sample shows how to implement a Custom Raster Type to provide support for DMCII data. 
' * Also provided is an optional test application to create a Geodatabase and a mosaic dataset 
' * and add data using the custom type.
' * The main interface to be implemented is the IRasterBuilder interface along with 
' * secondary interfaces such as IRasterBuilderInit (which provides access to the parent MD),
' * IPersistvariant (which implements persistence), IRasterBuilderInit2 and IRasterBuilder2 
' * (new interfaces added at 10.1).
' * A IRasterFactory implementation also needs to be created in order for the Raster type to 
' * show up in the list of Raster Types in the Add Rasters GP Tool. The factory is responsible 
' * for creating the raster type object and setting some properties on it. It also enables the 
' * use of the Raster Product.
' * 
' 



<Guid("5DEF8E3C-51E9-49af-A3BE-EF8C68A4BBBE")> _
<ClassInterface(ClassInterfaceType.None)> _
<ProgId("SampleRasterType.CustomRasterTypeFactory")> _
<ComVisible(True)> _
Public Class DMCIIRasterTypeFactory
	Implements IRasterTypeFactory
	#Region "Private Members"
	Private myRasterTypeNames As IStringArray
	' List of Raster Types that the factory can create.
	Private myUID As UID
	' UID for the DMCII Raster Type.
	#End Region
	#Region "IRasterTypeFactory Members"

	Public Sub New()
		Dim rasterTypeName As String = "DMCII Raster Type"
		myRasterTypeNames = New StrArrayClass()
		myRasterTypeNames.Add(rasterTypeName)

		myUID = New UIDClass()
		myUID.Value = "{5DEF8E3C-51E9-49af-A3BE-EF8C68A4BBBE}"
	End Sub

	Public ReadOnly Property CLSID() As UID Implements IRasterTypeFactory.CLSID
		Get
			Return myUID
		End Get
	End Property

	''' <summary>
	''' Create a Raster Type object given the name of the raster type (usually 
	''' the same name as the one in the UI list of raster types).
	''' </summary>
	''' <param name="RasterTypeName">Name of the Raster Type object to create.</param>
	''' <returns>The Raster type object.</returns>
	Public Function CreateRasterType(RasterTypeName As String) As IRasterType Implements IRasterTypeFactory.CreateRasterType
		' Create a new RasterType object and its corresponding name object.
		Dim theRasterType As IRasterType = New RasterTypeClass()
		Dim theRasterTypeName As IRasterTypeName = New RasterTypeNameClass()
		theRasterTypeName.Name = RasterTypeName
		theRasterType.FullName = DirectCast(theRasterTypeName, IName)

		' Set the properties for the raster type object. These are shown in the 
		' 'General' tab of the raster type properties page.
		DirectCast(theRasterType, IRasterTypeProperties).Name = "DMCII Raster Type"
		DirectCast(theRasterType, IRasterTypeProperties).Description = "Raster Type for DMCII data."
		DirectCast(theRasterType, IRasterTypeProperties).DataSourceFilter = "*.dim"
		DirectCast(theRasterType, IRasterTypeProperties).SupportsOrthorectification = True

		' Create the Custom Raster Builder object
		Dim customRasterBuilder As IRasterBuilder = New DMCIIRasterBuilder()
		' Set the Raster Builder of theRasterType to the above created builder.
		theRasterType.RasterBuilder = customRasterBuilder

		' Enable the use of the Raster Type as a Raster Product.
		DirectCast(theRasterType, IRasterTypeProperties2).IsSensorRasterType = True

		'#Region "Set Product Templates"
		' Create a new array of templates if needed.
		If theRasterType.ItemTemplates Is Nothing Then
			theRasterType.ItemTemplates = New ItemTemplateArrayClass()
		End If

		' Add a 'Raw' template.
		Dim nullTemplate As IItemTemplate = New ItemTemplateClass()
		nullTemplate.Enabled = False
		nullTemplate.Name = "Raw"
		DirectCast(nullTemplate, IItemTemplate2).IsSensorTemplate = True
		DirectCast(nullTemplate, IItemTemplate2).SupportsEnhancement = False
		theRasterType.ItemTemplates.Add(nullTemplate)

		' Add a 'Stretch' template. This is the default template.
		Dim strTemplate As IItemTemplate = New ItemTemplateClass()
		strTemplate.Enabled = True
		strTemplate.Name = "Stretch"
		Dim stretchFunction As IRasterFunction = New StretchFunctionClass()
		Dim stretchFunctionArgs As IStretchFunctionArguments = New StretchFunctionArgumentsClass()
		stretchFunctionArgs.StretchType = esriRasterStretchType.esriRasterStretchMinimumMaximum
		Dim rasterVar As IRasterFunctionVariable = New RasterFunctionVariableClass()
		rasterVar.IsDataset = True
		rasterVar.Name = "MS"
		rasterVar.Aliases = New StrArrayClass()
		rasterVar.Aliases.Add("MS")
		rasterVar.Description = "Variable for input raster"
		stretchFunctionArgs.Raster = rasterVar
		Dim stretchFunctionTemplate As IRasterFunctionTemplate = New RasterFunctionTemplateClass()
		stretchFunctionTemplate.[Function] = stretchFunction
		stretchFunctionTemplate.Arguments = stretchFunctionArgs
		strTemplate.RasterFunctionTemplate = stretchFunctionTemplate
		DirectCast(strTemplate, IItemTemplate2).IsSensorTemplate = True
		DirectCast(strTemplate, IItemTemplate2).SupportsEnhancement = True
		theRasterType.ItemTemplates.Add(strTemplate)
		'#End Region

		'#Region "Set Product Types"
		' Add Product types (called URI filters in the code).
		If DirectCast(theRasterType, IRasterTypeProperties).SupportedURIFilters Is Nothing Then
			DirectCast(theRasterType, IRasterTypeProperties).SupportedURIFilters = New ArrayClass()
		End If
		' Create and setup URI Filters
		Dim allFilter As IItemURIFilter = New URIProductNameFilterClass()
		allFilter.Name = "All"
		allFilter.SupportsOrthorectification = True
		allFilter.SupportedTemplateNames = New StrArrayClass()
		allFilter.SupportedTemplateNames.Add("Raw")
		allFilter.SupportedTemplateNames.Add("Stretch")
		Dim allProductNames As IStringArray = New StrArrayClass()
		allProductNames.Add("L1T")
		allProductNames.Add("L1R")
		DirectCast(allFilter, IURIProductNameFilter).ProductNames = allProductNames

		' The L1T filter does not support orthorectification.
		Dim l1tFilter As IItemURIFilter = New URIProductNameFilterClass()
		l1tFilter.Name = "L1T"
		l1tFilter.SupportsOrthorectification = False
		l1tFilter.SupportedTemplateNames = New StrArrayClass()
		l1tFilter.SupportedTemplateNames.Add("Raw")
		l1tFilter.SupportedTemplateNames.Add("Stretch")
		Dim l1tProductNames As IStringArray = New StrArrayClass()
		l1tProductNames.Add("L1T")
		DirectCast(l1tFilter, IURIProductNameFilter).ProductNames = l1tProductNames

		Dim l1rFilter As IItemURIFilter = New URIProductNameFilterClass()
		l1rFilter.Name = "L1R"
		l1rFilter.SupportsOrthorectification = True
		l1rFilter.SupportedTemplateNames = New StrArrayClass()
		l1rFilter.SupportedTemplateNames.Add("Raw")
		l1rFilter.SupportedTemplateNames.Add("Stretch")
		Dim l1rProductNames As IStringArray = New StrArrayClass()
		l1rProductNames.Add("L1R")
		DirectCast(l1rFilter, IURIProductNameFilter).ProductNames = l1rProductNames

		' Add them to the supported uri filters list
		DirectCast(theRasterType, IRasterTypeProperties).SupportedURIFilters.Add(allFilter)
		DirectCast(theRasterType, IRasterTypeProperties).SupportedURIFilters.Add(l1tFilter)
		DirectCast(theRasterType, IRasterTypeProperties).SupportedURIFilters.Add(l1rFilter)
		' Set 'All' as default
		theRasterType.URIFilter = allFilter
		'#End Region

		Return theRasterType
	End Function

	''' <summary>
	''' Name of the Raster Type Factory
	''' </summary>
	Public ReadOnly Property Name() As String Implements IRasterTypeFactory.Name
		Get
			Return "Custom Raster Type Factory"
		End Get
	End Property

	''' <summary>
	''' Names of the Raster Types supported by the factory.
	''' </summary>
	Public ReadOnly Property RasterTypeNames() As IStringArray Implements IRasterTypeFactory.RasterTypeNames
		Get
			Return myRasterTypeNames
		End Get
	End Property
	#End Region

	#Region "COM Registration Function(s)"
	<ComRegisterFunction> _
	Private Shared Sub Reg(regKey As String)
		ESRI.ArcGIS.ADF.CATIDs.RasterTypeFactory.Register(regKey)
	End Sub

	<ComUnregisterFunction> _
	Private Shared Sub Unreg(regKey As String)
		ESRI.ArcGIS.ADF.CATIDs.RasterTypeFactory.Unregister(regKey)
	End Sub
	#End Region
End Class

<Guid("316725CB-35F2-4159-BEBB-A1445ECE9CF1")> _
<ClassInterface(ClassInterfaceType.None)> _
<ProgId("SampleRasterType.CustomRasterType")> _
<ComVisible(True)> _
Public Class DMCIIRasterBuilder
	Implements IRasterBuilder
	Implements IRasterBuilderInit
	Implements IPersistVariant
	Implements IRasterBuilder2
	Implements IRasterBuilderInit2
	#Region "Private Members"
	' The Mosaic Dataset currently using the Raster Type.
	Private myMosaicDataset As IMosaicDataset
	' The default spatial reference to apply to added data (if no spatial reference exists).
	Private myDefaultSpatialReference As ISpatialReference

	' The Raster Type Operation object (usually a Raster Type object).
	Private myRasterTypeOperation As IRasterTypeOperation
	' The Raster Type Properties.
	Private myRasterTypeProperties As IPropertySet

	' Array to fill with ItemURI's.
	Private myURIArray As IItemURIArray

	' GeoTransform helper object.
	Private myGeoTransformationHelper As IGeoTransformationHelper
	' Flags to specify whether the Raster Type can merge items and 
	Private myCanMergeItems As Boolean
	' if it has merged item.
	Private myMergeItems As Boolean

	' Mapping from field names to names or properties in the item propertyset.
	Private myAuxiliaryFieldAlias As IPropertySet
	' Fields to add to the Mosaic Dataset when items are added through this Raster Type.
	Private myAuxiliaryFields As IFields

	Private myTrackCancel As ITrackCancel
	Private myUID As UID
	' UID for the Custom Builder.
	' The current dimap file being processed.
	Private myCurrentDimFile As String
	#End Region

	Public Sub New()
		myMosaicDataset = Nothing
		myDefaultSpatialReference = Nothing
		myRasterTypeOperation = Nothing
		myRasterTypeProperties = Nothing
		myTrackCancel = Nothing

		myURIArray = Nothing

		myGeoTransformationHelper = Nothing
		myCanMergeItems = False
		myMergeItems = False

		myAuxiliaryFieldAlias = Nothing
		myAuxiliaryFields = Nothing

		myUID = New UIDClass()
		myUID.Value = "{316725CB-35F2-4159-BEBB-A1445ECE9CF1}"
	End Sub

	#Region "IRasterBuilder Members"

	''' <summary>
	''' This defines a mapping from field names in the attribute table of the mosaic to
	''' properties in the property set associated with the dataset, incase a user wants 
	''' specify fields which are different from the property in the dataset.
	''' e.g. The field CloudCover may map to a property called C_C in the dataset built 
	''' by the builder.
	''' </summary>
	Public Property AuxiliaryFieldAlias() As IPropertySet Implements IRasterBuilder.AuxiliaryFieldAlias, IRasterBuilder2.AuxiliaryFieldAlias
		Get
			Return myAuxiliaryFieldAlias
		End Get
		Set
			myAuxiliaryFieldAlias = value
		End Set
	End Property

	''' <summary>
	''' Specify fields if necessary to be added to the Mosaic Dataset when 
	''' items are added throug hthis Raster Type.
	''' </summary>
	Public Property AuxiliaryFields() As IFields Implements IRasterBuilder.AuxiliaryFields, IRasterBuilder2.AuxiliaryFields
		Get
			If myAuxiliaryFields Is Nothing Then
				myAuxiliaryFields = New FieldsClass()
				AddFields(myAuxiliaryFields)
			End If
			Return myAuxiliaryFields
		End Get
		Set
			myAuxiliaryFields = value
		End Set
	End Property

	''' <summary>
	''' Get a crawler recommended by the Raster Type based on the data srouce properties provided.
	''' </summary>
	''' <param name="pDataSourceProperties">Data source properties.</param>
	''' <returns>Data source crawler recommended by the raster type.</returns>
	Public Function GetRecommendedCrawler(pDataSourceProperties As IPropertySet) As IDataSourceCrawler Implements IRasterBuilder.GetRecommendedCrawler, IRasterBuilder2.GetRecommendedCrawler
		Try
			' This is usually a file crawler because it can crawl directories as well, unless
			' special types of data needs to be crawled.
			Dim myCrawler As IDataSourceCrawler = New FileCrawlerClass()
			DirectCast(myCrawler, IFileCrawler).Path = Convert.ToString(pDataSourceProperties.GetProperty("Source"))
			DirectCast(myCrawler, IFileCrawler).Recurse = Convert.ToBoolean(pDataSourceProperties.GetProperty("Recurse"))
			myCrawler.Filter = Convert.ToString(pDataSourceProperties.GetProperty("Filter"))
			If myCrawler.Filter Is Nothing OrElse myCrawler.Filter = "" Then
				myCrawler.Filter = "*.dim"
			End If
			Return myCrawler
		Catch generatedExceptionName As Exception
			Throw
		End Try
	End Function

	''' <summary>
	''' Prepare the Raster Type for generating item Unique Resource Identifier (URI)
	''' </summary>
	''' <param name="pCrawler">Crawler to use to generate the item URI's</param>
	Public Sub BeginConstruction(pCrawler As IDataSourceCrawler) Implements IRasterBuilder.BeginConstruction, IRasterBuilder2.BeginConstruction
		myURIArray = New ItemURIArrayClass()
	End Sub

	''' <summary>
	''' Construct a Unique Resource Identifier (URI)
	''' for each crawler item
	''' </summary>
	''' <param name="crawlerItem">Crawled Item from which the URI is generated</param>
	Public Sub ConstructURIs(crawlerItem As Object) Implements IRasterBuilder.ConstructURIs, IRasterBuilder2.ConstructURIs
		myCurrentDimFile = DirectCast(crawlerItem, String)
	End Sub

	''' <summary>
	''' Finish construction of the URI's
	''' </summary>
	''' <returns>Array containing finised URI's</returns>
	Public Function EndConstruction() As IItemURIArray Implements IRasterBuilder.EndConstruction, IRasterBuilder2.EndConstruction
		Return myURIArray
	End Function

	''' <summary>
	''' Generate the next URI.
	''' </summary>
	''' <returns>The URI generated.</returns>
	Public Function GetNextURI() As IItemURI Implements IRasterBuilder.GetNextURI, IRasterBuilder2.GetNextURI
		Dim newURI As IItemURI = Nothing
		Try
			' Check to see if the item cralwed is a .dim file.
			If myCurrentDimFile <> "" AndAlso myCurrentDimFile IsNot Nothing AndAlso myCurrentDimFile.EndsWith(".dim") Then
				' Create a new Dimap Parser obect and item uri.
				Dim myDimParser As New DiMapParser(myCurrentDimFile)
				newURI = New ItemURIClass()
				' Set the display name, Group, Product Name, Tag and Key.
				newURI.DisplayName = System.IO.Path.GetFileName(myCurrentDimFile)
				newURI.Group = System.IO.Path.GetFileNameWithoutExtension(myCurrentDimFile)
				newURI.Key = myCurrentDimFile
				newURI.ProductName = myDimParser.ProductType
				newURI.Tag = "MS"
				' Set the timestamp of the dimfile as source time stamp. This helps 
				' with synchronization later.
				Dim myEnv As IRasterTypeEnvironment = New RasterTypeEnvironmentClass()
				Dim dimTS As DateTime = myEnv.GetTimeStamp(myCurrentDimFile)
				newURI.SourceTimeStamp = dimTS

				myDimParser = Nothing
				myCurrentDimFile = ""
				myURIArray.Add(newURI)
			End If
		Catch generatedExceptionName As Exception
			Throw
		End Try
		Return newURI
	End Function

	''' <summary>
	''' Build the Builder Item which includes the function raster dataset and its footprint 
	''' given the ItemURI.
	''' </summary>
	''' <param name="pItemURI">ItemURi to use to build the Builder Item.</param>
	''' <returns>The builder item.</returns>
	Public Function Build(pItemURI As IItemURI) As IBuilderItem Implements IRasterBuilder.Build, IRasterBuilder2.Build
		Try
			' Create a new parser object and builder item.
			Dim myDimParser As New DiMapParser(pItemURI.Key)
			Dim currItem As IBuilderItem = New BuilderItemClass()

			' Set Category and URI
			currItem.Category = esriRasterCatalogItemCategory.esriRasterCatalogItemCategoryPrimary
			currItem.URI = pItemURI

			' Set FunctionRasterDataset
			Dim inputFrd As IFunctionRasterDataset = GetFRD(myDimParser, pItemURI)
			currItem.Dataset = inputFrd
			' Set band information for the function dataset including names, wavelengths and stats if available.
			SetBandProperties(DirectCast(inputFrd, IDataset), myDimParser)

			' Set Footprint
			Dim geoDset As IGeoDataset = DirectCast(inputFrd, IGeoDataset)
			' Set it to the current raster extent first. If the raster has no 
			' spatial reference, the extents will be in pixel space.
			currItem.Footprint = DirectCast(geoDset.Extent, IGeometry)
			' The get the footprint from the dim file is it exists.
			currItem.Footprint = GetFootprint(myDimParser)

			' Set Properties. These properties are used to fill the Auxiliary Fields 
			' defined earlier and also key properties if the names are correct.
			Dim propSet As IPropertySet = currItem.Dataset.Properties
			If propSet Is Nothing Then
				propSet = New PropertySetClass()
			End If
			Dim sunAzimuth As Double = Convert.ToDouble(myDimParser.SunAzimuth)
			Dim sunElevation As Double = Convert.ToDouble(myDimParser.SunElevation)
			Dim sensorAzimuth As Double = Convert.ToDouble(myDimParser.SensorAzimuth)
			Dim sensorElevation As Double = 180 - Convert.ToDouble(myDimParser.IncidenceAngle)
			Dim acqDate As String = myDimParser.AcquisitionDate
			Dim acqTime As String = myDimParser.AcquisitionTime
			' Create a time object from the provided date and time.
			Dim acqDateTimeObj As ITime = New TimeClass()
			acqDateTimeObj.SetFromTimeString(esriTimeStringFormat.esriTSFYearThruSubSecondWithDash, acqDate & " " & acqTime)
			' and obtain a DateTime object to set as value of the property. This ensures the 
			' field displays the value correctly.
			Dim acqDateTimeFieldVal As DateTime = acqDateTimeObj.QueryOleTime()

			propSet.SetProperty("AcquisitionDate", acqDateTimeFieldVal)
			propSet.SetProperty("SensorName", myDimParser.MetadataProfile)
			propSet.SetProperty("SunAzimuth", sunAzimuth)
			propSet.SetProperty("SunElevation", sunElevation)
			propSet.SetProperty("SatAzimuth", sensorAzimuth)
			propSet.SetProperty("SatElevation", sensorElevation)
			currItem.Dataset.Properties = propSet

			Return currItem
		Catch exc As Exception
			Throw exc
		End Try
	End Function

	''' <summary>
	''' Flag to specify whether the Raster Builder can build items in place.
	''' </summary>
	Public ReadOnly Property CanBuildInPlace() As Boolean Implements IRasterBuilder.CanBuildInPlace, IRasterBuilder2.CanBuildInPlace
		Get
			Return False
		End Get
	End Property

	''' <summary>
	''' Check if the item provided is "stale" or not valid
	''' </summary>
	''' <param name="pItemURI">URI for the item to be checked</param>
	''' <returns>Flag to specify whether the item is stale or not.</returns>
	Public Function IsStale(pItemURI As IItemURI) As Boolean Implements IRasterBuilder.IsStale, IRasterBuilder2.IsStale
		Try
			Dim myEnv As IRasterTypeEnvironment = New RasterTypeEnvironmentClass()
			Dim currDimTS As DateTime = myEnv.GetTimeStamp(pItemURI.Key)
			Return pItemURI.SourceTimeStamp <> currDimTS
		Catch generatedExceptionName As Exception
			Throw
		End Try
	End Function


	''' <summary>
	''' Properties associated with the Raster Type
	''' </summary>
	Public Property Properties() As IPropertySet Implements IRasterBuilder.Properties, IRasterBuilder2.Properties
		Get
			If myRasterTypeProperties Is Nothing Then
				myRasterTypeProperties = New PropertySetClass()
			End If
			Return myRasterTypeProperties
		End Get
		Set
			myRasterTypeProperties = value
		End Set
	End Property

	''' <summary>
	''' Sets band properties on a given dataset including stats, band names and wavelengths.
	''' </summary>
	''' <param name="dataset">The dataset to set properties on.</param>
	''' <param name="dimParser">Dimap parser to read properties from.</param>
	Private Sub SetBandProperties(dataset As IDataset, dimParser As DiMapParser)
		Try
			' Set band band props.
			Dim rasterKeyProps As IRasterKeyProperties = DirectCast(dataset, IRasterKeyProperties)
			Dim rasterBandColl As IRasterBandCollection = DirectCast(dataset, IRasterBandCollection)
			Dim imageNumBands As Integer = DirectCast(dataset, IFunctionRasterDataset).RasterInfo.BandCount
			Dim dinNumBands As Integer = dimParser.NumBands
			Dim bandIndexes As Integer() = New Integer(dinNumBands - 1) {}
			Dim bandNames As IStringArray = New StrArrayClass()
			For i As Integer = 0 To dinNumBands - 1
				' Get band index for the first band.
				bandIndexes(i) = Convert.ToInt16(dimParser.GetBandIndex(i))
				' Validate band index.
				If bandIndexes(i) > 0 AndAlso bandIndexes(i) <= imageNumBands Then
					' Get Band Name for the index.
					bandNames.Add(dimParser.GetBandDesc(bandIndexes(i)))
					' Get Band stats for the index.
					Dim bandStats As IRasterStatistics = New RasterStatisticsClass()
					bandStats.Minimum = Convert.ToDouble(dimParser.GetBandStatMin(bandIndexes(i)))
					bandStats.Maximum = Convert.ToDouble(dimParser.GetBandStatMax(bandIndexes(i)))
					bandStats.Mean = Convert.ToDouble(dimParser.GetBandStatMean(bandIndexes(i)))
					bandStats.StandardDeviation = Convert.ToDouble(dimParser.GetBandStatStdDev(bandIndexes(i)))
					' Set stats on the dataset.
					DirectCast(rasterBandColl.Item(bandIndexes(i) - 1), IRasterBandEdit2).AlterStatistics(bandStats)
					' Set Band Name and wavelengths according to the name.
                    rasterKeyProps.SetBandProperty("BandName", (bandIndexes(i) - 1), bandNames.Element(i))
					SetBandWavelengths(dataset, (bandIndexes(i) - 1))
					' Refresh dataset so changes are saved.
					DirectCast(dataset, IRasterDataset3).Refresh()
				End If
			Next
		Catch exc As Exception
			Dim [error] As String = exc.Message
		End Try
	End Sub

	''' <summary>
	''' Set the wavelengths corresponding to the band name.
	''' </summary>
	Private Sub SetBandWavelengths(dataset As IDataset, bandIndex As Integer)
		Dim rasterKeyProps As IRasterKeyProperties = DirectCast(dataset, IRasterKeyProperties)
		Dim rasterBandColl As IRasterBandCollection = DirectCast(dataset, IRasterBandCollection)
		Dim bandName As String = DirectCast(rasterKeyProps.GetBandProperty("BandName", bandIndex), String)
		' Set wavelengths for the bands
		Select Case bandName.ToLower()
			Case "red"
				If True Then
					rasterKeyProps.SetBandProperty("WavelengthMin", bandIndex, 630)
					rasterKeyProps.SetBandProperty("WavelengthMax", bandIndex, 690)
				End If
				Exit Select

			Case "green"
				If True Then
					rasterKeyProps.SetBandProperty("WavelengthMin", bandIndex, 520)
					rasterKeyProps.SetBandProperty("WavelengthMax", bandIndex, 600)
				End If
				Exit Select

			Case "nir", "nearinfrared"
				If True Then
					rasterKeyProps.SetBandProperty("WavelengthMin", bandIndex, 770)
					rasterKeyProps.SetBandProperty("WavelengthMax", bandIndex, 900)
				End If
				Exit Select
		End Select
	End Sub

	'private IGeometry GetFootprint(DiMapParser dimParser)
	'{
	'    IGeometry currFootprint = null;
	'    dimParser.ResetVertexCount();
	'    string xs = "";
	'    string ys = "";
	'    string rows = "";
	'    string cols = "";
	'    double minX = 10000000000.0;
	'    double maxX = -1000000000.00;
	'    double minY = 1000000000.00;
	'    double maxY = -1000000000.00;
	'    double minRow = 1000000000.00;
	'    double maxRow = -1000000000.0;
	'    double minCol = 1000000000.00;
	'    double maxCol = -1000000000.0;
	'    double x = 0.0;
	'    double y = 0.0;
	'    double row = 0.0;
	'    double col = 0.0;

	'    while (dimParser.GetNextVertex(out xs, out ys, out rows, out cols))
	'    {
	'        x = Convert.ToDouble(xs);
	'        y = Convert.ToDouble(ys);
	'        row = Convert.ToDouble(rows);
	'        col = Convert.ToDouble(cols);

	'        if (x < minX)
	'            minX = x;
	'        if (x > maxX)
	'            maxX = x;

	'        if (y < minY)
	'            minY = y;
	'        if (y > maxY)
	'            maxY = y;

	'        if (row < minRow)
	'            minRow = row;
	'        if (row > maxRow)
	'            maxRow = row;

	'        if (col < minCol)
	'            minCol = col;
	'        if (col > maxCol)
	'            maxCol = col;

	'        x = 0.0;
	'        y = 0.0;
	'        row = 0.0;
	'        col = 0.0;
	'        xs = "";
	'        ys = "";
	'        rows = "";
	'        cols = "";
	'    }
	'    x = Convert.ToDouble(xs);
	'    y = Convert.ToDouble(ys);
	'    row = Convert.ToDouble(rows);
	'    col = Convert.ToDouble(cols);

	'    if (x < minX)
	'        minX = x;
	'    if (x > maxX)
	'        maxX = x;

	'    if (y < minY)
	'        minY = y;
	'    if (y > maxY)
	'        maxY = y;

	'    if (row < minRow)
	'        minRow = row;
	'    if (row > maxRow)
	'        maxRow = row;

	'    if (col < minCol)
	'        minCol = col;
	'    if (col > maxCol)
	'        maxCol = col;

	'    currFootprint = new PolygonClass();
	'    IPointCollection currPointColl = (IPointCollection)currFootprint;
	'    IEnvelope rectEnvelope = new EnvelopeClass();
	'    rectEnvelope.PutCoords(minX, minY, maxX, maxY);
	'    ISegmentCollection segmentCollection = (ISegmentCollection)currFootprint;
	'    segmentCollection.SetRectangle(rectEnvelope);

	'    // Get Srs
	'    int epsgcode = Convert.ToInt32((dimParser.SrsCode.Split(':'))[1]);
	'    ISpatialReferenceFactory3 srsfactory = new SpatialReferenceEnvironmentClass();
	'    ISpatialReference dimSrs = srsfactory.CreateSpatialReference(epsgcode);
	'    ISpatialReferenceResolution srsRes = (ISpatialReferenceResolution)dimSrs;
	'    srsRes.ConstructFromHorizon();
	'    srsRes.SetDefaultXYResolution();
	'    ((ISpatialReferenceTolerance)dimSrs).SetDefaultXYTolerance();
	'    currFootprint.SpatialReference = dimSrs;

	'    #region Commented
	'    //IEnvelope extent = new EnvelopeClass();
	'    //extent.XMin = geoDset.Extent.XMin;
	'    //extent.XMax = geoDset.Extent.XMax;
	'    //extent.YMin = geoDset.Extent.YMin;
	'    //extent.YMax = geoDset.Extent.YMax;
	'    //extent.SpatialReference = geoDset.SpatialReference;
	'    //extent.Width = inputFrd.RasterInfo.Extent.Width;
	'    //extent.Height = inputFrd.RasterInfo.Extent.Height;
	'    //currItem.Footprint = (IGeometry)extent;

	'    //myDimParser.ResetVertexCount();
	'    //string x = "";
	'    //string y = "";
	'    //string row = "";
	'    //string col = "";
	'    //IGeometry currFootprint = new PolygonClass();
	'    //IPointCollection currPointColl = (IPointCollection)currFootprint;

	'    // Creating a polygon!!!

	'    ////Build a polygon from a sequence of points. 
	'    ////Add arrays of points to a geometry using the IGeometryBridge2 interface on the 
	'    ////GeometryEnvironment singleton object.
	'    //IGeometryBridge2 geometryBridge2 = new GeometryEnvironmentClass();
	'    //IPointCollection4 pointCollection4 = new PolygonClass();

	'    ////TODO:
	'    ////pointCollection4.SpatialReference = 'Define the spatial reference of the new polygon.

	'    //WKSPoint[] aWKSPointBuffer = null;
	'    //long cPoints = 4; //The number of points in the first part.
	'    //aWKSPointBuffer = new WKSPoint[System.Convert.ToInt32(cPoints - 1) + 1];

	'    ////TODO:
	'    ////aWKSPointBuffer = 'Read cPoints into the point buffer.

	'    //geometryBridge2.SetWKSPoints(pointCollection4, ref aWKSPointBuffer);

	'    //myDimParser.GetNextVertex(out x, out y, out col, out row);
	'    //IPoint currPoint1 = new PointClass();
	'    //currPoint1.X = Convert.ToDouble(x);
	'    //currPoint1.Y = Convert.ToDouble(y);
	'    //myDimParser.GetNextVertex(out x, out y, out col, out row);
	'    //IPoint currPoint2 = new PointClass();
	'    //currPoint1.X = Convert.ToDouble(x);
	'    //currPoint1.Y = Convert.ToDouble(y);
	'    //myDimParser.GetNextVertex(out x, out y, out col, out row);
	'    //IPoint currPoint3 = new PointClass();
	'    //currPoint1.X = Convert.ToDouble(x);
	'    //currPoint1.Y = Convert.ToDouble(y);
	'    //myDimParser.GetNextVertex(out x, out y, out col, out row);
	'    //IPoint currPoint4 = new PointClass();
	'    //currPoint1.X = Convert.ToDouble(x);
	'    //currPoint1.Y = Convert.ToDouble(y);
	'    //object refPoint1 = (object)currPoint1;
	'    //object refPoint2 = (object)currPoint2;
	'    //object refPoint3 = (object)currPoint3;
	'    //object refPoint4 = (object)currPoint4;
	'    //currPointColl.AddPoint(currPoint1, ref refPoint4, ref refPoint2);
	'    //currPointColl.AddPoint(currPoint2, ref refPoint1, ref refPoint3);
	'    //currPointColl.AddPoint(currPoint3, ref refPoint2, ref refPoint4);
	'    //currPointColl.AddPoint(currPoint4, ref refPoint3, ref refPoint1);
	'    //((IPolygon)currFootprint).Close();
	'    //currFootprint.SpatialReference = dimSrs;
	'    #endregion
	'    return currFootprint;
	'}

	''' <summary>
	''' Get the footprint from the dimap file if it exists.
	''' </summary>
	''' <param name="dimParser">Dimap file parser.</param>
	''' <returns>Footprint geomtry.</returns>
	Private Function GetFootprint(dimParser As DiMapParser) As IGeometry
		Dim currFootprint As IGeometry = Nothing
		dimParser.ResetVertexCount()
		Dim xs As String = ""
		Dim ys As String = ""
		Dim minX As Double = 10000000000.0
		Dim maxX As Double = -1000000000.0
		Dim minY As Double = 1000000000.0
		Dim maxY As Double = -1000000000.0
		Dim x As Double = 0.0
		Dim y As Double = 0.0
		Dim units As String = dimParser.ProductType
		If units.ToLower() = "L1T".ToLower() Then
			units = "M"
		ElseIf units.ToLower() = "L1R".ToLower() Then
			units = "Deg"
		End If
		' Get vertices from the dimap file and figure out the min,max.
		While dimParser.GetNextVertex2(xs, ys, units)
			x = Convert.ToDouble(xs)
			y = Convert.ToDouble(ys)

			If x < minX Then
				minX = x
			End If
			If x > maxX Then
				maxX = x
			End If

			If y < minY Then
				minY = y
			End If
			If y > maxY Then
				maxY = y
			End If

			x = 0.0
			y = 0.0
			xs = ""
			ys = ""
		End While
		x = Convert.ToDouble(xs)
		y = Convert.ToDouble(ys)

		If x < minX Then
			minX = x
		End If
		If x > maxX Then
			maxX = x
		End If

		If y < minY Then
			minY = y
		End If
		If y > maxY Then
			maxY = y
		End If

		' create a new polygon and fill it using the vertices calculated.
		currFootprint = New PolygonClass()
		Dim currPointColl As IPointCollection = DirectCast(currFootprint, IPointCollection)
		Dim rectEnvelope As IEnvelope = New EnvelopeClass()
		rectEnvelope.PutCoords(minX, minY, maxX, maxY)
		Dim segmentCollection As ISegmentCollection = DirectCast(currFootprint, ISegmentCollection)
		segmentCollection.SetRectangle(rectEnvelope)

		' Get Srs from the dim file and set it on the footprint.
		Dim epsgcode As Integer = Convert.ToInt32((dimParser.SrsCode.Split(":"C))(1))
		Dim srsfactory As ISpatialReferenceFactory3 = New SpatialReferenceEnvironmentClass()
		Dim dimSrs As ISpatialReference = srsfactory.CreateSpatialReference(epsgcode)
		Dim srsRes As ISpatialReferenceResolution = DirectCast(dimSrs, ISpatialReferenceResolution)
		srsRes.ConstructFromHorizon()
		srsRes.SetDefaultXYResolution()
		DirectCast(dimSrs, ISpatialReferenceTolerance).SetDefaultXYTolerance()
		currFootprint.SpatialReference = dimSrs
		Return currFootprint
	End Function

	''' <summary>
	''' Create the function raster dataset from the source images.
	''' </summary>
	''' <param name="dimPar">Parser for the dimap file.</param>
	''' <param name="pItemURI">ItemURi to use.</param>
	''' <returns>Function raster dataset created.</returns>
	Private Function GetFRD(dimPar As DiMapParser, pItemURI As IItemURI) As IFunctionRasterDataset
		Dim opFrd As IFunctionRasterDataset = Nothing
		Try
			Dim factoryType As Type = Type.GetTypeFromProgID("esriDataSourcesRaster.RasterWorkspaceFactory")
			Dim workspaceFactory As IWorkspaceFactory = DirectCast(Activator.CreateInstance(factoryType), IWorkspaceFactory)
			Dim workspace As IWorkspace = workspaceFactory.OpenFromFile(System.IO.Path.GetDirectoryName(pItemURI.Key), 0)
			Dim rasterWorkspace As IRasterWorkspace = DirectCast(workspace, IRasterWorkspace)
			' Open the tif file associated with the .dim file as a raster dataset.
			Dim imagePath As String = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(pItemURI.Key), pItemURI.Group & ".tif")
			Dim rpcPath As String = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(pItemURI.Key), pItemURI.Group & ".rpc")
			Dim inputRasterDataset As IRasterDataset = Nothing
			If File.Exists(imagePath) Then
				inputRasterDataset = rasterWorkspace.OpenRasterDataset(pItemURI.Group & ".tif")
			Else
				Return Nothing
			End If

			Dim intermedFrd As IFunctionRasterDataset = Nothing
			' If the file opes successfully, add a RasterInfo function on top.
			If inputRasterDataset IsNot Nothing Then
				' Create an Identity function dataset to get the raster info.
				Dim identityFunction As IRasterFunction = New IdentityFunctionClass()
				Dim idFrd As IFunctionRasterDataset = New FunctionRasterDatasetClass()
				idFrd.Init(identityFunction, inputRasterDataset)
				' Create a raster info function dataset.
				Dim rasterInfoFunction As IRasterFunction = New RasterInfoFunctionClass()
				Dim rasterInfoFuncArgs As IRasterInfoFunctionArguments = New RasterInfoFunctionArgumentsClass()
				rasterInfoFuncArgs.Raster = inputRasterDataset
				rasterInfoFuncArgs.RasterInfo = idFrd.RasterInfo
				intermedFrd = New FunctionRasterDatasetClass()
				intermedFrd.Init(rasterInfoFunction, rasterInfoFuncArgs)
			Else
				Return Nothing
			End If
			' Check if there is an RPC file associated with the image. If so
			' then add a geometric function to apply the rpc xform.
			If File.Exists(rpcPath) Then
				opFrd = ApplyRPC(rpcPath, DirectCast(intermedFrd, IRasterDataset))
			End If

			' If no rpc pars exist or applying rpc fails, use the intermediate 
			' function raster dataset created.
			If opFrd Is Nothing Then
				opFrd = intermedFrd

				'IRasterFunction ebFunction = new ExtractBandFunctionClass();
				'IRasterFunctionArguments ebFuncArgs = new ExtractBandFunctionArgumentsClass();
				'ILongArray bandIDs = new LongArrayClass();
				'bandIDs.Add(2);
				'bandIDs.Add(1);
				'bandIDs.Add(0);
				'''/bandIDs.Add(4);
				'((IExtractBandFunctionArguments)ebFuncArgs).BandIDs = bandIDs;
				'((IExtractBandFunctionArguments)ebFuncArgs).Raster = inputRasterDataset;
				'opFrd = new FunctionRasterDatasetClass();
				'opFrd.Init(ebFunction, ebFuncArgs);

				'if (opFrd == null)
				'{
				'    IRasterFunction identityFunction = new IdentityFunctionClass();
				'    opFrd = new FunctionRasterDatasetClass();
				'    opFrd.Init(identityFunction, inputRasterDataset);
				'}
			End If
		Catch exc As Exception
			Dim [error] As String = exc.Message
		End Try
		Return opFrd
	End Function

	''' <summary>
	''' Parse the RPC parameters file associated with the image and create an RPCXform
	''' to bea applied to the inputDataset as a geomtric function.
	''' </summary>
	''' <param name="rpcPath">Path to the rpc parameters file.</param>
	''' <param name="inputDataset">Input dataset to apply the xform on.</param>
	''' <returns>Function raster dataset created.</returns>
	Private Function ApplyRPC(rpcPath As String, inputDataset As IRasterDataset) As IFunctionRasterDataset
		Dim opFrd As IFunctionRasterDataset = Nothing
		Try
			' Open the RPC file and create Geometric transform
			Dim finalXForm As IGeodataXform = Nothing
			Dim rpcXForm As IRPCXform = GetRPCXForm(rpcPath)

			Dim idFrd As IFunctionRasterDataset = Nothing
			If Not (TypeOf inputDataset Is IFunctionRasterDataset) Then
				Dim identityFunction As IRasterFunction = New IdentityFunctionClass()
				idFrd = New FunctionRasterDatasetClass()
				idFrd.Init(identityFunction, inputDataset)
			Else
				idFrd = DirectCast(inputDataset, IFunctionRasterDataset)
			End If

			Dim datasetRasterInfo As IRasterInfo = idFrd.RasterInfo
			Dim datasetExtent As IEnvelope = datasetRasterInfo.Extent
			Dim datasetSrs As ISpatialReference = DirectCast(idFrd, IGeoDataset).SpatialReference

			Dim dRows As Long = datasetRasterInfo.Height
			Dim dCols As Long = datasetRasterInfo.Width
			Dim pixelExtent As IEnvelope = New EnvelopeClass()
			pixelExtent.PutCoords(-0.5, 0.5 - dRows, -0.5 + dCols, 0.5)

			Dim noAffineNeeded As Boolean = DirectCast(pixelExtent, IClone).IsEqual(DirectCast(datasetExtent, IClone))
			If Not noAffineNeeded Then
				' Tranform ground space to pixel space.
				Dim affineXform As IAffineTransformation2D = New AffineTransformation2DClass()
				affineXform.DefineFromEnvelopes(datasetExtent, pixelExtent)
				Dim geoXform As IGeometricXform = New GeometricXformClass()
				geoXform.Transformation = affineXform
				finalXForm = geoXform
			End If

			' Transform from pixel space back to ground space to set as the forward transform.
			Dim groundExtent As IEnvelope = DirectCast(idFrd, IGeoDataset).Extent
			groundExtent.Project(datasetSrs)
			Dim affineXform2 As IAffineTransformation2D = New AffineTransformation2DClass()
			affineXform2.DefineFromEnvelopes(pixelExtent, groundExtent)
			Dim forwardXForm As IGeometricXform = New GeometricXformClass()
			forwardXForm.Transformation = affineXform2
			rpcXForm.ForwardXform = forwardXForm

			' Create the composite transform that changes ground values to pixel space
			' then applies the rpc transform which will transform them back to ground space.
			Dim compositeXForm As ICompositeXform = New CompositeXformClass()
			compositeXForm.Add(finalXForm)
			compositeXForm.Add(rpcXForm)
			finalXForm = DirectCast(compositeXForm, IGeodataXform)

			' Then apply the transform on the raster using the geometric function.
			If finalXForm IsNot Nothing Then
				Dim geometricFunction As IRasterFunction = New GeometricFunctionClass()
				Dim geometricFunctionArgs As IGeometricFunctionArguments = Nothing
				' Get the geomtric function arguments if supplied by the user (in the UI).
				If myRasterTypeOperation IsNot Nothing AndAlso DirectCast(myRasterTypeOperation, IRasterTypeProperties).OrthorectificationParameters IsNot Nothing Then
					geometricFunctionArgs = DirectCast(myRasterTypeOperation, IRasterTypeProperties).OrthorectificationParameters
				Else
					geometricFunctionArgs = New GeometricFunctionArgumentsClass()
				End If
				' Append the xform to the existing ones from the image.
				geometricFunctionArgs.AppendGeodataXform = True
				geometricFunctionArgs.GeodataXform = finalXForm
				geometricFunctionArgs.Raster = inputDataset
				opFrd = New FunctionRasterDatasetClass()
				opFrd.Init(geometricFunction, geometricFunctionArgs)
			End If
			Return opFrd
		Catch exc As Exception
			Dim [error] As String = exc.Message
			Return opFrd
		End Try
	End Function

	''' <summary>
	''' Create an RPCXForm from a text file containing parameters.
	''' </summary>
	''' <param name="rpcFilePath">Text file containing the parameters.</param>
	''' <returns>The RPCXForm generated.</returns>
	Private Function GetRPCXForm(rpcFilePath As String) As IRPCXform
		Try
			' Array for parameters.
			Dim RPC As Double() = New Double(89) {}
			' Propertyset to store properties as backup.
			'IPropertySet coefficients = new PropertySetClass();

			'#Region "Parse RPC text file"
			' Use the stream reader to open the file and read lines from it.
			Using sr As New StreamReader(rpcFilePath)
                Dim line As String = ""
				Dim lineNumber As Integer = 0

				While (InlineAssignHelper(line, sr.ReadLine())) IsNot Nothing
					lineNumber += 1
					Try
						' Split the line into tokens based on delimiters
						Dim delimiters As Char() = {":"C, " "C}
						Dim tokens As String() = line.Split(delimiters, System.StringSplitOptions.RemoveEmptyEntries)
						Dim numTokens As Integer = tokens.GetLength(0)
						If numTokens > 1 Then
							Dim currPar As String = tokens(0)
							Dim currValue As Double = Convert.ToDouble(tokens(1))
							' Convert the Value to a double and store in the array.
								' Store the property and the value in the propertyset to lookup later if needed.
								'coefficients.SetProperty(currPar, currValue);
								' Read units for conversion if needed
								'string currUnit = tokens[2];
							RPC((lineNumber - 1)) = currValue
						Else
							Console.WriteLine("Could not parse line " & lineNumber.ToString())
						End If
					Catch ex As Exception
						Console.Write(ex.ToString())
					End Try
				End While
				sr.Close()
			End Using
			'#End Region

			' Create the new RPCXForm from the parameter array.
			Dim myRPCXform As IRPCXform = DirectCast(New RPCXform(), IRPCXform)
			Dim rpcCoeffs As Object = DirectCast(RPC, Object)
			myRPCXform.DefineFromCoefficients(rpcCoeffs)

			Return myRPCXform
		Catch exc As Exception
			Dim [error] As String = exc.Message
			Throw exc
		End Try
	End Function

	''' <summary>
	''' Create new fields to add to the mosaic dataset attribute table.
	''' </summary>
	''' <param name="myFields">Fields to be added.</param>
	Private Sub AddFields(myFields As IFields)
		' Create a new field object
		Dim pField As IField = New FieldClass()
		' Set the field editor for this field
		Dim objectIDFieldEditor As IFieldEdit = DirectCast(pField, IFieldEdit)
		' Set the name and alias of the field
		objectIDFieldEditor.Name_2 = "SensorName"
		objectIDFieldEditor.AliasName_2 = "Sensor Name"
		' Set the type of the field
		objectIDFieldEditor.Type_2 = esriFieldType.esriFieldTypeString
		' Add the newly created field to list of existing fields
		Dim fieldsEditor As IFieldsEdit = DirectCast(myFields, IFieldsEdit)
		fieldsEditor.AddField(pField)

		' Create a new field object
		pField = New FieldClass()
		' Set the field editor for this field
		objectIDFieldEditor = DirectCast(pField, IFieldEdit)
		' Set the name and alias of the field
		objectIDFieldEditor.Name_2 = "AcquisitionDate"
		objectIDFieldEditor.AliasName_2 = "Acquisition Date"
		' Set the type of the field
		objectIDFieldEditor.Type_2 = esriFieldType.esriFieldTypeDate
		fieldsEditor.AddField(pField)

		' Create a new field object
		pField = New FieldClass()
		' Set the field editor for this field
		objectIDFieldEditor = DirectCast(pField, IFieldEdit)
		' Set the name and alias of the field
		objectIDFieldEditor.Name_2 = "SunAzimuth"
		objectIDFieldEditor.AliasName_2 = "Sun Azimuth"
		' Set the type of the field
		objectIDFieldEditor.Type_2 = esriFieldType.esriFieldTypeDouble
		fieldsEditor.AddField(pField)

		' Create a new field object
		pField = New FieldClass()
		' Set the field editor for this field
		objectIDFieldEditor = DirectCast(pField, IFieldEdit)
		' Set the name and alias of the field
		objectIDFieldEditor.Name_2 = "SunElevation"
		objectIDFieldEditor.AliasName_2 = "Sun Elevation"
		' Set the type of the field
		objectIDFieldEditor.Type_2 = esriFieldType.esriFieldTypeDouble
		fieldsEditor.AddField(pField)

		' Create a new field object
		pField = New FieldClass()
		' Set the field editor for this field
		objectIDFieldEditor = DirectCast(pField, IFieldEdit)
		' Set the name and alias of the field
		objectIDFieldEditor.Name_2 = "SatAzimuth"
		objectIDFieldEditor.AliasName_2 = "Satellite Azimuth"
		' Set the type of the field as Blob
		objectIDFieldEditor.Type_2 = esriFieldType.esriFieldTypeDouble
		fieldsEditor.AddField(pField)

		' Create a new field object
		pField = New FieldClass()
		' Set the field editor for this field
		objectIDFieldEditor = DirectCast(pField, IFieldEdit)
		' Set the name and alias of the field
		objectIDFieldEditor.Name_2 = "SatElevation"
		objectIDFieldEditor.AliasName_2 = "Satellite Elevation"
		' Set the type of the field as Blob
		objectIDFieldEditor.Type_2 = esriFieldType.esriFieldTypeDouble
		fieldsEditor.AddField(pField)
	End Sub
	#End Region

	#Region "IRasterBuilderInit Members"

	Public Property DefaultSpatialReference() As ISpatialReference Implements IRasterBuilderInit.DefaultSpatialReference, IRasterBuilderInit2.DefaultSpatialReference
		Get
			Return myDefaultSpatialReference
		End Get
		Set
			myDefaultSpatialReference = value
		End Set
	End Property

	Public Property MosaicDataset() As IMosaicDataset Implements IRasterBuilderInit.MosaicDataset, IRasterBuilderInit2.MosaicDataset
		Get
			Return myMosaicDataset
		End Get
		Set
			myMosaicDataset = value
		End Set
	End Property

	Public Property RasterTypeOperation() As IRasterTypeOperation Implements IRasterBuilderInit.RasterTypeOperation, IRasterBuilderInit2.RasterTypeOperation
		Get
			Return myRasterTypeOperation
		End Get
		Set
			myRasterTypeOperation = value
		End Set
	End Property

	Public Property TrackCancel() As ITrackCancel Implements IRasterBuilderInit.TrackCancel, IRasterBuilderInit2.TrackCancel
		Get
			Return myTrackCancel
		End Get
		Set
			myTrackCancel = value
		End Set
	End Property

	#End Region

	#Region "IPersistVariant Members"
	''' <summary>
	''' UID for the object implementing the Persist Variant
	''' </summary>
	Public ReadOnly Property ID() As UID Implements IPersistVariant.ID
		Get
			Return myUID
		End Get
	End Property

	''' <summary>
	''' Load the object from the stream provided
	''' </summary>
	''' <param name="Stream">Stream that represents the serialized Raster Type</param>
	Public Sub Load(Stream As IVariantStream) Implements IPersistVariant.Load
		Dim name As String = DirectCast(Stream.Read(), String)
		'if (innerRasterBuilder is IPersistVariant)
		'    ((IPersistVariant)innerRasterBuilder).Load(Stream);
		'innerRasterBuilder = (IRasterBuilder)Stream.Read(); // Load the innerRasterBuilder from the stream.
	End Sub

	''' <summary>
	''' Same the Raster Type to the stream provided
	''' </summary>
	''' <param name="Stream">Stream to serialize the Raster Type into</param>
	Public Sub Save(Stream As IVariantStream) Implements IPersistVariant.Save
		Stream.Write("CustomRasterType")
		'if (innerRasterBuilder is IPersistVariant)
		'    ((IPersistVariant)innerRasterBuilder).Save(Stream);
		'Stream.Write(innerRasterBuilder); // Save the innerRasterBuilder into the stream.
	End Sub

	#End Region

	#Region "IRasterBuilder2 Members"
	''' <summary>
	''' Check if the data source provided is a valid data source for the builder.
	''' </summary>
	''' <param name="vtDataSource">Data source (usually the path to a metadta file)</param>
	''' <returns>Flag to specify whether it is  valid source.</returns>
	Public Function CanBuild(vtDataSource As Object) As Boolean Implements IRasterBuilder2.CanBuild
		If Not (TypeOf vtDataSource Is String) Then
			Return False
		End If
		Dim dimFilePath As String = DirectCast(vtDataSource, String)
		If Not dimFilePath.ToLower().EndsWith(".dim") Then
			Return False
		End If
		Dim myDimParser As DiMapParser = Nothing
		Try
			myDimParser = New DiMapParser(dimFilePath)
			If myDimParser.MetadataProfile.ToLower() = "DMCII".ToLower() Then
				myDimParser = Nothing
				Return True
			Else
				myDimParser = Nothing
				Return False
			End If
		Catch exc As Exception
			myDimParser = Nothing
			Dim [error] As String = exc.Message
			Return False
		End Try
	End Function

	Public ReadOnly Property CanMergeItems() As Boolean Implements IRasterBuilder2.CanMergeItems
		Get
			Return myCanMergeItems
		End Get
	End Property

	Public Property MergeItems() As Boolean Implements IRasterBuilder2.MergeItems
		Get
			Return myMergeItems
		End Get
		Set
			myMergeItems = value
		End Set
	End Property

	''' <summary>
	''' Check to see if the properties provided to the raster type/builder 
	''' are sufficient for it to work. Usually used for UI validation.
	''' </summary>
	Public Sub Validate() Implements IRasterBuilder2.Validate
		Return
	End Sub
	#End Region

	#Region "IRasterBuilderInit2 Members"

	''' <summary>
	''' Helper object to store geographic transformations set on the mosaic dataset.
	''' </summary>
	Public Property GeoTransformationHelper() As IGeoTransformationHelper Implements IRasterBuilderInit2.GeoTransformationHelper
		Get
			Return myGeoTransformationHelper
		End Get
		Set
			myGeoTransformationHelper = Nothing
		End Set
	End Property
	Private Shared Function InlineAssignHelper(Of T)(ByRef target As T, value As T) As T
		target = value
		Return value
	End Function

	#End Region
End Class

''' <summary>
''' Class used to parse the dim file (xml format) and get relevant properties from it.
''' </summary>
Public Class DiMapParser
	Private myXmlPath As String
	Private myXmlDoc As XmlDocument
	Private bandInfo As XmlNodeList
	Private bandStats As XmlNodeList
	Private footprintInfo As XmlNodeList
	Private vertexCount As Integer

	Public Sub New()
		myXmlPath = Nothing
		bandInfo = Nothing
		bandStats = Nothing
		footprintInfo = Nothing
		vertexCount = 0
	End Sub

	Public Sub New(xmlPath As String)
		myXmlPath = xmlPath
		bandInfo = Nothing
		bandStats = Nothing
		footprintInfo = Nothing
		vertexCount = 0
		myXmlDoc = New XmlDocument()
		myXmlDoc.Load(myXmlPath)
	End Sub

	''' <summary>
	''' Flag to specify whether the footprint exists in the xml file.
	''' </summary>
	Public ReadOnly Property FootPrintExists() As Boolean
		Get
			If footprintInfo Is Nothing Then
				footprintInfo = myXmlDoc.SelectSingleNode("//Dataset_Frame").SelectNodes("Vertex")
			End If
			Return footprintInfo IsNot Nothing
		End Get
	End Property

	''' <summary>
	''' Reset the vertex count to get vertices of the footprint.
	''' </summary>
	Public Sub ResetVertexCount()
		vertexCount = 0
	End Sub

	Public Function GetNextVertex(ByRef x As String, ByRef y As String, ByRef col As String, ByRef row As String) As Boolean
		If footprintInfo Is Nothing Then
			footprintInfo = myXmlDoc.SelectSingleNode("//Dataset_Frame").SelectNodes("Vertex")
		End If
		x = footprintInfo(vertexCount).SelectSingleNode("FRAME_LON").InnerText
		y = footprintInfo(vertexCount).SelectSingleNode("FRAME_LAT").InnerText
		col = footprintInfo(vertexCount).SelectSingleNode("FRAME_COL").InnerText
		row = footprintInfo(vertexCount).SelectSingleNode("FRAME_ROW").InnerText
		vertexCount += 1

		If vertexCount >= footprintInfo.Count Then
			Return False
		Else
			Return True
		End If
	End Function

	''' <summary>
	''' Get next vertex from the footprint defined in the xml based on the vertex count and unit.
	''' </summary>
	''' <param name="x">The X value.</param>
	''' <param name="y">The Y value.</param>
	''' <param name="unit">Unit to check which parameter to get vertex from.</param>
	''' <returns>True if next vertex exists.</returns>
	Public Function GetNextVertex2(ByRef x As String, ByRef y As String, unit As String) As Boolean
		If unit = "Deg" Then
			If footprintInfo Is Nothing Then
				footprintInfo = myXmlDoc.SelectSingleNode("//Dataset_Frame").SelectNodes("Vertex")
			End If
			x = footprintInfo(vertexCount).SelectSingleNode("FRAME_LON").InnerText
				'col = footprintInfo[vertexCount].SelectSingleNode("FRAME_COL").InnerText;
				'row = footprintInfo[vertexCount].SelectSingleNode("FRAME_ROW").InnerText;
			y = footprintInfo(vertexCount).SelectSingleNode("FRAME_LAT").InnerText
		Else
			If footprintInfo Is Nothing Then
				footprintInfo = myXmlDoc.SelectSingleNode("//Dataset_Frame").SelectNodes("Vertex")
			End If
			x = footprintInfo(vertexCount).SelectSingleNode("FRAME_X").InnerText
				'col = footprintInfo[vertexCount].SelectSingleNode("FRAME_COL").InnerText;
				'row = footprintInfo[vertexCount].SelectSingleNode("FRAME_ROW").InnerText;
			y = footprintInfo(vertexCount).SelectSingleNode("FRAME_Y").InnerText
		End If
		vertexCount += 1

		If vertexCount >= footprintInfo.Count Then
			Return False
		Else
			Return True
		End If
	End Function

	''' <summary>
	''' The number of bands defined in the xml.
	''' </summary>
	Public ReadOnly Property NumBands() As Integer
		Get
			If bandInfo Is Nothing Then
				bandInfo = myXmlDoc.SelectNodes("//Spectral_Band_Info")
			End If
			If bandStats Is Nothing Then
				bandStats = myXmlDoc.SelectNodes("//Band_Statistics")
			End If
			Return bandInfo.Count
		End Get
	End Property

	''' <summary>
	''' Index of the band based on the counter.
	''' </summary>
	''' <param name="indexCounter">Counter (similar to vertexCount) to get the index for.</param>
	''' <returns>Index of the band as string.</returns>
	Public Function GetBandIndex(indexCounter As Integer) As String
		If bandInfo Is Nothing Then
			bandInfo = myXmlDoc.SelectNodes("//Spectral_Band_Info")
		End If
		Return bandInfo(indexCounter).SelectSingleNode("BAND_INDEX").InnerText
	End Function

	''' <summary>
	''' Get the name of the band.
	''' </summary>
	''' <param name="bandIndex">Index of the band for which to get the name.</param>
	''' <returns>Band name as string.</returns>
	Public Function GetBandDesc(bandIndex As Integer) As String
		If bandInfo Is Nothing Then
			bandInfo = myXmlDoc.SelectNodes("//Spectral_Band_Info")
		End If
		Return bandInfo(bandIndex - 1).SelectSingleNode("BAND_DESCRIPTION").InnerText
	End Function

	''' <summary>
	''' Get minimum value for the band.
	''' </summary>
	''' <param name="bandIndex">Index of the band for which to get the value.</param>
	''' <returns>Value requested as string.</returns>
	Public Function GetBandStatMin(bandIndex As Integer) As String
		If bandStats Is Nothing Then
			bandStats = myXmlDoc.SelectNodes("//Band_Statistics")
		End If
		Return bandStats(bandIndex - 1).SelectSingleNode("STX_LIN_MIN").InnerText
	End Function

	''' <summary>
	''' Get maximum value for the band.
	''' </summary>
	''' <param name="bandIndex">Index of the band for which to get the value.</param>
	''' <returns>Value requested as string.</returns>
	Public Function GetBandStatMax(bandIndex As Integer) As String
		If bandStats Is Nothing Then
			bandStats = myXmlDoc.SelectNodes("//Band_Statistics")
		End If
		Return bandStats(bandIndex - 1).SelectSingleNode("STX_LIN_MAX").InnerText
	End Function

	''' <summary>
	''' Get mean value for the band.
	''' </summary>
	''' <param name="bandIndex">Index of the band for which to get the value.</param>
	''' <returns>Value requested as string.</returns>
	Public Function GetBandStatMean(bandIndex As Integer) As String
		If bandStats Is Nothing Then
			bandStats = myXmlDoc.SelectNodes("//Band_Statistics")
		End If
		Return bandStats(bandIndex - 1).SelectSingleNode("STX_MEAN").InnerText
	End Function

	''' <summary>
	''' Get standard deviation value for the band.
	''' </summary>
	''' <param name="bandIndex">Index of the band for which to get the value.</param>
	''' <returns>Value requested as string.</returns>
	Public Function GetBandStatStdDev(bandIndex As Integer) As String
		If bandStats Is Nothing Then
			bandStats = myXmlDoc.SelectNodes("//Band_Statistics")
		End If
		Return bandStats(bandIndex - 1).SelectSingleNode("STX_STDV").InnerText
	End Function

	''' <summary>
	''' Get the product type for the dataset.
	''' </summary>
	Public ReadOnly Property ProductType() As String
		Get
			Return myXmlDoc.SelectSingleNode("//PRODUCT_TYPE").InnerText
		End Get
	End Property

	''' <summary>
	''' Get the sensor name for the dataset.
	''' </summary>
	Public ReadOnly Property MetadataProfile() As String
		Get
			Return myXmlDoc.SelectSingleNode("//METADATA_PROFILE").InnerText
		End Get
	End Property

	''' <summary>
	''' Get the geometric processing for the dataset.
	''' </summary>
	Public ReadOnly Property GeometricProcessing() As String
		Get
			Return myXmlDoc.SelectSingleNode("//GEOMETRIC_PROCESSING").InnerText
		End Get
	End Property

	''' <summary>
	''' Get the Acquisition Date for the dataset.
	''' </summary>
	Public ReadOnly Property AcquisitionDate() As String
		Get
			Return myXmlDoc.SelectSingleNode("//IMAGING_DATE").InnerText
		End Get
	End Property

	''' <summary>
	''' Get the Acquisition Time for the dataset.
	''' </summary>
	Public ReadOnly Property AcquisitionTime() As String
		Get
			Return myXmlDoc.SelectSingleNode("//IMAGING_TIME").InnerText
		End Get
	End Property

	''' <summary>
	''' Get the Sensor Angle for the dataset.
	''' </summary>
	Public ReadOnly Property IncidenceAngle() As String
		Get
			Return myXmlDoc.SelectSingleNode("//INCIDENCE_ANGLE").InnerText
		End Get
	End Property

	''' <summary>
	''' Get the Sun Azimuth for the dataset.
	''' </summary>
	Public ReadOnly Property SunAzimuth() As String
		Get
			Return myXmlDoc.SelectSingleNode("//SUN_AZIMUTH").InnerText
		End Get
	End Property

	''' <summary>
	''' Get the Sun Elevation for the dataset.
	''' </summary>
	Public ReadOnly Property SunElevation() As String
		Get
			Return myXmlDoc.SelectSingleNode("//SUN_ELEVATION").InnerText
		End Get
	End Property

	''' <summary>
	''' Get the epsg code for the spatial reference of the dataset.
	''' </summary>
	Public ReadOnly Property SrsCode() As String
		Get
			Return myXmlDoc.SelectSingleNode("//HORIZONTAL_CS_CODE").InnerText
		End Get
	End Property

	''' <summary>
	''' Get the Sensor Azimuth for the dataset.
	''' </summary>
	Public ReadOnly Property SensorAzimuth() As String
		Get
			Dim qualityAssessments As XmlNodeList = myXmlDoc.SelectNodes("//Quality_Assessment")
			Dim qualityPars As XmlNodeList = qualityAssessments(1).SelectNodes("Quality_Parameter")
			For i As Integer = 0 To qualityPars.Count - 1
				If qualityPars(i).SelectSingleNode("QUALITY_PARAMETER_CODE").InnerText.Contains("SENSOR_AZIMUTH") Then
					Return qualityPars(i).SelectSingleNode("QUALITY_PARAMETER_VALUE").InnerText
				End If
			Next
			Return ""
		End Get
	End Property
End Class
