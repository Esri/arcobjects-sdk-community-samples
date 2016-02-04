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
using ESRI.ArcGIS.Schematic;

namespace SchematicFeatureRemoveCS
{
	public class FeatureRemovalMgmt : ESRI.ArcGIS.Desktop.AddIns.Extension
	{
		private SchematicDatasetManager m_SchemDatasetMgr;
		private const string DiagramClassName = "InsidePlants";
		private const string AttributeNameObjectID = "Object_Id";
		private const string TableNameNodes = "Inside_Nodes";
		private const string TableNameLinks = "Inside_Links";
		private const string SampleDatasetName = "RemovalSample_Schematic";

		protected override void OnStartup()
		{
			m_SchemDatasetMgr = new SchematicDatasetManager();
			m_SchemDatasetMgr.BeforeRemoveFeature += new ISchematicDatasetEvents_BeforeRemoveFeatureEventHandler(OnBeforeRemoveFeature);
		}

		protected override void OnShutdown()
		{
			m_SchemDatasetMgr.BeforeRemoveFeature -= OnBeforeRemoveFeature;
			m_SchemDatasetMgr = null;
			base.OnShutdown();
		}

		void OnBeforeRemoveFeature(ISchematicInMemoryFeature inMemoryFeature, ref bool canRemove)
		{
			if (State != ESRI.ArcGIS.Desktop.AddIns.ExtensionState.Enabled) return;

			ESRI.ArcGIS.Geodatabase.IDataset esriData = (ESRI.ArcGIS.Geodatabase.IDataset)inMemoryFeature.SchematicDiagram.SchematicDiagramClass.SchematicDataset;

			//  Remove only elements contained in a specific Schematic dataset
			if (esriData.Name != SampleDatasetName) return;
			//  Remove only elements contained in a specific type of diagram
			if (inMemoryFeature.SchematicDiagram.SchematicDiagramClass.Name != DiagramClassName) return;

			canRemove = false;
			// can't remove SubStation
			if (inMemoryFeature.SchematicElementClass.Name == "InsidePlant_SubStation") return;

			ISchematicDiagramClass schemDiagramClass;
			schemDiagramClass = (ISchematicDiagramClass)inMemoryFeature.SchematicDiagram.SchematicDiagramClass;

			//  For this specific diagram type, we retrieve the datasource 
			//  and the tables where the elements are stored
			ISchematicDataSource schemDataSource = schemDiagramClass.SchematicDataSource;

			string tableName = "";
			switch (inMemoryFeature.SchematicElementClass.SchematicElementType)
			{
				case esriSchematicElementType.esriSchematicNodeType:
					tableName = TableNameNodes;
					break;
				case esriSchematicElementType.esriSchematicLinkType:
					tableName = TableNameLinks;
					break;
				case esriSchematicElementType.esriSchematicDrawingType:
					return;
					break;
			}

			// Retrieve Feature Workspace
			ESRI.ArcGIS.Geodatabase.IWorkspace esriWorkspace = (ESRI.ArcGIS.Geodatabase.IWorkspace)schemDataSource.Object;
			ESRI.ArcGIS.Geodatabase.IFeatureWorkspace esriFeatureWorkspace = (ESRI.ArcGIS.Geodatabase.IFeatureWorkspace)esriWorkspace;

			// Get Attributes values
			ISchematicAttributeContainer schemAttribCont = (ISchematicAttributeContainer)inMemoryFeature.SchematicElementClass;
			ISchematicAttributeContainer schemFatherAttribCont = (ISchematicAttributeContainer)inMemoryFeature.SchematicElementClass.Parent;

			if ((!(schemAttribCont.GetSchematicAttribute(AttributeNameObjectID, false) == null)
				|| !(schemFatherAttribCont.GetSchematicAttribute(AttributeNameObjectID, false) == null)))
			{
				int indField = inMemoryFeature.Fields.FindFieldByAliasName(AttributeNameObjectID);
				int OID = int.Parse(inMemoryFeature.get_Value(indField).ToString(), System.Globalization.NumberStyles.Integer);
				//Get table and row
				ESRI.ArcGIS.Geodatabase.ITable esriTable = esriFeatureWorkspace.OpenTable(tableName);
				ESRI.ArcGIS.Geodatabase.IRow esriRow = esriTable.GetRow(OID);

				//  When the row is identified in the table, it is deleted and
				//  the CanRemove returns True so that the associated
				//  schematic element is graphically removed from the active diagram
				if (!(esriRow == null))
				{
					esriRow.Delete();
					canRemove = true;
				}
			}
		}
	}
}
