/*

   Copyright 2016 Esri

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.

   See the License for the specific language governing permissions and
   limitations under the License.

*/
using System;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.NetworkAnalyst;

namespace RouteLayer
{
	/// <summary>
	/// Main class that checks out the appropriate ArcGIS license and calls RouteClass.SolveRoute to perform the analysis
	/// </summary>
	class Program
	{
		private static LicenseInitializer m_AOLicenseInitializer;

		static void Main(string[] args)
		{
			if (!ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.Engine))
			{
				if (!ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.Desktop))
				{
					System.Console.WriteLine("This application could not load the correct version of ArcGIS.");
					return;
				}
			}

			m_AOLicenseInitializer = new LicenseInitializer();

			//ESRI License Initializer generated code.
			if (!m_AOLicenseInitializer.InitializeApplication(new esriLicenseProductCode[] { esriLicenseProductCode.esriLicenseProductCodeBasic, esriLicenseProductCode.esriLicenseProductCodeStandard, esriLicenseProductCode.esriLicenseProductCodeAdvanced },
			new esriLicenseExtensionCode[] { esriLicenseExtensionCode.esriLicenseExtensionCodeNetwork }))
			{
				System.Console.WriteLine(m_AOLicenseInitializer.LicenseMessage());
				System.Console.WriteLine("This application could not initialize with the correct ArcGIS license and will shutdown.");
				m_AOLicenseInitializer.ShutdownApplication();
				return;
			}

			RouteClass routeClass = new RouteClass();
			routeClass.SolveRoute();

			//ESRI License Initializer generated code.
			//Do not make any call to ArcObjects after ShutDownApplication()
			m_AOLicenseInitializer.ShutdownApplication();
		}
	}

	/// <summary>
	/// The RouteClass class is the workhorse class that does the analysis and writes it to disk
	/// </summary>
	public class RouteClass
	{
		private const String FGDB_WORKSPACE =  @"ArcGIS\data\SanFrancisco\SanFrancisco.gdb";
		private const String INPUT_STOPS_FC = "Stores";
		private const String INPUT_NAME_FIELD = "Name";
		private const String FEATURE_DATASET = "Transportation";
		private const String NETWORK_DATASET = "Streets_ND";

		/// <summary>
		/// Create the analysis layer, load the locations, solve the analysis, and write to disk
		/// </summary>
		public void SolveRoute()
		{
			// Open the feature workspace, input feature class, and network dataset
			// As Workspace Factories are Singleton objects, they must be instantiated with the Activator
			IWorkspaceFactory workspaceFactory = System.Activator.CreateInstance(System.Type.GetTypeFromProgID("esriDataSourcesGDB.FileGDBWorkspaceFactory")) as IWorkspaceFactory;
			IFeatureWorkspace featureWorkspace = workspaceFactory.OpenFromFile(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), FGDB_WORKSPACE), 0) as IFeatureWorkspace;
			IFeatureClass inputStopsFClass = featureWorkspace.OpenFeatureClass(INPUT_STOPS_FC);

			// Obtain the dataset container from the workspace
			ESRI.ArcGIS.Geodatabase.IFeatureDataset featureDataset = featureWorkspace.OpenFeatureDataset(FEATURE_DATASET);
			var featureDatasetExtensionContainer = featureDataset as ESRI.ArcGIS.Geodatabase.IFeatureDatasetExtensionContainer;
			ESRI.ArcGIS.Geodatabase.IFeatureDatasetExtension featureDatasetExtension = featureDatasetExtensionContainer.FindExtension(ESRI.ArcGIS.Geodatabase.esriDatasetType.esriDTNetworkDataset);
			var datasetContainer3 = featureDatasetExtension as ESRI.ArcGIS.Geodatabase.IDatasetContainer3;

			// Use the container to open the network dataset.
			ESRI.ArcGIS.Geodatabase.IDataset dataset = datasetContainer3.get_DatasetByName(ESRI.ArcGIS.Geodatabase.esriDatasetType.esriDTNetworkDataset, NETWORK_DATASET);
			INetworkDataset networkDataset = dataset as INetworkDataset;

			// Create the Route NALayer
			INALayer naLayer = CreateRouteAnalysisLayer("Route", networkDataset);
			INAContext naContext = naLayer.Context;
			INAClass stopsNAClass = naContext.NAClasses.get_ItemByName("Stops") as INAClass;
			IFeatureClass routesFC = naContext.NAClasses.get_ItemByName("Routes") as IFeatureClass;

			// Load the Stops
			INAClassFieldMap naClassFieldMap = new NAClassFieldMapClass();
			naClassFieldMap.set_MappedField("Name", INPUT_NAME_FIELD);

			INAClassLoader naLoader = new NAClassLoaderClass();
			naLoader.Locator = naContext.Locator;
			naLoader.NAClass = stopsNAClass;
			naLoader.FieldMap = naClassFieldMap;

			// Avoid loading network locations onto non-traversable portions of elements
			INALocator3 locator = naContext.Locator as INALocator3;
			locator.ExcludeRestrictedElements = true;
			locator.CacheRestrictedElements(naContext);

			int rowsInCursor = 0;
			int rowsLocated = 0;
			naLoader.Load(inputStopsFClass.Search(new QueryFilterClass(), false) as ICursor, new CancelTrackerClass(), ref rowsInCursor, ref rowsLocated);

			//Message all of the network analysis agents that the analysis context has changed
			((INAContextEdit)naContext).ContextChanged();

			//Solve
			INASolver naSolver = naContext.Solver;
			naSolver.Solve(naContext, new GPMessagesClass(), new CancelTrackerClass());

			//Save the layer to disk
			SaveLayerToDisk(naLayer as ILayer, System.Environment.CurrentDirectory + @"\Route.lyr");
		}

		/// <summary>
		/// Create a new network analysis layer and set some solver settings
		/// </summary>
		private INALayer CreateRouteAnalysisLayer(String layerName, INetworkDataset networkDataset)
		{
			INARouteSolver naRouteSolver = new NARouteSolverClass();
			INASolverSettings naSolverSettings = naRouteSolver as INASolverSettings;
			INASolver naSolver = naRouteSolver as INASolver;

			//Get the NetworkDataset's Data Element
			IDatasetComponent datasetComponent = networkDataset as IDatasetComponent;
			IDENetworkDataset deNetworkDataset = datasetComponent.DataElement as IDENetworkDataset;

			//Create the NAContext and bind to it
			INAContext naContext;
			naContext = naSolver.CreateContext(deNetworkDataset, layerName);
			INAContextEdit naContextEdit = naContext as INAContextEdit;
			naContextEdit.Bind(networkDataset, new GPMessagesClass());

			//Create the NALayer
			INALayer naLayer;
			naLayer = naSolver.CreateLayer(naContext);
			(naLayer as ILayer).Name = layerName;

			//Set properties on the route solver interface
			naRouteSolver.FindBestSequence = true;
			naRouteSolver.PreserveFirstStop = true;
			naRouteSolver.PreserveLastStop = false;
			naRouteSolver.UseTimeWindows = false;
			naRouteSolver.OutputLines = esriNAOutputLineType.esriNAOutputLineTrueShapeWithMeasure;

			//Set some properties on the general INASolverSettings interface
			IStringArray restrictions = naSolverSettings.RestrictionAttributeNames;
			restrictions.Add("Oneway");
			naSolverSettings.RestrictionAttributeNames = restrictions;

			// Update the context based on the changes made to the solver settings
			naSolver.UpdateContext(naContext, deNetworkDataset, new GPMessagesClass());

			//Return the layer
			return naLayer;
		}

		/// <summary>
		/// Write the NALayer out to disk as a layer file.
		/// </summary>
		private void SaveLayerToDisk(ILayer layer, String path)
		{
			try
			{
				Console.WriteLine("Writing layer file containing analysis to " + path);
				ILayerFile layerFile = new LayerFileClass();
				layerFile.New(path);
				layerFile.ReplaceContents(layer as ILayer);
				layerFile.Save();
				Console.WriteLine("Writing layer file successfully saved");
			}
			catch (Exception err)
			{
				// Write out errors
				Console.WriteLine(err.Message);
			}
		}
	}
}
