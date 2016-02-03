Imports ESRI.ArcGIS.Schematic

''' <summary>
''' EnforcingDiagramName class handling event AfterGenerateDiagram in order to prefix the name of the generated diagrams
''' </summary>
Public Class EnforcingDiagramName
	Inherits ESRI.ArcGIS.Desktop.AddIns.Extension

	Private m_schematicDatasetMgr As SchematicDatasetManager

	Public Sub New()

	End Sub

	Protected Overrides Sub OnStartup()
		' Instantiate the schematic dataset manager which fires events coming from all schematic datasets
		m_schematicDatasetMgr = New SchematicDatasetManager()

		' Handles new diagram generation
		AddHandler m_schematicDatasetMgr.AfterGenerateDiagram, AddressOf OnAfterGenerateDiagram
	End Sub

	Protected Overrides Sub OnShutdown()
		RemoveHandler m_schematicDatasetMgr.AfterGenerateDiagram, AddressOf OnAfterGenerateDiagram
		m_schematicDatasetMgr = Nothing
	End Sub

	''' <summary>
	''' Occurs when a new diagram is generated
	''' </summary>
	''' <param name="schematicDiagram">Schematic diagram just generated</param>
	Sub OnAfterGenerateDiagram(ByVal schematicDiagram As ISchematicDiagram)
		' Add user name before diagram name
		Dim userName As String = System.Environment.UserName

		If State = ESRI.ArcGIS.Desktop.AddIns.ExtensionState.Enabled Then
			schematicDiagram.Name = userName + "_" + schematicDiagram.Name
		End If
	End Sub
End Class
