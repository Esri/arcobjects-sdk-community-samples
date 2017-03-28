## Committing ink sketches using the controls ink commands

  <div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">This sample demonstrates how the Ink Sketch Commit options can be set and how the ink engine environment events are fired on either a desktop or Tablet PC. This sample is used in conjunction with the MapControl, ToolbarControl, and controls commands. </div>  


<!-- TODO: Fill this section below with metadata about this sample-->
```
Language:              C#, VB
Subject:               Controls
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
1. Load some map data and navigate around using the tools and scroll bars in the application.   
1. Use the Ink Sketch Commit Options controls on the application form to set the ink sketch commit strategy (manual or automatic).   
1. Examine the Sketch Report on the application form to identify when the ink is being collected.   





#### Additional information  
<div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">The ink collection and sketches are managed by the singleton object called EngineInkEnvironment. One of its properties, ToolCommitType, is used to indicate whether the ink sketch is committed manually, automatically as a graphic, or automatically as text. Setting this commit strategy is done by clicking the appropriate buttons in the Ink Sketch Options on the application form. </div>  
<div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53"> </div>  
<div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">Automatically committing an ink sketch as text is only available on a Tablet PC where a recognizer can convert the graphic to a TextElement. The application examines whether it is executing on a Tablet PC by making a Windows application programming interface (API) call to the GetSystemMetrics method in the User32.dll. </div>  
<div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53"> </div>  
<div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">Feedback on the ink capture process is provided in the Report Sketch area on the application form. The GraphicsContainer of the MapControl is examined to determine how many InkGraphics are present. The Report Sketch area also indicates whether the ink collector is or is not collecting ink by listening to the OnStart and OnStop events of the EngineInkEnvironmentEvent class. Ink is collected when either the Ink tool, Highlight tool, or Eraser tool have been selected for use.</div>  


#### See Also  
[EngineInkEnvironment Class](http://desktop.arcgis.com/search/?q=EngineInkEnvironment%20Class&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[IEngineInkEnvironment Interface](http://desktop.arcgis.com/search/?q=IEngineInkEnvironment%20Interface&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[IEngineInkEnvironmentEvents Interface](http://desktop.arcgis.com/search/?q=IEngineInkEnvironmentEvents%20Interface&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  


---------------------------------

#### Licensing  
| Development licensing | Deployment licensing | 
| ------------- | ------------- | 
| Engine Developer Kit | Engine |  
|  | ArcGIS Desktop Basic |  
|  | ArcGIS Desktop Standard |  
|  | ArcGIS Desktop Advanced |  


