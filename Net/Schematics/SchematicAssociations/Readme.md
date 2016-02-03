##Implementing associations between GIS features and schematic features

###Purpose  
This sample shows how to implement associations between geographic information system (GIS) features displayed on a map and schematic features contained in a schematic diagram. Two particular attributes, RelatedFeatureCN and RelatedFeatureOID, are used on the AfterLoadDiagram schematic event to automatically create the associations when an edit session is started on a diagram.  These attributes can be created on schematic feature classes associated with the diagram template that implements these diagrams; they must return the feature class name and ObjectID that allow you to identify the GIS feature you want associated with a schematic feature. This sample is developed as an add-in ArcMap extension.  


###Usage
####Building the component  
1. Open the solution file and build the project. This automatically registers the component.  

####Configuring the RelatedFeatureCN and RelatedFeatureOID attributes on the schematic feature class  
1. Start ArcCatalog.  
1. Browse to a geodatabase that contains a schematic dataset.  
1. Right-click the schematic dataset entry and click Edit. The Schematic Dataset Editor starts.  
1. In the Dataset Editor tree window, right-click the schematic feature class entry for which you want to define an association and click New Attribute.  
1. Type RelatedFeatureCN as the attribute name.  
1. Configure the schematic attribute parameter so it returns the name of the GIS feature class that contains the desired associated GIS features.  
1. Check On Start Editing in the Evaluation mode section.  
1. Click OK. The RelatedFeatureCN attribute creation is complete.  
1. Right-click the schematic feature class entry and click New Attribute.  
1. Type RelatedFeatureOID for the attribute name.  
1. Configure the schematic attribute parameter so it returns the ObjectID of the GIS feature.  
1. Check On Start Editing in the Evaluation mode section.  
1. Click OK. The RelatedFeatureOID attribute creation is complete.  
1. Click Save on the Schematic Dataset Editor toolbar and close the Schematic Dataset Editor.  

####Testing the newly specified associations  
1. Start ArcMap with a new empty map.  
1. Enable the Schematic Features Associations with Geographic Features (VB .NET) or Schematic Features Associations with Geographic Features (C#) extension using the Customize>Extensions menu.  
1. Add the GIS features in the active data frame.  
1. Open a diagram containing schematic features for which new associations have been configured, or generate a diagram containing such schematic features.  
1. Start an edit session on the schematic diagram.  
1. Click the Identify tool and click any schematic feature that should be impacted by this configuration change. In the Identify window, under the schematic feature branch, a new branch shows the newly associated GIS feature.  
1. Select any schematic feature that should be impacted by this configuration change and click Propagate Schematic Selection To Map. The associated GIS feature is selected on the map.  







####See Also  
[ISchematicInMemoryFeatureLinkerEdit interface](http://desktop.arcgis.com/search/?q=ISchematicInMemoryFeatureLinkerEdit%20interface&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[AfterLoadDiagram Schematic event](http://desktop.arcgis.com/search/?q=AfterLoadDiagram%20Schematic%20event&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[ISchematicInMemoryDiagram interface](http://desktop.arcgis.com/search/?q=ISchematicInMemoryDiagram%20interface&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[ISchematicInMemoryFeature interface](http://desktop.arcgis.com/search/?q=ISchematicInMemoryFeature%20interface&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  


---------------------------------

####Licensing  
| Development licensing | Deployment licensing | 
| :------------- | :------------- | 
| ArcGIS for Desktop Basic: Schematics | ArcGIS for Desktop Basic: Schematics |  
| ArcGIS for Desktop Advanced: Schematics | ArcGIS for Desktop Standard: Schematics |  
| ArcGIS for Desktop Standard: Schematics | ArcGIS for Desktop Advanced: Schematics |  


