##Implementing schematic containers around schematic features

###Purpose  
This sample demonstrates the implementation of schematic containers. It uses the AfterLoadDiagram procedure to define relations between schematic features contained in a diagram. The schematic diagrams contains Stations and Containers features. For the Stations schematic feature class, the RelatedFeeder schematic attribute has been created using the Schematic Dataset Editor. This attribute is used in the AfterLoadDiagram procedure to identify the schematic container related to each station contained in the diagram. This sample shows how schematic relations and containers can be managed by custom code; however, schematic containers can also be configured in schematic diagrams without developing custom code when configuring the PEN and PTN predefined schematic attributes.  


###Usage
1. Start Visual Studio, open the solution, and build the project.  
1. Navigate to the ContainerSample.gdb sample geodatabase, located by default at <ArcGIS DeveloperKit install location>\Samples\data\Schematics and copy this geodatabase in a folder for which you have full rights.  
1. Start ArcMap and open a new empty map.  
1. Click Customize and click Extensions. The Extensions dialog box opens.  
1. Confirm that neither the Implementation of Schematic Containers (VBNet) nor Implementation of Schematic Containers (C#) extension is checked, and close the Extensions dialog box.  
1. Click Open Schematic Diagrams on the Schematic toolbar. The Select Schematic Diagrams To Open dialog box opens.   
1. Browse and select the All schematic diagram contained in the ContainerSample_Schematic schematic dataset in the ContainerSample.gdb you copied at step#2.  
1. Start an edit session on the All schematic diagram.  
1. Right-click the Containers feature layer entry in the TOC under the All schematic diagram layer, click Selection, then click Select All. Three schematic features containers are selected in the diagram.  
1. Click Selection on the ArcMap menu and click Clear Selected Features.  
1. Close the edit session on the All schematic diagram. If you are prompted to save, click No.  
1. Click Customize and click Extensions. The Extensions dialog box opens.  
1. Check the Implementation of Schematic Containers (VBNet) or Implementation of Schematic Containers (C#) extension (depending on the language you're using) and close the Extensions dialog box.  
1. Start an edit session on the All schematic diagram. The Containers schematic features now display around the Stations schematic features to which they are related.  
1. Stop the edit session. If you are prompted to save, click No.  







####See Also  
[ISchematicContainerManager interface](http://desktop.arcgis.com/search/?q=ISchematicContainerManager%20interface&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[ISchematicRelationManager interface](http://desktop.arcgis.com/search/?q=ISchematicRelationManager%20interface&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  


---------------------------------

####Licensing  
| Development licensing | Deployment licensing | 
| :------------- | :------------- | 
| ArcGIS for Desktop Basic: Schematics | ArcGIS for Desktop Basic: Schematics |  
| ArcGIS for Desktop Standard: Schematics | ArcGIS for Desktop Standard: Schematics |  
| ArcGIS for Desktop Advanced: Schematics | ArcGIS for Desktop Advanced: Schematics |  


