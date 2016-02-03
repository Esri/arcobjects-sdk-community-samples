##Extending the replication synchronization process

###Purpose  
Developers might want to extend the core replica behavior during synchronization. The synchronization process can be extended by implementing a workspace extension that supports IWorkspaceReplicaSyncEvents. By implementing this interface in an extension, custom behavior can be executed before and after any replica synchronization process executed on a geodatabase.This sample describes the process using the example of a developer wanting to copy new raster datasets from a raster catalog during the synchronization. In this example, when synchronization is executed, the extension identifies and copies the new raster datasets from the source geodatabase to the target geodatabase.This sample is for connected synchronization where connections are made to local geodatabases.This sample can be used as a reference for developers needing to extend synchronization for any reason.  


###Usage
####Preparing the data  
1. This sample requires that the raster catalog has a long integer column named "gen." If the replica has been created and has that column, proceed with Step 7 in this section.   
1. Open the parent raster catalog to be synchronized in ArcMap.  
1. Add the gen column to the raster catalog using the Add Field option in the attribute table.  
1. Set the value of the gen column for any existing raster datasets in the raster catalog to 0. The field calculator can be used to do this quickly.  
1. Repeat Steps 2-4 for the child raster catalog.  
1. Create a replica of the raster catalog. For more information, see "How to create a replica in a connected environment" in the See Also section in this topic. The creation process copies the raster catalog and the raster datasets that intersect the replica geometry by default. To create the replica, at least one versioned feature class with edit permission must also be included in the replica. It is not possible to create a replica consisting only of the raster dataset.  
1. Once the replica exists, make sure that the raster catalog exists in the parent and the child replica geodatabases, and that each has a long integer column named gen.  
1. If there are common raster datasets, set the value in the gen column to 0 for the common datasets in the parent and child replica geodatabases.  

####Building the extension  
1. Once the data is prepared, build the extension and register it with the replica geodatabases by completing the following steps.  
1. Copy the solution locally and open the solution.  
1. Open the RasterSyncWorkspaceExtension.cs (if working in C#) or RasterSyncWorkspaceExtension.vb (if working in VB .NET) source file.  
1. There are two member variables in the extension that can be adjusted. The rasterCatalog variable (a private constant string, currently set to "ras_cat") is the name of the raster catalog to involve in the synchronization. By changing this value before building the assembly, the synchronized raster catalog can be changed.  
1. The other variable is the replica variable (a private constant string, currently set to "myreplicaras"), which is the name of the replica whose synchronization event triggers this behavior. By changing this value before building the assembly, the replica with extended behavior can be changed.  
1. Right-click the project and choose build. This creates the assembly in the project's debug or release directory (depending on the selected option).  

####Applying the extension to one or more geodatabases  
1. Which geodatabase or geodatabases require the workspace extension depends on the type of replica being used. For one-way parent-to-child replication, both geodatabases involved with the replica requires the extension. For two-way replication, both geodatabases involved with the replica requires the extension. The RasterSynchExtensionReg* project compiles to an executable that registers the workspace extension with a geodatabase.  
1. Right-click the RasterSyncExtensionReg* project and choose Set as StartUp Project.  
1. Open RegisterExtension.cs.  
1. Change the RegisterExtension.workspacePath value to the path of an ArcSDE connection file or local geodatabase that has the extension applied to it.  
1. Change the RegisterExtension.geodatabaseType value to reflect the type of geodatabase on which the extension is being registered.  
1. Right-click the project and choose build.  
1. Press F5 to run the project.  
1. Steps 2-7 in this section registers the extension with one of the replica geodatabases. If two-way replication is being used, repeat these steps for the other replica geodatabase. Once this is completed, synchronizing the replica identified in the previous steps executes the extension.  

####Using the extension  
1. Synchronizing the replica associated with the extension using the wizards, or the geoprocessing tools in ArcCatalog or ArcMap, triggers the extension's behavior. To test this, assuming that a two-way replica is being used, complete the following steps.  
1. Load one or more new raster datasets into the raster catalog in the parent geodatabase. When this is done, the gen column has a null value for the new raster datasets.  
1. In the same way, load one or more new raster datasets into the raster catalog in the child.  
1. In ArcCatalog, connect to the parent replica geodatabase and synchronize from the parent to the child. Notice that a value is assigned to the gen column of the new raster datasets. This is the generation number of the synchronization in which the raster datasets were sent.  
1. In ArcCatalog, connect to the child replica geodatabase and preview the raster catalog. Notice that the raster datasets from the parent have been applied and have a gen value of –1. The –1 value indicates that these have been received from the parent.  
1. In ArcCatalog, synchronize from the child to the parent. Notice that the raster datasets that were added to the child in Step 3 were applied to the parent and the values in the gen column for these datasets were adjusted.   
1. As raster datasets are added and synchronized, the parent and child are kept up to date with the new raster datasets. The extension also supports cases where (for any reason) synchronization fails. Here, the generation information in the gen column is used to determine if rasters need to be resent.  





####Additional information  
<div style="FONT-WEIGHT: normal" xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">The main goal of the sample is to show how to extend replica synchronization. There are a number of enhancements that can be added to support your specific requirements. Some of these include the following:</div>  


####See Also  
[How to create a replica in a connected environment](http://desktop.arcgis.com/search/?q=How%20to%20create%20a%20replica%20in%20a%20connected%20environment&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  


---------------------------------

####Licensing  
| Development licensing | Deployment licensing | 
| :------------- | :------------- | 
| ArcGIS for Desktop Standard | ArcGIS for Desktop Standard |  
| ArcGIS for Desktop Advanced | ArcGIS for Desktop Advanced |  
| Engine Developer Kit | Engine: Geodatabase Update |  


