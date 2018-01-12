## Dynamic display animated zoom

This sample shows how to use rubber banding envelopes to animate zooming in and out of the map in dynamic mode.  


<!-- TODO: Fill this section below with metadata about this sample-->
```
Language:              C#, VB
Subject:               Graphics Pipeline
Organization:          Esri, http://www.esri.com
Date:                  11/17/2017
ArcObjects SDK:        10.6
Visual Studio:         2015, 2017
.NET Target Framework: 4.5
```

### Resources

* [ArcObjects .NET API Reference online](http://desktop.arcgis.com/en/arcobjects/latest/net/webframe.htm)  
* [Sample Data Download](../../releases)  
* [What's new](http://desktop.arcgis.com/en/arcobjects/latest/net/webframe.htm#05247c04-bfd9-4e36-ae09-bc6e833c3b14.htm)  
* [Download the ArcObjects SDK for .Net from MyEsri.com](https://my.esri.com/)  

### Usage
1. Start Visual Studio and open the solution.  
1. Build the solution to make the Animated Zoom In and Animated Zoom Out tool .dll files. Use both tools in an application with a MapControl and ToolbarControl, such as the MyDynamicDisplayApp sample located in the .NET samples folder.  
1. Add the Animated Zoom In and Animated Zoom Out tools to the ToolbarControl (located in the .NET samples category).  
1. Run the application.  
1. Click the Add Data command, select the Rasters data type on the dialog box, browse to <your ArcGIS developer kit install location>\Samples\data\wsiearth, then choose the wsiearth.tif file.  
1. Click Open to add the .tif file to the map.  
1. Make sure the map is in dynamic mode (see Additional Requirements in this sample for details if not using the MyDynamicDisplayApp sample).  
1. Click the Animated Zoom In tool, then click and drag an envelope on to the map.  
1. Click the Animated Zoom Out tool, then click and drag an envelope on to the map.  
1. Turn off the dynamic map and close the application.  







#### See Also  
[Dynamic display](http://desktop.arcgis.com/search/?q=Dynamic%20display&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[Persisting cache information for use in dynamic display](http://desktop.arcgis.com/search/?q=Persisting%20cache%20information%20for%20use%20in%20dynamic%20display&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[Limitations for dynamic display](http://desktop.arcgis.com/search/?q=Limitations%20for%20dynamic%20display&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[Sample: Dynamic display layer](../../../Net/GraphicsPipeline/MyDynamicLayer)  


---------------------------------

#### Licensing  
| Development licensing | Deployment licensing | 
| ------------- | ------------- | 
| Engine Developer Kit | Engine |  
|  | ArcGIS Desktop Basic |  
|  | ArcGIS Desktop Standard |  
|  | ArcGIS Desktop Advanced |  


