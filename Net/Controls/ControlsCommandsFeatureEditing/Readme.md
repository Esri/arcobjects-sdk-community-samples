## Feature editing with the control commands

  <div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">This sample demonstrates feature editing using the control commands in conjunction with the ToolbarControl and MapControl.</div>
  <div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53"> </div>
  <div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">The AddItem method is used within the Form_Load event to add new feature editing commands to the ToolbarControl with their Style set. A Sketch menu and Vertex menu are added to ToolbarMenu's using the AddItem method. Within the MapControl's OnMouseDown event, the PopupMenu method is used to display the appropriate ToolbarMenu when the right mouse button has been clicked.</div>
  <div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53"> </div>
  <div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">The editing properties used by the feature editing commands in the ToolbarControl and the ToolbarMenu are determined by the IEditProperties and IEditProperties2 interface members implemented by the EngineEditor object. The EditProperties and EditProperties2 forms are used to manage and validate changes to these properties by the end user.</div>
  <div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53"> </div>
  <div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">Note: If you are editing features from a shapefile or a personal geodatabase, the minimum license required to initialize this application is an ArcGIS Engine or ArcGIS Desktop Basic license. If you are editing features from an enterprise geodatabase, the minimum license required to initialize this application is an ArcGIS Engine Enterprise GeoDatabase or ArcGIS Desktop Standard license. Before running this application, check the product licenses in the LicenseControl property pages accordingly. </div>  


<!-- TODO: Fill this section below with metadata about this sample-->
```
Language:              C#, VB
Subject:               Controls
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
1. Browse to a map document to load into the MapControl.   
1. Use the commands on the ToolbarControl to sketch new features, update existing features, update parts of a sketch, or update attributes.   
1. Right-click the MapControl to access the Sketch context menu.   
1. Click the Option buttons to change the edit properties.  







#### See Also  
[EngineEditor class](http://desktop.arcgis.com/search/?q=EngineEditor%20class&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[IEngineEditProperties interface](http://desktop.arcgis.com/search/?q=IEngineEditProperties%20interface&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[IEngineEditProperties2 interface](http://desktop.arcgis.com/search/?q=IEngineEditProperties2%20interface&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  


---------------------------------

#### Licensing  
| Development licensing | Deployment licensing | 
| ------------- | ------------- | 
| Engine Developer Kit | Engine |  
|  | ArcGIS Desktop Basic |  
|  | ArcGIS Desktop Standard |  
|  | ArcGIS Desktop Advanced |  


