##Implementing the ISchematicRulesHelper to easily develop a custom schematic rule

###Purpose  
Beginning with ArcGIS 10.2, the Schematics Extension provides a Schematics Rules Helper to aid in the implementation of custom rules. Before this helper, a developer had to have a good knowledge of the internal mechanisms and constraints of schematics such as the following:How to report the associationsHow to report relationsWhen to update the current geometryHow to update or create schematic features, and so onThis new helper facilitates the implementation of custom Schematic Rules by exposing a public application programming interface (API) that processes these crucial tasks for the developer. Caution: If you plan to implement a custom schematic rule to be executed via a client-server application, the solution must implement two projects; one for the custom rule property page and the other for the custom rule execution itself. The component dedicated to the custom rule execution will have to be registered with the ArcGIS server to be used on any client-server application. This sample demonstrates how to use the rules helper to develop a rule. The rule will find nodes of a certain schematic feature class that have only two links connected. For each of those nodes that are found, the rule will create a new node that is a certain distance away and then connect the two nodes with a new link. The placement angle of the new node and link will be based on the bisector (middle) of the existing two links.  


###Usage
1. Open the solution file and build the project. Note that this step automatically registers the components.  
1. Start ArcCatalog.  
1. Browse to any geodatabase that contains a schematic dataset.  
1. Right-click this schematic dataset entry and select Edit. The Schematic Dataset Editor starts.  
1. Select any diagram template entry that works with the Standard builder or create a diagram template that works with the Standard builder.  
1. Click the Rules tab.  
1. Click Add Rule on the Rules tab toolbar.  
1. Select the new custom rule from the Type drop-down list (that is, select Bisector Rule C# or Bisector Rule VBNet).  
1. Click Rule Properties on the Rules tab toolbar. The custom rule properties page appears.  
1. Select the node schematic feature class that implements the nodes connecting to two schematic links for which a bisector link will be created in the Select the parent node schematic feature class drop-down list.  
1. Select the node schematic feature class that will be used to implement the new end schematic nodes for each bisector in the Select the target node schematic feature class drop-down list.  
1. Select the link schematic feature class that will be used to implement the schematic bisector link in the Select to target link schematic feature class drop-down list.  
1. Add the distance value used to initialize the length of the newly created bisector link (that is, the distance between the initial node and the newly created end node of the bisector).  
1. Click Save to save the schematic dataset's new parameters.  
1. Start ArcMap. Generate a diagram containing nodes based on the node schematic feature class specified in the Select the parent node schematic feature class drop-down list; those nodes being connected to only two nodes. The custom rule is executed during the diagram generation and automatically creates a bisector link for each of these nodes.  







####See Also  
[ISchematicRulesHelper interface](http://desktop.arcgis.com/search/?q=ISchematicRulesHelper%20interface&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[ISchematicRule interface](http://desktop.arcgis.com/search/?q=ISchematicRule%20interface&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[ISchematicRuleDesign interface](http://desktop.arcgis.com/search/?q=ISchematicRuleDesign%20interface&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  


---------------------------------

####Licensing  
| Development licensing | Deployment licensing | 
| :------------- | :------------- | 
| ArcGIS for Desktop Basic: Schematics | ArcGIS for Desktop Basic: Schematics |  
| ArcGIS for Desktop Standard: Schematics | ArcGIS for Desktop Standard: Schematics |  
| ArcGIS for Desktop Advanced: Schematics | ArcGIS for Desktop Advanced: Schematics |  


