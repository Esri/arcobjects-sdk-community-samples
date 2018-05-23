## Simple REST SOE with properties

  <div xmlns="http://www.w3.org/1999/xhtml">The purpose of this sample is to show how to use a REST server object extension (SOE) that has one subresource and several properties. This sample also includes custom property pages for ArcMap and ArcGIS Manager. </div>
  <div xmlns="http://www.w3.org/1999/xhtml"> </div>
  <div xmlns="http://www.w3.org/1999/xhtml">The subresource, called layers, is a list of all layers accessible through the associated map service. The SOE has several properties that affect its behavior at run time, such as layerType, which dictates the types of layers the layers subresource will return. Other properties exist to showcase use of a property page in ArcMap and ArcGIS Server Manager.</div>  


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
#### Using this sample  
1. Deploy the NetSimpleRESTSOEWithProperties.soe file to the server.   
1. Enable the SOE on a map service of your choice. Ensure that the map service has at least one feature layer and one raster layer.  
1. After the map service has started with the SOE enabled on it, open the Services Directory and access the http://<server name>:6080/arcgis/rest/services page.  
1. Click the map service on which you enabled your SOE.  
1. Scroll down and click NetSimpleRESTSOEWithProperties listed in the Supported Extensions section. If your REST SOE isn't listed here, log in to the Services Directory as an administrator and clear the REST cache. Repeat steps 3, 4, and 5 as needed.  
1. The NetSimpleRESTSOEWithProperties web page displays the root resource details, such as name and description, along with the Child Resources and Supported Operations sections.  
1. Click the layers subresource. It displays information about all the features layers in JSON format.   

#### Modify property values on the custom property page for ArcGIS Manager  
1. Modify the value of the layerType property so that the layers subresource returns only raster layers. Log in to ArcGIS Server Manager and edit the map service on which you enabled the SOE. On the map service's editing page, click Capabilities, then click Simple REST SOE With Properties. A section called Properties will appear below the SOE. This is a custom property page created for Manager.  
1. Modify other property values as necessary and click Save and Restart.  
1. Access the layers subresource and verify that only raster layers are being returned.   

#### Modify property values on the custom property page for ArcMap  
1. Copy NetSimpleRESTSOEWithProperties.Cat.dll to C:\Temp.   
1. Open the command line window. Navigate to the \Program Files (x86)\Common Files\ArcGIS\bin folder and type ESRIRegAsm.exe C:\Temp\NetSimpleRESTSOEWithProperties.Cat.dll /p:Desktop. Wait for confirmation of successful registration.   
1. Connect to your ArcGIS server in the ArcCatalog window of ArcMap. Open the service properties of the map service on which you enabled the SOE.  
1. Click the ".NET Simple REST SOE With Properties" extension. The layerType and other properties will be displayed in a properties dialog box in the Service Editor.  
1. Modify this property value by choosing one of the layer types, then click OK.  
1. After the service has restarted, check the layers subresource. It will display layers of the type you chose in the Service Editor dialog box in ArcMap.  
1. When your test is done, open the command line window again. Navigate to the \Program Files (x86)\Common Files\ArcGIS\bin folder and type ESRIRegAsm.exe /u C:\Temp\NetSimpleRESTSOEWithProperties.Cat.dll /p:Desktop. Wait for confirmation of successful unregistration.   









---------------------------------

#### Licensing  
| Development licensing | Deployment licensing | 
| ------------- | ------------- | 
|  |  |  


