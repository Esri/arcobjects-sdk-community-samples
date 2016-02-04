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
using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;

namespace SelectionRestriction
{
	[ClassInterface(ClassInterfaceType.None)]
	[Guid("1f75097c-7224-4d1f-ae38-1242e26efcef")]
	public class SelectionRestrictionEvaluator : INetworkEvaluator2, INetworkEvaluatorSetup
	{
		#region Member Variables

		private INetworkDataset m_networkDataset;        // Used to store a reference to the network dataset
		private INetworkSource m_networkSource;          // Used to store a reference to the network source associated with this evaluator
		private IMxDocument m_mxDocument;                // Used to store a reference to the current MxDocument
		private Dictionary<int, int> m_sourceHashTable;  // Used to store a dynamic hash table of selected OIDs for the evaluator's network source

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
			get { return "SelectionRestriction"; }
		}

		public string Name
		{
			get { return "SelectionRestriction.SelectionRestrictionEvaluator"; }
		}

		#endregion

		#region INetworkEvaluatorSetup Members

		public UID CLSID
		{
			get
			{
				// Create and return the GUID for this custom evaluator
				UID uid = new UIDClass();
				uid.Value = "{1f75097c-7224-4d1f-ae38-1242e26efcef}";
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

		public void Initialize(INetworkDataset networkDataset, IDENetworkDataset dataElement, INetworkSource source, IEvaluatedNetworkAttribute attribute)
		{
			// Initialize is called once per session (ArcMap session, ArcCatalog session, etc.) to initialize the evaluator for an associated network dataset
			Type t = Type.GetTypeFromProgID("esriFramework.AppRef");
			try
			{
				// Activator.CreateInstance(t) is expected to error if the evaluator is created in an engine application 
				// which can�t get a reference to the AppRef singleton.  
				// This evaluator won�t work in Engine due to this design limitation.  It is, however,
				// fully functional in ArcMap.
				System.Object obj = Activator.CreateInstance(t);
				IApplication app = obj as IApplication;
				if (app != null && app is IMxApplication)
					m_mxDocument = app.Document as IMxDocument;
			}
			catch (Exception e)
			{
				m_mxDocument = null;
			}

			// Store reference to the network dataset and the network source
			m_networkDataset = networkDataset;
			m_networkSource = source;

			// Create a new Dictionary hashtable for this network source
			m_sourceHashTable = new Dictionary<int, int>();
		}

		public object QueryValue(INetworkElement element, IRow row)
		{
			// This element is restricted if its associated ObjectID is currently stored within the network source's hashtable
			return m_sourceHashTable.ContainsKey(element.OID);
		}

		public bool SupportsDefault(esriNetworkElementType elementType, IEvaluatedNetworkAttribute attribute)
		{
			// This custom evaluator can not be used for assigning default attribute values
			return false;
		}

		public bool SupportsSource(INetworkSource source, IEvaluatedNetworkAttribute attribute)
		{
			// This custom evaluator supports restriction attributes for all sources
			return attribute.UsageType == esriNetworkAttributeUsageType.esriNAUTRestriction;
		}

		public bool ValidateDefault(esriNetworkElementType elementType, IEvaluatedNetworkAttribute attribute, ref int errorCode, ref string errorDescription, ref string errorAppendInfo)
		{
			if (SupportsDefault(elementType, attribute))
			{
				errorCode = 0;
				errorDescription = errorAppendInfo = string.Empty;
				return true;
			}
			else
			{
				errorCode = -1;
				errorDescription = errorAppendInfo = string.Empty;
				return false;
			}
		}

		public bool ValidateSource(IDatasetContainer2 datasetContainer, INetworkSource networkSource, IEvaluatedNetworkAttribute attribute, ref int errorCode, ref string errorDescription, ref string errorAppendInfo)
		{
			if (SupportsSource(networkSource, attribute))
			{
				errorCode = 0;
				errorDescription = errorAppendInfo = string.Empty;
				return true;
			}
			else
			{
				errorCode = -1;
				errorDescription = errorAppendInfo = string.Empty;
				return false;
			}
		}

		#endregion

		#region INetworkEvaluator2 Members

		public void Refresh()
		{
			// This method is called internally during a solve operation immediately prior to performing the actual solve
			// This gives us an opportunity to update our evaluator's internal state based on changes to the current source feature selection set within ArcMap

			if (m_mxDocument != null)
			{
				// Clear the hashtable of any previous selections
				m_sourceHashTable.Clear();

				// Loop through every layer in the map, find the appropriate network source feature layer, and add its selection set to the source hashtable
				IMap map = m_mxDocument.FocusMap;
				IFeatureClassContainer fcContainer = m_networkDataset as IFeatureClassContainer;
				IFeatureClass sourceFC = fcContainer.get_ClassByName(m_networkSource.Name);

				ILayer layer;
				IFeatureClass layerFC;
				IEnumLayer enumLayer = map.get_Layers(null, true);
				while ((layer = enumLayer.Next()) != null)
				{
					if (layer.Visible && layer is IFeatureLayer)
					{
						layerFC = ((IFeatureLayer)layer).FeatureClass;
						if (layerFC == sourceFC)
						{
							IFeatureSelection featureSelection = layer as IFeatureSelection;
							ISelectionSet selectionSet = featureSelection.SelectionSet;
							IEnumIDs idEnumerator = selectionSet.IDs;
							idEnumerator.Reset();
							int oid;
							while ((oid = idEnumerator.Next()) != -1)
							{
								m_sourceHashTable.Add(oid, oid);
							}
							break;
						}
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
	}
}
