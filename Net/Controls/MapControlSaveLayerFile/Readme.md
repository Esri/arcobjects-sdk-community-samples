## Save a layer file in a MapControl application

This sample demonstrates how to create and save a layer file in a stand-alone ArcGIS Engine application. In addition to providing the ability to save layer files, this sample also implements a context menu that opens when you right-click a layer in the application's table of contents (TOC). This context menu hosts two commands: Save the current layer as a layer file and Remove the current layer from the map.   


<!-- TODO: Fill this section below with metadata about this sample-->
```
Language:              C#, VB
Subject:               Controls
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
1. Press F5 to run the MapControl application.  
1. Use the AddData command to add a dataset to the map, or as an alternative, use Add MapDocument to add an existing map document to the map.  
1. Right-click a layer in the TOC to open the context menu.  
1. Click the Create Layer File command to save the current layer with its current renderer as a layer file.  
1. Use the Load Layer File command to load the new layer to the map.  







#### See Also  
[How to save a layer file](http://desktop.arcgis.com/search/?q=How%20to%20save%20a%20layer%20file&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  


---------------------------------

#### Licensing  
| Development licensing | Deployment licensing | 
| ------------- | ------------- | 
| Engine Developer Kit | Engine |  
|  | ArcGIS Desktop Basic |  
|  | ArcGIS Desktop Standard |  
|  | ArcGIS Desktop Advanced |  


