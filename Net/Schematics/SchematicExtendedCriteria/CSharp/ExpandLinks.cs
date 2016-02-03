using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.Schematic;
using ESRI.ArcGIS.Geodatabase;

namespace CustomExtCriteriaCS
{
	[ClassInterface(System.Runtime.InteropServices.ClassInterfaceType.None)]
	[Guid(ExpandLinks.GUID)]
	[ProgId(ExpandLinks.PROGID)]
	public class ExpandLinks : ISchematicExpandLinksByAttributeExtended
	{
		public const string GUID = "F942BDC2-2AD4-47c4-B96D-A8CA0002E9B3";
		public const string PROGID = "CustomExtCriteriaCS.ExpandLinks";

		#region Component Category Registration
		[ComRegisterFunction()]
		public static void Register(string CLSID)
		{
			ESRI.ArcGIS.ADF.CATIDs.SchematicRulesExtendedCriteria.Register(CLSID);
		}

		[ComUnregisterFunction()]
		public static void Unregister(string CLSID)
		{
			ESRI.ArcGIS.ADF.CATIDs.SchematicRulesExtendedCriteria.Unregister(CLSID);
		}
		#endregion

		#region ISchematicExpandLinksByAttributeExtended Membres

		public string Evaluate(ISchematicInMemoryFeature schematicFeature)
		{
			if (schematicFeature.SchematicElementClass.SchematicElementType != esriSchematicElementType.esriSchematicLinkType) return "";

			ISchematicInMemoryFeatureLink schemLink;
			schemLink = (ISchematicInMemoryFeatureLink)schematicFeature;

			// Get From node
			ISchematicInMemoryFeatureNode schemFromNode;
			schemFromNode = schemLink.FromNode;

			// Get Associated feature
			ISchematicElementAssociatedObject scheAsso;
			scheAsso = (ISchematicElementAssociatedObject)schemFromNode.SchematicElement;

			IFeature esriFeature;
			esriFeature = (IFeature)scheAsso.AssociatedObject;

			// Get list of fields
			IFields esriFields;
			esriFields = esriFeature.Fields;
			int numField;
			// Find Field MaxOutLines
			numField = esriFields.FindFieldByAliasName("MaxOutLines");
			if (numField <= 0) return ""; // Field not found

			// Return value of the field
			return esriFeature.get_Value(numField).ToString();
		}

		public string Name
		{
			get { return "Use origin plant's MaxOutLines value (C#)"; }
		}

		#endregion
	}
}
