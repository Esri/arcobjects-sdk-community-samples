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

Public Class FeatureRemovalMgmt
	Inherits ESRI.ArcGIS.Desktop.AddIns.Extension

	Private m_SchemDatasetMgr As SchematicDatasetManager
	Private Const DiagramClassName As String = "InsidePlants"
	Private Const AttributeNameObjectID As String = "Object_Id"
	Private Const TableNameNodes As String = "Inside_Nodes"
	Private Const TableNameLinks As String = "Inside_Links"
	Private Const SampleDatasetName As String = "RemovalSample_Schematic"

	Protected Overrides Sub OnStartup()
		m_SchemDatasetMgr = New SchematicDatasetManager()
		AddHandler m_SchemDatasetMgr.BeforeRemoveFeature, AddressOf OnBeforeRemoveFeature
	End Sub

	Protected Overrides Sub OnShutdown()
		RemoveHandler m_SchemDatasetMgr.BeforeRemoveFeature, AddressOf OnBeforeRemoveFeature
		m_SchemDatasetMgr = Nothing
	End Sub

	Sub OnBeforeRemoveFeature(ByVal inMemoryFeature As ISchematicInMemoryFeature, ByRef canRemove As Boolean)

		If (State <> ESRI.ArcGIS.Desktop.AddIns.ExtensionState.Enabled) Then Return

		Dim esriData As ESRI.ArcGIS.Geodatabase.IDataset = CType(inMemoryFeature.SchematicDiagram.SchematicDiagramClass.SchematicDataset, ESRI.ArcGIS.Geodatabase.IDataset)

		'  Remove only elements contained in a specific Schematic dataset
		If (esriData.Name <> SampleDatasetName) Then Return
		'  Remove only elements contained in a specific type of diagram
		If (inMemoryFeature.SchematicDiagram.SchematicDiagramClass.Name <> DiagramClassName) Then Return

		canRemove = False
		' can't remove SubStation
		If (inMemoryFeature.SchematicElementClass.Name = "InsidePlant_SubStation") Then Return

		Dim schemDiagramClass As ISchematicDiagramClass
		schemDiagramClass = CType(inMemoryFeature.SchematicDiagram.SchematicDiagramClass, ISchematicDiagramClass)

		'  For this specific diagram type, we retrieve the datasource 
		'  and the tables where the elements are stored
		Dim schemDataSource As ISchematicDataSource = schemDiagramClass.SchematicDataSource

		Dim tableName As String = ""
		Select Case (inMemoryFeature.SchematicElementClass.SchematicElementType)
			Case esriSchematicElementType.esriSchematicNodeType
				tableName = TableNameNodes
			Case esriSchematicElementType.esriSchematicLinkType
				tableName = TableNameLinks
			Case esriSchematicElementType.esriSchematicDrawingType
				Return
		End Select

		' Retrieve Feature Workspace
		Dim esriWorkspace As ESRI.ArcGIS.Geodatabase.IWorkspace = CType(schemDataSource.Object, ESRI.ArcGIS.Geodatabase.IWorkspace)
		Dim esriFeatureWorkspace As ESRI.ArcGIS.Geodatabase.IFeatureWorkspace = CType(esriWorkspace, ESRI.ArcGIS.Geodatabase.IFeatureWorkspace)

		' Get Attributes values
		Dim schemAttribCont As ISchematicAttributeContainer = CType(inMemoryFeature.SchematicElementClass, ISchematicAttributeContainer)
		Dim schemFatherAttribCont As ISchematicAttributeContainer = CType(inMemoryFeature.SchematicElementClass.Parent, ISchematicAttributeContainer)

		If ((schemAttribCont.GetSchematicAttribute(AttributeNameObjectID, False) IsNot Nothing) OrElse (schemFatherAttribCont.GetSchematicAttribute(AttributeNameObjectID, False) IsNot Nothing)) Then

			Dim indField As Integer = inMemoryFeature.Fields.FindFieldByAliasName(AttributeNameObjectID)
			Dim OID As Integer = Integer.Parse(inMemoryFeature.Value(indField).ToString(), System.Globalization.NumberStyles.Integer)

			'Get table and row
			Dim esriTable As ESRI.ArcGIS.Geodatabase.ITable = esriFeatureWorkspace.OpenTable(tableName)
			Dim esriRow As ESRI.ArcGIS.Geodatabase.IRow = esriTable.GetRow(OID)

			'  When the row is identified in the table, it is deleted and
			'  the CanRemove returns True so that the associated
			'  schematic element is graphically removed from the active diagram
			If (esriRow IsNot Nothing) Then
				esriRow.Delete()
				canRemove = True
			End If
		End If
	End Sub
End Class
