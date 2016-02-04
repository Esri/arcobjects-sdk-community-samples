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
using System.Reflection;
using System.ComponentModel;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;

/// <summary>
/// Utility methods for working with parameter values, and other shared utilities in setting up
/// these custom subset evaluators.
/// </summary>

namespace SubsetNetworkEvaluators
{
	[Flags]
	public enum VarType
	{
		Empty = 0x0000,		//VT_EMPTY
		Null = 0x0001,		//VT_NULL
		Short = 0x0002,		//VT_I2
		Integer = 0x0003,	//VT_I4
		Float = 0x0004,		//VT_R4
		Double = 0x0005,	//VT_R8
		Date = 0x0007,		//VT_DATE
		String = 0x0008,    //VT_BSTR
		Bool = 0x000B,		//VT_BOOL
		ComObject = 0x000D,	//VT_UNKNOWN
		Array = 0x2000      //VT_ARRAY
	}; // enum VarType

	public class SubsetHelper
	{
		public static int FindParameter(IArray netAttributeParams, string searchName)
		{
			if (netAttributeParams == null || searchName.Length <= 0)
				return -1;

			string compareName;
			INetworkAttributeParameter netAttributeParam;
			int count = netAttributeParams.Count;
			for (int i = 0; i < count; ++i)
			{
				netAttributeParam = netAttributeParams.get_Element(i) as INetworkAttributeParameter;
				if (netAttributeParam != null)
				{
					compareName = netAttributeParam.Name;
					if (String.Compare(searchName, compareName, true) == 0)
						return i;
				}
			}

			return -1;
		}

		public static List<string> GetSourceNames(List<INetworkSource> netSources)
		{
			List<string> sourceNames = new List<string>();
			if (netSources == null)
				return sourceNames;

			foreach (INetworkSource netSource in netSources)
				sourceNames.Add(netSource.Name);

			return sourceNames;
		}

		public static List<INetworkSource> GetSourceList(IArray netSourcesArray)
		{
			List<INetworkSource> netSources = new List<INetworkSource>();
			int count = netSourcesArray.Count;
			INetworkSource netSource;
			for (int i = 0; i < count; ++i)
			{
				netSource = netSourcesArray.get_Element(i) as INetworkSource;
				if (netSource != null)
					netSources.Add(netSource);
			}
			return netSources;
		}

		public static List<INetworkSource> GetSourceList(List<INetworkSource> netSources, esriNetworkElementType eType)
		{
			List<esriNetworkElementType> eTypes = new List<esriNetworkElementType>();
			eTypes.Add(eType);
			return GetSourceList(netSources, eTypes);
		}

		public static List<INetworkSource> GetSourceList(List<INetworkSource> netSources, List<esriNetworkElementType> eTypes)
		{
			List<INetworkSource> subList = new List<INetworkSource>();
			if (netSources == null || eTypes == null)
				return subList;

			foreach (INetworkSource netSource in netSources)
			{
				foreach (esriNetworkElementType eType in eTypes)
				{
					if (netSource.ElementType == eType)
					{
						subList.Add(netSource);
						break;
					}
				}
			}
			return subList;
		}

		public static IArray RemoveAttributeByName(IArray netAttributes, string name)
		{
			return RemoveAttributesByKeyName(netAttributes, name, true);
		}

		public static IArray RemoveAttributesByPrefix(IArray netAttributes, string prefix)
		{
			return RemoveAttributesByKeyName(netAttributes, prefix, true);
		}

		public static IArray RemoveAttributesBySuffix(IArray netAttributes, string suffix)
		{
			return RemoveAttributesByKeyName(netAttributes, suffix, false);
		}

		public static IArray RemoveAttributesByKeyName(IArray netAttributes, string keyName, bool keyIsPrefix)
		{
			IArray preservedNetAttributes = new ArrayClass();

			int keyNameLen = keyName.Length;
			int netAttributeNameLen;
			INetworkAttribute netAttribute;
			string netAttributeName;
			bool isKeyAttribute;
			bool ignoreCase = true;

			int count = netAttributes.Count;
			for (int i = 0; i < count; ++i)
			{
				netAttribute = netAttributes.get_Element(i) as INetworkAttribute;
				if (netAttribute == null)
					continue;

				netAttributeName = netAttribute.Name;
				netAttributeNameLen = netAttributeName.Length;

				isKeyAttribute = false;
				if (keyNameLen == 0)
					isKeyAttribute = false;
				else if (netAttributeNameLen < keyNameLen)
					isKeyAttribute = false;
				else
				{
					int startIndex = 0;
					if (!keyIsPrefix)
						startIndex = netAttributeNameLen - keyNameLen;

					if (String.Compare(netAttributeName.Substring(startIndex, keyNameLen), keyName, ignoreCase) == 0)
						isKeyAttribute = true;
				}

				if (!isKeyAttribute)
					preservedNetAttributes.Add(netAttribute);
			}

			return preservedNetAttributes;
		}

		public static List<int> FindAttributeIndexes(IArray netAttributes, esriNetworkAttributeUsageType usage, esriNetworkAttributeDataType dataType, bool searchTimeUnits, bool ignoreDataType)
		{
			INetworkAttribute2 netAttribute = null;
			esriNetworkAttributeUnits units = esriNetworkAttributeUnits.esriNAUUnknown;
			bool isSearchUnits = false;
			bool isUnknownUnits = false;
			bool isTimeUnits = false;

			List<int> netAttributeIndexes = new List<int>();
			int count = netAttributes.Count;

			for (int i = 0; i < count; ++i)
			{
				netAttribute = netAttributes.get_Element(i) as INetworkAttribute2;
				if (netAttribute == null)
					continue;

				if (netAttribute.UsageType == usage && (ignoreDataType || netAttribute.DataType == dataType))
				{
					units = netAttribute.Units;
					isSearchUnits = false;

					if (usage != esriNetworkAttributeUsageType.esriNAUTCost)
						isSearchUnits = true;
					else
					{
						isUnknownUnits = false;
						if (units == esriNetworkAttributeUnits.esriNAUUnknown)
							isUnknownUnits = true;

						isTimeUnits = false;
						if (!isUnknownUnits)
						{
							if (units == esriNetworkAttributeUnits.esriNAUMinutes ||
								units == esriNetworkAttributeUnits.esriNAUSeconds ||
								units == esriNetworkAttributeUnits.esriNAUHours ||
								units == esriNetworkAttributeUnits.esriNAUDays)
							{
								isTimeUnits = true;
							}

							if (searchTimeUnits)
								isSearchUnits = isTimeUnits;
							else
								isSearchUnits = !isTimeUnits;
						}
					}
					if (isSearchUnits)
						netAttributeIndexes.Add(i);
				}
			}

			return netAttributeIndexes;
		}

		public static List<INetworkAttribute2> FindAttributes(IArray netAttributesArray, List<int> netAttributeIndexes)
		{
			List<INetworkAttribute2> netAttributes = new List<INetworkAttribute2>();
			foreach (int i in netAttributeIndexes)
			{
				INetworkAttribute2 netAttribute = netAttributesArray.get_Element(i) as INetworkAttribute2;
				if (netAttribute != null)
					netAttributes.Add(netAttribute);
			}

			return netAttributes;
		}

		public static void SetDefaultEvaluator(IEvaluatedNetworkAttribute netAttribute, object defaultValue, esriNetworkElementType eType)
		{
			INetworkConstantEvaluator constEvaluator = new NetworkConstantEvaluatorClass();
			constEvaluator.ConstantValue = defaultValue;
			INetworkEvaluator eval = constEvaluator as INetworkEvaluator;
			netAttribute.set_DefaultEvaluator(eType, eval);
		}

		public static void SetEvaluators(IEvaluatedNetworkAttribute netAttribute, INetworkSource netSource, Type t)
		{
			esriNetworkElementType eType = netSource.ElementType;
			if (eType == esriNetworkElementType.esriNETEdge)
			{
				SetEvaluator(netAttribute, netSource, t, esriNetworkEdgeDirection.esriNEDAlongDigitized);
				SetEvaluator(netAttribute, netSource, t, esriNetworkEdgeDirection.esriNEDAgainstDigitized);
			}
			else
			{
				SetEvaluator(netAttribute, netSource, t, esriNetworkEdgeDirection.esriNEDNone);
			}
		}

		public static void SetEvaluator(IEvaluatedNetworkAttribute netAttribute, INetworkSource netSource, Type t, esriNetworkEdgeDirection dirType)
		{
			object obj = Activator.CreateInstance(t);
			INetworkEvaluator eval = obj as INetworkEvaluator;
			netAttribute.set_Evaluator(netSource, dirType, eval);
		}
	}
}