## Customizing schematic feature removal events

  <div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">This sample shows how to customize the BeforeRemoveFeature predefined schematic event used to manage the removal of schematic features contained in schematic diagrams. By default, when the Remove Schematic Features command is clicked, schematic features are graphically removed without being removed from the database.</div>
  <div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53"> </div>
  <div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">In this sample, the BeforeRemoveFeature predefined event is customized so the related record in the database is definitively removed.</div>  


<!-- TODO: Fill this section below with metadata about this sample-->
```
Language:              C#, VB
Subject:               Schematics
Organization:          Esri, http://www.esri.com
Date:                  11/17/2017
ArcObjects SDK:        10.6
Visual Studio:         2015, 2017
.NET Target Framework: 4.5
```

### Resources

* [ArcObjects .NET API Reference online](http://desktop.arcgis.com/en/arcobjects/latest/net/webframe.htm)  
* [Sample Data Download](../../releases)  
* [What's new](http://desktop.arcgis.com/en/arcobjects/latest/net/webframe.htm#91cabc68-2271-400a-8ff9-c7fb25108546.htm)  
* [Download the ArcObjects SDK for .Net from MyEsri.com](https://my.esri.com/)  

### Usage
1. Start Visual Studio, open the solution file, and build the project.  
1. Navigate to <ArcGIS DeveloperKit install location>\Samples\data\Schematics, and copy the SampleRemoval.gdb geodatabase in a folder for which you have full rights.  
1. Start ArcMap and open a new empty map.  
1. Click Customize and click Extension.  
1. Confirm that neither the Schematic Features Removal Events (C#) nor the Schematic Feature Removal Events (VBNet) extension is checked and close the Extensions dialog box.  
1. Open the Substation 08 schematic diagram contained in the SampleRemoval_Schematic schematic dataset in the SampleRemoval.gdb geodatabase you copied at step#2.  
1. Start an edit session on this schematic diagram.  
1. Select a set of schematic features in this diagram.  
1. Click Remove Schematic Features. The selected schematic features are graphically removed, but they are kept in the schematic feature classes.  
1. Close the edit session on the Substation 08 schematic diagram, and click Yes on the prompt message for saving.  
1. Click Update Diagram. The Update Diagram dialog box opens.  
1. Check the Synchronize against original selection/trace/query option and uncheck the Persist manually removed, reduced, or reconnected features option.  
1. Click OK. The diagram is updated and the schematic features removed in step 8 are restored and reappear in the diagram after updating.   
1. Click Customize and click Extension.  
1. Check the Schematic Features Removal Events (C#) or Schematic Feature Removal Events (VBNet) extension option and close the Extensions dialog box.  
1. Start an edit session on the Substation 08 schematic diagram.  
1. Select a set of schematic features in this diagram.  
1. Click Remove Schematic Features. The selected schematic features are graphically removed, but the associated records on the Inside_Nodes or Inside_Links tables from which they were queried (upon diagram generation) are also definitively removed. Updating the diagram will never restore the removed schematic features in the diagram regardless of the Persist manually removed, reduced, or reconnected features option that is used. They are no longer queried from the Inside_Nodes or Inside_Links tables because the related records have been removed.  







#### See Also  
[BeforeRemoveFeature Schematic event](http://desktop.arcgis.com/search/?q=BeforeRemoveFeature%20Schematic%20event&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  


---------------------------------

#### Licensing  
| Development licensing | Deployment licensing | 
| ------------- | ------------- | 
| ArcGIS Desktop Basic: Schematics | ArcGIS Desktop Basic: Schematics |  
| ArcGIS Desktop Standard: Schematics | ArcGIS Desktop Standard: Schematics |  
| ArcGIS Desktop Advanced: Schematics | ArcGIS Desktop Advanced: Schematics |  


