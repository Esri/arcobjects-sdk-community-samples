using ESRI.ArcGIS.Schematic;
using ESRI.ArcGIS.Geodatabase;

namespace LinkerManagerCS
{
	public class ElementFeatureAssociation : ESRI.ArcGIS.Desktop.AddIns.Extension
	{
		private SchematicDatasetManager m_schemDatasetMgr;
		// Attribute names that specified the ClassName and OBJECTID of the feature related to each schematic element
		private const string AttClassNameName = "RelatedFeatureCN";
		private const string AttObjectIdName = "RelatedFeatureOID";

		protected override void OnStartup()
		{
			m_schemDatasetMgr = new SchematicDatasetManager();
			m_schemDatasetMgr.AfterLoadDiagram += new ISchematicDatasetEvents_AfterLoadDiagramEventHandler(OnAfterLoadDiagram);
			m_schemDatasetMgr.AfterRefreshDiagram += new ISchematicDatasetEvents_AfterRefreshDiagramEventHandler(OnAfterRefreshDiagram);
		}

		protected override void OnShutdown()
		{
			m_schemDatasetMgr.AfterRefreshDiagram -= OnAfterRefreshDiagram;
			m_schemDatasetMgr.AfterLoadDiagram -= OnAfterLoadDiagram;
			m_schemDatasetMgr = null;

			base.OnShutdown();
		}

		public void OnAfterLoadDiagram(ISchematicInMemoryDiagram inMemoryDiagram)
		{
			// if add-in is not enabled then quit
			if (State != ESRI.ArcGIS.Desktop.AddIns.ExtensionState.Enabled) return;

			IEnumSchematicInMemoryFeature enumSchemElements;
			ISchematicInMemoryFeature schemElement;
			string nameRelatedCN = "";
			int numRelatedOID = 0;
			IFeatureClass esriFeatureClass;
			IFeature esriFeature;
			ISchematicInMemoryFeatureLinkerEdit schemFeatureLinkerEdit = (ISchematicInMemoryFeatureLinkerEdit)new SchematicLinker();
			ISchematicObjectClass myObjectType;
			ISchematicAttribute schemAttributeCID;
			ISchematicAttribute schemAttributeOID;
			ISchematicAttributeContainer schemAttributeContainer;

			IWorkspace esriWorkspace;
			Microsoft.VisualBasic.Collection colFCByName;

			colFCByName = new Microsoft.VisualBasic.Collection();
			esriWorkspace = inMemoryDiagram.SchematicDiagramClass.SchematicDataset.SchematicWorkspace.Workspace;

			// Retrieve all schematic element of the diagram

			enumSchemElements = (IEnumSchematicInMemoryFeature)inMemoryDiagram.SchematicInMemoryFeatures;
			enumSchemElements.Reset();

			schemElement = enumSchemElements.Next();
			while (schemElement != null)
			{
				// retrieve attribute
				myObjectType = schemElement.SchematicElementClass;
				schemAttributeContainer = (ISchematicAttributeContainer)schemElement.SchematicElementClass;
				schemAttributeCID = schemAttributeContainer.GetSchematicAttribute(AttClassNameName, false);
				schemAttributeOID = schemAttributeContainer.GetSchematicAttribute(AttObjectIdName, false);
				if (schemAttributeCID != null && schemAttributeOID != null)
				{
					// get value of attribute
					nameRelatedCN = schemAttributeCID.GetValue((ISchematicObject)schemElement).ToString();
					numRelatedOID = System.Convert.ToInt32(schemAttributeOID.GetValue((ISchematicObject)schemElement));

					// get feature from geodatabase
					esriFeatureClass = FindFeatureClassByName(esriWorkspace, nameRelatedCN, colFCByName);
					if (esriFeatureClass != null)
					{
						// get feature from FeatureClass
						esriFeature = esriFeatureClass.GetFeature(numRelatedOID);
						if (esriFeature != null)
						{
							// Associate geographical feature with schematic feature
							schemFeatureLinkerEdit.Associate(schemElement, esriFeature);
						}
					}
				}
				schemElement = enumSchemElements.Next();
			}
			colFCByName.Clear();
			colFCByName = null;
		}

		public void OnAfterRefreshDiagram(ESRI.ArcGIS.Schematic.ISchematicInMemoryDiagram InMemoryDiagram)
		{
			OnAfterLoadDiagram(InMemoryDiagram);
		}

		private IFeatureClass FindFeatureClassByName(IWorkspace workspace, string name, Microsoft.VisualBasic.Collection colFCByName)
		{
			IFeatureClass esriFeatureClass = null;

			try
			{
				// try to retrieve FeatureClass in collection
				esriFeatureClass = (IFeatureClass)colFCByName[name];
				return esriFeatureClass;
			}
			catch { }

			IEnumDataset enumDatasets;
			IDataset esriDataset;
			IFeatureClassContainer featContainer;

			// get datasets
			enumDatasets = workspace.get_Datasets(esriDatasetType.esriDTFeatureDataset);
			enumDatasets.Reset();
			esriDataset = enumDatasets.Next();
			while (esriDataset != null)
			{
				// try to find class in dataset
				try
				{
					featContainer = (IFeatureClassContainer)esriDataset;
					// get FeatureClass from current dataset
					esriFeatureClass = featContainer.get_ClassByName(name);
					if (esriFeatureClass != null)
					{
						// if exists add to collection and quit
						colFCByName.Add(esriFeatureClass, name, null, null);
						return esriFeatureClass;
					}
				}
				catch { }
				// try another dataset
				esriDataset = enumDatasets.Next();
			}

			try
			{
				// try to find FeatureClass in workspace
				featContainer = (IFeatureClassContainer)workspace;
				esriFeatureClass = featContainer.get_ClassByName(name);
				if (esriFeatureClass != null)
				{
					// if exists add to collection and quit
					colFCByName.Add(esriFeatureClass, name, null, null);
					return esriFeatureClass;
				}
			}
			catch { }
			return null;
		}
	}
}
