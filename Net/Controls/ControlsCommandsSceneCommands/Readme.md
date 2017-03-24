## Custom scene navigation commands

This sample provides a set of Scene commands and tools that can be used in conjunction with the SceneControl. It is assumed you have previous experience in creating custom tools and commands.   


<!-- TODO: Fill this section below with metadata about this sample-->
```
Language:              C#, VB
Subject:               Controls
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
1. Build the project.  
1. Do one of the following: a) Add the commands to the ToolbarControl programmatically using the IToolbarControl.AddItem method; b) Use the commands directly with the SceneControl programmatically by creating a new instance of a command and passing the SceneControl to its OnCreate event; c) Add the commands interactively to the ToolbarControl by clicking the Add from File button in the ToolbarControl's Customizing dialog box, (available through the property pages) and browsing to the *.tlb file; or d) Add the commands interactively to the ToolbarControl by dragging them from the Sample_SceneControl category in the ToolbarControl's Customizing dialog box.  





#### Additional information  
<div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">This sample uses the following:</div>  


#### See Also  
[SceneHookHelper Class](http://desktop.arcgis.com/search/?q=SceneHookHelper%20Class&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[ISceneHookHelper Interface](http://desktop.arcgis.com/search/?q=ISceneHookHelper%20Interface&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  


---------------------------------

#### Licensing  
| Development licensing | Deployment licensing | 
| ------------- | ------------- | 
| Engine Developer Kit | Engine: 3D Analyst |  
|  | ArcGIS Desktop Basic: 3D Analyst |  
|  | ArcGIS Desktop Standard: 3D Analyst |  
|  | ArcGIS Desktop Advanced: 3D Analyst |  


