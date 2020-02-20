## Executing geoprocessing tools in the background

  <div xmlns="http://www.w3.org/1999/xhtml">This sample demonstrates how to run multiple geoprocessing tools in the background of an ArcGIS Engine application. It illustrates how the result of one tool execution can then be used as an input to another tool. It also shows how to provide the user with feedback while the tools are executing by listening to events fired by the geoprocessor.</div>
  <div xmlns="http://www.w3.org/1999/xhtml"> </div>  


<!-- TODO: Fill this section below with metadata about this sample-->
```
Language:              C#, VB
Subject:               Geoprocessing
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
1. Start Visual Studio, open the solution file, and compile the project.  
1. In the Engine application that opens, select some cities using the Select tool or accept the preselected city of Los Angeles.  
1. Specify a buffer distance or accept the default of 30 miles.  
1. Click the Run button. The geoprocessing analysis starts. Progress is reported in the ListView control, and the final result is added to the map.   
1. While the tools are executing, check that the application is still responsive by navigating around the map.  





#### Additional information  
<div xmlns="http://www.w3.org/1999/xhtml">Multiple, unrelated geoprocessing tools can be executed by calling the ExecuteAsync method for each tool from within the same method. For example, you can buffer a selected city by a distance to produce a result, then union the Highways and Zip Codes layer to produce another result. The output from the first tool does not affect the inputs to the second tool.</div>  
<div xmlns="http://www.w3.org/1999/xhtml"> </div>  
<div xmlns="http://www.w3.org/1999/xhtml">Another common scenario is where the output from one tool becomes an input parameter to another tool. In this sample, selected cities are buffered to output a result that is then used to clip features in the Zip Codes layer. Calling the ExecuteAsync method from within the same method does not work in this case since all input parameters must exist prior to tool execution. This sample works around that restriction.</div>  
<div xmlns="http://www.w3.org/1999/xhtml"> </div>  
<div xmlns="http://www.w3.org/1999/xhtml">Each tool is added to a Queue object. The first tool is de-queued and called for execution from within the <font size="2">btnRunGP_Click event handler. The second tool is then called</font><font size="2"> for execution from within the ToolExecuted event handler but only if the first has completed successfully. This approach is scalable, as any number of tools can be added to the queue.</font></div>  




---------------------------------

#### Licensing  
| Development licensing | Deployment licensing | 
| ------------- | ------------- | 
| Engine Developer Kit | Engine |  
|  | ArcGIS Desktop Basic |  
|  | ArcGIS Desktop Standard |  
|  | ArcGIS Desktop Advanced |  


