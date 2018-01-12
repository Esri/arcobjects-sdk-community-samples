## Spatial Query REST SOE

  <div xmlns="http://www.w3.org/1999/xhtml">This sample shows a server object extension (SOE) that queries and clips features within a buffer distance and summarizes the resulting area based on a given attribute. The SOE is exposed as a representational state transfer (REST) Web service. It works with map services only.</div>
  <div xmlns="http://www.w3.org/1999/xhtml"> </div>
  <div xmlns="http://www.w3.org/1999/xhtml">All the information needed to deploy the SOE is included in this sample encapsulated in a .soe file. Deploying the SOE from this file does not require you to open Visual Studio. However, you can open the Visual Studio solution included with this sample to explore the coding patterns used in the SOE.</div>
  <div xmlns="http://www.w3.org/1999/xhtml"> </div>
  <div xmlns="http://www.w3.org/1999/xhtml">This sample also includes a simple JavaScript application that consumes the SOE. It lets the user specify a buffer distance and a point on the map. In response, the application draws the clipped geometries and displays a summarized table of areas for the clipped polygons, based on vegetation type. The application assumes you have deployed the SOE on the Yellowstone sample map from the .NET ArcObjects software development kit (SDK). Instructions for testing the SOE based with this dataset are given below.</div>
  <div xmlns="http://www.w3.org/1999/xhtml"> </div>
  <div xmlns="http://www.w3.org/1999/xhtml">The instructions below assume that you have installed the developer kit on the machine running ArcGIS Server Manager. If you installed the developer kit on some other machine, you'll need to copy the .soe file to the machine running Manager, or otherwise make the .soe file visible to Manager by placing it in a shared folder.</div>  


<!-- TODO: Fill this section below with metadata about this sample-->
```
Language:              C#
Subject:               Server
Organization:          Esri, http://www.esri.com
Date:                  11/17/2017
ArcObjects SDK:        10.6
Visual Studio:         2015, 2017
.NET Target Framework: 4.5
```

### Resources

* [ArcObjects .NET API Reference online](http://desktop.arcgis.com/en/arcobjects/latest/net/webframe.htm)  
* [Sample Data Download](../../releases)  
* [What's new](http://desktop.arcgis.com/en/arcobjects/latest/net/webframe.htm#05247c04-bfd9-4e36-ae09-bc6e833c3b14.htm)  
* [Download the ArcObjects SDK for .Net from MyEsri.com](https://my.esri.com/)  

### Usage
#### Deploy the SOE  
1. Log in to ArcGIS Server Manager and click Site.  
1. Click Extensions.  
1. Click Add Extension.  
1. Click Browse and navigate to the .soe file, which by default is located at <ArcGIS DeveloperKit install location>\Samples\ArcObjectsNet\ServerSpatialQueryRESTSOE\CSharp\SpatialQueryREST\bin\Debug\SpatialQueryREST.soe.   
1. Click OK.  

#### Enable the SOE on a service  
1. Start ArcMap and click File > Open.  
1. Browse to or type the location of Yellowstone.mxd, which is located in <ArcGIS Developer Kit Location>\Samples\data\Yellowstone.  
1. Click File > Share As > Service.  
1. Click Save a service definition and click Next.  
1. Choose "No available connection" and check "Include data in service definition when publishing".  
1. Change the Server type to ArcGIS Server.  
1. Leave the Service name as Yellowstone and click Next.  
1. Choose a location where you want to save the service definition, then click Continue.  
1. Click Stage to create the service definition. In the success message, note the path of your service definition (.sd).  
1. Copy the Yellowstone.sd file to the machine running ArcGIS Server Manager.  
1. On the machine running ArcGIS Server Manager, log in to Manager and click Services.  
1. If necessary, click the Manage Services tab.  
1. Click Publish Service.  
1. Click Browse, browse to the location of Yellowstone.sd on the local machine, and click Open. Then click Next.  
1. Accept the default properties for the service by clicking Next.  
1. Click Publish. This creates the USA map service.  
1. On the Services tab of Manager, select the USA map service, then select Capabilities. In the list of available capabilities, find SpatialQueryREST and check the box to enable it. If there is a list of available operations allowed, select all of them.  
1. Click Capabilities and click SpatialQueryREST (be careful not to uncheck the box). You are now viewing the properties page for the service. Ensure that the LayerName is "veg" (no quotes) and that the FieldName is "PRIMARY_" (no quotes).  
1. Click Save, then Restart to restart the service.  

#### Test the SOE in the ArcGIS Server Services Directory  
1. Clear the Representational State Transfer (REST) cache of your Services directory. You can find cache clearing options if you open a web browser and log into http://<server name>:6080/arcgis/rest/admin. Click REST Cache, then click Clear Cache Now.  
1. Access your services directory by opening http://<server name>:6080/arcgis/rest/services in a Web browser.  
1. Click Yellowstone, scroll down to the bottom of the page, and click SpatialQueryREST. You’re now at the root resource page of your SOE.  
1. Click the one operation available, SpatialQuery. You can test this operation by adding some simple JavaScript Object Notation (JSON) parameters in the input boxes.  
1. Type {x:544000,y:4900000} in the location box.  
1. Type 5000 in the distance box.  
1. Click either the SpatialQuery (GET) or SpatialQuery (POST) button. You should see a long JSON response containing the clipped polygon geometries from the spatial query. Scroll to the bottom of the response and you’ll see a list of the total areas for each vegetation type.  

#### Use the SOE in a JavaScript application  
1. Using a text editor, such as Notepad, open the SpatialQueryRESTClient.html file included with this sample. This is a sample application built with the ArcGIS application programming interface (API) for JavaScript that uses the SOE. You must have Internet access to successfully run this application.  
1. Find the line of code, var soeURL = "http://<server name>:6080/arcgis/rest/services/Yellowstone/MapServer/exts/SpatialQueryREST/SpatialQuery";  
1. If necessary, replace the uniform resource locator (URL) in the preceding line of code with the REST URL to your SOE.  
1. Find the line of code, var dynamicMapServiceLayer = new esri.layers.ArcGISDynamicMapServiceLayer("http://<server name>:6080/arcgis/rest/services/Yellowstone/MapServer");  
1. If necessary, replace the URL in the preceding line of code with the REST URL to your Yellowstone map service.  
1. Save the file.  
1. Double-click the SpatialQueryRESTClient.html file in Windows Explorer to run it.  
1. Type a buffer distance, such as 5000, then click anywhere on the map. After a few seconds, you'll see the buffered and clipped features displayed on the map, along with a table summarizing the area of each vegetation type.  







#### See Also  
[Walkthrough: What's in the REST SOE](http://desktop.arcgis.com/search/?q=Walkthrough%3A%20What%27s%20in%20the%20REST%20SOE&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[How to develop the REST SOE](http://desktop.arcgis.com/search/?q=How%20to%20develop%20the%20REST%20SOE&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[How to register the REST SOE](http://desktop.arcgis.com/search/?q=How%20to%20register%20the%20REST%20SOE&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[How to develop a property page for the REST SOE](http://desktop.arcgis.com/search/?q=How%20to%20develop%20a%20property%20page%20for%20the%20REST%20SOE&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[How to enable and test the REST SOE on a service](http://desktop.arcgis.com/search/?q=How%20to%20enable%20and%20test%20the%20REST%20SOE%20on%20a%20service&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[How to use the REST SOE in a Web application](http://desktop.arcgis.com/search/?q=How%20to%20use%20the%20REST%20SOE%20in%20a%20Web%20application&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  


---------------------------------

#### Licensing  
| Development licensing | Deployment licensing | 
| ------------- | ------------- | 
|  |  |  


