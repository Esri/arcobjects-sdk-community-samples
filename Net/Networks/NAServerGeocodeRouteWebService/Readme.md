##Routing and geocoding application using the NAServer extension in ArcGIS for Server via a Web service

###Purpose  
This sample is a stand-alone application that shows how to programmatically perform point-to-point routing using the ArcGIS Network Analyst extension in ArcGIS for Server connecting to a Web service catalog. This sample demonstrates some of the key programming patterns when using ArcGIS Network Analyst extension server objects. These programming patterns include the following:Connecting to the Web service.Accessing solver properties from a route analysis layer.Geocoding two addresses.Creating stop locations from the geocoded addresses.Solving to find the best route.Displaying the map showing the route and total impedance of the route.   


###Usage
####Publish a network analysis service  
1. Open ArcMap.  
1. Open <Your ArcGIS Developer Kit install folder>\samples\data\SanFrancisco\SanFrancisco.mxd.  
1. Navigate to File > Share As > Service.  
1. Choose Publish a service, and click Next.  
1. Choose Publisher connection to ArcGIS 10.2 Server, which will host the published document.   
1. Click Next, then click Continue.  
1. Choose Capabilities, and check the Network Analysis check box.  
1. Click the Analyze button and address any significant issues that may be present.  
1. Click the Publish button. Note: If the publishing connection does not have "Copy data to the server when publishing" selected, SanFrancisco.gdb should be present on the server in exactly the same location as on the publishing machine.  

####Publish a geocoding service  
1. Open the Catalog window in ArcMap, navigate to SanFrancisco.gdb, right-click SanFranciscoLocator, and click Share As > Geocode Service.  
1. Type SanFranciscoLocator as the service name and click Next.  
1. Choose Publish a service, and click Next.  
1. Choose Publisher connection to ArcGIS 10.2 Server, which will host the published document.   
1. Click Next, then click Continue.  
1. Click Publish.  

####Add a reference to the service to your .NET solution  
1. Start Visual Studio.  
1. Open the solution file.  
1. In the Solution Explorer, right-click References and choose Add Web Reference.  
1. Type the following URL: http://<server>:6080/arcgis/services/SanFrancisco/MapServer/NAServer?wsdl, then click Go.  
1. Change the Web reference name to WebService.  
1. Click Add Reference.  
1. Save the solution.  
1. Close Visual Studio.  

####Merge Web Service Description Languages (WSDLs) from NAServer and GeocodeServer  
1. Open a Visual Studio command prompt.  
1. Change to the directory where the proxy stub file was generated, <Solution Folder>\Web References\WebService.  
1. For C#, type the following: wsdl.exe /Language:cs /verbose /sharetypes /namespace:GeocodeRoute_WebService.WebService /out:Reference.cs http://<server>:6080/arcgis/services/SanFrancisco/MapServer/NAServer?wsdl http://<server>:6080/arcgis/services/SanFranciscoLocator/GeocodeServer?wsdl  
1. For VB .NET, type the following (change localhost if ArcGIS  for Server is on a different machine): wsdl.exe /Language:VB /verbose /sharetypes /namespace:GeocodeRoute_WebService.WebService /out:Reference.vb http://<server>:6080/arcgis/services/SanFrancisco/MapServer/NAServer?wsdl http://<server>:6080/arcgis/services/SanFranciscoLocator/GeocodeServer?wsdl  

####Run the sample  
1. Open the solution file in Visual Studio.  
1. Build and run the project.  
1. Choose the route solver options.  
1. Click the Find Route button.  
1. Click the different tabs to get the solver results.  









---------------------------------

####Licensing  
| Development licensing | Deployment licensing | 
| :------------- | :------------- | 
| Engine Developer Kit | Engine: Network Analyst |  
| ArcGIS for Desktop Basic: Network Analyst | ArcGIS for Desktop Basic: Network Analyst |  
| ArcGIS for Desktop Standard: Network Analyst | ArcGIS for Desktop Standard: Network Analyst |  
| ArcGIS for Desktop Advanced: Network Analyst | ArcGIS for Desktop Advanced: Network Analyst |  


