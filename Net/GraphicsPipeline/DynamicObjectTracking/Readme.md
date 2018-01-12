## Dynamic display—tracking dynamic object

  <div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">This sample shows how to use the ArcGIS dynamic display to track a dynamic object and simulate a tracking mode where the map is centered and aligned according to the tracked object. The following items are discussed in this sample:</div>

*   Switching the display into dynamic mode

*   Updating the map visible extent and rotation according to information for the tracked object

*   Listening to the AfterDynamicDraw event to show the location of the tracked object
*   Creating dynamic glyphs from in-memory bitmaps

*   Changing different characters of the dynamic symbol
  <div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53"> </div>
  <div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">The tracking data for this sample gets created synthetically out of an existing feature class. This process might take time before the sample starts running. </div>  


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
1. Start Visual Studio and open the solution file.  
1. Build the solution to make the Track Dynamic Object command .dll file. Use the command in an application with a MapControl and ToolbarControl, such as the MapControlApplication template included with the Visual Studio integration.  
1. Add this command to a ToolbarControl. The command (Track Dynamic Object) is located in the .NET samples category.  
1. Add the provided data to the MapControl. Make sure the file type is set to Shape files (*.shp).  
1. Run the application containing the MapControl and ToolbarControl, then zoom in an area on the roads. The application loads the navigation information from the target feature class and starts navigating (this procedure can take a few seconds).  
1. Click Track Dynamic Object to start tracking.  







#### See Also  
[Dynamic display](http://desktop.arcgis.com/search/?q=Dynamic%20display&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[How dynamic display works](http://desktop.arcgis.com/search/?q=How%20dynamic%20display%20works&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[Best practices for using dynamic display](http://desktop.arcgis.com/search/?q=Best%20practices%20for%20using%20dynamic%20display&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[Walkthrough: Getting started with dynamic display](http://desktop.arcgis.com/search/?q=Walkthrough%3A%20Getting%20started%20with%20dynamic%20display&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[Sample: Dynamic display layer](../../../Net/GraphicsPipeline/MyDynamicLayer)  
[Sample: Dynamic display compass](../../../Net/GraphicsPipeline/DynamicDisplayCompass)  
[How to create a dynamic glyph from a marker symbol](http://desktop.arcgis.com/search/?q=How%20to%20create%20a%20dynamic%20glyph%20from%20a%20marker%20symbol&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  


---------------------------------

#### Licensing  
| Development licensing | Deployment licensing | 
| ------------- | ------------- | 
| Engine Developer Kit | Engine |  
|  | ArcGIS Desktop Basic |  
|  | ArcGIS Desktop Standard |  
|  | ArcGIS Desktop Advanced |  


