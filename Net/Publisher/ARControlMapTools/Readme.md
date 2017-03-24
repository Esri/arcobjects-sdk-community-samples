## ArcReaderControl map tools

  <div xmlns="http://www.w3.org/1999/xhtml">This sample demonstrates how to set the IARControl.CurrentTool property to the MapZoomIn, MapZoomOut, or MapPan tool to navigate around the map view or any of the data frames when the current view is a PageLayout. </div>
  <div xmlns="http://www.w3.org/1999/xhtml"> </div>
  <div xmlns="http://www.w3.org/1999/xhtml">The OnAfterScreenDraw event uses the IARMap.CanUndoExtent and CanRedoExtent properties to determine whether the IARMap.UndoExtent and RedoExtent methods can be used to undo or redo extent changes. The IARMap.SetExtent method is set to the result of the IARMap.GetFullExtent method to zoom to the full extent of the data in the map. </div>  


<!-- TODO: Fill this section below with metadata about this sample-->
```
Language:              C#, VB
Subject:               Publisher
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
1. Compile and run the sample from the provided code.   
1. Enter a valid file path of a Published Map File (PMF) from ArcMap and load.   
1. Click the Map button to display the data view.   
1. Use the tools to navigate around the focus map.   
1. Click the Layout button to display the layout view.  
1. Use the tools to navigate the data in any of the data frames on the page layout.   









---------------------------------

#### Licensing  
| Development licensing | Deployment licensing | 
| ------------- | ------------- | 
| ArcGIS Desktop Basic: Publisher | ArcReader |  
| ArcGIS Desktop Standard: Publisher | Engine |  
| ArcGIS Desktop Advanced: Publisher | ArcGIS Desktop Basic |  
|  | ArcGIS Desktop Standard |  
|  | ArcGIS Desktop Advanced |  


