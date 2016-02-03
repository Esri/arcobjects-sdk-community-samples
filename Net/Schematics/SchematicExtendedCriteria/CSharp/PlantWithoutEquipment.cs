namespace CustomExtCriteriaCS
{
	[System.Runtime.InteropServices.ClassInterface(System.Runtime.InteropServices.ClassInterfaceType.None)]
	[System.Runtime.InteropServices.Guid(PlantWithoutEquipment.GUID)]
	[System.Runtime.InteropServices.ProgId(PlantWithoutEquipment.PROGID)]
	public class PlantWithoutEquipment : ESRI.ArcGIS.Schematic.ISchematicNodeReductionExtended
	{
		public const string GUID = "08CABF7A-1CC8-4cc2-A950-67485A56F7BA";
		public const string PROGID = "CustomExtCriteriaCS.PlantWithoutEquipment";

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

		#region ISchematicNodeReductionExtended Implementations
		// Implementation of a new ISchematicNodeReductionExtended interface 
		// to be used as an additional criteria during the execution of a 
		// Node Reduction By Priority rule

		//  Description of the new schematic node reduction criteria
		public string Name
		{
			get
			{
				return "Reduce plant without equipments (C#)";
			}
		}

		//The SelectReduction procedure works with the input node schematic node candidate to the 
		//reduction and with the input linkElements list of schematic link elements incident to 
		//this schematic node.	It must return True for the output reduce boolean parameter if 
		//the node is reduced, false if the node is kept.	 When the output ppLink schematic link 
		//is not nothing, it determines the target node that will be used to reconnect the links 
		//incidents to the reduced node.	In this sample procedure, the node candidate to the 
		//reduction is analyzed. If records related to this node exist in the plants_equipments table, 
		//the node is kept (output reduce parameter is False); else, it is reduced (output reduce 
		//parameter is True).
        public bool SelectReduction(ESRI.ArcGIS.Schematic.ISchematicInMemoryFeatureNode node, ESRI.ArcGIS.Schematic.IEnumSchematicInMemoryFeature enumLink, ref ESRI.ArcGIS.Schematic.ISchematicInMemoryFeatureLink link)
        {
            // if associated feature doesn't exist do nothing
            ESRI.ArcGIS.Schematic.ISchematicInMemoryFeature schemAssociatedNode;
            schemAssociatedNode = node as ESRI.ArcGIS.Schematic.ISchematicInMemoryFeature;
            if (schemAssociatedNode == null) return false;

            // if dataset is not plants do nothing
            ESRI.ArcGIS.Geodatabase.IDataset schemElementClass;
            schemElementClass = (ESRI.ArcGIS.Geodatabase.IDataset)schemAssociatedNode.SchematicElementClass;
            if (schemElementClass == null) return false;
            if (schemElementClass.Name.IndexOf("plants") < 0) return false;

            // get workspace
            ESRI.ArcGIS.Geodatabase.IFeatureWorkspace plantsWorkspace;
            plantsWorkspace = (ESRI.ArcGIS.Geodatabase.IFeatureWorkspace)schemElementClass.Workspace;

            // open table plants_equipments
            ESRI.ArcGIS.Geodatabase.ITable plantsEquipment;
            plantsEquipment = plantsWorkspace.OpenTable("plants_equipments");
            if (plantsEquipment == null) return false;


            // filter for the selected feature
            ESRI.ArcGIS.Schematic.ISchematicInMemoryFeaturePrimaryAssociation schemAssociation;
            schemAssociation = schemAssociatedNode as ESRI.ArcGIS.Schematic.ISchematicInMemoryFeaturePrimaryAssociation;
            if (schemAssociation == null) return false;

            ESRI.ArcGIS.Geodatabase.IQueryFilter plantsFilter;
            plantsFilter = new ESRI.ArcGIS.Geodatabase.QueryFilterClass();
            plantsFilter.WhereClause = ("PlantID = " + schemAssociation.ObjectID);

            ESRI.ArcGIS.Geodatabase.ICursor plantsCursor;
            plantsCursor = plantsEquipment.Search(plantsFilter, true);

            // if found equipment return false
            if (plantsCursor != null && plantsCursor.NextRow() != null) return false;

            return true; // if this far
        }
		#endregion

	}
}
