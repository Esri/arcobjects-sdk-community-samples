## Extent view

The GxExtentView sample demonstrates how to create a custom view for ArcCatalog. It displays an overview map with a red rectangle on top that delineates the spatial extent of the currently selected dataset. You can add the view as a custom preview, a custom tabbed view, or both.   


<!-- TODO: Fill this section below with metadata about this sample-->
```
Language:              C#, VB
Subject:               Catalog
Organization:          Esri, http://www.esri.com
Date:                  3/28/2017
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
1. Register the CustomGxObject_CS.dll. For more information, see "How to register COM components" in the following "See Also" section in this sample.  
1. Start ArcCatalog and browse to <your ArcGIS Developer Kit installation location>/Samples/Data/World.  
1. Select a dataset in the tree view (such as World Cities.lyr).  
1. Select the Extent view (either through preview or from one of the tabs). An overview map of the world shows with a red rectangle that depicts the spatial extent of your selected dataset. Since you are viewing the extent on a world scale, the rectangle depicting your data might be difficult to see if it does not cover a large geographic area.   







#### See Also  
[How to register COM components](http://desktop.arcgis.com/search/?q=How%20to%20register%20COM%20components&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  


---------------------------------

#### Licensing  
| Development licensing | Deployment licensing | 
| ------------- | ------------- | 
| ArcGIS Desktop Basic | ArcGIS Desktop Basic |  
| ArcGIS Desktop Standard | ArcGIS Desktop Standard |  
| ArcGIS Desktop Advanced | ArcGIS Desktop Advanced |  
| Engine Developer Kit | Engine |  


