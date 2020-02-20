## Simple Image Service REST SOE

  <div xmlns="http://www.w3.org/1999/xhtml">This sample illustrates the basic framework for creating an image service REST server object extension (SOE) that will be hosted as an ArcGIS Server REST SOE. The SOE extends the functionality of an image service with a single operation to access raster's statistics in a mosaic dataset. </div>
  <div xmlns="http://www.w3.org/1999/xhtml"> </div>
  <div xmlns="http://www.w3.org/1999/xhtml">All the information needed to deploy the SOE is included in this sample encapsulated in a .soe file. Deploying the SOE from this file does not require you to open Visual Studio. However, you can open the Visual Studio solution included with this sample to explore the coding patterns used in the SOE.</div>
  <div xmlns="http://www.w3.org/1999/xhtml"> </div>
  <div xmlns="http://www.w3.org/1999/xhtml">The instructions below assume that you have installed the developer kit on the machine running ArcGIS Server Manager. If you installed the developer kit on some other machine, you'll need to copy the .soe file to the machine running Manager, or otherwise make the .soe file visible to Manager by placing it in a shared folder.</div>  


<!-- TODO: Fill this section below with metadata about this sample-->
```
Language:              C#
Subject:               Server
Organization:          Esri, http://www.esri.com
Date:                  10/17/2019
ArcObjects SDK:        10.8
Visual Studio:         2017, 2019
.NET Target Framework: 4.5
```

### Resources

* [ArcObjects .NET API Reference online](http://desktop.arcgis.com/en/arcobjects/latest/net/webframe.htm)  
* [Sample Data Download](../../releases)  
* [What's new](http://desktop.arcgis.com/en/arcobjects/latest/net/webframe.htm#91cabc68-2271-400a-8ff9-c7fb25108546.htm)  
* [Download the ArcObjects SDK for .Net from MyEsri.com](https://my.esri.com/)  

### Usage
#### Deploy the image service SOE  
1. Log in to ArcGIS Server Manager and click Site.  
1. Click Extensions.  
1. Click Add Extension.  
1. Click Browse and navigate to the .soe file, which by default is located at <ArcGIS DeveloperKit install location>\Samples\ArcObjectsNet\ISSOERasterAccess\CSharp\ISSOERasterAccess\bin\Release\ISSOERasterAccess.soe.   
1. Click OK.  

#### Enable the SOE on an image service  
1. Login to ArcGIS Server Manager and click Manage Services.  
1. On the Services tab of Manager, select an image service that is published from a mosaic dataset. In the list of available capabilities, find "Raster Access" and check the box to enable it. If there is a list of available operations allowed, select all of them.  
1. Click the Save and Restart button to restart the service.  

#### Test the SOE in ArcGIS Server Services Directory  
1. Open a browser and navigate to the root REST services endpoint for ArcGIS Server (for example: http://<server name>:6080/arcgis/rest/services). You'll see a list of services, including the USA map service created in the previous section.   
1. Click the image service created in the previous section  
1. Use the following url to access the root resource of the SOE: http://<server name>:<port>/arcgis/rest/services/<name of service>/ImageServer/exts/ISSOERasterAccess.  
1. Click the SOE name. The REST SOE description page displays the description and whether the service support raster item access (whether the service is created from mosaic dataset), and one operation called GetRasterStatistics.   
1. Click the GetRasterStatistics operation and type in a valid ObjectID, e.g. 1 and click either the echo (GET) or echo (POST) button. The operation will return the max, min, mean, and standard deviation values. --- For this to work, raster items in the mosaic dataset needs to have statistics built, you can use "build pyramids and statistics" to achieve this.  









---------------------------------

#### Licensing  
| Development licensing | Deployment licensing | 
| ------------- | ------------- | 
|  |  |  


