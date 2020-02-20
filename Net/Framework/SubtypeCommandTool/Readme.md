## Custom subtyped command and tool

  <div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">You can create multiple commands or tools with similar functionalities in a single class by implementing ICommandSubType. The primary purpose of this sample is to illustrate how to create a single subtyped command that behaves and appears like multiple commands.</div>  


<!-- TODO: Fill this section below with metadata about this sample-->
```
Language:              C#, VB
Subject:               Framework
Organization:          Esri, http://www.esri.com
Date:                  12/13/2018
ArcObjects SDK:        10.7
Visual Studio:         2015, 2017
.NET Target Framework: 4.5
```

### Resources

* [ArcObjects .NET API Reference online](http://desktop.arcgis.com/en/arcobjects/latest/net/webframe.htm)  
* [Sample Data Download](../../releases)  
* [What's new](http://desktop.arcgis.com/en/arcobjects/latest/net/webframe.htm#91cabc68-2271-400a-8ff9-c7fb25108546.htm)  
* [Download the ArcObjects SDK for .Net from MyEsri.com](https://my.esri.com/)  

### Usage
1. Open and compile the sample project in Visual Studio. Set up a debug application if needed.  
1. The ZoomInCommands class works in ArcMap, ArcScene, or ArcGlobe. Drag and drop the Fixed zoom in x# (C#) or Fixed zoom in x# (VB .NET) command under the .NET Samples category on the Customize dialog box onto the application.  
1. Click the command to perform the fixed zoom in at various levels. If necessary, add data to verify each command is zooming at a different level.  
1. The PolyFeedbackTools class works in ArcMap only. Drag and drop the # sides feedback (C#) or # sides feedback (VB .NET) tool from the .NET Samples category on the Customize dialog box onto the application.  
1. Click to activate the feedback tool and click the map to start a polygon display feedback. The display feedback automatically finishes when the shape has reached the number of sides limit of the tool. The status bar also reports the area of the tracked polygon.  





#### Additional information  
<div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">This sample contains a command class called ZoomInCommands that implements three subtyped commands performing different levels of fixed zoom in, and a tool class called PolyFeedbackTools that implements three subtyped tools tracking different kinds of polygon display feedback (3- to 5-sided). </div>  


#### See Also  
[How to create multiple commands or tools in a single class subtyped command](http://desktop.arcgis.com/search/?q=How%20to%20create%20multiple%20commands%20or%20tools%20in%20a%20single%20class%20subtyped%20command&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[Sample: Recently used files—Command, MultiItem, and ComboBox](../../../Net/Framework/RecentFilesCommands)  


---------------------------------

#### Licensing  
| Development licensing | Deployment licensing | 
| ------------- | ------------- | 
| ArcGIS Desktop Basic | ArcGIS Desktop Basic |  
| ArcGIS Desktop Standard | ArcGIS Desktop Standard |  
| ArcGIS Desktop Advanced | ArcGIS Desktop Advanced |  


