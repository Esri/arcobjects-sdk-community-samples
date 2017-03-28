## Layer access

  <div xmlns="http://www.w3.org/1999/xhtml">This sample demonstrates two things:</div>
  <div xmlns="http://www.w3.org/1999/xhtml">
    <font size="2"></font> </div>

*   <font size="2">How to handle REST, SOAP, and binary requests.</font>

*   <font size="2">How to implement layer level controls and conditionally give access to certain layers based on an external permissions file.</font>

  <div xmlns="http://www.w3.org/1999/xhtml"> </div>
  <div xmlns="http://www.w3.org/1999/xhtml">
    <font size="2">As part of the handling of SOAP and binary requests, this samples includes an SOISupport utility class to show how to call into the ArcGIS Server default operation handlers. This sample class can be used in your own SOIs where you need to handle SOAP and binary requests.</font>
  </div>
  <div xmlns="http://www.w3.org/1999/xhtml">
    <font size="2"></font> </div>
  <div xmlns="http://www.w3.org/1999/xhtml">
    <font size="2">Note that this sample is not comprehensive in what operations are intercepted. </font>
    <font size="2">Developers should use this as the basis for learning how to implement security related functionality, but all production code should be carefully vetted to ensure that all appropriate operations are handled and no information is inadvertently leaked or made accessible.</font>
  </div>
  <div xmlns="http://www.w3.org/1999/xhtml"> </div>
  <div xmlns="http://www.w3.org/1999/xhtml">This sample is dependent on a file named permission.json to be present in the directory containing the arcgisoutput server directory. For example, in a default Windows installation this is C:\arcgisserver.</div>
  <div xmlns="http://www.w3.org/1999/xhtml"> </div>
  <div xmlns="http://www.w3.org/1999/xhtml">See [Deploying extensions](http://427963af-7269-4d5b-91ab-fbf84b9a86b0) to learn how to deploy the extension to your server.</div>  


<!-- TODO: Fill this section below with metadata about this sample-->
```
Language:              C#
Subject:               Server
Organization:          Esri, http://www.esri.com
Date:                  3/28/2017
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
1. Deploy LayerAccessSOI.soe to your server (or your own modified version if you have changed the path for the permission.json file).  
1. Enable the SOI on a map service.  
1. Disable caching of layer resources in the ArcGIS Server REST handler.  
1. Consume the service using different users.  
1. Note how certain layers are only visible when the request is authenticated and matches the permissions defined in permission.json.  









---------------------------------

#### Licensing  
| Development licensing | Deployment licensing | 
| ------------- | ------------- | 
|  |  |  


