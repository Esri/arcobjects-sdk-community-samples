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
Imports ESRI.ArcGIS.Geodatabase

Public Class ElementFeatureAssociation
	Inherits ESRI.ArcGIS.Desktop.AddIns.Extension

	'The OnAfterLoadDiagram procedure is used to specify the associations 
	'between schematic elements and features.
	'The diagram contains schematic elements for which particular attributes have been created. 
	'These attributes specify the ClassID and the ObjectID the schematic element is related to.
	'The ISchematicInMemoryFeatureLinkerEdit Associate method is used to specify 
	'that a schematic element is associated with a feature.

	Private m_schemDatasetMgr As SchematicDatasetManager

	' Attribute names that specified the ClassName and OBJECTID of the feature related to each schematic element
	Private Const AttClassNameName As String = "RelatedFeatureCN"
	Private Const AttObjectIdName As String = "RelatedFeatureOID"

	Protected Overrides Sub OnStartup()
		m_schemDatasetMgr = New SchematicDatasetManager
		AddHandler m_schemDatasetMgr.AfterLoadDiagram, AddressOf OnAfterLoadDiagram
	End Sub

	Protected Overrides Sub OnShutdown()
		RemoveHandler m_schemDatasetMgr.AfterLoadDiagram, AddressOf OnAfterLoadDiagram
		m_schemDatasetMgr = Nothing
	End Sub

	Public Sub OnAfterLoadDiagram(ByVal inMemoryDiagram As ESRI.ArcGIS.Schematic.ISchematicInMemoryDiagram)
        ' if add-in is not enabled then quit
		If State <> ESRI.ArcGIS.Desktop.AddIns.ExtensionState.Enabled Then Return

		Dim enumSchemElements As IEnumSchematicInMemoryFeature
		Dim schemElement As ISchematicInMemoryFeature
		Dim nameRelatedCN As String
		Dim numRelatedOID As Long
		Dim esriFeatureClass As IFeatureClass
		Dim esriFeature As IFeature
		Dim schemFeatureLinkerEdit As ISchematicInMemoryFeatureLinkerEdit = New SchematicLinker
		Dim schemObjectType As ISchematicObjectClass
		Dim schemAttributeCID As ISchematicAttribute
		Dim schemAttributeOID As ISchematicAttribute
		Dim schemAttributeContainer As ISchematicAttributeContainer
		Dim esriWorkspace As IWorkspace
		Dim colFCByName As Collection

		colFCByName = New Collection
		esriWorkspace = inMemoryDiagram.SchematicDiagramClass.SchematicDataset.SchematicWorkspace.Workspace

		' Retrieve all schematic element of the diagram
		enumSchemElements = CType(inMemoryDiagram.SchematicInMemoryFeatures, IEnumSchematicInMemoryFeature)
		enumSchemElements.Reset()

		schemElement = enumSchemElements.Next()
		While (schemElement IsNot Nothing)
			schemObjectType = schemElement.SchematicElementClass

			' get Attributes
			schemAttributeContainer = CType(schemElement.SchematicElementClass, ISchematicAttributeContainer)
			schemAttributeCID = schemAttributeContainer.GetSchematicAttribute(AttClassNameName)
			schemAttributeOID = schemAttributeContainer.GetSchematicAttribute(AttObjectIdName)

			If (schemAttributeCID IsNot Nothing) AndAlso (schemAttributeOID IsNot Nothing) Then
				' get value of attribute
				nameRelatedCN = schemAttributeCID.GetValue(schemElement)
				numRelatedOID = schemAttributeOID.GetValue(schemElement)

				' get feature from geodatabase
				esriFeatureClass = FindFeatureClassByName(esriWorkspace, nameRelatedCN, colFCByName)
				If esriFeatureClass IsNot Nothing Then
					' get feature from FeatureClass
					esriFeature = esriFeatureClass.GetFeature(numRelatedOID)
					If esriFeature IsNot Nothing Then
						' Associate geographical feature with schematic feature
						schemFeatureLinkerEdit.Associate(schemElement, esriFeature)
					End If
				End If
			End If
			schemElement = enumSchemElements.Next()
		End While
		colFCByName.Clear()
		colFCByName = Nothing
	End Sub

	Public Sub OnAfterRefreshDiagram(ByVal InMemoryDiagram As ESRI.ArcGIS.Schematic.ISchematicInMemoryDiagram)
		OnAfterLoadDiagram(InMemoryDiagram)
	End Sub

	Private Function FindFeatureClassByName(ByVal workspace As IWorkspace, ByVal name As String, ByVal colFCByName As Collection) As IFeatureClass
		Dim esriFeatureClass As IFeatureClass

		Try
			' try to retrieve FeatureClass in collection
			esriFeatureClass = CType(colFCByName(name), IFeatureClass)
			Return esriFeatureClass
		Catch
		End Try

		Dim enumDatasets As IEnumDataset
		Dim esriDataset As IDataset
		Dim featContainer As IFeatureClassContainer

		' get datasets
		enumDatasets = workspace.Datasets(esriDatasetType.esriDTFeatureDataset)
		enumDatasets.Reset()
		esriDataset = enumDatasets.Next()
		While esriDataset IsNot Nothing
			' try to find class in dataset
			Try
				featContainer = CType(esriDataset, IFeatureClassContainer)
				' get FeatureClass from current dataset
				esriFeatureClass = featContainer.ClassByName(name)
				If esriFeatureClass IsNot Nothing Then
					' if exists add to collection and quit
					colFCByName.Add(esriFeatureClass, name)
					Return esriFeatureClass
				End If
			Catch
			End Try
			' try another dataset
			esriDataset = enumDatasets.Next()
		End While

		Try
			' try to find FeatureClass in workspace
			featContainer = CType(workspace, IFeatureClassContainer)
			esriFeatureClass = featContainer.ClassByName(name)
			If esriFeatureClass IsNot Nothing Then
				' if exists add to collection and quit
				colFCByName.Add(esriFeatureClass, name)
				Return esriFeatureClass
			End If
		Catch
		End Try
		Return Nothing
	End Function
End Class
