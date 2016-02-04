'Copyright 2016 Esri

'Licensed under the Apache License, Version 2.0 (the "License");
'you may not use this file except in compliance with the License.
'You may obtain a copy of the License at

'    http://www.apache.org/licenses/LICENSE-2.0

'Unless required by applicable law or agreed to in writing, software
'distributed under the License is distributed on an "AS IS" BASIS,
'WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.

'See the License for the specific language governing permissions and
'limitations under the License.
Imports System.Runtime.InteropServices

<Guid("A9FC5EB2-33F9-4941-AE69-775ECF688647"), _
 ClassInterface(ClassInterfaceType.None), _
 ProgId("RasterSyncExtensionVB.RasterSyncWorkspaceExtension")> _
Public Class RasterSyncWorkspaceExtension
	Implements IWorkspaceExtensionControl
	Implements IWorkspaceExtension2
	Implements IWorkspaceReplicaSyncEvents

#Region "Private Variables"
	''' <summary>
	''' Provides a weak reference to the extension's workspace.
	''' </summary>
	Private workspaceHelper As IWorkspaceHelper = Nothing

	''' <summary>
	''' The name of the raster catalog to synchronize.
	''' </summary>
	Private Const rasterCatalogName As String = "ras_cat"

	''' <summary>
	''' The name of the replica that requires raster synchronization.
	''' </summary>
	Private Const rasterReplicaName As String = "myreplicaras"

	''' <summary>
	''' The name of the integer field in the raster catalogs that stores generation numbers.
	''' </summary>
	Private Const genFieldName As String = "gen"
#End Region

#Region "IWorkspaceExtensionControl Members"
	''' <summary>
	''' Initializes the workspace extension.
	''' </summary>
	''' <param name="workspaceHelper">Provides a weak reference to the workspace.</param>
	Public Sub Init(ByVal workspaceHelper As IWorkspaceHelper) Implements IWorkspaceExtensionControl.Init
		Me.workspaceHelper = workspaceHelper
	End Sub

	''' <summary>
	''' Called to shutdown the extension.
	''' </summary>
	Public Sub Shutdown() Implements IWorkspaceExtensionControl.Shutdown
		workspaceHelper = Nothing
	End Sub
#End Region

#Region "IWorkspaceExtension[2] Members"
	''' <summary>
	''' The name of the extension.
	''' </summary>
	Public ReadOnly Property Name() As String _
	Implements IWorkspaceExtension.Name, IWorkspaceExtension2.Name
		Get
			Return "RasterSyncWorkspaceExtension"
		End Get
	End Property

	''' <summary>
	''' The extension's GUID.
	''' </summary>
	Public ReadOnly Property GUID() As UID _
	Implements IWorkspaceExtension.GUID, IWorkspaceExtension2.GUID
		Get
			Dim uid As UID = New UIDClass()
			uid.Value = "{A9FC5EB2-33F9-4941-AE69-775ECF688647}"
			Return uid
		End Get
	End Property

	''' <summary>
	''' An enumerator of private dataset names used by the extension.
	''' Not used in this implementation.
	''' </summary>
	''' <param name="datasetType">The dataset type.</param>
	''' <returns>An enumerator of strings.</returns>
	Public ReadOnly Property PrivateDatasetNames(ByVal datasetType As esriDatasetType) As IEnumBSTR _
	Implements IWorkspaceExtension.PrivateDatasetNames, IWorkspaceExtension2.PrivateDatasetNames
		Get
			Return Nothing
		End Get
	End Property

	''' <summary>
	''' An enumerator of data dictionary names used by the extension.
	''' Not used in this implementation.
	''' </summary>
	Public ReadOnly Property DataDictionaryTableNames() As IEnumBSTR _
	Implements IWorkspaceExtension.DataDictionaryTableNames, IWorkspaceExtension2.DataDictionaryTableNames
		Get
			Return Nothing
		End Get
	End Property

	''' <summary>
	''' Indicates whether the extension owns a dataset type.
	''' </summary>
	''' <param name="datasetType">The type of dataset to check.</param>
	''' <returns>False; this extension owns no dataset types.</returns>
	Public Function OwnsDatasetType(ByVal datasetType As esriDatasetType) As Boolean _
	Implements IWorkspaceExtension2.OwnsDatasetType
		Return False
	End Function

	''' <summary>
	''' Returns a reference to the extension's workspace.
	''' </summary>
	Public ReadOnly Property Workspace() As IWorkspace _
	Implements IWorkspaceExtension2.Workspace
		Get
			Return workspaceHelper.Workspace
		End Get
	End Property
#End Region

#Region "IWorkspaceReplicaSyncEvents Members"
	''' <summary>
	''' Occurs in the replica geodatabase after data changes have been exported
	''' from that replica geodatabase to a delta database.
	''' Not used in this implementation.
	''' </summary>
	Public Sub AfterExportingDataChanges(ByVal sourceReplica As IReplica, ByVal dataChangesSource As Object, ByVal deltaFile As Object) _
	Implements IWorkspaceReplicaSyncEvents.AfterExportingDataChanges
		' Not used in this implementation.
	End Sub

	''' <summary>
	''' Occurs in the master geodatabase after data changes in either a replica
	''' geodatabase or delta database are transferred to the master geodatabase.
	''' </summary>
	''' <param name="targetReplica">The target replica.</param>
	''' <param name="dataChangesSource">A collection of changes made to the master geodatabase.</param>
	''' <param name="oidMappingTable">Not used in this implementation.</param>
	''' <param name="changesTable">Not used in this implemented.</param>
	Public Sub AfterSynchronizingDataChanges(ByVal targetReplica As IReplica, ByVal dataChangesSource As Object, _
	ByVal oidMappingTable As ITable, ByVal changesTable As ITable) _
	Implements IWorkspaceReplicaSyncEvents.AfterSynchronizingDataChanges
		' Make sure that the correct replica is being synchronized.
		Dim replicaName As String = targetReplica.Name
		Dim unqualifiedReplicaName As String = replicaName.Substring(replicaName.LastIndexOf(".") + 1)
		If Not unqualifiedReplicaName.Equals(rasterReplicaName) Then
			Return
		End If

		' Get the rasters to pull if connected synchronization is occurring.
		Dim dataChanges3 As IDataChanges3 = TryCast(dataChangesSource, IDataChanges3)
		If Not dataChanges3 Is Nothing Then
			' Get the source's replicas.
			Dim sourceWorkspaceName As IName = CType(dataChanges3.ParentWorkspaceName, IName)
			Dim sourceWorkspace As IWorkspace = CType(sourceWorkspaceName.Open(), IWorkspace)
			Dim sourceWorkspaceReplicas As IWorkspaceReplicas = CType(sourceWorkspace, IWorkspaceReplicas)

			' Get the replica generation numbers.
			Dim genBegin As Integer = 0
			Dim genEnd As Integer = 0
			Dim targetGen As Integer = 0
			dataChanges3.GenerationNumbers(genBegin, genEnd, targetGen)
			Dim queryFilter As IQueryFilter = New QueryFilterClass()
			queryFilter.WhereClause = String.Format("{0} > {1} or {0} is NULL", genFieldName, genBegin)

			' Open a cursor to get the rasters to copy form the source.
			Dim sourceRasterWorkspaceEx As IRasterWorkspaceEx = CType(sourceWorkspace, IRasterWorkspaceEx)
			Dim sourceRasterCatalog As IRasterCatalog = sourceRasterWorkspaceEx.OpenRasterCatalog(rasterCatalogName)
			Dim sourceFeatureClass As IFeatureClass = CType(sourceRasterCatalog, IFeatureClass)
			Dim sourceGenFieldIndex As Integer = sourceFeatureClass.FindField(genFieldName)
			Dim sourceCursor As IFeatureCursor = sourceFeatureClass.Search(queryFilter, True)

			' Open the target raster catalog.
			Dim targetRasterWorkspaceEx As IRasterWorkspaceEx = CType(workspaceHelper.Workspace, IRasterWorkspaceEx)
			Dim targetRasterCatalog As IRasterCatalog = targetRasterWorkspaceEx.OpenRasterCatalog(rasterCatalogName)
			Dim targetFeatureClass As IFeatureClass = CType(targetRasterCatalog, IFeatureClass)
			Dim targetGenFieldIndex As Integer = targetFeatureClass.FindField(genFieldName)
			Dim targetCursor As IFeatureCursor = targetFeatureClass.Insert(True)

			' Copy the rasters from the source to the target.
			Dim sourceFeature As IFeature = sourceCursor.NextFeature()
			While Not sourceFeature Is Nothing
				' Copy the raster and set the target gen to -1 (received).
				Dim featureBuffer As IFeatureBuffer = targetFeatureClass.CreateFeatureBuffer()
				featureBuffer.Value(targetRasterCatalog.RasterFieldIndex) = sourceFeature.Value(sourceRasterCatalog.RasterFieldIndex)
				featureBuffer.Value(targetGenFieldIndex) = -1
				targetCursor.InsertFeature(featureBuffer)

				' Set the source row value to the current generation.
				If sourceFeature.Value(sourceGenFieldIndex).Equals(DBNull.Value) Then
					sourceFeature.Value(sourceGenFieldIndex) = genEnd
				End If
				sourceFeature.Store()
				sourceFeature = sourceCursor.NextFeature()
			End While
			Marshal.FinalReleaseComObject(sourceCursor)
			Marshal.FinalReleaseComObject(targetCursor)
		End If
	End Sub

	''' <summary>
	''' Occurs in the replica geodatabase before data changes are exported from that replica geodatabase to a delta database.
	''' Not used in this implementation.
	''' </summary>
	Public Sub BeforeExportingDataChanges(ByVal sourceReplica As IReplica, ByVal dataChangesSource As Object, ByVal deltaFile As Object) _
	 Implements IWorkspaceReplicaSyncEvents.BeforeExportingDataChanges
		' Not used in this implementation.
	End Sub

	''' <summary>
	''' Occurs in the master geodatabase before data changes in either a replica geodatabase or delta
	''' database are transferred to the master geodatabase.
	''' Not used in this implementation.
	''' </summary>
	Public Sub BeforeSynchronizingDataChanges(ByVal targetReplica As IReplica, ByVal dataChangesSource As Object) _
	Implements IWorkspaceReplicaSyncEvents.BeforeSynchronizingDataChanges
		' Not used in this implementation.
	End Sub
#End Region

End Class


