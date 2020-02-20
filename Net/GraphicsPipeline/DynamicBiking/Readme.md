## Dynamic biking

  <div xmlns="http://www.w3.org/1999/xhtml">This sample shows how to work with the geometry bridge in dynamic display and animate dynamic symbols. This sample can be used to play back any global positioning system (GPS) data saved in a GPS exchange format (GPX) or Garmin history (HST) format. </div>  


<!-- TODO: Fill this section below with metadata about this sample-->
```
Language:              C#, VB
Subject:               Graphics Pipeline
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
1. Start Visual Studio and open the solution file.  
1. Build the solution to make .dll files for the dynamic biking, dynamic biking track mode, and dynamic biking speed commands.   
1. Add the Dynamic Biking toolbar to an application with a MapControl and ToolbarControl, such as the MyDynamicDisplayApp sample located in the .NET samples folder.  
1. Click Add Data, choose the Shapefiles data type on the dialog box, then browse to <your ArcGIS developer kit install location >\Samples\data\SouthernCaliforniaRoadClip. Select SouthernCaliforniaRoadClip.shp and click Open to add it to the map.  
1. Make sure the map is in dynamic mode (see Additional Requirements for details, if you are not using the MyDynamicDisplayApp sample).  
1. Click the Dynamic Biking command, then browse to <your ArcGIS developer kit install location>/Samples/Data/GPSRecording on the dialog box for the Input Biking log file.  
1. Make sure the file type is set to .hst files, then open the biking.hst file.  
1. Zoom in the map around the bike image until the street lines in the city are shown clearly.  
1. Click Dynamic Biking Track mode to track the biking route with the animation of a heart beat.  
1. Change the biking speed by sliding the track bar.  
1. Click the Dynamic Biking command to stop the biking.   
1. To try the GPX format, click the Dynamic Biking command again and browse to the GPSRecording folder on the dialog box for the Input Biking log file.  
1. Make sure the file type is set to .gpx files, then open the run.gpx file.  
1. Change the biking speed by sliding the track bar.  
1. Turn off the dynamic map and close the application.  







#### See Also  
[How to use IGeometryBridge to update dynamic geometries](http://desktop.arcgis.com/search/?q=How%20to%20use%20IGeometryBridge%20to%20update%20dynamic%20geometries&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[Walkthrough: Getting started with dynamic display](http://desktop.arcgis.com/search/?q=Walkthrough%3A%20Getting%20started%20with%20dynamic%20display&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[Dynamic display](http://desktop.arcgis.com/search/?q=Dynamic%20display&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[How dynamic display works](http://desktop.arcgis.com/search/?q=How%20dynamic%20display%20works&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[Sample: Dynamic display layer](../../../Net/GraphicsPipeline/MyDynamicLayer)  
[Sample: Dynamic display—tracking dynamic object](../../../Net/GraphicsPipeline/DynamicObjectTracking)  


---------------------------------

#### Licensing  
| Development licensing | Deployment licensing | 
| ------------- | ------------- | 
| Engine Developer Kit | Engine |  
|  | ArcGIS Desktop Basic |  
|  | ArcGIS Desktop Standard |  
|  | ArcGIS Desktop Advanced |  


