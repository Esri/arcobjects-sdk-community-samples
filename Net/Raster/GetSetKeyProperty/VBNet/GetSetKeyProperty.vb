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
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports ESRI.ArcGIS
Imports ESRI.ArcGIS.DataSourcesGDB
Imports ESRI.ArcGIS.DataSourcesRaster
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Geometry

'
'    This sample opens a Mosaic Dataset and goes through each row in the attribute table.
'    It checks whether the dataset in the row has any band information (band properties) 
'    associated with it. 
'    If the item has no band information, it inserts band properties for the first 3 bands 
'    in the item (if the item has 3 or more bands).
'    Finally the mosaic dataset product definition is changed to Natural Color RGB so that 
'    ArcGIS can use the band names of the mosaic dataset.
'    The sample also shows how to set a key property on the mosaic dataset.
'
'    The sample has functions to get/set any key property for a dataset.
'    The sample has functions to get/set any band property for a dataset.
'    The sample has functions to get all the properties and all the band properties 
'    for a dataset.
'
'    Key Properties:
'
'    Key Properties of type 'double':
'    CloudCover
'    SunElevation
'    SunAzimuth
'    SensorElevation
'    SensorAzimuth
'    OffNadir
'    VerticalAccuracy
'    HorizontalAccuracy
'    LowCellSize
'    HighCellSize
'    MinCellSize
'    MaxCellSize
'
'    Key Properties of type 'date':
'    AcquisitionDate
'
'    Key Properties of type 'string':
'    SensorName
'    ParentRasterType
'    DataType
'    ProductName
'    DatasetTag
'


Namespace RasterSamples
	Class GetSetKeyProperty
		<STAThread> _
		Friend Shared Sub Main(args As String())
			'#Region "Initialize"
			Dim aoInit As ESRI.ArcGIS.esriSystem.AoInitialize = Nothing
			Try
				Console.WriteLine("Obtaining license")
				ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.Desktop)
                aoInit = New AoInitialize()
				Dim licStatus As esriLicenseStatus = aoInit.Initialize(esriLicenseProductCode.esriLicenseProductCodeAdvanced)
				Console.WriteLine("Ready with license.")
			Catch exc As Exception
				' If it fails at this point, shutdown the test and ignore any subsequent errors.
				Console.WriteLine(exc.Message)
			End Try
			'#End Region

			Try
				' Input database and Mosaic Dataset
				Dim MDWorkspaceFolder As String = "e:\md\Samples\GetSetKP\RasterSamples.gdb"
				Dim MDName As String = "LAC"

				' Command line setting of above input if provided.
				Dim commandLineArgs As String() = Environment.GetCommandLineArgs()
				If commandLineArgs.GetLength(0) > 1 Then
					MDWorkspaceFolder = commandLineArgs(1)
					MDName = commandLineArgs(2)
				End If

				' Open MD
				Dim factoryType As Type = Type.GetTypeFromProgID("esriDataSourcesGDB.FileGDBWorkspaceFactory")
				Dim mdWorkspaceFactory As IWorkspaceFactory = DirectCast(Activator.CreateInstance(factoryType), IWorkspaceFactory)
				Dim mdWorkspace As IWorkspace = mdWorkspaceFactory.OpenFromFile(MDWorkspaceFolder, 0)
				Dim workspaceEx As IRasterWorkspaceEx = DirectCast(mdWorkspace, IRasterWorkspaceEx)
				Dim mosaicDataset As IMosaicDataset = DirectCast(workspaceEx.OpenRasterDataset(MDName), IMosaicDataset)

				' Set Mosaic Dataset item information.
				SetMosaicDatasetItemInformation(mosaicDataset)

				' Set Key Property 'DataType' on the Mosaic Dataset to value 'Processed'
				' The change will be reflected on the 'General' page of the mosaic dataset
				' properties under the 'Source Type' property.
				SetKeyProperty(DirectCast(mosaicDataset, IDataset), "DataType", "Processed")

				' Set the Product Definition on the Mosaic Dataset to 'NATURAL_COLOR_RGB'
				' First set the 'BandDefinitionKeyword' key property to natural color RGB.
				SetKeyProperty(DirectCast(mosaicDataset, IDataset), "BandDefinitionKeyword", "NATURAL_COLOR_RGB")
				' Then set band names and wavelengths on the mosaic dataset.
				SetBandProperties(DirectCast(mosaicDataset, IDataset))
				' Last and most important, refresh the mosaic dataset so the changes are saved.
				DirectCast(mosaicDataset, IRasterDataset3).Refresh()

				'#Region "Shutdown"
				Console.WriteLine("Success.")
				Console.WriteLine("Press any key...")
				Console.ReadKey()

				' Shutdown License
					'#End Region
				aoInit.Shutdown()
			Catch exc As Exception
				'#Region "Shutdown"
				Console.WriteLine("Exception Caught while creating Function Raster Dataset. " & exc.Message)
				Console.WriteLine("Failed.")
				Console.WriteLine("Press any key...")
				Console.ReadKey()

				' Shutdown License
					'#End Region
				aoInit.Shutdown()
			End Try
		End Sub


		''' <summary>
		''' Sets band information on items in a mosaic dataset
		''' </summary>
		''' <param name="md">The mosaic dataset with the items</param>
		Private Shared Sub SetMosaicDatasetItemInformation(md As IMosaicDataset)
			' Get the Attribute table from the Mosaic Dataset.
			Dim featureClass As IFeatureClass = md.Catalog
			Dim schemaLock As ISchemaLock = DirectCast(featureClass, ISchemaLock)
			Dim rasDs As IRasterDataset3 = Nothing
			Try
				' A try block is necessary, as an exclusive lock might not be available.
				schemaLock.ChangeSchemaLock(esriSchemaLock.esriExclusiveSchemaLock)

				' Get an update cursor going through all the rows in the Moasic Dataset.
				Dim fcCursor As IFeatureCursor = featureClass.Update(Nothing, False)
				' Alternatively, a read cursor can be used if the item does not need to be changed.
				' featureClass.Search(null, false);

				' For each row,
				Dim rasCatItem As IRasterCatalogItem = DirectCast(fcCursor.NextFeature(), IRasterCatalogItem)
				While rasCatItem IsNot Nothing
					' get the functionrasterdataset from the Raster field.
					Dim funcDs As IFunctionRasterDataset = DirectCast(rasCatItem.RasterDataset, IFunctionRasterDataset)
					If funcDs IsNot Nothing Then
						' Check if the 'BandName' property exists in the dataset.
						Dim propertyExists As Boolean = False
						For bandID As Integer = 0 To funcDs.RasterInfo.BandCount - 1
							Dim bandNameProperty As Object = Nothing
							bandNameProperty = GetBandProperty(DirectCast(funcDs, IDataset), "BandName", bandID)
							If bandNameProperty IsNot Nothing Then
								propertyExists = True
							End If
						Next
						If propertyExists = False AndAlso funcDs.RasterInfo.BandCount > 2 Then
							' If the property does not exist and the dataset has atleast 3 bands,
							' set Band Definition Properties for first 3 bands of the dataset.
							SetBandProperties(DirectCast(funcDs, IDataset))
							funcDs.AlterDefinition()
							rasDs = DirectCast(funcDs, IRasterDataset3)
							' Refresh the dataset.
							rasDs.Refresh()
						End If
					End If
					fcCursor.UpdateFeature(DirectCast(rasCatItem, IFeature))
					rasDs = Nothing
					rasCatItem = DirectCast(fcCursor.NextFeature(), IRasterCatalogItem)
				End While
				rasCatItem = Nothing
				fcCursor = Nothing
				featureClass = Nothing
			Catch exc As Exception
				Console.WriteLine("Exception Caught in SetMosaicDatasetItemInformation: " & exc.Message)
			Finally
				' Set the lock to shared, whether or not an error occurred.
				schemaLock.ChangeSchemaLock(esriSchemaLock.esriSharedSchemaLock)
			End Try
		End Sub

		''' <summary>
		''' Sets band properties on a given dataset
		''' </summary>
		''' <param name="ds">The target dataset</param>
		Private Shared Sub SetBandProperties(dataset As IDataset)
			Try
				' Set properties for Band 1.
				SetBandProperty(dataset, "BandName", 0, "Red")
				SetBandProperty(dataset, "WavelengthMin", 0, 630)
				SetBandProperty(dataset, "WavelengthMax", 0, 690)

				' Set properties for Band 2.
				SetBandProperty(dataset, "BandName", 1, "Green")
				SetBandProperty(dataset, "WavelengthMin", 1, 530)
				SetBandProperty(dataset, "WavelengthMax", 1, 570)

				' Set properties for Band 3.
				SetBandProperty(dataset, "BandName", 2, "Blue")
				SetBandProperty(dataset, "WavelengthMin", 2, 440)
				SetBandProperty(dataset, "WavelengthMax", 2, 480)
			Catch generatedExceptionName As Exception
			End Try
		End Sub

		''' <summary>
		''' Get all the properties associated with the dataset.
		''' </summary>
		''' <param name="dataset">Dataset to get the property from.</param>
		''' <param name="allKeys">String Array passed in by reference to fill with all keys.</param>
		''' <param name="allProperties">Object array passed in by reference to fill with all properties.</param>
		Private Shared Sub GetAllProperties(dataset As IDataset, ByRef allKeys As IStringArray, ByRef allProperties As IVariantArray)
			Dim rasterKeyProps As IRasterKeyProperties = DirectCast(dataset, IRasterKeyProperties)
			rasterKeyProps.GetAllProperties(allKeys, allProperties)
		End Sub

		''' <summary>
		''' Get all the properties associated with a particular band of the dataset.
		''' </summary>
		''' <param name="dataset">Dataset to get the property from.</param>
		''' <param name="bandIndex">band for which to get all properties.</param>
		''' <param name="bandKeys">String Array passed in by reference to fill with all keys.</param>
		''' <param name="bandProperties">Object array passed in by reference to fill with all properties.</param>
		Private Shared Sub GetAllBandProperties(dataset As IDataset, bandIndex As Integer, ByRef bandKeys As IStringArray, ByRef bandProperties As IVariantArray)
			Dim rasterKeyProps As IRasterKeyProperties = DirectCast(dataset, IRasterKeyProperties)
			rasterKeyProps.GetAllBandProperties(bandIndex, bandKeys, bandProperties)
		End Sub

		''' <summary>
		''' Get the Key Property corresponding to the key 'key' from the dataset.
		''' </summary>
		''' <param name="dataset">Dataset to get the property from.</param>
		''' <param name="key">The key for which to get the value.</param>
		''' <returns>Property corresponding to the key or null if it doesnt exist.</returns>
		Private Shared Function GetKeyProperty(dataset As IDataset, key As String) As Object
			Dim rasterKeyProps As IRasterKeyProperties = DirectCast(dataset, IRasterKeyProperties)
			Dim value As Object = Nothing
			Try
				value = rasterKeyProps.GetProperty(key)
			Catch generatedExceptionName As Exception
			End Try
			Return value
		End Function

		''' <summary>
		''' Set the Key Property 'value' corresponding to the key 'key' on the dataset.
		''' </summary>
		''' <param name="dataset">Dataset to set the property on.</param>
		''' <param name="key">The key on which to set the property.</param>
		''' <param name="value">The value to set.</param>
		Private Shared Sub SetKeyProperty(dataset As IDataset, key As String, value As Object)
			Dim rasterKeyProps As IRasterKeyProperties = DirectCast(dataset, IRasterKeyProperties)
			rasterKeyProps.SetProperty(key, value)
		End Sub

		''' <summary>
		''' Get the KeyProperty corresponding to the bandIndex and 'key' from the dataset.
		''' </summary>
		''' <param name="dataset">Dataset to get the property from.</param>
		''' <param name="key">The key for which to get the value.</param>
		''' <param name="bandIndex">Band for which to get the property.</param>
		''' <returns>Property corresponding to the key or null if none found.</returns>
		Private Shared Function GetBandProperty(dataset As IDataset, key As String, bandIndex As Integer) As Object
			Dim rasterKeyProps As IRasterKeyProperties = DirectCast(dataset, IRasterKeyProperties)
			Dim value As Object = Nothing
			Try
				value = rasterKeyProps.GetBandProperty(key, bandIndex)
			Catch generatedExceptionName As Exception
			End Try

			Return value
		End Function

		''' <summary>
		''' Set the KeyProperty corresponding to the bandIndex and 'key' from the dataset.
		''' </summary>
		''' <param name="dataset">Dataset to set the property on.</param>
		''' <param name="key">The key on which to set the property.</param>
		''' <param name="bandIndex">Band from which to get the property.</param>
		''' <param name="value">The value to set.</param>
		Private Shared Sub SetBandProperty(dataset As IDataset, key As String, bandIndex As Integer, value As Object)
			Dim rasterKeyProps As IRasterKeyProperties = DirectCast(dataset, IRasterKeyProperties)
			rasterKeyProps.SetBandProperty(key, bandIndex, value)
		End Sub
	End Class
End Namespace
