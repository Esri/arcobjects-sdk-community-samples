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
namespace CustomExtCriteriaCS
{
	[System.Runtime.InteropServices.ClassInterface(System.Runtime.InteropServices.ClassInterfaceType.None)]
	[System.Runtime.InteropServices.Guid(PlantOnCableDiameter.GUID)]
	[System.Runtime.InteropServices.ProgId(PlantOnCableDiameter.PROGID)]
	public class PlantOnCableDiameter : ESRI.ArcGIS.Schematic.ISchematicNodeReductionExtended
	{
		public const string GUID = "28231119-7E86-4924-AC04-F5AE21858112";
		public const string PROGID = "CustomExtCriteriaCS.PlantOnCableDiameter";

		#region Component Category Registration
		[System.Runtime.InteropServices.ComRegisterFunction()]
		public static void Register(string regKey)
		{
			ESRI.ArcGIS.ADF.CATIDs.SchematicRulesExtendedCriteria.Register(regKey);
		}

		[System.Runtime.InteropServices.ComUnregisterFunction()]
		public static void Unregister(string regKey)
		{
			ESRI.ArcGIS.ADF.CATIDs.SchematicRulesExtendedCriteria.Unregister(regKey);
		}
		#endregion

		#region SchematicNodeReductionExtended Implementations
		//  Description of the new schematic node reduction criteria
		public string Name
		{
			get
			{
				return "Reduce if connected cable diameters are 8 (C#)";
			}
		}

		public bool SelectReduction(ESRI.ArcGIS.Schematic.ISchematicInMemoryFeatureNode node, ESRI.ArcGIS.Schematic.IEnumSchematicInMemoryFeature enumLink, ref ESRI.ArcGIS.Schematic.ISchematicInMemoryFeatureLink link)
		{
			// if enumLink is empty do nothing
			if (enumLink == null) return false;
			if (enumLink.Count == 0) return false;

			enumLink.Reset();

			ESRI.ArcGIS.Schematic.ISchematicInMemoryFeature schemAssociatedLink;
			schemAssociatedLink = enumLink.Next();

			// for each link in enumLink
			while (schemAssociatedLink != null)
			{
				// get cables
				ESRI.ArcGIS.Geodatabase.IFeature cablesFeature;
				cablesFeature = schemAssociatedLink.SchematicElement as ESRI.ArcGIS.Geodatabase.IFeature;

				if (cablesFeature == null) return false;

				// get cables class
				ESRI.ArcGIS.Geodatabase.IDataset cablesDataset;
				cablesDataset = (ESRI.ArcGIS.Geodatabase.IDataset)cablesFeature.Class;

				// if not the right class do nothing
				if (cablesDataset.Name.IndexOf("cables") == 0) return false;

				// get workspace
				ESRI.ArcGIS.Geodatabase.IFeatureWorkspace cablesWorkspace;
				cablesWorkspace = (ESRI.ArcGIS.Geodatabase.IFeatureWorkspace)cablesDataset.Workspace;

				// open table cables_attributes
				ESRI.ArcGIS.Geodatabase.ITable cablesTable;
				cablesTable = cablesWorkspace.OpenTable("cables_attributes");
				if (cablesTable == null) return false;

				// get diameter value
				object cableDiameter = cablesTable.GetRow(cablesFeature.OID).get_Value(1);

				if (cableDiameter.ToString() != "8") return false; //if not 8 do nothing

				schemAssociatedLink = enumLink.Next();
			}
			return true; // if this far
		}
		#endregion
	}
}
