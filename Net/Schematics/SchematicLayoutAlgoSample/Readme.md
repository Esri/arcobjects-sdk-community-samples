##Implementing a schematic layout algorithm and its layout property page

###Purpose  
This sample shows a customized schematic layout algorithm implementation. Schematics handles the customized algorithm in the same way as a standard algorithm. The customized implementation of ISchematicAlgorithm can be added to the Layout Task drop-down list and applied from the Apply Layout button in the same way as any standard algorithm. Implementing a new IPropertyPage provides access to your custom layout algorithm's parameters.  Caution: If you plan to implement a customized schematic layout algorithm to be executed via a client-server application, the solution must implement two projects; one for the custom algorithm property page and the other for the custom algorithm execution itself. The component dedicated to the custom algorithm execution will have to be registered with the ArcGIS server to be used on any client-server application. This sample creates a schematic layout algorithm (named TranslateTree) that translates a set of connected schematic features starting from a selected node according to given X and Y translation factors. The Apply Layout Task button is available for the algorithm only when a single node is currently selected in the active diagram.  


###Usage
1. Open the solution file in Visual Studio and build the project.  
1. Start ArcMap.  
1. Open a schematic diagram and start an edit session on the diagram.  
1. Click the Edit/Move Schematic Features tool on the Schematic Editor toolbar and select an only schematic node in the active diagram.  
1. Select Translate Tree in the language you're using (C# or VB .NET) from the Layout Task drop-down list.  
1. Click Layout Algorithm Properties to edit and modify the Translate Tree properties (X  and Y translation factors) if needed.  
1. Click OK.  
1. Click Apply Layout Task.  







####See Also  
[ISchematicAlgorithm interface](http://desktop.arcgis.com/search/?q=ISchematicAlgorithm%20interface&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[ISchematicAlgorithmEventsTrigger interface](http://desktop.arcgis.com/search/?q=ISchematicAlgorithmEventsTrigger%20interface&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[ISchematicAnalystFindConnected interface](http://desktop.arcgis.com/search/?q=ISchematicAnalystFindConnected%20interface&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[ISchematicInMemoryDiagram interface](http://desktop.arcgis.com/search/?q=ISchematicInMemoryDiagram%20interface&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[ISchematicInMemoryFeature interface](http://desktop.arcgis.com/search/?q=ISchematicInMemoryFeature%20interface&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  


---------------------------------

####Licensing  
| Development licensing | Deployment licensing | 
| :------------- | :------------- | 
| ArcGIS for Desktop Standard: Schematics | ArcGIS for Desktop Basic: Schematics |  
| ArcGIS for Desktop Basic: Schematics | ArcGIS for Desktop Standard: Schematics |  
| ArcGIS for Desktop Advanced: Schematics | ArcGIS for Desktop Advanced: Schematics |  


