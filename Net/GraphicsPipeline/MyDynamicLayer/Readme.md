## Dynamic display layer

  <div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">This sample demonstrates using the ArcGIS Dynamic Display to implement a dynamic layer. A dynamic layer is a custom layer that, in addition to the standard interfaces, implements the IDynamicLayer interface. That interface allows this layer to draw its items into the Dynamic Disiplay and take advantage of graphics card hardware acceleration.</div>
  <div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53"> </div>
  <div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">This sample shows implementation of a dynamic layer through inheritance of base-class 'DynamicLayerBase' which is part of the ESRI.ArcGIS.ADF.BaseClasses namespace. In order to use the 'DynamicLayerBase' class, the user must derive from it and override method 'DrawDynamicLayer()'. </div>
  <div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53"> </div>
  <div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">This layer uses synthetic data to simulate dynamic data. In case of a real-life dynamic layer, the dynamic data might be streamed to the layer by a communication port, Extensible Markup Language (XML) stream, ArcGIS tracking server, or any other live feed. </div>  


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
1. Open the solution using Visual Studio.  
1. Build the solution to make the 'Add Dynamic Layer' command dll.  
1. You will need to use this command in an application with a MapControl and a ToolbarControl such as, the MapControlApplication template included with the Visual Studio Integration.  
1. Add the command to the ToolbarControl. The command can be found in the '.NET Samples' category with the name 'Add Dynamic Layer'.   
1. Run the application and add any layer to use as a background.  
1. Click the 'Add Dynamic Layer' command in order to add the dynamic layer to the map. You will notice that the map will switch into Dynamic-Mode and the layer with 100 dynamic items will get added to the map.  







#### See Also  
[Dynamic display](http://desktop.arcgis.com/search/?q=Dynamic%20display&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[Walkthrough: Getting started with dynamic display](http://desktop.arcgis.com/search/?q=Walkthrough%3A%20Getting%20started%20with%20dynamic%20display&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[How dynamic display works](http://desktop.arcgis.com/search/?q=How%20dynamic%20display%20works&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[Best practices for using dynamic display](http://desktop.arcgis.com/search/?q=Best%20practices%20for%20using%20dynamic%20display&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[Sample: Dynamic display—tracking dynamic object](../../../Net/GraphicsPipeline/DynamicObjectTracking)  
[Sample: Dynamic display compass](../../../Net/GraphicsPipeline/DynamicDisplayCompass)  
[Writing multithreaded ArcObjects code](http://desktop.arcgis.com/search/?q=Writing%20multithreaded%20ArcObjects%20code&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[How to create a dynamic glyph from a marker symbol](http://desktop.arcgis.com/search/?q=How%20to%20create%20a%20dynamic%20glyph%20from%20a%20marker%20symbol&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[Sample: Dynamic logo](../../../Net/GraphicsPipeline/DynamicLogo)  
[Sample: Dynamic heads up display](../../../Net/GraphicsPipeline/DynamicDisplayHUD)  


---------------------------------

#### Licensing  
| Development licensing | Deployment licensing | 
| ------------- | ------------- | 
| Engine Developer Kit | Engine |  
|  | ArcGIS Desktop Basic |  
|  | ArcGIS Desktop Standard |  
|  | ArcGIS Desktop Advanced |  


