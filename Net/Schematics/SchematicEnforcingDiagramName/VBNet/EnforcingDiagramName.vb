'Copyright 2019 Esri

'Licensed under the Apache License, Version 2.0 (the "License");
'you may not use this file except in compliance with the License.
'You may obtain a copy of the License at

'    http://www.apache.org/licenses/LICENSE-2.0

'Unless required by applicable law or agreed to in writing, software
'distributed under the License is distributed on an "AS IS" BASIS,
'WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.

'See the License for the specific language governing permissions and
'limitations under the License.
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
