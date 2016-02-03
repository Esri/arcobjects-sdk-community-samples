##Executing geoprocessing tools in the background

###Purpose  
This sample demonstrates how to run multiple geoprocessing tools in the background of an ArcGIS Engine application. It illustrates how the result of one tool execution can then be used as an input to another tool. It also shows how to provide the user with feedback while the tools are executing by listening to events fired by the geoprocessor.   


###Usage
1. Start Visual Studio, open the solution file, and compile the project.  
1. In the Engine application that opens, select some cities using the Select tool or accept the preselected city of Los Angeles.  
1. Specify a buffer distance or accept the default of 30 miles.  
1. Click the Run button. The geoprocessing analysis starts. Progress is reported in the ListView control, and the final result is added to the map.   
1. While the tools are executing, check that the application is still responsive by navigating around the map.  





####Additional information  
<div xmlns="http://www.w3.org/1999/xhtml">Multiple, unrelated geoprocessing tools can be executed by calling the ExecuteAsync method for each tool from within the same method. For example, you can buffer a selected city by a distance to produce a result, then union the Highways and Zip Codes layer to produce another result. The output from the first tool does not affect the inputs to the second tool.</div>  
<div xmlns="http://www.w3.org/1999/xhtml"> </div>  
<div xmlns="http://www.w3.org/1999/xhtml">Another common scenario is where the output from one tool becomes an input parameter to another tool. In this sample, selected cities are buffered to output a result that is then used to clip features in the Zip Codes layer. Calling the ExecuteAsync method from within the same method does not work in this case since all input parameters must exist prior to tool execution. This sample works around that restriction.</div>  
<div xmlns="http://www.w3.org/1999/xhtml"> </div>  
<div xmlns="http://www.w3.org/1999/xhtml">Each tool is added to a Queue object. The first tool is de-queued and called for execution from within the <font size="2">btnRunGP_Click event handler. The second tool is then called</font><font size="2"> for execution from within the ToolExecuted event handler but only if the first has completed successfully. This approach is scalable, as any number of tools can be added to the queue.</font></div>  




---------------------------------

####Licensing  
| Development licensing | Deployment licensing | 
| :------------- | :------------- | 
| Engine Developer Kit | Engine |  
|  | ArcGIS for Desktop Basic |  
|  | ArcGIS for Desktop Standard |  
|  | ArcGIS for Desktop Advanced |  


