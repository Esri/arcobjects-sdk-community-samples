## Selection restriction evaluator

This sample demonstrates how to create a dynamic network evaluator that can be used by a restriction attribute in a network dataset. By using this sample, you can dynamically restrict network elements based on the currently selected network source features in ArcMap.  


<!-- TODO: Fill this section below with metadata about this sample-->
```
Language:              C#, VB
Subject:               Networks
Organization:          Esri, http://www.esri.com
Date:                  10/17/2019
ArcObjects SDK:        10.8
Visual Studio:         2017, 2019
.NET Target Framework: 4.5
```

### Resources

* [ArcObjects .NET API Reference online](http://desktop.arcgis.com/en/arcobjects/latest/net/webframe.htm)  
* [Sample Data Download](../../releases)  
* [What's new](http://desktop.arcgis.com/en/arcobjects/latest/net/webframe.htm#91cabc68-2271-400a-8ff9-c7fb25108546.htm)  
* [Download the ArcObjects SDK for .Net from MyEsri.com](https://my.esri.com/)  

### Usage
1. Start Visual Studio, open the solution file, then build the solution.  Be sure to open Visual Studio "As Administrator."  
1. Start ArcCatalog and select an appropriate network dataset to work with.  
1. Add a new restriction attribute to the network dataset.  
1. Set SelectionRestriction as the evaluator type for all participating network sources for this new restriction attribute.   
1. Start ArcMap and add your network dataset to the map.  
1. Click Yes when prompted, to add the network dataset's source feature classes to the map.  
1. Create a new network analysis layer and make sure that your new restriction is selected under the Analysis Settings tab of the analysis layer's property pages.  
1. Create a selection of the features that you want to restrict from your analysis.  
1. Solve your network analysis layer.  







#### See Also  
[Programming with ArcGIS Network Analyst extension](http://desktop.arcgis.com/search/?q=Programming%20with%20ArcGIS%20Network%20Analyst%20extension&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[What is ArcGIS Network Analyst extension?](http://desktop.arcgis.com/search/?q=What%20is%20ArcGIS%20Network%20Analyst%20extension%3F&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[What is a network dataset?](http://desktop.arcgis.com/search/?q=What%20is%20a%20network%20dataset%3F&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[Geodatabase](http://desktop.arcgis.com/search/?q=Geodatabase&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[About the ArcGIS Network Analyst extension tutorial](http://desktop.arcgis.com/search/?q=About%20the%20ArcGIS%20Network%20Analyst%20extension%20tutorial&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[NetworkAnalyst](http://desktop.arcgis.com/search/?q=NetworkAnalyst&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[ArcGIS Network Analyst extension Object Model Diagram](http://desktop.arcgis.com/search/?q=ArcGIS%20Network%20Analyst%20extension%20Object%20Model%20Diagram&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[An overview of the network analyst toolbox](http://desktop.arcgis.com/search/?q=An%20overview%20of%20the%20network%20analyst%20toolbox&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  


---------------------------------

#### Licensing  
| Development licensing | Deployment licensing | 
| ------------- | ------------- | 
| ArcGIS Desktop Basic: Network Analyst | ArcGIS Desktop Basic: Network Analyst |  
| ArcGIS Desktop Standard: Network Analyst | ArcGIS Desktop Standard: Network Analyst |  
| ArcGIS Desktop Advanced: Network Analyst | ArcGIS Desktop Advanced: Network Analyst |  


