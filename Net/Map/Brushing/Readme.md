## Brushing to create a selection

The Brushing tool allows you to use any graphic to create a selection within the map view in ArcMap. You can select a feature by dragging the graphic object so that it overlaps the feature. Selected features update dynamically as the user drags the graphic across the map area. If a graph is created showing the selected features, it gets updated dynamically.   


<!-- TODO: Fill this section below with metadata about this sample-->
```
Language:              C#, VB
Subject:               Map
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
1. Start Visual Studio, open the solution file, then build the project.  
1. Start ArcMap and add some data.   
1. Click the Customize menu and click Customize Mode. The Customize dialog box appears.   
1. Click the Commands tab on the Customize dialog box, find Brushing from Custom Graph Controls, then drag the Brushing tool from the commands to any toolbar in ArcMap. Close the Customize dialog box.  
1. Draw a graphic object (for example, a rectangle) using the Draw tool on the Draw toolbar.   
1. Select the Brushing tool and slowly drag the graphic across the features. You will see features that are overlapping with the graphic get selected while you drag.   
1. To further explore the data, you can add a graph based on the feature. The selected items in the graph update as the graphic is being dragged across the feature.   
1. The Brushing tool is intended to be used for a small dataset. If your dataset is large, the dynamic update is slow. For the best results in using this tool, slowly drag the graphic across the feature to see the dynamic update between the map and graph.   









---------------------------------

#### Licensing  
| Development licensing | Deployment licensing | 
| ------------- | ------------- | 
| ArcGIS Desktop Basic | ArcGIS Desktop Basic |  
| ArcGIS Desktop Standard | ArcGIS Desktop Standard |  
| ArcGIS Desktop Advanced | ArcGIS Desktop Advanced |  


