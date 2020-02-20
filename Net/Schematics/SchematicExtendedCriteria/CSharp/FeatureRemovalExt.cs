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

namespace CustomExtCriteriaCS
{
	[System.Runtime.InteropServices.ClassInterface(System.Runtime.InteropServices.ClassInterfaceType.None)]
	[System.Runtime.InteropServices.Guid(FeatureRemovalExt.GUID)]
	[System.Runtime.InteropServices.ProgId(FeatureRemovalExt.PROGID)]
	public class FeatureRemovalExt : ESRI.ArcGIS.Schematic.ISchematicFeatureRemovalExtended
	{
		public const string GUID = "f1ff48de-93b1-4765-b943-d04284e63fb9";
		public const string PROGID = "CustomExtCriteriaCS.FeatureRemovalExt";

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

		#region ISchematicFeatureRemovalExtended Membres

		public string Name
		{
			get { return "Remove cables with particular ID (C#)"; }
		}

		public bool Evaluate(ESRI.ArcGIS.Schematic.ISchematicInMemoryFeature schematicFeature)
		{
			// if not the right class do nothing
			if (schematicFeature.SchematicElementClass.Name != "cables") return false;

			// Remove specific schematic elements
			if ((schematicFeature.Name == "5-7-0") || (schematicFeature.Name == "5-4-0"))
				return true;

			return false;
		}

		#endregion
	}
}
