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
namespace CustomExtCriteriaCS
{
	[System.Runtime.InteropServices.ClassInterface(System.Runtime.InteropServices.ClassInterfaceType.None)]
	[System.Runtime.InteropServices.Guid(CollapseRelatedElts.GUID)]
	[System.Runtime.InteropServices.ProgId(CollapseRelatedElts.PROGID)]
	public class CollapseRelatedElts : ESRI.ArcGIS.Schematic.ISchematicCollapseRelatedElementsExtended
	{
		public const string GUID = "A851A853-48B2-4701-B133-6A08F3E0DEB6";
		public const string PROGID = "CustomExtCriteriaCS.CollapseRelatedElts";

		#region Component Category Registration
		[System.Runtime.InteropServices.ComRegisterFunction()]
		public static void Register(string CLSID)
		{
			ESRI.ArcGIS.ADF.CATIDs.SchematicRulesExtendedCriteria.Register(CLSID);
		}

		[System.Runtime.InteropServices.ComUnregisterFunction()]
		public static void Unregister(string CLSID)
		{
			ESRI.ArcGIS.ADF.CATIDs.SchematicRulesExtendedCriteria.Unregister(CLSID);
		}
		#endregion

		#region ISchematicCollapseRelatedElementsExtended Implementations
		public string Name
		{
			get
			{
				return "Test extended collapse (C#)";
			}
		}

		public ESRI.ArcGIS.Schematic.IEnumSchematicInMemoryFeature SelectElementsToCollapse(ESRI.ArcGIS.Schematic.ISchematicInMemoryFeatureNode node, ESRI.ArcGIS.Schematic.IEnumSchematicInMemoryFeature relatedFeatures)
		{
			// get feature
			ESRI.ArcGIS.Geodatabase.IFeature esriFeature;
			esriFeature = node as ESRI.ArcGIS.Geodatabase.IFeature;
			if (esriFeature == null) return null;

			// get feature class
			ESRI.ArcGIS.Geodatabase.IFeatureClass esriFeatureClass;
			esriFeatureClass = esriFeature.Class as ESRI.ArcGIS.Geodatabase.IFeatureClass;

			// if not the right feature class do nothing
			if (esriFeatureClass.AliasName != "plants") return null;

			bool okToCollapse = true;

			relatedFeatures.Reset();

			// Test if you want to collapse related element
			//ESRI.ArcGIS.Schematic.ISchematicElement schemElement = relatedElements.Next();
			//while ((schemElement != null) && okToCollapse)
			//{
			//  okToCollapse = CanCollapseElement(schemElement);
			//  schemElement = relatedElements.Next();
			//}

			if (!okToCollapse)
				return null; // if nothing to collapse return nothing
			else if (relatedFeatures.Count == 0)
			{
				EnumCollapsedElts enumCollapse = new EnumCollapsedElts(); // create a list of feature to collapse

				// get incident links
				ESRI.ArcGIS.Schematic.IEnumSchematicInMemoryFeatureLink enumLinks;
				enumLinks = node.GetIncidentLinks(ESRI.ArcGIS.Schematic.esriSchematicEndPointType.esriSchematicOriginOrExtremityNode);

				if (enumLinks == null) return enumCollapse;
				if (enumLinks.Count > 1)
				{
					enumLinks.Reset();
					ESRI.ArcGIS.Schematic.ISchematicInMemoryFeatureLink schemLink;
					// for each link
					schemLink = enumLinks.Next();
					if (schemLink != null)
					{
						enumCollapse.Add((ESRI.ArcGIS.Schematic.ISchematicInMemoryFeature)node); // Add node to collapse

						while (schemLink != null)
						{
							enumCollapse.Add((ESRI.ArcGIS.Schematic.ISchematicInMemoryFeature)schemLink); // Add link to collapse
							schemLink = enumLinks.Next();
						}
					}
				}
				return enumCollapse;
			}
			else
			{
				return relatedFeatures;
			}
		}
		#endregion
	}
}
