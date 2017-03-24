## Subset network evaluators

  <div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">This sample includes two custom evaluators that can be plugged into a network dataset and used by the ArcGIS Network Analyst extension. The FilterSubset evaluator can be used to restrict streets based on the current selection in ArcMap. The ScaleSubset evaluator can be used to slow down streets that fall within polygon graphics on the map. Both evaluators use parameterized attributes to pass information from ArcMap to the evaluator. </div>
  <div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53"> </div>
  <div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">Building the solution installs these two custom evaluators, adds a custom command to ArcMap, and automatically adds two context items to the ArcCatalog network dataset context menu, which is used to help create or remove network attributes that use these evaluators.</div>
  <div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53"> </div>
  <div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">While the functionality provided by this sample is more easily handled by using polygon barriers in your analysis, the purpose of the sample is to provide help getting started using custom evaluators.</div>
  <div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53"> </div>
  <div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">The same concepts can be used to create and consume these evaluators in an ArcGIS Engine environment. </div>  


<!-- TODO: Fill this section below with metadata about this sample-->
```
Language:              C#, VB
Subject:               Networks
Organization:          Esri, http://www.esri.com
Date:                  3/24/2017
ArcObjects SDK:        10.5
Visual Studio:         2013, 2015
.NET Target Framework: 4.5
```

### Resources

* [ArcObjects .NET API Reference online](http://desktop.arcgis.com/en/arcobjects/latest/net/webframe.htm)  
* [Sample Data Download](../../releases)  
* [What's new](http://desktop.arcgis.com/en/arcobjects/latest/net/webframe.htm#05247c04-bfd9-4e36-ae09-bc6e833c3b14.htm)  
* [Download the ArcObjects SDK for .Net from MyEsri.com](https://my.esri.com/)  

### Usage
#### Installation  
1. Start Visual Studio.  
1. Rebuild the SubsetNetworkEvaluators solution.  

#### Editing the network dataset to use subset network evaluators in ArcCatalog  
1. Start ArcCatalog using an ArcGIS Desktop Basic or higher license and check out an ArcGIS Network Analyst extension license to enable network dataset schema updates.  
1. Right-click the Streets_ND network dataset in the SanFrancisco.gdb/Transportation dataset in the SanFrancisco folder, and choose Add Subset Attributes to add new attributes that have parameters and are assigned the new custom evaluators (this can also be done manually by using the property page).  
1. If you decide to uninstall these sample subset evaluators, choose Remove Subset Attributes on any network dataset that has added them. Otherwise, the network dataset will not open because it cannot instantiate these custom evaluators. Alternatively, you can manually remove or edit any attributes that use these evaluators using the property page.  

#### Using the auto update EID array parameters command in ArcMap  
1. Close ArcCatalog.  
1. Start ArcMap with a new document and add the Streets_ND network dataset in the SanFrancisco.gdb/Transportation dataset in the SanFrancisco folder. When prompted, add the feature classes to use the feature selection.  
1. Ensure that an ArcGIS Network Analyst extension license is checked out and the Network Analyst toolbar is active.  
1. On the Network Analyst drop-down command, choose the New Route command.  
1. On the Analysis Settings property page of the new Route NALayer, set the ScaleSubset_Minutes attribute as the impedance attribute, then select the FilterSubset restriction.  
1. On the Attribute Parameters property page of the new Route NALayer, the parameters were automatically added. On the ScaleSubset_Minutes attribute, the ScaleSubset_Factor parameter value defaults to 1. In this case, use these polygons as slowdown polygons in areas of severe congestion. Therefore, to slowdown elements within slowdown polygons by a factor of 5, type 5 for the parameter value. Also, notice the *_eids_* parameters on the Attribute Parameters property page. These are the network parameters that are automatically updated if the AutoUpdateNetworkElementArrayParametersCommand command is toggled on. These arrays can also be manually set by typing in the list on this property page or by other programmatic means.  
1. Add two or more stops to the new route and solve.  
1. In ArcMap, click Customize > Custom Mode. On the Customize dialog box, click the Commands tab and the Network Analyst Samples category, then add the AutoUpdateNetworkElementArrayParametersCommand to the Network Analyst toolbar.  
1. Click the command to toggle on the auto update subset parameters, and toggle off to stop listening to selection change and graphic element change events.  
1. Select some street features, including some streets overlapping the previously solved route. Solve again. Since the AutoUpdateNetworkElementArrayParametersCommand is toggled on and you are using the FilterSubset restriction attribute for this NALayer, the selected features update the subset parameters to restrict the network elements corresponding to the selected features. The path should not go on the selected features, or if you selected too many streets, the path might not be traversable.  
1. Clear the selection and solve again. The path should go back to the original path.  
1. On the Drawing toolbar's Drawing drop-down command, click Default Symbol Properties and change the symbol to have a hollow or hatched fill symbol so you can see through polygon graphic elements added to ArcMap.  
1. Using one of the polygon drawing tools, such as New Polygon on the Drawing toolbar, digitize in one or more polygons in the vicinity of the path or stops, and resolve the route.  
1. Solve again, and the route might change. If it did not change, increase the scale factor or add larger slowdown polygon graphic elements. Unlike the filter scale evaluator, if necessary, the route can still traverse elements in the subset.  
1. Toggle off the AutoUpdateNetworkElementArrayParametersCommand to clear the EID array parameters and stop listening for selection or graphic element changes.  







#### See Also  
[Programming with the ArcGIS Network Analyst extension](http://desktop.arcgis.com/search/?q=Programming%20with%20the%20ArcGIS%20Network%20Analyst%20extension&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[What is the ArcGIS Network Analyst extension?](http://desktop.arcgis.com/search/?q=What%20is%20the%20ArcGIS%20Network%20Analyst%20extension%3F&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[What is a network dataset?](http://desktop.arcgis.com/search/?q=What%20is%20a%20network%20dataset%3F&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[Geodatabase](http://desktop.arcgis.com/search/?q=Geodatabase&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[About the ArcGIS Network Analyst extension tutorial](http://desktop.arcgis.com/search/?q=About%20the%20ArcGIS%20Network%20Analyst%20extension%20tutorial&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[NetworkAnalyst](http://desktop.arcgis.com/search/?q=NetworkAnalyst&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[ArcGIS Network Analyst extension Object Model Diagram](http://desktop.arcgis.com/search/?q=ArcGIS%20Network%20Analyst%20extension%20Object%20Model%20Diagram&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[An overview of the Network Analyst toolbox](http://desktop.arcgis.com/search/?q=An%20overview%20of%20the%20Network%20Analyst%20toolbox&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  


---------------------------------

#### Licensing  
| Development licensing | Deployment licensing | 
| ------------- | ------------- | 
| ArcGIS Desktop Basic: Network Analyst | ArcGIS Desktop Basic: Network Analyst |  
| ArcGIS Desktop Standard: Network Analyst | ArcGIS Desktop Standard: Network Analyst |  
| ArcGIS Desktop Advanced: Network Analyst | ArcGIS Desktop Advanced: Network Analyst |  


