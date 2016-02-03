using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ESRI.ArcGIS.Schematic;

namespace EnforcingDiagramName
{
	/// <summary>
	/// EnforcingDiagramName class handling event AfterGenerateDiagram in order to prefix the name of the generated diagrams
	/// </summary>
	public class EnforcingDiagramName : ESRI.ArcGIS.Desktop.AddIns.Extension
	{

		private SchematicDatasetManager m_schematicDatasetMgr;

		public EnforcingDiagramName()
		{
		}

		protected override void OnStartup()
		{
		// Instantiate the schematic dataset manager which fires events coming from all schematic datasets
			m_schematicDatasetMgr = new SchematicDatasetManager();
			// Handles new diagram generation
			m_schematicDatasetMgr.AfterGenerateDiagram += new ISchematicDatasetEvents_AfterGenerateDiagramEventHandler(OnAfterGenerateDiagram);
		}

		protected override void OnShutdown()
		{
			m_schematicDatasetMgr.AfterGenerateDiagram -= new ISchematicDatasetEvents_AfterGenerateDiagramEventHandler(OnAfterGenerateDiagram);
			m_schematicDatasetMgr = null;
		}

		/// <summary>
		/// Occurs when a new diagram is generated
		/// </summary>
		/// <param name="schematicDiagram">Schematic diagram just generated</param>
		void OnAfterGenerateDiagram(ISchematicDiagram schematicDiagram)
		{
			// Add user name before generate diagram name
			string userName = System.Environment.UserName;

			if (State == ESRI.ArcGIS.Desktop.AddIns.ExtensionState.Enabled)
				schematicDiagram.Name = userName + "_" + schematicDiagram.Name;
		}

	}

}
