using System;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.GeoDatabaseDistributed;

namespace RasterSyncExtension
{
	/// <summary>
	/// An example of a workspace extension that extends the synchronization process. This
	/// implementation synchronizes a specific raster catalog in addition to the out-of-the-box
	/// synchronization process.
	/// </summary>
	[Guid("97CD2883-37CB-4f76-BD0F-945279C783DC")]
	[ClassInterface(ClassInterfaceType.None)]
	[ProgId("RasterSyncExtension.RasterSyncWorkspaceExtension")]
	[ComVisible(true)]
	public class RasterSyncWorkspaceExtension : IWorkspaceExtensionControl, IWorkspaceExtension2, IWorkspaceReplicaSyncEvents
	{
		#region Private Variables
		/// <summary>
		/// Provides a weak reference to the extension's workspace.
		/// </summary>
		private IWorkspaceHelper workspaceHelper = null;

		/// <summary>
		/// The name of the raster catalog to synchronize.
		/// </summary>
		private const String rasterCatalogName = "ras_cat";

		/// <summary>
		/// The name of the replica that requires raster synchronization.
		/// </summary>
		private const String rasterReplicaName = "myreplicaras";

		/// <summary>
		/// The name of the integer field in the raster catalogs that stores generation numbers.
		/// </summary>
		private const String genFieldName = "gen";
		#endregion

		#region IWorkspaceExtensionControl Members
		/// <summary>
		/// Initializes the workspace extension.
		/// </summary>
		/// <param name="workspaceHelper">Provides a weak reference to the workspace.</param>
		public void Init(IWorkspaceHelper workspaceHelper)
		{
			this.workspaceHelper = workspaceHelper;
		}

		/// <summary>
		/// Called to shutdown the extension.
		/// </summary>
		public void Shutdown()
		{
			workspaceHelper = null;
		}
		#endregion

		#region IWorkspaceExtension2 Members
		/// <summary>
		/// The name of the extension.
		/// </summary>
		public string Name
		{
			get { return "RasterSyncWorkspaceExtension"; }
		}

		/// <summary>
		/// The extension's GUID.
		/// </summary>
		public UID GUID
		{
			get
			{
				UID uid = new UIDClass();
				uid.Value = "{97CD2883-37CB-4f76-BD0F-945279C783DC}";
				return uid;
			}
		}

		/// <summary>
		/// An enumerator of private dataset names used by the extension.
		/// Not used in this implementation.
		/// </summary>
		/// <param name="datasetType">The dataset type.</param>
		/// <returns>An enumerator of strings.</returns>
		public IEnumBSTR get_PrivateDatasetNames(esriDatasetType datasetType)
		{
			return null;
		}

		/// <summary>
		/// An enumerator of data dictionary names used by the extension.
		/// Not used in this implementation.
		/// </summary>
		public IEnumBSTR DataDictionaryTableNames
		{
			get { return null; }
		}

		/// <summary>
		/// Indicates whether the extension owns a dataset type.
		/// </summary>
		/// <param name="datasetType">The type of dataset to check.</param>
		/// <returns>False; this extension owns no dataset types.</returns>
		public Boolean OwnsDatasetType(esriDatasetType datasetType)
		{
			return false;
		}

		/// <summary>
		/// Returns a reference to the extension's workspace.
		/// </summary>
		public IWorkspace Workspace
		{
			get { return workspaceHelper.Workspace; }
		}
		#endregion

		#region IWorkspaceReplicaSyncEvents Members
		/// <summary>
		/// Occurs in the replica geodatabase after data changes have been exported
		/// from that replica geodatabase to a delta database.
		/// Not used in this implementation.
		/// </summary>
		public void AfterExportingDataChanges(IReplica sourceReplica, object dataChangesSource, object deltaFile)
		{
			// Not used in this implementation.
		}

		/// <summary>
		/// Occurs in the master geodatabase after data changes in either a replica
		/// geodatabase or delta database are transferred to the master geodatabase.
		/// </summary>
		/// <param name="targetReplica">The target replica.</param>
		/// <param name="dataChangesSource">A collection of changes made to the master geodatabase.</param>
		/// <param name="oidMappingTable">Not used in this implementation.</param>
		/// <param name="changesTable">Not used in this implemented.</param>
		public void AfterSynchronizingDataChanges(IReplica targetReplica, object dataChangesSource, ITable oidMappingTable, ITable changesTable)
		{
			// Make sure that the correct replica is being synchronized.
			String replicaName = targetReplica.Name;
			String unqualifiedReplicaName = replicaName.Substring(replicaName.LastIndexOf('.') + 1);
			if (!unqualifiedReplicaName.Equals(rasterReplicaName))
			{
				return;
			}

			// Get the rasters to pull if connected synchronization is occurring.
			IDataChanges3 dataChanges3 = dataChangesSource as IDataChanges3;
			if (dataChanges3 != null)
			{
				// Get the source's replicas.
				IName sourceWorkspaceName = (IName)dataChanges3.ParentWorkspaceName;
				IWorkspace sourceWorkspace = (IWorkspace)sourceWorkspaceName.Open();
				IWorkspaceReplicas sourceWorkspaceReplicas = (IWorkspaceReplicas)sourceWorkspace;

				// Get the replica generation numbers.
				int genBegin = 0;
				int genEnd = 0;
				int targetGen = 0;
				dataChanges3.GenerationNumbers(out genBegin, out genEnd, out targetGen);
				IQueryFilter queryFilter = new QueryFilterClass();
				queryFilter.WhereClause = String.Format("{0} > {1} or {0} is NULL", genFieldName, genBegin);

				// Open a cursor to get the rasters to copy form the source.
				IRasterWorkspaceEx sourceRasterWorkspaceEx = (IRasterWorkspaceEx)sourceWorkspace;
				IRasterCatalog sourceRasterCatalog = sourceRasterWorkspaceEx.OpenRasterCatalog(rasterCatalogName);
				IFeatureClass sourceFeatureClass = (IFeatureClass)sourceRasterCatalog;
				int sourceGenFieldIndex = sourceFeatureClass.FindField(genFieldName);
				IFeatureCursor sourceCursor = sourceFeatureClass.Search(queryFilter, true);

				// Open the target raster catalog.
				IRasterWorkspaceEx targetRasterWorkspaceEx = (IRasterWorkspaceEx)workspaceHelper.Workspace;
				IRasterCatalog targetRasterCatalog = targetRasterWorkspaceEx.OpenRasterCatalog(rasterCatalogName);
				IFeatureClass targetFeatureClass = (IFeatureClass)targetRasterCatalog;
				int targetGenFieldIndex = targetFeatureClass.FindField(genFieldName);
				IFeatureCursor targetCursor = targetFeatureClass.Insert(true);

				// Copy the rasters from the source to the target.
				IFeature sourceFeature = null;
				while ((sourceFeature = sourceCursor.NextFeature()) != null)
				{
					// Copy the raster and set the target gen to -1 (received).
					IFeatureBuffer featureBuffer = targetFeatureClass.CreateFeatureBuffer();
					featureBuffer.set_Value(targetRasterCatalog.RasterFieldIndex, sourceFeature.get_Value(sourceRasterCatalog.RasterFieldIndex));
					featureBuffer.set_Value(targetGenFieldIndex, -1);
					targetCursor.InsertFeature(featureBuffer);

					// Set the source row value to the current generation.
					if (sourceFeature.get_Value(sourceGenFieldIndex) == DBNull.Value)
					{
						sourceFeature.set_Value(sourceGenFieldIndex, genEnd);
					}
					sourceFeature.Store();
				}
				Marshal.FinalReleaseComObject(sourceCursor);
				Marshal.FinalReleaseComObject(targetCursor);
			}
		}

		/// <summary>
		/// Occurs in the replica geodatabase before data changes are exported from that replica geodatabase to a delta database.
		/// Not used in this implementation.
		/// </summary>
		public void BeforeExportingDataChanges(IReplica sourceReplica, object dataChangesSource, object deltaFile)
		{
			// Not used in this implementation.
		}

		/// <summary>
		/// Occurs in the master geodatabase before data changes in either a replica geodatabase or delta
		/// database are transferred to the master geodatabase.
		/// Not used in this implementation.
		/// </summary>
		public void BeforeSynchronizingDataChanges(IReplica targetReplica, object dataChangesSource)
		{
			// Not used in this implementation.
		}
		#endregion
	}
}