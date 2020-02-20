/*

   Copyright 2019 Esri

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
	/// The scale subset network evaluator is a custom network evaluator for modeling slowdown polygons
	/// where traffic speeds are slowed down in only a particular subset of the network.  In this
	/// example the subset of network elements that are slowed down is determined based on the geometry
	/// of graphic elements drawn in arc map, but it does not matter how the element subset is determined.
	/// The elements that are not in the subset just return the non-scaled base attribute value.  This could
	/// be useful, for example, if certain low lying areas had a flash flood, or other localized congestion that
	/// does not affect the network as a whole.  The subset of elements to be scaled and the scale factor to
	/// scale the base attribute by in the scale subset evaluator are network attribute parameters of the attribute
	/// the evaluator is assigned to.
	/// </summary>

	[ClassInterface(ClassInterfaceType.None)]
	[Guid("67cf8446-22a2-4baf-9c97-3c22a33cc0c7")]
	[ProgId("SubsetNetworkEvaluators.ScaleSubsetEvaluator")]
	public class ScaleSubsetEvaluator : INetworkEvaluator2, INetworkEvaluatorSetup
	{
		#region Member Variables

		private INetworkDataset m_networkDataset;
		private INetworkSource m_networkSource;
		private INetworkAttribute m_networkAttribute;

		private double m_scaleFactor = 1;
		private int m_thisNetworkAttributeID = -1;  // the ID for this attribute
		private int m_baseNetworkAttributeID = -1;  // the ID for the other attribute that should be scale (determined based on attribute name)
		private int m_countSourceEIDs = 0;          // number of EIDs to override for this source 
		private Dictionary<int, int> m_sourceEIDHashTable;           // the EIDs to override values for, by scaling, for this source

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
			get { return "ScaleSubset"; }
		}

		public string Name
		{
			get { return "SubsetNetworkEvaluators.ScaleSubset"; }
		}

		#endregion

		#region INetworkEvaluator2 Members

		public void Refresh()
		{
			// This method is called internally during a solve operation immediately prior to performing the actual solve
			// This gives us an opportunity to update our evaluator's internal state based on parameter values

			m_scaleFactor = 1;
			m_countSourceEIDs = 0;
			m_sourceEIDHashTable = new Dictionary<int, int>();

			INetworkAttribute2 netAttribute2 = m_networkAttribute as INetworkAttribute2;
			IArray netAttributeParams = netAttribute2.Parameters;

			// Parameters: "ScaleSubset_Factor", "ScaleSubset_eids_<SourceName>"
			string prefix = BaseParameterName + "_";

			string paramScaleFactorName = prefix + "Factor";
			string paramEIDsName = prefix + "eids_" + m_networkSource.Name;

			int nParamScaleFactor = SubsetHelper.FindParameter(netAttributeParams, paramScaleFactorName);
			int nParamEIDs = SubsetHelper.FindParameter(netAttributeParams, paramEIDsName);

			object value;

			INetworkAttributeParameter paramScaleFactor;
			INetworkAttributeParameter paramEIDs;

			if (nParamScaleFactor >= 0)
			{
				paramScaleFactor = netAttributeParams.get_Element(nParamScaleFactor) as INetworkAttributeParameter;
				value = paramScaleFactor.Value;
				if (value != null)
					m_scaleFactor = (double)value;
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
				// Create and return the GUID for this custom evaluator
				UID uid = new UIDClass();
				uid.Value = "{67cf8446-22a2-4baf-9c97-3c22a33cc0c7}";
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

			m_thisNetworkAttributeID = netAttribute.ID;
			m_baseNetworkAttributeID = -1;

			//The attribute name must begin with one or more non underscore characters followed by
			//an underscore character and then the name of the base cost attribute.
			//The underscore prior to the base attribute name should be the first underscore in the name.

			string thisAttributeName = netAttribute.Name;
			int nPos = thisAttributeName.IndexOf('_');
			int nLastPos = thisAttributeName.Length - 1;

			string baseNetAttributeName;
			INetworkAttribute baseNetAttribute = null;

			if (nPos > 0 && nPos < nLastPos)
			{
				baseNetAttributeName = thisAttributeName.Remove(0, nPos + 1);
				try
				{
					baseNetAttribute = networkDataset.get_AttributeByName(baseNetAttributeName);
				}
				catch (COMException ex)
				{
					baseNetAttribute = null;
					string msg = string.Format("Base Attribute ({0}) not found. {1}.", baseNetAttributeName, ex.Message);
					System.Diagnostics.Trace.WriteLine(msg, "Scale Subset Network Evaluator");
				}

				if (baseNetAttribute != null)
				{
					if (baseNetAttribute.ID != m_thisNetworkAttributeID)
						m_baseNetworkAttributeID = baseNetAttribute.ID;
				}
			}

			Refresh();
		}

		public object QueryValue(INetworkElement Element, IRow Row)
		{
			if (m_baseNetworkAttributeID < 0)
				return -1;

			object value = Element.get_AttributeValue(m_baseNetworkAttributeID);
			if (value == null)
				return -1;

			double baseValue = (double)value;
			if (baseValue <= 0 || m_scaleFactor == 1 || m_countSourceEIDs <= 0)
				return baseValue;

			bool isScaled = false;

			int eid = -1;
			if (m_sourceEIDHashTable.TryGetValue(Element.EID, out eid))
				isScaled = (eid > 0);

			object resultValue = baseValue;
			if (isScaled)
			{
				if (m_scaleFactor >= 0)
					resultValue = m_scaleFactor * baseValue;
				else
					resultValue = -1;
			}

			return resultValue;
		}

		public object QueryPartialEdgeValue(INetworkEdge2 edge, double fromPos, double toPos)
		{
			// Partial Edge values are not appropriate for restriction evaluators.
			throw new NotImplementedException();
		}

		public bool SupportsDefault(esriNetworkElementType ElementType, IEvaluatedNetworkAttribute netAttribute)
		{
			return false;
		}

		public bool SupportsSource(INetworkSource netSource, IEvaluatedNetworkAttribute netAttribute)
		{
			// This custom evaluator supports cost attributes for all sources
			return netAttribute.UsageType == esriNetworkAttributeUsageType.esriNAUTCost;
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
				return "ScaleSubset";
			}
		}

		public static void RemoveScaleSubsetAttributes(IDENetworkDataset deNet)
		{
			IArray netAttributes = SubsetHelper.RemoveAttributesByPrefix(deNet.Attributes, BaseParameterName);
			deNet.Attributes = netAttributes;
		}

		public static List<IEvaluatedNetworkAttribute> AddScaleSubsetAttributes(IDENetworkDataset deNet)
		{
			List<IEvaluatedNetworkAttribute> scaleSubsetAttributes = new List<IEvaluatedNetworkAttribute>();

			IArray netAttributesArray = deNet.Attributes;
			List<int> baseIndexes = SubsetHelper.FindAttributeIndexes(netAttributesArray, esriNetworkAttributeUsageType.esriNAUTCost, esriNetworkAttributeDataType.esriNADTDouble, true, false);
			List<INetworkAttribute2> baseNetAttributes = SubsetHelper.FindAttributes(netAttributesArray, baseIndexes);
			foreach (INetworkAttribute2 baseNetAttribute in baseNetAttributes)
				scaleSubsetAttributes.Add(AddScaleSubsetAttribute(deNet, baseNetAttribute));

			return scaleSubsetAttributes;
		}

		public static IEvaluatedNetworkAttribute AddScaleSubsetAttribute(IDENetworkDataset deNet, INetworkAttribute2 baseNetAttribute)
		{
			if (baseNetAttribute == null)
				return null;

			if (baseNetAttribute.UsageType != esriNetworkAttributeUsageType.esriNAUTCost)
				return null;

			IArray netAttributes = deNet.Attributes;
			IEvaluatedNetworkAttribute netAttribute = new EvaluatedNetworkAttributeClass() as IEvaluatedNetworkAttribute;

			string netAttributeName = BaseParameterName;
			netAttributeName += "_";
			netAttributeName += baseNetAttribute.Name;

			netAttribute.Name = netAttributeName;
			netAttribute.UsageType = baseNetAttribute.UsageType;
			netAttribute.DataType = baseNetAttribute.DataType;
			netAttribute.Units = baseNetAttribute.Units;

			List<INetworkSource> allNetSources = SubsetHelper.GetSourceList(deNet.Sources);
			List<INetworkSource> netSources = SubsetHelper.GetSourceList(allNetSources, esriNetworkElementType.esriNETEdge);
			List<string> netSourceNames = SubsetHelper.GetSourceNames(netSources);

			ResetScaleSubsetParameters((INetworkAttribute2)netAttribute, netSourceNames);

			bool supportTurns = deNet.SupportsTurns;

			//default evaluators
			SubsetHelper.SetDefaultEvaluator(netAttribute, 0, esriNetworkElementType.esriNETEdge);
			SubsetHelper.SetDefaultEvaluator(netAttribute, 0, esriNetworkElementType.esriNETJunction);
			if (supportTurns)
				SubsetHelper.SetDefaultEvaluator(netAttribute, 0, esriNetworkElementType.esriNETTurn);

			//sourced evaluators
			foreach (INetworkSource netSource in netSources)
				SubsetHelper.SetEvaluators(netAttribute, netSource, typeof(ScaleSubsetEvaluator));

			netAttributes.Add(netAttribute);
			deNet.Attributes = netAttributes;

			return netAttribute;
		}

		public static void ResetScaleSubsetParameters(INetworkAttribute2 netAttribute, List<string> netSourceNames)
		{
			IArray netParams = new ESRI.ArcGIS.esriSystem.ArrayClass();
			INetworkAttributeParameter netParam = null;
			object paramValue = null;
			string paramName = "";

			netParam = new NetworkAttributeParameterClass();
			paramValue = 1;

			paramName = BaseParameterName;
			paramName += "_Factor";

			netParam.Name = paramName;
			netParam.VarType = (int)VarType.Double;
			netParam.Value = paramValue;
			netParam.DefaultValue = paramValue;
			netParams.Add(netParam);

			foreach (string netSourceName in netSourceNames)
			{
				netParam = new NetworkAttributeParameterClass();
				paramValue = null;

				paramName = BaseParameterName;
				paramName += "_eids_";
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

