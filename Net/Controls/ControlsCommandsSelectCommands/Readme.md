## Custom map selection commands

  <div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">This sample provides a set of Select commands and tools that can be used in conjunction with the MapControl, PageLayoutControl, and ToolbarControl. It is assumed you have previous experience in creating custom tools and commands.</div>
  <div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53"> </div>
  <div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53"> </div>  


<!-- TODO: Fill this section below with metadata about this sample-->
```
Language:              C#, VB
Subject:               Controls
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
1. Build the project.  
1. Do one of the following: a) Add the commands to the ToolbarControl programmatically using the IToolbarControl.AddItem method; b) Use the commands directly with the MapControl or PageLayoutControl programmatically by creating a new instance of a command and passing the MapControl or PageLayoutControl to its OnCreate event; c) Add the commands interactively to the ToolbarControl by clicking the Add from File button in the ToolbarControl's Customizing dialog box, (available through the property pages) and browsing to the *.tlb file; or d) Add the commands interactively to the ToolbarControl by dragging them from the Sample_Select category in the ToolbarControl's Customizing dialog box.  





#### Additional information  
<div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">This sample uses the following:</div>  


#### See Also  
[HookHelper Class](http://desktop.arcgis.com/search/?q=HookHelper%20Class&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[IHookHelper Interface](http://desktop.arcgis.com/search/?q=IHookHelper%20Interface&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  


---------------------------------

#### Licensing  
| Development licensing | Deployment licensing | 
| ------------- | ------------- | 
| Engine Developer Kit | Engine |  
|  | ArcGIS Desktop Basic |  
|  | ArcGIS Desktop Standard |  
|  | ArcGIS Desktop Advanced |  


