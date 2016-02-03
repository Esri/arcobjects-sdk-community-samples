##Geoprocessing events listener

###Purpose  
When using geoprocessing (GP) to execute tools, you usually get the tool execution results when it is done. In many cases, you need to listen to the GP events as they happen, for example, when a toolbox is loaded, or messages received while a tool is executing. This sample demonstrates the implementation of a listener helper that allows you to listen to the various GP events. The TestListner application serves as the client that listens to the GP events.   


###Usage
1. Start Visual Studio and open the solution.  
1. Build the solution to make the GeoprocessorEventHelper class .dll and the TestListner test application.  
1. Right-click the project and select Set as Startup Project to ensure the TestListner project is the start-up project for the solution (notice the messages on the Output window in Visual Studio as they get fired by the geoprocessor). The application creates a random shapefile in the machine's temporary directory.  









---------------------------------

####Licensing  
| Development licensing | Deployment licensing | 
| :------------- | :------------- | 
| Engine Developer Kit | Engine |  
| ArcGIS for Desktop Basic | ArcGIS for Desktop Basic |  
| ArcGIS for Desktop Standard | ArcGIS for Desktop Standard |  
| ArcGIS for Desktop Advanced | ArcGIS for Desktop Advanced |  


