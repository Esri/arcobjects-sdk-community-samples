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
using System.Drawing;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.NetworkAnalyst;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.NetworkAnalystUI;
using SubsetNetworkEvaluators;

namespace SubsetNetworkEvaluatorsUI
{
	/// <summary>
	/// The SubsetHelperUI is a utility class to aid in determining the relevant set of parameters
	/// to auto-update when set to listen to the events and other shared utilities.
	/// </summary>	
	class SubsetHelperUI
	{
		public static void PushParameterValuesToNetwork(INetworkAnalystExtension nax)
		{
			try
			{
				if (nax == null)
					return;

				bool naxEnabled = false;
				IExtensionConfig naxConfig = nax as IExtensionConfig;
				naxEnabled = naxConfig.State == esriExtensionState.esriESEnabled;

				if (!naxEnabled)
					return;

				INAWindow naWindow = nax.NAWindow;
				INALayer naLayer = null;
				INAContext naContext = null;
				INetworkDataset nds = null;

				naLayer = naWindow.ActiveAnalysis;
				if (naLayer != null)
					naContext = naLayer.Context;

				if (naContext != null)
					nds = naContext.NetworkDataset;

				if (nds == null)
					return;

				IDatasetComponent dsComponent = nds as IDatasetComponent;
				IDENetworkDataset deNet = dsComponent.DataElement as IDENetworkDataset;

				INASolver naSolver = naContext.Solver;
				INASolverSettings2 naSolverSettings2 = naSolver as INASolverSettings2;

				if (naSolverSettings2 == null)
					return;

				INetworkAttribute2 netAttribute;
				string attributeName;

				IArray netParameters;
				INetworkAttributeParameter netParameter;
				string paramName;
				int cParameters;

				object paramValue;

				int cAttributes = nds.AttributeCount;
				for (int a = 0; a < cAttributes; ++a)
				{
					netAttribute = nds.get_Attribute(a) as INetworkAttribute2;
					attributeName = netAttribute.Name;
					netParameters = netAttribute.Parameters;

					cParameters = netParameters.Count;
					for (int p = 0; p < cParameters; ++p)
					{
						netParameter = netParameters.get_Element(p) as INetworkAttributeParameter;
						paramName = netParameter.Name;

						paramValue = naSolverSettings2.get_AttributeParameterValue(attributeName, paramName);
						netParameter.Value = paramValue;
					}

					netAttribute.Refresh();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Push Parameter Values To Network");
			}
		}

		public static bool ParameterExists(INetworkDataset nds, string searchName, VarType vt)
		{
			bool found = false;

			INetworkAttribute2 netAttribute;
			IArray netParams;
			INetworkAttributeParameter netParam;

			int cAttributes = nds.AttributeCount;
			for (int a = 0; a < cAttributes; ++a)
			{
				netAttribute = nds.get_Attribute(a) as INetworkAttribute2;
				netParams = null;
				int cParams = 0;
				if (netAttribute != null)
					netParams = netAttribute.Parameters;

				if (netParams != null)
					cParams = netParams.Count;

				string compareName;
				for (int p = 0; p < cParams; ++p)
				{
					netParam = netParams.get_Element(p) as INetworkAttributeParameter;
					compareName = netParam.Name;
					if (String.Compare(searchName, compareName, true) == 0)
					{
						found = true;
						break;
					}
				}
				if (found)
					break;
			}

			return found;
		}

		public static void ClearEIDArrayParameterValues(INetworkAnalystExtension nax, string baseName)
		{
			try
			{
				INAWindow naWindow = nax.NAWindow;
				INALayer naLayer = null;
				INAContext naContext = null;
				INetworkDataset nds = null;

				naLayer = naWindow.ActiveAnalysis;
				if (naLayer != null)
					naContext = naLayer.Context;

				if (naContext != null)
					nds = naContext.NetworkDataset;

				if (nds == null)
					return;

				VarType vt = SubsetHelperUI.GetEIDArrayParameterType();
				List<string> sourceNames = SubsetHelperUI.FindParameterizedSourceNames(nds, baseName, vt);

				SubsetHelperUI.ClearEIDArrayParameterValues(nax, sourceNames, baseName);
				SubsetHelperUI.PushParameterValuesToNetwork(nax);
			}
			catch (Exception ex)
			{
				string msg = SubsetHelperUI.GetFullExceptionMessage(ex);
				MessageBox.Show(msg, "Clear Network Element Array Parameters");
			}
		}

		private static void ClearEIDArrayParameterValues(INetworkAnalystExtension nax, List<string> sourceNames, string baseName)
		{
			if (nax == null)
				return;

			bool naxEnabled = false;
			IExtensionConfig naxConfig = nax as IExtensionConfig;
			naxEnabled = naxConfig.State == esriExtensionState.esriESEnabled;

			if (!naxEnabled)
				return;

			Dictionary<string, List<int>> eidsBySourceName = new Dictionary<string, List<int>>();
			foreach (string sourceName in sourceNames)
			{
				List<int> eids = null;
				if (!eidsBySourceName.TryGetValue(sourceName, out eids))
					eidsBySourceName.Add(sourceName, null);
			}

			UpdateEIDArrayParameterValuesFromEIDLists(nax, eidsBySourceName, baseName);
		}

		public static void UpdateEIDArrayParameterValuesFromEIDLists(INetworkAnalystExtension nax, Dictionary<string, List<int>> eidsBySourceName, string baseName)
		{
			if (nax == null)
				return;

			bool naxEnabled = false;
			IExtensionConfig naxConfig = nax as IExtensionConfig;
			naxEnabled = naxConfig.State == esriExtensionState.esriESEnabled;

			if (!naxEnabled)
				return;

			INAWindow naWindow = nax.NAWindow;
			INALayer naLayer = null;
			INAContext naContext = null;
			INetworkDataset nds = null;

			naLayer = naWindow.ActiveAnalysis;
			if (naLayer != null)
				naContext = naLayer.Context;

			if (naContext != null)
				nds = naContext.NetworkDataset;

			if (nds == null)
				return;

			IDatasetComponent dsComponent = nds as IDatasetComponent;
			IDENetworkDataset deNet = dsComponent.DataElement as IDENetworkDataset;

			INASolver naSolver = naContext.Solver;
			INASolverSettings2 naSolverSettings2 = naSolver as INASolverSettings2;

			if (naSolverSettings2 == null)
				return;

			string prefix = GetEIDArrayPrefixFromBaseName(baseName);
			VarType vt = GetEIDArrayParameterType();

			int cAttributes = nds.AttributeCount;
			for (int a = 0; a < cAttributes; ++a)
			{
				INetworkAttribute2 netAttribute = nds.get_Attribute(a) as INetworkAttribute2;
				IArray netParams = netAttribute.Parameters;
				int cParams = netParams.Count;
				object paramValue;
				for (int p = 0; p < cParams; ++p)
				{
					INetworkAttributeParameter param = netParams.get_Element(p) as INetworkAttributeParameter;
					if (param.VarType != (int)vt)
						continue;

					string paramName = param.Name;
					string sourceName = GetSourceNameFromParameterName(prefix, paramName);
					if (sourceName.Length == 0)
						continue;

					List<int> eids = null;
					if (eidsBySourceName.TryGetValue(sourceName, out eids))
					{
						if (eids != null)
						{
							if (eids.Count == 0)
								eids = null;
						}
					}

					paramValue = (eids != null) ? eids.ToArray() : null;
					naSolverSettings2.set_AttributeParameterValue(netAttribute.Name, param.Name, paramValue);
				}
			}
		}

		public static void UpdateEIDArrayParameterValuesFromOIDArrays(INetworkAnalystExtension nax, Dictionary<string, ILongArray> oidArraysBySourceName, string baseName)
		{
			Dictionary<string, List<int>> eidsBySourceName = GetEIDListsBySourceName(nax, oidArraysBySourceName, baseName);
			UpdateEIDArrayParameterValuesFromEIDLists(nax, eidsBySourceName, baseName);
		}

		public static void UpdateEIDArrayParameterValuesFromGeometry(INetworkAnalystExtension nax, IGeometry searchGeometry, string baseName)
		{
			Dictionary<string, List<int>> eidsBySourceName = GetEIDListsBySourceName(nax, searchGeometry, baseName);
			UpdateEIDArrayParameterValuesFromEIDLists(nax, eidsBySourceName, baseName);
		}

		private static Dictionary<string, List<int>> GetEIDListsBySourceName(INetworkAnalystExtension nax, object searchObject, string baseName)
		{
			if (nax == null)
				return null;

			bool naxEnabled = false;
			IExtensionConfig naxConfig = nax as IExtensionConfig;
			naxEnabled = naxConfig.State == esriExtensionState.esriESEnabled;

			if (!naxEnabled)
				return null;

			INAWindow naWindow = nax.NAWindow;
			INALayer naLayer = null;
			INAContext naContext = null;
			INetworkDataset nds = null;

			naLayer = naWindow.ActiveAnalysis;
			if (naLayer != null)
				naContext = naLayer.Context;

			if (naContext != null)
				nds = naContext.NetworkDataset;

			INetworkQuery netQuery = nds as INetworkQuery;
			if (netQuery == null)
				return null;

			bool oidSearch = false;
			bool geometrySearch = false;

			if (searchObject == null)
				return null;
			else if (searchObject is Dictionary<string, ILongArray>)
				oidSearch = true;
			else if (searchObject is IGeometry)
				geometrySearch = true;
			else
				return null;

			VarType vt = GetEIDArrayParameterType();
			List<string> sourceNames = FindParameterizedSourceNames(nds, baseName, vt);
			Dictionary<string, List<int>> eidsBySourceName = new Dictionary<string, List<int>>();
			foreach (string sourceName in sourceNames)
			{
				INetworkSource netSource = nds.get_SourceByName(sourceName);
				int sourceID = netSource.ID;
				List<int> eids = new List<int>();

				if (oidSearch)
				{
					Dictionary<string, ILongArray> oidArraysBySourceName = (Dictionary<string, ILongArray>)searchObject;
					ILongArray oids = null;
					IEnumNetworkElement enumNetElement;
					INetworkElement netElement;

					if (oidArraysBySourceName.TryGetValue(sourceName, out oids))
					{
						enumNetElement = netQuery.get_ElementsByOIDs(sourceID, oids);
						enumNetElement.Reset();
						netElement = enumNetElement.Next();
						while (netElement != null)
						{
							eids.Add(netElement.EID);
							netElement = enumNetElement.Next();
						}
					}
				}
				else if (geometrySearch)
				{
					IGeometry searchGeometry = (IGeometry)searchObject;
					if (searchGeometry != null && !searchGeometry.IsEmpty)
					{
						IGeometry elementGeometry = null;
						esriNetworkElementType elementType = esriNetworkElementType.esriNETEdge;
						int eid = -1;

						// Search for the network dataset layer associated with the active analysis layer or create one using the
						// network dataset if matching one not found.
						// If, for example, multiple network dataset layers are added to the map, the active analysis layer
						// might not reference the current network dataset layer (nax.CurrentNetworkLayer).

						INetworkLayer ndsLayer = new NetworkLayerClass();
						ndsLayer.NetworkDataset = nds;

						int count = nax.NetworkLayerCount;
						for (int i = 0; i < count; ++i)
						{
							ndsLayer = nax.get_NetworkLayer(i);
							if (ndsLayer.NetworkDataset == nds)
								break;
							else
								ndsLayer = null;
						}

						if (ndsLayer == null)
						{
							ndsLayer = new NetworkLayerClass();
							ndsLayer.NetworkDataset = nds;
						}

						IEnumLocatedNetworkElement enumLocatedNetElement = null;
						if (ndsLayer != null)
						{
							enumLocatedNetElement = ndsLayer.SearchLocatedNetworkElements(sourceName, searchGeometry);
							enumLocatedNetElement.Reset();
							eid = enumLocatedNetElement.Next(ref elementGeometry, ref elementType);
							while (eid != -1)
							{
								eids.Add(eid);
								eid = enumLocatedNetElement.Next(ref elementGeometry, ref elementType);
							}
						}
					}
				}

				eidsBySourceName.Add(sourceName, eids);
			}

			return eidsBySourceName;
		}

		public static Dictionary<string, ILongArray> GetOIDArraysBySourceNameFromMapSelection(IMap map, List<string> sourceNames)
		{
			UIDClass uid = new UIDClass();
			uid.Value = "{E156D7E5-22AF-11D3-9F99-00C04F6BC78E}"; //IGeoFeatureLayer

			IEnumLayer searchEnumLayer = map.get_Layers(uid, true);
			searchEnumLayer.Reset();

			//create result dictionary from source names with empty oidArrays

			Dictionary<string, ILongArray> oidArraysBySourceName = new Dictionary<string, ILongArray>();
			ILongArray oidArray = null;

			foreach (string sourceName in sourceNames)
			{
				if (!oidArraysBySourceName.TryGetValue(sourceName, out oidArray))
				{
					oidArray = new LongArrayClass();
					oidArraysBySourceName.Add(sourceName, oidArray);
				}
			}

			ILayer layer = searchEnumLayer.Next();
			while (layer != null)
			{
				IDisplayTable displayTable = layer as IDisplayTable;
				string sourceName = "";
				if (layer.Valid && layer.Visible && displayTable != null)
				{
					IDataset ds = displayTable.DisplayTable as IDataset;
					if (ds != null)
						sourceName = ds.Name;
				}

				if (sourceName.Length > 0)
				{
					if (oidArraysBySourceName.TryGetValue(sourceName, out oidArray))
					{
						ISelectionSet selSet = displayTable.DisplaySelectionSet;
						IEnumIDs enumOIDs = null;
						if (selSet != null)
							enumOIDs = selSet.IDs;

						if (enumOIDs != null)
						{
							enumOIDs.Reset();
							int oid = enumOIDs.Next();
							while (oid != -1)
							{
								oidArray.Add(oid);
								oid = enumOIDs.Next();
							}
						}
					}
				}

				layer = searchEnumLayer.Next();
			}

			return oidArraysBySourceName;
		}

		public static IGeometry GetSearchGeometryFromGraphics(IGraphicsContainer graphics)
		{
			IGeometryCollection geometryBag = new GeometryBagClass();
			IElement element;
			IGeometry geometry;

			graphics.Reset();
			element = graphics.Next();

			object before = Type.Missing;
			object after = Type.Missing;

			while (element != null)
			{
				geometry = element.Geometry;
				if (geometry is IPolygon)
					geometryBag.AddGeometry(geometry, ref before, ref after);

				element = graphics.Next();
			}

			IGeometry searchGeometry = geometryBag as IGeometry;

			return searchGeometry;
		}

		public static List<string> FindParameterizedSourceNames(INetworkDataset nds, string baseName, VarType vt)
		{
			List<string> sourceNamesList = new List<string>();
			Dictionary<string, int?> sourceNamesDictionary = new Dictionary<string, int?>();

			int? dummyValue = null;
			int? foundDummyValue = null;

			string prefix = GetEIDArrayPrefixFromBaseName(baseName);

			INetworkSource netSource;
			string sourceName;
			string searchParamName;
			int count = nds.SourceCount;
			for (int i = 0; i < count; ++i)
			{
				netSource = nds.get_Source(i);
				sourceName = netSource.Name;
				if (sourceNamesDictionary.TryGetValue(sourceName, out foundDummyValue))
					continue;

				searchParamName = GetSourceParameterName(prefix, sourceName);

				if (ParameterExists(nds, searchParamName, vt))
				{
					sourceNamesList.Add(sourceName);
					sourceNamesDictionary.Add(sourceName, dummyValue);
				}
			}

			return sourceNamesList;
		}

		public static VarType GetEIDArrayParameterType()
		{
			VarType vt = VarType.Array | VarType.Integer;
			return vt;
		}

		public static string SelectionEIDArrayBaseName
		{
			get
			{
				return FilterSubsetEvaluator.BaseParameterName;
			}
		}

		public static string GraphicsEIDArrayBaseName
		{
			get
			{
				return ScaleSubsetEvaluator.BaseParameterName;
			}
		}

		private static string GetEIDArrayPrefixFromBaseName(string baseName)
		{
			string baseNameEIDArrayModifer = "_eids";
			string prefix = baseName;
			prefix += baseNameEIDArrayModifer;

			return prefix;
		}

		private static string GetSourceNameFromParameterName(string prefix, string paramName)
		{
			string searchSubName = prefix + "_";

			int searchSubNameLen = searchSubName.Length;
			int paramNameLen = paramName.Length;
			if (searchSubNameLen <= 0 || searchSubNameLen >= paramNameLen)
				return "";

			string compareSubName = paramName.Substring(0, searchSubNameLen);
			if (String.Compare(compareSubName, searchSubName, true) != 0)
				return "";

			string sourceName = paramName.Substring(searchSubNameLen);
			return sourceName;
		}

		private static string GetSourceParameterName(string prefix, string sourceName)
		{
			string paramName = prefix;
			paramName += "_";
			paramName += sourceName;

			return paramName;
		}

		public static IExtensionConfig GetNAXConfiguration(IApplication app)
		{
			IExtensionConfig extConfig = null;
			try
			{
				if (app != null)
				{
					UID extCLSID = new UIDClass();
					extCLSID.Value = "{C967BD39-1118-42EE-AAAB-B31642C89C3E}"; // Network Analyst
					extConfig = app.FindExtensionByCLSID(extCLSID) as IExtensionConfig;
				}
			}
			catch
			{
				extConfig = null;
			}

			return extConfig;
		}

		public static string GetFullExceptionMessage(Exception ex)
		{
			string msg = "";
			string subMsg = "";

			while (ex != null)
			{
				subMsg = ex.Message;
				if (subMsg.Length > 0 && msg.Length > 0)
					msg += "\n";

				msg += subMsg;
				ex = ex.InnerException;
			}

			return msg;
		}
	}
}
