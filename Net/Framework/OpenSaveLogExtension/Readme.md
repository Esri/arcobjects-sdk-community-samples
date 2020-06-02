## Extension to listen to document open and save events

A component can listen to events fired by the application when a document is created, opened, or closed. However, no event is fired when a document is saved. To log the time when a document is saved, create a custom extension that implements persistence. This sample shows how to implement an application extension that logs user and date time information when a document is opened or saved.   


<!-- TODO: Fill this section below with metadata about this sample-->
```
Language:              C#, VB
Subject:               Framework
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
1. Open and compile the sample project in Visual Studio. Set up a debug application if needed.   
1. Start ArcMap, ArcScene, or ArcGlobe.  
1. Open or save a document.   
1. Check the status bar for a message with the user name and time information of the event.  







#### See Also  
[How to listen to document events](http://desktop.arcgis.com/search/?q=How%20to%20listen%20to%20document%20events&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  


---------------------------------

#### Licensing  
| Development licensing | Deployment licensing | 
| ------------- | ------------- | 
| ArcGIS Desktop Basic | ArcGIS Desktop Basic |  
| ArcGIS Desktop Standard | ArcGIS Desktop Standard |  
| ArcGIS Desktop Advanced | ArcGIS Desktop Advanced |  


