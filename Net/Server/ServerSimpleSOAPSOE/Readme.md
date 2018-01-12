## Simple SOAP SOE

  <div xmlns="http://www.w3.org/1999/xhtml">This sample illustrates the basic framework for creating a server object extension (SOE) that will be accessed as an ArcGIS Server Simple Object Access Protocol (SOAP) Web service. The SOE extends the functionality of a map service with a single operation to echo input text. </div>
  <div xmlns="http://www.w3.org/1999/xhtml"> </div>
  <div xmlns="http://www.w3.org/1999/xhtml">This sample is divided into the following parts: </div>

*   The SOE implementation, which receives SOAP messages, processes requests, and generates SOAP responses.
*   A simple desktop client to consume the custom SOE via SOAP.
  <div xmlns="http://www.w3.org/1999/xhtml"> </div>
  <div xmlns="http://www.w3.org/1999/xhtml">All the information needed to deploy the SOE is included in this sample encapsulated in a .soe file. Deploying the SOE from this file does not require you to open Visual Studio. However, you can open the Visual Studio solution included with this sample to explore the coding patterns used in the SOE.</div>
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
1. Click Browse and navigate to the .soe file, which by default is located at <ArcGIS DeveloperKit install location>\Samples\ArcObjectsNet\ServerSimpleSOAPSOE\CSharp\SimpleSoapSOE\bin\Debug\SimpleSoapSOE.soe.   
1. Click OK.  

#### Enable the SOE on a service  
1. Start ArcMap and click File > Open.  
1. Browse to or type the location of USA.mxd, which is located in <ArcGIS Developer Kit Location>\Samples\data\Usa.  
1. Click File > Share As > Service.  
1. Click Save a service definition and click Next.  
1. Choose "No available connection" and check "Include data in service definition when publishing".  
1. Change the Server type to ArcGIS Server.  
1. Leave the Service name as USA and click Next.  
1. Choose a location where you want to save the service definition, then click Continue.  
1. Click Stage to create the service definition. In the success message, note the path of your service definition (.sd).  
1. Copy the USA.sd file to the machine running ArcGIS Server Manager.  
1. On the machine running ArcGIS Server Manager, log in to Manager and click Services.  
1. If necessary, click the Manage Services tab.  
1. Click Publish Service.  
1. Click Browse, browse to the location of USA.sd on the local machine, and click Open. Then click Next.  
1. Accept the default properties for the service by clicking Next.  
1. Click Publish. This creates the USA map service.  
1. On the Services tab of Manager, select the USA map service, then select Capabilities. In the list of available capabilities, find ".NET Simple SOAP SOE" and check the box to enable it. If there is a list of available operations allowed, select all of them.  
1. Click Save, then Restart to restart the service.  

#### Use the SOE in a client application  
1. In Visual Studio, open the SimpleSoapClient<vs_version> solution (for example, SimpleSoapClient2010.sln). The <vs_version> references the Visual Studio version of the solution. The application contains a simple Windows form with a button and a text box.   
1. A Web Reference has been added to the project to reference the SOE SOAP endpoint hosted within the ArcGIS Server Web service instance. Open the code behind for Form1 (Form1.cs).   
1. In the click event for the button, change the URL property of the proxy instance to reference the HTTP URL to the map service created in the previous section, then append the path to the Simple SOE (for example: http://<server name>:6080/arcgis/services/USA/MapServer/SimpleSoapSOE), where USA is the map service name on which the Simple SOE has been enabled.   
1. Run the application, type text in the text box, and click Execute. The same text entered is returned from the SOE.   









---------------------------------

#### Licensing  
| Development licensing | Deployment licensing | 
| ------------- | ------------- | 
|  |  |  


