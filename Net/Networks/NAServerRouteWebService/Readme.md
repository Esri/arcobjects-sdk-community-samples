##Route application using the NAServer extension in ArcGIS for Server via a Web service

###Purpose  
This sample is a stand-alone application that shows how to programmatically perform point-to-point routing using the ArcGIS Network Analyst extension in ArcGIS for Server connecting to a Web service catalog. This sample demonstrates some of the key programming patterns when using ArcGIS Network Analyst extension server objects. These programming patterns include the following:Connecting to the Web service.Accessing solver properties from a route analysis layer.Creating stop locations from a set of x,y coordinates.Setting NAServer route solver parameters.Solving to find the best route.Accessing NAServer result objects (map showing the route, driving directions, and input and output NAClasses).   


###Usage
1. Publish a network analysis service (see the following section).  
1. Open the solution file in Visual Studio.  
1. In the Solution Explorer, right-click References, and choose Add Web Reference.  
1. Type in the following URL: http://<server>:6080/arcgis/services/SanFrancisco/MapServer/NAServer?wsdl, then click Go.  
1. In the Web Reference Name control, type the name WebService.  
1. Click Add Reference.  
1. Save the solution.  
1. Build and run the project.  
1. Choose the route solver options.  
1. Click the Find Route button.  
1. Click the different tabs to get the solver results according to your NAServerRouteParams.  

####Publish a network analysis service  
1. Open ArcMap.  
1. Open <Your ArcGIS Developer Kit install folder>\samples\data\SanFrancisco\SanFrancisco.mxd.  
1. Navigate to File > Share As > Service.  
1. Choose Publish a service, and click Next.  
1. Choose the Publisher connection to ArcGIS 10.2 for Server, which will host the published document.   
1. Click Next, then click Continue.  
1. Choose Capabilities and check the Network Analysis check box.  
1. Click the Analyze button and address any significant issues that may be present.  
1. Click the Publish button. Note: If the publishing connection does not have "Copy data to the server when publishing" selected, SanFrancisco.gdb should be present on the server in exactly the same location as on the publishing machine.  









---------------------------------

####Licensing  
| Development licensing | Deployment licensing | 
| :------------- | :------------- | 
| Engine Developer Kit | Engine: Network Analyst |  
| ArcGIS for Desktop Basic: Network Analyst | ArcGIS for Desktop Basic: Network Analyst |  
| ArcGIS for Desktop Standard: Network Analyst | ArcGIS for Desktop Standard: Network Analyst |  
| ArcGIS for Desktop Advanced: Network Analyst | ArcGIS for Desktop Advanced: Network Analyst |  


