## Server operation access

  <div xmlns="http://www.w3.org/1999/xhtml">This sample illustrates how to filter access to individual operations available on the map server. Three operations – "find", "identify", and "export" – are checked against the incoming request’s user’s role.</div>
  <div xmlns="http://www.w3.org/1999/xhtml"> </div>
  <div xmlns="http://www.w3.org/1999/xhtml">See [Deploying extensions](http://427963af-7269-4d5b-91ab-fbf84b9a86b0) to learn how to deploy the extension to your server.</div>  


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
* [What's new](http://desktop.arcgis.com/en/arcobjects/latest/net/webframe.htm#91cabc68-2271-400a-8ff9-c7fb25108546.htm)  
* [Download the ArcObjects SDK for .Net from MyEsri.com](https://my.esri.com/)  

### Usage
1. Define at least two new custom roles in you ArcGIS Server site called "gold" and "platinum".  
1. Associate one or more users to those two roles, as well as other roles.  
1. Deploy NetOperationAccessSOI.soe to your server.  
1. Attach the SOI to a map service of your choice.  
1. Optionally, secure the service.  
1. Observe how the find, identify, and export operations are only available when the request is authenticated and the authenticated user is in either the gold or platinum roles.  









---------------------------------

#### Licensing  
| Development licensing | Deployment licensing | 
| ------------- | ------------- | 
|  |  |  


