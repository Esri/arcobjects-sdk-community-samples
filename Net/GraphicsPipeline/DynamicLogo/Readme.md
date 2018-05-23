## Dynamic logo

  <div xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53" xmlns="http://www.w3.org/1999/xhtml">This sample demonstrates implementation of a logo that will be shown in both standard display and dynamic display. In this sample, the ESRI logo draws in the lower-left corner of the map display.</div>  


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
* [What's new](http://desktop.arcgis.com/en/arcobjects/latest/net/webframe.htm#91cabc68-2271-400a-8ff9-c7fb25108546.htm)  
* [Download the ArcObjects SDK for .Net from MyEsri.com](https://my.esri.com/)  

### Usage
1. Start Visual Studio and open the solution.  
1. Build the solution to make the DynamicLogo library. Use this command in an application with a MapControl and a ToolbarControl, such as in the MapControl Application template provided in the Visual Studio Integration.  
1. Add the command to the ToolbarControl. The command (Show Logo) is located in the .NET Samples category.  
1. Run the application and add any layer to use as a background.  
1. Click the Show Logo command and add the logo to the map. A logo is added in the lower-left corner of the map. When you switch the map into dynamic mode, the logo remains in place.  







#### See Also  
[Dynamic display](http://desktop.arcgis.com/search/?q=Dynamic%20display&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[How to create a dynamic glyph from a marker symbol](http://desktop.arcgis.com/search/?q=How%20to%20create%20a%20dynamic%20glyph%20from%20a%20marker%20symbol&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[Sample: Dynamic display layer](../../../Net/GraphicsPipeline/MyDynamicLayer)  
[Sample: Dynamic display compass](../../../Net/GraphicsPipeline/DynamicDisplayCompass)  
[Sample: Dynamic display—tracking dynamic object](../../../Net/GraphicsPipeline/DynamicObjectTracking)  


---------------------------------

#### Licensing  
| Development licensing | Deployment licensing | 
| ------------- | ------------- | 
| Engine Developer Kit | Engine |  
|  | ArcGIS Desktop Basic |  
|  | ArcGIS Desktop Standard |  
|  | ArcGIS Desktop Advanced |  


