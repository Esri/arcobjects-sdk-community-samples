##Create a geoprocessing tool to buffer a layer and retrieve messages

###Purpose  
This sample demonstrates execution of a geoprocessing tool using the .NET geoprocessor assembly. In addition, it demonstrates how to retrieve messages from the geoprocessing framework. To show these concepts, this sample makes a geoprocessing tool that buffers a layer.   


###Usage
1. Start Visual Studio and open the solution.  
1. Build the solution to make the Buffer selected layer command .dll. You will need to use this command in an application with a MapControl and a ToolbarControl, or inside ArcMap.  
1. Add the command to the ToolbarControl. The command can be found in the .NET Samples category with the name, Buffer selected layer.  
1. Run the application and add any feature layer to the map (you can add as many feature layers as you want).  
1. Click the Buffer selected layer command to show the Buffer dialog box.  
1. From the layers drop-down list, select the layer that you want to buffer.  
1. Select the buffer distance and units.  
1. Click Buffer to run the Buffer tool. As the tool executes, note the messages on the messages dialog box.  







####See Also  
[How to run a geoprocessing tool](http://desktop.arcgis.com/search/?q=How%20to%20run%20a%20geoprocessing%20tool&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[Accessing licensing and extensions for the geoprocessor](http://desktop.arcgis.com/search/?q=Accessing%20licensing%20and%20extensions%20for%20the%20geoprocessor&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[How to get returned messages](http://desktop.arcgis.com/search/?q=How%20to%20get%20returned%20messages&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  


---------------------------------

####Licensing  
| Development licensing | Deployment licensing | 
| :------------- | :------------- | 
| Engine Developer Kit | Engine |  
| ArcGIS for Desktop Basic | ArcGIS for Desktop Basic |  
| ArcGIS for Desktop Standard | ArcGIS for Desktop Standard |  
| ArcGIS for Desktop Advanced | ArcGIS for Desktop Advanced |  


