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
Imports System
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.NetworkAnalyst
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Display
Imports ESRI.ArcGIS.Geoprocessor
Imports ESRI.ArcGIS.DataManagementTools

' Main class that checks out the appropriate ArcGIS license and calls RouteClass.SolveRoute to perform the analysis
Module Program
	Private m_AOLicenseInitializer As LicenseInitializer


	Sub Main()

		If (Not ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.Engine)) Then
			If (Not ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.Desktop)) Then
				Console.WriteLine("This application could not load the correct version of ArcGIS.")
			End If
		End If

		m_AOLicenseInitializer = New LicenseInitializer()

		'ESRI License Initializer generated code.
		If (Not m_AOLicenseInitializer.InitializeApplication(New esriLicenseProductCode() {esriLicenseProductCode.esriLicenseProductCodeBasic, esriLicenseProductCode.esriLicenseProductCodeStandard, esriLicenseProductCode.esriLicenseProductCodeAdvanced}, _
		New esriLicenseExtensionCode() {esriLicenseExtensionCode.esriLicenseExtensionCodeNetwork})) Then
			Console.WriteLine(m_AOLicenseInitializer.LicenseMessage())
			Console.WriteLine("This application could not initialize with the correct ArcGIS license and will shutdown.")
			m_AOLicenseInitializer.ShutdownApplication()
			Return
		End If

		Dim routeClass As RouteClass = New RouteClass
		routeClass.SolveRoute()

		'ESRI License Initializer generated code.
		'Do not make any call to ArcObjects after ShutDownApplication()
		m_AOLicenseInitializer.ShutdownApplication()
	End Sub

End Module

'The RouteClass class is the workhorse class that does the analysis and writes it to disk
Public Class RouteClass
	Private Const FGDB_WORKSPACE As String = "..\..\..\..\..\Data\SanFrancisco\SanFrancisco.gdb"
	Private Const INPUT_STOPS_FC As String = "Stores"
	Private Const SHAPE_INPUT_NAME_FIELD As String = "Name"
	Private Const FEATURE_DATASET As String = "Transportation"
	Private Const NETWORK_DATASET As String = "Streets_ND"

	'Create the analysis layer, load the locations, solve the analysis, and write to disk
	Public Sub SolveRoute()
		' Open the feature workspace, input feature class, and network dataset
		' As Workspace Factories are Singleton objects, they must be instantiated with the Activator
		Dim workspaceFactory As IWorkspaceFactory = Activator.CreateInstance(Type.GetTypeFromProgID("esriDataSourcesGDB.FileGDBWorkspaceFactory"))
		Dim featureWorkspace As IFeatureWorkspace = workspaceFactory.OpenFromFile(FGDB_WORKSPACE, 0)
		Dim inputStopsFClass As IFeatureClass = featureWorkspace.OpenFeatureClass(INPUT_STOPS_FC)

		' Obtain the dataset container from the workspace
		Dim featureDataset As IFeatureDataset = featureWorkspace.OpenFeatureDataset(FEATURE_DATASET)
		Dim featureDatasetExtensionContainer As IFeatureDatasetExtensionContainer = featureDataset
		Dim featureDatasetExtension As IFeatureDatasetExtension = featureDatasetExtensionContainer.FindExtension(ESRI.ArcGIS.Geodatabase.esriDatasetType.esriDTNetworkDataset)
		Dim datasetContainer As IDatasetContainer3 = featureDatasetExtension

		' Use the container to open the network dataset.
		Dim dataset As IDataset = datasetContainer.DatasetByName(ESRI.ArcGIS.Geodatabase.esriDatasetType.esriDTNetworkDataset, NETWORK_DATASET)
		Dim networkDataset As INetworkDataset = dataset

		' Create the Route NALayer
		Dim naLayer As INALayer = CreateRouteAnalysisLayer("Route", networkDataset)
		Dim naContext As INAContext = naLayer.Context
		Dim stopsNAClass As INAClass = naContext.NAClasses.ItemByName("Stops")
		Dim routesFC As IFeatureClass = naContext.NAClasses.ItemByName("Routes")

		' Load the Stops
		Dim naClassFieldMap As INAClassFieldMap = New NAClassFieldMapClass()
		naClassFieldMap.MappedField("Name") = SHAPE_INPUT_NAME_FIELD

		Dim naLoader As INAClassLoader = New NAClassLoaderClass()
		naLoader.Locator = naContext.Locator
		naLoader.NAClass = stopsNAClass
		naLoader.FieldMap = naClassFieldMap

		' Avoid loading network locations onto non-traversable portions of elements
		Dim locator As INALocator3 = TryCast(naContext.Locator, INALocator3)
		locator.ExcludeRestrictedElements = True
		locator.CacheRestrictedElements(naContext)

		Dim rowsInCursor As Integer = 0
		Dim rowsLocated As Integer = 0
		naLoader.Load(inputStopsFClass.Search(New QueryFilterClass(), False), New CancelTrackerClass(), rowsInCursor, rowsLocated)

		' Message all of the network analysis agents that the analysis context has changed
		Dim naContextEdit As INAContextEdit = naContext
		naContextEdit.ContextChanged()

		'Solve
		Dim naSolver As INASolver = naContext.Solver
		naSolver.Solve(naContext, New GPMessagesClass(), New CancelTrackerClass())

		'Save the layer to disk
		SaveLayerToDisk(naLayer, System.Environment.CurrentDirectory + "\Route.lyr")
	End Sub

	'Create a new network analysis layer and set some solver settings
	Private Function CreateRouteAnalysisLayer(ByVal layerName As String, ByVal networkDataset As INetworkDataset) As INALayer
		Dim naRouteSolver As INARouteSolver = New NARouteSolverClass()
		Dim naSolverSettings As INASolverSettings = naRouteSolver
		Dim naSolver As INASolver = naRouteSolver

		'Get the NetworkDataset's Data Element
		Dim datasetComponent As IDatasetComponent = networkDataset
		Dim deNetworkDataset As IDENetworkDataset = datasetComponent.DataElement

		'Create the NAContext and bind to it
		Dim naContext As INAContext = naSolver.CreateContext(deNetworkDataset, layerName)
		Dim naContextEdit As INAContextEdit = naContext
		naContextEdit.Bind(networkDataset, New GPMessagesClass())

		'Create the NALayer
		Dim naLayer As INALayer = naSolver.CreateLayer(naContext)
		Dim layer As ILayer = naLayer
		layer.Name = layerName

		'Set some properties on the route solver interface
		naRouteSolver.FindBestSequence = True
		naRouteSolver.PreserveFirstStop = True
		naRouteSolver.PreserveLastStop = False
		naRouteSolver.UseTimeWindows = False
		naRouteSolver.OutputLines = esriNAOutputLineType.esriNAOutputLineTrueShapeWithMeasure

		'Set some properties on the general INASolverSettings interface
		Dim restrictions As IStringArray = naSolverSettings.RestrictionAttributeNames
		restrictions.Add("Oneway")
		naSolverSettings.RestrictionAttributeNames = restrictions

		' Update the context based on the changes made to the solver settings
		naSolver.UpdateContext(naContext, deNetworkDataset, New GPMessagesClass())

		'Return the layer
		Return naLayer
	End Function

	'Write the NALayer out to disk as a layer file.
	Private Sub SaveLayerToDisk(ByVal layer As ILayer, ByVal path As String)
		Try
			Console.WriteLine("Writing layer file containing analysis to " + path)
			Dim layerfile As ILayerFile = New LayerFileClass()
			layerfile.New(path)
			layerfile.ReplaceContents(layer)
			layerfile.Save()
			Console.WriteLine("Writing layer file successfully saved")
		Catch err As Exception
			' Write out errors
			Console.WriteLine(err.Message)
		End Try

	End Sub
End Class
