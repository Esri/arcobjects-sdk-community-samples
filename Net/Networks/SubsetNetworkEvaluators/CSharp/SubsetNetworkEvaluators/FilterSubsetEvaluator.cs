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
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;

namespace SubsetNetworkEvaluators
{
	/// <summary>
	/// The filter subset network evaluator is a custom network evaluator for modeling dynamic restriction attributes
	/// where the subset of elements to restrict can be quickly switched at run-time without having to update the network
	/// dataset schema in ArcCatalog.  In this example the subset of network elements that are restricted is determined
	/// based on the selected network features in ArcMap, but it does not matter how the element subset is determined.
	/// The selected features can be interpreted as the only restricted features or as the only traversable features based
	/// on a parameter flag value, and the subset of elements is also specified as a network attribute parameter value.
	/// </summary>

	[ClassInterface(ClassInterfaceType.None)]
	[Guid("e2a9fbbf-8950-48cb-b487-0ee3f43dccca")]
	[ProgId("SubsetNetworkEvaluators.FilterSubsetEvaluator")]
	public class FilterSubsetEvaluator : INetworkEvaluator2, INetworkEvaluatorSetup
	{
		#region Member Variables

		private INetworkDataset m_networkDataset;
		private INetworkSource m_networkSource;
		private INetworkAttribute m_networkAttribute;

		// Indicates if filter values should be restricted (otherwise NON filter values are restricted)
		// SPECIAL CASE: if NO elements are in the filter, then no elements for THIS SOURCE will be restricted.
		// number of EIDs to filter for this source 
		// the EIDs to override values for, by scaling, for this source
		private bool m_restrictFilterElements = true;

		private int m_countSourceEIDs = 0;
		private Dictionary<int, int> m_sourceEIDHashTable;

		#endregion

		#region INetworkEvaluator Members

		public bool CacheAttribute
		{
			// CacheAttribute returns whether or not we want the network dataset to cache our evaluated attribute values during the network dataset build
			// Since this is a dynamic evaluator, we will return false, so that our attribute values are dynamically queried at runtime
			get { return false; }
		}

		public string DisplayName
		{
			get { return "FilterSubset"; }
		}

		public string Name
		{
			get { return "SubsetNetworkEvaluators.FilterSubset"; }
		}

		#endregion

		#region INetworkEvaluator2 Members

		public void Refresh()
		{
			// This method is called internally during a solve operation immediately prior to performing the actual solve
			// This gives us an opportunity to update our evaluator's internal state based on parameter values

			m_restrictFilterElements = true;
			m_countSourceEIDs = 0;
			m_sourceEIDHashTable = new Dictionary<int, int>();

			INetworkAttribute2 netAttribute2 = m_networkAttribute as INetworkAttribute2;
			IArray netAttributeParams = netAttribute2.Parameters;

			// Parameters: "FilterSubset_Restrict", "FilterSubset_EID_<SourceName>"
			string prefix = BaseParameterName + "_";
			string paramRestrictFilterName = prefix + "Restrict";
			string paramEIDsName = prefix + "EIDs_" + m_networkSource.Name;

			int nParamRestrictFilter = SubsetHelper.FindParameter(netAttributeParams, paramRestrictFilterName);
			int nParamEIDs = SubsetHelper.FindParameter(netAttributeParams, paramEIDsName);

			object value;

			INetworkAttributeParameter paramRestrictFilter;
			INetworkAttributeParameter paramEIDs;

			if (nParamRestrictFilter >= 0)
			{
				paramRestrictFilter = netAttributeParams.get_Element(nParamRestrictFilter) as INetworkAttributeParameter;
				value = paramRestrictFilter.Value;
				if (value != null)
					m_restrictFilterElements = (bool)value;
			}

			if (nParamEIDs >= 0)
			{
				paramEIDs = netAttributeParams.get_Element(nParamEIDs) as INetworkAttributeParameter;
				value = paramEIDs.Value as int[];
				if (value != null)
				{
					int eid;
					int[] rgEIDs;
					rgEIDs = (int[])value;

					int lb = rgEIDs.GetLowerBound(0);
					int ub = rgEIDs.GetUpperBound(0);

					for (int i = lb; i <= ub; ++i)
					{
						++m_countSourceEIDs;
						eid = rgEIDs[i];
						m_sourceEIDHashTable.Add(eid, eid);
					}
				}
			}
		}

		public IStringArray RequiredFieldNames
		{
			// This custom evaluator does not require any field names
			get { return null; }
		}

		#endregion

		#region INetworkEvaluatorSetup Members

		public UID CLSID
		{
			get
			{
				UID uid = new UIDClass();
				uid.Value = "{e2a9fbbf-8950-48cb-b487-0ee3f43dccca}";
				return uid;
			}
		}

		public IPropertySet Data
		{
			// The Data property is intended to make use of property sets to get/set the custom evaluator's properties using only one call to the evaluator object
			// This custom evaluator does not make use of this property
			get { return null; }
			set { }
		}

		public bool DataHasEdits
		{
			// Since this custom evaluator does not make any data edits, return false
			get { return false; }
		}

		public void Initialize(INetworkDataset networkDataset, IDENetworkDataset DataElement, INetworkSource netSource, IEvaluatedNetworkAttribute netAttribute)
		{
			// Initialize is called once per session (ArcMap session, ArcCatalog session, etc.) to initialize the evaluator for an associated network dataset            
			m_networkDataset = networkDataset;
			m_networkSource = netSource;
			m_networkAttribute = netAttribute;

			Refresh();
		}

		public object QueryValue(INetworkElement Element, IRow Row)
		{
			if (m_countSourceEIDs <= 0)
				return false;

			bool restrict = !m_restrictFilterElements;
			int eid = -1;
			if (m_sourceEIDHashTable.TryGetValue(Element.EID, out eid))
				restrict = m_restrictFilterElements;

			return restrict;
		}

		public bool SupportsDefault(esriNetworkElementType ElementType, IEvaluatedNetworkAttribute netAttribute)
		{
			return false;
		}

		public bool SupportsSource(INetworkSource Source, IEvaluatedNetworkAttribute netAttribute)
		{
			// This custom evaluator supports restriction attributes for all sources
			return netAttribute.UsageType == esriNetworkAttributeUsageType.esriNAUTRestriction;
		}

		public bool ValidateDefault(esriNetworkElementType ElementType, IEvaluatedNetworkAttribute netAttribute, ref int ErrorCode, ref string ErrorDescription, ref string errorAppendInfo)
		{
			if (SupportsDefault(ElementType, netAttribute))
			{
				ErrorCode = 0;
				ErrorDescription = errorAppendInfo = string.Empty;
				return true;
			}
			else
			{
				ErrorCode = -1;
				ErrorDescription = errorAppendInfo = string.Empty;
				return false;
			}
		}

		public bool ValidateSource(IDatasetContainer2 datasetContainer, INetworkSource netSource, IEvaluatedNetworkAttribute netAttribute, ref int ErrorCode, ref string ErrorDescription, ref string errorAppendInfo)
		{
			if (SupportsSource(netSource, netAttribute))
			{
				ErrorCode = 0;
				ErrorDescription = errorAppendInfo = string.Empty;
				return true;
			}
			else
			{
				ErrorCode = -1;
				ErrorDescription = errorAppendInfo = string.Empty;
				return false;
			}
		}

		#endregion

		#region Static Members

		public static string BaseParameterName
		{
			get
			{
				return "FilterSubset";
			}
		}

		public static void RemoveFilterSubsetAttribute(IDENetworkDataset deNet)
		{
			IArray netAttributes = SubsetHelper.RemoveAttributesByPrefix(deNet.Attributes, "Filter");
			deNet.Attributes = netAttributes;
		}

		public static IEvaluatedNetworkAttribute AddFilterSubsetAttribute(IDENetworkDataset deNet)
		{
			IArray netAttributes = deNet.Attributes;
			IEvaluatedNetworkAttribute netAttribute = new EvaluatedNetworkAttributeClass() as IEvaluatedNetworkAttribute;

			netAttribute.Name = BaseParameterName;
			netAttribute.UsageType = esriNetworkAttributeUsageType.esriNAUTRestriction;
			netAttribute.DataType = esriNetworkAttributeDataType.esriNADTBoolean;
			netAttribute.Units = esriNetworkAttributeUnits.esriNAUUnknown;

			INetworkAttribute2 netAttribute2 = netAttribute as INetworkAttribute2;
			netAttribute2.UseByDefault = true;

			List<INetworkSource> allNetSources = SubsetHelper.GetSourceList(deNet.Sources);
			List<INetworkSource> netSources = SubsetHelper.GetSourceList(allNetSources, esriNetworkElementType.esriNETEdge);
			List<string> netSourceNames = SubsetHelper.GetSourceNames(netSources);

			ResetFilterSubsetParameters((INetworkAttribute2)netAttribute, netSourceNames);

			bool supportTurns = deNet.SupportsTurns;

			//default evaluators
			SubsetHelper.SetDefaultEvaluator(netAttribute, false, esriNetworkElementType.esriNETEdge);
			SubsetHelper.SetDefaultEvaluator(netAttribute, false, esriNetworkElementType.esriNETJunction);
			if (supportTurns)
				SubsetHelper.SetDefaultEvaluator(netAttribute, false, esriNetworkElementType.esriNETTurn);

			//sourced evaluators
			foreach (INetworkSource netSource in netSources)
				SubsetHelper.SetEvaluators(netAttribute, netSource, typeof(FilterSubsetEvaluator));

			netAttributes.Add(netAttribute);
			deNet.Attributes = netAttributes;

			return netAttribute;
		}

		public static void ResetFilterSubsetParameters(INetworkAttribute2 netAttribute, List<string> netSourceNames)
		{
			IArray netParams = new ESRI.ArcGIS.esriSystem.ArrayClass();
			INetworkAttributeParameter netParam = null;
			object paramValue = null;

			netParam = new NetworkAttributeParameterClass();
			paramValue = true;

			string paramName = "";

			paramName = BaseParameterName;
			paramName += "_Restrict";

			netParam.Name = paramName;
			netParam.VarType = (int)VarType.Bool;
			netParam.Value = paramValue;
			netParam.DefaultValue = paramValue;
			netParams.Add(netParam);

			netParam = new NetworkAttributeParameterClass();
			paramValue = 1;

			foreach (string netSourceName in netSourceNames)
			{
				netParam = new NetworkAttributeParameterClass();
				paramValue = null;

				paramName = BaseParameterName;
				paramName += "_EIDs_";
				paramName += netSourceName;
				netParam.Name = paramName;
				netParam.VarType = (int)(VarType.Array | VarType.Integer);
				netParam.Value = paramValue;
				netParam.DefaultValue = paramValue;
				netParams.Add(netParam);
			}

			//does not preserve existing parameters if any
			netAttribute.Parameters = netParams;
		}

		#endregion
	}
}